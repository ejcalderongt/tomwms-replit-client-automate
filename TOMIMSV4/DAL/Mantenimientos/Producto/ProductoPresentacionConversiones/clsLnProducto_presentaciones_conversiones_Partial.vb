Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_Presentaciones_conversiones
    Implements IDisposable

    Public Shared Function Exists(ByVal pidConversion As Integer, ByVal pIdPresentacionOrigen As Integer) As Boolean

        Dim lExists As Boolean = False

        Dim vSQL As String = "SELECT COUNT(1) FROM producto_presentaciones_conversiones WHERE IdConversion=@IdConversion AND IdPresentacionOrigen=@IdPresentacionOrigen"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdConversion", pidConversion)
                    lCommand.Parameters.AddWithValue("@IdPresentacionOrigen", pIdPresentacionOrigen)

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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdConversion),0) FROM producto_presentaciones_conversiones"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Delete(ByVal pIdConversion As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("UPDATE producto_presentaciones_conversiones SET activo=0 WHERE IdConversion={0}", pIdConversion), lConnection)

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

    Public Shared Function GetFactor(ByVal pidPresentacionDestino As Integer, ByVal pIdPresentacionOrigen As Integer) As Boolean

        Dim lFactor As Double = 0

        Dim vSQL As String = "SELECT factor FROM producto_presentaciones_conversiones WHERE IdPresentacionDestino=@IdPresentacionDestino AND IdPresentacionOrigen=@IdPresentacionOrigen"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdPresentacionDestino", pidPresentacionDestino)
                    lCommand.Parameters.AddWithValue("@IdPresentacionOrigen", pIdPresentacionOrigen)

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lFactor = lReturnValue
                    End If

                End Using

            End Using

            Return lFactor

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdPresentacionOrigen As Integer,
                                     ByVal IdPresentacionDestino As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeProducto_presentaciones_conversiones

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Producto_presentaciones_conversiones" &
            " Where(IdPresentacionOrigen = @IdPresentacionOrigen
            AND IdPresentacionDestino = @IdPresentacionDestino)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPresentacionOrigen", IdPresentacionOrigen))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPresentacionDestino", IdPresentacionDestino))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeProducto_presentaciones_conversiones As New clsBeProducto_presentaciones_conversiones
                Cargar(pBeProducto_presentaciones_conversiones, dt.Rows(0))
                Return pBeProducto_presentaciones_conversiones
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdPresentacionOrigen As Integer,
                                     ByVal IdPresentacionDestino As Integer) As clsBeProducto_presentaciones_conversiones

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Producto_presentaciones_conversiones" &
            " Where(IdPresentacionOrigen = @IdPresentacionOrigen
            AND IdPresentacionDestino = @IdPresentacionDestino)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPresentacionOrigen", IdPresentacionOrigen))
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPresentacionDestino", IdPresentacionDestino))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Dim pBeProducto_presentaciones_conversiones As New clsBeProducto_presentaciones_conversiones
                    Cargar(pBeProducto_presentaciones_conversiones, dt.Rows(0))
                    Return pBeProducto_presentaciones_conversiones
                End If

            End Using

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pidConversion As Integer) As clsBeProducto_presentaciones_conversiones

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Producto_presentaciones_conversiones" &
            " Where(IdConversion = @IdConversion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCONVERSION", pidConversion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeProducto_presentaciones_conversiones As New clsBeProducto_presentaciones_conversiones
                Cargar(pBeProducto_presentaciones_conversiones, dt.Rows(0))
                Return pBeProducto_presentaciones_conversiones
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
