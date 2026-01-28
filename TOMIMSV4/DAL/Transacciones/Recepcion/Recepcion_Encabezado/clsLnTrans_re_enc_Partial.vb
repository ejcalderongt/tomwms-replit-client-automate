Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Utils.Drawing.Helpers
Imports DevExpress.XtraEditors
Imports TOMWMS.clsDataContractDI

<Runtime.InteropServices.Guid("D88A0426-FE42-4D97-ACA3-E0BFE164A94D")>
Partial Public Class clsLnTrans_re_enc

    Public Shared Function Get_Impresion_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT *
                                  FROM VW_Reporte_Recepcion_20190726
                                  WHERE IdRecepcionEnc = @IdRecepcionEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Impresion_By_IdRecepcionEnc_SinOC(ByVal pIdRecepcionEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT *
                    FROM VW_Reporte_Recepcion_20190727 
                    WHERE trans_re_enc.IdRecepcionEnc = @IdRecepcionEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeTrans_re_enc)

        Dim lReturnList As New List(Of clsBeTrans_re_enc)

        Dim vSQL As String = "SELECT  pb.IdPropietario,p.nombre_comercial AS Propietario,b.nombre AS Bodega,tr.Descripcion, 
                ISNULL(u.nombres,'') + ' ' + ISNULL(u.apellidos,'') AS Usuario, enc.*  
                FROM Trans_re_enc AS enc INNER JOIN trans_re_tr AS tr ON enc.IdTipoTransaccion = tr.IdTipoTransaccion  
                INNER JOIN propietario_bodega AS pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega  
                INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario  
                INNER JOIN bodega AS b ON pb.IdBodega = b.IdBodega   
                INNER JOIN usuario AS u ON enc.user_agr = u.IdUsuario  
                WHERE estado='CERRADO' AND Revision_Inconsistencia = 0 AND pb.IdBodega=@IdBodega"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim Obj As clsBeTrans_re_enc

                            For Each lRow As DataRow In lDT.Rows

                                Obj = New clsBeTrans_re_enc()

                                Cargar(Obj, lRow)

                                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                    Obj.PropietarioBodega.IdPropietario = CType(lRow("IdPropietario"), Integer)
                                    Obj.NombrePropietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Bodega") IsNot DBNull.Value AndAlso lRow("Bodega") IsNot Nothing Then
                                    Obj.Bodega = CType(lRow("Bodega"), String)
                                End If

                                If lRow("IdTipoTransaccion") IsNot DBNull.Value AndAlso lRow("IdTipoTransaccion") IsNot Nothing Then
                                    Obj.Descripcion = CType(lRow("Descripcion"), String)
                                End If

                                Obj.IsNew = False

                                Obj.OrdenCompraRec = clsLnTrans_re_oc.GetSingle(Obj.IdRecepcionEnc, lConnection, lTransaction)
                                Obj.Detalle = clsLnTrans_re_det.Get_Detalle_By_IdRecepcionEnc(Obj.IdRecepcionEnc, Obj.IdBodega, lConnection, lTransaction)
                                Obj.DetalleFacturas = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(Obj.IdRecepcionEnc, lConnection, lTransaction)

                                lReturnList.Add(Obj)

                            Next

                        End If

                        lDT.Dispose()
                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()
                lConnection.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#EJC20180114: Transaccionalidad agregada
    Public Shared Function GetSingle(ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT b.descripcion AS UbicacionRecepcion, 
                                  tr.Descripcion, enc.IdRecepcionEnc, enc.IdPropietarioBodega, 
                                  enc.IdMuelle, enc.IdUbicacionRecepcion, enc.IdTipoTransaccion, 
                                  enc.fecha_recepcion, enc.hora_ini_pc, enc.hora_fin_pc, 
                                  enc.muestra_precio, enc.estado, enc.user_agr, enc.fec_agr, enc.user_mod, 
                                  enc.fec_mod, enc.fecha_tarea, enc.tomar_fotos, enc.escanear_rec_ubic, 
                                  enc.para_por_codigo, enc.observacion, enc.firma_piloto, enc.activo, 
                                  enc.NoGuia, enc.CorreoEnviado, enc.Revision_Inconsistencia, enc.bloqueada, 
                                  enc.bloqueada_por, enc.idusuariobloqueo, enc.idmotivoanulacionbodega, 
                                  enc.Habilitar_Stock, enc.idvehiculo, enc.idpiloto, enc.No_Marchamo, 
                                  enc.mostrar_cantidad_esperada, enc.IdBodega, enc.carta_cupo, no_contenedor,
                                  enc.IdEstado_Defecto_Recepcion
                                  FROM trans_re_enc AS enc INNER JOIN
                                  trans_re_tr AS tr ON enc.IdTipoTransaccion = tr.IdTipoTransaccion LEFT JOIN 
                                  bodega_ubicacion AS b ON enc.IdUbicacionRecepcion = b.IdUbicacion LEFT JOIN 
                                  propietario_bodega ON enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega AND b.IdBodega = propietario_bodega.IdBodega
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransReEnc As New clsBeTrans_re_enc()

                            Cargar(BeTransReEnc, lRow)

                            If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                                BeTransReEnc.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                                If clsLnPropietario_bodega.Obtener(BeTransReEnc.PropietarioBodega, lConnection, lTransaction) Then
                                    clsLnPropietarios.Obtener(BeTransReEnc.PropietarioBodega.Propietario, lConnection, lTransaction)
                                End If
                            End If

                            If lRow("IdUbicacionRecepcion") IsNot DBNull.Value AndAlso lRow("IdUbicacionRecepcion") IsNot Nothing Then
                                BeTransReEnc.UbicacionRecepcion = IIf(IsDBNull(lRow("UbicacionRecepcion")), 0, lRow("UbicacionRecepcion"))
                            End If

                            If lRow("IdTipoTransaccion") IsNot DBNull.Value AndAlso lRow("IdTipoTransaccion") IsNot Nothing Then
                                BeTransReEnc.Descripcion = CType(lRow("Descripcion"), String)
                            End If

                            '#EJC20190401: Agregado por optimización de carga en HH.
                            BeTransReEnc.Muelle = New clsBeBodega_muelles()
                            BeTransReEnc.Muelle.IdMuelle = BeTransReEnc.IdMuelle
                            clsLnBodega_muelles.GetSingle(BeTransReEnc.Muelle, lConnection, lTransaction)

                            BeTransReEnc.IsNew = False

                            BeTransReEnc.OrdenCompraRec = clsLnTrans_re_oc.GetSingle(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            BeTransReEnc.Detalle = clsLnTrans_re_det.Get_Detalle_By_IdRecepcionEnc(BeTransReEnc.IdRecepcionEnc, BeTransReEnc.IdBodega, lConnection, lTransaction)
                            BeTransReEnc.DetalleImagenes = clsLnTrans_re_img.Get_Detalle_Imagenes_By_IdRecepcionEnc(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            BeTransReEnc.DetalleParametros = clsLnTrans_re_det_parametros.Get_Detalle_Parametros_By_RecepcionEnc(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            BeTransReEnc.DetalleFacturas = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            BeTransReEnc.TareaHH = clsLnTarea_hh.GetSingle(1, BeTransReEnc.IdRecepcionEnc, lTransaction, lConnection)

                            Return BeTransReEnc

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

    '#CKFK_20181114: Función con transaccionalidad por referencia
    Public Shared Function GetSingle(ByVal pIdRecepcionEnc As Integer,
                                     ByRef pConnection As SqlConnection,
                                     ByRef pTransaction As SqlTransaction) As clsBeTrans_re_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT b.Descripcion AS UbicacionRecepcion,tr.Descripcion, enc.* FROM Trans_re_enc AS enc 
                                   INNER JOIN trans_re_tr AS tr ON enc.IdTipoTransaccion = tr.IdTipoTransaccion 
                                   INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionRecepcion = b.IdUbicacion 
                                   WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_re_enc()

                    Cargar(Obj, lRow)

                    If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                        Obj.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                        clsLnPropietario_bodega.Obtener(Obj.PropietarioBodega, pConnection, pTransaction)
                    End If

                    If lRow("IdUbicacionRecepcion") IsNot DBNull.Value AndAlso lRow("IdUbicacionRecepcion") IsNot Nothing Then
                        Obj.UbicacionRecepcion = CType(lRow("UbicacionRecepcion"), String)
                    End If

                    If lRow("IdTipoTransaccion") IsNot DBNull.Value AndAlso lRow("IdTipoTransaccion") IsNot Nothing Then
                        Obj.Descripcion = CType(lRow("Descripcion"), String)
                    End If

                    Obj.IsNew = False

                    Obj.OrdenCompraRec = clsLnTrans_re_oc.GetSingle(Obj.IdRecepcionEnc, pConnection, pTransaction)
                    Obj.Detalle = clsLnTrans_re_det.Get_Detalle_By_IdRecepcionEnc(Obj.IdRecepcionEnc, Obj.IdBodega, pConnection, pTransaction)
                    Obj.DetalleImagenes = clsLnTrans_re_img.Get_Detalle_Imagenes_By_IdRecepcionEnc(Obj.IdRecepcionEnc, pConnection, pTransaction)
                    Obj.DetalleParametros = clsLnTrans_re_det_parametros.Get_Detalle_Parametros_By_RecepcionEnc(Obj.IdRecepcionEnc, pConnection, pTransaction)
                    Obj.DetalleFacturas = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(Obj.IdRecepcionEnc, pConnection, pTransaction)
                    Obj.TareaHH = clsLnTarea_hh.GetSingle(1, Obj.IdRecepcionEnc, pTransaction, pConnection)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#EJC20180114: Agregué transaccionalidad
    Public Shared Function Get_Single_By_IdREcepcionEnc(ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_enc

        Get_Single_By_IdREcepcionEnc = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_enc  
                    WHERE IdRecepcionEnc=@IdRecepcionEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_re_enc()

                            Cargar(Obj, lRow)

                            Obj.IsNew = False

                            Obj.OrdenCompraRec = clsLnTrans_re_oc.GetSingle(Obj.IdRecepcionEnc, lConnection, lTransaction)
                            Obj.Detalle = clsLnTrans_re_det.Get_All_By_IdRecepcionEnc(Obj.IdRecepcionEnc, lConnection, lTransaction)
                            Obj.DetalleParametros = clsLnTrans_re_det_parametros.Get_Detalle_Parametros_By_RecepcionEnc(Obj.IdRecepcionEnc, lConnection, lTransaction)
                            Obj.DetalleFacturas = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(Obj.IdRecepcionEnc, lConnection, lTransaction)
                            Obj.DetalleOperadores = clsLnTrans_re_op.Get_All_Operadores_By_IdRecepcionEnc(Obj.IdRecepcionEnc, lConnection, lTransaction)

                            Return Obj

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

    Public Shared Function Get_Single_By_IdREcepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As clsBeTrans_re_enc

        Get_Single_By_IdREcepcionEnc = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_enc  
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeRecepcionEnc As New clsBeTrans_re_enc()

                    Cargar(BeRecepcionEnc, lRow)

                    BeRecepcionEnc.IsNew = False

                    BeRecepcionEnc.OrdenCompraRec = clsLnTrans_re_oc.GetSingle(BeRecepcionEnc.IdRecepcionEnc, lConnection, lTransaction)
                    BeRecepcionEnc.Detalle = clsLnTrans_re_det.Get_All_By_IdRecepcionEnc(BeRecepcionEnc.IdRecepcionEnc, lConnection, lTransaction)
                    BeRecepcionEnc.DetalleParametros = clsLnTrans_re_det_parametros.Get_Detalle_Parametros_By_RecepcionEnc(BeRecepcionEnc.IdRecepcionEnc, lConnection, lTransaction)
                    BeRecepcionEnc.DetalleFacturas = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(BeRecepcionEnc.IdRecepcionEnc, lConnection, lTransaction)
                    BeRecepcionEnc.DetalleOperadores = clsLnTrans_re_op.Get_All_Operadores_By_IdRecepcionEnc(BeRecepcionEnc.IdRecepcionEnc, lConnection, lTransaction)

                    Return BeRecepcionEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingleForMovs(ByVal pIdRecepcionEnc As Integer, ByVal pIdBodega As Integer) As clsBeTrans_re_enc

        GetSingleForMovs = Nothing

        Try

            Dim vSQL As String = "SELECT trans_re_enc.IdRecepcionEnc, trans_re_enc.IdPropietarioBodega,trans_re_enc.IdMuelle, trans_re_enc.IdUbicacionRecepcion, 
                                  trans_re_enc.IdTipoTransaccion, trans_re_enc.fecha_recepcion, trans_re_enc.hora_ini_pc, trans_re_enc.hora_fin_pc, 
                                  trans_re_enc.muestra_precio, trans_re_enc.estado, trans_re_enc.user_agr, trans_re_enc.fec_agr, trans_re_enc.user_mod, 
                                  trans_re_enc.fec_mod, trans_re_enc.fecha_tarea, trans_re_enc.tomar_fotos, trans_re_enc.escanear_rec_ubic, trans_re_enc.para_por_codigo, 
                                  trans_re_enc.observacion, trans_re_enc.firma_piloto, trans_re_enc.activo, trans_re_enc.NoGuia, trans_re_enc.CorreoEnviado, 
                                  trans_re_enc.Revision_Inconsistencia,trans_re_enc.bloqueada,trans_re_enc.bloqueada_por,trans_re_enc.idusuariobloqueo, 
                                  trans_re_enc.idmotivoanulacionbodega,trans_re_fact.NoFactura
                                  FROM trans_re_fact RIGHT OUTER JOIN
                                  trans_re_enc ON trans_re_fact.IdRecepcionEnc = trans_re_enc.IdRecepcionEnc inner join 
						          propietario_bodega on trans_re_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega
                                  WHERE (trans_re_enc.IdRecepcionEnc = @IdRecepcionEnc and propietario_bodega.IdBodega = @IdBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_re_enc()

                            Cargar(Obj, lRow)

                            If lRow("NoFactura") IsNot DBNull.Value AndAlso lRow("NoFactura") IsNot Nothing Then
                                Obj.NOFactura = CType(lRow("NoFactura"), String)
                            End If

                            Return Obj

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

    Public Shared Function MaxID() As Integer

        MaxID = 0

        Try

            Dim vSQL As String = "SELECT ISNULL(Max(IdRecepcionEnc),0) FROM trans_re_enc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            MaxID = CInt(lReturnValue) + 1
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

    Public Shared Function Finalizada(ByVal pIdRecepcionEnc As Integer) As Boolean

        Finalizada = False

        Try

            Dim vSQL As String = "SELECT Estado FROM trans_re_enc WHERE IdRecepcionEnc = @IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        Dim lValue = lCommand.ExecuteScalar()
                        Finalizada = (lValue = "Cerrado")

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Anulada(ByVal pIdRecepcionEnc As Integer) As Boolean

        Anulada = False

        Try

            Dim vSQL As String = "SELECT Estado FROM trans_re_enc WHERE IdRecepcionEnc = @IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        Dim lValue = lCommand.ExecuteScalar()
                        Anulada = (lValue = "Anulado")

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Anulada(ByVal pIdRecepcionEnc As Integer,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Anulada = False

        Try

            Dim vSQL As String = "SELECT Estado FROM trans_re_enc WHERE IdRecepcionEnc = @IdRecepcionEnc"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                Dim lValue = lCommand.ExecuteScalar()
                Anulada = (lValue = "Anulado")

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Get_Banderas_Recepcion(ByVal pIdRecepcionEnc As Integer, ByRef pFinalizada As Boolean, ByRef pAnulada As Boolean)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            pAnulada = Anulada(pIdRecepcionEnc, lConnection, lTransaction)
            pFinalizada = Finalizada(pIdRecepcionEnc, lConnection, lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try


    End Sub

    Public Shared Function Get_All_Ingreso_By_RangoFechas(ByVal pActivo As Boolean,
                                                          ByVal pFechaDel As Date,
                                                          ByVal pFechaAl As Date,
                                                          ByVal pIdBodega As Integer) As DataTable

        Get_All_Ingreso_By_RangoFechas = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Recepcion_Det WHERE 1 > 0  "

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega=@IdBodega"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += "Order by Fecha Desc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If
                        lDataAdapter.Fill(lTable)
                        Get_All_Ingreso_By_RangoFechas = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT30092024: clon del metodo anterior, para manejar parametros de bodega fiscal.
    Public Shared Function Get_All_Ingreso_By_RangoFechas_and_IdBodega(ByVal pActivo As Boolean,
                                                                       ByVal pFechaDel As Date,
                                                                       ByVal pFechaAl As Date,
                                                                       ByVal pBodega As clsBeBodega) As DataTable

        Get_All_Ingreso_By_RangoFechas_and_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Recepcion_Det WHERE 1 > 0  "

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            If pBodega.IdBodega <> 0 Then
                vSQL += " AND IdBodega=@IdBodega"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            If pBodega.Es_Bodega_Fiscal Then
                vSQL += " AND poliza_activa=1 "
            End If

            vSQL += "Order by Fecha Desc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        If pBodega.IdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pBodega.IdBodega)
                        End If
                        lDataAdapter.Fill(lTable)

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then
                            Get_All_Ingreso_By_RangoFechas_and_IdBodega = lTable
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

    Public Shared Function Get_All_Existencias_Por_Documento(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Get_All_Existencias_Por_Documento = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_ExistenciasPorNoDocumento WHERE 1 > 0  "

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += "order by Fecha_Agrego DESC "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandTimeout = 10
                        lDataAdapter.Fill(lTable)
                        Get_All_Existencias_Por_Documento = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Try

            Dim vSQL As String = "SELECT * FROM VW_Recepcion  WHERE IdTipoTransaccion <> 'PICH000'  "

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.Fill(lTable)
                        GetAll = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal pActivo As Boolean,
                                  ByVal pFechaDel As Date,
                                  ByVal pFechaAl As Date, ByVal pTipoTrans As clsBeTrans_re_enc.pTipoTrans) As DataTable


        GetAll = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Recepcion WHERE IdTipoTransaccion = '" & pTipoTrans.ToString() & "'"

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.Fill(lTable)
                        GetAll = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, pIdBodega As Integer) As DataTable

        Get_All_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Recepcion WHERE IdTipoTransaccion <> 'PICH000' AND IdBodega = @IdBodega "

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.Fill(lTable)
                        Get_All_By_IdBodega = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Actualizar_Estado_Recepcion(ByVal pIdRecepcionEnc As Integer,
                                                  ByRef Estado As String,
                                                  Optional ByVal pConnection As SqlConnection = Nothing,
                                                  Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lCommand As New SqlCommand

        Try

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim sp As String = ("UPDATE trans_re_enc SET 
                                 Estado=@Estado 
                                 WHERE IdRecepcionEnc=@IdRecepcionEnc AND  Estado <> 'Cerrado'")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(sp, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(sp, lConnection)
                lConnection.Open()
            End If

            lCommand.Parameters.Add(New SqlParameter("@Estado", Estado))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))

            lCommand.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        Finally
            lCommand.Dispose()
        End Try

    End Sub

    Public Shared Sub Actualizar_Estado_Cerrado_Recepcion(ByVal pIdRecepcionEnc As Integer,
                                                          Optional ByVal pConnection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lCommand As New SqlCommand

        Try

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim sp As String = ("UPDATE trans_re_enc SET 
                                 Estado=@Estado 
                                 WHERE IdRecepcionEnc=@IdRecepcionEnc")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(sp, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(sp, lConnection)
                lConnection.Open()
            End If

            lCommand.Parameters.Add(New SqlParameter("@Estado", "Cerrado"))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))

            lCommand.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        Finally
            lCommand.Dispose()
        End Try

    End Sub

    Public Shared Sub Actualizar_Banderas_Emergencia(ByVal pIdRecepcionEnc As Integer,
                                                     Optional ByVal pConnection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lCommand As New SqlCommand

        Try

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim sp As String = ("UPDATE trans_re_enc 
                                SET NoGuia='rHotFix',
                                Habilitar_Stock = 0
                                WHERE IdRecepcionEnc=@IdRecepcionEnc")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(sp, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(sp, lConnection)
                lConnection.Open()
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))

            lCommand.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        Finally
            lCommand.Dispose()
        End Try

    End Sub

    '#CKFK 20180206 06:30PM Creé esta función para guardar la firma del piloto
    Public Shared Sub Guarda_Firma_Recepcion(ByVal pIdRecepcionEnc As Integer,
                                             ByVal firma_piloto As Byte(),
                                             Optional ByVal pConnection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lCommand As New SqlCommand

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = ("UPDATE trans_re_enc SET firma_piloto=@firma_piloto WHERE IdRecepcionEnc=@IdRecepcionEnc")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(sp, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lCommand = New SqlCommand(sp, lConnection, lTransaction)
            End If

            lCommand.Parameters.Add(New SqlParameter("@firma_piloto", firma_piloto))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))

            lCommand.ExecuteNonQuery()

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then lTransaction.Rollback()
            Throw ex
        Finally
            lCommand.Dispose()
            If Not Es_Transaccion_Remota Then
                If Not lTransaction Is Nothing Then lTransaction.Commit()
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
        End Try

    End Sub

    Public Shared Function ExisteRecepcionNoFinalizada(ByVal pIdOrdenCompraEnc As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_re_oc AS reoc INNER JOIN trans_re_enc AS re ON reoc.IdRecepcionEnc = re.IdRecepcionEnc WHERE (UPPER(re.Estado)<>'FINALIZADO' AND UPPER(re.Estado)<>'CERRADO') AND reoc.IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Recepcion_No_Finalizada(ByVal pIdOrdenCompraEnc As Integer,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Recepcion_No_Finalizada = False
        Existe_Recepcion_No_Finalizada = False

        Try

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_re_oc AS reoc " &
                " INNER JOIN trans_re_enc AS re ON reoc.IdRecepcionEnc = re.IdRecepcionEnc " &
                " WHERE (UPPER(re.Estado)<>'FINALIZADO' AND UPPER(re.Estado)<>'CERRADO' AND UPPER(re.Estado)<>'ANULADO' ) " &
                " AND reoc.IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Existe_Recepcion_No_Finalizada = CInt(lReturnValue) > 0
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Anular_Recepcion(ByVal gBeRecepcion As clsBeTrans_re_enc,
                                            ByVal pIdMotivoAnulacionBodega As Integer,
                                            ByVal pObjTareaHH As clsBeTarea_hh) As Boolean

        Anular_Recepcion = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Anular(gBeRecepcion.IdRecepcionEnc,
                   pIdMotivoAnulacionBodega,
                   lConnection,
                   lTransaction)

            If gBeRecepcion.OrdenCompraRec IsNot Nothing Then

                Dim BeOC As New clsBeTrans_oc_enc() With {.IdOrdenCompraEnc = gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc}

                If clsLnTrans_oc_enc.Get_Encabezado_OC(gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc,
                                                       BeOC,
                                                       lTransaction,
                                                       lConnection) Then

                    If BeOC.IdEstadoOC = clsLnTrans_oc_enc.pEstadoOC.ASIGNADA OrElse BeOC.IdEstadoOC = clsLnTrans_oc_enc.pEstadoOC.EN_PROCESO Then

                        clsLnTrans_oc_enc.Actualizar_Estado_Nueva(gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc,
                                                                  lConnection,
                                                                  lTransaction)
                    End If

                End If

            End If

            '#EJC20180405: Las transacciones de recepción de BOF no tienen tarea de HH.
            If Not pObjTareaHH Is Nothing Then
                clsLnTarea_hh.Eliminar_By_IdTareaHH(pObjTareaHH.IdTareahh,
                                                    lConnection,
                                                    lTransaction)
            End If

            lTransaction.Commit()

            Anular_Recepcion = True

        Catch ex As Exception
            Throw ex
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
        Finally
            If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Sub Anular(ByVal pIdRecepcionEnc As Integer,
                             ByVal pIdMotivoAnulacion As Integer,
                             ByVal pConnection As SqlConnection,
                             ByVal pTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE trans_re_enc 
                                  SET Estado=@Estado,                    
                                  IdMotivoAnulacionBodega=@IdMotivoAnulacion  
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc "

            Using lCommand As New SqlCommand(vSQL, pConnection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
                lCommand.Parameters.AddWithValue("@Estado", "Anulado")
                lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lCommand.Parameters.AddWithValue("@IdMotivoAnulacion", pIdMotivoAnulacion)
                lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub Anular(ByVal pIdRecepcionEnc As Integer,
                             ByVal pConnection As SqlConnection,
                             ByVal pTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE trans_re_enc " &
                   " SET Estado=@Estado " &
                   " WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Estado", "Anulado")
                lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function OC_Tiene_Recepciones_Activas(ByVal pIdOrdenCompraEnc As Integer) As Boolean

        OC_Tiene_Recepciones_Activas = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT isnull(count(IdRecepcionEnc),0) as Recepciones FROM VW_RecepcionesEncOC WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc And Estado = 'Nueva' "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Contador As Integer = 0

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            If lDataTable.Rows(0).Item("Recepciones") IsNot DBNull.Value AndAlso lDataTable.Rows(0).Item("Recepciones") IsNot Nothing Then
                                Contador = lDataTable.Rows(0).Item("Recepciones")
                            End If

                        End If

                        OC_Tiene_Recepciones_Activas = (Contador > 0)

                    End Using

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar(ByVal pObjTareaHH As clsBeTarea_hh,
                                   ByVal pRecEnc As clsBeTrans_re_enc,
                                   ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                   ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                   ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                   ByVal pListRecOpe As List(Of clsBeTrans_re_op),
                                   ByVal pListRecFact As List(Of clsBeTrans_re_fact),
                                   ByVal pListRecImg As List(Of clsBeTrans_re_img),
                                   ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                   ByVal pListStockRec As List(Of clsBeStock_rec),
                                   ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                   ByVal IdBodega As Integer,
                                   ByVal No_Ticket_Tms As String) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            '#EJC20171022_0852AM: Refactorizado por mí.
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Not Finalizada(pRecEnc.IdRecepcionEnc, lConnection, lTransaction) Then

                '#GT10102025: validar que no este anulada (concurrencia donde en una pc anulan, pero en otra dejan abierta la tarea ;( )
                If Not Anulada(pRecEnc.IdRecepcionEnc, lConnection, lTransaction) Then

                    Dim pRecEnc1 As New clsBeTrans_re_enc
                    pRecEnc1 = Get_Single_By_IdREcepcionEnc_Sin_Det(pRecEnc.IdRecepcionEnc,
                                                                    lConnection,
                                                                    lTransaction)

                    '#EJC202303021159: Mantener el último estado con el que fue actualizada la recepción. (Por si dejaron la pantalla abierta)
                    If Not pRecEnc1 Is Nothing Then
                        If pRecEnc1.Estado <> "Nuevo" AndAlso pRecEnc.Estado = "Nuevo" Then
                            pRecEnc.Estado = pRecEnc1.Estado
                        End If
                    End If

                    ' Recepción Encabezado
                    Guarda_Trans_re_enc(pRecEnc,
                                        lConnection,
                                        lTransaction)

                    ' Recepción Orden Compra
                    clsLnTrans_re_oc.Guarda_Trans_Re_OC(pRecEnc,
                                                        pRecOrdenCompra,
                                                        lConnection,
                                                        lTransaction)

                    ' Recepción Detalle
                    clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet,
                                                          True,
                                                          pRecEnc,
                                                          lConnection,
                                                          lTransaction)

                    If pRecEnc.IdTipoTransaccion <> clsBeTrans_re_enc.pTipoTrans.PICH000.ToString() Then 'Si no es pre-ingreso, actualizar cantidad_recibida en O.C.
                        'Actualiza cantidad recibida OC.
                        clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra,
                                                                         pListRecDet,
                                                                         lConnection,
                                                                         lTransaction)
                    End If

                    'Guarda parámetros de productos.
                    clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                                pListRecDet,
                                                                                pListRecDetParam,
                                                                                lConnection,
                                                                                lTransaction)

                    'Recepción Operadores
                    clsLnTrans_re_op.Guarda_Trans_Re_Op(pRecEnc.IdRecepcionEnc,
                                                        pListRecOpe,
                                                        lConnection,
                                                        lTransaction)

                    ' Imagenes
                    clsLnTrans_re_img.Guarda_Trans_Re_Img(pRecEnc.IdRecepcionEnc,
                                                          pListRecImg,
                                                          lConnection,
                                                          lTransaction)

                    ' Facturas asociadas
                    clsLnTrans_re_fact.Guarda_facturas_asoc(pRecEnc.IdRecepcionEnc,
                                                            pListRecFact,
                                                            lConnection,
                                                            lTransaction)

                    ' Stock Rec
                    clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                                    IdBodega,
                                                    pListStockRec,
                                                    lConnection,
                                                    lTransaction)

                    ' Producto_pallet
                    clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                                pListProductoPallet,
                                                                lConnection,
                                                                lTransaction)

                    ' Stock Serializado Rec
                    clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                          pListStockRec,
                                                          lConnection,
                                                          lTransaction)

                    'Tarea de recepción para la HH.
                    clsLnTarea_hh.Guardar_Tarea_Recepcion_HH(pObjTareaHH,
                                                             lConnection,
                                                             lTransaction)

                    'Tarea de actualizar el estado de un ticket existente
                    clsLnTrans_oc_enc.Cambiar_A_Estado_Procesado(No_Ticket_Tms,
                                                                 lConnection,
                                                                 lTransaction)

                End If

            End If

            lTransaction.Commit()

            Return pRecEnc.IdRecepcionEnc

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Guardar(ByVal IdBodega As Integer,
                                   ByVal pObjTareaHH As clsBeTarea_hh,
                                   ByVal pRecEnc As clsBeTrans_re_enc,
                                   ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                   ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                   ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                   ByVal pListRecOpe As List(Of clsBeTrans_re_op),
                                   ByVal pListRecFact As List(Of clsBeTrans_re_fact),
                                   ByVal pListRecImg As List(Of clsBeTrans_re_img),
                                   ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                   ByVal pListStockRec As List(Of clsBeStock_rec),
                                   ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Integer

        Try

            ' Recepción Encabezado
            Guarda_Trans_re_enc(pRecEnc,
                                lConnection,
                                lTransaction)

            ' Recepción Orden Compra
            clsLnTrans_re_oc.Guarda_Trans_Re_OC(pRecEnc,
                                                pRecOrdenCompra,
                                                lConnection,
                                                lTransaction)

            ' Recepción Detalle
            clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet,
                                                  True,
                                                  pRecEnc,
                                                  lConnection,
                                                  lTransaction)

            If pRecEnc.IdTipoTransaccion <> clsBeTrans_re_enc.pTipoTrans.PICH000.ToString() Then 'Si no es pre-ingreso, actualizar cantidad_recibida en O.C.
                'Actualiza cantidad recibida OC.
                clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra,
                                                                 pListRecDet,
                                                                 lConnection,
                                                                 lTransaction)
            End If

            'Guarda parámetros de productos.
            clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                        pListRecDet,
                                                                        pListRecDetParam,
                                                                        lConnection,
                                                                        lTransaction)

            'Recepción Operadores
            clsLnTrans_re_op.Guarda_Trans_Re_Op(pRecEnc.IdRecepcionEnc,
                                                pListRecOpe,
                                                lConnection,
                                                lTransaction)

            ' Imagenes
            clsLnTrans_re_img.Guarda_Trans_Re_Img(pRecEnc.IdRecepcionEnc,
                                                  pListRecImg,
                                                  lConnection,
                                                  lTransaction)

            ' Facturas asociadas
            clsLnTrans_re_fact.Guarda_facturas_asoc(pRecEnc.IdRecepcionEnc,
                                                    pListRecFact,
                                                    lConnection,
                                                    lTransaction)

            ' Stock Rec
            clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                            IdBodega,
                                            pListStockRec,
                                            lConnection,
                                            lTransaction)

            ' Producto_pallet
            clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                        pListProductoPallet,
                                                        lConnection,
                                                        lTransaction)

            ' Stock Serializado Rec
            clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                  pListStockRec,
                                                  lConnection,
                                                  lTransaction)

            'Tarea de recepción para la HH.
            clsLnTarea_hh.Guardar_Tarea_Recepcion_HH(pObjTareaHH,
                                                     lConnection,
                                                     lTransaction)

            Return pRecEnc.IdRecepcionEnc

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva opcion de log para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdBodega:=pRecEnc.IdBodega,
                                                 pIdUsuarioAgr:=pRecEnc.User_agr,
                                                 pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
        End Try

    End Function

    Private Shared Sub Guarda_Trans_re_enc(ByVal pRecEnc As clsBeTrans_re_enc,
                                           ByRef lConnection As SqlConnection,
                                           ByRef lTransaction As SqlTransaction)

        Try

            If pRecEnc.IsNew Then
                Insertar(pRecEnc, lConnection, lTransaction)

                '#MECR30092025: Se agrego bitacora para logs de recepciones.
                clsLnLog_error_wms_rec.Agregar_Error("Se creó la recepcion " + pRecEnc.IdRecepcionEnc.ToString() + " satisfactoriamente.",
                                                     pIdBodega:=pRecEnc.IdBodega,
                                                     pIdUsuarioAgr:=pRecEnc.User_agr,
                                                     pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                     pConection:=lConnection,
                                                     pTransaction:=lTransaction)

            Else
                Actualizar(pRecEnc, lConnection, lTransaction)
            End If

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva opcion de log para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdBodega:=pRecEnc.IdBodega,
                                                 pIdUsuarioAgr:=pRecEnc.User_agr,
                                                 pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    '#CKFK20220524 Función para guardar la recepción cuando se recibe sin presentacion 
    Public Shared Function GuardarHHSP(ByVal pRecEnc As clsBeTrans_re_enc,
                                       ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                       ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                       ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                       ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                       ByVal pListStockRec As List(Of clsBeStock_rec),
                                       ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                       ByVal pLotesRec As clsBeTrans_oc_det_lote,
                                       ByVal pIdEmpresa As Integer,
                                       ByVal pIdBodega As Integer,
                                       ByVal pIdUsuario As Integer,
                                       ByVal pIdResolucionLp As Integer,
                                       ByVal pBeTransOcDet As clsBeTrans_oc_det) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim CadenaResultado As String = ""
        Dim pIdOrdenCompraEnc As Integer = 0
        Dim IdTipoDocumento As Integer = 0
        Dim pNuevoBeTransOcDet As New clsBeTrans_oc_det
        Dim vResultadoInsertReEnc As Integer = 0
        Dim vResultadoGuarda_Trans_Re_OC As Integer = 0
        Dim vResultadoEliminar_Detalle As String = ""
        Dim vResultadoGuarda_Trans_re_det As Integer = 0
        Dim vResultadoGuarda_Trans_re_det_lote As Integer = 0
        Dim vResultadoGuarda_Trans_Re_Det_Parametros As Integer = 0
        Dim vResultadoActualiza_Cantidad_Recibida_OC As Integer = 0
        Dim vResultadoGuarda_Stock_Rec As Integer = 0
        Dim vResultadoGuarda_Producto_Pallet As Integer = 0
        Dim vResultadoInsertar_Movimientos_Recepcion As Integer = 0
        Dim vResultadoInsertarStock As Integer = 0
        Dim vResultadoInsertar_Stock_Parametro_Recepcion As Integer = 0
        Dim vResultadoInsertar_Stock_Serializado_Recepcion As Integer = 0
        Dim vResultadoActualiza_Estado_Barras_Pallet As Integer = 0
        Dim vBeRecepcionEncabezado As New clsBeTrans_re_enc

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Not Finalizada(pRecEnc.IdRecepcionEnc, lConnection, lTransaction) Then

                If Not Anulada(pRecEnc.IdRecepcionEnc, lConnection, lTransaction) Then

                    If pRecOrdenCompra IsNot Nothing Then

                        If pListStockRec.Count > 0 Then

                            For Each StockRec In pListStockRec

                                pNuevoBeTransOcDet = clsLnTrans_oc_det.Crear_Linea_Unidades(pBeTransOcDet,
                                                                                            pListRecDet(0).cantidad_recibida,
                                                                                            pListRecDet(0).cantidad_recibida,
                                                                                            pIdBodega,
                                                                                            pListRecDet,
                                                                                            lConnection,
                                                                                            lTransaction)
                            Next

                        End If

                        pIdOrdenCompraEnc = pRecOrdenCompra.IdOrdenCompraEnc
                        IdTipoDocumento = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)

                        pRecOrdenCompra.Hora_fin_hh = Date.Now()

                        vResultadoGuarda_Trans_Re_OC = clsLnTrans_re_oc.Guarda_Trans_Re_OC(pRecEnc,
                                                                                           pRecOrdenCompra,
                                                                                           lConnection,
                                                                                           lTransaction)

                        If vResultadoGuarda_Trans_Re_OC > 0 Then
                            CadenaResultado += "Guarda_Trans_Re_OC " & vResultadoGuarda_Trans_Re_OC
                        Else
                            Throw New Exception("ERROR_202210051158: No se pudo insertar la cabecera de la recepción.")
                        End If

                        vResultadoActualiza_Cantidad_Recibida_OC = clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra,
                                                                                                                    pListRecDet,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)

                        If vResultadoActualiza_Cantidad_Recibida_OC > 0 Then
                            CadenaResultado += "Actualiza_Cantidad_Recibida_OC " & vResultadoActualiza_Cantidad_Recibida_OC
                        Else
                            Throw New Exception("ERROR_202210051210: No se pudo actualizar la cantidad recibida del documento de ingreso.")
                        End If


                    End If

                    '#EJC20220908:Consultar configuración de bodega antes de proceso.
                    Dim BeBodega As New clsBeBodega()
                    BeBodega = clsLnBodega.GetSingle_By_Idbodega(pRecEnc.IdBodega,
                                                                 lConnection,
                                                                 lTransaction)

                    If BeBodega Is Nothing Then
                        Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega para el IdBodega: " & pRecEnc.IdBodega)
                    End If


                    vBeRecepcionEncabezado = New clsBeTrans_re_enc
                    vBeRecepcionEncabezado = Get_Single_By_IdREcepcionEnc_Sin_Det(pRecEnc.IdRecepcionEnc,
                                                                                   lConnection,
                                                                                   lTransaction)

                    If vBeRecepcionEncabezado Is Nothing Then
                        Throw New Exception("ERROR_211020252000: No se obtuvo la recepción asociada al documento de ingreso.")
                    End If



                    'Recepción Encabezado
                    If pRecEnc.IsNew Then

                        vResultadoInsertReEnc = Insertar(pRecEnc, lConnection, lTransaction)

                        If vResultadoInsertReEnc > 0 Then
                            CadenaResultado += "Inserté encabezado recepción " & vResultadoInsertReEnc
                        Else
                            Throw New Exception("ERROR_202210051158: No se pudo insertar la cabecera de la recepción.")
                        End If

                    End If

                    If Not pListRecDet Is Nothing Then

                        If pListRecDet.Count > 0 Then

                            For Each det In pListRecDet
                                det.No_Linea = pNuevoBeTransOcDet.No_Linea
                                det.IdOrdenCompraDet = pNuevoBeTransOcDet.IdOrdenCompraDet
                            Next

                            vResultadoEliminar_Detalle = clsLnTrans_re_det.Eliminar_Detalle(pIdOrdenCompraEnc,
                                                                                            pListRecDet,
                                                                                            lConnection,
                                                                                            lTransaction)

                            If vResultadoEliminar_Detalle <> "" Then
                                CadenaResultado += "Eliminar_Detalle_Recepción " & vResultadoEliminar_Detalle
                            End If

                            vResultadoGuarda_Trans_re_det = clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet,
                                                                                                  pListStockRec,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                            If vResultadoGuarda_Trans_re_det > 0 Then
                                CadenaResultado += "Guarda_Trans_re_det: " & vResultadoGuarda_Trans_re_det
                            Else
                                Throw New Exception("ERROR_202210051158: No se pudo insertar el detalle de la recepción.")
                            End If

                            If Not pLotesRec Is Nothing Then

                                Dim TieneLotes As Boolean = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(pRecOrdenCompra.IdOrdenCompraEnc,
                                                                                                           lConnection,
                                                                                                           lTransaction).ToList.Count > 0

                                If TieneLotes Then
                                    pLotesRec.No_linea = pNuevoBeTransOcDet.No_Linea
                                    pLotesRec.IdOrdenCompraDet = pNuevoBeTransOcDet.IdOrdenCompraDet

                                    '#EJC20210412:Agregado para BYB, actualizar la cantidad recibida por lote.
                                    vResultadoGuarda_Trans_re_det_lote = clsLnTrans_oc_det_lote.Guarda_Trans_re_det_lote(pLotesRec,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)

                                    If vResultadoGuarda_Trans_re_det_lote > 0 Then
                                        CadenaResultado += "Guarda_Trans_re_det_lote: " & vResultadoGuarda_Trans_re_det_lote
                                    Else
                                        Throw New Exception("ERROR_202210051158: No se pudo actualizar la información de ltoes.")
                                    End If
                                End If

                            End If

                            vResultadoGuarda_Trans_Re_Det_Parametros = clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                                                                                   pListRecDet,
                                                                                                                                   pListRecDetParam,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction)

                            If vResultadoGuarda_Trans_Re_Det_Parametros > 0 Then
                                CadenaResultado += "Guarda_Trans_Re_Det_Parametros " & vResultadoGuarda_Trans_Re_Det_Parametros
                            End If

                            If Not pListStockRec Is Nothing Then

                                If pListStockRec.Count > 0 Then

                                    For Each pBeStockRec In pListStockRec

                                        If BeBodega.bloquear_lp_hh Then

                                            Dim vLPExiste As Boolean = False

                                            vLPExiste = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(pBeStockRec.Lic_plate,
                                                                                          pIdBodega,
                                                                                          lConnection,
                                                                                          lTransaction)

                                            If vLPExiste Then
                                                Throw New Exception("ERROR_20220823_1604: La licencia: " & pBeStockRec.Lic_plate & " ya existe.")
                                            End If

                                        End If

                                    Next

                                    For Each stockrec In pListStockRec
                                        stockrec.No_linea = pNuevoBeTransOcDet.No_Linea
                                    Next

                                    '#EJC20210504: Incrementar contador de LP.
                                    If pIdResolucionLp <> 0 Then

                                        Dim BeResolLp As New clsBeResolucion_lp_operador()
                                        BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucionLp,
                                                                                          lConnection,
                                                                                          lTransaction)

                                        If Not BeResolLp Is Nothing Then
                                            BeResolLp.Correlativo_Actual += 1
                                            clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp,
                                                                                                      lConnection,
                                                                                                      lTransaction)
                                        End If

                                    End If

                                    vResultadoGuarda_Stock_Rec = clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                                                                                 pIdBodega,
                                                                                                 pListStockRec,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                                    If vResultadoGuarda_Stock_Rec > 0 Then

                                        CadenaResultado += "Guarda_Stock_Rec: " & vResultadoGuarda_Stock_Rec

                                        clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                                              pListStockRec,
                                                                              lConnection,
                                                                              lTransaction)

                                        CadenaResultado += "Guarda_Stock_Se_Rec "

                                    Else
                                        Throw New Exception("ERROR_202210051158: No se pudo insertar el stock de la recepción.")
                                    End If

                                    If Not pListProductoPallet Is Nothing Then

                                        If pListProductoPallet.Count > 0 Then

                                            vResultadoGuarda_Producto_Pallet = clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                                                                                           pListProductoPallet,
                                                                                                                           lConnection,
                                                                                                                           lTransaction)

                                            If vResultadoGuarda_Producto_Pallet > 0 Then
                                                CadenaResultado += "Guarda_Producto_Pallet: " & vResultadoGuarda_Producto_Pallet
                                            Else
                                                Throw New Exception("ERROR_202210051158: No se pudo guardar la información relacionada a la licencia (Guarda_Producto_Pallet).")
                                            End If

                                        End If

                                    End If


                                    Dim BeStock As New clsBeStock()

                                    '#GT22102025: validar contra nuevo objeto, el anterior podria estar inconsistente.
                                    'If pRecEnc.Habilitar_Stock Then
                                    If vBeRecepcionEncabezado.Habilitar_Stock Then

                                        Dim pBeINavBarraPallet As New clsBeI_nav_barras_pallet

                                        For Each pBeStockRec As clsBeStock_rec In pListStockRec

                                            BeStock = New clsBeStock

                                            pBeStockRec.IdBodega = pIdBodega

                                            '#EJC20200207: Para evitar fechas malas de la HH
                                            pBeStockRec.Fecha_Ingreso = Now
                                            pBeStockRec.Fec_agr = Now
                                            pBeStockRec.Fec_mod = Now
                                            clsPublic.CopyObject(pBeStockRec, BeStock)

                                            Dim lMaxS As Integer = clsLnStock.MaxID(lConnection, lTransaction)
                                            lMaxS += 1

                                            BeStock.IdStock = lMaxS

                                            vResultadoInsertar_Movimientos_Recepcion = clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                                                                                             pIdBodega,
                                                                                                                                             pIdUsuario,
                                                                                                                                             pBeStockRec,
                                                                                                                                             lConnection,
                                                                                                                                             lTransaction)

                                            If vResultadoInsertar_Movimientos_Recepcion > 0 Then

                                                CadenaResultado += "Insertar_Movimiento_Recepcion: IdMovimiento" & vResultadoInsertar_Movimientos_Recepcion

                                                '#EJC20191218: IdBodega2Stock
                                                vResultadoInsertarStock = clsLnStock.Insertar(BeStock,
                                                                                          lConnection,
                                                                                          lTransaction)

                                                If vResultadoInsertarStock > 0 Then

                                                    CadenaResultado += "clsLnStock.Insertar: " & vResultadoInsertarStock

                                                    vResultadoInsertar_Stock_Parametro_Recepcion = clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                                                                                           lMaxS,
                                                                                                                                                           lConnection,
                                                                                                                                                           lTransaction)

                                                    If vResultadoInsertarStock > 0 Then
                                                        CadenaResultado += "clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion: " & vResultadoInsertar_Stock_Parametro_Recepcion
                                                    End If

                                                    vResultadoInsertar_Stock_Serializado_Recepcion = clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                                                                                                        lMaxS,
                                                                                                                                                        lConnection,
                                                                                                                                                        lTransaction)

                                                    If vResultadoInsertar_Stock_Serializado_Recepcion > 0 Then
                                                        CadenaResultado += "Insertar_Stock_Serializado_Recepcion: " & vResultadoInsertar_Stock_Serializado_Recepcion
                                                    End If

                                                    '#EJC20190329_0538PM: Marcar el pallet como recibido.
                                                    If pBeStockRec.Lic_plate <> "" Then
                                                        pBeINavBarraPallet.Recibido = True
                                                        pBeINavBarraPallet.IdRecepcion = pRecEnc.IdRecepcionEnc
                                                        pBeINavBarraPallet.Codigo_barra = pBeStockRec.Lic_plate
                                                        pBeINavBarraPallet.Fecha_Ingreso = Now
                                                        pBeINavBarraPallet.Fecha_Agregado = Now

                                                        pBeINavBarraPallet.Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)

                                                        If Not pBeINavBarraPallet.Bodega_Destino Is Nothing Then
                                                            CadenaResultado += "Get_Codigo_By_IdBodega: " & pBeINavBarraPallet.Bodega_Destino
                                                        Else
                                                            Throw New Exception("ERROR_202210051226: No se pudo obtener la bodega destino con el IdBodega: " & pIdBodega)
                                                        End If

                                                        vResultadoActualiza_Estado_Barras_Pallet = clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet(pBeINavBarraPallet,
                                                                                                                                                           lConnection,
                                                                                                                                                           lTransaction)

                                                        If vResultadoActualiza_Estado_Barras_Pallet > 0 Then
                                                            CadenaResultado += "Actualiza_Estado_Barras_Pallet: " & vResultadoActualiza_Estado_Barras_Pallet
                                                        End If

                                                    End If

                                                    If Not pListRecDet Is Nothing Then

                                                        If pListRecDet.Count > 0 Then

                                                            '#EJC20190607: Insertar stock parcial (no con pallet) en interface.
                                                            For Each pBeTransReDet As clsBeTrans_re_det In pListRecDet

                                                                If pBeTransReDet.IsNew Then

                                                                    CadenaResultado += "Inserta transacciones out"

                                                                    Dim vResultado As String = clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                                                                                                     pIdBodega,
                                                                                                                                                     IdTipoDocumento,
                                                                                                                                                     pBeTransReDet,
                                                                                                                                                     pIdOrdenCompraEnc,
                                                                                                                                                     pIdUsuario,
                                                                                                                                                     False,
                                                                                                                                                     lConnection,
                                                                                                                                                     lTransaction)

                                                                    CadenaResultado += "Insertar_Ingreso_Parcial: " & vResultado

                                                                    Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                                                                    BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                                                                    BeLoteNum.IdProductoBodega = pBeTransReDet.IdProductoBodega
                                                                    BeLoteNum.IdRecepcionEnc = pRecEnc.IdRecepcionEnc
                                                                    BeLoteNum.Codigo = pBeINavBarraPallet.Codigo
                                                                    BeLoteNum.Lote = pBeINavBarraPallet.Lote
                                                                    BeLoteNum.Lote_Numerico = pBeINavBarraPallet.Lote_Numerico
                                                                    BeLoteNum.Cantidad = pBeTransReDet.cantidad_recibida
                                                                    BeLoteNum.FechaIngreso = Now
                                                                    clsLnTrans_re_det_lote_num.Insertar(BeLoteNum, lConnection, lTransaction)

                                                                End If

                                                                Dim vPosiciones As Integer = 0

                                                                If pBeTransReDet.Pallet_No_Estandar Then

                                                                    Dim BeStockDet As New clsBeStock_det
                                                                    BeStockDet.IdStock = BeStock.IdStock
                                                                    BeStockDet.Posiciones = pBeTransReDet.Posiciones

                                                                    If clsLnStock_det.Get_Single_By_IdStock(BeStockDet, lConnection, lTransaction) Then
                                                                        '#EJC20220505: Porqué ya existe?
                                                                        BeStockDet.Posiciones = vPosiciones
                                                                        clsLnStock_det.Actualizar(BeStockDet, lConnection, lTransaction)
                                                                    Else
                                                                        clsLnStock_det.Insertar(BeStockDet, lConnection, lTransaction)
                                                                    End If


                                                                End If

                                                            Next

                                                        Else
                                                            Throw New Exception("ERROR_202210051228: El count del detalle de la recepción es 0.")
                                                        End If

                                                    Else
                                                        Throw New Exception("ERROR_202210051228: El detalle de la recepción es nothing.")
                                                    End If

                                                Else
                                                    Throw New Exception("ERROR_202210051223: No se pudo insertar el stock.")
                                                End If

                                            Else
                                                Throw New Exception("ERROR_202210051221: No se pudo guardar el movimiento (Insertar_Movimientos_Recepcion).")
                                            End If

                                        Next

                                    End If

                                    CadenaResultado += " Terminé la recepción " & pRecEnc.IdRecepcionEnc.ToString()

                                Else
                                    Throw New Exception("#ERR20200317A: La lista de stock no tiene registros.")
                                End If

                            Else
                                Throw New Exception("#ERR20200317B: La lista de stock para recepción está vacía.")
                            End If

                        Else
                            Throw New Exception("ERROR_202210051158A: El count del detalle de la recepción es 0.")
                        End If

                    Else
                        Throw New Exception("ERROR_202210051158B: El detalle de la recepción es Nothing.")
                    End If


                Else
                    Throw New Exception("ERROR_DE_PROCESO_202302221011: La recepción fue anulada previamente, regrese al menú principal.")
                End If

            Else
                Throw New Exception("ERROR_DE_PROCESO_202302221011: La recepción fue finalizada previamente, regrese al menú principal.")
            End If

            lTransaction.Commit()

            Return CadenaResultado

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", ex.Message, CadenaResultado))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GuardarHH_Original(ByVal pRecEnc As clsBeTrans_re_enc,
                                              ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                              ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                              ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                              ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                              ByVal pListStockRec As List(Of clsBeStock_rec),
                                              ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                              ByVal pLotesRec As clsBeTrans_oc_det_lote,
                                              ByVal pIdEmpresa As Integer,
                                              ByVal pIdBodega As Integer,
                                              ByVal pIdUsuario As Integer,
                                              ByVal pIdResolucionLp As Integer) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim CadenaResultado As String = ""
        Dim pIdOrdenCompraEnc As Integer = 0
        Dim IdTipoDocumento As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pRecOrdenCompra IsNot Nothing Then
                pIdOrdenCompraEnc = pRecOrdenCompra.IdOrdenCompraEnc
                IdTipoDocumento = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                            lConnection,
                                                                                            lTransaction)
            End If

            '#EJC20220908:Consultar configuración de bodega antes de proceso.
            Dim BeBodega As New clsBeBodega()

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pRecEnc.IdBodega,
                                                         lConnection,
                                                         lTransaction)


            For Each S In pListStockRec

                If BeBodega.bloquear_lp_hh Then
                    Dim vLPExiste As Boolean = False

                    vLPExiste = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(S.Lic_plate,
                                                                          pIdBodega,
                                                                          lConnection,
                                                                          lTransaction)



                    'Console.WriteLine(vLPExiste)

                    If vLPExiste Then
                        Throw New Exception("ERROR_20220823_1604: La licencia: " & S.Lic_plate & " ya existe.")
                    Else
                        'GT23082022: Si todo se guardó correctamente se hace update para la siguiente LP
                        '#EJC20210504: Incrementar contador de LP.

                        If pIdResolucionLp <> 0 Then

                            Dim BeResolLp As New clsBeResolucion_lp_operador()
                            BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucionLp, lConnection, lTransaction)

                            If Not BeResolLp Is Nothing Then
                                BeResolLp.Correlativo_Actual += 1
                                clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp,
                                                                                          lConnection,
                                                                                          lTransaction)
                            End If

                        End If
                    End If
                End If
            Next


            'Recepción Encabezado
            If pRecEnc.IsNew Then
                Insertar(pRecEnc, lConnection, lTransaction)
                CadenaResultado += "Inserté encabezado recepción "
            Else
                '#EJC20190716: Para que ?
                'Actualizar(pRecEnc, lConnection, lTransaction)
                CadenaResultado += "Actualicé encabezado recepción "
            End If



            pRecOrdenCompra.Hora_fin_hh = Date.Now()

            clsLnTrans_re_oc.Guarda_Trans_Re_OC(pRecEnc,
                                                pRecOrdenCompra,
                                                lConnection,
                                                lTransaction)
            CadenaResultado += "Guarda_Trans_Re_OC "

            clsLnTrans_re_det.Eliminar_Detalle(pIdOrdenCompraEnc,
                                               pListRecDet,
                                               lConnection,
                                               lTransaction)
            CadenaResultado += "Eliminar_Detalle_Recepción "

            clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet,
                                                  lConnection,
                                                  lTransaction)

            CadenaResultado += "Guarda_Trans_re_det "

            Dim vIdRecepcionDetNuevoPorConcurrencia As Integer = 0

            If Not pListRecDet Is Nothing Then

                If pListRecDet.Count > 0 Then

                    If pListRecDet.Count = 1 Then
                        vIdRecepcionDetNuevoPorConcurrencia = pListRecDet(0).IdRecepcionDet
                    Else
                        Throw New Exception("#ERR202209161211A: Escenario no controlado al guardar el detalle de recepción, la lista tiene más de un objeto y no se puede inferir el idrecepción det para la lista de stock de forma automática.")
                    End If

                Else
                    Throw New Exception("#ERR202209161211A: El count de recepcion det es 0.")
                End If

            Else
                Throw New Exception("#ERR202209161211B: La lista de recepcion det es nothing.")
            End If

            For Each S In pListStockRec
                S.IdRecepcionDet = vIdRecepcionDetNuevoPorConcurrencia
            Next


            '#EJC20210412:Agregado para BYB, actualizar la cantidad recibida por lote.
            clsLnTrans_oc_det_lote.Guarda_Trans_re_det_lote(pLotesRec,
                                                            lConnection,
                                                            lTransaction)

            clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                        pListRecDet,
                                                                        pListRecDetParam,
                                                                        lConnection,
                                                                        lTransaction)
            CadenaResultado += "Guarda_Trans_Re_Det_Parametros "

            CadenaResultado += "Actualiza_Cantidad_Recibida_OC "
            CadenaResultado += clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra,
                                                                                pListRecDet,
                                                                                lConnection,
                                                                                lTransaction)

            If Not pListStockRec Is Nothing Then

                If pListStockRec.Count > 0 Then

                    clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                                    pIdBodega,
                                                    pListStockRec,
                                                    lConnection,
                                                    lTransaction)
                    CadenaResultado += " Guarda_Stock_Rec "

                    clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                          pListStockRec,
                                                          lConnection,
                                                          lTransaction)
                    CadenaResultado += "Guarda_Stock_Se_Rec "

                Else
                    Throw New Exception("#ERR20200317A: La lista de stock no tiene registros.")
                End If

            Else
                Throw New Exception("#ERR20200317B: La lista de stock para recepción está vacía.")
            End If

            If Not pListProductoPallet Is Nothing Then

                clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                            pListProductoPallet,
                                                            lConnection,
                                                            lTransaction)
                CadenaResultado += "Guarda_Producto_Pallet "

            End If

            Dim BeStock As New clsBeStock()

            If pRecEnc.Habilitar_Stock Then

                Dim pBeINavBarraPallet As New clsBeI_nav_barras_pallet

                Dim lMaxS As Integer = clsLnStock.MaxID(lConnection, lTransaction)

                If Not pListStockRec Is Nothing Then

                    If pListStockRec.Count > 0 Then

                        For Each pBeStockRec As clsBeStock_rec In pListStockRec

                            BeStock = New clsBeStock

                            pBeStockRec.IdBodega = pIdBodega

                            '#EJC20200207: Para evitar fechas malas de la HH
                            pBeStockRec.Fecha_Ingreso = Now
                            pBeStockRec.Fec_agr = Now
                            pBeStockRec.Fec_mod = Now
                            clsPublic.CopyObject(pBeStockRec, BeStock)

                            lMaxS += 1

                            BeStock.IdStock = lMaxS

                            clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                                  pIdBodega,
                                                                                  pIdUsuario,
                                                                                  pBeStockRec,
                                                                                  lConnection,
                                                                                  lTransaction)
                            CadenaResultado += "Insertar_Movimientos_Recepcion"

                            '#EJC202208231605: Validar que la licencia no exista antes de insertarla en stock.
                            Dim vLpExistente As Boolean = False

                            If BeBodega.bloquear_lp_hh Then

                                vLpExistente = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(pBeStockRec.Lic_plate,
                                                                                         pIdBodega,
                                                                                         lConnection,
                                                                                         lTransaction)

                                If vLpExistente Then
                                    Throw New Exception("ERROR_20220823_1604: La licencia: " & pBeStockRec.Lic_plate & " ya existe")
                                End If

                                '#EJC202209081217: Prevenir que la licencia esté vacía.
                                If pIdResolucionLp <> 0 Then
                                    If pBeStockRec.Lic_plate.Trim = "" Then
                                        Throw New Exception("ERROR_20220908_1214: Error al procesar la licencia, la configuración requiere un valor diferente de vacío.")
                                    End If
                                End If

                            End If


                            '#EJC20191218: IdBodega2Stock
                            clsLnStock.Insertar(BeStock,
                                                lConnection,
                                                lTransaction)

                            CadenaResultado += "Inserta_Stock"

                            clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                    lMaxS,
                                                                                    lConnection,
                                                                                    lTransaction)
                            CadenaResultado += "Insertar_Stock_Parametro_Recepcion"

                            clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                               lMaxS,
                                                                               lConnection,
                                                                               lTransaction)
                            CadenaResultado += "Insertar_Stock_Serializado_Recepcion"

                            '#EJC20190329_0538PM: Marcar el pallet como recibido.
                            If pBeStockRec.Lic_plate <> "" Then
                                pBeINavBarraPallet.Recibido = True
                                pBeINavBarraPallet.IdRecepcion = pRecEnc.IdRecepcionEnc
                                pBeINavBarraPallet.Codigo_barra = pBeStockRec.Lic_plate
                                pBeINavBarraPallet.Fecha_Ingreso = Now
                                pBeINavBarraPallet.Fecha_Agregado = Now
                                pBeINavBarraPallet.Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega,
                                                                                                       lConnection,
                                                                                                       lTransaction)
                                CadenaResultado += "Get_Codigo_By_IdBodega"
                                clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet(pBeINavBarraPallet,
                                                                                        lConnection,
                                                                                        lTransaction)
                                CadenaResultado += "Actualiza_Estado_Barras_Pallet"
                            End If

                        Next

                    Else
                        Throw New Exception("ERROR_20220914_1048: Se encontró una inconsistencia al procesar el registro de ingreso el count() de la lista de stock es 0.")
                    End If

                Else
                    Throw New Exception("ERROR_20220914_1047: Se encontró una inconsistencia al procesar el registro de ingreso la lista de stock está vacía.")
                End If

                '#EJC20190607: Insertar stock parcial (no con pallet) en interface.
                For Each pBeTransReDet As clsBeTrans_re_det In pListRecDet

                    If pBeTransReDet.IsNew Then

                        CadenaResultado += "Inserta transacciones out"

                        Dim vResultado As String = clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                                                         pIdBodega,
                                                                                                         IdTipoDocumento,
                                                                                                         pBeTransReDet,
                                                                                                         pIdOrdenCompraEnc,
                                                                                                         pIdUsuario,
                                                                                                         False,
                                                                                                         lConnection,
                                                                                                         lTransaction)

                        CadenaResultado += "Insertar_Ingreso_Parcial: " & vResultado

                        Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                        BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                        BeLoteNum.IdProductoBodega = pBeTransReDet.IdProductoBodega
                        BeLoteNum.IdRecepcionEnc = pRecEnc.IdRecepcionEnc
                        BeLoteNum.Codigo = pBeINavBarraPallet.Codigo
                        BeLoteNum.Lote = pBeINavBarraPallet.Lote
                        BeLoteNum.Lote_Numerico = pBeINavBarraPallet.Lote_Numerico
                        BeLoteNum.Cantidad = pBeTransReDet.cantidad_recibida
                        BeLoteNum.FechaIngreso = Now
                        clsLnTrans_re_det_lote_num.Insertar(BeLoteNum,
                                                            lConnection,
                                                            lTransaction)

                    End If

                    Dim vPosiciones As Integer = 0

                    If pBeTransReDet.Pallet_No_Estandar Then

                        Dim BeStockDet As New clsBeStock_det
                        BeStockDet.IdStock = BeStock.IdStock
                        BeStockDet.Posiciones = pBeTransReDet.Posiciones

                        If clsLnStock_det.Get_Single_By_IdStock(BeStockDet, lConnection, lTransaction) Then
                            '#EJC20220505: Porqué ya existe?
                            BeStockDet.Posiciones = vPosiciones
                            clsLnStock_det.Actualizar(BeStockDet, lConnection, lTransaction)
                        Else
                            clsLnStock_det.Insertar(BeStockDet, lConnection, lTransaction)
                        End If


                    End If

                Next

            End If

            CadenaResultado += " Terminé la recepción " & pRecEnc.IdRecepcionEnc.ToString

            lTransaction.Commit()

            Return CadenaResultado

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", ex.Message, CadenaResultado))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GuardarHH(ByVal pRecEnc As clsBeTrans_re_enc,
                                    ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                    ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                    ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                    ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                    ByVal pListStockRec As List(Of clsBeStock_rec),
                                    ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                    ByVal pBeStockAnt As clsBeStock,
                                    ByVal pIdEmpresa As Integer,
                                    ByVal pIdBodega As Integer,
                                    ByVal pIdUsuario As Integer,
                                    Optional pIdResolucionLp As Integer = 0,
                                    Optional pIdOperadorBodega As Integer = 0) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim CadenaResultado As String = ""
        Dim pIdOrdenCompra As Integer = 0
        Dim BeBodega As New clsBeBodega()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pRecEnc.Habilitar_Stock Then

                If Not clsLnStock.Elimina_Stock_Anterior(pBeStockAnt, CadenaResultado, lConnection, lTransaction) Then
                    CadenaResultado += "No eliminé el stock anterior "
                    Throw New Exception("No se puede realizar la modificación de la recepción, el stock fue modificado")
                Else
                    CadenaResultado += "Eliminé el stock anterior "
                End If

            End If

            If pRecOrdenCompra IsNot Nothing Then
                pIdOrdenCompra = pRecOrdenCompra.IdOrdenCompraEnc
            End If

            'Recepción Encabezado
            If pRecEnc.IsNew Then
                Insertar(pRecEnc, lConnection, lTransaction)
                CadenaResultado += "Inserté encabezado recepción "
            Else
                Actualizar(pRecEnc, lConnection, lTransaction)
                CadenaResultado += "Actualicé encabezado recepción "
            End If

            clsLnTrans_re_oc.Guarda_Trans_Re_OC(pRecEnc,
                                                pRecOrdenCompra,
                                                lConnection,
                                                lTransaction)
            CadenaResultado += "Guarda_Trans_Re_OC "

            clsLnTrans_re_det.Eliminar_Detalle(pIdOrdenCompra,
                                               pListRecDet,
                                               lConnection,
                                               lTransaction)
            CadenaResultado += "Eliminar_Detalle_Recepción "

            clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet,
                                                  lConnection,
                                                  lTransaction)
            CadenaResultado += "Guarda_Trans_re_det "

            clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                        pListRecDet,
                                                                        pListRecDetParam,
                                                                        lConnection,
                                                                        lTransaction)
            CadenaResultado += "Guarda_Trans_Re_Det_Parametros "

            CadenaResultado += "Actualiza_Cantidad_Recibida_OC "
            CadenaResultado &= clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra,
                                                                                pListRecDet,
                                                                                lConnection,
                                                                                lTransaction)

            If Not pListStockRec Is Nothing Then

                clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                                pIdBodega,
                                                pListStockRec,
                                                lConnection,
                                                lTransaction)
                CadenaResultado += " Guarda_Stock_Rec "

                clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                      pListStockRec,
                                                      lConnection,
                                                      lTransaction)
                CadenaResultado += "Guarda_Stock_Se_Rec "

            End If

            If Not pListProductoPallet Is Nothing Then

                clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                            pListProductoPallet,
                                                            lConnection,
                                                            lTransaction)
                CadenaResultado += "Guarda_Producto_Pallet "

            End If

            '#EJC20210504: Incrementar contador de LP.
            If pIdResolucionLp <> 0 Then

                Dim BeResolLp As New clsBeResolucion_lp_operador()
                BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucionLp,
                                                                  lConnection,
                                                                  lTransaction)

                If Not BeResolLp Is Nothing Then
                    BeResolLp.Correlativo_Actual += 1
                    clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp,
                                                                              lConnection,
                                                                              lTransaction)
                End If

            End If

            Dim Obj As New clsBeStock()

            If pRecEnc.Habilitar_Stock Then

                If Not pListStockRec Is Nothing Then

                    If pListStockRec.Count > 0 Then

                        For Each pBeStockRec As clsBeStock_rec In pListStockRec

                            Obj = New clsBeStock

                            pBeStockRec.IdBodega = pIdBodega
                            pBeStockRec.Fecha_Ingreso = Now
                            pBeStockRec.Fec_agr = Now
                            pBeStockRec.Fec_mod = Now

                            clsPublic.CopyObject(pBeStockRec, Obj)

                            Dim lMaxS As Integer = clsLnStock.MaxID(lConnection, lTransaction)
                            lMaxS += 1

                            Obj.IdStock = lMaxS

                            clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                                  pIdBodega,
                                                                                  pIdUsuario,
                                                                                  pBeStockRec,
                                                                                  lConnection,
                                                                                  lTransaction,
                                                                                  pIdOperadorBodega)



                            If Not BeBodega Is Nothing Then

                                If BeBodega.bloquear_lp_hh Then

                                    If clsLnStock.Existe_Lp_In_Stock(pBeStockRec.Lic_plate,
                                                                     pBeStockRec.IdBodega,
                                                                     lConnection,
                                                                     lTransaction) Then

                                        Throw New Exception("ERROR_202208182042: La licencia: " & pBeStockRec.Lic_plate & " ya existe en stock y la unicidad es requerida (bloquear_lp_hh en bodega), por favor genere una nueva licencia")

                                    End If


                                    '#GT06022023: validar que la LP del stock exista en la lista de recepción
                                    Dim pLPEncontrada = pListRecDet.Find(Function(x) x.Lic_plate = pBeStockRec.Lic_plate)

                                    If pLPEncontrada Is Nothing Then

                                        Dim alerta As String = " lp_vacia en obj IdStockRec:" & pBeStockRec.IdStockRec
                                        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), alerta)
                                        'clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, pIdRecEnc:=pRecEnc.IdRecepcionEnc)
                                        clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pConection:=lConnection, pTransaction:=lTransaction)

                                    End If



                                End If

                            End If


                            '#EJC20191218: IdBodega2Stock
                            clsLnStock.Insertar(Obj,
                                                lConnection,
                                                lTransaction)

                            clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                    lMaxS,
                                                                                    lConnection,
                                                                                    lTransaction)

                            clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                               lMaxS,
                                                                               lConnection,
                                                                               lTransaction)



                        Next

                    Else
                        Throw New Exception("ERROR_20220914_1048A: Se encontró una inconsistencia al procesar el registro de ingreso el count() de la lista de stock es 0.")
                    End If

                Else
                    Throw New Exception("ERROR_20220914_1047B: Se encontró una inconsistencia al procesar el registro de ingreso la lista de stock está vacía.")
                End If

            End If

            CadenaResultado += " Terminé la recepción " & pRecEnc.IdRecepcionEnc.ToString

            lTransaction.Commit()

            Return CadenaResultado

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", ex.Message, CadenaResultado))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' AT20220921 Recibir cantidad de copias.
    ''' </summary>
    ''' <param name="pRecEnc"></param>
    ''' <param name="pRecOrdenCompra"></param>
    ''' <param name="pListRecDet"></param>
    ''' <param name="pListRecDetParam"></param>
    ''' <param name="pListStockRecSer"></param>
    ''' <param name="pListStockRec"></param>
    ''' <param name="pListProductoPallet"></param>
    ''' <param name="pLotesRec"></param>
    ''' <param name="pIdEmpresa"></param>
    ''' <param name="pIdBodega"></param>
    ''' <param name="pIdUsuario"></param>
    ''' <param name="pIdResolucionLp"></param>
    ''' <param name="pIdOperadorBodega"></param>
    ''' <returns></returns>
    Public Shared Function GuardarHH(ByVal pRecEnc As clsBeTrans_re_enc,
                                     ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                     ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                     ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                     ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                     ByVal pListStockRec As List(Of clsBeStock_rec),
                                     ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                     ByVal pLotesRec As clsBeTrans_oc_det_lote,
                                     ByVal pIdEmpresa As Integer,
                                     ByVal pIdBodega As Integer,
                                     ByVal pIdUsuario As Integer,
                                     ByVal pIdResolucionLp As Integer,
                                     Optional pIdOperadorBodega As Integer = 0,
                                     Optional pRecepcionCajaMaster As Boolean = False) As String



        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim CadenaResultado As String = ""
        Dim pIdOrdenCompraEnc As Integer = 0
        Dim IdTipoDocumento As Integer = 0
        Dim vResultadoOc As Integer = 0
        Dim vResultadoGuardarReDet As Integer = 0
        Dim vResultadoEliminar As String = ""
        Dim vResultadoActualizarCantidadRecibidaDI As Integer = 0
        Dim vResultadoStockSeRec As Integer = 0
        Dim vResultadoStockRec As Integer = 0
        Dim vResultGuarda_Producto_Pallet As Integer = 0
        Dim vResultadoInsertMovimientos As Integer = 0
        Dim vResultadoInsertStock As Integer = 0
        Dim vResultadoStockParametroRec As Integer = 0
        Dim vResultadoInsertar_Stock_Serializado_Recepcion As Integer = 0
        Dim vResultadoActualiza_Estado_Barras_Pallet As Integer = 0
        Dim vResultadoGuardaLotes As Integer = 0
        Dim Guarda_Trans_Re_Det_Parametros As Integer = 0
        '#GT02122024: variable de control en genera LP
        Dim vGenera_LP As Boolean = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#EJC20220121: Validar que no haya sido Finalizada previamente.
            '#GT05012024: si ya esta cerrada, lanzar aviso en la HH.
            If Not Finalizada(pRecEnc.IdRecepcionEnc, lConnection, lTransaction) Then
                If pRecOrdenCompra IsNot Nothing Then
                    pIdOrdenCompraEnc = pRecOrdenCompra.IdOrdenCompraEnc
                    If pIdOrdenCompraEnc > 0 Then
                        IdTipoDocumento = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                    End If
                End If

                '#GT19012023: bandera para aplicar historico 
                Dim BeEmpresa As New clsBeEmpresa
                BeEmpresa.IdEmpresa = pIdEmpresa
                BeEmpresa = clsLnEmpresa.GetSingle(BeEmpresa,
                                                   lConnection,
                                                   lTransaction)

                '#EJC20220908:Consultar configuración de bodega antes de proceso.
                Dim BeBodega As New clsBeBodega()
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pRecEnc.IdBodega,
                                                         lConnection,
                                                         lTransaction)

                If BeBodega Is Nothing Then
                    Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega para el IdBodega: " & pRecEnc.IdBodega)
                End If

                '#CKFK20250205 Es importante agregar la presentación en la validación de si genera LP o no
                '#GT02122024: variable para saber si producto genera LP (teoricamente solo viene un producto aunque sea lista)
                vGenera_LP = pListRecDet.Any(Function(x) x.Producto.Genera_lp OrElse x.Presentacion.Genera_lp_auto)
                Dim objRecepcion = pListRecDet.FirstOrDefault()

                If Not pListStockRec Is Nothing Then

                    If pListStockRec.Count > 0 Then

                        Dim vCantStock As Integer = 0

                        For Each pBeStockRec In pListStockRec

                            Dim vLPExiste As Boolean = False
                            Dim vLPexisteEnRec = False

                            If vGenera_LP AndAlso String.IsNullOrEmpty(pBeStockRec.Lic_plate) Then
                                Throw New Exception("ERROR_02122024A_HH_GuardarRecepcion: La licencia en stock esta vacia!.")
                            End If

                            If vGenera_LP AndAlso String.IsNullOrEmpty(objRecepcion.Lic_plate) Then
                                Throw New Exception("ERROR_02122024B_HH_GuardarRecepcion: La licencia en la recepciòn esta vacia!.")
                            End If

                            '#GT04122024: validar en recepcion y stock que la licencia no exista, hacerlo solo en una causa inconsistencia
                            vLPexisteEnRec = clsLnTrans_re_det.Existe_By_IdRecepcionEnc_And_IdRecepcionDet(objRecepcion, lConnection, lTransaction)
                            vLPExiste = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(pBeStockRec.Lic_plate, pIdBodega, lConnection, lTransaction)

                            If (vLPExiste OrElse vLPexisteEnRec) AndAlso Not pRecepcionCajaMaster Then
                                Throw New Exception("ERROR_20220823C_HH_GuardarRecepcion: La licencia: " & pBeStockRec.Lic_plate & " fue registrada previamente.")
                            Else
                                If (pRecepcionCajaMaster AndAlso vCantStock = 0) OrElse Not pRecepcionCajaMaster Then
                                    '#CKFK20250205 Agregué este sino para que se actualice la resolicop
                                    If pIdResolucionLp <> 0 Then

                                        Dim BeResolLp As New clsBeResolucion_lp_operador()
                                        BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucionLp, lConnection, lTransaction)

                                        If Not BeResolLp Is Nothing Then
                                            BeResolLp.Correlativo_Actual += 1
                                            clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp,
                                                                                                      lConnection,
                                                                                                      lTransaction)
                                        End If

                                    End If
                                End If
                            End If
                            vCantStock += 1
                        Next

                        If vGenera_LP AndAlso pIdResolucionLp <= 0 Then
                            Throw New Exception("ERROR_02122024_HH_GuardarRecepcion: El producto maneja lic_plate, pero la resoluciòn no es correcta!." & pIdResolucionLp)
                        End If

                        If Not vGenera_LP And pIdResolucionLp <= 0 Then
                            Dim vMsgError As String = "AVISO_20242211_HH_GuardarRecepcion recepcion sin licencia : " & pRecEnc.IdRecepcionEnc
                            clsLnLog_error_wms.Agregar_Error(vMsgError)
                        End If

                        Dim vResultInsertEncabezadoRec As Integer = 0
                        'Recepción Encabezado
                        If pRecEnc.IsNew Then

                            vResultInsertEncabezadoRec = Insertar(pRecEnc,
                                                                  lConnection,
                                                                  lTransaction)

                            If vResultInsertEncabezadoRec > 0 Then
                                CadenaResultado += "Inserté encabezado recepción " & vResultInsertEncabezadoRec
                            Else
                                Throw New Exception("ERROR_202210051030C: No se pudo insertar el encabezado de la recepción")
                            End If

                        End If

                        '#CKFK20221101 Insertar datos en la trans_re_det con la lista de trans_re_det
                        If Not pListRecDet Is Nothing Then

                            If pListRecDet.Count > 0 Then

                                vResultadoEliminar = clsLnTrans_re_det.Eliminar_Detalle(pIdOrdenCompraEnc,
                                                                                        pListRecDet,
                                                                                        lConnection,
                                                                                        lTransaction)

                                '#CKFK20240806 La función de arriba no devuelve un entero
                                ' If vResultadoEliminar > 0 Then
                                CadenaResultado += "Eliminar_Detalle_Recepción " & vResultadoEliminar
                                ' End If


                                '#GT05012024:validar AQUI que la lp si la tuviera en eliminar detalle, no exista antes de hacer la nueva inserción
                                For Each pRecepcionDet In pListRecDet
                                    If clsLnTrans_re_det.Existe_By_BeRecepcionDet(pRecepcionDet, lConnection, lTransaction) Then

                                        Dim vMsgError As String = "ERROR_19122024_HH_GuardarRecepcion: La recepcion " & pRecepcionDet.IdRecepcionEnc & " con linea: " & pRecepcionDet.IdRecepcionDet & " ya existe"
                                        clsLnLog_error_wms.Agregar_Error(vMsgError)

                                        Throw New Exception("ERROR_19122024_HH_GuardarRecepcion: La linea de recepcion existe, no se puede guardar nuevamente.")
                                    End If
                                Next

                                vResultadoGuardarReDet = clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet,
                                                                                           pListStockRec,
                                                                                           lConnection,
                                                                                           lTransaction)

                                If vResultadoGuardarReDet > 0 Then
                                    CadenaResultado += "Guarda_Trans_re_det " & vResultadoGuardarReDet
                                End If

                                '#EJC20210412:Agregado para actualizar la cantidad recibida por lote.
                                vResultadoGuardaLotes = clsLnTrans_oc_det_lote.Guarda_Trans_re_det_lote(pLotesRec,
                                                                                                    lConnection,
                                                                                                    lTransaction)

                                If vResultadoGuardaLotes > 0 Then
                                    CadenaResultado += "clsLnTrans_oc_det_lote " & vResultadoGuardaLotes
                                End If

                                Guarda_Trans_Re_Det_Parametros = clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                                                                         pListRecDet,
                                                                                                                         pListRecDetParam,
                                                                                                                         lConnection,
                                                                                                                         lTransaction)

                                If Guarda_Trans_Re_Det_Parametros > 0 Then
                                    CadenaResultado += "Guarda_Trans_Re_Det_Parametros " & Guarda_Trans_Re_Det_Parametros
                                End If

                            Else
                                Throw New Exception("ERROR_202210051030F: El count de la lista de recepción es 0.")
                            End If

                        Else
                            Throw New Exception("ERROR_202210051030E: La lista de RecDet Is Nothing.")
                        End If

                        If pRecOrdenCompra IsNot Nothing Then

                            If Not pListRecDet Is Nothing Then

                                If pListRecDet.Count > 0 Then

                                    vResultadoActualizarCantidadRecibidaDI = clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra,
                                                                                                                              pListRecDet,
                                                                                                                              lConnection,
                                                                                                                              lTransaction)

                                    If vResultadoActualizarCantidadRecibidaDI > 0 Then
                                        CadenaResultado += "Actualiza_Cantidad_Recibida_OC " & vResultadoActualizarCantidadRecibidaDI
                                    Else
                                        Throw New Exception("ERROR_202210051030G: No se pudo actualizar la cantidad recibida en el documento de ingreso.")
                                    End If

                                End If

                            End If

                        End If

                        '#CKFK20221101 Insertar datos en la tablas stock_rec con la lista de stock_rec
                        If Not pListStockRec Is Nothing Then

                            If pListStockRec.Count > 0 Then

                                vResultadoStockRec = clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                                                                     pIdBodega,
                                                                                     pListStockRec,
                                                                                     lConnection,
                                                                                     lTransaction)

                                If vResultadoStockRec > 0 Then
                                    CadenaResultado += " Guarda_Stock_Rec " & vResultadoStockRec
                                Else
                                    Throw New Exception("ERROR_202210051058: No se pudo insertar en stock_rec.")
                                End If

                                vResultadoStockSeRec = clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                                                             pListStockRec,
                                                                                             lConnection,
                                                                                             lTransaction)

                                If vResultadoStockSeRec > 0 Then
                                    CadenaResultado += "Guarda_Stock_Se_Rec " & vResultadoStockSeRec
                                End If

                            Else
                                Throw New Exception("#ERR20200317A: La lista de stock no tiene registros.")
                            End If

                        Else
                            Throw New Exception("#ERR20200317B: La lista de stock para recepción está vacía.")
                        End If

                        If Not pListProductoPallet Is Nothing Then

                            vResultGuarda_Producto_Pallet = clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                                                                         pListProductoPallet,
                                                                                                         lConnection,
                                                                                                         lTransaction)

                            If vResultGuarda_Producto_Pallet > 0 Then
                                CadenaResultado += "Guarda_Producto_Pallet " & vResultGuarda_Producto_Pallet
                            End If

                        End If

                        Dim BeStock As New clsBeStock()

                        '#CKFK20221101 Insertar datos en las tablas stock y movimientos e i_nav_transacciones_out con la lista de stock_rec
                        If pRecEnc.Habilitar_Stock Then

                            Dim pBeINavBarraPallet As New clsBeI_nav_barras_pallet

                            If Not pListStockRec Is Nothing Then

                                If pListStockRec.Count > 0 Then

                                    For Each pBeStockRec As clsBeStock_rec In pListStockRec

                                        BeStock = New clsBeStock
                                        pBeStockRec.IdBodega = pIdBodega
                                        '#GT21102022_1600: si el obj se itera mas de una vez, validar que en cada insert, es único
                                        vResultadoInsertMovimientos = 0


                                        '#EJC20200207: Para evitar fechas malas de la HH
                                        pBeStockRec.Fecha_Ingreso = Now
                                        pBeStockRec.Fec_agr = Now
                                        pBeStockRec.Fec_mod = Now
                                        clsPublic.CopyObject(pBeStockRec, BeStock)

                                        vResultadoInsertMovimientos = clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                                                                            pIdBodega,
                                                                                                                            pIdUsuario,
                                                                                                                            pBeStockRec,
                                                                                                                            lConnection,
                                                                                                                            lTransaction,
                                                                                                                            pIdOperadorBodega)

                                        If vResultadoInsertMovimientos > 0 Then

                                            CadenaResultado += "Insertar_Movimientos_Recepcion: " & vResultadoInsertMovimientos


                                            '#EJC2022102513356: Corrección por concurrencia.
                                            BeStock.IdStock = clsLnStock.MaxID(lConnection, lTransaction) + 1

                                            '#EJC20191218: IdBodega2Stock
                                            vResultadoInsertStock = clsLnStock.Insertar(BeStock,
                                                                                    lConnection,
                                                                                    lTransaction)

                                            If vResultadoInsertStock > 0 Then

                                                CadenaResultado += "Inserta_Stock: " & vResultadoInsertStock



                                                vResultadoStockParametroRec = clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                                                                      BeStock.IdStock,
                                                                                                                                      lConnection,
                                                                                                                                      lTransaction)

                                                If vResultadoStockParametroRec > 0 Then
                                                    CadenaResultado += "Insertar_Stock_Parametro_Recepcion " & vResultadoStockParametroRec
                                                End If




                                                vResultadoInsertar_Stock_Serializado_Recepcion = clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                                                                                                    BeStock.IdStock,
                                                                                                                                                    lConnection,
                                                                                                                                                    lTransaction)

                                                If vResultadoInsertar_Stock_Serializado_Recepcion > 0 Then
                                                    CadenaResultado += "Insertar_Stock_Serializado_Recepcion: " & vResultadoInsertar_Stock_Serializado_Recepcion
                                                End If
                                            Else
                                                '#GT21102022_1600: sino inserta stock se lanza excepción
                                                Throw New Exception("ERROR_202210211600: No se pudo insertar el stock.")
                                            End If

                                        Else
                                            Throw New Exception("ERROR_202210051111: No se pudo insertar el movimiento.")
                                        End If


                                        '#EJC20190329_0538PM: Marcar el pallet como recibido.
                                        If pBeStockRec.Lic_plate <> "" Then

                                            pBeINavBarraPallet.Recibido = True
                                            pBeINavBarraPallet.IdRecepcion = pRecEnc.IdRecepcionEnc
                                            pBeINavBarraPallet.Codigo_barra = pBeStockRec.Lic_plate
                                            pBeINavBarraPallet.Fecha_Ingreso = Now
                                            pBeINavBarraPallet.Fecha_Agregado = Now
                                            pBeINavBarraPallet.Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)

                                            If Not pBeINavBarraPallet.Bodega_Destino Is Nothing Then
                                                CadenaResultado += "Get_Codigo_By_IdBodega: " & pBeINavBarraPallet.Bodega_Destino
                                            Else
                                                Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega destino para el IdBodega: " & pIdBodega)
                                            End If

                                            vResultadoActualiza_Estado_Barras_Pallet = clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet(pBeINavBarraPallet,
                                                                                                                                               lConnection,
                                                                                                                                               lTransaction)

                                            If vResultadoActualiza_Estado_Barras_Pallet > 0 Then
                                                CadenaResultado += "Actualiza_Estado_Barras_Pallet: " & vResultadoActualiza_Estado_Barras_Pallet
                                            End If

                                        End If

                                    Next

                                Else
                                    Throw New Exception("ERROR_20220914_1048: Se encontró una inconsistencia al procesar el registro de ingreso el count() de la lista de stock es 0.")
                                End If

                            Else
                                Throw New Exception("ERROR_20220914_1047: Se encontró una inconsistencia al procesar el registro de ingreso la lista de stock está vacía.")
                            End If

                            '#EJC20190607: Insertar stock parcial (no con pallet) en interface
                            For Each pBeTransReDet As clsBeTrans_re_det In pListRecDet

                                If pListRecDet.Count > 0 Then

                                    If pBeTransReDet.IsNew Then

                                        CadenaResultado += "Inserta transacciones out"

                                        Dim vResultado As String = clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                                                                         pIdBodega,
                                                                                                                         IdTipoDocumento,
                                                                                                                         pBeTransReDet,
                                                                                                                         pIdOrdenCompraEnc,
                                                                                                                         pIdUsuario,
                                                                                                                         False,
                                                                                                                         lConnection,
                                                                                                                         lTransaction)

                                        CadenaResultado += "Insertar_Ingreso_Parcial: " & vResultado

                                        Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                                        BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                                        BeLoteNum.IdProductoBodega = pBeTransReDet.IdProductoBodega
                                        BeLoteNum.IdRecepcionEnc = pRecEnc.IdRecepcionEnc
                                        BeLoteNum.Codigo = pBeINavBarraPallet.Codigo
                                        BeLoteNum.Lote = pBeINavBarraPallet.Lote
                                        BeLoteNum.Lote_Numerico = pBeINavBarraPallet.Lote_Numerico
                                        BeLoteNum.Cantidad = pBeTransReDet.cantidad_recibida
                                        BeLoteNum.FechaIngreso = Now
                                        clsLnTrans_re_det_lote_num.Insertar(BeLoteNum,
                                                                        lConnection,
                                                                        lTransaction)

                                    End If

                                    Dim vPosiciones As Integer = 0

                                    If pBeTransReDet.Pallet_No_Estandar Then

                                        Dim BeStockDet As New clsBeStock_det
                                        BeStockDet.IdStock = BeStock.IdStock
                                        BeStockDet.Posiciones = pBeTransReDet.Posiciones

                                        If clsLnStock_det.Get_Single_By_IdStock(BeStockDet, lConnection, lTransaction) Then
                                            BeStockDet.Posiciones = vPosiciones
                                            clsLnStock_det.Actualizar(BeStockDet, lConnection, lTransaction)
                                        Else
                                            clsLnStock_det.Insertar(BeStockDet, lConnection, lTransaction)
                                        End If

                                    End If

                                End If

                            Next

                        End If

                        CadenaResultado += " Terminé la recepción " & pRecEnc.IdRecepcionEnc.ToString

                    Else
                        Throw New Exception("ERROR_202210051030A: El count de la lista de stock es 0.")
                    End If

                Else
                    Throw New Exception("ERROR_202210051030B: La lista de stock Is Nothing.")
                End If

            Else
                Throw New Exception("ERROR_DE_PROCESO_20241205_HH: La recepción " & pRecEnc.IdRecepcionEnc & " fue previamente finalizada.")
            End If

            lTransaction.Commit()

            Return CadenaResultado


        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", ex.Message, CadenaResultado))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Reglas_De_Recepcion_Permiten_Ingreso(ByVal pRecEnc As clsBeTrans_re_enc,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As Boolean

        Reglas_De_Recepcion_Permiten_Ingreso = False

        Dim lProductosConDiferencia As New List(Of String)

        Try

            If Not pRecEnc.OrdenCompraRec Is Nothing Then

                If Not pRecEnc.OrdenCompraRec.OC.IdTipoIngresoOC = tTipoDocumentoIngreso.Ingreso_Inventario_Inicial Then

                    Dim vReglaProp As New clsBePropietario_reglas_enc

                    Dim ListaRegla As New List(Of clsBePropietario_reglas_enc)

                    pRecEnc.PropietarioBodega.IdPropietario = clsLnPropietarios.Get_IdPropietario(pRecEnc.PropietarioBodega.IdBodega,
                                                                                              pRecEnc.PropietarioBodega.IdPropietarioBodega,
                                                                                              lConnection,
                                                                                              lTransaction)

                    If pRecEnc.PropietarioBodega.IdPropietario = 0 Then
                        pRecEnc.PropietarioBodega.IdPropietario = pRecEnc.PropietarioBodega.IdPropietario
                    End If

                    ListaRegla = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(pRecEnc.PropietarioBodega.IdPropietario,
                                                                                  lConnection,
                                                                                  lTransaction)

                    If Not ListaRegla Is Nothing Then
                        ListaRegla = ListaRegla.FindAll(Function(x) x.ReglasDet.Count > 0)
                    End If

                    '#EJC20170712
                    'Busca si existe la regla, si existe toma el valor que indique en rechazar 
                    'para saber si se acepta o se rechaza, si no existe la regla, se permite por defecto        
                    Using BeReglaRec As New clsBeReglas_RecepcionRes()

                        '#CKFK 20180928 Se agregó la inicialización de las reglas en False
                        BeReglaRec.PermitirProductoFaltantes = False
                        BeReglaRec.PermitirProductosAdicionales = False
                        BeReglaRec.PermitirCantidadFaltantePorProducto = False
                        BeReglaRec.PermitirCantidadSobrantePorProducto = False
                        BeReglaRec.PermitirCostoDiferentePorProducto = False
                        BeReglaRec.PermitirPesoMenor = False
                        BeReglaRec.PermitirPesoMayor = False

                        '#EJC202107141551_RP: Por defecto, una regla de recepción es válida y por excepción se validan(activan/desactivan) las reglas específicas configuradas.
                        vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 1) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirProductoFaltantes = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirProductoFaltantes = True
                        vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 2) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirProductosAdicionales = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirProductosAdicionales = True
                        vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 3) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCantidadFaltantePorProducto = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirCantidadFaltantePorProducto = True
                        vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 4) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCantidadSobrantePorProducto = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirCantidadSobrantePorProducto = True
                        vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 5) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCostoDiferentePorProducto = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirCostoDiferentePorProducto = True
                        vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 6) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirPesoMenor = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirPesoMenor = True
                        vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 7) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirPesoMayor = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirPesoMayor = True

                        Dim vMensaje As String = ""

                        'Si hay reglas definidas por propietario
                        If ListaRegla.Count > 0 Then

                            'Si la recepción fue con OC.
                            If pRecEnc.OrdenCompraRec IsNot Nothing AndAlso pRecEnc.OrdenCompraRec.IdOrdenCompraEnc > 0 AndAlso pRecEnc.OrdenCompraRec.IsNew = False Then

                                'Obtener detalle de la OC.
                                Dim ObjOC As clsBeTrans_oc_enc = clsLnTrans_oc_enc.GetSingle(pRecEnc.OrdenCompraRec.IdOrdenCompraEnc,
                                                                                             lConnection,
                                                                                             lTransaction)

                                ' Validamos que traiga datos 
                                If ObjOC IsNot Nothing AndAlso ObjOC.IdOrdenCompraEnc > 0 AndAlso ObjOC.IsNew = False Then

                                    Dim lRecDetByProd As New List(Of clsBeTrans_re_det)
                                    Dim vPresRec As New clsBeProducto_Presentacion
                                    Dim vCantidadRecibidaUmBas As Double = 0
                                    Dim vPesoRecibido As Double = 0
                                    Dim vCantidadOCUMBas As Double = 0
                                    Dim vDiferenciaCantUmBas As Double = 0
                                    Dim vDiferenciaCosto As Double = 0
                                    Dim vDiferenciaPeso As Double = 0
                                    Dim vCostoRec As Double = 0

                                    pRecEnc.Detalle = clsLnTrans_re_det.Get_All_By_IdRecepcionEnc(pRecEnc.IdRecepcionEnc,
                                                                                              lConnection,
                                                                                              lTransaction)

                                    'Recorremos el detalle de la Orden de Compra
                                    For Each ocd As clsBeTrans_oc_det In ObjOC.DetalleOC

                                        If Not ocd.IdPresentacion = 0 Then
                                            vPresRec.IdPresentacion = ocd.IdPresentacion
                                            vPresRec = clsLnProducto_presentacion.GetSingle(vPresRec.IdPresentacion,
                                                                                        lConnection,
                                                                                        lTransaction)
                                            '#EJC20210630:Mostrar la cantidad en presentación.
                                            vCantidadOCUMBas = ocd.Cantidad
                                        Else
                                            vCantidadOCUMBas = ocd.Cantidad
                                        End If

                                        'Buscar en la recepción cuanto se recibió de cada producto de la OC.
                                        lRecDetByProd = pRecEnc.Detalle.FindAll(Function(x) x.IdProductoBodega = ocd.IdProductoBodega AndAlso x.No_Linea = ocd.No_Linea)

                                        vCantidadRecibidaUmBas = 0

                                        If Not lRecDetByProd Is Nothing Then

                                            For Each RecDetProd As clsBeTrans_re_det In lRecDetByProd

                                                If Not RecDetProd.IdPresentacion = 0 Then

                                                    vPresRec.IdPresentacion = RecDetProd.IdPresentacion
                                                    vPresRec = clsLnProducto_presentacion.GetSingle(vPresRec.IdPresentacion,
                                                                                                lConnection,
                                                                                                lTransaction)

                                                    If Not vPresRec.EsPallet Then
                                                        '#EJC20210630:No multiplicar por el factor
                                                        'vCantidadRecibidaUmBas += RecDetProd.cantidad_recibida * vPresRec.Factor
                                                        vCantidadRecibidaUmBas += RecDetProd.cantidad_recibida
                                                    Else
                                                        vCantidadRecibidaUmBas += RecDetProd.cantidad_recibida * vPresRec.Factor * vPresRec.CajasPorCama * vPresRec.CamasPorTarima
                                                    End If

                                                Else
                                                    vCantidadRecibidaUmBas += RecDetProd.cantidad_recibida
                                                End If

                                                If vDiferenciaCosto = 0 Then
                                                    vCostoRec = RecDetProd.Costo
                                                    vDiferenciaCosto = Math.Round(ocd.Costo - RecDetProd.Costo, 2)
                                                End If

                                                vPesoRecibido += RecDetProd.Peso

                                            Next RecDetProd

                                        Else

                                            'No se recepcionó ese producto.
                                            If Not BeReglaRec.PermitirProductoFaltantes Then

                                                '#CKFK 20210708
                                                vMensaje = String.Format("La regla de recepción configurada " &
                                            " para el propietario no permite faltante de producto. " &
                                            " Producto en OC.: {0} Cantidad O.C.: {1} " &
                                            " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", ocd.Producto.Codigo, ocd.Producto.Nombre), ocd.Cantidad, ocd.Cantidad_recibida)

                                                Throw New Exception(vMensaje)

                                            End If

                                        End If

                                        Dim vCantidadOriginalUMBas As Double = vCantidadOCUMBas

                                        If Not vPresRec Is Nothing Then

                                            If Not vPresRec.Factor = 0 Then
                                                vCantidadOriginalUMBas = vCantidadOCUMBas * vPresRec.Factor
                                            Else
                                                vCantidadOriginalUMBas = vCantidadOCUMBas
                                            End If

                                        End If

                                        vDiferenciaCantUmBas = Math.Round(vCantidadOriginalUMBas - vCantidadRecibidaUmBas, 6)

                                        vDiferenciaPeso = ocd.Peso - vPesoRecibido

                                        If vDiferenciaCantUmBas <> 0 Then
                                            lProductosConDiferencia.Add(ocd.Producto.IdProducto)
                                        End If

                                        Select Case vDiferenciaCantUmBas

                                            Case Is = 0
                                                    'No hay diferencia.

                                            Case Is > 0

                                                If Not BeReglaRec.PermitirCantidadFaltantePorProducto Then

                                                    vMensaje = String.Format("La regla de recepción configurada " &
                                                                        " para el propietario no permite recibir menos producto " &
                                                                        " que el indicado por la orden de compra " &
                                                                        " Producto: {0} Cantidad O.C.: {1} " &
                                                                        " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", ocd.Producto.Codigo, ocd.Producto.Nombre), vCantidadOCUMBas, vCantidadRecibidaUmBas)

                                                    Throw New Exception(vMensaje)
                                                    ' Se recibió de menos que lo indicado por la O.C.

                                                End If

                                            Case Is < 0

                                                If Not BeReglaRec.PermitirCantidadSobrantePorProducto Then

                                                    vMensaje = String.Format("La regla de recepción configurada " &
                                                                        " para el propietario no permite recibir más producto " &
                                                                        " que el indicado por la orden de compra " &
                                                                        " Producto:{0} Cantidad O.C.: {1} " &
                                                                        " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", ocd.Producto.Codigo, ocd.Producto.Nombre), vCantidadOCUMBas, vCantidadRecibidaUmBas)

                                                    Throw New Exception(vMensaje)
                                                    ' Se recibió más que lo indicado por la O.C.

                                                End If

                                            Case Else

                                                Exit Select

                                        End Select

                                        Select Case vDiferenciaCosto

                                            Case Is = 0
                                                    'No hay diferencia.

                                            Case Is <> 0

                                                'El costo de la línea en la O.C. es diferente que el de la recepción.
                                                If Not BeReglaRec.PermitirCostoDiferentePorProducto Then

                                                    vMensaje = String.Format("La regla de recepción configurada " &
                                                                   " para el propietario no permite recibir productos " &
                                                                   " con costo diferente que el indicado por la orden de compra " &
                                                                   " Producto: {0} Costo en O.C.: {1} " &
                                                                   " Costo en Recepción.: {2}", String.Format("""{0} - {1}""", ocd.Producto.Codigo, ocd.Producto.Nombre), ocd.Costo, vCostoRec)

                                                    Throw New Exception(vMensaje)

                                                End If

                                            Case Else
                                                Exit Select

                                        End Select

                                        Select Case vDiferenciaPeso

                                            Case Is = 0
                                                    'No hay diferencia.
                                            Case Is > 0

                                                ' Se recibió menos peso que lo indicado por la O.C.   
                                                If Not BeReglaRec.PermitirPesoMenor Then

                                                    vMensaje = String.Format("La regla de recepción configurada " &
                                            " para el propietario no permite recibir menos peso " &
                                            " que el indicado por la orden de compra " &
                                            " Producto: {0} Peso O.C.: {1} " &
                                            " Peso Recepción.: {2}", String.Format("""{0} - {1}""", ocd.Producto.Codigo, ocd.Producto.Nombre), ocd.Peso, vPesoRecibido)

                                                    Throw New Exception(vMensaje)

                                                End If


                                            Case Is < 0

                                                If Not BeReglaRec.PermitirPesoMayor Then
                                                    vMensaje = String.Format("La regla de recepción configurada " &
                                                                        " para el propietario no permite recibir más peso " &
                                                                        " que el indicado por la orden de compra " &
                                                                        " Producto: {0} Peso O.C.: {1} " &
                                                                        " Peso Recepción.: {2}", String.Format("""{0} - {1}""", ocd.Producto.Codigo, ocd.Producto.Nombre), ocd.Peso, vPesoRecibido)

                                                    Throw New Exception(vMensaje)
                                                End If

                                            Case Else
                                                Exit Select

                                        End Select

                                    Next ocd

                                End If

                            End If

                        End If

                    End Using

                End If

            End If

            Reglas_De_Recepcion_Permiten_Ingreso = True

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva opcion de log para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdBodega:=pRecEnc.IdBodega,
                                                 pIdUsuarioAgr:=pRecEnc.User_agr,
                                                 pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Sub Finalizar_Recepcion(ByVal pRecEnc As clsBeTrans_re_enc,
                                          ByVal backOrder As Boolean,
                                          ByVal pIdOrdenCompraEnc As Integer,
                                          ByVal pIdRecepcionEnc As Integer,
                                          ByVal pIdEmpresa As Integer,
                                          ByVal pIdBodega As Integer,
                                          ByVal pIdUsuario As String,
                                          ByVal pListObjDetR As List(Of clsBeTrans_re_det),
                                          ByVal pHabilitarStock As Boolean)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeTMSTicket As New clsBeTms_ticket

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim listaStockRec As List(Of clsBeStock_rec) = clsLnStock_rec.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)

            '#EJC20220121: Validar que no haya sido Finalizada previamente.
            If Not Finalizada(pIdRecepcionEnc, lConnection, lTransaction) Then

                '#GT10102025: validar que no haya sido anulada previamente.
                If Not Anulada(pIdRecepcionEnc, lConnection, lTransaction) Then


                    '#EJC20220121_1102: Cambiar el estado primero.
                    Actualizar_Estado_Cerrado_Recepcion(pIdRecepcionEnc,
                                                        lConnection,
                                                        lTransaction)

                    Dim BeOc As New clsBeTrans_oc_enc
                    BeOc = clsLnTrans_oc_enc.GetSingle(pIdOrdenCompraEnc,
                                                       lConnection,
                                                       lTransaction)
                    Dim BeTipoIngreso As New clsBeTrans_oc_ti
                    BeTipoIngreso = clsLnTrans_oc_ti.GetSingle(BeOc.IdTipoIngresoOC,
                                                               lConnection,
                                                               lTransaction)
                    If Not BeTipoIngreso Is Nothing Then
                        If BeTipoIngreso.Marcar_Registros_Enviados_MI3 Then
                            clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(pIdOrdenCompraEnc,
                                                                              True,
                                                                              lConnection,
                                                                              lTransaction)
                        End If
                    End If

                    '#MECR23092025: Se agrego bitacora de logs para recepciones
                    Dim msjAdvertencia As String = "#240313A: Se cerró la recepción: " & pIdRecepcionEnc & " IdUsuario_BOF: " & pIdUsuario
                    clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia,
                                                     pIdEmpresa:=pIdEmpresa,
                                                     pIdBodega:=pIdBodega,
                                                     pIdUsuarioAgr:=pIdUsuario,
                                                     pIdRecEnc:=pIdRecepcionEnc,
                                                     pConection:=lConnection,
                                                     pTransaction:=lTransaction)

                    If Not Registros_Pendientes_Push(pIdRecepcionEnc, lConnection, lTransaction) Then

                        If Reglas_De_Recepcion_Permiten_Ingreso(pRecEnc,
                                                                lConnection,
                                                                lTransaction) Then

                            Dim lMaxS As Integer = clsLnStock.MaxID(lConnection,
                                                                    lTransaction)

                            If listaStockRec IsNot Nothing AndAlso listaStockRec.Count > 0 Then

                                If Not pHabilitarStock Then

                                    '#MECR23092025: Se agrego bitacora de logs para recepciones
                                    Dim msjAdvertencia1 As String = "ADVERTENCIA_202302230102:  Se está finalizando la recepción: " & pIdRecepcionEnc & " con Habilitar_Stock = False, Usuario: " & pIdUsuario
                                    'clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia1, pIdEmpresa, pIdBodega, pIdUsuario, pIdRecEnc:=pIdRecepcionEnc)
                                    clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia1,
                                                                         pIdEmpresa:=pIdEmpresa,
                                                                         pIdBodega:=pIdBodega,
                                                                         pIdUsuarioAgr:=pIdUsuario,
                                                                         pIdRecEnc:=pIdRecepcionEnc,
                                                                         pConection:=lConnection,
                                                                         pTransaction:=lTransaction)

                                    Habilitar_Stock_Desde_StockRec(pIdEmpresa,
                                                               pIdBodega,
                                                               pIdOrdenCompraEnc,
                                                               pIdUsuario,
                                                               listaStockRec,
                                                               pListObjDetR,
                                                               lConnection,
                                                               lTransaction)
                                Else

                                    Habilitar_Stock_Desde_Detalle_Recepcion(pIdRecepcionEnc,
                                                                            pIdOrdenCompraEnc,
                                                                            pIdUsuario,
                                                                            pIdEmpresa,
                                                                            pIdBodega,
                                                                            pRecEnc,
                                                                            lConnection,
                                                                            lTransaction)

                                End If

                            End If

                            clsLnStock_rec.Actualiza_Stock_Rec(listaStockRec,
                                                               lConnection,
                                                               lTransaction)

                            Actualizar_Estado_Pedido_Ingreso(pIdOrdenCompraEnc,
                                                             pIdRecepcionEnc,
                                                             lConnection,
                                                             lTransaction,
                                                             backOrder)

                            Actualizar_Hora_Fin_Recepcion(pIdOrdenCompraEnc,
                                                          pIdRecepcionEnc,
                                                          lConnection,
                                                          lTransaction)


                            Dim BeTareaHH As New clsBeTarea_hh
                            BeTareaHH = clsLnTarea_hh.GetSingle(1,
                                                                pIdRecepcionEnc,
                                                                pRecEnc.PropietarioBodega.IdPropietario,
                                                                lConnection,
                                                                lTransaction)

                            If Not BeTareaHH Is Nothing Then

                                If Not BeTareaHH.IdEstado = 4 Then

                                    clsLnTarea_hh.Finalizar_Tarea_Recepcion(pIdRecepcionEnc,
                                                                            lConnection,
                                                                            lTransaction)

                                Else
                                    Throw New Exception("Error_202211011918: Al parecer la recepción ya fue finalizada.")
                                End If

                            End If

                            If Not pIdOrdenCompraEnc = 0 Then

                                '#EJC20220803_1536: Validar que si tiene documento de ingreso si tiene o no ticket de TMS.
                                BeTMSTicket = clsLnTrans_oc_enc.Get_BeTicket_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                                'Si tiene ticket de TMS el documento de ingreso.
                                If Not BeTMSTicket Is Nothing Then

                                    clsLnTms_ticket.Actualizar_Tms_Ticket_Finalizado(BeTMSTicket.IdTicket,
                                                                                     lConnection,
                                                                                     lTransaction)

                                End If

                            End If

                        End If

                    Else
                        Throw New Exception("Error_20220308: La recepción tiene registros pendientes de push")
                    End If

                Else
                    Throw New Exception("Error_20220121_0004: La recepción fue anulada previamente.")
                End If

            Else
                Throw New Exception("Error_20220121_0004: La recepción fue finalizada previamente.")
            End If

            lTransaction.Commit()

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva opcion de log para recepciones.
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=pIdEmpresa,
                                                 pIdBodega:=pIdBodega,
                                                 pIdUsuarioAgr:=pIdUsuario,
                                                 pIdRecEnc:=pIdRecepcionEnc)
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Private Shared Function Habilitar_Stock_Desde_StockRec(ByVal pIdEmpresa As Integer,
                                                           ByVal pIdBodega As Integer,
                                                           ByVal pIdOrdenCompraEnc As Integer,
                                                           ByVal pIdUsuario As Integer,
                                                           ByVal listaStockRec As List(Of clsBeStock_rec),
                                                           ByVal pListObjDetR As List(Of clsBeTrans_re_det),
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As Boolean

        Habilitar_Stock_Desde_StockRec = False

        Dim lMaxS As Integer = 0
        Dim vIdPropietarioBodega As Integer = 0

        Try

            '#EJC20200715: GetMaxIdStock before insert
            lMaxS = clsLnStock.MaxID(lConnection, lTransaction)

            Dim BeEmpresa As New clsBeEmpresa
            BeEmpresa.IdEmpresa = pIdEmpresa
            BeEmpresa = clsLnEmpresa.GetSingle(BeEmpresa, lConnection, lTransaction)

            For Each BeStockRec As clsBeStock_rec In listaStockRec

                Dim BeStock As New clsBeStock()
                BeStockRec.IdBodega = pIdBodega
                clsPublic.CopyObject(BeStockRec, BeStock)

                lMaxS += 1

                BeStock.IdStock = lMaxS

                clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                      pIdBodega,
                                                                      pIdUsuario,
                                                                      BeStockRec,
                                                                      lConnection,
                                                                      lTransaction)
                clsLnStock.Insertar(BeStock,
                                    lConnection,
                                    lTransaction)

                clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(BeStockRec,
                                                                        lMaxS,
                                                                        lConnection,
                                                                        lTransaction)

                clsLnStock_se.Insertar_Stock_Serializado_Recepcion(BeStockRec,
                                                                   lMaxS,
                                                                   lConnection,
                                                                   lTransaction)
                BeStockRec.Regularizado = True
                BeStockRec.Fecha_regularizacion = Now

                vIdPropietarioBodega = BeStockRec.IdPropietarioBodega

                '#GT20012023: genera el historico cuando finaliza la recepción y no esta habilitado Stock disponible.
                'If BeEmpresa.Generar_Stock_Jornada Then
                '    Recepcion_Historico_By_Proces(False, pIdEmpresa, pIdBodega, pIdUsuario, BeStock.IdStock,
                '                                                                            lConnection,
                '                                                                            lTransaction)
                'End If

            Next

            clsLnI_nav_transacciones_out.Insertar_Ingreso(pIdEmpresa,
                                                          pIdBodega,
                                                          pListObjDetR,
                                                          pIdOrdenCompraEnc,
                                                          pIdUsuario,
                                                          vIdPropietarioBodega,
                                                          lConnection,
                                                          lTransaction)

            Habilitar_Stock_Desde_StockRec = True

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva opcion de log para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=pIdEmpresa,
                                                 pIdBodega:=pIdBodega,
                                                 pIdUsuarioAgr:=pIdUsuario,
                                                 pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

    Private Shared Function Habilitar_Stock_Desde_Detalle_Recepcion(ByVal pIdRecepcionEnc As Integer,
                                                                    ByVal pIdOrdenCompraEnc As Integer,
                                                                    ByVal pIdUsuario As Integer,
                                                                    ByVal pIdEmpresa As Integer,
                                                                    ByVal pIdBodega As Integer,
                                                                    ByVal pBeReEnc As clsBeTrans_re_enc,
                                                                    ByVal lConnection As SqlConnection,
                                                                    ByVal lTransaction As SqlTransaction) As Boolean

        Habilitar_Stock_Desde_Detalle_Recepcion = False

        Try

            Dim BeTransReEnc As New clsBeTrans_re_enc

            BeTransReEnc = GetSingle(pIdRecepcionEnc,
                                    lConnection,
                                    lTransaction)

            Dim lUsaHH As Boolean = clsLnTrans_re_tr_Partial.UsaHH(BeTransReEnc.IdTipoTransaccion,
                                                                   lConnection,
                                                                   lTransaction)

            '#CKFK20240214 Agregué la condición de insertar el stock cuando no usaHH y el tipo de transacción es distinta de MCOC00
            If Not lUsaHH AndAlso BeTransReEnc.IdTipoTransaccion <> clsBeTrans_re_enc.pTipoTrans.MCOC00.ToString Then

                If clsLnStock.Insertar_Stock_Recepcion(BeTransReEnc.Detalle,
                                                       pIdUsuario,
                                                       pIdEmpresa,
                                                       pIdBodega,
                                                       lConnection,
                                                       lTransaction) Then

                    If clsLnI_nav_transacciones_out.Insertar_Ingreso(pIdEmpresa,
                                                                     pIdBodega,
                                                                     BeTransReEnc.Detalle,
                                                                     pIdOrdenCompraEnc,
                                                                     pIdUsuario,
                                                                     BeTransReEnc.PropietarioBodega.IdPropietarioBodega,
                                                                     lConnection,
                                                                     lTransaction) Then

                        Habilitar_Stock_Desde_Detalle_Recepcion = True

                    End If

                End If

            End If

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva opcion de log para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=pIdEmpresa,
                                                 pIdBodega:=pIdBodega,
                                                 pIdUsuarioAgr:=pIdUsuario,
                                                 pIdRecEnc:=pIdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Shared Function Actualizar_Estado_Pedido_Ingreso(ByVal pIdOrdenCompraEnc As Integer,
                                                             ByVal pIdRecepcionEnc As Integer,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction,
                                                             ByVal BackOrder As Boolean) As Integer

        Actualizar_Estado_Pedido_Ingreso = 0

        Try

            Dim BeRecOrdenCompra As New clsBeTrans_re_oc

            BeRecOrdenCompra = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(pIdOrdenCompraEnc,
                                                                                                  pIdRecepcionEnc,
                                                                                                  lConnection,
                                                                                                  lTransaction)

            If BackOrder Then
                Actualizar_Estado_Pedido_Ingreso = clsLnTrans_oc_enc.Actualizar_Estado_BackOrder(pIdOrdenCompraEnc,
                                                                                                 lConnection,
                                                                                                 lTransaction)
            Else
                Actualizar_Estado_Pedido_Ingreso = clsLnTrans_oc_enc.Actualizar_Estado_Cerrada(pIdOrdenCompraEnc,
                                                                                               lConnection,
                                                                                               lTransaction)
            End If


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Actualizar_Hora_Fin_Recepcion(ByVal pIdOrdenCompraEnc As Integer,
                                                          ByVal pIdRecepcionEnc As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As Integer

        Actualizar_Hora_Fin_Recepcion = 0

        Try

            Dim BeTransReOC As New clsBeTrans_re_oc
            BeTransReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(pIdOrdenCompraEnc,
                                                                                              pIdRecepcionEnc,
                                                                                              lConnection,
                                                                                              lTransaction)

            If Not BeTransReOC Is Nothing Then
                BeTransReOC.Hora_fin_hh = Now
                Actualizar_Hora_Fin_Recepcion = clsLnTrans_re_oc.Actualizar_Hora_Fin_Recepcion(BeTransReOC, lConnection, lTransaction)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Finalizar_Recepcion_Parcial(ByVal pRecEnc As clsBeTrans_re_enc,
                                                  ByVal pIdOrdenCompraEnc As Integer,
                                                  ByVal pIdRecepcionEnc As Integer,
                                                  ByVal pIdEmpresa As Integer,
                                                  ByVal pIdBodega As Integer,
                                                  ByVal pIdUsuario As String,
                                                  ByVal pBeStockRec As clsBeStock_rec)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeTMSTicket As New clsBeTms_ticket

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pBeStockRec IsNot Nothing Then

                If Reglas_De_Recepcion_Permiten_Ingreso(pRecEnc,
                                                        lConnection,
                                                        lTransaction) Then

                    If pBeStockRec.Uds_lic_plate = 0 Then
                        Throw New Exception("La cantidad de unidades por LP es 0")
                    End If

                    Dim lMaxS As Integer = clsLnStock.MaxID(lConnection,
                                                            lTransaction)

                    If pBeStockRec.Activo = False Then 'Bandera para saber si el pallet ya fue recibido (en su totalidad).

                        If pBeStockRec.No_bulto <> 0 Then 'Se modificaron propiedades del pallet en la HH (Estado o Cantidad menor)

                            If pBeStockRec.Uds_lic_plate <= pBeStockRec.Cantidad Then

                                Dim lMaxNoBulto As Integer = clsLnStock_rec.MaxNB(lConnection, lTransaction) + 1
                                Dim lMaxIdStockRec As Integer = clsLnStock_rec.MaxID(lConnection, lTransaction) + 1
                                pBeStockRec.IdBodega = pIdBodega
                                pBeStockRec.IdStockRec = lMaxIdStockRec
                                pBeStockRec.Fecha_Ingreso = Now
                                clsLnStock_rec.Insertar(pBeStockRec, lConnection, lTransaction)

                            Else
                                Throw New Exception("Cantidad a recibir excede cantidad permitida por pallet")
                            End If

                        End If

                        Dim Obj As New clsBeStock()

                        clsPublic.CopyObject(pBeStockRec, Obj)

                        lMaxS += 1

                        Obj.IdStock = lMaxS

                        clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                              pIdBodega,
                                                                              pIdUsuario,
                                                                              pBeStockRec,
                                                                              lConnection,
                                                                              lTransaction)
                        '#EJC20191218: IdBodega2Stock
                        clsLnStock.Insertar(Obj,
                                            lConnection,
                                            lTransaction)

                        clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                lMaxS,
                                                                                lConnection,
                                                                                lTransaction)

                        clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                           lMaxS,
                                                                           lConnection,
                                                                           lTransaction)

                        'Actualiza cantidad recibida OC.
                        If pRecEnc.IdTipoTransaccion = clsBeTrans_re_enc.pTipoTrans.PICH000.ToString() AndAlso pIdOrdenCompraEnc <> 0 Then 'Si no es pre-ingreso, actualizar cantidad_recibida en O.C.

                            Dim pRecDet As New clsBeTrans_re_det
                            pRecDet.IdProductoBodega = pBeStockRec.IdProductoBodega
                            pRecDet.IdPresentacion = pBeStockRec.IdPresentacion

                            '#EJC20190624: Guardar el lic_plate en trans_re_det
                            pRecDet.Lic_plate = pBeStockRec.Lic_plate
                            pRecDet.Uds_lic_plate = pBeStockRec.Uds_lic_plate

                            If pBeStockRec.Lic_plate <> "" Then
                                pRecDet.Presentacion.EsPallet = True
                            End If

                            pRecDet.No_Linea = pBeStockRec.No_linea
                            pRecDet.cantidad_recibida = 1

                            clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC_Pallet(pIdOrdenCompraEnc,
                                                                                    pRecDet,
                                                                                    lConnection, lTransaction)

                        End If

                        pBeStockRec.Regularizado = True
                        pBeStockRec.Fecha_regularizacion = Now
                        pBeStockRec.Activo = True

                    Else
                        Throw New Exception("El pallet ya fué recibido, no se procesará")
                    End If

                    clsLnStock_rec.Actualiza_Stock_Rec(pBeStockRec,
                                                       lConnection,
                                                       lTransaction)

                Else
                    Throw New Exception("Infracción de reglas de ingreso")
                End If

            Else
                Throw New Exception("No se obtuvieron valores para Stock_Rec: " & pBeStockRec.Lic_plate & " Lenght: " & pBeStockRec.Lic_plate.Length & "LP Válido?")
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Sub Finalizar_Recepcion_Parcial_Pallet_Proveedor(ByVal pRecEnc As clsBeTrans_re_enc,
                                                                   ByVal pIdOrdenCompraEnc As Integer,
                                                                   ByVal pIdRecepcionEnc As Integer,
                                                                   ByVal pIdEmpresa As Integer,
                                                                   ByVal pIdBodega As Integer,
                                                                   ByVal pIdUsuario As String,
                                                                   ByVal pBeStockRec As clsBeStock_rec,
                                                                   ByVal pBeTransReDet As clsBeTrans_re_det,
                                                                   ByVal pBeINavBarraPallet As clsBeI_nav_barras_pallet,
                                                                   ByVal pEsTransferencia As Boolean)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lTransOcDet As New List(Of clsBeTrans_oc_det)
        Dim BePresentacion As New clsBeProducto_Presentacion
        Dim BeTransOcDet As New clsBeTrans_oc_det

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pBeStockRec IsNot Nothing Then

                '#GT30012025: abusando del log, dejo constancia de la barra recibida
                '#MECR23092025: se agrego nueva opcion de bitacora de logs en recepciones.
                Dim Pallet = "Aviso_30012025: HH_RecepcionPallet: " & pBeINavBarraPallet.Codigo_barra
                clsLnLog_error_wms_rec.Agregar_Error(Pallet,
                                                     pIdEmpresa:=pIdEmpresa,
                                                     pIdBodega:=pIdBodega,
                                                     pIdUsuarioAgr:=pIdUsuario,
                                                     pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                     pConection:=lConnection,
                                                     pTransaction:=lTransaction)


                If Reglas_De_Recepcion_Permiten_Ingreso(pRecEnc,
                                                        lConnection,
                                                        lTransaction) Then

                    If pBeStockRec.Uds_lic_plate = 0 Then
                        Throw New Exception("La cantidad de unidades por LP es 0")
                    End If

                    Dim lMaxS As Integer = clsLnStock.MaxID(lConnection,
                                                            lTransaction)

                    If Not pBeStockRec.Activo Then 'Bandera para saber si el pallet ya fue recibido (en su totalidad).

                        If pBeStockRec.Uds_lic_plate <= pBeStockRec.Cantidad Then

                            '#EJC20250603: Agregué esto para inferir el núnero de línea correcto
                            'porque en Idealsa se pueden recibir pallets con diferente licencia 
                            'asociados al mismo producto pero que vienen en diferente línea y la HH
                            'no sabe cuál es la línea correcta.
                            lTransOcDet = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, lConnection, lTransaction)

                            If Not lTransOcDet Is Nothing Then

                                If lTransOcDet.Count > 0 Then

                                    Dim vCantidadMatch As Double = pBeStockRec.Cantidad

                                    If pBeStockRec.IdPresentacion > 0 Then

                                        BePresentacion = clsLnProducto_presentacion.GetSingle(pBeStockRec.IdPresentacion, lConnection, lTransaction)
                                        If Not BePresentacion Is Nothing Then
                                            vCantidadMatch = Math.Round(pBeStockRec.Cantidad / BePresentacion.Factor, 6)
                                        End If

                                    End If

                                    BeTransOcDet = lTransOcDet.Where(Function(x) x.IdProductoBodega = pBeStockRec.IdProductoBodega _
                                                                     AndAlso x.IdPresentacion = pBeStockRec.IdPresentacion _
                                                                     AndAlso x.IdUnidadMedidaBasica = pBeStockRec.IdUnidadMedida _
                                                                     AndAlso x.Cantidad = vCantidadMatch).FirstOrDefault()

                                    If Not BeTransOcDet Is Nothing Then
                                        pBeStockRec.No_linea = BeTransOcDet.No_Linea
                                        pBeTransReDet.No_Linea = BeTransOcDet.No_Linea
                                    End If

                                End If

                            End If

                            Dim lMaxNoBulto As Integer = clsLnStock_rec.MaxNB(lConnection, lTransaction) + 1
                            Dim lMaxIdStockRec As Integer = clsLnStock_rec.MaxID(lConnection, lTransaction) + 1
                            pBeStockRec.IdBodega = pIdBodega
                            pBeStockRec.IdStockRec = lMaxIdStockRec
                            pBeStockRec.Fecha_Ingreso = Now

                            '#EJC20190624: Actualizar lic_plate en trans_re_det
                            pBeTransReDet.Lic_plate = pBeStockRec.Lic_plate
                            pBeTransReDet.Uds_lic_plate = pBeStockRec.Uds_lic_plate

                            If pBeStockRec.Lic_plate <> "" Then
                                pBeTransReDet.Presentacion.EsPallet = True
                                pBeTransReDet.Presentacion.CamasPorTarima = pBeINavBarraPallet.Camas_Por_Tarima
                                pBeTransReDet.Presentacion.CajasPorCama = pBeINavBarraPallet.Cajas_Por_Cama
                            End If

                            Dim FilaAfectada = clsLnTrans_re_det.Guarda_Trans_re_det(pBeTransReDet, lConnection, lTransaction)

                            '#GT30012025: se debe insertar en cada tabla, no hacerlo conlleva a inconsistencias
                            '#MECR23092025: se agrego nueva opcion de bitacora de logs en recepciones.
                            If FilaAfectada = 0 Then
                                Dim mensajeError = "ERROR_30012025: Por una razón desconocida no se registro la recepción de la pallet " & pBeINavBarraPallet.Codigo_barra & " licencia " & pBeTransReDet.Lic_plate
                                clsLnLog_error_wms_rec.Agregar_Error(mensajeError,
                                                                     pIdEmpresa:=pIdEmpresa,
                                                                     pIdBodega:=pIdBodega,
                                                                     pIdUsuarioAgr:=pIdUsuario,
                                                                     pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                                     pConection:=lConnection,
                                                                     pTransaction:=lTransaction)

                                Throw New Exception("ERROR_30012025: Por una razón desconocida no se insertola recepción de la pallet " & pBeINavBarraPallet.Codigo_barra)
                            End If


                            pBeStockRec.IdRecepcionDet = pBeTransReDet.IdRecepcionDet

                            Dim pFilaAfectada = clsLnStock_rec.Insertar(pBeStockRec, lConnection, lTransaction)
                            '#GT30012025: se debe insertar en cada tabla, no hacerlo conlleva a inconsistencias
                            '#MECR23092025: Se agrego nueva opcion de bitacora de logs en recepciones.
                            If pFilaAfectada = 0 Then
                                Dim mensajeError = "ERROR_30012025: Por una razón desconocida no se registro el stock recibido de la pallet " & pBeINavBarraPallet.Codigo_barra & " licencia " & pBeStockRec.Lic_plate
                                'clsLnLog_error_wms_rec.Agregar_Error(mensajeError, pIdEmpresa, pIdBodega, pIdUsuario, pIdRecEnc:=pIdRecepcionEnc)
                                clsLnLog_error_wms_rec.Agregar_Error(mensajeError,
                                                                     pIdEmpresa:=pIdEmpresa,
                                                                     pIdBodega:=pIdBodega,
                                                                     pIdUsuarioAgr:=pIdUsuario,
                                                                     pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                                     pConection:=lConnection,
                                                                     pTransaction:=lTransaction)

                                Throw New Exception("ERROR_30012025: Por una razón desconocida no se insertoel stock recibido de la pallet " & pBeINavBarraPallet.Codigo_barra)
                            End If

                            '#EJC20190329_0538PM: Marcar el pallet como recibido.
                            If pBeStockRec.Lic_plate <> "" Then
                                pBeINavBarraPallet.Recibido = True
                                pBeINavBarraPallet.IdRecepcion = pIdRecepcionEnc
                                pBeINavBarraPallet.Codigo_barra = pBeStockRec.Lic_plate
                                pBeINavBarraPallet.Fecha_Ingreso = Now
                                pBeINavBarraPallet.Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega, lConnection, lTransaction)

                                Dim BeOCEnc As New clsBeTrans_oc_enc
                                BeOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, lConnection, lTransaction)

                                If BeOCEnc IsNot Nothing Then
                                    pBeINavBarraPallet.Bodega_Origen = BeOCEnc.ProveedorBodega.Proveedor.Codigo
                                Else
                                    pBeINavBarraPallet.Bodega_Origen = ""
                                End If

                                Dim pPalletActualizado = clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet(pBeINavBarraPallet, lConnection, lTransaction)
                                If pPalletActualizado = 0 Then
                                    '#MECR23092025: Se agrego nueva opcion de bitacora de logs en recepciones.
                                    Dim mensajeError = "ERROR_30012025: Por una razón desconocida no se actualizó la pallet " & pBeINavBarraPallet.Codigo_barra & " licencia " & pBeStockRec.Lic_plate
                                    'clsLnLog_error_wms_rec.Agregar_Error(mensajeError, pIdEmpresa, pIdBodega, pIdUsuario, pIdRecEnc:=pIdRecepcionEnc)
                                    clsLnLog_error_wms_rec.Agregar_Error(mensajeError,
                                                                         pIdEmpresa:=pIdEmpresa,
                                                                         pIdBodega:=pIdBodega,
                                                                         pIdUsuarioAgr:=pIdUsuario,
                                                                         pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                                         pConection:=lConnection,
                                                                         pTransaction:=lTransaction)

                                    Throw New Exception("ERROR_30012025: Por una razón desconocida no se actualizó la pallet " & pBeINavBarraPallet.Codigo_barra)
                                End If

                            End If

                        Else
                            Throw New Exception("Cantidad a recibir excede cantidad permitida por pallet")
                        End If

                        Dim BeStock As New clsBeStock()

                        clsPublic.CopyObject(pBeStockRec, BeStock)

                        lMaxS += 1

                        BeStock.IdStock = lMaxS

                        Dim BeOperadorBodega As New clsBeOperador_bodega
                        BeOperadorBodega.IdOperador = pIdUsuario
                        BeOperadorBodega.IdBodega = pIdBodega

                        clsLnOperador_bodega.Get_OperadorBodega_By_IdOperador_By_Bodega(BeOperadorBodega, lConnection, lTransaction)

                        Dim FilasAfectadas = clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                                                  pIdBodega,
                                                                                                  pIdUsuario,
                                                                                                  pBeStockRec,
                                                                                                  lConnection,
                                                                                                  lTransaction,
                                                                                                  BeOperadorBodega.IdOperadorBodega)


                        '#GT30012025: se debe insertar en cada tabla, no hacerlo conlleva a inconsistencias
                        If FilasAfectadas = 0 Then
                            '#MECR23092025: Se agrego nueva opcion de bitacora de logs en recepciones.
                            Dim mensajeError = "ERROR_30012025: Por una razón desconocida no se inserto el movimiento de la pallet recibida" & pBeINavBarraPallet.Codigo_barra
                            'clsLnLog_error_wms_rec.Agregar_Error(mensajeError, pIdEmpresa, pIdBodega, pIdUsuario, pIdRecEnc:=pIdRecepcionEnc)
                            clsLnLog_error_wms_rec.Agregar_Error(mensajeError,
                                                                 pIdEmpresa:=pIdEmpresa,
                                                                 pIdBodega:=pIdBodega,
                                                                 pIdUsuarioAgr:=pIdUsuario,
                                                                 pIdRecEnc:=pIdRecepcionEnc,
                                                                 pConection:=lConnection,
                                                                 pTransaction:=lTransaction)

                            Throw New Exception("ERROR_30012025: Por una razón desconocida no se inserto el movimiento de la pallet recibida" & pBeINavBarraPallet.Codigo_barra)
                        End If

                        '#EJC20191218: IdBodega2Stock
                        clsLnStock.Insertar(BeStock,
                                            lConnection,
                                            lTransaction)

                        clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                lMaxS,
                                                                                lConnection,
                                                                                lTransaction)

                        clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                           lMaxS,
                                                                           lConnection,
                                                                           lTransaction)

                        'Actualiza cantidad recibida OC.
                        If pRecEnc.IdTipoTransaccion = clsBeTrans_re_enc.pTipoTrans.PICH000.ToString() _
                            OrElse pRecEnc.IdTipoTransaccion = clsBeTrans_re_enc.pTipoTrans.HCOC00.ToString() _
                            AndAlso pIdOrdenCompraEnc <> 0 Then 'Si no es pre-ingreso, actualizar cantidad_recibida en O.C.


                            '#GT30012025: sino actualizó pasó algo
                            If Not clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC_Pallet(pIdOrdenCompraEnc,
                                                                                           pBeTransReDet,
                                                                                           lConnection, lTransaction) Then

                                '#MECR23092025: Se agrego nueva opcion de bitacora de logs en recepciones.
                                Dim mensajeError = "ERROR_30012025: Por una razón desconocida no se actualizó la cantidad en oc_det de la pallet recibida " & pBeINavBarraPallet.Codigo_barra
                                'clsLnLog_error_wms_rec.Agregar_Error(mensajeError, pIdEmpresa, pIdBodega, pIdUsuario, pIdRecEnc:=pIdRecepcionEnc)
                                clsLnLog_error_wms_rec.Agregar_Error(mensajeError,
                                                                     pIdEmpresa:=pIdEmpresa,
                                                                     pIdBodega:=pIdBodega,
                                                                     pIdUsuarioAgr:=pIdUsuario,
                                                                     pIdRecEnc:=pIdRecepcionEnc,
                                                                     pConection:=lConnection,
                                                                     pTransaction:=lTransaction)

                                Throw New Exception("ERROR_30012025: Por una razón desconocida no se actualizó la cantidad en oc_det de la pallet recibida " & pBeINavBarraPallet.Codigo_barra)
                            End If

                        End If

                        pBeStockRec.Regularizado = True
                        pBeStockRec.Fecha_regularizacion = Now
                        pBeStockRec.Activo = True

                    Else
                        Throw New Exception("El pallet ya fué recibido, no se procesará")
                    End If

                    clsLnStock_rec.Actualiza_Stock_Rec(pBeStockRec, lConnection, lTransaction)

                    pBeTransReDet.Lic_plate = pBeStockRec.Lic_plate
                    pBeTransReDet.Uds_lic_plate = pBeStockRec.Uds_lic_plate

                    If pBeStockRec.Lic_plate <> "" Then
                        pBeTransReDet.Presentacion.EsPallet = True
                        pBeTransReDet.Presentacion.CamasPorTarima = pBeINavBarraPallet.Camas_Por_Tarima
                        pBeTransReDet.Presentacion.CajasPorCama = pBeINavBarraPallet.Cajas_Por_Cama
                    End If

                    '#EJC202208031122: Prevenir que el IdPropietarioBodega e IdPropietario queden en 0 en I_NAV_TRANSACCIONES_OUT
                    If pBeTransReDet.IdPropietarioBodega = 0 Then
                        pBeTransReDet.IdPropietarioBodega = pBeStockRec.IdPropietarioBodega
                    End If

                    Dim IdTipoDocumento As Integer = 0
                    IdTipoDocumento = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                lConnection,
                                                                                                lTransaction)

                    clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                          pIdBodega,
                                                                          IdTipoDocumento,
                                                                          pBeTransReDet,
                                                                          pIdOrdenCompraEnc,
                                                                          pIdUsuario,
                                                                          False,
                                                                          lConnection,
                                                                          lTransaction)

                    Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                    BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                    BeLoteNum.IdProductoBodega = pBeStockRec.IdProductoBodega
                    BeLoteNum.IdRecepcionEnc = pIdRecepcionEnc
                    BeLoteNum.Codigo = pBeINavBarraPallet.Codigo
                    BeLoteNum.Lote = pBeINavBarraPallet.Lote
                    BeLoteNum.Lote_Numerico = pBeINavBarraPallet.Lote_Numerico
                    BeLoteNum.Cantidad = pBeStockRec.Cantidad
                    BeLoteNum.FechaIngreso = Now
                    clsLnTrans_re_det_lote_num.Insertar(BeLoteNum, lConnection, lTransaction)

                    If pEsTransferencia Then

                        Dim lBeStockEnTransito As New List(Of clsBeStock_transito)
                        Dim lBeStockTransitoFilter As New List(Of clsBeStock_transito)
                        Dim BeStockEnTransito As New clsBeStock_transito
                        Dim IdDespachoEnc As Integer = 0

                        Dim BeOCEnc As New clsBeTrans_oc_enc
                        BeOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, lConnection, lTransaction)

                        If BeOCEnc IsNot Nothing Then
                            IdDespachoEnc = BeOCEnc.IdDespachoEnc
                        End If

                        lBeStockEnTransito = clsLnStock_transito.Get_All_By_IdOrdenCompraEnc_And_IdRecepcionEnc(pIdOrdenCompraEnc,
                                                                                                                pIdRecepcionEnc,
                                                                                                                lConnection,
                                                                                                                lTransaction)

                        If Not lBeStockEnTransito Is Nothing Then

                            lBeStockTransitoFilter = lBeStockEnTransito.FindAll(Function(x) x.IdProductoBodegaDestino = pBeStockRec.IdProductoBodega _
                                                                                AndAlso x.IdUnidadMedida = pBeStockRec.IdUnidadMedida _
                                                                                AndAlso x.Lote = pBeStockRec.Lote _
                                                                                AndAlso (x.Lic_Plate = pBeStockRec.Lic_plate) _
                                                                                AndAlso (x.IdPresentacion = pBeStockRec.IdPresentacion) _
                                                                                AndAlso x.IdProductoEstado = pBeStockRec.IdProductoEstado _
                                                                                AndAlso x.Fecha_Vence = pBeStockRec.Fecha_vence _
                                                                                AndAlso x.IdDespachoEnc = IdDespachoEnc)

                            If Not lBeStockTransitoFilter Is Nothing Then

                                If lBeStockTransitoFilter.Count = 1 Then

                                    Dim BeStockTransito As New clsBeStock_transito
                                    BeStockTransito = lBeStockTransitoFilter(0)

                                    Dim BeStockBodegaOrigen As New clsBeStock
                                    BeStockBodegaOrigen.IdStock = BeStockTransito.IdStock
                                    BeStockBodegaOrigen = clsLnStock.GetSingle(BeStockBodegaOrigen.IdStock, lConnection, lTransaction)

                                    If Not BeStockBodegaOrigen Is Nothing Then

                                        If BeStockBodegaOrigen.Cantidad = pBeStockRec.Cantidad Then
                                            '#EJC20190719: La cantidad origen coincide con la recibida.
                                            clsLnStock.Eliminar_By_IdStock(BeStockBodegaOrigen.IdStock, lConnection, lTransaction)

                                            BeStockTransito.Cantidad_Recibida = pBeStockRec.Cantidad
                                            clsLnStock_transito.Actualizar(BeStockTransito, lConnection, lTransaction)

                                        ElseIf pBeStockRec.Cantidad < BeStockBodegaOrigen.Cantidad Then

                                            '#EJC20190719: La cantidad recibida es menor que la cantidad enviada.
                                            BeStockBodegaOrigen.Cantidad -= pBeStockRec.Cantidad
                                            clsLnStock.Actualiza_Cantidad_Y_Peso(BeStockBodegaOrigen, lConnection, lTransaction)

                                            BeStockTransito.Cantidad_Recibida = pBeStockRec.Cantidad
                                            clsLnStock_transito.Actualizar(BeStockTransito, lConnection, lTransaction)

                                        End If

                                    Else
                                        Throw New Exception("Error 20190713_1546: No se pudo obtener el IdStock de la bodega origen para stock en tránsito.")
                                    End If

                                Else
                                    Throw New Exception("Error 20190719_1531: Se obtuvo más de un registro para la actualización del stock en tránsito, condición no controlada.")
                                End If

                            Else
                                Throw New Exception("Error 20190713_1543: No se pudo obtener el registro de stock en tránsito de la bodega origen.")
                            End If

                        End If

                    End If

                Else
                    Throw New Exception("Infracción de reglas de ingreso")
                End If

            Else
                Throw New Exception("No se obtuvieron valores para Stock_Rec: " & pBeStockRec.Lic_plate & " Lenght: " & pBeStockRec.Lic_plate.Length & "LP Válido?")
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    ''' <summary>
    ''' #EJC20221011: Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet 
    ''' #EJC20221011: WMS - Corrección de transacción y conexión en función Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet 
    ''' </summary>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <param name="pIdRecepcionDet"></param>
    ''' <param name="pConection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal pIdRecepcionEnc As Integer,
                                                                                             ByVal pIdRecepcionDet As Integer,
                                                                                             Optional ByVal pConection As SqlConnection = Nothing,
                                                                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Double

        Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet = 0

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Dim lCommand As New SqlCommand
            Dim lReturnValue As Object


            Dim vSQL As String = "SELECT ISNULL(SUM(det.cantidad_recibida),0) as cant FROM Trans_re_det AS det
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc AND IdRecepcionDet=@IdRecepcioDet "

            lCommand.CommandType = CommandType.Text
            lCommand.CommandText = vSQL

            lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
            lCommand.Parameters.AddWithValue("@IdRecepcioDet", pIdRecepcionDet)

            If Es_Transaccion_Remota Then
                lCommand.Connection = pConection
                lCommand.Transaction = pTransaction
                lReturnValue = lCommand.ExecuteScalar()
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lCommand.Connection = lConnection
                lCommand.Transaction = lTransaction
                lReturnValue = lCommand.ExecuteScalar()
            End If

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet = lReturnValue
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then
                If Not lTransaction Is Nothing Then
                    lTransaction.Rollback()
                End If
            End If
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                If Not lConnection Is Nothing Then
                    If lConnection.State = ConnectionState.Open Then
                        lConnection.Close()
                    End If
                End If
            End If
        End Try

    End Function

    ''' <summary>
    ''' Creada por Carlos Manuel
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_IdOrdenRecEnc(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_re_enc)

        Get_All_By_IdOrdenRecEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_re_enc)

            Dim vSQL As String = "SELECT * FROM VW_Recepcion WHERE NoDocIngreso=@NoDocIngreso and Activo=1"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@NoDocIngreso", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_enc

                                If lRow("Código") IsNot DBNull.Value AndAlso lRow("Código") IsNot Nothing Then
                                    Obj.IdRecepcionEnc = CType(lRow("Código"), Integer)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.PropietarioOC = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Proveedor") IsNot DBNull.Value AndAlso lRow("Proveedor") IsNot Nothing Then
                                    Obj.Proveedor = CType(lRow("Proveedor"), String)
                                End If

                                If lRow("Bodega") IsNot DBNull.Value AndAlso lRow("Bodega") IsNot Nothing Then
                                    Obj.Bodega = CType(lRow("Bodega"), String)
                                End If

                                If lRow("NoDocIngreso") IsNot DBNull.Value AndAlso lRow("NoDocIngreso") IsNot Nothing Then
                                    Obj.NoOrdencompra = CType(lRow("NoDocIngreso"), Integer)
                                End If

                                If lRow("Referencia_DI") IsNot DBNull.Value AndAlso lRow("Referencia_DI") IsNot Nothing Then
                                    Obj.NoDocumentoOC = CType(lRow("Referencia_DI"), String)
                                End If

                                If lRow("Fecha") IsNot DBNull.Value AndAlso lRow("Fecha") IsNot Nothing Then
                                    Obj.Fecha_recepcion = CType(lRow("Fecha"), Date)
                                End If

                                If lRow("estado") IsNot DBNull.Value AndAlso lRow("estado") IsNot Nothing Then
                                    Obj.Estado = CType(lRow("estado"), String)
                                End If

                                If lRow("TipoTrans") IsNot DBNull.Value AndAlso lRow("TipoTrans") IsNot Nothing Then
                                    Obj.IdTipoTransaccion = CType(lRow("TipoTrans"), String)
                                End If

                                If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                                    Obj.Descripcion = CType(lRow("Descripcion"), String)
                                End If

                                If lRow("Muelle") IsNot DBNull.Value AndAlso lRow("Muelle") IsNot Nothing Then
                                    Obj.Muelle = New clsBeBodega_muelles
                                    Obj.Muelle.Nombre = CType(lRow("Muelle"), String)
                                    Obj.MuelleRec = CType(lRow("Muelle"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            Return lReturnList

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

    Public Shared Function Get_Reporte_ConOC(ByVal IdRecepcionEnc As Integer) As DataTable

        Get_Reporte_ConOC = Nothing

        Try

            Dim lTable As New DataTable("Result")
            Dim vSQL As String = "SELECT * FROM VW_REC_CON_OC WHERE IdRecepcionEnc = @IdRecepcionEnc  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)
                        lDataAdapter.Fill(lTable)
                        Get_Reporte_ConOC = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Con_OC_Consolidada(ByVal IdRecepcionEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT t.*, firma_piloto as firma_piloto from 
                    (select IdPropietarioBodega, IdRecepcionEnc,
                    Propietario,CONVERT(date,fecha_recepcion, 112) as fecha_recepcion,
                    hora_ini_pc,hora_fin_pc,TipoTrans,No_Linea,
                    codigo,codigo_barra,producto,Sum(CantidadRecibida) as CantidadRecibida,CONVERT (date,fecha_ingreso, 112) as fecha_ingreso,
                    lote,fecha_vence,Presentacion,EstadoRec,Unidad_Medida,IdOrdenCompraEnc,
                    IdRecepcionOc,no_docto,Id_Proveedor,Proveedor,IdProductoBodega,IdProveedorBodega,cantidad,Referencia,
                    NombrePiloto,placa,marca,Operador,EstadoProducto,No_Marchamo
                    FROM VW_REC_CON_OC where IdRecepcionEnc = @IdRecepcionEnc
                    group by IdPropietarioBodega,Propietario,
                    hora_ini_pc,hora_fin_pc,TipoTrans,No_Linea,
                    codigo,codigo_barra,producto,CONVERT (date,fecha_ingreso, 112),
                    lote,fecha_vence,Presentacion,EstadoRec,Unidad_Medida,IdOrdenCompraEnc,
                    IdRecepcionOc,no_docto,Id_Proveedor,Proveedor,IdProductoBodega,IdProveedorBodega,cantidad,Referencia,
                    NombrePiloto,placa,marca,Operador,No_Marchamo,CONVERT (date,fecha_recepcion, 112),EstadoProducto,IdRecepcionEnc) as T
                    Left join trans_re_enc tr
                    on tr.IdRecepcionEnc = t.IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_SinOC(ByVal IdRecepcionEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_REC_SIN_OC WHERE IdRecepcionEnc = @IdRecepcionEnc  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = "SELECT ISNULL(Max(IdRecepcionEnc),0) FROM trans_re_enc "

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Generar_Tarea_Recepcion_By_OrdenCompraEnc(ByRef BeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                                     ByRef Resultado As String,
                                                                     ByVal CrearRecepcionPorDefecto As Boolean,
                                                                     ByVal BeMI3Config As clsBeI_nav_config_enc,
                                                                     ByRef OutBeRecepcionEnc As clsBeTrans_re_enc,
                                                                     ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As Integer


        Generar_Tarea_Recepcion_By_OrdenCompraEnc = 0 : OutBeRecepcionEnc = Nothing

        Dim RegistrosAfectados As Integer = 0
        Dim lblResult As New RichTextBox
        Dim OrdenCompraReOc As New clsBeTrans_re_oc
        Dim BeTareaHH As New clsBeTarea_hh
        Dim BeRecepcionEnc As New clsBeTrans_re_enc
        Dim IdBodegaDestino As Integer = 0
        Dim IdPropietario As Integer = 0
        Dim TiempoMedioIngresoMinutos As Double

        Try

            If Not clsLnTrans_re_oc.Existe_Documento_By_IdOrdenCompraEnc(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction) Then

                IdBodegaDestino = BeOrdenCompraEnc.IdBodega

                BeRecepcionEnc.IsNew = True
                BeRecepcionEnc.IdRecepcionEnc = MaxID(lConnection, lTransaction) + 1
                BeRecepcionEnc.PropietarioBodega = New clsBePropietario_bodega
                BeRecepcionEnc.PropietarioBodega.IdBodega = BeOrdenCompraEnc.IdBodega
                '#EJC20210715: IdBodega se comparte a la tarea de recepción en generación automática de tarea..
                BeRecepcionEnc.IdBodega = BeOrdenCompraEnc.IdBodega
                BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega
                BeRecepcionEnc.User_agr = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fec_agr = Now
                BeRecepcionEnc.Activo = True
                BeRecepcionEnc.Estado = "Nuevo"

                BeRecepcionEnc.OrdenCompraRec = New clsBeTrans_re_oc
                BeRecepcionEnc.OrdenCompraRec.IsNew = True
                '#EJC20200120: Lo hace más tarde en el guardar...
                'BeRecepcionEnc.OrdenCompraRec.IdRecepcionOc = clsLnTrans_re_oc.MaxID(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction) + 1
                BeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc

                If BeRecepcionEnc.PropietarioBodega Is Nothing OrElse BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega <= 0 Then
                    Throw New Exception("Propietario no válido al crear la recepción")
                End If

                'Ingreso con referenci a orden de compra para procesar en HH
                BeRecepcionEnc.IdTipoTransaccion = "HCOC00"

                BeRecepcionEnc.IdMuelle = clsLnBodega_muelles.Get_IdMuelle_Default_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdMuelle = 0 Then
                    Throw New Exception("No existe ningún muelle por defecto para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                BeRecepcionEnc.IdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdUbicacionRecepcion = 0 Then
                    Throw New Exception("No está configurada la ubicación por defecto para recepción para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                TiempoMedioIngresoMinutos = clsLnTarea_hh.Get_Tiempo_Medio_Tarea_Ingreso_Minutos(lConnection, lTransaction)

                BeRecepcionEnc.Fecha_recepcion = Now.Date
                BeRecepcionEnc.Hora_ini_pc = Now
                BeRecepcionEnc.Hora_fin_pc = Now.AddMinutes(TiempoMedioIngresoMinutos)
                BeRecepcionEnc.Muestra_precio = False
                BeRecepcionEnc.Fec_mod = Now
                BeRecepcionEnc.User_mod = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fecha_tarea = Now
                BeRecepcionEnc.Tomar_fotos = False
                BeRecepcionEnc.Escanear_rec_ubic = False
                BeRecepcionEnc.Para_por_codigo = False
                BeRecepcionEnc.Observacion = "FROMMI3"
                BeRecepcionEnc.IdPiloto = 0
                BeRecepcionEnc.IdVehiculo = 0
                BeRecepcionEnc.Habilitar_Stock = True
                BeRecepcionEnc.IdBodega = IdBodegaDestino
                '#CKFK20250516 Agregué el estado por defecto del tipo de documento
                BeOrdenCompraEnc.TipoIngreso = clsLnTrans_oc_ti.GetSingle(BeOrdenCompraEnc.IdTipoIngresoOC)
                If BeOrdenCompraEnc.TipoIngreso IsNot Nothing Then
                    If BeOrdenCompraEnc.TipoIngreso.IdProductoEstado <> 0 Then
                        BeRecepcionEnc.IdEstado_Defecto_Recepcion = BeOrdenCompraEnc.TipoIngreso.IdProductoEstado
                    End If
                Else
                    Dim BeTipoDocumentoIngreso As New clsBeTrans_oc_ti
                    BeTipoDocumentoIngreso = clsLnTrans_oc_ti.GetSingle(BeOrdenCompraEnc.IdTipoIngresoOC)
                    If BeTipoDocumentoIngreso.IdProductoEstado <> 0 Then
                        BeRecepcionEnc.IdEstado_Defecto_Recepcion = BeTipoDocumentoIngreso.IdProductoEstado
                    End If
                End If

                OrdenCompraReOc.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                OrdenCompraReOc.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc
                OrdenCompraReOc.IsNew = True
                OrdenCompraReOc.No_docto = BeOrdenCompraEnc.Referencia
                OrdenCompraReOc.OC = BeOrdenCompraEnc
                OrdenCompraReOc.Recepcion_ciega = False
                OrdenCompraReOc.Recepcion_manual = False
                OrdenCompraReOc.User_agr = BeMI3Config.IdUsuario

                BeRecepcionEnc.OrdenCompraRec = OrdenCompraReOc


                Dim pListOperadorRecepcion As New List(Of clsBeTrans_re_op)
                Dim pListOperadorBodega As New List(Of clsBeOperador_bodega)

                '#EJC20190711: Broadcast a todos los operadores de la bodega con la tarea.
                pListOperadorBodega = clsLnOperador_bodega.Get_All_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If Not pListOperadorBodega Is Nothing Then

                    Dim BeTransReOp As New clsBeTrans_re_op()

                    For Each Op In pListOperadorBodega

                        BeTransReOp = New clsBeTrans_re_op()
                        BeTransReOp.IdOperadorBodega = Op.IdOperadorBodega
                        BeTransReOp.User_agr = BeMI3Config.IdUsuario
                        BeTransReOp.Fec_agr = Now
                        BeTransReOp.User_mod = BeMI3Config.IdUsuario
                        BeTransReOp.Fec_mod = Now
                        BeTransReOp.IsNew = True
                        BeTransReOp.UsaHH = True
                        pListOperadorRecepcion.Add(BeTransReOp)

                    Next

                End If

                BeTareaHH = New clsBeTarea_hh
                BeTareaHH.IdPropietario = clsLnPropietarios.Get_IdPropietario(IdBodegaDestino, BeOrdenCompraEnc.IdPropietarioBodega)
                BeTareaHH.IdBodega = IdBodegaDestino
                BeTareaHH.IdMuelle = BeRecepcionEnc.IdMuelle
                BeTareaHH.IdEstado = 1
                BeTareaHH.IdPrioridad = 1
                BeTareaHH.IdTipoTarea = 1
                BeTareaHH.IdTransaccion = BeRecepcionEnc.IdRecepcionEnc
                BeTareaHH.Tipo = 0
                BeTareaHH.FechaInicio = Now
                BeTareaHH.FechaFin = Now.AddHours(2)
                BeTareaHH.DiaCompleto = False
                BeTareaHH.Descripcion = ""
                BeTareaHH.CreaTarea = True
                BeTareaHH.IsNew = True

                Select Case BeRecepcionEnc.IdTipoTransaccion.ToString()
                    Case "HSOC00"
                        BeTareaHH.Asunto = "Ingreso sin Orden de Compra "
                    Case "HSOD00"
                        BeTareaHH.Asunto = "Ingreso de Devolución sin referencia"
                    Case "HCOC00"
                        BeTareaHH.Asunto = "Ingreso con Orden de Compra"
                    Case "HCOD00"
                        BeTareaHH.Asunto = "Devolución de Pedido"
                    Case "HHSR00"
                        BeTareaHH.Asunto = "Ingreso sin referencia"
                    Case "PICH000"
                        BeTareaHH.Asunto = "Pre-ingreso con HH"
                    Case Else
                        Exit Select
                End Select

                Guardar(BeOrdenCompraEnc.IdBodega,
                        BeTareaHH,
                        BeRecepcionEnc,
                        BeRecepcionEnc.OrdenCompraRec,
                        Nothing,
                        Nothing,
                        pListOperadorRecepcion,
                        Nothing,
                        Nothing,
                        Nothing,
                        Nothing,
                        Nothing,
                        lConnection,
                        lTransaction)

                Generar_Tarea_Recepcion_By_OrdenCompraEnc = 1

                OutBeRecepcionEnc = BeRecepcionEnc

            End If

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva opcion de bitacora de logs de recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdRecEnc:=OutBeRecepcionEnc.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Valida_Lp_By_IdRece_Enc(ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                                   ByVal pConnection As SqlConnection,
                                                   ByVal pTransaction As SqlTransaction) As String

        Valida_Lp_By_IdRece_Enc = ""

        Dim CadernaTexto = ""

        Try

            If Not pListRecDet Is Nothing Then

                For Each BeDet As clsBeTrans_re_det In pListRecDet

                    If BeDet.Lic_plate <> "" Then

                        Dim vSQL As String = "SELECT * FROM trans_re_det 
                                    WHERE lic_plate=@lp and codigo_producto <> @codigo 
                                    AND IdRecepcionEnc=@IdRecepcionEnc "

                        Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Transaction = pTransaction
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", BeDet.IdRecepcionEnc)
                            lDTA.SelectCommand.Parameters.AddWithValue("@lp", BeDet.Lic_plate)
                            lDTA.SelectCommand.Parameters.AddWithValue("@codigo", BeDet.Codigo_Producto)

                            Dim lDT As New DataTable()
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                                CadernaTexto = String.Format("Ya existe Lic_plate: {0} ", BeDet.Lic_plate)

                                Return CadernaTexto

                            End If

                        End Using

                    End If

                Next

            End If

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva bitacora de logs para recepcion.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdRecEnc:=pListRecDet.FirstOrDefault().IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function


    Public Shared Function Guardar_by_Import(ByVal pObjTareaHH As clsBeTarea_hh,
                                           ByVal pRecEnc As clsBeTrans_re_enc,
                                           ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                           ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                           ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                           ByVal pListRecOpe As List(Of clsBeTrans_re_op),
                                           ByVal pListRecFact As List(Of clsBeTrans_re_fact),
                                           ByVal pListRecImg As List(Of clsBeTrans_re_img),
                                           ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                           ByVal pListStockRec As List(Of clsBeStock_rec),
                                           ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                           ByVal IdBodega As Integer,
                                           ByVal No_Ticket_Tms As String) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            '#EJC20171022_0852AM: Refactorizado por mí.
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            ' Recepción Encabezado
            Guarda_Trans_re_enc(pRecEnc, lConnection, lTransaction)

            ' Recepción Orden Compra
            clsLnTrans_re_oc.Guarda_Trans_Re_OC(pRecEnc, pRecOrdenCompra, lConnection, lTransaction)

            ' Recepción Detalle
            clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, True, pRecEnc, lConnection, lTransaction)

            If pRecEnc.IdTipoTransaccion <> clsBeTrans_re_enc.pTipoTrans.PICH000.ToString() Then 'Si no es pre-ingreso, actualizar cantidad_recibida en O.C.
                'Actualiza cantidad recibida OC.
                clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra, pListRecDet, lConnection, lTransaction)
            End If

            'Guarda parámetros de productos.
            clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc, pListRecDet, pListRecDetParam, lConnection, lTransaction)

            'Recepción Operadores
            clsLnTrans_re_op.Guarda_Trans_Re_Op(pRecEnc.IdRecepcionEnc, pListRecOpe, lConnection, lTransaction)

            ' Imagenes
            clsLnTrans_re_img.Guarda_Trans_Re_Img(pRecEnc.IdRecepcionEnc, pListRecImg, lConnection, lTransaction)

            ' Facturas asociadas
            clsLnTrans_re_fact.Guarda_facturas_asoc(pRecEnc.IdRecepcionEnc, pListRecFact, lConnection, lTransaction)

            ' Stock Rec
            clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc, IdBodega, pListStockRec, lConnection, lTransaction)

            ' Producto_pallet
            clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc, pListProductoPallet, lConnection, lTransaction)

            ' Stock Serializado Rec
            clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer, pListStockRec, lConnection, lTransaction)

            'Tarea de recepción para la HH.
            clsLnTarea_hh.Guardar_Tarea_Recepcion_HH(pObjTareaHH, lConnection, lTransaction)

            'Tarea de actualizar el estado de un ticket existente
            clsLnTrans_oc_enc.Cambiar_A_Estado_Procesado(No_Ticket_Tms, lConnection, lTransaction)

            lTransaction.Commit()

            Return pRecEnc.IdRecepcionEnc

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    Public Shared Function Guardar_By_Import_Excel(ByVal pObjTareaHH As clsBeTarea_hh,
                                                    ByVal pRecEnc As clsBeTrans_re_enc,
                                                    ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                                    ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                                    ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                                    ByVal pListRecOpe As List(Of clsBeTrans_re_op),
                                                    ByVal pListRecFact As List(Of clsBeTrans_re_fact),
                                                    ByVal pListRecImg As List(Of clsBeTrans_re_img),
                                                    ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                                    ByVal pListStockRec As List(Of clsBeStock_rec),
                                                    ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                                    ByVal pIdBodega As Integer,
                                                    ByVal pIdEmpresa As Integer,
                                                    ByVal pIdUsuario As Integer,
                                                    ByVal pIdOrdenCompraEnc As Integer,
                                                    ByVal pIdResolucionlp As Integer,
                                                    ByVal No_Ticket_Tms As String,
                                                    ByVal pOC_ENC As clsBeTrans_oc_enc,
                                                    ByVal pObjListaInvInicialExcel As List(Of clsBeTrans_inv_inicial_excel_op_log),
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As Integer

        Try


            ' Recepción Encabezado
            Guarda_Trans_re_enc(pRecEnc, lConnection, lTransaction)

            ' Recepción Orden Compra
            clsLnTrans_re_oc.Guarda_Trans_Re_OC(pRecEnc, pRecOrdenCompra, lConnection, lTransaction)

            ' Recepción Detalle
            clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, True, pRecEnc, lConnection, lTransaction)

            If pRecEnc.IdTipoTransaccion <> clsBeTrans_re_enc.pTipoTrans.PICH000.ToString() Then 'Si no es pre-ingreso, actualizar cantidad_recibida en O.C.
                'Actualiza cantidad recibida OC.
                clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pRecOrdenCompra, pListRecDet, lConnection, lTransaction)
            End If

            'Guarda parámetros de productos.
            clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc, pListRecDet, pListRecDetParam, lConnection, lTransaction)

            'Recepción Operadores
            clsLnTrans_re_op.Guarda_Trans_Re_Op(pRecEnc.IdRecepcionEnc, pListRecOpe, lConnection, lTransaction)

            'clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc, pIdBodega, pListStockRec, lConnection, lTransaction)
            '#GT02052022_0840: guardar fecha_llegada del Excel en el stock y demas tablas
            clsLnStock_rec.Guarda_Stock_Rec_By_Excel(pRecEnc.IdRecepcionEnc, pIdBodega, pListStockRec, lConnection, lTransaction)

            ' Producto_pallet
            'clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc, pListProductoPallet, lConnection, lTransaction)

            ' Stock Serializado Rec
            'clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer, pListStockRec, lConnection, lTransaction)

            'Tarea de recepción para la HH.
            clsLnTarea_hh.Guardar_Tarea_Recepcion_HH(pObjTareaHH, lConnection, lTransaction)

            Dim BeStock As New clsBeStock()

            If pRecEnc.Habilitar_Stock Then

                Dim pBeINavBarraPallet As New clsBeI_nav_barras_pallet
                Dim lMaxS As Integer = clsLnStock.MaxID(lConnection, lTransaction) + 1

                For Each pBeStockRec As clsBeStock_rec In pListStockRec

                    BeStock = New clsBeStock
                    pBeStockRec.IdBodega = pIdBodega

                    '#EJC20200207: Para evitar fechas malas de la HH
                    '#GT01052022_2219: se guarda la fecha_llegada del excel
                    'pBeStockRec.Fecha_Ingreso = Now
                    'pBeStockRec.Fec_agr = Now
                    'pBeStockRec.Fec_mod = Now
                    clsPublic.CopyObject(pBeStockRec, BeStock)
                    BeStock.IdStock = lMaxS

                    clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                          pIdBodega,
                                                                          pIdUsuario,
                                                                          pBeStockRec,
                                                                          lConnection,
                                                                          lTransaction)
                    'CadenaResultado += "Insertar_Movimientos_Recepcion"
                    '#EJC20191218: IdBodega2Stock
                    clsLnStock.Insertar(BeStock, lConnection, lTransaction)

                    '#GT04052022_0952: si trae posiciones, se guarda Stock_det
                    Dim pFilaPosicion = pObjListaInvInicialExcel.Find(Function(x) x.Licencia = BeStock.Lic_plate)
                    Dim vPosiciones As Integer = 0

                    If Not pFilaPosicion Is Nothing Then

                        If pFilaPosicion.Posiciones > 0 Then

                            Dim BeStockDet As New clsBeStock_det()
                            BeStockDet.IdStock = BeStock.IdStock
                            BeStockDet.Posiciones = pFilaPosicion.Posiciones
                            vPosiciones = pFilaPosicion.Posiciones

                            If clsLnStock_det.Get_Single_By_IdStock(BeStockDet, lConnection, lTransaction) Then
                                '#EJC20220505: Porqué ya existe?
                                BeStockDet.Posiciones = vPosiciones
                                clsLnStock_det.Actualizar(BeStockDet, lConnection, lTransaction)
                            Else
                                clsLnStock_det.Insertar(BeStockDet, lConnection, lTransaction)
                            End If

                        End If

                    End If

                    'CadenaResultado += "Inserta_Stock"
                    clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec, lMaxS, lConnection, lTransaction)
                    'CadenaResultado += "Insertar_Stock_Parametro_Recepcion"
                    clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec, lMaxS, lConnection, lTransaction)
                    'CadenaResultado += "Insertar_Stock_Serializado_Recepcion"

                    '#EJC20190329_0538PM: Marcar el pallet como recibido.
                    If pBeStockRec.Lic_plate <> "" Then
                        pBeINavBarraPallet.Recibido = True
                        pBeINavBarraPallet.IdRecepcion = pRecEnc.IdRecepcionEnc
                        pBeINavBarraPallet.Codigo_barra = pBeStockRec.Lic_plate
                        pBeINavBarraPallet.Fecha_Ingreso = Now
                        pBeINavBarraPallet.Fecha_Agregado = Now
                        pBeINavBarraPallet.Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega, lConnection, lTransaction)
                        'CadenaResultado += "Get_Codigo_By_IdBodega"
                        clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet(pBeINavBarraPallet, lConnection, lTransaction)
                        'CadenaResultado += "Actualiza_Estado_Barras_Pallet"
                    End If

                    lMaxS += 1

                Next

                Dim IdTipoDocumento As Integer = 0
                IdTipoDocumento = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, lConnection, lTransaction)

                '#EJC20190607: Insertar stock parcial (no con pallet) en interface.
                For Each pBeTransReDet As clsBeTrans_re_det In pListRecDet

                    If pBeTransReDet.IsNew Then

                        'CadenaResultado += "Inserta transacciones out"

                        Dim vResultado As String = clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                                                         pIdBodega,
                                                                                                         IdTipoDocumento,
                                                                                                         pBeTransReDet,
                                                                                                         pIdOrdenCompraEnc,
                                                                                                         pIdUsuario,
                                                                                                         False,
                                                                                                         lConnection,
                                                                                                         lTransaction)

                        'CadenaResultado += "Insertar_Ingreso_Parcial: " & vResultado

                        Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                        BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                        BeLoteNum.IdProductoBodega = pBeTransReDet.IdProductoBodega
                        BeLoteNum.IdRecepcionEnc = pRecEnc.IdRecepcionEnc
                        BeLoteNum.Codigo = pBeINavBarraPallet.Codigo
                        BeLoteNum.Lote = pBeINavBarraPallet.Lote
                        BeLoteNum.Lote_Numerico = pBeINavBarraPallet.Lote_Numerico
                        BeLoteNum.Cantidad = pBeTransReDet.cantidad_recibida
                        BeLoteNum.FechaIngreso = Now
                        clsLnTrans_re_det_lote_num.Insertar(BeLoteNum, lConnection, lTransaction)

                    End If

                Next

            End If

            'GT19012022: se actualizan los estados para OC Y RE a CERRADO
            pOC_ENC.IdEstadoOC = 4 'Corresponde a cerrado porque el proceso es automatico, y no requiere proceso en la HH
            clsLnTrans_oc_enc.Actualizar(pOC_ENC, lConnection, lTransaction)
            pRecEnc.Estado = "Cerrado" 'Corresponde a cerrado porque el proceso es automatico, y no requiere proceso en la HH
            Actualizar(pRecEnc, lConnection, lTransaction)
            'GT20012022: se cierra el estado de las tareas, porque el proceso es automatico, no requiere proceso en la HH          
            pObjTareaHH.IdEstado = 4
            clsLnTarea_hh.Actualizar(pObjTareaHH, lConnection, lTransaction)

        Catch ex As Exception
            '#MECR23092025: Se agrego nueva opcion de bitacora de logs en recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=pIdEmpresa,
                                                 pIdBodega:=pIdBodega,
                                                 pIdUsuarioAgr:=pIdUsuario,
                                                 pIdRecEnc:=pRecEnc.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220121: Validar si el estado de la recepción es finalizada.
    ''' </summary>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Finalizada(ByVal pIdRecepcionEnc As Integer,
                                      ByVal lConnection As SqlConnection,
                                      ByVal lTransaction As SqlTransaction) As Boolean

        Finalizada = False

        Try

            Dim vSQL As String = "SELECT Estado FROM trans_re_enc WHERE IdRecepcionEnc = @IdRecepcionEnc"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                'Dim lValue = lCommand.ExecuteScalar()
                'Finalizada = (lValue = "Cerrado")

                '#GT23022023: se obtiene la conversión, sea la descrpcion o un null 
                Dim lValue = Convert.ToString(lCommand.ExecuteScalar())
                If lValue = "Cerrado" Then
                    Finalizada = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #CKFK20220308: Validar si la recepción tiene registros pendientes de push.
    ''' </summary>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Registros_Pendientes_Push(ByVal pIdRecepcionEnc As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As Boolean

        Registros_Pendientes_Push = False

        Try

            Try
                Dim pSQL As String = "SELECT *
                                      FROM trans_re_det
                                      WHERE IdRecepcionEnc = @IdRecepcionEnc  AND
                                            lic_plate NOT IN (SELECT l.lic_plate
                                      FROM i_nav_transacciones_push p inner join 
                                           trans_oc_det_lote l on p.IdOrdenCompra = l.IdOrdenCompraEnc and 
                                           l.Ubicacion = p.documento_ubicacion
                                      WHERE IdRecepcionEnc  = @IdRecepcionEnc and l.lic_plate<>'' )"

                Using lDTA As New SqlDataAdapter(pSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim vDocumentoUbicacion As String = ""
                        Dim vRecepcionAlmacen As String = ""
                        Dim vTipoPush As String = "Push_Recepcion_Produccion_To_NAV_For_BYB"
                        Dim IdRecepcionDet, pIdUsuario As Integer
                        Dim BeTransReDet As clsBeTrans_re_det

                        pIdUsuario = 1

                        For Each dr As DataRow In lDT.Rows

                            IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))

                            BeTransReDet = New clsBeTrans_re_det()
                            BeTransReDet.IdRecepcionDet = IdRecepcionDet
                            BeTransReDet.IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                            BeTransReDet.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                            BeTransReDet.No_Linea = IIf(IsDBNull(dr.Item("No_Linea")), 0, dr.Item("No_Linea"))
                            BeTransReDet.Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                            BeTransReDet.Lic_plate = IIf(IsDBNull(dr.Item("Lic_plate")), "", dr.Item("Lic_plate"))
                            BeTransReDet.Fecha_vence = IIf(IsDBNull(dr.Item("Fecha_vence")), New Date(1900, 1, 1), dr.Item("Fecha_vence"))

                            vDocumentoUbicacion = clsLnTrans_oc_det_lote.Get_Ubicacion_By_BeTransReDet(BeTransReDet,
                                                                                                       lConnection,
                                                                                                       lTransaction)

                            If vDocumentoUbicacion <> "" Then
                                clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion,
                                                                                            vRecepcionAlmacen,
                                                                                            vTipoPush, "",
                                                                                            pIdRecepcionEnc,
                                                                                            IdRecepcionDet,
                                                                                            pIdUsuario,
                                                                                            lConnection,
                                                                                            lTransaction)

                            End If

                        Next

                    End If

                    lDT.Dispose()
                    lDTA.Dispose()

                End Using

            Catch ex As Exception
                Throw New Exception("No se pudo guardar la transaccion de push " + ex.Message)
            End Try

            Dim vSQL As String = "SELECT count(IdTransaccionPush) AS Cant 
                                  FROM i_nav_transacciones_push 
                                  WHERE IdRecepcionEnc = @IdRecepcionEnc AND Enviado_A_ERP = 0"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                Dim lValue = lCommand.ExecuteScalar()
                Registros_Pendientes_Push = (lValue > 0)

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220505: Genera la tarea de recepción en estado cerrada para transferencias directas entre bodegas de WMS.
    ''' </summary>
    ''' <param name="BeOrdenCompraEnc"></param>
    ''' <param name="Resultado"></param>
    ''' <param name="CrearRecepcionPorDefecto"></param>
    ''' <param name="BeMI3Config"></param>
    ''' <param name="OutBeRecepcionEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Generar_Tarea_Recepcion_By_OrdenCompraEnc_Transfer(ByRef BeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                                              ByRef Resultado As String,
                                                                              ByVal CrearRecepcionPorDefecto As Boolean,
                                                                              ByVal BeMI3Config As clsBeI_nav_config_enc,
                                                                              ByRef OutBeRecepcionEnc As clsBeTrans_re_enc,
                                                                              ByRef Recibir_Producto_Bodega_Destino As Boolean,
                                                                              ByRef lConnection As SqlConnection,
                                                                              ByRef lTransaction As SqlTransaction) As Boolean


        Generar_Tarea_Recepcion_By_OrdenCompraEnc_Transfer = False : OutBeRecepcionEnc = Nothing

        Dim RegistrosAfectados As Integer = 0
        Dim lblResult As New RichTextBox
        Dim OrdenCompraReOc As New clsBeTrans_re_oc
        Dim BeTareaHH As New clsBeTarea_hh
        Dim BeRecepcionEnc As New clsBeTrans_re_enc
        Dim IdBodegaDestino As Integer = 0
        Dim IdPropietario As Integer = 0
        Dim TiempoMedioIngresoMinutos As Double

        Try

            If Not clsLnTrans_re_oc.Existe_Documento_By_IdOrdenCompraEnc(BeOrdenCompraEnc.IdOrdenCompraEnc,
                                                                         lConnection,
                                                                         lTransaction) Then

                IdBodegaDestino = BeOrdenCompraEnc.IdBodega

                BeRecepcionEnc.IsNew = True
                BeRecepcionEnc.IdRecepcionEnc = MaxID(lConnection, lTransaction) + 1
                BeRecepcionEnc.PropietarioBodega = New clsBePropietario_bodega
                BeRecepcionEnc.PropietarioBodega.IdBodega = BeOrdenCompraEnc.IdBodega
                '#EJC20210715: IdBodega se comparte a la tarea de recepción en generación automática de tarea..
                BeRecepcionEnc.IdBodega = BeOrdenCompraEnc.IdBodega
                BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega
                BeRecepcionEnc.User_agr = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fec_agr = Now
                BeRecepcionEnc.Activo = True
                BeRecepcionEnc.Estado = IIf(Recibir_Producto_Bodega_Destino, "Cerrado", "Nuevo")

                BeRecepcionEnc.OrdenCompraRec = New clsBeTrans_re_oc
                BeRecepcionEnc.OrdenCompraRec.IsNew = True
                BeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc

                If BeRecepcionEnc.PropietarioBodega Is Nothing OrElse BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega <= 0 Then
                    Throw New Exception("Propietario no válido al crear la recepción")
                End If

                'Ingreso con referenci a orden de compra para procesar en HH
                BeRecepcionEnc.IdTipoTransaccion = IIf(Recibir_Producto_Bodega_Destino, "MSOC00", "HCOC00")
                BeRecepcionEnc.IdMuelle = clsLnBodega_muelles.Get_IdMuelle_Default_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdMuelle = 0 Then
                    Throw New Exception("No existe ningún muelle por defecto para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                BeRecepcionEnc.IdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdUbicacionRecepcion = 0 Then
                    Throw New Exception("No está configurada la ubicación por defecto para recepción para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                TiempoMedioIngresoMinutos = clsLnTarea_hh.Get_Tiempo_Medio_Tarea_Ingreso_Minutos(lConnection, lTransaction)

                BeRecepcionEnc.Fecha_recepcion = Now.Date
                BeRecepcionEnc.Hora_ini_pc = Now
                BeRecepcionEnc.Hora_fin_pc = Now.AddMinutes(TiempoMedioIngresoMinutos)
                BeRecepcionEnc.Muestra_precio = False
                BeRecepcionEnc.Fec_mod = Now
                BeRecepcionEnc.User_mod = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fecha_tarea = Now
                BeRecepcionEnc.Tomar_fotos = False
                BeRecepcionEnc.Escanear_rec_ubic = False
                BeRecepcionEnc.Para_por_codigo = False
                BeRecepcionEnc.Observacion = "FROMMI3"
                BeRecepcionEnc.IdPiloto = 0
                BeRecepcionEnc.IdVehiculo = 0
                BeRecepcionEnc.Habilitar_Stock = True
                '#CKFK Agregué el IdBodega porque no lo estaba enviando
                BeRecepcionEnc.IdBodega = IdBodegaDestino

                '#EJC2024: Mapeo de campo No contenedor para cumbre en recepción automática.
                BeRecepcionEnc.No_Contenedor = BeOrdenCompraEnc.No_Documento_Recepcion_ERP

                OrdenCompraReOc.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                OrdenCompraReOc.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc
                OrdenCompraReOc.IsNew = True
                OrdenCompraReOc.No_docto = BeOrdenCompraEnc.Referencia
                OrdenCompraReOc.OC = BeOrdenCompraEnc
                OrdenCompraReOc.Recepcion_ciega = False
                OrdenCompraReOc.Recepcion_manual = False
                OrdenCompraReOc.User_agr = BeMI3Config.IdUsuario

                BeRecepcionEnc.OrdenCompraRec = OrdenCompraReOc


                Dim pListOperadorRecepcion As New List(Of clsBeTrans_re_op)
                Dim pListOperadorBodega As New List(Of clsBeOperador_bodega)

                '#EJC20190711: Broadcast a todos los operadores de la bodega con la tarea.
                pListOperadorBodega = clsLnOperador_bodega.Get_All_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If Not pListOperadorBodega Is Nothing Then

                    For Each Op In pListOperadorBodega

                        Dim Obj As New clsBeTrans_re_op()
                        Obj.IdOperadorBodega = Op.IdOperadorBodega
                        Obj.User_agr = BeMI3Config.IdUsuario
                        Obj.Fec_agr = Now
                        Obj.User_mod = BeMI3Config.IdUsuario
                        Obj.Fec_mod = Now
                        Obj.IsNew = True
                        Obj.UsaHH = True
                        pListOperadorRecepcion.Add(Obj)

                    Next

                End If

                Guardar(BeOrdenCompraEnc.IdBodega,
                        Nothing,
                        BeRecepcionEnc,
                        BeRecepcionEnc.OrdenCompraRec,
                        Nothing,
                        Nothing,
                        pListOperadorRecepcion,
                        Nothing,
                        Nothing,
                        Nothing,
                        Nothing,
                        Nothing,
                        lConnection,
                        lTransaction)

                Generar_Tarea_Recepcion_By_OrdenCompraEnc_Transfer = True

                OutBeRecepcionEnc = BeRecepcionEnc
            End If

        Catch ex As Exception
            '#MECR23092025: Se agrego bitacora de los para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdBodega:=BeOrdenCompraEnc.IdBodega,
                                                 pIdUsuarioAgr:=BeOrdenCompraEnc.User_Agr,
                                                 pIdRecEnc:=OutBeRecepcionEnc.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function GuardarHH_S(ByVal pIdRecepcionEnc As Integer,
                                       ByVal pIdOrdenCompraEnc As Integer,
                                       ByVal BeTransReDet As clsBeTrans_re_det,
                                       ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                       ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                       ByVal pListStockRec As List(Of clsBeStock_rec),
                                       ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                       ByVal pLotesRec As clsBeTrans_oc_det_lote,
                                       ByVal pIdEmpresa As Integer,
                                       ByVal pIdBodega As Integer,
                                       ByVal pIdUsuario As Integer,
                                       ByVal pIdResolucionLp As Integer,
                                       Optional pIdOperadorBodega As Integer = 0,
                                       Optional EsCajaMaster As Boolean = False) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim CadenaResultado As String = ""
        Dim IdTipoDocumento As Integer = 0
        Dim vResultadoOc As Integer = 0
        Dim vResultadoGuardarReDet As Integer = 0
        Dim vResultadoEliminar As String = ""
        Dim vResultadoActualizarCantidadRecibidaDI As Integer = 0
        Dim vResultadoStockSeRec As Integer = 0
        Dim vResultadoStockRec As Integer = 0
        Dim vResultGuarda_Producto_Pallet As Integer = 0
        Dim vResultadoInsertMovimientos As Integer = 0
        Dim vResultadoInsertStock As Integer = 0
        Dim vResultadoStockParametroRec As Integer = 0
        Dim vResultadoInsertar_Stock_Serializado_Recepcion As Integer = 0
        Dim vResultadoActualiza_Estado_Barras_Pallet As Integer = 0
        Dim vResultadoGuardaLotes As Integer = 0
        Dim Guarda_Trans_Re_Det_Parametros As Integer = 0
        Dim pIdStock As Integer = 0
        Dim Stock_Disponible As Boolean = False
        'Dim vControlLp As Boolean = False

        '#GT02122024: variable de control en genera LP
        Dim vGenera_LP As Boolean = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#EJC20220121: Validar que no haya sido Finalizada previamente.
            If Not Finalizada(pIdRecepcionEnc, lConnection, lTransaction) Then

                'Dim stopwatch As Stopwatch = Stopwatch.StartNew()

                If pIdOrdenCompraEnc > 0 Then

                    IdTipoDocumento = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                lConnection,
                                                                                                lTransaction)

                End If

                '#GT19012023: bandera para aplicar historico 
                Dim BeEmpresa As New clsBeEmpresa
                BeEmpresa.IdEmpresa = pIdEmpresa
                BeEmpresa = clsLnEmpresa.GetSingle(BeEmpresa,
                                                   lConnection,
                                                   lTransaction)

                Dim BeBodega As New clsBeBodega()
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega,
                                                             lConnection,
                                                             lTransaction)

                If BeBodega Is Nothing Then
                    Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega para el IdBodega: " & pIdBodega)
                End If

                '#GT02122024: variable para saber si producto genera LP
                If Not BeTransReDet.Presentacion Is Nothing Then
                    vGenera_LP = BeTransReDet.Producto.Genera_lp Or BeTransReDet.Presentacion.Genera_lp_auto
                Else
                    vGenera_LP = BeTransReDet.Producto.Genera_lp
                End If

                Dim vLPExisteEnLote = False
                Dim pRecEnc As New clsBeTrans_re_enc
                pRecEnc = Get_Single_By_IdREcepcionEnc_Sin_Det(pIdRecepcionEnc,
                                                               lConnection,
                                                               lTransaction)

                If Not pListStockRec Is Nothing Then

                    If pListStockRec.Count > 0 Then

                        For Each pBeStockRec In pListStockRec

                            If BeBodega.bloquear_lp_hh Then

                                Dim vLPExiste As Boolean = False
                                Dim vLPexisteEnRec = False

                                If vGenera_LP AndAlso String.IsNullOrEmpty(pBeStockRec.Lic_plate) Then
                                    Throw New Exception("ERROR_02122024A_HH_GuardarRecepcion_S: La licencia en stock esta vacia!.")
                                End If

                                If vGenera_LP AndAlso String.IsNullOrEmpty(BeTransReDet.Lic_plate) Then
                                    Throw New Exception("ERROR_02122024B_HH_GuardarRecepcion_S: La licencia en la recepciòn esta vacia!.")
                                End If

                                vLPexisteEnRec = clsLnTrans_re_det.Existe_By_IdRecepcionEnc_And_IdRecepcionDet(BeTransReDet, lConnection, lTransaction)
                                vLPExiste = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(pBeStockRec.Lic_plate, pIdBodega, lConnection, lTransaction)

                                If vLPExiste OrElse vLPexisteEnRec Then
                                    Throw New Exception("ERROR_20220823C_HH_GuardarRecepcion_S: La licencia: " & pBeStockRec.Lic_plate & " fue registrada previamente.")
                                End If

                            End If

                            'vLPExisteEnLote = clsLnTrans_oc_det_lote.Get_Single_By_Licencia(pBeStockRec.Lic_plate, lConnection, lTransaction)
                            vLPExisteEnLote = clsLnI_nav_barras_pallet.Get_Single_By_Licencia(pBeStockRec.Lic_plate, lConnection, lTransaction)

                        Next

                        '#AT20250915 Agregue EsCajaMaster a la validacion
                        If pIdResolucionLp > 0 AndAlso vGenera_LP AndAlso Not EsCajaMaster Then

                            Dim BeResolLp As New clsBeResolucion_lp_operador()
                            BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucionLp,
                                                                              lConnection,
                                                                              lTransaction)

                            If Not BeResolLp Is Nothing Then
                                BeResolLp.Correlativo_Actual += 1
                                clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp,
                                                                                          lConnection,
                                                                                          lTransaction)

                                '#GT02122024: nuevo punto de control
                                Dim vMsgError As String = "AVISO_20242211_HH resolucion serie : " & BeResolLp.Serie & " y correlativo: " & BeResolLp.Correlativo_Actual
                                clsLnLog_error_wms.Agregar_Error(vMsgError)
                            Else
                                Throw New Exception("ERROR_02122024D_HH_GuardarRecepcion_S: No se obtuvo la resolucion de LP para el producto!.")
                            End If

                        Else
                            '#AT20250915 Solo si escajamaster = false
                            If Not EsCajaMaster Then

                                'If vGenera_LP AndAlso pIdResolucionLp <= 0 Then
                                '    Throw New Exception("ERROR_02122024_HH_GuardarRecepcion: El producto maneja lic_plate, pero la resoluciòn no es correcta!." & pIdResolucionLp)
                                'End If

                                If (vGenera_LP AndAlso pIdResolucionLp <= 0) And Not vLPExisteEnLote Then
                                    Dim vMsgError As String = "ERROR_20262211_HH_GuardarRecepcion_S El producto maneja lic_plate, pero la resoluciòn no es correcta!. " & pIdResolucionLp
                                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                                    Throw New Exception("ERROR_20262211_HH_GuardarRecepcion_S: El producto maneja lic_plate, pero la resoluciòn no es correcta!. " & pIdResolucionLp)
                                End If

                                If Not vGenera_LP And pIdResolucionLp <= 0 Then
                                    Dim vMsgError As String = "AVISO_20242211_HH_GuardarRecepcion_S recepcion sin licencia : " & pRecEnc.IdRecepcionEnc
                                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                                End If
                            End If
                        End If

                        Dim vResultInsertEncabezadoRec As Integer = 0

                        If pRecEnc.IsNew Then

                            vResultInsertEncabezadoRec = Insertar(pRecEnc,
                                                                  lConnection,
                                                                  lTransaction)

                            If vResultInsertEncabezadoRec > 0 Then
                                CadenaResultado += "Inserté encabezado recepción " & vResultInsertEncabezadoRec
                            Else
                                Throw New Exception("ERROR_202210051030F_HH_GuardarRecepcion_S: No se pudo insertar el encabezado de la recepción")
                            End If

                        End If

                        If Not BeTransReDet Is Nothing Then

                            vResultadoEliminar = clsLnTrans_re_det.Eliminar_Detalle(pIdOrdenCompraEnc,
                                                                                    BeTransReDet,
                                                                                    lConnection,
                                                                                    lTransaction)

                            If vResultadoEliminar <> "" Then
                                CadenaResultado += "Eliminar_Detalle_Recepción: " & vResultadoEliminar
                            End If

                            If clsLnTrans_re_det.Existe_By_BeRecepcionDet(BeTransReDet, lConnection, lTransaction) Then
                                Throw New Exception("ERROR_19122024_HH_GuardarRecepcion_S: La linea de recepcion existe, no se puede guardar nuevamente.")
                            End If

                            If vGenera_LP AndAlso String.IsNullOrEmpty(BeTransReDet.Lic_plate) Then
                                Throw New Exception("ERROR_02122024_1929_HH_GuardarRecepcion_S: La linea de recepcion no tiene una LP asignada!.")
                            End If

                            vResultadoGuardarReDet = clsLnTrans_re_det.Guarda_Trans_re_det(BeTransReDet,
                                                                                           pListStockRec,
                                                                                           lConnection,
                                                                                           lTransaction)

                            If vResultadoGuardarReDet > 0 Then
                                CadenaResultado += "Guarda_Trans_re_det " & vResultadoGuardarReDet
                            End If

                            '#EJC20210412:Agregado para actualizar la cantidad recibida por lote.
                            vResultadoGuardaLotes = clsLnTrans_oc_det_lote.Guarda_Trans_re_det_lote(pLotesRec,
                                                                                                    lConnection,
                                                                                                    lTransaction)

                            If vResultadoGuardaLotes > 0 Then
                                CadenaResultado += "clsLnTrans_oc_det_lote " & vResultadoGuardaLotes
                            End If

                            Guarda_Trans_Re_Det_Parametros = clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                                                                         BeTransReDet,
                                                                                                                         pListRecDetParam,
                                                                                                                         lConnection,
                                                                                                                         lTransaction)

                            If Guarda_Trans_Re_Det_Parametros > 0 Then
                                CadenaResultado += "Guarda_Trans_Re_Det_Parametros " & Guarda_Trans_Re_Det_Parametros
                            End If

                        Else
                            Throw New Exception("ERROR_202210051030E: La lista de RecDet Is Nothing.")
                        End If

                        If pIdOrdenCompraEnc > 0 Then

                            If Not BeTransReDet Is Nothing Then

                                vResultadoActualizarCantidadRecibidaDI = clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pIdOrdenCompraEnc,
                                                                                                                          BeTransReDet,
                                                                                                                          lConnection,
                                                                                                                          lTransaction)

                                If vResultadoActualizarCantidadRecibidaDI > 0 Then
                                    CadenaResultado += "Actualiza_Cantidad_Recibida_OC " & vResultadoActualizarCantidadRecibidaDI
                                Else
                                    Throw New Exception("ERROR_202210051030G: No se pudo actualizar la cantidad recibida en el documento de ingreso.")
                                End If

                            End If

                        End If

                        If Not pListStockRec Is Nothing Then

                            If pListStockRec.Count > 0 Then

                                'GT02122024: antes de insertar stock validar que genera LP y el stock maneje una LP
                                Dim pExisteLicencia = pListStockRec.Find(Function(x) Not String.IsNullOrEmpty(x.Lic_plate))

                                If vGenera_LP AndAlso pExisteLicencia Is Nothing Then
                                    Throw New Exception("ERROR_02122024_1950_HH_GuardarRecepcion_S: No se puede registrar stock_rec sin licencia.")
                                End If

                                '#GT22112024: aqui es donde se ha dado error de log, validamos que datos viene antes de intentar guardar
                                vResultadoStockRec = clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                                                                     pIdBodega,
                                                                                     pListStockRec,
                                                                                     lConnection,
                                                                                     lTransaction)

                                If vResultadoStockRec > 0 Then
                                    CadenaResultado += " Guarda_Stock_Rec " & vResultadoStockRec
                                Else
                                    Throw New Exception("ERROR_202210051058: No se pudo insertar en stock_rec.")
                                End If

                                vResultadoStockSeRec = clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                                                             pListStockRec,
                                                                                             lConnection,
                                                                                             lTransaction)

                                If vResultadoStockSeRec > 0 Then
                                    CadenaResultado += "Guarda_Stock_Se_Rec " & vResultadoStockSeRec
                                End If

                            Else
                                Throw New Exception("#ERR20200317A: La lista de stock no tiene registros.")
                            End If

                        Else
                            Throw New Exception("#ERR20200317B: La lista de stock para recepción está vacía.")
                        End If


                        If Not pListProductoPallet Is Nothing Then

                            vResultGuarda_Producto_Pallet = clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                                                                        pListProductoPallet,
                                                                                                        lConnection,
                                                                                                        lTransaction)

                            If vResultGuarda_Producto_Pallet > 0 Then
                                CadenaResultado += "Guarda_Producto_Pallet " & vResultGuarda_Producto_Pallet
                            End If

                        End If

                        Dim BeStock As New clsBeStock()

                        If pRecEnc.Habilitar_Stock Then

                            Dim pBeINavBarraPallet As New clsBeI_nav_barras_pallet

                            If Not pListStockRec Is Nothing Then

                                If pListStockRec.Count > 0 Then

                                    For Each pBeStockRec As clsBeStock_rec In pListStockRec

                                        BeStock = New clsBeStock
                                        pBeStockRec.IdBodega = pIdBodega
                                        vResultadoInsertMovimientos = 0
                                        pBeStockRec.Fecha_Ingreso = Now
                                        pBeStockRec.Fec_agr = Now
                                        pBeStockRec.Fec_mod = Now
                                        clsPublic.CopyObject(pBeStockRec, BeStock)

                                        If vGenera_LP AndAlso String.IsNullOrEmpty(pBeStockRec.Lic_plate) Then
                                            Throw New Exception("ERROR_02122024_1955_HH_GuardarRecepcion_S: No se puede registrar el movimiento sin licencia.")
                                        End If

                                        vResultadoInsertMovimientos = clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                                                                            pIdBodega,
                                                                                                                            pIdUsuario,
                                                                                                                            pBeStockRec,
                                                                                                                            lConnection,
                                                                                                                            lTransaction,
                                                                                                                            pIdOperadorBodega)


                                        If vResultadoInsertMovimientos > 0 Then

                                            BeStock.IdStock = clsLnStock.MaxID(lConnection, lTransaction) + 1

                                            If vGenera_LP AndAlso String.IsNullOrEmpty(BeStock.Lic_plate) Then
                                                Throw New Exception("ERROR_02122024_2000_HH_GuardarRecepcion_S: No se puede registrar el stock sin licencia.")
                                            End If

                                            vResultadoInsertStock = clsLnStock.Insertar(BeStock,
                                                                                        lConnection,
                                                                                        lTransaction)

                                            If vResultadoInsertStock > 0 Then
                                                '#GT31012023 confirma stock disponible y el idstock para generar historico.
                                                Stock_Disponible = True
                                                pIdStock = 0
                                                pIdStock = BeStock.IdStock

                                                CadenaResultado += " Inserta_Stock: " & vResultadoInsertStock

                                                vResultadoStockParametroRec = clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                                                                      BeStock.IdStock,
                                                                                                                                      lConnection,
                                                                                                                                      lTransaction)

                                                If vResultadoStockParametroRec > 0 Then
                                                    CadenaResultado += " Insertar_Stock_Parametro_Recepcion: " & vResultadoStockParametroRec
                                                End If


                                                vResultadoInsertar_Stock_Serializado_Recepcion = clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                                                                                                    BeStock.IdStock,
                                                                                                                                                    lConnection,
                                                                                                                                                    lTransaction)

                                                If vResultadoInsertar_Stock_Serializado_Recepcion > 0 Then
                                                    CadenaResultado += " Insertar_Stock_Serializado_Recepcion: " & vResultadoInsertar_Stock_Serializado_Recepcion
                                                End If

                                            Else
                                                '#GT21102022_1600: sino inserta stock se lanza excepción
                                                Throw New Exception("ERROR_202210211600: No se pudo insertar el stock.")
                                            End If

                                        Else
                                            Throw New Exception("ERROR_202210051111: No se pudo insertar el movimiento.")
                                        End If


                                        '#EJC20190329_0538PM: Marcar el pallet como recibido.
                                        If pBeStockRec.Lic_plate <> "" Then

                                            pBeINavBarraPallet.Recibido = True
                                            pBeINavBarraPallet.IdRecepcion = pRecEnc.IdRecepcionEnc
                                            pBeINavBarraPallet.Codigo_barra = pBeStockRec.Lic_plate
                                            pBeINavBarraPallet.Fecha_Ingreso = Now
                                            pBeINavBarraPallet.Fecha_Agregado = Now
                                            pBeINavBarraPallet.Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)

                                            If Not pBeINavBarraPallet.Bodega_Destino Is Nothing Then
                                                CadenaResultado += "Get_Codigo_By_IdBodega: " & pBeINavBarraPallet.Bodega_Destino
                                            Else
                                                Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega destino para el IdBodega: " & pIdBodega)
                                            End If

                                            vResultadoActualiza_Estado_Barras_Pallet = clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet(pBeINavBarraPallet,
                                                                                                                                               lConnection,
                                                                                                                                               lTransaction)

                                            If vResultadoActualiza_Estado_Barras_Pallet > 0 Then
                                                CadenaResultado += "Actualiza_Estado_Barras_Pallet: " & vResultadoActualiza_Estado_Barras_Pallet
                                            End If

                                        End If

                                    Next

                                Else
                                    Throw New Exception("ERROR_20220914_1048: Se encontró una inconsistencia al procesar el registro de ingreso el count() de la lista de stock es 0.")
                                End If

                            Else
                                Throw New Exception("ERROR_20220914_1047: Se encontró una inconsistencia al procesar el registro de ingreso la lista de stock está vacía.")
                            End If

                            If Not BeTransReDet Is Nothing Then

                                If BeTransReDet.IsNew Then

                                    CadenaResultado += "Inserta transacciones out"

                                    Dim vResultado As String = clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                                                                     pIdBodega,
                                                                                                                     IdTipoDocumento,
                                                                                                                     BeTransReDet,
                                                                                                                     pIdOrdenCompraEnc,
                                                                                                                     pIdUsuario,
                                                                                                                     False,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)

                                    CadenaResultado += " Insertar_Ingreso_Parcial: " & vResultado

                                    Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                                    BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                                    BeLoteNum.IdProductoBodega = BeTransReDet.IdProductoBodega
                                    BeLoteNum.IdRecepcionEnc = pRecEnc.IdRecepcionEnc
                                    BeLoteNum.Codigo = pBeINavBarraPallet.Codigo
                                    BeLoteNum.Lote = pBeINavBarraPallet.Lote
                                    BeLoteNum.Lote_Numerico = pBeINavBarraPallet.Lote_Numerico
                                    BeLoteNum.Cantidad = BeTransReDet.cantidad_recibida
                                    BeLoteNum.FechaIngreso = Now
                                    clsLnTrans_re_det_lote_num.Insertar(BeLoteNum,
                                                                            lConnection,
                                                                            lTransaction)

                                End If

                                Dim vPosiciones As Integer = 0

                                If BeTransReDet.Pallet_No_Estandar Then

                                    Dim BeStockDet As New clsBeStock_det
                                    BeStockDet.IdStock = BeStock.IdStock
                                    BeStockDet.Posiciones = BeTransReDet.Posiciones

                                    If clsLnStock_det.Get_Single_By_IdStock(BeStockDet, lConnection, lTransaction) Then
                                        BeStockDet.Posiciones = vPosiciones
                                        clsLnStock_det.Actualizar(BeStockDet, lConnection, lTransaction)
                                    Else
                                        clsLnStock_det.Insertar(BeStockDet, lConnection, lTransaction)
                                    End If

                                End If

                            End If

                        Else
                            '#GTZ_nuevo control
                            Dim vMsgError As String = "AVISO_20242211_HH_GuardarRecepcion_S: no_habilitar_stock re_enc: " & pRecEnc.IdRecepcionEnc
                            clsLnLog_error_wms.Agregar_Error(vMsgError)
                        End If

                        'Dim vMsgErrorGuardarStock As String = "AVISO_20242211_HH_GuardarRecepcion_S: timer_GuardarStock " & stopwatch.ElapsedMilliseconds
                        'clsLnLog_error_wms.Agregar_Error(vMsgErrorGuardarStock, lConnection, lTransaction)

                        CadenaResultado += " Terminé la recepción " & pRecEnc.IdRecepcionEnc.ToString

                    Else
                        Throw New Exception("ERROR_202210051030A: El count de la lista de stock es 0.")
                    End If

                Else
                    Throw New Exception("ERROR_202210051030B: La lista de stock Is Nothing.")
                End If

            Else
                Throw New Exception("ERROR_DE_PROCESO_202302221004: La recepción fue previamente finalizada.")
            End If

            lTransaction.Commit()

            Return CadenaResultado

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", ex.Message, CadenaResultado))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GuardarHH_CM(ByVal pIdRecepcionEnc As Integer,
                                        ByVal pIdOrdenCompraEnc As Integer,
                                        ByVal pIdEmpresa As Integer,
                                        ByVal pIdBodega As Integer,
                                        ByVal pIdUsuario As Integer,
                                        ByVal pIdOperadorBodega As Integer,
                                        ByVal pListStockRec As List(Of clsBeStock_rec),
                                        ByVal pListaDetLote As List(Of clsBeTrans_oc_det_lote),
                                        ByVal pListaRecDet As List(Of clsBeTrans_re_det)) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim pListRecDetParam As New List(Of clsBeTrans_re_det_parametros)
        Dim pListStockRecSer As New List(Of clsBeStock_se_rec)
        Dim pListProductoPallet As New List(Of clsBeProducto_pallet)
        'Dim BeTransReDet As clsBeTrans_re_det
        Dim pIdResolucionLp As Integer
        Dim EsCajaMaster As Boolean = False

        Dim CadenaResultado As String = ""
        Dim IdTipoDocumento As Integer = 0
        Dim vResultadoOc As Integer = 0
        Dim vResultadoGuardarReDet As Integer = 0
        Dim vResultadoEliminar As String = ""
        Dim vResultadoActualizarCantidadRecibidaDI As Integer = 0
        Dim vResultadoStockSeRec As Integer = 0
        Dim vResultadoStockRec As Integer = 0
        Dim vResultGuarda_Producto_Pallet As Integer = 0
        Dim vResultadoInsertMovimientos As Integer = 0
        Dim vResultadoInsertStock As Integer = 0
        Dim vResultadoStockParametroRec As Integer = 0
        Dim vResultadoInsertar_Stock_Serializado_Recepcion As Integer = 0
        Dim vResultadoActualiza_Estado_Barras_Pallet As Integer = 0
        Dim vResultadoGuardaLotes As Integer = 0
        Dim Guarda_Trans_Re_Det_Parametros As Integer = 0
        Dim pIdStock As Integer = 0
        Dim Stock_Disponible As Boolean = False
        'Dim vControlLp As Boolean = False

        '#GT02122024: variable de control en genera LP
        Dim vGenera_LP As Boolean = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#EJC20220121: Validar que no haya sido Finalizada previamente.
            If Not Finalizada(pIdRecepcionEnc, lConnection, lTransaction) Then

                'Dim stopwatch As Stopwatch = Stopwatch.StartNew()

                If pIdOrdenCompraEnc > 0 Then

                    IdTipoDocumento = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                lConnection,
                                                                                                lTransaction)

                End If

                '#GT19012023: bandera para aplicar historico 
                Dim BeEmpresa As New clsBeEmpresa
                BeEmpresa.IdEmpresa = pIdEmpresa
                BeEmpresa = clsLnEmpresa.GetSingle(BeEmpresa,
                                                   lConnection,
                                                   lTransaction)

                Dim BeBodega As New clsBeBodega()
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega,
                                                             lConnection,
                                                             lTransaction)

                If BeBodega Is Nothing Then
                    Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega para el IdBodega: " & pIdBodega)
                End If

                '#GT02122024: variable para saber si producto genera LP
                '#AT CajaMaster
                'If Not BeTransReDet.Presentacion Is Nothing Then
                '    vGenera_LP = BeTransReDet.Producto.Genera_lp Or BeTransReDet.Presentacion.Genera_lp_auto
                'Else
                '    vGenera_LP = BeTransReDet.Producto.Genera_lp
                'End If

                Dim pRecEnc As New clsBeTrans_re_enc
                pRecEnc = Get_Single_By_IdREcepcionEnc_Sin_Det(pIdRecepcionEnc,
                                                               lConnection,
                                                               lTransaction)

                If Not pListStockRec Is Nothing Then

                    If pListStockRec.Count > 0 Then

                        For Each pBeStockRec In pListStockRec

                            If BeBodega.bloquear_lp_hh Then

                                Dim vLPExiste As Boolean = False
                                Dim vLPexisteEnRec = False

                                If vGenera_LP AndAlso String.IsNullOrEmpty(pBeStockRec.Lic_plate) Then
                                    Throw New Exception("ERROR_02122024A_HH_GuardarRecepcion_S: La licencia en stock esta vacia!.")
                                End If

                                '#AT CajaMaster
                                'If vGenera_LP AndAlso String.IsNullOrEmpty(BeTransReDet.Lic_plate) Then
                                '    Throw New Exception("ERROR_02122024B_HH_GuardarRecepcion_S: La licencia en la recepciòn esta vacia!.")
                                'End If

                                '#AT CajaMaster
                                'vLPexisteEnRec = clsLnTrans_re_det.Existe_By_IdRecepcionEnc_And_IdRecepcionDet(BeTransReDet, lConnection, lTransaction)
                                vLPExiste = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(pBeStockRec.Lic_plate, pIdBodega, lConnection, lTransaction)

                                If vLPExiste OrElse vLPexisteEnRec Then
                                    Throw New Exception("ERROR_20220823C_HH_GuardarRecepcion_S: La licencia: " & pBeStockRec.Lic_plate & " fue registrada previamente.")
                                End If

                            End If

                        Next

                        '#AT20250915 Agregue EsCajaMaster a la validacion
                        If pIdResolucionLp > 0 AndAlso vGenera_LP Then

                            Dim BeResolLp As New clsBeResolucion_lp_operador()
                            BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucionLp,
                                                                              lConnection,
                                                                              lTransaction)

                            If Not BeResolLp Is Nothing Then
                                BeResolLp.Correlativo_Actual += 1
                                clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp,
                                                                                          lConnection,
                                                                                          lTransaction)

                                '#GT02122024: nuevo punto de control
                                Dim vMsgError As String = "AVISO_20242211_HH resolucion serie : " & BeResolLp.Serie & " y correlativo: " & BeResolLp.Correlativo_Actual
                                clsLnLog_error_wms.Agregar_Error(vMsgError)
                            Else
                                Throw New Exception("ERROR_02122024D_HH_GuardarRecepcion_S: No se obtuvo la resolucion de LP para el producto!.")
                            End If
                        End If

                        Dim vResultInsertEncabezadoRec As Integer = 0

                        If pRecEnc.IsNew Then

                            vResultInsertEncabezadoRec = Insertar(pRecEnc,
                                                                  lConnection,
                                                                  lTransaction)

                            If vResultInsertEncabezadoRec > 0 Then
                                CadenaResultado += "Inserté encabezado recepción " & vResultInsertEncabezadoRec
                            Else
                                Throw New Exception("ERROR_202210051030F_HH_GuardarRecepcion_S: No se pudo insertar el encabezado de la recepción")
                            End If

                        End If

                        If Not pListaRecDet Is Nothing Then

                            For Each BeTransReDet In pListaRecDet
                                vResultadoEliminar = clsLnTrans_re_det.Eliminar_Detalle(pIdOrdenCompraEnc,
                                                                                    BeTransReDet,
                                                                                    lConnection,
                                                                                    lTransaction)

                                If vResultadoEliminar <> "" Then
                                    CadenaResultado += "Eliminar_Detalle_Recepción: " & vResultadoEliminar
                                End If

                                If clsLnTrans_re_det.Existe_By_BeRecepcionDet(BeTransReDet, lConnection, lTransaction) Then
                                    Throw New Exception("ERROR_19122024_HH_GuardarRecepcion_S: La linea de recepcion existe, no se puede guardar nuevamente.")
                                End If

                                If vGenera_LP AndAlso String.IsNullOrEmpty(BeTransReDet.Lic_plate) Then
                                    Throw New Exception("ERROR_02122024_1929_HH_GuardarRecepcion_S: La linea de recepcion no tiene una LP asignada!.")
                                End If

                                vResultadoGuardarReDet = clsLnTrans_re_det.Guarda_Trans_re_det(BeTransReDet,
                                                                                               pListStockRec,
                                                                                               lConnection,
                                                                                               lTransaction)

                                If vResultadoGuardarReDet > 0 Then
                                    CadenaResultado += "Guarda_Trans_re_det " & vResultadoGuardarReDet
                                End If

                                '#EJC20210412:Agregado para actualizar la cantidad recibida por lote.

                                Dim Obj As clsBeTrans_oc_det_lote = pListaDetLote.
                                        FirstOrDefault(Function(x) x.IdOrdenCompraDet = BeTransReDet.IdOrdenCompraDet And x.IdProductoBodega = BeTransReDet.IdProductoBodega)

                                If Obj IsNot Nothing Then
                                    vResultadoGuardaLotes = clsLnTrans_oc_det_lote.Guarda_Trans_re_det_lote(Obj,
                                                                                                            lConnection,
                                                                                                            lTransaction)

                                    Obj.Cantidad_recibida = BeTransReDet.cantidad_recibida
                                    clsLnTrans_oc_det_lote.Actualizar_Parcial(Obj, lConnection, lTransaction)

                                End If

                                If vResultadoGuardaLotes > 0 Then
                                    CadenaResultado += "clsLnTrans_oc_det_lote " & vResultadoGuardaLotes
                                End If

                                Guarda_Trans_Re_Det_Parametros = clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                                                                             BeTransReDet,
                                                                                                                             pListRecDetParam,
                                                                                                                             lConnection,
                                                                                                                             lTransaction)

                                If Guarda_Trans_Re_Det_Parametros > 0 Then
                                    CadenaResultado += "Guarda_Trans_Re_Det_Parametros " & Guarda_Trans_Re_Det_Parametros
                                End If
                            Next
                        Else
                            Throw New Exception("ERROR_202210051030E: La lista de RecDet Is Nothing.")
                        End If

                        If pIdOrdenCompraEnc > 0 Then
                            If Not pListaRecDet Is Nothing Then
                                For Each BeTransReDet In pListaRecDet
                                    vResultadoActualizarCantidadRecibidaDI = clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pIdOrdenCompraEnc,
                                                                                                                          BeTransReDet,
                                                                                                                          lConnection,
                                                                                                                          lTransaction)

                                    If vResultadoActualizarCantidadRecibidaDI > 0 Then
                                        CadenaResultado += "Actualiza_Cantidad_Recibida_OC " & vResultadoActualizarCantidadRecibidaDI
                                    Else
                                        Throw New Exception("ERROR_202210051030G: No se pudo actualizar la cantidad recibida en el documento de ingreso.")
                                    End If
                                Next
                            End If
                        End If

                        If Not pListStockRec Is Nothing Then

                            If pListStockRec.Count > 0 Then

                                'GT02122024: antes de insertar stock validar que genera LP y el stock maneje una LP
                                Dim pExisteLicencia = pListStockRec.Find(Function(x) Not String.IsNullOrEmpty(x.Lic_plate))

                                If vGenera_LP AndAlso pExisteLicencia Is Nothing Then
                                    Throw New Exception("ERROR_02122024_1950_HH_GuardarRecepcion_S: No se puede registrar stock_rec sin licencia.")
                                End If

                                '#GT22112024: aqui es donde se ha dado error de log, validamos que datos viene antes de intentar guardar
                                vResultadoStockRec = clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                                                                     pIdBodega,
                                                                                     pListStockRec,
                                                                                     lConnection,
                                                                                     lTransaction)

                                If vResultadoStockRec > 0 Then
                                    CadenaResultado += " Guarda_Stock_Rec " & vResultadoStockRec
                                Else
                                    Throw New Exception("ERROR_202210051058: No se pudo insertar en stock_rec.")
                                End If

                                vResultadoStockSeRec = clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                                                             pListStockRec,
                                                                                             lConnection,
                                                                                             lTransaction)

                                If vResultadoStockSeRec > 0 Then
                                    CadenaResultado += "Guarda_Stock_Se_Rec " & vResultadoStockSeRec
                                End If

                            Else
                                Throw New Exception("#ERR20200317A: La lista de stock no tiene registros.")
                            End If

                        Else
                            Throw New Exception("#ERR20200317B: La lista de stock para recepción está vacía.")
                        End If


                        If Not pListProductoPallet Is Nothing Then

                            vResultGuarda_Producto_Pallet = clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                                                                        pListProductoPallet,
                                                                                                        lConnection,
                                                                                                        lTransaction)

                            If vResultGuarda_Producto_Pallet > 0 Then
                                CadenaResultado += "Guarda_Producto_Pallet " & vResultGuarda_Producto_Pallet
                            End If

                        End If

                        Dim BeStock As New clsBeStock()

                        If pRecEnc.Habilitar_Stock Then

                            Dim pBeINavBarraPallet As New clsBeI_nav_barras_pallet

                            If Not pListStockRec Is Nothing Then

                                If pListStockRec.Count > 0 Then

                                    For Each pBeStockRec As clsBeStock_rec In pListStockRec

                                        BeStock = New clsBeStock
                                        pBeStockRec.IdBodega = pIdBodega
                                        vResultadoInsertMovimientos = 0
                                        pBeStockRec.Fecha_Ingreso = Now
                                        pBeStockRec.Fec_agr = Now
                                        pBeStockRec.Fec_mod = Now
                                        clsPublic.CopyObject(pBeStockRec, BeStock)

                                        If vGenera_LP AndAlso String.IsNullOrEmpty(pBeStockRec.Lic_plate) Then
                                            Throw New Exception("ERROR_02122024_1955_HH_GuardarRecepcion_S: No se puede registrar el movimiento sin licencia.")
                                        End If

                                        vResultadoInsertMovimientos = clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                                                                            pIdBodega,
                                                                                                                            pIdUsuario,
                                                                                                                            pBeStockRec,
                                                                                                                            lConnection,
                                                                                                                            lTransaction,
                                                                                                                            pIdOperadorBodega)


                                        If vResultadoInsertMovimientos > 0 Then

                                            BeStock.IdStock = clsLnStock.MaxID(lConnection, lTransaction) + 1

                                            If vGenera_LP AndAlso String.IsNullOrEmpty(BeStock.Lic_plate) Then
                                                Throw New Exception("ERROR_02122024_2000_HH_GuardarRecepcion_S: No se puede registrar el stock sin licencia.")
                                            End If

                                            vResultadoInsertStock = clsLnStock.Insertar(BeStock,
                                                                                        lConnection,
                                                                                        lTransaction)

                                            If vResultadoInsertStock > 0 Then
                                                '#GT31012023 confirma stock disponible y el idstock para generar historico.
                                                Stock_Disponible = True
                                                pIdStock = 0
                                                pIdStock = BeStock.IdStock

                                                CadenaResultado += " Inserta_Stock: " & vResultadoInsertStock

                                                vResultadoStockParametroRec = clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                                                                      BeStock.IdStock,
                                                                                                                                      lConnection,
                                                                                                                                      lTransaction)

                                                If vResultadoStockParametroRec > 0 Then
                                                    CadenaResultado += " Insertar_Stock_Parametro_Recepcion: " & vResultadoStockParametroRec
                                                End If


                                                vResultadoInsertar_Stock_Serializado_Recepcion = clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                                                                                                    BeStock.IdStock,
                                                                                                                                                    lConnection,
                                                                                                                                                    lTransaction)

                                                If vResultadoInsertar_Stock_Serializado_Recepcion > 0 Then
                                                    CadenaResultado += " Insertar_Stock_Serializado_Recepcion: " & vResultadoInsertar_Stock_Serializado_Recepcion
                                                End If

                                            Else
                                                '#GT21102022_1600: sino inserta stock se lanza excepción
                                                Throw New Exception("ERROR_202210211600: No se pudo insertar el stock.")
                                            End If

                                        Else
                                            Throw New Exception("ERROR_202210051111: No se pudo insertar el movimiento.")
                                        End If


                                        '#EJC20190329_0538PM: Marcar el pallet como recibido.
                                        If pBeStockRec.Lic_plate <> "" Then

                                            pBeINavBarraPallet.Recibido = True
                                            pBeINavBarraPallet.IdRecepcion = pRecEnc.IdRecepcionEnc
                                            pBeINavBarraPallet.Codigo_barra = pBeStockRec.Lic_plate
                                            pBeINavBarraPallet.Fecha_Ingreso = Now
                                            pBeINavBarraPallet.Fecha_Agregado = Now
                                            pBeINavBarraPallet.Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)

                                            If Not pBeINavBarraPallet.Bodega_Destino Is Nothing Then
                                                CadenaResultado += "Get_Codigo_By_IdBodega: " & pBeINavBarraPallet.Bodega_Destino
                                            Else
                                                Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega destino para el IdBodega: " & pIdBodega)
                                            End If

                                            vResultadoActualiza_Estado_Barras_Pallet = clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet(pBeINavBarraPallet,
                                                                                                                                               lConnection,
                                                                                                                                               lTransaction)

                                            If vResultadoActualiza_Estado_Barras_Pallet > 0 Then
                                                CadenaResultado += "Actualiza_Estado_Barras_Pallet: " & vResultadoActualiza_Estado_Barras_Pallet
                                            End If

                                        End If

                                    Next

                                Else
                                    Throw New Exception("ERROR_20220914_1048: Se encontró una inconsistencia al procesar el registro de ingreso el count() de la lista de stock es 0.")
                                End If

                            Else
                                Throw New Exception("ERROR_20220914_1047: Se encontró una inconsistencia al procesar el registro de ingreso la lista de stock está vacía.")
                            End If

                            If Not pListaRecDet Is Nothing Then

                                For Each BeTransReDet In pListaRecDet
                                    If BeTransReDet.IsNew Then

                                        CadenaResultado += "Inserta transacciones out"

                                        Dim vResultado As String = clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                                                                         pIdBodega,
                                                                                                                         IdTipoDocumento,
                                                                                                                         BeTransReDet,
                                                                                                                         pIdOrdenCompraEnc,
                                                                                                                         pIdUsuario,
                                                                                                                         False,
                                                                                                                         lConnection,
                                                                                                                         lTransaction)

                                        CadenaResultado += " Insertar_Ingreso_Parcial: " & vResultado

                                        Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                                        BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                                        BeLoteNum.IdProductoBodega = BeTransReDet.IdProductoBodega
                                        BeLoteNum.IdRecepcionEnc = pRecEnc.IdRecepcionEnc
                                        BeLoteNum.Codigo = pBeINavBarraPallet.Codigo
                                        BeLoteNum.Lote = pBeINavBarraPallet.Lote
                                        BeLoteNum.Lote_Numerico = pBeINavBarraPallet.Lote_Numerico
                                        BeLoteNum.Cantidad = BeTransReDet.cantidad_recibida
                                        BeLoteNum.FechaIngreso = Now
                                        clsLnTrans_re_det_lote_num.Insertar(BeLoteNum,
                                                                                lConnection,
                                                                                lTransaction)

                                    End If

                                    Dim vPosiciones As Integer = 0

                                    If BeTransReDet.Pallet_No_Estandar Then

                                        Dim BeStockDet As New clsBeStock_det
                                        BeStockDet.IdStock = BeStock.IdStock
                                        BeStockDet.Posiciones = BeTransReDet.Posiciones

                                        If clsLnStock_det.Get_Single_By_IdStock(BeStockDet, lConnection, lTransaction) Then
                                            BeStockDet.Posiciones = vPosiciones
                                            clsLnStock_det.Actualizar(BeStockDet, lConnection, lTransaction)
                                        Else
                                            clsLnStock_det.Insertar(BeStockDet, lConnection, lTransaction)
                                        End If

                                    End If
                                Next
                            End If
                        Else
                            '#GTZ_nuevo control
                            Dim vMsgError As String = "AVISO_20242211_HH_GuardarRecepcion_S: no_habilitar_stock re_enc: " & pRecEnc.IdRecepcionEnc
                            clsLnLog_error_wms.Agregar_Error(vMsgError)
                        End If

                        'Dim vMsgErrorGuardarStock As String = "AVISO_20242211_HH_GuardarRecepcion_S: timer_GuardarStock " & stopwatch.ElapsedMilliseconds
                        'clsLnLog_error_wms.Agregar_Error(vMsgErrorGuardarStock, lConnection, lTransaction)

                        CadenaResultado += " Terminé la recepción " & pRecEnc.IdRecepcionEnc.ToString

                    Else
                        Throw New Exception("ERROR_202210051030A: El count de la lista de stock es 0.")
                    End If

                Else
                    Throw New Exception("ERROR_202210051030B: La lista de stock Is Nothing.")
                End If

            Else
                Throw New Exception("ERROR_DE_PROCESO_202302221004: La recepción fue previamente finalizada.")
            End If

            lTransaction.Commit()

            Return CadenaResultado

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", ex.Message, CadenaResultado))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#GT20012023: Determina si el historico se guarda inmediato segun stock disponbile, o se itera de la lista
    Private Shared Function Recepcion_Historico_By_Proces(ByVal pStock_Disponible As Boolean,
                                                          ByVal pIdEmpresa As Integer,
                                                          ByVal pIdBodega As Integer,
                                                          ByVal pIdusuario As Integer,
                                                          ByVal pIdStock As Integer,
                                                          Optional ByVal lconnection As SqlConnection = Nothing,
                                                          Optional ByVal ltransaction As SqlTransaction = Nothing) As Boolean

        Recepcion_Historico_By_Proces = False
        Try

            If pStock_Disponible Then
                Recepcion_genera_historico(pIdEmpresa, pIdBodega, pIdusuario, pIdStock)
            Else
                Recepcion_genera_historico(pIdEmpresa, pIdBodega, pIdusuario, pIdStock, lconnection, ltransaction)
            End If

            Return Recepcion_Historico_By_Proces = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Private Shared Function Recepcion_genera_historico(ByVal pIdEmpresa As Integer,
                                                       ByVal pIdBodega As Integer,
                                                       ByVal pIdusuario As Integer,
                                                       ByVal pIdStock As Integer) As Boolean


        Dim lConnection2 As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction2 As SqlTransaction = Nothing
        Dim lIngresosYSalidasDelDia As New List(Of clsBeStockEnUnaFecha)
        Dim JornadaActual As New clsBeJornada_sistema

        Recepcion_genera_historico = False

        Try

            lConnection2.Open() : lTransaction2 = lConnection2.BeginTransaction(IsolationLevel.ReadUncommitted)


            If clsLnJornada_sistema.Existe_Jornada(lConnection2, lTransaction2) Then
                JornadaActual = clsLnJornada_sistema.Get_Jornada_Actual(lConnection2, lTransaction2)

                If Not JornadaActual Is Nothing Then
                    lIngresosYSalidasDelDia = Nothing
                    Dim DTVWStockJornada As New DataTable
                    DTVWStockJornada = clsLnJornada_sistema.Get_VW_Stock_Jornada_By_IdStock(pIdStock, lConnection2, lTransaction2)

                    If DTVWStockJornada.Rows.Count > 0 Then
                        Debug.Print("aqui")

                    End If


                    If clsLnJornada_sistema.Insertar_Stock_Jornada_Desde_HH(DTVWStockJornada,
                                                                         JornadaActual.Fecha,
                                                                         JornadaActual,
                                                                         lIngresosYSalidasDelDia,
                                                                         Nothing,
                                                                         pIdEmpresa,
                                                                         pIdusuario,
                                                                         lConnection2,
                                                                         lTransaction2,
                                                                         1) Then


                        Recepcion_genera_historico = True

                    Else

                        '#MECR23092025: Se agrego bitacora de logs para recepciones.
                        Dim message = "No inserte historico para idstock: " & pIdStock & " y user: " & pIdusuario
                        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), message)
                        clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                             pIdEmpresa:=pIdEmpresa,
                                                             pIdBodega:=pIdBodega,
                                                             pIdUsuarioAgr:=pIdusuario,
                                                             pConection:=lConnection2,
                                                             pTransaction:=lTransaction2)
                    End If

                End If

            End If

            lTransaction2.Commit()

            Return Recepcion_genera_historico

        Catch ex As Exception
            If Not lTransaction2 Is Nothing Then lTransaction2.Rollback()
            '#MECR23092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=pIdEmpresa,
                                                 pIdBodega:=pIdBodega,
                                                 pIdUsuarioAgr:=pIdusuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        Finally
            If lConnection2.State = ConnectionState.Open Then lConnection2.Close()
            If Not lConnection2 Is Nothing Then lConnection2.Dispose()
            If Not lTransaction2 Is Nothing Then lTransaction2.Dispose()
        End Try


    End Function

    Private Shared Function Recepcion_genera_historico(ByVal pIdEmpresa As Integer,
                                                       ByVal pIdBodega As Integer,
                                                       ByVal pIdusuario As Integer,
                                                       ByVal pIdStock As Integer,
                                                       ByVal lconnection2 As SqlConnection,
                                                       ByVal ltransaction2 As SqlTransaction) As Boolean


        Recepcion_genera_historico = False

        Try

            Dim lIngresosYSalidasDelDia As New List(Of clsBeStockEnUnaFecha)
            Dim JornadaActual As New clsBeJornada_sistema

            If clsLnJornada_sistema.Existe_Jornada(lconnection2, ltransaction2) Then
                JornadaActual = clsLnJornada_sistema.Get_Jornada_Actual(lconnection2, ltransaction2)

                If Not JornadaActual Is Nothing Then
                    lIngresosYSalidasDelDia = Nothing
                    Dim DTVWStockJornada As New DataTable
                    DTVWStockJornada = clsLnJornada_sistema.Get_VW_Stock_Jornada_By_IdStock(pIdStock, lconnection2, ltransaction2)

                    If clsLnJornada_sistema.Insertar_Stock_Jornada_Desde_HH(DTVWStockJornada,
                                                                         JornadaActual.Fecha,
                                                                         JornadaActual,
                                                                         lIngresosYSalidasDelDia,
                                                                         Nothing,
                                                                         pIdEmpresa,
                                                                         pIdusuario,
                                                                         lconnection2,
                                                                         ltransaction2,
                                                                         1) Then

                        Recepcion_genera_historico = True
                    Else

                        Dim message = "No inserte historico stock_no_dispoible para idstock: " & pIdStock & " y user: " & pIdusuario
                        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), message)
                        clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                          pIdEmpresa:=pIdEmpresa,
                                                          pIdBodega:=pIdBodega,
                                                          pIdUsuarioAgr:=pIdusuario,
                                                          pConection:=lconnection2,
                                                          pTransaction:=ltransaction2)

                    End If

                End If

            End If

            Return Recepcion_genera_historico

        Catch ex As Exception
            'If Not ltransaction2 Is Nothing Then ltransaction2.Rollback()
            '#MECR23092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=pIdEmpresa,
                                                 pIdBodega:=pIdBodega,
                                                 pIdUsuarioAgr:=pIdusuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try


    End Function

    Public Shared Function Get_Single_By_IdREcepcionEnc_Sin_Det(ByVal pIdRecepcionEnc As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As clsBeTrans_re_enc

        Get_Single_By_IdREcepcionEnc_Sin_Det = Nothing
        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_enc  
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_re_enc()
                    Get_Single_By_IdREcepcionEnc_Sin_Det = New clsBeTrans_re_enc()

                    Cargar(Obj, lRow)

                    Obj.IsNew = False
                    Get_Single_By_IdREcepcionEnc_Sin_Det = Obj

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Finalizar_Recepcion_By_IdRecepcionEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                            ByVal backOrder As Boolean,
                                                            ByVal pIdRecepcionEnc As Integer,
                                                            ByVal pIdEmpresa As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIdUsuario As String,
                                                            ByVal pHabilitarStock As Boolean)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeTMSTicket As New clsBeTms_ticket
        Dim BeReEnc As New clsBeTrans_re_enc

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim listaStockRec As List(Of clsBeStock_rec) = clsLnStock_rec.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)

            '#EJC20220121: Validar que no haya sido Finalizada previamente.
            If Not Finalizada(pIdRecepcionEnc, lConnection, lTransaction) Then
                '#MECR23092025: se agrego bitacora de logs para recepciones.
                Dim msjAdvertencia As String = "#240313: Se cerró la recepción: " & pIdRecepcionEnc & " IdUsuario_HH: " & pIdUsuario
                'clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia, pIdEmpresa, pIdBodega, pIdUsuario, pIdRecEnc:=pIdRecepcionEnc)
                clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia,
                                                     pIdEmpresa:=pIdEmpresa,
                                                     pIdBodega:=pIdBodega,
                                                     pIdUsuarioAgr:=pIdUsuario,
                                                     pIdRecEnc:=pIdRecepcionEnc,
                                                     pConection:=lConnection,
                                                     pTransaction:=lTransaction)

                '#EJC20220121_1102: Cambiar el estado primero.
                Actualizar_Estado_Cerrado_Recepcion(pIdRecepcionEnc,
                                                    lConnection,
                                                    lTransaction)


                BeReEnc = Get_Single_By_IdREcepcionEnc(pIdRecepcionEnc,
                                                       lConnection,
                                                       lTransaction)

                If Not BeReEnc Is Nothing Then

                    If Not Registros_Pendientes_Push(pIdRecepcionEnc,
                                                     lConnection,
                                                     lTransaction) Then

                        If Reglas_De_Recepcion_Permiten_Ingreso(BeReEnc,
                                                                lConnection,
                                                                lTransaction) Then

                            Dim lMaxS As Integer = clsLnStock.MaxID(lConnection,
                                                                lTransaction)

                            If listaStockRec IsNot Nothing AndAlso listaStockRec.Count > 0 Then

                                If Not pHabilitarStock Then

                                    Habilitar_Stock_Desde_StockRec(pIdEmpresa,
                                                                   pIdBodega,
                                                                   pIdOrdenCompraEnc,
                                                                   pIdUsuario,
                                                                   listaStockRec,
                                                                   BeReEnc.Detalle,
                                                                   lConnection,
                                                                   lTransaction)
                                Else

                                    Habilitar_Stock_Desde_Detalle_Recepcion(pIdRecepcionEnc,
                                                                            pIdOrdenCompraEnc,
                                                                            pIdUsuario,
                                                                            pIdEmpresa,
                                                                            pIdBodega,
                                                                            BeReEnc,
                                                                            lConnection,
                                                                            lTransaction)

                                End If

                            End If

                            clsLnStock_rec.Actualiza_Stock_Rec(listaStockRec,
                                                               lConnection,
                                                               lTransaction)

                            Actualizar_Estado_Pedido_Ingreso(pIdOrdenCompraEnc,
                                                             pIdRecepcionEnc,
                                                             lConnection,
                                                             lTransaction,
                                                             backOrder)

                            Actualizar_Hora_Fin_Recepcion(pIdOrdenCompraEnc,
                                                          pIdRecepcionEnc,
                                                          lConnection,
                                                          lTransaction)


                            Dim BeTareaHH As New clsBeTarea_hh
                            BeTareaHH = clsLnTarea_hh.GetSingle(1,
                                                                pIdRecepcionEnc,
                                                                BeReEnc.PropietarioBodega.IdPropietario,
                                                                lConnection,
                                                                lTransaction)

                            If Not BeTareaHH Is Nothing Then

                                If Not BeTareaHH.IdEstado = 4 Then

                                    clsLnTarea_hh.Finalizar_Tarea_Recepcion(pIdRecepcionEnc,
                                                                            lConnection,
                                                                            lTransaction)

                                Else
                                    Throw New Exception("Error_202211011918: Al parecer la recepción ya fue finalizada.")
                                End If

                            End If


                            If Not pIdOrdenCompraEnc = 0 Then

                                '#EJC20220803_1536: Validar que si tiene documento de ingreso si tiene o no ticket de TMS.
                                BeTMSTicket = clsLnTrans_oc_enc.Get_BeTicket_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                                'Si tiene ticket de TMS el documento de ingreso.
                                If Not BeTMSTicket Is Nothing Then

                                    clsLnTms_ticket.Actualizar_Tms_Ticket_Finalizado(BeTMSTicket.IdTicket,
                                                                                     lConnection,
                                                                                     lTransaction)

                                End If

                            End If

                        End If

                    Else
                        Throw New Exception("Error_20220308: La recepción tiene registros pendientes de push")
                    End If

                Else
                    Throw New Exception("ERROR_202210270019: No se pudo obtener la recepción con el identificador: " & pIdRecepcionEnc)
                End If

            Else
                Throw New Exception("Error_20220121_0004: La recepción fue finalizada previamente.")
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            '#MECR23092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdEmpresa:=pIdEmpresa,
                                                 pIdBodega:=pIdBodega,
                                                 pIdUsuarioAgr:=pIdUsuario,
                                                 pIdRecEnc:=pIdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function GuardarHH_BOF(ByVal pIdRecepcionEnc As Integer,
                                         ByVal pIdOrdenCompraEnc As Integer,
                                         ByVal BeTransReDet As clsBeTrans_re_det,
                                         ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                         ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                         ByVal pListStockRec As List(Of clsBeStock_rec),
                                         ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                         ByVal pLotesRec As clsBeTrans_oc_det_lote,
                                         ByVal pIdEmpresa As Integer,
                                         ByVal pIdBodega As Integer,
                                         ByVal pIdUsuario As Integer,
                                         ByRef IdRecepcionDet As Integer,
                                         ByVal lConnection As SqlConnection,
                                         ByVal lTransaction As SqlTransaction) As Boolean


        Dim CadenaResultado As String = ""
        Dim IdTipoDocumento As Integer = 0
        Dim vResultadoOc As Integer = 0
        Dim vResultadoGuardarReDet As Integer = 0
        Dim vResultadoEliminar As String = ""
        Dim vResultadoActualizarCantidadRecibidaDI As Integer = 0
        Dim vResultadoStockSeRec As Integer = 0
        Dim vResultadoStockRec As Integer = 0
        Dim vResultGuarda_Producto_Pallet As Integer = 0
        Dim vResultadoInsertMovimientos As Integer = 0
        Dim vResultadoInsertStock As Integer = 0
        Dim vResultadoStockParametroRec As Integer = 0
        Dim vResultadoInsertar_Stock_Serializado_Recepcion As Integer = 0
        Dim vResultadoActualiza_Estado_Barras_Pallet As Integer = 0
        Dim vResultadoGuardaLotes As Integer = 0
        Dim Guarda_Trans_Re_Det_Parametros As Integer = 0
        Dim pIdStock As Integer = 0
        Dim Stock_Disponible As Boolean = False

        GuardarHH_BOF = False

        Try

            '#EJC20220121: Validar que no haya sido Finalizada previamente.
            If Not Finalizada(pIdRecepcionEnc, lConnection, lTransaction) Then

                '#GT19012025: validar que no se haya finalizado previamente.
                If Not Anulada(pIdRecepcionEnc, lConnection, lTransaction) Then


                    If pIdOrdenCompraEnc > 0 Then

                        IdTipoDocumento = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                    End If

                    '#GT19012023: bandera para aplicar historico 
                    Dim BeEmpresa As New clsBeEmpresa
                    BeEmpresa.IdEmpresa = pIdEmpresa
                    BeEmpresa = clsLnEmpresa.GetSingle(BeEmpresa,
                                                       lConnection,
                                                       lTransaction)


                    '#EJC20220908:Consultar configuración de bodega antes de proceso.
                    Dim BeBodega As New clsBeBodega()
                    BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega,
                                                                 lConnection,
                                                                 lTransaction)

                    If BeBodega Is Nothing Then
                        Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega para el IdBodega: " & pIdBodega)
                    End If

                    Dim pRecEnc As New clsBeTrans_re_enc
                    pRecEnc = Get_Single_By_IdREcepcionEnc_Sin_Det(pIdRecepcionEnc,
                                                                   lConnection,
                                                                   lTransaction)

                    If Not pListStockRec Is Nothing Then

                        If pListStockRec.Count > 0 Then

                            For Each pBeStockRec In pListStockRec

                                If pBeStockRec.IsNew Then

                                    If BeBodega.bloquear_lp_hh Then

                                        Dim vLPExiste As Boolean = False

                                        vLPExiste = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(pBeStockRec.Lic_plate,
                                                                                              pIdBodega,
                                                                                              lConnection,
                                                                                              lTransaction)

                                        If vLPExiste Then
                                            Throw New Exception("ERROR_20220823_1604: La licencia: " & pBeStockRec.Lic_plate & " ya existe.")
                                        End If

                                    End If

                                End If

                                '#GT23012023: validamos que ambos objetos tengan la misma LP y que para LP recepcion detalle no vaya vacia.
                                If pBeStockRec.Lic_plate <> BeTransReDet.Lic_plate Then
                                    If BeTransReDet.Lic_plate.Equals("") Then
                                        BeTransReDet.Lic_plate = pBeStockRec.Lic_plate
                                        Dim vMsgError As String = " lp_vacia en obj recepcion_enc:" & BeTransReDet.IdRecepcionEnc & "y recepcion_det:" & BeTransReDet.IdRecepcionDet
                                        clsLnLog_error_wms.Agregar_Error(vMsgError)
                                    ElseIf pBeStockRec.Lic_plate.Equals("") Then
                                        Dim vMsgError As String = " lp_vacia en obj StockRec:" & pBeStockRec.IdStockRec
                                        clsLnLog_error_wms.Agregar_Error(vMsgError)
                                    End If
                                End If

                            Next

                            Dim vResultInsertEncabezadoRec As Integer = 0
                            'Recepción Encabezado
                            If pRecEnc.IsNew Then

                                vResultInsertEncabezadoRec = Insertar(pRecEnc,
                                                                      lConnection,
                                                                      lTransaction)

                                If vResultInsertEncabezadoRec > 0 Then
                                    CadenaResultado += "Inserté encabezado recepción " & vResultInsertEncabezadoRec
                                Else
                                    Throw New Exception("ERROR_202210051030C: No se pudo insertar el encabezado de la recepción")
                                End If

                            End If

                            If Not BeTransReDet Is Nothing Then

                                vResultadoEliminar = clsLnTrans_re_det.Eliminar_Detalle(pIdOrdenCompraEnc,
                                                                                        BeTransReDet,
                                                                                        lConnection,
                                                                                        lTransaction)


                                If BeTransReDet.Lic_plate = "" Then
                                    Dim vMsgError As String = " GT_26072024_GuardarHH_BOF LP vacia en IdRecepcionEnc:" & BeTransReDet.IdRecepcionEnc & " ,IdRecepciondet: " & BeTransReDet.IdRecepcionDet
                                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                                End If


                                If vResultadoEliminar <> "" Then
                                    CadenaResultado += "Eliminar_Detalle_Recepción: " & vResultadoEliminar
                                End If

                                vResultadoGuardarReDet = clsLnTrans_re_det.Guarda_Trans_re_det(BeTransReDet,
                                                                                               pListStockRec,
                                                                                               lConnection,
                                                                                               lTransaction)

                                If vResultadoGuardarReDet > 0 Then
                                    CadenaResultado += "Guarda_Trans_re_det " & vResultadoGuardarReDet
                                End If

                                '#EJC202402121836: Retornar el MaxIdRecepcionDet (para recepción BOF)
                                IdRecepcionDet = vResultadoGuardarReDet

                                '#EJC20210412:Agregado para actualizar la cantidad recibida por lote.
                                vResultadoGuardaLotes = clsLnTrans_oc_det_lote.Guarda_Trans_re_det_lote(pLotesRec,
                                                                                                        lConnection,
                                                                                                        lTransaction)

                                If vResultadoGuardaLotes > 0 Then
                                    CadenaResultado += "clsLnTrans_oc_det_lote " & vResultadoGuardaLotes
                                End If

                                Guarda_Trans_Re_Det_Parametros = clsLnTrans_re_det_parametros.Guarda_Trans_Re_Det_Parametros(pRecEnc.IdRecepcionEnc,
                                                                                                                             BeTransReDet,
                                                                                                                             pListRecDetParam,
                                                                                                                             lConnection,
                                                                                                                             lTransaction)

                                If Guarda_Trans_Re_Det_Parametros > 0 Then
                                    CadenaResultado += "Guarda_Trans_Re_Det_Parametros " & Guarda_Trans_Re_Det_Parametros
                                End If

                            Else
                                Throw New Exception("ERROR_202210051030E: La lista de RecDet Is Nothing.")
                            End If

                            If pIdOrdenCompraEnc > 0 Then

                                If Not BeTransReDet Is Nothing Then

                                    vResultadoActualizarCantidadRecibidaDI = clsLnTrans_oc_det.Actualiza_Cantidad_Recibida_OC(pIdOrdenCompraEnc,
                                                                                                                              BeTransReDet,
                                                                                                                              lConnection,
                                                                                                                              lTransaction)

                                    If vResultadoActualizarCantidadRecibidaDI > 0 Then
                                        CadenaResultado += "Actualiza_Cantidad_Recibida_OC " & vResultadoActualizarCantidadRecibidaDI
                                    Else
                                        Throw New Exception("ERROR_202210051030G: No se pudo actualizar la cantidad recibida en el documento de ingreso.")
                                    End If

                                End If

                            End If

                            If Not pListStockRec Is Nothing Then

                                If pListStockRec.Count > 0 Then

                                    vResultadoStockRec = clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc,
                                                                                         pIdBodega,
                                                                                         pListStockRec,
                                                                                         lConnection,
                                                                                         lTransaction)

                                    If vResultadoStockRec > 0 Then
                                        CadenaResultado += " Guarda_Stock_Rec " & vResultadoStockRec
                                    Else
                                        Throw New Exception("ERROR_202210051058: No se pudo insertar en stock_rec.")
                                    End If

                                    vResultadoStockSeRec = clsLnStock_se_rec.Guarda_Stock_Se_Rec(pListStockRecSer,
                                                                                                 pListStockRec,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                                    If vResultadoStockSeRec > 0 Then
                                        CadenaResultado += "Guarda_Stock_Se_Rec " & vResultadoStockSeRec
                                    End If

                                Else
                                    Throw New Exception("#ERR20200317A: La lista de stock no tiene registros.")
                                End If

                            Else
                                Throw New Exception("#ERR20200317B: La lista de stock para recepción está vacía.")
                            End If

                            If Not pListProductoPallet Is Nothing Then

                                vResultGuarda_Producto_Pallet = clsLnProducto_pallet.Guarda_Producto_Pallet(pRecEnc.IdRecepcionEnc,
                                                                                                            pListProductoPallet,
                                                                                                            lConnection,
                                                                                                            lTransaction)

                                If vResultGuarda_Producto_Pallet > 0 Then
                                    CadenaResultado += "Guarda_Producto_Pallet " & vResultGuarda_Producto_Pallet
                                End If

                            End If

                            Dim BeStock As New clsBeStock()

                            If pRecEnc.Habilitar_Stock Then

                                Dim pBeINavBarraPallet As New clsBeI_nav_barras_pallet
                                Dim BeVWStockRec As New clsBeVW_stock_res
                                Dim pBeTransReDet As New clsBeTrans_re_det

                                If Not pListStockRec Is Nothing Then

                                    If pListStockRec.Count > 0 Then

                                        For Each pBeStockRec As clsBeStock_rec In pListStockRec

                                            BeStock = New clsBeStock
                                            pBeStockRec.IdBodega = pIdBodega
                                            vResultadoInsertMovimientos = 0
                                            pBeStockRec.Fecha_Ingreso = Now
                                            pBeStockRec.Fec_agr = Now
                                            pBeStockRec.Fec_mod = Now
                                            clsPublic.CopyObject(pBeStockRec, BeStock)

                                            BeVWStockRec = New clsBeVW_stock_res
                                            pBeTransReDet = New clsBeTrans_re_det

                                            pBeTransReDet = clsLnTrans_re_det.Get_Recepcion_By_IdRecepcionEnc(BeStock.IdRecepcionEnc,
                                                                                                              BeStock.IdRecepcionDet,
                                                                                                              lConnection,
                                                                                                              lTransaction)

                                            '#EJC20230222349: Agregué validación para evitar que se vuelva a insertar un registro duplicado en stock.
                                            BeVWStockRec = clsLnStock.Get_Single_By_BeRecepcionDet(pBeTransReDet,
                                                                                                  pIdBodega,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                                            If BeVWStockRec Is Nothing Then

                                                vResultadoInsertMovimientos = clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                                                                                    pIdBodega,
                                                                                                                                    pIdUsuario,
                                                                                                                                    pBeStockRec,
                                                                                                                                    lConnection,
                                                                                                                                    lTransaction,
                                                                                                                                    0)

                                                If vResultadoInsertMovimientos > 0 Then

                                                    CadenaResultado += "Insertar_Movimientos_Recepcion: " & vResultadoInsertMovimientos

                                                    BeStock.IdStock = clsLnStock.MaxID(lConnection, lTransaction) + 1

                                                    vResultadoInsertStock = clsLnStock.Insertar(BeStock,
                                                                                                lConnection,
                                                                                                lTransaction)

                                                    If vResultadoInsertStock > 0 Then
                                                        '#GT31012023 confirma stock disponible y el idstock para generar historico.
                                                        Stock_Disponible = True
                                                        pIdStock = 0
                                                        pIdStock = BeStock.IdStock

                                                        CadenaResultado += "Inserta_Stock: " & vResultadoInsertStock

                                                        vResultadoStockParametroRec = clsLnStock_parametro.Insertar_Stock_Parametro_Recepcion(pBeStockRec,
                                                                                                                                          BeStock.IdStock,
                                                                                                                                          lConnection,
                                                                                                                                          lTransaction)

                                                        If vResultadoStockParametroRec > 0 Then
                                                            CadenaResultado += "Insertar_Stock_Parametro_Recepcion " & vResultadoStockParametroRec
                                                        End If


                                                        vResultadoInsertar_Stock_Serializado_Recepcion = clsLnStock_se.Insertar_Stock_Serializado_Recepcion(pBeStockRec,
                                                                                                                                                        BeStock.IdStock,
                                                                                                                                                        lConnection,
                                                                                                                                                        lTransaction)

                                                        If vResultadoInsertar_Stock_Serializado_Recepcion > 0 Then
                                                            CadenaResultado += "Insertar_Stock_Serializado_Recepcion: " & vResultadoInsertar_Stock_Serializado_Recepcion
                                                        End If

                                                    Else
                                                        '#GT21102022_1600: sino inserta stock se lanza excepción
                                                        Throw New Exception("ERROR_202210211600: No se pudo insertar el stock.")
                                                    End If

                                                Else
                                                    Throw New Exception("ERROR_202210051111: No se pudo insertar el movimiento.")
                                                End If

                                                '#EJC20190329_0538PM: Marcar el pallet como recibido.
                                                If pBeStockRec.Lic_plate <> "" Then

                                                    pBeINavBarraPallet.Recibido = True
                                                    pBeINavBarraPallet.IdRecepcion = pRecEnc.IdRecepcionEnc
                                                    pBeINavBarraPallet.Codigo_barra = pBeStockRec.Lic_plate
                                                    pBeINavBarraPallet.Fecha_Ingreso = Now
                                                    pBeINavBarraPallet.Fecha_Agregado = Now
                                                    pBeINavBarraPallet.Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega,
                                                                                                                           lConnection,
                                                                                                                           lTransaction)

                                                    If Not pBeINavBarraPallet.Bodega_Destino Is Nothing Then
                                                        CadenaResultado += "Get_Codigo_By_IdBodega: " & pBeINavBarraPallet.Bodega_Destino
                                                    Else
                                                        Throw New Exception("ERROR_202210051121: No se obtuvo el código de la bodega destino para el IdBodega: " & pIdBodega)
                                                    End If

                                                    vResultadoActualiza_Estado_Barras_Pallet = clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet(pBeINavBarraPallet,
                                                                                                                                                       lConnection,
                                                                                                                                                       lTransaction)

                                                    If vResultadoActualiza_Estado_Barras_Pallet > 0 Then
                                                        CadenaResultado += "Actualiza_Estado_Barras_Pallet: " & vResultadoActualiza_Estado_Barras_Pallet
                                                    End If

                                                End If

                                            End If

                                        Next

                                    Else
                                        Throw New Exception("ERROR_20220914_1048: Se encontró una inconsistencia al procesar el registro de ingreso el count() de la lista de stock es 0.")
                                    End If

                                Else
                                    Throw New Exception("ERROR_20220914_1047: Se encontró una inconsistencia al procesar el registro de ingreso la lista de stock está vacía.")
                                End If

                                If Not BeTransReDet Is Nothing Then

                                    If BeTransReDet.IsNew Then

                                        CadenaResultado += "Inserta transacciones out"

                                        Dim vResultado As String = clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                                                                         pIdBodega,
                                                                                                                         IdTipoDocumento,
                                                                                                                         BeTransReDet,
                                                                                                                         pIdOrdenCompraEnc,
                                                                                                                         pIdUsuario,
                                                                                                                         False,
                                                                                                                         lConnection,
                                                                                                                         lTransaction)

                                        CadenaResultado += "Insertar_Ingreso_Parcial: " & vResultado

                                        Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                                        BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                                        BeLoteNum.IdProductoBodega = BeTransReDet.IdProductoBodega
                                        BeLoteNum.IdRecepcionEnc = pRecEnc.IdRecepcionEnc
                                        BeLoteNum.Codigo = pBeINavBarraPallet.Codigo
                                        BeLoteNum.Lote = pBeINavBarraPallet.Lote
                                        BeLoteNum.Lote_Numerico = pBeINavBarraPallet.Lote_Numerico
                                        BeLoteNum.Cantidad = BeTransReDet.cantidad_recibida
                                        BeLoteNum.FechaIngreso = Now
                                        clsLnTrans_re_det_lote_num.Insertar(BeLoteNum,
                                                                            lConnection,
                                                                            lTransaction)

                                    Else
                                        Throw New Exception("ERROR_202404262253: El detalle de la recepción es nothing, se ha detenido la transacción para evitar inconsistencias.")
                                    End If

                                    Dim vPosiciones As Integer = 0

                                    If BeTransReDet.Pallet_No_Estandar Then

                                        Dim BeStockDet As New clsBeStock_det
                                        BeStockDet.IdStock = BeStock.IdStock
                                        BeStockDet.Posiciones = BeTransReDet.Posiciones

                                        If clsLnStock_det.Get_Single_By_IdStock(BeStockDet, lConnection, lTransaction) Then
                                            BeStockDet.Posiciones = vPosiciones
                                            clsLnStock_det.Actualizar(BeStockDet, lConnection, lTransaction)
                                        Else
                                            clsLnStock_det.Insertar(BeStockDet, lConnection, lTransaction)
                                        End If

                                    End If

                                End If

                            End If

                            GuardarHH_BOF = True

                            CadenaResultado += " Terminé la recepción " & pRecEnc.IdRecepcionEnc.ToString

                        Else
                            Throw New Exception("ERROR_202210051030A: El count de la lista de stock es 0.")
                        End If

                    Else
                        Throw New Exception("ERROR_202210051030B: La lista de stock Is Nothing.")
                    End If

                End If

            Else
                Throw New Exception("ERROR_DE_PROCESO_202302221004: La recepción fue previamente finalizada.")
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", ex.Message, CadenaResultado))
        End Try

    End Function

    Public Shared Function Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC(ByVal IdOrdenCompraEnc As Integer,
                                                                           ByVal IdPropietario As Integer,
                                                                           ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                                           ByVal BeTransReDet As clsBeTrans_re_det) As Boolean

        Dim lProductosConDiferencia As New List(Of String)
        '#GT12022024: inferimos que todo esta bien, pero si entra en alguna excepcion se retorna false
        Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = True

        Try

            Dim vReglaProp As New clsBePropietario_reglas_enc
            Dim ListaRegla As New List(Of clsBePropietario_reglas_enc)

            ListaRegla = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(IdPropietario)


            Using BeReglaRec As New clsBeReglas_RecepcionRes()

                BeReglaRec.PermitirProductoFaltantes = False
                BeReglaRec.PermitirProductosAdicionales = False
                BeReglaRec.PermitirCantidadFaltantePorProducto = False
                BeReglaRec.PermitirCantidadSobrantePorProducto = False
                BeReglaRec.PermitirCostoDiferentePorProducto = False
                BeReglaRec.PermitirPesoMenor = False
                BeReglaRec.PermitirPesoMayor = False

                '#EJC202107141551_RP: Por defecto, una regla de recepción es válida y por excepción se validan(activan/desactivan) las reglas específicas configuradas.
                vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 1) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirProductoFaltantes = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirProductoFaltantes = True
                vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 2) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirProductosAdicionales = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirProductosAdicionales = True
                vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 3) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCantidadFaltantePorProducto = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirCantidadFaltantePorProducto = True
                vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 4) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCantidadSobrantePorProducto = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirProductoFaltantes = True
                vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 5) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCostoDiferentePorProducto = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirCostoDiferentePorProducto = True
                vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 6) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirPesoMenor = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirPesoMenor = True
                vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 7) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirPesoMayor = vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirPesoMayor = True

                Dim vMensaje As String = ""

                'Si hay reglas definidas por propietario
                If ListaRegla.Count > 0 Then

                    'Si la recepción fue con OC.
                    If IdOrdenCompraEnc > 0 Then

                        'Obtener detalle de la OC.
                        'Dim BeTransOCEnc As clsBeTrans_oc_enc = clsLnTrans_oc_enc.GetSingle(IdOrdenCompraEnc)

                        ' Validamos que traiga datos 
                        If BeTransOCEnc IsNot Nothing Then

                            'Dim lRecDetByProd As clsBeTrans_re_det
                            Dim vPresRec As New clsBeProducto_Presentacion
                            Dim vCantidadRecibidaUmBas As Double = 0
                            Dim vPesoRecibido As Double = 0
                            Dim vCantidadOCUMBas As Double = 0
                            Dim vDiferenciaCantUmBas As Double = 0
                            Dim vDiferenciaCosto As Double = 0
                            Dim vDiferenciaPeso As Double = 0
                            Dim vCostoRec As Double = 0

                            If BeTransOCEnc.DetalleOC IsNot Nothing Then

                                '#GT07022024: aplicamos regla de propietario por linea y no por el global de la OC
                                'Recorremos el detalle de la Orden de Compra
                                For Each oc_det As clsBeTrans_oc_det In BeTransOCEnc.DetalleOC.Where(Function(x) x.No_Linea = BeTransReDet.No_Linea AndAlso x.Codigo_Producto = BeTransReDet.Codigo_Producto)

                                    If Not oc_det.IdPresentacion = 0 Then
                                        vPresRec.IdPresentacion = oc_det.IdPresentacion
                                        vPresRec = clsLnProducto_presentacion.GetSingle(vPresRec.IdPresentacion)

                                        '#EJC20210630:Mostrar la cantidad en presentación.
                                        vCantidadOCUMBas = oc_det.Cantidad
                                    Else
                                        vCantidadOCUMBas = oc_det.Cantidad
                                    End If

                                    vCantidadRecibidaUmBas = 0

                                    If Not BeTransReDet Is Nothing Then

                                        '#GT06022024: tiene presentacion
                                        If Not BeTransReDet.IdPresentacion = 0 Then

                                            vPresRec.IdPresentacion = BeTransReDet.IdPresentacion
                                            vPresRec = clsLnProducto_presentacion.GetSingle(vPresRec.IdPresentacion)

                                            If Not vPresRec.EsPallet Then
                                                vCantidadRecibidaUmBas += BeTransReDet.cantidad_recibida + oc_det.Cantidad_recibida
                                            Else
                                                vCantidadRecibidaUmBas += BeTransReDet.cantidad_recibida * vPresRec.Factor * vPresRec.CajasPorCama * vPresRec.CamasPorTarima + oc_det.Cantidad_recibida
                                            End If

                                        Else
                                            vCantidadRecibidaUmBas += BeTransReDet.cantidad_recibida + oc_det.Cantidad_recibida
                                        End If

                                        If vDiferenciaCosto = 0 Then
                                            vCostoRec = BeTransReDet.Costo
                                            vDiferenciaCosto = Math.Round(oc_det.Costo - BeTransReDet.Costo, 2)
                                        End If

                                        vPesoRecibido += BeTransReDet.Peso

                                    Else

                                        'No se recepcionó ese producto.
                                        If Not BeReglaRec.PermitirProductoFaltantes Then

                                            '#CKFK 20210708
                                            vMensaje = String.Format("La regla de recepción configurada " &
                                        " para el propietario no permite faltante de producto. " &
                                        " Producto en OC.: {0} Cantidad O.C.: {1} " &
                                        " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", oc_det.Producto.Codigo, oc_det.Producto.Nombre), oc_det.Cantidad, oc_det.Cantidad_recibida)

                                            'Throw New Exception(vMensaje)
                                            Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = False
                                            XtraMessageBox.Show(vMensaje, "Regla de recepción. ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                        End If

                                    End If

                                    Dim vCAntidadOriginalUMBas As Double = vCantidadOCUMBas

                                    If Not vPresRec Is Nothing Then

                                        If Not vPresRec.Factor = 0 Then
                                            vCAntidadOriginalUMBas = vCantidadOCUMBas * vPresRec.Factor
                                        Else
                                            vCAntidadOriginalUMBas = vCantidadOCUMBas
                                        End If

                                    End If

                                    vDiferenciaCantUmBas = vCAntidadOriginalUMBas - vCantidadRecibidaUmBas

                                    vDiferenciaPeso = oc_det.Peso - vPesoRecibido

                                    If vDiferenciaCantUmBas <> 0 Then
                                        lProductosConDiferencia.Add(oc_det.Producto.IdProducto)
                                    End If

                                    Select Case vDiferenciaCantUmBas

                                        Case Is = 0
                                                'No hay diferencia.

                                        Case Is > 0

                                            If Not BeReglaRec.PermitirCantidadFaltantePorProducto Then

                                                vMensaje = String.Format("La regla de recepción configurada " &
                                                                    " para el propietario no permite recibir menos producto " &
                                                                    " que el indicado por la orden de compra " &
                                                                    " Producto: {0} Cantidad O.C.: {1} " &
                                                                    " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", oc_det.Producto.Codigo, oc_det.Producto.Nombre), vCantidadOCUMBas, vCantidadRecibidaUmBas)

                                                'Throw New Exception(vMensaje)
                                                Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = False
                                                XtraMessageBox.Show(vMensaje, "Regla de recepción. ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                ' Se recibió de menos que lo indicado por la O.C.

                                            End If

                                        Case Is < 0

                                            If Not BeReglaRec.PermitirCantidadSobrantePorProducto Then

                                                vMensaje = String.Format("La regla de recepción configurada " &
                                                                    " para el propietario no permite recibir más producto " &
                                                                    " que el indicado por la orden de compra " &
                                                                    " Producto:{0} Cantidad O.C.: {1} " &
                                                                    " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", oc_det.Producto.Codigo, oc_det.Producto.Nombre), vCantidadOCUMBas, vCantidadRecibidaUmBas)


                                                'Throw New Exception(vMensaje)
                                                Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = False
                                                XtraMessageBox.Show(vMensaje, "Regla de recepción. ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                ' Se recibió más que lo indicado por la O.C.

                                            End If

                                        Case Else

                                            Exit Select

                                    End Select

                                    Select Case vDiferenciaCosto

                                        Case Is = 0
                                                'No hay diferencia.

                                        Case Is <> 0

                                            'El costo de la línea en la O.C. es diferente que el de la recepción.
                                            If Not BeReglaRec.PermitirCostoDiferentePorProducto Then

                                                vMensaje = String.Format("La regla de recepción configurada " &
                                                               " para el propietario no permite recibir productos " &
                                                               " con costo diferente que el indicado por la orden de compra " &
                                                               " Producto: {0} Costo en O.C.: {1} " &
                                                               " Costo en Recepción.: {2}", String.Format("""{0} - {1}""", oc_det.Producto.Codigo, oc_det.Producto.Nombre), oc_det.Costo, vCostoRec)

                                                'Throw New Exception(vMensaje)
                                                Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = False
                                                XtraMessageBox.Show(vMensaje, "Regla de recepción. ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                            End If

                                        Case Else
                                            Exit Select

                                    End Select

                                Next oc_det


                            End If


                        End If

                    End If

                End If

            End Using



        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Recepcion_Activa_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer) As Integer

        Get_Recepcion_Activa_By_IdOrdenCompraEnc = 0

        Try

            Dim vSQL As String = "SELECT top(1) IdRecepcionEnc 
                                  FROM VW_RecepcionesEncOC
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc And Estado <> 'Anulado' 
                                  ORDER BY IdRecepcionEnc DESC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Recepcion_Activa_By_IdOrdenCompraEnc = CInt(lReturnValue)
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

    Public Shared Function Generar_Tarea_Recepcion_By_OrdenCompraEnc_For_SAP(ByRef BeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                                             ByVal BeMI3Config As clsBeI_nav_config_enc,
                                                                             ByRef OutBeRecepcionEnc As clsBeTrans_re_enc,
                                                                             ByVal CodigoBodegaAreaSAP As String,
                                                                             ByRef lConnection As SqlConnection,
                                                                             ByRef lTransaction As SqlTransaction) As Integer


        Generar_Tarea_Recepcion_By_OrdenCompraEnc_For_SAP = 0 : OutBeRecepcionEnc = Nothing

        Dim RegistrosAfectados As Integer = 0
        Dim lblResult As New RichTextBox
        Dim OrdenCompraReOc As New clsBeTrans_re_oc
        Dim BeTareaHH As New clsBeTarea_hh
        Dim BeRecepcionEnc As New clsBeTrans_re_enc
        Dim IdBodegaDestino As Integer = 0
        Dim IdPropietario As Integer = 0
        Dim TiempoMedioIngresoMinutos As Double
        Dim BeBodega As New clsBeBodega

        Try

            If Not clsLnTrans_re_oc.Existe_Documento_By_IdOrdenCompraEnc(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction) Then

                IdBodegaDestino = BeOrdenCompraEnc.IdBodega

                BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeOrdenCompraEnc.IdBodega, lConnection, lTransaction)

                BeRecepcionEnc.IsNew = True
                BeRecepcionEnc.IdRecepcionEnc = MaxID(lConnection, lTransaction) + 1
                BeRecepcionEnc.PropietarioBodega = New clsBePropietario_bodega
                BeRecepcionEnc.PropietarioBodega.IdBodega = BeOrdenCompraEnc.IdBodega
                BeRecepcionEnc.IdBodega = BeOrdenCompraEnc.IdBodega
                BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega
                BeRecepcionEnc.User_agr = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fec_agr = Now
                BeRecepcionEnc.Activo = True
                BeRecepcionEnc.Estado = "Nuevo"

                BeRecepcionEnc.OrdenCompraRec = New clsBeTrans_re_oc
                BeRecepcionEnc.OrdenCompraRec.IsNew = True
                BeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc

                If BeRecepcionEnc.PropietarioBodega Is Nothing OrElse BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega <= 0 Then
                    Throw New Exception("Propietario no válido al crear la recepción")
                End If

                BeRecepcionEnc.IdTipoTransaccion = BeBodega.IdTipoTransaccion

                BeRecepcionEnc.IdMuelle = clsLnBodega_muelles.Get_IdMuelle_Default_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdMuelle = 0 Then
                    Throw New Exception("No existe ningún muelle por defecto para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                BeRecepcionEnc.IdUbicacionRecepcion = clsLnBodega_area.Get_IdUbicacion_Recepcion_By_Codigo_Area(CodigoBodegaAreaSAP, lConnection, lTransaction)
                BeRecepcionEnc.IdEstado_Defecto_Recepcion = clsLnProducto_estado.Get_IdEstado_By_Codigo_Area(CodigoBodegaAreaSAP, lConnection, lTransaction)

                If BeRecepcionEnc.IdUbicacionRecepcion = 0 Then
                    Throw New Exception("No está configurada la ubicación por defecto para recepción para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                TiempoMedioIngresoMinutos = clsLnTarea_hh.Get_Tiempo_Medio_Tarea_Ingreso_Minutos(lConnection, lTransaction)

                BeRecepcionEnc.Fecha_recepcion = Now.Date
                BeRecepcionEnc.Hora_ini_pc = Now
                BeRecepcionEnc.Hora_fin_pc = Now.AddMinutes(TiempoMedioIngresoMinutos)
                BeRecepcionEnc.Muestra_precio = False
                BeRecepcionEnc.Fec_mod = Now
                BeRecepcionEnc.User_mod = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fecha_tarea = Now
                BeRecepcionEnc.Tomar_fotos = False
                BeRecepcionEnc.Escanear_rec_ubic = False
                BeRecepcionEnc.Para_por_codigo = False
                BeRecepcionEnc.Observacion = "FROMMI3"
                BeRecepcionEnc.IdPiloto = 0
                BeRecepcionEnc.IdVehiculo = 0
                BeRecepcionEnc.Habilitar_Stock = True
                BeRecepcionEnc.IdBodega = IdBodegaDestino

                OrdenCompraReOc.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                OrdenCompraReOc.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc
                OrdenCompraReOc.IsNew = True
                OrdenCompraReOc.No_docto = BeOrdenCompraEnc.Referencia
                OrdenCompraReOc.OC = BeOrdenCompraEnc
                OrdenCompraReOc.Recepcion_ciega = False
                OrdenCompraReOc.Recepcion_manual = False
                OrdenCompraReOc.User_agr = BeMI3Config.IdUsuario

                BeRecepcionEnc.OrdenCompraRec = OrdenCompraReOc

                Dim pListOperadorRecepcion As New List(Of clsBeTrans_re_op)
                Dim pListOperadorBodega As New List(Of clsBeOperador_bodega)

                pListOperadorBodega = clsLnOperador_bodega.Get_All_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If Not pListOperadorBodega Is Nothing Then

                    Dim BeTransReOp As New clsBeTrans_re_op()

                    For Each Op In pListOperadorBodega

                        BeTransReOp = New clsBeTrans_re_op()
                        BeTransReOp.IdOperadorBodega = Op.IdOperadorBodega
                        BeTransReOp.User_agr = BeMI3Config.IdUsuario
                        BeTransReOp.Fec_agr = Now
                        BeTransReOp.User_mod = BeMI3Config.IdUsuario
                        BeTransReOp.Fec_mod = Now
                        BeTransReOp.IsNew = True
                        BeTransReOp.UsaHH = True
                        pListOperadorRecepcion.Add(BeTransReOp)

                    Next

                End If

                Guardar(BeOrdenCompraEnc.IdBodega,
                        Nothing,
                        BeRecepcionEnc,
                        BeRecepcionEnc.OrdenCompraRec,
                        Nothing,
                        Nothing,
                        pListOperadorRecepcion,
                        Nothing,
                        Nothing,
                        Nothing,
                        Nothing,
                        Nothing,
                        lConnection,
                        lTransaction)

                Generar_Tarea_Recepcion_By_OrdenCompraEnc_For_SAP = 1

                OutBeRecepcionEnc = BeRecepcionEnc



            End If

        Catch ex As Exception
            '#MECR23092025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdBodega:=BeRecepcionEnc.IdBodega,
                                                 pIdUsuarioAgr:=BeRecepcionEnc.User_agr,
                                                 pIdRecEnc:=BeRecepcionEnc.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Sub Actualizar_Piloto_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                          ByVal pIdPiloto As Integer,
                                                          Optional ByVal pConnection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lCommand As New SqlCommand

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = ("UPDATE trans_re_enc SET 
                                 IdPiloto=@IdPiloto 
                                 WHERE IdRecepcionEnc=@IdRecepcionEnc ")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(sp, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                lCommand = New SqlCommand(sp, lConnection, lTransaction)
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPiloto", pIdPiloto))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))
            lCommand.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso Not lTransaction Is Nothing Then
                lTransaction.Rollback()
            End If
        Finally
            lCommand.Dispose()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Sub Actualizar_Vehiculo_By_IdRecepcionEnc(idRecepcionEnc As Integer,
                                                            idVehiculo As Integer,
                                                            Optional pConnection As SqlConnection = Nothing,
                                                            Optional pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lCommand As New SqlCommand

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = ("UPDATE trans_re_enc SET 
                                 IdVehiculo=@IdVehiculo 
                                 WHERE IdRecepcionEnc=@IdRecepcionEnc ")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(sp, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                lCommand = New SqlCommand(sp, lConnection, lTransaction)
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdVehiculo", idVehiculo))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", idRecepcionEnc))

            lCommand.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            lCommand.Dispose()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Friend Shared Function Get_No_Contenedor_By_IdRecepcionEnc(IdRecepcionEnc As Integer) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_No_Contenedor_By_IdRecepcionEnc = ""

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT No_Contenedor FROM Trans_re_enc Where(IdRecepcionEnc = @IdRecepcionEnc) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IdRecepcionEnc))
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then

                Dim dr As DataRow
                dr = dt.Rows(0)

                Get_No_Contenedor_By_IdRecepcionEnc = IIf(IsDBNull(dr.Item("No_Contenedor")), "", dr.Item("No_Contenedor"))

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
    Shared Function get_cantidad_recepciones_activas(ByVal idBodega As Integer, ByVal date1 As Date, ByVal date2 As Date) As Integer

        get_cantidad_recepciones_activas = 0

        Try

            Dim vSQL As String = "SELECT count(distinct Código) Cant_Rec
                                  FROM VW_Recepcion
                                  WHERE fecha between @FechaDesde and @FechaHasta and estado <> 'Anulado' 
                                  AND IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", idBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaDesde", date1)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaHasta", date2)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            get_cantidad_recepciones_activas = IIf(IsDBNull(lRow("Cant_Rec")), 0, lRow("Cant_Rec"))

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

    Public Shared Function Generar_Reporte_Tiempos_Recepcion(ByVal IdBodega As Integer,
                                                             ByVal IdPropietarioBodega As Integer,
                                                             ByVal FechaDesde As Date,
                                                             ByVal FechaHasta As Date) As DataTable

        ' Crear el DataTable para almacenar el reporte
        Dim dtReporte As New DataTable("ReporteTiemposRecepcion")
        dtReporte.Columns.Add("FechaRecepcion", GetType(Date))
        dtReporte.Columns.Add("NumeroRecepciones", GetType(Integer))
        dtReporte.Columns.Add("TiempoPromedioMinutos", GetType(Double))
        dtReporte.Columns.Add("TiempoMinimoMinutos", GetType(Integer))
        dtReporte.Columns.Add("TiempoMaximoMinutos", GetType(Integer))

        ' Consulta SQL con parámetros
        Dim query As String = "
                                WITH TiemposRecepcion AS (
                            SELECT 
                                trans_re_enc.IdRecepcionEnc,
                                trans_re_enc.IdPropietarioBodega,
                                trans_re_enc.IdBodega,
                                CONVERT(DATE, MIN(trans_re_det.fec_agr)) AS FechaRecepcion,
                                DATEDIFF(MINUTE, MIN(trans_re_det.fec_agr), MAX(trans_re_det.fec_agr)) AS TiempoRecepcionMinutos
                            FROM 
                                trans_re_det 
                            JOIN 
                                trans_re_enc ON trans_re_det.IdRecepcionEnc = trans_re_enc.IdRecepcionEnc
                            WHERE
                                trans_re_enc.IdBodega = @IdBodega
                                AND trans_re_enc.IdPropietarioBodega = @IdPropietarioBodega
                                AND CONVERT(DATE, trans_re_det.fec_agr) BETWEEN @FechaDesde AND @FechaHasta
                            GROUP BY 
                                trans_re_enc.IdRecepcionEnc,
                                trans_re_enc.IdPropietarioBodega,
                                trans_re_enc.IdBodega,
                                CONVERT(DATE, trans_re_det.fec_agr)
                        )
                        SELECT 
                            FechaRecepcion,
                            COUNT(IdRecepcionEnc) AS NumeroRecepciones,
                            AVG(TiempoRecepcionMinutos) AS TiempoPromedioMinutos,
                            MIN(TiempoRecepcionMinutos) AS TiempoMinimoMinutos,
                            MAX(TiempoRecepcionMinutos) AS TiempoMaximoMinutos
                        FROM 
                            TiemposRecepcion
                        GROUP BY 
                            FechaRecepcion
                        ORDER BY 
                            FechaRecepcion; "

        ' Ejecutar la consulta y procesar los resultados
        Using connection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Using command As New SqlCommand(query, connection)
                ' Agregar los parámetros
                command.Parameters.AddWithValue("@IdBodega", IdBodega)
                command.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                command.Parameters.AddWithValue("@FechaDesde", FechaDesde)
                command.Parameters.AddWithValue("@FechaHasta", FechaHasta)

                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    dtReporte.Load(reader)
                End Using
            End Using
        End Using

        Return dtReporte

    End Function

    Public Shared Function Generar_Tarea_Recepcion_By_OrdenCompraEnc_And_Pedido(ByRef BeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                                                ByRef Resultado As String,
                                                                                ByVal CrearRecepcionPorDefecto As Boolean,
                                                                                ByVal BeMI3Config As clsBeI_nav_config_enc,
                                                                                ByRef OutBeRecepcionEnc As clsBeTrans_re_enc,
                                                                                ByRef BePedidoEnc As clsBeTrans_pe_enc,
                                                                                ByRef lConnection As SqlConnection,
                                                                                ByRef lTransaction As SqlTransaction) As Integer


        Generar_Tarea_Recepcion_By_OrdenCompraEnc_And_Pedido = 0 : OutBeRecepcionEnc = Nothing

        Dim RegistrosAfectados As Integer = 0
        Dim lblResult As New RichTextBox
        Dim OrdenCompraReOc As New clsBeTrans_re_oc
        Dim BeTareaHH As New clsBeTarea_hh
        Dim BeRecepcionEnc As New clsBeTrans_re_enc
        Dim IdBodegaDestino As Integer = 0
        Dim IdPropietario As Integer = 0
        Dim TiempoMedioIngresoMinutos As Double
        Dim lBeRecDet As New List(Of clsBeTrans_re_det)
        Dim lBeStockRec As New List(Of clsBeStock_rec)

        Try

            If Not clsLnTrans_re_oc.Existe_Documento_By_IdOrdenCompraEnc(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction) Then

                IdBodegaDestino = BeOrdenCompraEnc.IdBodega

                BeRecepcionEnc.IsNew = True
                BeRecepcionEnc.IdRecepcionEnc = MaxID(lConnection, lTransaction) + 1
                BeRecepcionEnc.PropietarioBodega = New clsBePropietario_bodega
                BeRecepcionEnc.PropietarioBodega.IdBodega = BeOrdenCompraEnc.IdBodega
                '#EJC20210715: IdBodega se comparte a la tarea de recepción en generación automática de tarea..
                BeRecepcionEnc.IdBodega = BeOrdenCompraEnc.IdBodega
                BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega
                BeRecepcionEnc.User_agr = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fec_agr = Now
                BeRecepcionEnc.Activo = True
                BeRecepcionEnc.Estado = "Nuevo"

                BeRecepcionEnc.OrdenCompraRec = New clsBeTrans_re_oc
                BeRecepcionEnc.OrdenCompraRec.IsNew = True
                '#EJC20200120: Lo hace más tarde en el guardar...
                'BeRecepcionEnc.OrdenCompraRec.IdRecepcionOc = clsLnTrans_re_oc.MaxID(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction) + 1
                BeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc

                If BeRecepcionEnc.PropietarioBodega Is Nothing OrElse BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega <= 0 Then
                    Throw New Exception("Propietario no válido al crear la recepción")
                End If

                'Ingreso con referenci a orden de compra para procesar en HH
                BeRecepcionEnc.IdTipoTransaccion = "HCOC00"

                BeRecepcionEnc.IdMuelle = clsLnBodega_muelles.Get_IdMuelle_Default_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdMuelle = 0 Then
                    Throw New Exception("No existe ningún muelle por defecto para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                BeRecepcionEnc.IdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdUbicacionRecepcion = 0 Then
                    Throw New Exception("No está configurada la ubicación por defecto para recepción para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                TiempoMedioIngresoMinutos = clsLnTarea_hh.Get_Tiempo_Medio_Tarea_Ingreso_Minutos(lConnection, lTransaction)

                BeRecepcionEnc.Fecha_recepcion = Now.Date
                BeRecepcionEnc.Hora_ini_pc = Now
                BeRecepcionEnc.Hora_fin_pc = Now.AddMinutes(TiempoMedioIngresoMinutos)
                BeRecepcionEnc.Muestra_precio = False
                BeRecepcionEnc.Fec_mod = Now
                BeRecepcionEnc.User_mod = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fecha_tarea = Now
                BeRecepcionEnc.Tomar_fotos = False
                BeRecepcionEnc.Escanear_rec_ubic = False
                BeRecepcionEnc.Para_por_codigo = False
                BeRecepcionEnc.Observacion = "FROMMI3"
                BeRecepcionEnc.IdPiloto = 0
                BeRecepcionEnc.IdVehiculo = 0
                BeRecepcionEnc.Habilitar_Stock = False
                BeRecepcionEnc.IdBodega = IdBodegaDestino

                OrdenCompraReOc.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                OrdenCompraReOc.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc
                OrdenCompraReOc.IsNew = True
                OrdenCompraReOc.No_docto = BeOrdenCompraEnc.Referencia
                OrdenCompraReOc.OC = BeOrdenCompraEnc
                OrdenCompraReOc.Recepcion_ciega = False
                OrdenCompraReOc.Recepcion_manual = False
                OrdenCompraReOc.User_agr = BeMI3Config.IdUsuario

                BeRecepcionEnc.OrdenCompraRec = OrdenCompraReOc


                Dim pListOperadorRecepcion As New List(Of clsBeTrans_re_op)
                Dim pListOperadorBodega As New List(Of clsBeOperador_bodega)

                '#EJC20190711: Broadcast a todos los operadores de la bodega con la tarea.
                pListOperadorBodega = clsLnOperador_bodega.Get_All_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If Not pListOperadorBodega Is Nothing Then

                    Dim BeTransReOp As New clsBeTrans_re_op()

                    For Each Op In pListOperadorBodega

                        BeTransReOp = New clsBeTrans_re_op()
                        BeTransReOp.IdOperadorBodega = Op.IdOperadorBodega
                        BeTransReOp.User_agr = BeMI3Config.IdUsuario
                        BeTransReOp.Fec_agr = Now
                        BeTransReOp.User_mod = BeMI3Config.IdUsuario
                        BeTransReOp.Fec_mod = Now
                        BeTransReOp.IsNew = True
                        BeTransReOp.UsaHH = True
                        pListOperadorRecepcion.Add(BeTransReOp)

                    Next

                End If

                BeTareaHH = New clsBeTarea_hh
                BeTareaHH.IdPropietario = clsLnPropietarios.Get_IdPropietario(IdBodegaDestino, BeOrdenCompraEnc.IdPropietarioBodega)
                BeTareaHH.IdBodega = IdBodegaDestino
                BeTareaHH.IdMuelle = BeRecepcionEnc.IdMuelle
                BeTareaHH.IdEstado = 1
                BeTareaHH.IdPrioridad = 1
                BeTareaHH.IdTipoTarea = 1
                BeTareaHH.IdTransaccion = BeRecepcionEnc.IdRecepcionEnc
                BeTareaHH.Tipo = 0
                BeTareaHH.FechaInicio = Now
                BeTareaHH.FechaFin = Now.AddHours(2)
                BeTareaHH.DiaCompleto = False
                BeTareaHH.Descripcion = ""
                BeTareaHH.CreaTarea = True
                BeTareaHH.IsNew = True

                Select Case BeRecepcionEnc.IdTipoTransaccion.ToString()
                    Case "HSOC00"
                        BeTareaHH.Asunto = "Ingreso sin Orden de Compra "
                    Case "HSOD00"
                        BeTareaHH.Asunto = "Ingreso de Devolución sin referencia"
                    Case "HCOC00"
                        BeTareaHH.Asunto = "Ingreso con Orden de Compra"
                    Case "HCOD00"
                        BeTareaHH.Asunto = "Devolución de Pedido"
                    Case "HHSR00"
                        BeTareaHH.Asunto = "Ingreso sin referencia"
                    Case "PICH000"
                        BeTareaHH.Asunto = "Pre-ingreso con HH"
                    Case Else
                        Exit Select
                End Select

                Dim i As Integer = 0

                '#EJC20190719: Se verifica si en la lista de pedidos del despacho hay pedidos para sucursales WMS.
                For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle

                    If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                        Dim BeTransReDet As New clsBeTrans_re_det
                        Dim BeINavBarraPallet As New clsBeI_nav_barras_pallet
                        Dim BeINavBarraPalletOriginal As New clsBeI_nav_barras_pallet
                        Dim BeStock As New clsBeStock
                        Dim BeStockRec As New clsBeStock_rec
                        Dim BeProductoPresentacion As New clsBeProducto_Presentacion
                        Dim vFactor As Double = 1

                        For Each PickingUbic In BePedidoDet.ListaPickingUbic

                            i += 1
                            BeTransReDet = New clsBeTrans_re_det()
                            BeTransReDet.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                            BeTransReDet.IdRecepcionDet = clsLnTrans_re_det.MaxID(BeRecepcionEnc.IdRecepcionEnc, lConnection, lTransaction) + i
                            BeTransReDet.IdProductoBodega = PickingUbic.IdProductoBodega
                            BeTransReDet.IdPresentacion = PickingUbic.IdPresentacion
                            BeTransReDet.IdUnidadMedida = PickingUbic.IdUnidadMedida
                            BeTransReDet.UnidadMedida.IdUnidadMedida = PickingUbic.IdUnidadMedida
                            BeTransReDet.IdProductoEstado = PickingUbic.IdProductoEstado
                            BeTransReDet.ProductoEstado.IdEstado = PickingUbic.IdProductoEstado
                            BeTransReDet.IdOperadorBodega = PickingUbic.IdOperadorBodega_Verifico 'corregir después.
                            BeTransReDet.No_Linea = BePedidoDet.No_linea
                            BeTransReDet.cantidad_recibida = PickingUbic.Cantidad_despachada
                            BeTransReDet.Nombre_producto = PickingUbic.NombreProducto
                            BeTransReDet.Nombre_presentacion = BePedidoDet.Nom_presentacion
                            BeTransReDet.Nombre_unidad_medida = BePedidoDet.Nom_unid_med
                            BeTransReDet.Nombre_producto_estado = BePedidoDet.Nom_estado
                            BeTransReDet.Lote = PickingUbic.Lote
                            BeTransReDet.Fecha_vence = PickingUbic.Fecha_Vence
                            BeTransReDet.Fecha_ingreso = Now
                            BeTransReDet.Peso = PickingUbic.Peso_despachado
                            BeTransReDet.Peso = PickingUbic.Peso_despachado
                            BeTransReDet.User_agr = OrdenCompraReOc.User_agr
                            BeTransReDet.Fec_agr = Now
                            BeTransReDet.Observacion = "Reversión de despacho del pedido: " & BePedidoEnc.IdPedidoEnc
                            BeTransReDet.Codigo_Producto = PickingUbic.CodigoProducto
                            BeTransReDet.Lic_plate = PickingUbic.Lic_plate
                            BeTransReDet.Pallet_No_Estandar = False
                            BeTransReDet.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc

                            Dim BeTransOCDet As New clsBeTrans_oc_det
                            BeTransOCDet = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_Linea(BeOrdenCompraEnc.IdOrdenCompraEnc,
                                                                                                      BePedidoDet.No_linea,
                                                                                                      PickingUbic.IdProductoBodega,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                            BeTransReDet.IdOrdenCompraDet = BeTransOCDet.IdOrdenCompraDet
                            BeTransReDet.IdJornadaSistema = 0
                            lBeRecDet.Add(BeTransReDet)

                            BeProductoPresentacion = clsLnProducto_presentacion.GetSingle(BePedidoDet.IdPresentacion, lConnection, lTransaction)
                            If BeProductoPresentacion IsNot Nothing Then vFactor = BeProductoPresentacion.Factor

                            BeStockRec = New clsBeStock_rec()
                            BeStockRec.IdStockRec = clsLnStock_rec.MaxID(lConnection, lTransaction)
                            BeStockRec.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega
                            BeStockRec.IdProductoBodega = PickingUbic.IdProductoBodega
                            BeStockRec.IdProductoEstado = PickingUbic.IdProductoEstado
                            BeStockRec.ProductoEstado.IdEstado = PickingUbic.IdProductoEstado
                            BeStockRec.IdPresentacion = PickingUbic.IdPresentacion
                            BeStockRec.IdUnidadMedida = PickingUbic.IdUnidadMedida
                            BeStockRec.IdUbicacion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(BeOrdenCompraEnc.IdBodega,
                                                                                                       lConnection,
                                                                                                       lTransaction)
                            BeStockRec.Cantidad = PickingUbic.Cantidad_despachada * vFactor
                            BeStockRec.IdUbicacion_anterior = PickingUbic.IdUbicacion
                            BeStockRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                            BeStockRec.IdRecepcionDet = BeTransReDet.IdRecepcionDet
                            BeStockRec.Lote = PickingUbic.Lote
                            BeStockRec.Lic_plate = PickingUbic.Lic_plate
                            BeStockRec.Serial = PickingUbic.Serial
                            BeStockRec.Fecha_Ingreso = Now
                            BeStockRec.Fecha_vence = PickingUbic.Fecha_Vence
                            BeStockRec.User_agr = BeOrdenCompraEnc.User_Agr
                            BeStockRec.Fec_agr = Now
                            BeStockRec.User_mod = BeOrdenCompraEnc.User_Agr
                            BeStockRec.Fec_mod = Now
                            BeStockRec.Activo = 1
                            BeStockRec.Peso = PickingUbic.Peso_despachado
                            BeStockRec.No_linea = BePedidoDet.No_linea
                            BeStockRec.Atributo_Variante_1 = BePedidoDet.Atributo_Variante_1
                            BeStockRec.IdBodega = BePedidoEnc.IdBodega
                            BeStockRec.Pallet_No_Estandar = False
                            lBeStockRec.Add(BeStockRec)

                        Next

                    End If

                Next

                Guardar(BeOrdenCompraEnc.IdBodega,
                        BeTareaHH,
                        BeRecepcionEnc,
                        BeRecepcionEnc.OrdenCompraRec,
                        lBeRecDet,
                        Nothing,
                        pListOperadorRecepcion,
                        Nothing,
                        Nothing,
                        Nothing,
                        lBeStockRec,
                        Nothing,
                        lConnection,
                        lTransaction)

                Dim vIdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(BeRecepcionEnc.IdBodega, lConnection, lTransaction)

                Finalizar_Recepcion(BeRecepcionEnc,
                                    False,
                                    BeOrdenCompraEnc.IdOrdenCompraEnc,
                                    BeRecepcionEnc.IdRecepcionEnc,
                                    vIdEmpresa,
                                    BeRecepcionEnc.IdBodega,
                                    BeRecepcionEnc.User_agr,
                                    lBeRecDet,
                                    False,
                                    lConnection,
                                    lTransaction)

                Generar_Tarea_Recepcion_By_OrdenCompraEnc_And_Pedido = 1

                OutBeRecepcionEnc = BeRecepcionEnc

            End If

        Catch ex As Exception
            '#MECR23092025: se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdBodega:=BeRecepcionEnc.IdBodega,
                                                 pIdUsuarioAgr:=BeRecepcionEnc.User_agr,
                                                 pIdRecEnc:=BeRecepcionEnc.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Sub Finalizar_Recepcion(ByVal pRecEnc As clsBeTrans_re_enc,
                                          ByVal backOrder As Boolean,
                                          ByVal pIdOrdenCompraEnc As Integer,
                                          ByVal pIdRecepcionEnc As Integer,
                                          ByVal pIdEmpresa As Integer,
                                          ByVal pIdBodega As Integer,
                                          ByVal pIdUsuario As String,
                                          ByVal pListObjDetR As List(Of clsBeTrans_re_det),
                                          ByVal pHabilitarStock As Boolean,
                                          ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction)

        Dim BeTMSTicket As New clsBeTms_ticket

        Try

            Dim listaStockRec As List(Of clsBeStock_rec) = clsLnStock_rec.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)

            '#EJC20220121: Validar que no haya sido Finalizada previamente.
            If Not Finalizada(pIdRecepcionEnc, lConnection, lTransaction) Then

                '#EJC20220121_1102: Cambiar el estado primero.
                Actualizar_Estado_Cerrado_Recepcion(pIdRecepcionEnc,
                                                    lConnection,
                                                    lTransaction)

                clsLnLog_error_wms.Agregar_Error(pIdEmpresa, pIdBodega, "#240313A: Se cerró la recepción: " & pIdRecepcionEnc & " IdUsuario_BOF: " & pIdUsuario)

                If Not Registros_Pendientes_Push(pIdRecepcionEnc, lConnection, lTransaction) Then

                    If Reglas_De_Recepcion_Permiten_Ingreso(pRecEnc,
                                                            lConnection,
                                                            lTransaction) Then

                        Dim lMaxS As Integer = clsLnStock.MaxID(lConnection,
                                                                lTransaction)

                        If listaStockRec IsNot Nothing AndAlso listaStockRec.Count > 0 Then

                            If Not pHabilitarStock Then

                                clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302230102:  Se está finalizando la recepción: " & pIdRecepcionEnc & " con Habilitar_Stock = False, Usuario: " & pIdUsuario)

                                Habilitar_Stock_Desde_StockRec(pIdEmpresa,
                                                               pIdBodega,
                                                               pIdOrdenCompraEnc,
                                                               pIdUsuario,
                                                               listaStockRec,
                                                               pListObjDetR,
                                                               lConnection,
                                                               lTransaction)
                            Else

                                Habilitar_Stock_Desde_Detalle_Recepcion(pIdRecepcionEnc,
                                                                        pIdOrdenCompraEnc,
                                                                        pIdUsuario,
                                                                        pIdEmpresa,
                                                                        pIdBodega,
                                                                        pRecEnc,
                                                                        lConnection,
                                                                        lTransaction)

                            End If

                        End If

                        clsLnStock_rec.Actualiza_Stock_Rec(listaStockRec,
                                                           lConnection,
                                                           lTransaction)

                        Actualizar_Estado_Pedido_Ingreso(pIdOrdenCompraEnc,
                                                         pIdRecepcionEnc,
                                                         lConnection,
                                                         lTransaction,
                                                         backOrder)

                        Actualizar_Hora_Fin_Recepcion(pIdOrdenCompraEnc,
                                                      pIdRecepcionEnc,
                                                      lConnection,
                                                      lTransaction)


                        Dim BeTareaHH As New clsBeTarea_hh
                        BeTareaHH = clsLnTarea_hh.GetSingle(1,
                                                            pIdRecepcionEnc,
                                                            pRecEnc.PropietarioBodega.IdPropietario,
                                                            lConnection,
                                                            lTransaction)

                        If Not BeTareaHH Is Nothing Then

                            If Not BeTareaHH.IdEstado = 4 Then

                                clsLnTarea_hh.Finalizar_Tarea_Recepcion(pIdRecepcionEnc,
                                                                        lConnection,
                                                                        lTransaction)

                            Else
                                Throw New Exception("Error_202211011918: Al parecer la recepción ya fue finalizada.")
                            End If

                        End If

                        If Not pIdOrdenCompraEnc = 0 Then

                            '#EJC20220803_1536: Validar que si tiene documento de ingreso si tiene o no ticket de TMS.
                            BeTMSTicket = clsLnTrans_oc_enc.Get_BeTicket_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                             lConnection,
                                                                                             lTransaction)

                            'Si tiene ticket de TMS el documento de ingreso.
                            If Not BeTMSTicket Is Nothing Then

                                clsLnTms_ticket.Actualizar_Tms_Ticket_Finalizado(BeTMSTicket.IdTicket,
                                                                                 lConnection,
                                                                                 lTransaction)

                            End If

                        End If

                    End If

                Else
                    Throw New Exception("Error_20220308: La recepción tiene registros pendientes de push")
                End If

            Else
                Throw New Exception("Error_20220121_0004: La recepción fue finalizada previamente.")
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CKFK20241018: Hice copia de esta función GetSingle para no cargar esto clsLnTrans_re_det.Get_Detalle_By_IdRecepcionEnc
    Public Shared Function GetSingleHH(ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_enc

        GetSingleHH = Nothing

        Try

            Dim vSQL As String = "SELECT b.descripcion AS UbicacionRecepcion, 
                                  tr.Descripcion, enc.IdRecepcionEnc, enc.IdPropietarioBodega, 
                                  enc.IdMuelle, enc.IdUbicacionRecepcion, enc.IdTipoTransaccion, 
                                  enc.fecha_recepcion, enc.hora_ini_pc, enc.hora_fin_pc, 
                                  enc.muestra_precio, enc.estado, enc.user_agr, enc.fec_agr, enc.user_mod, 
                                  enc.fec_mod, enc.fecha_tarea, enc.tomar_fotos, enc.escanear_rec_ubic, 
                                  enc.para_por_codigo, enc.observacion, enc.firma_piloto, enc.activo, 
                                  enc.NoGuia, enc.CorreoEnviado, enc.Revision_Inconsistencia, enc.bloqueada, 
                                  enc.bloqueada_por, enc.idusuariobloqueo, enc.idmotivoanulacionbodega, 
                                  enc.Habilitar_Stock, enc.idvehiculo, enc.idpiloto, enc.No_Marchamo, 
                                  enc.mostrar_cantidad_esperada, enc.IdBodega, enc.carta_cupo, no_contenedor,
                                  enc.IdEstado_Defecto_Recepcion
                                  FROM trans_re_enc AS enc INNER JOIN
                                  trans_re_tr AS tr ON enc.IdTipoTransaccion = tr.IdTipoTransaccion INNER JOIN
                                  bodega_ubicacion AS b ON enc.IdUbicacionRecepcion = b.IdUbicacion INNER JOIN
                                  propietario_bodega ON enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega AND b.IdBodega = propietario_bodega.IdBodega
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransReEnc As New clsBeTrans_re_enc()

                            Cargar(BeTransReEnc, lRow)

                            If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                                BeTransReEnc.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                                If clsLnPropietario_bodega.Obtener(BeTransReEnc.PropietarioBodega, lConnection, lTransaction) Then
                                    clsLnPropietarios.Obtener(BeTransReEnc.PropietarioBodega.Propietario, lConnection, lTransaction)
                                End If
                            End If

                            If lRow("IdUbicacionRecepcion") IsNot DBNull.Value AndAlso lRow("IdUbicacionRecepcion") IsNot Nothing Then
                                BeTransReEnc.UbicacionRecepcion = CType(lRow("UbicacionRecepcion"), String)
                            End If

                            If lRow("IdTipoTransaccion") IsNot DBNull.Value AndAlso lRow("IdTipoTransaccion") IsNot Nothing Then
                                BeTransReEnc.Descripcion = CType(lRow("Descripcion"), String)
                            End If

                            '#EJC20190401: Agregado por optimización de carga en HH.
                            BeTransReEnc.Muelle = New clsBeBodega_muelles()
                            BeTransReEnc.Muelle.IdMuelle = BeTransReEnc.IdMuelle
                            clsLnBodega_muelles.GetSingle(BeTransReEnc.Muelle, lConnection, lTransaction)

                            BeTransReEnc.IsNew = False

                            BeTransReEnc.OrdenCompraRec = clsLnTrans_re_oc.GetSingle(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            '#CKFK20241018 No voy a cargar todo el detalle de lotes voy a hacer una prueba
                            BeTransReEnc.Detalle = Nothing 'clsLnTrans_re_det.Get_Detalle_By_IdRecepcionEnc(BeTransReEnc.IdRecepcionEnc, BeTransReEnc.IdBodega, lConnection, lTransaction)
                            BeTransReEnc.DetalleImagenes = clsLnTrans_re_img.Get_Detalle_Imagenes_By_IdRecepcionEnc(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            BeTransReEnc.DetalleParametros = clsLnTrans_re_det_parametros.Get_Detalle_Parametros_By_RecepcionEnc(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            BeTransReEnc.DetalleFacturas = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            BeTransReEnc.TareaHH = clsLnTarea_hh.GetSingle(1, BeTransReEnc.IdRecepcionEnc, lTransaction, lConnection)

                            Return BeTransReEnc

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

    Friend Shared Function Get_No_Contenedor_By_IdRecepcionEnc(IdRecepcionEnc As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As String

        Get_No_Contenedor_By_IdRecepcionEnc = ""

        Try

            Dim sp As String = "SELECT No_Contenedor FROM Trans_re_enc Where(IdRecepcionEnc = @IdRecepcionEnc) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IdRecepcionEnc))
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then

                Dim dr As DataRow
                dr = dt.Rows(0)

                Get_No_Contenedor_By_IdRecepcionEnc = IIf(IsDBNull(dr.Item("No_Contenedor")), "", dr.Item("No_Contenedor"))

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingleHH_By_Codigo(ByVal pIdRecepcionEnc As Integer, ByVal CodigoProducto As String) As clsBeTrans_re_enc

        GetSingleHH_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT b.descripcion AS UbicacionRecepcion, 
                                  tr.Descripcion, enc.IdRecepcionEnc, enc.IdPropietarioBodega, 
                                  enc.IdMuelle, enc.IdUbicacionRecepcion, enc.IdTipoTransaccion, 
                                  enc.fecha_recepcion, enc.hora_ini_pc, enc.hora_fin_pc, 
                                  enc.muestra_precio, enc.estado, enc.user_agr, enc.fec_agr, enc.user_mod, 
                                  enc.fec_mod, enc.fecha_tarea, enc.tomar_fotos, enc.escanear_rec_ubic, 
                                  enc.para_por_codigo, enc.observacion, enc.firma_piloto, enc.activo, 
                                  enc.NoGuia, enc.CorreoEnviado, enc.Revision_Inconsistencia, enc.bloqueada, 
                                  enc.bloqueada_por, enc.idusuariobloqueo, enc.idmotivoanulacionbodega, 
                                  enc.Habilitar_Stock, enc.idvehiculo, enc.idpiloto, enc.No_Marchamo, 
                                  enc.mostrar_cantidad_esperada, enc.IdBodega, enc.carta_cupo, no_contenedor,
                                  enc.IdEstado_Defecto_Recepcion
                                  FROM trans_re_enc AS enc INNER JOIN
                                  trans_re_tr AS tr ON enc.IdTipoTransaccion = tr.IdTipoTransaccion INNER JOIN
                                  bodega_ubicacion AS b ON enc.IdUbicacionRecepcion = b.IdUbicacion INNER JOIN
                                  propietario_bodega ON enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega AND b.IdBodega = propietario_bodega.IdBodega
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransReEnc As New clsBeTrans_re_enc()

                            Cargar(BeTransReEnc, lRow)

                            If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                                BeTransReEnc.PropietarioBodega.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                            End If

                            If lRow("IdUbicacionRecepcion") IsNot DBNull.Value AndAlso lRow("IdUbicacionRecepcion") IsNot Nothing Then
                                BeTransReEnc.UbicacionRecepcion = CType(lRow("UbicacionRecepcion"), String)
                            End If

                            If lRow("IdTipoTransaccion") IsNot DBNull.Value AndAlso lRow("IdTipoTransaccion") IsNot Nothing Then
                                BeTransReEnc.Descripcion = CType(lRow("Descripcion"), String)
                            End If

                            '#EJC20190401: Agregado por optimización de carga en HH.
                            BeTransReEnc.Muelle = New clsBeBodega_muelles()
                            BeTransReEnc.Muelle.IdMuelle = BeTransReEnc.IdMuelle

                            BeTransReEnc.IsNew = False

                            BeTransReEnc.OrdenCompraRec = clsLnTrans_re_oc.GetSingle(BeTransReEnc.IdRecepcionEnc, lConnection, lTransaction)
                            BeTransReEnc.Detalle = clsLnTrans_re_det.Get_Detalle_By_IdRecepcionEnc_And_Codigo(BeTransReEnc.IdRecepcionEnc, BeTransReEnc.IdBodega, CodigoProducto, lConnection, lTransaction)
                            BeTransReEnc.TareaHH = clsLnTarea_hh.GetSingle(1, BeTransReEnc.IdRecepcionEnc, lTransaction, lConnection)

                            Return BeTransReEnc

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

    Public Shared Function Generar_Tarea_Recepcion_By_OrdenCompraEnc_Doc_Devolucion(ByRef BeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                                                    ByRef Resultado As String,
                                                                                    ByVal CrearRecepcionPorDefecto As Boolean,
                                                                                    ByVal BeMI3Config As clsBeI_nav_config_enc,
                                                                                    ByRef OutBeRecepcionEnc As clsBeTrans_re_enc,
                                                                                    ByRef lConnection As SqlConnection,
                                                                                    ByRef lTransaction As SqlTransaction) As Integer


        Generar_Tarea_Recepcion_By_OrdenCompraEnc_Doc_Devolucion = 0
        OutBeRecepcionEnc = Nothing

        Dim RegistrosAfectados As Integer = 0
        Dim lblResult As New RichTextBox
        Dim OrdenCompraReOc As New clsBeTrans_re_oc
        Dim BeTareaHH As New clsBeTarea_hh
        Dim BeRecepcionEnc As New clsBeTrans_re_enc
        Dim IdBodegaDestino As Integer = 0
        Dim IdPropietario As Integer = 0
        Dim TiempoMedioIngresoMinutos As Double
        Dim lBeRecDet As New List(Of clsBeTrans_re_det)
        Dim lBeStockRec As New List(Of clsBeStock_rec)
        Dim BeProductoEstado As New clsBeProducto_estado

        Try

            If Not clsLnTrans_re_oc.Existe_Documento_By_IdOrdenCompraEnc(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction) Then

                IdBodegaDestino = BeOrdenCompraEnc.IdBodega
                BeProductoEstado = clsLnProducto_estado.GetSingleByIdEstado(BeMI3Config.IdProductoEstado_NC, lConnection, lTransaction)

                BeRecepcionEnc.IsNew = True
                BeRecepcionEnc.IdRecepcionEnc = MaxID(lConnection, lTransaction) + 1
                BeRecepcionEnc.PropietarioBodega = New clsBePropietario_bodega
                BeRecepcionEnc.PropietarioBodega.IdBodega = BeOrdenCompraEnc.IdBodega
                '#EJC20210715: IdBodega se comparte a la tarea de recepción en generación automática de tarea..
                BeRecepcionEnc.IdBodega = BeOrdenCompraEnc.IdBodega
                BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega
                BeRecepcionEnc.User_agr = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fec_agr = Now
                BeRecepcionEnc.Activo = True
                BeRecepcionEnc.Estado = "Nuevo"

                BeRecepcionEnc.OrdenCompraRec = New clsBeTrans_re_oc
                BeRecepcionEnc.OrdenCompraRec.IsNew = True
                '#EJC20200120: Lo hace más tarde en el guardar...
                'BeRecepcionEnc.OrdenCompraRec.IdRecepcionOc = clsLnTrans_re_oc.MaxID(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction) + 1
                BeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc

                If BeRecepcionEnc.PropietarioBodega Is Nothing OrElse BeRecepcionEnc.PropietarioBodega.IdPropietarioBodega <= 0 Then
                    Throw New Exception("Propietario no válido al crear la recepción")
                End If

                'Ingreso con referenci a orden de compra para procesar en HH
                BeRecepcionEnc.IdTipoTransaccion = "HCOC00"

                BeRecepcionEnc.IdMuelle = clsLnBodega_muelles.Get_IdMuelle_Default_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdMuelle = 0 Then
                    Throw New Exception("No existe ningún muelle por defecto para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                BeRecepcionEnc.IdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If BeRecepcionEnc.IdUbicacionRecepcion = 0 Then
                    Throw New Exception("No está configurada la ubicación por defecto para recepción para el IdBodegaDestino: " & IdBodegaDestino)
                End If

                TiempoMedioIngresoMinutos = clsLnTarea_hh.Get_Tiempo_Medio_Tarea_Ingreso_Minutos(lConnection, lTransaction)

                BeRecepcionEnc.Fecha_recepcion = Now.Date
                BeRecepcionEnc.Hora_ini_pc = Now
                BeRecepcionEnc.Hora_fin_pc = Now.AddMinutes(TiempoMedioIngresoMinutos)
                BeRecepcionEnc.Muestra_precio = False
                BeRecepcionEnc.Fec_mod = Now
                BeRecepcionEnc.User_mod = BeMI3Config.IdUsuario
                BeRecepcionEnc.Fecha_tarea = Now
                BeRecepcionEnc.Tomar_fotos = False
                BeRecepcionEnc.Escanear_rec_ubic = False
                BeRecepcionEnc.Para_por_codigo = False
                BeRecepcionEnc.Observacion = "FROMMI3"
                BeRecepcionEnc.IdPiloto = 0
                BeRecepcionEnc.IdVehiculo = 0
                BeRecepcionEnc.Habilitar_Stock = False
                BeRecepcionEnc.IdBodega = IdBodegaDestino

                OrdenCompraReOc.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                OrdenCompraReOc.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc
                OrdenCompraReOc.IsNew = True
                OrdenCompraReOc.No_docto = BeOrdenCompraEnc.Referencia
                OrdenCompraReOc.OC = BeOrdenCompraEnc
                OrdenCompraReOc.Recepcion_ciega = False
                OrdenCompraReOc.Recepcion_manual = False
                OrdenCompraReOc.User_agr = BeMI3Config.IdUsuario

                BeRecepcionEnc.OrdenCompraRec = OrdenCompraReOc

                Dim pListOperadorRecepcion As New List(Of clsBeTrans_re_op)
                Dim pListOperadorBodega As New List(Of clsBeOperador_bodega)

                '#EJC20190711: Broadcast a todos los operadores de la bodega con la tarea.
                pListOperadorBodega = clsLnOperador_bodega.Get_All_By_IdBodega(IdBodegaDestino, lConnection, lTransaction)

                If Not pListOperadorBodega Is Nothing Then

                    Dim BeTransReOp As New clsBeTrans_re_op()

                    For Each Op In pListOperadorBodega

                        BeTransReOp = New clsBeTrans_re_op()
                        BeTransReOp.IdOperadorBodega = Op.IdOperadorBodega
                        BeTransReOp.User_agr = BeMI3Config.IdUsuario
                        BeTransReOp.Fec_agr = Now
                        BeTransReOp.User_mod = BeMI3Config.IdUsuario
                        BeTransReOp.Fec_mod = Now
                        BeTransReOp.IsNew = True
                        BeTransReOp.UsaHH = True
                        pListOperadorRecepcion.Add(BeTransReOp)

                    Next

                End If

                BeTareaHH = New clsBeTarea_hh
                BeTareaHH.IdPropietario = clsLnPropietarios.Get_IdPropietario(IdBodegaDestino, BeOrdenCompraEnc.IdPropietarioBodega)
                BeTareaHH.IdBodega = IdBodegaDestino
                BeTareaHH.IdMuelle = BeRecepcionEnc.IdMuelle
                BeTareaHH.IdEstado = 1
                BeTareaHH.IdPrioridad = 1
                BeTareaHH.IdTipoTarea = 1
                BeTareaHH.IdTransaccion = BeRecepcionEnc.IdRecepcionEnc
                BeTareaHH.Tipo = 0
                BeTareaHH.FechaInicio = Now
                BeTareaHH.FechaFin = Now.AddHours(2)
                BeTareaHH.DiaCompleto = False
                BeTareaHH.Descripcion = ""
                BeTareaHH.CreaTarea = True
                BeTareaHH.IsNew = True

                Select Case BeRecepcionEnc.IdTipoTransaccion.ToString()
                    Case "HSOC00"
                        BeTareaHH.Asunto = "Ingreso sin Orden de Compra "
                    Case "HSOD00"
                        BeTareaHH.Asunto = "Ingreso de Devolución sin referencia"
                    Case "HCOC00"
                        BeTareaHH.Asunto = "Ingreso con Orden de Compra"
                    Case "HCOD00"
                        BeTareaHH.Asunto = "Devolución de Pedido"
                    Case "HHSR00"
                        BeTareaHH.Asunto = "Ingreso sin referencia"
                    Case "PICH000"
                        BeTareaHH.Asunto = "Pre-ingreso con HH"
                    Case Else
                        Exit Select
                End Select

                Dim i As Integer = 0

                BeOrdenCompraEnc.DetalleOC = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(BeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction)

                '#EJC20190719: Se verifica si en la lista de pedidos del despacho hay pedidos para sucursales WMS.
                For Each BeOCDet As clsBeTrans_oc_det In BeOrdenCompraEnc.DetalleOC

                    Dim BeTransReDet As New clsBeTrans_re_det
                    Dim BeINavBarraPallet As New clsBeI_nav_barras_pallet
                    Dim BeINavBarraPalletOriginal As New clsBeI_nav_barras_pallet
                    Dim BeStock As New clsBeStock
                    Dim BeStockRec As New clsBeStock_rec
                    i += 1
                    BeTransReDet = New clsBeTrans_re_det()
                    BeTransReDet.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                    BeTransReDet.IdRecepcionDet = clsLnTrans_re_det.MaxID(BeRecepcionEnc.IdRecepcionEnc, lConnection, lTransaction) + i
                    BeTransReDet.IdProductoBodega = BeOCDet.IdProductoBodega
                    BeTransReDet.IdPresentacion = BeOCDet.IdPresentacion
                    BeTransReDet.IdUnidadMedida = BeOCDet.IdUnidadMedidaBasica
                    BeTransReDet.UnidadMedida.IdUnidadMedida = BeOCDet.IdUnidadMedidaBasica
                    BeTransReDet.IdProductoEstado = BeProductoEstado.IdEstado
                    BeTransReDet.ProductoEstado.IdEstado = BeProductoEstado.IdEstado
                    BeTransReDet.IdOperadorBodega = 1 'corregir después.
                    BeTransReDet.No_Linea = BeOCDet.No_Linea
                    BeTransReDet.cantidad_recibida = BeOCDet.Cantidad
                    BeTransReDet.Nombre_producto = BeOCDet.Nombre_producto
                    BeTransReDet.Nombre_presentacion = BeOCDet.Nombre_presentacion
                    BeTransReDet.Nombre_unidad_medida = BeOCDet.Nombre_unidad_medida_basica
                    BeTransReDet.Nombre_producto_estado = BeProductoEstado.Nombre
                    BeTransReDet.Lote = BeOCDet.Talla.Codigo & " " & BeOCDet.Color.Codigo
                    BeTransReDet.Fecha_vence = New Date(1900, 1, 1)
                    BeTransReDet.Fecha_ingreso = Now
                    BeTransReDet.Peso = 0
                    BeTransReDet.peso_unitario = 0
                    BeTransReDet.User_agr = OrdenCompraReOc.User_agr
                    BeTransReDet.Fec_agr = Now
                    BeTransReDet.Observacion = "Nota de crédito: " & BeOrdenCompraEnc.No_Documento
                    BeTransReDet.Codigo_Producto = BeOCDet.Codigo_Producto
                    BeTransReDet.Lic_plate = ""
                    BeTransReDet.Pallet_No_Estandar = False
                    BeTransReDet.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc
                    BeTransReDet.IdOrdenCompraDet = BeOCDet.IdOrdenCompraDet
                    BeTransReDet.IdJornadaSistema = 0
                    lBeRecDet.Add(BeTransReDet)

                    BeStockRec.IdStockRec = clsLnStock_rec.MaxID(lConnection, lTransaction)
                    BeStockRec.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega
                    BeStockRec.IdProductoBodega = BeOCDet.IdProductoBodega
                    BeStockRec.IdProductoEstado = BeTransReDet.IdProductoEstado
                    BeStockRec.ProductoEstado.IdEstado = BeTransReDet.IdProductoEstado
                    BeStockRec.IdPresentacion = BeOCDet.IdPresentacion
                    BeStockRec.IdUnidadMedida = BeOCDet.IdUnidadMedidaBasica
                    BeStockRec.IdUbicacion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(BeOrdenCompraEnc.IdBodega,
                                                                                                       lConnection,
                                                                                                       lTransaction)
                    BeStockRec.IdUbicacion_anterior = 0
                    BeStockRec.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                    BeStockRec.IdRecepcionDet = BeTransReDet.IdRecepcionDet
                    BeStockRec.Lote = BeTransReDet.Lote
                    BeStockRec.Lic_plate = BeTransReDet.Lic_plate
                    BeStockRec.Serial = ""
                    BeStockRec.Cantidad = BeTransReDet.cantidad_recibida
                    BeStockRec.Fecha_Ingreso = Now
                    BeStockRec.Fecha_vence = BeTransReDet.Fecha_vence
                    BeStockRec.User_agr = BeOrdenCompraEnc.User_Agr
                    BeStockRec.Fec_agr = Now
                    BeStockRec.User_mod = BeOrdenCompraEnc.User_Agr
                    BeStockRec.Fec_mod = Now
                    BeStockRec.Activo = 1
                    BeStockRec.Peso = BeTransReDet.Peso
                    BeStockRec.No_linea = BeTransReDet.No_Linea
                    BeStockRec.Atributo_Variante_1 = BeTransReDet.Atributo_Variante_1
                    BeStockRec.IdBodega = BeRecepcionEnc.IdBodega
                    BeStockRec.Pallet_No_Estandar = False
                    lBeStockRec.Add(BeStockRec)

                Next

                Guardar(BeOrdenCompraEnc.IdBodega,
                        BeTareaHH,
                        BeRecepcionEnc,
                        BeRecepcionEnc.OrdenCompraRec,
                        lBeRecDet,
                        Nothing,
                        pListOperadorRecepcion,
                        Nothing,
                        Nothing,
                        Nothing,
                        lBeStockRec,
                        Nothing,
                        lConnection,
                        lTransaction)

                Dim vIdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(BeRecepcionEnc.IdBodega, lConnection, lTransaction)

                Finalizar_Recepcion(BeRecepcionEnc,
                                    False,
                                    BeOrdenCompraEnc.IdOrdenCompraEnc,
                                    BeRecepcionEnc.IdRecepcionEnc,
                                    vIdEmpresa,
                                    BeRecepcionEnc.IdBodega,
                                    BeRecepcionEnc.User_agr,
                                    lBeRecDet,
                                    False,
                                    lConnection,
                                    lTransaction)

                Generar_Tarea_Recepcion_By_OrdenCompraEnc_Doc_Devolucion = 1

                OutBeRecepcionEnc = BeRecepcionEnc

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC(ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                                           ByVal IdPropietario As Integer,
                                                                           ByVal BeTransReDet As clsBeTrans_re_det,
                                                                           ByVal lConnection As SqlConnection,
                                                                           ByVal lTransaction As SqlTransaction) As Boolean

        Dim lProductosConDiferencia As New List(Of String)
        Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = True

        'Dim clsTransaction As New clsTransaccion

        Try

            If Not BeTransOCEnc.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Inventario_Inicial Then
                Dim vReglaProp As New clsBePropietario_reglas_enc
                Dim ListaRegla As New List(Of clsBePropietario_reglas_enc)
                Dim vPresRec As New clsBeProducto_Presentacion
                Dim vCantidadRecibidaUmBas As Double = 0
                Dim vPesoRecibido As Double = 0
                Dim vCantidadOCUMBas As Double = 0
                Dim vDiferenciaCantUmBas As Double = 0
                Dim vDiferenciaCosto As Double = 0
                Dim vDiferenciaPeso As Double = 0
                Dim vCostoRec As Double = 0

                ListaRegla = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(IdPropietario, lConnection, lTransaction)

                Using BeReglaRec As New clsBeReglas_RecepcionRes()

                    BeReglaRec.PermitirProductoFaltantes = False
                    BeReglaRec.PermitirProductosAdicionales = False
                    BeReglaRec.PermitirCantidadFaltantePorProducto = False
                    BeReglaRec.PermitirCantidadSobrantePorProducto = False
                    BeReglaRec.PermitirCostoDiferentePorProducto = False
                    BeReglaRec.PermitirPesoMenor = False
                    BeReglaRec.PermitirPesoMayor = False

                    vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 1) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirProductoFaltantes = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirProductoFaltantes = True
                    vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 2) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirProductosAdicionales = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirProductosAdicionales = True
                    vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 3) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCantidadFaltantePorProducto = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirCantidadFaltantePorProducto = True
                    vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 4) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCantidadSobrantePorProducto = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirProductoFaltantes = True
                    vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 5) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirCostoDiferentePorProducto = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirCostoDiferentePorProducto = True
                    vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 6) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirPesoMenor = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirPesoMenor = True
                    vReglaProp = ListaRegla.Find(Function(x) x.IdReglaRecepcion = 7) : If Not vReglaProp Is Nothing Then BeReglaRec.PermitirPesoMayor = Not vReglaProp.Regla.Rechazar Else BeReglaRec.PermitirPesoMayor = True

                    Dim vMensaje As String = ""

                    If ListaRegla.Count > 0 Then

                        If BeTransOCEnc IsNot Nothing Then

                            If BeTransOCEnc.DetalleOC IsNot Nothing Then

                                For Each oc_det As clsBeTrans_oc_det In BeTransOCEnc.DetalleOC.Where(Function(x) x.No_Linea = BeTransReDet.No_Linea _
                                                                                                     AndAlso x.Codigo_Producto = BeTransReDet.Codigo_Producto)

                                    If Not oc_det.IdPresentacion = 0 Then
                                        vPresRec.IdPresentacion = oc_det.IdPresentacion
                                        vPresRec = clsLnProducto_presentacion.GetSingle(vPresRec.IdPresentacion, lConnection, lTransaction)
                                        vCantidadOCUMBas = oc_det.Cantidad
                                    Else
                                        vCantidadOCUMBas = oc_det.Cantidad
                                    End If

                                    vCantidadRecibidaUmBas = 0

                                    If Not BeTransReDet Is Nothing Then

                                        '#GT06022024: tiene presentacion
                                        If Not BeTransReDet.IdPresentacion = 0 Then

                                            vPresRec.IdPresentacion = BeTransReDet.IdPresentacion
                                            vPresRec = clsLnProducto_presentacion.GetSingle(vPresRec.IdPresentacion, lConnection, lTransaction)

                                            If Not vPresRec.EsPallet Then
                                                vCantidadRecibidaUmBas += BeTransReDet.cantidad_recibida + oc_det.Cantidad_recibida
                                            Else
                                                vCantidadRecibidaUmBas += BeTransReDet.cantidad_recibida * vPresRec.Factor * vPresRec.CajasPorCama * vPresRec.CamasPorTarima + oc_det.Cantidad_recibida
                                            End If

                                        Else
                                            vCantidadRecibidaUmBas += BeTransReDet.cantidad_recibida + oc_det.Cantidad_recibida
                                        End If

                                        If vDiferenciaCosto = 0 Then
                                            vCostoRec = BeTransReDet.Costo
                                            vDiferenciaCosto = Math.Round(oc_det.Costo - BeTransReDet.Costo, 2)
                                        End If

                                        vPesoRecibido += BeTransReDet.Peso

                                    Else

                                        'No se recepcionó ese producto.
                                        If Not BeReglaRec.PermitirProductoFaltantes Then

                                            '#CKFK 20210708
                                            vMensaje = String.Format("La regla de recepción configurada " &
                                        " para el propietario no permite faltante de producto. " &
                                        " Producto en OC.: {0} Cantidad O.C.: {1} " &
                                        " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", oc_det.Producto.Codigo, oc_det.Producto.Nombre), oc_det.Cantidad, oc_det.Cantidad_recibida)

                                            'Throw New Exception(vMensaje)
                                            Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = False
                                            XtraMessageBox.Show(vMensaje, "Regla de recepción. ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                        End If

                                    End If

                                    Dim vCAntidadOriginalUMBas As Double = vCantidadOCUMBas

                                    If Not vPresRec Is Nothing Then

                                        If Not vPresRec.Factor = 0 Then
                                            vCAntidadOriginalUMBas = vCantidadOCUMBas * vPresRec.Factor
                                        Else
                                            vCAntidadOriginalUMBas = vCantidadOCUMBas
                                        End If

                                    End If

                                    vDiferenciaCantUmBas = vCAntidadOriginalUMBas - vCantidadRecibidaUmBas

                                    vDiferenciaPeso = oc_det.Peso - vPesoRecibido

                                    If vDiferenciaCantUmBas <> 0 Then
                                        lProductosConDiferencia.Add(oc_det.Producto.IdProducto)
                                    End If

                                    Select Case vDiferenciaCantUmBas

                                        Case Is = 0
                                                'No hay diferencia.

                                        Case Is > 0

                                            If Not BeReglaRec.PermitirCantidadFaltantePorProducto Then

                                                vMensaje = String.Format("La regla de recepción configurada " &
                                                                    " para el propietario no permite recibir menos producto " &
                                                                    " que el indicado por la orden de compra " &
                                                                    " Producto: {0} Cantidad O.C.: {1} " &
                                                                    " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", oc_det.Producto.Codigo, oc_det.Producto.Nombre), vCantidadOCUMBas, vCantidadRecibidaUmBas)

                                                'Throw New Exception(vMensaje)
                                                Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = False
                                                XtraMessageBox.Show(vMensaje, "Regla de recepción. ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                ' Se recibió de menos que lo indicado por la O.C.

                                            End If

                                        Case Is < 0

                                            If Not BeReglaRec.PermitirCantidadSobrantePorProducto Then

                                                vMensaje = String.Format("La regla de recepción configurada " &
                                                                    " para el propietario no permite recibir más producto " &
                                                                    " que el indicado por la orden de compra " &
                                                                    " Producto:{0} Cantidad O.C.: {1} " &
                                                                    " Cantidad Recepción.: {2}", String.Format("""{0} - {1}""", oc_det.Producto.Codigo, oc_det.Producto.Nombre), vCantidadOCUMBas, vCantidadRecibidaUmBas)


                                                'Throw New Exception(vMensaje)
                                                Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = False
                                                XtraMessageBox.Show(vMensaje, "Regla de recepción. ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                ' Se recibió más que lo indicado por la O.C.

                                            End If

                                        Case Else

                                            Exit Select

                                    End Select

                                    Select Case vDiferenciaCosto

                                        Case Is = 0
                                                'No hay diferencia.

                                        Case Is <> 0

                                            'El costo de la línea en la O.C. es diferente que el de la recepción.
                                            If Not BeReglaRec.PermitirCostoDiferentePorProducto Then

                                                vMensaje = String.Format("La regla de recepción configurada " &
                                                               " para el propietario no permite recibir productos " &
                                                               " con costo diferente que el indicado por la orden de compra " &
                                                               " Producto: {0} Costo en O.C.: {1} " &
                                                               " Costo en Recepción.: {2}", String.Format("""{0} - {1}""", oc_det.Producto.Codigo, oc_det.Producto.Nombre), oc_det.Costo, vCostoRec)

                                                'Throw New Exception(vMensaje)
                                                Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC = False
                                                XtraMessageBox.Show(vMensaje, "Regla de recepción. ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                            End If

                                        Case Else
                                            Exit Select

                                    End Select

                                Next oc_det


                            End If


                        End If

                    End If

                End Using

            End If

        Catch ex As Exception
            '#MECR23092025: se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdRecEnc:=BeTransReDet.IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

End Class