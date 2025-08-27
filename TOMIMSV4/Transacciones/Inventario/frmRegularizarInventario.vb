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

    Private Sub Cargar_Datos()

        Dim clsTransaccion As New clsTransaccion
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

            grdRegularizar.DataSource = clsLnTrans_inv_ciclico.Get_All_By_Comparacion_Inventario_A_Regularizar(gBeInventario.Idinventarioenc,
                                                                                                               clsTransaccion.lConnection,
                                                                                                               clsTransaccion.lTransaction)

            If GridView1.RowCount > 0 Then

                GridView1.OptionsView.ShowFooter = True
                GridView1.BestFitColumns(True)

                'GridView1.Columns("Código").Group()

                Dim item As New GridGroupSummaryItem() _
                With {.FieldName = "CantidadConteo",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("CantidadConteo")}
                GridView1.GroupSummary.Add(item)

                Dim item1 As New GridGroupSummaryItem() _
                With {.FieldName = "PesoConteo",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("PesoConteo")}
                GridView1.GroupSummary.Add(item1)

                Dim item2 As New GridGroupSummaryItem() _
                With {.FieldName = "CantidadStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("CantidadStock")}
                GridView1.GroupSummary.Add(item2)

                Dim item3 As New GridGroupSummaryItem() _
                With {.FieldName = "PesoStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("PesoStock")}
                GridView1.GroupSummary.Add(item3)

                Dim item4 As New GridGroupSummaryItem() _
                With {.FieldName = "Entradas_Salidas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Entradas_Salidas")}
                GridView1.GroupSummary.Add(item4)

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

                Dim item8 As New GridGroupSummaryItem() _
                With {.FieldName = "Entradas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Entradas")}
                GridView1.GroupSummary.Add(item8)

                Dim item9 As New GridGroupSummaryItem() _
                With {.FieldName = "Salidas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Salidas")}
                GridView1.GroupSummary.Add(item9)

                Dim item10 As New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad_Reservada_UmBas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_Reservada_UmBas")}
                GridView1.GroupSummary.Add(item10)

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                GridView1.Columns("CantidadConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadConteo").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadConteo").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("CantidadStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadStock").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadStock").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Entradas_Salidas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Entradas_Salidas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Entradas_Salidas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Entradas_Salidas").SummaryItem.DisplayFormat = "{0:n6}"

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

                GridView1.Columns("Entradas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Entradas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Entradas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Entradas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Salidas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Salidas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Salidas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Salidas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Reservada_UmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Reservada_UmBas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad_Reservada_UmBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Reservada_UmBas").SummaryItem.DisplayFormat = "{0:n6}"

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

            grdInventarioConReserva.DataSource = DTNoRegularizar

            If (grdvInventarioConReserva.RowCount > 0) Then

                grdvInventarioConReserva.OptionsView.ShowFooter = True
                grdvInventarioConReserva.BestFitColumns(True)

                'grdvInventarioConReserva.Columns("Código").Group()

                Dim item As New GridGroupSummaryItem() _
                With {.FieldName = "CantidadConteo",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("CantidadConteo")}
                grdvInventarioConReserva.GroupSummary.Add(item)

                Dim item1 As New GridGroupSummaryItem() _
                With {.FieldName = "PesoConteo",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("PesoConteo")}
                grdvInventarioConReserva.GroupSummary.Add(item1)

                Dim item2 As New GridGroupSummaryItem() _
                With {.FieldName = "CantidadStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("CantidadStock")}
                grdvInventarioConReserva.GroupSummary.Add(item2)

                Dim item3 As New GridGroupSummaryItem() _
                With {.FieldName = "PesoStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("PesoStock")}
                grdvInventarioConReserva.GroupSummary.Add(item3)

                Dim item4 As New GridGroupSummaryItem() _
                With {.FieldName = "Entradas_Salidas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("Entradas_Salidas")}
                grdvInventarioConReserva.GroupSummary.Add(item4)

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

                Dim item8 As New GridGroupSummaryItem() _
                With {.FieldName = "Entradas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("Entradas")}
                grdvInventarioConReserva.GroupSummary.Add(item8)

                Dim item9 As New GridGroupSummaryItem() _
                With {.FieldName = "Salidas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("Salidas")}
                grdvInventarioConReserva.GroupSummary.Add(item9)

                Dim item10 As New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad_Reservada_UmBas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("Cantidad_Reservada_UmBas")}
                grdvInventarioConReserva.GroupSummary.Add(item10)

                lblRegs.Caption = String.Format("Registros: {0}", grdvInventarioConReserva.RowCount)

                grdvInventarioConReserva.Columns("CantidadConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("CantidadConteo").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("CantidadConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("CantidadConteo").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("CantidadStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("CantidadStock").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("CantidadStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("CantidadStock").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("Entradas_Salidas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("Entradas_Salidas").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("Entradas_Salidas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("Entradas_Salidas").SummaryItem.DisplayFormat = "{0:n6}"

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

                grdvInventarioConReserva.Columns("Entradas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("Entradas").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("Entradas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("Entradas").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("Salidas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("Salidas").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("Salidas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("Salidas").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("Cantidad_Reservada_UmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("Cantidad_Reservada_UmBas").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("Cantidad_Reservada_UmBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("Cantidad_Reservada_UmBas").SummaryItem.DisplayFormat = "{0:n6}"

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

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
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

        Dim reportHeader As String = vbNewLine & "Reporte de Invetario Cíclico"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub rdStockInventario_CheckedChanged(sender As Object, e As EventArgs) Handles rdStockInventario.CheckedChanged
        Cargar_Datos()
    End Sub

    Private Sub rdStockInventarioMovs_CheckedChanged(sender As Object, e As EventArgs) Handles rdStockInventarioMovs.CheckedChanged
        Cargar_Datos()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try

            Dim View As GridView = sender
            Dim CantidadCont As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("CantidadConteo"))
            Dim CantDif As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("DiferenciaCantidad"))

            If e.Column.FieldName = "DiferenciaCantidad" Then

                If Val(CantDif) <> 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf val(CantDif) = 0 Then
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
                    clsLnTrans_inv_ciclico.Actualizar_NuevoStock_By_IdInventarioEnc(gBeInventario.Idinventarioenc, 0)

                    Cargar_Datos()

                Case TipoTrans.Editar

            End Select

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

            lOperaciones.Clear()

            ' Carga datos del inventario cíclico
            ListCiclico = clsLnTrans_inv_ciclico.Get_All_By_IdInventarioEncAgrupado(gBeInventario.Idinventarioenc,
                                                                                    clsTrans.lConnection,
                                                                                    clsTrans.lTransaction)

            Dim vIdPropietarioBodega As Integer = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(gBeInventario.IdBodega,
                                                                                                                          gBeInventario.Idpropietario,
                                                                                                                          clsTrans.lConnection,
                                                                                                                          clsTrans.lTransaction)
            Dim vIdProducto As Integer = 0

            Dim ajustesCantidad = ListCiclico.Where(Function(x) (((x.Cant_stock <> x.Cantidad) And
                                                                x.Fecha_vence = x.Fecha_vence_stock AndAlso
                                                                (x.Lote = x.Lote_stock) AndAlso
                                                                (x.IdProductoEstado = x.IdProductoEst_nuevo OrElse
                                                                x.IdProductoEst_nuevo = 0) AndAlso x.Regularizar = True AndAlso
                                                                 x.Cantidad >= x.Cantidad_Reservada_UMBas)) OrElse
                                                                (x.Cant_stock = x.Cantidad AndAlso
                                                                 x.Nuevo_Stock = -1 AndAlso
                                                                 x.Regularizar = True)).ToList()

            Dim ListaExcluyente = ListCiclico.ToList

            Dim ajustesVencimiento = ListaExcluyente.Where(Function(x) x.Fecha_vence <> x.Fecha_vence_stock AndAlso x.Regularizar = True).ToList()
            ListaExcluyente = ListaExcluyente.Except(ajustesVencimiento).ToList

            Dim ajustesLote = ListaExcluyente.Where(Function(x) x.Lote <> x.Lote_stock AndAlso x.Regularizar = True).ToList()
            ListaExcluyente = ListaExcluyente.Except(ajustesLote).ToList

            Dim ajustesEstado = ListaExcluyente.Where(Function(x) x.IdProductoEstado <> x.IdProductoEst_nuevo AndAlso
                                                                  x.IdProductoEst_nuevo <> 0 AndAlso x.Regularizar = True).ToList()

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

            If ajustesCantidad.Any() Then

                ' Dividir ajustes positivos y negativos
                Dim ajustesPositivos = ajustesCantidad.Where(Function(x) (x.Cantidad > x.Cant_stock OrElse x.IdUbicacion_nuevo <> 0) AndAlso x.Regularizar = True).ToList()
                Dim ajustesNegativos = ajustesCantidad.Where(Function(x) x.Cantidad < x.Cant_stock AndAlso x.Regularizar = True).ToList().Except(ajustesPositivos).ToList()

                If ajustesPositivos.Any() Then
                    ProcesarAjustePorTipo(ajustesPositivos, clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo, clsTrans, vIdPropietarioBodega, "Positivo")
                End If

                If ajustesNegativos.Any() Then
                    ProcesarAjustePorTipo(ajustesNegativos, clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo, vIdPropietarioBodega, "Negativo", clsTrans)
                End If

            End If

            ' Regularizar inventario
            clsLnTrans_inv_ciclico.Regularizar_Inventario(gBeInventario,
                                                          ListStockNuevo,
                                                          ListMovs,
                                                          AP.UsuarioAp,
                                                          lBeTransAjusteDet,
                                                          lOperaciones,
                                                          AP.Bodega.codigo_bodega_erp,
                                                          clsTrans.lConnection,
                                                          clsTrans.lTransaction)

            ' Confirmar transacción
            clsTrans.Commit_Transaction()

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
            Dim vIdMotivoAjuste As Integer = 0
            Dim DT As New DataTable

            DT = clsLnAjuste_motivo.Listar()

            If DT IsNot Nothing Then
                If DT.Rows.Count > 0 Then
                    vIdMotivoAjuste = DT.Rows(0).Item("IdMotivoAjuste")
                End If
                DT.Dispose()
            End If

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

            Dim BodegaERP As Integer = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.codigo_bodega_erp,
                                                                           lConnection,
                                                                           lTransaction)

            pBeAjusteDet.IdBodegaERP = Val(BodegaERP)

            pBeAjusteDet.lic_plate = BeTransInvCiclico.lic_plate

            Dim vProducto As New clsBeProducto
            vProducto = clsLnProducto.Get_Single_By_IdProductoBodega(BeTransInvCiclico.IdProductoBodega,
                                                                     lConnection,
                                                                     lTransaction)

            pBeAjusteDet.Codigo_producto = vProducto.IdProducto
            pBeAjusteDet.Codigo_producto = vProducto.Codigo
            pBeAjusteDet.Nombre_producto = vProducto.Nombre

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
        Dim DT As New DataTable

        Try

            ' Crear encabezado para el ajuste
            pBeTransAjustEnc = New clsBeTrans_ajuste_enc()
            pBeTransAjustEnc.Idajusteenc = clsLnTrans_ajuste_enc.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
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

            DT = clsLnAjuste_motivo.Listar()

            pBeAjusteDet.IdMotivoAjuste = 0

            If DT IsNot Nothing Then
                If DT.Rows.Count > 0 Then
                    vIdMotivoAjuste = DT.Rows(0).Item("IdMotivoAjuste")
                End If
                DT.Dispose()
            End If

            ' Insertar encabezado
            clsLnTrans_ajuste_enc.Insertar(pBeTransAjustEnc, clsTrans.lConnection, clsTrans.lTransaction)

            ' Procesar los detalles de los ajustes
            For Each BeTransInvCiclico In ajustes
                ' Crear detalle del ajuste
                Dim pBeAjusteDet As New clsBeTrans_ajuste_det()
                pBeAjusteDet.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                pBeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.Idajusteenc
                pBeAjusteDet.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                pBeAjusteDet.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                pBeAjusteDet.IdPresentacion = BeTransInvCiclico.IdPresentacion
                pBeAjusteDet.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                pBeAjusteDet.IdUbicacion = BeTransInvCiclico.IdUbicacion
                pBeAjusteDet.Cantidad_original = IIf(BeTransInvCiclico.Cant_stock = -1, 0, BeTransInvCiclico.Cant_stock)
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
                End If

                pBeAjusteDet.Peso_original = BeTransInvCiclico.Peso
                pBeAjusteDet.Peso_nuevo = BeTransInvCiclico.Peso
                pBeAjusteDet.lic_plate = BeTransInvCiclico.lic_plate
                pBeAjusteDet.IdStock = BeTransInvCiclico.IdStock

                Dim vProducto As New clsBeProducto
                vProducto = clsLnProducto.Get_Single_By_IdProductoBodega(BeTransInvCiclico.IdProductoBodega, clsTrans.lConnection, clsTrans.lTransaction)

                pBeAjusteDet.Codigo_producto = vProducto.IdProducto
                pBeAjusteDet.Codigo_producto = vProducto.Codigo
                pBeAjusteDet.Nombre_producto = vProducto.Nombre
                pBeAjusteDet.IdMotivoAjuste = vIdMotivoAjuste

                Dim BodegaERP As Integer = clsLnCliente.Get_IdBodega_By_Codigo(Val(AP.Bodega.codigo_bodega_erp),
                                                                                   clsTrans.lConnection,
                                                                                   clsTrans.lTransaction)

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
                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                    pBeMovs.IdEstadoOrigen = BeTransInvCiclico.IdProductoEst_nuevo
                    pBeMovs.IdEstadoDestino = BeTransInvCiclico.IdProductoEst_nuevo
                Else
                    pBeMovs.IdEstadoOrigen = BeTransInvCiclico.IdProductoEstado
                    pBeMovs.IdEstadoDestino = BeTransInvCiclico.IdProductoEstado
                End If
                pBeMovs.IdPresentacion = BeTransInvCiclico.IdPresentacion
                pBeMovs.IdEstadoOrigen = BeTransInvCiclico.IdProductoEstado
                pBeMovs.IdEstadoDestino = BeTransInvCiclico.IdProductoEst_nuevo
                pBeMovs.IdUnidadMedida = Producto.IdUnidadMedidaBasica
                pBeMovs.IdTipoTarea = IdTipoTarea
                pBeMovs.IdBodegaDestino = AP.IdBodega
                pBeMovs.IdRecepcion = 0
                pBeMovs.IdRecepcionDet = 0
                pBeMovs.Serie = ""
                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote Then
                    pBeMovs.Lote = BeTransInvCiclico.Lote
                Else
                    pBeMovs.Lote = BeTransInvCiclico.Lote_stock
                End If
                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento Then
                    pBeMovs.Fecha_vence = BeTransInvCiclico.Fecha_vence
                Else
                    pBeMovs.Fecha_vence = BeTransInvCiclico.Fecha_vence_stock
                End If
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
                        pBeMovs.Cantidad = Math.Round((BeTransInvCiclico.Cantidad - IIf(BeTransInvCiclico.Cant_stock = -1, 0, BeTransInvCiclico.Cant_stock)), 6)
                    End If
                ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then
                    pBeMovs.Cantidad = Math.Round((BeTransInvCiclico.Cant_stock - BeTransInvCiclico.Cantidad), 6)
                ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                    pBeMovs.Cantidad = BeTransInvCiclico.Cantidad

                    If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTEPI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENCEPI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.CESTI
                    End If

                End If

                pBeMovs.Cantidad_hist = IIf(BeTransInvCiclico.Cant_stock = -1, 0, BeTransInvCiclico.Cant_stock)
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
                    If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                        pBeMovsInverso.IdEstadoOrigen = BeTransInvCiclico.IdProductoEst_nuevo
                        pBeMovsInverso.IdEstadoDestino = BeTransInvCiclico.IdProductoEst_nuevo
                    Else
                        pBeMovsInverso.IdEstadoOrigen = BeTransInvCiclico.IdProductoEstado
                        pBeMovsInverso.IdEstadoDestino = BeTransInvCiclico.IdProductoEstado
                    End If
                    pBeMovsInverso.IdUnidadMedida = Producto.IdUnidadMedidaBasica
                    pBeMovsInverso.IdTipoTarea = IdTipoTarea
                    pBeMovsInverso.IdBodegaDestino = AP.IdBodega
                    pBeMovsInverso.IdRecepcion = 0
                    pBeMovsInverso.IdRecepcionDet = 0
                    pBeMovsInverso.Serie = ""
                    If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote Then
                        pBeMovsInverso.Lote = BeTransInvCiclico.Lote
                    Else
                        pBeMovsInverso.Lote = BeTransInvCiclico.Lote_stock
                    End If
                    If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento Then
                        pBeMovsInverso.Fecha_vence = BeTransInvCiclico.Fecha_vence
                    Else
                        pBeMovsInverso.Fecha_vence = BeTransInvCiclico.Fecha_vence_stock
                    End If
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

                    If BeTransInvCiclico.Fecha_vence_stock <> BeTransInvCiclico.Fecha_vence Then
                        pBeStock.Fecha_vence = BeTransInvCiclico.Fecha_vence
                    Else
                        pBeStock.Fecha_vence = BeTransInvCiclico.Fecha_vence_stock
                    End If

                    If BeTransInvCiclico.Lote <> BeTransInvCiclico.Lote_stock AndAlso BeTransInvCiclico.Lote <> "" Then
                        pBeStock.Lote = BeTransInvCiclico.Lote
                    End If

                    lOperaciones.Add(New KeyValuePair(Of Integer, Integer)(pBeStock.IdStock, tipoAjuste))

                Else

                    pBeStock = New clsBeStock()
                    pBeStock.IdBodega = BeTransInvCiclico.IdBodega
                    pBeStock.IdStock = clsLnStock.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
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

                    clsLnStock.Insertar(pBeStock, clsTrans.lConnection, clsTrans.lTransaction)

                    lOperaciones.Add(New KeyValuePair(Of Integer, Integer)(pBeStock.IdStock, tipoAjuste))

                End If

                ListStockNuevo.Add(pBeStock)

            Next

        Catch ex As Exception
            Throw New Exception($"Error en ProcesarAjustePorTipo: {ex.Message}")
        End Try

    End Sub

End Class
