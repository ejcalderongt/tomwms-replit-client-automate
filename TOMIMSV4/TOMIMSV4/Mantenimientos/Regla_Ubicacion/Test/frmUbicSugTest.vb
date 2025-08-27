Imports DevExpress.XtraEditors

Public Class frmUbicSugTest

    Dim ubs As clsLnTrans_ubicsug

    Dim bodid, estid, prodid, presid, cant As Integer
    Dim lote As String

    Private Sub frmUbicSugTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Get_Ubicaciones_Sugeridas()
    End Sub

    Private Sub Get_Ubicaciones_Sugeridas()

        Try

            bodid = 1
            estid = 1
            prodid = 1
            presid = 2
            cant = 1100
            lote = "1707210001"

            Dim BeProducto As New clsBeProducto
            BeProducto.IdProducto = 1
            BeProducto = clsLnProducto.Get_Single_By_IdProducto(BeProducto.IdProducto)

            BeProducto.IdProductoBodega = clsLnProducto.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto, bodid)

            Dim BePresentacion As New clsBeProducto_Presentacion
            BePresentacion.IdPresentacion = 2
            BePresentacion = clsLnProducto_presentacion.GetSingle(BePresentacion.IdPresentacion)

            Dim BeEstadoProd As New clsBeProducto_estado
            BeEstadoProd.IdEstado = estid
            BeEstadoProd = clsLnProducto_estado.GetSingle(BeEstadoProd.IdEstado)

            ubs = New clsLnTrans_ubicsug()
            clsLnTrans_ubicsug.pIdBodega = bodid
            clsLnTrans_ubicsug.pBeProducto = BeProducto
            clsLnTrans_ubicsug.pBePresentacion = BePresentacion
            clsLnTrans_ubicsug.pBeEstado = BeEstadoProd
            clsLnTrans_ubicsug.pLote = lote
            clsLnTrans_ubicsug.Get_Ubicaciones_Sugeridas(cant, 0)

            dgridFiltro.DataSource = clsLnTrans_ubicsug.lUbicacionesSugeridas

            Label1.Text = dgridFiltro.Rows.Count
            Label2.Text = cant

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmUbicSugTest_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ubs.Dispose()
    End Sub

End Class