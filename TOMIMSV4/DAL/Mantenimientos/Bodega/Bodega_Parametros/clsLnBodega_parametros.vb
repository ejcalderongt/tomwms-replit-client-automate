Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnBodega_parametros

    Public Shared Sub Cargar(ByRef oBeBodega_parametros As clsBeBodega_parametros, ByRef dr As DataRow)
        Try
            With oBeBodega_parametros
                .IdParametroBodega = IIf(IsDBNull(dr.Item("IdParametroBodega")), 0, dr.Item("IdParametroBodega"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Function Insertar(ByRef oBeBodega_parametros As clsBeBodega_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("bodega_parametros")
            Ins.Add("idparametrobodega", "@idparametrobodega", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
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


            cmd.Parameters.Add(New SqlParameter("@IDPARAMETROBODEGA", oBeBodega_parametros.IdParametroBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega_parametros.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeBodega_parametros.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_parametros.Descripcion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

            oBeBodega_parametros.IdParametroBodega = CInt(cmd.Parameters("@IDPARAMETROBODEGA").Value)

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeBodega_parametros As clsBeBodega_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("bodega_parametros")
            Upd.Add("idparametrobodega", "@idparametrobodega", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Where("IdParametroBodega = @IdParametroBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETROBODEGA", oBeBodega_parametros.IdParametroBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega_parametros.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeBodega_parametros.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_parametros.Descripcion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeBodega_parametros As clsBeBodega_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Bodega_parametros" &
             "  Where(IdParametroBodega = @IdParametroBodega)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETROBODEGA", oBeBodega_parametros.IdParametroBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            lConnection.Dispose
            cmd.Dispose
        End Try
    End Function

    Public Shared Function Obtener(ByRef oBeBodega_parametros As clsBeBodega_parametros) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Bodega_parametros" &
            " Where(IdParametroBodega = @IdParametroBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPARAMETROBODEGA", oBeBodega_parametros.IdParametroBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_parametros, dt.Rows(0))
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

End Class
