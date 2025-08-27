Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnMarcaje

    Public Shared Sub Cargar(ByRef oBeMarcaje As clsBeMarcaje, ByRef dr As DataRow)
        Try
            With oBeMarcaje
                .IdMarcaje = IIf(IsDBNull(dr.Item("IdMarcaje")), 0, dr.Item("IdMarcaje"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
                .IdDispositivo = IIf(IsDBNull(dr.Item("IdDispositivo")), 0, dr.Item("IdDispositivo"))
                .IdHorarioLaboral = IIf(IsDBNull(dr.Item("IdHorarioLaboral")), 0, dr.Item("IdHorarioLaboral"))
                .Fec_lectura = IIf(IsDBNull(dr.Item("Fec_lectura")), Nothing, dr.Item("Fec_lectura"))
                .Hora_inicio_horario = IIf(IsDBNull(dr.Item("Hora_inicio_horario")), Date.Now, dr.Item("Hora_inicio_horario"))
                .Hora_fin_horario = IIf(IsDBNull(dr.Item("Hora_fin_horario")), Date.Now, dr.Item("Hora_fin_horario"))
                .Ingreso_anticipado = IIf(IsDBNull(dr.Item("Ingreso_anticipado")), False, dr.Item("Ingreso_anticipado"))
                .Salida_anticipada = IIf(IsDBNull(dr.Item("Salida_anticipada")), False, dr.Item("Salida_anticipada"))
                .Ingreso_tardio = IIf(IsDBNull(dr.Item("Ingreso_tardio")), False, dr.Item("Ingreso_tardio"))
                .Salida_tardia = IIf(IsDBNull(dr.Item("Salida_tardia")), False, dr.Item("Salida_tardia"))
                .Hora_lectura = IIf(IsDBNull(dr.Item("Hora_lectura")), Date.Now, dr.Item("Hora_lectura"))
                .Entro = IIf(IsDBNull(dr.Item("Entro")), False, dr.Item("Entro"))
                .Salio = IIf(IsDBNull(dr.Item("Salio")), False, dr.Item("Salio"))
                .Hora_entro = IIf(IsDBNull(dr.Item("Hora_entro")), Date.Now, dr.Item("Hora_entro"))
                .Hora_salio = IIf(IsDBNull(dr.Item("Hora_salio")), Date.Now, dr.Item("Hora_salio"))
                .Marcaje_manual = IIf(IsDBNull(dr.Item("Marcaje_manual")), False, dr.Item("Marcaje_manual"))
                .Primer_marcaje = IIf(IsDBNull(dr.Item("Primer_marcaje")), 0, dr.Item("Primer_marcaje"))
                .Marcaje_contabilizado = IIf(IsDBNull(dr.Item("Marcaje_contabilizado")), False, dr.Item("Marcaje_contabilizado"))
                .Marcaje_aproximado = IIf(IsDBNull(dr.Item("Marcaje_aproximado")), False, dr.Item("Marcaje_aproximado"))
                .Marcaje_fuera_de_sucursal = IIf(IsDBNull(dr.Item("Marcaje_fuera_de_sucursal")), False, dr.Item("Marcaje_fuera_de_sucursal"))
                .Es_bitacora = IIf(IsDBNull(dr.Item("Es_bitacora")), False, dr.Item("Es_bitacora"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeMarcaje As clsBeMarcaje, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("marcaje")
            Ins.Add("idmarcaje", "@idmarcaje", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("iddispositivo", "@iddispositivo", DataType.Parametro)
            Ins.Add("idhorariolaboral", "@idhorariolaboral", DataType.Parametro)
            Ins.Add("fec_lectura", "@fec_lectura", DataType.Parametro)
            Ins.Add("hora_inicio_horario", "@hora_inicio_horario", DataType.Parametro)
            Ins.Add("hora_fin_horario", "@hora_fin_horario", DataType.Parametro)
            Ins.Add("ingreso_anticipado", "@ingreso_anticipado", DataType.Parametro)
            Ins.Add("salida_anticipada", "@salida_anticipada", DataType.Parametro)
            Ins.Add("ingreso_tardio", "@ingreso_tardio", DataType.Parametro)
            Ins.Add("salida_tardia", "@salida_tardia", DataType.Parametro)
            Ins.Add("hora_lectura", "@hora_lectura", DataType.Parametro)
            Ins.Add("entro", "@entro", DataType.Parametro)
            Ins.Add("salio", "@salio", DataType.Parametro)
            Ins.Add("hora_entro", "@hora_entro", DataType.Parametro)
            Ins.Add("hora_salio", "@hora_salio", DataType.Parametro)
            Ins.Add("marcaje_manual", "@marcaje_manual", DataType.Parametro)
            Ins.Add("primer_marcaje", "@primer_marcaje", DataType.Parametro)
            Ins.Add("marcaje_contabilizado", "@marcaje_contabilizado", DataType.Parametro)
            Ins.Add("marcaje_aproximado", "@marcaje_aproximado", DataType.Parametro)
            Ins.Add("marcaje_fuera_de_sucursal", "@marcaje_fuera_de_sucursal", DataType.Parametro)
            Ins.Add("es_bitacora", "@es_bitacora", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMARCAJE", oBeMarcaje.IdMarcaje))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMarcaje.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeMarcaje.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeMarcaje.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IDDISPOSITIVO", oBeMarcaje.IdDispositivo))
            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORAL", oBeMarcaje.IdHorarioLaboral))
            cmd.Parameters.Add(New SqlParameter("@FEC_LECTURA", oBeMarcaje.Fec_lectura))
            cmd.Parameters.Add(New SqlParameter("@HORA_INICIO_HORARIO", oBeMarcaje.Hora_inicio_horario))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN_HORARIO", oBeMarcaje.Hora_fin_horario))
            cmd.Parameters.Add(New SqlParameter("@INGRESO_ANTICIPADO", oBeMarcaje.Ingreso_anticipado))
            cmd.Parameters.Add(New SqlParameter("@SALIDA_ANTICIPADA", oBeMarcaje.Salida_anticipada))
            cmd.Parameters.Add(New SqlParameter("@INGRESO_TARDIO", oBeMarcaje.Ingreso_tardio))
            cmd.Parameters.Add(New SqlParameter("@SALIDA_TARDIA", oBeMarcaje.Salida_tardia))
            cmd.Parameters.Add(New SqlParameter("@HORA_LECTURA", oBeMarcaje.Hora_lectura))
            cmd.Parameters.Add(New SqlParameter("@ENTRO", oBeMarcaje.Entro))
            cmd.Parameters.Add(New SqlParameter("@SALIO", oBeMarcaje.Salio))
            cmd.Parameters.Add(New SqlParameter("@HORA_ENTRO", oBeMarcaje.Hora_entro))
            cmd.Parameters.Add(New SqlParameter("@HORA_SALIO", oBeMarcaje.Hora_salio))
            cmd.Parameters.Add(New SqlParameter("@MARCAJE_MANUAL", oBeMarcaje.Marcaje_manual))
            cmd.Parameters.Add(New SqlParameter("@PRIMER_MARCAJE", oBeMarcaje.Primer_marcaje))
            cmd.Parameters.Add(New SqlParameter("@MARCAJE_CONTABILIZADO", oBeMarcaje.Marcaje_contabilizado))
            cmd.Parameters.Add(New SqlParameter("@MARCAJE_APROXIMADO", oBeMarcaje.Marcaje_aproximado))
            cmd.Parameters.Add(New SqlParameter("@MARCAJE_FUERA_DE_SUCURSAL", oBeMarcaje.Marcaje_fuera_de_sucursal))
            cmd.Parameters.Add(New SqlParameter("@ES_BITACORA", oBeMarcaje.Es_bitacora))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeMarcaje As clsBeMarcaje, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("marcaje")
            Upd.Add("idmarcaje", "@idmarcaje", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("iddispositivo", "@iddispositivo", DataType.Parametro)
            Upd.Add("idhorariolaboral", "@idhorariolaboral", DataType.Parametro)
            Upd.Add("fec_lectura", "@fec_lectura", DataType.Parametro)
            Upd.Add("hora_inicio_horario", "@hora_inicio_horario", DataType.Parametro)
            Upd.Add("hora_fin_horario", "@hora_fin_horario", DataType.Parametro)
            Upd.Add("ingreso_anticipado", "@ingreso_anticipado", DataType.Parametro)
            Upd.Add("salida_anticipada", "@salida_anticipada", DataType.Parametro)
            Upd.Add("ingreso_tardio", "@ingreso_tardio", DataType.Parametro)
            Upd.Add("salida_tardia", "@salida_tardia", DataType.Parametro)
            Upd.Add("hora_lectura", "@hora_lectura", DataType.Parametro)
            Upd.Add("entro", "@entro", DataType.Parametro)
            Upd.Add("salio", "@salio", DataType.Parametro)
            Upd.Add("hora_entro", "@hora_entro", DataType.Parametro)
            Upd.Add("hora_salio", "@hora_salio", DataType.Parametro)
            Upd.Add("marcaje_manual", "@marcaje_manual", DataType.Parametro)
            Upd.Add("primer_marcaje", "@primer_marcaje", DataType.Parametro)
            Upd.Add("marcaje_contabilizado", "@marcaje_contabilizado", DataType.Parametro)
            Upd.Add("marcaje_aproximado", "@marcaje_aproximado", DataType.Parametro)
            Upd.Add("marcaje_fuera_de_sucursal", "@marcaje_fuera_de_sucursal", DataType.Parametro)
            Upd.Add("es_bitacora", "@es_bitacora", DataType.Parametro)
            Upd.Where("IdMarcaje = @IdMarcaje")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMARCAJE", oBeMarcaje.IdMarcaje))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMarcaje.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeMarcaje.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeMarcaje.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IDDISPOSITIVO", oBeMarcaje.IdDispositivo))
            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORAL", oBeMarcaje.IdHorarioLaboral))
            cmd.Parameters.Add(New SqlParameter("@FEC_LECTURA", oBeMarcaje.Fec_lectura))
            cmd.Parameters.Add(New SqlParameter("@HORA_INICIO_HORARIO", oBeMarcaje.Hora_inicio_horario))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN_HORARIO", oBeMarcaje.Hora_fin_horario))
            cmd.Parameters.Add(New SqlParameter("@INGRESO_ANTICIPADO", oBeMarcaje.Ingreso_anticipado))
            cmd.Parameters.Add(New SqlParameter("@SALIDA_ANTICIPADA", oBeMarcaje.Salida_anticipada))
            cmd.Parameters.Add(New SqlParameter("@INGRESO_TARDIO", oBeMarcaje.Ingreso_tardio))
            cmd.Parameters.Add(New SqlParameter("@SALIDA_TARDIA", oBeMarcaje.Salida_tardia))
            cmd.Parameters.Add(New SqlParameter("@HORA_LECTURA", oBeMarcaje.Hora_lectura))
            cmd.Parameters.Add(New SqlParameter("@ENTRO", oBeMarcaje.Entro))
            cmd.Parameters.Add(New SqlParameter("@SALIO", oBeMarcaje.Salio))
            cmd.Parameters.Add(New SqlParameter("@HORA_ENTRO", oBeMarcaje.Hora_entro))
            cmd.Parameters.Add(New SqlParameter("@HORA_SALIO", oBeMarcaje.Hora_salio))
            cmd.Parameters.Add(New SqlParameter("@MARCAJE_MANUAL", oBeMarcaje.Marcaje_manual))
            cmd.Parameters.Add(New SqlParameter("@PRIMER_MARCAJE", oBeMarcaje.Primer_marcaje))
            cmd.Parameters.Add(New SqlParameter("@MARCAJE_CONTABILIZADO", oBeMarcaje.Marcaje_contabilizado))
            cmd.Parameters.Add(New SqlParameter("@MARCAJE_APROXIMADO", oBeMarcaje.Marcaje_aproximado))
            cmd.Parameters.Add(New SqlParameter("@MARCAJE_FUERA_DE_SUCURSAL", oBeMarcaje.Marcaje_fuera_de_sucursal))
            cmd.Parameters.Add(New SqlParameter("@ES_BITACORA", oBeMarcaje.Es_bitacora))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeMarcaje As clsBeMarcaje, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Marcaje" &
             "  Where(IdMarcaje = @IdMarcaje)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMARCAJE", oBeMarcaje.IdMarcaje))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function GetAll() As List(Of clsBeMarcaje)

        Dim lReturnList As New List(Of clsBeMarcaje)

        Try

            Const sp As String = "SELECT * FROM Marcaje"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeMarcaje As New clsBeMarcaje

                        For Each dr As DataRow In lDataTable.Rows
                            vBeMarcaje = New clsBeMarcaje
                            Cargar(vBeMarcaje, dr)
                            lReturnList.Add(vBeMarcaje)
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

    Public Shared Sub Get_Marcaje_By_Operador_And_FechaActual(ByRef pBeMarcaje As clsBeMarcaje)

        Try

            Dim sp As String = "SELECT * FROM Marcaje 
							    WHERE(IdEmpresa= @IdEmpresa
								AND IdBodega = @Idbodega
								AND IdOperador = @IdOperador
								AND Fec_Lectura = " & FormatoFechas.fFecha(Now.Date) & ")"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pBeMarcaje.IdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pBeMarcaje.IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pBeMarcaje.IdOperador)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeMarcaje As New clsBeMarcaje

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeMarcaje, lDataTable.Rows(0))
                        Else
                            pBeMarcaje = Nothing
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

    Public Shared Function Tiene_Marcaje_By_Operador_Bodega_And_FechaActual(pIdEmpresa As Integer,
                                                                            pIdBodega As Integer,
                                                                            pIdOperadorBodega As Integer) As Boolean

        Dim lReturnValue As Boolean = False

        Try

            Dim sp As String = "SELECT m.* 
								FROM Marcaje m
								WHERE EXISTS (SELECT IdOperadorBodega FROM Operador_bodega ob
											  WHERE ob.IdOperadorBodega = @IdOperadorBodega AND ob.IdOperador = m.IdOperador)
								      AND m.IdEmpresa= @IdEmpresa
							          AND m.IdBodega = @Idbodega
								      AND Fec_Lectura = " & FormatoFechas.fFecha(Now.Date)


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperadorBodega", pIdOperadorBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            lReturnValue = True
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

        Return lReturnValue

    End Function

    Public Shared Sub Get_Ultimo_Marcaje_By_Operador(ByRef pBeMarcaje As clsBeMarcaje)

        Try

            Dim sp As String = "SELECT * FROM Marcaje 
							    WHERE(IdEmpresa= @IdEmpresa
								AND IdBodega = @Idbodega
								AND IdOperador = @IdOperador) ORDER BY Fec_lectura DESC "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pBeMarcaje.IdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pBeMarcaje.IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pBeMarcaje.IdOperador)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeMarcaje As New clsBeMarcaje

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeMarcaje, lDataTable.Rows(0))
                        Else
                            pBeMarcaje = Nothing
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMarcaje),0) FROM Marcaje"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Marcaje_By_Operador_And_FechaActual(ByRef pBeMarcaje As clsBeMarcaje,
                                                                   ByRef lConnection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction) As clsBeMarcaje

        Get_Marcaje_By_Operador_And_FechaActual = Nothing

        Try

            Dim sp As String = "SELECT top 1 * FROM Marcaje 
							      WHERE(IdEmpresa= @IdEmpresa
								  AND IdBodega = @Idbodega
								  AND IdOperador = @IdOperador
								  AND Fec_Lectura = " & FormatoFechas.fFecha(Now.Date) & ")   
								  order by IdMarcaje Desc"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pBeMarcaje.IdEmpresa)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pBeMarcaje.IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pBeMarcaje.IdOperador)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeMarcaje As New clsBeMarcaje

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeMarcaje, lDataTable.Rows(0))
                    Return vBeMarcaje
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function MaxID(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMarcaje),0) FROM Marcaje"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class