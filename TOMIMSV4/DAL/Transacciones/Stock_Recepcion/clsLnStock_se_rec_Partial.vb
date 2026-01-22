Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnStock_se_rec

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdStockSeRec),0) FROM stock_se_rec"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}


                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllSerieByIdStockRec(ByVal pIdStockRec As Integer) As List(Of clsBeStock_se_rec)

        Dim lReturnList As New List(Of clsBeStock_se_rec)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Dim vSQL As String = String.Format("SELECT * FROM stock_se_rec WHERE IdStockRec = {0}", pIdStockRec)
                Dim vSQL As String = "SELECT * FROM stock_se_rec WHERE IdStockRec = @IdStockRec"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdStockRec", pIdStockRec)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeStock_se_rec

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeStock_se_rec
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

    Public Shared Function GetAllSerieByIdStockRec(ByVal pIdStockRec As Integer,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeStock_se_rec)

        Dim lReturnList As New List(Of clsBeStock_se_rec)

        Try

            Dim vSQL As String = "SELECT * FROM stock_se_rec WHERE IdStockRec = @IdStockRec"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStockRec", pIdStockRec)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeStock_se_rec

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeStock_se_rec
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Serie_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer) As List(Of clsBeStock_se_rec)

        Dim lReturnList As New List(Of clsBeStock_se_rec)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171024_1120am: Quité String.Format.
                'vSQL = String.Format(" SELECT * FROM stock_se_rec " & _
                '                                   " WHERE IdStockRec IN " & _
                '                                   " (SELECT IdStockRec FROM stock_rec " & _
                '                                   " WHERE IdRecepcionEnc = {0} AND IdRecepcionDet = {1})", pIdRecepcionEnc, pIdRecepcionDet)
                Dim vSQL As String = "SELECT * FROM stock_se_rec " &
                                                   " WHERE IdStockRec IN " &
                                                   " (SELECT IdStockRec FROM stock_rec " &
                                                   " WHERE IdRecepcionEnc = @IdRecepcionEnc AND IdRecepcionDet = @IdRecepcionDet)"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeStock_se_rec

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeStock_se_rec
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

    Public Shared Function Guarda_Stock_Se_Rec(ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                               ByVal pListStockRec As List(Of clsBeStock_rec),
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As Integer

        Guarda_Stock_Se_Rec = 0

        Dim vRegistros As Integer = 0

        Try

            If Not pListStockRec Is Nothing Then

                If pListStockRec.Count > 0 Then

                    If Not pListStockRecSer Is Nothing Then

                        For Each ObjS As clsBeStock_rec In pListStockRec

                            For Each BeStockSeRec As clsBeStock_se_rec In pListStockRecSer.FindAll(Function(b) b.IdStockRec = ObjS.IdStockRec)

                                If BeStockSeRec.IsNew Then
                                    BeStockSeRec.Fec_agr = Now
                                    BeStockSeRec.Fec_mod = Now
                                    vRegistros += Insertar(BeStockSeRec,
                                                          lConnection,
                                                          lTransaction)
                                Else
                                    BeStockSeRec.Fec_mod = Now
                                    vRegistros += Actualizar(BeStockSeRec,
                                                           lConnection,
                                                           lTransaction)
                                End If

                            Next

                        Next

                        Guarda_Stock_Se_Rec = vRegistros

                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Insertar_Stock_Serializado_Recepcion(ByVal bo As clsBeStock_rec,
                                                         ByVal IdStock As Integer,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction)

        Try

            Dim lMaxSE As Integer = clsLnStock_se.MaxID(lConnection, lTransaction)

            Dim lStockSerializadoRec As New List(Of clsBeStock_se_rec)

            lStockSerializadoRec = GetAllSerieByIdStockRec(bo.IdStockRec, lConnection, lTransaction)

            For Each Stock_Se_Rec As clsBeStock_se_rec In lStockSerializadoRec

                Dim ObjS As New clsBeStock_se()

                clsPublic.CopyObject(Stock_Se_Rec, ObjS)

                lMaxSE += 1
                ObjS.IdStock = IdStock
                ObjS.IdStockSe = lMaxSE

                Stock_Se_Rec.Regularizado = True
                Stock_Se_Rec.Fecha_regularizacion = Now

                Insertar(Stock_Se_Rec, lConnection, lTransaction)

            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

End Class
