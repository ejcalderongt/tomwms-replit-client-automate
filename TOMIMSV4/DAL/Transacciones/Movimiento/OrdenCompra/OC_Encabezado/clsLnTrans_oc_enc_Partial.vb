Imports System.Reflection
Imports System.Data.SqlClient
Imports AppGlobal.TOM

Partial Public Class clsLnTrans_oc_enc

    Public Shared Function GetSingle(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_enc

        'Dim clsLnProveedor_bodega As New clsLnProveedor_bodega


        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Acceso a los datos.
                Using lDTA As New SqlDataAdapter("sELECT  enc.*,ti.es_devolucion,ti.nombre AS TipoIngreso FROM Trans_oc_enc AS enc  " _
                                                         & "INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC " _
                                                         & "WHERE enc.IdOrdenCompraEnc=@IdOrdenCompraEnc", lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeTrans_oc_enc()

                        Cargar(Obj, lRow)

                        If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                            Obj.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                            clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega)
                        End If

                        If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                            Obj.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                            clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega)
                        End If

                        If lRow("IdTipoIngresoOC") IsNot DBNull.Value AndAlso lRow("IdTipoIngresoOC") IsNot Nothing Then
                            Obj.IdTipoIngresoOC = CType(lRow("IdTipoIngresoOC"), Integer)
                            Obj.TipoIngreso = CType(lRow("TipoIngreso"), String)
                        End If

                        Obj.IsNew = False

                        Obj.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.ExisteRecepcionNoFinalizada(Obj.IdOrdenCompraEnc)

                        Obj.listaD = clsLnTrans_oc_det.GetByOrdenCompra(Obj.IdOrdenCompraEnc)
                        Obj.ObjP = clsLnTrans_oc_pol.GetSingle(Obj.IdOrdenCompraEnc)
                        Obj.listaI = clsLnTrans_oc_imagen.GetByOrdenCompra(Obj.IdOrdenCompraEnc)

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function GetOrdenCompraByPropietario(ByVal pIdOrdenCompra As Integer, ByVal pIdPropietarioBodega As Integer) As clsBeTrans_oc_enc

        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Acceso a los datos.
                Using lDTA As New SqlDataAdapter("SELECT TOP 1 enc.*,ti.es_devolucion FROM trans_oc_enc AS enc " _
                                                         & "INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC " _
                                                         & "WHERE enc.IdOrdenCompraEnc=@IdOrdenCompraEnc AND enc.IdPropietarioBodega=@IdPropietarioBodega", lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text
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
                            clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega)
                        End If

                        If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                            Obj.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                            clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega)
                        End If

                        Obj.IsNew = False

                        Obj.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.ExisteRecepcionNoFinalizada(Obj.IdOrdenCompraEnc)

                        Obj.listaD = clsLnTrans_oc_det.GetByOrdenCompra(Obj.IdOrdenCompraEnc)
                        Obj.ObjP = clsLnTrans_oc_pol.GetSingle(Obj.IdOrdenCompraEnc)
                        Obj.listaI = clsLnTrans_oc_imagen.GetByOrdenCompra(Obj.IdOrdenCompraEnc)

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function GetOrdenCompra(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_enc

        Dim clsLnProveedor_bodega As New clsLnProveedor_bodega


        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Acceso a los datos.
                Using lDTA As New SqlDataAdapter("SELECT  enc.*,b.IdBodega,ti.es_devolucion,ti.nombre AS TipoIngreso FROM Trans_oc_enc AS enc " _
                                                         & "INNER JOIN propietario_bodega AS pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega " _
                                                         & "INNER JOIN bodega AS b ON pb.IdBodega = b.IdBodega " _
                                                         & "INNER JOIN trans_oc_ti AS ti ON enc.IdTipoIngresoOC = ti.IdtipoIngresoOC " _
                                                         & "WHERE enc.IdOrdenCompraEnc=@IdOrdenCompraEnc", lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeTrans_oc_enc()

                        Cargar(Obj, lRow)

                        Obj.IdBodega = CType(lRow("IdBodega"), Integer)

                        If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                            Obj.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                            clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega)
                        End If

                        If lRow("IdProveedorBodega") IsNot DBNull.Value AndAlso lRow("IdProveedorBodega") IsNot Nothing Then
                            Obj.ProveedorBodega.IdAsignacion = CType(lRow("IdProveedorBodega"), Integer)
                            clsLnProveedor_bodega.Obtener(Obj.ProveedorBodega)
                        End If

                        If lRow("IdTipoIngresoOC") IsNot DBNull.Value AndAlso lRow("IdTipoIngresoOC") IsNot Nothing Then
                            Obj.IdTipoIngresoOC = CType(lRow("IdTipoIngresoOC"), Integer)
                            Obj.TipoIngreso = CType(lRow("TipoIngreso"), String)
                        End If

                        Obj.IsNew = False

                        Obj.ExisteRecepcionNoFinalizada = clsLnTrans_re_enc.ExisteRecepcionNoFinalizada(Obj.IdOrdenCompraEnc)

                        Obj.listaD = clsLnTrans_oc_det.GetByOrdenCompra(Obj.IdOrdenCompraEnc)
                        Obj.ObjP = clsLnTrans_oc_pol.GetSingle(Obj.IdOrdenCompraEnc)
                        Obj.listaI = clsLnTrans_oc_imagen.GetByOrdenCompra(Obj.IdOrdenCompraEnc)

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0
            'Validacion y estandarizacion de los datos
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                'Acceso a los datos.
                Using lCommand As New SqlCommand("SELECT ISNULL(Max(IdOrdenCompraEnc),0) FROM trans_oc_enc", lConnection)
                    lCommand.CommandType = CommandType.Text
                    lCommand.CommandTimeout = 200
                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, Optional ByVal pIdBodega As Integer = 0, Optional ByVal pIdPropietario As Integer = 0) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim lSQL As String = "SELECT * FROM VW_OrdenCompra WHERE 1 > 0 "

            If pActivo = True Then
                lSQL += " And Activo=1"
            Else
                lSQL += " AND Activo=0"
            End If

            lSQL += " AND cast(Fecha AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)

            If pIdBodega <> 0 Then
                lSQL += String.Format(" AND IdBodega={0}", pIdBodega)
            End If

            If pIdPropietario <> 0 Then
                lSQL += String.Format(" AND IdPropietario={0}", pIdPropietario)
            End If

            If pIdBodega <> 0 AndAlso pIdPropietario <> 0 Then
                lSQL += "AND Estado IN ('NUEVA','BACK ORDER')"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(lSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function GetImpresionByOC(ByVal pIdOrdenCompraEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try
            Dim lSQl As String = String.Format("SELECT * FROM VW_OrdenCompraPreIngreso WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(lSQl, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creada por Erik Calderón
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    Public Shared Sub Anular(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdMotivoAnulacionBodega As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction)

        'El Estado 5 es Anulado para las Ordenes de Compra
        'Validacion y estandarizacion de los datos

        Try

            'Acceso a los datos.
            Using lCommand As New SqlCommand("UPDATE trans_oc_enc " &
                                                       " SET IdEstadoOC=@IdEstadoOC, " &
                                                       " IdMotivoAnulacionBodega = @IdMotivoAnulacionBodega " &
                                                       " WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc", pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdEstadoOC", 5)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lCommand.Parameters.AddWithValue("@IdMotivoAnulacionBodega", pIdMotivoAnulacionBodega)

                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
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

    Public Shared Sub ActualizarEstado(ByVal pEstadoOC As pEstadoOC, ByVal pIdOrdenCompraEnc As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction)

        'El Estado 6 es Back Order para las Ordenes de Compra
        'Validacion y estandarizacion de los datos
        'Acceso a los datos.

        Try

            Using lCommand As New SqlCommand("UPDATE trans_oc_enc SET IdEstadoOC=@IdEstadoOC WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc", pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdEstadoOC", pEstadoOC)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Sub

    'Public Shared Sub ActualizarEstado(ByVal pIdOrdenCompraEnc As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction)

    '    'El Estado 6 es Back Order para las Ordenes de Compra
    '    'Validacion y estandarizacion de los datos
    '    'Acceso a los datos.

    '    Try

    '        Using lCommand As New SqlCommand("UPDATE trans_oc_enc SET IdEstadoOC=@IdEstadoOC WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc", pConnection)

    '            lCommand.Transaction = pTransaction
    '            lCommand.CommandType = CommandType.Text
    '            lCommand.Parameters.AddWithValue("@IdEstadoOC", 6)
    '            lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

    '            lCommand.ExecuteNonQuery()

    '        End Using

    '    Catch ex As Exception
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
    '    End Try

    'End Sub

    Public Shared Function CantidadTransito(ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As Double

        Try

            Dim lCant As Double = 0.0
            'Validacion y estandarizacion de los datos
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                'Acceso a los datos.
                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(SUM(Cantidad),0) FROM trans_oc_enc AS enc INNER JOIN trans_oc_det AS det ON enc.IdOrdenCompraEnc = det.IdOrdenCompraEnc INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto INNER JOIN producto_presentacion AS pp ON p.IdProducto = pp.IdProducto WHERE enc.IdEstadoOc=1 AND p.IdProducto={0} AND pp.IdPresentacion={1}", pIdProducto, pIdPresentacion), lConnection)
                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lCant = CDbl(lReturnValue)
                    End If

                End Using

            End Using

            Return lCant

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function ActualizarDatos(ByVal pObjE As clsBeTrans_oc_enc,
                                    ByVal pListObjTD As List(Of clsBeTrans_oc_det),
                                    ByVal pObjP As clsBeTrans_oc_pol,
                                    ByVal pListObjI As List(Of clsBeTrans_oc_imagen),
                                    ByVal pListObjP As List(Of clsBeProducto)) As Integer

        'Dim ObjE As New clsLnTrans_oc_enc()
        'Dim ObjD As New clsLnTrans_oc_det()
        Dim ObjP As New clsLnTrans_oc_pol()
        Dim ObjI As New clsLnTrans_oc_imagen()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        ActualizarDatos = 0

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction()

            If pObjE.IsNew Then
                pObjE.IdOrdenCompraEnc = MaxID()
                pObjE.No_Documento = Genera_Correlativo_OC(pObjE.IdOrdenCompraEnc.ToString)
                Insertar(pObjE, lConnection, lTransaction)
            Else
                Actualizar(pObjE, lConnection, lTransaction)
            End If

            If pObjP.IsNew Then
                pObjP.IdOrdenCompraPol = clsLnTrans_oc_pol.MaxID(pObjE.IdOrdenCompraEnc)
                pObjP.IdOrdenCompraEnc = pObjE.IdOrdenCompraEnc
                ObjP.Insertar(pObjP, lConnection, lTransaction)
            Else
                ObjP.Actualizar(pObjP, lConnection, lTransaction)
            End If

            Dim lMax As Integer = clsLnTrans_oc_det.MaxID(pObjE.IdOrdenCompraEnc)
            For Each Obj As clsBeTrans_oc_det In pListObjTD
                If Obj.IsNew Then
                    lMax += 1
                    Obj.IdOrdenCompraDet = lMax
                    Obj.IdOrdenCompraEnc = pObjE.IdOrdenCompraEnc
                    clsLnTrans_oc_det.Insertar(Obj, lConnection, lTransaction)
                Else
                    clsLnTrans_oc_det.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            For Each Obj As clsBeTrans_oc_imagen In pListObjI
                If Obj.IsNew Then
                    Obj.IdOrdenCompraEnc = pObjE.IdOrdenCompraEnc
                    ObjI.Insertar(Obj, lConnection, lTransaction)
                Else
                    ObjI.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            For Each Obj As clsBeProducto In pListObjP
                If Obj.IdProducto <> Nothing AndAlso Obj.IdProducto <> 0 Then
                    clsLnProducto.UpdateCosto(lConnection, lTransaction, Obj)
                End If
            Next

            lTransaction.Commit()
            lConnection.Close()
            Return True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing Then lConnection.Close()
            Throw New Exception(ex.Message)
        Finally
            ActualizarDatos = pObjE.IdOrdenCompraEnc
        End Try
        '
    End Function

    Public Shared Function Genera_Correlativo_OC() As String

        Genera_Correlativo_OC = Right("000000" & 1,7)

        Try

            Dim MaxID As String = clsLnTrans_oc_enc.MaxID.ToString

            Genera_Correlativo_OC  = Right("000000" & MaxID,7)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Genera_Correlativo_OC(ByVal pIdOrdenCompraEnc As String) As String

        Genera_Correlativo_OC = "OC"

        Try
            
            Genera_Correlativo_OC = Right("000000" & pIdOrdenCompraEnc,7)
            
            Return Genera_Correlativo_OC

        Catch ex As Exception
            Return "OC0"
        End Try

    End Function    

    Public Shared Function Anula(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdMotivoAnulacionBodega As Integer) As Boolean

        Anula = False
        Dim lListRecepciones As New List(Of Integer)

        Try
            lListRecepciones = clsLnTrans_re_oc.GetIdRecepcionByOrdenCompra(pIdOrdenCompraEnc).ToList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction()

            ' Anulamos todas las recepciones ligadas a la orden de compra
            For Each recepcion As Integer In lListRecepciones
                clsLnTrans_re_enc.Anular(recepcion, lConnection, lTransaction)
            Next

            ' Anulamos la orden de compra correspondiente
            Anular(pIdOrdenCompraEnc, pIdMotivoAnulacionBodega, lConnection, lTransaction)

            lTransaction.Commit()
            lConnection.Close()

            Anula = True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing Then lConnection.Close()
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function IniciaRecepcionOC(ByRef oBeTrans_oc_enc As clsBeTrans_oc_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_oc_enc")
            Upd.Add("idestadooc", "@idestadooc", "F")
            Upd.Add("user_mod", "@user_mod", "F")
            Upd.Add("fec_mod", "@fec_mod", "F")
            Upd.Add("fecha_recepcion", "@fecha_recepcion", "F")
            Upd.Add("hora_inicio_recepcion", "@hora_inicio_recepcion", "F")
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc")

            Dim sp As String = Upd.SQL()

            Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            cmd.CommandType = CommandType.Text

            If EsTransaccional Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOOC", IIf(oBeTrans_oc_enc.IdEstadoOC = 0, DBNull.Value, oBeTrans_oc_enc.IdEstadoOC)))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_enc.User_Mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_enc.Fec_Mod))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", IIf(oBeTrans_oc_enc.Fecha_Recepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.Fecha_Recepcion)))
            cmd.Parameters.Add(New SqlParameter("@HORA_INICIO_RECEPCION", IIf(oBeTrans_oc_enc.Hora_Inicio_Recepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.Hora_Inicio_Recepcion)))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
            cmd.Dispose()
        End Try

    End Function

End Class
