Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Partial Public Class clsLnTarea_hh

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdTareahh),0) FROM tarea_hh"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax + 1

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByRef pConnection As SqlConnection, pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdTareahh),0) FROM tarea_hh"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                    lMax = CInt(lReturnValue)

                End If

            End Using

            Return lMax + 1

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdTareaHH As Integer) As clsBeTarea_hh

        Dim Obj As New clsBeTarea_hh

        Try

            Dim vSQL As String = "SELECT * FROM tarea_hh WHERE IdTareahh = @IdTareahh"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTareahh", pIdTareaHH)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Cargar(Obj, lRow)

                            Obj.IsNew = False

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Obj

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdTipoTarea As Integer,
                                     ByVal pIdTransaccion As Integer,
                                     ByRef lTransaction As SqlTransaction,
                                     ByRef lConnection As SqlConnection) As clsBeTarea_hh

        GetSingle = Nothing

        Dim Obj As New clsBeTarea_hh

        Try

            Dim vSQL As String = "SELECT * FROM tarea_hh WHERE IdTipoTarea = @IdTipoTarea AND IdTransaccion = @IdTransaccion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTarea", pIdTipoTarea)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTransaccion", pIdTransaccion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Cargar(Obj, lRow)
                    Obj.IsNew = False
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBeTarea_hh)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)


            Dim lReturnList As New List(Of clsBeTarea_hh)
            Const sp As String = "SELECT * FROM Tarea_hh Where IdBodega = @IdBodega"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            dad.Fill(dt)

            Dim vBeTarea_hh As New clsBeTarea_hh

            For Each dr As DataRow In dt.Rows

                vBeTarea_hh = New clsBeTarea_hh
                Cargar(vBeTarea_hh, dr)
                lReturnList.Add(vBeTarea_hh)

            Next

            lTransaction.Commit()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdTareaHH As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM tarea_hh WHERE IdTareahh=@IdTareahh"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdTareahh", pIdTareaHH)

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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Tareas_By_IdBodega(ByVal IdBodega As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            '#EJC20180726: Agregué ordenamiento, tarea mas reciente al inicio, mas antigua al fin de la lista.
            Dim vSQL As String = "SELECT * FROM vw_tareas_activas_hh WHERE IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Tareas_By_IdBodega(ByVal IdBodega As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            '#EJC20180726: Agregué ordenamiento, tarea mas reciente al inicio, mas antigua al fin de la lista.
            Dim vSQL As String = "SELECT * FROM vw_tareas_activas_hh WHERE IdBodega = @IdBodega "

            vSQL += " AND cast(Inicio AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Async Function Get_Lista_Tareas_By_IdBodega_Async(ByVal IdBodega As Integer,
                                                                    ByVal pFechaDel As Date,
                                                                    ByVal pFechaAl As Date,
                                                                    ByVal lConnection As SqlConnection,
                                                                    ByVal lTransaction As SqlTransaction) As Task(Of DataTable)

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM vw_tareas_activas_hh WHERE IdBodega = @IdBodega "
            vSQL += " AND cast(Inicio AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
               " AND " & FormatoFechas.fFecha(pFechaAl)

            Using sqlCommand As New SqlCommand(vSQL, lConnection)
                sqlCommand.CommandType = CommandType.Text
                sqlCommand.Transaction = lTransaction
                sqlCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                If lConnection.State <> ConnectionState.Open Then
                    Await lConnection.OpenAsync()
                End If

                Using reader As SqlDataReader = Await sqlCommand.ExecuteReaderAsync()
                    lTable.Load(reader)
                End Using

            End Using

            Return lTable

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_Lista_Tareas_By_IdBodega(ByVal IdBodega As Integer,
                                                        ByVal pFechaDel As Date,
                                                        ByVal pFechaAl As Date,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            '#EJC20180726: Agregué ordenamiento, tarea mas reciente al inicio, mas antigua al fin de la lista.
            Dim vSQL As String = "SELECT * FROM vw_tareas_activas_hh WHERE IdBodega = @IdBodega "

            vSQL += " AND cast(Inicio AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDataAdapter.Fill(lTable)
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try


    End Function
    Public Shared Function Get_Recepciones_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBeTrans_re_enc)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim lReturnList As New List(Of clsBeTrans_re_enc)

                    Const sp As String = " SELECT idtransaccion FROM Tarea_hh Where IdBodega = @IdBodega " &
                                 " AND IdTipoTarea = 1 " &
                                 " AND IdEstado In (1,2)"


                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

                    dad.Fill(dt)

                    Dim BeReEnc As New clsBeTrans_re_enc

                    For Each dr As DataRow In dt.Rows

                        BeReEnc = New clsBeTrans_re_enc
                        BeReEnc.IdRecepcionEnc = dr("IdTransaccion")
                        'clsLnTrans_re_enc.Obtener(BeReEnc)
                        BeReEnc = clsLnTrans_re_enc.Get_Single_By_IdREcepcionEnc(BeReEnc.IdRecepcionEnc, lConnection, lTransaction)
                        lReturnList.Add(BeReEnc)

                    Next

                    cmd.Dispose()

                    Return lReturnList

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("Tarea_hh_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Recepciones_By_TipoTarea(ByVal TipoTrans As String) As List(Of clsBeTareasIngresoHH)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Recepciones_By_TipoTarea = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = " SELECT trans_re_enc.IdRecepcionEnc AS N, " &
                " propietario_bodega.IdPropietarioBodega, propietario_bodega.IdPropietario, " &
                " propietarios.nombre_comercial AS Propietario, proveedor.IdProveedor,  proveedor.nombre AS Proveedor, " &
                " trans_oc_enc.No_Documento,  " &
                " motivo_devolucion.Nombre AS MotivoDevolucion, trans_oc_ti.Nombre AS Tipo, trans_oc_enc.Referencia  " &
                " FROM  trans_re_enc INNER JOIN " &
                " propietario_bodega ON trans_re_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega INNER JOIN " &
                " propietarios ON propietario_bodega.IdPropietario = propietarios.IdPropietario INNER JOIN " &
                " bodega_muelles ON trans_re_enc.IdMuelle = bodega_muelles.IdMuelle INNER JOIN " &
                " trans_re_oc ON trans_re_enc.IdRecepcionEnc = trans_re_oc.IdRecepcionEnc INNER JOIN " &
                " trans_oc_enc ON propietario_bodega.IdPropietarioBodega = trans_oc_enc.IdPropietarioBodega AND  " &
                " trans_re_oc.IdOrdenCompraEnc = trans_oc_enc.IdOrdenCompraEnc INNER JOIN " &
                " proveedor ON propietarios.IdPropietario = proveedor.IdPropietario AND trans_oc_enc.IdProveedor = proveedor.IdProveedor INNER JOIN " &
                " trans_oc_ti ON trans_oc_enc.IdTipoIngresoOC = trans_oc_ti.IdTipoIngresoOC INNER JOIN " &
                " trans_re_tr ON trans_re_enc.IdTipoTransaccion = trans_re_tr.IdTipoTransaccion LEFT JOIN " &
                " motivo_devolucion ON propietarios.IdPropietario = motivo_devolucion.IdPropietario AND  " &
                " trans_oc_enc.IdMotivoDevolucion = motivo_devolucion.IdMotivoDevolucion " &
                " Where(trans_re_enc.IdTipoTransaccion= @TipoTrans)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@TipoTrans", TipoTrans))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim Rec As clsBeTareasIngresoHH
            Dim lRecs As New List(Of clsBeTareasIngresoHH)

            For Each dr As DataRow In dt.Rows

                Rec = New clsBeTareasIngresoHH
                Rec.IdRecepcionEnc = dr("N")
                Rec.IdPropietarioBodega = dr("IdPropietarioBodega")
                Rec.IdPropietario = dr("IdPropietario")
                Rec.NombrePropietario = dr("Propietario")
                Rec.IdProveedor = dr("IdProveedor")
                Rec.NombreProveedor = dr("Proveedor")
                Rec.NoDocumentoOc = dr("No_Documento")
                Rec.NombreMotivoDevolucion = IIf(IsDBNull(dr("MotivoDevolucion")), "", dr("MotivoDevolucion"))
                Rec.NombreTipoIngresoOC = dr("Tipo")
                Rec.NoReferenciaOC = dr("Referencia")

                lRecs.Add(Rec)

            Next

            lTransaction.Commit()

            dt.Dispose()

            Return lRecs

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Tiempo_Medio_Tarea_Ingreso_Minutos(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        '#EJC20190123: La experiencia me dice que mas o menos eso se tardan en promedio en un ingreso, por eso le vamos a dejar por defecto una hora.
        Get_Tiempo_Medio_Tarea_Ingreso_Minutos = 60

        Try

            Dim sp As String = " select AVG(DATEDIFF(MINUTE,fechainicio,fechafin)) as minutos from tarea_hh where IdTipoTarea = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("minutos")), 60, dt.Rows(0).Item("minutos"))
            End If

            dt.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllRecepciones() As List(Of clsBeTareasIngresoHH)

        GetAllRecepciones = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = " SELECT trans_re_enc.IdRecepcionEnc AS N, " &
                " propietario_bodega.IdPropietarioBodega, propietario_bodega.IdPropietario, " &
                " propietarios.nombre_comercial AS Propietario, proveedor.IdProveedor,  proveedor.nombre AS Proveedor, " &
                " trans_oc_enc.No_Documento, " &
                " motivo_devolucion.Nombre AS MotivoDevolucion, trans_oc_ti.Nombre AS Tipo, trans_oc_enc.Referencia,  " &
                " trans_oc_enc.IdOrdenCompraEnc " &
                " FROM  trans_re_enc INNER JOIN " &
                " propietario_bodega ON trans_re_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega INNER JOIN " &
                " propietarios ON propietario_bodega.IdPropietario = propietarios.IdPropietario INNER JOIN " &
                " bodega_muelles ON trans_re_enc.IdMuelle = bodega_muelles.IdMuelle INNER JOIN " &
                " trans_re_oc ON trans_re_enc.IdRecepcionEnc = trans_re_oc.IdRecepcionEnc INNER JOIN " &
                " trans_oc_enc ON propietario_bodega.IdPropietarioBodega = trans_oc_enc.IdPropietarioBodega AND  " &
                " trans_re_oc.IdOrdenCompraEnc = trans_oc_enc.IdOrdenCompraEnc " &
                " INNER JOIN proveedor_bodega ON proveedor_bodega.IdAsignacion = trans_oc_enc.IdProveedorBodega " &
                " INNER JOIN proveedor ON propietarios.IdPropietario = proveedor.IdPropietario AND proveedor_bodega.IdProveedor = proveedor.IdProveedor INNER JOIN " &
                " trans_oc_ti ON trans_oc_enc.IdTipoIngresoOC = trans_oc_ti.IdTipoIngresoOC INNER JOIN " &
                " trans_re_tr ON trans_re_enc.IdTipoTransaccion = trans_re_tr.IdTipoTransaccion LEFT JOIN " &
                " motivo_devolucion ON propietarios.IdPropietario = motivo_devolucion.IdPropietario AND  " &
                " trans_oc_enc.IdMotivoDevolucion = motivo_devolucion.IdMotivoDevolucion "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Dim Rec As clsBeTareasIngresoHH
            Dim lRecs As New List(Of clsBeTareasIngresoHH)

            For Each dr As DataRow In dt.Rows

                Rec = New clsBeTareasIngresoHH
                Rec.IdRecepcionEnc = dr("N")
                Rec.IdPropietarioBodega = dr("IdPropietarioBodega")
                Rec.IdPropietario = dr("IdPropietario")
                Rec.NombrePropietario = dr("Propietario")
                Rec.IdProveedor = dr("IdProveedor")
                Rec.NombreProveedor = dr("Proveedor")
                Rec.NoDocumentoOc = dr("No_Documento")
                Rec.NombreMotivoDevolucion = IIf(IsDBNull(dr("MotivoDevolucion")), "", dr("MotivoDevolucion"))
                Rec.NombreTipoIngresoOC = dr("Tipo")
                Rec.NoReferenciaOC = dr("Referencia")
                Rec.IdOrderCompraEnc = dr("IdOrdenCompraEnc")
                lRecs.Add(Rec)

            Next

            lTransaction.Commit()

            dt.Dispose()

            Return lRecs

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Count_Recepciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                    ByVal pIdOperadorBodega As Integer,
                                                                    Optional pConnection As SqlConnection = Nothing,
                                                                    Optional pTransaction As SqlTransaction = Nothing) As Integer




        Try
            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand
            Dim lReturnValue As Integer
            'Dim lDataAdapter As New SqlDataAdapter

            Dim vSQL As String = "  SELECT COUNT(IdOrdenCompraEnc) 
                                    FROM VW_Recepcion_For_HH_By_IdBodega_By_Operador 
                                    WHERE IdBodega = @IdBodega AND IdOperadorBodega = @IdOperadorBodega"


            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            lCommand.Parameters.AddWithValue("@IdOperadorBodega", pIdOperadorBodega)

            lReturnValue = lCommand.ExecuteScalar()

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lReturnValue

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IDs_Recepciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                  ByVal pIdOperadorBodega As Integer,
                                                                  Optional pConnection As SqlConnection = Nothing,
                                                                  Optional pTransaction As SqlTransaction = Nothing) As List(Of Integer)


        Dim lDataAdapter As New SqlDataAdapter
        Dim id As Integer
        Dim lRecs As New List(Of Integer)

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand


            Dim vSQL As String = " SELECT N 
                                   FROM VW_Recepcion_For_HH_By_IdBodega_By_Operador 
                                    WHERE IdBodega = @IdBodega AND IdOperadorBodega = @IdOperadorBodega"


            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If


            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            lCommand.Parameters.AddWithValue("@IdOperadorBodega", pIdOperadorBodega)

            lDataAdapter = New SqlDataAdapter(lCommand)
            Dim dt As New DataTable
            lDataAdapter.Fill(dt)

            For Each dr As DataRow In dt.Rows
                id = dr.Item(0)
                lRecs.Add(id)
            Next

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lRecs

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Picking_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                ByVal pIdOperadorBodega As Integer,
                                                                Optional pConnection As SqlConnection = Nothing,
                                                                Optional pTransaction As SqlTransaction = Nothing) As Integer


        Dim lReturnValue As Integer
        Dim lDataAdapter As New SqlDataAdapter

        Try
            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand

            Dim vSQL As String = "SELECT COUNT(IdPickingEnc) AS CANT FROM VW_Tareas_Picking_HH 
                                  WHERE ((estado = 'Nuevo' OR estado = 'Pendiente') 
                                  AND (activo = 1) 
                                  AND IdBodega = @IdBodega 
                                  AND IdOperadorBodega = @IdOperadorBodega )"


            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            lCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))

            lReturnValue = lCommand.ExecuteScalar()

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lReturnValue

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IDs_Picking_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                              ByVal pIdOperadorBodega As Integer,
                                                              Optional pConnection As SqlConnection = Nothing,
                                                              Optional pTransaction As SqlTransaction = Nothing) As List(Of Integer)


        Dim lReturnValue As Integer = 0
        Dim lDataAdapter As New SqlDataAdapter
        Dim lRecs As New List(Of Integer)
        Dim id As Integer

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand

            Dim vSQL As String = "SELECT IdPickingEnc FROM VW_Tareas_Picking_HH 
                                  WHERE ((estado = 'Nuevo' OR estado = 'Pendiente') 
                                  AND (activo = 1) 
                                  AND IdBodega = @IdBodega 
                                  AND IdOperadorBodega = @IdOperadorBodega )"

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            lCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))

            lDataAdapter = New SqlDataAdapter(lCommand)
            Dim dt As New DataTable
            lDataAdapter.Fill(dt)

            For Each dr As DataRow In dt.Rows
                id = dr.Item(0)
                lRecs.Add(id)
            Next


            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lRecs

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Verificaciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                       ByVal pIdOperadorBodega As Integer,
                                                                       Optional pConnection As SqlConnection = Nothing,
                                                                       Optional pTransaction As SqlTransaction = Nothing) As Integer


        Dim lReturnValue As Integer
        'Dim lDataAdapter As New SqlDataAdapter

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand

            '#GT10012024: Se busca por estado Pickeado/Pendiente en el pedido, ya no por Pendiente
            'Dim vSQL As String = "SELECT COUNT(IdPedidoEnc)  
            '                      FROM  trans_pe_enc AS pe INNER JOIN
            '                            trans_picking_enc AS p ON pe.IdPickingEnc = p.IdPickingEnc INNER JOIN
            '                            trans_picking_op AS o ON p.IdPickingEnc = o.IdPickingEnc 
            '                      WHERE pe.IdBodega = @IdBodega AND pe.estado = 'Pendiente' AND o.IdOperadorBodega = @IdOperadorBodega
            '                      AND EXISTS 
            '                      (SELECT pd.*
            '                      FROM trans_pe_det pd INNER JOIN 
            '                      trans_picking_det pkd ON pd.IdPedidoDet = pkd.IdPedidoDet 
            '                      WHERE pkd.cantidad_recibida > 0 AND pd.IdPedidoEnc = pe.IdPedidoEnc)"


            Dim vSQL As String = "SELECT COUNT(distinct IdPedidoEnc)  
                                  FROM  trans_pe_enc AS pe INNER JOIN
                                        trans_picking_enc AS p ON pe.IdPickingEnc = p.IdPickingEnc INNER JOIN
                                        trans_picking_op AS o ON p.IdPickingEnc = o.IdPickingEnc 
                                  WHERE pe.IdBodega = @IdBodega AND pe.estado in('Pickeado','Pendiente')
                                  AND EXISTS 
                                  (SELECT pd.*
                                  FROM trans_pe_det pd INNER JOIN 
                                  trans_picking_det pkd ON pd.IdPedidoDet = pkd.IdPedidoDet 
                                  WHERE pkd.cantidad_recibida > 0 AND pd.IdPedidoEnc = pe.IdPedidoEnc)
                                        AND pe.IdPedidoEnc IN (SELECT DISTINCT IdPedido FROM stock_res) 
                                        AND pe.IdPedidoEnc NOT IN (SELECT IdPedidoEnc FROM trans_manufactura_enc WHERE estado <> 'Cerrado')"

            If pIdOperadorBodega <> 0 Then
                vSQL += "  AND o.IdOperadorBodega = @IdOperadorBodega "
            End If

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If

            lCommand.CommandTimeout = 60
            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            If pIdOperadorBodega <> 0 Then
                lCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
            End If

            lReturnValue = lCommand.ExecuteScalar()

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lReturnValue

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IDs_Verificaciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                     ByVal pIdOperadorBodega As Integer,
                                                                     Optional pConnection As SqlConnection = Nothing,
                                                                     Optional pTransaction As SqlTransaction = Nothing) As List(Of Integer)


        Dim id As Integer
        Dim lDataAdapter As New SqlDataAdapter
        Dim lRecs As New List(Of Integer)

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand

            Dim vSQL As String = "SELECT IdPedidoEnc   
                                  FROM  trans_pe_enc AS pe INNER JOIN
                                        trans_picking_enc AS p ON pe.IdPickingEnc = p.IdPickingEnc INNER JOIN
                                        trans_picking_op AS o ON p.IdPickingEnc = o.IdPickingEnc 
                                  WHERE pe.IdBodega = @IdBodega AND pe.estado = 'Pendiente' AND o.IdOperadorBodega = @IdOperadorBodega
                                  AND EXISTS 
                                  (SELECT pd.*
                                  FROM trans_pe_det pd INNER JOIN 
                                  trans_picking_det pkd ON pd.IdPedidoDet = pkd.IdPedidoDet 
                                  WHERE pkd.cantidad_recibida > 0 AND pd.IdPedidoEnc = pe.IdPedidoEnc)"

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If

            lCommand.CommandTimeout = 60
            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            lCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))

            lDataAdapter = New SqlDataAdapter(lCommand)
            Dim dt As New DataTable
            lDataAdapter.Fill(dt)

            For Each dr As DataRow In dt.Rows
                id = dr.Item(0)
                lRecs.Add(id)
            Next

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lRecs

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Cambio_Est_Ubic_For_HH(ByVal pCambioEstado As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIdOperadorBodega As Integer,
                                                            Optional pConnection As SqlConnection = Nothing,
                                                            Optional pTransaction As SqlTransaction = Nothing) As Integer

        Dim lReturnValue As Integer

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand


            'GT28122021: agregué campo cambio_estado para diferenciar tareas de cambio ubicacion
            'GT281220211831: se ajusta vista, para diferenciar por Cambio Estado
            'GT29122021: el distinct se maneja dentro del count para evitar mostrar 2 veces la misma tarea, cuando esta asignada a 2 operadores

            'Dim vSQL As String = "SELECT COUNT (DISTINCT IdTareaUbicacionEnc) as Tareas
            '                      FROM vw_cantidad_tareas_ubicacion_op 
            '                      WHERE IdOperadorBodega is not null AND IdBodega = @IdBodega AND (IdOperadorBodega = @IdOperadorBodega OR IdOperadorBodegaDet = @IdOperadorBodega) "

            Dim vSQL As String = "SELECT count(distinct IdTareaUbicacionEnc) as Tareas
                                    FROM vw_cantidad_tareas_ubicacion_op 
                                    where IdBodega=@IdBodega AND (IdOperadorBodega=@IdOperadorBodega OR IdOperadorBodegaOp=@IdOperadorBodega) 
                                    AND cambio_estado=@pCambioEstado "


            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            lCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
            lCommand.Parameters.Add(New SqlParameter("@pCambioEstado", pCambioEstado))

            lReturnValue = lCommand.ExecuteScalar()

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lReturnValue

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IDs_Cambio_Est_Ubic_For_HH(ByVal pCambioEstado As Integer,
                                                          ByVal pIdBodega As Integer,
                                                          ByVal pIdOperadorBodega As Integer,
                                                          Optional pConnection As SqlConnection = Nothing,
                                                          Optional pTransaction As SqlTransaction = Nothing) As List(Of Integer)




        Dim id As Integer
        Dim lDataAdapter As New SqlDataAdapter
        Dim lRecs As New List(Of Integer)

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As SqlCommand

            Dim vSQL As String = "SELECT IdTareaUbicacionEnc 
                                  FROM VW_Cantidad_Tareas_Ubicacion_Op_Items
                                  WHERE IdBodega=@IdBodega AND (IdOperadorBodega=@IdOperadorBodega OR IdOperadorBodegaOp=@IdOperadorBodega) and cambio_estado=@pCambioEstado "

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If


            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.Add(New SqlParameter("@pCambioEstado", pCambioEstado))
            lCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            lCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))

            lDataAdapter = New SqlDataAdapter(lCommand)
            Dim dt As New DataTable
            lDataAdapter.Fill(dt)

            For Each dr As DataRow In dt.Rows
                id = dr.Item(0)
                lRecs.Add(id)
            Next

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lRecs

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Recepciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeTareasIngresoHH)

        Get_All_Recepciones_For_HH_By_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim vSQL As String = " SELECT * FROM VW_RECEPCION_FOR_HH_BY_IDBODEGA WHERE IdBodega = @IdBodega"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim Rec As clsBeTareasIngresoHH
            Dim lRecs As New List(Of clsBeTareasIngresoHH)

            For Each dr As DataRow In dt.Rows

                Rec = New clsBeTareasIngresoHH
                Rec.IdRecepcionEnc = dr("N")
                Rec.IdPropietarioBodega = dr("IdPropietarioBodega")
                Rec.IdPropietario = dr("IdPropietario")
                Rec.NombrePropietario = dr("Propietario")
                Rec.IdProveedor = dr("IdProveedor")
                Rec.NombreProveedor = dr("Proveedor")
                Rec.NoDocumentoOc = dr("No_Documento")
                Rec.NombreMotivoDevolucion = IIf(IsDBNull(dr("MotivoDevolucion")), "", dr("MotivoDevolucion"))
                Rec.NombreTipoIngresoOC = dr("Tipo")
                Rec.NoReferenciaOC = dr("Referencia")
                Rec.IdOrderCompraEnc = dr("IdOrdenCompraEnc")
                Rec.NombreTipoIngresoOC = dr("Tipo")
                Rec.NombreTipoRecepcion = dr("TipoTrans")
                lRecs.Add(Rec)

            Next

            lTransaction.Commit()

            dt.Dispose()

            Return lRecs

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Recepciones_For_HH_By_IdBodega_By_Operador(ByVal pIdBodega As Integer, ByVal pIdOperadorBodega As Integer) As List(Of clsBeTareasIngresoHH)

        Get_All_Recepciones_For_HH_By_IdBodega_By_Operador = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = " SELECT * FROM VW_Recepcion_For_HH_By_IdBodega_By_Operador WHERE IdBodega = @IdBodega AND IdOperadorBodega = @IdOperadorBodega"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)
            cmd.Parameters.AddWithValue("@IdOperadorBodega", pIdOperadorBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim Rec As clsBeTareasIngresoHH
            Dim lRecs As New List(Of clsBeTareasIngresoHH)

            For Each dr As DataRow In dt.Rows

                Rec = New clsBeTareasIngresoHH
                Rec.IdRecepcionEnc = dr("N")
                Rec.IdPropietarioBodega = dr("IdPropietarioBodega")
                Rec.IdPropietario = dr("IdPropietario")
                Rec.NombrePropietario = dr("Propietario")
                Rec.IdProveedor = dr("IdProveedor")
                Rec.NombreProveedor = dr("Proveedor")
                Rec.NoDocumentoOc = dr("No_Documento")
                Rec.NombreMotivoDevolucion = IIf(IsDBNull(dr("MotivoDevolucion")), "", dr("MotivoDevolucion"))
                Rec.NombreTipoIngresoOC = dr("Tipo")
                Rec.NoReferenciaOC = dr("Referencia")
                Rec.IdOrderCompraEnc = dr("IdOrdenCompraEnc")
                Rec.NombreTipoIngresoOC = dr("Tipo")
                Rec.NombreTipoRecepcion = dr("TipoTrans")
                Rec.NumOrden = IIf(IsDBNull(dr("NumOrden")), "", dr("NumOrden"))
                Rec.NumPoliza = IIf(IsDBNull(dr("NumPoliza")), "", dr("NumPoliza"))
                Rec.Muelle = IIf(IsDBNull(dr("Muelle")), "", dr("Muelle"))

                lRecs.Add(Rec)

            Next

            lTransaction.Commit()

            dt.Dispose()

            Return lRecs

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Rec_Ciegas_For_HH_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeTareasIngresoHH)

        Get_All_Rec_Ciegas_For_HH_By_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim vSQL As String = " Select trans_re_enc.IdRecepcionEnc As N, 
                                 propietario_bodega.IdPropietarioBodega, propietario_bodega.IdPropietario,
                                 propietarios.nombre_comercial As Propietario
                                 FROM  trans_re_enc 
                                 INNER JOIN propietario_bodega On trans_re_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega 
                                 INNER JOIN propietarios On propietario_bodega.IdPropietario = propietarios.IdPropietario 
                                 INNER JOIN bodega_muelles On trans_re_enc.IdMuelle = bodega_muelles.IdMuelle 
                                 WHERE trans_re_enc.estado Not In ('Cerrado','Anulado') AND propietario_bodega.IdBodega = @IdBodega AND
                                 EXISTS( Select IdTipoTransaccion from trans_re_tr trt WHERE usahh = 1 And 
                                 trans_re_enc.IdTipoTransaccion=trt.IdTipoTransaccion And trt.IdTipoTransaccion In ('HSOC00','HSOD00'))"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim Rec As clsBeTareasIngresoHH
            Dim lRecs As New List(Of clsBeTareasIngresoHH)

            For Each dr As DataRow In dt.Rows

                Rec = New clsBeTareasIngresoHH
                Rec.IdRecepcionEnc = dr("N")
                Rec.IdPropietarioBodega = dr("IdPropietarioBodega")
                Rec.IdPropietario = dr("IdPropietario")
                Rec.NombrePropietario = dr("Propietario")
                lRecs.Add(Rec)

            Next

            lTransaction.Commit()

            dt.Dispose()

            Return lRecs

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#CKFK 20180604 Modifiqué esta función para que liste los picking por operador de bodega
    Public Shared Function Get_All_Picking_For_HH_By_IdBodega_And_IdOperadorBodega(ByVal pIdBodega As Integer, ByVal pIdOperadorBodega As Integer) As List(Of clsBeTrans_picking_enc)

        Get_All_Picking_For_HH_By_IdBodega_And_IdOperadorBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * FROM VW_Tareas_Picking_HH 
                                  WHERE ((estado = 'Nuevo' OR estado = 'Pendiente') 
                                  AND (activo = 1) 
                                  AND IdBodega = @IdBodega 
                                  AND IdOperadorBodega = @IdOperadorBodega) "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            cmd.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
            dad.Fill(dt)

            Dim Picking As clsBeTrans_picking_enc
            Dim lPicking As New List(Of clsBeTrans_picking_enc)

            For Each dr As DataRow In dt.Rows

                Picking = New clsBeTrans_picking_enc

                With Picking

                    .IdPickingEnc = dr("IdPickingEnc")
                    .IdPropietarioBodega = dr("IdPropietarioBodega")
                    .NombreBodega = dr("NombreBodega")
                    .IdPickingEnc = dr("IdPickingEnc")
                    .IdBodega = dr("IdBodega")
                    .IdPropietarioBodega = dr("IdPropietarioBodega")
                    .IdUbicacionPicking = dr("IdUbicacionPicking")
                    .Fecha_picking = dr("Fecha_picking")
                    .Hora_ini = dr("Hora_ini")
                    .Hora_fin = dr("Hora_fin")
                    .Estado = dr("Estado")
                    .User_agr = dr("User_agr")
                    .Fec_agr = dr("Fec_agr")
                    .User_mod = dr("User_mod")
                    .Fec_mod = dr("Fec_mod")
                    .Detalle_operador = dr("Detalle_operador")
                    .Activo = dr("Activo")
                    .NombreBodega = IIf(IsDBNull(dr("NombreBodega")), "", dr("NombreBodega"))
                    .NombrePropietarioPicking = IIf(IsDBNull(dr("nombre_comercial")), "", dr("nombre_comercial"))
                    .NombreUbicacionPicking = IIf(IsDBNull(dr("NombreUbicacion")), "", dr("NombreUbicacion"))
                    '#AT20220511 Agregé este nuevo campo para mostrarse en la HH
                    .Referencia = IIf(IsDBNull(dr("Referencia")), "", dr("Referencia"))
                    .IdBodegaMuelle = IIf(IsDBNull(dr("IdBodegaMuelle")), 0, dr("IdBodegaMuelle"))
                    .IdPrioridadPicking = IIf(IsDBNull(dr("IdPrioridadPicking")), 0, dr("IdPrioridadPicking"))
                    .NombrePrioridad = IIf(IsDBNull(dr("NombrePrioridad")), "", dr("NombrePrioridad"))
                    .Tiene_Manufactura = IIf(IsDBNull(dr("Tiene_Manufactura")), False, dr("Tiene_Manufactura"))
                    .NombreMuelle = IIf(IsDBNull(dr("NombreMuelle")), "", dr("NombreMuelle"))
                    '#GT29042025: campos para determinar el escaneo de muelle en picking
                    .IdUbicacionMuelle = IIf(IsDBNull(dr("IdUbicacionDefecto")), 0, dr("IdUbicacionDefecto"))
                    .Codigo_Barra_Muelle = IIf(IsDBNull(dr("codigo_barra_muelle")), "", dr("codigo_barra_muelle"))

                End With

                lPicking.Add(Picking)

            Next

            lTransaction.Commit()

            dt.Dispose()

            Get_All_Picking_For_HH_By_IdBodega_And_IdOperadorBodega = lPicking

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar_By_IdTareaHH(ByVal IdTareaHH As Integer,
                                                 Optional ByVal pConection As SqlConnection = Nothing,
                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tarea_hh Where(IdTareahh = @IdTareahh)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAHH", IdTareaHH))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Shared Function GetIdTarea(ByVal IdTransaccion As Integer, ByVal IdTipoTarea As Integer, ByRef pConection As SqlConnection, ByRef pTransaction As SqlTransaction) As Integer

        Dim lReturnValue As Integer = 0
        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lDataAdapter As New SqlDataAdapter

        Try

            Dim vSQL As String = "SELECT t.IdTareaHH FROM tarea_hh AS t WHERE t.IdTransaccion = @IdTransaccion AND IdTipoTarea = @IdTipoTarea "

            lDataAdapter = New SqlDataAdapter(vSQL, pConection)
            lDataAdapter.SelectCommand.Transaction = pTransaction
            lDataAdapter.SelectCommand.CommandType = CommandType.Text
            lDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@IdTransaccion", IdTransaccion))
            lDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@IdTipoTarea", IdTipoTarea))

            lReturnValue = lDataAdapter.SelectCommand.ExecuteScalar()

            Return lReturnValue

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180422 Modifiqué la función ActualizaEstadoTarea para poder modificar el estado de la tarea a finalizado o anulado 
    '#CKFK 20190123 Le agregué a la funcion que actualice la fechafin cuando el IdEstado es Anulado o Finalizado
    Public Shared Function Actualiza_Estado_Tarea(ByVal IdTransaccion As Integer,
                                                  ByVal IdTipoTarea As Integer,
                                                  ByVal IdEstado As Integer,
                                                  ByRef pConection As SqlConnection,
                                                  ByRef pTransaction As SqlTransaction) As Integer

        Try

            Dim vSQL As String = ""

            '#CKFK 20211129 No estaba correcto lo que estaba llamando ya que en IdTransaccion me llega el Id de la tabla tarea_hh y no de la tal
            If IdEstado = 4 OrElse IdEstado = 3 Then
                vSQL = "UPDATE tarea_hh SET IdEstado = @IdEstado, FechaFin = GetDate() 
                        WHERE IdTransaccion = @IdTransaccion AND IdTipoTarea = @IdTipoTarea "
            Else
                vSQL = "UPDATE tarea_hh SET IdEstado = @IdEstado 
                        WHERE IdTransaccion = @IdTransaccion 
                        AND IdTipoTarea = @IdTipoTarea "
            End If

            Dim cmd As New SqlCommand(vSQL, pConection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IdTransaccion", IdTransaccion))
            cmd.Parameters.Add(New SqlParameter("@IdTipoTarea", IdTipoTarea))
            cmd.Parameters.Add(New SqlParameter("@IdEstado", IdEstado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Finalizar_Tarea_Recepcion(ByVal pIdRecepcionEnc As Integer,
                                                     ByRef pConection As SqlConnection,
                                                     ByRef pTransaction As SqlTransaction) As Integer

        Finalizar_Tarea_Recepcion = 0

        Try

            Dim vSQL As String = ""


            vSQL = "UPDATE tarea_hh 
                    SET IdEstado = @IdEstado, 
                    FechaFin = GetDate() 
                    WHERE IdTransaccion = @IdTransaccion 
                    AND IdTipoTarea = @IdTipoTarea "

            Dim cmd As New SqlCommand(vSQL, pConection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IdTransaccion", pIdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IdTipoTarea", 1)) 'Recepción
            cmd.Parameters.Add(New SqlParameter("@IdEstado", 4)) 'Finalizado

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTarea_hh As clsBeTarea_hh,
                                     ByRef pConection As SqlConnection,
                                     ByRef pTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Dim sp As String = "SELECT * FROM Tarea_hh Where(IdTareahh = @IdTareahh)"
            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAHH", pBeTarea_hh.IdTareahh))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTarea_hh, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Guardar_Tarea_Ubicacion_HH(ByRef pBeTransUbicHHEnc As clsBeTrans_ubic_hh_enc,
                                                 ByVal pIdPropietario As Integer,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction)

        Try

            Dim BeTarea_Existente As New clsBeTarea_hh()

            BeTarea_Existente = GetSingle(pBeTransUbicHHEnc.IdTipoTarea, pBeTransUbicHHEnc.IdTareaUbicacionEnc, pIdPropietario, lConnection, lTransaction)

            '#EJC20171025_1143AM: Validar que la tarea de ubicación en la HH no ha sido finalizada antes de actualizar/insertar
            If pBeTransUbicHHEnc.Estado <> "Finalizado" Then
                '#CM_20171116: Guarda la tarea de la HH, se agregó IsNew para Insertar y Actualizar.
                If pBeTransUbicHHEnc.IsNew Then

                    Dim objTarea As New clsBeTarea_hh() With {
                   .IdTareahh = MaxID(lConnection, lTransaction),
                   .IdPropietario = pIdPropietario,
                   .IdEstado = 1,
                   .IdTransaccion = pBeTransUbicHHEnc.IdTareaUbicacionEnc,
                   .Tipo = 0,
                   .FechaInicio = pBeTransUbicHHEnc.FechaInicio,
                   .FechaFin = pBeTransUbicHHEnc.FechaFin,
                   .DiaCompleto = False,
                   .Ubicacion = "",
                   .Descripcion = pBeTransUbicHHEnc.Observacion.Trim,
                   .Asunto = pBeTransUbicHHEnc.Asunto,
                   .IdPrioridad = pBeTransUbicHHEnc.IdPrioridad,
                   .IdTipoTarea = pBeTransUbicHHEnc.IdTipoTarea,
                   .IdBodega = pBeTransUbicHHEnc.IdBodega,
                   .IdMuelle = Nothing}

                    Insertar(objTarea, lConnection, lTransaction)

                Else
                    Actualizar(BeTarea_Existente, lConnection, lTransaction)
                End If

            Else

                '#EJC20171025_1158AM: Asegurarse de que la tarea para la HH también esté en estado finalizada.
                BeTarea_Existente.IdEstado = 4
                Actualizar(BeTarea_Existente, lConnection, lTransaction)

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Guardar_Tarea_Recepcion_HH(ByRef pObjTareaHH As clsBeTarea_hh,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Try

            If pObjTareaHH IsNot Nothing Then
                If pObjTareaHH.IsNew AndAlso pObjTareaHH.CreaTarea Then
                    pObjTareaHH.IdTareahh = MaxID(lConnection, lTransaction)
                    Insertar(pObjTareaHH, lConnection, lTransaction)
                Else
                    '#EJC20171022_1049AM: Se agregó actualizar pero no se ha probado.
                    Actualizar(pObjTareaHH, lConnection, lTransaction)
                End If
            End If


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Guardar_Tarea_Picking_HH(ByRef pObjTareaHH As clsBeTarea_hh,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction)

        Try

            If pObjTareaHH IsNot Nothing AndAlso pObjTareaHH.IsNew AndAlso pObjTareaHH.CreaTarea Then
                pObjTareaHH.IdTareahh = MaxID(lConnection, lTransaction)
                Insertar(pObjTareaHH, lConnection, lTransaction)
            Else
                '#EJC20171022_1049AM: Se agregó actualizar pero no se ha probado.
                Actualizar(pObjTareaHH, lConnection, lTransaction)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CKFK 20180430 09:45 AM Se creo la función Guardar_Tarea_Verificacion_HH para crear la tarea de verificación cuando se haya pickeado el pedido.
    Public Shared Sub Guardar_Tarea_Verificacion_HH(ByRef pObjTareaHH As clsBeTarea_hh,
                                        ByRef lConnection As SqlConnection,
                                        ByRef lTransaction As SqlTransaction)

        Try

            If pObjTareaHH IsNot Nothing AndAlso pObjTareaHH.IsNew AndAlso pObjTareaHH.CreaTarea Then
                pObjTareaHH.IdTareahh = MaxID()
                Insertar(pObjTareaHH, lConnection, lTransaction)
            Else
                '#EJC20171022_1049AM: Se agregó actualizar pero no se ha probado.
                Actualizar(pObjTareaHH, lConnection, lTransaction)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function GetSingle(ByRef pBeTarea_hh As clsBeTarea_hh) As Boolean

        GetSingle = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Tarea_hh" &
            " Where(IdTareahh = @IdTareahh)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAHH", pBeTarea_hh.IdTareahh))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTarea_hh, dt.Rows(0))
            End If

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Picking_Para_Empaque_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeTrans_picking_enc)

        Get_All_Picking_Para_Empaque_By_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "  SELECT p.IdPickingEnc, 
                                    p.IdBodega, 
                                    p.IdPropietarioBodega, 
                                    p.IdUbicacionPicking, 
                                    p.fecha_picking, 
                                    p.hora_ini, p.hora_fin, 
                                    p.estado, 
                                    p.user_agr, 
                                    p.fec_agr, 
                                    p.user_mod, 
                                    p.fec_mod, 
                                    p.detalle_operador, 
                                    p.activo, 
                                    p.verifica_auto,
                                    p.procesado_bof,
                                    p.Requiere_Preparacion,
                                    p.Tipo_Preparacion,
                                    b.nombre AS NombreBodega, 
                                    pp.nombre_comercial,
                                    d.IdPedidoEnc,
		                            pe.bodega_destino Referencia
                                    FROM trans_picking_enc AS p INNER JOIN 
                                         (SELECT distinct IdPedidoEnc, IdPickingEnc 
                                          FROM trans_picking_ubic 
                                          WHERE cantidad_verificada>0 and 
                                                cantidad_despachada < cantidad_verificada AND
                                                dañado_picking =0 AND
                                                dañado_verificacion = 0 AND 
                                                no_encontrado = 0) AS d ON p.IdPickingEnc = d.IdPickingEnc INNER JOIN
                                    	  trans_pe_enc pe ON pe.IdPickingEnc = p.IdPickingEnc AND 
                                                             d.IdPedidoEnc = pe.IdPedidoEnc INNER JOIN
                                          bodega AS b ON p.IdBodega = b.IdBodega INNER JOIN
                                          propietario_bodega AS pb ON pb.IdPropietarioBodega = p.IdPropietarioBodega AND
                                                                      b.IdBodega = pb.IdBodega INNER JOIN
                                          propietarios AS pp ON pp.IdPropietario = pb.IdPropietario 
                                    WHERE (p.estado in ('Procesado', 'Pendiente','Verificado')
                                    AND p.Requiere_Preparacion=1 
                                    AND p.activo=1 
                                    AND p.IdBodega=@IdBodega 
                                    AND p.estado_preparacion IN ('Nuevo','Pendiente'))  
                                    AND d.IdPedidoEnc NOT IN (SELECT IdPedidoEnc FROM trans_packing_enc WHERE finalizado = 1 AND IdDespachoEnc = 0)
									AND d.IdPedidoEnc NOT IN (SELECT IdPedidoEnc FROM trans_pe_enc WHERE estado = 'Despachado')"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            dad.Fill(dt)

            Dim Picking As clsBeTrans_picking_enc
            Dim lPicking As New List(Of clsBeTrans_picking_enc)

            For Each dr As DataRow In dt.Rows

                Picking = New clsBeTrans_picking_enc

                With Picking

                    .IdPickingEnc = dr("IdPickingEnc")
                    .IdPropietarioBodega = dr("IdPropietarioBodega")
                    .NombreBodega = dr("NombreBodega")
                    .IdBodega = dr("IdBodega")
                    .Fecha_picking = dr("Fecha_picking")
                    .Hora_ini = dr("Hora_ini")
                    .Hora_fin = dr("Hora_fin")
                    .Estado = dr("Estado")
                    .User_agr = dr("User_agr")
                    .Fec_agr = dr("Fec_agr")
                    .User_mod = dr("User_mod")
                    .Fec_mod = dr("Fec_mod")
                    .Detalle_operador = dr("Detalle_operador")
                    .Activo = dr("Activo")
                    .verifica_auto = dr("verifica_auto")
                    .procesado_bof = dr("procesado_bof")
                    .Requiere_Preparacion = dr("Requiere_Preparacion")
                    .Tipo_Preparacion = dr("Tipo_Preparacion")
                    .NombreBodega = IIf(IsDBNull(dr("NombreBodega")), "", dr("NombreBodega"))
                    .NombrePropietarioPicking = IIf(IsDBNull(dr("nombre_comercial")), "", dr("nombre_comercial"))
                    .IdPedidoEnc = dr("IdPedidoEnc")
                    .Referencia = IIf(IsDBNull(dr("Referencia")), "", dr("Referencia"))

                End With

                lPicking.Add(Picking)

            Next

            lTransaction.Commit()

            dt.Dispose()

            Get_All_Picking_Para_Empaque_By_IdBodega = lPicking

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim ss As String = ex.Message
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Anular_By_IdTranHH(ByVal IdTranHH As Integer, ByVal IdBodega As Integer,
                                                 Optional ByVal pConection As SqlConnection = Nothing,
                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Update Tarea_hh set IdEstado=3 Where (IdTransaccion = @IdTranHH) and (IdBodega=@IdBodega)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANHH", IdTranHH))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Shared Function Get_Tarea_By_IdTransaccion_And_TipoTarea(ByVal IdTransaccion As Integer,
                                     ByVal IdTipoTarea As Integer) As clsBeTarea_hh

        Dim Obj As New clsBeTarea_hh

        Try

            Dim vSQL As String = "SELECT * FROM tarea_hh WHERE IdTransaccion = @IdTransaccion AND IdTipoTarea = @IdTipoTarea"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTransaccion", IdTransaccion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTarea", IdTipoTarea)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Cargar(Obj, lRow)

                            Obj.IsNew = False

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Obj

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Tarea_PickingPendiente(ByRef pBeTarea As clsBeTarea_hh,
                                                                   Optional ByRef pConection As SqlConnection = Nothing,
                                                                   Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tarea_hh")
            Upd.Add("IdEstado", "@IdEstado", DataType.Parametro)
            Upd.Add("IdOperadorBodega_Cerro", "@IdOperadorBodega_Cerro", DataType.Parametro)
            Upd.Add("Host_Cerro", "@Host_Cerro", DataType.Parametro)
            Upd.Where("IdTareahh = @IdTareahh")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdEstado", pBeTarea.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IdOperadorBodega_Cerro", pBeTarea.IdOperadorBodega_Cerro))
            cmd.Parameters.Add(New SqlParameter("@Host_Cerro", pBeTarea.Host_Cerro))
            cmd.Parameters.Add(New SqlParameter("@IdTareahh", pBeTarea.IdTareahh))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Lista_Tareas_Operador_By_IdBodega(ByVal IdBodega As Integer,
                                                                 ByVal pFechaDel As Date,
                                                                 ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT TipoTarea, IdTarea,Fecha, NombreOperador 
                                  FROM VW_Tareas_Operador WHERE IdBodega = @IdBodega "
            vSQL += " AND cast(Fecha AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
               " AND " & FormatoFechas.fFecha(pFechaAl)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using sqlCommand As New SqlCommand(vSQL, lConnection)

                        sqlCommand.CommandType = CommandType.Text
                        sqlCommand.Transaction = lTransaction
                        sqlCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        If lConnection.State <> ConnectionState.Open Then
                            lConnection.OpenAsync()
                        End If

                        Using reader As SqlDataReader = sqlCommand.ExecuteReader()
                            lTable.Load(reader)
                        End Using

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw
        End Try

    End Function

End Class
