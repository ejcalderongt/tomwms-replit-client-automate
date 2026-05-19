Imports SAPbobsCOM

Public Class clsSyncLotes : Inherits clsInterfaceBase

    Public Function Existe_Lote_SAP(oCompany As Company, ByVal ItemCode As String, ByVal Lote As String, ByVal Whs As String) As Double

        Existe_Lote_SAP = 0

        Try

            Dim vQueryLotes As String = $"SELECT Cantidad_Lote FROM VW_STOCK_POR_LOTE WHERE CODIGO = '{ItemCode}' AND LOTE = '{Lote}' AND Codigo_Bodega ='{Whs}' "

            Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(vQueryLotes)

            While rs.EoF = False

                Existe_Lote_SAP = IIf(IsDBNull(rs.Fields.Item("Cantidad_Lote").Value), "", rs.Fields.Item("Cantidad_Lote").Value.ToString())

                rs.MoveNext()

            End While

        Catch ex As Exception

        End Try

    End Function

    Public Function Existe_Disponible_SAP(ByVal oCompany As Company,
                                          ByVal ItemCode As String,
                                          ByVal Whs As String) As Double

        Dim disponible As Double = 0
        Dim rs As Recordset = Nothing

        Try
            Dim vQuery As String =
            "SELECT ISNULL(Inventario, 0) AS Inventario " &
            "FROM [VW_STOCK_SIN_LOTE] " &
            "WHERE Codigo = '" & ItemCode.Replace("'", "''") & "' " &
            "AND Codigo_Bodega = '" & Whs.Replace("'", "''") & "'"

            rs = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(vQuery)

            If rs.EoF = False Then
                If Not IsDBNull(rs.Fields.Item("Inventario").Value) Then
                    Double.TryParse(rs.Fields.Item("Inventario").Value.ToString(), disponible)
                End If
            End If

            Return disponible

        Catch ex As Exception
            Throw New Exception("Error consultando stock sin lote en [VW_STOCK_SIN_LOTE]. Producto: " &
                            ItemCode & ", Bodega: " & Whs & ". Detalle: " & ex.Message)
        Finally
            If rs IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.ReleaseComObject(rs)
                rs = Nothing
            End If
        End Try

    End Function

    Public Function Get_Total_Lote_SAP(oCompany As Company, ByVal ItemCode As String, ByVal Whs As String) As Double

        Get_Total_Lote_SAP = 0

        Try

            Dim vQueryLotes As String = $"SELECT total_almacen FROM VW_STOCK_POR_LOTE WHERE CODIGO = '{ItemCode}' AND Codigo_Bodega ='{Whs}' "

            Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(vQueryLotes)

            While rs.EoF = False

                Get_Total_Lote_SAP = IIf(IsDBNull(rs.Fields.Item("total_almacen").Value), "", rs.Fields.Item("total_almacen").Value.ToString())

                rs.MoveNext()

            End While

        Catch ex As Exception

        End Try

    End Function


End Class