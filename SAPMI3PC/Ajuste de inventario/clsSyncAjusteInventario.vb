Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM

Public Class clsSyncAjusteInventario : Inherits clsInterfaceBase
    Implements IDisposable

    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Dim CuentaSAPInventario As String
    Public Sub Sync_Ajustes(ByVal lblprg As RichTextBox,
                            ByRef prg As System.Windows.Forms.ProgressBar)

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Resultado As String = ""

        Try

            CnnLog.Open()

            clsPublic.Actualizar_Progreso(lblprg, "Consultando ajustes pendientes de envío.")

            Dim lAjustesPendEnvio As New List(Of clsBeAjustesMI3)
            lAjustesPendEnvio = clsLnI_nav_transacciones_out.Get_Ajustes_Pendientes_Envio_MI3(Resultado)

            If Not lAjustesPendEnvio Is Nothing Then

                Dim lVistaAjustesPendientesEnvio As New List(Of clsBe_vw_ajustes)
                Dim vDif As Double = 0
                Dim vNoDocumento As String = ""
                Dim vContador As Integer = 0
                Dim BeAjusteDet As New clsBeTrans_ajuste_det
                Dim DetallesEnviados As Integer = 0
                Dim BeFamilia As New clsBeProducto_familia
                Dim vSerieBodega As String = ""
                Dim BeCliente As New clsBeCliente
                Dim Cod_Variante As String = ""
                Dim AjusteDoc As New clsBeTrans_ajuste_det_doc
                Dim MaxIdAjusteDoc As Integer = 0
                Dim vNomenclaturaBase As String = ""
                Dim vCorrelativoActual As Integer = 0
                Dim BeBodega As New clsBeBodega
                Dim vCuentaIngresos As String = ""
                Dim vCuentaEgresos As String = ""

                prg.Maximum = lAjustesPendEnvio.Count

                If lAjustesPendEnvio.Count > 0 Then

                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

                    BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega)
                    'Get_Bodegas_SAP(BeConfigEnc.IdBodega)


                    vCuentaIngresos = BeBodega.Cuenta_Ingreso_Mercancias
                    vCuentaEgresos = BeBodega.Cuenta_Egreso_Mercancias

                    For Each AJ In lAjustesPendEnvio

                        vNoDocumento = Right("000000" & AJ.IdAjusteEnc & AJ.IdAjusteDet, 6)
                        vNoDocumento = "WMS" + vNoDocumento

                        DetallesEnviados = 0

                        If AJ.TipoAjusteWMS = "Ajuste Positivo" Then

                            clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste positivo + WMS:# :" & AJ.IdAjusteEnc & "/" & AJ.IdAjusteDet)

                            Try

                                Dim vResult As String = SBO_Entrada_Mercaderia(AJ.IdAjusteEnc,
                                                                               AJ.Codigo_Producto,
                                                                               AJ.Codigo_Bodega,
                                                                               1,
                                                                               AJ.Cantidad,
                                                                               vNoDocumento,
                                                                               vCuentaIngresos)

                                clsPublic.Actualizar_Progreso(lblprg, "Ajuste: " & AJ.IdAjusteEnc & " procesado correctamente - Doc SAP - > " & vResult)

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                           "Sync_Ajustes",
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajuste a SAP: {0}{1}", vbNewLine, ex.Message))
                                clsPublic.Actualizar_Progreso(lblprg, "Enc ->  " & AJ.IdAjusteEnc)
                                clsPublic.Actualizar_Progreso(lblprg, "Det ->  " & AJ.IdAjusteDet)
                                clsPublic.Actualizar_Progreso(lblprg, "Codigo ->  " & AJ.Codigo_Producto)
                                clsPublic.Actualizar_Progreso(lblprg, "Cantidad ->  " & AJ.Cantidad)

                            End Try

                        ElseIf AJ.TipoAjusteWMS = "Ajuste Negativo" Then

                            clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste negativo - WMS: #: " & AJ.IdAjusteEnc)

                            Try

                                SBO_Salida_Mercaderia(AJ.IdAjusteEnc,
                                                      AJ.Codigo_Producto,
                                                      AJ.Codigo_Bodega,
                                                      1,
                                                      AJ.Cantidad,
                                                      vNoDocumento,
                                                      vCuentaEgresos)

                                clsPublic.Actualizar_Progreso(lblprg, "Ajuste: " & AJ.IdAjusteEnc & " procesado correctamente")

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                           "Sync_Ajustes",
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a SAP: {0}{1}", vbNewLine, ex.Message))
                                clsPublic.Actualizar_Progreso(lblprg, "Enc ->  " & AJ.IdAjusteEnc)
                                clsPublic.Actualizar_Progreso(lblprg, "Det ->  " & AJ.IdAjusteDet)
                                clsPublic.Actualizar_Progreso(lblprg, "Codigo ->  " & AJ.Codigo_Producto)
                                clsPublic.Actualizar_Progreso(lblprg, "Cantidad ->  " & AJ.Cantidad)

                            End Try

                        End If

                        prg.Value = vContador : vContador += 1

                    Next

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes pendientes de envío.")
                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes pendientes de envío.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "Sync_Ajustes",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, CnnLog)

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If Not CnnLog Is Nothing AndAlso CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    Public Function SBO_Entrada_Mercaderia(ByVal IdAjusteEnc As Integer,
                                          ByVal receivedItemCode As String,
                                          ByVal warehouseCode As String,
                                          ByVal standardCost As Double,
                                          ByVal receivedQuantity As Double,
                                          ByVal referenceDoc As String,
                                          ByVal CuentaEntradaMercancia As String) As String

        SBO_Entrada_Mercaderia = ""

        Try

            Conectar_A_SAP(oCompany, lRetCode, sErrMsg)

            If lRetCode <> 0 Then
                Throw New Exception(sErrMsg)
            Else

                Dim grh = TryCast(oCompany.GetBusinessObject(BoObjectTypes.oInventoryGenEntry), Documents)
                grh.DocDate = Date.Today
                grh.JournalMemo = "Entrada Mercaderia:" & referenceDoc
                grh.Comments = "Importado desde WMS documento No : " & referenceDoc.ToString()
                Dim grl = grh.Lines
                grl.ItemCode = receivedItemCode
                grl.UnitPrice = standardCost
                grl.Quantity = receivedQuantity
                grl.WarehouseCode = warehouseCode.Trim()
                grl.AccountCode = CuentaEntradaMercancia '"_SYS00000000532"

                Dim oResultado As Integer
                oResultado = grh.Add()

                If oResultado <> 0 Then
                    Throw New Exception(oCompany.GetLastErrorDescription())
                    Dim entero As Integer = 0
                Else

                    Dim vNoDocSAP As String = oCompany.GetNewObjectKey()
                    clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(IdAjusteEnc, True, vNoDocSAP)
                    SBO_Entrada_Mercaderia = vNoDocSAP

                End If

            End If

        Catch errMsg As Exception
            Throw errMsg
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Sub SBO_Salida_Mercaderia(ByVal IdAjusteEnc As Integer,
                                     ByVal receivedItemCode As String,
                                     ByVal warehouseCode As String,
                                     ByVal standardCost As Double,
                                     ByVal receivedQuantity As Double,
                                     ByVal referenceDoc As String,
                                     ByVal CuentaSalidaMercancia As String)

        Try

            Dim lRetCode As Integer = 0
            Dim errMsg As String = ""
            Dim ErrNo As Integer = 0

            Conectar_A_SAP(oCompany, lRetCode, errMsg)

            If ErrNo <> 0 Then
                Throw New Exception(errMsg)
            Else

                Dim grh = TryCast(oCompany.GetBusinessObject(BoObjectTypes.oInventoryGenExit), Documents)
                grh.DocDate = Date.Today
                grh.JournalMemo = "Salida Mercaderia:" & referenceDoc
                grh.Comments = "Importado desde WMS documento No : " & referenceDoc.ToString()
                Dim grl = grh.Lines
                grl.ItemCode = receivedItemCode
                grl.UnitPrice = standardCost
                grl.Quantity = receivedQuantity
                grl.WarehouseCode = warehouseCode.Trim()
                grl.AccountCode = CuentaSalidaMercancia '"_SYS00000000532"
                Dim oResultado As Integer
                oResultado = grh.Add()

                If oResultado <> 0 Then
                    Throw New Exception(oCompany.GetLastErrorDescription())
                    Dim entero As Integer = 0
                Else
                    clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(IdAjusteEnc, True, referenceDoc)
                End If

            End If

        Catch errMsg As Exception
            Throw errMsg
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Sub
    Private Function Get_Bodegas_SAP(ByVal _Whscode As String) As List(Of clsBeI_nav_bodega)

        Get_Bodegas_SAP = Nothing

        Dim lBodegasWMS As New List(Of clsBeI_nav_bodega)
        Dim BeBodega As New clsBeI_nav_bodega

        Try

            Dim query_sap As String = "SELECT WhsCode,WhsName, TransferAc
                                       FROM OWHS WHERE WhsCode = " & _Whscode & " "

            Conectar_A_SAP(oCompany, lRetCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                While rs.EoF = False

                    CuentaSAPInventario = rs.Fields.Item("TransferAc").Value.ToString()

                    rs.MoveNext()

                End While

            End If

            Return lBodegasWMS

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Private Function Get_Ajustes_SAP(ByVal _Whscode As String) As List(Of clsBeI_nav_bodega)

        Get_Ajustes_SAP = Nothing

        ' Leer un documento de Entrada de Mercancía
        Dim oInventoryEntry As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryGenEntry), Documents)

        Try

            Conectar_A_SAP(oCompany, lRetCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                'rs.DoQuery(query_sap)

                While rs.EoF = False

                    CuentaSAPInventario = rs.Fields.Item("TransferAc").Value.ToString()

                    rs.MoveNext()

                End While

            End If

            'Return lBodegasWMS

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)        
    End Sub
#End Region

End Class