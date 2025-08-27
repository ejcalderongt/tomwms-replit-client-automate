Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTablas_relacionadas_placas

    Public Shared Sub Cargar(ByRef oBeTablas_relacionadas_placas As clsBeTablas_relacionadas_placas, ByRef dr As DataRow)
        Try
            With oBeTablas_relacionadas_placas
                .Coplacas = IIf(IsDBNull(dr.Item("coplacas")), "", dr.Item("coplacas"))
                .Copoliza = IIf(IsDBNull(dr.Item("copoliza")), 0.0, dr.Item("copoliza"))
                .Fecha_orden_entrega = IIf(IsDBNull(dr.Item("fecha_orden_entrega")), Date.Now, dr.Item("fecha_orden_entrega"))
                .Noordensalida = IIf(IsDBNull(dr.Item("noordensalida")), "", dr.Item("noordensalida"))
                .Correlativo1 = IIf(IsDBNull(dr.Item("correlativo1")), 0.0, dr.Item("correlativo1"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .Fecha_despacho = IIf(IsDBNull(dr.Item("fecha_despacho")), New Date(1900, 1, 1), dr.Item("fecha_despacho"))
                .NombreProducto = IIf(IsDBNull(dr.Item("NombreProducto")), "", dr.Item("NombreProducto"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .Utilizada = IIf(IsDBNull(dr.Item("utilizada")), False, dr.Item("utilizada"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTablas_relacionadas_placas As clsBeTablas_relacionadas_placas, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tablas_relacionadas_placas")
            Ins.Add("coplacas", "@coplacas", DataType.Parametro)
            Ins.Add("copoliza", "@copoliza", DataType.Parametro)
            Ins.Add("fecha_orden_entrega", "@fecha_orden_entrega", DataType.Parametro)
            Ins.Add("noordensalida", "@noordensalida", DataType.Parametro)
            Ins.Add("correlativo1", "@correlativo1", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("fecha_despacho", "@fecha_despacho", DataType.Parametro)
            Ins.Add("nombreproducto", "@nombreproducto", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@COPLACAS", oBeTablas_relacionadas_placas.Coplacas))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas_placas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ORDEN_ENTREGA", oBeTablas_relacionadas_placas.Fecha_orden_entrega))
            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas_placas.Noordensalida))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO1", oBeTablas_relacionadas_placas.Correlativo1))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTablas_relacionadas_placas.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTablas_relacionadas_placas.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESPACHO", oBeTablas_relacionadas_placas.Fecha_despacho))
            cmd.Parameters.Add(New SqlParameter("@NOMBREPRODUCTO", oBeTablas_relacionadas_placas.NombreProducto))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTablas_relacionadas_placas.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas_placas.Utilizada))

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

    Public Shared Function Actualizar(ByRef oBeTablas_relacionadas_placas As clsBeTablas_relacionadas_placas, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tablas_relacionadas_placas")
            Upd.Add("coplacas", "@coplacas", DataType.Parametro)
            Upd.Add("copoliza", "@copoliza", DataType.Parametro)
            Upd.Add("fecha_orden_entrega", "@fecha_orden_entrega", DataType.Parametro)
            Upd.Add("noordensalida", "@noordensalida", DataType.Parametro)
            Upd.Add("correlativo1", "@correlativo1", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("fecha_despacho", "@fecha_despacho", DataType.Parametro)
            Upd.Add("nombreproducto", "@nombreproducto", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COPLACAS", oBeTablas_relacionadas_placas.Coplacas))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas_placas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ORDEN_ENTREGA", oBeTablas_relacionadas_placas.Fecha_orden_entrega))
            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas_placas.Noordensalida))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO1", oBeTablas_relacionadas_placas.Correlativo1))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTablas_relacionadas_placas.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTablas_relacionadas_placas.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESPACHO", oBeTablas_relacionadas_placas.Fecha_despacho))
            cmd.Parameters.Add(New SqlParameter("@NOMBREPRODUCTO", oBeTablas_relacionadas_placas.NombreProducto))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTablas_relacionadas_placas.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas_placas.Utilizada))

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


    Public Shared Function Eliminar(ByRef oBeTablas_relacionadas_placas As clsBeTablas_relacionadas_placas, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tablas_relacionadas_placas"

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

            Const sp As String = "SELECT * FROM Tablas_relacionadas_placas"
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

    Public Shared Function Get_All() As List(Of clsBeTablas_relacionadas_placas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas_placas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas_placas"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTablas_relacionadas_placas As New clsBeTablas_relacionadas_placas

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTablas_relacionadas_placas = New clsBeTablas_relacionadas_placas()
                            Cargar(vBeTablas_relacionadas_placas, dr)
                            lReturnList.Add(vBeTablas_relacionadas_placas)
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

    Public Shared Sub GetSingle(ByRef pBeTablas_relacionadas_placas As clsBeTablas_relacionadas_placas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas_placas"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTablas_relacionadas_placas As New clsBeTablas_relacionadas_placas

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTablas_relacionadas_placas, lDataTable.Rows(0))
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

            Const sp As String = "SELECT * FROM Tablas_relacionadas_placas"

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

    Public Shared Function Get_All(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTablas_relacionadas_placas)

        Dim lReturnList As New List(Of clsBeTablas_relacionadas_placas)

        Try

            Const sp As String = "SELECT * FROM Tablas_relacionadas_placas "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas_placas As New clsBeTablas_relacionadas_placas

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas_placas = New clsBeTablas_relacionadas_placas()
                    Cargar(vBeTablas_relacionadas_placas, dr)
                    lReturnList.Add(vBeTablas_relacionadas_placas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Parcial_Tablas_Relacionadas(ByRef oBeTablas_relacionadas_placas As clsBeTablas_relacionadas_placas,
                                                                  Optional ByVal pConection As SqlConnection = Nothing,
                                                                  Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

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

            cmd.Parameters.Add(New SqlParameter("@FECHA_ORDEN_ENTREGA", oBeTablas_relacionadas_placas.Fecha_orden_entrega))
            cmd.Parameters.Add(New SqlParameter("@AGENTE_ADUANAL", oBeTablas_relacionadas_placas.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO1", oBeTablas_relacionadas_placas.Correlativo1))
            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas_placas.Noordensalida))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas_placas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas_placas.Utilizada))

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

    Public Shared Function Actualizar_Parcial_CEALSA(ByRef oBeTablas_relacionadas_placas As clsBeTablas_relacionadas_placas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tablas_relacionadas")
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)
            Upd.Where(" tabla = 'CEALSA' and correlativo1 = @correlativo1 and noordensalida=@noordensalida and copoliza=@copoliza ")
            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@FECHA_ORDEN_ENTREGA", oBeTablas_relacionadas_placas.Fecha_orden_entrega))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO1", oBeTablas_relacionadas_placas.Correlativo1))
            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas_placas.Noordensalida))
            cmd.Parameters.Add(New SqlParameter("@COPOLIZA", oBeTablas_relacionadas_placas.Copoliza))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas_placas.Utilizada))

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

    Public Shared Function Actualizar_Parcial(ByRef oBeTablas_relacionadas_placas As clsBeTablas_relacionadas_placas, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tablas_relacionadas_placas")
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)
            Upd.Where(" noordensalida=@noordensalida and IdPedidoEnc=@IdPedidoEnc ")
            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOORDENSALIDA", oBeTablas_relacionadas_placas.Noordensalida))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTablas_relacionadas_placas.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTablas_relacionadas_placas.Utilizada))

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

    Public Shared Sub Inicializar_Tablas_Placas(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim vRegistrosAfectados As Integer = 0
        Dim vRegistrosTablasRelacionadas As Integer = 0
        Dim vSQL As String = ""

        Try

#Region "Crear tabla tablas_relacionadas_placas si no existe"

            vSQL = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tablas_relacionadas_placas]') AND type in (N'U'))
					CREATE TABLE [dbo].[tablas_relacionadas_placas](
						[coplacas] [nvarchar](255) NULL,
						[copoliza] [float] NULL,
						[fecha_orden_entrega] [datetime] NULL,
						[noordensalida] [nvarchar](255) NULL,
						[correlativo1] [float] NULL,
						[descripcion] [nvarchar](4000) NULL,
						[IdPedidoEnc] [int] NULL,
						[fecha_despacho] [date] NULL,
						[NombreProducto] [nvarchar](250) NULL,
						[IdDespachoEnc] [int] NOT NULL,
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
            vSQL = "DELETE FROM tablas_relacionadas_placas "
            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosAfectados = CInt(lReturnValue)
                End If

            End Using

#End Region

#Region "LLenar Tablas_Relacionadas"

            '#LLenar Tablas_Relacionadas
            vSQL = "INSERT INTO tablas_relacionadas_placas
					SELECT DISTINCT coplacas, copoliza, fecha_orden_entrega, noordensalida, correlativo1,descripcion, 
					T.IdPedidoEnc, t.fecha_despacho, T.NombreProducto, T.IdDespachoEnc, 0 utilizada
					from tablas_relacionadas inner join 
					(SELECT DISTINCT A.PLACA collate SQL_Latin1_General_CP1_CI_AS PLACA, D.IdPedidoEnc, CONVERT(date,B.fecha) fecha_despacho, 
					d.NombreProducto, B.IdDespachoEnc
					 FROM trans_despacho_enc B inner join 
						  trans_despacho_det D on b.IdDespachoEnc = d.IdDespachoenc INNER JOIN
						  empresa_transporte_vehiculos A ON A.IdVehiculo = B.IdVehiculo 
					WHERE convert(date,b.fec_agr) BETWEEN '20230411' AND  '20230812') t on 
					t.placa = coplacas and t.fecha_despacho = tablas_relacionadas.fecha_orden_entrega
					WHERE utilizada = 0 and tabla = 'CEALSA'
					AND COPOLIZA NOT IN (SELECT COPOLIZA FROM tablas_relacionadas WHERE  tabla = 'WMS' )
					and fecha_orden_entrega BETWEEN '20230411' AND  '20230812'
					and t.IdPedidoEnc in (SELECT agente_aduanal from tablas_relacionadas where tabla = 'WMS' and fecha_orden_entrega BETWEEN '20230411' AND  '20230812' and utilizada=0)
					ORDER BY fecha_orden_entrega, t.fecha_despacho "

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

End Class
