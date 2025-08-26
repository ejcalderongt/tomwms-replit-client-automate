Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnUsuario_bodega

    'Public Function MaxID() As Integer

    '    Try

    '        Dim lMax As Integer = 0
    '        
    '        Using lConnection As New SqlClient.SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '            
    '            Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdUsuarioBodega),0) FROM usuario_bodega"), lConnection)
    '                lCommand.CommandType = CommandType.Text
    '                
    '                lConnection.Open()
    '                Dim lReturnValue As Object = lCommand.ExecuteScalar()
    '                lConnection.Close()
    '                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
    '                    lMax = CInt(lReturnValue) + 1
    '                End If
    '            End Using
    '        End Using

    '        Return lMax

    '    Catch ex As Exception
    '        Throw ex
    '    End Try





    'End Function

    '#CKFK 20210823 Le agregué la transacción a la función
    Public Shared Sub obtenerPermiso(ByRef oBeUsuario_bodega As clsBeUsuario_bodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim dt As New DataTable

            Const SQLQuery = "select * from usuario_bodega where IdUsuario=@IDUSUARIO AND IdBodega=@IDBODEGA and Activo=1"

            Using lCommand As New SqlCommand(SQLQuery, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text

                Using dad As New SqlDataAdapter(lCommand)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeUsuario_bodega.IdUsuario))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", oBeUsuario_bodega.IdBodega))
                    dad.Fill(dt)

                    '#CKFK 20210816 Agregué validacion para cuando el usuario superior sea nulo el valor por defecto sea 0 y no cadena vacia
                    If (dt.Rows.Count > 0) Then
                        oBeUsuario_bodega.IdUsuarioBodega = IIf(IsDBNull(dt.Rows(0).Item("idUsuarioBodega")), 0, dt.Rows(0).Item("idUsuarioBodega"))
                        oBeUsuario_bodega.IdUsuarioSuperior = IIf(IsDBNull(dt.Rows(0).Item("idUsuarioSuperior")), 0, dt.Rows(0).Item("idUsuarioSuperior"))
                        oBeUsuario_bodega.IdRol = IIf(IsDBNull(dt.Rows(0).Item("idRol")), "", dt.Rows(0).Item("idRol"))
                        oBeUsuario_bodega.Activo = IIf(IsDBNull(dt.Rows(0).Item("Activo")), "", dt.Rows(0).Item("Activo"))
                        oBeUsuario_bodega.User_agr = IIf(IsDBNull(dt.Rows(0).Item("User_agr")), "", dt.Rows(0).Item("User_agr"))
                        oBeUsuario_bodega.Fec_agr = IIf(IsDBNull(dt.Rows(0).Item("Fec_agr")), "", dt.Rows(0).Item("Fec_agr"))
                        oBeUsuario_bodega.User_mod = IIf(IsDBNull(dt.Rows(0).Item("User_mod")), "", dt.Rows(0).Item("User_mod"))
                        oBeUsuario_bodega.Fec_mod = IIf(IsDBNull(dt.Rows(0).Item("Fec_mod")), "", dt.Rows(0).Item("Fec_mod"))
                    End If

                    lTransaction.Commit()

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), vMsgError))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Get_All_By_IdUsuario(ByVal pIdUsuario As Integer) As List(Of clsBeUsuario_bodega)

        Dim lReturnList As New List(Of clsBeUsuario_bodega)

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim vSQL As String = "SELECT * FROM usuario_bodega WHERE IdUsuario=@IdUsuario"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeUsuario_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeUsuario_bodega
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

        End Using

        Return lReturnList

    End Function

    Public Shared Function Get_All_By_IdUsuario(ByVal pIdUsuario As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As List(Of clsBeUsuario_bodega)

        Dim lReturnList As New List(Of clsBeUsuario_bodega)

        Try

            Dim vSQL As String = "SELECT * FROM usuario_bodega WHERE IdUsuario=@IdUsuario"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeUsuario_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeUsuario_bodega
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Throw ex
        End Try
    End Function

End Class
