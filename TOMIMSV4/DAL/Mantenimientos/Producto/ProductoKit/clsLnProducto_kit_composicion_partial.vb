Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_kit_composicion

    Public Shared Function Get_All_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByRef BeListPk As List(Of clsBeProducto_kit_composicion))

        Get_All_By_IdProducto_And_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Producto_kit_composicion WHERE IdProductoPadre=@IdProducto AND IdBodega=@IdBodega"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdProducto", pIdProducto)
            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_kit_composicion As New clsBeProducto_kit_composicion

            For Each dr As DataRow In dt.Rows

                vBeProducto_kit_composicion = New clsBeProducto_kit_composicion
                Cargar(vBeProducto_kit_composicion, dr)
                vBeProducto_kit_composicion.Producto = clsLnProducto.Get_Single_By_IdProducto(vBeProducto_kit_composicion.IdProductoHijo,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                vBeProducto_kit_composicion.Producto.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(vBeProducto_kit_composicion.IdProductoHijo,
                                                                                                                                             pIdBodega)

                BeListPk.Add(vBeProducto_kit_composicion)

            Next

            lTransaction.Commit()

            lConnection.Dispose()

            cmd.Dispose()

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                              ByVal pIdBodega As Integer) As List(Of clsBeProducto_kit_composicion)

        Get_All_By_IdProducto_And_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Producto_kit_composicion WHERE IdProductoPadre=@IdProducto AND IdBodega=@IdBodega"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdProducto", pIdProducto)
            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_kit_composicion As New clsBeProducto_kit_composicion
            Dim BeListPk As New List(Of clsBeProducto_kit_composicion)

            For Each dr As DataRow In dt.Rows

                vBeProducto_kit_composicion = New clsBeProducto_kit_composicion
                Cargar(vBeProducto_kit_composicion, dr)
                vBeProducto_kit_composicion.Producto = clsLnProducto.Get_Single_By_IdProducto(vBeProducto_kit_composicion.IdProductoHijo,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                vBeProducto_kit_composicion.Producto.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(vBeProducto_kit_composicion.IdProductoHijo,
                                                                                                                                             pIdBodega)

                BeListPk.Add(vBeProducto_kit_composicion)

            Next

            lTransaction.Commit()

            Get_All_By_IdProducto_And_IdBodega = BeListPk

            cmd.Dispose()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Disp_And_All_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByRef BeListPk As List(Of clsBeProducto_kit_composicion),
                                                                       ByRef pBeStock As clsBeStock,
                                                                       ByRef lEstado As List(Of clsBeProducto_estado),
                                                                       ByRef lPres As List(Of clsBeProducto_Presentacion),
                                                                       ByVal NoLinea As Integer) As Boolean

        Get_Disp_And_All_By_IdProducto_And_IdBodega = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim vCantidadStockPadre As Double = 0
            Dim vCantidadStockHijos As Double = 0
            Dim vCantidadKit As Double = 0
            Dim vCantHijos As Double = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim vSQL As String = "SELECT * FROM Producto_kit_composicion where IdProductoPadre=@IdProducto AND IdBodega=@IdBodega"

            Dim dad As New SqlDataAdapter(vSQL, lConnection)
            dad.SelectCommand.Transaction = lTransaction
            dad.SelectCommand.CommandType = CommandType.Text

            dad.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_kit_composicion As New clsBeProducto_kit_composicion
            Dim BeProductoEstado As New List(Of clsBeProducto_estado)
            Dim BeProductoPresentacion As New clsBeProducto_Presentacion
            Dim vCantidadRelativaAReceta As Double = 0

            Dim lComponentes As New Dictionary(Of String, Double)

            BeListPk.Clear()

            If dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows

                    vBeProducto_kit_composicion = New clsBeProducto_kit_composicion
                    BeProductoEstado = New List(Of clsBeProducto_estado)
                    BeProductoPresentacion = New clsBeProducto_Presentacion
                    vBeProducto_kit_composicion.BeStock = New clsBeStock
                    vBeProducto_kit_composicion.Producto = New clsBeProducto

                    Cargar(vBeProducto_kit_composicion, dr)

                    vBeProducto_kit_composicion.No_Linea = NoLinea

                    vBeProducto_kit_composicion.Producto = clsLnProducto.Get_Single_By_IdProducto(vBeProducto_kit_composicion.IdProductoHijo,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                    vBeProducto_kit_composicion.Producto.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(vBeProducto_kit_composicion.Producto.IdProducto,
                                                                                                                                                 pIdBodega,
                                                                                                                                                 lConnection,
                                                                                                                                                 lTransaction)

                    'Dim vConEstado As Boolean = pBeStock.IdProductoEstado <> 0

                    'If vConEstado Then
                    '    vBeProducto_kit_composicion.Producto.Stock.IdProductoEstado = pBeStock.IdProductoEstado
                    'End If



                    vBeProducto_kit_composicion.BeStock = clsLnStock.Get_Cantidad_Disp_By_IdProducto(vBeProducto_kit_composicion.Producto,
                                                                                                     pBeStock.IdProductoEstado,
                                                                                                     pIdBodega,
                                                                                                     0,
                                                                                                     False,
                                                                                                     (pBeStock.IdProductoEstado <> 0),
                                                                                                     lConnection,
                                                                                                     lTransaction)

                    BeProductoEstado = clsLnProducto_estado.Get_All_Stock_Con_Estado_By_IdProductoBodega(vBeProducto_kit_composicion.Producto.IdProductoBodega, lConnection, lTransaction)

                    If vBeProducto_kit_composicion.BeStock.IdPresentacion > 0 Then
                        BeProductoPresentacion = clsLnProducto_presentacion.GetSingle(vBeProducto_kit_composicion.BeStock.IdPresentacion, lConnection, lTransaction)
                        lPres.Add(BeProductoPresentacion)
                    End If

                    vCantidadStockHijos += vBeProducto_kit_composicion.BeStock.Cantidad
                    vCantidadRelativaAReceta = Math.Round(vBeProducto_kit_composicion.BeStock.Cantidad / IIf(vBeProducto_kit_composicion.Cantidad > 0, vBeProducto_kit_composicion.Cantidad, 1), 6)
                    vCantidadKit += vBeProducto_kit_composicion.Cantidad

                    lComponentes.Add(vBeProducto_kit_composicion.IdProductoHijo, vCantidadRelativaAReceta)

                    If Not BeProductoEstado Is Nothing Then

                        For Each ObjPE In BeProductoEstado

                            If Not lEstado.Exists(Function(x) x.IdEstado = ObjPE.IdEstado) Then
                                lEstado.Add(ObjPE)
                            End If

                        Next

                    End If

                    BeListPk.Add(vBeProducto_kit_composicion)

                Next

                'No se que es esto...
                vCantHijos = BeListPk.FindAll(Function(x) x.IdProductoPadre = pIdProducto AndAlso x.No_Linea = NoLinea).Count

                'No entiendo para que es esto...
                vCantidadStockHijos = Math.Round(vCantidadStockHijos / vCantHijos, 6)

                'Esto menos...
                vCantidadStockPadre = Math.Round(vCantidadStockHijos / vCantidadKit)

                '#EJC20191105:
                'A partir de los componentes y sus cantidades relativas (pej. si la receta utiliza 10 de A y en stock tengo 100 de A, 
                'entonces la cantidad relativa a la receta es 100/10 = 10.
                'La cantidad máxima que puedo despachar de unidades completas, es entonces la cantidad mínima calculada a partir
                'de los elementos que conforman la receta.
                If Not lComponentes Is Nothing Then
                    If lComponentes.Count > 0 Then
                        Dim CantidadMaximaDeUnidades = lComponentes.Min(Function(x) x.Value)
                        pBeStock.Cantidad = CantidadMaximaDeUnidades
                    Else
                        pBeStock.Cantidad = 0
                    End If
                Else
                    pBeStock.Cantidad = 0
                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    Public Shared Function Get_Disp_And_All_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByRef BeListPk As List(Of clsBeProducto_kit_composicion),
                                                                       ByRef pBeStock As clsBeStock,
                                                                       ByRef lEstado As List(Of clsBeProducto_estado),
                                                                       ByRef lPres As List(Of clsBeProducto_Presentacion),
                                                                       ByVal NoLinea As Integer,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction) As Boolean

        Get_Disp_And_All_By_IdProducto_And_IdBodega = False

        Try

            Dim vCantidadStockPadre As Double = 0
            Dim vCantidadStockHijos As Double = 0
            Dim vCantidadKit As Double = 0
            Dim vCantHijos As Double = 0

            Dim vSQL As String = "SELECT * FROM Producto_kit_composicion where IdProductoPadre=@IdProducto AND IdBodega=@IdBodega"

            Dim dad As New SqlDataAdapter(vSQL, lConnection)
            dad.SelectCommand.Transaction = lTransaction
            dad.SelectCommand.CommandType = CommandType.Text

            dad.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_kit_composicion As New clsBeProducto_kit_composicion
            Dim BeProductoEstado As New List(Of clsBeProducto_estado)
            Dim BeProductoPresentacion As New clsBeProducto_Presentacion
            Dim vCantidadRelativaAReceta As Double = 0

            Dim lComponentes As New Dictionary(Of String, Double)

            BeListPk.Clear()

            If dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows

                    vBeProducto_kit_composicion = New clsBeProducto_kit_composicion
                    BeProductoEstado = New List(Of clsBeProducto_estado)
                    BeProductoPresentacion = New clsBeProducto_Presentacion
                    vBeProducto_kit_composicion.BeStock = New clsBeStock
                    vBeProducto_kit_composicion.Producto = New clsBeProducto

                    Cargar(vBeProducto_kit_composicion, dr)

                    vBeProducto_kit_composicion.No_Linea = NoLinea

                    vBeProducto_kit_composicion.Producto = clsLnProducto.Get_Single_By_IdProducto(vBeProducto_kit_composicion.IdProductoHijo,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                    vBeProducto_kit_composicion.Producto.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(vBeProducto_kit_composicion.Producto.IdProducto,
                                                                                                                                                 pIdBodega,
                                                                                                                                                 lConnection,
                                                                                                                                                 lTransaction)


                    vBeProducto_kit_composicion.BeStock = clsLnStock.Get_Cantidad_Disp_By_IdProducto(vBeProducto_kit_composicion.Producto,
                                                                                                     pBeStock.IdProductoEstado,
                                                                                                     pIdBodega,
                                                                                                     0,
                                                                                                     False,
                                                                                                     (pBeStock.IdProductoEstado <> 0),
                                                                                                     lConnection,
                                                                                                     lTransaction)

                    BeProductoEstado = clsLnProducto_estado.Get_All_Stock_Con_Estado_By_IdProductoBodega(vBeProducto_kit_composicion.Producto.IdProductoBodega, lConnection, lTransaction)

                    If vBeProducto_kit_composicion.BeStock.IdPresentacion > 0 Then
                        BeProductoPresentacion = clsLnProducto_presentacion.GetSingle(vBeProducto_kit_composicion.BeStock.IdPresentacion, lConnection, lTransaction)
                        lPres.Add(BeProductoPresentacion)
                    End If

                    vCantidadStockHijos += vBeProducto_kit_composicion.BeStock.Cantidad
                    vCantidadRelativaAReceta = Math.Round(vBeProducto_kit_composicion.BeStock.Cantidad / IIf(vBeProducto_kit_composicion.Cantidad > 0, vBeProducto_kit_composicion.Cantidad, 1), 6)
                    vCantidadKit += vBeProducto_kit_composicion.Cantidad

                    lComponentes.Add(vBeProducto_kit_composicion.IdProductoHijo, vCantidadRelativaAReceta)

                    If Not BeProductoEstado Is Nothing Then

                        For Each ObjPE In BeProductoEstado

                            If Not lEstado.Exists(Function(x) x.IdEstado = ObjPE.IdEstado) Then
                                lEstado.Add(ObjPE)
                            End If

                        Next

                    End If

                    BeListPk.Add(vBeProducto_kit_composicion)

                Next

                'No se que es esto...
                vCantHijos = BeListPk.FindAll(Function(x) x.IdProductoPadre = pIdProducto AndAlso x.No_Linea = NoLinea).Count

                'No entiendo para que es esto...
                vCantidadStockHijos = Math.Round(vCantidadStockHijos / vCantHijos, 6)

                'Esto menos...
                vCantidadStockPadre = Math.Round(vCantidadStockHijos / vCantidadKit)

                '#EJC20191105:
                'A partir de los componentes y sus cantidades relativas (pej. si la receta utiliza 10 de A y en stock tengo 100 de A, 
                'entonces la cantidad relativa a la receta es 100/10 = 10.
                'La cantidad máxima que puedo despachar de unidades completas, es entonces la cantidad mínima calculada a partir
                'de los elementos que conforman la receta.
                If Not lComponentes Is Nothing Then
                    If lComponentes.Count > 0 Then
                        Dim CantidadMaximaDeUnidades = lComponentes.Min(Function(x) x.Value)
                        pBeStock.Cantidad = CantidadMaximaDeUnidades
                    Else
                        pBeStock.Cantidad = 0
                    End If
                Else
                    pBeStock.Cantidad = 0
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
