Imports System.Data.SqlClient
Imports System.Reflection
Imports TOMWMS.wsTablaConversiones

Public Class clsSyncNavTablaConversion : Inherits clsInterfaceBase
    Implements IDisposable

    Private fichaConversiones() As Tabla_Conversiones

    Public Shared wsTablaConv As New Tabla_Conversiones_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Public Function Get_Tabla_Conversiones_FromWS(ByVal pItemNo As String, ByVal Codigo As String) As Tabla_Conversiones

        Try

            Return wsTablaConv.Read(pItemNo, Codigo)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Tabla_Conversiones_FromWS() As Tabla_Conversiones()

        Try

            Return wsTablaConv.ReadMultiple(Nothing, Nothing, 0)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Lista_Tabla_Conversiones_FromWS() As List(Of Tabla_Conversiones)

        Try

            wsTablaConv.Url = My.Settings.NavSync_wsTablaConversiones_Tabla_Conversiones_Service

            Return wsTablaConv.ReadMultiple(Nothing, Nothing, 0).ToList()

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Single_Conversiones_FromWS() As Tabla_Conversiones

        Try

            Return wsTablaConv.Read("00020500", "CJ")

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Conversiones_By_Codigo_Producto_FromWS(ByVal pCodigoProducto As String) As List(Of clsBeI_nav_conversion)

        Try

            Dim vFiltro1 As New Tabla_Conversiones_Filter() With {.Field = Tabla_Conversiones_Fields.Item_No, .Criteria = pCodigoProducto}
            Dim vFiltros As Tabla_Conversiones_Filter() = New Tabla_Conversiones_Filter() {vFiltro1}
            Dim lTablaConversioensNAV() As Tabla_Conversiones
            lTablaConversioensNAV = wsTablaConv.ReadMultiple(vFiltros, "", 0)

            Dim BeINavConversion As New clsBeI_nav_conversion
            Dim lINavConversion As New List(Of clsBeI_nav_conversion)

            For Each UM In lTablaConversioensNAV
                BeINavConversion = New clsBeI_nav_conversion()
                CopyObject(UM, BeINavConversion)
                lINavConversion.Add(BeINavConversion)
            Next

            Return lINavConversion

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Public Function Importar_Conversiones_DesdeWSNav_A_TOMIMS(ByVal lblprg As RichTextBox,
                                                              ByRef prg As ProgressBar) As Boolean
        Importar_Conversiones_DesdeWSNav_A_TOMIMS = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeINavConversion As New clsBeI_nav_conversion
        Dim vContador As Integer = 0
        Dim VContadorBitacoraIntermedia As Integer = 0
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** INICIANDO INSERCIÓN EN TABLA TOMIMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando conversiones desde webservice.")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            CnnLog.Open()

            fichaConversiones = Get_Tabla_Conversiones_FromWS()

            BeNavEjecucionRes.Registros_ws = fichaConversiones.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

            Application.DoEvents()

            lblprg.AppendText(String.Format("registros encontrados en WS: {0} ", fichaConversiones.Count))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            prg.Maximum = fichaConversiones.Count

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_conversion.Eliminar_Todos(lConnection, lTransaction)

            If CnnLog.State = ConnectionState.Closed Then CnnLog.Open()

            Dim UnidMedConv As Tabla_Conversiones

            For Each Conv As Tabla_Conversiones In fichaConversiones

                UnidMedConv = Conv

                Try

                    BeINavConversion = New clsBeI_nav_conversion

                    CopyObject(Conv, BeINavConversion)

                    lblprg.AppendText(String.Format("Procesando registro: {0} ", BeINavConversion.Item_No & " - " & BeINavConversion.Code, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BeINavConversion.IdConversion = clsLnI_nav_conversion.MaxID(lConnection, lTransaction) + 1

                    clsLnI_nav_conversion.Insertar(BeINavConversion, lConnection, lTransaction)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    'Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          BeINavConversion.Code,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet, CnnLog)

                    lblprg.AppendText("Error al insertar desde ws a intermedia: " & BeINavConversion.Code & vbNewLine &
                                                   ex.Message)

                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            Next

            lTransaction.Commit()

            lblprg.AppendText("********** FIN DE INSERCIÓN ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Conversiones_DesdeWSNav_A_TOMIMS = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, CnnLog)

            lblprg.AppendText(String.Format("Error al insertar proveedores desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not CnnLog Is Nothing AndAlso CnnLog.State = ConnectionState.Open Then CnnLog.Close()
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
        If wsTablaConv IsNot Nothing Then
            wsTablaConv.Dispose()
            wsTablaConv = Nothing
        End If
    End Sub
#End Region

End Class
