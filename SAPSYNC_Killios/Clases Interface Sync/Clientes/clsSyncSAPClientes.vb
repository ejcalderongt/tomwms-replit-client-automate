Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM
Public Class clsSyncSAPClientes : Inherits clsInterfaceBase

    Private Shared oCompany As Company
    Private Shared lRetCode, lErrCode As Long
    Private Shared sErrMsg As String = ""

    Private Shared Function Importar_Clientes_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                       ByRef prg As ProgressBar,
                                                                       ByRef cnnLog As SqlConnection) As Boolean
        Importar_Clientes_Desde_SAP_A_TablaIntermedia = False

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Consultando clientes en SAP.")

            Get_Clientes_SAP(lblprg, pEmpresa.Killios)

            Get_Clientes_SAP(lblprg, pEmpresa.Garesa)

            Importar_Clientes_Desde_SAP_A_TablaIntermedia = True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar proveedores desde SAP a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        End Try

    End Function

    Public Shared Function Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                               ByRef prg As ProgressBar,
                                                                               Optional ByVal ForzarEjecucion As Boolean = False,
                                                                               Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing

        Try

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Clientes_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Clientes_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = True

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error al insertar Proveedor a tabla de TOMWMS: {0}", ex.Message)
            Throw ex
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
            prg.Visible = False
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Shared Function Marcar_Cliente_Sincronizado_SAP(ByVal pCodigoProveedor As String) As Boolean

        Marcar_Cliente_Sincronizado_SAP = False

        Try

            If oCompany.Connected Then

                If lRetCode <> 0 Then
                    oCompany.GetLastError(lErrCode, sErrMsg)
                    Throw New Exception(sErrMsg)
                Else
                    Dim oBusinessPartnerSBO As BusinessPartners = CType(oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners), BusinessPartners)

                    If oBusinessPartnerSBO.GetByKey(pCodigoProveedor) Then
                        oBusinessPartnerSBO.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                        oBusinessPartnerSBO.Update()
                        Marcar_Cliente_Sincronizado_SAP = True
                    End If

                End If

            End If

        Catch ex As Exception
            If ex.Message.Contains("Invalid field name") Then
                Throw New Exception("El campo U_Enviado_WMS no existe en la tabla de socios de negocio (oBusinessPartners) ")
            Else
                Throw
            End If
        End Try

    End Function

    Private Shared Function Get_Clientes_SAP(lblprg As RichTextBox, Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As Boolean

        Get_Clientes_SAP = False

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios
        Dim clstrans As New clsTransaccion
        Dim vCodigoClienteSAP As String = ""
        Dim vClientesProcesados As Integer = 0

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg, pCompany)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim query_sap As String

                query_sap = "SELECT
                             T0.CARDCODE AS CODIGO,
                             T0.CARDNAME AS NOMBRE_COMERCIAL,
                             T0.Phone1,
                             T0.CntctPrsn AS CONTACTO,
                             T0.AddId AS NIT,
                             ISNULL(T1.Street, '') + ' ' + ISNULL(T1.City, '') AS DIR1,
                             T1.Address AS DIRECCION,
                             T0.E_Mail
                             FROM OCRD T0
                             LEFT JOIN CRD1 T1 ON T1.CardCode = T0.CardCode AND T1.Address = 'ENTREGA'
                             WHERE T0.CardType = 'C'"

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                clstrans.Begin_Transaction()

                Dim BeCliente As New clsBeCliente
                Dim BeClienteExistente As New clsBeCliente
                Dim count As Integer = 0
                Dim lBodegas As New List(Of clsBeBodega)
                lBodegas = clsLnBodega.Get_All(clstrans.lConnection, clstrans.lTransaction)

                If Not rs.EoF Then
                    rs.MoveFirst()
                    Do While Not rs.EoF
                        count += 1
                        rs.MoveNext()
                    Loop
                    rs.MoveFirst()
                End If

                clsPublic.Actualizar_Progreso(lblprg, "Clientes en: " & pCompany.ToString() & " -> " & count)

                While Not rs.EoF

                    vCodigoClienteSAP = rs.Fields.Item("CODIGO").Value.ToString()

                    clsPublic.Actualizar_Progreso(lblprg, "Procesando: " & vCodigoClienteSAP)

                    BeClienteExistente = clsLnCliente.Existe(vCodigoClienteSAP, clstrans.lConnection, clstrans.lTransaction)

                    If BeClienteExistente Is Nothing Then

                        BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(lBodegas.FirstOrDefault.IdBodega, clstrans.lConnection, clstrans.lTransaction)

                        BeCliente.IdCliente = clsLnCliente.MaxID(clstrans.lConnection, clstrans.lTransaction) + 1
                        BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                        BeCliente.Codigo = pCompany.ToString().Substring(0, 1) & vCodigoClienteSAP
                        BeCliente.Nombre_comercial = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString()
                        BeCliente.Sistema = True
                        BeCliente.Activo = True
                        BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                        BeCliente.Nit = rs.Fields.Item("NIT").Value.ToString()
                        BeCliente.IdTipoCliente = 1
                        BeCliente.Es_bodega_recepcion = False
                        BeCliente.Es_Bodega_Traslado = False
                        BeCliente.Direccion = rs.Fields.Item("DIR1").Value.ToString()
                        BeCliente.Codigo_Empresa_ERP = pCompany.ToString()
                        BeCliente.Nombre_contacto = rs.Fields.Item("CONTACTO").Value.ToString()

                        Try

                            clsLnCliente.Insertar(BeCliente, clstrans.lConnection, clstrans.lTransaction)

                            For Each Bod In lBodegas

                                BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(Bod.IdBodega, clstrans.lConnection, clstrans.lTransaction)

                                Dim BeClienteBodega As New clsBeCliente_bodega
                                BeClienteBodega = New clsBeCliente_bodega()
                                BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(clstrans.lConnection, clstrans.lTransaction) + 1
                                BeClienteBodega.IdCliente = BeCliente.IdCliente
                                BeClienteBodega.IdBodega = Bod.IdBodega
                                BeClienteBodega.Activo = True
                                BeClienteBodega.User_agr = BeConfigEnc.IdUsuario
                                BeClienteBodega.User_mod = BeConfigEnc.IdUsuario
                                BeClienteBodega.Fec_agr = Now
                                BeClienteBodega.Fec_mod = Now
                                BeClienteBodega.Cliente = BeCliente

                                clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                            clstrans.lConnection,
                                                                            clstrans.lTransaction)

                            Next

                        Catch ex As Exception
                            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                        End Try

                    Else

                        BeCliente = New clsBeCliente()
                        BeCliente.IdCliente = BeClienteExistente.IdCliente
                        BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                        BeCliente.Codigo = pCompany.ToString().Substring(0, 1) & vCodigoClienteSAP
                        BeCliente.Nombre_comercial = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString()
                        BeCliente.Sistema = True
                        BeCliente.Activo = True
                        BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                        BeCliente.Nit = rs.Fields.Item("NIT").Value.ToString()
                        BeCliente.IdTipoCliente = 1
                        BeCliente.Es_bodega_recepcion = False
                        BeCliente.Es_Bodega_Traslado = False
                        BeCliente.Direccion = rs.Fields.Item("DIR1").Value.ToString()
                        BeCliente.Codigo_Empresa_ERP = pCompany.ToString()
                        BeCliente.Nombre_contacto = rs.Fields.Item("CONTACTO").Value.ToString()

                        Try

                            If Not BeCliente.Nombre_comercial = BeClienteExistente.Nombre_comercial Then
                                clsLnCliente.Actualizar(BeCliente, clstrans.lConnection, clstrans.lTransaction)
                                clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & BeClienteExistente.Nombre_comercial & " Se actualizó a: " & BeCliente.Nombre_comercial)
                            End If

                        Catch ex As Exception
                            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                        End Try

                    End If

                    Dim oBusinessPartnerSBO As BusinessPartners = CType(oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners), SAPbobsCOM.BusinessPartners)

                    If oBusinessPartnerSBO.GetByKey(vCodigoClienteSAP) Then
                        oBusinessPartnerSBO.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                        'oBusinessPartnerSBO.UserFields.Fields.Item("U_Observaciones").Value = BeCliente.IdCliente.ToString()
                        If oBusinessPartnerSBO.Update() <> 0 Then
                            Throw New Exception("Error al actualizar UDFs del cliente en SAP: " & oCompany.GetLastErrorDescription())
                        End If
                    End If

                    Get_Clientes_SAP = True

                    rs.MoveNext()

                    vClientesProcesados += 1

                End While

                clstrans.Commit_Transaction()

            End If

            clsPublic.Actualizar_Progreso(lblprg, vClientesProcesados & " Clientes procesados correctamente para: " & pCompany.ToString())

        Catch ex As Exception
            clstrans.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente nuevo proviniente de SAP: " & ex.Message)
        Finally
            Desconectar_SAP(oCompany)
            clstrans.Close_Conection()
        End Try

    End Function

End Class