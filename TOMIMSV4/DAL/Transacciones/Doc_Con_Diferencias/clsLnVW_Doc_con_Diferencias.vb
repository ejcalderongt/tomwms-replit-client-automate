Imports System.Data.SqlClient

Public Class clsLnVW_Doc_con_Diferencias

    Public Shared Sub Cargar(ByRef oBeVW_Doc_con_Diferencias As clsBeVW_Doc_Con_Diferencias, ByRef dr As DataRow)
        Try
            With oBeVW_Doc_con_Diferencias
                .ORDENCOMPRA = IIf(IsDBNull(dr.Item("ORDENCOMPRA")), "", dr.Item("ORDENCOMPRA"))
                .CODIGO_PRODUCTO = IIf(IsDBNull(dr.Item("CODIGO_PRODUCTO")), "", dr.Item("CODIGO_PRODUCTO"))
                .NOMBRE_PRODUCTO = IIf(IsDBNull(dr.Item("NOMBRE_PRODUCTO")), "", dr.Item("NOMBRE_PRODUCTO"))
                .CANTIDAD = IIf(IsDBNull(dr.Item("CANTIDAD")), 0.0, dr.Item("CANTIDAD"))
                .CANTIDAD_RECIBIDA = IIf(IsDBNull(dr.Item("CANTIDAD_RECIBIDA")), 0.0, dr.Item("CANTIDAD_RECIBIDA"))
                .PRESENTACION = IIf(IsDBNull(dr.Item("PRESENTACION")), "", dr.Item("PRESENTACION"))
                .DIFERENCIA = IIf(IsDBNull(dr.Item("DIFERENCIA")), 0.0, dr.Item("DIFERENCIA"))
                .IDPROPIETARIOBODEGA = IIf(IsDBNull(dr.Item("IDPROPIETARIOBODEGA")), 0, dr.Item("IDPROPIETARIOBODEGA"))
                .BODEGA = IIf(IsDBNull(dr.Item("BODEGA")), "", dr.Item("BODEGA"))
                .PROPIETARIO = IIf(IsDBNull(dr.Item("PROPIETARIO")), "", dr.Item("PROPIETARIO"))
                .IDPROVEEDORBODEGA = IIf(IsDBNull(dr.Item("IDPROVEEDORBODEGA")), 0, dr.Item("IDPROVEEDORBODEGA"))
                .IDTIPOINGRESOOC = IIf(IsDBNull(dr.Item("IDTIPOINGRESOOC")), 0, dr.Item("IDTIPOINGRESOOC"))
                .NOMBRE_INGRESOOC = IIf(IsDBNull(dr.Item("NOMBRE_INGRESOOC")), "", dr.Item("NOMBRE_INGRESOOC"))
                .IDPRODUCTOBODEGA = IIf(IsDBNull(dr.Item("IDPRODUCTOBODEGA")), 0, dr.Item("IDPRODUCTOBODEGA"))
                .IDPRESENTACION = IIf(IsDBNull(dr.Item("IDPRESENTACION")), 0, dr.Item("IDPRESENTACION"))
                .IDUNIDADMEDIDABASICA = IIf(IsDBNull(dr.Item("IDUNIDADMEDIDABASICA")), 0, dr.Item("IDUNIDADMEDIDABASICA"))
                .UMBAS = IIf(IsDBNull(dr.Item("UMBAS")), 0, dr.Item("UMBAS"))
                .ESTADO = IIf(IsDBNull(dr.Item("ESTADO")), "", dr.Item("ESTADO"))
                .ACTIVO = IIf(IsDBNull(dr.Item("ACTIVO")), False, dr.Item("ACTIVO"))
                .FECHA_CREACION = IIf(IsDBNull(dr.Item("FECHA_CREACION")), "", dr.Item("FECHA_CREACION"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Public Shared Function Insertar(ByRef oBeVW_Doc_con_Diferencias As clsBeVW_Doc_Con_Diferencias, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

    '	Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
    '	Dim lTransaction As SqlTransaction = Nothing

    '	Try

    '		Ins.Init("vw_doc_con_diferencias")
    '		Ins.Add("ordencompra", "@ordencompra", DataType.Parametro)
    '		Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
    '		Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
    '		Ins.Add("cantidad", "@cantidad", DataType.Parametro)
    '		Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
    '		Ins.Add("um", "@um", DataType.Parametro)
    '		Ins.Add("diferencia", "@diferencia", DataType.Parametro)
    '		Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
    '		Ins.Add("bodega", "@bodega", DataType.Parametro)
    '		Ins.Add("propietario", "@propietario", DataType.Parametro)
    '		Ins.Add("idproveedorbodega", "@idproveedorbodega", DataType.Parametro)
    '		Ins.Add("idtipoingresooc", "@idtipoingresooc", DataType.Parametro)
    '		Ins.Add("nombre_ingresooc", "@nombre_ingresooc", DataType.Parametro)
    '		Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
    '		Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
    '		Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
    '		Ins.Add("estado", "@estado", DataType.Parametro)
    '		Ins.Add("activo", "@activo", DataType.Parametro)

    '		Dim sp As String = Ins.SQL()
    '		Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

    '		Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

    '		If Es_Transaccion_Remota Then
    '			cmd = New SqlCommand(sp, pConection, pTransaction)
    '		Else
    '			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
    '			cmd = New SqlCommand(sp, lConnection, lTransaction)
    '		End If

    '		cmd.Parameters.Add(New SqlParameter("@ORDENCOMPRA", oBeVW_Doc_con_Diferencias.ORDENCOMPRA))
    '		cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeVW_Doc_con_Diferencias.CODIGO_PRODUCTO))
    '		cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeVW_Doc_con_Diferencias.NOMBRE_PRODUCTO))
    '		cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeVW_Doc_con_Diferencias.CANTIDAD))
    '		cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeVW_Doc_con_Diferencias.CANTIDAD_RECIBIDA))
    '		cmd.Parameters.Add(New SqlParameter("@UM", oBeVW_Doc_con_Diferencias.UM))
    '		cmd.Parameters.Add(New SqlParameter("@DIFERENCIA", oBeVW_Doc_con_Diferencias.DIFERENCIA))
    '		cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeVW_Doc_con_Diferencias.IDPROPIETARIOBODEGA))
    '		cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeVW_Doc_con_Diferencias.BODEGA))
    '		cmd.Parameters.Add(New SqlParameter("@PROPIETARIO", oBeVW_Doc_con_Diferencias.PROPIETARIO))
    '		cmd.Parameters.Add(New SqlParameter("@IDPROVEEDORBODEGA", oBeVW_Doc_con_Diferencias.IDPROVEEDORBODEGA))
    '		cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeVW_Doc_con_Diferencias.IDTIPOINGRESOOC))
    '		cmd.Parameters.Add(New SqlParameter("@NOMBRE_INGRESOOC", oBeVW_Doc_con_Diferencias.NOMBRE_INGRESOOC))
    '		cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeVW_Doc_con_Diferencias.IDPRODUCTOBODEGA))
    '		cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeVW_Doc_con_Diferencias.IDPRESENTACION))
    '		cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeVW_Doc_con_Diferencias.IDUNIDADMEDIDABASICA))
    '		cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeVW_Doc_con_Diferencias.ESTADO))
    '		cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeVW_Doc_con_Diferencias.ACTIVO))

    '		Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '		cmd.Dispose()

    '		If Not Es_Transaccion_Remota Then lTransaction.Commit()

    '		Return rowsAffected

    '	Catch ex As Exception
    '		If Not lTransaction Is Nothing Then lTransaction.Rollback()
    '		Throw ex
    '	Finally
    '		If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '		If Not lConnection Is Nothing Then lConnection.Dispose()
    '		If Not lTransaction Is Nothing Then lTransaction.Dispose()
    '	End Try

    'End Function

    'Public Shared Function Actualizar(ByRef oBeVW_Doc_con_Diferencias As clsBeVW_Doc_Con_Diferencias, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

    '	Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
    '	Dim lTransaction As SqlTransaction = Nothing

    '	Try

    '		Upd.Init("vw_doc_con_diferencias")
    '		Upd.Add("ordencompra", "@ordencompra", DataType.Parametro)
    '		Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
    '		Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
    '		Upd.Add("cantidad", "@cantidad", DataType.Parametro)
    '		Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
    '		Upd.Add("um", "@um", DataType.Parametro)
    '		Upd.Add("diferencia", "@diferencia", DataType.Parametro)
    '		Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
    '		Upd.Add("bodega", "@bodega", DataType.Parametro)
    '		Upd.Add("propietario", "@propietario", DataType.Parametro)
    '		Upd.Add("idproveedorbodega", "@idproveedorbodega", DataType.Parametro)
    '		Upd.Add("idtipoingresooc", "@idtipoingresooc", DataType.Parametro)
    '		Upd.Add("nombre_ingresooc", "@nombre_ingresooc", DataType.Parametro)
    '		Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
    '		Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
    '		Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
    '		Upd.Add("estado", "@estado", DataType.Parametro)
    '		Upd.Add("activo", "@activo", DataType.Parametro)
    '		Upd.Add("activo", "@activo", DataType.Parametro)

    '		Dim sp As String = Upd.SQL()

    '		Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

    '		Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

    '		If Es_Transaccion_Remota Then
    '			cmd = New SqlCommand(sp, pConection, pTransaction)
    '		Else
    '			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
    '			cmd = New SqlCommand(sp, lConnection, lTransaction)
    '		End If

    '		cmd.Parameters.Add(New SqlParameter("@ORDENCOMPRA", oBeVW_Doc_con_Diferencias.ORDENCOMPRA))
    '		cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeVW_Doc_con_Diferencias.CODIGO_PRODUCTO))
    '		cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeVW_Doc_con_Diferencias.NOMBRE_PRODUCTO))
    '		cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeVW_Doc_con_Diferencias.CANTIDAD))
    '		cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeVW_Doc_con_Diferencias.CANTIDAD_RECIBIDA))
    '		cmd.Parameters.Add(New SqlParameter("@UM", oBeVW_Doc_con_Diferencias.UM))
    '		cmd.Parameters.Add(New SqlParameter("@DIFERENCIA", oBeVW_Doc_con_Diferencias.DIFERENCIA))
    '		cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeVW_Doc_con_Diferencias.IDPROPIETARIOBODEGA))
    '		cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeVW_Doc_con_Diferencias.BODEGA))
    '		cmd.Parameters.Add(New SqlParameter("@PROPIETARIO", oBeVW_Doc_con_Diferencias.PROPIETARIO))
    '		cmd.Parameters.Add(New SqlParameter("@IDPROVEEDORBODEGA", oBeVW_Doc_con_Diferencias.IDPROVEEDORBODEGA))
    '		cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeVW_Doc_con_Diferencias.IDTIPOINGRESOOC))
    '		cmd.Parameters.Add(New SqlParameter("@NOMBRE_INGRESOOC", oBeVW_Doc_con_Diferencias.NOMBRE_INGRESOOC))
    '		cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeVW_Doc_con_Diferencias.IDPRODUCTOBODEGA))
    '		cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeVW_Doc_con_Diferencias.IDPRESENTACION))
    '		cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeVW_Doc_con_Diferencias.IDUNIDADMEDIDABASICA))
    '		cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeVW_Doc_con_Diferencias.ESTADO))
    '		cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeVW_Doc_con_Diferencias.ACTIVO))

    '		Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '		cmd.Dispose()

    '		If Not Es_Transaccion_Remota Then lTransaction.Commit()

    '		Return rowsAffected

    '	Catch ex As Exception
    '		If Not lTransaction Is Nothing Then lTransaction.Rollback()
    '		Throw ex
    '	Finally
    '		If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '		If Not lConnection Is Nothing Then lConnection.Dispose()
    '		If Not lTransaction Is Nothing Then lTransaction.Dispose()
    '	End Try

    'End Function


    'Public Shared Function Eliminar(ByRef oBeVW_Doc_con_Diferencias As clsBeVW_Doc_Con_Diferencias, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

    '	Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
    '	Dim lTransaction As SqlTransaction = Nothing

    '	Try

    '		Const sp As String = " Delete from VW_Doc_con_Diferencias"

    '		Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

    '		Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

    '		If Es_Transaccion_Remota Then
    '			cmd = New SqlCommand(sp, pConection, pTransaction)
    '		Else
    '			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
    '			cmd = New SqlCommand(sp, lConnection, lTransaction)
    '		End If


    '		Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '		cmd.Dispose()

    '		If Not Es_Transaccion_Remota Then lTransaction.Commit()

    '		Return rowsAffected

    '	Catch ex As Exception
    '		If Not lTransaction Is Nothing Then lTransaction.Rollback()
    '		Throw ex
    '	Finally
    '		If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '		If Not lConnection Is Nothing Then lConnection.Dispose()
    '		If Not lTransaction Is Nothing Then lTransaction.Dispose()
    '	End Try

    'End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Doc_con_Diferencias"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeVW_Doc_Con_Diferencias)

        Dim lReturnList As New List(Of clsBeVW_Doc_Con_Diferencias)

        Try

            Const sp As String = "SELECT * FROM VW_Doc_con_Diferencias"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeVW_Doc_con_DiferenciasOC As New clsBeVW_Doc_Con_Diferencias

                        For Each dr As DataRow In lDataTable.Rows
                            vBeVW_Doc_con_DiferenciasOC = New clsBeVW_Doc_Con_Diferencias()
                            Cargar(vBeVW_Doc_con_DiferenciasOC, dr)
                            lReturnList.Add(vBeVW_Doc_con_DiferenciasOC)
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

    Public Shared Sub GetSingle(ByRef pBeVW_Doc_con_Diferencias As clsBeVW_Doc_Con_Diferencias)

        Try

            Const sp As String = "SELECT * FROM VW_Doc_con_Diferencias"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeVW_Doc_con_Diferencias As New clsBeVW_Doc_Con_Diferencias

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeVW_Doc_con_Diferencias, lDataTable.Rows(0))
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

            Const sp As String = "SELECT * FROM VW_Doc_con_Diferencias"

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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_DT_By_IdProducto(ByVal pFechaDel As Date,
                                                                ByVal pFechaAl As Date,
                                                                ByVal pIdProductoBodega As Integer,
                                                                ByVal pIdBodega As Integer,
                                                                ByVal pIdPropietario As Integer) As DataTable

        Get_All_Movimientos_DT_By_IdProducto = Nothing

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT PROPIETARIO as Propietario, codigobodega AS CodigoBodega, IdOrdenCompraEnc, 
					No_Documento, codigo_producto as Codigo_Producto, 
					nombre_producto as Nombre_Producto, UMBas,PRESENTACION as Presentacion, 
					cantidad as Solicitado, cantidad_recibida as Recibido, diferencia as Diferencia,
					Referencia as Referencia,
					NOMBRE_INGRESOOC as TipoDocumento, Fecha_Creacion as Fecha_Documento, 
                    ESTADO as Estado_Documento, Enviado_A_ERP MI3_Estatus, Talla, Color, Observacion 
					FROM VW_Doc_Con_Diferencias
                    WHERE DIFERENCIA <> 0 "

            If pIdProductoBodega <> 0 Then
                vSQL += " AND IDPRODUCTOBODEGA=@IdProductoBodega"
            End If

            vSQL += " AND IdBodega=@IdBodega and IdPropietario=@IdPropietario"

            vSQL += String.Format(" And cast(FECHA_CREACION AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " ORDER BY FECHA_CREACION,ORDENCOMPRA"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdProductoBodega <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        End If

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then
                            Get_All_Movimientos_DT_By_IdProducto = lTable
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


    Public Shared Function Get_All_Movimientos_By_IdProducto(ByVal pFechaDel As Date,
                                                             ByVal pFechaAl As Date,
                                                             ByVal pIdProductoBodega As Integer,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal pIdPropietario As Integer) As List(Of clsBeVW_Doc_Con_Diferencias)

        Dim lReturnList As New List(Of clsBeVW_Doc_Con_Diferencias)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT IDORDENCOMPRAENC AS IDDOCUMENTOINGRESO, ORDENCOMPRA,CODIGO_PRODUCTO,NOMBRE_PRODUCTO,CANTIDAD,CANTIDAD_RECIBIDA,
					DIFERENCIA,PRESENTACION,IDPROPIETARIOBODEGA,BODEGA,PROPIETARIO,IDPROVEEDORBODEGA,
					IDTIPOINGRESOOC,NOMBRE_INGRESOOC,IDPRODUCTOBODEGA,IDPRESENTACION,IDUNIDADMEDIDABASICA,UMBas,
					ESTADO,ACTIVO,FECHA_CREACION, OBSERVACION
                    FROM VW_Doc_Con_Diferencias
                    WHERE  DIFERENCIA <>0 AND IDPRODUCTOBODEGA=@IdProductoBodega"

            vSQL += " AND IdBodega=@IdBodega and IdPropietario=@IdPropietario"

            vSQL += String.Format(" And cast(FECHA_CREACION AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " ORDER BY FECHA_CREACION,ORDENCOMPRA"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Doc_Con_Diferencias

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_Doc_Con_Diferencias
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                        End If

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

    Public Shared Function Get_All_Movimientos_By_IdPropietario_And_Bodega(ByVal pFechaDel As Date,
                                                                           ByVal pFechaAl As Date,
                                                                           ByVal pIdBodega As Integer,
                                                                           ByVal pIdPropietario As Integer) As List(Of clsBeVW_Doc_Con_Diferencias)

        Dim lReturnList As New List(Of clsBeVW_Doc_Con_Diferencias)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT ORDENCOMPRA,CODIGO_PRODUCTO,NOMBRE_PRODUCTO,CANTIDAD,CANTIDAD_RECIBIDA,
					DIFERENCIA,PRESENTACION,IDPROPIETARIOBODEGA,BODEGA,PROPIETARIO,POLIZA,IDPROVEEDORBODEGA,
					IDTIPOINGRESOOC,NOMBRE_INGRESOOC,IDPRODUCTOBODEGA,IDPRESENTACION,IDUNIDADMEDIDABASICA,UMBas,
					ESTADO,ACTIVO,FECHA_CREACION, OBSERVACION
                    FROM VW_Doc_Con_Diferencias
                    WHERE DIFERENCIA <>0 AND IdBodega=@IdBodega AND IdPropietario=@IdPropietario"

            vSQL += String.Format(" And cast(FECHA_CREACION AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " ORDER BY FECHA_CREACION,ORDENCOMPRA"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Doc_Con_Diferencias

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_Doc_Con_Diferencias
                                clsLnVW_Doc_con_Diferencias.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                        End If

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

End Class
