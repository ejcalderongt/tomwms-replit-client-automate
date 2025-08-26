Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraSplashScreen

Module m_Global

    Public vSQL As String = ""
    Public BD As New clsBD
    Public AP As New IMS
    Public Emp As New clsBeEmpresa
    Public Ins As New clsInsert
    Public Upd As New clsUpdate
    Public vRutaInterfaceNAV As String = CurDir() & "/NavSync.exe"
    Public vRutaInterfaceSAP As String = CurDir() & "/SAPBOSync.exe"
    Public vRutaServicio As String = CurDir() & "/WMS_PrintService.exe"
    Public vRutaInterfaceCEALSA As String = CurDir() & "/CEALSASync.exe"

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

    Public Function Ejecutar_Interface(ByVal ParametroEjecucion As String, ByVal frm As RibbonForm) As Boolean

        Dim vRutaInterface As String = ""
        Dim vNombre_Ejecutable As String

        Ejecutar_Interface = False

        Try

            vNombre_Ejecutable = clsLnI_nav_config_enc.Get_Nombre_Ejecutable(AP.IdConfiguracionInterface)

            vRutaInterface = CurDir() & "/" & vNombre_Ejecutable

            If IO.File.Exists(vRutaInterface) Then

                If ParametroEjecucion <> "" Then
                    ShellandWait(vRutaInterface, ParametroEjecucion, frm)
                Else
                    Dim startInfo As New ProcessStartInfo() With {
                            .FileName = vRutaInterface,
                            .Arguments = ParametroEjecucion}
                    Process.Start(startInfo)
                End If

                Ejecutar_Interface = True

            Else
                MessageBox.Show("No existe archivo de interface",
                        "Exec_MI3_Sync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation)
            End If

            'Select Case Ap.IdConfiguracionInterface

            '    Case 1 'Interface NAV

            '        If IO.File.Exists(vRutaInterfaceNAV) Then

            '            If ParametroEjecucion <> "" Then
            '                ShellandWait(vRutaInterfaceNAV, ParametroEjecucion, frm)
            '            Else
            '                Dim startInfo As New ProcessStartInfo() With {
            '                .FileName = vRutaInterfaceNAV}
            '                Process.Start(startInfo)
            '            End If

            '            Ejecutar_Interface = True

            '        Else
            '            MessageBox.Show("No existe archivo de interface",
            '            "Exec_MI3_Sync",
            '            MessageBoxButtons.OK,
            '            MessageBoxIcon.Exclamation)
            '        End If

            '    Case 2 'Interface SAP.

            '        If IO.File.Exists(vRutaInterfaceSAP) Then

            '            If ParametroEjecucion(0) <> "" Then
            '                ShellandWait(vRutaInterfaceSAP, ParametroEjecucion, frm)
            '            Else
            '                Dim startInfo As New ProcessStartInfo() With {
            '                .FileName = vRutaInterfaceSAP,
            '                .Arguments = ParametroEjecucion}
            '                Process.Start(startInfo)
            '            End If

            '            Ejecutar_Interface = True

            '        Else
            '            MessageBox.Show("No existe archivo de interface",
            '            "Exec_MI3_Sync",
            '            MessageBoxButtons.OK,
            '            MessageBoxIcon.Exclamation)
            '        End If

            '    Case 3 'Interface CEALSA.

            '        If IO.File.Exists(vRutaInterfaceCEALSA) Then

            '            If Trim(ParametroEjecucion(0)) <> "" Then
            '                ShellandWait(vRutaInterfaceCEALSA, ParametroEjecucion, frm)
            '            Else
            '                Dim startInfo As New ProcessStartInfo() With {
            '                .FileName = vRutaInterfaceCEALSA}
            '                Process.Start(startInfo)
            '            End If

            '            Ejecutar_Interface = True

            '        Else
            '            MessageBox.Show("No existe archivo de interface",
            '            "Exec_MI3_Sync",
            '            MessageBoxButtons.OK,
            '            MessageBoxIcon.Exclamation)
            '        End If

            'End Select



        Catch ex As Exception
            MessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Exec_MI3_Sync",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Public Function Ejecutar_Servicio(ByVal ParametroEjecucion As String, ByVal frm As RibbonForm) As Boolean

        Ejecutar_Servicio = False

        Try

            If IO.File.Exists(vRutaServicio) Then

                Dim aplicacioncorriendo As Process() = Process.GetProcessesByName("WMS_PrintService.exe")

                If aplicacioncorriendo.Length > 0 Then

                    MessageBox.Show("El servicio de impresión de etiquetas de WMS ya se encuentra en ejecución",
                    "WMS_PrintService.exe",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                    Return False

                Else

                    If ParametroEjecucion <> "" Then
                        ShellandWait(vRutaServicio, ParametroEjecucion, frm)
                    Else
                        Dim startInfo As New ProcessStartInfo() With {
                        .FileName = vRutaServicio,
                        .Arguments = clsBD.Instancia.Indice & " " & AP.IdEmpresa & " " & AP.IdBodega}
                        Process.Start(startInfo)
                    End If

                End If

                Ejecutar_Servicio = True

            Else
                MessageBox.Show("No existe el ejecutable para servicio de impresión de WMS.",
                "WMS_PrintService.exe",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Exec_PrintsService",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub ShellandWait(ByVal ProcessPath As String,
                             ByVal Args As String,
                             ByVal frm As RibbonForm)

        Dim objProcess As Process

        Try

            objProcess = New Process()
            objProcess.StartInfo.FileName = ProcessPath
            objProcess.StartInfo.Arguments = Args
            objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            objProcess.Start()

            SplashScreenManager.ShowForm(frm, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Esperando a que la interface se cierre...")

            'Wait until the process passes back an exit code 
            'objProcess.WaitForExit()

            'Free resources associated with this process
            'objProcess.Close()

        Catch
            MessageBox.Show("Could not start process " & ProcessPath, "Error")
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

End Module
