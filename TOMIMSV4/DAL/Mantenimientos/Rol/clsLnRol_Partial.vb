Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRol

    Public Shared Function Existe_Rol(ByVal pIdrolSistema As Integer) As Integer

        Try

            Dim vSQL As String = "SELECT * FROM rol WHERE idRol=" & pIdrolSistema

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

    Public Shared Sub CargaMenuSistema(ByRef gv As DataGridView, Optional Filtro As String = "")

        Dim I As Integer, vMenuID$, vTitulo$
        Dim vSolicitarClave As Boolean = False
        Dim vFactor As Integer

        Try

            gv.Rows.Clear()

            Dim vSQL As String = "SELECT IdMenu, titulo, (nivel - 1) AS factor,solicitar_clave_autorizacion  FROM menu_sistema " &
                    IIf(Filtro <> "", " Where titulo Like '%" & Filtro & "%'", "")

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL$, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    'If Filtro <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@Filtro", ")

                    Dim DT As New DataTable
                    lDTA.Fill(DT)

                    If DT.Rows.Count > 0 Then

                        For I = 0 To DT.Rows.Count - 1

                            vMenuID$ = IIf(IsDBNull(DT.Rows(I).Item("IdMenu")), "", DT.Rows(I).Item("IdMenu")).ToString
                            vTitulo$ = IIf(IsDBNull(DT.Rows(I).Item("titulo")), "", DT.Rows(I).Item("titulo")).ToString
                            vFactor = Integer.Parse(IIf(IsDBNull(DT.Rows(I).Item("factor")), 0, DT.Rows(I).Item("factor")).ToString)
                            vSolicitarClave = IIf(IsDBNull(DT.Rows(I).Item("solicitar_clave_autorizacion")), 0, DT.Rows(I).Item("solicitar_clave_autorizacion"))

                            vTitulo$ = Space(5 * vFactor) & vTitulo$
                            gv.Rows.Add(False, vTitulo$, vMenuID$, vSolicitarClave)

                        Next

                    End If

                    DT.Dispose()

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Shared Sub CargaMenuRol(ByRef gv As DataGridView, ByVal IdRol As Integer, Optional Filtro As String = "")

        Dim I As Integer, vMenuID, vTitulo, vIdMenuRol As String
        Dim vFactor As Integer

        Try

            gv.Rows.Clear()

            Dim vSQL As String = "SELECT a.IdMenu, b.titulo as titulo, (b.nivel - 1) AS factor, a.IdMenuRol
                     From menu_rol a, menu_sistema b  
                     WHERE a.IdMenu = b.IdMenu 
                     AND a.IdRol = @IdRol " &
                     IIf(Filtro <> "", " And b.titulo Like '%" & Filtro & "%'", "") &
                     " And a.visible = 1  ORDER BY 1 ASC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL$, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRol", IdRol)
                    'lDTA.SelectCommand.Parameters.AddWithValue("@Filtro", "'%" & Filtro & "%'")

                    Dim DT As New DataTable
                    lDTA.Fill(DT)

                    If DT.Rows.Count > 0 Then

                        For I = 0 To DT.Rows.Count - 1

                            vMenuID = IIf(IsDBNull(DT.Rows(I).Item("IdMenu")), "", DT.Rows(I).Item("IdMenu")).ToString
                            vTitulo = IIf(IsDBNull(DT.Rows(I).Item("titulo")), "", DT.Rows(I).Item("titulo")).ToString
                            vFactor = Integer.Parse(IIf(IsDBNull(DT.Rows(I).Item("factor")), 0, DT.Rows(I).Item("factor")).ToString)
                            vIdMenuRol = IIf(IsDBNull(DT.Rows(I).Item("IdMenuRol")), 0, DT.Rows(I).Item("IdMenuRol")).ToString

                            vTitulo = Space(5 * vFactor) & vTitulo
                            gv.Rows.Add(False, vTitulo$, vMenuID, vIdMenuRol)

                        Next

                    End If

                    DT.Dispose()

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    ''' <summary>
    ''' Verifica si una opcion de menu de sistema ya existe en el menu de Rol
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExistePermiso(ByVal pIdMenu As String,
                                         ByVal pIdrol As Integer) As Boolean

        ExistePermiso = False

        Try

            Dim vSQL As String = "SELECT COUNT(1) AS numregs FROM Menu_Rol " &
            " WHERE IdMenu= '" & pIdMenu & "' AND IdRol = " & Val(pIdrol)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        If lDataTable.Rows.Count > 0 Then
                            ExistePermiso = Val(lDataTable.Rows(0).Item("numregs").ToString) > 0
                        End If

                        lDataTable.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function ListarxFiltro(ByVal pFiltro As String, ByVal pidEmpresa As Integer) As List(Of clsBeRol)

        Dim lReturnList As New List(Of clsBeRol)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM rol WHERE 1>0  "
                ' vSQL += IIf(pIdEmpresa > 0, " WHERE IdEmpresa=" + pIdEmpresa.ToString(), "WHERE IdEmpresa<>0")

                If String.IsNullOrEmpty(pFiltro) = False Then
                    'vSQL += String.Format(" AND (idRol LIKE '%{0}%'", pFiltro)
                    'vSQL += String.Format(" OR Nombre LIKE '%{0}%')", pFiltro)
                    vSQL += " AND (idRol LIKE '%@idRol%'"
                    vSQL += " OR Nombre LIKE '%@Nombre%')"
                End If

                'vSQL += String.Format(" AND idempresa={0}", pidEmpresa)
                vSQL += " AND idempresa=@idempresa"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@idempresa", pidEmpresa)
                    If String.IsNullOrEmpty(pFiltro) Then
                        lDTA.SelectCommand.Parameters.AddWithValue("@idRol", pFiltro)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pFiltro)
                    End If

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeRol

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                        For Each lRow As DataRow In lDataTable.Rows
                            Obj = New clsBeRol()

                            Obj.IdRol = CType(lRow("idRol"), System.Int32)

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

    Public Shared Function Get_All_By_Empresa(ByVal pIdEmpresa As Integer) As List(Of clsBeRol)

        Try

            Dim lReturnList As New List(Of clsBeRol)
            Const sp As String = "SELECT * FROM Rol WHERE IdEmpresa = @IdEmpresa "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRol As New clsBeRol

            For Each dr As DataRow In dt.Rows

                vBeRol = New clsBeRol
                Cargar(vBeRol, dr)
                lReturnList.Add(vBeRol)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub CargaMenuRol_Opciones(ByRef ListaChecked As CheckedListBox,
                                            ByVal IdRol As Integer,
                                            ByVal IdMenu As String)

        Try
            Dim vLeer As Boolean = False
            Dim vModificar As Boolean = False
            Dim vEliminar As Boolean = False

            Dim vSQL As String = "SELECT a.IdMenu, a.Leer, a.Modificar, a.Eliminar
                     From menu_rol a
                     WHERE a.IdMenu = @IdMenu
                     AND a.IdRol = @IdRol "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL$, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRol", IdRol)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMenu", IdMenu)

                    Dim DT As New DataTable
                    lDTA.Fill(DT)

                    If DT.Rows.Count > 0 Then

                        vLeer = IIf(IsDBNull(DT.Rows(0).Item("Leer")), False, DT.Rows(0).Item("Leer"))
                        vModificar = IIf(IsDBNull(DT.Rows(0).Item("Modificar")), False, DT.Rows(0).Item("Modificar"))
                        vEliminar = IIf(IsDBNull(DT.Rows(0).Item("Eliminar")), False, DT.Rows(0).Item("Eliminar"))

                        ListaChecked.SetItemChecked(0, vLeer)
                        ListaChecked.SetItemChecked(1, vModificar)
                        ListaChecked.SetItemChecked(2, vEliminar)

                    End If

                    DT.Dispose()

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Shared Function Get_MenuRol_Opciones(ByVal IdRol As Integer,
                                                ByVal IdMenu As String) As clsBeOpcionesMenuRol

        Dim BeOpciones As New clsBeOpcionesMenuRol

        Try

            Dim vSQL As String = "SELECT a.IdMenu, a.Leer, a.Modificar, a.Eliminar
                     From menu_rol a
                     WHERE a.IdMenu = @IdMenu
                     AND a.IdRol = @IdRol "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL$, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRol", IdRol)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMenu", IdMenu)

                    Dim DT As New DataTable
                    lDTA.Fill(DT)

                    If DT.Rows.Count > 0 Then

                        BeOpciones.Leer = IIf(IsDBNull(DT.Rows(0).Item("Leer")), False, DT.Rows(0).Item("Leer"))
                        BeOpciones.Modificar = IIf(IsDBNull(DT.Rows(0).Item("Modificar")), False, DT.Rows(0).Item("Modificar"))
                        BeOpciones.Eliminar = IIf(IsDBNull(DT.Rows(0).Item("Eliminar")), False, DT.Rows(0).Item("Eliminar"))

                    End If

                    DT.Dispose()

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        Return BeOpciones

    End Function

End Class
