Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnAjuste_motivo

    Public Shared Function GetAllForCombo() As DataTable

        Try

            Const sp As String = "select idmotivoajuste, nombre from ajuste_motivo where sistema = 1 and activo = 1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    ' Funcionalidad:
    '   Inserta un motivo de ajuste si NO existe ya por nombre (idempotente).
    '   Como la PK no es IDENTITY, calcula MAX(idmotivoajuste) + 1.
    '   Devuelve el IdMotivoAjuste resultante (insertado o existente).
    '   Devuelve 0 si hubo error.
    '=============================================================================

    ''' <summary>
    ''' Inserta un motivo de ajuste con el nombre dado, solo si no existe.
    ''' </summary>
    ''' <param name="nombre">Nombre del motivo (ej: ENTRADA, SALIDA, INVENTARIO).</param>
    ''' <returns>IdMotivoAjuste insertado o existente; 0 en caso de error.</returns>
    Public Shared Function Insertar_Motivo_Si_No_Existe(ByVal nombre As String, ByVal Usuario As String) As Integer

        If String.IsNullOrWhiteSpace(nombre) Then Return 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim resultId As Integer = 0

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.Serializable)

            Dim nombreNorm As String = nombre.Trim().ToUpper()

            ' 1) Verificar si ya existe por nombre exacto (case-insensitive)
            Const sqlCheck As String =
            "SELECT TOP 1 idmotivoajuste " &
            "FROM dbo.ajuste_motivo WITH (UPDLOCK, HOLDLOCK) " &
            "WHERE UPPER(nombre) = @Nombre"

            Using cmdCheck As New SqlCommand(sqlCheck, lConnection, lTransaction)
                cmdCheck.Parameters.AddWithValue("@Nombre", nombreNorm)
                Dim obj As Object = cmdCheck.ExecuteScalar()
                If obj IsNot Nothing AndAlso obj IsNot DBNull.Value Then
                    resultId = CInt(obj)
                    lTransaction.Commit()
                    Return resultId
                End If
            End Using

            ' 2) Calcular el siguiente IdMotivoAjuste (PK no es IDENTITY)
            Const sqlMax As String =
            "SELECT ISNULL(MAX(idmotivoajuste), 0) + 1 FROM dbo.ajuste_motivo"

            Dim nuevoId As Integer
            Using cmdMax As New SqlCommand(sqlMax, lConnection, lTransaction)
                nuevoId = CInt(cmdMax.ExecuteScalar())
            End Using

            ' 3) Insertar el motivo nuevo
            Const sqlIns As String =
            "INSERT INTO dbo.ajuste_motivo " &
            "(idmotivoajuste, nombre, fec_agr, user_agr, fec_mod, user_mod, activo, sistema) " &
            "VALUES (@Id, @Nombre, @Now, @User, @Now, @User, 1, 0)"

            Using cmdIns As New SqlCommand(sqlIns, lConnection, lTransaction)
                cmdIns.Parameters.AddWithValue("@Id", nuevoId)
                cmdIns.Parameters.AddWithValue("@Nombre", nombre.Trim())
                cmdIns.Parameters.AddWithValue("@Now", DateTime.Now)
                cmdIns.Parameters.AddWithValue("@User", If(Usuario IsNot Nothing,
                                                       Usuario,
                                                       "SISTEMA"))
                cmdIns.ExecuteNonQuery()
            End Using

            lTransaction.Commit()
            Return nuevoId

        Catch ex As Exception
            If lTransaction IsNot Nothing Then
                Try
                    lTransaction.Rollback()
                Catch
                End Try
            End If
            clsLnLog_error_wms.Agregar_Error("Insertar_Motivo_Si_No_Existe(" & nombre & "): " & ex.Message)
            Return 0
        Finally
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function


End Class
