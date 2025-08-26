Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRegla_Ubicacion

    Public Shared Function Get_All_By_IdReglaUbicacionEnc(ByVal pIdReglaUbicacionEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeRegla_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeRegla_ubicacion)
            Const sp As String = "SELECT * FROM Regla_ubicacion WHERE IdReglaUbicacionEnc = @IdReglaUbicacionEnc and IdBodega=@IdBodega"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdReglaUbicacionEnc", pIdReglaUbicacionEnc)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubicacion As New clsBeRegla_ubicacion

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubicacion = New clsBeRegla_ubicacion
                Cargar(vBeRegla_ubicacion, dr)
                lReturnList.Add(vBeRegla_ubicacion)

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

End Class
