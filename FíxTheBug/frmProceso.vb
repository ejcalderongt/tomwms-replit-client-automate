Imports System.Data.Common
Imports System.Data.Linq.SqlClient
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Security
Imports System.Windows.Forms
Imports DevExpress.Drawing
Imports DevExpress.XtraEditors
Imports TOMWMS

Public Class frmProceso

    Private Sub Asociar_Polizas_Excel_CEALSA()


        Dim clsTransaccion As New clsTransaccion()

        Try

            mnuAsociarPolizasExcelCEALSA.Enabled = False

            clsTransaccion.Begin_Transaction()

            If XtraMessageBox.Show("Inicializar tablas?",
                                    Text,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) = DialogResult.Yes Then

                lblprg.AppendText("Inicializando tablas: " & Now)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                clsLnTablas_relacionadas.Inicializar_Tablas(clsTransaccion.lConnection,
                                                            clsTransaccion.lTransaction)

            End If

            lblprg.AppendText("Obteniendo pedidos sin poliza: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim lPedidoSinPolizaWMS As New List(Of clsBeTrans_pe_enc)
            lPedidoSinPolizaWMS = clsLnTrans_pe_enc.Get_All_Pedidos_Sin_Poliza(clsTransaccion.lConnection,
                                                                               clsTransaccion.lTransaction)

            If Not lPedidoSinPolizaWMS Is Nothing Then

                lblprg.AppendText("Se encontraron: " & lPedidoSinPolizaWMS.Count)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            Else

                lblprg.AppendText("No hay pedidos sin póliza.")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End If

            lblprg.AppendText("Get_All_Cealsa: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim lPolizasCealsa As New List(Of clsBeTablas_relacionadas)
            lPolizasCealsa = clsLnTablas_relacionadas.Get_All_Cealsa_No_Utilizadas(clsTransaccion.lConnection,
                                                                                   clsTransaccion.lTransaction)

            lblprg.AppendText("Get_All_WMS: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim lPolizasWMS As New List(Of clsBeTablas_relacionadas)
            lPolizasWMS = clsLnTablas_relacionadas.Get_All_WMS(clsTransaccion.lConnection, clsTransaccion.lTransaction)
            Dim BeSinglePolizaCealsa As New clsBeTablas_relacionadas
            Dim lBeSinglePolizaCealsa As New List(Of clsBeTablas_relacionadas)
            Dim lPedidosVerificados As New List(Of Integer)
            Dim lPedidosSinPoliza As New List(Of Integer)
            Dim vParseString As String = ""
            Dim vRegistrosActualizados As Integer = 0
            Dim vContadorProgress As Integer = 0

            prg.Minimum = 0
            prg.Maximum = lPedidoSinPolizaWMS.Count()
            prg.Value = 0
            prg.Visible = True

            For Each PedidoSinPoliza In lPedidoSinPolizaWMS

                vRegistrosActualizados = 0

                For Each Det In PedidoSinPoliza.Detalle

                    If Det.IdPedidoEnc = 8036 Then
                        Debug.Print(Det.Nombre_producto)
                    End If

                    If Det.Nombre_producto = "BAGUETTE PLUS" AndAlso Det.Cantidad = 1326 Then
                        Debug.Print(Det.Nombre_producto)
                    End If

                    BeSinglePolizaCealsa = lPolizasCealsa.Find(Function(x) x.Fecha_orden_entrega.Date = PedidoSinPoliza.Fecha_Pedido.Date _
                                                               AndAlso (Det.Nombre_producto.StartsWith(x.Descripcion) _
                                                               OrElse Det.Nombre_producto.Contains(x.Descripcion)))

                    If Not BeSinglePolizaCealsa Is Nothing Then

                        If Not lPedidosVerificados.Contains(PedidoSinPoliza.IdPedidoEnc) Then

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("El nùmero de orden: " & BeSinglePolizaCealsa.Correlativo1 & " le corresponde al : " & PedidoSinPoliza.IdPedidoEnc)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            lPedidosVerificados.Add(PedidoSinPoliza.IdPedidoEnc)

                            BeSinglePolizaCealsa.Utilizada = True
                            clsLnTablas_relacionadas.Actualizar_Parcial_CEALSA(BeSinglePolizaCealsa,
                                                                               clsTransaccion.lConnection,
                                                                               clsTransaccion.lTransaction)

                            BeSinglePolizaCealsa.Agente_aduanal = PedidoSinPoliza.IdPedidoEnc
                            BeSinglePolizaCealsa.Utilizada = True
                            vRegistrosActualizados = clsLnTablas_relacionadas.Actualizar_Parcial(BeSinglePolizaCealsa,
                                                                                                 clsTransaccion.lConnection,
                                                                                                 clsTransaccion.lTransaction)

                            If vRegistrosActualizados > 1 Then
                                Debug.WriteLine("espera")
                            End If

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Registros actualizados (" & vRegistrosActualizados & ") nùmero de orden: " & BeSinglePolizaCealsa.Correlativo1 & " IdPedidoEnc: " & PedidoSinPoliza.IdPedidoEnc)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            If BeSinglePolizaCealsa.NoOrdenSalida = 0 Then
                                BeSinglePolizaCealsa.NoOrdenSalida = BeSinglePolizaCealsa.Correlativo1
                            End If

                            PedidoSinPoliza.No_Documento_Externo = BeSinglePolizaCealsa.Copoliza
                            clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(PedidoSinPoliza,
                                                                              clsTransaccion.lConnection,
                                                                              clsTransaccion.lTransaction)
                            lPolizasCealsa.Remove(BeSinglePolizaCealsa)

                            Exit For

                        End If

                    Else

                        Dim BeRelBienRel As New clsBeTablas_relacionadas
                        BeRelBienRel = clsLnTablas_relacionadas.Get_By_Nombre_Similar(Det.Nombre_producto,
                                                                                      clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

                        If Not BeRelBienRel Is Nothing Then

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Coincidencia relativa...")
                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Producto Pedido: " & Det.Nombre_producto & " - Producto CEALSA: " & BeRelBienRel.Descripcion)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            BeSinglePolizaCealsa = lPolizasCealsa.Find(Function(x) x.Fecha_orden_entrega.Date = BeRelBienRel.Fecha_orden_entrega.Date _
                                                                       AndAlso BeRelBienRel.Descripcion.StartsWith(x.Descripcion) _
                                                                       AndAlso x.Copoliza = BeRelBienRel.Copoliza _
                                                                       AndAlso x.Correlativo1 = BeRelBienRel.Correlativo1 _
                                                                       AndAlso x.NoOrdenSalida = BeRelBienRel.NoOrdenSalida)

                            If Not BeSinglePolizaCealsa Is Nothing Then

                                If Not lPedidosVerificados.Contains(PedidoSinPoliza.IdPedidoEnc) Then

                                    lblprg.AppendText(vbNewLine)
                                    lblprg.AppendText("El nùmero de orden: " & BeSinglePolizaCealsa.Correlativo1 & " le corresponde al : " & PedidoSinPoliza.IdPedidoEnc)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                    lPedidosVerificados.Add(PedidoSinPoliza.IdPedidoEnc)

                                    BeSinglePolizaCealsa.Utilizada = True
                                    clsLnTablas_relacionadas.Actualizar_Parcial_CEALSA(BeSinglePolizaCealsa,
                                                                                       clsTransaccion.lConnection,
                                                                                       clsTransaccion.lTransaction)

                                    BeSinglePolizaCealsa.Agente_aduanal = PedidoSinPoliza.IdPedidoEnc
                                    BeSinglePolizaCealsa.Utilizada = True
                                    vRegistrosActualizados = clsLnTablas_relacionadas.Actualizar_Parcial(BeSinglePolizaCealsa,
                                                                                                         clsTransaccion.lConnection,
                                                                                                         clsTransaccion.lTransaction)

                                    If vRegistrosActualizados > 1 Then
                                        Debug.WriteLine("espera")
                                    End If

                                    lblprg.AppendText(vbNewLine)
                                    lblprg.AppendText("Registros actualizados (" & vRegistrosActualizados & ") nùmero de orden: " & BeSinglePolizaCealsa.Correlativo1 & " IdPedidoEnc: " & PedidoSinPoliza.IdPedidoEnc)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                    PedidoSinPoliza.No_Documento_Externo = BeSinglePolizaCealsa.Copoliza
                                    clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(PedidoSinPoliza,
                                                                                      clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)
                                    lPolizasCealsa.Remove(BeSinglePolizaCealsa)

                                    Exit For

                                End If

                            End If

                        Else

                        End If

                        If Not lPedidosSinPoliza.Contains(PedidoSinPoliza.IdPedidoEnc) Then

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("No hay registro de poliza para el IdPedidoEnc: " & PedidoSinPoliza.IdPedidoEnc & " Fecha: " & PedidoSinPoliza.Fecha_Pedido)
                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Producto: " & Det.Nombre_producto)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            lPedidosSinPoliza.Add(PedidoSinPoliza.IdPedidoEnc)

                        End If

                    End If

                    Application.DoEvents()

                Next

                vContadorProgress += 1
                prg.Value = vContadorProgress

                Application.DoEvents()

            Next

            lblprg.AppendText("Equiparando tabla para agrupar: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim vResultEquiparacion As Integer = 0

            vResultEquiparacion = clsLnTablas_relacionadas.Equiparar_Nombres_Agrupacion(clsTransaccion.lConnection,
                                                                                        clsTransaccion.lTransaction)

            lblprg.AppendText("Se homologaron: " & vResultEquiparacion & " registros.")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText("Get_All_Agrupadas: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim lPolizasAgrupadas As New List(Of clsBeTablas_relacionadas)
            lPolizasAgrupadas = clsLnTablas_relacionadas.Get_All_Agrupadas(clsTransaccion.lConnection,
                                                                           clsTransaccion.lTransaction)

            Dim lPolizaAgrupadaCEALSA As New List(Of clsBeTablas_relacionadas)
            lPolizaAgrupadaCEALSA = lPolizasAgrupadas.FindAll(Function(x) x.Tabla = "CEALSA")

            Dim lPolizaAgrupadaWMS As New List(Of clsBeTablas_relacionadas)
            Dim bePolizaAgrupadaWMS As New clsBeTablas_relacionadas
            Dim bePedidoSinPoliza As New clsBeTrans_pe_enc

            vContadorProgress = 0
            prg.Value = 0
            prg.Minimum = 0
            prg.Maximum = lPolizaAgrupadaCEALSA.Count

            For Each PolizaAgrupadaCEALSA In lPolizaAgrupadaCEALSA

                If PolizaAgrupadaCEALSA.Descripcion = "005700 GEELY R" Then
                    Debug.Print(PolizaAgrupadaCEALSA.Descripcion)
                End If

                If PolizaAgrupadaCEALSA.Descripcion = "TOHILGU" Then
                    Debug.Print(PolizaAgrupadaCEALSA.Descripcion)
                End If


                lPolizaAgrupadaWMS = lPolizasAgrupadas.FindAll(Function(x) x.Tabla = "WMS" _
                                                                   AndAlso x.Unidad = PolizaAgrupadaCEALSA.Unidad _
                                                                   AndAlso x.Descripcion = PolizaAgrupadaCEALSA.Descripcion _
                                                                   AndAlso x.Fecha_orden_entrega = PolizaAgrupadaCEALSA.Fecha_orden_entrega _
                                                                   AndAlso x.Cantidad = PolizaAgrupadaCEALSA.Cantidad)
                vRegistrosActualizados = 0

                If Not lPolizaAgrupadaWMS Is Nothing Then

                    If lPolizaAgrupadaWMS.Count > 0 Then

                        bePolizaAgrupadaWMS = lPolizaAgrupadaWMS.Item(0)

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("El número de orden: " & PolizaAgrupadaCEALSA.Correlativo1 & " le corresponde al : " & bePolizaAgrupadaWMS.Agente_aduanal)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        lPedidosVerificados.Add(bePolizaAgrupadaWMS.Agente_aduanal)

                        bePolizaAgrupadaWMS.Correlativo1 = PolizaAgrupadaCEALSA.Correlativo1
                        bePolizaAgrupadaWMS.NoOrdenSalida = PolizaAgrupadaCEALSA.NoOrdenSalida
                        bePolizaAgrupadaWMS.Copoliza = PolizaAgrupadaCEALSA.Copoliza
                        bePolizaAgrupadaWMS.Utilizada = True
                        vRegistrosActualizados = clsLnTablas_relacionadas.Actualizar_Parcial(bePolizaAgrupadaWMS,
                                                                                             clsTransaccion.lConnection,
                                                                                             clsTransaccion.lTransaction)

                        PolizaAgrupadaCEALSA.Utilizada = True
                        vRegistrosActualizados = clsLnTablas_relacionadas.Actualizar_Parcial_CEALSA(PolizaAgrupadaCEALSA,
                                                                                                    clsTransaccion.lConnection,
                                                                                                    clsTransaccion.lTransaction)

                        bePedidoSinPoliza = clsLnTrans_pe_enc.Get_Pedido_Sin_Poliza_By_IdPedidoEnc(bePolizaAgrupadaWMS.Agente_aduanal,
                                                                                                   clsTransaccion.lConnection,
                                                                                                   clsTransaccion.lTransaction)

                        bePedidoSinPoliza.No_Documento_Externo = PolizaAgrupadaCEALSA.Copoliza
                        clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(bePedidoSinPoliza,
                                                                          clsTransaccion.lConnection,
                                                                          clsTransaccion.lTransaction)

                        If vRegistrosActualizados > 1 Then
                            Debug.WriteLine("espera")
                        End If

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Registros actualizados (" & vRegistrosActualizados & ") nùmero de orden: " & PolizaAgrupadaCEALSA.NoOrdenSalida & " IdPedidoEnc: " & bePolizaAgrupadaWMS.Agente_aduanal)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    Else

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("No hay coincidencia por nombre para el producto: " & PolizaAgrupadaCEALSA.Descripcion)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                Else

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("No hay coincidencia por nombre para el producto: " & PolizaAgrupadaCEALSA.Descripcion)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End If

                vContadorProgress += 1
                prg.Value = vContadorProgress

                Application.DoEvents()

            Next

            clsLnTablas_relacionadas.Actualiza_Polizas_Cruzadas(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            clsLnTrans_pe_pol.Inserta_Polizas(clsTransaccion.lConnection,
                                              clsTransaccion.lTransaction,
                                              prg,
                                              lblprg)


            clsLnTrans_pe_pol.Eliminar_Polizas_Cero(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()

            lPedidoSinPolizaWMS = clsLnTrans_pe_enc.Get_All_Pedidos_Sin_Poliza()

            If Not lPedidoSinPolizaWMS Is Nothing Then

                lblprg.AppendText("Se encontraron: " & lPedidoSinPolizaWMS.Count)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            Else

                lblprg.AppendText("No hay pedidos sin póliza.")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End If

            Dgrid.DataSource = lPedidoSinPolizaWMS
            GridView1.BestFitColumns()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Total pólizas encontradas: " & lPedidosVerificados.Count)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Total pedidos sin póliza: " & lPedidoSinPolizaWMS.Count)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()


        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Finally
            prg.Value = 0
            prg.Visible = False
            mnuAsociarPolizasExcelCEALSA.Enabled = True
        End Try

    End Sub

    Private Function Scan_Poliza(ByVal pPolizaScantxt As String,
                                 ByRef BeTmpPoliza As clsBeTmp_trans_pe_pol,
                                 ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Boolean

        Scan_Poliza = False
        BeTmpPoliza = Nothing

        Dim encabezado_duca As New clsBeCEALSA_DUCA_ENC
        Dim barra_poliza As String = pPolizaScantxt
        Dim Fecha_string = ""
        Dim upper_regimen As String = ""
        Dim vTipoRegimenSalida As String = ""
        Dim BeTmpPolizaExistente As New clsBeTmp_trans_pe_pol

        Try

            If String.IsNullOrEmpty(barra_poliza) Then

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("No hay póliza para leer")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
            Else

                Try

                    If barra_poliza.Length = 219 Then

                        Fecha_string = barra_poliza.Substring(30, 8)
                        encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 10) 'OK
                        encabezado_duca.Numero_DUCA = barra_poliza.Substring(10, 20) 'OK
                        encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                        encabezado_duca.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                        upper_regimen = barra_poliza.Substring(70, 5)
                        encabezado_duca.Regimen = upper_regimen.ToUpper()
                        encabezado_duca.Clase = barra_poliza.Substring(75, 3).Trim()
                        encabezado_duca.Pais_procedencia = barra_poliza.Substring(78, 2)
                        encabezado_duca.Modo_transporte = barra_poliza.Substring(80, 1)
                        encabezado_duca.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(81, 7))
                        encabezado_duca.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(88, 16))
                        encabezado_duca.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(104, 15))
                        encabezado_duca.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(119, 16))
                        encabezado_duca.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(135, 15))
                        encabezado_duca.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(150, 15))
                        encabezado_duca.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(165, 15))
                        encabezado_duca.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(180, 15))
                        encabezado_duca.Total_General = Convert.ToDouble(barra_poliza.Substring(195, 15))
                        encabezado_duca.Codigo_Poliza = barra_poliza.Substring(210, 9)

                        Dim comodin As String = "/"
                        Dim dd As String = ""
                        Dim mm As String = ""
                        Dim anio As String = ""

                        dd = Fecha_string.ToString.Substring(0, 2)
                        mm = Fecha_string.ToString.Substring(2, 2)
                        anio = Fecha_string.ToString.Substring(4, 4)
                        encabezado_duca.Fecha_Aceptacion = New Date(anio, mm, dd)

                        vTipoRegimenSalida = encabezado_duca.Regimen.Trim

                        'If (vTipoRegimenSalida.Contains("DI") OrElse vTipoRegimenSalida.Contains("ID")) Then

                        'Else

                        '    lblprg.AppendText(vbNewLine)
                        '    lblprg.AppendText("La poliza escaneada no parece ser una poliza válida de salida: " & vTipoRegimenSalida)
                        '    lblprg.AppendText(vbNewLine)
                        '    lblprg.Refresh()
                        '    lblprg.SelectionStart = lblprg.TextLength
                        '    lblprg.ScrollToCaret()
                        '    Exit Function

                        'End If

                        Dim BeRegimen As New clsBeRegimen_fiscal()
                        BeRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(vTipoRegimenSalida,
                                                                                    lConnection,
                                                                                    lTransaction)

                        If BeRegimen Is Nothing Then
                            'Throw New Exception("El régimen: " & encabezado_duca.Regimen & " no esta registrado en Régimen Fiscal, o no es legible desde el archivo de importación")
                            Debug.Print(vTipoRegimenSalida)
                        Else
                            BeTmpPoliza = New clsBeTmp_trans_pe_pol
                            BeTmpPoliza.IdOrdenPedidoPol = clsLnTmp_trans_pe_pol.MaxID(lConnection, lTransaction) + 1
                            BeTmpPoliza.IdOrdenPedidoEnc = 0
                            BeTmpPoliza.Bl_no = ""
                            BeTmpPoliza.NoPoliza = encabezado_duca.Codigo_Poliza
                            BeTmpPoliza.Pto_descarga = ""
                            BeTmpPoliza.Viaje_no = ""
                            BeTmpPoliza.Buque_no = ""
                            BeTmpPoliza.Remitente = ""
                            BeTmpPoliza.Fecha_abordaje = encabezado_duca.Fecha_Llegada
                            BeTmpPoliza.Destino = ""
                            BeTmpPoliza.Dir_destino = ""
                            BeTmpPoliza.Descripcion = ""
                            BeTmpPoliza.Po_number = ""
                            BeTmpPoliza.Cantidad = 0
                            BeTmpPoliza.Piezas = 0
                            BeTmpPoliza.Total_kgs = 0
                            BeTmpPoliza.Cbm = 0
                            BeTmpPoliza.Dua = encabezado_duca.Numero_DUCA
                            BeTmpPoliza.Fecha_poliza = encabezado_duca.Fecha_Llegada
                            BeTmpPoliza.Pais_procede = encabezado_duca.Pais_procedencia
                            BeTmpPoliza.Tipo_cambio = encabezado_duca.Tipo_cambio
                            BeTmpPoliza.Total_valoraduana = encabezado_duca.Total_valor_aduana
                            BeTmpPoliza.Total_lineas = 0
                            BeTmpPoliza.Total_bultos = 0
                            BeTmpPoliza.Total_bultos_peso = 0
                            BeTmpPoliza.Total_usd = encabezado_duca.TotalFOBUSD
                            BeTmpPoliza.Total_flete = encabezado_duca.Total_Flete_USD
                            BeTmpPoliza.Total_seguro = encabezado_duca.Total_Seguro_USD
                            BeTmpPoliza.User_agr = "MI3"
                            BeTmpPoliza.Fec_agr = Now
                            BeTmpPoliza.User_mod = "MI3"
                            BeTmpPoliza.Fec_mod = Now
                            BeTmpPoliza.Clave_aduana = encabezado_duca.Clave_aduana_despacho_destino
                            BeTmpPoliza.Nit_imp_exp = encabezado_duca.NIT_Importador
                            BeTmpPoliza.Clase = encabezado_duca.Clase
                            BeTmpPoliza.Mod_transporte = encabezado_duca.Modo_transporte
                            BeTmpPoliza.Total_liquidar = encabezado_duca.Total_Liquidar
                            BeTmpPoliza.Total_general = encabezado_duca.Total_General
                            BeTmpPoliza.Codigo_poliza = encabezado_duca.Codigo_Poliza
                            BeTmpPoliza.Ticket = 0
                            BeTmpPoliza.Numero_orden = encabezado_duca.Numero_Orden
                            BeTmpPoliza.Fecha_aceptacion = encabezado_duca.Fecha_Aceptacion
                            BeTmpPoliza.Fecha_llegada = encabezado_duca.Fecha_Llegada
                            BeTmpPoliza.Total_otros = 0

                            Dim BeRegimentmpPol As New clsBeRegimen_fiscal
                            BeRegimentmpPol = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(encabezado_duca.Regimen.Trim,
                                                                                              lConnection,
                                                                                              lTransaction)

                            If Not BeRegimentmpPol Is Nothing Then

                                BeTmpPoliza.IdRegimen = BeRegimentmpPol.IdRegimen

                                BeTmpPolizaExistente = BeTmpPoliza

                                clsLnTmp_trans_pe_pol.GetSingle(BeTmpPolizaExistente,
                                                               lConnection,
                                                               lTransaction)

                                If BeTmpPolizaExistente Is Nothing Then

                                    clsLnTmp_trans_pe_pol.Insertar(BeTmpPoliza, lConnection, lTransaction)

                                End If

                                Scan_Poliza = True

                            Else
                                Throw New Exception("ERROR_202308181200: Regimen no encontrado: " & encabezado_duca.Regimen)
                            End If

                        End If

                    Else

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("ERROR_202308181246: al procesar la poliza: " & barra_poliza & " tiene una cantidad de: " & barra_poliza.Length & " la longitud esperada es: 219")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                Catch ex As Exception
                    Throw ex
                End Try

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Procesar_Polizar_Importadas()

        Dim clsTransaccion As New clsTransaccion
        Dim vResult As Integer = 0
        Dim BeTmpPolizaResultante As New clsBeTmp_trans_pe_pol
        Dim BePedidoEnc As New clsBeTrans_pe_enc
        Dim vSQL As String = ""
        Dim cmd As New SqlCommand

        Try

            clsTransaccion.Begin_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Obteniendo polizas...")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            'vSQL = "UPDATE Polizas SET procesada = 0  "
            'cmd = New SqlCommand(vSQL, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            'cmd.ExecuteNonQuery()

            Dim lPolizasSinPedido As New List(Of clsBePolizas)
            lPolizasSinPedido = clsLnPolizas.Get_All_Sin_Procesar(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Registros encontrados: " & lPolizasSinPedido.Count)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim vContadorPoliza As Integer = 0
            Dim vTotalPolizas As Integer = 0

            vTotalPolizas = lPolizasSinPedido.Count()

            For Each Registro In lPolizasSinPedido

                vContadorPoliza += 1

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Procesando Poliza (" & vContadorPoliza & " de: " & vTotalPolizas & ")")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Registro: " & Registro.Polizas)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                If Registro.Polizas = "2613703011GTGUACG230335440001928072023GTJUTVN                  760632K157DR10 307.86960000000242557.44000000002806.140000000030822.08000000000000.00000000000000.00000000000000.000000000000000000000000000000002PP3O9JH7" Then
                    Debug.Print("Hola")
                End If

                If Scan_Poliza(Registro.Polizas.Trim,
                               BeTmpPolizaResultante,
                               clsTransaccion.lConnection,
                               clsTransaccion.lTransaction) Then

                    Registro.Procesada = True
                    vResult = clsLnPolizas.Actualizar(Registro,
                                                      clsTransaccion.lConnection,
                                                      clsTransaccion.lTransaction)

                    If vResult > 0 Then

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(vbTab & " -> Poliza insertada correctamente en tmp_trans_pe_pol.")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                    'If Not BeTmpPolizaResultante Is Nothing Then

                    '    BePedidoEnc = clsLnTrans_pe_enc.Get_Single_By_NoDocumentoExterno(BeTmpPolizaResultante.Numero_orden,
                    '                                                                     clsTransaccion.lConnection,
                    '                                                                     clsTransaccion.lTransaction)

                    '    If BeTmpPolizaResultante.Numero_orden = "3293707125" Then
                    '        Debug.Print("3293707125")
                    '    End If

                    '    If Not BePedidoEnc Is Nothing Then

                    '        BeTmpPolizaResultante.IdOrdenPedidoEnc = BePedidoEnc.IdPedidoEnc
                    '        clsLnTmp_trans_pe_pol.Actualizar_IdPedidoEnc(BeTmpPolizaResultante,
                    '                                                     clsTransaccion.lConnection,
                    '                                                     clsTransaccion.lTransaction)


                    '        Registro.Procesada = True
                    '        vResult = clsLnPolizas.Actualizar(Registro,
                    '                                          clsTransaccion.lConnection,
                    '                                          clsTransaccion.lTransaction)

                    '        If vResult > 0 Then

                    '            lblprg.AppendText(vbNewLine)
                    '            lblprg.AppendText(vbTab & " -> Poliza insertada correctamente.")
                    '            lblprg.AppendText(vbNewLine)
                    '            lblprg.Refresh()
                    '            lblprg.SelectionStart = lblprg.TextLength
                    '            lblprg.ScrollToCaret()

                    '        End If

                    '    Else



                    '        lblprg.AppendText(vbNewLine)
                    '        lblprg.AppendText("No se encontró pedido asociado al número de órden: " & BeTmpPolizaResultante.Numero_orden)
                    '        lblprg.AppendText(vbNewLine)
                    '        lblprg.Refresh()
                    '        lblprg.SelectionStart = lblprg.TextLength
                    '        lblprg.ScrollToCaret()

                    '    End If

                    'End If

                Else

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("La poliza no pudo ser procesada.")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End If

            Next

            'clsLnTrans_pe_pol.Actualiza_Polizas(clsTransaccion.lConnection,
            '                                    clsTransaccion.lTransaction,
            '                                    prg,
            '                                    lblprg)

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtScanPoliza_KeyDown(sender As Object, e As KeyEventArgs) Handles txtScanPoliza.KeyDown

        If e.KeyCode = Keys.Enter Then
            If Not txtScanPoliza.Text.Trim = "" Then
                Scan_Poliza(txtScanPoliza.Text)
                txtScanPoliza.Text = ""
            End If

        End If

    End Sub

    Private Function Scan_Poliza(ByVal pPolizaScantxt As String) As Boolean

        Scan_Poliza = False

        Dim encabezado_duca As New clsBeCEALSA_DUCA_ENC
        Dim barra_poliza As String = pPolizaScantxt
        Dim Fecha_string = ""
        Dim upper_regimen As String = ""
        Dim clsTransaccion As New clsTransaccion
        'Dim BePedidoEnc As New clsBeTrans_pe_enc
        Dim BePedidoEncList As New List(Of clsBeTrans_pe_enc)
        Dim BeTmpPolizaExistente As New clsBeTmp_trans_pe_pol
        Dim vIdOrdenPedidoEnc As Integer = 0
        Dim BeTmpPoliza As New clsBeTmp_trans_pe_pol
        Dim vTipoRegimenSalida As String = ""

        Try

            clsTransaccion.Begin_Transaction()

            If String.IsNullOrEmpty(barra_poliza) Then

                XtraMessageBox.Show("No hay póliza para leer", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else

                Try

                    If barra_poliza.Length = 219 Then

                        Fecha_string = barra_poliza.Substring(30, 8)
                        encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 10) 'OK
                        encabezado_duca.Numero_DUCA = barra_poliza.Substring(10, 20) 'OK
                        encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                        encabezado_duca.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                        upper_regimen = barra_poliza.Substring(70, 5)
                        encabezado_duca.Regimen = upper_regimen.ToUpper()
                        encabezado_duca.Clase = barra_poliza.Substring(75, 3).Trim()
                        encabezado_duca.Pais_procedencia = barra_poliza.Substring(78, 2)
                        encabezado_duca.Modo_transporte = barra_poliza.Substring(80, 1)
                        encabezado_duca.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(81, 7))
                        encabezado_duca.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(88, 16))
                        encabezado_duca.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(104, 15))
                        encabezado_duca.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(119, 16))
                        encabezado_duca.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(135, 15))
                        encabezado_duca.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(150, 15))
                        encabezado_duca.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(165, 15))
                        encabezado_duca.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(180, 15))
                        encabezado_duca.Total_General = Convert.ToDouble(barra_poliza.Substring(195, 15))
                        encabezado_duca.Codigo_Poliza = barra_poliza.Substring(210, 9)

                        Dim comodin As String = "/"
                        Dim dd As String = ""
                        Dim mm As String = ""
                        Dim anio As String = ""

                        dd = Fecha_string.ToString.Substring(0, 2)
                        mm = Fecha_string.ToString.Substring(2, 2)
                        anio = Fecha_string.ToString.Substring(4, 4)
                        encabezado_duca.Fecha_Aceptacion = New Date(anio, mm, dd)

                        vTipoRegimenSalida = encabezado_duca.Regimen.Trim

                        'If (vTipoRegimenSalida.Contains("DI") OrElse vTipoRegimenSalida.Contains("ID")) Then

                        'Else

                        '    lblprg.AppendText(vbNewLine)
                        '    lblprg.AppendText("La poliza escaneada no parece ser una poliza válida de salida: " & vTipoRegimenSalida)
                        '    lblprg.AppendText(vbNewLine)
                        '    lblprg.Refresh()
                        '    lblprg.SelectionStart = lblprg.TextLength
                        '    lblprg.ScrollToCaret()
                        '    Exit Function

                        'End If

                        Dim BeRegimen As New clsBeRegimen_fiscal()
                        BeRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(vTipoRegimenSalida)

                        If BeRegimen Is Nothing Then
                            Throw New Exception("El régimen: " & encabezado_duca.Regimen & " no esta registrado en Régimen Fiscal, o no es legible desde el archivo de importación")
                        End If

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Póliza parseada correctamente: " & encabezado_duca.Numero_Orden)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        BePedidoEncList = clsLnTrans_pe_enc.Get_All_By_NoDocumentoExterno(encabezado_duca.Numero_Orden,
                                                                                      clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

                        If Not BePedidoEncList Is Nothing Then

                            If BePedidoEncList.Count > 0 Then

                                For Each BePedidoEnc In BePedidoEncList

                                    vIdOrdenPedidoEnc = 0

                                    If Not BePedidoEnc Is Nothing Then

                                        vIdOrdenPedidoEnc = BePedidoEnc.IdPedidoEnc

                                        lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText("Se obtuvo el pedido " & vIdOrdenPedidoEnc & " para el número de orden : " & encabezado_duca.Numero_Orden)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        BeTmpPoliza = New clsBeTmp_trans_pe_pol
                                        BeTmpPoliza.IdOrdenPedidoPol = clsLnTmp_trans_pe_pol.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                                        BeTmpPoliza.IdOrdenPedidoEnc = vIdOrdenPedidoEnc
                                        BeTmpPoliza.Bl_no = ""
                                        BeTmpPoliza.NoPoliza = encabezado_duca.Codigo_Poliza
                                        BeTmpPoliza.Pto_descarga = ""
                                        BeTmpPoliza.Viaje_no = ""
                                        BeTmpPoliza.Buque_no = ""
                                        BeTmpPoliza.Remitente = ""
                                        BeTmpPoliza.Fecha_abordaje = encabezado_duca.Fecha_Llegada
                                        BeTmpPoliza.Destino = ""
                                        BeTmpPoliza.Dir_destino = ""
                                        BeTmpPoliza.Descripcion = ""
                                        BeTmpPoliza.Po_number = ""
                                        BeTmpPoliza.Cantidad = 0
                                        BeTmpPoliza.Piezas = 0
                                        BeTmpPoliza.Total_kgs = 0
                                        BeTmpPoliza.Cbm = 0
                                        BeTmpPoliza.Dua = encabezado_duca.Numero_DUCA
                                        BeTmpPoliza.Fecha_poliza = encabezado_duca.Fecha_Llegada
                                        BeTmpPoliza.Pais_procede = encabezado_duca.Pais_procedencia
                                        BeTmpPoliza.Tipo_cambio = encabezado_duca.Tipo_cambio
                                        BeTmpPoliza.Total_valoraduana = encabezado_duca.Total_valor_aduana
                                        BeTmpPoliza.Total_lineas = 0
                                        BeTmpPoliza.Total_bultos = 0
                                        BeTmpPoliza.Total_bultos_peso = 0
                                        BeTmpPoliza.Total_usd = encabezado_duca.TotalFOBUSD
                                        BeTmpPoliza.Total_flete = encabezado_duca.Total_Flete_USD
                                        BeTmpPoliza.Total_seguro = encabezado_duca.Total_Seguro_USD
                                        BeTmpPoliza.User_agr = "MI3"
                                        BeTmpPoliza.Fec_agr = Now
                                        BeTmpPoliza.User_mod = "MI3"
                                        BeTmpPoliza.Fec_mod = Now
                                        BeTmpPoliza.Clave_aduana = encabezado_duca.Clave_aduana_despacho_destino
                                        BeTmpPoliza.Nit_imp_exp = encabezado_duca.NIT_Importador
                                        BeTmpPoliza.Clase = encabezado_duca.Clase
                                        BeTmpPoliza.Mod_transporte = encabezado_duca.Modo_transporte
                                        BeTmpPoliza.Total_liquidar = encabezado_duca.Total_Liquidar
                                        BeTmpPoliza.Total_general = encabezado_duca.Total_General
                                        BeTmpPoliza.Codigo_poliza = encabezado_duca.Codigo_Poliza
                                        BeTmpPoliza.Ticket = 0
                                        BeTmpPoliza.Numero_orden = encabezado_duca.Numero_Orden
                                        BeTmpPoliza.Fecha_aceptacion = encabezado_duca.Fecha_Aceptacion
                                        BeTmpPoliza.Fecha_llegada = encabezado_duca.Fecha_Llegada
                                        BeTmpPoliza.Total_otros = 0

                                        Dim BeRegimentmpPol As New clsBeRegimen_fiscal
                                        BeRegimentmpPol = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(encabezado_duca.Regimen.Trim,
                                                                                      clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

                                        If Not BeRegimentmpPol Is Nothing Then
                                            BeTmpPoliza.IdRegimen = BeRegimentmpPol.IdRegimen
                                        Else
                                            Throw New Exception("ERROR_202308181200: Regimen no encontrado: " & encabezado_duca.Regimen)
                                        End If

                                        BeTmpPolizaExistente = BeTmpPoliza

                                        clsLnTmp_trans_pe_pol.GetSingle(BeTmpPolizaExistente,
                                                                    clsTransaccion.lConnection,
                                                                    clsTransaccion.lTransaction)

                                        If BeTmpPolizaExistente Is Nothing Then
                                            clsLnTmp_trans_pe_pol.Insertar(BeTmpPoliza,
                                                                       clsTransaccion.lConnection,
                                                                       clsTransaccion.lTransaction)

                                            lblprg.AppendText(vbNewLine)
                                            lblprg.AppendText("Se insertó el IdOrdenPedidoPoliza (" & BeTmpPoliza.IdOrdenPedidoPol & ") de la póliza  escaneada con el número de orden : " & encabezado_duca.Numero_Orden & " en la tabla temporal")
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        Else

                                            lblprg.AppendText(vbNewLine)
                                            lblprg.AppendText("No se insertó el IdOrdenPedidoPoliza (" & BeTmpPoliza.IdOrdenPedidoPol & ") de la póliza escaneada con el número de orden : " & encabezado_duca.Numero_Orden & " en la tabla temporal porque ya existe")
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        End If

                                        If BeTmpPoliza IsNot Nothing And BeTmpPoliza.IdOrdenPedidoEnc <> 0 Then
                                            BeTmpPoliza.IdOrdenPedidoPol = 1
                                            clsLnTrans_pe_pol.Actualizar(BeTmpPoliza,
                                                                     clsTransaccion.lConnection,
                                                                     clsTransaccion.lTransaction)

                                            lblprg.AppendText(vbNewLine)
                                            lblprg.AppendText("Se actualizó la póliza escaneada con el número de orden : " & encabezado_duca.Numero_Orden & " para el pedido " & vIdOrdenPedidoEnc)
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        End If

                                    End If

                                Next

                            Else

                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText("No se obtuvo pedido para el número de orden : " & encabezado_duca.Numero_Orden)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                                vIdOrdenPedidoEnc = 0

                                BeTmpPoliza = New clsBeTmp_trans_pe_pol
                                BeTmpPoliza.IdOrdenPedidoPol = clsLnTmp_trans_pe_pol.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                                BeTmpPoliza.IdOrdenPedidoEnc = vIdOrdenPedidoEnc
                                BeTmpPoliza.Bl_no = ""
                                BeTmpPoliza.NoPoliza = encabezado_duca.Codigo_Poliza
                                BeTmpPoliza.Pto_descarga = ""
                                BeTmpPoliza.Viaje_no = ""
                                BeTmpPoliza.Buque_no = ""
                                BeTmpPoliza.Remitente = ""
                                BeTmpPoliza.Fecha_abordaje = encabezado_duca.Fecha_Llegada
                                BeTmpPoliza.Destino = ""
                                BeTmpPoliza.Dir_destino = ""
                                BeTmpPoliza.Descripcion = ""
                                BeTmpPoliza.Po_number = ""
                                BeTmpPoliza.Cantidad = 0
                                BeTmpPoliza.Piezas = 0
                                BeTmpPoliza.Total_kgs = 0
                                BeTmpPoliza.Cbm = 0
                                BeTmpPoliza.Dua = encabezado_duca.Numero_DUCA
                                BeTmpPoliza.Fecha_poliza = encabezado_duca.Fecha_Llegada
                                BeTmpPoliza.Pais_procede = encabezado_duca.Pais_procedencia
                                BeTmpPoliza.Tipo_cambio = encabezado_duca.Tipo_cambio
                                BeTmpPoliza.Total_valoraduana = encabezado_duca.Total_valor_aduana
                                BeTmpPoliza.Total_lineas = 0
                                BeTmpPoliza.Total_bultos = 0
                                BeTmpPoliza.Total_bultos_peso = 0
                                BeTmpPoliza.Total_usd = encabezado_duca.TotalFOBUSD
                                BeTmpPoliza.Total_flete = encabezado_duca.Total_Flete_USD
                                BeTmpPoliza.Total_seguro = encabezado_duca.Total_Seguro_USD
                                BeTmpPoliza.User_agr = "MI3"
                                BeTmpPoliza.Fec_agr = Now
                                BeTmpPoliza.User_mod = "MI3"
                                BeTmpPoliza.Fec_mod = Now
                                BeTmpPoliza.Clave_aduana = encabezado_duca.Clave_aduana_despacho_destino
                                BeTmpPoliza.Nit_imp_exp = encabezado_duca.NIT_Importador
                                BeTmpPoliza.Clase = encabezado_duca.Clase
                                BeTmpPoliza.Mod_transporte = encabezado_duca.Modo_transporte
                                BeTmpPoliza.Total_liquidar = encabezado_duca.Total_Liquidar
                                BeTmpPoliza.Total_general = encabezado_duca.Total_General
                                BeTmpPoliza.Codigo_poliza = encabezado_duca.Codigo_Poliza
                                BeTmpPoliza.Ticket = 0
                                BeTmpPoliza.Numero_orden = encabezado_duca.Numero_Orden
                                BeTmpPoliza.Fecha_aceptacion = encabezado_duca.Fecha_Aceptacion
                                BeTmpPoliza.Fecha_llegada = encabezado_duca.Fecha_Llegada
                                BeTmpPoliza.Total_otros = 0

                                Dim BeRegimentmpPol As New clsBeRegimen_fiscal
                                BeRegimentmpPol = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(encabezado_duca.Regimen.Trim,
                                                                                      clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

                                If Not BeRegimentmpPol Is Nothing Then
                                    BeTmpPoliza.IdRegimen = BeRegimentmpPol.IdRegimen
                                Else
                                    Throw New Exception("ERROR_202308181200: Regimen no encontrado: " & encabezado_duca.Regimen)
                                End If

                                BeTmpPolizaExistente = BeTmpPoliza

                                clsLnTmp_trans_pe_pol.GetSingle(BeTmpPolizaExistente,
                                                    clsTransaccion.lConnection,
                                                    clsTransaccion.lTransaction)

                                If BeTmpPolizaExistente Is Nothing Then
                                    clsLnTmp_trans_pe_pol.Insertar(BeTmpPoliza,
                                                       clsTransaccion.lConnection,
                                                       clsTransaccion.lTransaction)

                                    lblprg.AppendText(vbNewLine)
                                    lblprg.AppendText("Se insertó el IdOrdenPedidoPoliza (" & BeTmpPoliza.IdOrdenPedidoPol & ") de la póliza  escaneada con el número de orden : " & encabezado_duca.Numero_Orden & " en la tabla temporal")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                Else

                                    lblprg.AppendText(vbNewLine)
                                    lblprg.AppendText("No se insertó el IdOrdenPedidoPoliza (" & BeTmpPoliza.IdOrdenPedidoPol & ") de la póliza escaneada con el número de orden : " & encabezado_duca.Numero_Orden & " en la tabla temporal porque ya existe")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                End If


                            End If

                        Else

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("No se obtuvo pedido para el número de orden : " & encabezado_duca.Numero_Orden)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            vIdOrdenPedidoEnc = 0

                            BeTmpPoliza = New clsBeTmp_trans_pe_pol
                            BeTmpPoliza.IdOrdenPedidoPol = clsLnTmp_trans_pe_pol.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                            BeTmpPoliza.IdOrdenPedidoEnc = vIdOrdenPedidoEnc
                            BeTmpPoliza.Bl_no = ""
                            BeTmpPoliza.NoPoliza = encabezado_duca.Codigo_Poliza
                            BeTmpPoliza.Pto_descarga = ""
                            BeTmpPoliza.Viaje_no = ""
                            BeTmpPoliza.Buque_no = ""
                            BeTmpPoliza.Remitente = ""
                            BeTmpPoliza.Fecha_abordaje = encabezado_duca.Fecha_Llegada
                            BeTmpPoliza.Destino = ""
                            BeTmpPoliza.Dir_destino = ""
                            BeTmpPoliza.Descripcion = ""
                            BeTmpPoliza.Po_number = ""
                            BeTmpPoliza.Cantidad = 0
                            BeTmpPoliza.Piezas = 0
                            BeTmpPoliza.Total_kgs = 0
                            BeTmpPoliza.Cbm = 0
                            BeTmpPoliza.Dua = encabezado_duca.Numero_DUCA
                            BeTmpPoliza.Fecha_poliza = encabezado_duca.Fecha_Llegada
                            BeTmpPoliza.Pais_procede = encabezado_duca.Pais_procedencia
                            BeTmpPoliza.Tipo_cambio = encabezado_duca.Tipo_cambio
                            BeTmpPoliza.Total_valoraduana = encabezado_duca.Total_valor_aduana
                            BeTmpPoliza.Total_lineas = 0
                            BeTmpPoliza.Total_bultos = 0
                            BeTmpPoliza.Total_bultos_peso = 0
                            BeTmpPoliza.Total_usd = encabezado_duca.TotalFOBUSD
                            BeTmpPoliza.Total_flete = encabezado_duca.Total_Flete_USD
                            BeTmpPoliza.Total_seguro = encabezado_duca.Total_Seguro_USD
                            BeTmpPoliza.User_agr = "MI3"
                            BeTmpPoliza.Fec_agr = Now
                            BeTmpPoliza.User_mod = "MI3"
                            BeTmpPoliza.Fec_mod = Now
                            BeTmpPoliza.Clave_aduana = encabezado_duca.Clave_aduana_despacho_destino
                            BeTmpPoliza.Nit_imp_exp = encabezado_duca.NIT_Importador
                            BeTmpPoliza.Clase = encabezado_duca.Clase
                            BeTmpPoliza.Mod_transporte = encabezado_duca.Modo_transporte
                            BeTmpPoliza.Total_liquidar = encabezado_duca.Total_Liquidar
                            BeTmpPoliza.Total_general = encabezado_duca.Total_General
                            BeTmpPoliza.Codigo_poliza = encabezado_duca.Codigo_Poliza
                            BeTmpPoliza.Ticket = 0
                            BeTmpPoliza.Numero_orden = encabezado_duca.Numero_Orden
                            BeTmpPoliza.Fecha_aceptacion = encabezado_duca.Fecha_Aceptacion
                            BeTmpPoliza.Fecha_llegada = encabezado_duca.Fecha_Llegada
                            BeTmpPoliza.Total_otros = 0

                            Dim BeRegimentmpPol As New clsBeRegimen_fiscal
                            BeRegimentmpPol = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(encabezado_duca.Regimen.Trim,
                                                                                      clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

                            If Not BeRegimentmpPol Is Nothing Then
                                BeTmpPoliza.IdRegimen = BeRegimentmpPol.IdRegimen
                            Else
                                Throw New Exception("ERROR_202308181200: Regimen no encontrado: " & encabezado_duca.Regimen)
                            End If

                            BeTmpPolizaExistente = BeTmpPoliza

                            clsLnTmp_trans_pe_pol.GetSingle(BeTmpPolizaExistente,
                                                        clsTransaccion.lConnection,
                                                        clsTransaccion.lTransaction)

                            If BeTmpPolizaExistente Is Nothing Then
                                clsLnTmp_trans_pe_pol.Insertar(BeTmpPoliza,
                                                           clsTransaccion.lConnection,
                                                           clsTransaccion.lTransaction)

                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText("Se insertó el IdOrdenPedidoPoliza (" & BeTmpPoliza.IdOrdenPedidoPol & ") de la póliza  escaneada con el número de orden : " & encabezado_duca.Numero_Orden & " en la tabla temporal")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            Else

                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText("No se insertó el IdOrdenPedidoPoliza (" & BeTmpPoliza.IdOrdenPedidoPol & ") de la póliza escaneada con el número de orden : " & encabezado_duca.Numero_Orden & " en la tabla temporal porque ya existe")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            End If

                        End If

                        Scan_Poliza = True

                    Else

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("ERROR_202308181246: al procesar la poliza: " & barra_poliza & " tiene una cantidad de: " & barra_poliza.Length & " la longitud esperada es: 219")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                Catch ex As Exception
                    Throw ex
                End Try

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Asociar_Poliza_Por_Copoliza()

        Dim clsTransaccion As New clsTransaccion
        Dim BeTablaRelacionada As New clsBeTablas_relacionadas
        Dim lPolizasParciales As New List(Of clsBeTmp_trans_pe_pol)
        Dim vNoOrden As String = ""
        Dim vIdPedidoEncFromAgenteAduanal As Integer = 0
        Dim BePolizaParcialExiste As New clsBeTrans_pe_pol

        Try

            mnuProceso2.Enabled = False

            clsTransaccion.Begin_Transaction()

            lPolizasParciales = clsLnTablas_relacionadas.Get_All_Polizas_Parciales(clsTransaccion.lConnection,
                                                                                   clsTransaccion.lTransaction)

            For Each PolizaParcial In lPolizasParciales

                vNoOrden = PolizaParcial.Numero_orden
                BeTablaRelacionada = clsLnTablas_relacionadas.Get_Single_By_NoOrden(vNoOrden,
                                                                              clsTransaccion.lConnection,
                                                                              clsTransaccion.lTransaction)

                If Not BeTablaRelacionada Is Nothing Then

                    vIdPedidoEncFromAgenteAduanal = BeTablaRelacionada.Agente_aduanal
                    PolizaParcial.IdOrdenPedidoEnc = vIdPedidoEncFromAgenteAduanal

                    If vIdPedidoEncFromAgenteAduanal = 6958 Then
                        Debug.Print(vIdPedidoEncFromAgenteAduanal)
                    End If

                    BePolizaParcialExiste = clsLnTrans_pe_pol.GetSingleId(vIdPedidoEncFromAgenteAduanal, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    If BePolizaParcialExiste Is Nothing Then

                        clsLnTrans_pe_pol.Insertar(PolizaParcial,
                                               clsTransaccion.lConnection,
                                               clsTransaccion.lTransaction)

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Se insertó el IdOrdenPedidoPoliza (" & PolizaParcial.IdOrdenPedidoPol & ") de la póliza  escaneada con el número de orden : " & vNoOrden & " en la tabla temporal")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    Else

                        If BePolizaParcialExiste.Dua = "-" Then

                            clsLnTrans_pe_pol.Actualizar(PolizaParcial,
                                              clsTransaccion.lConnection,
                                              clsTransaccion.lTransaction)

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Se insertó el IdOrdenPedidoPoliza (" & PolizaParcial.IdOrdenPedidoPol & ") de la póliza  escaneada con el número de orden : " & vNoOrden & " en la tabla temporal")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        Else

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("No se encontró registro para el número de órden: " & vNoOrden)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If


                    End If

                Else

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("No se encontró registro para el número de órden: " & vNoOrden)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End If

            Next

            clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()

            mnuProceso2.Enabled = True

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            mnuProceso2.Enabled = True

        End Try


    End Sub


    Private Sub Buscar_Por_Coincidencias()

        Dim clsTransaccion As New clsTransaccion
        Dim BeTransPePol As New clsBeTablas_relacionadas
        Dim vCantidadCoincidenciasSimiles As Integer = 0
        Dim lBeTablas_relacionadas As New List(Of clsBeTablas_relacionadas)
        Dim vNoOrdenSalidaCealsa As String = ""
        Dim vCoPoliza As String = ""
        Dim BeTmpPolizaEscaneada As New clsBeTmp_trans_pe_pol
        Dim vPolizasSinScan As Integer = 0
        Dim vNoCoincidencias As Integer = 0
        Dim vEncontroPorCoPoliza As Boolean = False
        Dim vRegistrosPorCoPoliza As Integer = 0

        Try

            lblprg.Text = "" : Dgrid.DataSource = Nothing

            clsTransaccion.Begin_Transaction()

            Dim lPolizasCealsa As New List(Of clsBeTablas_relacionadas)
            lPolizasCealsa = clsLnTablas_relacionadas.Get_All_Cealsa(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            lblprg.AppendText("Get_All_CEALSA: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Registros: " & lPolizasCealsa.Count)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim lPolizasWMS As New List(Of clsBeTablas_relacionadas)
            lPolizasWMS = clsLnTablas_relacionadas.Get_All_WMS_Sin_Asociacion(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            lblprg.AppendText("Get_All_WMS: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Registros: " & lPolizasWMS.Count)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim BeCoincidenciaWMS As New clsBeTablas_relacionadas

            For Each PedidoImportadoWMS In lPolizasWMS

                vEncontroPorCoPoliza = False

                BeCoincidenciaWMS = lPolizasCealsa.Find(Function(x) x.Descripcion.Contains(PedidoImportadoWMS.Descripcion) _
                                                        AndAlso x.Cantidad = PedidoImportadoWMS.Cantidad _
                                                        AndAlso x.Fecha_orden_entrega = PedidoImportadoWMS.Fecha_orden_entrega _
                                                        AndAlso x.Consignatario.Contains(PedidoImportadoWMS.Consignatario))

                If Not BeCoincidenciaWMS Is Nothing Then

                    vNoOrdenSalidaCealsa = BeCoincidenciaWMS.NoOrdenSalida
                    vCoPoliza = BeCoincidenciaWMS.Copoliza

                    BeTmpPolizaEscaneada = clsLnTmp_trans_pe_pol.Get_Single_By_No_Orden(vNoOrdenSalidaCealsa,
                                                                                        clsTransaccion.lConnection,
                                                                                        clsTransaccion.lTransaction)

                    If BeTmpPolizaEscaneada Is Nothing Then

                        BeTmpPolizaEscaneada = clsLnTmp_trans_pe_pol.Get_Single_By_No_Orden(vCoPoliza,
                                                                                            clsTransaccion.lConnection,
                                                                                            clsTransaccion.lTransaction)

                        If Not BeTmpPolizaEscaneada Is Nothing Then
                            vEncontroPorCoPoliza = True
                        Else
                            vEncontroPorCoPoliza = False
                        End If

                        If vEncontroPorCoPoliza Then

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Registro por copoliza encontrado (en teoría): " & vCoPoliza)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            vRegistrosPorCoPoliza += 1

                        End If

                    End If

                    If Not BeTmpPolizaEscaneada Is Nothing Then

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("¿Es esta una coincidencia?: " & vNoOrdenSalidaCealsa)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        vCantidadCoincidenciasSimiles += 1

                        lBeTablas_relacionadas.Add(BeCoincidenciaWMS)
                        lBeTablas_relacionadas.Add(PedidoImportadoWMS)

                    Else

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Se encontró coincidencia pero la poliza no está escaneada: " & BeCoincidenciaWMS.NoOrdenSalida)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        vPolizasSinScan += 1

                        If Not BeCoincidenciaWMS Is Nothing Then
                            lBeTablas_relacionadas.Add(BeCoincidenciaWMS)
                            lBeTablas_relacionadas.Add(PedidoImportadoWMS)
                        End If

                    End If

                Else

                    '#EJC20230821: Buscar con 1 día de desfase hacia adelante.
                    BeCoincidenciaWMS = lPolizasCealsa.Find(Function(x) x.Descripcion.Contains(PedidoImportadoWMS.Descripcion) _
                                                            AndAlso x.Cantidad = PedidoImportadoWMS.Cantidad _
                                                            AndAlso x.Fecha_orden_entrega = PedidoImportadoWMS.Fecha_orden_entrega.AddDays(1) _
                                                            AndAlso x.Consignatario.Contains(PedidoImportadoWMS.Consignatario))

                    If Not BeCoincidenciaWMS Is Nothing Then

                        BeTmpPolizaEscaneada = clsLnTmp_trans_pe_pol.Get_Single_By_No_Orden(vNoOrdenSalidaCealsa,
                                                                                            clsTransaccion.lConnection,
                                                                                            clsTransaccion.lTransaction)

                        If BeTmpPolizaEscaneada Is Nothing Then

                            BeTmpPolizaEscaneada = clsLnTmp_trans_pe_pol.Get_Single_By_No_Orden(vCoPoliza,
                                                                                                clsTransaccion.lConnection,
                                                                                                clsTransaccion.lTransaction)

                            If Not BeTmpPolizaEscaneada Is Nothing Then
                                vEncontroPorCoPoliza = True
                            Else
                                vEncontroPorCoPoliza = False
                            End If

                            If vEncontroPorCoPoliza Then

                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText("Registro por copoliza encontrado (en teoría): " & vCoPoliza)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                vRegistrosPorCoPoliza += 1

                            Else

                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText("Definitivamente no existe: " & vCoPoliza)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                vNoCoincidencias += 1

                            End If

                        Else

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Una singularidad.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            vCantidadCoincidenciasSimiles += 1

                        End If

                    Else

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("No se encontró coincidencia para: " & PedidoImportadoWMS.Descripcion & " " & PedidoImportadoWMS.Correlativo1)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        vNoCoincidencias += 1

                    End If

                End If

                Application.DoEvents()

            Next

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Coincidencias: " & vCantidadCoincidenciasSimiles)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("NO Coincidencias: " & vNoCoincidencias)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Polizas sin scan: " & vPolizasSinScan)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dgrid.DataSource = lBeTablas_relacionadas

            If GridView1.Columns.Count > 0 Then
                GridView1.BestFitColumns()
            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuAsociarPolizasImportadas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsociarPolizasExcelCEALSA.ItemClick
        Asociar_Polizas_Excel_CEALSA()
    End Sub

    Private Sub mnuProceso2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProceso2.ItemClick
        Asociar_Poliza_Por_Copoliza()
    End Sub

    Private Sub mnuProcesarPorSimilitud_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProcesarPorSimilitud.ItemClick
        Buscar_Por_Coincidencias()
    End Sub

    Private Sub mnuEquipararNombres_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEquipararNombres.ItemClick

        Dim clsTransaccion As New clsTransaccion
        Dim vRegistrosActualizados As Integer = 0
        Dim lPedidosVerificados As New List(Of Integer)

        Try

            clsTransaccion.Begin_Transaction()

            clsLnTablas_relacionadas.Equiparar_Nombres_Agrupacion(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            Dim lPolizasAgrupadas As New List(Of clsBeTablas_relacionadas)
            lPolizasAgrupadas = clsLnTablas_relacionadas.Get_All_Agrupadas(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            Dim lPolizaAgrupadaCEALSA As New List(Of clsBeTablas_relacionadas)
            lPolizaAgrupadaCEALSA = lPolizasAgrupadas.FindAll(Function(x) x.Tabla = "CEALSA")

            Dim lPolizaAgrupadaWMS As New List(Of clsBeTablas_relacionadas)
            Dim bePolizaAgrupadaWMS As New clsBeTablas_relacionadas
            Dim bePedidoSinPoliza As New clsBeTrans_pe_enc

            For Each PolizaAgrupadaCEALSA In lPolizaAgrupadaCEALSA

                lPolizaAgrupadaWMS = lPolizasAgrupadas.FindAll(Function(x) x.Tabla = "WMS" AndAlso
                                                               x.Unidad = PolizaAgrupadaCEALSA.Unidad AndAlso
                                                               x.Descripcion = PolizaAgrupadaCEALSA.Descripcion AndAlso
                                                               x.Fecha_orden_entrega = PolizaAgrupadaCEALSA.Fecha_orden_entrega AndAlso
                                                               x.Cantidad = PolizaAgrupadaCEALSA.Cantidad)
                vRegistrosActualizados = 0

                If Not lPolizaAgrupadaWMS Is Nothing Then

                    If lPolizaAgrupadaWMS.Count > 0 Then

                        bePolizaAgrupadaWMS = lPolizaAgrupadaWMS.Item(0)

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("El número de orden: " & PolizaAgrupadaCEALSA.Correlativo1 & " le corresponde al : " & bePolizaAgrupadaWMS.Agente_aduanal)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        lPedidosVerificados.Add(bePolizaAgrupadaWMS.Agente_aduanal)

                        bePolizaAgrupadaWMS.Correlativo1 = PolizaAgrupadaCEALSA.Correlativo1
                        bePolizaAgrupadaWMS.NoOrdenSalida = PolizaAgrupadaCEALSA.NoOrdenSalida
                        bePolizaAgrupadaWMS.Copoliza = PolizaAgrupadaCEALSA.Copoliza
                        bePolizaAgrupadaWMS.Utilizada = True
                        vRegistrosActualizados = clsLnTablas_relacionadas.Actualizar_Parcial(bePolizaAgrupadaWMS,
                                                                                             clsTransaccion.lConnection,
                                                                                             clsTransaccion.lTransaction)

                        PolizaAgrupadaCEALSA.Utilizada = True
                        vRegistrosActualizados = clsLnTablas_relacionadas.Actualizar_Parcial_CEALSA(PolizaAgrupadaCEALSA,
                                                                                                    clsTransaccion.lConnection,
                                                                                                    clsTransaccion.lTransaction)

                        bePedidoSinPoliza = clsLnTrans_pe_enc.Get_Pedido_Sin_Poliza_By_IdPedidoEnc(bePolizaAgrupadaWMS.Agente_aduanal,
                                                                                                   clsTransaccion.lConnection,
                                                                                                   clsTransaccion.lTransaction)

                        bePedidoSinPoliza.No_Documento_Externo = PolizaAgrupadaCEALSA.Copoliza
                        clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(bePedidoSinPoliza,
                                                                          clsTransaccion.lConnection,
                                                                          clsTransaccion.lTransaction)

                        lblprg.AppendText(vbNewLine)
                        'lblprg.AppendText("Registros actualizados (" & vRegistrosActualizados & ") nùmero de orden: " & BeSinglePolizaPlacas.Noordensalida & " IdPedidoEnc: " & bePolizaAgrupadaWMS.Agente_aduanal)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    Else

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("No hay coincidencia por nombre para el producto: " & PolizaAgrupadaCEALSA.Descripcion)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                Else

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("No hay coincidencia por nombre para el producto: " & PolizaAgrupadaCEALSA.Descripcion)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End If

                Application.DoEvents()

            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuProcesarPolizasExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProcesarPolizasExcel.ItemClick
        Procesar_Polizar_Importadas()
    End Sub

    Private Sub frmProceso_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Set_Indicadores()

    End Sub

    Private Sub Set_Indicadores()

        Dim vBarrasEscaneadas As Integer = 0
        Dim vCantidadPedidosSinPoliza As Integer = 0
        Dim vCantidadPedidosConPoliza As Integer = 0
        Dim vCantidadBarrasEscaneadas As Integer = 0

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Base de datos inicializada correctamente: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            vCantidadPedidosSinPoliza = clsLnvw_pedidos_revision.Get_Cantidad_Pedidos_Sin_Poliza()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Se encontraron: " & vCantidadPedidosSinPoliza & " pedidos sin poliza")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            vCantidadPedidosConPoliza = clsLnvw_pedidos_revision.Get_Cantidad_Pedidos_Con_Poliza()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Se encontraron: " & vCantidadPedidosConPoliza & " pedidos con poliza")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            vCantidadBarrasEscaneadas = clsLnvw_pedidos_revision.Get_Cantidad_Barras_Escaneadas()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Se encontraron: " & vCantidadBarrasEscaneadas & " barras escaneadas.")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        End Try


    End Sub

    Private Sub mnuActualizarPorCoPoliza_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarPorCoPoliza.ItemClick

        Dim clsTransaccion As New clsTransaccion
        Dim lResult As New List(Of clsBeTablas_relacionadas)
        Dim vNoOrden As String = ""
        Dim vIdPedidoEnc As Integer = 0
        Dim BeTmpPoliza As New clsBeTmp_trans_pe_pol
        Dim BePolizaWMS As New clsBeTrans_pe_pol
        Dim vResult As Integer = 0

        Try

            lblprg.Text = "" : Dgrid.DataSource = Nothing

            clsTransaccion.Begin_Transaction()

            lResult = clsLnTablas_relacionadas.Get_All_WMS_Asociadas_By_CoPoliza(clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

            For Each PolizasAsociadas In lResult

                vNoOrden = PolizasAsociadas.Copoliza
                vIdPedidoEnc = PolizasAsociadas.Agente_aduanal

                BeTmpPoliza = clsLnTmp_trans_pe_pol.Get_Single_By_No_Orden(vNoOrden,
                                                                           clsTransaccion.lConnection,
                                                                           clsTransaccion.lTransaction)

                If Not BeTmpPoliza Is Nothing Then

                    BePolizaWMS = clsLnTrans_pe_pol.Get_Single_By_No_Orden(vNoOrden,
                                                                           clsTransaccion.lConnection,
                                                                           clsTransaccion.lTransaction)


                    If Not BePolizaWMS Is Nothing Then

                        BeTmpPoliza.IdOrdenPedidoEnc = vIdPedidoEnc
                        BeTmpPoliza.IdOrdenPedidoPol = BePolizaWMS.IdOrdenPedidoPol

                        vResult += clsLnTrans_pe_pol.Actualizar(BeTmpPoliza,
                                                               clsTransaccion.lConnection,
                                                               clsTransaccion.lTransaction)

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Poliza actualizada: " & PolizasAsociadas.NoOrdenSalida & " para el IdPedido: " & vIdPedidoEnc)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    Else

                        'lblprg.AppendText(vbNewLine)
                        'lblprg.AppendText("Se encontró asociación para la poliza: " & PolizasAsociadas.NoOrdenSalida & " pero no se encontró su homólogo en WMS.")
                        'lblprg.AppendText(vbNewLine)
                        'lblprg.Refresh()
                        'lblprg.SelectionStart = lblprg.TextLength
                        'lblprg.ScrollToCaret()

                    End If

                Else

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("No se encontró asociación para la poliza: " & PolizasAsociadas.NoOrdenSalida)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End If

            Next

            clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Registros actualizados: " & vResult)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub mnuInsertarPolizasNoExistentes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInsertarPolizasNoExistentes.ItemClick

        Dim clsTransaccion As New clsTransaccion
        Dim lResultPendientesInsert As New List(Of clsBeTmp_trans_pe_pol)
        Dim vNoOrden As String = ""
        Dim vIdPedidoEnc As Integer = 0
        Dim BeTablaRelacionada As New clsBeTablas_relacionadas
        Dim BePolizaWMS As New clsBeTrans_pe_pol
        Dim vResult As Integer = 0

        Try

            lblprg.Text = "" : Dgrid.DataSource = Nothing

            clsTransaccion.Begin_Transaction()

            lResultPendientesInsert = clsLnTmp_trans_pe_pol.Get_All_Pendientes_Insert(clsTransaccion.lConnection,
                                                                                      clsTransaccion.lTransaction)

            For Each PolizasPendientes In lResultPendientesInsert

                vNoOrden = PolizasPendientes.Numero_orden

                If vNoOrden = "3293703538" Then
                    Debug.WriteLine("esperate un poco")
                End If

                BeTablaRelacionada = clsLnTablas_relacionadas.Get_Single_By_Copoliza(vNoOrden,
                                                                                    clsTransaccion.lConnection,
                                                                                    clsTransaccion.lTransaction)

                If Not BeTablaRelacionada Is Nothing Then

                    vIdPedidoEnc = BeTablaRelacionada.Agente_aduanal
                    PolizasPendientes.IdOrdenPedidoEnc = vIdPedidoEnc
                    PolizasPendientes.IdOrdenPedidoPol = 1

                    Try

                        vResult += clsLnTrans_pe_pol.Actualizar(PolizasPendientes,
                                                               clsTransaccion.lConnection,
                                                               clsTransaccion.lTransaction)

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Poliza actualizada: " & PolizasPendientes.Numero_orden & " para el IdPedidoEnc: " & vIdPedidoEnc)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    Catch ex As Exception

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Error al procesar poliza: " & PolizasPendientes.Numero_orden & " para el IdPedidoEnc: " & vIdPedidoEnc)
                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(ex.Message)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End Try

                End If

            Next

            clsTransaccion.Commit_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Registros actualizados: " & vResult)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub cmdInsertaTablaRelacionada_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdInsertaTablaRelacionada.ItemClick

        Dim frm As New frmTablaRelacionada

        Try

            frm.ShowDialog()
            frm.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vMsgError)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        End Try

    End Sub

    Private Sub cmdInsertaPolizaTemporal_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdInsertaPolizaTemporal.ItemClick

        Dim frm As New frmPolizaTemporal

        Try

            frm.ShowDialog()
            frm.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vMsgError)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        End Try

    End Sub

    Private Sub cmdImportar_Polizas_Ilegibles_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportar_Polizas_Ilegibles.ItemClick

        Dim clsTransaccion As New clsTransaccion()
        Dim BePoliza As clsBeTmp_trans_pe_pol
        Dim BePolizaExistente As clsBeTmp_trans_pe_pol
        Dim vRegistrosInsertados As Integer
        Dim vTipoRegimenSalida As String

        Try

            clsTransaccion.Begin_Transaction()

            Dim lPolizasIlegibles As New List(Of clsBePolizas_Ilegibles)
            lPolizasIlegibles = clsLnPolizas_Ilegibles.Get_All(clsTransaccion.lConnection,
                                                               clsTransaccion.lTransaction)

            If Not lPolizasIlegibles Is Nothing Then

                lblprg.AppendText("Se encontraron: " & lPolizasIlegibles.Count)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                vRegistrosInsertados = 0

                For Each Poliza In lPolizasIlegibles

                    BePoliza = New clsBeTmp_trans_pe_pol

                    If Poliza.Numero_orden = "3773708678" Then
                        Debug.Print("Hola")
                    End If

                    BePolizaExistente = clsLnTmp_trans_pe_pol.Get_Single_By_No_Orden(Poliza.Numero_orden, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    If BePolizaExistente Is Nothing Then

                        BePoliza.IdOrdenPedidoPol = 1
                        BePoliza.IdOrdenPedidoEnc = 0
                        BePoliza.User_agr = "dts"
                        BePoliza.Fec_agr = Now
                        BePoliza.NoPoliza = ""
                        BePoliza.Pais_procede = ""
                        BePoliza.Total_valoraduana = 0
                        BePoliza.Total_bultos_peso = 0
                        BePoliza.Total_flete = 0
                        BePoliza.Total_usd = 0
                        BePoliza.Dua = Poliza.Dua
                        BePoliza.Fecha_poliza = Now
                        BePoliza.Tipo_cambio = Poliza.Tipo_cambio
                        BePoliza.Total_lineas = 1
                        BePoliza.Total_bultos = 0
                        BePoliza.Total_seguro = 0
                        BePoliza.User_mod = "dts"
                        BePoliza.Fec_mod = Now
                        BePoliza.IdRegimen = 17
                        BePoliza.Codigo_poliza = Poliza.No_poliza
                        BePoliza.Ticket = 0
                        BePoliza.Numero_orden = Poliza.Numero_orden
                        BePoliza.Fecha_aceptacion = Poliza.Fecha_aceptación
                        BePoliza.Fecha_llegada = Poliza.Fecha_llegada
                        BePoliza.Fecha_abordaje = Poliza.Fecha_documento
                        BePoliza.Total_otros = 0
                        BePoliza.Clave_aduana = ""
                        BePoliza.Nit_imp_exp = ""
                        BePoliza.Clase = "10"
                        BePoliza.Mod_transporte = "3"
                        BePoliza.Total_liquidar = 0
                        BePoliza.Total_general = 0


                        vTipoRegimenSalida = Poliza.Regimen.Trim

                        'If (vTipoRegimenSalida.Contains("DI") OrElse vTipoRegimenSalida.Contains("ID")) Then

                        'Else

                        '    lblprg.AppendText(vbNewLine)
                        '    lblprg.AppendText("La poliza escaneada no parece ser una poliza válida de salida: " & vTipoRegimenSalida)
                        '    lblprg.AppendText(vbNewLine)
                        '    lblprg.Refresh()
                        '    lblprg.SelectionStart = lblprg.TextLength
                        '    lblprg.ScrollToCaret()
                        '    Continue For

                        'End If

                        Dim BeRegimen As New clsBeRegimen_fiscal()
                        BeRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(vTipoRegimenSalida,
                                                                                    clsTransaccion.lConnection,
                                                                                    clsTransaccion.lTransaction)

                        If BeRegimen Is Nothing Then

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("El régimen: " & vTipoRegimenSalida & " no esta registrado en Régimen Fiscal, o no es legible desde el archivo de importación")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Continue For

                        End If

                        BePoliza.IdRegimen = BeRegimen.IdRegimen

                        clsLnTmp_trans_pe_pol.Insertar(BePoliza,
                                                       clsTransaccion.lConnection,
                                                       clsTransaccion.lTransaction)

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Se insertó póliza con nùmero de orden: " & Poliza.Numero_orden)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        vRegistrosInsertados += 1

                    End If

                    Application.DoEvents()

                Next

                clsTransaccion.Commit_Transaction()

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Se insertaron " & vRegistrosInsertados & " pólizas")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End If

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(vMsgError)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        End Try

    End Sub
End Class