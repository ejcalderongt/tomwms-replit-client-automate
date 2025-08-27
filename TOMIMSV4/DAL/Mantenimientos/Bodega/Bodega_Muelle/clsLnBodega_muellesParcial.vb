Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnBodega_muelles
    Implements IDisposable

    Public Shared Function Es_ValidoMuelle_By_CodigoBarra(ByVal IdOBarra As String) As String

        Es_ValidoMuelle_By_CodigoBarra = ""

        Try

            Const sp As String = " SELECT nombre FROM Bodega_muelles " &
                                 " Where (IdMuelle = @IdMuelle) OR (codigo_barra = @codigo_barra)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdMuelle", IdOBarra))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@codigo_barra", IdOBarra))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 0 Then
                Throw New Exception("No es válido el muelle")
            End If

            Return dt.Rows(0).Item("nombre")

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByBodega(ByVal pIdBodega As Integer) As List(Of clsBeBodega_muelles)

        Try

            Dim lReturnList As New List(Of clsBeBodega_muelles)
            Const sp As String = "SELECT * FROM Bodega_muelles WHERE IdBodega =@IdBodega AND Activo = 1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega_muelles As New clsBeBodega_muelles

            For Each dr As DataRow In dt.Rows

                vBeBodega_muelles = New clsBeBodega_muelles
                Cargar(vBeBodega_muelles, dr)
                lReturnList.Add(vBeBodega_muelles)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList



        Catch ex As Exception
            Throw New Exception("Bodega_muelles_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_For_Combo(ByVal pIdBodega As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_By_IdBodega_For_Combo = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT IdMuelle,nombre, IdubicacionDefecto, dbo.Nombre_Completo_Ubicacion(IdUbicacionDefecto,IdBodega) AS Ubicacion  
                                  FROM Bodega_muelles WHERE IdBodega =@IdBodega AND Activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            lTransaction.Commit()
            lConnection.Close()

            Get_All_By_IdBodega_For_Combo = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception("Bodega_muelles_GetAll: " & ex.Message)
        Finally
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_For_Combo(ByVal pIdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Const sp As String = "SELECT IdMuelle,nombre, IdubicacionDefecto, dbo.Nombre_Completo_Ubicacion(IdUbicacionDefecto,IdBodega) AS Ubicacion  
                                  FROM Bodega_muelles WHERE IdBodega =@IdBodega AND Activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Bodega_muelles_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function Listar(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

        Listar = Nothing

        Try

            Dim sp As String = "SELECT * FROM VW_BodegaMuelle WHERE 1 > 0 "

            If pActivo Then
                sp += " AND Activo=1"
            Else
                sp += " AND Activo=0"
            End If

            If Not String.IsNullOrEmpty(pFiltro) Then
                sp += String.Format(" AND (código LIKE '%{0}%'", pFiltro)
                sp += String.Format(" OR Bodega LIKE '%{0}%'", pFiltro)
                sp += String.Format(" OR Muelle LIKE '%{0}%')", pFiltro)
            End If

            sp += " ORDER BY código "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Listar = dt
            End If

            'Return dt

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
