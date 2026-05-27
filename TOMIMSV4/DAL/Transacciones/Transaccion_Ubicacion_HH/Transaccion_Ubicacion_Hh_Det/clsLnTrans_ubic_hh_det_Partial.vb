Imports System.Configuration
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
            Using lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))

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

            Using lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))

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

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If oBeTrans_ubic_hh_det.ProductoPresentacion.IdPresentacion <> 0 Then
                pMovimiento.Cantidad = pMovimiento.Cantidad * oBeTrans_ubic_hh_det.ProductoPresentacion.Factor
            End If

            '#EJC20260526:
            'Flujo dirigido: si la bodega tiene implosión automática activa y la ubicación destino
            'define una licencia diferente a la licencia origen, usar el flujo integrado
            '(estado -> implosión -> ubicación) para evitar mover sin cambiar licencia.
            Dim aplicarFlujoIntegradoImplosionUbicacion As Boolean = False
            Dim idStockNuevo As Integer = 0
            Dim idMovNuevo As Integer = 0

            If pMovimiento IsNot Nothing AndAlso
               pMovimiento.IdTipoTarea = 2 AndAlso
               pMovimiento.IdBodegaDestino > 0 AndAlso
               pMovimiento.IdUbicacionDestino > 0 Then

                Dim aplicaImplosionAuto As Boolean = Get_Parametro_Ubic_Implosion_Auto(pMovimiento.IdBodegaDestino,
                                                                                        lConnection,
                                                                                        lTransaction)

                If aplicaImplosionAuto Then
                    Dim stockActual As clsBeVW_stock_res = clsLnStock.Get_Single_By_IdStock(oBeTrans_ubic_hh_det.Stock.IdStock)

                    If stockActual IsNot Nothing Then
                        Dim infoDestinoDT As DataTable = clsLnBodega_ubicacion.Get_Info_Ubicacion_Destino(pMovimiento.IdUbicacionDestino,
                                                                                                           pMovimiento.IdBodegaDestino,
                                                                                                           lConnection,
                                                                                                           lTransaction)

                        If infoDestinoDT IsNot Nothing AndAlso infoDestinoDT.Rows.Count > 0 Then
                            Dim esRackDestino As Boolean = False
                            If Not IsDBNull(infoDestinoDT.Rows(0).Item("es_rack")) Then
                                esRackDestino = CBool(infoDestinoDT.Rows(0).Item("es_rack"))
                            End If

                            Dim licenciaDestino As String = If(IsDBNull(infoDestinoDT.Rows(0).Item("LicenciaDestino")),
                                                               "",
                                                               infoDestinoDT.Rows(0).Item("LicenciaDestino").ToString().Trim())

                            '#EJC20260526:
                            'Implosión automática solo aplica cuando el destino es rack.
                            If esRackDestino AndAlso
                               Not String.IsNullOrWhiteSpace(licenciaDestino) AndAlso
                               Not licenciaDestino.Equals(stockActual.Lic_plate, StringComparison.OrdinalIgnoreCase) Then
                                If Get_Cantidad_Licencias_Distintas_En_Ubicacion(pMovimiento.IdUbicacionDestino,
                                                                                 pMovimiento.IdBodegaDestino,
                                                                                 lConnection,
                                                                                 lTransaction) > 1 Then
                                    Throw New Exception("No se puede aplicar implosión automática: la ubicación destino (rack) tiene más de una licencia activa. Seleccione destino/licencia de forma explícita.")
                                End If

                                stockActual.CantidadUmBas = pMovimiento.Cantidad
                                stockActual.Lic_plate_Anterior = stockActual.Lic_plate
                                pMovimiento.Cantidad = stockActual.CantidadUmBas

                                aplicarFlujoIntegradoImplosionUbicacion = Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack_Interno(pMovimiento,
                                                                                                                                    stockActual,
                                                                                                                                    idStockNuevo,
                                                                                                                                    idMovNuevo,
                                                                                                                                    pPosiciones,
                                                                                                                                    lConnection,
                                                                                                                                    lTransaction,
                                                                                                                                    False)

                                If Not aplicarFlujoIntegradoImplosionUbicacion Then
                                    Throw New Exception("No se pudo completar el flujo integrado de implosión y cambio de ubicación.")
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            If Not aplicarFlujoIntegradoImplosionUbicacion Then
                pMovimiento.IdMovimiento = 0

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
            End If

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

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim result As String = clsLnTrans_movimientos.Aplicar(pMovimiento, idstock, True, lConnection, lTransaction)

            pMovimiento.IdMovimiento = 0
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
                                       ByVal pIdResolucionLp As Integer,
                                       Optional ByVal ValidarImplosionDirecta As Boolean = False) As String

        Aplica_LP_Stock = ""

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim ListaStock As New List(Of clsBeVW_stock_res)
            Dim result As String = ""
            Dim vCantidadCompletada As Double = 0
            Dim vCantidadPendiente As Double = 0
            Dim vCantidadDisponible As Double = 0

            Dim vNuevoLicPlate As String = pStockRes.Lic_plate
            Dim vPresentacion As Integer = pStockRes.IdPresentacion

            'Validación implosión antes de aplicar LP Stock
            If Not String.IsNullOrWhiteSpace(pStockRes.Lic_plate_Anterior) AndAlso
               Not String.IsNullOrWhiteSpace(vNuevoLicPlate) AndAlso
               pStockRes.Lic_plate_Anterior.Trim().ToUpper() <> vNuevoLicPlate.Trim().ToUpper() Then

                Validar_Implosion_MismaUbicacionEstado(pStockRes.Lic_plate_Anterior,
                                                       vNuevoLicPlate,
                                                       pMovimiento.IdBodegaDestino,
                                                       lConnection,
                                                       lTransaction,
                                                       True)

            End If

            pStockRes.Lic_plate = pStockRes.Lic_plate_Anterior
            pStockRes.IdPresentacion = pStockRes.IdPresentacion_Anterior

            If pStockRes.Lic_plate_Anterior = "" Then
                ListaStock = clsLnVW_stock_res.Get_Lista_Stock(pStockRes, lConnection, lTransaction)
            Else
                ListaStock = clsLnVW_stock_res.Get_Lista_Stock_By_Lic_Plate(pStockRes, lConnection, lTransaction)
            End If

            If ListaStock Is Nothing OrElse ListaStock.Count = 0 Then
                Throw New Exception("No se puede implosionar, no se encontró stock de la licencia origen.")
            End If


            pStockRes.Lic_plate = vNuevoLicPlate
            pStockRes.IdPresentacion = vPresentacion

            vCantidadPendiente = pStockRes.CantidadUmBas

            For Each StockRes In ListaStock

                vCantidadDisponible = Math.Round(StockRes.CantidadUmBas - StockRes.CantidadReservadaUMBas, 6)

                pMovimiento.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(
                pMovimiento.IdBodegaDestino,
                pMovimiento.IdPropietarioBodega,
                lConnection,
                lTransaction
            )

                If vCantidadDisponible > 0 Then

                    If vCantidadPendiente >= vCantidadDisponible Then

                        pMovimiento.Cantidad = vCantidadDisponible
                        pMovimiento.Barra_pallet = pStockRes.Lic_plate

                        result += " " & clsLnTrans_movimientos.Aplicar_Packing(
                        pMovimiento,
                        StockRes.IdStock,
                        vNuevoLicPlate,
                        vPresentacion,
                        lConnection,
                        lTransaction
                    )

                        pMovimiento.IdMovimiento = 0

                        clsLnTrans_movimientos.Insertar(pMovimiento, lConnection, lTransaction)

                        vCantidadPendiente -= vCantidadDisponible
                        vCantidadPendiente = Math.Round(vCantidadPendiente, 6)
                        vCantidadCompletada = (vCantidadPendiente = 0)

                        If vCantidadCompletada Then Exit For

                    ElseIf vCantidadPendiente < vCantidadDisponible Then

                        pMovimiento.Cantidad = vCantidadPendiente
                        pMovimiento.Barra_pallet = vNuevoLicPlate

                        result += " " & clsLnTrans_movimientos.Aplicar_Packing(
                        pMovimiento,
                        StockRes.IdStock,
                        vNuevoLicPlate,
                        vPresentacion,
                        lConnection,
                        lTransaction
                    )

                        pMovimiento.IdMovimiento = 0

                        clsLnTrans_movimientos.Insertar(pMovimiento, lConnection, lTransaction)

                        vCantidadPendiente = 0
                        vCantidadPendiente = Math.Round(vCantidadPendiente, 6)
                        vCantidadCompletada = True

                        If vCantidadCompletada Then Exit For

                    End If

                    Dim vMsgInformacion As String = "Se agrego implosion, Licencia: " + pMovimiento.Lic_plate + " por el operador: " + pMovimiento.IdOperadorBodega.ToString()

                    clsLnLog_error_wms_pack.Agregar_Error(
                    vMsgInformacion,
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
                    pEsImplosion:=True
                )

                Else
                    Throw New Exception("No hay cantidad disponible para implosionar")
                End If

            Next

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
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If
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

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim BePickingUbic As New clsBeTrans_picking_ubic()
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()

        If pMovimiento.IdTipoTarea = 0 Then
            Throw New Exception("ERROR_20220909_0724: " & "El identificador de tipo de tarea es incorrecto, salga de la pantalla e intente nuevamente por favor.")
        End If

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = "AVISO_20242211_HH_CambioEstadoUbic: ubicacion: " & pStockRes.IdUbicacion & " ubicacion anterior " & pStockRes.IdUbicacion_Anterior & "opoerador " & pMovimiento.IdOperadorBodega
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pMovimiento.IdEmpresa, pMovimiento.Usuario_agr, pIdMovimiento, pStockRes.IdStock,
                                                  pMovimiento.IdUnidadMedida, pMovimiento.IdPresentacion, pMovimiento.IdUbicacionOrigen,
                                                  pMovimiento.IdUbicacionDestino, pMovimiento.IdEstadoOrigen, pMovimiento.IdEstadoDestino,
                                                  pMovimiento.Cantidad, pMovimiento.IdOperadorBodega,
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

                                    IdMovimiento = 0

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

                                    IdMovimiento = 0

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
                                                                ByVal IdPickingEnc As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As String

        Aplica_Cambio_Estado_Ubic_En_Picking = ""

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
                        pMovimiento.IdTransaccion = IdPickingEnc
                        result += " " & clsLnTrans_movimientos.Aplicar_Cambio_Ubicacion_Automatico_Por_Picking(pMovimiento,
                                                                                                               StockRes.IdStock,
                                                                                                               EsIdStockIgual,
                                                                                                               lConnection,
                                                                                                               lTransaction)

                        pMovimiento.IdMovimiento = 0

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
                        pMovimiento.IdTransaccion = IdPickingEnc
                        result += " " & clsLnTrans_movimientos.Aplicar_Cambio_Ubicacion_Automatico_Por_Picking(pMovimiento,
                                                                                                               StockRes.IdStock,
                                                                                                               EsIdStockIgual,
                                                                                                               lConnection,
                                                                                                               lTransaction)

                        pMovimiento.IdMovimiento = 0

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

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
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

    '#EJC20260526:
    'Parametro para habilitar la implosión automática en cambio de ubicación.
    Private Shared Function Get_Parametro_Ubic_Implosion_Auto(ByVal pIdBodega As Integer,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As Boolean
        Try
            Const vSQL As String = "SELECT ISNULL(ubic_implosion_auto, 0) FROM bodega WHERE IdBodega = @IdBodega"

            Using cmd As New SqlCommand(vSQL, lConnection, lTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim value As Object = cmd.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then Return False

                Return Convert.ToBoolean(value)
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try
    End Function


    '#MA20260415 metodo para implosionar, mejoras en la cumbre
    Public Shared Function Aplica_Implosion(ByVal pMovimiento As clsBeTrans_movimientos,
                                            ByVal pStockRes As clsBeVW_stock_res,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction,
                                            ByVal esImplosion As Boolean) As String

        Aplica_Implosion = ""

        Try

            Dim ListaStock As New List(Of clsBeVW_stock_res)
            Dim result As String = ""
            Dim vCantidadCompletada As Double = 0
            Dim vCantidadPendiente As Double = 0
            Dim vCantidadDisponible As Double = 0

            '#EJC20200117: Almacenar aquí temporalmente, para hacer la lista de stock en base a lic_plate anterior.
            '#AT20220316 Agregué la presentación
            Dim vNuevoLicPlate As String = pStockRes.Lic_plate
            Dim vPresentacion As Integer = pStockRes.IdPresentacion

            Validar_Implosion_MismaUbicacionEstado(pStockRes.Lic_plate_Anterior,
                                                   pStockRes.Lic_plate,
                                                   pMovimiento.IdBodegaDestino,
                                                   lConnection,
                                                   lTransaction,
                                                   esImplosion)

            pStockRes.Lic_plate = pStockRes.Lic_plate_Anterior
            '#CKFK20260518 Puse esto en comentario porque la presentacion no se debe cambiar
            'pStockRes.IdPresentacion = pStockRes.IdPresentacion_Anterior

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

                                result += " " & clsLnTrans_movimientos.Aplicar_Packing(pMovimiento,
                                                                                       StockRes.IdStock,
                                                                                       vNuevoLicPlate,
                                                                                       vPresentacion,
                                                                                       lConnection,
                                                                                       lTransaction)

                                pMovimiento.IdMovimiento = 0

                                clsLnTrans_movimientos.Insertar(pMovimiento, lConnection, lTransaction)

                                vCantidadPendiente -= vCantidadDisponible
                                vCantidadPendiente = Math.Round(vCantidadPendiente, 6)

                                vCantidadCompletada = (vCantidadPendiente = 0)

                                If vCantidadCompletada Then Exit For

                            ElseIf vCantidadPendiente < vCantidadDisponible Then



                                pMovimiento.Cantidad = vCantidadPendiente

                                result += " " & clsLnTrans_movimientos.Aplicar_Packing(pMovimiento,
                                                                                       StockRes.IdStock,
                                                                                       vNuevoLicPlate,
                                                                                       vPresentacion,
                                                                                       lConnection,
                                                                                       lTransaction)

                                pMovimiento.IdMovimiento = 0

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

            Return result

        Catch ex As Exception
            'If lTransaction IsNot Nothing Then lTransaction.Rollback()
            'Throw New Exception(ex.Message)
            Throw
        End Try

    End Function

    '#MA20260415  metodo para el cambio de ubicacion - mejoras para la cumbre
    Public Shared Function Aplica_Cambio_Estado_Ubic(ByVal pMovimiento As clsBeTrans_movimientos,
                                                     ByVal pStockRes As clsBeVW_stock_res,
                                                     ByRef pIdStockNuevo As Integer,
                                                     ByRef pIdMovimiento As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction,
                                                     Optional pPosiciones As Integer = 0) As Boolean

        Aplica_Cambio_Estado_Ubic = False

        Dim ListaStock As New List(Of clsBeVW_stock_res)
        Dim result As String = ""
        Dim vCantidadCompletada As Double = 0
        Dim vCantidadPendiente As Double = 0
        Dim vCantidadDisponible As Double = 0
        Dim IdMovimiento As Integer
        Dim IdStockNuevo As Integer = 0

        Dim BePickingUbic As New clsBeTrans_picking_ubic()
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()

        If pMovimiento.IdTipoTarea = 0 Then
            Throw New Exception("ERROR_20220909_0724: " & "El identificador de tipo de tarea es incorrecto, salga de la pantalla e intente nuevamente por favor.")
        End If

        Try

            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = "AVISO_20242211_HH_CambioEstadoUbic: ubicacion: " & pStockRes.IdUbicacion & " ubicacion anterior " & pStockRes.IdUbicacion_Anterior & "opoerador " & pMovimiento.IdOperadorBodega
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
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

                                    IdMovimiento = 0

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

                                    IdMovimiento = 0

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

        Catch ex As Exception
            'If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        End Try

    End Function

    Public Shared Function EsRackDobleProfundidadHH(ByVal ubic As clsBeBodega_ubicacion) As Boolean
        Try
            If ubic Is Nothing Then Return False
            If ubic.IdTramo <= 0 Then Return False
            If ubic.IdBodega <= 0 Then Return False

            Dim beTramo As clsBeBodega_tramo =
            clsLnBodega_tramo.GetSingle(ubic.IdTramo, ubic.IdBodega)

            If beTramo Is Nothing Then Return False

            Return beTramo.Es_Rack AndAlso beTramo.IdTipoRack = 4

        Catch ex As Exception
            Throw New Exception("Error validando si el tramo es rack de doble profundidad: " & ex.Message)
        End Try
    End Function

    Public Shared Function ObtenerOrientacionParejaHH(ByVal orientacion As String) As String
        If String.IsNullOrWhiteSpace(orientacion) Then Return ""

        Select Case orientacion.Trim().ToUpper()
            Case "A" : Return "B"
            Case "B" : Return "A"
            Case "C" : Return "D"
            Case "D" : Return "C"
            Case Else : Return ""
        End Select
    End Function

    Public Shared Function ObtenerUbicacionParejaDobleProfundidadHH(ByVal ubic As clsBeBodega_ubicacion) As clsBeBodega_ubicacion
        Try
            If ubic Is Nothing Then Return Nothing

            Dim orientacionPareja As String = ObtenerOrientacionParejaHH(ubic.Orientacion_pos)

            If String.IsNullOrWhiteSpace(orientacionPareja) Then Return Nothing

            Dim ubicacionesRelacionadas As List(Of clsBeBodega_ubicacion) =
            clsLnBodega_ubicacion.Get_Ubicaciones_Misma_Posicion(
                ubic.IdBodega,
                ubic.IdTramo,
                ubic.Indice_x,
                ubic.Nivel,
                ubic.IdUbicacion)

            If ubicacionesRelacionadas Is Nothing OrElse ubicacionesRelacionadas.Count = 0 Then
                Return Nothing
            End If

            Return ubicacionesRelacionadas.
            FirstOrDefault(Function(x) x IsNot Nothing AndAlso
                                      Not String.IsNullOrWhiteSpace(x.Orientacion_pos) AndAlso
                                      x.Orientacion_pos.Trim().ToUpper() = orientacionPareja)

        Catch ex As Exception
            Throw New Exception("Error obteniendo ubicación relacionada de doble profundidad: " & ex.Message)
        End Try
    End Function

    Public Shared Function ExisteProductoBodegaDistintoEnUbicacionHH(ByVal idUbicacion As Integer,
                                                           ByVal idBodega As Integer,
                                                           ByVal idProductoBodega As Integer) As Boolean
        Try
            Dim lStock As List(Of clsBeVW_stock_res) =
            clsLnStock.Get_All_By_IdUbicacion(idUbicacion, idBodega)

            If lStock Is Nothing OrElse lStock.Count = 0 Then Return False

            Return lStock.Any(Function(s) s IsNot Nothing AndAlso
                                      s.IdProductoBodega > 0 AndAlso
                                      s.IdProductoBodega <> idProductoBodega)

        Catch ex As Exception
            Throw New Exception("Error validando producto en ubicación: " & ex.Message)
        End Try
    End Function

    Public Shared Function ObtenerCodigoProductoBodegaEnUbicacionHH(ByVal idUbicacion As Integer,
                                                          ByVal idBodega As Integer,
                                                          ByVal idProductoBodegaAUbicar As Integer) As String
        Try
            Dim lStock As List(Of clsBeVW_stock_res) =
            clsLnStock.Get_All_By_IdUbicacion(idUbicacion, idBodega)

            If lStock Is Nothing OrElse lStock.Count = 0 Then Return ""

            Dim stockDistinto = lStock.FirstOrDefault(Function(s) s IsNot Nothing AndAlso
                                                              s.IdProductoBodega > 0 AndAlso
                                                              s.IdProductoBodega <> idProductoBodegaAUbicar)

            If stockDistinto Is Nothing Then Return ""

            Return stockDistinto.Codigo_Producto

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function ConstruirMensajePosicionPosteriorHH(ByVal codigoProductoRelacionado As String) As String
        Return "La posición posterior ya contiene un producto diferente" &
           If(String.IsNullOrWhiteSpace(codigoProductoRelacionado), "", " (" & codigoProductoRelacionado & ")") &
           ". Solo se permite ubicar el mismo producto en esta posición."
    End Function

    Public Shared Function Validar_Mismo_Producto_Posicion_JSON(ByVal pIdBodega As Integer,
                                                                ByVal pIdTramo As Integer,
                                                                ByVal pIndice_x As Integer,
                                                                ByVal pNivel As Integer,
                                                                ByVal pIdUbicacion As Integer,
                                                                ByVal pIdProductoBodega As Integer,
                                                                ByRef posicionValida As Boolean,
                                                                ByRef mensaje As String,
                                                                ByRef aplicaDobleProfundidad As Boolean) As Boolean

        Dim resultado As Boolean = False

        Try
            posicionValida = True
            mensaje = ""
            aplicaDobleProfundidad = False

            Dim ubicDestino As clsBeBodega_ubicacion =
            clsLnBodega_ubicacion.GetSingle(pIdUbicacion, pIdBodega)

            If ubicDestino Is Nothing Then
                Throw New Exception("No se encontró la ubicación destino.")
            End If

            ' 1) Validar ubicación destino misma
            If ExisteProductoBodegaDistintoEnUbicacionHH(
            ubicDestino.IdUbicacion,
            ubicDestino.IdBodega,
            pIdProductoBodega) Then

                posicionValida = False
                mensaje = "La ubicación destino ya contiene un producto diferente. Solo se permite ubicar el mismo producto en esa posición."
            End If

            ' 2) Si la ubicación destino misma está bien, validar doble profundidad
            If posicionValida Then
                aplicaDobleProfundidad = EsRackDobleProfundidadHH(ubicDestino)

                If aplicaDobleProfundidad Then
                    Dim ubicPareja As clsBeBodega_ubicacion =
                    ObtenerUbicacionParejaDobleProfundidadHH(ubicDestino)

                    If ubicPareja IsNot Nothing Then
                        If ExisteProductoBodegaDistintoEnUbicacionHH(
                        ubicPareja.IdUbicacion,
                        ubicPareja.IdBodega,
                        pIdProductoBodega) Then

                            Dim codigoProductoRelacionado As String =
                            ObtenerCodigoProductoBodegaEnUbicacionHH(ubicPareja.IdUbicacion,
                                                                     ubicPareja.IdBodega,
                                                                     pIdProductoBodega)

                            posicionValida = False
                            mensaje = ConstruirMensajePosicionPosteriorHH(codigoProductoRelacionado)
                        End If
                    End If
                End If
            End If

            resultado = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
        End Try

        Return resultado

    End Function

    Public Shared Function Validar_Regla_Ubicacion_JSON(ByVal pIdProducto As Integer,
                                                        ByVal pIdUbicacion As Integer,
                                                        ByVal pIdBodega As Integer,
                                                        ByVal pIdEmpresa As Integer,
                                                        ByVal pIdEstado As Integer,
                                                        ByRef ubicacionValida As Boolean,
                                                        ByRef mensaje As String) As Boolean

        Dim resultado As Boolean = False

        Try

            Dim BeProducto As clsBeProducto = Nothing
            Dim BeUbicacion As clsBeBodega_ubicacion = Nothing
            Dim BeEstadoProd As clsBeProducto_estado = Nothing

            BeProducto = clsLnProducto.Get_Single_By_IdProducto(pIdProducto)
            BeUbicacion = clsLnBodega_ubicacion.GetSingle(pIdUbicacion, pIdBodega)

            If BeProducto Is Nothing Then
                ubicacionValida = False
                mensaje = "No se pudo obtener la información del producto."
            End If

            If ubicacionValida AndAlso BeUbicacion Is Nothing Then
                ubicacionValida = False
                mensaje = "La ubicación destino no es válida."
            End If

            If ubicacionValida AndAlso pIdEstado > 0 Then
                BeEstadoProd = clsLnProducto_estado.Get_Single_By_IdEstado(pIdEstado)
            End If

            ' 1. Validación directa por tipo de rotación
            If ubicacionValida Then
                If BeProducto.IdTipoRotacion > 0 AndAlso BeUbicacion.IdTipoRotacion > 0 Then
                    If BeProducto.IdTipoRotacion <> BeUbicacion.IdTipoRotacion Then
                        ubicacionValida = False
                        mensaje = String.Format(
                        "La ubicación destino no cumple la regla de ubicación. El tipo de rotación del producto ({0}) no coincide con el de la ubicación destino ({1}).",
                        BeProducto.IdTipoRotacion,
                        BeUbicacion.IdTipoRotacion)
                    End If
                End If
            End If

            ' 2. Validación directa por estado dañado
            If ubicacionValida AndAlso BeEstadoProd IsNot Nothing Then
                If BeEstadoProd.Dañado AndAlso Not BeUbicacion.Dañado Then
                    ubicacionValida = False
                    mensaje = "La ubicación destino no cumple la regla de ubicación. El producto está en estado dañado y la ubicación destino no está configurada para productos dañados."
                End If
            End If

            ' 3. Validación por reglas configuradas
            If ubicacionValida Then

                Dim dtReglas As DataTable = clsLnRegla_ubic_enc.Listar(pIdBodega, pIdEmpresa, True)

                If dtReglas IsNot Nothing AndAlso dtReglas.Rows.Count > 0 Then

                    Dim hayReglasAplicables As Boolean = False
                    Dim existeReglaCompatible As Boolean = False

                    For Each dr As DataRow In dtReglas.Rows

                        Dim regla As New clsBeRegla_ubic_enc()
                        regla.IdReglaUbicacionEnc = CInt(dr("Código"))
                        clsLnRegla_ubic_enc.GetSingleWithDetails(regla)

                        Dim cumple As Boolean = True
                        Dim reglaAplica As Boolean = False

                        If regla.listDetRegla_Ubic_Det_Ir IsNot Nothing AndAlso regla.listDetRegla_Ubic_Det_Ir.Count > 0 Then
                            reglaAplica = True

                            If BeProducto.IdIndiceRotacion = 0 Then
                                cumple = False
                            Else
                                Dim okIndice = regla.listDetRegla_Ubic_Det_Ir.
                                Any(Function(x) x.Activo AndAlso x.IdIndiceRotacion = BeProducto.IdIndiceRotacion)

                                cumple = cumple AndAlso okIndice
                            End If
                        End If

                        If regla.listDetRegla_Ubic_Det_Tr IsNot Nothing AndAlso regla.listDetRegla_Ubic_Det_Tr.Count > 0 Then
                            reglaAplica = True

                            If BeUbicacion.IdTipoRotacion = 0 Then
                                cumple = False
                            Else
                                Dim okTipo = regla.listDetRegla_Ubic_Det_Tr.
                                Any(Function(x) x.Activo AndAlso x.IdTipoRotacion = BeUbicacion.IdTipoRotacion)

                                cumple = cumple AndAlso okTipo
                            End If
                        End If

                        If regla.listDetRegla_Ubic_Det_tp IsNot Nothing AndAlso regla.listDetRegla_Ubic_Det_tp.Count > 0 Then
                            reglaAplica = True

                            If BeProducto Is Nothing OrElse BeProducto.IdTipoProducto = 0 Then
                                cumple = False
                            Else
                                Dim okTipoProducto = regla.listDetRegla_Ubic_Det_tp.
                                Any(Function(x) x.Activo AndAlso x.IdTipoProducto = BeProducto.IdTipoProducto)

                                cumple = cumple AndAlso okTipoProducto
                            End If
                        End If

                        If regla.listDetRegla_Ubic_Det_Pe IsNot Nothing AndAlso regla.listDetRegla_Ubic_Det_Pe.Count > 0 Then
                            reglaAplica = True

                            If BeEstadoProd Is Nothing OrElse BeEstadoProd.IdEstado = 0 Then
                                cumple = False
                            Else
                                Dim okEstado = regla.listDetRegla_Ubic_Det_Pe.
                                Any(Function(x) x.Activo AndAlso x.IdEstado = BeEstadoProd.IdEstado)

                                cumple = cumple AndAlso okEstado
                            End If
                        End If

                        If Not reglaAplica Then
                            Continue For
                        End If

                        hayReglasAplicables = True

                        If cumple Then
                            existeReglaCompatible = True
                            Exit For
                        End If
                    Next

                    If hayReglasAplicables AndAlso Not existeReglaCompatible Then
                        ubicacionValida = False
                        mensaje = "La ubicación destino no cumple con las propiedades requeridas del producto (tipo de rotación, índice de rotación, tipo de producto o estado)."
                    End If

                End If

            End If

            resultado = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
        End Try

        Return resultado

    End Function

    Private Shared Sub Validar_Implosion_MismaUbicacionEstado(ByVal pLicenciaOrigen As String,
                                                              ByVal pLicenciaDestino As String,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction,
                                                              ByVal esImplosion As Boolean)

        If String.IsNullOrWhiteSpace(pLicenciaOrigen) Then
            Throw New Exception("No se puede implosionar, licencia origen vacía.")
        End If

        If String.IsNullOrWhiteSpace(pLicenciaDestino) Then
            Throw New Exception("No se puede implosionar, licencia destino vacía.")
        End If

        Dim stockOrigen As clsBeVW_stock_res =
        clsLnVW_stock_res.Get_Stock_Implosion_By_LicPlate(pLicenciaOrigen,
                                                          pIdBodega,
                                                          lConnection,
                                                          lTransaction)

        Dim stockDestino As clsBeVW_stock_res =
        clsLnVW_stock_res.Get_Stock_Implosion_By_LicPlate(pLicenciaDestino,
                                                          pIdBodega,
                                                          lConnection,
                                                          lTransaction)

        If stockOrigen Is Nothing Then
            Throw New Exception("No se puede implosionar, no se encontró stock de la licencia origen.")
        End If

        If stockDestino Is Nothing Then
            Throw New Exception("No se puede implosionar, no se encontró stock de la licencia destino.")
        End If

        Dim nombreUbicacionOrigen As String = If(String.IsNullOrWhiteSpace(stockOrigen.Nombre_Completo),
                                             stockOrigen.IdUbicacion.ToString(),
                                             stockOrigen.Nombre_Completo)

        Dim nombreUbicacionDestino As String = If(String.IsNullOrWhiteSpace(stockDestino.Nombre_Completo),
                                              stockDestino.IdUbicacion.ToString(),
                                              stockDestino.Nombre_Completo)

        Dim nombreEstadoOrigen As String = If(String.IsNullOrWhiteSpace(stockOrigen.NomEstado),
                                          stockOrigen.IdProductoEstado.ToString(),
                                          stockOrigen.NomEstado)

        Dim nombreEstadoDestino As String = If(String.IsNullOrWhiteSpace(stockDestino.NomEstado),
                                               stockDestino.IdProductoEstado.ToString(),
                                               stockDestino.NomEstado)

        If esImplosion Then
            If stockOrigen.IdUbicacion <> stockDestino.IdUbicacion Then
                Throw New Exception("No se puede implosionar, ubicaciones diferentes." & vbCrLf &
                                    "Origen: " & nombreUbicacionOrigen &
                                    ", destino: " & nombreUbicacionDestino & ".")
            End If
        End If

        If stockOrigen.IdProductoEstado <> stockDestino.IdProductoEstado Then
            Throw New Exception("No se puede implosionar, estados diferentes." & vbCrLf &
                            "Origen: " & nombreEstadoOrigen &
                            ", destino: " & nombreEstadoDestino & ".")
        End If

    End Sub

    '#EJC20260416:
    'Este método orquesta en un solo flujo los procesos de:
    '1) cambio de estado
    '2) implosión
    '3) cambio de ubicación
    '
    'La regla es que NO siempre se ejecutan los 3 procesos,
    'pero si aplican varios, SIEMPRE deben ejecutarse en este orden:
    '   estado -> implosión -> ubicación
    '
    '#EJC20260416:
    'Importante:
    'Los métodos internos buscan stock por atributos (estado, licencia, ubicación, etc.),
    'por lo tanto el stock "lógico" va mutando entre pasos.
    'Por esa razón se actualiza pStockRes después de cada proceso exitoso,
    'para que el siguiente proceso trabaje sobre el estado más reciente del stock.
    Public Shared Function Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack(ByVal pMovimiento As clsBeTrans_movimientos,
                                                                          ByVal pStockRes As clsBeVW_stock_res,
                                                                          ByRef pIdStockNuevo As Integer,
                                                                          ByRef pIdMovimientoNuevo As Integer,
                                                                          ByVal pPosiciones As Integer,
                                                                          Optional ByVal EsCambioEstado As Boolean = False) As Boolean

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim infoDestinoDT As DataTable = clsLnBodega_ubicacion.Get_Info_Ubicacion_Destino(pMovimiento.IdUbicacionDestino,
                                                                                               pMovimiento.IdBodegaDestino)

            Dim esRack As Boolean = False
            Dim licenciaDestino As String = ""
            Dim IdProductoEstadoDestino As Integer = 0

            '#EJC20260416:
            'Se guardan los valores originales porque el objeto pMovimiento y pStockRes
            'se reutilizan en varios pasos del flujo y van mutando durante la ejecución.
            Dim IdUbicacionOrigen As Integer = pMovimiento.IdUbicacionOrigen
            Dim IdUbicacionDestino As Integer = pMovimiento.IdUbicacionDestino
            Dim licenciaOrigen As String = pStockRes.Lic_plate
            Dim IdProductoEstadoOrigen As Integer = pStockRes.IdProductoEstado
            Dim propietarioOriginal = pMovimiento.IdPropietarioBodega

            If infoDestinoDT IsNot Nothing AndAlso infoDestinoDT.Rows.Count > 0 Then
                Dim row = infoDestinoDT.Rows(0)

                esRack = CBool(row("es_rack"))
                licenciaDestino = If(IsDBNull(row("LicenciaDestino")), "", row("LicenciaDestino").ToString())
                IdProductoEstadoDestino = If(IsDBNull(row("IdProductoEstadoDestino")), 0, CInt(row("IdProductoEstadoDestino")))
            End If

            Dim estadoRackDefecto As Integer = clsLnBodega.Get_Estado_Defecto_Rack(pMovimiento.IdBodegaDestino,
                                                                                   lConnection,
                                                                                   lTransaction)

            '#EJC20260416:
            'Se definen flags independientes.
            'Antes el flujo mezclaba decisiones entre ubicación, estado e implosión,
            'lo que hacía difícil combinar procesos y respetar el orden.
            Dim requiereCambioEstado As Boolean = False
            Dim requiereImplosion As Boolean = False
            Dim requiereCambioUbicacion As Boolean = False

            Dim tieneLicenciaDestino As Boolean = licenciaDestino <> ""
            Dim tieneEstadoDestino As Boolean = IdProductoEstadoDestino > 0

            If EsCambioEstado AndAlso pMovimiento.IdEstadoDestino > 0 Then
                IdProductoEstadoDestino = pMovimiento.IdEstadoDestino
                tieneEstadoDestino = True
            End If

            '#EJC20260416:
            'Cambio de estado:
            'Aplica si el destino trae estado y es distinto al actual.
            If tieneEstadoDestino AndAlso IdProductoEstadoDestino <> IdProductoEstadoOrigen Then
                requiereCambioEstado = True
            End If

            If EsCambioEstado Then
                requiereCambioEstado = True
            End If

            '#EJC20260416:
            'Regla especial de rack:
            'Si la ubicación destino es rack y el estado actual no coincide con el estado por defecto del rack,
            'se obliga el cambio de estado, incluso si el destino no envía un estado explícito.
            If Not EsCambioEstado Then
                If esRack AndAlso estadoRackDefecto > 0 AndAlso IdProductoEstadoOrigen <> estadoRackDefecto Then
                    requiereCambioEstado = True

                    If Not tieneEstadoDestino Then
                        IdProductoEstadoDestino = estadoRackDefecto
                        tieneEstadoDestino = True
                    End If
                End If
            End If

            '#EJC20260416:
            'Si no hay estado destino explícito ni regla de rack, se conserva el estado actual.
            If Not tieneEstadoDestino Then
                IdProductoEstadoDestino = IdProductoEstadoOrigen
            End If

            'si es distinto, solo validar si hay estado rack configurado
            If esRack AndAlso estadoRackDefecto > 0 AndAlso IdProductoEstadoDestino <> estadoRackDefecto Then

                Dim BeEstadoRack = clsLnProducto_estado.Get_Single_By_IdEstado(estadoRackDefecto,
                                                                               lConnection,
                                                                               lTransaction)

                Dim nombreEstadoRack As String = If(BeEstadoRack IsNot Nothing, BeEstadoRack.Nombre, estadoRackDefecto.ToString())

                Throw New Exception("Cambio de estado no válido." & vbCrLf & "Destino es rack y el estado no es " & nombreEstadoRack & ".")

            End If

            '#EJC20260416:
            'Implosión:
            'Aplica solo si la ubicación destino tiene una licencia configurada
            'y esa licencia es distinta a la licencia actual del stock.
            If Not EsCambioEstado Then
                '#EJC20260526:
                'Implosión automática solo aplica para ubicaciones destino tipo rack.
                If esRack AndAlso tieneLicenciaDestino AndAlso licenciaDestino <> licenciaOrigen Then
                    If Get_Cantidad_Licencias_Distintas_En_Ubicacion(IdUbicacionDestino,
                                                                     pMovimiento.IdBodegaDestino,
                                                                     lConnection,
                                                                     lTransaction) > 1 Then
                        Throw New Exception("No se puede aplicar implosión automática: la ubicación destino (rack) tiene más de una licencia activa. Seleccione destino/licencia de forma explícita.")
                    End If
                    requiereImplosion = True
                End If
            End If

            '#EJC20260416:
            'Cambio de ubicación:
            'Aplica si la ubicación destino es válida y distinta a la de origen.
            If Not EsCambioEstado Then
                If IdUbicacionDestino > 0 AndAlso IdUbicacionDestino <> IdUbicacionOrigen Then
                    requiereCambioUbicacion = True
                End If
            End If

            If EsCambioEstado Then
                requiereImplosion = False
                requiereCambioUbicacion = False
            End If

            '#EJC20260416:
            'Si no hay ningún cambio a aplicar, se confirma la transacción y se retorna True.
            If Not requiereCambioEstado AndAlso Not requiereImplosion AndAlso Not requiereCambioUbicacion Then
                lTransaction.Commit()
                Return True
            End If

            Dim exitoPaso As Boolean = False

            '==========================================================
            '#EJC20260416:
            'PASO 1 - CAMBIO DE ESTADO
            '==========================================================
            If requiereCambioEstado Then

                '#EJC20260416:
                'Para cambio de estado se deja la misma ubicación,
                'porque este paso solo debe mutar el estado del stock.
                pMovimiento.IdTipoTarea = 3
                pMovimiento.IdEstadoOrigen = pStockRes.IdProductoEstado
                pMovimiento.IdEstadoDestino = IdProductoEstadoDestino
                pMovimiento.IdUbicacionOrigen = IdUbicacionOrigen
                pMovimiento.IdUbicacionDestino = IdUbicacionOrigen
                pMovimiento.Fecha = DateTime.Now
                pMovimiento.Fecha_agr = DateTime.Now

                exitoPaso = Aplica_Cambio_Estado_Ubic(pMovimiento,
                                                      pStockRes,
                                                      pIdStockNuevo,
                                                      pIdMovimientoNuevo,
                                                      lConnection,
                                                      lTransaction,
                                                      pPosiciones)

                If Not exitoPaso Then
                    Throw New Exception("Error al aplicar cambio de estado.")
                End If

                '#EJC20260416:
                'El stock ya mutó.
                'Se actualiza el contexto en memoria para que el siguiente paso
                '(implosión o ubicación) use el nuevo estado como filtro de entrada.
                pStockRes.IdProductoEstado = IdProductoEstadoDestino

                '#EJC20260416:
                'Si se obtuvo un nuevo IdStock, se actualiza también en memoria.
                'Esto ayuda a mantener sincronizado el contexto lógico del stock resultante.
                If pIdStockNuevo > 0 Then
                    pStockRes.IdStock = pIdStockNuevo
                End If
            End If

            '==========================================================
            '#EJC20260416:
            'PASO 2 - IMPLOSIÓN
            '==========================================================
            If requiereImplosion Then

                '#EJC20260416:
                'La implosión debe ejecutarse sobre el stock ya mutado por el paso anterior,
                'si hubo cambio de estado.

                'Validar_Implosion_MismaUbicacionEstado(licenciaOrigen,
                '                                       licenciaDestino,
                '                                       pMovimiento.IdBodegaDestino,
                '                                       lConnection,
                '                                       lTransaction)

                'Por eso aquí pStockRes ya contiene el estado vigente del stock.
                'Se deja registrada la licencia anterior y se establece la licencia nueva.
                pMovimiento.IdPropietarioBodega = propietarioOriginal
                pStockRes.Lic_plate_Anterior = pStockRes.Lic_plate
                pStockRes.Lic_plate = licenciaDestino

                pMovimiento.IdTipoTarea = 12
                pMovimiento.Lic_plate = pStockRes.Lic_plate_Anterior
                pMovimiento.Barra_pallet = licenciaDestino
                pMovimiento.IdEstadoOrigen = pStockRes.IdProductoEstado
                pMovimiento.IdEstadoDestino = pStockRes.IdProductoEstado
                pMovimiento.IdUbicacionOrigen = IdUbicacionOrigen
                pMovimiento.IdUbicacionDestino = IdUbicacionOrigen
                pMovimiento.Fecha = DateTime.Now
                pMovimiento.Fecha_agr = DateTime.Now

                Aplica_Implosion(pMovimiento,
                                 pStockRes,
                                 lConnection,
                                 lTransaction,
                                 False)

                '#EJC20260416:
                'Después de implosionar, el stock lógico ya quedó con nueva licencia.
                'Se conserva esa licencia en memoria para que el siguiente paso
                '(cambio de ubicación) busque el stock correcto.
                '
                'Nota:
                'Aplica_Implosion actualmente no devuelve el nuevo IdStock.
                'Por eso aquí solo se actualiza el contexto por atributos.
            End If

            '==========================================================
            '#EJC20260416:
            'PASO 3 - CAMBIO DE UBICACIÓN
            '==========================================================
            If requiereCambioUbicacion Then

                '#EJC20260416:
                'Este paso debe usar como input el stock ya mutado por los pasos previos:
                'estado y/o implosión, según hayan aplicado.
                pMovimiento.IdPropietarioBodega = propietarioOriginal

                pMovimiento.IdTipoTarea = 2
                pMovimiento.IdUbicacionOrigen = IdUbicacionOrigen
                pMovimiento.IdUbicacionDestino = IdUbicacionDestino
                pMovimiento.IdEstadoOrigen = pStockRes.IdProductoEstado
                pMovimiento.IdEstadoDestino = pStockRes.IdProductoEstado
                pMovimiento.Lic_plate = ""
                pMovimiento.Fecha = DateTime.Now
                pMovimiento.Fecha_agr = DateTime.Now

                exitoPaso = Aplica_Cambio_Estado_Ubic(pMovimiento,
                                                      pStockRes,
                                                      pIdStockNuevo,
                                                      pIdMovimientoNuevo,
                                                      lConnection,
                                                      lTransaction,
                                                      pPosiciones)

                If Not exitoPaso Then
                    Throw New Exception("Error al aplicar cambio de ubicación.")
                End If

                '#EJC20260416:
                'Se actualiza el contexto final del stock.
                pStockRes.IdUbicacion_Anterior = pStockRes.IdUbicacion
                pStockRes.IdUbicacion = IdUbicacionDestino

                If pIdStockNuevo > 0 Then
                    pStockRes.IdStock = pIdStockNuevo
                End If
            End If

            lTransaction.Commit()
            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then
                lTransaction.Rollback()
            End If

            Dim vMsgError As String = String.Format("{0} {1}",
                                           MethodBase.GetCurrentMethod().Name,
                                           ex.Message)

            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw

        Finally
            If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If
        End Try

    End Function

    '#EJC20260416:
    'Orquesta el movimiento de una licencia completa mixta.
    'La licencia se reconstruye desde BD y luego se procesa línea por línea
    'dentro de una sola transacción lógica del WS.
    Public Shared Function Aplica_Cambio_Estado_Ubic_HH_LicenciaCompleta_ConValidacionRack(ByVal pMovimiento As clsBeTrans_movimientos,
                                                                                           ByVal pLicPlate As String,
                                                                                           ByVal pIdUbicacionOrigen As Integer,
                                                                                           ByVal pIdUbicacionDestino As Integer,
                                                                                           ByRef pIdStockNuevo As Integer,
                                                                                           ByRef pIdMovimientoNuevo As Integer,
                                                                                           Optional ByVal pPosiciones As Integer = 0,
                                                                                           Optional EsCambioEstado As Boolean = False) As Boolean

        Aplica_Cambio_Estado_Ubic_HH_LicenciaCompleta_ConValidacionRack = False

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#EJC20260416:
            'La fuente de verdad de la licencia completa debe ser BD, no la HH.
            Dim listaLicencia As List(Of clsBeVW_stock_res) = clsLnVW_stock_res.Get_Lista_Stock_Licencia_Completa(pLicPlate,
                                                                                                                 pIdUbicacionOrigen,
                                                                                                                 pMovimiento.IdBodegaOrigen,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)

            If listaLicencia Is Nothing OrElse listaLicencia.Count = 0 Then
                Throw New Exception("No se encontró stock para la licencia completa.")
            End If

            Dim propietarioOriginal As Integer = pMovimiento.IdPropietarioBodega

            ' Dim BeMovimientoTransitorio As clsBeTrans_movimientos = pMovimiento
            Dim BeMovimientoTransitorio As clsBeTrans_movimientos


            '#EJC20260416:
            'Se procesa cada línea real de la licencia dentro de la misma transacción.
            Dim listaMovimientos As New List(Of Integer)
            Dim listaStocks As New List(Of Integer)

            For Each stockLinea As clsBeVW_stock_res In listaLicencia

                Dim idStockLinea As Integer = 0
                Dim idMovLinea As Integer = 0

                '#EJC20260416:
                'Se prepara un movimiento por línea, manteniendo la intención común de la licencia completa.
                BeMovimientoTransitorio = New clsBeTrans_movimientos()
                clsPublic.CopyObject(pMovimiento, BeMovimientoTransitorio)
                BeMovimientoTransitorio.Fecha_vence = stockLinea.Fecha_Vence
                ' clsPublic.CopyObject(stockLinea, BeMovimientoTransitorio)
                stockLinea.Movimiento = BeMovimientoTransitorio
                stockLinea.Movimiento.IdProductoBodega = stockLinea.IdProductoBodega
                stockLinea.Movimiento.IdUbicacionOrigen = pIdUbicacionOrigen
                stockLinea.Movimiento.IdUbicacionDestino = pIdUbicacionDestino
                stockLinea.Movimiento.Lic_plate = pLicPlate
                stockLinea.Movimiento.Fecha = DateTime.Now
                stockLinea.Movimiento.Fecha_agr = DateTime.Now

                stockLinea.Movimiento.IdPropietarioBodega = propietarioOriginal
                '#EJC20260416:
                'Se reutiliza el método actual por línea.
                Dim exito As Boolean = Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack_Interno(stockLinea.Movimiento,
                                                                                              stockLinea,
                                                                                              idStockLinea,
                                                                                              idMovLinea,
                                                                                              pPosiciones,
                                                                                              lConnection,
                                                                                              lTransaction,
                                                                                              EsCambioEstado)

                If Not exito Then
                    Throw New Exception("No se pudo aplicar el proceso a una línea de la licencia.")
                End If

                If idStockLinea > 0 Then listaStocks.Add(idStockLinea)
                If idMovLinea > 0 Then listaMovimientos.Add(idMovLinea)

                'If idStockLinea > 0 Then pIdStockNuevo = idStockLinea
                'If idMovLinea > 0 Then pIdMovimientoNuevo = idMovLinea
            Next

            If listaStocks.Count > 0 Then
                pIdStockNuevo = listaStocks(0)
            End If

            If listaMovimientos.Count > 0 Then
                pIdMovimientoNuevo = listaMovimientos(0)
            End If

            lTransaction.Commit()
            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw
            'Return False

        Finally
            If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If
        End Try

    End Function

    '#EJC20260416:
    'Versión interna para reutilizar la lógica actual dentro de una transacción ya abierta.
    Private Shared Function Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack_Interno(ByVal pMovimiento As clsBeTrans_movimientos,
                                                                                    ByVal pStockRes As clsBeVW_stock_res,
                                                                                    ByRef pIdStockNuevo As Integer,
                                                                                    ByRef pIdMovimientoNuevo As Integer,
                                                                                    ByVal pPosiciones As Integer,
                                                                                    ByVal lConnection As SqlConnection,
                                                                                    ByVal lTransaction As SqlTransaction,
                                                                                    ByVal EsCambioEstado As Boolean) As Boolean

        Try

            Dim infoDestinoDT As DataTable = clsLnBodega_ubicacion.Get_Info_Ubicacion_Destino(pMovimiento.IdUbicacionDestino,
                                                                                              pMovimiento.IdBodegaDestino,
                                                                                              lConnection,
                                                                                              lTransaction)
            Dim esRack As Boolean = False
            Dim licenciaDestino As String = ""
            Dim IdProductoEstadoDestino As Integer = 0

            Dim IdUbicacionOrigen As Integer = pMovimiento.IdUbicacionOrigen
            Dim IdUbicacionDestino As Integer = pMovimiento.IdUbicacionDestino
            Dim licenciaOrigen As String = pStockRes.Lic_plate
            Dim IdProductoEstadoOrigen As Integer = pStockRes.IdProductoEstado
            Dim propietarioOriginal = pMovimiento.IdPropietarioBodega

            If infoDestinoDT IsNot Nothing AndAlso infoDestinoDT.Rows.Count > 0 Then
                Dim row = infoDestinoDT.Rows(0)
                esRack = CBool(row("es_rack"))
                licenciaDestino = If(IsDBNull(row("LicenciaDestino")), "", row("LicenciaDestino").ToString())
                IdProductoEstadoDestino = If(IsDBNull(row("IdProductoEstadoDestino")), 0, CInt(row("IdProductoEstadoDestino")))
            End If

            Dim estadoRackDefecto As Integer = clsLnBodega.Get_Estado_Defecto_Rack(pMovimiento.IdBodegaDestino, lConnection, lTransaction)

            Dim requiereCambioEstado As Boolean = False
            Dim requiereImplosion As Boolean = False
            Dim requiereCambioUbicacion As Boolean = False

            Dim tieneLicenciaDestino As Boolean = licenciaDestino <> ""
            Dim tieneEstadoDestino As Boolean = IdProductoEstadoDestino > 0

            If EsCambioEstado AndAlso pMovimiento.IdEstadoDestino > 0 Then
                IdProductoEstadoDestino = pMovimiento.IdEstadoDestino
                tieneEstadoDestino = True
            End If

            If tieneEstadoDestino AndAlso IdProductoEstadoDestino <> IdProductoEstadoOrigen Then
                requiereCambioEstado = True
            End If

            '#MA20260427:
            'La regla automática de rack aplica para Cambio de Ubicación.
            'En Cambio de Estado no se fuerza el estado del rack; solo se valida el estado seleccionado.
            If Not EsCambioEstado Then
                If esRack AndAlso estadoRackDefecto > 0 AndAlso IdProductoEstadoOrigen <> estadoRackDefecto Then
                    requiereCambioEstado = True

                    If Not tieneEstadoDestino Then
                        IdProductoEstadoDestino = estadoRackDefecto
                        tieneEstadoDestino = True
                    End If
                End If
            Else
                If IdProductoEstadoDestino = IdProductoEstadoOrigen Then
                    Throw New Exception("Estado destino y origen son iguales, no aplica el cambio de estado")
                End If
            End If

            If Not tieneEstadoDestino Then
                IdProductoEstadoDestino = IdProductoEstadoOrigen
            End If

            If esRack AndAlso estadoRackDefecto > 0 AndAlso IdProductoEstadoDestino <> estadoRackDefecto Then

                Dim BeEstadoRack = clsLnProducto_estado.Get_Single_By_IdEstado(estadoRackDefecto,
                                                                               lConnection,
                                                                               lTransaction)

                Dim nombreEstadoRack As String = If(BeEstadoRack IsNot Nothing,
                                        BeEstadoRack.Nombre,
                                        estadoRackDefecto.ToString())

                Dim tipoCambio As String = If(EsCambioEstado, "estado", "ubicación")

                Throw New Exception("Cambio de " & tipoCambio & " no válido." & vbCrLf &
                        "Destino es rack y el estado no es " & nombreEstadoRack & ".")

            End If

            If Not EsCambioEstado Then
                '#EJC20260526:
                'Implosión automática solo aplica para ubicaciones destino tipo rack.
                If esRack AndAlso tieneLicenciaDestino AndAlso licenciaDestino <> licenciaOrigen Then
                    If Get_Cantidad_Licencias_Distintas_En_Ubicacion(IdUbicacionDestino,
                                                                     pMovimiento.IdBodegaDestino,
                                                                     lConnection,
                                                                     lTransaction) > 1 Then
                        Throw New Exception("No se puede aplicar implosión automática: la ubicación destino (rack) tiene más de una licencia activa. Seleccione destino/licencia de forma explícita.")
                    End If
                    requiereImplosion = True
                End If
            End If


            If Not EsCambioEstado Then
                If IdUbicacionDestino > 0 AndAlso IdUbicacionDestino <> IdUbicacionOrigen Then
                    requiereCambioUbicacion = True
                End If
            End If

            '#MA20260427:
            'Seguro final: Cambio de Estado no debe ejecutar procesos adicionales.
            If EsCambioEstado Then
                requiereImplosion = False
                requiereCambioUbicacion = False
            End If

            If Not requiereCambioEstado AndAlso Not requiereImplosion AndAlso Not requiereCambioUbicacion Then
                Return True
            End If

            Dim exitoPaso As Boolean = False

            If infoDestinoDT.Rows.Count > 0 Then

                Dim row As DataRow = infoDestinoDT.Rows(0)
                esRack = CBool(row("es_rack"))

                If esRack Then

                    Dim estadoDestino As Integer = If(EsCambioEstado AndAlso pMovimiento.IdEstadoDestino > 0,
                                                      pMovimiento.IdEstadoDestino,
                                                      CInt(row("IdProductoEstadoDestino")))

                    estadoRackDefecto = clsLnBodega.Get_Estado_Defecto_Rack(pMovimiento.IdBodegaDestino, lConnection, lTransaction)

                    If estadoRackDefecto > 0 AndAlso estadoDestino > 0 AndAlso estadoDestino <> estadoRackDefecto Then
                        Dim BePEstadoDesst = clsLnProducto_estado.Get_Single_By_IdEstado(estadoDestino, lConnection, lTransaction)
                        Dim BeEstadoRack = clsLnProducto_estado.Get_Single_By_IdEstado(estadoRackDefecto, lConnection, lTransaction)

                        If BePEstadoDesst IsNot Nothing AndAlso BeEstadoRack IsNot Nothing Then
                            Dim tipoCambio As String = If(EsCambioEstado, "estado", "ubicación")

                            Dim msgRack As String = "Cambio de " & tipoCambio & " no válido." & vbCrLf & "Destino es rack y el estado no es " & BeEstadoRack.Nombre & "."

                            Throw New Exception(msgRack)
                        Else
                            If Not estadoDestino = 0 Then
                                Throw New Exception("MSG20260422A: No se pudo obtener la información del estado asociado al tramo (rack) y no se puede completar la validación del destino.")
                            End If
                        End If

                    End If

                End If

            End If

            '#EJC20260416: Paso 1 - Estado
            If requiereCambioEstado Then

                pMovimiento.IdTipoTarea = 3
                pMovimiento.IdEstadoOrigen = pStockRes.IdProductoEstado
                pMovimiento.IdEstadoDestino = IdProductoEstadoDestino
                pMovimiento.IdUbicacionOrigen = IdUbicacionOrigen

                If Not EsCambioEstado Then
                    pMovimiento.IdUbicacionDestino = IdUbicacionOrigen
                End If

                pMovimiento.Lic_plate = ""
                pMovimiento.Fecha = DateTime.Now
                pMovimiento.Fecha_agr = DateTime.Now

                exitoPaso = Aplica_Cambio_Estado_Ubic(pMovimiento,
                                                      pStockRes,
                                                      pIdStockNuevo,
                                                      pIdMovimientoNuevo,
                                                      lConnection,
                                                      lTransaction,
                                                      pPosiciones)

                If Not exitoPaso Then Throw New Exception("Error al aplicar cambio de estado.")

                pStockRes.IdProductoEstado = IdProductoEstadoDestino

                If pIdStockNuevo > 0 Then pStockRes.IdStock = pIdStockNuevo
            End If

            '#EJC20260416: Paso 2 - Implosión
            If requiereImplosion Then
                Validar_Implosion_MismaUbicacionEstado(licenciaOrigen,
                                                       licenciaDestino,
                                                       pMovimiento.IdBodegaDestino,
                                                       lConnection,
                                                       lTransaction,
                                                       False)

                pStockRes.Lic_plate_Anterior = pStockRes.Lic_plate 'pStockRes.Lic_plate_Anterior 'pStockRes.Lic_plate
                pStockRes.Lic_plate = licenciaDestino

                pMovimiento.IdPropietarioBodega = propietarioOriginal
                pMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.PACK
                pMovimiento.Lic_plate = pStockRes.Lic_plate_Anterior
                pMovimiento.Barra_pallet = licenciaDestino
                pMovimiento.IdEstadoOrigen = pStockRes.IdProductoEstado
                pMovimiento.IdEstadoDestino = pStockRes.IdProductoEstado
                pMovimiento.IdUbicacionOrigen = IdUbicacionOrigen
                pMovimiento.IdUbicacionDestino = IdUbicacionOrigen ' 0 'No mover de ubicación, hasta el final.
                pMovimiento.Fecha = DateTime.Now
                pMovimiento.Fecha_agr = DateTime.Now

                Aplica_Implosion(pMovimiento, pStockRes, lConnection, lTransaction, False)
            End If

            '#EJC20260416: Paso 3 - Ubicación
            If requiereCambioUbicacion Then

                pMovimiento.IdPropietarioBodega = propietarioOriginal
                pMovimiento.IdTipoTarea = 2
                pMovimiento.IdUbicacionOrigen = IdUbicacionOrigen
                pMovimiento.IdUbicacionDestino = IdUbicacionDestino
                pMovimiento.IdEstadoOrigen = pStockRes.IdProductoEstado
                pMovimiento.IdEstadoDestino = pStockRes.IdProductoEstado
                pMovimiento.Lic_plate = ""
                pMovimiento.Fecha = DateTime.Now
                pMovimiento.Fecha_agr = DateTime.Now

                exitoPaso = Aplica_Cambio_Estado_Ubic(pMovimiento,
                                                      pStockRes,
                                                      pIdStockNuevo,
                                                      pIdMovimientoNuevo,
                                                      lConnection,
                                                      lTransaction,
                                                      pPosiciones)

                If Not exitoPaso Then Throw New Exception("Error al aplicar cambio de ubicación.")

                pStockRes.IdUbicacion_Anterior = pStockRes.IdUbicacion
                pStockRes.IdUbicacion = IdUbicacionDestino

                If pIdStockNuevo > 0 Then pStockRes.IdStock = pIdStockNuevo
            End If

            Return True

        Catch
            Throw
        End Try

    End Function

    '#EJC20260526:
    'Evita selección ambigua de licencia destino cuando hay más de una licencia activa en el rack.
    Private Shared Function Get_Cantidad_Licencias_Distintas_En_Ubicacion(ByVal pIdUbicacion As Integer,
                                                                           ByVal pIdBodega As Integer,
                                                                           ByVal lConnection As SqlConnection,
                                                                           ByVal lTransaction As SqlTransaction) As Integer
        Try
            Const vSQL As String = "SELECT COUNT(DISTINCT LTRIM(RTRIM(lic_plate))) " &
                                   "FROM vw_stock_res " &
                                   "WHERE IdUbicacion = @IdUbicacion " &
                                   "  AND IdBodega = @IdBodega " &
                                   "  AND ISNULL(LTRIM(RTRIM(lic_plate)), '') <> ''"

            Using cmd As New SqlCommand(vSQL, lConnection, lTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = cmd.ExecuteScalar()
                If lReturnValue IsNot Nothing AndAlso lReturnValue IsNot DBNull.Value Then
                    Return CInt(lReturnValue)
                End If
            End Using

            Return 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
