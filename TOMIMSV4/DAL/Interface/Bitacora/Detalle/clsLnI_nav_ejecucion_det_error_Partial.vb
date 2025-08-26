Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ejecucion_det_error

    Public Shared Sub Inserta_Log(ByVal MensajeDeError As String,
                                  ByVal NoDocumento As String,
                                  ByVal IdEjecucionEnc As Integer,
                                  ByVal IdNavconfigDet As Integer,
                                  Optional ByVal No_Linea As Integer = 0,
                                  Optional ByVal Codigo_Producto As String = "",
                                  Optional ByVal UMBas As String = "",
                                  Optional ByVal Codigo_Presentacion As String = "")

        If NoDocumento Is Nothing Then
            NoDocumento = ""
        End If

        Dim BeNavEjecucionDet As New clsBeI_nav_ejecucion_det_error()
        BeNavEjecucionDet.Idejecuciondet = MaxID() + 1
        BeNavEjecucionDet.Fecha = Now
        BeNavEjecucionDet.Idejecucionenc = IdEjecucionEnc
        BeNavEjecucionDet.Idnavconfigdet = IdNavconfigDet
        BeNavEjecucionDet.vError = MensajeDeError
        BeNavEjecucionDet.Referencia = NoDocumento
        BeNavEjecucionDet.No_Linea = No_Linea
        BeNavEjecucionDet.Codigo_Producto = Codigo_Producto
        BeNavEjecucionDet.UMBas = UMBas
        BeNavEjecucionDet.Codigo_Presentacion = Codigo_Presentacion

        Try
            Insertar_From_Interface(BeNavEjecucionDet)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Inserta_Log(ByVal MensajeDeError As String,
                                  ByVal NoDocumento As String,
                                  ByVal IdEjecucionEnc As Integer,
                                  ByVal IdNavconfigDet As Integer,
                                  ByRef pConection As SqlConnection,
                                  Optional ByVal No_Linea As Integer = 0,
                                  Optional ByVal Codigo_Producto As String = "",
                                  Optional ByVal UMBas As String = "",
                                  Optional ByVal Codigo_Presentacion As String = "")

        If NoDocumento Is Nothing Then
            NoDocumento = ""
        End If

        Dim BeNavEjecucionDet As New clsBeI_nav_ejecucion_det_error()
        BeNavEjecucionDet.Idejecuciondet = MaxID() + 1
        BeNavEjecucionDet.Fecha = Now
        BeNavEjecucionDet.Idejecucionenc = IdEjecucionEnc
        BeNavEjecucionDet.Idnavconfigdet = IdNavconfigDet
        BeNavEjecucionDet.vError = MensajeDeError
        BeNavEjecucionDet.Referencia = NoDocumento
        BeNavEjecucionDet.No_Linea = No_Linea
        BeNavEjecucionDet.Codigo_Producto = Codigo_Producto
        BeNavEjecucionDet.UMBas = UMBas
        BeNavEjecucionDet.Codigo_Presentacion = Codigo_Presentacion

        Try
            Insertar_From_Interface(BeNavEjecucionDet, pConection)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Inserta_Log(ByVal MensajeDeError As String,
                                  ByVal Referencia As String,
                                  ByVal IdEjecucionEnc As Integer,
                                  ByVal IdNavconfigDet As Integer)

        If Referencia Is Nothing Then Referencia = ""
        Dim BeNavEjecucionDet As New clsBeI_nav_ejecucion_det_error() With {.Idejecuciondet = MaxID() + 1,
            .Fecha = Now,
            .Idejecucionenc = IdEjecucionEnc,
            .Idnavconfigdet = IdNavconfigDet,
            .vError = MensajeDeError,
            .Referencia = Referencia}

        Try

            Insertar_From_Interface(BeNavEjecucionDet)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar_From_Interface(ByRef oBeI_nav_ejecucion_det_error As clsBeI_nav_ejecucion_det_error,
                                                   ByVal pConection As SqlConnection) As Integer

        Try

            Ins.Init("i_nav_ejecucion_det_error")
            Ins.Add("idejecuciondet", "@idejecuciondet", DataType.Parametro)
            Ins.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Ins.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Ins.Add("error", "@error", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)

            '#ejc20220314: byb, idealsa.
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("umbas", "@umbas", DataType.Parametro)
            Ins.Add("codigo_presentacion", "@codigo_presentacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONDET", oBeI_nav_ejecucion_det_error.Idejecuciondet))
            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_det_error.Idejecucionenc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_ejecucion_det_error.Idnavconfigdet))
            cmd.Parameters.Add(New SqlParameter("@ERROR", oBeI_nav_ejecucion_det_error.vError))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeI_nav_ejecucion_det_error.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_det_error.Fecha))

            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeI_nav_ejecucion_det_error.No_Linea))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_ejecucion_det_error.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeI_nav_ejecucion_det_error.UMBas))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRESENTACION", oBeI_nav_ejecucion_det_error.Codigo_Presentacion))

            If pConection.State = ConnectionState.Closed Then pConection.Open()

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Rango_Fecha(ByVal pFechaDel As Date,
                                                ByVal pFechaAl As Date) As List(Of clsBeI_nav_ejecucion_det_error)

        Dim lReturnList As New List(Of clsBeI_nav_ejecucion_det_error)

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_ejecucion_det_error WHERE 1>0 "

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text

                    Dim lTable As New DataTable
                    lDataAdapter.Fill(lTable)

                    Dim Obj As clsBeI_nav_ejecucion_det_error

                    If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lTable.Rows

                            Obj = New clsBeI_nav_ejecucion_det_error
                            Cargar(Obj, lRow)
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

    Public Shared Function Insertar_From_Interface(ByRef oBeI_nav_ejecucion_det_error As clsBeI_nav_ejecucion_det_error) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Ins.Init("i_nav_ejecucion_det_error")
            Ins.Add("idejecuciondet", "@idejecuciondet", DataType.Parametro)
            Ins.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Ins.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Ins.Add("error", "@error", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)

            '#ejc20220314: byb, idealsa.
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("umbas", "@umbas", DataType.Parametro)
            Ins.Add("codigo_presentacion", "@codigo_presentacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONDET", oBeI_nav_ejecucion_det_error.Idejecuciondet))
            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_det_error.Idejecucionenc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_ejecucion_det_error.Idnavconfigdet))
            cmd.Parameters.Add(New SqlParameter("@ERROR", oBeI_nav_ejecucion_det_error.vError))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeI_nav_ejecucion_det_error.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_det_error.Fecha))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeI_nav_ejecucion_det_error.No_Linea))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_ejecucion_det_error.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeI_nav_ejecucion_det_error.UMBas))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRESENTACION", oBeI_nav_ejecucion_det_error.Codigo_Presentacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            lTransaction.Commit()

            cmd.Dispose()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            lConnection.Close()
        End Try

    End Function

End Class
