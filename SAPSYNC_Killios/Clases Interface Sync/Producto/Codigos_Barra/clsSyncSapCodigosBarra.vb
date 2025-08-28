Imports System.Data.SqlClient
Imports SAPbobsCOM

Public Class clsSyncSapCodigosBarra : Inherits clsInterfaceBase

    Private oCompany As Company
    Dim sErrMsg As String = ""
    Dim lRetCode, lErrCode As Long
    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private Function Get_Codigos_Barra_Productos_From_SAP() As List(Of clsBeI_nav_producto)

        Get_Codigos_Barra_Productos_From_SAP = Nothing

        Dim lReturnList As New List(Of clsBeI_nav_producto)
        Dim lBodegasWMS As New List(Of clsBeI_nav_bodega)
        Dim query_sap As String = ""
        Dim sCookie As String = Nothing

        Try

            query_sap = "SELECT OITM.U_CodWMS, BcdCode
                         FROM " & BD.Instancia.SAP_COMPANY_DB & ".dbo.OBCD INNER JOIN " & BD.Instancia.SAP_COMPANY_DB & ".dbo.OITM ON OBCD.ItemCode = OITM.ItemCode
                         WHERE U_CodWMS is not null
                         UNION
                         SELECT OITM.U_CodWMS, BcdCode
                         FROM " & BD.Instancia.SAP_COMPANY_DB2 & ".dbo.OBCD INNER JOIN " & BD.Instancia.SAP_COMPANY_DB & ".dbo.OITM ON OBCD.ItemCode = OITM.ItemCode
                         WHERE U_CodWMS is not null "

            If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeProducto As clsBeI_nav_producto

                While rs.EoF = False

                    BeProducto = New clsBeI_nav_producto()
                    BeProducto.No = rs.Fields.Item(0).Value.ToString() 'ItemCode
                    BeProducto.Item_Tracking_Code = rs.Fields.Item(1).Value.ToString()
                    lReturnList.Add(BeProducto)
                    rs.MoveNext()

                End While

                Get_Codigos_Barra_Productos_From_SAP = lReturnList

            Else
                Throw New Exception(sErrMsg)
            End If

        Catch ex As Exception
            Throw
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Codigos_Barra_Productos(ByRef lblprg As RichTextBox,
                                                     ByRef prg As ProgressBar) As Boolean

        Importar_Codigos_Barra_Productos = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try


            If MessageBox.Show("¿Actualizar códigos de barra de producto desde SAP?", "Alias", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim lProductosFromSAP As New List(Of clsBeI_nav_producto)

                clsPublic.Actualizar_Progreso(lblprg, "Consultando productos en tabla intermedia ")

                lProductosFromSAP = Get_Codigos_Barra_Productos_From_SAP()

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos en tabla intermedia: {0}", lProductosFromSAP.Count))

                If lProductosFromSAP.Count > 0 Then

                    Dim BeProductoExistente As clsBeProducto = Nothing
                    Dim BeProductoCodigoBarra As clsBeProducto_codigos_barra = Nothing
                    Dim vExisteBarra As Boolean = False

                    lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

                    prg.Maximum = lProductosFromSAP.Count
                    prg.Visible = True

                    Dim vContador As Integer = 0

                    prg.Value = 0

                    clsPublic.Actualizar_Progreso(lblprg, "Trasladando codigos de barra de producto desde SAP a TOMWMS...")

                    clsLnProducto_codigos_barra.Delete_All()

                    For Each BeSAPProducto As clsBeI_nav_producto In lProductosFromSAP

                        BeProductoExistente = New clsBeProducto
                        BeProductoExistente = clsLnProducto.Existe(BeSAPProducto.No, lConnection, lTransaction)

                        If Not BeProductoExistente Is Nothing Then

                            vExisteBarra = clsLnProducto_codigos_barra.Existe_Codigo_Barra(BeProductoExistente.IdProducto, 1, BeSAPProducto.Item_Tracking_Code, lConnection, lTransaction)

                            If Not vExisteBarra Then

                                Try

                                    BeProductoCodigoBarra = New clsBeProducto_codigos_barra()
                                    BeProductoCodigoBarra.IdProductoCodigoBarra = clsLnProducto_codigos_barra.MaxID(lConnection, lTransaction) + 1
                                    BeProductoCodigoBarra.IdProducto = BeProductoExistente.IdProducto
                                    BeProductoCodigoBarra.IdProveedor = 1
                                    BeProductoCodigoBarra.Codigo_barra = BeSAPProducto.Item_Tracking_Code
                                    BeProductoCodigoBarra.User_agr = BeConfigEnc.IdUsuario
                                    BeProductoCodigoBarra.User_mod = BeConfigEnc.IdUsuario
                                    BeProductoCodigoBarra.Fec_agr = Now
                                    BeProductoCodigoBarra.Fec_mod = Now
                                    BeProductoCodigoBarra.Activo = True

                                    If clsLnProducto_codigos_barra.Insertar(BeProductoCodigoBarra, lConnection, lTransaction) > 0 Then

                                        If BeProductoExistente.Codigo_barra = "" Then
                                            BeProductoExistente.Codigo_barra = BeSAPProducto.Item_Tracking_Code
                                            clsLnProducto.Actualizar_CodigoBarra_By_IdProducto(BeProductoExistente, lConnection, lTransaction)
                                            clsPublic.Actualizar_Progreso(lblprg, "Se actualizó el código de barra: " & BeSAPProducto.Item_Tracking_Code & " para el dato maestro de WMS: " & BeProductoExistente.Codigo)
                                        End If

                                        VContadorBitacoraTOMWMS += 1

                                        clsPublic.Actualizar_Progreso(lblprg, "Código de barra: " & BeSAPProducto.Item_Tracking_Code & " actualizado para el itemcode: " & BeProductoExistente.Codigo)

                                    End If

                                Catch ex As Exception

                                    clsLnLog_error_wms.Agregar_Error(ex.Message)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo actualizar el producto: {0} {1}", BeSAPProducto.No, vbNewLine))

                                    Application.DoEvents()

                                End Try

                            Else

                                clsPublic.Actualizar_Progreso(lblprg, "El código de barra: " & BeSAPProducto.Item_Tracking_Code & " ya existe para el itemcode: " & BeSAPProducto.No)

                            End If

                        Else
                            clsPublic.Actualizar_Progreso(lblprg, "No se encontró el itemcode: " & BeSAPProducto.No & " en el maestro de productos de WMS.")
                        End If

                        vContador += 1
                        prg.Value = vContador

                    Next

                End If

                lTransaction.Commit()

                clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso -> " & Now)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos procesados correctamente: {0}", VContadorBitacoraTOMWMS))
                Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            End If


        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar producto a tabla de TOMWMS: {0}", ex.Message))
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

End Class
