Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Public Class clsLnEstructura_tramo

    Public Shared Sub Cargar(ByRef oBeEstructura_tramo As clsBeEstructura_tramo, ByRef dr As DataRow)

        Try

            With oBeEstructura_tramo

                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .IdSector = IIf(IsDBNull(dr.Item("IdSector")), 0, dr.Item("IdSector"))
                .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Margen_izquierdo = IIf(IsDBNull(dr.Item("margen_izquierdo")), 0.0, dr.Item("margen_izquierdo"))
                .Margen_derecho = IIf(IsDBNull(dr.Item("margen_derecho")), 0.0, dr.Item("margen_derecho"))
                .Margen_superior = IIf(IsDBNull(dr.Item("margen_superior")), 0.0, dr.Item("margen_superior"))
                .Margen_inferior = IIf(IsDBNull(dr.Item("margen_inferior")), 0.0, dr.Item("margen_inferior"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Indice_x = IIf(IsDBNull(dr.Item("Indice_x")), 0, dr.Item("Indice_x"))
                .Orientacion = IIf(IsDBNull(dr.Item("Orientacion")), 0, dr.Item("Orientacion"))
                .IdTipoProductoDefault = IIf(IsDBNull(dr.Item("IdTipoProductoDefault")), 0, dr.Item("IdTipoProductoDefault"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Horizontal = IIf(IsDBNull(dr.Item("Horizontal")), False, dr.Item("Horizontal"))
                .Orden_Descendente = IIf(IsDBNull(dr.Item("Orden_Descendente")), False, dr.Item("Orden_Descendente"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeEstructura_tramo As clsBeEstructura_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("estructura_tramo")
            Ins.Add("idbodega", "@IdBodega", DataType.Parametro)
            Ins.Add("idarea", "@idarea", DataType.Parametro)
            Ins.Add("idsector", "@idsector", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Ins.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Ins.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Ins.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("indice_x", "@indice_x", DataType.Parametro)
            Ins.Add("orientacion", "@orientacion", DataType.Parametro)
            Ins.Add("idtipoproductodefault", "@idtipoproductodefault", DataType.Parametro)
            Ins.Add("Horizontal", "@Horizontal", DataType.Parametro)
            Ins.Add("orden_descendente", "@orden_descendente", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_tramo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeEstructura_tramo.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeEstructura_tramo.IdSector))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeEstructura_tramo.Sistema))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeEstructura_tramo.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEstructura_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEstructura_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEstructura_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEstructura_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEstructura_tramo.Activo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEstructura_tramo.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEstructura_tramo.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEstructura_tramo.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeEstructura_tramo.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeEstructura_tramo.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeEstructura_tramo.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeEstructura_tramo.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeEstructura_tramo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeEstructura_tramo.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeEstructura_tramo.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTODEFAULT", oBeEstructura_tramo.IdTipoProductoDefault))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeEstructura_tramo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@HORIZONTAL", oBeEstructura_tramo.Horizontal))
            cmd.Parameters.Add(New SqlParameter("@ORDEN_DESCENDENTE", oBeEstructura_tramo.Orden_Descendente))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeEstructura_tramo.IdTramo = CInt(cmd.Parameters("@IDTRAMO").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeEstructura_tramo As clsBeEstructura_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("estructura_tramo")
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("idsector", "@idsector", DataType.Parametro)
            Upd.Add("idarea", "@idarea", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Upd.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Upd.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Upd.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("indice_x", "@indice_x", DataType.Parametro)
            Upd.Add("orientacion", "@orientacion", DataType.Parametro)
            Upd.Add("idtipoproductodefault", "@idtipoproductodefault", DataType.Parametro)
            Upd.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Upd.Add("Horizontal", "@Horizontal", DataType.Parametro)
            Upd.Add("Orden_Descendente", "@Orden_Descendente", DataType.Parametro)
            Upd.Where("IdTramo = @IdTramo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_tramo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeEstructura_tramo.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeEstructura_tramo.IdSector))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeEstructura_tramo.Sistema))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeEstructura_tramo.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEstructura_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEstructura_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEstructura_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEstructura_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEstructura_tramo.Activo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEstructura_tramo.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEstructura_tramo.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEstructura_tramo.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeEstructura_tramo.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeEstructura_tramo.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeEstructura_tramo.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeEstructura_tramo.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeEstructura_tramo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeEstructura_tramo.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeEstructura_tramo.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTODEFAULT", oBeEstructura_tramo.IdTipoProductoDefault))
            cmd.Parameters.Add(New SqlParameter("@IdBodega", oBeEstructura_tramo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@HORIZONTAL", oBeEstructura_tramo.Horizontal))
            cmd.Parameters.Add(New SqlParameter("@ORDEN_DESCENDENTE", oBeEstructura_tramo.Orden_Descendente))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeEstructura_tramo As clsBeEstructura_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Estructura_tramo" &
             "  Where(IdTramo = @IdTramo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_tramo.IdTramo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Limpiar_Todo(ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from Estructura_tramo where IdBodega = @IdBodega"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

                Dim rowsAffected As Integer = lCommand.ExecuteNonQuery()
                lCommand.Dispose()

                Return rowsAffected
            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Estructura_tramo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeEstructura_tramo As clsBeEstructura_tramo) As Boolean

        Try

            Const sp As String = "SELECT * FROM Estructura_tramo" &
            " Where(IdTramo = @IdTramo)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_tramo.IdTramo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEstructura_tramo, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeEstructura_tramo)

        Try

            Dim lReturnList As New List(Of clsBeEstructura_tramo)
            Const sp As String = "SELECT * FROM Estructura_tramo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEstructura_tramo As New clsBeEstructura_tramo

            For Each dr As DataRow In dt.Rows
                vBeEstructura_tramo = New clsBeEstructura_tramo
                Cargar(vBeEstructura_tramo, dr)
                lReturnList.Add(vBeEstructura_tramo)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeEstructura_tramo As clsBeEstructura_tramo)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Estructura_tramo " &
            " Where(IdTramo = @IdTramo) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", pBeEstructura_tramo.IdTramo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeEstructura_tramo, dt.Rows(0))
            End If

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Async Function GetSingle_Async(ByVal IdTramo As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As Task(Of clsBeEstructura_tramo)

        Dim pBeEstructura_tramo As New clsBeEstructura_tramo()

        Try
            Const sp As String = "SELECT * FROM Estructura_tramo WHERE IdTramo = @IdTramo"

            Using cmd As New SqlCommand(sp, lConnection, lTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(New SqlParameter("@IDTRAMO", IdTramo))

                Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync()
                    Dim dt As New DataTable
                    dt.Load(reader)

                    If dt.Rows.Count = 1 Then
                        Cargar(pBeEstructura_tramo, dt.Rows(0))
                    End If
                End Using
            End Using

            Return pBeEstructura_tramo

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    'Public Shared Async Function GetSingle_Async(ByVal IdTramo As Integer,
    '                                             ByVal lConnection As SqlConnection,
    '                                             ByVal lTransaction As SqlTransaction) As Task(Of clsBeEstructura_tramo)

    '    Dim pBeEstructura_tramo As New clsBeEstructura_tramo()

    '    Try

    '        Const sp As String = "SELECT * FROM Estructura_tramo " &
    '        " Where(IdTramo = @IdTramo) "

    '        Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)
    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", IdTramo))

    '        Dim dt As New DataTable
    '        dad.Fill(dt)

    '        If dt.Rows.Count = 1 Then
    '            Cargar(pBeEstructura_tramo, dt.Rows(0))
    '        End If

    '        Return pBeEstructura_tramo

    '    Catch ex As Exception
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTramo),0) FROM Estructura_tramo"

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

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTramo),0) FROM Estructura_tramo"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTramo(ByVal IdBodega As Integer, ByVal IdTramo As Integer) As clsBeEstructura_tramo

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Single_By_IdTramo = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Estructura_tramo " &
                                  " Where(IdBodega = @IdBodega AND IdTramo = @IdTramo) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", IdTramo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeEstructura_tramo As New clsBeEstructura_tramo
                Cargar(pBeEstructura_tramo, dt.Rows(0))
                Get_Single_By_IdTramo = pBeEstructura_tramo
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTramo(ByVal IdBodega As Integer,
                                                 ByVal IdTramo As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeEstructura_tramo

        Get_Single_By_IdTramo = Nothing

        Try

            Const sp As String = "SELECT * FROM Estructura_tramo " &
                                  " Where(IdBodega = @IdBodega AND IdTramo = @IdTramo) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", IdTramo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeEstructura_tramo As New clsBeEstructura_tramo
                Cargar(pBeEstructura_tramo, dt.Rows(0))
                Get_Single_By_IdTramo = pBeEstructura_tramo
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
