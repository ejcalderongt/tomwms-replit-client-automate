Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnOperador_bodega
    Implements IDisposable

    Public Shared Function Get_All_For_Cuadrilla_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT ob.IdOperadorBodega,o.Nombres,
                                      o.usa_hh, 
                                      o.recibe, 
                                      o.ubica,
                                      o.transporta, 
                                      o.pickea, 
                                      o.verifica,
                                      o.costo_hora
                                      FROM operador_bodega AS ob 
                                      INNER JOIN operador AS o ON ob.IdOperador = o.IdOperador 
                                      INNER JOIN bodega AS b ON ob.IdBodega = b.IdBodega 
                                      WHERE ob.IdBodega=@IdBodega and o.activo=1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_DT(ByVal pIdBodega As Integer) As DataTable

        Get_All_By_IdBodega_DT = Nothing

        Try

            Dim vSQL As String = "SELECT ob.IdOperadorBodega,ISNULL(o.Nombres,'') + ' ' + ISNULL(o.apellidos,'') AS Nombres,o.usa_hh, o.Foto 
                                  FROM operador_bodega AS ob 
                                  INNER JOIN operador AS o ON ob.IdOperador = o.IdOperador 
                                  INNER JOIN bodega AS b ON ob.IdBodega = b.IdBodega 
                                  WHERE ob.IdBodega=@IdBodega and o.activo=1 and ob.activo = 1  
                                  ORDER BY ISNULL(o.Nombres,'') + ' ' + ISNULL(o.apellidos,'') "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.CommandTimeout = 60
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Get_All_By_IdBodega_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_For_Tarea_DT(ByVal pIdBodega As Integer,
                                                            ByVal pTipoTarea As clsDataContractDI.tTipoTarea) As DataTable

        Get_All_By_IdBodega_For_Tarea_DT = Nothing

        Try

            Dim vSQL As String = "SELECT ob.IdOperadorBodega,ISNULL(o.Nombres,'') + ' ' + ISNULL(o.apellidos,'') AS Nombres,o.usa_hh, o.Foto 
                                   FROM operador_bodega AS ob 
                                   INNER JOIN operador AS o ON ob.IdOperador = o.IdOperador 
                                   INNER JOIN bodega AS b ON ob.IdBodega = b.IdBodega 
                                   WHERE ob.IdBodega=@IdBodega and o.activo=1 and ob.activo = 1 AND o.usa_hh = 1 "

            If pTipoTarea = clsDataContractDI.tTipoTarea.PICK Then
                vSQL += " AND (o.pickea = 1 OR o.verifica = 1)"
            ElseIf pTipoTarea = clsDataContractDI.tTipoTarea.RECE Then
                vSQL += " AND (o.recibe = 1) "
            ElseIf pTipoTarea = clsDataContractDI.tTipoTarea.UBIC Then
                vSQL += " AND (o.ubica = 1) "
            End If

            vSQL += " ORDER BY ISNULL(o.Nombres,'') + ' ' + ISNULL(o.apellidos,'') "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Get_All_By_IdBodega_For_Tarea_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_DT_For_Combo(ByVal pIdBodega As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT ob.IdOperadorBodega as Id,
                                      ISNULL(o.Nombres,'') + ' ' + ISNULL(o.apellidos,'') AS Nombres,
                                      o.usa_hh as Utilizacion_Handheld
                                      FROM operador_bodega AS ob 
                                      INNER JOIN operador AS o ON ob.IdOperador = o.IdOperador 
                                      INNER JOIN bodega AS b ON ob.IdBodega = b.IdBodega 
                                      WHERE ob.IdBodega=@IdBodega and o.activo=1 "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)
                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdOperador(ByVal pIdOperador As Integer) As List(Of clsBeOperador_bodega)

        Dim lReturnList As New List(Of clsBeOperador_bodega)

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim vSQL As String = "SELECT * FROM operador_bodega WHERE IdOperador=@IdOperador"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeOperador_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeOperador_bodega
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

        End Using

        Return lReturnList

    End Function

    Public Shared Function Get_All_By_IdOperador(ByVal pIdOperador As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As List(Of clsBeOperador_bodega)

        Dim lReturnList As New List(Of clsBeOperador_bodega)

        Try

            Dim vSQL As String = "SELECT * FROM operador_bodega WHERE IdOperador=@IdOperador"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeOperador_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeOperador_bodega
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Function

    Public Shared Function Get_All_By_IdBodega_HH(ByVal Idbodega As Integer) As List(Of clsBeOperador_bodega)

        Get_All_By_IdBodega_HH = Nothing

        Try

            Dim vSQL As String = "SELECT operador_bodega.* 
                                  FROM operador_bodega INNER JOIN 
                                       operador ON  operador_bodega.IdOperador = operador.IdOperador
                                  WHERE operador_bodega.Idbodega=@Idbodega AND
                                        operador_bodega.Activo =1 AND 
                                        operador.usa_hh = 1"

            Dim lReturnList As New List(Of clsBeOperador_bodega)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Idbodega", Idbodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeOperador_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeOperador_bodega
                                CargarHH(Obj, lRow, lConnection, lTransaction)
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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal Idbodega As Integer) As List(Of clsBeOperador_bodega)

        Get_All_By_IdBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeOperador_bodega)
            Dim vSQL As String = "SELECT * FROM operador_bodega WHERE Idbodega=@Idbodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Idbodega", Idbodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeOperadorBodea As clsBeOperador_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeOperadorBodea = New clsBeOperador_bodega
                                CargarHH(BeOperadorBodea, lRow, lConnection, ltransaction)
                                lReturnList.Add(BeOperadorBodea)

                            Next

                            Return lReturnList

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal Idbodega As Integer,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeOperador_bodega)

        Get_All_By_IdBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeOperador_bodega)

            Dim vSQL As String = "SELECT * FROM operador_bodega WHERE Idbodega=@Idbodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Idbodega", Idbodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeOperador_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeOperador_bodega
                        CargarHH(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                    Return lReturnList

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub CargarHH(ByRef oBeOperador_bodega As clsBeOperador_bodega,
                               ByRef dr As DataRow,
                               ByRef lConnection As SqlConnection,
                               ByRef lTransaction As SqlTransaction)

        Try

            With oBeOperador_bodega

                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                .IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Operador.IdOperador = .IdOperador
                clsLnOperador.Obtener(.Operador, lConnection, lTransaction)

            End With

        Catch ex As Exception
            Throw New Exception("Operador_bodega_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Shared Function Operador_Valido(ByRef oBeOperador As clsBeOperador_bodega) As Boolean

        Operador_Valido = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "SELECT * FROM operador OP INNER JOIN 
                                operador_bodega opb on op.IdOperador = opb.IdOperador 
                                WHERE (op.IdOperador = @IDOPERADOR 
                                AND op.Clave =@CLAVE 
                                AND opb.IdBodega =@IDBODEGA) "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador.IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CLAVE", oBeOperador.Operador.Clave))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", oBeOperador.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                CargarHH(oBeOperador, dt.Rows(0), lConnection, lTransaction)
                Operador_Valido = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Operador_Valido(ByRef pBeOperadorBodega As clsBeOperador_bodega,
                                           ByRef pCodigoBodega As String,
                                           ByRef pIdProductoEstadoNE As Integer) As Boolean

        Operador_Valido = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "SELECT opb.*, OP.*, b.codigo as codigo_bodega, b.IdProductoEstadoNE 
                                FROM operador OP INNER JOIN 
                                operador_bodega opb on op.IdOperador = opb.IdOperador 
                                INNER JOIN bodega b on b.IdBodega = opb.idbodega
                                WHERE (op.IdOperador = @IDOPERADOR 
                                AND op.Clave =@CLAVE 
                                AND opb.IdBodega =@IDBODEGA) "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", pBeOperadorBodega.IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CLAVE", pBeOperadorBodega.Operador.Clave))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeOperadorBodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                CargarHH(pBeOperadorBodega, dt.Rows(0), lConnection, lTransaction)
                pCodigoBodega = IIf(IsDBNull(dt.Rows(0).Item("codigo_bodega")), "ND?", dt.Rows(0).Item("codigo_bodega"))
                pIdProductoEstadoNE = IIf(IsDBNull(dt.Rows(0).Item("IdProductoEstadoNE")), "0", dt.Rows(0).Item("IdProductoEstadoNE"))
                Operador_Valido = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOperadorBodega),0) FROM Operador_bodega"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdOperador_By_IdOperadorBodega(ByVal pIdOperadorBodega) As Integer

        Get_IdOperador_By_IdOperadorBodega = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT IdOperador FROM Operador_bodega " &
            " Where(IdOperadorBodega = @IdOperadorBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", pIdOperadorBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_IdOperador_By_IdOperadorBodega = dt.Rows(0).Item("IdOperador")
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_IdOperadorBodega_By_IdOperador(ByVal pIdOperador As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              Optional ByVal pConection As SqlConnection = Nothing,
                                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Get_IdOperadorBodega_By_IdOperador = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Const sp As String = "SELECT IdOperadorBodega FROM Operador_bodega " &
            " Where(IdOperador = @IdOperador) AND (IdBodega = @IdBodega)"

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", pIdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_IdOperadorBodega_By_IdOperador = dt.Rows(0).Item("IdOperadorBodega")
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Get_IdOperadorBodega_By_IdOperador_Activo(ByVal pIdOperador As Integer,
                                                                     ByVal pIdBodega As Integer,
                                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Get_IdOperadorBodega_By_IdOperador_Activo = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Const sp As String = "SELECT IdOperadorBodega FROM Operador_bodega " &
            " Where(IdOperador = @IdOperador) AND (IdBodega = @IdBodega) AND activo = 1 "

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", pIdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_IdOperadorBodega_By_IdOperador_Activo = dt.Rows(0).Item("IdOperadorBodega")
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Get_IdOperadorBodega_Default(ByVal Idbodega As Integer,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As Integer

        Get_IdOperadorBodega_Default = Nothing

        Try

            Dim lReturnList As New List(Of clsBeOperador_bodega)


            Dim vSQL As String = "select top 1 IdOperadorBodega from operador_bodega ob inner join operador op
                                  on ob.IdOperador = op.IdOperador where ob.IdBodega=@Idbodega and op.activo=1 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Idbodega", Idbodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count = 1 Then
                    Get_IdOperadorBodega_Default = lDataTable.Rows(0).Item("IdOperadorBodega")
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Existe_Operador(ByVal IdOperador As Integer,
                                           ByVal IdBodega As Integer,
                                           ByRef lConnection As SqlConnection,
                                           ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Operador = False

        Try

            Const sp As String = "SELECT * FROM operador_bodega  
                                  Where(IdOperador = @IdOperador) AND (IdBodega = @IdBodega)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("IdOperador", IdOperador)
                lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", IdBodega)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_Operador = True
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_OperadorBodega_By_IdOperadoBodega(ByVal pIdOperadorBodega As Integer,
                                                                 Optional ByVal pConection As SqlConnection = Nothing,
                                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeOperador_bodega


        Get_OperadorBodega_By_IdOperadoBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Const sp As String = "SELECT * FROM Operador_bodega " &
                                 " Where(IdOPeradorBodega = @IdOPeradorBodega) "

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", pIdOperadorBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim BeOperadorBodega As New clsBeOperador_bodega
                Cargar(BeOperadorBodega, dt.Rows(0))
                BeOperadorBodega.Operador = New clsBeOperador()
                BeOperadorBodega.Operador = clsLnOperador.Get_Single_By_IdOperador(BeOperadorBodega.IdOperador, lConnection, lTransaction)
                Get_OperadorBodega_By_IdOperadoBodega = BeOperadorBodega
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Get_OperadorBodega_By_IdOperador_By_Bodega(ByRef BeOperadorBodega As clsBeOperador_bodega,
                                                                      Optional ByVal pConection As SqlConnection = Nothing,
                                                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean


        Get_OperadorBodega_By_IdOperador_By_Bodega = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Const sp As String = "SELECT * FROM Operador_bodega " &
            " Where(IdOperador = @IdOperador) AND (IdBodega = @IdBodega)"

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", BeOperadorBodega.IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", BeOperadorBodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(BeOperadorBodega, dt.Rows(0))
                Get_OperadorBodega_By_IdOperador_By_Bodega = True
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220611:Obtener los operadores que pueden hacer picking.
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Operador_For_Picking_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeOperador_bodega)

        Get_All_Operador_For_Picking_By_IdBodega = Nothing

        Dim lReturnList As New List(Of clsBeOperador_bodega)

        Try

            Dim vSQL As String = "SELECT ob.*
                                  FROM operador_bodega AS ob 
                                  INNER JOIN operador AS o ON ob.IdOperador = o.IdOperador 
                                  INNER JOIN bodega AS b ON ob.IdBodega = b.IdBodega 
                                  WHERE ob.IdBodega=@IdBodega AND o.activo=1 AND o.Pickea = 1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If Not lDataTable Is Nothing Then

                            If lDataTable.Rows.Count > 0 Then

                                Dim BeOperadorBodega As New clsBeOperador_bodega

                                For Each lRow As DataRow In lDataTable.Rows

                                    BeOperadorBodega = New clsBeOperador_bodega
                                    CargarHH(BeOperadorBodega, lRow, lConnection, lTransaction)
                                    lReturnList.Add(BeOperadorBodega)

                                Next

                                Get_All_Operador_For_Picking_By_IdBodega = lReturnList

                            End If

                        End If


                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_DT(ByVal pIdBodega As Integer,
                                                  ByVal pConnection As SqlConnection,
                                                  ByVal pTransaction As SqlTransaction) As DataTable

        Get_All_By_IdBodega_DT = Nothing

        Try

            Dim vSQL As String = "SELECT ob.IdOperadorBodega,ISNULL(o.Nombres,'') + ' ' + ISNULL(o.apellidos,'') AS Nombres,o.usa_hh, o.Foto 
                                  FROM operador_bodega AS ob 
                                  INNER JOIN operador AS o ON ob.IdOperador = o.IdOperador 
                                  INNER JOIN bodega AS b ON ob.IdBodega = b.IdBodega 
                                  WHERE ob.IdBodega=@IdBodega and o.activo=1 and ob.activo = 1  
                                  ORDER BY ISNULL(o.Nombres,'') + ' ' + ISNULL(o.apellidos,'') "

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.CommandTimeout = 60
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)
                Get_All_By_IdBodega_DT = lDataTable

            End Using


        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#GT19052025: obtener operador_bodega para enviar datos a la nube
    Public Shared Function GetSingle_By_IdOperadorBodega(ByVal pIdOperadorBodega As Integer, ByVal pConnection As SqlConnection,
                                                                                             ByVal pTransaction As SqlTransaction) As clsBeOperador_bodega

        GetSingle_By_IdOperadorBodega = Nothing

        Try

            Const sp As String = " SELECT * FROM Operador_bodega " &
                                 " Where(IdOperadorBodega = @IdOperadorBodega)"

            Using lDTA As New SqlDataAdapter(sp, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", pIdOperadorBodega))
                Dim dt As New DataTable
                lDTA.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Dim pBeOperador_bodega As New clsBeOperador_bodega()
                    Cargar(pBeOperador_bodega, dt.Rows(0))
                    GetSingle_By_IdOperadorBodega = pBeOperador_bodega
                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function



#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class