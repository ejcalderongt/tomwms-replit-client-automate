Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmStockResJornadaMerca


    Private IsLoading As Boolean = False
    Private fecha_inicia As Date
    Private fecha_final As Date
    Private prima As Decimal
    Private emision As Decimal
    Private decreto As Decimal
    Private redondeo As Integer
    Private DT As New DataTable
    Private DT2 As New DataTable("Mercaderia")
    'Private DT3 As New DataTable("Resumen")
    Public Property Modo As pModo
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum


    Private Sub SetDatataTable2()

        Try

            DT2.Columns.Add("codigobodega", GetType(String))
            DT2.Columns.Add("codigomercaderia", GetType(String))
            DT2.Columns.Add("certificadodeposito", GetType(String))
            DT2.Columns.Add("bonoprenda", GetType(String))
            DT2.Columns.Add("saldosincertificado", GetType(String))
            DT2.Columns.Add("saldocertificado", GetType(String))
            DT2.Columns.Add("saldobono", GetType(String))
            DT2.Columns.Add("nombreacreedor", GetType(String))
            DT2.Columns.Add("descripcionmercaderia", GetType(String))
            DT2.Columns.Add("fechaemisioncertificado", GetType(String))
            DT2.Columns.Add("fechavencimientocertificado", GetType(String))
            DT2.Columns.Add("fechaemisionbono", GetType(String))
            DT2.Columns.Add("fechavencimientobono", GetType(String))
            DT2.Columns.Add("fechaemisionds", GetType(String))
            DT2.Columns.Add("fechavencimientods", GetType(String))
            DT2.Columns.Add("cif", GetType(String))
            DT2.Columns.Add("impuestos", GetType(String))
            DT2.Columns.Add("seguros", GetType(String))
            DT2.Columns.Add("seguros2", GetType(String))
            DT2.Columns.Add("primerapellido", GetType(String))
            DT2.Columns.Add("segundoapellido", GetType(String))
            DT2.Columns.Add("apellidocasada", GetType(String))
            DT2.Columns.Add("primernombre", GetType(String))
            DT2.Columns.Add("segundonombre", GetType(String))
            DT2.Columns.Add("tercernombre", GetType(String))
            DT2.Columns.Add("razonsocial", GetType(String))
            DT2.Columns.Add("nombrecomercial", GetType(String))
            DT2.Columns.Add("terminacion", GetType(String))



        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub


    Private Sub frmStockResJornadaMerca_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IMS.ListarRegimen(cmbRegimen)
            SetDatataTable2()

            deMesReporte.EditValue = Date.Now


            'GT 05102021: se obtiene fecha inicial y final del mes por defecto
            Dim Fecha_ As Date = deMesReporte.EditValue
            fecha_inicia = DateSerial(Fecha_.Year, Fecha_.Month, 1)
            fecha_final = DateSerial(Fecha_.Year, Fecha_.Month + 1, 0)

            'Dim ci As CultureInfo = New CultureInfo("en-US")
            'ci.NumberFormat.NumberDecimalSeparator = " "
            'ci.NumberFormat.NumberGroupSeparator = ""

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

    End Sub

    Private Sub btnGenerar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGenerar.ItemClick

        Generar_Reporte()

    End Sub


    Private Sub Generar_Reporte()

        Dim DT As New DataTable

        dgridDetalle.DataSource = Nothing

        DT.Clear()

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If

            DT = clsLnReportesFiscales.Get_Stock_res_jornada_merca(fecha_inicia, fecha_final, cmbRegimen.EditValue)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    DT2.Clear()

                    For Each dr As DataRow In DT.Rows

                        Dim vcodigobodega = dr.Item("codigobodega")
                        Dim vcodigomercaderia = dr.Item("codigomercaderia")
                        Dim vcertificadodeposito = dr.Item("certificadodeposito")
                        Dim vbonoprenda = dr.Item("bonoprenda")
                        Dim vSaldosincertificado = dr.Item("saldosincertificado")
                        Dim vSaldocertificado = dr.Item("saldocertificado")
                        Dim vsaldobono = dr.Item("saldobono")
                        Dim vnombreacreedor = dr.Item("nombreacreedor")
                        Dim vdescripcionmercaderia = dr.Item("descripcionmercaderia")
                        Dim vfechaemisioncertificado = dr.Item("fechaemisioncertificado")
                        Dim vfechavencimientocertificado = dr.Item("fechavencimientocertificado")
                        Dim vfechaemisionbono = dr.Item("fechaemisionbono")
                        Dim vfechavencimientobono = dr.Item("fechavencimientobono")
                        Dim vfechaemisionds = dr.Item("fechaemisionds")
                        Dim vfechavencimientods = dr.Item("fechavencimientods")
                        Dim vCif = dr.Item("cif")
                        Dim vImpuestos = dr.Item("impuestos")
                        Dim vSeguros = dr.Item("seguros")
                        Dim vSeguros2 = dr.Item("seguros2")
                        Dim vprimerapellido = dr.Item("primerapellido")
                        Dim vsegundoapellido = dr.Item("segundoapellido")
                        Dim vapellidocasada = dr.Item("apellidocasada")
                        Dim vprimernombre = dr.Item("primernombre")
                        Dim vsegundonombre = dr.Item("segundonombre")
                        Dim vtercernombre = dr.Item("tercernombre")
                        Dim vrazonsocial = dr.Item("razonsocial")
                        Dim vnombrecomercial = dr.Item("nombrecomercial")
                        Dim vterminacion = dr.Item("terminacion")
                        Dim Saldosincertificado_ = convertidor_sin_decimales(vSaldosincertificado)
                        Dim Saldocertificado_ = convertidor_sin_decimales(vSaldocertificado)
                        Dim Cif_ = convertidor_sin_decimales(vCif)
                        Dim Impuestos_ = convertidor_sin_decimales(vImpuestos)
                        Dim Seguros_ = convertidor_sin_decimales(vSeguros)
                        Dim Seguros2_ = convertidor_sin_decimales(vSeguros2)

                        DT2.Rows.Add(vcodigobodega, vcodigomercaderia, vcertificadodeposito, vbonoprenda, Saldosincertificado_,
                                     Saldocertificado_, vsaldobono, vnombreacreedor, vdescripcionmercaderia, vfechaemisioncertificado,
                                     vfechavencimientocertificado, vfechaemisionbono, vfechavencimientobono, vfechaemisionds, vfechavencimientods,
                                     Cif_, Impuestos_, Seguros_, Seguros2_, vprimerapellido, vsegundoapellido, vapellidocasada, vprimernombre, vsegundonombre, vtercernombre,
                                     vrazonsocial, vnombrecomercial, vterminacion)

                    Next

                    dgridDetalle.DataSource = DT2
                    GridView1.OptionsView.ShowFooter = True
                    GridView1.BestFitColumns(True)

                    lblRegs.Caption = "Registros: " & DT.Rows.Count

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

        Application.DoEvents()

    End Sub

    Private Sub btnImport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnImport.ItemClick
        Exportar_Grid_A_Excel(dgridDetalle, "WMS_ValorizacionStockJornadaMercaderia.xlsx")
    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub btnSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSalir.ItemClick
        Close()
    End Sub

    Function convertidor_sin_decimales(ByVal vValor As String)

        Dim integer_part As String
        Dim decimal_part As String
        Dim full_part As String = ""


        If vValor > 0 Then

            integer_part = Split(vValor, ".")(0)
            decimal_part = Split(vValor, ".")(1)
            full_part = integer_part + decimal_part

        End If

        Return full_part

    End Function

End Class