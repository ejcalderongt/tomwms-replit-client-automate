Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProducto_estado

    Public Shared Sub Cargar(ByRef oBeProducto_estado As clsBeProducto_estado, ByRef dr As DataRow, Optional ByVal CargarUbicacionDefectoBodega As Boolean = False)

        Try

            With oBeProducto_estado
                .IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .IdUbicacionDefecto = IIf(IsDBNull(dr.Item("IdUbicacionDefecto")), 0, dr.Item("IdUbicacionDefecto"))
                If CargarUbicacionDefectoBodega Then
                    .IdUbicacionBodegaDefecto = IIf(IsDBNull(dr.Item("IdUbicacionBodegaDefecto")), 0, dr.Item("IdUbicacionBodegaDefecto"))
                End If
                .Utilizable = IIf(IsDBNull(dr.Item("utilizable")), False, dr.Item("utilizable"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Dañado = IIf(IsDBNull(dr.Item("Dañado")), False, dr.Item("Dañado"))
                '#CKFK20220704 Agregué que se cargue el campo sistema en la clase
                .Sistema = IIf(IsDBNull(dr.Item("Sistema")), False, dr.Item("Sistema"))
                .Codigo_Bodega_ERP = IIf(IsDBNull(dr.Item("Codigo_Bodega_ERP")), "", dr.Item("Codigo_Bodega_ERP"))
                .Dias_Vencimiento_Clasificacion = IIf(IsDBNull(dr.Item("Dias_Vencimiento_Clasificacion")), 0, dr.Item("Dias_Vencimiento_Clasificacion"))
                .Tolerancia_Dias_Vencimiento = IIf(IsDBNull(dr.Item("Tolerancia_Dias_Vencimiento")), 0, dr.Item("Tolerancia_Dias_Vencimiento"))
                .Reservar_En_UmBas = IIf(IsDBNull(dr.Item("Reserva_En_UmBas")), False, dr.Item("Reserva_En_UmBas"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_estado As clsBeProducto_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto_estado")
            Ins.Add("idestado", "@idestado", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            'Ins.Add("idubicaciondefecto","@idubicaciondefecto", DataType.Parametro)
            Ins.Add("utilizable", "@utilizable", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("dañado", "@dañado", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("codigo_bodega_erp", "@codigo_bodega_erp", DataType.Parametro)
            Ins.Add("dias_vencimiento_clasificacion", "@dias_vencimiento_clasificacion", DataType.Parametro)
            Ins.Add("tolerancia_dias_vencimiento", "@tolerancia_dias_vencimiento", DataType.Parametro)
            Ins.Add("reserva_en_umbas", "@reserva_en_umbas", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeProducto_estado.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_estado.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_estado.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@UTILIZABLE", oBeProducto_estado.Utilizable))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_estado.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_estado.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_estado.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_estado.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_estado.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO", oBeProducto_estado.Dañado))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProducto_estado.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_ERP", oBeProducto_estado.Codigo_Bodega_ERP))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VENCIMIENTO_CLASIFICACION", oBeProducto_estado.Dias_Vencimiento_Clasificacion))
            cmd.Parameters.Add(New SqlParameter("@TOLERANCIA_DIAS_VENCIMIENTO", oBeProducto_estado.Tolerancia_Dias_Vencimiento))
            cmd.Parameters.Add(New SqlParameter("@RESERVA_EN_UMBAS", oBeProducto_estado.Reservar_En_UmBas))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_estado As clsBeProducto_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto_estado")
            Upd.Add("idestado", "@idestado", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("idubicaciondefecto", "@idubicaciondefecto", DataType.Parametro)
            Upd.Add("utilizable", "@utilizable", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("Dañado", "@Dañado", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("codigo_bodega_erp", "@codigo_bodega_erp", DataType.Parametro)
            Upd.Add("dias_vencimiento_clasificacion", "@dias_vencimiento_clasificacion", DataType.Parametro)
            Upd.Add("tolerancia_dias_vencimiento", "@tolerancia_dias_vencimiento", DataType.Parametro)
            Upd.Add("reserva_en_umbas", "@reserva_en_umbas", DataType.Parametro)
            Upd.Where("IdEstado = @IdEstado")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeProducto_estado.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_estado.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_estado.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDEFECTO", oBeProducto_estado.IdUbicacionDefecto))
            cmd.Parameters.Add(New SqlParameter("@UTILIZABLE", oBeProducto_estado.Utilizable))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_estado.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_estado.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_estado.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_estado.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_estado.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO", oBeProducto_estado.Dañado))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProducto_estado.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_ERP", oBeProducto_estado.Codigo_Bodega_ERP))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VENCIMIENTO_CLASIFICACION", oBeProducto_estado.Dias_Vencimiento_Clasificacion))
            cmd.Parameters.Add(New SqlParameter("@TOLERANCIA_DIAS_VENCIMIENTO", oBeProducto_estado.Tolerancia_Dias_Vencimiento))
            cmd.Parameters.Add(New SqlParameter("@RESERVA_EN_UMBAS", oBeProducto_estado.Reservar_En_UmBas))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeProducto_estado As clsBeProducto_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_estado" &
             "  Where(IdEstado = @IdEstado)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeProducto_estado.IdEstado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_estado"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Producto_estado"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProducto_estado As clsBeProducto_estado) As Boolean

        Try

            Const sp As String = "SELECT * FROM Producto_estado" &
            " Where(IdEstado = @IdEstado)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDESTADO", oBeProducto_estado.IdEstado))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_estado, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeProducto_estado)

        Try

            Dim lReturnList As New List(Of clsBeProducto_estado)
            Const sp As String = "SELECT * FROM Producto_estado"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_estado As New clsBeProducto_estado

            For Each dr As DataRow In dt.Rows

                vBeProducto_estado = New clsBeProducto_estado
                Cargar(vBeProducto_estado, dr)
                lReturnList.Add(vBeProducto_estado)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeProducto_estado As clsBeProducto_estado)

        Try

            Const sp As String = "SELECT * FROM Producto_estado" &
            " Where(IdEstado = @IdEstado)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDESTADO", pBeProducto_estado.IdEstado))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProducto_estado, dt.Rows(0))
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdEstado),0) FROM Producto_estado"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class