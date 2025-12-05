Imports SAPbobsCOM

Public Class SapHelper

    Public Shared Function Obtener_UoMEntry_Por_Codigo_Con_Recordset(ByVal UomCode As String, ByRef oCompany As Company) As Integer

        Dim oRS As Recordset = Nothing

        Obtener_UoMEntry_Por_Codigo_Con_Recordset = 1

        Try

            oRS = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

            Dim sQuery As String = "SELECT UomEntry FROM OUOM WHERE UomCode = '" & UomCode & "'"
            oRS.DoQuery(sQuery)

            If Not oRS.EoF Then
                Return Convert.ToInt32(oRS.Fields.Item("UomEntry").Value)
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw New Exception("Error al obtener UoMEntry para el código '" & UomCode & "': " & ex.Message)

        Finally
            If oRS IsNot Nothing Then
                Runtime.InteropServices.Marshal.ReleaseComObject(oRS)
                oRS = Nothing
            End If
        End Try
    End Function
    Public Shared Function Obtener_UoMEntry_Producto(ByVal ItemCode As String,
                                                     ByVal oCompany As Company,
                                                     Optional ByVal NombreUOM As String = "Unidad") As Integer

        Dim oRs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim UoMEntry As Integer = 0

        Try
            Dim query As String = $"SELECT TOP 1 U.UomEntry 
                              From OITM I 
                              JOIN OUGP UG ON I.UgpEntry = UG.UgpEntry 
                              JOIN UGP1 U1 ON UG.UgpEntry = U1.UgpEntry 
                              Join OUOM U ON U1.UomEntry = U.UomEntry 
                              Where I.ItemCode = '{ItemCode}' AND U.UomName = '{NombreUOM}'"

            oRs.DoQuery(query)

            If Not oRs.EoF Then
                UoMEntry = Convert.ToInt32(oRs.Fields.Item("UomEntry").Value)
            End If

        Catch ex As Exception
            Throw New Exception($"Error al obtener UoMEntry del producto {ItemCode}: {ex.Message}")
        Finally
            Runtime.InteropServices.Marshal.ReleaseComObject(oRs)
            oRs = Nothing
            GC.Collect()
        End Try

        Return UoMEntry
    End Function
    Public Shared Function Obtener_Codigo_Exportacion_SAP(code As String, codigoWMS As String, oCompany As Company) As String

        Dim rs As Recordset = Nothing
        Dim mensajeError As String = "No fue configurado el código de exportación en SAP para el código de producto WMS: " & codigoWMS & " ItemCodeSAP: " & code

        Try
            rs = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

            ' Escapar por seguridad
            Dim codigoSeguro As String = code.Replace("'", "''")
            Dim query As String = "SELECT U_CodExport FROM [@Cod_Export] WHERE Code = '" & codigoSeguro & "'"

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                Dim codigoExportacion As String = rs.Fields.Item("U_CodExport").Value.ToString().Trim()

                If String.IsNullOrEmpty(codigoExportacion) Then
                    codigoExportacion = code
                End If

                Obtener_Codigo_Exportacion_SAP = codigoExportacion
            Else
                Obtener_Codigo_Exportacion_SAP = codigoSeguro 'Throw New Exception(mensajeError)
            End If

        Catch ex As Exception
            Throw New Exception("Error al consultar código de exportación SAP: " & ex.Message, ex)

        Finally
            If rs IsNot Nothing Then
                Runtime.InteropServices.Marshal.ReleaseComObject(rs)
                rs = Nothing
            End If
        End Try
    End Function
    Public Shared Function Obtener_UoMEntry_De_InventoryUOM(ByVal ItemCode As String,
                                                            ByRef oCompany As Company) As (UoMEntry As Integer, Factor As Integer)

        Dim oRS As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

        Dim sql As String = " SELECT U.UomEntry, U.UomCode, G.BaseQty
                              FROM OITM I
                                   INNER JOIN UGP1 G ON I.UgpEntry = G.UgpEntry
                                   INNER JOIN OUOM U ON G.UomEntry = U.UomEntry
                              WHERE I.ItemCode = '" & ItemCode & "' AND U.UomName = i.InvntryUom"

        oRS.DoQuery(sql)

        If Not oRS.EoF Then
            Return (CInt(oRS.Fields.Item("UomEntry").Value), CInt(oRS.Fields.Item("BaseQty").Value))
        Else
            Throw New Exception("No se encontró InventoryUOM para el artículo " & ItemCode)
        End If
    End Function
    Public Shared Function Obtener_FactorConversion(ByVal ItemCode As String,
                                                    ByVal UomEntry As Integer,
                                                    ByVal oCompany As Company) As Double

        Dim oRs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim factor As Double = 0

        Try
            Dim query As String = $"
            SELECT TOP 1 
                U1.BaseQty AS FactorConversion
            FROM OITM I
            JOIN OUGP UG ON I.UgpEntry = UG.UgpEntry
            JOIN UGP1 U1 ON UG.UgpEntry = U1.UgpEntry
            WHERE I.ItemCode = '{ItemCode}' AND U1.UomEntry = {UomEntry}
        "

            oRs.DoQuery(query)

            If Not oRs.EoF Then
                factor = Convert.ToDouble(oRs.Fields.Item("FactorConversion").Value)
            End If

        Catch ex As Exception
            Throw New Exception($"Error al obtener el factor de conversión del producto {ItemCode}: {ex.Message}")
        Finally
            Runtime.InteropServices.Marshal.ReleaseComObject(oRs)
            oRs = Nothing
            GC.Collect()
        End Try

        Return factor
    End Function

    Public Structure UoMInfo
        Public UoMEntry As Integer
        Public UoMCode As String
    End Structure

    ''' <summary>
    ''' Obtiene UoMEntry y UoMCode de una línea de una Inventory Transfer Request.
    ''' </summary>
    ''' <param name="company">Instancia conectada de SAPbobsCOM.Company</param>
    ''' <param name="docEntry">DocEntry del documento (Transfer Request)</param>
    ''' <param name="itemCode">Código de artículo (ItemCode) a buscar</param>
    ''' <param name="lineNum">Número de línea EXACTO (base 0 en SAP B1)</param>
    ''' <param name="result">Estructura de salida con UoMEntry y UoMCode</param>
    ''' <returns>True si encontró la línea y llenó result; False en caso contrario</returns>
    Public Shared Function GetUoMFromTransferRequest(
        ByVal company As SAPbobsCOM.Company,
        ByVal docEntry As Integer,
        ByVal itemCode As String,
        ByVal lineNum As Integer,
        ByRef result As UoMInfo
    ) As Boolean

        If company Is Nothing OrElse company.Connected = False Then
            Throw New InvalidOperationException("La conexión a SAP (Company) no está establecida.")
        End If

        ' Obtener el documento de tipo Inventory Transfer Request
        Dim trq As SAPbobsCOM.StockTransfer =
            CType(company.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), SAPbobsCOM.StockTransfer)

        If trq Is Nothing Then
            Throw New ApplicationException("No se pudo crear el objeto oInventoryTransferRequest.")
        End If

        If Not trq.GetByKey(docEntry) Then
            ' No existe el DocEntry solicitado
            Return False
        End If

        ' Recorrer líneas y encontrar coincidencia por ItemCode y LineNum
        For i As Integer = 0 To trq.Lines.Count - 1
            trq.Lines.SetCurrentLine(i)

            ' Comparación estricta por línea e ItemCode
            If trq.Lines.LineNum = lineNum AndAlso
               String.Equals(trq.Lines.ItemCode, itemCode, StringComparison.OrdinalIgnoreCase) Then

                result = New UoMInfo With {
                    .UoMEntry = trq.Lines.UoMEntry,
                    .UoMCode = trq.Lines.UoMCode
                }
                Return True
            End If
        Next

        ' Si no se encontró la línea exacta, intentar por ItemCode y devolver la primera coincidencia (opcional).
        ' Descomenta este bloque si quieres fallback por ItemCode:
        'For i As Integer = 0 To trq.Lines.Count - 1
        '    trq.Lines.SetCurrentLine(i)
        '    If String.Equals(trq.Lines.ItemCode, itemCode, StringComparison.OrdinalIgnoreCase) Then
        '        result = New UoMInfo With {
        '            .UoMEntry = trq.Lines.UoMEntry,
        '            .UoMCode = trq.Lines.UoMCode
        '        }
        '        Return True
        '    End If
        'Next

        Return False
    End Function

End Class