Imports System.Data.SqlClient

Partial Public Class clsLnConfiguracion_usuario_det

    Public Function Get_Configuracion_Det(ByVal pIdEmpresa As Integer,
                                          ByVal pIdUsuario As Integer,
                                          ByVal pNombreTemplate As String) As clsBeConfiguracion_usuario_det

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Configuracion_Det = Nothing

        Try

            Dim vSQL As String = "SELECT configuracion_usuario_enc.IdConfiguracionUsuarioEnc, configuracion_usuario_enc.IdEmpresa, configuracion_usuario_enc.IdUsuario, configuracion_usuario_det.IdConfiguracionUsuarioDet, 
                                  configuracion_usuario_det.Maquina_Host, configuracion_usuario_det.Maquina_IP, configuracion_usuario_det.Nombre_Template, configuracion_usuario_det.String_Template, 
                                  configuracion_usuario_det.Fecha_Agregado, configuracion_usuario_det.Fecha_Modificado
                                  FROM configuracion_usuario_det INNER JOIN
                                  configuracion_usuario_enc ON configuracion_usuario_det.IdConfiguracionUsuarioEnc = configuracion_usuario_enc.IdConfiguracionUsuarioEnc 
                                  WHERE configuracion_usuario_enc.IdEmpresa = @IdEmpresa 
                                  AND configuracion_usuario_det.IdUsuario = @IdUsuario 
                                  AND configuracion_usuario_det.Nombre_Template = @NombreTemplate "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)
                lDTA.SelectCommand.Parameters.AddWithValue("@NombreTemplate", pNombreTemplate)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeConfiguracion_usuario_det As New clsBeConfiguracion_usuario_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeConfiguracion_usuario_det, lDataTable.Rows(0))
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
