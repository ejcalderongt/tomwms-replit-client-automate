Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmEstructuraBod

    Public Property IdBodega As Integer = 0
    Public Property CodigoBodega As String = ""
    Public Property NombreBodega As String = ""
    Public Property Descripcion_import As String = ""

    Private Sub Modificar_Sector()

        Dim estSect As New frmEstSector

        Try
            btnTramos.Enabled = False

            estSect.IdBodega = IdBodega
            estSect.IdSector = cmbSector.SelectedValue
            estSect.Text = "Estructura de tramos - Sector " & cmbSector.Text
            estSect.StartPosition = FormStartPosition.CenterParent
            estSect.WindowState = FormWindowState.Maximized
            estSect.ShowDialog()

            Listar_Tramos()

            btnTramos.Enabled = True

        Catch ex As Exception
            btnTramos.Enabled = True
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            estSect.Dispose()
        End Try

    End Sub

    Private Sub btnTramos_Click(sender As Object, e As EventArgs) Handles btnTramos.Click
        Modificar_Sector()
    End Sub

    Private Sub mnuModificaSector_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuModificaSector.ItemClick
        Modificar_Sector()
    End Sub

    Private Sub Listar_Areas()

        Dim DT, DT2 As New DataTable

        Try

            DT = clsLnBodega_area.Get_All_Areas_By_IdBodega(IdBodega)


            If DT.Rows.Count > 0 Then

                cmbArea.DisplayMember = "descripcion"
                cmbArea.ValueMember = "IdArea"
                cmbArea.DataSource = DT
                cmbArea.SelectedIndex = 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Listar_Tramos()

        Dim DT As New DataTable
        Dim DT1 As New DataTable

        Try

            DT = clsLnEstructura_Tramo.Get_All_By_IdBodega_And_IdSector_DT(IdBodega,
                                                                           cmbSector.SelectedValue)

            DT1 = clsLnEstructura_Tramo.Get_All_By_IdBodega(IdBodega)

            If DT.Rows.Count > 0 Then

                cmbTramo.DisplayMember = "descripcion"
                cmbTramo.ValueMember = "IdTramo"
                cmbTramo.DataSource = DT
                cmbTramo.SelectedIndex = 0

                cboTramo2.DisplayMember = "descripcion"
                cboTramo2.ValueMember = "IdTramo"
                cboTramo2.DataSource = DT1
                cboTramo2.SelectedIndex = 0

            End If

            mnuModificaSector.Caption = "Modificar : " & cmbSector.Text
            mnuModificaSector.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub btnUbic_Click(sender As Object, e As EventArgs) Handles btnUbic.Click
        Modificar_Tramo()
    End Sub

    Private Sub Modificar_Tramo()

        Dim estTramo As New frmEstTramo

        Try
            btnUbic.Enabled = False
            estTramo.IdTramo = cmbTramo.SelectedValue
            estTramo.IdBodega = IdBodega
            estTramo.Text = "Estructura de ubicaciones - Tramo " & cmbTramo.Text
            estTramo.ShowDialog()
            btnUbic.Enabled = True
        Catch ex As Exception
            btnUbic.Enabled = True
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            estTramo.Dispose()
        End Try


    End Sub

    Private Sub mnuModificarTramo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuModificarTramo.ItemClick
        Modificar_Tramo()
    End Sub

    Private Sub btnCopiar_Click(sender As Object, e As EventArgs) Handles btnCopiar.Click
        Copiar_Tramo()
    End Sub

    Private Sub Copiar_Tramo()


        Dim idorig, iddest As Integer

        Try
            btnCopiar.Enabled = False

            idorig = cmbTramo.SelectedValue
            If (idorig < 1) Then
                MsgBox("Tramo origen incorrecto.") : Return
            End If

            iddest = cboTramo2.SelectedValue
            If (iddest < 1) Or (idorig = iddest) Then
                MsgBox("Tramo destino incorrecto.") : Return
            End If

            If MessageBox.Show("Borrar la estructura actual de tramo " & cboTramo2.Text & vbCrLf & "y reescribir con estructura de tramo " & cmbTramo.Text & "?", "Copiar estructura ", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

            copiarEstructura(idorig, iddest)
            btnCopiar.Enabled = True

        Catch ex As Exception
            btnCopiar.Enabled = True
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try


    End Sub

    Private Sub copiarEstructura(ByVal IdTramoOrigen As Integer,
                                 ByVal IdTramoDestino As Integer)

        Dim values As New List(Of clsBeEstructura_grupo)
        Dim lItemsEstructuraGrupo As New List(Of clsBeEstructura_grupo)
        Dim BeEstructuraGrupo As New clsBeEstructura_grupo
        Dim maxid As Integer

        Try

            values = clsLnEstructura_grupo.Get_All_By_IdBodega_And_IdTramo(IdBodega, IdTramoOrigen)
            maxid = clsLnEstructura_grupo.MaxID() + 1

            For Each it As clsBeEstructura_grupo In values

                BeEstructuraGrupo = New clsBeEstructura_grupo
                BeEstructuraGrupo.IdGrupo = maxid : maxid += 1
                BeEstructuraGrupo.IdTramo = IdTramoDestino
                BeEstructuraGrupo.Pos = it.Pos
                BeEstructuraGrupo.Cant = it.Cant
                BeEstructuraGrupo.Tamano = it.Tamano
                BeEstructuraGrupo.Offset = it.Offset
                BeEstructuraGrupo.Ancho = it.Ancho
                BeEstructuraGrupo.Alto = it.Alto
                BeEstructuraGrupo.Largo = it.Largo
                BeEstructuraGrupo.Palet = it.Palet
                BeEstructuraGrupo.Orient = it.Orient
                BeEstructuraGrupo.Agrupacion = it.Agrupacion
                BeEstructuraGrupo.Orden_Descendente = it.Orden_Descendente
                BeEstructuraGrupo.IdBodega = IdBodega
                lItemsEstructuraGrupo.Add(BeEstructuraGrupo)

            Next

            clsLnEstructura_grupo.Insertar_Batch(IdTramoDestino, IdBodega, lItemsEstructuraGrupo)

            XtraMessageBox.Show("Estructura copiada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub frmEstructuraBod_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Me.Text = "Estructura Inicial - " & NombreBodega

            Listar_Areas()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Listar_Sectores()

        Dim DT As New DataTable

        Try

            If Not cmbArea.SelectedValue Is Nothing Then

                DT = clsLnBodega_sector.Get_All_Sector_By_IdArea_And_IdBodega_For_Combo(cmbArea.SelectedValue, IdBodega)

                If DT.Rows.Count > 0 Then
                    cmbSector.DisplayMember = "Nombre"
                    cmbSector.ValueMember = "IdSector"
                    cmbSector.DataSource = DT
                    cmbSector.SelectedIndex = 0
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmbArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArea.SelectedIndexChanged
        Listar_Sectores()
    End Sub

    Private Sub cboSector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSector.SelectedIndexChanged
        Listar_Tramos()
    End Sub

    Private Sub mnuVerDiseño_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuVerDiseño.ItemClick

        Try

            Using GrafBod As New frmDiseñoB() With {.IdEmpresa = AP.IdEmpresa, .IdBodega = IdBodega}
                GrafBod.ShowDialog(Me)
            End Using

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuValidarEstructura_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuValidarEstructura.ItemClick
        Dim valid As New frmValidacion
        valid.IdBodega = IdBodega
        valid.ShowDialog()
        valid.Dispose()
    End Sub

    Private Sub mnuGenerarEstructura_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGenerarEstructura.ItemClick

        mnuGenerarEstructura.Enabled = False
        Dim gen As New frmEstGeneracion
        gen.IdBodega = IdBodega
        gen.ShowDialog()
        gen.Dispose()
        mnuGenerarEstructura.Enabled = True

    End Sub

    Private Sub cmbTramo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTramo.SelectedIndexChanged

        mnuModificarTramo.Caption = "Modificar " & cmbTramo.Text
        mnuModificarTramo.Refresh()

    End Sub

    Private Sub mnuCopiarEstructura_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCopiarEstructura.ItemClick
        Copiar_Tramo()
    End Sub

    Private Sub mnuImportarEstructura_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuImportarEstructura.ItemClick

        mnuImportarEstructura.Enabled = False

        'GT12012021: si la bodega tiene movimientos, ya no es posible importar el excel!
        If clsLnTrans_bodega_ubicaciones_excel.Existe_Mov_By_IdBodega(IdBodega) Then
            XtraMessageBox.Show("La bodega ya tiene movimientos registrados, no se puede importar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            'GT10012021: se avisa sobre la eliminación de la data existente en la bodega a importar.
            If cmbArea.SelectedIndex <> -1 OrElse cmbSector.SelectedIndex <> -1 OrElse cmbTramo.SelectedIndex <> -1 Then
                If XtraMessageBox.Show("¿Existe data configurada, y será eliminada, desea continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Iniciando limpieza")
                    clsLnTrans_bodega_ubicaciones_excel.Limpiar_Todo(IdBodega)
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Limpieza completada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Else
                    mnuImportarEstructura.Enabled = True
                    Return
                End If
            End If
            Importar_Excel()
        End If

        mnuImportarEstructura.Enabled = True

    End Sub

    Private Sub Importar_Excel()

        Try

            Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Bodega " + Me.Text,
                .pTipoMantenimiento = "EstructuraBodega",
                .Listar = Nothing,
                .IdInventarioEnc = -1}

            If Carga.ShowDialog() = DialogResult.OK Then

                Dim i As Integer = 0
                Dim vContador As Integer = 1

                RibbonControl.Enabled = False

            End If

            Carga.Dispose()

        Catch ex As Exception
            'SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            'SplashScreenManager.CloseForm(False)
            RibbonControl.Enabled = True
        End Try

    End Sub

End Class