Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnFont_det
    Implements IDisposable

    Public Shared Sub Cargar(ByRef oBeFont_det As clsBeFont_det, ByRef dr As DataRow)
        Try
            With oBeFont_det
                .IdFontDet = IIf(IsDBNull(dr.Item("IdFontDet")), 0, dr.Item("IdFontDet"))
                .IdFontEnc = IIf(IsDBNull(dr.Item("IdFontEnc")), 0, dr.Item("IdFontEnc"))
                .Letra = IIf(IsDBNull(dr.Item("Letra")), "", dr.Item("Letra"))
                .Tamaño = IIf(IsDBNull(dr.Item("Tamaño")), 0.0, dr.Item("Tamaño"))
                .Negrita = IIf(IsDBNull(dr.Item("Negrita")), False, dr.Item("Negrita"))
                .Cursiva = IIf(IsDBNull(dr.Item("Cursiva")), False, dr.Item("Cursiva"))
                .Subrayado = IIf(IsDBNull(dr.Item("Subrayado")), False, dr.Item("Subrayado"))
                .ColorFont = IIf(IsDBNull(dr.Item("ColorFont")), "", dr.Item("ColorFont"))
                .ColorFondo = IIf(IsDBNull(dr.Item("ColorFondo")), "", dr.Item("ColorFondo"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeFont_det As clsBeFont_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("font_det")
            Ins.Add("idfontdet", "@idfontdet", DataType.Parametro)
            Ins.Add("idfontenc", "@idfontenc", DataType.Parametro)
            Ins.Add("letra", "@letra", DataType.Parametro)
            Ins.Add("tamaño", "@tamaño", DataType.Parametro)
            Ins.Add("negrita", "@negrita", DataType.Parametro)
            Ins.Add("cursiva", "@cursiva", DataType.Parametro)
            Ins.Add("subrayado", "@subrayado", DataType.Parametro)
            Ins.Add("colorfont", "@colorfont", DataType.Parametro)
            Ins.Add("colorfondo", "@colorfondo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDFONTDET", oBeFont_det.IdFontDet))
            cmd.Parameters.Add(New SqlParameter("@IDFONTENC", oBeFont_det.IdFontEnc))
            cmd.Parameters.Add(New SqlParameter("@LETRA", oBeFont_det.Letra))
            cmd.Parameters.Add(New SqlParameter("@TAMAÑO", oBeFont_det.Tamaño))
            cmd.Parameters.Add(New SqlParameter("@NEGRITA", oBeFont_det.Negrita))
            cmd.Parameters.Add(New SqlParameter("@CURSIVA", oBeFont_det.Cursiva))
            cmd.Parameters.Add(New SqlParameter("@SUBRAYADO", oBeFont_det.Subrayado))
            cmd.Parameters.Add(New SqlParameter("@COLORFONT", oBeFont_det.ColorFont))
            cmd.Parameters.Add(New SqlParameter("@COLORFONDO", oBeFont_det.ColorFondo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeFont_det.IdFontDet = CInt(cmd.Parameters("@IDFONTDET").Value)

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeFont_det As clsBeFont_Det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("font_det")
            Upd.Add("idfontdet", "@idfontdet", DataType.Parametro)
            Upd.Add("idfontenc", "@idfontenc", DataType.Parametro)
            Upd.Add("letra", "@letra", DataType.Parametro)
            Upd.Add("tamaño", "@tamaño", DataType.Parametro)
            Upd.Add("negrita", "@negrita", DataType.Parametro)
            Upd.Add("cursiva", "@cursiva", DataType.Parametro)
            Upd.Add("subrayado", "@subrayado", DataType.Parametro)
            Upd.Add("colorfont", "@colorfont", DataType.Parametro)
            Upd.Add("colorfondo", "@colorfondo", DataType.Parametro)
            Upd.Where("IdFontDet = @IdFontDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDFONTDET", oBeFont_det.IdFontDet))
            cmd.Parameters.Add(New SqlParameter("@IDFONTENC", oBeFont_det.IdFontEnc))
            cmd.Parameters.Add(New SqlParameter("@LETRA", oBeFont_det.Letra))
            cmd.Parameters.Add(New SqlParameter("@TAMAÑO", oBeFont_det.Tamaño))
            cmd.Parameters.Add(New SqlParameter("@NEGRITA", oBeFont_det.Negrita))
            cmd.Parameters.Add(New SqlParameter("@CURSIVA", oBeFont_det.Cursiva))
            cmd.Parameters.Add(New SqlParameter("@SUBRAYADO", oBeFont_det.Subrayado))
            cmd.Parameters.Add(New SqlParameter("@COLORFONT", oBeFont_det.ColorFont))
            cmd.Parameters.Add(New SqlParameter("@COLORFONDO", oBeFont_det.ColorFondo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeFont_det As clsBeFont_Det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Font_det" &
             "  Where(IdFontDet = @IdFontDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDFONTDET", oBeFont_det.IdFontDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Font_det"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Font_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeFont_det As clsBeFont_det) As Boolean

        Try

            Const sp As String = "SELECT * FROM Font_det" & _
            " Where(IdFontDet = @IdFontDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDFONTDET", oBeFont_det.IDFONTDET))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeFont_det, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeFont_det)

        Try

            Dim lReturnList As New List(Of clsBeFont_det)
            Const sp As String = "SELECT * FROM Font_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeFont_det As New clsBeFont_Det

            For Each dr As DataRow In dt.Rows

                vBeFont_det = New clsBeFont_Det
                Cargar(vBeFont_det, dr)
                lReturnList.Add(vBeFont_det)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeFont_det As clsBeFont_det)

        Try

            Const sp As String = "SELECT * FROM Font_det" & _
            " Where(IdFontDet = @IdFontDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDFONTDET", pBeFont_det.IDFONTDET))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeFont_det, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdFontDet),0) FROM Font_det"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
