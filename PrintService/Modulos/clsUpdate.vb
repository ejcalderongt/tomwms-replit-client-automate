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

        Public Sub Add(ByVal pField As String, ByVal pValue As Object, ByVal pTipo As String)
            Dim SV As String

            Try

                If (pField = "") Or (pTipo = "") Then Return

                If pTipo = "D" Then SV = FormatoFechas.fFecha(pValue) : clFList.Add(String.Format("{0} = {1}", pField, SV))
                If pTipo = "H" Then SV = FormatoFechas.fFecha(pValue, True) : clFList.Add(String.Format("{0} = {1}", pField, SV))
                If pTipo = "S" Then SV = String.Format("'{0}'", pValue) : clFList.Add(String.Format("{0} = {1}", pField, SV))
                If pTipo = "N" Then SV = Val(pValue) : clFList.Add(String.Format("{0} = {1}", pField, SV))
                If pTipo = "F" Then SV = pValue : clFList.Add(String.Format("{0} = {1}", pField, SV))

            Catch ex As Exception
            EventLog.WriteEntry("Agora Fiscal", "Error " + ex.Message, EventLogEntryType.Error, 234)
        End Try
        End Sub

        Public Sub Where(ByVal pWhere$)
            vWhere$ = " WHERE " & pWhere$
        End Sub

        Public Function SQL() As String

            Dim vUpDate$

            If clTable = "" Then Return ""
            If clFList.Count = 0 Then Return ""

            Try
                vUpDate$ = clTable
                For I = 0 To clFList.Count - 1
                    vUpDate$ = vUpDate$ & IIf(I < (clFList.Count - 1), (clFList(I) & ","), clFList(I))
                Next
                vUpDate$ = vUpDate$ & vWhere$
                Return vUpDate$

            Catch ex As Exception
                Return ""
            End Try

        End Function

    End Class
