Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTipo_contenedor

    Public Shared Sub Cargar(ByRef oBeTipo_contenedor As clsBeTipo_contenedor, ByRef dr As DataRow)
        Try
            With oBeTipo_contenedor
                .IdTipoContenedor = IIf(IsDBNull(dr.Item("IdTipoContenedor")), 0, dr.Item("IdTipoContenedor"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Largo = IIf(IsDBNull(dr.Item("Largo")), 0.0, dr.Item("Largo"))
                .Ancho = IIf(IsDBNull(dr.Item("Ancho")), 0.0, dr.Item("Ancho"))
                .Alto = IIf(IsDBNull(dr.Item("Alto")), 0.0, dr.Item("Alto"))
                .Pies = IIf(IsDBNull(dr.Item("Pies")), 0.0, dr.Item("Pies"))
                .Tonealadas = IIf(IsDBNull(dr.Item("Tonealadas")), 0.0, dr.Item("Tonealadas"))
                .VolumenUtil = IIf(IsDBNull(dr.Item("VolumenUtil")), 0.0, dr.Item("VolumenUtil"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Tara = IIf(IsDBNull(dr.Item("Tara")), 0.0, dr.Item("Tara"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTipo_contenedor As clsBeTipo_contenedor, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tipo_contenedor")
            Ins.Add("idtipocontenedor", "@idtipocontenedor", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("pies", "@pies", DataType.Parametro)
            Ins.Add("tonealadas", "@tonealadas", DataType.Parametro)
            Ins.Add("volumenutil", "@volumenutil", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
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


            cmd.Parameters.Add(New SqlParameter("@IDTIPOCONTENEDOR", oBeTipo_contenedor.IdTipoContenedor))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTipo_contenedor.Nombre))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeTipo_contenedor.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeTipo_contenedor.Ancho))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeTipo_contenedor.Alto))
            cmd.Parameters.Add(New SqlParameter("@PIES", oBeTipo_contenedor.Pies))
            cmd.Parameters.Add(New SqlParameter("@TONEALADAS", oBeTipo_contenedor.Tonealadas))
            cmd.Parameters.Add(New SqlParameter("@VOLUMENUTIL", oBeTipo_contenedor.VolumenUtil))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTipo_contenedor.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTipo_contenedor.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTipo_contenedor.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTipo_contenedor.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTipo_contenedor.Activo))
            cmd.Parameters.Add(New SqlParameter("@TARA", oBeTipo_contenedor.Tara))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTipo_contenedor.IdTipoContenedor = CInt(cmd.Parameters("@IDTIPOCONTENEDOR").Value)

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

    Public Shared Function Actualizar(ByRef oBeTipo_contenedor As clsBeTipo_contenedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tipo_contenedor")
            Upd.Add("idtipocontenedor", "@idtipocontenedor", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("pies", "@pies", DataType.Parametro)
            Upd.Add("tonealadas", "@tonealadas", DataType.Parametro)
            Upd.Add("volumenutil", "@volumenutil", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("tara", "@tara", DataType.Parametro)
            Upd.Where("IdTipoContenedor = @IdTipoContenedor")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDTIPOCONTENEDOR", oBeTipo_contenedor.IdTipoContenedor))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTipo_contenedor.Nombre))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeTipo_contenedor.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeTipo_contenedor.Ancho))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeTipo_contenedor.Alto))
            cmd.Parameters.Add(New SqlParameter("@PIES", oBeTipo_contenedor.Pies))
            cmd.Parameters.Add(New SqlParameter("@TONEALADAS", oBeTipo_contenedor.Tonealadas))
            cmd.Parameters.Add(New SqlParameter("@VOLUMENUTIL", oBeTipo_contenedor.VolumenUtil))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTipo_contenedor.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTipo_contenedor.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTipo_contenedor.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTipo_contenedor.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTipo_contenedor.Activo))
            cmd.Parameters.Add(New SqlParameter("@TARA", oBeTipo_contenedor.Tara))


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


    Public Shared Function Eliminar(ByRef oBeTipo_contenedor As clsBeTipo_contenedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tipo_contenedor" &
             "  Where(IdTipoContenedor = @IdTipoContenedor)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDTIPOCONTENEDOR", oBeTipo_contenedor.IdTipoContenedor))

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

            Const sp As String = " Delete from Tipo_contenedor"
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

            Const sp As String = "SELECT * FROM Tipo_contenedor"
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

    Public Shared Function Obtener(ByRef oBeTipo_contenedor As clsBeTipo_contenedor) As Boolean

        Try

            Const sp As String = "SELECT * FROM Tipo_contenedor" & _
            " Where(IdTipoContenedor = @IdTipoContenedor)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOCONTENEDOR", oBeTipo_contenedor.IDTIPOCONTENEDOR))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTipo_contenedor, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeTipo_contenedor)

        Try

            Dim lReturnList As New List(Of clsBeTipo_contenedor)
            Const sp As String = "SELECT * FROM Tipo_contenedor"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTipo_contenedor As New clsBeTipo_contenedor

            For Each dr As DataRow In dt.Rows

                vBeTipo_contenedor = New clsBeTipo_contenedor
                Cargar(vBeTipo_contenedor, dr)
                lReturnList.Add(vBeTipo_contenedor)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTipo_contenedor As clsBeTipo_contenedor)

        Try

            Const sp As String = "SELECT * FROM Tipo_contenedor" & _
            " Where(IdTipoContenedor = @IdTipoContenedor)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOCONTENEDOR", pBeTipo_contenedor.IDTIPOCONTENEDOR))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTipo_contenedor, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdTipoContenedor),0) FROM Tipo_contenedor"

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
