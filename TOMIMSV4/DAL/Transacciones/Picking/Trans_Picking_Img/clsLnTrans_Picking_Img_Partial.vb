Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_Picking_Img
    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = "SELECT ISNULL(Max(IdImagen),0) FROM trans_picking_img "

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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

    Public Shared Sub Guardar_Imagen_Verificacion(ByVal pIdPedidoDet As Integer,
                                                  ByVal imagen_verificacion As Byte(),
                                                  Optional ByVal pConnection As SqlConnection = Nothing,
                                                  Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim BePickingDet As New clsBeTrans_picking_det
        Dim BePickingImg As New clsBeTrans_picking_img
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            If Not Es_Transaccion_Remota Then
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            If Es_Transaccion_Remota Then

                BePickingDet = clsLnTrans_picking_det.GetSingle(pIdPedidoDet, pConnection, pTransaction)

                BePickingImg.IdImagen = MaxID(pConnection, pTransaction) + 1

            Else

                BePickingDet = clsLnTrans_picking_det.GetSingle(pIdPedidoDet, lConnection, lTransaction)

                BePickingImg.IdImagen = MaxID(lConnection, lTransaction) + 1

            End If

            BePickingImg.Imagen = imagen_verificacion
            BePickingImg.IdPedidoDet = BePickingDet.IdPedidoDet
            BePickingImg.IdPickingDet = BePickingDet.IdPickingDet
            BePickingImg.IdPickingEnc = BePickingDet.IdPickingEnc
            BePickingImg.IdPedidoEnc = BePickingDet.IdPedidoEnc

            If Es_Transaccion_Remota Then
                Insertar(BePickingImg, pConnection, pTransaction)
            Else
                Insertar(BePickingImg, lConnection, lTransaction)
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

    Public Shared Function Get_All_Imagen_By_IdPedidoDet(ByVal pIdPedidoDet As Integer) As List(Of clsBeTrans_picking_img)

        Dim lReturnList As New List(Of clsBeTrans_picking_img)

        Try
            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM Trans_picking_img WHERE IdPedidoDet=@IdPedidoDet"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_img

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_img
                                Cargar(Obj, lRow)

                                Obj.IsNew = False
                                lReturnList.Add(Obj)

                            Next

                        End If

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

    '#GT23052025: obtener imagenes del picking para enviar a la nube.
    Public Shared Function Get_All_Imagen_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_picking_img)


        Get_All_Imagen_By_IdPedidoEnc = Nothing
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            ' Comando SQL
            Dim vSQL As String = "SELECT * FROM Trans_picking_img WHERE IdPedidoEnc=@pIdPedidoEnc"


            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction
            End If

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(vSQL, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                cmd = New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            End If

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@pIdPedidoEnc", pIdPedidoEnc))


            Dim dt As New DataTable
            dad.Fill(dt)

            ' Procesamos los resultados si los hay
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim pTransPicking_Img As clsBeTrans_picking_img
                Get_All_Imagen_By_IdPedidoEnc = New List(Of clsBeTrans_picking_img)()

                For Each lRow As DataRow In dt.Rows
                    pTransPicking_Img = New clsBeTrans_picking_img
                    Cargar(pTransPicking_Img, lRow)
                    pTransPicking_Img.IsNew = False
                    Get_All_Imagen_By_IdPedidoEnc.Add(pTransPicking_Img)
                Next
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


End Class
