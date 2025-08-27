Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnMontacarga

    Public Shared Sub Cargar(ByRef oBeMontacarga As clsBeMontacarga, ByRef dr As DataRow)

        Try

            With oBeMontacarga

                .IdMontacarga = IIf(IsDBNull(dr.Item("IdMontacarga")), 0, dr.Item("IdMontacarga"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Modelo = IIf(IsDBNull(dr.Item("Modelo")), "", dr.Item("Modelo"))
                .Serie = IIf(IsDBNull(dr.Item("Serie")), "", dr.Item("Serie"))
                .Capacidad_basica = IIf(IsDBNull(dr.Item("capacidad_basica")), 0.0, dr.Item("capacidad_basica"))
                .Desplazamiento_motor = IIf(IsDBNull(dr.Item("desplazamiento_motor")), 0.0, dr.Item("desplazamiento_motor"))
                .Costo_Hora = IIf(IsDBNull(dr.Item("Costo_Hora")), 0.0, dr.Item("Costo_Hora"))
                .Tipo_combustible = IIf(IsDBNull(dr.Item("tipo_combustible")), "", dr.Item("tipo_combustible"))
                .Tipo_montacarga = IIf(IsDBNull(dr.Item("tipo_montacarga")), "", dr.Item("tipo_montacarga"))
                .Fecha_compra = IIf(IsDBNull(dr.Item("fecha_compra")), Date.Now, dr.Item("fecha_compra"))
                .Fecha_inicio_operaciones = IIf(IsDBNull(dr.Item("fecha_inicio_operaciones")), Date.Now, dr.Item("fecha_inicio_operaciones"))
                .Proximo_mantenimiento = IIf(IsDBNull(dr.Item("proximo_mantenimiento")), Date.Now, dr.Item("proximo_mantenimiento"))
                .Nivel_Desde = IIf(IsDBNull(dr.Item("Nivel_Desde")), 0, dr.Item("Nivel_Desde"))
                .Nivel_Hasta = IIf(IsDBNull(dr.Item("Nivel_Hasta")), 0, dr.Item("Nivel_Hasta"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeMontacarga As clsBeMontacarga, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("montacarga")
            Ins.Add("idmontacarga", "@idmontacarga", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("modelo", "@modelo", DataType.Parametro)
            Ins.Add("serie", "@serie", DataType.Parametro)
            Ins.Add("capacidad_basica", "@capacidad_basica", DataType.Parametro)
            Ins.Add("desplazamiento_motor", "@desplazamiento_motor", DataType.Parametro)
            Ins.Add("costo_hora", "@costo_hora", DataType.Parametro)
            Ins.Add("tipo_combustible", "@tipo_combustible", DataType.Parametro)
            Ins.Add("tipo_montacarga", "@tipo_montacarga", DataType.Parametro)
            Ins.Add("fecha_compra", "@fecha_compra", DataType.Parametro)
            Ins.Add("fecha_inicio_operaciones", "@fecha_inicio_operaciones", DataType.Parametro)
            Ins.Add("proximo_mantenimiento", "@proximo_mantenimiento", DataType.Parametro)
            Ins.Add("nivel_desde", "@nivel_desde", DataType.Parametro)
            Ins.Add("nivel_hasta", "@nivel_hasta", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDMONTACARGA", oBeMontacarga.IdMontacarga))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMontacarga.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMontacarga.Nombre))
            cmd.Parameters.Add(New SqlParameter("@MODELO", oBeMontacarga.Modelo))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeMontacarga.Serie))
            cmd.Parameters.Add(New SqlParameter("@CAPACIDAD_BASICA", oBeMontacarga.Capacidad_basica))
            cmd.Parameters.Add(New SqlParameter("@DESPLAZAMIENTO_MOTOR", oBeMontacarga.Desplazamiento_motor))
            cmd.Parameters.Add(New SqlParameter("@COSTO_HORA", oBeMontacarga.Costo_Hora))
            cmd.Parameters.Add(New SqlParameter("@TIPO_COMBUSTIBLE", oBeMontacarga.Tipo_combustible))
            cmd.Parameters.Add(New SqlParameter("@TIPO_MONTACARGA", oBeMontacarga.Tipo_montacarga))
            cmd.Parameters.Add(New SqlParameter("@FECHA_COMPRA", oBeMontacarga.Fecha_compra))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INICIO_OPERACIONES", oBeMontacarga.Fecha_inicio_operaciones))
            cmd.Parameters.Add(New SqlParameter("@PROXIMO_MANTENIMIENTO", oBeMontacarga.Proximo_mantenimiento))
            cmd.Parameters.Add(New SqlParameter("@NIVEL_DESDE", oBeMontacarga.Nivel_Desde))
            cmd.Parameters.Add(New SqlParameter("@NIVEL_HASTA", oBeMontacarga.Nivel_Hasta))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeMontacarga.IdMontacarga = CInt(cmd.Parameters("@IDMONTACARGA").Value)

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeMontacarga As clsBeMontacarga, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("montacarga")
            Upd.Add("idmontacarga", "@idmontacarga", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("modelo", "@modelo", DataType.Parametro)
            Upd.Add("serie", "@serie", DataType.Parametro)
            Upd.Add("capacidad_basica", "@capacidad_basica", DataType.Parametro)
            Upd.Add("desplazamiento_motor", "@desplazamiento_motor", DataType.Parametro)
            Upd.Add("costo_hora", "@costo_hora", DataType.Parametro)
            Upd.Add("tipo_combustible", "@tipo_combustible", DataType.Parametro)
            Upd.Add("tipo_montacarga", "@tipo_montacarga", DataType.Parametro)
            Upd.Add("fecha_compra", "@fecha_compra", DataType.Parametro)
            Upd.Add("fecha_inicio_operaciones", "@fecha_inicio_operaciones", DataType.Parametro)
            Upd.Add("proximo_mantenimiento", "@proximo_mantenimiento", DataType.Parametro)
            Upd.Add("nivel_desde", "@nivel_desde", DataType.Parametro)
            Upd.Add("nivel_hasta", "@nivel_hasta", DataType.Parametro)
            Upd.Where("IdMontacarga = @IdMontacarga")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMONTACARGA", oBeMontacarga.IdMontacarga))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMontacarga.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMontacarga.Nombre))
            cmd.Parameters.Add(New SqlParameter("@MODELO", oBeMontacarga.Modelo))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeMontacarga.Serie))
            cmd.Parameters.Add(New SqlParameter("@CAPACIDAD_BASICA", oBeMontacarga.Capacidad_basica))
            cmd.Parameters.Add(New SqlParameter("@DESPLAZAMIENTO_MOTOR", oBeMontacarga.Desplazamiento_motor))
            cmd.Parameters.Add(New SqlParameter("@COSTO_HORA", oBeMontacarga.Costo_Hora))
            cmd.Parameters.Add(New SqlParameter("@TIPO_COMBUSTIBLE", oBeMontacarga.Tipo_combustible))
            cmd.Parameters.Add(New SqlParameter("@TIPO_MONTACARGA", oBeMontacarga.Tipo_montacarga))
            cmd.Parameters.Add(New SqlParameter("@FECHA_COMPRA", oBeMontacarga.Fecha_compra))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INICIO_OPERACIONES", oBeMontacarga.Fecha_inicio_operaciones))
            cmd.Parameters.Add(New SqlParameter("@PROXIMO_MANTENIMIENTO", oBeMontacarga.Proximo_mantenimiento))
            cmd.Parameters.Add(New SqlParameter("@NIVEL_DESDE", oBeMontacarga.Nivel_Desde))
            cmd.Parameters.Add(New SqlParameter("@NIVEL_HASTA", oBeMontacarga.Nivel_Hasta))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeMontacarga As clsBeMontacarga, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Montacarga Where(IdMontacarga = @IdMontacarga)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMONTACARGA", oBeMontacarga.IdMontacarga))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Montacarga"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Montacarga"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeMontacarga As clsBeMontacarga) As Boolean

        Try

            Const sp As String = "SELECT * FROM Montacarga " &
            " Where(IdMontacarga = @IdMontacarga)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMONTACARGA", oBeMontacarga.IDMONTACARGA))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMontacarga, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeMontacarga)

        Try

            Dim lReturnList As New List(Of clsBeMontacarga)
            Const sp As String = "SELECT * FROM Montacarga"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMontacarga As New clsBeMontacarga

            For Each dr As DataRow In dt.Rows

                vBeMontacarga = New clsBeMontacarga
                Cargar(vBeMontacarga, dr)
                lReturnList.Add(vBeMontacarga)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeMontacarga As clsBeMontacarga)

        Try

            Const sp As String = "SELECT * FROM Montacarga" & _
            " Where(IdMontacarga = @IdMontacarga)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMONTACARGA", pBeMontacarga.IDMONTACARGA))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMontacarga, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMontacarga),0) FROM Montacarga"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
