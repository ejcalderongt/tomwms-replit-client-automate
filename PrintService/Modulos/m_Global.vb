Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Windows.Forms

Module m_Global

    Public vSQL As String = ""
    Public BD As New clsBD
    Public Property UsuarioAp As New clsBeUsuario
    Public Ins As New clsInsert
    Public Upd As New clsUpdate
    Public oWriteD As System.IO.StreamWriter
    Public Conexion As New clsCadenaConexion

    Public Sub CopyObject(Of tom)(ByVal ObjOrigen As Object, ByRef ObjDestino As tom)

        Try

            If ObjOrigen Is Nothing OrElse ObjDestino Is Nothing Then Return
            Dim TipoFuente As Type = ObjOrigen.[GetType]()
            Dim TipoDestino As Type = ObjDestino.[GetType]()

            If TipoFuente IsNot Nothing AndAlso TipoDestino IsNot Nothing Then

                For Each p As PropertyInfo In TipoFuente.GetProperties()

                    Dim ObjPI As PropertyInfo = TipoDestino.GetProperty(p.Name)

                    If ObjPI IsNot Nothing Then
                        Dim l As Object = p.GetValue(ObjOrigen, Nothing)
                        ObjPI.SetValue(ObjDestino, l)
                        ObjPI.SetValue(ObjDestino, p.GetValue(ObjOrigen, Nothing), Nothing)
                    End If

                Next

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Function ToDataTable(Of T)(items As List(Of T)) As DataTable
        Dim dataTable As New DataTable(GetType(T).Name)

        'Get all the properties
        Dim Props As PropertyInfo() = GetType(T).GetProperties(BindingFlags.[Public] Or BindingFlags.Instance)
        For Each prop As PropertyInfo In Props
            'Setting column names as Property names
            dataTable.Columns.Add(prop.Name)
        Next
        For Each item As T In items
            Dim values = New Object(Props.Length - 1) {}
            For i As Integer = 0 To Props.Length - 1
                'inserting property values to datatable rows
                values(i) = Props(i).GetValue(item, Nothing)
            Next
            dataTable.Rows.Add(values)
        Next
        'put a breakpoint here and check datatable
        Return dataTable
    End Function

    Public Function GetDBValue(ByVal pCampo As String, ByVal pTabla As String, ByVal pFiltro As String) As String

        Try

            Dim lResult As String
            Using lCnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

                Dim lCmd As New SqlCommand(String.Format("SELECT {0} FROM {1} WHERE {2}", pCampo, pTabla, pFiltro), lCnn) With {.CommandType = CommandType.Text}

                Dim lDT As New DataTable("Result")

                Using lDA As New SqlDataAdapter()
                    lDA.SelectCommand = lCmd
                    lDA.Fill(lDT)
                End Using

                If lDT.Rows.Count > 0 Then
                    If IsDBNull(lDT(0)(0)) Then
                        lResult = String.Empty
                    Else
                        lResult = lDT.Rows(0)(0)
                    End If
                Else
                    lResult = String.Empty
                End If

                Return lResult

            End Using

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

    End Function

    Public Function ObtenerImagen(ByVal pCampo As String, ByVal pTabla As String, ByVal pFiltro As String) As DataTable

        Try

            Using lCnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

                Dim lCmd As New SqlCommand(String.Format("SELECT {0} FROM {1} WHERE {2}", pCampo, pTabla, pFiltro), lCnn) With {.CommandType = CommandType.Text}

                Dim lDT As New DataTable("Result")
                Dim lDA As New SqlDataAdapter()
                lDA.SelectCommand = lCmd
                lDA.Fill(lDT)

                Return lDT

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function NITvalido(ByVal pNIT As String) As Boolean

        Dim POS As Integer
        Dim Correlativo As String
        Dim DigitoVerificador As String
        Dim Factor As Integer
        Dim Suma As Integer = 0
        Dim Valor As Integer = 0
        Dim X As Integer
        Dim xMOD11 As Double = 0
        Dim S As String = Nothing

        NITvalido = False

        Try

            POS = pNIT.IndexOf("-")
            If POS = -1 Then Exit Function
            Correlativo = pNIT.Substring(0, POS)

            DigitoVerificador = pNIT.Substring(POS + 1)
            Factor = Correlativo.Length + 1

            For X = 0 To (pNIT.IndexOf("-") - 1)
                Valor = Convert.ToInt32(pNIT.Substring(X, 1))
                Suma += (Valor * Factor)
                Factor -= 1
            Next

            xMOD11 = (11 - (Suma Mod 11)) Mod 11
            S = Convert.ToString(xMOD11)

            If (xMOD11 = 10 And DigitoVerificador = "K") Or (S = DigitoVerificador) Then
                NITvalido = True
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

    End Function

    Public Sub ShellandWait(ByVal ProcessPath As String, ByVal Args As String)

        Dim objProcess As Process

        Try

            objProcess = New Process()
            objProcess.StartInfo.FileName = ProcessPath
            objProcess.StartInfo.Arguments = Args
            objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            objProcess.Start()

            'Wait until the process passes back an exit code 
            objProcess.WaitForExit()

            'Free resources associated with this process
            objProcess.Close()

        Catch
            MessageBox.Show("Could not start process " & ProcessPath, "Error")
        End Try

    End Sub

    Public Function Existe_Ini(ByVal AppPath As String) As Boolean
        AppPath += "\"
        If IO.File.Exists(AppPath & "Conn.ini") Then
            Existe_Ini = True
        Else
            Existe_Ini = False
        End If
    End Function

End Module
