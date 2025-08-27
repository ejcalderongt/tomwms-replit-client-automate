Imports System.Data.SqlClient

Partial Public Class clsLnMontacarga

    Public Shared Function GetMontaCargas(ByVal pIdEmpresa As Integer, ByVal pFiltro As String) As List(Of clsBeMontacarga)

        Dim lReturnList As New List(Of clsBeMontacarga)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM MontaCarga WHERE 1>0  "


                If String.IsNullOrEmpty(pFiltro) = False Then

                    '#HS20171023_1630pm: Quité String.Format.
                    'vSQL += String.Format(" AND (IdMontaCarga LIKE '%{0}%'", pFiltro)
                    'vSQL += String.Format(" OR Nombre LIKE '%{0}%'", pFiltro)
                    'vSQL += String.Format(" OR modelo LIKE '%{0}%'", pFiltro)
                    'vSQL += String.Format(" OR serie LIKE '%{0}%')", pFiltro)

                    vSQL += " AND IdMontaCarga LIKE '%@IdMontaCarga%'"
                    vSQL += " OR Nombre LIKE '%@Nombre%'"
                    vSQL += " OR modelo LIKE '%@modelo%'"
                    vSQL += " OR serie LIKE '%@serie%'"
                End If

                If pIdEmpresa <> 0 Then
                    'vSQL += String.Format(" AND IdEmpresa={0}", pIdEmpresa)
                    vSQL += " AND IdEmpresa=@IdEmpresa"
                End If
                ' MsgBox(lSQL)

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    If String.IsNullOrEmpty(pFiltro) = False Then
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdMontaCarga", pFiltro)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pFiltro)
                        lDTA.SelectCommand.Parameters.AddWithValue("@modelo", pFiltro)
                        lDTA.SelectCommand.Parameters.AddWithValue("@serie", pFiltro)
                    Else
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                    End If


                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMontacarga

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMontacarga

                            Obj.IdMontacarga = CType(lRow("IdMontacarga"), Int32)

                            Obj.IdEmpresa = CType(lRow("IdEmpresa"), Int32)

                            If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("Nombre"), String)
                            End If

                            If lRow("Modelo") IsNot DBNull.Value AndAlso lRow("Modelo") IsNot Nothing Then
                                Obj.Modelo = CType(lRow("Modelo"), String)
                            End If

                            If lRow("Serie") IsNot DBNull.Value AndAlso lRow("Serie") IsNot Nothing Then
                                Obj.Serie = CType(lRow("Serie"), String)
                            End If

                            If lRow("capacidad_basica") IsNot DBNull.Value AndAlso lRow("capacidad_basica") IsNot Nothing Then
                                Obj.Capacidad_basica = CType(lRow("capacidad_basica"), Double)
                            End If

                            If lRow("desplazamiento_motor") IsNot DBNull.Value AndAlso lRow("desplazamiento_motor") IsNot Nothing Then
                                Obj.Desplazamiento_motor = CType(lRow("desplazamiento_motor"), Double)
                            End If

                            If lRow("tipo_combustible") IsNot DBNull.Value AndAlso lRow("tipo_combustible") IsNot Nothing Then
                                Obj.Tipo_combustible = CType(lRow("tipo_combustible"), String)
                            End If

                            If lRow("tipo_montacarga") IsNot DBNull.Value AndAlso lRow("tipo_montacarga") IsNot Nothing Then
                                Obj.Tipo_montacarga = CType(lRow("tipo_montacarga"), String)
                            End If

                            If lRow("fecha_compra") IsNot DBNull.Value AndAlso lRow("fecha_compra") IsNot Nothing Then
                                Obj.Fecha_compra = CType(lRow("fecha_compra"), DateTime)
                            End If

                            If lRow("fecha_inicio_operaciones") IsNot DBNull.Value AndAlso lRow("fecha_inicio_operaciones") IsNot Nothing Then
                                Obj.Fecha_inicio_operaciones = CType(lRow("fecha_inicio_operaciones"), DateTime)
                            End If

                            If lRow("proximo_mantenimiento") IsNot DBNull.Value AndAlso lRow("proximo_mantenimiento") IsNot Nothing Then
                                Obj.Proximo_mantenimiento = CType(lRow("proximo_mantenimiento"), DateTime)
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

End Class
