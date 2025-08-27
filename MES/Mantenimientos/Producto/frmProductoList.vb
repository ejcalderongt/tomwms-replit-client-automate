Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraWaitForm
Imports TOMWMS

Public Class frmProductoList

    Public Property IsLoading As Boolean = True
    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub New()
        InitializeComponent()
        IsLoading = False
    End Sub

    Private Sub frmProductoList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Listando productos...")

            Cargar()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Sub

    Private Sub Cargar()
        Try

            If IsLoading Then Exit Sub

            Dim pTabla As New DataTable
            pTabla = clsLnQT_Producto_Sucursal.Listar()

            If pTabla IsNot Nothing AndAlso pTabla.Rows.Count > 0 Then

                gridProducto.DataSource = pTabla

                If gridViewProducto.Columns.Count > 0 Then
                    gridViewProducto.BestFitColumns()
                    gridViewProducto.OptionsBehavior.Editable = False
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gridProducto_DoubleClick(sender As Object, e As EventArgs) Handles gridProducto.DoubleClick
        Try
            Procesar_Registro()
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Procesar_Registro()

        Try

            Try
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Sucursal")
            Catch ex As Exception
            End Try

            If gridViewProducto.RowCount > 0 Then

                Dim Dr As DataRowView = gridViewProducto.GetFocusedRow

                Dim pObjProducto = New clsBeQTProducto_Sucursal


                Dim IdProducto As Integer = Integer.Parse(Dr.Item("IdProducto"))
                pObjProducto.IdProducto = IdProducto
                clsLnQT_Producto_Sucursal.GetSingle(pObjProducto)


                Dim lSelectionIndex As Integer = gridViewProducto.FocusedRowHandle

                If Modo = pModo.Lista Then

                    'Cierra_Instancia_Previa(frmSucursal)

                    With frmProducto
                        .Modo = frmProducto.TipoTrans.Editar
                        .pProducto_Finca = pObjProducto
                        .InvokeListar = AddressOf Cargar
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    gridViewProducto.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                    SplashScreenManager.CloseForm(False)
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

End Class