Imports System
Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.DataAccess
Imports DevExpress.DataAccess.Native
Imports DevExpress.XtraEditors.Drawing

Partial Public Class clsLnTrans_oc_enc
    '#20190125: This is a intentional comment
    Public Shared Function Get_Encabezado_OC(ByVal pIdOrdenCompra As Integer,
                                             ByRef pOC As clsBeTrans_oc_enc,
                                             ByRef lTransaction As SqlTransaction,
                                             ByRef lConnection As SqlConnection) As Boolean

        Get_Encabezado_OC = False

        Try

            Dim vSQL As String = " SELECT  enc.*,ti.es_devolucion,ti.nombre AS TipoIngreso FROM Trans_oc_enc AS enc  
                     INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC  
                     WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    pOC = New clsBeTrans_oc_enc()
                    Cargar(pOC, lRow)
                    pOC.IsNew = False
                    Get_Encabezado_OC = True
                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = " SELECT  enc.*,ti.es_devolucion,ti.nombre AS TipoIngreso FROM Trans_oc_enc AS enc  
                   INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC  
                   WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Dim lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim BeTransOcEnc As New clsBeTrans_oc_enc()

                        Cargar(BeTransOcEnc, lRow)

                        If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                            BeTransOcEnc.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                            clsLnPropietario_bodega.Obtener(BeTransOcEnc.PropietarioBodega, lConnection, lTransaction)
                        End If

                        If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                            BeTransOcEnc.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                            clsLnProveedor_bodega.Obtener(BeTransOcEnc.ProveedorBodega, lConnection, lTransaction)
                        End If

                        If lRow("IdTipoIngresoOC") IsNot DBNull.Value AndAlso lRow("IdTipoIngresoOC") IsNot Nothing Then
                            BeTransOcEnc.IdTipoIngresoOC = CType(lRow("IdTipoIngresoOC"), Integer)
                            BeTransOcEnc.TipoIngreso = clsLnTrans_oc_ti.GetSingle(BeTransOcEnc.IdTipoIngresoOC, lConnection, lTransaction)
                        End If

                        BeTransOcEnc.IsNew = False

                        BeTransOcEnc.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(BeTransOcEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                        BeTransOcEnc.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(BeTransOcEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                        BeTransOcEnc.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(BeTransOcEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                        BeTransOcEnc.ObjPoliza = clsLnTrans_oc_pol.GetSingle(BeTransOcEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                        BeTransOcEnc.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(BeTransOcEnc.IdOrdenCompraEnc, lConnection, lTransaction)

                        lTransaction.Commit()

                        lConnection.Close()

                        Return BeTransOcEnc

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_enc

        Get_Single_By_IdOrdenCompraEnc = Nothing

        Try

            Dim vSQL As String = " SELECT  * FROM Trans_oc_enc 
                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_enc()

                            Cargar(Obj, lRow)

                            If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                                Obj.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                                clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega, lConnection, lTransaction)
                            End If

                            If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                                Obj.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                                clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega, lConnection, lTransaction)
                            End If

                            Return Obj

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

    Public Shared Function Get_Single_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As clsBeTrans_oc_enc

        Get_Single_By_IdOrdenCompraEnc = Nothing

        Try

            Dim vSQL As String = " SELECT  * FROM Trans_oc_enc 
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransOCEnc As New clsBeTrans_oc_enc()

                    Cargar(BeTransOCEnc, lRow)

                    If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                        BeTransOCEnc.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                        clsLnPropietario_bodega.Obtener(BeTransOCEnc.PropietarioBodega, lConnection, lTransaction)
                    End If

                    If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                        BeTransOCEnc.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                        clsLnProveedor_bodega.Obtener(BeTransOCEnc.ProveedorBodega, lConnection, lTransaction)
                    End If

                    Return BeTransOCEnc

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdOrdenCompra As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = " SELECT enc.*,ti.es_devolucion,ti.nombre AS TipoIngreso FROM Trans_oc_enc AS enc  
                                                   INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC  
                                                   WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_oc_enc()

                    Cargar(Obj, lRow)

                    If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                        Obj.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                        clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega, lConnection, lTransaction)
                    End If

                    If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                        Obj.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                        clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega, lConnection, lTransaction)
                    End If

                    If lRow("IdTipoIngresoOC") IsNot DBNull.Value AndAlso lRow("IdTipoIngresoOC") IsNot Nothing Then
                        Obj.IdTipoIngresoOC = CType(lRow("IdTipoIngresoOC"), Integer)
                        Obj.TipoIngreso.Nombre = CType(lRow("TipoIngreso"), String)
                    End If

                    Obj.IsNew = False

                    Obj.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(Obj.IdOrdenCompraEnc, lConnection, lTransaction)

                    Obj.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                    Obj.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                    Obj.ObjPoliza = clsLnTrans_oc_pol.GetSingle(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                    Obj.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_No_Pedido(ByVal pIdOrdenCompra As Integer,
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction) As String

        Get_No_Pedido = ""

        Try

            Dim vSQL As String = " SELECT referencia FROM Trans_oc_enc AS enc                                               
                     WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_No_Pedido = IIf(IsDBNull(lRow.Item("Referencia")), "", lRow.Item("Referencia"))
                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_BeTransOcEnc_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_enc

        Get_BeTransOcEnc_By_IdOrdenCompraEnc = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = " SELECT  enc.*,ti.es_devolucion,ti.nombre AS TipoIngreso 
                        FROM Trans_oc_enc AS enc  
                        INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC  
                        WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransOCEnc As New clsBeTrans_oc_enc()

                    Cargar(BeTransOCEnc, lRow)

                    If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                        BeTransOCEnc.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                        clsLnPropietario_bodega.Obtener(BeTransOCEnc.PropietarioBodega, lConnection, lTransaction)
                    End If

                    If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                        BeTransOCEnc.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                        clsLnProveedor_bodega.Obtener(BeTransOCEnc.ProveedorBodega, lConnection, lTransaction)
                    End If

                    If lRow("IdTipoIngresoOC") IsNot DBNull.Value AndAlso lRow("IdTipoIngresoOC") IsNot Nothing Then
                        BeTransOCEnc.IdTipoIngresoOC = CType(lRow("IdTipoIngresoOC"), Integer)
                        BeTransOCEnc.TipoIngreso.Nombre = CType(lRow("TipoIngreso"), String)
                    End If

                    BeTransOCEnc.IsNew = False

                    BeTransOCEnc.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)

                    BeTransOCEnc.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOCEnc.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOCEnc.ObjPoliza = clsLnTrans_oc_pol.GetSingle(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOCEnc.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)

                    Get_BeTransOcEnc_By_IdOrdenCompraEnc = BeTransOCEnc

                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#EJC20180113: Agregué transacción en GetOrdenCompraByPropietario
    Public Shared Function Get_Orden_Compra_By_Propietario(ByVal pIdOrdenCompra As Integer,
                                                           ByVal pIdPropietarioBodega As Integer) As clsBeTrans_oc_enc

        Get_Orden_Compra_By_Propietario = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 enc.*,ti.es_devolucion FROM trans_oc_enc AS enc 
                    INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC 
                    WHERE enc.IdOrdenCompraEnc=@IdOrdenCompraEnc AND enc.IdPropietarioBodega=@IdPropietarioBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_enc()

                            Cargar(Obj, lRow)

                            If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                                Obj.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                                clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega, lConnection, lTransaction)
                            End If

                            If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                                Obj.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                                clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega, lConnection, lTransaction)
                            End If

                            Obj.IsNew = False

                            Obj.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                            Obj.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                            Obj.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                            Obj.ObjPoliza = clsLnTrans_oc_pol.GetSingle(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                            Obj.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)

                            Return Obj

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

    '#EJC20180113: Agregué transacción en Get_Orden_Compra
    Public Shared Function Get_Orden_Compra(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_enc

        Get_Orden_Compra = Nothing

        Try

            '#CKFK 20210317 Modifiqué esta consulta porque ahora Trans_oc_enc tiene IdBodega
            'Dim vSQL As String = "SELECT  enc.*,b.IdBodega,ti.es_devolucion,ti.nombre AS TipoIngreso FROM Trans_oc_enc AS enc " _
            '       & "INNER JOIN propietario_bodega AS pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega " _
            '       & "INNER JOIN bodega AS b ON pb.IdBodega = b.IdBodega " _
            '       & "INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC " _
            '       & "WHERE enc.IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Dim vSQL As String = "SELECT  enc.*,ti.es_devolucion,ti.nombre AS TipoIngreso FROM Trans_oc_enc AS enc " _
                   & "INNER JOIN propietario_bodega AS pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega " _
                   & "INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC " _
                   & "WHERE enc.IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_enc()

                            Cargar(Obj, lRow)

                            Obj.IdBodega = CType(lRow("IdBodega"), Integer)

                            If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                                Obj.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                                clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega, lConnection, lTransaction)
                            End If

                            If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                                Obj.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                                clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega, lConnection, lTransaction)
                            End If

                            If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                                Obj.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                                'clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega, lConnection, lTransaction)
                                Obj.ProveedorBodega.Proveedor.TiemposProveedor = clsLnProveedor_tiempos.Get_All_Tiempos_By_IdProveedor(Obj.ProveedorBodega.IdProveedor,
                                                                                                                                       lConnection,
                                                                                                                                       lTransaction)
                            End If

                            If lRow("IdTipoIngresoOC") IsNot DBNull.Value AndAlso lRow("IdTipoIngresoOC") IsNot Nothing Then
                                Obj.IdTipoIngresoOC = CType(lRow("IdTipoIngresoOC"), Integer)
                                Obj.TipoIngreso.Nombre = CType(lRow("TipoIngreso"), String)
                            End If

                            Obj.IsNew = False

                            Obj.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                            Obj.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                            Obj.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                            Obj.ObjPoliza = clsLnTrans_oc_pol.GetSingle(Obj.IdOrdenCompraEnc, lConnection, lTransaction)
                            Obj.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(Obj.IdOrdenCompraEnc, lConnection, lTransaction)

                            Return Obj

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

    Public Shared Function Get_Orden_Compra(ByVal pIdOrdenCompra As Integer,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_enc

        Get_Orden_Compra = Nothing

        Try

            Dim vSQL As String = "SELECT  enc.*,ti.es_devolucion,ti.nombre AS TipoIngreso FROM Trans_oc_enc AS enc 
                                   INNER JOIN propietario_bodega AS pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega 
                                   INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC 
                                   WHERE enc.IdOrdenCompraEnc=@IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransOcEnc1 As New clsBeTrans_oc_enc()

                    Cargar(BeTransOcEnc1, lRow)

                    BeTransOcEnc1.IdBodega = CType(lRow("IdBodega"), Integer)

                    '#EJC20190401: Obtener estado para la HH por optimización.
                    clsLnTrans_oc_estado.GetSingle(BeTransOcEnc1.EstadoOC, lConnection, lTransaction)

                    If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                        BeTransOcEnc1.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                        clsLnPropietario_bodega.Obtener(BeTransOcEnc1.PropietarioBodega, lConnection, lTransaction)
                    End If

                    If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                        BeTransOcEnc1.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                        clsLnProveedor_bodega.Obtener(BeTransOcEnc1.ProveedorBodega, lConnection, lTransaction)
                    End If

                    If BeTransOcEnc1.ProveedorBodega.IdProveedor > 0 Then
                        BeTransOcEnc1.ProveedorBodega.Proveedor.TiemposProveedor = clsLnProveedor_tiempos.Get_All_Tiempos_By_IdProveedor(BeTransOcEnc1.ProveedorBodega.IdProveedor, lConnection, lTransaction)
                    End If

                    If lRow("IdTipoIngresoOC") IsNot DBNull.Value AndAlso lRow("IdTipoIngresoOC") IsNot Nothing Then
                        BeTransOcEnc1.IdTipoIngresoOC = CType(lRow("IdTipoIngresoOC"), Integer)
                        BeTransOcEnc1.TipoIngreso.Nombre = CType(lRow("TipoIngreso"), String)
                    End If

                    BeTransOcEnc1.IsNew = False
                    BeTransOcEnc1.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOcEnc1.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOcEnc1.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOcEnc1.ObjPoliza = clsLnTrans_oc_pol.GetSingle(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOcEnc1.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(BeTransOcEnc1.IdOrdenCompraEnc, lConnection, lTransaction)

                    Return BeTransOcEnc1

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdOrdenCompraEnc),0) FROM trans_oc_enc"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

            Return lMax

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date,
                                  ByVal pFechaAl As Date,
                                  Optional ByVal pIdBodega As Integer = 0,
                                  Optional ByVal pIdPropietario As Integer = 0) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_OrdenCompra WHERE 1 > 0 "

            If pActivo = True Then
                vSQL += " And Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            vSQL += " AND cast(Fecha AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega=@IdBodega"
            End If

            If pIdPropietario <> 0 Then
                vSQL += " AND IdPropietario=@IdPropietario"
            End If

            If pIdBodega <> 0 AndAlso pIdPropietario <> 0 Then
                vSQL += " AND Estado IN ('NUEVA','BACK ORDER')"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        If pIdBodega <> 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        If pIdPropietario <> 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetImpresionByOC(ByVal pIdOrdenCompraEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try
            Dim vSQL As String = String.Format("SELECT * FROM VW_OrdenCompraPreIngreso WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creada por Erik Calderón
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    Public Shared Sub Set_Estado_Anulado_OC(ByVal pIdOrdenCompraEnc As Integer,
                                            ByVal pIdMotivoAnulacionBodega As Integer,
                                            ByRef pConnection As SqlConnection,
                                            ByRef pTransaction As SqlTransaction)

        Try

            Dim vSQL As String = " UPDATE trans_oc_enc " &
                                 " SET IdEstadoOC=@IdEstadoOC, " &
                                 " IdMotivoAnulacionBodega = @IdMotivoAnulacionBodega " &
                                 " WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdEstadoOC", 5)
                lCommand.Parameters.AddWithValue("@IdMotivoAnulacionBodega", pIdMotivoAnulacionBodega)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Enum pEstadoOC
        NUEVA = 1
        ASIGNADA = 2
        EN_PROCESO = 3
        CERRADA = 4
        ANULADA = 5
        BACK_ORDER = 6
    End Enum

    Public Shared Function Actualizar_Estado_Nueva(ByVal pIdOrdenCompraEnc As Integer,
                                                   ByVal pConnection As SqlConnection,
                                                   ByVal pTransaction As SqlTransaction) As Integer

        Actualizar_Estado_Nueva = 0

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc 
                                  SET IdEstadoOC=1,
                                  Hora_Fin_Recepcion = GETDATE()
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                Actualizar_Estado_Nueva = lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Actualizar_Estado_BackOrder(ByVal pIdOrdenCompraEnc As Integer,
                                                       ByVal pConnection As SqlConnection,
                                                       ByVal pTransaction As SqlTransaction) As Integer

        'El Estado 6 es Back Order para las Ordenes de Compra

        Actualizar_Estado_BackOrder = 0

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc 
                                  SET IdEstadoOC=@IdEstadoOC 
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdEstadoOC", clsDataContractDI.tEstadoOC.BACK_ORDER)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                Actualizar_Estado_BackOrder = lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function
    ''' <summary>
    ''' El Estado 4 es CERRADA para las Ordenes de Compra (Ver tabla trans_oc_estado)
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>    
    Public Shared Function Actualizar_Estado_Cerrada(ByVal pIdOrdenCompraEnc As Integer,
                                                     ByVal pConnection As SqlConnection,
                                                     ByVal pTransaction As SqlTransaction) As Integer

        Actualizar_Estado_Cerrada = 0

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc 
                                  SET IdEstadoOC=4,
                                  Hora_Fin_Recepcion = GETDATE()
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                Actualizar_Estado_Cerrada = lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub Actualizar_Estado(ByVal pEstadoOC As pEstadoOC,
                                        ByVal pIdOrdenCompraEnc As Integer)

        'El Estado 6 es Back Order para las Ordenes de Compra

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc SET IdEstadoOC=@IdEstadoOC 
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@IdEstadoOC", pEstadoOC)
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Actualizar_NoMarchamo(ByVal NoMarchamo As String,
                                            ByVal pIdOrdenCompraEnc As Integer)

        'El Estado 6 es Back Order para las Ordenes de Compra

        Dim vSQL As String = "UPDATE trans_oc_enc SET no_marchamo =@no_marchamo  
                              WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc "

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@no_marchamo", NoMarchamo)
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Actualizar_NoMarchamo(ByVal NoMarchamo As String,
                                            ByVal pIdOrdenCompraEnc As Integer,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction)

        'El Estado 6 es Back Order para las Ordenes de Compra

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc SET no_marchamo =@no_marchamo 
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@no_marchamo", NoMarchamo)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Actualizar_No_Documento_Recepcion_ERP(ByVal no_documento_recepcion_erp As String,
                                                            ByVal pIdOrdenCompraEnc As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc SET no_documento_recepcion_erp =@no_documento_recepcion_erp 
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@no_documento_recepcion_erp", no_documento_recepcion_erp)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Actualizar_No_Documento_Ubicacion_ERP(ByVal no_documento_ubicacion_erp As String,
                                                            ByVal pIdOrdenCompraEnc As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc SET no_documento_ubicacion_erp =@no_documento_devolucion_erp 
                                      WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@no_documento_ubicacion_erp", no_documento_ubicacion_erp)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lCommand.ExecuteNonQuery()
            End Using


        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Actualizar_No_Documento_Ubicacion_ERP(ByVal no_documento_ubicacion_erp As String,
                                                            ByVal pIdOrdenCompraEnc As Integer)

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc SET no_documento_ubicacion_erp =@no_documento_ubicacion_erp 
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@no_documento_ubicacion_erp", no_documento_ubicacion_erp)
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function Actualizar_Estado_BackOrder(ByVal pIdOrdenCompraEnc As Integer) As Integer

        Actualizar_Estado_BackOrder = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#EJC20220720_1331: Colocar enviado a ERP =0 Cuando backorder.
                Dim vSQL As String = "UPDATE trans_oc_enc 
                                      SET IdEstadoOC=@IdEstadoOC, 
                                      Enviado_A_ERP = 0 
                                      WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@IdEstadoOC", 6)
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        Actualizar_Estado_BackOrder = lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub Actualizar_Estado_Cerrada(ByVal pIdOrdenCompraEnc As Integer)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "UPDATE trans_oc_enc SET IdEstadoOC=@IdEstadoOC 
                                      WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@IdEstadoOC", 4)
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function CantidadTransito(ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As Double

        Try

            Dim lCant As Double = 0.0

            Dim vSQL As String = String.Format("SELECT ISNULL(SUM(Cantidad),0) FROM trans_oc_enc AS enc INNER JOIN trans_oc_det AS det ON enc.IdOrdenCompraEnc = det.IdOrdenCompraEnc INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto INNER JOIN producto_presentacion AS pp ON p.IdProducto = pp.IdProducto WHERE enc.IdEstadoOc=1 AND p.IdProducto={0} AND pp.IdPresentacion={1}", pIdProducto, pIdPresentacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)
                        lCommand.CommandType = CommandType.Text
                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lCant = CDbl(lReturnValue)
                        End If
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lCant

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#CKFK 20210114 Agregué la lista de servicios de la póliza
    Public Shared Function Actualizar_Datos(ByVal BeTransOcEnc As clsBeTrans_oc_enc,
                                            ByVal pListObjTD As List(Of clsBeTrans_oc_det),
                                            ByVal pListObjI As List(Of clsBeTrans_oc_imagen),
                                            ByVal pListObjP As List(Of clsBeProducto),
                                            ByVal pListObjServ As List(Of clsBeTrans_oc_servicios),
                                            Optional ByVal pObjP As clsBeTrans_oc_pol = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Actualizar_Datos = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Guarda_Trans_OC_Enc(BeTransOcEnc, lConnection, lTransaction)
            Guarda_Trans_oc_pol(BeTransOcEnc.IdOrdenCompraEnc, pObjP, lConnection, lTransaction)
            Valida_Talla_Color(pListObjTD, lConnection, lTransaction)
            Guarda_Trans_oc_det(BeTransOcEnc.IdOrdenCompraEnc, pListObjTD, lConnection, lTransaction)
            Guarda_Trans_oc_imagen(BeTransOcEnc.IdOrdenCompraEnc, pListObjI, lConnection, lTransaction)
            Guarda_Trans_oc_servicio(BeTransOcEnc.IdOrdenCompraEnc, pListObjServ, lConnection, lTransaction)
            Cambiar_A_Estado_Asignado(BeTransOcEnc.No_Ticket_TMS, lConnection, lTransaction)
            Valida_Documento_Ref(BeTransOcEnc, lConnection, lTransaction)

            For Each Obj As clsBeProducto In pListObjP
                If Obj.IdProducto <> Nothing AndAlso Obj.IdProducto <> 0 Then
                    clsLnProducto.UpdateCosto(lConnection, lTransaction, Obj)
                End If
            Next

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
            Actualizar_Datos = BeTransOcEnc.IdOrdenCompraEnc
        End Try
        '
    End Function

    Private Shared Sub Valida_Talla_Color(ByVal pListObjTD As List(Of clsBeTrans_oc_det),
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)
        Try

            If pListObjTD IsNot Nothing AndAlso pListObjTD.Count > 0 Then

                'el detalle de la lista debe adquirir las propiedades de IdProductoTallaColor y Codigo_producto.
                For Each Detalle As clsBeTrans_oc_det In pListObjTD

                    If Detalle.IsNew Then

                        Detalle.Talla = clsLnTalla.GetSingle(Detalle.Talla.IdTalla, lConnection, lTransaction)
                        Detalle.Color = clsLnColor.GetSingle(Detalle.Color.IdColor, lConnection, lTransaction)

                        Detalle.Producto.IdProducto = clsLnProducto.Get_IdProducto_By_Codigo(Detalle.Codigo_Producto, lConnection, lTransaction)

                        Dim producto_talla_color = clsLnProducto_talla_color.Get_Single_By_IdProducto(Detalle.Producto.IdProducto,
                                                                                            Detalle.Talla.Codigo,
                                                                                            Detalle.Color.Codigo,
                                                                                            lConnection,
                                                                                            lTransaction)

                        If producto_talla_color Is Nothing Then

                            Dim pProducto_Talla_color As New clsBeProducto_talla_color()
                            pProducto_Talla_color.IdProductoTallaColor = clsLnProducto_talla_color.MaxID(lConnection, lTransaction) + 1
                            pProducto_Talla_color.IdColor = Detalle.Color.IdColor
                            pProducto_Talla_color.IdTalla = Detalle.Talla.IdTalla
                            pProducto_Talla_color.IdProducto = Detalle.Producto.IdProducto
                            pProducto_Talla_color.Activo = 1
                            pProducto_Talla_color.Fec_mod = Now
                            pProducto_Talla_color.Fec_agr = Now
                            pProducto_Talla_color.IdCampaña = 0
                            pProducto_Talla_color.User_agr = 1
                            pProducto_Talla_color.User_mod = 1
                            pProducto_Talla_color.CodigoSKU = Detalle.Codigo_Producto + Detalle.Color.Codigo + Detalle.Talla.Codigo

                            clsLnProducto_talla_color.Insertar(pProducto_Talla_color, lConnection, lTransaction)

                            Detalle.IdProductoTallaColor = pProducto_Talla_color.IdProductoTallaColor
                            Detalle.Codigo_Producto = pProducto_Talla_color.CodigoSKU
                        Else

                            Dim nuevo_codigo = Detalle.Codigo_Producto + "" + Detalle.Talla.Codigo + "" + Detalle.Color.Codigo
                            Detalle.Codigo_Producto = nuevo_codigo
                            Detalle.IdProductoTallaColor = producto_talla_color.IdProductoTallaColor

                        End If

                    End If

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Sub Cambiar_A_Estado_Asignado(ByVal No_Ticket_TMS As String,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction)

        Try

            If No_Ticket_TMS <> "" Then
                clsLnTms_ticket.Actualizar_Tms_Ticket_Asignado(No_Ticket_TMS, lConnection, lTransaction)
            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Guardar_Estado_Ticket(No_Ticket_TMS As String, estado_ticket As String)

        Try

            If No_Ticket_TMS <> "" Then
                clsLnTms_ticket.Actualizar_Tms_Ticket(No_Ticket_TMS, estado_ticket)
            End If

        Catch ex As Exception

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Cambiar_A_Estado_Procesado(ByVal No_Ticket_TMS As String,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction)

        Try

            If No_Ticket_TMS <> "" Then
                clsLnTms_ticket.Actualizar_Tms_Ticket_Procesado(No_Ticket_TMS,
                                                                lConnection,
                                                                lTransaction)
            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function Actualizar_Datos_Enc(ByRef pObjE As clsBeTrans_oc_enc) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Actualizar_Datos_Enc = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Guarda_Trans_OC_Enc(pObjE, lConnection, lTransaction)

            lTransaction.Commit()

            Actualizar_Datos_Enc = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
            Actualizar_Datos_Enc = pObjE.IdOrdenCompraEnc
        End Try
        '
    End Function

    Public Shared Function Guarda_Trans_OC_Enc(ByRef pObjE As clsBeTrans_oc_enc,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As Boolean


        Guarda_Trans_OC_Enc = False

        Try

            If pObjE.IsNew Then
                pObjE.IdOrdenCompraEnc = MaxID(lConnection, lTransaction) + 1
                pObjE.No_Documento = Genera_Correlativo_OC(pObjE.IdOrdenCompraEnc.ToString)
                pObjE.IsNew = False
                Insertar(pObjE,
                         lConnection,
                         lTransaction)

            Else
                Actualizar(pObjE,
                           lConnection,
                           lTransaction)
            End If

            Guarda_Trans_OC_Enc = True

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, pObjE.IdBodega, pObjE.User_Agr, ex.StackTrace, pObjE.IdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Private Shared Sub Valida_Documento_Ref(ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction)

        Try

            If BeTransOCEnc.IdNoDocumentoRef <> 0 Then
                clsLnTrans_oc_docu_ref.Actualizar_Fecha_Asignacion(BeTransOCEnc.IdNoDocumentoRef, lConnection, lTransaction)
            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, BeTransOCEnc.IdBodega, BeTransOCEnc.User_Agr, ex.StackTrace, BeTransOCEnc.IdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Guarda_Trans_oc_pol(ByVal IdOrdenCompraEnc As Integer,
                                          ByVal pObjP As clsBeTrans_oc_pol,
                                          ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction)

        Try

            If pObjP IsNot Nothing Then

                If pObjP.IsNew Then
                    pObjP.IdOrdenCompraPol = clsLnTrans_oc_pol.MaxID(IdOrdenCompraEnc, lConnection, lTransaction)
                    pObjP.IdOrdenCompraEnc = IdOrdenCompraEnc
                    clsLnTrans_oc_pol.Insertar(pObjP, lConnection, lTransaction)
                Else
                    clsLnTrans_oc_pol.Actualizar(pObjP, lConnection, lTransaction)
                End If

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, IdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Shared Sub Guarda_Trans_oc_det(ByVal IdOrdenCompraEnc As Integer,
                                               ByVal pListObjTD As List(Of clsBeTrans_oc_det),
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)


        Dim BeTransOcDetExistente As New clsBeTrans_oc_det

        Try

            If pListObjTD IsNot Nothing Then

                Dim lMax As Integer = clsLnTrans_oc_det.MaxID(IdOrdenCompraEnc, lConnection, lTransaction)

                For Each Obj As clsBeTrans_oc_det In pListObjTD

                    If Obj.IsNew Then
                        lMax += 1
                        Obj.IdOrdenCompraDet = lMax
                        Obj.IdOrdenCompraEnc = IdOrdenCompraEnc
                        clsLnTrans_oc_det.Insertar(Obj, lConnection, lTransaction)

                        '#MECR06102025: Se agrego bitacora de logs para OC
                        Dim objError As New clsBeLog_error_wms_oc
                        objError.MensajeError = "Se registró el detalle " + lMax.ToString() + " del producto: " + Obj.Codigo_Producto
                        objError.Fecha = Now
                        objError.Cantidad = Obj.Cantidad
                        objError.Codigo_producto = Obj.Codigo_Producto
                        objError.IdOrdenCompraEnc = IdOrdenCompraEnc
                        objError.IdOrdenCompraDet = lMax
                        objError.IdUsuarioAgr = Obj.User_agr
                        objError.UmBas = Obj.IdUnidadMedidaBasica

                        clsLnLog_error_wms_oc.Insertar(objError, lConnection, lTransaction)

                    Else

                        '#EJC20190625: Validación de concurrencia en transaccion.
                        'Si el pedido de compra se abre en la PC, mientras la HH está recibiendo, la cantidad recibida del pedido de compra en el BOF
                        'será menor (o puede ser menor) que la cantidad recibida en la HH y si se presiona actualizar, se actualizará con el documento
                        'con una cantidad menor a la que la HH ya procesó, por eso se obtiene el último valor registrado por la HH y se compara contra el que se está
                        'intentando actualizar, si es menor se actualiza con el mayor (el de la HH).
                        'tengo un sueño dlgp son las 2.15 am, pero estamos a unos dias del piloto de salida en Idealsa,
                        'hoy es un dia martes que empezó lunes, 
                        'el dia fue una aventura, G.M me tiene ingobernable jaja. -: 14/06/2019 Al 14/08/2019                    
                        BeTransOcDetExistente = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(Obj.IdOrdenCompraEnc,
                                                                                                                    Obj.IdOrdenCompraDet,
                                                                                                                    Obj.IdProductoBodega,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)

                        If Not BeTransOcDetExistente Is Nothing Then
                            If BeTransOcDetExistente.Cantidad_recibida > Obj.Cantidad_recibida Then
                                Obj.Cantidad_recibida = BeTransOcDetExistente.Cantidad_recibida
                            End If
                        End If

                        clsLnTrans_oc_det.Actualizar(Obj, lConnection, lTransaction)

                        '#MECR06102025: Se agrego bitacora de logs para OC
                        Dim objError As New clsBeLog_error_wms_oc
                        objError.MensajeError = "Se atualizó el detalle " + lMax.ToString() + " del producto " + Obj.Codigo_Producto
                        objError.Fecha = Now
                        objError.Cantidad = Obj.Cantidad
                        objError.Codigo_producto = Obj.Codigo_Producto
                        objError.IdOrdenCompraEnc = IdOrdenCompraEnc
                        objError.IdOrdenCompraDet = lMax
                        objError.IdUsuarioAgr = Obj.User_agr
                        objError.UmBas = Obj.IdUnidadMedidaBasica

                        clsLnLog_error_wms_oc.Insertar(objError, lConnection, lTransaction)
                    End If

                Next

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, IdOrdenCompraEnc)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub Guarda_Trans_oc_servicio(ByVal pIdOrdenCompraEnc As Integer,
                                                    ByVal pListObjServicio As List(Of clsBeTrans_oc_servicios),
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction)

        Try

            If pListObjServicio IsNot Nothing Then

                Dim BeTransOcDetExistente As New clsBeTrans_oc_servicios

                Dim lMax As Integer = clsLnTrans_oc_servicios.MaxID(lConnection, lTransaction)

                For Each Obj As clsBeTrans_oc_servicios In pListObjServicio

                    If Obj.IsNew Then
                        lMax += 1
                        Obj.IdOrdenCompraServicio = lMax
                        Obj.IdOrdenCompraEnc = pIdOrdenCompraEnc
                        clsLnTrans_oc_servicios.Insertar(Obj, lConnection, lTransaction)
                    Else
                        clsLnTrans_oc_servicios.Actualizar_By_OC_IdOrdencompraServicio(Obj, lConnection, lTransaction)
                    End If

                Next

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub


    'Public Shared Function Actualizar_Ticket(ByVal Tms_Ticket As Integer, ByVal Estado_Ticket As String) As Integer

    '    Update_Tms_Ticket(Tms_Ticket, Estado_Ticket)


    'End Function

    'Private Shared Sub Update_Tms_Ticket(ByVal tms_ticket As Integer, ByVal Estado_Ticket As String)

    '    Try

    '        clsLnTms_ticket.ActualizarTmsTicket(tms_ticket, Estado_Ticket)

    '    Catch ex As Exception
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '    End Try

    'End Sub

    Private Shared Sub Guarda_Trans_oc_imagen(ByVal IdOrdenCompraEnc As Integer,
                                               ByVal pListObjI As List(Of clsBeTrans_oc_imagen),
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Try

            If pListObjI IsNot Nothing Then

                Dim ObjI As New clsLnTrans_oc_imagen()

                For Each Obj As clsBeTrans_oc_imagen In pListObjI
                    If Obj.IsNew Then
                        Obj.IdOrdenCompraEnc = IdOrdenCompraEnc
                        ObjI.Insertar(Obj, lConnection, lTransaction)
                    Else
                        ObjI.Actualizar(Obj, lConnection, lTransaction)
                    End If
                Next

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, IdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function Genera_Correlativo_OC() As String

        Genera_Correlativo_OC = Right("000000" & 1, 7)

        Try

            Dim MaxID As String = clsLnTrans_oc_enc.MaxID().ToString

            Genera_Correlativo_OC = Right("000000" & MaxID, 7)

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Genera_Correlativo_OC(ByVal pIdOrdenCompraEnc As String) As String

        Genera_Correlativo_OC = "OC"

        Try

            Genera_Correlativo_OC = Right("000000" & pIdOrdenCompraEnc, 7)

            Return Genera_Correlativo_OC

        Catch ex As Exception
            Return "OC0"
        End Try

    End Function

    Public Shared Function Anular_OC(ByVal pIdOrdenCompraEnc As Integer,
                                     ByVal pObjPolizaOC As clsBeTrans_oc_pol,
                                     ByVal pIdMotivoAnulacionBodega As Integer,
                                     ByVal pIdBodega As Integer) As Boolean

        Anular_OC = False

        Dim lListRecepciones As New List(Of Integer)

        Try
            lListRecepciones = clsLnTrans_re_oc.Get_IdRecepcionEnc_By_IdOrdenCompraEnc(pIdOrdenCompraEnc).ToList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            ' Anulamos todas las recepciones ligadas a la orden de compra
            For Each recepcion As Integer In lListRecepciones
                clsLnTrans_re_enc.Anular(recepcion,
                                         lConnection,
                                         lTransaction)
            Next

            ' Anulamos la orden de compra correspondiente
            Set_Estado_Anulado_OC(pIdOrdenCompraEnc,
                                  pIdMotivoAnulacionBodega,
                                  lConnection,
                                  lTransaction)


            'GT 27082021: se cambia el estado de la tarea, para que no la cargue en el monitor si fue anulada
            clsLnTarea_hh.Anular_By_IdTranHH(pIdOrdenCompraEnc,
                                             pIdBodega,
                                             lConnection,
                                             lTransaction)


            '#GT12062024: anular el estado de la póliza asociada
            If pObjPolizaOC IsNot Nothing Then
                pObjPolizaOC.activo = 0
                clsLnTrans_oc_pol.Desactivar_Pol_By_Numero_Orden_and_OC(pObjPolizaOC, lConnection, lTransaction)
            End If

            lTransaction.Commit()

            Anular_OC = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar_Inicio_Recepcion_OC(ByVal pIdOrdenCompraEnc As Integer,
                                                          Optional ByVal pConection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("trans_oc_enc")
            Upd.Add("idestadooc", "@idestadooc", DataType.Parametro)
            Upd.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Upd.Add("hora_inicio_recepcion", "@hora_inicio_recepcion", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", pIdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOOC", 3))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", Now))
            cmd.Parameters.Add(New SqlParameter("@HORA_INICIO_RECEPCION", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If Not cmd Is Nothing Then cmd.Dispose()
        End Try

    End Function

    Public Shared Function Iniciar_Recepcion_OC(ByVal pIdOrdenCompraEnc As Integer,
                                                ByVal pIdRecepcionEnc As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing


        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Actualizar_Inicio_Recepcion_OC(pIdOrdenCompraEnc, lConnection, lTransaction)

            Dim BeReOC As New clsBeTrans_re_oc
            'GT08122021: se incluye dentro de la tran, porque estaba fuera
            BeReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(pIdOrdenCompraEnc, pIdRecepcionEnc,
                                                                                        lConnection,
                                                                                        lTransaction)

            If Not BeReOC Is Nothing Then

                clsLnTrans_re_oc.Actualizar_Hora_Inicio_Recepcion(BeReOC.IdRecepcionOc,
                                                                  lConnection,
                                                                  lTransaction)

            End If

            clsLnTrans_re_enc.Actualizar_Estado_Recepcion(pIdRecepcionEnc, "Pendiente",
                                                                  lConnection,
                                                                  lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Tipo_Documento(ByVal Referencia As String) As Integer

        Get_Tipo_Documento = -1

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = " SELECT IdTipoIngresoOC FROM Trans_oc_enc" &
                                 " Where(Referencia = @Referencia)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", Referencia))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("IdTipoIngresoOC")), -1, dt.Rows(0).Item("IdTipoIngresoOC"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single_By_Referencia(ByRef pBeTrans_oc_enc As clsBeTrans_oc_enc,
                                                    ByVal pConection As SqlConnection,
                                                    ByVal pTransaction As SqlTransaction,
                                                    Optional ByRef Llenar_Lotes As Boolean = False) As clsBeTrans_oc_enc

        Get_Single_By_Referencia = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_enc 
                                  Where(Referencia = @Referencia AND IdTipoIngresoOC = @IdTipoIngresoOC)"

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", pBeTrans_oc_enc.Referencia))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTipoIngresoOC", pBeTrans_oc_enc.IdTipoIngresoOC))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim BeOcEnc As New clsBeTrans_oc_enc()
                Cargar(BeOcEnc, dt.Rows(0))

                If Llenar_Lotes Then
                    BeOcEnc.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(BeOcEnc.IdOrdenCompraEnc,
                                                                                          pConection,
                                                                                          pTransaction)
                End If

                Return BeOcEnc

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, pBeTrans_oc_enc.IdBodega, pBeTrans_oc_enc.User_Agr, ex.StackTrace, pBeTrans_oc_enc.IdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Single_By_NoDocumento(ByVal pNoDocumento As String,
                                                     ByVal pConection As SqlConnection,
                                                     ByVal pTransaction As SqlTransaction,
                                                     Optional ByRef Llenar_Lotes As Boolean = False) As clsBeTrans_oc_enc

        Get_Single_By_NoDocumento = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_enc " &
                                 " Where(No_Documento = @No_Documento)"

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@No_Documento", pNoDocumento))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim BeOcEnc As New clsBeTrans_oc_enc()
                Cargar(BeOcEnc, dt.Rows(0))

                If Llenar_Lotes Then
                    BeOcEnc.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(BeOcEnc.IdOrdenCompraEnc, pConection, pTransaction)
                End If

                Return BeOcEnc

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingleForMovs(ByVal pIdOrdenCompra As Integer, ByVal pIdBodega As Integer) As clsBeTrans_oc_enc

        GetSingleForMovs = Nothing

        Try

            Dim vSQL As String = "SELECT * 
                                      FROM Trans_oc_enc INNER JOIN 
                                      propietario_bodega on trans_oc_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega
                                      WHERE Trans_oc_enc.IdOrdenCompraEnc = @IdOrdenCompraEnc AND propietario_bodega.IdBodega = @IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_enc()

                            Cargar(Obj, lRow)

                            Return Obj

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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenCompraEnc),0) FROM trans_oc_enc"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text

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

    Public Shared Function Actualizar_Estado_OC(ByVal pRecEnc As clsBeTrans_re_enc,
                                                ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As Integer

        Actualizar_Estado_OC = 0

        Dim vResult As Integer = 0

        Try

            Dim boOC As New clsBeTrans_oc_enc() With {.IdOrdenCompraEnc = pRecOrdenCompra.IdOrdenCompraEnc}

            Obtener(boOC,
                    lConnection,
                    lTransaction)

            If pRecEnc.IsNew Then

                '#CKFK 20180517 01:15 PM Actualizar campo Enviado_A_ERP si el estado es 6 de la orden de compra 
                If boOC.IdEstadoOC = 6 Then boOC.Enviado_A_ERP = False

                boOC.IdEstadoOC = 2
                Actualizar_Estado_OC = Actualizar(boOC, lConnection, lTransaction)

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pRecOrdenCompra.IdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub Actualizar_Estado_OC_By_Interface(ByVal pIdOrdenCompraEnc As Integer, ByVal IdEstadoOC As Integer)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim boOC As New clsBeTrans_oc_enc() With {.IdOrdenCompraEnc = pIdOrdenCompraEnc}
                    Obtener(boOC, lConnection, lTransaction)

                    'Que no esté cerrada y no esté anulada
                    If boOC.IdEstadoOC <> clsDataContractDI.tEstadoOC.CERRADA AndAlso boOC.IdEstadoOC <> clsDataContractDI.tEstadoOC.ANULADA Then

                        Const vSQL As String = "UPDATE trans_oc_enc SET IdEstadoOC=@IdEstadoOC
                        WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

                        '#EJC20180810_0349AM: Se agregó transacción por un error reportado en la interface según correo de Ricardo, jueves, 09 de agosto de 2018 04:41 p.m. asunto ->   RE: RECEPCION DE COMPRA REGISTRADA EN NAV.
                        Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                            lCommand.Parameters.AddWithValue("@IdEstadoOC", IdEstadoOC)
                            lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                            lCommand.ExecuteNonQuery()
                        End Using

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function Actualizar_Estado_Enviado_A_ERP(ByVal pIdOrdenCompraEnc As Integer,
                                                           ByVal pEnviado_A_ERP As Boolean) As Integer

        Actualizar_Estado_Enviado_A_ERP = 0

        Try

            '·EJC202208111542: Cambiar a estado Cerrada (4)
            Dim vSQL As String = "UPDATE trans_oc_enc SET Enviado_A_ERP=@Enviado_A_ERP, IdEstadoOC = 4
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        Actualizar_Estado_Enviado_A_ERP = lCommand.ExecuteNonQuery()
                    End Using

                    '#EJC20191227: Marcar los registros en la tabla intermedia.
                    Actualizar_Estado_Enviado_A_ERP += clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                                                   pEnviado_A_ERP,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction)

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#EJC20210715: Actualizar_Estado_Enviado_A_ERP_By_Referencia (Recibe parámetro IdEstado como Enum)
    Public Shared Function Actualizar_Estado_Enviado_A_ERP_By_Referencia(ByVal pReferencia As String,
                                                                         ByVal pEnviadoAERP As Boolean,
                                                                         ByVal pIdEstado As clsDataContractDI.tEstadoOC) As Integer

        Actualizar_Estado_Enviado_A_ERP_By_Referencia = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " UPDATE trans_oc_enc 
                    SET Enviado_A_ERP=@Enviado_A_ERP,
                    IdEstadoOC=@IdEstadoOC
                    WHERE Referencia=@pReferencia "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviadoAERP)
                        lCommand.Parameters.AddWithValue("@IdEstadoOC", pIdEstado)
                        lCommand.Parameters.AddWithValue("@pReferencia", pReferencia)
                        Actualizar_Estado_Enviado_A_ERP_By_Referencia = lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Documento_Ingreso_By_Referencia(ByVal pReferencia As String,
                                                                             ByVal pEnviado_A_ERP As Boolean) As Integer

        Actualizar_Estado_Documento_Ingreso_By_Referencia = 0

        Try

            Dim vSQL As String = " UPDATE trans_oc_enc 
                                    SET Enviado_A_ERP=@Enviado_A_ERP,
                                    IdEstadoOC=4
                                    WHERE Referencia=@pReferencia "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                        lCommand.Parameters.AddWithValue("@pReferencia", pReferencia)
                        Actualizar_Estado_Documento_Ingreso_By_Referencia = lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Documento_Ingreso_By_No_Documento_Devolucion(ByVal pNo_Documento_Devolucion As String,
                                                                                          ByVal pEnviado_A_ERP As Boolean) As Integer

        Actualizar_Estado_Documento_Ingreso_By_No_Documento_Devolucion = 0

        Dim vResult As Integer = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " UPDATE trans_oc_enc 
                                            SET Enviado_A_ERP=@Enviado_A_ERP,
                                            IdEstadoOC=4
                    WHERE No_Documento_Devolucion=@No_Documento_Devolucion "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                        lCommand.Parameters.AddWithValue("@No_Documento_Devolucion", pNo_Documento_Devolucion)
                        vResult = lCommand.ExecuteNonQuery()

                    End Using

                    '#EJC20191227: Marcar los registros en la tabla intermedia.
                    vResult += clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado_By_No_Documento_Salida_Ref_Devol(pNo_Documento_Devolucion,
                                                                                                                        pEnviado_A_ERP,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Actualizar_Estado_Documento_Ingreso_By_No_Documento_Devolucion = vResult

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function



    Public Shared Function Obtener(ByRef oBeTrans_oc_enc As clsBeTrans_oc_enc,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Trans_oc_enc 
                                  Where(IdOrdenCompraEnc = @IdOrdenCompraEnc) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_enc.IdOrdenCompraEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_oc_enc, dt.Rows(0))
                clsLnTrans_oc_estado.GetSingle(oBeTrans_oc_enc.EstadoOC, lConnection, lTransaction)
                Return True
            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, oBeTrans_oc_enc.IdBodega, oBeTrans_oc_enc.User_Agr, ex.StackTrace, oBeTrans_oc_enc.IdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Es_Devolucion(ByVal pIdTipoIngresoOC As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_oc_ti WHERE es_devolucion=1 AND IdTipoIngresoOC=@IdTipoIngresoOC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdTipoIngresoOC", pIdTipoIngresoOC)
                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Control_Poliza(ByVal pIdTipoIngresoOC As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_oc_ti WHERE control_poliza=1 AND IdTipoIngresoOC=@IdTipoIngresoOC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdTipoIngresoOC", pIdTipoIngresoOC)
                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Tiene_Pendientes(ByVal pIdOrdenCompra As Integer) As Boolean

        Tiene_Pendientes = False

        Try

            Dim vSQL As String = "SELECT sum(cantidad-cantidad_recibida) as pendiente 
                    from trans_oc_det inner join 
                    trans_oc_enc on trans_oc_enc.IdOrdenCompraEnc = trans_oc_det.IdOrdenCompraEnc
                    where trans_oc_enc.IdOrdenCompraEnc= @IdOrdenCompraEnc and trans_oc_enc.IdEstadoOC = 4"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Tiene_Pendientes = CInt(lReturnValue) > 0
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

    Public Shared Function Get_Parametros_Devol_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer,
                                                                    ByRef pIdPedidoEncDevol As Integer,
                                                                    ByRef pNoDocumentoRefDevol As String) As Boolean

        Get_Parametros_Devol_By_IdOrdenCompraEnc = False

        Try

            Dim vSQL As String = " SELECT IdPedidoEncDevolucion, no_documento_devolucion 
                                   FROM Trans_oc_enc 
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            pIdPedidoEncDevol = IIf(IsDBNull(lDT.Rows(0).Item("IdPedidoEncDevolucion")), 0, lDT.Rows(0).Item("IdPedidoEncDevolucion"))
                            pNoDocumentoRefDevol = IIf(IsDBNull(lDT.Rows(0).Item("no_documento_devolucion")), 0, lDT.Rows(0).Item("no_documento_devolucion"))
                            Get_Parametros_Devol_By_IdOrdenCompraEnc = True

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

    Public Shared Function Get_Parametros_Devol_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer,
                                                                    ByRef pIdPedidoEncDevol As Integer,
                                                                    ByRef pNoDocumentoRefDevol As String,
                                                                    ByVal lConnection As SqlConnection,
                                                                    ByVal lTransaction As SqlTransaction) As Boolean

        Get_Parametros_Devol_By_IdOrdenCompraEnc = False

        pIdPedidoEncDevol = 0 : pNoDocumentoRefDevol = ""

        Try

            Dim vSQL As String = " SELECT IdPedidoEncDevolucion, no_documento_devolucion 
                                   FROM Trans_oc_enc 
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing Then

                    If lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        pIdPedidoEncDevol = IIf(IsDBNull(lDT.Rows(0).Item("IdPedidoEncDevolucion")), 0, lDT.Rows(0).Item("IdPedidoEncDevolucion"))
                        pNoDocumentoRefDevol = IIf(IsDBNull(lDT.Rows(0).Item("no_documento_devolucion")), 0, lDT.Rows(0).Item("no_documento_devolucion"))
                        Get_Parametros_Devol_By_IdOrdenCompraEnc = True

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdTipoDocumento_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer) As clsDataContractDI.tTipoDocumentoIngreso

        Get_IdTipoDocumento_By_IdOrdenCompraEnc = 0

        Try

            Dim vSQL As String = " SELECT IdTipoIngresoOC FROM Trans_oc_enc 
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdTipoDocumento_By_IdOrdenCompraEnc = IIf(IsDBNull(lDT.Rows(0).Item("IdTipoIngresoOC")), 0, lDT.Rows(0).Item("IdTipoIngresoOC"))

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

    Public Shared Function Get_IdTipoDocumento_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer,
                                                                   ByVal lConnection As SqlConnection,
                                                                   ByVal lTransaction As SqlTransaction) As clsDataContractDI.tTipoDocumentoIngreso

        Get_IdTipoDocumento_By_IdOrdenCompraEnc = 0

        Try

            Dim vSQL As String = " SELECT IdTipoIngresoOC FROM Trans_oc_enc 
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Get_IdTipoDocumento_By_IdOrdenCompraEnc = IIf(IsDBNull(lDT.Rows(0).Item("IdTipoIngresoOC")), 0, lDT.Rows(0).Item("IdTipoIngresoOC"))

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_BeTipoDocumento_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer,
                                                                   ByVal lConnection As SqlConnection,
                                                                   ByVal lTransaction As SqlTransaction) As clsBeTrans_oc_ti

        Get_BeTipoDocumento_By_IdOrdenCompraEnc = Nothing

        Try

            Dim vSQL As String = " SELECT B.* FROM Trans_oc_enc A INNER JOIN Trans_OC_Ti B 
                                   ON a.IdTipoIngresoOC = b.IdTipoIngresoOC
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransOCTI As New clsBeTrans_oc_ti()
                    clsLnTrans_oc_ti.Cargar(BeTransOCTI, lRow)
                    Get_BeTipoDocumento_By_IdOrdenCompraEnc = BeTransOCTI

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function getTicket_Retroactivo(ByVal pIdOrdenCompraEnc As Integer) As DataTable


        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "select no_ticket_tms,tms.Fecha_Ingreso from 
                                    trans_oc_enc oc_enc inner join tms_ticket tms on oc_enc.no_ticket_tms=tms.IdTicket
                                    where oc_enc.Activo =1 and IdOrdenCompraEnc=@pIdOrdenCompraEnc "



            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)

                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try


    End Function

    Public Shared Function Get_IdPropietario_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer) As Integer

        Get_IdPropietario_By_IdOrdenCompraEnc = 0

        Try

            Dim vSQL As String = " SELECT pb.IdPropietario FROM Trans_oc_enc oc 
                                   INNER JOIN propietario_bodega pb on oc.IdPropietarioBodega = pb.IdPropietarioBodega
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdPropietario_By_IdOrdenCompraEnc = IIf(IsDBNull(lDT.Rows(0).Item("IdPropietario")), 0, lDT.Rows(0).Item("IdPropietario"))

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

    Public Shared Function Get_IdBodega_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Integer

        Get_IdBodega_By_IdOrdenCompraEnc = 0

        Try

            Dim vSQL As String = " SELECT idbodega FROM trans_oc_enc
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_IdBodega_By_IdOrdenCompraEnc = IIf(IsDBNull(lDT.Rows(0).Item("idbodega")), 0, lDT.Rows(0).Item("idbodega"))

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdPropietario_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As Integer

        Get_IdPropietario_By_IdOrdenCompraEnc = 0

        Try

            Dim vSQL As String = " SELECT pb.IdPropietario FROM Trans_oc_enc oc 
                                   INNER JOIN propietario_bodega pb on oc.IdPropietarioBodega = pb.IdPropietarioBodega
                                   WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_IdPropietario_By_IdOrdenCompraEnc = IIf(IsDBNull(lDT.Rows(0).Item("IdPropietario")), 0, lDT.Rows(0).Item("IdPropietario"))

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Existe_Documento_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer) As Boolean

        Existe_Documento_By_IdOrdenCompraEnc = False

        Try

            Dim vSQL As String = " SELECT  * FROM Trans_oc_enc 
                                    WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Existe_Documento_By_IdOrdenCompraEnc = True
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

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdBodega(ByVal pIdOrdenCompraEnc As Integer,
                                                                       ByVal pIdBodega As Integer) As clsBeTrans_oc_enc

        Get_Single_By_IdOrdenCompraEnc_And_IdBodega = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_enc 
                                  Where(IdOrdenCompraEnc = @IdOrdenCompraEnc AND IdBodega = @IdBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim BeOcEnc As New clsBeTrans_oc_enc()
                            Cargar(BeOcEnc, lDT.Rows(0))
                            Get_Single_By_IdOrdenCompraEnc_And_IdBodega = BeOcEnc
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, pIdBodega, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Actualizar_PutAway_Registrado(ByVal pIdOrdenCompraEnc As Integer) As Integer

        Actualizar_PutAway_Registrado = 0

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc 
                                  SET PutAway_Registrado=@PutAway_Registrado , Enviado_A_ERP= 1
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@PutAway_Registrado", True)
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        Actualizar_PutAway_Registrado = lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' #CKFK20220308: Validar si la OC tiene registros pendientes de push.
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <returns></returns>
    Public Shared Function Registros_Pendientes_Push(ByVal pIdOrdenCompraEnc As Integer) As Boolean

        Registros_Pendientes_Push = False

        Try

            Dim vSQL As String = "SELECT count(IdTransaccionPush) AS Cant 
                                  FROM i_nav_transacciones_push 
                                  WHERE IdOrdenCompra = @IdOrdenCompra AND Enviado_A_ERP = 0"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdOrdenCompra", pIdOrdenCompraEnc)
                        Dim lValue = lCommand.ExecuteScalar()
                        Registros_Pendientes_Push = (lValue > 0)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_BeTransOcEnc_By_NoDocumento(ByVal pNoDocumento As String) As clsBeTrans_oc_enc

        Get_BeTransOcEnc_By_NoDocumento = Nothing

        Try

            Dim vSQL As String = " SELECT  * FROM Trans_oc_enc 
                                   WHERE No_Documento = @No_Documento "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@No_Documento", pNoDocumento)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_enc()

                            Cargar(Obj, lRow)

                            Get_BeTransOcEnc_By_NoDocumento = Obj

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

    Public Shared Function Actualizar_Estado_Enviado_A_ERP(ByVal pIdOrdenCompraEnc As Integer,
                                                           ByVal pEnviado_A_ERP As Boolean,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As Integer

        Actualizar_Estado_Enviado_A_ERP = 0

        Try

            Dim vSQL As String = "UPDATE trans_oc_enc SET Enviado_A_ERP=@Enviado_A_ERP 
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                Actualizar_Estado_Enviado_A_ERP = lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function
    Public Shared Function Get_BeTicket_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As clsBeTms_ticket

        Get_BeTicket_By_IdOrdenCompraEnc = Nothing

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "select tms.* from 
                                  trans_oc_enc oc_enc inner join tms_ticket tms on oc_enc.no_ticket_tms=tms.IdTicket
                                  where oc_enc.Activo =1 and IdOrdenCompraEnc=@pIdOrdenCompraEnc "



            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)
                lDataAdapter.Fill(lTable)

                If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lTable.Rows(0)
                    Dim BeTMSTicket As New clsBeTms_ticket()

                    clsLnTms_ticket.Cargar(BeTMSTicket, lRow)

                    Get_BeTicket_By_IdOrdenCompraEnc = BeTMSTicket

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try


    End Function

    Public Shared Function Get_Tipo_Documento(ByVal Referencia As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Get_Tipo_Documento = -1

        Try

            Const sp As String = " SELECT IdTipoIngresoOC FROM Trans_oc_enc" &
                                 " Where(Referencia = @Referencia)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", Referencia))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("IdTipoIngresoOC")), -1, dt.Rows(0).Item("IdTipoIngresoOC"))
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub Actualizar_Estado_OC_By_Interface(ByVal pIdOrdenCompraEnc As Integer,
                                                        ByVal IdEstadoOC As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction)

        Try

            Const vSQL As String = "UPDATE trans_oc_enc SET IdEstadoOC=@IdEstadoOC
                                    WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Dim boOC As New clsBeTrans_oc_enc() With {.IdOrdenCompraEnc = pIdOrdenCompraEnc}
            Obtener(boOC, lConnection, lTransaction)

            'Que no esté cerrada y no esté anulada
            If boOC.IdEstadoOC <> clsDataContractDI.tEstadoOC.CERRADA AndAlso boOC.IdEstadoOC <> clsDataContractDI.tEstadoOC.ANULADA Then

                Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    lCommand.Parameters.AddWithValue("@IdEstadoOC", IdEstadoOC)
                    lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                    lCommand.ExecuteNonQuery()
                End Using

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    ''' <summary>
    ''' #EJC202309261356: Se utiliza en la interface de SAP para becofarma, obtiene el objeto de la orden de compra con todo su detalle.
    ''' </summary>
    ''' <param name="pIdOrdenCompra"></param>
    ''' <returns></returns>
    Public Shared Function Get_BeTransOcEnc_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As clsBeTrans_oc_enc

        Get_BeTransOcEnc_By_IdOrdenCompraEnc = Nothing

        Try

            Dim vSQL As String = " SELECT  enc.*,ti.es_devolucion,ti.nombre AS TipoIngreso 
                                    FROM Trans_oc_enc AS enc  
                                    INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC  
                                    WHERE enc.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransOCEnc As New clsBeTrans_oc_enc()

                    Cargar(BeTransOCEnc, lRow)

                    If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                        BeTransOCEnc.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                        clsLnPropietario_bodega.Obtener(BeTransOCEnc.PropietarioBodega, lConnection, lTransaction)
                    End If

                    If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                        BeTransOCEnc.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                        clsLnProveedor_bodega.Obtener(BeTransOCEnc.ProveedorBodega, lConnection, lTransaction)
                    End If

                    If lRow("IdTipoIngresoOC") IsNot DBNull.Value AndAlso lRow("IdTipoIngresoOC") IsNot Nothing Then
                        BeTransOCEnc.IdTipoIngresoOC = CType(lRow("IdTipoIngresoOC"), Integer)
                        BeTransOCEnc.TipoIngreso.Nombre = CType(lRow("TipoIngreso"), String)
                    End If

                    BeTransOCEnc.IsNew = False
                    BeTransOCEnc.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.Existe_Recepcion_No_Finalizada(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOCEnc.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOCEnc.DetalleLotes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOCEnc.ObjPoliza = clsLnTrans_oc_pol.GetSingle(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)
                    BeTransOCEnc.ListaImg = clsLnTrans_oc_imagen.Get_Imagenes_By_IdOrdenCompraEnc(BeTransOCEnc.IdOrdenCompraEnc, lConnection, lTransaction)

                    Return BeTransOCEnc

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' #CKFK+EJC20240217: Duplica un documento de ingreso, retorna el IdOrdenCompraEnc generado.
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <param name="pIdUsuario"></param>
    ''' <returns></returns>
    Public Shared Function Duplicar(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdUsuario As Integer) As Integer

        Dim pBeTransOcEnc As New clsBeTrans_oc_enc
        Dim pListBeTransOcDet As New List(Of clsBeTrans_oc_det)

        Duplicar = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    pBeTransOcEnc = Get_Single_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                   lConnection,
                                                                   lTransaction)

                    pListBeTransOcDet = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                      lConnection,
                                                                                      lTransaction)

                    pBeTransOcEnc.IdOrdenCompraEnc = MaxID(lConnection, lTransaction) + 1
                    pBeTransOcEnc.No_Documento = Genera_Correlativo_OC(pBeTransOcEnc.IdOrdenCompraEnc.ToString)
                    pBeTransOcEnc.IdEstadoOC = 1
                    pBeTransOcEnc.Enviado_A_ERP = False
                    pBeTransOcEnc.Fecha_Creacion = Now
                    pBeTransOcEnc.Fecha_Recepcion = Now
                    pBeTransOcEnc.Fec_Agr = Now
                    pBeTransOcEnc.User_Agr = pIdUsuario
                    pBeTransOcEnc.Fec_Mod = Now
                    pBeTransOcEnc.User_Mod = pIdUsuario
                    pBeTransOcEnc.IsNew = True
                    pBeTransOcEnc.Observacion = "Documento generado como copia de documento base IdOrdenCompraEnc: " & pIdOrdenCompraEnc

                    Insertar(pBeTransOcEnc,
                             lConnection,
                             lTransaction)

                    For Each OCDet In pListBeTransOcDet

                        OCDet.IdOrdenCompraEnc = pBeTransOcEnc.IdOrdenCompraEnc
                        OCDet.Cantidad_recibida = 0
                        OCDet.Fec_agr = Now
                        OCDet.Fec_mod = Now
                        OCDet.User_mod = pIdUsuario
                        OCDet.Fec_agr = Now
                        clsLnTrans_oc_det.Insertar(OCDet,
                                                   lConnection,
                                                   lTransaction)
                    Next


                    lTransaction.Commit()

                    Duplicar = pBeTransOcEnc.IdOrdenCompraEnc

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, pBeTransOcEnc.IdOrdenCompraEnc, pBeTransOcEnc.User_Agr, ex.StackTrace, pBeTransOcEnc.IdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Documento_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As String) As String

        Get_Documento_By_IdOrdenCompraEnc = ""

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = " SELECT No_Documento FROM Trans_oc_enc" &
                                 " Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", pIdOrdenCompraEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("No_Documento")), "", dt.Rows(0).Item("No_Documento"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#GT10072024: listar documentos de ingreso para bodega general para prefacturación.
    Public Shared Function Get_All_By_IdPropietarioBodega_And_IdBodega(ByVal IdPropietarioBodega As Integer,
                                                                       ByVal IdBodega As Integer) As DataTable

        Get_All_By_IdPropietarioBodega_And_IdBodega = Nothing

        Try
            Dim listPolizas As New clsBeTrans_oc_pol

            'Dim vSQL As String = " select IdOrdenCompraEnc,Referencia as numero_orden,Procedencia+' - Doc. Ingreso '+ cast(IdOrdenCompraEnc as varchar) codigo_poliza
            '                              from trans_oc_enc oc_enc inner join trans_oc_estado oc_estado on oc_enc.IdEstadoOC=oc_estado.IdEstadoOC
            '                              where oc_estado.Nombre not in ('ANULADA') and IdPropietarioBodega=@IdPropietarioBodega and oc_enc.IdBodega = @IdBodega "


            '#GT14112024: mejora para mostrar fecha ingreso si tuviera ticket y poliza si tuviera por transferencia

            Dim vSQL As String = " select oc_enc.IdOrdenCompraEnc,Referencia as numero_orden,Procedencia+' -Doc. Ingreso '+ cast(oc_enc.IdOrdenCompraEnc as varchar) codigo_poliza,
		                                  case when oc_enc.no_ticket_tms>0 then tk.Fecha_Ingreso else oc_enc.Fecha_Creacion end as fecha_ingreso
			                       from trans_oc_enc oc_enc inner join trans_oc_estado oc_estado on oc_enc.IdEstadoOC=oc_estado.IdEstadoOC
								          left outer join tms_ticket tk on oc_enc.no_ticket_tms = tk.IdTicket
									      left outer join trans_oc_pol oc_pol on oc_pol.IdOrdenCompraEnc=oc_enc.IdOrdenCompraEnc
                                    where oc_estado.Nombre not in ('ANULADA') and IdPropietarioBodega=@IdPropietarioBodega and oc_enc.IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdPropietarioBodega_And_IdBodega = lDataTable

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

    '#GT11072024: buscar OC para prefacturación
    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_Referencia(ByVal pIdOrdenCompraEnc As Integer, ByVal pReferencia As String) As clsBeTrans_oc_enc


        Get_Single_By_IdOrdenCompraEnc_And_Referencia = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_enc 
                                  Where(IdOrdenCompraEnc = @IdOrdenCompraEnc AND Referencia = @pReferencia)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pReferencia", pReferencia)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim BeOcEnc As New clsBeTrans_oc_enc()
                            Cargar(BeOcEnc, lDT.Rows(0))
                            Get_Single_By_IdOrdenCompraEnc_And_Referencia = BeOcEnc
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Single_By_Referencia(ByVal pReferencia As String, ByVal pIdOrdenCompraEnc As Integer, ByVal pIdBodega As Integer) As clsBeTrans_oc_enc


        Get_Single_By_Referencia = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_enc 
                                  Where( IdOrdenCompraEnc=@pIdOrdenCompraEnc and Referencia = @pReferencia and IdBodega=@pIdBodega )"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pReferencia", pReferencia)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim BeOcEnc As New clsBeTrans_oc_enc()
                            Cargar(BeOcEnc, lDT.Rows(0))
                            Get_Single_By_Referencia = BeOcEnc
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, pIdBodega, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function



    '#GT14082024: actualizar idacuerdo cuando no se ha hecho desde el guardar sino cuando se abre el documento.
    Public Shared Function Actualizar_AcuerdoComercial_By_IdOrdenCompraEnc(ByRef oBeTrans_oc_enc As clsBeTrans_oc_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("trans_oc_enc")
            Upd.Add("idacuerdocomercial", "@idacuerdocomercial", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc")

            Dim sp As String = Upd.SQL()

            '#EJC20191205: Trans_Ref02
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDOCOMERCIAL", oBeTrans_oc_enc.IdAcuerdoComercial))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_enc.User_Mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_enc.Fec_Mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Existe_Devolucion_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As Boolean

        Existe_Devolucion_By_IdPedidoEnc = False

        Try

            Dim vSQL As String = " SELECT IdOrdenCompraEnc FROM Trans_oc_enc 
                                    WHERE NO_DOCUMENTO = @IdPedidoEnc AND IdTipoIngresoOC = @IdTipoIngresoOC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoIngresoOC", clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Por_NC_Anulada)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Existe_Devolucion_By_IdPedidoEnc = True
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

    Public Shared Function Existe_Devolucion_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Devolucion_By_IdPedidoEnc = False

        Try

            Dim vSQL As String = " SELECT IdOrdenCompraEnc FROM Trans_oc_enc 
                                    WHERE NO_DOCUMENTO = @IdPedidoEnc AND IdTipoIngresoOC = @IdTipoIngresoOC"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoIngresoOC", clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Por_NC_Anulada)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Existe_Devolucion_By_IdPedidoEnc = True
                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    '#GT07052025: listar todas las oc's para proceso de importacion hacia portal web

    Public Shared Function GetAll_By_CDC(ByVal pUltimaSincronizacion As Date, ByVal plistaPropietariosBodega As List(Of clsBePropietario_bodega),
                                                                              ByVal listaIngresosPendientes As List(Of Integer),
                                                                              Optional ByVal pConection As SqlConnection = Nothing,
                                                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_oc_enc)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        Dim Es_Transaccion_Remota As Boolean
        GetAll_By_CDC = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM trans_oc_enc WHERE (idEstadoOC=4 and Activo=1) and fec_agr >=@pUltimaSincronizacion "
            'Dim vSQL As String = "SELECT * FROM trans_oc_enc WHERE IdOrdenCompraEnc=12526 "

            If plistaPropietariosBodega IsNot Nothing AndAlso plistaPropietariosBodega.Count > 0 Then
                Dim propietarioIds As String = String.Join(",", plistaPropietariosBodega.Select(Function(p) p.IdPropietarioBodega.ToString()))
                vSQL &= " AND idPropietarioBodega IN (" & propietarioIds & ")"
            End If

            'If plistaPropietariosBodega > 0 Then
            '    vSQL &= " AND idPropietarioBodega = @pPropietario "
            'End If

            If listaIngresosPendientes IsNot Nothing AndAlso listaIngresosPendientes.Count > 0 Then
                Dim IdOrdenCompraIds As String = String.Join(",", listaIngresosPendientes)
                vSQL &= " AND IdOrdenCompraEnc NOT IN (" & IdOrdenCompraIds & ")"
            End If

            Es_Transaccion_Remota = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@pUltimaSincronizacion", pUltimaSincronizacion)

            'If plistaPropietariosBodega > 0 Then
            '    lDTA.SelectCommand.Parameters.AddWithValue("@pPropietario", plistaPropietariosBodega)
            'End If

            Dim lDT As New DataTable()
            lDTA.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                GetAll_By_CDC = New List(Of clsBeTrans_oc_enc)()
                For Each row As DataRow In lDT.Rows
                    Dim BeOcEnc As New clsBeTrans_oc_enc()
                    Cargar(BeOcEnc, row)
                    BeOcEnc.PropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(BeOcEnc.IdPropietarioBodega, pConection, pTransaction)
                    GetAll_By_CDC.Add(BeOcEnc)
                Next

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso lTransaction IsNot Nothing Then
                lTransaction.Rollback()
            End If
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
                If lTransaction IsNot Nothing Then lTransaction.Dispose()
                If lConnection IsNot Nothing Then lConnection.Dispose()
            End If
        End Try

    End Function



    'Public Shared Function GetAll_By_CDC(ByVal pUltimaSincronizacion As Date, ByVal ListPropietariosBodega As List(Of Integer),
    '                                                                          ByVal listaIngresosPendientes As List(Of Integer),
    '                                                                          Optional ByVal pConection As SqlConnection = Nothing,
    '                                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_oc_enc)

    '    Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing
    '    Dim lDTA As New SqlDataAdapter
    '    Dim Es_Transaccion_Remota As Boolean
    '    GetAll_By_CDC = Nothing

    '    Try

    '        Dim vSQL As String = "SELECT * FROM trans_oc_enc WHERE (idEstadoOC=4 and Activo=1) and fec_agr >=@pUltimaSincronizacion "

    '        If ListPropietariosBodega IsNot Nothing AndAlso ListPropietariosBodega.Count > 0 Then
    '            Dim propietarioIds As String = String.Join(",", ListPropietariosBodega.Select(Function(p) p.IdPropietarioBodega.ToString()))
    '            vSQL &= " AND idPropietarioBodega IN (" & propietarioIds & ")"
    '        End If

    '        If listaIngresosPendientes IsNot Nothing AndAlso listaIngresosPendientes.Count > 0 Then
    '            Dim IdOrdenCompraIds As String = String.Join(",", listaIngresosPendientes)
    '            vSQL &= " AND IdOrdenCompraEnc NOT IN (" & IdOrdenCompraIds & ")"
    '        End If


    '        Es_Transaccion_Remota = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

    '        If Es_Transaccion_Remota Then
    '            lDTA = New SqlDataAdapter(vSQL, pConection)
    '            lDTA.SelectCommand.Transaction = pTransaction
    '        Else
    '            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
    '            lDTA = New SqlDataAdapter(vSQL, lConnection)
    '        End If

    '        lDTA.SelectCommand.CommandType = CommandType.Text
    '        lDTA.SelectCommand.Parameters.AddWithValue("@pUltimaSincronizacion", pUltimaSincronizacion)
    '        Dim lDT As New DataTable()
    '        lDTA.Fill(lDT)

    '        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
    '            GetAll_By_CDC = New List(Of clsBeTrans_oc_enc)()
    '            For Each row As DataRow In lDT.Rows
    '                Dim BeOcEnc As New clsBeTrans_oc_enc()
    '                Cargar(BeOcEnc, row)
    '                GetAll_By_CDC.Add(BeOcEnc)
    '            Next

    '        End If

    '        If Not Es_Transaccion_Remota Then lTransaction.Commit()

    '    Catch ex As Exception
    '        If Not Es_Transaccion_Remota AndAlso lTransaction IsNot Nothing Then
    '            lTransaction.Rollback()
    '        End If
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '    Finally
    '        If Not Es_Transaccion_Remota Then
    '            If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
    '            If lTransaction IsNot Nothing Then lTransaction.Dispose()
    '            If lConnection IsNot Nothing Then lConnection.Dispose()
    '        End If
    '    End Try

    'End Function

    Public Shared Function Eliminar_OC(ByVal pOrdenCompraEnc As clsBeTrans_oc_enc,
                                       ByVal pUsuario As clsBeUsuario) As Boolean


        Eliminar_OC = False

        Dim lListRecepciones As New List(Of Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Try
                lListRecepciones = clsLnTrans_re_oc.Get_IdRecepcionEnc_By_IdOrdenCompraEnc(pOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction).ToList
                If Not lListRecepciones Is Nothing Then
                    If lListRecepciones.Count > 0 Then
                        Throw New Exception("No se puede eliminar un documento con recepciones asociadas")
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            'Eliminamos la póliza asociada
            If pOrdenCompraEnc.ObjPoliza IsNot Nothing Then
                clsLnTrans_oc_pol.Eliminar_Poliza_By_IdOrdenCompra(pOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction)
            End If

            ' Eliminamos la orden de compra correspondiente
            Eliminar_Encabezado_Detalle(pOrdenCompraEnc.IdOrdenCompraEnc,
                                        lConnection,
                                        lTransaction)

            If pOrdenCompraEnc.Referencia <> "" Then

                'Eliminamos el detalle en la temporal
                clsLnI_nav_ped_compra_det.Eliminar_By_NoEnc(pOrdenCompraEnc.Referencia,
                                                            lConnection,
                                                            lTransaction)

                'Eliminamos el encabezado en la temporal
                clsLnI_nav_ped_compra_enc.Delete_By_NoEnc(pOrdenCompraEnc.Referencia,
                                                          lConnection,
                                                          lTransaction)

            End If

            lTransaction.Commit()

            '#MECR06102025: Se agrego bitacora de logs para OC
            Dim BeLogErrorWMS As New clsBeLog_error_wms_oc
            BeLogErrorWMS.IdEmpresa = pUsuario.IdEmpresa
            BeLogErrorWMS.IdBodega = pOrdenCompraEnc.IdBodega
            BeLogErrorWMS.Fecha = Now
            BeLogErrorWMS.MensajeError = "OC_DEL: Se eliminó el documento de ingreso: " & pOrdenCompraEnc.IdOrdenCompraEnc.ToString() & " con referencia: " & pOrdenCompraEnc.Referencia
            BeLogErrorWMS.IdUsuarioAgr = pUsuario.IdUsuario
            BeLogErrorWMS.IdOrdenCompraEnc = pOrdenCompraEnc.IdOrdenCompraEnc

            clsLnLog_error_wms_oc.Insertar(BeLogErrorWMS, lConnection, lTransaction)

            Eliminar_OC = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Eliminar_Encabezado_Detalle(ByVal pIdOrdenCompraEnc As Integer,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "DELETE FROM trans_oc_imagen Where(IdOrdenCompraEnc = @IdOrdenCompraEnc); 
                                  DELETE FROM trans_oc_servicios Where(IdOrdenCompraEnc = @IdOrdenCompraEnc); 
                                  DELETE FROM trans_oc_det_lote Where(IdOrdenCompraEnc = @IdOrdenCompraEnc); 
                                  DELETE FROM trans_oc_det Where(IdOrdenCompraEnc = @IdOrdenCompraEnc); 
                                  DELETE FROM Trans_oc_enc Where(IdOrdenCompraEnc = @IdOrdenCompraEnc);"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", pIdOrdenCompraEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception("No se puedo eliminar la orden de compra " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Usuario_Defecto_By_IdOrdenCompraEnc(ByVal pIdOrdenCompra As Integer) As String

        Get_Usuario_Defecto_By_IdOrdenCompraEnc = ""

        Try

            Dim vSQL As String = " select CONCAT(usuario.nombres,'',usuario.apellidos) as usuario 
                                   from trans_oc_enc 
                                   join usuario on usuario.IdUsuario = trans_oc_enc.User_Agr 
                                    Where IdOrdenCompraEnc=  @IdOrdenCompraEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_Usuario_Defecto_By_IdOrdenCompraEnc = lRow.Item("usuario").ToString()

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
End Class