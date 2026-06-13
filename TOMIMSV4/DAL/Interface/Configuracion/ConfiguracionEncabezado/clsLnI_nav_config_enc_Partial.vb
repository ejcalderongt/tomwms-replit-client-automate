Imports System.Data.SqlClient
Partial Public Class clsLnI_nav_config_enc

    Public Shared Function ListarFiltrados(ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT * FROM VW_Configuracioninv  where idEmpresa = 1 AND  idBodega=@idBodega"

            'Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@idBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ListarFiltrados_By_Correlativo(ByVal pCorrelativo As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT * FROM VW_Configuracioninv  where correlativo=@pCorrelativo"

            'Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@pCorrelativo", pCorrelativo)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ListarFiltrados() As DataTable

        Try

            Dim vSQL As String = "SELECT * FROM VW_Configuracioninv"

            'Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdConfiguracion(ByVal pIdBodega As Integer,
                                               ByVal pIdEmpresa As Integer) As Integer

        Dim IdProductoEstado = 0

        Try

            Dim vSQL As String = "SELECT idnavconfigenc FROM i_nav_config_enc 
                                  WHERE IdBodega=@IdBodega 
                                  AND IdEmpresa=@IdEmpresa "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)
                    dad.SelectCommand.Parameters.AddWithValue("IdEmpresa", pIdEmpresa)

                    Dim lDT As New DataTable
                    dad.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        IdProductoEstado = CType(lRow("idnavconfigenc"), Integer)

                    End If

                End Using

            End Using

            Return IdProductoEstado

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdConfiguracionEncabezado As Integer,
                                     ByRef pConnection As SqlConnection,
                                     ByRef pTransaction As SqlTransaction) As clsBeI_nav_config_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_config_enc WHERE idnavconfigenc=@pIdConfiguracionEncabezado"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("pIdConfiguracionEncabezado", pIdConfiguracionEncabezado)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeI_nav_config_enc()

                    Cargar(ObjUM, lRow)

                    Return ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdBodega_And_IdEmpresa(ByVal pIdBodega As Integer,
                                                                ByVal pIdEmpresa As Integer) As clsBeI_nav_config_enc

        Get_Single_By_IdBodega_And_IdEmpresa = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_config_enc 
                                  WHERE IdBodega=@IdBodega 
                                  AND IdEmpresa=@IdEmpresa "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("IdEmpresa", pIdEmpresa)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjUM As New clsBeI_nav_config_enc()
                            Cargar(ObjUM, lRow)
                            Get_Single_By_IdBodega_And_IdEmpresa = ObjUM
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

    Public Shared Function Get_Single_By_IdBodega_And_IdEmpresa(ByVal pIdBodega As Integer,
                                                                ByVal pIdEmpresa As Integer,
                                                                ByRef pConnection As SqlConnection,
                                                                ByRef pTransaction As SqlTransaction) As clsBeI_nav_config_enc

        Get_Single_By_IdBodega_And_IdEmpresa = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_config_enc 
                                  WHERE IdBodega=@IdBodega 
                                  AND IdEmpresa=@IdEmpresa "

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("IdEmpresa", pIdEmpresa)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeI_nav_config_enc()

                    Cargar(ObjUM, lRow)

                    Get_Single_By_IdBodega_And_IdEmpresa = ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdBodega_And_IdPropietario(ByVal pIdBodega As Integer,
                                                                   ByVal pIdPropietario As Integer,
                                                                   ByRef pConnection As SqlConnection,
                                                                   ByRef pTransaction As SqlTransaction) As clsBeI_nav_config_enc

        GetSingle_By_IdBodega_And_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_config_enc 
                                  WHERE IdBodega=@IdBodega 
                                  AND IdPropietario=@IdPropietario "

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("IdPropietario", pIdPropietario)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeI_nav_config_enc()
                    Cargar(ObjUM, lRow)
                    GetSingle_By_IdBodega_And_IdPropietario = ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT12012023_2000: Agregue el guardar, porque no existia y se hacia por el Actualizar.
    Public Shared Function Guardar(ByRef oBeI_nav_config_enc As clsBeI_nav_config_enc,
                                   ByRef lConfiDet As List(Of clsBeI_nav_config_det),
                                   ByRef pBeINavConfigEnt As clsBeI_nav_config_ent) As Boolean


        Guardar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Insertar(oBeI_nav_config_enc, lConnection, lTransaction)

            '#EJC20171108_REF01_0846AM: Actualización de EndPoint interface
            'clsLnI_nav_config_ent.Actualizar(pBeINavConfigEnt, lConnection, lTransaction)
            clsLnI_nav_config_ent.Insertar(pBeINavConfigEnt, lConnection, lTransaction)

            For Each Det As clsBeI_nav_config_det In lConfiDet

                If Not Det.Activo Then
                    clsLnI_nav_config_det.Eliminar(Det, lConnection, lTransaction)
                Else

                    If clsLnI_nav_config_det.Exists(Det.IdNavEnt, Det.Dia, lConnection, lTransaction) Then
                        If (Det.Activo) Then
                            clsLnI_nav_config_det.Actualizar(Det, lConnection, lTransaction)
                        Else
                            clsLnI_nav_config_det.Eliminar(Det, lConnection, lTransaction)
                        End If
                    Else
                        Det.Idnavconfigdet = clsLnI_nav_config_det.MaxID(lConnection, lTransaction) + 1
                        clsLnI_nav_config_det.Insertar(Det, lConnection, lTransaction)
                    End If
                End If

            Next

            lTransaction.Commit()

            Guardar = True

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeI_nav_config_enc As clsBeI_nav_config_enc,
                                      ByRef lConfiDet As List(Of clsBeI_nav_config_det),
                                      ByRef pBeINavConfigEnt As clsBeI_nav_config_ent) As Boolean


        Actualizar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Actualizar(oBeI_nav_config_enc, lConnection, lTransaction)

            '#EJC20171108_REF01_0846AM: Actualización de EndPoint interface
            clsLnI_nav_config_ent.Actualizar(pBeINavConfigEnt, lConnection, lTransaction)

            For Each Det As clsBeI_nav_config_det In lConfiDet

                If Not Det.Activo Then
                    clsLnI_nav_config_det.Eliminar(Det, lConnection, lTransaction)
                Else

                    If clsLnI_nav_config_det.Exists(Det.IdNavEnt, Det.Dia, lConnection, lTransaction) Then
                        If (Det.Activo) Then
                            clsLnI_nav_config_det.Actualizar(Det, lConnection, lTransaction)
                        Else
                            clsLnI_nav_config_det.Eliminar(Det, lConnection, lTransaction)
                        End If
                    Else
                        Det.Idnavconfigdet = clsLnI_nav_config_det.MaxID(lConnection, lTransaction) + 1
                        clsLnI_nav_config_det.Insertar(Det, lConnection, lTransaction)
                    End If
                End If

            Next

            lTransaction.Commit()

            Actualizar = True

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_IdProductoEstado_By_IdBodega_And_IdEmpresa(ByVal pIdBodega As Integer,
                                                                          ByVal pIdEmpresa As Integer,
                                     ByRef pConnection As SqlConnection,
                                     ByRef pTransaction As SqlTransaction) As Integer

        Dim IdProductoEstado = 0

        Try

            Dim vSQL As String = "SELECT IdProductoEstado FROM i_nav_config_enc 
                                  WHERE IdBodega=@IdBodega 
                                  AND IdEmpresa=@IdEmpresa "

            Dim cmd As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("IdEmpresa", pIdEmpresa)

            Dim lDT As New DataTable
            dad.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                Dim lRow As DataRow = lDT.Rows(0)
                IdProductoEstado = CType(lRow("IdProductoEstado"), Integer)

            End If

            Return IdProductoEstado

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_Ejecutable(ByVal pidNavConfigEnc As Integer) As String

        Dim vNombre_Ejecutable = ""

        Try

            Dim vSQL As String = "SELECT nombre_ejecutable FROM i_nav_config_enc 
                                  WHERE idnavconfigenc=@pidNavConfigEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.AddWithValue("pidNavConfigEnc", pidNavConfigEnc)

                    Dim lDT As New DataTable
                    dad.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        vNombre_Ejecutable = IIf(IsDBNull(lRow("nombre_ejecutable")), "", lRow("nombre_ejecutable"))

                    End If

                End Using

            End Using

            Return vNombre_Ejecutable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdBodega_And_IdPropietario(ByVal pIdBodega As Integer,
                                                                   ByVal pIdPropietario As Integer) As clsBeI_nav_config_enc

        GetSingle_By_IdBodega_And_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_config_enc 
                                  WHERE IdBodega=@IdBodega 
                                  AND IdPropietario=@IdPropietario "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("IdPropietario", pIdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjUM As New clsBeI_nav_config_enc()
                            Cargar(ObjUM, lRow)
                            Return ObjUM

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

    Public Shared Function Get_Interface_SAP(ByVal pidNavConfigEnc As Integer) As Boolean

        Dim vInterface_SAP As Boolean = False

        Try

            Dim vSQL As String = "SELECT Interface_SAP FROM i_nav_config_enc 
                                  WHERE idnavconfigenc=@pidNavConfigEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    cmd.Parameters.AddWithValue("@pidNavConfigEnc", pidNavConfigEnc)
                    Dim lReturnValue As Object = cmd.ExecuteScalar()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        vInterface_SAP = CInt(lReturnValue)
                    End If

                    lTransaction.Commit()

                End Using

            End Using

            Return vInterface_SAP

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdBodega(ByVal pIdBodega As Integer) As clsBeI_nav_config_enc

        Get_Single_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_config_enc 
                                  WHERE IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjUM As New clsBeI_nav_config_enc()
                            Cargar(ObjUM, lRow)
                            Get_Single_By_IdBodega = ObjUM
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

    Public Shared Function Get_Single_By_IdBodega(ByVal pIdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeI_nav_config_enc

        Get_Single_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_config_enc 
                                  WHERE IdBodega=@IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeI_nav_config_enc()
                    Cargar(ObjUM, lRow)
                    Get_Single_By_IdBodega = ObjUM
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Existe_by_Ejecutable(ByVal pEjecutable As String) As Boolean

        Get_Existe_by_Ejecutable = False

        Try

            Dim vSQL As String = "SELECT top 1 * FROM i_nav_config_enc 
                                  WHERE nombre_ejecutable=@pEjecutable "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.AddWithValue("pEjecutable", pEjecutable)

                    Dim lDT As New DataTable
                    dad.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        'Dim lRow As DataRow = lDT.Rows(0)
                        'vNombre_Ejecutable = IIf(IsDBNull(lRow("nombre_ejecutable")), "", lRow("nombre_ejecutable"))
                        Get_Existe_by_Ejecutable = True

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT12062026: obtener intervalo autoimpresion rfid sin tocar el metodo de carga.
    Public Shared Function Get_Intervalo_AutoImpresionRFID(ByVal pIdBodega As Integer, pIdEmpresa As Integer) As Integer

        Dim vIntervalo_autoimpresion_rfid As Integer = 0

        Try

            Dim vSQL As String = "SELECT intervalo_autoimpresion_rfid FROM i_nav_config_enc 
                                  WHERE (IdBodega=@IdBodega and IdEmpresa=@IdEmpresa) "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    cmd.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                    Dim lReturnValue As Object = cmd.ExecuteScalar()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        vIntervalo_autoimpresion_rfid = CInt(lReturnValue)
                    End If

                    lTransaction.Commit()

                End Using

            End Using

            Return vIntervalo_autoimpresion_rfid

        Catch ex As Exception
            '#GT12062026: excepción silenciosa para no arruinar el menu principal/UI
            clsLnLog_error_wms.Agregar_Error("AUTO_RFID: Get_Intervalo_AutoImpresionRFID " & ex.Message)
            Return 0
        End Try

    End Function

End Class
