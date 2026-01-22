Imports System.Data.SqlClient

Public Class clsLnTrans_re_oc

    Public Shared Sub Cargar(ByRef oBeTrans_re_oc As clsBeTrans_re_oc, ByRef dr As DataRow)
        Try
            With oBeTrans_re_oc
                .IdRecepcionOc = IIf(IsDBNull(dr.Item("IdRecepcionOc")), 0, dr.Item("IdRecepcionOc"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .Recepcion_ciega = IIf(IsDBNull(dr.Item("recepcion_ciega")), False, dr.Item("recepcion_ciega"))
                .Recepcion_manual = IIf(IsDBNull(dr.Item("recepcion_manual")), False, dr.Item("recepcion_manual"))
                .No_docto = IIf(IsDBNull(dr.Item("no_docto")), "", dr.Item("no_docto"))
                .Hora_ini_hh = IIf(IsDBNull(dr.Item("hora_ini_hh")), Date.Now, dr.Item("hora_ini_hh"))
                .Hora_fin_hh = IIf(IsDBNull(dr.Item("hora_fin_hh")), Date.Now, dr.Item("hora_fin_hh"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .Firma_operador = IIf(IsDBNull(dr.Item("firma_operador")), Nothing, dr.Item("firma_operador"))
                .No_Erp_Docentry_Entrega = IIf(IsDBNull(dr.Item("No_Erp_Docentry_Entrega")), "", dr.Item("No_Erp_Docentry_Entrega"))
                .No_Erp_Docnum_Entrega = IIf(IsDBNull(dr.Item("No_Erp_Docnum_Entrega")), "", dr.Item("No_Erp_Docnum_Entrega"))
                .No_Erp_Docentry_Faltante = IIf(IsDBNull(dr.Item("No_Erp_Docentry_Faltante")), "", dr.Item("No_Erp_Docentry_Faltante"))
                .No_Erp_Docnum_Faltante = IIf(IsDBNull(dr.Item("No_Erp_Docnum_Faltante")), "", dr.Item("No_Erp_Docnum_Faltante"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_re_oc As clsBeTrans_re_oc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_re_oc")
            Ins.Add("idrecepcionoc", "@idrecepcionoc", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("recepcion_ciega", "@recepcion_ciega", DataType.Parametro)
            Ins.Add("recepcion_manual", "@recepcion_manual", DataType.Parametro)
            Ins.Add("no_docto", "@no_docto", DataType.Parametro)
            Ins.Add("hora_ini_hh", "@hora_ini_hh", DataType.Parametro)
            Ins.Add("hora_fin_hh", "@hora_fin_hh", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("firma_operador", "@firma_operador", DataType.Parametro)
            Ins.Add("No_Erp_Docentry_Entrega", "@No_Erp_Docentry_Entrega", DataType.Parametro)
            Ins.Add("No_Erp_Docnum_Entrega", "@No_Erp_Docnum_Entrega", DataType.Parametro)
            Ins.Add("No_Erp_Docentry_Faltante", "@No_Erp_Docentry_Faltante", DataType.Parametro)
            Ins.Add("No_Erp_Docnum_Faltante", "@No_Erp_Docnum_Faltante", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            '#EJC20191205: Trans_Ref02
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONOC", oBeTrans_re_oc.IdRecepcionOc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_oc.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_re_oc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@RECEPCION_CIEGA", oBeTrans_re_oc.Recepcion_ciega))
            cmd.Parameters.Add(New SqlParameter("@RECEPCION_MANUAL", oBeTrans_re_oc.Recepcion_manual))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCTO", oBeTrans_re_oc.No_docto))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI_HH", oBeTrans_re_oc.Hora_ini_hh))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN_HH", oBeTrans_re_oc.Hora_fin_hh))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_oc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_oc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCENTRY_ENTREGA", oBeTrans_re_oc.No_Erp_Docentry_Entrega))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCNUM_ENTREGA", oBeTrans_re_oc.No_Erp_Docnum_Entrega))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCENTRY_FALTANTE", oBeTrans_re_oc.No_Erp_Docentry_Faltante))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCNUM_FALTANTE", oBeTrans_re_oc.No_Erp_Docnum_Faltante))

            If oBeTrans_re_oc.Firma_operador IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@firma_operador", oBeTrans_re_oc.Firma_operador))
            Else
                cmd.Parameters.Add(New SqlParameter("@firma_operador", SqlDbType.Image)).Value = DBNull.Value
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_re_oc As clsBeTrans_re_oc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_oc")
            Upd.Add("idrecepcionoc", "@idrecepcionoc", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("recepcion_ciega", "@recepcion_ciega", DataType.Parametro)
            Upd.Add("recepcion_manual", "@recepcion_manual", DataType.Parametro)
            Upd.Add("no_docto", "@no_docto", DataType.Parametro)
            Upd.Add("firma_operador", "@firma_operador", DataType.Parametro)
            Upd.Add("No_Erp_Docentry_Entrega", "@No_Erp_Docentry_Entrega", DataType.Parametro)
            Upd.Add("No_Erp_Docnum_Entrega", "@No_Erp_Docnum_Entrega", DataType.Parametro)
            Upd.Add("No_Erp_Docentry_Faltante", "@No_Erp_Docentry_Faltante", DataType.Parametro)
            Upd.Add("No_Erp_Docnum_Faltante", "@No_Erp_Docnum_Faltante", DataType.Parametro)
            Upd.Where("IdRecepcionOc = @IdRecepcionOc " &
                      "AND IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            '#EJC20191205: Trans_Ref02
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONOC", oBeTrans_re_oc.IdRecepcionOc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_oc.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_re_oc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@RECEPCION_CIEGA", oBeTrans_re_oc.Recepcion_ciega))
            cmd.Parameters.Add(New SqlParameter("@RECEPCION_MANUAL", oBeTrans_re_oc.Recepcion_manual))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCTO", oBeTrans_re_oc.No_docto))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCENTRY_ENTREGA", oBeTrans_re_oc.No_Erp_Docentry_Entrega))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCNUM_ENTREGA", oBeTrans_re_oc.No_Erp_Docnum_Entrega))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCENTRY_FALTANTE", oBeTrans_re_oc.No_Erp_Docentry_Faltante))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCNUM_FALTANTE", oBeTrans_re_oc.No_Erp_Docnum_Faltante))

            If oBeTrans_re_oc.Firma_operador IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@firma_operador", oBeTrans_re_oc.Firma_operador))
            Else
                cmd.Parameters.Add(New SqlParameter("@firma_operador", SqlDbType.Image)).Value = DBNull.Value
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_No_Docto(ByRef oBeTrans_re_oc As clsBeTrans_re_oc,
                                               Optional ByVal pConection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_oc")
            Upd.Add("no_docto", "@no_docto", DataType.Parametro)
            Upd.Where("IdRecepcionOc = @IdRecepcionOc 
                       AND IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            '#EJC20191205: Trans_Ref02
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONOC", oBeTrans_re_oc.IdRecepcionOc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_oc.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCTO", oBeTrans_re_oc.No_docto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Hora_Fin_Recepcion(ByRef oBeTrans_re_oc As clsBeTrans_re_oc,
                                                         Optional ByVal pConection As SqlConnection = Nothing,
                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_oc")
            Upd.Add("hora_fin_hh", "@Hora_Fin_Recepcion ", DataType.Parametro)
            Upd.Where("IdRecepcionOc = @IdRecepcionOc 
                       AND IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            '#EJC20191205: Trans_Ref02
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONOC", oBeTrans_re_oc.IdRecepcionOc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_oc.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@Hora_Fin_Recepcion ", oBeTrans_re_oc.Hora_fin_hh))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Hora_Inicio_Recepcion(ByVal pIdOredenCompraEnc As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_oc_enc")
            Upd.Add("Hora_Inicio_Recepcion ", "@Hora_Inicio_Recepcion ", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc")

            Dim sp As String = Upd.SQL()

            '#EJC20191205: Trans_Ref02
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", pIdOredenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@HORA_INICIO_RECEPCION  ", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_re_oc As clsBeTrans_re_oc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_re_oc" &
             "  Where(IdRecepcionOc = @IdRecepcionOc) " &
             "  AND (IdRecepcionEnc = @IdRecepcionEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONOC", oBeTrans_re_oc.IdRecepcionOc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_oc.IdRecepcionEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_re_oc As clsBeTrans_re_oc) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_re_oc" &
            " Where(IdRecepcionOc = @IdRecepcionOc)" &
            "AND (IdRecepcionEnc = @IdRecepcionEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONOC", oBeTrans_re_oc.IdRecepcionOc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_oc.IdRecepcionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_oc, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_No_Entrega_ERP(ByRef oBeTrans_re_oc As clsBeTrans_re_oc,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            ' Build UPDATE
            Upd.Init("trans_re_oc")
            Upd.Add("no_erp_docentry_entrega", "@no_erp_docentry_entrega", DataType.Parametro)
            Upd.Add("no_erp_docnum_entrega", "@no_erp_docnum_entrega", DataType.Parametro)
            Upd.Where("IdRecepcionOc = @IdRecepcionOc AND IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            ' Transacción remota/local
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            ' Parámetros clave
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONOC", oBeTrans_re_oc.IdRecepcionOc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_oc.IdRecepcionEnc))

            ' Parámetros de los NVARCHAR(50) con manejo de nulos
            Dim pDocEntry As Object = If(String.IsNullOrWhiteSpace(oBeTrans_re_oc.No_Erp_Docentry_Entrega), CType(DBNull.Value, Object), oBeTrans_re_oc.No_Erp_Docentry_Entrega)
            Dim pDocNum As Object = If(String.IsNullOrWhiteSpace(oBeTrans_re_oc.No_Erp_Docnum_Entrega), CType(DBNull.Value, Object), oBeTrans_re_oc.No_Erp_Docnum_Entrega)

            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCENTRY_ENTREGA", pDocEntry))
            cmd.Parameters.Add(New SqlParameter("@NO_ERP_DOCNUM_ENTREGA", pDocNum))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function


End Class