Imports System.Data.SqlClient

Partial Public Class clsLnMontacarga_tipoFalla

    Public Shared Function ListarxFiltro(ByVal pIdEmpresa As Integer, ByVal pFiltro As String, ByVal pActivo As Boolean) As List(Of clsBeMontacarga_tipoFalla)

        Dim lReturnList As New List(Of clsBeMontacarga_tipoFalla)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM montacarga_tipoFalla WHERE 1>0  "


                If pActivo = True Then
                    vSQL += " AND activo=1"
                Else
                    vSQL += " AND activo=0"
                End If

                If String.IsNullOrEmpty(pFiltro) = False Then
                    'vSQL += String.Format(" AND (IdTipoFalla LIKE '%{0}%'", pFiltro)
                    'vSQL += String.Format(" OR Nombre LIKE '%{0}%')", pFiltro)
                    vSQL += " AND (IdTipoFalla LIKE '%@IdTipoFalla%'"
                    vSQL += " OR Nombre LIKE '%@Nombre%')"
                End If

                If pIdEmpresa <> 0 Then
                    vSQL += String.Format(" AND IdEmpresa={0}", pIdEmpresa)
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    If String.IsNullOrEmpty(pFiltro) = False Then
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoFalla", pFiltro)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pFiltro)
                    End If

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMontacarga_tipoFalla

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMontacarga_tipoFalla

                            Obj.IdTipoFalla = CType(lRow("IdTipoFalla"), String)

                            Obj.IdEmpresa = CType(lRow("IdEmpresa"), Int32)

                            If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("Nombre"), String)
                            End If

                            Obj.Activo = CType(lRow("Activo"), Int32)

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
