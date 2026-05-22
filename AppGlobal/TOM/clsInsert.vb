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

    Public Function Execute(ByVal pConnection As SqlConnection,
                            ByVal pTransaction As SqlTransaction,
                            Optional ByVal pBatchSize As Integer = 1000,
                            Optional ByVal pTimeout As Integer = 0,
                            Optional ByVal pOptions As SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints Or SqlBulkCopyOptions.FireTriggers) As Integer

        If String.IsNullOrWhiteSpace(clTable) Then Throw New Exception("Tabla batch no definida.")
        If clRow IsNot Nothing Then EndRow()
        If clData Is Nothing OrElse clData.Rows.Count = 0 Then Return 0

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

                For Each col As BatchColumn In clColumnas
                    bulk.ColumnMappings.Add(col.Nombre, col.Nombre)
                Next

                bulk.WriteToServer(clData)
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
