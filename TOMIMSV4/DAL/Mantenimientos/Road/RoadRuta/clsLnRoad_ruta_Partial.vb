
Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRoad_ruta

    Public Shared Function GetSingle(ByVal pIdRuta As Integer) As clsBeRoad_ruta


        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM road_ruta WHERE idruta=@idruta"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@Idruta", pIdRuta)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeRoad_ruta()

                        Cargar(Obj, lRow)

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdRuta As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeRoad_ruta


        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM road_ruta WHERE idruta=@idruta"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Idruta", pIdRuta)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeRoad_ruta()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdRuta_By_Codigo(ByVal pCodigoRuta As String,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As clsBeRoad_ruta


        Get_IdRuta_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM road_ruta WHERE codigo=@codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigoRuta)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeRoad_ruta()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeRoad_ruta)

        Dim lReturnList As New List(Of clsBeRoad_ruta)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM road_ruta WHERE 1 > 0 "

                If pActivo = True Then
                    vSQL += " AND activo='S'"
                Else
                    vSQL += " AND activo='N'"
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeRoad_ruta

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeRoad_ruta

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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdRuta),0) FROM road_ruta"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Enum TipoRuta
        Venta = 0
        Preventa = 1
        Despacho = 2
        Pedido = 3
        Todas = 4
    End Enum

    Public Overloads Shared Function Listar_RoadRutas(ByVal pTipoRuta As TipoRuta) As DataTable

        Dim DT As New DataTable

        Try

            Dim vSQL As String = "SELECT IdRuta, ISNULL(Codigo,'') + ' ' + ISNULL(Nombre,'') AS Nombre " &
                                 " FROM road_ruta WHERE Activo='S' "

            Select Case pTipoRuta

                Case TipoRuta.Despacho

                    vSQL += "And Venta = 'D' "

                Case TipoRuta.Preventa

                    vSQL += "And Venta = 'P' "

                Case TipoRuta.Pedido

                    vSQL += "And Venta IN('V','P','T') "

                Case TipoRuta.Venta

                    vSQL += "And Venta = 'V' "

                Case TipoRuta.Todas

                    '

            End Select


            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.Fill(DT)

            'If DT.Rows.Count > 0 Then
            '    cmbRuta.DisplayMember = "Nombre"
            '    cmbRuta.ValueMember = "IdRuta"
            '    cmbRuta.DataSource = DT
            'End If

            Return DT

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Overloads Shared Function Listar_RoadRutas(ByVal pTipoRuta As TipoRuta,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As DataTable

        Dim DT As New DataTable

        Try

            Dim vSQL As String = "SELECT IdRuta, ISNULL(Codigo,'') + ' ' + ISNULL(Nombre,'') AS Nombre " &
                " FROM road_ruta WHERE Activo='S' "

            Select Case pTipoRuta

                Case TipoRuta.Despacho

                    vSQL += "And Venta = 'D' "

                Case TipoRuta.Preventa

                    vSQL += "And Venta = 'P' "

                Case TipoRuta.Pedido

                    vSQL += "And Venta IN('V','P','T') "

                Case TipoRuta.Venta

                    vSQL += "And Venta = 'V' "

                Case TipoRuta.Todas

            End Select

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.Fill(DT)

            Return DT

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdRuta),0) FROM road_ruta"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class