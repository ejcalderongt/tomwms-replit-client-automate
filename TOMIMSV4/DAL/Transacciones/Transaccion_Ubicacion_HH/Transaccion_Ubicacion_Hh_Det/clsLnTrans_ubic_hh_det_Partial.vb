Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_ubic_hh_det

    Public Shared Function Get_All_By_Id_Trans_Ubic_Enc(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_det)

        Dim lReturnList As New List(Of clsBeTrans_ubic_hh_det)

        Try

            Dim vSQL As String = "SELECT * from VW_TransUbicHhDet 
                                  WHERE IdTareaUbicacionEnc = @IdTareaUbicacionEnc and activo=1 
                                  ORDER BY IdTareaUbicacionDet "

            '#EJC20180419:1109PM: Se agrego transaccionalidad a función.
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", pIdTransUbicHhEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_ubic_hh_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_ubic_hh_det

                                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("IdStock"), Integer)
                                    Obj.Stock.IdStock = CType(lRow("IdStock"), Integer)
                                    Obj.Stock = clsLnStock.GetSingle(Obj.Stock.IdStock, lConnection, lTransaction)
                                    '#EJC20180613: If the obj stock doesn't exist, means that the IdStock also doesn't exist any more
                                    If Obj.Stock Is Nothing Then Obj.Stock = New clsBeStock
                                End If

                                If lRow("IdTareaUbicacionDet") IsNot DBNull.Value AndAlso lRow("IdTareaUbicacionDet") IsNot Nothing Then
                                    Obj.IdTareaUbicacionDet = CType(lRow("IdTareaUbicacionDet"), Integer)
                                End If

                                If lRow("IdTareaUbicacionEnc") IsNot DBNull.Value AndAlso lRow("IdTareaUbicacionEnc") IsNot Nothing Then
                                    Obj.IdTareaUbicacionEnc = CType(lRow("IdTareaUbicacionEnc"), Integer)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Stock.Fecha_vence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("idProducto") IsNot DBNull.Value AndAlso lRow("idProducto") IsNot Nothing Then
                                    Obj.Producto.IdProducto = CType(lRow("idProducto"), Integer)
                                    Obj.Producto = clsLnProducto.GetSingle(Obj.Producto.IdProducto,
                                                                           lConnection,
                                                                           lTransaction)
                                    Obj.Producto.IdProducto = CType(lRow("idProducto"), Integer)
                                    Obj.Producto.Nombre = CType(lRow("Nombre"), String)
                                    Obj.Producto.IdProductoBodega = CType(lRow("idProductoBodega"), Integer)
                                    Obj.ProductoEstado.Nombre = CType(lRow("NomEstado"), String)
                                End If

                                If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                    Obj.UnidadMedida.IdUnidadMedida = lRow("IdUnidadMedidaBasica")
                                    Obj.UnidadMedida = clsLnUnidad_medida.GetSingle(Obj.UnidadMedida.IdUnidadMedida,
                                                                                    lConnection,
                                                                                    lTransaction)
                                End If

                                If lRow("Serial") IsNot DBNull.Value AndAlso lRow("Serial") IsNot Nothing Then
                                    Obj.Stock.Serial = CType(lRow("Serial"), String)
                                End If

                                If lRow("Añada") IsNot DBNull.Value AndAlso lRow("Añada") IsNot Nothing Then
                                    Obj.Stock.Añada = CType(lRow("Añada"), Integer)
                                End If

                                If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
                                    Obj.Stock.Lote = CType(lRow("Lote"), String)
                                End If

                                If lRow("Fecha_ingreso") IsNot DBNull.Value AndAlso lRow("Fecha_ingreso") IsNot Nothing Then
                                    Obj.Stock.Fecha_Ingreso = CType(lRow("Fecha_ingreso"), DateTime)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.ProductoPresentacion.IdPresentacion = lRow("IdPresentacion")
                                    Obj.Stock.IdPresentacion = Obj.ProductoPresentacion.IdPresentacion
                                    Obj.ProductoPresentacion = clsLnProducto_presentacion.GetSingle(Obj.ProductoPresentacion.IdPresentacion,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                                    Obj.ProductoPresentacion.Nombre = CType(lRow("Presentacion"), String)
                                End If

                                If lRow("UnidadMedida") IsNot DBNull.Value AndAlso lRow("UnidadMedida") IsNot Nothing Then
                                    Obj.UnidadMedida.Nombre = CType(lRow("UnidadMedida"), String)
                                End If

                                If lRow("IdUbicacionOrigen") IsNot DBNull.Value AndAlso lRow("IdUbicacionOrigen") IsNot Nothing Then
                                    Obj.IdUbicacionOrigen = CType(lRow("IdUbicacionOrigen"), Integer)
                                    Obj.UbicacionOrigen.IdUbicacion = Obj.IdUbicacionOrigen
                                    Obj.UbicacionOrigen.IdBodega = CType(lRow("IdBodega"), Integer)
                                    Obj.UbicacionOrigen = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Obj.IdUbicacionOrigen,
                                                                                                            Obj.UbicacionOrigen.IdBodega,
                                                                                                            lConnection,
                                                                                                            lTransaction)
                                    Obj.UbicacionOrigen.Descripcion = lRow("NombreCompletoUbicaiconOrigen")
                                End If

                                If lRow("IdUbicacionDestino") IsNot DBNull.Value AndAlso lRow("IdUbicacionDestino") IsNot Nothing Then
                                    Obj.IdUbicacionDestino = CType(lRow("IdUbicacionDestino"), Integer)
                                    Obj.UbicacionDestino.IdUbicacion = Obj.IdUbicacionDestino
                                    Obj.UbicacionDestino.IdBodega = CType(lRow("IdBodega"), Integer)
                                    Obj.UbicacionDestino = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Obj.IdUbicacionDestino,
                                                                                                             Obj.UbicacionDestino.IdBodega,
                                                                                                             lConnection,
                                                                                                             lTransaction)
                                    Obj.UbicacionDestino.Descripcion = lRow("NombreCompletoUbicaiconDestino")

                                End If

                                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                    Obj.Producto.Propietario.IdPropietario = CType(lRow("IdPropietario"), Integer)
                                    Obj.Producto.Propietario.Nombre_comercial = lRow("nombre_comercial")
                                End If

                                If lRow("NomEstado") IsNot DBNull.Value AndAlso lRow("NomEstado") IsNot Nothing Then
                                    Obj.Stock.ProductoEstado.IdEstado = CType(lRow("IdEstado"), Integer)
                                    Obj.Stock.ProductoEstado.Nombre = lRow("NomEstado")
                                End If

                                If lRow("IdOperadorBodega") IsNot DBNull.Value AndAlso lRow("IdOperadorBodega") IsNot Nothing Then
                                    Obj.IdOperadorBodega = CType(lRow("IdOperadorBodega"), Integer)
                                End If

                                If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                                    Obj.IdBodega = CType(lRow("IdBodega"), Integer)
                                End If

                                If lRow("Nombres") IsNot DBNull.Value AndAlso lRow("Nombres") IsNot Nothing Then
                                    Obj.Operador.Nombres = CType(lRow("Nombres"), String)
                                End If

                                If lRow("HoraInicio") IsNot DBNull.Value AndAlso lRow("HoraInicio") IsNot Nothing Then
                                    Obj.HoraInicio = CType(lRow("HoraInicio"), DateTime)
                                End If

                                If lRow("HoraFin") IsNot DBNull.Value AndAlso lRow("HoraFin") IsNot Nothing Then
                                    Obj.HoraFin = CType(lRow("HoraFin"), DateTime)
                                End If

                                If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                    Obj.Cantidad = CType(lRow("Cantidad"), Double)
                                End If

                                If lRow("Recibido") IsNot DBNull.Value AndAlso lRow("Recibido") IsNot Nothing Then
                                    Obj.Recibido = CType(lRow("Recibido"), Double)
                                End If

                                If lRow("Realizado") IsNot DBNull.Value AndAlso lRow("Realizado") IsNot Nothing Then
                                    Obj.Realizado = CType(lRow("Realizado"), Boolean)
                                End If

                                If lRow("Activo") IsNot DBNull.Value AndAlso lRow("Activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("Activo"), Boolean)
                                End If

                                If lRow("idEstadoDestino") IsNot DBNull.Value AndAlso lRow("idEstadoDestino") IsNot Nothing Then
                                    Obj.IdEstadoDestino = CType(lRow("idEstadoDestino"), Int32)
                                End If

                                If lRow("idEstadoOrigen") IsNot DBNull.Value AndAlso lRow("idEstadoOrigen") IsNot Nothing Then
                                    Obj.IdEstadoOrigen = CType(lRow("idEstadoOrigen"), Int32)
                                End If

                                If lRow("estado") IsNot DBNull.Value AndAlso lRow("estado") IsNot Nothing Then
                                    Obj.Estado = CType(lRow("estado"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

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

    Public Shared Function Get_All_By_Id_Trans_Ubic_Enc(ByVal pIdTransUbicHhEnc As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_ubic_hh_det)

        Dim lReturnList As New List(Of clsBeTrans_ubic_hh_det)

        Try

            Dim vSQL As String = "SELECT * from VW_TransUbicHhDet 
                        WHERE IdTareaUbicacionEnc = @IdTareaUbicacionEnc and activo=1 
                        ORDER BY tramo, indice_x,nivel, descripcion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", pIdTransUbicHhEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTrans_ubic_hh_det = New clsBeTrans_ubic_hh_det

                        If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdStock = CType(lRow("IdStock"), Integer)
                            BeTrans_ubic_hh_det.Stock.IdStock = CType(lRow("IdStock"), Integer)
                            BeTrans_ubic_hh_det.Stock = clsLnStock.GetSingle(BeTrans_ubic_hh_det.Stock.IdStock, lConnection, lTransaction)
                            '#EJC20180613: If the obj stock doesn't exist, means that the IdStock also doesn't exist any more
                            If BeTrans_ubic_hh_det.Stock Is Nothing Then
                                BeTrans_ubic_hh_det.Stock = New clsBeStock
                                BeTrans_ubic_hh_det.Stock.Lic_plate = CType(lRow("Lic_plate"), String)
                            End If
                        End If

                        If lRow("IdTareaUbicacionDet") IsNot DBNull.Value AndAlso lRow("IdTareaUbicacionDet") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdTareaUbicacionDet = CType(lRow("IdTareaUbicacionDet"), Integer)
                        End If

                        If lRow("IdTareaUbicacionEnc") IsNot DBNull.Value AndAlso lRow("IdTareaUbicacionEnc") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdTareaUbicacionEnc = CType(lRow("IdTareaUbicacionEnc"), Integer)
                        End If

                        If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Stock.Fecha_vence = CType(lRow("fecha_vence"), Date)
                        End If

                        If lRow("idProducto") IsNot DBNull.Value AndAlso lRow("idProducto") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Producto.IdProducto = CType(lRow("idProducto"), Integer)
                            BeTrans_ubic_hh_det.Producto = clsLnProducto.GetSingle(BeTrans_ubic_hh_det.Producto.IdProducto,
                                                                           lConnection,
                                                                           lTransaction)
                            BeTrans_ubic_hh_det.Producto.IdProducto = CType(lRow("idProducto"), Integer)
                            BeTrans_ubic_hh_det.Producto.Nombre = CType(lRow("Nombre"), String)
                            BeTrans_ubic_hh_det.Producto.IdProductoBodega = CType(lRow("idProductoBodega"), Integer)
                            BeTrans_ubic_hh_det.ProductoEstado.Nombre = CType(lRow("NomEstado"), String)
                        End If

                        If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                            BeTrans_ubic_hh_det.UnidadMedida.IdUnidadMedida = lRow("IdUnidadMedidaBasica")
                            BeTrans_ubic_hh_det.UnidadMedida = clsLnUnidad_medida.GetSingle(BeTrans_ubic_hh_det.UnidadMedida.IdUnidadMedida,
                                                                                    lConnection,
                                                                                    lTransaction)
                        End If

                        If lRow("Serial") IsNot DBNull.Value AndAlso lRow("Serial") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Stock.Serial = CType(lRow("Serial"), String)
                        End If

                        If lRow("Añada") IsNot DBNull.Value AndAlso lRow("Añada") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Stock.Añada = CType(lRow("Añada"), Integer)
                        End If

                        If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Stock.Lote = CType(lRow("Lote"), String)
                        End If

                        If lRow("Fecha_ingreso") IsNot DBNull.Value AndAlso lRow("Fecha_ingreso") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Stock.Fecha_Ingreso = CType(lRow("Fecha_ingreso"), DateTime)
                        End If

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            BeTrans_ubic_hh_det.ProductoPresentacion.IdPresentacion = lRow("IdPresentacion")
                            BeTrans_ubic_hh_det.Stock.IdPresentacion = BeTrans_ubic_hh_det.ProductoPresentacion.IdPresentacion
                            BeTrans_ubic_hh_det.ProductoPresentacion = clsLnProducto_presentacion.GetSingle(BeTrans_ubic_hh_det.ProductoPresentacion.IdPresentacion,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                            BeTrans_ubic_hh_det.ProductoPresentacion.Nombre = CType(lRow("Presentacion"), String)
                        End If

                        If lRow("UnidadMedida") IsNot DBNull.Value AndAlso lRow("UnidadMedida") IsNot Nothing Then
                            BeTrans_ubic_hh_det.UnidadMedida.Nombre = CType(lRow("UnidadMedida"), String)
                        End If

                        If lRow("IdUbicacionOrigen") IsNot DBNull.Value AndAlso lRow("IdUbicacionOrigen") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdUbicacionOrigen = CType(lRow("IdUbicacionOrigen"), Integer)
                            BeTrans_ubic_hh_det.UbicacionOrigen.IdUbicacion = BeTrans_ubic_hh_det.IdUbicacionOrigen
                            BeTrans_ubic_hh_det.UbicacionOrigen.IdBodega = CType(lRow("IdBodega"), Integer)
                            BeTrans_ubic_hh_det.UbicacionOrigen = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(BeTrans_ubic_hh_det.IdUbicacionOrigen,
                                                                                                            BeTrans_ubic_hh_det.UbicacionOrigen.IdBodega,
                                                                                                            lConnection,
                                                                                                            lTransaction)
                            BeTrans_ubic_hh_det.UbicacionOrigen.Descripcion = lRow("NombreCompletoUbicaiconOrigen")
                        End If

                        If lRow("IdUbicacionDestino") IsNot DBNull.Value AndAlso lRow("IdUbicacionDestino") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdUbicacionDestino = CType(lRow("IdUbicacionDestino"), Integer)
                            BeTrans_ubic_hh_det.UbicacionDestino.IdUbicacion = BeTrans_ubic_hh_det.IdUbicacionDestino
                            BeTrans_ubic_hh_det.UbicacionDestino.IdBodega = CType(lRow("IdBodega"), Integer)
                            BeTrans_ubic_hh_det.UbicacionDestino = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(BeTrans_ubic_hh_det.IdUbicacionDestino,
                                                                                                             BeTrans_ubic_hh_det.UbicacionDestino.IdBodega,
                                                                                                             lConnection,
                                                                                                             lTransaction)
                            BeTrans_ubic_hh_det.UbicacionDestino.Descripcion = lRow("NombreCompletoUbicaiconDestino")
                        End If

                        If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Producto.Propietario.IdPropietario = CType(lRow("IdPropietario"), Integer)
                            BeTrans_ubic_hh_det.Producto.Propietario.Nombre_comercial = lRow("nombre_comercial")
                        End If

                        If lRow("NomEstado") IsNot DBNull.Value AndAlso lRow("NomEstado") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Stock.ProductoEstado.IdEstado = CType(lRow("IdEstado"), Integer)
                            BeTrans_ubic_hh_det.Stock.ProductoEstado.Nombre = lRow("NomEstado")
                        End If

                        If lRow("IdOperadorBodega") IsNot DBNull.Value AndAlso lRow("IdOperadorBodega") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdOperadorBodega = CType(lRow("IdOperadorBodega"), Integer)
                            '#CKFK20230126 Agregué esto porque el objeto Operador quedaba nothing
                            BeTrans_ubic_hh_det.Operador = clsLnOperador.Get_Single_By_IdOperadorBodega(BeTrans_ubic_hh_det.IdOperadorBodega, lConnection, lTransaction)
                        End If

                        If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdBodega = CType(lRow("IdBodega"), Integer)
                        End If

                        If lRow("Nombres") IsNot DBNull.Value AndAlso lRow("Nombres") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Operador.Nombres = CType(lRow("Nombres"), String)
                        End If

                        If lRow("HoraInicio") IsNot DBNull.Value AndAlso lRow("HoraInicio") IsNot Nothing Then
                            BeTrans_ubic_hh_det.HoraInicio = CType(lRow("HoraInicio"), DateTime)
                        End If

                        If lRow("HoraFin") IsNot DBNull.Value AndAlso lRow("HoraFin") IsNot Nothing Then
                            BeTrans_ubic_hh_det.HoraFin = CType(lRow("HoraFin"), DateTime)
                        End If

                        If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Cantidad = CType(lRow("Cantidad"), Double)
                        End If

                        If lRow("Recibido") IsNot DBNull.Value AndAlso lRow("Recibido") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Recibido = CType(lRow("Recibido"), Double)
                        End If

                        If lRow("Realizado") IsNot DBNull.Value AndAlso lRow("Realizado") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Realizado = CType(lRow("Realizado"), Boolean)
                        End If

                        If lRow("Activo") IsNot DBNull.Value AndAlso lRow("Activo") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Activo = CType(lRow("Activo"), Boolean)
                        End If

                        If lRow("idEstadoDestino") IsNot DBNull.Value AndAlso lRow("idEstadoDestino") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdEstadoDestino = CType(lRow("idEstadoDestino"), Int32)
                        End If

                        If lRow("idEstadoOrigen") IsNot DBNull.Value AndAlso lRow("idEstadoOrigen") IsNot Nothing Then
                            BeTrans_ubic_hh_det.IdEstadoOrigen = CType(lRow("idEstadoOrigen"), Int32)
                        End If

                        If lRow("estado") IsNot DBNull.Value AndAlso lRow("estado") IsNot Nothing Then
                            BeTrans_ubic_hh_det.Estado = CType(lRow("estado"), String)
                        End If

                        If lRow("No_Linea") IsNot DBNull.Value AndAlso lRow("No_Linea") IsNot Nothing Then
                            BeTrans_ubic_hh_det.No_Linea = CType(lRow("No_Linea"), Integer)
                        End If

                        lReturnList.Add(BeTrans_ubic_hh_det)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdTransUbicEnc_And_IdOperador(ByVal pIdTransUbicHhEnc As Integer, ByVal pIdOperador As Integer, MostrarSoloIncompletas As Boolean) As List(Of clsBeTrans_ubic_hh_det)

        Dim lReturnList As New List(Of clsBeTrans_ubic_hh_det)
        Dim lDataList As New List(Of clsBeTrans_ubic_hh_det)

        Try

            If pIdOperador > 0 Then
                lDataList = Get_All_By_Id_Trans_Ubic_Enc(pIdTransUbicHhEnc)
                lReturnList = lDataList.Where(Function(x) x.IdOperadorBodega = pIdOperador).ToList

                If MostrarSoloIncompletas Then lReturnList = lReturnList.Where(Function(x) x.Realizado = False).ToList
            Else
                lReturnList = Get_All_By_Id_Trans_Ubic_Enc(pIdTransUbicHhEnc)
                If MostrarSoloIncompletas Then lReturnList = lReturnList.Where(Function(x) x.Realizado = False).ToList
            End If

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdTransUbicHhDet As Integer) As clsBeTrans_ubic_hh_det

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * from trans_ubic_hh_det where IdTareaUbicacionDet=@IdTransUbicHhDet"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", IdTransUbicHhDet)

                        Dim lDT As New DataTable()

                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Dim Obj As New clsBeTrans_ubic_hh_det()

                            Cargar(Obj, lRow)

                            If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then

                                Obj.Producto.Nombre = CType(lRow("Nombre"), String)

                            End If

                            If lRow("IdUbicacionDestino") IsNot DBNull.Value AndAlso lRow("IdUbicacionDestino") IsNot Nothing Then

                                Obj.UbicacionDestino.Descripcion = CType(lRow("Descripcion"), String)

                            End If

                            If lRow("IdOperador") IsNot DBNull.Value AndAlso lRow("IdOperador") IsNot Nothing Then

                                Obj.Operador.Nombres = CType(lRow("Nombres"), String)

                            End If

                            Return Obj

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

    Public Shared Function Guardar_Detalle(ByVal oBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det,
                                           ByVal pMovimiento As clsBeTrans_movimientos,
                                           ByVal pIdReabastecimientoLog As Integer,
                                           Optional ByVal pPosiciones As Integer = 0) As Boolean

        Dim det As New clsBeTrans_ubic_hh_det

        Guardar_Detalle = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If oBeTrans_ubic_hh_det.ProductoPresentacion.IdPresentacion <> 0 Then
                pMovimiento.Cantidad = pMovimiento.Cantidad * oBeTrans_ubic_hh_det.ProductoPresentacion.Factor
            End If

            pMovimiento.IdMovimiento = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)

            If (pIdReabastecimientoLog = 0) Then

                clsLnTrans_movimientos.Aplicar(pMovimiento,
                                               oBeTrans_ubic_hh_det.Stock.IdStock,
                                               False,
                                               lConnection,
                                               lTransaction,
                                               pPosiciones)

            Else

                clsLnTrans_movimientos.Aplicar_Con_Reabastecimiento(pMovimiento,
                                                                     oBeTrans_ubic_hh_det.Stock.IdStock,
                                                                     False,
                                                                     lConnection,
                                                                     lTransaction,
                                                                     pIdReabastecimientoLog,
                                                                     pPosiciones)

            End If

            clsLnTrans_movimientos.Insertar(pMovimiento,
                                            lConnection,
                                            lTransaction)

            If Not oBeTrans_ubic_hh_det Is Nothing Then
                GetSingle(det)
                oBeTrans_ubic_hh_det.Recibido = oBeTrans_ubic_hh_det.Recibido + det.Recibido
                Actualizar(oBeTrans_ubic_hh_det, lConnection, lTransaction)
            End If

            ''#EJC20210304: Actualizar banderas de reabastecimiento.
            'If Not (pIdReabastecimientoLog = 0) Then

            '    Dim BeTransReabasto As New clsBeTrans_reabastecimiento_log()
            '    BeTransReabasto = clsLnTrans_reabastecimiento_log.GetSingle(pIdReabastecimientoLog, lConnection, lTransaction)

            '    If Not BeTransReabasto Is Nothing Then
            '        BeTransReabasto.Fecha_Procesamiento_HH = Now
            '        BeTransReabasto.Procesado_HH = True
            '        clsLnTrans_reabastecimiento_log.Actualizar_Procesamiento_HH(BeTransReabasto, lConnection, lTransaction)
            '    End If

            'End If

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Aplicar_Movimiento(ByVal pMovimiento As clsBeTrans_movimientos, idstock As Integer) As String

        Aplicar_Movimiento = ""

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim idMaxmov = clsLnTrans_movimientos.MaxID(lConnection, lTransaction) + 1

            Dim result As String = clsLnTrans_movimientos.Aplicar(pMovimiento, idstock, True, lConnection, lTransaction)

            pMovimiento.IdMovimiento = idMaxmov
            clsLnTrans_movimientos.Insertar(pMovimiento, lConnection, lTransaction)

            lTransaction.Commit()

            Return result

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Aplica_LP_Stock(ByVal pMovimiento As clsBeTrans_movimientos,
                                           ByVal pStockRes As clsBeVW_stock_res,
                                           ByVal pIdResolucionLp As Integer) As String

        Aplica_LP_Stock = ""

        Dim IdMaxMov As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim ListaStock As New List(Of clsBeVW_stock_res)
            Dim result As String = ""
            Dim vCantidadCompletada As Double = 0
            Dim vCantidadPendiente As Double = 0
            Dim vCantidadDisponible As Double = 0

            '#EJC20200117: Almacenar aquí temporalmente, para hacer la lista de stock en base a lic_plate anterior.
            '#AT20220316 Agregué la presentación
            Dim vNuevoLicPlate As String = pStockRes.Lic_plate
            Dim vPresentacion As Integer = pStockRes.IdPresentacion

            pStockRes.Lic_plate = pStockRes.Lic_plate_Anterior
            pStockRes.IdPresentacion = pStockRes.IdPresentacion_Anterior

            If pStockRes.Lic_plate_Anterior = "" Then
                ListaStock = clsLnVW_stock_res.Get_Lista_Stock(pStockRes, lConnection, lTransaction)
            Else
                ListaStock = clsLnVW_stock_res.Get_Lista_Stock_By_Lic_Plate(pStockRes, lConnection, lTransaction)
            End If

            '#EJC20200117: Resetear de nuevo el LP para que inserte el nuevo LP en el stock.
            '#AT20220316 Agregué la presentación
            pStockRes.Lic_plate = vNuevoLicPlate
            pStockRes.IdPresentacion = vPresentacion

            vCantidadPendiente = pStockRes.CantidadUmBas

            If Not ListaStock Is Nothing Then

                If ListaStock.Count > 0 Then

                    For Each StockRes In ListaStock

                        vCantidadDisponible = Math.Round(StockRes.CantidadUmBas - StockRes.CantidadReservadaUMBas, 6) '#CKFK 20181025 0547PM Agregué el redondeo a 6 cifras decimales cuando hace la resta

                        pMovimiento.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(pMovimiento.IdBodegaDestino,
                                                                                                                                  pMovimiento.IdPropietarioBodega,
                                                                                                                                  lConnection,
                                                                                                                                  lTransaction)

                        If vCantidadDisponible > 0 Then

                            If vCantidadPendiente >= vCantidadDisponible Then

                                pMovimiento.Cantidad = vCantidadDisponible
                                pMovimiento.Barra_pallet = pStockRes.Lic_plate

                                result += " " & clsLnTrans_movimientos.Aplicar_Packing(pMovimiento,
                                                                                       StockRes.IdStock,
                                                                                       vNuevoLicPlate,
                                                                                       vPresentacion,
                                                                                       lConnection,
                                                                                       lTransaction)

                                IdMaxMov = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)

                                pMovimiento.IdMovimiento = IdMaxMov

                                clsLnTrans_movimientos.Insertar(pMovimiento, lConnection, lTransaction)

                                vCantidadPendiente -= vCantidadDisponible
                                vCantidadPendiente = Math.Round(vCantidadPendiente, 6)

                                vCantidadCompletada = (vCantidadPendiente = 0)

                                If vCantidadCompletada Then Exit For

                            ElseIf vCantidadPendiente < vCantidadDisponible Then

                                pMovimiento.Cantidad = vCantidadPendiente

                                pMovimiento.Barra_pallet = vNuevoLicPlate

                                result += " " & clsLnTrans_movimientos.Aplicar_Packing(pMovimiento,
                                                                                       StockRes.IdStock,
                                                                                       vNuevoLicPlate,
                                                                                       vPresentacion,
                                                                                       lConnection,
                                                                                       lTransaction)

                                IdMaxMov = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)

                                pMovimiento.IdMovimiento = IdMaxMov

                                clsLnTrans_movimientos.Insertar(pMovimiento, lConnection, lTransaction)

                                vCantidadPendiente -= vCantidadPendiente
                                vCantidadPendiente = Math.Round(vCantidadPendiente, 6)

                                vCantidadCompletada = (vCantidadPendiente = 0)

                                If vCantidadCompletada Then Exit For

                            End If

                            '#MECR19112025: Se agregó bitacora de logs para implosion
                            Dim vMsgInformacion As String = "Se agrego implosion, Licencia: " + pMovimiento.Lic_plate + " por el operador: " + pMovimiento.IdOperadorBodega.ToString()
                            clsLnLog_error_wms_pack.Agregar_Error(vMsgInformacion,
                                                                  pIdEmpresa:=pMovimiento.IdEmpresa,
                                                                  pIdBodega:=pMovimiento.IdBodegaDestino,
                                                                  pIdPedidoEnc:=pMovimiento.IdPedidoEnc,
                                                                  pIdDespachoEnc:=pMovimiento.IdDespachoEnc,
                                                                  pIdProductoBodega:=pMovimiento.IdProductoBodega,
                                                                  pIdPresentacion:=pMovimiento.IdPresentacion,
                                                                  pIdUnidadMedida:=pMovimiento.IdUnidadMedida,
                                                                  pLic_Plate:=pMovimiento.Lic_plate,
                                                                  pIdOperador:=pMovimiento.IdOperadorBodega,
                                                                  pUsuario_agr:=pMovimiento.Usuario_agr,
                                                                  pEsImplosion:=True)

                        Else
                            Throw New Exception("No hay cantidad disponible para implosionar")
                        End If

                    Next

                Else
                    Throw New Exception("Error #EJC20200117_A: No se obtuvieron registros con los parámetros solicitados")
                End If
            Else
                Throw New Exception("Error #EJC20200117_B: No se obtuvieron registros con los parámetros solicitados")
            End If

            '#CKFK20210617: Incrementar contador de LP.
            If pIdResolucionLp <> 0 Then

                Dim BeResolLp As New clsBeResolucion_lp_operador()
                BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucionLp, lConnection, lTransaction)

                If Not BeResolLp Is Nothing Then
                    BeResolLp.Correlativo_Actual += 1
                    clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp, lConnection, lTransaction)
                End If

            End If


            lTransaction.Commit()

            Return result

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#AT20240703 Para completar el proceso mixto de implosión
    Public Shared Function Aplica_LP_Stock_Mixto(ByVal pStockResList As List(Of clsBeVW_stock_res),
                                                ByVal pIdResolucionLp As Integer) As Boolean

        Aplica_LP_Stock_Mixto = False
        Try

            For Each BeVWStockRes As clsBeVW_stock_res In pStockResList
                Aplica_LP_Stock(BeVWStockRes.Movimiento, BeVWStockRes, pIdResolucionLp)
            Next

            Return Aplica_LP_Stock_Mixto = True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Aplica_Cambio_Estado_Ubic(ByVal pMovimiento As clsBeTrans_movimientos,
                                                     ByVal pStockRes As clsBeVW_stock_res,
                                                     ByRef pIdStockNuevo As Integer,
                                                     ByRef pIdMovimiento As Integer,
                                                     Optional pPosiciones As Integer = 0) As Boolean

        Aplica_Cambio_Estado_Ubic = False

        Dim ListaStock As New List(Of clsBeVW_stock_res)
        Dim result As String = ""
        Dim vCantidadCompletada As Double = 0
        Dim vCantidadPendiente As Double = 0
        Dim vCantidadDisponible As Double = 0
        Dim IdMovimiento As Integer
        Dim IdStockNuevo As Integer = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim BePickingUbic As New clsBeTrans_picking_ubic()
        Dim stopwatch As Stopwatch = stopwatch.StartNew()

        If pMovimiento.IdTipoTarea = 0 Then
            Throw New Exception("ERROR_20220909_0724: " & "El identificador de tipo de tarea es incorrecto, salga de la pantalla e intente nuevamente por favor.")
        End If

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = "AVISO_20242211_HH_CambioEstadoUbic: ubicacion: " & pStockRes.IdUbicacion & " ubicacion anterior " & pStockRes.IdUbicacion_Anterior & "opoerador " & pMovimiento.IdOperadorBodega
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,pMovimiento.IdEmpresa,pMovimiento.Usuario_agr,pIdMovimiento,pStockRes.IdStock,
                                                  pMovimiento.IdUnidadMedida,pMovimiento.IdPresentacion,pMovimiento.IdUbicacionOrigen,
                                                  pMovimiento.IdUbicacionDestino,pMovimiento.IdEstadoOrigen,pMovimiento.IdEstadoDestino,
                                                  pMovimiento.Cantidad,pMovimiento.IdOperadorBodega,
                                                  pTransaction:=lTransaction,
                                                  pConection:=lConnection)

            If pStockRes.Lic_plate = "" Then
                ListaStock = clsLnVW_stock_res.Get_Lista_Stock(pStockRes,
                                                               lConnection,
                                                               lTransaction)
            Else
                ListaStock = clsLnVW_stock_res.Get_Lista_Stock_By_Lic_Plate(pStockRes,
                                                                            lConnection,
                                                                            lTransaction)
            End If

            vCantidadPendiente = pStockRes.CantidadUmBas

            If Not ListaStock Is Nothing Then

                If ListaStock.Count > 0 Then

                    For Each StockRes In ListaStock

                        If pStockRes.IdUbicacionVirtual = 0 Then

                            '#CKFK 20181025 0547PM Agregué el redondeo a 6 cifras decimales cuando hace la resta
                            vCantidadDisponible = Math.Round(StockRes.CantidadUmBas - StockRes.CantidadReservadaUMBas, 6)

                            'CM_20190523: Agregué método para buscar el propietario bodega, porque de la HH trae el IdPropietario
                            pMovimiento.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(pMovimiento.IdBodegaDestino,
                                                                                                                                      pMovimiento.IdPropietarioBodega,
                                                                                                                                      lConnection,
                                                                                                                                      lTransaction)

                            '#CKFK 20181113 Agregué esta validación para que validara que la cantidad disponible fuera mayor que 0.
                            If vCantidadDisponible > 0 Then

                                If vCantidadPendiente >= vCantidadDisponible Then

                                    pMovimiento.Cantidad = vCantidadDisponible

                                    IdStockNuevo = clsLnTrans_movimientos.Aplicar(pMovimiento,
                                                                                  StockRes.IdStock,
                                                                                  True,
                                                                                  lConnection,
                                                                                  lTransaction,
                                                                                  pPosiciones)

                                    IdMovimiento = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)

                                    pMovimiento.IdMovimiento = IdMovimiento
                                    pMovimiento.IdUnidadMedida = StockRes.IdUnidadMedida

                                    If pMovimiento.IdTipoTarea = 20 Then
                                        pMovimiento.IdPresentacion = 0
                                    End If

                                    clsLnTrans_movimientos.Insertar(pMovimiento,
                                                                    lConnection,
                                                                    lTransaction)

                                    vCantidadPendiente -= vCantidadDisponible
                                    '#CKFK 20181025 0547PM Agregué el redondeo a 6 cifras decimales cuando hace la resta
                                    vCantidadPendiente = Math.Round(vCantidadPendiente, 6)

                                    vCantidadCompletada = (vCantidadPendiente = 0)

                                    If vCantidadCompletada Then Exit For

                                ElseIf vCantidadPendiente < vCantidadDisponible Then

                                    pMovimiento.Cantidad = vCantidadPendiente

                                    IdStockNuevo = clsLnTrans_movimientos.Aplicar(pMovimiento,
                                                                                  StockRes.IdStock,
                                                                                  True,
                                                                                  lConnection,
                                                                                  lTransaction,
                                                                                  pPosiciones)

                                    IdMovimiento = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)

                                    pMovimiento.IdMovimiento = IdMovimiento
                                    pMovimiento.IdUnidadMedida = StockRes.IdUnidadMedida

                                    clsLnTrans_movimientos.Insertar(pMovimiento,
                                                                    lConnection,
                                                                    lTransaction)

                                    vCantidadPendiente -= vCantidadPendiente
                                    vCantidadPendiente = Math.Round(vCantidadPendiente, 6)

                                    vCantidadCompletada = (vCantidadPendiente = 0)

                                    If vCantidadCompletada Then Exit For

                                End If

                            End If

                        Else '#EJC20220330 - IdUbicacionVirtual <> 0

                            'Actualizar, IdUbicacionVirtual en picking y no hacer nada mas!!!
                            BePickingUbic = clsLnTrans_picking_ubic.Get_Single_By_IdStock(StockRes.IdStock,
                                                                                          StockRes.IdBodega,
                                                                                          lConnection,
                                                                                          lTransaction)

                            If Not BePickingUbic Is Nothing Then
                                BePickingUbic.IdUbicacionTemporal = pStockRes.IdUbicacionVirtual
                                clsLnTrans_picking_ubic.Actualizar_IdUbicacionTemporal(BePickingUbic,
                                                                                       lConnection,
                                                                                       lTransaction)
                            End If

                        End If

                    Next

                    pIdStockNuevo = IdStockNuevo
                    pIdMovimiento = IdMovimiento

                    Aplica_Cambio_Estado_Ubic = (IdStockNuevo <> 0)

                Else
                    '#MECR03112025: Se agrego bitacora de ubicacion
                    'clsLnLog_error_wms.Agregar_Error(pMovimiento.IdEmpresa, pMovimiento.IdBodegaOrigen, "No se pudo obtener la información de stock, IdStock: " & pStockRes.IdStock)
                    Dim msgError As String = "No se pudo obtener la información de stock, IdStock: " & pStockRes.IdStock
                    clsLnLog_error_wms_ubic.Agregar_Error(msgError,
                                                          pIdEmpresa:=pMovimiento.IdEmpresa,
                                                          pUsrAgr:=pMovimiento.Usuario_agr,
                                                          pIdTareaUbicacionEnc:=pIdMovimiento,
                                                          pIdStock:=pStockRes.IdStock,
                                                          pIdUMBAs:=pMovimiento.IdUnidadMedida,
                                                          pIdPresentacion:=pMovimiento.IdPresentacion,
                                                          pIdUbicacionOrigen:=pMovimiento.IdUbicacionOrigen,
                                                          pIdUbicacionDestino:=pMovimiento.IdUbicacionDestino,
                                                          pIdEstadoOrigen:=pMovimiento.IdEstadoOrigen,
                                                          pIdEstadoDestino:=pMovimiento.IdEstadoDestino,
                                                          pCantidad:=pMovimiento.Cantidad,
                                                          pIdOperador:=pMovimiento.IdOperadorBodega,
                                                          pTransaction:=lTransaction,
                                                          pConection:=lConnection)

                    Throw New Exception("ERROR_202208241645A: Es probable que la licencia haya tenido una actualización, recargue la licencia.")
                End If

            Else
                '#MECR03112025: Se agrego bitacora de ubicacion
                'clsLnLog_error_wms.Agregar_Error(pMovimiento.IdEmpresa, pMovimiento.IdBodegaOrigen, "No se pudo obtener la información de stock, IdStock: " & pStockRes.IdStock)
                Dim msgError As String = "No se pudo obtener la información de stock, IdStock: " & pStockRes.IdStock
                clsLnLog_error_wms_ubic.Agregar_Error(msgError,
                                                          pIdEmpresa:=pMovimiento.IdEmpresa,
                                                          pUsrAgr:=pMovimiento.Usuario_agr,
                                                          pIdTareaUbicacionEnc:=pIdMovimiento,
                                                          pIdStock:=pStockRes.IdStock,
                                                          pIdUMBAs:=pMovimiento.IdUnidadMedida,
                                                          pIdPresentacion:=pMovimiento.IdPresentacion,
                                                          pIdUbicacionOrigen:=pMovimiento.IdUbicacionOrigen,
                                                          pIdUbicacionDestino:=pMovimiento.IdUbicacionDestino,
                                                          pIdEstadoOrigen:=pMovimiento.IdEstadoOrigen,
                                                          pIdEstadoDestino:=pMovimiento.IdEstadoDestino,
                                                          pCantidad:=pMovimiento.Cantidad,
                                                          pIdOperador:=pMovimiento.IdOperadorBodega,
                                                          pTransaction:=lTransaction,
                                                          pConection:=lConnection)

                Throw New Exception("ERROR_202208241645B: No se pudo obtener la información de stock con los parámetros solicitados (lista is nothing)")
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Aplica_Cambio_Estado_Ubic_En_Picking(ByVal pMovimiento As clsBeTrans_movimientos,
                                                                ByVal pStockRes As clsBeVW_stock_res,
                                                                ByVal EsIdStockIgual As Boolean,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As String

        Aplica_Cambio_Estado_Ubic_En_Picking = ""

        Dim idMaxmov As Integer

        Try

            Dim ListaStock As New List(Of clsBeVW_stock_res)
            Dim result As String = ""
            Dim vCantidadCompletada As Double = 0
            Dim vCantidadPendiente As Double = 0
            Dim vCantidadDisponible As Double = 0

            ListaStock = clsLnVW_stock_res.Get_Lista_Stock(pStockRes,
                                                           lConnection,
                                                           lTransaction)

            vCantidadPendiente = pStockRes.CantidadUmBas

            For Each StockRes In ListaStock

                'StockRes.CantidadReservadaUMBas += pMovimiento.Cantidad
                vCantidadDisponible = Math.Round(StockRes.CantidadUmBas - StockRes.CantidadReservadaUMBas, 6) '#CKFK 20181025 0547PM Agregué el redondeo a 6 cifras decimales cuando hace la resta

                '#CKFK 20181113 Agregué esta validación para que validara que la cantidad disponible fuera mayor que 0.
                If vCantidadDisponible > 0 Then

                    If vCantidadPendiente >= vCantidadDisponible Then

                        pMovimiento.Cantidad = vCantidadDisponible

                        result += " " & clsLnTrans_movimientos.Aplicar_Cambio_Ubicacion_Automatico_Por_Picking(pMovimiento,
                                                                                                               StockRes.IdStock,
                                                                                                               EsIdStockIgual,
                                                                                                               lConnection,
                                                                                                               lTransaction)

                        idMaxmov = clsLnTrans_movimientos.MaxID(lConnection,
                                                                lTransaction)

                        pMovimiento.IdMovimiento = idMaxmov

                        clsLnTrans_movimientos.Insertar(pMovimiento,
                                                        lConnection,
                                                        lTransaction)

                        '#CKFK 20181025 0547PM Agregué el redondeo a 6 cifras decimales cuando hace la resta
                        vCantidadPendiente -= vCantidadDisponible
                        vCantidadPendiente = Math.Round(vCantidadPendiente, 6)

                        vCantidadCompletada = (vCantidadPendiente = 0)

                        If vCantidadCompletada Then
                            Exit For
                        End If

                    ElseIf vCantidadPendiente < vCantidadDisponible Then

                        pMovimiento.Cantidad = vCantidadPendiente

                        result += " " & clsLnTrans_movimientos.Aplicar_Cambio_Ubicacion_Automatico_Por_Picking(pMovimiento,
                                                                                                               StockRes.IdStock,
                                                                                                               EsIdStockIgual,
                                                                                                               lConnection,
                                                                                                               lTransaction)

                        idMaxmov = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)

                        pMovimiento.IdMovimiento = idMaxmov

                        clsLnTrans_movimientos.Insertar(pMovimiento,
                                                        lConnection,
                                                        lTransaction)

                        vCantidadPendiente -= vCantidadPendiente
                        vCantidadPendiente = Math.Round(vCantidadPendiente, 6) '#CKFK 20181025 0547PM Agregué el redondeo a 6 cifras decimales cuando hace la resta

                        vCantidadCompletada = (vCantidadPendiente = 0)

                        If vCantidadCompletada Then
                            Exit For
                        End If

                    End If

                End If

            Next

            Return result

        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=pMovimiento.IdEmpresa,
                                                  pLicencia:=pMovimiento.Lic_plate,
                                                  pIdStock:=pStockRes.IdStock,
                                                  pIdUMBAs:=pMovimiento.IdUnidadMedida,
                                                  pIdPresentacion:=pMovimiento.IdPresentacion,
                                                  pIdUbicacionOrigen:=pMovimiento.IdUbicacionOrigen,
                                                  pIdUbicacionDestino:=pMovimiento.IdUbicacionDestino,
                                                  pIdEstadoOrigen:=pMovimiento.IdEstadoOrigen,
                                                  pIdEstadoDestino:=pMovimiento.IdEstadoDestino,
                                                  pCantidad:=pMovimiento.Cantidad,
                                                  pIdOperador:=pMovimiento.IdOperadorBodega)

            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTareaUbicacionDet),0) FROM Trans_ubic_hh_det"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

    Public Shared Function Guardar_Trans_Ubic_HH_Det(ByVal IdTareaUbicacionEnc As Integer,
                                                     ByVal pListBeTransUbicHHDet As List(Of clsBeTrans_ubic_hh_det),
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As Boolean

        Dim BeTransUbicHHStock As clsBeTrans_ubic_hh_stock

        Guardar_Trans_Ubic_HH_Det = False

        Try

            If pListBeTransUbicHHDet IsNot Nothing AndAlso pListBeTransUbicHHDet.Count > 0 Then

                For Each BeTransUbicHHDet As clsBeTrans_ubic_hh_det In pListBeTransUbicHHDet

                    If BeTransUbicHHDet.IdTareaUbicacionEnc = 0 Then
                        Dim lMax As Integer = MaxID(lConnection, lTransaction) + 1
                        BeTransUbicHHDet.IdTareaUbicacionDet = lMax
                        BeTransUbicHHDet.IdTareaUbicacionEnc = IdTareaUbicacionEnc
                        Insertar(BeTransUbicHHDet, lConnection, lTransaction)
                    Else

                        If Not BeTransUbicHHDet.Activo Then

                            'Desactivado por -> '#EJC20171025_1050AM_REF:
                            'Eliminar(Obj, lConnection, lTransaction)

                            BeTransUbicHHStock = New clsBeTrans_ubic_hh_stock
                            BeTransUbicHHStock.IdTareaUbicacionEnc = IdTareaUbicacionEnc
                            BeTransUbicHHStock.IdTareaUbicacionDet = BeTransUbicHHDet.IdTareaUbicacionDet
                            BeTransUbicHHStock.IdStock = BeTransUbicHHDet.IdStock
                            BeTransUbicHHStock.IdStockTransUbicHHDet = clsLnTrans_ubic_hh_stock.GetIdStockTransUbicHHDet(BeTransUbicHHStock,
                                                                                                                         lConnection,
                                                                                                                         lTransaction)

                            '#EJC20171025_1043AM: Si la HH no procesó una ubicación y el BOF lo desactivó se libera el stock reservado.                            
                            clsLnStock_res.Eliminar_Stock_Res_Ubic_By_IdStock(IdTareaUbicacionEnc, BeTransUbicHHDet.IdStock, lConnection, lTransaction)

                        End If

                        '#EJC20171025_1050AM_REF: No eliminar el detalle, queda como bitácora, solo actualizar bandera activo = false.
                        Actualizar(BeTransUbicHHDet, lConnection, lTransaction)

                    End If

                Next

                Guardar_Trans_Ubic_HH_Det = True

            Else
                Throw New Exception("#EJC20211119_1647: La lista pListObjDet Is Nothing OrElse pListObjDet.Count = 0 ")
            End If

        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdTareaUbicacionEnc:=IdTareaUbicacionEnc)

            Throw ex
        End Try

    End Function

    Public Shared Function Procesar_Cambio_Ubicacion_Dirigido(ByRef oBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det,
                                                              ByVal pIdReabastecimientoLog As Integer) As Boolean

        Procesar_Cambio_Ubicacion_Dirigido = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeTransReabasto As New clsBeTrans_reabastecimiento_log()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Actualizar(oBeTrans_ubic_hh_det, lConnection, lTransaction)

            If Not (pIdReabastecimientoLog = 0) Then

                BeTransReabasto = clsLnTrans_reabastecimiento_log.GetSingle(pIdReabastecimientoLog, lConnection, lTransaction)

                If Not BeTransReabasto Is Nothing Then
                    BeTransReabasto.Fecha_Procesamiento_HH = Now
                    clsLnTrans_reabastecimiento_log.Actualizar_Procesamiento_HH(BeTransReabasto, lConnection, lTransaction)
                End If

            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class