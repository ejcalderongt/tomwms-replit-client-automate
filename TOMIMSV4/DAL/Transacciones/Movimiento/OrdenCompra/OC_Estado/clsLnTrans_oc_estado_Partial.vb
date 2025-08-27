Imports System.Reflection
Imports System.Data.SqlClient

Partial Public Class clsLnTrans_oc_estado

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0
            'Validacion y estandarizacion de los datos
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                'Acceso a los datos.
                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdEstadoOC),0) FROM trans_oc_estado"), lConnection)
                    lCommand.CommandType = CommandType.Text
                    lCommand.CommandTimeout = 200
                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
              Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdEstadoOc As Integer) As clsBeTrans_oc_estado

        Try

            Const sp As String = "SELECT * FROM Trans_oc_estado" &
            " Where(IdEstadoOC = @IdEstadoOC)"

            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@IDESTADOOC", pIdEstadoOc))

            Dim pBeTrans_oc_estado As New clsBeTrans_oc_estado            
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_oc_estado, dt.Rows(0))
            End If

            Return pBeTrans_oc_estado

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
