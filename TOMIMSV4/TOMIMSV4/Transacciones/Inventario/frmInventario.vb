Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Reflection
Imports System.Web.UI.WebControls.Expressions
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class frmInventario

    Public gBeTransInvEnc As New clsBeTrans_inv_enc
    Public gBeTransTramoInv As New clsBeTrans_inv_tramo
    Public ListaConteos As New List(Of clsBeTrans_inv_enc)
    Public glistaInv As New List(Of clsBeTrans_inv_enc)
    Public ObjN As New clsBeBodega_Tramo_Seleccion
    Public gBeBodegaTramos As New List(Of clsBeBodega_Tramo_Seleccion)
    Public gBeBodegaTramosAsignados As New List(Of clsBeBodega_Tramo_Seleccion)
    Public listaUbicaciones As New List(Of clsBeTrans_inv_ciclico_ubic)
    'Private ListInventarioCiclico As New DataTable
    Private ListInventarioCiclico As New List(Of clsBeTrans_inv_ciclico)
    Private ListReconteo As New List(Of clsBeTrans_inv_enc_reconteo)
    Public gBeAgregar As New clsBeTrans_inv_enc
    Public pListProductos As New List(Of Integer)
    Private pIdPropietario As Integer

    Private DTUbicaciones As New DataTable
    Private DTInventarioConteo As New DataTable("InventarioConteo")
    Private DTInventarioVerifica As New DataTable("InventarioVerifica")
    Private DTC As New DataTable("InventarioComparación")
    Private DTU As New DataTable("UbicacionesInvetario")
    Public Shared DTInventarioCiclico As New DataTable("InventarioCiclico")
    Private DTInventarioCongelado As New DataTable("InventarioCongelado")

    '#GT17012025: variables para controlar el inventario con diferencias
    Private ListInventarioDiferenciaCiclico As New List(Of clsBeTrans_inv_ciclico)
    Public Shared DTInventarioDiferenciaCiclico As New DataTable("InventarioDiferenciaCiclico")


    Public Delegate Sub Cargar()
    Public Property InvokeListarInventario As Cargar

    Public pBeUbicacion As New clsBeBodega_ubicacion

    Public ListUbicacion As New List(Of clsBeBodega_ubicacion)

    Public nombreCampo As String = ""
    Public Property Dañado As Boolean
    Public IdInventario As Integer
    Private DTOri As New DataTable
    Private gConnection As SqlConnection
    Private gTransaction As SqlTransaction


    Private DTOriOperador As New DataTable

    Private ufilt() As String
    Private ufiltcod As Boolean
    Private ufiltubic As String
    Private ufiltcnt As Integer

    'MA20260105
    Private TotalUbicaciones As Integer = 0
    Private UbicacionesContadas As Integer = 0
    Private UbicacionesPendientes As Integer = 0
    Private ValorObjetivoGauge As Double = 0

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Cargar_Forma()

        Dim clsTrans As New clsTransaccion

        Try

            clsTrans.Begin_Transaction()

            IsLoading = True

            CheckForIllegalCrossThreadCalls = False

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando datos..")

            GridViewTramos.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFullFocus
            Dim ritem As RepositoryItemCheckEdit = TryCast(GridViewTramos.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritemPropietario_CheckedChanged

            If Not AP.Listar_Bodegas_By_Usuario(cmbBodega, clsTrans.lConnection, clsTrans.lTransaction) Then
                XtraMessageBox.Show("No hay bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            If Not IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue, clsTrans.lConnection, clsTrans.lTransaction) Then
                XtraMessageBox.Show("No hay propietarios definidos para la bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_TipoInventario(cmbTipoInventario, clsTrans.lConnection, clsTrans.lTransaction) Then
                XtraMessageBox.Show("No hay tipos de inventarios definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_TipoConteo(cmbTipoConteo, clsTrans.lConnection, clsTrans.lTransaction) Then
                XtraMessageBox.Show("No hay tipos de conteos definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_Operadores(cmbOperador, clsTrans.lConnection, clsTrans.lTransaction) Then
                XtraMessageBox.Show("No hay Operadores definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_Operadores_By_Rol_Inventario(cmbOperadorProd, clsTrans.lConnection, clsTrans.lTransaction) Then
                XtraMessageBox.Show("No hay Operadores definidos para toma de inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            IMS.Listar_ClientesByEmpresaSistema(cmbCliente, AP.IdEmpresa, clsTrans.lConnection, clsTrans.lTransaction)

            IMS.Listar_Producto_Familia(cmbProductoFamilia, AP.IdEmpresa, clsTrans.lConnection, clsTrans.lTransaction)

            gBeBodegaTramos = clsLnBodega_tramo.Get_All_For_Seleccion(AP.IdBodega, clsTrans.lConnection, clsTrans.lTransaction)

            SetDatataTableConteo()
            SetDatataTableVerifica()
            SetDatataTableCompara()
            SetDatataTableUbicaciones()
            SetDatataTableCiclico()
            SetDatataTableCongelado()
            SetDatataTableDiferenciaCiclico()

            Crea_TreeList_Bodega(dgridAsignacionUbicaciones)
            Crea_TreeList_BodegaOperador(dgridAsignacionOperadores)
            Crea_TreeList_AsignaProductos(dgridAsignacionProductos)

            rgrpRegularizar.Visible = False
            rgprImportar.Visible = False

            IMS.Listar_Centro_Costo_Inv_By_IdEmpresa(cmbCentroCosto, AP.IdEmpresa, clsTrans.lConnection, clsTrans.lTransaction)

            Select Case Modo

                Case TipoTrans.Nuevo

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    dtpHoraInicio.EditValue = Now
                    dtpHoraFin.EditValue = Now
                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    cmdActualizarInvInicial.Enabled = False
                    cmdConvertir.Enabled = False
                    rgrp.Enabled = False
                    cmdCompracionStock.Enabled = False
                    cmbTipoInventario.Enabled = True

                    cmbTipoInventario.EditValue = 1
                    xtraTabInv.TabPages.Remove(tabDetalle)
                    xtraTabInv.TabPages.Remove(Tramos)
                    xtraTabInv.TabPages.Remove(tabAsignacionUbicaciones)
                    xtraTabInv.TabPages.Remove(tabAsignacionOperadores)
                    xtraTabInv.TabPages.Remove(tabAsignacionProductos)
                    xtraTabInv.TabPages.Remove(tabConteo)
                    xtraTabInv.TabPages.Remove(tabReconteo)
                    xtraTabInv.TabPages.Remove(tabInvTeorico)
                    xtraTabInv.TabPages.Remove(tabInvCongelado)
                    xtraTabInv.TabPages.Remove(TabInventarioCostos)
                    xtraTabInv.TabPages.Remove(tbne)
                    xtraTabInv.TabPages.Remove(tabKPI)
                    xtraTabInv.TabPages.Remove(tabComparativoERPWMS)
                    xtraTabInv.TabPages.Remove(tabDiferenciasInventario)
                    xtraTabInv.TabPages.Remove(tabConteoOperador)
                    xtraTabInv.TabPages.Remove(tabUbicacionesNoContadas)

                    lblEsSistema.Visible = False
                    chkSistema.Visible = False
                    lblMostrarCantidad.Visible = True
                    chkMostrarCantidad.Visible = True
                    lblCambiaUbicacion.Visible = False
                    chkCambiaUbicacion.Visible = False
                    Label2.Visible = False
                    chkCaptNtExist.Visible = False
                    Label4.Visible = False
                    chkCapturarNoAsignado.Visible = False
                    chkDobleVerifica.Visible = True
                    lblDobleVerif.Visible = True

                    grpRegularizarInvStock.Visible = False

                    chkTramosAsig.Visible = False
                    rpgReconteo.Visible = False
                    pgImprimir.Visible = False
                    pgExportar.Visible = False
                    grpImprimirInicial.Visible = False
                    'EFREN10052021
                    chkMultiPropietario.Visible = True

                    lblCod.Text = clsLnTrans_inv_enc.MaxID()

                    '#EJC20180809: Para agregar una funcionalidad a futuro de productos con movimientos en un rango de fecha.
                    dtpUltimoInv.EditValue = clsLnTrans_inv_enc.Get_Fecha_Ultimo_Inventario()

                    Estado.Text = "Nuevo"
                    Fecha.DateTime = Today
                    gBeTransInvEnc = New clsBeTrans_inv_enc With {.IsNew = True}

                    Listar_Tramos()

                    cmbCliente.EditValue = clsLnCliente.Get_IdBodega_By_Codigo(AP.Bodega.Codigo, clsTrans.lConnection, clsTrans.lTransaction)

                Case TipoTrans.Editar

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = True
                    mnuEliminar.Enabled = True
                    cmdActualizarInvInicial.Enabled = True
                    chkTramosAsig.Checked = True
                    chkSeleccionarTodos.Visible = False
                    cmdConvertir.Enabled = True
                    rgrp.Enabled = True
                    rgprImportar.Visible = True
                    cmdCompracionStock.Enabled = True
                    cmbTipoInventario.Enabled = False
                    'EFREN290720211107: si edita, el checkbox se valida desde el registro almacenado
                    chkMultiPropietario.Enabled = gBeTransInvEnc.multi_propietario
                    chkCapturarNoAsignado.Checked = gBeTransInvEnc.Capturar_No_Asignados

                    If gBeTransInvEnc.Inicial Then
                        xtraTabInv.TabPages.Add(Tramos)
                        xtraTabInv.TabPages.Add(tabDetalle)
                        xtraTabInv.TabPages.Remove(tabAsignacionUbicaciones)
                        xtraTabInv.TabPages.Remove(tabAsignacionOperadores)
                        xtraTabInv.TabPages.Remove(tabAsignacionProductos)
                        xtraTabInv.TabPages.Remove(tabConteo)
                        xtraTabInv.TabPages.Remove(tabReconteo)
                        xtraTabInv.TabPages.Remove(tabInvCongelado)
                        xtraTabInv.TabPages.Remove(TabInventarioCostos)
                        xtraTabInv.TabPages.Add(tbne)

                        cmdReconteo.Enabled = False
                        rpgReconteo.Visible = False
                        pgImprimir.Visible = False
                        grpRegularizarInvStock.Visible = False
                        RibbonPage1.Text = "Inventario Inicial"
                        Text = "Inventario Inicial"
                        lblCliente.Visible = False
                        cmbCliente.Visible = False
                        lblSeccionAjuste.Visible = False
                        cmbProductoFamilia.Visible = False
                    Else
                        xtraTabInv.TabPages.Remove(Tramos)
                        xtraTabInv.TabPages.Remove(tabDetalle)
                        xtraTabInv.TabPages.Add(tabAsignacionUbicaciones)
                        xtraTabInv.TabPages.Add(tabAsignacionOperadores)
                        xtraTabInv.TabPages.Add(tabAsignacionProductos)
                        xtraTabInv.TabPages.Add(tabConteo)
                        xtraTabInv.TabPages.Add(tabReconteo)
                        xtraTabInv.TabPages.Add(tabInvCongelado)
                        xtraTabInv.TabPages.Add(TabInventarioCostos)
                        xtraTabInv.TabPages.Add(tbne)
                        xtraTabInv.TabPages.Add(tabUbicacionesNoContadas)

                        grpImprimirInicial.Visible = False
                        cmdConvertir.Enabled = False
                        rgrpRegularizar.Visible = False
                        cmdCompracionStock.Enabled = False
                        rpgComparacion.Visible = False
                        rgprImportar.Visible = True
                        rgrp.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        rgrp.Enabled = True
                        lblEsSistema.Visible = True
                        chkSistema.Visible = True
                        lblMostrarCantidad.Visible = True
                        chkMostrarCantidad.Visible = True
                        lblCambiaUbicacion.Visible = True
                        chkCambiaUbicacion.Visible = True
                        pgImprimir.Visible = True
                        RibbonPage1.Text = "Inventario Cíclico"
                        Text = "Inventario Cíclico"
                        lblCliente.Visible = True
                        cmbCliente.Visible = True
                        lblSeccionAjuste.Visible = True
                        cmbProductoFamilia.Visible = True
                        rpgKPI.Visible = True

                    End If

                    cmbPropietario.EditValue = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(gBeTransInvEnc.Idpropietario,
                                                                                                                     gBeTransInvEnc.IdBodega,
                                                                                                                     clsTrans.lConnection,
                                                                                                                     clsTrans.lTransaction)

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Listando datos del encabezado...")
                    Cargar_Datos_Encabezado()

                    'SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Listando inventario congelado...")
                    Carga_Inventario_Congelado(clsTrans.lConnection, clsTrans.lTransaction)
                    'SplashScreenManager.CloseForm(False)

                    SplashScreenManager.Default.SetWaitFormDescription("Listando productos no existentes...")
                    Cargar_Productos_No_Existentes(clsTrans.lConnection, clsTrans.lTransaction)

                    SplashScreenManager.Default.SetWaitFormDescription("Listando datos del inventario...")
                    Listar_Datos_De_Inventario()

                    gBeBodegaTramosAsignados = clsLnBodega_tramo.Get_All_Tramos_For_Seleccion_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc,
                                                                                                                 clsTrans.lConnection,
                                                                                                                 clsTrans.lTransaction)

                    Listar_Tramos()

                    If gBeTransInvEnc.Inicial = False Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Listando bodegas")

                        Listar_Bodega(clsTrans.lConnection, clsTrans.lTransaction)

                    End If

                    'SplashScreenManager.Default.SetWaitFormDescription("Listando productos")

                    Listar_Productos(clsTrans.lConnection, clsTrans.lTransaction)

                    Carga_Detalle_Ciclico()

                    Carga_Regularizacion(clsTrans.lConnection, clsTrans.lTransaction)
                    'Timer1.Enabled = True

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            SplashScreenManager.CloseForm(False)
            IsLoading = False
        End Try

    End Sub

    Private Sub SetDatataTableConteo()

        DTInventarioConteo.Columns.Clear()
        DTInventarioConteo.Columns.Add("IdInventario", GetType(Integer))
        DTInventarioConteo.Columns.Add("IdTramo", GetType(Integer))
        DTInventarioConteo.Columns.Add("Tramo", GetType(String))
        DTInventarioConteo.Columns.Add("Código", GetType(String))
        DTInventarioConteo.Columns.Add("Producto", GetType(String))
        DTInventarioConteo.Columns.Add("Presentación", GetType(String))
        DTInventarioConteo.Columns.Add("Cantidad Conteo", GetType(Double))
        DTInventarioConteo.Columns.Add("Estado Detalle", GetType(String))
        DTInventarioConteo.Columns.Add("Operador", GetType(String))
        DTInventarioConteo.Columns.Add("FechaConteo", GetType(Date))
        DTInventarioConteo.Columns.Add("Ubicación", GetType(String))
        DTInventarioConteo.Columns.Add("Lote", GetType(String))
        DTInventarioConteo.Columns.Add("Licencia", GetType(String))
        DTInventarioConteo.Columns.Add("FechaVence", GetType(Date))
        DTInventarioConteo.Columns.Add("IdProducto", GetType(Integer))
        DTInventarioConteo.Columns.Add("Idinventariodet", GetType(Integer))

    End Sub

    Private Sub SetDatataTableUbicaciones()

        DTU.Columns.Clear()
        DTU.Columns.Add("Área", GetType(String))
        DTU.Columns.Add("Sector", GetType(String))
        DTU.Columns.Add("Tramo", GetType(String))
        DTU.Columns.Add("IdUbicación", GetType(Integer))
        DTU.Columns.Add("Ubicación", GetType(String))

    End Sub

    Private Sub SetDatataTableVerifica()

        DTInventarioVerifica.Columns.Clear()
        DTInventarioVerifica.Columns.Add("IdInventario", GetType(Integer))
        DTInventarioVerifica.Columns.Add("IdTramo", GetType(Integer))
        DTInventarioVerifica.Columns.Add("Tramo", GetType(String))
        DTInventarioVerifica.Columns.Add("Código", GetType(String))
        DTInventarioVerifica.Columns.Add("Producto", GetType(String))
        DTInventarioVerifica.Columns.Add("Presentación", GetType(String))
        DTInventarioVerifica.Columns.Add("Cantidad Verifica", GetType(Double))
        DTInventarioVerifica.Columns.Add("Estado Verifica", GetType(String))
        DTInventarioVerifica.Columns.Add("Operador", GetType(String))
        DTInventarioVerifica.Columns.Add("FechaVerifica", GetType(Date))
        DTInventarioVerifica.Columns.Add("IdProducto", GetType(Integer))
        DTInventarioVerifica.Columns.Add("IdInventarioRes", GetType(Integer))
        DTInventarioVerifica.Columns.Add("Ubicacion", GetType(String))
        DTInventarioVerifica.Columns.Add("Licencia", GetType(String))

    End Sub

    Private Sub SetDatataTableCompara()

        DTC.Columns.Clear()
        DTC.Columns.Add("IdInventario", GetType(Integer))
        DTC.Columns.Add("IdTramo", GetType(Integer))
        DTC.Columns.Add("IdProducto", GetType(Integer))
        DTC.Columns.Add("Tramo", GetType(String))
        DTC.Columns.Add("Código", GetType(String))
        DTC.Columns.Add("Producto", GetType(String))
        DTC.Columns.Add("Presentación", GetType(String))
        DTC.Columns.Add("UM", GetType(String))
        DTC.Columns.Add("Conteo", GetType(Double))
        DTC.Columns.Add("Verifica", GetType(Double))
        DTC.Columns.Add("Diferencia", GetType(Double))
        DTC.Columns.Add("EstadoConteo", GetType(String))
        DTC.Columns.Add("EstadoVerifica", GetType(String))
        DTC.Columns.Add("Ubicacion", GetType(String))

    End Sub

    Private Sub SetDatataTableCiclico()

        DTInventarioCiclico.Columns.Clear()
        DTInventarioCiclico.Columns.Add("IdInvCiclico", GetType(Integer))
        DTInventarioCiclico.Columns.Add("Ubicación", GetType(String))
        DTInventarioCiclico.Columns.Add("Ubicación_Nueva", GetType(String))
        DTInventarioCiclico.Columns.Add("IdStock", GetType(Integer))
        DTInventarioCiclico.Columns.Add("Código", GetType(String))
        DTInventarioCiclico.Columns.Add("Producto", GetType(String))
        DTInventarioCiclico.Columns.Add("TipoProducto", GetType(String))
        DTInventarioCiclico.Columns.Add("Presentación", GetType(String))
        DTInventarioCiclico.Columns.Add("Estado_Stock", GetType(String))
        DTInventarioCiclico.Columns.Add("Estado", GetType(String))
        DTInventarioCiclico.Columns.Add("Lote_Stock", GetType(String))
        DTInventarioCiclico.Columns.Add("Lote", GetType(String))
        DTInventarioCiclico.Columns.Add("Vence_Stock", GetType(Date))
        DTInventarioCiclico.Columns.Add("Vence", GetType(Date))
        'DTInventarioCiclico.Columns.Add("Operador", GetType(String))
        DTInventarioCiclico.Columns.Add("Cant.Teorica.Pres", GetType(Double))
        DTInventarioCiclico.Columns.Add("Cant.Teorica.UMBas", GetType(Double))
        DTInventarioCiclico.Columns.Add("PesoStock", GetType(Double))
        DTInventarioCiclico.Columns.Add("Cant.Conteo.Pres", GetType(Double))
        DTInventarioCiclico.Columns.Add("Cant.Conteo.UMBas", GetType(Double))
        DTInventarioCiclico.Columns.Add("PesoConteo", GetType(Double))
        DTInventarioCiclico.Columns.Add("Cant.Reconteo.Pres", GetType(Double))
        DTInventarioCiclico.Columns.Add("Cant.Reconteo.UMBas", GetType(Double))
        DTInventarioCiclico.Columns.Add("PesoReconteo", GetType(Double))
        DTInventarioCiclico.Columns.Add("Dif.Cant.UMBas", GetType(Double))
        DTInventarioCiclico.Columns.Add("Extraviado", GetType(Double))
        DTInventarioCiclico.Columns.Add("IdInventario", GetType(Integer))
        DTInventarioCiclico.Columns.Add("IdProductoBodega", GetType(Integer))
        DTInventarioCiclico.Columns.Add("Licencia", GetType(String))
        DTInventarioCiclico.Columns.Add("Cant.Reservada", GetType(Double))
        DTInventarioCiclico.Columns.Add("Contado", GetType(Boolean))
    End Sub

    '#GT17012025: tabla para el ciclico basico solo con diferencias, sin grupos ni filtros
    Private Sub SetDatataTableDiferenciaCiclico()

        DTInventarioDiferenciaCiclico.Columns.Clear()
        DTInventarioDiferenciaCiclico.Columns.Add("IdInvCiclico", GetType(Integer))
        DTInventarioDiferenciaCiclico.Columns.Add("IdInventario", GetType(Integer))
        DTInventarioDiferenciaCiclico.Columns.Add("Código", GetType(String))
        DTInventarioDiferenciaCiclico.Columns.Add("Producto", GetType(String))
        DTInventarioDiferenciaCiclico.Columns.Add("TipoProducto", GetType(String))
        DTInventarioDiferenciaCiclico.Columns.Add("Cant.Teorica.UMBas", GetType(Double))
        DTInventarioDiferenciaCiclico.Columns.Add("Cant.Conteo.UMBas", GetType(Double))
        DTInventarioDiferenciaCiclico.Columns.Add("Cant.Reconteo.UMBas", GetType(Double))
        DTInventarioDiferenciaCiclico.Columns.Add("Dif.Cant.UMBas", GetType(Double))
        DTInventarioDiferenciaCiclico.Columns.Add("NombreTipoProducto", GetType(String))
        DTInventarioDiferenciaCiclico.Columns.Add("IdProductoBodega", GetType(Integer))
        DTInventarioDiferenciaCiclico.Columns.Add("Cant.Reservada", GetType(Double))

    End Sub



    Private Sub SetDatataTableCongelado()

        DTInventarioCongelado.Columns.Clear()
        DTInventarioCongelado.Columns.Add("Código", GetType(String))
        DTInventarioCongelado.Columns.Add("Producto", GetType(String))
        DTInventarioCongelado.Columns.Add("UMBas", GetType(String))
        DTInventarioCongelado.Columns.Add("Presentación", GetType(String))
        DTInventarioCongelado.Columns.Add("Estado", GetType(String))
        DTInventarioCongelado.Columns.Add("Lote", GetType(String))
        DTInventarioCongelado.Columns.Add("Vence", GetType(Date))
        DTInventarioCongelado.Columns.Add("Cantidad", GetType(Double))
        DTInventarioCongelado.Columns.Add("Peso", GetType(Double))
        DTInventarioCongelado.Columns.Add("Ubicación", GetType(String))
        DTInventarioCongelado.Columns.Add("IdStock", GetType(Integer))
        DTInventarioCongelado.Columns.Add("TipoProducto", GetType(String))

    End Sub

    Private Sub Cargar_Datos_Encabezado()

        Try

            lblCod.Text = gBeTransInvEnc.Idinventarioenc
            Estado.Text = gBeTransInvEnc.Estado
            cmbBodega.EditValue = gBeTransInvEnc.IdBodega

            chkDobleVerifica.Checked = gBeTransInvEnc.Doble_verificacion
            cmbTipoConteo.EditValue = gBeTransInvEnc.Tipo_Conteo_Producto
            cmbProductoFamilia.EditValue = gBeTransInvEnc.IdProductoFamilia

            cmbTipoInventario.EditValue = gBeTransInvEnc.IdTipoInventario
            Fecha.EditValue = gBeTransInvEnc.Fecha
            dtpUltimoInv.EditValue = gBeTransInvEnc.Fecha_Ultimo_Inventario
            dtpHoraInicio.EditValue = gBeTransInvEnc.Hora_ini
            dtpHoraFin.EditValue = gBeTransInvEnc.Hora_fin
            chkActivo.Checked = gBeTransInvEnc.Activo
            User_agrTextEdit.Text = gBeTransInvEnc.User_agr
            Fec_agrDateEdit.Text = gBeTransInvEnc.Fec_agr
            User_modTextEdit.Text = gBeTransInvEnc.User_mod
            Fec_modDateEdit.Text = gBeTransInvEnc.Fec_mod
            chkSistema.Checked = gBeTransInvEnc.EsSistema
            chkMostrarCantidad.Checked = gBeTransInvEnc.Mostrar_Cantidad_Teorica_hh
            chkCambiaUbicacion.Checked = gBeTransInvEnc.Cambia_Ubicacion
            rgrpRegularizar.Visible = gBeTransInvEnc.IdTipoInventario = 1
            cmbCliente.EditValue = gBeTransInvEnc.IdBodegaVirtual
            chkCaptNtExist.Checked = gBeTransInvEnc.Capturar_no_existente
            chkMultiPropietario.Checked = gBeTransInvEnc.multi_propietario
            cmbCentroCosto.EditValue = gBeTransInvEnc.IdCentroCosto

            If gBeTransInvEnc.IdTipoInventario = 1 Then

                rgrpRegularizar.Enabled = Not gBeTransInvEnc.Regularizado
                rgrp.Enabled = Not gBeTransInvEnc.Regularizado

                If gBeTransInvEnc.Regularizado Then Desactiva_Menu()

            End If

            If gBeTransInvEnc.Estado.ToUpper = "ANULADO" Then Desactiva_Menu()

            If gBeTransInvEnc.Inicial = False And gBeTransInvEnc.Estado.ToUpper = "FINALIZADO" And gBeTransInvEnc.Regularizado Then Desactiva_Menu()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Desactiva_Menu()

        'Desactiva botones. 
        cmdActualizarInvInicial.Enabled = False
        mnuGuardar.Enabled = False
        mnuActualizar.Enabled = False
        mnuEliminar.Enabled = False

        'Desactiva objetos. 
        cmbBodega.Enabled = False
        cmbPropietario.Enabled = False
        cmbTipoConteo.Enabled = False
        cmbTipoInventario.Enabled = False
        chkDobleVerifica.Enabled = False
        chkActivo.Enabled = False
        Fecha.Enabled = False
        dtpHoraInicio.Enabled = False
        dtpHoraFin.Enabled = False

        cmdCompracionStock.Enabled = True

        'Desactiva para inventario cíclico
        cmdAgregar.Enabled = False
        cmdQuitar.Enabled = False
        cmdAsignarOperador.Enabled = False
        cmdQuitarOperador.Enabled = False
        cmdAgregarProducto.Enabled = False
        cmdQuitarProducto.Enabled = False
        cmdAsignarOp.Enabled = False
        cmdEliminarOpProd.Enabled = False
        grpRegularizarInvStock.Enabled = False
        rpgReconteo.Enabled = False

    End Sub

    Private Function Guardar(Optional ByVal Actualizar_Tramos As Boolean = False) As Boolean

        Guardar = False
        'Dim IdPropietario As Integer = 0

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            SplashScreenManager.Default.SetWaitFormDescription("Guardando inventario...")

            If gBeTransInvEnc.IsNew Then

                SplashScreenManager.Default.SetWaitFormDescription("Creando tarea de inventario...")

                Crea_Tarea_HH()

                'EFREN06052021 pIdPropietario no se actualiza si se selecciona otro valor en el combobox propietario
                gBeTransInvEnc.Idpropietario = pIdPropietario
                If chkMultiPropietario.Checked Then
                    pIdPropietario = 0
                Else
                    Dim fila As Object = cmbPropietario.GetSelectedDataRow
                    pIdPropietario = fila.Item("IdPropietario")
                End If

                gBeTransInvEnc.Idpropietario = pIdPropietario
                gBeTransInvEnc.IdBodega = cmbBodega.EditValue
                gBeTransInvEnc.IdTipoInventario = cmbTipoInventario.EditValue
                gBeTransInvEnc.Tipo_Conteo_Producto = cmbTipoConteo.EditValue
                gBeTransInvEnc.Fecha = Fecha.EditValue
                gBeTransInvEnc.Fecha_Ultimo_Inventario = dtpUltimoInv.EditValue
                gBeTransInvEnc.Estado = Estado.Text

                If cmbTipoInventario.Text = "Inicial" Then
                    gBeTransInvEnc.Inicial = True
                    gBeTransInvEnc.Doble_verificacion = chkDobleVerifica.Checked
                    gBeTransInvEnc.EsSistema = False
                    gBeTransInvEnc.Mostrar_Cantidad_Teorica_hh = False
                    gBeTransInvEnc.Cambia_Ubicacion = False
                Else
                    gBeTransInvEnc.Inicial = False
                    gBeTransInvEnc.Doble_verificacion = False
                    gBeTransInvEnc.EsSistema = chkSistema.Checked
                    gBeTransInvEnc.Mostrar_Cantidad_Teorica_hh = chkMostrarCantidad.Checked
                    gBeTransInvEnc.Cambia_Ubicacion = chkCambiaUbicacion.Checked
                    '#EJC20180822: Utilizado para manejar la sección en la que se registra en el diario del ERP una transacción de ajuste.
                    gBeTransInvEnc.IdProductoFamilia = cmbProductoFamilia.EditValue
                    gBeTransInvEnc.IdBodegaVirtual = cmbCliente.EditValue
                End If

                gBeTransInvEnc.Capturar_no_existente = chkCaptNtExist.Checked
                gBeTransInvEnc.Activo = chkActivo.Checked
                gBeTransInvEnc.Regularizado = False
                gBeTransInvEnc.Hora_ini = dtpHoraInicio.EditValue
                gBeTransInvEnc.Hora_fin = dtpHoraFin.EditValue
                gBeTransInvEnc.User_agr = AP.UsuarioAp.Nombres
                gBeTransInvEnc.Fec_agr = Now
                gBeTransInvEnc.User_mod = AP.UsuarioAp.Nombres
                gBeTransInvEnc.Fec_mod = Now
                'EFREN09052021 utilizado para indicar que no se validar propietario especifico. Se hace una carga para todo producto existente.
                gBeTransInvEnc.multi_propietario = chkMultiPropietario.Checked
                gBeTransInvEnc.IdCentroCosto = cmbCentroCosto.EditValue
                gBeTransInvEnc.Tipo_Asignacion = 0
                gBeTransInvEnc.Capturar_No_Asignados = chkCapturarNoAsignado.EditValue

                SplashScreenManager.Default.SetWaitFormDescription("Guardando transacción...")

                Guardar = clsLnTrans_inv_enc.Guardar(gBeTransInvEnc, BeTareaHH, prg, lblPrg)

            Else
                '#EJC20180827: Utilizado para manejar la sección en la que se registra en el diario del ERP una transacción de ajuste.
                gBeTransInvEnc.IdProductoFamilia = cmbProductoFamilia.EditValue
                gBeTransInvEnc.IdBodegaVirtual = cmbCliente.EditValue
                gBeTransInvEnc.Doble_verificacion = chkDobleVerifica.Checked
                gBeTransInvEnc.Capturar_no_existente = chkCaptNtExist.Checked
                gBeTransInvEnc.Cambia_Ubicacion = chkCambiaUbicacion.Checked
                gBeTransInvEnc.Mostrar_Cantidad_Teorica_hh = chkMostrarCantidad.Checked
                gBeTransInvEnc.IdCentroCosto = cmbCentroCosto.EditValue
                gBeTransInvEnc.Tipo_Asignacion = 0
                gBeTransInvEnc.Capturar_No_Asignados = chkCapturarNoAsignado.EditValue
                clsLnTrans_inv_enc.Guarda_Trans_Inv_Enc(gBeTransInvEnc, Nothing, Nothing)
            End If

            If Guardar OrElse Actualizar_Tramos Then
                SplashScreenManager.Default.SetWaitFormDescription("Guardando ubicaciones de conteo...")
                Guardar_Tramos()
                If Actualizar_Tramos Then Guardar = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Dim Count_IdStocks_En_Bodega As Integer

        If cmbTipoInventario.EditValue = 1 Then

            If clsLnTrans_inv_enc.Existe_Inv_Inicial(AP.IdBodega) Then
                XtraMessageBox.Show("Existe un inventario inicial no finalizado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            If AP.Bodega.Validar_Existencias_Inv_Ini Then

                Count_IdStocks_En_Bodega = clsLnStock.Get_Count_IdStock_By_IdBodega(AP.IdBodega)

                If Count_IdStocks_En_Bodega > 0 Then
                    XtraMessageBox.Show("No se puede crear tarea inventario inicial, hay existencias en la bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If

            End If

        End If

        chkActivo.Checked = True

        If Guardar() Then

            gBeTransInvEnc.IsNew = False

            SplashScreenManager.CloseForm(False)

            Modo = TipoTrans.Editar

            XtraMessageBox.Show("Se guardó el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.Close()

            'Cargar_Forma()

            If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke

        Else
            XtraMessageBox.Show("No se pudo guardar el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Guardar(True) Then

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show("Se actualizó el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke

            Close()
        Else
            XtraMessageBox.Show("No se pudo actualizar el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    'Grid Comparación
    Private Sub Cargar_Datos_Comparativos(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try

            Dim Comparacion As New clsBeTrans_inv_enc
            Dim vCantidad As Double = 0.0

            glistaInv = New List(Of clsBeTrans_inv_enc)

            ListaConteos = clsLnTrans_inv_enc.Get_All_By_Comparacion_Inventario(gBeTransInvEnc.Idinventarioenc, chkComparativoConUbicacion.Checked, lConnection, lTransaction)

            If txtPropietarioId.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.Idpropietario = txtPropietarioId.Text)
            End If

            If txtProductoId.Text <> "" Then

                ListaConteos = ListaConteos.FindAll(Function(x) x.Codigo = txtProductoId.Text)
            End If

            If txtTramoId.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.IdTramo = txtTramoId.Text)
            End If

            If txtIdUbicacionInvInicial.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.Ubicacion.IdUbicacion = txtIdUbicacionInvInicial.Text)
            End If

            For Each Ob In ListaConteos

                vCantidad = 0.0

                gBeAgregar = New clsBeTrans_inv_enc()
                gBeAgregar.Idinventarioenc = Ob.Idinventarioenc
                gBeAgregar.IdProducto = Ob.IdProducto
                gBeAgregar.IdPresentacion = Ob.IdPresentacion
                gBeAgregar.IdTramo = Ob.IdTramo
                gBeAgregar.Tramo = Ob.Tramo
                gBeAgregar.Presentacion = Ob.Presentacion
                gBeAgregar.Detalle = Ob.Detalle
                gBeAgregar.Resumen = Ob.Resumen
                gBeAgregar.EstadoDetalle = Ob.EstadoDetalle
                gBeAgregar.EstadoResumen = Ob.EstadoResumen
                gBeAgregar.OperadorConteo = Ob.OperadorConteo
                gBeAgregar.OperadorVerifica = Ob.OperadorVerifica
                gBeAgregar.Producto = Ob.Producto
                gBeAgregar.Codigo = Ob.Codigo
                gBeAgregar.UMBas = Ob.UMBas
                gBeAgregar.UbicacionCompleta = Ob.UbicacionCompleta
                'clsLnTrans_inv_enc.InsertarComparacionInventario(gBeAgregar)
                glistaInv.Add(gBeAgregar)

            Next

            llena_Grid_Comparacion()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub llena_Grid_Comparacion()

        Try

            Dim vDif As Double = 0.0

            'listaInv = clsLnTrans_inv_enc.GetAllByComparacionInventario(gBeTransInvEnc.Idinventarioenc)

            ListaConteos = glistaInv.ToList()

            If ListaConteos.Count > 0 Then

                DTC.Clear()

                For Each BeTransInvEnc In ListaConteos

                    vDif = (BeTransInvEnc.Detalle - BeTransInvEnc.Resumen)

                    If vDif < 0 Then
                        vDif = vDif * -1
                    End If

                    If chkComparativoConUbicacion.Checked Then
                        DTC.Rows.Add(BeTransInvEnc.Idinventarioenc,
                                 BeTransInvEnc.IdTramo,
                                 BeTransInvEnc.IdProducto,
                                 BeTransInvEnc.Tramo,
                                 BeTransInvEnc.Codigo,
                                 BeTransInvEnc.Producto,
                                 BeTransInvEnc.Presentacion,
                                 BeTransInvEnc.UMBas,
                                 BeTransInvEnc.Detalle,
                                 BeTransInvEnc.Resumen,
                                 vDif,
                                 BeTransInvEnc.EstadoDetalle,
                                 BeTransInvEnc.EstadoResumen,
                                 BeTransInvEnc.UbicacionCompleta)
                    Else
                        DTC.Rows.Add(BeTransInvEnc.Idinventarioenc,
                                 BeTransInvEnc.IdTramo,
                                 BeTransInvEnc.IdProducto,
                                 BeTransInvEnc.Tramo,
                                 BeTransInvEnc.Codigo,
                                 BeTransInvEnc.Producto,
                                 BeTransInvEnc.Presentacion,
                                 BeTransInvEnc.UMBas,
                                 BeTransInvEnc.Detalle,
                                 BeTransInvEnc.Resumen,
                                 vDif,
                                 BeTransInvEnc.EstadoDetalle,
                                 BeTransInvEnc.EstadoResumen)
                    End If


                Next

                dgridComparativoInvInicial.DataSource = DTC

                gviewComparativo.BestFitColumns()

                If gviewComparativo.RowCount > 0 Then
                    gviewComparativo.Columns("IdProducto").Visible = False
                    gviewComparativo.Columns("IdInventario").Visible = False

                    gviewComparativo.OptionsView.ShowFooter = True

                    gviewComparativo.Columns("Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gviewComparativo.Columns("Conteo").DisplayFormat.FormatString = "{0:n6}"

                    gviewComparativo.Columns("Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gviewComparativo.Columns("Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gviewComparativo.Columns("Verifica").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gviewComparativo.Columns("Verifica").DisplayFormat.FormatString = "{0:n6}"

                    gviewComparativo.Columns("Verifica").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gviewComparativo.Columns("Verifica").SummaryItem.DisplayFormat = "{0:n6}"

                    gviewComparativo.Columns("Diferencia").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gviewComparativo.Columns("Diferencia").DisplayFormat.FormatString = "{0:n6}"

                    gviewComparativo.Columns("Diferencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gviewComparativo.Columns("Diferencia").SummaryItem.DisplayFormat = "{0:n6}"

                End If

            Else

                dgridComparativoInvInicial.DataSource = Nothing

            End If

            Set_LayOut_Grid(vNombreArchivoLayOutGridCompara)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Datos_De_Inventario()

        Dim clsTrans As New clsTransaccion

        Try

            SplashScreenManager.CloseForm(False)
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Actualizando productos asignados")

            clsTrans.Begin_Transaction()

            Listar_Productos_Asignados(dgridAsignacionProductos, clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando verificación")
            Catch ex As Exception
            End Try

            Cargar_Verificacion_Inventario(clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando conteo")
            Catch ex As Exception
            End Try

            Cargar_Conteo_Inventario(clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando teórico WMS")
            Catch ex As Exception
            End Try

            Calcular_Inventario_Teorico_WMS(clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando teórico ERP")
            Catch ex As Exception
            End Try

            Calcular_Inventario_Teorico_ERP(clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando teórico con costos")
            Catch ex As Exception
            End Try

            Calcula_Inventario_Teorico_Costos(clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando ciclico")
            Catch ex As Exception
            End Try

            Carga_Detalle_Ciclico(clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando Diferencias")
            Catch ex As Exception
            End Try

            Carga_Detalle_Diferencias_Ciclico(clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando reconteos")
            Catch ex As Exception
            End Try

            Cargar_Reconteo(clsTrans.lConnection, clsTrans.lTransaction)

            Try
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando comparativo")
            Catch ex As Exception
            End Try

            Cargar_Datos_Comparativos(clsTrans.lConnection, clsTrans.lTransaction)

            Cargar_Conteos_Operador(clsTrans.lConnection, clsTrans.lTransaction)

            Carga_Regularizacion(clsTrans.lConnection, clsTrans.lTransaction)

            Cargar_KPI_Ubicaciones(clsTrans.lConnection, clsTrans.lTransaction)

            Me.BeginInvoke(New Action(Sub()
                                          Actualizar_Gauge_Ubicaciones(clsTrans.lConnection, clsTrans.lTransaction)
                                      End Sub))

            clsTrans.Commit_Transaction()

        Catch ex As Exception
            If Not clsTrans.lTransaction Is Nothing Then clsTrans.RollBack_Transaction()
            Throw
        Finally
            SplashScreenManager.CloseForm(False)
            clsTrans.Close_Conection()
        End Try

    End Sub

    Private Sub Cargar_Conteos_Operador(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            Dim dt As New DataTable
            dt = clsLnTrans_inv_ciclico.Get_All_Conteos_By_IdInventarioEnc_And_Operador(gBeTransInvEnc.Idinventarioenc, lConnection, lTransaction)
            dgridConteoOperador.DataSource = dt

            Set_LayOut_Grid(vNombreArchivoLayOutGridConteoOpe)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    Private Sub limpiarTodos()

        txtTramoId.Text = ""
        txtTramoNombre.Text = ""
        txtPropietarioId.Text = ""
        txtPropietarioNombre.Text = ""
        txtProductoId.Text = ""
        txtProductoNombre.Text = ""
        txtIdUbicacionInvInicial.Text = ""

    End Sub

    Private Sub cmdActualizarInvInicial_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizarInvInicial.ItemClick
        'limpiarTodos()
        Try
            Listar_Datos_De_Inventario()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If AnularInventario Then

                If XtraMessageBox.Show("¿Anular proceso de inventario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If XtraMessageBox.Show("¿Está completamente seguro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Eliminando...")

                        If (gBeTransInvEnc.Estado = "Nuevo") Then
                            gBeTransInvEnc.Activo = False
                            gBeTransInvEnc.Estado = "Anulado"

                            clsLnTrans_inv_enc.Actualizar(gBeTransInvEnc)

                            SplashScreenManager.CloseForm(False)
                            XtraMessageBox.Show("Se anuló el proceso de inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke
                            Close()

                        Else

                            If Not clsLnTrans_inv_enc.Tiene_Conteos(gBeTransInvEnc.Idinventarioenc) Then
                                gBeTransInvEnc.Activo = False
                                gBeTransInvEnc.Estado = "Anulado"
                                clsLnTrans_inv_enc.Actualizar(gBeTransInvEnc)
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("Se anuló el proceso de inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke
                                Close()
                            Else
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("El inventario tiene conteos, no se puede anular.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If

                        End If
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Dim BeTareaHH As clsBeTarea_hh

    Private Sub Crea_Tarea_HH()

        Try

            BeTareaHH = New clsBeTarea_hh

            If gBeTransInvEnc IsNot Nothing AndAlso gBeTransInvEnc.IsNew Then

                BeTareaHH.IdPropietario = pIdPropietario
                BeTareaHH.IdBodega = cmbBodega.EditValue

                BeTareaHH.IdMuelle = 0

                BeTareaHH.IdEstado = 1
                BeTareaHH.IdPrioridad = 1
                BeTareaHH.IdTipoTarea = 6
                BeTareaHH.IdTransaccion = gBeTransInvEnc.Idinventarioenc
                BeTareaHH.Tipo = 0
                BeTareaHH.FechaInicio = Fecha.EditValue
                BeTareaHH.FechaFin = Fecha.EditValue
                BeTareaHH.DiaCompleto = False
                BeTareaHH.CreaTarea = True
                BeTareaHH.IsNew = True
                BeTareaHH.Asunto = "Inventario"

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    'Grid de conteo
    Public Sub Cargar_Conteo_Inventario(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try

            ListaConteos = clsLnTrans_inv_enc.Get_All_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc, lConnection, lTransaction)

            If txtPropietarioId.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.Idpropietario = txtPropietarioId.Text)
            End If

            If txtProductoId.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.Codigo = txtProductoId.Text)
            End If

            If txtTramoId.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.IdTramo = txtTramoId.Text)
            End If

            If txtIdUbicacionInvInicial.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.Ubicacion.IdUbicacion = txtIdUbicacionInvInicial.Text)
            End If

            If ListaConteos.Count > 0 Then

                DTInventarioConteo.Clear()

                For Each BeConteoDetalle As clsBeTrans_inv_enc In ListaConteos

                    DTInventarioConteo.Rows.Add(BeConteoDetalle.Idinventarioenc,
                                                BeConteoDetalle.IdTramo,
                                                BeConteoDetalle.Tramo,
                                                BeConteoDetalle.Codigo,
                                                BeConteoDetalle.Producto,
                                                BeConteoDetalle.Presentacion,
                                                BeConteoDetalle.Detalle,
                                                BeConteoDetalle.EstadoDetalle,
                                                BeConteoDetalle.OperadorConteo,
                                                BeConteoDetalle.Fecha,
                                                BeConteoDetalle.Ubicacion.Descripcion,
                                                BeConteoDetalle.Lote,
                                                BeConteoDetalle.Licencia,
                                                BeConteoDetalle.FechaVence,
                                                BeConteoDetalle.IdProducto,
                                                BeConteoDetalle.IdInventarioDet)

                Next

                grdConteo.DataSource = DTInventarioConteo

                gviewConteo.BestFitColumns()

                If gviewConteo.RowCount > 0 Then

                    gviewConteo.Columns("IdProducto").Visible = False
                    gviewConteo.Columns("IdInventario").Visible = False
                    gviewConteo.Columns("Idinventariodet").Visible = False

                    gviewConteo.OptionsView.ShowFooter = True

                    gviewConteo.Columns("Cantidad Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gviewConteo.Columns("Cantidad Conteo").DisplayFormat.FormatString = "{0:n6}"

                    gviewConteo.Columns("Cantidad Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gviewConteo.Columns("Cantidad Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gviewConteo.Columns("Operador").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gviewConteo.Columns("Operador").DisplayFormat.FormatString = "{0:n0}"

                    gviewConteo.Columns("Operador").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    gviewConteo.Columns("Operador").SummaryItem.DisplayFormat = "{0:n0}"

                End If
            Else
                grdConteo.DataSource = Nothing
            End If

            Set_LayOut_Grid(vNombreArchivoLayOutGridIvConteo)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    'Grid de Verificación
    Private Sub Cargar_Verificacion_Inventario(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try

            ListaConteos = clsLnTrans_inv_enc.Get_All_By_ConteoEnc_Veri(gBeTransInvEnc.Idinventarioenc,
                                                                        gBeTransInvEnc.IdBodega,
                                                                        lConnection,
                                                                        lTransaction)

            If txtPropietarioId.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.Idpropietario = txtPropietarioId.Text)
            End If

            If txtProductoId.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.Codigo = txtProductoId.Text)
            End If

            If txtTramoId.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.IdTramo = txtTramoId.Text)
            End If

            If txtIdUbicacionInvInicial.Text <> "" Then
                ListaConteos = ListaConteos.FindAll(Function(x) x.Ubicacion.IdUbicacion = txtIdUbicacionInvInicial.Text)
            End If

            If ListaConteos.Count > 0 Then

                DTInventarioVerifica.Clear()

                For Each BeTrans_inv_enc As clsBeTrans_inv_enc In ListaConteos

                    DTInventarioVerifica.Rows.Add(BeTrans_inv_enc.Idinventarioenc,
                                                  BeTrans_inv_enc.IdTramo,
                                                  BeTrans_inv_enc.Tramo,
                                                  BeTrans_inv_enc.Codigo,
                                                  BeTrans_inv_enc.Producto,
                                                  BeTrans_inv_enc.Presentacion,
                                                  BeTrans_inv_enc.Resumen,
                                                  BeTrans_inv_enc.EstadoResumen,
                                                  BeTrans_inv_enc.OperadorVerifica,
                                                  BeTrans_inv_enc.Fecha,
                                                  BeTrans_inv_enc.IdProducto,
                                                  BeTrans_inv_enc.IdInventarioRes,
                                                  BeTrans_inv_enc.UbicacionCompleta,
                                                  BeTrans_inv_enc.Licencia)

                Next

                grdVerifica.DataSource = DTInventarioVerifica

                gviewVerifica.BestFitColumns()

                If gviewVerifica.RowCount > 0 Then

                    gviewVerifica.Columns("IdProducto").Visible = False
                    gviewVerifica.Columns("IdInventario").Visible = False
                    gviewVerifica.Columns("IdInventarioRes").Visible = False

                    gviewVerifica.OptionsView.ShowFooter = True

                    gviewVerifica.Columns("Cantidad Verifica").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gviewVerifica.Columns("Cantidad Verifica").DisplayFormat.FormatString = "{0:n6}"

                    gviewVerifica.Columns("Cantidad Verifica").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gviewVerifica.Columns("Cantidad Verifica").SummaryItem.DisplayFormat = "{0:n6}"

                    gviewVerifica.Columns("Operador").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gviewVerifica.Columns("Operador").DisplayFormat.FormatString = "{0:n0}"

                    gviewVerifica.Columns("Operador").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    gviewVerifica.Columns("Operador").SummaryItem.DisplayFormat = "{0:n0}"

                End If
            Else

                grdVerifica.DataSource = Nothing

            End If

            Set_LayOut_Grid(vNombreArchivoLayOutGridVerificacion)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub linkTramo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkTramo.LinkClicked

        Try

            txtPropietarioNombre.Text = ""
            txtPropietarioId.Text = ""
            txtProductoId.Text = ""
            txtProductoNombre.Text = ""

            Dim Tramo As New frmBodegaTramo_List() With {.pIdBodega = cmbBodega.EditValue, .Modo = frmBodegaTramo_List.pModo.Seleccion}
            Tramo.ShowDialog()

            If Tramo.gBeBodegaTramo IsNot Nothing AndAlso Tramo.gBeBodegaTramo.IdTramo <> 0 Then
                txtTramoId.Text = Tramo.gBeBodegaTramo.IdTramo
                txtTramoNombre.Text = Tramo.gBeBodegaTramo.Descripcion
            End If

            Tramo.Close()
            Tramo.Dispose()

            Listar_Datos_De_Inventario()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub linkProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkProducto.LinkClicked

        Try

            txtPropietarioNombre.Text = ""
            txtPropietarioId.Text = ""
            txtTramoId.Text = ""
            txtTramoNombre.Text = ""

            Dim Producto As New frmProductoList() With {.pIdPropietario = cmbPropietario.EditValue, .Modo = frmProductoList.pModo.Seleccion}
            Producto.ShowDialog()

            If Producto.pObjProducto IsNot Nothing AndAlso Producto.pObjProducto.IdProducto <> 0 Then
                txtProductoId.Text = Producto.pObjProducto.Codigo
                txtProductoNombre.Text = Producto.pObjProducto.Nombre
            End If

            Producto.Close()
            Producto.Dispose()

            Listar_Datos_De_Inventario()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub linkPropietario_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkPropietario.LinkClicked

        Try

            txtProductoId.Text = ""
            txtProductoNombre.Text = ""
            txtTramoId.Text = ""
            txtTramoNombre.Text = ""

            Dim Propietario As New frmPropietario_List() With {.pIdBodega = cmbBodega.EditValue, .Modo = frmPropietario_List.pModo.Seleccion}
            Propietario.ShowDialog()

            If Propietario.pObjPropietario IsNot Nothing AndAlso Propietario.pObjPropietario.IdPropietario <> 0 Then
                txtPropietarioId.Text = Propietario.pObjPropietario.IdPropietario
                txtPropietarioNombre.Text = Propietario.pObjPropietario.Nombre_comercial
            End If

            Propietario.Close()
            Propietario.Dispose()

            Listar_Datos_De_Inventario()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub grdConteo_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdConteo.ViewRegistered

        Dim gridView As GridView = e.View
        gridView.OptionsView.ColumnAutoWidth = False
        gridView.BestFitColumns()

        If gridView.IsDetailView Then
            gridView.Columns("IdInventarioEnc").Visible = False
            gridView.Columns("IdTramo").Visible = False
        End If

    End Sub

    Private Sub grdVerifica_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdVerifica.ViewRegistered

        Dim gridView As GridView = e.View
        gridView.OptionsView.ColumnAutoWidth = False
        gridView.BestFitColumns()

        If gridView.IsDetailView Then
            gridView.Columns("IdInventarioEnc").Visible = False
            gridView.Columns("IdTramo").Visible = False
        End If

    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        limpiarTodos()
        Listar_Datos_De_Inventario()
    End Sub

    Private Sub Listar_Tramos()

        Try

            If gBeTransInvEnc.IsNew Then
                chkTramosAsig.Checked = False
            End If

            If chkTramosAsig.Checked Then

                dgridAsignacionTramos.DataSource = gBeBodegaTramosAsignados

            Else

                For Each OBj As clsBeBodega_Tramo_Seleccion In gBeBodegaTramos
                    If gBeBodegaTramosAsignados.Count > 0 Then
                        For Each ObjN In gBeBodegaTramosAsignados
                            If OBj.IdTramo = ObjN.IdTramo Then
                                OBj.Seleccionar = True
                            End If
                        Next
                    End If
                Next

                dgridAsignacionTramos.DataSource = gBeBodegaTramos

            End If

            GridViewTramos.LayoutChanged()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridViewTramos.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritemPropietario_CheckedChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ritemPropietario_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)
            Dim Dr As clsBeBodega_Tramo_Seleccion = GridViewTramos.GetFocusedRow

            If Not ritem Is Nothing Then

                If Not Dr Is Nothing Then

                    If chkTramosAsig.Checked Then

                        If ritem.Checked Then
                            gBeBodegaTramosAsignados.Where(Function(x) x.IdTramo = Dr.IdTramo).FirstOrDefault.Seleccionar = True
                        Else
                            gBeBodegaTramosAsignados.Where(Function(x) x.IdTramo = Dr.IdTramo).FirstOrDefault.Seleccionar = False
                        End If

                    Else
                        If ritem.Checked Then
                            gBeBodegaTramos.Where(Function(x) x.IdTramo = Dr.IdTramo).FirstOrDefault.Seleccionar = True
                        Else
                            gBeBodegaTramos.Where(Function(x) x.IdTramo = Dr.IdTramo).FirstOrDefault.Seleccionar = False
                        End If
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Guardar_Tramos()

        Try

            Dim TramosGet As New clsBeTrans_inv_tramo

            If chkTramosAsig.Checked Then

                For Each Tramo As clsBeBodega_Tramo_Seleccion In gBeBodegaTramosAsignados

                    If gBeTransInvEnc.IsNew Then
                        gBeTransTramoInv.Idinventario = lblCod.Text
                    Else
                        gBeTransTramoInv.Idinventario = gBeTransInvEnc.Idinventarioenc
                    End If

                    gBeTransTramoInv.Idtramo = Tramo.IdTramo
                    gBeTransTramoInv.Det_idoperador = 0
                    gBeTransTramoInv.Det_estado = "Nuevo"
                    gBeTransTramoInv.Det_inicio = Now
                    gBeTransTramoInv.Det_fin = Now
                    gBeTransTramoInv.Res_idoperador = 0
                    gBeTransTramoInv.Res_estado = "Nuevo"
                    gBeTransTramoInv.Res_inicio = Now
                    gBeTransTramoInv.Res_fin = Now
                    gBeTransTramoInv.Aplicado = False
                    gBeTransTramoInv.IdBodega = AP.IdBodega

                    TramosGet = clsLnTrans_inv_tramo.GetSingle(Tramo.IdTramo, gBeTransInvEnc.Idinventarioenc)

                    If Tramo.Seleccionar Then

                        If TramosGet IsNot Nothing Then

                            If TramosGet.Res_estado.ToUpper = "NUEVO" And TramosGet.Det_estado.ToUpper = "NUEVO" Then
                                clsLnTrans_inv_tramo.Actualizar(gBeTransTramoInv)
                            End If

                        Else
                            clsLnTrans_inv_tramo.Insertar(gBeTransTramoInv)
                        End If

                    Else

                        If TramosGet IsNot Nothing Then

                            If TramosGet.Res_estado.ToUpper = "NUEVO" And TramosGet.Det_estado.ToUpper = "NUEVO" Then
                                clsLnTrans_inv_tramo.Eliminar(gBeTransTramoInv)
                            End If

                        End If

                    End If

                Next

            Else

                SplashScreenManager.Default.SetWaitFormDescription("Procesando tramos...")

                For Each Tramo As clsBeBodega_Tramo_Seleccion In gBeBodegaTramos

                    If gBeTransInvEnc.IsNew Then
                        gBeTransTramoInv.Idinventario = lblCod.Text
                    Else
                        gBeTransTramoInv.Idinventario = gBeTransInvEnc.Idinventarioenc
                    End If

                    gBeTransTramoInv.Idtramo = Tramo.IdTramo
                    gBeTransTramoInv.Det_idoperador = 0
                    gBeTransTramoInv.Det_estado = "Nuevo"
                    gBeTransTramoInv.Det_inicio = Now
                    gBeTransTramoInv.Det_fin = Now
                    gBeTransTramoInv.Res_idoperador = 0
                    gBeTransTramoInv.Res_estado = "Nuevo"
                    gBeTransTramoInv.Res_inicio = Now
                    gBeTransTramoInv.Res_fin = Now
                    gBeTransTramoInv.Aplicado = False
                    gBeTransTramoInv.IdBodega = AP.IdBodega

                    TramosGet = clsLnTrans_inv_tramo.GetSingle(Tramo.IdTramo, gBeTransInvEnc.Idinventarioenc)

                    If Tramo.Seleccionar Then

                        If TramosGet IsNot Nothing Then

                            If TramosGet.Res_estado.ToUpper = "NUEVO" And TramosGet.Det_estado.ToUpper = "NUEVO" Then
                                clsLnTrans_inv_tramo.Actualizar(gBeTransTramoInv)
                            End If

                        Else
                            clsLnTrans_inv_tramo.Insertar(gBeTransTramoInv)
                        End If

                    Else

                        If TramosGet IsNot Nothing Then

                            If TramosGet.Res_estado.ToUpper = "NUEVO" And TramosGet.Det_estado.ToUpper = "NUEVO" Then
                                clsLnTrans_inv_tramo.Eliminar(gBeTransTramoInv)
                            End If

                        End If

                    End If

                Next

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub chkTramosAsig_CheckedChanged(sender As Object, e As EventArgs) Handles chkTramosAsig.CheckedChanged

        Try

            If chkTramosAsig.Checked Then

                Listar_Tramos()

            Else

                chkSeleccionarTodos.Visible = True
                Listar_Tramos()

            End If

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridViewTramos.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritemPropietario_CheckedChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtTramoId_KeyPress(sender As Object, e As KeyPressEventArgs)

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtTramoId.Text.Length = 1 Then
                txtTramoNombre.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtTramoId_Validated(sender As Object, e As EventArgs)

        Try

            If String.IsNullOrEmpty(txtTramoId.Text.Trim()) = False AndAlso txtTramoId.Text > "0" Then

                Dim Obj As New clsBeBodega_tramo
                Obj = clsLnBodega_tramo.GetSingle(txtTramoId.Text, AP.IdBodega)

                If Obj IsNot Nothing AndAlso Obj.IdTramo > 0 Then
                    txtTramoNombre.Text = Trim(String.Format("{0}", Obj.Descripcion))
                    Listar_Datos_De_Inventario()
                Else

                    XtraMessageBox.Show(String.Format("No existe tramo con código {0}", txtTramoId.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtTramoId.Focus()
                    txtTramoId.SelectAll()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtProductoId_KeyPress(sender As Object, e As KeyPressEventArgs)

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtProductoId.Text.Length = 1 Then
                txtProductoNombre.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtProductoId_Validated(sender As Object, e As EventArgs)

        Try

            If String.IsNullOrEmpty(txtProductoId.Text.Trim()) = False AndAlso txtProductoId.Text > "0" Then

                Dim Obj As New clsBeProducto
                Obj = clsLnProducto.Get_Single_By_IdProducto_And_IdPropietario(txtProductoId.Text, cmbPropietario.EditValue)

                If Obj IsNot Nothing AndAlso Obj.IdProducto > 0 Then
                    txtProductoNombre.Text = Trim(String.Format("{0}", Obj.Nombre))
                    Listar_Datos_De_Inventario()
                Else

                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtProductoId.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtProductoId.Focus()
                    txtProductoId.SelectAll()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtPropietarioId_KeyPress(sender As Object, e As KeyPressEventArgs)

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtPropietarioId.Text.Length = 1 Then
                txtPropietarioNombre.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtPropietarioId_Validated(sender As Object, e As EventArgs)

        Try

            If String.IsNullOrEmpty(txtPropietarioId.Text.Trim()) = False AndAlso txtPropietarioId.Text > "0" Then

                Dim Obj As New clsBePropietarios
                Obj = clsLnPropietarios.Get_Single_By_IdEmpresa(AP.IdEmpresa, txtPropietarioId.Text)

                If Obj IsNot Nothing AndAlso Obj.IdPropietario > 0 Then
                    txtPropietarioNombre.Text = Trim(String.Format("{0}", Obj.Nombre_comercial))
                    Listar_Datos_De_Inventario()
                Else

                    XtraMessageBox.Show(String.Format("No existe propietario con código {0}", txtPropietarioId.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtPropietarioId.Focus()
                    txtPropietarioId.SelectAll()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkSeleccionarTodos_CheckedChanged(sender As Object, e As EventArgs) Handles chkSeleccionarTodos.CheckedChanged

        Try

            Dim Prod As clsBeBodega_Tramo_Seleccion
            Dim vMarcado As Boolean = False

            For Each Prod In dgridAsignacionTramos.DataSource

                vMarcado = Prod.Seleccionar = False

                If vMarcado Then
                    gBeBodegaTramos.Find(Function(x) x.IdTramo = Prod.IdTramo).Seleccionar = True
                Else
                    gBeBodegaTramos.Find(Function(x) x.IdTramo = Prod.IdTramo).Seleccionar = False
                End If

            Next

            Listar_Tramos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub grdConteo_DoubleClick(sender As Object, e As EventArgs) Handles grdConteo.DoubleClick

        Try

            If (gviewConteo.RowCount > 0) Then

                Dim Dr As DataRowView = gviewConteo.GetFocusedRow
                Dim Obj As New clsBeTrans_inv_detalle

                If Not Dr Is Nothing Then

                    Obj = clsLnTrans_inv_detalle.Get_Single_By_IdInvDet(Dr.Item("Idinventariodet"))
                    Dim lSelectionIndex As Integer = gviewConteo.FocusedRowHandle

                    If Modo = TipoTrans.Editar Then
                        Dim NavEnt As New frmInventarioConteo(frmInventarioConteo.TipoTrans.Editar) With {.gBeTransInvConteo = Obj}
                        NavEnt.ShowDialog()
                        NavEnt.Dispose()
                        Listar_Datos_De_Inventario()
                        gviewConteo.FocusedRowHandle = lSelectionIndex
                    ElseIf Modo = TipoTrans.Nuevo Then
                        Hide()
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub rgrp_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles rgrp.ItemClick

        Try

            Dim InvImp As New frmInventarioImport
            InvImp.IdInventario = gBeTransInvEnc.Idinventarioenc
            InvImp.DobleVerificacion = gBeTransInvEnc.Doble_verificacion
            InvImp.InvTeorico_Multi_Propietario = gBeTransInvEnc.multi_propietario
            InvImp.TipoInventario = cmbTipoInventario.Properties.GetDisplayText(cmbTipoInventario.EditValue)
            InvImp.TipoTeoricoImportacion = frmInventarioImport.pTipoImportacion.WMS

            If gBeTransInvEnc.multi_propietario Then
                InvImp.IdPropietarioBodega = 0
            Else
                InvImp.IdPropietarioBodega = cmbPropietario.EditValue
            End If

            InvImp.ShowDialog()
            InvImp.Dispose()

            Listar_Datos_De_Inventario()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private DTComparativoWMSVrsFisico As New DataTable
    Private DTComparativoWMSVrsERP As New DataTable
    Private gCoincidencias As Boolean = False

    Private Sub Calcular_Inventario_Teorico_WMS(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try


            If gBeTransInvEnc.Inicial Then

                If AP.Bodega.Interface_SAP AndAlso AP.Bodega.Restringir_Areas_SAP Then
                    gCoincidencias = True
                End If

                DTComparativoWMSVrsFisico = clsLnTrans_inv_enc.Get_Inventario_Vrs_Stock_Det_WMS(gBeTransInvEnc.Idinventarioenc,
                                                                                             gBeTransInvEnc.IdBodega,
                                                                                             gCoincidencias,
                                                                                             lConnection,
                                                                                             lTransaction)
            Else
                DTComparativoWMSVrsFisico = clsLnTrans_inv_enc.Get_Inventario_Vrs_Stock_Det_Teorico_WMS(gBeTransInvEnc.Idinventarioenc,
                                                                                                     lConnection,
                                                                                                     lTransaction)
            End If

            dgridInvTeorico.DataSource = DTComparativoWMSVrsFisico

            If gvInvTeoricoWMS.Columns.Count > 0 Then

                If Not gBeTransInvEnc.Inicial Then

                    gvInvTeoricoWMS.Columns("Tipo").Group()

                    gvInvTeoricoWMS.Columns("Codigo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    gvInvTeoricoWMS.Columns("Codigo").SummaryItem.DisplayFormat = "Registros: {0}"

                    gvInvTeoricoWMS.Columns("Stock_WMS").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoWMS.Columns("Stock_WMS").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Stock_WMS").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoWMS.Columns("Stock_WMS").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Teorico_ERP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoWMS.Columns("Teorico_ERP").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Teorico_ERP").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoWMS.Columns("Teorico_ERP").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Dif_ERP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoWMS.Columns("Dif_ERP").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Dif_ERP").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoWMS.Columns("Dif_ERP").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Dif_Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoWMS.Columns("Dif_Conteo").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Dif_Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoWMS.Columns("Dif_Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoWMS.Columns("Conteo").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoWMS.Columns("Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                Else

                    gvInvTeoricoWMS.Columns("Inv").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoWMS.Columns("Inv").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Inv").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoWMS.Columns("Inv").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Stock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoWMS.Columns("Stock").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Stock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoWMS.Columns("Stock").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Dif").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoWMS.Columns("Dif").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoWMS.Columns("Dif").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoWMS.Columns("Dif").SummaryItem.DisplayFormat = "{0:n6}"

                End If

                gvInvTeoricoWMS.OptionsView.ShowFooter = True

                gvInvTeoricoWMS.ExpandAllGroups()

                gvInvTeoricoWMS.BestFitColumns(True)

                lblRegs.Caption = String.Format("Registros: {0}", gviewComparativo.RowCount)

                If Not gBeTransInvEnc.Inicial Then

                    gvInvTeoricoWMS.Columns("Codigo").Width = 100

                    Dim gridFormatRule As New GridFormatRule()
                    Dim formatConditionRuleExpression As New FormatConditionRuleExpression()
                    gridFormatRule.Column = gvInvTeoricoWMS.Columns("Dif_Conteo")
                    gridFormatRule.ApplyToRow = False
                    formatConditionRuleExpression.PredefinedName = "Red Fill, Red Text"
                    formatConditionRuleExpression.Expression = "[Dif_Conteo] <> 0"
                    gridFormatRule.Rule = formatConditionRuleExpression
                    gvInvTeoricoWMS.FormatRules.Add(gridFormatRule)

                    Dim gridFormatRule1 As New GridFormatRule()
                    Dim formatConditionRuleExpression1 As New FormatConditionRuleExpression()
                    gridFormatRule1.Column = gvInvTeoricoWMS.Columns("Dif_ERP")
                    gridFormatRule1.ApplyToRow = False
                    formatConditionRuleExpression1.PredefinedName = "Red Fill, Red Text"
                    formatConditionRuleExpression1.Expression = "[Dif_ERP] <> 0"
                    gridFormatRule1.Rule = formatConditionRuleExpression1
                    gvInvTeoricoWMS.FormatRules.Add(gridFormatRule1)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Calcula_Inventario_Teorico_Costos(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try

            Dim DT As New DataTable

            DT = clsLnTrans_inv_enc.Get_Teorico_Conteo_Costos(gBeTransInvEnc.Idinventarioenc, lConnection, lTransaction)

            grdCostos.DataSource = DT

            If GridView7.Columns.Count > 0 Then

                If Not gBeTransInvEnc.Inicial Then

                    GridView7.Columns("Tipo").Group()

                    GridView7.Columns("Codigo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    GridView7.Columns("Codigo").SummaryItem.DisplayFormat = "Registros: {0}"

                    GridView7.Columns("Stock_WMS").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Stock_WMS").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Stock_WMS").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Stock_WMS").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Teorico_ERP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Teorico_ERP").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Teorico_ERP").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Teorico_ERP").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Dif_ERP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Dif_ERP").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Dif_ERP").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Dif_ERP").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Dif_Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Dif_Conteo").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Dif_Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Dif_Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Conteo").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Costo_Nav").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Costo_Nav").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Costo_Nav").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Costo_Nav").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Costo_Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Costo_Conteo").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Costo_Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Costo_Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Dif_Costo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Dif_Costo").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Dif_Costo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Dif_Costo").SummaryItem.DisplayFormat = "{0:n6}"

                Else

                    GridView7.Columns("Inv").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Inv").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Inv").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Inv").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Stock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Stock").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Stock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Stock").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView7.Columns("Dif").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView7.Columns("Dif").DisplayFormat.FormatString = "{0:n6}"

                    GridView7.Columns("Dif").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView7.Columns("Dif").SummaryItem.DisplayFormat = "{0:n6}"

                End If

                GridView7.OptionsView.ShowFooter = True

                GridView7.ExpandAllGroups()

                GridView7.BestFitColumns(True)

                GridView7.Columns("Codigo").Width = 100

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Reporte_Inventario_Teorico_Costos()

        Try

            Dim Rep As New rptComparativoConCostos
            Rep.DataSource = clsLnTrans_inv_enc.Get_Teorico_Conteo_Costos(gBeTransInvEnc.Idinventarioenc)
            Rep.DataMember = "Result"
            Rep.RequestParameters = False
            Rep.ShowPreviewDialog()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al generar documento de inventario: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub grdVerifica_DoubleClick(sender As Object, e As EventArgs) Handles grdVerifica.DoubleClick

        Try

            If (gviewVerifica.RowCount > 0) Then

                Dim Dr As DataRowView = gviewVerifica.GetFocusedRow
                Dim Obj As New clsBeTrans_inv_resumen

                If Not Dr Is Nothing Then

                    Obj = clsLnTrans_inv_resumen.GetSingleByInvRes(Dr.Item("IdInventarioRes"))
                    Dim lSelectionIndex As Integer = gviewVerifica.FocusedRowHandle

                    If Modo = TipoTrans.Editar Then
                        Dim NavEnt As New frmInventarioVerifica(frmInventarioVerifica.TipoTrans.Editar) With {.gBeTransInvVer = Obj}
                        NavEnt.ShowDialog()
                        NavEnt.Dispose()
                        Listar_Datos_De_Inventario()
                        gviewVerifica.FocusedRowHandle = lSelectionIndex
                    ElseIf Modo = TipoTrans.Nuevo Then
                        Hide()
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub GridView2_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gviewConteo.RowStyle

        Try

            gviewConteo.OptionsBehavior.Editable = False
            gviewConteo.OptionsSelection.EnableAppearanceFocusedCell = False

            gviewConteo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            gviewConteo.OptionsSelection.EnableAppearanceFocusedRow = True
            gviewConteo.OptionsSelection.EnableAppearanceHideSelection = True
            gviewConteo.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gviewConteo.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            gviewConteo.Appearance.FocusedRow.ForeColor = Color.White
            gviewConteo.Appearance.SelectedRow.ForeColor = Color.White

            gviewConteo.Appearance.SelectedRow.Options.UseBackColor = True
            gviewConteo.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView3_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gviewVerifica.RowStyle

        Try

            gviewVerifica.OptionsBehavior.Editable = False
            gviewVerifica.OptionsSelection.EnableAppearanceFocusedCell = False

            gviewVerifica.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            gviewVerifica.OptionsSelection.EnableAppearanceFocusedRow = True
            gviewVerifica.OptionsSelection.EnableAppearanceHideSelection = True
            gviewVerifica.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gviewVerifica.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            gviewVerifica.Appearance.FocusedRow.ForeColor = Color.White
            gviewVerifica.Appearance.SelectedRow.ForeColor = Color.White

            gviewVerifica.Appearance.SelectedRow.Options.UseBackColor = True
            gviewVerifica.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView6_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView6.RowStyle

        Try

            GridView6.OptionsBehavior.Editable = False
            GridView6.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridView6.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView6.OptionsSelection.EnableAppearanceHideSelection = True
            GridView6.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView6.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView6.Appearance.FocusedRow.ForeColor = Color.White
            GridView6.Appearance.SelectedRow.ForeColor = Color.White

            GridView6.Appearance.SelectedRow.Options.UseBackColor = True
            GridView6.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gviewComparativo.RowCellStyle

        Try

            Dim View As GridView = sender
            Dim CantidadCont As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Conteo"))
            Dim CantidadVer As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Verifica"))
            Dim Diferencia As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Diferencia"))

            'If e.Column.FieldName = "Verifica" Then

            '    If CantidadCont <> CantidadVer Then
            '        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            '        e.Appearance.ForeColor = Color.Black
            '        e.Appearance.BackColor = Color.Salmon
            '        e.Appearance.BackColor2 = Color.SeaShell
            '    ElseIf CantidadCont = CantidadVer Then
            '        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
            '        e.Appearance.ForeColor = Color.Black
            '        e.Appearance.BackColor = Color.Green
            '        e.Appearance.BackColor2 = Color.White
            '    End If

            'End If

            If e.Column.FieldName = "Diferencia" Then

                If Val(Diferencia) > 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf Val(Diferencia) = 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdCompracionStock_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCompracionStock.ItemClick

        Try
            If Modo = TipoTrans.Editar Then

                Dim CompraStock As New frmComparacionInventarioStock(frmComparacionInventarioStock.TipoTrans.Editar) With {.BeInventarioEnc = gBeTransInvEnc}
                CompraStock.ShowDialog()
                CompraStock.Dispose()

            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdConvertir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdConvertir.ItemClick
        Regularizar_Inventario()
    End Sub

    Private lPresentaciones As New List(Of clsBeProducto_Presentacion)

    Private Sub Regularizar_Inventario()

        Dim stock As New List(Of clsBeTrans_inv_detalle)
        Dim items As New List(Of clsBeStock)
        Dim movs As New List(Of clsBeTrans_movimientos)
        Dim item As New clsBeStock
        Dim prod As New clsBeProducto
        Dim mov As New clsBeTrans_movimientos
        Dim BePresentacion As New clsBeProducto_Presentacion
        Dim IdxPres As Integer = 0

        If XtraMessageBox.Show("¿Iniciar proceso de regularizacion?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
        If XtraMessageBox.Show("¡Este proceso no se puede revertir !" & vbCrLf & "¿Está seguro de continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Obteniendo inventario")

            stock = clsLnTrans_inv_detalle.Get_All_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc)

            For Each st As clsBeTrans_inv_detalle In stock

                item = New clsBeStock

                SplashScreenManager.Default.SetWaitFormDescription("Procesando producto: " & st.Idproducto)

                With item

                    'EFREN11052021 si es multipropietario el propietario bodega del encabezado es 0, por eso se obtiene del detalle donde si existe.
                    If gBeTransInvEnc.multi_propietario Then
                        .IdPropietarioBodega = st.IdPropietarioBodega
                    Else
                        .IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(AP.IdBodega, gBeTransInvEnc.Idpropietario)
                    End If

                    .IdStock = 0
                    .IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(st.Idproducto, AP.IdBodega)
                    .IdProductoEstado = st.Idproductoestado
                    .ProductoEstado.IdEstado = st.Idproductoestado
                    .IdPresentacion = st.IdPresentacion
                    .Presentacion.IdPresentacion = st.IdPresentacion
                    .IdUnidadMedida = st.Idunidadmedida
                    .IdUbicacion = st.IdUbicacion
                    .IdUbicacion_anterior = st.IdUbicacion
                    .IdRecepcionEnc = 0
                    .IdRecepcionDet = 0
                    .IdPedidoEnc = 0
                    .IdPickingEnc = 0
                    .IdDespachoEnc = 0
                    .Lote = st.Lote
                    .Lic_plate = st.License_plate
                    .Serial = ""
                    .Cantidad = st.Cantidad
                    .Fecha_Ingreso = st.Fecha_captura '#CKFK 20180602 Modifiqué el valor que se guarda en fecha de ingreso, porque estaba Date.Now y debe ser st.Fecha_captura
                    .Fecha_vence = st.Fecha_vence '#CKFK 20180602 Modifiqué el valor que se guarda en fecha vence, porque estaba Date.Now y debe ser st.Fecha_vence
                    .Uds_lic_plate = 0
                    .No_bulto = 0 '#CKFK 20180405 Se modificó el valor por defecto del campo No_Bulto por 0
                    .Fecha_Manufactura = Date.Now
                    .Añada = 0
                    .User_agr = AP.UsuarioAp.IdUsuario
                    .Fec_agr = Date.Now
                    .User_mod = AP.UsuarioAp.IdUsuario
                    .Fec_mod = Date.Now
                    .Activo = True '#CKFK 20180625 10:13 AM Se modificó el Activo = False a Activo = True porque no hay razón para la cual esté en False
                    .Peso = st.Peso
                    .Temperatura = 0
                    .Atributo_Variante_1 = st.Codigo_variante

                End With

                If st.IdPresentacion <> 0 Then

                    IdxPres = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = st.IdPresentacion)

                    If IdxPres = -1 Then
                        BePresentacion = New clsBeProducto_Presentacion
                        BePresentacion.IdPresentacion = st.IdPresentacion
                        clsLnProducto_presentacion.GetSingle(BePresentacion)
                        lPresentaciones.Add(BePresentacion)
                    Else
                        BePresentacion = lPresentaciones(IdxPres)
                    End If

                    item.Cantidad = item.Cantidad * BePresentacion.Factor

                End If

                items.Add(item)

                mov = New clsBeTrans_movimientos

                mov.IdMovimiento = 0
                mov.IdEmpresa = AP.IdEmpresa
                mov.IdBodegaOrigen = AP.IdBodega
                mov.IdTransaccion = gBeTransInvEnc.Idinventarioenc
                mov.IdPropietarioBodega = item.IdPropietarioBodega
                mov.IdProductoBodega = item.IdProductoBodega
                mov.IdUbicacionOrigen = item.IdUbicacion
                mov.IdUbicacionDestino = item.IdUbicacion
                mov.IdPresentacion = item.IdPresentacion
                mov.IdEstadoOrigen = item.IdProductoEstado
                mov.IdEstadoDestino = item.IdProductoEstado
                mov.IdUnidadMedida = item.IdUnidadMedida
                mov.IdTipoTarea = 6
                mov.IdBodegaDestino = AP.IdBodega
                mov.IdRecepcion = 0
                mov.IdRecepcionDet = 0
                mov.Serie = ""
                mov.Lote = item.Lote
                mov.Fecha_vence = item.Fecha_vence
                mov.Fecha = Now
                mov.Barra_pallet = st.License_plate
                mov.Hora_ini = Now
                mov.Hora_fin = Now
                mov.Fecha_agr = Now
                mov.Usuario_agr = AP.UsuarioAp.IdUsuario
                mov.Cantidad = item.Cantidad
                mov.Cantidad_hist = 0
                mov.Peso = item.Peso
                mov.Peso_hist = 0

                movs.Add(mov)

                Application.DoEvents()

            Next

            SplashScreenManager.Default.SetWaitFormDescription("Insertando inventario...")

            clsLnStock.Importar_Inventario(gBeTransInvEnc, items, movs)

            SplashScreenManager.Default.SetWaitFormDescription("Finalizando...")

            rgrpRegularizar.Visible = False
            rgprImportar.Visible = False

            Desactiva_Menu()

            XtraMessageBox.Show("Regularización completa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            SplashScreenManager.CloseForm(False)

            DialogResult = DialogResult.OK

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmInventario_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            rpgAplicarAjustesFecha.Visible = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.O Then
            gCoincidencias = Not gCoincidencias
        End If

    End Sub

    Private Sub cmbTipoInventario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTipoInventario.EditValueChanged

        Try

            If cmbTipoInventario.Text <> "Inicial" Then
                chkDobleVerifica.Visible = False
                lblDobleVerif.Visible = False
                lblEsSistema.Visible = True
                chkSistema.Visible = True
                lblMostrarCantidad.Visible = True
                chkMostrarCantidad.Visible = True
                lblCambiaUbicacion.Visible = True
                chkCambiaUbicacion.Visible = True
                lblCliente.Visible = True
                cmbCliente.Visible = True
                lblSeccionAjuste.Visible = True
                cmbProductoFamilia.Visible = True
                Label2.Visible = True
                chkCaptNtExist.Visible = True
                Label4.Visible = True
                chkCapturarNoAsignado.Visible = True
            Else
                chkDobleVerifica.Visible = True
                lblDobleVerif.Visible = True
                lblMostrarCantidad.Visible = True
                chkMostrarCantidad.Visible = True
                lblCambiaUbicacion.Visible = False
                chkCambiaUbicacion.Visible = False
                lblCliente.Visible = False
                cmbCliente.Visible = False
                lblSeccionAjuste.Visible = False
                cmbProductoFamilia.Visible = False
                Label2.Visible = False
                chkCaptNtExist.Visible = False
                Label4.Visible = False
                chkCapturarNoAsignado.Visible = False
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

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click

        Dim clsTrans As New clsTransaccion

        Try

            clsTrans.Begin_Transaction()

            Dim Ubicacion As New frmSeleccionUbicacionesInv()
            Ubicacion.pObjBeB.IdBodega = cmbBodega.EditValue
            Ubicacion.pObjBeB.Nombre = cmbBodega.Text
            Ubicacion.IdInventarioEnc = lblCod.Text
            Ubicacion.IdOperador = cmbOperador.EditValue
            Ubicacion.lStockCongelado = clsLnTrans_inv_stock.Get_All_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc, gBeTransInvEnc.IdBodega, clsTrans.lConnection, clsTrans.lTransaction)
            Ubicacion.ShowDialog()
            Ubicacion.Dispose()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Ubicaciones...")

            Listar_Bodega()
            Listar_Productos()
            actualizaEncabezado()

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub actualizaEncabezado()

        Try

            If gBeTransInvEnc.Estado.ToUpper = "NUEVO" Then
                gBeTransInvEnc.Estado = "En Proceso"
                clsLnTrans_inv_enc.Actualizar(gBeTransInvEnc)
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Function EliminarUbicaciones() As Boolean

        Dim BeTransInvCiclicoUbic As New clsBeTrans_inv_ciclico_ubic
        Dim lBeTransInvCiclico As New List(Of clsBeTrans_inv_ciclico)
        Dim vUbicacion As Integer = 0
        Dim vIdOperador As Integer = 0
        Dim vIdProductoBodega As Integer = 0
        Dim clsTrans As New clsTransaccion

        EliminarUbicaciones = False

        Try

            For Each NA As TreeListNode In dgridAsignacionOperadores.Nodes

                For Each NS As TreeListNode In NA.Nodes

                    For Each NT As TreeListNode In NS.Nodes

                        For Each NU As TreeListNode In NT.Nodes

                            If NU.Checked Then

                                '#CKFK20240823 Esto es lo que estoy cambiando
                                BeTransInvCiclicoUbic.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                vUbicacion = NU.Tag
                                vIdOperador = NU.Item(4)
                                'clsLnTrans_inv_ciclico_ubic.Eliminar(BeTransInvCiclicoUbic)

                                If Not vIdOperador = 0 Then
                                    lBeTransInvCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdUbicacion_And_IdOperador(gBeTransInvEnc.Idinventarioenc,
                                                                                                                                    vIdOperador,
                                                                                                                                    vUbicacion)
                                Else
                                    lBeTransInvCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdUbicacion(gBeTransInvEnc.Idinventarioenc,
                                                                                                                         vIdOperador,
                                                                                                                         vUbicacion)
                                End If

                                Try

                                    clsTrans.Begin_Transaction()

                                    If lBeTransInvCiclico.Count > 0 Then

                                        For Each InvCiclico As clsBeTrans_inv_ciclico In lBeTransInvCiclico

                                            vIdProductoBodega = InvCiclico.IdProductoBodega

                                            If clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador(InvCiclico.Idoperador,
                                                                                                gBeTransInvEnc.Idinventarioenc,
                                                                                                vIdProductoBodega,
                                                                                                clsTrans.lConnection,
                                                                                                clsTrans.lTransaction) Then

                                                clsLnTrans_inv_ciclico_ubic.Elimina_Ubicaciones_By_IdUbicacion_And_IdOperador(gBeTransInvEnc.Idinventarioenc,
                                                                                                                          vIdProductoBodega,
                                                                                                                          InvCiclico.Idoperador,
                                                                                                                          clsTrans.lConnection,
                                                                                                                          clsTrans.lTransaction)

                                                clsLnTrans_inv_operador.Eliminar_IdUbicacion_By_IdUbicacion_And_IdOperador_And_IdProductoBodega(gBeTransInvEnc.Idinventarioenc,
                                                                                                                                        InvCiclico.Idoperador,
                                                                                                                                        vIdProductoBodega,
                                                                                                                                        vUbicacion,
                                                                                                                                        clsTrans.lConnection,
                                                                                                                                        clsTrans.lTransaction)

                                                Dim vTransInvCiclico As New clsBeTrans_inv_ciclico
                                                vTransInvCiclico.IdUbicacion = vUbicacion
                                                vTransInvCiclico.IdProductoBodega = vIdProductoBodega
                                                vTransInvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                                vTransInvCiclico.Idoperador = InvCiclico.Idoperador

                                                clsLnTrans_inv_ciclico.Eliminar_By_IdOperador_And_IdProductoBodega_And_IdUbicacion(vTransInvCiclico,
                                                                                                                               clsTrans.lConnection,
                                                                                                                               clsTrans.lTransaction)

                                            End If


                                        Next

                                    Else

                                        clsLnTrans_inv_ciclico_ubic.Elimina_By_IdUbicacion(BeTransInvCiclicoUbic.Idinventarioenc,
                                                                                           vUbicacion,
                                                                                           clsTrans.lConnection,
                                                                                           clsTrans.lTransaction)

                                    End If

                                    clsTrans.Commit_Transaction()

                                    EliminarUbicaciones = True

                                Catch ex As Exception
                                    clsTrans.RollBack_Transaction()
                                Finally
                                    clsTrans.Close_Conection()
                                End Try
                            End If

                        Next

                    Next

                Next

            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub cmdQuitar_Click(sender As Object, e As EventArgs) Handles cmdQuitar.Click

        Try

            If XtraMessageBox.Show("¿Eliminar Ubicaciones?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Not Comprueba_Grid() Then

                    XtraMessageBox.Show("No es posible eliminar ubicaciones", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Else

                    If EliminarUbicaciones() Then

                        XtraMessageBox.Show("Ubicaciones eliminadas", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Listar_Bodega() : Listar_Productos()

                    Else

                        XtraMessageBox.Show("Ubicaciones no eliminadas", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function Comprueba_Grid() As Boolean
        Comprueba_Grid = False
        If dgridAsignacionOperadores.Nodes.Count > 0 Then
            Comprueba_Grid = True
        End If
    End Function
    Private Sub Crea_TreeList_Bodega(ByRef tl As TreeList)

        Try

            tl.BeginUpdate()
            tl.Columns.Add()
            tl.Columns(0).Caption = "Bodega: " & cmbBodega.EditValue
            tl.Columns(0).VisibleIndex = 0
            tl.Columns(0).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(1).Caption = cmbBodega.Text
            tl.Columns(1).VisibleIndex = 1
            tl.Columns(1).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            '#EJC20220421:En devexpress 21.2 se exige unicidad del caption.
            tl.Columns(2).Caption = cmbBodega.Text & "_Ref"
            tl.Columns(2).VisibleIndex = 1
            tl.Columns(2).Visible = False
            tl.Columns(2).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.EndUpdate()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        DTOri = clsLnBodega_orientacion_pos.Listar

    End Sub

    'Private Sub Crea_Estructura_Ubicaciones(ByRef tl As TreeList, ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

    '    'Obtener las areas de la bodega
    '    Dim parentForRootNodes As TreeListNode = Nothing
    '    Dim NodoArea As TreeListNode
    '    Dim NodoSector As TreeListNode = Nothing
    '    Dim NodoTramo As TreeListNode = Nothing
    '    Dim NodoUbicacion As TreeListNode = Nothing

    '    Try

    '        tl.BeginUnboundLoad()

    '        DTUbicaciones = clsLnBodega.Get_Estructura_By_IdBodega_And_IdInventarioEnc(IdBodega,
    '                                                                                   gBeTransInvEnc.Idinventarioenc,
    '                                                                                   lConnection,
    '                                                                                   lTransaction)

    '        Dim Areas = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
    '                                                             Key .IdArea = row.Field(Of Integer)("IdArea"),
    '                                                             Key .Name = row.Field(Of String)("Area")
    '                                                         }).Distinct().ToArray().OrderBy(Function(x) x.IdArea)

    '        For Each Area In Areas

    '            NodoArea = tl.AppendNode(New Object() {"Área: " & Area.IdArea, Area.Name, ("A")}, parentForRootNodes)

    '            Dim Sectores = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
    '                                                                     Key .IdSector = row.Field(Of Integer)("IdSector"),
    '                                                                     Key .IdArea = row.Field(Of Integer)("IdArea"),
    '                                                                     Key .Name = row.Field(Of String)("Sector")
    '                                                                 }).Where(Function(e) e.IdArea = Area.IdArea).Distinct().ToArray

    '            For Each Sector In Sectores

    '                NodoSector = tl.AppendNode(New Object() {Sector.IdSector, Sector.Name}, NodoArea)

    '                Dim Tramos = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
    '                                                                         Key .IdTramo = row.Field(Of Integer)("IdTramo"),
    '                                                                         Key .IdArea = row.Field(Of Integer)("IdArea"),
    '                                                                         Key .IdSector = row.Field(Of Integer)("IdSector"),
    '                                                                         Key .Name = row.Field(Of String)("Tramo")
    '                                                                     }).Where(Function(e) e.IdSector = Sector.IdSector AndAlso e.IdArea = Sector.IdArea).Distinct().ToArray

    '                For Each Tramo In Tramos

    '                    NodoTramo = tl.AppendNode(New Object() {Tramo.IdTramo, Tramo.Name}, NodoSector)


    '                    Dim Ubicaciones = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
    '                                                                              Key .IdUbicacion = row.Field(Of Integer)("IdUbicacion"),
    '                                                                              Key .IdArea = row.Field(Of Integer)("IdArea"),
    '                                                                              Key .IdSector = row.Field(Of Integer)("IdSector"),
    '                                                                              Key .IdTramo = row.Field(Of Integer)("IdTramo"),
    '                                                                              Key .Name = row.Field(Of String)("Ubicacion"),
    '                                                                              Key .IdOperador = IIf(IsDBNull(row.Item("idoperador")), 0, row.Item("idoperador"))
    '                                                                            }).Where(Function(e) e.IdTramo = Tramo.IdTramo _
    '                                                                                     AndAlso e.IdSector = Tramo.IdSector _
    '                                                                                     AndAlso e.IdArea = Tramo.IdArea).Distinct().ToArray

    '                    For Each Ubic In Ubicaciones
    '                        NodoUbicacion = tl.AppendNode(New Object() {Ubic.IdUbicacion, Ubic.Name, IIf(IsDBNull(Ubic.IdOperador), 0, Ubic.IdOperador.ToString())}, NodoTramo)
    '                    Next

    '                Next

    '            Next

    '        Next

    '        tl.EndUnboundLoad()

    '        tl.ExpandAll()

    '    Catch ex As Exception
    '        SplashScreenManager.CloseForm(False)
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

    Private Sub Crea_Estructura_Ubicaciones_Operador(ByRef tl As TreeList, ByVal IdBodega As Integer)

        'Obtener las areas de la bodega
        Dim parentForRootNodes As TreeListNode = Nothing
        Dim NodoArea As TreeListNode
        Dim NodoSector As TreeListNode = Nothing
        Dim NodoTramo As TreeListNode = Nothing
        Dim NodoUbicacion As TreeListNode = Nothing
        Dim NodoOperador As TreeListNode = Nothing

        Try

            tl.ClearNodes()

            tl.BeginUnboundLoad()

            Dim Areas = DTUbicaciones.AsEnumerable().[Select](Function(row) New With
                                                            {
                                                                Key .Id = row.Field(Of Integer)("IdArea"),
                                                                Key .Name = row.Field(Of String)("Area")
                                                            }).Distinct().ToArray().OrderBy(Function(x) x.Id)

            For Each Area In Areas

                NodoArea = tl.AppendNode(New Object() {"Área: " & Area.Id, Area.Name, ("A")}, parentForRootNodes)

                Dim Sectores = DTUbicaciones.AsEnumerable().[Select](Function(row) New With
                                                                        {
                                                                            Key .Id = row.Field(Of Integer)("IdSector"),
                                                                            Key .ParentId = row.Field(Of Integer)("IdArea"),
                                                                            Key .Name = row.Field(Of String)("Sector")
                                                                        }).Where(Function(e) e.ParentId = Area.Id).Distinct().ToArray

                For Each Sector In Sectores

                    NodoSector = tl.AppendNode(New Object() {Sector.Id, Sector.Name}, NodoArea)

                    Dim Tramos = DTUbicaciones.AsEnumerable().[Select](Function(row) New With
                                                                           {
                                                                                Key .Id = row.Field(Of Integer)("IdTramo"),
                                                                                Key .ParentId = row.Field(Of Integer)("IdSector"),
                                                                                Key .Name = row.Field(Of String)("Tramo")
                                                                           }).Where(Function(e) e.ParentId = Sector.Id).Distinct().ToArray

                    For Each Tramo In Tramos

                        NodoTramo = tl.AppendNode(New Object() {Tramo.Id, Tramo.Name}, NodoSector)

                        Dim Ubicaciones = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                        Key .Id = row.Field(Of Integer)("IdUbicacion"),
                        Key .ParentId = row.Field(Of Integer)("IdTramo"),
                        Key .NameUbic = row.Field(Of String)("Ubicacion"),
                        Key .Operador = IIf(IsDBNull(row.Item("Operador")), "", row.Item("Operador")),
                        Key .IdOperador = IIf(IsDBNull(row.Item("idoperador")), 0, row.Item("idoperador"))
                        }).Where(Function(e) e.ParentId = Tramo.Id).Distinct().ToArray

                        For Each Ubic In Ubicaciones
                            NodoUbicacion = tl.AppendNode(New Object() {Ubic.Id,
                                                                        Ubic.NameUbic,
                                                                        ("U"),
                                                                        Ubic.Operador,
                                                                        IIf(IsDBNull(Ubic.IdOperador), 0, Ubic.IdOperador)},
                                                                        NodoTramo)
                            NodoUbicacion.Tag = Ubic.Id
                        Next

                    Next

                Next

            Next

            tl.ExpandAll()

            tl.EndUnboundLoad()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub treeList1_NodeCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs) Handles dgridAsignacionOperadores.NodeCellStyle

        Try

            Dim tree As TreeList = TryCast(sender, TreeList)
            Dim index As Integer = tree.GetVisibleIndexByNode(e.Node)

            If e.Node.Level = 2 Then
                e.Appearance.Font = New Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_TreeList_BodegaOperador(ByRef tlO As TreeList)

        Try

            tlO.BeginUnboundLoad()
            tlO.Columns.Add()
            tlO.Columns(0).Caption = "Bodega: " & cmbBodega.EditValue
            tlO.Columns(0).VisibleIndex = 0
            tlO.Columns(0).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(1).Caption = cmbBodega.Text
            tlO.Columns(1).VisibleIndex = 1
            tlO.Columns(1).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            '#EJC20220421:En devexpress 21.2 se exige unicidad del caption.
            tlO.Columns(2).Caption = cmbBodega.Text & "_Ref"
            tlO.Columns(2).VisibleIndex = 2
            tlO.Columns(2).Visible = False
            tlO.Columns(2).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(3).Caption = "Operadores"
            tlO.Columns(3).VisibleIndex = 3
            tlO.Columns(3).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.EndUnboundLoad()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        DTOriOperador = clsLnBodega_orientacion_pos.Listar

    End Sub

    Private Sub Listar_Bodega(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            dgridAsignacionUbicaciones.ClearNodes()
            dgridAsignacionOperadores.ClearNodes()

            Crea_Estructura_Ubicaciones(dgridAsignacionUbicaciones, cmbBodega.EditValue, lConnection, lTransaction)

            Crea_Estructura_Ubicaciones_Operador(dgridAsignacionOperadores, cmbBodega.EditValue)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Bodega()

        Try

            dgridAsignacionUbicaciones.ClearNodes()
            dgridAsignacionOperadores.ClearNodes()

            Crea_Estructura_Ubicaciones(dgridAsignacionUbicaciones, cmbBodega.EditValue)

            Crea_Estructura_Ubicaciones_Operador(dgridAsignacionOperadores, cmbBodega.EditValue)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tlUbicaciones_AfterCheckNode(sender As Object, e As NodeEventArgs) Handles dgridAsignacionUbicaciones.AfterCheckNode

        dgridAsignacionUbicaciones.SetFocusedNode(e.Node)

        Try

            Dim ND = dgridAsignacionUbicaciones.FocusedNode

            If ND Is Nothing Then
                Return
            End If

            If ND.Checked Then

                ND.CheckAll()

            Else

                ND.UncheckAll()

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub btnFiltLimpia_Click(sender As Object, e As EventArgs) Handles btnFiltLimpia.Click
        limpiaFiltro()
    End Sub

    Private Sub Aplicar_Filtro()

        Dim nl, fcnt As Integer
        Dim nd As TreeListNode
        Dim flag As Boolean
        Dim subic As String

        Try

            nl = dgridAsignacionUbicaciones.FocusedNode.Level

            If nl < 2 Then
                XtraMessageBox.Show("Debe definir un tramo para aplicar filtro de búsqueda", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return
            End If

            If nl = 2 Then nd = dgridAsignacionUbicaciones.FocusedNode Else nd = dgridAsignacionUbicaciones.FocusedNode.ParentNode

            limpiaFiltro()
            nd.Expand()

            For Each r1 As TreeListNode In nd.Nodes

                subic = r1.GetDisplayText(dgridAsignacionUbicaciones.Columns(1))
                flag = False : fcnt = 0
                For Each ss As String In ufilt
                    If ss <> "X" Then
                        If InStr(subic, ss, CompareMethod.Text) > 0 Then
                            fcnt += 1
                        End If
                    End If
                Next

                r1.Visible = ufiltcnt = fcnt

            Next

            dgridAsignacionUbicaciones.MakeNodeVisible(nd)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Buscar_Ubicacion()

        Dim ss As String

        Try

            For Each r1 As TreeListNode In dgridAsignacionUbicaciones.Nodes
                For Each r2 As TreeListNode In r1.Nodes
                    For Each r3 As TreeListNode In r2.Nodes
                        For Each r4 As TreeListNode In r3.Nodes

                            ss = r4.GetDisplayText(dgridAsignacionUbicaciones.Columns(0))
                            If ss = ufiltubic Then

                                dgridAsignacionUbicaciones.CollapseAll()

                                r3.Expand()
                                r4.Selected = True

                                dgridAsignacionUbicaciones.SetFocusedNode(r4)
                                dgridAsignacionUbicaciones.MakeNodeVisible(r4)

                                Return
                            End If
                        Next
                    Next
                Next
            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtFiltroUbic_EditValueChanged(sender As Object, e As EventArgs) Handles txtFiltroUbic.EditValueChanged
        If txtFiltroUbic.Text = "" Then
            limpiaFiltro()
            Exit Sub
        End If
    End Sub

    Private Sub txtFiltroUbic_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFiltroUbic.KeyDown

        Dim ii, val As Integer
        Dim ss, sv As String
        Dim flag As Boolean

        If e.KeyCode <> Keys.Enter Then Exit Sub

        Try

            ufilt = txtFiltroUbic.Text.Split(" ")

            If ufilt.Length = 0 Then Return

            ufiltcod = False

            ufiltcnt = 0

            For ii = 0 To ufilt.Length - 1

                ss = UCase(ufilt(ii))

                If (Mid(ss, 1, 1) = "C") Then
                    sv = Mid(ss, 2)
                    Try
                        val = sv : If val < 1 Then Throw New Exception
                        ss = String.Format("C[{0}]", sv) : ufiltcnt += 1
                    Catch ex As Exception
                        ss = "X"
                    End Try
                Else
                    If (Mid(ss, 1, 1) = "N") Then
                        sv = Mid(ss, 2)
                        Try
                            val = sv : If val < 1 Then Throw New Exception
                            ss = String.Format("N[{0}]", sv) : ufiltcnt += 1
                        Catch ex As Exception
                            ss = "X"
                        End Try
                    Else
                        If (Mid(ss, 1, 1) = "P") Then
                            sv = Mid(ss, 2, 1) : If sv = "A" Or sv = "B" Or sv = "C" Or sv = "D" Then
                                flag = True
                            Else
                                flag = False
                            End If
                            If flag Then
                                ss = String.Format("P[{0}]", sv) : ufiltcnt += 1
                            Else
                                ss = "X"
                            End If
                        Else
                            Try
                                val = ss : If val < 1 Then Throw New Exception
                                ufiltcod = True
                                ufiltubic = ss
                            Catch ex As Exception
                                ss = "X"
                            End Try
                        End If
                    End If

                End If

                ufilt(ii) = ss

            Next

            If ufiltcod Then Buscar_Ubicacion() Else Aplicar_Filtro()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub limpiaFiltro()

        dgridAsignacionUbicaciones.BeginUnboundLoad()

        Try
            For Each r1 As TreeListNode In dgridAsignacionUbicaciones.Nodes
                r1.Visible = True
                For Each r2 As TreeListNode In r1.Nodes
                    r2.Visible = True
                    For Each r3 As TreeListNode In r2.Nodes
                        r3.Visible = True
                        For Each r4 As TreeListNode In r3.Nodes
                            r4.Visible = True
                        Next
                    Next
                Next
            Next
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        dgridAsignacionUbicaciones.EndUnboundLoad()

    End Sub

    Private Sub tlUbicacionesOperador_AfterCheckNode(sender As Object, e As NodeEventArgs) Handles dgridAsignacionOperadores.AfterCheckNode

        dgridAsignacionOperadores.SetFocusedNode(e.Node)

        Try

            Dim ND = dgridAsignacionOperadores.FocusedNode

            If ND.Checked Then
                'ND.ExpandAll()
                ND.CheckAll()
            Else
                'ND.ExpandAll()
                ND.UncheckAll()
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdAsignarOperador_Click(sender As Object, e As EventArgs) Handles cmdAsignarOperador.Click

        If dgridAsignacionOperadores.Nodes.Count > 0 Then

            If GuardarOperador() Then
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Operadores guardados correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Cargando datos...")
                Listar_Productos()
                Listar_Bodega()
                Carga_Detalle_Ciclico()
                SplashScreenManager.CloseForm(False)
            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Operadores no guardados", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Else
            XtraMessageBox.Show("Agregue ubicaciones primero.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function GuardarOperador() As Boolean

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Asignando Operador...")

        GuardarOperador = False

        Dim Ciclico As New clsBeTrans_inv_ciclico
        Dim lStockCongelado As New DataTable
        lStockCongelado = clsLnTrans_inv_stock.Get_All_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc,
                                                                          gBeTransInvEnc.IdBodega)
        Dim vIdProductoBodega As Integer = 0
        Dim vIdStock As Integer = 0
        Dim clsTrans As New clsTransaccion

        Try

            clsTrans.Open_Connection()
            clsTrans.Begin_Transaction()

            Dim Operador As New clsBeTrans_inv_operador

            For Each NOperador As TreeListNode In dgridAsignacionOperadores.Nodes

                For Each NSector As TreeListNode In NOperador.Nodes

                    For Each NTramo As TreeListNode In NSector.Nodes

                        For Each NUbicacion As TreeListNode In NTramo.Nodes

                            If NUbicacion.Checked Then

                                Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                                Operador.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                Operador.Idinvencreconteo = 0
                                Operador.Idubic = NUbicacion.Tag
                                Operador.Idoperador = cmbOperador.EditValue
                                Operador.IdBodega = AP.IdBodega

                                Dim FilasFiltradas() As DataRow = lStockCongelado.Select("IdUbicacion = " & NUbicacion.Tag)

                                For Each ProductoCongeladoInvByUbic As DataRow In FilasFiltradas

                                    vIdProductoBodega = ProductoCongeladoInvByUbic.Item("IdProductoBodega")

                                    If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(NUbicacion.Tag,
                                                                                        gBeTransInvEnc.Idinventarioenc,
                                                                                        clsTrans.lConnection,
                                                                                        clsTrans.lTransaction) Then

                                        Dim BeTransInvCiclicoUbic As New clsBeTrans_inv_ciclico_ubic()
                                        BeTransInvCiclicoUbic.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                        BeTransInvCiclicoUbic.Idubicacion = NUbicacion.Tag
                                        BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                                        clsLnTrans_inv_ciclico_ubic.Insertar(BeTransInvCiclicoUbic)

                                    End If

                                    Dim lBeTransInvCiclico As New List(Of clsBeTrans_inv_ciclico)
                                    lBeTransInvCiclico = clsLnTrans_inv_ciclico.Get_All_By_IdProductoBodega_And_IdUbicacion(gBeTransInvEnc.Idinventarioenc,
                                                                                                                            vIdProductoBodega,
                                                                                                                            NUbicacion.Tag,
                                                                                                                            clsTrans.lConnection,
                                                                                                                            clsTrans.lTransaction)

                                    Dim InvCiclico As New clsBeTrans_inv_ciclico

                                    If lBeTransInvCiclico.Count > 0 Then

                                        For Each invcic In lBeTransInvCiclico

                                            vIdStock = invcic.IdStock

                                            If Not clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador_And_IdStock(cmbOperador.EditValue,
                                                                                                                    gBeTransInvEnc.Idinventarioenc,
                                                                                                                    vIdProductoBodega,
                                                                                                                    vIdStock,
                                                                                                                    clsTrans.lConnection,
                                                                                                                    clsTrans.lTransaction) Then

                                                InvCiclico = New clsBeTrans_inv_ciclico
                                                InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                                                InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                                InvCiclico.IdStock = invcic.IdStock
                                                InvCiclico.IdProductoBodega = invcic.IdProductoBodega
                                                InvCiclico.IdProductoEstado = invcic.IdProductoEstado
                                                InvCiclico.IdProductoEst_nuevo = invcic.IdProductoEstado
                                                InvCiclico.IdUbicacion = invcic.IdUbicacion
                                                InvCiclico.IdPresentacion = invcic.IdPresentacion
                                                InvCiclico.EsNuevo = False
                                                InvCiclico.Lote_stock = invcic.Lote_stock
                                                InvCiclico.Lote = invcic.Lote
                                                InvCiclico.Fecha_vence_stock = invcic.Fecha_vence_stock
                                                InvCiclico.Fecha_vence = invcic.Fecha_vence
                                                InvCiclico.Cant_stock = invcic.Cant_stock
                                                InvCiclico.Cantidad = 0
                                                InvCiclico.Cant_reconteo = invcic.Cant_reconteo
                                                InvCiclico.Peso_stock = invcic.Peso_stock
                                                InvCiclico.Peso = invcic.Peso
                                                InvCiclico.Peso_reconteo = invcic.Peso_reconteo
                                                InvCiclico.Idoperador = Operador.Idoperador
                                                InvCiclico.User_agr = AP.UsuarioAp.Nombres
                                                InvCiclico.Fec_agr = Now
                                                InvCiclico.EsPallet = invcic.EsPallet
                                                InvCiclico.lic_plate = invcic.lic_plate
                                                InvCiclico.IdBodega = AP.IdBodega
                                                InvCiclico.IdUnidadMedida = invcic.IdUnidadMedida
                                                InvCiclico.Cantidad_Reservada_UMBas = invcic.Cantidad_Reservada_UMBas

                                                clsLnTrans_inv_ciclico.Insertar(InvCiclico, clsTrans.lConnection, clsTrans.lTransaction)

                                            End If

                                        Next

                                    Else

                                        Dim lInvCongelado As New List(Of clsBeTrans_inv_stock)

                                        lInvCongelado = clsLnTrans_inv_stock.Get_All_By_IdInventarioEnc_And_IdProductoBodega(gBeTransInvEnc.Idinventarioenc,
                                                                                                                             vIdProductoBodega,
                                                                                                                             NUbicacion.Tag,
                                                                                                                             clsTrans.lConnection,
                                                                                                                             clsTrans.lTransaction)
                                        'GT 02092021 1222: si hay existencia iterar
                                        If lInvCongelado.Count > 0 Then

                                            For Each StockCongelado In lInvCongelado

                                                If Not clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador_And_IdStock(cmbOperador.EditValue,
                                                                                                                        gBeTransInvEnc.Idinventarioenc,
                                                                                                                        vIdProductoBodega,
                                                                                                                        StockCongelado.IdStock,
                                                                                                                        clsTrans.lConnection,
                                                                                                                        clsTrans.lTransaction) Then
                                                    InvCiclico = New clsBeTrans_inv_ciclico
                                                    InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                                                    InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                                    InvCiclico.IdStock = StockCongelado.IdStock
                                                    InvCiclico.IdProductoBodega = StockCongelado.IdProductoBodega
                                                    InvCiclico.IdProductoEstado = StockCongelado.IdProductoEstado
                                                    InvCiclico.IdProductoEst_nuevo = StockCongelado.IdProductoEstado
                                                    InvCiclico.IdPresentacion = StockCongelado.IdPresentacion
                                                    InvCiclico.IdUbicacion = StockCongelado.IdUbicacion
                                                    InvCiclico.IdUnidadMedida = StockCongelado.IdUnidadMedida
                                                    InvCiclico.Lote_stock = StockCongelado.Lote
                                                    InvCiclico.Lote = StockCongelado.Lote
                                                    InvCiclico.Fecha_vence_stock = StockCongelado.Fecha_vence
                                                    InvCiclico.Fecha_vence = StockCongelado.Fecha_vence
                                                    InvCiclico.Cant_stock = StockCongelado.Cantidad
                                                    InvCiclico.Peso_stock = StockCongelado.Peso
                                                    InvCiclico.EsNuevo = False
                                                    InvCiclico.Idoperador = cmbOperador.EditValue
                                                    InvCiclico.User_agr = AP.UsuarioAp.User_agr
                                                    InvCiclico.Fec_agr = Now
                                                    InvCiclico.Cantidad = 0.0
                                                    InvCiclico.EsPallet = False 'StockCongelado.IdPresentacion Is Pallet ? -> EJC20180807
                                                    InvCiclico.lic_plate = StockCongelado.Lic_plate
                                                    InvCiclico.IdBodega = StockCongelado.IdBodega
                                                    InvCiclico.Cantidad_Reservada_UMBas = StockCongelado.Cantidad_Reservada_UMBas

                                                    clsLnTrans_inv_ciclico.Insertar(InvCiclico, clsTrans.lConnection, clsTrans.lTransaction)

                                                End If

                                                Debug.Print("Procesando interno IdStock: " & StockCongelado.IdStock)

                                            Next

                                        End If

                                    End If

                                Next

                                If Not clsLnTrans_inv_operador.Get_By_Operador(Operador, clsTrans.lConnection, clsTrans.lTransaction) Then

                                    '    XtraMessageBox.Show("Ya existe operador", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    '    GuardarOperador = False
                                    '    Exit For

                                    'Else

                                    Try

                                        clsLnTrans_inv_operador.Insertar(Operador, clsTrans.lConnection, clsTrans.lTransaction)

                                        Ciclico.Idoperador = cmbOperador.EditValue
                                        Ciclico.IdUbicacion = NUbicacion.Tag
                                        Ciclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc

                                        '#CKFK20250801 Modifiqué la validación
                                        If Not clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador(cmbOperador.EditValue,
                                                                                                    gBeTransInvEnc.Idinventarioenc,
                                                                                                    vIdProductoBodega,
                                                                                                    clsTrans.lConnection,
                                                                                                    clsTrans.lTransaction) Then

                                            Dim lBeTransInvCiclico As New List(Of clsBeTrans_inv_ciclico)
                                            lBeTransInvCiclico = clsLnTrans_inv_ciclico.Get_All_By_IdProductoBodega_And_IdUbicacion(gBeTransInvEnc.Idinventarioenc,
                                                                                                                                    vIdProductoBodega,
                                                                                                                                    NUbicacion.Tag,
                                                                                                                                    clsTrans.lConnection,
                                                                                                                                    clsTrans.lTransaction)

                                            Dim InvCiclico As New clsBeTrans_inv_ciclico

                                            For Each invcic In lBeTransInvCiclico

                                                InvCiclico = New clsBeTrans_inv_ciclico
                                                InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                                                InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                                InvCiclico.IdStock = invcic.IdStock
                                                InvCiclico.IdProductoBodega = invcic.IdProductoBodega
                                                InvCiclico.IdProductoEstado = invcic.IdProductoEstado
                                                InvCiclico.IdProductoEst_nuevo = invcic.IdProductoEstado
                                                InvCiclico.IdUbicacion = invcic.IdUbicacion
                                                InvCiclico.IdPresentacion = invcic.IdPresentacion
                                                InvCiclico.EsNuevo = False
                                                InvCiclico.Lote_stock = invcic.Lote_stock
                                                InvCiclico.Lote = invcic.Lote
                                                InvCiclico.Fecha_vence_stock = invcic.Fecha_vence_stock
                                                InvCiclico.Fecha_vence = invcic.Fecha_vence
                                                InvCiclico.Cant_stock = invcic.Cant_stock
                                                InvCiclico.Cantidad = invcic.Cantidad
                                                InvCiclico.Cant_reconteo = invcic.Cant_reconteo
                                                InvCiclico.Peso_stock = invcic.Peso_stock
                                                InvCiclico.Peso = invcic.Peso
                                                InvCiclico.Peso_reconteo = invcic.Peso_reconteo
                                                InvCiclico.Idoperador = Operador.Idoperador
                                                InvCiclico.User_agr = AP.UsuarioAp.Nombres
                                                InvCiclico.Fec_agr = Now
                                                InvCiclico.EsPallet = invcic.EsPallet
                                                InvCiclico.lic_plate = invcic.lic_plate
                                                InvCiclico.IdBodega = AP.IdBodega
                                                InvCiclico.IdUnidadMedida = invcic.IdUnidadMedida
                                                InvCiclico.Cantidad_Reservada_UMBas = invcic.Cantidad_Reservada_UMBas

                                                clsLnTrans_inv_ciclico.Insertar(InvCiclico, clsTrans.lConnection, clsTrans.lTransaction)

                                            Next


                                        End If

                                        GuardarOperador = True

                                    Catch ex As Exception

                                    End Try

                                End If

                            Else
                                XtraMessageBox.Show("Debe seleccionar un registro para asignar al operador.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If

                        Next

                    Next

                Next

            Next

            clsTrans.Commit_Transaction()

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            clsTrans.Close_Conection()
        End Try

    End Function

    Private Function EliminaOperadores() As Boolean

        EliminaOperadores = False

        Dim clsTrans As New clsTransaccion

        Try

            clsTrans.Begin_Transaction()

            Dim Operador As New clsBeTrans_inv_operador

            For Each NA As TreeListNode In dgridAsignacionOperadores.Nodes

                For Each NS As TreeListNode In NA.Nodes

                    For Each NT As TreeListNode In NS.Nodes

                        For Each NU As TreeListNode In NT.Nodes

                            If NU.Checked Then

                                Operador.Idubic = NU.Tag
                                Operador.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                Operador.Idoperador = cmbOperador.EditValue 'IdOperador, EJC

                                Dim BeInventarioCiclico As New clsBeTrans_inv_ciclico
                                BeInventarioCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                BeInventarioCiclico.IdUbicacion = NU.Tag
                                BeInventarioCiclico.Idoperador = cmbOperador.EditValue
                                clsLnTrans_inv_ciclico.Eliminar_By_IdOperador_And_IdUbicacion(BeInventarioCiclico, clsTrans.lConnection, clsTrans.lTransaction)

                                If BeInventarioCiclico.Idoperador = 0 Then
                                    clsLnTrans_inv_ciclico.Eliminar_By_IdUbicacion(BeInventarioCiclico, clsTrans.lConnection, clsTrans.lTransaction)
                                    clsLnTrans_inv_ciclico_ubic.Elimina_By_IdUbicacion(gBeTransInvEnc.Idinventarioenc, BeInventarioCiclico.IdUbicacion, clsTrans.lConnection, clsTrans.lTransaction)
                                    clsLnTrans_inv_operador.Eliminar_Operador_By_IdUbicacion(Operador, clsTrans.lConnection, clsTrans.lTransaction)
                                End If

                                Try

                                    clsLnTrans_inv_operador.EliminarByOperador(Operador, clsTrans.lConnection, clsTrans.lTransaction)

                                    EliminaOperadores = True

                                Catch ex As Exception

                                End Try

                            End If

                        Next

                    Next

                Next

            Next

            clsTrans.Commit_Transaction()

        Catch ex As Exception

            clsTrans.RollBack_Transaction()

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            clsTrans.Close_Conection()
        End Try

    End Function

    Private Sub cmdQuitarOperador_Click(sender As Object, e As EventArgs) Handles cmdQuitarOperador.Click

        If dgridAsignacionOperadores.Nodes.Count > 0 Then

            If XtraMessageBox.Show("Está seguro de eliminar el operador de las ubicaciones seleccionadas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then

                If EliminaOperadores() Then

                    Listar_Bodega()
                    Listar_Productos()
                    Listar_Datos_De_Inventario()

                Else
                    XtraMessageBox.Show("No se pudo eliminar", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

        Else
            XtraMessageBox.Show("No hay operadores asignados aún.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub cmdAgregarProducto_Click(sender As Object, e As EventArgs) Handles cmdAgregarProducto.Click

        Dim Operador As New clsBeTrans_inv_operador

        Try

            Dim Productos As New frmInventarioProductos()
            Productos.Propietario = cmbPropietario.Text
            Productos.IdBodega = cmbBodega.EditValue
            Productos.FechasCongelacion = gBeTransInvEnc.Fec_agr
            Productos.IdPropietario = pIdPropietario
            Productos.IdInventario = gBeTransInvEnc.Idinventarioenc
            Productos.IdOperador = cmbOperadorProd.EditValue
            Productos.ShowDialog()
            Productos.Dispose()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando productos...")

            'Inserta_Operadores()

            Listar_Productos()

            Carga_Detalle_Ciclico()

            Listar_Bodega()

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    'Private Sub Listar_Productos(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

    '    Try

    '        dgridAsignacionProductos.ClearNodes()
    '        Listar_Productos_Asignados(dgridAsignacionProductos, lConnection, lTransaction)
    '        dgridAsignacionProductos.CollapseAll()

    '    Catch ex As Exception
    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '        Text,
    '        MessageBoxButtons.OK,
    '        MessageBoxIcon.Error)
    '    End Try


    'End Sub

    Private Sub Crea_TreeList_AsignaProductos(ByRef tl As TreeList)

        Try

            tl.BeginUpdate()
            tl.Columns.Add()
            tl.Columns(0).Caption = "IdProductoBodega"
            tl.Columns(0).VisibleIndex = 0
            tl.Columns(0).Visible = False
            tl.Columns(0).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(0).Width = 50
            tl.Columns.Add()
            tl.Columns(1).Caption = "Código"
            tl.Columns(1).VisibleIndex = 1
            tl.Columns(1).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(1).Width = 100
            tl.Columns(1).Visible = True
            tl.Columns.Add()
            tl.Columns(2).Caption = "Nombre"
            tl.Columns(2).VisibleIndex = 2
            tl.Columns(2).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(2).Width = 150
            tl.Columns(1).Visible = True
            tl.Columns.Add()
            tl.Columns(3).Caption = "UMBas"
            tl.Columns(3).VisibleIndex = 3
            tl.Columns(3).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(3).Width = 150
            tl.Columns(1).Visible = True
            tl.Columns.Add()
            tl.Columns(4).Caption = "Operador"
            tl.Columns(4).VisibleIndex = 4
            tl.Columns(4).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(4).Width = 150
            tl.Columns(1).Visible = True
            tl.Columns.Add()
            tl.Columns(5).Caption = "TipoProducto"
            tl.Columns(5).VisibleIndex = 5
            tl.Columns(5).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(5).Width = 150
            tl.Columns(1).Visible = True
            tl.Columns.Add()
            tl.Columns(6).Caption = "IdOperador"
            tl.Columns(6).VisibleIndex = 6
            tl.Columns(6).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(6).Width = 150
            tl.Columns(6).Visible = False
            tl.Columns.Add()
            tl.Columns(7).Caption = "Ubicacion"
            tl.Columns(7).VisibleIndex = 7
            tl.Columns(7).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(7).Width = 150
            tl.Columns(7).Visible = True
            tl.Columns.Add()
            tl.EndUpdate()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub Listar_Productos_Asignados(ByRef tl As TreeList, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

    '    Try

    '        tl.BeginUnboundLoad()

    '        ListInventarioCiclico.Clear()

    '        tl.ClearNodes()

    '        ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(gBeTransInvEnc.Idinventarioenc,
    '                                                                                                               AP.IdBodega,
    '                                                                                                               lConnection,
    '                                                                                                               lTransaction)

    '        If ListInventarioCiclico.Count > 0 Then

    '            Dim parentForRootNodes As TreeListNode = Nothing

    '            Dim rootNode As TreeListNode

    '            Dim Lista = From i In ListInventarioCiclico Group i By Keys = New With {
    '                                                                          Key i.IdProductoBodega,
    '                                                                          Key i.Codigo,
    '                                                                          Key i.Producto,
    '                                                                          Key i.UnidadMedida,
    '                                                                          Key i.TipoProducto,
    '                                                                          Key i.Operador,
    '                                                                          Key i.Idoperador,
    '                                                                          Key i.Ubicacion} Into Group
    '                        Select New With {Keys.Codigo, Keys.Producto, Keys.UnidadMedida,
    '                                         Keys.TipoProducto, Keys.IdProductoBodega,
    '                                         Keys.Operador, Keys.Idoperador, Keys.Ubicacion,
    '                                         .Inventario_Inicial = Group.Sum(Function(x) x.Cant_stock)}

    '            For Each BeTransInvCiclico In Lista

    '                rootNode = tl.AppendNode(New Object() {BeTransInvCiclico.IdProductoBodega,
    '                                                       BeTransInvCiclico.Codigo,
    '                                                       BeTransInvCiclico.Producto,
    '                                                       BeTransInvCiclico.UnidadMedida,
    '                                                       BeTransInvCiclico.Operador,
    '                                                       BeTransInvCiclico.TipoProducto,
    '                                                       BeTransInvCiclico.Idoperador,
    '                                                       BeTransInvCiclico.Ubicacion}, parentForRootNodes)
    '                rootNode.Expanded = True
    '                rootNode.Tag = BeTransInvCiclico.IdProductoBodega

    '            Next

    '        End If

    '        '#Inserta_Ubicaciones
    '        'Inserta_Ubicaciones(Obj.IdUbicacion)

    '        tl.EndUnboundLoad()

    '        dgridAsignacionProductos.OptionsView.ShowSummaryFooter = True
    '        dgridAsignacionProductos.Columns(1).AllNodesSummary = True
    '        dgridAsignacionProductos.Columns(1).SummaryFooterStrFormat = "Productos: {0:n0}"
    '        dgridAsignacionProductos.Columns(1).SummaryFooter = SummaryItemType.Count

    '    Catch ex As Exception
    '        SplashScreenManager.CloseForm(False)
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

    Private Sub operadoresUbic()

        Try

            Dim gBeInvCiclico As New clsBeTrans_inv_ciclico

            For Each Op As TreeListNode In dgridAsignacionProductos.Nodes

                If Op.Checked Then

                    gBeInvCiclico.IdStock = Op.Tag
                    gBeInvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                    gBeInvCiclico.IdProductoBodega = Op.Item("IdProductoBodega")
                    Dim Operador As New frmEliminOperadores()

                    If Op.Tag = 0 Then
                        Operador.Modo = frmEliminOperadores.TipoTrans.NoStock
                    Else
                        Operador.Modo = frmEliminOperadores.TipoTrans.Editar
                    End If

                    clsLnTrans_inv_ciclico.GetSingleByStock(gBeInvCiclico)

                    Operador.gBeCiclico = gBeInvCiclico
                    Operador.ShowDialog()
                    Operador.Dispose()

                End If

            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdAsignarOp_Click(sender As Object, e As EventArgs) Handles cmdAsignarOp.Click

        If XtraMessageBox.Show("¿Asignar todos los operadores?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            If Inserta_Operadores() Then
                XtraMessageBox.Show("Operadores Asignados", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar_Productos() : Listar_Bodega()
                Carga_Detalle_Ciclico()
            End If
        Else
            operadoresUbic()
            Listar_Productos()
            Carga_Detalle_Ciclico()
        End If

    End Sub

    Private Function Inserta_Operadores() As Boolean

        Inserta_Operadores = False

        Dim Ubicaciones As New List(Of clsBeTrans_inv_ciclico_ubic)
        Dim cTrans As New clsTransaccion()
        Dim InvCiclico As New clsBeTrans_inv_ciclico
        Dim BeTransInvCiclicoUbic As New clsBeTrans_inv_ciclico_ubic

        Try

            Dim NuevoStock As New clsBeTrans_inv_ciclico
            Dim DTOperador As New DataTable
            Dim Ciclico As New List(Of clsBeTrans_inv_ciclico)
            Dim Operador As New clsBeTrans_inv_operador

            cTrans.Open_Connection() : cTrans.Begin_Transaction()

            DTOperador = clsLnOperador.Get_All_For_Combo_By_Rol_Inventario(AP.IdBodega,
                                                                           cTrans.lConnection,
                                                                           cTrans.lTransaction)

            For Each NAsignacion As TreeListNode In dgridAsignacionProductos.Nodes

                If NAsignacion.Checked Then

                    Dim vIdProductoBodega As Integer = clsLnProducto.Get_IdProductoBodega_By_Codigo_And_IdBodega(NAsignacion.Item("Código"), AP.IdBodega, cTrans.lConnection, cTrans.lTransaction)
                    Dim vUbicacion As Integer = 0

                    If NAsignacion.Item("Ubicacion").ToString.Split("#").Any Then
                        vUbicacion = Val(NAsignacion.Item("Ubicacion").ToString.Split("#")(1))
                    End If

                    For Each op As DataRow In DTOperador.Rows

                        Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(cTrans.lConnection, cTrans.lTransaction)
                        Operador.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                        Operador.Idinvencreconteo = 0
                        Operador.Idubic = vUbicacion
                        Operador.Idoperador = op.Item("IdOperador")
                        Operador.IdBodega = AP.IdBodega

                        If clsLnTrans_inv_operador.Get_By_Operador(Operador, cTrans.lConnection, cTrans.lTransaction) Then

                            If Not clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador(Operador.Idoperador,
                                                                                                gBeTransInvEnc.Idinventarioenc,
                                                                                                vIdProductoBodega,
                                                                                                cTrans.lConnection,
                                                                                                cTrans.lTransaction) Then

                                Dim lBeTransInvCiclico As New List(Of clsBeTrans_inv_ciclico)
                                lBeTransInvCiclico = clsLnTrans_inv_ciclico.Get_All_By_IdProductoBodega_And_IdUbicacion(gBeTransInvEnc.Idinventarioenc,
                                                                                                                                vIdProductoBodega,
                                                                                                                                vUbicacion,
                                                                                                                                cTrans.lConnection,
                                                                                                                                cTrans.lTransaction)



                                ' Crear una lista única por IdStock e IdUbicacion
                                Dim distinctList As List(Of clsBeTrans_inv_ciclico) = lBeTransInvCiclico _
                                                                                            .GroupBy(Function(x) New With {Key x.IdStock, Key x.IdUbicacion}) _
                                                                                            .Select(Function(g) g.First()) _
                                                                                            .ToList()


                                For Each invcic In distinctList

                                    InvCiclico = New clsBeTrans_inv_ciclico
                                    InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(cTrans.lConnection, cTrans.lTransaction)
                                    InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                    InvCiclico.IdStock = invcic.IdStock
                                    InvCiclico.IdProductoBodega = invcic.IdProductoBodega
                                    InvCiclico.IdProductoEstado = invcic.IdProductoEstado
                                    InvCiclico.IdProductoEst_nuevo = invcic.IdProductoEstado
                                    InvCiclico.IdUbicacion = invcic.IdUbicacion
                                    InvCiclico.IdPresentacion = invcic.IdPresentacion
                                    InvCiclico.EsNuevo = False
                                    InvCiclico.Lote_stock = invcic.Lote_stock
                                    InvCiclico.Lote = invcic.Lote
                                    InvCiclico.Fecha_vence_stock = invcic.Fecha_vence_stock
                                    InvCiclico.Fecha_vence = invcic.Fecha_vence
                                    InvCiclico.Cant_stock = invcic.Cant_stock
                                    InvCiclico.Cantidad = 0
                                    InvCiclico.Cant_reconteo = 0
                                    InvCiclico.Peso_stock = invcic.Peso_stock
                                    InvCiclico.Peso = invcic.Peso
                                    InvCiclico.Peso_reconteo = invcic.Peso_reconteo
                                    InvCiclico.Idoperador = Operador.Idoperador
                                    InvCiclico.User_agr = AP.UsuarioAp.Nombres
                                    InvCiclico.Fec_agr = Now
                                    InvCiclico.EsPallet = invcic.EsPallet
                                    InvCiclico.lic_plate = invcic.lic_plate
                                    InvCiclico.IdBodega = AP.IdBodega
                                    InvCiclico.IdUnidadMedida = invcic.IdUnidadMedida
                                    InvCiclico.Cantidad_Reservada_UMBas = invcic.Cantidad_Reservada_UMBas

                                    If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(invcic.IdUbicacion, invcic.Idinventarioenc, cTrans.lConnection, cTrans.lTransaction) Then

                                        BeTransInvCiclicoUbic.Idinventarioenc = IdInventario
                                        BeTransInvCiclicoUbic.Idubicacion = invcic.IdUbicacion
                                        BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                                        Ubicaciones.Add(BeTransInvCiclicoUbic)

                                    End If

                                    clsLnTrans_inv_ciclico.Insertar(InvCiclico, cTrans.lConnection, cTrans.lTransaction)

                                Next

                            End If

                        Else

                            Try

                                If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(vUbicacion, gBeTransInvEnc.Idinventarioenc, cTrans.lConnection, cTrans.lTransaction) Then

                                    Dim Ubicacion As New clsBeTrans_inv_ciclico_ubic
                                    Ubicacion.Idubicacion = vUbicacion
                                    Ubicacion.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                    Ubicacion.IdBodega = AP.IdBodega
                                    Ubicaciones.Add(BeTransInvCiclicoUbic)

                                End If

                                If Not clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador(Operador.Idoperador,
                                                                                                    gBeTransInvEnc.Idinventarioenc,
                                                                                                    vIdProductoBodega,
                                                                                                    cTrans.lConnection,
                                                                                                    cTrans.lTransaction) Then

                                    Dim lBeTransInvCiclico As New List(Of clsBeTrans_inv_ciclico)
                                    lBeTransInvCiclico = clsLnTrans_inv_ciclico.Get_All_By_IdProductoBodega_And_IdUbicacion(gBeTransInvEnc.Idinventarioenc,
                                                                                                                                    vIdProductoBodega,
                                                                                                                                    vUbicacion,
                                                                                                                                    cTrans.lConnection,
                                                                                                                                    cTrans.lTransaction)

                                    ' Crear una lista única por IdStock e IdUbicacion
                                    Dim distinctList As List(Of clsBeTrans_inv_ciclico) = lBeTransInvCiclico _
                                                                                            .GroupBy(Function(x) New With {Key x.IdStock, Key x.IdUbicacion}) _
                                                                                            .Select(Function(g) g.First()) _
                                                                                            .ToList()

                                    'Dim InvCiclico As New clsBeTrans_inv_ciclico

                                    For Each invcic In distinctList

                                        InvCiclico = New clsBeTrans_inv_ciclico
                                        InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(cTrans.lConnection, cTrans.lTransaction)
                                        InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                        InvCiclico.IdStock = invcic.IdStock
                                        InvCiclico.IdProductoBodega = invcic.IdProductoBodega
                                        InvCiclico.IdProductoEstado = invcic.IdProductoEstado
                                        InvCiclico.IdProductoEst_nuevo = invcic.IdProductoEstado
                                        InvCiclico.IdUbicacion = invcic.IdUbicacion
                                        InvCiclico.IdPresentacion = invcic.IdPresentacion
                                        InvCiclico.EsNuevo = False
                                        InvCiclico.Lote_stock = invcic.Lote_stock
                                        InvCiclico.Lote = invcic.Lote
                                        InvCiclico.Fecha_vence_stock = invcic.Fecha_vence_stock
                                        InvCiclico.Fecha_vence = invcic.Fecha_vence
                                        InvCiclico.Cant_stock = invcic.Cant_stock
                                        InvCiclico.Cantidad = 0
                                        InvCiclico.Cant_reconteo = 0
                                        InvCiclico.Peso_stock = invcic.Peso_stock
                                        InvCiclico.Peso = invcic.Peso
                                        InvCiclico.Peso_reconteo = invcic.Peso_reconteo
                                        InvCiclico.Idoperador = Operador.Idoperador
                                        InvCiclico.User_agr = AP.UsuarioAp.Nombres
                                        InvCiclico.Fec_agr = Now
                                        InvCiclico.EsPallet = invcic.EsPallet
                                        InvCiclico.lic_plate = invcic.lic_plate
                                        InvCiclico.IdBodega = AP.IdBodega
                                        InvCiclico.IdUnidadMedida = invcic.IdUnidadMedida
                                        InvCiclico.Cantidad_Reservada_UMBas = invcic.Cantidad_Reservada_UMBas

                                        clsLnTrans_inv_ciclico.Insertar(InvCiclico, cTrans.lConnection, cTrans.lTransaction)

                                        If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(invcic.IdUbicacion, gBeTransInvEnc.Idinventarioenc, cTrans.lConnection, cTrans.lTransaction) Then

                                            BeTransInvCiclicoUbic.Idinventarioenc = IdInventario
                                            BeTransInvCiclicoUbic.Idubicacion = invcic.IdUbicacion
                                            BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                                            Ubicaciones.Add(BeTransInvCiclicoUbic)

                                        End If

                                    Next

                                    Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(cTrans.lConnection, cTrans.lTransaction)
                                    Operador.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                    Operador.Idinvencreconteo = 0
                                    Operador.Idubic = vUbicacion
                                    Operador.IdBodega = gBeTransInvEnc.IdBodega
                                    Operador.Idoperador = Operador.Idoperador

                                    If Not clsLnTrans_inv_operador.Existe_Ubicacion_By_IdOperador(Operador, cTrans.lConnection, cTrans.lTransaction) Then
                                        clsLnTrans_inv_operador.Insertar(Operador, cTrans.lConnection, cTrans.lTransaction)
                                    End If

                                Else

                                    Dim lBeTransInvCiclico As New List(Of clsBeTrans_inv_ciclico)
                                    lBeTransInvCiclico = clsLnTrans_inv_ciclico.Get_All_By_IdProductoBodega_And_IdUbicacion(gBeTransInvEnc.Idinventarioenc,
                                                                                                                                    vIdProductoBodega,
                                                                                                                                    vUbicacion,
                                                                                                                                    cTrans.lConnection,
                                                                                                                                    cTrans.lTransaction)



                                    ' Crear una lista única por IdStock e IdUbicacion
                                    Dim distinctList As List(Of clsBeTrans_inv_ciclico) = lBeTransInvCiclico _
                                                                                            .GroupBy(Function(x) New With {Key x.IdStock, Key x.IdUbicacion}) _
                                                                                            .Select(Function(g) g.First()) _
                                                                                            .ToList()


                                    'Dim InvCiclico As New clsBeTrans_inv_ciclico

                                    For Each invcic In distinctList

                                        InvCiclico = New clsBeTrans_inv_ciclico
                                        InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(cTrans.lConnection, cTrans.lTransaction)
                                        InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                        InvCiclico.IdStock = invcic.IdStock
                                        InvCiclico.IdProductoBodega = invcic.IdProductoBodega
                                        InvCiclico.IdProductoEstado = invcic.IdProductoEstado
                                        InvCiclico.IdProductoEst_nuevo = invcic.IdProductoEstado
                                        InvCiclico.IdUbicacion = invcic.IdUbicacion
                                        InvCiclico.IdPresentacion = invcic.IdPresentacion
                                        InvCiclico.EsNuevo = False
                                        InvCiclico.Lote_stock = invcic.Lote_stock
                                        InvCiclico.Lote = invcic.Lote
                                        InvCiclico.Fecha_vence_stock = invcic.Fecha_vence_stock
                                        InvCiclico.Fecha_vence = invcic.Fecha_vence
                                        InvCiclico.Cant_stock = invcic.Cant_stock
                                        InvCiclico.Cantidad = 0
                                        InvCiclico.Cant_reconteo = 0
                                        InvCiclico.Peso_stock = invcic.Peso_stock
                                        InvCiclico.Peso = invcic.Peso
                                        InvCiclico.Peso_reconteo = invcic.Peso_reconteo
                                        InvCiclico.Idoperador = Operador.Idoperador
                                        InvCiclico.User_agr = AP.UsuarioAp.Nombres
                                        InvCiclico.Fec_agr = Now
                                        InvCiclico.EsPallet = invcic.EsPallet
                                        InvCiclico.lic_plate = invcic.lic_plate
                                        InvCiclico.IdBodega = AP.IdBodega
                                        InvCiclico.IdUnidadMedida = invcic.IdUnidadMedida
                                        InvCiclico.Cantidad_Reservada_UMBas = invcic.Cantidad_Reservada_UMBas

                                        If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(invcic.IdUbicacion, invcic.Idinventarioenc, cTrans.lConnection, cTrans.lTransaction) Then

                                            BeTransInvCiclicoUbic.Idinventarioenc = IdInventario
                                            BeTransInvCiclicoUbic.Idubicacion = invcic.IdUbicacion
                                            BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                                            Ubicaciones.Add(BeTransInvCiclicoUbic)

                                        End If

                                        clsLnTrans_inv_ciclico.Insertar(InvCiclico, cTrans.lConnection, cTrans.lTransaction)

                                        Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(cTrans.lConnection, cTrans.lTransaction)
                                        Operador.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                        Operador.Idinvencreconteo = 0
                                        Operador.Idubic = vUbicacion
                                        Operador.IdBodega = gBeTransInvEnc.IdBodega
                                        Operador.Idoperador = Operador.Idoperador

                                        If Not clsLnTrans_inv_operador.Existe_Ubicacion_By_IdOperador(Operador, cTrans.lConnection, cTrans.lTransaction) Then
                                            clsLnTrans_inv_operador.Insertar(Operador, cTrans.lConnection, cTrans.lTransaction)
                                        End If

                                    Next

                                    'Dim Ciclico As New clsBeTrans_inv_ciclico
                                    'Ciclico.Idoperador = cmbOperador.EditValue
                                    'Ciclico.IdUbicacion = NAsignacion.Tag
                                    'Ciclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                    'Ciclico.IdProductoBodega = vIdProductoBodega
                                    ''clsLnTrans_inv_ciclico.Actualizar_By_IdProducto(Ciclico, cTrans.lConnection, cTrans.lTransaction)

                                End If

                            Catch ex As Exception
                                Throw
                            End Try

                        End If


                    Next
                End If

            Next


            Inserta_Operadores = True

            cTrans.Commit_Transaction()

        Catch ex As Exception
            cTrans.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cTrans.Close_Conection()
        End Try

    End Function

    Private Function Inserta_Operadores_anterior() As Boolean

        Inserta_Operadores_anterior = False

        Dim Ubicaciones As New List(Of clsBeTrans_inv_ciclico_ubic)
        Dim cTrans As New clsTransaccion()
        Dim InvCiclico As New clsBeTrans_inv_ciclico
        Dim BeTransInvCiclicoUbic As New clsBeTrans_inv_ciclico_ubic

        Try

            Dim NuevoStock As New clsBeTrans_inv_ciclico
            Dim DTOperador As New DataTable
            Dim Ciclico As New List(Of clsBeTrans_inv_ciclico)
            Dim Operador As New clsBeTrans_inv_operador

            cTrans.Open_Connection() : cTrans.Begin_Transaction()

            Ciclico = clsLnTrans_inv_ciclico.Get_All_By_IdInventario(gBeTransInvEnc.Idinventarioenc,
                                                                     cTrans.lConnection,
                                                                     cTrans.lTransaction)

            DTOperador = clsLnOperador.Get_All_For_Combo_By_Rol_Inventario(AP.IdBodega,
                                                                           cTrans.lConnection,
                                                                           cTrans.lTransaction)

            For Each BeTransInvCiclico As clsBeTrans_inv_ciclico In Ciclico

                For Each op As DataRow In DTOperador.Rows

                    Dim vIdProductoBodega As Integer = BeTransInvCiclico.IdProductoBodega
                    Dim vIdStock As Integer = BeTransInvCiclico.IdStock
                    Dim vUbicacion As Integer = 0

                    vUbicacion = BeTransInvCiclico.IdUbicacion

                    Operador = New clsBeTrans_inv_operador
                    Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(cTrans.lConnection, cTrans.lTransaction)
                    Operador.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                    Operador.Idinvencreconteo = 0
                    Operador.Idubic = vUbicacion
                    Operador.Idoperador = op.Item("IdOperador")
                    Operador.IdBodega = AP.IdBodega

                    If clsLnTrans_inv_operador.Existe_Ubicacion_By_IdOperador(Operador,
                                                                              cTrans.lConnection,
                                                                              cTrans.lTransaction) Then

                        If Not clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador(Operador.Idoperador,
                                                                                    gBeTransInvEnc.Idinventarioenc,
                                                                                    vIdProductoBodega,
                                                                                    vIdStock,
                                                                                    vUbicacion,
                                                                                    cTrans.lConnection,
                                                                                    cTrans.lTransaction) Then

                            InvCiclico = New clsBeTrans_inv_ciclico
                            InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(cTrans.lConnection,
                                                                                       cTrans.lTransaction)
                            InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                            InvCiclico.IdStock = BeTransInvCiclico.IdStock
                            InvCiclico.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                            InvCiclico.IdProductoEstado = BeTransInvCiclico.IdProductoEstado
                            InvCiclico.IdProductoEst_nuevo = BeTransInvCiclico.IdProductoEstado
                            InvCiclico.IdUbicacion = BeTransInvCiclico.IdUbicacion
                            InvCiclico.IdPresentacion = BeTransInvCiclico.IdPresentacion
                            InvCiclico.EsNuevo = False
                            InvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock
                            InvCiclico.Lote = BeTransInvCiclico.Lote
                            InvCiclico.Fecha_vence_stock = BeTransInvCiclico.Fecha_vence_stock
                            InvCiclico.Fecha_vence = BeTransInvCiclico.Fecha_vence
                            InvCiclico.Cant_stock = BeTransInvCiclico.Cant_stock
                            InvCiclico.Cantidad = BeTransInvCiclico.Cantidad
                            InvCiclico.Cant_reconteo = BeTransInvCiclico.Cant_reconteo
                            InvCiclico.Peso_stock = BeTransInvCiclico.Peso_stock
                            InvCiclico.Peso = BeTransInvCiclico.Peso
                            InvCiclico.Peso_reconteo = BeTransInvCiclico.Peso_reconteo
                            InvCiclico.Idoperador = Operador.Idoperador
                            InvCiclico.User_agr = AP.UsuarioAp.Nombres
                            InvCiclico.Fec_agr = Now
                            InvCiclico.EsPallet = BeTransInvCiclico.EsPallet
                            InvCiclico.lic_plate = BeTransInvCiclico.lic_plate
                            InvCiclico.IdBodega = AP.IdBodega
                            InvCiclico.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                            InvCiclico.Cantidad_Reservada_UMBas = BeTransInvCiclico.Cantidad_Reservada_UMBas

                            clsLnTrans_inv_ciclico.Insertar(InvCiclico,
                                                                cTrans.lConnection,
                                                                cTrans.lTransaction)

                            If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(BeTransInvCiclico.IdUbicacion,
                                                                                    BeTransInvCiclico.Idinventarioenc,
                                                                                    cTrans.lConnection,
                                                                                    cTrans.lTransaction) Then

                                BeTransInvCiclicoUbic = New clsBeTrans_inv_ciclico_ubic
                                BeTransInvCiclicoUbic.Idinventarioenc = IdInventario
                                BeTransInvCiclicoUbic.Idubicacion = BeTransInvCiclico.IdUbicacion
                                BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                                Ubicaciones.Add(BeTransInvCiclicoUbic)

                            End If

                        Else

                            Try

                                If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(vUbicacion,
                                                                                gBeTransInvEnc.Idinventarioenc,
                                                                                cTrans.lConnection,
                                                                                cTrans.lTransaction) Then

                                    Dim Ubicacion As New clsBeTrans_inv_ciclico_ubic
                                    Ubicacion.Idubicacion = vUbicacion
                                    Ubicacion.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                    Ubicacion.IdBodega = AP.IdBodega
                                    Ubicaciones.Add(Ubicacion)

                                End If

                                If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(BeTransInvCiclico.IdUbicacion,
                                                                                    gBeTransInvEnc.Idinventarioenc,
                                                                                    cTrans.lConnection,
                                                                                    cTrans.lTransaction) Then

                                    BeTransInvCiclicoUbic = New clsBeTrans_inv_ciclico_ubic
                                    BeTransInvCiclicoUbic.Idinventarioenc = IdInventario
                                    BeTransInvCiclicoUbic.Idubicacion = BeTransInvCiclico.IdUbicacion
                                    BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                                    clsLnTrans_inv_ciclico_ubic.Insertar(BeTransInvCiclicoUbic,
                                                                         cTrans.lConnection,
                                                                         cTrans.lTransaction)

                                End If

                                If Not clsLnTrans_inv_operador.Existe_Ubicacion_By_IdOperador(Operador,
                                                                                              cTrans.lConnection,
                                                                                              cTrans.lTransaction) Then
                                    clsLnTrans_inv_operador.Insertar(Operador,
                                                                     cTrans.lConnection,
                                                                     cTrans.lTransaction)
                                End If

                            Catch ex As Exception
                                Throw
                            End Try

                        End If

                    Else

                        clsLnTrans_inv_operador.Insertar(Operador,
                                                         cTrans.lConnection,
                                                         cTrans.lTransaction)

                        Try

                            If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(vUbicacion,
                                                                                gBeTransInvEnc.Idinventarioenc,
                                                                                cTrans.lConnection,
                                                                                cTrans.lTransaction) Then

                                BeTransInvCiclicoUbic = New clsBeTrans_inv_ciclico_ubic
                                BeTransInvCiclicoUbic.Idinventarioenc = IdInventario
                                BeTransInvCiclicoUbic.Idubicacion = BeTransInvCiclico.IdUbicacion
                                BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                                clsLnTrans_inv_ciclico_ubic.Insertar(BeTransInvCiclicoUbic,
                                                                     cTrans.lConnection,
                                                                     cTrans.lTransaction)

                            End If

                            If Not clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador(Operador.Idoperador,
                                                                                        gBeTransInvEnc.Idinventarioenc,
                                                                                        vIdProductoBodega,
                                                                                        vIdStock,
                                                                                        vUbicacion,
                                                                                        cTrans.lConnection,
                                                                                        cTrans.lTransaction) Then
                                InvCiclico = New clsBeTrans_inv_ciclico
                                InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(cTrans.lConnection, cTrans.lTransaction)
                                InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                InvCiclico.IdStock = BeTransInvCiclico.IdStock
                                InvCiclico.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                                InvCiclico.IdProductoEstado = BeTransInvCiclico.IdProductoEstado
                                InvCiclico.IdProductoEst_nuevo = BeTransInvCiclico.IdProductoEstado
                                InvCiclico.IdUbicacion = BeTransInvCiclico.IdUbicacion
                                InvCiclico.IdPresentacion = BeTransInvCiclico.IdPresentacion
                                InvCiclico.EsNuevo = False
                                InvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock
                                InvCiclico.Lote = BeTransInvCiclico.Lote
                                InvCiclico.Fecha_vence_stock = BeTransInvCiclico.Fecha_vence_stock
                                InvCiclico.Fecha_vence = BeTransInvCiclico.Fecha_vence
                                InvCiclico.Cant_stock = BeTransInvCiclico.Cant_stock
                                InvCiclico.Cantidad = BeTransInvCiclico.Cantidad
                                InvCiclico.Cant_reconteo = BeTransInvCiclico.Cant_reconteo
                                InvCiclico.Peso_stock = BeTransInvCiclico.Peso_stock
                                InvCiclico.Peso = BeTransInvCiclico.Peso
                                InvCiclico.Peso_reconteo = BeTransInvCiclico.Peso_reconteo
                                InvCiclico.Idoperador = Operador.Idoperador
                                InvCiclico.User_agr = AP.UsuarioAp.Nombres
                                InvCiclico.Fec_agr = Now
                                InvCiclico.EsPallet = BeTransInvCiclico.EsPallet
                                InvCiclico.lic_plate = BeTransInvCiclico.lic_plate
                                InvCiclico.IdBodega = AP.IdBodega
                                InvCiclico.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                                InvCiclico.Cantidad_Reservada_UMBas = BeTransInvCiclico.Cantidad_Reservada_UMBas

                                clsLnTrans_inv_ciclico.Insertar(InvCiclico,
                                                                        cTrans.lConnection,
                                                                        cTrans.lTransaction)

                            End If

                        Catch ex As Exception
                            Throw
                        End Try

                    End If

                Next

            Next

            Inserta_Operadores_anterior = True

            cTrans.Commit_Transaction()

        Catch ex As Exception
            cTrans.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cTrans.Close_Conection()
        End Try

    End Function

    Private Sub cmdQuitarProducto_Click(sender As Object, e As EventArgs) Handles cmdQuitarProducto.Click

        If XtraMessageBox.Show("¿Eliminar los productos asociados al inventario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Eliminando productos asignados...")
            If Eliminar_Producto_Asignado() Then
                SplashScreenManager.CloseForm()
                XtraMessageBox.Show("Asignación de producto eliminada", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar_Productos()
                Carga_Detalle_Ciclico()
                Listar_Bodega()
            Else
                XtraMessageBox.Show("No fue posible eliminar el producto", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If

    End Sub

    Private Function Eliminar_Producto_Asignado() As Boolean

        Dim Operador As New clsBeTrans_inv_operador
        Dim InvCiclicoUbic As New clsBeTrans_inv_ciclico_ubic
        Dim IdProductoBodega As Integer
        Dim Ubicaciones As New List(Of Integer)

        Eliminar_Producto_Asignado = False

        Try

            Dim gBeInvCiclico As New clsBeTrans_inv_ciclico

            For Each Op As TreeListNode In dgridAsignacionProductos.Nodes

                If Op.Checked Then

                    IdProductoBodega = Op.Tag

                    clsLnTrans_inv_ciclico_ubic.Elimina_UbicacionesByIdUbicacion(gBeTransInvEnc.Idinventarioenc, IdProductoBodega)

                    clsLnTrans_inv_operador.Eliminar_IdUbicacion_By_IdOperador_And_IdProductoBodega(gBeTransInvEnc.Idinventarioenc, Op.Item("IdOperador"), IdProductoBodega)

                    clsLnTrans_inv_ciclico.Eliminar_By_IdProductoBodega(IdProductoBodega, gBeTransInvEnc.Idinventarioenc)

                    Eliminar_Producto_Asignado = True

                End If

            Next

        Catch ex As Exception
            SplashScreenManager.CloseForm()
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdEliminarOpProd_Click(sender As Object, e As EventArgs) Handles cmdEliminarOpProd.Click

        Try
            If XtraMessageBox.Show("¿Eliminar todos los operadores?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Elimina_Operadores() Then
                    XtraMessageBox.Show("Operadores Eliminados", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Listar_Productos()
                    Carga_Detalle_Ciclico()
                    Listar_Bodega()
                End If
            Else
                If EliminaOperadorProductos() Then
                    Listar_Productos()
                    Carga_Detalle_Ciclico()
                    Listar_Bodega()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Elimina_Operadores() As Boolean

        Dim clsTrans As New clsTransaccion

        Elimina_Operadores = False

        Try

            Dim Operador As New List(Of clsBeTrans_inv_operador)
            Dim Ciclico As New List(Of clsBeTrans_inv_ciclico)
            Dim BuscaId As New List(Of clsBeTrans_inv_ciclico)
            Dim vIdProductoBodega As Integer = 0
            Dim vIdUbicacion As Integer = 0
            Dim vIdOperador As Integer = 0

            Ciclico = clsLnTrans_inv_ciclico.Get_All_By_IdInventario(gBeTransInvEnc.Idinventarioenc)

            clsTrans.Begin_Transaction()

            For Each BeTransInvCic As clsBeTrans_inv_ciclico In Ciclico

                vIdProductoBodega = BeTransInvCic.IdProductoBodega
                vIdUbicacion = BeTransInvCic.IdUbicacion
                vIdOperador = BeTransInvCic.Idoperador

                If clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador(vIdOperador,
                                                                        gBeTransInvEnc.Idinventarioenc,
                                                                        vIdProductoBodega,
                                                                        clsTrans.lConnection,
                                                                        clsTrans.lTransaction) Then

                    clsLnTrans_inv_ciclico_ubic.Elimina_Ubicaciones_By_IdUbicacion_And_IdOperador(gBeTransInvEnc.Idinventarioenc,
                                                                                                  vIdProductoBodega,
                                                                                                  vIdOperador,
                                                                                                  clsTrans.lConnection,
                                                                                                  clsTrans.lTransaction)

                    clsLnTrans_inv_operador.Eliminar_IdUbicacion_By_IdUbicacion_And_IdOperador_And_IdProductoBodega(gBeTransInvEnc.Idinventarioenc,
                                                                                                                    vIdOperador,
                                                                                                                    vIdProductoBodega,
                                                                                                                    vIdUbicacion,
                                                                                                                    clsTrans.lConnection,
                                                                                                                    clsTrans.lTransaction)

                    Dim vTransInvCiclico As New clsBeTrans_inv_ciclico
                    vTransInvCiclico.IdUbicacion = vIdUbicacion
                    vTransInvCiclico.IdProductoBodega = vIdProductoBodega
                    vTransInvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                    vTransInvCiclico.Idoperador = vIdOperador

                    clsLnTrans_inv_ciclico.Eliminar_By_IdOperador_And_IdProductoBodega_And_IdUbicacion(vTransInvCiclico,
                                                                                                       clsTrans.lConnection,
                                                                                                       clsTrans.lTransaction)

                End If

            Next

            clsTrans.Commit_Transaction()

            Elimina_Operadores = True

        Catch ex As Exception
            If Not clsTrans Is Nothing Then clsTrans.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            clsTrans.Close_Conection()
        End Try

    End Function

    Private Function EliminaOperadorProductos() As Boolean

        EliminaOperadorProductos = False

        Try

            Dim gBeInvCiclico As New clsBeTrans_inv_ciclico

            For Each Op As TreeListNode In dgridAsignacionProductos.Nodes

                If Op.Checked Then

                    'gBeInvCiclico.IdStock = Op.Tag
                    gBeInvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                    gBeInvCiclico.IdProductoBodega = Op.Tag
                    gBeInvCiclico.Idoperador = cmbOperador.EditValue

                    If clsLnTrans_inv_ciclico.ObtenerByStockEliminarByIdOperador(gBeInvCiclico) Then

                        clsLnTrans_inv_ciclico.GetSingleByStock(gBeInvCiclico)

                        gBeInvCiclico.Idoperador = 0

                        clsLnTrans_inv_ciclico.Actualizar(gBeInvCiclico)

                    Else

                        Dim Operador As New frmEliminOperadores()
                        Operador.Modo = frmEliminOperadores.TipoTrans.Eliminar
                        clsLnTrans_inv_ciclico.GetSingleByStock(gBeInvCiclico)
                        Operador.gBeCiclico = gBeInvCiclico
                        Operador.ShowDialog()
                        Operador.Dispose()

                    End If

                End If

            Next

            EliminaOperadorProductos = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub Carga_Detalle_Ciclico(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Dim CantidadUMBas As Double = 0
        Dim Cantidad_Contada_Pres As Double = 0
        Dim CantStockUM As Double = 0
        Dim Cantidad_Teorica_Stock_Pres As Double = 0
        Dim CantReUM As Double = 0
        Dim Cantidad_Reconteo_Pres As Double = 0
        Dim AbrioWaitForm As Boolean = False
        Dim vDiferencia As Double = 0
        Dim EstadoNuevo As String = ""
        Dim UbicacionNueva As String = ""

        Dim clsTrans As New clsTransaccion

        Try


            Dim Extraviado As Double = 0.0
            Dim Reconteo As New clsBeTrans_inv_reconteo
            Dim Ubicacion As New clsBeBodega_ubicacion

            ListInventarioCiclico.Clear()

            DTInventarioCiclico.Clear()

            dgridInventarioCiclico.DataSource = Nothing

            Application.DoEvents()

            ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc,
                                                                                                        AP.IdBodega,
                                                                                                        lConnection,
                                                                                                        lTransaction)

            If pIdPropietario > 0 Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdPropietario = pIdPropietario)
            End If

            If txtIdFamilia.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdFamilia = txtIdFamilia.Text)
            End If

            If txtIdClasificacion.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdClasificacion = txtIdClasificacion.Text)
            End If

            If txtIdProducto.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.Codigo = txtIdProducto.Text)
            End If

            If txtIdUbicacion.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdUbicacion = txtIdUbicacion.Text)
            End If

            If txtIdTramo.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdTramo = txtIdTramo.Text)
            End If

            If txtIdOperador.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.Idoperador = txtIdOperador.Text)
            End If

            If ListInventarioCiclico.Count > 0 Then

                prgPanInvConteo.Visible = True
                prgPanInvConteo.Maximum = ListInventarioCiclico.Count

                Dim vContador As Integer = 0

                If SplashScreenManager.Default Is Nothing Then
                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Cargando datos..")
                    AbrioWaitForm = True
                End If

                'cTrans.Begin_Transaction()
                Application.DoEvents()

                Dim BeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

                For Each BeTransInvCiclico As clsBeTrans_inv_ciclico In ListInventarioCiclico

                    Ubicacion.IdUbicacion = BeTransInvCiclico.IdUbicacion

                    EstadoNuevo = BeTransInvCiclico.EstadoNuevo

                    UbicacionNueva = ""

                    If BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then
                        UbicacionNueva = BeTransInvCiclico.Ubicacion_Nueva 'clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(BeTransInvCiclico.IdUbicacion_nuevo,
                        '                                                                            AP.IdBodega,
                        '                                                                            lConnection,
                        '                                                                            lTransaction)
                    End If

                    If BeTransInvCiclico.EsNuevo Then

                        Extraviado = BeTransInvCiclico.Cantidad
                        '#CKFK20250730 Aunque sea nuevo la cantidad de stock no se coloca en 0 porque si existe en el congelado
                        'BeTransInvCiclico.Cant_stock = 0
                        'BeTransInvCiclico.Cant_reconteo = 0
                        'BeTransInvCiclico.Peso_stock = 0
                        'BeTransInvCiclico.Peso_reconteo = 0

                    Else

                        Reconteo.Idinventarioenc = BeTransInvCiclico.Idinventarioenc
                        Reconteo.IdOperador = BeTransInvCiclico.Idoperador
                        Reconteo.IdStock = BeTransInvCiclico.IdStock
                        Reconteo.IdUbicacionAnterior = BeTransInvCiclico.IdUbicacion
                        Reconteo.IdProductoBodega = BeTransInvCiclico.IdProductoBodega

                        If BeTransInvCiclico.IdStock > 0 Then

                            'If clsLnTrans_inv_reconteo.Obtener_By_Ubicacion(Reconteo,
                            '                                                lConnection,
                            '                                                lTransaction) Then
                            '    'BeTransInvCiclico.EstadoNuevo = gBeTransInvEnc.ide
                            '    BeTransInvCiclico.Cant_reconteo = Reconteo.Cantidad
                            '    BeTransInvCiclico.Peso_reconteo = Reconteo.Peso
                            'End If

                        End If

                        Extraviado = 0

                    End If

                    CantidadUMBas = 0
                    Cantidad_Contada_Pres = 0
                    Cantidad_Teorica_Stock_Pres = 0
                    CantStockUM = 0
                    Cantidad_Reconteo_Pres = 0
                    CantReUM = 0

                    If BeTransInvCiclico.IdPresentacion > 0 Then
                        '#EJC20180821: Desde la hermana república del salvador, con ustedes, los cálculos.
                        Cantidad_Contada_Pres = Math.Round(BeTransInvCiclico.Cantidad / BeTransInvCiclico.Factor, 6)
                        Cantidad_Teorica_Stock_Pres = Math.Round(BeTransInvCiclico.Cant_stock / BeTransInvCiclico.Factor, 6)
                        Cantidad_Reconteo_Pres = Math.Round(BeTransInvCiclico.Cant_reconteo / BeTransInvCiclico.Factor, 6)
                        CantidadUMBas = BeTransInvCiclico.Cantidad
                        CantStockUM = BeTransInvCiclico.Cant_stock
                        CantReUM = BeTransInvCiclico.Cant_reconteo
                    Else
                        CantidadUMBas = BeTransInvCiclico.Cantidad
                        CantStockUM = BeTransInvCiclico.Cant_stock
                        CantReUM = BeTransInvCiclico.Cant_reconteo
                    End If

                    vDiferencia = (CantStockUM - CantidadUMBas)

                    DTInventarioCiclico.Rows.Add(BeTransInvCiclico.IdInvCiclico,
                                                  BeTransInvCiclico.Ubicacion,
                                                  UbicacionNueva,
                                                  BeTransInvCiclico.IdStock,
                                                  BeTransInvCiclico.Codigo,
                                                  BeTransInvCiclico.Producto,
                                                  BeTransInvCiclico.TipoProducto,
                                                  BeTransInvCiclico.Presentacion,
                                                  BeTransInvCiclico.Estado,
                                                  EstadoNuevo,
                                                  BeTransInvCiclico.Lote_stock,
                                                  BeTransInvCiclico.Lote,
                                                  BeTransInvCiclico.Fecha_vence_stock,
                                                  BeTransInvCiclico.Fecha_vence,
                                                  Cantidad_Teorica_Stock_Pres,
                                                  CantStockUM,
                                                  BeTransInvCiclico.Peso_stock,
                                                  Cantidad_Contada_Pres,
                                                  CantidadUMBas,
                                                  BeTransInvCiclico.Peso,
                                                  Cantidad_Reconteo_Pres,
                                                  CantReUM,
                                                  BeTransInvCiclico.Peso_reconteo,
                                                  vDiferencia * -1,
                                                  Extraviado,
                                                  BeTransInvCiclico.Idinventarioenc,
                                                  BeTransInvCiclico.IdProductoBodega,
                                                  IIf(BeTransInvCiclico.lic_plate = "", "", BeTransInvCiclico.lic_plate),
                                                  BeTransInvCiclico.Cantidad_Reservada_UMBas,
                                                  BeTransInvCiclico.Contado)

                    SplashScreenManager.Default.SetWaitFormDescription(vContador & " de: " & ListInventarioCiclico.Count)

                    prgPanInvConteo.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Next

                'cTrans.Commit_Transaction()

                dgridInventarioCiclico.DataSource = DTInventarioCiclico

                If gdviewTeorico.RowCount > 0 Then

                    gdviewTeorico.Columns("IdInventario").Visible = False
                    gdviewTeorico.Columns("IdProductoBodega").Visible = False
                    gdviewTeorico.Columns("IdInvCiclico").Visible = False

                    gdviewTeorico.OptionsView.ShowFooter = True

                    '#EJC20180830_0540PM: Hot fix para funcionalidad de tablet
                    gdviewTeorico.Columns("Código").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                    gdviewTeorico.Columns("Cant.Teorica.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Teorica.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Teorica.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Teorica.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Teorica.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Conteo.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Conteo.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Conteo.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Conteo.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Reconteo.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Reconteo.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoReconteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoReconteo").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoReconteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoReconteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Extraviado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Extraviado").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Extraviado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Extraviado").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Dif.Cant.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Dif.Cant.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Reservada").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Ubicación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    gdviewTeorico.Columns("Ubicación").SummaryItem.DisplayFormat = "Registros: {0}"

                    gdviewTeorico.Columns("PesoReconteo").Visible = False
                    gdviewTeorico.Columns("PesoConteo").Visible = False
                    gdviewTeorico.Columns("PesoStock").Visible = False

                    gdviewTeorico.BestFitColumns()

                End If

            End If

            Application.DoEvents()

            Actualiza_KPIS(lConnection, lTransaction)

        Catch ex As Exception
            Try
                'cTrans.RollBack_Transaction()
            Catch ex1 As Exception
            End Try
            'SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            prgPanInvConteo.Value = 0
            prgPanInvConteo.Visible = False
        End Try

    End Sub


    '#GT17012025: Una copia de Cargar_Detalle_Ciclico pero sin las agrupaciones de producto, tipo entre otros, se requiere solo visualizar UMBAS para la CUMBRE
    Private Sub Carga_Detalle_Diferencias_Ciclico(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Dim CantidadUMBas As Double = 0
        Dim Cantidad_Contada_Pres As Double = 0
        Dim CantStockUM As Double = 0
        Dim Cantidad_Teorica_Stock_Pres As Double = 0
        Dim CantReUM As Double = 0
        Dim Cantidad_Reconteo_Pres As Double = 0
        Dim AbrioWaitForm As Boolean = False
        Dim vDiferencia As Double = 0
        Dim EstadoNuevo As String = ""
        Dim UbicacionNueva As String = ""

        Dim clsTrans As New clsTransaccion

        Try


            Dim Extraviado As Double = 0.0
            Dim Reconteo As New clsBeTrans_inv_reconteo
            Dim Ubicacion As New clsBeBodega_ubicacion

            ListInventarioDiferenciaCiclico.Clear()

            DTInventarioDiferenciaCiclico.Clear()

            dgridDiferenciasCiclico.DataSource = Nothing

            Application.DoEvents()

            ListInventarioDiferenciaCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc_Diferencias(gBeTransInvEnc.Idinventarioenc,
                                                                                                                               AP.IdBodega,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)

            If ListInventarioDiferenciaCiclico.Count > 0 Then

                prgPanInvConteo.Visible = True
                prgPanInvConteo.Maximum = ListInventarioDiferenciaCiclico.Count

                Dim vContador As Integer = 0

                If SplashScreenManager.Default Is Nothing Then
                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Cargando datos..")
                    AbrioWaitForm = True
                End If

                Application.DoEvents()

                Dim BeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

                For Each BeTransInvCiclico As clsBeTrans_inv_ciclico In ListInventarioDiferenciaCiclico

                    Ubicacion.IdUbicacion = BeTransInvCiclico.IdUbicacion

                    EstadoNuevo = BeTransInvCiclico.EstadoNuevo

                    UbicacionNueva = ""

                    If BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then
                        UbicacionNueva = BeTransInvCiclico.Ubicacion_Nueva
                    End If

                    If BeTransInvCiclico.EsNuevo Then

                        Extraviado = BeTransInvCiclico.Cantidad
                        BeTransInvCiclico.Cant_stock = 0
                        BeTransInvCiclico.Cant_reconteo = 0
                        BeTransInvCiclico.Peso_stock = 0
                        BeTransInvCiclico.Peso_reconteo = 0

                    Else

                        Reconteo.Idinventarioenc = BeTransInvCiclico.Idinventarioenc
                        Extraviado = 0

                    End If

                    CantidadUMBas = 0
                    Cantidad_Contada_Pres = 0
                    Cantidad_Teorica_Stock_Pres = 0
                    CantStockUM = 0
                    Cantidad_Reconteo_Pres = 0
                    CantReUM = 0

                    If BeTransInvCiclico.IdPresentacion > 0 Then
                        '#EJC20180821: Desde la hermana república del salvador, con ustedes, los cálculos.
                        Cantidad_Contada_Pres = Math.Round(BeTransInvCiclico.Cantidad / BeTransInvCiclico.Factor, 6)
                        Cantidad_Teorica_Stock_Pres = Math.Round(BeTransInvCiclico.Cant_stock / BeTransInvCiclico.Factor, 6)
                        Cantidad_Reconteo_Pres = Math.Round(BeTransInvCiclico.Cant_reconteo / BeTransInvCiclico.Factor, 6)
                        CantidadUMBas = BeTransInvCiclico.Cantidad
                        CantStockUM = BeTransInvCiclico.Cant_stock
                        CantReUM = BeTransInvCiclico.Cant_reconteo
                    Else
                        CantidadUMBas = BeTransInvCiclico.Cantidad
                        CantStockUM = BeTransInvCiclico.Cant_stock
                        CantReUM = BeTransInvCiclico.Cant_reconteo
                    End If

                    vDiferencia = (CantStockUM - CantidadUMBas)

                    DTInventarioDiferenciaCiclico.Rows.Add(BeTransInvCiclico.IdInvCiclico,
                                                           BeTransInvCiclico.Idinventarioenc,
                                                           BeTransInvCiclico.Codigo,
                                                           BeTransInvCiclico.Producto,
                                                           BeTransInvCiclico.TipoProducto,
                                                           CantStockUM,
                                                           CantidadUMBas,
                                                           CantReUM,
                                                           vDiferencia * -1,
                                                           "",
                                                           BeTransInvCiclico.IdProductoBodega,
                                                           BeTransInvCiclico.Cantidad_Reservada_UMBas)

                    SplashScreenManager.Default.SetWaitFormDescription(vContador & " de: " & ListInventarioDiferenciaCiclico.Count)

                    prgPanInvConteo.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Next

                dgridDiferenciasCiclico.DataSource = DTInventarioDiferenciaCiclico

                If gvDiferenciasCiclico.RowCount > 0 Then

                    gvDiferenciasCiclico.Columns("IdInventario").Visible = False
                    gvDiferenciasCiclico.Columns("IdProductoBodega").Visible = False
                    gvDiferenciasCiclico.Columns("IdInvCiclico").Visible = False

                    gvDiferenciasCiclico.OptionsView.ShowFooter = True

                    '#EJC20180830_0540PM: Hot fix para funcionalidad de tablet
                    gvDiferenciasCiclico.Columns("Código").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                    gvDiferenciasCiclico.Columns("Cant.Teorica.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvDiferenciasCiclico.Columns("Cant.Teorica.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Cant.Teorica.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvDiferenciasCiclico.Columns("Cant.Teorica.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Cant.Conteo.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvDiferenciasCiclico.Columns("Cant.Conteo.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Cant.Conteo.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvDiferenciasCiclico.Columns("Cant.Conteo.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Cant.Reconteo.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvDiferenciasCiclico.Columns("Cant.Reconteo.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Cant.Reconteo.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvDiferenciasCiclico.Columns("Cant.Reconteo.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvDiferenciasCiclico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Dif.Cant.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvDiferenciasCiclico.Columns("Dif.Cant.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Cant.Reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvDiferenciasCiclico.Columns("Cant.Reservada").DisplayFormat.FormatString = "{0:n6}"

                    gvDiferenciasCiclico.Columns("Cant.Reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvDiferenciasCiclico.Columns("Cant.Reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    gvDiferenciasCiclico.BestFitColumns()

                End If

            End If

            Application.DoEvents()

            Actualiza_KPIS(lConnection, lTransaction)

        Catch ex As Exception
            Try
                'cTrans.RollBack_Transaction()
            Catch ex1 As Exception
            End Try
            'SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            prgPanInvConteo.Value = 0
            prgPanInvConteo.Visible = False
        End Try

    End Sub



    Private Sub Cargar_Productos_No_Existentes(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            Dim DT As New DataTable("NoExistentes")
            DT = clsLnTrans_inv_ne.GetAllByInventario(gBeTransInvEnc.Idinventarioenc, lConnection, lTransaction)

            If DT.Rows.Count > 0 Then

                grdNE.DataSource = DT

                GridView10.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView10.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView10.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView10.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView10.BestFitColumns(True)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Actualiza_KPIS()

        Try

            ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(gBeTransInvEnc.Idinventarioenc, AP.IdBodega)

            If Not ListInventarioCiclico Is Nothing Then

                Get_KPI_Porcentaje_Registros_Contados()

                Get_KPI_Universo_Vrs_Muestra()

                Get_KPI_Conteo_Por_Estrato_Tipo()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_KPI_Porcentaje_Registros_Contados()

        Try

            If ListInventarioCiclico Is Nothing Then Exit Sub

            '#EJC20180830_0124AM: BI
            Dim RegistrosContados As Integer = (From p In ListInventarioCiclico).Where(Function(x) x.Cantidad <> 0).Count()
            ArcScaleComponent1.MinValue = 1
            ArcScaleComponent1.MaxValue = 100

            Dim Z As Double = 0

            '#EJC20180830_0124AM: R3
            If RegistrosContados > 0 Then
                Z = (RegistrosContados * 100) / ListInventarioCiclico.Count
                Z = Math.Round(Z, 2)
                ArcScaleComponent1.Value = Z
            Else
                ArcScaleComponent1.Value = Z
            End If

            ''#EJC20180830_0124AM: Indicador de conteo
            lblRegsCont.Text = "Registros: " & RegistrosContados & "/" & ListInventarioCiclico.Count
            lblRegistrosContados.Text = Z & "% registros contados"

            ''#EJC20180830_0124AM: Barras
            Dim series1 As New DevExpress.XtraCharts.Series("Universo", ViewType.Bar)
            series1.Points.Add(New SeriesPoint("Universo", DTInventarioCongelado.Rows.Count))
            series1.Points.Add(New SeriesPoint("Muestra", ListInventarioCiclico.Count))
            series1.ValueScaleType = ScaleType.Numerical

            Dim title = New ChartTitle() With {.Text = "Universo Vrs. <br> Muestra de inventario"}
            charcUniverso.Titles.Clear()
            charcUniverso.Titles.Add(title)
            charcUniverso.Series.Clear()
            charcUniverso.Series.Add(series1)

            Dim diagram = (CType(charcUniverso.Diagram, XYDiagram))
            diagram.AxisY.Title.Text = "Registros de Stock"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_KPI_Universo_Vrs_Muestra()

        Try

            Dim series1 As New DevExpress.XtraCharts.Series("Universo", ViewType.Bar)
            series1.Points.Add(New SeriesPoint("Universo", DTInventarioCongelado.Rows.Count))
            series1.Points.Add(New SeriesPoint("Muestra", ListInventarioCiclico.Count))
            series1.ValueScaleType = ScaleType.Numerical

            Dim title1 = New ChartTitle() With {.Text = "Universo Vrs. <br> Muestra de inventario"}
            charcUniverso.Titles.Clear()
            charcUniverso.Titles.Add(title1)
            charcUniverso.Series.Clear()
            charcUniverso.Series.Add(series1)

            Dim diagram2 = (CType(charcUniverso.Diagram, XYDiagram))
            diagram2.AxisY.Title.Text = "Registros de Stock"
            diagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub linklblFamilia_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklblFamilia.LinkClicked

        Try

            Dim Familia As New frmProducto_FamiliaList() With {.pIdPropietario = cmbPropietario.EditValue, .Modo = frmProducto_FamiliaList.pModo.Seleccion}
            Familia.ShowDialog()

            If Familia.pObjPF IsNot Nothing AndAlso Familia.pObjPF.IdFamilia <> 0 Then
                txtIdFamilia.Text = Familia.pObjPF.IdFamilia
                txtFamiliaNombre.Text = Familia.pObjPF.Nombre
            End If

            Familia.Close()
            Familia.Dispose()

            Carga_Detalle_Ciclico()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub linklblClasificacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklblClasificacion.LinkClicked

        Try

            Dim Clasificacion As New frmProducto_ClasificacionList() With {.pIdPropietario = cmbPropietario.EditValue, .Modo = frmProducto_ClasificacionList.pModo.Seleccion}
            Clasificacion.ShowDialog()

            If Clasificacion.pObjPC IsNot Nothing AndAlso Clasificacion.pObjPC.IdClasificacion <> 0 Then
                txtIdClasificacion.Text = Clasificacion.pObjPC.IdClasificacion
                txtClasificacionNombre.Text = Clasificacion.pObjPC.Nombre
            End If

            Clasificacion.Close()
            Clasificacion.Dispose()

            Carga_Detalle_Ciclico()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub linklblTramo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklblTramo.LinkClicked

        Try

            Dim Tramo As New frmBodegaTramo_List() With {.pIdInventario = gBeTransInvEnc.Idinventarioenc, .Modo = frmBodegaTramo_List.pModo.Inventario}
            Tramo.ShowDialog()

            If Tramo.gBeBodegaTramo IsNot Nothing AndAlso Tramo.gBeBodegaTramo.IdTramo <> 0 Then
                txtIdTramo.Text = Tramo.gBeBodegaTramo.IdTramo
                txtNombreTramo.Text = Tramo.gBeBodegaTramo.Descripcion
            End If

            Tramo.Close()
            Tramo.Dispose()

            Carga_Detalle_Ciclico()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub LinklblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinklblProducto.LinkClicked

        Try

            Dim Productos As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Productos.ShowDialog()

            If Productos.pObjProducto IsNot Nothing AndAlso Productos.pObjProducto.IdProducto <> 0 Then

                txtIdProducto.Text = Productos.pObjProducto.Codigo
                txtNombreProducto.Text = Productos.pObjProducto.Nombre
            End If

            Carga_Detalle_Ciclico()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub LinklblUbicacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinklblUbicacion.LinkClicked

        Try

            Dim Ubicacion As New frmBodegaUbicacion_List() With
                   {.IdInventario = gBeTransInvEnc.Idinventarioenc,
                   .Modo = frmBodegaUbicacion_List.pModo.Inventario}
            Ubicacion.ShowDialog()

            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacion.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacion.Text = Ubicacion.pObj.Descripcion
            End If

            Ubicacion.Close()
            Ubicacion.Dispose()

            Carga_Detalle_Ciclico()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub LinklblOperador_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinklblOperador.LinkClicked

        Try

            Dim Operador As New frmOperador_List() With
                   {.Modo = frmBodegaUbicacion_List.pModo.Seleccion}
            Operador.ShowDialog()

            If Operador.pObj IsNot Nothing AndAlso Operador.pObj.IdOperador <> 0 Then
                txtIdOperador.Text = Operador.pObj.IdOperador
                txtNombreOperador.Text = Operador.pObj.Nombres
            End If

            Operador.Close()
            Operador.Dispose()

            Carga_Detalle_Ciclico()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdFamilia_Validated(sender As Object, e As EventArgs) Handles txtIdFamilia.Validated

        Try

            If String.IsNullOrEmpty(txtIdFamilia.Text.Trim()) = False AndAlso txtIdFamilia.Text > 0 Then

                Dim BeProductoFamilia As New clsBeProducto_familia

                BeProductoFamilia = clsLnProducto_familia.GetSingle(txtIdFamilia.Text.Trim(), cmbPropietario.EditValue)

                If BeProductoFamilia IsNot Nothing AndAlso BeProductoFamilia.IdFamilia > 0 Then
                    txtFamiliaNombre.Text = BeProductoFamilia.Nombre
                    Carga_Detalle_Ciclico()
                Else

                    XtraMessageBox.Show(String.Format("No existe Familia con código {0}.", txtIdFamilia.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdFamilia.Focus()
                    txtIdFamilia.SelectAll()

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

    Private Sub txtIdClasificacion_Validated(sender As Object, e As EventArgs) Handles txtIdClasificacion.Validated

        Try

            If String.IsNullOrEmpty(txtIdClasificacion.Text.Trim()) = False AndAlso txtIdClasificacion.Text > 0 Then

                Dim BeProductoClasificacion As New clsBeProducto_clasificacion

                BeProductoClasificacion = clsLnProducto_clasificacion.GetSingle(txtIdClasificacion.Text.Trim(), cmbPropietario.EditValue)

                If BeProductoClasificacion IsNot Nothing AndAlso BeProductoClasificacion.IdClasificacion > 0 Then
                    txtClasificacionNombre.Text = BeProductoClasificacion.Nombre
                    Carga_Detalle_Ciclico()
                Else
                    XtraMessageBox.Show(String.Format("No existe Clasificación con código {0}.", txtIdClasificacion.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdClasificacion.Focus()
                    txtIdClasificacion.SelectAll()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdTramo_Validated(sender As Object, e As EventArgs) Handles txtIdTramo.Validated

        Try

            If String.IsNullOrEmpty(txtIdTramo.Text.Trim()) = False AndAlso txtIdTramo.Text > "0" Then

                Dim BeBodegaTramo As New clsBeBodega_tramo

                BeBodegaTramo = clsLnBodega_tramo.GetSingleByInventario(gBeTransInvEnc.Idinventarioenc, txtIdTramo.Text, cmbBodega.EditValue)

                If BeBodegaTramo IsNot Nothing AndAlso BeBodegaTramo.IdTramo > 0 Then
                    txtNombreTramo.Text = Trim(String.Format("{0}", BeBodegaTramo.Descripcion))
                    Carga_Detalle_Ciclico()
                Else

                    XtraMessageBox.Show(String.Format("No existe tramo con código {0}", txtIdTramo.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdTramo.Focus()
                    txtIdTramo.SelectAll()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated

        Try

            Dim ProductoEspecifico As New clsBeProducto

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                    Carga_Detalle_Ciclico()
                Else
                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdProducto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdProducto.Focus()
                    txtIdProducto.SelectAll()
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdUbicacion_Validated(sender As Object, e As EventArgs) Handles txtIdUbicacion.Validated

        Try

            If String.IsNullOrEmpty(txtIdUbicacion.Text.Trim()) = False AndAlso txtIdUbicacion.Text > "0" Then

                Dim Obj As New clsBeBodega_ubicacion

                Obj = clsLnBodega_ubicacion.Get_Single_By_IdInventarioEnc(txtIdUbicacion.Text.Trim, gBeTransInvEnc.Idinventarioenc, cmbBodega.EditValue)

                If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                    txtNombreUbicacion.Text = Obj.Descripcion
                    Carga_Detalle_Ciclico()
                Else

                    XtraMessageBox.Show(String.Format("No existe ubicación con código {0}", txtIdUbicacion.Text.Trim(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                    txtIdUbicacion.Focus()
                    txtIdUbicacion.SelectAll()

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

    Private Sub txtIdOperador_Validated(sender As Object, e As EventArgs) Handles txtIdOperador.Validated

        Try

            If String.IsNullOrEmpty(txtIdOperador.Text.Trim()) = False AndAlso txtIdOperador.Text > "0" Then

                Dim BeOperador As New clsBeOperador
                BeOperador.IdOperador = txtIdOperador.Text.Trim
                clsLnOperador.Obtener(BeOperador)

                If BeOperador IsNot Nothing AndAlso BeOperador.IdOperador > 0 Then
                    txtNombreOperador.Text = BeOperador.Nombres
                    Carga_Detalle_Ciclico()
                Else

                    XtraMessageBox.Show(String.Format("No existe operador con código {0}", txtIdOperador.Text.Trim(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                    txtIdUbicacion.Focus()
                    txtIdUbicacion.SelectAll()

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

    Private Sub txtIdClasificacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdClasificacion.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdClasificacion.Text.Length = 1 Then
                txtClasificacionNombre.Text = String.Empty
            End If

            Carga_Detalle_Ciclico()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdFamilia_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdFamilia.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdFamilia.Text.Length = 1 Then
                txtFamiliaNombre.Text = String.Empty
            End If

            Carga_Detalle_Ciclico()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdOperador_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdOperador.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdOperador.Text.Length = 1 Then
                txtNombreOperador.Text = String.Empty
            End If

            Carga_Detalle_Ciclico()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdProducto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdProducto.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdProducto.Text.Length = 1 Then
                txtNombreProducto.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdTramo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdTramo.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdTramo.Text.Length = 1 Then
                txtNombreTramo.Text = String.Empty
            End If

            Carga_Detalle_Ciclico()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacion.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdUbicacion.Text.Length = 1 Then
                txtNombreUbicacion.Text = String.Empty
            End If

            Carga_Detalle_Ciclico()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub gdviewTeorico_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gdviewTeorico.RowCellStyle

        Try
            If e.Column.FieldName = "Dif.Cant.UMBas" Then

                Dim View As GridView = sender
                Dim CantidadConteo As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant.Conteo.UMBas"))
                Dim CantidadStock As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant.Teorica.UMBas"))

                If CantidadConteo <> CantidadStock Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf CantidadConteo = CantidadStock Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If

            End If

            If e.Column.FieldName = "Cant.Reconteo.UMBas" Then

                Dim View As GridView = sender
                Dim CantidadReconteo As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant.Reconteo.UMBas"))
                Dim CantidadConteo As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant.Conteo.UMBas"))
                Dim CantidadStock As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant.Teorica.UMBas"))

                If (CantidadReconteo <> CantidadStock) AndAlso (CantidadConteo > 0) AndAlso (CantidadReconteo > 0) Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf CantidadReconteo = CantidadStock Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If

            End If

            If e.Column.FieldName = "PesoConteo" Then

                Dim View As GridView = sender
                Dim PesoConteo As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("PesoConteo"))
                Dim PesoStock As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("PesoStock"))

                If PesoConteo <> PesoStock Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf PesoConteo = PesoStock Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If

            End If

            If e.Column.FieldName = "PesoReconteo" Then

                Dim View As GridView = sender
                Dim PesoReconteo As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("PesoReconteo"))
                Dim PesoStock As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("PesoStock"))

                If PesoReconteo <> PesoStock Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf PesoReconteo = PesoStock Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If

            End If

            If e.Column.FieldName = "Cant.Conteo.UMBas" Then

                Dim View As GridView = sender
                Dim IdInvCiclico As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("IdInvCiclico"))
                Dim CantidadConteo As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant.Conteo.UMBas"))
                Dim CantidadStock As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant.Teorica.UMBas"))
                Dim CantidadReConteo As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cant.Reconteo.UMBas"))
                Dim vDif As Double = 0
                Dim Dr As DataRow = Nothing

                Try

                    Dr = DTInventarioCiclico.Select("IdInvCiclico = " & IdInvCiclico).FirstOrDefault()

                    If CantidadConteo > 0 AndAlso CantidadReConteo > 0 Then
                        vDif = Math.Round(CantidadStock - CantidadReConteo, 6)
                    ElseIf CantidadReConteo = 0 AndAlso CantidadConteo > 0 Then
                        '#EJC20180801_1116PM: Evita diferencia por un valor decimal pequeño.
                        vDif = Math.Round(CantidadStock - CantidadConteo, 6)
                        If Math.Abs(CDec(vDif)) = 0.000001 Then
                            If vDif > 0 Then
                                Dr("Cant.Conteo.UMBas") = CantidadConteo - vDif
                            Else
                                Dr("Cant.Conteo.UMBas") = CantidadConteo + vDif
                            End If
                            vDif = 0
                        End If
                    ElseIf CantidadConteo = 0 Then
                        vDif = Math.Round(CantidadStock - CantidadConteo, 6)
                    End If
                Catch ex As Exception

                End Try

            End If

            If e.Column.FieldName = "Cant.Reservada" Then

                Dim View As GridView = CType(sender, GridView)
                Dim rawValue As Object = View.GetRowCellValue(e.RowHandle, "Cant.Reservada")

                Dim CantReservada As Double = 0

                If rawValue IsNot Nothing AndAlso Double.TryParse(rawValue.ToString(), CantReservada) Then
                    If CantReservada <> 0 Then
                        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                        e.Appearance.ForeColor = Color.Black
                        e.Appearance.BackColor = Color.Plum
                        e.Appearance.BackColor2 = Color.WhiteSmoke
                        e.Appearance.GradientMode = LinearGradientMode.Vertical
                    Else
                        ' Cantidad es cero
                        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                        e.Appearance.ForeColor = Color.Black
                        e.Appearance.BackColor = Color.White
                        e.Appearance.BackColor2 = Color.White
                    End If
                Else
                    ' Si el valor es nulo o no numérico, lo consideramos como cero (o ignoramos estilo)
                    e.Appearance.BackColor = Color.White
                    e.Appearance.BackColor2 = Color.White
                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gdviewTeorico_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gdviewTeorico.RowStyle

        Try

            gdviewTeorico.OptionsBehavior.Editable = False
            gdviewTeorico.OptionsSelection.EnableAppearanceFocusedCell = False

            gdviewTeorico.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFocus

            gdviewTeorico.OptionsSelection.EnableAppearanceFocusedRow = True
            gdviewTeorico.OptionsSelection.EnableAppearanceHideSelection = True
            gdviewTeorico.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gdviewTeorico.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            gdviewTeorico.Appearance.FocusedRow.ForeColor = Color.White
            gdviewTeorico.Appearance.SelectedRow.ForeColor = Color.White

            gdviewTeorico.Appearance.SelectedRow.Options.UseBackColor = True
            gdviewTeorico.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub grdInvCiclico_DoubleClick(sender As Object, e As EventArgs) Handles dgridInventarioCiclico.DoubleClick

        Try

            If (gdviewTeorico.RowCount > 0) Then

                Dim Dr As DataRowView = gdviewTeorico.GetFocusedRow
                Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico
                Dim Ciclico As New clsBeTrans_inv_ciclico

                BeTransInvCiclico.IdInvCiclico = Dr.Item("IdInvCiclico")
                BeTransInvCiclico.IdStock = Dr.Item("IdStock")
                BeTransInvCiclico.Idinventarioenc = Dr.Item("IdInventario")
                BeTransInvCiclico.IdProductoBodega = Dr.Item("IdProductoBodega")

                clsLnTrans_inv_ciclico.Get_By_IdStock_And_IdProductoBodega(BeTransInvCiclico)
                Dim lSelectionIndex As Integer = gdviewTeorico.FocusedRowHandle

                If Modo = TipoTrans.Editar Then

                    Dim Reconteo As New frmConteo(frmConteo.TipoTrans.Nuevo) With {
                        .gBeCiclico = BeTransInvCiclico,
                        .Regulariza = gBeTransInvEnc.Regularizado}
                    Reconteo.DrDetalleInv = DTInventarioCiclico.Select("IdInvCiclico = " & BeTransInvCiclico.IdInvCiclico).FirstOrDefault()
                    Reconteo.ShowDialog()
                    Reconteo.Dispose()

                    Carga_Detalle_Ciclico()

                    dgridInventarioCiclico.RefreshDataSource()
                    gdviewTeorico.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = TipoTrans.Nuevo Then

                    Dim Reconteo As New frmConteo(frmConteo.TipoTrans.Nuevo) With {
                        .gBeCiclico = BeTransInvCiclico,
                        .Regulariza = gBeTransInvEnc.Regularizado}
                    Reconteo.DrDetalleInv = DTInventarioCiclico.Select("IdInvCiclico = " & BeTransInvCiclico.IdInvCiclico).FirstOrDefault()
                    Reconteo.ShowDialog()
                    Reconteo.Dispose()
                    Carga_Detalle_Ciclico()
                    gdviewTeorico.FocusedRowHandle = lSelectionIndex

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Generar_Reconteo()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando reconteo(s)...")

            Dim Detalle As New List(Of clsBeTrans_inv_ciclico)
            Dim Reconteo As New clsBeTrans_inv_reconteo
            Dim Ubicacion As New clsBeBodega_ubicacion

            ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_By_InventarioStock(gBeTransInvEnc.Idinventarioenc)

            For Each BeTransInvCiclico As clsBeTrans_inv_ciclico In ListInventarioCiclico

                Ubicacion.IdUbicacion = BeTransInvCiclico.IdUbicacion

                Reconteo.Idinventarioenc = BeTransInvCiclico.Idinventarioenc
                Reconteo.IdOperador = BeTransInvCiclico.Idoperador
                Reconteo.IdStock = BeTransInvCiclico.IdStock
                Reconteo.IdUbicacionAnterior = BeTransInvCiclico.IdUbicacion
                Reconteo.IdProductoBodega = BeTransInvCiclico.IdProductoBodega

                If BeTransInvCiclico.IdStock > 0 Then
                    If clsLnTrans_inv_reconteo.ObtenerByUbicacion(Reconteo) Then
                        BeTransInvCiclico.Cant_reconteo = Reconteo.Cantidad
                        BeTransInvCiclico.Peso_reconteo = Reconteo.Peso
                    End If
                End If

                If BeTransInvCiclico.User_agr <> "" Then

                    If BeTransInvCiclico.IdStock > 0 Then

                        If BeTransInvCiclico.Cant_stock <> BeTransInvCiclico.Cantidad Or BeTransInvCiclico.Peso_stock <> BeTransInvCiclico.Peso Then

                            If BeTransInvCiclico.EsNuevo = False Then
                                Detalle.Add(BeTransInvCiclico)
                            End If

                        ElseIf BeTransInvCiclico.Cant_stock <> BeTransInvCiclico.Cant_reconteo Or BeTransInvCiclico.Peso_stock <> BeTransInvCiclico.Peso_reconteo Then

                            If BeTransInvCiclico.EsNuevo = False Then
                                Detalle.Add(BeTransInvCiclico)
                            End If

                        End If

                    End If

                End If

            Next

            SplashScreenManager.CloseForm(False)

            If Detalle.Count > 0 Then
                Dim GenReconteo As New frmGeneraReconteo(frmGeneraReconteo.TipoTrans.Nuevo) With {.ListInventarioCiclico = Detalle, .pIdInventario = gBeTransInvEnc.Idinventarioenc}
                GenReconteo.ShowDialog()
                GenReconteo.Dispose()
                Carga_Detalle_Ciclico()
                Cargar_Reconteo()
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub cmdReconteo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdReconteo.ItemClick
        Generar_Reconteo()
    End Sub

    Private Sub Genera_Reporte_Inventario()

        Try

            Dim Rep As New rptInventarioConteo
            Rep.DataSource = clsLnTrans_inv_ciclico.Get_Reporte_Inventario(gBeTransInvEnc.Idinventarioenc)
            Rep.DataMember = "Result"
            Rep.Parameters("Empresa").Value = AP.NomEmpresa
            Rep.Parameters("Empresa").Visible = False
            Rep.Parameters("Bodega").Value = AP.NomBodega
            Rep.Parameters("Bodega").Visible = False
            Rep.RequestParameters = False
            Rep.ShowPreviewDialog()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al generar documento de inventario: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Genera_Reporte_InventarioByOperador()

        Try

            Dim Rep As New rptInventarioConteoOperador
            Rep.DataSource = clsLnTrans_inv_ciclico.Get_Reporte_InventarioByOperador(gBeTransInvEnc.Idinventarioenc)
            Rep.DataMember = "Result"
            Rep.Parameters("Empresa").Value = AP.NomEmpresa
            Rep.Parameters("Empresa").Visible = False
            Rep.Parameters("Bodega").Value = AP.NomBodega
            Rep.Parameters("Bodega").Visible = False
            Rep.RequestParameters = False
            Rep.ShowPreviewDialog()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al generar documento de inventario: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirdetalle.ItemClick
        Genera_Reporte_Inventario()
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
            printLink.Component = dgridInventarioCiclico
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Imprimir_VistaConteo()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderConteo

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
            printLink.Component = grdConteo
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Imprimir_VistaVerifica()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderVerifica

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
            printLink.Component = grdVerifica
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Imprimir_VistaCompara()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderCompara

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
            printLink.Component = dgridComparativoInvInicial
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Inventarios"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderConteo(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Detalle de Conteo"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderVerifica(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Detalle de Verificacion"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderCompara(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Detalle de Comparación"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimirGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirGrid.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdImprimirporoperador_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirporoperador.ItemClick
        Genera_Reporte_InventarioByOperador()
    End Sub

    Private Sub Cargar_Reconteo()

        Try

            grdReconteo.BeginUpdate()

            Listar_Encabezado()

            Listar_Detalle()

            GridView8.OptionsView.ColumnAutoWidth = False

            grdReconteo.EndUpdate()

            grdReconteo.ForceInitialize()

            GridView8.BestFitColumns()

            If GridView8.RowCount > 0 Then

                lblRegsRec.Caption = String.Format("Registros: {0}", GridView8.RowCount)

                GridView8.Columns("Hora_Inicio").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView8.Columns("Hora_Inicio").DisplayFormat.FormatString = "{0:H:mm:ss}"

                GridView8.Columns("Hora_Fin").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView8.Columns("Hora_Fin").DisplayFormat.FormatString = "{0:H:mm:ss}"

            End If

            GridView8.Columns("IdInventarioEnc").Visible = False
            GridView8.Columns("IdReconteoEnc").Visible = False

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    Private Sub Listar_Encabezado(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim lRow As DataRow

        Try

            ListReconteo = clsLnTrans_inv_enc_reconteo.Get_All_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc, lConnection, lTransaction)

            Dim ListaEncabezado = From i In ListReconteo Group i By Keys = New With {Key i.Idinvencreconteo, Key i.Estado,
                                                                Key i.Hora_ini, Key i.Hora_fin, Key i.Idinventarioenc, Key i.Reconteo} Into Group
                                  Select New With {.idrec = Keys.Idinvencreconteo, .idinv = Keys.Idinventarioenc, .estadorec = Keys.Estado, .hrini = Keys.Hora_fin, .hrfin = Keys.Hora_fin, .corre = Keys.Reconteo}

            If ListaEncabezado IsNot Nothing AndAlso ListaEncabezado.Count > 0 Then

                DSReconteo.Encabezado.Clear()

                DSReconteo.Encabezado.BeginLoadData()

                For Each Obj In ListaEncabezado

                    lRow = DSReconteo.Encabezado.NewRow

                    lRow.Item("IdReconteoEnc") = Obj.idrec
                    lRow.Item("Estado") = Obj.estadorec
                    lRow.Item("Hora_Inicio") = Obj.hrini
                    lRow.Item("Hora_Fin") = Obj.hrfin
                    lRow.Item("IdInventarioEnc") = Obj.idinv
                    lRow.Item("Correlativo") = Obj.corre

                    DSReconteo.Encabezado.AddEncabezadoRow(lRow)

                Next

                DSReconteo.Encabezado.EndLoadData()

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

    Private Sub Listar_Detalle()

        Dim lRow As DataRow

        Try

            Dim ListaEncabezado = From i In ListReconteo Group i By Keys = New With {Key i.Idinvencreconteo, Key i.Codigo,
                                                                Key i.Producto, Key i.Presentacion, Key i.EstadoProd, Key i.IdStock,
                                                                Key i.Lote, Key i.cantidadAnterior, Key i.Cantidad, Key i.pesoAnterior,
                                                             Key i.Peso, Key i.Fecha_Vence, Key i.Ubicacion, Key i.Operador, Key i.IdInvReconteo} Into Group
                                  Select New With {.idrec = Keys.Idinvencreconteo, .cod = Keys.Codigo, .prod = Keys.Producto, .pres = Keys.Presentacion,
                                                   .estaPr = Keys.EstadoProd, .idS = Keys.IdStock, .lt = Keys.Lote, .cntAnt = Keys.cantidadAnterior,
                                                   .cnt = Keys.Cantidad, .psAnt = Keys.pesoAnterior, .ps = Keys.Peso, .venche = Keys.Fecha_Vence, .ubic = Keys.Ubicacion,
                                                   .op = Keys.Operador, .idreconteo = Keys.IdInvReconteo}

            If ListaEncabezado IsNot Nothing AndAlso ListaEncabezado.Count > 0 Then

                DSReconteo.Detalle.Clear()

                DSReconteo.Detalle.BeginLoadData()

                For Each Obj In ListaEncabezado

                    lRow = DSReconteo.Detalle.NewRow

                    lRow.Item("IdReconteoEnc") = Obj.idrec
                    lRow.Item("Código") = Obj.cod
                    lRow.Item("Producto") = Obj.prod
                    lRow.Item("Presentación") = Obj.pres
                    lRow.Item("Estado") = Obj.estaPr
                    lRow.Item("IdStock") = Obj.idS
                    lRow.Item("Lote") = Obj.lt
                    lRow.Item("CantidadAnterior") = Obj.cntAnt
                    lRow.Item("Cantidad") = Obj.cnt
                    lRow.Item("PesoAnterior") = Obj.psAnt
                    lRow.Item("Peso") = Obj.ps
                    lRow.Item("Fecha_Vence") = Obj.venche
                    lRow.Item("Ubicación") = Obj.ubic
                    lRow.Item("Operador") = Obj.op
                    lRow.Item("Id_Inv_Reconteo") = Obj.idreconteo

                    DSReconteo.Detalle.AddDetalleRow(lRow)

                Next

                DSReconteo.Detalle.EndLoadData()

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

    Dim gridView As GridView
    Private Sub grdReconteo_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdReconteo.ViewRegistered

        Try

            gridView = e.View

            gridView.OptionsView.ColumnAutoWidth = False

            If gridView.Columns.Count > 0 Then

                gridView.OptionsView.ShowFooter = True

                gridView.Columns("CantidadAnterior").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("CantidadAnterior").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("CantidadAnterior").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("CantidadAnterior").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("PesoAnterior").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("PesoAnterior").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("PesoAnterior").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("PesoAnterior").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("Id_Inv_Reconteo").Visible = False
                gridView.Columns("IdReconteoEnc").Visible = False

                gridView.BestFitColumns()

            End If

            Try

                gridView.OptionsBehavior.Editable = False
                gridView.OptionsSelection.EnableAppearanceFocusedCell = False

                gridView.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFocus

                gridView.OptionsSelection.EnableAppearanceFocusedRow = True
                gridView.OptionsSelection.EnableAppearanceHideSelection = True
                gridView.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
                gridView.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

                gridView.Appearance.FocusedRow.ForeColor = Color.White
                gridView.Appearance.SelectedRow.ForeColor = Color.White

                gridView.Appearance.SelectedRow.Options.UseBackColor = True
                gridView.Appearance.SelectedRow.Options.UseForeColor = True

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Imprimir_VistaReconteo()

        Try

            GridView8.OptionsPrint.ExpandAllDetails = True
            GridView8.OptionsPrint.PrintDetails = True

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderAreaReconteo

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
            printLink.Component = grdReconteo
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderAreaReconteo(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Reporte de Reconteo"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimirReconteo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirReconteo.ItemClick
        Imprimir_VistaReconteo()
    End Sub

    Private Sub grdReconteo_DoubleClick(sender As Object, e As EventArgs) Handles grdReconteo.DoubleClick

        Try

            Dim Dr As DataRowView = gridView.GetFocusedRow
            Dim Obj As New clsBeTrans_inv_reconteo
            Obj.Idinvreconteo = Dr.Item("Id_Inv_Reconteo")
            clsLnTrans_inv_reconteo.Obtener(Obj)
            Dim lSelectionIndex As Integer = gridView.FocusedRowHandle

            If Modo = TipoTrans.Editar Then
                Dim Reconteo As New frmReconteo(frmReconteo.TipoTrans.Nuevo) With {.gBeReconteoDet = Obj, .Regulariza = gBeTransInvEnc.Regularizado}
                Reconteo.ShowDialog()
                Reconteo.Dispose()
                Cargar_Reconteo()
                Carga_Detalle_Ciclico()
                gridView.FocusedRowHandle = lSelectionIndex

            ElseIf Modo = TipoTrans.Nuevo Then
                Dim Reconteo As New frmReconteo(frmReconteo.TipoTrans.Nuevo) With {.gBeReconteoDet = Obj}
                Reconteo.ShowDialog()
                Reconteo.Dispose()
                Cargar_Reconteo()
                Carga_Detalle_Ciclico()
                gridView.FocusedRowHandle = lSelectionIndex
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

    Private Sub Procesar_Regularizar_Inventario()


        Try
            Dim RegularizaInventario As New frmRegularizarInventario(frmRegularizarInventario.TipoTrans.Nuevo) With
                {.gBeInventario = gBeTransInvEnc}
            RegularizaInventario.ShowDialog()
            RegularizaInventario.Dispose()

            If gBeTransInvEnc.Regularizado Then
                Close()
                'Desactiva_Menu()
            Else
                Listar_Datos_De_Inventario()
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

    Private Sub cmdActualizarInventario_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizarInventario.ItemClick
        Procesar_Regularizar_Inventario()
    End Sub

    Private Sub tlProductosInventario_MouseClick(sender As Object, e As MouseEventArgs) Handles dgridAsignacionProductos.MouseClick
        dgridAsignacionProductos.SetFocusedNode(dgridAsignacionProductos.GetNodeAt(e.X, e.Y))
    End Sub

    Private Sub tlUbicaciones_MouseClick(sender As Object, e As MouseEventArgs) Handles dgridAsignacionUbicaciones.MouseClick
        dgridAsignacionUbicaciones.SetFocusedNode(dgridAsignacionUbicaciones.GetNodeAt(e.X, e.Y))

    End Sub

    Private Sub tlUbicacionesOperador_MouseClick(sender As Object, e As MouseEventArgs) Handles dgridAsignacionOperadores.MouseClick
        dgridAsignacionOperadores.SetFocusedNode(dgridAsignacionOperadores.GetNodeAt(e.X, e.Y))
    End Sub

    Private Sub GridView8_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView8.RowCellStyle

        If e.Column.FieldName = "Cantidad" Then

            Dim View As GridView = sender
            Dim CantidadConteo As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("CantidadAnterior"))
            Dim CantidadStock As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cantidad"))

            If CantidadConteo <> CantidadStock Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.ForeColor = Color.Black
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf CantidadConteo = CantidadStock Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.ForeColor = Color.Black
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            End If

        End If

    End Sub

    Private Sub cmdImprimirConteo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirConteo.ItemClick
        Imprimir_VistaConteo()
    End Sub

    Private Sub cmdImprimirVerifi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirVerifi.ItemClick
        Imprimir_VistaVerifica()
    End Sub

    Private Sub cmdImprimirComparacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirComparacion.ItemClick
        Imprimir_VistaCompara()
    End Sub

    Private Sub frmInventario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            Cargar_Forma()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub tlProductosInventario_AfterCheckNode(sender As Object, e As NodeEventArgs) Handles dgridAsignacionProductos.AfterCheckNode

        dgridAsignacionProductos.SetFocusedNode(e.Node)

        Try

            Dim ND = dgridAsignacionProductos.FocusedNode

            If ND Is Nothing Then
                Return
            End If

            If ND.Checked Then

                ND.CheckAll()

            Else

                ND.UncheckAll()

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Carga_Inventario_Congelado(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try

            DTInventarioCongelado = clsLnTrans_inv_stock.Get_All_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc,
                                                                                    AP.IdBodega,
                                                                                    lConnection,
                                                                                    lTransaction)

            dgridCongelado.DataSource = DTInventarioCongelado

            If GridView9.RowCount > 0 Then

                GridView9.Columns("Codigo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                GridView9.Columns("Codigo").SummaryItem.DisplayFormat = "Registros: {0}"

                GridView9.OptionsView.ShowFooter = True

                GridView9.BestFitColumns(True)

                GridView9.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView9.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"
                GridView9.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView9.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView9.Columns("Cantidad_Reservada_UmBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView9.Columns("Cantidad_Reservada_UmBas").SummaryItem.DisplayFormat = "{0:n6}"
                GridView9.Columns("Cantidad_Reservada_UmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView9.Columns("Cantidad_Reservada_UmBas").DisplayFormat.FormatString = "{0:n6}"

                GridView9.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView9.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"
                GridView9.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView9.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

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

    '#EJC20180627: Exportar a excel el grid dependiendo del tab en el que se encuentre el usuario
    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcel.ItemClick

        Try

            If xtraTabInv.SelectedTabPage Is Tramos Then
                Exportar_Grid_A_Excel(dgridAsignacionTramos, "WMS_Inv_Ciclico_Asignacion_Tramos.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabAsignacionUbicaciones Then
                Exportar_Grid_A_Excel(dgridAsignacionUbicaciones, "WMS_Inv_Ciclico_Asignacion_Ubicaciones.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabAsignacionProductos Then
                Exportar_Grid_A_Excel(dgridAsignacionProductos, "WMS_Inv_Ciclico_Asignacion_Productos.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabAsignacionOperadores Then
                Exportar_Grid_A_Excel(dgridAsignacionOperadores, "WMS_Inv_Ciclico_Asignacion_Operadores.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabDetalle Then
                Exportar_Grid_A_Excel(dgridComparativoInvInicial, "WMS_Inv_Inicial.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabConteo Then
                Exportar_Grid_A_Excel(dgridInventarioCiclico, "WMS_Inv_Ciclico.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabInvTeorico Then
                Exportar_Grid_A_Excel(dgridInvTeorico, "WMS_Inv_Teorico.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabInvCongelado Then
                Exportar_Grid_A_Excel(dgridCongelado, "WMS_Inv_Congelado.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is TabInventarioCostos Then
                Exportar_Grid_A_Excel(grdCostos, "WMS_In_Comparacion_Valorizacion.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabComparativoERPWMS Then
                Exportar_Grid_A_Excel(dgridcomparativoerpwms, "WMS_Inv_Comparativo_ERP.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is tabDiferenciasInventario Then
                Exportar_Grid_A_Excel(dgridDiferenciasCiclico, "WMS_Diferencias_Ciclico.xlsx")
            ElseIf xtraTabInv.SelectedTabPage Is xtpRegularizacion Then
                Exportar_Grid_A_Excel(grdRegularizar, "WMS_Regularizacion_Ciclico" & gBeTransInvEnc.Idinventarioenc & ".xlsx")
            Else
                XtraMessageBox.Show("No hay nada que exportar en esta página", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As TreeList, ByVal NomArchivo As String)

        Try

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = NomArchivo

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    dGrid.ExportToXlsx(myStream)
                    myStream.Close()
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

    Private Sub txtIdProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdProducto.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtIdProducto.Text.Trim <> "" Then
                Carga_Detalle_Ciclico()
            End If
        End If
    End Sub

    Private Sub Aplicar_Ajustes_Fecha_Vencimiento()

        Dim pIdAjusteEnc As Integer = 0
        Dim clsTransaccion As New clsTransaccion

        Try

            If XtraMessageBox.Show("¿Realizar ajustes de fechas de vencimiento en proceso de inventario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim vListaTeoricoNAV As New List(Of clsBeTrans_inv_stock_prod)

                clsTransaccion.Begin_Transaction()

                vListaTeoricoNAV = clsLnTrans_inv_stock_prod.Get_All_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                With frmRegularizarInventario

                    If vListaTeoricoNAV.Count > 0 Then

                        '#CKFK20241210: Enviar 0 en ajuste por inventario porque solo es por fecha,
                        'cuando se envía un valor mayor que 0, en el ajuste se habilita la edición del campo bodega
                        '#EJC20180924: Enviar false en ajuste por inventario porque solo es por fecha,
                        'cuando se envía true, en el ajuste se habilita la edición del campo bodega

                        '#Metodo para insertar encabezado de ajuste
                        clsLnTrans_inv_ciclico.Inserta_Encabezado_Ajuste(gBeTransInvEnc.Idinventarioenc,
                                                                         AP.UsuarioAp,
                                                                         cmbBodega.EditValue,
                                                                         cmbProductoFamilia.EditValue,
                                                                         cmbPropietario.EditValue,
                                                                         pIdAjusteEnc,
                                                                         False,
                                                                         gBeTransInvEnc.IdCentroCosto,
                                                                         clsTransaccion.lConnection,
                                                                         clsTransaccion.lTransaction)

                        '#EJC20180822:0315PM: No se estaba enviando el IdPropietarioBodega a la función que inserta el detalle del ajuste, por lo que se insertaba el IdPropietario
                        Dim vIdPropietarioBodega As Integer = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(cmbBodega.EditValue, cmbPropietario.EditValue)

                        For Each lInvStoc In vListaTeoricoNAV

                            If lInvStoc.Lote = "TET-157-230415.." Then
                                Debug.Print("Espera")
                            End If

                            .Inserta_Detalle_Ajuste_Fecha(lInvStoc,
                                                          clsTransaccion.lConnection,
                                                          clsTransaccion.lTransaction,
                                                          pIdAjusteEnc,
                                                          vIdPropietarioBodega)

                        Next

                        MsgBox("Se finalizó el proceso de ajuste de fechas", MsgBoxStyle.Information)

                    End If

                End With

                clsTransaccion.Commit_Transaction()

            End If

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub twTodos_Toggled(sender As Object, e As EventArgs) Handles twTodos.Toggled

        Try

            For Each Obj As TreeListNode In dgridAsignacionProductos.Nodes

                If Obj.Visible Then Obj.Checked = twTodos.IsOn

            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdAplicarAjustesFecha_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAplicarAjustesFecha.ItemClick
        Aplicar_Ajustes_Fecha_Vencimiento()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        'Exportar_Grid_A_PDF(grdCostos, "WMS_Inventario_comparativo_por_costos.pdf")
        Llena_Reporte_Inventario_Teorico_Costos()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)

    End Sub

    Private Sub bwKPI_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwKPI.DoWork
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            Actualiza_KPIS()
            SplashScreenManager.Default.CloseWaitForm()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private DTConteoPorEstratoTipo As New DataTable("DTConteoPorEstratoTipo")
    Private Sub Get_KPI_Conteo_Por_Estrato_Tipo()

        Try

            DTConteoPorEstratoTipo = clsLnTrans_inv_ciclico.Get_Conteo_Por_Estrato_Tipo(gBeTransInvEnc.Idinventarioenc)

            Dim vTipoProducto As String = ""
            Dim vCantidadTeorica As Double = 0
            Dim vCantidadContada As Double = 0

            chartcEstratoTipo.Series.Clear()

            Dim Teorico As New DevExpress.XtraCharts.Series("Teórico", ViewType.Bar)
            For Each Tipo As DataRow In DTConteoPorEstratoTipo.Rows
                vTipoProducto = IIf(IsDBNull(Tipo.Item("NombreTipoProducto")), "", Tipo.Item("NombreTipoProducto"))
                vCantidadTeorica = IIf(IsDBNull(Tipo.Item("Teórico")), 0, Tipo.Item("Teórico"))
                vCantidadContada = IIf(IsDBNull(Tipo.Item("Contado")), 0, Tipo.Item("Contado"))
                Teorico.Points.Add(New SeriesPoint(vTipoProducto, vCantidadTeorica))
            Next

            Dim Contado As New DevExpress.XtraCharts.Series("Contado", ViewType.Bar)
            For Each Tipo As DataRow In DTConteoPorEstratoTipo.Rows
                vTipoProducto = IIf(IsDBNull(Tipo.Item("NombreTipoProducto")), "", Tipo.Item("NombreTipoProducto"))
                vCantidadTeorica = IIf(IsDBNull(Tipo.Item("Teórico")), 0, Tipo.Item("Teórico"))
                vCantidadContada = IIf(IsDBNull(Tipo.Item("Contado")), 0, Tipo.Item("Contado"))
                Contado.Points.Add(New SeriesPoint(vTipoProducto, vCantidadContada))
            Next

            chartcEstratoTipo.Series.Add(Teorico)
            chartcEstratoTipo.Series.Add(Contado)

            Dim diagram = (CType(chartcEstratoTipo.Diagram, XYDiagram))
            diagram.AxisY.Title.Text = "Unidades en UMBas"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Label.TextPattern = "{V:0,,}K"

            Dim title = New ChartTitle() With {.Text = "Teórico Vrs. Conteo <br> Segregación por tipo"}
            chartcEstratoTipo.Titles.Add(title)
            chartcEstratoTipo.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center
            chartcEstratoTipo.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside
            chartcEstratoTipo.Legend.Direction = LegendDirection.LeftToRight
            chartcEstratoTipo.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Actualizando KPI's..")
        Actualiza_KPIS()
        SplashScreenManager.Default.CloseWaitForm()

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
        pIdPropietario = clsLnPropietarios.Get_IdPropietario(AP.IdBodega, cmbPropietario.EditValue)

    End Sub

    Private Sub chkMultiEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles chkMultiPropietario.CheckedChanged

        Try

            If chkMultiPropietario.Checked Then
                cmbPropietario.Enabled = False
                pIdPropietario = 0
            Else
                cmbPropietario.Enabled = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged

        'EFREN29072021: pIdPropietario no se actualiza si es el único valor en el combo. por eso se obtiene a traves de getselectedDataRow
        If chkMultiPropietario.Checked Then
            pIdPropietario = 0
        Else
            Dim fila As Object = cmbPropietario.GetSelectedDataRow
            If fila IsNot Nothing Then
                pIdPropietario = fila.Item("IdPropietario")
            End If
        End If

        'pIdPropietario = cmbPropietario.EditValue

    End Sub

    Private Sub mnuImportarTeoricoERP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImportarTeoricoERP.ItemClick

        Try

            Dim InvImp As New frmInventarioImport
            InvImp.IdInventario = gBeTransInvEnc.Idinventarioenc
            InvImp.DobleVerificacion = gBeTransInvEnc.Doble_verificacion
            InvImp.InvTeorico_Multi_Propietario = gBeTransInvEnc.multi_propietario
            InvImp.TipoInventario = cmbTipoInventario.Properties.GetDisplayText(cmbTipoInventario.EditValue)
            InvImp.TipoTeoricoImportacion = frmInventarioImport.pTipoImportacion.ERP

            If gBeTransInvEnc.multi_propietario Then
                InvImp.IdPropietarioBodega = 0
            Else
                InvImp.IdPropietarioBodega = cmbPropietario.EditValue
            End If

            InvImp.ShowDialog()
            InvImp.Dispose()

            Listar_Datos_De_Inventario()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Calcular_Inventario_Teorico_ERP(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try

            If gBeTransInvEnc.Inicial Then
                DTComparativoWMSVrsERP = clsLnTrans_inv_enc.Get_Inventario_Vrs_Stock_Det_ERP(gBeTransInvEnc.Idinventarioenc,
                                                                                             gBeTransInvEnc.IdBodega,
                                                                                             chkConUbicacion.IsOn,
                                                                                             chkLoteVence.IsOn,
                                                                                             lConnection,
                                                                                             lTransaction)
            Else
                DTComparativoWMSVrsERP = clsLnTrans_inv_enc.Get_Inventario_Vrs_Stock_Det_Teorico_ERP(gBeTransInvEnc.Idinventarioenc,
                                                                                                     lConnection,
                                                                                                     lTransaction)
            End If

            dgridcomparativoerpwms.DataSource = DTComparativoWMSVrsERP

            If gvInvTeoricoERP.Columns.Count > 0 Then

                If Not gBeTransInvEnc.Inicial Then

                    gvInvTeoricoERP.Columns("Tipo").Group()

                    gvInvTeoricoERP.Columns("Codigo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    gvInvTeoricoERP.Columns("Codigo").SummaryItem.DisplayFormat = "Registros: {0}"

                    gvInvTeoricoERP.Columns("Stock_WMS").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoERP.Columns("Stock_WMS").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoERP.Columns("Stock_WMS").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoERP.Columns("Stock_WMS").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoERP.Columns("Teorico_ERP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoERP.Columns("Teorico_ERP").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoERP.Columns("Teorico_ERP").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoERP.Columns("Teorico_ERP").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoERP.Columns("Dif_ERP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoERP.Columns("Dif_ERP").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoERP.Columns("Dif_ERP").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoERP.Columns("Dif_ERP").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoERP.Columns("Dif_Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoERP.Columns("Dif_Conteo").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoERP.Columns("Dif_Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoERP.Columns("Dif_Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoERP.Columns("Conteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoERP.Columns("Conteo").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoERP.Columns("Conteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoERP.Columns("Conteo").SummaryItem.DisplayFormat = "{0:n6}"

                Else

                    gvInvTeoricoERP.Columns("Inv").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoERP.Columns("Inv").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoERP.Columns("Inv").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoERP.Columns("Inv").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoERP.Columns("Stock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoERP.Columns("Stock").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoERP.Columns("Stock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoERP.Columns("Stock").SummaryItem.DisplayFormat = "{0:n6}"

                    gvInvTeoricoERP.Columns("Dif").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gvInvTeoricoERP.Columns("Dif").DisplayFormat.FormatString = "{0:n6}"

                    gvInvTeoricoERP.Columns("Dif").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gvInvTeoricoERP.Columns("Dif").SummaryItem.DisplayFormat = "{0:n6}"

                End If

                gvInvTeoricoERP.OptionsView.ShowFooter = True

                gvInvTeoricoERP.ExpandAllGroups()

                gvInvTeoricoERP.BestFitColumns(True)

                lblRegs.Caption = String.Format("Registros: {0}", gviewComparativo.RowCount)

                If Not gBeTransInvEnc.Inicial Then

                    gvInvTeoricoERP.Columns("Codigo").Width = 100

                    Dim gridFormatRule As New GridFormatRule()
                    Dim formatConditionRuleExpression As New FormatConditionRuleExpression()
                    gridFormatRule.Column = gvInvTeoricoERP.Columns("Dif_Conteo")
                    gridFormatRule.ApplyToRow = False
                    formatConditionRuleExpression.PredefinedName = "Red Fill, Red Text"
                    formatConditionRuleExpression.Expression = "[Dif_Conteo] <> 0"
                    gridFormatRule.Rule = formatConditionRuleExpression
                    gvInvTeoricoERP.FormatRules.Add(gridFormatRule)

                    Dim gridFormatRule1 As New GridFormatRule()
                    Dim formatConditionRuleExpression1 As New FormatConditionRuleExpression()
                    gridFormatRule1.Column = gvInvTeoricoERP.Columns("Dif_ERP")
                    gridFormatRule1.ApplyToRow = False
                    formatConditionRuleExpression1.PredefinedName = "Red Fill, Red Text"
                    formatConditionRuleExpression1.Expression = "[Dif_ERP] <> 0"
                    gridFormatRule1.Rule = formatConditionRuleExpression1
                    gvInvTeoricoERP.FormatRules.Add(gridFormatRule1)

                End If

            End If

            Set_LayOut_Grid(vNombreArchivoLayOutGridComparaERP)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdAsignaOpProd_Click(sender As Object, e As EventArgs) Handles cmdAsignaOpProd.Click

        Dim Operador As New clsBeTrans_inv_operador
        Dim BeTransInvCiclicoUbic As New clsBeTrans_inv_ciclico_ubic
        Dim Ubicaciones As New List(Of clsBeTrans_inv_ciclico_ubic)
        Dim clsTrans As New clsTransaccion
        Dim cantReg As Integer = 0

        Try

            clsTrans.Open_Connection() : clsTrans.Begin_Transaction()

            For Each NAsignacion As TreeListNode In dgridAsignacionProductos.Nodes

                If NAsignacion.Checked Then

                    If cmbOperadorProd.EditValue >= 0 Then

                        Dim vIdProductoBodega As Integer = clsLnProducto.Get_IdProductoBodega_By_Codigo_And_IdBodega(NAsignacion.Item("Código"), AP.IdBodega, clsTrans.lConnection, clsTrans.lTransaction)
                        Dim vUbicacion As Integer = 0

                        If NAsignacion.Item("Ubicacion").ToString.Split("#").Any Then
                            vUbicacion = Val(NAsignacion.Item("Ubicacion").ToString.Split("#")(1))
                        End If

                        Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                        Operador.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                        Operador.Idinvencreconteo = 0
                        Operador.Idubic = vUbicacion
                        Operador.Idoperador = cmbOperadorProd.EditValue
                        Operador.IdBodega = AP.IdBodega

                        If Not clsLnTrans_inv_operador.Existe_Ubicacion_By_IdOperador(Operador, clsTrans.lConnection, clsTrans.lTransaction) Then

                            clsLnTrans_inv_operador.Insertar(Operador, clsTrans.lConnection, clsTrans.lTransaction)

                        End If

                        If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(vUbicacion, gBeTransInvEnc.Idinventarioenc, clsTrans.lConnection, clsTrans.lTransaction) Then

                            BeTransInvCiclicoUbic.Idinventarioenc = IdInventario
                            BeTransInvCiclicoUbic.Idubicacion = vUbicacion
                            BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                            Ubicaciones.Add(BeTransInvCiclicoUbic)

                        End If

                        Dim lBeTransInvCiclico As New List(Of clsBeTrans_inv_ciclico)
                        lBeTransInvCiclico = clsLnTrans_inv_ciclico.Get_All_By_IdProductoBodega_And_IdUbicacion(gBeTransInvEnc.Idinventarioenc,
                                                                                                                        vIdProductoBodega,
                                                                                                                        vUbicacion,
                                                                                                                        clsTrans.lConnection,
                                                                                                                        clsTrans.lTransaction)



                        ' Crear una lista única por IdStock e IdUbicacion
                        Dim distinctList As List(Of clsBeTrans_inv_ciclico) = lBeTransInvCiclico _
                                                                                    .GroupBy(Function(x) New With {Key x.IdStock, Key x.IdUbicacion}) _
                                                                                    .Select(Function(g) g.First()) _
                                                                                    .ToList()


                        Dim InvCiclico As New clsBeTrans_inv_ciclico

                        For Each invcic In distinctList

                            If Not clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador_And_IdStock(Operador.Idoperador, invcic.Idinventarioenc, invcic.IdProductoBodega, invcic.IdStock, clsTrans.lConnection, clsTrans.lTransaction) Then

                                InvCiclico = New clsBeTrans_inv_ciclico
                                InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                                InvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                                InvCiclico.IdStock = invcic.IdStock
                                InvCiclico.IdProductoBodega = invcic.IdProductoBodega
                                InvCiclico.IdProductoEstado = invcic.IdProductoEstado
                                InvCiclico.IdProductoEst_nuevo = invcic.IdProductoEstado
                                InvCiclico.IdUbicacion = invcic.IdUbicacion
                                InvCiclico.IdPresentacion = invcic.IdPresentacion
                                InvCiclico.EsNuevo = False
                                InvCiclico.Lote_stock = invcic.Lote_stock
                                InvCiclico.Lote = invcic.Lote
                                InvCiclico.Fecha_vence_stock = invcic.Fecha_vence_stock
                                InvCiclico.Fecha_vence = invcic.Fecha_vence
                                InvCiclico.Cant_stock = invcic.Cant_stock
                                InvCiclico.Cantidad = 0
                                InvCiclico.Cant_reconteo = 0
                                InvCiclico.Peso_stock = invcic.Peso_stock
                                InvCiclico.Peso = invcic.Peso
                                InvCiclico.Peso_reconteo = invcic.Peso_reconteo
                                InvCiclico.Idoperador = Operador.Idoperador
                                InvCiclico.User_agr = AP.UsuarioAp.Nombres
                                InvCiclico.Fec_agr = Now
                                InvCiclico.EsPallet = invcic.EsPallet
                                InvCiclico.lic_plate = invcic.lic_plate
                                InvCiclico.IdBodega = AP.IdBodega
                                InvCiclico.IdUnidadMedida = invcic.IdUnidadMedida
                                InvCiclico.Cantidad_Reservada_UMBas = invcic.Cantidad_Reservada_UMBas

                                clsLnTrans_inv_ciclico.Insertar(InvCiclico, clsTrans.lConnection, clsTrans.lTransaction)

                            End If

                        Next

                    End If


                Else
                    XtraMessageBox.Show("Debe seleccionar un registro para asignar al operador.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                cantReg += 1

            Next

            clsLnTrans_inv_ciclico_ubic.Guardar_Ubicaciones(Ubicaciones, cmbOperadorProd.EditValue, clsTrans.lConnection, clsTrans.lTransaction)

            clsTrans.Commit_Transaction()

            Cargar_Forma()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdQuitaOpProd_Click(sender As Object, e As EventArgs) Handles cmdQuitaOpProd.Click

        Dim vIdProductoBodega As Integer = 0
        Dim vTransInvCiclico As New clsBeTrans_inv_ciclico
        Dim vUbicacion As Integer = 0
        Dim clsTrans As New clsTransaccion

        Try

            clsTrans.Begin_Transaction()

            For Each NA As TreeListNode In dgridAsignacionProductos.Nodes

                If NA.Checked Then

                    If NA.Item("IdOperador") > 0 Then

                        vIdProductoBodega = clsLnProducto.Get_IdProductoBodega_By_Codigo_And_IdBodega(NA.Item("Código"), AP.IdBodega,
                                                                                                      clsTrans.lConnection,
                                                                                                      clsTrans.lTransaction)

                        If NA.Item("Ubicacion").ToString.Split("#").Any Then
                            vUbicacion = Val(NA.Item("Ubicacion").ToString.Split("#")(1))
                        End If

                        If clsLnTrans_inv_ciclico.Existe_Producto_By_IdOperador(NA.Item("IdOperador"),
                                                                                gBeTransInvEnc.Idinventarioenc,
                                                                                vIdProductoBodega,
                                                                                clsTrans.lConnection,
                                                                                clsTrans.lTransaction) Then

                            clsLnTrans_inv_ciclico_ubic.Elimina_Ubicaciones_By_IdUbicacion_And_IdOperador(gBeTransInvEnc.Idinventarioenc,
                                                                                                          vIdProductoBodega,
                                                                                                          NA.Item("IdOperador"),
                                                                                                          clsTrans.lConnection,
                                                                                                          clsTrans.lTransaction)

                            clsLnTrans_inv_operador.Eliminar_IdUbicacion_By_IdUbicacion_And_IdOperador_And_IdProductoBodega(gBeTransInvEnc.Idinventarioenc,
                                                                                                                            NA.Item("IdOperador"),
                                                                                                                            vIdProductoBodega,
                                                                                                                            vUbicacion,
                                                                                                                            clsTrans.lConnection,
                                                                                                                            clsTrans.lTransaction)

                            vTransInvCiclico = New clsBeTrans_inv_ciclico
                            vTransInvCiclico.IdUbicacion = vUbicacion
                            vTransInvCiclico.IdProductoBodega = vIdProductoBodega
                            vTransInvCiclico.Idinventarioenc = gBeTransInvEnc.Idinventarioenc
                            vTransInvCiclico.Idoperador = NA.Item("IdOperador")

                            clsLnTrans_inv_ciclico.Eliminar_By_IdOperador_And_IdProductoBodega_And_IdUbicacion(vTransInvCiclico,
                                                                                                               clsTrans.lConnection,
                                                                                                               clsTrans.lTransaction)

                        End If

                    End If

                End If

            Next

            clsTrans.Commit_Transaction()

            Cargar_Forma()

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            clsTrans.Close_Conection()
        End Try

    End Sub

    Private vNombreArchivoLayOutGridIvConteo As String = "grdInvConteo"
    Private vNombreArchivoLayOutGridVerificacion As String = "grdInvVerifica"
    Private vNombreArchivoLayOutGridCompara As String = "grdInvCompara"
    Private vNombreArchivoLayOutGridComparaERP As String = "grdInvComparaERP"
    Private vNombreArchivoLayOutGridConteoOpe As String = "ArchivoLayOutGridConteoOpe"
    Private Sub Set_LayOut_Grid(ByVal vNombreArchivoLayOutGrid)

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)

            Select Case vNombreArchivoLayOutGrid

                Case vNombreArchivoLayOutGridIvConteo

                    If Not BeConfiguracionUsuarioDet Is Nothing Then
                        gviewConteo.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                        gviewConteo.LayoutChanged()
                    End If

                Case vNombreArchivoLayOutGridVerificacion

                    If Not BeConfiguracionUsuarioDet Is Nothing Then
                        gviewVerifica.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                    End If

                Case vNombreArchivoLayOutGridCompara

                    If Not BeConfiguracionUsuarioDet Is Nothing Then
                        gviewComparativo.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                    End If

                Case vNombreArchivoLayOutGridComparaERP

                    If Not BeConfiguracionUsuarioDet Is Nothing Then
                        gvInvTeoricoERP.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                    End If

                Case vNombreArchivoLayOutGridConteoOpe

                    If Not BeConfiguracionUsuarioDet Is Nothing Then
                        gvConteoOperador.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                    End If

            End Select


        Catch ex As Exception

            'XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            'Text,
            'MessageBoxButtons.OK,
            'MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Property IsLoading As Boolean = False

    Private Sub mnuImprimirComparacionERP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimirComparacionERP.ItemClick
        Imprimir_VistaCompara_ERP()
    End Sub

    Private Sub Imprimir_VistaCompara_ERP()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderComparaERP

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
            printLink.Component = dgridcomparativoerpwms
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderComparaERP(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Inventario: Comaprativo WMS - ERP"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub gviewConteo_Layout(sender As Object, e As EventArgs) Handles gviewConteo.Layout

        Try

            If IsLoading Then Exit Sub

            Dim vView As GridView = TryCast(sender, GridView)
            Dim vNombreArchivoLayOutGrid As String = ""

            Select Case vView.Name

                Case gviewConteo.Name

                    vNombreArchivoLayOutGrid = vNombreArchivoLayOutGridIvConteo

                    Dim Ms As New MemoryStream
                    gviewConteo.SaveLayoutToStream(Ms)
                    Ms.Seek(0, SeekOrigin.Begin)
                    Dim MsReader As New StreamReader(Ms)
                    Dim LayoutToText As String = MsReader.ReadToEnd()

                    clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  AP.HostName,
                                                                  vNombreArchivoLayOutGrid,
                                                                  LayoutToText)
            End Select


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gviewVerifica_Layout(sender As Object, e As EventArgs) Handles gviewVerifica.Layout

        Try

            If IsLoading Then Exit Sub

            Dim vView As GridView = TryCast(sender, GridView)
            Dim vNombreArchivoLayOutGrid As String = ""

            Select Case vView.Name

                Case gviewVerifica.Name

                    vNombreArchivoLayOutGrid = vNombreArchivoLayOutGridVerificacion

                    Dim Ms As New MemoryStream
                    gviewVerifica.SaveLayoutToStream(Ms)
                    Ms.Seek(0, SeekOrigin.Begin)
                    Dim MsReader As New StreamReader(Ms)
                    Dim LayoutToText As String = MsReader.ReadToEnd()

                    clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  AP.HostName,
                                                                  vNombreArchivoLayOutGrid,
                                                                  LayoutToText)
            End Select


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gvInvTeoricoERP_Layout(sender As Object, e As EventArgs) Handles gvInvTeoricoERP.Layout

        Try

            If IsLoading Then Exit Sub

            Dim vView As GridView = TryCast(sender, GridView)
            Dim vNombreArchivoLayOutGrid As String = ""

            Select Case vView.Name


                Case gvInvTeoricoERP.Name

                    vNombreArchivoLayOutGrid = vNombreArchivoLayOutGridComparaERP

                    Dim Ms As New MemoryStream
                    gvInvTeoricoERP.SaveLayoutToStream(Ms)
                    Ms.Seek(0, SeekOrigin.Begin)
                    Dim MsReader As New StreamReader(Ms)

                    Dim LayoutToText As String = MsReader.ReadToEnd()

                    clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  AP.HostName,
                                                                  vNombreArchivoLayOutGrid,
                                                                  LayoutToText)

            End Select


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gviewComparativo_Layout(sender As Object, e As EventArgs) Handles gviewComparativo.Layout

        Try

            If IsLoading Then Exit Sub

            Dim vView As GridView = TryCast(sender, GridView)
            Dim vNombreArchivoLayOutGrid As String = ""

            Select Case vView.Name

                Case gviewComparativo.Name

                    vNombreArchivoLayOutGrid = vNombreArchivoLayOutGridComparaERP

                    Dim Ms As New MemoryStream
                    gviewComparativo.SaveLayoutToStream(Ms)
                    Ms.Seek(0, SeekOrigin.Begin)
                    Dim MsReader As New StreamReader(Ms)

                    Dim LayoutToText As String = MsReader.ReadToEnd()

                    clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  AP.HostName,
                                                                  vNombreArchivoLayOutGrid,
                                                                  LayoutToText)

            End Select


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnkUbicacionInvInicial_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacionInvInicial.LinkClicked

        Try

            Dim Ubicacion As New frmBodegaUbicacion_List() With
                   {.IdInventario = gBeTransInvEnc.Idinventarioenc,
                   .Modo = frmBodegaUbicacion_List.pModo.Lista}
            Ubicacion.ShowDialog()

            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacionInvInicial.Tag = Ubicacion.pObj.IdUbicacion
                txtIdUbicacionInvInicial.Text = Ubicacion.pObj.IdUbicacion
            End If

            Ubicacion.Close()
            Ubicacion.Dispose()

            Listar_Datos_De_Inventario()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Carga_Detalle_Ciclico_Anterior()

        Dim CantidadUMBas As Double = 0
        Dim Cantidad_Contada_Pres As Double = 0
        Dim CantStockUM As Double = 0
        Dim Cantidad_Teorica_Stock_Pres As Double = 0
        Dim CantReUM As Double = 0
        Dim Cantidad_Reconteo_Pres As Double = 0
        Dim AbrioWaitForm As Boolean = False
        Dim vDiferencia As Double = 0
        Dim EstadoNuevo As String = ""
        Dim UbicacionNueva As String = ""

        Try
            'InventarioCiclico
            Dim Extraviado As Double = 0.0
            Dim Reconteo As New clsBeTrans_inv_reconteo
            Dim Ubicacion As New clsBeBodega_ubicacion

            ListInventarioCiclico.Clear()

            DTInventarioCiclico.Clear()

            dgridInventarioCiclico.DataSource = Nothing

            Application.DoEvents()

            ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc, AP.IdBodega)

            If pIdPropietario > 0 Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdPropietario = pIdPropietario)
            End If

            If txtIdFamilia.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdFamilia = txtIdFamilia.Text)
            End If

            If txtIdClasificacion.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdClasificacion = txtIdClasificacion.Text)
            End If

            If txtIdProducto.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.Codigo = txtIdProducto.Text)
            End If

            If txtIdUbicacion.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdUbicacion = txtIdUbicacion.Text)
            End If

            If txtIdTramo.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdTramo = txtIdTramo.Text)
            End If

            If txtIdOperador.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.Idoperador = txtIdOperador.Text)
            End If

            If ListInventarioCiclico.Count > 0 Then

                prgPanInvConteo.Visible = True
                prgPanInvConteo.Maximum = ListInventarioCiclico.Count

                Dim vContador As Integer = 0

                If SplashScreenManager.Default Is Nothing Then
                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Cargando datos..")
                    AbrioWaitForm = True
                End If

                ListInventarioCiclico = ListInventarioCiclico _
                                        .GroupBy(Function(x) x.IdStock) _
                                        .Select(Function(g) g.First()) _
                                        .ToList()

                Dim BeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

                For Each BeTransInvCiclico As clsBeTrans_inv_ciclico In ListInventarioCiclico

                    Ubicacion.IdUbicacion = BeTransInvCiclico.IdUbicacion

                    EstadoNuevo = clsLnProducto_estado.GetNombreByIdEstado(BeTransInvCiclico.IdProductoEst_nuevo)
                    UbicacionNueva = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(BeTransInvCiclico.IdUbicacion_nuevo,
                                                                                              BeTransInvCiclico.IdBodega)

                    If BeTransInvCiclico.EsNuevo Then

                        Extraviado = BeTransInvCiclico.Cantidad
                        BeTransInvCiclico.Cant_stock = 0
                        BeTransInvCiclico.Cant_reconteo = 0
                        BeTransInvCiclico.Peso_stock = 0
                        BeTransInvCiclico.Peso_reconteo = 0

                    Else

                        Reconteo.Idinventarioenc = BeTransInvCiclico.Idinventarioenc
                        Reconteo.IdOperador = BeTransInvCiclico.Idoperador
                        Reconteo.IdStock = BeTransInvCiclico.IdStock
                        Reconteo.IdUbicacionAnterior = BeTransInvCiclico.IdUbicacion
                        Reconteo.IdProductoBodega = BeTransInvCiclico.IdProductoBodega

                        If BeTransInvCiclico.IdStock > 0 Then

                            If clsLnTrans_inv_reconteo.Obtener_By_Ubicacion(Reconteo) Then
                                BeTransInvCiclico.Cant_reconteo = Reconteo.Cantidad
                                BeTransInvCiclico.Peso_reconteo = Reconteo.Peso
                            End If

                        End If

                        Extraviado = 0

                    End If

                    CantidadUMBas = 0
                    Cantidad_Contada_Pres = 0
                    Cantidad_Teorica_Stock_Pres = 0
                    CantStockUM = 0
                    Cantidad_Reconteo_Pres = 0
                    CantReUM = 0

                    If BeTransInvCiclico.IdUbicacion = 9685 Then
                        Debug.Print("Espera")
                    End If
                    If BeTransInvCiclico.IdPresentacion > 0 Then
                        '#EJC20180821: Desde la hermana república del salvador, con ustedes, los cálculos.
                        Cantidad_Contada_Pres = Math.Round(BeTransInvCiclico.Cantidad / BeTransInvCiclico.Factor, 6)
                        Cantidad_Teorica_Stock_Pres = Math.Round(BeTransInvCiclico.Cant_stock / BeTransInvCiclico.Factor, 6)
                        Cantidad_Reconteo_Pres = Math.Round(BeTransInvCiclico.Cant_reconteo / BeTransInvCiclico.Factor, 6)
                        CantidadUMBas = BeTransInvCiclico.Cantidad
                        CantStockUM = BeTransInvCiclico.Cant_stock
                        CantReUM = BeTransInvCiclico.Cant_reconteo
                    Else
                        CantidadUMBas = BeTransInvCiclico.Cantidad
                        CantStockUM = BeTransInvCiclico.Cant_stock
                        CantReUM = BeTransInvCiclico.Cant_reconteo
                    End If

                    vDiferencia = (CantStockUM - CantidadUMBas)

                    DTInventarioCiclico.Rows.Add(BeTransInvCiclico.IdInvCiclico,
                                                  BeTransInvCiclico.Ubicacion,
                                                  UbicacionNueva,
                                                  BeTransInvCiclico.IdStock,
                                                  BeTransInvCiclico.Codigo,
                                                  BeTransInvCiclico.Producto,
                                                  BeTransInvCiclico.TipoProducto,
                                                  BeTransInvCiclico.Presentacion,
                                                  BeTransInvCiclico.Estado,
                                                  EstadoNuevo,
                                                  BeTransInvCiclico.Lote_stock,
                                                  BeTransInvCiclico.Lote,
                                                  BeTransInvCiclico.Fecha_vence_stock,
                                                  BeTransInvCiclico.Fecha_vence,
                                                  Cantidad_Teorica_Stock_Pres,
                                                  CantStockUM,
                                                  BeTransInvCiclico.Peso_stock,
                                                  Cantidad_Contada_Pres,
                                                  CantidadUMBas,
                                                  BeTransInvCiclico.Peso,
                                                  Cantidad_Reconteo_Pres,
                                                  CantReUM,
                                                  BeTransInvCiclico.Peso_reconteo,
                                                  vDiferencia * -1,
                                                  Extraviado,
                                                  BeTransInvCiclico.Idinventarioenc,
                                                  BeTransInvCiclico.IdProductoBodega,
                                                  IIf(BeTransInvCiclico.lic_plate = "", "", BeTransInvCiclico.lic_plate),
                                                  BeTransInvCiclico.Contado)

                    SplashScreenManager.Default.SetWaitFormDescription(vContador & " de: " & ListInventarioCiclico.Count)

                    prgPanInvConteo.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Next

                'cTrans.Commit_Transaction()

                dgridInventarioCiclico.DataSource = DTInventarioCiclico

                If gdviewTeorico.RowCount > 0 Then

                    gdviewTeorico.Columns("IdInventario").Visible = False
                    gdviewTeorico.Columns("IdProductoBodega").Visible = False
                    gdviewTeorico.Columns("IdInvCiclico").Visible = False

                    gdviewTeorico.OptionsView.ShowFooter = True

                    '#EJC20180830_0540PM: Hot fix para funcionalidad de tablet
                    gdviewTeorico.Columns("Código").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                    gdviewTeorico.Columns("Cant.Teorica.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Teorica.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Teorica.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Teorica.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Teorica.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Conteo.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Conteo.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Conteo.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Conteo.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Reconteo.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Reconteo.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoReconteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoReconteo").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoReconteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoReconteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Extraviado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Extraviado").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Extraviado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Extraviado").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Dif.Cant.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Dif.Cant.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Ubicación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    gdviewTeorico.Columns("Ubicación").SummaryItem.DisplayFormat = "Registros: {0}"

                    gdviewTeorico.Columns("PesoReconteo").Visible = False
                    gdviewTeorico.Columns("PesoConteo").Visible = False
                    gdviewTeorico.Columns("PesoStock").Visible = False

                    gdviewTeorico.BestFitColumns()

                End If

            End If


            Actualiza_KPIS()


        Catch ex As Exception
            Try
                'cTrans.RollBack_Transaction()
            Catch ex1 As Exception
            End Try
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If AbrioWaitForm Then SplashScreenManager.CloseForm(False)
            prgPanInvConteo.Value = 0
            prgPanInvConteo.Visible = False
        End Try

    End Sub

    Private Sub Actualiza_KPIS(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try

            ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc, AP.IdBodega, lConnection, lTransaction)

            If Not ListInventarioCiclico Is Nothing Then

                Get_KPI_Porcentaje_Registros_Contados()

                Get_KPI_Universo_Vrs_Muestra()

                Get_KPI_Conteo_Por_Estrato_Tipo()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Reconteo(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            grdReconteo.BeginUpdate()

            Listar_Encabezado()

            Listar_Detalle()

            GridView8.OptionsView.ColumnAutoWidth = False

            grdReconteo.EndUpdate()

            grdReconteo.ForceInitialize()

            GridView8.BestFitColumns()

            If GridView8.RowCount > 0 Then

                lblRegsRec.Caption = String.Format("Registros: {0}", GridView8.RowCount)

                GridView8.Columns("Hora_Inicio").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView8.Columns("Hora_Inicio").DisplayFormat.FormatString = "{0:H:mm:ss}"

                GridView8.Columns("Hora_Fin").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView8.Columns("Hora_Fin").DisplayFormat.FormatString = "{0:H:mm:ss}"

            End If

            GridView8.Columns("IdInventarioEnc").Visible = False
            GridView8.Columns("IdReconteoEnc").Visible = False

        Catch ex As Exception
            'SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Listar_Encabezado()

        Dim lRow As DataRow

        Try

            ListReconteo = clsLnTrans_inv_enc_reconteo.Get_All_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc)

            Dim ListaEncabezado = From i In ListReconteo Group i By Keys = New With {Key i.Idinvencreconteo, Key i.Estado,
                                                                Key i.Hora_ini, Key i.Hora_fin, Key i.Idinventarioenc, Key i.Reconteo} Into Group
                                  Select New With {.idrec = Keys.Idinvencreconteo, .idinv = Keys.Idinventarioenc, .estadorec = Keys.Estado, .hrini = Keys.Hora_fin, .hrfin = Keys.Hora_fin, .corre = Keys.Reconteo}

            If ListaEncabezado IsNot Nothing AndAlso ListaEncabezado.Count > 0 Then

                DSReconteo.Encabezado.Clear()

                DSReconteo.Encabezado.BeginLoadData()

                For Each Obj In ListaEncabezado

                    lRow = DSReconteo.Encabezado.NewRow

                    lRow.Item("IdReconteoEnc") = Obj.idrec
                    lRow.Item("Estado") = Obj.estadorec
                    lRow.Item("Hora_Inicio") = Obj.hrini
                    lRow.Item("Hora_Fin") = Obj.hrfin
                    lRow.Item("IdInventarioEnc") = Obj.idinv
                    lRow.Item("Correlativo") = Obj.corre

                    DSReconteo.Encabezado.AddEncabezadoRow(lRow)

                Next

                DSReconteo.Encabezado.EndLoadData()

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

    Private Sub txtIdUbicacionInvInicial_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdUbicacionInvInicial.KeyDown
        If e.KeyValue = 13 Then
            chkComparativoConUbicacion.Checked = True
            Listar_Datos_De_Inventario()
        End If
    End Sub

    Private Sub gvConteoOperador_Layout(sender As Object, e As EventArgs) Handles gvConteoOperador.Layout

        Try

            If IsLoading Then Exit Sub

            Dim vView As GridView = TryCast(sender, GridView)


            Select Case vView.Name

                Case gviewConteo.Name

                    Dim Ms As New MemoryStream
                    gvConteoOperador.SaveLayoutToStream(Ms)
                    Ms.Seek(0, SeekOrigin.Begin)
                    Dim MsReader As New StreamReader(Ms)
                    Dim LayoutToText As String = MsReader.ReadToEnd()

                    clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  AP.HostName,
                                                                  vNombreArchivoLayOutGridConteoOpe,
                                                                  LayoutToText)
            End Select


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Carga_Detalle_Ciclico()

        Dim CantidadUMBas As Double = 0
        Dim Cantidad_Contada_Pres As Double = 0
        Dim CantStockUM As Double = 0
        Dim Cantidad_Teorica_Stock_Pres As Double = 0
        Dim CantReUM As Double = 0
        Dim Cantidad_Reconteo_Pres As Double = 0
        Dim AbrioWaitForm As Boolean = False
        Dim vDiferencia As Double = 0
        Dim EstadoNuevo As String = ""
        Dim UbicacionNueva As String = ""
        Dim clsTrans As New clsTransaccion

        Try
            'InventarioCiclico
            Dim Extraviado As Double = 0.0
            Dim Reconteo As New clsBeTrans_inv_reconteo
            Dim Ubicacion As New clsBeBodega_ubicacion

            ListInventarioCiclico.Clear()

            DTInventarioCiclico.Clear()

            dgridInventarioCiclico.DataSource = Nothing

            Application.DoEvents()

            clsTrans.Begin_Transaction()

            ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc,
                                                                                                        AP.IdBodega,
                                                                                                        clsTrans.lConnection,
                                                                                                        clsTrans.lTransaction)

            If pIdPropietario > 0 Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdPropietario = pIdPropietario)
            End If

            If txtIdFamilia.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdFamilia = txtIdFamilia.Text)
            End If

            If txtIdClasificacion.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdClasificacion = txtIdClasificacion.Text)
            End If

            If txtIdProducto.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.Codigo = txtIdProducto.Text)
            End If

            If txtIdUbicacion.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdUbicacion = txtIdUbicacion.Text)
            End If

            If txtIdTramo.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdTramo = txtIdTramo.Text)
            End If

            If txtIdOperador.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.Idoperador = txtIdOperador.Text)
            End If

            If ListInventarioCiclico.Count > 0 Then

                prgPanInvConteo.Visible = True
                prgPanInvConteo.Maximum = ListInventarioCiclico.Count

                Dim vContador As Integer = 0

                If SplashScreenManager.Default Is Nothing Then
                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Cargando datos..")
                    AbrioWaitForm = True
                End If

                Dim BeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

                For Each BeTransInvCiclico As clsBeTrans_inv_ciclico In ListInventarioCiclico

                    Ubicacion.IdUbicacion = BeTransInvCiclico.IdUbicacion

                    EstadoNuevo = BeTransInvCiclico.Estado

                    UbicacionNueva = ""

                    If BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then
                        UbicacionNueva = BeTransInvCiclico.Ubicacion_Nueva
                    End If

                    If BeTransInvCiclico.EsNuevo Then

                        Extraviado = BeTransInvCiclico.Cantidad
                        BeTransInvCiclico.Cant_stock = 0
                        BeTransInvCiclico.Cant_reconteo = 0
                        BeTransInvCiclico.Peso_stock = 0
                        BeTransInvCiclico.Peso_reconteo = 0

                    Else

                        Reconteo.Idinventarioenc = BeTransInvCiclico.Idinventarioenc
                        Reconteo.IdOperador = BeTransInvCiclico.Idoperador
                        Reconteo.IdStock = BeTransInvCiclico.IdStock
                        Reconteo.IdUbicacionAnterior = BeTransInvCiclico.IdUbicacion
                        Reconteo.IdProductoBodega = BeTransInvCiclico.IdProductoBodega

                        If BeTransInvCiclico.IdStock > 0 Then

                            If clsLnTrans_inv_reconteo.Obtener_By_Ubicacion(Reconteo, clsTrans.lConnection, clsTrans.lTransaction) Then
                                BeTransInvCiclico.Cant_reconteo = Reconteo.Cantidad
                                BeTransInvCiclico.Peso_reconteo = Reconteo.Peso
                            End If

                        End If

                        Extraviado = 0

                    End If

                    CantidadUMBas = 0
                    Cantidad_Contada_Pres = 0
                    Cantidad_Teorica_Stock_Pres = 0
                    CantStockUM = 0
                    Cantidad_Reconteo_Pres = 0
                    CantReUM = 0

                    If BeTransInvCiclico.IdPresentacion > 0 Then
                        '#EJC20180821: Desde la hermana república del salvador, con ustedes, los cálculos.
                        Cantidad_Contada_Pres = Math.Round(BeTransInvCiclico.Cantidad / BeTransInvCiclico.Factor, 6)
                        Cantidad_Teorica_Stock_Pres = Math.Round(BeTransInvCiclico.Cant_stock / BeTransInvCiclico.Factor, 6)
                        Cantidad_Reconteo_Pres = Math.Round(BeTransInvCiclico.Cant_reconteo / BeTransInvCiclico.Factor, 6)
                        CantidadUMBas = BeTransInvCiclico.Cantidad
                        CantStockUM = BeTransInvCiclico.Cant_stock
                        CantReUM = BeTransInvCiclico.Cant_reconteo
                    Else
                        CantidadUMBas = BeTransInvCiclico.Cantidad
                        CantStockUM = BeTransInvCiclico.Cant_stock
                        CantReUM = BeTransInvCiclico.Cant_reconteo
                    End If

                    vDiferencia = (CantStockUM - CantidadUMBas)

                    DTInventarioCiclico.Rows.Add(BeTransInvCiclico.IdInvCiclico,
                                                  BeTransInvCiclico.Ubicacion,
                                                  UbicacionNueva,
                                                  BeTransInvCiclico.IdStock,
                                                  BeTransInvCiclico.Codigo,
                                                  BeTransInvCiclico.Producto,
                                                  BeTransInvCiclico.TipoProducto,
                                                  BeTransInvCiclico.Presentacion,
                                                  BeTransInvCiclico.Estado,
                                                  EstadoNuevo,
                                                  BeTransInvCiclico.Lote_stock,
                                                  BeTransInvCiclico.Lote,
                                                  BeTransInvCiclico.Fecha_vence_stock,
                                                  BeTransInvCiclico.Fecha_vence,
                                                  Cantidad_Teorica_Stock_Pres,
                                                  CantStockUM,
                                                  BeTransInvCiclico.Peso_stock,
                                                  Cantidad_Contada_Pres,
                                                  CantidadUMBas,
                                                  BeTransInvCiclico.Peso,
                                                  Cantidad_Reconteo_Pres,
                                                  CantReUM,
                                                  BeTransInvCiclico.Peso_reconteo,
                                                  vDiferencia * -1,
                                                  Extraviado,
                                                  BeTransInvCiclico.Idinventarioenc,
                                                  BeTransInvCiclico.IdProductoBodega,
                                                  IIf(BeTransInvCiclico.lic_plate = "", "", BeTransInvCiclico.lic_plate),
                                                  BeTransInvCiclico.Cantidad_Reservada_UMBas,
                                                  BeTransInvCiclico.Contado)

                    SplashScreenManager.Default.SetWaitFormDescription(vContador + 1 & " de: " & ListInventarioCiclico.Count)

                    prgPanInvConteo.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Next

                dgridInventarioCiclico.DataSource = DTInventarioCiclico

                If gdviewTeorico.RowCount > 0 Then

                    gdviewTeorico.Columns("IdInventario").Visible = False
                    gdviewTeorico.Columns("IdProductoBodega").Visible = False
                    gdviewTeorico.Columns("IdInvCiclico").Visible = False

                    gdviewTeorico.OptionsView.ShowFooter = True

                    '#EJC20180830_0540PM: Hot fix para funcionalidad de tablet
                    gdviewTeorico.Columns("Código").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                    gdviewTeorico.Columns("Cant.Teorica.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Teorica.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Teorica.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Teorica.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Teorica.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Teorica.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Conteo.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Conteo.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Conteo.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Conteo.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Conteo.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Reconteo.Pres").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Reconteo.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Cant.Reconteo.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("PesoReconteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("PesoReconteo").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("PesoReconteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("PesoReconteo").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Extraviado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Extraviado").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Extraviado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Extraviado").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gdviewTeorico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gdviewTeorico.Columns("Dif.Cant.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gdviewTeorico.Columns("Dif.Cant.UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    gdviewTeorico.Columns("Ubicación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    gdviewTeorico.Columns("Ubicación").SummaryItem.DisplayFormat = "Registros: {0}"

                    gdviewTeorico.Columns("PesoReconteo").Visible = False
                    gdviewTeorico.Columns("PesoConteo").Visible = False
                    gdviewTeorico.Columns("PesoStock").Visible = False

                    gdviewTeorico.BestFitColumns()

                End If

            End If

            Actualiza_KPIS()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            prgPanInvConteo.Value = 0
            prgPanInvConteo.Visible = False
        End Try

    End Sub
    Private Sub Listar_Productos()

        Try

            dgridAsignacionProductos.ClearNodes()
            Listar_Productos_Asignados(dgridAsignacionProductos)
            dgridAsignacionProductos.CollapseAll()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try


    End Sub

    'Private Sub Listar_Productos_Asignados(ByRef tl As TreeList, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

    '    Try

    '        tl.BeginUnboundLoad()

    '        ListInventarioCiclico.Clear()

    '        tl.ClearNodes()

    '        ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(gBeTransInvEnc.Idinventarioenc,
    '                                                                                                               AP.IdBodega,
    '                                                                                                               lConnection,
    '                                                                                                               lTransaction)

    '        If ListInventarioCiclico.Count > 0 Then

    '            Dim parentForRootNodes As TreeListNode = Nothing

    '            Dim rootNode As TreeListNode

    '            Dim Lista = From i In ListInventarioCiclico Group i By Keys = New With {
    '                                                                          Key i.IdProductoBodega,
    '                                                                          Key i.Codigo,
    '                                                                          Key i.Producto,
    '                                                                          Key i.UnidadMedida,
    '                                                                          Key i.TipoProducto,
    '                                                                          Key i.Operador,
    '                                                                          Key i.Idoperador,
    '                                                                          Key i.Ubicacion} Into Group
    '                        Select New With {Keys.Codigo, Keys.Producto, Keys.UnidadMedida,
    '                                         Keys.TipoProducto, Keys.IdProductoBodega,
    '                                         Keys.Operador, Keys.Idoperador, Keys.Ubicacion,
    '                                         .Inventario_Inicial = Group.Sum(Function(x) x.Cant_stock)}

    '            For Each BeTransInvCiclico In Lista

    '                rootNode = tl.AppendNode(New Object() {BeTransInvCiclico.IdProductoBodega,
    '                                                       BeTransInvCiclico.Codigo,
    '                                                       BeTransInvCiclico.Producto,
    '                                                       BeTransInvCiclico.UnidadMedida,
    '                                                       BeTransInvCiclico.Operador,
    '                                                       BeTransInvCiclico.TipoProducto,
    '                                                       BeTransInvCiclico.Idoperador,
    '                                                       BeTransInvCiclico.Ubicacion}, parentForRootNodes)
    '                rootNode.Expanded = True
    '                rootNode.Tag = BeTransInvCiclico.IdProductoBodega

    '            Next

    '        End If

    '        '#Inserta_Ubicaciones
    '        'Inserta_Ubicaciones(Obj.IdUbicacion)

    '        tl.EndUnboundLoad()

    '        dgridAsignacionProductos.OptionsView.ShowSummaryFooter = True
    '        dgridAsignacionProductos.Columns(1).AllNodesSummary = True
    '        dgridAsignacionProductos.Columns(1).SummaryFooterStrFormat = "Productos: {0:n0}"
    '        dgridAsignacionProductos.Columns(1).SummaryFooter = SummaryItemType.Count

    '    Catch ex As Exception
    '        SplashScreenManager.CloseForm(False)
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

    Private Sub Crea_Estructura_Ubicaciones(ByRef tl As TreeList, ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        'Obtener las areas de la bodega
        Dim parentForRootNodes As TreeListNode = Nothing
        Dim NodoArea As TreeListNode
        Dim NodoSector As TreeListNode = Nothing
        Dim NodoTramo As TreeListNode = Nothing
        Dim NodoUbicacion As TreeListNode = Nothing

        Try

            tl.BeginUnboundLoad()

            DTUbicaciones = clsLnBodega.Get_Estructura_By_IdBodega_And_IdInventarioEnc(IdBodega,
                                                                                       gBeTransInvEnc.Idinventarioenc,
                                                                                       lConnection,
                                                                                       lTransaction)

            Dim Areas = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                                                                 Key .IdArea = row.Field(Of Integer)("IdArea"),
                                                                 Key .Name = row.Field(Of String)("Area")
                                                             }).Distinct().ToArray().OrderBy(Function(x) x.IdArea)

            For Each Area In Areas

                NodoArea = tl.AppendNode(New Object() {"Área: " & Area.IdArea, Area.Name, ("A")}, parentForRootNodes)

                Dim Sectores = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                                                                         Key .IdSector = row.Field(Of Integer)("IdSector"),
                                                                         Key .IdArea = row.Field(Of Integer)("IdArea"),
                                                                         Key .Name = row.Field(Of String)("Sector")
                                                                     }).Where(Function(e) e.IdArea = Area.IdArea).Distinct().ToArray

                For Each Sector In Sectores

                    NodoSector = tl.AppendNode(New Object() {Sector.IdSector, Sector.Name}, NodoArea)

                    Dim Tramos = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                                                                             Key .IdTramo = row.Field(Of Integer)("IdTramo"),
                                                                             Key .IdArea = row.Field(Of Integer)("IdArea"),
                                                                             Key .IdSector = row.Field(Of Integer)("IdSector"),
                                                                             Key .Name = row.Field(Of String)("Tramo")
                                                                         }).Where(Function(e) e.IdSector = Sector.IdSector AndAlso e.IdArea = Sector.IdArea).Distinct().ToArray

                    For Each Tramo In Tramos

                        NodoTramo = tl.AppendNode(New Object() {Tramo.IdTramo, Tramo.Name}, NodoSector)


                        Dim Ubicaciones = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                                                                                  Key .IdUbicacion = row.Field(Of Integer)("IdUbicacion"),
                                                                                  Key .IdArea = row.Field(Of Integer)("IdArea"),
                                                                                  Key .IdSector = row.Field(Of Integer)("IdSector"),
                                                                                  Key .IdTramo = row.Field(Of Integer)("IdTramo"),
                                                                                  Key .Name = row.Field(Of String)("Ubicacion"),
                                                                                  Key .IdOperador = IIf(IsDBNull(row.Item("idoperador")), 0, row.Item("idoperador"))
                                                                                }).Where(Function(e) e.IdTramo = Tramo.IdTramo _
                                                                                         AndAlso e.IdSector = Tramo.IdSector _
                                                                                         AndAlso e.IdArea = Tramo.IdArea).Distinct().ToArray

                        For Each Ubic In Ubicaciones
                            NodoUbicacion = tl.AppendNode(New Object() {Ubic.IdUbicacion, Ubic.Name, IIf(IsDBNull(Ubic.IdOperador), 0, Ubic.IdOperador.ToString())}, NodoTramo)
                        Next

                    Next

                Next

            Next

            tl.EndUnboundLoad()

            tl.ExpandAll()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Productos(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            dgridAsignacionProductos.ClearNodes()
            Listar_Productos_Asignados(dgridAsignacionProductos, lConnection, lTransaction)
            dgridAsignacionProductos.CollapseAll()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub Crea_Estructura_Ubicaciones(ByRef tl As TreeList, ByVal IdBodega As Integer)

        'Obtener las areas de la bodega
        Dim parentForRootNodes As TreeListNode = Nothing
        Dim NodoArea As TreeListNode
        Dim NodoSector As TreeListNode = Nothing
        Dim NodoTramo As TreeListNode = Nothing
        Dim NodoUbicacion As TreeListNode = Nothing

        Try

            tl.BeginUnboundLoad()

            DTUbicaciones = clsLnBodega.Get_Estructura_By_IdBodega_And_IdInventarioEnc(IdBodega,
                                                                                       gBeTransInvEnc.Idinventarioenc)

            Dim Areas = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                                                                 Key .IdArea = row.Field(Of Integer)("IdArea"),
                                                                 Key .Name = row.Field(Of String)("Area")
                                                             }).Distinct().ToArray().OrderBy(Function(x) x.IdArea)

            For Each Area In Areas

                NodoArea = tl.AppendNode(New Object() {"Área: " & Area.IdArea, Area.Name, ("A")}, parentForRootNodes)

                Dim Sectores = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                                                                         Key .IdSector = row.Field(Of Integer)("IdSector"),
                                                                         Key .IdArea = row.Field(Of Integer)("IdArea"),
                                                                         Key .Name = row.Field(Of String)("Sector")
                                                                     }).Where(Function(e) e.IdArea = Area.IdArea).Distinct().ToArray

                For Each Sector In Sectores

                    NodoSector = tl.AppendNode(New Object() {Sector.IdSector, Sector.Name}, NodoArea)

                    Dim Tramos = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                                                                             Key .IdTramo = row.Field(Of Integer)("IdTramo"),
                                                                             Key .IdArea = row.Field(Of Integer)("IdArea"),
                                                                             Key .IdSector = row.Field(Of Integer)("IdSector"),
                                                                             Key .Name = row.Field(Of String)("Tramo")
                                                                         }).Where(Function(e) e.IdSector = Sector.IdSector AndAlso e.IdArea = Sector.IdArea).Distinct().ToArray

                    For Each Tramo In Tramos

                        NodoTramo = tl.AppendNode(New Object() {Tramo.IdTramo, Tramo.Name}, NodoSector)


                        Dim Ubicaciones = DTUbicaciones.AsEnumerable().[Select](Function(row) New With {
                                                                                  Key .IdUbicacion = row.Field(Of Integer)("IdUbicacion"),
                                                                                  Key .IdArea = row.Field(Of Integer)("IdArea"),
                                                                                  Key .IdSector = row.Field(Of Integer)("IdSector"),
                                                                                  Key .IdTramo = row.Field(Of Integer)("IdTramo"),
                                                                                  Key .Name = row.Field(Of String)("Ubicacion"),
                                                                                  Key .IdOperador = IIf(IsDBNull(row.Item("idoperador")), 0, row.Item("idoperador"))
                                                                                }).Where(Function(e) e.IdTramo = Tramo.IdTramo _
                                                                                         AndAlso e.IdSector = Tramo.IdSector _
                                                                                         AndAlso e.IdArea = Tramo.IdArea).Distinct().ToArray

                        For Each Ubic In Ubicaciones
                            NodoUbicacion = tl.AppendNode(New Object() {Ubic.IdUbicacion, Ubic.Name, IIf(IsDBNull(Ubic.IdOperador), 0, Ubic.IdOperador.ToString())}, NodoTramo)
                        Next

                    Next

                Next

            Next

            tl.EndUnboundLoad()

            tl.ExpandAll()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Productos_Asignados(ByRef tl As TreeList, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim CountUbicacionesUnicas As Integer
        Dim CountProductosUnicos As Integer

        Try

            tl.BeginUnboundLoad()

            ListInventarioCiclico.Clear()

            tl.ClearNodes()

            ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(gBeTransInvEnc.Idinventarioenc,
                                                                                                                   AP.IdBodega,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)

            If ListInventarioCiclico.Count > 0 Then


                Dim ProductosUnicos = ListInventarioCiclico _
                                    .GroupBy(Function(x) x.Codigo) _
                                    .Select(Function(grupo) New With {
                                    Key .Codigo = grupo.Key,
                                    Key .Count = grupo.Count()
                                    })

                Dim UbicacionesUnicas = ListInventarioCiclico _
                                    .GroupBy(Function(x) x.IdUbicacion) _
                                    .Select(Function(grupo) New With {
                                    Key .IdUbicacion = grupo.Key,
                                    Key .Count = grupo.Count()
                                    })

                CountProductosUnicos = ProductosUnicos.Count()
                CountUbicacionesUnicas = UbicacionesUnicas.Count()

                Dim parentForRootNodes As TreeListNode = Nothing

                Dim rootNode As TreeListNode

                Dim Lista = From i In ListInventarioCiclico Group i By Keys = New With {
                                                                              Key i.IdProductoBodega,
                                                                              Key i.Codigo,
                                                                              Key i.Producto,
                                                                              Key i.UnidadMedida,
                                                                              Key i.TipoProducto,
                                                                              Key i.Operador,
                                                                              Key i.Idoperador,
                                                                              Key i.Ubicacion} Into Group
                            Select New With {Keys.Codigo, Keys.Producto, Keys.UnidadMedida,
                                             Keys.TipoProducto, Keys.IdProductoBodega,
                                             Keys.Operador, Keys.Idoperador, Keys.Ubicacion,
                                             .Inventario_Inicial = Group.Sum(Function(x) x.Cant_stock)}

                For Each BeTransInvCiclico In Lista

                    rootNode = tl.AppendNode(New Object() {BeTransInvCiclico.IdProductoBodega,
                                                           BeTransInvCiclico.Codigo,
                                                           BeTransInvCiclico.Producto,
                                                           BeTransInvCiclico.UnidadMedida,
                                                           BeTransInvCiclico.Operador,
                                                           BeTransInvCiclico.TipoProducto,
                                                           BeTransInvCiclico.Idoperador,
                                                           BeTransInvCiclico.Ubicacion}, parentForRootNodes)
                    rootNode.Expanded = True
                    rootNode.Tag = BeTransInvCiclico.IdProductoBodega

                Next

                '#GT18012025: mostrar conteo
                lblRegistros.Text = Lista.Count
                lblProductosUnicos.Text = CountProductosUnicos
                lblUbicacionesUnicas.Text = CountUbicacionesUnicas

            End If

            '#Inserta_Ubicaciones
            'Inserta_Ubicaciones(Obj.IdUbicacion)

            tl.EndUnboundLoad()

            '#GT18012025: se muestra conteo dentro de labels, ya que se requiere filtro unico por producto, ubicaciones y registros
            'dgridAsignacionProductos.OptionsView.ShowSummaryFooter = True
            'dgridAsignacionProductos.Columns(1).AllNodesSummary = True
            'dgridAsignacionProductos.Columns(1).SummaryFooterStrFormat = "Registros: {0:n0}"
            'dgridAsignacionProductos.Columns(1).SummaryFooterStrFormat = "Registros: {0:n0}"
            'dgridAsignacionProductos.Columns(1).SummaryFooter = SummaryItemType.Count

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Productos_Asignados(ByRef tl As TreeList)

        Try

            tl.BeginUnboundLoad()

            ListInventarioCiclico.Clear()

            tl.ClearNodes()

            ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(gBeTransInvEnc.Idinventarioenc,
                                                                                                                   AP.IdBodega)

            If ListInventarioCiclico.Count > 0 Then

                Dim parentForRootNodes As TreeListNode = Nothing

                Dim rootNode As TreeListNode
                Dim CountUbicacionesUnicas As Integer
                Dim CountProductosUnicos As Integer

                Dim Lista = From i In ListInventarioCiclico Group i By Keys = New With {
                                                                              Key i.IdProductoBodega,
                                                                              Key i.Codigo,
                                                                              Key i.Producto,
                                                                              Key i.UnidadMedida,
                                                                              Key i.TipoProducto,
                                                                              Key i.Operador,
                                                                              Key i.Idoperador,
                                                                              Key i.Ubicacion} Into Group
                            Select New With {Keys.Codigo, Keys.Producto, Keys.UnidadMedida,
                                             Keys.TipoProducto, Keys.IdProductoBodega,
                                             Keys.Operador, Keys.Idoperador, Keys.Ubicacion,
                                             .Inventario_Inicial = Group.Sum(Function(x) x.Cant_stock)}

                For Each BeTransInvCiclico In Lista

                    rootNode = tl.AppendNode(New Object() {BeTransInvCiclico.IdProductoBodega,
                                                           BeTransInvCiclico.Codigo,
                                                           BeTransInvCiclico.Producto,
                                                           BeTransInvCiclico.UnidadMedida,
                                                           BeTransInvCiclico.Operador,
                                                           BeTransInvCiclico.TipoProducto,
                                                           BeTransInvCiclico.Idoperador,
                                                           BeTransInvCiclico.Ubicacion}, parentForRootNodes)
                    rootNode.Expanded = True
                    rootNode.Tag = BeTransInvCiclico.IdProductoBodega

                Next

                Dim ProductosUnicos = ListInventarioCiclico _
                                    .GroupBy(Function(x) x.Codigo) _
                                    .Select(Function(grupo) New With {
                                    Key .Codigo = grupo.Key,
                                    Key .Count = grupo.Count()
                                    })

                Dim UbicacionesUnicas = ListInventarioCiclico _
                                    .GroupBy(Function(x) x.IdUbicacion) _
                                    .Select(Function(grupo) New With {
                                    Key .IdUbicacion = grupo.Key,
                                    Key .Count = grupo.Count()
                                    })

                CountProductosUnicos = ProductosUnicos.Count()
                CountUbicacionesUnicas = UbicacionesUnicas.Count()

                '#GT18012025: mostrar conteo
                lblRegistros.Text = Lista.Count
                lblProductosUnicos.Text = CountProductosUnicos
                lblUbicacionesUnicas.Text = CountUbicacionesUnicas

            End If

            '#Inserta_Ubicaciones
            'Inserta_Ubicaciones(Obj.IdUbicacion)

            tl.EndUnboundLoad()

            '#GT18012025: se muestra conteo dentro de labels, ya que se requiere filtro unico por producto, ubicaciones y registros
            'dgridAsignacionProductos.OptionsView.ShowSummaryFooter = True
            'dgridAsignacionProductos.Columns(1).AllNodesSummary = True
            'dgridAsignacionProductos.Columns(1).SummaryFooterStrFormat = "Productos: {0:n0}"
            'dgridAsignacionProductos.Columns(1).SummaryFooter = SummaryItemType.Count

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function AnularInventario() As Boolean

        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String

        Try

            us.IdUsuario = AP.UsuarioAp.IdUsuario
            clsLnUsuario.GetSingle(us)

            Try

                clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                If (clave = "") Then Throw New Exception("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.")

            Catch ex As Exception
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End Try

            Dim frmlog As New frmAjusteLogin() With {.clave = clave}

            If frmlog.ShowDialog() <> DialogResult.Yes Then
                frmlog.Dispose() : Return False
            End If

            frmlog.Dispose()

            Return True

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function Get_All_By_Comparacion_Inventario(ByVal pIdInv As Integer, lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_Comparacion_Inventario = Nothing

        Try

            Dim vSQL As String = "SELECT 
                    codigo AS Código,
                    producto AS Producto,
                    LoteOrigen AS LoteOrigen,
                    Lote,
                    FechaVence,
                    Licencia, 
                    EstadoOrigen,
                    EstadoDestino,
                    UbicacionOrigen,
                    UbicacionDestino,
                    Cantidad_Stock AS CantidadStock,
                    Peso_Stock AS PesoStock,
                    Cantidad AS CantidadConteo,
                    Peso AS PesoConteo,
                    Entradas,
                    Salidas,
                    Entradas_Salidas,

                    -- NuevoStock con múltiples condiciones 
                    CASE 
                        WHEN Cantidad = (Cantidad_Stock + Entradas ) AND Salidas = 0  THEN Cantidad
                        WHEN Salidas IS NOT NULL AND Salidas < 0 THEN (Cantidad_Stock + Salidas)
                        ELSE (Cantidad + Entradas_Salidas + Salidas)
                    END AS NuevoStock,

                    -- DiferenciaCantidad con múltiples condiciones 
                    CASE 
                        WHEN (Cantidad = (Cantidad_Stock + Entradas)) AND Salidas = 0 THEN (Cantidad_Stock + Entradas - Cantidad)
                        WHEN Salidas IS NOT NULL AND Salidas < 0 THEN  (Cantidad_Stock + Salidas - Cantidad) * -1
                        ELSE ((Cantidad_Stock + Entradas_Salidas) - Cantidad) * -1
                    END AS DiferenciaCantidad,

                    -- DiferenciaPeso se mantiene igual
                    (Peso_Stock - Peso) AS DiferenciaPeso,
                    Cantidad_Reservada_UmBas,
                    TieneReservaYConteoInsuficiente,  
                    Observacion
                FROM 
                    tempComparacionInventario
                WHERE 
                    IdInventario = @idinventarioenc;"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    Return lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Carga_Regularizacion(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)
        Try

            grdRegularizar.DataSource = clsLnTrans_inv_ciclico.Get_All_By_Regularizacion_Inventario(gBeTransInvEnc.Idinventarioenc,
                                                                                                    lConnection,
                                                                                                    lTransaction)

            If GridView1.RowCount > 0 Then

                GridView1.OptionsView.ShowFooter = True
                GridView1.BestFitColumns(True)

                'GridView1.Columns("Código").Group()

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "CantidadConteo",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("CantidadConteo")}
                GridView1.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "PesoConteo",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("PesoConteo")}
                GridView1.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "CantidadStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("CantidadStock")}
                GridView1.GroupSummary.Add(item2)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "PesoStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("PesoStock")}
                GridView1.GroupSummary.Add(item3)

                Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Entradas_Salidas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Entradas_Salidas")}
                GridView1.GroupSummary.Add(item4)

                Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "NuevoStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("NuevoStock")}
                GridView1.GroupSummary.Add(item5)

                Dim item6 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "DiferenciaCantidad",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("DiferenciaCantidad")}
                GridView1.GroupSummary.Add(item6)

                Dim item7 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "DiferenciaPeso",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("DiferenciaPeso")}
                GridView1.GroupSummary.Add(item7)

                Dim item8 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Entradas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Entradas")}
                GridView1.GroupSummary.Add(item8)

                Dim item9 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Salidas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Salidas")}
                GridView1.GroupSummary.Add(item9)

                Dim item10 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad_Reservada_UmBas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_Reservada_UmBas")}
                GridView1.GroupSummary.Add(item10)

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                GridView1.Columns("CantidadConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadConteo").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadConteo").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("CantidadStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadStock").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadStock").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Entradas_Salidas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Entradas_Salidas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Entradas_Salidas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Entradas_Salidas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("NuevoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("NuevoStock").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("NuevoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("NuevoStock").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("DiferenciaCantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("DiferenciaCantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("DiferenciaCantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("DiferenciaCantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("DiferenciaPeso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("DiferenciaPeso").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("DiferenciaPeso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("DiferenciaPeso").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Entradas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Entradas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Entradas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Entradas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Salidas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Salidas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Salidas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Salidas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Reservada_UmBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Reservada_UmBas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad_Reservada_UmBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Reservada_UmBas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("PesoConteo").Visible = False
                GridView1.Columns("PesoStock").Visible = False
                GridView1.Columns("DiferenciaPeso").Visible = False
                GridView1.ExpandAllGroups()

            End If

        Catch ex As Exception

        End Try

    End Sub

    '#MA20260105
    Private Sub Cargar_KPI_Ubicaciones(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try
            Dim dtPendientes As DataTable = clsLnBodega_ubicacion.Get_Ubicaciones_No_Contadas_DT(gBeTransInvEnc.Idinventarioenc,
                                                                                                  AP.IdBodega,
                                                                                                  lConnection,
                                                                                                  lTransaction)


            UbicacionesContadas = clsLnBodega_ubicacion.Get_Ubicaciones_Contadas(gBeTransInvEnc.Idinventarioenc,
                                                                                 AP.IdBodega,
                                                                                 lConnection,
                                                                                 lTransaction)

            TotalUbicaciones = clsLnBodega_ubicacion.Get_Total_Ubicaciones_Asig(gBeTransInvEnc.Idinventarioenc,
                                                                                AP.IdBodega,
                                                                                lConnection,
                                                                                lTransaction)

            UbicacionesPendientes = TotalUbicaciones - UbicacionesContadas

            If TotalUbicaciones <= 0 Then TotalUbicaciones = 1

            Dim porcentaje As Double = (UbicacionesContadas / TotalUbicaciones) * 100
            porcentaje = Math.Min(Math.Max(porcentaje, 0), 100)

            ArcScaleComponent2.Value = porcentaje

            lblGaugeUbicaciones.Text = $"Contadas: {UbicacionesContadas} / Total: {TotalUbicaciones} (Pendientes: {UbicacionesPendientes})"

            lblGaugeUbicaciones.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        Catch ex As Exception
            XtraMessageBox.Show("Error al cargar KPI de ubicaciones: " & ex.Message,
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    '#MA20260105
    Private Sub Actualizar_Gauge_Ubicaciones(lConnection As SqlConnection, lTransaction As SqlTransaction)

        Try
            Dim dtPendientes As DataTable = clsLnBodega_ubicacion.Get_Ubicaciones_No_Contadas_DT(gBeTransInvEnc.Idinventarioenc,
                                                                                                  AP.IdBodega,
                                                                                                  lConnection,
                                                                                                  lTransaction)

            dgridUbicacionesNoContadas.DataSource = dtPendientes

            With GridViewUbicacionesNoContadas
                .PopulateColumns()
                .OptionsView.ShowIndicator = True
                .OptionsView.ColumnAutoWidth = False
                .OptionsView.ShowFooter = True

                If .Columns("IdUbicacion") IsNot Nothing Then
                    .Columns("IdUbicacion").Caption = "ID Ubicación"
                    .Columns("IdUbicacion").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .Columns("IdUbicacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    .Columns("IdUbicacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    .Columns("IdUbicacion").SummaryItem.DisplayFormat = "Registros: {0}"
                End If

                If .Columns("Ubicacion") IsNot Nothing Then .Columns("Ubicacion").Caption = "Ubicación Completa"
                If .Columns("Area") IsNot Nothing Then .Columns("Area").Caption = "Área"
                If .Columns("Sector") IsNot Nothing Then .Columns("Sector").Caption = "Sector"
                If .Columns("Tramo") IsNot Nothing Then .Columns("Tramo").Caption = "Tramo"

                .BestFitColumns()
                .OptionsView.ColumnAutoWidth = True
                .Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                .Appearance.HeaderPanel.Font = New Font(.Appearance.HeaderPanel.Font, FontStyle.Bold)
                .RowHeight = 28
                .OptionsBehavior.Editable = False
                .OptionsView.ShowAutoFilterRow = True
            End With

            UbicacionesContadas = clsLnBodega_ubicacion.Get_Ubicaciones_Contadas(gBeTransInvEnc.Idinventarioenc,
                                                                                 AP.IdBodega,
                                                                                 lConnection,
                                                                                 lTransaction)

            TotalUbicaciones = clsLnBodega_ubicacion.Get_Total_Ubicaciones_Asig(gBeTransInvEnc.Idinventarioenc,
                                                                                AP.IdBodega,
                                                                                lConnection,
                                                                                lTransaction)
            UbicacionesPendientes = TotalUbicaciones - UbicacionesContadas

            If TotalUbicaciones <= 0 Then TotalUbicaciones = 1

            Dim porcentaje As Double = (UbicacionesContadas / TotalUbicaciones) * 100
            porcentaje = Math.Min(Math.Max(porcentaje, 0), 100)
            'porcentaje = 10.0  ' <-- Descomenta si quieres comprobar que funciona el gauge

            Me.Invoke(Sub() Animar_Gauge_Ubicaciones(porcentaje))

            lblGaugeUbicaciones.Text = $"Contadas: {UbicacionesContadas} / Total: {TotalUbicaciones} (Pendientes: {UbicacionesPendientes})"

            lblGaugeUbicaciones.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        Catch ex As Exception
            XtraMessageBox.Show("Error al actualizar Gauge de ubicaciones: " & ex.Message,
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    '#MA20260105
    Private Sub Animar_Gauge_Ubicaciones(valorFinal As Double)
        If valorFinal < 0 Then valorFinal = 0
        If valorFinal > 100 Then valorFinal = 100

        ValorObjetivoGauge = valorFinal

        Timer1.Interval = 20
        Timer1.Start()
    End Sub

    '#MA20260105
    Private Sub tmrGauge_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim paso As Double = 2.5

        If ArcScaleComponent2.Value < ValorObjetivoGauge Then
            ArcScaleComponent2.Value = Math.Min(ArcScaleComponent2.Value + paso, ValorObjetivoGauge)
        ElseIf ArcScaleComponent2.Value > ValorObjetivoGauge Then
            ArcScaleComponent2.Value = Math.Max(ArcScaleComponent2.Value - paso, ValorObjetivoGauge)
        End If

        If ArcScaleComponent2.Value = ValorObjetivoGauge Then Timer1.Stop()

        gaugeUbicaciones.Update()
        gaugeUbicaciones.Refresh()
        Application.DoEvents()
    End Sub

    Private Sub frmInventario_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        InvokeListarInventario?.Invoke()
    End Sub

    '#MA20260108 Agregar
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim frm As New frmInventarioDet(frmInventarioDet.TipoTrans.Nuevo)
        frm.IdInventario = gBeTransInvEnc.Idinventarioenc
        frm.IdBodega = AP.IdBodega

        If frm.ShowDialog(Me) = DialogResult.OK Then
            Using cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                cn.Open()
                Calcular_Inventario_Teorico_WMS(cn, Nothing)
            End Using
        End If
    End Sub
End Class
