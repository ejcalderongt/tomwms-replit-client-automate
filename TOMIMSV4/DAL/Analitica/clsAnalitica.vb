Imports System.Data.SqlClient

Public Class clsAnalitica

    Public Shared Function Get_Distribucion_Por_Tramo_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_Distribucion_Por_Tramo_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Stock_Rep_20200112 WHERE IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Resumen_Exitencia_Codigos_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_Resumen_Exitencia_Codigos_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT tipo Arg, sum(cantidad) as Val 
                                  FROM vw_stock_res WHERE IdBodega=@IdBodega 
                                  GROUP BY tipo  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Resumen_Exitencia_Estado_Producto_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_Resumen_Exitencia_Estado_Producto_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT NomEstado AS Arg, sum(cantidad) as Val 
                                  FROM vw_stock_res WHERE IdBodega=@IdBodega group by NomEstado "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Cantidad_Documentos_Por_Tipo_En_Rango_Fechas(ByVal pIdBodega As Integer) As DataTable

        Get_Cantidad_Documentos_Por_Tipo_En_Rango_Fechas = Nothing

        Try

            Dim vSQL As String = "SELECT NomEstado AS Arg, sum(cantidad) as Val 
                                  FROM vw_stock_res WHERE IdBodega=@IdBodega group by NomEstado "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_indicador_enc() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'GT 16032021 El parametro es_bodega_fiscal determina si es fiscal o general
            Const sp As String = "SELECT IdIndicadorEnc as codigo,descripcion from indicador_enc where estado='A' "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function
    Public Shared Function Get_indicador_det(Optional pIndicadorEnc As Integer = Nothing) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim sp As String = ""

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            sp = "SELECT IdIndicadorDet as codigo,descripcion from indicador_det where estado='A' "

            If pIndicadorEnc > 0 Then
                sp += " and IdIndicadorEnc = @IdIndicadorEnc "
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            If pIndicadorEnc > 0 Then
                dad.SelectCommand.Parameters.AddWithValue("@IdIndicadorEnc", pIndicadorEnc)
            End If

            dad.Fill(dt)

            cmd.Dispose()

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Resumen_Exitencia_Estado_Producto_Tramo_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_Resumen_Exitencia_Estado_Producto_Tramo_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT 
                                        NomEstado as EstadoProducto,
                                        Ubicacion_Tramo as Tramo,
                                        SUM(CantidadSF) AS Cantidad
                                    FROM 
                                        vw_stock_res  -- Suponiendo que esta es tu tabla de inventario
                                    WHERE 
                                        IdBodega = @IdBodega -- Filtra por el ID de la bodega, si es necesario
                                    GROUP BY 
                                        NomEstado, 
                                        Ubicacion_Tramo
                                    ORDER BY 
                                        Ubicacion_Tramo, NomEstado; "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class