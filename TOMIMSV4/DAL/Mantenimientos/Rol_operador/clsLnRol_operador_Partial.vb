Imports System.Data.SqlClient

Partial Public Class clsLnRol_operador

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdRolOperador),0) FROM rol_operador"

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

    Public Shared Function ExistOperador(ByVal pIdrolOperador As Integer) As Integer

        Try

            Dim vSQL As String = "SELECT * FROM rol_Operador WHERE idRolOperador=" & pIdrolOperador

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 08112017 Quité query dentro de SqlDataAdapter.
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable

                    lDTA.Fill(lDataTable)

                    Return (lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0)

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ListarxFiltro(ByVal pFiltro As String) As List(Of clsBeRol_operador)

        Dim lReturnList As New List(Of clsBeRol_operador)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM rol_operador WHERE 1>0  "

                ' vSQL += IIf(pIdEmpresa > 0, " WHERE IdEmpresa=" + pIdEmpresa.ToString(), "WHERE IdEmpresa<>0")

                If String.IsNullOrEmpty(pFiltro) = False Then

                    '#HS20171023_1640pm: Quité String.Format.
                    vSQL += " AND (idRolOperador LIKE '%@idRolOperador%'"
                    vSQL += " OR Nombre LIKE '%@Nombre%')"
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    If String.IsNullOrEmpty(pFiltro) = False Then lDTA.SelectCommand.Parameters.AddWithValue("@idRolOperador", pFiltro)
                    If String.IsNullOrEmpty(pFiltro) = False Then lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pFiltro)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeRol_operador

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeRol_operador

                            Obj.IdRolOperador = CType(lRow("idRolOperador"), System.Int32)

                            If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("Nombre"), System.String)
                            End If

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

    Public Shared Sub CargaMenuSistema(ByRef Lv As ListView)

        Dim I As Integer, vMenuID$, vTitulo$
        Dim vFactor As Integer, Idx As Integer

        Try

            Lv.Items.Clear()

            Dim vSQL As String = "SELECT IdMenuSistemaOP, Nombre, (Nivel - 1) AS factor FROM menu_sistema_op ORDER BY 1 ASC "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL$, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    Dim DT As New DataTable
                    lDTA.Fill(DT)
                    Lv.BeginUpdate()

                    If DT.Rows.Count > 0 Then

                        For I = 0 To DT.Rows.Count - 1

                            vMenuID$ = IIf(IsDBNull(DT.Rows(I).Item("IdMenuSistemaOP")), "", DT.Rows(I).Item("IdMenuSistemaOP")).ToString
                            vTitulo$ = IIf(IsDBNull(DT.Rows(I).Item("Nombre")), "", DT.Rows(I).Item("Nombre")).ToString
                            vFactor = Integer.Parse(IIf(IsDBNull(DT.Rows(I).Item("factor")), 0, DT.Rows(I).Item("factor")).ToString)

                            vTitulo$ = Space(5 * vFactor) & vTitulo$
                            Lv.Items.Add(New ListViewItem(vTitulo$))
                            Idx = Lv.Items.Count - 1
                            Lv.Items(Idx).SubItems.Add(vMenuID$)

                        Next

                        If Lv.Items.Count > 0 Then Lv.Items(0).Selected = True

                    End If

                    Lv.EndUpdate()

                    DT.Dispose()

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Shared Sub CargaMenuRol(ByRef lv As ListView, ByVal IdRol As Integer)

        Dim I As Integer, vMenuID$, vTitulo$
        Dim vFactor As Integer, Idx As Integer

        Try

            lv.Items.Clear()

            Dim vSQL As String = "SELECT a.IdMenuSistemaOP, b.Nombre as titulo, (b.nivel - 1) AS factor  " &
            " FROM menu_rol_op a, menu_sistema_op b  " &
            " WHERE a.IdMenuSistemaOP = b.IdMenuSistemaOP " &
            " AND a.IdRolOperador= " & IdRol &
            " AND a.visible = 1 ORDER BY 1 ASC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL$, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    Dim DT As New DataTable

                    lDTA.Fill(DT)
                    lv.BeginUpdate()

                    If DT.Rows.Count > 0 Then

                        For I = 0 To DT.Rows.Count - 1

                            vMenuID$ = IIf(IsDBNull(DT.Rows(I).Item("IdMenuSistemaOP")), "", DT.Rows(I).Item("IdMenuSistemaOP")).ToString
                            vTitulo$ = IIf(IsDBNull(DT.Rows(I).Item("titulo")), "", DT.Rows(I).Item("titulo")).ToString
                            vFactor = Integer.Parse(IIf(IsDBNull(DT.Rows(I).Item("factor")), 0, DT.Rows(I).Item("factor")).ToString)
                            vTitulo$ = Space(5 * vFactor) & vTitulo$
                            lv.Items.Add(New ListViewItem(vTitulo$))
                            Idx = lv.Items.Count - 1
                            lv.Items(Idx).SubItems.Add(vMenuID$)

                        Next

                        lv.Items(0).Selected = True

                    End If

                    lv.EndUpdate() : DT.Dispose()

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Shared Function ExistePermiso(ByVal pIdMenu As String, ByVal pIdrolOperador As Integer) As Boolean

        ExistePermiso = False

        Try

            Dim vSQL As String = "SELECT COUNT(1) AS numregs FROM menu_rol_op " &
            " WHERE IdMenuSistemaOP= '" & pIdMenu & "' AND IdRolOperador = " & Val(pIdrolOperador)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDTA As New SqlDataAdapter(vSQL$, lConnection)
                    lDTA.SelectCommand.CommandType = CommandType.Text
                    Dim DT As New DataTable
                    lDTA.Fill(DT)
                    If DT.Rows.Count > 0 Then
                        ExistePermiso = Val(DT.Rows(0).Item("numregs").ToString) > 0
                    End If
                    DT.Dispose()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

End Class
