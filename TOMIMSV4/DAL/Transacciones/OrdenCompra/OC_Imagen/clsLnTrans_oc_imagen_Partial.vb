Imports System.Data.SqlClient

Partial Public Class clsLnTrans_oc_imagen


    Public Shared Function GetByOrdenCompra(ByVal pIdOrdenCompraEnc) As List(Of clsBeTrans_oc_imagen)

        Dim lReturnList As New List(Of clsBeTrans_oc_imagen)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM Trans_oc_imagen WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_oc_imagen

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_oc_imagen

                            Cargar(Obj, lRow)

                            Obj.IsNew = False
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

    Public Shared Function Get_Imagenes_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_oc_imagen)

        Dim lReturnList As New List(Of clsBeTrans_oc_imagen)

        Try

            Dim vSQL As String = "SELECT * FROM Trans_oc_imagen WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_oc_imagen

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_oc_imagen
                        Cargar(Obj, lRow)
                        Obj.IsNew = False
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdOrdenCompraEnc As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdOrdenCompraImg),0) FROM trans_oc_imagen WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc), lConnection)
                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Sub Delete(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdOrdenCompraImg As Integer)

        Dim vSQL As String = "DELETE FROM trans_oc_imagen WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc AND IdOrdenCompraImg=@IdOrdenCompraImg"

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            '#HS 07112017 Quité query dentro de SqlCommand.
            Using lCommand As New SqlCommand(vSQL, lConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lCommand.Parameters.AddWithValue("@IdOrdenCompraImg", pIdOrdenCompraImg)

                lConnection.Open()
                lCommand.ExecuteNonQuery()
                lConnection.Close()

            End Using

        End Using

    End Sub

End Class
