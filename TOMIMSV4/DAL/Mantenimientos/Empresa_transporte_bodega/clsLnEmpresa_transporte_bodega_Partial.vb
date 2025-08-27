Imports System.Data.SqlClient

Partial Public Class clsLnEmpresa_transporte_bodega

    Public Shared Function GetAllByEmpresaTransporte(ByVal pIdEmpresaTransporte As Integer) As List(Of clsBeEmpresa_transporte_bodega)

        Dim lReturnList As New List(Of clsBeEmpresa_transporte_bodega)


        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim vSQL As String = "SELECT * FROM empresa_transporte_bodega WHERE IdEmpresaTransporte=@IdEmpresaTransporte"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresaTransporte", pIdEmpresaTransporte)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeEmpresa_transporte_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeEmpresa_transporte_bodega

                        Obj.IdAsignacion = CType(lRow("IdAsignacion"), Int32)

                        If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                            Obj.IdBodega = CType(lRow("IdBodega"), Int32)
                        End If

                        If lRow("IdEmpresaTransporte") IsNot DBNull.Value AndAlso lRow("IdEmpresaTransporte") IsNot Nothing Then
                            Obj.IdEmpresaTransporte = CType(lRow("IdEmpresaTransporte"), Int32)
                        End If

                        If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                            Obj.Activo = CType(lRow("activo"), Boolean)
                        End If

                        If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                            Obj.User_agr = CType(lRow("user_agr"), String)
                        End If

                        If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                            Obj.Fec_agr = CType(lRow("fec_agr"), DateTime)
                        End If

                        If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                            Obj.User_mod = CType(lRow("user_mod"), String)
                        End If

                        If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                            Obj.Fec_mod = CType(lRow("fec_mod"), DateTime)
                        End If

                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

        End Using

        Return lReturnList

    End Function

End Class
