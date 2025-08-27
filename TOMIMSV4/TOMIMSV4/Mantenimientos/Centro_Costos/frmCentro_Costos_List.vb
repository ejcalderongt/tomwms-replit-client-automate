Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmCentro_Costos_List


    Public gCentroCosto As clsBeCentro_costo

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmCentro_Costos_List_Load(sender As Object, e As EventArgs) Handles Me.Load
        Listar_CentroCosto()
    End Sub

    Private Sub Listar_CentroCosto()
        Try
            Dim lista As New DataTable


            If Modo = pModo.Seleccion Then

                lista = clsLnCentro_costo.Listar()
            Else
                lista = clsLnCentro_costo.Listar()
                'lista = clsLnCliente.Get_All_Filtro_DT(chkActivos.Checked, AP.IdBodega)
            End If

            If lista IsNot Nothing AndAlso lista.Rows.Count > 0 Then

                Dgrid.DataSource = lista

                'lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            Else
                Dgrid.DataSource = Nothing
            End If


            If (GridView1.Columns.Count <> 0) Then

                GridView1.BestFitColumns()

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim Obj As New clsBeCentro_costo
                Dim pCentrocostos = Dr.Item("IdCentroCosto")
                Obj.IdCentroCosto = pCentrocostos

                clsLnCentro_costo.GetSingle(Obj)

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmCentro_Costos)

                    With frmCentro_Costos
                        .Modo = frmCentro_Costos.TipoTrans.Editar
                        .OpcionesMenu = OpcionesMenu
                        .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                        .gBeCentroCostos = Obj
                        .InvokeListarCentroCosto = AddressOf Listar_CentroCosto
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    gCentroCosto = Obj
                    Hide()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        mnuNuevo.Enabled = False
        Nuevo_Centro()
        mnuNuevo.Enabled = True
    End Sub

    Private Sub Nuevo_Centro()

        Try

            Cierra_Instancia_Previa(frmCliente)

            With frmCentro_Costos
                .Modo = frmCentro_Costos.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarCentroCosto = AddressOf Listar_CentroCosto
                .WindowState = FormWindowState.Normal
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        Listar_CentroCosto()
        mnuActualizar.Enabled = True

    End Sub
End Class