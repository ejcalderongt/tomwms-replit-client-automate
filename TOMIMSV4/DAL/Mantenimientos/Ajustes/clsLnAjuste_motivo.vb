Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Utils.BindToTypePolicy

Public Class clsLnAjuste_motivo

    Public Shared Sub Cargar(ByRef oBeAjuste_motivo As clsBeAjuste_motivo, ByRef dr As DataRow)
        Try
            With oBeAjuste_motivo
                .Idmotivoajuste = IIf(IsDBNull(dr.Item("idmotivoajuste")), 0, dr.Item("idmotivoajuste"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Function Insertar(ByRef oBeAjuste_motivo As clsBeAjuste_motivo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("ajuste_motivo")
            Ins.Add("idmotivoajuste", "@idmotivoajuste", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOAJUSTE", oBeAjuste_motivo.Idmotivoajuste))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeAjuste_motivo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeAjuste_motivo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeAjuste_motivo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeAjuste_motivo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeAjuste_motivo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeAjuste_motivo.Activo))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeAjuste_motivo.Sistema))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeAjuste_motivo.Idmotivoajuste = CInt(cmd.Parameters("@IDMOTIVOAJUSTE").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeAjuste_motivo As clsBeAjuste_motivo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("ajuste_motivo")
            Upd.Add("idmotivoajuste", "@idmotivoajuste", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Where("idmotivoajuste = @idmotivoajuste")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOAJUSTE", oBeAjuste_motivo.Idmotivoajuste))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeAjuste_motivo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeAjuste_motivo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeAjuste_motivo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeAjuste_motivo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeAjuste_motivo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeAjuste_motivo.Activo))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeAjuste_motivo.Sistema))

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
    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Ajuste_motivo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeAjuste_motivo)

        Try

            Dim lReturnList As New List(Of clsBeAjuste_motivo)
            Dim sp As String = "SELECT * FROM Ajuste_motivo where activo ="
            If (pActivo) Then
                sp = sp + "1"
            Else
                sp = sp + "0"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeAjuste_motivo As New clsBeAjuste_motivo

            For Each dr As DataRow In dt.Rows
                vBeAjuste_motivo = New clsBeAjuste_motivo
                Cargar(vBeAjuste_motivo, dr)
                lReturnList.Add(vBeAjuste_motivo)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idmotivoajuste),0) FROM Ajuste_motivo"

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

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Listar(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Const sp As String = "SELECT * FROM Ajuste_motivo"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Obtiene el ID del motivo de ajuste por su c�digo o nombre
    ''' </summary>
    ''' <param name="motivoTexto">C�digo o nombre del motivo (ENT, SAL, INV, MER, DEV, CAM, OTR, etc.)</param>
    ''' <returns>ID del motivo, o 0 si no se encuentra</returns>
    Public Shared Function Get_IdMotivo_By_Codigo(motivoTexto As String) As Integer
        If String.IsNullOrWhiteSpace(motivoTexto) Then
            Return 0
        End If

        Dim idMotivo As Integer = 0
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try
            ' Limpiar y normalizar el texto de b�squeda
            Dim textoBusqueda As String = motivoTexto.Trim().ToUpper()

            ' Mapeo de c�digos comunes por si la tabla no tiene los valores exactos
            Dim codigosConocidos As New Dictionary(Of String, String) From {
            {"ENT", "ENTRADA"},
            {"SAL", "SALIDA"},
            {"INV", "INVENTARIO"},
            {"MER", "MERMA"},
            {"DEV", "DEVOLUCION"},
            {"CAM", "CAMBIO"},
            {"OTR", "OTRO"}
        }

            ' Si el c�digo est� en el diccionario, buscar tambi�n por el nombre completo
            Dim textoAlternativo As String = Nothing
            If codigosConocidos.ContainsKey(textoBusqueda) Then
                textoAlternativo = codigosConocidos(textoBusqueda)
            End If

            Dim vSQL As String = "
            SELECT IdMotivoAjuste
            FROM ajuste_motivo WITH (NOLOCK)
            WHERE activo = 1
                AND (
                    UPPER(nombre) = @MotivoTexto
                    OR UPPER(nombre) LIKE @MotivoTextoPattern
                    OR (@MotivoAlternativo IS NOT NULL AND UPPER(nombre) = @MotivoAlternativo)
                )"

            Using cmd As New SqlCommand(vSQL, lConnection)
                cmd.Parameters.AddWithValue("@MotivoTexto", textoBusqueda)
                cmd.Parameters.AddWithValue("@MotivoTextoPattern", $"%{textoBusqueda}%")
                cmd.Parameters.AddWithValue("@MotivoAlternativo", If(textoAlternativo IsNot Nothing, textoAlternativo, DBNull.Value))

                lConnection.Open()
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                    idMotivo = Convert.ToInt32(result)
                Else
                    ' Si no se encontr�, registrar advertencia
                    clsLnLog_error_wms.Agregar_Error($"Motivo no encontrado: {motivoTexto}")
                End If
            End Using

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error($"Error en Get_IdMotivo_By_Codigo: {ex.Message}")
            Throw
        Finally
            If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If
        End Try

        Return idMotivo
    End Function
End Class
