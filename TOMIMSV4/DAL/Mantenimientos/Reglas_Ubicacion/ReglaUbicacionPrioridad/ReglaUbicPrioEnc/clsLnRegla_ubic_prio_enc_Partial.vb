Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRegla_ubic_prio_enc

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer,
                                               ByVal pActivo As Boolean) As List(Of clsBeRegla_ubic_prio_enc)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeRegla_ubic_prio_enc)

            Const sp As String = "SELECT * FROM regla_ubic_prio_enc 
                                  WHERE (IdBodega=@IdBodega AND (Activo=@Activo))"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubic_enc As New clsBeRegla_ubic_prio_enc

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubic_enc = New clsBeRegla_ubic_prio_enc
                Cargar(vBeRegla_ubic_enc, dr)
                lReturnList.Add(vBeRegla_ubic_enc)

            Next

            lTransaction.Commit()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Guardar_Regla_Prioridad(ByVal pReglaEnc As clsBeRegla_ubic_prio_enc) As Boolean

        Guardar_Regla_Prioridad = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            'Guarda/Actualiza encabezado de la regla.
            If pReglaEnc.IsNew Then
                'Ensure the Id is unic in the transacction.
                'pReglaEnc.IdReglaUbicacionEnc = MaxID(lConnection,lTransaction)
                Insertar(pReglaEnc, lConnection, lTransaction)
            Else
                Actualizar(pReglaEnc, lConnection, lTransaction)
            End If

            Dim lMaxID As Integer = -1

            'Insert/update the detail of the rule by owner 
            If Not pReglaEnc.lReglaUbicPrioDet Is Nothing Then

                lMaxID = clsLnRegla_ubic_prio_det.MaxID(lConnection, lTransaction)

                For Each Prio As clsBeRegla_ubic_prio_det In pReglaEnc.lReglaUbicPrioDet

                    'If the Id change while MaxId() is calculated the list, its begin updated with the new Id.
                    Prio.IdReglaUbicPrioEnc = pReglaEnc.IdReglaUbicPrioEnc

                    If Prio.IsNew Then
                        lMaxID += 1
                        Prio.IdReglaUbicPrioDet = lMaxID
                        clsLnRegla_ubic_prio_det.Insertar(Prio, lConnection, lTransaction)
                    Else
                        clsLnRegla_ubic_prio_det.Actualizar(Prio, lConnection, lTransaction)
                    End If

                Next

            End If

            lTransaction.Commit()

            Guardar_Regla_Prioridad = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_With_Details(ByRef oBeRegla_ubic_prio_enc As clsBeRegla_ubic_prio_enc) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Regla_ubic_prio_enc 
			                      Where(IdReglaUbicPrioEnc = @IdReglaUbicPrioEnc) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAUBICPRIOENC", oBeRegla_ubic_prio_enc.IdReglaUbicPrioEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeRegla_ubic_prio_enc, dt.Rows(0))
                oBeRegla_ubic_prio_enc.lReglaUbicPrioDet = clsLnRegla_ubic_prio_det.Get_All_By_IdReglaUbicPrioEnc(oBeRegla_ubic_prio_enc.IdReglaUbicPrioEnc, lConnection, lTransaction)
                oBeRegla_ubic_prio_enc.IsNew = False
            End If

            lTransaction.Commit()

            Return True

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_With_Details_By_IdProducto(ByVal IdProducto As Integer,
                                                                 ByRef oBeRegla_ubic_prio_enc As clsBeRegla_ubic_prio_enc,
                                                                 Optional ByVal BuscarDefault As Boolean = False) As Boolean

        oBeRegla_ubic_prio_enc = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If Not BuscarDefault Then

                Const sp As String = "SELECT * FROM Regla_ubic_prio_enc A 
                                      INNER JOIN regla_ubic_prio_producto B ON a.IdReglaUbicPrioEnc = b.IdReglaUbicPrioEnc 
                                      Where(A.Activo = 1) AND (B.IdProducto = @IdProducto)"

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)

                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", IdProducto))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    oBeRegla_ubic_prio_enc = New clsBeRegla_ubic_prio_enc
                    Cargar(oBeRegla_ubic_prio_enc, dt.Rows(0))
                    oBeRegla_ubic_prio_enc.lReglaUbicPrioDet = clsLnRegla_ubic_prio_det.Get_All_By_IdReglaUbicPrioEnc(oBeRegla_ubic_prio_enc.IdReglaUbicPrioEnc)
                    oBeRegla_ubic_prio_enc.IsNew = False
                Else
                    Get_Single_With_Details_By_IdProducto(IdProducto, oBeRegla_ubic_prio_enc, True)
                End If

            Else

                Const sp As String = " SELECT * FROM Regla_ubic_prio_enc A " &
                                     " Where(A.IdReglaUbicPrioEnc = @IdReglaUbicPrioEnc) "

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)


                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAUBICPRIOENC", 1))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    oBeRegla_ubic_prio_enc = New clsBeRegla_ubic_prio_enc
                    Cargar(oBeRegla_ubic_prio_enc, dt.Rows(0))
                    oBeRegla_ubic_prio_enc.lReglaUbicPrioDet = clsLnRegla_ubic_prio_det.Get_All_By_IdReglaUbicPrioEnc(oBeRegla_ubic_prio_enc.IdReglaUbicPrioEnc, lConnection, lTransaction)
                    oBeRegla_ubic_prio_enc.IsNew = False
                End If

            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_With_Details_By_IdProducto(ByVal IdProducto As Integer,
                                                                 ByRef oBeRegla_ubic_prio_enc As clsBeRegla_ubic_prio_enc,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction,
                                                                 Optional ByVal BuscarDefault As Boolean = False) As Boolean

        oBeRegla_ubic_prio_enc = Nothing

        Try

            If Not BuscarDefault Then

                Const sp As String = "SELECT * FROM Regla_ubic_prio_enc A 
                                      INNER JOIN regla_ubic_prio_producto B ON a.IdReglaUbicPrioEnc = b.IdReglaUbicPrioEnc 
                                      Where(A.Activo = 1) AND (B.IdProducto = @IdProducto)"

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)

                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", IdProducto))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    oBeRegla_ubic_prio_enc = New clsBeRegla_ubic_prio_enc
                    Cargar(oBeRegla_ubic_prio_enc, dt.Rows(0))
                    oBeRegla_ubic_prio_enc.lReglaUbicPrioDet = clsLnRegla_ubic_prio_det.Get_All_By_IdReglaUbicPrioEnc(oBeRegla_ubic_prio_enc.IdReglaUbicPrioEnc, lConnection, lTransaction)
                    oBeRegla_ubic_prio_enc.IsNew = False
                Else
                    Get_Single_With_Details_By_IdProducto(IdProducto, oBeRegla_ubic_prio_enc, lConnection, lTransaction, True)
                End If

            Else

                Const sp As String = " SELECT * FROM Regla_ubic_prio_enc A " &
                                     " Where(A.IdReglaUbicPrioEnc = @IdReglaUbicPrioEnc) "

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)


                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAUBICPRIOENC", 1))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    oBeRegla_ubic_prio_enc = New clsBeRegla_ubic_prio_enc
                    Cargar(oBeRegla_ubic_prio_enc, dt.Rows(0))
                    oBeRegla_ubic_prio_enc.lReglaUbicPrioDet = clsLnRegla_ubic_prio_det.Get_All_By_IdReglaUbicPrioEnc(oBeRegla_ubic_prio_enc.IdReglaUbicPrioEnc, lConnection, lTransaction)
                    oBeRegla_ubic_prio_enc.IsNew = False
                End If

            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdBodega(ByVal pIdBodega As Integer) As clsBeRegla_ubic_prio_enc

        Get_Single_By_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Regla_ubic_prio_enc 
			                      WHERE(IdBodega = @IdBodega) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim oBeRegla_ubic_prio_enc As New clsBeRegla_ubic_prio_enc

            If dt.Rows.Count >= 1 Then
                Cargar(oBeRegla_ubic_prio_enc, dt.Rows(0))
                oBeRegla_ubic_prio_enc.lReglaUbicPrioDet = clsLnRegla_ubic_prio_det.Get_All_By_IdReglaUbicPrioEnc(oBeRegla_ubic_prio_enc.IdReglaUbicPrioEnc, lConnection, lTransaction)
                oBeRegla_ubic_prio_enc.IsNew = False
            End If

            lTransaction.Commit()

            If dt.Rows.Count >= 1 Then
                Return oBeRegla_ubic_prio_enc
            End If

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdBodega(ByVal pIdBodega As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As clsBeRegla_ubic_prio_enc

        Get_Single_By_IdBodega = Nothing

        Try

            Const sp As String = "SELECT * FROM Regla_ubic_prio_enc 
			                      WHERE(IdBodega = @IdBodega) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim oBeRegla_ubic_prio_enc As New clsBeRegla_ubic_prio_enc

            If dt.Rows.Count >= 1 Then
                Cargar(oBeRegla_ubic_prio_enc, dt.Rows(0))
                oBeRegla_ubic_prio_enc.lReglaUbicPrioDet = clsLnRegla_ubic_prio_det.Get_All_By_IdReglaUbicPrioEnc(oBeRegla_ubic_prio_enc.IdReglaUbicPrioEnc, lConnection, lTransaction)
                oBeRegla_ubic_prio_enc.IsNew = False
                Return oBeRegla_ubic_prio_enc
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
