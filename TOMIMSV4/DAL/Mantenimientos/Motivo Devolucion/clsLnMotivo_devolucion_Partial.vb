Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnMotivo_devolucion
    Implements IDisposable

    ''' <summary>
    ''' Funcion utizada para regresar un registro de la tabla segun parámetro
    ''' </summary>
    ''' <param name="pIdMotivoDevolucion"></param>
    ''' <returns name = "Regresa un objeto de tipo de la tabla motivo_devolucion"></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSingle(ByVal pIdMotivoDevolucion As Integer) As clsBeMotivo_devolucion

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM motivo_devolucion WHERE IdMotivoDevolucion=@IdMotivoDevolucion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdMotivoDevolucion", pIdMotivoDevolucion)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeMotivo_devolucion()

                            Cargar(Obj, lRow)
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

    Public Shared Function Get_Motivo_Devolucion_Por_Defecto(ByVal IdBodega As Integer, ByVal IdPropietario As Integer) As clsBeMotivo_devolucion

        Try

            Dim vSQL As String = "SELECT md.* FROM motivo_devolucion_bodega AS mdb
                                  INNER JOIN motivo_devolucion AS md ON mdb.IdmotivoDevolucion = md.IdMotivoDevolucion 
                                  WHERE mdb.IdBodega =@IdBodega AND md.IdPropietario =@IdPropietario AND md.es_detalle=0"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeMotivo_devolucion()
                            Cargar(Obj, lRow)
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

    ''' <summary>
    ''' Función utilizada para regresar una lista de Motivos Devolución
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <returns name ="Reresa una lista de registros de la tabla motivo_devolucion"></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeMotivo_devolucion)

        Dim lReturnList As New List(Of clsBeMotivo_devolucion)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_MotivoDevolucion WHERE 1 > 0 "

                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMotivo_devolucion

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMotivo_devolucion
                            Cargar(Obj, lRow)

                            If lRow("IdEmpresa") IsNot DBNull.Value AndAlso lRow("IdEmpresa") IsNot Nothing Then

                                Obj.Empresa = New clsBeEmpresa
                                Obj.Empresa.Nombre = CType(lRow("Empresa"), String)

                            End If

                            If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then

                                Obj.Propietario = New clsBePropietarios
                                Obj.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)

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

    Public Shared Function GetAllByPropietarioBodega(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeMotivo_devolucion)

        Dim lReturnList As New List(Of clsBeMotivo_devolucion)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT md.IdMotivoDevolucion, md.Nombre FROM motivo_devolucion_bodega AS mdb " _
                                     & " INNER JOIN motivo_devolucion AS md ON mdb.IdmotivoDevolucion = md.IdMotivoDevolucion " _
                                     & " WHERE mdb.IdBodega =@IdBodega AND md.IdPropietario =@IdPropietario AND md.es_detalle=0"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMotivo_devolucion

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMotivo_devolucion
                            Obj.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Int32)

                            If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("Nombre"), String)
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

    Public Shared Function Get_All_By_IdPropietario_And_Bodega_DT(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As DataTable

        Dim lReturnDT As New DataTable

        Try


            Dim vSQL As String = "SELECT md.IdMotivoDevolucion, md.Nombre FROM motivo_devolucion_bodega AS mdb
                        INNER JOIN motivo_devolucion AS md ON mdb.IdmotivoDevolucion = md.IdMotivoDevolucion 
                        WHERE mdb.IdBodega =@IdBodega AND md.IdPropietario =@IdPropietario AND md.es_detalle=0"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        lReturnDT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnDT

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                  ByVal pIdBodega As Integer) As List(Of clsBeMotivo_devolucion)

        Dim lReturnList As New List(Of clsBeMotivo_devolucion)

        Try


            Dim vSQL As String = "SELECT md.IdMotivoDevolucion, md.Nombre FROM motivo_devolucion_bodega AS mdb " _
                                     & " INNER JOIN motivo_devolucion AS md ON mdb.IdmotivoDevolucion = md.IdMotivoDevolucion " _
                                     & " WHERE mdb.IdBodega =@IdBodega AND md.IdPropietario =@IdPropietario AND md.es_detalle=1"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeMotivo_devolucion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeMotivo_devolucion
                                Obj.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Int32)

                                If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                    Obj.Nombre = CType(lRow("Nombre"), String)
                                End If

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

    ''' <summary>
    ''' Procedimiento para eliminar definitivamente un motivo devolucion segun el Propietario
    ''' </summary>
    ''' <param name="pIdEmpresa"></param>
    ''' <param name="pIdPropietario"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal pIdEmpresa As Integer, ByVal pIdPropietario As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("DELETE FROM motivo_devolucion WHERE IdEmpresa={0} AND IdPropietario={1}", pIdEmpresa, pIdPropietario), lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    lCommand.ExecuteNonQuery()
                    lConnection.Close()

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Función utilizada para validar si existe el motivo devolución
    ''' </summary>
    ''' <param name="pIdMotivoDevolucion"></param>
    ''' <returns name = "Regresa verdadero si existe de lo contrario falso"></returns>
    ''' <remarks></remarks>
    Public Shared Function Exists(ByVal pIdMotivoDevolucion As Integer) As Boolean

        Dim lExists As Boolean = False

        Dim vSQL As String = "SELECT COUNT(1) FROM motivo_devolucion WHERE IdMotivoDevolucion=@IdMotivoDevolucion"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdMotivoDevolucion", pIdMotivoDevolucion)

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw New Exception("Existe_MotvioDevolucionPartial: " & ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Función utilizada para saber cual es el valor máximo de la tabla motivo_devolucion
    ''' </summary>
    ''' <returns name = "Regresa el valor máximo + 1 de la tabla motivo_devolucion"></returns>
    ''' <remarks></remarks>
    Public Shared Function MAXID() As Integer

        Try

            MAXID = 1

            Dim vSQL As String = "SELECT  MAX(IdMotivoDevolucion) + 1 as nuevo FROM motivo_devolucion"

            Dim sp As String = vSQL
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                MAXID = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            End If

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()


        Catch ex As Exception
            Throw New Exception("MaxId_MotivoDevolucionPartial " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBeMotivo_devolucion)


        Get_All_By_IdBodega = Nothing

        Try

            Dim lMA As New List(Of clsBeMotivo_devolucion)
            Dim MA As New clsBeMotivo_devolucion

            Dim sp As String = " SELECT  Motivo_devolucion.* " &
                               " FROM Motivo_devolucion INNER JOIN " &
                               " motivo_devolucion_bodega ON Motivo_devolucion.IdMotivoDevolucion = motivo_devolucion_bodega.IdMotivoDevolucion " &
                               " WHERE (motivo_devolucion_bodega.IdBodega = @IdBodega) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            For Each drow In dt.Rows
                MA = New clsBeMotivo_devolucion
                Cargar(MA, drow)
                lMA.Add(MA)
            Next

            Return lMA

        Catch ex As Exception
            Throw New Exception("GetAllByBodega_Motivo_Devolucion_Partial: " & ex.Message)
        End Try

    End Function


    Public Shared Sub GuardarTransaccion(ByVal pListObjMD As List(Of clsBeMotivo_devolucion))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeMotivo_devolucion In pListObjMD
                If Obj.IsNew Then
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function GetSingle(ByRef pBeMotivo_devolucion As clsBeMotivo_devolucion,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Motivo_devolucion" &
            " Where(IdMotivoDevolucion = @IdMotivoDevolucion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", pBeMotivo_devolucion.IdMotivoDevolucion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMotivo_devolucion, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeMotivo_devolucion As clsBeMotivo_devolucion,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Motivo_devolucion" &
            " Where(IdMotivoDevolucion = @IdMotivoDevolucion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeMotivo_devolucion.IdMotivoDevolucion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMotivo_devolucion, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByPropietarioBodegaDetalle(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeMotivo_devolucion)

        Dim lReturnList As New List(Of clsBeMotivo_devolucion)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT md.IdMotivoDevolucion, md.Nombre FROM motivo_devolucion_bodega AS mdb " _
                                     & " INNER JOIN motivo_devolucion AS md ON mdb.IdmotivoDevolucion = md.IdMotivoDevolucion " _
                                     & " WHERE mdb.IdBodega =@IdBodega AND md.IdPropietario =@IdPropietario AND md.es_detalle=1"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMotivo_devolucion

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMotivo_devolucion
                            Obj.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Int32)

                            If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("Nombre"), String)
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
