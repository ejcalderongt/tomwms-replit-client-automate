Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnRegla_ubic_det_pp

    Public Shared Sub Cargar(ByRef oBeRegla_ubic_det_pp As clsBeRegla_ubic_det_pp, ByRef dr As DataRow)
        Try
            With oBeRegla_ubic_det_pp
                .IdReglaUbicacionDetPP = IIf(IsDBNull(dr.Item("IdReglaUbicacionDetPP")), 0, dr.Item("IdReglaUbicacionDetPP"))
                .IdReglaUbicacionEnc = IIf(IsDBNull(dr.Item("IdReglaUbicacionEnc")), 0, dr.Item("IdReglaUbicacionEnc"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeRegla_ubic_det_pp As clsBeRegla_ubic_det_pp, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("regla_ubic_det_pp")
            Ins.Add("idreglaubicaciondetpp", "@idreglaubicaciondetpp", DataType.Parametro)
            Ins.Add("idreglaubicacionenc", "@idreglaubicacionenc", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDREGLAUBICACIONDETPP", oBeRegla_ubic_det_pp.IdReglaUbicacionDetPP))
            cmd.Parameters.Add(New SqlParameter("@IDREGLAUBICACIONENC", oBeRegla_ubic_det_pp.IdReglaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeRegla_ubic_det_pp.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeRegla_ubic_det_pp.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeRegla_ubic_det_pp.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeRegla_ubic_det_pp.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeRegla_ubic_det_pp.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeRegla_ubic_det_pp.Fec_mod))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeRegla_ubic_det_pp As clsBeRegla_ubic_det_pp, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("regla_ubic_det_pp")
            Upd.Add("idreglaubicaciondetpp", "@idreglaubicaciondetpp", DataType.Parametro)
            Upd.Add("idreglaubicacionenc", "@idreglaubicacionenc", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdReglaUbicacionDetPP = @IdReglaUbicacionDetPP")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDREGLAUBICACIONDETPP", oBeRegla_ubic_det_pp.IdReglaUbicacionDetPP))
            cmd.Parameters.Add(New SqlParameter("@IDREGLAUBICACIONENC", oBeRegla_ubic_det_pp.IdReglaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeRegla_ubic_det_pp.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeRegla_ubic_det_pp.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeRegla_ubic_det_pp.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeRegla_ubic_det_pp.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeRegla_ubic_det_pp.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeRegla_ubic_det_pp.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Try
                cmd.CommandText = "DELETE FROM regla_ubic_det_pp WHERE Activo=0"
                cmd.ExecuteNonQuery()
            Catch ex As Exception

            End Try


            cmd.Dispose()

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


    Public Shared Function Eliminar(ByRef oBeRegla_ubic_det_pp As clsBeRegla_ubic_det_pp, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Regla_ubic_det_pp" &
             "  Where(IdReglaUbicacionDetPP = @IdReglaUbicacionDetPP)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDREGLAUBICACIONDETPP", oBeRegla_ubic_det_pp.IdReglaUbicacionDetPP))

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

            Const sp As String = " Delete from Regla_ubic_det_pp"
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

            Const sp As String = "SELECT * FROM Regla_ubic_det_pp"
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

    Public Shared Function Obtener(ByRef oBeRegla_ubic_det_pp As clsBeRegla_ubic_det_pp) As Boolean

        Try

            Const sp As String = "SELECT * FROM Regla_ubic_det_pp" & _
            " Where(IdReglaUbicacionDetPP = @IdReglaUbicacionDetPP)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAUBICACIONDETPP", oBeRegla_ubic_det_pp.IDREGLAUBICACIONDETPP))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeRegla_ubic_det_pp, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeRegla_ubic_det_pp)

        Try

            Dim lReturnList As New List(Of clsBeRegla_ubic_det_pp)
            Const sp As String = "SELECT * FROM Regla_ubic_det_pp"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubic_det_pp As New clsBeRegla_ubic_det_pp

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubic_det_pp = New clsBeRegla_ubic_det_pp
                Cargar(vBeRegla_ubic_det_pp, dr)
                lReturnList.Add(vBeRegla_ubic_det_pp)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeRegla_ubic_det_pp As clsBeRegla_ubic_det_pp)

        Try

            Const sp As String = "SELECT * FROM Regla_ubic_det_pp" & _
            " Where(IdReglaUbicacionDetPP = @IdReglaUbicacionDetPP)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAUBICACIONDETPP", pBeRegla_ubic_det_pp.IDREGLAUBICACIONDETPP))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeRegla_ubic_det_pp, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdReglaUbicacionDetPP),0) FROM Regla_ubic_det_pp"

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
