Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_ciclico_vw

    Public Shared Sub Cargar(ByRef oBeTrans_inv_ciclico_View As clsBeTrans_inv_ciclico_vw, ByRef dr As DataRow)

        Try

            With oBeTrans_inv_ciclico_View

                If dr.Table.Columns.Contains("idinvciclico") Then .Idinvciclico = IIf(IsDBNull(dr.Item("idinvciclico")), 0, dr.Item("idinvciclico"))
                .Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
                If dr.Table.Columns.Contains("IdStock") Then .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .EsNuevo = IIf(IsDBNull(dr.Item("EsNuevo")), False, dr.Item("EsNuevo"))
                .Lote_stock = IIf(IsDBNull(dr.Item("lote_stock")), "", dr.Item("lote_stock"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence_stock = IIf(IsDBNull(dr.Item("fecha_vence_stock")), "", dr.Item("fecha_vence_stock"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Cant_stock = IIf(IsDBNull(dr.Item("cant_stock")), 0.0, dr.Item("cant_stock"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Cant_reconteo = IIf(IsDBNull(dr.Item("cant_reconteo")), 0.0, dr.Item("cant_reconteo"))
                .Peso_stock = IIf(IsDBNull(dr.Item("peso_stock")), 0.0, dr.Item("peso_stock"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Peso_reconteo = IIf(IsDBNull(dr.Item("peso_reconteo")), 0.0, dr.Item("peso_reconteo"))
                .Idoperador = IIf(IsDBNull(dr.Item("idoperador")), 0, dr.Item("idoperador"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .Estado_nombre = IIf(IsDBNull(dr.Item("estado_nombre")), "", dr.Item("estado_nombre"))
                .Producto_nombre = IIf(IsDBNull(dr.Item("producto_nombre")), "", dr.Item("producto_nombre"))
                .Ubic_nombre = IIf(IsDBNull(dr.Item("ubic_nombre")), "", dr.Item("ubic_nombre"))
                .Pres_nombre = IIf(IsDBNull(dr.Item("pres_nombre")), "", dr.Item("pres_nombre"))
                .Unid_nombre = IIf(IsDBNull(dr.Item("unid_nombre")), "", dr.Item("unid_nombre"))
                .Control_peso = IIf(IsDBNull(dr.Item("control_peso")), False, dr.Item("control_peso"))
                .Control_vencimiento = IIf(IsDBNull(dr.Item("Control_vencimiento")), False, dr.Item("Control_vencimiento"))
                .Genera_lote = IIf(IsDBNull(dr.Item("Genera_lote")), False, dr.Item("Genera_lote"))
                .idPresentacion_nuevo = IIf(IsDBNull(dr.Item("idPresentacion_nuevo")), 0, dr.Item("idPresentacion_nuevo"))
                .IdProductoEst_nuevo = IIf(IsDBNull(dr.Item("IdProductoEst_nuevo")), 0, dr.Item("IdProductoEst_nuevo"))
                .IdReconteo = IIf(IsDBNull(dr.Item("idreconteo")), 0, dr.Item("idreconteo"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Columna = IIf(IsDBNull(dr.Item("Columna")), 0, dr.Item("Columna"))
                .Nivel = IIf(IsDBNull(dr.Item("Nivel")), 0, dr.Item("Nivel"))
                .Posicion = IIf(IsDBNull(dr.Item("Posicion")), "", dr.Item("Posicion"))
                .Total = IIf(IsDBNull(dr.Item("Total")), 0, dr.Item("Total"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico_View"
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

            Const sp As String = "SELECT * FROM Trans_inv_ciclico_View"
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

    Public Shared Function Obtener(ByRef oBeTrans_inv_ciclico_View As clsBeTrans_inv_ciclico_vw) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico_View"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_ciclico_View, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_ciclico_vw)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico_vw)
            Const sp As String = "SELECT * FROM Trans_inv_ciclico_View"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico_View As New clsBeTrans_inv_ciclico_vw

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_ciclico_View = New clsBeTrans_inv_ciclico_vw
                Cargar(vBeTrans_inv_ciclico_View, dr)
                lReturnList.Add(vBeTrans_inv_ciclico_View)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_ciclico_View As clsBeTrans_inv_ciclico_vw)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico_View"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_ciclico_View, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
