Imports DevExpress.XtraEditors

Public Class frmDatosProducto

    Public Prod As clsBeProducto

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

    Private Sub Cargar_Datos_Producto()

        Try

            txtCodigo.Text = Prod.Codigo
            txtProducto.Text = Prod.Nombre

            dtFechaVence.EditValue = Now
            txtLote.Text = ""
            txtCantidad.Value = 0

            If Prod.Control_lote And Prod.Control_vencimiento Then
                lblLote.Visible = True
                txtLote.Visible = True
                lblVence.Visible = True
                dtFechaVence.Visible = True
            ElseIf Prod.Control_lote Then
                lblLote.Visible = True
                txtLote.Visible = True
            ElseIf Prod.Control_vencimiento Then
                lblVence.Visible = True
                dtFechaVence.Visible = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmDatosProducto_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cargar_Datos_Producto()
    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If Prod.Control_lote Then

                If txtLote.Text = "" Then
                    XtraMessageBox.Show("Lote vacío, por favor ingrese un lote", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Datos_Correctos = False
                Else
                    Datos_Correctos = True
                End If

            End If

            If Prod.Control_vencimiento Then

                If dtFechaVence.EditValue < Now Then

                    If XtraMessageBox.Show("La fecha de vencimiento es menor a la fecha actual, ¿Está seguro de guardar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Datos_Correctos = True
                    End If
                End If

            End If

            If txtCantidad.Value = 0 Then
                XtraMessageBox.Show("Ingrese una cantidad mayor a 0.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Datos_Correctos = False
            Else
                Datos_Correctos = True
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        If Datos_Correctos() Then
            Prod.Lote = txtLote.Text
            Prod.FechaVence = dtFechaVence.EditValue
            Prod.Cantidad = txtCantidad.Value
            Close()
        End If
    End Sub

End Class