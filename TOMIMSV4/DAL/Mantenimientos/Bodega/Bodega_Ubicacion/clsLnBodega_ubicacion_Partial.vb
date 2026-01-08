Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Partial Public Class clsLnBodega_ubicacion
    Implements IDisposable

    Public Shared Function GetSingle(ByVal pIdUbicacion As Integer, ByVal pIdBodega As Integer, Optional pSoloRecepcion As Boolean = False) As clsBeBodega_ubicacion

        GetSingle = Nothing

        Dim vSQL As String = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    If pSoloRecepcion Then

                        vSQL = "SELECT DISTINCT u.* FROM bodega_ubicacion AS u " _
                                         & "INNER JOIN bodega_tramo AS t ON u.Idtramo = u.IdTramo " _
                                         & "INNER JOIN bodega_sector AS s ON t.IdSector = s.IdSector " _
                                         & "INNER JOIN bodega_area AS a ON s.IdArea = a.IdArea " _
                                         & "WHERE a.IdBodega=@IdBodega AND u.IdUbicacion=@IdUbicacion AND u.ubicacion_recepcion=1"

                    Else

                        vSQL = "SELECT DISTINCT u.* FROM bodega_ubicacion AS u " _
                                     & "INNER JOIN bodega_tramo AS t ON u.Idtramo = u.IdTramo " _
                                     & "INNER JOIN bodega_sector AS s ON t.IdSector = s.IdSector " _
                                     & "INNER JOIN bodega_area AS a ON s.IdArea = a.IdArea " _
                                     & "WHERE a.IdBodega=@IdBodega AND u.IdUbicacion=@IdUbicacion"

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBeBodega_ubicacion()
                            Cargar(Obj, lRow, lTransaction, lConnection)
                            Return Obj

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

    ''' <summary>
    ''' 'Funcion que me devuelve una lista de objetos de tipo clsBeBodega_ubicacion
    ''' </summary>
    ''' <param name="pActivo">filtra las ubicaciones que sean activas y que pertenezcan a un PidTramo</param>
    ''' <param name="pIdTramo"></param>
    ''' <returns>retorna una lista de objetos de tipo clsBeBodega_ubicacion</returns>
    ''' <remarks>Bcuscul 13052016</remarks>
    Public Shared Function Get_All_Ubicaciones_By_IdTramo_And_IdBodega(ByVal pActivo As Boolean,
                                                                       ByVal pIdTramo As Integer,
                                                                       ByVal pIdBodega As Integer) As List(Of clsBeBodega_ubicacion)

        Get_All_Ubicaciones_By_IdTramo_And_IdBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Dim vSQL As String = "SELECT * FROM bodega_Ubicacion WHERE IdTramo=@IdTramo AND IdBodega=@IdBodega "

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
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_ubicacion()
                                Cargar(Obj, lRow, lTransaction, lConnection)
                                lReturnList.Add(Obj)

                            Next

                            Get_All_Ubicaciones_By_IdTramo_And_IdBodega = lReturnList

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

    Public Shared Function Get_All_Ubicaciones_By_IdTramo_And_IdBodega_DT(ByVal pActivo As Boolean,
                                                                          ByVal pIdTramo As Integer,
                                                                          ByVal pIdBodega As Integer) As DataTable

        Get_All_Ubicaciones_By_IdTramo_And_IdBodega_DT = Nothing

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Dim vSQL As String = "SELECT * FROM bodega_Ubicacion WHERE IdTramo=@IdTramo AND IdBodega=@IdBodega "

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
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_All_Ubicaciones_By_IdTramo_And_IdBodega_DT = lDataTable
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

    Public Shared Function Get_All_Ubicaciones_By_IdTramo(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer) As List(Of clsBeBodega_Ubicacion_Seleccion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_Ubicacion_Seleccion)

            Dim vSQL As String = "SELECT bodega_ubicacion.IdBodega, 
                                 bodega_ubicacion.IdUbicacion,
                                 bodega_ubicacion.descripcion,bodega_ubicacion.codigo_barra, 
                                 bodega_tramo.descripcion AS DescripcionTramo, 
                                 bodega_ubicacion.ubicacion_picking,
                                 dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS Nombre_Completo
                                 FROM bodega_tramo 
                                 INNER JOIN bodega_ubicacion 
                                 ON bodega_tramo.IdTramo = bodega_ubicacion.IdTramo and bodega_tramo.IdBodega = bodega_ubicacion.IdBodega
                                 WHERE bodega_ubicacion.IdTramo=@IdTramo 
                                 AND bodega_ubicacion.IdBodega = @IdBodega 
                                 AND bodega_ubicacion.Activo=1
                                 ORDER BY bodega_ubicacion.IdTramo, 
                                 bodega_ubicacion.Indice_x,bodega_ubicacion.Nivel,bodega_ubicacion.IdUbicacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_Ubicacion_Seleccion
                        Dim Ubi As New clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_Ubicacion_Seleccion()
                                Obj.IdBodega = lRow.Item("IdBodega")
                                Obj.IdUbicacion = lRow.Item("IdUbicacion")
                                Obj.Codigo_Barra = IIf(IsDBNull(lRow.Item("Codigo_Barra")), "", lRow.Item("Codigo_Barra"))
                                Obj.Descripcion = IIf(IsDBNull(lRow.Item("Descripcion")), "", lRow.Item("Descripcion"))
                                Obj.IdTramo = pIdTramo
                                Obj.DescripcionTramo = IIf(IsDBNull(lRow.Item("DescripcionTramo")), "", lRow.Item("DescripcionTramo"))
                                Obj.Ubicacion_Picking = IIf(IsDBNull(lRow.Item("ubicacion_picking")), False, lRow.Item("ubicacion_picking"))
                                Ubi.IdUbicacion = Obj.IdUbicacion
                                Ubi.IdBodega = pIdBodega
                                Obj.Descripcion = IIf(IsDBNull(lRow.Item("Nombre_Completo")), "", lRow.Item("Nombre_Completo")) 'Ubi.NombreCompleto
                                Obj.Seleccionar = False
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

    ''' <summary>
    ''' Función que regresa una lista de ubicaciones para la seleccion, inicialmente se hizo para seleccionar en producto estado
    ''' CREADA POR Erik Calderón
    ''' Fue modificada por Bcuscul se agrego el parametro tipo de Ubicacion para saber de que tipo de ubicacion 
    ''' se quiere filtrar 
    ''' #CKFK20230427 quité las relaciones con tramo, sector y bodega porque ya no son necesarias y porque el inner join estaba incorrecto
    ''' y no se aplicaba correctamente el filtro por bodega
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_All_By_IdBodega(ByVal pActivo As Boolean, ByVal pIdBodega As Integer, ByVal nombreCampo As String) As List(Of clsBeBodega_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = ""

                    If pIdBodega <> 0 Then

                        vSQL = "SELECT DISTINCT u.IdUbicacion, 
                                       dbo.Nombre_Completo_Ubicacion(u.IdUbicacion, u.IdBodega) as Descripcion  
                                FROM bodega_ubicacion AS u 
                                WHERE u.IdBodega=@IdBodega AND u." & nombreCampo & " = 1 "

                    Else

                        vSQL = String.Format("SELECT DISTINCT u.IdUbicacion, 
                                                     dbo.Nombre_Completo_Ubicacion(u.IdUbicacion, u.IdBodega) as Descripcion  
                                              FROM bodega_ubicacion AS u  
                                              WHERE u." & nombreCampo & " = 1 ", pIdBodega)

                    End If

                    If (nombreCampo = "") Then

                        vSQL = "SELECT DISTINCT u.IdUbicacion,  
                                       dbo.Nombre_Completo_Ubicacion(u.IdUbicacion, u.IdBodega) as Descripcion    
                                FROM bodega_ubicacion AS u  
                                WHERE u.IdBodega=@IdBodega"

                    End If

                    If pActivo Then
                        vSQL += " AND u.Activo = 1 "
                    Else
                        vSQL += " AND u.Activo = 0 "
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_ubicacion()
                                Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)

                                If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                                    Obj.Descripcion = CType(lRow("Descripcion"), String)
                                End If

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

    Public Shared Function Get_Ubicaciones_Picking_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeBodega_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_Ubicaciones_Picking WHERE IdBodega = @IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_ubicacion()
                                Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                                Obj.IdBodega = pIdBodega
                                If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                                    Obj.Descripcion = CType(lRow("Descripcion"), String)
                                End If

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

    Public Shared Function Get_All_Ubicaciones_Nivel_By_Posicion(ByVal BeBodegaUbicacion As clsBeBodega_ubicacion) As List(Of clsBeBodega_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT * FROM bodega_Ubicacion 
                        WHERE IdTramo=@IdTramo
                        And indice_x = @indice_x 
                        And descripcion Like '%" & BeBodegaUbicacion.Descripcion.Substring(1, 1) & "%' "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", BeBodegaUbicacion.IdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@indice_x", BeBodegaUbicacion.Indice_x)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_ubicacion()

                                Cargar(Obj, lRow, lTransaction, lConnection)

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

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeBodega_ubicacion)

        Dim vSQL As String = ""

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    If pIdBodega <> 0 Then

                        vSQL = "SELECT DISTINCT u.IdUbicacion, u.Descripcion FROM bodega_ubicacion AS u " _
                                   & "INNER JOIN bodega_tramo AS t ON u.Idtramo = u.IdTramo " _
                                   & "INNER JOIN bodega_sector AS s ON t.IdSector = s.IdSector " _
                                   & "INNER JOIN bodega_area AS a ON s.IdArea = a.IdArea " _
                                   & "WHERE a.IdBodega=@IdBodega And u.Activo =1 And u.Bloqueada = 0 "

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_ubicacion()
                                Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)

                                If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                                    Obj.Descripcion = CType(lRow("Descripcion"), String)
                                End If

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

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_ubicacion)

        Dim vSQL As String = ""

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            If pIdBodega <> 0 Then

                vSQL = "SELECT DISTINCT u.IdUbicacion, u.Descripcion FROM bodega_ubicacion AS u " _
                        & "INNER JOIN bodega_tramo AS t ON u.Idtramo = u.IdTramo AND u.IdBodega = t.IdBodega " _
                        & "INNER JOIN bodega_sector AS s ON t.IdSector = s.IdSector AND u.IdBodega = s.IdBodega " _
                        & "INNER JOIN bodega_area AS a ON s.IdArea = a.IdArea AND u.IdBodega = a.IdBodega " _
                        & "WHERE a.IdBodega=@IdBodega And u.Activo =1 And u.Bloqueada = 0 "

            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_ubicacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_ubicacion()
                        Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)

                        If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                            Obj.Descripcion = CType(lRow("Descripcion"), String)
                        End If

                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_IdSector_And_IdTramo(ByVal pIdBodega As Integer,
                                                                        ByVal pIdSector As Integer,
                                                                        ByVal pIdTramo As Integer,
                                                                        ByVal lConnection As SqlConnection,
                                                                        ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_ubicacion)

        Dim vSQL As String = ""

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            If pIdBodega <> 0 Then

                vSQL = "SELECT *
                        FROM bodega_ubicacion AS u INNER JOIN 
                             bodega_tramo AS t ON t.Idtramo = u.IdTramo AND 
                                                  t.IdBodega = u.IdBodega INNER JOIN 
                             bodega_sector AS s ON s.IdSector = u.IdSector AND 
                                                   s.IdBodega = u.IdBodega INNER JOIN 
                             bodega_area AS a ON a.IdArea = u.IdArea AND 
                                                 a.IdBodega = u.IdBodega 
                        WHERE u.IdBodega=@IdBodega AND 
                              u.Activo =1 AND 
                              u.Bloqueada = 0 AND
                              u.IdTramo =@IdTramo AND 
                              u.IdSector = @IdSector"

            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_ubicacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_ubicacion()
                        clsLnBodega_ubicacion.Cargar(Obj, lRow)
                        Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)

                        If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                            Obj.Descripcion = CType(lRow("Descripcion"), String)
                        End If

                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Ubicaciones_By_IdTramo_And_IdBodega(ByVal pIdTramo As Integer,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction,
                                                                       Optional NombreCampo As String = "",
                                                                       Optional Activo As Integer = 1,
                                                                       Optional IdIndiceRotacion As Integer = 0) As DataTable

        Dim vSQL As String = ""

        Try

            If NombreCampo = Nothing Then

                If IdIndiceRotacion = 0 Then
                    vSQL = "SELECT bodega_ubicacion.*,dbo.Nombre_Completo_Ubicacion(IdUbicacion,IdBodega) as Nombre_Completo
                                    FROM bodega_ubicacion WHERE IdTramo=@IdTramo AND IdBodega=@IdBodega AND Activo = 1 "
                Else
                    vSQL = "SELECT * FROM bodega_ubicacion WHERE IdTramo=@IdTramo AND IdBodega=@IdBodega And (IdIndiceRotacion=@IdIndiceRotacion Or IdIndiceRotacion Is null)"
                End If

            Else
                vSQL = String.Format("SELECT * FROM bodega_ubicacion 
                                    WHERE IdTramo=@IdTramo 
                                    AND IdBodega=@IdBodega 
                                    AND Activo=@Activo 
                                    AND {0} = {1} ", NombreCampo, Activo)
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdIndiceRotacion", IdIndiceRotacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@Activo", Activo)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 'Funcion creada para filtrar las ubicacines dependiendo del tipo que sean 
    ''' </summary>
    ''' <param name="pIdTramo">Id Tramo, nombreCampo(para seleccionar las ubicaciones de tipo recepcion, despacho,picking,merma, Activo</param>
    ''' <param name="NombreCampo"></param>
    ''' <param name="Activo"></param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul/Ejcalderon</remarks>
    Public Shared Function Get_All_Ubicaciones_By_IdTramo_And_IdBodega(ByVal pIdTramo As Integer,
                                                                       ByVal pIdBodega As Integer,
                                                                       Optional NombreCampo As String = "",
                                                                       Optional Activo As Integer = 1,
                                                                       Optional IdIndiceRotacion As Integer = 0) As DataTable

        Dim vSQL As String = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    If NombreCampo = Nothing Then

                        If IdIndiceRotacion = 0 Then

                            vSQL = "SELECT IdUbicacion,
                                    [IdTramo], dbo.Nombre_Completo_Ubicacion(IdUbicacion,IdBodega) as [descripcion] ,[ancho],[largo],[alto],[nivel],[indice_x],[IdIndiceRotacion],
                                    [IdTipoRotacion],[sistema],[codigo_barra],[codigo_barra2],[user_agr],[fec_agr],[user_mod],
                                    [fec_mod],[dañado],[activo],[bloqueada],[acepta_pallet],[ubicacion_picking],
                                    [ubicacion_recepcion],[ubicacion_despacho],[ubicacion_merma],[margen_izquierdo],
                                    [margen_derecho],[margen_superior],[margen_inferior],[orientacion_pos],[ubicacion_virtual],
                                    [ubicacion_ne],[IdBodega],[IdArea],[IdSector]
                                    FROM bodega_ubicacion 
                                    WHERE IdTramo=@IdTramo AND IdBodega=@IdBodega"
                        Else
                            vSQL = "SELECT * FROM bodega_ubicacion WHERE IdTramo=@IdTramo AND IdBodega=@IdBodega And (IdIndiceRotacion=@IdIndiceRotacion Or IdIndiceRotacion Is null)"
                        End If

                    Else
                        vSQL = String.Format("SELECT * FROM bodega_ubicacion 
                            WHERE IdTramo=@IdTramo 
                            AND IdBodega=@IdBodega 
                            AND Activo=@Activo 
                            AND {0} = {1} ", NombreCampo, Activo)
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdIndiceRotacion", IdIndiceRotacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Activo", Activo)

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

    Public Shared Function Get_All_Ubicaciones_By_Existencias(ByVal pIdTramo As Integer,
                                                              ByVal IdInventario As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction,
                                                              Optional NombreCampo As String = "",
                                                              Optional Activo As Integer = 1,
                                                              Optional IdIndiceRotacion As Integer = 0) As DataTable

        Dim vSQL As String = ""

        Try

            If NombreCampo = Nothing Then

                If IdIndiceRotacion = 0 Then

                    vSQL = "SELECT * FROM bodega_ubicacion WHERE IdTramo=@IdTramo AND IdBodega = @IdBodega "

                Else

                    'GT 02092021: se agrega al group by la funcion nombre_completo_ubicacion
                    vSQL = " SELECT bodega_ubicacion.IdUbicacion,
                            dbo.Nombre_Completo_Ubicacion(trans_inv_stock.IdUbicacion,trans_inv_stock.IdBodega) AS Nombre_Completo
                            FROM bodega_ubicacion INNER JOIN
                            trans_inv_stock ON bodega_ubicacion.IdUbicacion = trans_inv_stock.IdUbicacion 
                            AND dbo.bodega_ubicacion.IdBodega = dbo.trans_inv_stock.IdBodega
                            INNER JOIN
                            bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdSector = bodega_tramo.IdSector AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega AND 
                            bodega_ubicacion.IdArea = bodega_tramo.IdArea 
                            WHERE bodega_tramo.IdTramo=@IdTramo
                            AND trans_inv_stock.idinventario = @IdInventario 
                            AND trans_inv_stock.IdBodega = @IdBodega 
                            AND bodega_ubicacion.activo = @Activo
                            GROUP BY bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.descripcion, bodega_tramo.user_agr, bodega_tramo.fec_agr, bodega_tramo.user_mod, bodega_tramo.fec_mod, 
                            bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, bodega_tramo.margen_superior, bodega_tramo.margen_inferior, 
                            bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc, bodega_tramo.IdTipoRack, bodega_tramo.sistema, 
                            bodega_tramo.activo, bodega_tramo.es_rack,bodega_ubicacion.nivel,bodega_ubicacion.orientacion_pos, bodega_ubicacion.IdUbicacion,bodega_ubicacion.indice_x,
                            dbo.Nombre_Completo_Ubicacion(trans_inv_stock.IdUbicacion,trans_inv_stock.IdBodega)
                            ORDER BY bodega_tramo.IdSector,  bodega_tramo.IdTramo"


                End If

            Else
                vSQL = String.Format("SELECT * FROM bodega_ubicacion 
                            WHERE 
                            IdTramo=@IdTramo 
                            AND IdBodega =@IdBodega
                            AND Activo=@Activo 
                            AND {0} = {1} ", NombreCampo, Activo)
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdIndiceRotacion", IdIndiceRotacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Ubicaciones_By_Existencias(ByVal pIdTramo As Integer,
                                                              ByVal IdInventario As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              Optional NombreCampo As String = "",
                                                              Optional Activo As Integer = 1,
                                                              Optional IdIndiceRotacion As Integer = 0) As DataTable

        Dim vSQL As String = ""

        Try

            If NombreCampo = Nothing Then

                If IdIndiceRotacion = 0 Then

                    vSQL = "SELECT * FROM bodega_ubicacion WHERE IdTramo=@IdTramo AND IdBodega = @IdBodega "

                Else

                    'GT 02092021: se agrega al group by la funcion nombre_completo_ubicacion
                    vSQL = " SELECT bodega_ubicacion.IdUbicacion,
                            dbo.Nombre_Completo_Ubicacion(trans_inv_stock.IdUbicacion,trans_inv_stock.IdBodega) AS Nombre_Completo
                            FROM bodega_ubicacion INNER JOIN
                            trans_inv_stock ON bodega_ubicacion.IdUbicacion = trans_inv_stock.IdUbicacion 
                            AND dbo.bodega_ubicacion.IdBodega = dbo.trans_inv_stock.IdBodega
                            INNER JOIN
                            bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdSector = bodega_tramo.IdSector AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega AND 
                            bodega_ubicacion.IdArea = bodega_tramo.IdArea 
                            WHERE bodega_tramo.IdTramo=@IdTramo
                            AND trans_inv_stock.idinventario = @IdInventario 
                            AND trans_inv_stock.IdBodega = @IdBodega 
                            AND bodega_ubicacion.activo = @Activo
                            GROUP BY bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.descripcion, bodega_tramo.user_agr, bodega_tramo.fec_agr, bodega_tramo.user_mod, bodega_tramo.fec_mod, 
                            bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, bodega_tramo.margen_superior, bodega_tramo.margen_inferior, 
                            bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc, bodega_tramo.IdTipoRack, bodega_tramo.sistema, 
                            bodega_tramo.activo, bodega_tramo.es_rack,bodega_ubicacion.nivel,bodega_ubicacion.orientacion_pos, bodega_ubicacion.IdUbicacion,bodega_ubicacion.indice_x,
                            dbo.Nombre_Completo_Ubicacion(trans_inv_stock.IdUbicacion,trans_inv_stock.IdBodega)
                            ORDER BY bodega_tramo.IdSector,  bodega_tramo.IdTramo"


                End If

            Else
                vSQL = String.Format("SELECT * FROM bodega_ubicacion 
                            WHERE 
                            IdTramo=@IdTramo 
                            AND IdBodega =@IdBodega
                            AND Activo=@Activo 
                            AND {0} = {1} ", NombreCampo, Activo)
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdIndiceRotacion", IdIndiceRotacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)

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

    '#EJC20180411
    Public Shared Function Get_All_Ubicaciones_By_IdTramo_And_IdBodega_DT(ByVal pIdTramo As Integer,
                                                                          ByVal pIdBodega As Integer,
                                                                          ByRef lConnection As SqlConnection,
                                                                          ByRef lTransaction As SqlTransaction) As DataTable

        Try

            Dim vSQL As String = String.Format("SELECT u.*,
                                                dbo.Nombre_Completo_Ubicacion(u.IdUbicacion,u.IdBodega) AS NOMBRE_COMPLETO
                                                FROM bodega_ubicacion u 
                                                inner join bodega_tramo t on u.idtramo = t.idtramo 
                                                and t.IdBodega = u.IdBodega
                                                WHERE u.IdTramo=@IdTramo 
                                                AND u.IdBodega=@IdBodega 
                                                AND u.Activo = 1")

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Ubicaciones_By_IdTramo_And_IdBodega_DT(ByVal pIdTramo As Integer,
                                                                          ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = String.Format("SELECT u.IdBodega, u.IdArea, u.IdSector, 
                                                u.IdTramo, 
                                                u.IdUbicacion, 
                                                u.ancho,
                                                u.alto,
                                                u.largo,
                                                u.nivel, 
                                                u.indice_x, 
                                                i.Descripcion IdIndiceRotacion, 
                                                u.orientacion_pos,
                                                dbo.Nombre_Completo_Ubicacion(u.IdUbicacion,u.IdBodega) AS Nombre,
                                                u.descripcion
                                                FROM bodega_ubicacion u 
                                                inner join bodega_tramo t on u.idtramo = t.idtramo 
                                                join indice_rotacion i on u.IdIndiceRotacion = i.IdIndiceRotacion
                                                and t.IdBodega = u.IdBodega                                                
                                                WHERE u.IdTramo=@IdTramo 
                                                AND u.IdBodega=@IdBodega 
                                                AND u.Activo = 1")

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Get_All_Ubicaciones_By_IdTramo_And_IdBodega_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdTramo_For_Inventario(ByVal pIdTramo As Integer,
                                                             ByVal pIdInventario As Integer,
                                                             Optional Activo As Integer = 1,
                                                             Optional Dañado As Boolean = False) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT trans_inv_ciclico_ubic.idinventarioenc, bodega_ubicacion.*
                                FROM bodega_ubicacion INNER JOIN
                                     trans_inv_ciclico_ubic ON bodega_ubicacion.IdUbicacion = trans_inv_ciclico_ubic.idubicacion
                                WHERE bodega_ubicacion.IdTramo=@IdTramo AND trans_inv_ciclico_ubic.idinventarioenc = @IdInventario AND bodega_ubicacion.Activo = @Activo"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Activo", Activo)

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

    Public Shared Function Get_All_Ubicaciones_By_Tramo_For_Operador(ByVal pIdTramo As Integer,
                                                                     ByVal pIdInventario As Integer,
                                                                     Optional Activo As Integer = 1,
                                                                     Optional Dañado As Boolean = False) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT trans_inv_ciclico_ubic.idinventarioenc, bodega_ubicacion.*
                        FROM operador INNER JOIN
                        trans_inv_operador ON operador.IdOperador = trans_inv_operador.idoperador RIGHT OUTER JOIN
                        bodega_ubicacion INNER JOIN
                        trans_inv_ciclico_ubic ON bodega_ubicacion.IdUbicacion = trans_inv_ciclico_ubic.idubicacion ON 
                        trans_inv_operador.idubic = trans_inv_ciclico_ubic.idubicacion AND 
                        trans_inv_operador.idinventarioenc = trans_inv_ciclico_ubic.idinventarioenc
                        WHERE (bodega_ubicacion.IdTramo = @IdTramo) AND (trans_inv_ciclico_ubic.idinventarioenc = @IdInventario) 
                        AND (bodega_ubicacion.activo = @Activo)
                        GROUP BY trans_inv_ciclico_ubic.idinventarioenc, bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdTramo, bodega_ubicacion.descripcion, 
                        bodega_ubicacion.ancho, bodega_ubicacion.largo, bodega_ubicacion.alto, bodega_ubicacion.nivel,bodega_ubicacion.indice_x, 
                        bodega_ubicacion.IdIndiceRotacion, bodega_ubicacion.IdTipoRotacion, bodega_ubicacion.codigo_barra, bodega_ubicacion.codigo_barra2, 
                        bodega_ubicacion.user_agr, bodega_ubicacion.fec_agr, bodega_ubicacion.user_mod, bodega_ubicacion.fec_mod, 
                        bodega_ubicacion.margen_izquierdo, bodega_ubicacion.margen_derecho, bodega_ubicacion.margen_superior, bodega_ubicacion.margen_inferior, 
                        bodega_ubicacion.orientacion_pos, bodega_ubicacion.sistema, bodega_ubicacion.dañado, bodega_ubicacion.activo, 
                        bodega_ubicacion.bloqueada, bodega_ubicacion.acepta_pallet, bodega_ubicacion.ubicacion_picking, bodega_ubicacion.ubicacion_recepcion, 
                        bodega_ubicacion.ubicacion_despacho, bodega_ubicacion.ubicacion_merma, bodega_ubicacion.ubicacion_virtual"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Activo", Activo)

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

    Public Shared Function Get_Count_Ubicaciones_By_IdTramo(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer) As Integer

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT Count(IdUbicacion) as Cant FROM bodega_ubicacion WHERE IdTramo=@IdTramo and IdBodega=@IdBodega and Activo = 1"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Return lDataTable.Rows(0).Item("Cant")

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Ubicaciones_By_IdTramo(ByVal pIdTramo As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim vSQL As String = "SELECT Count(IdUbicacion) as Cant FROM bodega_ubicacion WHERE IdTramo=@IdTramo and IdBodega=@IdBodega and Activo = 1"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Return lDataTable.Rows(0).Item("Cant")

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Column_Info_By_IdTramo(ByVal pIdTramo As Integer, ByVal pIndiceColumna As Integer) As List(Of clsBeBodega_tramo)

        Get_Column_Info_By_IdTramo = Nothing

        Dim lReturnList As New List(Of clsBeBodega_tramo)
        Dim BeBodegaTramo As New clsBeBodega_tramo

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT nivel as Nivel, alto as Alto, Ancho as Ancho
                        FROM bodega_ubicacion 
                        WHERE IdTramo =@IdTramo 
                        AND Activo = 1
                        AND indice_x = @IndiceColumna
                        group by nivel, alto, ancho, indice_x  "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IndiceColumna", pIndiceColumna)

                        Dim DT As New DataTable

                        lDTA.Fill(DT)

                        For Each Dr As DataRow In DT.Rows

                            BeBodegaTramo = New clsBeBodega_tramo()
                            BeBodegaTramo.Nivel = IIf(IsDBNull(Dr("Nivel")), 0, Dr("Nivel"))
                            BeBodegaTramo.Alto = IIf(IsDBNull(Dr("Alto")), 0, Dr("Alto"))
                            BeBodegaTramo.Ancho = IIf(IsDBNull(Dr("Ancho")), 0, Dr("Ancho"))
                            lReturnList.Add(BeBodegaTramo)

                        Next

                        Return lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Column_Info_By_IdTramo_And_IdBodega(ByVal pIdTramo As Integer,
                                                                  ByVal pIndiceColumna As Integer,
                                                                  ByRef pIdBodega As Integer) As List(Of clsBeBodega_tramo)

        Get_Column_Info_By_IdTramo_And_IdBodega = Nothing

        Dim lReturnList As New List(Of clsBeBodega_tramo)
        Dim BeBodegaTramo As New clsBeBodega_tramo

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT nivel as Nivel, alto as Alto, Ancho as Ancho
                        FROM bodega_ubicacion 
                        WHERE IdTramo =@IdTramo 
                        AND Activo = 1
                        AND indice_x = @IndiceColumna
                        AND idbodega = @IdBodega
                        group by nivel, alto, ancho, indice_x  "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IndiceColumna", pIndiceColumna)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim DT As New DataTable

                        lDTA.Fill(DT)

                        For Each Dr As DataRow In DT.Rows

                            BeBodegaTramo = New clsBeBodega_tramo()
                            BeBodegaTramo.Nivel = IIf(IsDBNull(Dr("Nivel")), 0, Dr("Nivel"))
                            BeBodegaTramo.Alto = IIf(IsDBNull(Dr("Alto")), 0, Dr("Alto"))
                            BeBodegaTramo.Ancho = IIf(IsDBNull(Dr("Ancho")), 0, Dr("Ancho"))
                            lReturnList.Add(BeBodegaTramo)

                        Next

                        Get_Column_Info_By_IdTramo_And_IdBodega = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 'Funcion que me trae una sola ubicacion
    ''' </summary>
    ''' <param name="pIdUbicacion">el parametro que espera la funcion es pIdUbicacion</param>
    ''' <returns>retorna un objeto de tipo clsBeBodega_ubicacion</returns>
    ''' <remarks>Bcuscul13052016</remarks>
    Public Shared Function GetSingle(ByVal pIdUbicacion As Integer,
                                     ByVal pIdBodega As Integer,
                                     Optional ByVal nombreCampo As String = "",
                                     Optional dañado As Boolean = False,
                                     Optional idIndiceRotacion As Integer = 0) As clsBeBodega_ubicacion

        GetSingle = Nothing

        Try

            Dim vSQL As String = ""

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    If nombreCampo = "" Then
                        If idIndiceRotacion = 0 Then
                            vSQL = "SELECT * FROM bodega_ubicacion WHERE IdUbicacion=@IdUbicacion and IdBodega=@IdBodega and dañado=@dañado "
                        Else
                            vSQL = "SELECT * FROM bodega_Ubicacion WHERE IdUbicacion=@IdUbicacion and IdBodega=@IdBodega and dañado=@dañado and (IdIndiceRotacion=@IdIndiceRotacion OR IdIndiceRotacion is null)"
                        End If
                    Else
                        vSQL = String.Format("SELECT * FROM bodega_Ubicacion WHERE IdUbicacion={0} and {1}=1 and IdBodega={2}", pIdUbicacion, nombreCampo, pIdBodega)

                        If nombreCampo = "ubicaciones" Then
                            vSQL = "SELECT * FROM bodega_Ubicacion WHERE IdUbicacion=" & pIdUbicacion & " and IdBodega=" & pIdBodega
                        End If

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@dañado", Int(dañado))
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdIndiceRotacion", idIndiceRotacion)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBeBodega_ubicacion()
                            Cargar_With_Tramo_And_Sector(Obj, lRow, lTransaction, lConnection)
                            Return Obj

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

    Public Shared Function Get_Ubicacion_Recepcion(ByVal IdUbicacion As Integer) As clsBeBodega_ubicacion

        Get_Ubicacion_Recepcion = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " SELECT * FROM Bodega_ubicacion " &
                                 " Where(IdUbicacion = @IdUbicacion AND ubicacion_recepcion = 1)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.Fill(dt)

            Dim Obj As New clsBeBodega_ubicacion()

            If dt.Rows.Count > 0 Then

                Dim lRow As DataRow = dt.Rows(0)
                Cargar(Obj, lRow, lTransaction, lConnection)

            End If

            Get_Ubicacion_Recepcion = Obj

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Ubicacion_Recepcion(ByVal IdUbicacion As Integer,
                                                   ByVal IdBodega As Integer,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As clsBeBodega_ubicacion

        Get_Ubicacion_Recepcion = Nothing

        Try

            Const sp As String = " SELECT * FROM Bodega_ubicacion " &
                                 " Where(IdUbicacion = @IdUbicacion AND IdBodega=@IdBodega AND ubicacion_recepcion = 1)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.Fill(dt)

            Dim Obj As New clsBeBodega_ubicacion()

            If dt.Rows.Count > 0 Then
                Dim lRow As DataRow = dt.Rows(0)
                Cargar(Obj, lRow, lTransaction, lConnection)
                Get_Ubicacion_Recepcion = Obj
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdInventarioEnc(ByVal pIdUbicacion As Integer,
                                                 ByVal pIdInventario As Integer,
                                                 ByVal pIdBodega As Integer) As clsBeBodega_ubicacion

        Get_Single_By_IdInventarioEnc = Nothing

        Try

            Dim vSQL As String = String.Format("SELECT bodega_ubicacion.IdUbicacion, bodega_ubicacion.descripcion
                                                  FROM bodega_ubicacion 
                                                  INNER JOIN trans_inv_ciclico_ubic 
                                                  ON bodega_ubicacion.IdUbicacion = trans_inv_ciclico_ubic.idubicacion
                                                  AND bodega_ubicacion.IdBodega = trans_inv_ciclico_ubic.IdBodega
                                                  WHERE trans_inv_ciclico_ubic.idubicacion=@IdUbicacion 
                                                  AND trans_inv_ciclico_ubic.idinventarioenc=@IdInventario
                                                  AND trans_inv_ciclico_ubic.IdBodega = @IdBodega")

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBeBodega_ubicacion

                            Obj.IdUbicacion = CType(lRow("IdUbicacion"), Int32)

                            If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                                Obj.Descripcion = CType(lRow("Descripcion"), String)
                            End If

                            Return Obj

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

    Public Shared Function GetSingleWithTramoAndSector(ByVal pIdUbicacion As Integer, ByVal pIdBodega As Integer) As clsBeBodega_ubicacion

        GetSingleWithTramoAndSector = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM bodega_ubicacion WHERE IdUbicacion=@IdUbicacion AND IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBeBodega_ubicacion()
                            Cargar(Obj, lRow)
                            Obj.Tramo.IdTramo = Obj.IdTramo
                            Obj.Tramo.IdBodega = Obj.IdBodega
                            clsLnBodega_tramo.GetSingle(Obj.Tramo, lConnection, lTransaction)
                            Obj.Sector = clsLnBodega_sector.Get_Single_By_IdTramo(Obj.IdTramo, Obj.IdBodega, lConnection, lTransaction)

                            Return Obj

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

    Public Shared Function Get_Single_With_Tramo_And_Sector(ByVal pIdUbicacion As Integer,
                                                       ByVal pIdBodega As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As clsBeBodega_ubicacion

        Get_Single_With_Tramo_And_Sector = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM bodega_ubicacion WHERE IdUbicacion=@IdUbicacion AND IdBodega=@IdBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Dim Obj As clsBeBodega_ubicacion

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Obj = New clsBeBodega_ubicacion()
                    Cargar(Obj, lRow, lTransaction, lConnection)
                    Obj.Tramo.IdTramo = Obj.IdTramo
                    Obj.Tramo.IdBodega = Obj.IdBodega

                    clsLnBodega_tramo.GetSingle(Obj.Tramo, lConnection, lTransaction)

                    Obj.Sector = clsLnBodega_sector.Get_Single_By_IdTramo(Obj.IdTramo, Obj.IdBodega, lConnection, lTransaction)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetVolumenUbicacionByIdUbicacion(ByVal pIdUbicacion As Integer, ByVal pIdBodega As Integer) As Double

        GetVolumenUbicacionByIdUbicacion = 0

        Try

            Dim lStockConLote As New List(Of clsBeVW_stock_res)
            'Dim BePresentacionProductoEnStock As New clsBeProducto_presentacion
            'Dim BeProductoInStock As New clsBeProducto

            lStockConLote = clsLnStock.Get_All_By_IdUbicacion(pIdUbicacion, pIdBodega)

            Dim vVolumenOcupadoUbicacion As Double = 0
            'Dim vCantidadStock As Double =0

            For Each ProdInStock As clsBeVW_stock_res In lStockConLote

                If ProdInStock.IdPresentacion <> 0 Then

                    '#EJC2017091018_REVISIÓN_CAMBIO_CANTIDAD_PRES_PENDREV?
                    vVolumenOcupadoUbicacion += ProdInStock.VolumenPresEnUbicacion()

                    'BePresentacionProductoEnStock.IdPresentacion = ProdInStock.IdPresentacion
                    'BePresentacionProductoEnStock = clsLnProducto_presentacion.GetSingle(ProdInStock.IdPresentacion)

                    'If BePresentacionProductoEnStock.

                    '    vVolumenOcupadoUbicacion += (ProdInStock.CantidadPresentacion * BePresentacionProductoEnStock.Volumen())
                    'Else
                    '    vCantidadStock= (ProdInStock.CantidadUmBas / BePresentacionProductoEnStock.Factor)
                    '    vVolumenOcupadoUbicacion += (vCantidadStock * BePresentacionProductoEnStock.Volumen())
                    'End If                    

                Else
                    'BeProductoInStock.IdProducto = ProdInStock.IdProducto
                    'BeProductoInStock = clsLnProducto.GetSingle(ProdInStock.IdProducto)
                    'vVolumenOcupadoUbicacion += (ProdInStock.CantidadPresentacion * BeProductoInStock.Volumen())
                    vVolumenOcupadoUbicacion += ProdInStock.VolumenUmBasEnUbicacion
                    '#EJC2017091018_REVISIÓN_CAMBIO_CANTIDAD_PRES_PENDREV?
                End If

            Next

            Return vVolumenOcupadoUbicacion

        Catch ex As Exception
            Throw ex
            Return 0
        End Try

    End Function

    Public Shared Function GetVolumenUbicacionByIdStock(ByVal pIdStock As Integer) As Double

        GetVolumenUbicacionByIdStock = 0

        Try

            Dim lStockConLote As New List(Of clsBeVW_stock_res)

            lStockConLote = clsLnStock.Get_All_By_IdStock(pIdStock)

            Dim vVolumenOcupadoUbicacion As Double = 0

            For Each ProdInStock As clsBeVW_stock_res In lStockConLote
                If ProdInStock.IdPresentacion <> 0 Then
                    vVolumenOcupadoUbicacion += ProdInStock.VolumenPresEnUbicacion()
                Else
                    vVolumenOcupadoUbicacion += ProdInStock.VolumenUmBasEnUbicacion()
                End If
            Next

            Return vVolumenOcupadoUbicacion

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Es_ValidaUbicacion_By_IdUbicacion(ByVal IdUbicacion As Integer) As String

        Es_ValidaUbicacion_By_IdUbicacion = ""

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = " SELECT descripcion FROM Bodega_ubicacion " &
                                 " Where(IdUbicacion = @IdUbicacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Es_ValidaUbicacion_By_IdUbicacion = dt.Rows(0).Item("Descripcion")
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#CKFK20180128 01:08AM: Agregué la función EsValidaUbicacionEstado para saber si una ubicación es válida para un estado de producto
    Public Shared Function EsValidaUbicacionEstado(ByVal IdUbicacion As Integer, ByVal IdEstado As Integer, ByVal IdBodega As Integer) As String

        EsValidaUbicacionEstado = ""

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = " SELECT u.descripcion
                                   FROM producto_estado AS p 
                                        INNER JOIN bodega_ubicacion AS u ON p.IdUbicacionDefecto = u.IdUbicacion 
                                        INNER JOIN bodega_tramo t ON t.IdTramo = u.IdTramo
	                                    INNER JOIN bodega_sector s ON s.IdSector = t.IdSector
	                                    INNER JOIN bodega_area a ON s.IdArea = a.IdArea
                                   WHERE(u.IdUbicacion = @IdUbicacion and p.IdEstado = @IdEstado and a.IdBodega = @IdBodega)
                                   UNION
                                   SELECT u.descripcion
                                   FROM producto_estado_ubic AS p 
                                         INNER JOIN bodega_ubicacion AS u ON p.IdUbicacionDefecto = u.IdUbicacion
	                                     INNER JOIN bodega_tramo t ON t.IdTramo = u.IdTramo
	                                     INNER JOIN bodega_sector s ON s.IdSector = t.IdSector
	                                     INNER JOIN bodega_area a ON s.IdArea = a.IdArea
                                   WHERE(u.IdUbicacion = @IdUbicacion and p.IdEstado = @IdEstado and a.IdBodega = @IdBodega) "


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEstado", IdEstado))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 0 Then
                EsValidaUbicacionEstado = IIf(IsDBNull(dt.Rows(0).Item("Descripcion")), "", dt.Rows(0).Item("Descripcion"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Ubicacion_Valida_By_IdUbicacion_And_IdEstado(ByVal IdUbicacion As Integer,
                                                                        ByVal IdEstado As Integer,
                                                                        ByVal IdBodega As Integer,
                                                                        ByRef pNombreUbicacion As String) As Boolean

        Ubicacion_Valida_By_IdUbicacion_And_IdEstado = False

        Try

            Const sp As String = " SELECT IdUbicacionBodegaDefecto, IdEstado, IdBodega, nombreubic as Descripcion FROM VW_Producto_Estado_Ubic_Bodega
                                       WHERE (IdUbicacionBodegaDefecto = @IdUbicacion and IdEstado = @IdEstado AND IdBodega = @IdBodega)
                                   UNION
                                       SELECT *
                                       FROM  VW_ProductoEstadoUbic
                                       WHERE(IdUbicacion = @IdUbicacion AND IdEstado = @IdEstado AND IdBodega = @IdBodega) "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEstado", IdEstado))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        pNombreUbicacion = IIf(IsDBNull(dt.Rows(0).Item("Descripcion")), "", dt.Rows(0).Item("Descripcion"))
                        Ubicacion_Valida_By_IdUbicacion_And_IdEstado = True
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

    Public Shared Function Get_Ubicacion_By_Codigo_Barra_And_IdBodega(ByVal pBarra As String,
                                                                      ByVal pIdBodega As Integer) As clsBeBodega_ubicacion

        Get_Ubicacion_By_Codigo_Barra_And_IdBodega = Nothing

        Try

            pBarra = pBarra.Trim()

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = ""

                    If IsNumeric(pBarra) Then

                        vSQL = "SELECT u.*, dbo.Nombre_Completo_Ubicacion_Barra (@IdUbicacion,@IdBodega) AS NOMBRE_COMPLETO
                                        FROM bodega_ubicacion u inner join bodega_tramo t on u.idtramo = t.idtramo and t.IdBodega = u.IdBodega
                                        WHERE (u.IdUbicacion=@IdUbicacion And u.idbodega = @IdBodega) And u.activo = 1 
                                        And ( u.bloqueada = 0 
								        OR (u.sistema = 1 AND bloqueada = 1)) "
                    Else

                        vSQL = "SELECT u.*, dbo.Nombre_Completo_Ubicacion_Barra(@IdUbicacion,@IdBodega) AS NOMBRE_COMPLETO
                                        FROM bodega_ubicacion u 
                                        inner join bodega_tramo t on u.idtramo = t.idtramo 
                                        and t.IdBodega = u.IdBodega 
                                        and t.IdArea = u.IdArea 
                                        and t.IdSector = u.IdSector 
                                        WHERE (u.codigo_barra=@codigo_barra OR u.codigo_barra2=@codigo_barra2) 
                                        And u.idbodega = @IdBodega 
                                        And u.activo = 1 
                                        And ( u.bloqueada = 0 
								        OR (u.sistema = 1 AND bloqueada = 1))  "

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo_barra", pBarra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo_barra2", pBarra)

                        If IsNumeric(pBarra) Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", Val(pBarra))
                        Else
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pBarra)
                        End If

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBeBodega_ubicacion()
                            Cargar(Obj, lRow, lTransaction, lConnection)
                            Obj.Descripcion = IIf(IsDBNull(lRow.Item("NOMBRE_COMPLETO")), "", lRow.Item("NOMBRE_COMPLETO"))
                            Obj.Disponibilidad_Ubicacion = clsLnVW_stock_res.Get_Disponibilidad_Ubicacion_By_IdBodega_And_IdUbicacion(pIdBodega,
                                                                                                                                      Obj.IdUbicacion,
                                                                                                                                      lConnection,
                                                                                                                                      lTransaction)

                            Get_Ubicacion_By_Codigo_Barra_And_IdBodega = Obj

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

    Public Shared Function GetSingle(ByVal IdUbicacion As Integer, ByVal IdBodega As Integer) As clsBeBodega_ubicacion

        'GetSingle = Nothing

        Dim pBeBodega_ubicacion As New clsBeBodega_ubicacion

        pBeBodega_ubicacion = Nothing

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion" &
            " Where(IdUbicacion = @IdUbicacion AND IdBodega=@IdBodega )"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                pBeBodega_ubicacion = New clsBeBodega_ubicacion
                Cargar(pBeBodega_ubicacion, dt.Rows(0))
            End If

            Return pBeBodega_ubicacion

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdUbicacion_And_IdBodega(ByVal IdUbicacion As Integer,
                                                                  ByVal IdBodega As Integer,
                                                                  ByVal lConnection As SqlConnection,
                                                                  ByVal lTransaction As SqlTransaction) As clsBeBodega_ubicacion

        Get_Single_By_IdUbicacion_And_IdBodega = Nothing

        Dim pBeBodega_ubicacion As New clsBeBodega_ubicacion

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion " &
            " Where(IdUbicacion = @IdUbicacion AND IdBodega=@IdBodega )"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                Get_Single_By_IdUbicacion_And_IdBodega = pBeBodega_ubicacion
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion" &
            " Where(IdUbicacion = @IdUbicacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                Obtener = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ObtenerWithTramo(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        ObtenerWithTramo = False

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion" &
            " Where(IdUbicacion = @IdUbicacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar_With_Tramo(oBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                ObtenerWithTramo = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_Completo_By_IdUbicacion(ByVal IdUbicacion As Integer,
                                                              ByVal IdBodega As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As String

        Get_Nombre_Completo_By_IdUbicacion = ""

        Try

            Const sp As String = " SELECT dbo.Nombre_Completo_Ubicacion(@IdUbicacion,  @IdBodega) AS NOMBRE_COMPLETO"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Nombre_Completo_By_IdUbicacion = IIf(IsDBNull(dt.Rows(0).Item("NOMBRE_COMPLETO")), "", dt.Rows(0).Item("NOMBRE_COMPLETO"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_Completo_By_IdUbicacion(ByVal IdUbicacion As Integer,
                                                              ByVal IdBodega As Integer) As String

        Get_Nombre_Completo_By_IdUbicacion = ""

        Try

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


            Const sp As String = " SELECT dbo.Nombre_Completo_Ubicacion (@IdUbicacion, @IdBodega) AS NOMBRE_COMPLETO"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Nombre_Completo_By_IdUbicacion = IIf(IsDBNull(dt.Rows(0).Item("NOMBRE_COMPLETO")), "", dt.Rows(0).Item("NOMBRE_COMPLETO"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function UbicacionVecina(oBeBodega_ubicacion As clsBeBodega_ubicacion, letra As String) As Integer
        Dim sp As String
        Dim idubic As Integer = 0

        Try

            sp = "SELECT idubicacion FROM Bodega_ubicacion 
                         WHERE (indice_x = @indice_x)
                         AND   (nivel = @nivel)
                         AND   (idtramo = @idtramo)
                         AND   (descripcion LIKE '%" & letra & "%')"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@indice_x", oBeBodega_ubicacion.Indice_x))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@nivel", oBeBodega_ubicacion.Nivel))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idtramo", oBeBodega_ubicacion.IdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@letra", letra))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then idubic = dt.Rows(0).Item(0)

            Return idubic

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#EJC20171213; Transaccionalidad.
    Public Shared Function GetSingle(ByRef pBeBodega_ubicacion As clsBeBodega_ubicacion,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion" &
            " Where(IdUbicacion = @IdUbicacion AND IdBodega=@IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", pBeBodega_ubicacion.IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeBodega_ubicacion.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                GetSingle = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeBodega_ubicacion As clsBeBodega_ubicacion,
                                     ByVal ParametrosTramoYSector As Boolean,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion" &
            " Where(IdUbicacion = @IdUbicacion  AND IdBodega=@IdBodega )"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", pBeBodega_ubicacion.IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeBodega_ubicacion.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                If ParametrosTramoYSector Then
                    Cargar_With_Tramo_And_Sector(pBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                Else
                    Cargar(pBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                End If

                GetSingle = True

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Bodega_Ubicacion(ByRef IdBodega As Integer,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            '#EJC20180817_0602PM: Quité dbo. de consulta, JP lo dejó con dbo.

            Dim sp As String = "DELETE FROM bodega_ubicacion WHERE bodega_ubicacion.Idtramo IN  
                               (SELECT bodega_tramo.IdTramo
                               FROM bodega_tramo INNER JOIN
                               bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector and bodega_tramo.idbodega = bodega_sector.idbodega INNER JOIN
                               bodega_area ON bodega_sector.IdArea = bodega_area.IdArea
                               WHERE (bodega_area.IdBodega = @idbodega)) AND bodega_ubicacion.IdBodega=@idbodega  "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idbodega", IdBodega))


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

    Public Shared Async Function Eliminar_Bodega_Ubicacion_Async(ByVal IdBodega As Integer,
                                                                 Optional ByVal pConection As SqlConnection = Nothing,
                                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As Task(Of Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            '#EJC20180817_0602PM: Quité dbo. de consulta, JP lo dejó con dbo.

            Dim sp As String = "DELETE FROM bodega_ubicacion WHERE bodega_ubicacion.Idtramo IN  
                               (SELECT bodega_tramo.IdTramo
                               FROM bodega_tramo INNER JOIN
                               bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector and bodega_tramo.idbodega = bodega_sector.idbodega INNER JOIN
                               bodega_area ON bodega_sector.IdArea = bodega_area.IdArea
                               WHERE (bodega_area.IdBodega = @idbodega)) AND bodega_ubicacion.IdBodega=@idbodega  "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                Await lConnection.OpenAsync() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idbodega", IdBodega))


            Dim rowsAffected As Task(Of Integer) = cmd.ExecuteNonQueryAsync()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected.Result

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Async Function Importar_Estructura_Async(ByVal IdBodega As Integer,
                                                           ByVal ListBeBodegaTramo As List(Of clsBeBodega_tramo),
                                                           ByVal ListBeBodegaUbicacion As List(Of clsBeBodega_ubicacion),
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As Task(Of Boolean)

        Dim lReturn As Boolean = False
        Dim ListUbicaciones_Por_Tramo As New List(Of clsBeBodega_ubicacion)
        Dim ttrm As clsBeBodega_tramo
        Dim BeBodegaUbic As New clsBeBodega_ubicacion
        Dim IdTramo, origidtramo, IdUbic As Integer

        Try

            Await clsLnBodega_tramo.Eliminar_Bodega_Tramo_Async(IdBodega, lConnection, lTransaction)

            IdTramo = Await clsLnBodega_tramo.MaxID_Async(IdBodega, lConnection, lTransaction)

            Await Eliminar_Bodega_Ubicacion_Async(IdBodega, lConnection, lTransaction)

            '#EJC20180817: Ordenar para que no se inserten con saltos en numeración los tramos
            'A futuro utilizar Indice X para el ordenamiento (para que sea numérico, creo que este campo no se utiliza actualmente
            ListBeBodegaTramo = ListBeBodegaTramo.OrderBy(Function(x) x.Descripcion).ThenBy(Function(x) x.Indice_x).ToList()

            '#CKFK 20180828 Cambio temporal para CLC
            IdUbic = MaxID(IdBodega, lConnection, lTransaction) '+ IIf(IdBodega = 1, 200, 0)

            For Each BeTramo As clsBeBodega_tramo In ListBeBodegaTramo

                origidtramo = BeTramo.IdTramo
                'tramo.IdTramo = idtramo

                ttrm = New clsBeBodega_tramo
                ttrm = BeTramo.Clone
                ttrm.IdTramo = IdTramo
                ttrm.Orden_Descendente = BeTramo.Orden_Descendente

                Await clsLnBodega_tramo.Insertar_Async(ttrm, lConnection, lTransaction)

                ListUbicaciones_Por_Tramo = New List(Of clsBeBodega_ubicacion)

                '#EJC20180817: Ordenar las ubicaciones para que no se inserten primero filas y despues columnas del rack                
                ListUbicaciones_Por_Tramo = ListBeBodegaUbicacion.FindAll(Function(x) x.IdTramo = origidtramo).OrderBy(Function(y) y.Indice_x).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Orientacion_pos).ToList

                For Each ubic As clsBeBodega_ubicacion In ListUbicaciones_Por_Tramo

                    BeBodegaUbic = New clsBeBodega_ubicacion
                    BeBodegaUbic = ubic.Clone()
                    BeBodegaUbic.IdUbicacion = IdUbic
                    BeBodegaUbic.IdTramo = IdTramo
                    BeBodegaUbic.Codigo_barra = Mid(100000 + IdUbic, 2, 5)
                    BeBodegaUbic.Codigo_barra2 = ubic.Descripcion
                    Await Insertar_Async(BeBodegaUbic, lConnection, lTransaction)

                    IdUbic += 1

                    Application.DoEvents()

                    Debug.Print("InsBodegaUbic: " & ubic.IdUbicacion)

                Next

                IdTramo += 1

            Next

            lReturn = True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return lReturn

    End Function

    Public Shared Function Importar_Estructura(ByVal IdBodega As Integer,
                                               ByVal ListBeBodegaTramo As List(Of clsBeBodega_tramo),
                                               ByVal ListBeBodegaUbicacion As List(Of clsBeBodega_ubicacion),
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As Boolean

        Dim lReturn As Boolean = False
        Dim ListUbicaciones_Por_Tramo As New List(Of clsBeBodega_ubicacion)
        Dim ttrm As clsBeBodega_tramo
        Dim BeBodegaUbic As New clsBeBodega_ubicacion
        Dim IdTramo, origidtramo, IdUbic As Integer

        Try

            clsLnBodega_tramo.Eliminar_Bodega_Tramo_By_IdBodega(IdBodega, lConnection, lTransaction)

            IdTramo = clsLnBodega_tramo.MaxID(IdBodega, lConnection, lTransaction)

            Eliminar_Bodega_Ubicacion(IdBodega, lConnection, lTransaction)

            '#EJC20180817: Ordenar para que no se inserten con saltos en numeración los tramos
            'A futuro utilizar Indice X para el ordenamiento (para que sea numérico, creo que este campo no se utiliza actualmente
            ListBeBodegaTramo = ListBeBodegaTramo.OrderBy(Function(x) x.Descripcion).ThenBy(Function(x) x.Indice_x).ToList()

            IdUbic = MaxID(IdBodega, lConnection, lTransaction)

            For Each BeTramo As clsBeBodega_tramo In ListBeBodegaTramo

                origidtramo = BeTramo.IdTramo

                ttrm = New clsBeBodega_tramo
                ttrm = BeTramo.Clone
                ttrm.IdTramo = IdTramo
                ttrm.Orden_Descendente = BeTramo.Orden_Descendente

                clsLnBodega_tramo.Insertar(ttrm, lConnection, lTransaction)

                ListUbicaciones_Por_Tramo = New List(Of clsBeBodega_ubicacion)

                '#EJC20180817: Ordenar las ubicaciones para que no se inserten primero filas y despues columnas del rack                
                ListUbicaciones_Por_Tramo = ListBeBodegaUbicacion.FindAll(Function(x) x.IdTramo = origidtramo).OrderBy(Function(y) y.Indice_x).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Orientacion_pos).ToList

                For Each ubic As clsBeBodega_ubicacion In ListUbicaciones_Por_Tramo

                    BeBodegaUbic = New clsBeBodega_ubicacion
                    BeBodegaUbic = ubic.Clone()
                    BeBodegaUbic.IdUbicacion = IdUbic
                    BeBodegaUbic.IdTramo = IdTramo
                    BeBodegaUbic.Codigo_barra = Mid(100000 + IdUbic, 2, 5)
                    BeBodegaUbic.Codigo_barra2 = ubic.Descripcion
                    Insertar(BeBodegaUbic, lConnection, lTransaction)

                    IdUbic += 1

                    Application.DoEvents()

                    Debug.Print("InsBodegaUbic: " & ubic.IdUbicacion)

                Next

                IdTramo += 1

            Next

            lReturn = True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return lReturn

    End Function

    Public Shared Sub Borrar_Estructura_Actual(idbodega As Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Eliminar_Bodega_Ubicacion(idbodega, lConnection, lTransaction)

            clsLnBodega_tramo.Eliminar_Bodega(idbodega, lConnection, lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try
    End Sub

    Public Shared Sub Borrar_Estructura_Actual(idbodega As Integer,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction)

        Try

            Eliminar_Bodega_Ubicacion(idbodega, lConnection, lTransaction)

            clsLnBodega_tramo.Eliminar_Bodega(idbodega, lConnection, lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try
    End Sub

    Public Shared Function Get_All_By_Bodega_By_IdInventarioEnc(ByVal pIdInventario As Integer) As List(Of clsBeBodega_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = String.Format("SELECT bodega_ubicacion.IdUbicacion, bodega_ubicacion.descripcion
                                       FROM  bodega_ubicacion INNER JOIN
                                            trans_inv_ciclico_ubic ON bodega_ubicacion.IdUbicacion = trans_inv_ciclico_ubic.idubicacion
                                       WHERE trans_inv_ciclico_ubic.idinventarioenc=@IdInventario")

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_ubicacion()

                                Obj.IdUbicacion = CType(lRow("IdUbicacion"), Int32)

                                If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                                    Obj.Descripcion = CType(lRow("Descripcion"), String)
                                End If

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

    Public Shared Function Get_All_By_IdUbicacion(ByVal pIdUbicacion As Integer) As List(Of clsBeBodega_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = String.Format("SELECT * from bodega_ubicacion 
                                       WHERE bodega_ubicacion.IdUbicacion=@IdUbicacion")

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_ubicacion()
                                Cargar(Obj, lRow, lTransaction, lConnection)
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

    Public Shared Function Listar(ByRef lTransaction As SqlTransaction,
                                  ByRef lConnection As SqlConnection) As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
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

    Public Shared Function MaxID(ByVal pIdBodega As Integer,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 1

            Dim sp As String = "SELECT ISNULL(Max(IdUbicacion),0) FROM Bodega_ubicacion 
                                WHERE IdBodega = @IdBodega "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

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

            Const sp As String = " Delete from bodega_ubicacion where IdBodega = @IdBodega"

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

    Public Shared Function Exists(ByVal ubicacion As String,
                                  ByVal IdBodega As Integer,
                                  ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = "select IdUbicacion from bodega_ubicacion 
                                where IdBodega=@IdBodega and "

            If IsNumeric(ubicacion) Then
                sp += "(IdUbicacion=@ubicacion or descripcion=@ubicacion or codigo_barra=@ubicacion)"
            Else
                sp += "(descripcion=@ubicacion Or codigo_barra=@ubicacion)"
            End If

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lCommand.Parameters.AddWithValue("@ubicacion", ubicacion)

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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Ubicacion_Recepcion_By_Codigo_Barra_And_IdBodega(ByVal pBarra As String,
                                                                               ByVal pIdBodega As Integer) As clsBeBodega_ubicacion

        Get_Ubicacion_Recepcion_By_Codigo_Barra_And_IdBodega = Nothing

        Try

            pBarra = pBarra.Trim()

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = ""

                    If IsNumeric(pBarra) Then

                        vSQL = "SELECT u.*, dbo.Nombre_Completo_Ubicacion_Barra (@IdUbicacion,@IdBodega) AS NOMBRE_COMPLETO
                                        FROM bodega_ubicacion u inner join bodega_tramo t on u.idtramo = t.idtramo and t.IdBodega = u.IdBodega
                                        WHERE (u.IdUbicacion=@IdUbicacion And u.idbodega = @IdBodega and u.ubicacion_recepcion = 1)"
                    Else

                        vSQL = "SELECT u.*, dbo.Nombre_Completo_Ubicacion_Barra(@IdUbicacion,@IdBodega) AS NOMBRE_COMPLETO
                                        FROM bodega_ubicacion u 
                                        inner join bodega_tramo t on u.idtramo = t.idtramo 
                                        and t.IdBodega = u.IdBodega 
                                        and t.IdArea = u.IdArea 
                                        and t.IdSector = u.IdSector 
                                        WHERE (u.codigo_barra=@codigo_barra OR u.codigo_barra2=@codigo_barra2 u.ubicacion_recepcion = 1) 
                                        And u.idbodega = @IdBodega"

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo_barra", pBarra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo_barra2", pBarra)

                        If IsNumeric(pBarra) Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", Val(pBarra))
                        Else
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pBarra)
                        End If

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim Obj As clsBeBodega_ubicacion

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBeBodega_ubicacion()
                            Cargar(Obj, lRow, lTransaction, lConnection)
                            Obj.Descripcion = IIf(IsDBNull(lRow.Item("NOMBRE_COMPLETO")), "", lRow.Item("NOMBRE_COMPLETO"))
                            Obj.Disponibilidad_Ubicacion = clsLnVW_stock_res.Get_Disponibilidad_Ubicacion_By_IdBodega_And_IdUbicacion(pIdBodega,
                                                                                                                                      Obj.IdUbicacion,
                                                                                                                                      lConnection,
                                                                                                                                      lTransaction)

                            Get_Ubicacion_Recepcion_By_Codigo_Barra_And_IdBodega = Obj

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

    Public Shared Function Get_All_Ubicaciones_Despacho_By_IdBodega_DT(ByVal pIdBodega As Integer) As DataTable

        Dim vSQL As String = ""

        Get_All_Ubicaciones_Despacho_By_IdBodega_DT = Nothing

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    If pIdBodega <> 0 Then

                        vSQL = "SELECT Area, Sector, Tramo, UbicacionCompleta as Nombre, 
                                IdUbicacion FROM VW_BodegaUbicacion 
                                WHERE IdBodega=@IdBodega 
                                And Activo =1 
                                And Bloqueada = 0                                 
                                And ubicacion_despacho = 1 "

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_Ubicaciones_Despacho_By_IdBodega_DT = lDataTable

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


    Public Shared Function Get_All_Ubicaciones_By_IdBodega_DT(ByVal pIdBodega As Integer) As DataTable

        Dim vSQL As String = ""

        Get_All_Ubicaciones_By_IdBodega_DT = Nothing

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    If pIdBodega <> 0 Then

                        vSQL = "SELECT * FROM VW_BodegaUbicacion 
                                WHERE IdBodega=@IdBodega 
                                And Activo =1 "

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_Ubicaciones_By_IdBodega_DT = lDataTable

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

    Public Shared Function Get_Single_By_IdUbicacion_And_IdBodega(ByVal IdUbicacion As Integer,
                                                                  ByVal IdBodega As Integer) As clsBeBodega_ubicacion

        Get_Single_By_IdUbicacion_And_IdBodega = Nothing

        Dim pBeBodega_ubicacion As New clsBeBodega_ubicacion

        Try

            Const sp As String = "SELECT *, dbo.Nombre_Completo_Ubicacion(@IdUbicacion, @IdBodega) as NombreCompleto FROM Bodega_ubicacion " &
                                 " WHERE(IdUbicacion = @IdUbicacion AND IdBodega=@IdBodega )"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        Cargar(pBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                        pBeBodega_ubicacion.Descripcion = IIf(IsDBNull(dt.Rows(0).Item("NombreCompleto")), "", dt.Rows(0).Item("NombreCompleto"))
                        Get_Single_By_IdUbicacion_And_IdBodega = pBeBodega_ubicacion
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

    'GT27062022_1100: para importar reabasto
    Public Shared Function Get_List_By_Tramo_And_IdBodega(ByVal IdTramo As Integer, ByVal IdBodega As Integer,
                                                          ByVal colIni As Integer, ByVal colFin As Integer,
                                                          ByVal NivelIni As Integer, ByVal NivelFin As Integer,
                                                          Optional ByVal pConection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeBodega_ubicacion)

        Get_List_By_Tramo_And_IdBodega = Nothing

        Dim pBeBodega_ubicacion As New clsBeBodega_ubicacion
        Dim lReturnList As New List(Of clsBeBodega_ubicacion)

        Try

            Dim vSQL As String = "SELECT * FROM Bodega_ubicacion 
                                   where(IdTramo = @IDTRAMO AND IdBodega=@IdBodega and 
                                   indice_x between @colIni and @colFin and nivel between @NIVELINI and @NIVELFIN) "

            Using lDTA As New SqlDataAdapter(vSQL, pConection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", IdTramo))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@colIni", colIni))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@colFin", colFin))

                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NIVELINI", NivelIni))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NIVELFIN", NivelFin))

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                lReturnList = New List(Of clsBeBodega_ubicacion)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    'GT27062022_1120: cargamos todas las ubicaciones que seran seteadas para uso de reabasto
                    For Each lRow As DataRow In lDT.Rows

                        pBeBodega_ubicacion = New clsBeBodega_ubicacion()
                        Cargar(pBeBodega_ubicacion, lRow, pTransaction, pConection)
                        lReturnList.Add(pBeBodega_ubicacion)
                    Next

                    Get_List_By_Tramo_And_IdBodega = lReturnList

                End If

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

    Public Shared Function Get_Ubicaciones_Vacias(ByVal pIdBodega As Integer) As DataTable

        Get_Ubicaciones_Vacias = Nothing

        Try

            Dim vSQL As String = "SELECT IdBodega, Ubicacion, Area
                                  FROM VW_OcupacionBodega
                                  WHERE IDSTOCK =0
                                  AND (IdBodega = @IdBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Get_Ubicaciones_Vacias = lDT

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

    Public Shared Function Get_Single_Ubicacion_No_Picking(ByVal IdBodega As Integer,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction,
                                                           Optional nivel As Integer = 2,
                                                           Optional indice As Integer = 1) As clsBeBodega_ubicacion

        Get_Single_Ubicacion_No_Picking = Nothing

        Dim pBeBodega_ubicacion As New clsBeBodega_ubicacion

        Try

            Const sp As String = "SELECT TOP(1) * FROM Bodega_ubicacion " &
            " Where(IdBodega = @IdBodega AND ubicacion_picking =0 AND Activo = 1 
                    and dañado = 0 and ubicacion_despacho = 0
                    and nivel = @nivel AND indice_x = @indice) ORDER BY NEWID() "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@nivel", nivel))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@indice", indice))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                Get_Single_Ubicacion_No_Picking = pBeBodega_ubicacion
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_Ubicacion_Picking(ByVal IdBodega As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction,
                                                        Optional nivel As Integer = 1,
                                                        Optional indice As Integer = 1) As clsBeBodega_ubicacion

        Get_Single_Ubicacion_Picking = Nothing

        Dim pBeBodega_ubicacion As New clsBeBodega_ubicacion

        Try

            Const sp As String = "SELECT TOP(1) * FROM Bodega_ubicacion 
                                  Where(IdBodega = @IdBodega AND ubicacion_picking =1 AND Activo = 1 
                                       and dañado = 0 and ubicacion_despacho = 0
                                       and (nivel = @nivel OR nivel = 0) AND (indice_x = @indice OR indice_x=0)) ORDER BY NEWID() "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@nivel", nivel))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@indice", indice))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                Get_Single_Ubicacion_Picking = pBeBodega_ubicacion
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Bodega_ERP(ByVal pIdUbicacion As Integer,
                                          ByVal pIdBodega As Integer) As String

        Try

            Dim lCodigo As String = ""

            Dim sp As String = "SELECT distinct a.Codigo
                                FROM bodega_ubicacion u INNER JOIN 
                                     bodega_area a ON u.IdArea = a.IdArea AND u.IdBodega = a.IdBodega
                                WHERE u.sistema = 1 AND 
                                      a.IdUbicacionRef = @IdUbicacion AND 
                                      a.IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lCodigo = lReturnValue
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lCodigo

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Ubicaciones_Muelle_By_IdBodega_DT(ByVal pIdBodega As Integer) As DataTable

        Dim vSQL As String = ""

        Get_All_Ubicaciones_Muelle_By_IdBodega_DT = Nothing

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    If pIdBodega <> 0 Then

                        vSQL = "SELECT Area, Sector, Tramo, UbicacionCompleta as Nombre, 
                                IdUbicacion FROM VW_BodegaUbicacion 
                                WHERE IdBodega=@IdBodega 
                                And Activo =1 
                                And Bloqueada = 0                                 
                                And ubicacion_muelle = 1 "

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_Ubicaciones_Muelle_By_IdBodega_DT = lDataTable

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

    Public Shared Function Get_Ubicacion_Picking(ByVal IdUbicacion As Integer,
                                                 ByVal pIdBodega As Integer) As clsBeBodega_ubicacion

        Get_Ubicacion_Picking = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " SELECT * FROM Bodega_ubicacion " &
                                 " Where(IdUbicacion = @IdUbicacion AND ubicacion_picking = 1 AND IdBodega = @IdBodega)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            dad.Fill(dt)

            Dim Obj As New clsBeBodega_ubicacion()

            If dt.Rows.Count > 0 Then

                Dim lRow As DataRow = dt.Rows(0)
                Cargar(Obj, lRow, lTransaction, lConnection)

            End If

            Get_Ubicacion_Picking = Obj

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Descripcion_IdUbicacion(ByVal IdUbicacion As Integer,
                                                       ByVal IdBodega As Integer) As String

        Get_Descripcion_IdUbicacion = ""

        Try

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


            Const sp As String = " SELECT descripcion AS NOMBRE_COMPLETO from bodega_ubicacion where Idbodega = @Idbodega AND IdUbicacion = @IdUbicacion "

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Descripcion_IdUbicacion = IIf(IsDBNull(dt.Rows(0).Item("NOMBRE_COMPLETO")), "", dt.Rows(0).Item("NOMBRE_COMPLETO"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Indice_Rotacion_Bodega(ByVal IdUbicacion As String,
                                                             ByVal IdIndiceRotacion As Integer,
                                                             ByVal IdUserMod As Integer,
                                                             Optional ByVal pConection As SqlConnection = Nothing,
                                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega_ubicacion")
            Upd.Add("IdIndiceRotacion", "@IdIndiceRotacion", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdUbicacion = @IdUbicacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", IdUserMod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))
            cmd.Parameters.Add(New SqlParameter("@IdIndiceRotacion", IdIndiceRotacion))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()

        End Try

    End Function

    Public Shared Function Existe_By_IdUbicacion_And_IdBodega(ByVal IdUbicacion As Integer,
                                                             ByVal IdBodega As Integer,
                                                             ByRef lConnection As SqlConnection,
                                                             ByRef lTransaction As SqlTransaction) As Boolean

        Existe_By_IdUbicacion_And_IdBodega = False

        Try

            Dim lMax As Integer = 0

            Dim sp As String = "select IdUbicacion from bodega_ubicacion 
                                where IdBodega=@IdBodega and IdUbicacion = @IdUbicacion"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lCommand.Parameters.AddWithValue("@IdUbicacion", IdUbicacion)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Existe_By_IdUbicacion_And_IdBodega = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Ubicaciones(ByVal pIdBodega As Integer,
                                           ByVal Ocupadas As Boolean,
                                           ByVal Todas As Boolean) As DataTable

        Get_Ubicaciones = Nothing

        Try

            Dim vSQL As String = "SELECT IdBodega, Ubicacion, Area, Case when Max(IdStock)>0 then 'Ocupada' Else 'Vacia' End 'Estado'
                                  FROM VW_OcupacionBodega
                                  WHERE  (IdBodega = @IdBodega)"

            If Not Todas Then
                If Ocupadas Then
                    vSQL += " AND IDSTOCK > 0 "
                Else
                    vSQL += " AND IDSTOCK = 0 "
                End If
            End If

            vSQL += " GROUP BY IdBodega, Ubicacion, Area"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Get_Ubicaciones = lDT

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Ubicaciones_Detallado(ByVal pIdBodega As Integer) As DataTable

        Get_Ubicaciones_Detallado = Nothing

        Try

            Dim vSQL As String = "SELECT IdUbicacion, Ubicacion, Código, Nombre, Lote, Fecha, Lic_Plate, Cantidad, UM
                                  FROM VW_Ocupacion_Bodega_Detallado
                                  WHERE  (IdBodega = @IdBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Get_Ubicaciones_Detallado = lDT

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#MA20260105 
    Public Shared Function Get_Ubicaciones_No_Contadas_DT(IdInventario As Integer,
                                                          IdBodega As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As DataTable

        Dim DT As New DataTable

        Dim SQL As String = "SELECT DISTINCT
                             u.IdUbicacion, u.IdBodega,
                             dbo.Nombre_Completo_Ubicacion(u.IdUbicacion, u.IdBodega) AS Ubicacion,
                                a.Descripcion AS Area,
                                s.Descripcion AS Sector,
                                t.Descripcion AS Tramo
                            FROM bodega_ubicacion u
                            INNER JOIN bodega_tramo t ON u.IdTramo = t.IdTramo AND u.IdBodega = t.IdBodega
                            INNER JOIN bodega_sector s ON t.IdSector = s.IdSector AND u.IdBodega = s.IdBodega
                            INNER JOIN bodega_area a ON s.IdArea = a.IdArea AND u.IdBodega = a.IdBodega
                            WHERE u.IdBodega = @IdBodega
						    AND
							Exists(Select * 
							from trans_inv_tramo tit
							where IdInventario = @IdInventario AND tit.idtramo = u.IdTramo And u.IdBodega = tit.IdBodega)
							AND u.IdUbicacion NOT IN (
							Select Distinct IdUbicacion 
							from trans_inv_detalle
							where idinventarioenc = @IdInventario)
                            ORDER BY Area, Sector, Tramo, Ubicacion"

        Using da As New SqlDataAdapter(SQL, lConnection)
            da.SelectCommand.Transaction = lTransaction
            da.SelectCommand.CommandType = CommandType.Text

            da.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
            da.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

            da.Fill(DT)
        End Using
        Return DT
    End Function

    '#MA20260105
    Public Shared Function Get_Total_Ubicaciones_Asig(IdInventarioEnc As Integer,
                                                      IdBodega As Integer,
                                                      lConnection As SqlConnection,
                                                      lTransaction As SqlTransaction) As Integer

        Dim cmd As New SqlCommand()
        Dim Total As Integer = 0

        Try
            cmd.Connection = lConnection
            cmd.Transaction = lTransaction
            cmd.CommandType = CommandType.Text

            cmd.CommandText = "SELECT COUNT(*) 
                               FROM bodega_ubicacion u
                               INNER JOIN trans_inv_tramo tt ON u.IdTramo = tt.IdTramo
                               WHERE tt.IdInventario = @IdInventarioEnc
                               AND u.IdBodega = @IdBodega"

            cmd.Parameters.AddWithValue("@IdInventarioEnc", IdInventarioEnc)
            cmd.Parameters.AddWithValue("@IdBodega", IdBodega)

            Total = Convert.ToInt32(cmd.ExecuteScalar())

        Catch ex As Exception
            Throw
        End Try

        Return Total

    End Function

    '#MA20260105
    Public Shared Function Get_Ubicaciones_Contadas(IdInventario As Integer,
                                                    IdBodega As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As Integer
        Dim Total As Integer = 0

        Try
            If lConnection.State <> ConnectionState.Open Then
                lConnection.Open()
            End If

            Dim SQL As String = "SELECT COUNT(DISTINCT u.IdUbicacion)
                                 FROM bodega_ubicacion u
                                 INNER JOIN trans_inv_tramo tt ON u.IdTramo = tt.IdTramo
                                 WHERE tt.IdInventario = @IdInventario
                                  AND u.IdBodega = @IdBodega
                                  AND EXISTS (
                                        SELECT 1
                                        FROM trans_inv_detalle d
                                        WHERE d.IdUbicacion = u.IdUbicacion
                                          AND d.IdInventarioEnc = @IdInventario
                                      )"

            Using cmd As New SqlCommand(SQL, lConnection, lTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@IdInventario", IdInventario)
                cmd.Parameters.AddWithValue("@IdBodega", IdBodega)
                Total = Convert.ToInt32(cmd.ExecuteScalar())
            End Using

        Catch
            Throw
        End Try

        Return Total
    End Function

End Class
