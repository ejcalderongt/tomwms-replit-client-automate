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
            Dim query As String = $"SELECT TOP 1 U.UomEntry " &
                              $"FROM OITM I " &
                              $"JOIN OUGP UG ON I.UgpEntry = UG.UgpEntry " &
                              $"JOIN UGP1 U1 ON UG.UgpEntry = U1.UgpEntry " &
                              $"JOIN OUOM U ON U1.UomEntry = U.UomEntry " &
                              $"WHERE I.ItemCode = '{ItemCode}' AND U.UomName = '{NombreUOM}'"

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



    Public Shared Function Obtener_UoM_Por_Cantidad(ByVal ItemCode As String,
                                                    ByVal TotalUnidades As Integer,
                                                    ByRef oCompany As Company) As (UoMEntry As Integer, Factor As Integer)

        Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

        Dim sql As String = " SELECT U.UomEntry, U.UomCode, G.BaseQty
                              FROM OITM I
                                   INNER JOIN UGP1 G ON I.UgpEntry = G.UgpEntry
                                   INNER JOIN OUOM U ON G.UomEntry = U.UomEntry
                              WHERE I.ItemCode = '" & ItemCode & "'"

        rs.DoQuery(sql)

        Dim mejorUomEntry As Integer = 0
        Dim mejorFactor As Integer = 1
        Dim factor As Integer = 0

        While Not rs.EoF
            Dim uomEntry As Integer = CInt(rs.Fields.Item("UomEntry").Value)
            Dim factor1 As Integer = CInt(rs.Fields.Item("BaseQty").Value)

            If factor1 > 0 Then

                If factor1 > factor AndAlso TotalUnidades >= factor1 Then
                    factor = factor1

                    mejorUomEntry = uomEntry
                    mejorFactor = factor
                End If

            End If

            rs.MoveNext()
        End While

        ' Liberar objeto COM
        Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        rs = Nothing

        Return (mejorUomEntry, mejorFactor)

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
End Class