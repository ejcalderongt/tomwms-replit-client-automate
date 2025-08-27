Imports System.Drawing.Printing
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmTicketN

    Private BePiloto As New clsBeEmpresa_transporte_pilotos
    Private BeVehiculo As New clsBeEmpresa_transporte_vehiculos
    Private ProcesoPiloto As Boolean = False
    Private Nombre_Impresora As String = ""

    'GT 26012021 clase para tms_ticket_pol
    Private BeTmsTicketPol As New clsBeTms_ticket_pol
    Private enc_duca As New clsBeCEALSA_DUCA_ENC

    Public Property Modo As ModoTrans

    Public Enum ModoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Private Sub txtNoDocumentoPiloto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNoDocumentoPiloto.KeyDown

        If e.KeyCode = Keys.Enter Then

            If txtNoDocumentoPiloto.Text.Trim <> "" Then

                If txtNoDocumentoPiloto.Text.Length >= 200 Then

                    Try

                        Dim vValoresLicencia() As String = txtNoDocumentoPiloto.Text.Split("|")

                        txtNoDocumentoPiloto.Text = vValoresLicencia(8)
                        txtTipoLicencia.Text = vValoresLicencia(1)
                        txtNombresPiloto.Text = vValoresLicencia(2) & " " & vValoresLicencia(3)
                        txtApellidosPiloto.Text = vValoresLicencia(4)
                        dtpFechaVencePiloto.EditValue = vValoresLicencia(7)

                        Select Case txtTipoLicencia.Text

                            Case "A"
                                txtTipoLicencia.ForeColor = Color.Firebrick
                            Case "B"
                                txtTipoLicencia.ForeColor = Color.DarkGreen
                            Case "C"
                                txtTipoLicencia.ForeColor = Color.Chocolate
                            Case "M"
                                txtTipoLicencia.ForeColor = Color.MediumVioletRed
                            Case "E"
                                txtTipoLicencia.ForeColor = Color.DarkGreen

                        End Select

                        ProcesoPiloto = Procesar_Piloto(True)

                        Dim vFechaVence As Date = dtpFechaVencePiloto.EditValue
                        Dim vDiasVencimiento As Integer = DateDiff(DateInterval.Day, Now, vFechaVence)

                        lblStatusLicencia.AppendText("Días al vencimiento: " & vDiasVencimiento)
                        lblStatusLicencia.AppendText(vbNewLine)
                        lblStatusLicencia.SelectionStart = lblStatusLicencia.TextLength
                        lblStatusLicencia.ScrollToCaret()
                        lblStatusLicencia.Refresh()

                        If vDiasVencimiento < 0 Then
                            lblStatusLicencia.BackColor = Color.LightCoral
                        Else
                            lblStatusLicencia.BackColor = Color.LightGreen
                        End If

                        Dim BeTicketUltVisita As New clsBeTms_ticket
                        BeTicketUltVisita = clsLnTms_ticket.Get_Ultima_Visita_By_IdPiloto(BePiloto.IdPiloto)

                        If Not BeTicketUltVisita Is Nothing Then
                            lblStatusLicencia.AppendText("Ult. Visita: " & BeTicketUltVisita.Fecha_Ingreso)
                        Else
                            lblStatusLicencia.AppendText("El piloto nos visita por primera vez, una cordial bienvenida" & vbNewLine)
                        End If

                        lblStatusLicencia.AppendText(vbNewLine)
                        lblStatusLicencia.SelectionStart = lblStatusLicencia.TextLength
                        lblStatusLicencia.ScrollToCaret()
                        lblStatusLicencia.Refresh()

                    Catch ex As Exception
                        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                ElseIf txtNoDocumentoPiloto.Text.Length >= 13 Then 'Búsqueda por DPI o Licencia

                    ProcesoPiloto = Procesar_Piloto(False)

                    Dim vFechaVence As Date = dtpFechaVencePiloto.EditValue
                    Dim vDiasVencimiento As Integer = DateDiff(DateInterval.Day, Now, vFechaVence)

                    lblStatusLicencia.AppendText("Días al vencimiento: " & vDiasVencimiento)
                    lblStatusLicencia.AppendText(vbNewLine)
                    lblStatusLicencia.SelectionStart = lblStatusLicencia.TextLength
                    lblStatusLicencia.ScrollToCaret()
                    lblStatusLicencia.Refresh()

                    If vDiasVencimiento < 0 Then
                        lblStatusLicencia.BackColor = Color.LightCoral
                    Else
                        lblStatusLicencia.BackColor = Color.LightGreen
                    End If

                    Dim BeTicketUltVisita As New clsBeTms_ticket
                    BeTicketUltVisita = clsLnTms_ticket.Get_Ultima_Visita_By_IdPiloto(BePiloto.IdPiloto)

                    If Not BeTicketUltVisita Is Nothing Then
                        lblStatusLicencia.AppendText("Ult. Visita: " & BeTicketUltVisita.Fecha_Ingreso)
                    Else
                        lblStatusLicencia.AppendText("El piloto nos visita por primera vez, una cordial bienvenida" & vbNewLine)
                    End If

                    lblStatusLicencia.AppendText(vbNewLine)
                    lblStatusLicencia.SelectionStart = lblStatusLicencia.TextLength
                    lblStatusLicencia.ScrollToCaret()
                    lblStatusLicencia.Refresh()

                End If

            End If

        End If

    End Sub


    Private Function Procesar_Piloto(ByVal EscaneoDocumento As Boolean) As Boolean

        Procesar_Piloto = False

        Try

            If txtNombresPiloto.Text.Trim = "" Then
                XtraMessageBox.Show("Ingrese nombre de piloto", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtNombresPiloto.Focus()
                Exit Function
            End If

            If txtApellidosPiloto.Text.Trim = "" Then
                XtraMessageBox.Show("Ingrese apellidos de piloto", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtApellidosPiloto.Focus()
                Exit Function
            End If

            BePiloto.No_dpi = txtNoDocumentoPiloto.Text.Trim()
            BePiloto.No_Licencia = txtNoDocumentoPiloto.Text.Trim()
            BePiloto.EscaneoDocumento = EscaneoDocumento
            BePiloto.Nombres = txtNombresPiloto.Text.Trim()
            BePiloto.Apellidos = txtApellidosPiloto.Text.Trim()
            BePiloto.IdEmpresaTransporte = cmbEmpresaTransporte.EditValue
            BePiloto.Activo = True
            BePiloto.Fecha_expiracion_licencia = dtpFechaVencePiloto.EditValue
            BePiloto.Fec_agr = Now
            BePiloto.Fec_mod = Now
            BePiloto.User_agr = AP.UsuarioAp.IdUsuario
            BePiloto.User_mod = AP.UsuarioAp.IdUsuario
            BePiloto.Codigo_barra = txtNoDocumentoPiloto.Text
            BePiloto.IdTipoLicencia = txtTipoLicencia.Text.Trim()
            BePiloto.IsNew = True

            If clsLnEmpresa_transporte_pilotos.Existe_No_Licencia(txtNoDocumentoPiloto.Text) Then
                BePiloto = clsLnEmpresa_transporte_pilotos.Get_By_No_Documento(txtNoDocumentoPiloto.Text)
                BePiloto.IsNew = False
            End If

            Procesar_Piloto = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblFechaIngreso.Text = Now
    End Sub

    Private Sub frmTicketN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            mnuActualizar.Enabled = False
            mnuEliminar.Enabled = False

            AP.Listar_BodegasLogin(cmbBodega)
            Listar_Propietarios()
            Listar_Tipo_Contenedor()
            Listar_Empresas_Transporte()

            mnuIncidenciaPiloto.Enabled = False
            mnuIncidenciaTransporte.Enabled = False
            mnuHistorialPiloto.Enabled = False
            mnuHistorialTransporte.Enabled = False
            mnuInspeccionHH.Enabled = False

            dtpFechaVencePiloto.EditValue = Now

            If Modo = ModoTrans.Nuevo Then

                Dim vNuevoTicketNumero As String = Strings.Right("000000000" & clsLnTms_ticket.MaxID() + 1, 9)
                txtNoTicket.Text = vNuevoTicketNumero

                mnuImprimir.Enabled = False
                mnuGuardar.Enabled = True

            Else

                Cargar_Datos()

                mnuImprimir.Enabled = True
                mnuGuardar.Enabled = False

            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)


            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Public BeTicket As New clsBeTms_ticket

    Private Sub Cargar_Datos()

        Try

            txtNoTicket.Text = Strings.Right("000000000" & BeTicket.IdTicket, 9)
            cmbPropietario.EditValue = BeTicket.IdPropietario
            cmbBodega.EditValue = BeTicket.IdUbicacionDestino
            cmbEmpresaTransporte.EditValue = BeTicket.IdEmpresaTransporte
            txtNoPoliza.Text = BeTicket.No_Poliza
            chkOperacion.EditValue = IIf(BeTicket.Tipo_Operacion = "Carga", True, False)
            txtNoPlaca.Text = BeTicket.No_Placa
            txtNoDocumentoPiloto.Text = BeTicket.No_Documento_Piloto
            txtTipoLicencia.Text = BeTicket.Tipo_Documento_Piloto
            txtNombresPiloto.Text = BeTicket.Nombres_Piloto
            txtApellidosPiloto.Text = BeTicket.Apellidos_Piloto
            txtNoTC.Text = BeTicket.No_TC

            Dim BePiloto As New clsBeEmpresa_transporte_pilotos
            BePiloto = clsLnEmpresa_transporte_pilotos.Get_By_IdPiloto(BeTicket.IdPiloto)

            If Not BePiloto Is Nothing Then
                dtpFechaVencePiloto.EditValue = BePiloto.Fecha_expiracion_licencia
            End If

            Dim vFechaVence As Date = dtpFechaVencePiloto.EditValue
            Dim vDiasVencimiento As Integer = DateDiff(DateInterval.Day, Now, vFechaVence)

            lblStatusLicencia.AppendText("Días al vencimiento: " & vDiasVencimiento)
            lblStatusLicencia.AppendText(vbNewLine)
            lblStatusLicencia.SelectionStart = lblStatusLicencia.TextLength
            lblStatusLicencia.ScrollToCaret()
            lblStatusLicencia.Refresh()

            If vDiasVencimiento < 0 Then
                lblStatusLicencia.BackColor = Color.LightCoral
            Else
                lblStatusLicencia.BackColor = Color.LightGreen
            End If

            Dim BeTicketUltVisita As New clsBeTms_ticket
            BeTicketUltVisita = clsLnTms_ticket.Get_Ultima_Visita_By_IdPiloto(BePiloto.IdPiloto)

            If Not BeTicketUltVisita Is Nothing Then
                lblStatusLicencia.AppendText("Ult. Visita: " & BeTicketUltVisita.Fecha_Ingreso)
            Else
                lblStatusLicencia.AppendText("El piloto nos visita por primera vez, una cordial bienvenida" & vbNewLine)
            End If

            lblStatusLicencia.AppendText(vbNewLine)
            lblStatusLicencia.SelectionStart = lblStatusLicencia.TextLength
            lblStatusLicencia.ScrollToCaret()
            lblStatusLicencia.Refresh()


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Propietarios()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_By_Empresa_For_Combo(AP.IdEmpresa)

            cmbPropietario.Properties.DataSource = DT1
            cmbPropietario.Properties.ValueMember = "IdPropietario"
            cmbPropietario.Properties.DisplayMember = "Nombre"
            cmbPropietario.Properties.PopupWidth = 700
            cmbPropietario.Properties.BestFit()
            cmbPropietario.Properties.PopulateColumns()

            If cmbPropietario.Properties.Columns.Count > 0 Then
                cmbPropietario.Properties.Columns(0).Visible = False
                cmbPropietario.Properties.Columns(1).Visible = False
                cmbPropietario.ItemIndex = 0
            End If

            cmbPropietario.Properties.NullText = ""

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Tipo_Contenedor()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnTipo_contenedor.Get_All_For_Combo(True)

            cmbTipoTransporte.Properties.DataSource = DT1
            cmbTipoTransporte.Properties.ValueMember = "IdTipoContenedor"
            cmbTipoTransporte.Properties.DisplayMember = "Nombre"
            'cmbTipoTransporte.Properties.PopupWidth = 700
            cmbTipoTransporte.Properties.BestFit()
            cmbTipoTransporte.Properties.PopulateColumns()

            If cmbTipoTransporte.Properties.Columns.Count > 0 Then
                cmbTipoTransporte.Properties.Columns(0).Visible = False
                cmbTipoTransporte.ItemIndex = 0
            End If

            cmbTipoTransporte.Properties.NullText = ""

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Empresas_Transporte()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnEmpresa_transporte.Get_All_For_Combo(AP.IdEmpresa)

            cmbEmpresaTransporte.Properties.DataSource = DT1
            cmbEmpresaTransporte.Properties.ValueMember = "IdEmpresaTransporte"
            cmbEmpresaTransporte.Properties.DisplayMember = "Nombre"
            'cmbTipoTransporte.Properties.PopupWidth = 700
            cmbEmpresaTransporte.Properties.BestFit()
            cmbEmpresaTransporte.Properties.PopulateColumns()

            If cmbEmpresaTransporte.Properties.Columns.Count > 0 Then
                cmbEmpresaTransporte.Properties.Columns(0).Visible = False
                cmbEmpresaTransporte.ItemIndex = 0
            End If

            cmbEmpresaTransporte.Properties.NullText = ""

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            mnuGuardar.Enabled = False

            If ProcesoPiloto OrElse Procesar_Piloto(False) Then

                If Procesar_Vehiculo() Then

                    Dim BeTicket As New clsBeTms_ticket
                    BeTicket.Fecha_Ingreso = Now
                    BeTicket.Fecha_Salida = New Date(1900, 1, 1)
                    BeTicket.IdEmpresa = AP.IdEmpresa
                    BeTicket.IdPropietario = cmbPropietario.EditValue
                    BeTicket.IdUbicacionDestino = cmbBodega.EditValue
                    BeTicket.IdEmpresaTransporte = cmbEmpresaTransporte.EditValue
                    BeTicket.No_Poliza = txtNoPoliza.Text
                    BeTicket.Tipo_Operacion = IIf(chkOperacion.EditValue, "Carga", "Descarga")
                    BeTicket.IdPiloto = BePiloto.IdPiloto
                    BeTicket.IdVehiculo = BeVehiculo.IdVehiculo
                    BeTicket.Estado = "Abierto"
                    BeTicket.No_Placa = txtNoPlaca.Text.Trim()
                    BeTicket.No_Documento_Piloto = txtNoDocumentoPiloto.Text.Trim()
                    BeTicket.Tipo_Documento_Piloto = txtTipoLicencia.Text.Trim()
                    BeTicket.Nombres_Piloto = txtNombresPiloto.Text.Trim()
                    BeTicket.Apellidos_Piloto = txtApellidosPiloto.Text.Trim()
                    BeTicket.No_TC = txtNoTC.Text.Trim()

                    If XtraMessageBox.Show("¿Guardar ticket?", "TMS.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        If clsLnTms_ticket.Guardar_Ticket(BePiloto, BeVehiculo, BeTicket, BeTmsTicketPol) Then

                            XtraMessageBox.Show("Se guardó el ticket", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Imprimir_Ticket()

                            Close()

                        End If

                    End If

                Else

                    XtraMessageBox.Show("Falta datos del vehiculo/póliza", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
            Else

                XtraMessageBox.Show("Falta datos del piloto", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            mnuGuardar.Enabled = True

        Catch ex As Exception
            mnuGuardar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Imprimir_Ticket()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            If Nombre_Impresora = "" Then

                Dim pd As PrintDialog = New PrintDialog()
                pd.PrinterSettings = New PrinterSettings()
                Nombre_Impresora = pd.PrinterSettings.PrinterName

                If txtNoTicket.Text.Trim() <> "" Then
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Imprimiendo ticket a impresora default: " & Nombre_Impresora, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Imprimir_Etiqueta(txtNoTicket.Text.Trim(), Nombre_Impresora)
                End If

            Else

                If txtNoTicket.Text.Trim() <> "" Then
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Imprimiendo ticket", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Imprimir_Etiqueta(txtNoTicket.Text.Trim(), Nombre_Impresora)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function Procesar_Vehiculo() As Boolean

        Procesar_Vehiculo = False

        Try


            If txtNoPlaca.Text.Trim = "" Then
                XtraMessageBox.Show("Ingrese número de placa", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtNoPlaca.Focus()
                Exit Function
            End If

            'GT 26012021 valida DUCA, 
            'GT 10022021 Poliza no es obligatoria en TMS
            If txtNoPoliza.Text.Trim = "" Then
                BeTmsTicketPol = Nothing
            Else

                'GT 26012021 se asigna la cadena DUCA a la clase TMS
                BeTmsTicketPol.NoPoliza = enc_duca.Codigo_Poliza
                BeTmsTicketPol.Dua = enc_duca.Numero_DUCA
                BeTmsTicketPol.Fecha_poliza = enc_duca.Fecha_Aceptacion
                BeTmsTicketPol.Pais_procede = enc_duca.Pais_procedencia
                BeTmsTicketPol.Tipo_cambio = enc_duca.Tipo_cambio
                BeTmsTicketPol.Total_valoraduana = enc_duca.Total_valor_aduana
                BeTmsTicketPol.Total_usd = enc_duca.TotalFOBUSD
                BeTmsTicketPol.Total_flete = enc_duca.Total_Flete_USD
                BeTmsTicketPol.Total_seguro = enc_duca.Total_Seguro_USD
                BeTmsTicketPol.User_agr = AP.UsuarioAp.IdUsuario
                BeTmsTicketPol.Fec_agr = Now
                BeTmsTicketPol.User_mod = AP.UsuarioAp.IdUsuario
                BeTmsTicketPol.Fec_mod = Now
                BeTmsTicketPol.Clave_aduana = enc_duca.Clave_aduana_despacho_destino
                BeTmsTicketPol.Nit_imp_exp = enc_duca.NIT_Importador
                BeTmsTicketPol.Dua = enc_duca.Numero_Orden
                BeTmsTicketPol.Clase = enc_duca.Clase
                BeTmsTicketPol.Mod_transporte = enc_duca.Modo_transporte
                BeTmsTicketPol.Total_liquidar = enc_duca.Total_Liquidar
                BeTmsTicketPol.Total_general = enc_duca.Total_General
                BeTmsTicketPol.IdRegimen = Convert.ToInt32(enc_duca.Regimen)

            End If


            BeVehiculo.Placa = txtNoPlaca.Text.Trim()
            BeVehiculo.Marca = "NA"
            BeVehiculo.Modelo = "NA"
            BeVehiculo.Activo = True
            BeVehiculo.Alto = 0
            BeVehiculo.Ancho = 0
            BeVehiculo.Alto = 0
            BeVehiculo.Volumen = 0
            BeVehiculo.Es_contedor = False
            BeVehiculo.IdTipoContenedor = 1
            BeVehiculo.Placa_comercial = txtNoTC.Text.Trim()
            BeVehiculo.User_agr = AP.UsuarioAp.IdUsuario
            BeVehiculo.Fec_agr = Now
            BeVehiculo.User_mod = AP.UsuarioAp.IdUsuario
            BeVehiculo.IdEmpresaTransporte = cmbEmpresaTransporte.EditValue
            BeVehiculo.Fec_mod = Now
            Procesar_Vehiculo = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Imprimir_Etiqueta(ByVal pNoTicket As String, ByVal PrinterName As String)

        Try

            'Dim ZPLString As String = String.Format("^XA " &
            '                                        "^MMT " &
            '                                        "^PW700 " &
            '                                        "^LL0406 " &
            '                                        "^LS0 " &
            '                                        "^FT171,61^A0I,25,14^FH\^FD{0}^FS " &
            '                                        "^FT550,61^A0I,25,14^FH\^FD{1}^FS " &
            '                                        "^FT670,306^A0I,25,14^FH\^FD{2}^FS " &
            '                                        "^FT292,61^A0I,25,24^FH\^FDBodega:^FS " &
            '                                        "^FT670,61^A0I,25,24^FH\^FDEmpresa:^FS " &
            '                                        "^FT670,367^A0I,25,24^FH\^FDTOM, WMS. TMS TICKET^FS " &
            '                                        "^FO2,340^GB670,0,14^FS " &
            '                                        "^BY3,3,160^FT670,131^BCI,,Y,N " &
            '                                        "^FD{3}^FS " &
            '                                        "^PQ1,0,1,Y " &
            '                                        "^XZ", AP.NomBodega,
            '                                        AP.NomEmpresa,
            '                                        pNoTicket,
            '                                        pNoTicket)

            Dim ZPLString As String = String.Format("^XA
                                                    ^CI28
                                                    ^CF0,15
                                                    ^FO20,20^FDTOMWMS, TMS TICKET^FS
                                                    ^FO2,35^GB400,5,5^FS 
                                                    ^BY2,2,100
                                                    ^FT20,150^BCN,,Y,N ^FD{1}^FS 
                                                    ^A0N,18,18
                                                    ^FT20,190^FDEMPRESA:{0}^FS
                                                    ^A0N,18,18
                                                    ^FT220,190^FD{2}^FS
                                                    ^XZ",
                                                    AP.NomEmpresa,
                                                    pNoTicket,
                                                    Now)



            If Not PrinterName Is Nothing Then
                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
            Else
                Throw New Exception("No hay impresora para enviar archivo raw!")
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

    Private Sub lcPoliza_Click(sender As Object, e As EventArgs) Handles lcPoliza.Click
        Escan_Poliza()
    End Sub

    Private Sub Escan_Poliza()

        Try

            Dim barra_poliza As String = txtNoPoliza.Text

            If String.IsNullOrEmpty(barra_poliza) Then
                XtraMessageBox.Show("No hay póliza para leer", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                txtNoPoliza.Focus()

            Else

                'GT04022022: Se agrega trim y UpperCase a DUCA y a Regimen
                Dim Fecha_string = barra_poliza.Substring(30, 8)
                enc_duca.Numero_Orden = barra_poliza.Substring(0, 10)
                enc_duca.Numero_DUCA = barra_poliza.Substring(10, 20).ToUpper()
                enc_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                enc_duca.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                'GT 170820211745: se convierte a mayuscula el regimen.
                Dim upper_regimen As String = barra_poliza.Substring(70, 5)
                enc_duca.Regimen = upper_regimen.Trim.ToUpper()
                enc_duca.Clase = barra_poliza.Substring(75, 3).Trim()
                enc_duca.Pais_procedencia = barra_poliza.Substring(78, 2)
                enc_duca.Modo_transporte = barra_poliza.Substring(80, 1)
                enc_duca.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(81, 7))
                enc_duca.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(88, 16))
                enc_duca.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(104, 15))
                enc_duca.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(119, 16))
                enc_duca.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(135, 15))
                enc_duca.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(150, 15))
                enc_duca.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(165, 15))
                enc_duca.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(180, 15))
                enc_duca.Total_General = Convert.ToDouble(barra_poliza.Substring(195, 15))
                enc_duca.Codigo_Poliza = barra_poliza.Substring(210, 9).ToUpper()

                'concatenación para fecha dd/mm/yyyy
                Dim comodin As String = "/"
                Dim dd As String
                Dim mm As String
                Dim anio As String

                dd = Fecha_string.Substring(0, 2)
                mm = Fecha_string.Substring(2, 2)
                anio = Fecha_string.Substring(4, 4)
                enc_duca.Fecha_Aceptacion = dd & comodin & mm & comodin & anio


                Try
                    'GT 14022021 convierte codigo_regimen a Idregimen
                    Dim getRegimen As New clsBeRegimen_fiscal
                    getRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(enc_duca.Regimen)

                    '#EJC20210222: Validar que no sea nothing
                    If Not getRegimen Is Nothing Then
                        enc_duca.Regimen = getRegimen.IdRegimen
                    Else
                        Throw New Exception("El regimen: " & enc_duca.Regimen & " no existe en el mantenimiento.")
                    End If

                    '#EJC20210222: Conservar la barra leída.
                    BeTmsTicketPol.Codigo_Barra = txtNoPoliza.Text

                    txtNoPoliza.Text = ""
                    txtNoPoliza.Text = enc_duca.Numero_DUCA

                    txtNoTC.Focus()
                Catch ex As Exception
                    Throw New Exception("Error_08022022_1600: La póliza no es legible, no tiene espacios o campos con tamaño requerido.")
                End Try

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtNoPlaca_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNoPlaca.KeyDown

        If e.KeyCode = Keys.Enter Then
            If Not txtNoPlaca.Text.Trim = "" Then
                validar_placa()
            End If
        End If

    End Sub

    Private Sub txtNoPlaca_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtNoPlaca.PreviewKeyDown
        If e.KeyData = Keys.Tab Then

            validar_placa()
            'txtNoPoliza.Focus()

        End If
    End Sub

    Private Sub validar_placa()

        Try
            If txtNoPlaca.Text.Trim <> "" Then

                '#EJC20210520: Para que hacerlo fácil, si lo podemos hacer difícil.
                'Dim valPlaca As Regex = New Regex("^\w{1,3}\d{3,3}\w{3,3}$")
                'GT04022022: esta expresión valida 3 numeros y 3 caracteres para la placa
                Dim valPlaca As Regex = New Regex("^[0-9][0-9][0-9][A-Za-z][A-Za-z][A-Za-z]$")
                Dim vPlacaValida As Match = valPlaca.Match(txtNoPlaca.Text)

                If Not vPlacaValida.Success Then
                    XtraMessageBox.Show("El formato de placa debe coincider con: [123abc], si lo es, reporte a soporte.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtNoPlaca.Focus()
                Else
                    txtNoPoliza.Focus()
                End If

            Else
                XtraMessageBox.Show("Ingrese placa del vehiculo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNoPlaca.Focus()
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub txtNoPoliza_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNoPoliza.KeyDown

        If e.KeyCode = Keys.Enter Then
            If txtNoPoliza.Text.Trim <> "" Then
                Escan_Poliza()
            End If
        End If

    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick

        Try

            If XtraMessageBox.Show("¿Imprimir ticket?", "TMS.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Imprimir_Ticket()
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

    Private Sub dtpFechaVencePiloto_EditValueChanged(sender As Object, e As EventArgs) Handles dtpFechaVencePiloto.EditValueChanged

        Try

            Dim vFechaVence As Date = dtpFechaVencePiloto.EditValue
            Dim vDiasVencimiento As Integer = DateDiff(DateInterval.Day, Now, vFechaVence)

            lblStatusLicencia.Text = ""
            lblStatusLicencia.AppendText("Días al vencimiento: " & vDiasVencimiento)
            lblStatusLicencia.AppendText(vbNewLine)
            lblStatusLicencia.SelectionStart = lblStatusLicencia.TextLength
            lblStatusLicencia.ScrollToCaret()
            lblStatusLicencia.Refresh()

            If vDiasVencimiento < 0 Then
                lblStatusLicencia.BackColor = Color.LightCoral
            Else
                lblStatusLicencia.BackColor = Color.LightGreen
            End If

        Catch ex As Exception

        End Try

    End Sub

End Class