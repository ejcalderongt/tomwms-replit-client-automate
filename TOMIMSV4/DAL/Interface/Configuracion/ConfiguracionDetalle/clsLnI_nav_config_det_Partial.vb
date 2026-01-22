Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_config_det
    Public Shared Function ListarFiltrados(ByVal pIdEnc As Integer) As DataTable
        Dim sp As String
        Try

            '#HS20171023_1620pm: Quité String.Format.
            sp = "SELECT * FROM VW_navdetalleconfiguracion  where idnavconfigenc= idnavconfigenc"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@idnavconfigenc", pIdEnc)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdNavEnt As Integer, ByVal pIdDia As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM i_nav_config_det WHERE idnavent=@pIdNavEnt and  dia = @pIdDia"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)
                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@pIdNavEnt", pIdNavEnt)
                    lCommand.Parameters.AddWithValue("@pIdDia", pIdDia)
                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If
                End Using
            End Using
            Return lExists

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '#EJC20171107_REF20_0950PM: Transaccionalidad en función exist
    Public Shared Function Exists(ByVal pIdNavEnt As Integer,
                                  ByVal pIdDia As Integer,
                                  ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM i_nav_config_det WHERE idnavent=@pIdNavEnt and  dia = @pIdDia"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@pIdNavEnt", pIdNavEnt)
                lCommand.Parameters.AddWithValue("@pIdDia", pIdDia)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllFiltro(ByVal pIdNavEnt As Integer, ByVal pActivo As Boolean) As List(Of clsBeI_nav_config_det)

        Dim lReturnList As New List(Of clsBeI_nav_config_det)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = ""

                If pIdNavEnt = 0 Then
                    vSQL = "SELECT * FROM i_nav_config_det WHERE 1 > 0"
                Else
                    vSQL = "SELECT * FROM i_nav_config_det WHERE idnavent=@idnavent"
                End If

                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    If pIdNavEnt <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@idnavent", pIdNavEnt)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeI_nav_config_det

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeI_nav_config_det

                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEnc(ByVal pIdNavConfigEnc As Integer,
                                            ByVal pEntidad As String) As List(Of clsBeI_nav_config_det)

        Dim lReturnList As New List(Of clsBeI_nav_config_det)

        Try

            Dim vSQL As String = "SELECT det.idnavconfigdet,det.idnavent,det.idnavconfigenc, 
                                det.dia,det.horainicio,det.horafin,det.frecuencia,det.activo, 
                                det.fec_agr,det.user_agr,det.fec_mod,det.user_mod 
                                from i_nav_config_det det  inner join i_nav_ent ent 
                                on det.idnavent = ent.idnavent where det.idnavconfigenc = @idnavconfigenc 
                                And ent.nombre=@nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@idnavconfigenc", pIdNavConfigEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@nombre", pEntidad)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeI_nav_config_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeI_nav_config_det

                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idnavconfigdet),0) FROM I_nav_config_det"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
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


    '#GT22072025: validar si existe el día asociado a la interface y ejecutable.
    Public Shared Function Exists(ByVal pInavConfigDet As clsBeI_nav_config_det, Optional pConection As SqlConnection = Nothing, Optional pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        Dim Es_Transaccion_Remota As Boolean
        Exists = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM i_nav_config_det WHERE (idnavconfigenc=@idnavconfigenc and dia=@dia and idnavent=@idnavent and activo=1)"

            Es_Transaccion_Remota = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            End If



            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdNavConfigEnc", pInavConfigDet.IdNavConfigEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdNavEnt", pInavConfigDet.IdNavEnt)
            lDTA.SelectCommand.Parameters.AddWithValue("@Dia", pInavConfigDet.Dia)


            Dim lReturnValue As Object = lDTA.SelectCommand.ExecuteScalar()
            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Exists = CInt(lReturnValue) > 0
            End If


        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso lTransaction IsNot Nothing Then
                lTransaction.Rollback()
            End If
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
                If lTransaction IsNot Nothing Then lTransaction.Dispose()
                If lConnection IsNot Nothing Then lConnection.Dispose()
            End If
        End Try
    End Function

End Class
