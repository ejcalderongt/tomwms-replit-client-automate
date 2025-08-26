Imports MySql.Data.MySqlClient
Imports System.Data
Imports MySql.Data

Public Class clsLnRol

    Public Sub Cargar(ByRef oBeRol As clsBeRol, ByRef dr As DataRow)
        Try
            With oBeRol
                .IdRol = Integer.Parse(IIf(IsDBNull(dr.Item("IdRol")), 0, dr.Item("IdRol")).ToString)
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre")).ToString
                .Fec_agr = DateTime.Parse(IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr")).ToString)
                .Fec_mod = DateTime.Parse(IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod")).ToString)
                .User_agr = Integer.Parse(IIf(IsDBNull(dr.Item("user_agr")), 0, dr.Item("user_agr")).ToString)
                .User_mod = Integer.Parse(IIf(IsDBNull(dr.Item("user_mod")), 0, dr.Item("user_mod")).ToString)
                .Activo = Integer.Parse(IIf(IsDBNull(dr.Item("Activo")), 0, dr.Item("Activo")).ToString)
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Insertar(ByRef oBeRol As clsBeRol, Optional ByVal pConection As MySqlConnection = Nothing, Optional ByVal pTransaction As MySqlTransaction = Nothing) As Integer

        Dim cnn As New MySqlConnection(BD.CadenaConexion)
        Dim cmd As New MySqlCommand()

        Try

            Ins.Init("rol")
            Ins.Add("idrol", "@idrol", "F")
            Ins.Add("nombre", "@nombre", "F")
            Ins.Add("fec_agr", "@fec_agr", "F")
            Ins.Add("fec_mod", "@fec_mod", "F")
            Ins.Add("user_agr", "@user_agr", "F")
            Ins.Add("user_mod", "@user_mod", "F")
            Ins.Add("activo", "@activo", "F")

            Dim sp As String = Ins.SQL()

            Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            cmd.CommandType = CommandType.Text


            If EsTransaccional Then
                cmd = New MySqlClient.MySqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                cmd = New MySqlClient.MySqlCommand(sp, cnn)
                cnn.Open()
            End If


            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@IDROL", oBeRol.IdRol))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@NOMBRE", oBeRol.Nombre))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@FEC_AGR", oBeRol.Fec_agr))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@FEC_MOD", oBeRol.Fec_mod))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@USER_AGR", oBeRol.User_agr))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@USER_MOD", oBeRol.User_mod))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@ACTIVO", oBeRol.Activo))

            Dim rowsAffected As Integer = 0
            rowsAffected = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeRol As clsBeRol, Optional ByVal pConection As MySqlConnection = Nothing, Optional ByVal pTransaction As MySqlTransaction = Nothing) As Integer

        Dim cnn As New MySqlConnection(BD.CadenaConexion)
        Dim cmd As New MySqlCommand()

        Try

            Upd.Init("rol")
            Upd.Add("idrol", "@idrol", "F")
            Upd.Add("nombre", "@nombre", "F")
            Upd.Add("fec_agr", "@fec_agr", "F")
            Upd.Add("fec_mod", "@fec_mod", "F")
            Upd.Add("user_agr", "@user_agr", "F")
            Upd.Add("user_mod", "@user_mod", "F")
            Upd.Add("activo", "@activo", "F")
            Upd.Where("IdRol = @IdRol")

            Dim sp As String = Upd.SQL()

            Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            cmd.CommandType = CommandType.Text


            If EsTransaccional Then
                cmd = New MySqlClient.MySqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New MySqlClient.MySqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@IDROL", oBeRol.IdRol))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@NOMBRE", oBeRol.Nombre))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@FEC_AGR", oBeRol.Fec_agr))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@FEC_MOD", oBeRol.Fec_mod))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@USER_AGR", oBeRol.User_agr))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@USER_MOD", oBeRol.User_mod))
            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@ACTIVO", oBeRol.Activo))

            Dim rowsAffected As Integer = 0
            rowsAffected = cmd.ExecuteNonQuery()
            Return rowsAffected


        Catch ex As Exception
            Throw ex
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
            cmd.Dispose()
        End Try

    End Function


    Public Function Eliminar(ByRef oBeRol As clsBeRol, Optional ByVal pConection As MySqlConnection = Nothing, Optional ByVal pTransaction As MySqlTransaction = Nothing) As Integer

        Dim cnn As New MySqlConnection(BD.CadenaConexion)
        Dim cmd As New MySqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Rol" & _
             "  Where(IdRol = @IdRol)"


            Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If EsTransaccional Then

                cmd = New MySqlClient.MySqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                cmd = New MySqlClient.MySqlCommand(sp, cnn)
                cnn.Open()

            End If


            cmd.Parameters.Add(New MySqlClient.MySqlParameter("@IDROL", oBeRol.IdRol))

            Dim rowsAffected As Integer = 0
            rowsAffected = cmd.ExecuteNonQuery()
            Return rowsAffected


        Catch ex As Exception
            Throw ex
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
            cmd.Dispose()
        End Try
    End Function

    Public Function Listar() As DataTable
        Try
            Dim sp As String = "SELECT * FROM Rol"

            Dim cnn As New MySqlConnection(BD.CadenaConexion)
            Dim cmd As New MySqlCommand(sp, cnn)
            cmd.CommandType = CommandType.Text


            Dim dad As New MySqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Listar(ByVal Activos As Boolean, Filtro As String) As DataTable

        Try

            Dim sp As String = "SELECT IdRol as Codigo, Nombre, Activo FROM Rol " & _
                "WHERE 1 >0  "

            If Activos Then
                sp += " and activo =1 "
            End If

            If Filtro <> "" Then
                sp += " and (IdRol like '%" & Filtro & "%'"
                sp += " or Nombre like '%" & Filtro & "%')"
            End If

            Dim cnn As New MySqlConnection(BD.CadenaConexion)
            Dim cmd As New MySqlCommand(sp, cnn)
            cmd.CommandType = CommandType.Text

            Dim dad As New MySqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Obtener(ByRef oBeRol As clsBeRol) As Boolean
        Try
            Dim sp As String = "SELECT * FROM Rol" & _
            " Where(IdRol = @IdRol)"

            Dim cnn As New MySqlConnection(BD.CadenaConexion)
            Dim cmd As New MySqlCommand(sp, cnn)
            cmd.CommandType = CommandType.Text


            Dim dad As New MySqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New MySqlClient.MySqlParameter("@IDROL", oBeRol.IdRol))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeRol, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Generar_Nuevo_IdRol() As Integer

        Generar_Nuevo_IdRol = 1

        Try

            vSQL$ = "SELECT MAX(IdRol) + 1 AS nuevo FROM rol "

            Dim sp As String = vSQL
            Dim cnn As New MySqlConnection(BD.CadenaConexion)
            Dim cmd As New MySqlCommand(sp, cnn)
            cmd.CommandType = CommandType.Text

            Dim dad As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Generar_Nuevo_IdRol = Integer.Parse(IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo")).ToString)
            End If

            cnn.Dispose()
            cmd.Dispose()
            dad.Dispose()
            dt.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function


    Public Sub CargaMenuSistema(ByRef Lv As ListView)

        ' Llena las opciones del menu sistema (List1)
        Dim DT As New DataTable
        Dim I As Integer, vMenuID$, vTitulo$
        Dim vFactor As Integer, Idx As Integer

        Try

            Lv.Items.Clear()

            vSQL$ = "SELECT IdMenu, NombreMenu, (nivel - 1) AS factor " & _
            " FROM menu ORDER BY 1 ASC "
            BD.OpenDT(DT, vSQL$)

            Lv.BeginUpdate()

            If DT.Rows.Count > 0 Then

                For I = 0 To DT.Rows.Count - 1

                    vMenuID$ = IIf(IsDBNull(DT.Rows(I).Item("IdMenu")), "", DT.Rows(I).Item("IdMenu")).ToString
                    vTitulo$ = IIf(IsDBNull(DT.Rows(I).Item("NombreMenu")), "", DT.Rows(I).Item("NombreMenu")).ToString
                    vFactor = Integer.Parse(IIf(IsDBNull(DT.Rows(I).Item("factor")), 0, DT.Rows(I).Item("factor")).ToString)

                    ' Agrega items al listView de registros
                    vTitulo$ = Space(5 * vFactor) & vTitulo$
                    Lv.Items.Add(New ListViewItem(vTitulo$))

                    ' Captura el indice del nuevo indice insertado para agregar columnas
                    Idx = Lv.Items.Count - 1
                    Lv.Items(Idx).SubItems.Add(vMenuID$)

                Next

                ' Se posiciona en el primer item
                If Lv.Items.Count > 0 Then Lv.Items(0).Selected = True

            End If

            Lv.EndUpdate()

            DT.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message) : Exit Sub
        End Try

    End Sub

    Public Sub CargaMenuRol(ByRef lv As ListView, ByVal IdRol As Integer)

        Dim DT As New DataTable
        Dim I As Integer, vMenuID$, vTitulo$
        Dim vFactor As Integer, Idx As Integer

        Try

            lv.Items.Clear()

            vSQL$ = "SELECT a.IdMenu, b.NombreMenu as titulo, (b.nivel - 1) AS factor " & _
            " FROM menurol a, menu b " & _
            " WHERE a.IdMenu = b.IdMenu " & _
            " AND a.IdRol= " & Val(IdRol) & _
            " AND a.visible = 1 ORDER BY 1 ASC"

            BD.OpenDT(DT, vSQL$)

            lv.BeginUpdate()

            If DT.Rows.Count > 0 Then

                For I = 0 To DT.Rows.Count - 1

                    vMenuID$ = IIf(IsDBNull(DT.Rows(I).Item("IdMenu")), "", DT.Rows(I).Item("IdMenu")).ToString
                    vTitulo$ = IIf(IsDBNull(DT.Rows(I).Item("titulo")), "", DT.Rows(I).Item("titulo")).ToString
                    vFactor = Integer.Parse(IIf(IsDBNull(DT.Rows(I).Item("factor")), 0, DT.Rows(I).Item("factor")).ToString)

                    ' Agrega items al listView de registros
                    vTitulo$ = Space(5 * vFactor) & vTitulo$
                    lv.Items.Add(New ListViewItem(vTitulo$))

                    ' Captura el indice del nuevo item para agregarle columnas
                    Idx = lv.Items.Count - 1
                    lv.Items(Idx).SubItems.Add(vMenuID$)

                Next

                ' Se posiciona en el primer item
                lv.Items(0).Selected = True

            End If

            lv.EndUpdate()

            DT.Dispose()

            Application.DoEvents()

        Catch ex As Exception
            MsgBox(ex.Message) : Exit Sub
        End Try

    End Sub

    ''' <summary>
    ''' Verifica si una opcion de menu de sistema ya existe en el menu de Rol
    ''' </summary>
    ''' <param name="pMenuID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExisteOpcion(ByVal pIDMenu As String, ByVal pIdrol As Integer) As Boolean

        ExisteOpcion = False

        Dim DT As New DataTable

        Try

            vSQL$ = "SELECT count(*) AS numregs FROM MenuRol " & _
            " WHERE IdMenu= '" & pIDMenu & "' AND IdRol = " & Val(pIdrol)

            BD.OpenDT(DT, vSQL$)

            ExisteOpcion = CBool(Val(DT.Rows(0).Item("numregs").ToString) > 0)

            DT.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DT.Dispose()
        End Try

    End Function

    Public Function Inserta_MenuRol(ByVal IdMenu As String, ByVal IdRod As Integer) As Boolean

        Inserta_MenuRol = False

        Try

            Ins.Init("menurol")
            Ins.Add("Idrol", Val(IdRod), "N")
            Ins.Add("IdMenu", IdMenu, "S")
            Ins.Add("visible", 1, "N")
            Ins.Add("user_agr", gUsuario.IdUsuario, "S")
            Ins.Add("fec_agr", Now, "D")
            BD.Xcute(Ins.SQL)

            Inserta_MenuRol = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function Actualiza_MenuRol(ByVal IdMenu As String, ByVal IdRol As Integer) As Boolean

        Actualiza_MenuRol = False

        Try

            Upd.Init("MenuRol")
            Upd.Add("visible", 1, "N")
            Upd.Where("IdMenu= '" & IdMenu & "'" & _
            " AND idrol = " & IdRol)
            BD.Xcute(Upd.SQL)

            Actualiza_MenuRol = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function Elimina_MenuRol(ByVal IdMenu As String, ByVal IdRol As Integer) As Boolean

        Elimina_MenuRol = False

        Try

            Upd.Init("menurol")
            Upd.Add("visible", 0, "N")
            Upd.Where("IdMenu LIKE '" & IdMenu & "%'" & _
            " AND IdRol= " & Val(IdRol))
            BD.Xcute(Upd.SQL)

            Elimina_MenuRol = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function


End Class
