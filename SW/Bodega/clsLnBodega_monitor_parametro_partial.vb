Partial Class clsLnBodega_monitor_parametro


    Public Shared Function GetAllBodegasActivas() As List(Of clsBeBodega_monitor_parametro)

        Dim lReturnList As New List(Of clsBeBodega_monitor_parametro)

        Try

            'Validacion y estandarización de los datos
            Using lCnn As New SqlClient.SqlConnection(BD.CadenaConexion)

                Dim lSQL As String = String.Format("SELECT bmp.IdBodega,e.nombre,CAST(e.imagen AS VARBINARY(8000)) AS Imagen " _
                                                 & "FROM bodega_monitor_parametro AS bmp INNER JOIN bodega AS b ON bmp.IdBodega = b.IdBodega " _
                                                 & "INNER JOIN empresa AS e ON b.IdEmpresa = e.IdEmpresa")

                'Acceso a los datos.
                Using lDTA As New SqlClient.SqlDataAdapter(lSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeBodega_monitor_parametro

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeBodega_monitor_parametro

                            Obj.IdBodega = CType(lRow("IdBodega"), System.Int32)
                            Obj.NombreEmpresa = CType(lRow("Nombre"), System.String)
                            Obj.ImagenEmpresa = CType(lRow("Imagen"), System.Byte())
                           
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", Reflection.MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try





    End Function

End Class
