Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ejecucion_enc

    Public Shared Function GetLogEjecucionesDataSet(ByVal pFechaDel As Date,
                                                    ByVal pFechaAl As Date,
                                                    Optional ByVal pTexto As String = "",
                                                    Optional ByVal pTransaccion As String = "",
                                                    Optional ByVal pProceso As String = "") As DataSet

        Dim lDataSet As New DataSet("LogEjecucionesInterface")

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lEncAdapter As New SqlDataAdapter(GetSqlLogEjecucionesEncabezado(), lConnection),
                      lResAdapter As New SqlDataAdapter(GetSqlLogEjecucionesResultado(), lConnection),
                      lErrAdapter As New SqlDataAdapter(GetSqlLogEjecucionesErrores(), lConnection)

                    lEncAdapter.SelectCommand.CommandType = CommandType.Text
                    lResAdapter.SelectCommand.CommandType = CommandType.Text
                    lErrAdapter.SelectCommand.CommandType = CommandType.Text

                    AgregarParametrosLog(lEncAdapter.SelectCommand, pFechaDel, pFechaAl, pTexto, pTransaccion, pProceso)
                    AgregarParametrosLog(lResAdapter.SelectCommand, pFechaDel, pFechaAl, pTexto, pTransaccion, pProceso)
                    AgregarParametrosLog(lErrAdapter.SelectCommand, pFechaDel, pFechaAl, pTexto, pTransaccion, pProceso)

                    lEncAdapter.Fill(lDataSet, "Encabezado")
                    lResAdapter.Fill(lDataSet, "Resultados")
                    lErrAdapter.Fill(lDataSet, "Errores")

                End Using

            End Using

            If lDataSet.Tables.Contains("Encabezado") Then
                lDataSet.Tables("Encabezado").PrimaryKey = New DataColumn() {lDataSet.Tables("Encabezado").Columns("IdEjecucionEnc")}
            End If

            If lDataSet.Tables.Contains("Encabezado") AndAlso lDataSet.Tables.Contains("Resultados") Then
                lDataSet.Relations.Add("Resultados",
                                       lDataSet.Tables("Encabezado").Columns("IdEjecucionEnc"),
                                       lDataSet.Tables("Resultados").Columns("IdEjecucionEnc"),
                                       False)
            End If

            If lDataSet.Tables.Contains("Encabezado") AndAlso lDataSet.Tables.Contains("Errores") Then
                lDataSet.Relations.Add("Errores",
                                       lDataSet.Tables("Encabezado").Columns("IdEjecucionEnc"),
                                       lDataSet.Tables("Errores").Columns("IdEjecucionEnc"),
                                       False)
            End If

            Return lDataSet

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Sub AgregarParametrosLog(ByVal pCommand As SqlCommand,
                                            ByVal pFechaDel As Date,
                                            ByVal pFechaAl As Date,
                                            ByVal pTexto As String,
                                            ByVal pTransaccion As String,
                                            ByVal pProceso As String)

        Dim lTexto As String = If(pTexto, "").Trim()
        Dim lTransaccion As String = If(pTransaccion, "").Trim()
        Dim lProceso As String = If(pProceso, "").Trim()

        pCommand.Parameters.Add("@FechaDel", SqlDbType.DateTime).Value = pFechaDel.Date
        pCommand.Parameters.Add("@FechaAl", SqlDbType.DateTime).Value = pFechaAl.Date.AddDays(1)
        pCommand.Parameters.Add("@Texto", SqlDbType.NVarChar, 200).Value = lTexto
        pCommand.Parameters.Add("@TextoLike", SqlDbType.NVarChar, 210).Value = "%" & lTexto & "%"
        pCommand.Parameters.Add("@Transaccion", SqlDbType.NVarChar, 100).Value = lTransaccion
        pCommand.Parameters.Add("@TransaccionLike", SqlDbType.NVarChar, 110).Value = "%" & lTransaccion & "%"
        pCommand.Parameters.Add("@Proceso", SqlDbType.NVarChar, 100).Value = lProceso
        pCommand.Parameters.Add("@ProcesoLike", SqlDbType.NVarChar, 110).Value = "%" & lProceso & "%"

    End Sub

    Private Shared Function GetFiltroLogEjecuciones() As String

        Return " enc.fecha >= @FechaDel " &
               " AND enc.fecha < @FechaAl " &
               " AND (@Proceso = '' OR EXISTS (SELECT 1 FROM i_nav_config_det fdet INNER JOIN i_nav_ent fent ON fdet.idnavent = fent.idnavent WHERE fdet.idnavconfigenc = enc.idnavconfigenc AND fent.nombre LIKE @ProcesoLike)) " &
               " AND (@Transaccion = '' OR EXISTS (SELECT 1 FROM i_nav_config_det fdet INNER JOIN i_nav_ent fent ON fdet.idnavent = fent.idnavent WHERE fdet.idnavconfigenc = enc.idnavconfigenc AND (fent.nombre LIKE @TransaccionLike OR CONVERT(NVARCHAR(20), fdet.idnavconfigdet) = @Transaccion))) " &
               " AND (@Texto = '' " &
               "      OR CONVERT(NVARCHAR(20), enc.idejecucionenc) LIKE @TextoLike " &
               "      OR ISNULL(cfg.nombre, '') LIKE @TextoLike " &
               "      OR ISNULL(emp.nombre, '') LIKE @TextoLike " &
               "      OR ISNULL(bod.nombre, '') LIKE @TextoLike " &
               "      OR ISNULL(prop.nombre_comercial, '') LIKE @TextoLike " &
               "      OR EXISTS (SELECT 1 FROM i_nav_config_det tdet INNER JOIN i_nav_ent tent ON tdet.idnavent = tent.idnavent WHERE tdet.idnavconfigenc = enc.idnavconfigenc AND tent.nombre LIKE @TextoLike) " &
               "      OR EXISTS (SELECT 1 FROM i_nav_ejecucion_res tres LEFT JOIN i_nav_config_det tdet ON tres.idnavconfigdet = tdet.idnavconfigdet LEFT JOIN i_nav_ent tent ON tdet.idnavent = tent.idnavent WHERE tres.idejecucionenc = enc.idejecucionenc AND (CONVERT(NVARCHAR(20), tres.idejecucionres) LIKE @TextoLike OR CONVERT(NVARCHAR(20), tres.idnavconfigdet) LIKE @TextoLike OR ISNULL(tent.nombre, '') LIKE @TextoLike)) " &
               "      OR EXISTS (SELECT 1 FROM i_nav_ejecucion_det_error terr WHERE terr.idejecucionenc = enc.idejecucionenc AND (CONVERT(NVARCHAR(20), terr.idejecuciondet) LIKE @TextoLike OR ISNULL(terr.referencia, '') LIKE @TextoLike OR ISNULL(terr.error, '') LIKE @TextoLike OR ISNULL(terr.codigo_producto, '') LIKE @TextoLike OR ISNULL(terr.umbas, '') LIKE @TextoLike OR ISNULL(terr.codigo_presentacion, '') LIKE @TextoLike))) "

    End Function

    Private Shared Function GetSqlLogEjecucionesEncabezado() As String

        Return "SELECT enc.idejecucionenc AS IdEjecucionEnc, " &
               "       enc.idnavconfigenc AS IdNavConfigEnc, " &
               "       enc.fecha AS Fecha, " &
               "       enc.exitosa AS Exitosa, " &
               "       ISNULL(cfg.nombre, '') AS Configuracion, " &
               "       ISNULL(emp.nombre, '') AS Empresa, " &
               "       ISNULL(bod.nombre, '') AS Bodega, " &
               "       ISNULL(prop.nombre_comercial, '') AS Propietario, " &
               "       ISNULL(STUFF((SELECT DISTINCT ', ' + ent.nombre " &
               "                     FROM i_nav_config_det det " &
               "                     INNER JOIN i_nav_ent ent ON det.idnavent = ent.idnavent " &
               "                     WHERE det.idnavconfigenc = enc.idnavconfigenc " &
               "                     FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''), '') AS Procesos, " &
               "       (SELECT COUNT(1) FROM i_nav_ejecucion_res res WHERE res.idejecucionenc = enc.idejecucionenc) AS CantidadResultados, " &
               "       (SELECT COUNT(1) FROM i_nav_ejecucion_det_error err WHERE err.idejecucionenc = enc.idejecucionenc) AS CantidadErrores " &
               "FROM i_nav_ejecucion_enc enc " &
               "LEFT JOIN i_nav_config_enc cfg ON enc.idnavconfigenc = cfg.idnavconfigenc " &
               "LEFT JOIN empresa emp ON cfg.idempresa = emp.IdEmpresa " &
               "LEFT JOIN bodega bod ON cfg.idbodega = bod.IdBodega AND cfg.idempresa = bod.IdEmpresa " &
               "LEFT JOIN propietarios prop ON cfg.idPropietario = prop.IdPropietario AND cfg.idempresa = prop.IdEmpresa " &
               "WHERE " & GetFiltroLogEjecuciones() &
               "ORDER BY enc.fecha DESC, enc.idejecucionenc DESC"

    End Function

    Private Shared Function GetSqlLogEjecucionesResultado() As String

        Return "SELECT res.idejecucionres AS IdEjecucionRes, " &
               "       res.idejecucionenc AS IdEjecucionEnc, " &
               "       res.idnavconfigdet AS IdNavConfigDet, " &
               "       ISNULL(ent.nombre, '') AS Proceso, " &
               "       res.registros_ws AS RegistrosWS, " &
               "       res.registros_ti AS RegistrosTI, " &
               "       res.registros_wms AS RegistrosWMS, " &
               "       res.exitosa AS Exitosa " &
               "FROM i_nav_ejecucion_res res " &
               "INNER JOIN i_nav_ejecucion_enc enc ON res.idejecucionenc = enc.idejecucionenc " &
               "LEFT JOIN i_nav_config_enc cfg ON enc.idnavconfigenc = cfg.idnavconfigenc " &
               "LEFT JOIN empresa emp ON cfg.idempresa = emp.IdEmpresa " &
               "LEFT JOIN bodega bod ON cfg.idbodega = bod.IdBodega AND cfg.idempresa = bod.IdEmpresa " &
               "LEFT JOIN propietarios prop ON cfg.idPropietario = prop.IdPropietario AND cfg.idempresa = prop.IdEmpresa " &
               "LEFT JOIN i_nav_config_det det ON res.idnavconfigdet = det.idnavconfigdet " &
               "LEFT JOIN i_nav_ent ent ON det.idnavent = ent.idnavent " &
               "WHERE " & GetFiltroLogEjecuciones() &
               " AND (@Proceso = '' OR ISNULL(ent.nombre, '') LIKE @ProcesoLike) " &
               " AND (@Transaccion = '' OR ISNULL(ent.nombre, '') LIKE @TransaccionLike OR CONVERT(NVARCHAR(20), res.idnavconfigdet) = @Transaccion) " &
               "ORDER BY res.idejecucionenc DESC, res.idejecucionres"

    End Function

    Private Shared Function GetSqlLogEjecucionesErrores() As String

        Return "SELECT err.idejecuciondet AS IdEjecucionDet, " &
               "       err.idejecucionenc AS IdEjecucionEnc, " &
               "       err.idnavconfigdet AS IdNavConfigDet, " &
               "       ISNULL(ent.nombre, '') AS Proceso, " &
               "       err.fecha AS Fecha, " &
               "       err.referencia AS Referencia, " &
               "       err.error AS Error, " &
               "       err.no_linea AS NoLinea, " &
               "       err.codigo_producto AS CodigoProducto, " &
               "       err.umbas AS UMBas, " &
               "       err.codigo_presentacion AS CodigoPresentacion " &
               "FROM i_nav_ejecucion_det_error err " &
               "INNER JOIN i_nav_ejecucion_enc enc ON err.idejecucionenc = enc.idejecucionenc " &
               "LEFT JOIN i_nav_config_enc cfg ON enc.idnavconfigenc = cfg.idnavconfigenc " &
               "LEFT JOIN empresa emp ON cfg.idempresa = emp.IdEmpresa " &
               "LEFT JOIN bodega bod ON cfg.idbodega = bod.IdBodega AND cfg.idempresa = bod.IdEmpresa " &
               "LEFT JOIN propietarios prop ON cfg.idPropietario = prop.IdPropietario AND cfg.idempresa = prop.IdEmpresa " &
               "LEFT JOIN i_nav_config_det det ON err.idnavconfigdet = det.idnavconfigdet " &
               "LEFT JOIN i_nav_ent ent ON det.idnavent = ent.idnavent " &
               "WHERE " & GetFiltroLogEjecuciones() &
               " AND (@Proceso = '' OR ISNULL(ent.nombre, '') LIKE @ProcesoLike) " &
               " AND (@Transaccion = '' OR ISNULL(ent.nombre, '') LIKE @TransaccionLike OR CONVERT(NVARCHAR(20), err.idnavconfigdet) = @Transaccion) " &
               "ORDER BY err.idejecucionenc DESC, err.fecha DESC, err.idejecuciondet DESC"

    End Function

    Public Shared Function Insertar_From_Interface(ByRef oBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc, ByVal pConection As SqlConnection) As Integer

        Try

            Ins.Init("i_nav_ejecucion_enc")
            Ins.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("exitosa", "@exitosa", DataType.Parametro)

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim sp As String = Ins.SQLIdentity("idejecucionenc")

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_ejecucion_enc.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_enc.Exitosa))

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            oBeI_nav_ejecucion_enc.IdEjecucionEnc = newId

            cmd.Dispose()

            'If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return newId

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_From_Interface(ByRef oBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Ins.Init("i_nav_ejecucion_enc")
            Ins.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("exitosa", "@exitosa", DataType.Parametro)

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim sp As String = Ins.SQLIdentity("idejecucionenc")

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_ejecucion_enc.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_enc.Exitosa))

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            oBeI_nav_ejecucion_enc.IdEjecucionEnc = newId

            cmd.Dispose()

            lTransaction.Commit()

            Insertar_From_Interface = newId

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetAllEjecuciones(ByVal pFechaDel As Date, ByVal pFechaAl As Date) As List(Of clsBeI_nav_ejecucion_enc)

        Dim lReturnList As New List(Of clsBeI_nav_ejecucion_enc)

        Try

            Dim vSQL As String = "SELECT i_nav_ejecucion_enc.idejecucionenc, empresa.nombre AS Empresa, bodega.nombre AS Bodega, propietarios.nombre_comercial AS Propietario, 
                         i_nav_config_enc.nombre AS Configuración, i_nav_ent.nombre AS Interface, dbo.i_nav_ejecucion_enc.fecha
                         FROM i_nav_config_enc INNER JOIN
                         i_nav_ejecucion_enc ON i_nav_config_enc.idnavconfigenc = i_nav_ejecucion_enc.idnavconfigenc INNER JOIN
                         empresa ON i_nav_config_enc.idempresa = empresa.IdEmpresa INNER JOIN
                         bodega ON i_nav_config_enc.idbodega = bodega.IdBodega AND empresa.IdEmpresa = bodega.IdEmpresa INNER JOIN
                         propietarios ON i_nav_config_enc.idPropietario = propietarios.IdPropietario AND empresa.IdEmpresa = propietarios.IdEmpresa INNER JOIN
                         i_nav_config_det ON i_nav_config_enc.idnavconfigenc = i_nav_config_det.idnavconfigenc INNER JOIN
                         i_nav_ent ON i_nav_config_det.idnavent = i_nav_ent.idnavent LEFT OUTER JOIN
                         i_nav_ejecucion_res ON i_nav_ejecucion_enc.idejecucionenc = i_nav_ejecucion_res.idejecucionenc LEFT OUTER JOIN
                         i_nav_ejecucion_det_error ON i_nav_ejecucion_enc.idejecucionenc = i_nav_ejecucion_det_error.idejecucionenc
                         WHERE cast(i_nav_ejecucion_enc.fecha  AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                         " AND " & FormatoFechas.fFecha(pFechaAl) &
                         "GROUP BY dbo.i_nav_ejecucion_enc.idejecucionenc, dbo.empresa.nombre, dbo.bodega.nombre, dbo.propietarios.nombre_comercial, dbo.i_nav_config_enc.nombre, 
                         dbo.i_nav_ent.nombre, dbo.i_nav_ejecucion_enc.fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    Dim lDataTable As New DataTable
                    lDataAdapter.Fill(lDataTable)

                    Dim Obj As clsBeI_nav_ejecucion_enc

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeI_nav_ejecucion_enc

                            If lRow("idejecucionenc") IsNot DBNull.Value AndAlso lRow("idejecucionenc") IsNot Nothing Then
                                Obj.IdEjecucionEnc = CType(lRow("idejecucionenc"), Integer)
                            End If

                            If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                Obj.Empresa = CType(lRow("Empresa"), String)
                            End If

                            If lRow("Bodega") IsNot DBNull.Value AndAlso lRow("Bodega") IsNot Nothing Then
                                Obj.Bodega = CType(lRow("Bodega"), String)
                            End If

                            If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                Obj.Propietario = CType(lRow("Propietario"), String)
                            End If

                            If lRow("Configuración") IsNot DBNull.Value AndAlso lRow("Configuración") IsNot Nothing Then
                                Obj.Configuracion = CType(lRow("Configuración"), String)
                            End If

                            If lRow("Interface") IsNot DBNull.Value AndAlso lRow("Interface") IsNot Nothing Then
                                Obj.Interfaces = CType(lRow("Interface"), String)
                            End If

                            If lRow("fecha") IsNot DBNull.Value AndAlso lRow("fecha") IsNot Nothing Then
                                Obj.Fecha = CType(lRow("fecha"), Date)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllEjecucionesDetalle(ByVal pFechaDel As Date, ByVal pFechaAl As Date) As List(Of clsBeI_nav_ejecucion_enc)

        Dim lReturnList As New List(Of clsBeI_nav_ejecucion_enc)

        Try

            Dim vSQL As String = "SELECT i_nav_ejecucion_enc.idejecucionenc, i_nav_config_enc.nombre AS Configuración, i_nav_ent.nombre AS Interface, i_nav_ejecucion_res.registros_ws, 
                         i_nav_ejecucion_res.registros_ti, i_nav_ejecucion_res.registros_wms, i_nav_ejecucion_res.exitosa, i_nav_ejecucion_det_error.error, 
                         i_nav_ejecucion_det_error.fecha AS fecha_error, i_nav_ejecucion_det_error.referencia
                         FROM i_nav_config_enc INNER JOIN
                         i_nav_ejecucion_enc ON i_nav_config_enc.idnavconfigenc = i_nav_ejecucion_enc.idnavconfigenc INNER JOIN
                         empresa ON i_nav_config_enc.idempresa = empresa.IdEmpresa INNER JOIN
                         bodega ON i_nav_config_enc.idbodega = bodega.IdBodega AND empresa.IdEmpresa = bodega.IdEmpresa INNER JOIN
                         propietarios ON i_nav_config_enc.idPropietario = propietarios.IdPropietario AND empresa.IdEmpresa = propietarios.IdEmpresa INNER JOIN
                         i_nav_config_det ON i_nav_config_enc.idnavconfigenc = i_nav_config_det.idnavconfigenc INNER JOIN
                         i_nav_ent ON i_nav_config_det.idnavent = i_nav_ent.idnavent LEFT OUTER JOIN
                         i_nav_ejecucion_res ON i_nav_ejecucion_enc.idejecucionenc = i_nav_ejecucion_res.idejecucionenc LEFT OUTER JOIN
                         i_nav_ejecucion_det_error ON i_nav_ejecucion_enc.idejecucionenc = i_nav_ejecucion_det_error.idejecucionenc
                         WHERE cast(i_nav_ejecucion_enc.fecha  AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                         " AND " & FormatoFechas.fFecha(pFechaAl)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDataAdapter.Fill(lDataTable)

                    Dim Obj As clsBeI_nav_ejecucion_enc

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeI_nav_ejecucion_enc

                            If lRow("idejecucionenc") IsNot DBNull.Value AndAlso lRow("idejecucionenc") IsNot Nothing Then
                                Obj.IdEjecucionEnc = CType(lRow("idejecucionenc"), Integer)
                            End If

                            If lRow("Configuración") IsNot DBNull.Value AndAlso lRow("Configuración") IsNot Nothing Then
                                Obj.Configuracion = CType(lRow("Configuración"), String)
                            End If

                            If lRow("Interface") IsNot DBNull.Value AndAlso lRow("Interface") IsNot Nothing Then
                                Obj.Interfaces = CType(lRow("Interface"), String)
                            End If

                            If lRow("registros_ws") IsNot DBNull.Value AndAlso lRow("registros_ws") IsNot Nothing Then
                                Obj.registros_ws = CType(lRow("registros_ws"), Integer)
                            End If

                            If lRow("registros_ti") IsNot DBNull.Value AndAlso lRow("registros_ti") IsNot Nothing Then
                                Obj.registros_ti = CType(lRow("registros_ti"), Integer)
                            End If

                            If lRow("registros_wms") IsNot DBNull.Value AndAlso lRow("registros_wms") IsNot Nothing Then
                                Obj.registros_wms = CType(lRow("registros_wms"), Integer)
                            End If

                            If lRow("exitosa") IsNot DBNull.Value AndAlso lRow("exitosa") IsNot Nothing Then
                                Obj.Exitosa_res = CType(lRow("exitosa"), Boolean)
                            End If

                            If lRow("error") IsNot DBNull.Value AndAlso lRow("error") IsNot Nothing Then
                                Obj.Errores = CType(lRow("error"), String)
                            End If

                            If lRow("fecha_error") IsNot DBNull.Value AndAlso lRow("fecha_error") IsNot Nothing Then
                                Obj.Fecha_error = CType(lRow("fecha_error"), Date)
                            End If

                            If lRow("referencia") IsNot DBNull.Value AndAlso lRow("referencia") IsNot Nothing Then
                                Obj.Referencia = CType(lRow("referencia"), String)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
