Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmPrintTicket

    Private Nombre_Impresora As String = ""

    Private Sub btnImprimir_Click_1(sender As Object, e As EventArgs) Handles btnImprimir.Click
        btn_Imprimir_Ticket()
    End Sub

    Private Sub setAPP()

        Try

            lblServerAPP.Caption += " " & clsBD.Instancia.Server
            lblBD.Caption += " " & clsBD.Instancia.NombreBD
            'lblEmpresa.Caption += " " & AP.NomEmpresa
            'lblBodega.Caption += " " & AP.NomBodega
            'bbiCambiaBodega.Caption = lblBodega.Caption

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub btn_Imprimir_Ticket()

        Try


            Dim BeTicket As New clsBeTms_ticket
            BeTicket.Fecha_Ingreso = Now
            BeTicket.Fecha_Salida = New Date(1900, 1, 1)
            BeTicket.IdEmpresa = AP.IdEmpresa
            'BeTicket.IdPropietario = 
            BeTicket.IdUbicacionDestino = 0
            'BeTicket.IdEmpresaTransporte = 1
            BeTicket.No_Poliza = ""
            BeTicket.Tipo_Operacion = "Descarga"
            'BeTicket.IdPiloto = 1
            'BeTicket.IdVehiculo = 5
            BeTicket.Estado = "Abierto"
            BeTicket.No_Placa = ""
            BeTicket.No_Documento_Piloto = ""
            BeTicket.Tipo_Documento_Piloto = ""
            BeTicket.Nombres_Piloto = ""
            BeTicket.Apellidos_Piloto = ""
            BeTicket.No_TC = ""

            If clsLnTms_ticket.Guardar_Ticket(BeTicket) Then

                Imprimir_Ticket()
                Nuevo_Ticket()

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

    Private Sub Imprimir_Ticket()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            If Nombre_Impresora = "" Then

                Dim pd As PrintDialog = New PrintDialog()
                pd.PrinterSettings = New PrinterSettings()
                Nombre_Impresora = pd.PrinterSettings.PrinterName

                If txtNoTicket.Text.Trim() <> "" Then
                    SplashScreenManager.CloseForm(False)
                    'XtraMessageBox.Show("Imprimiendo ticket a impresora default: " & Nombre_Impresora, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Imprimir_Etiqueta(txtNoTicket.Text.Trim(), Nombre_Impresora)
                End If

            Else

                If txtNoTicket.Text.Trim() <> "" Then
                    SplashScreenManager.CloseForm(False)
                    'XtraMessageBox.Show("Imprimiendo ticket", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Imprimir_Etiqueta(txtNoTicket.Text.Trim(), Nombre_Impresora)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

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
                                                    ^CF0,13
                                                    ^FO10,20^FDTOMWMS, TMS TICKET^FS
                                                    ^FO2,35^GB290,5,5^FS 
                                                    ^BY2,2,70
                                                    ^FT30,130^BCN,,Y,N ^FD{1}^FS 
                                                    ^A0N,18,18
                                                    ^FT10,180^FDEMPRESA:{0}^FS
                                                    ^A0N,18,18
                                                    ^FT150,180^FD{2}^FS
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


    Private Sub Nuevo_Ticket()
        Dim vNuevoTicketNumero As String = Strings.Right("000000000" & clsLnTms_ticket.MaxID() + 1, 9)
        txtNoTicket.Text = vNuevoTicketNumero

    End Sub

    Private Sub frmPrintTicket_Load(sender As Object, e As EventArgs) Handles Me.Load
        Nuevo_Ticket()
        setAPP()
    End Sub

    Private Sub mnuListaTickets_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuListaTickets.ItemClick

        Try
            Dim MenuP As New frmPrincipal()
            MenuP.lblUsuario.Caption = AP.UsuarioAp.Nombres
            MenuP.ShowDialog()
            MenuP.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub frmPrintTicket_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown


        Try

            If e.Control AndAlso e.KeyCode = Keys.P Then
                btn_Imprimir_Ticket()
            End If

        Catch ex As Exception

        End Try

    End Sub

End Class