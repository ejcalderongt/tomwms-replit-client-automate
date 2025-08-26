Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRegla_ubic_prio_det

    Public Shared Function Get_All_By_IdReglaUbicPrioEnc(ByVal pIdReglaUbicPrioEnc As Integer) As List(Of clsBeRegla_ubic_prio_det)

        Try

            Dim lReturnList As New List(Of clsBeRegla_ubic_prio_det)
            Const sp As String = "SELECT * FROM Regla_ubic_prio_det WHERE IdReglaUbicPrioEnc = @IdReglaUbicPrioEnc "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdReglaUbicPrioEnc", pIdReglaUbicPrioEnc)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubic_prio_det As New clsBeRegla_ubic_prio_det

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubic_prio_det = New clsBeRegla_ubic_prio_det
                Cargar(vBeRegla_ubic_prio_det, dr)
                vBeRegla_ubic_prio_det.IsNew = False
                lReturnList.Add(vBeRegla_ubic_prio_det)

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

    Public Shared Function Get_All_By_IdReglaUbicPrioEnc(ByVal pIdReglaUbicPrioEnc As Integer,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBeRegla_ubic_prio_det)

        Try

            Dim lReturnList As New List(Of clsBeRegla_ubic_prio_det)
            Const sp As String = "SELECT * FROM Regla_ubic_prio_det 
                                  WHERE IdReglaUbicPrioEnc = @IdReglaUbicPrioEnc "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdReglaUbicPrioEnc", pIdReglaUbicPrioEnc)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubic_prio_det As New clsBeRegla_ubic_prio_det

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubic_prio_det = New clsBeRegla_ubic_prio_det
                Cargar(vBeRegla_ubic_prio_det, dr)
                vBeRegla_ubic_prio_det.IsNew = False
                lReturnList.Add(vBeRegla_ubic_prio_det)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdReglaUbicPrioDet),0) FROM Regla_ubic_prio_det"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
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

End Class
