Imports System.Text
Public Class clsUpdate

    Private clFList As New ArrayList
    Private clTable As String
    Private vWhere As String = ""

    Public Sub New()
        clFList.Clear()
        clTable = ""
    End Sub

    Public Sub Init(ByVal TableName As String)
        clFList.Clear()
        clTable = String.Format(" UPDATE {0} SET ", TableName)
    End Sub

    Public Sub Add(ByVal pField As String, ByVal pValue As Object)
        Add(pField, pValue, WMSTipoDato.Tipo.Parametro)
    End Sub

    ' Versión original de Add con DataType
    Public Sub Add(ByVal pField As String, ByVal pValue As Object, ByVal pTipo As WMSTipoDato.Tipo)

        If String.IsNullOrEmpty(pField) Then Return

        Dim SV As String = ""

        Try
            Select Case pTipo
                Case WMSTipoDato.Tipo.Fecha
                    SV = FormatoFechas.fFecha(Convert.ToDateTime(pValue))
                    clFList.Add($"{pField} = '{SV}'")

                Case WMSTipoDato.Tipo.FechaHora
                    SV = FormatoFechas.fFecha(Convert.ToDateTime(pValue), True)
                    clFList.Add($"{pField} = '{SV}'")

                Case WMSTipoDato.Tipo.Texto
                    SV = pValue.ToString()
                    clFList.Add($"{pField} = '{SV}'")

                Case WMSTipoDato.Tipo.Numero
                    SV = Convert.ToString(pValue)
                    clFList.Add($"{pField} = {SV}")

                Case WMSTipoDato.Tipo.Parametro
                    ' Asume que pValue ya está formateado como parámetro SQL
                    SV = pValue.ToString()
                    clFList.Add($"{pField} = {SV}")
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub Where(ByVal pWhere$)
        vWhere$ = " WHERE " & pWhere$
    End Sub

    Public Function SQL() As String
        ' Verifica si la tabla o la lista de campos están vacíos
        If String.IsNullOrEmpty(clTable) OrElse clFList.Count = 0 Then Return ""

        Try
            Dim vUpdate As New StringBuilder(clTable)

            ' Construye la cadena de actualización
            For i As Integer = 0 To clFList.Count - 1
                vUpdate.Append(clFList(i))
                If i < clFList.Count - 1 Then
                    vUpdate.Append(", ")
                End If
            Next

            ' Agrega la cláusula WHERE si existe
            If Not String.IsNullOrEmpty(vWhere) Then
                vUpdate.Append(vWhere)
            End If

            ' Devuelve la cadena construida
            Return vUpdate.ToString()

        Catch ex As Exception
            ' Opcionalmente, manejar o registrar la excepción aquí
            Return ""
        End Try
    End Function

End Class