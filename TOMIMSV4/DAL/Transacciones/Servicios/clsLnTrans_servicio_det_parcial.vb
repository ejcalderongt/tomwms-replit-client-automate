Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_servicio_det

    '#CKFK 20210622 Creé esta función para obtener los servicios para enviar al ERP
    Public Shared Function Get_Servicios_By_Rango_Fechas(ByVal pIdCliente As Integer,
                                                         ByVal pIdConsolidador As Integer,
                                                         ByVal pNoOrden As String,
                                                         ByVal FechaDesde As Date,
                                                         ByVal FechaHasta As Date,
                                                         ByVal Almacen As clsDataContractDI.tTipoAlmacen,
                                                         ByVal pCodigoProducto As String) As List(Of clsBe_Servicio_Logistico)

        Dim lServicioCealsa As New List(Of clsBe_Servicio_Logistico)
        Dim vTipoDocIngreso As New clsDataContractDI.tTipoDocumentoIngreso

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lServicio As List(Of clsBe_Servicio) = clsLn_servicio.Get_All_By_Rango_Fechas(pIdCliente,
                                                                                                      pNoOrden,
                                                                                                      FechaDesde,
                                                                                                      FechaHasta,
                                                                                                      Almacen,
                                                                                                      pCodigoProducto,
                                                                                                     lConnection,
                                                                                                     lTransaction)

                    Dim BeServicioCealsa As New clsBe_Servicio_Logistico

                    For Each S In lServicio

                        BeServicioCealsa = New clsBe_Servicio_Logistico

                        vTipoDocIngreso = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                            BeServicioCealsa.IdConsolidador = clsLnTrans_oc_enc.Get_IdPropietario_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        Else
                            BeServicioCealsa.IdConsolidador = 0
                        End If

                        If BeServicioCealsa.IdConsolidador = pIdConsolidador Then

                            BeServicioCealsa.Almacen = IIf(S.Almacen = "General", clsDataContractDI.tTipoAlmacen.General, clsDataContractDI.tTipoAlmacen.Fiscal)
                            BeServicioCealsa.IdCliente = S.IdCliente
                            BeServicioCealsa.No_orden = S.No_orden
                            BeServicioCealsa.No_Linea = S.No_Linea
                            BeServicioCealsa.Codigo_producto = S.Codigo_producto
                            BeServicioCealsa.Nombre_Producto = S.Nombre_Producto
                            BeServicioCealsa.Cantidad = S.Cantidad
                            BeServicioCealsa.Fecha_Servicio = S.Fecha_Servicio

                            lServicioCealsa.Add(BeServicioCealsa)

                        End If

                    Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lServicioCealsa

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    '#CKFK 20210625 Creé esta función para obtener los servicios para enviar al ERP solo por fecha
    Public Shared Function Get_Servicios_By_Rango_Fechas(ByVal FechaDesde As Date,
                                                         ByVal FechaHasta As Date) As List(Of clsBe_Servicio_Logistico)

        Dim lServicioCealsa As New List(Of clsBe_Servicio_Logistico)
        Dim vTipoDocIngreso As New clsDataContractDI.tTipoDocumentoIngreso

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lServicio As List(Of clsBe_Servicio) = clsLn_servicio.Get_All_By_Rango_Fechas(FechaDesde,
                                                                                                      FechaHasta,
                                                                                                     lConnection,
                                                                                                     lTransaction)

                    Dim BeServicioCealsa As New clsBe_Servicio_Logistico

                    For Each S In lServicio

                        BeServicioCealsa = New clsBe_Servicio_Logistico

                        vTipoDocIngreso = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                            BeServicioCealsa.IdConsolidador = clsLnTrans_oc_enc.Get_IdPropietario_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        Else
                            BeServicioCealsa.IdConsolidador = 0
                        End If

                        BeServicioCealsa.Almacen = S.Almacen
                        BeServicioCealsa.IdCliente = S.IdCliente
                        BeServicioCealsa.No_orden = S.No_orden
                        BeServicioCealsa.No_Linea = S.No_Linea
                        BeServicioCealsa.Codigo_producto = S.Codigo_producto
                        BeServicioCealsa.Nombre_Producto = S.Nombre_Producto
                        BeServicioCealsa.Cantidad = S.Cantidad
                        BeServicioCealsa.Fecha_Servicio = S.Fecha_Servicio

                        lServicioCealsa.Add(BeServicioCealsa)

                    Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lServicioCealsa

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdServicioEnc As Integer, ByVal IdServicioDet As Integer) As clsBeTrans_servicio_det

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_servicio_det " &
            " Where(IdServicioEnc = @IdServicioEnc AND IdServicioDet = @IdServicioDet)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdServicioEnc", IdServicioEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdServicioDet", IdServicioDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_servicio_det As New clsBeTrans_servicio_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_servicio_det, lDataTable.Rows(0))
                            vBeTrans_servicio_det.IsNew = False
                            GetSingle = vBeTrans_servicio_det
                        End If

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
