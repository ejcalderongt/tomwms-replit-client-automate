Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_ubic_hh_op

    ''' <summary>
    ''' Busca el detalle para un cambio de Ubicacion trans_ubic_hh_enc
    ''' </summary>
    ''' <param name="pIdTransUbicHhEnc">Filtra el detalle asociado a un IdTransUbicHhEnc</param>
    ''' <returns>Devuelve lista de objetos de tipo: clsBeTrans_ubic_hh_det</returns>
    ''' <remarks>Bcuscul_13052016</remarks>
    Public Shared Function Get_All_By_IdTareaUbicacion(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_op)

        Dim lReturnList As New List(Of clsBeTrans_ubic_hh_op)

        Try

            Dim vSQL As String = "SELECT * from trans_ubic_hh_op where IdTareaUbicacionEnc = @IdTareaUbicacionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", pIdTransUbicHhEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_ubic_hh_op

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_ubic_hh_op
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Delete(ByVal pIdTransUbicOp As Integer, ByVal pIdTransUbicHhEnc As Integer)

        Try

            Dim sp As String = String.Format("DELETE FROM trans_ubic_hh_op WHERE IdTransUbicHhOp={0} AND   IdTareaUbicacionEnc={1}", pIdTransUbicOp, pIdTransUbicHhEnc)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection)

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

    Public Shared Function MaxID(ByRef lConnection As SqlConnection, ByRef ltransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdTransUbicHhOp),0) FROM trans_ubic_hh_op "

            Using lCommand As New SqlCommand(vSQL, lConnection, ltransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guarda_Operadores(ByVal IdTareaUbicacionEnc As Integer,
                                            ByVal pListObjOp As List(Of clsBeTrans_ubic_hh_op),
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As Boolean

        Guarda_Operadores = False

        Try


            Dim lMaxOp As Integer = MaxID(lConnection, lTransaction)

            If pListObjOp IsNot Nothing AndAlso pListObjOp.Count > 0 Then

                For Each OBj As clsBeTrans_ubic_hh_op In pListObjOp

                    If Not Existe_Operador(OBj, lConnection, lTransaction) Then
                        lMaxOp += 1
                        OBj.IdTransUbicHhOp = lMaxOp
                        OBj.IdTareaUbicacionEnc = IdTareaUbicacionEnc
                        Insertar(OBj, lConnection, lTransaction)
                    End If

                Next

                Guarda_Operadores = True

            End If


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

        Return Guarda_Operadores

    End Function

    Public Shared Function Existe_Operador(ByRef oBeTrans_ubic_hh_op As clsBeTrans_ubic_hh_op,
                                    ByRef lConnection As SqlConnection,
                                    ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Operador = False

        Try

            Dim sp As String = "SELECT * FROM Trans_ubic_hh_op" &
            " Where(IdOperadorBodega = @IdOperadorBodega)" &
            "AND (IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_ubic_hh_op.IdOperadorBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_op.IdTareaUbicacionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Existe_Operador = dt.Rows.Count > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guarda_Operadores_By_Enc(ByVal pIdTareaUbicacionEnc As clsBeTrans_ubic_hh_enc,
                                                    ByVal pListObjOp As List(Of clsBeTrans_ubic_hh_op),
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As Boolean

        Guarda_Operadores_By_Enc = False

        Try

            Dim lMaxOp As Integer = MaxID(lConnection, lTransaction)

            '#CKFK20230126 Agregué esto porque cuando se cambian todos los operadores se quedaban creados en la tabla
            If pIdTareaUbicacionEnc.Operador_por_linea Then

                clsLnTrans_ubic_hh_op.Eliminar_Operadores_By_IdTareaUbicacionEnc(pIdTareaUbicacionEnc.IdTareaUbicacionEnc,
                                                                                     lConnection,
                                                                                     lTransaction)

            End If

            If pListObjOp IsNot Nothing AndAlso pListObjOp.Count > 0 Then

                If Not pIdTareaUbicacionEnc.Operador_por_linea Then

                    If pIdTareaUbicacionEnc.IsNew Then

                        For Each OBj As clsBeTrans_ubic_hh_op In pListObjOp

                            If Not Existe_Operador(OBj, lConnection, lTransaction) Then
                                OBj.IdTransUbicHhOp = lMaxOp
                                OBj.IdTareaUbicacionEnc = pIdTareaUbicacionEnc.IdTareaUbicacionEnc
                                Insertar(OBj, lConnection, lTransaction)
                                lMaxOp += 1
                            End If
                        Next

                    Else

                        For Each OBj As clsBeTrans_ubic_hh_op In pListObjOp

                            If Not Existe_Operador(OBj, lConnection, lTransaction) Then
                                OBj.IdTransUbicHhOp = lMaxOp
                                OBj.IdTareaUbicacionEnc = pIdTareaUbicacionEnc.IdTareaUbicacionEnc
                                Insertar(OBj, lConnection, lTransaction)
                                lMaxOp += 1
                            Else
                                Actualizar(OBj, lConnection, lTransaction)
                            End If

                        Next

                    End If

                    Guarda_Operadores_By_Enc = True

                Else

                    For Each OBj As clsBeTrans_ubic_hh_op In pListObjOp

                        If Not Existe_Operador(OBj, lConnection, lTransaction) Then
                            OBj.IdTransUbicHhOp = lMaxOp
                            OBj.IdTareaUbicacionEnc = pIdTareaUbicacionEnc.IdTareaUbicacionEnc
                            Insertar(OBj, lConnection, lTransaction)
                            lMaxOp += 1
                        Else
                            '#EJC20220503:No atualizar de momento.
                            Actualizar_Operador(OBj, lConnection, lTransaction)
                        End If

                    Next

                    Guarda_Operadores_By_Enc = True

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

        Return Guarda_Operadores_By_Enc

    End Function

End Class