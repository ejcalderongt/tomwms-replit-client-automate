Imports System.Reflection
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Public Class clsInsert

    Private ReadOnly clFList As New List(Of String)
    Private ReadOnly clVList As New List(Of String)
    Private clTable As String
    Public Sub New()
        Reset()
    End Sub

    Public Sub Init(ByVal TableName As String)
        Reset()
        clTable = TableName
    End Sub

    Private Sub Reset()
        clFList.Clear()
        clVList.Clear()
        clTable = String.Empty
    End Sub

    Public Sub Add(ByVal pField As String, ByVal pValue As Object)
        Add(pField, pValue, WMSTipoDato.Tipo.Parametro)
    End Sub

    Public Sub Add(ByVal pField As String, ByVal pValue As Object, ByVal pTipo As WMSTipoDato.Tipo)
        If String.IsNullOrEmpty(pField) Then Return

        Dim SV As String = ""
        Dim fieldEntry As String = ""
        Dim valueEntry As String = ""

        Try
            Select Case pTipo
                Case WMSTipoDato.Tipo.Fecha
                    SV = FormatoFechas.fFecha(Convert.ToDateTime(pValue))
                    fieldEntry = pField
                    valueEntry = $"'{SV}'"

                Case WMSTipoDato.Tipo.FechaHora
                    SV = FormatoFechas.fFecha(Convert.ToDateTime(pValue), True)
                    fieldEntry = pField
                    valueEntry = $"'{SV}'"

                Case WMSTipoDato.Tipo.Texto
                    SV = pValue.ToString()
                    fieldEntry = pField
                    valueEntry = $"'{SV}'"

                Case WMSTipoDato.Tipo.Numero
                    SV = Convert.ToString(pValue)
                    fieldEntry = pField
                    valueEntry = SV

                Case WMSTipoDato.Tipo.Parametro
                    SV = pValue.ToString()
                    fieldEntry = pField
                    valueEntry = SV

            End Select

            clFList.Add(fieldEntry)
            clVList.Add(valueEntry)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function SQL() As String

        If String.IsNullOrEmpty(clTable) OrElse clFList.Count = 0 Then Return String.Empty

        Try
            Dim fields As New StringBuilder()
            Dim values As New StringBuilder()

            For i As Integer = 0 To clFList.Count - 1
                fields.Append(clFList(i))
                values.Append(clVList(i))

                If i < clFList.Count - 1 Then
                    fields.Append(", ")
                    values.Append(", ")
                End If
            Next

            Return $"INSERT INTO {clTable} ({fields}) VALUES ({values})"

        Catch ex As Exception
            Dim errorMsg As String = $"Error en {MethodBase.GetCurrentMethod().Name}: {ex.Message}"
            Throw New Exception(errorMsg)
        End Try
    End Function

    Public Function SQLIdentity(ByVal identityField As String) As String
        If String.IsNullOrEmpty(clTable) OrElse clFList.Count = 0 Then Return String.Empty
        If String.IsNullOrWhiteSpace(identityField) Then Throw New Exception("identityField es requerido.")

        Try
            Dim fields As New StringBuilder()
            Dim values As New StringBuilder()

            For i As Integer = 0 To clFList.Count - 1
                fields.Append(clFList(i))
                values.Append(clVList(i))

                If i < clFList.Count - 1 Then
                    fields.Append(", ")
                    values.Append(", ")
                End If
            Next

            Return $"INSERT INTO {clTable} ({fields}) OUTPUT INSERTED.{identityField} VALUES ({values})"

        Catch ex As Exception
            Dim errorMsg As String = $"Error en {MethodBase.GetCurrentMethod().Name}: {ex.Message}"
            Throw New Exception(errorMsg)
        End Try
    End Function

End Class

Public Class clsInsertBatch

    Private Class BatchColumn
        Public Property Nombre As String
        Public Property Tipo As Type
    End Class

    Private ReadOnly clColumnas As New List(Of BatchColumn)
    Private clTable As String = ""
    Private clData As System.Data.DataTable
    Private clRow As System.Data.DataRow

    Public Sub New()
        Reset()
    End Sub

    Public Sub Init(ByVal TableName As String)
        Reset()
        clTable = If(TableName, "").Trim()
        clData.TableName = clTable
    End Sub

    Private Sub Reset()
        clColumnas.Clear()
        clTable = String.Empty
        clData = New System.Data.DataTable("Batch")
        clRow = Nothing
    End Sub

    Public Sub AddColumn(ByVal pField As String, ByVal pTipo As Type)
        If String.IsNullOrWhiteSpace(pField) Then Return
        If pTipo Is Nothing Then pTipo = GetType(String)
        If clData.Columns.Contains(pField) Then Return

        clColumnas.Add(New BatchColumn With {.Nombre = pField, .Tipo = pTipo})
        clData.Columns.Add(pField, pTipo)
    End Sub

    Public Sub BeginRow()
        If clData.Columns.Count = 0 Then Throw New Exception("Debe agregar columnas antes de iniciar una fila batch.")
        clRow = clData.NewRow()
    End Sub

    Public Sub Add(ByVal pField As String, ByVal pValue As Object)
        If clRow Is Nothing Then BeginRow()
        If String.IsNullOrWhiteSpace(pField) OrElse Not clData.Columns.Contains(pField) Then Return

        If pValue Is Nothing Then
            clRow(pField) = DBNull.Value
        Else
            clRow(pField) = pValue
        End If
    End Sub

    Public Sub EndRow()
        If clRow Is Nothing Then Return
        clData.Rows.Add(clRow)
        clRow = Nothing
    End Sub

    Public ReadOnly Property Count As Integer
        Get
            Return If(clData Is Nothing, 0, clData.Rows.Count)
        End Get
    End Property

    '#EJC20260523_INSERTBATCH_SCHEMA_SAFE: en clientes con schema parcial, SqlBulkCopy debe mapear solo columnas existentes.
    Private Function GetDestinationColumns(ByVal pConnection As SqlConnection,
                                           ByVal pTransaction As SqlTransaction) As HashSet(Of String)

        Dim vColumnas As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim vSchema As String = ""
        Dim vTable As String = clTable

        If clTable.Contains(".") Then
            Dim vPartes = clTable.Split("."c)
            If vPartes.Length >= 2 Then
                vSchema = vPartes(vPartes.Length - 2).Replace("[", "").Replace("]", "")
                vTable = vPartes(vPartes.Length - 1).Replace("[", "").Replace("]", "")
            End If
        End If

        Using vCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@TableName AND (@SchemaName='' OR TABLE_SCHEMA=@SchemaName)", pConnection, pTransaction)
            vCmd.CommandType = CommandType.Text
            vCmd.Parameters.AddWithValue("@TableName", vTable.Replace("[", "").Replace("]", ""))
            vCmd.Parameters.AddWithValue("@SchemaName", vSchema)

            Using vReader As SqlDataReader = vCmd.ExecuteReader()
                While vReader.Read()
                    vColumnas.Add(vReader.Item("COLUMN_NAME").ToString())
                End While
            End Using
        End Using

        Return vColumnas

    End Function

    Private Sub TraceColumnasOmitidas(ByVal pColumnasOmitidas As List(Of String))
        If pColumnasOmitidas Is Nothing OrElse pColumnasOmitidas.Count = 0 Then Return

        Try
            Dim vDir As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "TOMWMS")
            If Not System.IO.Directory.Exists(vDir) Then System.IO.Directory.CreateDirectory(vDir)

            Dim vPath As String = System.IO.Path.Combine(vDir, "insert-batch-trace.log")
            Dim vLinea As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") &
                                   "|#EJC20260523_INSERTBATCH_SCHEMA_SAFE" &
                                   "|Tabla=" & clTable &
                                   "|ColumnasOmitidas=" & String.Join(",", pColumnasOmitidas)

            System.IO.File.AppendAllText(vPath, vLinea & Environment.NewLine, System.Text.Encoding.UTF8)
            System.Diagnostics.Debug.WriteLine(vLinea)
        Catch
        End Try

    End Sub

    Public Function Execute(ByVal pConnection As SqlConnection,
                            ByVal pTransaction As SqlTransaction,
                            Optional ByVal pBatchSize As Integer = 1000,
                            Optional ByVal pTimeout As Integer = 0,
                            Optional ByVal pOptions As SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints Or SqlBulkCopyOptions.FireTriggers,
                            Optional ByVal pCancelar As Func(Of Boolean) = Nothing,
                            Optional ByVal pProgreso As Action(Of Long) = Nothing) As Integer

        If String.IsNullOrWhiteSpace(clTable) Then Throw New Exception("Tabla batch no definida.")
        If clRow IsNot Nothing Then EndRow()
        If clData Is Nothing OrElse clData.Rows.Count = 0 Then Return 0
        If pCancelar IsNot Nothing AndAlso pCancelar.Invoke() Then Throw New OperationCanceledException("#EJC20260523_INSERTBATCH_CANCEL: operación batch cancelada antes de iniciar.")

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try
            Dim vConnection As SqlConnection = pConnection
            Dim vTransaction As SqlTransaction = pTransaction

            If Not Es_Transaccion_Remota Then
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                vConnection = lConnection
                vTransaction = lTransaction
            End If

            Using bulk As New SqlBulkCopy(vConnection, pOptions, vTransaction)
                bulk.DestinationTableName = clTable
                bulk.BatchSize = If(pBatchSize <= 0, 1000, pBatchSize)
                bulk.BulkCopyTimeout = pTimeout
                '#EJC20260523_INSERTBATCH_CANCEL: SqlBulkCopy permite abortar cooperativamente entre paquetes.
                bulk.NotifyAfter = bulk.BatchSize
                AddHandler bulk.SqlRowsCopied,
                    Sub(sender As Object, e As SqlRowsCopiedEventArgs)
                        If pProgreso IsNot Nothing Then pProgreso.Invoke(e.RowsCopied)
                        If pCancelar IsNot Nothing AndAlso pCancelar.Invoke() Then e.Abort = True
                    End Sub

                Dim vColumnasDestino As HashSet(Of String) = GetDestinationColumns(vConnection, vTransaction)
                Dim vColumnasOmitidas As New List(Of String)

                For Each col As BatchColumn In clColumnas
                    If vColumnasDestino.Contains(col.Nombre) Then
                        bulk.ColumnMappings.Add(col.Nombre, col.Nombre)
                    Else
                        vColumnasOmitidas.Add(col.Nombre)
                    End If
                Next

                If bulk.ColumnMappings.Count = 0 Then
                    Throw New Exception("No hay columnas compatibles para ejecutar batch sobre tabla: " & clTable)
                End If

                TraceColumnasOmitidas(vColumnasOmitidas)

                bulk.WriteToServer(clData)
                If pCancelar IsNot Nothing AndAlso pCancelar.Invoke() Then Throw New OperationCanceledException("#EJC20260523_INSERTBATCH_CANCEL: operación batch cancelada.")
            End Using

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return clData.Rows.Count

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
