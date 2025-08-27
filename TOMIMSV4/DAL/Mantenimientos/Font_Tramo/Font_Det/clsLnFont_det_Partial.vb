Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnFont_det
    Implements IDisposable

    Public Shared Function GetAllByIdFontEnc(ByVal IdFontEnc As Integer) As List(Of clsBeFont_det)

        Dim lReturnList As List(Of clsBeFont_det) = Nothing

        Try

            Const sp As String = "SELECT * FROM Font_det WHERE IdFontEnc = @IdFontEnc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdFontEnc", IdFontEnc))
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeFont_det As New clsBeFont_det

            If dt.Rows.Count > 0 Then

                lReturnList = New List(Of clsBeFont_det)

                For Each dr As DataRow In dt.Rows

                    vBeFont_det = New clsBeFont_det
                    Cargar(vBeFont_det, dr)
                    lReturnList.Add(vBeFont_det)

                Next

            End If

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

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