Imports System.Data.SqlClient

Public Class clsLnTablas_relacionadas

    Public Shared Sub Cargar(ByRef oBeTablas_relacionadas As clsBeTablas_relacionadas, ByRef dr As DataRow)
        Try
            With oBeTablas_relacionadas
                .Tabla = IIf(IsDBNull(dr.Item("Tabla")), "", dr.Item("Tabla"))
                .Unidad = IIf(IsDBNull(dr.Item("unidad")), "", dr.Item("unidad"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Año = IIf(IsDBNull(dr.Item("año")), 0.0, dr.Item("año"))
                .Correlativo = IIf(IsDBNull(dr.Item("correlativo")), 0.0, dr.Item("correlativo"))
                .Turno = IIf(IsDBNull(dr.Item("turno")), 0.0, dr.Item("turno"))
                .fecha_orden_entrega = IIf(IsDBNull(dr.Item("fecha_orden_entrega")), Date.Now, dr.Item("fecha_orden_entrega"))
                .agente_aduanal = IIf(IsDBNull(dr.Item("agente_aduanal")), 0.0, dr.Item("agente_aduanal"))
                .Correlativo1 = IIf(IsDBNull(dr.Item("correlativo1")), 0.0, dr.Item("correlativo1"))
                .NoOrdenSalida = IIf(IsDBNull(dr.Item("noOrdenSalida")), "", dr.Item("noOrdenSalida"))
                .Coaniorec = IIf(IsDBNull(dr.Item("coaniorec")), 0.0, dr.Item("coaniorec"))
                .Covehiculo = IIf(IsDBNull(dr.Item("covehiculo")), "", dr.Item("covehiculo"))
                .Coplacas = IIf(IsDBNull(dr.Item("coplacas")), "", dr.Item("coplacas"))
                .Copoliza = IIf(IsDBNull(dr.Item("copoliza")), 0.0, dr.Item("copoliza"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Tmoriginal = IIf(IsDBNull(dr.Item("tmoriginal")), 0.0, dr.Item("tmoriginal"))
                .Tmsalidas = IIf(IsDBNull(dr.Item("tmsalidas")), 0.0, dr.Item("tmsalidas"))
                .Crfecha = IIf(IsDBNull(dr.Item("crfecha")), Date.Now, dr.Item("crfecha"))
                .Consignatario = IIf(IsDBNull(dr.Item("consignatario")), "", dr.Item("consignatario"))
                .Utilizada = IIf(IsDBNull(dr.Item("utilizada")), False, dr.Item("Utilizada"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTablas_relacionadas As clsBeTablas_relacionadas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tablas_relacionadas")
            Ins.Add("tabla", "@tabla", DataType.Parametro)
            Ins.Add("unidad", "@unidad", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("año", "@año", DataType.Parametro)
            Ins.Add("correlativo", "@correlativo", DataType.Parametro)
            Ins.Add("turno", "@turno", DataType.Parametro)
            Ins.Add("fecha_orden_entrega", "@fecha_orden_entrega", DataType.Parametro)
            Ins.Add("agente_aduanal", "@agente_aduanal", DataType.Parametro)
            Ins.Add("correlativo1", "@correlativo1", DataType.Parametro)
            Ins.Add("noordensalida", "@noordensalida", DataType.Parametro)
            Ins.Add("coaniorec", "@coaniorec", DataType.Parametro)
            Ins.Add("covehiculo", "@covehiculo", DataType.Parametro)
            Ins.Add("coplacas", "@coplacas", DataType.Parametro)
            Ins.Add("copoliza", "@copoliza", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("tmoriginal", "@tmoriginal", DataType.Parametro)
            Ins.Add("tmsalidas", "@tmsalidas", DataType.Parametro)
            Ins.Add("crfecha", "@crfecha", DataType.Parametro)
            Ins.Add("consignatario", "@consignatario", DataType.Parametro)
            Ins.Add("utilizada", "@utilizada", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@TABLA", oBeTablas_relacionadas.Tabla))
            cmd.Parameters.Add(New SqlParameter("@UNIDAD", oBeTablas_relacionadas.Unidad))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTablas_relacionadas.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTablas_relacionadas.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@AÑO", oBeTablas_relacionadas.Año))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeTablas_relacionadas.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@TURNO", oBeTablas_relacionadas.Turno))
            cmd.Parameters.Add(New SqlParameter("@fecha_orden_entrega", oBeTablas_relacionadas.fecha_orden_entrega))
            cmd.Parameters.Add(New SqlParameter("@agente_aduanal", oBeTablas_relacionadas.agente_aduanal))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO1", oBeTablas_relacionadas.Correlativo1))
            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas.NoOrdenSalida))
            cmd.Parameters.Add(New SqlParameter("@COANIOREC", oBeTablas_relacionadas.Coaniorec))
            cmd.Parameters.Add(New SqlParameter("@COVEHICULO", oBeTablas_relacionadas.Covehiculo))
            cmd.Parameters.Add(New SqlParameter("@COPLACAS", oBeTablas_relacionadas.Coplacas))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTablas_relacionadas.Observacion))
            cmd.Parameters.Add(New SqlParameter("@TMORIGINAL", oBeTablas_relacionadas.Tmoriginal))
            cmd.Parameters.Add(New SqlParameter("@TMSALIDAS", oBeTablas_relacionadas.Tmsalidas))
            cmd.Parameters.Add(New SqlParameter("@CRFECHA", oBeTablas_relacionadas.Crfecha))
            cmd.Parameters.Add(New SqlParameter("@CONSIGNATARIO", oBeTablas_relacionadas.Consignatario))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas.Utilizada))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTablas_relacionadas As clsBeTablas_relacionadas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tablas_relacionadas")
            Upd.Add("tabla", "@tabla", DataType.Parametro)
            Upd.Add("unidad", "@unidad", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("año", "@año", DataType.Parametro)
            Upd.Add("correlativo", "@correlativo", DataType.Parametro)
            Upd.Add("turno", "@turno", DataType.Parametro)
            Upd.Add("fecha_orden_entrega", "@fecha_orden_entrega", DataType.Parametro)
            Upd.Add("agente_aduanal", "@agente_aduanal", DataType.Parametro)
            Upd.Add("correlativo1", "@correlativo1", DataType.Parametro)
            Upd.Add("noordensalida", "@noordensalida", DataType.Parametro)
            Upd.Add("coaniorec", "@coaniorec", DataType.Parametro)
            Upd.Add("covehiculo", "@covehiculo", DataType.Parametro)
            Upd.Add("coplacas", "@coplacas", DataType.Parametro)
            Upd.Add("copoliza", "@copoliza", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("tmoriginal", "@tmoriginal", DataType.Parametro)
            Upd.Add("tmsalidas", "@tmsalidas", DataType.Parametro)
            Upd.Add("crfecha", "@crfecha", DataType.Parametro)
            Upd.Add("consignatario", "@consignatario", DataType.Parametro)
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@TABLA", oBeTablas_relacionadas.Tabla))
            cmd.Parameters.Add(New SqlParameter("@UNIDAD", oBeTablas_relacionadas.Unidad))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTablas_relacionadas.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTablas_relacionadas.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@AÑO", oBeTablas_relacionadas.Año))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeTablas_relacionadas.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@TURNO", oBeTablas_relacionadas.Turno))
            cmd.Parameters.Add(New SqlParameter("@fecha_orden_entrega", oBeTablas_relacionadas.fecha_orden_entrega))
            cmd.Parameters.Add(New SqlParameter("@agente_aduanal", oBeTablas_relacionadas.agente_aduanal))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO1", oBeTablas_relacionadas.Correlativo1))
            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas.NoOrdenSalida))
            cmd.Parameters.Add(New SqlParameter("@COANIOREC", oBeTablas_relacionadas.Coaniorec))
            cmd.Parameters.Add(New SqlParameter("@COVEHICULO", oBeTablas_relacionadas.Covehiculo))
            cmd.Parameters.Add(New SqlParameter("@COPLACAS", oBeTablas_relacionadas.Coplacas))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTablas_relacionadas.Observacion))
            cmd.Parameters.Add(New SqlParameter("@TMORIGINAL", oBeTablas_relacionadas.Tmoriginal))
            cmd.Parameters.Add(New SqlParameter("@TMSALIDAS", oBeTablas_relacionadas.Tmsalidas))
            cmd.Parameters.Add(New SqlParameter("@CRFECHA", oBeTablas_relacionadas.Crfecha))
            cmd.Parameters.Add(New SqlParameter("@CONSIGNATARIO", oBeTablas_relacionadas.Consignatario))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas.Utilizada))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTablas_relacionadas As clsBeTablas_relacionadas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tablas_relacionadas"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
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
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Cealsa() As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas WHERE TABLA = 'CEALSA' AND utilizada=0 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                            Cargar(vBeTablas_relacionadas, dr)
                            lReturnList.Add(vBeTablas_relacionadas)
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

    Public Shared Sub GetSingle(ByRef pBeTablas_relacionadas As clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTablas_relacionadas, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM Tablas_relacionadas"

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

    Public Shared Function Get_All_WMS() As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas WHERE TABLA = 'WMS' "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                            Cargar(vBeTablas_relacionadas, dr)
                            lReturnList.Add(vBeTablas_relacionadas)
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

    Public Shared Function Actualizar_Parcial(ByRef oBeTablas_relacionadas As clsBeTablas_relacionadas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tablas_relacionadas")
            Upd.Add("correlativo1", "@correlativo1", DataType.Parametro)
            Upd.Add("noordensalida", "@noordensalida", DataType.Parametro)
            Upd.Add("copoliza", "@copoliza", DataType.Parametro)
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)
            Upd.Where("agente_aduanal = @agente_aduanal and tabla = 'WMS' and correlativo1 = 0 ")
            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@FECHA_ORDEN_ENTREGA", oBeTablas_relacionadas.Fecha_orden_entrega))
            cmd.Parameters.Add(New SqlParameter("@AGENTE_ADUANAL", oBeTablas_relacionadas.Agente_aduanal))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO1", oBeTablas_relacionadas.Correlativo1))
            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas.NoOrdenSalida))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas.Utilizada))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Parcial_WMS(ByRef oBeTablas_relacionadas As clsBeTablas_relacionadas,
                                                  ByRef oBeTablaRelacionadaExistente As clsBeTablas_relacionadas,
                                                  Optional ByVal pConection As SqlConnection = Nothing,
                                                  Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tablas_relacionadas")
            Upd.Add("agente_aduanal", "@agente_aduanal", DataType.Parametro)
            Upd.Add("copoliza", "@copoliza", DataType.Parametro)
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)
            Upd.Where("agente_aduanal = @agente_aduanale and copoliza = @copolizae and tabla = 'WMS' and correlativo1 = 0 ")
            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@AGENTE_ADUANAL", oBeTablas_relacionadas.Agente_aduanal))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@AGENTE_ADUANALE", oBeTablaRelacionadaExistente.Agente_aduanal))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZAE", oBeTablaRelacionadaExistente.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", 0))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Parcial_CEALSA(ByRef oBeTablas_relacionadas As clsBeTablas_relacionadas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tablas_relacionadas")
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)
            Upd.Where("agente_aduanal = @agente_aduanal and tabla = 'CEALSA' and 
                       correlativo1 = @correlativo1 and noordensalida=@noordensalida and 
                       copoliza=@copoliza ")
            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@FECHA_ORDEN_ENTREGA", oBeTablas_relacionadas.Fecha_orden_entrega))
            cmd.Parameters.Add(New SqlParameter("@AGENTE_ADUANAL", oBeTablas_relacionadas.Agente_aduanal))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO1", oBeTablas_relacionadas.Correlativo1))
            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas.NoOrdenSalida))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas.Utilizada))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_By_Nombre_Similar(ByVal pNombre As String) As clsBeTablas_relacionadas

        Dim beResult As clsBeTablas_relacionadas = Nothing

        Try

            Dim sp As String = "Select * FROM Tablas_relacionadas WHERE tabla = 'CEALSA' AND (replace(descripcion,' ','')  like '%" & pNombre.Replace(" ", "") & "%'
                                Or '" & pNombre.Replace(" ", "") & "'   like '%' + replace(descripcion,' ','') + '%') AND utilizada = 0 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTablas_relacionadas, lDataTable.Rows(0))
                            beResult = vBeTablas_relacionadas
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return beResult

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Inicializar_Tablas()

        Dim vRegistrosAfectados As Integer = 0
        Dim vRegistrosTablasRelacionadas As Integer = 0

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

#Region "DELETE FROM tablas_relacionadas"

                    '#DELETE FROM tablas_relacionadas
                    Dim vSQL As String = "DELETE FROM tablas_relacionadas "
                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            vRegistrosAfectados = CInt(lReturnValue)
                        End If

                    End Using

#End Region

#Region "LLenar Tablas_Relacionadas"

                    '#LLenar Tablas_Relacionadas
                    vSQL = "INSERT INTO tablas_relacionadas
							select 'CEALSA' Tabla, *, 0
							from Polizas_CEALSA 
							WHERE [fecha orden entrega] BETWEEN '20230411' AND  '20230811'
							union 
							select 'WMS' Tabla, case when um.Nombre= 'bulto' then 'BULTOS' else um.Nombre end unidad, 
							REPLACE(REPLACE(p.nombre_producto,', S.A',''),', SA','') descripcion,sum(p.Cantidad) cantidad,DATEPART(year, CONVERT(date, p.fec_agr)) año, 0 correlativo,
							0 turno, CONVERT(date, p.fec_agr) fecha_orden_entrega, p.IdPedidoEnc agente_aduanal, 0 correlativo1, '' noOrdenSalida, 
							0 coaniorec, '' covehiculo, '' coplacas,0 copoliza,'' observacion, 0 tmoriginal, 0 tmsalidas, CONVERT(date, p.fec_agr) crfecha, 
							'' consignatario, 0
							from trans_pe_det p inner join unidad_medida um on p.IdUnidadMedidaBasica = um.IdUnidadMedida
							WHERE IdPedidoEnc in (select IdPedidoEnc from trans_pe_enc WHERE Fecha_Pedido BETWEEN '20230411' AND  '20230811' and IdBodega =  2)
							group by um.Nombre, p.nombre_producto,CONVERT(date, p.fec_agr),p.IdPedidoEnc
							order by descripcion "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            vRegistrosTablasRelacionadas = CInt(lReturnValue)
                        End If

                    End Using

#End Region

#Region "Disminuir la cantidad de caracteres por descripción para mejorar las coincidencias."

                    '#Disminuir la cantidad de caracteres por descripción para mejorar las coincidencias.
                    vSQL = "UPDATE tablas_relacionadas set descripcion = CASE WHEN LEN(descripcion)>=15 THEN SUBSTRING(descripcion,0,15) 
							ELSE SUBSTRING(descripcion,0,len(descripcion)) END 
							WHERE tabla = 'CEALSA'"

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            vRegistrosTablasRelacionadas = CInt(lReturnValue)
                        End If

                    End Using

#End Region

#Region "Establecer una bandera que sirva de punto de referencia para saber si a ese pedido ya se le encontró o no una póliza relacionada."

                    '#Establecer una bandera que sirva de punto de referencia para saber si a ese pedido ya se le encontró o no una póliza relacionada.
                    vSQL = "UPDATE trans_pe_enc set no_documento_externo = '' WHERE no_documento_externo <> ''"

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            vRegistrosTablasRelacionadas = CInt(lReturnValue)
                        End If

                    End Using

#End Region

#Region "Equiparar descripcíones, orden, valor WMS en tablas_relacionadas."

                    '#Equiparar descripcíones, orden, valor WMS en tablas_relacionadas.
                    vSQL = "update tablas_relacionadas set descripcion = 'A.T IMPRESOS' WHERE descripcion = 'A. T. IMPRESO';
							update tablas_relacionadas set descripcion = 'A.T IMPRESOS' WHERE descripcion = 'A.T. IMPRESO';
							update tablas_relacionadas set descripcion = 'BAYER S.A' WHERE descripcion = 'BAYER, SOCIEDA';
							update tablas_relacionadas set descripcion = 'BRANDS AND TRIMS' WHERE descripcion = 'BRANS AND TRIM';
							update tablas_relacionadas set descripcion = 'BRETANO GUATEMALA' WHERE descripcion = 'BRETENO GUATEM';
							update tablas_relacionadas set descripcion = 'COFIO STAHL Y COMPAA.' WHERE descripcion = 'COFIÑO STAH';
							update tablas_relacionadas set descripcion = 'CORDELES CODIGO 71274' WHERE descripcion = 'CORDELES Y ART';
							update tablas_relacionadas set descripcion = 'DISEOS GRAFICOS' WHERE descripcion = 'DISEÑOS GRAFIC';
							update tablas_relacionadas set descripcion = 'ELECTRICIDAD GENERAL GUATEMALA S.A' WHERE descripcion = 'ELECTRICIAD GE';
							update tablas_relacionadas set descripcion = 'FELIPE AGUSTO CARIAS ESTRADA' WHERE descripcion = 'FELIPE AUGUST';
							update tablas_relacionadas set descripcion = 'FERRETERIA LEWONSKI.' WHERE descripcion = 'FERRETARIA LOWONSKI';
							update tablas_relacionadas set descripcion = 'FERRETORNILLOS' WHERE descripcion = 'FERROTORNILLO';
							update tablas_relacionadas set descripcion = 'GARCIA, OSCAL,ALEMAN, ELIZABETH' WHERE descripcion = 'GARCIA OSCA';
							update tablas_relacionadas set descripcion = 'GARCIA, OSCAL, ALEMAN, GLADYS, ELIZABETH' WHERE descripcion = 'GARCIA OSCAL G';
							update tablas_relacionadas set descripcion = 'HOSSIER MANUFACTURING COMPAA LIMITADA' WHERE descripcion = 'HOSIER MANUFAC';
							update tablas_relacionadas set descripcion = 'INDUBRAS CENTROAMERICA' WHERE descripcion = 'INDRUMAS CENTR';
							update tablas_relacionadas set descripcion = 'INGIENERIA Y EQUIPOS INDUSTRIALES, SOCIEDAD ANONIMA' WHERE descripcion = 'INGENIERIA Y E';
							update tablas_relacionadas set descripcion = 'LABORATORIOS Y DROGUERIA DONOVAN WERKE A G' WHERE descripcion = 'LABORATORIO Y ' AND CANTIDAD = 6;
							update tablas_relacionadas set descripcion = 'LABORATORIOS Y DROGUERIA DONOVAN WERKE A G' WHERE descripcion = 'LABORATORIO Y ' AND CANTIDAD = 5;
							update tablas_relacionadas set descripcion = 'LIBRERA E IMPRENTA VIVIAN' WHERE descripcion = 'LIBRERIA E IMP' AND CANTIDAD = 8;
							update tablas_relacionadas set descripcion = 'LUIS ALEXANDER ESTACUY' WHERE descripcion = 'LUIS ESTACU' AND CANTIDAD = 5;
							update tablas_relacionadas set descripcion = 'M H AUTOPARTES.' WHERE descripcion = 'M&H AUTOPARTE' AND CANTIDAD = 716;
							update tablas_relacionadas set descripcion = 'MONTACARGAS RUIZ' WHERE descripcion = 'MONTACARGA RUI' ;
							update tablas_relacionadas set descripcion = 'MULTIVAC CENTROAMERICA Y CARIBE S.A' WHERE descripcion = 'MULTIVAC, S .' ;
							update tablas_relacionadas set descripcion = 'OPERADORES LOGISTICOS RANSA.' WHERE descripcion = 'OPERADORA LOGI'   AND CANTIDAD = 4 ;
							update tablas_relacionadas set descripcion = 'OFICINAS PRODUCTIVAS' WHERE descripcion = 'OFICINA PRODUC'   AND CORRELATIVO1 = '3708694' ;
							update tablas_relacionadas set descripcion = 'PETROSULUCIONES DE GUATEMALA' WHERE descripcion = 'PETROSOLUCIONE'   AND CORRELATIVO1 = '3707218' ;
							update tablas_relacionadas set descripcion = 'PLATINO S.A' WHERE descripcion = 'PLATINO, S.'   AND CORRELATIVO1 = '3701975' ;
							update tablas_relacionadas set descripcion = 'POLYMERIC POLYOL PURANOL' WHERE descripcion = 'POLYMERC POLYO'   AND CORRELATIVO1 = '3704576' ;
							update tablas_relacionadas set descripcion = 'RICZA S.A' WHERE descripcion = 'RICZA, SOCIEDA' ;
							update tablas_relacionadas set descripcion = 'RODIO EWISSBORING GUATEMALA' WHERE descripcion = 'RODIO SWINSSBO' ;
							update tablas_relacionadas set descripcion = 'RODRIGUEZ,,,,JAIME, ANDRES' WHERE descripcion = 'RODRIGUEZ,,,JA' ;
							update tablas_relacionadas set descripcion = 'SAN DIEGO S.A' WHERE descripcion = 'SAN DIEGO,S .' ;
							update tablas_relacionadas set descripcion = 'SAN DIEGO S.A' WHERE descripcion = 'SAN DIEGO, S.A' ;
							update tablas_relacionadas set descripcion = 'SECOGUA' WHERE descripcion = 'SERCOGU' ;
							update tablas_relacionadas set descripcion = 'SINOCERAS S.A' WHERE descripcion = 'SINOCERAS, SOC' ;
							update tablas_relacionadas set descripcion = 'STRONG SUPPLY GUATEMALA.' WHERE descripcion = 'STRONG SUPL' ;
							update tablas_relacionadas set descripcion = 'TECNOLOGIA LUBRICANTES' WHERE descripcion = 'TECNOLOGIA EN ' ;
							update tablas_relacionadas set descripcion = 'ZUCCHELLI ALPHA DE GUATEMALA S.A' WHERE descripcion = 'ZUCHELLI ALPH' ;
							update tablas_relacionadas set descripcion = 'CLARA HILDA MARIA, FALLA BIANCHI DE CRUZ-GOMAR' WHERE descripcion = 'CLARA BIANCH' ;
							update tablas_relacionadas set descripcion = 'COMPAA DE JARABES Y BEBIDAS GASEOSAS LA MARIPOSA, S.A' WHERE descripcion = 'COMPAÑIA DE JA' ;
							update tablas_relacionadas set descripcion = 'PECUARIA EXPORTADORA, S.A' WHERE descripcion = 'PECUERIA EXPOR' ;
							update tablas_relacionadas set descripcion = 'IMPORTADORA Y EXPORTADORA GUERRA RUANO, S.A' WHERE descripcion = 'IMPORTADORA GU' ;
							update tablas_relacionadas set descripcion = 'COMPAA FRANZ SERVICIOS COMERCIALES' WHERE descripcion = 'COMPAÑIA FRAN' ;
							update tablas_relacionadas set descripcion = 'DIRECT TO SOURCE GUATEMALA, S.A' WHERE descripcion = 'DIRET TO SOURC' ;
							update tablas_relacionadas set descripcion = 'INGIENERIA Y EQUIPOS INDUSTRIALES, SOCIEDAD' WHERE descripcion = 'INGENIERIA Y EQUIPOS INDUSTRIALES, S.A' ;"

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            vRegistrosTablasRelacionadas = CInt(lReturnValue)
                        End If

                    End Using

#End Region

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_By_Nombre_Similar(ByVal pNombre As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeTablas_relacionadas

        Dim beResult As clsBeTablas_relacionadas = Nothing

        Try

            Dim sp As String = "Select * FROM Tablas_relacionadas WHERE tabla = 'CEALSA' AND (replace(descripcion,' ','')  like '%" & pNombre.Replace(" ", "") & "%'
                                Or '" & pNombre.Replace(" ", "") & "'   like '%' + replace(descripcion,' ','') + '%') AND utilizada = 0 "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTablas_relacionadas, lDataTable.Rows(0))
                    beResult = vBeTablas_relacionadas
                End If

            End Using

            Return beResult

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Inicializar_Tablas(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim vRegistrosAfectados As Integer = 0
        Dim vRegistrosTablasRelacionadas As Integer = 0
        Dim vSQL As String = ""

        Try

#Region "Crear tabla tablas_relacionadas si no existe"

            vSQL = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tablas_relacionadas]') AND type in (N'U'))
					CREATE TABLE [dbo].[tablas_relacionadas](
						[Tabla] [varchar](6) NOT NULL,
						[unidad] [nvarchar](255) NULL,
						[descripcion] [nvarchar](4000) NULL,
						[cantidad] [float] NULL,
						[año] [float] NULL,
						[correlativo] [float] NULL,
						[turno] [float] NULL,
						[fecha_orden_entrega] [datetime] NULL,
						[agente_aduanal] [float] NULL,
						[correlativo1] [float] NULL,
						[noOrdenSalida] [nvarchar](255) NULL,
						[coaniorec] [float] NULL,
						[covehiculo] [nvarchar](255) NULL,
						[coplacas] [nvarchar](255) NULL,
						[copoliza] [float] NULL,
						[observacion] [nvarchar](255) NULL,
						[tmoriginal] [float] NULL,
						[tmsalidas] [float] NULL,
						[crfecha] [datetime] NULL,
						[consignatario] [nvarchar](255) NULL,
						[utilizada] [bit] NOT NULL DEFAULT 0
					) ON [PRIMARY];"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosAfectados = CInt(lReturnValue)
                End If

            End Using

#End Region

#Region "DELETE FROM tablas_relacionadas"

            '#DELETE FROM tablas_relacionadas
            vSQL = "DELETE FROM tablas_relacionadas "
            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosAfectados = CInt(lReturnValue)
                End If

            End Using

#End Region

#Region "DELETE FROM trans_pe_pol"

            '#DELETE FROM trans_pe_pol
            vSQL = "DELETE FROM trans_pe_pol WHERE fecha_poliza BETWEEN '20230411 00:00:00' AND  '20230811 23:59:59' "
            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosAfectados = CInt(lReturnValue)
                End If

            End Using

#End Region

#Region "LLenar Tablas_Relacionadas"

            '#LLenar Tablas_Relacionadas
            vSQL = "INSERT INTO tablas_relacionadas
					Select 'CEALSA' Tabla, *, 0
                    From Polizas_CEALSA
                    Where [fecha orden entrega] BETWEEN '20230411' AND  '20230811'
					union
				    Select  'WMS' Tabla, case when um.Nombre= 'bulto' then 'BULTOS' else um.Nombre end unidad, 
                            REPLACE(REPLACE(p.nombre_producto,', S.A',''),', SA','') descripcion,sum(p.Cantidad) cantidad,DATEPART(year, CONVERT(date, p.fec_agr)) año, 0 correlativo,
							0 turno, CONVERT(date, p.fec_agr) fecha_orden_entrega, p.IdPedidoEnc agente_aduanal, 0 correlativo1, '' noOrdenSalida, 
							0 coaniorec, '' covehiculo, '' coplacas,0 copoliza,'' observacion, 0 tmoriginal, 0 tmsalidas, CONVERT(date, p.fec_agr) crfecha, 
							'' consignatario, 0
					from trans_pe_det p inner join unidad_medida um on p.IdUnidadMedidaBasica = um.IdUnidadMedida
					WHERE IdPedidoEnc in (select IdPedidoEnc from trans_pe_enc WHERE Fecha_Pedido BETWEEN '20230411' AND  '20230811' and IdBodega =  2)
					group by um.Nombre, p.nombre_producto,CONVERT(date, p.fec_agr),p.IdPedidoEnc
					order by descripcion "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosTablasRelacionadas = CInt(lReturnValue)
                End If

            End Using

            '#LLenar Tablas_Relacionadas con los registros encontrados manualmente 
            vSQL = "INSERT INTO tablas_relacionadas
					SELECT 'WMS','BULTOS','',0,2023,0,0,fecha_pedido,IdPedidoEnc, 0,numero_orden, 0,'','',numero_orden, 'DTS-Manual',0,0,fecha_pedido,Propietario, 0
					FROM Polizas_Con_NoOrden "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosTablasRelacionadas = CInt(lReturnValue)
                End If

            End Using

#End Region

#Region "Disminuir la cantidad de caracteres por descripción para mejorar las coincidencias."

            '#Disminuir la cantidad de caracteres por descripción para mejorar las coincidencias.
            vSQL = "UPDATE tablas_relacionadas set descripcion = CASE WHEN LEN(descripcion)>=15 THEN SUBSTRING(descripcion,0,15) 
							ELSE SUBSTRING(descripcion,0,len(descripcion)) END 
							WHERE tabla = 'CEALSA'"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosTablasRelacionadas = CInt(lReturnValue)
                End If

            End Using

#End Region

#Region "Establecer una bandera que sirva de punto de referencia para saber si a ese pedido ya se le encontró o no una póliza relacionada."

            '#Establecer una bandera que sirva de punto de referencia para saber si a ese pedido ya se le encontró o no una póliza relacionada.
            vSQL = "UPDATE trans_pe_enc set no_documento_externo = '' WHERE no_documento_externo <> ''"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosTablasRelacionadas = CInt(lReturnValue)
                End If

            End Using

#End Region

#Region "Equiparar descripcíones, orden, valor WMS en tablas_relacionadas."

            '#Equiparar descripcíones, orden, valor WMS en tablas_relacionadas.
            vSQL = "update tablas_relacionadas set descripcion = 'A.T IMPRESOS' where descripcion = 'A. T. IMPRESO';
					update tablas_relacionadas set descripcion = 'A.T IMPRESOS' where descripcion = 'A.T. IMPRESO';
					update tablas_relacionadas set descripcion = 'BAYER S.A' where descripcion = 'BAYER, SOCIEDA';
					update tablas_relacionadas set descripcion = 'BRANDS AND TRIMS' where descripcion = 'BRANS AND TRIM';
					update tablas_relacionadas set descripcion = 'BRETANO GUATEMALA' where descripcion = 'BRETENO GUATEM';
					update tablas_relacionadas set descripcion = 'COFIO STAHL Y COMPAA, S.A.' where descripcion = 'COFIÑO STAH';
					update tablas_relacionadas set descripcion = 'CORDELES CODIGO 71274' where descripcion = 'CORDELES Y ART';
					update tablas_relacionadas set descripcion = 'DISEOS GRAFICOS' where descripcion = 'DISEÑOS GRAFIC';
					update tablas_relacionadas set descripcion = 'ELECTRICIDAD GENERAL GUATEMALA S.A' where descripcion = 'ELECTRICIAD GE';
					update tablas_relacionadas set descripcion = 'FELIPE AGUSTO CARIAS ESTRADA' where descripcion = 'FELIPE AUGUST';
					update tablas_relacionadas set descripcion = 'FERRETERIA LEWONSKI.' where descripcion = 'FERRETARIA LOWONSKI';
					update tablas_relacionadas set descripcion = 'FERRETORNILLOS' where descripcion = 'FERROTORNILLO';
					update tablas_relacionadas set descripcion = 'GARCIA, OSCAL,ALEMAN, ELIZABETH' where descripcion = 'GARCIA OSCA';
					update tablas_relacionadas set descripcion = 'GARCIA, OSCAL, ALEMAN, GLADYS, ELIZABETH' where descripcion = 'GARCIA OSCAL G';
					update tablas_relacionadas set descripcion = 'HOSSIER MANUFACTURING COMPAA LIMITADA' where descripcion = 'HOSIER MANUFAC';
					update tablas_relacionadas set descripcion = 'INDUBRAS CENTROAMERICA' where descripcion = 'INDRUMAS CENTR';
					update tablas_relacionadas set descripcion = 'INGIENERIA Y EQUIPOS INDUSTRIALES, SOCIEDAD ANONIMA' where descripcion = 'INGENIERIA Y E';
					update tablas_relacionadas set descripcion = 'LABORATORIOS Y DROGUERIA DONOVAN WERKE A G' where descripcion = 'LABORATORIO Y ' AND CANTIDAD = 6;
					update tablas_relacionadas set descripcion = 'LABORATORIOS Y DROGUERIA DONOVAN WERKE A G' where descripcion = 'LABORATORIO Y ' AND CANTIDAD = 5;
					update tablas_relacionadas set descripcion = 'LIBRERA E IMPRENTA VIVIAN' where descripcion = 'LIBRERIA E IMP' AND CANTIDAD = 8;
					update tablas_relacionadas set descripcion = 'LUIS ALEXANDER ESTACUY' where descripcion = 'LUIS ESTACU' AND CANTIDAD = 5;
					update tablas_relacionadas set descripcion = 'M H AUTOPARTES, S.A.' where descripcion = 'M&H AUTOPARTE' AND CANTIDAD = 716;
					update tablas_relacionadas set descripcion = 'MONTACARGAS RUIZ' where descripcion = 'MONTACARGA RUI' ;
					update tablas_relacionadas set descripcion = 'MULTIVAC CENTROAMERICA Y CARIBE S.A' where descripcion = 'MULTIVAC, S .' ;
					update tablas_relacionadas set descripcion = 'OPERADORES LOGISTICOS RANSA.' where descripcion = 'OPERADORA LOGI'   AND CANTIDAD = 4 ;
					update tablas_relacionadas set descripcion = 'OFICINAS PRODUCTIVAS' where descripcion = 'OFICINA PRODUC'   AND CORRELATIVO1 = '3708694' ;
					update tablas_relacionadas set descripcion = 'PETROSULUCIONES DE GUATEMALA' where descripcion = 'PETROSOLUCIONE'   AND CORRELATIVO1 = '3707218' ;
					update tablas_relacionadas set descripcion = 'PLATINO S.A' where descripcion = 'PLATINO, S.'   AND CORRELATIVO1 = '3701975' ;
					update tablas_relacionadas set descripcion = 'POLYMERIC POLYOL PURANOL' where descripcion = 'POLYMERC POLYO'   AND CORRELATIVO1 = '3704576' ;
					update tablas_relacionadas set descripcion = 'RICZA S.A' where descripcion = 'RICZA, SOCIEDA' ;
					update tablas_relacionadas set descripcion = 'RODIO EWISSBORING GUATEMALA' where descripcion = 'RODIO SWINSSBO' ;
					update tablas_relacionadas set descripcion = 'RODRIGUEZ,,,,JAIME, ANDRES' where descripcion = 'RODRIGUEZ,,,JA' ;
					update tablas_relacionadas set descripcion = 'SAN DIEGO S.A' where descripcion = 'SAN DIEGO,S .' ;
					update tablas_relacionadas set descripcion = 'SAN DIEGO S.A' where descripcion = 'SAN DIEGO, S.A' ;
					update tablas_relacionadas set descripcion = 'SECOGUA' where descripcion = 'SERCOGU' ;
					update tablas_relacionadas set descripcion = 'SINOCERAS S.A' where descripcion = 'SINOCERAS, SOC' ;
					update tablas_relacionadas set descripcion = 'STRONG SUPPLY GUATEMALA, S.A.' where descripcion = 'STRONG SUPL' ;
					update tablas_relacionadas set descripcion = 'TECNOLOGIA LUBRICANTES' where descripcion = 'TECNOLOGIA EN ' ;
					update tablas_relacionadas set descripcion = 'ZUCCHELLI ALPHA DE GUATEMALA S.A' where descripcion = 'ZUCHELLI ALPH' ;
					update tablas_relacionadas set descripcion = 'CLARA HILDA MARIA, FALLA BIANCHI DE CRUZ-GOMAR' where descripcion = 'CLARA BIANCH' ;
					update tablas_relacionadas set descripcion = 'COMPAA DE JARABES Y BEBIDAS GASEOSAS LA MARIPOSA, S.A' where descripcion = 'COMPAÑIA DE JA' ;
					update tablas_relacionadas set descripcion = 'PECUARIA EXPORTADORA, S.A' where descripcion = 'PECUERIA EXPOR' ;
					update tablas_relacionadas set descripcion = 'IMPORTADORA Y EXPORTADORA GUERRA RUANO, S.A' where descripcion = 'IMPORTADORA GU' ;
					update tablas_relacionadas set descripcion = 'COMPAA FRANZ SERVICIOS COMERCIALES' where descripcion = 'COMPAÑIA FRAN' ;
					update tablas_relacionadas set descripcion = 'DIRECT TO SOURCE GUATEMALA, S.A' where descripcion = 'DIRET TO SOURC' ;
					update tablas_relacionadas set descripcion = 'INGIENERIA Y EQUIPOS INDUSTRIALES, SOCIEDAD' where descripcion = 'INGENIERIA Y EQUIPOS INDUSTRIALES, S.A' ;
					update tablas_relacionadas set descripcion = 'IMPERMIABILIZANTES' where descripcion = 'IMPERMEABILIZA' ;
					update tablas_relacionadas set descripcion = 'MOLINOS CENTRAL HEVELTIA S.A' where descripcion = 'MOLINO CENTRA' ;
					update tablas_relacionadas set descripcion = 'SUMINISTROS, INUSUMOS Y SOLUCIONES ECOLOGICAS' where descripcion = 'SUMINISTROS E' ;
					update tablas_relacionadas set descripcion = 'COMPAA DE DISTRIBUCION CENTROAMERICANA S.A' where descripcion = 'COMPAÑIA DE DI' ;
					update tablas_relacionadas set descripcion = 'OPERADORES LOGISTICOS RANSA, S.A.' where descripcion = 'OPERADORES LOGISTICOS RANSA.' ;
					update tablas_relacionadas set descripcion = 'OPERADORES LOGISTICOS RANSA, S.A.' where descripcion = 'OPERADORA LOGI'
					update tablas_relacionadas set descripcion = 'COPORACION EL SOL DE OCCIDENTE' where descripcion = 'CORPORACION EL' ;
					update tablas_relacionadas set descripcion = 'IMPOTRACTOR S.A' where descripcion = 'IMPOTRACTOR,S ';
					update tablas_relacionadas set descripcion = 'INVERSIONES INTERNACIONALES JR, S.A.' where descripcion = 'INVERSIONES JY'
					update tablas_relacionadas set descripcion = 'COMPAA PROMOTORA DE SERVICIOS' where descripcion = 'COMPAÑIA PROMO';
					update tablas_relacionadas set descripcion = 'NATURAL WOODS DESING' where descripcion = 'NATURAL WOOD';
					update tablas_relacionadas set descripcion = 'TECNOLOGIA EN LUBRICANTES' where descripcion = 'TECNOLOGIA LUBRICANTES';
					update tablas_relacionadas set descripcion = 'IMPORTADORA Y EXPORTADORA SOLARES' where descripcion = 'IMPORTADORA SO';
					update tablas_relacionadas set descripcion = 'COPYLOT S.A' where descripcion = 'COPYPLO';
					update tablas_relacionadas set descripcion = 'LABORATORIO Y DROGUERIA DONOVAN WEKE A G.' where descripcion = 'LABORATORIOS Y DROGUERIA DONOVAN WERKE A G';"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosTablasRelacionadas = CInt(lReturnValue)
                End If

            End Using

#End Region

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Equiparar_Nombres_Agrupacion(ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As Integer

        Dim vRegistrosTablasRelacionadas As Integer = 0
        Dim vSQL As String = ""

        Try

#Region "Equiparar descripcíones en tablas_relacionadas para agrupar"

            '#Equiparar descripcíones, orden, valor WMS en tablas_relacionadas.
            vSQL = "UPDATE tablas_relacionadas SET unidad = 'BULTOS' WHERE utilizada = 0 AND Tabla = 'WMS';
					UPDATE tablas_relacionadas SET descripcion = 'MEDICAMENTO VA' WHERE agente_aduanal IN (SELECT IdPedidoEnc FROM trans_pe_enc WHERE no_documento_externo = '' AND IdBodega = 2 AND fec_agr BETWEEN '20230411' AND  '20230812' 
					AND IdPropietarioBodega = 2082)  AND utilizada = 0;
					UPDATE tablas_relacionadas SET unidad = 'VEHICULOS' 
					WHERE utilizada = 0 AND Tabla = 'WMS' AND agente_aduanal IN ('8992','8996','8994','8995','8990','8993');
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR' WHERE agente_aduanal = '6943' and utilizada = 0;
					UPDATE tablas_relacionadas set  descripcion = 'QUESOS VARIOS ' WHERE descripcion = 'REBANADAS DE SANDWICH PROCESADAS Y PASTEURIZADAS'   and utilizada = 0;
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230413' WHERE agente_aduanal = '6943' and utilizada = 0;
					UPDATE tablas_relacionadas set  descripcion = 'MERCADERIA VAR' WHERE descripcion = 'DONAS DIFERENTES PRESENTACIONES'  and utilizada = 0;
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230512' WHERE agente_aduanal = '7718' and utilizada = 0;
					UPDATE tablas_relacionadas set  descripcion = 'MERCADERIA VAR' WHERE agente_aduanal = '8516' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = '005700 GEELY R' WHERE descripcion = 'GEELY CAMIONETA ROJO' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = '005703 GEELY P' WHERE descripcion = 'GEELY CAMIONETA PLATA' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = '200797 GEELY B' WHERE descripcion = 'GEELY CAMIONETA BLANCO' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = '200942 GEELY D' WHERE descripcion = 'GEELY CAMIONETA DORADO' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = '005701 GEELY G' WHERE descripcion = 'GEELY CAMIONETA GRIS' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'PIMIENTA NEGRA' WHERE descripcion = 'CONDIMENTOS' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE descripcion = 'TAPAS PLASTICAS, PAJILLAS, PALILLOS Y PALETAS DE MADERA' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE descripcion = 'TAPADERA 33-400 TAPERED SSQZ COLOR BLANCO' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE descripcion = 'CASCOS PARA MOTOCICLISTAS COLOR NEGRO' and utilizada = 0 and agente_aduanal = 9896;
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE descripcion = 'CASCOS PARA MOTO' and utilizada = 0 and agente_aduanal = 9897;
					UPDATE tablas_relacionadas set descripcion = 'DESHUMIDIFICAD'  WHERE descripcion = 'ARTICULOS PARA FERRETERIA Y MISCELANEOS' and utilizada = 0 and agente_aduanal = 9988;
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTOS Y'  WHERE utilizada = 0 and agente_aduanal = 9999;
					UPDATE tablas_relacionadas set descripcion = 'GAJOS SAZONADO', fecha_orden_entrega = '20230411'  WHERE utilizada = 0 and agente_aduanal = 6862;
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE descripcion = 'PREPARACION DE LIMPIEZA P CITROMETRO' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'BAGUETTE PLUS', fecha_orden_entrega = '20230509'  WHERE descripcion = 'MERCADERIA VAR' and agente_aduanal ='7582' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE agente_aduanal ='7793' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR' WHERE agente_aduanal ='8915' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE agente_aduanal ='8980' and utilizada = 0; 
					UPDATE tablas_relacionadas set fecha_orden_entrega = '20230623' where agente_aduanal = '8901' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR', fecha_orden_entrega = '20230628' WHERE agente_aduanal ='8987' and utilizada = 0;  
                    UPDATE tablas_relacionadas set descripcion = 'GRUPO A.P' WHERE descripcion = 'CRUPO A.';
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE agente_aduanal = '9287' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'ADRIGU'	WHERE DESCRIPCION = 'NATURAL WOODS DESING' AND agente_aduanal = '9269' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'	WHERE agente_aduanal = '9501' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE agente_aduanal = '7678' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR', fecha_orden_entrega = '20230718' WHERE agente_aduanal = '9523' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'ARTICULOS VARI', fecha_orden_entrega = '20230717' WHERE agente_aduanal = '9452' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'ARTICULOS VARI', fecha_orden_entrega = '20230524' WHERE agente_aduanal = '8117' and utilizada = 0;  
					UPDATE tablas_relacionadas set fecha_orden_entrega = '20230719' WHERE agente_aduanal = '9564' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='LWESTON SPICY SSND WED' and agente_aduanal = '10131' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='MAC FRY MACFRY CANOLA' and agente_aduanal = '10131' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='LWESTON SPICY SSND WED' and agente_aduanal = '9728' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='MAC FRY MACFRY CANOLA' and agente_aduanal = '9728' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='MAC PTY LAMB WESTON HA' and agente_aduanal = '9728' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='LWESTON SPICY SSND WED' and agente_aduanal = '8723' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='MAC FRY MACFRY CANOLA' and agente_aduanal = '8723' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='MAC PTY LAMB WESTON HA' and agente_aduanal = '8723' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='LWESTON SPICY SSND WED' and agente_aduanal = '8393' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI' WHERE descripcion='MAC FRY MACFRY CANOLA' and agente_aduanal = '8393' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR' WHERE descripcion='ALUPAPEL 20MY, ANCHO 1004MM' and agente_aduanal = '9324' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR' WHERE descripcion='ALUPAPEL 20MY, ANCHO 1004MM' and agente_aduanal = '9325' and utilizada = 0;
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230418' WHERE agente_aduanal = '7031' and utilizada = 0;  
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230504' WHERE agente_aduanal = '7468' and utilizada = 0;  
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230605' WHERE agente_aduanal = '8371' and utilizada = 0;  
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230609' WHERE agente_aduanal = '8442' and utilizada = 0;  
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230705' WHERE agente_aduanal = '9143' and utilizada = 0;  
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230724' WHERE agente_aduanal = '9696' and utilizada = 0;  
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230803' WHERE agente_aduanal = '10003' and utilizada = 0;				
					UPDATE tablas_relacionadas set descripcion = 'INDUSTRIAS COS' WHERE descripcion='INDUSTRIA COSMETICA KENT.' and agente_aduanal = '7287' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'OPERADORES LOGISTICOS RANSA, S.A.' WHERE descripcion='OPERADORA LOGISTICA GUATEMALA S.A' and agente_aduanal = '8777' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'CORP DE SERVIC' WHERE descripcion='COPRPORACION DE SERVICIOS INDUSTRIALES ESPECIALIZADOS' and agente_aduanal = '8841' and utilizada = 0;
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230712' WHERE agente_aduanal = '9269' and utilizada = 0;  
					UPDATE tablas_relacionadas set descripcion = 'NATURAL WOOD´' WHERE descripcion='NATURAL WOODS DESING' and agente_aduanal = '9610' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'M&H AUTOPARTE' WHERE descripcion='MH AUTOPARTES S.A' and agente_aduanal = '7810' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'M&H AUTOPARTE' WHERE descripcion='MH AUTOPARTES S.A' and agente_aduanal = '7811' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'M&H AUTOPARTE' WHERE descripcion='MH AUTOPARTES S.A' and agente_aduanal = '7816' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'ALIMENTOS, S.' WHERE descripcion='ALIMENTOS S,A' and agente_aduanal = '8795' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'CERVECERIA NAC' WHERE descripcion='IMPORTADORA Y EXPORTADORA SOLARES' and agente_aduanal = '9659' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'TOHILGU' WHERE descripcion='CERVECERIA CENTRO AMERICANA.' and agente_aduanal = '9922' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTOS V' WHERE descripcion='MEDICAMENTO VA' and agente_aduanal = '8345' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTOS V' WHERE descripcion='MEDICAMENTO VA' and agente_aduanal = '8442' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTOS V' WHERE descripcion='MEDICAMENTO VA' and agente_aduanal = '10004' and utilizada = 0; 
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230724' WHERE agente_aduanal = '9698' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'REPUESTOS VARI' WHERE agente_aduanal = '7617' and utilizada = 0;
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI', fecha_orden_entrega = '20230420' WHERE agente_aduanal = '7101' and utilizada = 0; 
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230518' WHERE agente_aduanal = '7933' and utilizada = 0; 
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230522' WHERE agente_aduanal = '7985' and utilizada = 0; 
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230529' WHERE agente_aduanal = '8200' and utilizada = 0; 
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230620' WHERE agente_aduanal = '8767' and utilizada = 0; 
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230706' WHERE agente_aduanal = '9173' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'BAGUETTE PLUS '  WHERE agente_aduanal = '7582' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE agente_aduanal = '7101' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'PRODUCTOS VARI'  WHERE agente_aduanal = '7933' and utilizada = 0; 
					UPDATE tablas_relacionadas set  fecha_orden_entrega = '20230717' WHERE agente_aduanal = '9501' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTO VA', fecha_orden_entrega = '20230510'  WHERE agente_aduanal = '7630' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTO VA', fecha_orden_entrega = '20230712'  WHERE agente_aduanal = '9385' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTO VA', fecha_orden_entrega = '20230712'  WHERE agente_aduanal = '9396' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTO VA', fecha_orden_entrega = '20230712'  WHERE agente_aduanal = '9393' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MEDICAMENTO VA', fecha_orden_entrega = '20230530'  WHERE agente_aduanal = '8226' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE agente_aduanal = '8486' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'BULTOS '  WHERE agente_aduanal = '9922' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE agente_aduanal = '9550' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR'  WHERE agente_aduanal = '9953' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR', fecha_orden_entrega = '20230731'  WHERE agente_aduanal = '9955' and utilizada = 0; 
					UPDATE tablas_relacionadas set descripcion = 'MERCADERIA VAR', fecha_orden_entrega = '20230720'  WHERE agente_aduanal = '9758' and utilizada = 0; "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosTablasRelacionadas = CInt(lReturnValue)
                End If

            End Using

#End Region

            Return vRegistrosTablasRelacionadas

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualiza_Polizas_Cruzadas(ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Integer

        Dim vRegistrosTablasRelacionadas As Integer = 0
        Dim vSQL As String = ""

        Try

#Region "Actualizar pólizas cruzadas"

            '#Actualizar pólizas cruzadas o documentos con polizas en la tmp_trans_pe_pol
            vSQL = "UPDATE trans_pe_pol SET numero_orden = '3403703434' WHERE IdOrdenPedidoEnc = 8290 and numero_orden = '3403703446'
					UPDATE trans_pe_enc SET no_documento_externo = '3403703434' WHERE IdPedidoEnc = 8290 and no_documento_externo = '3403703446'
					UPDATE trans_pe_pol SET numero_orden = '3403703446' WHERE IdOrdenPedidoEnc = 8423 and numero_orden = '3403703634'
					UPDATE trans_pe_enc SET no_documento_externo = '3403703446' WHERE idpedidoenc = 8423 and no_documento_externo = '3403703634'
					UPDATE trans_pe_pol SET numero_orden = '3403703634' WHERE IdOrdenPedidoEnc = 8638 and numero_orden = '3403703850'
					UPDATE trans_pe_enc SET no_documento_externo = '3403703634' WHERE IdPedidoEnc = 8638 and no_documento_externo = '3403703850'
					UPDATE trans_pe_pol SET numero_orden = '3403703850' WHERE IdOrdenPedidoEnc = 8959 and numero_orden = '3403704123'
					UPDATE trans_pe_enc SET no_documento_externo = '3403703850' WHERE IdPedidoEnc = 8959 and no_documento_externo = '3403704123'
					UPDATE trans_pe_pol SET numero_orden = '3403704123' WHERE IdOrdenPedidoEnc = 9134 and numero_orden = '3403704162'
					UPDATE trans_pe_enc SET no_documento_externo = '3403704123' WHERE IdPedidoEnc = 9134 and no_documento_externo = '3403704162'
					UPDATE trans_pe_pol SET numero_orden = '3403704162' WHERE IdOrdenPedidoEnc = 9529 and numero_orden = '3403704172'
					UPDATE trans_pe_enc SET no_documento_externo = '3403704162' WHERE IdPedidoEnc = 9529 and no_documento_externo = '3403704172'
					UPDATE trans_pe_pol SET numero_orden = '3403704172' WHERE IdOrdenPedidoEnc = 9841 and numero_orden = '3403704707'
					UPDATE trans_pe_enc SET no_documento_externo = '3403704172' WHERE IdPedidoEnc = 9841 and no_documento_externo = '3403704707'
					UPDATE trans_pe_pol SET numero_orden = '2742704633' WHERE IdOrdenPedidoEnc = 9369 and numero_orden = '2743703281'
					UPDATE trans_pe_enc SET no_documento_externo = '2742704633' WHERE IdPedidoEnc =  9369 and no_documento_externo = '2743703281'
                    UPDATE trans_pe_enc SET no_documento_externo = '2543707165' WHERE IdPedidoEnc = 9633 and no_documento_externo = ''
                    UPDATE trans_pe_enc SET no_documento_externo = '2543707873' WHERE IdPedidoEnc = 9754 and no_documento_externo = ''
                    UPDATE trans_pe_enc SET no_documento_externo = '3013712186' WHERE IdPedidoEnc = 9938 and no_documento_externo = ''
                    UPDATE trans_pe_enc SET no_documento_externo = '3403704353' WHERE IdPedidoEnc = 10037 and no_documento_externo = ''
                    UPDATE trans_pe_enc SET no_documento_externo = '2333706041' WHERE IdPedidoEnc = 10102 and no_documento_externo = ''
                    UPDATE trans_pe_enc SET no_documento_externo = '3403704353' WHERE IdPedidoEnc = 10098 and no_documento_externo = ''
                    UPDATE trans_pe_enc SET no_documento_externo = '3403704609' WHERE IdPedidoEnc = 10101 and no_documento_externo = ''
                    UPDATE trans_pe_enc SET no_documento_externo = '3473708595' WHERE IdPedidoEnc = 10130 and no_documento_externo = ''
                    UPDATE trans_pe_enc SET no_documento_externo = '3013713066' WHERE IdPedidoEnc = 10194 and no_documento_externo = ''	
                    UPDATE trans_pe_enc SET no_documento_externo = '2513721758' WHERE IdPedidoEnc =  9246 and no_documento_externo = '2513727109'	
                    UPDATE trans_pe_enc SET no_documento_externo = '2513720303' WHERE IdPedidoEnc =  7562 and no_documento_externo = '2513728734'	
                    UPDATE trans_pe_enc SET no_documento_externo = '2513734653' WHERE IdPedidoEnc =  8842 and no_documento_externo = '2513729251'	
                    UPDATE trans_pe_enc SET no_documento_externo = '2513732015' WHERE IdPedidoEnc =  8935 and no_documento_externo = '2513729261'	
                    UPDATE trans_pe_enc SET no_documento_externo = '2963710996' WHERE IdPedidoEnc =  8606 and no_documento_externo = '2963709134'	
                    UPDATE trans_pe_enc SET no_documento_externo = '3273702376' WHERE IdPedidoEnc =  7586 and no_documento_externo = '3273701963'	
                    UPDATE trans_pe_enc SET no_documento_externo = '3323704664' WHERE IdPedidoEnc =  9472 and no_documento_externo = '3323704665'	
                    UPDATE trans_pe_enc SET no_documento_externo = '2963706407' WHERE IdPedidoEnc =  7552 and no_documento_externo = '3393700269'	
                    UPDATE trans_pe_enc SET no_documento_externo = '3573703730' WHERE IdPedidoEnc =  8751 and no_documento_externo = '3573702727'	
                    UPDATE trans_pe_enc SET no_documento_externo = '3403704093' WHERE IdPedidoEnc =  8982 and no_documento_externo = '3573702728'
					UPDATE trans_pe_enc SET no_documento_externo = '2513718691' WHERE IdPedidoEnc =  7713 and no_documento_externo = '2513722754'	
					UPDATE trans_pe_enc SET no_documento_externo = '2963706407' WHERE IdPedidoEnc =  7350 and no_documento_externo = '2613701928'	
					UPDATE trans_pe_enc SET no_documento_externo = '3133707158' WHERE IdPedidoEnc =  9531 and no_documento_externo = '2613702418'	
					UPDATE trans_pe_enc SET no_documento_externo = '3493715995' WHERE IdPedidoEnc =  9118 and no_documento_externo = '2673704682'	
					UPDATE trans_pe_enc SET no_documento_externo = '3313701005' WHERE IdPedidoEnc =  9195 and no_documento_externo = '3083701450'	
					UPDATE trans_pe_enc SET no_documento_externo = '2613701928' WHERE IdPedidoEnc =  7545 and no_documento_externo = '3133707158'	
					UPDATE trans_pe_enc SET no_documento_externo = '3573703730' WHERE IdPedidoEnc =  8981 and no_documento_externo = '3163700924'	
					UPDATE trans_pe_enc SET no_documento_externo = '3083701185' WHERE IdPedidoEnc =  7668 and no_documento_externo = '3313701005'	
					UPDATE trans_pe_enc SET no_documento_externo = '3083701003' WHERE IdPedidoEnc =  7331 and no_documento_externo = '3313701012'	
					UPDATE trans_pe_enc SET no_documento_externo = '3313701012' WHERE IdPedidoEnc =  7917 and no_documento_externo = '3313701405'	
					UPDATE trans_pe_enc SET no_documento_externo = '3353710401' WHERE IdPedidoEnc =  8944 and no_documento_externo = '3353710030'	
					UPDATE trans_pe_enc SET no_documento_externo = '3353710030' WHERE IdPedidoEnc =  9606 and no_documento_externo = '3353711772'	
					UPDATE trans_pe_enc SET no_documento_externo = '3393700220' WHERE IdPedidoEnc =  9175 and no_documento_externo = '3393700360'	
					UPDATE trans_pe_enc SET no_documento_externo = '3403704093' WHERE IdPedidoEnc =  8710 and no_documento_externo = '3573703267'
					UPDATE trans_pe_enc SET no_documento_externo = '2513722754' WHERE IdPedidoEnc = 7713 and no_documento_externo = '2513718691'
					UPDATE trans_pe_enc SET no_documento_externo = '3083701185' WHERE IdPedidoEnc = 8041 and no_documento_externo = '3083701553'
					UPDATE trans_pe_enc SET no_documento_externo = '3083701003' WHERE IdPedidoEnc = 7807 and no_documento_externo = '3083701632'
					UPDATE trans_pe_enc SET no_documento_externo = '2333705573' WHERE IdPedidoEnc = 9618 and no_documento_externo = '2333705393'
					UPDATE trans_pe_enc SET no_documento_externo = '2333705892' WHERE IdPedidoEnc = 9228 and no_documento_externo = '2333705573'
					UPDATE trans_pe_enc SET no_documento_externo = '2513722754' WHERE IdPedidoEnc = 7013 and no_documento_externo = '2513720303'
					UPDATE trans_pe_enc SET no_documento_externo = '2513734675' WHERE IdPedidoEnc = 8129 and no_documento_externo = '2513721758'
					UPDATE trans_pe_enc SET no_documento_externo = '2513721780' WHERE IdPedidoEnc = 7587 and no_documento_externo = '2513721772'
					UPDATE trans_pe_enc SET no_documento_externo = '2513734652' WHERE IdPedidoEnc = 7707 and no_documento_externo = '2513721773'
					UPDATE trans_pe_enc SET no_documento_externo = '2513721772' WHERE IdPedidoEnc = 7734 and no_documento_externo = '2513732015'
					UPDATE trans_pe_enc SET no_documento_externo = '2513732030' WHERE IdPedidoEnc = 9338 and no_documento_externo = '2513734652'
					UPDATE trans_pe_enc SET no_documento_externo = '2513721773' WHERE IdPedidoEnc = 7076 and no_documento_externo = '2513734653'
					UPDATE trans_pe_enc SET no_documento_externo = '2513734684' WHERE IdPedidoEnc = 9526 and no_documento_externo = '2513734675'
					UPDATE trans_pe_enc SET no_documento_externo = '2513714906' WHERE IdPedidoEnc = 8404 and no_documento_externo = '2513734684'
					UPDATE trans_pe_enc SET no_documento_externo = '2963707167' WHERE IdPedidoEnc = 8499 and no_documento_externo = '2543707165'
					UPDATE trans_pe_enc SET no_documento_externo = '2753700403' WHERE IdPedidoEnc = 7145 and no_documento_externo = '2753700632'	
					UPDATE trans_pe_enc SET no_documento_externo = '2753700632' WHERE IdPedidoEnc = 7156 and no_documento_externo = '2753700654'
					UPDATE trans_pe_enc SET no_documento_externo = '3403704437' WHERE IdPedidoEnc = 9470 and no_documento_externo = '3403704094'
					UPDATE trans_pe_enc SET no_documento_externo = '3403704094' WHERE IdPedidoEnc = 7352 and no_documento_externo = '3403704437'
                    UPDATE trans_pe_enc SET no_documento_externo = '2333705393' WHERE IdPedidoEnc = 9619 and no_documento_externo = '2333702928'	
					UPDATE trans_pe_enc SET no_documento_externo = '2333702928' WHERE IdPedidoEnc = 7029 and no_documento_externo = ''
					UPDATE trans_pe_enc SET no_documento_externo = '2343702758' WHERE IdPedidoEnc = 8475 and no_documento_externo = ''
					UPDATE trans_pe_enc SET no_documento_externo = '2343702759' WHERE IdPedidoEnc = 8479 and no_documento_externo = '' "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosTablasRelacionadas = CInt(lReturnValue)
                End If

            End Using

#End Region

            Return vRegistrosTablasRelacionadas

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Cealsa_No_Utilizadas(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas WHERE TABLA = 'CEALSA' AND utilizada=0 "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                    Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_WMS(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas WHERE TABLA = 'WMS' "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                    Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Agrupadas(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT Tabla, unidad, descripcion, sum(cantidad) cantidad, año, correlativo, turno, fecha_orden_entrega, agente_aduanal, 
                                         correlativo1, noOrdenSalida, coaniorec, covehiculo, coplacas, copoliza, observacion, tmoriginal, tmsalidas, crfecha, consignatario, utilizada
								  FROM tablas_relacionadas 
								  WHERE utilizada = 0								  
								  GROUP BY Tabla, unidad, descripcion, año, correlativo, turno, fecha_orden_entrega, agente_aduanal, correlativo1, noOrdenSalida, coaniorec, covehiculo, coplacas, copoliza, observacion, tmoriginal, tmsalidas, crfecha, consignatario, utilizada
								  ORDER BY fecha_orden_entrega, cantidad"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                    Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                              ByVal lConnection As SqlConnection,
                                              ByVal lTransaction As SqlTransaction) As clsBeTablas_relacionadas

        Dim vReturn As clsBeTablas_relacionadas = Nothing

        Try

            Const sp As String = "SELECT Tabla, unidad, descripcion, cantidad, año, correlativo, turno, fecha_orden_entrega, agente_aduanal, 
                                         correlativo1, noOrdenSalida, coaniorec, covehiculo, coplacas, copoliza, observacion, tmoriginal, tmsalidas, crfecha, consignatario, utilizada
								  FROM tablas_relacionadas 
								  WHERE agente_aduanal = @agente_aduanal "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@agente_aduanal", IdPedidoEnc)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If Not lDataTable Is Nothing Then

                    If lDataTable.Rows.Count > 0 Then

                        Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                        vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                        Cargar(vBeTablas_relacionadas, lDataTable.Rows(0))
                        vReturn = vBeTablas_relacionadas

                    End If

                End If

            End Using

            Return vReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Polizas_Parciales() As List(Of clsBeTmp_trans_pe_pol)

        Dim lReturnList As New List(Of clsBeTmp_trans_pe_pol)

        Try

            Const sp As String = "select * from tmp_trans_pe_pol where numero_orden IN (
									select copoliza from tablas_relacionadas where tabla = 'WMS'
									AND agente_aduanal <> 0 AND noOrdenSalida NOT IN (select numero_orden from trans_pe_pol))
									and IdOrdenPedidoEnc =0 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTablas_relacionadas As New clsBeTmp_trans_pe_pol

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTablas_relacionadas = New clsBeTmp_trans_pe_pol()
                            clsLnTmp_trans_pe_pol.Cargar(vBeTablas_relacionadas, dr)
                            lReturnList.Add(vBeTablas_relacionadas)
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

    Public Shared Function Get_Single_By_NoOrden(ByVal pNoOrden As String) As clsBeTablas_relacionadas

        Get_Single_By_NoOrden = Nothing

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas WHERE copoliza = @numero_orden AND Tabla = 'WMS '"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pNoOrden)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTablas_relacionadas, lDataTable.Rows(0))
                            Get_Single_By_NoOrden = vBeTablas_relacionadas
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_NoOrden(ByVal pNoOrden As String,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeTablas_relacionadas

        Get_Single_By_NoOrden = Nothing

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas WHERE copoliza = @numero_orden AND Tabla = 'WMS '"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pNoOrden)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTablas_relacionadas, lDataTable.Rows(0))
                    Get_Single_By_NoOrden = vBeTablas_relacionadas
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pTablaRelacionada As clsBeTablas_relacionadas,
                                  ByVal lConnection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As clsBeTablas_relacionadas

        Existe = Nothing

        Try

            Const sp As String = "SELECT * 
                                  FROM Tablas_relacionadas 
                                  WHERE copoliza = @copoliza AND Tabla = 'WMS' AND agente_aduanal = @agente_aduanal "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@copoliza", pTablaRelacionada.Copoliza)
                lDTA.SelectCommand.Parameters.AddWithValue("@agente_aduanal", pTablaRelacionada.Agente_aduanal)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTablas_relacionadas, lDataTable.Rows(0))
                    Existe = vBeTablas_relacionadas
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC202308211920: Devuelve el listado de polizas que están en la tmp_trans_pe_pol
    ''' con un número de órden y el número de órden existe en tabla (tablas_relacionadas) en el campo copoliza.
    ''' </summary>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Polizas_Parciales(ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeTmp_trans_pe_pol)

        Dim lReturnList As New List(Of clsBeTmp_trans_pe_pol)

        Try

            Const sp As String = "SELECT * from tmp_trans_pe_pol WHERE numero_orden IN (
								  SELECT copoliza from tablas_relacionadas WHERE tabla = 'WMS'
								  AND agente_aduanal <> 0 AND noOrdenSalida NOT IN (SELECT numero_orden FROM trans_pe_pol))
								  AND IdOrdenPedidoEnc =0 "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTmp_trans_pe_pol

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTmp_trans_pe_pol()
                    clsLnTmp_trans_pe_pol.Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_WMS_Sin_Asociacion(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT t.Tabla, r.nombre_comercial consignatario, t.unidad, t.descripcion, sum(t.cantidad) cantidad, t.año, t.correlativo, t.turno, t.fecha_orden_entrega, 
										 t.agente_aduanal, t.correlativo1, t.noOrdenSalida, t.coaniorec, t.covehiculo, t.coplacas, t.copoliza, 
										 t.observacion, t.tmoriginal, t.tmsalidas, t.crfecha, t.utilizada
							      FROM tablas_relacionadas t inner join 
									   trans_pe_enc p on t.agente_aduanal = p.IdPedidoEnc inner join 
									   propietario_bodega b on p.IdPropietarioBodega = b.IdPropietarioBodega inner join
									   propietarios r on r.IdPropietario = b.IdPropietario
								  WHERE utilizada = 0 and tabla =  'WMS'
								  GROUP BY  t.Tabla, t.consignatario, r.nombre_comercial, t.unidad, t.descripcion,t.año, t.correlativo, t.turno, t.fecha_orden_entrega, 
										    t.agente_aduanal, t.correlativo1, t.noOrdenSalida, t.coaniorec, t.covehiculo, t.coplacas, t.copoliza, 
										    t.observacion, t.tmoriginal, t.tmsalidas, t.crfecha, t.utilizada"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                    Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Cealsa(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas WHERE TABLA = 'CEALSA' "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                    Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_WMS_Asociadas(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT * FROM tablas_relacionadas WHERE utilizada =1 
								  AND tabla = 'WMS' AND noOrdenSalida <> '' 
								  AND noOrdenSalida IN (SELECT numero_orden FROM tmp_trans_pe_pol) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                    Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_WMS_Asociadas_By_CoPoliza(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTablas_relacionadas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas)

        Try

            Const sp As String = "SELECT * FROM tablas_relacionadas WHERE utilizada =1 
								  AND tabla = 'WMS' AND copoliza <> '' 
								  AND copoliza IN (SELECT numero_orden FROM tmp_trans_pe_pol) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTablas_relacionadas()
                    Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Copoliza(ByVal pNoOrden As String,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeTablas_relacionadas

        Get_Single_By_Copoliza = Nothing

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas 
								  WHERE copoliza = @numero_orden AND Tabla = 'WMS '"
            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pNoOrden)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTablas_relacionadas

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTablas_relacionadas, lDataTable.Rows(0))
                    Get_Single_By_Copoliza = vBeTablas_relacionadas
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class