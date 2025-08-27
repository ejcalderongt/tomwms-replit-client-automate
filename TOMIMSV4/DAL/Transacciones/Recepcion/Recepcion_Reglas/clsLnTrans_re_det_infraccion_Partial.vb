Imports System.Data.SqlClient

Partial Public Class clsLnTrans_re_det_infraccion


    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdRecepcionDetInfraccion),0) FROM trans_re_det_infraccion"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
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


    Public Shared Function Exists(ByVal pIdReglaPropietarioEnc As Integer,
                                  ByVal pIdPresentacion As Integer,
                                  ByVal pIdProductoBodega As Integer,
                                  ByVal pConnection As SqlConnection,
                                  ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_re_det_infraccion " &
                " WHERE IdReglaPropietarioEnc=@IdReglaPropietarioEnc " &
                " And IdPresentacion=@IdPresentacion And IdProductoBodega=@IdProductoBodega "

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdReglaPropietarioEnc", pIdReglaPropietarioEnc)
                lCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Sub Guardar(ByVal pListObjRD As List(Of clsBeTrans_re_det_infraccion))

        Dim ObjD As New clsLnTrans_re_det_infraccion()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMaxIdDet As Integer = MaxID(lConnection, lTransaction)
            For Each ObjI As clsBeTrans_re_det_infraccion In pListObjRD
                If Exists(ObjI.IdReglaPropietarioEnc, ObjI.IdPresentacion, ObjI.IdProductoBodega, lConnection, lTransaction) = False Then
                    If ObjI.IsNew Then
                        lMaxIdDet += 1
                        ObjI.IdRecepcionDetInfraccion = lMaxIdDet
                        ObjD.Insertar(ObjI, lConnection, lTransaction)
                    End If
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            lConnection.Close()
        End Try

    End Sub

End Class
