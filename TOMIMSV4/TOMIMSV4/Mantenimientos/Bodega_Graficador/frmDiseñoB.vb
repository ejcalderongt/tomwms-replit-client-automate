Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraSplashScreen

Public Class frmDiseñoB

    Public BeBodega As New clsBeBodega
    Public lSectores As New List(Of clsBeBodega_sector)
    Public Zoom As Double = 0
    Private pZoom As Double = 0

    Private vFont As New Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Pixel, 1, False)

    Public Property IdBodega As Integer = 0
    Public Property IdEmpresa As Integer = 0
    Public Property CantidadUbicaciones As Double = 0

    Private tramo_orient As Integer
    Private sector_horiz As Boolean

    Dim DTSTock As New DataTable
    Public Property ListUbicacionesVisibles As New List(Of clsBeBodega_ubicacion)
    Public Property ListUbicacionesNoVisibles As New List(Of clsBeBodega_ubicacion)
    Public Property ListUbicacionesPiso As New List(Of clsBeBodega_ubicacion)

    Dim PanSector As Panel

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Dibujar_Bodega(Optional ByVal Get_Data As Boolean = True)

        Try

            If Get_Data AndAlso Not BeBodega.Ubicaciones.Count = 0 Then
                If XtraMessageBox.Show("¿Al parecer la data ya existe, volver a cargar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Get_Data = False
                End If
            End If

            SplashScreenManager.CloseForm(False)
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando...")

            If Get_Data Then
                Get_Parametros_Bodega()
            End If

            Ajustar_Zoom()

            Application.DoEvents()

            Dibujar_Sectores()

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

            If pZoom > 0 AndAlso pZoom <> BeBodega.Zoom Then
                txtZoom.Value = pZoom
                Zoom = pZoom
            End If

            PanBodega.Width = txtAncho.Value * Zoom
            PanBodega.Height = txtLargo.Value * Zoom
            PanBorde.Width = (txtAncho.Value * Zoom) + 20
            PanBorde.Height = (txtLargo.Value * Zoom) + 20

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Get_Parametros_Bodega()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Leyendo estructura...")

            BeBodega.IdBodega = cmbBodegas.SelectedValue

            clsLnBodega.Get_Estructura_By_IdBodega(BeBodega)

            If Not BeBodega Is Nothing Then
                txtLargo.Value = BeBodega.Largo
                txtAncho.Value = BeBodega.Ancho
                txtZoom.Value = BeBodega.Zoom
                Zoom = txtZoom.Value
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Dim lControlsToClear As New List(Of Control)

    Private Sub Clear_Panel(ByRef container As PanelControl, Optional Recurse As Boolean = True)

        Try

            lControlsToClear = New List(Of Control)

            container.Controls.Clear()

            If Not container Is Nothing Then

                container.SuspendLayout()
                Dim ctrl As Control

                For Each ctrl In container.Controls

                    If Recurse Then
                        If (ctrl.GetType() Is GetType(Panel)) Then
                            Dim pnl As Panel = CType(ctrl, Panel)
                            Clear_Pan_Sector(pnl, Recurse)
                            lControlsToClear.Add(pnl)
                            Debug.WriteLine("Panel " & pnl.Name & " Of : " & container.Name & " Pending to delete")
                        ElseIf ctrl.GetType() Is GetType(LabelControl) Then
                            Dim grbx As LabelControl = CType(ctrl, LabelControl)
                            lControlsToClear.Add(grbx)
                        Else
                            Debug.WriteLine("Not found action for control " & ctrl.GetType().ToString)
                        End If
                    End If

                Next

                If lControlsToClear.Count > 0 Then
                    For Each Ctl In lControlsToClear
                        container.Controls.Remove(Ctl)
                        Debug.WriteLine("Panel " & Ctl.Name & " Of : " & container.Name & " Eliminado")
                    Next
                End If

                container.ResumeLayout(True)

            End If

        Catch ex As Exception
            Debug.Write("Err: " & ex.Message)
        Finally
            Me.ResumeLayout()
        End Try

    End Sub

    Private Sub Clear_Pan_Sector(ByRef container As Panel, Optional Recurse As Boolean = True)

        Try

            If Not container Is Nothing Then

                container.SuspendLayout()

                'Clear all of the controls within the container object
                'If "Recurse" is true, then also clear controls within any sub-containers
                Dim ctrl As Control
                For Each ctrl In container.Controls
                    If Recurse Then
                        If (ctrl.GetType() Is GetType(Panel)) Then
                            Dim pnl As Panel = CType(ctrl, Panel)
                            Clear_Pan_Sector(pnl, Recurse)
                            lControlsToClear.Add(pnl)
                            Debug.WriteLine("Pan_Sector: " & pnl.Name & " Of: " & container.Name & " Eliminado")
                        ElseIf ctrl.GetType() Is GetType(LabelControl) Then
                            Dim grbx As LabelControl = CType(ctrl, LabelControl)
                            container.Controls.Remove(grbx)
                            Debug.WriteLine("Lbl_Pan_Sector: " & grbx.Name & " Eliminado")
                        Else
                            Debug.WriteLine("Not found action for Control : " & ctrl.GetType().ToString)
                        End If
                    End If
                Next

                container.ResumeLayout()

            End If

            Application.DoEvents()

        Catch ex As Exception
            Debug.Write("Err: " & ex.Message)
        End Try

    End Sub

    Private Sub Dibujar_Sectores()

        Dim ListTramos As New List(Of clsBeBodega_tramo)

        Try

            Clear_Panel(PanBodega, True)

            PanBodega.Visible = False : PanBodega.SuspendLayout()

            For Each BodegaSector As clsBeBodega_sector In BeBodega.Sectores

                'If BS.Activo Then

                'End If

                PanSector = New Panel
                PanSector.Parent = PanBodega
                PanSector.Left = BodegaSector.Pos_x * Zoom
                PanSector.Top = BodegaSector.Pos_y * Zoom

                If Not BodegaSector.Horizontal Then
                    PanSector.Width = BodegaSector.Largo * Zoom + 2
                    PanSector.Height = BodegaSector.Ancho * Zoom
                Else
                    PanSector.Width = BodegaSector.Ancho * Zoom
                    PanSector.Height = BodegaSector.Largo * Zoom + 2
                End If

                PanSector.BorderStyle = BorderStyle.FixedSingle
                PanSector.Visible = True
                PanSector.Name = "IdSedctor: " & BodegaSector.IdSector & " Código: " & BodegaSector.Codigo & " Nombre: " & BodegaSector.Descripcion
                PanSector.BackColor = Color.Transparent
                PanSector.Tag = BodegaSector.IdSector

                AddHandler PanSector.MouseClick, AddressOf PanSector_Click

                Debug.WriteLine("Sector creado: " & PanSector.Name)

                ListTramos = BeBodega.Tramos.FindAll(Function(x) x.IdSector = BodegaSector.IdSector _
                                                                AndAlso x.IdBodega = BodegaSector.IdBodega _
                                                                AndAlso x.IdArea = BodegaSector.IdArea)

                sector_horiz = BodegaSector.Horizontal

                If Not ListTramos Is Nothing Then

                    Try

                        PanSector.SuspendLayout()

                        Dibujar_Tramos(PanSector, ListTramos)

                    Catch ex As Exception
                        Throw ex
                    Finally
                        PanSector.ResumeLayout()
                    End Try


                End If

            Next

            For Each C As Control In PanBodega.Controls
                C.Visible = True
            Next

            Application.DoEvents()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            PanBodega.Visible = True : PanBodega.ResumeLayout()
        End Try

    End Sub

    Private Class clsBeRackExistenteEnTramo

        Public Property IdSector As Integer = 0
        Public Property IdTramo As Integer = 0
        Public Property NombreTramo As String = ""
        Public Property Ancho As Double = 0
        Public Property Izquierda As Double = 0
        Public Property Alto As Double = 0

    End Class

    Private BeTramoAnterior As New clsBeBodega_tramo()
    Dim lTramosExistentes As New List(Of clsBeRackExistenteEnTramo)

    Private Sub Dibujar_Tramos(ByRef SectorContenedor As Panel,
                               ByVal listTramos As List(Of clsBeBodega_tramo))

        Dim PanTramo As Panel
        Dim ListUbic As New List(Of clsBeBodega_ubicacion)
        Dim nivelgr As Integer
        Dim vIndiceExistente As Integer = -1
        Dim Skip_Ubicacion As Boolean = False

        Try

            prgTramos.Properties.PercentView = False
            prgTramos.Properties.Minimum = 0
            prgTramos.Properties.Maximum = listTramos.Count + 1
            prgTramos.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgTramos.Visible = True
            prgTramos.EditValue = 0

            For Each BodegaTramo As clsBeBodega_tramo In listTramos.OrderBy(Function(x) x.IdTramo)

                prgTramos.EditValue = BodegaTramo.IdTramo

                PanTramo = New Panel
                PanTramo.Parent = SectorContenedor
                PanTramo.Left = BodegaTramo.Margen_izquierdo * Zoom
                PanTramo.Top = BodegaTramo.Margen_superior * Zoom

                If BodegaTramo.Horizontal = 0 Then 'vertical
                    PanTramo.Width = BodegaTramo.Ancho * Zoom
                    PanTramo.Height = BodegaTramo.Largo * Zoom
                Else 'Horizontal
                    PanTramo.Width = BodegaTramo.Largo * Zoom
                    PanTramo.Height = BodegaTramo.Ancho * Zoom
                End If

                PanTramo.BorderStyle = BorderStyle.FixedSingle
                PanTramo.Tag = BodegaTramo.IdTramo
                PanTramo.Name = BodegaTramo.IdTramo
                PanTramo.BorderStyle = BorderStyle.FixedSingle

                Dim vFontLabel As New Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel, 1, False)
                Dim BeTramoExistente As New clsBeRackExistenteEnTramo

                If BodegaTramo.Es_Rack Then

                    If sector_horiz Then

                        Dim lblUbicacionA As New LabelControl
                        Dim vLefetiqueta As Integer = 0

                        If BodegaTramo.Descripcion = "R07-A" Then
                            Debug.Assert(True, "aqui")
                        End If

                        vIndiceExistente = lTramosExistentes.FindIndex(Function(x) x.IdSector = BodegaTramo.IdSector)

                        If Not BodegaTramo.Horizontal Then

                            If BodegaTramo.Margen_izquierdo = 1 Then

                                If BodegaTramo.Orientacion = 1 Then

                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left - PanTramo.Width + 60
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - PanTramo.Width + 60
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If

                                Else

                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then

                                            If listTramos.Count > 1 Then
                                                vLefetiqueta = SectorContenedor.Left + (PanTramo.Width * 2)
                                                Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                            Else
                                                vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 10)
                                                Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                            End If

                                        Else
                                            vLefetiqueta = SectorContenedor.Left - PanTramo.Width
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If
                                End If

                            Else

                                If BodegaTramo.Orientacion = 1 Then

                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If
                                Else
                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 10)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If

                                End If

                            End If

                            If vIndiceExistente = -1 Then
                                BeTramoExistente = New clsBeRackExistenteEnTramo()
                                BeTramoExistente.IdTramo = BodegaTramo.IdTramo
                                BeTramoExistente.IdSector = BodegaTramo.IdSector
                                BeTramoExistente.NombreTramo = BodegaTramo.Descripcion
                                BeTramoExistente.Izquierda = PanTramo.Left
                                BeTramoExistente.Ancho = PanTramo.Width
                                BeTramoExistente.Alto = PanTramo.Top
                                lTramosExistentes.Add(BeTramoExistente)
                            End If

                        Else


                        End If

                        '#EJC20210427: No pintar las etiquetas de los pasillos...
                        If Not BodegaTramo.Descripcion.StartsWith("P") Then

                            If BeTramoAnterior.Margen_izquierdo = 0 OrElse SectorContenedor.Left = 0 Then
                                lblUbicacionA.Left = SectorContenedor.Left
                            Else
                                lblUbicacionA.Left = SectorContenedor.Left  '+ BeTramoAnterior.Ancho
                            End If

                            lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.Horizontal

                            If PanTramo.Top = 0 Then
                                lblUbicacionA.Top = (SectorContenedor.Top) - 20
                            Else
                                lblUbicacionA.Top = (SectorContenedor.Top) - 5
                            End If

                            lblUbicacionA.Width = 30
                            lblUbicacionA.Height = 30
                            lblUbicacionA.BorderStyle = BorderStyle.FixedSingle
                            lblUbicacionA.Text = BodegaTramo.Descripcion
                            lblUbicacionA.Font = vFontLabel
                            lblUbicacionA.BackColor = Color.MistyRose
                            lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            lblUbicacionA.Appearance.Options.UseBackColor = True
                            lblUbicacionA.Appearance.Options.UseBackColor = True
                            lblUbicacionA.Parent = PanBodega
                            lblUbicacionA.Tag = BodegaTramo.IdTramo
                            lblUbicacionA.Name = lblUbicacionA.Tag
                            lblUbicacionA.ToolTip = BodegaTramo.Descripcion
                            lblUbicacionA.Cursor = Cursors.Hand
                            lblUbicacionA.BringToFront()
                            SectorContenedor.BackColor = Color.Transparent 'RandomRGBColor()
                            AddHandler lblUbicacionA.Click, AddressOf lblUbicacionG_Click

                        End If

                    Else

                        Dim lblUbicacionA As New LabelControl
                        Dim vLefetiqueta As Integer = 0

                        '#CKFK20250311 Hice este cambio para que se  muestre mejor la etiqueta
                        lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.Horizontal

                        If PanTramo.Top = 0 Then
                            lblUbicacionA.Top = (SectorContenedor.Top) + 10
                        Else
                            lblUbicacionA.Top = (SectorContenedor.Top) - 5
                        End If

                        lblUbicacionA.Left = SectorContenedor.Left + SectorContenedor.Width + 5

                        vIndiceExistente = lTramosExistentes.FindIndex(Function(x) x.IdSector = BodegaTramo.IdSector)

                        If Not BodegaTramo.Horizontal Then

                            If BodegaTramo.Margen_izquierdo = 1 Then

                                If BodegaTramo.Orientacion = 1 Then

                                    If SectorContenedor.Left > 0 Then
                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left - PanTramo.Width + 60
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - PanTramo.Width + 60
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If

                                Else

                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then

                                            If listTramos.Count > 1 Then
                                                vLefetiqueta = SectorContenedor.Left + (PanTramo.Width * 2)
                                                Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                            Else
                                                vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 10)
                                                Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                            End If

                                        Else
                                            vLefetiqueta = SectorContenedor.Left - PanTramo.Width
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If
                                End If

                            Else

                                If BodegaTramo.Orientacion = 1 Then

                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If
                                Else
                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 10)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If

                                End If

                            End If

                            If vIndiceExistente = -1 Then
                                BeTramoExistente = New clsBeRackExistenteEnTramo()
                                BeTramoExistente.IdTramo = BodegaTramo.IdTramo
                                BeTramoExistente.IdSector = BodegaTramo.IdSector
                                BeTramoExistente.NombreTramo = BodegaTramo.Descripcion
                                BeTramoExistente.Izquierda = PanTramo.Left
                                BeTramoExistente.Ancho = PanTramo.Width
                                BeTramoExistente.Alto = PanTramo.Top
                                lTramosExistentes.Add(BeTramoExistente)
                            End If

                        Else

                            If BodegaTramo.Margen_izquierdo = 0 Then

                                If BodegaTramo.Orientacion = 1 Then

                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left ' - PanTramo.Width + 60
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - PanTramo.Width + 60
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If

                                Else

                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then

                                            If listTramos.Count > 1 Then
                                                vLefetiqueta = SectorContenedor.Left + (PanTramo.Width * 2)
                                                Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                            Else
                                                vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 10)
                                                Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                            End If

                                        Else
                                            vLefetiqueta = SectorContenedor.Left - PanTramo.Width
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If
                                End If

                            Else

                                If BodegaTramo.Orientacion = 1 Then

                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If
                                Else
                                    If SectorContenedor.Left > 0 Then

                                        If vIndiceExistente = -1 Then
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 15)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        Else
                                            vLefetiqueta = SectorContenedor.Left - (PanTramo.Width + 10)
                                            Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                        End If

                                    Else
                                        vLefetiqueta = SectorContenedor.Left + PanTramo.Width
                                        Debug.WriteLine("Tramo: " & BodegaTramo.Descripcion & " Left: " & vLefetiqueta)
                                    End If

                                End If

                            End If

                            If vIndiceExistente = -1 Then
                                BeTramoExistente = New clsBeRackExistenteEnTramo()
                                BeTramoExistente.IdTramo = BodegaTramo.IdTramo
                                BeTramoExistente.IdSector = BodegaTramo.IdSector
                                BeTramoExistente.NombreTramo = BodegaTramo.Descripcion
                                BeTramoExistente.Izquierda = PanTramo.Left
                                BeTramoExistente.Ancho = PanTramo.Width
                                lTramosExistentes.Add(BeTramoExistente)
                            End If

                        End If

                        '#EJC20210427: No pintar las etiquetas de los passillos...
                        If Not BodegaTramo.Descripcion.StartsWith("P") Then

                            If BeTramoAnterior.Margen_izquierdo = 0 OrElse SectorContenedor.Left = 0 Then
                                lblUbicacionA.Left = SectorContenedor.Left
                            Else
                                lblUbicacionA.Left = SectorContenedor.Left  '+ BeTramoAnterior.Ancho
                            End If

                            lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.Horizontal

                            If PanTramo.Top = 0 Then
                                lblUbicacionA.Top = (SectorContenedor.Top) + 10
                            Else
                                lblUbicacionA.Top = (SectorContenedor.Top) - 5
                            End If

                            lblUbicacionA.Left = SectorContenedor.Left + SectorContenedor.Width + 5

                            lblUbicacionA.Width = 50
                            lblUbicacionA.Height = 30
                            lblUbicacionA.BorderStyle = BorderStyle.FixedSingle
                            lblUbicacionA.Text = BodegaTramo.Descripcion
                            lblUbicacionA.Font = vFontLabel
                            lblUbicacionA.BackColor = Color.MistyRose
                            lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            lblUbicacionA.Appearance.Options.UseBackColor = True
                            lblUbicacionA.Appearance.Options.UseBackColor = True
                            lblUbicacionA.Parent = PanBodega
                            lblUbicacionA.Tag = BodegaTramo.IdTramo
                            lblUbicacionA.Name = lblUbicacionA.Tag
                            lblUbicacionA.ToolTip = BodegaTramo.Descripcion
                            lblUbicacionA.Cursor = Cursors.Hand
                            lblUbicacionA.BringToFront()
                            SectorContenedor.BackColor = Color.Transparent 'RandomRGBColor()
                            AddHandler lblUbicacionA.Click, AddressOf lblUbicacionG_Click

                        End If

                    End If

                End If

                Debug.WriteLine("Tramo creado: " & PanTramo.Name)

                Try
                    nivelgr = clsLnBodega_tramo.NivelGrafico(BodegaTramo.IdTramo, BodegaTramo.IdBodega)
                Catch ex As Exception
                    nivelgr = 1
                End Try

                ListUbic = BeBodega.Ubicaciones.FindAll(Function(x) x.IdTramo = BodegaTramo.IdTramo _
                                                                    AndAlso x.Nivel = nivelgr _
                                                                    AndAlso x.IdArea = BodegaTramo.IdArea _
                                                                    AndAlso x.IdSector = BodegaTramo.IdSector _
                                                                    AndAlso x.IdBodega = BodegaTramo.IdBodega)

                tramo_orient = BodegaTramo.Orientacion

                Dim Es_Ubicacion_Horizontal As Boolean = BodegaTramo.Horizontal

                '#If DEBUG Then
                '                Skip_Ubicacion = True
                '#End If

                If Not Skip_Ubicacion Then

                    If Not listTramos Is Nothing Then

                        If BodegaTramo.IdTramo = 27 Then

                        End If

                        If BodegaTramo.Es_Rack AndAlso Es_Ubicacion_Horizontal Then
                            'Dibujar_Ubicacion_Horizontal(PanTramo, ListUbic, BodegaTramo)
                            Dibujar_GeoEspacialmente(PanTramo, ListUbic, BodegaTramo)
                        ElseIf BodegaTramo.Es_Rack Then
                            Dibujar_Ubicacion(PanTramo, ListUbic, BodegaTramo)
                        Else
                            If Not sector_horiz Then
                                Dibujar_Ubicacion_Piso_Horizontal(PanTramo, ListUbic, BodegaTramo)
                            Else
                                Dibujar_Ubicacion_Piso_Vertical(PanTramo, ListUbic, BodegaTramo)
                            End If
                        End If

                    End If

                End If

                prgTramos.PerformStep()
                prgTramos.Update()

                'BeTramoAnterior = New clsBeBodega_tramo()
                BeTramoAnterior = BodegaTramo
                BeTramoAnterior.Ancho = SectorContenedor.Width
                BeTramoAnterior.Margen_izquierdo = SectorContenedor.Left

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

    Public Sub Dibujar_GeoEspacialmente(ByVal TramoContenedor As Panel,
                                            ByVal listUbic As List(Of clsBeBodega_ubicacion),
                                            ByVal pTramo As clsBeBodega_tramo)

        Dim vContador As Integer = 1

        Dim lblUbicacionD As New LabelControl
        Dim lblUbicacionC As New LabelControl
        Dim lblUbicacionB As New LabelControl
        Dim lblUbicacionA As New LabelControl

        Dim vTop As Integer = 0
        Dim vLeft As Integer = 0

        Dim vUbicacionesConPosicion As New List(Of clsBeBodega_ubicacion)
        Dim vGraficarPorPosicionDefinidaEnUbicacion As Boolean = False

        Try

            'Aplicar color por defecto.
            Dim ColorTramo As Color = Color.Black
            Dim ColorFont As Color = Color.Black

            If pTramo.pFont.lDet IsNot Nothing AndAlso (pTramo.pFont.lDet.Count > 0) Then

                'Aplicar color por tramo específico
                Dim vColor As String = pTramo.pFont.lDet(0).ColorFondo

                If IsNumeric(vColor) Then
                    ColorTramo = Color.FromArgb(pTramo.pFont.lDet(0).ColorFondo)
                    ColorFont = Color.FromArgb(pTramo.pFont.lDet(0).ColorFont)
                End If

                vFont = New Font(pTramo.pFont.lDet(0).Letra, pTramo.pFont.lDet(0).Tamaño, IIf(pTramo.pFont.lDet(0).Negrita, FontStyle.Bold, FontStyle.Regular), GraphicsUnit.Pixel, 1, False)

            Else
                vFont = New Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Pixel, 1, False)
            End If

            Dim BeBodegaUbic As New clsBeBodega_ubicacion
            prgUbicacionesPorTramo.Properties.PercentView = False
            prgUbicacionesPorTramo.Properties.Minimum = 0
            prgUbicacionesPorTramo.Properties.Maximum = listUbic.Count
            prgUbicacionesPorTramo.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgUbicacionesPorTramo.EditValue = 0

            vContador = 0

            UpdateProgressBar(vContador)

            'Dim vOrientacionPos As String
            '-----------------
            '|BL=2=B | BR=4=D|
            '-----------------
            '|FL=1=A | FR=3=C|
            '-----------------

            'Cuando el IdTipoRack es 1 (Solo una posición) se genera el tipo de orientación FL
            'Cuando el IdTipoRack es 2 (dos posiciones horizontales) se genera el tipo de orientación FL,FR
            'Cuando el IdTipoRack es 3 (dos posiciones verticales) se genera el tipo de orientación FL,BL
            'Cuando el IdTipoRack es 4 (dos posiciones verticales) se genera el tipo de orientación FL,BL,BR,FR

            If Not listUbic Is Nothing Then

                If listUbic.Count = 0 Then
                    Debug.Print("Tramo: " & pTramo.Descripcion & " no tiene ubicaciones definidas")
                    Return
                End If

                Dim IdMaxColumna As Integer = 0

                If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                    IdMaxColumna = (From value In listUbic).Select(Function(x) x.Indice_x).Max()
                End If

                Dim vContadorPosicion As Integer = 0

                vUbicacionesConPosicion = BeBodega.Ubicaciones.FindAll(Function(x) x.IdTramo = pTramo.IdTramo AndAlso (x.Posicion_X > 0))

                If Not vUbicacionesConPosicion Is Nothing Then

                    If vUbicacionesConPosicion.Count > 0 Then
                        vGraficarPorPosicionDefinidaEnUbicacion = True
                    End If

                End If

                If Not vGraficarPorPosicionDefinidaEnUbicacion Then

                    Dim indice_anterior As Integer = 0

                    If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                        listUbic = listUbic.OrderByDescending(Function(x) x.IdUbicacion).ToList
                    Else
                        listUbic = listUbic.FindAll(Function(x) x.IdUbicacion).ToList
                    End If

                    For Each B As clsBeBodega_ubicacion In listUbic '.OrderByDescending(Function(x) x.IdUbicacion)

                        If ListUbicacionesVisibles.Exists(Function(x) x.IdUbicacion = B.IdUbicacion) Then
                            Continue For
                        End If

                        lblUbicacionA = New LabelControl
                        lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionA.Parent = TramoContenedor

                        Select Case pTramo.IdTipoRack

                            Case 1
                                vLeft = B.Ancho * Zoom * (B.Indice_x - 1)
                            Case 2
                                If pTramo.Orden_Descendente Then
                                    If indice_anterior - 1 <> B.Indice_x AndAlso indice_anterior <> 0 Then
                                        vContadorPosicion += 1
                                        vLeft = B.Ancho * Zoom * (vContadorPosicion) * 2
                                    Else
                                        vLeft = B.Ancho * Zoom * (vContadorPosicion) * 2
                                    End If

                                    indice_anterior = B.Indice_x
                                Else
                                    vLeft = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                                End If
                            Case 3, 4
                                If pTramo.Orden_Descendente Then
                                    If indice_anterior - 1 <> B.Indice_x AndAlso indice_anterior <> 0 Then
                                        vContadorPosicion += 1
                                        vLeft = B.Ancho * Zoom * (vContadorPosicion) * 2
                                    Else
                                        vLeft = B.Ancho * Zoom * (vContadorPosicion) * 2
                                    End If
                                    indice_anterior = B.Indice_x
                                Else
                                    If B.Indice_x = 0 Then
                                        vLeft = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                                    Else
                                        vLeft = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                                    End If
                                End If


                        End Select 'Pos A

                        vTop = 0
                        lblUbicacionA.Left = vLeft
                        lblUbicacionA.Top = vTop
                        lblUbicacionA.Width = B.Ancho * Zoom
                        lblUbicacionA.Height = B.Largo * Zoom
                        lblUbicacionA.BorderStyle = BorderStyle.FixedSingle

                        If pTramo.IdTipoRack = 1 Then
                            lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A") '"FL"  - "A"
                        ElseIf B.Tramo.IdTipoRack = 2 Then
                            lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "B")
                        ElseIf B.Tramo.IdTipoRack = 3 Then
                            lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "C") '"FL"  - "A"
                        End If

                        If pTramo.IdTipoRack = 4 Then
                            If pTramo.Orientacion = 0 Then
                                lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 0, "C", "B") '"FL"  - "A"
                            Else
                                lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "C") '"FL"  - "A"
                            End If
                        End If

                        lblUbicacionA.Font = vFont
                        lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionA.Appearance.Options.UseBackColor = True

                        If pTramo.IdTipoRack = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                     AndAlso (x.Orientacion_pos = "1" _
                                                     OrElse
                                                     x.Orientacion_pos = "FL" _
                                                     OrElse
                                                     x.Orientacion_pos = "A") _
                                                     AndAlso x.Nivel = B.Nivel _
                                                     AndAlso x.Indice_x = B.Indice_x)

                        Else

                            If pTramo.IdTipoRack = 2 OrElse pTramo.IdTipoRack = 3 Then

                                If B.Tramo.Orientacion = 1 Then

                                    BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "1" _
                                                      OrElse
                                                      x.Orientacion_pos = "FL" _
                                                      OrElse
                                                      x.Orientacion_pos = "A") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                                Else

                                    '#CKFK 20210923 Agregue esto OrElse x.Orientacion_pos = "B"
                                    BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "3" _
                                                      OrElse
                                                      x.Orientacion_pos = "FR" _
                                                      OrElse
                                                      x.Orientacion_pos = "C" OrElse
                                                      x.Orientacion_pos = "B") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                                End If


                            End If

                        End If

                        If pTramo.IdTipoRack = 4 Then

                            If B.Tramo.Orientacion = 1 Then

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                     AndAlso (x.Orientacion_pos = "1" _
                                                     OrElse
                                                     x.Orientacion_pos = "BL" _
                                                     OrElse
                                                     x.Orientacion_pos = "B") _
                                                     AndAlso x.Nivel = B.Nivel _
                                                     AndAlso x.Indice_x = B.Indice_x)

                            Else

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                     AndAlso (x.Orientacion_pos = "1" _
                                                     OrElse
                                                     x.Orientacion_pos = "FR" _
                                                     OrElse
                                                     x.Orientacion_pos = "C") _
                                                     AndAlso x.Nivel = B.Nivel _
                                                     AndAlso x.Indice_x = B.Indice_x)

                            End If

                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionA.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionA.Name = lblUbicacionA.Tag
                            lblUbicacionA.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionA.Visible = False
                        End If

                        lblUbicacionA.Cursor = Cursors.Hand

                        lblUbicacionA.Appearance.BorderColor = ColorTramo
                        lblUbicacionA.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionA.Click, AddressOf lblUbicacion_Click

                        If pTramo.IdTipoRack = 4 Then

                            lblUbicacionD = New LabelControl
                            lblUbicacionD.AutoSizeMode = LabelAutoSizeMode.None
                            lblUbicacionD.Parent = TramoContenedor

                            'If B.IdUbicacion = 426 Or B.IdUbicacion = 427 Or B.IdUbicacion = 428 Or B.IdUbicacion = 429 Then
                            '    Debug.Print(B.IdUbicacion)
                            'End If

                            If pTramo.Orientacion = 1 Then
                                'lblUbicacionD.Left = lblUbicacionC.Left
                                lblUbicacionD.Left = (B.Ancho * Zoom) + vLeft
                            Else
                                ' lblUbicacionD.Left = lblUbicacionC.Left
                                lblUbicacionD.Left = (B.Ancho * Zoom) + vLeft
                                'lblUbicacionD.Left = (B.Ancho * Zoom) + vLeft
                            End If

                            lblUbicacionD.Top = 0

                            If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                'If pTramo.Orientacion = 1 Then
                                lblUbicacionD.Top = vTop + B.Largo * Zoom
                                'Else
                                '    lblUbicacionD.Top = vTop + B.Largo * Zoom
                                'End If
                            Else
                                lblUbicacionD.Top = vTop + (B.Largo * Zoom)
                            End If

                            lblUbicacionD.Width = B.Ancho * Zoom
                            lblUbicacionD.Height = B.Largo * Zoom
                            lblUbicacionD.BorderStyle = BorderStyle.FixedSingle

                            lblUbicacionD.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "B")

                            lblUbicacionD.Font = vFont
                            lblUbicacionD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            lblUbicacionD.Appearance.Options.UseBackColor = True
                            lblUbicacionD.Appearance.Options.UseBackColor = True
                            lblUbicacionD.Cursor = Cursors.Hand

                            If B.Tramo.Orientacion = 1 Then

                                If pTramo.Orden_Descendente Then

                                    'BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                    '            AndAlso (x.Orientacion_pos = "1" _
                                    '            OrElse
                                    '            x.Orientacion_pos = "FL" _
                                    '            OrElse
                                    '            x.Orientacion_pos = "A") _
                                    '            AndAlso x.Nivel = B.Nivel _
                                    '            AndAlso x.Indice_x = B.Indice_x)

                                    '#CKFK20250514 cambié esto porque no era correcto
                                    BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                          AndAlso (x.Orientacion_pos = "3" _
                                                          OrElse
                                                          x.Orientacion_pos = "BR" _
                                                          OrElse
                                                          x.Orientacion_pos = "C") _
                                                          AndAlso x.Nivel = B.Nivel _
                                                          AndAlso x.Indice_x = B.Indice_x)
                                Else

                                    BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                          AndAlso (x.Orientacion_pos = "3" _
                                                          OrElse
                                                          x.Orientacion_pos = "BR" _
                                                          OrElse
                                                          x.Orientacion_pos = "C") _
                                                          AndAlso x.Nivel = B.Nivel _
                                                          AndAlso x.Indice_x = B.Indice_x)
                                End If



                            Else

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "4" _
                                                      OrElse
                                                      x.Orientacion_pos = "BL" _
                                                      OrElse
                                                      x.Orientacion_pos = "B") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                            End If

                            If Not BeBodegaUbic Is Nothing Then
                                lblUbicacionD.Tag = BeBodegaUbic.IdUbicacion
                                lblUbicacionD.Name = lblUbicacionD.Tag
                                lblUbicacionD.ToolTip = BeBodegaUbic.Descripcion
                                ListUbicacionesVisibles.Add(BeBodegaUbic)
                            Else
                                lblUbicacionD.Visible = False
                            End If

                            lblUbicacionD.Appearance.BorderColor = ColorTramo
                            lblUbicacionD.Appearance.ForeColor = ColorFont

                            AddHandler lblUbicacionD.Click, AddressOf lblUbicacion_Click

                        End If 'fin tipo rack pos D                    

                        If pTramo.IdTipoRack = 2 Then

                            lblUbicacionC = New LabelControl
                            lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                            lblUbicacionC.Parent = TramoContenedor

                            'If B.IdUbicacion = 562 Or B.IdUbicacion = 563 Or B.IdUbicacion = 564 Or B.IdUbicacion = 565 Then
                            '    Debug.Print(B.IdUbicacion)
                            'End If

                            Select Case pTramo.IdTipoRack

                                Case 2

                                    lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                                Case 4

                                    lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * Zoom

                                    ' If pTramo.Orientacion = 0 Then
                                    lblUbicacionC.Left = (B.Margen_izquierdo) * Zoom
                                    'Else
                                    'lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * Zoom
                                    'End If

                                    If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                        lblUbicacionC.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                                    Else
                                        lblUbicacionC.Top = 0
                                    End If

                            End Select

                            lblUbicacionC.Width = B.Ancho * Zoom
                            lblUbicacionC.Height = B.Largo * Zoom
                            lblUbicacionC.BorderStyle = BorderStyle.FixedSingle

                            'Ok
                            '#CKFK 20210727 Cambio en las letras CXB cuando la orientacion es 2
                            If B.Tramo.IdTipoRack = 2 Then
                                lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "A")
                            Else
                                lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A")
                            End If

                            lblUbicacionC.Font = vFont
                            lblUbicacionC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            lblUbicacionC.Appearance.Options.UseBackColor = True
                            lblUbicacionC.ToolTip = " Un mensaje aquí"

                            If B.Tramo.Orientacion = 1 Then

                                '#CKFK 20210923 Agregue esto OrElse
                                'x.Orientacion_pos = "B"
                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "3" _
                                                      OrElse
                                                      x.Orientacion_pos = "FR" OrElse
                                                      x.Orientacion_pos = "C" OrElse
                                                      x.Orientacion_pos = "B") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                            Else

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "1" _
                                                      OrElse
                                                      x.Orientacion_pos = "FL" _
                                                      OrElse
                                                      x.Orientacion_pos = "A") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                            End If

                            If Not BeBodegaUbic Is Nothing Then
                                lblUbicacionC.Tag = BeBodegaUbic.IdUbicacion
                                lblUbicacionC.Name = lblUbicacionC.Tag
                                lblUbicacionC.ToolTip = BeBodegaUbic.Descripcion
                                ListUbicacionesVisibles.Add(BeBodegaUbic)
                            Else
                                lblUbicacionC.Visible = False
                            End If

                            lblUbicacionC.Cursor = Cursors.Hand

                            lblUbicacionC.Appearance.BorderColor = ColorTramo
                            lblUbicacionC.Appearance.ForeColor = ColorFont

                            AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                        End If 'Fin tipo rack

                        If pTramo.IdTipoRack = 4 Then

                            lblUbicacionC = New LabelControl
                            lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                            lblUbicacionC.Parent = TramoContenedor

                            Select Case pTramo.IdTipoRack

                                Case 2

                                    lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                                Case 4

                                    lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                            End Select

                            If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                'If pTramo.Orientacion = 1 Then
                                lblUbicacionC.Top = 0
                                'Else
                                '    lblUbicacionC.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                                'End If
                            Else
                                lblUbicacionC.Top = 0
                            End If

                            lblUbicacionC.Width = B.Ancho * Zoom
                            lblUbicacionC.Height = B.Largo * Zoom
                            lblUbicacionC.BorderStyle = BorderStyle.FixedSingle

                            'Ok
                            lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "D", "A") '"C" '"FR" 'B.Indice_x & IIf(tramo_orient = 1, "C", "D")

                            lblUbicacionC.Font = vFont
                            lblUbicacionC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            lblUbicacionC.Appearance.Options.UseBackColor = True
                            lblUbicacionC.ToolTip = " Un mensaje aquí"

                            If B.Tramo.Orientacion = 1 Then

                                '#CKFK 20211031 cambié  x.Orientacion_pos = "D") _ por C
                                '#CKFK 20220428 agregué condicion de orden
                                If pTramo.Orden_Descendente Then

                                    BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                     AndAlso (x.Orientacion_pos = "4" _
                                                     OrElse
                                                     x.Orientacion_pos = "BR" _
                                                     OrElse
                                                     x.Orientacion_pos = "D") _
                                                     AndAlso x.Nivel = B.Nivel _
                                                     AndAlso x.Indice_x = B.Indice_x)
                                Else
                                    BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                     AndAlso (x.Orientacion_pos = "4" _
                                                     OrElse
                                                     x.Orientacion_pos = "BR" _
                                                     OrElse
                                                     x.Orientacion_pos = "D") _
                                                     AndAlso x.Nivel = B.Nivel _
                                                     AndAlso x.Indice_x = B.Indice_x)
                                End If

                            Else

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "1" _
                                                      OrElse
                                                      x.Orientacion_pos = "FL" _
                                                      OrElse
                                                      x.Orientacion_pos = "A") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                            End If

                            If Not BeBodegaUbic Is Nothing Then
                                lblUbicacionC.Tag = BeBodegaUbic.IdUbicacion
                                lblUbicacionC.Name = lblUbicacionC.Tag
                                lblUbicacionC.ToolTip = BeBodegaUbic.Descripcion
                                ListUbicacionesVisibles.Add(BeBodegaUbic)
                            Else
                                lblUbicacionC.Visible = False
                            End If

                            lblUbicacionC.Cursor = Cursors.Hand

                            lblUbicacionC.Appearance.BorderColor = ColorTramo
                            lblUbicacionC.Appearance.ForeColor = ColorFont

                            AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                        End If 'Pos C                    

                        If pTramo.IdTipoRack = 3 OrElse pTramo.IdTipoRack = 4 Then

                            lblUbicacionB = New LabelControl
                            lblUbicacionB.AutoSizeMode = LabelAutoSizeMode.None
                            lblUbicacionB.Parent = TramoContenedor

                            'If B.IdUbicacion = 426 Or B.IdUbicacion = 427 Or B.IdUbicacion = 428 Or B.IdUbicacion = 429 Then
                            '    Debug.Print(B.IdUbicacion)
                            'End If

                            If pTramo.Orientacion = 1 Then
                                lblUbicacionB.Left = lblUbicacionA.Left '(B.Ancho * Zoom) + vLeft
                            Else
                                lblUbicacionB.Left = lblUbicacionA.Left
                            End If

                            lblUbicacionB.Width = B.Ancho * Zoom
                            lblUbicacionB.Height = B.Largo * Zoom
                            lblUbicacionB.BorderStyle = BorderStyle.FixedSingle

                            If B.Tramo.IdTipoRack = 2 Then
                                lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 
                            Else
                                lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 
                            End If

                            If B.Tramo.IdTipoRack = 4 Then
                                lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 
                            End If

                            If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                ' If pTramo.Orientacion = 1 Then
                                lblUbicacionB.Top = lblUbicacionD.Top ' + B.Ancho * Zoom
                                'Else
                                '    lblUbicacionB.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                                'End If

                            Else
                                lblUbicacionB.Top = lblUbicacionD.Top ' + B.Ancho * Zoom
                            End If

                            lblUbicacionB.Font = vFont
                            lblUbicacionB.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            lblUbicacionB.Appearance.Options.UseBackColor = True
                            lblUbicacionB.ToolTip = " Un mensaje aquí"

                            If B.Tramo.Orientacion = 1 Then

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "1" _
                                                      OrElse
                                                      x.Orientacion_pos = "FL" _
                                                      OrElse
                                                      x.Orientacion_pos = "A") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)

                            Else

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "4" _
                                                      OrElse
                                                      x.Orientacion_pos = "BR" _
                                                      OrElse
                                                      x.Orientacion_pos = "D") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)

                            End If

                            If Not BeBodegaUbic Is Nothing Then
                                lblUbicacionB.Tag = BeBodegaUbic.IdUbicacion
                                lblUbicacionB.Name = lblUbicacionB.Tag
                                lblUbicacionB.ToolTip = BeBodegaUbic.Descripcion
                                ListUbicacionesVisibles.Add(BeBodegaUbic)
                            Else
                                lblUbicacionB.Visible = False
                            End If

                            lblUbicacionB.Cursor = Cursors.Hand

                            lblUbicacionB.Appearance.BorderColor = ColorTramo
                            lblUbicacionB.Appearance.ForeColor = ColorFont

                            AddHandler lblUbicacionB.Click, AddressOf lblUbicacion_Click

                        End If 'Pos B                    

                        UpdateProgressBar(vContador)

                        vContador += 1

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            IdMaxColumna -= 1 : vContadorPosicion += 1
                        End If

                        If Not BeBodegaUbic Is Nothing Then

                            Debug.Print("Tramo: " & BeBodegaUbic.Tramo.Descripcion & " Columna: " & BeBodegaUbic.Indice_x)
                            Debug.Print("lblUbicacionA Tag: " & lblUbicacionA.Tag & ", Left: " & lblUbicacionA.Left & ", Top: " & lblUbicacionA.Top & ", Texto: " & lblUbicacionA.Text & vbCrLf &
                                        "lblUbicacionB Tag: " & lblUbicacionB.Tag & ", Left: " & lblUbicacionB.Left & ", Top: " & lblUbicacionB.Top & ", Texto: " & lblUbicacionB.Text & vbCrLf &
                                        "lblUbicacionC Tag: " & lblUbicacionC.Tag & ", Left: " & lblUbicacionC.Left & ", Top: " & lblUbicacionC.Top & ", Texto: " & lblUbicacionC.Text & vbCrLf &
                                        "lblUbicacionD Tag: " & lblUbicacionD.Tag & ", Left: " & lblUbicacionD.Left & ", Top: " & lblUbicacionD.Top & ", Texto: " & lblUbicacionD.Text)

                        End If

                        'If B.IdUbicacion = 426 Or B.IdUbicacion = 427 Or B.IdUbicacion = 428 Or B.IdUbicacion = 429 Then
                        '    Debug.Print(B.IdUbicacion)
                        'End If

                        TramoContenedor.BackColor = RandomRGBColor()
                        TramoContenedor.Refresh()
                    Next

                Else

                    Try

                        For Each BodegaUbicacionPosicionDefinida As clsBeBodega_ubicacion In listUbic.Where(Function(x) x.Nivel = 1).OrderBy(Function(x) x.IdUbicacion)

                            lblUbicacionA = New LabelControl
                            lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                            lblUbicacionA.Parent = TramoContenedor
                            lblUbicacionA.Left = BodegaUbicacionPosicionDefinida.Posicion_X * Zoom
                            lblUbicacionA.Top = BodegaUbicacionPosicionDefinida.Posicion_Y * Zoom
                            lblUbicacionA.Width = BodegaUbicacionPosicionDefinida.Ancho * Zoom
                            lblUbicacionA.Height = BodegaUbicacionPosicionDefinida.Largo * Zoom
                            lblUbicacionA.BorderStyle = BorderStyle.FixedSingle
                            lblUbicacionA.Font = vFont
                            lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            lblUbicacionA.Appearance.Options.UseBackColor = True
                            lblUbicacionA.Appearance.BorderColor = ColorTramo
                            lblUbicacionA.Appearance.ForeColor = ColorFont
                            lblUbicacionA.Tag = BodegaUbicacionPosicionDefinida.IdUbicacion
                            lblUbicacionA.Name = lblUbicacionA.Tag
                            lblUbicacionA.ToolTip = BodegaUbicacionPosicionDefinida.Descripcion
                            lblUbicacionA.Visible = True
                            lblUbicacionA.Cursor = Cursors.Hand
                            lblUbicacionA.Text = BodegaUbicacionPosicionDefinida.Descripcion
                            ListUbicacionesVisibles.Add(BodegaUbicacionPosicionDefinida)
                            AddHandler lblUbicacionA.Click, AddressOf lblUbicacion_Click
                            UpdateProgressBar(vContador)

                            vContador += 1

                            If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                IdMaxColumna -= 1 : vContadorPosicion += 1
                            End If

                            If Not BeBodegaUbic Is Nothing Then

                                Debug.Print("Tramo: " & BeBodegaUbic.Tramo.Descripcion & " Columna: " & BeBodegaUbic.Indice_x)
                                Debug.Print("lblUbicacionA Tag: " & lblUbicacionA.Tag)

                            End If

                            TramoContenedor.BackColor = RandomRGBColor()
                            TramoContenedor.Refresh()

                        Next

                    Catch ex As Exception
                        Debug.WriteLine(ex.Message)
                    End Try

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", ex.Message, vContador), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    Public Sub Dibujar_Ubicacion_Horizontal_original(ByVal TramoContenedor As Panel,
                                            ByVal listUbic As List(Of clsBeBodega_ubicacion),
                                            ByVal pTramo As clsBeBodega_tramo)

        Dim vContador As Integer = 1

        Dim lblUbicacionD As New LabelControl
        Dim lblUbicacionC As New LabelControl
        Dim lblUbicacionB As New LabelControl
        Dim lblUbicacionA As New LabelControl

        Dim vTop As Integer = 0
        Dim vLeft As Integer = 0

        Try

            'Aplicar color por defecto.
            Dim ColorTramo As Color = Color.Black
            Dim ColorFont As Color = Color.Black

            If pTramo.pFont.lDet IsNot Nothing AndAlso (pTramo.pFont.lDet.Count > 0) Then

                'Aplicar color por tramo específico
                Dim vColor As String = pTramo.pFont.lDet(0).ColorFondo

                If IsNumeric(vColor) Then
                    ColorTramo = Color.FromArgb(pTramo.pFont.lDet(0).ColorFondo)
                    ColorFont = Color.FromArgb(pTramo.pFont.lDet(0).ColorFont)
                End If

                vFont = New Font(pTramo.pFont.lDet(0).Letra, pTramo.pFont.lDet(0).Tamaño, IIf(pTramo.pFont.lDet(0).Negrita, FontStyle.Bold, FontStyle.Regular), GraphicsUnit.Pixel, 1, False)

            Else
                vFont = New Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Pixel, 1, False)
            End If

            Dim BeBodegaUbic As New clsBeBodega_ubicacion
            prgUbicacionesPorTramo.Properties.PercentView = False
            prgUbicacionesPorTramo.Properties.Minimum = 0
            prgUbicacionesPorTramo.Properties.Maximum = listUbic.Count
            prgUbicacionesPorTramo.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgUbicacionesPorTramo.EditValue = 0

            vContador = 0

            UpdateProgressBar(vContador)

            'Dim vOrientacionPos As String
            '-----------------
            '|BL=2=B | BR=4=D|
            '-----------------
            '|FL=1=A | FR=3=C|
            '-----------------

            'Cuando el IdTipoRack es 1 (Solo una posición) se genera el tipo de orientación FL
            'Cuando el IdTipoRack es 2 (dos posiciones horizontales) se genera el tipo de orientación FL,FR
            'Cuando el IdTipoRack es 3 (dos posiciones verticales) se genera el tipo de orientación FL,BL
            'Cuando el IdTipoRack es 4 (dos posiciones verticales) se genera el tipo de orientación FL,BL,BR,FR

            If Not listUbic Is Nothing Then

                Dim IdMaxColumna As Integer = 0

                If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                    IdMaxColumna = (From value In listUbic).Select(Function(x) x.Indice_x).Max()
                End If

                Dim vContadorPosicion As Integer = 0

                For Each B As clsBeBodega_ubicacion In listUbic.OrderByDescending(Function(x) x.IdUbicacion)

                    If B.IdUbicacion = 8601 Or B.IdUbicacion = 8602 Or B.IdUbicacion = 8603 Or B.IdUbicacion = 8604 Then
                        Debug.Print(B.IdUbicacion)
                    End If

                    If ListUbicacionesVisibles.Exists(Function(x) x.IdUbicacion = B.IdUbicacion) Then
                        Continue For
                    End If

                    lblUbicacionA = New LabelControl
                    lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                    lblUbicacionA.Parent = TramoContenedor

                    Select Case pTramo.IdTipoRack

                        Case 1
                            vLeft = B.Ancho * Zoom * (B.Indice_x - 1)
                        Case 2
                            If pTramo.Orden_Descendente Then
                                vLeft = B.Ancho * Zoom * (vContadorPosicion) * 2
                            Else
                                vLeft = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                            End If
                        Case 3, 4
                            If pTramo.Orden_Descendente Then
                                vLeft = B.Ancho * Zoom * (vContadorPosicion) * 2
                            Else
                                vLeft = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                            End If


                    End Select 'Pos A

                    vTop = 0

                    lblUbicacionA.Left = vLeft
                    lblUbicacionA.Top = vTop
                    lblUbicacionA.Width = B.Ancho * Zoom
                    lblUbicacionA.Height = B.Largo * Zoom
                    lblUbicacionA.BorderStyle = BorderStyle.FixedSingle


                    If pTramo.IdTipoRack = 1 Then
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A") '"FL"  - "A"
                    ElseIf B.Tramo.IdTipoRack = 2 Then
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "B")
                    Else
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "C") '"FL"  - "A"
                    End If


                    If pTramo.IdTipoRack = 4 Then
                        If pTramo.Orientacion = 0 Then
                            lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "B") '"FL"  - "A"
                        Else
                            lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "C") '"FL"  - "A"
                        End If
                    End If

                    lblUbicacionA.Font = vFont
                    lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    lblUbicacionA.Appearance.Options.UseBackColor = True

                    If pTramo.IdTipoRack = 1 Then

                        BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "1" _
                                                 OrElse
                                                 x.Orientacion_pos = "FL" _
                                                 OrElse
                                                 x.Orientacion_pos = "A") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)

                    Else

                        If pTramo.IdTipoRack = 2 OrElse pTramo.IdTipoRack = 3 Then

                            If B.Tramo.Orientacion = 1 Then

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                            Else

                                '#CKFK 20210923 Agregue esto OrElse x.Orientacion_pos = "B"
                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "3" _
                                                  OrElse
                                                  x.Orientacion_pos = "FR" _
                                                  OrElse
                                                  x.Orientacion_pos = "C" OrElse
                                                  x.Orientacion_pos = "B") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                            End If


                        End If

                    End If

                    If pTramo.IdTipoRack = 4 Then

                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "1" _
                                                 OrElse
                                                 x.Orientacion_pos = "FR" _
                                                 OrElse
                                                 x.Orientacion_pos = "B") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "1" _
                                                 OrElse
                                                 x.Orientacion_pos = "FR" _
                                                 OrElse
                                                 x.Orientacion_pos = "C") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)

                        End If

                    End If

                    If Not BeBodegaUbic Is Nothing Then
                        lblUbicacionA.Tag = BeBodegaUbic.IdUbicacion
                        lblUbicacionA.Name = lblUbicacionA.Tag
                        lblUbicacionA.ToolTip = BeBodegaUbic.Descripcion
                        ListUbicacionesVisibles.Add(BeBodegaUbic)
                    Else
                        lblUbicacionA.Visible = False
                    End If

                    lblUbicacionA.Cursor = Cursors.Hand

                    lblUbicacionA.Appearance.BorderColor = ColorTramo
                    lblUbicacionA.Appearance.ForeColor = ColorFont

                    AddHandler lblUbicacionA.Click, AddressOf lblUbicacion_Click

                    If pTramo.IdTipoRack = 4 Then

                        lblUbicacionD = New LabelControl
                        lblUbicacionD.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionD.Parent = TramoContenedor

                        If pTramo.Orientacion = 1 Then
                            lblUbicacionD.Left = lblUbicacionA.Left
                        Else
                            lblUbicacionD.Left = (B.Ancho * Zoom) + vLeft
                        End If

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            lblUbicacionD.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                        Else
                            lblUbicacionD.Top = vTop + B.Largo * Zoom
                        End If

                        lblUbicacionD.Top = 0
                        lblUbicacionD.Top = vTop + B.Largo * Zoom

                        lblUbicacionD.Width = B.Ancho * Zoom
                        lblUbicacionD.Height = B.Largo * Zoom
                        lblUbicacionD.BorderStyle = BorderStyle.FixedSingle

                        If pTramo.IdTipoRack = 4 Then
                            lblUbicacionD.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "B")
                        Else
                            lblUbicacionD.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "B")
                        End If

                        lblUbicacionD.Font = vFont
                        lblUbicacionD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionD.Appearance.Options.UseBackColor = True
                        lblUbicacionD.Appearance.Options.UseBackColor = True
                        lblUbicacionD.Cursor = Cursors.Hand

                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "4" _
                                                  OrElse
                                                  x.Orientacion_pos = "BR" _
                                                  OrElse
                                                  x.Orientacion_pos = "D") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "4" _
                                                  OrElse
                                                  x.Orientacion_pos = "BR" _
                                                  OrElse
                                                  x.Orientacion_pos = "B") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionD.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionD.Name = lblUbicacionD.Tag
                            lblUbicacionD.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionD.Visible = False
                        End If

                        lblUbicacionD.Appearance.BorderColor = ColorTramo
                        lblUbicacionD.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionD.Click, AddressOf lblUbicacion_Click

                    End If 'fin tipo rack pos D                    

                    If pTramo.IdTipoRack = 2 Then

                        lblUbicacionC = New LabelControl
                        lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionC.Parent = TramoContenedor

                        Select Case pTramo.IdTipoRack

                            Case 2

                                lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                            Case 4

                                lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * Zoom

                                If pTramo.Orientacion = 0 Then
                                    lblUbicacionC.Left = (B.Margen_izquierdo) * Zoom
                                Else
                                    lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * Zoom
                                End If

                                If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                    lblUbicacionC.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                                Else
                                    lblUbicacionC.Top = 0
                                End If

                        End Select

                        lblUbicacionC.Width = B.Ancho * Zoom
                        lblUbicacionC.Height = B.Largo * Zoom
                        lblUbicacionC.BorderStyle = BorderStyle.FixedSingle

                        'Ok
                        '#CKFK 20210727 Cambio en las letras CXB cuando la orientacion es 2
                        If B.Tramo.IdTipoRack = 2 Then
                            lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "A")
                        Else
                            lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A")
                        End If

                        lblUbicacionC.Font = vFont
                        lblUbicacionC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionC.Appearance.Options.UseBackColor = True
                        lblUbicacionC.ToolTip = " Un mensaje aquí"

                        If B.Tramo.Orientacion = 1 Then

                            '#CKFK 20210923 Agregue esto OrElse
                            'x.Orientacion_pos = "B"
                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "3" _
                                                  OrElse
                                                  x.Orientacion_pos = "FR" OrElse
                                                  x.Orientacion_pos = "C" OrElse
                                                  x.Orientacion_pos = "B") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionC.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionC.Name = lblUbicacionC.Tag
                            lblUbicacionC.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionC.Visible = False
                        End If

                        lblUbicacionC.Cursor = Cursors.Hand

                        lblUbicacionC.Appearance.BorderColor = ColorTramo
                        lblUbicacionC.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                    End If 'Fin tipo rack

                    If pTramo.IdTipoRack = 4 Then

                        lblUbicacionC = New LabelControl
                        lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionC.Parent = TramoContenedor

                        Select Case pTramo.IdTipoRack

                            Case 2

                                lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                            Case 4

                                lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                        End Select

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            lblUbicacionC.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                        Else
                            lblUbicacionC.Top = 0
                        End If

                        lblUbicacionC.Width = B.Ancho * Zoom
                        lblUbicacionC.Height = B.Largo * Zoom
                        lblUbicacionC.BorderStyle = BorderStyle.FixedSingle

                        'Ok
                        lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "D", "A") '"C" '"FR" 'B.Indice_x & IIf(tramo_orient = 1, "C", "D")

                        lblUbicacionC.Font = vFont
                        lblUbicacionC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionC.Appearance.Options.UseBackColor = True
                        lblUbicacionC.ToolTip = " Un mensaje aquí"

                        If B.Tramo.Orientacion = 1 Then

                            '#CKFK 20211031 cambié  x.Orientacion_pos = "D") _ por C
                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "3" _
                                                  OrElse
                                                  x.Orientacion_pos = "FR" OrElse
                                                  x.Orientacion_pos = "C") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionC.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionC.Name = lblUbicacionC.Tag
                            lblUbicacionC.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionC.Visible = False
                        End If

                        lblUbicacionC.Cursor = Cursors.Hand

                        lblUbicacionC.Appearance.BorderColor = ColorTramo
                        lblUbicacionC.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                    End If 'Pos C                    

                    If pTramo.IdTipoRack = 3 OrElse pTramo.IdTipoRack = 4 Then

                        lblUbicacionB = New LabelControl
                        lblUbicacionB.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionB.Parent = TramoContenedor

                        If pTramo.Orientacion = 1 Then
                            lblUbicacionB.Left = (B.Ancho * Zoom) + vLeft
                        Else
                            lblUbicacionB.Left = lblUbicacionA.Left
                        End If

                        lblUbicacionB.Top = lblUbicacionD.Top ' + B.Ancho * Zoom

                        lblUbicacionB.Width = B.Ancho * Zoom
                        lblUbicacionB.Height = B.Largo * Zoom
                        lblUbicacionB.BorderStyle = BorderStyle.FixedSingle

                        If B.Tramo.IdTipoRack = 2 Then
                            lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 
                        Else
                            lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 
                        End If

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            lblUbicacionB.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                        Else
                            ' lblUbicacionB.Top = 0
                        End If

                        lblUbicacionB.Font = vFont
                        lblUbicacionB.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionB.Appearance.Options.UseBackColor = True
                        lblUbicacionB.ToolTip = " Un mensaje aquí"

                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "2" _
                                                  OrElse
                                                  x.Orientacion_pos = "BL" _
                                                  OrElse
                                                  x.Orientacion_pos = "C") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "2" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "D") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)

                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionB.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionB.Name = lblUbicacionB.Tag
                            lblUbicacionB.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionB.Visible = False
                        End If

                        lblUbicacionB.Cursor = Cursors.Hand

                        lblUbicacionB.Appearance.BorderColor = ColorTramo
                        lblUbicacionB.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionB.Click, AddressOf lblUbicacion_Click

                    End If 'Pos B                    

                    UpdateProgressBar(vContador)

                    vContador += 1

                    If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                        IdMaxColumna -= 1 : vContadorPosicion += 1
                    End If

                    Debug.Print("Tramo: " & BeBodegaUbic.Tramo.Descripcion & " Columna: " & BeBodegaUbic.Indice_x)
                    Debug.Print("lblUbicacionA Tag: " & lblUbicacionA.Tag & ", Left: " & lblUbicacionA.Left & ", Top: " & lblUbicacionA.Top & vbCrLf & "lblUbicacionB Tag: " & lblUbicacionB.Tag & ", Left: " & lblUbicacionB.Left & ", Top: " & lblUbicacionB.Top & vbCrLf & "lblUbicacionC Tag: " & lblUbicacionC.Tag & ", Left: " & lblUbicacionC.Left & ", Top: " & lblUbicacionC.Top & vbCrLf & "lblUbicacionD Tag: " & lblUbicacionD.Tag & ", Left: " & lblUbicacionD.Left & ", Top: " & lblUbicacionD.Top)

                    TramoContenedor.BackColor = RandomRGBColor()
                    TramoContenedor.Refresh()
                Next

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", ex.Message, vContador), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    Public Sub Dibujar_Ubicacion(ByVal TramoContenedor As Panel,
                                 ByVal listUbic As List(Of clsBeBodega_ubicacion),
                                 ByVal pTramo As clsBeBodega_tramo)

        Dim vContador As Integer = 1
        Dim lblUbicacionD As New LabelControl
        Dim lblUbicacionC As New LabelControl
        Dim lblUbicacionB As New LabelControl
        Dim lblUbicacionA As New LabelControl
        Dim vTop As Integer = 0
        Dim vListaAnchos As New List(Of Double)
        Dim vSumAnchos As Double = 0
        Dim vLeft As Integer = 0
        Dim IdMaxColumna As Integer = 0
        Dim vCantidadColumnasTramo As Integer = 0
        Dim vIndiceXInverso As Integer = 0

        Try

            'Aplicar color por defecto.
            Dim ColorTramo As Color = Color.Black
            Dim ColorFont As Color = Color.Black

            vListaAnchos.Clear()

            If pTramo.pFont.lDet IsNot Nothing AndAlso (pTramo.pFont.lDet.Count > 0) Then

                'Aplicar color por tramo específico
                Dim vColor As String = pTramo.pFont.lDet(0).ColorFondo

                If IsNumeric(vColor) Then
                    ColorTramo = Color.FromArgb(pTramo.pFont.lDet(0).ColorFondo)
                    ColorFont = Color.FromArgb(pTramo.pFont.lDet(0).ColorFont)
                End If

                vFont = New Font(pTramo.pFont.lDet(0).Letra, pTramo.pFont.lDet(0).Tamaño, IIf(pTramo.pFont.lDet(0).Negrita, FontStyle.Bold, FontStyle.Regular), GraphicsUnit.Pixel, 1, False)

            Else

                'Aplicar font por defecto.
                vFont = New Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Pixel, 1, False)

            End If

            Dim BeBodegaUbic As New clsBeBodega_ubicacion

            prgUbicacionesPorTramo.Properties.PercentView = False
            prgUbicacionesPorTramo.Properties.Minimum = 0
            prgUbicacionesPorTramo.Properties.Maximum = listUbic.Count
            prgUbicacionesPorTramo.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgUbicacionesPorTramo.EditValue = 0

            vContador = 0

            UpdateProgressBar(vContador)

            'Dim vOrientacionPos As String
            '-----------------
            '|BL=2=B | BR=4=D|
            '-----------------
            '|FL=1=A | FR=3=C|
            '-----------------

            'Cuando el IdTipoRack es 1 (Solo una posición) se genera el tipo de orientación FL
            'Cuando el IdTipoRack es 2 (dos posiciones horizontales) se genera el tipo de orientación FL,FR
            'Cuando el IdTipoRack es 3 (dos posiciones verticales) se genera el tipo de orientación FL,BL
            'Cuando el IdTipoRack es 4 (dos posiciones verticales) se genera el tipo de orientación FL,BL,BR,FR

            If Not listUbic Is Nothing Then

                If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then

                    If listUbic.Count > 0 Then
                        IdMaxColumna = (From value In listUbic).Select(Function(x) x.Indice_x).Max()
                        vCantidadColumnasTramo = IdMaxColumna
                    End If

                End If

                For Each B As clsBeBodega_ubicacion In listUbic

                    If ListUbicacionesVisibles.Exists(Function(x) x.IdUbicacion = B.IdUbicacion _
                                                      AndAlso x.IdTramo = pTramo.IdTramo) Then
                        Continue For
                    End If

                    vIndiceXInverso = vCantidadColumnasTramo - B.Indice_x + 1

                    '#EJC20211110: No pintar los offsets cuando se dibuja en posición inversa las columnas.
                    If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                        If Not vIndiceXInverso = IdMaxColumna Then
                            IdMaxColumna -= 1
                            Continue For
                        End If
                    End If

                    If pTramo.IdTipoRack = 4 Then

                        lblUbicacionD = New LabelControl
                        lblUbicacionD.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionD.Parent = TramoContenedor

                        If pTramo.Orientacion = 1 Then
                            lblUbicacionD.Left = B.Margen_izquierdo * Zoom
                        Else
                            lblUbicacionD.Left = (B.Margen_izquierdo + B.Largo) * Zoom
                        End If

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            lblUbicacionD.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                        Else
                            lblUbicacionD.Top = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                        End If

                        lblUbicacionD.Width = B.Largo * Zoom
                        lblUbicacionD.Height = B.Ancho * Zoom
                        lblUbicacionD.BorderStyle = BorderStyle.FixedSingle
                        lblUbicacionD.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "D", "B")
                        lblUbicacionD.Font = vFont
                        lblUbicacionD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionD.Appearance.Options.UseBackColor = True
                        lblUbicacionD.Cursor = Cursors.Hand

                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                                      AndAlso (x.Orientacion_pos = "4" _
                                                                      OrElse
                                                                      x.Orientacion_pos = "BR" _
                                                                      OrElse
                                                                      x.Orientacion_pos = "D") _
                                                                      AndAlso x.Nivel = B.Nivel _
                                                                      AndAlso x.Indice_x = B.Indice_x)

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                                      AndAlso (x.Orientacion_pos = "4" _
                                                                      OrElse
                                                                      x.Orientacion_pos = "BR" _
                                                                      OrElse
                                                                      x.Orientacion_pos = "B") _
                                                                      AndAlso x.Nivel = B.Nivel _
                                                                      AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionD.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionD.Name = lblUbicacionD.Tag
                            lblUbicacionD.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionD.Visible = False
                        End If

                        lblUbicacionD.Appearance.BorderColor = ColorTramo
                        lblUbicacionD.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionD.Click, AddressOf lblUbicacion_Click

                    End If 'fin tipo rack

                    If pTramo.IdTipoRack = 4 Then

                        lblUbicacionC = New LabelControl
                        lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionC.Parent = TramoContenedor

                        lblUbicacionC.Left = (B.Margen_izquierdo + B.Largo) * Zoom

                        If pTramo.Orientacion = 0 Then
                            lblUbicacionC.Left = (B.Margen_izquierdo) * Zoom
                        Else
                            lblUbicacionC.Left = (B.Margen_izquierdo + B.Largo) * Zoom
                        End If

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            lblUbicacionC.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                        Else
                            lblUbicacionC.Top = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                        End If

                        lblUbicacionC.Width = B.Largo * Zoom
                        lblUbicacionC.Height = B.Ancho * Zoom
                        lblUbicacionC.BorderStyle = BorderStyle.FixedSingle
                        lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A")
                        lblUbicacionC.Font = vFont
                        lblUbicacionC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionC.Appearance.Options.UseBackColor = True

                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                                      AndAlso (x.Orientacion_pos = "3" _
                                                                      OrElse
                                                                      x.Orientacion_pos = "FR" OrElse
                                                                      x.Orientacion_pos = "C") _
                                                                      AndAlso x.Nivel = B.Nivel _
                                                                      AndAlso x.Indice_x = B.Indice_x)
                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                                      AndAlso (x.Orientacion_pos = "1" _
                                                                      OrElse
                                                                      x.Orientacion_pos = "FL" _
                                                                      OrElse
                                                                      x.Orientacion_pos = "A") _
                                                                      AndAlso x.Nivel = B.Nivel _
                                                                      AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionC.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionC.Name = lblUbicacionC.Tag
                            lblUbicacionC.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionC.Visible = False
                        End If

                        lblUbicacionC.Cursor = Cursors.Hand

                        'lblUbicacionC.Appearance.BorderColor = ColorTramo
                        lblUbicacionC.Appearance.BorderColor = ColorTramo
                        lblUbicacionC.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                    End If 'Fin tipo rack

                    If pTramo.IdTipoRack = 2 Then

                        lblUbicacionC = New LabelControl
                        lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionC.Parent = TramoContenedor

                        Select Case pTramo.IdTipoRack

                            Case 2

                                lblUbicacionC.Left = (B.Margen_izquierdo) * Zoom

                            Case 4

                                lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * Zoom

                                If pTramo.Orientacion = 0 Then
                                    lblUbicacionC.Left = (B.Margen_izquierdo) * Zoom
                                Else
                                    lblUbicacionC.Left = (B.Margen_izquierdo + B.Largo) * Zoom
                                End If

                        End Select

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            lblUbicacionC.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                        Else
                            lblUbicacionC.Top = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                        End If

                        If pTramo.IdTipoRack = 2 Then
                            lblUbicacionC.Width = B.Largo * Zoom
                            lblUbicacionC.Height = B.Ancho * Zoom
                        Else
                            lblUbicacionC.Width = B.Ancho * Zoom
                            lblUbicacionC.Height = B.Ancho * Zoom
                        End If

                        lblUbicacionC.BorderStyle = BorderStyle.FixedSingle

                        'Ok
                        '#CKFK 20210727 Cambio en las letras CXB cuando la orientacion es 2
                        If B.Tramo.IdTipoRack = 2 Then
                            lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "A")
                        Else
                            lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A")
                        End If

                        lblUbicacionC.Font = vFont
                        lblUbicacionC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionC.Appearance.Options.UseBackColor = True

                        If B.Tramo.Orientacion = 1 Then

                            '#CKFK 20211014 Agregué la orientacion x.Orientacion_pos = "C"
                            If (B.Tramo.IdTipoRack = 4) Then
                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "3" _
                                                 OrElse
                                                 x.Orientacion_pos = "FR" _
                                                 OrElse
                                                 x.Orientacion_pos = "C") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)
                            Else
                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "3" _
                                                 OrElse
                                                 x.Orientacion_pos = "FR" _
                                                 OrElse
                                                 x.Orientacion_pos = "B") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)
                            End If

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionC.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionC.Name = lblUbicacionC.Tag
                            lblUbicacionC.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionC.Visible = False
                        End If

                        lblUbicacionC.Cursor = Cursors.Hand

                        'lblUbicacionC.Appearance.BorderColor = ColorTramo
                        lblUbicacionC.Appearance.BorderColor = ColorTramo
                        lblUbicacionC.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                    End If 'Fin tipo rack

                    If pTramo.IdTipoRack = 3 OrElse pTramo.IdTipoRack = 4 Then

                        lblUbicacionB = New LabelControl
                        lblUbicacionB.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionB.Parent = TramoContenedor

                        If pTramo.Orientacion = 1 Then
                            lblUbicacionB.Left = B.Margen_izquierdo * Zoom
                        Else
                            lblUbicacionB.Left = (B.Margen_izquierdo + B.Largo) * Zoom
                        End If

                        lblUbicacionB.Top = lblUbicacionD.Top + lblUbicacionD.Height

                        lblUbicacionB.Width = B.Largo * Zoom
                        lblUbicacionB.Height = B.Ancho * Zoom
                        lblUbicacionB.BorderStyle = BorderStyle.FixedSingle

                        lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 

                        lblUbicacionB.Font = vFont
                        lblUbicacionB.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionB.Appearance.Options.UseBackColor = True

                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "2" _
                                                  OrElse
                                                  x.Orientacion_pos = "BL" _
                                                  OrElse
                                                  x.Orientacion_pos = "B") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "D") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)

                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionB.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionB.Name = lblUbicacionB.Tag
                            lblUbicacionB.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionB.Visible = False
                        End If

                        lblUbicacionB.Cursor = Cursors.Hand

                        lblUbicacionB.Appearance.BorderColor = ColorTramo
                        lblUbicacionB.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionB.Click, AddressOf lblUbicacion_Click

                    End If

                    lblUbicacionA = New LabelControl
                    lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                    lblUbicacionA.Parent = TramoContenedor

                    Select Case pTramo.IdTipoRack

                        Case 1, 2

                            vLeft = (B.Margen_izquierdo) * Zoom

                        Case 3, 4

                            vLeft = (B.Margen_izquierdo + B.Ancho) * Zoom

                            If pTramo.Orientacion = 0 Then '0 el rack está del lado izquierdo
                                vLeft = (B.Margen_izquierdo) * Zoom
                            Else 'El rack está del lado derecho
                                vLeft = (B.Margen_izquierdo + B.Largo) * Zoom
                            End If

                    End Select

                    Select Case pTramo.IdTipoRack

                        Case 1, 3

                            If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                '#CKFK20230707 Agregué el -1 para que lo comenzara a pintar correctamente
                                vTop = (B.Ancho) * Zoom * (IdMaxColumna - 1)
                            Else

                                '#CKFK 20210814 Agregué lista de anchos para pintar Racks de tipo 1 con columnas de diferentes anchos
                                vSumAnchos = vListaAnchos.Sum
                                vTop = vSumAnchos * Zoom
                                vListaAnchos.Add(B.Ancho)
                                'vTop = (B.Ancho) * Zoom * (B.Indice_x - 1)

                            End If

                        Case 2, 4

                            If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                vTop = (B.Ancho) * Zoom * (IdMaxColumna * 2 - 1)
                            Else
                                vTop = (B.Ancho) * Zoom * (B.Indice_x * 2 - 1)
                            End If

                    End Select

                    lblUbicacionA.Left = vLeft
                    lblUbicacionA.Top = vTop
                    lblUbicacionA.Width = B.Largo * Zoom
                    lblUbicacionA.Height = B.Ancho * Zoom
                    lblUbicacionA.BorderStyle = BorderStyle.FixedSingle

                    If pTramo.IdTipoRack = 1 Then
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A") '"FL"  - "A"
                    ElseIf B.Tramo.IdTipoRack = 2 Then
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "B")
                    Else
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "C") '"FL"  - "A"
                    End If

                    If B.Tramo.IdTipoRack = 2 Then
                        lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "A")
                    Else
                        lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A")
                    End If


                    lblUbicacionA.Font = vFont
                    lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    lblUbicacionA.Appearance.Options.UseBackColor = True

                    If pTramo.IdTipoRack = 1 Then

                        BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)

                    Else
                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        Else

                            '#CKFK 20211014 Agregué la orientacion x.Orientacion_pos = "C"
                            If pTramo.IdTipoRack = 4 Then

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "3" _
                                                      OrElse
                                                      x.Orientacion_pos = "FR" _
                                                      OrElse
                                                      x.Orientacion_pos = "C") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                            Else

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "3" _
                                                      OrElse
                                                      x.Orientacion_pos = "FR" _
                                                      OrElse
                                                      x.Orientacion_pos = "B") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                            End If


                        End If
                    End If

                    If Not BeBodegaUbic Is Nothing Then
                        lblUbicacionA.Tag = BeBodegaUbic.IdUbicacion
                        lblUbicacionA.Name = lblUbicacionA.Tag
                        lblUbicacionA.ToolTip = BeBodegaUbic.Descripcion
                        ListUbicacionesVisibles.Add(BeBodegaUbic)
                    Else
                        lblUbicacionA.Visible = False
                    End If

                    lblUbicacionA.Cursor = Cursors.Hand

                    lblUbicacionA.Appearance.BorderColor = ColorTramo
                    lblUbicacionA.Appearance.ForeColor = ColorFont

                    AddHandler lblUbicacionA.Click, AddressOf lblUbicacion_Click

                    UpdateProgressBar(vContador)

                    vContador += 1

                    If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                        IdMaxColumna -= 1
                    End If

                    Application.DoEvents()

                Next

                TramoContenedor.BackColor = RandomRGBColor()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", ex.Message, vContador), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Sub Dibujar_Ubicacion_Piso_Vertical(ByVal TramoContenedor As Panel,
                                               ByVal listUbic As List(Of clsBeBodega_ubicacion),
                                               ByVal pTramo As clsBeBodega_tramo)

        Dim vContador As Integer = 1

        Dim lblUbicacionD As New LabelControl
        Dim lblUbicacionC As New LabelControl
        Dim lblUbicacionB As New LabelControl
        Dim lblUbicacionA As New LabelControl

        Dim vTop As Integer = 0
        Dim vLeft As Integer = 0

        Try

            'Aplicar color por defecto.
            Dim ColorTramo As Color = Color.Black
            Dim ColorFont As Color = Color.Black

            If pTramo.pFont.lDet IsNot Nothing AndAlso (pTramo.pFont.lDet.Count > 0) Then

                'Aplicar color por tramo específico
                Dim vColor As String = pTramo.pFont.lDet(0).ColorFondo

                If IsNumeric(vColor) Then
                    ColorTramo = Color.FromArgb(pTramo.pFont.lDet(0).ColorFondo)
                    ColorFont = Color.FromArgb(pTramo.pFont.lDet(0).ColorFont)
                End If

                vFont = New Font(pTramo.pFont.lDet(0).Letra, pTramo.pFont.lDet(0).Tamaño, IIf(pTramo.pFont.lDet(0).Negrita, FontStyle.Bold, FontStyle.Regular), GraphicsUnit.Pixel, 1, False)

            Else

                'Aplicar font por defecto.
                vFont = New Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Pixel, 1, False)

            End If

            Dim BeBodegaUbic As New clsBeBodega_ubicacion
            prgUbicacionesPorTramo.Properties.PercentView = False
            prgUbicacionesPorTramo.Properties.Minimum = 0
            prgUbicacionesPorTramo.Properties.Maximum = listUbic.Count
            prgUbicacionesPorTramo.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgUbicacionesPorTramo.EditValue = 0

            vContador = 0

            UpdateProgressBar(vContador)

            For Each B As clsBeBodega_ubicacion In listUbic

                lblUbicacionA = New LabelControl
                lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                lblUbicacionA.Parent = TramoContenedor

                vTop = 0
                vLeft = B.Ancho * Zoom * (B.Indice_x - 1)

                lblUbicacionA.Left = vLeft
                lblUbicacionA.Top = vTop
                lblUbicacionA.Width = B.Ancho * Zoom
                lblUbicacionA.Height = B.Alto * Zoom
                lblUbicacionA.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionA.Text = IIf(B.Tramo.Orientacion = 1, "B", "A") '"FL" 'B.Indice_x & IIf(tramo_orient = 1, "A", "B")
                lblUbicacionA.Font = vFont
                lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                lblUbicacionA.Appearance.Options.UseBackColor = True
                lblUbicacionA.Appearance.Options.UseBackColor = True

                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "1" _
                                                 OrElse
                                                 x.Orientacion_pos = "FL" _
                                                 OrElse
                                                 x.Orientacion_pos = "A") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)

                If Not BeBodegaUbic Is Nothing Then
                    lblUbicacionA.Tag = BeBodegaUbic.IdUbicacion
                Else
                    lblUbicacionA.Visible = False
                End If

                lblUbicacionA.Cursor = Cursors.Hand

                lblUbicacionA.Appearance.BorderColor = ColorTramo
                lblUbicacionA.Appearance.ForeColor = ColorFont

                AddHandler lblUbicacionA.Click, AddressOf lblUbicacion_Click

                UpdateProgressBar(vContador)

                vContador += 1

                TramoContenedor.BackColor = RandomRGBColor()

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", ex.Message, vContador), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Sub Dibujar_Ubicacion_Piso_Horizontal(ByVal TramoContenedor As Panel, ByVal listUbic As List(Of clsBeBodega_ubicacion), ByVal pTramo As clsBeBodega_tramo)

        Dim vContador As Integer = 1

        Dim lblUbicacionD As New LabelControl
        Dim lblUbicacionC As New LabelControl
        Dim lblUbicacionB As New LabelControl
        Dim lblUbicacionA As New LabelControl

        Dim vTop As Integer = 0
        Dim vLeft As Integer = 0

        Try

            'Aplicar color por defecto.
            Dim ColorTramo As Color = Color.Black
            Dim ColorFont As Color = Color.Black

            If pTramo.pFont.lDet IsNot Nothing AndAlso (pTramo.pFont.lDet.Count > 0) Then

                'Aplicar color por tramo específico
                Dim vColor As String = pTramo.pFont.lDet(0).ColorFondo

                If IsNumeric(vColor) Then
                    ColorTramo = Color.FromArgb(pTramo.pFont.lDet(0).ColorFondo)
                    ColorFont = Color.FromArgb(pTramo.pFont.lDet(0).ColorFont)
                End If

                vFont = New Font(pTramo.pFont.lDet(0).Letra, pTramo.pFont.lDet(0).Tamaño, IIf(pTramo.pFont.lDet(0).Negrita, FontStyle.Bold, FontStyle.Regular), GraphicsUnit.Pixel, 1, False)

            Else

                'Aplicar font por defecto.
                vFont = New Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Pixel, 1, False)

            End If

            Dim BeBodegaUbic As New clsBeBodega_ubicacion

            prgUbicacionesPorTramo.Properties.PercentView = False
            prgUbicacionesPorTramo.Properties.Minimum = 0
            prgUbicacionesPorTramo.Properties.Maximum = listUbic.Count
            prgUbicacionesPorTramo.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgUbicacionesPorTramo.EditValue = 0

            vContador = 0

            UpdateProgressBar(vContador)

            For Each B As clsBeBodega_ubicacion In listUbic

                lblUbicacionA = New LabelControl
                lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                lblUbicacionA.Parent = TramoContenedor

                vLeft = 0
                vTop = B.Largo * Zoom * (B.Indice_x - 1)

                lblUbicacionA.Left = vLeft
                lblUbicacionA.Top = vTop
                lblUbicacionA.Width = B.Ancho * Zoom
                lblUbicacionA.Height = B.Largo * Zoom
                lblUbicacionA.BorderStyle = BorderStyle.FixedSingle
                lblUbicacionA.Text = IIf(B.Tramo.Orientacion = 1, "B", "A") '"FL" 'B.Indice_x & IIf(tramo_orient = 1, "A", "B")
                lblUbicacionA.Font = vFont
                lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                lblUbicacionA.Appearance.Options.UseBackColor = True
                lblUbicacionA.Appearance.Options.UseBackColor = True

                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "1" _
                                                 OrElse
                                                 x.Orientacion_pos = "FL" _
                                                 OrElse
                                                 x.Orientacion_pos = "A") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)

                If Not BeBodegaUbic Is Nothing Then
                    lblUbicacionA.Tag = BeBodegaUbic.IdUbicacion
                Else
                    lblUbicacionA.Visible = False
                End If

                lblUbicacionA.Cursor = Cursors.Hand
                lblUbicacionA.Appearance.BorderColor = ColorTramo
                lblUbicacionA.Appearance.ForeColor = ColorFont

                AddHandler lblUbicacionA.Click, AddressOf lblUbicacion_Click

                UpdateProgressBar(vContador)

                vContador += 1

                TramoContenedor.BackColor = RandomRGBColor()

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", ex.Message, vContador), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        '#EJC20180424: FIX
    End Sub

    Public Delegate Sub UpdateProgressBarDelegate(ByVal Contador As Integer)

    Public Sub ReportProgress(ByVal Contador As Integer)
        BeginInvoke(Sub()
                        prgUbicacionesPorTramo.EditValue = Contador
                        prgUbicacionesPorTramo.PerformStep()
                        prgUbicacionesPorTramo.Update()
                    End Sub)
    End Sub

    Public Sub UpdateProgressBar(ByVal pContador As Integer)
        Try
            If (InvokeRequired) Then
                Dim del As New UpdateProgressBarDelegate(AddressOf ReportProgress)
                Invoke(del, pContador)
            Else
                ReportProgress(pContador)
            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub lblUbicacion_Click(sender As Object, e As EventArgs)

        Try

            Dim Ubic As LabelControl
            Ubic = TryCast(sender, LabelControl)

            If Not IsNothing(Ubic) Then

                If Ubic.BackColor = Color.LightGreen Then
                    Ubic.BackColor = Color.White
                Else
                    Ubic.BackColor = Color.LightGreen
                End If

            End If

            Dim vInfo As String = String.Format("IdUbicación: {0} {1}Left: {2},  Top: {3}, {4}Width: {5}, Height: {6}", sender.tag, vbCrLf, Ubic.Left, Ubic.Top, vbCrLf, Ubic.Width, Ubic.Height)

            'XtraMessageBox.Show(vInfo, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim BeBodegaUbicacion As New clsBeBodega_ubicacion() With {.IdUbicacion = sender.tag}
            BeBodegaUbicacion.IdBodega = IdBodega
            clsLnBodega_ubicacion.GetSingle(BeBodegaUbicacion)

            Debug.Print(BeBodegaUbicacion.Tramo.Es_Rack)

            Dim ColView As New frmColumViewervb
            ColView.BeBodegaUbicacion = BeBodegaUbicacion
            ColView.IdBodega = IdBodega
            ColView.ShowDialog()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lblUbicacionG_Click(sender As Object, e As EventArgs)

        Try

            Dim Ubic As LabelControl
            Ubic = TryCast(sender, LabelControl)

            If Not IsNothing(Ubic) Then

                If Ubic.BackColor = Color.LightGreen Then
                    Ubic.BackColor = Color.White
                Else
                    Ubic.BackColor = Color.LightGreen
                End If

            End If

            Dim vInfo As String = String.Format("Identificador: {0} {1}Left: {2},  Top: {3}, {4}Width: {5}, Height: {6}", Ubic.Text & " - TramoId: " & sender.tag, vbCrLf, Ubic.Left, Ubic.Top, vbCrLf, Ubic.Width, Ubic.Height)

            XtraMessageBox.Show(vInfo, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub PanSector_Click(sender As Object, e As EventArgs)

        Try

            Dim Ubic As Panel
            Ubic = TryCast(sender, Panel)

            If Not IsNothing(Ubic) Then

                If Ubic.BackColor = Color.LightGreen Then
                    'Ubic.BackColor = Color.White
                Else
                    'Ubic.BackColor = Color.LightGreen
                End If

            End If

            Dim vInfo As String = String.Format("IdSector: {0} {1} Left: {2},  Top: {3}, {4}Width: {5}, Height: {6} Desc:{7} ", sender.tag, vbCrLf, Ubic.Left, Ubic.Top, vbCrLf, Ubic.Width, Ubic.Height, Ubic.Name)

            XtraMessageBox.Show(vInfo, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Empresas()

        Try

            If IMS.Listar_Empresas(cmbEmpresa) Then

                cmbEmpresa.EditValue = IdEmpresa

                If AP.Listar_Bodegas_Login(cmbBodegas) Then

                    cmbBodegas.SelectedValue = IdBodega

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
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub frmDiseño_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            Listar_Empresas()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub prgTramos_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles prgTramos.CustomDisplayText

        Dim val As String = e.Value.ToString()
        e.DisplayText = "Procesando tramo: " & val

    End Sub

    Private Sub txtZoom_KeyDown(sender As Object, e As KeyEventArgs) Handles txtZoom.KeyDown

        If e.KeyCode = Keys.Enter Then
            Dibujar_Bodega()
        End If

    End Sub

    Private Sub txtCodigo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodigo.KeyDown

        If e.KeyCode = Keys.Enter Then

            If txtCodigo.Text.Trim <> "" Then

                Try

                    If clsLnProducto.Existe_Codigo(txtCodigo.Text.Trim) Then

                        Dim result() As DataRow = DTSTock.Select("codigo = '" & txtCodigo.Text & "'")

                        Dim IdUbicacion As Integer = 0
                        Dim BeUbicacion As New clsBeBodega_ubicacion
                        Dim vContador As Integer = 0

                        ListUbicacionesNoVisibles.Clear()

                        For Each row As DataRow In result

                            IdUbicacion = row("IdUbicacion")

                            BeUbicacion = ListUbicacionesVisibles.Find(Function(x) x.IdUbicacion = IdUbicacion)

                            If BeUbicacion Is Nothing Then
                                BeUbicacion = New clsBeBodega_ubicacion
                                BeUbicacion.IdUbicacion = IdUbicacion
                                BeUbicacion = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(BeUbicacion.IdUbicacion, cmbBodegas.SelectedValue)
                                ListUbicacionesNoVisibles.Add(BeUbicacion)
                            Else
                                CheckLabels(PanBodega, BeUbicacion.IdUbicacion, Color.Red)
                                vContador += 1
                            End If

                            Debug.WriteLine("{0}, {1}", row(0), row(1))

                        Next

                        For Each PosNoVisible In ListUbicacionesNoVisibles.Distinct()

                            BeUbicacion = ListUbicacionesVisibles.Find(Function(x) x.Indice_x = PosNoVisible.Indice_x AndAlso
                                                             x.IdArea = PosNoVisible.IdArea AndAlso
                                                             x.IdBodega = PosNoVisible.IdBodega AndAlso
                                                             x.IdSector = PosNoVisible.IdSector AndAlso
                                                             x.IdTramo = PosNoVisible.IdTramo)


                            If Not BeUbicacion Is Nothing Then
                                Debug.WriteLine("Trying to paint: " & BeUbicacion.NombreCompleto)
                                If BeUbicacion.Nivel = 1 Then
                                    CheckLabels(PanBodega, BeUbicacion.IdUbicacion, Color.Red)
                                    vContador += 1
                                Else
                                    Debug.WriteLine("Aun no se que hacer con esta ubicación... " & BeUbicacion.Nivel)
                                End If
                            Else
                                Debug.WriteLine("Aun no se que hacer con esta ubicación... " & PosNoVisible.NombreCompleto)
                            End If

                        Next

                        XtraMessageBox.Show(vContador & " Coincidencias", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Else
                        XtraMessageBox.Show("Código de producto no válido", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If

        End If

    End Sub

    Private Sub CheckLabels(Container As Control, Value As String)
        For Each C As Control In Container.Controls
            If TypeOf C Is LabelControl AndAlso CType(C, LabelControl).Tag = Value Then
                C.BackColor = Color.Yellow
            ElseIf C.HasChildren Then
                CheckLabels(C, Value)
            End If
        Next
    End Sub

    Private Sub CheckLabels(Container As Control, Value As String, ByVal pColor As Color)
        For Each C As Control In Container.Controls
            If TypeOf C Is LabelControl AndAlso CType(C, LabelControl).Tag = Value Then
                C.BackColor = pColor
            ElseIf C.HasChildren Then
                CheckLabels(C, Value, pColor)
            End If
        Next
    End Sub

    Private Sub Cargar_Stock()

        UseWaitCursor = True

        Try

            DTSTock.Clear()
            DTSTock = clsLnStock.Get_Inventario_Stock(IdBodega)

            'GT 13042021 valida que existe un resultado del inventario
            If DTSTock IsNot Nothing Then
                txtCodigo.AutoCompleteCustomSource.Clear()
                txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
                txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend

                ' Crear una lista para almacenar los valores de la columna que se usará para el autocompletado
                Dim autoCompleteList As New AutoCompleteStringCollection()

                ' Rellenar la lista con los valores de la columna "Codigo"
                For Each row As DataRow In DTSTock.Rows
                    autoCompleteList.Add(row("Codigo").ToString())
                Next

                txtCodigo.AutoCompleteCustomSource = autoCompleteList

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            UseWaitCursor = False
        End Try

    End Sub

    Private Sub Dibujar_Stock()

        UseWaitCursor = True

        Dim IdUbicacion As Integer = 0
        Dim BeUbicacion As New clsBeBodega_ubicacion

        Try

            ListUbicacionesNoVisibles.Clear()
            DTSTock.Clear()
            DTSTock = clsLnStock.Get_Inventario_Stock(IdBodega)

            'GT04012021: si no hay data asociada a la bodega avisar
            If Not DTSTock Is Nothing Then

                For Each Prod As DataRow In DTSTock.Rows

                    IdUbicacion = Prod("IdUbicacion")

                    BeUbicacion = ListUbicacionesVisibles.Find(Function(x) x.IdUbicacion = IdUbicacion)

                    If BeUbicacion Is Nothing Then
                        BeUbicacion = New clsBeBodega_ubicacion
                        BeUbicacion.IdUbicacion = IdUbicacion
                        BeUbicacion = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(BeUbicacion.IdUbicacion,
                                                                                        cmbBodegas.SelectedValue)
                        If BeUbicacion IsNot Nothing Then
                            ListUbicacionesNoVisibles.Add(BeUbicacion)
                        End If
                    Else
                        CheckLabels(PanBodega, BeUbicacion.IdUbicacion)
                    End If

                Next

                ListUbicacionesPiso = ListUbicacionesNoVisibles.FindAll(Function(x) Not x.Tramo.Es_Rack).ToList

                If Not ListUbicacionesNoVisibles Is Nothing Then
                    ListUbicacionesNoVisibles.RemoveAll(Function(x) Not x.Tramo.Es_Rack)
                End If

                For Each PosNoVisible In ListUbicacionesNoVisibles.Distinct()

                    BeUbicacion = ListUbicacionesVisibles.Find(Function(x) x.Indice_x = PosNoVisible.Indice_x AndAlso
                                                     x.IdArea = PosNoVisible.IdArea AndAlso
                                                     x.IdBodega = PosNoVisible.IdBodega AndAlso
                                                     x.IdSector = PosNoVisible.IdSector AndAlso
                                                     x.IdTramo = PosNoVisible.IdTramo)


                    If Not BeUbicacion Is Nothing Then
                        Console.Write("Trying to paint: " & BeUbicacion.NombreCompleto)
                        If BeUbicacion.Nivel = 1 Then
                            CheckLabels(PanBodega, BeUbicacion.IdUbicacion)
                        Else
                            Console.Write("Aun no se que hacer con esta ubicación... " & BeUbicacion.Nivel)
                        End If
                    End If

                Next

            Else
                MsgBox("No hay data asociada a la bodega", MsgBoxStyle.Information, Text)
            End If

            'MsgBox("Inventario actualizado", MsgBoxStyle.Information, Text)

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            UseWaitCursor = False
        End Try

    End Sub

    Private Sub cmdGetStock_Click(sender As Object, e As EventArgs) Handles cmdGetStock.Click

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Consultando existencias...")

            Cargar_Stock()

            Dibujar_Stock()

            SplashScreenManager.Default.SetWaitFormDescription("Fin.")

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub chkInvertir_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvertir.CheckedChanged
        Dibujar_Bodega(False)
    End Sub

    Private Sub cmdActualizar_Click(sender As Object, e As EventArgs) Handles cmdActualizar.Click

        Try

            lControlsToClear = New List(Of Control)
            ListUbicacionesVisibles = New List(Of clsBeBodega_ubicacion)
            ListUbicacionesNoVisibles = New List(Of clsBeBodega_ubicacion)
            ListUbicacionesPiso = New List(Of clsBeBodega_ubicacion)

            pZoom = txtZoom.Value

            Listar_Empresas()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub cmdExportar_Click(sender As Object, e As EventArgs) Handles cmdExportar.Click

        Try

            Dim frm = ActiveForm

            Using bmp = New Bitmap(PanBodega.Width, PanBodega.Height)

                frm.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                sfd.Filter = "PNG Files (*.png*)|*.png"

                If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then

                    PanBodega.DrawToBitmap(bmp, PanBodega.ClientRectangle)
                    bmp.Save(sfd.FileName())

                    XtraMessageBox.Show("Se exportó la imagen de la bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            End Using

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private m_Rnd As New Random

    ' Return a random QB color.
    Public Function RandomQBColor() As Color
        Dim color_num As Integer = m_Rnd.Next(0, 15)
        Return Color.FromArgb(QBColor(color_num) +
            &HFF000000)
    End Function

    ' Return a random RGB color.
    Public Function RandomRGBColor() As Color
        Return Color.FromArgb(255,
            m_Rnd.Next(0, 255),
            m_Rnd.Next(0, 255),
            m_Rnd.Next(0, 255))
    End Function

    Private Sub cmdDibujarGrid_Click(sender As Object, e As EventArgs) Handles cmdDibujarGrid.Click

        Dim vLargoRelativo As Double = 0
        Dim vAnchoRelativo As Double = 0
        Dim vAreaCuadrado As Double = 0

        Try

            If BeBodega Is Nothing Then
                clsLnBodega.Get_Estructura_By_IdBodega(BeBodega)
            End If

            If Not BeBodega Is Nothing Then

                vLargoRelativo = BeBodega.Largo * BeBodega.Zoom
                vAnchoRelativo = BeBodega.Ancho * BeBodega.Zoom

            End If

            Dim lblUbicacionA As New LabelControl

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando cuadrantes...")

            vAreaCuadrado = BeBodega.Zoom

            Dim vAnchoRelativo2 = Math.Round(vAnchoRelativo / vAreaCuadrado, 2)
            Dim vLargoRelativo2 = Math.Round(vLargoRelativo / vAreaCuadrado, 2)
            Dim vCoordenada As String = ""

            'Clear_Panel(PanBodega, True)

            PanBodega.SuspendLayout()

            For x = 0 To vAnchoRelativo2 / 4

                For y = 0 To vLargoRelativo2 / 4

                    SplashScreenManager.Default.SetWaitFormDescription("X : " & x & " Y: " & y)

                    vCoordenada = x & "," & y

                    lblUbicacionA = New LabelControl()
                    lblUbicacionA.Width = (1 * BeBodega.Zoom) * 4
                    lblUbicacionA.Height = (1 * BeBodega.Zoom) * 4
                    lblUbicacionA.Location = New Point((x * BeBodega.Zoom * 4), (y * BeBodega.Zoom * 4))
                    lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                    lblUbicacionA.BorderStyle = BorderStyle.FixedSingle
                    lblUbicacionA.Text = vCoordenada
                    lblUbicacionA.BackColor = Color.Transparent 'RandomRGBColor()
                    lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    lblUbicacionA.Appearance.Options.UseBackColor = True
                    lblUbicacionA.Parent = PanBodega
                    lblUbicacionA.Tag = vCoordenada
                    lblUbicacionA.Name = lblUbicacionA.Tag
                    lblUbicacionA.ToolTip = vCoordenada
                    lblUbicacionA.Cursor = Cursors.Hand
                    lblUbicacionA.SendToBack()
                    'AddHandler lblUbicacionA.Click, AddressOf lblUbicacionG_Click

                    'Application.DoEvents()

                Next

                'Application.DoEvents()

            Next

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show("Done", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
            PanBodega.ResumeLayout()
        End Try


    End Sub

    Public Sub Dibujar_Ubicacion_Horizontal(ByVal TramoContenedor As Panel,
                                            ByVal listUbic As List(Of clsBeBodega_ubicacion),
                                            ByVal pTramo As clsBeBodega_tramo)

        Dim vContador As Integer = 1

        Dim lblUbicacionD As New LabelControl
        Dim lblUbicacionC As New LabelControl
        Dim lblUbicacionB As New LabelControl
        Dim lblUbicacionA As New LabelControl

        Dim vTop As Integer = 0
        Dim vLeft As Integer = 0

        Try

            'Aplicar color por defecto.
            Dim ColorTramo As Color = Color.Black
            Dim ColorFont As Color = Color.Black

            If pTramo.pFont.lDet IsNot Nothing AndAlso (pTramo.pFont.lDet.Count > 0) Then

                'Aplicar color por tramo específico
                Dim vColor As String = pTramo.pFont.lDet(0).ColorFondo

                If IsNumeric(vColor) Then
                    ColorTramo = Color.FromArgb(pTramo.pFont.lDet(0).ColorFondo)
                    ColorFont = Color.FromArgb(pTramo.pFont.lDet(0).ColorFont)
                End If

                vFont = New Font(pTramo.pFont.lDet(0).Letra, pTramo.pFont.lDet(0).Tamaño, IIf(pTramo.pFont.lDet(0).Negrita, FontStyle.Bold, FontStyle.Regular), GraphicsUnit.Pixel, 1, False)

            Else
                vFont = New Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Pixel, 1, False)
            End If

            Dim BeBodegaUbic As New clsBeBodega_ubicacion
            prgUbicacionesPorTramo.Properties.PercentView = False
            prgUbicacionesPorTramo.Properties.Minimum = 0
            prgUbicacionesPorTramo.Properties.Maximum = listUbic.Count
            prgUbicacionesPorTramo.Properties.Step = 1
            prgUbicacionesPorTramo.Visible = True
            prgUbicacionesPorTramo.EditValue = 0

            vContador = 0

            UpdateProgressBar(vContador)

            'Dim vOrientacionPos As String
            '-----------------
            '|BL=2=B | BR=4=D|
            '-----------------
            '|FL=1=A | FR=3=C|
            '-----------------

            'Cuando el IdTipoRack es 1 (Solo una posición) se genera el tipo de orientación FL
            'Cuando el IdTipoRack es 2 (dos posiciones horizontales) se genera el tipo de orientación FL,FR
            'Cuando el IdTipoRack es 3 (dos posiciones verticales) se genera el tipo de orientación FL,BL
            'Cuando el IdTipoRack es 4 (dos posiciones verticales) se genera el tipo de orientación FL,BL,BR,FR

            If Not listUbic Is Nothing Then

                If listUbic.Count = 0 Then
                    Debug.Print("Tramo: " & pTramo.Descripcion & " no tiene ubicaciones definidas")
                    Return
                End If

                Dim IdMaxColumna As Integer = 0

                If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                    IdMaxColumna = (From value In listUbic).Select(Function(x) x.Indice_x).Max()
                End If

                Dim vContadorPosicion As Integer = 0

                For Each B As clsBeBodega_ubicacion In listUbic.OrderByDescending(Function(x) x.IdUbicacion)

                    If ListUbicacionesVisibles.Exists(Function(x) x.IdUbicacion = B.IdUbicacion) Then
                        Continue For
                    End If

                    'If B.IdUbicacion = 426 Or B.IdUbicacion = 427 Or B.IdUbicacion = 428 Or B.IdUbicacion = 429 Then
                    '    Debug.Print(B.IdUbicacion)
                    'End If

                    lblUbicacionA = New LabelControl
                    lblUbicacionA.AutoSizeMode = LabelAutoSizeMode.None
                    lblUbicacionA.Parent = TramoContenedor

                    Select Case pTramo.IdTipoRack

                        Case 1
                            vLeft = B.Ancho * Zoom * (B.Indice_x - 1)
                        Case 2
                            If pTramo.Orden_Descendente Then
                                vLeft = B.Ancho * Zoom * (vContadorPosicion) * 2
                            Else
                                vLeft = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                            End If
                        Case 3, 4
                            If pTramo.Orden_Descendente Then
                                vLeft = B.Ancho * Zoom * (vContadorPosicion) * 2
                            Else
                                If B.Indice_x = 0 Then
                                    vLeft = B.Ancho * Zoom * (B.Indice_x - 1) * 2
                                Else
                                    vLeft = B.Posicion_X
                                End If
                            End If


                    End Select 'Pos A

                    vTop = 0

                    'If B.IdUbicacion = 426 Or B.IdUbicacion = 427 Or B.IdUbicacion = 428 Or B.IdUbicacion = 429 Then
                    '    Debug.Print(B.IdUbicacion)
                    'End If

                    lblUbicacionA.Left = vLeft
                    lblUbicacionA.Top = vTop
                    lblUbicacionA.Width = B.Ancho * Zoom
                    lblUbicacionA.Height = B.Largo * Zoom
                    lblUbicacionA.BorderStyle = BorderStyle.FixedSingle

                    If pTramo.IdTipoRack = 1 Then
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A") '"FL"  - "A"
                    ElseIf B.Tramo.IdTipoRack = 2 Then
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "B")
                    ElseIf B.Tramo.IdTipoRack = 3 Then
                        lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "C") '"FL"  - "A"
                    End If

                    If pTramo.IdTipoRack = 4 Then
                        If pTramo.Orientacion = 0 Then
                            lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 0, "C", "B") '"FL"  - "A"
                        Else
                            lblUbicacionA.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "C") '"FL"  - "A"
                        End If
                    End If

                    lblUbicacionA.Font = vFont
                    lblUbicacionA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    lblUbicacionA.Appearance.Options.UseBackColor = True

                    If pTramo.IdTipoRack = 1 Then

                        BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "1" _
                                                 OrElse
                                                 x.Orientacion_pos = "FL" _
                                                 OrElse
                                                 x.Orientacion_pos = "A") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)

                    Else

                        If pTramo.IdTipoRack = 2 OrElse pTramo.IdTipoRack = 3 Then

                            If B.Tramo.Orientacion = 1 Then

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                            Else

                                '#CKFK 20210923 Agregue esto OrElse x.Orientacion_pos = "B"
                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "3" _
                                                  OrElse
                                                  x.Orientacion_pos = "FR" _
                                                  OrElse
                                                  x.Orientacion_pos = "C" OrElse
                                                  x.Orientacion_pos = "B") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                            End If


                        End If

                    End If

                    If pTramo.IdTipoRack = 4 Then

                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "1" _
                                                 OrElse
                                                 x.Orientacion_pos = "BL" _
                                                 OrElse
                                                 x.Orientacion_pos = "B") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "1" _
                                                 OrElse
                                                 x.Orientacion_pos = "FR" _
                                                 OrElse
                                                 x.Orientacion_pos = "C") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)

                        End If

                    End If

                    If Not BeBodegaUbic Is Nothing Then
                        lblUbicacionA.Tag = BeBodegaUbic.IdUbicacion
                        lblUbicacionA.Name = lblUbicacionA.Tag
                        lblUbicacionA.ToolTip = BeBodegaUbic.Descripcion
                        ListUbicacionesVisibles.Add(BeBodegaUbic)
                    Else
                        lblUbicacionA.Visible = False
                    End If

                    lblUbicacionA.Cursor = Cursors.Hand

                    lblUbicacionA.Appearance.BorderColor = ColorTramo
                    lblUbicacionA.Appearance.ForeColor = ColorFont

                    AddHandler lblUbicacionA.Click, AddressOf lblUbicacion_Click

                    If pTramo.IdTipoRack = 4 Then

                        lblUbicacionD = New LabelControl
                        lblUbicacionD.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionD.Parent = TramoContenedor

                        'If B.IdUbicacion = 426 Or B.IdUbicacion = 427 Or B.IdUbicacion = 428 Or B.IdUbicacion = 429 Then
                        '    Debug.Print(B.IdUbicacion)
                        'End If

                        If pTramo.Orientacion = 1 Then
                            'lblUbicacionD.Left = lblUbicacionC.Left
                            lblUbicacionD.Left = (B.Ancho * Zoom) + vLeft
                        Else
                            ' lblUbicacionD.Left = lblUbicacionC.Left
                            lblUbicacionD.Left = (B.Ancho * Zoom) + vLeft
                            'lblUbicacionD.Left = (B.Ancho * Zoom) + vLeft
                        End If

                        lblUbicacionD.Top = 0

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            'If pTramo.Orientacion = 1 Then
                            lblUbicacionD.Top = vTop + B.Largo * Zoom
                            'Else
                            '    lblUbicacionD.Top = vTop + B.Largo * Zoom
                            'End If
                        Else
                            lblUbicacionD.Top = vTop + (B.Largo * Zoom)
                        End If

                        lblUbicacionD.Width = B.Ancho * Zoom
                        lblUbicacionD.Height = B.Largo * Zoom
                        lblUbicacionD.BorderStyle = BorderStyle.FixedSingle

                        lblUbicacionD.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "B")

                        lblUbicacionD.Font = vFont
                        lblUbicacionD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionD.Appearance.Options.UseBackColor = True
                        lblUbicacionD.Appearance.Options.UseBackColor = True
                        lblUbicacionD.Cursor = Cursors.Hand

                        If B.Tramo.Orientacion = 1 Then

                            If pTramo.Orden_Descendente Then

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                            AndAlso (x.Orientacion_pos = "1" _
                                            OrElse
                                            x.Orientacion_pos = "FL" _
                                            OrElse
                                            x.Orientacion_pos = "A") _
                                            AndAlso x.Nivel = B.Nivel _
                                            AndAlso x.Indice_x = B.Indice_x)
                            Else

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                      AndAlso (x.Orientacion_pos = "3" _
                                                      OrElse
                                                      x.Orientacion_pos = "BR" _
                                                      OrElse
                                                      x.Orientacion_pos = "C") _
                                                      AndAlso x.Nivel = B.Nivel _
                                                      AndAlso x.Indice_x = B.Indice_x)
                            End If



                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "4" _
                                                  OrElse
                                                  x.Orientacion_pos = "BL" _
                                                  OrElse
                                                  x.Orientacion_pos = "B") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionD.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionD.Name = lblUbicacionD.Tag
                            lblUbicacionD.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionD.Visible = False
                        End If

                        lblUbicacionD.Appearance.BorderColor = ColorTramo
                        lblUbicacionD.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionD.Click, AddressOf lblUbicacion_Click

                    End If 'fin tipo rack pos D                    

                    If pTramo.IdTipoRack = 2 Then

                        lblUbicacionC = New LabelControl
                        lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionC.Parent = TramoContenedor

                        'If B.IdUbicacion = 562 Or B.IdUbicacion = 563 Or B.IdUbicacion = 564 Or B.IdUbicacion = 565 Then
                        '    Debug.Print(B.IdUbicacion)
                        'End If

                        Select Case pTramo.IdTipoRack

                            Case 2

                                lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                            Case 4

                                lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * Zoom

                                ' If pTramo.Orientacion = 0 Then
                                lblUbicacionC.Left = (B.Margen_izquierdo) * Zoom
                                'Else
                                'lblUbicacionC.Left = (B.Margen_izquierdo + B.Ancho) * Zoom
                                'End If

                                If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                                    lblUbicacionC.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                                Else
                                    lblUbicacionC.Top = 0
                                End If

                        End Select

                        lblUbicacionC.Width = B.Ancho * Zoom
                        lblUbicacionC.Height = B.Largo * Zoom
                        lblUbicacionC.BorderStyle = BorderStyle.FixedSingle

                        'Ok
                        '#CKFK 20210727 Cambio en las letras CXB cuando la orientacion es 2
                        If B.Tramo.IdTipoRack = 2 Then
                            lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "B", "A")
                        Else
                            lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "A")
                        End If

                        lblUbicacionC.Font = vFont
                        lblUbicacionC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionC.Appearance.Options.UseBackColor = True
                        lblUbicacionC.ToolTip = " Un mensaje aquí"

                        If B.Tramo.Orientacion = 1 Then

                            '#CKFK 20210923 Agregue esto OrElse
                            'x.Orientacion_pos = "B"
                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "3" _
                                                  OrElse
                                                  x.Orientacion_pos = "FR" OrElse
                                                  x.Orientacion_pos = "C" OrElse
                                                  x.Orientacion_pos = "B") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionC.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionC.Name = lblUbicacionC.Tag
                            lblUbicacionC.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionC.Visible = False
                        End If

                        lblUbicacionC.Cursor = Cursors.Hand

                        lblUbicacionC.Appearance.BorderColor = ColorTramo
                        lblUbicacionC.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                    End If 'Fin tipo rack

                    If pTramo.IdTipoRack = 4 Then

                        lblUbicacionC = New LabelControl
                        lblUbicacionC.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionC.Parent = TramoContenedor

                        Select Case pTramo.IdTipoRack

                            Case 2

                                lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                            Case 4

                                lblUbicacionC.Left = (B.Ancho * Zoom) + vLeft

                        End Select

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            'If pTramo.Orientacion = 1 Then
                            lblUbicacionC.Top = 0
                            'Else
                            '    lblUbicacionC.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                            'End If
                        Else
                            lblUbicacionC.Top = 0
                        End If

                        lblUbicacionC.Width = B.Ancho * Zoom
                        lblUbicacionC.Height = B.Largo * Zoom
                        lblUbicacionC.BorderStyle = BorderStyle.FixedSingle

                        'Ok
                        lblUbicacionC.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "D", "A") '"C" '"FR" 'B.Indice_x & IIf(tramo_orient = 1, "C", "D")

                        lblUbicacionC.Font = vFont
                        lblUbicacionC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionC.Appearance.Options.UseBackColor = True
                        lblUbicacionC.ToolTip = " Un mensaje aquí"

                        If B.Tramo.Orientacion = 1 Then

                            '#CKFK 20211031 cambié  x.Orientacion_pos = "D") _ por C
                            '#CKFK 20220428 agregué condicion de orden
                            If pTramo.Orden_Descendente Then

                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "4" _
                                                 OrElse
                                                 x.Orientacion_pos = "BR" _
                                                 OrElse
                                                 x.Orientacion_pos = "D") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)
                            Else
                                BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                 AndAlso (x.Orientacion_pos = "4" _
                                                 OrElse
                                                 x.Orientacion_pos = "BR" _
                                                 OrElse
                                                 x.Orientacion_pos = "D") _
                                                 AndAlso x.Nivel = B.Nivel _
                                                 AndAlso x.Indice_x = B.Indice_x)
                            End If

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                                  AndAlso (x.Orientacion_pos = "1" _
                                                  OrElse
                                                  x.Orientacion_pos = "FL" _
                                                  OrElse
                                                  x.Orientacion_pos = "A") _
                                                  AndAlso x.Nivel = B.Nivel _
                                                  AndAlso x.Indice_x = B.Indice_x)
                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionC.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionC.Name = lblUbicacionC.Tag
                            lblUbicacionC.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionC.Visible = False
                        End If

                        lblUbicacionC.Cursor = Cursors.Hand

                        lblUbicacionC.Appearance.BorderColor = ColorTramo
                        lblUbicacionC.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionC.Click, AddressOf lblUbicacion_Click

                    End If 'Pos C                    

                    If pTramo.IdTipoRack = 3 OrElse pTramo.IdTipoRack = 4 Then

                        lblUbicacionB = New LabelControl
                        lblUbicacionB.AutoSizeMode = LabelAutoSizeMode.None
                        lblUbicacionB.Parent = TramoContenedor

                        'If B.IdUbicacion = 426 Or B.IdUbicacion = 427 Or B.IdUbicacion = 428 Or B.IdUbicacion = 429 Then
                        '    Debug.Print(B.IdUbicacion)
                        'End If

                        If pTramo.Orientacion = 1 Then
                            lblUbicacionB.Left = lblUbicacionA.Left '(B.Ancho * Zoom) + vLeft
                        Else
                            lblUbicacionB.Left = lblUbicacionA.Left
                        End If

                        lblUbicacionB.Width = B.Ancho * Zoom
                        lblUbicacionB.Height = B.Largo * Zoom
                        lblUbicacionB.BorderStyle = BorderStyle.FixedSingle

                        If B.Tramo.IdTipoRack = 2 Then
                            lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 
                        Else
                            lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "C", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 
                        End If

                        If B.Tramo.IdTipoRack = 4 Then
                            lblUbicacionB.Text = B.Indice_x & IIf(B.Tramo.Orientacion = 1, "A", "D")   '"BL" 'B.Indice_x & IIf(tramo_orient = 1, "B", "A") "B" 
                        End If

                        If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                            ' If pTramo.Orientacion = 1 Then
                            lblUbicacionB.Top = lblUbicacionD.Top ' + B.Ancho * Zoom
                            'Else
                            '    lblUbicacionB.Top = B.Ancho * Zoom * (IdMaxColumna - 1) * 2
                            'End If

                        Else
                            lblUbicacionB.Top = lblUbicacionD.Top ' + B.Ancho * Zoom
                        End If

                        lblUbicacionB.Font = vFont
                        lblUbicacionB.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        lblUbicacionB.Appearance.Options.UseBackColor = True
                        lblUbicacionB.ToolTip = " Un mensaje aquí"

                        If B.Tramo.Orientacion = 1 Then

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                              AndAlso (x.Orientacion_pos = "1" _
                                              OrElse
                                              x.Orientacion_pos = "FL" _
                                              OrElse
                                              x.Orientacion_pos = "A") _
                                              AndAlso x.Nivel = B.Nivel _
                                              AndAlso x.Indice_x = B.Indice_x)

                        Else

                            BeBodegaUbic = BeBodega.Ubicaciones.Find(Function(x) x.IdTramo = B.IdTramo _
                                              AndAlso (x.Orientacion_pos = "4" _
                                              OrElse
                                              x.Orientacion_pos = "BR" _
                                              OrElse
                                              x.Orientacion_pos = "D") _
                                              AndAlso x.Nivel = B.Nivel _
                                              AndAlso x.Indice_x = B.Indice_x)

                        End If

                        If Not BeBodegaUbic Is Nothing Then
                            lblUbicacionB.Tag = BeBodegaUbic.IdUbicacion
                            lblUbicacionB.Name = lblUbicacionB.Tag
                            lblUbicacionB.ToolTip = BeBodegaUbic.Descripcion
                            ListUbicacionesVisibles.Add(BeBodegaUbic)
                        Else
                            lblUbicacionB.Visible = False
                        End If

                        lblUbicacionB.Cursor = Cursors.Hand

                        lblUbicacionB.Appearance.BorderColor = ColorTramo
                        lblUbicacionB.Appearance.ForeColor = ColorFont

                        AddHandler lblUbicacionB.Click, AddressOf lblUbicacion_Click

                    End If 'Pos B                    

                    UpdateProgressBar(vContador)

                    vContador += 1

                    If chkInvertir.Checked OrElse pTramo.Orden_Descendente Then
                        IdMaxColumna -= 1 : vContadorPosicion += 1
                    End If

                    If Not BeBodegaUbic Is Nothing Then

                        Debug.Print("Tramo: " & BeBodegaUbic.Tramo.Descripcion & " Columna: " & BeBodegaUbic.Indice_x)
                        Debug.Print("lblUbicacionA Tag: " & lblUbicacionA.Tag & ", Left: " & lblUbicacionA.Left & ", Top: " & lblUbicacionA.Top & ", Texto: " & lblUbicacionA.Text & vbCrLf &
                                    "lblUbicacionB Tag: " & lblUbicacionB.Tag & ", Left: " & lblUbicacionB.Left & ", Top: " & lblUbicacionB.Top & ", Texto: " & lblUbicacionB.Text & vbCrLf &
                                    "lblUbicacionC Tag: " & lblUbicacionC.Tag & ", Left: " & lblUbicacionC.Left & ", Top: " & lblUbicacionC.Top & ", Texto: " & lblUbicacionC.Text & vbCrLf &
                                    "lblUbicacionD Tag: " & lblUbicacionD.Tag & ", Left: " & lblUbicacionD.Left & ", Top: " & lblUbicacionD.Top & ", Texto: " & lblUbicacionD.Text)

                    End If

                    'If B.IdUbicacion = 426 Or B.IdUbicacion = 427 Or B.IdUbicacion = 428 Or B.IdUbicacion = 429 Then
                    '    Debug.Print(B.IdUbicacion)
                    'End If

                    TramoContenedor.BackColor = RandomRGBColor()
                    TramoContenedor.Refresh()
                Next

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", ex.Message, vContador), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

End Class