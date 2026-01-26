Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnBodega_sector
    Implements IDisposable

    ''' <summary>
    ''' Funcion que me devuelte un Sector y recibe como parametro un IDSECTOR
    ''' </summary>
    ''' <param name="pIdSector"></param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul</remarks>
    Public Shared Function GetSingle(ByVal pIdSector As Integer, ByVal pIdBodega As Integer) As clsBeBodega_sector

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM bodega_Sector WHERE IdSector=@IdSector AND IdBodega=@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim Obj As clsBeBodega_sector

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBeBodega_sector()
                            Cargar(Obj, lRow)
                            GetSingle = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaSector_GetSingle: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTramo(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer) As clsBeBodega_sector

        Get_Single_By_IdTramo = Nothing

        Try


            Dim vSQL As String = "SELECT bodega_tramo.IdTramo, bodega_sector.* " &
                    " FROM bodega_sector INNER JOIN " &
                    " bodega_tramo ON bodega_sector.IdSector = bodega_tramo.IdSector " &
                    " WHERE bodega_tramo.IdTramo = @IdTramo AND bodega_sector.IdBodega=@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    Debug.Print("GetSingleByIdTramo: " & pIdTramo)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeBodega_sector

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)

                        Obj = New clsBeBodega_sector()

                        Cargar(Obj, lRow)
                        Return Obj

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaSector_GetSingle: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTramo(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As clsBeBodega_sector

        Get_Single_By_IdTramo = Nothing

        Try

            Dim vSQL As String = "SELECT bodega_tramo.IdTramo, bodega_sector.* " &
                        " FROM bodega_sector INNER JOIN " &
                        " bodega_tramo ON bodega_sector.IdSector = bodega_tramo.IdSector " &
                        " WHERE bodega_tramo.IdTramo = @IdTramo AND bodega_sector.IdBodega=@IdBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Dim Obj As clsBeBodega_sector

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Obj = New clsBeBodega_sector()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaSector_GetSingle: " & ex.Message)
        End Try

    End Function


    Public Shared Function Get_Single_By_IdBodega_IdArea_IdSector(ByRef pIdBodega As Integer, ByRef pIdArea As Integer, ByRef pIdSector As Integer,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As clsBeBodega_sector

        Get_Single_By_IdBodega_IdArea_IdSector = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Bodega_sector 
                                    Where(IdBodega=@IdBodega  and IdArea=@IdArea and IdSector = @IdSector)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)


                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Dim Obj As clsBeBodega_sector

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Obj = New clsBeBodega_sector()

                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaSector_GetSingle: " & ex.Message)
        End Try

    End Function


    ''' <summary>
    ''' Funcion que me devuelve todos los sectores recibe como parametros si son Activos o No, y un IDAREA
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <param name="pIdArea"></param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul</remarks>
    Public Shared Function Get_All_By_IdArea_And_IdBodega(ByVal pActivo As Boolean,
                                                          ByVal pIdArea As Integer,
                                                          ByVal pIdBodega As Integer) As List(Of clsBeBodega_sector)


        Try

            Dim lReturnList As New List(Of clsBeBodega_sector)


            Dim vSQL As String = "SELECT * FROM bodega_sector WHERE IdArea=@IdArea AND IdBodega=@IdBodega "

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_sector

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_sector()
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
            Throw New Exception("BodegaSector_GetAllBySectorBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdArea_And_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeBodega_sector)


        Try

            Dim lReturnList As New List(Of clsBeBodega_sector)


            Dim vSQL As String = "SELECT * FROM bodega_sector WHERE IdBodega=@IdBodega "

            vSQL += " AND Activo=1"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_sector

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_sector()
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
            Throw New Exception("BodegaSector_GetAllBySectorBodega: " & ex.Message)
        End Try

    End Function


    Public Shared Function Get_All_By_IdArea_And_IdBodega(ByVal pIdBodega As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_sector)


        Try

            Dim lReturnList As New List(Of clsBeBodega_sector)

            Dim vSQL As String = "SELECT * FROM bodega_sector WHERE IdBodega=@IdBodega AND Activo=1 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_sector

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_sector()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaSector_GetAllBySectorBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdArea_And_IdBodega_DT(ByVal pActivo As Boolean,
                                                             ByVal pIdArea As Integer,
                                                             ByVal pIdBodega As Integer) As DataTable


        Get_All_By_IdArea_And_IdBodega_DT = Nothing

        Try

            Dim vSQL As String = "SELECT IdSector, IdArea, sistema, descripcion, user_agr, fec_agr, user_mod, fec_mod, activo, alto, largo, ancho, 
                                         margen_izquierdo, margen_derecho, margen_superior, margen_inferior, Codigo, IdSectorIzquierda, 
                                         IdSectorDerecha, horizontal Vertical, pos_x, pos_y, IdBodega 
                                  FROM bodega_sector 
                                  WHERE IdArea=@IdArea AND IdBodega=@IdBodega "

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            vSQL += " ORDER BY DESCRIPCION "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdArea_And_IdBodega_DT = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaSector_GetAllBySectorBodega: " & ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Funcion que me devuelve en una tabla todos los sectores recibe como parametro un id Area
    ''' </summary>
    ''' <param name="pIdArea"></param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul</remarks>
    Public Shared Function Get_All_Sector_By_Area_And_IdBodega(ByVal pIdArea As Integer,
                                                               ByVal IdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT * FROM bodega_sector WHERE IdArea=@IdArea And IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

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

    Public Shared Function Get_All_Sector_By_Area_And_IdBodega(ByVal pIdArea As Integer,
                                                               ByVal IdBodega As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim vSQL As String = "SELECT * FROM bodega_sector WHERE IdArea=@IdArea And IdBodega=@IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Sector_By_IdArea_And_IdInventario(ByVal pIdArea As Integer,
                                                                     ByVal IdInventario As Integer,
                                                                     ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT bodega_sector.IdSector, bodega_sector.IdArea, bodega_sector.sistema, bodega_sector.descripcion, bodega_sector.user_agr, bodega_sector.fec_agr, 
                                    bodega_sector.user_mod, bodega_sector.fec_mod, bodega_sector.activo, bodega_sector.alto, bodega_sector.largo, bodega_sector.ancho, bodega_sector.margen_izquierdo, 
                                    bodega_sector.margen_derecho, bodega_sector.margen_superior, bodega_sector.margen_inferior, bodega_sector.Codigo, bodega_sector.IdSectorIzquierda, bodega_sector.IdSectorDerecha, 
                                    bodega_sector.horizontal, bodega_sector.pos_x, bodega_sector.pos_y
                                    FROM bodega_ubicacion INNER JOIN
                                    trans_inv_stock ON bodega_ubicacion.IdUbicacion = trans_inv_stock.IdUbicacion AND bodega_ubicacion.IdBodega = trans_inv_stock.IdBodega INNER JOIN
                                    bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdSector = bodega_tramo.IdSector AND 
                                    bodega_ubicacion.IdBodega = bodega_tramo.IdBodega INNER JOIN
                                    bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector AND bodega_tramo.IdBodega = bodega_sector.IdBodega                                    
                         WHERE bodega_sector.IdArea=@IdArea 
                         AND trans_inv_stock.idinventario = @IdInventario 
                         AND trans_inv_stock.IdBodega = @IdBodega
                         GROUP BY bodega_sector.IdSector, bodega_sector.IdArea, bodega_sector.descripcion, bodega_sector.user_agr, bodega_sector.fec_agr, 
                               bodega_sector.user_mod, bodega_sector.fec_mod, bodega_sector.alto, bodega_sector.largo, bodega_sector.ancho, 
                               bodega_sector.margen_izquierdo, bodega_sector.margen_derecho, bodega_sector.margen_superior, bodega_sector.margen_inferior, 
                               bodega_sector.Codigo, bodega_sector.IdSectorIzquierda, bodega_sector.IdSectorDerecha, bodega_sector.pos_x, bodega_sector.pos_y, 
                               bodega_sector.sistema, bodega_sector.activo, bodega_sector.horizontal
                         ORDER BY bodega_sector.IdArea, bodega_sector.IdSector "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
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

    Public Shared Function Get_All_Sector_By_IdArea_And_IdInventario(ByVal pIdArea As Integer,
                                                                     ByVal IdInventario As Integer,
                                                                     ByVal pIdBodega As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim vSQL As String = "SELECT bodega_sector.IdSector, bodega_sector.IdArea, bodega_sector.sistema, bodega_sector.descripcion, bodega_sector.user_agr, bodega_sector.fec_agr, 
                                    bodega_sector.user_mod, bodega_sector.fec_mod, bodega_sector.activo, bodega_sector.alto, bodega_sector.largo, bodega_sector.ancho, bodega_sector.margen_izquierdo, 
                                    bodega_sector.margen_derecho, bodega_sector.margen_superior, bodega_sector.margen_inferior, bodega_sector.Codigo, bodega_sector.IdSectorIzquierda, bodega_sector.IdSectorDerecha, 
                                    bodega_sector.horizontal, bodega_sector.pos_x, bodega_sector.pos_y
                                    FROM bodega_ubicacion INNER JOIN
                                    trans_inv_stock ON bodega_ubicacion.IdUbicacion = trans_inv_stock.IdUbicacion AND bodega_ubicacion.IdBodega = trans_inv_stock.IdBodega INNER JOIN
                                    bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdSector = bodega_tramo.IdSector AND 
                                    bodega_ubicacion.IdBodega = bodega_tramo.IdBodega INNER JOIN
                                    bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector AND bodega_tramo.IdBodega = bodega_sector.IdBodega                                    
                                    WHERE bodega_sector.IdArea=@IdArea 
                                    AND trans_inv_stock.idinventario = @IdInventario 
                                    AND trans_inv_stock.IdBodega = @IdBodega
                                    GROUP BY bodega_sector.IdSector, bodega_sector.IdArea, bodega_sector.descripcion, bodega_sector.user_agr, bodega_sector.fec_agr, 
                                           bodega_sector.user_mod, bodega_sector.fec_mod, bodega_sector.alto, bodega_sector.largo, bodega_sector.ancho, 
                                           bodega_sector.margen_izquierdo, bodega_sector.margen_derecho, bodega_sector.margen_superior, bodega_sector.margen_inferior, 
                                           bodega_sector.Codigo, bodega_sector.IdSectorIzquierda, bodega_sector.IdSectorDerecha, bodega_sector.pos_x, bodega_sector.pos_y, 
                                           bodega_sector.sistema, bodega_sector.activo, bodega_sector.horizontal
                                    ORDER BY bodega_sector.IdArea, bodega_sector.IdSector "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdArea_For_Inventario(ByVal pIdArea As Integer, ByVal pIdInventario As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Dim vSQL As String = "SELECT * FROM bodega_sector WHERE IdArea=@IdArea"

                Dim vSQL As String = "SELECT trans_inv_ciclico_ubic.idinventarioenc, bodega_sector.IdSector, bodega_sector.IdArea, bodega_sector.sistema, bodega_sector.descripcion, 
                         bodega_sector.user_agr, bodega_sector.fec_agr, bodega_sector.user_mod, bodega_sector.fec_mod, bodega_sector.activo, 
                         bodega_sector.alto, bodega_sector.largo, bodega_sector.ancho, bodega_sector.margen_izquierdo, bodega_sector.margen_derecho, 
                         bodega_sector.margen_superior, bodega_sector.margen_inferior, bodega_sector.Codigo, bodega_sector.IdSectorIzquierda, 
                         bodega_sector.IdSectorDerecha, bodega_sector.horizontal, bodega_sector.pos_x, bodega_sector.pos_y
                        FROM bodega_ubicacion INNER JOIN
                         trans_inv_ciclico_ubic ON bodega_ubicacion.IdUbicacion = trans_inv_ciclico_ubic.idubicacion INNER JOIN
                         bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo INNER JOIN
                         bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector
                        WHERE bodega_sector.IdArea=@IdArea AND trans_inv_ciclico_ubic.idinventarioenc = @IdInventario
                        GROUP BY trans_inv_ciclico_ubic.idinventarioenc, bodega_sector.IdSector, bodega_sector.IdArea, bodega_sector.descripcion,bodega_sector.user_agr, 
                         bodega_sector.fec_agr, bodega_sector.user_mod, bodega_sector.fec_mod, bodega_sector.alto, bodega_sector.largo, 
                         bodega_sector.ancho, bodega_sector.margen_izquierdo,bodega_sector.margen_derecho,bodega_sector.margen_superior, 
                         bodega_sector.margen_inferior, bodega_sector.Codigo, bodega_sector.IdSectorIzquierda,bodega_sector.IdSectorDerecha, 
                         bodega_sector.pos_x, bodega_sector.pos_y, bodega_sector.sistema, bodega_sector.activo, bodega_sector.horizontal"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

                    Dim lDataTable As New DataTable

                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Sector_By_IdArea_And_IdBodega_For_Combo(ByVal pIdArea As Integer, ByVal pIdBodega As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdSector,Descripcion as Nombre FROM bodega_sector WHERE IdArea=@IdArea AND IdBodega=@IdBodega ORDER BY Descripcion "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
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

    Public Shared Function Get_All_Sector_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_All_Sector_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT bodega_sector.*
                      FROM   bodega_sector INNER JOIN dbo.bodega_area ON bodega_sector.IdArea = bodega_area.IdArea 
                             AND bodega_sector.IdBodega = bodega_area.IdBodega
                      WHERE  (bodega_area.IdBodega = @IdBodega AND bodega_sector.activo=1) ORDER BY bodega_sector.descripcion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Get_All_Sector_By_IdBodega = lDataTable

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()


            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Sector_By_IdBodega(ByVal pIdBodega As Integer,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_Sector_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT bodega_sector.*
                      FROM   bodega_sector INNER JOIN dbo.bodega_area ON bodega_sector.IdArea = bodega_area.IdArea 
                             AND bodega_sector.IdBodega = bodega_area.IdBodega
                      WHERE  (bodega_area.IdBodega = @IdBodega AND bodega_sector.activo=1) ORDER BY bodega_sector.descripcion"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Get_All_Sector_By_IdBodega = lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Listar(ByVal pIdBodega As Integer, ByRef lTransaction As SqlTransaction,
                                  ByRef lConnection As SqlConnection) As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega_sector WHERE IdBodega=@IdBodega order by idsector, idarea, descripcion "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeBodega_sector As clsBeBodega_sector,
                                   ByRef lTransaction As SqlTransaction,
                                   ByRef lConnection As SqlConnection) As Boolean

        Try

            Const sp As String = "SELECT * FROM Bodega_sector" &
            " Where(IdSector = @IdSector AND IdBodega=@IdBodega )"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_sector.IdSector))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", oBeBodega_sector.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_sector, dt.Rows(0))
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


    Public Shared Function Limpiar_Todo(ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from bodega_sector where IdBodega = @IdBodega"

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

    '#GT14012025: obtener sectores según la ubicación para portal web cealsa
    Public Shared Function Get_All_By_IdArea_And_IdSector(ByVal pIdArea As Integer,
                                                          ByVal pIdSector As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_sector)

        Get_All_By_IdArea_And_IdSector = Nothing

        Try



            Dim vSQL As String = "SELECT * FROM bodega_sector WHERE (IdArea=@IdArea and IdSector=@IdSector AND Activo=1 )"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_sector

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_All_By_IdArea_And_IdSector = New List(Of clsBeBodega_sector)

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeBodega_sector()
                        Cargar(Obj, lRow)
                        Get_All_By_IdArea_And_IdSector.Add(Obj)

                    Next

                End If

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaSector_GetAllByIdIdAreaIdSector: " & ex.Message)
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
