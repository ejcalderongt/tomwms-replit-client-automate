Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen

Public Class frmCambioUbicacion

    Private ReadOnly pListTransUbicTarimaDisponibles As New List(Of clsBeTrans_ubic_tarima)
    Private ReadOnly pListTransUbicTarimaUsadas As New List(Of clsBeTrans_ubic_tarima)

    Private pListObjMov As New List(Of clsBeTrans_movimientos)
    Private pListStockMov As New List(Of clsBeStock)

    Public pListObjDet As New List(Of clsBeTrans_ubic_hh_det)
    Private pObjDet As New clsBeTrans_ubic_hh_det

    Private DT As DataTable

    Private pListObjOp As New List(Of clsBeTrans_ubic_hh_op)
    Private cantidadMovida As Integer = 0
    Private pObjVWStock As New clsBeVW_stock_res
    Private pObjStockMov As New clsBeStock
    Private lStockUbicHH As New List(Of clsBeTrans_ubic_hh_stock)
    Private ReadOnly pUbicSugReq As New clsBeUbicacionSugeridaRequest
    Private BePres As New clsBeProducto_Presentacion
    Private pDimensionProducoSeleccionado As Double = 0
    Public idUbicacionDestino As Integer
    Public idOperador As Integer

    Public gBeTransUbicacinHhDet As New clsBeTrans_ubic_hh_det

    Public Property Dañado As Boolean = False
    Public Property Utilizable As Boolean = False
    Public Property IdIndiceRotacion As Integer = 0
    Public Property Modo As TipoTrans
    Public Property tipoOperacion As Integer
    Public gBeTransubicacionHHEnc As New clsBeTrans_ubic_hh_enc
    Public gBeTareaHh As New clsBeTarea_hh
    Public gBeMotivoUbicacion As New clsBeMotivo_ubicacion
    Private Stock As FrmStockList
    Public Delegate Sub ListarTransaccionUbicHhEnc()
    Public Property InvokeListarUbicHH As ListarTransaccionUbicHhEnc
    Private pBetrans_ubic_hh_op As New clsBeTrans_ubic_hh_op
    Public Property InvokeListarTareasUbicacion As Listar_Tareas_Ubicacion
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Delegate Sub Listar_Tareas_Ubicacion()

    Private DTStockReservado As New DataTable("StockReservado")

    '#GT21102024: lista de stock por seleccion múltple
    Private pStockRes_SeleccionMultiple As New List(Of clsBeVW_stock_res)

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Sub Verifica_Permiso_Ubicacion_Sin_HH()

        Try

            Dim lMenuRol As New List(Of clsBeMenu_sistema)
            Dim BeMenuSistema As New clsBeMenu_sistema

            lMenuRol = clsLnMenu_sistema.Get_All_By_IdRol(AP.UsuarioAp.IdRol)

            BeMenuSistema = lMenuRol.Find(Function(x) (x.IdMenu = "5.1.1.1") AndAlso x.Nivel = 1)

            If Not BeMenuSistema Is Nothing Then
                '#EJC20171212_0409AM: Está activa la opción?
                chkUbicacionConHh.Checked = BeMenuSistema.Visible
                chkUbicacionConHh.Enabled = BeMenuSistema.Visible
            Else
                chkUbicacionConHh.Checked = True
                chkUbicacionConHh.Enabled = False
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

#Region " Constructor "

    Sub New(ByVal pModo As TipoTrans, ByVal pTipoOp As Integer)
        InitializeComponent()
        Modo = pModo
        tipoOperacion = pTipoOp
        Stock = New FrmStockList With {.Modo = FrmStockList.pModo.Seleccion,
        .WindowState = FormWindowState.Maximized}
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

    Dim Bodega As New clsBeBodega

    Private _inicializado As Boolean = False

    Private Sub frmCambioUbicacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Se deja vacío a propósito.
        ' Toda la inicialización se mueve a Shown.
    End Sub

    Private Sub frmCambioUbicacion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        If _inicializado Then Exit Sub
        _inicializado = True

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("...")

        Try
            InicializarFormulario()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub InicializarFormulario()

        If chkOperadorPorlinea.Checked Then
            cmbOperadores.Visible = True
            If tabDatos.TabPages.Contains(xtabOperador) Then
                tabDatos.TabPages.Remove(xtabOperador)
            End If
        Else
            cmbOperadores.Visible = False
            If Not tabDatos.TabPages.Contains(xtabOperador) Then
                tabDatos.TabPages.Add(xtabOperador)
            End If
        End If

        If Not AP.Listar_Bodegas_By_Usuario(cmbBodega) Then
            XtraMessageBox.Show("No hay bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        '#CKFK20181001: Colocar bodega por defecto.
        cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
        cmbBodega.RefreshEditValue()

        If Not IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue) Then
            XtraMessageBox.Show("No hay propietarios definidos para la bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If Not IMS.Listar_Operadores(cmbOperadores) Then
            XtraMessageBox.Show("No hay operadores definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        lblItemBandera.Text = ""

        Validar_Operadores()

        txtIdMotivoUbicacion.ReadOnly = False

        Select Case Modo

            Case TipoTrans.Nuevo

                Bodega = New clsBeBodega
                Bodega = AP.Bodega

                User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                Fec_agrDateEdit.Text = Now
                User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                Fec_modDateEdit.Text = Now

                If tabDatos.TabPages.Contains(xtabOperador) Then
                    tabDatos.TabPages.Remove(xtabOperador)
                End If

                mnuGuardar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                mnuActualizar.Enabled = False
                mnuEliminar.Enabled = False
                mnuAsignacion.Enabled = False
                mnuPendiente.Enabled = False

                mnuLiberarStockNoProcesado.Enabled = False
                mnuImprimir1.Enabled = False
                cmdEliminarDocumento.Visibility = BarItemVisibility.Never

                dtpFechaInicio.DateTime = Now
                dtpFechaFin.DateTime = Now.AddHours(1)

                clsLnTarimas.GetAllTarimas(lvTarimasDisponibles)

                Verifica_Permiso_Ubicacion_Sin_HH()

                If tipoOperacion = 2 Then
                    groupCambioDeEstado.Visible = False
                    Text = "Cambio de ubicación"
                    RibbonPage1.Text = "Cambio de ubicación"
                    Es_Seleccion_Multiple = True

                ElseIf tipoOperacion = 3 Then
                    groupCambioDeEstado.Visible = True
                    Text = "Cambio de estado"
                    RibbonPage1.Text = "Cambio de estado"
                    Es_Seleccion_Multiple = False
                End If

                If Bodega IsNot Nothing Then
                    If Not Bodega.Control_Talla_Color Then
                        lblTalla.Visible = False
                        lblColor.Visible = False
                        txtTalla.Visible = False
                        txtColor.Visible = False
                    End If
                End If

            Case TipoTrans.Editar

                lnkUbicacionDestino.Enabled = False

                Bodega = New clsBeBodega

                clsLnTarimas.GetAllTarimas(lvTarimasDisponibles)
                clsLnTarimas.GetAllTarimasUsadas(lvTarimasUsadas, gBeTransubicacionHHEnc.IdTareaUbicacionEnc)

                If tipoOperacion = 2 Then
                    groupCambioDeEstado.Visible = False
                    Text = "Cambio de ubicación"

                ElseIf tipoOperacion = 3 Then
                    groupCambioDeEstado.Visible = True
                    Text = "Cambio de estado"
                    mnuImportarListaCambioUbic.Enabled = False
                End If

                If gBeTransubicacionHHEnc.Estado = "Finalizado" Then
                    Deshabilitar_Controles()
                    Deshabilita_Menu()
                Else
                    Habilita_Menu()
                    chkOperadorPorlinea.Enabled = False
                    cmbBodega.Enabled = False
                    cmbPropietarioBodega.Enabled = False
                    mnuGuardar.Enabled = False

                    mnuActualizar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuEliminar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)
                    mnuAsignacion.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                End If

                lblEstado.Text = gBeTransubicacionHHEnc.Estado
                lbl.Text = gBeTransubicacionHHEnc.IdTareaUbicacionEnc
                gBeTareaHh.IdTransaccion = gBeTransubicacionHHEnc.IdTareaUbicacionEnc
                gBeTareaHh.IdTareahh = gBeTransubicacionHHEnc.IdTareaUbicacionEnc

                txtNoDocumento.Text = gBeTransubicacionHHEnc.No_Documento

                '#CM_20092018_1142AM: se busca la bodega por el IdPropietarioBodega ya que no está en la tabla de encabezado.
                Bodega = clsLnPropietario_bodega.GetBodegaByIdPropietarioBodega(gBeTransubicacionHHEnc.IdPropietarioBodega)

                If Bodega IsNot Nothing Then
                    clsLnBodega.GetSingle(Bodega)
                    cmbBodega.EditValue = Bodega.IdBodega
                End If

                If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.2", AP.IdRol) Then
                    cmdEliminarDocumento.Visibility = BarItemVisibility.Always
                Else
                    cmdEliminarDocumento.Visibility = BarItemVisibility.Never
                End If

                cmbPropietarioBodega.EditValue = gBeTransubicacionHHEnc.IdPropietarioBodega
                txtIdMotivoUbicacion.Text = gBeTransubicacionHHEnc.IdMotivoUbicacion
                txtIdMotivoUbicacion_LostFocus(Nothing, Nothing)
                dtpFechaInicio.EditValue = gBeTransubicacionHHEnc.FechaInicio
                dtpHoraInicio.Value = gBeTransubicacionHHEnc.HoraInicio
                dtpFechaFin.EditValue = gBeTransubicacionHHEnc.FechaFin
                dtpHoraFin.Value = gBeTransubicacionHHEnc.HoraFin
                txtObservacion.Text = gBeTransubicacionHHEnc.Observacion
                chkActivo.Checked = gBeTransubicacionHHEnc.Activo
                chkOperadorPorlinea.Checked = gBeTransubicacionHHEnc.Operador_por_linea
                chkUbicacionConHh.Checked = gBeTransubicacionHHEnc.Ubicacion_con_hh
                User_agrTextEdit.Text = gBeTransubicacionHHEnc.User_agr
                Fec_agrDateEdit.Text = gBeTransubicacionHHEnc.Fec_agr
                User_modTextEdit.Text = gBeTransubicacionHHEnc.User_mod
                Fec_modDateEdit.Text = gBeTransubicacionHHEnc.Fec_mod

                Cargar_Detalle(False)
                Listar_Operadores()

                'GT06012022: se carga el stock reservado asociado a la tarea de ubicación
                Cargar_Datos_Stock_Reservado(gBeTransubicacionHHEnc.IdTareaUbicacionEnc)

        End Select

        Habilita_Item()

    End Sub


    Private Sub Validar_Operadores()

        Try

            DsOperadorUbicacion.Clear()
            '#CKFK20220703 Cambié el query para listar los operadores
            'DT = clsLnOperador_bodega.Get_All_By_IdBodega_DT(cmbBodega.EditValue)
            DT = clsLnOperador_bodega.Get_All_By_IdBodega_For_Tarea_DT(cmbBodega.EditValue, clsDataContractDI.tTipoTarea.UBIC)

            pListObjOp = New List(Of clsBeTrans_ubic_hh_op)

            Select Case Modo

                Case TipoTrans.Editar
                    pListObjOp = clsLnTrans_ubic_hh_op.Get_All_By_IdTareaUbicacion(gBeTransubicacionHHEnc.IdTareaUbicacionEnc).ToList()
            End Select

            ListaOperadores()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Dim IdPropietario As Integer = 0

        Try

            gBeTransubicacionHHEnc.IdPropietarioBodega = cmbPropietarioBodega.EditValue
            gBeTransubicacionHHEnc.IdMotivoUbicacion = CInt(txtIdMotivoUbicacion.Text)
            gBeTransubicacionHHEnc.FechaInicio = dtpFechaInicio.EditValue
            gBeTransubicacionHHEnc.HoraInicio = dtpHoraInicio.Value
            gBeTransubicacionHHEnc.FechaFin = dtpFechaFin.EditValue
            gBeTransubicacionHHEnc.HoraFin = dtpHoraFin.Value
            gBeTransubicacionHHEnc.Activo = chkActivo.Checked
            gBeTransubicacionHHEnc.Observacion = txtObservacion.Text.Trim()
            gBeTransubicacionHHEnc.User_agr = AP.UsuarioAp.IdUsuario
            gBeTransubicacionHHEnc.Fec_agr = Now
            gBeTransubicacionHHEnc.User_mod = AP.UsuarioAp.IdUsuario
            gBeTransubicacionHHEnc.Fec_mod = Now
            gBeTransubicacionHHEnc.Operador_por_linea = chkOperadorPorlinea.Checked
            gBeTransubicacionHHEnc.Ubicacion_con_hh = chkUbicacionConHh.Checked
            gBeTransubicacionHHEnc.Cambio_estado = (tipoOperacion = 3)
            gBeTransubicacionHHEnc.Asunto = String.Format("{0}  {1}", cmbPropietarioBodega.SelectedText, lblEstado.Text)
            gBeTransubicacionHHEnc.No_Documento = txtNoDocumento.Text

            If rbAlto.Checked Then
                gBeTransubicacionHHEnc.IdPrioridad = 1
            ElseIf rbMedio.Checked Then
                gBeTransubicacionHHEnc.IdPrioridad = 2
            ElseIf rbBajo.Checked Then
                gBeTransubicacionHHEnc.IdPrioridad = 3
            End If

            gBeTransubicacionHHEnc.IdTipoTarea = tipoOperacion
            gBeTransubicacionHHEnc.IdBodega = cmbBodega.EditValue

            '#EJC20171025_1154AM: Validar que el estado de la tarea de ubicación no sea finalizado antes de cambiarla a Nuevo otra vez.
            If chkUbicacionConHh.Checked AndAlso Not gBeTransubicacionHHEnc.Estado = "Finalizado" Then
                gBeTransubicacionHHEnc.Estado = "Nuevo"
            Else
                gBeTransubicacionHHEnc.Estado = "Finalizado"
            End If

            If Modo = TipoTrans.Nuevo Then
                gBeTransubicacionHHEnc.IsNew = True
            ElseIf Modo = TipoTrans.Editar Then
                gBeTransubicacionHHEnc.IsNew = False
            End If

            If pListStockMov IsNot Nothing AndAlso pListStockMov.Count > 0 Then
                For Each Obj As clsBeStock In pListStockMov
                    cantidadMovida += Obj.Cantidad
                Next
            End If

            For Each lv As ListViewItem In lvTarimasDisponibles.Items
                Dim pObjTransUbicTarimaDisponible = New clsBeTrans_ubic_tarima
                Dim vIDTarima As Integer = lv.Text
                pObjTransUbicTarimaDisponible.IdTarima = vIDTarima
                pListTransUbicTarimaDisponibles.Add(pObjTransUbicTarimaDisponible)
            Next

            For Each lv As ListViewItem In lvTarimasUsadas.Items

                Dim pObjTransUbicTarimaUsada = New clsBeTrans_ubic_tarima
                Dim vIDTarima As Integer = (lv.Text)

                pObjTransUbicTarimaUsada.IdTarima = vIDTarima
                pObjTransUbicTarimaUsada.Codigo = ""
                pObjTransUbicTarimaUsada.Utilizada = True
                pObjTransUbicTarimaUsada.FechaUtilizacion = Now
                pObjTransUbicTarimaUsada.User_agr = AP.UsuarioAp.IdUsuario
                pObjTransUbicTarimaUsada.Fec_agr = Now
                pObjTransUbicTarimaUsada.Fec_mod = Now
                pObjTransUbicTarimaUsada.User_mod = AP.UsuarioAp.IdUsuario
                pListTransUbicTarimaUsadas.Add(pObjTransUbicTarimaUsada)

            Next

            '#CKFK20230126 Agregué esto porque cuando se cambian los operadores se quedaban creados en la tabla
            If chkOperadorPorlinea.Checked Then

                Dim tmpObjOpe As New List(Of clsBeTrans_ubic_hh_op)
                tmpObjOpe = clsLnTrans_ubic_hh_op.Get_All_By_IdTareaUbicacion(gBeTransubicacionHHEnc.IdTareaUbicacionEnc)

                For Each op As clsBeTrans_ubic_hh_op In tmpObjOpe
                    If pListObjDet.FindAll(Function(x) x.IdOperadorBodega = op.IdOperadorBodega).Count = 0 Then
                        pListObjOp.RemoveAt(pListObjOp.FindIndex(Function(y) y.IdOperadorBodega = op.IdOperadorBodega))
                    End If
                Next

            End If

            'CM_20092018_1128AM: Se busca el IdPropietario.
            IdPropietario = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, cmbPropietarioBodega.EditValue)

            clsLnTrans_ubic_hh_enc.Guardar_Transaccion(gBeTransubicacionHHEnc,
                                                       pListObjDet,
                                                       pListObjOp,
                                                       pListObjMov,
                                                       chkUbicacionConHh.Checked,
                                                       IdPropietario,
                                                       pListStockMov,
                                                       pListTransUbicTarimaDisponibles,
                                                       pListTransUbicTarimaUsadas,
                                                       gBeTareaHh.IdTareahh,
                                                       AP.HostName
                                                       )

            gBeTransUbicacinHhDet.IdTareaUbicacionEnc = gBeTransubicacionHHEnc.IdTareaUbicacionEnc

            pListTransUbicTarimaDisponibles.Clear()
            pListTransUbicTarimaUsadas.Clear()

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then
                If Guardar() Then
                    Actualizar = True
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Function

    Private Sub Cargar_Detalle(ByVal pGuardo As Boolean)

        Dim UbicOrigen As New clsBeBodega_ubicacion
        Dim UbicDestino As New clsBeBodega_ubicacion
        Dim TramoOrigen As New clsBeBodega_tramo
        Dim TramoDestino As New clsBeBodega_tramo
        Dim clsTransaccion As New clsTransaccion

        Try

            clsTransaccion.Begin_Transaction()

            If Not pGuardo Then

                pListObjDet = clsLnTrans_ubic_hh_det.Get_All_By_Id_Trans_Ubic_Enc(gBeTransubicacionHHEnc.IdTareaUbicacionEnc,
                                                                                  clsTransaccion.lConnection,
                                                                                  clsTransaccion.lTransaction)

                lStockUbicHH = clsLnTrans_ubic_hh_stock.Get_All_By_IdTareaUbicacionEnc(gBeTransubicacionHHEnc.IdTareaUbicacionEnc,
                                                                                       clsTransaccion.lConnection,
                                                                                       clsTransaccion.lTransaction)

                Dim BeStock As clsBeStock

                pListStockMov.Clear()

                For Each SU In lStockUbicHH

                    BeStock = New clsBeStock

                    '#EJC20171017_0408PM_REF: La tabla trans_ubic_hh_stock comparte los mismo campos que la tabla stock mas tres campos adicionales
                    'Que son llaves para la transacción, por lo tanto se copia el resultado del objeto trans_ubic_hh_stock en un objeto de tipo clsBeStock
                    'Para poder tener así el listado de stock con el que fué creada la transacción para posteriormente editarla a través del botón actualizar.

                    clsPublic.CopyObject(SU, BeStock)

                    '#EJC20171017_0907: Se agregó para evitar que se inserte nuevamente el listado de stock cuando la transacción ya existe.
                    BeStock.IsNew = False

                    BeStock.ProductoEstado.IdEstado = BeStock.IdProductoEstado
                    BeStock.IdBodega = cmbBodega.EditValue

                    '#EJC20171017_0408PM: Colocar en el IdUbicacion la ubicación en donde fué colocado a traves del cambio de ubicación
                    'Para que el proceso de guardar funcione.
                    'BeStock.IdUbicacion = pListObjDet.Find(Function(x) x.IdStock = SU.IdStock AndAlso x.IdTareaUbicacionDet = su.IdTareaUbicacionDet).IdUbicacionDestino
                    pListStockMov.Add(BeStock)

                Next

            End If

            Dim DT As New DataTable("Detalle")
            DT.Columns.Add("Linea", GetType(String))
            DT.Columns.Add("IdStock", GetType(Integer))
            DT.Columns.Add("Código", GetType(String))
            DT.Columns.Add("Producto", GetType(String))
            DT.Columns.Add("IdUbicacionOrigen", GetType(Integer))
            DT.Columns.Add("IdUbicacionDestino", GetType(Integer))
            DT.Columns.Add("Ubicacion_Origen", GetType(String))
            DT.Columns.Add("Ubicacion_Destino", GetType(String))
            DT.Columns.Add("Estado_Origen", GetType(String))
            DT.Columns.Add("Estado_Destino", GetType(String))
            DT.Columns.Add("IdOperadorBodega", GetType(Integer))
            DT.Columns.Add("Operador", GetType(String))
            DT.Columns.Add("U.M.Bas", GetType(String))
            DT.Columns.Add("Cantidad U.M.Bas", GetType(Double))
            DT.Columns.Add("Presentacion", GetType(String))
            DT.Columns.Add("Cantidad Presentacion", GetType(Double))
            DT.Columns.Add("Ubicado", GetType(Double))
            DT.Columns.Add("Realizado", GetType(String))
            'GT21122021:campos agregados
            DT.Columns.Add("Licencia", GetType(String))
            DT.Columns.Add("Lote", GetType(String))
            DT.Columns.Add("FechaVence", GetType(Date))
            DT.Columns.Add("No_Linea", GetType(Integer))
            '#GT08092025: campos talla/color

            If Bodega.Control_Talla_Color Then
                DT.Columns.Add("Talla", GetType(String))
                DT.Columns.Add("Color", GetType(String))
            End If

            grdDetalle.DataSource = Nothing

            For Each Obj As clsBeTrans_ubic_hh_det In pListObjDet.OrderBy(Function(a) a.Tramo).ThenBy(Function(b) b.Indice_x).ThenBy(Function(b) b.Nivel)

                Dim lRow As DataRow = DT.NewRow()

                lRow.Item("Linea") = Obj.IdTareaUbicacionDet '#EJC20170917 -> utilizar nombres no índices

                If Obj.IdStock <> Nothing AndAlso Obj.IdStock <> 0 Then
                    lRow.Item("IdStock") = Obj.IdStock
                    lRow.Item("Código") = Obj.Producto.Codigo
                    lRow.Item("Producto") = Obj.Producto.Nombre
                End If

                'GT21122021:campos agregados en el detalle del producto de ubicación dirigida
                lRow.Item("Licencia") = Obj.Stock.Lic_plate
                lRow.Item("Lote") = Obj.Stock.Lote
                lRow.Item("FechaVence") = Obj.Stock.Fecha_vence

                '#GT08092025: valida si mostrar talla/color
                If Bodega.Control_Talla_Color Then

                    Dim pProductoTallaColor = clsLnProducto_talla_color.GetSingle(Obj.Stock.IdProductoTallaColor)
                    Dim tmpTalla As New clsBeTalla
                    Dim tmpColor As New clsBeColor

                    tmpTalla = clsLnTalla.GetSingle_By_IdTalla(pProductoTallaColor.IdTalla)
                    tmpColor = clsLnColor.GetSingle_By_IdColor(pProductoTallaColor.IdColor)

                    lRow.Item("Talla") = tmpTalla.Codigo
                    lRow.Item("Color") = tmpColor.Codigo

                End If

                If Obj.IdUbicacionOrigen <> Nothing AndAlso Obj.IdUbicacionOrigen <> 0 Then
                    lRow("IdUbicacionOrigen") = Obj.IdUbicacionOrigen
                    Obj.UbicacionOrigen.IdUbicacion = Obj.IdUbicacionOrigen
                    Obj.UbicacionOrigen.IdBodega = cmbBodega.EditValue
                    lRow("Ubicacion_Origen") = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(Obj.IdUbicacionOrigen, Obj.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                End If

                If Obj.IdUbicacionDestino <> Nothing AndAlso Obj.IdUbicacionDestino <> 0 Then
                    lRow("IdUbicacionDestino") = Obj.IdUbicacionDestino
                    Obj.UbicacionDestino.IdUbicacion = Obj.IdUbicacionDestino
                    Obj.UbicacionDestino.IdBodega = cmbBodega.EditValue
                    lRow("Ubicacion_Destino") = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(Obj.IdUbicacionDestino, Obj.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                End If

                ''#EJC20171025_0100AM: No se desplegaban los estados origen/destino
                lRow("Estado_Origen") = clsLnProducto_estado.GetNombreByIdEstado(Obj.IdEstadoOrigen, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                lRow("Estado_Destino") = clsLnProducto_estado.GetNombreByIdEstado(Obj.IdEstadoDestino, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                If Obj.IdOperadorBodega <> Nothing AndAlso Obj.IdOperadorBodega <> 0 Then
                    lRow("IdOperadorBodega") = Obj.IdOperadorBodega
                    '#CKFK20230126 Agregué que se mostrara también el apellido del operador
                    lRow("Operador") = Obj.Operador.Nombres + " " + Obj.Operador.Apellidos '#EJC20220503: En la vista viene en nombres la concatenación de nombres + ' ' + apellidos
                End If

                If Obj.ProductoPresentacion.IdPresentacion <> 0 Then
                    lRow("Cantidad U.M.Bas") = Obj.Cantidad
                    lRow("Cantidad Presentacion") = Obj.Cantidad / Obj.ProductoPresentacion.Factor
                    lRow.Item("Presentacion") = Obj.ProductoPresentacion.Nombre
                Else
                    lRow("Cantidad U.M.Bas") = Obj.Cantidad
                    lRow("Cantidad Presentacion") = 0
                    lRow.Item("Presentacion") = ""
                End If

                lRow.Item("U.M.Bas") = Obj.UnidadMedida.Nombre 'clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(Obj.Stock.IdUnidadMedida, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                lRow("Ubicado") = Obj.Recibido

                If Obj.Realizado Then lRow("Realizado") = "SI" Else lRow("Realizado") = "No"

                If Obj.No_Linea <> Nothing Then
                    lRow("No_Linea") = Obj.No_Linea
                End If

                If Obj.Activo Then DT.Rows.Add(lRow)

            Next

            grdDetalle.DataSource = DT
            'GridViewDet.Columns("IdStock").Visible = False
            GridViewDet.Columns("IdUbicacionOrigen").Visible = False
            GridViewDet.Columns("IdUbicacionDestino").Visible = False
            GridViewDet.Columns("IdOperadorBodega").Visible = False

            Try

                GridViewDet.Columns("Linea").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                GridViewDet.Columns("Linea").SummaryItem.DisplayFormat = "Count = {0}"

                GridViewDet.Columns("Cantidad U.M.Bas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridViewDet.Columns("Cantidad U.M.Bas").DisplayFormat.FormatString = "{0:n6}"

                GridViewDet.Columns("Cantidad U.M.Bas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridViewDet.Columns("Cantidad U.M.Bas").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                GridViewDet.Columns("Cantidad Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridViewDet.Columns("Cantidad Presentacion").DisplayFormat.FormatString = "{0:n6}"

                GridViewDet.Columns("Cantidad Presentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridViewDet.Columns("Cantidad Presentacion").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                GridViewDet.Columns("Ubicado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridViewDet.Columns("Ubicado").DisplayFormat.FormatString = "{0:n6}"

            Catch ex As Exception

            End Try

            clsTransaccion.Commit_Transaction()

            grdDetalle.Refresh()

            GridViewDet.BestFitColumns()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

#Region " Eventos principales "

    Private Sub grdDetalle_DoubleClick(sender As Object, e As EventArgs) Handles grdDetalle.DoubleClick

        'deshabilitaItem()
        'Habilita_Item()

        Dim ubic_orig As New clsBeBodega_ubicacion

        Try

            Dim Dr As DataRowView = GridViewDet.GetFocusedRow
            Dim lIndex As Integer = -1

            lIndex = pListObjDet.FindIndex(Function(b) b.IdTareaUbicacionDet = CInt(Dr.Item("Linea")))

            If lIndex > -1 Then

                If Not gBeTransubicacionHHEnc.Estado = "Finalizado" Then
                    If pListObjDet(lIndex).Realizado Then
                        Deshabilita_Item()
                    Else
                        Habilita_Item()
                    End If
                ElseIf pListObjDet(lIndex).Activo AndAlso Not pListObjDet(lIndex).Realizado Then '#EJC20171025_1047AM: Validar que no se haya liberado/desactivado posteriormente el registro de detalle de cambio de ubicación
                    mnuEliminarDet.Text = "Liberar"
                    mnuEliminarDet.Enabled = True
                End If

                cmdGuardar.Tag = Dr.Item("Linea")

                txtIdStock.Text = pListObjDet(lIndex).IdStock

                ubic_orig.IdUbicacion = pListObjDet(lIndex).IdUbicacionOrigen
                ubic_orig.IdBodega = AP.IdBodega
                clsLnBodega_ubicacion.GetSingle(ubic_orig)

                '#EJC20171025_1255AM: Ya no es necesario, se hace en el getsingle de arriba.
                'ubic_orig.Tramo.IdTramo = ubic_orig.IdTramo
                'clsLnBodega_tramo.GetSingle(ubic_orig.Tramo)

                txtIdOrigen.Text = ubic_orig.NombreCompleto
                txtIdOrigen.Tag = ubic_orig.IdUbicacion

                txtProducto.Text = pListObjDet(lIndex).Producto.Nombre
                txtVence.Text = pListObjDet(lIndex).Stock.Fecha_vence
                txtSerie.Text = pListObjDet(lIndex).Stock.Serial

                ''#EJC20171025_0100AM: Desplegar el estado origen, antes se desplegaba el estado destino.
                'txtEstado.Text = pListObjDet(lIndex).ProductoEstado.Nombre
                txtEstado.Text = clsLnProducto_estado.Get_Nombre_By_IdEstado(pListObjDet(lIndex).IdEstadoOrigen)
                txtAñada.Text = pListObjDet(lIndex).Stock.Añada
                txtLote.Text = pListObjDet(lIndex).Stock.Lote
                txtIngreso.Text = pListObjDet(lIndex).Stock.Fecha_Ingreso
                txtPresentacion.Text = pListObjDet(lIndex).ProductoPresentacion.Nombre
                txtUnidadMedida.Text = pListObjDet(lIndex).UnidadMedida.Nombre
                txtIdUbicacionDestino.Text = pListObjDet(lIndex).IdUbicacionDestino
                'GT20122021: se muestra la ubicacion completa, no solo el # de rack
                'txtUbicacionDestino.Text = pListObjDet(lIndex).UbicacionDestino.Descripcion
                txtUbicacionDestino.Text = pListObjDet(lIndex).UbicacionDestino.NombreCompleto

                'GT21122021: se agrega la licencia al campo correspondiente
                txtLicPlate.Text = pListObjDet(lIndex).Stock.Lic_plate


                txtCantidad.Maximum = pListObjDet(lIndex).Cantidad
                txtCantidad.Minimum = 1
                txtCantidad.Text = pListObjDet(lIndex).Cantidad
                txtCantidad.Value = pListObjDet(lIndex).Cantidad

                '#CKFK20230126 Agregué esto porque se estaba guardando el IdOperador y no el IdOperadorBodega
                cmbOperadores.EditValue = clsLnOperador_bodega.Get_IdOperador_By_IdOperadorBodega(pListObjDet(lIndex).IdOperadorBodega)

                chkRealizadoDet.Checked = pListObjDet(lIndex).Realizado
                chkActivo.Checked = pListObjDet(lIndex).Activo
                txtIdEstado.Text = pListObjDet(lIndex).IdEstadoDestino
                txtNombreEstado.Text = pListObjDet(lIndex).ProductoEstado.Nombre

                cmdGuardar.Visible = Not (pListObjDet(lIndex).Realizado)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Try
            For Each m As ListViewItem In lvTarimasUsadas.Items
                m.Checked = (CheckBox2.Checked = True)
            Next

            Application.DoEvents()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try

            For Each m As ListViewItem In lvTarimasDisponibles.Items
                m.Checked = (CheckBox1.Checked = True)
            Next

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub chkUbicacionConHh_CheckedChanged(sender As Object, e As EventArgs) Handles chkUbicacionConHh.CheckedChanged

        Try

            If chkUbicacionConHh.Checked Then
                groupPrioridad.Visible = True
            Else
                groupPrioridad.Visible = False
                If String.IsNullOrEmpty(lbl.Text.Trim) = False Then
                    If gBeTransubicacionHHEnc.Estado = "Nuevo" Then

                        If gBeTransubicacionHHEnc.Ubicacion_con_hh Then
                            If MessageBox.Show("¿Seguro que quiere cambiar la tarea de  HH a BOF?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                groupPrioridad.Visible = False
                                gBeTransubicacionHHEnc.Ubicacion_con_hh = False
                            Else
                                chkUbicacionConHh.Checked = True
                            End If
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkOperadorPorlinea_CheckedChanged(sender As Object, e As EventArgs) Handles chkOperadorPorlinea.CheckedChanged

        Try

            If (chkOperadorPorlinea.Checked) Then
                cmbOperadores.Visible = True
                tabDatos.TabPages.Remove(xtabOperador)

                'GT17122021: si cambio de operador se limpia la lista, porque solo se guarda un operador a la vez
                If Not cmbOperadores.EditValue Is Nothing Then

                    pListObjOp.Clear()
                    pBetrans_ubic_hh_op = New clsBeTrans_ubic_hh_op()
                    pBetrans_ubic_hh_op.IdTransUbicHhOp = 0
                    pBetrans_ubic_hh_op.IdTareaUbicacionEnc = 0
                    pBetrans_ubic_hh_op.IdOperadorBodega = cmbOperadores.EditValue
                    pBetrans_ubic_hh_op.User_agr = AP.UsuarioAp.IdUsuario
                    pBetrans_ubic_hh_op.Fec_agr = Now
                    pBetrans_ubic_hh_op.User_mod = AP.UsuarioAp.IdUsuario
                    pBetrans_ubic_hh_op.Fec_mod = Now
                    pListObjOp.Add(pBetrans_ubic_hh_op)
                End If

            Else
                cmbOperadores.Visible = False
                tabDatos.TabPages.Add(xtabOperador)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = GrdOperadorBobega.GetFocusedRow
                Dim lIndex As Integer = -1
                Dim d As Integer = Dr.Item("IdOperadorBodega")

                lIndex = pListObjOp.FindIndex(Function(b) b.IdOperadorBodega = d)

                If lIndex > -1 Then

                    If ritem.Checked = False Then

                        If pListObjOp(lIndex).IdTransUbicHhOp > 0 AndAlso pListObjOp(lIndex).IdTransUbicHhOp Then
                            clsLnTrans_re_op.Delete(pListObjOp(lIndex).IdTransUbicHhOp, pListObjOp(lIndex).IdTareaUbicacionEnc)
                            DsOperadorUbicacion.Clear()
                            ListaOperadores()
                        End If

                        pListObjOp.RemoveAt(lIndex)

                    End If

                Else

                    Dim Obj As New clsBeTrans_ubic_hh_op() _
                        With {.IdOperadorBodega = Dr.Item("IdOperadorBodega"),
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now}

                    pListObjOp.Add(Obj)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

#End Region

#Region " Eventos Menu "

    ' ***  Menu encabezado

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            If Datos_Correctos() Then

                '#CKFK 20180128 01:25AM modifiqué el OK por el YesNo porque no estaba guardando el cambio de ubicación
                If XtraMessageBox.Show("¿Guardar transacción?",
                                        Text,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then

                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = True
                        mnuEliminar.Enabled = True
                        chkOperadorPorlinea.Enabled = False

                        DialogResult = DialogResult.OK

                        If Not InvokeListarUbicHH Is Nothing Then
                            InvokeListarUbicHH.Invoke
                        End If

                        Close()

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If Actualizar() Then

                XtraMessageBox.Show("Se actualizó el cambio de ubicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If Not InvokeListarUbicHH Is Nothing Then
                    InvokeListarUbicHH.Invoke
                End If

                Close()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Eliminar cambio de ubicación?",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                gBeTransubicacionHHEnc.Activo = False

                Dim vResultadoEliminoStockReservado As Boolean = False

                If clsLnTrans_ubic_hh_enc.Anular_Tarea_Cambio_Ubic_O_Estado(gBeTransubicacionHHEnc) Then

                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If Not InvokeListarUbicHH Is Nothing Then InvokeListarUbicHH.Invoke

                    Close()

                    frmCambioUbicacion_List.Dgrid.Refresh()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    ' ***  Menu detalle

    Private Sub cmdGuardar_Click(sender As Object, e As EventArgs) Handles cmdGuardar.Click

        Try

            Dim lIndex As Integer = -1
            Dim lIndexOp As New List(Of clsBeTrans_ubic_hh_op)
            Dim lIndexNuevoOP As Integer = -1
            Dim lIndexOpActual As Integer = -1
            Dim vIdOperadorBodegaActual As Integer = 0
            Dim lDetallesPorOperador As New List(Of clsBeTrans_ubic_hh_det)

            lIndex = pListObjDet.FindIndex(Function(b) b.IdTareaUbicacionDet = CInt(cmdGuardar.Tag))

            If lIndex > -1 Then

                pListObjDet(lIndex).IdUbicacionDestino = CInt(txtIdUbicacionDestino.Text.Trim())

                '#EJC20220503: Holds some sort of wonder.
                '#CKFK20230126 Agregué esto porque cuando se cambian el operador de la línea se seguía manteniendo el anterior
                If chkOperadorPorlinea.Checked Then
                    vIdOperadorBodegaActual = cmbOperadores.Tag
                Else
                    vIdOperadorBodegaActual = pListObjDet(lIndex).IdOperadorBodega
                End If

                '#EJC20220503: Buscar cuantas líneas están asociadas al operador que se quiere modificar.
                lDetallesPorOperador = pListObjDet.FindAll(Function(b) b.IdOperadorBodega = vIdOperadorBodegaActual)

                pListObjDet(lIndex).IdOperadorBodega = vIdOperadorBodegaActual
                pListObjDet(lIndex).Operador.Nombres = cmbOperadores.Text

                If (tipoOperacion = 3) Then
                    If (Datos_CorrectosCambioEstado()) Then
                        pListObjDet(lIndex).IdEstadoDestino = CInt(txtIdEstado.Text.Trim())
                    End If
                End If

                pListObjDet(lIndex).Realizado = chkRealizadoDet.Checked
                pListObjDet(lIndex).Cantidad = Double.Parse(txtCantidad.Text.Trim())
                pListObjDet(lIndex).Activo = chkActivo.Checked
                pListObjDet(lIndex).IdBodega = cmbBodega.EditValue

                If Not pListObjOp Is Nothing Then

                    If pListObjOp.Count > 0 Then

                        If Not lDetallesPorOperador Is Nothing Then

                            If lDetallesPorOperador.Count > 0 Then

                                If lDetallesPorOperador.Count = 1 Then

                                    '#EJC20220503: Buscar el operador que están editando.
                                    lIndexOp = pListObjOp.FindAll(Function(b) b.IdOperadorBodega = vIdOperadorBodegaActual)

                                    '#EJC20220503:Buscar si en la lista ya existe el nuevo operador asignado.
                                    lIndexNuevoOP = pListObjOp.FindIndex(Function(b) b.IdOperadorBodega = cmbOperadores.EditValue)

                                    lIndexOpActual = pListObjOp.FindIndex(Function(b) b.IdOperadorBodega = vIdOperadorBodegaActual)

                                    If lIndexNuevoOP = -1 Then

                                        pListObjOp(lIndexOpActual).IdOperadorBodega = cmbOperadores.EditValue
                                        pListObjOp(lIndexOpActual).User_mod = AP.UsuarioAp.IdUsuario
                                        pListObjOp(lIndexOpActual).Fec_mod = Now


                                        'pBetrans_ubic_hh_op = New clsBeTrans_ubic_hh_op()
                                        'pBetrans_ubic_hh_op.IdTransUbicHhOp = 0
                                        'pBetrans_ubic_hh_op.IdTareaUbicacionEnc = 0
                                        'pBetrans_ubic_hh_op.IdOperadorBodega = cmbOperadores.EditValue
                                        'pBetrans_ubic_hh_op.User_agr = AP.UsuarioAp.IdUsuario
                                        'pBetrans_ubic_hh_op.Fec_agr = Now
                                        'pBetrans_ubic_hh_op.User_mod = AP.UsuarioAp.IdUsuario
                                        'pBetrans_ubic_hh_op.Fec_mod = Now
                                        'pListObjOp.Add(pBetrans_ubic_hh_op)

                                    End If

                                Else

                                    '#EJC20220503:Insertar el nuevo operador en el detalle.
                                    pBetrans_ubic_hh_op = New clsBeTrans_ubic_hh_op()
                                    pBetrans_ubic_hh_op.IdTransUbicHhOp = 0
                                    pBetrans_ubic_hh_op.IdTareaUbicacionEnc = gBeTransubicacionHHEnc.IdTareaUbicacionEnc
                                    pBetrans_ubic_hh_op.IdOperadorBodega = cmbOperadores.EditValue
                                    pBetrans_ubic_hh_op.User_agr = AP.UsuarioAp.IdUsuario
                                    pBetrans_ubic_hh_op.Fec_agr = Now
                                    pBetrans_ubic_hh_op.User_mod = AP.UsuarioAp.IdUsuario
                                    pBetrans_ubic_hh_op.Fec_mod = Now
                                    pListObjOp.Add(pBetrans_ubic_hh_op)

                                End If

                            End If

                        End If

                    End If

                End If

            End If

            Cargar_Detalle(True)
            Limpiar_Campos_Detalle()
            Habilita_Item()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click
        Try
            cargarTarimasTransaccionAgregar()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdNewP_Click(sender As Object, e As EventArgs) Handles cmdNewP.Click
        Limpiar_Campos_Detalle()
        Habilita_Item()
    End Sub

    Private Sub cmdEliminarDetalle_Click(sender As Object, e As EventArgs) Handles mnuEliminarDet.Click

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            If GridViewDet.RowCount > 0 Then

                Dim Dr As DataRowView = GridViewDet.GetFocusedRow
                Dim lIndex As Integer = -1
                Dim DTStock_Res As New DataTable
                Dim Stock_res As New clsBeStock_res

                lIndex = pListObjDet.FindIndex(Function(b) b.IdTareaUbicacionDet = CInt(Dr.Item("Linea")))

                If lIndex > -1 Then

                    If Modo = TipoTrans.Nuevo Then
                        pListObjDet.RemoveAt(lIndex)
                    Else
                        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                        pListObjDet.Item(lIndex).Activo = False
                        'GT06012022: se obtiene el idstock para obtener el stock_reservado y su id respectivo
                        Dim pIdStock = pListObjDet.Item(lIndex).IdStock
                        DTStock_Res = clsLnVW_Stock_Res_Pedido.Get_All_By_IdStock_DT(AP.IdBodega, pIdStock,
                                                                                                lConnection,
                                                                                                lTransaction)
                        Stock_res.IdStockRes = DTStock_Res.Rows(0).Item("IdStockRes").ToString()
                        clsLnStock_res.Eliminar(Stock_res,
                                                lConnection,
                                                        lTransaction)

                        lTransaction.Commit()

                    End If

                    If gBeTransubicacionHHEnc.Estado = "Finalizado" AndAlso mnuActualizar.Enabled = False Then
                        mnuActualizar.Enabled = True
                        XtraMessageBox.Show("Se ha desactivado el registro, actualice y confirme la liberación de stock", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else

                        Dr.Delete()

                        XtraMessageBox.Show("Se " & "ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        '#CKFK20220128 Agregué esta funcionalidad por lo reportado por Marcelo de las tareas que se quedaban sin registros y activas e inconsistentes
                        If GridViewDet.RowCount = 0 Then

                            If Not gBeTransubicacionHHEnc.Estado = "" Then

                                XtraMessageBox.Show("La tarea ya no tiene registros se va a desactivar", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                gBeTransubicacionHHEnc.Activo = False

                                If Not clsLnTrans_ubic_hh_enc.Anular_Tarea_Cambio_Ubic_O_Estado_Sin_Registros(gBeTransubicacionHHEnc) Then
                                    Throw New Exception("No se pudo anular la tarea")
                                Else

                                    If Not InvokeListarUbicHH Is Nothing Then InvokeListarUbicHH.Invoke

                                    Close()

                                    'frmCambioUbicacion_List.Dgrid.Refresh()

                                    Return

                                End If

                            Else

                                If Not InvokeListarUbicHH Is Nothing Then InvokeListarUbicHH.Invoke

                                Close()

                                Return

                            End If

                        End If

                    End If

                    Limpiar_Campos_Detalle()
                    Cargar_Datos_Stock_Reservado(lbl.Text)

                End If

            Else
                XtraMessageBox.Show("No se ha eliminado ningún registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        Finally
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
            lConnection.Dispose()
        End Try

        'Catch ex As Exception
        '    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Dim vMsgError As String = ex.Message
        '    clsLnLog_error_wms.Agregar_Error(vMsgError)
        'End Try

    End Sub

#End Region

#Region " Eventos Link "

    Private Sub lnkMotivoUbicacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkMotivoUbicacion.LinkClicked

        Try

            Dim motivoUbicacion As New frmMotivo_UbicacionList() _
                With {.Modo = frmMotivo_UbicacionList.pModo.Seleccion}
            motivoUbicacion.ShowDialog()

            If motivoUbicacion.gBeMotivoUbicacion IsNot Nothing AndAlso motivoUbicacion.gBeMotivoUbicacion.IdMotivoUbicacion <> 0 Then
                txtIdMotivoUbicacion.Text = motivoUbicacion.gBeMotivoUbicacion.IdMotivoUbicacion
                txtNombreMotivoUbicacion.Text = motivoUbicacion.gBeMotivoUbicacion.Nombre
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub lnkCambioDeEstado_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCambioDeEstado.LinkClicked

        Try

            '#GT20072022_1301: Filtrar estados por propietario (especialmente cealsa que tiene varios)
            Dim fila As Object = cmbPropietarioBodega.GetSelectedDataRow
            Dim IdPropietario_ As Integer
            If fila Is Nothing Then
                Throw New Exception("Error_20220720_1300: El propietario no es valido.")
            Else
                IdPropietario_ = fila.Item("IdPropietario")
            End If

            Dim CambioEstado As New frmProducto_EstadoList() With
            {
            .Modo = frmProducto_EstadoList.pModo.Seleccion,
            .pIdPropietario = IdPropietario_
            }
            CambioEstado.ShowDialog()

            If CambioEstado.pObj IsNot Nothing AndAlso CambioEstado.pObj.IdEstado <> 0 Then
                txtIdEstado.Text = CambioEstado.pObj.IdEstado
                txtNombreEstado.Text = CambioEstado.pObj.Nombre
                Dañado = CambioEstado.pObj.Dañado
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Es_Seleccion_Multiple As Boolean


    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked

        Try

            '#CKFK 20210517 Inicialicé la forma para cuando cambie la bodega
            '#GT21122023: agregue seleccion multiple, se bloquea si es llamado el form desde otra función
            Stock = Nothing

            If Stock Is Nothing Then
                Stock = New FrmStockList With {.Modo = FrmStockList.pModo.Seleccion,
                .WindowState = FormWindowState.Maximized,
                .pIdPropietarioBodega = cmbPropietarioBodega.EditValue,
                .Es_Seleccion_Multiple = Es_Seleccion_Multiple,
                .listaStockSeleccionado = pStockRes_SeleccionMultiple, .IdBodega = cmbBodega.EditValue}
            End If

            Stock.Modo = FrmStock_List.pModo.Seleccion
            Stock.pListObjDet = pListObjDet
            Stock.WindowState = FormWindowState.Maximized

            If Stock.ShowDialog() = DialogResult.OK Then

                If (Stock.pObjStock IsNot Nothing AndAlso Stock.pObjStock.IdStock <> 0) Then

                    '#GT18102024: Validar si es un registro o es selección múltiple
                    Es_Seleccion_Multiple = False

                    pObjVWStock = Stock.pObjStock

                    txtIdStock.Text = Stock.pObjStock.IdStock
                    txtIdStock.Tag = Stock.pObjStock.IdStock

                    Dim BU As New clsBeBodega_ubicacion
                    BU = clsLnBodega_ubicacion.GetSingle(Stock.pObjStock.IdUbicacion,
                                                         cmbBodega.EditValue)

                    BU.Tramo.IdTramo = BU.IdTramo
                    clsLnBodega_tramo.GetSingle(BU.Tramo)

                    'txtIdOrigen.Text = String.Format("{0} - {1}", Stock.pObjStock.IdUbicacionActual, BU.Descripcion)
                    txtIdOrigen.Text = BU.NombreCompleto
                    txtIdOrigen.Tag = Stock.pObjStock.IdUbicacion
                    'txtProducto.Text = String.Format("{0} {1}", Stock.pObjStock.Codigo, Stock.pObjStock.Nombre)
                    txtProducto.Text = String.Format("{0}", Stock.pObjStock.Nombre_Producto)

                    '******************
                    'Stock.pObjStock.CantidadPresentacion = Stock.pObjStock.CantidadUmBas

                    txtVence.Text = Stock.pObjStock.Fecha_Vence
                    txtEstado.Text = Stock.pObjStock.NomEstado
                    txtSerie.Text = Stock.pObjStock.Serial
                    txtUnidadMedida.Text = Stock.pObjStock.UMBas
                    txtAñada.Text = Stock.pObjStock.Añada
                    txtPresentacion.Text = Stock.pObjStock.Nombre_Presentacion
                    txtIngreso.Text = Stock.pObjStock.Fecha_ingreso
                    txtLote.Text = Stock.pObjStock.Lote
                    txtLicPlate.Text = Stock.pObjStock.Lic_plate

                    '#EJC20171015_1127PM_R03:
                    'Deshabilitado por esto -> '#EJC20171015_1121PM_R01:
                    'pCantidadReservada = clsLnStock_res.GetCantidadReservadaByIdStock(Stock.pObjStock.IdStock)

                    '#EJC2017090901 Desplegar cantidad en base a la presentación
                    Dim vCantidadAUbicar As Double = 0

                    BePres.IdPresentacion = Stock.pObjStock.IdPresentacion

                    '#CM_29102018_14:00: Se obtiene la presentación porque la clase iba vacía. 
                    'Se divide la cantidad UMBas dentro del factor para obtener la cantidad de presentación disponible.
                    If BePres.IdPresentacion <> 0 Then
                        BePres = clsLnProducto_presentacion.GetSingle(BePres.IdPresentacion)
                        Stock.pObjStock.CantidadPresentacion = Stock.pObjStock.CantidadUmBas / BePres.Factor
                        vCantidadAUbicar = Stock.pObjStock.CantidadPresentacion
                    Else
                        vCantidadAUbicar = Stock.pObjStock.CantidadUmBas
                    End If

                    Stock.pObjStock.CantidadPresentacion = vCantidadAUbicar
                    txtCantidad.Minimum = 0
                    txtCantidad.Value = 0
                    txtCantidad.Maximum = Stock.pObjStock.CantidadPresentacion '- pCantidadReservada
                    'txtCantidad.Maximum = Stock.pObjStock.Cantidad
                    'txtCantidad.Minimum = ? #EJC20170909 -> No sé como calcular el mínimo aún.
                    txtCantidad.Value = Stock.pObjStock.CantidadPresentacion '- pCantidadReservada
                    txtCantidad.Tag = Stock.pObjStock.CantidadPresentacion '- pCantidadReservada
                    txtCantidad.Refresh()
                    txtCantidad.Select(0, txtCantidad.Text.Length)
                    lblCantRef.Text = FormatNumber(Stock.pObjStock.CantidadPresentacion, 6, TriState.False, TriState.False, TriState.True) '- pCantidadReservada

                    If BePres.IdPresentacion <> 0 Then

                        If BePres.EsPallet Then
                            lblFactor.Text = String.Format("{0} x {1} x {2}", BePres.Factor, BePres.CajasPorCama, BePres.CamasPorTarima)
                        Else
                            lblFactor.Text = BePres.Factor
                        End If

                    Else
                        lblFactor.Text = 1
                    End If

                    Dañado = Stock.pObjStock.Dañado
                    Utilizable = Stock.pObjStock.EstadoUtilizable
                    IdIndiceRotacion = Stock.pObjStock.IdIndiceRotacion
                    'pDimensionProducoSeleccionado = Val(Stock.pObjStock.CantidadPresentacion - pCantidadReservada) * Stock.pObjStock.AltoUbicacion * Stock.pObjStock.LargoUbicacion * Stock.pObjStock.AnchoUbicacion
                    pDimensionProducoSeleccionado = Val(Stock.pObjStock.CantidadPresentacion) * Stock.pObjStock.AltoUbicacion * Stock.pObjStock.LargoUbicacion * Stock.pObjStock.AnchoUbicacion

                    lblVolumenProducto.Text = String.Format("{0} * ({1} x {2} x {3}) = {4}", Val(lblCantRef.Text), Stock.pObjStock.AltoUbicacion, Stock.pObjStock.LargoUbicacion, Stock.pObjStock.AnchoUbicacion, pDimensionProducoSeleccionado) & " m3"

                    txtIdUbicacionDestino.Focus()

                    pUbicSugReq.IdProducto = Stock.pObjStock.IdProducto
                    pUbicSugReq.IdPresentacion = Stock.pObjStock.IdPresentacion
                    pUbicSugReq.IdEstadoProd = Stock.pObjStock.IdProductoEstado
                    pUbicSugReq.Lote = Stock.pObjStock.Lote
                    pUbicSugReq.IdUbicStock = Stock.pObjStock.IdStock

                    '#GT08092025: mostrar descriptores talla/color si la bodega tiene el parametro.
                    If Bodega.Control_Talla_Color Then
                        txtTalla.Text = Stock.pObjStock.Codigo_Talla
                        txtColor.Text = Stock.pObjStock.Codigo_Color
                    End If

                ElseIf (Stock.listaStockSeleccionado.Count > 0) Then

                    '#GT18102024: con selección multiple, asociados lista de stock para enviarla a la forma de bodegaUbic
                    Es_Seleccion_Multiple = True
                    txtIdStock.Text = 1
                    txtCantidad.Value = 1

                    If pStockRes_SeleccionMultiple Is Nothing Then
                        pStockRes_SeleccionMultiple = New List(Of clsBeVW_stock_res)
                    End If

                    For Each item In Stock.listaStockSeleccionado
                        If Not pStockRes_SeleccionMultiple.Any(Function(x) x.IdStock = item.IdStock) Then
                            pStockRes_SeleccionMultiple.Add(item)
                        End If
                    Next
                    'For Each pObjStock In Stock.listaStockSeleccionado
                    '    prepararObj_Ubic_HH_Det(pObjStock)
                    '    pListObjDet.Add(pBeTransUbicHHDet)
                    'Next
                    'Cargar_Detalle(True)

                End If

            End If

            Stock.Hide()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub lnkUbicacionDestino_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacionDestino.LinkClicked

        Try

            Dim StockId As Integer
            Dim cant As Double

            If txtIdStock.Text.Trim <> "" Then

                '#EJC2017090902
                'Agruegué Val porque cuando no se ha seleccionado un IdStock da error de conversión de string "" a número.
                StockId = Val(txtIdStock.Text)

                If Val(txtCantidad.Text) > 0 Then

                    If (tipoOperacion = 3) Then
                        If txtIdEstado.Text.Trim = "" Then
                            XtraMessageBox.Show("Seleccione el estado destino, antes de seleccionar la ubicación",
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
                            txtIdEstado.Focus()
                            Exit Sub
                        End If
                    End If

                    cant = Val(txtCantidad.Value)

                    preparaObjDet()

                    Bodega = clsLnBodega.GetSingle_By_Idbodega(AP.IdBodega)

                    Dim Ubicacion As New frmBodegaSelUbic() With
                        {.Modo = frmBodegaT.pModo.Seleccion,
                        .pListObjDet = pListObjDet,
                        .pStockRes_SeleccionMultiple = pStockRes_SeleccionMultiple,
                        .SeleccionMultiple = Es_Seleccion_Multiple}

                    Ubicacion.pObjBeB.IdBodega = cmbBodega.EditValue
                    Ubicacion.pObjBeB.Nombre = cmbBodega.Text
                    Ubicacion.Dañado = Dañado
                    Ubicacion.Utilizable = Utilizable
                    Ubicacion.IdIndiceRotacion = IdIndiceRotacion
                    Ubicacion.lUbicacionesExcluidas = Lista_Ubicaciones_Excluidas()
                    Ubicacion.pUbicSugReq = pUbicSugReq
                    Ubicacion.pUbicSugReq.IdBodega = cmbBodega.EditValue
                    Ubicacion.pUbicSugReq.Cantidad = cant

                    Ubicacion.pListObjDet = pListObjDet

                    Ubicacion.pObjBeB.cambio_ubicacion_restrictivo = Bodega.cambio_ubicacion_restrictivo
                    Ubicacion.pObjBeB.permitir_cambio_ubic_indice_menor = Bodega.permitir_cambio_ubic_indice_menor
                    Ubicacion.pObjBeB.requerir_mismo_producto_posiciones = Bodega.requerir_mismo_producto_posiciones

                    '#EJC20170913 - Funcionalidad perdida para realizar el cambio de ubicación desde BOF (Sin HH)
                    If pListObjDet IsNot Nothing AndAlso pListObjDet.Count > 0 Then
                        pObjDet.IdTareaUbicacionDet = pListObjDet.Max(Function(b) b.IdTareaUbicacionDet) + 1
                    Else
                        pObjDet.IdTareaUbicacionDet = 1
                    End If

                    '#GT18102024: seleccion simple, se debe llenar unico objeto
                    If Not Es_Seleccion_Multiple Then

                        pObjDet.IdStock = CInt(txtIdStock.Text)
                        pObjDet.Producto = New clsBeProducto
                        pObjDet.Stock = New clsBeStock
                        pObjDet.ProductoEstado = New clsBeProducto_estado
                        pObjDet.ProductoPresentacion = New clsBeProducto_Presentacion
                        pObjDet.UnidadMedida = New clsBeUnidad_medida
                        pObjDet.UbicacionDestino = New clsBeBodega_ubicacion
                        pObjDet.Producto.Nombre = txtProducto.Text.Trim()
                        pObjDet.Producto.Codigo = Stock.pObjStock.Codigo_Producto
                        pObjDet.Stock.IdUbicacion_anterior = Val(txtIdOrigen.Tag)
                        pObjDet.IdUbicacionOrigen = Val(txtIdOrigen.Tag)
                        pObjDet.Stock.Fecha_vence = txtVence.Text.Trim
                        pObjDet.Stock.Serial = txtSerie.Text.Trim
                        pObjDet.ProductoEstado.Nombre = txtEstado.Text.Trim
                        pObjDet.Stock.Añada = txtAñada.Text.Trim
                        pObjDet.Stock.Lote = txtLote.Text.Trim
                        pObjDet.Stock.Fecha_Ingreso = txtIngreso.Text.Trim
                        pObjDet.ProductoPresentacion.IdPresentacion = Stock.pObjStock.IdPresentacion
                        pObjDet.ProductoPresentacion.Nombre = txtPresentacion.Text.Trim
                        pObjDet.UnidadMedida.Nombre = txtUnidadMedida.Text.Trim
                        pObjDet.IdUbicacionDestino = Val(txtIdUbicacionDestino.Text)
                        pObjDet.Cantidad = Val(txtCantidad.Text.Trim)

                        If (tipoOperacion = 3) Then
                            pObjDet.IdEstadoDestino = txtIdEstado.Text.Trim
                            pObjDet.ProductoEstado.Nombre = txtNombreEstado.Text.Trim
                        End If

                        If String.IsNullOrEmpty(txtUbicacionDestino.Text.Trim()) = False Then
                            pObjDet.UbicacionDestino = New clsBeBodega_ubicacion
                            pObjDet.IdUbicacionDestino = CInt(txtIdUbicacionDestino.Text.Trim())
                            pObjDet.UbicacionDestino.Descripcion = txtUbicacionDestino.Text.Trim()
                        End If

                        If (chkOperadorPorlinea.Checked) Then

                            pObjDet.Operador = New clsBeOperador
                            pObjDet.IdOperadorBodega = cmbOperadores.Tag
                            pObjDet.Operador.Nombres = cmbOperadores.Text.Trim()

                            Dim lIndex As Integer = -1
                            lIndex = pListObjOp.FindIndex(Function(b) b.IdOperadorBodega = cmbOperadores.Tag)

                            If lIndex = -1 Then

                                '#EJC20220211: Agregar Operador por Línea. (Mercopan)
                                Dim Obj As New clsBeTrans_ubic_hh_op() _
                                With {.IdOperadorBodega = cmbOperadores.Tag,
                                      .User_agr = AP.UsuarioAp.IdUsuario,
                                      .Fec_agr = Now,
                                      .User_mod = AP.UsuarioAp.IdUsuario,
                                      .Fec_mod = Now}

                                pListObjOp.Add(Obj)

                            End If

                        End If

                        If chkUbicacionConHh.Checked = False Then
                            pObjDet.HoraInicio = dtpHoraInicio.Value
                            pObjDet.HoraFin = dtpHoraFin.Value
                            chkRealizadoDet.Checked = True
                        Else
                            pObjDet.HoraInicio = New DateTime(1900, 1, 1, 0, 0, 0)
                            pObjDet.HoraFin = New DateTime(1900, 1, 1, 0, 0, 0)
                        End If

                        'pObjDet.Realizado = chkRealizadoDet.Checked
                        pObjDet.Realizado = False
                        pObjDet.Cantidad = Double.Parse(txtCantidad.Text.Trim())
                        pObjDet.Activo = chkActivoDet.Checked
                        pObjDet.IdBodega = cmbBodega.EditValue

                        Ubicacion.pObjDet = pObjDet.Clone

                    End If


                    Try

                        If pListObjDet.Count > 0 Then
                            Ubicacion.pDetCorrel = pListObjDet.Max(Function(b) b.IdTareaUbicacionDet)
                        Else
                            Ubicacion.pDetCorrel = 0
                        End If

                    Catch ex As Exception
                        Ubicacion.pDetCorrel = 0
                    End Try

                    Ubicacion.pListObjMov = pListObjMov
                    Ubicacion.pListStockMov = pListStockMov

                    Ubicacion.EsCambioEstado = (tipoOperacion = 3)

                    If Ubicacion.ShowDialog() = DialogResult.Yes Then

                        pListObjMov = Ubicacion.pListObjMov
                        pListStockMov = Ubicacion.pListStockMov

                        '#EJC20171023_0400PM: Compartir lista de detalle entre formas.
                        pListObjDet = Ubicacion.pListObjDet

                        '#GT21102024: se asigna el operador a cada ubic_hh de la lista
                        If Es_Seleccion_Multiple Then
                            Asignar_Operador_Seleccion_Multiple()
                        End If

                        Cargar_Detalle(True)

                        Limpiar_Campos_Detalle()

                        Habilita_Item()

                    End If

                Else
                    '#EJC2017090903 Cambie y quité la excepción del mensaje
                    XtraMessageBox.Show("Cantidad incorrecta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Else
                '#EJC2017090904 Cambie y quité la excepción del mensaje
                XtraMessageBox.Show("Identificador de stock incorrecto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Asignar_Operador_Seleccion_Multiple()

        Try

            If (chkOperadorPorlinea.Checked) Then

                For Each ubic_det In pListObjDet

                    ubic_det.Operador = New clsBeOperador
                    ubic_det.IdOperadorBodega = cmbOperadores.Tag
                    ubic_det.Operador.Nombres = cmbOperadores.Text.Trim()

                Next

                Dim lIndex As Integer = -1
                lIndex = pListObjOp.FindIndex(Function(b) b.IdOperadorBodega = cmbOperadores.Tag)

                If lIndex = -1 Then

                    '#EJC20220211: Agregar Operador por Línea. (Mercopan)
                    Dim Obj As New clsBeTrans_ubic_hh_op() _
                    With {.IdOperadorBodega = cmbOperadores.Tag,
                          .User_agr = AP.UsuarioAp.IdUsuario,
                          .Fec_agr = Now,
                          .User_mod = AP.UsuarioAp.IdUsuario,
                          .Fec_mod = Now}

                    pListObjOp.Add(Obj)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

#End Region

#Region " Eventos TextEdit "

    Private Sub txtIdUbicacionDestino_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdUbicacionDestino.KeyDown
        If e.KeyCode = Keys.Enter AndAlso txtIdUbicacionDestino.Text.Trim <> "" Then
            txtCantidad.Focus()
        End If
    End Sub

    Private Sub txtIdMotivoUbicacion_LostFocus(sender As Object, e As EventArgs) Handles txtIdMotivoUbicacion.LostFocus

        Try

            If String.IsNullOrEmpty(txtIdMotivoUbicacion.Text.Trim()) = False Then

                gBeMotivoUbicacion = clsLnMotivo_ubicacion.GetSingle(txtIdMotivoUbicacion.Text.Trim())
                If gBeMotivoUbicacion IsNot Nothing AndAlso gBeMotivoUbicacion.IdMotivoUbicacion > 0 Then
                    txtIdMotivoUbicacion.Text = gBeMotivoUbicacion.IdMotivoUbicacion
                    txtNombreMotivoUbicacion.Text = gBeMotivoUbicacion.Nombre
                Else
                    txtIdMotivoUbicacion.Text = String.Empty
                    txtNombreMotivoUbicacion.Text = String.Empty
                    txtIdMotivoUbicacion.Focus()
                    XtraMessageBox.Show(String.Format("No existe motivo ubicación con código {0}", txtIdMotivoUbicacion.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdMotivoUbicacion_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdMotivoUbicacion.PreviewKeyDown
        Try
            If e.KeyData = Keys.Tab Then
                txtIdMotivoUbicacion_LostFocus(Nothing, Nothing)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtCodigo_LostFocus(sender As Object, e As EventArgs) Handles txtIdStock.LostFocus

        Try

            If String.IsNullOrEmpty(txtIdStock.Text.Trim()) = False Then

                pObjVWStock = clsLnStock.Get_Single_By_IdStock((txtIdStock.Text.Trim()))

                If pObjVWStock IsNot Nothing AndAlso pObjVWStock.IdStock > 0 Then

                    txtIdStock.Text = pObjVWStock.IdStock
                    txtIdStock.Tag = pObjVWStock.IdStock
                    txtIdOrigen.Text = pObjVWStock.IdUbicacion
                    txtProducto.Text = pObjVWStock.Nombre_Producto
                    txtVence.Text = pObjVWStock.Fecha_Vence
                    txtEstado.Text = pObjVWStock.NomEstado
                    txtSerie.Text = pObjVWStock.Serial
                    txtUnidadMedida.Text = pObjVWStock.UMBas
                    txtAñada.Text = pObjVWStock.Añada
                    txtPresentacion.Text = pObjVWStock.Nombre_Presentacion
                    txtIngreso.Text = pObjVWStock.Fecha_ingreso
                    txtLote.Text = pObjVWStock.Lote

                    '#EJC2017090911 Desplegar cantidad en base a la presentación
                    Dim vCantidadDisponible As Double = 0

                    BePres.IdPresentacion = pObjVWStock.IdPresentacion

                    If BePres.IdPresentacion <> 0 Then

                        'BePres.IdPresentacion = pObjStock.IdPresentacion
                        'clsLnProducto_presentacion.GetSingle(BePres)

                        'If BePres.EsPallet
                        '    vCantidadDisponible = (pObjStock.CantidadUmBas / (BePres.Factor * BePres.CamasPorTarima * BePres.CajasPorCama)) - pCantidadReservada
                        'Else
                        '    vCantidadDisponible = (pObjStock.CantidadUmBas / BePres.Factor)
                        'End If

                        vCantidadDisponible = pObjVWStock.CantidadPresentacion

                    Else
                        vCantidadDisponible = pObjVWStock.CantidadUmBas
                    End If

                    pObjVWStock.CantidadPresentacion = Math.Round(vCantidadDisponible, 6)

                    txtCantidad.Maximum = pObjVWStock.CantidadPresentacion  'IIf(pObjStock.Factor <> 0, pObjStock.Cantidad / pObjStock.Factor, pObjStock.Cantidad)

                    'txtCantidad.Minimum = #EJC2017090910
                    txtCantidad.Value = pObjVWStock.CantidadPresentacion  'IIf(pObjStock.Factor <> 0, pObjStock.Cantidad / pObjStock.Factor, pObjStock.Cantidad)
                    txtCantidad.Tag = pObjVWStock.CantidadPresentacion
                    Dañado = pObjVWStock.Dañado
                    Utilizable = pObjVWStock.EstadoUtilizable
                    IdIndiceRotacion = pObjVWStock.IdIndiceRotacion
                    lblCantRef.Text = pObjVWStock.CantidadPresentacion

                Else

                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdStock.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdStock.Text = String.Empty
                    Limpiar_Campos_Detalle()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtCodigo_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdStock.PreviewKeyDown
        Try
            If e.KeyData = Keys.Enter Then
                txtCodigo_LostFocus(Nothing, Nothing)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdUbicacionDestino_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacionDestino.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdUbicacionDestino_LostFocus(sender As Object, e As EventArgs) Handles txtIdUbicacionDestino.LostFocus

    End Sub

    Private Sub txtIdUbicacionDestino_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdUbicacionDestino.PreviewKeyDown
        Try
            If e.KeyData = Keys.Enter Then
                txtIdUbicacionDestino_LostFocus(Nothing, Nothing)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdEstado_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdEstado.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdEstado_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdEstado.PreviewKeyDown
        Try
            If e.KeyData = Keys.Enter Then
                txtIdEstado_LostFocus(Nothing, Nothing)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdEstado_LostFocus(sender As Object, e As EventArgs) Handles txtIdEstado.LostFocus

        Try

            If String.IsNullOrEmpty(txtIdEstado.Text.Trim()) = False Then

                Dim Obj As New clsBeProducto_estado

                Obj = clsLnProducto_estado.GetSingle(txtIdEstado.Text.Trim())

                If Obj IsNot Nothing AndAlso Obj.IdEstado > 0 Then
                    txtNombreEstado.Text = Obj.Nombre
                    Dañado = Obj.Dañado
                Else
                    txtIdEstado.Text = String.Empty
                    txtNombreEstado.Text = String.Empty
                    txtIdEstado.Focus()
                    XtraMessageBox.Show(String.Format("No existe cambio de estado con código {0}", txtIdEstado.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdMotivoUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdMotivoUbicacion.KeyPress

        Try

            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If
            If e.KeyChar = "." Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If
            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdMotivoUbicacion.Text.Length = 1 Then
                txtNombreMotivoUbicacion.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

#End Region

#Region " Tarimas "

    Private Sub cargarTarimasTransaccionAgregar()

        Dim I As Integer = lvTarimasUsadas.Items.Count
        Dim vTitulo$, vCodigo$, vIdTarima$

        Try

            For Each lv As ListViewItem In lvTarimasDisponibles.Items
                If (lv.Checked) Then
                    vIdTarima = lv.Text
                    vCodigo$ = lv.SubItems(1).Text
                    vTitulo$ = lv.SubItems(2).Text
                    lvTarimasUsadas.Items.Add(New ListViewItem(vIdTarima$))
                    lvTarimasUsadas.Items(I).SubItems.Add(vCodigo$)
                    lvTarimasUsadas.Items(I).SubItems.Add(vTitulo$)
                    I += 1
                    lvTarimasDisponibles.Items(lv.Index).Remove()
                End If
            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cargarTarimasTransaccionQuitar()

        Dim I As Integer = lvTarimasDisponibles.Items.Count
        Dim vTitulo$, vCodigo$, vIdTarima$

        Try
            For Each lv As ListViewItem In lvTarimasUsadas.Items
                If (lv.Checked) Then
                    vIdTarima = lv.Text
                    vCodigo$ = lv.SubItems(1).Text
                    vTitulo$ = lv.SubItems(2).Text
                    lvTarimasDisponibles.Items.Add(New ListViewItem(vIdTarima$))
                    lvTarimasDisponibles.Items(I).SubItems.Add(vCodigo$)
                    lvTarimasDisponibles.Items(I).SubItems.Add(vTitulo$)
                    I += 1
                    lvTarimasUsadas.Items(lv.Index).Remove()
                End If
            Next
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub cmdQuitar_Click(sender As Object, e As EventArgs) Handles cmdQuitar.Click

        Try
            cargarTarimasTransaccionQuitar()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

#End Region

#Region " Auxiliar "

    Private Sub Deshabilitar_Controles()

        Try

            mnuGuardar.Enabled = False
            mnuActualizar.Enabled = False
            mnuEliminar.Enabled = False
            cmdGuardar.Enabled = False
            LinkLabel2.Enabled = False
            lnkUbicacionDestino.Enabled = False
            chkUbicacionConHh.Enabled = False
            chkActivo.Enabled = False
            chkOperadorPorlinea.Enabled = False
            lnkMotivoUbicacion.Enabled = False
            cmdAgregar.Enabled = False
            cmdQuitar.Enabled = False
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            txtIdMotivoUbicacion.Enabled = False
            cmbBodega.Enabled = False
            cmbPropietarioBodega.Enabled = False

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Limpiar_Campos_Detalle()

        Try

            txtIdStock.Text = String.Empty
            txtIdStock.Tag = Nothing
            cmdGuardar.Tag = Nothing
            txtCantidad.Tag = Nothing
            txtIdOrigen.Text = String.Empty
            txtProducto.Text = String.Empty
            txtVence.Text = String.Empty
            txtEstado.Text = String.Empty
            txtSerie.Text = String.Empty
            txtUnidadMedida.Text = String.Empty
            txtAñada.Text = String.Empty
            txtPresentacion.Text = String.Empty
            txtIngreso.Text = String.Empty
            txtLote.Text = String.Empty
            txtIdUbicacionDestino.Text = String.Empty
            txtUbicacionDestino.Text = String.Empty
            txtCantidad.Text = String.Empty
            txtIdEstado.Text = String.Empty
            txtNombreEstado.Text = String.Empty
            lblCantRef.Text = String.Empty
            lblFactor.Text = String.Empty

            chkRealizadoDet.Checked = False
            lblItemBandera.Visible = False

            txtTalla.Text = String.Empty
            txtColor.Text = String.Empty
            txtLicPlate.Text = String.Empty

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub preparaObjDet()



    End Sub

    Private Sub Listar_Operadores()

        Try

            Dim lista As New List(Of clsBeTrans_ubic_hh_op)

            lista = clsLnTrans_ubic_hh_op.Get_All_By_IdTareaUbicacion(gBeTransubicacionHHEnc.IdTareaUbicacionEnc).ToList

            If lista.Count > 0 Then

                Using DT As New DataTable("Operador")

                    DT.Columns.Add("TareaUbicacion", GetType(Integer))
                    DT.Columns.Add("Operador", GetType(Integer))

                    For Each Obj As clsBeTrans_ubic_hh_op In lista
                        DT.Rows.Add(Obj.IdTareaUbicacionEnc, Obj.IdOperadorBodega)
                    Next

                End Using

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Datos_Correctos() As Boolean

        Try

            If cmbBodega.Text = "" Then
                XtraMessageBox.Show("Falta definir la bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
            End If

            If cmbPropietarioBodega.ItemIndex < 0 Then
                XtraMessageBox.Show("Falta definir el propietario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
            End If

            If txtIdMotivoUbicacion.Text = "" Then
                XtraMessageBox.Show("Falta motivo de ubicacion", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
            End If

            If pListObjDet.Count = 0 Then
                XtraMessageBox.Show("Transaccion no tiene detalle", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
            End If

            If pListObjOp.Count = 0 Then
                'If pListObjOperador.Count = 0 Then
                XtraMessageBox.Show("Transaccion no tiene operador", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
            End If



            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_CorrectosCambioEstado() As Boolean

        Try

            If String.IsNullOrEmpty(txtIdEstado.Text.Trim()) Then

                XtraMessageBox.Show("Ingrese cambio de estado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdEstado.Focus()

            Else

                Datos_CorrectosCambioEstado = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub ListaOperadores()

        Try

            Grid.BeginUpdate()

            If DT.Rows.Count > 0 Then

                For i As Integer = 0 To DT.Rows.Count - 1

                    Dim lRow As DataRow = DsOperadorUbicacion.Data.NewRow
                    lRow.Item("IdOperadorBodega") = DT(i)(0)
                    lRow.Item("Operador") = DT(i)(1)
                    lRow.Item("Selección") = False

                    If TipoTrans.Editar Then

                        If pListObjOp IsNot Nothing AndAlso pListObjOp.Count > 0 Then

                            For Each Obj As clsBeTrans_ubic_hh_op In pListObjOp

                                If Obj.IdOperadorBodega = CInt(DT(i)(0)) Then
                                    lRow.Item("Selección") = True
                                    lRow.Item("IdTransUbicHhOp") = Obj.IdTransUbicHhOp
                                End If

                            Next

                        End If

                    End If

                    DsOperadorUbicacion.Data.AddDataRow(lRow)

                Next

            End If

            Grid.EndUpdate()
            Grid.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GrdOperadorBobega.Columns("Selección").RealColumnEdit, RepositoryItemCheckEdit)

            RemoveHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdDetalle
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            SplashScreenManager.CloseForm(False)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Cambio de ubicacion"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Function Lista_Ubicaciones_Excluidas() As List(Of clsBeUbicacionExcluida)

        Dim lUbicList As New List(Of clsBeUbicacionExcluida)
        Dim lUbic As clsBeUbicacionExcluida

        For Each Obj As clsBeTrans_ubic_hh_det In pListObjDet

            lUbic = New clsBeUbicacionExcluida
            lUbic.idStock = CInt(txtIdStock.Tag)
            lUbic.idUbicacion = Obj.IdUbicacionDestino
            lUbicList.Add(lUbic)

        Next

        Return lUbicList

    End Function

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir1.ItemClick

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")
        Imprimir_Vista()

    End Sub

    Private Sub Habilita_Item()
        GroupControl4.Enabled = True
        GroupControl5.Enabled = True
        cmdGuardar.Visible = False
    End Sub

    Private Sub Deshabilita_Item()

        cmbOperadores.Enabled = False
        txtIdStock.Enabled = False
        txtProducto.Enabled = False
        txtSerie.Enabled = False
        txtAñada.Enabled = False
        txtIngreso.Enabled = False
        txtUnidadMedida.Enabled = False
        txtIdOrigen.Enabled = False
        txtVence.Enabled = False
        txtEstado.Enabled = False
        txtLote.Enabled = False
        txtPresentacion.Enabled = False
        txtIdUbicacionDestino.Enabled = False
        txtUbicacionDestino.Enabled = False
        txtCantidad.Enabled = False
        chkRealizadoDet.Enabled = False
        chkActivoDet.Enabled = False
        txtIdEstado.Enabled = False
        txtNombreEstado.Enabled = False
        cmdNewP.Enabled = False
        cmdGuardar.Enabled = False
        mnuEliminarDet.Enabled = False
        lnkCambioDeEstado.Enabled = False
        cmdGuardar.Visible = True

    End Sub

    Private Sub Habilita_Menu()

        mnuGuardar.Enabled = True
        mnuActualizar.Enabled = True
        mnuEliminar.Enabled = True
        mnuAsignacion.Enabled = True

        xtabGeneral.Enabled = True
        xtabOperador.Enabled = True
        xtabTarima.Enabled = True
        xTabDetalle.Enabled = True

        cmdNewP.Enabled = True
        cmdGuardar.Enabled = True
        mnuEliminarDet.Enabled = True

    End Sub

    Private Sub Deshabilita_Menu()

        Deshabilitar_Controles()

        mnuGuardar.Enabled = False
        mnuActualizar.Enabled = False
        mnuEliminar.Enabled = False
        mnuPendiente.Enabled = False

        cmdNewP.Enabled = False
        cmdGuardar.Enabled = False
        mnuEliminarDet.Enabled = False
        cmdEliminarDocumento.Enabled = False

    End Sub

    Private Sub txtIdUbicacionDestino_Validated(sender As Object, e As EventArgs) Handles txtIdUbicacionDestino.Validated

        Try

            If String.IsNullOrEmpty(txtIdUbicacionDestino.Text.Trim()) = False Then

                If (txtIdOrigen.Text.Trim() <> txtIdUbicacionDestino.Text.Trim()) Then

                    Dim BeBodegaUbicacion As New clsBeBodega_ubicacion
                    BeBodegaUbicacion = clsLnBodega_ubicacion.GetSingle(txtIdUbicacionDestino.Text.Trim(), AP.IdBodega, "", Dañado, 0)

                    If BeBodegaUbicacion IsNot Nothing AndAlso BeBodegaUbicacion.IdUbicacion > 0 Then

                        If (IdIndiceRotacion <> 0 AndAlso BeBodegaUbicacion.IdIndiceRotacion <> 0) AndAlso
                                 (IdIndiceRotacion <> BeBodegaUbicacion.IdIndiceRotacion) Then
                            '#EJC20180126: Validación del índice de rotación Producto Vrs. Ubicación.
                            If XtraMessageBox.Show(String.Format("¿El índice de rotación de la ubicación {0} 
                                no coincide con el índice de rotación del producto {1}, 
                                colocar en ésta ubicación de todas formas?", BeBodegaUbicacion.IdIndiceRotacion, IdIndiceRotacion),
                                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                Exit Sub
                            End If

                        End If

                        txtIdUbicacionDestino.Text = BeBodegaUbicacion.IdUbicacion
                        txtUbicacionDestino.Text = BeBodegaUbicacion.NombreCompleto
                    Else
                        XtraMessageBox.Show(String.Format("No existe Ubicación código {0}", txtIdUbicacionDestino.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtIdUbicacionDestino.Text = String.Empty
                        txtUbicacionDestino.Text = String.Empty
                    End If

                Else
                    txtIdUbicacionDestino.Text = String.Empty
                    txtUbicacionDestino.Text = String.Empty
                    XtraMessageBox.Show(String.Format("La ubicación debe ser diferente", txtIdUbicacionDestino.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdUbicacionDestino.Focus()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmCambioUbicacion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            Bodega = New clsBeBodega
            Bodega = clsLnBodega.GetSingle_By_Idbodega(cmbBodega.EditValue)

            If Not IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue) Then
                XtraMessageBox.Show("No hay propietarios definidos para la bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_Operadores(cmbOperadores) Then
                XtraMessageBox.Show("No hay operadores definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbOperadores_EditValueChanged(sender As Object, e As EventArgs) Handles cmbOperadores.EditValueChanged

        If cmbOperadores.EditValue <> 0 Then

            If cmbBodega.Text <> "" Then
                cmbOperadores.Tag = clsLnOperador_bodega.Get_IdOperadorBodega_By_IdOperador(cmbOperadores.EditValue, cmbBodega.EditValue)
            End If

            'pListObjOp.Clear()
            'pBetrans_ubic_hh_op = New clsBeTrans_ubic_hh_op()
            'pBetrans_ubic_hh_op.IdTransUbicHhOp = 0
            'pBetrans_ubic_hh_op.IdTareaUbicacionEnc = 0
            'pBetrans_ubic_hh_op.IdOperadorBodega = cmbOperadores.EditValue
            'pBetrans_ubic_hh_op.User_agr = AP.UsuarioAp.IdUsuario
            'pBetrans_ubic_hh_op.Fec_agr = Now
            'pBetrans_ubic_hh_op.User_mod = AP.UsuarioAp.IdUsuario
            'pBetrans_ubic_hh_op.Fec_mod = Now
            'pListObjOp.Add(pBetrans_ubic_hh_op)

        End If

    End Sub

    Private Function Tiene_Registros_Sin_Procesar() As Boolean

        Tiene_Registros_Sin_Procesar = False

        Try

            For Each Det In pListObjDet

                If Not Det.Realizado Then
                    Tiene_Registros_Sin_Procesar = True
                    Exit For
                End If

            Next

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try


    End Function

    Private Sub mnuLiberarStockNoProcesado_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLiberarStockNoProcesado.ItemClick

        Try

            If Tiene_Registros_Sin_Procesar() Then

                If XtraMessageBox.Show("¿Liberar producto no procesado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    If Not clsLnTrans_ubic_hh_enc.Liberar_Stock_Reservado_Sin_Procesar(gBeTransubicacionHHEnc) Then
                        XtraMessageBox.Show("No se pudo liberar el producto reservado. ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Close()
                    Else
                        XtraMessageBox.Show("Se ha liberado el producto!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Not InvokeListarTareasUbicacion Is Nothing Then
                            InvokeListarTareasUbicacion.Invoke
                        End If
                        Close()
                    End If

                End If

            Else
                XtraMessageBox.Show("No hay productos pendientes de procesar!", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

#End Region


    Private Sub Cargar_Datos_Stock_Reservado(ByVal vIdTran As Integer)

        Try

            DTStockReservado.Clear() : grdStockReservado.DataSource = Nothing

            DT = clsLnVW_Stock_Res_Pedido.Get_All_By_IdBodega_DT(AP.IdBodega, vIdTran)

            If DT.Rows.Count > 0 Then

                mnuPendiente.Enabled = True

                grdStockReservado.DataSource = DT

                If GridView6.RowCount > 0 Then

                    GridView6.OptionsView.ShowFooter = True
                    GridView6.OptionsView.ColumnAutoWidth = False


                    lblRegs.Caption = String.Format("Registros: {0}", GridView6.RowCount)

                    GridView6.Columns("CantidadFisica").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView6.Columns("CantidadFisica").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView6.Columns("CantidadFisica").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView6.Columns("CantidadFisica").DisplayFormat.FormatString = "{0:n6}"


                    GridView6.Columns("Factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView6.Columns("Factor").DisplayFormat.FormatString = "{0:n6}"

                    GridView6.Columns("cantidad_presentacion_reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView6.Columns("cantidad_presentacion_reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView6.Columns("cantidad_presentacion_reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView6.Columns("cantidad_presentacion_reservada").DisplayFormat.FormatString = "{0:n6}"

                    GridView6.Columns("cantidad_umbas_reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView6.Columns("cantidad_umbas_reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView6.Columns("cantidad_umbas_reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView6.Columns("cantidad_umbas_reservada").DisplayFormat.FormatString = "{0:n6}"


                    GridView6.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView6.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView6.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView6.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView6.Columns("fec_agr").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView6.Columns("fec_agr").DisplayFormat.FormatString = "g"

                    GridView6.BestFitColumns(True)


                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuImportarListaCambioUbic_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImportarListaCambioUbic.ItemClick

        Try

            mnuImportarListaCambioUbic.Enabled = False

            Importar_Excel()

            mnuImportarListaCambioUbic.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Importar_Excel()

        Try

            chkOperadorPorlinea.Checked = False
            chkUbicacionConHh.Checked = False

            chkOperadorPorlinea.Enabled = False
            chkUbicacionConHh.Enabled = False

            '#GT20062022_1520: enviamos la bodega para comparar contra la bodega leida x linea.
            Dim pBodega As New clsBeBodega
            pBodega.IdBodega = cmbBodega.EditValue
            pBodega.Nombre = cmbBodega.Text

            Dim vTipoMantenimiento As String = IIf(tipoOperacion = 2, "Reubicación", "CambioEstado")
            Dim vEsCambioEstado As Boolean = tipoOperacion = 3

            Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Importación " + Me.Text,
                .pTipoMantenimiento = vTipoMantenimiento,
                .Listar = Nothing,
                .pBodegaOrigen = pBodega,
                .IdInventarioEnc = -1,
                .EsCambioEstado = vEsCambioEstado}

            If Carga.ShowDialog() = DialogResult.OK Then

                '#GT16062022_1913: hago esto para que tome focus en el tab donde esta el comboOperadores
                tabDatos.SelectedTabPageIndex = 1

                pListObjMov = Carga.pListObjMov
                pListStockMov = Carga.pListStockMov

                '#EJC20171023_0400PM: Compartir lista de detalle entre formas.
                pListObjDet = Carga.pListObjDet


                If (chkOperadorPorlinea.Checked) Then

                    '#GT16062022_0905: el operador podria enviarlo al fmrExcel, pero si quieren cambiarlo previo a guardar, aun se podría.
                    For Each Obj As clsBeTrans_ubic_hh_det In pListObjDet

                        Obj.Operador = New clsBeOperador
                        Dim fila As Object = cmbOperadores.GetSelectedDataRow

                        If fila Is Nothing Then
                            Throw New Exception("Error_20220616_1848: No hay un operador para asignar.")
                        Else
                            Obj.IdOperadorBodega = fila.Item("IdOperador")
                            Obj.Operador.Nombres = fila.Item("Nombre")
                        End If
                    Next

                End If

                Cargar_Detalle(True)

                tabDatos.SelectedTabPageIndex = 1

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            RibbonControl.Enabled = True
        End Try

    End Sub

    Private Sub mnuPendiente_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPendiente.ItemClick
        Set_Tarea_Pendiente()
    End Sub

    Private Sub Set_Tarea_Pendiente()

        Try

            If XtraMessageBox.Show("¿Cambiar a estado Nuevo para que la HH visualice la tarea nuevamente?",
                                    Text,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) = DialogResult.Yes Then

                gBeTransubicacionHHEnc.Estado = "Nuevo"

                Dim vResutl As Integer = clsLnTrans_ubic_hh_enc.Actualizar_Estado_Tarea(gBeTransubicacionHHEnc)

                If vResutl > 0 Then

                    XtraMessageBox.Show("Se actualizó el estado, verifique la handheld",
                     Text,
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                     Text,
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdDescargarPlantilla_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdDescargarPlantilla.ItemClick

        Dim vNombreArchivo As String = "WMS_Formato_Importacion_Cambio_Ubicacion.xlsx"
        Dim vRutaArchivo As String = CurDir() & "\Mantenimientos\plantillas\" & vNombreArchivo

        If Not tipoOperacion = 2 Then
            vNombreArchivo = "WMS_Formato_Importacion_Cambio_Estado"
            vRutaArchivo = CurDir() & "\Mantenimientos\plantillas\" & vNombreArchivo
        End If



        Try
            If File.Exists(vRutaArchivo) Then
                ' Crear un nuevo SaveFileDialog
                Using saveDialog As New SaveFileDialog()
                    saveDialog.Title = "Guardar plantilla de importación de productos"
                    saveDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
                    saveDialog.FileName = vNombreArchivo
                    ' Mostrar el cuadro de diálogo de guardado
                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        ' Copiar el archivo a la ruta seleccionada por el usuario
                        File.Copy(vRutaArchivo, saveDialog.FileName, True)
                        XtraMessageBox.Show("Archivo guardado exitosamente en: " & saveDialog.FileName, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Else
                XtraMessageBox.Show("No existe el formato en: " & vRutaArchivo, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdEliminarDocumento_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdEliminarDocumento.ItemClick

        Dim BeUbicacionHH As New clsBeTrans_ubic_hh_enc

        Try

            If Not permiteMenu("3.2.1.2") Then
                Return
            End If

            cmdEliminarDocumento.Enabled = False

            If XtraMessageBox.Show("¿Eliminar cambio de ubicación?",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                gBeTransubicacionHHEnc.Activo = False

                Dim vResultadoEliminoStockReservado As Boolean = False

                If gBeTransubicacionHHEnc.Estado = "NUEVO" Then

                    If clsLnTrans_ubic_hh_enc.Eliminar(gBeTransubicacionHHEnc) Then

                        Dim vInterfaceSAP As Boolean = clsLnI_nav_config_enc.Get_Interface_SAP(AP.IdConfiguracionInterface)

                        Try

                            If vInterfaceSAP Then

                                'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.
                                If Ejecutar_Interface("8-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & gBeTransubicacionHHEnc.No_Documento & "-2" & "-" & clsBD.Instancia.NombreInstancia, Me) Then
                                    XtraMessageBox.Show("Se ha eliminado el cambio de ubicación asociado al traslado " & gBeTransubicacionHHEnc.No_Documento & " y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If

                            Else

                                XtraMessageBox.Show("Se ha eliminado el cambio de ubicación y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            End If

                        Catch ex As Exception
                            XtraMessageBox.Show("Se ha eliminado el cambio de ubicación asociado al traslado " & gBeTransubicacionHHEnc.No_Documento, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End Try

                        If Not InvokeListarUbicHH Is Nothing Then InvokeListarUbicHH.Invoke

                        Close()

                        frmCambioUbicacion_List.Dgrid.Refresh()

                    End If

                Else

                    XtraMessageBox.Show("No se puede eliminar el registro porque ya se está procesando", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            End If

            cmdEliminarDocumento.Enabled = True

        Catch ex As Exception
            cmdEliminarDocumento.Enabled = True
            'Actualizar_Progreso(lblprg, "Error_202312121133: " & ex.Message())
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'lblprg.Text = "" : lblprg.Visible = False
        End Try

    End Sub
End Class