Imports DevExpress.XtraEditors

Public Class frmEstBodega

    Public Property NombreBodega As String = ""
    Public Property IdBodega As Integer = 0
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub frmBodEstructura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Estructura Inicial - " & NombreBodega
        Listar_Sectores()
        Listar_Tramos()
    End Sub

#Region " Metodos principales "

    Private Sub Copiar_Estructura(ByVal IdTramoOrigen As Integer,
                                  ByVal IdTramoDestino As Integer,
                                  ByVal IdBodega As Integer)

        Dim values As New List(Of clsBeEstructura_grupo)
        Dim lItemsEstructura_grupo As New List(Of clsBeEstructura_grupo)
        Dim BeEstructura_grupo As New clsBeEstructura_grupo
        Dim maxid As Integer

        Try

            values = clsLnEstructura_grupo.Get_All_By_IdBodega_And_IdTramo(IdBodega, IdTramoOrigen)
            maxid = clsLnEstructura_grupo.MaxID() + 1

            For Each it As clsBeEstructura_grupo In values

                BeEstructura_grupo = New clsBeEstructura_grupo
                BeEstructura_grupo.IdGrupo = maxid : maxid += 1
                BeEstructura_grupo.IdTramo = IdTramoDestino
                BeEstructura_grupo.Pos = it.Pos
                BeEstructura_grupo.Cant = it.Cant
                BeEstructura_grupo.Tamano = it.Tamano
                BeEstructura_grupo.Offset = it.Offset
                BeEstructura_grupo.Ancho = it.Ancho
                BeEstructura_grupo.Alto = it.Alto
                BeEstructura_grupo.Largo = it.Largo
                BeEstructura_grupo.Palet = it.Palet
                BeEstructura_grupo.Orient = it.Orient
                BeEstructura_grupo.Agrupacion = it.Agrupacion
                BeEstructura_grupo.IdBodega = IdBodega
                lItemsEstructura_grupo.Add(BeEstructura_grupo)

            Next

            clsLnEstructura_grupo.Insertar_Batch(IdTramoDestino, IdBodega, lItemsEstructura_grupo)

            XtraMessageBox.Show("Estructura copiada.",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

#End Region

#Region " Eventos "

    Private Sub btnTramos_Click(sender As Object, e As EventArgs) Handles btnTramos.Click



        Dim estSect As New frmEstSector

        Try
            btnTramos.Enabled = False

            estSect.IdSector = cboSector.SelectedValue
            estSect.Text = "Estructura de tramos - Sector " & cboSector.Text
            estSect.ShowDialog()

            Listar_Tramos()

            btnTramos.Enabled = True

        Catch ex As Exception

            btnTramos.Enabled = True
            MsgBox(ex.Message)
        Finally
            estSect.Dispose()
        End Try

    End Sub

    Private Sub btnUbic_Click(sender As Object, e As EventArgs) Handles btnUbic.Click

        Dim estTramo As New frmEstTramo

        Try
            btnUbic.Enabled = False
            estTramo.IdTramo = cboTramo.SelectedValue
            estTramo.IdBodega = IdBodega
            estTramo.Text = "Estructura de ubicaciones - Tramo " & cboTramo.Text
            estTramo.ShowDialog()
            btnUbic.Enabled = True
        Catch ex As Exception
            btnUbic.Enabled = True
            MsgBox(ex.Message)
        Finally
            estTramo.Dispose()
        End Try
    End Sub

    Private Sub btnValidar_Click(sender As Object, e As EventArgs) Handles btnValidar.Click
        Dim valid As New frmValidacion
        valid.ShowDialog()
        valid.Dispose()
    End Sub

    Private Sub btnCopiar_Click(sender As Object, e As EventArgs) Handles btnCopiar.Click

        Dim idorig, iddest As Integer

        Try
            btnCopiar.Enabled = False

            idorig = cboTramo.SelectedValue
            If (idorig < 1) Then
                MsgBox("Tramo origen incorrecto.") : Return
            End If

            iddest = cboTramo2.SelectedValue
            If (iddest < 1) Or (idorig = iddest) Then
                MsgBox("Tramo destino incorrecto.") : Return
            End If

            If MessageBox.Show("Borrar la estructura actual de tramo " & cboTramo2.Text & vbCrLf & "y reescribir con estructura de tramo " & cboTramo.Text & "?", "Copiar estructura ", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

            Copiar_Estructura(idorig, iddest, 0)
            btnCopiar.Enabled = True

        Catch ex As Exception
            btnCopiar.Enabled = True
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click

        btnAplicar.Enabled = False
        Dim gen As New frmEstGeneracion
        gen.ShowDialog()
        gen.Dispose()
        btnAplicar.Enabled = True
    End Sub

#End Region

#Region " Aux "

    Private Sub Listar_Sectores()
        Dim DT As New DataTable

        Try
            DT = clsLnBodega_sector.Get_All_Sector_By_IdBodega(AP.IdBodega)

            If DT.Rows.Count > 0 Then
                cboSector.DisplayMember = "descripcion"
                cboSector.ValueMember = "Idsector"
                cboSector.DataSource = DT
                cboSector.SelectedIndex = 0
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Listar_Tramos()

        Dim DT, DT2 As New DataTable

        Try

            DT = clsLnEstructura_tramo.Get_All_By_IdBodega(AP.IdBodega)
            'DT2 = clsLnEstructura_tramo.Get_All_By_IdBodega(AP.IdBodega)

            If DT.Rows.Count > 0 Then

                cboTramo.DisplayMember = "descripcion"
                cboTramo.ValueMember = "Idtramo"
                cboTramo.DataSource = DT
                cboTramo.SelectedIndex = 0

                cboTramo2.DisplayMember = "descripcion"
                cboTramo2.ValueMember = "Idtramo"
                cboTramo2.DataSource = DT 'DT2
                cboTramo2.SelectedIndex = 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        Try

            Using GrafBod As New frmDiseñoB() With {.IdEmpresa = AP.IdEmpresa, .IdBodega = AP.IdBodega}
                GrafBod.ShowDialog(Me)
            End Using

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub sbEstTramo_Click(sender As Object, e As EventArgs) Handles sbEstTramo.Click
        'Dim frm As New frmTramo

        'frm.Text = "Estructura tramos"
        'frm.tipo = 2
        'frm.ShowDialog()
    End Sub

    Private Sub sbEstSector_Click_1(sender As Object, e As EventArgs) Handles sbEstSector.Click
        'Dim frm As New frmTramo

        'frm.Text = "Estructura sectores"
        'frm.tipo = 1
        'frm.ShowDialog()
    End Sub


#End Region

End Class