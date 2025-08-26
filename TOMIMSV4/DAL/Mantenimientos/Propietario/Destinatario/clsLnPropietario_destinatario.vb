Imports System.Configuration
Imports System.Data.SqlClient

Public Class clsLnPropietario_destinatario

    Public Shared Sub Cargar(ByRef oBePropietario_destinatario As clsBePropietario_destinatario, ByRef dr As DataRow)
        Try

            With oBePropietario_destinatario
                .IdDestinatarioPropietario = IIf(IsDBNull(dr.Item("IdDestinatarioPropietario")), 0, dr.Item("IdDestinatarioPropietario"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Apellido = IIf(IsDBNull(dr.Item("apellido")), "", dr.Item("apellido"))
                .Correo_electronico = IIf(IsDBNull(dr.Item("correo_electronico")), "", dr.Item("correo_electronico"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Telefono1 = IIf(IsDBNull(dr.Item("telefono1")), "", dr.Item("telefono1"))
                .Cargo = IIf(IsDBNull(dr.Item("cargo")), "", dr.Item("cargo"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Insertar(ByRef oBePropietario_destinatario As clsBePropietario_destinatario,
                             Optional ByVal pConection As SqlConnection = Nothing,
                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("propietario_destinatario")
            Ins.Add("iddestinatariopropietario", "@iddestinatariopropietario", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("apellido", "@apellido", DataType.Parametro)
            Ins.Add("correo_electronico", "@correo_electronico", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("telefono1", "@telefono1", DataType.Parametro)
            Ins.Add("cargo", "@cargo", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDESTINATARIOPROPIETARIO", oBePropietario_destinatario.IdDestinatarioPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietario_destinatario.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBePropietario_destinatario.Nombre))
            cmd.Parameters.Add(New SqlParameter("@APELLIDO", oBePropietario_destinatario.Apellido))
            cmd.Parameters.Add(New SqlParameter("@CORREO_ELECTRONICO", oBePropietario_destinatario.Correo_electronico))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBePropietario_destinatario.Telefono))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO1", oBePropietario_destinatario.Telefono1))
            cmd.Parameters.Add(New SqlParameter("@CARGO", oBePropietario_destinatario.Cargo))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBePropietario_destinatario.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

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

    Public Function Actualizar(ByRef oBePropietario_destinatario As clsBePropietario_destinatario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("propietario_destinatario")
            Upd.Add("iddestinatariopropietario", "@iddestinatariopropietario", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("apellido", "@apellido", DataType.Parametro)
            Upd.Add("correo_electronico", "@correo_electronico", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("telefono1", "@telefono1", DataType.Parametro)
            Upd.Add("cargo", "@cargo", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdDestinatarioPropietario = @IdDestinatarioPropietario")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDESTINATARIOPROPIETARIO", oBePropietario_destinatario.IdDestinatarioPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietario_destinatario.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBePropietario_destinatario.Nombre))
            cmd.Parameters.Add(New SqlParameter("@APELLIDO", oBePropietario_destinatario.Apellido))
            cmd.Parameters.Add(New SqlParameter("@CORREO_ELECTRONICO", oBePropietario_destinatario.Correo_electronico))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBePropietario_destinatario.Telefono))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO1", oBePropietario_destinatario.Telefono1))
            cmd.Parameters.Add(New SqlParameter("@CARGO", oBePropietario_destinatario.Cargo))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBePropietario_destinatario.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

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

    Public Shared Function Eliminar(ByRef oBePropietario_destinatario As clsBePropietario_destinatario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Propietario_destinatario" &
             "  Where(IdDestinatarioPropietario = @IdDestinatarioPropietario)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDESTINATARIOPROPIETARIO", oBePropietario_destinatario.IdDestinatarioPropietario))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Propietario_destinatario"

            Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBePropietario_destinatario As clsBePropietario_destinatario) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Propietario_destinatario" &
            " Where(IdDestinatarioPropietario = @IdDestinatarioPropietario)"

            Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDESTINATARIOPROPIETARIO", oBePropietario_destinatario.IdDestinatarioPropietario))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBePropietario_destinatario, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
