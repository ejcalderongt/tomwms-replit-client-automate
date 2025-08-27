Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnCliente_tipo
    Implements IDisposable

    'Public Shared Function GetSingle(ByVal pIdMotivoAnulacion As Integer) As clsBeCliente_tipo        

    '    Try

    '        Dim vSQL As String = "SELECT TOP 1 * FROM cliente_tipo WHERE IdTipoCliente=@IdTipoCliente"

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '                lDTA.SelectCommand.CommandType = CommandType.Text
    '                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoCliente", pIdMotivoAnulacion)

    '                Dim lDT As New DataTable()
    '                lDTA.Fill(lDT)

    '                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

    '                    Dim lRow As DataRow = lDT.Rows(0)
    '                    Dim Obj As New clsBeCliente_tipo()

    '                    Cargar(Obj, lRow)

    '                    Return Obj

    '                End If

    '            End Using

    '        End Using

    '        Return Nothing

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeCliente_tipo)

        Dim lReturnList As New List(Of clsBeCliente_tipo)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_ClienteTipo WHERE 1 > 0"

                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeCliente_tipo

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeCliente_tipo
                            Obj.Propietario = New clsBePropietarios
                            Cargar(Obj, lRow)
                            Obj.IdTipoCliente = CType(lRow("IdTipoCliente"), Int32)

                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                Obj.Propietario.Nombre_comercial = CType(lRow("IdPropietario"), String)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

        Return lReturnList

    End Function

    Public Shared Function MaxIDClienteTipo() As Integer

        Dim lMax As Integer = 0

        Dim vSQL As String = "SELECT ISNULL(Max(IdTipoCliente),0) FROM cliente_tipo"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

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

    Public Shared Function Existe_By_IdPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM cliente_tipo WHERE IdPropietario=@IdPropietario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietarioBodega)

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
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeCliente_tipo)

        Try

            Dim lReturnList As New List(Of clsBeCliente_tipo)
            Const sp As String = "SELECT * FROM Cliente_tipo WHERE Activo = @Activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeCliente_tipo As New clsBeCliente_tipo

            For Each dr As DataRow In dt.Rows

                vBeCliente_tipo = New clsBeCliente_tipo
                Cargar(vBeCliente_tipo, dr)
                lReturnList.Add(vBeCliente_tipo)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo(ByVal pActivo As Boolean) As DataTable

        Try

            Const sp As String = "SELECT IdTipoCliente,NombreTipoCliente FROM Cliente_tipo WHERE Activo = @Activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdMotivoAnulacion As Integer) As clsBeCliente_tipo

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM cliente_tipo WHERE IdTipoCliente=@IdTipoCliente"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoCliente", pIdMotivoAnulacion)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeCliente_tipo()

                        Cargar(Obj, lRow)

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTipoCliente),0) FROM cliente_tipo"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

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