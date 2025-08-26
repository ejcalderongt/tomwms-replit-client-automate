Imports System.Data.SqlClient

Partial Public Class clsLnTrans_tras_enc
    Implements IDisposable

    ''' <summary>
    ''' Funcion obtiene el nuevo indice de transaccion.
    ''' </summary>
    ''' <returns>Nuevo ID A utilizar para la transaccion</returns>
    ''' <remarks></remarks>
    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdTrasladoEnc),0) FROM trans_tras_enc"), lConnection)
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
            Return -1
        End Try

    End Function

    ''' <summary>
    ''' Funcion que muestra todos los translado
    ''' Filtrado por si esta activo o desactivado 
    ''' y por nombre en especifico
    ''' </summary>
    ''' <param name="pActivo">Parametro para definir si dsea mostrar las transacciones activas o eliminadas</param>
    ''' <param name="pFiltro">Buscar en la lista por este nombre  y los filtra</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListarTraslados(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

        Dim datos As New DataSet

        Dim lDataTable As New DataTable("Trans_tras_enc") '
        lDataTable.Columns.Add("Traslado", GetType(String))
        lDataTable.Columns.Add("Guia", GetType(String))
        lDataTable.Columns.Add("Propietario", GetType(String))
        lDataTable.Columns.Add("Bodega_Origen", GetType(String))
        lDataTable.Columns.Add("Bodega_Destino", GetType(String))
        lDataTable.Columns.Add("Muelle", GetType(String))
        lDataTable.Columns.Add("Fecha_Traslado", GetType(String))
        lDataTable.Columns.Add("Estado", GetType(String))
        ''Dim DT As New DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT tr.IdTrasladoEnc AS [Traslado],  tr.NoGuia AS [Guia],pr.nombre_comercial AS [Propietario],(SELECT bodega.nombre FROM trans_tras_enc as T2 INNER JOIN bodega ON bodega.IdBodega =T2.IdBodegaOrigen WHERE T2.IdTrasladoEnc =tr.IdTrasladoEnc ) AS [Bodega_Origen],(SELECT bodega.nombre FROM trans_tras_enc as T2 INNER JOIN bodega ON bodega.IdBodega =T2.IdBodegaDestino WHERE T2.IdTrasladoEnc =tr.IdTrasladoEnc) AS [Bodega_Destino],bodega_muelles.nombre AS [Muelle],tr.FechaTraslado AS [Fecha_Traslado],tr.estado as [Estado] " &
                                        " FROM trans_tras_enc AS tr INNER JOIN bodega ON (tr.IdBodegaOrigen = bodega.IdBodega) " &
                                        " INNER JOIN bodega AS b2 ON (tr.IdBodegaDestino= b2.IdBodega)" &
                                        " INNER JOIN propietarios AS pr ON tr.IdPropietarioBodega = pr.IdPropietario" &
                                        " INNER JOIN bodega_muelles ON bodega_muelles.IdMuelle = tr.IdMuelleOrigen" &
                                        " WHERE 1 > 0"

                If pActivo Then
                    vSQL += " AND tr.Activo=1"
                Else
                    vSQL += " AND tr.Activo=0"
                End If

                If String.IsNullOrEmpty(pFiltro) = False Then
                    vSQL += String.Format(" AND (tr.IdTrasladoEnc LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR tr.NoGuia LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR bodega.nombre LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR b2.nombre_comercial LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR pr.nombre_comercial LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR bodega_muelles.nombre LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR tr.FechaTraslado LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR tr.estado LIKE '%{0}%')", pFiltro)
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.Fill(lDataTable)
                    datos.Tables.Add(lDataTable)
                End Using

                Return lDataTable

            End Using

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
