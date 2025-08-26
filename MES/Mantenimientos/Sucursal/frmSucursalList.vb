Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraWaitForm
Imports TOMWMS

Public Class frmSucursalList

    Public pObjSucursal As clsBeQT_Sucursal
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

    Private Sub frmSucursalList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Listando productos...")
            Cargar()

            SplashScreenManager.CloseForm(False)
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cargar()
        Try
            Dim pTabla As New DataTable

            pTabla = clsLnQT_Sucursal.Listar()

            If pTabla IsNot Nothing AndAlso pTabla.Rows.Count > 0 Then

                gridSucursal.DataSource = pTabla
                If gridViewSucursal.Columns.Count > 0 Then
                    gridViewSucursal.BestFitColumns()
                    gridViewSucursal.OptionsBehavior.Editable = False
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try

            cmdActualizar.Enabled = False
            Cargar()
            cmdActualizar.Enabled = True

        Catch ex As Exception
            cmdActualizar.Enabled = False
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gridSucursal_DoubleClick(sender As Object, e As EventArgs) Handles gridSucursal.DoubleClick
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

            If gridViewSucursal.RowCount > 0 Then

                Dim Dr As DataRowView = gridViewSucursal.GetFocusedRow

                pObjSucursal = New clsBeQT_Sucursal


                Dim IdFinca As Integer = Integer.Parse(Dr.Item("IdFinca"))
                pObjSucursal.IdFinca = IdFinca
                clsLnQT_Sucursal.GetSingle(pObjSucursal)


                Dim lSelectionIndex As Integer = gridViewSucursal.FocusedRowHandle

                If Modo = pModo.Lista Then

                    'Cierra_Instancia_Previa(frmSucursal)

                    With frmSucursal
                        .Modo = frmSucursal.TipoTrans.Editar
                        .pSucursal = pObjSucursal
                        .InvokeListar = AddressOf Cargar
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    gridViewSucursal.FocusedRowHandle = lSelectionIndex

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