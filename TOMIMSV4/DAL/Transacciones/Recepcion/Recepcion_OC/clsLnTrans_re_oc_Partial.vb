Imports System
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_re_oc

    Public Shared Function GetSingle(ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_oc

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM Trans_re_oc WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)


                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_re_oc()

                            Cargar(Obj, lRow)

                            Obj.IsNew = False

                            Obj.OC.IdOrdenCompraEnc = Obj.IdOrdenCompraEnc

                            clsLnTrans_oc_enc.Obtener(Obj.OC, lConnection, lTransaction)

                            Return Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdRecepcionEnc As Integer,
                                    ByRef lConnection As SqlConnection,
                                    ByRef lTransaction As SqlTransaction) As clsBeTrans_re_oc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM Trans_re_oc WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransReOC As New clsBeTrans_re_oc()

                    Cargar(BeTransReOC, lRow)

                    BeTransReOC.IsNew = False
                    BeTransReOC.OC.IdOrdenCompraEnc = BeTransReOC.IdOrdenCompraEnc
                    BeTransReOC.OC = clsLnTrans_oc_enc.Get_Orden_Compra(BeTransReOC.OC.IdOrdenCompraEnc, lConnection, lTransaction)

                    If Not BeTransReOC.OC Is Nothing Then
                        '#EJC20190401: Obtener el objeto de tipo de documento, se utilizó para optimizar la carga de datos en la HH.
                        BeTransReOC.OC.TipoIngreso = clsLnTrans_oc_ti.GetSingle(BeTransReOC.OC.IdTipoIngresoOC, lConnection, lTransaction)
                    End If

                    Return BeTransReOC

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creada por Erik Calderón, se usa para obtener todas las Recepciones para posteriormente buscar su estado y anularlas
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <returns>Lista de Recepciones solamente Ids</returns>

    Public Shared Function Get_IdRecepcionEnc_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer) As List(Of Integer)

        Dim lReturnList As New List(Of Integer)

        Try

            Dim vSQL As String = "SELECT IdRecepcionEnc FROM trans_re_oc WHERE IdOrdenCompraEnc=" & pIdOrdenCompraEnc

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDT.Rows

                                Dim lIdRecepcion As Integer = lRow("IdRecepcionEnc")

                                lReturnList.Add(lIdRecepcion)

                            Next

                        End If

                        lDT.Dispose()
                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()
                lConnection.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_IdOrdenCompraEnc_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of Integer)

        Dim lReturnList As New List(Of Integer)

        Try

            Dim vSQL As String = "SELECT IdOrdenCompraEnc FROM trans_re_oc WHERE IdRecepcionEnc =@IdRecepcionEnc  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDT.Rows

                                Dim lIdRecepcion As Integer = lRow("IdOrdenCompraEnc")
                                lReturnList.Add(lIdRecepcion)

                            Next

                        End If

                        lDT.Dispose()
                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                lConnection.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdOrdenCompraEnc_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                                  ByVal lConnection As SqlConnection,
                                                                  ByVal lTransaction As SqlTransaction) As List(Of Integer)

        Dim lReturnList As New List(Of Integer)

        Try

            Dim vSQL As String = "SELECT IdOrdenCompraEnc FROM trans_re_oc WHERE IdRecepcionEnc =@IdRecepcionEnc  "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDT.Rows

                        Dim lIdRecepcion As Integer = lRow("IdOrdenCompraEnc")
                        lReturnList.Add(lIdRecepcion)

                    Next

                End If

                lDT.Dispose()
                lDTA.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function OrdenCompra_Tiene_Recepciones_Finalizadas(ByVal pIdOrdenCompraEnc As Integer) As Boolean

        OrdenCompra_Tiene_Recepciones_Finalizadas = False

        Try

            Dim vSQL As String = "SELECT trans_re_oc.IdOrdenCompraEnc, trans_re_enc.IdRecepcionEnc 
                        FROM trans_re_oc INNER JOIN 
                        trans_re_enc ON trans_re_oc.IdRecepcionEnc = trans_re_enc.IdRecepcionEnc 
                        WHERE trans_re_oc.IdOrdenCompraEnc = @IdOrdenCompraEnc AND trans_re_enc.estado = 'Cerrado' "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        OrdenCompra_Tiene_Recepciones_Finalizadas = lDT.Rows.Count > 0

                        lDT.Dispose()

                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                lConnection.Dispose()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdRecepcionEnc As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = String.Format("SELECT ISNULL(Max(IdRecepcionOc),0) FROM trans_re_oc WHERE IdRecepcionEnc={0}", pIdRecepcionEnc)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
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

    Public Shared Function MaxID(ByVal pIdOrdenCompraEnc As Integer,
                                 ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = String.Format("SELECT ISNULL(Max(IdRecepcionOc),0) FROM trans_re_oc WHERE IdOrdenCompraenc={0}", pIdOrdenCompraEnc)

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guarda_Trans_Re_OC(ByVal pRecEnc As clsBeTrans_re_enc,
                                              ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Integer

        Guarda_Trans_Re_OC = 0

        Dim vFilas As Integer = 0

        Try

            If pRecOrdenCompra IsNot Nothing AndAlso pRecOrdenCompra.IdOrdenCompraEnc > 0 Then

                vFilas = clsLnTrans_oc_enc.Actualizar_Estado_OC(pRecEnc,
                                                                pRecOrdenCompra,
                                                                lConnection,
                                                                lTransaction)

                If pRecOrdenCompra.IsNew Then

                    pRecOrdenCompra.IdRecepcionOc = MaxID(pRecOrdenCompra.IdOrdenCompraEnc,
                                                          lConnection,
                                                          lTransaction) + 1

                    vFilas += Insertar(pRecOrdenCompra,
                                       lConnection,
                                       lTransaction)
                Else
                    vFilas += Actualizar(pRecOrdenCompra,
                                         lConnection,
                                         lTransaction)
                End If

                Guarda_Trans_Re_OC = vFilas

            End If

        Catch ex As Exception
            '#MECR25092025: se agrego bitacora de logs en recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, pRecEnc.IdBodega, pRecEnc.User_agr, pStackTrace:=ex.StackTrace, pIdRecEnc:=pRecEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                                             ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_oc

        Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_oc 
                    WHERE IdRecepcionEnc=@IdRecepcionEnc 
                    AND IdOrdenCompraEnc = @IdOrdenCompraEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransReOC As New clsBeTrans_re_oc()
                            Cargar(BeTransReOC, lRow)
                            BeTransReOC.IsNew = False
                            BeTransReOC.OC.IdOrdenCompraEnc = BeTransReOC.IdOrdenCompraEnc
                            Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc = BeTransReOC

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                                             ByVal pIdRecepcionEnc As Integer,
                                                                             ByRef lConnection As SqlConnection,
                                                                             ByRef lTransaction As SqlTransaction) As clsBeTrans_re_oc

        Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_oc 
                    WHERE IdRecepcionEnc=@IdRecepcionEnc 
                    AND IdOrdenCompraEnc = @IdOrdenCompraEnc"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_re_oc()

                    Cargar(Obj, lRow)

                    Obj.IsNew = False

                    Obj.OC.IdOrdenCompraEnc = Obj.IdOrdenCompraEnc

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc_With_OC(ByVal pIdOrdenCompraEnc As Integer,
                                                                                     ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_oc

        Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc_With_OC = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_oc 
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc 
                                  AND IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransOC As New clsBeTrans_re_oc()

                            Cargar(BeTransOC, lRow)

                            BeTransOC.IsNew = False
                            BeTransOC.OC.IdOrdenCompraEnc = BeTransOC.IdOrdenCompraEnc
                            BeTransOC.OC = clsLnTrans_oc_enc.GetSingle(BeTransOC.IdOrdenCompraEnc, lConnection, lTransaction)

                            Return BeTransOC

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Existe_Documento_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Documento_By_IdOrdenCompraEnc = False

        Try

            Const sp As String = "SELECT IdRecepcionOC FROM Trans_re_oc 
			 Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", pIdOrdenCompraEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            Existe_Documento_By_IdOrdenCompraEnc = dt.Rows.Count > 0

        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs en recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_RE_OC_by_IdOrdenCompra(ByVal pIdOrdenCompraEnc As Integer,
                                                                             ByRef lConnection As SqlConnection,
                                                                             ByRef lTransaction As SqlTransaction) As clsBeTrans_re_oc

        Get_RE_OC_by_IdOrdenCompra = Nothing


        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_oc 
                                  WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_re_oc()

                    Cargar(Obj, lRow)

                    Obj.IsNew = False

                    Obj.OC.IdOrdenCompraEnc = Obj.IdOrdenCompraEnc

                    Obj.OC = clsLnTrans_oc_enc.Get_Orden_Compra(Obj.OC.IdOrdenCompraEnc, lConnection, lTransaction)

                    If Not Obj.OC Is Nothing Then
                        '#EJC20190401: Obtener el objeto de tipo de documento, se utilizó para optimizar la carga de datos en la HH.
                        Obj.OC.TipoIngreso = clsLnTrans_oc_ti.GetSingle(Obj.OC.IdTipoIngresoOC, lConnection, lTransaction)
                    End If

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc_With_OC(ByVal pIdOrdenCompraEnc As Integer,
                                                                                     ByVal pIdRecepcionEnc As Integer,
                                                                                     ByVal lConnection As SqlConnection,
                                                                                     ByVal lTransaction As SqlTransaction) As clsBeTrans_re_oc

        Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc_With_OC = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_oc 
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc 
                                  AND IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransOC As New clsBeTrans_re_oc()

                    Cargar(BeTransOC, lRow)

                    BeTransOC.IsNew = False
                    BeTransOC.OC.IdOrdenCompraEnc = BeTransOC.IdOrdenCompraEnc
                    BeTransOC.OC = clsLnTrans_oc_enc.GetSingle(BeTransOC.IdOrdenCompraEnc, lConnection, lTransaction)

                    Return BeTransOC

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function GetListReOC_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                           ByRef lConnection As SqlConnection,
                                                           ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_oc)


        GetListReOC_By_IdOrdenCompraEnc = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_re_oc WHERE  IdOrdenCompraEnc = @IdOrdenCompraEnc "


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    GetListReOC_By_IdOrdenCompraEnc = New List(Of clsBeTrans_re_oc)()

                    For Each lRow As DataRow In lDT.Rows
                        Dim BeTransOC As New clsBeTrans_re_oc()
                        Cargar(BeTransOC, lRow)
                        BeTransOC.IsNew = False
                        GetListReOC_By_IdOrdenCompraEnc.Add(BeTransOC)
                    Next

                End If

            End Using



        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20250902: Creada para la interface de mampa.
    ''' </summary>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function GetSingle(ByVal pIdRecepcionEnc As Integer,
                                     ByVal pIdOrdenCompraEnc As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeTrans_re_oc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM Trans_re_oc WHERE IdRecepcionEnc=@IdRecepcionEnc AND IdOrdenCompraEnc = @IdOrdenCompraEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                        Tiene_Recepciones = lDT.Rows.Count > 0

                        lDT.Dispose()

                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                lConnection.Dispose()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdRecepcionEnc_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                                  ByVal pConnection As SqlConnection,
                                                                  ByVal pTransaction As SqlTransaction) As List(Of Integer)

        Dim lReturnList As New List(Of Integer)

        Try

            Dim vSQL As String = "SELECT IdRecepcionEnc FROM trans_re_oc WHERE IdOrdenCompraEnc=" & pIdOrdenCompraEnc

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransReOC As New clsBeTrans_re_oc()
                    Cargar(BeTransReOC, lRow)
                    BeTransReOC.IsNew = False
                    BeTransReOC.OC.IdOrdenCompraEnc = BeTransReOC.IdOrdenCompraEnc
                    Return BeTransReOC

                End If

                lDT.Dispose()
                lDTA.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Recepciones(ByVal pIdOrdenCompraEnc As Integer) As Boolean

        Tiene_Recepciones = False

        Try

            Dim vSQL As String = "SELECT trans_re_oc.IdOrdenCompraEnc, trans_re_enc.IdRecepcionEnc 
                        FROM trans_re_oc INNER JOIN 
                        trans_re_enc ON trans_re_oc.IdRecepcionEnc = trans_re_enc.IdRecepcionEnc 
                        WHERE trans_re_oc.IdOrdenCompraEnc = @IdOrdenCompraEnc AND ANULADA=0 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        Tiene_Recepciones = lDT.Rows.Count > 0

                        lDT.Dispose()

                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                lConnection.Dispose()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class
