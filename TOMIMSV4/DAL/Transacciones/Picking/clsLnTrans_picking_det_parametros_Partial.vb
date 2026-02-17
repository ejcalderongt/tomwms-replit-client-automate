Imports System.Data.SqlClient
Imports System.Reflection


Partial Public Class clsLnTrans_picking_det_parametros

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdParametroPicking),0) FROM trans_picking_det_parametros"

            Using lCommand As New SqlCommand(vSQL, pConnection)

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

    Public Shared Function Get_All_By_Picking_Detalle(ByVal pIdPickingDet As Integer) As List(Of clsBeTrans_picking_det_parametros)

        Dim lReturnList As New List(Of clsBeTrans_picking_det_parametros)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_picking_det_parametros WHERE IdPickingDet=@IdPickingDet"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingDet", pIdPickingDet)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_picking_det_parametros

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows
                            Obj = New clsBeTrans_picking_det_parametros

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

    Public Shared Function Get_All_By_IdPickingDet(ByVal pIdPickingDet As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_det_parametros)

        Dim lReturnList As New List(Of clsBeTrans_picking_det_parametros)

        Try

            Dim vSQL As String = "SELECT * FROM trans_picking_det_parametros WHERE IdPickingDet=@IdPickingDet"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingDet", pIdPickingDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_det_parametros

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_det_parametros
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

    ''' <summary>
    '''  Creada por Carlos Manuel - Guarda y actualiza el proceso de Parametros    
    ''' </summary>
    ''' <param name="lParametros"></param>
    Public Shared Sub Guardar(ByVal lParametros As List(Of clsBeTrans_picking_det_parametros))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        ''#EJC20171022_0812PM:: Actualizada por Optimizada.

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim MaxIDS As Integer = MaxID(lConnection, lTransaction)

            For Each Obj As clsBeTrans_picking_det_parametros In lParametros

                If Obj.IsNew Then
                    MaxIDS += 1
                    Obj.IdParametroPicking = MaxIDS
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} Transacción: {1}", ex.Message))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Sub Guardar_Lista_Parametros(ByVal lParametros As List(Of clsBeTrans_picking_det_parametros),
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction)

        ''#EJC20171122_0812PM:: Actualizada por Optimizada, se adicionó transaccionalidad remota.

        Try

            Dim MaxIDS As Integer = MaxID(lConnection, lTransaction)

            For Each Obj As clsBeTrans_picking_det_parametros In lParametros

                If Obj.IsNew Then
                    MaxIDS += 1
                    Obj.IdParametroPicking = MaxIDS
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If

            Next

        Catch ex As Exception
            Throw New Exception(String.Format("{0} Transacción: ", ex.Message))
        End Try

    End Sub

    Public Shared Sub Guarda_Trans_picking_parametros(ByVal pListObjPickingDetParametros As List(Of clsBeTrans_picking_det_parametros),
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Try

            Dim lMaxIdDet As Integer = MaxID(lConnection, lTransaction)

            For Each Obj As clsBeTrans_picking_det_parametros In pListObjPickingDetParametros

                If Obj.IsNew Then
                    lMaxIdDet += 1
                    Obj.IdParametroPicking = lMaxIdDet
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If

            Next

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar_Parametros_Stock_Para_Picking(ByVal pIdStock As Integer,
                                                                  ByVal pIdPickingDet As Integer,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As Boolean

        Insertar_Parametros_Stock_Para_Picking = False

        Try

            Dim BeParametros As New clsBeTrans_picking_det_parametros
            Dim lStockP As New List(Of clsBeStock_parametro)

            lStockP = clsLnStock_parametro.Get_All_By_IdStock(pIdStock, lConnection, lTransaction)

            If Not lStockP Is Nothing Then

                Dim MaxIDS As Integer = MaxID(lConnection, lTransaction) + 1

                For Each Obj As clsBeStock_parametro In lStockP

                    BeParametros = New clsBeTrans_picking_det_parametros
                    BeParametros.IdParametroPicking = MaxIDS
                    BeParametros.IdPickingDet = pIdPickingDet
                    BeParametros.IdProductoParametro = Obj.IdProductoParametro
                    BeParametros.Valor_texto = Obj.Valor_texto
                    BeParametros.Valor_numerico = Obj.Valor_numerico
                    BeParametros.Valor_logico = Obj.Valor_logico
                    BeParametros.Valor_fecha = Obj.Valor_fecha
                    BeParametros.Fec_agr = Now
                    BeParametros.User_agr = Obj.User_agr
                    BeParametros.IsNew = True

                    Insertar(BeParametros, lConnection, lTransaction)

                    MaxIDS += 1

                Next

            End If

            Insertar_Parametros_Stock_Para_Picking = True

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pIdPickingDet:=pIdPickingDet, pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_det_parametros)

        Dim lReturnList As New List(Of clsBeTrans_picking_det_parametros)

        Try

            Dim vSQL As String = "SELECT * FROM trans_picking_det_parametros pp
                                  JOIN trans_picking_det pd on pd.IdPickingEnc = pp.IdPickingDet 
                                  WHERE pd.IdPickingEnc=@IdPickingEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_det_parametros

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_det_parametros
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

End Class
