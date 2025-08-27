Imports System.Data.SqlClient
Imports System.Reflection
Imports TOMWMS.WSDimensiones

Public Class clsSyncCentrosCosto : Inherits clsInterfaceBase
    Implements IDisposable

    Private Shared fichaDimensiones() As Dimensiones
    Shared VContadorBitacoraTomims As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Private BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Private Shared wsDimensionesService As New Dimensiones_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsDimensionesService IsNot Nothing Then
            wsDimensionesService.Dispose()
            wsDimensionesService = Nothing
        End If
    End Sub

    Public Shared Function Importar_Centros_Costo_DesdeWSNav(ByVal lblprg As RichTextBox,
                                                             ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Importar_Centros_Costo_DesdeWSNav = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Try

            wsDimensionesService.Url = BD.Instancia.WSDimensiones

            lblprg.AppendText("Consultando WebService de Dimensiones en: " & wsDimensionesService.Url)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            '#EJC20220330: Este desarrollo se concedió a  BYB (no se cobró), 
            'Se realizó en la última semana previo a la salida en vivo y no se tenía tiempo para modelar el comportamiento
            'de filtros parametrizables, por eso no lo son.
            'Las modificaciones futuras a este método deberían ser cobradas...
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** APLICANDO FILTROS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** (No Parametrizables) ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vFiltro1 As New Dimensiones_Filter() With {.Field = Dimensiones_Fields.Dimension_Code, .Criteria = 1}

            lblprg.AppendText(vbTab & "-Dimension_Code-")
            lblprg.AppendText(vbTab & "Criteria: 1")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vFiltro2 As New Dimensiones_Filter() With {.Field = Dimensiones_Fields.Name, .Criteria = "CeCo*"}

            lblprg.AppendText(vbTab & "-Name-")
            lblprg.AppendText(vbTab & "Criteria: CeCo*")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vFiltro3 As New Dimensiones_Filter() With {.Field = Dimensiones_Fields.Blocked, .Criteria = "No"}

            lblprg.AppendText(vbTab & "-Blocked-")
            lblprg.AppendText(vbTab & "Criteria: No")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vFiltros As Dimensiones_Filter() = New Dimensiones_Filter() {vFiltro1, vFiltro2, vFiltro3}

            CnnLog.Open()

            fichaDimensiones = wsDimensionesService.ReadMultiple(vFiltros, "", 0)

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Application.DoEvents()

            lblprg.AppendText(String.Format("Centros de costo encontrados en WS: {0} ", fichaDimensiones.Count))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vContador As Integer = 0

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            BeNavEjecucionRes.Registros_ws = fichaDimensiones.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

            Dim vIdCentroCosto As Integer = clsLnCentro_costo.MaxID(lConnection, lTransaction) + 1
            Dim BeCentroCosto As New clsBeCentro_costo()

            For Each CC As Dimensiones In fichaDimensiones

                Try

                    If Not clsLnCentro_costo.Exist_By_Codigo(CC.Code) Then

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(String.Format("Procesando CC: {0} - {1} {2}", CC.Code, CC.Name, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        BeCentroCosto = New clsBeCentro_costo()
                        BeCentroCosto.Codigo = CC.Code
                        BeCentroCosto.Nombre = CC.Name
                        BeCentroCosto.IdCentroCosto = vIdCentroCosto
                        BeCentroCosto.Fec_agr = Now
                        BeCentroCosto.Fec_mod = Now
                        BeCentroCosto.User_agr = BeConfigEnc.IdUsuario
                        BeCentroCosto.User_mod = BeConfigEnc.IdUsuario
                        BeCentroCosto.Activo = True
                        BeCentroCosto.IdEmpresa = BeConfigEnc.Idempresa

                        clsLnCentro_costo.Insertar(BeCentroCosto,
                                                   lConnection,
                                                   lTransaction)

                        vIdCentroCosto += 1

                    Else

                        '#EJC20220330B: No se actualiza por la misma aclaración explicada en este TAG '#EJC20220330 al inicio del método.
                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(String.Format("Procesando CC: {0} - {1} {2} - Ya existe, no se inserta, no se actualiza.", CC.Code, CC.Name, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                    VContadorBitacoraIntermedia += 1

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    Throw ex

                End Try

            Next

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Finalizó la importación de los {0} Centros de Costo", vContador, vbNewLine))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Centros_Costo_DesdeWSNav = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            '#EJC20171107_REF03_0237AM: Insertar en log, excepción general
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, CnnLog)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Error: {0} ", ex.Message, vbNewLine))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220406: Solicitado por Ricardo Aristondo, por ajuste a realizar por Kilder.
    ''' </summary>
    ''' <param name="lblprg"></param>
    ''' <param name="prg"></param>
    ''' <returns></returns>
    Public Shared Function Importar_Centros_Costo_Mercadeo_DesdeWSNav(ByVal lblprg As RichTextBox,
                                                                      ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Importar_Centros_Costo_Mercadeo_DesdeWSNav = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Try

            wsDimensionesService.Url = BD.Instancia.WSDimensiones

            lblprg.AppendText("Consultando WebService de Dimensiones en: " & wsDimensionesService.Url)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            '#EJC20220330: Este desarrollo se concedió a  BYB (no se cobró), 
            'Se realizó en la última semana previo a la salida en vivo y no se tenía tiempo para modelar el comportamiento
            'de filtros parametrizables, por eso no lo son.
            'Las modificaciones futuras a este método deberían ser cobradas...
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** APLICANDO FILTROS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** (No Parametrizables) ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vFiltro1 As New Dimensiones_Filter() With {.Field = Dimensiones_Fields.Dimension_Code, .Criteria = 1}

            lblprg.AppendText(vbTab & "-Dimension_Code-")
            lblprg.AppendText(vbTab & "Criteria: 1")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vFiltro2 As New Dimensiones_Filter() With {.Field = Dimensiones_Fields.Code, .Criteria = "1920..1929T"}

            lblprg.AppendText(vbTab & "-Code-")
            lblprg.AppendText(vbTab & "Criteria: 1920..1929T")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vFiltro3 As New Dimensiones_Filter() With {.Field = Dimensiones_Fields.Blocked, .Criteria = "No"}

            lblprg.AppendText(vbTab & "-Blocked-")
            lblprg.AppendText(vbTab & "Criteria: No")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vFiltros As Dimensiones_Filter() = New Dimensiones_Filter() {vFiltro1, vFiltro2, vFiltro3}

            CnnLog.Open()

            fichaDimensiones = wsDimensionesService.ReadMultiple(vFiltros, "", 0)

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Application.DoEvents()

            lblprg.AppendText(String.Format("Centros de costo encontrados en WS: {0} ", fichaDimensiones.Count))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vContador As Integer = 0

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            BeNavEjecucionRes.Registros_ws = fichaDimensiones.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

            Dim vIdCentroCosto As Integer = clsLnCentro_costo.MaxID(lConnection, lTransaction) + 1
            Dim BeCentroCosto As New clsBeCentro_costo()

            For Each CC As Dimensiones In fichaDimensiones

                Try

                    If Not clsLnCentro_costo.Exist_By_Codigo(CC.Code) Then

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(String.Format("Procesando CC: {0} - {1} {2}", CC.Code, CC.Name, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        BeCentroCosto = New clsBeCentro_costo()
                        BeCentroCosto.Codigo = CC.Code
                        BeCentroCosto.Nombre = CC.Name
                        BeCentroCosto.IdCentroCosto = vIdCentroCosto
                        BeCentroCosto.Fec_agr = Now
                        BeCentroCosto.Fec_mod = Now
                        BeCentroCosto.User_agr = BeConfigEnc.IdUsuario
                        BeCentroCosto.User_mod = BeConfigEnc.IdUsuario
                        BeCentroCosto.Activo = True
                        BeCentroCosto.IdEmpresa = BeConfigEnc.Idempresa

                        clsLnCentro_costo.Insertar(BeCentroCosto,
                                                   lConnection,
                                                   lTransaction)

                        vIdCentroCosto += 1

                    Else

                        '#EJC20220330B: No se actualiza por la misma aclaración explicada en este TAG '#EJC20220330 al inicio del método.
                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(String.Format("Procesando CC: {0} - {1} {2} - Ya existe, no se inserta, no se actualiza.", CC.Code, CC.Name, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                    VContadorBitacoraIntermedia += 1

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    Throw ex

                End Try

            Next

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Finalizó la importación de los {0} Centros de Costo", vContador, vbNewLine))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Centros_Costo_Mercadeo_DesdeWSNav = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            '#EJC20171107_REF03_0237AM: Insertar en log, excepción general
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, CnnLog)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Error: {0} ", ex.Message, vbNewLine))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

End Class
