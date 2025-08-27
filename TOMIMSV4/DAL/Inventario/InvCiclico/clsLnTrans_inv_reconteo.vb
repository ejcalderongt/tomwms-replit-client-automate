Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_reconteo

    Public Shared Sub Cargar(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo, ByRef dr As DataRow)
        Try
            With oBeTrans_inv_reconteo
                .Idinvreconteo = IIf(IsDBNull(dr.Item("idinvreconteo")), 0, dr.Item("idinvreconteo"))
                .Idreconteo = IIf(IsDBNull(dr.Item("idreconteo")), 0, dr.Item("idreconteo"))
                .Idinvciclico = IIf(IsDBNull(dr.Item("idinvciclico")), 0, dr.Item("idinvciclico"))
                .Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUbicacionAnterior = IIf(IsDBNull(dr.Item("idUbicacionAnterior")), 0, dr.Item("idUbicacionAnterior"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .EsNuevo = IIf(IsDBNull(dr.Item("EsNuevo")), False, dr.Item("EsNuevo"))
                .CantidadAnterior = IIf(IsDBNull(dr.Item("cantidadAnterior")), 0.0, dr.Item("cantidadAnterior"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .PesoAnterior = IIf(IsDBNull(dr.Item("pesoAnterior")), 0.0, dr.Item("pesoAnterior"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
                .EsPallet = IIf(IsDBNull(dr.Item("EsPallet")), False, dr.Item("EsPallet"))
                .lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub Cargar(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo, ByRef record As IDataRecord)
        Try
            With oBeTrans_inv_reconteo
                .Idinvreconteo = ObtenerValor(Of Integer)(record, "idinvreconteo", 0)
                .Idreconteo = ObtenerValor(Of Integer)(record, "idreconteo", 0)
                .Idinvciclico = ObtenerValor(Of Integer)(record, "idinvciclico", 0)
                .Idinventarioenc = ObtenerValor(Of Integer)(record, "idinventarioenc", 0)
                .IdStock = ObtenerValor(Of Integer)(record, "IdStock", 0)
                .IdProductoBodega = ObtenerValor(Of Integer)(record, "IdProductoBodega", 0)
                .IdProductoEstado = ObtenerValor(Of Integer)(record, "IdProductoEstado", 0)
                .IdPresentacion = ObtenerValor(Of Integer)(record, "IdPresentacion", 0)
                .IdUbicacionAnterior = ObtenerValor(Of Integer)(record, "idUbicacionAnterior", 0)
                .IdUbicacion = ObtenerValor(Of Integer)(record, "IdUbicacion", 0)
                .EsNuevo = ObtenerValor(Of Boolean)(record, "EsNuevo", False)
                .CantidadAnterior = ObtenerValor(Of Double)(record, "cantidadAnterior", 0.0)
                .Cantidad = ObtenerValor(Of Double)(record, "cantidad", 0.0)
                .Lote = ObtenerValor(Of String)(record, "lote", "")
                .Fecha_vence = ObtenerValor(Of Date)(record, "fecha_vence", New Date(1900, 1, 1))
                .PesoAnterior = ObtenerValor(Of Double)(record, "pesoAnterior", 0.0)
                .Peso = ObtenerValor(Of Double)(record, "peso", 0.0)
                .User_agr = ObtenerValor(Of String)(record, "user_agr", "")
                .Fec_agr = ObtenerValor(Of Date)(record, "fec_agr", New Date(1900, 1, 1))
                .IdOperador = ObtenerValor(Of Integer)(record, "IdOperador", 0)
                .EsPallet = ObtenerValor(Of Boolean)(record, "EsPallet", False)
                .lic_plate = ObtenerValor(Of String)(record, "lic_plate", "")
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Shared Function ObtenerValor(Of T)(ByVal record As IDataRecord, ByVal columna As String, ByVal valorPorDefecto As T) As T
        Try
            Dim ordinal As Integer = record.GetOrdinal(columna)
            If record.IsDBNull(ordinal) Then
                Return valorPorDefecto
            Else
                Return record.GetValue(ordinal)
            End If
        Catch ex As Exception
            Throw New Exception($"Error al obtener valor de la columna '{columna}': {ex.Message}")
        End Try
    End Function


    Public Shared Function Insertar(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_reconteo")
            Ins.Add("idinvreconteo", "@idinvreconteo", DataType.Parametro)
            Ins.Add("idreconteo", "@idreconteo", DataType.Parametro)
            Ins.Add("idinvciclico", "@idinvciclico", DataType.Parametro)
            Ins.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idubicacionanterior", "@idubicacionanterior", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("esnuevo", "@esnuevo", DataType.Parametro)
            Ins.Add("cantidadanterior", "@cantidadanterior", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("pesoanterior", "@pesoanterior", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("espallet", "@espallet", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDINVRECONTEO", oBeTrans_inv_reconteo.Idinvreconteo))
            cmd.Parameters.Add(New SqlParameter("@IDRECONTEO", oBeTrans_inv_reconteo.Idreconteo))
            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_reconteo.Idinvciclico))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_reconteo.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_reconteo.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_reconteo.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_reconteo.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_reconteo.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_inv_reconteo.IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_reconteo.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@ESNUEVO", oBeTrans_inv_reconteo.EsNuevo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDADANTERIOR", oBeTrans_inv_reconteo.CantidadAnterior))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_reconteo.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_reconteo.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_reconteo.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@PESOANTERIOR", oBeTrans_inv_reconteo.PesoAnterior))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_reconteo.Peso))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_reconteo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_reconteo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_reconteo.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeTrans_inv_reconteo.EsPallet))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_reconteo.lic_plate))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTrans_inv_reconteo.Idinvreconteo = CInt(cmd.Parameters("@IDINVRECONTEO").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_reconteo")
            Upd.Add("idinvreconteo", "@idinvreconteo", DataType.Parametro)
            Upd.Add("idreconteo", "@idreconteo", DataType.Parametro)
            Upd.Add("idinvciclico", "@idinvciclico", DataType.Parametro)
            Upd.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idubicacionanterior", "@idubicacionanterior", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("esnuevo", "@esnuevo", DataType.Parametro)
            Upd.Add("cantidadanterior", "@cantidadanterior", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("pesoanterior", "@pesoanterior", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("espallet", "@espallet", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Where("idinvreconteo = @idinvreconteo")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVRECONTEO", oBeTrans_inv_reconteo.Idinvreconteo))
            cmd.Parameters.Add(New SqlParameter("@IDRECONTEO", oBeTrans_inv_reconteo.Idreconteo))
            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_reconteo.Idinvciclico))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_reconteo.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_reconteo.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_reconteo.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_reconteo.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_reconteo.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_inv_reconteo.IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_reconteo.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@ESNUEVO", oBeTrans_inv_reconteo.EsNuevo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDADANTERIOR", oBeTrans_inv_reconteo.CantidadAnterior))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_reconteo.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_reconteo.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_reconteo.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@PESOANTERIOR", oBeTrans_inv_reconteo.PesoAnterior))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_reconteo.Peso))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_reconteo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_reconteo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_reconteo.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeTrans_inv_reconteo.EsPallet))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_reconteo.lic_plate))

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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_reconteo" &
             "  Where(idinvreconteo = @idinvreconteo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVRECONTEO", oBeTrans_inv_reconteo.Idinvreconteo))

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_reconteo"
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

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_inv_reconteo"
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

    Public Shared Function Obtener(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_reconteo" &
            " Where(idinvreconteo = @idinvreconteo)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVRECONTEO", oBeTrans_inv_reconteo.Idinvreconteo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_reconteo, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_reconteo)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_reconteo)
            Const sp As String = "SELECT * FROM Trans_inv_reconteo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_reconteo As New clsBeTrans_inv_reconteo

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_reconteo = New clsBeTrans_inv_reconteo
                Cargar(vBeTrans_inv_reconteo, dr)
                lReturnList.Add(vBeTrans_inv_reconteo)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_reconteo As clsBeTrans_inv_reconteo)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_reconteo" &
            " Where(idinvreconteo = @idinvreconteo)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVRECONTEO", pBeTrans_inv_reconteo.IDINVRECONTEO))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_reconteo, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinvreconteo),0) FROM Trans_inv_reconteo"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
