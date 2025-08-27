Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraSplashScreen

Public Class frmDiseño

    Public BeBodega As New clsBeBodega
    Public lSectores As New List(Of clsBeBodega_sector)
    Public Zoom As Double = 0

    Private vFont As New Font("Tahoma", 8, FontStyle.Bold, GraphicsUnit.Pixel, 1, False)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Dibujar_Bodega()

        Try

            Get_Parametros_Bodega()

            Ajustar_Zoom()

            Dibujar_Secotres()

            PanBorde.Visible = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            prgTramos.Visible = False
            prgUbicacionesPorTramo.Visible = False
            SplashScreenManager.CloseForm(False)
        End Try


    End Sub

    Private Sub Ajustar_Zoom()

        Try

            PanBodega.Width = txtAncho.Value * Zoom
            PanBodega.Height = txtLargo.Value * Zoom

            PanBorde.Width = (txtAncho.Value * Zoom) + 20
            PanBorde.Height = (txtLargo.Value * Zoom) + 20

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Get_Parametros_Bodega()

        Try

            BeBodega.IdBodega = cmbBodegas.SelectedValue

            clsLnBodega.Get_Estructura_By_IdBodega(BeBodega)

            txtLargo.Value = BeBodega.Largo
            txtAncho.Value = BeBodega.Ancho
            txtZoom.Value = BeBodega.Zoom

            Zoom = txtZoom.Value

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Dibujar_Secotres()

        Dim PanSector As Panel
        Dim ListTramos As New List(Of clsBeBodega_tramo)

        Try

            For Each BS As clsBeBodega_sector In BeBodega.Sectores

                PanSector = New Panel With {.Parent = PanBodega,
                    .Left = BS.Pos_x * Zoom,
                    .Top = BS.Pos_y * Zoom,
                    .Width = BS.Ancho * Zoom,
                    .Height = BS.Largo * Zoom,
                    .BorderStyle = BorderStyle.None,
                    .Visible = False}

                ListTramos = BeBodega.Tramos.FindAll(Function(x) x.IdSector = BS.IdSector)

                If Not ListTramos Is Nothing Then
                    Dibujar_Tramos(PanSector, ListTramos)
                End If

            Next

            For Each C As Control In PanBodega.Controls
                C.Visible = True
            Next

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Dibujar_Tramos(ByRef SectorContenedor As Panel, listTramos As List(Of clsBeBodega_tramo))

        Dim PanTramo As Panel
        Dim ListUbic As New List(Of clsBeBodega_ubicacion)

        Try

            prgTramos.Properties.PercentView = False
            prgTramos.Properties.Minimum = 0
            prgTramos.Properties.Maximum = listTramos.Count + 1
            prgTramos.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgTramos.Visible = True
            prgTramos.EditValue = 0

            For Each T As clsBeBodega_tramo In listTramos

                prgTramos.EditValue = T.IdTramo

                PanTramo = New Panel With
                    {
                 .Parent = SectorContenedor,
                 .Left = T.Margen_izquierdo * Zoom,
                 .Top = T.Margen_superior * Zoom,
                 .Width = T.Ancho * Zoom,
                 .Height = T.Largo * Zoom,
                 .BorderStyle = BorderStyle.FixedSingle,
                 .Tag = T.IdTramo}

                ListUbic = BeBodega.Ubicaciones.FindAll(Function(x) x.IdTramo = T.IdTramo _
                                                            AndAlso x.Nivel = 4 _
                                                            AndAlso x.Descripcion.Contains("A"))

                If Not listTramos Is Nothing Then
                    Dibujar_Ubicacion(PanTramo, ListUbic)
                End If

                prgTramos.PerformStep
                prgTramos.Update()

            Next

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Sub Dibujar_Ubicacion(ByVal TramoContenedor As Panel, ByVal listUbic As List(Of clsBeBodega_ubicacion))

        Dim lblUbicacionD As LabelControl
        Dim lblUbicacionC As LabelControl
        Dim lblUbicacionB As LabelControl
        Dim lblUbicacionA As LabelControl
        Dim vContador As Integer = 1

        Try



            Dim ColorTramo As Color = GetRandomColor()
            Dim BeBodegaUbic As New clsBeBodega_ubicacion

            prgUbicacionesPorTramo.Properties.PercentView = False
            prgUbicacionesPorTramo.Properties.Minimum = 0
            prgUbicacionesPorTramo.Properties.Maximum = listUbic.Count
            prgUbicacionesPorTramo.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgUbicacionesPorTramo.EditValue = 0

            For Each B As clsBeBodega_ubicacion In listUbic

                lblUbicacionD = New LabelControl
                lblUbicacionD.AutoSizeMode = LabelAutoSizeMode.None
                lblUbicacionD.Parent = TramoContenedor
                lblUbicacionD.Left = B.Margen_izquierdo * Zoom
                lblUbicacionD.Top = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                lblUbicacionD.Width = B.Ancho * Zoom
                lblUbicacionD.Height = B.Ancho * Zoom
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
                lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * Zoom
                lblUbicacionC.Top = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                lblUbicacionC.Width = B.Ancho * Zoom
                lblUbicacionC.Height = B.Ancho * Zoom
                lblUbicacionC.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionC.Text = B.Indice_x & "C"
                lblUbicacionC.Font = LabelControl1.Font
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
                lblUbicacionB.Left = B.Margen_izquierdo * Zoom
                lblUbicacionB.Top = lblUbicacionD.Top + lblUbicacionD.Height
                lblUbicacionB.Width = B.Ancho * Zoom
                lblUbicacionB.Height = B.Ancho * Zoom
                lblUbicacionB.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionB.Text = B.Indice_x & "B"
                lblUbicacionB.Font = LabelControl1.Font
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
                lblUbicacionA.Left = (B.Margen_izquierdo + B.Ancho) * Zoom
                lblUbicacionA.Top = lblUbicacionD.Top + lblUbicacionD.Height
                lblUbicacionA.Width = B.Ancho * Zoom
                lblUbicacionA.Height = B.Ancho * Zoom
                lblUbicacionA.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionA.Text = B.Indice_x & "A"
                lblUbicacionA.Font = LabelControl1.Font
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

                prgUbicacionesPorTramo.EditValue = vContador
                prgUbicacionesPorTramo.PerformStep
                prgUbicacionesPorTramo.Update()
                vContador += 1

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", ex.Message, vContador), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Shared Function GetRandomColor() As Color

        Dim rand As New Random

        Return Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0,
            256))

    End Function

    Private Sub txtZoom_ValueChanged(sender As Object, e As EventArgs) Handles txtZoom.ValueChanged
        Zoom = txtZoom.Value
    End Sub

    Private Sub Actualiza_APP_Config()

        Try

            System.Configuration.ConfigurationManager.AppSettings("CST") = BD.CadenaConexion

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lblUbicacion_Click(sender As Object, e As EventArgs)

        Try

            XtraMessageBox.Show("IdUbicación: " & sender.tag, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim BeBodegaUbicacion As New clsBeBodega_ubicacion
            BeBodegaUbicacion.IdUbicacion = sender.tag
            clsLnBodega_ubicacion.GetSingle(BeBodegaUbicacion)

            Dim lUbicaciones As New List(Of clsBeBodega_ubicacion)

            lUbicaciones = clsLnBodega_ubicacion.Get_All_Ubicaciones_Nivel_By_Posicion(BeBodegaUbicacion)

            DgridPosiciones.DataSource = lUbicaciones

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Empresas()

        Try

            If AP.Listar_Empresas(cmbEmpresa) Then

                AP.IdEmpresa = cmbEmpresa.SelectedValue

                If AP.Listar_BodegasLogin(cmbBodegas) Then
                    Dibujar_Bodega()

                Else

                    XtraMessageBox.Show("No hay bodegas definidas para la empresa: " & cmbEmpresa.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()

                End If

            Else

                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Sub frmDiseño_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            If Not AP.Existe_Ini() Then

                XtraMessageBox.Show("No existe INI", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()

            Else

                If Not BD.Leer_Archivo_Configuracion_INI() Then
                    Close()
                Else
                    Actualiza_APP_Config()
                    Listar_Empresas()
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub prgTramos_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles prgTramos.CustomDisplayText

        Dim val As String = e.Value.ToString()
        e.DisplayText = "Procesando tramo: " & val

    End Sub

    Private Sub prgUbicacionesPorTramo_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles prgUbicacionesPorTramo.CustomDisplayText
        Dim val As String = e.Value.ToString()
        e.DisplayText = String.Format("Procesando {0} de {1} ubicaciones", val, prgUbicacionesPorTramo.Properties.Maximum)
    End Sub

    Private Sub frmDiseño_Load(sender As Object, e As EventArgs) Handles Me.Load
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Fetching data...")
    End Sub

    Private Sub DockPanel3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PanBorde_Paint(sender As Object, e As PaintEventArgs) Handles PanBorde.Paint

    End Sub
End Class