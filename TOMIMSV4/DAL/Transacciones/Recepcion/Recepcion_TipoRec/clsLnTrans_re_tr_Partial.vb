Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_re_tr_Partial : Inherits clsLnTrans_re_tr

    Public Overrides Sub Cargar(ByRef oBeTrans_re_tr As clsBeTrans_re_tr, ByRef dr As DataRow)
        Try
            MyBase.Cargar(oBeTrans_re_tr, dr)
            With oBeTrans_re_tr
                .CantidadTareas = IIf(IsDBNull(dr.Item("Cantidad")), False, dr.Item("Cantidad"))
            End With
        Catch ex As Exception
            Throw New Exception("Trans_re_tr_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Function Get_All_For_HH() As List(Of clsBeTrans_re_tr)

        Try

            Dim sp As String = " SELECT trans_re_tr.IdTipoTransaccion, trans_re_tr.Descripcion, trans_re_tr.TipoTrans, SUBSTRING(trans_re_tr.Funcionalidad,1,1000) AS Funcionalidad, " &
                               " trans_re_tr.UsaHH,SUBSTRING(trans_re_tr.DescDev,1,1000) AS DescDev, trans_re_tr.ConRef, " &
                               " COUNT(trans_re_enc.IdRecepcionEnc) AS Cantidad " &
                               " FROM trans_re_tr LEFT JOIN " &
                               " trans_re_enc ON trans_re_enc.IdTipoTransaccion = trans_re_tr.IdTipoTransaccion " &
                               " WHERE trans_re_tr.UsaHH = 1 AND trans_re_tr.Activo = 1  " &
                               " GROUP BY trans_re_tr.IdTipoTransaccion, trans_re_tr.Descripcion, trans_re_tr.TipoTrans,trans_re_tr.UsaHH,trans_re_tr.ConRef, " &
                               " SUBSTRING(trans_re_tr.DescDev,1,1000), SUBSTRING(trans_re_tr.Funcionalidad,1,1000) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Dim lTr As New List(Of clsBeTrans_re_tr)
            Dim Tr As clsBeTrans_re_tr

            For Each dr As DataRow In dt.Rows

                Tr = New clsBeTrans_re_tr
                Cargar(Tr, dr)
                lTr.Add(Tr)

            Next

            dt.Dispose()

            Return lTr

        Catch ex As Exception
            Throw New Exception("Trans_re_tr_Listar: " & ex.Message)
        End Try

    End Function

    Public Overloads Shared Function GetAll() As List(Of clsBeTrans_re_tr)

        Try

            Dim lReturnList As New List(Of clsBeTrans_re_tr)


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_re_tr WHERE Activo = 1 "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_re_tr

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_re_tr()

                            Obj.IdTipoTransaccion = CType(lRow("IdTipoTransaccion"), String)

                            If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                                Obj.Descripcion = CType(lRow("Descripcion"), String)
                            End If

                            If lRow("Funcionalidad") IsNot DBNull.Value AndAlso lRow("Funcionalidad") IsNot Nothing Then
                                Obj.Funcionalidad = CType(lRow("Funcionalidad"), String)
                            End If

                            If lRow("UsaHH") IsNot DBNull.Value AndAlso lRow("UsaHH") IsNot Nothing Then
                                Obj.UsaHH = CType(lRow("UsaHH"), Boolean)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetAllFiltro(ByVal pConRef As Boolean, ByVal pTipoTrans As String, ByVal pFiltro As String) As List(Of clsBeTrans_re_tr)

        Try

            Dim lReturnList As New List(Of clsBeTrans_re_tr)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_re_tr WHERE Activo = 1 "

                If pFiltro Then
                    If pConRef Then
                        vSQL += "AND ConRef=1 "
                    Else
                        vSQL += "AND ConRef=0 "
                    End If
                End If

                If Not String.IsNullOrEmpty(pTipoTrans) Then

                    '#HS20171024_1000am: Quité String.Format.
                    'vSQL += String.Format(" AND TipoTrans='{0}'", pTipoTrans)

                    vSQL += " AND TipoTrans= @TipoTrans"

                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    If Not String.IsNullOrEmpty(pTipoTrans) Then lDTA.SelectCommand.Parameters.AddWithValue("@TipoTrans", pTipoTrans)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_re_tr

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_re_tr()

                            Obj.IdTipoTransaccion = CType(lRow("IdTipoTransaccion"), String)

                            If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                                Obj.Descripcion = CType(lRow("Descripcion"), String)
                            End If

                            If lRow("Funcionalidad") IsNot DBNull.Value AndAlso lRow("Funcionalidad") IsNot Nothing Then
                                Obj.Funcionalidad = CType(lRow("Funcionalidad"), String)
                            End If

                            If lRow("UsaHH") IsNot DBNull.Value AndAlso lRow("UsaHH") IsNot Nothing Then
                                Obj.UsaHH = CType(lRow("UsaHH"), Boolean)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#GT10062025: modifique el método para que opcionalmente maneje transacción
    Public Overloads Shared Function GetSingle(ByVal pIdTipoTransaccion As String,
                                                        Optional ByVal pConnection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeTrans_re_tr


        Dim Obj As clsBeTrans_re_tr
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        Dim lCommand As SqlCommand
        Dim lDT As New DataTable

        Try

            Dim vSQL As String = "SELECT * FROM trans_re_tr WHERE IdTipoTransaccion=@IdTipoTransaccion"

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lCommand = New SqlCommand(vSQL, lConnection, lTransaction)
            End If

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", pIdTipoTransaccion)

            lDTA = New SqlDataAdapter(lCommand)
            lDTA.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                Dim lRow As DataRow = lDT.Rows(0)

                Obj = New clsBeTrans_re_tr()

                Obj.IdTipoTransaccion = CType(lRow("IdTipoTransaccion"), String)

                If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                    Obj.Descripcion = CType(lRow("Descripcion"), String)
                End If

                If lRow("Funcionalidad") IsNot DBNull.Value AndAlso lRow("Funcionalidad") IsNot Nothing Then
                    Obj.Funcionalidad = CType(lRow("Funcionalidad"), String)
                End If

                If lRow("UsaHH") IsNot DBNull.Value AndAlso lRow("UsaHH") IsNot Nothing Then
                    Obj.UsaHH = CType(lRow("UsaHH"), Boolean)
                End If

                If lRow("ConRef") IsNot DBNull.Value AndAlso lRow("ConRef") IsNot Nothing Then
                    Obj.ConRef = CType(lRow("ConRef"), Boolean)
                End If
            Else
                Obj = Nothing
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return Obj

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Overloads Shared Function GetAll(ByVal pIngreso As Boolean, ByVal pUsaHH As Boolean, ByVal pRefer As Boolean) As List(Of clsBeTrans_re_tr)

        Try

            Dim lReturnList As New List(Of clsBeTrans_re_tr)


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT IdTipoTransaccion AS Código, 
                        Descripcion AS Descripción,
                        UsaHH,
                        ConRef 
                        FROM trans_re_tr WHERE Activo = 1"

                If pIngreso Then
                    vSQL += " AND TipoTrans='INGRESO' "
                Else
                    vSQL += " AND TipoTrans='DEVOLUCION' "
                End If

                If pUsaHH Then
                    vSQL += " AND UsaHH=1 "
                Else
                    vSQL += " AND UsaHH=0 "
                End If

                If pRefer Then
                    vSQL += " AND ConRef=1 "
                Else
                    vSQL += " AND ConRef=0 "
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_re_tr

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_re_tr()
                            Obj.IdTipoTransaccion = CType(lRow("Código"), String)

                            If lRow("Descripción") IsNot DBNull.Value AndAlso lRow("Descripción") IsNot Nothing Then
                                Obj.Descripcion = CType(lRow("Descripción"), String)
                            End If

                            If lRow("UsaHH") IsNot DBNull.Value AndAlso lRow("UsaHH") IsNot Nothing Then
                                Obj.UsaHH = CType(lRow("UsaHH"), Boolean)
                            End If

                            If lRow("ConRef") IsNot DBNull.Value AndAlso lRow("ConRef") IsNot Nothing Then
                                Obj.ConRef = CType(lRow("ConRef"), Boolean)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function UsaHH(ByVal pIdTipoTrans As String, ByRef pConnection As SqlConnection, ByRef pTransaction As SqlTransaction) As Boolean

        Try

            Dim lValue As Boolean = False

            Dim vSQL As String = "SELECT  UsaHH FROM trans_re_tr 
                                  WHERE IdTipoTransaccion = @pIdTipoTrans "

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@pIdTipoTrans", pIdTipoTrans)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lValue = CInt(lReturnValue)
                End If

            End Using

            Return lValue

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


End Class
