Imports System.Data.SqlClient

Partial Public Class clsLnRoad_p_vendedor

    Public Shared Function GetSingle(ByVal pIdVendedor As Integer) As clsBeRoad_p_vendedor


        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM road_p_vendedor WHERE IdVendedor=@IdVendedor"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdVendedor", pIdVendedor)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeRoad_p_vendedor()

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

    Public Shared Function GetSingle(ByVal pIdVendedor As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeRoad_p_vendedor


        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM road_p_vendedor WHERE IdVendedor=@IdVendedor"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdVendedor", pIdVendedor)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeRoad_p_vendedor()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeRoad_p_vendedor)

        Dim lReturnList As New List(Of clsBeRoad_p_vendedor)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM road_p_vendedor WHERE 1 > 0 "

                If pActivo = True Then
                    vSQL += " AND bloqueado=1"
                Else
                    vSQL += " AND bloqueado=0"
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeRoad_p_vendedor

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeRoad_p_vendedor

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

    Public Shared Function Get_All_For_Combo() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_For_Combo = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const vSQL As String = "SELECT IdVendedor,nombre as Nombre FROM road_p_vendedor WHERE 1 > 0 "
            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            lTransaction.Commit()

            Get_All_For_Combo = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetAllByRuta(ByVal pIdRuta As Integer) As List(Of clsBeRoad_p_vendedor)

        Dim lReturnList As New List(Of clsBeRoad_p_vendedor)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM road_p_vendedor WHERE IdRuta = @IdRuta or Ruta = @IdRuta "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRuta", pIdRuta)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeRoad_p_vendedor

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeRoad_p_vendedor
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

    Public Shared Function GetAllByRutaForCombo(ByVal pIdRuta As Integer) As DataTable

        Try

            Dim lReturnList As New List(Of clsBeRoad_p_vendedor)

            Const sp As String = "SELECT IdRuta,nombre FROM road_p_vendedor WHERE IdRuta = @IdRuta or Ruta = @IdRuta "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdRuta", pIdRuta)
            Dim dt As New DataTable

            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdVendedor),0) FROM road_p_vendedor"

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

    Public Shared Function Get_Vendedor_By_Codigo(ByVal pCodigoVendedor As String) As clsBeRoad_p_vendedor


        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM road_p_vendedor WHERE codigo= @codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigoVendedor)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeRoad_p_vendedor()
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


    Public Shared Function Get_Vendedor_By_Codigo(ByVal pCodigoVendedor As String,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As clsBeRoad_p_vendedor


        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM road_p_vendedor WHERE codigo= @codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigoVendedor)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeRoad_p_vendedor()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByRutaForCombo(ByVal pIdRuta As Integer,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim lReturnList As New List(Of clsBeRoad_p_vendedor)

            Const sp As String = "SELECT IdRuta,nombre FROM road_p_vendedor WHERE IdRuta = @IdRuta or Ruta = @IdRuta "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdRuta", pIdRuta)
            Dim dt As New DataTable

            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
