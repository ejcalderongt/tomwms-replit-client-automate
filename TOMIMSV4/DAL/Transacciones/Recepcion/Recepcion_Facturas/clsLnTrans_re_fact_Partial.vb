Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles

Partial Public Class clsLnTrans_re_fact


    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdFacturaRecepcion),0) FROM trans_re_fact"), lConnection)
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


    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdFacturaRecepcion),0) FROM trans_re_fact "

            '#HS 07112017 Quité query dentro de SqlCommand.
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

    Public Shared Function GetAllByRecepcion(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeTrans_re_fact)

        Dim lReturnList As New List(Of clsBeTrans_re_fact)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM Trans_re_fact WHERE IdRecepcionEnc=@IdRecepcionEnc"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_re_fact

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_re_fact

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

    Public Shared Function Get_Cadena_Factura_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As String

        Dim lReturn As String = ""

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT NoFactura FROM Trans_re_fact WHERE IdRecepcionEnc=@IdRecepcionEnc"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            lReturn += lRow("NoFactura").ToString & ", "

                        Next

                        lReturn = lReturn.Substring(0, lReturn.Length - 2)

                    End If

                End Using

            End Using

            Return lReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_Facturas_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_fact)

        Dim lReturnList As New List(Of clsBeTrans_re_fact)

        Try


            Dim vSQL As String = "SELECT * FROM Trans_re_fact WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_re_fact

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_re_fact

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

    Public Shared Sub Delete(ByVal pIdFacturaRecepcion As Integer)

        Dim vSQL As String = "DELETE FROM trans_re_fact WHERE IdFacturaRecepcion=@IdFacturaRecepcion"

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            '#HS 07112017 Quité query dentro de SqlCommand.
            Using lCommand As New SqlCommand(vSQL, lConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdFacturaRecepcion", pIdFacturaRecepcion)

                lConnection.Open()
                lCommand.ExecuteNonQuery()
                lConnection.Close()

            End Using

        End Using

    End Sub

    Public Shared Sub Guarda_facturas_asoc(ByVal IdRecepcionEnc As Integer,
                                           ByVal pListRecFact As List(Of clsBeTrans_re_fact),
                                           ByRef lConnection As SqlConnection,
                                           ByRef lTransaction As SqlTransaction)

        Try

            If Not pListRecFact Is Nothing Then

                Dim lMaxIdF As Integer = MaxID(lConnection, lTransaction)

                For Each ObjF As clsBeTrans_re_fact In pListRecFact
                    If ObjF.IsNew Then
                        lMaxIdF += 1
                        ObjF.IdFacturaRecepcion = lMaxIdF
                        ObjF.IdRecepcionEnc = IdRecepcionEnc
                        Insertar(ObjF, lConnection, lTransaction)
                    Else
                        Actualizar(ObjF, lConnection, lTransaction)
                    End If
                Next

            End If

        Catch ex As Exception
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdRecEnc:=IdRecepcionEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Guarda_facturas_asoc(ByVal IdRecepcionEnc As Integer,
                                           ByVal pListRecFact As List(Of clsBeTrans_re_fact))


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If Not pListRecFact Is Nothing Then

                Dim lMaxIdF As Integer = MaxID(lConnection, lTransaction)

                For Each BeTransReFact As clsBeTrans_re_fact In pListRecFact
                    If BeTransReFact.IsNew Then
                        lMaxIdF += 1
                        BeTransReFact.IdFacturaRecepcion = lMaxIdF
                        BeTransReFact.IdRecepcionEnc = IdRecepcionEnc
                        Insertar(BeTransReFact, lConnection, lTransaction)
                    Else
                        Actualizar(BeTransReFact, lConnection, lTransaction)
                    End If
                Next

            End If

            If Not lTransaction Is Nothing Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                lTransaction.Rollback()
            End If
            '#MECR25092025: Se agrego bitacora de logs para recepciones
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdRecEnc:=IdRecepcionEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    '#GT10102025: sin transaccion remota porque es para recargar el objeto cuando se esta realizando la recepción.
    Public Shared Function Get_Detalle_Facturas_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeTrans_re_fact)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lReturnList As New List(Of clsBeTrans_re_fact)

        Try


            Dim vSQL As String = "SELECT * FROM Trans_re_fact WHERE IdRecepcionEnc=@IdRecepcionEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_re_fact

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_re_fact

                        Cargar(Obj, lRow)

                        Obj.IsNew = False

                        lReturnList.Add(Obj)

                    Next

                End If

                If Not lTransaction Is Nothing Then
                    lTransaction.Commit()
                End If


            End Using

            Return lReturnList

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                lTransaction.Rollback()
            End If
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Existe_By_NoFactura(ByVal pNoFactura As String) As Boolean

        Existe_By_NoFactura = False

        Try
            Dim sp As String = "SELECT COUNT(1) FROM Trans_re_fact WHERE (NoFactura = @NOFACTURA)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using cmd As New SqlCommand(sp, lConnection)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add(New SqlParameter("@NOFACTURA", pNoFactura))

                    lConnection.Open()
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If count > 0 Then
                        Existe_By_NoFactura = True
                    End If
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("ObtenerTransReFact: " & ex.Message)
        End Try

    End Function

End Class
