Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class clsGraficadorBodega
    Implements IDisposable

    Public Property BeBodega As New clsBeBodega
    Public Property NivelMinimoBase As Integer = 4

    Private ReadOnly vFont As New Font("Tahoma", 8.5, FontStyle.Bold, GraphicsUnit.Pixel, 1, False)

    Sub New(ByRef PanBodega As PanelControl, ByRef PanBorde As PanelControl)

        If BeBodega.IdBodega <> 0 Then
            Get_Parametros_Bodega(PanBodega, PanBorde)
        End If

    End Sub

    Private Sub Get_Parametros_Bodega(ByRef PanBodega As PanelControl, ByRef PanBorde As PanelControl)

        Try

            clsLnBodega.Get_Estructura_By_IdBodega(BeBodega)

            PanBodega.Parent = PanBorde
            PanBorde.Size = New Size(300, 210)
            PanBodega.Size = New Size(200, 100)
            PanBodega.Location = New Point(10, 10)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Init",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Dibujar_Bodega(ByRef PanBodega As PanelControl, ByRef PanBorde As PanelControl)

        Try

            Ajustar_Zoom(PanBodega, PanBorde)

            Dibujar_Secotres(PanBodega, PanBorde)

            PanBorde.Visible = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Ajustar_Zoom(ByRef PanBodega As PanelControl, ByRef PanBorde As PanelControl)

        Try

            PanBodega.Width = BeBodega.Ancho * BeBodega.Zoom
            PanBodega.Height = BeBodega.Largo * BeBodega.Zoom

            PanBorde.Width = (BeBodega.Ancho * BeBodega.Zoom) + 20
            PanBorde.Height = (BeBodega.Largo * BeBodega.Zoom) + 20

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Dibujar_Secotres(ByRef PanBodega As PanelControl, ByRef PanBorde As PanelControl)

        Dim PanSector As Panel

        Dim ListTramos As New List(Of clsBeBodega_tramo)

        Try

            For Each BS As clsBeBodega_sector In BeBodega.Sectores

                PanSector = New Panel With {.Parent = PanBodega,
                    .Left = BS.Pos_x * BeBodega.Zoom,
                    .Top = BS.Pos_y * BeBodega.Zoom,
                    .Width = BS.Ancho * BeBodega.Zoom,
                    .Height = BS.Largo * BeBodega.Zoom,
                    .BorderStyle = BorderStyle.FixedSingle,
                    .Visible = False}

                ListTramos = BeBodega.Tramos.FindAll(Function(x) x.IdSector = BS.IdSector)

                If Not ListTramos Is Nothing
                    Dibujar_Tramos(PanSector, ListTramos)
                End If

            Next

            For Each C As Control In PanBodega.Controls
                C.Visible = True
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Dibujar_Tramos(ByRef SectorContenedor As Panel, listTramos As List(Of clsBeBodega_tramo))

        Dim PanTramo As Panel
        Dim ListUbic As New List(Of clsBeBodega_ubicacion)

        Try

            For Each T As clsBeBodega_tramo In listTramos

                PanTramo = New Panel With _
                    {
                 .Parent = SectorContenedor,
                 .Left = T.Margen_izquierdo * BeBodega.Zoom,
                 .Top = T.Margen_superior * BeBodega.Zoom,
                 .Width = T.Ancho * BeBodega.Zoom,
                 .Height = T.Largo * BeBodega.Zoom,
                 .BorderStyle = BorderStyle.FixedSingle,
                 .Tag = T.IdTramo}

                ListUbic = BeBodega.Ubicaciones.FindAll(Function(x) x.IdTramo = T.IdTramo _
                                                            AndAlso x.Nivel = NivelMinimoBase _
                                                            AndAlso x.Descripcion.Contains("A"))

                If Not listTramos Is Nothing
                    Dibujar_Ubicacion(PanTramo, ListUbic)
                End If

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Shared Function GetRandomColor() As Color

        Dim rand As New Random

        Return Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, _
            256))

    End Function

    Public Sub Dibujar_Ubicacion(ByVal TramoContenedor As Panel, ByVal listUbic As List(Of clsBeBodega_ubicacion))

        Dim lblUbicacionD As LabelControl
        Dim lblUbicacionC As LabelControl
        Dim lblUbicacionB As LabelControl
        Dim lblUbicacionA As LabelControl
        Dim vContador As Integer = 1

        Try

            Dim ColorTramo As Color = GetRandomColor
            Dim BeBodegaUbic As New clsBeBodega_ubicacion

            For Each B As clsBeBodega_ubicacion In listUbic

                lblUbicacionD = New LabelControl
                lblUbicacionD.AutoSizeMode = LabelAutoSizeMode.None
                lblUbicacionD.Parent = TramoContenedor
                lblUbicacionD.Left = B.Margen_izquierdo * BeBodega.Zoom
                lblUbicacionD.Top = B.Ancho * BeBodega.Zoom * (B.Indice_x - 1) * 2
                lblUbicacionD.Width = B.Ancho * BeBodega.Zoom
                lblUbicacionD.Height = B.Ancho * BeBodega.Zoom
                lblUbicacionD.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionD.Text = B.Indice_x & "D"
                lblUbicacionD.Font = vFont
                lblUbicacionD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                lblUbicacionD.Cursor = Cursors.Hand
                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                          AndAlso x.Orientacion_pos = "BR" _
                                          AndAlso x.Nivel = B.Nivel _
                                          AndAlso x.Indice_x = B.Indice_x)

                If Not BeBodegaUbic Is Nothing Then
                    lblUbicacionD.Tag = BeBodegaUbic.IdUbicacion
                End If


                lblUbicacionD.Appearance.BorderColor = ColorTramo

                AddHandler lblUbicacionD.Click, AddressOf lblUbicacion_Click

                lblUbicacionC = New LabelControl
                lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                lblUbicacionC.Parent = TramoContenedor
                lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * BeBodega.Zoom
                lblUbicacionC.Top = B.Ancho * BeBodega.Zoom * (B.Indice_x - 1) * 2
                lblUbicacionC.Width = B.Ancho * BeBodega.Zoom
                lblUbicacionC.Height = B.Ancho * BeBodega.Zoom
                lblUbicacionC.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionC.Text = B.Indice_x & "C"
                lblUbicacionC.Font = vFont
                lblUbicacionD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                          AndAlso x.Orientacion_pos = "FR" _
                                          AndAlso x.Nivel = B.Nivel _
                                          AndAlso x.Indice_x = B.Indice_x)

                If Not BeBodegaUbic Is Nothing Then
                    lblUbicacionD.Tag = BeBodegaUbic.IdUbicacion
                End If

                lblUbicacionC.Cursor = Cursors.Hand

                lblUbicacionC.Appearance.BorderColor = ColorTramo

                AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                lblUbicacionB = New LabelControl
                lblUbicacionB.AutoSizeMode = LabelAutoSizeMode.None
                lblUbicacionB.Parent = TramoContenedor
                lblUbicacionB.Left = B.Margen_izquierdo * BeBodega.Zoom
                lblUbicacionB.Top = lblUbicacionD.Top + lblUbicacionD.Height
                lblUbicacionB.Width = B.Ancho * BeBodega.Zoom
                lblUbicacionB.Height = B.Ancho * BeBodega.Zoom
                lblUbicacionB.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionB.Text = B.Indice_x & "B"
                lblUbicacionB.Font = vFont
                lblUbicacionD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                          AndAlso x.Orientacion_pos = "BL" _
                                          AndAlso x.Nivel = B.Nivel _
                                          AndAlso x.Indice_x = B.Indice_x)

                If Not BeBodegaUbic Is Nothing Then
                    lblUbicacionD.Tag = BeBodegaUbic.IdUbicacion
                End If
                lblUbicacionB.Cursor = Cursors.Hand

                lblUbicacionB.Appearance.BorderColor = ColorTramo

                AddHandler lblUbicacionB.Click, AddressOf lblUbicacion_Click

                lblUbicacionA = New LabelControl
                lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                lblUbicacionA.Parent = TramoContenedor
                lblUbicacionA.Left = (B.Margen_izquierdo + B.Ancho) * BeBodega.Zoom
                lblUbicacionA.Top = lblUbicacionD.Top + lblUbicacionD.Height
                lblUbicacionA.Width = B.Ancho * BeBodega.Zoom
                lblUbicacionA.Height = B.Ancho * BeBodega.Zoom
                lblUbicacionA.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionA.Text = B.Indice_x & "A"
                lblUbicacionA.Font = vFont
                lblUbicacionD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                          AndAlso x.Orientacion_pos = "FL" _
                                          AndAlso x.Nivel = B.Nivel _
                                          AndAlso x.Indice_x = B.Indice_x)

                If Not BeBodegaUbic Is Nothing Then
                    lblUbicacionD.Tag = BeBodegaUbic.IdUbicacion
                End If

                lblUbicacionA.Cursor = Cursors.Hand

                lblUbicacionA.Appearance.BorderColor = ColorTramo

                AddHandler lblUbicacionA.Click, AddressOf lblUbicacion_Click

                vContador += 1

            Next

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} {1}", ex.Message, vContador), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub lblUbicacion_Click(sender As Object, e As EventArgs)

        Try

            MessageBox.Show("IdUbicación: " & sender.tag, "Ubicación", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim BeBodegaUbicacion As New clsBeBodega_ubicacion() With {.IdUbicacion = sender.tag}
            BeBodegaUbicacion.IdBodega = AP.IdBodega
            clsLnBodega_ubicacion.GetSingle(BeBodegaUbicacion)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If BeBodega IsNot Nothing Then
            BeBodega.Dispose()
            BeBodega = Nothing
        End If
        If vFont IsNot Nothing
            vFont.Dispose()
        End If
    End Sub

End Class
