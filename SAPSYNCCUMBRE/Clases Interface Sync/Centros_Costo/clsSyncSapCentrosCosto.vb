Imports System.Data.SqlClient
Imports SAPbobsCOM

Public Class clsSyncSapCentrosCosto : Inherits clsInterfaceBase

    Private oCompany As Company
    Dim sErrMsg As String = ""
    Dim lRetCode, lErrCode As Long
    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private Function Get_Centros_Costo_From_SAP() As List(Of clsBeSAPCentroCostoCumbre)

        Get_Centros_Costo_From_SAP = Nothing

        Dim lReturnList As New List(Of clsBeSAPCentroCostoCumbre)
        Dim query_sap As String = ""
        Dim sCookie As String = Nothing

        Try

            query_sap = "Select * from [dbo].[@REMARK1] WHERE U_Activo = 'Y' and U_Enviado_WMS = '2'  "

            If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeProducto As clsBeSAPCentroCostoCumbre

                While rs.EoF = False

                    BeProducto = New clsBeSAPCentroCostoCumbre()
                    BeProducto.Code = rs.Fields.Item(0).Value.ToString() 'ItemCode
                    BeProducto.LineId = rs.Fields.Item(1).Value.ToString()
                    BeProducto.U_Activo = rs.Fields.Item("U_Activo").Value.ToString()
                    BeProducto.U_Remak = rs.Fields.Item("U_Remak").Value.ToString()
                    BeProducto.U_Codigo_Remark = rs.Fields.Item("U_Codigo_Remark").Value.ToString()
                    BeProducto.U_Tipo_Documento = rs.Fields.Item("U_Tipo_Documento").Value.ToString()
                    If Not IsDBNull(rs.Fields.Item("U_CecosDimen").Value) Then
                        BeProducto.U_CecosDimen = rs.Fields.Item("U_CecosDimen").Value.ToString()
                    Else
                        BeProducto.U_CecosDimen = Nothing
                    End If

                    lReturnList.Add(BeProducto)

                    rs.MoveNext()

                End While

                Get_Centros_Costo_From_SAP = lReturnList

            Else
                Throw New Exception(sErrMsg)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Public Function Importar_Centros_Costos_From_SAP(ByRef lblprg As RichTextBox,
                                                     ByRef prg As ProgressBar) As Boolean

        Importar_Centros_Costos_From_SAP = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransacion As SqlTransaction = Nothing

        Try


            If MessageBox.Show("¿Actualizar centros de costo desde SAP?", "Alias", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim lCentrosCostoSAP As New List(Of clsBeSAPCentroCostoCumbre)

                clsPublic.Actualizar_Progreso(lblprg, "Consultando centros de costo en tabla intermedia [@REMARK1] WHERE U_Activo = 'Y' and U_Enviado_WMS = '2'")

                lCentrosCostoSAP = Get_Centros_Costo_From_SAP()

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Centros de costo en SAP: {0}", lCentrosCostoSAP.Count))

                If lCentrosCostoSAP.Count > 0 Then

                    Dim BeCentroCostoExistente As clsBeCentro_costo = Nothing
                    Dim BeCentroCosto As clsBeCentro_costo = Nothing
                    Dim vExisteBarra As Boolean = False

                    lConnection.Open() : lTransacion = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransacion)

                    prg.Maximum = lCentrosCostoSAP.Count
                    prg.Visible = True

                    Dim vContador As Integer = 0

                    prg.Value = 0

                    clsPublic.Actualizar_Progreso(lblprg, "Trasladando codigos de barra de producto desde SAP a TOMWMS...")

                    If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then

                        For Each BeSAPCentroCosto As clsBeSAPCentroCostoCumbre In lCentrosCostoSAP

                            BeCentroCostoExistente = New clsBeCentro_costo
                            BeCentroCostoExistente = clsLnCentro_costo.Existe_By_Codigo(BeSAPCentroCosto.U_Codigo_Remark,
                                                                                        lConnection,
                                                                                        lTransacion)

                            If BeCentroCostoExistente Is Nothing Then

                                Try
                                    '#EJC20240409
                                    BeCentroCosto = New clsBeCentro_costo()
                                    BeCentroCosto.IdCentroCosto = clsLnCentro_costo.MaxID(lConnection, lTransacion) + 1
                                    BeCentroCosto.IdEmpresa = BeConfigEnc.Idempresa
                                    BeCentroCosto.Codigo = BeSAPCentroCosto.U_Codigo_Remark
                                    BeCentroCosto.Nombre = BeSAPCentroCosto.U_Remak
                                    BeCentroCosto.Referencia = BeSAPCentroCosto.U_CecosDimen
                                    BeCentroCosto.User_agr = BeConfigEnc.IdUsuario
                                    BeCentroCosto.Fec_agr = Now
                                    BeCentroCosto.Fec_mod = Now
                                    BeCentroCosto.User_mod = BeConfigEnc.IdUsuario
                                    BeCentroCosto.Activo = True

                                    If clsLnCentro_costo.Insertar(BeCentroCosto, lConnection, lTransacion) > 0 Then

                                        If Marcar_Centro_Costo_Sincronizado_SAP_Usuario_Compuesto(BeSAPCentroCosto.Code, BeSAPCentroCosto.LineId) Then

                                            VContadorBitacoraTOMWMS += 1
                                            clsPublic.Actualizar_Progreso(lblprg, "Centro de costo insertado: " & BeSAPCentroCosto.U_Codigo_Remark & " - " & BeSAPCentroCosto.U_Remak)

                                        End If

                                    End If

                                Catch ex As Exception

                                    clsLnLog_error_wms.Agregar_Error(ex.Message)
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el centro de costo: {0} {1}", BeSAPCentroCosto.U_Codigo_Remark, vbNewLine))
                                    Application.DoEvents()

                                End Try

                            Else

                                Try

                                    BeCentroCostoExistente.Codigo = BeSAPCentroCosto.U_Codigo_Remark
                                    BeCentroCostoExistente.Nombre = BeSAPCentroCosto.U_Remak
                                    BeCentroCostoExistente.Referencia = BeSAPCentroCosto.U_CecosDimen
                                    BeCentroCostoExistente.User_mod = BeConfigEnc.IdUsuario
                                    BeCentroCostoExistente.Fec_mod = Now
                                    BeCentroCostoExistente.Activo = True

                                    If clsLnCentro_costo.Actualizar(BeCentroCostoExistente, lConnection, lTransacion) > 0 Then

                                        If Marcar_Centro_Costo_Sincronizado_SAP_Usuario_Compuesto(BeSAPCentroCosto.Code, BeSAPCentroCosto.LineId) Then

                                            VContadorBitacoraTOMWMS += 1
                                            clsPublic.Actualizar_Progreso(lblprg, "Centro de costo actualizado: " & BeSAPCentroCosto.U_Codigo_Remark & " - " & BeSAPCentroCosto.U_Remak)

                                        End If

                                    End If

                                Catch ex As Exception

                                    clsLnLog_error_wms.Agregar_Error(ex.Message)
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo actualizar el centro de costo: {0} {1}", BeSAPCentroCosto.U_Codigo_Remark, vbNewLine))
                                    Application.DoEvents()

                                End Try

                            End If

                            prg.Value = vContador
                            vContador += 1

                        Next

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "ERROR_202404102157: " & sErrMsg)
                    End If

                End If

                If Not lTransacion Is Nothing Then lTransacion.Commit()

                clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso -> " & Now)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos procesados correctamente: {0}", VContadorBitacoraTOMWMS))
                Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            End If


        Catch ex As Exception
            If Not lTransacion Is Nothing Then lTransacion.Rollback()
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar producto a tabla de TOMWMS: {0}", ex.Message))
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
            prg.Visible = False
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Marcar_Centro_Costo_Sincronizado_SAP_Usuario_Compuesto(ByVal code As String, ByVal lineId As Integer) As Boolean

        Dim resultado As Boolean = False

        Try


            ' Crear y preparar el objeto Recordset para la consulta
            Dim oRecordset As Recordset
            oRecordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

            ' Construir la consulta SQL para actualizar el registro basado en la clave compuesta
            Dim query As String = String.Format("UPDATE ""@REMARK1"" SET U_ENVIADO_WMS = '1' WHERE Code = '{0}' AND LineId = {1}", code, lineId)

            ' Ejecutar la consulta de actualización
            oRecordset.DoQuery(query)

            resultado = True

        Catch ex As Exception
            Throw New Exception(String.Format("Error al marcar el centro de costo sincronizado para clave compuesta: {0}", ex.Message))
        End Try

        Return resultado

    End Function




End Class
