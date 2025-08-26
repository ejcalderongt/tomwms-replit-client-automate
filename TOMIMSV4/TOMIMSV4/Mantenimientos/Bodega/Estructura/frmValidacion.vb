Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmValidacion

    Public Property IdBodega As Integer = 0
    Public Property CodigoBodega As String = ""
    Public Property NombreBodega As String = ""


    Private Sub frmValidacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrarEstructura()
        Me.Text = "Estructura - " & NombreBodega
    End Sub

    Private Sub mostrarEstructura()
        Dim dts As New DataTable
        Dim tramos As New List(Of clsBeEstructura_tramo)
        Dim ns, nt As TreeNode
        Dim si, ti, mx, my, smx As Integer
        Dim idsector, IdTramo As Integer

        tvData.Nodes.Clear()

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Actualizando registros...")

        Try

            dts = clsLnBodega_sector.Get_All_Sector_By_IdBodega(IdBodega)

            For si = 0 To dts.Rows.Count - 1

                idsector = dts.Rows(si).Item("idsector")
                tramos = clsLnEstructura_tramo.Get_All_By_IdSector(idsector, IdBodega)
                ns = tvData.Nodes.Add(dts.Rows(si).Item("descripcion"))

                smx = 0

                For ti = 0 To tramos.Count - 1

                    IdTramo = tramos(ti).IdTramo
                    nt = ns.Nodes.Add(tramos(ti).Descripcion)

                    Estructura_Tramo(IdBodega, IdTramo, mx, my)

                    If (mx * my <> 0) Then nt.Text = nt.Text & " - [ " & mx & " x " & my & " ]"

                    smx += mx

                Next

                ns.Text = ns.Text & " - [ tramos : " & tramos.Count & " , posiciones : " & smx & " ]"

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Estructura_Tramo(ByVal IdBodega As Integer,
                                ByVal IdTramo As Integer,
                                ByRef mx As Integer,
                                ByRef my As Integer)

        Dim grupos As New List(Of clsBeEstructura_grupo)
        Dim ii, vx, vy, vagrup As Integer

        mx = 0 : my = 0

        Try
            grupos = clsLnEstructura_grupo.Get_All_By_IdBodega_And_IdTramo(IdBodega, IdTramo)
            If grupos.Count = 0 Then Return

            vagrup = grupos(0).Agrupacion

            For ii = 0 To grupos.Count - 1

                If vagrup = 1 Then
                    vx = grupos(ii).Cant
                    vy = grupos(ii).Tamano + grupos(ii).Offset

                    mx += vx
                    If (vy > my) Then my = vy
                Else
                    vx = grupos(ii).Tamano + grupos(ii).Offset
                    vy = grupos(ii).Cant

                    If (vx > mx) Then mx = vx
                    my += vy
                End If

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

End Class