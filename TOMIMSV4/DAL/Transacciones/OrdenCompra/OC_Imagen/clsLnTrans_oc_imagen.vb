Imports System.Data.SqlClient

Public Class clsLnTrans_oc_imagen

    Public Shared Sub Cargar(ByRef oBeTrans_oc_imagen As clsBeTrans_oc_imagen, ByRef dr As DataRow)
        Try
            With oBeTrans_oc_imagen
                .IdOrdenCompraImg = IIf(IsDBNull(dr.Item("IdOrdenCompraImg")), 0, dr.Item("IdOrdenCompraImg"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .Orden = IIf(IsDBNull(dr.Item("Orden")), 0, dr.Item("Orden"))
                .Imagen = IIf(IsDBNull(dr.Item("Imagen")), "", dr.Item("Imagen"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Insertar(ByRef oBeTrans_oc_imagen As clsBeTrans_oc_imagen, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_oc_imagen")
            Ins.Add("idordencompraimg", "@idordencompraimg", DataType.Parametro)
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("orden", "@orden", DataType.Parametro)
            Ins.Add("imagen", "@imagen", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAIMG", oBeTrans_oc_imagen.IdOrdenCompraImg))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_imagen.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@ORDEN", oBeTrans_oc_imagen.Orden))

            If oBeTrans_oc_imagen.Imagen IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeTrans_oc_imagen.Imagen))
            Else
                cmd.Parameters.Add(New SqlParameter("@IMAGEN", SqlDbType.Image)).Value = DBNull.Value
            End If

            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_oc_imagen.Descripcion))

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

    Public Function Actualizar(ByRef oBeTrans_oc_imagen As clsBeTrans_oc_imagen, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_oc_imagen")
            Upd.Add("idordencompraimg", "@idordencompraimg", DataType.Parametro)
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("orden", "@orden", DataType.Parametro)
            Upd.Add("imagen", "@imagen", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Where("IdOrdenCompraImg = @IdOrdenCompraImg " &
                "AND IdOrdenCompraEnc = @IdOrdenCompraEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAIMG", oBeTrans_oc_imagen.IdOrdenCompraImg))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_imagen.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@ORDEN", oBeTrans_oc_imagen.Orden))

            If oBeTrans_oc_imagen.Imagen IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeTrans_oc_imagen.Imagen))
            Else
                cmd.Parameters.Add(New SqlParameter("@IMAGEN", SqlDbType.Image)).Value = DBNull.Value
            End If

            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_oc_imagen.Descripcion))

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

    Public Function Eliminar(ByRef oBeTrans_oc_imagen As clsBeTrans_oc_imagen, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_oc_imagen" &
             "  Where(IdOrdenCompraImg = @IdOrdenCompraImg) " &
             "  AND (IdOrdenCompraEnc = @IdOrdenCompraEnc)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAIMG", oBeTrans_oc_imagen.IdOrdenCompraImg))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_imagen.IdOrdenCompraEnc))

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

    Public Function Obtener(ByRef oBeTrans_oc_imagen As clsBeTrans_oc_imagen) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_oc_imagen" &
            " Where(IdOrdenCompraImg = @IdOrdenCompraImg) " &
            "AND (IdOrdenCompraEnc = @IdOrdenCompraEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAIMG", oBeTrans_oc_imagen.IdOrdenCompraImg))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_imagen.IdOrdenCompraEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_oc_imagen, dt.Rows(0))
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
