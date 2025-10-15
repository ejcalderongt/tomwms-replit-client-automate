Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_re_tr

    Public Overridable Sub Cargar(ByRef oBeTrans_re_tr As clsBeTrans_re_tr, ByRef dr As DataRow)
        Try
            With oBeTrans_re_tr
                .IdTipoTransaccion = IIf(IsDBNull(dr.Item("IdTipoTransaccion")), "", dr.Item("IdTipoTransaccion"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .Funcionalidad = IIf(IsDBNull(dr.Item("Funcionalidad")), "", dr.Item("Funcionalidad"))
                .UsaHH = IIf(IsDBNull(dr.Item("UsaHH")), False, dr.Item("UsaHH"))
                .DescDev = IIf(IsDBNull(dr.Item("DescDev")), "", dr.Item("DescDev"))
                .TipoTrans = IIf(IsDBNull(dr.Item("TipoTrans")), "", dr.Item("TipoTrans"))
                .ConRef = IIf(IsDBNull(dr.Item("ConRef")), False, dr.Item("ConRef"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_re_tr As clsBeTrans_re_tr, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_re_tr")
            If Not oBeTrans_re_tr.IdTipoTransaccion Is Nothing Then Ins.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            If Not oBeTrans_re_tr.Descripcion Is Nothing Then Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            If Not oBeTrans_re_tr.Funcionalidad Is Nothing Then Ins.Add("funcionalidad", "@funcionalidad", DataType.Parametro)
            If Not oBeTrans_re_tr.UsaHH Is Nothing Then Ins.Add("usahh", "@usahh", DataType.Parametro)
            If Not oBeTrans_re_tr.DescDev Is Nothing Then Ins.Add("descdev", "@descdev", DataType.Parametro)
            If Not oBeTrans_re_tr.TipoTrans Is Nothing Then Ins.Add("tipotrans", "@tipotrans", DataType.Parametro)
            If Not oBeTrans_re_tr.ConRef Is Nothing Then Ins.Add("conref", "@conref", DataType.Parametro)
            If Not oBeTrans_re_tr.ConRef Is Nothing Then Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            If Not oBeTrans_re_tr.IdTipoTransaccion Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTrans_re_tr.IdTipoTransaccion))
            If Not oBeTrans_re_tr.Descripcion Is Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_re_tr.Descripcion))
            If Not oBeTrans_re_tr.Funcionalidad Is Nothing Then cmd.Parameters.Add(New SqlParameter("@FUNCIONALIDAD", oBeTrans_re_tr.Funcionalidad))
            If Not oBeTrans_re_tr.UsaHH Is Nothing Then cmd.Parameters.Add(New SqlParameter("@USAHH", oBeTrans_re_tr.UsaHH))
            If Not oBeTrans_re_tr.DescDev Is Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCDEV", oBeTrans_re_tr.DescDev))
            If Not oBeTrans_re_tr.TipoTrans Is Nothing Then cmd.Parameters.Add(New SqlParameter("@TIPOTRANS", oBeTrans_re_tr.TipoTrans))
            If Not oBeTrans_re_tr.ConRef Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CONREF", oBeTrans_re_tr.ConRef))
            If Not oBeTrans_re_tr.ConRef Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_re_tr.Activo))

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
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_re_tr As clsBeTrans_re_tr, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_re_tr")
            If Not oBeTrans_re_tr.IdTipoTransaccion Is Nothing Then Upd.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            If Not oBeTrans_re_tr.Descripcion Is Nothing Then Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            If Not oBeTrans_re_tr.Funcionalidad Is Nothing Then Upd.Add("funcionalidad", "@funcionalidad", DataType.Parametro)
            If Not oBeTrans_re_tr.UsaHH Is Nothing Then Upd.Add("usahh", "@usahh", DataType.Parametro)
            If Not oBeTrans_re_tr.DescDev Is Nothing Then Upd.Add("descdev", "@descdev", DataType.Parametro)
            If Not oBeTrans_re_tr.TipoTrans Is Nothing Then Upd.Add("tipotrans", "@tipotrans", DataType.Parametro)
            If Not oBeTrans_re_tr.ConRef Is Nothing Then Upd.Add("conref", "@conref", DataType.Parametro)
            If Not oBeTrans_re_tr.ConRef Is Nothing Then Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdTipoTransaccion = @IdTipoTransaccion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            If Not oBeTrans_re_tr.IdTipoTransaccion Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTrans_re_tr.IdTipoTransaccion))
            If Not oBeTrans_re_tr.Descripcion Is Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_re_tr.Descripcion))
            If Not oBeTrans_re_tr.Funcionalidad Is Nothing Then cmd.Parameters.Add(New SqlParameter("@FUNCIONALIDAD", oBeTrans_re_tr.Funcionalidad))
            If Not oBeTrans_re_tr.UsaHH Is Nothing Then cmd.Parameters.Add(New SqlParameter("@USAHH", oBeTrans_re_tr.UsaHH))
            If Not oBeTrans_re_tr.DescDev Is Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCDEV", oBeTrans_re_tr.DescDev))
            If Not oBeTrans_re_tr.TipoTrans Is Nothing Then cmd.Parameters.Add(New SqlParameter("@TIPOTRANS", oBeTrans_re_tr.TipoTrans))
            If Not oBeTrans_re_tr.ConRef Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CONREF", oBeTrans_re_tr.ConRef))
            If Not oBeTrans_re_tr.ConRef Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_re_tr.Activo))

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
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeTrans_re_tr As clsBeTrans_re_tr, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_re_tr" &
             "  Where(IdTipoTransaccion = @IdTipoTransaccion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTrans_re_tr.IdTipoTransaccion))

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
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_re_tr"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
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
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_re_tr"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Function Obtener(ByRef oBeTrans_re_tr As clsBeTrans_re_tr) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Trans_re_tr" &
            " Where(IdTipoTransaccion = @IdTipoTransaccion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTrans_re_tr.IdTipoTransaccion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_tr, dt.Rows(0))
                Obtener = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR25092025: se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

    Public Function GetAll() As List(Of clsBeTrans_re_tr)

        Try

            Dim lReturnList As New List(Of clsBeTrans_re_tr)
            Const sp As String = "SELECT * FROM Trans_re_tr"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_re_tr As New clsBeTrans_re_tr

            For Each dr As DataRow In dt.Rows

                vBeTrans_re_tr = New clsBeTrans_re_tr
                Cargar(vBeTrans_re_tr, dr)
                lReturnList.Add(vBeTrans_re_tr)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Function GetSingle(ByRef pBeTrans_re_tr As clsBeTrans_re_tr)

        Try

            Const sp As String = "SELECT * FROM Trans_re_tr" &
            " Where(IdTipoTransaccion = @IdTipoTransaccion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", pBeTrans_re_tr.IdTipoTransaccion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Cargar(pBeTrans_re_tr, dt.Rows(0))

            End If

            Return pBeTrans_re_tr

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTipoTransaccion),0) FROM Trans_re_tr"

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
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Function GetSingle_By_IdTipoTransaccion(ByRef pIdTipoTransaccion As String) As clsBeTrans_re_tr

        GetSingle_By_IdTipoTransaccion = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_re_tr" &
            " Where(IdTipoTransaccion = @IdTipoTransaccion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", pIdTipoTransaccion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim oBeTrans_re_tr = New clsBeTrans_re_tr
                Cargar(oBeTrans_re_tr, dt.Rows(0))
                GetSingle_By_IdTipoTransaccion = oBeTrans_re_tr
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

End Class
