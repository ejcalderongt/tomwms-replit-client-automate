Imports System.Reflection
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


End Class