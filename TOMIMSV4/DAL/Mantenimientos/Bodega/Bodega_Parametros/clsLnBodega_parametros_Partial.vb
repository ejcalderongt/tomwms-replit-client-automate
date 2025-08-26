Imports System.Data.SqlClient
Imports System.Reflection

Partial Class clsLnBodega_parametros
    Implements IDisposable

    Public Shared Function Listar() As DataTable
        Try
            Const sp As String = "Select idParametroBodega as Correlativo,Codigo,Nombre,Descripcion from bodega_parametros"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Function

    Public Shared Function GetSingle(ByVal pIdParametroBodega As Integer) As clsBeBodega_parametros


        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1625pm: Quité String.Format.
                Dim vSQL As String = "SELECT * FROM   bodega_parametros  WHERE IdParametroBodega=@IdParametroBodega"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdParametroBodega", pIdParametroBodega)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeBodega_parametros


                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Obj = New clsBeBodega_parametros()

                        Obj.IdParametroBodega = CType(lRow("IdParametroBodega"), Int32)

                        If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                            Obj.Codigo = CType(lRow("Codigo"), String)
                        End If
                        If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                            Obj.Nombre = CType(lRow("Nombre"), String)
                        End If
                        If lRow("Descripcion") IsNot DBNull.Value AndAlso lRow("Descripcion") IsNot Nothing Then
                            Obj.Descripcion = CType(lRow("Descripcion"), String)
                        End If

                        Return Obj

                    End If
                End Using
            End Using

            Return Nothing

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


