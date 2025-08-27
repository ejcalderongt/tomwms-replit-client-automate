Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnBodega_area
    Implements IDisposable

    ''' <summary>
    ''' Funcion para obtener una lista de Areas filtradas por un ID BODEGA
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul</remarks>
    Public Shared Function GetAllByAreaBodega(ByVal pActivo As Boolean, ByVal pIdBodega As Integer) As List(Of clsBeBodega_area)

        Try

            Dim lReturnList As New List(Of clsBeBodega_area)

            Dim vSQL As String = "SELECT * FROM bodega_area WHERE IdBodega=@IdBodega"

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            vSQL += " Order by Codigo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_area

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_area()
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
            Throw New Exception("BodegaArea_GetAllByAreaBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pActivo As Boolean,
                                               ByVal pIdBodega As Integer,
                                               ByRef lTransaction As SqlTransaction,
                                               ByRef lConnection As SqlConnection) As List(Of clsBeBodega_area)

        Try

            Dim lReturnList As New List(Of clsBeBodega_area)

            Dim vSQL As String = "SELECT * FROM bodega_area WHERE IdBodega=@IdBodega "

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            vSQL += " Order by Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_area

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_area()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaArea_GetAllByAreaBodega: " & ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Funcion que devuelve Datatable las areas filtradas por un IDBODEGA
    ''' Recibe filtro por area desde algunos metodos, por eso es Optional
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul</remarks>
    Public Shared Function Get_All_Areas_By_IdBodega(ByVal pIdBodega As Integer, Optional ByVal pIdAreaFiltro As Integer = 0) As DataTable

        Get_All_Areas_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM bodega_area  WHERE IdBodega=@IdBodega "

            If pIdAreaFiltro > 0 Then
                vSQL += " and IdArea=@IdArea "
            End If

            vSQL += " ORDER BY Descripcion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        If pIdAreaFiltro > 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdAreaFiltro)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Get_All_Areas_By_IdBodega = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Areas_By_IdBodega(ByVal pIdBodega As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_Areas_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM bodega_area  WHERE IdBodega=@IdBodega ORDER BY Descripcion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)
                Get_All_Areas_By_IdBodega = lDataTable

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Area_By_IdBodega_And_IdIventario(ByVal pIdBodega As Integer,
                                                                    ByVal IdInventario As Integer,
                                                                    ByVal lConnection As SqlConnection,
                                                                    ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim vSQL As String = "SELECT bodega_area.IdArea, 
                         bodega_area.IdBodega, 
                         bodega_area.Descripcion, 
                         bodega_area.sistema
                    FROM bodega_ubicacion INNER JOIN
                         trans_inv_stock ON bodega_ubicacion.IdUbicacion = trans_inv_stock.IdUbicacion INNER JOIN
                         bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo INNER JOIN
                         bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector INNER JOIN
                         bodega_area ON bodega_sector.IdArea = bodega_area.IdArea 
                         AND bodega_sector.IdBodega = bodega_area.IdBodega "

            vSQL += "WHERE bodega_area.IdBodega=@IdBodega and trans_inv_stock.idinventario = @IdInventario "

            vSQL += "GROUP BY bodega_area.IdArea, bodega_area.IdBodega, bodega_area.Descripcion, bodega_area.user_agr, bodega_area.fec_agr, 
                         bodega_area.user_mod, bodega_area.fec_mod, bodega_area.Codigo, bodega_area.alto, bodega_area.largo, bodega_area.ancho, 
                         bodega_area.margen_izquierdo, bodega_area.margen_derecho, bodega_area.margen_superior, bodega_area.margen_inferior, 
                         bodega_area.sistema, bodega_area.activo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)
                Return lDataTable

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodegaExist: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Areas_By_IdBodega_For_Combo(ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT IdArea,Descripcion as Nombre FROM bodega_area WHERE IdBodega=@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Areas_By_IdBodega_For_Combo(ByVal pActivo As Boolean,
                                                               ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT IdArea,Codigo + ' - ' + Descripcion as Nombre FROM bodega_area WHERE IdBodega=@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Existe_Codigo_By_IdBodega(ByVal pCodigoArea As String, ByVal IdBodega As Integer) As Boolean

        Existe_Codigo_By_IdBodega = False

        Try

            Const sp As String = " SELECT * FROM Bodega_area 
                                   WHERE(Codigo = @Codigo AND IdBodega=@IdBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", pCodigoArea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Existe_Codigo_By_IdBodega = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Codigo_By_IdBodega(ByVal pCodigoArea As String,
                                                     ByVal IdBodega As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Codigo_By_IdBodega = False

        Try

            Const sp As String = "SELECT * FROM Bodega_area " &
                     "WHERE ((CAST(IdArea AS VARCHAR) = @Codigo OR Codigo = @Codigo) AND IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", pCodigoArea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Existe_Codigo_By_IdBodega = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdArea_and_IdBodega(ByVal pCodigoArea As String,
                                                            ByVal IdBodega As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As clsBeBodega_area

        GetSingle_By_IdArea_and_IdBodega = Nothing

        Try

            Const sp As String = "SELECT * FROM Bodega_area " &
            " Where (IdArea = @Codigo or Codigo = @Codigo) AND IdBodega=@IdBodega"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", pCodigoArea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega_area
                Cargar(pBeBodega, dt.Rows(0))
                GetSingle_By_IdArea_and_IdBodega = pBeBodega
            Else
                GetSingle_By_IdArea_and_IdBodega = Nothing
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdArea_By_IdTramo(ByVal pIdTramo As Integer) As Integer

        Get_IdArea_By_IdTramo = 0

        Try

            Const sp As String = "SELECT TOP(1) bodega_area.IdArea                                 
                                 FROM bodega_area INNER JOIN
                                 bodega_sector ON bodega_area.IdArea = bodega_sector.IdArea INNER JOIN
                                 bodega_tramo ON bodega_sector.IdSector = bodega_tramo.IdSector 
                                 Where(bodega_tramo.IdTramo = @IdTramo)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", pIdTramo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_IdArea_By_IdTramo = dt.Rows(0).Item("IdArea")
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdArea_By_IdTramo_And_IdBodega(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer) As Integer

        Get_IdArea_By_IdTramo_And_IdBodega = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT TOP(1) bodega_area.IdArea                                 
                                 FROM bodega_area INNER JOIN
                                      bodega_sector ON  bodega_area.IdArea = bodega_sector.IdArea and bodega_area.IdBodega = bodega_sector.IdBodega
                                                   INNER JOIN  
                                      bodega_tramo ON bodega_sector.IdSector = bodega_tramo.IdSector  and bodega_sector.IdBodega = bodega_tramo.IdBodega
                                 Where(bodega_tramo.IdTramo = @IdTramo AND bodega_tramo.IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", pIdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_IdArea_By_IdTramo_And_IdBodega = dt.Rows(0).Item("IdArea")
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Limpiar_Todo(ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from bodega_area where IdBodega = @IdBodega"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

                Dim rowsAffected As Integer = lCommand.ExecuteNonQuery()
                lCommand.Dispose()

                Return rowsAffected
            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Areas_For_Combo_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_All_Areas_For_Combo_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT IdArea, codigo + ' - ' + descripcion AS Nombre FROM bodega_area 
                              WHERE IdBodega=@IdBodega ORDER BY Descripcion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Get_All_Areas_For_Combo_By_IdBodega = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_IdArea_By_Codigo_Area(ByVal pCodigo As String,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim rIdArea As Integer = 0

            Const sp As String = "SELECT IdArea
                                  FROM bodega_area
                                  WHERE codigo = @codigo "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@codigo", pCodigo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    rIdArea = CInt(lReturnValue)
                End If

            End Using

            Return rIdArea

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_For_SAP(ByVal pIdBodega As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega_area)

        Try

            Dim lReturnList As New List(Of clsBeBodega_area)

            Dim vSQL As String = "SELECT * FROM bodega_area WHERE IdBodega=@IdBodega AND Activo=1 AND Sistema =0 Order by Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_area

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_area()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaArea_GetAllByAreaBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_IdUbicacion_Recepcion_By_Codigo_Area(ByVal pCodigo As String,
                                                                    ByRef lConnection As SqlConnection,
                                                                    ByRef lTransaction As SqlTransaction) As Integer

        Get_IdUbicacion_Recepcion_By_Codigo_Area = 0

        Try

            Dim IdUbicacionRecepcionByArea As Integer = 0

            Const sp As String = "SELECT a.IdUbicacion from bodega_ubicacion a 
                                  INNER JOIN bodega_area b
                                  on a.IdBodega = b.IdBodega
                                  AND a.IdArea = b.IdArea
                                  WHERE b.Codigo = @codigo  AND a.sistema = 1
                                  and a.ubicacion_recepcion = 1 "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@codigo", pCodigo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    IdUbicacionRecepcionByArea = CInt(lReturnValue)
                End If

            End Using

            Get_IdUbicacion_Recepcion_By_Codigo_Area = IdUbicacionRecepcionByArea

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicacion_Recepcion_By_IdArea(ByVal pIdArea As Integer,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As Integer

        Get_IdUbicacion_Recepcion_By_IdArea = 0

        Try

            Dim IdUbicacionRecepcionByArea As Integer = 0

            Const sp As String = "SELECT a.IdUbicacion from bodega_ubicacion a 
                                  INNER JOIN bodega_area b
                                  on a.IdBodega = b.IdBodega
                                  AND a.IdArea = b.IdArea
                                  WHERE b.IdArea = @IdArea  AND a.sistema = 1
                                  and a.ubicacion_recepcion = 1 "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdArea", pIdArea)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    IdUbicacionRecepcionByArea = CInt(lReturnValue)
                End If

            End Using

            Get_IdUbicacion_Recepcion_By_IdArea = IdUbicacionRecepcionByArea

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicacion_Confirmacion_By_IdArea(ByVal pIdArea As Integer,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As Integer

        Get_IdUbicacion_Confirmacion_By_IdArea = 0

        Try

            Dim IdUbicacionRecepcionByArea As Integer = 0

            Const sp As String = "SELECT a.IdUbicacion from bodega_ubicacion a 
                                  INNER JOIN bodega_area b
                                  on a.IdBodega = b.IdBodega
                                  AND a.IdArea = b.IdArea
                                  WHERE b.IdArea = @IdArea  AND a.sistema = 0
                                  and a.ubicacion_recepcion = 1 "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdArea", pIdArea)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    IdUbicacionRecepcionByArea = CInt(lReturnValue)
                End If

            End Using

            Get_IdUbicacion_Confirmacion_By_IdArea = IdUbicacionRecepcionByArea

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT04072024: mostrar lista de bodegas asociadas a un picking de una prefactura
    Public Shared Function GetSingle_By_IdArea_and_IdBodega(ByVal IdArea As Integer,
                                                            ByVal IdBodega As Integer) As clsBeBodega_area


        GetSingle_By_IdArea_and_IdBodega = Nothing

        Try

            Const sp As String = "SELECT * FROM Bodega_area Where (IdArea = @IdArea AND IdBodega=@IdBodega) "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", IdArea)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable.Rows.Count = 1 Then

                            GetSingle_By_IdArea_and_IdBodega = New clsBeBodega_area
                            Dim pBeBodega As New clsBeBodega_area
                            Cargar(pBeBodega, lDataTable.Rows(0))
                            GetSingle_By_IdArea_and_IdBodega = pBeBodega

                        End If

                    End Using

                End Using

            End Using

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    '#GT06082025: mostrar las areas (bodegas) de cealsa en combo
    Public Shared Function Get_All_For_Combo() As DataTable

        Try

            Dim vSQL As String = "SELECT IdArea,Descripcion as Nombre FROM bodega_area 
                                        group by IdArea,Descripcion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
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

    Public Shared Function Get_Single_By_Codigo_Bodega(CodigoBodega As String, lConnection As SqlConnection, lTransaction As SqlTransaction) As clsBeBodega_area

        Get_Single_By_Codigo_Bodega = Nothing

        Try


            Const sp As String = "SELECT * FROM Bodega_area " &
          " Where Codigo = @codigo_bodega "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BODEGA", CodigoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega_area
                Cargar(pBeBodega, dt.Rows(0))
                Get_Single_By_Codigo_Bodega = pBeBodega
            Else
                Get_Single_By_Codigo_Bodega = Nothing
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Function

#End Region

End Class