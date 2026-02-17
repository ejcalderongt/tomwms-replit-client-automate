Imports System.Data.SqlClient
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSAPTrasladoStock : Inherits clsInterfaceBase
    Implements IDisposable
    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub
    Private Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String, pEmpresa As pEmpresa, oCompany As Company) As Boolean

        Marcar_PI_Sincronizado_SAP = False

        Try

            If pNoDocumento.StartsWith("K") OrElse pNoDocumento.StartsWith("G") Then
                pNoDocumento = Val(pNoDocumento.Substring(1))
            End If

            Dim updateQuery As String = $"UPDATE OWTQ SET U_Enviado_WMS = '1' WHERE DocEntry = '{pNoDocumento}'"
            Dim rsUpdate As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rsUpdate.DoQuery(updateQuery)

            Marcar_PI_Sincronizado_SAP = True

        Catch ex As Exception
            Throw
        End Try

    End Function
    Private Function Get_Traslados_SAP(ByVal pCodigoBodegaInterface As String,
                                       ByVal pCompany As pEmpresa,
                                       ByVal oCompany As Company,
                                       lConnection As SqlConnection,
                                       lTransaction As SqlTransaction,
                                       Optional ByVal pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As clsBePropietarios = Nothing

        Try

            If BeConfigEnc Is Nothing Then
                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)
            End If

            If BeConfigEnc Is Nothing Then
                Throw New Exception("#ERROR_202401011209: No se pudo obtener la configuración de interface para el Id:  " & BD.Instancia.IdConfiguracionInterface)
            End If

            BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario, lConnection, lTransaction)

            If BePropietario Is Nothing Then
                Throw New Exception("#ERROR_202401011209: No se pudo obtener el propietario asociado a la configuración de interface para el IdPropietario:  " & BeConfigEnc.IdPropietario)
            End If

            Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            Dim SAP_Traslados As String = $"
            SELECT T0.DOCENTRY, T0.DOCNUM, T0.DOCDATE, T0.CARDCODE, T0.CARDNAME, T0.DOCCUR,
                   T0.DOCTOTAL, T0.JRNLMEMO, T0.CANCELED, T0.DOCSTATUS,
                   CASE WHEN T0.DOCTYPE = 'I' THEN 'ARTICULO' ELSE 'SERVICIO' END AS TIPO_ORDEN_VENTA,
                   T1.FromWhsCod AS Codigo_Bodega_Origen, OW1.WhsName AS Nombre_Bodega_Origen,
                   T1.WhsCode AS Codigo_Bodega_Destino, OW2.WhsName AS Nombre_Bodega_Destino,
                   T0.U_ToWhsCode
            FROM OWTQ T0
            INNER JOIN WTQ1 T1 ON T0.DocEntry = T1.DocEntry
            INNER JOIN OWHS OW1 ON T1.FromWhsCod = OW1.WhsCode
            INNER JOIN OWHS OW2 ON T1.WhsCode = OW2.WhsCode
            WHERE T0.DOCSTATUS = 'O' AND T0.U_Enviado_WMS = 2
              AND ((T1.FromWhsCod = '{pCodigoBodegaInterface}' OR T1.WhsCode = '{pCodigoBodegaInterface}')
              OR (T0.U_ToWhsCode = '{pCodigoBodegaInterface}'))
              {(If(pNoDocumentoSAP <> "", " And T0.DocNum = " & pNoDocumentoSAP, ""))}
            ORDER BY T0.DOCENTRY DESC"

            RsEnc.DoQuery(SAP_Traslados)

            While Not RsEnc.EoF
                Dim BePedidoWMS As New clsBeI_nav_ped_traslado_enc With {
                .No = RsEnc.Fields.Item("DOCENTRY").Value,
                .Posting_Date = RsEnc.Fields.Item("DOCDATE").Value,
                .Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value,
                .Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value,
                .Status = 1,
                .Transfer_from_Code = RsEnc.Fields.Item("CODIGO_BODEGA_ORIGEN").Value,
                .Transfer_from_Contact = RsEnc.Fields.Item("JRNLMEMO").Value,
                .Transfer_from_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_ORIGEN").Value,
                .Transfer_to_Code = RsEnc.Fields.Item("CODIGO_BODEGA_DESTINO").Value,
                .Transfer_to_Contact = RsEnc.Fields.Item("CARDNAME").Value,
                .Transfer_to_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_DESTINO").Value,
                .Transfer_to_CodeField = If(IsDBNull(RsEnc.Fields.Item("U_ToWhsCode").Value), "", RsEnc.Fields.Item("U_ToWhsCode").Value),
                .Product_Owner_Code = BePropietario.Codigo,
                .Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value,
                .Company_Code = pCompany.ToString(),
                .Document_Type = tTipoDocumentoSalida.Transferencia_Interna_WMS,
                .Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)()
            }

                If BePedidoWMS.Transfer_to_CodeField = "" Then
                    Throw New Exception("Error_250518: No se definió la bodega destino (final) en documento de SAP.")
                End If

                Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                Dim query_det As String = $"
                SELECT T1.U_CodWMS AS ITEMCODE, T0.DSCRIPTION, T0.QUANTITY, T0.PRICE,
                       T0.LINETOTAL, T0.VATSUM, T0.DOCENTRY, T0.WHSCODE, T0.LINENUM,
                       T0.UomCode AS UNIDAD_MEDIDA
                FROM WTQ1 T0
                INNER JOIN OITM T1 ON T1.ItemCode = T0.ItemCode
                WHERE T0.DOCENTRY = '{BePedidoWMS.No}'"

                RsDet.DoQuery(query_det)

                While Not RsDet.EoF
                    Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det With {
                    .NoEnc = BePedidoWMS.No,
                    .No = clsLnTrans_pe_det.MaxID() + 1,
                    .Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString(),
                    .Line_No = RsDet.Fields.Item("LINENUM").Value,
                    .Shipment_Date = Date.Now,
                    .Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value),
                    .Description = RsDet.Fields.Item("dscription").Value.ToString(),
                    .Unit_of_Measure_Code = "UN",
                    .Status = 1,
                    .Variant_Code = If(RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString() = "Unidad", Nothing, RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()),
                    .Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString(),
                    .Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
                }

                    BePedidoWMS.Lineas_Detalle.Add(BePedidoDetWMS)
                    RsDet.MoveNext()
                End While

                lPedidosCliente.Add(BePedidoWMS)

                RsEnc.MoveNext()

            End While

            Return lPedidosCliente

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Function Importar_Solicitudes_Traslados_SAP(ByRef lblprg As RichTextBox,
                                                       ByRef prg As Windows.Forms.ProgressBar,
                                                       Optional ByVal ForzarEjecucion As Boolean = False,
                                                       Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                       Optional ByVal pNoDocumentoTrasladoSAP As String = "") As Boolean
        Importar_Solicitudes_Traslados_SAP = False

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Resultado As String = ""

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido traslado") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Procesar_Solicitud_Traslado_SAP(BeConfigEnc, lblprg, prg, CnnLog, pNoDocumentoTrasladoSAP) Then
                Exit Function
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                      "",
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet, CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de traslado a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))

            Throw

        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

    End Function
    Private Shared Function Inserta_Cliente_SAP(ByVal pCodigo As String,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction,
                                                ByVal pCompany As pEmpresa,
                                                ByVal oCompany As Company) As Boolean

        Dim rs As Recordset = Nothing

        Try

            rs = ObtenerDatosClienteDesdeSAP(oCompany, pCodigo)
            If rs.EoF Then Return False

            While Not rs.EoF
                Dim cliente = ConstruirClienteDesdeRecordset(rs, lConnection, lTransaction)
                clsLnCliente.Insertar(cliente, lConnection, lTransaction)
                InsertarClienteEnBodegas(cliente, lConnection, lTransaction)
                MarcarClienteComoEnviadoSAP(oCompany, pCodigo)
                rs.MoveNext()
            End While

            Return True

        Catch ex As Exception
            Throw New Exception("No se pudo insertar el cliente nuevo proveniente de SAP: " & ex.Message)
        Finally
            If rs IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        End Try
    End Function
    Public Function Procesar_Solicitud_Traslado_SAP(ByVal BeI_nav_config_enc As clsBeI_nav_config_enc,
                                                    ByRef lblprg As RichTextBox,
                                                    ByRef prg As ProgressBar,
                                                    ByRef cnnLog As SqlConnection,
                                                    Optional ByVal pNoDocumento As String = "") As Boolean
        Procesar_Solicitud_Traslado_SAP = False

        Dim Resultado As String = ""
        Dim clsTrans As New clsTransaccion

        Try

            clsTrans.Begin_Transaction()

            Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeI_nav_config_enc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)

            If BeBodega Is Nothing Then
                Throw New Exception("ERROR_202311271751: Error no se pudo obtener el objeto de bodega asociado a la configuraciòn de interface: " & BeI_nav_config_enc.Idbodega)
            End If

            Dim procesado As Boolean = Procesar_Documentos(BeBodega.Codigo, pEmpresa.Killios, pNoDocumento, BeI_nav_config_enc, lblprg, Resultado, clsTrans)

            If Not procesado Then
                procesado = Procesar_Documentos(BeBodega.Codigo, pEmpresa.Garesa, pNoDocumento, BeI_nav_config_enc, lblprg, Resultado, clsTrans)
            End If

            clsTrans.Commit_Transaction()

            Procesar_Solicitud_Traslado_SAP = procesado

        Catch ex As Exception
            clsTrans.RollBack_Transaction()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            Throw

        Finally
            clsTrans.Close_Conection()
        End Try

    End Function
    Private Function Procesar_Documentos(ByVal codigoBodega As String,
                                         ByVal empresa As pEmpresa,
                                         ByVal pNoDocumento As String,
                                         ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                         ByVal lblprg As RichTextBox,
                                         ByRef Resultado As String,
                                         ByVal clsTrans As clsTransaccion) As Boolean

        Procesar_Documentos = False

        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(empresa)
            Dim oCompany As Company = conn.Company

            Dim solicitudes As List(Of clsBeI_nav_ped_traslado_enc) = Get_Traslados_SAP(codigoBodega, empresa, oCompany, clsTrans.lConnection, clsTrans.lTransaction, pNoDocumento)
            Dim pBePedidoEnc As New clsBeTrans_pe_enc
            Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
            Dim PedidoClienteExistente As New clsBeTrans_pe_enc

            If solicitudes.Count = 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay documentos para:" & empresa.ToString())
                Return False
            End If

            For Each solicitud In solicitudes

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando solicitud de traslado SAP (OWTQ): {solicitud.No}{vbNewLine}")

                Dim vEmpresa As pEmpresa = [Enum].Parse(GetType(pEmpresa), solicitud.Company_Code)

                Validar_Cliente_WMS(solicitud.Transfer_to_Code, lblprg, clsTrans, vEmpresa, oCompany)

                Dim origenEsWMS As Boolean = clsLnBodega_area.Existe_Codigo_By_IdBodega(solicitud.Transfer_to_Code, BeConfigEnc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)
                Dim destinoEsWMS As Boolean = clsLnBodega_area.Existe_Codigo_By_IdBodega(solicitud.Transfer_to_Code, BeConfigEnc.Idbodega, clsTrans.lConnection, clsTrans.lTransaction)

                Dim debeProcesar As Boolean = Not destinoEsWMS OrElse Not origenEsWMS OrElse (origenEsWMS AndAlso destinoEsWMS)

                If debeProcesar Then
                    Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(solicitud, lblprg, clsTrans.lConnection, clsTrans.lTransaction)
                    If pedidoEnc IsNot Nothing AndAlso Marcar_PI_Sincronizado_SAP(solicitud.No, empresa, oCompany) Then
                        clsPublic.Actualizar_Progreso(lblprg, Resultado)
                        Return True
                    End If
                End If

                Dim destinoWMS As clsBeBodega = clsLnBodega.GetSingle_By_Codigo(solicitud.Transfer_to_Code, clsTrans.lConnection, clsTrans.lTransaction)
                If destinoWMS IsNot Nothing Then
                    Dim pedidoEnc As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(solicitud, lblprg, clsTrans.lConnection, clsTrans.lTransaction)
                    If pedidoEnc IsNot Nothing AndAlso Marcar_PI_Sincronizado_SAP(solicitud.No, empresa, oCompany) Then
                        clsPublic.Actualizar_Progreso(lblprg, Resultado)
                        Return True
                    End If
                End If
            Next

            Return False

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
        End Try

    End Function
    Private Sub Validar_Cliente_WMS(ByVal codigoCliente As String, ByVal lblprg As RichTextBox, ByVal clsTrans As clsTransaccion, pCompany As pEmpresa, oCompany As Company)
        Dim clienteWMS As clsBeCliente = clsLnCliente.Existe(codigoCliente, clsTrans.lConnection, clsTrans.lTransaction)
        If clienteWMS Is Nothing Then
            Inserta_Cliente_SAP(codigoCliente, clsTrans.lConnection, clsTrans.lTransaction, pCompany, oCompany)
            clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & codigoCliente & " No existía en WMS y fue insertado.")
        End If
    End Sub
    Private Shared Function ObtenerDatosClienteDesdeSAP(oCompany As Company, pCodigo As String) As Recordset

        Dim query As String = "
        SELECT TOP 1 
            T0.CARDCODE AS CODIGO,
            T0.CARDNAME AS NOMBRE_COMERCIAL,
            T0.Phone1, 
            'TEST' AS CONTACTO,
            T0.ADDID AS NIT, 
            T0.Address AS DIRECCION, 
            T0.E_Mail 
        FROM OCRD T0 
        WHERE T0.CARDTYPE = 'C' 
          AND T0.CARDCODE = '" & pCodigo & "'"

        Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        rs.DoQuery(query)
        Return rs

    End Function
    Private Shared Sub MarcarClienteComoEnviadoSAP(oCompany As Company, codigoCliente As String)
        Dim query As String = $"UPDATE OCRD SET U_Enviado_WMS = '1' WHERE CardCode = '{codigoCliente}'"
        Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        rs.DoQuery(query)
        Runtime.InteropServices.Marshal.ReleaseComObject(rs)
    End Sub
    Private Shared Sub InsertarClienteEnBodegas(cliente As clsBeCliente, conn As SqlConnection, trx As SqlTransaction)

        Try

            Dim bodegas = clsLnBodega.Get_All_By_IdEmpresa(BeConfigEnc.Idempresa, conn, trx)

            For Each bodega In bodegas
                Dim clienteBodega As New clsBeCliente_bodega With {
                .IdClienteBodega = clsLnCliente_bodega.MaxID(conn, trx) + 1,
                .IdCliente = cliente.IdCliente,
                .IdBodega = bodega.IdBodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now,
                .Cliente = cliente
            }

                clsLnCliente_bodega.Insertar_From_Interface(clienteBodega, conn, trx)
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Shared Function ConstruirClienteDesdeRecordset(rs As Recordset, conn As SqlConnection, trx As SqlTransaction) As clsBeCliente
        Return New clsBeCliente With {
        .IdCliente = clsLnCliente.MaxID(conn, trx) + 1,
        .IdPropietario = BeConfigEnc.IdPropietario,
        .Codigo = rs.Fields.Item("CODIGO").Value.ToString(),
        .Nombre_comercial = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString(),
        .Sistema = True,
        .Activo = True,
        .IdEmpresa = BeConfigEnc.Idempresa,
        .Nit = rs.Fields.Item("NIT").Value.ToString(),
        .IdTipoCliente = 1,
        .Es_bodega_recepcion = False,
        .Es_Bodega_Traslado = False}
    End Function

End Class