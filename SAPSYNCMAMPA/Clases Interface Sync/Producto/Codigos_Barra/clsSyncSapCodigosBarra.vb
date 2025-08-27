Imports System.Data.SqlClient
Imports Sap.Data.Hana

Public Class clsSyncSapCodigosBarra : Inherits clsInterfaceBase

    Dim VContadorBitacoraTOMWMS As Integer = 0

    Public Shared Function Get_Codigos_Barra_Productos_SAP_HANA() As List(Of clsBeI_nav_producto)
        Dim codigo_barras As New List(Of clsBeI_nav_producto)

        Dim query As String =
            "SELECT DISTINCT T0.""Name"" ITEMCODE, T0.""CODIGOBARRA"" 
             FROM (
             SELECT ""Name"",concat(concat(""Name"", ""U_Color""), ""U_Talla"") CodigoBarra  
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND concat(concat(""Name"", ""U_Color""), ""U_Talla"") IS NOT NULL
                   AND concat(concat(""Name"", ""U_Color""), ""U_Talla"")<>''
             UNION ALL
             SELECT ""Name"", ""U_CodigoAnterior"" CodigoBarra
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND ""U_CodigoAnterior"" IS NOT NULL AND ""U_CodigoAnterior"" <> ''
             UNION ALL
             SELECT ""Name"", ""U_CodigoProv"" CodigoBarra
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND ""U_CodigoProv"" IS NOT NULL AND ""U_CodigoProv"" <> ''
             UNION ALL
             SELECT ""Name"", ""U_CodigoAnterior2"" CodigoBarra
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND ""U_CodigoAnterior2"" IS NOT NULL AND ""U_CodigoAnterior2""<>''
             UNION ALL
             SELECT ""Name"", ""U_CodigoAnterior3"" CodigoBarra
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND ""U_CodigoAnterior3"" IS NOT NULL AND ""U_CodigoAnterior3""<>'') AS T0
            ORDER BY T0.""Name"""

        Try
            Using conn As HanaConnection = HanaHelper.OpenDB()
                Dim dt As DataTable = HanaHelper.OpenDT(query, conn)
                If dt IsNot Nothing Then
                    For Each row As DataRow In dt.Rows
                        codigo_barras.Add(MapRowToCodigoBarra(row))
                    Next
                End If
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener productos: " & ex.Message)
        End Try

        Return codigo_barras
    End Function

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

                lProductosFromSAP = Get_Codigos_Barra_Productos_SAP_HANA()

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

                    lTransaction.Commit()

                End If


                clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso -> " & Now)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos procesados correctamente: {0}", VContadorBitacoraTOMWMS))
                Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            End If


        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar producto a tabla de TOMWMS: {0}", ex.Message))
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Private Shared Function MapRowToCodigoBarra(row As DataRow) As clsBeI_nav_producto
        Return New clsBeI_nav_producto With {
            .No = row("ITEMCODE").ToString(),
            .Item_Tracking_Code = row("CODIGOBARRA").ToString()
        }
    End Function

End Class