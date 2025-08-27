Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTarea_hh

    Public Shared Sub Cargar(ByRef oBeTarea_hh As clsBeTarea_hh, ByRef dr As DataRow)
        Try
            With oBeTarea_hh
                .IdTareahh = IIf(IsDBNull(dr.Item("IdTareahh")), 0, dr.Item("IdTareahh"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdMuelle = IIf(IsDBNull(dr.Item("IdMuelle")), 0, dr.Item("IdMuelle"))
                .IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
                .IdPrioridad = IIf(IsDBNull(dr.Item("IdPrioridad")), 0, dr.Item("IdPrioridad"))
                .IdTipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                .IdTransaccion = IIf(IsDBNull(dr.Item("IdTransaccion")), 0, dr.Item("IdTransaccion"))
                .Tipo = IIf(IsDBNull(dr.Item("Tipo")), 0, dr.Item("Tipo"))
                .FechaInicio = IIf(IsDBNull(dr.Item("FechaInicio")), Date.Now, dr.Item("FechaInicio"))
                .FechaFin = IIf(IsDBNull(dr.Item("FechaFin")), Date.Now, dr.Item("FechaFin"))
                .DiaCompleto = IIf(IsDBNull(dr.Item("DiaCompleto")), False, dr.Item("DiaCompleto"))
                .Asunto = IIf(IsDBNull(dr.Item("Asunto")), "", dr.Item("Asunto"))
                .Ubicacion = IIf(IsDBNull(dr.Item("Ubicacion")), "", dr.Item("Ubicacion"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .Recordatorio = IIf(IsDBNull(dr.Item("Recordatorio")), "", dr.Item("Recordatorio"))
            End With
        Catch ex As Exception
            Throw New Exception("Tarea_hh_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTarea_hh As clsBeTarea_hh, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tarea_hh")
            Ins.Add("idtareahh", "@idtareahh", DataType.Parametro)
            If Not oBeTarea_hh.IdPropietario = 0 Then Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idmuelle", "@idmuelle", DataType.Parametro)
            Ins.Add("idestado", "@idestado", DataType.Parametro)
            Ins.Add("idprioridad", "@idprioridad", DataType.Parametro)
            Ins.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Ins.Add("idtransaccion", "@idtransaccion", DataType.Parametro)
            Ins.Add("tipo", "@tipo", DataType.Parametro)
            Ins.Add("fechainicio", "@fechainicio", DataType.Parametro)
            Ins.Add("fechafin", "@fechafin", DataType.Parametro)
            Ins.Add("diacompleto", "@diacompleto", DataType.Parametro)
            Ins.Add("asunto", "@asunto", DataType.Parametro)
            Ins.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("recordatorio", "@recordatorio", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAHH", oBeTarea_hh.IdTareahh))
            If Not oBeTarea_hh.IdPropietario = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTarea_hh.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTarea_hh.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLE", oBeTarea_hh.IdMuelle))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeTarea_hh.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRIORIDAD", oBeTarea_hh.IdPrioridad))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeTarea_hh.IdTipoTarea))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeTarea_hh.IdTransaccion))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeTarea_hh.Tipo))
            cmd.Parameters.Add(New SqlParameter("@FECHAINICIO", oBeTarea_hh.FechaInicio))
            cmd.Parameters.Add(New SqlParameter("@FECHAFIN", oBeTarea_hh.FechaFin))
            cmd.Parameters.Add(New SqlParameter("@DIACOMPLETO", oBeTarea_hh.DiaCompleto))
            cmd.Parameters.Add(New SqlParameter("@ASUNTO", IIf(oBeTarea_hh.Asunto Is Nothing, DBNull.Value, oBeTarea_hh.Asunto)))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", IIf(oBeTarea_hh.Ubicacion Is Nothing, DBNull.Value, oBeTarea_hh.Ubicacion)))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", IIf(oBeTarea_hh.Descripcion Is Nothing, DBNull.Value, oBeTarea_hh.Descripcion)))
            cmd.Parameters.Add(New SqlParameter("@RECORDATORIO", IIf(oBeTarea_hh.Recordatorio Is Nothing, DBNull.Value, oBeTarea_hh.Recordatorio)))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Tarea_hh_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTarea_hh As clsBeTarea_hh, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tarea_hh")
            Upd.Add("idtareahh", "@idtareahh", DataType.Parametro)
            If Not oBeTarea_hh.IdPropietario = 0 Then Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idmuelle", "@idmuelle", DataType.Parametro)
            Upd.Add("idestado", "@idestado", DataType.Parametro)
            Upd.Add("idprioridad", "@idprioridad", DataType.Parametro)
            Upd.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Upd.Add("idtransaccion", "@idtransaccion", DataType.Parametro)
            Upd.Add("tipo", "@tipo", DataType.Parametro)
            Upd.Add("fechainicio", "@fechainicio", DataType.Parametro)
            Upd.Add("fechafin", "@fechafin", DataType.Parametro)
            Upd.Add("diacompleto", "@diacompleto", DataType.Parametro)
            Upd.Add("asunto", "@asunto", DataType.Parametro)
            Upd.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("recordatorio", "@recordatorio", DataType.Parametro)
            Upd.Add("idoperadorbodega_cerro", "@idoperadorbodega_cerro", DataType.Parametro)
            Upd.Add("host_cerro", "@host_cerro", DataType.Parametro)
            Upd.Where("IdTareahh = @IdTareahh")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAHH", oBeTarea_hh.IdTareahh))
            If Not oBeTarea_hh.IdPropietario = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTarea_hh.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTarea_hh.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLE", oBeTarea_hh.IdMuelle))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeTarea_hh.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRIORIDAD", oBeTarea_hh.IdPrioridad))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeTarea_hh.IdTipoTarea))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeTarea_hh.IdTransaccion))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeTarea_hh.Tipo))
            cmd.Parameters.Add(New SqlParameter("@FECHAINICIO", oBeTarea_hh.FechaInicio))
            cmd.Parameters.Add(New SqlParameter("@FECHAFIN", oBeTarea_hh.FechaFin))
            cmd.Parameters.Add(New SqlParameter("@DIACOMPLETO", oBeTarea_hh.DiaCompleto))
            cmd.Parameters.Add(New SqlParameter("@ASUNTO", IIf(oBeTarea_hh.Asunto Is Nothing, DBNull.Value, oBeTarea_hh.Asunto)))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", IIf(oBeTarea_hh.Ubicacion Is Nothing, DBNull.Value, oBeTarea_hh.Ubicacion)))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", IIf(oBeTarea_hh.Descripcion Is Nothing, DBNull.Value, oBeTarea_hh.Descripcion)))
            cmd.Parameters.Add(New SqlParameter("@RECORDATORIO", IIf(oBeTarea_hh.Recordatorio Is Nothing, DBNull.Value, oBeTarea_hh.Recordatorio)))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_CERRO", oBeTarea_hh.IdOperadorBodega_Cerro))
            cmd.Parameters.Add(New SqlParameter("@HOST_CERRO", oBeTarea_hh.Host_Cerro))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Tarea_hh_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTarea_hh As clsBeTarea_hh, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Const sp As String = " Delete from Tarea_hh" &
             "  Where(IdTareahh = @IdTareahh)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAHH", oBeTarea_hh.IdTareahh))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Tarea_hh_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tarea_hh"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Tarea_hh_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Tarea_hh"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Tarea_hh_Listar: " & ex.Message)
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTarea_hh As clsBeTarea_hh) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Tarea_hh " &
            " Where(IdTareahh = @IdTareahh)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAHH", oBeTarea_hh.IdTareahh))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTarea_hh, dt.Rows(0))
                Return True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTarea_hh)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeTarea_hh)
            Const sp As String = "SELECT * FROM Tarea_hh "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTarea_hh As New clsBeTarea_hh

            For Each dr As DataRow In dt.Rows

                vBeTarea_hh = New clsBeTarea_hh
                Cargar(vBeTarea_hh, dr)
                lReturnList.Add(vBeTarea_hh)

            Next

            cmd.Dispose()

            lTransaction.Commit()

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

    Public Shared Function GetSingle(ByVal IdTipoTarea As Integer,
                                     ByVal IdTransaccion As Integer,
                                     ByVal IdPropietario As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeTarea_hh

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Tarea_hh " &
                                " Where(IdTipoTarea = @IdTipoTarea " &
                                " And IdTransaccion = @IdTransaccion " &
                                " And IdPropietario = @IdPropietario ) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim pBeTarea_hh As New clsBeTarea_hh

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTipoTarea", IdTipoTarea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTransaccion", IdTransaccion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPropietario", IdPropietario))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTarea_hh, dt.Rows(0))
            End If

            Return pBeTarea_hh

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Count_All_Services(pIdBodega As Integer, pIdOperadorBodega As Integer) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Count_All_Services = ""
        Dim Rr, S, Ss As String
        Dim Rv As Integer
        Dim lRec1, lRec2, lRec3, lRec4, lRec5 As New List(Of Integer)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#GT18042023: llamadas con transaction
            Rv = Get_Count_Recepciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega, lRec1,
                                                                                        lConnection,
                                                                                        lTransaction)
            Rr = "R" & Rv & ","
            Rv = Get_Count_Picking_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega, lRec2,
                                                                                    lConnection,
                                                                                    lTransaction)
            Rr &= "P" & Rv & ","
            Rv = Get_Count_Verificaciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega, lRec3,
                                                                                           lConnection,
                                                                                           lTransaction)
            Rr &= "V" & Rv & ","
            Rv = Get_Count_Cambio_Est_Ubic_For_HH(0, pIdBodega, pIdOperadorBodega, lRec4,
                                                                                   lConnection,
                                                                                   lTransaction)
            Rr &= "U" & Rv & ","
            Rv = Get_Count_Cambio_Est_Ubic_For_HH(1, pIdBodega, pIdOperadorBodega, lRec5,
                                                                                   lConnection,
                                                                                   lTransaction)
            Rr &= "E" & Rv

            Ss = Rr & ";"

            For i = 0 To lRec1.Count - 1
                S = "R" & lRec1(i)
                Ss = Ss & S & ";"
            Next

            For i = 0 To lRec2.Count - 1
                S = "P" & lRec2(i)
                Ss = Ss & S & ";"
            Next

            For i = 0 To lRec3.Count - 1
                S = "V" & lRec3(i)
                Ss = Ss & S & ";"
            Next

            For i = 0 To lRec4.Count - 1
                S = "U" & lRec4(i)
                Ss = Ss & S & ";"
            Next

            For i = 0 To lRec5.Count - 1
                S = "E" & lRec5(i)
                Ss = Ss & S & ";"
            Next

            Get_Count_All_Services = Ss

            lTransaction.Commit()

            Return Get_Count_All_Services

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try
    End Function

    Public Shared Function Get_Count_Recepciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                    ByVal pIdOperadorBodega As Integer,
                                                                    ByRef lrec As List(Of Integer),
                                                                    lConnection As SqlConnection,
                                                                    lTransaction As SqlTransaction) As Integer

        Get_Count_Recepciones_For_HH_By_IdBodega = 0

        Try
            Get_Count_Recepciones_For_HH_By_IdBodega = clsLnTarea_hh.Get_Count_Recepciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)
            lrec = clsLnTarea_hh.Get_IDs_Recepciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega,
                                                                                   lConnection,
                                                                                   lTransaction)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Picking_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                ByVal pIdOperadorBodega As Integer, ByRef lrec As List(Of Integer),
                                                                lConnection As SqlConnection,
                                                                lTransaction As SqlTransaction) As Integer
        Get_Count_Picking_For_HH_By_IdBodega = 0
        Try
            Get_Count_Picking_For_HH_By_IdBodega = clsLnTarea_hh.Get_Count_Picking_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)
            lrec = clsLnTarea_hh.Get_IDs_Picking_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega,
                                                                               lConnection,
                                                                               lTransaction)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Verificaciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                       ByVal pIdOperadorBodega As Integer,
                                                                       ByRef lrec As List(Of Integer),
                                                                       lConnection As SqlConnection,
                                                                       lTransaction As SqlTransaction) As Integer

        Get_Count_Verificaciones_For_HH_By_IdBodega = 0

        Try
            Get_Count_Verificaciones_For_HH_By_IdBodega = Get_Count_Verificaciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)
            lrec = Get_IDs_Verificaciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega,
                                                                                      lConnection,
                                                                                      lTransaction)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Cambio_Est_Ubic_For_HH(ByVal pCambioEstado As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIdOperadorBodega As Integer,
                                                            ByRef lrec As List(Of Integer),
                                                            lConnection As SqlConnection,
                                                            lTransaction As SqlTransaction) As Integer

        Get_Count_Cambio_Est_Ubic_For_HH = 0
        Try
            Get_Count_Cambio_Est_Ubic_For_HH = clsLnTarea_hh.Get_Count_Cambio_Est_Ubic_For_HH(pCambioEstado, pIdBodega, pIdOperadorBodega,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)
            lrec = clsLnTarea_hh.Get_IDs_Cambio_Est_Ubic_For_HH(pCambioEstado, pIdBodega, pIdOperadorBodega,
                                                                                          lConnection,
                                                                                          lTransaction)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
