Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTipo_tarima

    Public Shared Sub Cargar(ByRef oBeTipo_tarima As clsBeTipo_tarima, ByRef dr As DataRow)
        Try
            With oBeTipo_tarima
                .IdTipoTarima = IIf(IsDBNull(dr.Item("IdTipoTarima")), 0, dr.Item("IdTipoTarima"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Alto = IIf(IsDBNull(dr.Item("Alto")), 0.0, dr.Item("Alto"))
                .Largo = IIf(IsDBNull(dr.Item("Largo")), 0.0, dr.Item("Largo"))
                .Ancho = IIf(IsDBNull(dr.Item("Ancho")), 0.0, dr.Item("Ancho"))
                .CargaDinamica = IIf(IsDBNull(dr.Item("CargaDinamica")), 0.0, dr.Item("CargaDinamica"))
                .CargaEstatica = IIf(IsDBNull(dr.Item("CargaEstatica")), 0.0, dr.Item("CargaEstatica"))
                .CargaEstanterias = IIf(IsDBNull(dr.Item("CargaEstanterias")), 0.0, dr.Item("CargaEstanterias"))
                .EntradasTransPaleta = IIf(IsDBNull(dr.Item("EntradasTransPaleta")), 0.0, dr.Item("EntradasTransPaleta"))
                .PesoPromedio = IIf(IsDBNull(dr.Item("PesoPromedio")), 0.0, dr.Item("PesoPromedio"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Tara = IIf(IsDBNull(dr.Item("Tara")), 0.0, dr.Item("Tara"))
            End With
        Catch ex As Exception
            Throw New Exception("Tipo_tarima_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Function Insertar(ByRef oBeTipo_tarima As clsBeTipo_tarima, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tipo_tarima")
            Ins.Add("idtipotarima", "@idtipotarima", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("cargadinamica", "@cargadinamica", DataType.Parametro)
            Ins.Add("cargaestatica", "@cargaestatica", DataType.Parametro)
            Ins.Add("cargaestanterias", "@cargaestanterias", DataType.Parametro)
            Ins.Add("entradastranspaleta", "@entradastranspaleta", DataType.Parametro)
            Ins.Add("pesopromedio", "@pesopromedio", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("tara", "@tara", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTARIMA", oBeTipo_tarima.IdTipoTarima))
            'cmd.Parameters("@IDTIPOTARIMA").Direction = ParameterDirection.Output
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTipo_tarima.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeTipo_tarima.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeTipo_tarima.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeTipo_tarima.Ancho))
            cmd.Parameters.Add(New SqlParameter("@CARGADINAMICA", oBeTipo_tarima.CargaDinamica))
            cmd.Parameters.Add(New SqlParameter("@CARGAESTATICA", oBeTipo_tarima.CargaEstatica))
            cmd.Parameters.Add(New SqlParameter("@CARGAESTANTERIAS", oBeTipo_tarima.CargaEstanterias))
            cmd.Parameters.Add(New SqlParameter("@ENTRADASTRANSPALETA", oBeTipo_tarima.EntradasTransPaleta))
            cmd.Parameters.Add(New SqlParameter("@PESOPROMEDIO", oBeTipo_tarima.PesoPromedio))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTipo_tarima.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTipo_tarima.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTipo_tarima.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTipo_tarima.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTipo_tarima.Activo))
            cmd.Parameters.Add(New SqlParameter("@TARA", oBeTipo_tarima.Tara))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTipo_tarima.IdTipoTarima = CInt(cmd.Parameters("@IDTIPOTARIMA").Value)

        Catch ex As Exception
            Throw New Exception("Tipo_tarima_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeTipo_tarima As clsBeTipo_tarima, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tipo_tarima")
            Upd.Add("idtipotarima", "@idtipotarima", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("cargadinamica", "@cargadinamica", DataType.Parametro)
            Upd.Add("cargaestatica", "@cargaestatica", DataType.Parametro)
            Upd.Add("cargaestanterias", "@cargaestanterias", DataType.Parametro)
            Upd.Add("entradastranspaleta", "@entradastranspaleta", DataType.Parametro)
            Upd.Add("pesopromedio", "@pesopromedio", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("tara", "@tara", DataType.Parametro)
            Upd.Where("IdTipoTarima = @IdTipoTarima")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTARIMA", oBeTipo_tarima.IdTipoTarima))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTipo_tarima.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeTipo_tarima.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeTipo_tarima.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeTipo_tarima.Ancho))
            cmd.Parameters.Add(New SqlParameter("@CARGADINAMICA", oBeTipo_tarima.CargaDinamica))
            cmd.Parameters.Add(New SqlParameter("@CARGAESTATICA", oBeTipo_tarima.CargaEstatica))
            cmd.Parameters.Add(New SqlParameter("@CARGAESTANTERIAS", oBeTipo_tarima.CargaEstanterias))
            cmd.Parameters.Add(New SqlParameter("@ENTRADASTRANSPALETA", oBeTipo_tarima.EntradasTransPaleta))
            cmd.Parameters.Add(New SqlParameter("@PESOPROMEDIO", oBeTipo_tarima.PesoPromedio))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTipo_tarima.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTipo_tarima.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTipo_tarima.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTipo_tarima.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTipo_tarima.Activo))
            cmd.Parameters.Add(New SqlParameter("@TARA", oBeTipo_tarima.Tara))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Tipo_tarima_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeTipo_tarima As clsBeTipo_tarima, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Const sp As String = " Delete from Tipo_tarima" &
             "  Where(IdTipoTarima = @IdTipoTarima)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then

                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)


            End If


            cmd.Parameters.Add(New SqlParameter("@IDTIPOTARIMA", oBeTipo_tarima.IdTipoTarima))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Tipo_tarima_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tipo_tarima"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

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
            Throw New Exception("Tipo_tarima_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Tipo_tarima"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Tipo_tarima_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBeTipo_tarima As clsBeTipo_tarima) As Boolean

        Try

            Const sp As String = "SELECT * FROM Tipo_tarima" &
            " Where(IdTipoTarima = @IdTipoTarima)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTARIMA", oBeTipo_tarima.IdTipoTarima))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTipo_tarima, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTipo_tarima)

        Try

            Dim lReturnList As New List(Of clsBeTipo_tarima)
            Const sp As String = "SELECT * FROM Tipo_tarima"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTipo_tarima As New clsBeTipo_tarima

            For Each dr As DataRow In dt.Rows

                vBeTipo_tarima = New clsBeTipo_tarima
                Cargar(vBeTipo_tarima, dr)
                lReturnList.Add(vBeTipo_tarima)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Tipo_tarima_GetAll: " & ex.Message)
        End Try

    End Function

    Public Function GetSingle(ByRef pBeTipo_tarima As clsBeTipo_tarima)

        Try

            Const sp As String = "SELECT * FROM Tipo_tarima" &
            " Where(IdTipoTarima = @IdTipoTarima)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTARIMA", pBeTipo_tarima.IdTipoTarima))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTipo_tarima, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
