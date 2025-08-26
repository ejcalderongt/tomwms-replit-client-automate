Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnCliente_bodega
    Implements IDisposable

    Public Shared Sub CargarHH(ByRef oBeCliente_bodega As clsBeCliente_bodega,
                               ByRef dr As DataRow,
                               ByRef lConnection As SqlConnection,
                               ByRef lTransaction As SqlTransaction)

        Try


            With oBeCliente_bodega

                .IdClienteBodega = IIf(IsDBNull(dr.Item("IdClienteBodega")), 0, dr.Item("IdClienteBodega"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Cliente.IdCliente = .IdCliente
                clsLnCliente.ObtenerHH(.Cliente, lConnection, lTransaction)

            End With

        Catch ex As Exception
            Throw New Exception("LnClienteBodega_CargarHH: " & ex.Message)
        End Try

    End Sub

    Public Shared Function Get_All_By_IdCliente(ByVal pIdCliente As Integer) As List(Of clsBeCliente_bodega)

        Dim lReturnList As New List(Of clsBeCliente_bodega)

        Get_All_By_IdCliente = Nothing

        Try

            Dim lSQL As String = "SELECT * FROM cliente_bodega WHERE IdCliente=@IdCliente"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(lSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeClienteBodega As clsBeCliente_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeClienteBodega = New clsBeCliente_bodega
                                Cargar(BeClienteBodega, lRow)
                                lReturnList.Add(BeClienteBodega)

                            Next

                            Get_All_By_IdCliente = lReturnList

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_HH(ByVal IdBodega As Integer) As List(Of clsBeCliente_bodega)

        Dim lReturnList As New List(Of clsBeCliente_bodega)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Const lSQl As String = "SELECT * FROM cliente_bodega WHERE IdBodega = @IdBodega AND Activo = 1 "

                    Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeCliente_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeCliente_bodega
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
            Throw New Exception("GetAllByBodegaHH_GetAllByBodegaHH: " & ex.Message)
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdClienteBodega),0) FROM cliente_bodega"

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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

    Public Shared Function Insertar_From_Interface(ByRef oBeCliente_bodega As clsBeCliente_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Try

            Ins.Init("cliente_bodega")
            Ins.Add("idclientebodega", "@idclientebodega", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}


            cmd.Parameters.Add(New SqlParameter("@IDCLIENTEBODEGA", oBeCliente_bodega.IdClienteBodega))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeCliente_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_bodega.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente_bodega.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente_bodega.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef IdClienteBodega As Integer) As clsBeCliente_bodega
        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Cliente_bodega" &
            " Where(IdClienteBodega = @IdClienteBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTEBODEGA", IdClienteBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeCliente_bodega As New clsBeCliente_bodega

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_bodega, dt.Rows(0))
                Return pBeCliente_bodega
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_ClienteBodega(ByVal IdBodega As Integer, ByVal IdCliente As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim vSQL As String = " Delete from cliente_bodega" &
             "  Where(IdCliente = @IdCliente and IdBodega=@IdBodega)"

            cmd = New SqlCommand(vSQL, lConnection)
            lConnection.Open()

            cmd.Parameters.Add(New SqlParameter("@IdCliente", IdCliente))
            cmd.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

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

    Public Shared Function GetSingle(ByRef IdCliente As Integer, ByVal IdBodega As Integer) As clsBeCliente_bodega

        GetSingle = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Cliente_bodega" &
            " Where(IdCliente = @IdCliente AND IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTEBODEGA", IdCliente))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdCliente))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeCliente_bodega As New clsBeCliente_bodega

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_bodega, dt.Rows(0))
                Return pBeCliente_bodega
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef IdCliente As Integer,
                                     ByVal IdBodega As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeCliente_bodega

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Cliente_bodega " &
            " Where(IdCliente = @IdCliente AND IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTE", IdCliente))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeCliente_bodega As New clsBeCliente_bodega

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_bodega, dt.Rows(0))
                Return pBeCliente_bodega
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
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


    Public Shared Function Get_All_Prefactura_By_IdBodega_For_Combo(ByVal IdBodega As Integer) As DataTable

        Get_All_Prefactura_By_IdBodega_For_Combo = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT cliente_bodega.IdClienteBodega,cliente.IdCliente Cliente, cliente.nit NIT,
	                                              cliente.codigo as Codigo, cliente.nombre_comercial as Nombre
                                                  from cliente inner join
			                                      cliente_bodega on cliente.IdCliente = cliente_bodega.IdCliente
                                           WHERE IdBodega=@IdBodega and codigo not like 'B%' "


                    'Dim vSQL As String = " SELECT cliente_bodega.IdClienteBodega,cliente.IdCliente, cliente.nit,
                    '                           cliente.codigo as Codigo, cliente.nombre_comercial as Nombre
                    '                              from cliente inner join
                    '                     cliente_bodega on cliente.IdCliente = cliente_bodega.IdCliente
                    '                       WHERE IdBodega=@IdBodega and codigo not like 'B%' "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_Prefactura_By_IdBodega_For_Combo = lDataTable

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


End Class
