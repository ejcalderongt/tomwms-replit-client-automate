Imports DevExpress.XtraEditors

Public Class frmConfirmaInventario

    Public gBeInventarioEnc As New clsBeTrans_inv_enc
    Private ListCiclico As New List(Of clsBeTrans_inv_ciclico)

    Private DT As New DataTable("InventarioCiclico")

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

    Private Sub SetDatataTable()

        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Recepciones", GetType(Double))
        DT.Columns.Add("Despachos", GetType(Double))

    End Sub

    Private Sub frmConfirmaInventario_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            SetDatataTable()

            Select Case Modo

                Case TipoTrans.Nuevo

                    Cargar_Datos()
                    Llena_Grid()

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

    Private Sub Cargar_Datos()

        Try

            dtFecha.EditValue = gBeInventarioEnc.Fec_agr
            dtHora.EditValue = gBeInventarioEnc.Fec_agr
            IMS.Listar_BodegasPorPropietario(cmbBodega, gBeInventarioEnc.Idpropietario)

            cmbBodega.EditValue = gBeInventarioEnc.IdBodega

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Llena_Grid()

        Try

            ListCiclico = clsLnTrans_inv_ciclico.GetAllByInventarioEnc(gBeInventarioEnc.Idinventarioenc, gBeInventarioEnc.Fec_agr)

            If ListCiclico.Count > 0 Then

                For Each Obj As clsBeTrans_inv_ciclico In ListCiclico

                    DT.Rows.Add(Obj.Codigo, Obj.Producto, Obj.Recepciones, Obj.Despachos)

                Next

                grdRecepciones.DataSource = DT

            End If

            If GridView1.RowCount > 0 Then

                GridView1.Columns("Recepciones").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Recepciones").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Recepciones").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Recepciones").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("Despachos").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Despachos").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Despachos").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Despachos").SummaryItem.DisplayFormat = "{0:n2}"


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

    Private Sub Acepta_Inventario()

        Try

            Dim SinMov As Integer = 0
            Dim ConMov As Integer = 0
            Dim NoRecalcula As Integer = 0

            If rdStockInventario.Checked Then
                SinMov = 1
            ElseIf rdStockInventarioMovs.Checked Then
                ConMov = 1
            ElseIf rdNoRecalcularStock.Checked Then
                NoRecalcula = 1
            End If

            Dim RegularizaInv As New frmRegularizarInventario(frmRegularizarInventario.TipoTrans.Nuevo) With {
                .gBeInventario = gBeInventarioEnc,
                .SinMovs = SinMov,
                .ConMovs = ConMov,
                .NoRecal = NoRecalcula}

            RegularizaInv.ShowDialog()
            RegularizaInv.Dispose()

            If gBeInventarioEnc.Regularizado And gBeInventarioEnc.Estado.ToUpper = "FINALIZADO" Then
                Close()
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

    Private Sub cmdCancelar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCancelar.ItemClick
        Close()
    End Sub

    Private Sub cmdAceptar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAceptar.ItemClick
        Acepta_Inventario()
    End Sub
End Class