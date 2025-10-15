Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_re_img

    Public Shared Function GetByOrdenCompraRecepcion(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeTrans_re_img)

        Dim lReturnList As New List(Of clsBeTrans_re_img)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM Trans_re_img WHERE IdRecepcionEnc=@IdRecepcionEnc"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_re_img

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows
                            Obj = New clsBeTrans_re_img

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

    Public Shared Function Get_Detalle_Imagenes_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_img)

        Get_Detalle_Imagenes_By_IdRecepcionEnc = Nothing

        Dim lReturnList As New List(Of clsBeTrans_re_img)

        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_img WHERE IdRecepcionEnc=@IdRecepcionEnc"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_re_img

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_re_img
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
    Public Shared Function Get_All_Imagen_Recepcion_By_IdRecepcion(ByVal pIdRecepcion As Integer) As List(Of clsBeTrans_re_img)

        Dim lReturnList As New List(Of clsBeTrans_re_img)

        Try
            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    lReturnList = Get_Detalle_Imagenes_By_IdRecepcionEnc(pIdRecepcion, lConnection, lTransaction)

                    lTransaction.Commit()

                End Using

                lConnection.Close()


            End Using

            Return lReturnList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function MaxID(ByVal pIdRecepcionEnc As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdFoto),0) FROM trans_re_img WHERE IdRecepcionEnc={0}", pIdRecepcionEnc), lConnection)
                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Sub Delete(ByVal pIdRecepcionEnc As Integer, ByVal pIdImagen As Integer)

        Dim vSQL As String = "DELETE FROM trans_re_img WHERE IdRecepcionEnc=@IdRecepcionEnc AND IdImagen=@IdImagen"

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            '#HS 07112017 Quité query dentro de SqlCommand.
            Using lCommand As New SqlCommand(vSQL, lConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@pIdRecepcionEnc", pIdRecepcionEnc)
                lCommand.Parameters.AddWithValue("@IdImagen", pIdImagen)

                lConnection.Open()
                lCommand.ExecuteNonQuery()
                lConnection.Close()

            End Using

        End Using

    End Sub

    Public Shared Sub Guarda_Trans_Re_Img(ByVal IdRecepcionEnc As Integer,
                                            ByVal pListRecImg As List(Of clsBeTrans_re_img),
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)
        Try

            If Not pListRecImg Is Nothing Then

                For Each Obj As clsBeTrans_re_img In pListRecImg
                    If Obj.IsNew Then
                        Obj.IdRecepcionEnc = IdRecepcionEnc
                        Insertar(Obj, lConnection, lTransaction)
                    Else
                        Actualizar(Obj, lConnection, lTransaction)
                    End If
                Next

            End If

        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 pIdRecEnc:=IdRecepcionEnc,
                                                 pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Sub

    Public Shared Sub Guardar_Imagen_Recepcion(ByVal pIdRecepcionEnc As Integer, ByVal imagen_recepcion As Byte(),
                                               Optional ByVal pConnection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeRecepcionImg As New clsBeTrans_re_img
        Dim vMaxId As Integer

        Try

            If Es_Transaccion_Remota Then
                vMaxId = MaxID(pIdRecepcionEnc, pConnection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                vMaxId = MaxID(pIdRecepcionEnc, lConnection, lTransaction)
            End If

            BeRecepcionImg.IdImagen = vMaxId + 1
            BeRecepcionImg.Imagen = imagen_recepcion
            BeRecepcionImg.IdRecepcionEnc = pIdRecepcionEnc

            If Es_Transaccion_Remota Then
                Insertar(BeRecepcionImg, pConnection, pTransaction)
            Else
                Insertar(BeRecepcionImg, lConnection, lTransaction)
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then
                If Not lTransaction Is Nothing Then
                    lTransaction.Rollback()
                End If
            End If
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
        End Try

    End Sub

    Public Shared Function MaxID(ByVal pIdRecepcionEnc As Integer,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = "SELECT ISNULL(Max(IdImagen),0) FROM trans_re_img WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                lCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

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


End Class
