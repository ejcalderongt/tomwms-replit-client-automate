Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_re_op


    Public Shared Sub Delete(ByVal pIdOperadorRec As Integer, ByVal pIdRecepcionEnc As Integer)

        Try


            Dim sp As String = String.Format("DELETE FROM trans_re_op WHERE IdOperadorRec={0} AND IdRecepcionEnc={1}", pIdOperadorRec, pIdRecepcionEnc)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction)
                        lCommand.CommandType = CommandType.Text
                        lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Public Shared Function Get_All_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeTrans_re_op)

        Get_All_By_IdRecepcionEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_re_op)

            Dim vSQL As String = "SELECT op_rec.*, op.usa_hh FROM trans_re_op AS op_rec 
                                  INNER JOIN operador_bodega AS b ON op_rec.IdOperadorBodega = b.IdOperadorBodega 
                                  INNER JOIN operador AS op ON b.IdOperador = op.IdOperador
                                  WHERE op_rec.IdRecepcionEnc=@IdRecepcionEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_op

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_op

                                Cargar(Obj, lRow)
                                Obj.UsaHH = IIf(IsDBNull(lRow.Item("usa_hh")), False, lRow.Item("usa_hh"))
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
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdRecEnc:=pIdRecepcionEnc, pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdCuadrilla(ByVal pIdCuadrilla As Integer) As List(Of clsBeCuadrilla_det_operador)

        Get_All_By_IdCuadrilla = Nothing

        Try

            Dim lReturnList As New List(Of clsBeCuadrilla_det_operador)

            Dim vSQL As String = "SELECT op.* FROM trans_re_op AS op 
                            INNER JOIN operador_bodega AS b ON op.IdOperadorBodega = b.IdOperadorBodega 
                            WHERE op.IdRecepcionEnc=@IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", pIdCuadrilla)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeCuadrilla_det_operador

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeCuadrilla_det_operador

                                clsLnCuadrilla_det_operador.Cargar(Obj, lRow)

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
            'MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Operadores_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_op)

        Get_All_Operadores_By_IdRecepcionEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_re_op)

            Dim vSQL As String = "SELECT op.* FROM trans_re_op AS op 
              INNER JOIN operador_bodega AS b ON op.IdOperadorBodega = b.IdOperadorBodega 
              WHERE op.IdRecepcionEnc=@IdRecepcionEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_re_op

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_re_op

                        Cargar(Obj, lRow)

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            'MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdRecEnc:=pIdRecepcionEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdRecepcionEnc As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(IdOperadorRec),0) FROM trans_re_op WHERE IdRecepcionEnc={0}", pIdRecepcionEnc)

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction)
                lCommand.CommandType = CommandType.Text
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Guarda_Trans_Re_Op(ByVal IdRecepcionEnc As Integer,
                                         ByVal pListRecOpe As List(Of clsBeTrans_re_op),
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction)

        Try

            Dim lMaxIdO As Integer = MaxID(IdRecepcionEnc, lConnection, lTransaction)

            For Each Obj As clsBeTrans_re_op In pListRecOpe
                If Obj.IsNew AndAlso Obj.UsaHH Then
                    lMaxIdO += 1
                    Obj.IdOperadorRec = lMaxIdO
                    Obj.IdRecepcionEnc = IdRecepcionEnc
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    If Obj.UsaHH Then
                        Actualizar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdRecEnc:=IdRecepcionEnc, pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Sub

    Public Shared Function Eliminar(ByVal IdRecepcionEnc As Integer,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_re_op" &
             "  Where (IdRecepcionEnc = @IdRecepcionEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IdRecepcionEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

End Class
