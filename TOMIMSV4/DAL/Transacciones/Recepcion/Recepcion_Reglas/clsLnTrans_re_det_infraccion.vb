Imports System.Data.SqlClient

Public Class clsLnTrans_re_det_infraccion

    Public Shared Sub Cargar(ByRef oBeTrans_re_det_infraccion As clsBeTrans_re_det_infraccion, ByRef dr As DataRow)
        Try
            With oBeTrans_re_det_infraccion
                .IdRecepcionDetInfraccion = IIf(IsDBNull(dr.Item("IdRecepcionDetInfraccion")), 0, dr.Item("IdRecepcionDetInfraccion"))
                .IdReglaPropietarioEnc = IIf(IsDBNull(dr.Item("IdReglaPropietarioEnc")), 0, dr.Item("IdReglaPropietarioEnc"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw New Exception("Trans_re_det_infraccion_Cargar: " & ex.message)
        End Try

    End Sub


    Public Function Insertar(ByRef oBeTrans_re_det_infraccion As clsBeTrans_re_det_infraccion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_re_det_infraccion")
            Ins.Add("idrecepciondetinfraccion", "@idrecepciondetinfraccion", DataType.Parametro)
            Ins.Add("idreglapropietarioenc", "@idreglapropietarioenc", DataType.Parametro)
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDETINFRACCION", oBeTrans_re_det_infraccion.IdRecepcionDetInfraccion))
            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIOENC", oBeTrans_re_det_infraccion.IdReglaPropietarioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", IIf(oBeTrans_re_det_infraccion.IdOrdenCompraEnc = 0, DBNull.Value, oBeTrans_re_det_infraccion.IdOrdenCompraEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IIf(oBeTrans_re_det_infraccion.IdRecepcionEnc = 0, DBNull.Value, oBeTrans_re_det_infraccion.IdRecepcionEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_re_det_infraccion.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_re_det_infraccion.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_det_infraccion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_det_infraccion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_re_det_infraccion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_re_det_infraccion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_re_det_infraccion.Activo))

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


    Public Function Actualizar(ByRef oBeTrans_re_det_infraccion As clsBeTrans_re_det_infraccion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_det_infraccion")
            Upd.Add("idrecepciondetinfraccion", "@idrecepciondetinfraccion", DataType.Parametro)
            Upd.Add("idreglapropietarioenc", "@idreglapropietarioenc", DataType.Parametro)
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdRecepcionDetInfraccion = @IdRecepcionDetInfraccion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDETINFRACCION", oBeTrans_re_det_infraccion.IdRecepcionDetInfraccion))
            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIOENC", oBeTrans_re_det_infraccion.IdReglaPropietarioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", IIf(oBeTrans_re_det_infraccion.IdOrdenCompraEnc = 0, DBNull.Value, oBeTrans_re_det_infraccion.IdOrdenCompraEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IIf(oBeTrans_re_det_infraccion.IdRecepcionEnc = 0, DBNull.Value, oBeTrans_re_det_infraccion.IdRecepcionEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_re_det_infraccion.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_re_det_infraccion.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_det_infraccion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_det_infraccion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_re_det_infraccion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_re_det_infraccion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_re_det_infraccion.Activo))

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


    Public Function Eliminar(ByRef oBeTrans_re_det_infraccion As clsBeTrans_re_det_infraccion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_re_det_infraccion" &
             "  Where(IdRecepcionDetInfraccion = @IdRecepcionDetInfraccion)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDETINFRACCION", oBeTrans_re_det_infraccion.IdRecepcionDetInfraccion))

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


    Public Function Listar() As DataTable
        Try
            Dim sp As String = "SELECT * FROM Trans_re_det_infraccion"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close
            cmd.Dispose()
            dad.Dispose()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function Obtener(ByRef oBeTrans_re_det_infraccion As clsBeTrans_re_det_infraccion) As Boolean
        Try
            Dim sp As String = "SELECT * FROM Trans_re_det_infraccion" &
            " Where(IdRecepcionDetInfraccion = @IdRecepcionDetInfraccion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONDETINFRACCION", oBeTrans_re_det_infraccion.IdRecepcionDetInfraccion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_det_infraccion, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
