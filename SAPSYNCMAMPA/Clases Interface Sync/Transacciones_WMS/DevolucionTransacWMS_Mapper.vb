Imports System.Data.SqlClient
Imports System.Globalization
Imports Newtonsoft.Json.Linq

Public Class DevolucionTransacWMS_Mapper

    Inherits FacturaReservaMapper

    Public Shared Function MapearEncabezado_Devolucion(enc As JObject) As clsBeI_nav_ped_compra_enc

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
        be.Buy_From_Vendor_No = enc("U_Transfer_to_Code").ToString()
        be.Buy_From_Vendor_Name = enc("U_Transfer_to_Name").ToString()
        be.Is_Internal_Transfer = False
        be.Location_Code = enc("U_Transfer_from_Code").ToString() ' codBodega 
        be.Vendor_Invoice_No = enc("U_External_Document_No").ToString()
        be.Posting_Description = ""
        be.Comments = ""
        be.Series = enc("U_Serie").ToString() 'Descripción de la serie.
        be.User_Document = enc("UserSign").ToString() 'Usuario que creeo el documento no el vendedor.
        be.Product_Owner_Code = BeConfigEnc.IdPropietario
        If String.IsNullOrEmpty(be.Vendor_Invoice_No) Then
            be.Vendor_Invoice_No = be.No.ToString()
        End If
        be.Campaign_No = ""
        be.IsImport = False
        be.Internal_Transfer_Document_No = "" 'No. Factura de Proveedor.
        be.Document_Type = 17

        Return be

    End Function

    Public Shared Function MapearDetalle_Devolucion(documentLines As JArray) As List(Of clsBeI_nav_ped_compra_det)
        Dim lista As New List(Of clsBeI_nav_ped_compra_det)()

        For Each linea As JObject In documentLines
            Dim d As New clsBeI_nav_ped_compra_det() With {
                    .NoEnc = linea("DocEntry")?.ToString(),
                    .No = linea("ItemCode")?.ToString(),
                    .Line_No = If(linea("LineNum") IsNot Nothing, Convert.ToInt32(linea("LineNum")), 0),
                    .Planed_Receipt_Date = Date.Now(),
                    .Quantity = Convert.ToDecimal(linea("Quantity")),
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
        Next

        Return lista
    End Function

    Public Shared Function ConstruirTablaDesdeJsonTallasColores_Devolucion(documentLines As JArray, docEntry As Integer, campaña As Integer) As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("U_Modelo", GetType(String))
        dt.Columns.Add("U_Talla", GetType(String))
        dt.Columns.Add("U_Color", GetType(String))
        dt.Columns.Add("U_Campania", GetType(Integer))
        dt.Columns.Add("DocEntry", GetType(Integer))

        For Each linea As JObject In documentLines
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
        Next

        Return dt
    End Function

    Public Shared Async Function MapearDetalleTallaColor_Devolucion(dtDetTallaColor As DataTable,
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
                d.IdProducto = clsSyncSAPProducto.Insertar_Producto_From_Sap_Hana(det("U_Modelo").ToString(), lConnection, lTransaction)
            End If

            Dim BeTalla = clsLnTalla.Get_Single_By_Codigo(det("U_Talla").ToString(), lConnection, lTransaction)
            If BeTalla IsNot Nothing Then
                d.IdTalla = BeTalla.IdTalla
            End If

            Dim BeColor = clsLnColor.Get_Single_By_Codigo(det("U_Color").ToString(), lConnection, lTransaction)
            If BeColor IsNot Nothing Then
                d.IdColor = BeColor.IdColor
            Else
                d.IdColor = clsSyncSapColor.Insertar_Color_From_Sap_Hana(det("U_Color").ToString(), lConnection, lTransaction)
            End If

            d.CodigoSKU = $"{det("U_Modelo")}{det("U_Color")}{det("U_Talla")}"

            d.IdCampaña = 0

            d.Fec_agr = Date.Now()
            d.User_agr = Usuario
            d.Fec_mod = Date.Now()
            d.User_mod = Usuario

            clsLnProducto_talla_color.InsertOrUpdate(d, lConnection, lTransaction)
            lista.Add(d)
        Next

        Return lista
    End Function

End Class
