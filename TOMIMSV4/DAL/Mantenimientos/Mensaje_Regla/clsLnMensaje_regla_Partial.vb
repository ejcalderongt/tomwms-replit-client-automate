Imports System.Data.SqlClient

Partial Public Class clsLnMensaje_regla

    Public Shared Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeMensaje_regla)

        Dim lReturnList As New List(Of clsBeMensaje_regla)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT IdMensajeRegla, Nombre FROM mensaje_regla WHERE 1 > 0 "

                If pActivo Then
                    vSQL += " AND activo=1 "
                Else
                    vSQL += " AND activo=0 "
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMensaje_regla

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMensaje_regla

                            Obj.IdMensajeRegla = CType(lRow("IdMensajeRegla"), System.Int32)

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

    Public Shared Function GetSingle(ByVal pIdMensajeRegla As Integer) As clsBeMensaje_regla

        GetSingle = Nothing

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM mensaje_regla WHERE IdMensajeRegla=" & pIdMensajeRegla


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDataTable.Rows(0)

                        Dim Obj As New clsBeMensaje_regla

                        Obj.IdMensajeRegla = CType(lRow("IdMensajeRegla"), System.Int32)

                        If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                            Obj.Nombre = CType(lRow("Nombre"), System.String)
                        End If
                        If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                            Obj.User_agr = CType(lRow("user_agr"), System.String)
                        End If
                        If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                            Obj.Fec_agr = CType(lRow("fec_agr"), System.DateTime)
                        End If
                        If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                            Obj.User_mod = CType(lRow("user_mod"), System.String)
                        End If
                        If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                            Obj.Fec_mod = CType(lRow("fec_mod"), System.DateTime)
                        End If
                        If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                            Obj.Activo = CType(lRow("activo"), System.Boolean)
                        End If

                        Obj.IsNew = False

                        Return Obj

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT14112025: cargar mensajes por proceso, no aplica para las reglas de recepción
    Public Shared Function GetAll_By_IdProceso(ByVal IdReglaRecepcion As Integer) As List(Of clsBeMensaje_regla)

        GetAll_By_IdProceso = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM mensaje_regla WHERE IdReglaRecepcion=" & IdReglaRecepcion

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                        GetAll_By_IdProceso = New List(Of clsBeMensaje_regla)

                        For Each lRow As DataRow In lDataTable.Rows
                            Dim Obj As New clsBeMensaje_regla
                            Cargar(Obj, lRow)
                            GetAll_By_IdProceso.Add(Obj)
                        Next

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class
