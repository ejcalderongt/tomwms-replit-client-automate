Imports System.Reflection
Imports System.Data.SqlClient

Partial Public Class clsLnStock_rec

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Const SP As String = "SELECT ISNULL(Max(IdStockRec),0) FROM stock_rec "

                    Using lCommand As New SqlCommand(SP, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0


            Const SP As String = "SELECT ISNULL(Max(IdStockRec),0) FROM stock_rec "
            Using lCommand As New SqlCommand(SP, pConnection)
                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                'If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                '    lMax = CInt(lReturnValue)
                'End If

                lMax = Convert.ToInt32(lReturnValue)

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxNB(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0


            Const SP As String = "SELECT ISNULL(Max(no_bulto),0) FROM stock_rec"
            Using lCommand As New SqlCommand(SP, pConnection)
                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeStock_rec)

        Dim lReturnList As New List(Of clsBeStock_rec)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM stock_rec WHERE IdRecepcionEnc = @IdRecepcionEnc"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeStockRec As clsBeStock_rec

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeStockRec = New clsBeStock_rec

                                Cargar(BeStockRec, lRow)

                                If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                    BeStockRec.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                                    BeStockRec.ProductoEstado = clsLnProducto_estado.GetSingle(BeStockRec.ProductoEstado.IdEstado, lConnection, lTransaction)
                                    '#EJC20190214: Agregado por el día del cariño! (no bromas, por un error de duplicado en el ingreso de clc)"..
                                    BeStockRec.IdProductoEstado = CType(lRow("IdProductoEstado"), Integer)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    BeStockRec.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    clsLnProducto_presentacion.GetSingle(BeStockRec.Presentacion, lConnection, lTransaction)
                                    BeStockRec.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                End If

                                BeStockRec.IsNew = False

                                lReturnList.Add(BeStockRec)

                            Next

                        End If

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_Barra_Proveedor_Cliente(ByVal Proveedor As String,
                                                              ByVal Barra As String,
                                                              ByVal IdOrdenCompraEnc As Integer) As clsBeStock_rec

        Dim lReturnList As New clsBeStock_rec

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT 0 as IdStockRec,ocenc.IdPropietarioBodega,oc.IdProductoBodega,0 AS IdProductoEstado, oc.idPresentacion as IdPresentacion, oc.IdUnidadMedidaBasica as IdUnidadMedida ,
                            0 as IdUbicacion, 0 As IdUbicacion_anterior, reoc.idrecepcionenc as IdRecepcionEnc , 0 as IdRecepcionDet,0 as IdPedidoEnC, 0 AS IdPickingEnc, 
                            0 as IdDespachoEnc, nav.lote, nav.codigo_barra as lic_plate,0 as serial, (nav.Camas_Por_Tarima * nav.Cajas_Por_Cama) as cantidad, GETDATE() as fecha_ingreso,
                            nav.fecha_vence, 0 as uds_lic_plate,0 as no_bulto, GETDATE() as fecha_manufactura, 
                            0 as añada, 0 as user_agr, GETDATE() as fec_agr, 0 as user_mod, GETDATE() as fec_mod, 1 as activo, 0 as peso, 
                            0 as temperatura, 1 as regularizado, GETDATE() as fecha_regularizacion, null as Atributo_Variante_1, oc.No_Linea as No_linea,nav.Cantidad as Cant_Nav 
                            From i_nav_barras_pallet nav inner join	
                            trans_oc_det oc on nav.Codigo = oc.codigo_producto inner join 
                            trans_oc_enc ocenc on oc.IdOrdenCompraEnc = ocenc.IdOrdenCompraEnc inner join
                            trans_re_oc reoc on reoc.IdOrdenCompraEnc = oc.IdOrdenCompraEnc
                            Where nav.Bodega_Origen = @Proveedor and nav.codigo_barra = @Barra AND oc.IdOrdenCompraEnc = @IdOrdenCompraEnc and nav.Recibido = 0 "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Proveedor", Proveedor)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Barra", Barra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeStock_rec

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeStock_rec

                                Cargar(Obj, lRow)

                                If lRow("Cant_Nav") IsNot DBNull.Value AndAlso lRow("Cant_Nav") IsNot Nothing Then
                                    Obj.Cantidad_Nav = CType(lRow("Cant_Nav"), Double)
                                End If

                                If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                    Obj.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                                    Obj.ProductoEstado = clsLnProducto_estado.GetSingle(Obj.ProductoEstado.IdEstado, lConnection, lTransaction)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    clsLnProducto_presentacion.GetSingle(Obj.Presentacion, lConnection, lTransaction)
                                End If

                                lReturnList = Obj

                            Next

                        End If

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetAllByRecepcion(ByVal pIdRecepcionEnc As Integer,
                                             ByVal Lic_Plate As String,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As List(Of clsBeStock_rec)

        Dim lReturnList As New List(Of clsBeStock_rec)

        Try

            Dim vSQL As String = "SELECT * FROM stock_rec WHERE IdRecepcionEnc = @IdRecepcionEnc 
                    AND Lic_Plate =@Lic_Plate "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lic_Plate", Lic_Plate)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeStock_rec

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeStock_rec

                        Cargar(Obj, lRow)

                        If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                            Obj.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                            Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Integer)
                        End If

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        End If

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeStock_rec)

        Dim lReturnList As New List(Of clsBeStock_rec)

        Try

            Dim vSQL As String = "SELECT * FROM stock_rec WHERE IdRecepcionEnc = @IdRecepcionEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeStock_rec

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeStock_rec

                        Cargar(Obj, lRow)

                        If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                            Obj.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                            Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Integer)
                        End If

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        End If

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Stock_By_IdRecepcionEnc_And_IdRecpecionDet(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer) As List(Of clsBeStock_rec)

        Get_Stock_By_IdRecepcionEnc_And_IdRecpecionDet = Nothing

        Try

            Dim lReturnList As New List(Of clsBeStock_rec)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT * FROM stock_rec WHERE IdRecepcionEnc = @IdRecepcionEnc and IdRecepcionDet =  @IdRecepcionDet"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeStock_rec

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeStock_rec

                                Cargar(Obj, lRow)

                                If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                    Obj.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            Get_Stock_By_IdRecepcionEnc_And_IdRecpecionDet = lReturnList

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Max_LP_Desde_Codigo_Barra(ByVal pIdEmpresa As Integer,
                                                         ByVal pIdBodega As Integer,
                                                         ByVal pIdPropietario As Integer,
                                                         ByVal pIdProducto As Integer) As Integer

        Try

            '#EJC20170905 - Generar el primer pallet en 1 cuando no existe ninguno.
            Dim lMax As Integer = 1

            '#EJC20180411: Anteriormente se buscaba también con un union en stock
            'Pero quité esa condición porque hay un escenario donde el pallet puede contener letras
            'Por lo tanto se debe buscar el maxid de producto_pallet para generar un correlativo único de LP.
            Const SP As String = "SELECT MAX(M.lp) AS LP FROM (
                                      SELECT MAX(CONVERT(INT, ISNULL(SUBSTRING(S.CODIGO_BARRA,12,5),0))) AS lp 
                                      FROM PRODUCTO_PALLET S
                                      INNER JOIN PRODUCTO_BODEGA PB ON PB.IDPRODUCTOBODEGA = S.IDPRODUCTOBODEGA
                                      INNER JOIN PROPIETARIO_BODEGA P ON S.IDPROPIETARIOBODEGA = P.IDPROPIETARIOBODEGA
                                      INNER JOIN BODEGA B ON P.IDBODEGA = B.IDBODEGA
                                      WHERE B.IDEMPRESA = @IDEMPRESA AND B.IDBODEGA = @IDBODEGA
                                      AND P.IDPROPIETARIO = @IDPROPIETARIO And PB.IDPRODUCTO = @IDPRODUCTO) AS M  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(SP, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", pIdEmpresa))
                        lCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
                        lCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIO", pIdPropietario))
                        lCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", pIdProducto))

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Nuevo_Correlativo_LicensePlate(ByVal pIdEmpresa As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal pIdPropietario As Integer,
                                                              pIdProducto As Integer) As String

        Get_Nuevo_Correlativo_LicensePlate = ""

        Try

            Dim vMaxIdPallet As Integer = Get_Max_LP_Desde_Codigo_Barra(pIdEmpresa,
                                                                        pIdBodega,
                                                                        pIdPropietario,
                                                                        pIdProducto)

            'Dim vMaxIdPallet As Integer = clsLnProducto_pallet.MaxID()
            Dim pNumeroLP As String = Right("00" & pIdEmpresa, 2)
            pNumeroLP += Right("00" & pIdBodega, 2)
            pNumeroLP += Right("00" & pIdPropietario, 2)
            pNumeroLP += Right("0000" & pIdProducto.ToString, 5)
            pNumeroLP += Right("0000" & vMaxIdPallet.ToString, 5)

            Return pNumeroLP

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Nuevo_Correlativo_LicensePlate(ByVal pIdEmpresa As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal pIdPropietario As Integer,
                                                              ByVal pIdProductoBodega As Integer,
                                                              ByVal UltimoPalletGenerado As String) As String

        Get_Nuevo_Correlativo_LicensePlate = ""

        Try

            Dim vIdProducto As Integer = clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(pIdProductoBodega)

            Dim vMaxIdPalletUltimo As Integer = 0

            If Not UltimoPalletGenerado.Trim = "" Then
                vMaxIdPalletUltimo = Val(UltimoPalletGenerado.Substring(11, 5))
            Else
                vMaxIdPalletUltimo = Get_Max_LP_Desde_Codigo_Barra(pIdEmpresa,
                                                                   pIdBodega,
                                                                   pIdPropietario,
                                                                   vIdProducto)
            End If

            vMaxIdPalletUltimo += 1

            Dim pNumeroLP As String = Right("00" & pIdEmpresa, 2)
            pNumeroLP += Right("00" & pIdBodega, 2)
            pNumeroLP += Right("00" & pIdPropietario, 2)
            pNumeroLP += Right("0000" & vIdProducto.ToString, 5)
            pNumeroLP += Right("0000" & vMaxIdPalletUltimo.ToString, 5)

            Return pNumeroLP

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Guarda_Stock_Rec(ByVal IdRecepcionEnc As Integer,
                                            ByVal IdBodega As Integer,
                                            ByVal pListStockRec As List(Of clsBeStock_rec),
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As Integer

        Guarda_Stock_Rec = 0

        Dim vRegistros As Integer = 0
        Dim vIdUbicacion As Integer = 0

        Try

            If Not pListStockRec Is Nothing Then

                If pListStockRec.Count > 0 Then

                    '#EJC202408100456AM: En la recepción ciega en el grid, se genera una línea vacía que se puede evitar con este filtro.
                    For Each BeStockRec As clsBeStock_rec In pListStockRec.Where(Function(x) x.Cantidad > 0)

                        If BeStockRec.IdUbicacion = 0 Then
                            Throw New Exception("Error_202303231204: El IdUbicación es 0 para el objeto de stock.")
                        End If

                        If BeStockRec.IsNew Then

                            BeStockRec.IdBodega = IdBodega
                            BeStockRec.IdStockRec = 0
                            BeStockRec.IdRecepcionEnc = IdRecepcionEnc
                            BeStockRec.Fecha_Ingreso = Now
                            BeStockRec.Fec_agr = Now
                            BeStockRec.Fec_mod = Now

                            vIdUbicacion = clsLnBodega_ubicacion.Exists(BeStockRec.IdUbicacion,
                                                                        BeStockRec.IdBodega,
                                                                        lConnection,
                                                                        lTransaction)

                            If vIdUbicacion = 0 Then
                                Throw New Exception("ERROR_202302231645A: El IdUbicación: " & BeStockRec.IdUbicacion & " con el que se intentó insertar el stock para la recepción: " & BeStockRec.IdRecepcionEnc & " no existe. IdProductoBodega: " & BeStockRec.IdProductoBodega & " IdOperador: " & BeStockRec.User_agr)
                            End If

                            If BeStockRec.IdProductoEstado = 0 Then
                                Throw New Exception("ERROR_20240825CKFK: El IdProductoEstado: con el que se intentó insertar el stock para la recepción: " & BeStockRec.IdRecepcionEnc & " no existe. IdProductoBodega: " & BeStockRec.IdProductoBodega & " IdOperador: " & BeStockRec.User_agr)
                            End If

                            vRegistros += Insertar(BeStockRec, lConnection, lTransaction)

                            BeStockRec.IsNew = False

                        Else

                            BeStockRec.Fec_mod = Now

                            vIdUbicacion = clsLnBodega_ubicacion.Exists(BeStockRec.IdUbicacion,
                                                                        BeStockRec.IdBodega,
                                                                        lConnection,
                                                                        lTransaction)

                            If vIdUbicacion = 0 Then
                                Throw New Exception("ERROR_202302231645UPD: El IdUbicación con el que se intentó insertar el stock para la recepción: " & BeStockRec.IdRecepcionEnc & " no existe. IdProductoBodega: " & BeStockRec.IdProductoBodega & " IdOperador: " & BeStockRec.User_agr)
                            End If

                            vRegistros += Actualizar(BeStockRec, lConnection, lTransaction)

                        End If

                    Next

                    Guarda_Stock_Rec = vRegistros

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#GT02052022_0839: guardar stock_rec importación Excel Cealsa, con fecha_llegada del archivo
    Public Shared Sub Guarda_Stock_Rec_By_Excel(ByVal IdRecepcionEnc As Integer,
                                               ByVal IdBodega As Integer,
                                               ByVal pListStockRec As List(Of clsBeStock_rec),
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction)

        Try

            If Not pListStockRec Is Nothing Then

                Dim lMaxIdStockRec As Integer = MaxID(lConnection, lTransaction)

                For Each ObjS As clsBeStock_rec In pListStockRec

                    If ObjS.IsNew Then
                        lMaxIdStockRec += 1
                        ObjS.IdBodega = IdBodega
                        ObjS.IdStockRec = lMaxIdStockRec
                        ObjS.IdRecepcionEnc = IdRecepcionEnc
                        'ObjS.Fecha_Ingreso = Now
                        'ObjS.Fec_agr = Now
                        'ObjS.Fec_mod = Now
                        Insertar(ObjS, lConnection, lTransaction)
                    Else
                        ObjS.Fec_mod = Now
                        Actualizar(ObjS, lConnection, lTransaction)
                    End If

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Actualiza_Stock_Rec(ByVal pListStockRec As List(Of clsBeStock_rec),
                                          ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction)

        Try

            For Each sr As clsBeStock_rec In pListStockRec
                Actualizar(sr, lConnection, lTransaction)
            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Actualiza_Stock_Rec(ByVal pBeStockRec As clsBeStock_rec,
                                          ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction)

        Try

            Actualizar(pBeStockRec, lConnection, lTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function GetSingle(ByRef pBeStock_rec As clsBeStock_rec)

        Try

            Const sp As String = "SELECT * FROM Stock_rec" &
            " Where(IdStockRec = @IdStockRec)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKREC", pBeStock_rec.IdStockRec))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeStock_rec, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByVal lic_plate As String) As clsBeStock_rec

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_rec " &
            " Where(lic_plate = @lic_plate)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", lic_plate))

            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeStock_rec, dt.Rows(0))
                Return pBeStock_rec
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    <Obsolete("#EJC20220830: Este método se encuentra obsoleto, no permite recibir en diferentes bodegas")>
    Public Shared Function GetSingleLP(ByVal lic_plate As String, ByVal IdRecepcionEnc As Integer) As clsBeStock_rec

        GetSingleLP = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_rec 
                                  Where(lic_plate = @lic_plate AND IdRecepcionEnc = @IdRecepcionEnc ) "


            Using lConnection As SqlConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", lic_plate))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", IdRecepcionEnc))

                    Dim dt As New DataTable
                    Dim pBeStock_rec As New clsBeStock_rec
                    dad.Fill(dt)

                    If dt.Rows.Count = 0 Then
                        Throw New Exception(String.Format("Error_20220830_2137: La licencia:{0} no se recibido ", lic_plate))
                    ElseIf dt.Rows.Count = 1 Then
                        Cargar(pBeStock_rec, dt.Rows(0))
                        Return pBeStock_rec
                    ElseIf dt.Rows.Count > 1 Then

                        Dim lStockRec As New List(Of clsBeStock_rec)
                        Dim Obj As New clsBeStock_rec

                        For Each lRow As DataRow In dt.Rows
                            Obj = New clsBeStock_rec
                            Cargar(Obj, lRow)
                            lStockRec.Add(Obj)
                        Next

                        'Se busca el pallet de stock_rec donde no se ha marcado como recibido 
                        '(éste sirve como pivote para recepción donde se recibe parcial el producto en el pallet)
                        'pej. se recibió menos unidades de manipulación por pallet o producto dañado en el estado.
                        Obj = lStockRec.Where(Function(x) x.Activo = 0).FirstOrDefault

                        If Not Obj Is Nothing Then

                            'Ahora buscar la cantidad recibida en los distintos registros adicionados para el mismo palelt.
                            Dim Cantidad_Recibida As Double = (From i As clsBeStock_rec In lStockRec).Sum(Function(x) x.Uds_lic_plate)

                            Obj.Uds_lic_plate = Cantidad_Recibida

                            If (Obj.Uds_lic_plate = Obj.Cantidad) AndAlso Obj.Uds_lic_plate > 0 Then
                                Throw New Exception("Ya se completaron las UMP para esta licencia.")
                            Else
                                Return Obj
                            End If

                        End If

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetIdStockRecByIdRecEncDet(ByVal pIdRecepcionEnc As Integer,
                                                      ByVal pIdRecepcionDet As Integer,
                                                      ByVal pConnection As SqlConnection,
                                                      ByVal pTransaction As SqlTransaction) As Integer

        Dim lReturnId As Integer = 0
        Dim lDTA As New SqlDataAdapter

        Try

            Dim vSQL As String = "SELECT IdStockRec FROM stock_rec 
                                  WHERE IdRecepcionEnc = @IdRecepcionEnc 
                                  AND IdRecepcionDet = @IdRecepcionDet"

            lDTA = New SqlDataAdapter(vSQL, pConnection)
            lDTA.SelectCommand.Transaction = pTransaction

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))
            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionDet", pIdRecepcionDet))

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                lReturnId = lDataTable.Rows(0).Item("IdStockRec")
            End If

            Return lReturnId

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Existe_Lp(ByVal lic_plate As String,
                                     ByVal IdBodega As Integer,
                                     ByVal IdStock As Integer) As Boolean


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            '#CKFK 20211210 Cambié esta consulta para que busque tambien en la tabla stock y que no se repita dentro de una misma bodega
            'Const sp As String = "SELECT * FROM Stock_rec " &
            '" Where(lic_plate = @lic_plate) "
            Dim sp As String = "SELECT IdProductoBodega FROM stock 
                                WHERE(lic_plate = @lic_plate) AND (IdBodega = @IdBodega)
                                UNION  
                                SELECT IdProductoBodega FROM stock_rec 
                                WHERE(lic_plate = @lic_plate)  AND (IdBodega = @IdBodega) "

            If IdStock <> 0 Then
                sp += " AND IdStockRec <> @IdStock "
            End If

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@lic_plate", lic_plate))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            If IdStock <> 0 Then
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", IdStock))
            End If
            dad.SelectCommand.CommandTimeout = 10
            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            If dt.Rows.Count = 0 Then
                Return False
            ElseIf dt.Rows.Count > 0 Then
                Return True
            End If

            lTransaction.Commit()

            cmd.Dispose()
            dad.Dispose()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Existe_Serie(ByVal pSerial As String,
                                        ByVal pIdBodega As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT IdProductoBodega FROM stock 
                                  WHERE(serial = @serial) AND (IdBodega = @IdBodega)
                                  UNION  
                                  SELECT IdProductoBodega FROM stock_rec 
                                  WHERE(serial = @serial)  AND (IdBodega = @IdBodega) "

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@serial", pSerial))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            If dt.Rows.Count = 0 Then
                Return False
            ElseIf dt.Rows.Count > 0 Then
                Return True
            End If

            lTransaction.Commit()

            cmd.Dispose()
            dad.Dispose()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(vMsgError)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Existe_Lp_By_Licencia_And_IdBodega(ByVal lic_plate As String, ByVal IdBodega As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Existe_Lp_By_Licencia_And_IdBodega = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Stock Where(lic_plate = @lic_plate AND IdBodega = @IdBodega) "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", lic_plate))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))

            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            Existe_Lp_By_Licencia_And_IdBodega = dt.Rows.Count > 0

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(vMsgError)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Ubicacion_LP(ByVal lic_plate As String,
                                            ByVal pIdBodega As Integer,
                                            ByVal pIdUbicStock As Integer,
                                            ByRef nombre_ubicacion As String) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT top(1) IdUbicacion, dbo.Nombre_Completo_Ubicacion(IdUbicacion, IdBodega) as Nombre FROM Stock " &
                                 " Where(lic_plate = @lic_plate) AND (IdBodega = @IdBodega) AND (IdUbicacion = @IdUbicacion) "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@lic_plate", lic_plate))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", pIdUbicStock))

            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            If dt.Rows.Count = 0 Then

                nombre_ubicacion = ""
                Return 0

            ElseIf dt.Rows.Count > 0 Then

                nombre_ubicacion = dt.Rows(0).Item("Nombre")
                Return dt.Rows(0).Item("IdUbicacion")

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(vMsgError)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220830:Agregado para recibir licencias de producción de IODESA.
    ''' </summary>
    ''' <param name="lic_plate"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function GetSingleLP(ByVal lic_plate As String,
                                       ByVal pIdRecepcionEnc As Integer,
                                       ByVal pIdBodega As Integer) As clsBeStock_rec

        GetSingleLP = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_rec 
                                  Where(lic_plate = @lic_plate AND IdBodega = @IdBodega AND IdRecepcionEnc= @IdRecepcionEnc ) "


            Using lConnection As SqlConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", lic_plate))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdRecepcionEnc))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdBodega))

                    Dim dt As New DataTable
                    Dim pBeStock_rec As New clsBeStock_rec
                    dad.Fill(dt)

                    If dt.Rows.Count = 0 Then
                        Throw New Exception(String.Format("La licencia no se ha recepcionado:{0} ", lic_plate))
                    ElseIf dt.Rows.Count = 1 Then
                        Cargar(pBeStock_rec, dt.Rows(0))
                        Return pBeStock_rec
                    ElseIf dt.Rows.Count > 1 Then

                        Dim lStockRec As New List(Of clsBeStock_rec)
                        Dim BeStockRec As New clsBeStock_rec

                        For Each lRow As DataRow In dt.Rows
                            BeStockRec = New clsBeStock_rec
                            Cargar(BeStockRec, lRow)
                            lStockRec.Add(BeStockRec)
                        Next

                        BeStockRec = lStockRec.Where(Function(x) x.Activo = 0).FirstOrDefault

                        If Not BeStockRec Is Nothing Then

                            'Ahora buscar la cantidad recibida en los distintos registros adicionados para el mismo palelt.
                            Dim Cantidad_Recibida As Double = (From i As clsBeStock_rec In lStockRec).Sum(Function(x) x.Uds_lic_plate)

                            BeStockRec.Uds_lic_plate = Cantidad_Recibida

                            If (BeStockRec.Uds_lic_plate = BeStockRec.Cantidad) AndAlso BeStockRec.Uds_lic_plate > 0 Then
                                Throw New Exception("Ya se completaron las UMP para esta licencia")
                            Else
                                Return BeStockRec
                            End If

                        End If

                    End If

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingleLP(ByVal lic_plate As String,
                                       ByVal pIdBodega As Integer,
                                       ByVal BeProducto As clsBeProducto) As clsBeStock_rec

        GetSingleLP = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_rec 
                                  Where(lic_plate = @lic_plate AND IdBodega = @IdBodega) "


            Using lConnection As SqlConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", lic_plate))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

                    Dim dt As New DataTable
                    Dim pBeStock_rec As New clsBeStock_rec
                    dad.Fill(dt)

                    If dt.Rows.Count = 0 Then
                        GetSingleLP = Nothing
                        BeProducto = clsLnI_nav_barras_pallet.Get_Single_By_Codigo_Barra_Pallet(lic_plate, pIdBodega, lConnection, lTransaction)
                    ElseIf dt.Rows.Count = 1 Then
                        Cargar(pBeStock_rec, dt.Rows(0))
                        BeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeStock_rec.IdProductoBodega, lConnection, lTransaction)
                        GetSingleLP = pBeStock_rec
                    ElseIf dt.Rows.Count > 1 Then

                        Dim lStockRec As New List(Of clsBeStock_rec)
                        Dim BeStockRec As New clsBeStock_rec

                        For Each lRow As DataRow In dt.Rows
                            BeStockRec = New clsBeStock_rec
                            Cargar(BeStockRec, lRow)
                            lStockRec.Add(BeStockRec)
                        Next

                        BeStockRec = lStockRec.Where(Function(x) x.Activo = 0).FirstOrDefault

                        If Not BeStockRec Is Nothing Then

                            'Ahora buscar la cantidad recibida en los distintos registros adicionados para el mismo palelt.
                            Dim Cantidad_Recibida As Double = (From i As clsBeStock_rec In lStockRec).Sum(Function(x) x.Uds_lic_plate)

                            BeStockRec.Uds_lic_plate = Cantidad_Recibida

                            If (BeStockRec.Uds_lic_plate = BeStockRec.Cantidad) AndAlso BeStockRec.Uds_lic_plate > 0 Then
                                Throw New Exception("Ya se completaron las UMP para esta licencia")
                            Else
                                GetSingleLP = BeStockRec
                                BeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(BeStockRec.IdProductoBodega, lConnection, lTransaction)
                            End If

                        End If

                    End If

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function
    Public Shared Function Get_Single_By_IdRecepcionEnc_And_Licencia(ByVal IdRecepcionEnc As Integer,
                                                                     ByVal Licencia As String,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As clsBeStock_rec

        Get_Single_By_IdRecepcionEnc_And_Licencia = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_rec 
                                  Where(lic_plate = @lic_plate AND IdRecepcionEnc = @IdRecepcionEnc ) "


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", Licencia))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", IdRecepcionEnc))

            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            If dt.Rows.Count = 0 Then
                Throw New Exception(String.Format("Error_20220830_2137: La licencia:{0} no se recibió ", Licencia))
            ElseIf dt.Rows.Count = 1 Then
                Cargar(pBeStock_rec, dt.Rows(0))
                Return pBeStock_rec
            ElseIf dt.Rows.Count > 1 Then

                Dim lStockRec As New List(Of clsBeStock_rec)
                Dim Obj As New clsBeStock_rec

                For Each lRow As DataRow In dt.Rows
                    Obj = New clsBeStock_rec
                    Cargar(Obj, lRow)
                    lStockRec.Add(Obj)
                Next

                'Se busca el pallet de stock_rec donde no se ha marcado como recibido 
                '(éste sirve como pivote para recepción donde se recibe parcial el producto en el pallet)
                'pej. se recibió menos unidades de manipulación por pallet o producto dañado en el estado.
                Obj = lStockRec.Where(Function(x) x.Activo = 0).FirstOrDefault

                If Not Obj Is Nothing Then

                    'Ahora buscar la cantidad recibida en los distintos registros adicionados para el mismo palelt.
                    Dim Cantidad_Recibida As Double = (From i As clsBeStock_rec In lStockRec).Sum(Function(x) x.Uds_lic_plate)

                    Obj.Uds_lic_plate = Cantidad_Recibida

                    If (Obj.Uds_lic_plate = Obj.Cantidad) AndAlso Obj.Uds_lic_plate > 0 Then
                        Throw New Exception("Ya se completaron las UMP para esta licencia.")
                    Else
                        Return Obj
                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#GT13052025: retornar el stock_rec para el proceso cdc
    Public Shared Function GetSingle_Stock_By_IdRecepcionEnc_And_IdRecpecionDet(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer,
                                                                                pLic_plate As String,
                                                                                Optional ByVal pConnection As SqlConnection = Nothing,
                                                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeStock_rec

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        GetSingle_Stock_By_IdRecepcionEnc_And_IdRecpecionDet = Nothing

        Try

            Dim lReturnList As New List(Of clsBeStock_rec)

            Dim vSQL As String = "SELECT * FROM stock_rec WHERE (IdRecepcionEnc = @IdRecepcionEnc and IdRecepcionDet =  @IdRecepcionDet and lic_plate=@pLic_plate and activo=1) "

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)
            lDTA.SelectCommand.Parameters.AddWithValue("@pLic_plate", pLic_plate)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            Dim Obj As clsBeStock_rec

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                Obj = New clsBeStock_rec
                Cargar(Obj, lDataTable.Rows(0))

                If lDataTable.Rows(0)("IdProductoEstado") IsNot DBNull.Value AndAlso lDataTable.Rows(0)("IdProductoEstado") IsNot Nothing Then
                    Obj.ProductoEstado.IdEstado = CType(lDataTable.Rows(0)("IdProductoEstado"), Integer)
                End If

                If lDataTable.Rows(0)("IdPresentacion") IsNot DBNull.Value AndAlso lDataTable.Rows(0)("IdPresentacion") IsNot Nothing Then
                    Obj.Presentacion.IdPresentacion = CType(lDataTable.Rows(0)("IdPresentacion"), Integer)
                End If

                GetSingle_Stock_By_IdRecepcionEnc_And_IdRecpecionDet = Obj

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


End Class