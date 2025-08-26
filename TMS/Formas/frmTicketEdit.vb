Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmTicketEdit
    Public Property BeTicket As New clsBeTms_ticket
    Public Property Modo As ModoTrans
    Private Nombre_Impresora As String = ""

    Public Enum ModoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Private Sub frmTicketEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Dim vNuevoTicketNumero As String = Strings.Right("000000000" & BeTicket.IdTicket, 9)
            txtNoTicket.Text = vNuevoTicketNumero

            dtpFechaIngreso.EditValue = BeTicket.Fecha_Ingreso
            dtpNuevaFechaIngreso.EditValue = Now

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub


    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

        Try

            If XtraMessageBox.Show("¿Actualizar ticket?", "TMS.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then


                Dim vNuevaFecha As DateTime = Now
                Dim vFechaOriginal As DateTime

                Try
                    vNuevaFecha = dtpNuevaFechaIngreso.EditValue.DateTime
                Catch ex As Exception
                    vNuevaFecha = dtpNuevaFechaIngreso.EditValue
                End Try

                'GT03082022: guardar fecha original primero, despues del update se pierde
                vFechaOriginal = BeTicket.Fecha_Ingreso
                BeTicket.Fecha_Ingreso = vNuevaFecha

                Dim vResult As Integer = clsLnTms_ticket.Actualizar_Fecha_Ingreso_Tms_Ticket(BeTicket)

                clsLnLog_error_wms.Agregar_Error("TMS: Se actualizó el ticket: " & BeTicket.IdTicket & " Fecha_Original: " & vFechaOriginal & " Nueva_Fecha: " & vNuevaFecha)

                If vResult > 0 Then

                    dtpNuevaFechaIngreso.Enabled = False

                    If XtraMessageBox.Show("Ticket actualizado ¿Reimprimir?", "TMS.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        If Imprimir_Ticket() Then
                            Close()
                        End If
                    End If

                    '#EJC20220803: No cerrar por si quieren reimprimir el ticket.
                    'Close()

                End If

            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuReimprimirTicket_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReimprimirTicket.ItemClick

        If XtraMessageBox.Show("¿Reimprimir Ticket?", "TMS.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            If Imprimir_Ticket() Then
                Close()
            End If
        End If

    End Sub

    Private Function Imprimir_Ticket() As Boolean

        Imprimir_Ticket = False

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
                    Imprimir_Ticket = True
                End If

            Else

                If txtNoTicket.Text.Trim() <> "" Then
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Imprimiendo ticket", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Imprimir_Etiqueta(txtNoTicket.Text.Trim(), Nombre_Impresora)
                    Imprimir_Ticket = True
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function


    Private Sub Imprimir_Etiqueta(ByVal pNoTicket As String, ByVal PrinterName As String)

        Try

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

End Class