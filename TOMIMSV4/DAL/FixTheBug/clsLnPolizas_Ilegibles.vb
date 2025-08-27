Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnPolizas_Ilegibles

    Public Shared Sub Cargar(ByRef oBePolizas_Ilegibles As clsBePolizas_Ilegibles, ByRef dr As DataRow)
        Try
            With oBePolizas_Ilegibles
                .No_poliza = IIf(IsDBNull(dr.Item("no_poliza")), "", dr.Item("no_poliza"))
                .Numero_orden = IIf(IsDBNull(dr.Item("numero_orden")), 0.0, dr.Item("numero_orden"))
                .Dua = IIf(IsDBNull(dr.Item("dua")), "", dr.Item("dua"))
                .Fecha_documento = IIf(IsDBNull(dr.Item("fecha_documento")), Date.Now, dr.Item("fecha_documento"))
                .Fecha_aceptación = IIf(IsDBNull(dr.Item("fecha_aceptación")), Date.Now, dr.Item("fecha_aceptación"))
                .Fecha_llegada = IIf(IsDBNull(dr.Item("fecha_llegada")), Date.Now, dr.Item("fecha_llegada"))
                .Tipo_cambio = IIf(IsDBNull(dr.Item("tipo_cambio")), 0.0, dr.Item("tipo_cambio"))
                .Regimen = IIf(IsDBNull(dr.Item("regimen")), "", dr.Item("regimen"))
                .Clase = IIf(IsDBNull(dr.Item("clase")), 0.0, dr.Item("clase"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBePolizas_Ilegibles As clsBePolizas_Ilegibles, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("polizas_ilegibles")
            Ins.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Ins.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Ins.Add("dua", "@dua", DataType.Parametro)
            Ins.Add("fecha_documento", "@fecha_documento", DataType.Parametro)
            Ins.Add("fecha_aceptación", "@fecha_aceptación", DataType.Parametro)
            Ins.Add("fecha_llegada", "@fecha_llegada", DataType.Parametro)
            Ins.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Ins.Add("regimen", "@regimen", DataType.Parametro)
            Ins.Add("clase", "@clase", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBePolizas_Ilegibles.No_poliza))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBePolizas_Ilegibles.Numero_orden))
            cmd.Parameters.Add(New SqlParameter("@DUA", oBePolizas_Ilegibles.Dua))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DOCUMENTO", oBePolizas_Ilegibles.Fecha_documento))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ACEPTACIÓN", oBePolizas_Ilegibles.Fecha_aceptación))
            cmd.Parameters.Add(New SqlParameter("@FECHA_LLEGADA", oBePolizas_Ilegibles.Fecha_llegada))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBePolizas_Ilegibles.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@REGIMEN", oBePolizas_Ilegibles.Regimen))
            cmd.Parameters.Add(New SqlParameter("@CLASE", oBePolizas_Ilegibles.Clase))

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

    Public Shared Function Actualizar(ByRef oBePolizas_Ilegibles As clsBePolizas_Ilegibles, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("polizas_ilegibles")
            Upd.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Upd.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Upd.Add("dua", "@dua", DataType.Parametro)
            Upd.Add("fecha_documento", "@fecha_documento", DataType.Parametro)
            Upd.Add("fecha_aceptación", "@fecha_aceptación", DataType.Parametro)
            Upd.Add("fecha_llegada", "@fecha_llegada", DataType.Parametro)
            Upd.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Upd.Add("regimen", "@regimen", DataType.Parametro)
            Upd.Add("clase", "@clase", DataType.Parametro)
            Upd.Add("clase", "@clase", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBePolizas_Ilegibles.No_poliza))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBePolizas_Ilegibles.Numero_orden))
            cmd.Parameters.Add(New SqlParameter("@DUA", oBePolizas_Ilegibles.Dua))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DOCUMENTO", oBePolizas_Ilegibles.Fecha_documento))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ACEPTACIÓN", oBePolizas_Ilegibles.Fecha_aceptación))
            cmd.Parameters.Add(New SqlParameter("@FECHA_LLEGADA", oBePolizas_Ilegibles.Fecha_llegada))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBePolizas_Ilegibles.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@REGIMEN", oBePolizas_Ilegibles.Regimen))
            cmd.Parameters.Add(New SqlParameter("@CLASE", oBePolizas_Ilegibles.Clase))

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


    Public Shared Function Eliminar(ByRef oBePolizas_Ilegibles As clsBePolizas_Ilegibles, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Polizas_Ilegibles"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


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

            Const sp As String = "SELECT * FROM Polizas_Ilegibles"
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

    Public Shared Function Get_All(ByVal lConnection As SqlConnection,
                                   ByVal lTransaction As SqlTransaction) As List(Of clsBePolizas_Ilegibles)

        Dim lReturnList As New List(Of clsBePolizas_Ilegibles)

        Try

            Const sp As String = "SELECT * FROM Polizas_Ilegibles"


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBePolizas_Ilegibles As New clsBePolizas_Ilegibles

                For Each dr As DataRow In lDataTable.Rows
                    vBePolizas_Ilegibles = New clsBePolizas_Ilegibles()
                    Cargar(vBePolizas_Ilegibles, dr)
                    lReturnList.Add(vBePolizas_Ilegibles)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBePolizas_Ilegibles As clsBePolizas_Ilegibles)

        Try

            Const sp As String = "SELECT * FROM Polizas_Ilegibles"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBePolizas_Ilegibles As New clsBePolizas_Ilegibles

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBePolizas_Ilegibles, lDataTable.Rows(0))
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

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM Polizas_Ilegibles"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
