Imports System.Data.SqlClient

Partial Public Class clsLnTrans_oc_ti


    Public Shared Function GetSingle(ByVal pIdTipoIngresoOC As Integer) As clsBeTrans_oc_ti

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM trans_oc_ti WHERE IdTipoIngresoOC=@IdTipoIngresoOC "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoIngresoOC", pIdTipoIngresoOC)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransOCTI As New clsBeTrans_oc_ti()
                            Cargar(BeTransOCTI, lRow)
                            GetSingle = BeTransOCTI

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdTipoIngresoOC As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_ti

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM trans_oc_ti WHERE IdTipoIngresoOC=@IdTipoIngresoOC"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoIngresoOC", pIdTipoIngresoOC)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_oc_ti()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeTrans_oc_ti)

        Get_All_Filtro = Nothing

        Try


            Dim lReturnList As New List(Of clsBeTrans_oc_ti)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM trans_oc_ti WHERE 1 > 0 "

                    If pActivo = True Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_oc_ti

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeTrans_oc_ti
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                            Return lReturnList

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_oc_ti)

        GetAll = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_ti)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM trans_oc_ti WHERE Activo=1 "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_oc_ti

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeTrans_oc_ti
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                            Return lReturnList

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Combo(ByVal EsBodegaFiscal As Boolean) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT IdTipoIngresoOC as IdTipoIngreso,Nombre FROM trans_oc_ti WHERE Activo = 1 "

            If Not EsBodegaFiscal Then
                sp += "AND (control_poliza =0 OR IdTipoIngresoOC = 10)"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Regimen() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'GT 16032021 El parametro es_bodega_fiscal determina si es fiscal o general
            Const sp As String = "SELECT 'Todo' AS Bodega
                                    UNION ALL
                                    select 
                                    CASE
                                        WHEN es_bodega_fiscal =1 THEN 'Fiscal'
                                        WHEN es_bodega_fiscal=0 THEN 'General'
                                     END as Bodega
                                    from bodega

                                    group by 
                                    CASE
                                    WHEN es_bodega_fiscal =1  THEN 'Fiscal'
                                    WHEN es_bodega_fiscal =0  THEN 'General'
                                    END "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Tipo_Transaccion() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT tipo_transaccion 
                                  FROM dbo.I_NAV_TRANSACCIONES_OUT 
                                  GROUP BY tipo_transaccion
                                  UNION ALL SELECT 'TODO' AS tipo_transaccion "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(IdTipoIngresoOC),0) FROM trans_oc_ti")

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

            Return lMax

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_ForCombo() As DataTable

        Get_All_ForCombo = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT IdTipoIngresoOC,Nombre from trans_oc_ti where es_devolucion = 0 and activo=1"
            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_IdTipoDocumento_Consolidado_Defecto() As Integer

        Get_IdTipoDocumento_Consolidado_Defecto = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT IdTipoIngresoOC from trans_oc_ti where es_devolucion = 0 and es_poliza_consolidada =1 and activo =1"
            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Get_IdTipoDocumento_Consolidado_Defecto = dt.Rows(0).Item("IdTipoIngresoOC")
            End If

            cmd.Dispose()
            dad.Dispose()

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All(ByVal Activos As Boolean) As List(Of clsBeTrans_oc_ti)

        Get_All = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_ti)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM trans_oc_ti WHERE 1 > 0 "

                    If Activos Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_oc_ti

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeTrans_oc_ti
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                            Get_All = lReturnList

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_DT(ByVal Activos As Boolean) As DataTable

        Get_All_DT = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_ti)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM trans_oc_ti WHERE 1 > 0 "

                    If Activos Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_DT = lDataTable

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
