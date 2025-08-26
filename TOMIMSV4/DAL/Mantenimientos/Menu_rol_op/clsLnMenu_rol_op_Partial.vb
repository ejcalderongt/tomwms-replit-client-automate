Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnMenu_rol_op
    Implements IDisposable

    Public Shared Function Get_List_Menu_By_IdRolOperador(ByVal IdRolOperador As Integer) As List(Of clsBeMenu_rol_op)

        Try

            Get_List_Menu_By_IdRolOperador = Nothing

            Dim sp As String = " SELECT menu_rol_op.IdRolOperador, menu_rol_op.IdMenuSistemaOP, menu_sistema_op.Nombre, " &
            " menu_rol_op.visible, menu_rol_op.activo,  " &
            " menu_sistema_op.Padre, menu_sistema_op.Nivel,menu_sistema_op.Posicion, menu_sistema_op.IdTipoTarea  " &
            " FROM menu_rol_op INNER JOIN " &
            " menu_sistema_op ON menu_rol_op.IdMenuSistemaOP = menu_sistema_op.IdMenuSistemaOP " &
            " WHERE (IdRolOperador = @IdRolOperador) " &
            " AND Padre >0 " &
            " AND Activo = 1 " &
            " And Visible = 1 "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDROLOPERADOR", IdRolOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim lReturnList As New List(Of clsBeMenu_rol_op)
            Dim MenuRol As New clsBeMenu_rol_op

            For Each R As DataRow In dt.Rows

                MenuRol = New clsBeMenu_rol_op
                MenuRol.IdMenuSistemaOP = R("IdMenuSistemaOP")
                MenuRol.MenuSistemaOp.Nombre = R("Nombre")
                MenuRol.MenuSistemaOp.Posicion = R("Posicion")
                MenuRol.MenuSistemaOp.IdTipoTarea = R("IdTipoTarea")
                lReturnList.Add(MenuRol)

            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_List_Menu_By_IdRolOperador(ByVal IdRolOperador As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As List(Of clsBeMenu_rol_op)

        Try

            Get_List_Menu_By_IdRolOperador = Nothing

            Dim sp As String = " SELECT menu_rol_op.IdRolOperador, menu_rol_op.IdMenuSistemaOP, menu_sistema_op.Nombre, 
             menu_rol_op.visible, menu_rol_op.activo,  
             menu_sistema_op.Padre, menu_sistema_op.Nivel,menu_sistema_op.Posicion, menu_sistema_op.IdTipoTarea  
             FROM menu_rol_op INNER JOIN 
             menu_sistema_op ON menu_rol_op.IdMenuSistemaOP = menu_sistema_op.IdMenuSistemaOP 
             WHERE (IdRolOperador = @IdRolOperador) 
             AND Padre >0 
             AND Activo = 1 
             And Visible = 1 Order By menu_sistema_op.Posicion"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDROLOPERADOR", IdRolOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim lReturnList As New List(Of clsBeMenu_rol_op)
            Dim MenuRol As New clsBeMenu_rol_op

            For Each R As DataRow In dt.Rows

                MenuRol = New clsBeMenu_rol_op
                MenuRol.IdMenuSistemaOP = R("IdMenuSistemaOP")
                MenuRol.MenuSistemaOp.Nombre = R("Nombre")
                MenuRol.MenuSistemaOp.Posicion = R("Posicion")
                MenuRol.MenuSistemaOp.IdTipoTarea = R("IdTipoTarea")
                lReturnList.Add(MenuRol)

            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
