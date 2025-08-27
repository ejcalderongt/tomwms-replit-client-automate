Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTipo_etiqueta_detalle

    Public Shared Sub Cargar(ByRef oBeTipo_etiqueta_detalle As clsBeTipo_etiqueta_detalle, ByRef dr As DataRow)
        Try
            With oBeTipo_etiqueta_detalle
                .IdTipoEtiquetaDetalle = IIf(IsDBNull(dr.Item("IdTipoEtiquetaDetalle")), 0, dr.Item("IdTipoEtiquetaDetalle"))
                .IdTipoEtiqueta = IIf(IsDBNull(dr.Item("IdTipoEtiqueta")), 0, dr.Item("IdTipoEtiqueta"))
                .Orden = IIf(IsDBNull(dr.Item("orden")), 0, dr.Item("orden"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Campo = IIf(IsDBNull(dr.Item("campo")), "", dr.Item("campo"))
                .Coor_x = IIf(IsDBNull(dr.Item("coor_x")), 0, dr.Item("coor_x"))
                .Coor_y = IIf(IsDBNull(dr.Item("coor_y")), 0, dr.Item("coor_y"))
                .Width = IIf(IsDBNull(dr.Item("width")), 0, dr.Item("width"))
                .Height = IIf(IsDBNull(dr.Item("height")), 0, dr.Item("height"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTipo_etiqueta_detalle As clsBeTipo_etiqueta_detalle, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tipo_etiqueta_detalle")
            Ins.Add("idtipoetiquetadetalle", "@idtipoetiquetadetalle", DataType.Parametro)
            Ins.Add("idtipoetiqueta", "@idtipoetiqueta", DataType.Parametro)
            Ins.Add("orden", "@orden", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("campo", "@campo", DataType.Parametro)
            Ins.Add("coor_x", "@coor_x", DataType.Parametro)
            Ins.Add("coor_y", "@coor_y", DataType.Parametro)
            Ins.Add("width", "@width", DataType.Parametro)
            Ins.Add("height", "@height", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETADETALLE", oBeTipo_etiqueta_detalle.IdTipoEtiquetaDetalle))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeTipo_etiqueta_detalle.IdTipoEtiqueta))
            cmd.Parameters.Add(New SqlParameter("@ORDEN", oBeTipo_etiqueta_detalle.Orden))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTipo_etiqueta_detalle.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CAMPO", oBeTipo_etiqueta_detalle.Campo))
            cmd.Parameters.Add(New SqlParameter("@COOR_X", oBeTipo_etiqueta_detalle.Coor_x))
            cmd.Parameters.Add(New SqlParameter("@COOR_Y", oBeTipo_etiqueta_detalle.Coor_y))
            cmd.Parameters.Add(New SqlParameter("@WIDTH", oBeTipo_etiqueta_detalle.Width))
            cmd.Parameters.Add(New SqlParameter("@HEIGHT", oBeTipo_etiqueta_detalle.Height))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTipo_etiqueta_detalle As clsBeTipo_etiqueta_detalle, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tipo_etiqueta_detalle")
            Upd.Add("idtipoetiquetadetalle", "@idtipoetiquetadetalle", DataType.Parametro)
            Upd.Add("idtipoetiqueta", "@idtipoetiqueta", DataType.Parametro)
            Upd.Add("orden", "@orden", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("campo", "@campo", DataType.Parametro)
            Upd.Add("coor_x", "@coor_x", DataType.Parametro)
            Upd.Add("coor_y", "@coor_y", DataType.Parametro)
            Upd.Add("width", "@width", DataType.Parametro)
            Upd.Add("height", "@height", DataType.Parametro)
            Upd.Where("IdTipoEtiquetaDetalle = @IdTipoEtiquetaDetalle")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETADETALLE", oBeTipo_etiqueta_detalle.IdTipoEtiquetaDetalle))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeTipo_etiqueta_detalle.IdTipoEtiqueta))
            cmd.Parameters.Add(New SqlParameter("@ORDEN", oBeTipo_etiqueta_detalle.Orden))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTipo_etiqueta_detalle.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CAMPO", oBeTipo_etiqueta_detalle.Campo))
            cmd.Parameters.Add(New SqlParameter("@COOR_X", oBeTipo_etiqueta_detalle.Coor_x))
            cmd.Parameters.Add(New SqlParameter("@COOR_Y", oBeTipo_etiqueta_detalle.Coor_y))
            cmd.Parameters.Add(New SqlParameter("@WIDTH", oBeTipo_etiqueta_detalle.Width))
            cmd.Parameters.Add(New SqlParameter("@HEIGHT", oBeTipo_etiqueta_detalle.Height))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeTipo_etiqueta_detalle As clsBeTipo_etiqueta_detalle, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tipo_etiqueta_detalle" & _
             "  Where(IdTipoEtiquetaDetalle = @IdTipoEtiquetaDetalle)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETADETALLE", oBeTipo_etiqueta_detalle.IdTipoEtiquetaDetalle))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Tipo_etiqueta_detalle"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTipo_etiqueta_detalle)

        Dim lReturnList As New List(Of clsBeTipo_etiqueta_detalle)

        Try

            Const sp As String = "SELECT * FROM Tipo_etiqueta_detalle"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTipo_etiqueta_detalle As New clsBeTipo_etiqueta_detalle

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTipo_etiqueta_detalle = New clsBeTipo_etiqueta_detalle()
                            Cargar(vBeTipo_etiqueta_detalle, dr)
                            lReturnList.Add(vBeTipo_etiqueta_detalle)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTipo_etiqueta_detalle As clsBeTipo_etiqueta_detalle)

        Try

            Const sp As String = "SELECT * FROM Tipo_etiqueta_detalle" & _
            " Where(IdTipoEtiquetaDetalle = @IdTipoEtiquetaDetalle)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTipo_etiqueta_detalle As New clsBeTipo_etiqueta_detalle

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTipo_etiqueta_detalle, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTipoEtiquetaDetalle),0) FROM Tipo_etiqueta_detalle"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_By_TipoEtiqueta(ByVal pIdTipoEtiqueta As Integer) As List(Of clsBeTipo_etiqueta_detalle)

        Dim lReturnList As New List(Of clsBeTipo_etiqueta_detalle)

        Try

            Const sp As String = "SELECT * FROM Tipo_etiqueta_detalle WHERE IdTipoEtiqueta = @IdTipoEtiqueta"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoEtiqueta", pIdTipoEtiqueta)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTipo_etiqueta_detalle As New clsBeTipo_etiqueta_detalle

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTipo_etiqueta_detalle = New clsBeTipo_etiqueta_detalle()
                            Cargar(vBeTipo_etiqueta_detalle, dr)
                            lReturnList.Add(vBeTipo_etiqueta_detalle)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
