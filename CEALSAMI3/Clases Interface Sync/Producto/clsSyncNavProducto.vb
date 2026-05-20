Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsSyncNavProducto : Inherits clsInterfaceBase
    Implements IDisposable

    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Private Function Importar_Productos_Desde_ERP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                     ByRef prg As ProgressBar,
                                                                     ByRef cnnLog As SqlConnection) As Boolean

        Importar_Productos_Desde_ERP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim RegistrosNoEncontrados As Boolean = False

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Iniciando procesamiento de productos a tabla intermedia -> " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()


            Dim lfichaProductos As New List(Of clsBeI_nav_servicio)

            lfichaProductos = clsLnI_nav_servicio.GetAll_Sin_Procesar()

            Application.DoEvents()

            prg.Maximum = lfichaProductos.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            BeNavEjecucionRes.Registros_ws = lfichaProductos.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            If lfichaProductos.Count > 0 Then

                RegistrosNoEncontrados = True

                Dim BeAcuerdoProd As New clsBeI_nav_servicio()

                For Each Prod In lfichaProductos

                    Try

                        lblprg.AppendText(String.Format("Procesando Producto: {0} {1} ", Prod.Codigo_servicio, vbNewLine))
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        BeAcuerdoProd = New clsBeI_nav_servicio()
                        BeAcuerdoProd.IdServicio = clsLnI_nav_servicio.MaxID(lConnection, lTransaction) + 1
                        BeAcuerdoProd.Codigo_servicio = Prod.Codigo_servicio
                        BeAcuerdoProd.Descripcion = Prod.Descripcion
                        BeAcuerdoProd.Nemonico = Prod.Nemonico

                        If Not clsLnI_nav_acuerdo_productos.Existe(Prod.Codigo_servicio, lConnection, lTransaction) Then
                            clsLnI_nav_servicio.Insertar(BeAcuerdoProd, lConnection, lTransaction)
                        Else
                            lblprg.AppendText(String.Format("Producto: {0} Ya existe. {1} ", Prod.Codigo_servicio, vbNewLine))
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End If

                        VContadorBitacoraIntermedia += 1

                        prg.Value = vContador

                        vContador += 1

                        Application.DoEvents()

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                   Prod.Codigo_servicio,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet, cnnLog)

                        lblprg.AppendText("####### Error #######")
                        lblprg.AppendText(ex.Message)
                        lblprg.AppendText("Ref: " & Prod.Codigo_servicio)
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        Application.DoEvents()

                    End Try

                Next

            Else

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("No se encontraron productos pendientes de procesar (procesado_wms =No) en ERP.")
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

            End If

            lTransaction.Commit()

            If RegistrosNoEncontrados Then
                Importar_Productos_Desde_ERP_A_TablaIntermedia = True
            End If

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Fin de procesamiento de productos -> " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               MethodBase.GetCurrentMethod.Name(),
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, cnnLog)
            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            lblprg.AppendText("####### Error #######")
            lblprg.AppendText(ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Function Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                             ByRef prg As ProgressBar,
                                                                             Optional ByVal ForzarEjecucion As Boolean = False,
                                                                             Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Producto") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Exit Function

                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0 '0'clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            CnnInterface.Open() : lTrans = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeNavEjecucionRes.IdEjecucionRes = 0 '0'clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Productos_Desde_ERP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde WS?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Productos_Desde_ERP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Actualiza_Procesado_ERP(lblprg, prg)

            Actualiza_Procesado_WMS(lblprg, prg)

            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA DE TOMIMS ********** ")

        Catch ex As Exception
            If Not lTrans Is Nothing Then lTrans.Rollback()
            prg.Value = 0
            lblprg.AppendText(String.Format("Error al insertar producto a tabla de TOMIMS: {0} {1}", ex.Message, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Shared Function Truncate(value As String, length As Integer) As String
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length)
        End If
    End Function


    Public Shared Function Actualiza_Procesado_WMS(ByVal lblprg As RichTextBox,
                                                   ByRef prg As ProgressBar) As Boolean

        Dim lReturn As Boolean = False

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Iniciando actualización de registros procesados en el WMS ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            lReturn = clsLnI_nav_servicio.Actuaizar_Procesado_WMS()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Fin de la  actualización de registros procesados en el WMS ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

        Return lReturn

    End Function


    Public Shared Function Actualiza_Procesado_ERP(ByVal lblprg As RichTextBox,
                                                   ByRef prg As ProgressBar) As Boolean

        Dim lReturn As Boolean = False

        Try


            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Iniciando actualización de procesado regsitro en el ERP ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            prg.Value = 0

            Dim vContador As Integer = 0

            Const sp As String = "SELECT * FROM I_nav_servicio WHERE procesado_wms = 0 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        prg.Maximum = lDataTable.Rows.Count

                        For Each dr As DataRow In lDataTable.Rows

                            vContador += 1

                            lblprg.AppendText(String.Format("Actualizando bandera en Producto ERP: {0} ({1} de {2}) {3} ", dr.Item("codigo_servicio"), vContador, lDataTable.Rows.Count, vbNewLine))
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            clsLnI_nav_servicio.ActualizarBandera(dr.Item("codigo_servicio"))

                            prg.Value = vContador

                            Application.DoEvents()

                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lReturn = True

                lConnection.Close()

            End Using

            Return lReturn

        Catch ex As Exception
            prg.Value = 0
            lblprg.AppendText(String.Format("Error al actualizar producto  ERP: {0} {1}", ex.Message, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class