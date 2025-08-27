Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRegla_ubic_det_tp
    Implements IDisposable

    Friend Shared Function GetAllByIdReglaUbicacionEnc(idReglaUbicacionEnc As Integer) As List(Of clsBeRegla_ubic_det_tp)

        Try

            Dim lReturnList As New List(Of clsBeRegla_ubic_det_tp)

            Const sp As String = "SELECT * FROM Regla_ubic_det_tp" &
                " Where (IdReglaUbicacionEnc = @IdReglaUbicacionEnc)"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdReglaUbicacionEnc", idReglaUbicacionEnc))
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubic_det_tp As New clsBeRegla_ubic_det_tp

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubic_det_tp = New clsBeRegla_ubic_det_tp
                Cargar(vBeRegla_ubic_det_tp, dr)
                vBeRegla_ubic_det_tp.IsNew = False
                lReturnList.Add(vBeRegla_ubic_det_tp)

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

    Friend Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdReglaUbicacoinTP),0) FROM Regla_ubic_det_tp "

            Using lCommand As New SqlCommand(sp, pConnection)
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

    Public Shared Function Aplicar_Regla_Por_TipoProducto(ByVal IdBodega As Integer,
                                                          ByVal IdTipoProducto As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Boolean

        Aplicar_Regla_Por_TipoProducto = False

        Try

            Dim vSQL As String = "SELECT regla_ubic_enc.IdBodega, regla_ubic_det_tp.IdTipoProducto 
                                  FROM regla_ubic_det_tp INNER JOIN
                                       regla_ubic_enc ON regla_ubic_det_tp.IdReglaUbicacionEnc = regla_ubic_enc.IdReglaUbicacionEnc 
                                  Where(regla_ubic_enc.IdBodega = @IdBodega AND 
                                        regla_ubic_det_tp.IdTipoProducto = @IdTipoProducto AND 
                                        regla_ubic_det_tp.Activo = 1) "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTipoProducto", IdTipoProducto))
            Dim dt As New DataTable

            dad.Fill(dt)

            Aplicar_Regla_Por_TipoProducto = (dt.Rows.Count > 0)

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
