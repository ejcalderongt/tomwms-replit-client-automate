Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnMenu_sistema

    Public Shared Function Get_All_For_Menu(ByVal pIdRol As Integer) As DataTable

        Get_All_For_Menu = Nothing

        Try

            Const sp As String = "SELECT a.IDMENU, a.TITULO, a.NOMBRE_LGCO, a.NIVEL, a.PADRE, b.VISIBLE, 
                                    CASE WHEN A.IDMENU IN
                                    (SELECT padre
                                    FROM  MENU_SISTEMA
                                    WHERE IDMENU LIKE '6%') THEN 1 ELSE 0 END AS Tiene_Hijos
                                    FROM  MENU_SISTEMA a INNER JOIN
                                    menu_rol AS b ON a.IDMENU = b.IDMENU
                                    WHERE (b.IdRol = @IdRol) AND (a.IDMENU LIKE '6%') "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRol", pIdRol)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_For_Menu = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdRol(ByVal pIdRol As Integer) As List(Of clsBeMenu_sistema)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeMenu_sistema)

            Const sp As String = "select  a.*, b.visible from menu_sistema a, menu_rol b " &
                " WHERE a.idmenu = b.idmenu  " &
                " AND b.IdRol = @IdRol AND b.activo = 1
                  ORDER BY a.IdMenu, a.padre"

            Dim cmd As New SqlCommand(sp, lConnection, ltransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdRol", pIdRol)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMenuSistema_rol As New clsBeMenu_sistema

            For Each dr As DataRow In dt.Rows

                vBeMenuSistema_rol = New clsBeMenu_sistema
                Cargar(vBeMenuSistema_rol, dr)
                vBeMenuSistema_rol.Visible = IIf(IsDBNull(dr.Item("visible")), False, dr.Item("visible"))
                lReturnList.Add(vBeMenuSistema_rol)

            Next

            ltransaction.Commit()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            If Not ltransaction Is Nothing Then ltransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetAllPermisos() As List(Of clsBeMenu_sistema)

        Try

            Dim lReturnList As New List(Of clsBeMenu_sistema)
            Const sp As String = "select a.IdMenu,b.*,a.visible from menu_rol as a, menu_sistema as b  where b.IdMenu = a.IdMenu"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMenu_sistema As New clsBeMenu_sistema

            For Each dr As DataRow In dt.Rows

                vBeMenu_sistema = New clsBeMenu_sistema
                Cargar(vBeMenu_sistema, dr)
                vBeMenu_sistema.Visible = IIf(IsDBNull(dr.Item("visible")), False, dr.Item("visible"))
                lReturnList.Add(vBeMenu_sistema)

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

End Class
