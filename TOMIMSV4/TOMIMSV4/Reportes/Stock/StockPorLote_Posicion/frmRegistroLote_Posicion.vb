Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmRegistroLote_Posicion


    Public pBeStockRes As New clsBeVW_stock_res
    Public pBeTrans_re_oc As New clsBeTrans_re_oc

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

    Private Sub frmRegistroLote_Posicion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Carga_Datos()
    End Sub

    Private Sub Carga_Datos()

        Dim Presentacion As New clsBeProducto_Presentacion

        Try

            lblIngreso.Text = pBeTrans_re_oc.IdOrdenCompraEnc
            lblCodigo.Text = pBeStockRes.IdStock
            lblRecepcion.Text = pBeStockRes.IdRecepcionEnc
            txtCodigo.Text = pBeStockRes.Codigo_Producto
            txtProducto.Text = pBeStockRes.Nombre_Producto
            txtUm.Text = pBeStockRes.UMBas
            chkPalletNoStandar.Checked = pBeStockRes.Pallet_No_Estandar
            If pBeStockRes.IdPresentacion > 0 Then
                Presentacion.IdPresentacion = pBeStockRes.IdPresentacion
                clsLnProducto_presentacion.GetSingle(Presentacion)
                txtPresentacion.Text = Presentacion.Nombre
            End If
            txtCantidad.Value = pBeStockRes.Posiciones
            txtEstado.Text = pBeStockRes.NomEstado
            dtRece.EditValue = pBeStockRes.Fecha_ingreso

            txtCantidad.Enabled = pBeStockRes.Pallet_No_Estandar

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        cmdActualizar.Enabled = False

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmdActualizar.Enabled = True
            Close()
        Else
            cmdActualizar.Enabled = True
            XtraMessageBox.Show("No fue posible actualizar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            pBeStockRes.IdStock = lblCodigo.Text
            pBeStockRes.Pallet_No_Estandar = chkPalletNoStandar.Checked
            pBeStockRes.Posiciones = txtCantidad.Text

            If chkPalletNoStandar.Checked AndAlso pBeStockRes.Posiciones = 1 Then
                XtraMessageBox.Show("Configuró el Pallet como no Standar, pero solo usa una posición!")
                Actualizar = False
            Else
                clsLnVW_stock_res.Actualizar_Pallet_No_Standar(pBeStockRes)
                Actualizar = True
            End If

        Catch ex As Exception

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub chkPalletNoStandar_CheckedChanged(sender As Object, e As EventArgs) Handles chkPalletNoStandar.CheckedChanged

        Dim Edit As CheckEdit = CType(sender, CheckEdit)
        Select Case Edit.Checked
            Case True
                txtCantidad.Enabled = True
            Case False
                txtCantidad.Enabled = False
        End Select

    End Sub

    Private Sub cmdCancelar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCancelar.ItemClick
        Close()
    End Sub

End Class