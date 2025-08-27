Imports System.Data.SqlClient
Imports SAPbobsCOM

Public Class clsSyncSAPPresentaciones : Inherits clsInterfaceBase

    Private oCompany As Company
    Dim sErrMsg As String = ""
    Dim lErrCode As Long
    Dim VContadorBitacoraTOMWMS As Integer = 0
    Public Function Get_Producto_Presentacion_From_SAP(Optional ByVal AplicarFiltros As Boolean = True,
                                                       Optional ByVal pNoDocumentoSAP As String = "",
                                                       Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As List(Of clsBeI_nav_producto_presentacion)

        Get_Producto_Presentacion_From_SAP = Nothing

        Dim lReturnList As New List(Of clsBeI_nav_producto_presentacion)
        Dim lBodegasWMS As New List(Of clsBeI_nav_bodega)
        Dim query_sap As String = ""
        Dim sCookie As String = Nothing

        Try
            '#EJC20240708: importar las unidades de medida que son presentaciones, es decir que no son unidad.
            query_sap = "SELECT t3.U_CodWMS ItemCode,
                                t2.UomCode,
                                t0.UgpName,
                                t1.BaseQty,
                                t0.UgpCode,
                                t2.UomName
                        FROM 
                            OUGP AS t0
                        JOIN 
                            UGP1 AS t1 ON t0.UgpEntry = t1.UgpEntry
                        JOIN 
                            OUOM AS t2 ON t1.UomEntry = t2.UomEntry
                        JOIN 
                            OITM AS t3 ON t0.UgpEntry = t3.UgpEntry
                        WHERE t2.UomCode <> 'Unidad'
                    -- AND oitm.U_ENVIADO_WMS = 2 "

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg, pCompany)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeProductoPres As clsBeI_nav_producto_presentacion

                While rs.EoF = False

                    BeProductoPres = New clsBeI_nav_producto_presentacion()
                    BeProductoPres.No = rs.Fields.Item(0).Value.ToString() 'ItemCode
                    BeProductoPres.Codigo_Pres = rs.Fields.Item(1).Value.ToString() 'Nombre Presentacion                    
                    BeProductoPres.Factor = rs.Fields.Item(3).Value
                    lReturnList.Add(BeProductoPres)
                    rs.MoveNext()

                End While

                Get_Producto_Presentacion_From_SAP = lReturnList

            End If

        Catch ex As Exception
            Throw
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Presentaciones_Productos(ByRef lblprg As RichTextBox,
                                                      ByRef prg As ProgressBar) As Boolean

        Importar_Presentaciones_Productos = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            If MessageBox.Show("¿Actualizar presentaciones de productos desde SAP?", "Alias", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim lPresentacionesFromSAPKillio As New List(Of clsBeI_nav_producto_presentacion)
                Dim lPresentacionesFromSAPGaresa As New List(Of clsBeI_nav_producto_presentacion)

                clsPublic.Actualizar_Progreso(lblprg, "Consultando productos en tabla intermedia ")

                lPresentacionesFromSAPKillio = Get_Producto_Presentacion_From_SAP(False, "", pEmpresa.Killios)
                lPresentacionesFromSAPGaresa = Get_Producto_Presentacion_From_SAP(False, "", pEmpresa.Garesa)

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Presentaciones en tabla intermedia: {0}", lPresentacionesFromSAPKillio.Count))

                Dim BeProductoExistente As clsBeProducto = Nothing
                Dim BeProductoPresentacion As clsBeProducto_Presentacion = Nothing
                Dim BeProductoPresentacionExistente As clsBeProducto_Presentacion = Nothing
                Dim vExistePresentacion As Boolean = False

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

                prg.Maximum = lPresentacionesFromSAPKillio.Count + lPresentacionesFromSAPGaresa.Count + 1
                prg.Visible = True

                Dim vContador As Integer = 0

                prg.Value = 0

                If lPresentacionesFromSAPKillio.Count > 0 Then

                    clsPublic.Actualizar_Progreso(lblprg, "Trasladando presentaciones de producto de Killio desde SAP a TOMWMS...")

                    For Each BeSAPPresentacion As clsBeI_nav_producto_presentacion In lPresentacionesFromSAPKillio

                        If Not BeSAPPresentacion.No = "" Then

                            BeProductoExistente = New clsBeProducto
                            BeProductoExistente = clsLnProducto.Existe_By_Codigo_Or_NoParte(BeSAPPresentacion.No, lConnection, lTransaction)

                            If Not BeProductoExistente Is Nothing Then

                                BeProductoPresentacionExistente = clsLnProducto_presentacion.Existe_By_IdProducto_And_Presentacion(BeProductoExistente.IdProducto,
                                                                                                                   BeSAPPresentacion.Codigo_Pres,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)

                                If BeProductoPresentacionExistente Is Nothing Then

                                    Try

                                        BeProductoPresentacion = New clsBeProducto_Presentacion()
                                        BeProductoPresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                                        BeProductoPresentacion.IdProducto = BeProductoExistente.IdProducto
                                        BeProductoPresentacion.Nombre = BeSAPPresentacion.Codigo_Pres
                                        BeProductoPresentacion.Codigo = BeSAPPresentacion.Codigo_Pres
                                        BeProductoPresentacion.Codigo_barra = ""
                                        BeProductoPresentacion.User_agr = BeConfigEnc.IdUsuario
                                        BeProductoPresentacion.User_mod = BeConfigEnc.IdUsuario
                                        BeProductoPresentacion.Fec_agr = Now
                                        BeProductoPresentacion.Fec_mod = Now
                                        BeProductoPresentacion.Activo = True
                                        BeProductoPresentacion.Genera_lp_auto = BeConfigEnc.Genera_lp
                                        BeProductoPresentacion.Factor = BeSAPPresentacion.Factor

                                        If clsLnProducto_presentacion.Insertar(BeProductoPresentacion, lConnection, lTransaction) > 0 Then

                                            VContadorBitacoraTOMWMS += 1

                                            clsPublic.Actualizar_Progreso(lblprg, "Presentación: " & BeSAPPresentacion.Codigo_Pres & " insertada para el producto: " & BeProductoExistente.Codigo)

                                        End If

                                    Catch ex As Exception

                                        clsLnLog_error_wms.Agregar_Error(ex.Message)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto: {0} {1}", BeSAPPresentacion.No, vbNewLine))

                                        Application.DoEvents()

                                    End Try

                                Else

                                    Try

                                        BeProductoPresentacion = New clsBeProducto_Presentacion()
                                        BeProductoPresentacion.IdPresentacion = BeProductoPresentacionExistente.IdPresentacion
                                        BeProductoPresentacion.IdProducto = BeProductoExistente.IdProducto
                                        BeProductoPresentacion.Nombre = BeSAPPresentacion.Codigo_Pres
                                        BeProductoPresentacion.Codigo = BeSAPPresentacion.Codigo_Pres
                                        BeProductoPresentacion.Codigo_barra = ""
                                        BeProductoPresentacion.User_agr = BeConfigEnc.IdUsuario
                                        BeProductoPresentacion.User_mod = BeConfigEnc.IdUsuario
                                        BeProductoPresentacion.Fec_agr = Now
                                        BeProductoPresentacion.Fec_mod = Now
                                        BeProductoPresentacion.Activo = True
                                        BeProductoPresentacion.Genera_lp_auto = BeConfigEnc.Genera_lp
                                        BeProductoPresentacion.Factor = BeSAPPresentacion.Factor

                                        If clsLnProducto_presentacion.Actualizar(BeProductoPresentacion, lConnection, lTransaction) > 0 Then

                                            VContadorBitacoraTOMWMS += 1

                                            clsPublic.Actualizar_Progreso(lblprg, "Presentación: " & BeSAPPresentacion.Codigo_Pres & " actulizada para el producto: " & BeProductoExistente.Codigo)

                                        End If

                                    Catch ex As Exception

                                        clsLnLog_error_wms.Agregar_Error(ex.Message)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto: {0} {1}", BeSAPPresentacion.No, vbNewLine))

                                        Application.DoEvents()

                                    End Try

                                End If

                            Else
                                clsPublic.Actualizar_Progreso(lblprg, "No se encontró el producto: " & BeSAPPresentacion.No & " en el maestro de productos de WMS.")
                            End If

                        Else
                            clsPublic.Actualizar_Progreso(lblprg, "El código de producto WMS, no está definido en SAP.")
                        End If

                        vContador += 1
                        prg.Value = vContador

                    Next

                End If

                If lPresentacionesFromSAPGaresa.Count > 0 Then

                    clsPublic.Actualizar_Progreso(lblprg, "Trasladando presentaciones de producto de Garesa desde SAP a TOMWMS...")

                    For Each BeSAPPresentacion As clsBeI_nav_producto_presentacion In lPresentacionesFromSAPGaresa

                        If Not BeSAPPresentacion.No = "" Then

                            BeProductoExistente = New clsBeProducto
                            BeProductoExistente = clsLnProducto.Existe(BeSAPPresentacion.No, lConnection, lTransaction)

                            If Not BeProductoExistente Is Nothing Then

                                BeProductoPresentacionExistente = clsLnProducto_presentacion.Existe_By_IdProducto_And_Presentacion(BeProductoExistente.IdProducto,
                                                                                                                                   BeSAPPresentacion.Codigo_Pres,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction)

                                If BeProductoPresentacionExistente Is Nothing Then

                                    Try

                                        BeProductoPresentacion = New clsBeProducto_Presentacion()
                                        BeProductoPresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                                        BeProductoPresentacion.IdProducto = BeProductoExistente.IdProducto
                                        BeProductoPresentacion.Nombre = BeSAPPresentacion.Codigo_Pres
                                        BeProductoPresentacion.Codigo = BeSAPPresentacion.Codigo_Pres
                                        BeProductoPresentacion.Codigo_barra = ""
                                        BeProductoPresentacion.User_agr = BeConfigEnc.IdUsuario
                                        BeProductoPresentacion.User_mod = BeConfigEnc.IdUsuario
                                        BeProductoPresentacion.Fec_agr = Now
                                        BeProductoPresentacion.Fec_mod = Now
                                        BeProductoPresentacion.Activo = True
                                        BeProductoPresentacion.Genera_lp_auto = BeConfigEnc.Genera_lp
                                        BeProductoPresentacion.Factor = BeSAPPresentacion.Factor

                                        If clsLnProducto_presentacion.Insertar(BeProductoPresentacion, lConnection, lTransaction) > 0 Then

                                            VContadorBitacoraTOMWMS += 1

                                            clsPublic.Actualizar_Progreso(lblprg, "Presentación: " & BeSAPPresentacion.Codigo_Pres & " insertada para el producto: " & BeProductoExistente.Codigo)

                                        End If

                                    Catch ex As Exception

                                        clsLnLog_error_wms.Agregar_Error(ex.Message)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto: {0} {1}", BeSAPPresentacion.No, vbNewLine))

                                        Application.DoEvents()

                                    End Try

                                Else

                                    Try

                                        BeProductoPresentacion = New clsBeProducto_Presentacion()
                                        BeProductoPresentacion.IdPresentacion = BeProductoPresentacionExistente.IdPresentacion
                                        BeProductoPresentacion.IdProducto = BeProductoExistente.IdProducto
                                        BeProductoPresentacion.Nombre = BeSAPPresentacion.Codigo_Pres
                                        BeProductoPresentacion.Codigo = BeSAPPresentacion.Codigo_Pres
                                        BeProductoPresentacion.Codigo_barra = ""
                                        BeProductoPresentacion.User_agr = BeConfigEnc.IdUsuario
                                        BeProductoPresentacion.User_mod = BeConfigEnc.IdUsuario
                                        BeProductoPresentacion.Fec_agr = Now
                                        BeProductoPresentacion.Fec_mod = Now
                                        BeProductoPresentacion.Activo = True
                                        BeProductoPresentacion.Genera_lp_auto = BeConfigEnc.Genera_lp
                                        BeProductoPresentacion.Factor = BeSAPPresentacion.Factor

                                        If clsLnProducto_presentacion.Actualizar(BeProductoPresentacion, lConnection, lTransaction) > 0 Then

                                            VContadorBitacoraTOMWMS += 1

                                            clsPublic.Actualizar_Progreso(lblprg, "Presentación: " & BeSAPPresentacion.Codigo_Pres & " actulizada para el producto: " & BeProductoExistente.Codigo)

                                        End If

                                    Catch ex As Exception

                                        clsLnLog_error_wms.Agregar_Error(ex.Message)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo insertar el producto: {0} {1}", BeSAPPresentacion.No, vbNewLine))

                                        Application.DoEvents()

                                    End Try

                                End If

                            Else
                                clsPublic.Actualizar_Progreso(lblprg, "No se encontró el producto: " & BeSAPPresentacion.No & " en el maestro de productos de WMS.")
                            End If

                        Else
                            clsPublic.Actualizar_Progreso(lblprg, "El código de producto WMS está vacío o no definido en SAP.")
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
                prg.Value = 0

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

    Public Function Importar_Presentaciones_By_Producto(ByVal Codigo As String,
                                                        ByRef lblprg As RichTextBox,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As Boolean

        Importar_Presentaciones_By_Producto = False

        Try

            Dim lPresentacionesFromSAP As New List(Of clsBeI_nav_producto_presentacion)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando productos en tabla intermedia ")

            lPresentacionesFromSAP = clsLnI_nav_producto_presentacion.Get_All_By_Codigo(Codigo, lConnection, lTransaction)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Presentaciones en tabla intermedia: {0}", lPresentacionesFromSAP.Count))

            If lPresentacionesFromSAP.Count > 0 Then

                Dim BeProductoExistente As clsBeProducto = Nothing
                Dim BeProductoPresentacion As clsBeProducto_Presentacion = Nothing
                Dim beProductoPresentacionExistente As New clsBeProducto_Presentacion
                Dim vExistePresentacion As Boolean = False

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

                Dim vContador As Integer = 0

                clsPublic.Actualizar_Progreso(lblprg, "Trasladando presentaciones de producto desde SAP a TOMWMS...")

                For Each BeSAPPresentacion As clsBeI_nav_producto_presentacion In lPresentacionesFromSAP

                    BeProductoExistente = New clsBeProducto
                    BeProductoExistente = clsLnProducto.Existe(BeSAPPresentacion.No, lConnection, lTransaction)

                    If Not BeProductoExistente Is Nothing Then

                        beProductoPresentacionExistente = clsLnProducto_presentacion.Existe_By_IdProducto_And_Presentacion(BeProductoExistente.IdProducto,
                                                                                                                           BeSAPPresentacion.Codigo_Pres,
                                                                                                                           lConnection,
                                                                                                                           lTransaction)

                        If Not beProductoPresentacionExistente Is Nothing Then

                            Try

                                BeProductoPresentacion = New clsBeProducto_Presentacion()
                                BeProductoPresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                                BeProductoPresentacion.IdProducto = BeProductoExistente.IdProducto
                                BeProductoPresentacion.Nombre = BeSAPPresentacion.Codigo_Pres
                                BeProductoPresentacion.Codigo = BeProductoExistente.Codigo
                                BeProductoPresentacion.Codigo_barra = ""
                                BeProductoPresentacion.User_agr = BeConfigEnc.IdUsuario
                                BeProductoPresentacion.User_mod = BeConfigEnc.IdUsuario
                                BeProductoPresentacion.Fec_agr = Now
                                BeProductoPresentacion.Fec_mod = Now
                                BeProductoPresentacion.Activo = True
                                BeProductoPresentacion.Genera_lp_auto = BeConfigEnc.Genera_lp
                                BeProductoPresentacion.Factor = BeSAPPresentacion.Factor

                                If clsLnProducto_presentacion.Insertar(BeProductoPresentacion, lConnection, lTransaction) > 0 Then

                                    VContadorBitacoraTOMWMS += 1

                                    clsPublic.Actualizar_Progreso(lblprg, "Presentación: " & BeSAPPresentacion.Codigo_Pres & " actualizado para el producto: " & BeProductoExistente.Codigo)

                                End If

                            Catch ex As Exception

                                clsLnLog_error_wms.Agregar_Error(ex.Message)

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo actualizar el producto: {0} {1}", BeSAPPresentacion.No, vbNewLine))

                                Application.DoEvents()

                            End Try

                            vContador += 1

                        Else

                            Try

                                BeProductoPresentacion = New clsBeProducto_Presentacion()
                                BeProductoPresentacion.IdPresentacion = beProductoPresentacionExistente.IdPresentacion
                                BeProductoPresentacion.IdProducto = BeProductoExistente.IdProducto
                                BeProductoPresentacion.Nombre = BeSAPPresentacion.Codigo_Pres
                                BeProductoPresentacion.Codigo = BeProductoExistente.Codigo
                                BeProductoPresentacion.Codigo_barra = ""
                                BeProductoPresentacion.User_agr = BeConfigEnc.IdUsuario
                                BeProductoPresentacion.User_mod = BeConfigEnc.IdUsuario
                                BeProductoPresentacion.Fec_agr = Now
                                BeProductoPresentacion.Fec_mod = Now
                                BeProductoPresentacion.Activo = True
                                BeProductoPresentacion.Genera_lp_auto = BeConfigEnc.Genera_lp
                                BeProductoPresentacion.Factor = BeSAPPresentacion.Factor

                                If clsLnProducto_presentacion.Actualizar(BeProductoPresentacion, lConnection, lTransaction) > 0 Then

                                    VContadorBitacoraTOMWMS += 1

                                    clsPublic.Actualizar_Progreso(lblprg, "Presentación: " & BeSAPPresentacion.Codigo_Pres & " actualizada para el producto: " & BeProductoExistente.Codigo)

                                End If

                            Catch ex As Exception

                                clsLnLog_error_wms.Agregar_Error(ex.Message)

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo actualizar el producto: {0} {1}", BeSAPPresentacion.No, vbNewLine))

                                Application.DoEvents()

                            End Try

                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "No se encontró el producto: " & BeSAPPresentacion.No & " en el maestro de productos de WMS.")
                    End If

                    vContador += 1

                Next

            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar producto a tabla de TOMWMS: {0}", ex.Message))
            Throw
        End Try

    End Function

End Class
