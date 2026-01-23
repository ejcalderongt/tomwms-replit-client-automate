Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_presentacion
    Implements IDisposable

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

        Get_All_Filtro = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_Presentacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoPresentacion WHERE 1 > 0 "

                    If pActivo Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeProductoPresentacion As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeProductoPresentacion = New clsBeProducto_Presentacion

                                BeProductoPresentacion.IdPresentacion = CType(lRow("IdPresentacion"), Int32)

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    BeProductoPresentacion.IdProducto = CType(lRow("IdProducto"), Int32)
                                End If

                                If lRow("Presentación") IsNot DBNull.Value AndAlso lRow("Presentación") IsNot Nothing Then
                                    BeProductoPresentacion.Nombre = CType(lRow("Presentación"), String)
                                End If

                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    BeProductoPresentacion.Activo = CType(lRow("activo"), Boolean)
                                End If

                                If lRow("Imprime Barra") IsNot DBNull.Value AndAlso lRow("Imprime Barra") IsNot Nothing Then
                                    BeProductoPresentacion.Imprime_barra = CType(lRow("Imprime Barra"), Boolean)
                                End If
                                If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                                    BeProductoPresentacion.Peso = CType(lRow("Peso"), Double)
                                End If
                                If lRow("Alto") IsNot DBNull.Value AndAlso lRow("Alto") IsNot Nothing Then
                                    BeProductoPresentacion.Alto = CType(lRow("Alto"), Double)
                                End If
                                If lRow("Largo") IsNot DBNull.Value AndAlso lRow("Largo") IsNot Nothing Then
                                    BeProductoPresentacion.Largo = CType(lRow("Largo"), Double)
                                End If
                                If lRow("Ancho") IsNot DBNull.Value AndAlso lRow("Ancho") IsNot Nothing Then
                                    BeProductoPresentacion.Ancho = CType(lRow("Ancho"), Double)
                                End If
                                If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                                    BeProductoPresentacion.Factor = CType(lRow("Factor"), Double)
                                End If

                                lReturnList.Add(BeProductoPresentacion)

                            Next

                            Return lReturnList

                        End If

                    End Using


                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("LnProductoPresentacion_GetAllFiltro " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Presentaciones_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

        Get_All_Presentaciones_By_IdProducto = New List(Of clsBeProducto_Presentacion)

        Try

            Dim lReturnList As New List(Of clsBeProducto_Presentacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTRansaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoPresentacion WHERE IdProducto=@IdProducto"

                    If pActivo Then
                        vSQL += " AND activo=1 "
                    Else
                        vSQL += " AND activo=0 "
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTRansaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Presentacion
                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)
                                Obj = GetSingle(Obj.IdPresentacion, lConnection, lTRansaction)
                                lReturnList.Add(Obj)

                            Next

                            Get_All_Presentaciones_By_IdProducto = lReturnList

                        End If

                    End Using

                    lTRansaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Presentaciones_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                                      ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

        Get_All_Presentaciones_By_IdProductoBodega = New List(Of clsBeProducto_Presentacion)

        Try

            Dim lReturnList As New List(Of clsBeProducto_Presentacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTRansaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoPresentacion WHERE IdProductoBodega=@IdProductoBodega"

                    If pActivo Then
                        vSQL += " AND activo=1 "
                    Else
                        vSQL += " AND activo=0 "
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTRansaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Presentacion
                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)
                                Obj = GetSingle(Obj.IdPresentacion, lConnection, lTRansaction)
                                lReturnList.Add(Obj.Clone())

                            Next

                            Get_All_Presentaciones_By_IdProductoBodega = lReturnList

                        End If

                    End Using

                    lTRansaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer,
                                                 ByVal pActivo As Boolean,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)


        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoPresentacion WHERE IdProducto=@IdProducto"

            If pActivo Then
                vSQL += " AND activo=1 "
            Else
                vSQL += " AND activo=0 "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_Presentacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_Presentacion
                        Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        Obj = GetSingle(Obj.IdPresentacion, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto_By_IdBodega(ByVal pIdProducto As Integer,
                                                             ByVal pActivo As Boolean,
                                                             ByVal pIdBodega As Integer,
                                                             ByRef lConnection As SqlConnection,
                                                             ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)


        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoPresentacion WHERE IdProducto=@IdProducto AND IdBodega = @IdBodega"

            If pActivo Then
                vSQL += " AND activo=1 "
            Else
                vSQL += " AND activo=0 "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_Presentacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_Presentacion
                        Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        Obj = GetSingle(Obj.IdPresentacion, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * FROM VW_ProductoPresentacion 
                                  WHERE IdProducto=@IdProducto 
                                  AND IdBodega = @IdBodega "

            If pActivo Then
                vSQL += " AND activo=1 "
            Else
                vSQL += " AND activo=0 "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_Presentacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_Presentacion
                        Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        Obj = GetSingle(Obj.IdPresentacion, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto_For_HH(ByVal pIdProducto As Integer) As List(Of clsBeProducto_Presentacion)

        Get_All_By_IdProducto_For_HH = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_Presentacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdProducto=@IdProducto AND activo=1 "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Presentacion
                                CargarHH(Obj, lRow, lConnection, lTransaction)
                                lReturnList.Add(Obj)

                            Next

                            Get_All_By_IdProducto_For_HH = lReturnList

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

    Public Shared Function Get_All_By_IdProducto_For_HH(ByVal pIdProducto As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try


            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdProducto=@IdProducto AND activo=1 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_Presentacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_Presentacion
                        CargarHH(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto_HH(ByVal pIdProducto As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdProducto=@IdProducto AND activo=1 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_Presentacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_Presentacion
                        CargarHH(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub CargarHH(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion,
                               ByRef dr As DataRow)

        Try

            With oBeProducto_presentacion

                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Imprime_barra = IIf(IsDBNull(dr.Item("imprime_barra")), False, dr.Item("imprime_barra"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                .MinimoExistencia = IIf(IsDBNull(dr.Item("MinimoExistencia")), 0.0, dr.Item("MinimoExistencia"))
                .MaximoExistencia = IIf(IsDBNull(dr.Item("MaximoExistencia")), 0.0, dr.Item("MaximoExistencia"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .EsPallet = IIf(IsDBNull(dr.Item("EsPallet")), False, dr.Item("EsPallet"))
                .Precio = IIf(IsDBNull(dr.Item("Precio")), 0.0, dr.Item("Precio"))
                .MinimoPeso = IIf(IsDBNull(dr.Item("MinimoPeso")), 0.0, dr.Item("MinimoPeso"))
                .MaximoPeso = IIf(IsDBNull(dr.Item("MaximoPeso")), 0.0, dr.Item("MaximoPeso"))
                .MedidasPorTarima = clsLnProducto_presentacion_tarima.GetAllByIdPresentacion(.IdPresentacion)
                .RellenadoPorUbicacionDePicking = clsLnProducto_rellenado.GetAllByPresentacion(.IdPresentacion, True)
                .CajasPorCama = IIf(IsDBNull(dr.Item("CajasPorCama")), 0, dr.Item("CajasPorCama"))
                .CamasPorTarima = IIf(IsDBNull(dr.Item("CamasPorTarima")), 0, dr.Item("CamasPorTarima"))
                .Genera_lp_auto = IIf(IsDBNull(dr.Item("Genera_lp_auto")), False, dr.Item("Genera_lp_auto"))
                .Permitir_paletizar = IIf(IsDBNull(dr.Item("Permitir_paletizar")), False, dr.Item("Permitir_paletizar"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("Codigo_barra")), False, dr.Item("Codigo_barra")) '#CKFK 20180403 09:48 AM Agregué el código de barra de la presentacion

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub CargarHH(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion, ByRef dr As DataRow,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction)

        Try

            With oBeProducto_presentacion

                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Imprime_barra = IIf(IsDBNull(dr.Item("imprime_barra")), False, dr.Item("imprime_barra"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                .MinimoExistencia = IIf(IsDBNull(dr.Item("MinimoExistencia")), 0.0, dr.Item("MinimoExistencia"))
                .MaximoExistencia = IIf(IsDBNull(dr.Item("MaximoExistencia")), 0.0, dr.Item("MaximoExistencia"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .EsPallet = IIf(IsDBNull(dr.Item("EsPallet")), False, dr.Item("EsPallet"))
                .Precio = IIf(IsDBNull(dr.Item("Precio")), 0.0, dr.Item("Precio"))
                .MinimoPeso = IIf(IsDBNull(dr.Item("MinimoPeso")), 0.0, dr.Item("MinimoPeso"))
                .MaximoPeso = IIf(IsDBNull(dr.Item("MaximoPeso")), 0.0, dr.Item("MaximoPeso"))
                .MedidasPorTarima = clsLnProducto_presentacion_tarima.Get_All_By_IdPresentacion(.IdPresentacion, lConnection, lTransaction)
                .RellenadoPorUbicacionDePicking = clsLnProducto_rellenado.Get_All_By_IdPresentacion(.IdPresentacion, True, lConnection, lTransaction)
                .CajasPorCama = IIf(IsDBNull(dr.Item("CajasPorCama")), 0, dr.Item("CajasPorCama"))
                .CamasPorTarima = IIf(IsDBNull(dr.Item("CamasPorTarima")), 0, dr.Item("CamasPorTarima"))
                .Genera_lp_auto = IIf(IsDBNull(dr.Item("Genera_lp_auto")), False, dr.Item("Genera_lp_auto"))
                .Permitir_paletizar = IIf(IsDBNull(dr.Item("Permitir_paletizar")), False, dr.Item("Permitir_paletizar"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CM20172310_0513PM: Corrección de String.Format en Producto_presentacion
    ''' <summary>
    ''' Obtiene las presentaciones disponibles en stock
    ''' </summary>
    ''' <param name="pIdProductoBodega">IdProducto</param>
    ''' <returns>Lista de presentaciones clsBeProducto_presentacion</returns>
    ''' <remarks>ejcalderon_20160516</remarks>
    Public Shared Function Get_All_Stock_Con_Presentacion_By_IdProducto(ByVal pIdProductoBodega As Integer) As List(Of clsBeProducto_Presentacion)

        Get_All_Stock_Con_Presentacion_By_IdProducto = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_Presentacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT distinct * FROM VW_StockPresentaciones WHERE IdProductoBodega=@IdProductoBodega ORDER BY [Nombre]"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Presentacion

                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)

                                If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                    Obj.Nombre = CType(lRow("Nombre"), String)
                                End If

                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("activo"), Boolean)
                                End If

                                If lRow("Imprime_Barra") IsNot DBNull.Value AndAlso lRow("Imprime_Barra") IsNot Nothing Then
                                    Obj.Imprime_barra = CType(lRow("Imprime_Barra"), Boolean)
                                End If

                                If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("Peso"), Double)
                                End If

                                If lRow("Alto") IsNot DBNull.Value AndAlso lRow("Alto") IsNot Nothing Then
                                    Obj.Alto = CType(lRow("Alto"), Double)
                                End If

                                If lRow("Largo") IsNot DBNull.Value AndAlso lRow("Largo") IsNot Nothing Then
                                    Obj.Largo = CType(lRow("Largo"), Double)
                                End If

                                If lRow("Ancho") IsNot DBNull.Value AndAlso lRow("Ancho") IsNot Nothing Then
                                    Obj.Ancho = CType(lRow("Ancho"), Double)
                                End If

                                If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                                    Obj.Factor = CType(lRow("Factor"), Double)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            'Get_All_Stock_Con_Presentacion_By_IdProducto = lReturnList

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

    '·CKFK 20211221 Modificacion de la vista
    '#CM20172310_0513PM: Corrección de String.Format en Producto_presentacion
    ''' <summary>
    ''' Obtiene las presentaciones disponibles en stock
    ''' </summary>
    ''' <param name="pIdProductoBodega">IdProducto</param>
    ''' <returns>Lista de presentaciones clsBeProducto_presentacion</returns>
    ''' <remarks>ejcalderon_20160516</remarks>
    Public Shared Function Get_All_Presentacion_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As List(Of clsBeProducto_Presentacion)

        Get_All_Presentacion_By_IdProductoBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_Presentacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT distinct IdPresentacion, nombre, IdProductoBodega, IdPropietarioBodega, IdPropietario, 
                                          IdProducto, codigo_barra, imprime_barra, peso, alto, largo, ancho, factor, MinimoExistencia, 
                                          MaximoExistencia, user_agr, fec_agr, user_mod, fec_mod, activo,  IdBodega 
                                          FROM VW_StockPresentaciones WHERE IdProductoBodega=@IdProductoBodega ORDER BY [Nombre]"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Presentacion

                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)

                                If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                    Obj.Nombre = CType(lRow("Nombre"), String)
                                End If

                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("activo"), Boolean)
                                End If

                                If lRow("Imprime_Barra") IsNot DBNull.Value AndAlso lRow("Imprime_Barra") IsNot Nothing Then
                                    Obj.Imprime_barra = CType(lRow("Imprime_Barra"), Boolean)
                                End If

                                If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("Peso"), Double)
                                End If

                                If lRow("Alto") IsNot DBNull.Value AndAlso lRow("Alto") IsNot Nothing Then
                                    Obj.Alto = CType(lRow("Alto"), Double)
                                End If

                                If lRow("Largo") IsNot DBNull.Value AndAlso lRow("Largo") IsNot Nothing Then
                                    Obj.Largo = CType(lRow("Largo"), Double)
                                End If

                                If lRow("Ancho") IsNot DBNull.Value AndAlso lRow("Ancho") IsNot Nothing Then
                                    Obj.Ancho = CType(lRow("Ancho"), Double)
                                End If

                                If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                                    Obj.Factor = CType(lRow("Factor"), Double)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            'Get_All_Stock_Con_Presentacion_By_IdProducto = lReturnList

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

    Public Shared Function GetSingle(ByVal pIdPresentacion As Integer) As clsBeProducto_Presentacion

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdPresentacion=@IdPresentacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Dim Obj As New clsBeProducto_Presentacion()
                            Cargar(Obj, lRow)
                            GetSingle = Obj

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

    Public Shared Function GetSinglew(ByVal pIdPresentacion As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        GetSinglew = Nothing

        Try


            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdPresentacion=@IdPresentacion"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdPresentacion As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdPresentacion=@IdPresentacion"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeProducto_presentacion As clsBeProducto_Presentacion, ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Producto_presentacion 
                                  Where(IdPresentacion = @IdPresentacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACION", pBeProducto_presentacion.IdPresentacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                pBeProducto_presentacion = New clsBeProducto_Presentacion()
                Cargar(pBeProducto_presentacion, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeProductoPresentacion_By_IdPresentacion(ByVal pIdPresentacion As Integer) As clsBeProducto_Presentacion

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdPresentacion=@IdPresentacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Dim Obj As New clsBeProducto_Presentacion()

                            Cargar(Obj, lRow)

                            If lRow("factor") IsNot DBNull.Value AndAlso lRow("factor") IsNot Nothing Then
                                Obj.Factor = CType(lRow("factor"), Double)
                            End If

                            Return Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeProductoPresentacion_By_IdPresentacion_And_IdProducto(ByVal pIdPresentacion As Integer,
                                                                                       ByVal pIdProducto As Integer,
                                                                                       ByVal lConnection As SqlConnection,
                                                                                       ByVal lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_BeProductoPresentacion_By_IdPresentacion_And_IdProducto = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion 
                                  WHERE IdPresentacion=@IdPresentacion 
                                  AND IdProducto = @IdProducto"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)

                    Get_BeProductoPresentacion_By_IdPresentacion_And_IdProducto = Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Delete(ByVal pIdProducto As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim sp As String = String.Format("DELETE FROM producto_presentacion WHERE IdProducto={0}", pIdProducto)

                Using lCommand As New SqlCommand(sp, lConnection)

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

    Public Shared Function Exists(ByVal pIdPresentacion As Integer) As Boolean

        Exists = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT COUNT(IdPresentacion) FROM producto_presentacion WHERE IdPresentacion=@IdPresentacion"

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Exists = CInt(lReturnValue) > 0
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

    Public Shared Function Existe_Stock(ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As Boolean

        Existe_Stock = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT COUNT(1) FROM stock AS s " _
                                                         & "INNER JOIN producto_bodega AS pb ON s.IdProductoBodega = pb.IdProductoBodega " _
                                                         & "INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto " _
                                                         & "INNER JOIN producto_presentacion AS pp ON s.IdPresentacion = pp.IdPresentacion " _
                                                         & "AND p.IdProducto = pp.IdProducto " _
                                                         & "WHERE p.IdProducto =@IdProducto AND s.IdPresentacion=@IdPresentacion"

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Existe_Stock = CInt(lReturnValue) > 0
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

    Public Shared Function Existe_Stock(ByVal pIdProducto As Integer,
                                        ByVal pIdPresentacion As Integer,
                                        ByRef lConnection As SqlConnection,
                                        ByRef lTransaction As SqlTransaction) As Boolean

        Dim lExists As Boolean = False

        Dim vSQL As String = "SELECT COUNT(1) FROM stock AS s 
                INNER JOIN producto_bodega AS pb ON s.IdProductoBodega = pb.IdProductoBodega
                INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto 
                INNER JOIN producto_presentacion AS pp ON s.IdPresentacion = pp.IdPresentacion 
                AND p.IdProducto = pp.IdProducto 
                WHERE p.IdProducto =@IdProducto AND s.IdPresentacion=@IdPresentacion "
        Try

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Existe_Presentacion_By_Codigo_Barra(ByVal pCodigoBarra As String) As Boolean

        Existe_Presentacion_By_Codigo_Barra = False

        Dim vSQL As String = "SELECT COUNT(IdPresentacion) FROM producto_presentacion WHERE codigo_barra=@codigo_barra"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@codigo_barra", pCodigoBarra)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Existe_Presentacion_By_Codigo_Barra = CInt(lReturnValue) > 0
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

    Public Shared Function Existe_Presentacion_By_Codigo_Barra(ByVal pCodigoBarra As String,
                                                               ByRef pPresentacion As clsBeProducto_Presentacion) As Boolean

        Existe_Presentacion_By_Codigo_Barra = False

        pPresentacion = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE codigo_barra=@codigo_barra"

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@codigo_barra", pCodigoBarra)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            pPresentacion = New clsBeProducto_Presentacion()
                            Cargar(pPresentacion, lRow)
                            Existe_Presentacion_By_Codigo_Barra = True

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

    Public Shared Function Existe_Presentacion_By_Codigo_And_Nombre(ByVal pCodigo As String,
                                                                    ByVal pNombre As String,
                                                                    ByVal lConnection As SqlConnection,
                                                                    ByVal lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Existe_Presentacion_By_Codigo_And_Nombre = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE codigo_barra =@codigo_barra AND nombre=@nombre"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@codigo_barra", pCodigo.Trim())
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@nombre", pNombre.Trim())

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Presentacion_By_Nombre(ByVal pIdProducto As Integer, ByVal pNombre As String) As clsBeProducto_Presentacion

        Existe_Presentacion_By_Nombre = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE nombre=@pNombre and idproducto = @IdProducto"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@codigo_barra", pNombre.Trim())

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Dim Obj As New clsBeProducto_Presentacion()

                            Cargar(Obj, lRow)

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
    ''' #EJC20220630: Se busca la presentación por código de presentación no por código_barra
    ''' </summary>
    ''' <param name="pIdProducto"></param>
    ''' <param name="pCodigo"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Existe_Presentacion_By_Codigo(ByVal pIdProducto As Integer,
                                                         ByVal pCodigo As String,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Existe_Presentacion_By_Codigo = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion 
                              WHERE (IdProducto = @pIdProducto AND codigo=@pCodigo)"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pCodigo", pCodigo.Trim())

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_IdProducto_And_NombrePresentacion(ByVal pIdProducto As Integer,
                                                                       ByVal pNombre As String,
                                                                       ByRef lConnection As SqlConnection,
                                                                       ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Existe_By_IdProducto_And_NombrePresentacion = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion 
                              WHERE IdProducto = @IdProducto AND nombre=@pNombre"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pNombre", clsPublic.Quitar_Caracteres_No_Permitidos(pNombre.Trim()))

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Presentacion_By_Nombre(ByVal pNombre As String,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_Presentacion_By_Nombre = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE nombre=@pNombre"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pNombre", pNombre.Trim())

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try

            Dim vSQL As String = String.Format("SELECT * FROM producto_presentacion WHERE activo = 1 AND IdProducto={0}", pIdProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_Presentacion

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_Presentacion
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As DataTable

        Get_All_By_IdProductoBodega = Nothing

        Try

            Dim vSQL As String = String.Format("SELECT pp.IdPresentacion, 
                                                codigo_barra as Codigo, Nombre as Nombre FROM producto_presentacion pp
                                                INNER JOIN producto_bodega pb
                                                ON pp.IdProducto = pb.IdProducto
                                                WHERE pp.activo = 1 AND pb.IdProductoBodega={0}
                                                GROUP BY pb.IdProductoBodega, pp.IdPresentacion, pp.codigo_barra, PP.nombre", pIdProductoBodega)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_All_By_IdProductoBodega = lDataTable
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

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_All_By_IdBodega = Nothing

        Try

            Dim vSQL As String = String.Format("SELECT pp.IdPresentacion, 
                                                codigo_barra as Codigo, Nombre as Nombre FROM producto_presentacion pp
                                                INNER JOIN producto_bodega pb
                                                ON pp.IdProducto = pb.IdProducto
                                                WHERE pp.activo = 1 AND pb.IdBodega={0}
                                                GROUP BY pb.IdProductoBodega, pp.IdPresentacion, pp.codigo_barra, PP.nombre", pIdBodega)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_All_By_IdBodega = lDataTable
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

    Public Shared Function Get_IdPresentacion_By_Codigo_Barra(ByVal pCodigoBarraPresentacion As String) As Integer

        Get_IdPresentacion_By_Codigo_Barra = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdPresentacion FROM producto_presentacion WHERE activo = 1 AND Codigo_Barra=@pCodigoBarra"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pCodigoBarra", pCodigoBarraPresentacion)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_IdPresentacion_By_Codigo_Barra = lDataTable.Rows(0).Item("IdPresentacion")

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

    Public Shared Function Get_Peso_By_IdPresentacion(ByVal pIdPresentacion As Integer) As Double

        Try

            Dim lPeso As Double = 0.0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(peso),0) FROM producto_presentacion WHERE idPresentacion={0}", pIdPresentacion), lConnection)

                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lPeso = CDbl(lReturnValue)
                    End If

                End Using

            End Using

            Return lPeso

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdPresentacion),0) FROM producto_presentacion"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GuardaPresentacion(ByVal pInsert As Boolean,
                                              ByVal ObjPR As clsBeProducto_Presentacion,
                                              ByVal pCodigoBarraAnterior As String) As Object

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GuardaPresentacion = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pInsert Then
                Insertar(ObjPR, lConnection, lTransaction)
            Else
                ActualizarExistente(ObjPR, pCodigoBarraAnterior, lConnection, lTransaction)
            End If

            lTransaction.Commit()

            GuardaPresentacion = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' Actualiza Minimos y Maximos cuando la presentacion del producto aun tiene stock
    ''' </summary>
    ''' <param name="pObjP"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateMM(ByVal pObjP As clsBeProducto_Presentacion)

        Dim vSQL As String = "UPDATE producto_presentacion SET MinimoExistencia=@MinimoExistencia,MaximoExistencia=@MaximoExistencia,MinimoPeso=@MinimoPeso,MaximoPeso=@MaximoPeso WHERE IdPresentacion=@IdPresentacion"

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Using lCommand As New SqlCommand(vSQL, lConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdPresentacion", pObjP.IdPresentacion)
                lCommand.Parameters.AddWithValue("@MinimoExistencia", pObjP.MinimoExistencia)
                lCommand.Parameters.AddWithValue("@MaximoExistencia", pObjP.MaximoExistencia)
                lCommand.Parameters.AddWithValue("@MinimoPeso", pObjP.MinimoPeso)
                lCommand.Parameters.AddWithValue("@MaximoPeso", pObjP.MaximoPeso)

                lConnection.Open()

                lCommand.ExecuteNonQuery()

                lConnection.Close()

            End Using

        End Using

    End Sub

    Public Shared Function ActualizarExistente(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion,
                                               ByVal pCodigoBarraAnterior As String,
                                               Optional ByVal pConection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("producto_presentacion")
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("espallet", "@espallet", DataType.Parametro)
            Upd.Add("imprime_barra", "@imprime_barra", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("factor", "@factor", DataType.Parametro)
            Upd.Add("CamasPorTarima", "@CamasPorTarima", DataType.Parametro)
            Upd.Add("CajasPorCama", "@CajasPorCama", DataType.Parametro)
            Upd.Add("MinimoExistencia", "@MinimoExistencia", DataType.Parametro)
            Upd.Add("MaximoExistencia", "@MaximoExistencia", DataType.Parametro)
            Upd.Add("Genera_lp_auto", "@Genera_lp_auto", DataType.Parametro)
            Upd.Add("Permitir_paletizar", "@Permitir_paletizar", DataType.Parametro)
            Upd.Where(String.Format("IdPresentacion = @IdPresentacion And codigo_barra ='{0}'", pCodigoBarraAnterior))

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_presentacion.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeProducto_presentacion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", IIf(oBeProducto_presentacion.Codigo_barra = String.Empty, DBNull.Value, oBeProducto_presentacion.Codigo_barra)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_presentacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@IMPRIME_BARRA", oBeProducto_presentacion.Imprime_barra))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_presentacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_presentacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_presentacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_presentacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeProducto_presentacion.Peso))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeProducto_presentacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeProducto_presentacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeProducto_presentacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeProducto_presentacion.Factor))
            cmd.Parameters.Add(New SqlParameter("@MINIMOEXISTENCIA", oBeProducto_presentacion.MinimoExistencia))
            cmd.Parameters.Add(New SqlParameter("@MAXIMOEXISTENCIA", oBeProducto_presentacion.MaximoExistencia))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LP_AUTO", oBeProducto_presentacion.Genera_lp_auto))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_PALETIZAR", oBeProducto_presentacion.Permitir_paletizar))

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

    Public Shared Function ExistePeso(ByVal pIdPresentacion As Integer) As Double

        Try

            Dim lPeso As Double = 0.0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(peso),0) FROM producto_presentacion WHERE idPresentacion={0}", pIdPresentacion), lConnection)
                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lPeso = CDbl(lReturnValue)
                    End If
                    lCommand.Dispose()
                End Using
                lConnection.Close()
                lConnection.Dispose()
            End Using

            Return lPeso

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Desactivar(ByVal pIdPresentacion As Integer)
        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = String.Format("UPDATE producto_presentacion SET Activo=0 WHERE IdPresentacion={0}", pIdPresentacion)

                Using lCommand As New SqlCommand(vSQL, lConnection)

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

    Public Shared Function Get_Factor_By_IdProducto_And_IdPresentacion(ByVal pIdProducto As Integer,
                                                                       ByVal pIdPresentacion As Integer) As Double

        Get_Factor_By_IdProducto_And_IdPresentacion = 0

        Try

            Dim vSQL As String = "SELECT ISNULL(Factor,0) AS Factor FROM producto_presentacion WHERE IdProducto=@IdProducto AND IdPresentacion=@IdPresentacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Factor_By_IdProducto_And_IdPresentacion = CDbl(lReturnValue)
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

    Public Shared Function Get_Factor_By_IdProducto_And_IdPresentacion(ByVal pIdProducto As Integer,
                                                                       ByVal pIdPresentacion As Integer,
                                                                       Optional ByVal pConnection As SqlConnection = Nothing,
                                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Double

        Get_Factor_By_IdProducto_And_IdPresentacion = 0

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim lFactor As Double
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand

            Dim vSQL As String = "SELECT ISNULL(Factor,0) AS Factor FROM producto_presentacion WHERE IdProducto=@IdProducto AND IdPresentacion=@IdPresentacion"

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
            lCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                lFactor = CDbl(lReturnValue)
            Else
                lFactor = 0.0
            End If

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lFactor

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Factor_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                          ByVal pIdPresentacion As Integer,
                                                          Optional ByVal pConnection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Double



        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim lFactor As Double
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lCommand As New SqlCommand

            Dim vSQL As String = "SELECT CASE WHEN pp.EsPallet = 1 AND pp.camasportarima <> 0 AND cajasporcama<>0 THEN 
                     ISNULL(pp.Factor * pp.camasportarima * pp.cajasporcama,0) ELSE 
                     ISNULL(pp.Factor,0) END AS Factor  FROM producto_presentacion pp INNER JOIN producto_bodega pb 
                     ON pp.IdProducto = pb.IdProducto 
                     WHERE pb.IdProductoBodega=@IdProductoBodega AND pp.IdPresentacion=@IdPresentacion"

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lCommand = New SqlCommand(vSQL, lConnection)
                lConnection.Open()
            End If

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
            lCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                lFactor = CDbl(lReturnValue)
            Else
                lFactor = 0.0
            End If

            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If

            Return lFactor

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Sub Insert_Multiple(ByVal pListBeProducto_Presentacion As List(Of clsBeProducto_Presentacion))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            For Each Obj As clsBeProducto_Presentacion In pListBeProducto_Presentacion
                If Obj.IsNew Then
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Get_All_For_Seleccion_By_IdBodega(ByVal idBodega As Integer) As List(Of clsBeProducto_presentacion_sel)

        Dim lReturnList As New List(Of clsBeProducto_presentacion_sel)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL = ""
                vSQL &= "SELECT producto_presentacion.IdPresentacion, producto_presentacion.nombre AS Nombre, producto.nombre AS Producto, propietarios.nombre_comercial AS Propietario "
                vSQL &= "FROM producto_presentacion INNER JOIN "
                vSQL &= "producto ON producto_presentacion.IdProducto = producto.IdProducto INNER JOIN "
                vSQL &= "propietarios On producto.IdPropietario = propietarios.IdPropietario INNER JOIN "
                vSQL &= "propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario "
                vSQL &= String.Format("WHERE (propietario_bodega.Idbodega ={0}) ORDER BY Nombre, Producto ", idBodega)


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_presentacion_sel

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_presentacion_sel
                            Obj.Seleccion = False
                            Obj.idPresentacion = lRow("IdPresentacion")
                            Obj.Presentacion = lRow("Nombre")
                            Obj.Producto = lRow("Producto")
                            Obj.Proprietario = lRow("Propietario")
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto_For_Combo(ByVal pidProducto As Integer) As DataTable

        Try

            Const sp As String = "Select IdPresentacion, nombre from producto_presentacion where IdProducto=@IdProducto AND activo = 1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdProducto", pidProducto)
            Dim dt As New DataTable

            dad.Fill(dt)

            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * FROM Producto_presentacion" &
            " Where(IdPresentacion = @IdPresentacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion.IdPresentacion))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_presentacion, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_Presentacion_By_Idbodega_And_IdPropietarioBodega(ByVal pIdBodega As Integer, pIdPropietarioBodega As Integer) As DataTable

        Get_Nombre_Presentacion_By_Idbodega_And_IdPropietarioBodega = Nothing

        Try

            Dim vSQL As String = "SELECT producto_presentacion.IdPresentacion, producto.IdProducto, producto_presentacion.nombre
                        From producto INNER Join
                        producto_bodega On producto.IdProducto = producto_bodega.IdProducto INNER Join
                        producto_presentacion On producto.IdProducto = producto_presentacion.IdProducto inner Join
                        propietario_bodega On propietario_bodega.IdPropietario = producto.IdPropietario
                        Where (producto_bodega.IdBodega = @IdBodega) And (propietario_bodega.IdPropietarioBodega = @IdPropietarioBodega)"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        lDataAdapter.Fill(lTable)
                        Get_Nombre_Presentacion_By_Idbodega_And_IdPropietarioBodega = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'GT 10052021 carga todas las presentaciones por bodega para multiempresa
    Public Shared Function Get_Nombre_Presentacion_By_Idbodega(ByVal pIdBodega As Integer) As DataTable

        Get_Nombre_Presentacion_By_Idbodega = Nothing

        Try

            Dim vSQL As String = "select pp.IdPresentacion,pr.IdProducto,pp.nombre from 
                                  producto pr INNER JOIN producto_presentacion pp on pr.IdProducto=pp.IdProducto inner join
                                  producto_bodega pb on pr.IdProducto = pb.IdProducto where (pb.IdBodega = @IdBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.Fill(lTable)
                        Get_Nombre_Presentacion_By_Idbodega = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Presentacion_By_IdProductoBodega_And_NomPres(ByVal pIdProductoBodega As Integer, ByVal pCodigo As String,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_Presentacion_By_IdProductoBodega_And_NomPres = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 pp.* FROM producto_presentacion pp inner join 
                    producto_bodega pb on pp.idproducto = pb.idproducto
                    WHERE  pp.Nombre=@Nombre and pb.idproductobodega=@IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeProducto_Presentacion()
                    Cargar(ObjUM, lRow)
                    Return ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_Presentacion_By_IdPresentacion(ByVal pIdPresentacion As Integer,
                                                                     ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As String
        Get_Nombre_Presentacion_By_IdPresentacion = ""

        Try

            Dim lNombre As String = ""

            Dim vSQL As String = "SELECT nombre 
                                  FROM producto_presentacion 
                                  WHERE  IdPresentacion=@pIdPresentacion"

            Using lCommand As New SqlCommand(vSQL, lConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = lTransaction

                lCommand.Parameters.AddWithValue("@pIdPresentacion", pIdPresentacion)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                lNombre = lReturnValue

                lCommand.Dispose()

            End Using

            Return lNombre

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto_ForImpresion(ByVal pIdProducto As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As DataTable

        Get_All_By_IdProducto_ForImpresion = Nothing

        Try


            Dim vSQL As String = "SELECT IdPresentacion,nombre,camasportarima,cajasporcama,factor FROM producto_presentacion WHERE IdProducto=@IdProducto AND activo=1 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDataTable As New DataTable("Presentaciones")
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Return lDataTable

                End If

            End Using

        Catch ex As Exception
            Throw New Exception("LnProducto_Presentacion_GetAllByProductoHH: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Presentacion_By_IdProductoBodega_And_CodPres(ByVal pIdProductoBodega As Integer,
                                                                            ByVal pCodigo As String,
                                                                            ByRef lConnection As SqlConnection,
                                                                            ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_Presentacion_By_IdProductoBodega_And_CodPres = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 pp.* FROM producto_presentacion pp inner join 
                    producto_bodega pb on pp.idproducto = pb.idproducto
                    WHERE pp.codigo=@Codigo and pb.idproductobodega=@IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeProducto_Presentacion()
                    Cargar(ObjUM, lRow)
                    Return ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo(ByVal pCodigo As String,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_Single_By_Codigo = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE (codigo_barra=@pCodigo)"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pCodigo", pCodigo.Trim())

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Presentacion_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                                    ByVal lConnection As SqlConnection,
                                                                    ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Get_All_Presentacion_By_IdProductoBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_Presentacion)


            Dim vSQL As String = "SELECT distinct IdPresentacion, nombre, IdProductoBodega, IdPropietarioBodega, IdPropietario, 
                                          IdProducto, codigo_barra, imprime_barra, peso, alto, largo, ancho, factor, MinimoExistencia, 
                                          MaximoExistencia, user_agr, fec_agr, user_mod, fec_mod, activo,  IdBodega 
                                          FROM VW_StockPresentaciones WHERE IdProductoBodega=@IdProductoBodega ORDER BY [Nombre]"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_Presentacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_Presentacion

                        Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)

                        If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                            Obj.Nombre = CType(lRow("Nombre"), String)
                        End If

                        If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                            Obj.Activo = CType(lRow("activo"), Boolean)
                        End If

                        If lRow("Imprime_Barra") IsNot DBNull.Value AndAlso lRow("Imprime_Barra") IsNot Nothing Then
                            Obj.Imprime_barra = CType(lRow("Imprime_Barra"), Boolean)
                        End If

                        If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                            Obj.Peso = CType(lRow("Peso"), Double)
                        End If

                        If lRow("Alto") IsNot DBNull.Value AndAlso lRow("Alto") IsNot Nothing Then
                            Obj.Alto = CType(lRow("Alto"), Double)
                        End If

                        If lRow("Largo") IsNot DBNull.Value AndAlso lRow("Largo") IsNot Nothing Then
                            Obj.Largo = CType(lRow("Largo"), Double)
                        End If

                        If lRow("Ancho") IsNot DBNull.Value AndAlso lRow("Ancho") IsNot Nothing Then
                            Obj.Ancho = CType(lRow("Ancho"), Double)
                        End If

                        If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                            Obj.Factor = CType(lRow("Factor"), Double)
                        End If

                        lReturnList.Add(Obj)

                    Next

                    'GT28012022: no aplica, porque retorna null en la lista
                    'Get_All_Presentacion_By_IdProductoBodega = lReturnList

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Presentaciones_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                                      ByVal pActivo As Boolean,
                                                                      ByVal lConnection As SqlConnection,
                                                                      ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Get_All_Presentaciones_By_IdProductoBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_Presentacion)

            Dim vSQL As String = "SELECT * FROM VW_ProductoPresentacion WHERE IdProductoBodega=@IdProductoBodega "

            If pActivo Then
                vSQL += " AND activo=1 "
            Else
                vSQL += " AND activo=0 "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_Presentacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_Presentacion
                        Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        Obj = GetSingle(Obj.IdPresentacion, lConnection, lTransaction)
                        lReturnList.Add(Obj.Clone())

                    Next

                    'GT28012022: no aplica porque retorna null en una lista
                    'Get_All_Presentaciones_By_IdProductoBodega = lReturnList

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Nombre(ByVal pNombre As String) As clsBeProducto_Presentacion

        Get_Single_By_Nombre = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE Nombre=@Nombre"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@Nombre", pNombre)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Dim Obj As New clsBeProducto_Presentacion()
                            Cargar(Obj, lRow)
                            Get_Single_By_Nombre = Obj

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

    Public Shared Function Get_Single_By_Nombre(ByVal pNombre As String,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_Single_By_Nombre = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE Nombre=@Nombre"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@Nombre", pNombre)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Get_Single_By_Nombre = Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo_Producto(ByVal pCodigoBarra As String,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_Single_By_Codigo_Producto = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE codigo_barra=@codigo_barra"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@codigo_barra", pCodigoBarra)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Get_Single_By_Codigo_Producto = Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_Codigo_Producto_And_Presentacion(ByVal pCodigoProducto As String,
                                                                   ByVal pNombrePresentacion As String,
                                                                   ByRef lConnection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion
        Get_By_Codigo_Producto_And_Presentacion = Nothing

        Try

            Dim lNombre As String = ""

            Dim vSQL As String = "SELECT * 
                                  FROM producto_presentacion 
                                  WHERE  codigo_barra=@codigo_producto AND nombre=@Nombre"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@codigo_producto", pCodigoProducto)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@Nombre", pNombrePresentacion)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Get_By_Codigo_Producto_And_Presentacion = Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Presentacion_Defecto_By_IdProducto(ByVal pIdProducto As Integer,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_Presentacion_Defecto_By_IdProducto = Nothing

        Dim vSQL As String = "SELECT TOP(1) * FROM producto_presentacion WHERE (IdProducto=@pIdProducto)"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim BeProductoPresentacion As New clsBeProducto_Presentacion()
                    Cargar(BeProductoPresentacion, lRow)
                    Return BeProductoPresentacion

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Presentacion_Defecto_By_IdProducto(ByVal pIdProducto As Integer) As clsBeProducto_Presentacion

        Get_Presentacion_Defecto_By_IdProducto = Nothing

        Dim vSQL As String = "SELECT TOP(1) * FROM producto_presentacion WHERE (IdProducto=@pIdProducto)"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Dim Obj As New clsBeProducto_Presentacion()

                            Cargar(Obj, lRow)

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

    Public Shared Function Get_All_BePresentacion_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeProducto_Presentacion)

        Get_All_BePresentacion_By_IdBodega = Nothing

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try

            Dim vSQL As String = "SELECT pp.* FROM producto_presentacion pp
                                  INNER JOIN producto_bodega pb
                                  ON pp.IdProducto = pb.IdProducto
                                  WHERE pp.activo = 1 AND pb.IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Presentacion
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

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

    Public Shared Function Get_All_BePresentacion_By_IdBodega_And_IdPedido(ByVal pIdBodega As Integer, ByVal pIdPedidoEnc As Integer) As List(Of clsBeProducto_Presentacion)

        Get_All_BePresentacion_By_IdBodega_And_IdPedido = Nothing

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try

            Dim vSQL As String = "SELECT pp.* FROM producto_presentacion pp
                                  INNER JOIN producto_bodega pb
                                  ON pp.IdProducto = pb.IdProducto
                                  WHERE pp.activo = 1 AND pb.IdBodega=@IdBodega 
                                  AND pp.IdPresentacion in (SELECT IdPresentacion FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc AND IdBodega= @IdBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Presentacion
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                            Get_All_BePresentacion_By_IdBodega_And_IdPedido = lReturnList

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

    Public Shared Function Existe_Presentacion_By_IdProducto_And_CodigoBarra(ByVal pIdProducto As Integer,
                                                                             ByVal pCodigoBarra As String,
                                                                             ByRef lConnection As SqlConnection,
                                                                             ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Existe_Presentacion_By_IdProducto_And_CodigoBarra = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion 
                              WHERE (IdProducto = @IdProducto AND codigo_barra=@codigo_barra)"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@codigo_barra", pCodigoBarra.Trim())

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdPresentacion_By_Codigo_Barra(ByVal pIdPresentacion As Integer, ByVal pIdProducto As Integer) As Integer

        Get_IdPresentacion_By_Codigo_Barra = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdPresentacion FROM producto_presentacion WHERE activo = 1 AND IdPresentacion=@IdPresentacion AND IdProducto = @IdProducto "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_IdPresentacion_By_Codigo_Barra = lDataTable.Rows(0).Item("IdPresentacion")

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

    Public Shared Function Get_IdPresentacion_By_IdPresentacion_And_IdProducto(ByVal pIdPresentacion As Integer,
                                                                              ByVal pIdProducto As Integer,
                                                                              ByVal lConnection As SqlConnection,
                                                                              ByVal ltransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_IdPresentacion_By_IdPresentacion_And_IdProducto = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE activo = 1 AND IdPresentacion=@IdPresentacion AND IdProducto = @IdProducto "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = ltransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim BeProductoPresentacion As New clsBeProducto_Presentacion()
                    Cargar(BeProductoPresentacion, lRow)
                    Get_IdPresentacion_By_IdPresentacion_And_IdProducto = BeProductoPresentacion

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_BePresentacion_By_IdBodega_And_IdPedido(ByVal pIdBodega As Integer,
                                                                           ByVal pIdPedidoEnc As Integer,
                                                                           ByVal lConnection As SqlConnection,
                                                                           ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Get_All_BePresentacion_By_IdBodega_And_IdPedido = Nothing

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try

            Dim vSQL As String = "SELECT pp.* FROM producto_presentacion pp
                                  INNER JOIN producto_bodega pb
                                  ON pp.IdProducto = pb.IdProducto
                                  WHERE pp.activo = 1 AND pb.IdBodega=@IdBodega 
                                  AND pp.IdPresentacion in (SELECT IdPresentacion FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc AND IdBodega= @IdBodega)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeProductoPresentacion As clsBeProducto_Presentacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        BeProductoPresentacion = New clsBeProducto_Presentacion
                        Cargar(BeProductoPresentacion, lRow)
                        lReturnList.Add(BeProductoPresentacion)
                    Next

                    Get_All_BePresentacion_By_IdBodega_And_IdPedido = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeProductoPresentacion_By_IdPresentacion(ByVal pIdPresentacion As Integer,
                                                                        ByVal lConnection As SqlConnection,
                                                                        ByVal lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdPresentacion=@IdPresentacion"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim BeProductoPresentacion As New clsBeProducto_Presentacion()

                    Cargar(BeProductoPresentacion, lRow)

                    If lRow("factor") IsNot DBNull.Value AndAlso lRow("factor") IsNot Nothing Then
                        BeProductoPresentacion.Factor = CType(lRow("factor"), Double)
                    End If

                    Return BeProductoPresentacion

                End If

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_IdProducto_And_Presentacion(ByVal pIdProducto As Integer,
                                                                 ByVal pNombre As String,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Existe_By_IdProducto_And_Presentacion = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion 
                              WHERE IdProducto = @IdProducto AND nombre=@pNombre"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pNombre", clsPublic.Quitar_Caracteres_No_Permitidos(pNombre.Trim()))

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing Then
                    If lDataTable.Rows.Count > 0 Then
                        Dim vBeProductoPres As New clsBeProducto_Presentacion
                        Cargar(vBeProductoPres, lDataTable.Rows(0))
                        Existe_By_IdProducto_And_Presentacion = vBeProductoPres
                    End If
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Presentacion_By_Nombre(ByVal pIdProducto As Integer, ByVal pNombre As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Existe_Presentacion_By_Nombre = Nothing

        Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE nombre=@pNombre and idproducto = @IdProducto"

        Try

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@pNombre", pNombre.Trim())
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_Codigo_Producto_And_Nombre_Presentacion(ByVal pCodigoProducto As String,
                                                                          ByVal pNombrePresentacion As String,
                                                                          ByRef lConnection As SqlConnection,
                                                                          ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion
        Get_By_Codigo_Producto_And_Nombre_Presentacion = Nothing

        Try

            Dim vSQL As String = "SELECT pp.*
                                  FROM producto_presentacion pp INNER JOIN producto p ON pp.IdProducto = p.IdProducto
                                  WHERE pp.nombre=@NombrePresentacion and p.Codigo = @CodigoProducto"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@CodigoProducto", pCodigoProducto)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@NombrePresentacion", pNombrePresentacion)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Get_By_Codigo_Producto_And_Nombre_Presentacion = Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Presentacion_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try
            Dim vSQL As String = "SELECT DISTINCT IdPresentacion, nombre, IdProductoBodega, IdPropietarioBodega, IdPropietario, 
                                        IdProducto, codigo_barra, imprime_barra, peso, alto, largo, ancho, factor, 
                                        MinimoExistencia, MaximoExistencia, user_agr, fec_agr, user_mod, fec_mod, activo, IdBodega, 
                                        EsPallet, Precio, MinimoPeso, MaximoPeso, Costo, CamasPorTarima, CajasPorCama, genera_lp_auto, permitir_paletizar, 
                                        sistema, IdPresentacionPallet, codigo
                                FROM VW_StockPresentaciones WHERE IdProductoBodega IN (SELECT IdProductoBodega FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc)
                                ORDER BY Nombre"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                For Each lRow As DataRow In lDataTable.Rows

                    Dim Obj As New clsBeProducto_Presentacion

                    Cargar(Obj, lRow)

                    Obj.IdPresentacion = Convert.ToInt32(lRow("IdPresentacion"))

                    If Not IsDBNull(lRow("Nombre")) Then Obj.Nombre = lRow("Nombre").ToString()
                    If Not IsDBNull(lRow("activo")) Then Obj.Activo = Convert.ToBoolean(lRow("activo"))
                    If Not IsDBNull(lRow("Imprime_Barra")) Then Obj.Imprime_barra = Convert.ToBoolean(lRow("Imprime_Barra"))
                    If Not IsDBNull(lRow("Peso")) Then Obj.Peso = Convert.ToDouble(lRow("Peso"))
                    If Not IsDBNull(lRow("Alto")) Then Obj.Alto = Convert.ToDouble(lRow("Alto"))
                    If Not IsDBNull(lRow("Largo")) Then Obj.Largo = Convert.ToDouble(lRow("Largo"))
                    If Not IsDBNull(lRow("Ancho")) Then Obj.Ancho = Convert.ToDouble(lRow("Ancho"))
                    If Not IsDBNull(lRow("Factor")) Then Obj.Factor = Convert.ToDouble(lRow("Factor"))

                    lReturnList.Add(Obj)

                Next
            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Error en Get_All_Presentacion_By_IdPedidoEnc: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_IdPresentacion_By_Codigo(ByVal pCodigo As String,
                                                        ByVal pIdProducto As String,
                                                        ByVal pConnection As SqlConnection,
                                                        ByVal pTransaction As SqlTransaction) As Integer

        Get_IdPresentacion_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT IdPresentacion FROM producto_presentacion WHERE Codigo=@pCodigo AND IdProducto = @pIdProducto "

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@pCodigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_IdPresentacion_By_Codigo = lDataTable.Rows(0).Item("IdPresentacion")

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_Codigo_Producto_And_Nombre_Presentacion(ByVal pCodigoProducto As String,
                                                                          ByVal pNombrePresentacion As String,
                                                                          ByRef lConnection As SqlConnection,
                                                                          ByRef lTransaction As SqlTransaction) As clsBeProducto_Presentacion
        Get_By_Codigo_Producto_And_Nombre_Presentacion = Nothing

        Try

            Dim vSQL As String = "SELECT pp.*
                                  FROM producto_presentacion pp INNER JOIN producto p ON pp.IdProducto = p.IdProducto
                                  WHERE pp.nombre=@NombrePresentacion and p.Codigo = @CodigoProducto"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@CodigoProducto", pCodigoProducto)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@NombrePresentacion", pNombrePresentacion)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_Presentacion()
                    Cargar(Obj, lRow)
                    Get_By_Codigo_Producto_And_Nombre_Presentacion = Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    '#GT20012025: cargar presentacion para preimpresion mhs
    Public Shared Function Get_Single_By_IdPresentacion(ByVal pIdPresentacion As Integer) As clsBeProducto_Presentacion

        Get_Single_By_IdPresentacion = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion WHERE IdPresentacion=@IdPresentacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Dim Obj As New clsBeProducto_Presentacion()

                            Cargar(Obj, lRow)

                            If lRow("factor") IsNot DBNull.Value AndAlso lRow("factor") IsNot Nothing Then
                                Obj.Factor = CType(lRow("factor"), Double)
                            End If

                            Get_Single_By_IdPresentacion = Obj

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
    Public Shared Function Get_All_Presentacion_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try
            Dim vSQL As String = "SELECT DISTINCT IdPresentacion, nombre, IdProductoBodega, IdPropietarioBodega, IdPropietario, 
                                        IdProducto, codigo_barra, imprime_barra, peso, alto, largo, ancho, factor, 
                                        MinimoExistencia, MaximoExistencia, user_agr, fec_agr, user_mod, fec_mod, activo, IdBodega, 
                                        EsPallet, Precio, MinimoPeso, MaximoPeso, Costo, CamasPorTarima, CajasPorCama, genera_lp_auto, permitir_paletizar, 
                                        sistema, IdPresentacionPallet, codigo
                                FROM VW_StockPresentaciones WHERE IdProductoBodega IN (SELECT IdProductoBodega FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc)
                                ORDER BY Nombre"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                For Each lRow As DataRow In lDataTable.Rows

                    Dim Obj As New clsBeProducto_Presentacion

                    Cargar(Obj, lRow)

                    Obj.IdPresentacion = Convert.ToInt32(lRow("IdPresentacion"))

                    If Not IsDBNull(lRow("Nombre")) Then Obj.Nombre = lRow("Nombre").ToString()
                    If Not IsDBNull(lRow("activo")) Then Obj.Activo = Convert.ToBoolean(lRow("activo"))
                    If Not IsDBNull(lRow("Imprime_Barra")) Then Obj.Imprime_barra = Convert.ToBoolean(lRow("Imprime_Barra"))
                    If Not IsDBNull(lRow("Peso")) Then Obj.Peso = Convert.ToDouble(lRow("Peso"))
                    If Not IsDBNull(lRow("Alto")) Then Obj.Alto = Convert.ToDouble(lRow("Alto"))
                    If Not IsDBNull(lRow("Largo")) Then Obj.Largo = Convert.ToDouble(lRow("Largo"))
                    If Not IsDBNull(lRow("Ancho")) Then Obj.Ancho = Convert.ToDouble(lRow("Ancho"))
                    If Not IsDBNull(lRow("Factor")) Then Obj.Factor = Convert.ToDouble(lRow("Factor"))

                    lReturnList.Add(Obj)

                Next
            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Error en Get_All_Presentacion_By_IdPedidoEnc: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_IdPresentacion_By_Codigo(ByVal pCodigo As String,
                                                        ByVal pIdProducto As String,
                                                        ByVal pConnection As SqlConnection,
                                                        ByVal pTransaction As SqlTransaction) As Integer

        Get_IdPresentacion_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT IdPresentacion FROM producto_presentacion WHERE Codigo=@pCodigo AND IdProducto = @pIdProducto "

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@pCodigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_IdPresentacion_By_Codigo = lDataTable.Rows(0).Item("IdPresentacion")

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
#End Region

End Class