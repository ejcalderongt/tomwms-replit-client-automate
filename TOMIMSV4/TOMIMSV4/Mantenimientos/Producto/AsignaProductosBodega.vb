Imports DevExpress.XtraEditors

Public Class AsignaProductosBodega

    Private BeListProductos As New List(Of clsBeProducto)

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub AsignaProductosBodega_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            If Not AP.Listar_BodegasByUsuario(cmbBodega) Then
                XtraMessageBox.Show("No hay bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            BeListProductos = clsLnProducto.GetAll()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cmdAsignar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAsignar.ItemClick

        Try

            Dim BeProductoBodega As New clsBeProducto_bodega

            For Each BeProducto In BeListProductos

                lblProgress.Text = "Insertando código: " & BeProducto.Codigo

                BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID + 1
                BeProductoBodega.IdProducto = BeProducto.IdProducto
                BeProductoBodega.IdBodega = cmbBodega.EditValue
                BeProductoBodega.Activo = True
                BeProductoBodega.Sistema = False
                BeProductoBodega.User_agr = AP.UsuarioAp.IdUsuario
                BeProductoBodega.Fec_agr = Date.Now
                BeProductoBodega.User_mod = AP.UsuarioAp.IdUsuario
                BeProductoBodega.Fec_mod = Date.Now

                If clsLnProducto_bodega.Insertar(BeProductoBodega) > 0 Then
                    lblProgress.Text = "Se insertó el código: " & BeProducto.Codigo
                End If

            Next


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class