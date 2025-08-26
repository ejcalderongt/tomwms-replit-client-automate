Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors

Partial Public Class clsLnEstructura_Tramo

    Public Shared Function Procesa_Batch(ByVal IdSector As Integer,
                                         ByVal listBeEstructuraTramos As List(Of clsBeEstructura_tramo),
                                         ByVal pLimpiaSector As Boolean) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Procesa_Batch = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If listBeEstructuraTramos.Count > 0 Then

                If pLimpiaSector Then
                    Limpiar_Sector(IdSector, listBeEstructuraTramos.Item(0).IdBodega)
                End If

                For Each BeEstructuraTramo As clsBeEstructura_tramo In listBeEstructuraTramos

                    Try

                        If Not Exist(BeEstructuraTramo.IdTramo, BeEstructuraTramo.IdBodega, lConnection, lTransaction) Then
                            Insertar(BeEstructuraTramo, lConnection, lTransaction)
                        Else
                            Actualizar(BeEstructuraTramo, lConnection, lTransaction)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
                    End Try

                Next

                Procesa_Batch = True

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Crear_Default(idusuario As Integer) As clsBeEstructura_tramo

        Dim oBeEstructura_tramo As New clsBeEstructura_tramo

        Try

            With oBeEstructura_tramo
                .IdTramo = 0
                .IdSector = 0
                .Sistema = False
                .Descripcion = ""
                .User_agr = idusuario
                .Fec_agr = Date.Now
                .User_mod = idusuario
                .Fec_mod = Date.Now
                .Activo = True
                .Alto = 0
                .Largo = 0
                .Ancho = 0
                .Margen_izquierdo = 0
                .Margen_derecho = 0
                .Margen_superior = 0
                .Margen_inferior = 0
                .Codigo = 0
                .Indice_x = 0
                .Orientacion = 0
                .IdTipoProductoDefault = 0
                .IdBodega = 0
            End With

            Return oBeEstructura_tramo

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Limpiar_Sector(ByVal IdSector As Integer,
                                          ByVal IdBodega As Integer,
                                          Optional ByVal pConection As SqlConnection = Nothing,
                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Estructura_tramo  Where (Idsector = @Idsector) AND (IdBodega = @IdBodega)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@Idsector", IdSector))
            cmd.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdSector(ByVal pIdsector As Integer, pIdBodega As Integer) As List(Of clsBeEstructura_tramo)

        Dim lReturnList As New List(Of clsBeEstructura_tramo)

        Try

            'lReturnList = GetAll().FindAll(Function(x) x.IdSector = pIdsector AndAlso x.IdBodega = pIdBodega).OrderBy(Function(Y) Y.Orientacion).ThenBy(Function(z) z.Indice_x).ToList()

            lReturnList = Get_All_By_IdBodega_And_IdSector(pIdBodega, pIdsector)

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_IdSector_DT(ByVal pIdBodega As Integer, ByVal pIdSector As Integer) As DataTable

        Get_All_By_IdBodega_And_IdSector_DT = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Estructura_tramo WHERE  (IdBodega = @IdBodega AND IdSector = @IdSector) ORDER BY Orientacion, Indice_x "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Get_All_By_IdBodega_And_IdSector_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_IdSector_DT(ByVal pIdBodega As Integer,
                                                               ByVal pIdSector As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_IdBodega_And_IdSector_DT = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Estructura_tramo WHERE  (IdBodega = @IdBodega AND IdSector = @IdSector) ORDER BY Orientacion, Indice_x "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Get_All_By_IdBodega_And_IdSector_DT = lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT * FROM Estructura_tramo WHERE  (IdBodega = @IdBodega) ORDER BY descripcion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_IdSector(ByVal pIdBodega As Integer,
                                                            ByVal pIdSector As Integer) As List(Of clsBeEstructura_tramo)

        Dim lReturnList As New List(Of clsBeEstructura_tramo)
        Try

            Dim vSQL As String = "SELECT * FROM Estructura_tramo WHERE  (IdBodega = @IdBodega AND IdSector = @IdSector) ORDER BY Orientacion, Indice_x "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Dim vBeEstructura_tramo As New clsBeEstructura_tramo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeEstructura_tramo = New clsBeEstructura_tramo
                            Cargar(vBeEstructura_tramo, dr)
                            lReturnList.Add(vBeEstructura_tramo)
                        Next

                        Get_All_By_IdBodega_And_IdSector = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exist(ByVal pIdTramo As Integer,
                                 ByVal pIdBodega As Integer,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Boolean

        Exist = False

        Try

            Const sp As String = "SELECT IdTramo FROM Estructura_tramo " &
            " Where(idtramo = @idtramo AND IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", pIdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Exist = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdBodega_And_IdTramo(ByVal pIdBodega As Integer,
                                                              ByVal pIdTramo As Integer) As clsBeEstructura_tramo

        Dim lReturnList As New List(Of clsBeEstructura_tramo)
        Try

            Dim vSQL As String = "SELECT * FROM Estructura_tramo WHERE  (IdBodega = @IdBodega AND IdTramo = @IdTramo) ORDER BY Orientacion, Indice_x "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Dim vBeEstructura_tramo As New clsBeEstructura_tramo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeEstructura_tramo = New clsBeEstructura_tramo
                            Cargar(vBeEstructura_tramo, dr)
                        Next

                        Get_Single_By_IdBodega_And_IdTramo = vBeEstructura_tramo

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdBodega_And_IdTramo(ByVal pIdBodega As Integer,
                                                              ByVal pIdTramo As Integer,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As clsBeEstructura_tramo

        Dim lReturnList As New List(Of clsBeEstructura_tramo)
        Try

            Dim vSQL As String = "SELECT * FROM Estructura_tramo WHERE  (IdBodega = @IdBodega AND IdTramo = @IdTramo) ORDER BY Orientacion, Indice_x "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Dim vBeEstructura_tramo As New clsBeEstructura_tramo

                For Each dr As DataRow In lDataTable.Rows
                    vBeEstructura_tramo = New clsBeEstructura_tramo
                    Cargar(vBeEstructura_tramo, dr)
                Next

                Get_Single_By_IdBodega_And_IdTramo = vBeEstructura_tramo

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
