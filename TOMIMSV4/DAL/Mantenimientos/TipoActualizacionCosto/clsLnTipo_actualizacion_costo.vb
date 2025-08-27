Imports System.Data.SqlClient

Public Class clsLnTipo_actualizacion_costo

    Public Shared Sub Cargar(ByRef oBeTipo_actualizacion_costo As clsBeTipo_actualizacion_costo, ByRef dr As DataRow)
        Try
            With oBeTipo_actualizacion_costo
                .IdTipoActualizacionCosto = IIf(IsDBNull(dr.Item("IdTipoActualizacionCosto")), 0, dr.Item("IdTipoActualizacionCosto"))
                .NombreActualizacionCosto = IIf(IsDBNull(dr.Item("NombreActualizacionCosto")), "", dr.Item("NombreActualizacionCosto"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Insertar(ByRef oBeTipo_actualizacion_costo As clsBeTipo_actualizacion_costo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("tipo_actualizacion_costo")
            Ins.Add("idtipoactualizacioncosto", "@idtipoactualizacioncosto", DataType.Parametro)
            Ins.Add("nombreactualizacioncosto", "@nombreactualizacioncosto", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOACTUALIZACIONCOSTO", oBeTipo_actualizacion_costo.IdTipoActualizacionCosto))
            cmd.Parameters.Add(New SqlParameter("@NOMBREACTUALIZACIONCOSTO", oBeTipo_actualizacion_costo.NombreActualizacionCosto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeTipo_actualizacion_costo As clsBeTipo_actualizacion_costo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("tipo_actualizacion_costo")
            Upd.Add("idtipoactualizacioncosto", "@idtipoactualizacioncosto", DataType.Parametro)
            Upd.Add("nombreactualizacioncosto", "@nombreactualizacioncosto", DataType.Parametro)
            Upd.Where("IdTipoActualizacionCosto = @IdTipoActualizacionCosto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOACTUALIZACIONCOSTO", oBeTipo_actualizacion_costo.IdTipoActualizacionCosto))
            cmd.Parameters.Add(New SqlParameter("@NOMBREACTUALIZACIONCOSTO", oBeTipo_actualizacion_costo.NombreActualizacionCosto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeTipo_actualizacion_costo As clsBeTipo_actualizacion_costo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Tipo_actualizacion_costo" &
             "  Where(IdTipoActualizacionCosto = @IdTipoActualizacionCosto)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOACTUALIZACIONCOSTO", oBeTipo_actualizacion_costo.IdTipoActualizacionCosto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Obtener(ByRef oBeTipo_actualizacion_costo As clsBeTipo_actualizacion_costo) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Tipo_actualizacion_costo" &
            " Where(IdTipoActualizacionCosto = @IdTipoActualizacionCosto)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOACTUALIZACIONCOSTO", oBeTipo_actualizacion_costo.IdTipoActualizacionCosto))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTipo_actualizacion_costo, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
