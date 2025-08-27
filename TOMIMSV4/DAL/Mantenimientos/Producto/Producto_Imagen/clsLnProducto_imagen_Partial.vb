Imports System.Data.SqlClient

Partial Public Class clsLnProducto_imagen

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoImagen),0) FROM Producto_imagen "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_imagen)

        Dim lReturnList As New List(Of clsBeProducto_imagen)

        Try

            Const sp As String = "SELECT * FROM Producto_imagen WHERE IdProducto =@IdProducto"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeProducto_imagen As New clsBeProducto_imagen

                        For Each dr As DataRow In lDataTable.Rows
                            vBeProducto_imagen = New clsBeProducto_imagen()
                            Cargar(vBeProducto_imagen, dr)
                            lReturnList.Add(vBeProducto_imagen)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar_Imagen(ByVal pBeProductoImagen As clsBeProducto_imagen,
                                          Optional ByVal pConnection As SqlConnection = Nothing,
                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean
        Guardar_Imagen = False

        Try
            Dim exito As Integer = 0

            pBeProductoImagen.IdProductoImagen = MaxID() + 1

            exito = Insertar(pBeProductoImagen)

            If exito > 0 Then
                Guardar_Imagen = True
            End If

            Return Guardar_Imagen

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
