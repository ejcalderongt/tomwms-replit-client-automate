Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_acuerdo_enc

    Public Shared Sub Cargar(ByRef oBeI_nav_acuerdo_enc As clsBeI_nav_acuerdo_enc, ByRef dr As DataRow)
        Try
            With oBeI_nav_acuerdo_enc

                .IdAcuerdo = IIf(IsDBNull(dr.Item("IdAcuerdo")), 0, dr.Item("IdAcuerdo"))
                .Idcliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Codigo_acuerdo = IIf(IsDBNull(dr.Item("codigo_acuerdo")), "", dr.Item("codigo_acuerdo"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Tipo_cobro = IIf(IsDBNull(dr.Item("tipo_cobro")), "", dr.Item("tipo_cobro"))
                .Cod_moneda = IIf(IsDBNull(dr.Item("cod_moneda")), 0, dr.Item("cod_moneda"))
                .Nom_moneda = IIf(IsDBNull(dr.Item("nom_moneda")), "", dr.Item("nom_moneda"))
                .Procesado_wms = IIf(IsDBNull(dr.Item("procesado_wms")), False, dr.Item("procesado_wms"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), False, dr.Item("estado"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_acuerdo_enc As clsBeI_nav_acuerdo_enc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_acuerdo_enc")
            Ins.Add("idacuerdo", "@idacuerdo", DataType.Parametro)
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("codigo_acuerdo", "@codigo_acuerdo", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("tipo_cobro", "@tipo_cobro", DataType.Parametro)
            Ins.Add("cod_moneda", "@cod_moneda", DataType.Parametro)
            Ins.Add("nom_moneda", "@nom_moneda", DataType.Parametro)
            Ins.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDO", oBeI_nav_acuerdo_enc.IdAcuerdo))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_acuerdo_enc.Idcliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeI_nav_acuerdo_enc.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeI_nav_acuerdo_enc.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_COBRO", oBeI_nav_acuerdo_enc.Tipo_cobro))
            cmd.Parameters.Add(New SqlParameter("@COD_MONEDA", oBeI_nav_acuerdo_enc.Cod_moneda))
            cmd.Parameters.Add(New SqlParameter("@NOM_MONEDA", oBeI_nav_acuerdo_enc.Nom_moneda))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_acuerdo_enc.Procesado_wms))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_acuerdo_enc.Estado))


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

    Public Shared Function Actualizar(ByRef oBeI_nav_acuerdo_enc As clsBeI_nav_acuerdo_enc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_acuerdo_enc")
            Upd.Add("idacuerdo", "@idacuerdo", DataType.Parametro)
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("codigo_acuerdo", "@codigo_acuerdo", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("tipo_cobro", "@tipo_cobro", DataType.Parametro)
            Upd.Add("cod_moneda", "@cod_moneda", DataType.Parametro)
            Upd.Add("nom_moneda", "@nom_moneda", DataType.Parametro)
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("IdAcuerdo = @IdAcuerdo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDO", oBeI_nav_acuerdo_enc.IdAcuerdo))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_acuerdo_enc.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeI_nav_acuerdo_enc.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeI_nav_acuerdo_enc.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_COBRO", oBeI_nav_acuerdo_enc.Tipo_cobro))
            cmd.Parameters.Add(New SqlParameter("@COD_MONEDA", oBeI_nav_acuerdo_enc.Cod_moneda))
            cmd.Parameters.Add(New SqlParameter("@NOM_MONEDA", oBeI_nav_acuerdo_enc.Nom_moneda))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_acuerdo_enc.Procesado_wms))

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

    Public Shared Function Eliminar(ByRef oBeI_nav_acuerdo_enc As clsBeI_nav_acuerdo_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_acuerdo_enc" &
             "  Where(IdAcuerdo = @IdAcuerdo)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDO", oBeI_nav_acuerdo_enc.IdAcuerdo))

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

    'Public Shared Function Get_All() As List(Of clsBeI_nav_acuerdo_enc)

    '	Dim lReturnList As New List(Of clsBeI_nav_acuerdo_enc)

    '	Try

    '		Const sp As String = "SELECT * FROM I_nav_acuerdo_enc "

    '		Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

    '			lConnection.Open()

    '			Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

    '				Using lDTA As New SqlDataAdapter(sp, lConnection)

    '				lDTA.SelectCommand.CommandType = CommandType.Text
    '				lDTA.SelectCommand.Transaction = lTransaction
    '				Dim lDataTable As New DataTable
    '				lDTA.Fill(lDataTable)

    '				Dim vBeI_nav_acuerdo_enc As New clsBeI_nav_acuerdo_enc

    '					For Each dr As DataRow In lDataTable.Rows
    '					vBeI_nav_acuerdo_enc = New clsBeI_nav_acuerdo_enc()
    '					Cargar(vBeI_nav_acuerdo_enc, dr)
    '					lReturnList.Add(vBeI_nav_acuerdo_enc)
    '					Next

    '				End Using

    '				lTransaction.Commit()

    '			End Using

    '			lConnection.Close()

    '		End Using

    '		Return lReturnList

    '	Catch ex As Exception
    '		Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '	End Try

    'End Function

    'Public Shared Sub GetSingle(ByRef pBeI_nav_acuerdo_enc As clsBeI_nav_acuerdo_enc) 

    '	Try

    '		Const sp As String = "SELECT * FROM I_nav_acuerdo_enc" & _ 
    '		" Where(IdAcuerdo = @IdAcuerdo)"


    '		Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

    '			lConnection.Open()

    '			Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

    '				Using lDTA As New SqlDataAdapter(sp, lConnection)

    '				lDTA.SelectCommand.CommandType = CommandType.Text
    '				lDTA.SelectCommand.Transaction = lTransaction
    '				Dim lDataTable As New DataTable
    '				lDTA.Fill(lDataTable)

    '				Dim vBeI_nav_acuerdo_enc As New clsBeI_nav_acuerdo_enc

    '				If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
    '					Cargar(vBeI_nav_acuerdo_enc, lDataTable.Rows(0))
    '				End If

    '				End Using

    '				lTransaction.Commit()

    '			End Using

    '			lConnection.Close()

    '		End Using

    '	Catch ex As Exception
    '		Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '	End Try

    'End Sub

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAcuerdo),0) FROM I_nav_acuerdo_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

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

    Public Shared Function Eliminar(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_acuerdo_enc"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

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

End Class
