Imports System.Diagnostics
Imports DevExpress.XtraEditors

Public Class frmDisenoB_V2
    Inherits XtraForm

    Private ReadOnly pnlLeft As New PanelControl()
    Private ReadOnly pnlCanvasHost As New XtraScrollableControl()
    Private ReadOnly pnlBorde As New PanelControl()
    Private ReadOnly pnlBodega As New PanelControl()

    Private ReadOnly lblEmpresa As New LabelControl()
    Private ReadOnly lblBodega As New LabelControl()
    Private ReadOnly lblZoom As New LabelControl()

    Private ReadOnly cmbEmpresa As New LookUpEdit()
    Private ReadOnly cmbBodegas As New LookUpEdit()
    Private ReadOnly nudZoom As New NumericUpDown()
    Private ReadOnly chkInvertir As New CheckBox()

    Private ReadOnly btnActualizar As New SimpleButton()
    Private ReadOnly btnRedibujar As New SimpleButton()

    Public Property IdEmpresa As Integer = 0
    Public Property IdBodega As Integer = 0

    Private _beBodega As New clsBeBodega()

    Public Sub New()
        MyBase.New()
        InitializeUi()
        AddHandler Me.Shown, AddressOf FrmDisenoB_V2_Shown
    End Sub

    Private Sub InitializeUi()
        Me.Text = "Graficador de bodega (V2)"
        Me.WindowState = FormWindowState.Maximized

        pnlLeft.Dock = DockStyle.Left
        pnlLeft.Width = 320

        pnlCanvasHost.Dock = DockStyle.Fill
        pnlCanvasHost.Controls.Add(pnlBorde)

        pnlBorde.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        pnlBorde.Location = New Point(12, 12)
        pnlBorde.Size = New Size(900, 600)
        pnlBorde.Controls.Add(pnlBodega)

        pnlBodega.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        pnlBodega.Location = New Point(10, 10)
        pnlBodega.Size = New Size(500, 300)

        lblEmpresa.Text = "Empresa"
        lblEmpresa.Location = New Point(16, 20)
        cmbEmpresa.Location = New Point(16, 42)
        cmbEmpresa.Width = 280

        lblBodega.Text = "Bodega"
        lblBodega.Location = New Point(16, 78)
        cmbBodegas.Location = New Point(16, 100)
        cmbBodegas.Width = 280
        cmbBodegas.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor

        lblZoom.Text = "Zoom"
        lblZoom.Location = New Point(16, 138)
        nudZoom.Location = New Point(16, 160)
        nudZoom.Width = 120
        nudZoom.DecimalPlaces = 2
        nudZoom.Minimum = 1
        nudZoom.Maximum = 100
        nudZoom.Value = 20

        chkInvertir.Text = "Invertir"
        chkInvertir.Location = New Point(16, 196)

        btnActualizar.Text = "Actualizar"
        btnActualizar.Location = New Point(16, 232)
        btnActualizar.Width = 280

        btnRedibujar.Text = "Redibujar"
        btnRedibujar.Location = New Point(16, 268)
        btnRedibujar.Width = 280

        AddHandler btnActualizar.Click, AddressOf BtnActualizar_Click
        AddHandler btnRedibujar.Click, AddressOf BtnRedibujar_Click
        AddHandler cmbBodegas.SelectedIndexChanged, AddressOf CmbBodegas_SelectedIndexChanged

        pnlLeft.Controls.Add(lblEmpresa)
        pnlLeft.Controls.Add(cmbEmpresa)
        pnlLeft.Controls.Add(lblBodega)
        pnlLeft.Controls.Add(cmbBodegas)
        pnlLeft.Controls.Add(lblZoom)
        pnlLeft.Controls.Add(nudZoom)
        pnlLeft.Controls.Add(chkInvertir)
        pnlLeft.Controls.Add(btnActualizar)
        pnlLeft.Controls.Add(btnRedibujar)

        Me.Controls.Add(pnlCanvasHost)
        Me.Controls.Add(pnlLeft)
    End Sub

    Private Sub FrmDisenoB_V2_Shown(sender As Object, e As EventArgs)
        CargarContexto()
    End Sub

    Private Sub CargarContexto()
        Try
            Debug.WriteLine("[frmDisenoB_V2] CargarContexto() iniciando")

            If IMS.Listar_Empresas(cmbEmpresa) Then
                If IdEmpresa > 0 Then
                    cmbEmpresa.EditValue = IdEmpresa
                End If
            End If

            If AP.Listar_BodegasLogin(cmbBodegas) Then
                If IdBodega > 0 Then
                    cmbBodegas.EditValue = IdBodega
                End If
            End If

            RedibujarBodega()

        Catch ex As Exception
            Debug.WriteLine("[frmDisenoB_V2] CargarContexto ERROR: " & ex.Message)
            XtraMessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmbBodegas_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Me.Visible Then
            RedibujarBodega()
        End If
    End Sub

    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs)
        RedibujarBodega()
    End Sub

    Private Sub BtnRedibujar_Click(sender As Object, e As EventArgs)
        RedibujarBodega()
    End Sub

    Private Sub RedibujarBodega()
        Dim sw As Stopwatch = Stopwatch.StartNew()

        Try
            If cmbBodegas.EditValue Is Nothing Then
                Debug.WriteLine("[frmDisenoB_V2] Redibujar cancelado: no hay bodega seleccionada")
                Return
            End If

            Dim idBodegaSeleccionada As Integer
            If Not Integer.TryParse(cmbBodegas.EditValue.ToString(), idBodegaSeleccionada) Then
                Debug.WriteLine("[frmDisenoB_V2] Redibujar cancelado: EditValue invalido=" & cmbBodegas.EditValue.ToString())
                Return
            End If

            _beBodega = New clsBeBodega With {
                .IdBodega = idBodegaSeleccionada,
                .Zoom = Convert.ToDouble(nudZoom.Value)
            }

            Dim swData As Stopwatch = Stopwatch.StartNew()
            clsLnBodega.Get_Estructura_By_IdBodega(_beBodega)
            swData.Stop()

            Debug.WriteLine(String.Format("[frmDisenoB_V2] Estructura cargada: IdBodega={0}, Sectores={1}, Tramos={2}, Ubicaciones={3}, Zoom={4}, DataMs={5}",
                _beBodega.IdBodega,
                If(_beBodega.Sectores Is Nothing, 0, _beBodega.Sectores.Count),
                If(_beBodega.Tramos Is Nothing, 0, _beBodega.Tramos.Count),
                If(_beBodega.Ubicaciones Is Nothing, 0, _beBodega.Ubicaciones.Count),
                _beBodega.Zoom,
                swData.ElapsedMilliseconds))

            pnlBodega.Controls.Clear()

            Dim graficador As New clsGraficadorBodega(pnlBodega, pnlBorde)
            graficador.BeBodega = _beBodega
            graficador.Dibujar_Bodega(pnlBodega, pnlBorde)

            sw.Stop()
            Debug.WriteLine("[frmDisenoB_V2] Redibujar completado en " & sw.ElapsedMilliseconds & " ms")

        Catch ex As Exception
            sw.Stop()
            Debug.WriteLine("[frmDisenoB_V2] Redibujar ERROR en " & sw.ElapsedMilliseconds & " ms: " & ex.Message)
            XtraMessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
