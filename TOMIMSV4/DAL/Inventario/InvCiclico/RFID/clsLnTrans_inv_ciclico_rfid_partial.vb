Imports System.Data.SqlClient

Partial Public Class clsLnTrans_inv_ciclico_rfid


    Public Shared Function Get_All_RFID_Candidatos_Ajuste_Negativo(ByVal pIdInventarioEnc As Integer,
                                                               ByVal pFechaInicio As DateTime,
                                                               ByVal pFechaFin As DateTime,
                                                               ByVal pIdBodega As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As DataTable
        Try

            Dim DT As New DataTable

            '#GT02062026: tipo 1 es ingreso, tipo 2 es salida
            Dim vIdTipoMovimientoSalida As Integer = 2

            Dim vSql As String =
            "SELECT " & vbCrLf &
            "    icr.IdInventarioEnc, " & vbCrLf &
            "    icr.SSCC AS tag, " & vbCrLf &
            "    icr.cantidad, " & vbCrLf &
            "    stk.IdRfidStock AS IdStock, " & vbCrLf &
            "    stk.IdProductoBodega, " & vbCrLf &
            "    stk.barra_epc, " & vbCrLf &
            "    producto.codigo, " & vbCrLf &
            "    producto.Nombre, " & vbCrLf &
            "    stk.Lote, " & vbCrLf &
            "    stk.Cantidad AS CantidadStock, " & vbCrLf &
            "    'No confirmado por HH. Existe en stock RFID. Candidato a ajuste negativo.' AS Observacion " & vbCrLf &
            "FROM trans_inv_ciclico_rfid icr " & vbCrLf &
            "INNER JOIN i_nav_barras_rfid_stock stk ON stk.barra_epc = icr.SSCC " & vbCrLf &
            "INNER JOIN producto_bodega ON stk.IdProductoBodega = producto_bodega.IdProductoBodega AND stk.IdBodega = producto_bodega.IdBodega " & vbCrLf &
            "INNER JOIN producto ON producto_bodega.IdProducto = producto.IdProducto " & vbCrLf &
            "WHERE icr.IdInventarioEnc = @IdInventarioEnc " & vbCrLf &
            "  AND ISNULL(icr.cantidad, 0) = 0 " & vbCrLf &
            "  AND stk.IdBodega = @IdBodega " & vbCrLf &
            "  AND ISNULL(stk.Cantidad, 0) > 0 " & vbCrLf &
            "  AND NOT EXISTS ( " & vbCrLf &
            "      SELECT 1 " & vbCrLf &
            "      FROM i_nav_barras_rfid_mov mov " & vbCrLf &
            "      WHERE mov.barra_epc = icr.SSCC " & vbCrLf &
            "        AND mov.IdBodega = @IdBodega " & vbCrLf &
            "        AND mov.Fecha BETWEEN @FechaInicio AND @FechaFin " & vbCrLf &
            "        AND mov.IdRfidTipoMov = @IdTipoMovimientoSalida " & vbCrLf &
            "  ) " & vbCrLf &
            "ORDER BY producto.codigo, icr.SSCC "

            Using cmd As New SqlCommand(vSql, lConnection, lTransaction)

                cmd.Parameters.Add("@IdInventarioEnc", SqlDbType.Int).Value = pIdInventarioEnc
                cmd.Parameters.Add("@IdBodega", SqlDbType.Int).Value = pIdBodega
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = pFechaInicio
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = pFechaFin
                cmd.Parameters.Add("@IdTipoMovimientoSalida", SqlDbType.Int).Value = vIdTipoMovimientoSalida

                Using DA As New SqlDataAdapter(cmd)
                    DA.Fill(DT)
                End Using

            End Using

            Return DT

        Catch ex As Exception
            Throw New Exception("Error en Get_All_RFID_Candidatos_Ajuste_Negativo: " & ex.Message, ex)
        End Try

    End Function


    Public Shared Function Get_All_RFID_Salidas_Confirmadas(ByVal pIdInventarioEnc As Integer,
                                                        ByVal pFechaInicio As DateTime,
                                                        ByVal pFechaFin As DateTime,
                                                        ByVal pIdBodega As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As DataTable
        Try

            Dim DT As New DataTable

            Dim vIdTipoMovimientoSalida As Integer = 2

            Dim vSql As String =
            "SELECT " & vbCrLf &
            "    icr.IdInventarioEnc, " & vbCrLf &
            "    icr.SSCC AS tag, " & vbCrLf &
            "    icr.cantidad, " & vbCrLf &
            "    mov.IdRfidMovimiento AS IdMovimientoRFID, " & vbCrLf &
            "    mov.IdRfidTipoMov, " & vbCrLf &
            "    mov.Fecha, " & vbCrLf &
            "    mov.IdBodega, " & vbCrLf &
            "    mov.barra_epc, " & vbCrLf &
            "    producto.codigo, " & vbCrLf &
            "    producto.Nombre, " & vbCrLf &
            "    mov.Lote, " & vbCrLf &
            "    mov.Cantidad AS CantidadMovimiento, " & vbCrLf &
            "    'No confirmado por HH. No existe en stock RFID y tiene salida confirmada.' AS Observacion " & vbCrLf &
            "FROM trans_inv_ciclico_rfid icr " & vbCrLf &
            "INNER JOIN i_nav_barras_rfid_mov mov ON mov.barra_epc = icr.SSCC " & vbCrLf &
            "INNER JOIN producto_bodega ON mov.IdProductoBodega = producto_bodega.IdProductoBodega AND mov.IdBodega = producto_bodega.IdBodega " & vbCrLf &
            "INNER JOIN producto ON producto_bodega.IdProducto = producto.IdProducto " & vbCrLf &
            "WHERE icr.IdInventarioEnc = @IdInventarioEnc " & vbCrLf &
            "  AND ISNULL(icr.cantidad, 0) = 0 " & vbCrLf &
            "  AND mov.IdBodega = @IdBodega " & vbCrLf &
            "  AND mov.Fecha BETWEEN @FechaInicio AND @FechaFin " & vbCrLf &
            "  AND mov.IdRfidTipoMov = @IdTipoMovimientoSalida " & vbCrLf &
            "  AND NOT EXISTS ( " & vbCrLf &
            "      SELECT 1 " & vbCrLf &
            "      FROM i_nav_barras_rfid_stock stk " & vbCrLf &
            "      WHERE stk.barra_epc = icr.SSCC " & vbCrLf &
            "        AND stk.IdBodega = @IdBodega " & vbCrLf &
            "        AND ISNULL(stk.Cantidad, 0) > 0 " & vbCrLf &
            "  ) " & vbCrLf &
            "ORDER BY mov.Fecha DESC, producto.codigo, icr.SSCC "

            Using cmd As New SqlCommand(vSql, lConnection, lTransaction)

                cmd.Parameters.Add("@IdInventarioEnc", SqlDbType.Int).Value = pIdInventarioEnc
                cmd.Parameters.Add("@IdBodega", SqlDbType.Int).Value = pIdBodega
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = pFechaInicio
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = pFechaFin
                cmd.Parameters.Add("@IdTipoMovimientoSalida", SqlDbType.Int).Value = vIdTipoMovimientoSalida

                Using DA As New SqlDataAdapter(cmd)
                    DA.Fill(DT)
                End Using

            End Using

            Return DT

        Catch ex As Exception
            Throw New Exception("Error en Get_All_RFID_Salidas_Confirmadas: " & ex.Message, ex)
        End Try

    End Function

End Class
