Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ped_compra_det

    Public Shared Function Exist(ByRef pBeI_nav_ped_compra_det As clsBeI_nav_ped_compra_det,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction)

        Exist = False

        Try

            If pBeI_nav_ped_compra_det.No IsNot Nothing Then

                '#EJC20180614: No estaba el código de producto
                Dim vSQL As String = "SELECT No FROM I_nav_ped_compra_det Where(NoEnc = @NoEnc
                        AND Line_No = @Line_No AND No = @No)"

                Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pBeI_nav_ped_compra_det.NoEnc))
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@LINE_NO", pBeI_nav_ped_compra_det.Line_No))
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_compra_det.No))
                    lDTA.SelectCommand.Transaction = pTransaction

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Exist = lDT.Rows.Count > 0

                End Using

            Else
                Throw New Exception(String.Format("La línea de detalle para la ordenden de compra:{0}, no tiene # de línea", pBeI_nav_ped_compra_det.NoEnc))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'Public Shared Function GetAllByNoEnc(ByVal pNoEnc As String) As List(Of clsBeI_nav_ped_compra_det)

    '    Try

    '        Dim lReturnList As New List(Of clsBeI_nav_ped_compra_det)
    '        Const sp As String = "SELECT * FROM I_nav_ped_compra_det WHERE NoEnc = @NoEnc"
    '        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '        Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)
    '        dad.SelectCommand.Parameters.AddWithValue("@NoEnc", pNoEnc)
    '        Dim dt As New DataTable

    '        dad.Fill(dt)

    '        Dim vBeI_nav_ped_compra_det As New clsBeI_nav_ped_compra_det

    '        For Each dr As DataRow In dt.Rows

    '            vBeI_nav_ped_compra_det = New clsBeI_nav_ped_compra_det
    '            Cargar(vBeI_nav_ped_compra_det, dr)
    '            lReturnList.Add(vBeI_nav_ped_compra_det)

    '        Next

    '        lConnection.Dispose()
    '        cmd.Dispose()

    '        Return lReturnList

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Get_All_By_NoEnc(ByRef lConnection As SqlConnection,
                                  ByRef lTrans As SqlTransaction,
                                  ByVal pNoEnc As String) As List(Of clsBeI_nav_ped_compra_det)

        Get_All_By_NoEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_compra_det)

            Const sp As String = "SELECT * FROM I_nav_ped_compra_det 
                                  Where (NoEnc = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTrans) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@No", pNoEnc))

            dad.Fill(dt)

            Dim vBeI_nav_ped_compra_det As New clsBeI_nav_ped_compra_det

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_compra_det = New clsBeI_nav_ped_compra_det
                Cargar(vBeI_nav_ped_compra_det, dr)
                lReturnList.Add(vBeI_nav_ped_compra_det)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'Public Shared Function getAllTablaIntermediaDetalle() As Integer

    '    Try

    '        Dim lTotal As Integer = 0

    '        Dim vSQL As String = "SELECT Count(*) total FROM i_nav_ped_compra_det"

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            'HS 07112017 Quité query dentro de SqlCommand.
    '            Using lCommand As New SqlCommand(vSQL, lConnection)

    '                lCommand.CommandType = CommandType.Text

    '                lConnection.Open()

    '                Dim lReturnValue As Object = lCommand.ExecuteScalar()

    '                lConnection.Close()

    '                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

    '                    lTotal = CInt(lReturnValue)

    '                End If

    '            End Using

    '        End Using

    '        Return lTotal

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    'Public Shared Function getAllTablaWmsDetalle() As Integer

    '    Try

    '        Dim lTotal As Integer = 0

    '        Dim vSQL As String = "SELECT Count(*) total FROM trans_oc_det"

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            'HS Quité query dentro de SqlCommand.
    '            Using lCommand As New SqlCommand(vSQL, lConnection)

    '                lCommand.CommandType = CommandType.Text
    '                lConnection.Open()

    '                Dim lReturnValue As Object = lCommand.ExecuteScalar()
    '                lConnection.Close()

    '                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
    '                    lTotal = CInt(lReturnValue)
    '                End If

    '            End Using

    '        End Using

    '        Return lTotal

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Eliminar_By_NoEnc(ByVal NoEnc As String, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Eliminar_By_NoEnc = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_compra_det
               Where(NoEnc = @NoEnc) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", NoEnc))

            cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
