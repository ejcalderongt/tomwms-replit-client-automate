Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnVersion_wms_hh

    Public Shared Sub Cargar(ByRef oBeVersion_wms_hh As clsBeVersion_wms_hh, ByRef dr As DataRow)
        Try
            With oBeVersion_wms_hh
                .IdEmpresaVersion = IIf(IsDBNull(dr.Item("IdEmpresaVersion")), 0, dr.Item("IdEmpresaVersion"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Version = IIf(IsDBNull(dr.Item("version")), "", dr.Item("version"))
                .Notas = IIf(IsDBNull(dr.Item("notas")), "", dr.Item("notas"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Date.Now, dr.Item("fecha"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeVersion_wms_hh As clsBeVersion_wms_hh, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Ins.Init("version_wms_hh")
            Ins.Add("idempresaversion", "@idempresaversion", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("version", "@version", DataType.Parametro)
            Ins.Add("notas", "@notas", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

            Dim EsTransaccional As Boolean = (Not pConection is Nothing Andalso Not pTransaction Is Nothing)

            If EsTransaccional then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESAVERSION", oBeVersion_wms_hh.IdEmpresaVersion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeVersion_wms_hh.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@VERSION", oBeVersion_wms_hh.Version))
            cmd.Parameters.Add(New SqlParameter("@NOTAS", oBeVersion_wms_hh.Notas))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeVersion_wms_hh.Fecha))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected

            oBeVersion_wms_hh.IdEmpresaVersion = CInt(cmd.Parameters("@IDEMPRESAVERSION").Value)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close
            cnn.Dispose
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeVersion_wms_hh As clsBeVersion_wms_hh, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Upd.Init("version_wms_hh")
            Upd.Add("idempresaversion", "@idempresaversion", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("version", "@version", DataType.Parametro)
            Upd.Add("notas", "@notas", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Where("IdEmpresaVersion = @IdEmpresaVersion")

            Dim sp As String = Upd.SQL()

            Dim EsTransaccional As Boolean = (Not pConection is Nothing Andalso Not pTransaction Is Nothing)
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}


            If EsTransaccional then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESAVERSION", oBeVersion_wms_hh.IdEmpresaVersion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeVersion_wms_hh.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@VERSION", oBeVersion_wms_hh.Version))
            cmd.Parameters.Add(New SqlParameter("@NOTAS", oBeVersion_wms_hh.Notas))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeVersion_wms_hh.Fecha))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close
            cnn.Dispose
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeVersion_wms_hh As clsBeVersion_wms_hh, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Const sp As String = " Delete from Version_wms_hh" & _
             "  Where(IdEmpresaVersion = @IdEmpresaVersion)"

            Dim EsTransaccional As Boolean = (Not pConection is Nothing Andalso Not pTransaction Is Nothing)
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

            If EsTransaccional then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESAVERSION", oBeVersion_wms_hh.IdEmpresaVersion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected


        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close
            cnn.Dispose
        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection as SQLConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Const sp As String = " Delete from Version_wms_hh"
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim EsTransaccional As Boolean = (Not pConection is Nothing Andalso Not pTransaction Is Nothing)

            If EsTransaccional then
                cmd = New SqlCommand(sp, pConection)
            Else
                cmd = New SqlCommand(sp, cnn)
                cnn.Open()
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close
            cnn.Dispose
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Version_wms_hh"
            Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeVersion_wms_hh As clsBeVersion_wms_hh) As Boolean

        Try

            Const sp As String = "SELECT * FROM Version_wms_hh" & _
            " Where(IdEmpresaVersion = @IdEmpresaVersion)"

            Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESAVERSION", oBeVersion_wms_hh.IDEMPRESAVERSION))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeVersion_wms_hh, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeVersion_wms_hh)

        Try

            Dim lReturnList As New List(Of clsBeVersion_wms_hh)
            Const sp As String = "SELECT * FROM Version_wms_hh"
            Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeVersion_wms_hh As New clsBeVersion_wms_hh

            For Each dr As DataRow In dt.Rows
                vBeVersion_wms_hh = New clsBeVersion_wms_hh
                Cargar(vBeVersion_wms_hh, dr)
                lReturnList.Add(vBeVersion_wms_hh)
            Next

            Return lReturnList

            If cnn.State = ConnectionState.Open Then cnn.Close
            cnn.Dispose
            cmd.Dispose

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeVersion_wms_hh As clsBeVersion_wms_hh)

        Try

            Const sp As String = "SELECT * FROM Version_wms_hh" & _
            " Where(IdEmpresaVersion = @IdEmpresaVersion)"

            Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESAVERSION", pBeVersion_wms_hh.IDEMPRESAVERSION))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeVersion_wms_hh, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdEmpresaVersion),0) FROM Version_wms_hh"

            Using lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
