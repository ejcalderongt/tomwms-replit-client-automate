Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnCliente_tiempos
    Implements IDisposable

    Public Shared Function Get_All_Tiempos_By_IdCliente(ByVal pIdCliente As Integer) As List(Of clsBeCliente_tiempos)

        Dim lReturnList As List(Of clsBeCliente_tiempos) = Nothing

        '#HS 07112017 Quité query dentro de SqlDataAdapter.
        Dim vSQL As String = "SELECT * FROM VW_TiempoCliente WHERE activo=1 AND IdCliente=@IdCliente"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1630pm: Quité String.Format.
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeCliente_tiempos
                    'Dim LnClienteTiempos As New clsLnCliente_tiempos

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        lReturnList = New List(Of clsBeCliente_tiempos)

                        For Each lRow As DataRow In lDT.Rows

                            Obj = New clsBeCliente_tiempos()
                            Obj.Familia = New clsBeProducto_familia()
                            Obj.Clasificacion = New clsBeProducto_clasificacion()
                            Cargar(Obj, lRow)

                            If lRow("Familia") IsNot DBNull.Value AndAlso lRow("Familia") IsNot Nothing Then
                                Obj.Familia.Nombre = CType(lRow("Familia"), String)
                            End If

                            If lRow("Clasificación") IsNot DBNull.Value AndAlso lRow("Clasificación") IsNot Nothing Then
                                Obj.Clasificacion.Nombre = CType(lRow("Clasificación"), String)
                            End If

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

    Public Shared Function Get_All_Tiempos_By_IdCliente(ByVal pIdCliente As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente_tiempos)

        Dim lReturnList As New List(Of clsBeCliente_tiempos)

        Dim vSQL As String = "SELECT * FROM VW_TiempoCliente WHERE activo=1 AND IdCliente=@IdCliente"

        Try

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                Dim BeClienteTiempos As clsBeCliente_tiempos
                'Dim LnClienteTiempos As New clsLnCliente_tiempos

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDT.Rows

                        BeClienteTiempos = New clsBeCliente_tiempos()
                        BeClienteTiempos.Familia = New clsBeProducto_familia()
                        BeClienteTiempos.Clasificacion = New clsBeProducto_clasificacion()
                        Cargar(BeClienteTiempos, lRow)

                        If lRow("Familia") IsNot DBNull.Value AndAlso lRow("Familia") IsNot Nothing Then
                            BeClienteTiempos.Familia.Nombre = CType(lRow("Familia"), String)
                        End If

                        If lRow("Clasificación") IsNot DBNull.Value AndAlso lRow("Clasificación") IsNot Nothing Then
                            BeClienteTiempos.Clasificacion.Nombre = CType(lRow("Clasificación"), String)
                        End If

                        lReturnList.Add(BeClienteTiempos)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdCliente As Integer,
                                     ByVal IdFamilia As Integer,
                                     ByVal IdClasificacion As Integer) As clsBeCliente_tiempos

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Cliente_tiempos" &
            " Where(IdCliente = @IdCliente) " &
            " And IdFamilia = @IdFamilia " &
            " And IdClasificacion = @IdClasificacion "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdCliente", IdCliente))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdFamilia", IdFamilia))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdClasificacion", IdClasificacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeCliente_tiempos As New clsBeCliente_tiempos

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_tiempos, dt.Rows(0))
            Else
                pBeCliente_tiempos = Nothing
            End If

            Return pBeCliente_tiempos


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tiempos_Clas_And_Fam(ByVal ConTiempos As Boolean) As DataTable

        Dim lDatatable As New DataTable

        '#HS.RIP 07112017 Quité query dentro de SqlDataAdapter.

        Dim vSQL As String = "SELECT * 
                              FROM VW_Clientes_Tiempos "

        If ConTiempos Then
            vSQL += "WHERE cant_familias>0 And cant_clasificacion>0"
        Else
            vSQL += "WHERE cant_familias=0 And cant_clasificacion=0"
        End If

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            lDatatable = lDT
                        End If

                    End Using

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

            Return lDatatable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tiempos_Det() As DataTable

        Dim lDatatable As New DataTable

        Dim vSQL As String = "SELECT Cliente, Familia, Clasificación, Dias_Local, Dias_Exterior
                              FROM VW_TiempoCliente "

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandTimeout = 0

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            lDatatable = lDT
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lDatatable

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function GetSingle(ByVal IdCliente As Integer,
                                     ByVal IdFamilia As Integer,
                                     ByVal IdClasificacion As Integer,
                                     ByVal pConnection As SqlConnection,
                                     ByVal pTransaction As SqlTransaction) As clsBeCliente_tiempos

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Cliente_tiempos" &
            " Where(IdCliente = @IdCliente) " &
            " And IdFamilia = @IdFamilia " &
            " And IdClasificacion = @IdClasificacion "

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdCliente", IdCliente))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdFamilia", IdFamilia))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdClasificacion", IdClasificacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeCliente_tiempos As New clsBeCliente_tiempos

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_tiempos, dt.Rows(0))
            Else
                pBeCliente_tiempos = Nothing
            End If

            Return pBeCliente_tiempos


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#CKFK20250306 Agregué esta función para que liste el detalle de un cliente específico
    Public Shared Function Get_All_Tiempos_Det_By_IdCliente(ByVal pCodCliente As String) As DataTable

        Dim lDatatable As New DataTable

        Dim vSQL As String = "SELECT Cliente, Familia, Clasificación, Dias_Local, Dias_Exterior
                              FROM VW_TiempoCliente
                              WHERE IdCliente IN (SELECT IdCliente FROM cliente WHERE codigo = @CodCliente) "

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandTimeout = 0

                        lDTA.SelectCommand.Parameters.AddWithValue("@CodCliente", pCodCliente)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            lDatatable = lDT
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lDatatable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
