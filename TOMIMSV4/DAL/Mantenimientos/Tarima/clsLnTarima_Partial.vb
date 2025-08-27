Imports System.Data.SqlClient

Partial Public Class clsLnTarimas

    'Public Function ActualizarEstado(ByRef oBeTarimas As clsBeTarimas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim cmd As New SqlCommand

    '    Try

    '        Upd.Init("tarimas")
    '        Upd.Add("idtarima", "@idtarima", DataType.Parametro)
    '        Upd.Add("disponible", "@disponible", DataType.Parametro)
    '        Upd.Where("IdTarima = @IdTarima")

    '        Dim sp As String = Upd.SQL()

    '        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

    '        cmd.CommandType = CommandType.Text

    '        If Es_Transaccion_Remota Then
    '            cmd = New SqlCommand(sp, pConection)
    '            cmd.Transaction = pTransaction
    '        Else
    '           cmd = New SqlCommand(sp, lConnection, lTransaction)

    '            lConnection.Open()
    '        End If

    '        cmd.Parameters.Add(New SqlParameter("@IDTARIMA", oBeTarimas.IdTarima))
    '        cmd.Parameters.Add(New SqlParameter("@DISPONIBLE", oBeTarimas.Disponible))

    '        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '        Return rowsAffected


    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        lConnection.Dispose()
    '        cmd.Dispose()
    '    End Try

    'End Function

    Public Shared Function Get_All_By_IdEmpresa(ByVal IdEmpresa As Integer) As List(Of clsBeTarimas)

        Try

            Dim lReturnList As New List(Of clsBeTarimas)
            Const sp As String = "SELECT * FROM Tarimas Where IdEmpresa= @IdEmpresa"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEmpresa", IdEmpresa))

            dad.Fill(dt)

            Dim vBeTarimas As New clsBeTarimas

            For Each dr As DataRow In dt.Rows

                vBeTarimas = New clsBeTarimas
                Cargar(vBeTarimas, dr)
                lReturnList.Add(vBeTarimas)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Tarimas_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT IdTarima AS Correlativo, Codigo AS Código FROM tarimas WHERE 1 > 0 "

            If pActivo Then
                sp += " AND activo=1"
            Else
                sp += " AND activo=0"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GuardarDatos(ByVal pObjList As List(Of clsBeTarimas)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GuardarDatos = False

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeTarimas In pObjList
                If Obj.IsNew Then
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()
            lConnection.Close()
            GuardarDatos = True

        Catch ex As Exception
            lTransaction.Rollback()
            lConnection.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdTarima As Integer) As Boolean

        Dim lExists As Boolean = False

        Dim vSQL As String = "SELECT COUNT(1) FROM tarimas WHERE IdTarima=@IdTarima"

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdTarima", pIdTarima)

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Sub GetAllTarimas(ByRef lv As ListView)

        Dim DT As New DataTable
        Dim i As Integer, vID$, vTitulo$, vCodigo$
        Dim Idx As Integer

        Try

            lv.Items.Clear()

            Dim vSQL As String = "SELECT a.IdTarima, a.codigo, b.Nombre from Tarimas a inner join Tipo_Tarima b on a.IdTarima = b.IdTipoTarima"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.Fill(DT)

            lv.BeginUpdate()

            If DT.Rows.Count > 0 Then

                For i = 0 To DT.Rows.Count - 1

                    vID$ = IIf(IsDBNull(DT.Rows(i).Item("IdTarima")), "", DT.Rows(i).Item("IdTarima")).ToString
                    vTitulo$ = IIf(IsDBNull(DT.Rows(i).Item("Nombre")), "", DT.Rows(i).Item("Nombre")).ToString
                    vCodigo$ = IIf(IsDBNull(DT.Rows(i).Item("codigo")), "", DT.Rows(i).Item("codigo")).ToString
                    lv.Items.Add(New ListViewItem({vID$}))

                    Idx = lv.Items.Count - 1
                    lv.Items(Idx).SubItems.Add(vCodigo$)
                    lv.Items(Idx).SubItems.Add(vTitulo$)

                Next

                If lv.Items.Count > 0 Then lv.Items(0).Selected = True

            End If

            lv.EndUpdate()

            DT.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub


    Public Shared Sub GetAllTarimasUsadas(ByRef lv As ListView, ByVal pIdTareaUbicEnc As Integer)

        Dim DT As New DataTable
        Dim i As Integer, vID$, vTitulo$, vCodigo$
        Dim Idx As Integer

        Try

            lv.Items.Clear()
            Dim vSQL As String = "SELECT * from [VW_TarimasUsadasEnTransaccion] Where IdTareaUbicacionEnc=" & pIdTareaUbicEnc

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.Fill(DT)

            lv.BeginUpdate()

            If DT.Rows.Count > 0 Then
                For i = 0 To DT.Rows.Count - 1
                    vID$ = IIf(IsDBNull(DT.Rows(i).Item("IdTarima")), "", DT.Rows(i).Item("IdTarima")).ToString
                    vTitulo$ = IIf(IsDBNull(DT.Rows(i).Item("nombreTipoTarima")), "", DT.Rows(i).Item("nombreTipoTarima")).ToString
                    vCodigo$ = IIf(IsDBNull(DT.Rows(i).Item("codigoTarima")), "", DT.Rows(i).Item("codigoTarima")).ToString
                    lv.Items.Add(New ListViewItem({vID$}))

                    Idx = lv.Items.Count - 1
                    lv.Items(Idx).SubItems.Add(vCodigo$)
                    lv.Items(Idx).SubItems.Add(vTitulo$)
                Next
                If lv.Items.Count > 0 Then lv.Items(0).Selected = True
            End If
            lv.EndUpdate()

            DT.Dispose()

            Application.DoEvents()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Shared Function ActualizarEstado(ByRef oBeTarimas As clsBeTarimas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("tarimas")
            Upd.Add("idtarima", "@idtarima", DataType.Parametro)
            Upd.Add("disponible", "@disponible", DataType.Parametro)
            Upd.Where("IdTarima = @IdTarima")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTARIMA", oBeTarimas.IdTarima))
            cmd.Parameters.Add(New SqlParameter("@DISPONIBLE", oBeTarimas.Disponible))

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

End Class
