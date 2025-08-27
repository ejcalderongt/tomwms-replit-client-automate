Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnFont_enc
    Implements IDisposable

    Public Shared Function GetSingleByIdFontEnc(ByVal IdFontEnc As Integer) As clsBeFont_Enc

        Try

            Const sp As String = "SELECT * FROM Font_enc WHERE IdFontEnc = @IdFontEnc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdFontEnc", IdFontEnc)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeFont_enc As New clsBeFont_Enc

            For Each dr As DataRow In dt.Rows

                vBeFont_enc = New clsBeFont_Enc
                Cargar(vBeFont_enc, dr)
                vBeFont_enc.IsNew = False

                vBeFont_enc.lDet = clsLnFont_det.GetAllByIdFontEnc(vBeFont_enc.IdFontEnc)

                For Each FD As clsBeFont_det In vBeFont_enc.lDet
                    vBeFont_enc.IsNew = False
                Next

            Next

            Return vBeFont_enc

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingleByIdFontEnc(ByVal IdFontEnc As Integer,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As clsBeFont_Enc

        Try

            Const sp As String = "SELECT * FROM Font_enc WHERE IdFontEnc = @IdFontEnc"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdFontEnc", IdFontEnc)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeFont_enc As New clsBeFont_Enc

            For Each dr As DataRow In dt.Rows

                vBeFont_enc = New clsBeFont_Enc
                Cargar(vBeFont_enc, dr)
                vBeFont_enc.IsNew = False

                vBeFont_enc.lDet = clsLnFont_det.GetAllByIdFontEnc(vBeFont_enc.IdFontEnc)

                For Each FD As clsBeFont_det In vBeFont_enc.lDet
                    vBeFont_enc.IsNew = False
                Next

            Next

            Return vBeFont_enc

            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0


            Const sp As String = "SELECT ISNULL(Max(IdFontEnc),0) FROM Font_enc"
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

    Public Shared Function Guardar_Font(ByRef oBeFont_enc As clsBeFont_enc) As Boolean

        Guardar_Font = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If oBeFont_enc.IsNew Then
                oBeFont_enc.IdFontEnc = MaxID(lConnection, lTransaction) + 1
                Insertar(oBeFont_enc, lConnection, lTransaction)
            Else
                Actualizar(oBeFont_enc, lConnection, lTransaction)
            End If

            Dim lMaxID As Integer = clsLnProducto_parametros.MaxID(lConnection, lTransaction)
            For Each FD As clsBeFont_det In oBeFont_enc.lDet
                If FD.IsNew Then
                    lMaxID += 1
                    FD.IdFontDet = lMaxID
                    FD.IdFontEnc = oBeFont_enc.IdFontEnc
                    clsLnFont_det.Insertar(FD, lConnection, lTransaction)
                Else
                    clsLnFont_det.Actualizar(FD, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

            Guardar_Font = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
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
