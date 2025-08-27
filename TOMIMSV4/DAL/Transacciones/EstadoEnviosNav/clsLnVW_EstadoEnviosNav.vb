Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnVW_EstadoEnviosNAV

    Public Shared Sub Cargar(ByRef oBe_vw_estado_envios_nav As clsBeVW_EstadoEnviosNAV, ByRef dr As DataRow)

        Try

            With oBe_vw_estado_envios_nav

                If dr.Table.Columns.Contains("Fecha") Then .Fecha = IIf(IsDBNull(dr.Item("Fecha")), New Date(1900, 1, 1), dr.Item("Fecha"))
                If dr.Table.Columns.Contains("Tipo") Then .Tipo = IIf(IsDBNull(dr.Item("Tipo")), "", dr.Item("Tipo"))
                If dr.Table.Columns.Contains("Estado") Then .Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
                If dr.Table.Columns.Contains("Cantidad") Then .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0, dr.Item("Cantidad"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Listar(ByVal FechaIni As Date, ByVal FechaFin As Date, Optional ByVal vTipo As String = "") As DataTable

        Dim strSQL As String = ""

        Try


            strSQL = String.Format("SELECT {0} Estado, SUM(Cantidad) AS Cantidad 
                                    FROM VW_ESTADO_ENVIOS_NAV 
                                    WHERE FECHA BETWEEN {1} AND {2} {3}
                                    GROUP BY ESTADO{4} ", IIf(vTipo = "", "CONVERT(DATE, GETDATE()) AS Fecha, TIPO, ", ""),
                                    FormatoFechas.fFecha(FechaIni), FormatoFechas.fFecha(FechaFin),
                                    IIf(vTipo <> "", String.Format(" AND Tipo = '{0}'", vTipo), ""),
                                    IIf(vTipo = "", ", TIPO", ""))

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(strSQL, lConnection) With {.CommandType = CommandType.Text}
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

    Public Shared Function ListarPorColumna(ByVal FechaIni As Date, ByVal FechaFin As Date) As DataTable

        Dim strSQL As String = ""

        Try


            strSQL = String.Format("SELECT Fecha, 
                                    SUM(Enviados_PC) AS Enviados_PC, 
                                    SUM(No_Enviados_PC) AS No_Enviados_PC,
                                    SUM(Enviados_PC) + SUM(No_Enviados_PC) AS Total_PC,
                                    SUM(Enviados_PT) AS Enviados_PT, 
                                    SUM(No_Enviados_PT) AS No_Enviados_PT,
                                    SUM(Enviados_PT) + SUM(No_Enviados_PT) AS Total_PT
                                    FROM (
                                    SELECT FECHA, 
                                    CASE WHEN TIPO = 'PC' THEN CASE WHEN ESTADO = 'Enviados' THEN SUM(CANTIDAD) ELSE 0 END 
                                                          ELSE 0 END AS Enviados_PC, 
                                    CASE WHEN TIPO = 'PC' THEN CASE WHEN ESTADO = 'NO Enviados' THEN SUM(CANTIDAD) ELSE 0 END 
                                                          ELSE 0 END AS No_Enviados_PC, 
                                    CASE WHEN TIPO = 'PT' THEN CASE WHEN ESTADO = 'Enviados' THEN SUM(CANTIDAD) ELSE 0 END 
                                                          ELSE 0 END AS Enviados_PT, 
                                    CASE WHEN TIPO = 'PT' THEN CASE WHEN ESTADO = 'NO Enviados' THEN SUM(CANTIDAD) ELSE 0 END 
                                                          ELSE 0 END AS No_Enviados_PT  
                                    FROM VW_ESTADO_ENVIOS_NAV 
                                    WHERE FECHA BETWEEN {0} AND {1}
                                    GROUP BY  FECHA, TIPO, ESTADO) AS T
                                    GROUP BY Fecha", FormatoFechas.fFecha(FechaIni), FormatoFechas.fFecha(FechaFin))

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(strSQL, lConnection) With {.CommandType = CommandType.Text}
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

    Public Shared Function GetAllByFecha(ByVal FechaIni As Date, ByVal FechaFin As Date) As List(Of clsBeVW_EstadoEnviosNAV)

        Try

            Dim lReturnList As New List(Of clsBeVW_EstadoEnviosNAV)

            Dim strSQL As String = String.Format("SELECT Fecha, Tipo, Estado, SUM(Cantidad) AS Cantidad 
                                                  FROM VW_ESTADO_ENVIOS_NAV 
                                                  WHERE FECHA BETWEEN {0} AND {1}
                                                  GROUP BY Fecha, ESTADO, TIPO ", FormatoFechas.fFecha(FechaIni), FormatoFechas.fFecha(FechaFin))

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(strSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeVW_EstadoEnviosNAV As New clsBeVW_EstadoEnviosNAV

            For Each dr As DataRow In dt.Rows
                vBeVW_EstadoEnviosNAV = New clsBeVW_EstadoEnviosNAV
                Cargar(vBeVW_EstadoEnviosNAV, dr)
                lReturnList.Add(vBeVW_EstadoEnviosNAV)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetTotalByFechaByTipo(ByVal FechaIni As Date, ByVal FechaFin As Date, vTipo As String) As Integer

        Try

            Dim vReturn As Integer = 0

            Dim strSQL As String = String.Format("SELECT SUM(Cantidad) AS Total 
                                                  FROM VW_ESTADO_ENVIOS_NAV 
                                                  WHERE FECHA BETWEEN {0} AND {1} AND TIPO = '{2}'",
                                                 FormatoFechas.fFecha(FechaIni), FormatoFechas.fFecha(FechaFin), vTipo)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lCommand As New SqlCommand(strSQL, lConnection) With {.CommandType = CommandType.Text}

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        vReturn = lReturnValue
                    End If

                End Using

            End Using

            Return vReturn

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
