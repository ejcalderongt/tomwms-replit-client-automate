Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ejecucion_enc

    Public Shared Function Insertar_From_Interface(ByRef oBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc, ByVal pConection As SqlConnection) As Integer

        Try

            Ins.Init("i_nav_ejecucion_enc")
            Ins.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Ins.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("exitosa", "@exitosa", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_enc.IdEjecucionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_ejecucion_enc.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_enc.Exitosa))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            'If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

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
            Ins.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Ins.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("exitosa", "@exitosa", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_enc.IdEjecucionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_ejecucion_enc.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_enc.Exitosa))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Insertar_From_Interface = rowsAffected

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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(idejecucionenc),0) FROM i_nav_ejecucion_enc"

            Using lCommand As New SqlCommand(vSQL, pConnection) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
