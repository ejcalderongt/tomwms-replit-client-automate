Imports System.Data.SqlClient

Partial Public Class clsLnMenu_rol

    'Public Shared Sub HabilitarMenuRol(ByVal pIdRol As Integer, ByVal prbMain As RibbonControl)

    '    Dim mCurrentItem As BarItem
    '    Dim mBarSubItem As BarSubItem
    '    Dim mSubLink As BarItemLink

    '    ' Este procedimiento verifica que cada item del menu 
    '    ' esté disponible para el usuario que se ha logeado
    '    Dim I As Integer
    '    Dim vMenu As String
    '    Dim vHabilitar As Boolean
    '    Dim lMenuRol As New List(Of clsBeMenu_sistema)

    '    Try

    '        prbMain.Enabled = True

    '        lMenuRol = clsLnMenu_sistema.GetAllByIdRol(pIdRol)

    '        For Each OpMenu As clsBeMenu_sistema In lMenuRol.FindAll(Function(x) x.Nivel = 1)
    '               For Each currentPage As RibbonPage In prbMain.Pages
    '                If currentPage.Text = OpMenu.Titulo Then
    '                    currentPage.Visible = OpMenu.Visible
    '                    currentPage.Ribbon.Enabled = OpMenu.Visible
    '                End If
    '            Next
    '        Next

    '        ' Nivel 2
    '        For Each OpMenu As clsBeMenu_sistema In lMenuRol.FindAll(Function(x) x.Nivel = 2)

    '            vMenu$ = OpMenu.Titulo
    '            vHabilitar = OpMenu.Visible

    '            For Each currentPage As RibbonPage In prbMain.Pages

    '                For Each currentGroup As RibbonPageGroup In currentPage.Groups
    '                    If currentPage.Text = vMenu$ Then
    '                        currentGroup.Visible = vHabilitar
    '                        currentGroup.Enabled = True
    '                    End If
    '                Next currentGroup

    '            Next currentPage

    '        Next

    '        ' Nivel 3
    '        For Each OpMenu As clsBeMenu_sistema In lMenuRol.FindAll(Function(x) x.Nivel = 3)

    '            vMenu$ = OpMenu.Titulo
    '            vHabilitar = OpMenu.Visible

    '            For Each currentPage As RibbonPage In prbMain.Pages

    '                For Each currentGroup As RibbonPageGroup In currentPage.Groups

    '                    For Each currenLink As BarItemLink In currentGroup.ItemLinks
    '                        If currenLink.Item.Caption = vMenu$ Then
    '                            currenLink.Visible = vHabilitar
    '                            currenLink.Item.Enabled = vHabilitar
    '                        End If
    '                    Next

    '                Next

    '            Next

    '        Next


    '        ' Nivel 4
    '        For Each OpMenu As clsBeMenu_sistema In lMenuRol.FindAll(Function(x) x.Nivel = 4)

    '            vMenu$ = OpMenu.titulo
    '            vHabilitar = OpMenu.Visible

    '            For Each currentPage As RibbonPage In prbMain.Pages

    '                For Each currentGroup As RibbonPageGroup In currentPage.Groups

    '                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

    '                        mCurrentItem = currenLink.Item

    '                        If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarSubItem" Then

    '                            mBarSubItem = mCurrentItem

    '                            For Each mSubLink In mBarSubItem.ItemLinks

    '                                If mSubLink.Item.Caption = vMenu$ Then
    '                                    mSubLink.Visible = vHabilitar
    '                                    mSubLink.Item.Enabled = vHabilitar
    '                                End If

    '                            Next

    '                        End If

    '                    Next

    '                Next

    '            Next

    '        Next            

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    Public Shared Function Permiso_Funcionalidad(ByVal pIdMenu As String,
                                               ByVal pIdRol As Integer) As Integer

        Try

            Dim lpermiso As Boolean = False

            Dim vSQL As String = "SELECT IdMenu FROM menu_rol 
                                  WHERE IdMenu = @IdMenu AND IdRol = @IdRol AND activo = 1 AND visible = 1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdMenu", pIdMenu)
                        lCommand.Parameters.AddWithValue("@IdRol", pIdRol)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lpermiso = True
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lpermiso

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class
