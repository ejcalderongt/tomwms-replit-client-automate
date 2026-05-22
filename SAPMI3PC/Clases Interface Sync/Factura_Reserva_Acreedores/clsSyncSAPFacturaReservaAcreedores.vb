Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports SAPbobsCOM

Public Class clsSyncSAPFacturaReservaAcreedores : Inherits clsInterfaceBase
    Implements IDisposable

    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub

    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""
    Private VContadorBitacoraTOMWMS As Integer = 0
    Private VContadorBitacoraIntermedia As Integer = 0
    Public Function Get_All_Facturas_Reserva() As List(Of clsBeI_nav_ped_compra_enc)

        Dim BePedidoCompraEnc As New clsBeI_nav_ped_compra_enc
        Dim LBePedidoCompraEnc As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det()
        Dim vNoLinea As Integer = 0
        Dim vNoLineaBase As Integer = 0

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim oRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                Dim queryEncabezado As String = $"SELECT T0.DOCENTRY,
                                            T0.DOCNUM,T0.DOCDATE,  
                                            T0.CARDCODE,
                                            T0.CARDNAME,
                                            T0.DOCCUR,
                                            T0.DOCTOTAL,
                                            T0.JRNLMEMO,
                                            T0.CANCELED,T0.DOCSTATUS,  
                                            CASE WHEN T0.DOCTYPE = 'I'THEN 'ARTICULO'    
                                            ELSE 'SERVICIO'    
                                            END AS TIPO_ORDEN_VENTA,
                                            (SELECT TOP 1 D0.WhsCode
                                            FROM PCH1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) AS BODEGA,
                                            T0.COMMENTS,
                                            T0.NumAtCard,
                                            T0.U_Es_ImportacionWMS
                                            FROM OPCH T0 
                                            WHERE DOCSTATUS = 'O' 
                                            AND CreateDate >= '2020-10-09 00:00:00.000' 
                                            AND U_EnviadoWMS = 2 
                                            AND (SELECT TOP 1 D0.WhsCode
                                            FROM PCH1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) IS NOT NULL 
                                                ORDER BY t0.DOCENTRY DESC "

                oRecSet.DoQuery(queryEncabezado)

                If Not oRecSet.EoF Then

                    While oRecSet.EoF = False

                        BePedidoCompraEnc = New clsBeI_nav_ped_compra_enc()
                        BePedidoCompraEnc.No = oRecSet.Fields.Item("DOCNUM").Value
                        BePedidoCompraEnc.Posting_Date = Convert.ToDateTime(oRecSet.Fields.Item("DOCDATE").Value)
                        BePedidoCompraEnc.Order_Date = Convert.ToDateTime(oRecSet.Fields.Item("DOCDATE").Value)
                        BePedidoCompraEnc.Document_Date = Convert.ToDateTime(oRecSet.Fields.Item("DOCDATE").Value)
                        BePedidoCompraEnc.Expected_Receipt_Date = Convert.ToDateTime(oRecSet.Fields.Item("DOCDATE").Value)
                        BePedidoCompraEnc.Status = 1
                        BePedidoCompraEnc.Buy_From_Vendor_No = oRecSet.Fields.Item("CARDCODE").Value.ToString()
                        BePedidoCompraEnc.Buy_From_Vendor_Name = oRecSet.Fields.Item("CARDNAME").Value.ToString()
                        BePedidoCompraEnc.Is_Internal_Transfer = False
                        BePedidoCompraEnc.Location_Code = Convert.ToString(oRecSet.Fields.Item("BODEGA").Value)
                        BePedidoCompraEnc.Vendor_Invoice_No = Convert.ToString(oRecSet.Fields.Item("DOCENTRY").Value).ToString()
                        BePedidoCompraEnc.Posting_Description = oRecSet.Fields.Item("COMMENTS").Value.ToString()
                        BePedidoCompraEnc.Product_Owner_Code = BeConfigEnc.IdPropietario
                        'BePedidoCompraEnc.Vendor_Invoice_No = oRecSet.Fields.Item("NUMATCARD").Value.ToString()
                        BePedidoCompraEnc.Document_Type = oRecSet.Fields.Item("U_ES_IMPORTACIONWMS").Value.ToString()

                        If BePedidoCompraEnc.Vendor_Invoice_No = "" Then
                            BePedidoCompraEnc.Vendor_Invoice_No = BePedidoCompraEnc.No.ToString()
                        End If

                        ' Consulta para obtener los detalles de la factura de reserva de acreedores
                        Dim oDetailsRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                        Dim query_det As String = "SELECT T0.ITEMCODE,
                                                    T0.DSCRIPTION,
                                                    T0.QUANTITY,
                                                    T0.PRICE,
                                                    T0.LINETOTAL,
                                                    T0.VATSUM,
                                                    T0.DOCENTRY,
                                                    T0.WHSCODE,
                                                    T0.WHSCODE,
                                                    T0.OPENCREQTY AS CANTIDAD_PENDIENTE,
                                                    T0.BASELINE,
                                                    T0.LINENUM,
                                                    T1.U_Um_Prod AS UNIDAD_MEDIDA  
                                                    FROM PCH1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode 
                                                    WHERE T0.DOCENTRY = '" & BePedidoCompraEnc.Vendor_Invoice_No & "' " &
                                                            "AND T0.LINESTATUS = 'O' "
                        oDetailsRecSet.DoQuery(query_det)

                        While Not oDetailsRecSet.EoF

                            BePedidoDetWMS = New clsBeI_nav_ped_compra_det()
                            BePedidoDetWMS.NoEnc = oDetailsRecSet.Fields.Item("DOCENTRY").Value.ToString()
                            BePedidoDetWMS.No = oDetailsRecSet.Fields.Item("ITEMCODE").Value.ToString()

                            vNoLineaBase = IIf(IsDBNull(oDetailsRecSet.Fields.Item("BASELINE").Value.ToString()), 0, oDetailsRecSet.Fields.Item("BASELINE").Value.ToString())
                            vNoLinea = IIf(IsDBNull(oDetailsRecSet.Fields.Item("LINENUM").Value.ToString()), 0, oDetailsRecSet.Fields.Item("LINENUM").Value.ToString())

                            BePedidoDetWMS.Line_No = Val(vNoLinea)
                            BePedidoDetWMS.Planed_Receipt_Date = Date.Now()
                            BePedidoDetWMS.Quantity = Convert.ToDecimal(oDetailsRecSet.Fields.Item("CANTIDAD_PENDIENTE").Value)
                            BePedidoDetWMS.Quantity_Received = 0
                            BePedidoDetWMS.Description = oDetailsRecSet.Fields.Item("DSCRIPTION").Value.ToString()
                            BePedidoDetWMS.Unit_of_Measure_Code = (oDetailsRecSet.Fields.Item("UNIDAD_MEDIDA").Value.ToString())
                            BePedidoDetWMS.Type = 2
                            BePedidoDetWMS.Variant_Code = Nothing
                            BePedidoDetWMS.Location_Code = oDetailsRecSet.Fields.Item("WHSCODE").Value.ToString()
                            BePedidoCompraEnc.Lineas_Detalle.Add(BePedidoDetWMS)
                            oDetailsRecSet.MoveNext()

                        End While

                        LBePedidoCompraEnc.Add(BePedidoCompraEnc)

                        oRecSet.MoveNext()

                    End While

                End If

            End If

        Catch ex As Exception
            Throw New Exception($"Error en {MethodBase.GetCurrentMethod.Name}: {ex.Message}")
        Finally
            Desconectar_SAP(oCompany)
        End Try

        Return LBePedidoCompraEnc

    End Function

    Public Function Importar_Facturas_Acreedores_A_Tabla_Intermedia(ByVal lblprg As RichTextBox,
                                                                    ByRef prg As Windows.Forms.ProgressBar,
                                                                    ByRef cnnLog As SqlConnection) As Boolean

        Importar_Facturas_Acreedores_A_Tabla_Intermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResult As String = ""
        Dim vOutBeTransOCEnc As New clsBeTrans_oc_enc

        Try
            clsPublic.Actualizar_Progreso(lblprg, "Procesando facturas en la tabla intermedia TOMWMS.")

            Dim lFacturas As New List(Of clsBeI_nav_ped_compra_enc)
            lFacturas = Get_All_Facturas_Reserva()

            BeNavEjecucionRes.Registros_ws = lFacturas.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            ' Borrar tablas intermedias
            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) AndAlso
                clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                For Each BeINavPedCompra In lFacturas

                    clsPublic.Actualizar_Progreso(lblprg, vbTab & $"Procesando Factura Reserva Acreedores: {BeINavPedCompra.No}")

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra, vOutBeTransOCEnc, vResult) Then
                        Marcar_FRES_Sincronizado_SAP(BeINavPedCompra.No)
                    End If

                    clsPublic.Actualizar_Progreso(lblprg, vResult)
                Next

            End If

            lTransaction.Commit()
            Importar_Facturas_Acreedores_A_Tabla_Intermedia = True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar facturas a intermedia: {ex.Message}")

            Throw ex

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Public Function Insertar_Facturas_Acreedores_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                ByRef prg As Windows.Forms.ProgressBar,
                                                                Optional ByVal ForzarEjecucion As Boolean = False,
                                                                Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Facturas_Acreedores_A_Tabla_TOMWMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing

        Try

            If Not ForzarEjecucion Then
                If Not Ejecutar_Interfaz("Factura Reserva Acreedores") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento.")
                    Exit Function
                End If
            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc =0' clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = 0'clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then
                If Not Importar_Facturas_Acreedores_A_Tabla_Intermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If
            Else
                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface facturas de reserva de acreedores.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Facturas_Acreedores_A_Tabla_Intermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If
            End If

            lTransInterface.Commit()

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, $"-> Fin de proceso, tiempo transcurrido: {difSegundos} segundo(s)")

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS

            BeNavEjecucionRes.Exitosa = (VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS)
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

            Insertar_Facturas_Acreedores_A_Tabla_TOMWMS = True

        Catch ex As Exception
            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, CnnLog)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar facturas a tabla TOMWMS: {ex.Message}")

            Throw New Exception($" (M) {MethodBase.GetCurrentMethod.Name()} {ex.Message}")

        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try
    End Function

    Public Function Marcar_FRES_Sincronizado_SAP(ByVal pNoDocumento As String) As Boolean

        Marcar_FRES_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim oFacturaResSBO As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseInvoices), Documents)

                If oFacturaResSBO.GetByKey(pNoDocumento) Then

                    Try

                        oFacturaResSBO.UserFields.Fields.Item("U_EnviadoWMS").Value = "1"
                        oFacturaResSBO.Update()

                    Catch e As Exception
                        Console.WriteLine(e.Message)
                    End Try

                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
