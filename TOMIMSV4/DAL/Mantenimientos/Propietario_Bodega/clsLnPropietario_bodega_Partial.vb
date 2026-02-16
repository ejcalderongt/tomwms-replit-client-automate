Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnPropietario_bodega
    Implements IDisposable

    Public Shared lPropietariosBodegaInMemory As New List(Of clsBePropietario_bodega)

    Public Shared Function Get_All_By_IdPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_bodega)

        Dim lReturnList As New List(Of clsBePropietario_bodega)

        Try


            Dim vSQL As String = "SELECT * FROM propietario_bodega WHERE IdPropietario=@IdPropietario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBePropietario_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBePropietario_bodega
                                Cargar(Obj, lRow)
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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As List(Of clsBePropietario_bodega)

        Dim lReturnList As New List(Of clsBePropietario_bodega)

        Try


            Dim vSQL As String = "SELECT * FROM propietario_bodega WHERE IdPropietarioBodega=@IdPropietarioBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBePropietario_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBePropietario_bodega
                                Cargar(Obj, lRow)
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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBePropietario_bodega)

        Dim lReturnList As New List(Of clsBePropietario_bodega)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM propietario_bodega WHERE IdBodega=@IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BePropietario As clsBePropietario_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BePropietario = New clsBePropietario_bodega
                                Cargar(BePropietario, lRow)

                                BePropietario.Propietario.IdPropietario = BePropietario.IdPropietario
                                clsLnPropietarios.Obtener(BePropietario.Propietario, lConnection, ltransaction)

                                BePropietario.ReglasEnc = clsLnPropietario_reglas_enc.GetAllHH(BePropietario.IdPropietario, lConnection, ltransaction)

                                lReturnList.Add(BePropietario)

                            Next

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_For_Combo(ByVal IdBodega As Integer) As DataTable

        Get_All_By_IdBodega_For_Combo = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " SELECT propietario_bodega.IdPropietarioBodega AS IdPropietarioBodega,
                                           propietarios.IdPropietario, 
                                           propietarios.codigo AS Codigo, propietarios.nombre_comercial AS Nombre
                                           FROM propietarios INNER JOIN
                                           propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario
                                           WHERE IdBodega=@IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdBodega_For_Combo = lDataTable

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

    Public Shared Function Get_All_By_IdBodega_For_Combo(ByVal IdBodega As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_IdBodega_For_Combo = Nothing

        Try

            Dim vSQL As String = " SELECT propietario_bodega.IdPropietarioBodega AS IdPropietarioBodega,
                                           propietarios.IdPropietario, 
                                           propietarios.codigo AS Codigo, propietarios.nombre_comercial AS Nombre
                                           FROM propietarios INNER JOIN
                                           propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario
                                           WHERE IdBodega=@IdBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_All_By_IdBodega_For_Combo = lDataTable

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_IdBodega_And_IdDocumentoIngreso_For_Combo(ByVal IdBodega As Integer,
                                                                                ByVal IdOrdenCompraEnc As Integer) As DataTable

        Get_All_By_IdBodega_And_IdDocumentoIngreso_For_Combo = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " SELECT propietario_bodega.IdPropietarioBodega AS IdPropietarioBodega,
                                           propietarios.IdPropietario, 
                                           propietarios.codigo AS Codigo, propietarios.nombre_comercial AS Nombre
                                           FROM propietarios INNER JOIN
                                           propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario
                                           WHERE IdBodega=@IdBodega 
                                           AND propietario_bodega.IdPropietarioBodega IN (
                                           SELECT idpropietariobodega FROM trans_oc_det WHERE idordencompraenc = @IdOrdenCompraEnc )"

                    vSQL += " UNION SELECT propietario_bodega.IdPropietarioBodega AS IdPropietarioBodega,
                                           propietarios.IdPropietario, 
                                           propietarios.codigo AS Codigo, propietarios.nombre_comercial AS Nombre
                                           FROM propietarios INNER JOIN
                                           propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario
                                           WHERE IdBodega=@IdBodega
                                           AND propietario_bodega.IdPropietarioBodega IN (
                                           SELECT idpropietariobodega FROM trans_oc_enc WHERE idordencompraenc = @IdOrdenCompraEnc )"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdBodega_And_IdDocumentoIngreso_For_Combo = lDataTable

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

    Public Shared Function Get_All_By_IdPropietarioBodega_For_Combo(ByVal IdPropietarioBodega As Integer) As DataTable

        Get_All_By_IdPropietarioBodega_For_Combo = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " SELECT propietario_bodega.IdPropietarioBodega AS IdPropietarioBodega,
                                           propietarios.IdPropietario, 
                                           propietarios.codigo AS Codigo, propietarios.nombre_comercial AS Nombre
                                           FROM propietarios INNER JOIN
                                           propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario
                                           WHERE propietario_bodega.IdPropietarioBodega=@IdPropietarioBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_All_By_IdPropietarioBodega_For_Combo = lDataTable
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

    ''' <summary>
    ''' For TMS By Erik C On 20210124
    ''' </summary>
    ''' <param name="IdEmpresa"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_Empresa_For_Combo(ByVal IdEmpresa As Integer) As DataTable

        Get_All_By_Empresa_For_Combo = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " SELECT propietarios.IdPropietario, 
                                           propietarios.codigo AS Código, propietarios.nombre_comercial AS Nombre
                                           FROM propietarios 
                                           WHERE propietarios.IdEmpresa=@Idempresa"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Idempresa", IdEmpresa)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Return lDataTable

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

    Public Shared Function Get_All_By_IdBodega_HH(ByVal IdBodega As Integer) As List(Of clsBePropietario_bodega)

        Dim lReturnList As New List(Of clsBePropietario_bodega)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM propietario_bodega WHERE IdBodega=@IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BePropietario As clsBePropietario_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BePropietario = New clsBePropietario_bodega
                                Cargar(BePropietario, lRow)

                                BePropietario.Propietario.IdPropietario = BePropietario.IdPropietario
                                clsLnPropietarios.Obtener(BePropietario.Propietario, True, lConnection, lTransaction)

                                BePropietario.ReglasEnc = clsLnPropietario_reglas_enc.GetAllHH(BePropietario.IdPropietario, lConnection, lTransaction)

                                lReturnList.Add(BePropietario)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_For_Seleccion(ByVal IdBodega As Integer) As List(Of clsBePropietarioBodegaSeleccion)

        Dim lReturnList As New List(Of clsBePropietarioBodegaSeleccion)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    '#HS20171023_1630pm: Quité String.Format.
                    Dim vSQL As String = "SELECT * FROM propietario_bodega WHERE IdBodega=@IdBodega"


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BePropietario As clsBePropietario_bodega
                        Dim BeProBodSel As New clsBePropietarioBodegaSeleccion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Debug.WriteLine("registros", lDataTable.Rows.Count)

                            For Each lRow As DataRow In lDataTable.Rows

                                BePropietario = New clsBePropietario_bodega
                                Cargar(BePropietario, lRow)

                                BePropietario.Propietario.IdPropietario = BePropietario.IdPropietario
                                clsLnPropietarios.Obtener(BePropietario.Propietario,
                                                                        lConnection,
                                                                        lTransaction)

                                BeProBodSel = New clsBePropietarioBodegaSeleccion
                                BeProBodSel.IdPropietarioBodega = BePropietario.IdPropietarioBodega
                                BeProBodSel.NombreComercial = BePropietario.Propietario.Nombre_comercial
                                BeProBodSel.Seleccion = False

                                lReturnList.Add(BeProBodSel)

                            Next

                        End If

                        lDataTable.Dispose()
                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()
                lConnection.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetBodegaByIdPropietario(ByVal pIdPropietarioBodega As Integer) As clsBeBodega

        GetBodegaByIdPropietario = Nothing

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT pb.IdBodega " &
                                     " FROM propietario_bodega AS pb  " &
                                     " INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario " &
                                     " WHERE p.activo=1  AND pb.IdPropietario =@IdPropietario"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietarioBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Dim BeBodega As New clsBeBodega
                        BeBodega.IdBodega = lDataTable.Rows(0).Item("IdBodega")
                        clsLnBodega.Obtener(BeBodega)

                        Return BeBodega

                    End If

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_IdPropietarioBodega(ByVal IdPropietarioBodega As Integer,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Boolean

        Existe_IdPropietarioBodega = False

        Try


            Dim vSQL As String = "SELECT pb.IdPropietarioBodega 
                        FROM propietario_bodega AS pb  
                        INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                        WHERE p.activo=1  AND pb.IdPropietarioBodega = @IdPropietarioBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_IdPropietarioBodega = True
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                                    ByVal pIdBodega As Integer,
                                                                                    ByVal lConnection As SqlConnection,
                                                                                    ByVal lTransaction As SqlTransaction) As Boolean

        Existe_IdPropietario_And_IdBodega = False

        Try


            Dim vSQL As String = "SELECT pb.IdPropietarioBodega 
                        FROM propietario_bodega AS pb  
                        INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                        WHERE p.activo=1  AND pb.IdPropietario = @IdPropietario AND pb.IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_IdPropietario_And_IdBodega = True
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                                 ByVal pIdBodega As Integer) As Integer

        Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega = 0

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT pb.IdPropietarioBodega 
                        FROM propietario_bodega AS pb  
                        INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                        WHERE p.activo=1  AND pb.IdPropietario = @IdPropietario AND pb.IdBodega = @IdBodega "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                        Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega = lDataTable.Rows(0).Item("IdPropietarioBodega")
                    End If

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                                 ByVal pIdBodega As Integer,
                                                                                 ByRef lConnection As SqlConnection,
                                                                                 ByRef lTransaction As SqlTransaction) As Integer

        Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT pb.IdPropietarioBodega 
                                  FROM propietario_bodega AS pb  
                                  INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                                  WHERE p.activo=1 AND pb.IdPropietario = @IdPropietario 
                                  AND pb.IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega = lDataTable.Rows(0).Item("IdPropietarioBodega")
                End If

            End Using


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdPropietarioBodega_By_IdPropietario_And_IdArea(ByVal pIdPropietario As Integer,
                                                                               ByVal pIdArea As Integer,
                                                                               ByRef lConnection As SqlConnection,
                                                                               ByRef lTransaction As SqlTransaction) As Integer

        Get_IdPropietarioBodega_By_IdPropietario_And_IdArea = 0

        Try

            Dim vSQL As String = "SELECT pb.IdPropietarioBodega 
                                  FROM propietario_bodega AS pb INNER JOIN
                                       propietarios AS p ON pb.IdPropietario = p.IdPropietario INNER JOIN 
                                       bodega_area a ON a.IdBodega = pb.IdBodega 
                                  WHERE p.activo=1 AND pb.IdPropietario = @IdPropietario 
                                  AND a.IdArea = @IdArea "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_IdPropietarioBodega_By_IdPropietario_And_IdArea = lDataTable.Rows(0).Item("IdPropietarioBodega")
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetBodegaByIdPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As clsBeBodega

        GetBodegaByIdPropietarioBodega = Nothing

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1630pm: Quité String.Format.
                Dim vSQL As String = "SELECT pb.IdBodega " &
                                     " FROM propietario_bodega AS pb  " &
                                     " INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario " &
                                     " WHERE p.activo=1  AND pb.IdPropietarioBodega =@IdPropietarioBodega"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Dim BeBodega As New clsBeBodega
                        BeBodega.IdBodega = lDataTable.Rows(0).Item("IdBodega")

                        Return BeBodega

                    End If

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_With_Propietario_By_IdPropietarioBodega(ByVal IdPropietarioBodega As Integer) As clsBePropietario_bodega

        Get_Single_With_Propietario_By_IdPropietarioBodega = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM propietario_bodega WHERE IdPropietarioBodega=@IdPropietarioBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BePropietario As clsBePropietario_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BePropietario = New clsBePropietario_bodega
                                Cargar(BePropietario, lRow)

                                BePropietario.Propietario.IdPropietario = BePropietario.IdPropietario
                                clsLnPropietarios.Obtener(BePropietario.Propietario, True, lConnection, lTransaction)

                                Return BePropietario

                            Next

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

    Public Shared Function Get_Single_With_Propietario(ByVal IdPropietarioBodega As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As clsBePropietario_bodega

        Get_Single_With_Propietario = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM propietario_bodega WHERE IdPropietarioBodega=@IdPropietarioBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BePropietario As clsBePropietario_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BePropietario = New clsBePropietario_bodega
                        Cargar(BePropietario, lRow)

                        BePropietario.Propietario.IdPropietario = BePropietario.IdPropietario
                        clsLnPropietarios.Obtener(BePropietario.Propietario, True, lConnection, lTransaction)

                        Return BePropietario

                    Next

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Obtener(ByRef oBePropietario_bodega As clsBePropietario_bodega,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * FROM Propietario_bodega" &
            " Where(IdPropietarioBodega = @IdPropietarioBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBePropietario_bodega.IdPropietarioBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBePropietario_bodega, dt.Rows(0))
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

    Public Shared Function Get_IdPropietario_By_IdBodega_IdPropietarioBodega(ByVal pIdBodega As Integer,
                                                                             ByVal pIdPropietarioBodega As Integer) As Integer

        Get_IdPropietario_By_IdBodega_IdPropietarioBodega = 0

        Try

            Dim vSQL As String = "SELECT pb.IdPropietario
                                  FROM propietario_bodega AS pb 
                                     INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                                  WHERE p.activo=1 AND pb.IdBodega=@IdBodega AND pb.IdPropietarioBodega=@IdPropietarioBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_IdPropietario_By_IdBodega_IdPropietarioBodega = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdPropietarioBodega_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdPropietarioBodega_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT top(1) pb.IdPropietarioBodega
                                  FROM propietario_bodega AS pb 
                                  WHERE pb.activo=1 AND pb.IdBodega=@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lReturnValue As Object = cmd.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        Get_IdPropietarioBodega_By_IdBodega = CInt(lReturnValue)
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try


    End Function

    '#CKFK20220131 Agregué esta función para obtener la Empresa del propietario
    Public Shared Function GetIdEmpresa_By_IdPropietarioBodega(ByVal pIdPropietarioBodega As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As Integer

        GetIdEmpresa_By_IdPropietarioBodega = 0

        Try

            Dim vSQL As String = "SELECT p.IdEmpresa 
                                  FROM propietario_bodega AS pb INNER JOIN 
                                       propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                                  WHERE pb.IdPropietarioBodega = @IdPropietarioBodega "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    GetIdEmpresa_By_IdPropietarioBodega = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Prefactura_By_IdBodega_For_Combo(ByVal IdBodega As Integer) As DataTable

        Get_All_Prefactura_By_IdBodega_For_Combo = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " SELECT propietario_bodega.IdPropietarioBodega AS IdPropietarioBodega,
                                           propietarios.IdPropietario, propietarios.NIT, 
                                           propietarios.codigo AS Codigo, propietarios.nombre_comercial AS Nombre
                                           FROM propietarios INNER JOIN
                                           propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario
                                           WHERE IdBodega=@IdBodega"

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
    Public Shared Function Get_IdPropietario_By_IdBodega_IdPropietarioBodega(ByVal pIdBodega As Integer,
                                                                            ByVal pIdPropietarioBodega As Integer,
                                                                            ByVal lConnection As SqlConnection,
                                                                            ByVal lTransaction As SqlTransaction) As Integer

        Try
            Dim vSQL As String = "SELECT pb.IdPropietario
                              FROM propietario_bodega AS pb 
                              INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario 
                              WHERE p.activo=1 AND pb.IdBodega=@IdBodega AND pb.IdPropietarioBodega=@IdPropietarioBodega"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Return CInt(lReturnValue)
                End If
            End Using

            Return 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(vMsgError)
        End Try
    End Function

    '#GT19062025: metodo para obtener propietarios bodega con tran para importar a la nube, por el idpropietario.
    Public Shared Function Get_All_By_IdPropietario(ByVal IdPropietario As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As List(Of clsBePropietario_bodega)

        Dim listPropietariosBodega As New List(Of clsBePropietario_bodega)()

        Try

            Dim vSQL As String = "SELECT * FROM propietario_bodega WHERE IdPropietario=@IdPropietario"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BePropietario As clsBePropietario_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    listPropietariosBodega = New List(Of clsBePropietario_bodega)()

                    For Each lRow As DataRow In lDataTable.Rows

                        BePropietario = New clsBePropietario_bodega
                        Cargar(BePropietario, lRow)
                        listPropietariosBodega.Add(BePropietario)
                    Next

                Else
                    listPropietariosBodega = Nothing
                End If

            End Using

            Return listPropietariosBodega

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
