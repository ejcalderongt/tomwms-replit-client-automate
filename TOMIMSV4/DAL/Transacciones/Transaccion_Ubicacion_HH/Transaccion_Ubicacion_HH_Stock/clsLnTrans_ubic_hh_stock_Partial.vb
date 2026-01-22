Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_ubic_hh_stock

    Public Shared Function MaxID(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdStockTransUbicHHDet),0) FROM Trans_ubic_hh_stock"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdTareaUbicacionEnc(ByVal IdTareaUbicacion As Integer) As List(Of clsBeTrans_ubic_hh_stock)

        Try

            Dim lReturnList As New List(Of clsBeTrans_ubic_hh_stock)
            Const sp As String = "SELECT * FROM Trans_ubic_hh_stock WHERE IdTareaUbicacionEnc= @IdTareaUbicacionEnc "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", IdTareaUbicacion)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ubic_hh_stock As New clsBeTrans_ubic_hh_stock

            For Each dr As DataRow In dt.Rows

                vBeTrans_ubic_hh_stock = New clsBeTrans_ubic_hh_stock
                Cargar(vBeTrans_ubic_hh_stock, dr)
                vBeTrans_ubic_hh_stock.IsNew = False
                lReturnList.Add(vBeTrans_ubic_hh_stock)

            Next

            Return lReturnList

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdTareaUbicacionEnc:=IdTareaUbicacion)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdTareaUbicacionEnc(ByVal IdTareaUbicacion As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_ubic_hh_stock)

        Try

            Dim lReturnList As New List(Of clsBeTrans_ubic_hh_stock)
            Const sp As String = "SELECT * FROM Trans_ubic_hh_stock WHERE IdTareaUbicacionEnc= @IdTareaUbicacionEnc "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", IdTareaUbicacion)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ubic_hh_stock As New clsBeTrans_ubic_hh_stock

            For Each dr As DataRow In dt.Rows

                vBeTrans_ubic_hh_stock = New clsBeTrans_ubic_hh_stock
                Cargar(vBeTrans_ubic_hh_stock, dr)
                vBeTrans_ubic_hh_stock.IsNew = False
                lReturnList.Add(vBeTrans_ubic_hh_stock)

            Next

            Return lReturnList

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdTareaUbicacionEnc:=IdTareaUbicacion)
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar_Trans_Ubic_HH_Stock(ByVal IdTareaUbicacionEnc As Integer,
                                                       ByVal pListObjStock As List(Of clsBeStock),
                                                       ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det),
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As Boolean

        Dim BeTransUbicHHStock As clsBeTrans_ubic_hh_stock

        Guardar_Trans_Ubic_HH_Stock = False

        Try

            Dim lMaxTransUbicIdStock As Integer = MaxID(lConnection, lTransaction) + 1

            If pListObjStock IsNot Nothing AndAlso pListObjStock.Count > 0 Then

                For Each Obj As clsBeStock In pListObjStock

                    If Obj.IsNew Then

                        BeTransUbicHHStock = New clsBeTrans_ubic_hh_stock

                        lMaxTransUbicIdStock += 1

                        BeTransUbicHHStock.IdStockTransUbicHHDet = lMaxTransUbicIdStock
                        BeTransUbicHHStock.IdTareaUbicacionEnc = IdTareaUbicacionEnc

                        If Not pListObjDet Is Nothing Then

                            If pListObjDet.Count > 0 Then

                                '#CKFK20220119 Corrección porque el Obj devuelve mas de un registro
                                Dim BeTarea As New clsBeTrans_ubic_hh_det

                                BeTarea = pListObjDet.Find(Function(x) x.IdStock = Obj.IdStock _
                                                               AndAlso x.IdUbicacionDestino = Obj.IdUbicacion)

                                If Not BeTarea Is Nothing Then

                                    BeTransUbicHHStock.IdTareaUbicacionDet = BeTarea.IdTareaUbicacionDet

                                    clsPublic.CopyObject(Obj, BeTransUbicHHStock)

                                    '#EJC20171016: Mantener la ubicación origen antes de insertar en el histórico.
                                    'BeTransUbicHHStock.IdUbicacion = BeTransUbicHHStock.IdUbicacion_anterior

                                    Insertar(BeTransUbicHHStock, lConnection, lTransaction)

                                End If

                            Else
                                Throw New Exception("#20211119_1639A: La pListObjDet.Count = 0.")
                            End If

                        Else
                            Throw New Exception("#20211119_1639: La lista pListObjDet Is Nothing.")
                        End If

                    End If

                Next

                Guardar_Trans_Ubic_HH_Stock = True

            End If

        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdTareaUbicacionEnc:=IdTareaUbicacionEnc)
            Throw ex
        End Try

    End Function

    '#EJC20171025_1149AM: Se convirtió en transaccional función GetIdStockTransUbicHHDet
    Public Shared Function GetIdStockTransUbicHHDet(ByRef pBeTrans_ubic_hh_stock As clsBeTrans_ubic_hh_stock,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As Integer

        GetIdStockTransUbicHHDet = 0

        Try

            Const sp As String = "SELECT IdStockTransUbicHHDet FROM Trans_ubic_hh_stock" &
            " Where(IdTareaUbicacionEnc = @IdTareaUbicacionEnc)" &
            " And (IdTareaUbicacionDet = @IdTareaUbicacionDet) " &
            " And (IdStock = @IdStock)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTareaUbicacionEnc", pBeTrans_ubic_hh_stock.IdTareaUbicacionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTareaUbicacionDet", pBeTrans_ubic_hh_stock.IdTareaUbicacionDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", pBeTrans_ubic_hh_stock.IdStock))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                GetIdStockTransUbicHHDet = dt.Rows(0).Item("IdStockTransUbicHHDet")
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class