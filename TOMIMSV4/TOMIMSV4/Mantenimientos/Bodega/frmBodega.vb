Imports System.IO
Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraPrinting.BarCode.Native
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class frmBodega

    Public pBeBodega As New clsBeBodega

    Private ReadOnly pObjBAB As New clsBeBodega_area
    Private ReadOnly pObjBS As New clsBeBodega_sector
    Private ReadOnly pObjBT As New clsBeBodega_tramo
    Private ReadOnly pBeBodegaUbicacion As New clsBeBodega_ubicacion

    Private ReadOnly ObjBP As New clsBeBodega_monitor_parametro
    Private ReadOnly clsLnBodegaP As New clsLnBodega_monitor_parametro

    Private pNuevoParametro As Boolean = True

    Private ReadOnly Usuario As New clsBeUsuario

    Private objUsuarioBodega As New clsBeUsuario_bodega

    Private pBodegaEmp As Boolean = False
    Private pNuevoSector As Boolean = True
    Private pNuevoTramo As Boolean = True
    Private pNuevaUbicacion As Boolean = True

    Private DTArea As DataTable
    Private DTSector As DataTable
    Private DTTramo As DataTable
    Private DTUbiacion As DataTable

    Private pListObjBodegaAreas As New List(Of clsBeBodega_area)

    Public Delegate Sub Listar_Bodegas()
    Public Property Listar As Listar_Bodegas

    Private BeINavConfigEnc As New clsBeI_nav_config_enc()

    'GT04042022: filas seleccionas del grid para aplicar update por tab
    Private pUbicacionRow As DataRowView
    Private pTramoRow As DataRowView

    Private FiltroCentroCosto As Integer = 0
    Private FiltroCentroCostoDirERP As Integer = 0
    Private FiltroCentroCostoDepERP As Integer = 0

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

    Private Sub frmBodega_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Check_Parametro_Interface()

        If BeINavConfigEnc IsNot Nothing Then

            If BeINavConfigEnc.Centro_Costo_Erp > 0 AndAlso BeINavConfigEnc.Centro_Costo_Dep_Erp > 0 AndAlso BeINavConfigEnc.Centro_Costo_Dir_Erp > 0 Then

                FiltroCentroCosto = BeINavConfigEnc.Centro_Costo_Erp
                FiltroCentroCostoDepERP = BeINavConfigEnc.Centro_Costo_Dep_Erp
                FiltroCentroCostoDirERP = BeINavConfigEnc.Centro_Costo_Dir_Erp

                Carga_Centro_Costo_Dir_Depto_ERP()

            End If

        End If

        ImplementarBarra()
        CargaSimbolosCodigoBarra()
        CargaComboEtiquetas()
        Buscar_Registro()
        '#MA20260225 Estado defecto rack
        CargarComboEstadosRack()
        txtCodigo.Focus()

    End Sub

    Private Sub Check_Parametro_Interface()

        Try

            If Not pBeBodega Is Nothing Then
                BeINavConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pBeBodega.IdBodega, AP.IdEmpresa)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Buscar_Registro()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Por favor espere....")

            mnuDiseñoGrafico.Enabled = False

            pListObjBodegaAreas = clsLnBodega_area.GetAllByAreaBodega(chkActivoAreaBodega.Checked, pBeBodega.IdBodega)

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_Paises(cmbPais) Then
                XtraMessageBox.Show("No hay paises definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            IMS.Listar_ProductoEstadoNE(cmbEstadoNe)

            cmbTipoRotacion.ItemIndex = -1
            If Not IMS.Listar_TipoRotacion(cmbTipoRotacion) Then
                XtraMessageBox.Show("No hay tipos de rotación definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            cmbIndiceRotacion.ItemIndex = -1
            If Not IMS.Listar_IndiceRotacion(cmbIndiceRotacion) Then
                XtraMessageBox.Show("No hay indices de rotación definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            cmbFont.ItemIndex = -1

            If Not IMS.Listar_FontTramo(cmbFont) Then
                XtraMessageBox.Show("No hay fonts de tramo definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Llena_Orientacion()

            Check_Parametro_Interface()

            Select Case Modo

                Case TipoTrans.Nuevo

                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrTextEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modTextEdit.Text = Now
                    chkSistemaUbicacion.Enabled = True

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    cmbEmpresa.Enabled = True
                    mnuDiseñoGrafico.Enabled = False
                    mnuParametrosInterface.Enabled = Not (BeINavConfigEnc Is Nothing)
                    cmdImprimir.Enabled = False

                    ControlPanelBodega.TabPages.Remove(tabArea)
                    ControlPanelBodega.TabPages.Remove(tabSector)
                    ControlPanelBodega.TabPages.Remove(tabTramo)
                    ControlPanelBodega.TabPages.Remove(TabUbicacion)
                    ControlPanelBodega.TabPages.Remove(tabReferencia)
                    ControlPanelBodega.TabPages.Remove(tabParametros)

                Case TipoTrans.Editar

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuDiseñoGrafico.Enabled = False
                    mnuEstructuraInicial.Enabled = False
                    cmdRefrescar.Enabled = True
                    mnuParametrosInterface.Enabled = Not (BeINavConfigEnc Is Nothing)
                    mnuUnificarBodegas.Enabled = False
                    cmdImprimir.Enabled = True

                    Cargar_Bodega()

                    Cargar_Bodega_Areas()

                    Cargar_Parametros_Bodega()

                    Llena_Bodega()

                    If Not pBeBodega Is Nothing Then

                        If Not IMS.Listar_Areas_By_Bodega(cmbArea, pBeBodega.IdBodega) Then
                        ElseIf Not IMS.Listar_Areas_By_Bodega(cmbAreasR, pBeBodega.IdBodega) Then
                        ElseIf Not IMS.Listar_Areas_By_Bodega(cmbAreaUbic, pBeBodega.IdBodega) Then
                        ElseIf Not IMS.Listar_Sectores_By_Area(cmbSector, cmbArea.EditValue, pBeBodega.IdBodega) Then
                        ElseIf Not IMS.Listar_TipoRotacion(cmbTipoRotacion) Then
                        ElseIf Not IMS.Listar_IndiceRotacion(cmbIndiceRotacion) Then
                        ElseIf Not IMS.Listar_FontTramo(cmbFont) Then
                        End If

                    End If

                    cmbTipoRotacion.ItemIndex = -1
                    cmbIndiceRotacion.ItemIndex = -1
                    cmbFont.ItemIndex = -1
                    cmbOrientacion.ItemIndex = -1

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuDiseñoGrafico.Enabled = True
                    mnuEstructuraInicial.Enabled = True
                    mnuUnificarBodegas.Enabled = True
                    mnuParametrosInterface.Enabled = Not (BeINavConfigEnc Is Nothing)

                    '#EJC20220427:Listar ubicaciones (todas) en grid.
                    Llena_Grid_Ubicaciones()

            End Select

            Application.DoEvents()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Llena_Grid_Ubicaciones()

        Try

            If pBeBodega Is Nothing Then Exit Sub

            Dim DT As New DataTable

            DT = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_IdBodega_DT(pBeBodega.IdBodega)

            dgridUbicaciones.DataSource = DT

            If GridView4.Columns.Count > 0 Then
                GridView4.BestFitColumns()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private Shared Function Correlativo() As String

        Correlativo = "B"

        Try

            Dim MaxID As String = clsLnBodega.MaxID.ToString
            Dim Longitud As Integer = Len(MaxID)

            Select Case Longitud

                Case 1
                    Correlativo += "000000" & MaxID
                Case 2
                    Correlativo += "00000" & MaxID
                Case 3
                    Correlativo += "0000" & MaxID
                Case 3
                    Correlativo += "000" & MaxID
                Case 4
                    Correlativo += "00" & MaxID
                Case 5
                    Correlativo += "0" & MaxID
                Case 6
                    Correlativo += MaxID

                Case Else
                    Exit Select
            End Select

            Return Correlativo

        Catch ex As Exception
            Return "B0"
        End Try

    End Function

    Private Function Guardar_Bodega_Sector() As Boolean

        Guardar_Bodega_Sector = False

        Try

            pObjBS.IdSector = clsLnBodega_sector.MaxID(pBeBodega.IdBodega)
            pObjBS.IdArea = cmbArea.EditValue
            pObjBS.IdBodega = pBeBodega.IdBodega
            pObjBS.Codigo = txtCodigoSector.Text.Trim()
            pObjBS.Descripcion = txtDescripcionSector.Text.Trim()
            pObjBS.Sistema = chkSistemaSector.Checked
            pObjBS.User_agr = AP.UsuarioAp.IdUsuario
            pObjBS.Fec_agr = Now
            pObjBS.User_mod = AP.UsuarioAp.IdUsuario
            pObjBS.Fec_mod = Now
            pObjBS.Activo = chkActivoSector.Checked()
            pObjBS.Alto = nUpdAltoSector.Value
            pObjBS.Largo = nUpdLargoSector.Value
            pObjBS.Ancho = nUpdAnchoSector.Value
            pObjBS.Margen_izquierdo = nUpdMargenIzquierdoSector.Value
            pObjBS.Margen_derecho = nUpdMargenDerechoSector.Value
            pObjBS.Margen_superior = nUpdMargenSuperiorSector.Value
            pObjBS.Margen_inferior = nUpdMargenInferiorSector.Value
            pObjBS.Horizontal = chkHorizontal.Checked
            pObjBS.Pos_x = txtPosX.Value
            pObjBS.Pos_y = txtPosY.Value
            Guardar_Bodega_Sector = clsLnBodega_sector.Insertar(pObjBS) > 0
            Cargar_Bodega_Sector(pObjBAB.IdArea)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Guardar_Bodega_Tramo() As Boolean

        Guardar_Bodega_Tramo = False

        Try

            pObjBT.IdSector = cmbSector.EditValue
            pObjBT.IdTramo = clsLnBodega_tramo.MaxID(pBeBodega.IdBodega)
            pObjBT.IdArea = cmbAreasR.EditValue
            pObjBT.IdBodega = pBeBodega.IdBodega
            pObjBT.Codigo = pObjBT.IdTramo 'txtCodigoTramo.Text.Trim()
            pObjBT.Descripcion = txtDescripcionTramo.Text.Trim()
            pObjBT.Sistema = chkSistemaTramo.Checked
            pObjBT.Es_Rack = chkEsRack.Checked
            pObjBT.User_agr = AP.UsuarioAp.IdUsuario
            pObjBT.Fec_agr = Now
            pObjBT.User_mod = AP.UsuarioAp.IdUsuario
            pObjBT.Fec_mod = Now
            pObjBT.Activo = chkActivoTramo.Checked()
            pObjBT.Alto = nUpdAltoTramo.Value
            pObjBT.Largo = nUpdLargoTramo.Value
            pObjBT.Ancho = nUpdAnchoTramo.Value
            pObjBT.Margen_izquierdo = nUpdMargenIzquierdoTramo.Value
            pObjBT.Margen_derecho = nUpdMargenDerechoTramo.Value
            pObjBT.Margen_superior = nUpdMargenSuperiorTramo.Value
            pObjBT.Margen_inferior = nUpdMargenInferiorTramo.Value
            pObjBT.IdFontEnc = cmbFont.EditValue
            pObjBT.Horizontal = chkOrientacion.Checked
            pObjBT.IdTipoRack = txtTipoRack.Text
            pObjBT.Indice_x = IIf(txtIndice.Text = "", 1, txtIndice.Text)
            pObjBT.Orientacion = chkOrient.Checked

            Guardar_Bodega_Tramo = clsLnBodega_tramo.Insertar(pObjBT) > 0
            Cargar_Bodega_Tramo(pObjBT.IdSector)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Guardar_Bodega_Ubicacion() As Boolean

        Guardar_Bodega_Ubicacion = False

        Try

            pBeBodegaUbicacion.IdUbicacion = clsLnBodega_ubicacion.MaxID(pBeBodega.IdBodega)

            pBeBodegaUbicacion.IdBodega = pBeBodega.IdBodega
            pBeBodegaUbicacion.IdArea = cmbAreaUbic.EditValue
            pBeBodegaUbicacion.IdSector = cmbSectorR.EditValue
            pBeBodegaUbicacion.IdTramo = cmbTramo.EditValue

            pBeBodegaUbicacion.Sistema = chkSistemaUbicacion.Checked
            pBeBodegaUbicacion.Descripcion = txtDescripcionUbicacion.Text.Trim()

            '#EJC20220329: Guardar el codigo barra del textbox, parametrizar después...
            pBeBodegaUbicacion.Codigo_barra = txtCodigoBarraUbicacion.Text.Trim()
            pBeBodegaUbicacion.Codigo_barra2 = Mid(100000 + pBeBodegaUbicacion.IdUbicacion, 2, 5)

            'pObjBU.Codigo_barra = Mid(CStr(100000 + pObjBU.IdUbicacion), 2, 5)
            'pObjBU.Codigo_barra2 = pObjBU.Descripcion

            pBeBodegaUbicacion.User_agr = AP.UsuarioAp.IdUsuario
            pBeBodegaUbicacion.Fec_agr = Now
            pBeBodegaUbicacion.User_mod = AP.UsuarioAp.IdUsuario
            pBeBodegaUbicacion.Fec_mod = Now
            pBeBodegaUbicacion.Dañado = chkDañadoUbicacion.Checked
            pBeBodegaUbicacion.Alto = nUpdAltoUbicacion.Value
            pBeBodegaUbicacion.Largo = nUpdLargoUbicacion.Value
            pBeBodegaUbicacion.Ancho = nUpdAnchoUbicacion.Value
            pBeBodegaUbicacion.Activo = chkActivoUbicacion.Checked
            pBeBodegaUbicacion.Bloqueada = chkBloqueadaUbicacion.Checked
            pBeBodegaUbicacion.Nivel = nUpdNivelUbicacion.Value
            pBeBodegaUbicacion.Acepta_pallet = chkAceptaPalletUbicacion.Checked
            pBeBodegaUbicacion.Ubicacion_picking = chkUbicacionPicking.Checked
            pBeBodegaUbicacion.Ubicacion_recepcion = chkRecepcion.Checked
            pBeBodegaUbicacion.Ubicacion_despacho = chkDespacho.Checked
            pBeBodegaUbicacion.Ubicacion_merma = chkMerma.Checked
            pBeBodegaUbicacion.Ubicacion_Virtual = chkEsBodegaVirtual.Checked
            pBeBodegaUbicacion.Margen_izquierdo = nUpdMargenIzquierdoUbicacion.Value
            pBeBodegaUbicacion.Margen_derecho = nUpdMargenDerechoUbicacion.Value
            pBeBodegaUbicacion.Margen_superior = nUpdMargenSuperiorUbicacion.Value
            pBeBodegaUbicacion.Margen_inferior = nUpdMargenInferiorUbicacion.Value
            pBeBodegaUbicacion.Orientacion_pos = cmbOrientacion.EditValue()
            pBeBodegaUbicacion.ubicacion_ne = chkUbicPrdNE.Checked
            pBeBodegaUbicacion.Ubicacion_muelle = chkUbicacionMuelle.Checked

            If cmbIndiceRotacion.ItemIndex = -1 Then
                pBeBodegaUbicacion.IdIndiceRotacion = Nothing
            Else
                pBeBodegaUbicacion.IdIndiceRotacion = cmbTipoRotacion.EditValue
            End If

            If cmbTipoRotacion.ItemIndex = -1 Then
                pBeBodegaUbicacion.IdTipoRotacion = Nothing
            Else
                pBeBodegaUbicacion.IdTipoRotacion = cmbTipoRotacion.EditValue
            End If

            pBeBodegaUbicacion.Indice_x = txtIndiceX.Value

            Guardar_Bodega_Ubicacion = clsLnBodega_ubicacion.Insertar(pBeBodegaUbicacion) > 0

            Cargar_Bodega_Ubicacion(pBeBodegaUbicacion.IdTramo)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar_Bodega_Area() As Boolean

        Actualizar_Bodega_Area = False

        Try

            Dim Dr As DataRowView = GridViewArea.GetFocusedRow
            Dim lIndex As Integer = -1

            If Not Dr Is Nothing Then
                lIndex = pListObjBodegaAreas.FindIndex(Function(b) b.IdArea = Dr.Item("Correlativo"))
            End If

            If lIndex > -1 Then

                If pListObjBodegaAreas(lIndex).IsNew Then
                    pListObjBodegaAreas(lIndex).IdArea = clsLnBodega_area.MaxID(pBeBodega.IdBodega)
                    Actualizar_Bodega_Area = clsLnBodega_area.Insertar(pListObjBodegaAreas(lIndex)) > 0
                Else
                    'GT19012022: obtendo el registro para actualizar y le seteo los valores de los inputs
                    Dim bodega_area = pListObjBodegaAreas(lIndex)
                    bodega_area.Descripcion = txtDescripcionAreaBodega.Text.Trim
                    bodega_area.Alto = txtAlto.Value
                    bodega_area.Largo = txtLargo.Value
                    bodega_area.Ancho = txtAncho.Value
                    bodega_area.Grupo = txtGrupoArea.Text.Trim
                    If Not String.IsNullOrEmpty(txtUbicacionRecepcionArea.EditValue) Then
                        bodega_area.IdUbicacionRef = txtUbicacionRecepcionArea.EditValue
                    End If

                    'GT19012022: con el objeto actualizado, lo asigno a la lista, en el index que corresponde a ser enviado
                    pListObjBodegaAreas(lIndex) = bodega_area

                    Actualizar_Bodega_Area = clsLnBodega_area.Actualizar(pListObjBodegaAreas(lIndex)) > 0
                End If

            End If

            Cargar_Bodega_Areas()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar_Bodega_Area(ByVal IndiceLista As Integer) As Boolean

        Actualizar_Bodega_Area = False

        Try

            Dim lIndex As Integer = IndiceLista

            If lIndex > -1 Then

                If pListObjBodegaAreas(lIndex).IsNew Then
                    pListObjBodegaAreas(lIndex).IdArea = clsLnBodega_area.MaxID(pBeBodega.IdBodega)
                    Actualizar_Bodega_Area = clsLnBodega_area.Insertar(pListObjBodegaAreas(lIndex)) > 0
                Else
                    Actualizar_Bodega_Area = clsLnBodega_area.Actualizar(pListObjBodegaAreas(lIndex)) > 0
                End If

            Else
                Throw New Exception("Índice de lista incorrecto al guardar el área")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar_Bodega_Sector() As Boolean

        Actualizar_Bodega_Sector = False

        Try

            ' Dim Dr As DataRowView = GridViewSec.GetFocusedRow
            pTramoRow = GridViewSec.GetFocusedRow

            If Not pTramoRow Is Nothing Then

                pObjBS.IdSector = CInt(pTramoRow.Item("IdSector"))
                pObjBS.IdArea = cmbArea.EditValue
                pObjBS.IdBodega = pBeBodega.IdBodega
                pObjBS.Codigo = txtCodigoSector.Text.Trim()
                pObjBS.Descripcion = txtDescripcionSector.Text.Trim()
                pObjBS.Sistema = chkSistemaSector.Checked
                pObjBS.User_agr = AP.UsuarioAp.IdUsuario
                pObjBS.Fec_agr = Now
                pObjBS.User_mod = AP.UsuarioAp.IdUsuario
                pObjBS.Fec_mod = Now
                pObjBS.Activo = chkActivoSector.Checked()
                pObjBS.Alto = nUpdAltoSector.Value
                pObjBS.Largo = nUpdLargoSector.Value
                pObjBS.Ancho = nUpdAnchoSector.Value
                pObjBS.Margen_izquierdo = nUpdMargenIzquierdoSector.Value
                pObjBS.Margen_derecho = nUpdMargenDerechoSector.Value
                pObjBS.Margen_superior = nUpdMargenSuperiorSector.Value
                pObjBS.Margen_inferior = nUpdMargenInferiorSector.Value
                pObjBS.Horizontal = chkHorizontal.Checked
                pObjBS.Pos_x = txtPosX.Value
                pObjBS.Pos_y = txtPosY.Value
                Actualizar_Bodega_Sector = clsLnBodega_sector.Actualizar(pObjBS) > 0
                Cargar_Bodega_Tramo(pObjBT.IdSector)

            Else
                Throw New Exception("Debe seleccionar un sector del grid primero para actualizar")
            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar_Bodega_Tramo() As Boolean

        Actualizar_Bodega_Tramo = False

        Try

            'GT04042022: la fila se obtiene del doble clic en el grid, sino tomara la primera que encuentre
            'Dim Dr As DataRowView = GridViewTramo.GetFocusedRow

            If Not pTramoRow Is Nothing Then

                pObjBT.IdTramo = CInt(pTramoRow.Item("Correlativo"))
                pObjBT.IdSector = cmbSector.EditValue
                pObjBT.IdArea = cmbAreasR.EditValue
                pObjBT.IdBodega = pBeBodega.IdBodega
                pObjBT.Codigo = txtCodigoTramo.Text.Trim()
                pObjBT.Descripcion = txtDescripcionTramo.Text.Trim()
                pObjBT.Sistema = chkSistemaTramo.Checked
                pObjBT.Es_Rack = chkEsRack.Checked
                pObjBT.User_agr = AP.UsuarioAp.IdUsuario
                pObjBT.Fec_agr = Now
                pObjBT.User_mod = AP.UsuarioAp.IdUsuario
                pObjBT.Fec_mod = Now
                pObjBT.Activo = chkActivoTramo.Checked()
                pObjBT.Alto = nUpdAltoTramo.Value
                pObjBT.Largo = nUpdLargoTramo.Value
                pObjBT.Ancho = nUpdAnchoTramo.Value
                pObjBT.Margen_izquierdo = nUpdMargenIzquierdoTramo.Value
                pObjBT.Margen_derecho = nUpdMargenDerechoTramo.Value
                pObjBT.Margen_superior = nUpdMargenSuperiorTramo.Value
                pObjBT.Margen_inferior = nUpdMargenInferiorTramo.Value
                pObjBT.IdFontEnc = cmbFont.EditValue
                pObjBT.Horizontal = chkOrientacion.Checked
                pObjBT.IdTipoRack = txtTipoRack.Text
                pObjBT.Indice_x = IIf(txtIndice.Text = "", 1, txtIndice.Text)
                pObjBT.Orientacion = chkOrient.Checked
                Actualizar_Bodega_Tramo = clsLnBodega_tramo.Actualizar(pObjBT) > 0
                Cargar_Bodega_Tramo(pObjBT.IdSector)
            Else
                Throw New Exception("Debe seleccionar primero un registro de Detalle Tramos para actualizar.")
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function



    Private Function Actualizar_Bodega_Ubicacion() As Boolean

        Try

            'GT04042022: la fila se obtiene del doble clic en el grid, sino tomara la primera que encuentre
            'Dim Dr As DataRowView = GridViewUbi.GetFocusedRow

            'GT31032022: si no se selecciona el registro del grid, no tendra un IdUbicacion
            If Not pUbicacionRow Is Nothing Then
                pBeBodegaUbicacion.IdUbicacion = CInt(pUbicacionRow.Item("IdUbicacion"))
                pBeBodegaUbicacion.IdBodega = pBeBodega.IdBodega
                pBeBodegaUbicacion.IdArea = cmbAreaUbic.EditValue
                pBeBodegaUbicacion.IdSector = cmbSectorR.EditValue
                pBeBodegaUbicacion.IdTramo = cmbTramo.EditValue
                pBeBodegaUbicacion.IdIndiceRotacion = cmbIndiceRotacion.EditValue
                pBeBodegaUbicacion.IdTipoRotacion = cmbTipoRotacion.EditValue
                pBeBodegaUbicacion.Sistema = chkSistemaUbicacion.Checked
                pBeBodegaUbicacion.Descripcion = txtDescripcionUbicacion.Text.Trim()
                pBeBodegaUbicacion.Codigo_barra = txtCodigoBarraUbicacion.Text.Trim()
                pBeBodegaUbicacion.Codigo_barra2 = txtCodigoBarra2ubicacion.Text.Trim()
                pBeBodegaUbicacion.User_agr = AP.UsuarioAp.IdUsuario
                pBeBodegaUbicacion.Fec_agr = Now
                pBeBodegaUbicacion.User_mod = AP.UsuarioAp.IdUsuario
                pBeBodegaUbicacion.Fec_mod = Now
                pBeBodegaUbicacion.Dañado = chkDañadoUbicacion.Checked
                pBeBodegaUbicacion.Alto = nUpdAltoUbicacion.Value
                pBeBodegaUbicacion.Largo = nUpdLargoUbicacion.Value
                pBeBodegaUbicacion.Ancho = nUpdAnchoUbicacion.Value
                pBeBodegaUbicacion.Activo = chkActivoUbicacion.Checked
                pBeBodegaUbicacion.Bloqueada = chkBloqueadaUbicacion.Checked
                pBeBodegaUbicacion.Nivel = nUpdNivelUbicacion.Value
                pBeBodegaUbicacion.Indice_x = txtIndiceX.Value
                pBeBodegaUbicacion.Acepta_pallet = chkAceptaPalletUbicacion.Checked
                pBeBodegaUbicacion.Ubicacion_recepcion = chkRecepcion.Checked
                pBeBodegaUbicacion.Ubicacion_despacho = chkDespacho.Checked
                pBeBodegaUbicacion.Ubicacion_merma = chkMerma.Checked
                pBeBodegaUbicacion.Ubicacion_Virtual = chkEsBodegaVirtual.Checked
                pBeBodegaUbicacion.Ubicacion_picking = chkUbicacionPicking.Checked
                pBeBodegaUbicacion.Margen_izquierdo = nUpdMargenIzquierdoUbicacion.Value
                pBeBodegaUbicacion.Margen_derecho = nUpdMargenDerechoUbicacion.Value
                pBeBodegaUbicacion.Margen_superior = nUpdMargenSuperiorUbicacion.Value
                pBeBodegaUbicacion.Margen_inferior = nUpdMargenInferiorUbicacion.Value
                pBeBodegaUbicacion.Orientacion_pos = cmbOrientacion.EditValue()
                pBeBodegaUbicacion.ubicacion_ne = chkUbicPrdNE.Checked
                pBeBodegaUbicacion.Ubicacion_muelle = chkUbicacionMuelle.Checked

                If pBeBodegaUbicacion.Orientacion_pos Is Nothing Then
                    pBeBodegaUbicacion.Orientacion_pos = ""
                End If

                Actualizar_Bodega_Ubicacion = clsLnBodega_ubicacion.Actualizar(pBeBodegaUbicacion)
            Else
                Throw New Exception("Debe seleccionar una ubicación del Detalle Ubicaciones para actualizar")
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Cargar_Bodega_Areas()

        Try

            If pListObjBodegaAreas IsNot Nothing AndAlso pListObjBodegaAreas.Count > 0 Then

                Dim DT As New DataTable("BodegaArea")
                DT.Columns.Add("Correlativo", GetType(Integer))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Descripción", GetType(String))
                DT.Columns.Add("Grupo", GetType(String))
                DT.Columns.Add("UbicacionRef", GetType(String))

                Parallel.ForEach(pListObjBodegaAreas.FindAll(Function(b) b.Activo = chkAreasBodegaActivos.Checked), Sub(Obj As clsBeBodega_area)
                                                                                                                        SyncLock DT
                                                                                                                            Dim lRow As DataRow = DT.NewRow()
                                                                                                                            lRow(0) = Obj.IdArea
                                                                                                                            lRow(1) = Obj.Codigo
                                                                                                                            lRow(2) = Obj.Descripcion
                                                                                                                            lRow(3) = Obj.Grupo
                                                                                                                            lRow(4) = Obj.IdUbicacionRef
                                                                                                                            DT.Rows.Add(lRow)
                                                                                                                        End SyncLock
                                                                                                                    End Sub)

                grdAreaBodega.DataSource = DT

                If GridViewArea.Columns.Count > 0 Then GridViewArea.Columns(0).Visible = False

                Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Count, "Código", "Areas={0}")

                If GridViewArea.Columns("Código").Summary.Count = 0 Then
                    GridViewArea.Columns("Código").Summary.Add(item)
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

    Private Sub Cargar_Bodega_Sector(ByVal pIdArea As Integer)

        Try

            Dim DT As New DataTable
            DT = clsLnBodega_sector.Get_All_By_IdArea_And_IdBodega_DT(chkSectoresActivos.Checked,
                                                                      pIdArea,
                                                                      pBeBodega.IdBodega)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    grdSectorArea.DataSource = DT

                    Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Count, "Correlativo", "Secotres={0}")

                    If GridViewSec.Columns("IdSector").Summary.Count = 0 Then
                        GridViewSec.Columns("IdSector").Summary.Add(item)
                    End If

                    If GridViewSec.Columns.Count > 0 Then
                        GridViewSec.BestFitColumns()
                    End If

                Else
                    grdSectorArea.DataSource = Nothing
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

    Private Sub Cargar_Bodega_Tramo(ByVal pIdSector As Integer)

        Try

            Dim ListObjTB As New List(Of clsBeBodega_tramo)
            ListObjTB = clsLnBodega_tramo.Get_All_By_IdSector_And_IdBodega(chkTramosActivos.Checked, pIdSector, pBeBodega.IdBodega)

            If ListObjTB.Count > 0 Then

                Dim DT As New DataTable("TramoSector")
                DT.Columns.Add("Correlativo", GetType(Integer))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Descripcion", GetType(String))

                Parallel.ForEach(ListObjTB, Sub(ByVal Obj As clsBeBodega_tramo)
                                                SyncLock DT
                                                    Dim lRow As DataRow = DT.NewRow()
                                                    lRow(0) = Obj.IdTramo
                                                    lRow(1) = Obj.Codigo
                                                    lRow(2) = Obj.Descripcion
                                                    DT.Rows.Add(lRow)
                                                End SyncLock
                                            End Sub)

                grdTramo.DataSource = DT

                Dim item As GridColumnSummaryItem = New GridColumnSummaryItem(SummaryItemType.Count, "Código", "Tramos={0}")

                If GridViewTramo.Columns("Código").Summary.Count = 0 Then
                    GridViewTramo.Columns("Código").Summary.Add(item)
                End If

            Else
                grdTramo.DataSource = Nothing
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

    Private Sub Cargar_Bodega_Ubicacion(ByVal pIdTramo As Integer)

        Try

            Dim DT As New DataTable
            DT = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_IdTramo_And_IdBodega_DT(pIdTramo,
                                                                                     pBeBodega.IdBodega)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    GroupControl15.Text = "Detalle de ubicaciones - Registros (" & DT.Rows.Count & ")"

                    grdUbicacion.DataSource = DT

                    GridViewUbi.BestFitColumns()

                Else
                    grdUbicacion.DataSource = Nothing
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

    Private Sub Cargar_Bodega()

        Try

            clsLnBodega.Obtener(pBeBodega)

            If pBeBodega.IdPais <> 0 Then
                cmbPais.EditValue = pBeBodega.IdPais
            End If

            cmbEmpresa.EditValue = pBeBodega.IdEmpresa
            cmbEmpresa.Enabled = False

            grpDatosGen.Text = "Datos de bodega - Correlativo#: " & pBeBodega.IdBodega
            txtCodigo.Text = pBeBodega.Codigo
            txtCodigoBarra.Text = pBeBodega.Codigo_barra
            txtNombre.Text = pBeBodega.Nombre
            txtNombreComercial.Text = pBeBodega.Nombre_comercial
            DireccionTextEdit.Text = pBeBodega.Direccion
            TelefonoTextEdit.Text = pBeBodega.Telefono
            EmailTextEdit.Text = pBeBodega.Email
            EncargadoTextEdit.Text = pBeBodega.Encargado

            txtAlto.Text = pBeBodega.Alto
            txtLargo.Text = pBeBodega.Largo
            txtAncho.Text = pBeBodega.Ancho

            '#CKFK 20210727 Corregí el texto de las ubicaciones faltantes
            Dim TextoMensaje As String = ""

            '#CM20171026_1150AM: LostFocus para campos de ubicaciones por defecto
            If pBeBodega.Ubic_despacho <> "" Then
                txtIdUbicacionDespacho.Text = pBeBodega.Ubic_despacho
                txtIdUbicacionDespacho_LostFocus(txtIdUbicacionDespacho, Nothing)
                '#CKFK 20210728 Agregué esto para cuando no es válido el ID de la ubicacion
                If txtIdUbicacionDespacho.Text = "" Then
                    If Trim(TextoMensaje) <> "" Then
                        TextoMensaje += ", Ubic Despacho inválida"
                    Else
                        TextoMensaje += "Ubic Despacho inválida"
                    End If
                End If
            Else
                If Trim(TextoMensaje) <> "" Then
                    TextoMensaje += ", Despacho"
                Else
                    TextoMensaje += "Despacho"
                End If
            End If

            If pBeBodega.Ubic_merma <> "" Then
                txtIdUbicacionMerma.Text = pBeBodega.Ubic_merma
                txtIdUbicacionMerma_LostFocus(txtIdUbicacionMerma, Nothing)
                '#CKFK 20210728 Agregué esto para cuando no es válido el ID de la ubicacion
                If txtIdUbicacionMerma.Text = "" Then
                    If Trim(TextoMensaje) <> "" Then
                        TextoMensaje += ", Ubic Merma inválida"
                    Else
                        TextoMensaje += "Ubic Merma inválida"
                    End If
                End If
            Else
                TextoMensaje += "Merma, "
            End If

            If pBeBodega.Ubic_picking <> "" Then
                txtIdUbicacionPicking.Text = pBeBodega.Ubic_picking
                txtIdUbicacionPicking_LostFocus(txtIdUbicacionPicking, Nothing)
                '#CKFK 20210728 Agregué esto para cuando no es válido el ID de la ubicacion
                If txtIdUbicacionPicking.Text = "" Then
                    If Trim(TextoMensaje) <> "" Then
                        TextoMensaje += ", Ubic Picking inválida"
                    Else
                        TextoMensaje += "Ubic Picking inválida"
                    End If
                End If
            Else
                If Trim(TextoMensaje) <> "" Then
                    TextoMensaje += ", Picking"
                Else
                    TextoMensaje += "Picking"
                End If
            End If

            If pBeBodega.Ubic_recepcion <> "" Then
                txtIdUbicacionRecepcion.Text = pBeBodega.Ubic_recepcion
                txtIdUbicacionRecepcion_LostFocus(txtIdUbicacionRecepcion, Nothing)
                '#CKFK 20210728 Agregué esto para cuando no es válido el ID de la ubicacion
                If txtIdUbicacionRecepcion.Text = "" Then
                    If Trim(TextoMensaje) <> "" Then
                        TextoMensaje += ", Ubic Recepción inválida"
                    Else
                        TextoMensaje += "Ubic Recepción inválida"
                    End If
                End If
            Else
                If Trim(TextoMensaje) <> "" Then
                    TextoMensaje += ", Recepción"
                Else
                    TextoMensaje += "Recepción"
                End If
            End If

            'txtNombreUbicacionRecepcion.Text = pObjBU.Descripcion

            If pBeBodega.IdMotivoUbicacionDañadoPicking > 0 Then
                txtidmotivoubicaciondañadopicking.Text = pBeBodega.IdMotivoUbicacionDañadoPicking
                txtidmotivoubicaciondañadopicking_LostFocus(txtidmotivoubicaciondañadopicking, Nothing)
                '#CKFK 20210728 Agregué esto para cuando no es válido el ID de la ubicacion
                If txtidmotivoubicaciondañadopicking.Text = "" Then
                    If Trim(TextoMensaje) <> "" Then
                        TextoMensaje += ", Ubic Dañado Picking inválida"
                    Else
                        TextoMensaje += "Ubic Dañado Picking inválida"
                    End If
                End If
            Else
                If Trim(TextoMensaje) <> "" Then
                    TextoMensaje += ", Dañado picking"
                Else
                    TextoMensaje += "Dañado picking"
                End If
            End If

            If pBeBodega.Id_Motivo_Ubic_Reabasto > 0 Then
                txtIdMotivoUbicReabasto.Text = pBeBodega.Id_Motivo_Ubic_Reabasto
                txtIdMotivoUbicReabasto_LostFocus(txtIdMotivoUbicReabasto, Nothing)
                '#CKFK 20210728 Agregué esto para cuando no es válido el ID de la ubicacion
                If txtIdMotivoUbicReabasto.Text = "" Then
                    If Trim(TextoMensaje) <> "" Then
                        TextoMensaje += ", Ubic Reabasto inválida"
                    Else
                        TextoMensaje += "Ubic Reabasto inválida"
                    End If
                End If
            Else
                If Trim(TextoMensaje) <> "" Then
                    TextoMensaje += ", Reabasto"
                Else
                    TextoMensaje += "Reabasto"
                End If
            End If

            If pBeBodega.ubic_producto_ne > 0 Then
                txtIdUbicacionPrdNE.Text = pBeBodega.ubic_producto_ne
                txtIdUbicacionPrdNE_LostFocus(txtIdUbicacionPrdNE, Nothing)
                '#CKFK 20210728 Agregué esto para cuando no es válido el ID de la ubicacion
                If txtIdUbicacionPrdNE.Text = "" Then
                    If Trim(TextoMensaje) <> "" Then
                        TextoMensaje += ", Ubic Producto NE inválida"
                    Else
                        TextoMensaje += "Ubic Producto NE inválida"
                    End If
                End If
            Else
                If Trim(TextoMensaje) <> "" Then
                    TextoMensaje += ", Ubicación Producto NE"
                Else
                    TextoMensaje += "Ubicación Producto NE"
                End If
            End If

            If TextoMensaje.Trim <> "" Then
                lblMensajeUbicacionesDef.Text = "Faltan las siguientes ubicaciones: " & TextoMensaje
            Else
                lblMensajeUbicacionesDef.Text = ""
            End If

            cmbEstadoNe.EditValue = Integer.Parse(pBeBodega.IdProductoEstadoNE)

            txtCoordenadaX.Text = pBeBodega.Coordenada_x
            txtCoordenadaY.Text = pBeBodega.Coordenada_y

            'Bitácora
            User_agrTextEdit.Text = pBeBodega.User_agr
            Fec_agrTextEdit.Text = pBeBodega.Fec_agr
            User_modTextEdit.Text = pBeBodega.User_mod
            Fec_modTextEdit.Text = pBeBodega.Fec_mod

            chkActivo.Checked = pBeBodega.Activo
            chkCambioUbiAuto.Checked = pBeBodega.cambio_ubicacion_auto
            chkNotificacionVoz.Checked = pBeBodega.Notificacion_Voz
            chkControlTarifaServ.Checked = pBeBodega.Control_Tarifa_Servicios

            txtIdTipoTR.Text = pBeBodega.IdTipoTransaccion
            txtZoom.Value = pBeBodega.Zoom

            '#EJC20190308: Desplegar el código de bodega ERP 
            txtCodigoBodegaERP.Text = pBeBodega.codigo_bodega_erp

            '#EJC20210316: Para cealsa, determinar si la bodega es o no fiscal.
            chkEsBodegaFiscal.Checked = pBeBodega.Es_Bodega_Fiscal

            '#EJC20210316: Para cealsa, determinar si en la lista de documentos de ingreso se puede crear un ingreso de consolidados (CEALSA)
            chkIngresoConsolidado.Checked = pBeBodega.habilitar_ingreso_consolidado

            'GT 03052021 Se obtiene el parametro de, si la bodega bloquea o no la LP en la HH al momento de recepcionar mercancia.
            chkBloquearLpHH.Checked = pBeBodega.bloquear_lp_hh

            '#EJC20210526: Para cealsa, determinar si se captura la estiba de ingreso.
            chkCapturaEstibaIngreso.Checked = pBeBodega.captura_estiba_ingreso

            '#EJC20210526: Para cealsa, determinar si se captura si es pallet no estandar.
            chkCapturaPalletNoEstandar.Checked = pBeBodega.captura_pallet_no_estandar

            '#EJC20210728:
            chkPermitir_Verificacion_Consolidada.Checked = pBeBodega.Permitir_Verificacion_Consolidada

            chkVerificacion_Consolidada.Checked = pBeBodega.Verificacion_Consolidada

            '#EJC20210929: control_banderas_cliente
            chkControlBanderasCliente.Checked = pBeBodega.control_banderas_cliente

            '#CKFK20220106: Priorizar la ubicación de la recepción sobre la ubicación del estado del producto
            chkPriorizar_UbicRec_Sobre_UbicEst.Checked = pBeBodega.priorizar_ubicrec_sobre_ubicest

            '#EJC20220129: Validar en proceso de ubicación, si la ubicación destino tiene producto o está "llena"
            chkValidarDisponibilidadEnUbicacionDestino.Checked = pBeBodega.validar_disponibilidad_ubicaicon_destino

            '#EJC20220224: CEALSA.
            chkMostrarAreaEnHH.Checked = pBeBodega.Mostrar_Area_En_HH

            '#EJC20220301: CEALSA.
            chkControlOperadorUbicacion.Checked = pBeBodega.control_operador_ubicacion

            '#EJC20220301: CEALSA.
            chkEscanearCodigoProductoEnPicking.Checked = pBeBodega.confirmar_codigo_en_picking

            '#EJC20220314: CEALSA, si true, entonces en el cambio de ubicación, al escanear únicamente licencia, se coloca automáticamente la ubicación de origen.
            chkinferir_origen_en_cambio_ubic.Checked = pBeBodega.inferir_origen_en_cambio_ubic

            '#EJC20220314: BYB, si true, entonces permite anular el documento para volverlo a importar
            chkPermitirEliminarDocumentosSalida.Checked = pBeBodega.Permitir_Eliminar_Documento_Salida

            '#CKFK20220318: BYB, si true, entonces permite eliminar el documento para volverlo a importar
            chkEliminarDocumentosSalida.Checked = pBeBodega.Eliminar_Documento_Salida

            '#CKFK20220721: Mercosa, si true, entonces permite ingresar decimales en las cantidades de las transacciones
            chkPermitirDecimales.Checked = pBeBodega.Permitir_Decimales

            '#CKFK20230209 Parámetros nuevos para el picking
            chkOrdenarPickingDescendente.Checked = pBeBodega.Ordenar_Picking_Descendente

            chkOrdenarNombreCompleto.Checked = pBeBodega.Ordenar_Por_Nombre_Completo

            '#CKFK20230209 Parámetros nuevos para el picking
            chkPermitirReemplazoPicking.Checked = pBeBodega.Permitir_Reemplazo_Picking
            chkPermitirReemplazoVerificacion.Checked = pBeBodega.Permitir_Reemplazo_Verificacion
            '#MA20260223 MEJORAS PARA LA CUMBRE
            chkreemplazoOpcional.Checked = pBeBodega.reemplazo_opcional
            chkPermitirNoEncontradoPicking.Checked = pBeBodega.Permitir_No_Encontrado_Picking

            '#EJC20220223
            If Val(pBeBodega.IdTipoTransaccionSalida > 0) Then
                txtIdTipoDocumentoSalida.Text = pBeBodega.IdTipoTransaccionSalida
                Set_IdTipoDocumentoSalida()
            Else
                txtIdTipoDocumentoSalida.Text = ""
            End If

            '#EJC20220330:
            chkOperadorPickingVerifica.Checked = pBeBodega.Operador_Picking_Realiza_Verificacion
            chkPermitirCambioUbicacionPicking.Checked = pBeBodega.Permitir_Cambio_Ubic_Producto_Picking

            txtIdConfiguracionPantallaPicking.Value = pBeBodega.tipo_pantalla_picking
            txtIdConfiguracionPantallaRecepcion.Value = pBeBodega.tipo_pantalla_recepcion
            txtIdConfiguracionPantallaVerificacion.Value = pBeBodega.tipo_pantalla_verificacion

            'GT29062022: valor IVA 
            txtValorIVA.Value = pBeBodega.valor_porcentaje_iva

            '#ejc20220701
            chkpermitir_buen_estado_en_reemplazo.Checked = pBeBodega.Permitir_Buen_Estado_En_Reemplazo

            'GT07072022: Tipo motriz 
            chkEsMotriz.Checked = pBeBodega.industria_motriz

            '#EJC20220707
            chkrestringir_lote_en_reemplazo.Checked = pBeBodega.Restringir_Lote_En_Reemplazo

            '#EJC20220707A
            chkrestringir_vencimiento_en_reemplazo.Checked = pBeBodega.Restringir_Vencimiento_En_Reemplazo

            '#CKFK20220717
            nudTopReabastecimientoManual.Value = pBeBodega.Top_Reabastecimiento_Manual

            '#GT02032023: dias de antiguedad permitidos en ticket para validar retroactivo
            txtIdDiasLimiteRetroactivo.Value = pBeBodega.Dias_Limite_Retroactivo

            '#EJC20220912_1748
            If pBeBodega.Dias_Maximo_Vencimiento_Reemplazo > txtDiasMaximoVencimientoReemplazo.Maximum Then
                txtDiasMaximoVencimientoReemplazo.Maximum = pBeBodega.Dias_Maximo_Vencimiento_Reemplazo + 1
            End If

            txtDiasMaximoVencimientoReemplazo.Value = pBeBodega.Dias_Maximo_Vencimiento_Reemplazo

            dtHorarioEjecucionHistorico.EditValue = pBeBodega.Horario_Ejecucion_Historico

            '#EJC20220912_1951:
            chkPermitirRepeticionesEnIngreso.Checked = pBeBodega.Permitir_Repeticiones_En_Ingreso

            '#EJC20221005: Validar_Existencias_Inv_Ini
            chkValidarExistenciasEnCargaInventarioInicial.Checked = pBeBodega.Validar_Existencias_Inv_Ini

            '#EJC202211211058: Ubicación sugerida HH
            chkcalcular_ubicacion_sugerida_ml.Checked = pBeBodega.Calcular_Ubicacion_Sugerida_ML

            '#EJC202302231731: Permitir_Reemplazo_Picking_Misma_Licencia
            chkPermitirReemplazoPickingMismaLIcencia.Checked = pBeBodega.Permitir_Reemplazo_Picking_Misma_Licencia

            '--#CKFK20230728 Parámetro para filtrar o no los pedidos en el despacho por usuario
            chkFiltrarPedidosUsuario.Checked = pBeBodega.Filtrar_Pedidos_Usuario

            '--#CKFK20230815 Parámetro para liberar el stock de despachos parciales
            chkLberarStockDepachosParciales.Checked = pBeBodega.Liberar_Stock_Despachos_Parciales

            '#EJC202310310846: Indica si la fecha de vencimiento debe ser siempre la misma para un lote (aplica en SAP y NAV)
            chkHomologarLoteConFechaVence.Checked = pBeBodega.Homologar_Lote_Vencimiento

            '#CKFK: Indica si se va a escanear la licencia en el Picking
            chkEscanearLicenciaPicking.Checked = pBeBodega.Escanear_Licencia_Picking

            '#CKFK20240403: Indicar los tipos de etiqueta y simbología para la licencia
            cmbEtiqueta.EditValue = pBeBodega.IdTipoEtiquetaLicencia
            cmbSymbology.EditValue = pBeBodega.IdSimbologiaLicencia

            cmbTamañoEtiquetaUbicacionDefecto.EditValue = pBeBodega.IdTamañoEtiquetaUbicacionDefecto

            '#CKFK: Indica si la bodega tiene interface con SAP
            chkInterface_SAP.Checked = pBeBodega.Interface_SAP

            '#CKFK20240627: Indica si vamos a tener restricción por áreas
            chkRestringirAreasSAP.Checked = pBeBodega.Restringir_Areas_SAP

            '#CKFK20240703: Indica si se va a llevar control de pallet mixtos
            chkControlPalletsMixtos.Checked = pBeBodega.Control_Pallet_Mixto

            '#EJC20220510_BYB: 
            chkDespacharProductoVencido.Checked = pBeBodega.despachar_producto_vencido

            chkdespachoautohh.Checked = pBeBodega.Despacho_Automatico_HH

            chkLimpiarCamposHH.Checked = pBeBodega.Limpiar_Campos

            chkPermitirCambioUbicacionRecepcion.Checked = pBeBodega.Permitir_Cambio_Ubic_Recepcion

            txtRutaCDN.Text = pBeBodega.Ruta_CDN

            chkControlTallaColor.Checked = pBeBodega.Control_Talla_Color
            chkControlGondola.Checked = pBeBodega.Control_Gondola

            nudRangoDiasDocumentos.Value = pBeBodega.Rango_Dias_Documentos

            chkAgrupar_sin_lic_veri_no_cons.Checked = pBeBodega.Agrupar_Sin_Lic_Veri_No_Cons
            chkAdvertirMpqUmbas.Checked = pBeBodega.Advertir_Mpq_Umbas
            cmbCentroCostoERP.EditValue = pBeBodega.Centro_Costo_Erp
            cmbCentroCostoDirERP.EditValue = pBeBodega.Centro_Costo_Dir_Erp
            cmbCentroCostoDepERP.EditValue = pBeBodega.Centro_Costo_Dep_Erp

            If pBeBodega.Estado_Defecto_Rack > 0 Then
                cmbEstadoDefectoRack.EditValue = pBeBodega.Estado_Defecto_Rack
            Else
                cmbEstadoDefectoRack.EditValue = 0
            End If

            chkCambioUbicacionRestrictivo.Checked = pBeBodega.cambio_ubicacion_restrictivo
            chkPermitirCambioUbicIndiceMenor.Checked = pBeBodega.permitir_cambio_ubic_indice_menor
            chkRequerirMismoProductoPosiciones.Checked = pBeBodega.requerir_mismo_producto_posiciones

            chkBodegaClienteAjusteByB.Checked = pBeBodega.Bodega_Cliente_Ajuste_ByB
            chkControlGuia.Checked = pBeBodega.Control_Guia

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_IdTipoDocumentoSalida()

        Try

            If Val(txtIdTipoDocumentoSalida.Text) > 0 Then

                Dim BeTipoDocumentoSalida As New clsBeTrans_pe_tipo
                BeTipoDocumentoSalida = clsLnTrans_pe_tipo.GetSingle(txtIdTipoDocumentoSalida.Text)

                If Not BeTipoDocumentoSalida Is Nothing Then

                    txtNombreDocumentoSalida.Text = BeTipoDocumentoSalida.Descripcion

                Else

                    XtraMessageBox.Show("El tipo de documento no es válido", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdTipoDocumentoSalida.Text = ""
                    txtNombreDocumentoSalida.Text = ""

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

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            '#GT18112025: guardar smpt
            'se hace aqui porque no guarda en la bd, sino en un archivo de configuración.
            _smtpConfig.Servidor = txtServidor.Text.Trim()
            _smtpConfig.Puerto = Integer.Parse(txtPuerto.Text.Trim())
            _smtpConfig.Usuario = txtUsuario.Text.Trim()
            _smtpConfig.Password = CryptoHelper.Encriptar(txtPassword.Text)  ' guardamos encriptada
            _smtpConfig.UsarSsl = chkSsl.Checked
            _smtpConfig.RemitentePorDefecto = txtUsuario.Text.Trim() 'el remitente es la misma cuenta del usuario
            ConfigManager.Guardar(_smtpConfig)

            If cmbPais.EditValue <> 0 Then
                pBeBodega.IdPais = cmbPais.EditValue
            End If

            pBeBodega.IdBodega = clsLnBodega.MaxID()
            pBeBodega.IdEmpresa = cmbEmpresa.EditValue

            If txtCodigo.Text.Trim() = "" Then
                pBeBodega.Codigo = Correlativo()
            Else
                pBeBodega.Codigo = txtCodigo.Text.Trim()
            End If

            pBeBodega.Codigo_barra = txtCodigoBarra.Text.Trim()
            pBeBodega.Nombre = txtNombre.Text.Trim()
            pBeBodega.Nombre_comercial = txtNombreComercial.Text.Trim()
            pBeBodega.Direccion = DireccionTextEdit.Text.Trim()
            pBeBodega.Telefono = TelefonoTextEdit.Text.Trim()
            pBeBodega.Email = EmailTextEdit.Text.Trim()
            pBeBodega.Encargado = EncargadoTextEdit.Text.Trim()

            If txtIdUbicacionRecepcion.Text.Trim <> "" Then
                pBeBodega.Ubic_recepcion = txtIdUbicacionRecepcion.Text.Trim()
            Else
                pBeBodega.Ubic_recepcion = 0
            End If

            If txtIdUbicacionDespacho.Text.Trim <> "" Then
                pBeBodega.Ubic_despacho = txtIdUbicacionDespacho.Text.Trim()
            Else
                pBeBodega.Ubic_despacho = 0
            End If

            If txtIdUbicacionPicking.Text.Trim <> "" Then
                pBeBodega.Ubic_picking = txtIdUbicacionPicking.Text.Trim()
            Else
                pBeBodega.Ubic_picking = 0
            End If

            If txtIdUbicacionMerma.Text.Trim <> "" Then
                pBeBodega.Ubic_merma = txtIdUbicacionMerma.Text.Trim()
            Else
                pBeBodega.Ubic_merma = 0
            End If

            If txtidmotivoubicaciondañadopicking.Text.Trim <> "" Then
                pBeBodega.IdMotivoUbicacionDañadoPicking = txtIdMotivoUbicReabasto.Text.Trim
            Else
                pBeBodega.IdMotivoUbicacionDañadoPicking = 0
            End If

            If txtIdMotivoUbicReabasto.Text.Trim <> "" Then
                pBeBodega.Id_Motivo_Ubic_Reabasto = txtIdMotivoUbicReabasto.Text.Trim
            Else
                pBeBodega.Id_Motivo_Ubic_Reabasto = 0
            End If

            If txtIdUbicacionPrdNE.Text.Trim <> "" Then
                pBeBodega.ubic_producto_ne = txtIdUbicacionPrdNE.Text.Trim
            Else
                pBeBodega.ubic_producto_ne = 0
            End If


            '#GT02032023: indica cuantos dias de antigúedad se permiten validar para historico
            If txtIdDiasLimiteRetroactivo.Text.Trim <> "" Then
                pBeBodega.Dias_Limite_Retroactivo = txtIdDiasLimiteRetroactivo.Text.Trim
            Else
                pBeBodega.Dias_Limite_Retroactivo = 0
            End If

            pBeBodega.IdProductoEstadoNE = cmbEstadoNe.EditValue

            pBeBodega.Largo = txtLargo.Text
            pBeBodega.Alto = txtAlto.Text
            pBeBodega.Ancho = txtAncho.Text
            pBeBodega.Coordenada_x = txtCoordenadaX.Text
            pBeBodega.Coordenada_y = txtCoordenadaY.Text
            pBeBodega.User_agr = AP.UsuarioAp.IdUsuario
            pBeBodega.Fec_agr = Now
            pBeBodega.User_mod = AP.UsuarioAp.IdUsuario
            pBeBodega.Fec_mod = Now
            pBeBodega.Activo = True
            pBeBodega.IdTipoTransaccion = txtIdTipoTR.Text
            pBeBodega.Zoom = txtZoom.Value
            pBeBodega.cambio_ubicacion_auto = chkCambioUbiAuto.Checked '#CKFK 20180413 07:10 PM Se agregó el campo cambio_ubicacion_auto para realizar o no el cambio de ubicación automático
            pBeBodega.Notificacion_Voz = chkNotificacionVoz.Checked '#CKFK 20210210 06:30 PM Se agregó el campo notificacio=ón_voz para que la HH notifique o no por voz las transacciones
            pBeBodega.Control_Tarifa_Servicios = chkControlTarifaServ.Checked '#CKFK 20210219 06:44 PM Se agregó el control tarifa servicios para poder saber si en la bodega se va a llevar control de los servicios

            '#CKFK 20190131 Le cambié el nombre a esta función porque ya existía otra igual
            If clsLnBodega.Exists_By_IdEmpresa(cmbEmpresa.EditValue) = False Then
                If XtraMessageBox.Show("¿Desea agregar Usuario Admin?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    GuardarUsuario()
                    pBodegaEmp = True
                End If
            End If

            '#EJC20190308: Asignar el código de bodega ERP 
            pBeBodega.codigo_bodega_erp = txtCodigoBodegaERP.Text.Trim

            '#EJC20210316: Para cealsa, determinar si la bodega es o no fiscal.
            pBeBodega.Es_Bodega_Fiscal = chkEsBodegaFiscal.Checked
            '#EJC20210317_1346PM
            pBeBodega.habilitar_ingreso_consolidado = chkIngresoConsolidado.Checked
            'GT01052021 Bloquea LP en HH
            pBeBodega.bloquear_lp_hh = chkBloquearLpHH.Checked
            '#CKFK 20210526
            pBeBodega.captura_estiba_ingreso = chkCapturaEstibaIngreso.Checked
            '#CKFK 20210526
            pBeBodega.captura_pallet_no_estandar = chkCapturaPalletNoEstandar.Checked
            '#EJC20210728
            pBeBodega.valor_porcentaje_iva = Val(txtValorIVA.Value)

            pBeBodega.Permitir_Verificacion_Consolidada = chkPermitir_Verificacion_Consolidada.Checked
            pBeBodega.Verificacion_Consolidada = chkVerificacion_Consolidada.Checked
            pBeBodega.IdTamañoEtiquetaUbicacionDefecto = cmbTamañoEtiquetaUbicacionDefecto.EditValue
            pBeBodega.control_banderas_cliente = chkControlBanderasCliente.Checked
            pBeBodega.priorizar_ubicrec_sobre_ubicest = chkPriorizar_UbicRec_Sobre_UbicEst.Checked
            pBeBodega.validar_disponibilidad_ubicaicon_destino = chkValidarDisponibilidadEnUbicacionDestino.Checked
            pBeBodega.Permitir_Eliminar_Documento_Salida = chkPermitirEliminarDocumentosSalida.Checked
            pBeBodega.Mostrar_Area_En_HH = chkMostrarAreaEnHH.Checked
            pBeBodega.confirmar_codigo_en_picking = chkEscanearCodigoProductoEnPicking.Checked
            pBeBodega.control_operador_ubicacion = chkControlOperadorUbicacion.Checked
            pBeBodega.inferir_origen_en_cambio_ubic = chkinferir_origen_en_cambio_ubic.Checked
            pBeBodega.Eliminar_Documento_Salida = chkEliminarDocumentosSalida.Checked
            pBeBodega.Permitir_Decimales = chkPermitirDecimales.Checked
            pBeBodega.Operador_Picking_Realiza_Verificacion = chkOperadorPickingVerifica.Checked
            pBeBodega.Permitir_Cambio_Ubic_Producto_Picking = chkPermitirCambioUbicacionPicking.Checked
            pBeBodega.despachar_producto_vencido = chkDespacharProductoVencido.Checked
            pBeBodega.tipo_pantalla_picking = txtIdConfiguracionPantallaPicking.Value
            pBeBodega.tipo_pantalla_recepcion = txtIdConfiguracionPantallaRecepcion.Value
            pBeBodega.tipo_pantalla_verificacion = txtIdConfiguracionPantallaVerificacion.Value
            pBeBodega.Permitir_Buen_Estado_En_Reemplazo = chkpermitir_buen_estado_en_reemplazo.Checked
            pBeBodega.industria_motriz = chkEsMotriz.Checked
            pBeBodega.Restringir_Lote_En_Reemplazo = chkrestringir_lote_en_reemplazo.Checked
            pBeBodega.Restringir_Vencimiento_En_Reemplazo = chkrestringir_vencimiento_en_reemplazo.Checked
            pBeBodega.Top_Reabastecimiento_Manual = nudTopReabastecimientoManual.Value
            pBeBodega.Dias_Maximo_Vencimiento_Reemplazo = txtDiasMaximoVencimientoReemplazo.Value
            pBeBodega.Permitir_Repeticiones_En_Ingreso = chkPermitirRepeticionesEnIngreso.Checked
            pBeBodega.Validar_Existencias_Inv_Ini = chkValidarExistenciasEnCargaInventarioInicial.Checked
            pBeBodega.Calcular_Ubicacion_Sugerida_ML = chkcalcular_ubicacion_sugerida_ml.Checked
            pBeBodega.Ordenar_Picking_Descendente = chkOrdenarPickingDescendente.Checked
            pBeBodega.Ordenar_Por_Nombre_Completo = chkOrdenarNombreCompleto.Checked
            pBeBodega.Permitir_Reemplazo_Picking = chkPermitirReemplazoPicking.Checked
            pBeBodega.Permitir_Reemplazo_Verificacion = chkPermitirReemplazoVerificacion.Checked
            pBeBodega.reemplazo_opcional = chkreemplazoOpcional.Checked
            pBeBodega.Permitir_No_Encontrado_Picking = chkPermitirNoEncontradoPicking.Checked
            pBeBodega.Permitir_Reemplazo_Picking_Misma_Licencia = chkPermitirReemplazoPickingMismaLIcencia.Checked
            pBeBodega.Filtrar_Pedidos_Usuario = chkFiltrarPedidosUsuario.Checked
            pBeBodega.Liberar_Stock_Despachos_Parciales = chkLberarStockDepachosParciales.Checked
            pBeBodega.Homologar_Lote_Vencimiento = chkHomologarLoteConFechaVence.Checked
            pBeBodega.Escanear_Licencia_Picking = chkEscanearLicenciaPicking.Checked
            pBeBodega.IdTipoEtiquetaLicencia = cmbEtiqueta.EditValue
            pBeBodega.IdSimbologiaLicencia = cmbSymbology.EditValue
            pBeBodega.Interface_SAP = chkInterface_SAP.Checked
            pBeBodega.Restringir_Areas_SAP = chkRestringirAreasSAP.Checked
            pBeBodega.Control_Pallet_Mixto = chkControlPalletsMixtos.Checked
            pBeBodega.Despacho_Automatico_HH = chkdespachoautohh.Checked
            pBeBodega.Limpiar_Campos = chkLimpiarCamposHH.Checked
            pBeBodega.Permitir_Cambio_Ubic_Recepcion = chkPermitirCambioUbicacionRecepcion.Checked
            pBeBodega.Ruta_CDN = txtRutaCDN.Text
            pBeBodega.Rango_Dias_Documentos = nudRangoDiasDocumentos.Value

            pBeBodega.Agrupar_Sin_Lic_Veri_No_Cons = chkAgrupar_sin_lic_veri_no_cons.Checked
            pBeBodega.Advertir_Mpq_Umbas = chkAdvertirMpqUmbas.Checked

            pBeBodega.Centro_Costo_Erp = If(cmbCentroCostoERP.EditValue = Nothing, "", cmbCentroCostoERP.EditValue)
            pBeBodega.Centro_Costo_Dir_Erp = If(cmbCentroCostoDirERP.EditValue = Nothing, "", cmbCentroCostoDirERP.EditValue)
            pBeBodega.Centro_Costo_Dep_Erp = If(cmbCentroCostoDepERP.EditValue = Nothing, "", cmbCentroCostoDepERP.EditValue)

            pBeBodega.Control_Talla_Color = chkControlTallaColor.Checked
            pBeBodega.Control_Gondola = chkControlGondola.Checked
            If cmbEstadoDefectoRack IsNot Nothing AndAlso cmbEstadoDefectoRack.EditValue IsNot Nothing Then
                If IsNumeric(cmbEstadoDefectoRack.EditValue) Then
                    pBeBodega.Estado_Defecto_Rack = Integer.Parse(cmbEstadoDefectoRack.EditValue.ToString())
                Else
                    pBeBodega.Estado_Defecto_Rack = 0
                End If
            Else
                pBeBodega.Estado_Defecto_Rack = 0
            End If

            '#Nuevos parámetros cambio ubicación
            pBeBodega.cambio_ubicacion_restrictivo = chkCambioUbicacionRestrictivo.Checked
            pBeBodega.permitir_cambio_ubic_indice_menor = chkPermitirCambioUbicIndiceMenor.Checked
            pBeBodega.requerir_mismo_producto_posiciones = chkRequerirMismoProductoPosiciones.Checked
            pBeBodega.Bodega_Cliente_Ajuste_ByB = chkBodegaClienteAjusteByB.Checked
            pBeBodega.Control_Guia = chkControlGuia.Checked

            Guardar = clsLnBodega.Insertar(pBeBodega) > 0

            pObjBAB.IdBodega = pBeBodega.IdBodega

            If pBodegaEmp Then
                GuardarPermisos()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function GuardarUsuario() As Boolean

        GuardarUsuario = False

        Try
            Usuario.IdUsuario = clsLnUsuario.MaxID()
            Usuario.IdEmpresa = cmbEmpresa.EditValue
            Usuario.Nombres = "Admin"
            Usuario.Apellidos = "Admin"
            Usuario.Cedula = ""
            Usuario.Direccion = DireccionTextEdit.Text
            Usuario.Telefono = TelefonoTextEdit.Text
            Usuario.Email = EmailTextEdit.Text
            Usuario.Codigo = clsPublic.Encriptar("Admin")
            Usuario.Clave = clsPublic.Encriptar("Admin")
            Usuario.Activo = chkActivo.Checked

            Usuario.User_agr = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
            Usuario.Fec_agr = Now
            Usuario.User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
            Usuario.Fec_mod = Now

            GuardarUsuario = clsLnUsuario.Insertar(Usuario) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function GuardarPermisos() As Boolean

        GuardarPermisos = False

        Try

            objUsuarioBodega = New clsBeUsuario_bodega() With {.IdUsuarioBodega = clsLnUsuario_bodega.MaxID(),
                .IdBodega = pBeBodega.IdBodega,
                .IdUsuario = Usuario.IdUsuario,
                .IdUsuarioSuperior = Usuario.IdUsuario,
                .IdRol = 1,
                .Activo = True,
                .User_agr = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos),
                .Fec_agr = Now,
                .User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos),
                .Fec_mod = Now}

            GuardarPermisos = clsLnUsuario_bodega.Insertar(objUsuarioBodega) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then


                '#GT18112025: guardar smpt
                'se hace aqui porque no guarda en la bd, sino en un archivo de configuración.
                _smtpConfig.Servidor = txtServidor.Text.Trim()
                _smtpConfig.Puerto = Integer.Parse(txtPuerto.Text.Trim())
                _smtpConfig.Usuario = txtUsuario.Text.Trim()
                _smtpConfig.Password = CryptoHelper.Encriptar(txtPassword.Text)  ' guardamos encriptada
                _smtpConfig.UsarSsl = chkSsl.Checked
                _smtpConfig.RemitentePorDefecto = txtUsuario.Text.Trim() 'el remitente es la misma cuenta del usuario
                ConfigManager.Guardar(_smtpConfig)


                If cmbPais.EditValue <> 0 Then
                    pBeBodega.IdPais = cmbPais.EditValue
                End If

                pBeBodega.Codigo = txtCodigo.Text.Trim()
                pBeBodega.Codigo_barra = txtCodigoBarra.Text.Trim()
                pBeBodega.Nombre = txtNombre.Text.Trim()
                pBeBodega.Nombre_comercial = txtNombreComercial.Text.Trim()
                pBeBodega.Direccion = DireccionTextEdit.Text.Trim()
                pBeBodega.Telefono = TelefonoTextEdit.Text.Trim()
                pBeBodega.Email = EmailTextEdit.Text.Trim()
                pBeBodega.Encargado = EncargadoTextEdit.Text.Trim()
                pBeBodega.Ubic_recepcion = txtIdUbicacionRecepcion.Text.Trim()
                pBeBodega.Ubic_despacho = txtIdUbicacionDespacho.Text.Trim()
                pBeBodega.Ubic_picking = txtIdUbicacionPicking.Text.Trim()
                pBeBodega.Ubic_merma = txtIdUbicacionMerma.Text.Trim()

                If txtidmotivoubicaciondañadopicking.Text.Trim <> "" Then
                    pBeBodega.IdMotivoUbicacionDañadoPicking = txtidmotivoubicaciondañadopicking.Text.Trim
                Else
                    pBeBodega.IdMotivoUbicacionDañadoPicking = 0
                End If

                If txtIdMotivoUbicReabasto.Text.Trim <> "" Then
                    pBeBodega.Id_Motivo_Ubic_Reabasto = txtIdMotivoUbicReabasto.Text.Trim
                Else
                    pBeBodega.Id_Motivo_Ubic_Reabasto = 0
                End If

                If txtIdUbicacionPrdNE.Text.Trim <> "" Then
                    pBeBodega.ubic_producto_ne = txtIdUbicacionPrdNE.Text.Trim
                Else
                    pBeBodega.ubic_producto_ne = 0
                End If

                pBeBodega.IdProductoEstadoNE = cmbEstadoNe.EditValue

                pBeBodega.Largo = txtLargo.Text
                pBeBodega.Alto = txtAlto.Text
                pBeBodega.Ancho = txtAncho.Text
                pBeBodega.Coordenada_x = txtCoordenadaX.Text
                pBeBodega.Coordenada_y = txtCoordenadaY.Text
                pBeBodega.User_mod = AP.UsuarioAp.IdUsuario
                pBeBodega.Fec_mod = Now
                pBeBodega.Activo = chkActivo.Checked
                pBeBodega.cambio_ubicacion_auto = chkCambioUbiAuto.Checked
                pBeBodega.Notificacion_Voz = chkNotificacionVoz.Checked
                pBeBodega.Control_Tarifa_Servicios = chkControlTarifaServ.Checked
                pBeBodega.IdTipoTransaccion = txtIdTipoTR.Text
                pBeBodega.Zoom = txtZoom.Value
                pBeBodega.codigo_bodega_erp = txtCodigoBodegaERP.Text
                pBeBodega.Es_Bodega_Fiscal = chkEsBodegaFiscal.Checked
                pBeBodega.habilitar_ingreso_consolidado = chkIngresoConsolidado.Checked
                pBeBodega.bloquear_lp_hh = chkBloquearLpHH.Checked
                pBeBodega.captura_estiba_ingreso = chkCapturaEstibaIngreso.Checked
                pBeBodega.captura_pallet_no_estandar = chkCapturaPalletNoEstandar.Checked
                pBeBodega.valor_porcentaje_iva = Val(txtValorIVA.Value)
                pBeBodega.Permitir_Verificacion_Consolidada = chkPermitir_Verificacion_Consolidada.Checked
                pBeBodega.Verificacion_Consolidada = chkVerificacion_Consolidada.Checked
                pBeBodega.priorizar_ubicrec_sobre_ubicest = chkPriorizar_UbicRec_Sobre_UbicEst.Checked
                pBeBodega.control_banderas_cliente = chkControlBanderasCliente.Checked
                pBeBodega.validar_disponibilidad_ubicaicon_destino = chkValidarDisponibilidadEnUbicacionDestino.Checked
                pBeBodega.IdTipoTransaccionSalida = Val(txtIdTipoDocumentoSalida.Text)
                pBeBodega.Permitir_Eliminar_Documento_Salida = chkPermitirEliminarDocumentosSalida.Checked
                pBeBodega.Mostrar_Area_En_HH = chkMostrarAreaEnHH.Checked
                pBeBodega.confirmar_codigo_en_picking = chkEscanearCodigoProductoEnPicking.Checked
                pBeBodega.control_operador_ubicacion = chkControlOperadorUbicacion.Checked
                pBeBodega.inferir_origen_en_cambio_ubic = chkinferir_origen_en_cambio_ubic.Checked
                pBeBodega.Eliminar_Documento_Salida = chkEliminarDocumentosSalida.Checked
                pBeBodega.Permitir_Decimales = chkPermitirDecimales.Checked
                pBeBodega.Operador_Picking_Realiza_Verificacion = chkOperadorPickingVerifica.Checked
                pBeBodega.Permitir_Cambio_Ubic_Producto_Picking = chkPermitirCambioUbicacionPicking.Checked
                pBeBodega.despachar_producto_vencido = chkDespacharProductoVencido.Checked
                pBeBodega.tipo_pantalla_picking = txtIdConfiguracionPantallaPicking.Value
                pBeBodega.tipo_pantalla_recepcion = txtIdConfiguracionPantallaRecepcion.Value
                pBeBodega.tipo_pantalla_verificacion = txtIdConfiguracionPantallaVerificacion.Value
                pBeBodega.Dias_Limite_Retroactivo = txtIdDiasLimiteRetroactivo.Value
                pBeBodega.Permitir_Buen_Estado_En_Reemplazo = chkpermitir_buen_estado_en_reemplazo.Checked
                pBeBodega.industria_motriz = chkEsMotriz.Checked
                pBeBodega.Restringir_Lote_En_Reemplazo = chkrestringir_lote_en_reemplazo.Checked
                pBeBodega.Restringir_Vencimiento_En_Reemplazo = chkrestringir_vencimiento_en_reemplazo.Checked
                pBeBodega.Top_Reabastecimiento_Manual = nudTopReabastecimientoManual.Value
                pBeBodega.Dias_Maximo_Vencimiento_Reemplazo = txtDiasMaximoVencimientoReemplazo.Value
                pBeBodega.Permitir_Repeticiones_En_Ingreso = chkPermitirRepeticionesEnIngreso.Checked
                pBeBodega.Validar_Existencias_Inv_Ini = chkValidarExistenciasEnCargaInventarioInicial.Checked
                pBeBodega.Calcular_Ubicacion_Sugerida_ML = chkcalcular_ubicacion_sugerida_ml.Checked
                pBeBodega.Ordenar_Picking_Descendente = chkOrdenarPickingDescendente.Checked
                pBeBodega.Ordenar_Por_Nombre_Completo = chkOrdenarNombreCompleto.Checked
                pBeBodega.Permitir_Reemplazo_Picking = chkPermitirReemplazoPicking.Checked
                pBeBodega.Permitir_Reemplazo_Verificacion = chkPermitirReemplazoVerificacion.Checked
                pBeBodega.Permitir_No_Encontrado_Picking = chkPermitirNoEncontradoPicking.Checked
                pBeBodega.Permitir_Reemplazo_Picking_Misma_Licencia = chkPermitirReemplazoPickingMismaLIcencia.Checked
                pBeBodega.Horario_Ejecucion_Historico = dtHorarioEjecucionHistorico.DateTimeOffset.TimeOfDay
                pBeBodega.Filtrar_Pedidos_Usuario = chkFiltrarPedidosUsuario.Checked
                pBeBodega.Liberar_Stock_Despachos_Parciales = chkLberarStockDepachosParciales.Checked
                pBeBodega.Homologar_Lote_Vencimiento = chkHomologarLoteConFechaVence.Checked
                pBeBodega.Escanear_Licencia_Picking = chkEscanearLicenciaPicking.Checked
                pBeBodega.IdTipoEtiquetaLicencia = cmbEtiqueta.EditValue
                pBeBodega.IdSimbologiaLicencia = cmbSymbology.EditValue
                pBeBodega.Interface_SAP = chkInterface_SAP.Checked
                pBeBodega.Restringir_Areas_SAP = chkRestringirAreasSAP.Checked
                pBeBodega.Control_Pallet_Mixto = chkControlPalletsMixtos.Checked
                pBeBodega.IdTamañoEtiquetaUbicacionDefecto = cmbTamañoEtiquetaUbicacionDefecto.EditValue
                pBeBodega.Despacho_Automatico_HH = chkdespachoautohh.Checked
                pBeBodega.Limpiar_Campos = chkLimpiarCamposHH.Checked
                pBeBodega.Permitir_Cambio_Ubic_Recepcion = chkPermitirCambioUbicacionRecepcion.Checked
                pBeBodega.Ruta_CDN = txtRutaCDN.Text
                pBeBodega.Rango_Dias_Documentos = nudRangoDiasDocumentos.Value
                pBeBodega.Control_Talla_Color = chkControlTallaColor.Checked
                pBeBodega.Agrupar_Sin_Lic_Veri_No_Cons = chkAgrupar_sin_lic_veri_no_cons.Checked
                pBeBodega.Advertir_Mpq_Umbas = chkAdvertirMpqUmbas.Checked
                pBeBodega.Centro_Costo_Erp = cmbCentroCostoERP.EditValue
                pBeBodega.Centro_Costo_Dir_Erp = cmbCentroCostoDirERP.EditValue
                pBeBodega.Centro_Costo_Dep_Erp = cmbCentroCostoDepERP.EditValue
                pBeBodega.Control_Gondola = chkControlGondola.Checked
                pBeBodega.Reemplazo_Opcional = chkreemplazoOpcional.Checked
                If cmbEstadoDefectoRack IsNot Nothing AndAlso cmbEstadoDefectoRack.EditValue IsNot Nothing Then
                    If IsNumeric(cmbEstadoDefectoRack.EditValue) Then
                        pBeBodega.Estado_Defecto_Rack = Integer.Parse(cmbEstadoDefectoRack.EditValue.ToString())
                    Else
                        pBeBodega.Estado_Defecto_Rack = 0
                    End If
                Else
                    pBeBodega.Estado_Defecto_Rack = 0
                End If
                pBeBodega.Bodega_Cliente_Ajuste_ByB = chkBodegaClienteAjusteByB.Checked
                pBeBodega.Control_Guia = chkControlGuia.Checked

                pBeBodega.impresion_verificacion = chkImprimir_Verificacion.Checked
                '#Nuevos parámetros cambio ubicación
                pBeBodega.cambio_ubicacion_restrictivo = chkCambioUbicacionRestrictivo.Checked
                pBeBodega.permitir_cambio_ubic_indice_menor = chkPermitirCambioUbicIndiceMenor.Checked
                pBeBodega.requerir_mismo_producto_posiciones = chkRequerirMismoProductoPosiciones.Checked

                Actualizar = clsLnBodega.Actualizar(pBeBodega) > 0

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try
            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Datos_CorrectosArea()

        Datos_CorrectosArea = False

        Try

            If String.IsNullOrEmpty(txtCodigoAreaBodega.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoAreaBodega.Focus()
            ElseIf String.IsNullOrEmpty(txtDescripcionAreaBodega.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Descripción.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDescripcionAreaBodega.Focus()
            Else
                Datos_CorrectosArea = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Datos_CorrectosSector()
        Datos_CorrectosSector = False
        Try
            If String.IsNullOrEmpty(txtCodigoSector.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoSector.Focus()
            ElseIf String.IsNullOrEmpty(txtDescripcionSector.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Descripción", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDescripcionSector.Focus()
            Else
                Datos_CorrectosSector = True
            End If
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Datos_CorrectosTramo()

        Datos_CorrectosTramo = False

        Try

            If String.IsNullOrEmpty(txtCodigoTramo.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoTramo.Focus()
            ElseIf String.IsNullOrEmpty(txtDescripcionTramo.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Descripción", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDescripcionTramo.Focus()
            Else
                Datos_CorrectosTramo = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Datos_Correctos_Ubicacion()
        Datos_Correctos_Ubicacion = False
        Try
            If String.IsNullOrEmpty(txtCodigoBarraUbicacion.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código de Barra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoBarraUbicacion.Focus()
            ElseIf String.IsNullOrEmpty(txtCodigoBarra2ubicacion.Text.Trim()) AndAlso chkEsBodegaVirtual.Checked Then
                XtraMessageBox.Show("Ingrese Código de Barra 2.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoBarra2ubicacion.Focus()
            ElseIf String.IsNullOrEmpty(txtDescripcionUbicacion.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Descripción", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDescripcionUbicacion.Focus()
            Else
                Datos_Correctos_Ubicacion = True
            End If
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try
            mnuGuardar.Enabled = False

            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then

                        If XtraMessageBox.Show("Se guardó el registro. ¿Asignar áreas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            ControlPanelBodega.TabPages.Add(tabArea)
                            ControlPanelBodega.SelectedTabPage = tabArea
                            ControlPanelBodega.TabPages.Add(tabSector)
                            ControlPanelBodega.TabPages.Add(tabTramo)
                            ControlPanelBodega.TabPages.Add(TabUbicacion)
                            ControlPanelBodega.TabPages.Add(tabReferencia)
                            ControlPanelBodega.TabPages.Add(tabParametros)
                            mnuGuardar.Enabled = False
                            mnuActualizar.Enabled = True
                            mnuEliminar.Enabled = True
                            Llena_Bodega()
                            tabArea.Focus()

                        Else

                            DialogResult = DialogResult.OK

                            Close()

                        End If

                    Else
                        Close()
                    End If

                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If

                End If

            End If

            mnuGuardar.Enabled = True

        Catch ex As Exception
            mnuGuardar.Enabled = True
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Limpiar_Campos_Bodega_Area()

        Try

            txtCodigoAreaBodega.Text = String.Empty
            txtDescripcionAreaBodega.Text = String.Empty
            chkSistemaAreaBodega.Checked = False
            chkActivoAreaBodega.Checked = True
            nUpdAlto.Value = 0
            nUpdAncho.Value = 0
            nUpdLargo.Value = 0
            nUpdMargenDerecho.Value = 0
            nUpdMargenInferior.Value = 0
            nUpdMargenIzquierdo.Value = 0
            nUpdMargenSuperior.Value = 0
            cmdGuardarArea.Tag = Nothing
            txtGrupoArea.Text = String.Empty

            'pNuevaArea = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Limpiar_Campos_Bodega_Sector()
        Try

            txtCodigoSector.Text = String.Empty
            txtDescripcionSector.Text = String.Empty
            chkSistemaSector.Checked = False
            chkActivoSector.Checked = True
            nUpdAltoSector.Value = 0
            nUpdAnchoSector.Value = 0
            nUpdLargoSector.Value = 0
            nUpdMargenDerechoSector.Value = 0
            nUpdMargenInferiorSector.Value = 0
            nUpdMargenIzquierdoSector.Value = 0
            nUpdMargenSuperiorSector.Value = 0
            chkHorizontal.Checked = True
            txtPosX.Value = 0
            txtPosY.Value = 0
            pNuevoSector = True
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Limpiar_Campos_Bodega_Tramo()
        Try
            txtCodigoTramo.Text = String.Empty
            txtDescripcionTramo.Text = String.Empty
            chkSistemaTramo.Checked = False
            chkEsRack.Checked = False
            chkActivoTramo.Checked = True
            nUpdAltoTramo.Value = 0
            nUpdAnchoTramo.Value = 0
            nUpdLargoTramo.Value = 0
            nUpdMargenDerechoTramo.Value = 0
            nUpdMargenInferiorTramo.Value = 0
            nUpdMargenIzquierdoTramo.Value = 0
            nUpdMargenSuperiorTramo.Value = 0
            pNuevoTramo = True
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Limpiar_Campos_Bodega_Ubicacion()
        Try
            txtCodigoBarraUbicacion.Text = String.Empty
            txtCodigoBarra2ubicacion.Text = String.Empty
            txtDescripcionUbicacion.Text = String.Empty
            nUpdNivelUbicacion.Value = 0
            chkSistemaUbicacion.Checked = False
            chkActivoUbicacion.Checked = True
            chkDañadoUbicacion.Checked = False
            chkBloqueadaUbicacion.Checked = False
            chkAceptaPalletUbicacion.Checked = False
            chkUbicacionPicking.Checked = False
            chkRecepcion.Checked = False
            chkDespacho.Checked = False
            chkMerma.Checked = False
            nUpdAltoUbicacion.Value = 0
            nUpdLargoUbicacion.Value = 0
            nUpdAnchoUbicacion.Value = 0
            nUpdMargenIzquierdoUbicacion.Value = 0
            nUpdMargenDerechoUbicacion.Value = 0
            nUpdMargenSuperiorUbicacion.Value = 0
            nUpdMargenInferiorUbicacion.Value = 0
            pNuevaUbicacion = True
            cmbOrientacion.EditValue = String.Empty
            chkUbicPrdNE.Checked = False
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False

            If clsLnUsuario.Usuario_Valido(AP.UsuarioAp, pBeBodega.IdBodega) Then

                If (ControlPanelBodega.SelectedTabPageIndex = 0) Then
                    If XtraMessageBox.Show("¿Eliminar la Bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        pBeBodega.Activo = False
                        If clsLnBodega.Actualizar(pBeBodega) > 0 Then
                            XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If Listar IsNot Nothing Then
                                Listar.Invoke()
                            End If
                            Close()
                        End If
                    End If

                ElseIf ControlPanelBodega.SelectedTabPageIndex = 2 Then
                    If MessageBox.Show("¿Desactivar el area bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim Dr As DataRowView = GridViewArea.GetFocusedRow
                        pObjBAB.IdBodega = pBeBodega.IdBodega
                        pObjBAB.IdArea = CInt(Dr.Item("Correlativo"))
                        pObjBAB.Activo = False
                        If clsLnBodega_area.Actualizar(pObjBAB) > 0 Then
                            XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Cargar_Bodega_Areas()
                            Limpiar_Campos_Bodega_Area()
                        End If
                    End If
                ElseIf ControlPanelBodega.SelectedTabPageIndex = 3 Then
                    If MessageBox.Show("¿Desactivar el sector bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim Dr As DataRowView = GridViewSec.GetFocusedRow
                        pObjBS.IdArea = cmbArea.EditValue
                        pObjBS.IdSector = CInt(Dr.Item("Correlativo"))
                        pObjBS.Activo = False
                        If clsLnBodega_sector.Actualizar(pObjBS) > 0 Then
                            XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Cargar_Bodega_Sector(cmbArea.EditValue)
                            Limpiar_Campos_Bodega_Sector()
                        End If
                    End If
                ElseIf ControlPanelBodega.SelectedTabPageIndex = 4 Then
                    If MessageBox.Show("¿Desactivar el tramo bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim Dr As DataRowView = GridViewTramo.GetFocusedRow
                        pObjBT.IdSector = cmbSector.EditValue
                        pObjBT.IdTramo = CInt(Dr.Item("Correlativo"))
                        pObjBT.Activo = False
                        If clsLnBodega_tramo.Actualizar(pObjBT) > 0 Then
                            XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Cargar_Bodega_Tramo(cmbSector.EditValue)
                            Limpiar_Campos_Bodega_Tramo()
                        End If
                    End If
                ElseIf ControlPanelBodega.SelectedTabPageIndex = 5 Then
                    If MessageBox.Show("¿Desactivar la ubicacion bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim Dr As DataRowView = GridViewUbi.GetFocusedRow
                        pBeBodegaUbicacion.IdUbicacion = CInt(Dr.Item("Correlativo"))
                        pBeBodegaUbicacion.IdTramo = cmbTramo.EditValue
                        pBeBodegaUbicacion.Activo = False
                        If (clsLnBodega_ubicacion.Actualizar(pBeBodegaUbicacion)) > 0 Then
                            XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Cargar_Bodega_Ubicacion(cmbTramo.EditValue)
                            Limpiar_Campos_Bodega_Ubicacion()
                        End If
                    End If
                ElseIf ControlPanelBodega.SelectedTabPageIndex = 7 Then
                    If MessageBox.Show("¿Eliminar Parametro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim Dr As DataRowView = GridView1.GetFocusedRow
                        ObjBP.IdMonitor = CInt(Dr.Item("Monitor"))
                        ObjBP.IdBodega = pBeBodega.IdBodega
                        If (clsLnBodega_monitor_parametro.Eliminar(ObjBP)) > 0 Then
                            XtraMessageBox.Show("Se ha eliminado el parametro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Cargar_Parametros_Bodega()
                            Limpiar_Campos_Parametro()
                        End If
                    End If
                End If

                Llena_Bodega()
                LlenaCombos()

            Else

                If XtraMessageBox.Show("¿Está seguro de borrar la bodega", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If (ControlPanelBodega.SelectedTabPageIndex = 0) Then
                        If XtraMessageBox.Show("¿Eliminar la Bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            pBeBodega.Activo = False
                            If clsLnBodega.Actualizar(pBeBodega) > 0 Then
                                XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                If Listar IsNot Nothing Then
                                    Listar.Invoke()
                                End If
                                Close()
                            End If
                        End If

                    ElseIf ControlPanelBodega.SelectedTabPageIndex = 2 Then
                        If MessageBox.Show("¿Desactivar el area bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim Dr As DataRowView = GridViewArea.GetFocusedRow
                            pObjBAB.IdBodega = pBeBodega.IdBodega
                            pObjBAB.IdArea = CInt(Dr.Item("Correlativo"))
                            pObjBAB.Activo = False
                            If clsLnBodega_area.Actualizar(pObjBAB) > 0 Then
                                XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Cargar_Bodega_Areas()
                                Limpiar_Campos_Bodega_Area()
                            End If
                        End If
                    ElseIf ControlPanelBodega.SelectedTabPageIndex = 3 Then
                        If MessageBox.Show("¿Desactivar el sector bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim Dr As DataRowView = GridViewSec.GetFocusedRow
                            pObjBS.IdArea = cmbArea.EditValue
                            pObjBS.IdSector = CInt(Dr.Item("Correlativo"))
                            pObjBS.Activo = False
                            If clsLnBodega_sector.Actualizar(pObjBS) > 0 Then
                                XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Cargar_Bodega_Sector(cmbArea.EditValue)
                                Limpiar_Campos_Bodega_Sector()
                            End If
                        End If
                    ElseIf ControlPanelBodega.SelectedTabPageIndex = 4 Then
                        If MessageBox.Show("¿Desactivar el tramo bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim Dr As DataRowView = GridViewTramo.GetFocusedRow
                            pObjBT.IdSector = cmbSector.EditValue
                            pObjBT.IdTramo = CInt(Dr.Item("Correlativo"))
                            pObjBT.Activo = False
                            If clsLnBodega_tramo.Actualizar(pObjBT) > 0 Then
                                XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Cargar_Bodega_Tramo(cmbSector.EditValue)
                                Limpiar_Campos_Bodega_Tramo()
                            End If
                        End If
                    ElseIf ControlPanelBodega.SelectedTabPageIndex = 5 Then
                        If MessageBox.Show("¿Desactivar la ubicacion bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim Dr As DataRowView = GridViewUbi.GetFocusedRow
                            pBeBodegaUbicacion.IdUbicacion = CInt(Dr.Item("Correlativo"))
                            pBeBodegaUbicacion.IdTramo = cmbTramo.EditValue
                            pBeBodegaUbicacion.Activo = False
                            If (clsLnBodega_ubicacion.Actualizar(pBeBodegaUbicacion)) > 0 Then
                                XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Cargar_Bodega_Ubicacion(cmbTramo.EditValue)
                                Limpiar_Campos_Bodega_Ubicacion()
                            End If
                        End If
                    ElseIf ControlPanelBodega.SelectedTabPageIndex = 7 Then
                        If MessageBox.Show("¿Eliminar Parametro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim Dr As DataRowView = GridView1.GetFocusedRow
                            ObjBP.IdMonitor = CInt(Dr.Item("Monitor"))
                            ObjBP.IdBodega = pBeBodega.IdBodega
                            If (clsLnBodega_monitor_parametro.Eliminar(ObjBP)) > 0 Then
                                XtraMessageBox.Show("Se ha eliminado el parametro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Cargar_Parametros_Bodega()
                                Limpiar_Campos_Parametro()
                            End If
                        End If
                    End If

                    Llena_Bodega()
                    LlenaCombos()

                End If

            End If

            mnuEliminar.Enabled = True

        Catch ex As Exception

            mnuEliminar.Enabled = True

            If ex.HResult = -2146233088 Then
                TablasRelacionadas("bodega", ObjBP.IdBodega)
            Else
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                   Text,
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub grdUbicacion_DoubleClick(sender As Object, e As EventArgs) Handles grdUbicacion.DoubleClick

        Try

            Dim Obj As New clsBeBodega_ubicacion
            'Dim Dr As DataRowView = GridViewUbi.GetFocusedRow
            pUbicacionRow = GridViewUbi.GetFocusedRow


            If Not pUbicacionRow Is Nothing Then

                Obj = clsLnBodega_ubicacion.GetSingle(pUbicacionRow.Item("IdUbicacion"), pBeBodega.IdBodega)  '#CKFK 20210113 Reemplacé AP.IdBodega para que me muestre la información de otras bodegas

                If Obj.IdUbicacion <> 0 Then
                    pNuevaUbicacion = False
                End If

                chkSistemaUbicacion.Checked = Obj.Sistema
                txtDescripcionUbicacion.Text = Obj.Descripcion
                txtCodigoBarraUbicacion.Text = Obj.Codigo_barra
                txtCodigoBarra2ubicacion.Text = Obj.Codigo_barra2
                chkDañadoUbicacion.Checked = Obj.Dañado
                nUpdAnchoUbicacion.Value = Obj.Ancho
                nUpdLargoUbicacion.Value = Obj.Largo
                nUpdAltoUbicacion.Value = Obj.Alto
                chkActivoUbicacion.Checked = Obj.Activo
                chkBloqueadaUbicacion.Checked = Obj.Bloqueada
                nUpdNivelUbicacion.Value = Obj.Nivel
                chkAceptaPalletUbicacion.Checked = Obj.Acepta_pallet
                chkUbicacionPicking.Checked = Obj.Ubicacion_picking
                chkRecepcion.Checked = Obj.Ubicacion_recepcion
                chkDespacho.Checked = Obj.Ubicacion_despacho
                chkMerma.Checked = Obj.Ubicacion_merma
                chkUbicacionMuelle.Checked = Obj.Ubicacion_muelle
                nUpdMargenIzquierdoUbicacion.Value = Obj.Margen_izquierdo
                nUpdMargenDerechoUbicacion.Value = Obj.Margen_derecho
                nUpdMargenInferiorUbicacion.Value = Obj.Margen_inferior
                nUpdMargenInferiorUbicacion.Value = Obj.Margen_inferior
                cmbTipoRotacion.EditValue = Obj.IdTipoRotacion
                cmbIndiceRotacion.EditValue = Obj.IdIndiceRotacion
                pBeBodegaUbicacion.IdUbicacion = Obj.IdUbicacion
                txtIndiceX.Value = Obj.Indice_x
                cmbOrientacion.EditValue = Obj.Orientacion_pos
                chkUbicPrdNE.Checked = Obj.ubicacion_ne
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

    Private Sub grdTramo_DoubleClick(sender As Object, e As EventArgs) Handles grdTramo.DoubleClick

        Try

            Dim Obj As New clsBeBodega_tramo
            'Dim Dr As DataRowView = GridViewTramo.GetFocusedRow
            pTramoRow = GridViewTramo.GetFocusedRow

            Obj = clsLnBodega_tramo.GetSingle(pTramoRow.Item("Correlativo"), pBeBodega.IdBodega)

            If Not Obj Is Nothing Then

                If Obj.IdTramo <> 0 Then
                    pNuevoTramo = False
                End If

                txtIdTramo.Text = Obj.IdTramo
                txtCodigoTramo.Text = Obj.Codigo
                txtDescripcionTramo.Text = Obj.Descripcion
                chkSistemaTramo.Checked = Obj.Sistema
                chkEsRack.Checked = Obj.Es_Rack
                chkActivoTramo.Checked = Obj.Activo
                nUpdAltoTramo.Value = Obj.Alto
                nUpdLargoTramo.Value = Obj.Largo
                nUpdAnchoTramo.Value = Obj.Ancho
                nUpdMargenIzquierdoTramo.Value = Obj.Margen_izquierdo
                nUpdMargenDerechoTramo.Value = Obj.Margen_derecho
                nUpdMargenSuperiorTramo.Value = Obj.Margen_superior
                nUpdMargenInferiorTramo.Value = Obj.Margen_inferior
                pObjBT.IdTramo = Obj.IdTramo
                cmbFont.EditValue = Obj.IdFontEnc
                chkOrientacion.Checked = Obj.Horizontal
                chkOrient.Checked = Obj.Orientacion
                txtIndice.Text = Obj.Indice_x
                txtTipoRack.Text = Obj.IdTipoRack
                'cargarBodegaTramo(Obj.IdSector)
            Else
                Throw New Exception("No se encontró data para Bodega-tramo, #GT20221701")
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

    Private Sub grdSectorArea_DoubleClick(sender As Object, e As EventArgs) Handles grdSectorArea.DoubleClick

        Try

            Dim Obj As New clsBeBodega_sector
            Dim Dr As DataRowView = GridViewSec.GetFocusedRow

            Obj = clsLnBodega_sector.GetSingle(Dr.Item("IdSector"), pBeBodega.IdBodega)

            If Obj.IdSector <> 0 Then
                pNuevoSector = False
            End If

            txtIdSector.Text = Obj.IdSector
            txtCodigoSector.Text = Obj.Codigo
            txtDescripcionSector.Text = Obj.Descripcion
            chkSistemaSector.Checked = Obj.Sistema
            chkActivoSector.Checked = Obj.Activo
            nUpdAltoSector.Value = Obj.Alto
            nUpdLargoSector.Value = Obj.Largo
            nUpdAnchoSector.Value = Obj.Ancho
            nUpdMargenIzquierdoSector.Value = Obj.Margen_izquierdo
            nUpdMargenDerechoSector.Value = Obj.Margen_derecho
            nUpdMargenSuperiorSector.Value = Obj.Margen_superior
            nUpdMargenInferiorSector.Value = Obj.Margen_inferior
            chkHorizontal.Checked = Obj.Horizontal
            txtPosX.Value = Obj.Pos_x
            txtPosY.Value = Obj.Pos_y
            pObjBS.IdSector = Obj.IdSector
            Cargar_Bodega_Tramo(Obj.IdSector)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub chkSectoresActivos_CheckedChanged(sender As Object, e As EventArgs) Handles chkSectoresActivos.CheckedChanged, chkActivoSector.CheckedChanged
        Try
            Cargar_Bodega_Sector(cmbArea.EditValue)
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub chkTramosActivos_CheckedChanged(sender As Object, e As EventArgs) Handles chkTramosActivos.CheckedChanged
        Try
            Cargar_Bodega_Tramo(cmbSector.EditValue)
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub chkUbicacionesActivas_CheckedChanged(sender As Object, e As EventArgs) Handles chkUbicacionesActivas.CheckedChanged
        Try
            Cargar_Bodega_Ubicacion(cmbTramo.EditValue)
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Public Sub Llena_Orientacion()

        Try

            Dim DT1 As DataTable = New DataTable()
            DT1 = clsLnBodega_orientacion_pos.Listar()

            'dt.Columns.Add("Codigo")
            'dt.Columns.Add("Descripcion")

            'Dim dr As DataRow = dt.NewRow()
            'dr("Codigo") = "FL"
            'dr(1) = "FL"
            'dt.Rows.Add(dr)

            'dr = dt.NewRow()
            'dr(0) = "BL"
            'dr(1) = "BL"
            'dt.Rows.Add(dr)

            'dr = dt.NewRow()
            'dr(0) = "FR"
            'dr(1) = "FR"
            'dt.Rows.Add(dr)

            'dr = dt.NewRow()
            'dr(0) = "BR"
            'dr(1) = "BR"
            'dt.Rows.Add(dr)

            cmbOrientacion.Properties.DataSource = DT1
            cmbOrientacion.Properties.ValueMember = "Codigo"
            cmbOrientacion.Properties.DisplayMember = "Nombre"

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub LlenaCombos()
        Try
            If Not IMS.Listar_Areas_By_Bodega(cmbArea, pBeBodega.IdBodega) Then
            End If
            If Not IMS.Listar_Areas_By_Bodega(cmbAreasR, pBeBodega.IdBodega) Then
            End If
            If Not IMS.Listar_Areas_By_Bodega(cmbAreaUbic, pBeBodega.IdBodega) Then
            End If
            cmbAreasR_EditValueChanged(Nothing, Nothing)
            cmbSectorR_EditValueChanged(Nothing, Nothing)
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Llena_Bodega()

        Try

            tlUbicaciones.ClearNodes()

            Crea_Bodega(tlUbicaciones)

            Crea_Areas(tlUbicaciones, pBeBodega.IdBodega)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Crea_Bodega(ByVal tl As TreeList)

        tl.BeginUpdate()
        tl.Columns.Add()
        tl.Columns(0).Caption = pBeBodega.IdBodega
        tl.Columns(0).VisibleIndex = 0
        tl.Columns.Add()
        tl.Columns(1).Caption = pBeBodega.Nombre
        tl.Columns(1).VisibleIndex = 1
        tl.Columns.Add()
        tl.EndUpdate()

    End Sub

    Private Sub Crea_Areas(ByVal tl As TreeList, ByVal IdBodega As Integer)

        Dim clsTransaccion As New clsTransaccion

        Try

            clsTransaccion.Begin_Transaction()

            DsBodega.Clear()
            tl.BeginUnboundLoad()

            DTArea = clsLnBodega_area.Get_All_Areas_By_IdBodega(IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            Dim parentForRootNodes As TreeListNode = Nothing
            Dim rootNode As TreeListNode

            For Each r As DataRow In DTArea.Rows
                rootNode = tl.AppendNode(New Object() {r.Item("IdArea").ToString(), "Area: " & r.Item("Descripcion")}, parentForRootNodes)
                Crea_Sectores(tlUbicaciones, r.Item("IdArea"), rootNode)
            Next

            tl.EndUnboundLoad()

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub Crea_Sectores(ByVal tl As TreeList, idArea As Integer, Padre As TreeListNode)

        Try

            tl.BeginUnboundLoad()

            If Not pBeBodega Is Nothing Then

                DTSector = clsLnBodega_sector.Get_All_Sector_By_Area_And_IdBodega(idArea, pBeBodega.IdBodega)

                Dim rootNode As TreeListNode

                For Each r As DataRow In DTSector.Rows
                    rootNode = tl.AppendNode(New Object() {r.Item("IdSector"), "Sector: " & r.Item("Descripcion")}, Padre)
                    Crea_Tramos(tlUbicaciones, r.Item("IdSector"), rootNode)
                    Application.DoEvents()
                Next

            End If

            tl.EndUnboundLoad()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Crea_Tramos(ByVal tl As TreeList,
                            ByVal IdSector As Integer,
                            ByVal Padre As TreeListNode)

        Try

            tl.BeginUnboundLoad()

            If Not pBeBodega Is Nothing Then

                DTTramo = clsLnBodega_tramo.Get_All_Tramos_By_Sector_And_IdBodega(IdSector, pBeBodega.IdBodega)

                Dim rootNode As TreeListNode

                For Each r As DataRow In DTTramo.Rows
                    rootNode = tl.AppendNode(New Object() {r.Item("IdTramo"), "Tramo: " & r.Item("Descripcion")}, Padre)
                    Crea_Ubicaciones(tlUbicaciones, r.Item("IdTramo"), rootNode)
                Next

                tl.EndUnboundLoad()

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

    Private Sub Crea_Ubicaciones(ByVal tl As TreeList,
                                 ByVal IdTramo As Integer,
                                 ByVal Padre As TreeListNode)

        Try

            tl.AppendNode(New Object() {"Cantidad_Ubicaciones:", clsLnBodega_ubicacion.Get_Count_Ubicaciones_By_IdTramo(IdTramo, pBeBodega.IdBodega)}, Padre)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            mnuActualizar.Enabled = False

            If (ControlPanelBodega.SelectedTabPageIndex = 0 Or ControlPanelBodega.SelectedTabPageIndex = 8 Or ControlPanelBodega.SelectedTabPageIndex = 1) Then
                If Actualizar() Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
                    Return
                End If
            ElseIf ControlPanelBodega.SelectedTabPageIndex = 2 Then

                If (Actualizar_Bodega_Area()) Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Limpiar_Campos_Bodega_Area()
                End If

            ElseIf ControlPanelBodega.SelectedTabPageIndex = 3 Then

                If (Actualizar_Bodega_Sector()) Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Limpiar_Campos_Bodega_Sector()
                    Cargar_Bodega_Sector(cmbArea.EditValue)
                End If

            ElseIf ControlPanelBodega.SelectedTabPageIndex = 4 Then

                If (Actualizar_Bodega_Tramo()) Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Limpiar_Campos_Bodega_Tramo()
                    Cargar_Bodega_Tramo(cmbSector.EditValue)
                End If

            ElseIf ControlPanelBodega.SelectedTabPageIndex = 5 Then

                If (Actualizar_Bodega_Ubicacion()) Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Limpiar_Campos_Bodega_Ubicacion()
                    Cargar_Bodega_Ubicacion(cmbTramo.EditValue)
                End If

            ElseIf ControlPanelBodega.SelectedTabPageIndex = 6 Then
                If (ActualizarParametroBodega()) Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Limpiar_Campos_Parametro()
                    Cargar_Parametros_Bodega()
                End If
            End If

            Llena_Bodega()

            mnuActualizar.Enabled = True

        Catch ex As Exception
            mnuActualizar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub lnkUbicacionRecepcion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacionRecepcion.LinkClicked
        Try
            Dim Ubicacion As New frmBodegaT() With {.Modo = frmBodegaT.pModo.Seleccion}
            Ubicacion.pObjBeB.IdBodega = pBeBodega.IdBodega
            Ubicacion.Nombre_Campo = "ubicacion_recepcion"
            Ubicacion.ShowDialog()
            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacionRecepcion.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacionRecepcion.Text = Ubicacion.pObj.Descripcion
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

    Private Sub lnkUbicacionPicking_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacionPicking.LinkClicked
        Try
            Dim Ubicacion As New frmBodegaT() With {.Modo = frmBodegaT.pModo.Seleccion}
            Ubicacion.pObjBeB.IdBodega = pBeBodega.IdBodega
            Ubicacion.Nombre_Campo = "ubicacion_picking"
            Ubicacion.ShowDialog()
            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacionPicking.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacionPicking.Text = Ubicacion.pObj.Descripcion
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

    Private Sub lnkUbicacionDespacho_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacionDespacho.LinkClicked
        Try
            Dim Ubicacion As New frmBodegaT() With {.Modo = frmBodegaT.pModo.Seleccion}
            Ubicacion.pObjBeB.IdBodega = pBeBodega.IdBodega
            Ubicacion.Nombre_Campo = "ubicacion_despacho"
            Ubicacion.ShowDialog()
            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacionDespacho.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacionDespacho.Text = Ubicacion.pObj.Descripcion
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

    Private Sub lnkUbicacionMerma_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacionMerma.LinkClicked
        Try
            Dim Ubicacion As New frmBodegaT() With {.Modo = frmBodegaT.pModo.Seleccion}
            Ubicacion.pObjBeB.IdBodega = pBeBodega.IdBodega
            Ubicacion.Nombre_Campo = "ubicacion_merma"
            Ubicacion.ShowDialog()
            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacionMerma.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacionMerma.Text = Ubicacion.pObj.Descripcion
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

    Private Sub txtIdUbicacionRecepcion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacionRecepcion.KeyPress, txtIdUbicacionPrdNE.KeyPress

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
            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdUbicacionRecepcion.Text.Length = 1 Then
                txtNombreUbicacionRecepcion.Text = String.Empty
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

    Private Sub txtIdUbicacionRecepcion_LostFocus(sender As Object, e As EventArgs) Handles txtIdUbicacionRecepcion.LostFocus

        Try

            If String.IsNullOrEmpty(txtIdUbicacionRecepcion.Text.Trim()) = False Then
                If Not pBeBodega Is Nothing Then
                    Dim Obj As New clsBeBodega_ubicacion
                    Obj = clsLnBodega_ubicacion.GetSingle(CInt(txtIdUbicacionRecepcion.Text.Trim()), pBeBodega.IdBodega, "ubicacion_recepcion")
                    If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                        txtIdUbicacionRecepcion.Text = Obj.IdUbicacion
                        txtNombreUbicacionRecepcion.Text = Obj.Descripcion
                    Else

                        '#CKFK 20210727 Quité este mensaje porque no es necesario, las ubicaciones por defecto se validan
                        'XtraMessageBox.Show(String.Format("No existe ubicación recepción con código {0}", txtIdUbicacionRecepcion.Text.Trim()),
                        '                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        txtIdUbicacionRecepcion.Text = String.Empty
                        txtNombreUbicacionRecepcion.Text = String.Empty

                        txtIdUbicacionRecepcion.Focus()
                    End If
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

    Private Sub txtIdUbicacionRecepcion_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdUbicacionRecepcion.PreviewKeyDown
        Try
            If e.KeyData = Keys.Enter Then
                If String.IsNullOrEmpty(txtIdUbicacionRecepcion.Text.Trim()) = False Then
                    Dim Obj As New clsBeBodega_ubicacion
                    Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacionRecepcion.Text.Trim(), "ubicacion_recepcion")
                    If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                        txtIdUbicacionRecepcion.Text = Obj.IdUbicacion
                        txtNombreUbicacionRecepcion.Text = Obj.Descripcion
                    Else

                        XtraMessageBox.Show(String.Format("No existe ubicación recepción con código {0}", txtIdUbicacionRecepcion.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        txtIdUbicacionRecepcion.Text = String.Empty
                        txtNombreUbicacionRecepcion.Text = String.Empty

                        txtIdUbicacionRecepcion.Focus()
                    End If
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

    Private Sub txtIdUbicacionPicking_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacionPicking.KeyPress
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
            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdUbicacionPicking.Text.Length = 1 Then
                txtNombreUbicacionPicking.Text = String.Empty
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

    Private Sub txtIdUbicacionPicking_LostFocus(sender As Object, e As EventArgs) Handles txtIdUbicacionPicking.LostFocus
        Try
            If String.IsNullOrEmpty(txtIdUbicacionPicking.Text.Trim()) = False Then
                Dim Obj As New clsBeBodega_ubicacion
                Obj = clsLnBodega_ubicacion.GetSingle(CInt(txtIdUbicacionPicking.Text.Trim()), pBeBodega.IdBodega, "ubicacion_picking")
                If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                    txtIdUbicacionPicking.Text = Obj.IdUbicacion
                    txtNombreUbicacionPicking.Text = Obj.Descripcion
                Else

                    '#CKFK 20210727 Quité este mensaje porque no es necesario, las ubicaciones por defecto se validan
                    'XtraMessageBox.Show(String.Format("No existe ubicación picking con código {0}", txtIdUbicacionPicking.Text.Trim()),
                    '                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    txtIdUbicacionPicking.Text = String.Empty
                    txtNombreUbicacionPicking.Text = String.Empty
                    txtIdUbicacionPicking.Focus()
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

    Private Sub txtIdUbicacionPicking_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdUbicacionPicking.PreviewKeyDown
        Try

            If e.KeyData = Keys.Enter Then

                If String.IsNullOrEmpty(txtIdUbicacionPicking.Text.Trim()) = False Then
                    Dim Obj As New clsBeBodega_ubicacion
                    Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacionPicking.Text.Trim(), "ubicacion_picking")

                    If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                        txtIdUbicacionPicking.Text = Obj.IdUbicacion
                        txtNombreUbicacionPicking.Text = Obj.Descripcion

                    Else

                        XtraMessageBox.Show(String.Format("No existe ubicación picking con código {0}", txtIdUbicacionPicking.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtIdUbicacionPicking.Text = String.Empty
                        txtNombreUbicacionPicking.Text = String.Empty
                        txtIdUbicacionPicking.Focus()

                    End If

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

    Private Sub txtIdUbicacionDespacho_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacionDespacho.KeyPress
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
            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdUbicacionDespacho.Text.Length = 1 Then
                txtNombreUbicacionDespacho.Text = String.Empty
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

    Private Sub txtIdUbicacionDespacho_LostFocus(sender As Object, e As EventArgs) Handles txtIdUbicacionDespacho.LostFocus

        Try

            If String.IsNullOrEmpty(txtIdUbicacionDespacho.Text.Trim()) = False Then

                If Not pBeBodega Is Nothing Then

                    Dim Obj As New clsBeBodega_ubicacion
                    Obj = clsLnBodega_ubicacion.GetSingle(CInt(txtIdUbicacionDespacho.Text.Trim()), pBeBodega.IdBodega, "ubicacion_despacho")

                    If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then

                        txtIdUbicacionDespacho.Text = Obj.IdUbicacion
                        txtNombreUbicacionDespacho.Text = Obj.Descripcion

                    Else

                        '#CKFK 20210727 Quité este mensaje porque no es necesario, las ubicaciones por defecto se validan
                        'XtraMessageBox.Show(String.Format("No existe ubicación despacho con código {0}", txtIdUbicacionDespacho.Text.Trim()),
                        '                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtIdUbicacionDespacho.Text = String.Empty
                        txtNombreUbicacionDespacho.Text = String.Empty
                        txtIdUbicacionDespacho.Focus()

                    End If

                End If

            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdUbicacionDespacho_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdUbicacionDespacho.PreviewKeyDown
        Try

            If e.KeyData = Keys.Enter Then

                If String.IsNullOrEmpty(txtIdUbicacionDespacho.Text.Trim()) = False Then

                    Dim Obj As New clsBeBodega_ubicacion
                    Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacionDespacho.Text.Trim(), "ubicacion_despacho")

                    If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then

                        txtIdUbicacionDespacho.Text = Obj.IdUbicacion
                        txtNombreUbicacionDespacho.Text = Obj.Descripcion

                    Else

                        XtraMessageBox.Show(String.Format("No existe ubicación despacho con código {0}", txtIdUbicacionPicking.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtIdUbicacionDespacho.Text = String.Empty
                        txtNombreUbicacionDespacho.Text = String.Empty
                        txtIdUbicacionDespacho.Focus()

                    End If

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

    Private Sub txtIdUbicacionMerma_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacionMerma.KeyPress
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
            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdUbicacionMerma.Text.Length = 1 Then
                txtNombreUbicacionMerma.Text = String.Empty
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

    Private Sub txtIdUbicacionMerma_LostFocus(sender As Object, e As EventArgs) Handles txtIdUbicacionMerma.LostFocus

        Try

            If String.IsNullOrEmpty(txtIdUbicacionMerma.Text.Trim()) = False Then

                If pBeBodega Is Nothing Then Exit Sub

                Dim Obj As New clsBeBodega_ubicacion
                Obj = clsLnBodega_ubicacion.GetSingle(CInt(txtIdUbicacionMerma.Text.Trim()), pBeBodega.IdBodega, "ubicacion_merma")

                If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then

                    txtIdUbicacionMerma.Text = Obj.IdUbicacion
                    txtNombreUbicacionMerma.Text = Obj.Descripcion

                Else

                    '#CKFK 20210727 Quité este mensaje porque no es necesario, las ubicaciones por defecto se validan
                    'XtraMessageBox.Show(String.Format("No existe ubicación merma con código {0}", txtIdUbicacionMerma.Text.Trim()),
                    '                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtIdUbicacionMerma.Text = String.Empty
                    txtIdUbicacionMerma.Focus()

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

    Private Sub txtIdUbicacionMerma_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdUbicacionMerma.PreviewKeyDown
        Try

            If e.KeyData = Keys.Enter Then

                If String.IsNullOrEmpty(txtIdUbicacionMerma.Text.Trim()) = False Then

                    Dim Obj As New clsBeBodega_ubicacion
                    Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacionMerma.Text.Trim(), "ubicacion_merma")

                    If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then

                        txtIdUbicacionMerma.Text = Obj.IdUbicacion
                        txtNombreUbicacionMerma.Text = Obj.Descripcion
                    Else

                        XtraMessageBox.Show(String.Format("No existe ubicación merma con código {0}", txtIdUbicacionMerma.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtIdUbicacionMerma.Text = String.Empty
                        txtIdUbicacionMerma.Focus()

                    End If

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
    Private Sub lnkTareas_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkTareas.LinkClicked
        Dim Tarea As New frmBodegaMonitor_List() With {.Modo = frmBodegaMonitor_List.pModo.Seleccion}
        Tarea.ShowDialog()

        If Tarea.pObjTB IsNot Nothing AndAlso Tarea.pObjTB.IdParametroBodega <> 0 Then
            txtIdTarea.Text = Tarea.pObjTB.IdParametroBodega
            txtNombreTarea.Text = Tarea.pObjTB.Nombre
        End If

    End Sub

    Private Sub txtIdTarea_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdTarea.KeyPress
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

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdTarea_LostFocus(sender As Object, e As EventArgs) Handles txtIdTarea.LostFocus
        Try

            If String.IsNullOrEmpty(txtIdTarea.Text.Trim()) = False Then

                Dim Obj As New clsBeBodega_parametros
                Obj = clsLnBodega_parametros.GetSingle(txtIdTarea.Text.Trim())

                If Obj IsNot Nothing AndAlso Obj.IdParametroBodega > 0 Then

                    txtIdTarea.Text = Obj.IdParametroBodega
                    txtNombreTarea.Text = Obj.Nombre

                Else

                    txtIdTarea.Text = String.Empty
                    txtNombreTarea.Text = String.Empty
                    XtraMessageBox.Show(String.Format("No existe motivo ubicación con código {0}", txtIdTarea.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdTarea.Focus()

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

    Private Sub txtIdTarea_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdTarea.PreviewKeyDown
        Try
            If e.KeyData = Keys.Tab Then
                txtIdTarea_LostFocus(Nothing, Nothing)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If (pNuevoParametro) Then
                If Datos_CorrectosParametros() Then
                    If (guardarParametroBodega()) Then
                        Limpiar_Campos_Parametro()
                    End If
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

    Private Function guardarParametroBodega() As Boolean

        guardarParametroBodega = False

        Try
            ObjBP.IdMonitor = clsLnBodega_monitor_parametro.MaxID()
            ObjBP.IdBodega = pBeBodega.IdBodega
            ObjBP.Nombre = txtNombreTarea.Text
            ObjBP.TiempoActualizacion = txtTiempoActualizacionP.Value

            If clsLnBodega_monitor_parametro.Exists(pBeBodega.IdBodega, txtIdTarea.Text) Then

                XtraMessageBox.Show("Ya existe un parametro con esta configuración", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Limpiar_Campos_Parametro()

            Else

                guardarParametroBodega = clsLnBodegaP.Insertar(ObjBP) > 0

            End If

            Cargar_Parametros_Bodega()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Cargar_Parametros_Bodega()

        Try

            Dim ListObjBP As New List(Of clsBeBodega_monitor_parametro)
            ListObjBP = clsLnBodega_monitor_parametro.GetAllByIdBodega(pBeBodega.IdBodega)

            If ListObjBP.Count > 0 Then

                Dim DT As New DataTable("BodegaArea")
                DT.Columns.Add("Monitor", GetType(Integer))
                DT.Columns.Add("Bodega", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("Tiempo", GetType(String))

                Parallel.ForEach(ListObjBP, Sub(ByVal Obj As clsBeBodega_monitor_parametro)
                                                SyncLock DT
                                                    Dim lRow As DataRow = DT.NewRow()
                                                    lRow(0) = Obj.IdMonitor
                                                    lRow(1) = Obj.IdBodega
                                                    lRow(2) = Obj.Nombre
                                                    lRow(3) = Obj.TiempoActualizacion
                                                    DT.Rows.Add(lRow)
                                                End SyncLock
                                            End Sub)

                'For Each Obj As clsBeBodega_monitor_parametro In ListObjBP
                '    Dim lRow As DataRow = DT.NewRow()
                '    lRow(0) = Obj.IdMonitor
                '    lRow(1) = Obj.IdBodega
                '    lRow(2) = Obj.Nombre
                '    lRow(3) = Obj.TiempoActualizacion
                '    DT.Rows.Add(lRow)
                'Next

                Dgrid.DataSource = DT

                If GridView1.Columns.Count > 0 Then GridView1.Columns(1).Visible = False

            Else
                Dgrid.DataSource = Nothing
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

    Private Function Datos_CorrectosParametros()
        Datos_CorrectosParametros = False

        Try

            If String.IsNullOrEmpty(txtIdTarea.Text) Then
                XtraMessageBox.Show("Seleccione la tarea", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtIdTarea.Focus()
            Else
                Datos_CorrectosParametros = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Limpiar_Campos_Parametro()
        txtIdTarea.Text = String.Empty
        txtNombreTarea.Text = String.Empty
        txtTiempoActualizacionP.Value = Nothing
        pNuevoParametro = True
    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Try
            Dim obj As New clsBeBodega_monitor_parametro
            Dim Dr As DataRowView = GridView1.GetFocusedRow

            obj = clsLnBodega_monitor_parametro.GetSingle(pBeBodega.IdBodega, Dr.Item("Monitor"))

            If obj.IdMonitor <> 0 Then
                pNuevoParametro = False
            End If

            txtIdTarea.Text = obj.IdMonitor
            txtNombreTarea.Text = obj.Nombre
            txtTiempoActualizacionP.Value = obj.TiempoActualizacion

            ObjBP.IdMonitor = obj.IdMonitor
            ObjBP.IdBodega = obj.IdBodega
            Cargar_Parametros_Bodega()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function ActualizarParametroBodega() As Boolean

        ActualizarParametroBodega = False

        Try
            Dim Dr As DataRowView = GridView1.GetFocusedRow

            ObjBP.IdMonitor = CInt(Dr.Item("Monitor"))
            ObjBP.IdBodega = pBeBodega.IdBodega
            ObjBP.TiempoActualizacion = txtTiempoActualizacionP.Value
            ActualizarParametroBodega = clsLnBodegaP.Actualizar(ObjBP) > 0
            Cargar_Parametros_Bodega()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Function

    Private Sub GridViewSec_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewSec.RowStyle

        Try

            GridViewSec.OptionsBehavior.Editable = False
            GridViewSec.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewSec.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridViewSec.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewSec.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewSec.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewSec.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewSec.Appearance.FocusedRow.ForeColor = Color.White
            GridViewSec.Appearance.SelectedRow.ForeColor = Color.White

            GridViewSec.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridViewTramo_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewTramo.RowStyle

        Try

            GridViewTramo.OptionsBehavior.Editable = False
            GridViewTramo.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewTramo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridViewTramo.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewTramo.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewTramo.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewTramo.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewTramo.Appearance.FocusedRow.ForeColor = Color.White
            GridViewTramo.Appearance.SelectedRow.ForeColor = Color.White

            GridViewTramo.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridViewUbi_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewUbi.RowStyle

        Try

            GridViewUbi.OptionsBehavior.Editable = False
            GridViewUbi.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewUbi.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridViewUbi.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewUbi.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewUbi.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewUbi.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewUbi.Appearance.FocusedRow.ForeColor = Color.White
            GridViewUbi.Appearance.SelectedRow.ForeColor = Color.White

            GridViewUbi.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdNuevaArea_Click(sender As Object, e As EventArgs) Handles cmdNuevaArea.Click
        Limpiar_Campos_Bodega_Area() : Focus() : txtCodigoAreaBodega.Focus()
    End Sub

    Private Sub cmdGuardarArea_Click(sender As Object, e As EventArgs) Handles cmdGuardarArea.Click

        If Datos_CorrectosArea() = False Then
            Cursor = Cursors.Default
            Return
        End If

        Try

            If cmdGuardarArea.Tag Is Nothing Then
                If clsLnBodega_area.Existe_Codigo_By_IdBodega(txtCodigoAreaBodega.Text.Trim(), pBeBodega.IdBodega) Then
                    XtraMessageBox.Show(String.Format("El código de área: {0} ya existe", txtCodigoAreaBodega.Text),
                                        Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If

            Cursor = Cursors.WaitCursor

            Dim pIndex As Integer = -1

            pIndex = pListObjBodegaAreas.FindIndex(Function(b) b.IdArea = CInt(cmdGuardarArea.Tag))

            If pIndex > -1 Then

                pListObjBodegaAreas(pIndex).Codigo = txtCodigoAreaBodega.Text.Trim()
                pListObjBodegaAreas(pIndex).Descripcion = txtDescripcionAreaBodega.Text.Trim()
                pListObjBodegaAreas(pIndex).Alto = nUpdAlto.Value
                pListObjBodegaAreas(pIndex).Largo = nUpdLargo.Value
                pListObjBodegaAreas(pIndex).Ancho = nUpdAncho.Value
                pListObjBodegaAreas(pIndex).Margen_izquierdo = nUpdMargenIzquierdo.Value
                pListObjBodegaAreas(pIndex).Margen_derecho = nUpdMargenDerecho.Value
                pListObjBodegaAreas(pIndex).Margen_superior = nUpdMargenSuperior.Value
                pListObjBodegaAreas(pIndex).Margen_inferior = nUpdMargenInferior.Value
                pListObjBodegaAreas(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                pListObjBodegaAreas(pIndex).Fec_mod = Now
                pListObjBodegaAreas(pIndex).Grupo = txtGrupoArea.Text.Trim()
                pListObjBodegaAreas(pIndex).Activo = chkActivoAreaBodega.Checked
                pListObjBodegaAreas(pIndex).IdUbicacionRef = txtUbicacionRecepcionArea.Text

            Else

                Dim ObjN As New clsBeBodega_area() With {.IdBodega = pBeBodega.IdBodega, .Sistema = False}

                If pListObjBodegaAreas IsNot Nothing AndAlso pListObjBodegaAreas.Count > 0 Then
                    ObjN.IdArea = pListObjBodegaAreas.Max(Function(b) b.IdArea) + 1
                Else
                    ObjN.IdArea = 1
                End If

                ObjN.Codigo = txtCodigoAreaBodega.Text.Trim()
                ObjN.Descripcion = txtDescripcionAreaBodega.Text.Trim()
                ObjN.Alto = nUpdAlto.Value
                ObjN.Largo = nUpdLargo.Value
                ObjN.Ancho = nUpdAncho.Value
                ObjN.Margen_izquierdo = nUpdMargenIzquierdo.Value
                ObjN.Margen_derecho = nUpdMargenDerecho.Value
                ObjN.Margen_superior = nUpdMargenSuperior.Value
                ObjN.Margen_inferior = nUpdMargenInferior.Value
                ObjN.Activo = True
                ObjN.User_agr = AP.UsuarioAp.IdUsuario
                ObjN.Fec_agr = Now
                ObjN.User_mod = AP.UsuarioAp.IdUsuario
                ObjN.Fec_mod = Now
                ObjN.Grupo = txtGrupoArea.Text.Trim
                ObjN.IsNew = True
                ObjN.IdUbicacionRef = txtUbicacionRecepcionArea.EditValue

                pListObjBodegaAreas.Add(ObjN)

                pIndex = pListObjBodegaAreas.Count - 1

            End If

            '#EJC201180122: Actualizar la BD directamente en bodega_area
            If (Actualizar_Bodega_Area(pIndex)) Then
                XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Limpiar_Campos_Bodega_Area()
            End If

            Cursor = Cursors.Default

            Cargar_Bodega_Areas()

            LlenaCombos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub grdAreaBodega_DoubleClick(sender As Object, e As EventArgs) Handles grdAreaBodega.DoubleClick

        Try

            If GridViewArea.RowCount > 0 Then

                Dim Dr As DataRowView = GridViewArea.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pListObjBodegaAreas.FindIndex(Function(b) b.IdArea = Dr.Item("Correlativo") AndAlso b.IdBodega = pBeBodega.IdBodega)

                If lIndex > -1 Then

                    txtIdArea.Text = pListObjBodegaAreas(lIndex).IdArea
                    cmdGuardarArea.Tag = pListObjBodegaAreas(lIndex).IdArea
                    txtCodigoAreaBodega.Text = pListObjBodegaAreas(lIndex).Codigo
                    txtDescripcionAreaBodega.Text = pListObjBodegaAreas(lIndex).Descripcion
                    chkSistemaAreaBodega.Checked = pListObjBodegaAreas(lIndex).Sistema
                    chkActivoAreaBodega.Checked = pListObjBodegaAreas(lIndex).Activo
                    nUpdAlto.Value = pListObjBodegaAreas(lIndex).Alto
                    nUpdLargo.Value = pListObjBodegaAreas(lIndex).Largo
                    nUpdAncho.Value = pListObjBodegaAreas(lIndex).Ancho
                    nUpdMargenIzquierdo.Value = pListObjBodegaAreas(lIndex).Margen_izquierdo
                    nUpdMargenDerecho.Value = pListObjBodegaAreas(lIndex).Margen_derecho
                    nUpdMargenSuperior.Value = pListObjBodegaAreas(lIndex).Margen_superior
                    nUpdMargenInferior.Value = pListObjBodegaAreas(lIndex).Margen_inferior
                    txtGrupoArea.Text = pListObjBodegaAreas(lIndex).Grupo
                    If pListObjBodegaAreas(lIndex).IdUbicacionRef > 0 Then
                        Dim pUbicacionRef = clsLnBodega_ubicacion.Get_Descripcion_IdUbicacion(pListObjBodegaAreas(lIndex).IdUbicacionRef, pListObjBodegaAreas(lIndex).IdBodega)
                        txtUbicacionRecepcionArea.Text = pListObjBodegaAreas(lIndex).IdUbicacionRef
                        txtNombreUbicacionRecepcionArea.Text = pUbicacionRef.ToString()
                    Else
                        txtUbicacionRecepcionArea.Text = ""
                        txtNombreUbicacionRecepcionArea.Text = ""
                    End If

                    Focus()

                    txtCodigoAreaBodega.SelectAll()

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

    Private Sub chkAreasBodegaActivos_CheckedChanged(sender As Object, e As EventArgs) Handles chkAreasBodegaActivos.CheckedChanged
        Cargar_Bodega_Areas()
    End Sub

    Private Sub txtIdTipoTR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdTipoTR.KeyPress

        Try

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdTipoTR.Text.Length = 1 Then
                txtDescripcionTR.Text = String.Empty
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub txtIdTipoTR_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdTipoTR.PreviewKeyDown

        Try

            If e.KeyData = Keys.Tab Then

                If String.IsNullOrEmpty(txtIdTipoTR.Text.Trim()) = False Then

                    Dim l As New clsLnTrans_re_tr
                    Dim Obj As New clsBeTrans_re_tr() With {.IdTipoTransaccion = txtIdTipoTR.Text.Trim}
                    l.Obtener(Obj)

                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.IdTipoTransaccion) = False Then

                        txtDescripcionTR.Text = Obj.Descripcion

                    Else

                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show(String.Format("No existe Tipo Transacción con código {0}", txtIdTipoTR.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtDescripcionTR.Text = String.Empty
                        txtIdTipoTR.Focus() : txtIdTipoTR.SelectAll()

                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub txtIdTipoTR_TextChanged(sender As Object, e As EventArgs) Handles txtIdTipoTR.TextChanged

        Try

            If String.IsNullOrEmpty(txtIdTipoTR.Text.Trim()) = False Then

                Dim l As New clsLnTrans_re_tr
                Dim Obj As New clsBeTrans_re_tr() With {.IdTipoTransaccion = txtIdTipoTR.Text.Trim}
                l.Obtener(Obj)

                If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Descripcion) = False Then
                    txtDescripcionTR.Text = Obj.Descripcion
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub lnkTipoT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkTipoT.LinkClicked

        Try

            Dim TR As New frmTipoTransaccion_List() With {.Modo = frmUnidad_MedidaList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                TR.OpcionesMenu = OpcionesMenu
                TR.mnuNuevo.Enabled = OpcionesMenu.Modificar
                TR.mnuActualizar.Enabled = OpcionesMenu.Leer
            End If

            TR.ShowDialog()

            If TR.pObj IsNot Nothing AndAlso String.IsNullOrEmpty(TR.pObj.IdTipoTransaccion) = False Then

                txtIdTipoTR.Text = TR.pObj.IdTipoTransaccion
                txtDescripcionTR.Text = TR.pObj.Descripcion

            End If

            TR.Close()
            TR.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub mnuDiseñoGrafico_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDiseñoGrafico.ItemClick

        Try
            mnuDiseñoGrafico.Enabled = False

            If txtLargo.Value = 0 OrElse txtAncho.Value = 0 OrElse txtAlto.Value = 0 Then
                XtraMessageBox.Show("Falta configurar dimensiones de bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Using GrafBod As New frmDiseñoB() With {.IdEmpresa = pBeBodega.IdEmpresa, .IdBodega = pBeBodega.IdBodega}
                    GrafBod.ShowDialog(Me)
                End Using
            End If
            mnuDiseñoGrafico.Enabled = True
        Catch ex As Exception
            mnuDiseñoGrafico.Enabled = True
        End Try

    End Sub

    Private Sub tabDatos_Paint(sender As Object, e As PaintEventArgs) Handles tabDatos.Paint

    End Sub
    Private Sub cmbAreasR_EditValueChanged(sender As Object, e As EventArgs) Handles cmbAreasR.EditValueChanged

        Try

            If Not IMS.Listar_Sectores_By_Area(cmbSector, cmbAreasR.EditValue, pBeBodega.IdBodega) Then '#CKFK 20210113 Reemplacé AP.IdBodega para que me muestre la información de otras bodegas
                cmbSector.Properties.DataSource = Nothing
            End If
            If Not IMS.Listar_Sectores_By_Area(cmbSectorR, cmbAreasR.EditValue, pBeBodega.IdBodega) Then '#CKFK 20210113 Reemplacé AP.IdBodega para que me muestre la información de otras bodegas
                cmbSectorR.Properties.DataSource = Nothing
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
    Private Sub cmbSector_EditValueChanged(sender As Object, e As EventArgs) Handles cmbSector.EditValueChanged
        Try
            Limpiar_Campos_Bodega_Tramo()
            Cargar_Bodega_Tramo(cmbSector.EditValue)
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub
    Private Sub cmbArea_EditValueChanged(sender As Object, e As EventArgs) Handles cmbArea.EditValueChanged
        Cargar_Bodega_Sector(cmbArea.EditValue)
    End Sub
    Private Sub cmbSectorR_EditValueChanged(sender As Object, e As EventArgs) Handles cmbSectorR.EditValueChanged
        Try
            If Not IMS.Listar_TramosBySector(cmbTramo, cmbSectorR.EditValue, pBeBodega.IdBodega) Then
                cmbTramo.Properties.DataSource = Nothing
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
    Private Sub cmbAreaUbic_EditValueChanged(sender As Object, e As EventArgs) Handles cmbAreaUbic.EditValueChanged
        Try
            If Not IMS.Listar_Sectores_By_Area(cmbSectorR, cmbAreaUbic.EditValue, pBeBodega.IdBodega) Then '#CKFK 20210113 Reemplacé AP.IdBodega para que me muestre la información de otras bodegas
                cmbSectorR.Properties.DataSource = Nothing
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

    Private Sub cmbTramo_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTramo.EditValueChanged

        Try

            Cargar_Bodega_Ubicacion(cmbTramo.EditValue)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbEmpresa_EditValueChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.EditValueChanged
        Dim ObjE As New clsBeEmpresa

        If cmbEmpresa.ItemIndex > -1 Then
            ObjE.IdEmpresa = cmbEmpresa.EditValue
            clsLnEmpresa.Obtener(ObjE)
        End If

        Try

            If ObjE.IdEmpresa > 0 Then

                Select Case Modo

                    Case TipoTrans.Nuevo

                        If ObjE.codigo_automatico Then
                            txtCodigo.Text = Correlativo()
                            txtCodigo.ReadOnly = True
                        Else
                            txtCodigo.ReadOnly = False
                        End If

                    Case TipoTrans.Editar

                        If ObjE.codigo_automatico Then
                            txtCodigo.ReadOnly = True
                        Else
                            txtCodigo.ReadOnly = False
                        End If

                End Select

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub chkEsBodegaVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkEsBodegaVirtual.CheckedChanged
        txtCodigoBarra2ubicacion.ReadOnly = Not chkEsBodegaVirtual.Checked
    End Sub

    Private Sub tsmnuGuardarSector_Click(sender As Object, e As EventArgs) Handles tsmnuGuardarSector.Click

        Try

            If (pNuevoSector) Then

                If Datos_CorrectosSector() Then

                    If (Guardar_Bodega_Sector()) Then
                        Limpiar_Campos_Bodega_Sector()
                        Cargar_Bodega_Sector(cmbArea.EditValue)
                        XtraMessageBox.Show("Se insertó el registro", Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information)
                        Limpiar_Campos_Bodega_Sector()
                        Cargar_Bodega_Sector(cmbArea.EditValue)
                    End If

                End If

            Else

                If (Actualizar_Bodega_Sector()) Then
                    XtraMessageBox.Show("Se actualizó el registro", Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information)
                    Limpiar_Campos_Bodega_Sector()
                    Cargar_Bodega_Sector(cmbArea.EditValue)
                End If

            End If

            Llena_Bodega()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub tsmnuNuevoSector_Click(sender As Object, e As EventArgs) Handles tsmnuNuevoSector.Click
        Limpiar_Campos_Bodega_Sector()
        txtCodigoSector.Focus()
        pNuevoSector = True
    End Sub

    Private Sub tsmnuGuardarTramo_Click(sender As Object, e As EventArgs) Handles tsmnuGuardarTramo.Click

        Try

            If (pNuevoTramo) Then
                If Datos_CorrectosTramo() Then
                    If (Guardar_Bodega_Tramo()) Then
                        Limpiar_Campos_Bodega_Tramo()
                        Cargar_Bodega_Tramo(cmbSector.EditValue)
                        XtraMessageBox.Show("Se insertó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else
                If (Actualizar_Bodega_Tramo()) Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Limpiar_Campos_Bodega_Tramo()
                    Cargar_Bodega_Tramo(cmbSector.EditValue)
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

    Private Sub tsmnuNuevoTramo_Click(sender As Object, e As EventArgs) Handles tsmnuNuevoTramo.Click
        Limpiar_Campos_Bodega_Tramo()
        pNuevoTramo = True
        txtCodigoTramo.Focus()
    End Sub

    Private Sub tsmnuGuardarUbicacion_Click(sender As Object, e As EventArgs) Handles tsmnuGuardarUbicacion.Click

        Try

            If (pNuevaUbicacion) Then
                If Datos_Correctos_Ubicacion() Then
                    If Guardar_Bodega_Ubicacion() Then
                        Limpiar_Campos_Bodega_Ubicacion()
                        Cargar_Bodega_Ubicacion(cmbTramo.EditValue)
                        XtraMessageBox.Show("Se insertó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else
                If (Actualizar_Bodega_Ubicacion()) Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Limpiar_Campos_Bodega_Ubicacion()
                    Cargar_Bodega_Ubicacion(cmbTramo.EditValue)
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

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs) Handles tsmnuNuevaUbicacion.Click

        Try

            pNuevaUbicacion = True

            Limpiar_Campos_Bodega_Ubicacion()

            pBeBodegaUbicacion.IdUbicacion = clsLnBodega_ubicacion.MaxID(pBeBodega.IdBodega)

            txtCodigoBarraUbicacion.Text = Mid(100000 + pBeBodegaUbicacion.IdUbicacion, 2, 5)

            txtCodigoBarraUbicacion.Focus()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub lblDañadoPicking_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblDañadoPicking.LinkClicked
        Try
            Dim MotivoUbic As New frmMotivo_UbicacionList() With {.Modo = frmMotivo_UbicacionList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                MotivoUbic.OpcionesMenu = OpcionesMenu
                MotivoUbic.mnuNuevo.Enabled = OpcionesMenu.Modificar
                MotivoUbic.mnuActualizar.Enabled = OpcionesMenu.Leer
            End If

            MotivoUbic.ShowDialog()
            If MotivoUbic.gBeMotivoUbicacion IsNot Nothing AndAlso MotivoUbic.gBeMotivoUbicacion.IdMotivoUbicacion <> 0 Then
                txtidmotivoubicaciondañadopicking.Text = MotivoUbic.gBeMotivoUbicacion.IdMotivoUbicacion
                txtMotivoUbicacionDañadoPicking.Text = MotivoUbic.gBeMotivoUbicacion.Nombre
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

    Private Sub txtIdMotivoUbicReabasto_LostFocus(sender As Object, e As EventArgs) Handles txtIdMotivoUbicReabasto.LostFocus

        Try

            If String.IsNullOrEmpty(txtIdMotivoUbicReabasto.Text.Trim()) = False Then

                Dim Obj As New clsBeMotivo_ubicacion
                Obj = clsLnMotivo_ubicacion.GetSingle(txtIdMotivoUbicReabasto.Text.Trim())

                If Obj IsNot Nothing AndAlso Obj.IdMotivoUbicacion > 0 Then

                    txtIdMotivoUbicReabasto.Text = Obj.IdMotivoUbicacion
                    txtMotivoUbicReabasto.Text = Obj.Nombre

                Else

                    SplashScreenManager.CloseForm(False)

                    XtraMessageBox.Show(String.Format("No existe ubicación con código {0}", txtIdMotivoUbicReabasto.Text.Trim()),
                                        Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdMotivoUbicReabasto.Text = String.Empty
                    txtIdMotivoUbicReabasto.Text = String.Empty
                    txtIdMotivoUbicReabasto.Focus()

                End If

            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdMotivoUbicReabasto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdMotivoUbicReabasto.KeyPress
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
            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdMotivoUbicReabasto.Text.Length = 1 Then
                txtIdMotivoUbicReabasto.Text = String.Empty
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

    Private Sub lnkReabasto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkReabasto.LinkClicked

        Try

            Dim MotivoReabasto As New frmMotivo_UbicacionList() With {.Modo = frmMotivo_UbicacionList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                MotivoReabasto.OpcionesMenu = OpcionesMenu
                MotivoReabasto.mnuNuevo.Enabled = OpcionesMenu.Modificar
                MotivoReabasto.mnuActualizar.Enabled = OpcionesMenu.Leer
            End If

            MotivoReabasto.ShowDialog()

            If MotivoReabasto.gBeMotivoUbicacion IsNot Nothing AndAlso MotivoReabasto.gBeMotivoUbicacion.IdMotivoUbicacion <> 0 Then

                txtIdMotivoUbicReabasto.Text = MotivoReabasto.gBeMotivoUbicacion.IdMotivoUbicacion
                txtMotivoUbicReabasto.Text = MotivoReabasto.gBeMotivoUbicacion.Nombre

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try '

    End Sub

    Private Sub txtidmotivoubicaciondañadopicking_LostFocus(sender As Object, e As EventArgs) Handles txtidmotivoubicaciondañadopicking.LostFocus
        Try

            If String.IsNullOrEmpty(txtidmotivoubicaciondañadopicking.Text.Trim()) = False Then

                Dim Obj As New clsBeMotivo_ubicacion
                Obj = clsLnMotivo_ubicacion.GetSingle(txtidmotivoubicaciondañadopicking.Text.Trim())

                If Obj IsNot Nothing AndAlso Obj.IdMotivoUbicacion > 0 Then

                    txtidmotivoubicaciondañadopicking.Text = Obj.IdMotivoUbicacion
                    txtMotivoUbicacionDañadoPicking.Text = Obj.Nombre

                Else

                    XtraMessageBox.Show(String.Format("No existe ubicación con código {0}", txtidmotivoubicaciondañadopicking.Text.Trim()),
                                        Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtidmotivoubicaciondañadopicking.Text = String.Empty
                    txtidmotivoubicaciondañadopicking.Text = String.Empty
                    txtidmotivoubicaciondañadopicking.Focus()

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

    Private Sub txtidmotivoubicaciondañadopicking_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtidmotivoubicaciondañadopicking.KeyPress
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
            If e.KeyChar = Convert.ToChar(8) AndAlso txtidmotivoubicaciondañadopicking.Text.Length = 1 Then
                txtMotivoUbicacionDañadoPicking.Text = String.Empty
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

    Private Sub frmBodega_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Try

            If e.KeyCode = Keys.Escape Then Close()

            If e.Control = True AndAlso e.KeyCode = Keys.I Then

                mnuEditarConnIni.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                If XtraMessageBox.Show("¿Abrir archivo de configuración?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim a As New frmEditorIni
                    a.WindowState = FormWindowState.Maximized
                    a.ShowDialog()
                End If

            ElseIf e.Control AndAlso e.KeyCode = Keys.U Then

                If XtraMessageBox.Show("¿Inserta ubicaciones por defecto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If clsLnBodega.Inserta_Ubicaciones_Por_Defecto(pBeBodega.IdBodega, AP.UsuarioAp.IdUsuario) Then
                        MsgBox("Ubicaciones insertadas", MsgBoxStyle.Information, Text)
                        Cargar_Bodega()
                    End If

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

    Private Sub ControlPanelBodega_TabIndexChanged(sender As Object, e As EventArgs) Handles ControlPanelBodega.TabIndexChanged

        Select Case ControlPanelBodega.SelectedTabPageIndex

            Case 3

                IMS.Listar_Areas_By_Bodega(cmbArea, pBeBodega.IdBodega)

        End Select

    End Sub

    Private Sub ControlPanelBodega_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles ControlPanelBodega.SelectedPageChanged

        Try

            Select Case ControlPanelBodega.SelectedTabPageIndex

                Case 3
                    IMS.Listar_Areas_By_Bodega(cmbArea, pBeBodega.IdBodega)

                Case 4
                    IMS.Listar_Areas_By_Bodega(cmbArea, pBeBodega.IdBodega)

            End Select

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicPrdNE.LinkClicked
        Try
            Dim Ubicacion As New frmBodegaT() With {.Modo = frmBodegaT.pModo.Seleccion}
            Ubicacion.pObjBeB.IdBodega = pBeBodega.IdBodega
            Ubicacion.Nombre_Campo = "ubicacion_ne"
            Ubicacion.ShowDialog()
            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacionPrdNE.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicNE.Text = Ubicacion.pObj.Descripcion
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

    Private Sub txtIdUbicacionPrdNE_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdUbicacionPrdNE.PreviewKeyDown

        Try
            If e.KeyData = Keys.Enter Then
                If String.IsNullOrEmpty(txtIdUbicacionPrdNE.Text.Trim()) = False Then
                    Dim Obj As New clsBeBodega_ubicacion
                    Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacionPrdNE.Text.Trim(), "ubicacion_ne")
                    If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                        txtIdUbicacionPrdNE.Text = Obj.IdUbicacion
                        txtNombreUbicNE.Text = Obj.Descripcion
                    Else

                        XtraMessageBox.Show(String.Format("No existe ubicación de producto ne con código {0}", txtIdUbicacionPrdNE.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        txtIdUbicacionPrdNE.Text = String.Empty
                        txtNombreUbicNE.Text = String.Empty

                        txtIdUbicacionPrdNE.Focus()
                    End If
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

    Private Sub txtIdUbicacionPrdNE_LostFocus(sender As Object, e As EventArgs) Handles txtIdUbicacionPrdNE.LostFocus
        Try

            If String.IsNullOrEmpty(txtIdUbicacionPrdNE.Text.Trim()) = False Then

                Dim BeBodegaUbicación As New clsBeBodega_ubicacion

                BeBodegaUbicación = clsLnBodega_ubicacion.GetSingle(txtIdUbicacionPrdNE.Text.Trim(), pBeBodega.IdBodega, "ubicacion_ne", False) '#CKFK 20210113 Reemplacé AP.IdBodega para que me muestre la información de otras bodegas

                If BeBodegaUbicación IsNot Nothing AndAlso BeBodegaUbicación.IdUbicacion > 0 Then
                    txtIdUbicacionPrdNE.Text = BeBodegaUbicación.IdUbicacion
                    txtNombreUbicNE.Text = BeBodegaUbicación.Descripcion
                Else

                    SplashScreenManager.CloseForm()

                    XtraMessageBox.Show($"No se encontró una ubicación de producto con el código '{txtIdUbicacionPrdNE.Text.Trim()}'.")
                    txtIdUbicacionPrdNE.Text = String.Empty
                    txtNombreUbicNE.Text = String.Empty
                    txtIdUbicacionPrdNE.Focus()

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

    Private Sub mnuEstructuraInicial_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEstructuraInicial.ItemClick

        Try
            mnuEstructuraInicial.Enabled = False

            'GT11012021: si la bodega no tiene largo, ancho o zoom no se puede importar, porque no se pintara correctamente
            If txtLargo.Value = 0 OrElse txtAncho.Value = 0 OrElse txtZoom.Value = 0 Then
                XtraMessageBox.Show("Falta configurar dimensiones de bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If clsLnUsuario.Usuario_Valido(AP.UsuarioAp, pBeBodega.IdBodega) Then

                    AP.IdBodegaAnterior = AP.IdBodega
                    AP.IdBodega = pBeBodega.IdBodega

                    Dim est As New frmEstructuraBod

                    est.IdBodega = pBeBodega.IdBodega
                    est.CodigoBodega = pBeBodega.Codigo
                    est.NombreBodega = pBeBodega.Nombre

                    est.ShowDialog()
                    est.Dispose()

                    AP.IdBodega = AP.IdBodegaAnterior

                Else
                    XtraMessageBox.Show(String.Format("Su usuario {0} no tiene permiso a la Bodega {1}!", AP.UsuarioAp.Nombres, pBeBodega.Nombre), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            mnuEstructuraInicial.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdRefrescar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdRefrescar.ItemClick
        cmdRefrescar.Enabled = False
        Buscar_Registro()
        cmdRefrescar.Enabled = True
    End Sub

    Private Sub mnuParametrosInterface_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuParametrosInterface.ItemClick

        Try

            If Not BeINavConfigEnc Is Nothing Then

                Dim Config As New frmConfiguracion(frmConfiguracion.TipoTrans.Editar)
                Config.BeConfigEnc.Idnavconfigenc = BeINavConfigEnc.Idnavconfigenc
                Config.WindowState = FormWindowState.Maximized
                Config.ShowDialog()
                Config.Dispose()

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

    Private Sub mnuEditarConnIni_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEditarConnIni.ItemClick

        Try

            Dim a As New frmEditorIni
            a.WindowState = FormWindowState.Maximized
            a.ShowDialog()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuUnificarBodegas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuUnificarBodegas.ItemClick


        Try

            Dim lfrmBodega_List As New frmBodega_List
            lfrmBodega_List.Modo = frmBodega_List.pModo.Seleccion
            lfrmBodega_List.IdBodegaAExcluir = pBeBodega.IdBodega

            If lfrmBodega_List.ShowDialog() = DialogResult.OK Then

                If lfrmBodega_List.lBodegasSeleccionadas.Count > 0 Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Unificando bodegas...")

                    clsLnBodega.Unificar_Bodegas(pBeBodega.IdBodega,
                                                 lfrmBodega_List.lBodegasSeleccionadas,
                                                 AP.UsuarioAp.IdUsuario)

                    SplashScreenManager.CloseForm()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub lnkTipoSalida_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkTipoSalida.LinkClicked


        Try

            Dim TR As New frmTipoTransaccionSalida_List() With {.Modo = frmUnidad_MedidaList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                TR.OpcionesMenu = OpcionesMenu
                TR.mnuNuevo.Enabled = OpcionesMenu.Modificar
                TR.mnuActualizar.Enabled = OpcionesMenu.Leer
            End If

            TR.ShowDialog()

            If TR.BePedidoTipo IsNot Nothing AndAlso String.IsNullOrEmpty(TR.BePedidoTipo.IdTipoPedido) = False Then

                txtIdTipoDocumentoSalida.Text = TR.BePedidoTipo.IdTipoPedido
                txtNombreDocumentoSalida.Text = TR.BePedidoTipo.Descripcion

            End If

            TR.Close()
            TR.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub txtIdTipoDocumentoSalida_Validated(sender As Object, e As EventArgs) Handles txtIdTipoDocumentoSalida.Validated

        Try

            Set_IdTipoDocumentoSalida()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Try


            cmdImprimir.Enabled = False
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

            Imprimir_Vista()

            cmdImprimir.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
            printLink.Component = dgridUbicaciones
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Bodega: " & pBeBodega.Codigo + " - " & pBeBodega.Nombre & " Lista de ubicaciones"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub frmBodega_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            If clsLnMenu_rol.Permiso_Funcionalidad("1.2.1.1", AP.IdRol) Then
                cmdHabilitarReemplazo.Enabled = True
            Else
                cmdHabilitarReemplazo.Enabled = False
            End If

            If BeINavConfigEnc IsNot Nothing Then

                If BeINavConfigEnc.Centro_Costo_Erp > 0 AndAlso BeINavConfigEnc.Centro_Costo_Dep_Erp > 0 AndAlso BeINavConfigEnc.Centro_Costo_Dir_Erp > 0 Then
                    gcCentroCosto.Visible = True
                Else
                    gcCentroCosto.Visible = False
                End If

            End If

            Cargar_Smpt()

        Catch ex As Exception

        End Try

    End Sub

    Private _smtpConfig As Smtp_Configuracion
    Private Sub Cargar_Smpt()
        Try
            '#GT18112025; cargar configuracion smtp
            gpSmtp.Visible = True
            _smtpConfig = ConfigManager.Cargar()
            txtServidor.Text = _smtpConfig.Servidor
            txtPuerto.Text = _smtpConfig.Puerto.ToString()
            txtUsuario.Text = _smtpConfig.Usuario
            '' Mostramos la contraseña desencriptada
            txtPassword.Text = CryptoHelper.Desencriptar(_smtpConfig.Password)
            chkSsl.Checked = _smtpConfig.UsarSsl
            'txtRemitente.Text = _smtpConfig.RemitentePorDefecto
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gpSmtp_CustomButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.BaseButtonEventArgs) Handles gpSmtp.CustomButtonClick
        ' Opcional: si tienes más de un botón, puedes distinguirlos por Caption o Tag
        Dim caption = e.Button.Properties.Caption

        If caption = "Limpiar" Then
            ConfigManager.RestablecerAValoresDefault()

            ' Volver a cargar el objeto en memoria
            _smtpConfig = ConfigManager.Cargar()

            ' Actualizar los controles en el formulario
            Cargar_Smpt()

        End If
    End Sub


    Private Sub cmdHabilitarReemplazo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdHabilitarReemplazo.ItemClick

        Try

            If XtraMessageBox.Show("¿Está seguro de habilitar el reemplazo?", "Bodega", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            If Not permiteMenu("1.2.1.1.1") Then
                Return
            End If

            cmdHabilitarReemplazo.Enabled = False

            clsLnBodega.Habilitar_Reemplazo(AP.IdBodega, True)
            Cargar_Bodega()

            cmdHabilitarReemplazo.Enabled = True

        Catch ex As Exception
            cmdHabilitarReemplazo.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdDeshabilitarReemplazo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdDeshabilitarReemplazo.ItemClick

        Try

            If XtraMessageBox.Show("¿Está seguro de deshabilitar el reemplazo?", "Bodega", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            If Not permiteMenu("1.2.1.1.1") Then
                Return
            End If

            cmdHabilitarReemplazo.Enabled = False

            clsLnBodega.Habilitar_Reemplazo(AP.IdBodega, False)
            Cargar_Bodega()

            cmdHabilitarReemplazo.Enabled = True

        Catch ex As Exception
            cmdHabilitarReemplazo.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function permiteMenu(menu As String) As Boolean

        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String

        Try

            ms.IdMenu = menu
            'MsgBox(link.KeyTip)
            clsLnMenu_sistema.GetSingle(ms)

            If (ms.Solicitar_clave_autorizacion) Then

                us.IdUsuario = AP.UsuarioAp.IdUsuario
                clsLnUsuario.GetSingle(us)

                Try

                    clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                    If (clave = "") Then Throw New Exception("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.")

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End Try

                Dim frmlog As New frmAjusteLogin() With {.clave = clave}

                If frmlog.ShowDialog() <> DialogResult.Yes Then
                    frmlog.Dispose() : Return False
                End If

                frmlog.Dispose()

                Return True

            Else
                Return True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

        Dim frmRV As New frmReglaVence
        frmRV.ShowDialog()

    End Sub

    Private Sub lnkUbicacionRecepcionArea_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacionRecepcionArea.LinkClicked
        Try

            If String.IsNullOrEmpty(txtIdArea.EditValue) Then
                Throw New Exception("GT13022024: debe seleccionar un área de la lista Detalle Areas.")
            End If

            Dim Ubicacion As New frmBodegaT() With {.Modo = frmBodegaT.pModo.Seleccion}
            Ubicacion.pObjBeB.IdBodega = pBeBodega.IdBodega
            Ubicacion.Nombre_Campo = "ubicacion_recepcion"
            Ubicacion.IdAreaFiltro = txtIdArea.EditValue

            If OpcionesMenu IsNot Nothing Then
                Ubicacion.OpcionesMenu = OpcionesMenu
                Ubicacion.mmuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Ubicacion.ShowDialog()
            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtUbicacionRecepcionArea.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacionRecepcionArea.Text = Ubicacion.pObj.Descripcion
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

    Private Sub CargaComboEtiquetas()

        Try

            cmbEtiqueta.Properties.DisplayMember = "Nombre"
            cmbEtiqueta.Properties.ValueMember = "IdTipoEtiqueta"
            cmbEtiqueta.Properties.DataSource = clsLnTipo_etiqueta.GetAllForCombo()
            cmbEtiqueta.ItemIndex = -1

            cmbTamañoEtiquetaUbicacionDefecto.Properties.DisplayMember = "Nombre"
            cmbTamañoEtiquetaUbicacionDefecto.Properties.ValueMember = "IdTipoEtiqueta"
            cmbTamañoEtiquetaUbicacionDefecto.Properties.DataSource = clsLnTipo_etiqueta.GetAllForCombo()
            cmbTamañoEtiquetaUbicacionDefecto.ItemIndex = -1

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub ImplementarBarra()

        Try

            Bcc.Text = txtCodigoBarra.Text.Trim()
            Dim symb As New QRCodeGenerator()
            Bcc.Symbology = New QRCodeGenerator()

            ' Adjust the QR barcode's specific properties.
            symb.CompactionMode = QRCodeCompactionMode.AlphaNumeric
            symb.ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H
            symb.Version = QRCodeVersion.AutoVersion

            AddHandler cmbSymbology.EditValueChanged, AddressOf OnBarCodeSymbologyChanged

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub OnBarCodeSymbologyChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim s As BarCodeGeneratorBase

            If cmbEtiqueta.EditValue = 0 AndAlso cmbSymbology.EditValue = 0 Then
                s = BarCodeGeneratorFactory.Create(1)
            Else
                s = BarCodeGeneratorFactory.Create(cmbSymbology.EditValue)
            End If
            Bcc.Symbology = s

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub CargaSimbolosCodigoBarra()

        Try

            cmbSymbology.Properties.DisplayMember = "Nombre"
            cmbSymbology.Properties.ValueMember = "IdSimbologia"
            cmbSymbology.Properties.DataSource = clsLnSimbologias_codigo_barra.GetAllForCombo()
            cmbSymbology.ItemIndex = 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub grdUbicacion_Click(sender As Object, e As EventArgs) Handles grdUbicacion.Click

    End Sub

    Private Sub mnuActualizarIndicesRotacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarIndicesRotacion.ItemClick


        Try

            mnuActualizarIndicesRotacion.Enabled = False
            Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Bodega", .pTipoMantenimiento = "Indices_Rotacion_Bodega"}
            Carga.ShowDialog()
            Carga.Dispose()
            mnuActualizarIndicesRotacion.Enabled = True

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuPlantillaIndicesRotacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPlantillaIndicesRotacion.ItemClick

        Dim vRutaArchivo As String = CurDir() & "\Mantenimientos\plantillas\WMS_plantilla_Actualizacion_IndiceRotacionUbicacion.xlsx"

        Try
            If File.Exists(vRutaArchivo) Then
                ' Crear un nuevo SaveFileDialog
                Using saveDialog As New SaveFileDialog()
                    saveDialog.Title = "Guardar plantilla de importación de productos"
                    saveDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
                    saveDialog.FileName = "WMS_plantilla_Importacion_Productos.xlsx"
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

    Private Sub cmdRutaCDN_Click(sender As Object, e As EventArgs) Handles cmdRutaCDN.Click
        Using folderDialog As New FolderBrowserDialog()
            folderDialog.Description = "Seleccione la ruta de red de imágenes"
            folderDialog.RootFolder = Environment.SpecialFolder.NetworkShortcuts

            If folderDialog.ShowDialog() = DialogResult.OK Then
                txtRutaCDN.Text = folderDialog.SelectedPath
            End If
        End Using
    End Sub

    Private Sub Carga_Centro_Costo_Dir_Depto_ERP()

        Try

            cmbCentroCostoERP.Properties.DisplayMember = "Codigo"
            cmbCentroCostoERP.Properties.ValueMember = "IdCentroCosto"
            cmbCentroCostoERP.Properties.DataSource = clsLnCentro_costo.GetAllForCombo(FiltroCentroCosto)
            cmbCentroCostoERP.ItemIndex = 0

            cmbCentroCostoDepERP.Properties.DisplayMember = "Codigo"
            cmbCentroCostoDepERP.Properties.ValueMember = "IdCentroCosto"
            cmbCentroCostoDepERP.Properties.DataSource = clsLnCentro_costo.GetAllForCombo(FiltroCentroCostoDepERP)
            cmbCentroCostoDepERP.ItemIndex = 0

            cmbCentroCostoDirERP.Properties.DisplayMember = "Codigo"
            cmbCentroCostoDirERP.Properties.ValueMember = "IdCentroCosto"
            cmbCentroCostoDirERP.Properties.DataSource = clsLnCentro_costo.GetAllForCombo(FiltroCentroCostoDirERP)
            cmbCentroCostoDirERP.ItemIndex = 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    '#MA20260225 Estado defecto rack
    Private Sub CargarComboEstadosRack()
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Nombre", GetType(String))

            dt.Rows.Add(0, "Seleccionar")

            Dim listaEstados As List(Of clsBeProducto_estado) = clsLnProducto_estado.GetAll()

            For Each est In listaEstados
                If est IsNot Nothing Then
                    dt.Rows.Add(est.IdEstado, est.Nombre)
                End If
            Next

            With cmbEstadoDefectoRack.Properties
                .DataSource = dt
                .DisplayMember = "Nombre"
                .ValueMember = "ID"
                .PopulateColumns()
            End With

            cmbEstadoDefectoRack.EditValue = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class