Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnOperador
    Implements IDisposable

    Public Shared Sub GuardarTransaccion(ByVal pListObjO As List(Of clsBeOperador))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeOperador In pListObjO
                If Obj.IsNew Then
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            lConnection.Close()
        End Try

    End Sub

    Public Shared Function Listar(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM VW_Operador WHERE 1 > 0 "

            If pActivo Then
                sp += " AND Activo=1"
            Else
                sp += " AND Activo=0"
            End If

            If String.IsNullOrEmpty(pFiltro) = False Then
                sp += " AND (Código LIKE '%@Código%'"
                sp += " OR Empresa LIKE '%@Empresa%'"
                sp += " OR Nombres LIKE '%@Nombres%'"
                sp += " OR Apellidos LIKE '%@Apellidos%'"
                sp += " OR Dirección LIKE '%@Dirección%'"
                sp += " OR Teléfono LIKE '%@Teléfono%')"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            If String.IsNullOrEmpty(pFiltro) = False Then
                dad.SelectCommand.Parameters.AddWithValue("@Código", pFiltro)
                dad.SelectCommand.Parameters.AddWithValue("@Empresa", pFiltro)
                dad.SelectCommand.Parameters.AddWithValue("@Nombres", pFiltro)
                dad.SelectCommand.Parameters.AddWithValue("@Apellidos", pFiltro)
                dad.SelectCommand.Parameters.AddWithValue("@Dirección", pFiltro)
                dad.SelectCommand.Parameters.AddWithValue("@Teléfono", pFiltro)
            End If

            Dim dt As New DataTable
            dad.Fill(dt)

            Listar = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Sub Delete(ByVal pIdEmpresa As Integer)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("DELETE FROM operador WHERE IdEmpresa={0}", pIdEmpresa), lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    lCommand.ExecuteNonQuery()
                    lConnection.Close()

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Exists(ByVal pIdOperador As Integer) As Boolean

        Dim lExists As Boolean = False

        Dim vSQL As String = "SELECT COUNT(1) FROM operador WHERE IdOperador=@IdOperador"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)

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

    Public Shared Function ActualizarDatos(ByVal pObjBEJ As clsBeOperador,
                                           ByVal pListObjPB As List(Of clsBeOperador_bodega))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Actualizar(pObjBEJ, lConnection, lTransaction)

            Dim lMax As Integer = clsLnOperador_bodega.MaxID(lConnection, lTransaction)

            If pListObjPB Is Nothing Then
                pListObjPB = clsLnOperador_bodega.Get_All_By_IdOperador(pObjBEJ.IdOperador, lConnection, lTransaction)
            End If

            For Each Obj As clsBeOperador_bodega In pListObjPB
                If Obj.IdOperadorBodega = 0 Then
                    lMax += 1
                    Obj.IdOperadorBodega = lMax
                    Obj.Activo = True
                    clsLnOperador_bodega.Insertar(Obj, lConnection, lTransaction)
                Else
                    clsLnOperador_bodega.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            '#CKFK20220702 Puse esto en comentario porque no era  necesario
            'Dim pListObjB As New List(Of clsBeBodega)
            'Dim pBeOperadorBodega As New clsBeOperador_bodega

            'pListObjB = clsLnBodega.Get_All(lConnection, lTransaction)

            'For Each Obj As clsBeBodega In pListObjB

            '    pBeOperadorBodega = New clsBeOperador_bodega
            '    pBeOperadorBodega.IdBodega = Obj.IdBodega
            '    pBeOperadorBodega.IdOperador = pObjBEJ.IdOperador

            '    If Not clsLnOperador_bodega.Existe_Operador(pObjBEJ.IdOperador, Obj.IdBodega, lConnection, lTransaction) Then

            '        lMax += 1
            '        pBeOperadorBodega.IdOperadorBodega = lMax
            '        pBeOperadorBodega.Activo = True
            '        pBeOperadorBodega.IsNew = True
            '        pBeOperadorBodega.User_agr = 1
            '        pBeOperadorBodega.User_mod = 1
            '        pBeOperadorBodega.Fec_agr = Now
            '        pBeOperadorBodega.Fec_mod = Now
            '        clsLnOperador_bodega.Insertar(pBeOperadorBodega, lConnection, lTransaction)
            '    Else
            '        clsLnOperador_bodega.Get_OperadorBodega_By_IdOperador_By_Bodega(pBeOperadorBodega, lConnection, lTransaction)
            '        pBeOperadorBodega.Activo = False
            '        pBeOperadorBodega.User_mod = 1
            '        pBeOperadorBodega.Fec_mod = Now
            '        clsLnOperador_bodega.Actualizar(pBeOperadorBodega, lConnection, lTransaction)
            '    End If

            'Next

            lTransaction.Commit()

            lConnection.Close()

            Return True

        Catch ex As Exception
            lTransaction.Rollback()
            lConnection.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function Activar_Desactivar_Operador(ByVal pObjBEJ As clsBeOperador,
                                                       ByVal Activo As Boolean) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            pObjBEJ.Activo = Activo

            Actualizar(pObjBEJ, lConnection, lTransaction)

            Dim lMax As Integer = clsLnOperador_bodega.MaxID(lConnection, lTransaction)
            Dim pListObjPB As New List(Of clsBeOperador_bodega)

            pListObjPB = clsLnOperador_bodega.Get_All_By_IdOperador(pObjBEJ.IdOperador, lConnection, lTransaction)

            For Each Obj As clsBeOperador_bodega In pListObjPB
                Obj.Activo = Activo
                clsLnOperador_bodega.Actualizar(Obj, lConnection, lTransaction)
            Next

            lTransaction.Commit()

            lConnection.Close()

            Return True

        Catch ex As Exception
            lTransaction.Rollback()
            lConnection.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Operador_By_Nombre_And_Apellido(ByRef pBeOperador As clsBeOperador,
                                               Optional ByVal pConection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Const sp As String = " SELECT * FROM Operador" &
                                 " Where(CONVERT(NVARCHAR(100),nombres) + ' ' + CONVERT(NVARCHAR(100),apellidos) = @Nombres) AND (IdEmpresa = @IdEmpresa)"

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Nombres", pBeOperador.Nombres))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEmpresa", pBeOperador.IdEmpresa))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeOperador, dt.Rows(0))
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            Return True

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

    Public Shared Function Get_IdOperadorBodega_Defecto(ByVal pIdBodega As Integer) As List(Of clsBeOperador)

        Try

            Dim lReturnList As New List(Of clsBeOperador)
            Const sp As String = "select * from operador o inner join operador_bodega ob on o.IdOperador = ob.IdOperador where ob.IdBdoega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)
                    Dim dt As New DataTable

                    dad.Fill(dt)

                    Dim vBeOperador As New clsBeOperador

                    For Each dr As DataRow In dt.Rows

                        vBeOperador = New clsBeOperador
                        Cargar(vBeOperador, dr)
                        lReturnList.Add(vBeOperador)

                    Next

                    lConnection.Dispose()
                    cmd.Dispose()

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdOperadorBodega_Defecto(ByVal pIdBodega As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As clsBeOperador


        Get_IdOperadorBodega_Defecto = Nothing

        Try

            Const sp As String = "select top(1) * from operador o 
                                  inner join operador_bodega ob on o.IdOperador = ob.IdOperador 
                                  where ob.IdBodega = @IdBodega and o.activo = 1 and ob.activo = 1"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeOperador As New clsBeOperador

            For Each dr As DataRow In dt.Rows

                vBeOperador = New clsBeOperador
                Cargar(vBeOperador, dr)
                Get_IdOperadorBodega_Defecto = vBeOperador

            Next

            cmd.Dispose()


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Resoluciones_Operador() As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Resoluciones_Operador "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.CommandTimeout = 100
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#MA20251204'
    Public Shared Function Existe(ByVal op As clsBeOperador) As clsBeOperador

        Existe = Nothing

        Dim vSQL As String = "SELECT * FROM operador WHERE Codigo = @codigo AND Clave = @clave"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@codigo", op.Codigo)
                    lCommand.Parameters.AddWithValue("@clave", op.Clave)



                    Dim dt As New DataTable()

                    Using da As New SqlDataAdapter(lCommand)
                        da.Fill(dt)
                    End Using

                    If dt.Rows.Count > 0 Then
                        Existe = New clsBeOperador
                        Dim operadorEncontrado As New clsBeOperador()
                        Cargar(operadorEncontrado, dt.Rows(0))

                        Existe = operadorEncontrado

                    End If


                End Using

            End Using



        Catch ex As Exception
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

End Class
