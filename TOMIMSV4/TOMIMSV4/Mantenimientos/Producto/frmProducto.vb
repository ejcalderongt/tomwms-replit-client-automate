Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.Data.SummaryItemType
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraPrinting.BarCode.Native
Imports DevExpress.XtraSplashScreen

Public Class frmProducto

    Private pTexto As New TextBox
    Private pNumero As New NumericUpDown
    Private pFecha As New DateTimePicker
    Private pLogico As New CheckBox
    Public Obj As clsBeTipo_etiqueta
    Public listaStock As New List(Of clsBeVW_stock_res)
    Private pbeProductoKitHijo As New clsBeProducto

    ' Producto
    Private pListObjTP As New List(Of clsTabla)
    ' Producto Parametros
    Private pListObjTPP As New List(Of clsTabla)
    ' Producto Codigo Barra
    Private pListObjTPC As New List(Of clsTabla)
    ' Producto Presentación
    Private pListObjTPR As New List(Of clsTabla)

    Private IngresoPorGridAParametro As Boolean = False

    Private pBeProductoParametroList As New List(Of clsBeProducto_parametros)
    Private pBeProductoCodigosBarraList As New List(Of clsBeProducto_codigos_barra)
    Private pBeProductoPresentacionList As New List(Of clsBeProducto_Presentacion)
    Private pBeProductoSustitutoList As New List(Of clsBeProducto_sustituto)
    Private pBeproductoRellenadoList As New List(Of clsBeProducto_rellenado)
    Private pBePresentacionTarimaList As New List(Of clsBeProducto_presentacion_tarima)
    Private pBeProductoPresConvList As New List(Of clsBeProducto_presentaciones_conversiones)
    Private pBeProductoKitList As New List(Of clsBeProducto_kit_composicion)

    Private DT As DataTable

    Public pListObjPB As List(Of clsBeProducto_bodega)
    Public ObjTE As List(Of clsBeTipo_etiqueta)
    Public pIdProducto As Integer
    Public pBeProducto As New clsBeProducto
    Public Delegate Sub Listar()
    Public Property InvokeListarProductos As Listar

    Public Property CodigoNuevoProducto As String = ""
    Public Property IdPropietarioNuevo As Integer = 0
    Public Property IdBodegaNuevoProducto As Integer = 0
    Public Property IdProductoBodegaReturn As Integer = 0

    Public Property lBodegas As New List(Of clsBeBodega)

    Private pProducto As New clsBeProducto

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
        Consulta = 3
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

    Private Sub Lista_Bodegas()

        Try

            dgridProductoBodega.BeginUpdate()

            If DT.Rows.Count > 0 Then

                Dim vIdBodega As Integer = 0

                For i As Integer = 0 To DT.Rows.Count - 1

                    vIdBodega = DT(i)(0)
                    Dim lRow As DataRow = DsProducto.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)

                    If Modo = TipoTrans.Nuevo Then
                        lRow.Item("Selección") = True
                    End If

                    If Modo = TipoTrans.Editar Then

                        If pListObjPB IsNot Nothing AndAlso pListObjPB.Count > 0 Then

                            For Each Obj As clsBeProducto_bodega In pListObjPB

                                If Obj.IdBodega = CInt(DT(i)(0)) AndAlso Obj.Activo Then
                                    lRow.Item("Selección") = True
                                    lRow.Item("IdProductoBodega") = Obj.IdProductoBodega
                                End If

                                lRow.Item("IdInterno") = Obj.IdProductoBodega

                            Next

                        End If

                    End If

                    DsProducto.Data.AddDataRow(lRow)

                Next

            End If

            dgridProductoBodega.EndUpdate()
            dgridProductoBodega.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GrdProductoBodega.Columns("Selección").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = GrdProductoBodega.GetFocusedRow
                Dim lIndex As Integer = -1

                If pListObjPB IsNot Nothing AndAlso pListObjPB.Count > 0 Then
                    lIndex = pListObjPB.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                  And b.IdProducto = pBeProducto.IdProducto)
                End If

                If lIndex > -1 Then

                    If ritem.Checked Then
                        pListObjPB(lIndex).Activo = True
                    Else
                        pListObjPB(lIndex).Activo = False
                    End If

                    pListObjPB(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pListObjPB(lIndex).Fec_mod = Now

                Else

                    pListObjPB = New List(Of clsBeProducto_bodega)

                    Dim Obj As New clsBeProducto_bodega() With {.IdBodega = Dr.Item("IdBodega"), .IdProducto = pBeProducto.IdProducto, .User_agr = AP.UsuarioAp.IdUsuario, .Fec_agr = Now, .User_mod = AP.UsuarioAp.IdUsuario, .Fec_mod = Now, .Activo = True}
                    pListObjPB.Add(Obj)

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

    Private Sub ValidaBodegas()

        Try

            DsProducto.Clear()
            DT = IMS.Listar_Bodegas()

            If pBeProducto.IdProducto <> 0 Then
                pListObjPB = New List(Of clsBeProducto_bodega)
                pListObjPB = clsLnProducto.Get_All_Bodegas_By_IdProducto(pBeProducto.IdProducto)
            End If

            Lista_Bodegas()

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

    Private Sub CargaComboEtiquetas()

        Try

            cmbEtiqueta.Properties.DisplayMember = "Nombre"
            cmbEtiqueta.Properties.ValueMember = "IdTipoEtiqueta"
            cmbEtiqueta.Properties.DataSource = clsLnTipo_etiqueta.GetAllForCombo()
            cmbEtiqueta.ItemIndex = 0

            cmbEtiquetaPresentacion.Properties.DisplayMember = "Nombre"
            cmbEtiquetaPresentacion.Properties.ValueMember = "IdTipoEtiqueta"
            cmbEtiquetaPresentacion.Properties.DataSource = clsLnTipo_etiqueta.GetAllForCombo()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Cargar_Producto()

        Try

            lblC.Text = pBeProducto.IdProducto
            lcmbPropietario.EditValue = pBeProducto.Propietario.IdPropietario

            Dim vCantMovimientos As Integer = clsLnTrans_movimientos.Get_Cantidad_Movimientos_By_IdProducto(pBeProducto.IdProducto)
            lcmbPropietario.Enabled = (vCantMovimientos = 0)

            mnuEliminarProducto.Enabled = (vCantMovimientos = 0) AndAlso Not (pBeProducto.Activo)
            mnuDesactivar.Enabled = (pBeProducto.Activo)

            txtCodigoBarra.Text = pBeProducto.Codigo_barra

            If pBeProducto.IdTipoRotacion <> 0 Then
                cmbTipoRotacion.EditValue = pBeProducto.IdTipoRotacion
            Else
                cmbTipoRotacion.ItemIndex = -1
            End If

            If pBeProducto.IdCamara <> 0 Then
                cmbCamara.EditValue = pBeProducto.IdCamara
            Else
                cmbCamara.ItemIndex = -1
            End If

            If pBeProducto.IdIndiceRotacion <> 0 Then
                cmbIndiceRotacion.EditValue = pBeProducto.IdIndiceRotacion
            Else
                cmbIndiceRotacion.ItemIndex = -1
            End If

            If pBeProducto.IdPerfilSerializado <> 0 Then
                cmbPerfilSerializado.EditValue = pBeProducto.IdPerfilSerializado
            Else
                cmbPerfilSerializado.ItemIndex = -1
            End If

            If pBeProducto.IdSimbologia <> 0 Then
                cmbSymbology.EditValue = pBeProducto.IdSimbologia
            Else
                cmbSymbology.ItemIndex = -1
            End If

            If pBeProducto.Arancel.IdArancel <> 0 Then
                cmbArancel.EditValue = pBeProducto.Arancel.IdArancel
            Else
                cmbArancel.ItemIndex = -1
            End If

            If pBeProducto.Clasificacion.IdClasificacion <> 0 Then
                lcmbClasificacion.EditValue = pBeProducto.Clasificacion.IdClasificacion
            Else
                lcmbClasificacion.ItemIndex = -1
                lcmbClasificacion.EditValue = Nothing
            End If

            If pBeProducto.Familia.IdFamilia <> 0 Then
                lcmbFamilia.EditValue = pBeProducto.Familia.IdFamilia
            Else
                lcmbFamilia.ItemIndex = -1
                lcmbFamilia.EditValue = Nothing
            End If

            If pBeProducto.Marca.IdMarca <> 0 Then
                lcmbMarca.EditValue = pBeProducto.Marca.IdMarca
            Else
                lcmbMarca.EditValue = Nothing
            End If

            If pBeProducto.TipoProducto.IdTipoProducto <> 0 Then
                txtIdTipoProducto.EditValue = pBeProducto.TipoProducto.IdTipoProducto
            Else
                txtIdTipoProducto.EditValue = Nothing
            End If

            If pBeProducto.UnidadMedida.IdUnidadMedida <> 0 Then

                lcmbUnidadMedidaBasica.EditValue = pBeProducto.UnidadMedida.IdUnidadMedida

                txtIdUnidadMedidaBasicaRellenado.Text = pBeProducto.UnidadMedida.IdUnidadMedida
                txtNombreUMBasRellenado.Text = pBeProducto.UnidadMedida.Nombre

                txtIdUMBasReabastecerCon.Text = pBeProducto.UnidadMedida.IdUnidadMedida
                txtNombreUMBasReabastecerCon.Text = pBeProducto.UnidadMedida.Nombre
            Else
                lcmbUnidadMedidaBasica.ItemIndex = -1
                lcmbUnidadMedidaBasica.EditValue = Nothing
            End If

            '#EJC20220327:
            If pBeProducto.IdUnidadMedidaCobro <> 0 Then
                lcmbUnidadMedidaCobro.EditValue = pBeProducto.IdUnidadMedidaCobro
            Else
                lcmbUnidadMedidaCobro.EditValue = Nothing
            End If

            txtCodigo.Text = pBeProducto.Codigo
            txtNombre.Text = pBeProducto.Nombre
            txtTolerancia.Text = pBeProducto.Tolerancia
            txtExitenciaMinima.Text = pBeProducto.Existencia_min
            txtExistenciaMaxima.Text = pBeProducto.Existencia_max

            txtCosto.Text = pBeProducto.Costo
            txtPrecio.Text = pBeProducto.Precio

            chkSerializado.Checked = pBeProducto.Serializado

            'GT 01092021: Si control lote es falso, el genera lote debe ser igual
            '#EJC20220327: Nooooo!
            If pBeProducto.Control_lote Then
                chkControlLote.Checked = pBeProducto.Control_lote
                chkGeneraLote.Checked = pBeProducto.Genera_lote
            Else
                chkControlLote.Checked = False
                chkGeneraLote.Checked = False
            End If

            '#EJC20210426: Por cealsa, este parámetro considera si utiliza LP para la UMBAS (Sin pres)
            chkGeneraLicAutoP.Checked = pBeProducto.Genera_lp

            chkControlVencimiento.Checked = pBeProducto.Control_vencimiento

            chkCapturarPeso.Checked = pBeProducto.Control_peso
            txtPesoReferencia.Text = pBeProducto.Peso_referencia
            txtPesoTolerancia.Text = pBeProducto.Peso_tolerancia

            chkCapturaTemperatura.Checked = pBeProducto.Temperatura_recepcion
            txtTemperaturaReferencia.Text = pBeProducto.Temperatura_referencia
            chkCapturaTemperatura.Checked = pBeProducto.Temperatura_despacho
            txtTemperaturaTolerancia.Text = pBeProducto.Temperatura_tolerancia

            txtCicloVida.Value = pBeProducto.Ciclo_vida
            chkControlVencimiento.Checked = pBeProducto.Control_vencimiento
            chkEsMateriaPrima.Checked = pBeProducto.Materia_prima
            chkEsKit.Checked = pBeProducto.Kit

            User_agrTextEdit.Text = pBeProducto.User_agr
            Fec_agrDateEdit.Text = pBeProducto.Fec_agr
            User_modTextEdit.Text = pBeProducto.User_mod
            Fec_modDateEdit.Text = pBeProducto.Fec_mod

            If pBeProducto.Imagen IsNot Nothing Then
                picFoto.Image = ByteArrayToImage(pBeProducto.Imagen)
            End If

            chkCapturarAniada.Checked = pBeProducto.Capturar_aniada
            chkFechaManufactura.Checked = pBeProducto.Fechamanufactura
            chkEsHW.Checked = pBeProducto.Es_hardware
            chkCapturaArancel.Checked = pBeProducto.Captura_arancel

            txtNoSerie.Text = pBeProducto.Noserie
            txtNoParte.Text = pBeProducto.Noparte

            picFoto.SizeMode = PictureBoxSizeMode.StretchImage

            chkActivo.Checked = pBeProducto.Activo

            cmbPerfilSerializado.Enabled = pBeProducto.Serializado
            txtNoSerie.Enabled = pBeProducto.Serializado

            txtNoParte.Enabled = pBeProducto.Es_hardware

            'Dimensiones
            txtLargoUB.Value = pBeProducto.Largo
            txtAltoUB.Value = pBeProducto.Alto
            txtAnchoUB.Value = pBeProducto.Ancho

            'Tipo Etiqueta
            If pBeProducto.IdTipoEtiqueta <> 0 Then
                cmbEtiqueta.EditValue = pBeProducto.IdTipoEtiqueta
            Else
                cmbEtiqueta.ItemIndex = -1
            End If

            '#EJC20220318: Cargar imágenes de producto.
            pListProdImgs = clsLnProducto_imagen.Get_All_By_IdProducto(pBeProducto.IdProducto)

            '#GT02092022: set del parametro A y B si el producto lo trae al cargar

            If pBeProducto.ParametroA.IdProductoParametroA <> 0 Then
                lcmbParametroA.EditValue = pBeProducto.IdProductoParametroA
            End If

            If pBeProducto.ParametroB.IdProductoParametroB <> 0 Then
                lcmbParametroB.EditValue = pBeProducto.IdProductoParametroB
            End If

            '#EJC20240328: IdTipoManufactura al cargar producto.
            cmbTipoManufactura.EditValue = pBeProducto.IdTipoManufactura

            '#AT20250430 Cargar margen del producto
            txtMargen.Text = pBeProducto.Margen_Impresion

            Cargar_Imagenes()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If lcmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf cmbTipoRotacion.EditValue = -1 Then
                XtraMessageBox.Show("Seleccione un tipo de rotación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf cmbEtiqueta.EditValue = -1 Then
                XtraMessageBox.Show("Seleccione un tipo de etiqueta para impresión.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrEmpty(lcmbUnidadMedidaBasica.EditValue) Then
                XtraMessageBox.Show("Seleccione Unidad de Medida.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf chkCapturaArancel.Checked AndAlso cmbArancel.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Arancel.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbArancel.Focus()
            ElseIf chkEsHW.Checked AndAlso String.IsNullOrEmpty(txtNoParte.Text) Then
                XtraMessageBox.Show("Ingrese Número de Parte.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNoParte.Focus()
            ElseIf chkSerializado.Checked AndAlso cmbPerfilSerializado.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Perfil Serializado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNoParte.Focus()
            ElseIf chkSerializado.Checked AndAlso cmbPerfilSerializado.ItemIndex = 2 AndAlso String.IsNullOrEmpty(txtNoSerie.Text) Then
                XtraMessageBox.Show("Ingrese Número de Serie.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNoSerie.Focus()
            ElseIf String.IsNullOrEmpty(txtCodigoBarra.Text.Trim) = False Then

                Dim lIndex As Integer = -1

                lIndex = pBeProductoCodigosBarraList.FindIndex(Function(b) b.Codigo_barra = txtCodigoBarra.Text.Trim())

                If lIndex > -1 Then
                    XtraMessageBox.Show(String.Format("El código de barra {0} ya existe en Producto Código de Barra.", txtCodigoBarra.Text.Trim), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    lIndex = pBeProductoPresentacionList.FindIndex(Function(c) c.Codigo_barra = txtCodigoBarra.Text.Trim() And c.IsNew = True)
                    If lIndex > -1 Then
                        XtraMessageBox.Show(String.Format("El código de barra {0} ya existe en Presentaciones.", txtCodigoBarra.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Datos_Correctos = True
                    End If
                End If
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

#Region " Parámetro "

    Private Sub Cargar_Parametros()

        Try

            DgridParametros.DataSource = Nothing

            If pBeProductoParametroList.Count > 0 Then

                Dim DT As New DataTable("ProductoParametro")
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("Nombre_Parámetro", GetType(String))
                DT.Columns.Add("Tipo_Parámetro", GetType(String))
                DT.Columns.Add("Valor", GetType(Object))

                For Each Obj As clsBeProducto_parametros In pBeProductoParametroList.FindAll(Function(b) b.Activo = chkActivoParametro.Checked)

                    Dim lRow As DataRow = DT.NewRow()
                    lRow(0) = Obj.IdProductoParametro
                    lRow(1) = Obj.TipoParametro.Descripcion
                    lRow(2) = Obj.TipoParametro.Tipo

                    Select Case Obj.TipoParametro.Tipo

                        Case "Lógico"

                            lRow(3) = Obj.Valor_logico

                        Case "Texto"

                            lRow(3) = Obj.Valor_texto

                        Case "Fecha"

                            lRow(3) = Obj.Valor_fecha

                        Case "Numérico", "Númerico"

                            lRow(3) = Obj.Valor_numerico

                        Case Else
                            Exit Select

                    End Select

                    DT.Rows.Add(lRow)

                Next

                DgridParametros.DataSource = DT

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

#End Region

#Region " Código de barra "

    Private Function ValidaCodigoBarra() As Boolean

        ValidaCodigoBarra = False

        Try
            If cmbProveedor.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccine Proveedor.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbProveedor.Focus()
            ElseIf String.IsNullOrEmpty(txtCodigoBarraL.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código de Barra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoBarra.Focus()
            Else
                ValidaCodigoBarra = True
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

    Private Sub Cargar_Codigos_Barra()

        Try

            GrdCodigoBarra.DataSource = Nothing

            If pBeProductoCodigosBarraList.Count > 0 Then

                Dim pBeTalla As New clsBeTalla()
                Dim pBeColor As New clsBeColor()
                Dim pCodigoTalla As String = ""
                Dim pDescripcionTalla As String = ""
                Dim pCodigoColor As String = ""
                Dim pDescripcionColor As String = ""


                Dim DT As New DataTable("CodigoBarra")
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("IdProveedor", GetType(Integer))
                DT.Columns.Add("Proveedor", GetType(String))
                DT.Columns.Add("Código de Barra", GetType(String))
                '#GT05122025: campos para talla color
                DT.Columns.Add("Código talla", GetType(String))
                DT.Columns.Add("Descripción talla", GetType(String))
                DT.Columns.Add("Código color", GetType(String))
                DT.Columns.Add("Descripción color", GetType(String))

                For Each BeCodigoBarra As clsBeProducto_codigos_barra In pBeProductoCodigosBarraList.FindAll(Function(b) b.Activo = chkActivoCB.Checked)

                    If BeCodigoBarra.IdTalla Then
                        pBeTalla = clsLnTalla.GetSingle_By_IdTalla(BeCodigoBarra.IdTalla)
                        If pBeTalla IsNot Nothing Then
                            pCodigoTalla = pBeTalla.Codigo
                            pDescripcionTalla = pBeTalla.Descripcion
                        End If
                    End If

                    If BeCodigoBarra.IdColor Then
                        pBeColor = clsLnColor.GetSingle_By_IdColor(BeCodigoBarra.IdColor)
                        If pBeColor IsNot Nothing Then
                            pCodigoColor = pBeColor.Codigo
                            pDescripcionColor = pBeColor.Nombre
                        End If
                    End If


                    Dim lRow As DataRow = DT.NewRow()
                    lRow(0) = BeCodigoBarra.IdProductoCodigoBarra
                    lRow(1) = BeCodigoBarra.IdProveedor
                    lRow(2) = BeCodigoBarra.Proveedor.Nombre
                    lRow(3) = BeCodigoBarra.Codigo_barra
                    '#GT05122025: campos para talla color
                    lRow(4) = pCodigoTalla
                    lRow(5) = pDescripcionTalla
                    lRow(6) = pCodigoColor
                    lRow(7) = pDescripcionColor

                    DT.Rows.Add(lRow)
                Next

                GrdCodigoBarra.DataSource = DT
                GridViewCB.Columns(1).Visible = False

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

    Private Sub cmdNewC_Click(sender As Object, e As EventArgs) Handles cmdNewC.Click
        Listar_Proveedor(cmbProveedor)
        cmdSaveC.Tag = Nothing
        cmbProveedor.ItemIndex = 0
        txtCodigoBarraL.Text = String.Empty
        txtCodigoBarraL.Focus()

    End Sub

    Private Sub cmdSaveC_Click(sender As Object, e As EventArgs) Handles cmdSaveC.Click

        Cursor = Cursors.WaitCursor

        If Not ValidaCodigoBarra() Then
            Cursor = Cursors.Default
            Return
        End If

        Try

            Dim pIndex As Integer = -1

            pIndex = pBeProductoCodigosBarraList.FindIndex(Function(b) b.IdProductoCodigoBarra = CInt(cmdSaveC.Tag))

            If pIndex > -1 Then

                pBeProductoCodigosBarraList(pIndex).IdProducto = pBeProducto.IdProducto
                pBeProductoCodigosBarraList(pIndex).IdProveedor = cmbProveedor.EditValue
                pBeProductoCodigosBarraList(pIndex).Proveedor.Nombre = cmbProveedor.Text
                pBeProductoCodigosBarraList(pIndex).Codigo_barra = txtCodigoBarraL.Text.Trim()

                If pBeProductoCodigosBarraList(pIndex).Codigo_barra.Count > pListObjTPC.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud Then
                    XtraMessageBox.Show(String.Format("El código de barra debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Focus()
                    txtCodigoBarraL.Focus()
                    Return
                End If

                pBeProductoCodigosBarraList(pIndex).Activo = chkActivarCB.Checked
                pBeProductoCodigosBarraList(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                pBeProductoCodigosBarraList(pIndex).Fec_mod = Now

            Else

                Dim ObjPCB As New clsBeProducto_codigos_barra

                If pBeProductoCodigosBarraList IsNot Nothing AndAlso pBeProductoCodigosBarraList.Count > 0 Then
                    ObjPCB.IdProductoCodigoBarra = pBeProductoCodigosBarraList.Max(Function(b) b.IdProductoCodigoBarra) + 1
                Else
                    ObjPCB.IdProductoCodigoBarra = 1
                End If

                Dim lIndexC As Integer = -1
                lIndexC = pBeProductoCodigosBarraList.FindIndex(Function(b) b.Codigo_barra = txtCodigoBarraL.Text.Trim)
                If lIndexC > -1 Then
                    Throw New Exception(String.Format("El código de barra {0} ya existe.", txtCodigoBarraL.Text.Trim()))
                ElseIf txtCodigoBarraL.Text.Trim = txtCodigoBarra.Text.Trim Then
                    Throw New Exception(String.Format("El código de barra {0} ya existe en Producto.", txtCodigoBarraL.Text.Trim()))
                Else
                    lIndexC = pBeProductoPresentacionList.FindIndex(Function(c) c.Codigo_barra = txtCodigoBarraL.Text.Trim)
                    If lIndexC > -1 Then
                        Throw New Exception(String.Format("El código de barra {0} ya existe en Presentaciones.", txtCodigoBarraL.Text.Trim()))
                    End If
                End If

                ObjPCB.IdProducto = pBeProducto.IdProducto
                ObjPCB.IdProveedor = cmbProveedor.EditValue
                ObjPCB.Proveedor = New clsBeProveedor
                ObjPCB.Proveedor.Nombre = cmbProveedor.Text
                ObjPCB.Codigo_barra = txtCodigoBarraL.Text.Trim()

                If ObjPCB.Codigo_barra.Count > pListObjTPC.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud Then
                    XtraMessageBox.Show(String.Format("El código de barra debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Focus()
                    txtCodigoBarraL.Focus()
                    Return
                End If

                ObjPCB.Activo = True
                ObjPCB.User_agr = AP.UsuarioAp.IdUsuario
                ObjPCB.Fec_agr = Now
                ObjPCB.User_mod = AP.UsuarioAp.IdUsuario
                ObjPCB.Fec_mod = Now
                ObjPCB.IsNew = True

                pBeProductoCodigosBarraList.Add(ObjPCB)

            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Cursor = Cursors.Default
            cmdSaveC.Tag = Nothing
            txtCodigoBarraL.Text = String.Empty
            Cargar_Codigos_Barra()
        End Try

    End Sub

#End Region

#Region " Producto Bodega "

    'Private Function ActualizaBodega() As Boolean

    '    Try

    '        If clsLnProductoBodega.ActualizaDatos(pListObjPB.ToArray()) Then
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex1 As FaultException
    '        MsgBox(ex1.Message, MsgBoxStyle.Information, Text)
    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try
    'End Function

#End Region

#Region " Presentación "

    Private Sub LimpiarPresentacion()

        cmdSavePR.Tag = Nothing
        txtNombrePresentacion.Text = String.Empty
        txtCodigoBarraPresentacion.Text = String.Empty
        txtFactor.Value = 1
        txtPeso.Value = 0
        txtAlto.Value = 0
        txtLargo.Value = 0
        txtAncho.Value = 0
        txtMinimoExistencia.Value = 0
        txtMaximoExistencia.Value = 0
        txtMinimoPeso.Value = 0
        txtMaximoPeso.Value = 0
        txtInfo.Text = String.Empty
        chkImprimeBarra.Checked = True
        ChkEsPallet.Checked = False
        chkGeneraLicAutoP.Checked = False
        chkPermitirPaletizar.Checked = True
        txtCajasPorCama.Value = 0
        txtCamasPorTarima.Value = 0
    End Sub

    Private Function Datos_Correctos_Presentacion() As Boolean

        Datos_Correctos_Presentacion = False

        Try

            If String.IsNullOrEmpty(txtNombrePresentacion.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre Presentación", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf String.IsNullOrEmpty(txtCodigoBarraPresentacion.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Código de Barra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoBarraPresentacion.Focus()
            ElseIf IsNumeric(txtFactor.Value) = False Then
                XtraMessageBox.Show("Ingrese valor númerico para el factor.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFactor.Focus()
            ElseIf txtFactor.Value <= 0 Then
                XtraMessageBox.Show("El factor debe ser mayor a 0.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFactor.Focus()
            Else
                Datos_Correctos_Presentacion = True
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

    Private Sub Cargar_Producto_Presentacion()

        Try

            dGridPresentacion.DataSource = Nothing

            If pBeProductoPresentacionList.Count > 0 Then

                Dim DT As New DataTable("Presentacion")
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("Presentación", GetType(String))
                DT.Columns.Add("Factor", GetType(Double))
                DT.Columns.Add("Peso", GetType(Double))
                DT.Columns.Add("Alto", GetType(Double))
                DT.Columns.Add("Largo", GetType(Double))
                DT.Columns.Add("Ancho", GetType(Double))
                DT.Columns.Add("Imprime Barra", GetType(Boolean))
                DT.Columns.Add("Sistema", GetType(Boolean))
                DT.Columns.Add("EsPallet", GetType(Boolean))
                DT.Columns.Add("PresentaciónPallet", GetType(String))

                Dim vNomPresentacionContenidaEnPallet As String = ""

                For Each Obj As clsBeProducto_Presentacion In pBeProductoPresentacionList.FindAll(Function(b) b.Activo = chkActivoPR.Checked)

                    If Obj.IdPresentacionPallet <> 0 Then

                        Dim BePresentacion As clsBeProducto_Presentacion
                        BePresentacion = clsLnProducto_presentacion.GetSingle(Obj.IdPresentacion)

                        If Not BePresentacion Is Nothing Then
                            vNomPresentacionContenidaEnPallet = BePresentacion.Codigo_barra + " - " + BePresentacion.Nombre
                        End If

                    End If

                    DT.Rows.Add(Obj.IdPresentacion,
                                Obj.Nombre,
                                Obj.Factor,
                                Obj.Peso,
                                Obj.Alto,
                                Obj.Largo,
                                Obj.Ancho,
                                Obj.Imprime_barra,
                                Obj.Sistema,
                                Obj.EsPallet,
                                vNomPresentacionContenidaEnPallet)
                Next

                dGridPresentacion.DataSource = DT

                If GrdPresentacion.Columns.Count > 0 Then
                    GrdPresentacion.Columns("Código").Visible = True
                    GrdPresentacion.Columns("Presentación").Visible = True
                    GrdPresentacion.Columns("Factor").Visible = True
                    GrdPresentacion.Columns("Peso").Visible = True
                    GrdPresentacion.Columns("Alto").Visible = True
                    GrdPresentacion.Columns("Largo").Visible = True
                    GrdPresentacion.Columns("Ancho").Visible = True
                    GrdPresentacion.Columns("Imprime Barra").Visible = True
                    GrdPresentacion.Columns("Sistema").Visible = True

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

#End Region

#Region " Producto Sustituto "

    Private Sub Limpiar()

        cmdSavePS.Tag = Nothing
        txtIdProductoR.Text = String.Empty
        txtNombrePR.Text = String.Empty
        cmbPresentacionR.ItemIndex = 0

    End Sub
    Private Sub LimpiarConver()

        cmdSaveCn.Tag = Nothing
        txtFactorConver.Text = 0.0
        cmbOriginal.ItemIndex = 0
        cmbInversa.ItemIndex = 0

    End Sub

    Private Sub ValidaProductoSustituto()

        If cmbProductoP.ItemIndex = -1 Then Throw New Exception("Seleccione Presentación Original.")
        If String.IsNullOrEmpty(txtIdProductoR.Text.Trim()) Then Throw New Exception("Seleccione Producto de Reemplazo.")
        If clsLnProducto.Exists(txtIdProductoR.Text) = False Then Throw New Exception("El Producto de Reeemplazo no existe.")
        If cmbPresentacionR.ItemIndex = -1 Then Throw New Exception("Seleccione Presentación de Reemplazo.")

    End Sub

    Private Sub Cargar_Producto_Sustituto()

        Try

            lblNombreProductoO.Text = txtNombre.Text.Trim()
            CargaComboPresentacion()
            ListarProductoSustituto()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub CargarProductoKit()

        Try

            lblNombreProductoO.Text = txtNombre.Text.Trim()
            ListarProductoKit()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Cargar_Conversion_Presentaciones()
        Try

            CargaComboPresentacionConver()
            CargaComboPresentacionInversa()
            ListarConversionesPresentacion()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub CargaComboPresentacion()

        Try

            If pBeProductoPresentacionList IsNot Nothing AndAlso pBeProductoPresentacionList.Count > 0 Then

                Dim DT As New DataTable("Presentacion")
                DT.Columns.Add("Id", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))

                For Each Obj As clsBeProducto_Presentacion In pBeProductoPresentacionList
                    DT.Rows.Add(Obj.IdPresentacion, Obj.Nombre)
                Next

                If DT.Rows.Count > 0 Then
                    cmbProductoP.Properties.DisplayMember = "Nombre"
                    cmbProductoP.Properties.ValueMember = "Id"
                    cmbProductoP.Properties.DataSource = DT
                    cmbProductoP.ItemIndex = 0
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub CargaComboPresentacion(ByVal pIdProductoReemplazo As Integer)

        Try

            Dim l As New List(Of clsBeProducto_Presentacion)

            l = clsLnProducto_presentacion.Get_All_Presentaciones_By_IdProducto(pIdProductoReemplazo, True).ToList()

            If l.Count > 0 Then

                Dim DT As New DataTable("ProductoR")
                DT.Columns.Add("Id", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))

                For Each Obj As clsBeProducto_Presentacion In l
                    DT.Rows.Add(Obj.IdPresentacion, Obj.Nombre)
                Next

                cmbPresentacionR.Properties.DisplayMember = "Nombre"
                cmbPresentacionR.Properties.ValueMember = "Id"
                cmbPresentacionR.Properties.DataSource = DT

            Else
                cmbPresentacionR.Properties.DataSource = Nothing
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub ListarProductoSustituto()

        Try

            GrdProductoS.DataSource = Nothing

            If pBeProductoSustitutoList.Count > 0 Then

                Dim DT As New DataTable("ProductoSustituto")
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("IdProductoPresentacionOriginal", GetType(Integer))
                DT.Columns.Add("IdProductoReemplazo", GetType(Integer))
                DT.Columns.Add("IdProductoPresentacionReemplazo", GetType(Integer))
                DT.Columns.Add("Presentación Original", GetType(String))
                DT.Columns.Add("Producto Reemplazo", GetType(String))
                DT.Columns.Add("Presentación Reemplazo", GetType(String))

                For Each Obj As clsBeProducto_sustituto In pBeProductoSustitutoList.FindAll(Function(b) b.Activo = chkActivoPS.Checked)
                    DT.Rows.Add(Obj.IdProductoSustituto, Obj.IdProductoPresentacionOriginal, Obj.IdProductoReemplazo, Obj.IdProductoPresentacionReemplazo,
                                Obj.ProductoPresentacionOriginal.Nombre, Obj.ProductoReemplazo.Nombre, Obj.ProductoPresentacionReemplazo.Nombre)
                Next

                GrdProductoS.DataSource = DT

                If GridViewProductoS.Columns.Count > 0 Then
                    GridViewProductoS.Columns("IdProductoPresentacionOriginal").Visible = False
                    GridViewProductoS.Columns("IdProductoReemplazo").Visible = False
                    GridViewProductoS.Columns("IdProductoPresentacionReemplazo").Visible = False
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
    Private Sub ListarConversionesPresentacion()

        Try

            GridControl1.DataSource = Nothing

            If pBeProductoPresConvList.Count > 0 Then

                Dim DT As New DataTable("PresentacionConversión")
                DT.Columns.Add("IdConversion", GetType(Integer))
                DT.Columns.Add("PresentacionOriginal", GetType(String))
                DT.Columns.Add("PresentacionDestino", GetType(String))
                DT.Columns.Add("IdPresentacionOrigen", GetType(Integer))
                DT.Columns.Add("IdPresentacionDestino", GetType(Integer))
                DT.Columns.Add("Factor", GetType(Integer))
                DT.Columns.Add("Inverso", GetType(String))

                For Each Obj As clsBeProducto_presentaciones_conversiones In pBeProductoPresConvList.FindAll(Function(b) b.Activo = chkActivosCn.Checked)
                    DT.Rows.Add(Obj.IdConversion, Obj.ProductoPresentacionOrigen.Nombre, Obj.ProductoPresentacionDestino.Nombre, Obj.IdPresentacionOrigen, Obj.IdPresentacionDestino, Obj.Factor, Obj.Inverso)
                Next

                GridControl1.DataSource = DT

                If GridView1.Columns.Count > 0 Then
                    GridView1.Columns("IdConversion").Visible = True
                    GridView1.Columns("PresentacionOriginal").Visible = True
                    GridView1.Columns("PresentacionDestino").Visible = True
                    GridView1.Columns("IdPresentacionOrigen").Visible = False
                    GridView1.Columns("IdPresentacionDestino").Visible = False
                    GridView1.Columns("Factor").Visible = True
                    GridView1.Columns("Inverso").Visible = True

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

#End Region

#Region " Producto Rellenado "

    Private Sub LimpiarPR()

        cmdSavePRL.Tag = Nothing
        txtIdUbicacion.Text = String.Empty
        txtNombreUbicacion.Text = String.Empty
        txtMinimoPicking.Value = 0
        txtMaximoPicking.Value = 0
        cmbPresentacionAbastecerCon.EditValue = Nothing
        'rbumbas.Checked = True

    End Sub

    Private Sub Valida_Producto_Rellenado()

        'If cmbPresentacionPR.ItemIndex = -1 Then Throw New Exception("Seleccione Presentación.")
        If cmbProductoEstado.ItemIndex = -1 Then Throw New Exception("Seleccione Estado.")
        If String.IsNullOrEmpty(txtIdUbicacion.Text.Trim()) Then Throw New Exception("Seleccione Ubicación.")

        If String.IsNullOrEmpty(txtMinimoPicking.Value) Then
            Throw New Exception("Ingrese valor en mínimo en picking.")
        ElseIf txtMinimoPicking.Value <= 0 Then
            Throw New Exception("Ingrese valor mayor a 0 en mínimo en picking.")
        End If

        If String.IsNullOrEmpty(txtMaximoPicking.Value) Then
            Throw New Exception("Ingrese valor en máximo en picking.")
        ElseIf txtMaximoPicking.Value <= 0 Then
            Throw New Exception("Ingrese valor mayor a 0 en máximo en picking.")
        End If

    End Sub

    Private Sub CargarProductoRellenado()

        Try

            lblNombreProductoPR.Text = pBeProducto.Codigo & " - " & pBeProducto.Nombre
            CargaComboPresentacionPR()
            CargaComboEstado()
            ListarProductoRellenado()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub CargaComboPresentacionPR()

        Try

            cmbPresentacionPR.Properties.DataSource = Nothing
            cmbPresentacionAbastecerCon.Properties.DataSource = Nothing

            If pBeProductoPresentacionList IsNot Nothing AndAlso pBeProductoPresentacionList.Count > 0 Then

                Dim DT As New DataTable("Presentacion")
                DT.Columns.Add("Id", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))

                For Each Obj As clsBeProducto_Presentacion In pBeProductoPresentacionList
                    DT.Rows.Add(Obj.IdPresentacion, Obj.Nombre)
                Next

                If DT.Rows.Count > 0 Then

                    cmbPresentacionPR.Properties.DisplayMember = "Nombre"
                    cmbPresentacionPR.Properties.ValueMember = "Id"
                    cmbPresentacionPR.Properties.DataSource = DT

                    cmbPresentacionAbastecerCon.Properties.DisplayMember = "Nombre"
                    cmbPresentacionAbastecerCon.Properties.ValueMember = "Id"
                    cmbPresentacionAbastecerCon.Properties.DataSource = DT

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub CargaComboPresentacionInversa()

        Try

            cmbInversa.Properties.DataSource = Nothing

            If pBeProductoPresentacionList IsNot Nothing AndAlso pBeProductoPresentacionList.Count > 0 Then

                Dim DT As New DataTable("Presentacion")
                DT.Columns.Add("Id", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))

                For Each Obj As clsBeProducto_Presentacion In pBeProductoPresentacionList
                    DT.Rows.Add(Obj.IdPresentacion, Obj.Nombre)
                Next

                If DT.Rows.Count > 0 Then
                    cmbInversa.Properties.DisplayMember = "Nombre"
                    cmbInversa.Properties.ValueMember = "Id"
                    cmbInversa.Properties.DataSource = DT
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Private Sub CargaComboEstado()

        Try

            cmbProductoEstado.Properties.DataSource = Nothing

            If lcmbPropietario.ItemIndex > -1 Then

                Dim DT As New DataTable("Estado")
                DT.Columns.Add("Id", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))

                Dim l As List(Of clsBeProducto_estado) = clsLnProducto_estado.GetAllByPropietario(lcmbPropietario.EditValue).ToList
                'Dim DT As DataTable = ToDataTable(l)

                For Each Obj As clsBeProducto_estado In l
                    DT.Rows.Add(Obj.IdEstado, Obj.Nombre)
                Next

                If DT.Rows.Count > 0 Then
                    cmbProductoEstado.Properties.DisplayMember = "Nombre"
                    cmbProductoEstado.Properties.ValueMember = "Id"
                    cmbProductoEstado.Properties.DataSource = DT
                    cmbProductoEstado.ItemIndex = 0
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub ListarProductoRellenado()

        Try

            GridProductoRellenado.DataSource = Nothing

            If pBeproductoRellenadoList.Count > 0 Then

                Dim DT As New DataTable("ProductoRellenado")
                DT.Columns.Add("IdRellenado", GetType(Integer))
                DT.Columns.Add("IdBodega", GetType(Integer))
                DT.Columns.Add("Ubicación", GetType(String))
                DT.Columns.Add("UMBas", GetType(String))
                DT.Columns.Add("Presentación", GetType(String))
                DT.Columns.Add("Estado", GetType(String))
                DT.Columns.Add("Mínimo", GetType(Double))
                DT.Columns.Add("Máximo", GetType(Double))
                DT.Columns.Add("AbastecerConPresentacion", GetType(String))
                DT.Columns.Add("Operador", GetType(String))

                For Each BeProductoRellenado As clsBeProducto_rellenado In pBeproductoRellenadoList

                    If BeProductoRellenado.Activo Then
                        DT.Rows.Add(BeProductoRellenado.IdRellenado,
                                    BeProductoRellenado.IdBodega,
                                    BeProductoRellenado.Ubicacion,
                                    pBeProducto.UnidadMedida.Nombre,
                                    BeProductoRellenado.Presentacion,
                                    BeProductoRellenado.Estado,
                                    BeProductoRellenado.Minimo,
                                    BeProductoRellenado.Maximo,
                                    BeProductoRellenado.NomPresentacionRellenarCon,
                                    BeProductoRellenado.NomOperador)

                    End If

                Next

                GridProductoRellenado.DataSource = DT

                If ViewProductoRellenado.Columns.Count > 0 Then
                    'ViewProductoRellenado.Columns("IdRellenado").Visible = False
                    ViewProductoRellenado.Columns("IdRellenado").Caption = "IdReabasto"
                    ViewProductoRellenado.BestFitColumns()
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

#End Region

#Region " Presentacion Tarima "

    Private Sub LimpiarPT()

        cmdSavePT.Tag = Nothing
        txtCantidad.Value = 0
        txtCantidadPorCama.Value = 0
        cmbPresentacionTarima.ItemIndex = 0
        cmbTipoTarima.ItemIndex = 0

    End Sub

    Private Sub ValidaPT()

        If cmbPresentacionTarima.ItemIndex = -1 Then Throw New Exception("Seleccione Presentación.")
        If cmbTipoTarima.ItemIndex = -1 Then Throw New Exception("Seleccione Tipo Tarima.")
        If txtCantidad.Value <= 0 Then Throw New Exception("Ingrese cantidad mayor a 0.")
        If txtCantidadPorCama.Value <= 0 Then Throw New Exception("Ingrese cantidad por cama mayor a 0.")

        If String.IsNullOrEmpty(txtCantidad.Value) Then
            Throw New Exception("Ingrese Cantidad.")
        End If

    End Sub

    Private Sub CargarPresentacionTarima()

        Try

            lblNombreProductoPT.Text = txtNombre.Text.Trim
            CargaComboPresentacionPT()
            CargaComboTipoTarima()
            ListarPresentacionTarima()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub CargaComboPresentacionPT()

        Try

            cmbPresentacionTarima.Properties.DataSource = Nothing

            If pBeProductoPresentacionList IsNot Nothing AndAlso pBeProductoPresentacionList.Count > 0 Then

                Dim DT As New DataTable("Presentacion")
                DT.Columns.Add("Id", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))

                For Each Obj As clsBeProducto_Presentacion In pBeProductoPresentacionList
                    DT.Rows.Add(Obj.IdPresentacion, Obj.Nombre)
                Next

                If DT.Rows.Count > 0 Then
                    cmbPresentacionTarima.Properties.DisplayMember = "Nombre"
                    cmbPresentacionTarima.Properties.ValueMember = "Id"
                    cmbPresentacionTarima.Properties.DataSource = DT
                    cmbPresentacionTarima.ItemIndex = 0
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub CargaComboPresentacionConver()

        Try

            cmbOriginal.Properties.DataSource = Nothing

            If pBeProductoPresentacionList IsNot Nothing AndAlso pBeProductoPresentacionList.Count > 0 Then

                Dim DT As New DataTable("Presentacion")
                DT.Columns.Add("Id", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))

                For Each Obj As clsBeProducto_Presentacion In pBeProductoPresentacionList
                    DT.Rows.Add(Obj.IdPresentacion, Obj.Nombre)
                Next

                If DT.Rows.Count > 0 Then
                    cmbOriginal.Properties.DisplayMember = "Nombre"
                    cmbOriginal.Properties.ValueMember = "Id"
                    cmbOriginal.Properties.DataSource = DT
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub CargaComboTipoTarima()

        Try

            cmbTipoTarima.Properties.DataSource = Nothing

            Dim DT As New DataTable("TipoTarima")
            DT.Columns.Add("Id", GetType(Integer))
            DT.Columns.Add("Nombre", GetType(String))

            Dim l As List(Of clsBeTipo_tarima) = clsLnTipo_tarima.GetAll(chkActivoPT.Checked)

            For Each Obj As clsBeTipo_tarima In l
                DT.Rows.Add(Obj.IdTipoTarima, Obj.Nombre)
            Next

            If DT.Rows.Count > 0 Then
                cmbTipoTarima.Properties.DisplayMember = "Nombre"
                cmbTipoTarima.Properties.ValueMember = "Id"
                cmbTipoTarima.Properties.DataSource = DT
                cmbTipoTarima.ItemIndex = 0
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub ListarPresentacionTarima()

        Try

            GridPresentacionTarima.DataSource = Nothing

            If pBePresentacionTarimaList.Count > 0 Then

                Dim DT As New DataTable("PresentacionTarima")
                DT.Columns.Add("IdPresentacionTarima", GetType(Integer))
                DT.Columns.Add("Presentación", GetType(String))
                DT.Columns.Add("Tipo Tarima", GetType(String))
                DT.Columns.Add("Cantidad", GetType(Double))
                DT.Columns.Add("CantidadPorCama", GetType(Double))

                For Each Obj As clsBeProducto_presentacion_tarima In pBePresentacionTarimaList.FindAll(Function(b) b.Activo = chkActivoPT.Checked)
                    DT.Rows.Add(Obj.IdPresentacionTarima, Obj.Presentacion, Obj.TipoTarima, Obj.Cantidad, Obj.CantidadPorCama)
                Next

                GridPresentacionTarima.DataSource = DT

                If ViewPT.Columns.Count > 0 Then
                    ViewPT.Columns("IdPresentacionTarima").Visible = True
                End If

            End If

        Catch ex As Exception
            'XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

#End Region

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try
            mnuGuardar.Enabled = False

            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Guardar Producto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                    If Guardar() Then

                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Not InvokeListarProductos Is Nothing Then InvokeListarProductos.Invoke
                        DialogResult = DialogResult.OK
                        Close()

                    End If

                End If

            End If

            mnuGuardar.Enabled = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mnuGuardar.Enabled = True
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim BeProducto As New clsBeProducto
            BeProducto.Propietario = New clsBePropietarios
            BeProducto.Clasificacion = New clsBeProducto_clasificacion
            BeProducto.Familia = New clsBeProducto_familia
            BeProducto.Marca = New clsBeProducto_marca
            BeProducto.TipoProducto = New clsBeProducto_tipo
            BeProducto.UnidadMedida = New clsBeUnidad_medida
            BeProducto.Arancel = New clsBeArancel

            BeProducto.Propietario.IdPropietario = lcmbPropietario.EditValue
            BeProducto.IdPropietario = lcmbPropietario.EditValue

            If Not String.IsNullOrEmpty(lcmbClasificacion.EditValue) Then
                BeProducto.Clasificacion.IdClasificacion = CInt(lcmbClasificacion.EditValue)
                BeProducto.IdClasificacion = CInt(lcmbClasificacion.EditValue) '#CKFK 20180926 12:25 PM Agregué esto porque no se estaba llenando este Id
            End If

            If Not String.IsNullOrEmpty(lcmbFamilia.EditValue) Then
                BeProducto.Familia.IdFamilia = CInt(lcmbFamilia.EditValue)
                BeProducto.IdFamilia = CInt(lcmbFamilia.EditValue) '#CKFK 20180926 12:25 PM Agregué esto porque no se estaba llenando este Id
            End If

            If Not String.IsNullOrEmpty(lcmbMarca.EditValue) Then
                BeProducto.Marca.IdMarca = CInt(lcmbMarca.EditValue)
                BeProducto.IdMarca = CInt(lcmbMarca.EditValue) '#CKFK 20180926 12:25 PM Agregué esto porque no se estaba llenando este Id
            End If

            If Not String.IsNullOrEmpty(txtIdTipoProducto.EditValue) Then
                BeProducto.TipoProducto.IdTipoProducto = CInt(txtIdTipoProducto.EditValue)
                BeProducto.IdTipoProducto = CInt(txtIdTipoProducto.EditValue) '#CKFK 20180926 12:25 PM Agregué esto porque no se estaba llenando este Id
            End If

            If Not String.IsNullOrEmpty(lcmbUnidadMedidaBasica.EditValue) Then
                BeProducto.UnidadMedida.IdUnidadMedida = CInt(lcmbUnidadMedidaBasica.EditValue)
                BeProducto.IdUnidadMedidaBasica = CInt(lcmbUnidadMedidaBasica.EditValue) '#CKFK 20180926 12:25 PM Agregué esto porque no se estaba llenando este Id
            End If

            If Not String.IsNullOrEmpty(lcmbUnidadMedidaCobro.EditValue) Then
                BeProducto.IdUnidadMedidaCobro = CInt(lcmbUnidadMedidaCobro.EditValue) '#GT 20240725 03:10 PM Agregué esto porque no se estaba llenando este Id
            End If

            If cmbPerfilSerializado.ItemIndex > -1 Then
                BeProducto.IdPerfilSerializado = cmbPerfilSerializado.EditValue
            Else
                BeProducto.IdPerfilSerializado = 0
            End If

            If cmbTipoRotacion.ItemIndex > -1 Then
                BeProducto.IdTipoRotacion = cmbTipoRotacion.EditValue
            End If

            If cmbCamara.ItemIndex > -1 Then
                BeProducto.IdCamara = cmbCamara.EditValue
            Else
                BeProducto.IdCamara = 0
            End If

            If cmbIndiceRotacion.ItemIndex > -1 Then
                BeProducto.IdIndiceRotacion = cmbIndiceRotacion.EditValue
            Else
                BeProducto.IdIndiceRotacion = 0
            End If

            If cmbSymbology.ItemIndex > -1 Then
                BeProducto.IdSimbologia = cmbSymbology.EditValue
            Else
                BeProducto.IdSimbologia = 0
            End If

            BeProducto.Captura_arancel = chkCapturaArancel.Checked

            If cmbArancel.ItemIndex > -1 Then
                BeProducto.Arancel.IdArancel = cmbArancel.EditValue
            Else
                BeProducto.Arancel.IdArancel = 0
            End If

            BeProducto.Tolerancia = CInt(txtTolerancia.Value)

            If txtCodigo.Text = "" Then

                If XtraMessageBox.Show("No le asignó un código al producto.¿Quiere que se le asigne automáticamente un código?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                    txtCodigo.Text = Correlativo()
                Else
                    XtraMessageBox.Show("Ingrese el Código para el producto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigo.Focus()
                    Return False
                End If

            Else

                BeProducto.Codigo = txtCodigo.Text 'Correlativo() #CKFK 20181005 10:26PM Quité el correlativo del producto para que lo guarde como lo crean en CLC

            End If

            BeProducto.Nombre = txtNombre.Text.Trim()
            BeProducto.Codigo_barra = txtCodigoBarra.Text.Trim()

            If BeProducto.Codigo.Count > pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud Then
                XtraMessageBox.Show(String.Format("El Código debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()
                Return False
            End If

            If BeProducto.Nombre.Count > pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre del Producto debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
                Return False
            End If

            If BeProducto.Codigo_barra.Count > pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud Then
                XtraMessageBox.Show(String.Format("El código de barra debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoBarra.Focus()
                Return False
            End If

            BeProducto.Existencia_min = txtExitenciaMinima.Value
            BeProducto.Existencia_max = txtExistenciaMaxima.Value

            BeProducto.Costo = CDbl(txtCosto.Text)
            BeProducto.Precio = CDbl(txtPrecio.Text)

            BeProducto.Control_lote = chkControlLote.Checked
            BeProducto.Genera_lote = chkGeneraLote.Checked

            '#EJC20210426: Por cealsa, este parámetro considera si utiliza LP para la UMBAS (Sin pres)
            BeProducto.Genera_lp = chkGeneraLicAutoP.Checked

            BeProducto.Serializado = chkSerializado.Checked

            BeProducto.Control_vencimiento = chkControlVencimiento.Checked

            ' Grupo Peso
            BeProducto.Control_peso = chkCapturarPeso.Checked
            BeProducto.Peso_recepcion = chkCapturarPeso.Checked
            BeProducto.Peso_referencia = CDbl(txtPesoReferencia.Text)
            BeProducto.Peso_despacho = chkCapturarPeso.Checked
            BeProducto.Peso_tolerancia = CDbl(txtPesoTolerancia.Text)

            ' Grupo Temperatura
            BeProducto.Temperatura_recepcion = chkCapturaTemperatura.Checked
            BeProducto.Temperatura_referencia = CDbl(txtTemperaturaReferencia.Text)
            BeProducto.Temperatura_despacho = chkCapturaTemperatura.Checked
            BeProducto.Temperatura_tolerancia = CDbl(txtPesoTolerancia.Text)

            BeProducto.Ciclo_vida = txtCicloVida.Value
            BeProducto.Control_vencimiento = chkControlVencimiento.Checked
            BeProducto.Materia_prima = chkEsMateriaPrima.Checked
            BeProducto.Kit = chkEsKit.Checked

            BeProducto.Activo = True

            BeProducto.User_agr = AP.UsuarioAp.IdUsuario
            BeProducto.Fec_agr = Now
            BeProducto.User_mod = AP.UsuarioAp.IdUsuario
            BeProducto.Fec_mod = Now

            If picFoto.Image IsNot Nothing Then
                BeProducto.Imagen = ImageToByteArray(picFoto.Image)
            End If

            BeProducto.Capturar_aniada = chkCapturarAniada.Checked
            BeProducto.Captura_arancel = chkCapturaArancel.Checked
            BeProducto.Es_hardware = chkEsHW.Checked
            pBeProducto.IdTipoEtiqueta = cmbEtiqueta.EditValue

            BeProducto.Fechamanufactura = chkFechaManufactura.Checked

            If BeProducto.IdPerfilSerializado = 3 Then
                BeProducto.Noserie = txtNoSerie.Text.Trim
            End If

            If chkEsHW.Checked Then
                BeProducto.Noparte = txtNoParte.Text.Trim
            Else
                BeProducto.Noparte = ""
            End If

            'Dimensiones 
            BeProducto.Largo = txtLargoUB.Value
            BeProducto.Alto = txtAltoUB.Value
            BeProducto.Ancho = txtAnchoUB.Value
            BeProducto.Margen_Impresion = txtMargen.Value

            BeProducto.IsNew = True

            'GT08072022 se guardan parametros A y B si existieran
            If lcmbParametroA.ItemIndex > -1 Then
                BeProducto.IdProductoParametroA = lcmbParametroA.EditValue
            Else
                BeProducto.IdProductoParametroA = 0
            End If

            If lcmbParametroB.ItemIndex > -1 Then
                BeProducto.IdProductoParametroB = lcmbParametroB.EditValue
            Else
                BeProducto.IdProductoParametroA = 0
            End If

            BeProducto.IdTipoEtiqueta = cmbEtiqueta.EditValue
            BeProducto.IdTipoManufactura = cmbTipoManufactura.EditValue

            '#EJC20210318: Asignar bodega por defecto cuando se cree un nuevo código desde otra pantalla.
            If IdBodegaNuevoProducto <> 0 Then

                pListObjPB = New List(Of clsBeProducto_bodega)

                Dim Obj As New clsBeProducto_bodega() With {.IdBodega = IdBodegaNuevoProducto,
                                                            .IdProducto = pBeProducto.IdProducto,
                                                            .User_agr = AP.UsuarioAp.IdUsuario,
                                                            .Fec_agr = Now,
                                                            .User_mod = AP.UsuarioAp.IdUsuario,
                                                            .Fec_mod = Now,
                                                            .Activo = True}
                pListObjPB.Add(Obj)


            Else

                Dim vIdBodega As Integer = 0
                Dim vSeleccion As Boolean = False
                pListObjPB = New List(Of clsBeProducto_bodega)

                For i As Integer = 0 To GrdProductoBodega.DataRowCount - 1

                    vSeleccion = GrdProductoBodega.GetRowCellValue(i, "Selección")
                    vIdBodega = GrdProductoBodega.GetRowCellValue(i, "IdBodega")

                    If vSeleccion Then

                        Dim BeProductoProdegaNew As New clsBeProducto_bodega() With {.IdBodega = vIdBodega,
                                                        .IdProducto = BeProducto.IdProducto,
                                                        .User_agr = AP.UsuarioAp.IdUsuario,
                                                        .Fec_agr = Now,
                                                        .User_mod = AP.UsuarioAp.IdUsuario,
                                                        .Fec_mod = Now,
                                                        .Activo = True}

                        pListObjPB.Add(BeProductoProdegaNew)

                    End If

                Next

            End If

            clsLnProducto.Guardar(BeProducto,
                                  pBeProductoParametroList,
                                  pBeProductoCodigosBarraList,
                                  pBeProductoPresentacionList,
                                  pBeProductoSustitutoList,
                                  pBeproductoRellenadoList,
                                  pBePresentacionTarimaList,
                                  pListObjPB,
                                  pBeProductoPresConvList,
                                  pBeProductoKitList,
                                  pListProdImgs)

            Guardar = True

            If IdBodegaNuevoProducto <> 0 Then
                IdProductoBodegaReturn = pListObjPB.Item(0).IdProductoBodega
            End If


            If tabPresentacionTarima.Text.EndsWith("*") Then
                tabPresentacionTarima.Text = tabPresentacionTarima.Text.Remove(tabPresentacionTarima.Text.Length - 1)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Function Actualizar_Producto() As Boolean

        Actualizar_Producto = False

        Try

            If Datos_Correctos() Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")

                If pBeProductoCodigosBarraList IsNot Nothing AndAlso pBeProductoCodigosBarraList.Count > 0 Then

                    Dim lIndex As Integer = -1

                    lIndex = pBeProductoCodigosBarraList.FindIndex(Function(c) c.IdProducto = pBeProducto.IdProducto AndAlso c.Codigo_barra = txtCodigoBarra.Text.Trim())

                    If lIndex > -1 Then
                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("El código de barra ya existe en producto código de barra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If
                End If

                If String.IsNullOrEmpty(lcmbClasificacion.EditValue) = False Then
                    pBeProducto.Clasificacion.IdClasificacion = CInt(lcmbClasificacion.EditValue)
                    pBeProducto.IdClasificacion = CInt(lcmbClasificacion.EditValue) '#CKFK 20181005 10:36 PM Agregué esto porque no se estaba llenando este Id
                End If

                If String.IsNullOrEmpty(lcmbFamilia.EditValue) = False Then
                    pBeProducto.Familia.IdFamilia = CInt(lcmbFamilia.EditValue)
                    pBeProducto.IdFamilia = CInt(lcmbFamilia.EditValue) '#CKFK 20181005 10:36 PM Agregué esto porque no se estaba llenando este Id
                End If

                If String.IsNullOrEmpty(lcmbMarca.EditValue) = False Then
                    pBeProducto.Marca.IdMarca = CInt(lcmbMarca.EditValue)
                    pBeProducto.IdMarca = CInt(lcmbMarca.EditValue)
                End If

                If String.IsNullOrEmpty(txtIdTipoProducto.EditValue) = False Then
                    pBeProducto.TipoProducto.IdTipoProducto = CInt(txtIdTipoProducto.EditValue)
                    pBeProducto.IdTipoProducto = CInt(txtIdTipoProducto.EditValue)
                End If

                If String.IsNullOrEmpty(lcmbUnidadMedidaBasica.EditValue) = False Then
                    pBeProducto.UnidadMedida.IdUnidadMedida = CInt(lcmbUnidadMedidaBasica.EditValue)
                    pBeProducto.IdUnidadMedidaBasica = CInt(lcmbUnidadMedidaBasica.EditValue)
                End If

                If Not String.IsNullOrEmpty(lcmbUnidadMedidaCobro.EditValue) Then
                    pBeProducto.IdUnidadMedidaCobro = CInt(lcmbUnidadMedidaCobro.EditValue)
                End If

                If cmbTipoRotacion.ItemIndex > -1 Then
                    pBeProducto.IdTipoRotacion = cmbTipoRotacion.EditValue
                End If

                If cmbCamara.ItemIndex > -1 Then
                    pBeProducto.IdCamara = cmbCamara.EditValue
                Else
                    pBeProducto.IdCamara = 0
                End If

                If cmbIndiceRotacion.ItemIndex > -1 Then
                    pBeProducto.IdIndiceRotacion = cmbIndiceRotacion.EditValue
                Else
                    pBeProducto.IdIndiceRotacion = 0
                End If

                If cmbSymbology.ItemIndex > -1 Then
                    pBeProducto.IdSimbologia = cmbSymbology.EditValue
                Else
                    pBeProducto.IdSimbologia = 0
                End If

                pBeProducto.Captura_arancel = chkCapturaArancel.Checked

                If cmbArancel.ItemIndex > -1 Then
                    pBeProducto.Arancel.IdArancel = cmbArancel.EditValue
                Else
                    pBeProducto.Arancel.IdArancel = 0
                End If

                If chkEsKit.Checked Then
                    If pBeProductoKitList.Count = 0 Then
                        If XtraMessageBox.Show("No tiene productos asociados para kit.¿Guardar sin definición de kit?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                            Exit Function
                        End If
                    End If
                End If

                pBeProducto.Tolerancia = CInt(txtTolerancia.Text)
                pBeProducto.Codigo = txtCodigo.Text.Trim()
                pBeProducto.Nombre = txtNombre.Text.Trim()
                pBeProducto.Codigo_barra = txtCodigoBarra.Text.Trim()

                If pBeProducto.Codigo.Count > pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud Then
                    XtraMessageBox.Show(String.Format("El Código debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigo.Focus()
                    Return False
                End If

                If pBeProducto.Nombre.Count > pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                    XtraMessageBox.Show(String.Format("El Nombre del Producto debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNombre.Focus()
                    Return False
                End If

                If pBeProducto.Codigo_barra.Count > pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud Then
                    XtraMessageBox.Show(String.Format("El código de barra debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigoBarra.Focus()
                    Return False
                End If

                pBeProducto.Existencia_min = txtExitenciaMinima.Value
                pBeProducto.Existencia_max = txtExistenciaMaxima.Value

                pBeProducto.Costo = txtCosto.Value
                pBeProducto.Precio = txtPrecio.Value

                pBeProducto.Serializado = chkSerializado.Checked
                '#EJC20210426: Por cealsa, este parámetro considera si utiliza LP para la UMBAS (Sin pres)
                pBeProducto.Genera_lp = chkGeneraLicAutoP.Checked

                pBeProducto.Control_lote = chkControlLote.Checked
                pBeProducto.Genera_lote = chkGeneraLote.Checked

                pBeProducto.Control_vencimiento = chkControlVencimiento.Checked

                pBeProducto.Peso_recepcion = chkCapturarPeso.Checked
                pBeProducto.Peso_referencia = txtPesoReferencia.Text

                pBeProducto.Peso_despacho = chkCapturarPeso.Checked
                pBeProducto.Peso_tolerancia = txtPesoTolerancia.Text

                ' Grupo Temperatura

                pBeProducto.Temperatura_recepcion = chkCapturaTemperatura.Checked
                pBeProducto.Temperatura_referencia = txtTemperaturaReferencia.Text

                pBeProducto.Temperatura_despacho = chkCapturaTemperatura.Checked
                pBeProducto.Temperatura_tolerancia = txtTemperaturaTolerancia.Text

                pBeProducto.Ciclo_vida = txtCicloVida.Value
                pBeProducto.Control_vencimiento = chkControlVencimiento.Checked
                pBeProducto.Materia_prima = chkEsMateriaPrima.Checked
                pBeProducto.Kit = chkEsKit.Checked

                pBeProducto.User_mod = AP.UsuarioAp.IdUsuario
                pBeProducto.Fec_mod = Now

                pBeProducto.Control_peso = chkCapturarPeso.Checked
                pBeProducto.Noparte = txtNoParte.Text
                pBeProducto.Activo = chkActivo.Checked

                If pBeProducto.Noparte = "" Then
                    If chkEsHW.Checked Then
                        pBeProducto.Noparte = txtNoParte.Text.Trim
                    Else
                        pBeProducto.Noparte = ""
                    End If
                End If

                pBeProducto.Capturar_aniada = chkCapturarAniada.Checked
                pBeProducto.Captura_arancel = chkCapturaArancel.Checked
                pBeProducto.Es_hardware = chkEsHW.Checked
                pBeProducto.IdTipoEtiqueta = cmbEtiqueta.EditValue

                pBeProducto.Noserie = txtNoSerie.Text.Trim()

                If pBeProducto.Noserie = "" Then
                    If chkSerializado.Checked Then
                        pBeProducto.IdPerfilSerializado = cmbPerfilSerializado.EditValue
                        pBeProducto.Noserie = txtNoSerie.Text.Trim
                    Else
                        pBeProducto.IdPerfilSerializado = 0
                        pBeProducto.Noserie = ""
                    End If

                    If pBeProducto.IdPerfilSerializado = 3 Then
                        pBeProducto.Noserie = txtNoSerie.Text.Trim
                    End If

                End If

                pBeProducto.Fechamanufactura = chkFechaManufactura.Checked

                'Dimensiones 
                pBeProducto.Largo = txtLargoUB.Value
                pBeProducto.Alto = txtAltoUB.Value
                pBeProducto.Ancho = txtAnchoUB.Value
                pBeProducto.Margen_Impresion = txtMargen.Value

                If picFoto.Image IsNot Nothing Then
                    pBeProducto.Imagen = ImageToByteArray(picFoto.Image)
                End If

                'GT08072022 se guardan parametros A y B si existieran
                If lcmbParametroA.ItemIndex > -1 Then
                    pBeProducto.IdProductoParametroA = lcmbParametroA.EditValue
                Else
                    pBeProducto.IdProductoParametroA = 0
                End If

                If lcmbParametroB.ItemIndex > -1 Then
                    pBeProducto.IdProductoParametroB = lcmbParametroB.EditValue
                Else
                    pBeProducto.IdProductoParametroA = 0
                End If

                pBeProducto.IdTipoManufactura = cmbTipoManufactura.EditValue

                '#GT19052023: valida que producto no tenga existencia antes de remover paramteros (peso,lote...)
                If Not pBeProducto.IsNew Then

                    Dim pIdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBeProducto.IdProducto, AP.IdBodega)
                    Dim pMensaje As String = ""
                    Dim pValida As Boolean = False
                    Dim pStock As New clsBeStock

                    If pBeProducto.Control_peso <> pProducto.Control_peso Then
                        pMensaje = String.Format("El producto ya tiene existencia, no se pueden modificar el parametro de Captura Peso.", txtCodigoBarra.Text.Trim())
                        pValida = True
                    ElseIf pBeProducto.Control_lote <> pProducto.Control_lote Then
                        pMensaje = String.Format("El producto ya tiene existencia, no se pueden modificar el parametro de Control Lote.", txtCodigoBarra.Text.Trim())
                        pValida = True
                    ElseIf pBeProducto.Control_vencimiento <> pProducto.Control_vencimiento Then
                        pMensaje = String.Format("El producto ya tiene existencia, no se pueden modificar el parametro Captura Vencimiento.", txtCodigoBarra.Text.Trim())
                        pValida = True
                    End If

                    If pValida Then

                        pStock.Producto = pProducto
                        clsLnStock.GetStockByIdProducto(pStock)

                        If Not pStock Is Nothing Then
                            If pStock.Cantidad > 0 Then
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show(pMensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Function
                            End If
                        End If

                    End If

                End If

                clsLnProducto.Guardar(pBeProducto,
                                      pBeProductoParametroList,
                                      pBeProductoCodigosBarraList,
                                      pBeProductoPresentacionList,
                                      pBeProductoSustitutoList,
                                      pBeproductoRellenadoList,
                                      pBePresentacionTarimaList,
                                      pListObjPB,
                                      pBeProductoPresConvList,
                                      pBeProductoKitList,
                                      pListProdImgs)

                Actualizar_Producto = True

                Cargar_Conversion_Presentaciones()

                If tabPresentacionTarima.Text.EndsWith("*") Then
                    tabPresentacionTarima.Text = tabPresentacionTarima.Text.Remove(tabPresentacionTarima.Text.Length - 2)
                End If

                SplashScreenManager.CloseForm(False)

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#Region " Busca Clasificación "

    Private Sub lnkClasificacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkClasificacion.LinkClicked

        Try

            If lcmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                Dim Clasificacion As New frmProducto_ClasificacionList() With {.pIdPropietario = lcmbPropietario.EditValue, .Modo = frmProducto_ClasificacionList.pModo.Seleccion}

                If OpcionesMenu IsNot Nothing Then
                    Clasificacion.OpcionesMenu = OpcionesMenu
                    Clasificacion.mnuNuevo.Enabled = OpcionesMenu.Modificar
                    Clasificacion.mnuActualizar.Enabled = OpcionesMenu.Leer
                End If

                Clasificacion.ShowDialog()

                '#EJC20220326B: Cambio a lookupedit clasificación.
                '#GT21202022_0810: se recarga el combobox porque se agregó un nuevo elemento y se debe setear
                IMS.Listar_Clasificaciones(lcmbClasificacion, lcmbPropietario.EditValue)

                If Clasificacion.pObjPC IsNot Nothing AndAlso Clasificacion.pObjPC.IdClasificacion <> 0 Then
                    lcmbClasificacion.EditValue = Clasificacion.pObjPC.IdClasificacion
                    'txtNombreClasificacion.Text = Clasificacion.pObjPC.Nombre
                End If

                Clasificacion.Close()
                Clasificacion.Dispose()



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

#End Region

#Region " Busca Familia "

    Private Sub lnkFamilia_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkFamilia.LinkClicked

        Try

            If lcmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                Dim Familia As New frmProducto_FamiliaList() With {.pIdPropietario = lcmbPropietario.EditValue, .Modo = frmProducto_FamiliaList.pModo.Seleccion}

                If OpcionesMenu IsNot Nothing Then
                    Familia.OpcionesMenu = OpcionesMenu
                    Familia.mnuNuevo.Enabled = OpcionesMenu.Modificar
                    Familia.mnuActualizar.Enabled = OpcionesMenu.Leer
                End If

                Familia.ShowDialog()


                '#EJC20220326C: Cambio a lookupedit familia.
                IMS.Listar_Familias(lcmbFamilia, lcmbPropietario.EditValue)

                If Familia.pObjPF IsNot Nothing AndAlso Familia.pObjPF.IdFamilia <> 0 Then
                    lcmbFamilia.EditValue = Familia.pObjPF.IdFamilia
                    'txtNombreFamilia.Text = Familia.pObjPF.Nombre
                End If

                Familia.Close()
                Familia.Dispose()

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

#End Region

#Region " Busca Marca "

    Private Sub lnkMarca_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkMarca.LinkClicked

        Try

            If lcmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim Marca As New frmProducto_MarcaList() With {.pIdPropietario = lcmbPropietario.EditValue, .Modo = frmProducto_MarcaList.pModo.Seleccion}

                If OpcionesMenu IsNot Nothing Then
                    Marca.OpcionesMenu = OpcionesMenu
                    Marca.mnuActualizar.Enabled = OpcionesMenu.Leer
                    Marca.mnuNuevo.Enabled = OpcionesMenu.Modificar
                End If

                Marca.ShowDialog()

                '#EJC20220326C: Cambio a lookupedit Marca.
                IMS.Listar_Marcas_By_IdPropietario(lcmbMarca, lcmbPropietario.EditValue)

                If Marca.pObjM IsNot Nothing AndAlso Marca.pObjM.IdMarca <> 0 Then
                    lcmbMarca.EditValue = Marca.pObjM.IdMarca
                End If

                Marca.Close()
                Marca.Dispose()
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

#End Region

#Region " Busca Tipo Producto "

    Private Sub lnkTipoProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkTipoProducto.LinkClicked

        Try

            If lcmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                Dim TipoProducto As New frmProducto_TipoList() With {.pIdPropietario = lcmbPropietario.EditValue, .Modo = frmProducto_TipoList.pModo.Seleccion}

                If OpcionesMenu IsNot Nothing Then
                    TipoProducto.OpcionesMenu = OpcionesMenu
                    TipoProducto.mnuNuevo.Enabled = OpcionesMenu.Modificar
                    TipoProducto.mnuActualizar.Enabled = OpcionesMenu.Leer
                End If

                TipoProducto.ShowDialog()

                '#CKFK20220721: Cambio a lookupedit TipoProducto.
                IMS.Listar_TipoProducto_By_IdPropietario(txtIdTipoProducto, lcmbPropietario.EditValue)

                If TipoProducto.pObjTP IsNot Nothing AndAlso TipoProducto.pObjTP.IdTipoProducto <> 0 Then
                    txtIdTipoProducto.EditValue = TipoProducto.pObjTP.IdTipoProducto
                Else
                    txtIdTipoProducto.EditValue = Nothing
                End If

                TipoProducto.Close() : TipoProducto.Dispose()

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

#End Region


#Region " Busca Parámetro A "

    Private Sub lnkParametroA_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkParametroA.LinkClicked

        Try

            If lcmbParametroA.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Parámetro A", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                Dim ParametroA As New frmProducto_Parametro_AList() With {.pIdPropietario = lcmbPropietario.EditValue,
                                                                         .Modo = frmProducto_Parametro_BList.pModo.Seleccion}
                ParametroA.ShowDialog()


                '#GT02092022:Listar parametro A
                IMS.Listar_ParametrosA(lcmbParametroA)

                If ParametroA.pObjPF IsNot Nothing AndAlso ParametroA.pObjPF.IdProductoParametroA <> 0 Then
                    txtIdTipoProducto.EditValue = ParametroA.pObjPF.IdProductoParametroA
                Else
                    txtIdTipoProducto.EditValue = Nothing
                End If

                ParametroA.Close() : ParametroA.Dispose()

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

#End Region


#Region " Busca Parámetro B "

    Private Sub lnkParametroB_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkParametroB.LinkClicked

        Try

            If lcmbParametroB.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Parámetro B", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                Dim ParametroB As New frmProducto_Parametro_BList() With {.pIdPropietario = lcmbPropietario.EditValue,
                                                                         .Modo = frmProducto_Parametro_BList.pModo.Seleccion}
                ParametroB.ShowDialog()


                '#GT02092022:Listar parametro B
                IMS.Listar_ParametrosB(lcmbParametroB)

                If ParametroB.pObjPF IsNot Nothing AndAlso ParametroB.pObjPF.IdProductoParametroB <> 0 Then
                    txtIdTipoProducto.EditValue = ParametroB.pObjPF.IdProductoParametroB
                Else
                    txtIdTipoProducto.EditValue = Nothing
                End If

                ParametroB.Close() : ParametroB.Dispose()

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

#End Region

#Region " VARIOS "
#End Region
    Private Sub btnExaminar_Click(sender As Object, e As EventArgs) Handles btnExaminar.Click
        Dim ofd As New OpenFileDialog() With {.Filter = "Imagenes JPG|*.jpg", .RestoreDirectory = True}
        If ofd.ShowDialog = DialogResult.OK Then
            picFoto.Image = Image.FromFile(ofd.FileName)
        End If
    End Sub

    Public Shared Function ImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New IO.MemoryStream()
        imageIn.Save(ms, ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Public Shared Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Dim ms As New IO.MemoryStream(byteArrayIn)
        Return Image.FromStream(ms)
    End Function

    Private Sub chkControlLote_CheckedChanged(sender As Object, e As EventArgs) Handles chkControlLote.CheckedChanged
        If chkControlLote.Checked Then
            chkGeneraLote.Enabled = True
        Else
            chkGeneraLote.Checked = False
        End If
    End Sub

    Private Sub Remover_Objeto_Personalizado()

        Try

            For Each control1 As Control In GprParametro.Controls
                Select Case control1.Name
                    Case "txtTexto" & cmbParametro.Tag, "txtNumero" & cmbParametro.Tag, "dtpFecha" & cmbParametro.Tag, "chkLogico" & cmbParametro.Tag
                        control1.Visible = False
                        control1.Dispose()
                        control1 = Nothing
                        GprParametro.Controls.Remove(control1)
                    Case Else
                        Exit Select
                End Select
            Next

            Refresh()

            ResumeLayout()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbParametro_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbParametro.EditValueChanged

        Try

            If Not cmbParametro.EditValue Is Nothing AndAlso Not IngresoPorGridAParametro Then
                Muestra_Objeto_Personalizado(cmbParametro.EditValue)
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

    Private Sub CreaObjetoNumero()

        pTexto.Visible = False
        pFecha.Visible = False
        pLogico.Visible = False

        pTexto.Dispose()
        pFecha.Dispose()
        pLogico.Dispose()

        Try

            pNumero = New NumericUpDown()

            pNumero.Name = "txtNumero" & cmbParametro.Tag

            With pNumero
                .Location = New Point(149, 112)
                .Width = 227
                .Maximum = 99999999999
                .Minimum = -9999999999999
                .DecimalPlaces = 6
                .Visible = True
            End With

            GprParametro.Controls.Add(pNumero)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub CreaObjetoTexto()

        pNumero.Visible = False
        pFecha.Visible = False
        pLogico.Visible = False

        pNumero.Dispose()
        pFecha.Dispose()
        pLogico.Dispose()

        Try

            pTexto = New TextBox()

            pTexto.Name = "txtTexto" & cmbParametro.Tag

            With pTexto
                .Location = New Point(149, 112)
                .Width = 227
                .Visible = True
            End With
            GprParametro.Controls.Add(pTexto)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub CreaObjetoFecha()

        pTexto.Visible = False
        pNumero.Visible = False
        pLogico.Visible = False

        pTexto.Dispose()
        pNumero.Dispose()
        pLogico.Dispose()

        Try

            pFecha = New DateTimePicker()
            pFecha.Name = "dtpFecha" & cmbParametro.Tag

            With pFecha
                .Location = New Point(149, 112)
                .Width = 227
                .CustomFormat = "dd/MM/yyyy hh:mm:ss"
                .Format = DateTimePickerFormat.Custom
                .Visible = True
            End With

            GprParametro.Controls.Add(pFecha)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub CreaObjetoLogico()

        pTexto.Visible = False
        pNumero.Visible = False
        pFecha.Visible = False

        pTexto.Dispose()
        pNumero.Dispose()
        pFecha.Dispose()

        Try

            pLogico = New CheckBox()

            pLogico.Name = "chkLogico" & cmbParametro.Tag

            With pLogico
                .Location = New Point(149, 112)
                .Width = 190
                .Visible = True
            End With

            GprParametro.Controls.Add(pLogico)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub chkSerializado_CheckedChanged(sender As Object, e As EventArgs) Handles chkSerializado.CheckedChanged

        If chkSerializado.Checked Then
            cmbPerfilSerializado.Enabled = True
            If pBeProducto.IdPerfilSerializado <> Nothing AndAlso pBeProducto.IdPerfilSerializado <> 0 Then
                cmbPerfilSerializado.EditValue = pBeProducto.IdPerfilSerializado
            End If
            txtNoSerie.Enabled = True
            txtNoSerie.Text = pBeProducto.Noserie
            'cmbPerfilSerializado.SelectedIndex = 0
            txtNoSerie.Focus()
        Else
            cmbPerfilSerializado.Enabled = False
            cmbPerfilSerializado.ItemIndex = -1
            txtNoSerie.Enabled = False
            txtNoSerie.Text = ""
        End If

    End Sub

    Private Sub Muestra_Objeto_Personalizado(ByVal IdProductoParametro As Integer)

        Try

            Remover_Objeto_Personalizado()

            Dim lIndex As Integer = -1

            If Not IngresoPorGridAParametro Then

                lIndex = pBeProductoParametroList.FindIndex(Function(b) b.TipoParametro.IdParametro = IdProductoParametro)

                If lIndex > -1 Then
                    XtraMessageBox.Show("El tipo de parámetro ya existe configurado para este producto", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Else

                    'Se está adicionando un nuevo parámetro, y en IdProductoParámetro, viene
                    'el Id de P_Parametro que indica el tipo y se crea el objeto.
                    '#EJC20170705
                    '#Enviar de nuevo.  
                    Dim BePParametro As New clsBeP_parametro With {.IdParametro = IdProductoParametro}
                    clsLnP_parametro.GetSingle(BePParametro)

                    txtTipo.Text = BePParametro.Tipo

                    Select Case BePParametro.Tipo

                        Case "Lógico"

                            CreaObjetoLogico()

                        Case "Texto"

                            CreaObjetoTexto()

                        Case "Fecha"

                            CreaObjetoFecha()

                        Case "Numérico", "Númerico"

                            CreaObjetoNumero()

                        Case Else
                            Exit Select

                    End Select

                End If

            Else
                lIndex = pBeProductoParametroList.FindIndex(Function(b) b.IdProductoParametro = IdProductoParametro)
            End If

            lIndex = pBeProductoParametroList.FindIndex(Function(b) b.IdProductoParametro = IdProductoParametro)

            If lIndex > -1 Then

                cmdSaveP.Tag = pBeProductoParametroList(lIndex).IdProductoParametro

                If IngresoPorGridAParametro Then
                    cmbParametro.EditValue = pBeProductoParametroList(lIndex).IdParametro
                End If

                Select Case pBeProductoParametroList(lIndex).TipoParametro.Tipo

                    Case "Lógico"

                        CreaObjetoLogico()
                        pLogico.Checked = pBeProductoParametroList(lIndex).Valor_logico

                    Case "Texto"

                        CreaObjetoTexto()
                        pTexto.Text = pBeProductoParametroList(lIndex).Valor_texto

                    Case "Fecha"

                        CreaObjetoFecha()
                        pFecha.Value = pBeProductoParametroList(lIndex).Valor_fecha

                    Case "Numérico", "Númerico"

                        CreaObjetoNumero()
                        pNumero.Value = pBeProductoParametroList(lIndex).Valor_numerico

                    Case Else
                        Exit Select

                End Select

                txtTipo.Text = pBeProductoParametroList(lIndex).TipoParametro.Tipo

                If pBeProductoParametroList(lIndex).Capturar_siempre = False Then
                    rdCapturarUnaVez.Checked = True
                ElseIf pBeProductoParametroList(lIndex).Capturar_siempre = True Then
                    rdCapturarSiempre.Checked = True
                End If

                chkActivarParametro.Checked = pBeProductoParametroList(lIndex).Activo

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            'IngresoPorGridAParametro =False
        End Try

    End Sub

    Private Sub DgridParametros_DoubleClick(sender As Object, e As EventArgs) Handles DgridParametros.DoubleClick

        Try

            If GridViewP.RowCount > 0 Then

                Dim Dr As DataRowView = GridViewP.GetFocusedRow
                IngresoPorGridAParametro = True
                Muestra_Objeto_Personalizado(Dr("Código"))

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IngresoPorGridAParametro = False
        End Try

    End Sub

    Private pObjCodigoBarra As New clsBeProducto_codigos_barra

    Private Sub GrdCodigoBarra_DoubleClick(sender As Object, e As EventArgs) Handles GrdCodigoBarra.DoubleClick

        Try

            If GridViewCB.RowCount > 0 Then

                Dim Dr As DataRowView = GridViewCB.GetFocusedRow

                pObjCodigoBarra = pBeProductoCodigosBarraList.Find(Function(b) b.IdProductoCodigoBarra = CInt(Dr.Item("Código")))

                If pObjCodigoBarra IsNot Nothing AndAlso pObjCodigoBarra.IdProductoCodigoBarra > 0 Then

                    cmdSaveC.Tag = pObjCodigoBarra.IdProductoCodigoBarra
                    cmbProveedor.EditValue = pObjCodigoBarra.IdProveedor
                    txtCodigoBarraL.Text = pObjCodigoBarra.Codigo_barra
                    chkActivarCB.Checked = pObjCodigoBarra.Activo

                    txtCodigoBarraL.Focus()

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

    Private Sub txtTolerancia_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTolerancia.KeyPress

        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If

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

            If cmbSymbology.EditValue = 0 AndAlso cmbSymbology.EditValue <> 0 Then
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

    'Private Sub cmdImprimeCodigoBarra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimeCodigoBarra.ItemClick

    '    Try

    '        If cmbSymbology.ItemIndex = -1 Then
    '            XtraMessageBox.Show("Seleccione Tipo de Código de Barra", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else

    '            If cmbEtiqueta.ItemIndex > -1 Then

    '                Obj = New clsBeTipo_etiqueta() With {.IdTipoEtiqueta = cmbEtiqueta.EditValue}
    '                clsLnTipo_etiqueta.Obtener(Obj)

    '                Dim report As New XtraReport() With {.pBarCodeText = txtCodigoBarra.Text.Trim(), .psymbol = Bcc.Symbology}
    '                report.Margins = New Margins(Obj.MargenIzq * 2.54 * 100, Obj.MagenDer * 2.54 * 100, Obj.MargenSup * 2.54 * 100, Obj.MargenInf * 2.54 * 100)
    '                report.PaperKind = PaperKind.Custom
    '                report.PageWidth = Obj.Ancho * 2.54 * 100
    '                report.PageHeight = Obj.Alto * 2.54 * 100
    '                report.AltoBarra = report.PageHeight - report.Margins.Top - report.Margins.Bottom
    '                report.AnchoBarra = report.PageWidth - report.Margins.Left - report.Margins.Right
    '                'report.PageWidth = 1016
    '                'report.PageHeight = 508
    '                report.ShowPreview()

    '            Else
    '                XtraMessageBox.Show("No está definido el tipo de etiqueta para impresión de código de barra", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If

    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    'End Sub

    Private Sub cmdImprimeCodigoBarra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimeCodigoBarra.ItemClick

        Try

            cmdImprimeCodigoBarra.Enabled = False

            Dim pd As PrintDialog = New PrintDialog()
            pd.PrinterSettings = New PrinterSettings()

            If DialogResult.OK = pd.ShowDialog(Me) Then

                If TabDatos.SelectedTabPageIndex = 0 Then
                    Imprimir_Etiqueta(pBeProducto, pBeProducto.Codigo_barra, pd.PrinterSettings.PrinterName)
                ElseIf TabDatos.SelectedTabPageIndex = 6 Then
                    Imprimir_Etiqueta(pBeProducto, pObjProductoPresentacion.Codigo_barra, pd.PrinterSettings.PrinterName)
                End If

            End If

            cmdImprimeCodigoBarra.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            cmdImprimeCodigoBarra.Enabled = True
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        cmdActualizar.Enabled = False

        If Actualizar_Producto() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarProductos.Invoke
        End If

        cmdActualizar.Enabled = True

    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarProducto.ItemClick

        Try

            mnuEliminarProducto.Enabled = False

            Eliminar_Producto()

            mnuEliminarProducto.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Desactivar_Producto()

        Try

            If Not pBeProducto.Activo Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                If clsLnProducto.Existe_Stock_By_IdProducto(pBeProducto.IdProducto) Then
                    XtraMessageBox.Show("El producto tiene existencias activas, no se puede eliminar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else

                    If XtraMessageBox.Show("¿Desactivar el producto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        clsLnProducto.Desactivar(pBeProducto)
                        If Not InvokeListarProductos Is Nothing Then InvokeListarProductos.Invoke
                        Close()
                    End If

                End If

            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then
                TablasRelacionadas("producto", pBeProducto.IdProducto)
            Else
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Private Sub Eliminar_Producto()

        Try

            If clsLnProducto.Existe_Stock_By_IdProducto(pBeProducto.IdProducto) Then
                XtraMessageBox.Show("El producto tiene existencias activas, no se puede eliminar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                If XtraMessageBox.Show("¿Eliminar el producto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim vRegistrosEliminados As Integer = clsLnProducto.Eliminar_Transaccion(pBeProducto)

                    If Not vRegistrosEliminados = 0 Then

                        XtraMessageBox.Show("Se eliminó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If Not InvokeListarProductos Is Nothing Then InvokeListarProductos.Invoke

                        Close()

                    End If

                End If

            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then
                TablasRelacionadas("producto", pBeProducto.IdProducto)
            Else
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Public pObjProductoPresentacion As New clsBeProducto_Presentacion

    Private Sub GridPresentacion_DoubleClick(sender As Object, e As EventArgs) Handles dGridPresentacion.DoubleClick

        Try

            If GrdPresentacion.RowCount > 0 Then

                Dim Dr As DataRowView = GrdPresentacion.GetFocusedRow

                Dim lIndex As Integer = -1
                lIndex = pBeProductoPresentacionList.FindIndex(Function(b) b.IdPresentacion = Dr.Item("Código"))

                If lIndex > -1 Then

                    pObjProductoPresentacion = pBeProductoPresentacionList.Find(Function(b) b.IdPresentacion = Dr.Item("Código"))

                    cmdSavePR.Tag = pBeProductoPresentacionList(lIndex).IdPresentacion
                    txtNombrePresentacion.Text = pBeProductoPresentacionList(lIndex).Nombre
                    txtCodigoBarraPresentacion.Text = pBeProductoPresentacionList(lIndex).Codigo_barra
                    txtFactor.Value = pBeProductoPresentacionList(lIndex).Factor
                    txtPeso.Value = pBeProductoPresentacionList(lIndex).Peso
                    txtAlto.Value = pBeProductoPresentacionList(lIndex).Alto
                    txtLargo.Value = pBeProductoPresentacionList(lIndex).Largo
                    txtAncho.Value = pBeProductoPresentacionList(lIndex).Ancho
                    txtMinimoExistencia.Value = pBeProductoPresentacionList(lIndex).MinimoExistencia
                    txtMaximoExistencia.Value = pBeProductoPresentacionList(lIndex).MaximoExistencia
                    txtMinimoPeso.Value = pBeProductoPresentacionList(lIndex).MinimoPeso
                    txtMaximoPeso.Value = pBeProductoPresentacionList(lIndex).MaximoPeso
                    chkImprimeBarra.Checked = pBeProductoPresentacionList(lIndex).Imprime_barra
                    chkActivarPR.Checked = pBeProductoPresentacionList(lIndex).Activo
                    ChkEsPallet.Checked = pBeProductoPresentacionList(lIndex).EsPallet
                    chkGeneraLicAutoP.Checked = pBeProductoPresentacionList(lIndex).Genera_lp_auto
                    chkPermitirPaletizar.Checked = pBeProductoPresentacionList(lIndex).Permitir_paletizar
                    txtCajasPorCama.Value = pBeProductoPresentacionList(lIndex).CajasPorCama
                    txtCamasPorTarima.Value = pBeProductoPresentacionList(lIndex).CamasPorTarima
                    chkSistema.Checked = pBeProductoPresentacionList(lIndex).Sistema
                    chkGeneraLPAuto.Checked = pBeProductoPresentacionList(lIndex).Genera_lp_auto

                    txtInfo.Text = "Equivalencia en U.M. Bas" & vbNewLine
                    txtInfo.Text += String.Format("{0} * 1  = {1} {2}", pBeProductoPresentacionList(lIndex).Factor, pBeProductoPresentacionList(lIndex).Factor, lcmbUnidadMedidaBasica.Text.Trim())
                    txtInfo.Text += vbNewLine

                    If ChkEsPallet.Checked OrElse chkPermitirPaletizar.Checked Then
                        Dim vUmBas As Double = pBeProductoPresentacionList(lIndex).Factor * pBeProductoPresentacionList(lIndex).CajasPorCama * pBeProductoPresentacionList(lIndex).CamasPorTarima
                        txtInfo.Text += vbNewLine
                        txtInfo.Text += "Equivalencia Pallet en U.M. Bas " & vbNewLine
                        txtInfo.Text += String.Format("{0} * {1} * {2}  = {3} {4}", pBeProductoPresentacionList(lIndex).Factor, pBeProductoPresentacionList(lIndex).CajasPorCama,
                                                      pBeProductoPresentacionList(lIndex).CamasPorTarima,
                                                      vUmBas,
                                                      lcmbUnidadMedidaBasica.Text.Trim())

                        Llena_Combo_Presentaciones_Pallet()
                        cmbPresentacionPallet.EditValue = pBeProductoPresentacionList(lIndex).IdPresentacionPallet
                    End If

                    txtNombrePresentacion.Focus()

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

    Private Sub txtCicloVida_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCicloVida.KeyPress
        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private Sub lnkProductoR_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkProductoR.LinkClicked

        Try

            Dim Producto As New frmProductoList()
            Producto.cmdImportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Producto.chkActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            If pBeProducto.IdProducto <> Nothing AndAlso pBeProducto.IdProducto <> 0 Then
                Producto.pIdProductoExcepto = pBeProducto.IdProducto
                Producto.pIdPropietario = CInt(lcmbPropietario.EditValue)
            End If
            Producto.Modo = frmProductoList.pModo.Seleccion

            If OpcionesMenu IsNot Nothing Then
                Producto.OpcionesMenu = OpcionesMenu
                Producto.mnuActualizar.Enabled = OpcionesMenu.Leer
                Producto.mnuNuevo.Enabled = OpcionesMenu.Modificar
            End If

            Producto.ShowDialog()

            If Producto.pObjProducto IsNot Nothing AndAlso Producto.pObjProducto.IdProducto <> 0 Then
                txtIdProductoR.Text = Producto.pObjProducto.IdProducto
                txtNombrePR.Text = Producto.pObjProducto.Nombre
                CargaComboPresentacion(Producto.pObjProducto.IdProducto)
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

    Private Sub txtIdProductoR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdProductoR.KeyPress
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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdProductoR.Text.Length = 1 Then
                txtNombrePR.Text = String.Empty
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

    Private Sub txtIdProductoR_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdProductoR.PreviewKeyDown
        Try
            If e.KeyData = Keys.Tab Then
                If String.IsNullOrEmpty(txtIdProductoR.Text.Trim()) = False Then
                    If txtIdProductoR.Text > "0" Then
                        Dim Obj As New clsBeProducto
                        Obj = clsLnProducto.Get_Single_By_IdProducto(txtIdProductoR.Text.Trim())
                        If Obj IsNot Nothing AndAlso Obj.IdProducto > 0 Then
                            txtNombrePR.Text = Obj.Nombre
                            CargaComboPresentacion(Obj.IdProducto)
                        Else
                            XtraMessageBox.Show(String.Format("No Existe Producto con código {0}", txtIdProductoR.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtNombrePR.Text = String.Empty
                            txtIdProductoR.Focus() : txtIdProductoR.SelectAll()
                        End If
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

    Private pObjProductoSustituto As New clsBeProducto_sustituto

    Private Sub GrdProductoS_DoubleClick(sender As Object, e As EventArgs) Handles GrdProductoS.DoubleClick

        Try

            If GridViewProductoS.RowCount > 0 Then

                Dim Dr As DataRowView = GridViewProductoS.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pBeProductoSustitutoList.FindIndex(Function(b) b.IdProductoSustituto = Dr.Item("Código"))

                If lIndex > -1 Then

                    pObjProductoSustituto = pBeProductoSustitutoList.Find(Function(b) b.IdProductoSustituto = Dr.Item("Código"))

                    cmdSavePS.Tag = pBeProductoSustitutoList(lIndex).IdProductoSustituto
                    cmbProductoP.EditValue = pBeProductoSustitutoList(lIndex).IdProductoPresentacionOriginal
                    CargaComboPresentacion(pBeProductoSustitutoList(lIndex).IdProductoReemplazo)
                    cmbPresentacionR.EditValue = pBeProductoSustitutoList(lIndex).IdProductoPresentacionReemplazo
                    txtIdProductoR.Text = pBeProductoSustitutoList(lIndex).IdProductoReemplazo
                    txtNombrePR.Text = pBeProductoSustitutoList(lIndex).ProductoReemplazo.Nombre
                    chkActivarPS.Checked = pBeProductoSustitutoList(lIndex).ProductoReemplazo.Activo

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

    Private pObjProdPresConv As New clsBeProducto_presentaciones_conversiones

    Private Sub GridControl1_DoubleClick(sender As Object, e As EventArgs) Handles GridControl1.DoubleClick

        Try

            If GridView1.RowCount > 0 Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pBeProductoPresConvList.FindIndex(Function(b) b.IdConversion = Dr.Item("IdConversion"))

                If lIndex > -1 Then

                    pObjProdPresConv = pBeProductoPresConvList.Find(Function(b) b.IdConversion = Dr.Item("IdConversion"))

                    cmdSaveCn.Tag = pBeProductoPresConvList(lIndex).IdConversion
                    cmbOriginal.EditValue = pBeProductoPresConvList(lIndex).IdPresentacionOrigen
                    cmbInversa.EditValue = pBeProductoPresConvList(lIndex).IdPresentacionDestino
                    txtFactorConver.Text = pBeProductoPresConvList(lIndex).Factor
                    chkActivarConver.Checked = pBeProductoPresConvList(lIndex).Activo

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

    Private Sub cmdNuevoParametro_Click(sender As Object, e As EventArgs) Handles cmdNuevoParametro.Click
        Try
            Dim Parametro As New frmParametro(frmParametro.TipoTrans.Nuevo)
            Parametro.ShowDialog()
            Parametro.Dispose()
            IMS.Listar_Parametro(cmbParametro)
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Overloads Shared Function Listar_Proveedor(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Proveedor = False

        Dim DT As New DataTable

        Try

            DT.Clear()
            DT = clsLnProveedor.Get_All_For_Combo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdProveedor"
                Cmb.Properties.DataSource = DT
            End If

            Listar_Proveedor = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Private DTOperadores As DataTable

    Private Sub Listar_Operador_Por_Defecto()

        Try

            DTOperadores = clsLnOperador.GetAllForCombo()
            cmbOperadorDefecto.Properties.DataSource = DTOperadores
            cmbOperadorDefecto.Properties.DisplayMember = "Nombre"
            cmbOperadorDefecto.Properties.ValueMember = "IdOperador"

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim BeEmpresa As New clsBeEmpresa() With {.IdEmpresa = AP.IdEmpresa}

        Try

            IMS.Listar_Propietarios_By_IdEmpresa(lcmbPropietario, AP.IdEmpresa)
            IMS.Listar_TipoRotacion(cmbTipoRotacion)
            IMS.Listar_PerfilSerializado(cmbPerfilSerializado)
            IMS.Listar_Parametro(cmbParametro)
            IMS.Listar_Camara(cmbCamara)
            IMS.Listar_IndiceRotacion(cmbIndiceRotacion)
            IMS.Listar_Aranceles(cmbArancel)
            IMS.Listar_Tipos_Manufactura_Ligera(cmbTipoManufactura)

            Listar_Operador_Por_Defecto()
            ImplementarBarra()
            CargaSimbolosCodigoBarra()
            CargaComboEtiquetas()

            clsLnEmpresa.Obtener(BeEmpresa)

            '#EJC20220326A: Cambio a lookupedit unidad de medida.
            IMS.Listar_Unidades_Medida(lcmbUnidadMedidaBasica)

            '#EJC20220326B: Cambio a lookupedit clasificación.
            IMS.Listar_Clasificaciones(lcmbClasificacion, lcmbPropietario.EditValue)

            '#EJC20220326C: Cambio a lookupedit familia.
            IMS.Listar_Familias(lcmbFamilia, lcmbPropietario.EditValue)

            '#EJC20220326C: Cambio a lookupedit Marca.
            IMS.Listar_Marcas_By_IdPropietario(lcmbMarca, lcmbPropietario.EditValue)

            '#CKFK20220721: Cambio a lookupedit TipoProducto.
            IMS.Listar_TipoProducto_By_IdPropietario(txtIdTipoProducto, lcmbPropietario.EditValue)

            '#EJC20220326A: Cambio a lookupedit unidad de medida de cobro.
            '#EJC20220326A: Envio el propietario especifico y true para que solo muestre las unidades configuradas para cobranza.
            '#GT02102024: No cargar porque no se sabe quien es el propietario, hacerlo en el case nuevo
            'IMS.Listar_Unidades_Medida_Es_Um_Cobro(lcmbUnidadMedidaCobro, 0, True)

            '#EJC20220616:Listar bodegas by usuario en reabasto.
            AP.Listar_Bodegas_By_Usuario(cmbBodegaRellenado)

            '#GT02092022:Listar parametro A
            IMS.Listar_ParametrosA(lcmbParametroA)

            '#GT02092022:Listar parametro B
            IMS.Listar_ParametrosB(lcmbParametroB)

            'GT07072022: se valida si mostrar parámetros
            Dim Bodega As New clsBeBodega
            Bodega.IdBodega = AP.IdBodega
            clsLnBodega.GetSingle(Bodega)

            If Bodega.industria_motriz Then
                lnkParametroA.Visible = True
                lnkParametroB.Visible = True
                lcmbParametroA.Visible = True
                lcmbParametroB.Visible = True
                lcmbParametroA.Enabled = True
                lcmbParametroB.Enabled = True
            Else
                lnkParametroA.Visible = False
                lnkParametroB.Visible = False
                lcmbParametroA.Visible = False
                lcmbParametroB.Visible = False
                lcmbParametroA.Enabled = False
                lcmbParametroB.Enabled = False
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        Try

            pListObjTP = clsBD.GetLongitudByTabla("producto")
            pListObjTPP = clsBD.GetLongitudByTabla("producto_parametros")
            pListObjTPC = clsBD.GetLongitudByTabla("producto_codigos_barra")
            pListObjTPR = clsBD.GetLongitudByTabla("producto_presentacion")

            '#GT19012024: desde la lista, ya carga presentaciones, ahorra un query
            If pBeProducto.Presentaciones.Count > 0 Then
                pBeProductoPresentacionList = pBeProducto.Presentaciones
            Else
                pBeProductoPresentacionList = clsLnProducto.Get_All_Presentacion_By_IdProducto(pBeProducto.IdProducto,
                                                                                               AP.IdBodega)
            End If

            pBeproductoRellenadoList = clsLnProducto_rellenado.Get_All_By_IdProducto(pBeProducto.IdProducto, True)
            pBePresentacionTarimaList = clsLnProducto_presentacion_tarima.Get_All_By_IdProducto(pBeProducto.IdProducto)

            CargarProductoRellenado()

            CargarPresentacionTarima()

            Cargar_Conversion_Presentaciones()

            '#EJC20210503: Habilitar UM de cobro, si hay control por tarifa de servicios.
            lnkUMCobro.Visible = AP.Bodega.Control_Tarifa_Servicios
            lcmbUnidadMedidaCobro.Visible = AP.Bodega.Control_Tarifa_Servicios


            '#EJC20220830_1021: Tomar configuración de interface para genera LP
            Dim BeConfi As New clsBeI_nav_config_enc
            BeConfi = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(AP.IdBodega,
                                                                                    lcmbPropietario.EditValue)
            If Not BeConfi Is Nothing Then
                chkGeneraLicAutoP.Checked = BeConfi.Genera_lp
            End If

            '#EJC20220830_1043:Lllenar bodegas en objeto paara asociar automáticamente después.
            lBodegas = clsLnBodega.Get_All_By_IdEmpresa_And_IdUsuario(AP.IdEmpresa,
                                                                      AP.UsuarioAp.IdUsuario)

            '#EJC202209280926: Cambiar etiqueta de campos en base a configuración personalizada del cliente.
            Set_Label_Personalizados()

            Dim vRutaCDN As String = clsLnBodega.GetRutaCDN_By_Idbodega(AP.IdBodega)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lcmbParametroA.EditValue = Nothing
                    lcmbParametroB.EditValue = Nothing

                    lblC.Text = clsLnProducto.MaxID()
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    cmdActualizar.Enabled = False
                    mnuEliminarProducto.Enabled = False
                    mnuAsignacion.Enabled = False
                    lcmbPropietario.Enabled = True

                    cmbPerfilSerializado.Enabled = False
                    cmbPerfilSerializado.ItemIndex = -1
                    lcmbClasificacion.Focus()

                    If BeEmpresa.codigo_automatico Then
                        txtCodigo.Text = Correlativo()
                        txtCodigo.ReadOnly = True
                    Else
                        txtCodigo.ReadOnly = False
                        If CodigoNuevoProducto.Length > 0 Then
                            txtCodigo.Text = CodigoNuevoProducto
                            txtCodigoBarra.Text = CodigoNuevoProducto
                            lcmbPropietario.EditValue = IdPropietarioNuevo
                            txtNombre.Focus()
                        End If

                    End If

                    lcmbClasificacion.EditValue = Nothing
                    lcmbFamilia.EditValue = Nothing
                    lcmbMarca.EditValue = Nothing
                    txtIdTipoProducto.EditValue = Nothing
                    lcmbUnidadMedidaCobro.EditValue = Nothing
                    '#GT10042025: limpiar el combo para recargar por el propietario default
                    lcmbUnidadMedidaBasica.EditValue = Nothing
                    lcmbUnidadMedidaBasica.Text = ""
                    lcmbUnidadMedidaBasica.Properties.DataSource = Nothing

                    '#GT30082022: set automatico del tipo etiqueta para cada producto, desde la interfaz de bodega
                    If BeConfi.IdTipoEtiqueta > 0 Then
                        cmbEtiqueta.EditValue = BeConfi.IdTipoEtiqueta
                    Else
                        cmbEtiqueta.EditValue = 0
                    End If

                    '#GT10042025: filtrar umbas para el propietario cargado por defecto
                    'No cambiar, ya que de lo contrario, se listara sin filtro, y se podria asociar la unidad de un propietario que no es del combo
                    IMS.Listar_Unidades_Medida(lcmbUnidadMedidaBasica, lcmbPropietario.EditValue)

                    '#GT10042025: filtrar umbas cobro para el propietario cargado por defecto
                    IMS.Listar_Unidades_Medida_Es_Um_Cobro(lcmbUnidadMedidaCobro, lcmbPropietario.EditValue, True)

                    '#GT02102024: aqui se cargan las unidades de cobro, porque previamente hay cargas de propietarioy eso ejecuta esta consulta 
                    IMS.Listar_Unidades_Medida_Es_Um_Cobro(lcmbUnidadMedidaCobro, lcmbPropietario.EditValue, True)

                Case TipoTrans.Editar

                    '#GT10042025: cargar umbas solo para el propietario guardado
                    IMS.Listar_Unidades_Medida(lcmbUnidadMedidaBasica, pBeProducto.Propietario.IdPropietario)

                    pProducto = pBeProducto.Clone()

                    Cargar_Producto_Presentacion()

                    Cargar_Producto()

                    pBeProductoParametroList = clsLnProducto.Get_All_Parametro_By_IdProducto(pBeProducto.IdProducto)

                    Cargar_Parametros()

                    pBeProductoCodigosBarraList = clsLnProducto.Get_All_Codigos_Barra_By_IdProducto(pBeProducto.IdProducto)

                    Cargar_Codigos_Barra()

                    pBeProductoSustitutoList = clsLnProducto.Get_All_Producto_Sustituto_By_IdProductoOriginal(pBeProducto.IdProducto)

                    Cargar_Producto_Sustituto()

                    pBeProductoPresConvList = clsLnProducto.Get_All_Conversion_Presentacion_By_IdProducto(pBeProducto.IdProducto)

                    Cargar_Conversion_Presentaciones()

                    '#GT02102024: aqui se cargan las unidades de cobro, porque previamente hay cargas de propietarioy eso ejecuta esta consulta 
                    IMS.Listar_Unidades_Medida_Es_Um_Cobro(lcmbUnidadMedidaCobro, pBeProducto.Propietario.IdPropietario, True)

                    Cargar_Datos_Stock()

                    If pBeProducto.Kit Then
                        tabProductoKit.PageVisible = True
                        clsLnProducto_kit_composicion.Get_All_By_IdProducto_And_IdBodega(pBeProducto.IdProducto, AP.IdBodega, pBeProductoKitList)
                        CargarProductoKit()
                    End If

                    mnuGuardar.Enabled = False

                    cmdActualizar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuAsignacion.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuDesactivar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuEliminarProducto.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)

                    txtCodigo.Focus()

                    If BeEmpresa.codigo_automatico Then
                        txtCodigo.ReadOnly = True
                    Else
                        txtCodigo.ReadOnly = False
                    End If

                    '#GT30082022: set de la etiqueta si se guardo un tipo
                    If pBeProducto.IdTipoEtiqueta > 0 Then
                        cmbEtiqueta.EditValue = pBeProducto.IdTipoEtiqueta
                    Else
                        cmbEtiqueta.EditValue = 0
                    End If

                    If Not String.IsNullOrEmpty(vRutaCDN) Then
                        Cargar_Talla_Color_Con_Imagen(pBeProducto.IdProducto, vRutaCDN)
                    Else
                        Cargar_Talla_Color(pBeProducto.IdProducto)
                    End If

                Case TipoTrans.Consulta

                    pBeProducto = clsLnProducto.Get_Single_By_IdProducto(pBeProducto.IdProducto)

                    Cargar_Producto()

                    pBeProductoParametroList = clsLnProducto.Get_All_Parametro_By_IdProducto(pBeProducto.IdProducto)

                    Cargar_Parametros()

                    pBeProductoCodigosBarraList = clsLnProducto.Get_All_Codigos_Barra_By_IdProducto(pBeProducto.IdProducto)

                    Cargar_Codigos_Barra()

                    pBeProductoPresentacionList = clsLnProducto.Get_All_Presentacion_By_IdProducto(pBeProducto.IdProducto, AP.IdBodega)

                    Cargar_Producto_Presentacion()

                    pBeProductoSustitutoList = clsLnProducto.Get_All_Producto_Sustituto_By_IdProductoOriginal(pBeProducto.IdProducto)

                    Cargar_Producto_Sustituto()

                    listaStock = clsLnStock.Get_All_Stock_By_IdProducto(pBeProducto.IdProducto)

                    Cargar_Datos_Stock()

                    If pBeProducto.Kit Then
                        tabProductoKit.PageVisible = True
                        clsLnProducto_kit_composicion.Get_All_By_IdProducto_And_IdBodega(pBeProducto.IdProducto, AP.IdBodega, pBeProductoKitList)
                        CargarProductoKit()
                    End If

                    mnuGuardar.Enabled = False
                    cmdActualizar.Enabled = False
                    mnuEliminarProducto.Enabled = False
                    mnuAsignacion.Enabled = False
                    txtCodigo.Focus()

                    GrpPresentacion.Enabled = False
                    GrpProducto.Enabled = False
                    GrpPeso.Enabled = False
                    GrpProductoReeemplazo.Enabled = False
                    GrpParametro.Enabled = False
                    GrpAtributo.Enabled = False
                    GprParametro.Enabled = False

                    TabDatos.SelectedTabPage = TabAtributo

                    If BeEmpresa.codigo_automatico Then
                        txtCodigo.ReadOnly = True
                    Else
                        txtCodigo.ReadOnly = False
                    End If

                    '#GT30082022: set de la etiqueta si se guardo un tipo
                    If pBeProducto.IdTipoEtiqueta > 0 Then
                        cmbEtiqueta.EditValue = pBeProducto.IdTipoEtiqueta
                    Else
                        cmbEtiqueta.EditValue = 0
                    End If

                    If Not String.IsNullOrEmpty(vRutaCDN) Then
                        Cargar_Talla_Color_Con_Imagen(pBeProducto.IdProducto, vRutaCDN)
                    Else
                        Cargar_Talla_Color(pBeProducto.IdProducto)
                    End If

            End Select

            ValidaBodegas()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

        txtCodigo.Focus()

        Application.DoEvents()

    End Sub

    Private Sub Set_Label_Personalizados()

        Try

            Dim BeConfiguracion As New clsBeConfiguracion_alias_campos

            If Not lConfiguracionAliasCampos Is Nothing Then

                If lConfiguracionAliasCampos.Count > 0 Then

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_a")

                    If Not BeConfiguracion Is Nothing Then
                        lnkParametroA.Text = BeConfiguracion.Alias_WMS
                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "parametro_b")

                    If Not BeConfiguracion Is Nothing Then
                        lnkParametroB.Text = BeConfiguracion.Alias_WMS
                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "familia")

                    If Not BeConfiguracion Is Nothing Then
                        lnkFamilia.Text = BeConfiguracion.Alias_WMS
                    End If

                    BeConfiguracion = lConfiguracionAliasCampos.Find(Function(x) x.Nombre_WMS.ToLower = "clasificacion")

                    If Not BeConfiguracion Is Nothing Then
                        lnkClasificacion.Text = BeConfiguracion.Alias_WMS
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
    Private Function Correlativo() As String

        Correlativo = "P"

        Try

            Dim MaxID As String = clsLnProducto.MaxID.ToString
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
            Return "P0"
        End Try

    End Function

    Private Sub chkActivoParametro_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivoParametro.CheckedChanged
        Cargar_Parametros()
    End Sub

    Private Sub chkActivoCB_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivoCB.CheckedChanged
        Cargar_Codigos_Barra()
    End Sub

    Private Sub chkActivoPS_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivoPS.CheckedChanged
        ListarProductoSustituto()
    End Sub

    Private Sub lnkUbicacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacion.LinkClicked

        Try

            Dim Ubicacion As New frmBodegaUbicacion_List() _
            With {
            .pUbicacionPicking = True,
            .pIdBodega = cmbBodegaRellenado.EditValue,
            .Modo = frmBodegaUbicacion_List.pModo.Seleccion
            }

            If OpcionesMenu IsNot Nothing Then
                Ubicacion.OpcionesMenu = OpcionesMenu
                Ubicacion.mnuNuevo.Enabled = OpcionesMenu.Modificar
                Ubicacion.mmuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Ubicacion.ShowDialog()

            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacion.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacion.Text = Ubicacion.pObj.NombreCompleto
            End If

            Ubicacion.Close() : Ubicacion.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacion.KeyPress

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

    End Sub

    Private pObjProductoRellenado As New clsBeProducto_rellenado

    Private Sub GridProductoRellenado_DoubleClick(sender As Object, e As EventArgs) Handles GridProductoRellenado.DoubleClick

        Try

            If ViewProductoRellenado.RowCount > 0 Then

                Dim Dr As DataRowView = ViewProductoRellenado.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pBeproductoRellenadoList.FindIndex(Function(b) b.IdRellenado = Dr.Item("IdRellenado"))

                If lIndex > -1 Then

                    pObjProductoRellenado = pBeproductoRellenadoList.Find(Function(b) b.IdRellenado = Dr.Item("IdRellenado"))

                    '#EJC20210301
                    lblIdRellenado.Text = pBeproductoRellenadoList(lIndex).IdRellenado
                    lblIdRellenado.Visible = True

                    cmdSavePRL.Tag = pBeproductoRellenadoList(lIndex).IdRellenado
                    cmbPresentacionPR.EditValue = pBeproductoRellenadoList(lIndex).IdPresentacion
                    cmbProductoEstado.EditValue = pBeproductoRellenadoList(lIndex).IdProductoEstado
                    txtIdUbicacion.Text = pBeproductoRellenadoList(lIndex).IdUbicacion
                    txtNombreUbicacion.Text = pBeproductoRellenadoList(lIndex).Ubicacion
                    txtMinimoPicking.Value = pBeproductoRellenadoList(lIndex).Minimo
                    txtMaximoPicking.Value = pBeproductoRellenadoList(lIndex).Maximo

                    If pBeproductoRellenadoList(lIndex).IdTipoAccion = 1 Then
                        optNotificar.Checked = True
                        optGenerarAutomaticamente.Checked = False
                    ElseIf pBeproductoRellenadoList(lIndex).IdTipoAccion = 2 Then
                        optNotificar.Checked = False
                        optGenerarAutomaticamente.Checked = True
                    End If

                    chkActivarProductoPRL.Checked = pBeproductoRellenadoList(lIndex).Activo

                    If pBeproductoRellenadoList(lIndex).IdPresentacionAbastercerCon <> 0 Then
                        cmbPresentacionAbastecerCon.EditValue = pBeproductoRellenadoList(lIndex).IdPresentacionAbastercerCon
                    End If

                    If pBeproductoRellenadoList(lIndex).IdOperadorDefecto <> 0 Then
                        cmbOperadorDefecto.EditValue = pBeproductoRellenadoList(lIndex).IdOperadorDefecto
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

    Private pObjPresentacionTarima As New clsBeProducto_presentacion_tarima

    Private Sub GridPresentacionTarima_DoubleClick(sender As Object, e As EventArgs) Handles GridPresentacionTarima.DoubleClick

        Try

            If ViewPT.RowCount > 0 Then

                Dim Dr As DataRowView = ViewPT.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pBePresentacionTarimaList.FindIndex(Function(b) b.IdPresentacionTarima = Dr.Item("IdPresentacionTarima"))

                If lIndex > -1 Then

                    pObjPresentacionTarima = pBePresentacionTarimaList.Find(Function(b) b.IdPresentacionTarima = Dr.Item("IdPresentacionTarima"))

                    cmdSavePT.Tag = pBePresentacionTarimaList(lIndex).IdPresentacionTarima
                    cmbPresentacionTarima.EditValue = pBePresentacionTarimaList(lIndex).IdPresentacion
                    cmbTipoTarima.EditValue = pBePresentacionTarimaList(lIndex).IdTipoTarima
                    txtCantidad.Value = pBePresentacionTarimaList(lIndex).Cantidad
                    txtCantidadPorCama.Value = pBePresentacionTarimaList(lIndex).CantidadPorCama
                    chkActivoPT2.Checked = pBePresentacionTarimaList(lIndex).Activo

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

    Private Sub chkProductoPRL_CheckedChanged(sender As Object, e As EventArgs) Handles chkProductoPRL.CheckedChanged
        ListarProductoRellenado()
    End Sub

    Private Sub chkActivoPT_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivoPT.CheckedChanged
        ListarPresentacionTarima()
    End Sub

    'Tab stock 
    'Private Sub ListarStock()

    '    Try

    '        GridStock.DataSource = Nothing

    '        Dim lista As New List(Of clsBeVW_stock_res)

    '        lista = clsLnStock.GetAllByBP(pBeProducto.IdProductoBodega).ToList

    '        If lista IsNot Nothing AndAlso lista.Count > 0 Then

    '            Dim Dt As New DataTable("Stock")
    '            Dt.Columns.Add(("IdStock"), GetType(Integer))
    '            Dt.Columns.Add(("IdProductoBodega"), GetType(Integer))
    '            Dt.Columns.Add(("IdUbicacionActual"), GetType(Integer))
    '            Dt.Columns.Add(("Nombre"), GetType(String))
    '            Dt.Columns.Add(("UnidadMedida"), GetType(String))
    '            Dt.Columns.Add(("Añada"), GetType(Integer))
    '            Dt.Columns.Add(("Presentacion"), GetType(String))
    '            Dt.Columns.Add(("Ingreso"), GetType(Date))
    '            Dt.Columns.Add(("Lote"), GetType(String))
    '            Dt.Columns.Add(("Cantidad"), GetType(Double))
    '            Dt.Columns.Add(("Factor"), GetType(Integer))

    '            For Each Obj As clsBeVW_stock_res In lista
    '                Dt.Rows.Add(Obj.IdStock, Obj.IdProductoBodega, Obj.IdUbicacionActual, Obj.Nombre, Obj.UnidadMedida, Obj.Añada, Obj.Presentacion, Obj.Fecha_ingreso, Obj.Lote, Obj.CantidadPresentacion, Obj.Factor)
    '            Next
    '            GridStock.DataSource = Dt

    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try

    'End Sub

    Private Sub GridViewP_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewP.RowStyle

        Try

            GridViewP.OptionsBehavior.Editable = False
            GridViewP.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewP.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridViewP.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewP.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewP.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewP.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewP.Appearance.FocusedRow.ForeColor = Color.White
            GridViewP.Appearance.SelectedRow.ForeColor = Color.White

            GridViewP.Appearance.SelectedRow.Options.UseBackColor = True
            GridViewP.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridViewCB_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewCB.RowStyle

        Try

            GridViewCB.OptionsBehavior.Editable = False
            GridViewCB.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewCB.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridViewCB.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewCB.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewCB.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewCB.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewCB.Appearance.FocusedRow.ForeColor = Color.White
            GridViewCB.Appearance.SelectedRow.ForeColor = Color.White

            GridViewCB.Appearance.SelectedRow.Options.UseBackColor = True
            GridViewCB.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GrdProductoBodega_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GrdProductoBodega.RowStyle

        Try

            GrdProductoBodega.OptionsBehavior.Editable = True
            GrdProductoBodega.OptionsSelection.EnableAppearanceFocusedCell = True

            GrdProductoBodega.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GrdProductoBodega.OptionsSelection.EnableAppearanceFocusedRow = True
            GrdProductoBodega.OptionsSelection.EnableAppearanceHideSelection = True
            GrdProductoBodega.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GrdProductoBodega.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GrdProductoBodega.Appearance.FocusedRow.ForeColor = Color.White
            GrdProductoBodega.Appearance.SelectedRow.ForeColor = Color.White

            GrdProductoBodega.Appearance.SelectedRow.Options.UseBackColor = True
            GrdProductoBodega.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GrdPresentacion_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GrdPresentacion.RowStyle

        Try

            GrdPresentacion.OptionsBehavior.Editable = False
            GrdPresentacion.OptionsSelection.EnableAppearanceFocusedCell = False

            GrdPresentacion.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GrdPresentacion.OptionsSelection.EnableAppearanceFocusedRow = True
            GrdPresentacion.OptionsSelection.EnableAppearanceHideSelection = True
            GrdPresentacion.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GrdPresentacion.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GrdPresentacion.Appearance.FocusedRow.ForeColor = Color.White
            GrdPresentacion.Appearance.SelectedRow.ForeColor = Color.White

            GrdPresentacion.Appearance.SelectedRow.Options.UseBackColor = True
            GrdPresentacion.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridViewProductoS_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewProductoS.RowStyle

        Try

            GridViewProductoS.OptionsBehavior.Editable = False
            GridViewProductoS.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewProductoS.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridViewProductoS.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewProductoS.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewProductoS.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewProductoS.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewProductoS.Appearance.FocusedRow.ForeColor = Color.White
            GridViewProductoS.Appearance.SelectedRow.ForeColor = Color.White

            GridViewProductoS.Appearance.SelectedRow.Options.UseBackColor = True
            GridViewProductoS.Appearance.SelectedRow.Options.UseForeColor = True

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
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Private Sub ViewProductoRellenado_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ViewProductoRellenado.RowStyle

        Try

            ViewProductoRellenado.OptionsBehavior.Editable = False
            ViewProductoRellenado.OptionsSelection.EnableAppearanceFocusedCell = False

            ViewProductoRellenado.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            ViewProductoRellenado.OptionsSelection.EnableAppearanceFocusedRow = True
            ViewProductoRellenado.OptionsSelection.EnableAppearanceHideSelection = True
            ViewProductoRellenado.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            ViewProductoRellenado.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            ViewProductoRellenado.Appearance.FocusedRow.ForeColor = Color.White
            ViewProductoRellenado.Appearance.SelectedRow.ForeColor = Color.White

            ViewProductoRellenado.Appearance.SelectedRow.Options.UseBackColor = True
            ViewProductoRellenado.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub ViewPT_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ViewPT.RowStyle

        Try

            ViewPT.OptionsBehavior.Editable = False
            ViewPT.OptionsSelection.EnableAppearanceFocusedCell = False

            ViewPT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            ViewPT.OptionsSelection.EnableAppearanceFocusedRow = True
            ViewPT.OptionsSelection.EnableAppearanceHideSelection = True
            ViewPT.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            ViewPT.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            ViewPT.Appearance.FocusedRow.ForeColor = Color.White
            ViewPT.Appearance.SelectedRow.ForeColor = Color.White

            ViewPT.Appearance.SelectedRow.Options.UseBackColor = True
            ViewPT.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtFactor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFactor.KeyPress

        If e.KeyChar = "-" Then
            e.Handled = True
        End If

    End Sub

    Private Sub cmdSaveP_Click(sender As Object, e As EventArgs) Handles cmdSaveP.Click

        Cursor = Cursors.WaitCursor

        Try

            If cmbParametro.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione un parámetro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Select Case txtTipo.Text

                Case "Númerico"
                    If String.IsNullOrEmpty(pNumero.Text.Trim()) Then
                        XtraMessageBox.Show("Ingrese un Valor Númerico.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        pNumero.Focus()
                        Return
                    End If
                Case "Texto"
                    If String.IsNullOrEmpty(pTexto.Text.Trim()) Then
                        XtraMessageBox.Show("Ingrese un Valor Texto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        pTexto.Focus()
                        Return
                    End If
                Case "Fecha"
                    If pFecha.Value = Nothing Then
                        XtraMessageBox.Show("Seleccione una Fecha.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        pFecha.Focus()
                        Return
                    End If
                Case Else
                    Exit Select
            End Select

            Dim pIndex As Integer = -1

            pIndex = pBeProductoParametroList.FindIndex(Function(b) b.IdProductoParametro = CInt(cmdSaveP.Tag))

            If pIndex > -1 Then

                Select Case txtTipo.Text.Trim
                    Case "Númerico"
                        pBeProductoParametroList(pIndex).Valor_numerico = pNumero.Text
                    Case "Texto"

                        pBeProductoParametroList(pIndex).Valor_texto = pTexto.Text.Trim()

                        If pBeProductoParametroList(pIndex).Valor_texto.Count > pListObjTPP.Find(Function(b) b.NombreCampo.ToUpper = "VALOR_TEXTO").Longitud Then
                            XtraMessageBox.Show(String.Format("El Valor del Texto debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "VALOR_TEXTO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            pTexto.Focus()
                            Return
                        End If

                    Case "Fecha"
                        pBeProductoParametroList(pIndex).Valor_fecha = pFecha.Value
                    Case "Lógico"
                        pBeProductoParametroList(pIndex).Valor_logico = pLogico.Checked
                    Case Else
                        Exit Select
                End Select

                pBeProductoParametroList(pIndex).TipoParametro.Descripcion = cmbParametro.Text
                pBeProductoParametroList(pIndex).TipoParametro.Tipo = txtTipo.Text.Trim

                pBeProductoParametroList(pIndex).IdParametro = cmbParametro.EditValue
                pBeProductoParametroList(pIndex).IdProducto = pBeProducto.IdProducto

                If rdCapturarUnaVez.Checked Then
                    pBeProductoParametroList(pIndex).Capturar_siempre = False
                ElseIf rdCapturarSiempre.Checked Then
                    pBeProductoParametroList(pIndex).Capturar_siempre = True
                End If

                pBeProductoParametroList(pIndex).Activo = chkActivarParametro.Checked
                pBeProductoParametroList(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                pBeProductoParametroList(pIndex).Fec_mod = Now

                cmdSaveP.Tag = Nothing

            Else

                Dim ObjPR As New clsBeProducto_parametros() With {.TipoParametro = New clsBeP_parametro()}

                Select Case txtTipo.Text.Trim
                    Case "Númerico"
                        ObjPR.Valor_numerico = pNumero.Text
                    Case "Texto"
                        ObjPR.Valor_texto = pTexto.Text.Trim()

                        If ObjPR.Valor_texto.Count > pListObjTPP.Find(Function(b) b.NombreCampo.ToUpper = "VALOR_TEXTO").Longitud Then
                            XtraMessageBox.Show(String.Format("El Valor del Texto debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "VALOR_TEXTO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            pTexto.Focus()
                            Return
                        End If

                    Case "Fecha"
                        ObjPR.Valor_fecha = pFecha.Value
                    Case "Lógico"
                        ObjPR.Valor_logico = pLogico.Checked
                    Case Else
                        Exit Select
                End Select

                ObjPR.TipoParametro.Descripcion = cmbParametro.Text
                ObjPR.TipoParametro.Tipo = txtTipo.Text.Trim

                If pBeProductoParametroList IsNot Nothing AndAlso pBeProductoParametroList.Count > 0 Then
                    ObjPR.IdProductoParametro = pBeProductoParametroList.Max(Function(b) b.IdProductoParametro) + 1
                Else
                    ObjPR.IdProductoParametro = 1
                End If

                ObjPR.IdParametro = cmbParametro.EditValue
                ObjPR.IdProducto = pBeProducto.IdProducto

                If rdCapturarUnaVez.Checked Then
                    ObjPR.Capturar_siempre = False
                ElseIf rdCapturarSiempre.Checked Then
                    ObjPR.Capturar_siempre = True
                End If

                ObjPR.Activo = True
                ObjPR.User_agr = AP.UsuarioAp.IdUsuario
                ObjPR.Fec_agr = Now
                ObjPR.User_mod = AP.UsuarioAp.IdUsuario
                ObjPR.Fec_mod = Now
                ObjPR.IsNew = True

                pBeProductoParametroList.Add(ObjPR)

            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Cursor = Cursors.Default
            cmdSaveP.Tag = Nothing
            Cargar_Parametros()
        End Try

    End Sub

    Private Sub cmdNewP_Click(sender As Object, e As EventArgs) Handles cmdNewP.Click

        cmdSaveP.Tag = Nothing
        cmbParametro.ItemIndex = 0
        txtTipo.Text = String.Empty
        rdCapturarUnaVez.Checked = True
        txtTipo.Focus()

    End Sub

    Private Sub cmdSavePR_Click(sender As Object, e As EventArgs) Handles cmdSavePR.Click

        Cursor = Cursors.WaitCursor

        If Datos_Correctos_Presentacion() = False Then
            Cursor = Cursors.Default
            Return
        End If

        Try

            Dim pIndex As Integer = -1

            pIndex = pBeProductoPresentacionList.FindIndex(Function(b) b.IdPresentacion = CInt(cmdSavePR.Tag))

            If pIndex > -1 Then

                If pBeProductoPresentacionList(pIndex).Sistema = 0 Then

                    If pBeProductoPresentacionList(pIndex).Nombre.Count > pListObjTPR.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                        XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Focus()
                        txtNombrePresentacion.Focus()
                        Return
                    End If

                    If pBeProductoPresentacionList(pIndex).Codigo_barra.Count > pListObjTPR.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud Then
                        XtraMessageBox.Show(String.Format("El código de barra debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Focus()
                        txtCodigoBarraPresentacion.Focus()
                        Return
                    End If

                    If ChkEsPallet.Checked OrElse chkPermitirPaletizar.Checked Then
                        If Val(txtCajasPorCama.Value = 0) Then
                            XtraMessageBox.Show("Ingrese cantidad de unidades de manipulación por cama", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Return
                        Else
                            If Val(txtCamasPorTarima.Value = 0) Then
                                XtraMessageBox.Show("Ingrese cantidad de camas por tarima", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Return
                            End If
                        End If
                    End If

                    If Modo = TipoTrans.Nuevo Then
                        Dim lIndexC As Integer = -1

                        lIndexC = pBeProductoPresentacionList.FindIndex(Function(b) b.Codigo_barra = txtCodigoBarraPresentacion.Text.Trim And b.IdPresentacion <> cmdSavePR.Tag)

                        If lIndexC > -1 Then
                            Throw New Exception(String.Format("El código de barra {0} ya existe.", txtCodigoBarraPresentacion.Text.Trim()))
                        ElseIf txtCodigoBarraPresentacion.Text.Trim = txtCodigoBarra.Text.Trim Then
                            Throw New Exception(String.Format("El código de barra {0} ya existe en Producto.", txtCodigoBarraPresentacion.Text.Trim()))
                        End If

                    End If

                    If pBeProductoPresentacionList(pIndex).ExisteStock Then

                        XtraMessageBox.Show("La presentación posee stock, No se actualizarán los campos relativos a factores.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        pBeProductoPresentacionList(pIndex).MinimoExistencia = txtMinimoExistencia.Value
                        pBeProductoPresentacionList(pIndex).MaximoExistencia = txtMaximoExistencia.Value
                        pBeProductoPresentacionList(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                        pBeProductoPresentacionList(pIndex).Fec_mod = Now
                        pBeProductoPresentacionList(pIndex).Nombre = txtNombrePresentacion.Text.Trim()
                        pBeProductoPresentacionList(pIndex).Codigo_barra = txtCodigoBarraPresentacion.Text.Trim()
                        pBeProductoPresentacionList(pIndex).Imprime_barra = chkImprimeBarra.Checked
                        pBeProductoPresentacionList(pIndex).Alto = txtAlto.Value
                        pBeProductoPresentacionList(pIndex).Largo = txtLargo.Value
                        pBeProductoPresentacionList(pIndex).Ancho = txtAncho.Value
                        pBeProductoPresentacionList(pIndex).Genera_lp_auto = chkGeneraLPAuto.Checked
                        pBeProductoPresentacionList(pIndex).Permitir_paletizar = chkPermitirPaletizar.Checked
                        pBeProductoPresentacionList(pIndex).IdTipoEtiqueta = cmbEtiquetaPresentacion.EditValue
                        'Dimensiones Pallet
                        If ChkEsPallet.Checked OrElse chkPermitirPaletizar.Checked Then
                            pBeProductoPresentacionList(pIndex).CajasPorCama = txtCajasPorCama.Value
                            pBeProductoPresentacionList(pIndex).CamasPorTarima = txtCamasPorTarima.Value
                            pBeProductoPresentacionList(pIndex).IdPresentacionPallet = cmbPresentacionPallet.EditValue
                        Else
                            pBeProductoPresentacionList(pIndex).CajasPorCama = 0
                            pBeProductoPresentacionList(pIndex).CamasPorTarima = 0
                        End If

                        Cursor = Cursors.Default

                        'Return

                    End If

                    pBeProductoPresentacionList(pIndex).IdProducto = pBeProducto.IdProducto

                    pBeProductoPresentacionList(pIndex).Nombre = txtNombrePresentacion.Text.Trim()
                    pBeProductoPresentacionList(pIndex).Codigo_barra = txtCodigoBarraPresentacion.Text.Trim()
                    pBeProductoPresentacionList(pIndex).Imprime_barra = chkImprimeBarra.Checked
                    pBeProductoPresentacionList(pIndex).Codigo = txtCodigo.Text.Trim()

                    pBeProductoPresentacionList(pIndex).Factor = txtFactor.Value
                    pBeProductoPresentacionList(pIndex).Peso = txtPeso.Value
                    pBeProductoPresentacionList(pIndex).Alto = txtAlto.Value
                    pBeProductoPresentacionList(pIndex).Largo = txtLargo.Value
                    pBeProductoPresentacionList(pIndex).Ancho = txtAncho.Value

                    pBeProductoPresentacionList(pIndex).MinimoExistencia = txtMinimoExistencia.Value
                    pBeProductoPresentacionList(pIndex).MaximoExistencia = txtMaximoExistencia.Value
                    pBeProductoPresentacionList(pIndex).MinimoPeso = txtMinimoPeso.Value
                    pBeProductoPresentacionList(pIndex).MaximoPeso = txtMaximoPeso.Value

                    pBeProductoPresentacionList(pIndex).Activo = chkActivarPR.Checked
                    pBeProductoPresentacionList(pIndex).Sistema = chkSistema.Checked
                    pBeProductoPresentacionList(pIndex).EsPallet = ChkEsPallet.Checked
                    pBeProductoPresentacionList(pIndex).Genera_lp_auto = chkGeneraLPAuto.Checked
                    pBeProductoPresentacionList(pIndex).Permitir_paletizar = chkPermitirPaletizar.Checked
                    pBeProductoPresentacionList(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pBeProductoPresentacionList(pIndex).Fec_mod = Now
                    pBeProductoPresentacionList(pIndex).IdTipoEtiqueta = cmbEtiquetaPresentacion.EditValue

                    'Dimensiones Pallet
                    If ChkEsPallet.Checked OrElse chkPermitirPaletizar.Checked Then
                        pBeProductoPresentacionList(pIndex).CajasPorCama = txtCajasPorCama.Value
                        pBeProductoPresentacionList(pIndex).CamasPorTarima = txtCamasPorTarima.Value
                        pBeProductoPresentacionList(pIndex).IdPresentacionPallet = cmbPresentacionPallet.EditValue
                    Else
                        pBeProductoPresentacionList(pIndex).CajasPorCama = 0
                        pBeProductoPresentacionList(pIndex).CamasPorTarima = 0
                    End If
                Else
                    XtraMessageBox.Show(String.Format("No se puede actualizar la presentación porque es de sistema"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Else

                Dim ObjN As New clsBeProducto_Presentacion

                If pBeProductoPresentacionList IsNot Nothing AndAlso pBeProductoPresentacionList.Count > 0 Then
                    ObjN.IdPresentacion = pBeProductoPresentacionList.Max(Function(b) b.IdPresentacion) + 1
                Else
                    ObjN.IdPresentacion = 1
                End If

                Dim lIndexC As Integer = -1

                lIndexC = pBeProductoPresentacionList.FindIndex(Function(b) b.Codigo_barra = txtCodigoBarraPresentacion.Text.Trim)

                If lIndexC > -1 Then
                    Throw New Exception(String.Format("El código de barra {0} ya existe.", txtCodigoBarraPresentacion.Text.Trim()))
                ElseIf txtCodigoBarraPresentacion.Text.Trim = txtCodigoBarra.Text.Trim Then
                    Throw New Exception(String.Format("El código de barra {0} ya existe en Producto.", txtCodigoBarraPresentacion.Text.Trim()))
                Else
                    lIndexC = pBeProductoCodigosBarraList.FindIndex(Function(c) c.Codigo_barra = txtCodigoBarraPresentacion.Text.Trim)
                    If lIndexC > -1 Then
                        Throw New Exception(String.Format("El código de barra {0} ya existe en Producto Código de Barra.", txtCodigoBarraPresentacion.Text.Trim()))
                    End If
                End If

                If ObjN.Nombre.Count > pListObjTPR.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                    XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Focus()
                    txtNombrePresentacion.Focus()
                    Return
                End If

                If ObjN.Codigo_barra.Count > pListObjTPR.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud Then
                    XtraMessageBox.Show(String.Format("El código de barra debe de tener como máximo {0} carácteres.", pListObjTP.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Focus()
                    txtCodigoBarraPresentacion.Focus()
                    Return
                End If

                If ChkEsPallet.Checked Then
                    If Val(txtCajasPorCama.Value = 0) Then
                        XtraMessageBox.Show("Ingrese cantidad de unidades de manipulación por cama", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    Else
                        If Val(txtCamasPorTarima.Value = 0) Then
                            XtraMessageBox.Show("Ingrese cantidad de camas por tarima", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Return
                        End If
                    End If
                End If

                ObjN.IdProducto = pBeProducto.IdProducto
                ObjN.Nombre = txtNombrePresentacion.Text.Trim()
                ObjN.Codigo_barra = txtCodigoBarraPresentacion.Text.Trim()
                ObjN.Imprime_barra = chkImprimeBarra.Checked
                ObjN.EsPallet = ChkEsPallet.Checked
                ObjN.Genera_lp_auto = chkGeneraLPAuto.Checked
                ObjN.Permitir_paletizar = chkPermitirPaletizar.Checked
                ObjN.Factor = txtFactor.Value
                ObjN.Peso = txtPeso.Value
                ObjN.Alto = txtAlto.Value
                ObjN.Largo = txtLargo.Value
                ObjN.Ancho = txtAncho.Value
                ObjN.MinimoExistencia = txtMinimoExistencia.Value
                ObjN.MaximoExistencia = txtMaximoExistencia.Value
                ObjN.MinimoPeso = txtMinimoPeso.Value
                ObjN.MaximoPeso = txtMaximoPeso.Value
                ObjN.Codigo = txtCodigo.Text
                ObjN.IdTipoEtiqueta = cmbEtiquetaPresentacion.EditValue
                ObjN.Activo = True
                ObjN.Sistema = chkSistema.Checked
                ObjN.User_agr = AP.UsuarioAp.IdUsuario
                ObjN.Fec_agr = Now
                ObjN.User_mod = AP.UsuarioAp.IdUsuario
                ObjN.Fec_mod = Now
                ObjN.IsNew = True

                'Dimensiones Pallet
                If ChkEsPallet.Checked OrElse chkPermitirPaletizar.Checked Then
                    ObjN.CajasPorCama = txtCajasPorCama.Value
                    ObjN.CamasPorTarima = txtCamasPorTarima.Value
                    ObjN.IdPresentacionPallet = cmbPresentacionPallet.EditValue
                Else
                    ObjN.CajasPorCama = 0
                    ObjN.CamasPorTarima = 0
                    ObjN.IdPresentacionPallet = 0
                End If

                pBeProductoPresentacionList.Add(ObjN)

            End If

            Cursor = Cursors.Default
            LimpiarPresentacion()
            Cargar_Producto_Presentacion()

            CargaComboPresentacionPR()
            CargaComboPresentacionPT()

        Catch ex As Exception
            Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdNewPR_Click(sender As Object, e As EventArgs) Handles cmdNewPR.Click
        LimpiarPresentacion() : txtNombrePresentacion.Focus()
    End Sub

    Private Sub cmdSavePS_Click(sender As Object, e As EventArgs) Handles cmdSavePS.Click

        Cursor = Cursors.WaitCursor

        Try

            ValidaProductoSustituto()

            Dim pIndex As Integer = -1

            pIndex = pBeProductoSustitutoList.FindIndex(Function(b) b.IdProductoSustituto = cmdSavePS.Tag)

            If pIndex > -1 Then

                pBeProductoSustitutoList(pIndex).IdProductoPresentacionOriginal = cmbProductoP.EditValue
                pBeProductoSustitutoList(pIndex).IdProductoReemplazo = CInt(txtIdProductoR.Text)
                pBeProductoSustitutoList(pIndex).ProductoReemplazo.Nombre = txtNombrePR.Text.Trim
                pBeProductoSustitutoList(pIndex).IdProductoPresentacionReemplazo = cmbPresentacionR.EditValue
                pBeProductoSustitutoList(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                pBeProductoSustitutoList(pIndex).Fec_mod = Now
                pBeProductoSustitutoList(pIndex).Activo = chkActivarPS.Checked

            Else

                Dim Obj As New clsBeProducto_sustituto

                If pBeProductoSustitutoList IsNot Nothing AndAlso pBeProductoSustitutoList.Count > 0 Then
                    Obj.IdProductoSustituto = pBeProductoSustitutoList.Max(Function(b) b.IdProductoSustituto) + 1
                Else
                    Obj.IdProductoSustituto = 1
                End If

                Obj.IdProductoOriginal = pBeProducto.IdProducto
                Obj.IdProductoPresentacionOriginal = cmbProductoP.EditValue
                Obj.IdProductoReemplazo = CInt(txtIdProductoR.Text)
                Obj.ProductoReemplazo = New clsBeProducto
                Obj.ProductoReemplazo.Nombre = txtNombrePR.Text.Trim
                Obj.IdProductoPresentacionReemplazo = cmbPresentacionR.EditValue
                Obj.User_agr = AP.UsuarioAp.IdUsuario
                Obj.Fec_agr = Now
                Obj.User_mod = AP.UsuarioAp.IdUsuario
                Obj.Fec_mod = Now
                Obj.Activo = True
                Obj.IsNew = True

                pBeProductoSustitutoList.Add(Obj)

            End If

            Cursor = Cursors.Default
            Limpiar()
            ListarProductoSustituto()

        Catch ex As Exception
            Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdNewPS_Click(sender As Object, e As EventArgs) Handles cmdNewPS.Click
        Limpiar()
    End Sub

    Private Sub cmdSavePRL_Click(sender As Object, e As EventArgs) Handles cmdSavePRL.Click

        Cursor = Cursors.WaitCursor

        Try

            Valida_Producto_Rellenado()

            Dim lIndex As Integer = -1

            lIndex = pBeproductoRellenadoList.FindIndex(Function(b) b.IdRellenado = Val(cmdSavePRL.Tag))

            If lIndex > -1 Then

                pBeproductoRellenadoList(lIndex).IdPresentacion = CInt(cmbPresentacionPR.EditValue)
                pBeproductoRellenadoList(lIndex).Presentacion = cmbPresentacionPR.Text
                pBeproductoRellenadoList(lIndex).IdProductoEstado = CInt(cmbProductoEstado.EditValue)
                pBeproductoRellenadoList(lIndex).Estado = cmbProductoEstado.Text
                pBeproductoRellenadoList(lIndex).IdUbicacion = CInt(txtIdUbicacion.Text.Trim)
                pBeproductoRellenadoList(lIndex).Ubicacion = txtNombreUbicacion.Text.Trim
                pBeproductoRellenadoList(lIndex).IdBodega = cmbBodegaRellenado.EditValue
                pBeproductoRellenadoList(lIndex).IdUnidadMedidaBasica = txtIdUnidadMedidaBasicaRellenado.Text
                pBeproductoRellenadoList(lIndex).IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBeProducto.IdProducto,
                                                                                                                                         cmbBodegaRellenado.EditValue)
                pBeproductoRellenadoList(lIndex).IdPropietario = lcmbPropietario.EditValue

                '#EJC20210225:                                
                pBeproductoRellenadoList(lIndex).IdPresentacionAbastercerCon = cmbPresentacionAbastecerCon.EditValue
                pBeproductoRellenadoList(lIndex).NomPresentacionRellenarCon = cmbPresentacionAbastecerCon.Text

                If optNotificar.Checked Then
                    pBeproductoRellenadoList(lIndex).IdTipoAccion = 1
                ElseIf optGenerarAutomaticamente.Checked Then
                    pBeproductoRellenadoList(lIndex).IdTipoAccion = 2
                End If

                pBeproductoRellenadoList(lIndex).Minimo = txtMinimoPicking.Value
                pBeproductoRellenadoList(lIndex).Maximo = txtMaximoPicking.Value
                pBeproductoRellenadoList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                pBeproductoRellenadoList(lIndex).Fec_mod = Now
                pBeproductoRellenadoList(lIndex).Activo = chkActivarProductoPRL.Checked

                '#EJC20210225:                                
                pBeproductoRellenadoList(lIndex).IdOperadorDefecto = cmbOperadorDefecto.EditValue
                pBeproductoRellenadoList(lIndex).NomOperador = cmbOperadorDefecto.Text

            Else

                Dim Obj As New clsBeProducto_rellenado

                If pBeproductoRellenadoList IsNot Nothing AndAlso pBeproductoRellenadoList.Count > 0 Then

                    Dim lIndexC As Integer = -1
                    Dim vIdPresentacion As Integer = IIf(cmbPresentacionPR.EditValue Is Nothing, 0, cmbPresentacionPR.EditValue)

                    lIndexC = pBeproductoRellenadoList.FindIndex(Function(b) b.IdPresentacion = vIdPresentacion AndAlso b.IdUbicacion = txtIdUbicacion.Text.Trim)

                    If lIndexC > -1 Then
                        Throw New Exception(String.Format("La ubicación {0} ya existe para la presentación {1}", txtNombreUbicacion.Text.Trim, cmbPresentacionPR.Text))
                    End If

                    Obj.IdRellenado = pBeproductoRellenadoList.Max(Function(b) b.IdRellenado) + 1
                Else
                    Obj.IdRellenado = clsLnProducto_rellenado.MaxID()
                End If

                Obj.IdPresentacion = CInt(cmbPresentacionPR.EditValue)
                Obj.Presentacion = cmbPresentacionPR.Text
                Obj.IdProductoEstado = CInt(cmbProductoEstado.EditValue)
                Obj.Estado = cmbProductoEstado.Text
                Obj.IdUbicacion = CInt(txtIdUbicacion.Text.Trim)
                Obj.Ubicacion = txtNombreUbicacion.Text.Trim

                If optNotificar.Checked Then
                    Obj.IdTipoAccion = 1
                ElseIf optGenerarAutomaticamente.Checked Then
                    Obj.IdTipoAccion = 2
                End If

                Obj.Minimo = txtMinimoPicking.Value
                Obj.Maximo = txtMaximoPicking.Value
                Obj.User_agr = AP.UsuarioAp.IdUsuario
                Obj.Fec_agr = Now
                Obj.User_mod = AP.UsuarioAp.IdUsuario
                Obj.Fec_mod = Now
                Obj.Activo = True
                Obj.IsNew = True
                Obj.IdBodega = cmbBodegaRellenado.EditValue
                Obj.IdUnidadMedidaBasica = txtIdUnidadMedidaBasicaRellenado.Text
                Obj.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBeProducto.IdProducto,
                                                                                                            cmbBodegaRellenado.EditValue)
                Obj.IdPropietario = lcmbPropietario.EditValue
                Obj.IdOperadorDefecto = CInt(cmbOperadorDefecto.EditValue)
                Obj.IdPresentacionAbastercerCon = cmbPresentacionAbastecerCon.EditValue
                Obj.NomPresentacionRellenarCon = cmbPresentacionAbastecerCon.Text
                Obj.NomOperador = cmbOperadorDefecto.Text

                pBeproductoRellenadoList.Add(Obj)

            End If

            lblIdRellenado.Visible = False

            LimpiarPR()

            ListarProductoRellenado()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmdSavePT_Click(sender As Object, e As EventArgs) Handles cmdSavePT.Click

        Cursor = Cursors.WaitCursor

        Try

            ValidaPT()

            Dim lIndex As Integer = -1

            lIndex = pBePresentacionTarimaList.FindIndex(Function(b) b.IdPresentacionTarima = cmdSavePT.Tag)

            If lIndex > -1 Then

                pBePresentacionTarimaList(lIndex).IdPresentacion = CInt(cmbPresentacionTarima.EditValue)
                pBePresentacionTarimaList(lIndex).Presentacion = cmbPresentacionTarima.Text
                pBePresentacionTarimaList(lIndex).IdTipoTarima = CInt(cmbTipoTarima.EditValue)
                pBePresentacionTarimaList(lIndex).TipoTarima = cmbTipoTarima.Text
                pBePresentacionTarimaList(lIndex).Cantidad = txtCantidad.Value
                pBePresentacionTarimaList(lIndex).CantidadPorCama = txtCantidadPorCama.Value

                pBePresentacionTarimaList(lIndex).Activo = chkActivoPT2.Checked
                pBePresentacionTarimaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                pBePresentacionTarimaList(lIndex).Fec_mod = Now

            Else

                Dim Obj As New clsBeProducto_presentacion_tarima

                If pBePresentacionTarimaList IsNot Nothing AndAlso pBePresentacionTarimaList.Count > 0 Then

                    Obj.IdPresentacionTarima = pBePresentacionTarimaList.Max(Function(b) b.IdPresentacionTarima) + 1

                    Dim lIndexE As Integer = pBePresentacionTarimaList.FindIndex(Function(b) b.IdPresentacion = cmbPresentacionTarima.EditValue AndAlso b.IdTipoTarima = cmbTipoTarima.EditValue)

                    If lIndexE > -1 Then
                        Throw New Exception(String.Format("La configuración de {0} con el Tipo Tarima {1} ya existe.", cmbPresentacionTarima.Text, cmbTipoTarima.Text))
                    End If

                Else
                    Obj.IdPresentacionTarima = 1
                End If

                Obj.IdPresentacion = CInt(cmbPresentacionTarima.EditValue)
                Obj.Presentacion = cmbPresentacionTarima.Text
                Obj.IdTipoTarima = CInt(cmbTipoTarima.EditValue)
                Obj.TipoTarima = cmbTipoTarima.Text
                Obj.Cantidad = txtCantidad.Value
                Obj.CantidadPorCama = txtCantidadPorCama.Value

                Obj.User_agr = AP.UsuarioAp.IdUsuario
                Obj.Fec_agr = Now
                Obj.User_mod = AP.UsuarioAp.IdUsuario
                Obj.Fec_mod = Now
                Obj.Activo = True
                Obj.IsNew = True

                pBePresentacionTarimaList.Add(Obj)

            End If

            XtraMessageBox.Show("Registro guardado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            'Indica que en el tab se realizaron cambios en memoria y no se han guardado a la BD 
            '#EJC ;) 30052017, Un bello dia.

            If Not tabPresentacionTarima.Text.EndsWith("*") Then
                tabPresentacionTarima.Text = tabPresentacionTarima.Text & " *"
            End If

            Cursor = Cursors.Default

            LimpiarPT()

            ListarPresentacionTarima()

        Catch ex As Exception
            Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdNewPT_Click(sender As Object, e As EventArgs) Handles cmdNewPT.Click
        LimpiarPT() : txtCantidad.Focus()
    End Sub

    Private Sub cmdNewPRL_Click(sender As Object, e As EventArgs) Handles cmdNewPRL.Click

        lblIdRellenado.Text = "" : lblIdRellenado.Visible = False
        cmbPresentacionPR.EditValue = Nothing
        cmbProductoEstado.EditValue = Nothing
        cmbBodegaRellenado.EditValue = AP.IdBodega
        cmbPresentacionAbastecerCon.EditValue = Nothing
        'rbumbas.Checked = True
        txtIdUbicacion.Text = ""
        txtIdUbicacion.Focus()
        cmdSavePRL.Tag = ""

    End Sub

    Private Sub chkCapturarPeso_CheckedChanged(sender As Object, e As EventArgs) Handles chkCapturarPeso.CheckedChanged
        txtPesoReferencia.Enabled = chkCapturarPeso.Checked
        txtPesoTolerancia.Enabled = chkCapturarPeso.Checked
    End Sub

    Private Sub chkCapturaTemperatura_CheckedChanged(sender As Object, e As EventArgs) Handles chkCapturaTemperatura.CheckedChanged
        txtTemperaturaReferencia.Enabled = chkCapturaTemperatura.Checked
        txtTemperaturaTolerancia.Enabled = chkCapturaTemperatura.Checked
    End Sub

    Private Sub chkControlVencimiento_CheckedChanged(sender As Object, e As EventArgs) Handles chkControlVencimiento.CheckedChanged
        txtCicloVida.Enabled = chkControlVencimiento.Checked
    End Sub

    Private Sub chkEsHW_CheckedChanged(sender As Object, e As EventArgs) Handles chkEsHW.CheckedChanged
        txtNoParte.Enabled = chkEsHW.Checked
        If chkEsHW.Checked Then
            txtNoParte.Focus()
        End If
    End Sub

    Private Sub chkCapturaArancel_CheckedChanged(sender As Object, e As EventArgs) Handles chkCapturaArancel.CheckedChanged
        If chkCapturaArancel.Checked Then
            cmbArancel.Enabled = True
            cmbArancel.ItemIndex = 0
        Else
            cmbArancel.Enabled = False
            cmbArancel.ItemIndex = -1
        End If
    End Sub

    Private Sub cmdDesactivarParametro_Click(sender As Object, e As EventArgs) Handles cmdDesactivarParametro.Click

        Try

            If String.IsNullOrEmpty(cmdSaveP.Tag) = False Then

                Dim lIndex As Integer = -1
                lIndex = pBeProductoParametroList.FindIndex(Function(b) b.IdProductoParametro = CInt(cmdSaveP.Tag))

                If lIndex > -1 Then

                    If pBeProductoParametroList(lIndex).Activo = False Then
                        Throw New Exception("El parámetro ya se encuentra desactivado.")
                    Else

                        If XtraMessageBox.Show(String.Format("¿Desactivar el Parámetro de tipo {0}?", txtTipo.Text), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            clsLnProducto_parametros.Desactivar(cmdSaveP.Tag)

                            pBeProductoParametroList(lIndex).Activo = False

                            Cargar_Parametros()
                            cmdSaveP.Tag = Nothing

                        End If

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

    Private Sub cmdDesactivarCodigoBarra_Click(sender As Object, e As EventArgs) Handles cmdDesactivarCodigoBarra.Click

        Try

            If pObjCodigoBarra IsNot Nothing AndAlso pObjCodigoBarra.IdProducto > 0 Then

                Dim lIndex As Integer = -1
                lIndex = pBeProductoCodigosBarraList.FindIndex(Function(b) b.IdProductoCodigoBarra = pObjCodigoBarra.IdProductoCodigoBarra)

                If lIndex > -1 Then

                    If pBeProductoCodigosBarraList(lIndex).Activo = False Then

                        Throw New Exception("El código de barra ya se encuentra desactivado.")

                    Else

                        If XtraMessageBox.Show(String.Format("¿Desactivar el Código de Barra {0}?", pObjCodigoBarra.Codigo_barra), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            clsLnProducto_codigos_barra.Desactivar(pObjCodigoBarra.IdProducto, pObjCodigoBarra.IdProveedor, pObjCodigoBarra.Codigo_barra)
                            txtCodigoBarraL.Text = String.Empty
                            pBeProductoCodigosBarraList(lIndex).Activo = False
                            Cargar_Codigos_Barra()
                            pObjCodigoBarra = New clsBeProducto_codigos_barra

                        End If

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

    Private Sub cmdDesactivarPresentacion_Click(sender As Object, e As EventArgs) Handles cmdDesactivarPresentacion.Click

        Try

            If pObjProductoPresentacion IsNot Nothing AndAlso pObjProductoPresentacion.IdPresentacion > 0 Then

                If pObjProductoPresentacion.Activo = False Then

                    Throw New Exception("La presentación se encuentra desactivada.")

                Else
                    If pObjProductoPresentacion.Sistema = 0 Then
                        If XtraMessageBox.Show(String.Format("¿Desactivar la Presentación {0}?", txtNombrePresentacion.Text.Trim), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            clsLnProducto_presentacion.Desactivar(pObjProductoPresentacion.IdPresentacion)
                            LimpiarPresentacion()
                            pObjProductoPresentacion.Activo = False
                            Cargar_Producto_Presentacion()
                            pObjProductoPresentacion = New clsBeProducto_Presentacion

                        End If
                    Else
                        XtraMessageBox.Show(String.Format("No se puede eliminar la presentación porque es de sistema"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub cmdDesactivarProductoSustituto_Click(sender As Object, e As EventArgs) Handles cmdDesactivarProductoSustituto.Click

        Try

            If pObjProductoSustituto IsNot Nothing AndAlso pObjProductoSustituto.IdProductoSustituto > 0 Then

                If pObjProductoSustituto.Activo = False Then
                    Throw New Exception("El producto sustituto ya se encuentra desactivado.")
                Else
                    If XtraMessageBox.Show(String.Format("¿Desactivar el Producto Sustituto {0}?", txtNombrePR.Text.Trim), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        clsLnProducto_sustituto.Delete(pObjProductoSustituto.IdProductoSustituto)
                        Limpiar()
                        pObjProductoSustituto.Activo = False
                        ListarProductoSustituto()
                        pObjProductoSustituto = New clsBeProducto_sustituto
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

    Private Sub cmdDesactivarProductoRellenado_Click(sender As Object, e As EventArgs) Handles cmdDesactivarProductoRellenado.Click

        Try

            If pObjProductoRellenado IsNot Nothing AndAlso pObjProductoRellenado.IdRellenado > 0 Then

                If pObjProductoRellenado.Activo = False Then

                    Throw New Exception("El reabastecimiento seleccionado ya se encuentra desactivado.")

                Else

                    If XtraMessageBox.Show("¿Desactivar el reabastecimiento de producto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        If clsLnProducto_rellenado.Desactivar(pObjProductoRellenado.IdRellenado) > 0 Then

                            '#EJC20210301: Marcar en la lista el registro como inactivo para evitar cargar desde la BD de nuevo.
                            pBeproductoRellenadoList.Find(Function(x) x.IdRellenado = pObjProductoRellenado.IdRellenado).Activo = False

                            LimpiarPR()

                            pObjProductoRellenado.Activo = False

                            ListarProductoRellenado()

                            pObjProductoRellenado = New clsBeProducto_rellenado

                        End If

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

    Private Sub cmdDesactivarPresentacionTarima_Click(sender As Object, e As EventArgs) Handles cmdDesactivarPresentacionTarima.Click

        Try

            If pObjPresentacionTarima IsNot Nothing AndAlso pObjPresentacionTarima.IdPresentacionTarima > 0 Then

                If pObjPresentacionTarima.Activo = False Then

                    Throw New Exception("La presentación tarima ya se encuentra desactivada.")

                Else

                    If XtraMessageBox.Show("¿Desactivar la presentación tarima?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        clsLnProducto_presentacion_tarima.Desactivar(pObjPresentacionTarima.IdPresentacionTarima)
                        LimpiarPT()
                        pObjPresentacionTarima.Activo = False
                        ListarPresentacionTarima()
                        pObjPresentacionTarima = New clsBeProducto_presentacion_tarima
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

    Private Sub chkGeneraLote_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneraLote.CheckedChanged

        Try

            If chkGeneraLote.Checked AndAlso chkControlLote.Checked = False Then
                chkGeneraLote.Checked = False
                Throw New Exception("Seleccione Control Lote.")
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

    Private Sub cmdSaveCn_Click(sender As Object, e As EventArgs) Handles cmdSaveCn.Click

        Cursor = Cursors.WaitCursor

        Try

            Dim pIndex As Integer = -1

            pIndex = pBeProductoPresConvList.FindIndex(Function(b) b.IdConversion = cmdSaveCn.Tag)

            If pIndex > -1 Then

                pBeProductoPresConvList(pIndex).IdPresentacionOrigen = cmbOriginal.EditValue
                pBeProductoPresConvList(pIndex).IdPresentacionDestino = cmbInversa.EditValue
                pBeProductoPresConvList(pIndex).Factor = txtFactorConver.Text.Trim
                pBeProductoPresConvList(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                pBeProductoPresConvList(pIndex).Fec_mod = Now
                pBeProductoPresConvList(pIndex).Activo = chkActivarConver.Checked
                pBeProductoPresConvList(pIndex).Inverso = chkInverso.Checked
                pBeProductoPresConvList(pIndex).ProductoPresentacionOrigen.Nombre = cmbOriginal.EditValue
            Else

                Dim Obj As New clsBeProducto_presentaciones_conversiones

                If pBeProductoPresConvList IsNot Nothing AndAlso pBeProductoPresConvList.Count > 0 Then
                    Obj.IdConversion = pBeProductoPresConvList.Max(Function(b) b.IdConversion) + 1
                Else
                    Obj.IdConversion = 1
                End If

                Obj.IdPresentacionOrigen = cmbOriginal.EditValue
                Obj.IdPresentacionDestino = cmbInversa.EditValue
                Obj.Factor = txtFactorConver.Text.Trim
                Obj.Inverso = True
                Obj.Activo = True
                Obj.Fec_agr = Now
                Obj.User_mod = AP.UsuarioAp.IdUsuario
                Obj.Fec_mod = Now
                Obj.User_agr = AP.UsuarioAp.IdUsuario
                Obj.IsNew = True
                Obj.ProductoPresentacionDestino.Nombre = cmbInversa.Text()
                Obj.ProductoPresentacionOrigen.Nombre = cmbOriginal.Text
                pBeProductoPresConvList.Add(Obj)

            End If

            Cursor = Cursors.Default
            LimpiarConver()
            ListarConversionesPresentacion()

        Catch ex As Exception
            Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub chkActivarConver_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivarConver.CheckedChanged
        ListarConversionesPresentacion()
    End Sub

    Private Sub cmdDesactivarCn_Click(sender As Object, e As EventArgs) Handles cmdDesactivarCn.Click
        Try

            If pObjProdPresConv IsNot Nothing AndAlso pObjProdPresConv.IdConversion > 0 Then

                If pObjProdPresConv.Activo = False Then
                    Throw New Exception("La presentación sustituta ya se encuentra desactivada.")
                Else
                    If XtraMessageBox.Show(String.Format("¿Desactivar el Presentación Sustituta?", cmbInversa.EditValue), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        clsLnProducto_Presentaciones_conversiones.Delete(pObjProdPresConv.IdConversion)
                        LimpiarConver()
                        pObjProdPresConv.Activo = False
                        ListarConversionesPresentacion()
                        pObjProdPresConv = New clsBeProducto_presentaciones_conversiones
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

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles cmdNuevoCn.Click
        LimpiarConver()
    End Sub

    Private Sub chkActivosCn_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivosCn.CheckedChanged
        ListarConversionesPresentacion()
    End Sub

    Private Sub lnkUnidadMedida_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUnidadMedida.LinkClicked

        Try

            If lcmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                Dim UnidadMedida As New frmUnidad_MedidaList() With {.pIdPropietario = lcmbPropietario.EditValue, .Modo = frmUnidad_MedidaList.pModo.Seleccion}

                If OpcionesMenu IsNot Nothing Then
                    UnidadMedida.OpcionesMenu = OpcionesMenu
                    UnidadMedida.mnuActualizar.Enabled = OpcionesMenu.Leer
                    UnidadMedida.mnuNuevo.Enabled = OpcionesMenu.Modificar
                End If

                UnidadMedida.ShowDialog()

                '#EJC20220326A: Cambio a lookupedit unidad de medida.
                IMS.Listar_Unidades_Medida(lcmbUnidadMedidaBasica, 0)

                If UnidadMedida.pObjUM IsNot Nothing AndAlso UnidadMedida.pObjUM.IdUnidadMedida <> 0 Then
                    lcmbUnidadMedidaBasica.EditValue = UnidadMedida.pObjUM.IdUnidadMedida
                    'txtNombreUnidadMedida.Text = UnidadMedida.pObjUM.Nombre
                    txtIdUnidadMedidaBasicaRellenado.Text = UnidadMedida.pObjUM.IdUnidadMedida
                    txtNombreUMBasRellenado.Text = UnidadMedida.pObjUM.Nombre
                    txtIdUMBasReabastecerCon.Text = UnidadMedida.pObjUM.IdUnidadMedida
                    txtNombreUMBasReabastecerCon.Text = UnidadMedida.pObjUM.Nombre
                End If

                UnidadMedida.Close()
                UnidadMedida.Dispose()

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

    Private Sub ChkEsPallet_CheckedChanged(sender As Object, e As EventArgs) Handles ChkEsPallet.CheckedChanged

        If ChkEsPallet.Checked = True Then
            grpConfigPallet.Enabled = True
            Llena_Combo_Presentaciones_Pallet()
        Else
            grpConfigPallet.Enabled = False
            cmbPresentacionPallet.Properties.DataSource = Nothing
        End If

    End Sub

    Private Sub Llena_Combo_Presentaciones_Pallet()

        Try

            cmbPresentacionPallet.Properties.DataSource = Nothing

            If pBeProductoPresentacionList IsNot Nothing AndAlso pBeProductoPresentacionList.Count > 0 Then

                Dim DT As New DataTable("Presentacion")
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("Nombre", GetType(String))


                For Each Obj As clsBeProducto_Presentacion In pBeProductoPresentacionList.Where(Function(x) x.EsPallet = False)
                    DT.Rows.Add(Obj.IdPresentacion, Obj.Codigo_barra + " - " + Obj.Nombre)
                Next

                If DT.Rows.Count > 0 Then
                    cmbPresentacionPallet.Properties.DisplayMember = "Nombre"
                    cmbPresentacionPallet.Properties.ValueMember = "Código"
                    cmbPresentacionPallet.Properties.DataSource = DT
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    'Private Sub chkGeneraIP_Validating(sender As Object, e As CancelEventArgs) 

    '    Try

    '        If GrdPresentacion.RowCount > 0 Then

    '            pObjProductoPresentacion = pBeProductoPresentacionList.Find(Function(b) b.EsPallet = True)

    '            If pObjProductoPresentacion is Nothing Then
    '                e.Cancel = True
    '                XtraMessageBox.Show("Debe configurar por lo menos una presentación como pallet, antes de poder marcar este parámetro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If

    '        Else
    '            XtraMessageBox.Show("Debe configurar por lo menos una presentación como pallet, antes de poder marcar este parámetro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            chkGeneraLp.Checked = False
    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try

    'End Sub

    Private Sub chkGeneraLPAuto_Validating(sender As Object, e As CancelEventArgs) Handles chkGeneraLPAuto.Validating

        If chkGeneraLPAuto.Checked Then

            If (Not (ChkEsPallet.Checked) AndAlso Not (chkPermitirPaletizar.Checked)) Then
                XtraMessageBox.Show("La presentación debe ser pallet o permitir paletizar debe ser verdadero para poder habilitar éste parámetro",
                 Text,
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Exclamation)
                chkGeneraLPAuto.ErrorText = "Marque la bandera permitir paletizar primero."
                e.Cancel = True
            Else
                'If chkPermitirPaletizar.Checked Then chkPermitirPaletizar.Checked = False
            End If

        End If

    End Sub

    Private Sub chkPermitirPaletizar_CheckedChanged(sender As Object, e As EventArgs) Handles chkPermitirPaletizar.CheckedChanged
        grpConfigPallet.Enabled = chkPermitirPaletizar.Checked
    End Sub

    Private Sub Listar_Encabezado_Stock()

        Dim lRow As DataRow

        Try

            listaStock = clsLnStock.Get_All_Stock_By_IdProducto(pBeProducto.IdProducto)

            Dim ListaEncabezadoStock = From i In listaStock Group i By Keys = New With {Key i.IdProducto, Key i.Codigo_Producto,
                                                                Key i.Propietario, Key i.Nombre_Producto, Key i.Nombre_Presentacion, Key i.Codigo_Barra, Key i.UMBas} Into Group
                                       Select New With {.id = Keys.IdProducto, .cod = Keys.Codigo_Producto, .prop = Keys.Propietario, .nom = Keys.Nombre_Producto, .pres = Keys.Nombre_Presentacion,
                                                        .barra = Keys.Codigo_Barra, .um = Keys.UMBas,
                                                         .CantidadUMBas = Group.Sum(Function(x) x.CantidadUmBas),
                                                         .CantidadPresentacion = Group.Sum(Function(x) x.CantidadPresentacion)}

            If ListaEncabezadoStock IsNot Nothing AndAlso ListaEncabezadoStock.Count > 0 Then

                For Each ObjSt In ListaEncabezadoStock

                    lRow = DsResumenStock.Encabezado.NewRow

                    lRow.Item("IdProducto") = ObjSt.id
                    lRow.Item("Código") = ObjSt.cod
                    lRow.Item("Propietario") = ObjSt.prop
                    lRow.Item("Producto") = ObjSt.nom
                    lRow.Item("Presentación") = ObjSt.pres
                    lRow.Item("Código_Barra") = ObjSt.barra
                    lRow.Item("CantidadUMBas") = ObjSt.CantidadUMBas
                    lRow.Item("UM_Bas") = ObjSt.um
                    lRow.Item("CantidadPresentación") = ObjSt.CantidadPresentacion

                    DsResumenStock.Encabezado.AddEncabezadoRow(lRow)

                Next

            End If

            '#CKFK 20210521 Voy a poner este mensaje en comentario para la demo
        Catch ex As Exception
            'XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Listar_Detalle_Stock()

        Dim lRow As DataRow

        Try

            '#EJC20200205: Qué pasa si no se llena otra vez porque ya se llenó en encabezado ?
            'listaStock = clsLnStock.Get_All_Stock_Detalle_Resumen(pBeProducto.IdProducto)

            Dim ListaEncabezadoStocks = From i In listaStock Group i By Keys = New With {Key i.IdProducto, Key i.Codigo_Producto,
                            Key i.Propietario, Key i.Nombre_Producto,
                            Key i.Nombre_Presentacion, Key i.Codigo_Barra,
                            Key i.UMBas, Key i.IdProductoBodega,
                            Key i.IdStock, Key i.NomEstado,
                            Key i.Serial, Key i.CantidadPresentacion,
                            Key i.CantidadUmBas, Key i.Fecha_ingreso, Key i.Fecha_Vence,
                            Key i.Lote, Key i.IdRecepcionEnc,
                            Key i.IdUbicacion, Key i.Ubicacion_Tramo,
                            Key i.Ubicacion_Nombre, Key i.LargoUbicacion} Into Group
                                        Select New With {.id = Keys.IdProducto, .cod = Keys.Codigo_Producto, .prop = Keys.Propietario, .nom = Keys.Nombre_Producto, .pres = Keys.Nombre_Presentacion,
                                                        .barra = Keys.Codigo_Barra, .um = Keys.UMBas, Keys.IdStock, .idProdBodega = Keys.IdProductoBodega, .estado = Keys.NomEstado, Keys.Serial,
                                                        .cant_pres = Keys.CantidadPresentacion, .cant_umbas = Keys.CantidadUmBas, .fechaing = Keys.Fecha_ingreso, Keys.Fecha_Vence,
                                                        Keys.Lote, .idrec = Keys.IdRecepcionEnc, .idubic = Keys.IdUbicacion, .tramo = Keys.Ubicacion_Tramo, .ubic = Keys.Ubicacion_Nombre, .largo = Keys.LargoUbicacion}

            If ListaEncabezadoStocks IsNot Nothing AndAlso ListaEncabezadoStocks.Count > 0 Then

                DsResumenStock.Detalle.Clear()

                For Each Objs In ListaEncabezadoStocks

                    lRow = DsResumenStock.Detalle.NewRow

                    lRow.Item("IdProducto") = Objs.id
                    lRow.Item("IdProductoBodega") = Objs.idProdBodega
                    lRow.Item("IdStock") = Objs.IdStock
                    lRow.Item("Codigo") = Objs.cod
                    lRow.Item("Propietario") = Objs.prop
                    lRow.Item("Producto") = Objs.nom
                    lRow.Item("Barra") = Objs.barra
                    lRow.Item("Estado") = Objs.estado
                    lRow.Item("Presentacion") = Objs.pres
                    lRow.Item("UMBas") = Objs.um
                    lRow.Item("serial") = Objs.Serial
                    lRow.Item("Cant_Presentacion") = Objs.cant_pres
                    lRow.Item("Cant_UMBas") = Objs.cant_umbas
                    lRow.Item("Fecha_Ingreso") = Objs.fechaing
                    lRow.Item("Fecha_Vence") = Objs.Fecha_Vence
                    lRow.Item("lote") = Objs.Lote
                    lRow.Item("NoRecepcion") = Objs.idrec
                    lRow.Item("IdUbicacion") = Objs.idubic
                    lRow.Item("Tramo") = Objs.tramo
                    lRow.Item("Ubicacion") = Objs.ubic
                    lRow.Item("largo") = Objs.largo

                    DsResumenStock.Detalle.AddDetalleRow(lRow)

                Next

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

    Private Sub Cargar_Datos_Stock()

        Try

            grdPStock.BeginUpdate()

            Listar_Encabezado_Stock()

            Listar_Detalle_Stock()

            DsResumenStock.Detalle.IdProductoBodegaColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.IdProductoColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.NoRecepcionColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.IdStockColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.CodigoColumn.ReadOnly = True And Visible = False
            DsResumenStock.Detalle.EstadoColumn.ReadOnly = True
            DsResumenStock.Detalle.loteColumn.ReadOnly = True
            DsResumenStock.Detalle.Fecha_VenceColumn.ReadOnly = True
            DsResumenStock.Detalle.PropietarioColumn.ReadOnly = True

            grdPStock.EndUpdate()

            grdPStock.ForceInitialize()

            If GridView2.Columns.Count > 0 Then

                GridView2.OptionsView.ShowFooter = True

                GridView2.Columns("CantidadUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n2}"

                GridView2.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n2}"

                GridView2.Columns("CantidadPresentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("CantidadPresentación").SummaryItem.DisplayFormat = "{0:n2}"

                GridView2.Columns("CantidadPresentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("CantidadPresentación").DisplayFormat.FormatString = "{0:n2}"

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick

        Try

            Dim Ubicacion As New frmReportMovimiento() With {.pIdProducto = pBeProducto.IdProducto,
                .Modo = frmReportMovimiento.pModo.Seleccion}
            Ubicacion.dtpFechaDel.Value = Now.AddMonths(-1)
            Ubicacion.ShowDialog()
            pIdProducto = 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    'Private Sub Listar_detalle()

    '    Dim BeUbicacionActual As New clsBeBodega_ubicacion

    '    DsResumenStock.Detalle.Clear()

    '    Try

    '        listaStock = clsLnStock.GetAllStockByProducto(pBeProducto.IdProducto)

    '        For Each Obj As clsBeVW_stock_res In listaStock

    '            Dim lRow As DataRow = DsResumenStock.Detalle.NewRow

    '            If listaStock IsNot Nothing AndAlso listaStock.Count > 0 Then

    '                BeUbicacionActual.IdUbicacion = Obj.IdUbicacionActual

    '                BeUbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(BeUbicacionActual.IdUbicacion)

    '                lRow.Item("Código") = Obj.Codigo
    '                lRow.Item("Propietario") = Obj.Propietario
    '                lRow.Item("IdProducto") = Obj.IdProducto
    '                lRow.Item("NomEstado") = Obj.NomEstado
    '                lRow.Item("lote") = Obj.Lote
    '                lRow.Item("IdProductoBodega") = Obj.IdProductoBodega
    '                lRow.Item("IdStock") = Obj.IdStock
    '                lRow.Item("Fecha_Vence") = Obj.Fecha_vence
    '                lRow.Item("IdUbicacion") = Obj.IdUbicacionActual
    '                lRow.Item("Ubicacion") = BeUbicacionActual.NombreCompleto
    '                lRow.Item("Tramo") = Obj.IdTramo
    '                lRow.Item("IdRecepcion") = Obj.IdRecepcionEnc

    '            End If

    '            DsResumenStock.Detalle.AddDetalleRow(lRow)

    '        Next

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    'End Sub

    Private Sub grdPStock_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdPStock.ViewRegistered

        Try

            Dim gridView As GridView = e.View

            If gridView.IsDetailView Then

                gridView.Columns("IdProductoBodega").Visible = False
                gridView.Columns("Producto").Visible = False
                gridView.Columns("IdProducto").Visible = False
                gridView.Columns("IdStock").Visible = False
                gridView.Columns("Codigo").Visible = False
                gridView.Columns("Propietario").Visible = False
                gridView.Columns("lote").Caption = "Lote"
                gridView.Columns("Estado").Caption = "Estado"
                gridView.Columns("Fecha_Vence").Caption = "Vence"
                gridView.Columns("NoRecepcion").Caption = "Recepción"

                Dim Dr As DataRowView = GridView2.GetFocusedRow
                Dim vPresActual As String = IIf(IsDBNull(Dr.Item("Presentación")), "", Dr.Item("Presentación"))
                Dim vUMBas As String = IIf(IsDBNull(Dr.Item("UM_Bas")), "", Dr.Item("UM_Bas"))

                If vPresActual <> "" Then
                    '#CM_20171113: Corrección del nombre de Presentación por Presentacion en aplicación de filtro para reporte de Stock en Producto.
                    gridView.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("Presentacion", vPresActual)
                Else
                    '#EJC20171027_0439AM: Filtrar por presentación 
                    gridView.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("UMBas", vUMBas)
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

    Private Sub frmProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub cmdImprmirCodigoBarra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprmirCodigoBarra.ItemClick

        Try

            Dim pd As PrintDialog = New PrintDialog()
            pd.PrinterSettings = New PrinterSettings()

            If DialogResult.OK = pd.ShowDialog(Me) Then
                Imprimir_Etiqueta(pBeProducto, pBeProducto.Codigo_barra, pd.PrinterSettings.PrinterName)
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

    Private Sub Imprimir_Etiqueta(ByVal pBeProducto As clsBeProducto,
                                  ByVal CodigoBarra As String,
                                  ByVal PrinterName As String)

        Dim vIdTipoEtiqueta As Integer
        Dim ZPLString As String = ""
        Dim Marca = pBeProducto.Marca.Nombre
        Dim Linea = pBeProducto.Familia.Nombre
        Dim Modelo = pBeProducto.Clasificacion.Nombre
        Dim Estado = pBeProducto.ParametroA.Nombre
        Dim Lado = pBeProducto.ParametroB.Nombre
        Dim CodigoBodega As String = ""
        Dim NombreBodega As String = ""
        Dim NombreEmpresa As String = ""

        vIdTipoEtiqueta = pBeProducto.IdTipoEtiqueta
        CodigoBodega = AP.Bodega.Codigo
        NombreBodega = AP.Bodega.Nombre
        NombreEmpresa = AP.NomEmpresa


        If vIdTipoEtiqueta = 1 Then

#Region "vIdTipoEtiqueta = 1"

            ZPLString = String.Format("^XA 
                         ^MMT
                         ^PW700 
                         ^LL0406 
                         ^LS0 
                         ^FT450,21^A0I,20,14^FH^FD{4}^FS 
                         ^FO2,40^GB670,0,5^FS 
                         ^FT270,61^A0I,30,24^FH^FD{0}^FS 
                         ^FT550,61^A0I,30,24^FH^FD{1}^FS 
                         ^FT670,306^A0I,30,24^FH^FD{2}^FS 
                         ^FT360,61^A0I,30,24^FH^FDBodega:^FS 
                         ^FT670,61^A0I,30,24^FH^FDEmpresa:^FS 
                         ^FT670,367^A0I,25,24^FH^FDTOMWMS Codigo de Producto^FS 
                         ^FO2,340^GB670,0,14^FS 
                         ^BY3,3,160^FT670,131^BCI,,Y,N 
                         ^FD{3}^FS 
                         ^PQ1,0,1,Y 
                         ^XZ", CodigoBodega + " - " + NombreBodega, NombreEmpresa,
                        pBeProducto.Codigo + " - " + pBeProducto.Nombre,
                        pBeProducto.Codigo_barra,
                        AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos)

#End Region

        ElseIf vIdTipoEtiqueta = 2 Then

#Region "vIdTipoEtiqueta = 2"
            ZPLString = String.Format("^XA " +
                                        "^MMT " +
                                        "^PW600 " +
                                        "^LL0406 " +
                                        "^LS0 " +
                                        "^FT450,21^A0I,20,14^FH^FD{4}^FS  " +
                                        "^FO2,40^GB670,0,5^FS  " +
                                        "^FT440,90^A0I,28,30^FH^FD{0}^FS " +
                                        "^FT560,90^A0I,26,30^FH^FDBodega:^FS " +
                                        "^FT440,125^A0I,28,30^FH^FD{1}^FS " +
                                        "^FT560,125^A0I,26,30^FH^FDEmpresa:^FS " +
                                        "^BY2,3,160^FT550,200^BCI,,Y,N " +
                                        "^FD{2}^FS " +
                                        "^PQ1,0,1,Y  " +
                                        "^FT560,400^A0I,35,40^FH^FD{3}^FS " +
                                        "^FO2,440^GB670,14,14^FS " +
                                        "^FT560,470^A0I,25,24^FH^FDTOMWMS  Codigo de Producto^FS " +
                                        "^XZ", CodigoBodega + "-" + NombreBodega,
                                        NombreEmpresa,
                                        pBeProducto.Codigo_barra,
                                        pBeProducto.Codigo + " - " + pBeProducto.Nombre,
                                        AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos)

#End Region

        ElseIf vIdTipoEtiqueta = 4 Then

#Region "vIdTipoEtiqueta = 4"
            ZPLString = String.Format("^XA " +
                                    "^MMT " +
                                    "^PW812 " +
                                    "^LL609 " +
                                    "^LS0 " +
                                    "^FT450,21^A0I,20,14^FH^FD{4}^FS  " +
                                    "^FO2,40^GB670,0,5^FS  " +
                                    "^FT440,90^A0I,28,30^FH^FD{0}^FS " +
                                    "^FT560,90^A0I,26,30^FH^FDBodega:^FS " +
                                    "^FT440,125^A0I,28,30^FH^FD{1}^FS " +
                                    "^FT560,125^A0I,26,30^FH^FDEmpresa:^FS " +
                                    "^BY3,3,160^FT550,200^BCI,,Y,N " +
                                    "^FD{2}^FS " +
                                    "^PQ1,0,1,Y  " +
                                    "^FT600,400^A0I,35,40^FH^FD{3}^FS " +
                                    "^FO2,440^GB670,14,14^FS " +
                                    "^FT600,470^A0I,25,24^FH^FDTOMWMS Codigo de Producto^FS " +
                                    "^XZ", CodigoBodega + "-" + NombreBodega,
                                    NombreEmpresa,
                                    pBeProducto.Codigo_barra,
                                    pBeProducto.Codigo + " - " + pBeProducto.Nombre,
                                    AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos)
#End Region

        ElseIf vIdTipoEtiqueta = 5 Then

#Region "vIdTipoEtiqueta = 5"

            ZPLString = String.Format(
            "
            ^XA
            ^MMT
            ^PW609
            ^LL0406
            ^LS0
            ^FT181,250^A0I,25,14^FH\^FD{0}^FS
            ^FT455,250^A0I,25,14^FH\^FD{1}^FS
            ^FT530,506^A0I,25,14^FH\^FD{2}^FS
            ^FT455,160^A0I,25,14^FH\^FD{3}^FS
            ^FT455,110^A0I,25,14^FH\^FD{4}^FS
            ^FT455,60^A0I,25,14^FH\^FD{5}^FS
            ^FT180,160^A0I,25,14^FH\^FD{6}^FS
            ^FT180,115^A0I,25,14^FH\^FD{7}^FS
            ^FT310,250^A0I,25,24^FH\^FDPropietario:^FS
            ^FT550,250^A0I,25,24^FH\^FDEmpresa:^FS
            ^FT550,160^A0I,25,24^FH\^FDMarca:^FS
            ^FT550,110^A0I,25,24^FH\^FDLinea:^FS
            ^FT550,60^A0I,25,24^FH\^FDModelo:^FS
            ^FT310,160^A0I,25,24^FH\^FDEstado:^FS
            ^FT310,115^A0I,25,24^FH\^FDLado:^FS
            ^FT530,560^A0I,25,24^FH\^FDTOM, WMS. - Product Barcode^FS
            ^FO2,540^GB606,0,12^FS
            ^BY2,3,155^FT530,331^BCI,,Y,N
            ^FD{8}^FS
            ^PQ1,0,1,Y
            ^XZ", pBeProducto.Propietario.Nombre_comercial,
            AP.NomEmpresa,
            pBeProducto.Nombre, Marca,
            Linea, Modelo, Estado, Lado,
            CodigoBarra)

#End Region

        End If

        Try

            RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Impresión de ubicaciones",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

#Region "ProductoKit"

    Private Sub linklblProductoKit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklblProductoKit.LinkClicked

        Try

            Dim Producto As New frmProductoList()
            Producto.cmdImportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Producto.chkActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Producto.EsKit = True
            If pBeProducto.IdProducto <> Nothing AndAlso pBeProducto.IdProducto <> 0 Then
                Producto.pIdProductoExcepto = pBeProducto.IdProducto
                Producto.pIdPropietario = CInt(lcmbPropietario.EditValue)
            End If
            Producto.Modo = frmProductoList.pModo.Seleccion
            Producto.ShowDialog()

            If Producto.pObjProducto IsNot Nothing AndAlso Producto.pObjProducto.IdProducto <> 0 Then
                pbeProductoKitHijo = New clsBeProducto
                txtCodPrdHijo.Text = Producto.pObjProducto.Codigo
                txtNombrePrdHijo.Text = Producto.pObjProducto.Nombre
                txtIdUMBHijo.Text = Producto.pObjProducto.UnidadMedida.IdUnidadMedida
                txtNomUMBHijo.Text = Producto.pObjProducto.UnidadMedida.Nombre
                pbeProductoKitHijo = Producto.pObjProducto
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

    Private Sub chkEsKit_CheckedChanged(sender As Object, e As EventArgs) Handles chkEsKit.CheckedChanged

        If chkEsKit.Checked Then
            tabProductoKit.PageVisible = True
            lblProdPadre.Text = pBeProducto.Codigo & " - " & pBeProducto.Nombre
        Else
            tabProductoKit.PageVisible = False
        End If

    End Sub

    Private Sub txtCodPrdHijo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodPrdHijo.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtCodPrdHijo.Text.Length = 1 Then
                txtNombrePrdHijo.Text = String.Empty
                txtIdUMBHijo.Text = String.Empty
                txtNomUMBHijo.Text = String.Empty
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

    Private Sub txtCodPrdHijo_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodPrdHijo.PreviewKeyDown

        Try
            If e.KeyData = Keys.Tab Then
                If String.IsNullOrEmpty(txtCodPrdHijo.Text.Trim()) = False Then
                    If txtCodPrdHijo.Text > "0" Then
                        pbeProductoKitHijo = New clsBeProducto
                        pbeProductoKitHijo = clsLnProducto.Get_Single_By_CodigoProducto(txtCodPrdHijo.Text.Trim())
                        If pbeProductoKitHijo IsNot Nothing AndAlso pbeProductoKitHijo.IdProducto > 0 Then
                            txtNombrePrdHijo.Text = pbeProductoKitHijo.Nombre
                            txtIdUMBHijo.Text = pbeProductoKitHijo.UnidadMedida.IdUnidadMedida
                            txtNomUMBHijo.Text = pbeProductoKitHijo.UnidadMedida.Nombre
                        Else
                            XtraMessageBox.Show(String.Format("No Existe Producto con código {0}", txtCodPrdHijo.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtNombrePrdHijo.Text = String.Empty
                            txtCodPrdHijo.Focus() : txtCodPrdHijo.SelectAll()
                        End If
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

    Private Sub Limpia_Producto_Kit()
        cmdGuardarPrk.Tag = Nothing
        txtCodPrdHijo.Text = String.Empty
        txtNombrePrdHijo.Text = String.Empty
        txtIdUMBHijo.Text = String.Empty
        txtNomUMBHijo.Text = String.Empty
        txtCantPrdHijo.Value = 0
    End Sub

    Private Sub cmdGuardarPrk_Click(sender As Object, e As EventArgs) Handles cmdGuardarPrk.Click

        Cursor = Cursors.WaitCursor

        Try

            Dim pIndex As Integer = -1

            pIndex = pBeProductoKitList.FindIndex(Function(b) b.IdProductoKitComposicion = cmdGuardarPrk.Tag)

            If Valida_Campos_Kit() Then

                If pIndex > -1 Then

                    pBeProductoKitList(pIndex).IdProductoPadre = pBeProducto.IdProducto
                    pBeProductoKitList(pIndex).IdProductoHijo = pbeProductoKitHijo.IdProducto
                    pBeProductoKitList(pIndex).IdUnidadMedidaBasicaPadre = pBeProducto.IdUnidadMedidaBasica
                    pBeProductoKitList(pIndex).IdUnidadMedidaBasicaHijo = pbeProductoKitHijo.IdUnidadMedidaBasica
                    pBeProductoKitList(pIndex).Cantidad = txtCantPrdHijo.Value
                    pBeProductoKitList(pIndex).Fecha_agr = Date.Now
                    pBeProductoKitList(pIndex).User_agr = AP.UsuarioAp.IdUsuario
                    pBeProductoKitList(pIndex).Fecha_mod = Date.Now
                    pBeProductoKitList(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pBeProductoKitList(pIndex).Producto = pbeProductoKitHijo

                Else

                    Dim Obj As New clsBeProducto_kit_composicion

                    If pBeProductoKitList IsNot Nothing AndAlso pBeProductoKitList.Count > 0 Then
                        Obj.IdProductoKitComposicion = pBeProductoKitList.Max(Function(b) b.IdProductoKitComposicion) + 1
                    Else
                        Obj.IdProductoKitComposicion = 1
                    End If

                    Obj.IdProductoPadre = pBeProducto.IdProducto
                    Obj.IdProductoHijo = pbeProductoKitHijo.IdProducto
                    Obj.IdUnidadMedidaBasicaPadre = pBeProducto.IdUnidadMedidaBasica
                    Obj.IdUnidadMedidaBasicaHijo = pbeProductoKitHijo.IdUnidadMedidaBasica
                    Obj.Cantidad = txtCantPrdHijo.Value
                    Obj.Fecha_agr = Date.Now
                    Obj.User_agr = AP.UsuarioAp.IdUsuario
                    Obj.Fecha_mod = Date.Now
                    Obj.User_mod = AP.UsuarioAp.IdUsuario
                    Obj.Producto = pbeProductoKitHijo
                    Obj.IsNew = True
                    Obj.IdBodega = AP.IdBodega

                    pBeProductoKitList.Add(Obj)

                End If


            End If

            Cursor = Cursors.Default
            Limpia_Producto_Kit()
            ListarProductoKit()
        Catch ex As Exception
            Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private pObjProductoKit As New clsBeProducto_kit_composicion
    Private Sub grdPrdKit_DoubleClick(sender As Object, e As EventArgs) Handles grdPrdKit.DoubleClick

        Try

            If GridView11.RowCount > 0 Then

                Dim Dr As DataRowView = GridView11.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pBeProductoKitList.FindIndex(Function(b) b.IdProductoKitComposicion = Dr.Item("Correlativo"))

                If lIndex > -1 Then

                    pbeProductoKitHijo = New clsBeProducto

                    pObjProductoKit = pBeProductoKitList.Find(Function(b) b.IdProductoKitComposicion = Dr.Item("Correlativo"))

                    pbeProductoKitHijo = clsLnProducto.Get_Single_By_IdProducto(pObjProductoKit.IdProductoHijo)
                    pBeProductoKitList(lIndex).Producto = pbeProductoKitHijo

                    cmdGuardarPrk.Tag = pBeProductoKitList(lIndex).IdProductoKitComposicion
                    txtCodPrdHijo.Text = pbeProductoKitHijo.Codigo
                    txtNombrePrdHijo.Text = pbeProductoKitHijo.Nombre
                    txtIdUMBHijo.Text = pbeProductoKitHijo.IdUnidadMedidaBasica
                    txtNomUMBHijo.Text = pbeProductoKitHijo.UnidadMedida.Nombre
                    txtCantPrdHijo.Value = pBeProductoKitList(lIndex).Cantidad

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

    Private Sub cmdEliminarPrk_Click(sender As Object, e As EventArgs) Handles cmdEliminarPrk.Click

        Try

            If pObjProductoKit IsNot Nothing AndAlso pObjProductoKit.IdProductoKitComposicion > 0 Then

                If XtraMessageBox.Show(String.Format("¿Desactivar el Producto{0}?", txtNombrePrdHijo.Text.Trim), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim Dr As DataRowView = GridView11.GetFocusedRow

                    Dim lIndex As Integer = -1

                    lIndex = pBeProductoKitList.RemoveAll(Function(x) x.IdProductoKitComposicion = Dr.Item("Correlativo"))

                    'If lIndex > -1 Then

                    'End If

                    clsLnProducto_kit_composicion.Eliminar(pObjProductoKit)
                    Limpia_Producto_Kit()
                    ListarProductoKit()
                    pObjProductoKit = New clsBeProducto_kit_composicion
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

    Private Function Valida_Campos_Kit() As Boolean

        Valida_Campos_Kit = False

        Try

            If txtCodPrdHijo.Text = "" Then
                XtraMessageBox.Show("Falta seleccionar un producto para agregar al kit", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            If txtCantPrdHijo.Value = 0 Then
                XtraMessageBox.Show("¡La cantidad de ser mayor a: 0!", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Valida_Campos_Kit = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub ListarProductoKit()

        Try

            grdPrdKit.DataSource = Nothing

            If pBeProductoKitList.Count > 0 Then

                Dim DT As New DataTable("ProductoKitComposicion")
                DT.Columns.Add("Correlativo", GetType(Integer))
                DT.Columns.Add("IdProductoPadre", GetType(Integer))
                DT.Columns.Add("IdProductoHijo", GetType(Integer))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("IdUnidadMedidaBasicaPadre", GetType(Integer))
                DT.Columns.Add("IdUnidadMedidaBasicaHijo", GetType(Integer))
                DT.Columns.Add("U.M.Bas", GetType(String))
                DT.Columns.Add("Cantidad", GetType(Double))

                For Each Obj As clsBeProducto_kit_composicion In pBeProductoKitList
                    DT.Rows.Add(Obj.IdProductoKitComposicion,
                                Obj.IdProductoPadre,
                                Obj.IdProductoHijo,
                                Obj.Producto.Codigo,
                                Obj.Producto.Nombre,
                                Obj.IdUnidadMedidaBasicaPadre,
                                Obj.IdUnidadMedidaBasicaHijo,
                                Obj.Producto.UnidadMedida.Nombre,
                                Obj.Cantidad)
                Next

                grdPrdKit.DataSource = DT

                If GridView11.Columns.Count > 0 Then

                    GridView11.BestFitColumns(True)

                    GridView11.OptionsView.ShowFooter = True

                    GridView11.Columns("Cantidad").SummaryItem.SummaryType = Sum
                    GridView11.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView11.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView11.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                    GridView11.Columns("Correlativo").SummaryItem.SummaryType = Count
                    GridView11.Columns("Correlativo").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView11.Columns("IdProductoPadre").Visible = False
                    GridView11.Columns("IdProductoHijo").Visible = False
                    GridView11.Columns("IdUnidadMedidaBasicaPadre").Visible = False
                    GridView11.Columns("IdUnidadMedidaBasicaHijo").Visible = False

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

    Private Sub GridView11_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView11.RowStyle
        Try

            GridView11.OptionsBehavior.Editable = False
            GridView11.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView11.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridView11.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView11.OptionsSelection.EnableAppearanceHideSelection = True
            GridView11.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView11.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView11.Appearance.FocusedRow.ForeColor = Color.White
            GridView11.Appearance.SelectedRow.ForeColor = Color.White

            GridView11.Appearance.SelectedRow.Options.UseBackColor = True
            GridView11.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub chkGeneraLPAuto_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneraLPAuto.CheckedChanged

        'If Not (ChkEsPallet.Checked) OrElse Not (chkPermitirPaletizar.Checked) Then
        '    XtraMessageBox.Show("La presentación debe ser pallet o permitir paletizar debe ser verdadero para poder habilitar éste parámetro",
        '     Text,
        '     MessageBoxButtons.OK,
        '     MessageBoxIcon.Exclamation)
        'Else
        '    'If chkPermitirPaletizar.Checked Then chkPermitirPaletizar.Checked = False
        'End If

    End Sub

    Private Sub lcmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles lcmbPropietario.EditValueChanged

        Try

            If Set_UmBas_Defecto() Then
                'txtCodigo.Focus()
            End If

            '#GT10042025: recargar Umbas para mostrar estados asociados al propietario
            IMS.Listar_Unidades_Medida(lcmbUnidadMedidaBasica, lcmbPropietario.EditValue)

            '#EJC20220326B: Cambio a lookupedit clasificación.
            IMS.Listar_Clasificaciones(lcmbClasificacion, lcmbPropietario.EditValue)

            '#CKFK20220721: Cambio a lookupedit TipoProducto.
            IMS.Listar_TipoProducto_By_IdPropietario(txtIdTipoProducto, lcmbPropietario.EditValue)

            '#GT02102024: Se valida que solo Nuevo cargue data porque el propietario podria cambiar
            Select Case Modo
                Case TipoTrans.Nuevo
                    '#GT02102024: Cargar la unidad de cobro por el propietario seleccionado
                    IMS.Listar_Unidades_Medida_Es_Um_Cobro(lcmbUnidadMedidaCobro, lcmbPropietario.EditValue, True)
            End Select

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function Set_UmBas_Defecto() As Boolean

        Set_UmBas_Defecto = False

        Try

            If Modo = TipoTrans.Nuevo Then

                Dim vIdUnidadMedida As Integer = 0

                vIdUnidadMedida = clsLnUnidad_medida.Get_UMBas_Default_By_IdPropietario(lcmbPropietario.EditValue)

                If vIdUnidadMedida <> 0 Then
                    lcmbUnidadMedidaBasica.EditValue = vIdUnidadMedida
                    Get_UmBas_By_Codigo(vIdUnidadMedida)
                    Set_UmBas_Defecto = True
                Else
                    Limpia_UMBAs()
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Function

    Private Sub Get_UmBas_By_Codigo(ByVal vIdUnidadMedida As Integer)

        Try

            txtIdUnidadMedidaBasicaRellenado.Text = vIdUnidadMedida
            txtIdUMBasReabastecerCon.Text = vIdUnidadMedida

            Dim vNombreUmBas As String = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(vIdUnidadMedida)
            'txtNombreUnidadMedida.Text = vNombreUmBas
            txtNombreUMBasRellenado.Text = vNombreUmBas
            txtNombreUMBasReabastecerCon.Text = vNombreUmBas

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Limpia_UMBAs()

        lcmbUnidadMedidaBasica.EditValue = ""
        'txtNombreUnidadMedida.Text = ""
        txtIdUnidadMedidaBasicaRellenado.Text = ""
        txtNombreUMBasRellenado.Text = ""
        txtIdUMBasReabastecerCon.Text = ""
        txtNombreUMBasReabastecerCon.Text = ""

    End Sub

    Private Sub frmProducto_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtCodigo.Focus()
    End Sub

    Private Sub mnuDesactivar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDesactivar.ItemClick

        Try

            mnuDesactivar.Enabled = False

            Desactivar_Producto()

            mnuDesactivar.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mnuDesactivar.Enabled = True
        End Try

    End Sub

    Private pListProdImgs As New List(Of clsBeProducto_imagen)

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        Try

            Dim ofd As New OpenFileDialog With {.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files|*.*",
                                                .RestoreDirectory = True,
                                                .Multiselect = True}

            If ofd.ShowDialog = DialogResult.OK Then

                picFoto.Image = Image.FromFile(ofd.FileName)

                For Each t In ofd.FileNames

                    PicImg.ImageLocation = t
                    PicImg.Tag = t

                    Dim ObjI As New clsBeProducto_imagen

                    If pListProdImgs IsNot Nothing AndAlso pListProdImgs.Count > 0 Then
                        ObjI.IdProductoImagen = (From b In pListProdImgs.AsEnumerable Select b.IdProductoImagen).Max + 1
                    Else
                        ObjI.IdProductoImagen = 1
                    End If

                    ObjI.Etiqueta = pBeProducto.Codigo
                    ObjI.IdProducto = pBeProducto.IdProducto
                    ObjI.Imagen = ReadBinaryFile(PicImg.ImageLocation)
                    ObjI.IsNew = True
                    ObjI.User_agr = AP.UsuarioAp.IdUsuario
                    ObjI.Fec_agr = Now
                    pListProdImgs.Add(ObjI)
                    Cargar_Imagenes()

                Next

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Shared Function ReadBinaryFile(ByVal fileName As String) As Byte()

        If Not File.Exists(fileName) Then Return Nothing

        Try
            Dim fs As New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim data() As Byte = New Byte(Convert.ToInt32(fs.Length)) {}
            fs.Read(data, 0, Convert.ToInt32(fs.Length))
            fs.Close()
            Return data
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub Cargar_Imagenes()

        Try

            Dim DT As New DataTable("Imagen")
            DT.Columns.Add("Código", GetType(Integer))
            DT.Columns.Add("Descripción", GetType(String))

            If pListProdImgs IsNot Nothing AndAlso pListProdImgs.Count > 0 Then

                For Each Obj As clsBeProducto_imagen In pListProdImgs
                    DT.Rows.Add(Obj.IdProductoImagen, Obj.Etiqueta)
                Next

            End If

            GrdImagen.DataSource = DT

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub GrdImagen_Click(sender As Object, e As EventArgs) Handles GrdImagen.Click

        Try

            If GridViewImg.RowCount > 0 Then

                Dim Dr As DataRowView = GridViewImg.GetFocusedRow
                Dim Obj As New clsBeProducto_imagen
                Obj = pListProdImgs.Find(Function(b) b.IdProductoImagen = CInt(Dr.Item("Código")))
                Dim ms As MemoryStream = New MemoryStream(Obj.Imagen)
                Dim bm As Bitmap = New Bitmap(ms)
                PicImg.Image = bm
                PicImg.Visible = True

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Function RotateImg(ByVal bmpimage As Bitmap, ByVal angle As Single) As Bitmap

        RotateImg = Nothing

        Try

            Dim w As Integer = bmpimage.Width
            Dim h As Integer = bmpimage.Height
            Dim pf As PixelFormat = Nothing
            pf = bmpimage.PixelFormat
            Dim tempImg As New Bitmap(w, h, pf)
            Dim g As Graphics = Graphics.FromImage(tempImg)
            g.DrawImageUnscaled(bmpimage, 1, 1)
            g.Dispose()
            Dim path As New GraphicsPath()
            path.AddRectangle(New RectangleF(0.0F, 0.0F, w, h))
            Dim mtrx As New Matrix()
            'Using System.Drawing.Drawing2D.Matrix class

            mtrx.Rotate(angle)
            Dim rct As RectangleF = path.GetBounds(mtrx)
            Dim newImg As New Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height), pf)
            g = Graphics.FromImage(newImg)
            g.TranslateTransform(-rct.X, -rct.Y)
            g.RotateTransform(angle)
            g.InterpolationMode = InterpolationMode.HighQualityBilinear
            g.DrawImageUnscaled(tempImg, 0, 0)
            g.Dispose()
            tempImg.Dispose()

            Return newImg

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

        Try

            If GridViewImg.RowCount > 0 Then

                Dim Dr As DataRow = GridViewImg.GetFocusedDataRow

                SplashScreenManager.CloseForm(False)

                If XtraMessageBox.Show(String.Format("¿Eliminar la imagen {0}", Dr.Item("Descripción")),
                                       Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim lIndex As Integer = -1

                    lIndex = pListProdImgs.FindIndex(Function(i) i.IdProductoImagen = CInt(Dr.Item("Código")))

                    If lIndex > -1 Then
                        If pListProdImgs(lIndex).IdProductoImagen > 0 Then
                            clsLnProducto_imagen.Eliminar(pListProdImgs(lIndex))
                        End If
                        pListProdImgs.RemoveAt(lIndex)
                        Cargar_Imagenes()
                        PicImg.Image = Nothing
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    'GT13072022_19_20: se llena el combo de proveedor con el id del propietario para rellenar codigos barra
    Private Sub cmbProveedor_EditValueChanged(sender As Object, e As ChangingEventArgs) Handles cmbProveedor.EditValueChanged

        Try
            '#EJC20210317: Colocar el proveedor por defecto asociado al código de propietario.
            Dim IdProveedor = lcmbPropietario.EditValue
            DT = clsLnProveedor.Get_All_For_Combo_By_IdProveedor_and_Bodega(AP.IdBodega, IdProveedor)

            If DT.Rows.Count > 0 Then
                cmbProveedor.Properties.DisplayMember = "Nombre"
                cmbProveedor.Properties.ValueMember = "IdProveedor"
                cmbProveedor.Properties.DataSource = DT
                cmbProveedor.ItemIndex = 1
            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub lcmbUnidadMedidaCobro_KeyDown(sender As Object, e As KeyEventArgs) Handles lcmbUnidadMedidaCobro.KeyDown
        Try

            If e.KeyCode = Keys.Back Then
                lcmbUnidadMedidaCobro.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try
    End Sub

#End Region

    Private Sub Cargar_Talla_Color(IdProducto As Integer)

        Try

            Dim dt As New DataTable
            dt = clsLnProducto_talla_color.Get_All_Dt_By_IdProducto(IdProducto)

            dgridTallaColor.DataSource = dt

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Talla_Color_Con_Imagen(IdProducto As Integer, vRutaCDN As String)
        Try
            Dim dt As New DataTable
            dt = clsLnProducto_talla_color.Get_All_Dt_By_IdProducto(IdProducto)

            ' Agregar columna de imagen
            If Not dt.Columns.Contains("Imagen") Then
                dt.Columns.Add("Imagen", GetType(Image))
            End If
            If Not dt.Columns.Contains("RutaImagen") Then
                dt.Columns.Add("RutaImagen", GetType(String))
            End If
            ' Mostrar datos primero, luego cargar imágenes en background
            dgridTallaColor.DataSource = dt
            dgridTallaColor.RefreshDataSource()

            ' Iniciar tarea en segundo plano
            Task.Run(Sub()
                         For Each row As DataRow In dt.Rows
                             Dim codigoSKU As String = row("CodigoSKU").ToString()
                             Dim productoBase As String = codigoSKU
                             Dim talla As String = ""
                             Dim color As String = ""

                             If codigoSKU.Length >= 13 Then
                                 productoBase = codigoSKU.Substring(0, 10)
                                 talla = codigoSKU.Substring(10, 3)
                                 If codigoSKU.Length > 13 Then
                                     color = codigoSKU.Substring(13)
                                 End If
                             ElseIf codigoSKU.Length >= 10 Then
                                 productoBase = codigoSKU.Substring(0, 10)
                             End If

                             Dim patrones As New List(Of String)

                             If talla <> "" AndAlso color <> "" Then
                                 patrones.Add("._" & productoBase & "-" & talla & "-" & color & "*.png")
                                 patrones.Add(productoBase & "-" & talla & "-" & color & "*.png")
                             End If

                             If talla <> "" Then
                                 patrones.Add("._" & productoBase & "-" & talla & "*.png")
                                 patrones.Add(productoBase & "-" & talla & "*.png")
                             End If

                             patrones.Add("._" & productoBase & "*.png")
                             patrones.Add(productoBase & "*.png")

                             Dim archivoEncontrado As String = Nothing
                             For Each patron In patrones
                                 Dim archivos() As String = Directory.GetFiles(vRutaCDN, patron)
                                 If archivos.Length > 0 Then
                                     archivoEncontrado = archivos(0)
                                     Exit For
                                 End If
                             Next

                             Try
                                 If Not String.IsNullOrEmpty(archivoEncontrado) Then
                                     Using fs As New FileStream(archivoEncontrado, FileMode.Open, FileAccess.Read)
                                         Dim img As Image = Image.FromStream(fs)
                                         row("Imagen") = CType(img.Clone(), Image)
                                         row("RutaImagen") = archivoEncontrado ' Para doble clic
                                     End Using
                                 Else
                                     row("Imagen") = Nothing
                                     row("RutaImagen") = Nothing
                                 End If
                             Catch ex As Exception
                                 row("Imagen") = Nothing
                                 row("RutaImagen") = Nothing
                             End Try
                         Next

                         ' Refrescar UI en hilo principal
                         dgridTallaColor.Invoke(Sub()
                                                    dgridTallaColor.RefreshDataSource()
                                                End Sub)
                     End Sub)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgridTallaColor_DoubleClick(sender As Object, e As EventArgs) Handles dgridTallaColor.DoubleClick
        Dim view As ColumnView = CType(dgridTallaColor.FocusedView, ColumnView)
        Dim rowHandle As Integer = view.FocusedRowHandle
        If rowHandle < 0 Then Exit Sub

        Dim rutaImagen As Object = view.GetRowCellValue(rowHandle, "RutaImagen")
        If rutaImagen IsNot Nothing AndAlso File.Exists(rutaImagen.ToString()) Then
            Dim previewForm As New DevExpress.XtraEditors.XtraForm()
            previewForm.Text = "Vista previa de imagen"
            previewForm.StartPosition = FormStartPosition.CenterParent
            previewForm.Size = New Size(700, 700)

            Dim pictureEdit As New DevExpress.XtraEditors.PictureEdit()
            pictureEdit.Dock = DockStyle.Fill
            pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
            pictureEdit.Image = Image.FromFile(rutaImagen.ToString())

            previewForm.Controls.Add(pictureEdit)
            previewForm.ShowDialog()
        End If
    End Sub


End Class