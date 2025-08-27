Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTipo_contenedor

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT IdTipoContenedor AS Código, Nombre FROM tipo_contenedor WHERE 1 > 0 "

            If pActivo Then
                sp += " AND activo=1"
            Else
                sp += " AND activo=0"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeTipo_contenedor)

        Try

            Dim lReturnList As New List(Of clsBeTipo_contenedor)
            Const sp As String = "SELECT * FROM Tipo_contenedor WHERE Activo = @Activo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTipo_contenedor As New clsBeTipo_contenedor

            For Each dr As DataRow In dt.Rows

                vBeTipo_contenedor = New clsBeTipo_contenedor
                Cargar(vBeTipo_contenedor, dr)
                lReturnList.Add(vBeTipo_contenedor)

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

    Public Shared Function Get_All_For_Combo(ByVal pActivo As Boolean) As DataTable

        Try

            Dim lReturnList As New List(Of clsBeTipo_contenedor)
            Const sp As String = "SELECT IdTipoContenedor,Nombre FROM Tipo_contenedor WHERE Activo = @Activo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
