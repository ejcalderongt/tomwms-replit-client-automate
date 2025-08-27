Public Class clsInsert

        Private ReadOnly clFList As New ArrayList
        Private ReadOnly clVList As New ArrayList
        Private clTable As String

        Public Sub New()
            clFList.Clear()
            clVList.Clear()
            clTable = ""
        End Sub

        Public Sub Init(ByVal TableName As String)
            clFList.Clear()
            clVList.Clear()
            clTable = TableName
        End Sub

        Public Sub Add(ByVal pField As String, ByVal pValue As Object, ByVal pTipo As String)
            Dim SV As String

            Try

                If (pField = "") Or (pTipo = "") Then Return

                If pTipo = "D" Then SV = FormatoFechas.fFecha(pValue) : clFList.Add(pField) : clVList.Add(SV)
                If pTipo = "H" Then SV = FormatoFechas.fFecha(pValue, True) : clFList.Add(pField) : clVList.Add(SV)
                If pTipo = "S" Then SV = String.Format("'{0}'", pValue) : clFList.Add(pField) : clVList.Add(SV)
                If pTipo = "N" Then SV = Val(pValue).ToString : clFList.Add(pField) : clVList.Add(SV)
                If pTipo = "F" Then SV = pValue : clFList.Add(pField) : clVList.Add(SV)
                If pTipo = "DT" Then
                    If IsDBNull(pValue) Or Trim(pValue) = "" Then
                        SV = "'01-Jan-1900 00:00:00'"
                    Else
                        SV = FormatoFechas.fFechaHora(pValue) : clFList.Add(pField) : clVList.Add(SV)
                    End If
                End If

            Catch ex As Exception
            EventLog.WriteEntry("Agora Fiscal", "Error " + ex.Message, EventLogEntryType.Error, 234)
        End Try

        End Sub

        Public Function SQL() As String

            Dim sVal, S, SF, SV As String

            If clTable = "" Then Return ""
            If clFList.Count = 0 Then Return ""

            Try

                SV = "" : SF = ""
                S = String.Format("INSERT INTO {0} (", clTable)

                For I = 0 To clFList.Count - 1
                    sVal = clFList.Item(I)
                    : If I < clFList.Count - 1 Then
                        SF = String.Format("{0}{1},", SF, sVal)
                    Else
                        SF = SF + sVal
                    End If
                    sVal = clVList.Item(I)
                    : If I < clFList.Count - 1 Then
                        SV = String.Format("{0}{1},", SV, sVal)
                    Else
                        SV = SV + sVal
                    End If
                Next

                S = String.Format("{0}{1}) VALUES ({2})", S, SF, SV)

                Return S

            Catch ex As Exception
                Return ""
            End Try

        End Function

    End Class

