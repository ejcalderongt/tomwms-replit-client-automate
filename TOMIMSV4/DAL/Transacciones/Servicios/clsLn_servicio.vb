Imports System.Data.SqlClient

Public Class clsLn_servicio

    Public Shared Sub Cargar(ByRef oBe_servicio As clsBe_Servicio, ByRef dr As DataRow)
        Try
            With oBe_servicio
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Almacen = IIf(IsDBNull(dr.Item("Almacen")), "", dr.Item("Almacen"))
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Nombre_Cliente = IIf(IsDBNull(dr.Item("Nombre_Cliente")), "", dr.Item("Nombre_Cliente"))
                .IdPropietario_Enc = IIf(IsDBNull(dr.Item("IdPropietario_Enc")), 0, dr.Item("IdPropietario_Enc"))
                .No_orden = IIf(IsDBNull(dr.Item("no_orden")), "", dr.Item("no_orden"))
                .Tipo_Transaccion = IIf(IsDBNull(dr.Item("Tipo_Transaccion")), "", dr.Item("Tipo_Transaccion"))
                .No_Linea = IIf(IsDBNull(dr.Item("No_Linea")), 0, dr.Item("No_Linea"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Nombre_Producto = IIf(IsDBNull(dr.Item("Nombre_Producto")), "", dr.Item("Nombre_Producto"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0, dr.Item("Cantidad"))
                .Fecha_Servicio = IIf(IsDBNull(dr.Item("Fecha_Servicio")), New Date(1900, 1, 1), dr.Item("Fecha_Servicio"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM vw_servicio"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBe_Servicio)

        Dim lReturnList As New List(Of clsBe_Servicio)

        Try

            Const sp As String = "SELECT * FROM VW_servicio"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBe_servicio As New clsBe_Servicio

                        For Each dr As DataRow In lDataTable.Rows
                            vBe_servicio = New clsBe_Servicio()
                            Cargar(vBe_servicio, dr)
                            lReturnList.Add(vBe_servicio)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeBe_servicio As clsBe_Servicio)

        Try

            Const sp As String = "SELECT * FROM vw_servicio"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBe_servicio As New clsBe_Servicio

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBe_servicio, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_All_By_Rango_Fechas(ByVal pIdCliente As Integer,
                                                   ByVal pNoOrden As String,
                                                   ByVal FechaDesde As Date,
                                                   ByVal FechaHasta As Date,
                                                   ByVal pAlmacen As clsDataContractDI.tTipoAlmacen,
                                                   ByVal pCodigoProducto As String,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBe_Servicio)

        Dim lReturnList As New List(Of clsBe_Servicio)

        Try

            Const sp As String = "SELECT * FROM VW_servicio
                                  WHERE Fecha_Servicio BETWEEN @Desde AND @Hasta
									AND IdCliente = @IdCliente 
									AND (no_orden = @no_orden)
                                    AND Almacen = @Almacen
                                    AND Codigo_Producto = @Codigo_Producto"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)
                lDTA.SelectCommand.Parameters.AddWithValue("@no_orden", pNoOrden)
                lDTA.SelectCommand.Parameters.AddWithValue("@Almacen", pAlmacen)
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Producto", pCodigoProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBe_servicio As New clsBe_Servicio

                For Each dr As DataRow In lDataTable.Rows
                    vBe_servicio = New clsBe_Servicio()
                    Cargar(vBe_servicio, dr)
                    lReturnList.Add(vBe_servicio)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Rango_Fechas(ByVal FechaDesde As Date,
                                                   ByVal FechaHasta As Date,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBe_Servicio)

        Dim lReturnList As New List(Of clsBe_Servicio)

        Try

            Const sp As String = "SELECT * FROM VW_servicio
                                  WHERE Fecha_Servicio BETWEEN @Desde AND @Hasta"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBe_servicio As New clsBe_Servicio

                For Each dr As DataRow In lDataTable.Rows
                    vBe_servicio = New clsBe_Servicio()
                    Cargar(vBe_servicio, dr)
                    lReturnList.Add(vBe_servicio)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
