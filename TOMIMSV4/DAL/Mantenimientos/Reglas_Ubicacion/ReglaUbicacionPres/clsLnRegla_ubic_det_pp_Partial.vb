Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRegla_ubic_det_pp

    Friend Shared Function GetAllByIdReglaUbicacionEnc(idReglaUbicacionEnc As Integer) As List(Of clsBeRegla_ubic_det_pp)

        Try

            Dim lReturnList As New List(Of clsBeRegla_ubic_det_pp)

            Const sp As String = "SELECT * FROM Regla_ubic_det_pp" &
                " Where (IdReglaUbicacionEnc = @IdReglaUbicacionEnc)"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdReglaUbicacionEnc", idReglaUbicacionEnc))
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubic_det_pp As New clsBeRegla_ubic_det_pp

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubic_det_pp = New clsBeRegla_ubic_det_pp
                Cargar(vBeRegla_ubic_det_pp, dr)
                vBeRegla_ubic_det_pp.IsNew = False
                lReturnList.Add(vBeRegla_ubic_det_pp)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Friend Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdReglaUbicacionDetPP),0) FROM Regla_ubic_det_pp"

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Aplicar_Regla_Por_Presentacion(ByVal IdBodega As Integer,
                                                          ByVal IdPresentacion As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Boolean

        Aplicar_Regla_Por_Presentacion = False

        Try

            Dim vSQL As String = "SELECT regla_ubic_enc.IdBodega, regla_ubic_det_pp.IdPresentacion 
                                  FROM regla_ubic_det_pp INNER JOIN 
                                  regla_ubic_enc ON regla_ubic_det_pp.IdReglaUbicacionEnc = regla_ubic_enc.IdReglaUbicacionEnc 
                                  Where(regla_ubic_enc.IdBodega = @IdBodega AND regla_ubic_det_pp.IdPresentacion = @IdPresentacion AND regla_ubic_det_pp.Activo = 1) "


            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPresentacion", IdPresentacion))
            Dim dt As New DataTable

            dad.Fill(dt)

            Aplicar_Regla_Por_Presentacion = (dt.Rows.Count > 0)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
