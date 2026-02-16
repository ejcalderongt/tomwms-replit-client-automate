Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_re_det_parametros

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0


            Const SP As String = "SELECT ISNULL(Max(IdParametroDet),0) FROM trans_re_det_parametros WHERE IdRecepcionEnc={0} AND IdRecepcionDet={1}"
            Using lCommand As New SqlCommand(SP, pConnection)
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

    Public Shared Function MaxID(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(IdParametroDet),0) FROM trans_re_det_parametros WHERE IdRecepcionEnc={0} AND IdRecepcionDet={1}", pIdRecepcionEnc, pIdRecepcionDet)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.CommandType = CommandType.Text

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdRecepcionEnc As Integer,
                                 ByVal pIdRecepcionDet As Integer,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = String.Format("SELECT ISNULL(Max(IdParametroDet),0) " &
                                             " FROM trans_re_det_parametros " &
                                             " WHERE IdRecepcionEnc={0} And IdRecepcionDet={1}", pIdRecepcionEnc, pIdRecepcionDet)

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

    Public Shared Function Get_Detalle_Parametros_By_RecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_det_parametros)

        Get_Detalle_Parametros_By_RecepcionEnc = Nothing

        Dim lReturnList As New List(Of clsBeTrans_re_det_parametros)

        Try

            Dim vSQL As String = "SELECT * FROM trans_re_det_parametros WHERE IdRecepcionEnc=@IdRecepcionEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_re_det_parametros
                Dim LnTrans_re_det_parametros As New clsLnTrans_re_det_parametros

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_re_det_parametros

                        LnTrans_re_det_parametros.Cargar(Obj, lRow)

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

    Public Shared Function Get_All_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal pIdRecepcionEnc As Integer,
                                                                        ByVal pIdRecepcionDet As Integer) As List(Of clsBeTrans_re_det_parametros)

        Dim lReturnList As New List(Of clsBeTrans_re_det_parametros)

        Try

            Dim vSQL As String = "SELECT * FROM trans_re_det_parametros WHERE IdRecepcionEnc=@IdRecepcionEnc And IdRecepcionDet=@IdRecepcionDet"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det_parametros
                        Dim LnTrans_re_det_parametros As New clsLnTrans_re_det_parametros

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeTrans_re_det_parametros

                                LnTrans_re_det_parametros.Cargar(Obj, lRow)

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

    Public Shared Function Get_All_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal pIdRecepcionEnc As Integer,
                                                                        ByVal pIdRecepcionDet As Integer,
                                                                        ByRef lConnection As SqlConnection,
                                                                        ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_det_parametros)

        Dim lReturnList As New List(Of clsBeTrans_re_det_parametros)

        Try

            Dim vSQL As String = "SELECT * FROM trans_re_det_parametros WHERE IdRecepcionEnc=@IdRecepcionEnc And IdRecepcionDet=@IdRecepcionDet"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_re_det_parametros
                Dim LnParam As New clsLnTrans_re_det_parametros

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_re_det_parametros

                        LnParam.Cargar(Obj, lRow)

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

    Public Shared Function Get_All_Params_By_IdRecepcionEnc_And_IdRecepcion_Det_For_HH(ByVal pIdRecepcionEnc As Integer,
                                                                                       ByVal pIdRecepcionDet As Integer) As List(Of clsBeTrans_re_det_parametros)

        Dim lReturnList As New List(Of clsBeTrans_re_det_parametros)

        Try


            Dim vSQL As String = "SELECT p.*,case when tipo = 'Lógico' then logico else case when tipo = 'Númerico' then numerico else 
                        case when tipo = 'Texto' then texto else case when tipo = 'Fecha' then fecha end end end end as Valor 
                        FROM (SELECT dp.*,
                        convert(nvarchar(50),iif(dp.valor_logico=0,'False', 'True')) as logico, 
                        convert(nvarchar(50),dp.valor_numerico) as numerico, 
                        convert(nvarchar(50),dp.valor_fecha,112) as fecha, 
                        convert(nvarchar(50),pp.valor_texto) as texto,p.IdParametro, p.Tipo 
                        FROM trans_re_det_parametros dp INNER JOIN 
                        producto_parametros pp ON pp.IdProductoParametro = dp.IdProductoParametro INNER JOIN 
                        p_parametro p ON pp.IdParametro = p.IdParametro 
                        WHERE IdRecepcionEnc=@IdRecepcionEnc AND IdRecepcionDet=@IdRecepcionDet) as p"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det_parametros
                        Dim LnTrans_re_det_parametros_sc As New clsLnTrans_re_det_parametros_sc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det_parametros

                                LnTrans_re_det_parametros_sc.Cargar(Obj, lRow, lConnection, lTransaction)

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

    Public Shared Function Guarda_Trans_Re_Det_Parametros(ByVal IdRecepcionEnc As Integer,
                                                         ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                                         ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As Integer

        Guarda_Trans_Re_Det_Parametros = 0

        Try

            Dim vFilas As Integer = 0

            If Not pListRecDet Is Nothing Then

                If pListRecDet.Count > 0 Then

                    If Not pListRecDetParam Is Nothing Then

                        For Each Obj1 As clsBeTrans_re_det In pListRecDet

                            Dim lMaxIdDP As Integer = MaxID(Obj1.IdRecepcionEnc,
                                                            Obj1.IdRecepcionDet,
                                                            lConnection,
                                                            lTransaction)

                            For Each Obj2 As clsBeTrans_re_det_parametros In pListRecDetParam.FindAll(Function(p) p.IdRecepcionDet = Obj1.IdRecepcionDet)

                                If Obj2.IsNew Then
                                    Obj2.IdRecepcionEnc = IdRecepcionEnc
                                    lMaxIdDP += 1
                                    Obj2.IdParametroDet = lMaxIdDP
                                    vFilas += Insertar(Obj2, lConnection, lTransaction)
                                Else
                                    vFilas += Actualizar(Obj2, lConnection, lTransaction)
                                End If

                            Next

                        Next

                    End If

                    Guarda_Trans_Re_Det_Parametros = vFilas

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Guarda_Trans_Re_Det_Parametros(ByVal IdRecepcionEnc As Integer,
                                                         ByVal BeTransReDet As clsBeTrans_re_det,
                                                         ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As Integer

        Guarda_Trans_Re_Det_Parametros = 0

        Try

            Dim vFilas As Integer = 0

            If Not BeTransReDet Is Nothing Then

                If Not pListRecDetParam Is Nothing Then

                    If pListRecDetParam.Count > 0 Then

                        For Each BeTransReDetParam As clsBeTrans_re_det_parametros In pListRecDetParam.FindAll(Function(p) p.IdRecepcionDet = BeTransReDet.IdRecepcionDet)

                            If BeTransReDetParam.IsNew Then
                                BeTransReDetParam.IdRecepcionEnc = IdRecepcionEnc
                                BeTransReDetParam.IdParametroDet = MaxID(BeTransReDet.IdRecepcionEnc,
                                                                        BeTransReDet.IdRecepcionDet,
                                                                        lConnection,
                                                                        lTransaction) + 1
                                vFilas += Insertar(BeTransReDetParam, lConnection, lTransaction)
                            Else
                                vFilas += Actualizar(BeTransReDetParam, lConnection, lTransaction)
                            End If

                        Next

                    End If

                End If

            End If

            Guarda_Trans_Re_Det_Parametros = vFilas

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


End Class
