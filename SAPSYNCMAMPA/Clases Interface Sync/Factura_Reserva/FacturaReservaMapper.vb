Imports System.Data.SqlClient
Imports System.Globalization
Imports Newtonsoft.Json.Linq

Public Class FacturaReservaMapper

    Shared vHanaService As SapServiceLayerClient

    Public Shared Function MapearEncabezado(enc As JObject, codBodega As String) As clsBeI_nav_ped_compra_enc

        Dim be As New clsBeI_nav_ped_compra_enc()
        be.No = Convert.ToInt32(enc("DocEntry"))
        Dim datePart As Date = Convert.ToDateTime(enc("DocDate")).Date
        Dim timeStr As String = enc("DocTime")?.ToString()
        Dim ts As TimeSpan
        If Not String.IsNullOrWhiteSpace(timeStr) AndAlso TimeSpan.TryParseExact(timeStr, "hh\:mm\:ss", CultureInfo.InvariantCulture, ts) Then
            be.Posting_Date = datePart.Add(ts)
        Else
            be.Posting_Date = datePart
        End If
        be.Order_Date = be.Posting_Date
        be.Document_Date = be.Posting_Date
        be.Expected_Receipt_Date = be.Posting_Date
        be.Status = 1
        be.Buy_From_Vendor_No = enc("CardCode").ToString()
        be.Buy_From_Vendor_Name = enc("CardName").ToString()
        be.Is_Internal_Transfer = False
        be.Location_Code = codBodega
        be.Vendor_Invoice_No = enc("DocNum").ToString()
        be.Posting_Description = enc("JournalMemo").ToString()
        be.Comments = enc("Comments").ToString()
        be.Series = enc("Series").ToString() 'Descripción de la serie.
        be.User_Document = enc("SalesEmployeeName").ToString() 'Usuario que creeo el documento no el vendedor.
        be.Product_Owner_Code = BeConfigEnc.IdPropietario
        If String.IsNullOrEmpty(be.Vendor_Invoice_No) Then
            be.Vendor_Invoice_No = be.No.ToString()
        End If
        be.Campaign_No = enc("U_Campania").ToString()
        be.IsImport = enc("U_Estado").ToString() = "3"
        be.Internal_Transfer_Document_No = enc("NumAtCard").ToString() 'No. Factura de Proveedor.
        If be.IsImport Then
            be.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_importación
        Else
            be.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Factura_Reserva_Proveedor
        End If

        Return be

    End Function

    Public Shared Function MapearDetalle(documentLines As JArray) As List(Of clsBeI_nav_ped_compra_det)
        Dim lista As New List(Of clsBeI_nav_ped_compra_det)()

        For Each linea As JObject In documentLines
            If linea("LineStatus")?.ToString() = "bost_Open" Then
                Dim d As New clsBeI_nav_ped_compra_det() With {
                    .NoEnc = linea("DocEntry")?.ToString(),
                    .No = linea("ItemCode")?.ToString(),
                    .Line_No = If(linea("LineNum") IsNot Nothing, Convert.ToInt32(linea("LineNum")), 0),
                    .Planed_Receipt_Date = Date.Now(),
                    .Quantity = Convert.ToDecimal(linea("RemainingOpenQuantity")),
                    .Quantity_Received = 0,
                    .Description = clsPublic.Quitar_Caracteres_No_Permitidos(linea("ItemDescription")?.ToString()),
                    .Unit_of_Measure_Code = linea("MeasureUnit")?.ToString(),
                    .Barcode = $"{linea("ItemCode")}{linea("U_Color")}{linea("U_Talla")}",
                    .Type = 2,
                    .Variant_Code = Nothing,
                    .Location_Code = linea("WarehouseCode")?.ToString(),
                    .Size = linea("U_Talla")?.ToString(),
                    .Color = linea("U_Color")?.ToString()
                }
                lista.Add(d)
            End If
        Next

        Return lista
    End Function

    Public Shared Function ConstruirTablaDesdeJsonTallasColores(documentLines As JArray, docEntry As Integer, campaña As Integer) As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("U_Modelo", GetType(String))
        dt.Columns.Add("U_Talla", GetType(String))
        dt.Columns.Add("U_Color", GetType(String))
        dt.Columns.Add("U_Campania", GetType(Integer))
        dt.Columns.Add("DocEntry", GetType(Integer))

        For Each linea As JObject In documentLines
            If linea("LineStatus")?.ToString() = "bost_Open" Then
                Dim modelo = linea.Value(Of String)("ItemCode")
                Dim talla = linea.Value(Of String)("U_Talla")
                Dim color = linea.Value(Of String)("U_Color")

                If Not String.IsNullOrWhiteSpace(talla) OrElse Not String.IsNullOrWhiteSpace(color) Then
                    Dim r = dt.NewRow()
                    r("U_Modelo") = modelo
                    r("U_Talla") = talla
                    r("U_Color") = color
                    r("U_Campania") = campaña
                    r("DocEntry") = docEntry
                    dt.Rows.Add(r)
                End If
            End If
        Next

        Return dt
    End Function

    Public Shared Async Function MapearDetalleTallaColor(dtDetTallaColor As DataTable,
                                                         Usuario As String,
                                                         lconnection As SqlConnection,
                                                         ltransaction As SqlTransaction,
                                                         SessionCookie As String,
                                                         BaseURL As String,
                                                         lbl As RichTextBox) As Task(Of List(Of clsBeProducto_talla_color))

        Dim lista As New List(Of clsBeProducto_talla_color)()

        For Each det As DataRow In dtDetTallaColor.Rows
            Dim d As New clsBeProducto_talla_color()

            Dim BeProducto = clsLnProducto.Get_Single_By_Codigo(det("U_Modelo").ToString(), lconnection, ltransaction)
            If BeProducto IsNot Nothing Then
                d.IdProducto = BeProducto.IdProducto
            Else
                Await clsSyncSAPProducto.Insertar_Producto_From_Sap_HanaAsync(det("U_Modelo").ToString(),
                                                                                              lconnection, ltransaction,
                                                                                              SessionCookie, BaseURL, lbl)
                BeProducto = clsLnProducto.Get_Single_By_Codigo(det("U_Modelo").ToString(), lconnection, ltransaction)
                If BeProducto IsNot Nothing Then
                    d.IdProducto = BeProducto.IdProducto
                Else
                    Throw New Exception("No se pudo insertar ni recuperar el producto con código: " & det("U_Modelo").ToString())
                End If
            End If

            Dim BeTalla = clsLnTalla.Get_Single_By_Codigo(det("U_Talla").ToString(), lconnection, ltransaction)
            If BeTalla IsNot Nothing Then
                d.IdTalla = BeTalla.IdTalla
            Else
                d.IdTalla = Await clsSyncSapTalla.Insertar_Talla_From_Sap_HanaAsync(det("U_Talla").ToString(), SessionCookie, BaseURL, lbl)

                If d.IdTalla = 0 Then
                    Throw New Exception("No se pudo insertar ni recuperar la talla con código: " & det("U_Color").ToString())
                End If

            End If

            Dim BeColor = clsLnColor.Get_Single_By_Codigo(det("U_Color").ToString(), lconnection, ltransaction)
            If BeColor IsNot Nothing Then
                d.IdColor = BeColor.IdColor
            Else
                d.IdColor = Await clsSyncSapColor.Insertar_Color_From_Sap_HanaAsync(det("U_Color").ToString(), SessionCookie, BaseURL, lbl)

                If d.IdColor = 0 Then
                    Throw New Exception("No se pudo insertar ni recuperar el color con código: " & det("U_Color").ToString())
                End If

            End If

            d.CodigoSKU = $"{det("U_Modelo")}{det("U_Color")}{det("U_Talla")}"

            Dim BeCampaña = clsLnCampaña.Get_Single_By_Codigo(det("U_Campania").ToString(), lconnection, ltransaction)
            If BeCampaña IsNot Nothing Then
                d.IdCampaña = det("U_Campania").ToString()
            Else
                d.IdCampaña = Await clsSyncSAPCampaña.Insertar_Campaña_From_Sap_Hana_SL(det("U_Campania").ToString(), lconnection, ltransaction, SessionCookie, BaseURL, IdUsuario)
            End If

            d.Fec_agr = Date.Now()
            d.User_agr = Usuario
            d.Fec_mod = Date.Now()
            d.User_mod = Usuario

            clsLnProducto_talla_color.InsertOrUpdate(d, lconnection, ltransaction)
            lista.Add(d)
        Next

        Return lista
    End Function

    Public Shared Function MapearCampaña(enc As DataRow,
                                         Usuario As String) As clsBeCampaña

        Dim be As clsBeCampaña = clsLnCampaña.Get_Single_By_Codigo(Convert.ToInt32(enc("DocEntry")))

        If be Is Nothing Then
            be = New clsBeCampaña With {
                .IdCampaña = Convert.ToInt32(enc("DocEntry")),
                .Codigo = Convert.ToInt32(enc("DocEntry")),
                .Nombre = enc("Remark").ToString(),
                .FechaInicio = Convert.ToDateTime(enc("U_FechaInicial")),
                .FechaFin = Convert.ToDateTime(enc("U_FechaFinal")),
                .Activo = True,
                .Fec_agr = Date.Now(),
                .Fec_mod = Date.Now(),
                .User_agr = Usuario,
                .User_mod = Usuario
            }
        End If

        Return be
    End Function

    Public Shared Async Function MapearDetalleTallaColor(dtDetTallaColor As DataTable,
                                                        lConnection As SqlConnection,
                                                        lTransaction As SqlTransaction,
                                                        Usuario As String,
                                                        SessionCookie As String,
                                                        BaseURL As String) As Task(Of List(Of clsBeProducto_talla_color))

        Dim lista As New List(Of clsBeProducto_talla_color)()

        For Each det As DataRow In dtDetTallaColor.Rows
            Dim d As New clsBeProducto_talla_color()

            Dim BeProducto = clsLnProducto.Get_Single_By_Codigo(det("U_Modelo").ToString(), lConnection, lTransaction)
            If BeProducto IsNot Nothing Then
                d.IdProducto = BeProducto.IdProducto
            Else
                Await clsSyncSAPProducto.Insertar_Producto_From_Sap_HanaAsync(det("U_Modelo").ToString(), lConnection, lTransaction, SessionCookie, BaseURL, Nothing)
                BeProducto = clsLnProducto.Get_Single_By_Codigo(det("U_Modelo").ToString(), lConnection, lTransaction)
                If BeProducto IsNot Nothing Then
                    d.IdProducto = BeProducto.IdProducto
                Else
                    Throw New Exception("No se pudo insertar ni recuperar el producto con código: " & det("U_Modelo").ToString())
                End If
            End If

            Dim BeTalla = clsLnTalla.Get_Single_By_Codigo(det("U_Talla").ToString(), lConnection, lTransaction)
            If BeTalla IsNot Nothing Then
                d.IdTalla = BeTalla.IdTalla
            Else
                d.IdTalla = Await clsSyncSapTalla.Insertar_Talla_From_Sap_HanaAsync(det("U_Talla").ToString(), SessionCookie, BaseURL, lConnection, lTransaction)

                If d.IdTalla = 0 Then
                    Throw New Exception("No se pudo insertar ni recuperar la talla con código: " & det("U_Color").ToString())
                End If

            End If

            Dim BeColor = clsLnColor.Get_Single_By_Codigo(det("U_Color").ToString(), lConnection, lTransaction)
            If BeColor IsNot Nothing Then
                d.IdColor = BeColor.IdColor
            Else
                d.IdColor = Await clsSyncSapColor.Insertar_Color_From_Sap_HanaAsync(det("U_Color").ToString(), SessionCookie, BaseURL, lConnection, lTransaction)

                If d.IdColor = 0 Then
                    Throw New Exception("No se pudo insertar ni recuperar el color con código: " & det("U_Color").ToString())
                End If

            End If

            d.CodigoSKU = $"{det("U_Modelo")}{det("U_Color")}{det("U_Talla")}"

            Dim BeCampaña = clsLnCampaña.Get_Single_By_Codigo(det("U_Campania").ToString(), lConnection, lTransaction)
            If BeCampaña IsNot Nothing Then
                d.IdCampaña = det("U_Campania").ToString()
            Else
                d.IdCampaña = Await clsSyncSAPCampaña.Insertar_Campaña_From_Sap_Hana_SL(det("U_Campania").ToString(), lConnection, lTransaction, SessionCookie, BaseURL, IdUsuario)
            End If

            d.Fec_agr = Date.Now()
            d.User_agr = Usuario
            d.Fec_mod = Date.Now()
            d.User_mod = Usuario

            clsLnProducto_talla_color.InsertOrUpdate(d, lConnection, lTransaction)
            lista.Add(d)
        Next

        Return lista
    End Function

    Public Shared Function MapearCampaña(enc As DataRow,
                                         lConnection As SqlConnection,
                                         lTransaction As SqlTransaction,
                                         Usuario As String) As clsBeCampaña

        Dim be As clsBeCampaña = clsLnCampaña.Get_Single_By_Codigo(Convert.ToInt32(enc("DocEntry")), lConnection, lTransaction)

        If be Is Nothing Then
            be = New clsBeCampaña With {
                .IdCampaña = Convert.ToInt32(enc("DocEntry")),
                .Codigo = Convert.ToInt32(enc("DocEntry")),
                .Nombre = enc("Remark").ToString(),
                .FechaInicio = Convert.ToDateTime(enc("U_FechaInicial")),
                .FechaFin = Convert.ToDateTime(enc("U_FechaFinal")),
                .Activo = True,
                .Fec_agr = Date.Now(),
                .Fec_mod = Date.Now(),
                .User_agr = Usuario,
                .User_mod = Usuario
            }
        End If

        Return be
    End Function

    Public Shared Async Function MapearDetalleTallaColor(dtDetTallaColor As DataTable,
                                                         Usuario As String,
                                                         SessionCookie As String,
                                                         BaseURL As String,
                                                         lbl As RichTextBox) As Task(Of List(Of clsBeProducto_talla_color))

        Dim lista As New List(Of clsBeProducto_talla_color)()

        For Each det As DataRow In dtDetTallaColor.Rows
            Dim d As New clsBeProducto_talla_color()

            Dim BeProducto = clsLnProducto.Get_Single_By_Codigo(det("U_Modelo").ToString())
            If BeProducto IsNot Nothing Then
                d.IdProducto = BeProducto.IdProducto
            Else

                Await clsSyncSAPProducto.Insertar_Producto_From_Sap_HanaAsync(det("U_Modelo").ToString(), SessionCookie, BaseURL, lbl)

                BeProducto = clsLnProducto.Get_Single_By_Codigo(det("U_Modelo").ToString())

                If BeProducto IsNot Nothing Then
                    d.IdProducto = BeProducto.IdProducto
                Else
                    Throw New Exception("No se pudo insertar ni recuperar el producto con código: " & det("U_Modelo").ToString())
                End If

            End If

            Dim BeTalla = clsLnTalla.Get_Single_By_Codigo(det("U_Talla").ToString())
            If BeTalla IsNot Nothing Then
                d.IdTalla = BeTalla.IdTalla
            Else
                d.IdTalla = Await clsSyncSapTalla.Insertar_Talla_From_Sap_HanaAsync(det("U_Color").ToString(), SessionCookie, BaseURL, lbl)

                If d.IdTalla = 0 Then
                    Throw New Exception("No se pudo insertar ni recuperar la talla con código: " & det("U_Color").ToString())
                End If

            End If

            Dim BeColor = clsLnColor.GetSingle_By_CodigoColor(det("U_Color").ToString())
            If BeColor IsNot Nothing Then
                d.IdColor = BeColor.IdColor
            Else
                d.IdColor = Await clsSyncSapColor.Insertar_Color_From_Sap_HanaAsync(det("U_Color").ToString(), SessionCookie, BaseURL, lbl)

                If d.IdColor = 0 Then
                    Throw New Exception("No se pudo insertar ni recuperar el color con código: " & det("U_Color").ToString())
                End If

            End If

            d.CodigoSKU = $"{det("U_Modelo")}{det("U_Color")}{det("U_Talla")}"

            Dim BeCampaña = clsLnCampaña.Get_Single_By_Codigo(det("U_Campania").ToString())
            If BeCampaña IsNot Nothing Then
                d.IdCampaña = det("U_Campania").ToString()
            Else
                d.IdCampaña = Await clsSyncSAPCampaña.Insertar_Campaña_From_Sap_Hana_SL(det("U_Campania").ToString(), SessionCookie, BaseURL, IdUsuario)
            End If

            d.Fec_agr = Date.Now()
            d.User_agr = Usuario
            d.Fec_mod = Date.Now()
            d.User_mod = Usuario

            clsLnProducto_talla_color.InsertOrUpdate(d)
            lista.Add(d)
        Next

        Return lista
    End Function

    Public Shared Function ObtenerBodegaUnica(documentLines As JArray) As String
        If documentLines Is Nothing OrElse documentLines.Count = 0 Then
            Throw New ApplicationException("No se recibieron líneas para determinar la bodega.")
        End If

        Dim bodegaUnica As String = Nothing

        For Each linea As JObject In documentLines
            Dim token = linea("WarehouseCode")
            If token Is Nothing OrElse token.Type = JTokenType.Null Then Continue For

            Dim whs As String = token.ToString()
            If String.IsNullOrWhiteSpace(whs) Then Continue For

            whs = whs.Trim()

            If bodegaUnica Is Nothing Then
                bodegaUnica = whs
            ElseIf Not whs.Equals(bodegaUnica, StringComparison.OrdinalIgnoreCase) Then
                Throw New ApplicationException($"Se detectaron múltiples bodegas: '{bodegaUnica}' y '{whs}'.")
            End If
        Next

        If bodegaUnica Is Nothing Then
            Throw New ApplicationException("Ninguna línea contiene WarehouseCode (bodega).")
        End If

        Return bodegaUnica
    End Function


End Class