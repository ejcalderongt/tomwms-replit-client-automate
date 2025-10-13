Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports DevExpress.Mvvm.Native
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class frmRecepcion

    Public pRecepcionInmediata As Boolean
    Public pIdOrdenCompraEnc As Integer
    Public pIdPropietarioBodega As Integer
    Public Property IdOperadorBodegaDefecto As Integer = 0

    '----------------------------------------------------------------'
    Private pListObjOrdeCompraDet As New List(Of clsBeTrans_oc_det)
    '----------------------------------------------------------------'    
    Private lBeTransRecDet As New List(Of clsBeTrans_re_det)
    Private pBeTR As New clsBeTrans_re_tr
    Private pListRecImgs As New List(Of clsBeTrans_re_img)
    Private pListOpe As New List(Of clsBeTrans_re_op)
    Private pListRecFact As New List(Of clsBeTrans_re_fact)
    Private pListBeStockRec As New List(Of clsBeStock_rec)
    Private pListBeStockSeRec As New List(Of clsBeStock_se_rec)
    Private plistBeReDetParametros As New List(Of clsBeTrans_re_det_parametros)
    Private pListBeProductoPallet As New List(Of clsBeProducto_pallet)

    Private ListPC As New List(Of clsProductoCargar)

    Private pListObjProductoP As New List(Of clsBeProducto_Presentacion)

    Private DTOperadores As DataTable

    Public gBeRecepcionEnc As New clsBeTrans_re_enc
    Public gBeOrdenCompra As New clsBeTrans_oc_enc

    Private BeTransOcEnc As New clsBeTrans_oc_enc
    Private oDateTimePicker As DateTimePicker
    Private lMaxIdRecepcionDetParametro As Integer = 0
    Private pTransHH As Boolean
    Private pSelecciono As Boolean

    Private DgComboPresentacion As New DataGridViewComboBoxCell()
    Private DgComboEstado As New DataGridViewComboBoxCell()
    Private DgComboUnidadMedida As New DataGridViewComboBoxCell()
    Private DgComboArancel As New DataGridViewComboBoxCell()

    Public IdBodega As Integer = 0

    Private pBodega As New clsBeBodega

    Public Delegate Sub Listar_Recepciones()
    Public Property Listar As Listar_Recepciones
    Private lBeProdPallet As New List(Of clsBeProducto_pallet)
    Private pBeTms_ticket As New clsBeTms_ticket
    Private pBeEmpresa_Transporte_Piloto As New clsBeEmpresa_transporte_pilotos
    Private pBeEmpresa_Transporte_Vehiculo As New clsBeEmpresa_transporte_vehiculos

    Private ProductoGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ProductoGridLookUpEditRec As New RepositoryItemGridLookUpEdit

    Private cmbProductoGridView As New GridView
    Private PropietarioGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PresentacionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ProductoLoteGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private MotivoDevolcuionGridLookUpEdit As New RepositoryItemGridLookUpEdit

    Private EstadoGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtNoLineaGrid As New RepositoryItemTextEdit
    Private IdUmBasGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtCantidadGrid As New RepositoryItemSpinEdit
    Private txtLoteGrid As New RepositoryItemTextEdit
    Private txtObservacionGrid As New RepositoryItemTextEdit
    Private txtLicPlateGrid As New RepositoryItemTextEdit
    Private txtFechaVenceGrid As New RepositoryItemDateEdit
    Private txtCostoGrid As New RepositoryItemSpinEdit
    Private txtCostoOCGrid As New RepositoryItemSpinEdit
    Private txtCompraEncGrid As New RepositoryItemDateEdit
    Private txtCompraEncDetGrid As New RepositoryItemDateEdit
    Private txtValorFOBGrid As New RepositoryItemSpinEdit
    Private txtValorDAIGrid As New RepositoryItemSpinEdit
    Private txtTotalGrid As New RepositoryItemSpinEdit
    Private BeBodega As New clsBeBodega()
    Private Control_Poliza As Boolean = False
    Private BeTipoDocumento As New clsBeTrans_oc_ti
    Private vBeProductoEstado As New clsBeProducto_estado
    Private NavConfigEnc As New clsBeI_nav_config_enc
    Private Guardar_Recepcion_Inmediata_BOF As Boolean
    Private lProductosRecepcion As New List(Of clsBeProductosRecepcion)
    Public Shared pImpresoraProdSeleccionada As String = ""
    Public Shared pImpresoraLicSeleccionada As String = ""
    Public Shared pIdImpresoraSojet As String = ""
    Private _filtroCodigoBarra As String = ""
    Private Cargando As Boolean = False
    Dim DT As New DataTable("Lotes")
    Private pBeTipo_Tarea_HH As New clsBeTrans_re_tr

    Private Sub lnk_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnk.LinkClicked

        Dim tem_IdOrdencompra = txtIdOrdenCompra.Text
        Dim tem_OC = txtOC.Text

        txtIdOrdenCompra.Text = String.Empty
        txtOC.Text = String.Empty

        Try

            Dim frmOrdenCompra As New frmOrdenCompra_List() With {.pIdBodega = cmbBodega.EditValue, .pIdPropietario = cmbPropietario.Tag, .Modo = frmOrdenCompra_List.pModo.Seleccion}

            If frmOrdenCompra.ShowDialog() = DialogResult.OK Then

                gBeOrdenCompra = frmOrdenCompra.gBeOrdenCompra

                If gBeOrdenCompra IsNot Nothing AndAlso gBeOrdenCompra.IdOrdenCompraEnc > 0 Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                    BeTipoDocumento = clsLnTrans_oc_ti.GetSingle(gBeOrdenCompra.IdTipoIngresoOC)

                    If gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.CERRADA Then

                        'Si el IdEstadoOc=4 osea si esta finalizada o cerrada entonces no dejar crear recepción con esta OC

                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show(String.Format("El documento de ingreso: {0} se encuentra finalizado. No puede generar Recepción con esta documento.", gBeOrdenCompra.IdOrdenCompraEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    ElseIf gBeOrdenCompra.ExisteRecepcionNoFinalizada Then

                        SplashScreenManager.CloseForm(False)
                        txtIdOrdenCompra.Text = String.Empty
                        txtOC.Text = String.Empty
                        XtraMessageBox.Show("Existe una recepción que aún no se ha finalizado. Favor de finalizarla antes de crear otra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Else

                        If Not BeTipoDocumento Is Nothing Then
                            If BeTipoDocumento.Requerir_Documento_Ref Then
                                If (gBeOrdenCompra.IdNoDocumentoRef = 0 AndAlso gBeOrdenCompra.IdPedidoEncDevolucion = 0 AndAlso gBeOrdenCompra.No_Documento_Devolucion = "") Then
                                    XtraMessageBox.Show(String.Format("El documento de ingreso: {0} requiere documento de referencia y no se ha definido. No puede generar Recepción con este documento de ingreso.", gBeOrdenCompra.IdOrdenCompraEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End If

                        ValidaOC(gBeOrdenCompra.IdOrdenCompraEnc)

                    End If

                    If gBeOrdenCompra.EsDevolucion Then
                        SplashScreenManager.Default.SetWaitFormCaption("Devolución")
                    Else
                        SplashScreenManager.Default.SetWaitFormCaption("Documento de Ingreso")
                    End If

                    BeTransOcEnc = clsLnTrans_oc_enc.Get_Orden_Compra(gBeOrdenCompra.IdOrdenCompraEnc)
                    '#GT31102023: asocio el tipodoc al objeto de la OC, para validar tiempos proveedor
                    BeTransOcEnc.TipoIngreso = New clsBeTrans_oc_ti
                    BeTransOcEnc.TipoIngreso = BeTipoDocumento

                    txtIdOrdenCompra.Text = BeTransOcEnc.IdOrdenCompraEnc
                    txtOC.Text = String.Format("{0} {1}", BeTransOcEnc.Referencia, BeTransOcEnc.No_Documento)
                    pListObjOrdeCompraDet = BeTransOcEnc.DetalleOC.ToList()

                    Cargar_Detalle_OC()

                    '#EJC20220330:Get the parameter by type of document.
                    chkEscanearUbicacionRec.Checked = BeTipoDocumento.Requerir_Ubic_Rec_Ingreso

                    cmbBodega.EditValue = BeTransOcEnc.IdBodega
                    cmbPropietario.EditValue = BeTransOcEnc.PropietarioBodega.IdPropietarioBodega
                    cmbBodega.Enabled = False
                    cmbPropietario.Enabled = False

                    grpDatosFiscalSAT.Visible = True

                    Actualizar_LookupGridProducto()

                    Actualizar_LookupGridEstadoProducto()

                    '#CKFK20240214 Agregué la funcionalidad de que se guarde la recepción cuando sea este tipo de transacción MCOC00
                    If txtIdTipoTR.Text = clsBeTrans_re_enc.pTipoTrans.MCOC00.ToString Then

                        chkRecepcionManual.Checked = True

                        '#CKFK 20210624 Se llama a la función creada por EJC para habilitar o no el stock basado en las reglas del propietario
                        Check_Reglas_Propietario_Ingreso()

                        chkHabilitaStock.Checked = True

                        If txtIdOrdenCompra.Text <> "" Then
                            '#GT08022024: guardar el encabezado al recibir por BOF.
                            Guardar_Recepcion_Inmediata_BOF = True
                            Guardar_Recepcion_Inmediata()

                            If chkDI2REC.Checked Then

                                If XtraMessageBox.Show("¿Convertir el documento de ingreso en recepción con valores por defecto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                                    Dim lDetSample As New List(Of clsBeTrans_re_det)
                                    lDetSample = clsLnTrans_re_det.DI_To_Rec(lblC.Text,
                                                                             gBeOrdenCompra.IdOrdenCompraEnc,
                                                                             AP.UsuarioAp.IdUsuario)

                                    Cargar_Detalle_Recepcion(lDetSample)

                                End If

                            End If

                        End If

                    End If

                Else
                    cmbBodega.Enabled = True
                    cmbPropietario.Enabled = True
                End If

                SplashScreenManager.CloseForm(False)

                If BeTransOcEnc IsNot Nothing AndAlso BeTransOcEnc.IsNew = False AndAlso BeTransOcEnc.IdOrdenCompraEnc > 0 Then
                    ListarTipoTransaccion(True, IIf(BeTransOcEnc.EsDevolucion, "DEVOLUCION", "INGRESO"))
                End If

                XtraMessageBox.Show("Documento de ingreso cargado correctamente",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)

                DgridDetalleRec.Enabled = True : lblStatus.Text = ""



            Else
                'GT 02062021 Si se carga la modal, pero no se selecciona nada, se reestablecen los valores orginales.
                txtIdOrdenCompra.Text = tem_IdOrdencompra
                txtOC.Text = tem_OC

            End If

            frmOrdenCompra.Close()
            frmOrdenCompra.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub ValidaOC(ByVal pIdOrdenCompra As Integer)

        Try

            ListPC = New List(Of clsProductoCargar)

            Dim ListaOC As List(Of clsBeTrans_oc_det) = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompra).ToList
            Dim ListaR As List(Of clsBeTrans_re_det) = clsLnTrans_re_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompra).ToList

            If ListaOC IsNot Nothing AndAlso ListaR IsNot Nothing Then

                For Each bo As clsBeTrans_oc_det In ListaOC

                    For Each boR As clsBeTrans_re_det In ListaR

                        If bo.No_Linea = boR.No_Linea Then

                            If bo.Cantidad_recibida < boR.cantidad_recibida Then

                                '#EJC20180113: No_Linea agregado en clsProductoCargar
                                Dim ObjPC As New clsProductoCargar() With
                                    {.IdProductoBodega = bo.IdProductoBodega,
                                    .CantidadACargar = bo.Cantidad_recibida - boR.cantidad_recibida,
                                    .No_Linea = bo.No_Linea}

                                ListPC.Add(ObjPC)

                            End If

                        End If

                    Next

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Sub

    Private Sub ListarTipoTransaccion(ByVal pConRef As Boolean, ByVal pTipoTrans As String)

        Try

            If txtIdTipoTR.Text.Trim = "" Then

                Dim TR As New frmTipoTransaccion_List() _
                    With
                    {.Modo = frmUnidad_MedidaList.pModo.Seleccion,
                    .pFiltro = True,
                    .pConReferencia = pConRef,
                    .pTipoTrans = pTipoTrans}

                TR.ShowDialog()

                If TR.DialogResult = DialogResult.OK Then

                    If TR.pObj IsNot Nothing AndAlso String.IsNullOrEmpty(TR.pObj.IdTipoTransaccion) = False Then

                        '#EJC20171021_0308PM: Agregado para validar al guardar si la orden de compra fue seleccionada o nó.
                        pBeTR = TR.pObj

                        txtIdTipoTR.Text = TR.pObj.IdTipoTransaccion
                        txtDescripcionTR.Text = TR.pObj.Descripcion
                        Configura_Opcion_Tipo_Rec(TR.pObj)

                    End If

                    TR.Close()
                    TR.Dispose()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

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

    Private Sub grdListaRecepcion_CellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs) Handles DgridDetalleRec.CellBeginEdit

        Try

            DgridDetalleRec.EndEdit()

            If DgridDetalleRec.Focused AndAlso DgridDetalleRec.CurrentCell.OwningColumn.Name = "FechaVencimiento" Then

                oDateTimePicker.Location = DgridDetalleRec.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False).Location
                oDateTimePicker.Visible = True

                If DgridDetalleRec.CurrentCell.Value IsNot DBNull.Value AndAlso IsDate(DgridDetalleRec.CurrentCell.Value) Then
                    oDateTimePicker.Value = DgridDetalleRec.CurrentCell.Value
                Else
                    oDateTimePicker.Value = Today
                End If

            Else
                oDateTimePicker.Visible = False
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub grdListaRecepcion_CellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgridDetalleRec.CellEndEdit

        Try

            If DgridDetalleRec.CurrentCell.OwningColumn.Name = "FechaVencimiento" Then
                DgridDetalleRec.CurrentCell.Value = oDateTimePicker.Value.Date
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Sub oDateTimePicker_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

        If DgridDetalleRec.CurrentCell.OwningColumn.Name = "FechaVencimiento" Then
            DgridDetalleRec.CurrentCell.Value = oDateTimePicker.Value
        End If

    End Sub

    ReadOnly TransaccionesQueNecesitanHH As New List(Of String)
    Private Sub Llena_Tipos_Tareas_Que_Necesitan_HH()

        Try

            TransaccionesQueNecesitanHH.Add("HCOC00")
            TransaccionesQueNecesitanHH.Add("HCOD00")
            TransaccionesQueNecesitanHH.Add("HHSR00")
            TransaccionesQueNecesitanHH.Add("HSOC00")
            TransaccionesQueNecesitanHH.Add("HSOD00")

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    '#CM20171026_1228PM: Agregué IdUbicación por defecto para recepción y picking
    '#EJC20171205_0635AM: Agregué Try, renombre.
    Private Sub Cargar_Ubicacion_Defecto_Recepcion()

        Try

            txtIdUbicacion.Text = BeBodega.Ubic_recepcion
            txtIdUbicacion_Validated(txtIdUbicacion, Nothing)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub CargaOCInmediata()

        Try

            BeTransOcEnc = clsLnTrans_oc_enc.Get_Orden_Compra_By_Propietario(pIdOrdenCompraEnc, pIdPropietarioBodega)

            '#GT26032025: en carga inmediata el tipodoc viene vacio, cargarlo para validar si aplica Estado por defecto
            BeTipoDocumento = clsLnTrans_oc_ti.GetSingle(BeTransOcEnc.IdTipoIngresoOC)
            BeTransOcEnc.TipoIngreso = New clsBeTrans_oc_ti
            BeTransOcEnc.TipoIngreso = BeTipoDocumento


            If IdOperadorBodegaDefecto <> 0 Then
                BeTransOcEnc.IdOperadorBodegaDefecto = IdOperadorBodegaDefecto
            End If

            If BeTransOcEnc IsNot Nothing Then

                '#EJC20210613: Compartir objecto de la OC.
                gBeOrdenCompra = BeTransOcEnc.Clone()
                txtIdOrdenCompra.Text = BeTransOcEnc.IdOrdenCompraEnc
                txtOC.Text = BeTransOcEnc.No_Documento

                lnk.Enabled = False
                txtIdOrdenCompra.Enabled = False

                Control_Poliza = gBeOrdenCompra.Control_Poliza

                '#GT25032025: validar que el propietario bodega del ingreso, sea el propietario asociado en el tipo de documento de ingreso.
                If gBeOrdenCompra.IdPropietarioBodega = BeTipoDocumento.IdPropietario Then
                    If gBeOrdenCompra.TipoIngreso.IdProductoEstado > 0 Then
                        txtIdEstadoDefectoRecepcion.Text = gBeOrdenCompra.TipoIngreso.IdProductoEstado
                        txtNombreEstado.Text = clsLnProducto_estado.GetNombreByIdEstado(gBeOrdenCompra.TipoIngreso.IdProductoEstado)
                        txtIdEstadoDefectoRecepcion.Enabled = False
                        txtNombreEstado.Enabled = False
                    End If
                End If

                'If gBeOrdenCompra.TipoIngreso.IdProductoEstado > 0 Then
                '    txtIdEstadoDefectoRecepcion.Text = gBeOrdenCompra.TipoIngreso.IdProductoEstado
                '    txtNombreEstado.Text = clsLnProducto_estado.GetNombreByIdEstado(gBeOrdenCompra.TipoIngreso.IdProductoEstado)
                '    txtIdEstadoDefectoRecepcion.Enabled = False
                '    txtNombreEstado.Enabled = False
                'Else
                '    txtIdEstadoDefectoRecepcion.Enabled = True
                '    txtNombreEstado.Enabled = True
                'End If

                '#GT26032025: bloquear el combo y el text

                Cargar_Detalle_OC()

            Else

                XtraMessageBox.Show("No se pudo obtener la orden de compra del propietario", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        PicImg.Visible = False

        Try

            Dim gFile As New OpenFileDialog() With {.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files|*.*"}
            gFile.ShowDialog()

            If gFile.FileName.Length <> 0 Then

                Dim iNombreDoc As String = InputBox("Ingrese una descripción:", Text, "")

                If String.IsNullOrEmpty(iNombreDoc) = False Then

                    PicImg.ImageLocation = gFile.FileName
                    PicImg.Tag = gFile.FileName

                    Dim ObjI As New clsBeTrans_re_img

                    If pListRecImgs IsNot Nothing AndAlso pListRecImgs.Count > 0 Then
                        ObjI.IdImagen = (From b In pListRecImgs.AsEnumerable Select b.IdImagen).Max + 1
                    Else
                        ObjI.IdImagen = 1
                    End If

                    ObjI.Observacion = iNombreDoc

                    ObjI.Imagen = ReadBinaryFile(PicImg.ImageLocation)
                    ObjI.IsNew = True
                    ObjI.User_agr = AP.UsuarioAp.IdUsuario
                    ObjI.Fec_agr = Now
                    pListRecImgs.Add(ObjI)
                    Cargar_Imagenes()

                End If

            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("No selecciono ninguna imagen.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
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

            If pListRecImgs IsNot Nothing AndAlso pListRecImgs.Count > 0 Then

                For Each Obj As clsBeTrans_re_img In pListRecImgs
                    DT.Rows.Add(Obj.IdImagen, Obj.Observacion)
                Next

            End If

            GrdImagen.DataSource = DT

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Facturas()

        Try

            grdListaFactura.SuspendLayout() : grdListaFactura.Rows.Clear()

            If pListRecFact IsNot Nothing AndAlso pListRecFact.Count > 0 Then

                For Each f As clsBeTrans_re_fact In pListRecFact
                    grdListaFactura.Rows.Add(f.IdFacturaRecepcion, f.IdRecepcionEnc, f.Orden, f.NoFactura, f.Observacion, f.Completa)
                Next

            End If

            grdListaFactura.ResumeLayout()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

        Try

            If GridViewImg.RowCount > 0 Then

                Dim Dr As DataRow = GridViewImg.GetFocusedDataRow

                SplashScreenManager.CloseForm(False)

                If XtraMessageBox.Show(String.Format("¿Eliminar la imagen {0}", Dr.Item("Descripción")),
                                       Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim lIndex As Integer = -1

                    lIndex = pListRecImgs.FindIndex(Function(i) i.IdImagen = CInt(Dr.Item("Código")))

                    If lIndex > -1 Then
                        If pListRecImgs(lIndex).IdRecepcionEnc > 0 Then
                            clsLnTrans_oc_imagen.Delete(pListRecImgs(lIndex).IdRecepcionEnc, pListRecImgs(lIndex).IdImagen)
                        End If
                        pListRecImgs.RemoveAt(lIndex)
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

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        cmdGuardar.Enabled = False
        Guardar_Recepcion(True)
        cmdGuardar.Enabled = True
    End Sub

    Private Sub Guardar_Recepcion(ByVal Preguntar As Boolean)

        Try
            'ubicame donde se llena el lookup de producto. xfa para el nuevo grid.
            If Datos_Correctos() Then

                SplashScreenManager.CloseForm(False)

                If Preguntar Then

                    If XtraMessageBox.Show("¿Guardar Recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                        '#EJC20220407: Prevent dataloss by an open on the middle transaction.
                        If Modo = TipoTrans.Editar Then

                            tmrActualizarDatosRecepcion.Enabled = False

                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                            SplashScreenManager.Default.SetWaitFormDescription("Actualizando ingreso...")

                            gBeRecepcionEnc = clsLnTrans_re_enc.GetSingle(gBeRecepcionEnc.IdRecepcionEnc)

                            Cargar_Datos()

                            Application.DoEvents()

                        End If

                        If Guardar() Then

                            If Not Guardar_Recepcion_Inmediata_BOF Then

                                SplashScreenManager.CloseForm(False)

                                XtraMessageBox.Show("Se ha creado la recepción", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                If Listar IsNot Nothing Then
                                    Listar.Invoke()
                                End If

                                DialogResult = DialogResult.OK

                                Close()

                            End If

                        End If

                    End If

                Else

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                    If Guardar() Then

                        If Not Guardar_Recepcion_Inmediata_BOF Then

                            SplashScreenManager.CloseForm(False)

                            XtraMessageBox.Show("Se ha creado la recepción: " & gBeRecepcionEnc.IdRecepcionEnc, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            If Listar IsNot Nothing Then
                                Listar.Invoke()
                            End If

                            DialogResult = DialogResult.OK

                            Close()

                        Else

                            gBeRecepcionEnc = clsLnTrans_re_enc.GetSingle(gBeRecepcionEnc.IdRecepcionEnc)

                            '#EJC202402122010: Modo edición.
                            Modo_Edicion()

                            Set_Columnas_Grid_Detalle_Documento_Recepcion()

                            Modo = TipoTrans.Editar

                            tmrActualizarDatosRecepcion.Enabled = False

                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                            SplashScreenManager.Default.SetWaitFormDescription("Actualizando ingreso...")

                            Cargar_Datos()

                            Application.DoEvents()

                        End If

                    End If

                End If


            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    ''' <summary>
    ''' #EJC202311061354: Validar campos requeridos por SAT.
    ''' </summary>
    ''' <returns></returns>
    Private Function Datos_Correctos_Por_Tipo_Ingreso() As Boolean

        Datos_Correctos_Por_Tipo_Ingreso = False

        Try

            If BeBodega.Es_Bodega_Fiscal Then

                If Not BeTipoDocumento Is Nothing Then

                    If BeTipoDocumento.Control_Poliza Then

                        If txtCartaCupo.Text.Trim = "" Then
                            txtCartaCupo.Focus()
                            Throw New Exception("ERROR_202311061352: El número de carta de cupo está vacío y es requerido según la configuración de bodega (es_fiscal=1) y tipo de documento (requiere_poliza=1)")
                        End If

                        If txtNoContenedor.Text.Trim = "" Then
                            txtNoContenedor.Focus()
                            Throw New Exception("ERROR_202311061353: El número de T.C. está vacío y es requerido según la configuración de bodega (es_fiscal=1) y tipo de documento (requiere_poliza=1)")
                        End If

                    End If

                End If

            End If

            Datos_Correctos_Por_Tipo_Ingreso = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            '#GT26092023: Es valido actualizar el marchamo, siempre que no este cerrada/anulada la recepción
            'si no esta cerrada/anulada el input esta abierto a modificación, aplica para carta cupo
            If txtNoMarchamo.Text <> "" AndAlso txtNoMarchamo.Text <> "N/A" Then
                gBeRecepcionEnc.No_Marchamo = txtNoMarchamo.Text
            End If

            gBeRecepcionEnc.Carta_Cupo = txtCartaCupo.Text

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf BeTransOcEnc Is Nothing AndAlso pBeTR.ConRef Then
                XtraMessageBox.Show("Seleccione Orden de Compra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtIdTipoTR.Text.Trim) Then
                XtraMessageBox.Show("Seleccione Tipo de Transacción.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdTipoTR.Focus()
            ElseIf String.IsNullOrEmpty(txtIdUbicacion.Text.Trim) Then
                XtraMessageBox.Show("Seleccione Ubicación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdUbicacion.Focus()
                xtrRecepcion.SelectedTabPageIndex = 0
            ElseIf Grid_Tiene_Errores() Then
                XtraMessageBox.Show("Existen errores en el detalle, corrija antes de guardar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf pTransHH Then

                '#CKFK 20200418 Se agregó este linq para que la validación de que al menos se seleccione un operador con HH funcione correctamente
                Dim query =
                    From c In pListOpe
                    Where c.UsaHH = True

                ' If pListOpe.Count = 0 Then
                If query.Count = 0 Then

                    XtraMessageBox.Show("Seleccione un operador con utilización de HandHeld.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    If xtrRecepcion.TabPages.Count >= 4 Then
                        xtrRecepcion.SelectedTabPageIndex = 5
                    Else
                        xtrRecepcion.SelectedTabPageIndex = 2
                    End If

                ElseIf txtIdOrdenCompra.Text = "" Then

                    XtraMessageBox.Show("Seleccione un documento de ingreso.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    xtrRecepcion.SelectedTabPageIndex = 0

                Else
                    Datos_Correctos = True
                End If
            Else
                Datos_Correctos = True
            End If

            '#GT26062024: Para cealsa no aplica que exija en update carta y contenedor.
            'If Datos_Correctos Then
            '    Datos_Correctos = Datos_Correctos_Por_Tipo_Ingreso()
            'End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Function

    Private Sub Cargar_Datos()

        Try

            If gBeRecepcionEnc IsNot Nothing Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Leyendo Recepción...")

                lblC.Text = gBeRecepcionEnc.IdRecepcionEnc

                cmbBodega.EditValue = gBeRecepcionEnc.PropietarioBodega.IdBodega
                cmbPropietario.Tag = gBeRecepcionEnc.PropietarioBodega.IdPropietario
                cmbPropietario.EditValue = gBeRecepcionEnc.PropietarioBodega.IdPropietarioBodega
                cmbBodega.Enabled = False
                cmbPropietario.Enabled = False

                If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then

                    BeTransOcEnc = gBeRecepcionEnc.OrdenCompraRec.OC

                    If Not BeTransOcEnc Is Nothing Then

                        '#EJC20220301: Set BeBodega en Editar.
                        Dim pIdBodega As Integer = cmbBodega.EditValue
                        BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)

                        txtIdOrdenCompra.Text = BeTransOcEnc.IdOrdenCompraEnc

                        txtOC.Text = String.Format("{0} {1}", BeTransOcEnc.Referencia, BeTransOcEnc.No_Documento)
                        pListObjOrdeCompraDet = BeTransOcEnc.DetalleOC

                        Cargar_Detalle_OC()

                        txtNoDocumento.Text = gBeRecepcionEnc.OrdenCompraRec.No_docto
                        chkRecepcionManual.Checked = gBeRecepcionEnc.OrdenCompraRec.Recepcion_manual
                        dtmHoraIhh.Value = gBeRecepcionEnc.OrdenCompraRec.Hora_ini_hh
                        dtmHoraFhh.Value = gBeRecepcionEnc.OrdenCompraRec.Hora_fin_hh

                        Control_Poliza = BeTransOcEnc.Control_Poliza

                        BeTipoDocumento = clsLnTrans_oc_ti.GetSingle(BeTransOcEnc.IdTipoIngresoOC)

                    End If

                End If

                Application.DoEvents()

                chkHabilitaStock.Checked = gBeRecepcionEnc.Habilitar_Stock

                chkMostrarCantidadPI.Checked = gBeRecepcionEnc.Mostrar_Cantidad_Esperada

                '#CKFK20251012 Esta validación es la que no aplica, no tiene nada que ver si se está editando o no una recepcion
                'If Modo = TipoTrans.Editar Then
                '    chkHabilitaStock.Enabled = False
                'End If

                txtIdOrdenCompra.Enabled = False

                cmbMuelle.EditValue = gBeRecepcionEnc.IdMuelle

                If gBeRecepcionEnc.IdUbicacionRecepcion > 0 Then
                    txtIdUbicacion.Text = gBeRecepcionEnc.IdUbicacionRecepcion
                    txtNombreUbicacion.Text = gBeRecepcionEnc.UbicacionRecepcion
                End If

                '#CKFK20231228 Llenar la el estado por defecto
                If gBeRecepcionEnc.IdEstado_Defecto_Recepcion > 0 Then
                    txtIdEstadoDefectoRecepcion.Text = gBeRecepcionEnc.IdEstado_Defecto_Recepcion
                    txtNombreEstado.Text = clsLnProducto_estado.GetNombreByIdEstado(gBeRecepcionEnc.IdEstado_Defecto_Recepcion)
                End If

                If String.IsNullOrEmpty(gBeRecepcionEnc.IdTipoTransaccion) = False Then
                    txtIdTipoTR.Text = gBeRecepcionEnc.IdTipoTransaccion
                    txtDescripcionTR.Text = gBeRecepcionEnc.Descripcion
                    HabilitarTabOperador(gBeRecepcionEnc.IdTipoTransaccion)
                Else
                    txtIdTipoTR.Text = String.Empty
                    txtDescripcionTR.Text = String.Empty
                End If

                dtmFechaRecepcion.EditValue = gBeRecepcionEnc.Fecha_recepcion
                dtmHoraI.Value = gBeRecepcionEnc.Hora_ini_pc
                dtmHoraF.Value = gBeRecepcionEnc.Hora_fin_pc
                chkMuestraPrecio.Checked = gBeRecepcionEnc.Muestra_precio
                dtmFechaTarea.EditValue = gBeRecepcionEnc.Fecha_tarea
                chkTomarFoto.Checked = gBeRecepcionEnc.Tomar_fotos
                chkEscanearUbicacionRec.Checked = gBeRecepcionEnc.Escanear_rec_ubic
                chkParaPorCodigo.Checked = gBeRecepcionEnc.Para_por_codigo
                txtObservacion.Text = gBeRecepcionEnc.Observacion
                txtNoMarchamo.Text = gBeRecepcionEnc.No_Marchamo
                User_agrTextEdit.Text = gBeRecepcionEnc.User_agr
                Fec_agrDateEdit.Text = gBeRecepcionEnc.Fec_agr
                User_modTextEdit.Text = gBeRecepcionEnc.User_mod
                Fec_modDateEdit.Text = gBeRecepcionEnc.Fec_mod
                txtNoContenedor.Text = gBeRecepcionEnc.No_Contenedor
                txtCartaCupo.Text = gBeRecepcionEnc.Carta_Cupo

                'Encabezados cargados ahora cargaremos detalle
                lBeTransRecDet = gBeRecepcionEnc.Detalle()
                pListRecImgs = gBeRecepcionEnc.DetalleImagenes()
                plistBeReDetParametros = gBeRecepcionEnc.DetalleParametros()
                pListRecFact = gBeRecepcionEnc.DetalleFacturas()
                pListBeStockRec = clsLnStock_rec.Get_All_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc)
                pListBeProductoPallet = clsLnProducto_pallet.Get_All_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc)

                txtIdPiloto.Text = gBeRecepcionEnc.IdPiloto
                txtIdVehiculo.Text = gBeRecepcionEnc.IdVehiculo

                txtIdVehiculo_Validated(Nothing, Nothing)
                txtIdPiloto_Validated(Nothing, Nothing)

                Dim lTran_re_tr As New clsLnTrans_re_tr

                pBeTipo_Tarea_HH = lTran_re_tr.GetSingle_By_IdTipoTransaccion(gBeRecepcionEnc.IdTipoTransaccion)

                If gBeRecepcionEnc.IdTipoTransaccion = "HCOC00" Or gBeRecepcionEnc.IdTipoTransaccion = "HSOC00" Then
                    DgridDetalleRec.ReadOnly = True
                    gvDetalleRec2.OptionsBehavior.ReadOnly = True
                Else
                    DgridDetalleRec.ReadOnly = False
                    gvDetalleRec2.OptionsBehavior.ReadOnly = False
                End If

                Cargar_Detalle_Recepcion()

                '#GT29012024: cargar el detalle de la recepción en el nuevo grid
                Cargar_Detalle_Recepcion2()

                Application.DoEvents()

                Cargar_Imagenes()

                Cargar_Facturas()

                Dim BeTransReTr As New clsBeTrans_re_tr()
                BeTransReTr.IdTipoTransaccion = txtIdTipoTR.Text

                If Not gBeRecepcionEnc Is Nothing Then
                    Configura_Opcion_Tipo_Rec(BeTransReTr)
                End If

                Application.DoEvents()

                If gBeRecepcionEnc.Estado.ToUpper = "CERRADO" Or gBeRecepcionEnc.Estado.ToUpper = "ANULADO" Then

                    If gBeRecepcionEnc.Estado.ToUpper = "CERRADO" Then
                        lblEstado.Text = "Cerrado"
                    ElseIf gBeRecepcionEnc.Estado.ToUpper = "ANULADO" Then
                        lblEstado.Text = "Anulado"
                        lblDiagonal.Visible = True
                        lblMotivoAnulacion.Visible = True
                        lblId.Visible = True

                        Dim ObjMA As New clsBeMotivo_anulacion_bodega() With {.IdMotivoAnulacionBodega = gBeRecepcionEnc.IdMotivoAnulacionBodega}

                        If clsLnMotivo_anulacion_bodega.Get_Single_With_Detail(ObjMA) Then
                            lblId.Text = ObjMA.IdMotivoAnulacion
                            lblMotivoAnulacion.Text = ObjMA.MotivoAnulacion.Nombre
                        End If

                    End If

                    'GrpImagen.Enabled = False
                    'GrpOperadorBodega.Enabled = False
                    cmdActualizar.Enabled = False
                    cmdActualizarDetalle.Enabled = False
                    cmdEliminar.Enabled = False
                    cmdFinalizar.Enabled = False
                    GrpFactura.Enabled = False

                    cmdAgregarProducto.Enabled = False
                    cmdEliminarFila.Enabled = False
                    ToolEliminarFila.Enabled = False

                    Bloquea_Objetos()

                Else

                    lblEstado.Text = gBeRecepcionEnc.Estado
                    cmdActualizar.Enabled = True
                    cmdEliminar.Enabled = True
                    cmdFinalizar.Enabled = True
                    GrpTransaccion.Enabled = True
                    GrpTIpoTransaccion.Enabled = True
                    GrpParametrosIngreso.Enabled = True
                    GrpAsignacionTransaccion.Enabled = True
                    GrpTarea.Enabled = True
                    GrpObservacion.Enabled = True
                    GrpDetalleRecepcion.Enabled = True
                    GrpImagen.Enabled = True
                    GrpOperadorBodega.Enabled = True
                    GrpFactura.Enabled = True
                    cmdEliminarFila.Enabled = True
                End If

                Application.DoEvents()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Check_Reglas_Propietario_Ingreso()

        Try

            '#EJC20210624: Validar si existen reglas por propietario.
            Dim fila As Object = cmbPropietario.GetSelectedDataRow
            Dim vReglasPropietarioDefinidas As Boolean = False

            If Not fila Is Nothing Then
                Dim IdPropietario As Integer = cmbPropietario.Tag
                Dim pListObjRE As New List(Of clsBePropietario_reglas_enc)
                pListObjRE = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(IdPropietario).ToList
                If Not pListObjRE Is Nothing Then
                    If pListObjRE.Count > 0 Then
                        vReglasPropietarioDefinidas = True
                    End If
                End If
            End If

            If vReglasPropietarioDefinidas AndAlso clsBD.Instancia.Formato_Recepcion = 1 Then
                chkHabilitaStock.Checked = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Public Sub Bloquea_Objetos()
        dtmFechaRecepcion.Enabled = False
        cmbBodega.Enabled = False
        cmbPropietario.Enabled = False
        txtIdOrdenCompra.Enabled = False
        txtOC.Enabled = False
        cmbMuelle.Enabled = False
        txtIdUbicacion.Enabled = False
        txtNombreUbicacion.Enabled = False
        txtObservacion.Enabled = False
        txtIdTipoTR.Enabled = False
        txtDescripcionTR.Enabled = False
        txtNoDocumento.Enabled = False
        chkEscanearUbicacionRec.Enabled = False
        chkTomarFoto.Enabled = False
        chkRecepcionManual.Enabled = False
        chkMuestraPrecio.Enabled = False
        chkParaPorCodigo.Enabled = False
        dtmFechaTarea.Enabled = False
        dtmHoraI.Enabled = False
        dtmHoraIhh.Enabled = False
        dtmHoraF.Enabled = False
        dtmHoraFhh.Enabled = False
        lnk.Enabled = False
        lblMarchamo.Enabled = False
        txtNoMarchamo.Enabled = False
        txtNoContenedor.Enabled = False
        txtCartaCupo.Enabled = False
    End Sub

    Private Sub Llena_Valores_Encabezado_Rec()

        Try
            ' Encabezado de Recepción
            If gBeRecepcionEnc.IsNew Then

                gBeRecepcionEnc.IdRecepcionEnc = clsLnTrans_re_enc.MaxID()
                gBeRecepcionEnc.PropietarioBodega = New clsBePropietario_bodega
                gBeRecepcionEnc.PropietarioBodega.IdBodega = cmbBodega.EditValue
                gBeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = cmbPropietario.EditValue
                gBeRecepcionEnc.User_agr = AP.UsuarioAp.IdUsuario
                gBeRecepcionEnc.Fec_agr = Now
                gBeRecepcionEnc.Activo = True
                gBeRecepcionEnc.Estado = "Nuevo"

                If txtNoMarchamo.Text = "" Then
                    gBeRecepcionEnc.No_Marchamo = "N/A"
                Else
                    gBeRecepcionEnc.No_Marchamo = txtNoMarchamo.Text
                End If

                If String.IsNullOrEmpty(txtIdOrdenCompra.Text) = False Then
                    gBeRecepcionEnc.OrdenCompraRec = New clsBeTrans_re_oc
                    gBeRecepcionEnc.OrdenCompraRec.IsNew = True
                    gBeRecepcionEnc.OrdenCompraRec.IdRecepcionOc = clsLnTrans_re_oc.MaxID(gBeRecepcionEnc.IdRecepcionEnc)
                    gBeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc
                End If

                '#GT27092023: guardar la carta de cupo
                gBeRecepcionEnc.Carta_Cupo = txtCartaCupo.Text

            End If

            If gBeRecepcionEnc.PropietarioBodega Is Nothing OrElse gBeRecepcionEnc.PropietarioBodega.IdPropietarioBodega <= 0 Then
                Throw New Exception("Seleccione Propietario")
            End If

            If String.IsNullOrEmpty(txtIdTipoTR.Text.Trim) = False Then
                gBeRecepcionEnc.IdTipoTransaccion = txtIdTipoTR.Text.Trim
            Else
                gBeRecepcionEnc.IdTipoTransaccion = ""
            End If

            gBeRecepcionEnc.IdMuelle = cmbMuelle.EditValue
            gBeRecepcionEnc.IdUbicacionRecepcion = CInt(txtIdUbicacion.Text.Trim())
            gBeRecepcionEnc.Fecha_recepcion = dtmFechaRecepcion.EditValue
            gBeRecepcionEnc.Hora_ini_pc = dtmHoraI.Value
            gBeRecepcionEnc.Hora_fin_pc = dtmHoraF.Value
            gBeRecepcionEnc.Muestra_precio = chkMuestraPrecio.Checked
            gBeRecepcionEnc.Fec_mod = Now
            gBeRecepcionEnc.User_mod = AP.UsuarioAp.IdUsuario
            gBeRecepcionEnc.Fecha_tarea = dtmFechaTarea.EditValue
            gBeRecepcionEnc.Tomar_fotos = chkTomarFoto.Checked
            gBeRecepcionEnc.Escanear_rec_ubic = chkEscanearUbicacionRec.Checked
            gBeRecepcionEnc.Para_por_codigo = chkParaPorCodigo.Checked
            gBeRecepcionEnc.Observacion = txtObservacion.Text.Trim
            gBeRecepcionEnc.IdPiloto = txtIdPiloto.EditValue
            gBeRecepcionEnc.IdVehiculo = Val(txtIdVehiculo.EditValue)
            gBeRecepcionEnc.Habilitar_Stock = chkHabilitaStock.Checked
            gBeRecepcionEnc.No_Contenedor = txtNoContenedor.Text
            '#EJC20190613: Mostrar la cantidad del pedido de compra en la HH
            gBeRecepcionEnc.Mostrar_Cantidad_Esperada = chkMostrarCantidadPI.Checked

            '#EJC20210315: Idbodega Add
            gBeRecepcionEnc.IdBodega = cmbBodega.EditValue

            '#CKFK20231227 Llenar el campo con el estado por defecto
            If txtIdEstadoDefectoRecepcion.Text <> "" Then
                gBeRecepcionEnc.IdEstado_Defecto_Recepcion = txtIdEstadoDefectoRecepcion.Text
            Else
                gBeRecepcionEnc.IdEstado_Defecto_Recepcion = 0
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

            Throw ex
        End Try

    End Sub

    Private Sub Llena_Valores_Rec_OC()

        Try

            ' Encabezado de Recepción OC
            If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing AndAlso gBeRecepcionEnc.OrdenCompraRec.IsNew Then
                gBeRecepcionEnc.OrdenCompraRec.User_agr = AP.UsuarioAp.IdUsuario
                gBeRecepcionEnc.OrdenCompraRec.Fec_agr = Now
            End If

            If String.IsNullOrEmpty(txtIdOrdenCompra.Text.Trim) = False Then
                gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc = CInt(txtIdOrdenCompra.Text)
            End If

            If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then
                gBeRecepcionEnc.OrdenCompraRec.Recepcion_ciega = False
                gBeRecepcionEnc.OrdenCompraRec.Recepcion_manual = chkRecepcionManual.Checked
                gBeRecepcionEnc.OrdenCompraRec.No_docto = txtNoDocumento.Text.Trim
                gBeRecepcionEnc.OrdenCompraRec.Hora_ini_hh = dtmHoraIhh.Value
                gBeRecepcionEnc.OrdenCompraRec.Hora_fin_hh = dtmHoraFhh.Value

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Sub

    Dim BeTareaHH As clsBeTarea_hh

    Private Sub Crea_Tarea_HH()

        Try

            BeTareaHH = New clsBeTarea_hh

            If gBeRecepcionEnc IsNot Nothing AndAlso gBeRecepcionEnc.IsNew AndAlso pTransHH Then

                BeTareaHH.IdPropietario = cmbPropietario.Tag
                BeTareaHH.IdBodega = cmbBodega.EditValue

                If cmbMuelle.EditValue > -1 Then
                    BeTareaHH.IdMuelle = CInt(cmbMuelle.EditValue)
                End If

                BeTareaHH.IdEstado = 1
                BeTareaHH.IdPrioridad = 1
                BeTareaHH.IdTipoTarea = 1
                BeTareaHH.IdTransaccion = gBeRecepcionEnc.IdRecepcionEnc
                BeTareaHH.Tipo = 0
                BeTareaHH.FechaInicio = dtmHoraI.Value
                BeTareaHH.FechaFin = dtmHoraF.Value
                BeTareaHH.DiaCompleto = False
                BeTareaHH.Descripcion = txtObservacion.Text.Trim
                BeTareaHH.CreaTarea = True
                BeTareaHH.IsNew = True

                Select Case gBeRecepcionEnc.IdTipoTransaccion.ToString()
                    Case "HSOC00"
                        BeTareaHH.Asunto = "Ingreso sin Orden de Compra "
                    Case "HSOD00"
                        BeTareaHH.Asunto = "Ingreso de Devolución sin referencia"
                    Case "HCOC00"
                        BeTareaHH.Asunto = "Ingreso con Orden de Compra"
                    Case "HCOD00"
                        BeTareaHH.Asunto = "Devolución de Pedido"
                    Case "HHSR00"
                        BeTareaHH.Asunto = "Ingreso sin referencia"
                    Case "PICH000"
                        BeTareaHH.Asunto = "Pre-ingreso con HH"
                    Case Else
                        Exit Select
                End Select

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Detalle_Recepcion()

        Try

            Dim lPesoPresentacion As Double = 0.0

            'lBeStockRec = New List(Of clsBeStock_rec)
            lBeTransRecDet = New List(Of clsBeTrans_re_det)

            Dim vIndice As Integer = 0
            Dim vlBeStockRec As New List(Of clsBeStock_rec)
            Dim vIdProductoBodega As Integer = 0
            Dim BePres As clsBeProducto_Presentacion
            Dim vPresentacionRequiereLicencia As Boolean = False

            For i As Integer = 0 To DgridDetalleRec.Rows.Count - 1

                vIndice = i

                vPresentacionRequiereLicencia = False
                BePres = Nothing

                If DgridDetalleRec.Rows(i).Cells("ProductoP").Value IsNot Nothing Then

                    pIndiceListaStock = pListBeStockRec.FindIndex(Function(f) f.IdRecepcionDet = DgridDetalleRec.Rows(vIndice).Cells("IdRecepcionDet").Value)

                    Dim BeTransReDet As New clsBeTrans_re_det() With
                        {.IdPropietarioBodega = cmbPropietario.EditValue,
                        .Producto = New clsBeProducto()}

                    vIdProductoBodega = CInt(DgridDetalleRec.Rows(i).Cells("IdProductoP").Value)

                    BeTransReDet.Producto = clsLnProducto.Get_Single_By_IdProductoBodega(vIdProductoBodega)

                    If BeTransReDet.Producto Is Nothing Then
                        Throw New Exception("ERROR_202302211753: No se pudo obtener el objeto de producto para el IdProductoBodega: (" & vIdProductoBodega & ")")
                    End If

                    BeTransReDet.Producto.Codigo = CStr(DgridDetalleRec.Rows(i).Cells("CodigoP").Value)
                    BeTransReDet.Codigo_Producto = CStr(DgridDetalleRec.Rows(i).Cells("CodigoP").Value)
                    BeTransReDet.IdProductoBodega = CInt(DgridDetalleRec.Rows(i).Cells("IdProductoP").Value)
                    BeTransReDet.Nombre_producto = CStr(DgridDetalleRec.Rows(i).Cells("ProductoP").Value)
                    '#EJC20180113: Atributo_Variante_1 en Llena_Detalle_Recepción
                    BeTransReDet.Atributo_Variante_1 = IIf(IsDBNull(DgridDetalleRec.Rows(i).Cells("Atributo_Variante_1").Value), "", CStr(DgridDetalleRec.Rows(i).Cells("Atributo_Variante_1").Value))

                    DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    BeTransReDet.Nombre_presentacion = DgComboPresentacion.FormattedValue

                    If DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value IsNot Nothing Then
                        BeTransReDet.IdRecepcionEnc = CInt(DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value)
                    Else
                        BeTransReDet.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc
                    End If

                    If DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value IsNot Nothing Then
                        BeTransReDet.IdRecepcionDet = CInt(DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value)
                    Else
                        If lBeTransRecDet.Count > 0 Then
                            BeTransReDet.IdRecepcionDet = lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1
                        Else
                            BeTransReDet.IdRecepcionDet = clsLnTrans_re_det.MaxID(BeTransReDet.IdRecepcionEnc) + 1
                        End If
                    End If

                    BeTransReDet.Presentacion = New clsBeProducto_Presentacion
                    If DgridDetalleRec.Rows(i).Cells("PresentacionP").Value IsNot Nothing Then
                        BeTransReDet.Presentacion.IdPresentacion = CInt(DgridDetalleRec.Rows(i).Cells("PresentacionP").Value)
                        BeTransReDet.IdPresentacion = CInt(DgridDetalleRec.Rows(i).Cells("PresentacionP").Value)
                    End If

                    If DgridDetalleRec.Rows(i).Cells("CostoP").Value Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("Ingrese costo")
                    Else
                        BeTransReDet.Costo = CDbl(DgridDetalleRec.Rows(i).Cells("CostoP").Value)
                    End If

                    If DgridDetalleRec.Rows(i).Cells("TotalP").Value Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("No existe total")
                    End If

                    If DgridDetalleRec.Rows(i).Cells("Peso").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(i).Cells("Peso").Value IsNot Nothing Then
                        If IsNumeric(DgridDetalleRec.Rows(i).Cells("Peso").Value) Then
                            BeTransReDet.Peso = CDbl(DgridDetalleRec.Rows(i).Cells("Peso").Value)
                        End If
                    End If

                    If DgridDetalleRec.Rows(i).Cells("PesoPresentacion").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(i).Cells("PesoPresentacion").Value IsNot Nothing Then
                        If IsNumeric(DgridDetalleRec.Rows(i).Cells("PesoPresentacion").Value) Then
                            lPesoPresentacion = CDbl(DgridDetalleRec.Rows(i).Cells("PesoPresentacion").Value)
                        End If
                    End If

                    '' VALIDACIÓN DE CANTIDADES CON REGLA SEGÚN PROPIETARIO
                    If DgridDetalleRec.Rows(i).Cells("CantidadP").Value Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("Ingrese la cantidad a Recibir")
                    ElseIf DgridDetalleRec.Rows(i).Cells("CantidadP").Value = 0 Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("La cantidad a Recibir debe ser mayor a 0")
                    Else
                        BeTransReDet.cantidad_recibida = (DgridDetalleRec.Rows(i).Cells("CantidadP").Value)
                        BeTransReDet.cantidad_recibida = Math.Round(BeTransReDet.cantidad_recibida, 6)
                    End If

                    If DgridDetalleRec.Rows(i).Cells("No_Linea").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(i).Cells("No_Linea").Value IsNot Nothing Then
                        BeTransReDet.No_Linea = CInt(DgridDetalleRec.Rows(i).Cells("No_Linea").Value)
                    End If

                    '#EJC202210080831
                    If DgridDetalleRec.Rows(i).Cells("Lic_Plate").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(i).Cells("Lic_Plate").Value IsNot Nothing Then
                        BeTransReDet.Lic_plate = CStr(IIf(IsDBNull(DgridDetalleRec.Rows(i).Cells("Lic_Plate").Value), "", DgridDetalleRec.Rows(i).Cells("Lic_Plate").Value))
                    End If

                    If DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception(String.Format("No existe Unidad de Medida en Producto {0}", DgridDetalleRec.Rows(i).Cells("ProductoP").Value))
                    Else
                        BeTransReDet.UnidadMedida = New clsBeUnidad_medida
                        BeTransReDet.UnidadMedida.IdUnidadMedida = CInt(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value)
                        BeTransReDet.IdUnidadMedida = CInt(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value)
                    End If

                    '#EJC20190214: tu día del cariño, bien pijiado es la 1.40 am.
                    '#CM20190214: Se llenaba el objeto BeTransReDet y debía ser el pListBeStockRec(pIndiceListaStock)
                    '#EJC202405090331AM: Creo que la lista no se inicializa aqui, pero aqui da error por ser nothing.
                    If pListBeStockRec Is Nothing Then
                        pListBeStockRec = New List(Of clsBeStock_rec)
                    End If

                    pListBeStockRec(pIndiceListaStock).ProductoEstado = New clsBeProducto_estado
                    BeTransReDet.ProductoEstado = New clsBeProducto_estado
                    If DgridDetalleRec.Rows(i).Cells("Estado").Value Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception(String.Format("No existe Estado en Producto {0}", DgridDetalleRec.Rows(i).Cells("ProductoP").Value))
                    Else
                        BeTransReDet.ProductoEstado.IdEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                        BeTransReDet.IdProductoEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                        pListBeStockRec(pIndiceListaStock).ProductoEstado.IdEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                        pListBeStockRec(pIndiceListaStock).IdProductoEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                    End If

                    '#CKFK20241017 Agregué esta validación para que no de error al asignar el estado
                    DgComboEstado = TryCast(DgridDetalleRec.Rows(i).Cells("Estado"), DataGridViewComboBoxCell)
                    'BeTransReDet.Nombre_producto_estado = DgComboEstado.FormattedValue

                    ' Verifica si el valor está en la lista de ítems del combo box
                    If DgComboEstado.Items.Count > 0 Then
                        BeTransReDet.Nombre_producto_estado = DgComboEstado.FormattedValue
                    End If

                    DgComboUnidadMedida = TryCast(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP"), DataGridViewComboBoxCell)
                    'BeTransReDet.Nombre_unidad_medida = DgComboUnidadMedida.FormattedValue

                    ' Verifica si el valor está en la lista de ítems del combo box
                    If DgComboUnidadMedida.Items.Count > 0 Then
                        BeTransReDet.Nombre_unidad_medida = DgComboUnidadMedida.FormattedValue
                    End If

                    If DgridDetalleRec.Rows(i).Cells("lote").Value IsNot Nothing Then
                        BeTransReDet.Lote = CStr(DgridDetalleRec.Rows(i).Cells("lote").Value)
                    End If

                    Dim ControlVencimiento As Boolean = DgridDetalleRec.Rows(i).Cells("ControlVencimiento").Value

                    If ControlVencimiento Then

                        If DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value IsNot Nothing Then
                            If IsDate(DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value) Then
                                BeTransReDet.Fecha_vence = CDate(DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value)
                                If pIndiceListaStock <> -1 Then
                                    pListBeStockRec(pIndiceListaStock).Fecha_vence = BeTransReDet.Fecha_vence
                                    If Not ValidaFechaVencimiento(BeTransReDet.Fecha_vence, DgridDetalleRec.Rows(i).Cells("ProductoP").Value.ToString) Then
                                        Throw New Exception(String.Format("Se debe corregir la fecha de vencimiento del producto: {0}", DgridDetalleRec.Rows(i).Cells("ProductoP").Value))
                                    End If
                                End If
                            Else
                                SplashScreenManager.CloseForm(False)
                                Throw New Exception(String.Format("La fecha vencimiento no es válida para el producto {0}.", DgridDetalleRec.Rows(i).Cells("ProductoP").Value.ToString))
                            End If
                        Else
                            SplashScreenManager.CloseForm(False)
                            Throw New Exception(String.Format("Ingrese fecha de vencimiento para el producto {0}.", DgridDetalleRec.Rows(i).Cells("ProductoP").Value.ToString))
                        End If

                    Else

                        'BeTransReDet.Fecha_vence = Nothing
                        BeTransReDet.Fecha_vence = New Date(1900, 1, 1)

                    End If

                    BeTransReDet.Fecha_ingreso = Now

                    'GT12092022_1720: se asigna la LP y el operador bodega porque estos valores no existen en el grid que se esta iterando
                    Dim pReDet As clsBeTrans_re_det = gBeRecepcionEnc.Detalle.Find(Function(x) x.IdRecepcionEnc = BeTransReDet.IdRecepcionEnc AndAlso
                                                                                               x.IdRecepcionDet = BeTransReDet.IdRecepcionDet AndAlso
                                                                                               x.No_Linea = BeTransReDet.No_Linea)

                    If BePres Is Nothing AndAlso BeTransReDet.IdPresentacion <> 0 Then
                        BePres = New clsBeProducto_Presentacion()
                        BePres = clsLnProducto_presentacion.Get_BeProductoPresentacion_By_IdPresentacion(BeTransReDet.IdPresentacion)
                        If Not BePres Is Nothing Then
                            vPresentacionRequiereLicencia = BePres.Genera_lp_auto
                        End If
                    End If

                    If BeTransReDet.Producto.Genera_lp OrElse vPresentacionRequiereLicencia Then

                        If BeTransReDet.Lic_plate = "" Then
                            If pReDet Is Nothing Then
                                Throw New Exception("ERROR_20220912_1550A: Producto sin LP no se puede actualizar.")
                            Else
                                BeTransReDet.Lic_plate = pReDet.Lic_plate
                            End If
                        End If

                    End If

                    '#EJC20221008: Si es una recepción desde BOF (Ciega) no tiene operador.
                    If Not (gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MSOC00") AndAlso Not (gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MCOC00") Then
                        If BeTransReDet.IdOperadorBodega = 0 Then
                            If pReDet Is Nothing Then
                                Throw New Exception("ERROR_20220912_1550: Producto sin Operador-Bodega no se puede actualizar.")
                            Else
                                BeTransReDet.IdOperadorBodega = pReDet.IdOperadorBodega
                            End If
                        End If
                    End If

                    If DgridDetalleRec.Rows(i).Cells("Peso").Value IsNot Nothing Then
                        BeTransReDet.Peso = CDbl(DgridDetalleRec.Rows(i).Cells("Peso").Value)
                    End If

                    If DgridDetalleRec.Rows(i).Cells("Observacion").Value IsNot Nothing Then
                        BeTransReDet.Observacion = CStr(DgridDetalleRec.Rows(i).Cells("Observacion").Value)
                    End If

                    BeTransReDet.User_agr = AP.UsuarioAp.IdUsuario
                    BeTransReDet.Fec_agr = Now
                    BeTransReDet.IsNew = DgridDetalleRec.Rows(i).Cells("IsNewR").Value
                    BeTransReDet.IdUbicacion = CInt(txtIdUbicacion.Text)

                    If Not BeTransOcEnc Is Nothing Then
                        '#EJC20220407:Encontrar el IdOrdenCompraEnc y Det.
                        BeTransReDet.IdOrdenCompraEnc = BeTransOcEnc.IdOrdenCompraEnc

                        If DgridDetalleRec.Rows(i).Cells("IdOrdenCompraDet").Value IsNot Nothing Then
                            BeTransReDet.IdOrdenCompraDet = DgridDetalleRec.Rows(i).Cells("IdOrdenCompraDet").Value
                        End If

                    End If

                    Dim listaProdPalletsNuevos As New List(Of clsBeProducto_pallet)

                    ' CAMPOS FALTANTES STOCK DE ASIGNACIÓN 
                    If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then

                        Dim listaProdPallets As New List(Of clsBeProducto_pallet)
                        Dim listaStockPalletsNuevos As New List(Of clsBeStock_rec)

                        If BeTransReDet.Presentacion.IdPresentacion <> 0 Then

                            vlBeStockRec = pListBeStockRec.FindAll(Function(p) _
                                                               p.IdProductoBodega = BeTransReDet.IdProductoBodega _
                                                               AndAlso p.ProductoValidado = False _
                                                               AndAlso p.Presentacion.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion _
                                                               AndAlso p.IdRecepcionDet = BeTransReDet.IdRecepcionDet _
                                                               AndAlso p.IdProductoEstado = BeTransReDet.IdProductoEstado _
                                                               AndAlso p.No_linea = BeTransReDet.No_Linea)

                            If pListBeProductoPallet IsNot Nothing AndAlso pListBeProductoPallet.Count > 0 Then

                                listaProdPallets = pListBeProductoPallet.FindAll(Function(p) _
                                                                             p.IdRecepcionDet = BeTransReDet.IdRecepcionDet _
                                                                             AndAlso p.IdProductoBodega = BeTransReDet.IdProductoBodega _
                                                                             AndAlso p.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion)

                                Dim BePresPP As New clsBeProducto_Presentacion With {.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion}
                                clsLnProducto_presentacion.GetSingle(BePresPP)

                                For Each BePP As clsBeProducto_pallet In listaProdPallets

                                    With BePP
                                        .Lote = CStr(DgridDetalleRec.Rows(i).Cells("lote").Value)
                                        .User_agr = AP.UsuarioAp.IdUsuario
                                        .User_mod = AP.UsuarioAp.IdUsuario
                                        .Cantidad = Math.Round((1 * BePresPP.Factor * BePresPP.CajasPorCama * BePresPP.CamasPorTarima), 6)
                                        If ControlVencimiento Then
                                            If DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value IsNot Nothing Then
                                                .Fecha_vence = CDate(DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value)
                                            End If
                                        Else
                                            .Fecha_vence = Nothing
                                        End If
                                    End With
                                Next

                            End If

                        Else

                            '#CKFK 20180420 Moví esto para acá por la forma en busca en la lista
                            If DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value IsNot Nothing Then
                                pListBeStockRec(pIndiceListaStock).IdUnidadMedida = CInt(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value)
                            End If

                            '#EJC20190214: tu día del cariño, bien pijiado es la 1.40 am..
                            BeTransReDet.ProductoEstado = New clsBeProducto_estado
                            If DgridDetalleRec.Rows(i).Cells("Estado").Value IsNot Nothing Then
                                BeTransReDet.ProductoEstado.IdEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                                BeTransReDet.IdProductoEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                            End If

                            '#EJC20171025_1241PM: Se agregó p.Presentacion.IdPresentacion =0 para que no mezcle el mismo código con y sin presentación..
                            '#EJC20190214: Se agrego filtro por IdProductoEstado, NoLinea e IdRecepcionDet..
                            vlBeStockRec = pListBeStockRec.FindAll(Function(p) _
                                                       p.IdProductoBodega = BeTransReDet.IdProductoBodega _
                                                       AndAlso p.ProductoValidado = False _
                                                       AndAlso p.IdUnidadMedida = BeTransReDet.IdUnidadMedida _
                                                       AndAlso p.IdPresentacion = 0 _
                                                       AndAlso p.IdRecepcionDet = BeTransReDet.IdRecepcionDet _
                                                       AndAlso p.IdProductoEstado = BeTransReDet.IdProductoEstado _
                                                       AndAlso p.No_linea = BeTransReDet.No_Linea)

                        End If

                        If vlBeStockRec.Count > 0 Then

                            For Each BeStockRec As clsBeStock_rec In vlBeStockRec

                                BeStockRec.ProductoEstado = New clsBeProducto_estado
                                If DgridDetalleRec.Rows(i).Cells("Estado").Value IsNot Nothing Then
                                    BeStockRec.ProductoEstado.IdEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                                    BeStockRec.IdProductoEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                                End If

                                BeStockRec.Presentacion = New clsBeProducto_Presentacion
                                If DgridDetalleRec.Rows(i).Cells("PresentacionP").Value IsNot Nothing Then
                                    BeStockRec.Presentacion.IdPresentacion = CInt(DgridDetalleRec.Rows(i).Cells("PresentacionP").Value)
                                    BeStockRec.IdPresentacion = CInt(DgridDetalleRec.Rows(i).Cells("PresentacionP").Value)
                                End If

                                '#EJC20180113:Atributo_Variante_1 en detalle de stock -> Llena_Detalle_Recepcion
                                If DgridDetalleRec.Rows(i).Cells("Atributo_Variante_1").Value IsNot Nothing Then
                                    BeStockRec.Atributo_Variante_1 = IIf(IsDBNull(DgridDetalleRec.Rows(i).Cells("Atributo_Variante_1").Value), "", CStr(DgridDetalleRec.Rows(i).Cells("Atributo_Variante_1").Value))
                                Else
                                    BeStockRec.Atributo_Variante_1 = ""
                                End If

                                If DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value IsNot Nothing Then
                                    BeStockRec.IdUnidadMedida = CInt(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value)
                                End If

                                BeStockRec.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc

                                If DgridDetalleRec.Rows(i).Cells("lote").Value IsNot Nothing Then
                                    BeStockRec.Lote = CStr(DgridDetalleRec.Rows(i).Cells("lote").Value)
                                End If

                                If ControlVencimiento Then
                                    If DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value IsNot Nothing Then
                                        BeStockRec.Fecha_vence = CDate(DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value)
                                    End If
                                Else
                                    BeStockRec.Fecha_vence = Nothing
                                End If

                                BeStockRec.IdUbicacion = CInt(txtIdUbicacion.Text.Trim)
                                BeStockRec.IdUbicacion_anterior = BeStockRec.IdUbicacion
                                BeStockRec.ProductoValidado = True
                                BeStockRec.Fecha_Ingreso = Now
                                BeStockRec.Fec_agr = Now
                                BeStockRec.Lic_plate = BeTransReDet.Lic_plate
                                If BeStockRec.Presentacion.IdPresentacion <> 0 Then

                                    BePres.IdPresentacion = BeStockRec.Presentacion.IdPresentacion
                                    clsLnProducto_presentacion.GetSingle(BePres)

                                    If BePres.EsPallet Then

                                        If CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value) = 1 Then

                                            BeStockRec.Cantidad = Math.Round((CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value) * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima), 6)

                                        Else

                                            If BePres.Genera_lp_auto Then

                                                Dim vCantidadPallets As Integer = DgridDetalleRec.Rows(i).Cells("CantidadP").Value
                                                Dim vIndiceLista As Integer = 0
                                                Dim vBeStockRec As New clsBeStock_rec
                                                Dim BeProdPallet As New clsBeProducto_pallet

                                                For x As Integer = 1 To vCantidadPallets - 1

                                                    vIndiceLista = listaStockPalletsNuevos.Count - 1

                                                    If x = 1 Then
                                                        clsPublic.CopyObject(BeStockRec, vBeStockRec)
                                                        vBeStockRec.Cantidad = (1 * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima)
                                                    Else
                                                        vBeStockRec = New clsBeStock_rec
                                                        clsPublic.CopyObject(listaStockPalletsNuevos(vIndiceLista), vBeStockRec)
                                                    End If

                                                    vBeStockRec.Cantidad = (1 * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima)

                                                    vBeStockRec.Lic_plate = clsLnStock_rec.Get_Nuevo_Correlativo_LicensePlate(AP.IdEmpresa,
                                                                                                                         cmbBodega.EditValue,
                                                                                                                         cmbPropietario.EditValue,
                                                                                                                         BeTransReDet.IdProductoBodega,
                                                                                                                         vBeStockRec.Lic_plate)

                                                    listaStockPalletsNuevos.Add(vBeStockRec)

                                                    If BePres.Imprime_barra Then

                                                        BeProdPallet = New clsBeProducto_pallet

                                                        With BeProdPallet
                                                            .IdPropietarioBodega = BeTransReDet.IdPropietarioBodega
                                                            .IdProductoBodega = BeTransReDet.IdProductoBodega
                                                            .IdOperadorBodega = Nothing
                                                            .IdPresentacion = BePres.IdPresentacion
                                                            .IdRecepcionDet = vBeStockRec.IdRecepcionDet
                                                            .Impreso = 0
                                                            .IdImpresora = 1
                                                            .Activo = True
                                                            .Fecha_ingreso = Now
                                                            .Codigo_Barra = vBeStockRec.Lic_plate
                                                            .Reimpresiones = 0
                                                            .Cantidad = vBeStockRec.Cantidad
                                                            .Fecha_vence = IIf(ControlVencimiento, vBeStockRec.Fecha_vence, Nothing)
                                                            .Fec_agr = Now
                                                            .Fec_mod = Now
                                                            .Lote = vBeStockRec.Lote.ToUpper()
                                                            .User_agr = AP.UsuarioAp.IdUsuario
                                                            .User_mod = AP.UsuarioAp.IdUsuario
                                                            .IsNew = True
                                                        End With

                                                        listaProdPalletsNuevos.Add(BeProdPallet)
                                                        lBeProdPallet.Add(BeProdPallet)

                                                    End If

                                                Next

                                            Else

                                                Throw New Exception("La cantidad de pallets es > 1 y genera_lp_auto es Falso, debe recibir los pallets de forma unitaria (Cantidad = 1)")
                                                'La cantidad es > 1 y genera_lp_auto es false

                                            End If

                                        End If

                                    Else

                                        BeStockRec.Cantidad = Math.Round((CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value) * BePres.Factor), 6)

                                        If BePres.Imprime_barra Then

                                            Dim BeProdPallet As New clsBeProducto_pallet With {
                                                      .IdPropietarioBodega = BeTransReDet.IdPropietarioBodega,
                                                      .IdProductoBodega = BeTransReDet.IdProductoBodega,
                                                      .IdPresentacion = BePres.IdPresentacion,
                                                      .IdRecepcionDet = BeTransReDet.IdRecepcionDet,
                                                      .IdOperadorBodega = Nothing,
                                                      .Impreso = 0,
                                                      .IdImpresora = 1,
                                                      .Activo = True,
                                                      .Fecha_ingreso = Now,
                                                      .Codigo_Barra = BePres.Codigo_barra,
                                                      .Reimpresiones = 0,
                                                      .Cantidad = BeStockRec.Cantidad,
                                                      .Fecha_vence = BeStockRec.Fecha_vence,
                                                      .Fec_agr = Now,
                                                      .Fec_mod = Now,
                                                      .Lote = BeStockRec.Lote.ToUpper(),
                                                      .User_agr = AP.UsuarioAp.IdUsuario,
                                                      .User_mod = AP.UsuarioAp.IdUsuario,
                                                      .IsNew = True}

                                            listaStockPalletsNuevos.Add(BeStockRec)
                                            listaProdPallets.Add(BeProdPallet)
                                            lBeProdPallet.Add(BeProdPallet)

                                        End If

                                    End If

                                Else
                                    'Agregado por Erik Calderón, si no hay presentación 
                                    'no se multiplica por factor, se recibe en unidad 
                                    'de medida básica.
                                    '#EJC20170710
                                    BeStockRec.Cantidad = Math.Round((CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value)), 6)

                                    Dim vBestockRec As New clsBeStock_rec()
                                    vBestockRec = BeStockRec.Clone()
                                    listaStockPalletsNuevos.Add(vBestockRec)

                                End If

                            Next

                        Else
                            Throw New Exception("Inconsistencia en la línea: " & BeTransReDet.No_Linea & " no se pudieron asignar los valores para stock, elimine la línea e intente de nuevo.")
                        End If

                        For Each PP As clsBeProducto_pallet In listaProdPalletsNuevos
                            listaProdPallets.Add(PP)
                        Next

                        For Each PP As clsBeProducto_pallet In listaProdPallets
                            lBeProdPallet.Add(PP)
                        Next

                    End If

                    BeTransReDet.MotivoDevolucion = New clsBeMotivo_devolucion
                    lBeTransRecDet.Add(BeTransReDet)

                End If

            Next

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            If clsBD.Instancia.Formato_Recepcion = 1 Then
                DgridDetalleRec.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If

            Llena_Valores_Encabezado_Rec()

            Llena_Valores_Rec_OC()

            Crea_Tarea_HH()

            If gBeRecepcionEnc.IdTipoTransaccion.ToString() = "HSOC00" Or
                gBeRecepcionEnc.IdTipoTransaccion.ToString() = "HSOD00" Or
                gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MCOC00" Or
                gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MCOD00" Or
                gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MSOC00" Or
                gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MSOD00" Then

                ' Ingreso sin Orden de Compra 
                ' Ingreso de Devolución sin referencia
                ' Ingreso con Orden de Compra 
                ' Devolución de Pedido (PC)
                ' Ingreso sin Orden de Compra  (PC)
                ' Devolución sin referencia (PC)

                If clsBD.Instancia.Formato_Recepcion = 1 Then

                    '#GT25012024: este es el proceso original, lo dejo asi mientras valido el nuevo.
                    If DgridDetalleRec.Rows.Count > 0 Then

                        pListBeStockRec.ForEach(AddressOf Restablecer)

                        Llena_Detalle_Recepcion()

                        pListBeStockRec.ForEach(AddressOf Restablecer)

                        If Not Guardar_Recepcion_Inmediata_BOF Then
                            If lBeTransRecDet.Count = 0 Then
                                Throw New Exception("La recepción no tiene detalle")
                            End If
                        End If

                    End If

                Else

                    '#GT24012023: llenamos el objeto con los datos del nuevo grid.
                    If gvDetalleRec2.RowCount > 0 Then

                        pListBeStockRec.ForEach(AddressOf Restablecer)
                        Llena_Detalle_Recepcion2()
                        pListBeStockRec.ForEach(AddressOf Restablecer)

                        '#GT08022024: si es recepcion por bof no validar detalle en el grid
                        If Not Guardar_Recepcion_Inmediata_BOF Then
                            If lBeTransRecDet.Count = 0 Then
                                Throw New Exception("GT:08022024: La recepción no tiene detalle")
                            End If
                        End If
                    End If

                End If

            ElseIf gBeRecepcionEnc.IdTipoTransaccion = "HCOC00" Or gBeRecepcionEnc.IdTipoTransaccion = "HCOD00" Then

                If DgridDetalleRec.Rows.Count > 0 Then

                    pListBeStockRec.ForEach(AddressOf Restablecer)

                    Llena_Detalle_Recepcion()

                    pListBeStockRec.ForEach(AddressOf Restablecer)

                    If Not (gBeRecepcionEnc.Estado = "Nuevo" AndAlso gBeRecepcionEnc.IdTipoTransaccion = "HCOC00") Then
                        If lBeTransRecDet.Count = 0 AndAlso Not (gBeRecepcionEnc.IdTipoTransaccion = "HCOC00") Then
                            Throw New Exception("ERROR_02032023: La recepción no tiene detalle")
                        End If
                    End If

                End If

            End If

            clsLnTrans_re_enc.Guardar(BeTareaHH,
                                      gBeRecepcionEnc,
                                      gBeRecepcionEnc.OrdenCompraRec,
                                      lBeTransRecDet,
                                      plistBeReDetParametros,
                                      pListOpe,
                                      pListRecFact,
                                      pListRecImgs,
                                      pListBeStockSeRec,
                                      pListBeStockRec,
                                      lBeProdPallet,
                                      cmbBodega.EditValue,
                                      BeTransOcEnc.No_Ticket_TMS)

            Guardar = True

            If Not TransaccionesQueNecesitanHH.Exists(Function(x) x = gBeRecepcionEnc.IdTipoTransaccion) Then

                '#CKFK20240214 Agregué la condición de que la recepción no sea del tipo gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MCOC00"
                If gBeRecepcionEnc.IdTipoTransaccion.ToString() <> "MCOC00" Then

                    'Ingreso sin referencia HH 'Solamente crea tarea para Handheld
                    If Finalizar(lBeTransRecDet) Then

                        If AP.IdConfiguracionInterface <> -1 Then

                            If Not BeTipoDocumento Is Nothing Then

                                If Not BeTipoDocumento.Marcar_Registros_Enviados_MI3 Then

                                    Ejecutar_Interface("5-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me)

                                End If

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Error al guardar la recepción: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub GrdImagen_Click(sender As Object, e As EventArgs) Handles GrdImagen.Click

        Try

            If GridViewImg.RowCount > 0 Then

                Dim Dr As DataRowView = GridViewImg.GetFocusedRow
                Dim Obj As New clsBeTrans_re_img
                Obj = pListRecImgs.Find(Function(b) b.IdImagen = CInt(Dr.Item("Código")))
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

    Private Sub lnkUbicacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacion.LinkClicked

        Try

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Dim Ubicacion As New frmBodegaUbicacion_List() With
                    {.pUbicacionRecepcion = True,
                    .pIdBodega = cmbBodega.EditValue,
                    .Modo = frmBodegaUbicacion_List.pModo.Seleccion}

                If OpcionesMenu IsNot Nothing Then
                    Ubicacion.OpcionesMenu = OpcionesMenu
                    Ubicacion.mmuActualizar.Enabled = OpcionesMenu.Leer
                End If

                Ubicacion.ShowDialog()

                If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                    txtIdUbicacion.Text = Ubicacion.pObj.IdUbicacion
                    txtNombreUbicacion.Text = Ubicacion.pObj.Descripcion
                End If

                Ubicacion.Close()
                Ubicacion.Dispose()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub lnkEstadoPorDefecto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkEstadoPorDefecto.LinkClicked

        Try

            If cmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Dim Estado As New frmProducto_EstadoList With
                    {.pIdPropietario = cmbPropietario.Tag,
                    .Modo = frmProducto_EstadoList.pModo.Seleccion}

                If OpcionesMenu IsNot Nothing Then
                    Estado.OpcionesMenu = OpcionesMenu
                    Estado.mnuNuevo.Enabled = OpcionesMenu.Modificar
                    Estado.mnuActualizar.Enabled = OpcionesMenu.Leer
                End If

                Estado.ShowDialog()

                If Estado.pObj IsNot Nothing AndAlso Estado.pObj.IdEstado <> 0 Then
                    txtIdEstadoDefectoRecepcion.Text = Estado.pObj.IdEstado
                    txtNombreEstado.Text = Estado.pObj.Nombre
                End If

                Estado.Close()
                Estado.Dispose()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub txtIdUbicacion_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdUbicacion.PreviewKeyDown

        Try

            If e.KeyData = Keys.Tab Then

                If cmbBodega.ItemIndex = -1 Then

                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Else

                    If String.IsNullOrEmpty(txtIdUbicacion.Text.Trim()) = False Then

                        If txtIdUbicacion.Text > 0 Then

                            Dim Obj As New clsBeBodega_ubicacion
                            Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacion.Text.Trim, cmbBodega.EditValue)

                            If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                                txtNombreUbicacion.Text = Obj.Descripcion

                            Else

                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show(String.Format("No existe Ubicación con código {0}", txtIdUbicacion.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                txtIdUbicacion.SelectAll() : txtIdUbicacion.Focus()

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub txtId_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdOrdenCompra.PreviewKeyDown

        Try

            If cmbPropietario.EditValue <> 0 Then

                If e.KeyData = Keys.Tab Then

                    If txtIdOrdenCompra.Text > "0" Then

                        If String.IsNullOrEmpty(txtIdOrdenCompra.Text.Trim()) = False Then

                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                            BeTransOcEnc = clsLnTrans_oc_enc.Get_Orden_Compra_By_Propietario(txtIdOrdenCompra.Text.Trim(), cmbPropietario.EditValue)

                            If BeTransOcEnc IsNot Nothing AndAlso BeTransOcEnc.IdOrdenCompraEnc > 0 Then

                                txtOC.Text = String.Format("{0} {1}", BeTransOcEnc.Referencia, BeTransOcEnc.No_Documento)

                                If BeTransOcEnc.IdEstadoOC = 4 Then

                                    'Si el IdEstadoOc=4 osea si esta finalizada o cerrada entonces no dejar crear recepción con esta OC

                                    SplashScreenManager.CloseForm(False)
                                    XtraMessageBox.Show(String.Format(
                                    "El pedido de compra {0} se encuentra finalizado. 
No puede generar recepción con éste  documento.", gBeOrdenCompra.IdOrdenCompraEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    txtIdOrdenCompra.Text = String.Empty
                                    txtOC.Text = String.Empty
                                    txtIdOrdenCompra.Focus()
                                    txtIdOrdenCompra.SelectAll()
                                    Return

                                ElseIf BeTransOcEnc.ExisteRecepcionNoFinalizada() Then

                                    SplashScreenManager.CloseForm(False)
                                    XtraMessageBox.Show("Existe una Recepción que aún no se ha finalizado. Favor de finalizarla antes de crear otra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return
                                    'ElseIf gBeOrdenCompra.ExisteRecepcionNoFinalizada = False Then

                                Else

                                    ValidaOC(BeTransOcEnc.IdOrdenCompraEnc)

                                End If

                                If BeTransOcEnc.EsDevolucion Then
                                    SplashScreenManager.Default.SetWaitFormCaption("Devolución")
                                Else
                                    SplashScreenManager.Default.SetWaitFormCaption("Orden de Compra")
                                End If

                                pListObjOrdeCompraDet = BeTransOcEnc.DetalleOC.ToList
                                Cargar_Detalle_OC()

                                SplashScreenManager.CloseForm(False)

                                If BeTransOcEnc IsNot Nothing AndAlso BeTransOcEnc.IsNew = False AndAlso BeTransOcEnc.IdOrdenCompraEnc > 0 Then
                                    ListarTipoTransaccion(True, IIf(BeTransOcEnc.EsDevolucion, "DEVOLUCION", "INGRESO"))
                                End If

                                XtraMessageBox.Show("Documento de ingreso seleccionado correctamente.",
                                                    Text,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information)

                            Else
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show(String.Format("No existe Orden de Compra con código {0}", txtIdOrdenCompra.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                txtIdOrdenCompra.SelectAll() : txtIdOrdenCompra.Focus()
                            End If

                        End If

                    End If

                End If

            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Detalle_Recepcion()

        Try

            DgridDetalleRec.SuspendLayout() : DgridDetalleRec.Rows.Clear()

            Dim i As Integer = -1

            For Each BeTransReDet As clsBeTrans_re_det In lBeTransRecDet

                i = DgridDetalleRec.Rows.Add(0, BeTransReDet.Producto.Codigo, BeTransReDet.Producto.Nombre)

                If BeTransReDet.UnidadMedida IsNot Nothing Then
                    setUnidadMedidaGrid2(BeTransReDet, i)
                Else
                    Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", BeTransReDet.Producto.Nombre))
                End If

                DgridDetalleRec.Rows(i).Cells("CantidadP").Value = BeTransReDet.cantidad_recibida
                DgridDetalleRec.Rows(i).Cells("CostoP").Value = BeTransReDet.Costo
                DgridDetalleRec.Rows(i).Cells("IdProductoP").Value = BeTransReDet.IdProductoBodega
                DgridDetalleRec.Rows(i).Cells("KeyP").Value = BeTransReDet.Producto.IdProducto

                Dim bo As clsBeProducto = clsLnProducto.Get_Control_Vencimiento_By_IdProducto(BeTransReDet.Producto.IdProducto)
                DgridDetalleRec.Rows(i).Cells("ControlVencimiento").Value = bo.Control_vencimiento
                DgridDetalleRec.Rows(i).Cells("ControlPeso").Value = bo.Control_peso

                Dim lBeProductoPresentacion As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(BeTransReDet.Producto.IdProducto).ToList
                Dim lBeProductoEstado As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodegaHH(gBeRecepcionEnc.PropietarioBodega.IdPropietario, gBeRecepcionEnc.IdBodega).ToList

                If lBeProductoPresentacion.Count > 0 Then
                    If BeTransReDet.IdPresentacion <> 0 Then
                        If Not BeTransReDet.Presentacion Is Nothing Then
                            Llena_Presentacion_Grid2(lBeProductoPresentacion, i, BeTransReDet.Presentacion.IdPresentacion)
                        End If
                    End If
                Else
                    DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    DgComboPresentacion.DataSource = Nothing
                End If

                If BeTransReDet.IdPresentacion <> 0 Then
                    If BeTransReDet.Presentacion.IdPresentacion <> 0 Then
                        If BeTransReDet.Presentacion.EsPallet Then
                            DgridDetalleRec.Rows(i).Cells("CantidadP").Value = BeTransReDet.cantidad_recibida '/ (Obj.Presentacion.Factor * Obj.Presentacion.CajasPorCama * Obj.Presentacion.CamasPorTarima)
                        Else
                            DgridDetalleRec.Rows(i).Cells("CantidadP").Value = BeTransReDet.cantidad_recibida '/ Obj.Presentacion.Factor
                        End If
                    End If
                End If

                If lBeProductoEstado.Count > 0 Then
                    Llena_Estados(lBeProductoEstado, i, BeTransReDet.ProductoEstado.IdEstado)
                Else
                    DgComboEstado = TryCast(DgridDetalleRec.Rows(i).Cells("Estado"), DataGridViewComboBoxCell)
                    DgComboEstado.DataSource = Nothing
                End If

                If BeTransReDet.Fecha_vence <> Nothing Then
                    If bo.Control_vencimiento Then
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value = BeTransReDet.Fecha_vence
                    Else
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value = "N/A"
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").ReadOnly = True
                    End If
                End If

                Dim valor_peso = 0

                If BeTransReDet.cantidad_recibida > 0 Then
                    valor_peso = BeTransReDet.Peso / BeTransReDet.cantidad_recibida
                End If

                DgridDetalleRec.Rows(i).Cells("Lote").Value = BeTransReDet.Lote
                DgridDetalleRec.Rows(i).Cells("Peso").Value = BeTransReDet.Peso
                DgridDetalleRec.Rows(i).Cells("PesoUnitario").Value = IIf((valor_peso > 0), valor_peso, 0)
                DgridDetalleRec.Rows(i).Cells("CostoOC").Value = GetCostoByIdProducto(BeTransReDet.IdProductoBodega)
                DgridDetalleRec.Rows(i).Cells("Observacion").Value = BeTransReDet.Observacion
                DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value = BeTransReDet.IdRecepcionEnc
                DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value = BeTransReDet.IdRecepcionDet
                DgridDetalleRec.Rows(i).Cells("IsNewR").Value = BeTransReDet.IsNew
                DgridDetalleRec.Rows(i).Cells("No_Linea").Value = BeTransReDet.No_Linea
                DgridDetalleRec.Rows(i).Cells("atributo_variante_1").Value = BeTransReDet.Atributo_Variante_1
                DgridDetalleRec.Rows(i).Cells("lic_plate").Value = BeTransReDet.Lic_plate
                DgridDetalleRec.Rows(i).Cells("IdOrdenCompraEnc").Value = BeTransReDet.IdOrdenCompraEnc
                DgridDetalleRec.Rows(i).Cells("IdOrdenCompraDet").Value = BeTransReDet.IdOrdenCompraDet

                Set_TotalGrid2(i)

            Next

            DgridDetalleRec.ResumeLayout()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Cargar_Detalle_Recepcion(ByVal lBeTransRecDet As List(Of clsBeTrans_re_det))

        Try

            DgridDetalleRec.SuspendLayout() : DgridDetalleRec.Rows.Clear()

            Dim i As Integer = -1

            For Each BeTransReDet As clsBeTrans_re_det In lBeTransRecDet.OrderBy(Function(x) x.IdRecepcionDet)

                i = DgridDetalleRec.Rows.Add(0, BeTransReDet.Producto.Codigo, BeTransReDet.Producto.Nombre)

                If BeTransReDet.UnidadMedida IsNot Nothing Then
                    setUnidadMedidaGrid2(BeTransReDet, i)
                Else
                    Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", BeTransReDet.Producto.Nombre))
                End If

                DgridDetalleRec.Rows(i).Cells("CantidadP").Value = BeTransReDet.cantidad_recibida
                DgridDetalleRec.Rows(i).Cells("CostoP").Value = BeTransReDet.Costo
                DgridDetalleRec.Rows(i).Cells("IdProductoP").Value = BeTransReDet.IdProductoBodega
                DgridDetalleRec.Rows(i).Cells("KeyP").Value = BeTransReDet.Producto.IdProducto

                Dim bo As clsBeProducto = clsLnProducto.Get_Control_Vencimiento_By_IdProducto(BeTransReDet.Producto.IdProducto)
                DgridDetalleRec.Rows(i).Cells("ControlVencimiento").Value = bo.Control_vencimiento
                DgridDetalleRec.Rows(i).Cells("ControlPeso").Value = bo.Control_peso

                Dim lBeProductoPresentacion As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(BeTransReDet.Producto.IdProducto).ToList

                Dim IdPropietario As Integer = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(cmbBodega.EditValue, BeTransReDet.IdPropietarioBodega)

                Dim lBeProductoEstado As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodegaHH(IdPropietario, cmbBodega.EditValue).ToList

                If lBeProductoPresentacion.Count > 0 Then
                    If BeTransReDet.IdPresentacion <> 0 Then
                        If Not BeTransReDet.Presentacion Is Nothing Then
                            Llena_Presentacion_Grid2(lBeProductoPresentacion, i, BeTransReDet.Presentacion.IdPresentacion)
                        End If
                    End If
                Else
                    DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    DgComboPresentacion.DataSource = Nothing
                End If

                If BeTransReDet.IdPresentacion <> 0 Then
                    '#EJC20180315: Pendiente de analisis hasta realizar el cambio (por mí) en la hh.
                    If BeTransReDet.Presentacion.IdPresentacion <> 0 Then
                        If BeTransReDet.Presentacion.EsPallet Then
                            DgridDetalleRec.Rows(i).Cells("CantidadP").Value = BeTransReDet.cantidad_recibida '/ (Obj.Presentacion.Factor * Obj.Presentacion.CajasPorCama * Obj.Presentacion.CamasPorTarima)
                        Else
                            DgridDetalleRec.Rows(i).Cells("CantidadP").Value = BeTransReDet.cantidad_recibida '/ Obj.Presentacion.Factor
                        End If
                    End If
                End If

                If lBeProductoEstado.Count > 0 Then
                    Llena_Estados(lBeProductoEstado, i, BeTransReDet.ProductoEstado.IdEstado)
                Else
                    DgComboEstado = TryCast(DgridDetalleRec.Rows(i).Cells("Estado"), DataGridViewComboBoxCell)
                    DgComboEstado.DataSource = Nothing
                End If

                If BeTransReDet.Fecha_vence <> Nothing Then
                    If bo.Control_vencimiento Then
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value = BeTransReDet.Fecha_vence
                    Else
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value = "N/A"
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").ReadOnly = True
                    End If
                End If

                DgridDetalleRec.Rows(i).Cells("Lote").Value = BeTransReDet.Lote
                DgridDetalleRec.Rows(i).Cells("Peso").Value = BeTransReDet.Peso
                DgridDetalleRec.Rows(i).Cells("PesoUnitario").Value = BeTransReDet.Peso / BeTransReDet.cantidad_recibida
                DgridDetalleRec.Rows(i).Cells("CostoOC").Value = GetCostoByIdProducto(BeTransReDet.IdProductoBodega)
                DgridDetalleRec.Rows(i).Cells("Observacion").Value = BeTransReDet.Observacion
                DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value = BeTransReDet.IdRecepcionEnc
                DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value = BeTransReDet.IdRecepcionDet
                DgridDetalleRec.Rows(i).Cells("IsNewR").Value = BeTransReDet.IsNew
                DgridDetalleRec.Rows(i).Cells("No_Linea").Value = BeTransReDet.No_Linea
                DgridDetalleRec.Rows(i).Cells("atributo_variante_1").Value = BeTransReDet.Atributo_Variante_1
                DgridDetalleRec.Rows(i).Cells("lic_plate").Value = BeTransReDet.Lic_plate

                Set_TotalGrid2(i)

                '#EJC20210613: Lllenado manual de stock
                Dim pIndex As Integer = -1

                Dim BeStock_rec As New clsBeStock_rec With
                        {.IdRecepcionDet = BeTransReDet.IdRecepcionDet,
                        .IdPropietarioBodega = pIdPropietarioBodega,
                        .IdProductoBodega = BeTransReDet.IdProductoBodega,
                        .IsNew = True
                        }

                pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = BeTransReDet.IdProductoBodega)

                If pIndex >= -1 Then

                    If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then
                        BeStock_rec.IdStockRec = pListBeStockRec.Max(Function(b) b.IdStockRec) + 1
                    Else
                        BeStock_rec.IdStockRec = clsLnStock_rec.MaxID() + 1
                    End If

                    BeStock_rec.Presentacion.IdPresentacion = 0
                    BeStock_rec.Lic_plate = BeTransReDet.Lic_plate
                    BeStock_rec.Fecha_Ingreso = Date.Now
                    BeStock_rec.Fecha_Manufactura = Nothing
                    BeStock_rec.Serial = 0
                    BeStock_rec.Añada = 0
                    BeStock_rec.Peso = 0.0
                    BeStock_rec.Temperatura = 0.0
                    BeStock_rec.IdRecepcionDet = BeTransReDet.IdRecepcionDet
                    BeStock_rec.IdRecepcionEnc = BeTransReDet.IdRecepcionEnc
                    BeStock_rec.Fec_mod = Date.Now
                    BeStock_rec.User_mod = AP.UsuarioAp.IdUsuario
                    BeStock_rec.IdPropietarioBodega = BeTransReDet.IdPropietarioBodega
                    BeStock_rec.IdProductoBodega = BeTransReDet.IdProductoBodega
                    BeStock_rec.No_linea = BeTransReDet.No_Linea
                    pListBeStockRec.Add(BeStock_rec)

                End If

            Next

            DgridDetalleRec.ResumeLayout()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function GetCostoByIdProducto(ByVal pIdProducto As Integer) As Double

        GetCostoByIdProducto = 0

        Try

            'For i As Integer = 0 To DgridOC.Rows.Count - 1
            '    If DgridOC.Rows(i).Cells("IdProducto").Value = pIdProducto Then
            '        GetCostoByIdProducto = DgridOC.Rows(i).Cells("Costo").Value
            '        Exit For
            '    End If
            'Next

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Sub cmbGridPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim i As Integer = DgridDetalleRec.CurrentRow.Index

            DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
            'Console.Write(DgComboPresentacion.Value)

            If Not DgComboPresentacion.Value Is Nothing Then

                Dim Idx As Integer = -1

                Idx = pListBeStockRec.FindIndex(Function(p) _
                                     p.IdProductoBodega = CInt(DgridDetalleRec.Rows(i).Cells("IdProductoP").Value) AndAlso
                                     p.ProductoValidado = False AndAlso
                                     p.Presentacion.IdPresentacion = CInt(DgridDetalleRec.Rows(i).Cells("PresentacionP").Value))

                If Idx <> -1 Then

                    If pListBeStockRec(Idx).Presentacion.IdPresentacion <> DgComboPresentacion.Value Then

                        pListBeStockRec(Idx).Presentacion.IdPresentacion = DgComboPresentacion.Value

                        Set_Stock_Parametro(BeProducto, DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value, DgridDetalleRec.CurrentRow.Index, pListBeStockRec(Idx).IdProductoEstado)

                    End If

                Else

                    Idx = pListBeStockRec.FindIndex(Function(p) _
                                     p.IdProductoBodega = CInt(DgridDetalleRec.Rows(i).Cells("IdProductoP").Value) _
                                     AndAlso
                                     p.ProductoValidado = False _
                                     AndAlso
                                     p.No_linea = CInt(DgridDetalleRec.Rows(i).Cells("No_Linea").Value))

                    If Idx <> -1 Then

                        pListBeStockRec(Idx).Presentacion.IdPresentacion = DgComboPresentacion.Value

                        Set_Stock_Parametro(BeProducto, DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value, DgridDetalleRec.CurrentRow.Index, pListBeStockRec(Idx).IdProductoEstado)

                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try

    End Sub

    Private Function Calcula_Total_Grid() As Double

        Dim c As Double

        Try

            For x = 0 To DgridDetalleRec.Rows.Count - 1
                If String.IsNullOrEmpty(DgridDetalleRec.Rows(x).Cells("ProductoP").Value) = False Then
                    Dim str As String = DgridDetalleRec.Rows(x).Cells("TotalP").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(DgridDetalleRec.Item("TotalP".ToLower, x).Value)
                    End If
                End If
            Next

            Return c

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Function

    Private Sub setUnidadMedidaGrid2(ByVal Obj As Object, ByVal pIndex As Integer)

        Try

            Dim D As New DataTable("UnidadMedida")
            D.Columns.Add("Id", GetType(Integer))
            D.Columns.Add("Nombre", GetType(String))
            D.Rows.Add(Obj.UnidadMedida.IdUnidadMedida, Obj.UnidadMedida.Nombre)

            Dim DgCombo As New DataGridViewComboBoxCell()
            DgCombo = TryCast(DgridDetalleRec.Rows(pIndex).Cells("UnidadMedidaP"), DataGridViewComboBoxCell)
            DgCombo.DataSource = D
            DgCombo.ValueMember = "Id"
            DgCombo.DisplayMember = "Nombre"
            DgCombo.Value = CInt(D(0)(0))

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Presentacion_Grid2(ByVal pList As List(Of clsBeProducto_Presentacion),
                                         ByVal pIndex As Integer,
                                         Optional ByVal pIdPresentacion As Integer = 0)

        Try

            Dim DgCombo As New DataGridViewComboBoxCell()
            DgCombo = TryCast(DgridDetalleRec.Rows(pIndex).Cells("PresentacionP"), DataGridViewComboBoxCell)

            'If txtIdTipoTR.Text = "MSOC00" Then
            '    Dim item As New clsBeProducto_Presentacion With {.IdPresentacion = 0, .Nombre = ""}
            '    pList.Add(item)
            'End If

            DgCombo.DataSource = pList
            DgCombo.ValueMember = "IdPresentacion"
            DgCombo.DisplayMember = "Nombre"

            If pIdPresentacion <> 0 Then
                DgCombo.Value = pIdPresentacion
            End If

            'If txtIdTipoTR.Text = "MSOC00" Then
            '    DgCombo.Value = 0
            'End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Llena_Estados(ByVal pList As List(Of clsBeProducto_estado),
                             ByVal pIndex As Integer,
                             Optional ByVal pIdEstado As Integer = 0)

        Try

            Dim DgCombo As New DataGridViewComboBoxCell()
            DgCombo = TryCast(DgridDetalleRec.Rows(pIndex).Cells("Estado"), DataGridViewComboBoxCell)

            DgCombo.DataSource = pList
            DgCombo.ValueMember = "IdEstado"
            DgCombo.DisplayMember = "Nombre"

            If pIdEstado <> 0 Then
                DgCombo.Value = pIdEstado
            Else
                If pList.Count > 0 Then
                    If txtIdEstadoDefectoRecepcion.Text <> "" Then
                        DgCombo.Value = pList.Find(Function(x) x.IdEstado = txtIdEstadoDefectoRecepcion.Text).IdEstado
                    Else
                        DgCombo.Value = pList(0).IdEstado
                    End If
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function Get_Peso_Grid() As Double

        Dim c As Double

        Try

            For x = 0 To DgridDetalleRec.Rows.Count - 1
                If String.IsNullOrEmpty(DgridDetalleRec.Rows(x).Cells("ProductoP").Value) = False Then
                    Dim str As String = DgridDetalleRec.Rows(x).Cells("Peso").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(DgridDetalleRec.Item("Peso".ToLower, x).Value)
                    End If
                End If
            Next

            Return c

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Function

    Private Function SetCantidadGrid2() As Double

        Dim c As Double

        Try

            For x = 0 To DgridDetalleRec.Rows.Count - 1
                If String.IsNullOrEmpty(DgridDetalleRec.Rows(x).Cells("ProductoP").Value) = False Then
                    Dim str As String = DgridDetalleRec.Rows(x).Cells("CantidadP").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(DgridDetalleRec.Item("CantidadP".ToLower, x).Value)
                    End If
                End If
            Next

            Return c

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Function

    Private Function SetCostoGrid2() As Double

        Dim c As Double

        Try

            For x = 0 To DgridDetalleRec.Rows.Count - 1
                If String.IsNullOrEmpty(DgridDetalleRec.Rows(x).Cells("ProductoP").Value) = False Then
                    Dim str As String = DgridDetalleRec.Rows(x).Cells("CostoP").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(DgridDetalleRec.Item("CostoP".ToLower, x).Value)
                    End If
                End If
            Next

            Return c

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Function

    Private Sub Set_TotalGrid2(ByVal pIndex As Integer)

        Try

            If IsNumeric(DgridDetalleRec.Rows(pIndex).Cells("CantidadP").Value) AndAlso
                IsNumeric(DgridDetalleRec.Rows(pIndex).Cells("CostoP").Value) Then

                Dim costo As Decimal

                Dim cantidad As Decimal = DgridDetalleRec.Rows(pIndex).Cells("CantidadP").Value
                costo = CDec(DgridDetalleRec.Rows(pIndex).Cells("CostoP").Value)

                Dim total As Decimal = Math.Round(cantidad * costo, 6)
                Dim vPesoUnitario As Double = Math.Round(DgridDetalleRec.Rows(pIndex).Cells("PesoUnitario").Value, 6)

                DgridDetalleRec.Rows(pIndex).Cells("TotalP").Value = total

                If cantidad > 1 Then
                    'vPesoUnitario = vPesoUnitario / cantidad
                ElseIf cantidad = 1 Then

                    'Obtener el peso unitario

                    Dim vPesoPresentacion As Double = 0

                    If Not DgridDetalleRec.Rows(pIndex).Cells("PresentacionP").Value Is Nothing Then

                        Dim lIdPresentacion As Integer = DgridDetalleRec.Rows(pIndex).Cells("PresentacionP").Value
                        vPesoPresentacion = clsLnProducto_presentacion.Get_Peso_By_IdPresentacion(lIdPresentacion)

                    Else
                        'Revisar si esto está correcto
                        '#EJC20170718-02:34AM
                        If Not BeProducto Is Nothing Then
                            vPesoPresentacion = BeProducto.Peso_referencia
                        End If
                    End If

                    If DgridDetalleRec.Rows(pIndex).Cells("CantidadP").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(pIndex).Cells("CantidadP").Value IsNot Nothing Then
                        Validar_Cambio_Presentacion(pIndex)
                    End If

                End If

                DgridDetalleRec.Rows(pIndex).Cells("Peso").Value = vPesoUnitario * cantidad

                lblCantidadR.Text = "Cant: " & Format(SetCantidadGrid2(), "N6")
                lblCostoR.Text = "Costo: " & Format(SetCostoGrid2(), "c")
                lblTotalR.Text = "Total: " & Format(Calcula_Total_Grid(), "c")
                lblPesoR.Text = "Peso: " & Format(Get_Peso_Grid(), "N6")

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub lnkAgregarProducto_LinkClicked()

        Try

            ValidaCantidad()

            Using Producto As New frmProductoList(True)

                Producto.cmdImportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Producto.RbgActivo.Visible = False
                Producto.pIdBodega = cmbBodega.EditValue
                Producto.pIdPropietarioBodega = cmbPropietario.EditValue
                Producto.Modo = frmProductoList.pModo.Seleccion
                Producto.WindowState = FormWindowState.Maximized

                Producto.ShowDialog()

                If Producto.pObjProducto IsNot Nothing AndAlso Producto.pObjProducto.IdProducto <> 0 Then
                    pSelecciono = True
                    setProducto(Producto.pObjProducto)
                    pSelecciono = False
                End If

            End Using

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function GetCostoOrdenCompraByProducto(ByVal pProducto As String) As Double

        Try

            Dim Costo As Double = 0.0
            'For i As Integer = 0 To DgridOC.Rows.Count - 1

            '    If DgridOC.Rows(i).Cells("Producto").Value = pProducto Then
            '        Costo = CDbl(DgridOC.Rows(i).Cells("Costo").Value)
            '        Exit For
            '    End If

            'Next

            Return Costo

        Catch ex As Exception
            Return 0.0
        End Try

    End Function

    Private Sub Set_Stock_Parametro(ByVal pObjProducto As clsBeProducto,
                                    ByVal pIdRecepcionDet As Integer,
                                    ByVal pRowIndex As Integer,
                                    ByVal pIdEstado As Integer,
                                    Optional ByVal SkipPantallaParametros As Boolean = False)

        Dim vNoLinea As Integer = 0

        Try

            If DgridDetalleRec.Rows.Count > 0 Then

                Dim frmCapturaParametros As New frmCapturaParametroRecepcionS() _
                With
                    {.IdEmpresa = AP.IdEmpresa,
                    .IdBodega = cmbBodega.EditValue,
                    .IdPropietario = clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, cmbPropietario.EditValue),
                    .pIdPropietarioBodega = cmbPropietario.EditValue,
                    .pBeProducto = pObjProducto,
                    .pIdRecepcionDet = pIdRecepcionDet,
                    .pFechaRecepcion = dtmFechaRecepcion.EditValue,
                    .pIndex = pListBeStockRec.FindIndex(Function(f) _
                                                        f.IdRecepcionDet = pIdRecepcionDet)}

                If pListBeStockSeRec IsNot Nothing AndAlso pListBeStockSeRec.Count = 0 And frmCapturaParametros.pIndex > -1 Then
                    pListBeStockSeRec = clsLnStock_se_rec.GetAllSerieByIdStockRec(pListBeStockRec(frmCapturaParametros.pIndex).IdStockRec).ToList
                End If

                frmCapturaParametros.IdTipoTransaccion = txtIdTipoTR.Text

                If frmCapturaParametros.Mostrar_Parametros() Then

                    frmCapturaParametros.pListBeStockRec = pListBeStockRec
                    frmCapturaParametros.pListBeStockSeRec = pListBeStockSeRec
                    frmCapturaParametros.plistBeReDetParametros = plistBeReDetParametros
                    frmCapturaParametros.pListBeProductoPallet = pListBeProductoPallet
                    vNoLinea = Val(DgridDetalleRec.Rows(pRowIndex).Cells("No_Linea").Value)
                    frmCapturaParametros.pNoLinea = vNoLinea

                    If Not DgridDetalleRec.Rows(pRowIndex).Cells("PresentacionP").Value Is Nothing Then
                        frmCapturaParametros.pIdPresentacion = DgridDetalleRec.Rows(pRowIndex).Cells("PresentacionP").Value
                    Else
                        '#EJC20240812_1457:
                        Try

                            Dim BeTransOCDet As New clsBeTrans_oc_det
                            BeTransOCDet = pListObjOrdeCompraDet.Find(Function(x) x.IdProductoBodega = pObjProducto.IdProductoBodega)

                            If Not BeTransOCDet Is Nothing Then
                                frmCapturaParametros.pIdPresentacion = BeTransOCDet.IdPresentacion
                            End If

                        Catch ex As Exception
                            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                Text,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error)
                        End Try

                    End If

                    frmCapturaParametros.IdTipoTransaccion = txtIdTipoTR.Text

                    Dim vLicActual As String = DgridDetalleRec.Rows(pRowIndex).Cells("Lic_Plate").Value
                    frmCapturaParametros.pNumeroLP = vLicActual

                    If frmCapturaParametros.ShowDialog() = DialogResult.OK Then

                        DgridDetalleRec.Rows(pRowIndex).Cells("PesoUnitario").Value = frmCapturaParametros.txtPesoReal.Value
                        DgridDetalleRec.Rows(pRowIndex).Cells("Peso").Value = frmCapturaParametros.txtPesoReal.Value
                        '#CKFK 20210901 Agregué esta línea para que se guardara el LP cuando el ingreso sea manual
                        DgridDetalleRec.Rows(pRowIndex).Cells("Lic_Plate").Value = frmCapturaParametros.txtLicPlate.Text

                        If frmCapturaParametros.txtLicPlate.Text.Trim() <> "" Then
                            DgridDetalleRec.Rows(pRowIndex).Cells("Lic_Plate").ReadOnly = True
                        End If

                        Dim vCantidad As Double = DgridDetalleRec.Rows(pRowIndex).Cells("CantidadP").Value
                        Dim vPesoUnitario As Double = DgridDetalleRec.Rows(pRowIndex).Cells("PesoUnitario").Value

                        If vCantidad = 0 Then
                            DgridDetalleRec.Rows(pRowIndex).Cells("CantidadP").Value = 1
                            vCantidad = 1
                        End If

                        DgridDetalleRec.Rows(pRowIndex).Cells("Peso").Value = vCantidad * vPesoUnitario

                        pListBeStockRec = frmCapturaParametros.pListBeStockRec
                        pListBeStockSeRec = frmCapturaParametros.pListBeStockSeRec
                        plistBeReDetParametros = frmCapturaParametros.plistBeReDetParametros
                        pListBeProductoPallet = frmCapturaParametros.pListBeProductoPallet

                        pListObjProductoP = clsLnProducto_presentacion.Get_All_By_IdProducto(pObjProducto.IdProducto).ToList

                        If frmCapturaParametros.pBePresentacionProducto IsNot Nothing AndAlso pListObjProductoP.Count > 0 Then
                            Llena_Presentacion_Grid2(pListObjProductoP, pRowIndex, frmCapturaParametros.pBePresentacionProducto.IdPresentacion)
                            '#EJC20240808 Agregado,
                            Guardar_Stock_Rec(pObjProducto, pIdEstado)
                        Else
                            DgridDetalleRec.Rows(pRowIndex).Cells("PresentacionP").Value = Nothing
                            Dim DgCombo As New DataGridViewComboBoxCell()
                            DgCombo = TryCast(DgridDetalleRec.Rows(pRowIndex).Cells("PresentacionP"), DataGridViewComboBoxCell)
                            DgCombo.DataSource = Nothing
                        End If

                    Else
                        Guardar_Stock_Rec(pObjProducto, pIdEstado)
                    End If

                Else
                    Guardar_Stock_Rec(pObjProducto, pIdEstado)
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Sub

    Private Sub Guardar_Stock_Rec(ByVal pObjProducto As clsBeProducto,
                                  ByVal IdEstado As Integer,
                                  Optional ByVal CheckManualInput As Boolean = True)

        Dim pIdRecepcionDet As Integer
        Dim pIndex As Integer
        Dim vNoLinea As Integer = 0

        Try

            pIdPropietarioBodega = cmbPropietario.EditValue

            If Not DgridDetalleRec.CurrentRow Is Nothing Then
                vNoLinea = DgridDetalleRec.Rows(DgridDetalleRec.CurrentRow.Index).Cells("No_Linea").Value
            Else
                vNoLinea = 0
            End If

            pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = pObjProducto.IdProductoBodega _
                                                                              AndAlso f.IdProductoEstado = IdEstado _
                                                                              AndAlso f.No_linea = vNoLinea AndAlso f.IdRecepcionDet = pListBeStockRec(pIndex).IdRecepcionDet)

            If pIndex > -1 Then
                pIdRecepcionDet = pListBeStockRec(pIndex).IdRecepcionDet
            Else

                If CheckManualInput Then

                    '#EJC20200311: Si modifican él código de producto en la misma línea, eliminar el objeto anterior.
                    Dim vIndiceAnte As Integer = pListBeStockRec.FindIndex(Function(f) f.No_linea = vNoLinea)

                    If vIndiceAnte <> -1 Then

                        If pListBeStockRec(vIndiceAnte).IdProductoBodega <> pObjProducto.IdProducto Then
                            'pListBeStockRec.RemoveAt(vIndiceAnte)
                            'pIdRecepcionDet = -1
                        End If

                    End If

                End If

                If lBeTransRecDet IsNot Nothing AndAlso lBeTransRecDet.Count > 0 Then
                    pIdRecepcionDet = lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1
                Else
                    pIdRecepcionDet = lMaxIdRecepcionDetParametro
                End If

            End If

            Dim BeStock_rec As New clsBeStock_rec With
                        {.IdRecepcionDet = pIdRecepcionDet,
                        .IdPropietarioBodega = pIdPropietarioBodega,
                        .IdProductoBodega = pObjProducto.IdProductoBodega,
                        .IsNew = True
                        }

            '#GT30122024: como obtenemos un nuevo pIdrecepcionDet, se debe validar que no exista en el objeto Lista Stock Rec, de lo contrario, si se reciben varias lineas del mismo producto, solo insertará una.
            pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = pObjProducto.IdProductoBodega AndAlso f.IdRecepcionDet = pIdRecepcionDet)

            If pIndex = -1 Then

                If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then
                    BeStock_rec.IdStockRec = pListBeStockRec.Max(Function(b) b.IdStockRec) + 1
                Else
                    BeStock_rec.IdStockRec = clsLnStock_rec.MaxID() + 1
                End If

                'BeStock_rec.Presentacion.IdPresentacion = 0
                BeStock_rec.Lic_plate = 0
                BeStock_rec.Fecha_Ingreso = Date.Now
                BeStock_rec.Fecha_Manufactura = Nothing
                BeStock_rec.Serial = 0
                BeStock_rec.Añada = 0
                BeStock_rec.Peso = 0.0
                BeStock_rec.Temperatura = 0.0
                BeStock_rec.Fec_mod = Date.Now
                BeStock_rec.User_mod = AP.UsuarioAp.IdUsuario
                BeStock_rec.IdPropietarioBodega = pIdPropietarioBodega
                BeStock_rec.IdProductoBodega = pObjProducto.IdProductoBodega
                BeStock_rec.No_linea = DgridDetalleRec.Rows(DgridDetalleRec.CurrentRow.Index).Cells("No_Linea").Value
                pListBeStockRec.Add(BeStock_rec)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Sub setProducto(ByVal ObjP As clsBeProducto)

        Try

            Dim i As Integer = RowIndex()

            If ObjP.IdProducto > 0 Then

                If ObjP.UnidadMedida IsNot Nothing Then
                    setUnidadMedidaGrid2(ObjP, i)
                Else
                    Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", ObjP.Nombre))
                End If

                DgridDetalleRec.Rows(i).Cells("CodigoP").Value = ObjP.Codigo
                DgridDetalleRec.Rows(i).Cells("IdProductoP").Value = ObjP.IdProductoBodega
                DgridDetalleRec.Rows(i).Cells("KeyP").Value = ObjP.IdProducto
                DgridDetalleRec.Rows(i).Cells("ProductoP").Value = ObjP.Nombre

                If txtIdOrdenCompra.Text.Trim <> "" AndAlso Not DgridDetalleRec.Rows(i).Cells("CostoP").Value Is Nothing Then
                    DgridDetalleRec.Rows(i).Cells("CostoP").Value = GetCostoOrdenCompraByProducto(ObjP.Nombre)
                    DgridDetalleRec.Rows(i).Cells("CostoOC").Value = DgridDetalleRec.Rows(i).Cells("CostoP").Value
                Else
                    DgridDetalleRec.Rows(i).Cells("CostoP").Value = ObjP.Costo
                    DgridDetalleRec.Rows(i).Cells("CostoOC").Value = ObjP.Costo
                End If

                If ObjP.Control_lote And ObjP.Genera_lote Then

                    Dim anio As String = Mid(Now.Date.Year.ToString, 3)
                    Dim mes As String = String.Empty

                    If Now.Date.Month.ToString.Length = 1 Then
                        mes = String.Format("0{0}", Now.Date.Month.ToString)
                    Else
                        mes = Now.Date.Month.ToString
                    End If

                    Dim dia As String = String.Empty

                    If Now.Date.Day.ToString.Length = 1 Then
                        dia = String.Format("0{0}", Now.Date.Day.ToString)
                    Else
                        dia = Now.Date.Day.ToString
                    End If

                    Dim lCorrelativo As String = String.Format("{0}{1}{2}0001", anio, mes, dia)
                    DgridDetalleRec.Rows(i).Cells("Lote").ReadOnly = True
                    DgridDetalleRec.Rows(i).Cells("Lote").Value = lCorrelativo
                    DgridDetalleRec.Update()

                ElseIf ObjP.Control_lote AndAlso Not ObjP.Genera_lote Then

                    DgridDetalleRec.Rows(i).Cells("Lote").ReadOnly = False
                    DgridDetalleRec.Update()

                ElseIf Not ObjP.Control_lote Then

                    DgridDetalleRec.Rows(i).Cells("Lote").ReadOnly = True
                    DgridDetalleRec.Update()

                End If

                If ObjP.Control_peso Then
                    DgridDetalleRec.Rows(i).Cells("Peso").ReadOnly = False
                Else
                    DgridDetalleRec.Rows(i).Cells("Peso").ReadOnly = True
                End If

                Dim lBeProductoEstado As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodegaHH(ObjP.IdPropietario, cmbBodega.EditValue).ToList

                pListObjProductoP = clsLnProducto_presentacion.Get_All_By_IdProducto(ObjP.IdProducto).ToList

                If pListObjProductoP IsNot Nothing AndAlso pListObjProductoP.Count > 0 Then
                    Llena_Presentacion_Grid2(pListObjProductoP, i)
                Else
                    Dim DgCombo As New DataGridViewComboBoxCell()
                    DgCombo = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    DgCombo.DataSource = Nothing
                End If

                If lBeProductoEstado.Count > 0 Then
                    Llena_Estados(lBeProductoEstado, i)
                Else
                    Dim DgCombo As New DataGridViewComboBoxCell()
                    DgCombo = TryCast(DgridDetalleRec.Rows(i).Cells("Estado"), DataGridViewComboBoxCell)
                    DgCombo.DataSource = Nothing
                End If

                DgridDetalleRec.Rows(i).Cells("IsNewR").Value = True
                DgridDetalleRec.Rows(i).Cells("ControlVencimiento").Value = ObjP.Control_vencimiento
                DgridDetalleRec.Rows(i).Cells("ControlPeso").Value = ObjP.Control_peso

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Sub

    Private Function RowIndex() As Integer

        Dim i As Integer = -1

        For i = 0 To DgridDetalleRec.Rows.Count - 1
            If DgridDetalleRec.Rows(i).Cells("ProductoP").Value Is Nothing Then
                Return i
            End If
        Next

    End Function

    Dim pIndiceListaStock As Integer = -1

    Dim BeProducto As clsBeProducto = Nothing

    Private Sub grdListaRecepcion_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgridDetalleRec.CellValueChanged

        Try

            If DgridDetalleRec.RowCount > 0 Then

                DgridDetalleRec.EndEdit()

                Dim vIndiceRecDet As Integer = -1

                '#20180113: Buscar atributo_variante_1/Cod_Variante por número de línea en la O.C.
                If DgridDetalleRec.Columns(e.ColumnIndex).Name() = "No_Linea" Then

                    Dim vNoLinea As Integer = Val(DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value)
                    Dim vTransOcDet As New clsBeTrans_oc_det

                    If Not BeTransOcEnc Is Nothing Then

                        If BeTransOcEnc.IdOrdenCompraEnc <> 0 Then

                            vTransOcDet = BeTransOcEnc.DetalleOC.Find(Function(x) x.No_Linea = vNoLinea)

                            If Not vTransOcDet Is Nothing Then
                                DgridDetalleRec.Rows(e.RowIndex).Cells("atributo_variante_1").Value = vTransOcDet.Atributo_variante_1
                            End If

                        End If
                    End If

                ElseIf DgridDetalleRec.Columns(e.ColumnIndex).Name() = "CodigoP" Then

                    Dim lCodigo As String = DgridDetalleRec.Rows(e.RowIndex).Cells("CodigoP").Value

                    If lCodigo Is Nothing Then Exit Sub

                    If clsLnProducto.Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega(cmbBodega.EditValue, cmbPropietario.EditValue, lCodigo) = False Then
                        Throw New Exception(String.Format("El Código {0} no existe o no está asociado a la bodega", lCodigo))
                    Else

                        BeProducto = New clsBeProducto
                        BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(lCodigo, cmbBodega.EditValue)

                        If BeProducto Is Nothing Then
                            Throw New Exception(String.Format("El Código {0} no existe.", lCodigo))
                        Else
                            '#EJC20180411: Almacenar el código por el que fue encontrado (el que se digitó en el grid)
                            'Se utilizará más tarde, para encontrar la presentación asociada al código.
                            BeProducto.Tag = lCodigo
                        End If

                        If BeProducto.IdProducto > 0 Then

                            If BeProducto.Control_lote AndAlso BeProducto.Genera_lote Then

                                Dim anio As String = Mid(Now.Date.Year.ToString, 3)
                                Dim mes As String = String.Empty

                                If Now.Date.Month.ToString.Length = 1 Then
                                    mes = String.Format("0{0}", Now.Date.Month.ToString)
                                Else
                                    mes = Now.Date.Month.ToString
                                End If

                                Dim dia As String = String.Empty

                                If Now.Date.Day.ToString.Length = 1 Then
                                    dia = String.Format("0{0}", Now.Date.Day.ToString)
                                Else
                                    dia = Now.Date.Day.ToString
                                End If

                                Dim lCorrelativo As String = String.Format("{0}{1}{2}0001", anio, mes, dia)
                                DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").ReadOnly = True
                                DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").Value = lCorrelativo
                                DgridDetalleRec.Update()

                            ElseIf BeProducto.Control_lote AndAlso Not BeProducto.Genera_lote Then
                                DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").ReadOnly = False
                                DgridDetalleRec.Update()
                            ElseIf Not BeProducto.Control_lote Then
                                DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").ReadOnly = True
                                DgridDetalleRec.Update()
                            End If

                            If BeProducto.Control_peso Then
                                DgridDetalleRec.Rows(e.RowIndex).Cells("Peso").ReadOnly = False
                            Else
                                DgridDetalleRec.Rows(e.RowIndex).Cells("Peso").ReadOnly = True
                            End If

                            If BeProducto.UnidadMedida IsNot Nothing Then
                                setUnidadMedidaGrid2(BeProducto, e.RowIndex)
                            Else
                                Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", BeProducto.Nombre))
                            End If

                            DgridDetalleRec.Rows(e.RowIndex).Cells("IdProductoP").Value = BeProducto.IdProductoBodega
                            DgridDetalleRec.Rows(e.RowIndex).Cells("KeyP").Value = BeProducto.IdProducto
                            DgridDetalleRec.Rows(e.RowIndex).Cells("ProductoP").Value = BeProducto.Nombre

                            If txtIdOrdenCompra.Text.Trim <> "" AndAlso Not DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value Is Nothing Then
                                DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value = GetCostoOrdenCompraByProducto(BeProducto.Nombre)
                                DgridDetalleRec.Rows(e.RowIndex).Cells("CostoOC").Value = DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value
                            Else
                                DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value = BeProducto.Costo
                                DgridDetalleRec.Rows(e.RowIndex).Cells("CostoOC").Value = BeProducto.Costo
                            End If

                            pListObjProductoP = clsLnProducto_presentacion.Get_All_By_IdProducto(BeProducto.IdProducto).ToList

                            Dim le As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodegaHH(BeProducto.IdPropietario, cmbBodega.EditValue).ToList
                            Dim BePres As New clsBeProducto_Presentacion

                            If pListObjProductoP IsNot Nothing AndAlso pListObjProductoP.Count > 0 Then

                                BePres = pListObjProductoP.Find(Function(x) x.Codigo_barra = lCodigo)

                                If Not BePres Is Nothing Then
                                    Llena_Presentacion_Grid2(pListObjProductoP, e.RowIndex, BePres.IdPresentacion)
                                Else
                                    Llena_Presentacion_Grid2(pListObjProductoP, e.RowIndex)
                                End If

                            Else
                                Dim DgCombo As New DataGridViewComboBoxCell()
                                DgCombo = TryCast(DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP"), DataGridViewComboBoxCell)
                                DgCombo.DataSource = Nothing
                            End If

                            'AndAlso pObjOC.listaD.Count > 0'

                            If Not (BeTransOcEnc Is Nothing) AndAlso Not (BeTransOcEnc.DetalleOC Is Nothing) Then

                                Dim LIst As List(Of clsBeTrans_oc_det) = BeTransOcEnc.DetalleOC.ToList

                                If LIst IsNot Nothing AndAlso LIst.Count > 0 Then

                                    Dim vNoLinea As Integer = Val(DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value)

                                    Dim lIndex As Integer = LIst.FindIndex(Function(b) b.IdProductoBodega = BeProducto.IdProductoBodega _
                                                                           AndAlso b.Producto.IdProducto = BeProducto.IdProducto AndAlso b.No_Linea = vNoLinea)

                                    If lIndex > -1 Then

                                        If LIst(lIndex).Presentacion.IdPresentacion = 0 Then
                                            DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").Value = Nothing
                                            DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").ReadOnly = True
                                        Else
                                            Dim DgCombo As New DataGridViewComboBoxCell()
                                            DgCombo = TryCast(DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP"), DataGridViewComboBoxCell)
                                            DgCombo.Value = LIst(lIndex).Presentacion.IdPresentacion
                                            DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").ReadOnly = False
                                        End If

                                        '#EJC20220407:Yo se que lo voy a lograr...
                                        DgridDetalleRec.Rows(e.RowIndex).Cells("IdOrdenCompraEnc").Value = LIst(lIndex).IdOrdenCompraEnc
                                        DgridDetalleRec.Rows(e.RowIndex).Cells("IdOrdenCompraDet").Value = LIst(lIndex).IdOrdenCompraDet

                                    End If

                                End If

                            End If

                            If le.Count > 0 Then
                                Llena_Estados(le, e.RowIndex)
                            Else
                                Dim DgCombo As New DataGridViewComboBoxCell()
                                DgCombo = TryCast(DgridDetalleRec.Rows(e.RowIndex).Cells("Estado"), DataGridViewComboBoxCell)
                                DgCombo.DataSource = Nothing
                            End If

                            DgridDetalleRec.Rows(e.RowIndex).Cells("IsNewR").Value = True
                            DgridDetalleRec.Rows(e.RowIndex).Cells("ControlVencimiento").Value = BeProducto.Control_vencimiento
                            DgridDetalleRec.Rows(e.RowIndex).Cells("ControlPeso").Value = BeProducto.Control_peso
                            DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").ReadOnly = Not BeProducto.Control_vencimiento

                            If lBeTransRecDet IsNot Nothing AndAlso lBeTransRecDet.Count > 0 Then

                                Dim vIdEstado As Integer = 0
                                Dim DgCombo As New DataGridViewComboBoxCell()
                                DgCombo = TryCast(DgridDetalleRec.Rows(e.RowIndex).Cells("Estado"), DataGridViewComboBoxCell)
                                vIdEstado = DgCombo.Value

                                DgridDetalleRec.Rows(e.RowIndex).Cells("IdRecepcionDet").Value = lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1

                                If Val(DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value) = 0 Then
                                    DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value = lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1
                                End If

                                Set_Stock_Parametro(BeProducto, lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1, e.RowIndex, vIdEstado)

                            Else

                                Dim vNoLinea As Integer = DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value
                                Dim vIdEstado As Integer = 0
                                Dim DgCombo As New DataGridViewComboBoxCell()
                                DgCombo = TryCast(DgridDetalleRec.Rows(e.RowIndex).Cells("Estado"), DataGridViewComboBoxCell)
                                vIdEstado = DgCombo.Value

                                vIndiceRecDet = pListBeStockRec.FindIndex(Function(b) b.IdRecepcionDet = DgridDetalleRec.Rows(e.RowIndex).Cells("IdRecepcionDet").Value _
                                                                          AndAlso b.IdProductoEstado = vIdEstado _
                                                                          AndAlso b.No_linea = vNoLinea)

                                If vIndiceRecDet = -1 Then

                                    If pListBeStockRec.Count <> 0 Then

                                        Dim vIndiceRecDetAnt As Integer = pListBeStockRec.FindIndex(Function(b) _
                                                                             b.IdRecepcionDet = DgridDetalleRec.Rows(e.RowIndex).Cells("IdRecepcionDet").Value _
                                                                             AndAlso b.No_linea = vNoLinea)

                                        If vIndiceRecDetAnt <> -1 Then
                                            pListBeStockRec.RemoveAt(vIndiceRecDetAnt)
                                            lMaxIdRecepcionDetParametro -= 1
                                        Else
                                            lMaxIdRecepcionDetParametro += 1
                                        End If

                                    Else
                                        lMaxIdRecepcionDetParametro += 1
                                    End If

                                Else
                                    If lBeTransRecDet.Count > 0 Then
                                        lMaxIdRecepcionDetParametro = lBeTransRecDet(vIndiceRecDet).IdRecepcionDet
                                    Else
                                        lMaxIdRecepcionDetParametro = 1
                                    End If
                                End If

                                Dim vIdRecepcionDet As Integer = Get_IdRecepcionDet_Rec_BOF()
                                DgridDetalleRec.Rows(e.RowIndex).Cells("IdRecepcionDet").Value = vIdRecepcionDet

                                If Val(DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value) = 0 Then
                                    DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value = vIdRecepcionDet
                                End If

                                Set_Stock_Parametro(BeProducto, vIdRecepcionDet, e.RowIndex, vIdEstado)

                                DgridDetalleRec.Rows(e.RowIndex).Cells("IdRecepcionDet").Value = vIdRecepcionDet

                            End If

                        Else
                            Throw New Exception(String.Format("El Código {0} no existe.", lCodigo))
                        End If

                    End If

                ElseIf DgridDetalleRec.Columns(e.ColumnIndex).Name() = "CantidadP" Or DgridDetalleRec.Columns(e.ColumnIndex).Name() = "CostoP" Then
                    Set_TotalGrid2(e.RowIndex)
                ElseIf DgridDetalleRec.Columns(e.ColumnIndex).Name() = "PresentacionP" Or DgridDetalleRec.Columns(e.ColumnIndex).Name() = "TotalP" Then

                    '#EJC20171018_0928AM: Fix, error al colocar peso presentación por el Or, 
                    'la columna afectada es el TotalP, pero siempre se ejecutaba la presentación aunque no fuera esa columna.
                    If DgridDetalleRec.Columns(e.ColumnIndex).Name() = "PresentacionP" Then
                        Set_Peso_Presentacion(e.RowIndex, e.ColumnIndex)
                    End If

                    If DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value IsNot Nothing Then
                        Validar_Cambio_Presentacion(e.RowIndex)
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("Error: {0} ", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Validar_Cambio_Presentacion(ByVal pIndexRow As Integer)

        Try

            Dim lIdProducto As Integer = DgridDetalleRec.Rows(pIndexRow).Cells("KeyP").Value
            Dim lIdProductoBodega As Integer = DgridDetalleRec.Rows(pIndexRow).Cells("IdProductoP").Value
            Dim lIdPresentacion As Integer = DgridDetalleRec.Rows(pIndexRow).Cells("PresentacionP").Value
            Dim lCantidad As Integer = DgridDetalleRec.Rows(pIndexRow).Cells("CantidadP").Value

            '#EJC20171018_0916AM: Se valida primero el encabezado si el encabezado es nothing, el detalle throw exception por object reference null
            If Not (BeTransOcEnc Is Nothing) Then

                '#EJC20171018_0916AM: Se valida después el detalle porque si el encabezado es nothing, el detalle throw exception por object reference null
                If Not (BeTransOcEnc.DetalleOC Is Nothing) Then

                    Dim LIst As List(Of clsBeTrans_oc_det) = BeTransOcEnc.DetalleOC.ToList

                    If LIst IsNot Nothing AndAlso LIst.Count > 0 Then

                        Dim lIndex As Integer = LIst.FindIndex(Function(b) b.IdProductoBodega = lIdProductoBodega _
                                                               AndAlso b.Presentacion.IdProducto = lIdProducto _
                                                               AndAlso b.Presentacion.IdPresentacion = lIdPresentacion)

                        If lIndex > -1 = False Then

                            Dim lFactorR As Double = clsLnProducto_presentacion.Get_Factor_By_IdProducto_And_IdPresentacion(lIdProducto, lIdPresentacion)
                            Dim ltotalR As Double = lCantidad * lFactorR

                            lIndex = BeTransOcEnc.DetalleOC.ToList.FindIndex(Function(b) b.IdProductoBodega = lIdProductoBodega AndAlso b.Presentacion.IdProducto = lIdProducto)

                            If lIndex > -1 Then

                                Dim lFactorOC As Double = clsLnProducto_presentacion.Get_Factor_By_IdProducto_And_IdPresentacion(lIdProducto, LIst(lIndex).Presentacion.IdPresentacion)
                                Dim lCantidadOC As Double = LIst(lIndex).Cantidad
                                Dim lTotalOC As Double = lCantidadOC * lFactorOC

                                Dim lEquivale As Double = lTotalOC / lFactorR

                                If ltotalR > lTotalOC Then
                                    XtraMessageBox.Show(String.Format("La cantidad {0} ingresada sobrepasa la cantidad en presentación {1} La cantidad equivalente para esta presentación es: {2}", lCantidad, LIst(lIndex).Presentacion.Nombre, lEquivale), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub Set_Peso_Presentacion(ByVal pIndexRow As Integer, ByVal pIndexColumn As Integer)

        Try

            Dim lIdPresentacion As Integer = DgridDetalleRec.Rows(pIndexRow).Cells(pIndexColumn).Value
            DgridDetalleRec.Rows(pIndexRow).Cells("PesoPresentacion").Value = clsLnProducto_presentacion.Get_Peso_By_IdPresentacion(lIdPresentacion)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Sub

    Dim FechaVencimientoCell As DataGridViewCell
    Dim LoteCell As DataGridViewCell

    Private Sub grdListaRecepcion_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DgridDetalleRec.RowValidating

        Try

            FechaVencimientoCell = DgridDetalleRec.Rows(e.RowIndex).Cells(DgridDetalleRec.Columns("FechaVencimiento").Index)
            LoteCell = DgridDetalleRec.Rows(e.RowIndex).Cells(DgridDetalleRec.Columns("Lote").Index)

            If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("ProductoP").Value) = False Then

                If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value) Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").ErrorText = "La Cantidad no puede estar vacía."
                ElseIf IsNumeric(DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value) = False Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").ErrorText = "La Cantidad debe ser un valor númerico."
                ElseIf CDbl(DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value) = 0 Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").ErrorText = "La Cantidad no puede ser 0."
                Else
                    e.Cancel = False
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").ErrorText = String.Empty
                End If

                If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value) Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").ErrorText = "El Costo no puede estar vacío."
                ElseIf IsNumeric(DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value) = False Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").ErrorText = "El Costo debe ser un valor númerico."
                Else
                    e.Cancel = False
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").ErrorText = String.Empty
                End If

                If Not BeProducto Is Nothing Then

                    If BeProducto.Control_vencimiento Then
                        If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").Value) Then
                            e.Cancel = True
                            FechaVencimientoCell.ErrorText = "Ingrese vencimiento"
                            Exit Sub
                        Else
                            Dim pFechavence As Date = DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").Value

                            If Validos_Tiempos_Aceptacion_Proveedor(pFechavence) Then
                                e.Cancel = False
                                FechaVencimientoCell.ErrorText = ""
                            Else
                                e.Cancel = True
                                FechaVencimientoCell.ErrorText = "Vencimiento no válido."
                                DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").ErrorText = "Vencimiento no válido."
                                DgridDetalleRec.Focus()
                            End If
                        End If
                    Else
                        e.Cancel = False
                        FechaVencimientoCell.ErrorText = ""
                    End If

                    If BeProducto.Control_lote Then

                        If pBodega.Homologar_Lote_Vencimiento Then

                            Dim vFechaDefecto As New Date(1900, 1, 1)

                            If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").Value) Then
                                e.Cancel = True
                                LoteCell.ErrorText = "Ingrese lote"
                                DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").ErrorText = "Ingrese lote"
                            Else

                                Dim result() As DataRow = DT.Select("lote = '" & LoteCell.Value & "'")
                                Dim fechaVence As Date = IIf(IsDBNull(FechaVencimientoCell.Value), New Date(1900, 1, 1), FechaVencimientoCell.Value)
                                Dim vCodigoProducto As String = IIf(IsDBNull(DgridDetalleRec.Rows(e.RowIndex).Cells("ProductoP").Value), "", DgridDetalleRec.Rows(e.RowIndex).Cells("ProductoP").Value)

                                If Not Existe_Lote_Con_Vencimiento_Diferente(e.RowIndex,
                                                                             vCodigoProducto,
                                                                             LoteCell.Value,
                                                                             FechaVencimientoCell.Value) Then

                                    DT.Rows.Add(fechaVence, LoteCell.Value)

                                Else
                                    e.Cancel = True
                                    FechaVencimientoCell.ErrorText = "Lote existe con vencimiento diferente, homologación de lote activa."
                                    Exit Sub

                                End If

                            End If
                        Else

                            If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").Value) Then
                                e.Cancel = True
                                LoteCell.ErrorText = "Ingrese lote"
                                DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").ErrorText = "Ingrese lote"
                            Else
                                e.Cancel = False
                                LoteCell.ErrorText = ""
                            End If

                        End If
                    Else
                        e.Cancel = False
                        LoteCell.ErrorText = ""
                    End If

#Region "Validación no_linea"
                    Dim vNoLinea As String = IIf(IsDBNull(DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value), "", DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value)

                    'If vNoLinea <> "" Then

                    '    Dim Tiene_campos_iguales As Boolean = False
                    '    For Each Fila As DataGridViewRow In DgridDetalleRec.Rows

                    '        If DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value = Fila.Cells(0).Value Then
                    '            If DgridDetalleRec.Rows(e.RowIndex).Cells("CodigoP").Value = Fila.Cells(1).Value Then
                    '                If BeProducto.Control_lote AndAlso BeProducto.Control_vencimiento Then
                    '                    If DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").Value = Fila.Cells(13).Value AndAlso
                    '                        DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").Value = Fila.Cells(11).Value AndAlso
                    '                        DgridDetalleRec.Rows(e.RowIndex).Cells("Estado").Value = Fila.Cells(12).Value AndAlso
                    '                        DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").Value = Fila.Cells(3).Value Then
                    '                        Tiene_campos_iguales = True
                    '                    End If
                    '                ElseIf BeProducto.Control_lote Then
                    '                    If DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").Value = Fila.Cells(13).Value AndAlso
                    '                        DgridDetalleRec.Rows(e.RowIndex).Cells("Estado").Value = Fila.Cells(12).Value AndAlso
                    '                        DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").Value = Fila.Cells(3).Value Then
                    '                        Tiene_campos_iguales = True
                    '                    End If
                    '                ElseIf BeProducto.Control_vencimiento Then
                    '                    If DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").Value = Fila.Cells(11).Value AndAlso
                    '                        DgridDetalleRec.Rows(e.RowIndex).Cells("Estado").Value = Fila.Cells(12).Value AndAlso
                    '                        DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").Value = Fila.Cells(3).Value Then
                    '                        Tiene_campos_iguales = True
                    '                    End If
                    '                Else
                    '                    If DgridDetalleRec.Rows(e.RowIndex).Cells("Estado").Value = Fila.Cells(12).Value AndAlso
                    '                        DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").Value = Fila.Cells(3).Value Then
                    '                        Tiene_campos_iguales = True
                    '                    End If
                    '                End If

                    '                If Not Tiene_campos_iguales Then
                    '                    e.Cancel = True
                    '                    DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = "Tiene campos distintos, no puede usar un número de línea ya asociado."
                    '                    Exit Sub
                    '                Else
                    '                    e.Cancel = False
                    '                    DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = ""
                    '                End If

                    '            End If
                    '        End If
                    '    Next

                    'End If

                    If Not BeTransOcEnc Is Nothing AndAlso BeTransOcEnc.IdOrdenCompraEnc <> 0 Then

                        If vNoLinea <> "" Then

                            If Not IsNumeric(vNoLinea) Then
                                e.Cancel = True
                                DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = "El número de línea debe ser un valor numérico"
                                Exit Sub
                            End If

                            Dim ExisteNoLineaEnPC As Boolean = False

                            For Each DetInOc In BeTransOcEnc.DetalleOC
                                If DetInOc.No_Linea = vNoLinea Then
                                    ExisteNoLineaEnPC = True
                                    Exit For
                                End If
                            Next

                            If Not ExisteNoLineaEnPC Then
                                e.Cancel = True
                                DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = "El número de línea ingresado no corresponde a ninguno relacionado con el documento de ingreso."
                                Exit Sub
                            Else
                                If Not e.Cancel Then
                                    e.Cancel = False
                                    DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = ""
                                End If
                            End If

                        Else

                            Dim PCContieneLineasVacias As Boolean = False
                            For Each DetInOc In BeTransOcEnc.DetalleOC
                                If DetInOc.No_Linea = 0 Then
                                    PCContieneLineasVacias = True
                                End If
                            Next

                            If Not PCContieneLineasVacias Then
                                e.Cancel = True
                                DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = "Ingrese número de línea relacionado con el documento de ingreso."
                                Exit Sub
                            Else
                                e.Cancel = False
                                DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = ""
                            End If

                        End If

                    End If
#End Region

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function Validos_Tiempos_Aceptacion_Proveedor(pFechaIngresadaVence As Date) As Boolean

        Validos_Tiempos_Aceptacion_Proveedor = True

        Try

            Dim pFechaHoy As Date = Now.Date
            Dim pTipoIngreso As clsBeTrans_oc_ti = BeTransOcEnc.TipoIngreso
            Dim pTiemposProveedorList As List(Of clsBeProveedor_tiempos) = BeTransOcEnc.ProveedorBodega.Proveedor.TiemposProveedor
            Dim pTiempoProveedor As New clsBeProveedor_tiempos

            If pTiemposProveedorList IsNot Nothing Then
                pTiempoProveedor = pTiemposProveedorList.Find(Function(x) x.IdClasificacion = BeProducto.IdClasificacion AndAlso
                                                                                              x.IdFamilia = BeProducto.IdFamilia)
            End If

            If pTiempoProveedor IsNot Nothing Then

                If (pTiempoProveedor.Dias_Exterior > 0 OrElse pTiempoProveedor.Dias_Local > 0) Then

                    If pTipoIngreso.Es_Importacion Then
                        pFechaHoy = pFechaHoy.AddDays(pTiempoProveedor.Dias_Exterior)
                    Else
                        pFechaHoy = pFechaHoy.AddDays(pTiempoProveedor.Dias_Local)
                    End If

                    If pFechaIngresadaVence < pFechaHoy Then
                        Validos_Tiempos_Aceptacion_Proveedor = False
                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
            Throw ex
        End Try

    End Function

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        Try
            cmdActualizar.Enabled = False

            If Actualizar() Then

                If Not gBeRecepcionEnc Is Nothing Then
                    '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
                    Dim mensajeAdvertencia As String = "ADVERTENCIA_202302231712: Se actualizó la recepción: " & gBeRecepcionEnc.IdRecepcionEnc & " Por el IdUsuario: " & AP.UsuarioAp.IdUsuario
                    clsLnLog_error_wms_rec.Agregar_Error(mensajeAdvertencia,
                                                         AP.UsuarioAp.IdEmpresa,
                                                         AP.IdBodega,
                                                         AP.UsuarioAp.IdUsuario,
                                                         pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)
                End If

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se actualizó la recepción.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If

                Cargar_Recepcion(True)

            End If

            cmdActualizar.Enabled = True

        Catch ex As Exception
            cmdActualizar.Enabled = True
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")

                Actualizar = Guardar()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Sub Configura_Opcion_Tipo_Rec(ByVal pBeTR As clsBeTrans_re_tr)

        Try

            DgridDetalleRec.Enabled = True

            Set_Transaccion(pBeTR)

            pTransHH = pBeTR.UsaHH

            xtrRecepcion.TabPages.Remove(tabDetalleOC)
            xtrRecepcion.TabPages.Remove(tabDetRec)
            xtrRecepcion.TabPages.Remove(tabImagenes)
            xtrRecepcion.TabPages.Remove(tabDetOp)
            xtrRecepcion.TabPages.Remove(tabDetalleRecepcion2)

            If Not pBeTR.ConRef Then

                If txtIdOrdenCompra.Text.Trim = "" Then
                    lnk.Enabled = False
                    txtIdOrdenCompra.Enabled = False
                    txtIdOrdenCompra.Text = ""
                    txtOC.Text = ""
                Else
                    txtIdOrdenCompra.Enabled = True
                    lnk.Enabled = (txtIdOrdenCompra.Text = "")
                    txtIdOrdenCompra.Enabled = (txtIdOrdenCompra.Text = "")
                    xtrRecepcion.TabPages.Add(tabDetalleOC)
                End If

            Else
                txtIdOrdenCompra.Enabled = True
                lnk.Enabled = (txtIdOrdenCompra.Text = "")
                txtIdOrdenCompra.Enabled = (txtIdOrdenCompra.Text = "")
                xtrRecepcion.TabPages.Add(tabDetalleOC)
            End If

            HabilitarTabDetalleRecepcion(pBeTR)
            xtrRecepcion.TabPages.Add(tabImagenes)
            HabilitarTabOperador(pBeTR.IdTipoTransaccion)

            DgridDetalleRec.PerformLayout()

            Application.DoEvents()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

            Throw ex
        End Try

    End Sub

    Private Sub lnkTipoT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkTipoT.LinkClicked

        Try

            Dim TR As New frmTipoTransaccion_List() With {.Modo = frmTipoTransaccion_List.pModo.Seleccion}
            TR.ShowDialog()

            If TR.DialogResult = DialogResult.OK Then

                If TR.pObj IsNot Nothing AndAlso String.IsNullOrEmpty(TR.pObj.IdTipoTransaccion) = False Then

                    If TR.pObj.IdTipoTransaccion = "MCOC00" Then
                        If txtIdOrdenCompra.Text.Trim = "" Then
                            XtraMessageBox.Show("Seleccione primero el documento de ingreso", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtIdOrdenCompra.Focus()
                            Exit Sub
                        End If
                    End If

                    txtIdTipoTR.Text = TR.pObj.IdTipoTransaccion
                    txtDescripcionTR.Text = TR.pObj.Descripcion

                    Configura_Opcion_Tipo_Rec(TR.pObj)

                    '#GT15102024: Asignar el tipo de tarea HH
                    pBeTipo_Tarea_HH = TR.pObj

                    If Not TR.pObj.UsaHH Then

                        chkRecepcionManual.Checked = True

                        '#CKFK 20210624 Se llama a la función creada por EJC para habilitar o no el stock basado en las reglas del propietario
                        Check_Reglas_Propietario_Ingreso()

                        If clsBD.Instancia.Formato_Recepcion = 2 Then
                            chkHabilitaStock.Checked = (TR.pObj.IdTipoTransaccion = "MCOC00")
                        End If

                        If TR.pObj.IdTipoTransaccion = "MCOC00" Then

                            If clsBD.Instancia.Formato_Recepcion = 2 Then
                                '#GT08022024: guardar el encabezado al recibir por BOF.
                                Guardar_Recepcion_Inmediata_BOF = True
                                Guardar_Recepcion_Inmediata()
                            End If

                            If chkDI2REC.Checked Then

                                If XtraMessageBox.Show("¿Convertir el documento de ingreso en recepción con valores por defecto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                                    Dim lDetSample As New List(Of clsBeTrans_re_det)
                                    lDetSample = clsLnTrans_re_det.DI_To_Rec(lblC.Text,
                                                                             gBeOrdenCompra.IdOrdenCompraEnc,
                                                                             AP.UsuarioAp.IdUsuario)

                                    Cargar_Detalle_Recepcion(lDetSample)

                                End If

                            End If

                        End If

                    Else

                        chkRecepcionManual.Checked = False
                        chkHabilitaStock.Checked = True

                        '#CKFK 20210624 Se llama a la función creada por EJC para habilitar o no el stock basado en las reglas del propietario
                        Check_Reglas_Propietario_Ingreso()

                        If NavConfigEnc.Interface_SAP Then
                            '#EJC20240212: Se permite habilitar stock en consecuencia del cambio de Grid a devexpress.
                            chkHabilitaStock.Checked = (TR.pObj.IdTipoTransaccion = "MCOC00")
                        End If

                    End If

                End If

                TR.Close()
                TR.Dispose()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Guardar_Recepcion_Inmediata()

        Try

            Guardar_Recepcion(False)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub HabilitarTabDetalleRecepcion(ByVal Obj As clsBeTrans_re_tr)

        Try

            If Obj.IdTipoTransaccion = "HSOC00" Or
               Obj.IdTipoTransaccion = "HSOD00" Or
               Obj.IdTipoTransaccion = "MCOC00" Or
               Obj.IdTipoTransaccion = "MCOD00" Or
               Obj.IdTipoTransaccion = "MSOC00" Or
               Obj.IdTipoTransaccion = "MSOD00" Then

                '#EJC202405090340: Utilizar el nuevo grid tab para recepción por bof.
                If clsBD.Instancia.Formato_Recepcion = 1 Then
                    xtrRecepcion.TabPages.Add(tabDetRec)
                ElseIf clsBD.Instancia.Formato_Recepcion = 2 Then
                    xtrRecepcion.TabPages.Add(tabDetalleRecepcion2)
                End If

                ProductoP.Width = 300
                Estado.Width = 130

                Dim vHabilitarDetalleRecManual As Boolean = ((Obj.IdTipoTransaccion = "MCOC00") AndAlso Not (txtIdOrdenCompra.Text = "")) Or
                   (Obj.IdTipoTransaccion = "MSOC00")

                '#EJC20210930: Si es con D.I. no habilitar el tab hasta que seleccionen la O.C.
                '#CKFK20230512 Modifiqué para el Grid el Enabled por el ReadOnly
                DgridDetalleRec.ReadOnly = Not vHabilitarDetalleRecManual
                cmdAgregarProducto.Enabled = vHabilitarDetalleRecManual
                cmdEliminarFila.Enabled = vHabilitarDetalleRecManual
                ToolEliminarFila.Enabled = vHabilitarDetalleRecManual

                If vHabilitarDetalleRecManual AndAlso txtIdOrdenCompra.Text = "" Then
                    lblStatus.Text = "Seleccione documento de ingreso para habilitar"
                Else
                    lblStatus.Text = ""
                    GrpDetalleRecepcion.Enabled = True
                    DgridDetalleRec.Enabled = True
                    cmdAgregarProducto.Enabled = True
                    cmdEliminarFila.Enabled = True
                    ToolEliminarFila.Enabled = True
                End If

            ElseIf Modo = TipoTrans.Editar Then

                '#EJC202405090340: Utilizar el nuevo grid tab para recepción por bof.
                If clsBD.Instancia.Formato_Recepcion = 1 Then
                    xtrRecepcion.TabPages.Add(tabDetRec)
                ElseIf clsBD.Instancia.Formato_Recepcion = 2 Then
                    xtrRecepcion.TabPages.Add(tabDetalleRecepcion2)
                End If

                ProductoP.Width = 300
                Estado.Width = 130

                Dim vHabilitarDetalleRecManual As Boolean = ((Obj.IdTipoTransaccion = "MCOC00") AndAlso Not (txtIdOrdenCompra.Text = ""))

                '#EJC20210930: Si es con D.I. no habilitar el tab hasta que seleccionen la O.C.
                '#CKFK20230512 Modifiqué para el Grid el Enabled por el ReadOnly
                DgridDetalleRec.ReadOnly = Not vHabilitarDetalleRecManual
                cmdAgregarProducto.Enabled = vHabilitarDetalleRecManual
                cmdEliminarFila.Enabled = vHabilitarDetalleRecManual
                ToolEliminarFila.Enabled = vHabilitarDetalleRecManual

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

                    If l.Obtener(Obj) Then

                        If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.IdTipoTransaccion) = False Then

                            txtDescripcionTR.Text = Obj.Descripcion
                            HabilitarTabOperador(Obj.IdTipoTransaccion)
                            Configura_Opcion_Tipo_Rec(Obj)

                        Else

                            SplashScreenManager.CloseForm(False)
                            XtraMessageBox.Show(String.Format("No existe Tipo Transacción con código {0}", txtIdTipoTR.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtDescripcionTR.Text = String.Empty
                            txtIdTipoTR.Focus() : txtIdTipoTR.SelectAll()

                        End If

                    Else
                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show(String.Format("No existe tipo transacción con código {0}", txtIdTipoTR.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub Set_Transaccion(ByVal pBeTransReTR As clsBeTrans_re_tr)

        Try

            If pBeTransReTR.IdTipoTransaccion = "HCOD00" Or pBeTransReTR.IdTipoTransaccion = "MCOD00" Then
                ' SI ENTRA ACÁ ES PORQUE LOS TIPOS DE TRANSACCIONES SON PEDIDO
                lnk.Text = "Pedido"
                tabDetalleOC.Text = "Detalle de pedido"
                tabDetRec.Text = "Detalle de devolución"
                tabDetalleRecepcion2.Text = "Detalle de devolución"
            Else

                Dim Ti As New clsDataContractDI.tTipoDocumentoIngreso
                Ti = gBeOrdenCompra.TipoIngreso.IdTipoIngresoOC

                lnk.Text = IIf(Ti.ToString = "NoDefinido", "Doc. Ingreso", Ti.ToString)
                tabDetalleOC.Text = "Detalle de pedido"
                tabDetRec.Text = "Detalle de recepción"
                tabDetalleRecepcion2.Text = "Detalle de recepción"

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub HabilitarTabOperador(ByVal IdTipoTransaccion As String)

        Try

            xtrRecepcion.TabPages.Remove(tabDetOp)

            If IdTipoTransaccion = "HCOC00" Or IdTipoTransaccion = "HCOD00" Or
                IdTipoTransaccion = "HSOC00" Or IdTipoTransaccion = "HSOD00" Or IdTipoTransaccion = "HHSR00" Or
                (IdTipoTransaccion = "MCOC00" AndAlso clsBD.Instancia.Formato_Recepcion = 2) Then
                xtrRecepcion.TabPages.Add(tabDetOp)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Listar_Operadores()

        Dim vIdOperadorBodega As Integer = 0
        Dim IdOperadorBodegaDefecto_edit As Integer = 0
        Dim TieneMarcaje As Boolean = False

        Try

            DsOrdenCompraRecepcionOperador.Clear()

            Dim lRow_default As DataRow = DsOrdenCompraRecepcionOperador.Data.NewRow

            If TipoTrans.Editar And DTOperadores.Rows.Count > 0 And IdOperadorBodegaDefecto_edit <> 0 Then

                For i As Integer = 0 To DTOperadores.Rows.Count - 1

                    vIdOperadorBodega = DTOperadores(i)(0)

                    TieneMarcaje = clsLnMarcaje.Tiene_Marcaje_By_Operador_Bodega_And_FechaActual(AP.IdEmpresa,
                                                                                                 AP.IdBodega,
                                                                                                 vIdOperadorBodega)

                    If vIdOperadorBodega = IdOperadorBodegaDefecto_edit Then
                        lRow_default.Item("IdOperadorBodega") = vIdOperadorBodega
                        lRow_default.Item("Operador") = DTOperadores(i)(1)
                        lRow_default.Item("Selección") = True
                        lRow_default.Item("colUsaHH") = CBool(DTOperadores(i)(2))
                        lRow_default.Item("Foto") = DTOperadores(i)(3)
                        lRow_default.Item("IngresoHH") = TieneMarcaje
                    End If

                Next

                DsOrdenCompraRecepcionOperador.Data.AddDataRow(lRow_default)

            Else

                If DTOperadores.Rows.Count > 0 Then

                    For i As Integer = 0 To DTOperadores.Rows.Count - 1

                        vIdOperadorBodega = DTOperadores(i)(0)

                        TieneMarcaje = clsLnMarcaje.Tiene_Marcaje_By_Operador_Bodega_And_FechaActual(AP.IdEmpresa,
                                                                                                     AP.IdBodega,
                                                                                                     vIdOperadorBodega)
                        Dim lRow As DataRow = DsOrdenCompraRecepcionOperador.Data.NewRow
                        lRow.Item("IdOperadorBodega") = vIdOperadorBodega
                        lRow.Item("Operador") = DTOperadores(i)(1)
                        lRow.Item("IngresoHH") = TieneMarcaje

                        If IdOperadorBodegaDefecto = 0 Then

                            If DesmarcandoOperadores Then
                                lRow.Item("Selección") = False
                            Else
                                If Not BeBodega.control_operador_ubicacion Then
                                    lRow.Item("Selección") = False
                                Else
                                    lRow.Item("Selección") = IIf(Modo = TipoTrans.Nuevo, True, False)
                                End If
                            End If

                        Else
                            If vIdOperadorBodega = IdOperadorBodegaDefecto Then
                                lRow.Item("Selección") = True
                            End If
                        End If

                        lRow.Item("colUsaHH") = CBool(DTOperadores(i)(2))
                        lRow.Item("Foto") = DTOperadores(i)(3)

                        If Modo = TipoTrans.Nuevo Then

                            If Not DesmarcandoOperadores Then

                                If IdOperadorBodegaDefecto = 0 Then

                                    '#GT02032022_1906: si no tiene control operador, visualmente esta desmarcado y pListOp debe estar vacio
                                    If BeBodega.control_operador_ubicacion Then

                                        Dim tmpIdOperadorBodega As Integer = CInt(DTOperadores(i)(0))

                                        '#GT23092025: validar que no se añada el mismo operador mas de una vez
                                        If Not pListOpe.Any(Function(x) x.IdOperadorBodega = tmpIdOperadorBodega) Then

                                            Dim ReOperador As New clsBeTrans_re_op() With {
                                                .IdOperadorBodega = tmpIdOperadorBodega,
                                                .User_agr = AP.UsuarioAp.IdUsuario,
                                                .Fec_agr = Now,
                                                .User_mod = AP.UsuarioAp.IdUsuario,
                                                .Fec_mod = Now,
                                                .IsNew = True,
                                                .UsaHH = DTOperadores(i)(2)
                                                }
                                            pListOpe.Add(ReOperador)

                                        End If

                                        'Dim Obj As New clsBeTrans_re_op() With
                                        '  {.IdOperadorBodega = DTOperadores(i)(0),
                                        '     .User_agr = AP.UsuarioAp.IdUsuario,
                                        '     .Fec_agr = Now,
                                        '     .User_mod = AP.UsuarioAp.IdUsuario,
                                        '     .Fec_mod = Now,
                                        '     .IsNew = True,
                                        '     .UsaHH = DTOperadores(i)(2)}
                                        'pListOpe.Add(Obj)

                                    End If

                                Else
                                    If vIdOperadorBodega = IdOperadorBodegaDefecto Then

                                        '#GT23092025: validar que el operador no existe en la lista para no duplicar
                                        Dim tmpReOperador = DTOperadores(i)(0)
                                        If Not pListOpe.Any(Function(x) x.IdOperadorBodega = tmpReOperador) Then

                                            Dim Obj As New clsBeTrans_re_op() With
                                                                              {.IdOperadorBodega = tmpReOperador,
                                                                              .User_agr = AP.UsuarioAp.IdUsuario,
                                                                              .Fec_agr = Now,
                                                                              .User_mod = AP.UsuarioAp.IdUsuario,
                                                                              .Fec_mod = Now,
                                                                              .IsNew = True,
                                                                              .UsaHH = DTOperadores(i)(2)}
                                            pListOpe.Add(Obj)

                                        End If


                                        'Dim Obj As New clsBeTrans_re_op() With
                                        '                                      {.IdOperadorBodega = DTOperadores(i)(0),
                                        '                                      .User_agr = AP.UsuarioAp.IdUsuario,
                                        '                                      .Fec_agr = Now,
                                        '                                      .User_mod = AP.UsuarioAp.IdUsuario,
                                        '                                      .Fec_mod = Now,
                                        '                                      .IsNew = True,
                                        '                                      .UsaHH = DTOperadores(i)(2)}
                                        'pListOpe.Add(Obj)

                                    End If
                                End If
                            End If
                        Else
                            lRow.Item("IdOperadorRec") = 0
                        End If

                        If Modo = TipoTrans.Editar Then

                            If pListOpe IsNot Nothing AndAlso pListOpe.Count > 0 Then

                                For Each Obj As clsBeTrans_re_op In pListOpe

                                    If Obj.IdOperadorBodega = CInt(DTOperadores(i)(0)) Then

                                        If IdOperadorBodegaDefecto <> 0 Then

                                            TieneMarcaje = clsLnMarcaje.Tiene_Marcaje_By_Operador_Bodega_And_FechaActual(AP.IdEmpresa,
                                                                                                                         AP.IdBodega,
                                                                                                                         DTOperadores(i)(0))

                                            If Obj.IdOperadorBodega = IdOperadorBodegaDefecto Then
                                                lRow.Item("Selección") = True
                                                lRow.Item("IdOperadorRec") = Obj.IdOperadorRec
                                                lRow.Item("colUsaHH") = CBool(DTOperadores(i)(2))
                                                lRow_default.Item("IngresoHH") = TieneMarcaje
                                            End If

                                        Else
                                            lRow.Item("Selección") = True
                                            lRow.Item("IdOperadorRec") = Obj.IdOperadorRec
                                            lRow.Item("colUsaHH") = CBool(DTOperadores(i)(2))
                                            lRow_default.Item("IngresoHH") = TieneMarcaje
                                        End If

                                    End If

                                Next

                            End If

                        End If

                        DsOrdenCompraRecepcionOperador.Data.AddDataRow(lRow)

                    Next

                End If

            End If

            Dim ritem As New RepositoryItemPictureEdit()
            ritem.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
            ritem.BestFitWidth = 200
            DGridOperadores.RepositoryItems.Add(ritem)

            If Not ritem Is Nothing Then
                If Not GrdOperadorBobega.Columns("foto") Is Nothing Then
                    GrdOperadorBobega.Columns("foto").ColumnEdit = ritem
                End If
            End If

            If GrdOperadorBobega.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GrdOperadorBobega.RowCount)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Valida_Operadores()

        Try

            '#CKFK20220703 Cambié el query para listar los operadores            
            DTOperadores = clsLnOperador_bodega.Get_All_By_IdBodega_For_Tarea_DT(cmbBodega.EditValue, clsDataContractDI.tTipoTarea.RECE)

            'ejc_18092016
            If gBeRecepcionEnc IsNot Nothing Then
                pListOpe.Clear()
                pListOpe = clsLnTrans_re_op.Get_All_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc).ToList
            Else
                pListOpe = New List(Of clsBeTrans_re_op)()
            End If

            Listar_Operadores()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub EliminarFila(e As KeyEventArgs)

        Try

            If DgridDetalleRec.CurrentRow IsNot Nothing AndAlso DgridDetalleRec.CurrentRow.Index > -1 Then

                If DgridDetalleRec.CurrentRow.Cells("ProductoP").Value IsNot DBNull.Value AndAlso DgridDetalleRec.CurrentRow.Cells("ProductoP").Value IsNot Nothing Then

                    If XtraMessageBox.Show(String.Format("¿Desea eliminar el Producto {0}?", DgridDetalleRec.CurrentRow.Cells("ProductoP").Value) _
                                          , Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then


                        Dim selectedRow As DataGridViewRow = DgridDetalleRec.SelectedRows(0)

                        ' Verifica si la fila es una fila nueva no confirmada
                        If selectedRow.IsNewRow Then
                            DgridDetalleRec.CancelEdit()
                            XtraMessageBox.Show("No se puede eliminar la fila sin confirmar", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else

                            Dim lIndexF As Integer = DgridDetalleRec.CurrentRow.Index
                            Dim lIdRecepcionEnc As Integer = DgridDetalleRec.CurrentRow.Cells("IdRecepcionEnc").Value
                            Dim lIdRecepcionDet As Integer = DgridDetalleRec.CurrentRow.Cells("IdRecepcionDet").Value
                            Dim lIdProducto As Integer = DgridDetalleRec.CurrentRow.Cells("IdProductoP").Value
                            Dim lIndex As Integer = -1

                            If lIdRecepcionEnc > 0 AndAlso lIdRecepcionDet > 0 Then
                                lIndex = lBeTransRecDet.FindIndex(Function(p) p.IdRecepcionEnc = lIdRecepcionEnc And p.IdRecepcionDet = lIdRecepcionDet And p.IdProductoBodega = lIdProducto)
                            Else
                                lIndex = lBeTransRecDet.FindIndex(Function(p) p.IdProductoBodega = lIdProducto)
                            End If

                            If lIndex > -1 Then

                                If Not e Is Nothing Then e.Handled = True

                                pIdOrdenCompraEnc = 0
                                If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then
                                    pIdOrdenCompraEnc = gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc
                                End If

                                If gBeRecepcionEnc.Estado <> "Cerrado" Then
                                    clsLnTrans_re_det.Delete(pIdOrdenCompraEnc, lIdRecepcionEnc, lIdRecepcionDet)
                                End If

                                lBeTransRecDet.RemoveAt(lIndex)

                                '#EJC20240212: ERRORSOTE DEJADO ADREDE PARA CORREGIR EL ELIMINAR COMPLETO.
                                DgridDetalleRec.Rows.RemoveAt(lIndexF)

                            End If

                            Dim listaStock As New List(Of clsBeStock_rec)
                            listaStock = pListBeStockRec.FindAll(Function(p) p.IdProductoBodega = lIdProducto And p.ProductoValidado = False And p.IdRecepcionDet = lIdRecepcionDet)

                            For Each BeStockRec In listaStock
                                pListBeStockRec.Remove(BeStockRec)
                            Next

                            DgridDetalleRec.Rows.RemoveAt(lIndexF)

                        End If

                    Else

                        Try
                            If Not e Is Nothing Then
                                e.SuppressKeyPress = True
                            End If
                        Catch ex As Exception
                            '#EJC2018012: Al borrar una letra dentro de una celda del grid, da excepción
                        End Try

                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub ValidaCantidad()

        Try

            For i As Integer = 0 To DgridDetalleRec.Rows.Count - 1
                If DgridDetalleRec.Rows(i).Cells("ProductoP").Value IsNot Nothing Then
                    If DgridDetalleRec.Rows(i).Cells("CantidadP").Value Is Nothing Then
                        Throw New Exception(String.Format("Ingrese cantidad en fila {0}", i + 1))
                    End If
                End If
            Next

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

            Throw ex
        End Try

    End Sub

    Private Sub txtIdOrdenCompra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdOrdenCompra.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdOrdenCompra.Text.Length = 1 Then
                txtOC.Text = String.Empty
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

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

    Private Sub cmdFinalizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdFinalizar.ItemClick
        cmdFinalizar.Enabled = False
        Finalizar(lBeTransRecDet)
        cmdFinalizar.Enabled = True
    End Sub

    Public Enum tIndicadorDiferenciaRec
        Producto_Faltante = 0
        Producto_Sobrante = 1
    End Enum

    ''' <summary>
    ''' Valida la cantidad de la OC Vrs. Cantidad de la recepción (En UMBas), para actualizar el estatus de BackOrder en la O.C.
    ''' #EJC20170721
    ''' </summary>
    ''' <param name="pListRecepcionDetalle"></param>
    ''' <returns></returns>
    Private Function Detalle_Tiene_Diferencia_Vrs_OC(ByVal pListRecepcionDetalle As List(Of clsBeTrans_re_det)) As Boolean

        Detalle_Tiene_Diferencia_Vrs_OC = False

        Try

            Dim vPresRec As New clsBeProducto_Presentacion
            Dim vPresOC As New clsBeProducto_Presentacion
            Dim vCantidadOCUMBas As Double = 0
            Dim vCantidadRecUMBas As Double = 0
            Dim vCantidadRecAntUMBas As Double = 0
            Dim lDetRec As New List(Of clsBeTrans_re_det)
            Dim lDetRecAnteriores As New List(Of clsBeTrans_re_det)
            Dim lDetRecAnterioresFiltro As New List(Of clsBeTrans_re_det)
            Dim lDetRecAnterioresFiltroFinal As New List(Of clsBeTrans_re_det)
            Dim lDetRecAnteriorResult As New List(Of clsBeTrans_re_det)
            Dim vNoLinea As Integer = 0

            If Not BeTransOcEnc Is Nothing Then

                lDetRecAnteriores = clsLnTrans_re_det.Get_All_By_Orden_Compra_Filtro(BeTransOcEnc.IdOrdenCompraEnc, gBeRecepcionEnc.IdRecepcionEnc)

                For Each ProdInOc As clsBeTrans_oc_det In pListObjOrdeCompraDet

                    vPresOC = ProdInOc.Presentacion
                    vNoLinea = ProdInOc.No_Linea

                    If ProdInOc.Presentacion.IdPresentacion = 0 Then
                        vCantidadOCUMBas = ProdInOc.Cantidad
                    Else
                        vCantidadOCUMBas = ProdInOc.Cantidad * vPresOC.Factor
                    End If

                    If Not lDetRecAnteriores Is Nothing Then

                        If lDetRecAnteriores.Count > 0 Then

                            If ProdInOc.IdPresentacion = 0 Then
                                lDetRecAnterioresFiltro = lDetRecAnteriores.FindAll(Function(b) b.IdProductoBodega = ProdInOc.IdProductoBodega)
                            Else
                                lDetRecAnterioresFiltro = lDetRecAnteriores.FindAll(Function(b) b.IdProductoBodega = ProdInOc.IdProductoBodega AndAlso b.IdPresentacion = ProdInOc.IdPresentacion)
                            End If

                            lDetRecAnterioresFiltroFinal.Clear()

                            For Each ProdInDetRec As clsBeTrans_re_det In lDetRecAnterioresFiltro

                                vPresRec = ProdInDetRec.Presentacion

                                If Not vPresRec Is Nothing AndAlso vPresRec.IdPresentacion <> 0 Then
                                    vPresRec = clsLnProducto_presentacion.GetSingle(vPresOC.IdPresentacion)
                                    vCantidadRecUMBas += ProdInOc.Cantidad * vPresOC.Factor
                                Else
                                    vCantidadRecUMBas += ProdInDetRec.cantidad_recibida
                                End If

                                ProdInDetRec.cantidad_recibida = vCantidadRecUMBas

                                lDetRecAnterioresFiltroFinal.Add(ProdInDetRec)

                            Next ProdInDetRec

                        End If

                    End If

                    vCantidadRecUMBas = 0

                    If ProdInOc.IdPresentacion = 0 Then
                        lDetRec = pListRecepcionDetalle.FindAll(Function(b) b.IdProductoBodega = ProdInOc.IdProductoBodega)
                    Else
                        lDetRec = pListRecepcionDetalle.FindAll(Function(b) b.IdProductoBodega = ProdInOc.IdProductoBodega AndAlso b.IdPresentacion = ProdInOc.IdPresentacion)
                    End If

                    If Not lDetRec Is Nothing Then

                        For Each ProdInDetRec As clsBeTrans_re_det In lDetRec.Where(Function(x) x.No_Linea = vNoLinea)

                            vPresRec = ProdInDetRec.Presentacion

                            If Not vPresRec Is Nothing AndAlso vPresRec.IdPresentacion <> 0 Then

                                vPresRec = clsLnProducto_presentacion.GetSingle(vPresRec.IdPresentacion)

                                If vPresRec.EsPallet Then
                                    vCantidadRecUMBas += ProdInDetRec.cantidad_recibida * (vPresRec.Factor * vPresRec.CajasPorCama * vPresRec.CamasPorTarima)
                                Else
                                    vCantidadRecUMBas += ProdInDetRec.cantidad_recibida * vPresRec.Factor
                                End If

                            Else
                                vCantidadRecUMBas += ProdInDetRec.cantidad_recibida
                            End If

                        Next ProdInDetRec

                        If Not lDetRecAnterioresFiltroFinal Is Nothing Then
                            vCantidadRecAntUMBas += lDetRecAnterioresFiltroFinal.FindAll(Function(y) y.IdProductoBodega = ProdInOc.IdProductoBodega).Sum(Function(z) z.cantidad_recibida)
                        End If

                        Dim vDiferencia As Double = (vCantidadRecUMBas + vCantidadRecAntUMBas) - vCantidadOCUMBas

                        If vDiferencia < 0 Then
                            'Se recibió menos producto de lo esperado.
                            Detalle_Tiene_Diferencia_Vrs_OC = True
                            Exit Function
                        ElseIf vDiferencia > 0 Then
                            'Se recibió más producto de lo esperado.
                            Detalle_Tiene_Diferencia_Vrs_OC = True
                            Exit Function
                        Else
                            vCantidadRecUMBas = 0
                        End If

                    Else
                        'No se recepcionó nada de ese código de producto
                        Detalle_Tiene_Diferencia_Vrs_OC = True
                        Exit Function
                    End If

                Next ProdInOc

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    '#CKFK 20180625 08:30 PM Agregué el campo habilitaStock, para permitir que el inventario se vaya actualizando a medida que se va recepcionando
    Private Function Finaliza_Recepcion(ByVal backOrder As Boolean, ByVal habilitaStock As Boolean) As Boolean

        Finaliza_Recepcion = False

        Try

            Dim vIdOrdenCompra As Integer

            If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then
                vIdOrdenCompra = gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc
            Else
                vIdOrdenCompra = 0

            End If

            clsLnTrans_re_enc.Finalizar_Recepcion(gBeRecepcionEnc,
                                                  backOrder,
                                                  vIdOrdenCompra,
                                                  gBeRecepcionEnc.IdRecepcionEnc,
                                                  AP.IdEmpresa,
                                                  cmbBodega.EditValue,
                                                  AP.UsuarioAp.IdUsuario,
                                                  lBeTransRecDet,
                                                  habilitaStock)

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            Dim msjAdvertencia As String = "ADVERTENCIA_202302231712B: Se finalizó la recepción desde BOF: " & gBeRecepcionEnc.IdRecepcionEnc & " Por el IdUsuario: " & AP.UsuarioAp.IdUsuario
            clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

            Finaliza_Recepcion = True

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("Recepción {0} finalizada correctamente.", gBeRecepcionEnc.IdRecepcionEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Function

    Private Function Finalizar(ByVal pListRecepcionDetalle As List(Of clsBeTrans_re_det)) As Boolean

        Finalizar = False

        Dim vRecargoObjetoRecepcion As Boolean = False

        Try

            '#EJC202302230117AM: Resulta, que si no se recarga el objeto antes de preguntar se corre el riesgo de que algún operador
            '(No muy inteligentemente) le de finalizar a la recepción cuando aún no se ha finalizado en la hH.
            'Esto podría provocar que se mande a finalizar con la lista de BOF vacía y entraría en una condición donde volvería a insertar
            '(a pesar de que la HH ya lo insertó) el inventario en stock generando duplicidad en stock y movimiento, SPTM.
            If gBeRecepcionEnc.Habilitar_Stock AndAlso
                (gBeRecepcionEnc.IdTipoTransaccion = "HCOC00" OrElse
                gBeRecepcionEnc.IdTipoTransaccion = "HSOC00" OrElse
                gBeRecepcionEnc.IdTipoTransaccion = "MCOC00") Then
                gBeRecepcionEnc = clsLnTrans_re_enc.GetSingle(gBeRecepcionEnc.IdRecepcionEnc)
                pListRecepcionDetalle = gBeRecepcionEnc.Detalle

                '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
                Dim msjAdvertencia As String = "ADVERTENCIA_202302230123: Se realizó recarga del objeto para la recepción: " & gBeRecepcionEnc.IdRecepcionEnc & " el tipo de transacción es: " & gBeRecepcionEnc.IdTipoTransaccion
                clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

                vRecargoObjetoRecepcion = True
            End If

            SplashScreenManager.CloseForm(False)

            If pListRecepcionDetalle.Count = 0 Then

                If Not vRecargoObjetoRecepcion Then

                    '#GT08022024: si es recepcion por bof y guardamos el encabezado automatico, no preguntar por detalle.
                    If Not Guardar_Recepcion_Inmediata_BOF Then

                        If XtraMessageBox.Show("La recepción no tiene detalle, ¿Actualizar recepción de HH?",
                                       Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            Recargar_Objeto_Recepcion()

                            If Not gBeRecepcionEnc.Detalle Is Nothing Then
                                If gBeRecepcionEnc.Detalle.Count = 0 Then
                                    If XtraMessageBox.Show("La recepción no tiene detalle de HH, ¿Salir de la recepción?",
                                           Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                        Close()
                                    End If
                                End If
                            Else
                                If XtraMessageBox.Show("La recepción no tiene detalle de HH, ¿Salir de la recepción?",
                                           Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                    Close()
                                End If
                            End If

                            If Not gBeRecepcionEnc.Detalle Is Nothing Then
                                If gBeRecepcionEnc.Detalle.Count > 0 Then

                                    '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
                                    Dim msjAdvertencia As String = "ADVERTENCIA_202302230139A: Se realizó llamada recursiva del objeto para la recepción: " & gBeRecepcionEnc.IdRecepcionEnc & " el tipo de transacción es: " & gBeRecepcionEnc.IdTipoTransaccion
                                    clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

                                    Finalizar(gBeRecepcionEnc.Detalle)
                                End If
                            End If

                        End If

                    End If

                Else
                    XtraMessageBox.Show("La recepción no tiene detalle, No se puede finalizar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            Else

                If clsBD.Instancia.Formato_Recepcion = 2 Then

                    If BeTipoDocumento.IdTipoIngresoOC <> clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Inventario_Inicial Then

                        If Not BeTipoDocumento.Es_devolucion Then
                            If txtIdPiloto.EditValue = "0" Then
                                XtraMessageBox.Show("Debe ingresar el piloto no se puede finalizar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Function
                            End If

                            If gBeRecepcionEnc.DetalleFacturas.Count = 0 Then
                                XtraMessageBox.Show("Debe ingresar la factura asociada a la recepción, no se puede finalizar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Function
                            End If

                        End If

                        If pListOpe.Count = 0 Then
                            XtraMessageBox.Show("Debe ingresar el operador encargado de la recepción, no se puede finalizar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Function
                        End If

                    End If

                End If

                Dim BackOrder As Boolean = False
                Dim lIdOrdenCompra As Integer = 0

                If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then
                    lIdOrdenCompra = gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc
                    BackOrder = Detalle_Tiene_Diferencia_Vrs_OC(pListRecepcionDetalle)
                End If

                If Not BackOrder Then
                    If XtraMessageBox.Show("¿La recepción no tiene diferencia, finalizar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        Exit Function
                    End If
                Else

                    If XtraMessageBox.Show("¿Finalizar la Recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        Exit Function
                    Else
                        If XtraMessageBox.Show("¿Dejar en Backorder?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                            BackOrder = False
                        End If
                    End If
                End If

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Finalizando...")

                Finalizar = Finaliza_Recepcion(BackOrder, gBeRecepcionEnc.Habilitar_Stock)

                If Finalizar Then

                    If AP.IdConfiguracionInterface <> -1 Then

                        If Not BeTipoDocumento Is Nothing Then

                            If Not BeTipoDocumento.Marcar_Registros_Enviados_MI3 Then

                                Ejecutar_Interface("5-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me)

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Shared Sub Restablecer(ByVal pObj As clsBeStock_rec)

        pObj.ProductoValidado = False

    End Sub

    Private Sub frmRecepcion_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F3 Then
            cmdVerParametros_Click(Nothing, Nothing)
        ElseIf DgridDetalleRec2.IsFocused AndAlso e.KeyCode = Keys.Delete Then
            Eliminar_Fila(Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            '#EJC20240326: Validar al salir.
            Dim vMensaje As String = ""

            If TipoTrans.Nuevo Then
                vMensaje = "¿Al parecer no se ha guardado la recepción, salir de todas formas?"
            Else
                vMensaje = "¿Salir de la recepción?"
            End If

            If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Close()
            End If

        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            cmdFinalizar_ItemClick(Nothing, Nothing)
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            chkDI2REC.Checked = Not chkDI2REC.Checked
        ElseIf e.Control AndAlso e.KeyCode = Keys.Delete Then
            If txtIdTipoTR.Text = "MSOC00" Then
                Dim currentRow As Integer = DgridDetalleRec.CurrentCell.RowIndex
                Dim currentColumn As Integer = DgridDetalleRec.CurrentCell.ColumnIndex

                ' Verificar si la celda actual pertenece a la columna de presentaciones
                If DgridDetalleRec.Columns(currentColumn).Name = "PresentacionP" Then

                    ' Realizar la acción deseada, por ejemplo, borrar el contenido de la celda
                    DgridDetalleRec.Rows(currentRow).Cells(currentColumn).Value = -1

                    ' Evitar que el DataGridView maneje el evento por defecto
                    e.Handled = True
                End If
            End If
        End If

    End Sub

    Private Sub cmdPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPrint.ItemClick

        cmdPrint.Enabled = False

        If XtraMessageBox.Show(String.Format("¿Imprimir recepción consolidada?"), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ImprimirRecepcion(True)
        Else
            ImprimirRecepcion(False)
        End If

        cmdPrint.Enabled = True

    End Sub

    Private Sub ImprimirRecepcion(ByVal Cosolida As Boolean)

        Cursor = Cursors.WaitCursor

        Try

            If gBeRecepcionEnc.OrdenCompraRec Is Nothing Then
                Genera_Reporte_SinOC()
            Else

                If gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc <> 0 Then
                    If Cosolida Then
                        Genera_Reporte_ConOCConsolidada()
                    Else
                        Genera_Reporte_ConOC()
                    End If
                Else
                    Genera_Reporte_SinOC()
                End If

            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Genera_Reporte_ConOC()

        Try

            Dim Rep As New rptRepcionOC
            Dim DT As New DataTable
            Dim DTs As New DataTable
            Dim CantidadTotal As Double = 0
            Dim CantidadRestante As Double = 0
            Dim RestaCantidades As Double = 0
            Dim NewRow As DataRow = DTs.NewRow()
            Dim Tipo As String = "Detallado"

            DTs.Columns.Add("IdRecepcionEnc", GetType(Integer))
            DTs.Columns.Add("IdRecepcionDet", GetType(Integer))
            DTs.Columns.Add("IdPropietarioBodega", GetType(Integer))
            DTs.Columns.Add("Propietario", GetType(String))
            DTs.Columns.Add("Fecha_recepcion", GetType(Date))
            DTs.Columns.Add("hora_ini_pc", GetType(DateTime))
            DTs.Columns.Add("hora_fin_pc", GetType(DateTime))
            DTs.Columns.Add("TipoTrans", GetType(String))
            DTs.Columns.Add("No_Linea", GetType(Integer))
            DTs.Columns.Add("codigo", GetType(String))
            DTs.Columns.Add("codigo_barra", GetType(String))
            DTs.Columns.Add("Producto", GetType(String))
            DTs.Columns.Add("CantidadRecibida", GetType(Double))
            DTs.Columns.Add("fecha_ingreso", GetType(Date))
            DTs.Columns.Add("lote", GetType(String))
            DTs.Columns.Add("fecha_vence", GetType(Date))
            DTs.Columns.Add("EstadoProducto", GetType(String))
            DTs.Columns.Add("Presentacion", GetType(String))
            DTs.Columns.Add("EstadoRec", GetType(String))
            DTs.Columns.Add("Unidad_Medida", GetType(String))
            DTs.Columns.Add("cantidad", GetType(Double))
            DTs.Columns.Add("IdOrdenCompraEnc", GetType(Integer))
            DTs.Columns.Add("IdRecepcionOc", GetType(Integer))
            DTs.Columns.Add("no_docto", GetType(String))
            DTs.Columns.Add("Id_Proveedor", GetType(Integer))
            DTs.Columns.Add("Proveedor", GetType(String))
            DTs.Columns.Add("IdProductoBodega", GetType(Integer))
            DTs.Columns.Add("IdProveedorBodega", GetType(Integer))
            DTs.Columns.Add("Referencia", GetType(String))
            DTs.Columns.Add("NombrePiloto", GetType(String))
            DTs.Columns.Add("placa", GetType(String))
            DTs.Columns.Add("marca", GetType(String))
            DTs.Columns.Add("firma_piloto", GetType(Image))
            DTs.Columns.Add("Operador", GetType(String))
            DTs.Columns.Add("No_Marchamo", GetType(String))
            DTs.Columns.Add("Diferencia", GetType(String))
            DTs.Columns.Add("lic_plate", GetType(String))

            DT = clsLnTrans_re_enc.Get_Reporte_ConOC(gBeRecepcionEnc.IdRecepcionEnc)

            If DT.Rows.Count > 0 Then

                For Each row As DataRow In DT.Rows

                    NewRow = DTs.NewRow

                    CantidadTotal = IIf(row("Cantidad") IsNot DBNull.Value, row("Cantidad"), 0)
                    CantidadRestante = row("CantidadRecibida")

                    RestaCantidades = CantidadTotal - CantidadRestante

                    If RestaCantidades < 0 Then
                        CantidadTotal = RestaCantidades
                    Else
                        CantidadTotal -= RestaCantidades
                    End If

                    NewRow("IdRecepcionEnc") = row("IdRecepcionEnc")
                    NewRow("IdRecepcionDet") = row("IdRecepcionDet")
                    NewRow("IdPropietarioBodega") = row("IdPropietarioBodega")
                    NewRow("Propietario") = row("Propietario")
                    NewRow("Fecha_recepcion") = row("Fecha_recepcion")
                    NewRow("hora_ini_pc") = row("hora_ini_pc")
                    NewRow("hora_fin_pc") = row("hora_fin_pc")
                    NewRow("TipoTrans") = row("TipoTrans")

                    Dim query =
                    From c In DTs.AsEnumerable()
                    Where c.Field(Of Integer)("No_Linea") = (row("No_Linea"))
                    Select New With {.id = c.Field(Of Integer)("No_Linea")}

                    If query.Count >= 1 Then
                        NewRow("No_Linea") = row("No_Linea")
                        NewRow("codigo") = row("codigo")
                        NewRow("codigo_barra") = row("codigo_barra")
                        NewRow("Producto") = row("Producto")
                        NewRow("CantidadRecibida") = row("CantidadRecibida")
                        NewRow("fecha_ingreso") = row("fecha_ingreso")
                        NewRow("EstadoProducto") = row("EstadoProducto")
                        NewRow("fecha_vence") = row("fecha_vence")
                        NewRow("Unidad_Medida") = row("Unidad_Medida")
                    Else
                        NewRow("No_Linea") = row("No_Linea")
                        NewRow("codigo") = row("codigo")
                        NewRow("codigo_barra") = row("codigo_barra")
                        NewRow("Producto") = row("Producto")
                        NewRow("CantidadRecibida") = row("CantidadRecibida")
                        NewRow("fecha_ingreso") = row("fecha_ingreso")
                        NewRow("lote") = row("lote")
                        NewRow("fecha_vence") = row("fecha_vence")
                        NewRow("EstadoProducto") = row("EstadoProducto")
                        NewRow("Presentacion") = row("Presentacion")
                        NewRow("EstadoRec") = row("EstadoRec")
                        NewRow("Unidad_Medida") = row("Unidad_Medida")
                        NewRow("cantidad") = row("cantidad")
                        NewRow("lic_plate") = row("lic_plate")

                    End If

                    NewRow("IdOrdenCompraEnc") = row("IdOrdenCompraEnc")
                    NewRow("IdRecepcionOc") = row("IdRecepcionOc")
                    NewRow("no_docto") = row("no_docto")
                    NewRow("Id_Proveedor") = row("Id_Proveedor")
                    NewRow("Proveedor") = row("Proveedor")
                    NewRow("IdProductoBodega") = row("IdProductoBodega")
                    NewRow("IdProveedorBodega") = row("IdProveedorBodega")
                    NewRow("Referencia") = row("Referencia")
                    NewRow("NombrePiloto") = row("NombrePiloto")
                    NewRow("placa") = row("placa")
                    NewRow("marca") = row("marca")
                    If row("firma_piloto") IsNot DBNull.Value Then
                        NewRow("firma_piloto") = clsPublic.ByteArrayToImage(row("firma_piloto"))
                    End If
                    NewRow("Operador") = row("Operador")
                    NewRow("No_Marchamo") = row("No_Marchamo")

                    If row("CantidadRecibida") = CantidadTotal Then
                        NewRow("Diferencia") = "Igual"
                    ElseIf row("CantidadRecibida") < CantidadTotal Then
                        NewRow("Diferencia") = "Menor"
                    ElseIf row("CantidadRecibida") > CantidadTotal Then
                        NewRow("Diferencia") = "Mayor"
                    End If

                    DTs.Rows.Add(NewRow)

                Next

                Rep.DataSource = DTs
                Rep.DataMember = "Result"
                Rep.Parameters("Empresa").Value = AP.NomEmpresa
                Rep.Parameters("Empresa").Visible = False
                Rep.Parameters("Bodega").Value = AP.NomBodega
                Rep.Parameters("Bodega").Visible = False
                Rep.Parameters("Tipo").Value = Tipo
                Rep.Parameters("Tipo").Visible = False
                Rep.Parameters("Factura").Value = clsLnTrans_re_fact.Get_Cadena_Factura_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc)
                Rep.Parameters("Factura").Visible = True

                Rep.RequestParameters = False
                Rep.XrLogo.Image = clsPublic.ByteArrayToImage(clsLnEmpresa.GetImagen(AP.IdEmpresa))
                Rep.ShowPreviewDialog()

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al generar documento de recepción: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Genera_Reporte_ConOCConsolidada()

        Try

            Dim Rep As New rptRepcionOC
            Dim DT As New DataTable
            Dim DTs As New DataTable
            Dim CantidadTotal As Double = 0
            Dim CantidadRestante As Double = 0
            Dim RestaCantidades As Double = 0
            Dim NewRow As DataRow = DTs.NewRow()
            Dim Tipo As String = "Consolidado"


            DTs.Columns.Add("IdRecepcionEnc", GetType(Integer))
            DTs.Columns.Add("IdRecepcionDet", GetType(Integer))
            DTs.Columns.Add("IdPropietarioBodega", GetType(Integer))
            DTs.Columns.Add("Propietario", GetType(String))
            DTs.Columns.Add("Fecha_recepcion", GetType(Date))
            DTs.Columns.Add("hora_ini_pc", GetType(DateTime))
            DTs.Columns.Add("hora_fin_pc", GetType(DateTime))
            DTs.Columns.Add("TipoTrans", GetType(String))
            DTs.Columns.Add("No_Linea", GetType(Integer))
            DTs.Columns.Add("codigo", GetType(String))
            DTs.Columns.Add("codigo_barra", GetType(String))
            DTs.Columns.Add("Producto", GetType(String))
            DTs.Columns.Add("CantidadRecibida", GetType(Double))
            DTs.Columns.Add("fecha_ingreso", GetType(Date))
            DTs.Columns.Add("lote", GetType(String))
            DTs.Columns.Add("fecha_vence", GetType(Date))
            DTs.Columns.Add("EstadoProducto", GetType(String))
            DTs.Columns.Add("Presentacion", GetType(String))
            DTs.Columns.Add("EstadoRec", GetType(String))
            DTs.Columns.Add("Unidad_Medida", GetType(String))
            DTs.Columns.Add("cantidad", GetType(Double))
            DTs.Columns.Add("IdOrdenCompraEnc", GetType(Integer))
            DTs.Columns.Add("IdRecepcionOc", GetType(Integer))
            DTs.Columns.Add("no_docto", GetType(String))
            DTs.Columns.Add("Id_Proveedor", GetType(Integer))
            DTs.Columns.Add("Proveedor", GetType(String))
            DTs.Columns.Add("IdProductoBodega", GetType(Integer))
            DTs.Columns.Add("IdProveedorBodega", GetType(Integer))
            DTs.Columns.Add("Referencia", GetType(String))
            DTs.Columns.Add("NombrePiloto", GetType(String))
            DTs.Columns.Add("placa", GetType(String))
            DTs.Columns.Add("marca", GetType(String))
            DTs.Columns.Add("firma_piloto", GetType(Image))
            DTs.Columns.Add("Operador", GetType(String))
            DTs.Columns.Add("No_Marchamo", GetType(String))
            DTs.Columns.Add("Diferencia", GetType(String))


            DT = clsLnTrans_re_enc.Get_Reporte_Con_OC_Consolidada(gBeRecepcionEnc.IdRecepcionEnc)

            If DT.Rows.Count > 0 Then

                For Each row As DataRow In DT.Rows

                    NewRow = DTs.NewRow

                    CantidadTotal = IIf(row("Cantidad") IsNot DBNull.Value, row("Cantidad"), 0)
                    CantidadRestante = row("CantidadRecibida")

                    RestaCantidades = CantidadTotal - CantidadRestante

                    If RestaCantidades < 0 Then
                        CantidadTotal = RestaCantidades
                    Else
                        CantidadTotal -= RestaCantidades
                    End If

                    NewRow("IdRecepcionEnc") = row("IdRecepcionEnc")
                    NewRow("IdPropietarioBodega") = row("IdPropietarioBodega")
                    NewRow("Propietario") = row("Propietario")
                    NewRow("Fecha_recepcion") = row("Fecha_recepcion")
                    NewRow("hora_ini_pc") = row("hora_ini_pc")
                    NewRow("hora_fin_pc") = row("hora_fin_pc")
                    NewRow("TipoTrans") = row("TipoTrans")

                    Dim query =
                    From c In DTs.AsEnumerable()
                    Where c.Field(Of Integer)("No_Linea") = (row("No_Linea"))
                    Select New With {.id = c.Field(Of Integer)("No_Linea")}

                    If query.Count >= 1 Then
                        NewRow("No_Linea") = row("No_Linea")
                    Else
                        NewRow("No_Linea") = row("No_Linea")
                        NewRow("cantidad") = row("cantidad")
                    End If

                    NewRow("codigo") = row("codigo")
                    NewRow("codigo_barra") = row("codigo_barra")
                    NewRow("Producto") = row("Producto")
                    NewRow("CantidadRecibida") = row("CantidadRecibida")
                    NewRow("fecha_ingreso") = row("fecha_ingreso")
                    NewRow("EstadoProducto") = row("EstadoProducto")
                    NewRow("lote") = row("lote")
                    NewRow("fecha_vence") = row("fecha_vence")
                    NewRow("Presentacion") = row("Presentacion")
                    NewRow("EstadoRec") = row("EstadoRec")
                    NewRow("Unidad_Medida") = row("Unidad_Medida")
                    NewRow("IdOrdenCompraEnc") = row("IdOrdenCompraEnc")
                    NewRow("IdRecepcionOc") = row("IdRecepcionOc")
                    NewRow("no_docto") = row("no_docto")
                    NewRow("Id_Proveedor") = row("Id_Proveedor")
                    NewRow("Proveedor") = row("Proveedor")
                    NewRow("IdProductoBodega") = row("IdProductoBodega")
                    NewRow("IdProveedorBodega") = row("IdProveedorBodega")
                    NewRow("Referencia") = row("Referencia")
                    NewRow("NombrePiloto") = row("NombrePiloto")
                    NewRow("placa") = row("placa")
                    NewRow("marca") = row("marca")
                    If row("firma_piloto") IsNot DBNull.Value Then
                        NewRow("firma_piloto") = clsPublic.ByteArrayToImage(row("firma_piloto"))
                    End If
                    NewRow("Operador") = row("Operador")
                    NewRow("No_Marchamo") = row("No_Marchamo")

                    If row("CantidadRecibida") = CantidadTotal Then
                        NewRow("Diferencia") = "Igual"
                    ElseIf row("CantidadRecibida") < CantidadTotal Then
                        NewRow("Diferencia") = "Menor"
                    ElseIf row("CantidadRecibida") > CantidadTotal Then
                        NewRow("Diferencia") = "Mayor"
                    End If

                    DTs.Rows.Add(NewRow)

                Next

                Rep.DataSource = DTs
                Rep.DataMember = "Result"
                Rep.Parameters("Empresa").Value = AP.NomEmpresa
                Rep.Parameters("Empresa").Visible = False
                Rep.Parameters("Bodega").Value = AP.NomBodega
                Rep.Parameters("Bodega").Visible = False
                Rep.Parameters("Tipo").Value = Tipo
                Rep.Parameters("Tipo").Visible = False
                Rep.Parameters("Factura").Value = clsLnTrans_re_fact.Get_Cadena_Factura_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc)
                Rep.Parameters("Factura").Visible = True
                Rep.RequestParameters = False
                Rep.XrLogo.Image = clsPublic.ByteArrayToImage(clsLnEmpresa.GetImagen(AP.IdEmpresa))
                Rep.ShowPreviewDialog()

                SplashScreenManager.CloseForm(False)

            Else

                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("No se ha recepcionado ningún producto de esta tarea de recepción",
                                        Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al generar documento de recepción: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Genera_Reporte_SinOC()

        Try

            Dim Rep As New rptRecepcionSinOc
            Rep.DataSource = clsLnTrans_re_enc.Get_Reporte_SinOC(gBeRecepcionEnc.IdRecepcionEnc)
            Rep.DataMember = "Result"
            Rep.Parameters("Empresa").Value = AP.NomEmpresa
            Rep.Parameters("Empresa").Visible = False
            Rep.Parameters("Bodega").Value = AP.NomBodega
            Rep.Parameters("Bodega").Visible = False
            Rep.Parameters("Factura").Value = clsLnTrans_re_fact.Get_Cadena_Factura_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc)
            Rep.Parameters("Factura").Visible = True
            Rep.RequestParameters = False
            Rep.XrLogo.Image = clsPublic.ByteArrayToImage(clsLnEmpresa.GetImagen(AP.IdEmpresa))
            Rep.ShowPreviewDialog()

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al generar documento de recepción: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function ValidaFechaVencimiento(ByVal pFechaIngresada As Date, ByVal pNombreProducto As String) As Boolean

        ValidaFechaVencimiento = False

        Try

            Dim FHoy As Date = Now.Date

            If pFechaIngresada <= FHoy Then

                SplashScreenManager.CloseForm(False)

                If XtraMessageBox.Show(String.Format("La fecha de vencimiento del producto {0} es igual o menor a la fecha de hoy. ¿Ingresar producto vencido?", pNombreProducto), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    ValidaFechaVencimiento = True
                End If

            Else
                ValidaFechaVencimiento = True
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            Throw ex
        End Try

    End Function

    Private txtCodigoProductoGrid As TextBox
    Private txtNoLinea As TextBox

    Private Sub grdListaRecepcion_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DgridDetalleRec.EditingControlShowing

        Dim Dt As New List(Of String)

        Try

            DgridDetalleRec.EndEdit()

            If txtCodigoProductoGrid IsNot Nothing Then txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()

            Dim lProducto As String = String.Empty
            Dim vNoLinea As String = String.Empty

            If DgridDetalleRec.CurrentCell.ColumnIndex = 1 Then
                txtCodigoProductoGrid = TryCast(e.Control, TextBox)
                txtCodigoProductoGrid.BackColor = Color.White
            ElseIf DgridDetalleRec.CurrentCell.ColumnIndex = 0 Then
                txtNoLinea = TryCast(e.Control, TextBox)
                txtNoLinea.BackColor = Color.White
            End If

            lProducto = IIf(IsDBNull(DgridDetalleRec.Item(1, DgridDetalleRec.CurrentCell.RowIndex).Value), "", DgridDetalleRec.Item(1, DgridDetalleRec.CurrentCell.RowIndex).Value)
            vNoLinea = IIf(IsDBNull(DgridDetalleRec.Item("No_Linea", DgridDetalleRec.CurrentCell.RowIndex).Value), "", DgridDetalleRec.Item("No_Linea", DgridDetalleRec.CurrentCell.RowIndex).Value)

            txtCodigoProductoGrid = TryCast(e.Control, TextBox)

            Select Case DgridDetalleRec.CurrentCell.OwningColumn.Name

                '#EJC20180113: Sugerir número de línea si en la O.C. existe. en la recepción.
                Case "No_Linea"

                    If Not txtNoLinea Is Nothing Then

                        If Not BeTransOcEnc Is Nothing Then

                            txtNoLinea.AutoCompleteCustomSource.Clear()
                            txtNoLinea.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                            txtNoLinea.AutoCompleteSource = AutoCompleteSource.CustomSource

                            Dim lNoLineas = (From u In BeTransOcEnc.DetalleOC.AsEnumerable()
                                             Select u.No_Linea).Distinct()

                            For Each Nl In lNoLineas
                                txtNoLinea.AutoCompleteCustomSource.Add(Nl.ToString())
                            Next

                        End If

                    End If

                Case "CodigoP"

                    If txtCodigoProductoGrid IsNot Nothing Then

                        txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()
                        txtCodigoProductoGrid.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                        txtCodigoProductoGrid.AutoCompleteSource = AutoCompleteSource.CustomSource

                        If (BeTransOcEnc Is Nothing OrElse BeTransOcEnc.IdOrdenCompraEnc = 0) Then

                            Dt = GetCodigosSugeridos()

                            If Not Dt Is Nothing Then
                                txtCodigoProductoGrid.AutoCompleteCustomSource.AddRange(Dt.ToArray())
                            End If

                        Else

                            Dim lCodigosTodos As New List(Of String)

                            Dim lCodigosOC = (From u In BeTransOcEnc.DetalleOC.AsEnumerable()
                                              Select u.Producto.IdProducto).Distinct()

                            Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(AP.IdBodega, BeTransOcEnc.IdPropietarioBodega)

                            For Each IdProd In lCodigosOC
                                lCodigosTodos.AddRange(clsLnProducto.Get_Codigos_Sugeridos_By_IdProducto_And_IdPropietario(IdProd, vIdPropietario, cmbBodega.EditValue))
                            Next

                            txtCodigoProductoGrid.AutoCompleteCustomSource.AddRange(lCodigosTodos.ToArray())

                        End If

                    Else
                        If Not IsNothing(txtCodigoProductoGrid) Then txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()
                    End If

                Case Else

                    If txtCodigoProductoGrid IsNot Nothing Then
                        If String.IsNullOrEmpty(txtCodigoProductoGrid.Text.Trim) = False Then
                            txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()
                        End If
                    End If

            End Select

            If TypeOf e.Control Is Windows.Forms.ComboBox Then
                If DgridDetalleRec.CurrentCell.OwningColumn.Name = "PresentacionP" Then
                    Dim cmbPresentacion As Windows.Forms.ComboBox = TryCast(e.Control, Windows.Forms.ComboBox)

                    'remove handler if it was added before
                    RemoveHandler cmbPresentacion.SelectedIndexChanged, AddressOf cmbGridPresentacion_SelectedIndexChanged
                    AddHandler cmbPresentacion.SelectedIndexChanged, AddressOf cmbGridPresentacion_SelectedIndexChanged
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function GetCodigosSugeridos() As List(Of String)

        GetCodigosSugeridos = Nothing

        Try

            Dim vIdPropietario As Integer = 0

            Try

                vIdPropietario = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, cmbPropietario.EditValue)

            Catch ex As Exception
                'Console.Write("Nothing to do")
            End Try

            If vIdPropietario = 0 Then Exit Function

            Return clsLnProducto.Get_Codigos_Sugeridos_By_IdPropietario_And_IdBodega(vIdPropietario, cmbBodega.EditValue)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Sub cmdEliminarFila_Click(sender As Object, e As EventArgs) Handles cmdEliminarFila.Click

        Try

            If pBeTipo_Tarea_HH.UsaHH Then
                XtraMessageBox.Show("El tipo de tarea requiere que la linea se elimine desde la HH.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                EliminarFila(Nothing)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try


    End Sub

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click

        Try

            Dim factura As New frmFactura() With {.pIndex = -1, .pListObjF = pListRecFact, .Cargar = New frmFactura.Operar(AddressOf Cargar_Facturas)}
            factura.ShowDialog()
            factura.Dispose()

            'Gaurdar Facturas asociadas
            clsLnTrans_re_fact.Guarda_facturas_asoc(gBeRecepcionEnc.IdRecepcionEnc,
                                                    pListRecFact)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub grdListaFactura_DoubleClick(sender As Object, e As EventArgs) Handles grdListaFactura.DoubleClick

        Try

            If grdListaFactura.Rows.Count > 0 Then

                Dim IdFactura As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("IdFacturaRecepcion").Index)
                Dim IdRecepcionF As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("IdRecepcion").Index)
                Dim colFac As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("NoFactura").Index)

                Dim lIndexF As Integer = -1
                If IdFactura.Value > 0 AndAlso IdRecepcionF.Value > 0 Then
                    lIndexF = pListRecFact.FindIndex(Function(b) b.IdFacturaRecepcion = IdFactura.Value And b.IdRecepcionEnc = IdRecepcionF.Value)
                Else
                    lIndexF = pListRecFact.FindIndex(Function(b) b.NoFactura = colFac.Value)
                End If

                Dim factura As New frmFactura() With {.pIndex = lIndexF, .pListObjF = pListRecFact, .Cargar = New frmFactura.Operar(AddressOf Cargar_Facturas)}
                factura.ShowDialog()
                factura.Dispose()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdArriba_Click(sender As Object, e As EventArgs) Handles cmdArriba.Click
        SetOrden(True)
    End Sub

    Private Sub cmdAbajo_Click(sender As Object, e As EventArgs) Handles cmdAbajo.Click

        SetOrden(False)

    End Sub

    Private Sub SetOrden(ByVal pArriba As Boolean)

        Try

            If grdListaFactura.Rows.Count > 0 AndAlso grdListaFactura.CurrentRow.Index <> grdListaFactura.Rows.Count - 1 Then

                ' Fila seleccionada
                Dim IdFacturaS As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("IdFacturaRecepcion").Index)
                Dim IdRecepcionFS As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("IdRecepcion").Index)
                Dim colFacS As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("NoFactura").Index)

                Dim lIndexFS As Integer = -1
                If IdFacturaS.Value > 0 AndAlso IdRecepcionFS.Value > 0 Then
                    lIndexFS = pListRecFact.FindIndex(Function(b) b.IdFacturaRecepcion = IdFacturaS.Value And b.IdRecepcionEnc = IdRecepcionFS.Value)
                Else
                    lIndexFS = pListRecFact.FindIndex(Function(b) b.NoFactura = colFacS.Value)
                End If

                If lIndexFS > -1 Then

                    Dim OrdenArriba As Integer = 0
                    Dim OrdenAbajo As Integer = 0

                    If pArriba Then

                        OrdenArriba = pListRecFact(lIndexFS - 1).Orden
                        OrdenAbajo = pListRecFact(lIndexFS).Orden

                        pListRecFact(lIndexFS - 1).Orden = OrdenAbajo
                        pListRecFact(lIndexFS).Orden = OrdenArriba
                        Cargar_Facturas()

                    Else

                        OrdenArriba = pListRecFact(lIndexFS).Orden
                        OrdenAbajo = pListRecFact(lIndexFS + 1).Orden

                        pListRecFact(lIndexFS).Orden = OrdenAbajo
                        pListRecFact(lIndexFS + 1).Orden = OrdenArriba

                    End If

                    Cargar_Facturas()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdEliminarFactura_Click(sender As Object, e As EventArgs) Handles cmdEliminarFactura.Click

        Try

            Cursor = Cursors.WaitCursor

            If grdListaFactura.Rows.Count > 0 Then

                Dim NoFactura As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("NoFactura").Index)

                If XtraMessageBox.Show(String.Format("¿Desea eliminar la factura {0}?", NoFactura.Value), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Eliminando...")

                    Dim IdFacturaRecepcion As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("IdFacturaRecepcion").Index)
                    Dim IdRecepcion As DataGridViewCell = grdListaFactura.CurrentRow.Cells(grdListaFactura.Columns("IdRecepcion").Index)

                    Dim lIndexFS As Integer = -1
                    If IdFacturaRecepcion.Value > 0 AndAlso IdRecepcion.Value > 0 Then
                        lIndexFS = pListRecFact.FindIndex(Function(b) b.IdFacturaRecepcion = IdFacturaRecepcion.Value And b.IdRecepcionEnc = IdRecepcion.Value)
                    Else
                        lIndexFS = pListRecFact.FindIndex(Function(b) b.NoFactura = NoFactura.Value)
                    End If

                    If lIndexFS > -1 Then
                        clsLnTrans_re_fact.Delete(IdFacturaRecepcion.Value)
                        pListRecFact.RemoveAt(lIndexFS)
                        SplashScreenManager.CloseForm(False)
                        Cargar_Facturas()
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GridViewImg_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridViewImg.RowStyle

        Try

            GridViewImg.OptionsBehavior.Editable = False
            GridViewImg.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewImg.FocusRectStyle = DrawFocusRectStyle.RowFocus

            GridViewImg.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewImg.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewImg.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewImg.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewImg.Appearance.FocusedRow.ForeColor = Color.White
            GridViewImg.Appearance.SelectedRow.ForeColor = Color.White

            GridViewImg.Appearance.SelectedRow.Options.UseBackColor = True
            GridViewImg.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub grdListaRecepcion_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgridDetalleRec.CellFormatting

        Try

            If e.ColumnIndex >= 5 AndAlso e.ColumnIndex <= 8 Then
                If e.Value IsNot Nothing AndAlso IsNumeric(e.Value.ToString()) Then
                    Dim Valor As Double = Double.Parse(e.Value.ToString())
                    e.Value = Valor.ToString("N6")
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdTipoTR_TextChanged(sender As Object, e As EventArgs) Handles txtIdTipoTR.TextChanged

        Try

            If String.IsNullOrEmpty(txtIdTipoTR.Text.Trim()) = False Then

                Dim BeTransReTR As New clsBeTrans_re_tr() With {.IdTipoTransaccion = txtIdTipoTR.Text.Trim}
                BeTransReTR = clsLnTrans_re_tr_Partial.GetSingle(BeTransReTR.IdTipoTransaccion)

                If BeTransReTR IsNot Nothing AndAlso String.IsNullOrEmpty(BeTransReTR.Descripcion) = False Then

                    txtDescripcionTR.Text = BeTransReTR.Descripcion
                    Configura_Opcion_Tipo_Rec(BeTransReTR)

                    If BeTransReTR.UsaHH = 0 Then
                        chkRecepcionManual.Checked = True
                        '#CKFK20251012 Quité esta funcionalidad
                        'chkHabilitaStock.Checked = False
                    Else
                        chkRecepcionManual.Checked = False
                        '#CKFK20251012 Quité esta funcionalidad
                        'chkHabilitaStock.Checked = True
                    End If

                    '#CKFK 20210624 Se llama a la función creada por EJC para habilitar o no el stock basado en las reglas del propietario
                    Check_Reglas_Propietario_Ingreso()

                Else
                    txtDescripcionTR.Text = ""
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub grdListaRecepcion_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DgridDetalleRec.CurrentCellDirtyStateChanged

        Try

            Dim col As DataGridViewColumn = DgridDetalleRec.Columns(DgridDetalleRec.CurrentCell.ColumnIndex)

            If col.Name = "PresentacionP" Or col.Name = "CantidadP" Then
                DgridDetalleRec.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Sub cmdAgregarProducto_Click(sender As Object, e As EventArgs) Handles cmdAgregarProducto.Click

        Try

            ValidaCantidad()

            Using Producto As New frmProductoList(True)

                Producto.cmdImportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Producto.RbgActivo.Visible = False
                Producto.pIdBodega = cmbBodega.EditValue
                Producto.pIdPropietarioBodega = cmbPropietario.EditValue
                Producto.Modo = frmProductoList.pModo.Seleccion
                Producto.WindowState = FormWindowState.Maximized

                Producto.ShowDialog()

                If Producto.pObjProducto IsNot Nothing AndAlso Producto.pObjProducto.IdProducto <> 0 Then
                    pSelecciono = True
                    setProducto(Producto.pObjProducto)
                    pSelecciono = False
                End If

            End Using

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdVerParametros_Click(sender As Object, e As EventArgs) Handles cmdVerParametros.Click

        Try

            If DgridDetalleRec.Rows.Count > 0 AndAlso DgridDetalleRec.CurrentRow IsNot Nothing Then

                Dim vIdRecDet As Integer = DgridDetalleRec.CurrentRow.Cells("IdRecepcionDet").Value
                Dim vIdEstado As Integer = 0
                Dim DgCombo As New DataGridViewComboBoxCell()
                DgCombo = TryCast(DgridDetalleRec.CurrentRow.Cells("Estado"), DataGridViewComboBoxCell)
                vIdEstado = DgCombo.Value

                If BeProducto Is Nothing Then

                    Dim lCodigo As Object = DgridDetalleRec.CurrentRow.Cells("CodigoP").Value

                    BeProducto = New clsBeProducto
                    BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(lCodigo, cmbBodega.EditValue)

                    If BeProducto.IdProducto > 0 Then
                        Set_Stock_Parametro(BeProducto, vIdRecDet, DgridDetalleRec.CurrentRow.Index, vIdEstado)
                    End If

                Else
                    Set_Stock_Parametro(BeProducto, vIdRecDet, DgridDetalleRec.CurrentRow.Index, vIdEstado)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub xtrRecepcion_TabIndexChanged(sender As Object, e As EventArgs) Handles xtrRecepcion.TabIndexChanged

        cmdAgregarProducto.Enabled = False
        cmdVerParametros.Enabled = False
        DgridDetalleRec.Enabled = False

        Try

            If (txtIdTipoTR.Text = String.Empty OrElse txtDescripcionTR.Text = String.Empty) AndAlso xtrRecepcion.SelectedTabPageIndex = 2 Then

                XtraMessageBox.Show("Seleccione Tipo de Transacción", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            ElseIf (String.IsNullOrEmpty(txtIdUbicacion.Text) OrElse String.IsNullOrEmpty(txtNombreUbicacion.Text)) AndAlso xtrRecepcion.SelectedTabPageIndex = 2 Then

                XtraMessageBox.Show("Seleccione Ubicación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                xtrRecepcion.SelectedTabPageIndex = 0
                txtIdUbicacion.Focus()
            ElseIf xtrRecepcion.SelectedTabPage Is tabDetalleRecepcion2 Then

                Dim ColLinea As GridColumn = gvDetalleRec2.Columns("No_Linea")

                If Not ColLinea Is Nothing Then

                    DgridDetalleRec2.BeginInvoke(New MethodInvoker(Sub()
                                                                       gvDetalleRec2.FocusedRowHandle = GridControl.NewItemRowHandle
                                                                       gvDetalleRec2.FocusedColumn = ColLinea
                                                                       gvDetalleRec2.MakeColumnVisible(ColLinea)
                                                                       If gvDetalleRec2.FocusedColumn IsNot Nothing Then
                                                                           gvDetalleRec2.ShowEditor()
                                                                       End If
                                                                   End Sub))

                End If

            Else
                cmdAgregarProducto.Enabled = True
                cmdVerParametros.Enabled = True
                DgridDetalleRec.Enabled = True
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick

        Try

            If cmdEliminar.Enabled Then

                If gBeRecepcionEnc.Estado = "Cerrado" Then
                    XtraMessageBox.Show("No se puede anular la recepción, el stock ya fue procesado.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
                Else

                    Dim IdMotivoAnulacionBodega As Integer = 0

                    Using MA As New frmMotivo_AnulacionList()
                        With MA
                            .Modo = frmMotivo_AnulacionList.pModo.Seleccion
                            .BeMotivoAnulacionBodega.IdBodega = cmbBodega.EditValue
                            If .ShowDialog() = DialogResult.OK Then
                                IdMotivoAnulacionBodega = .BeMotivoAnulacionBodega.IdMotivoAnulacionBodega
                            End If
                        End With
                    End Using

                    If IdMotivoAnulacionBodega <> 0 Then

                        '#EJC20221011_1148: WMS - Agregué función Existe_Stock_By_IdBodega_And_IdRecepcionEnc para validar si existe stock antes de anular una recepción.
                        If Not clsLnStock.Existe_Stock_By_IdBodega_And_IdRecepcionEnc(gBeRecepcionEnc.IdBodega,
                                                                                      gBeRecepcionEnc.IdRecepcionEnc) Then

                            If clsLnTrans_re_enc.Anular_Recepcion(gBeRecepcionEnc,
                                                                  IdMotivoAnulacionBodega,
                                                                  gBeRecepcionEnc.TareaHH) Then

                                '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
                                Dim msjAdvertencia As String = "ADVERTENCIA_202302231712A: Se anuló la recepción: " & gBeRecepcionEnc.IdRecepcionEnc & " Por el IdUsuario: " & AP.UsuarioAp.IdUsuario
                                clsLnLog_error_wms_rec.Agregar_Error(msjAdvertencia,
                                                                     AP.UsuarioAp.IdEmpresa,
                                                                     AP.IdBodega,
                                                                     AP.UsuarioAp.IdUsuario,
                                                                     pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

                                SplashScreenManager.CloseForm(False)

                                XtraMessageBox.Show("Recepción anulada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                If Listar IsNot Nothing Then
                                    Listar.Invoke()
                                End If

                                Close()

                            Else
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("No se pudo anular la recepcion.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If

                        Else
                            SplashScreenManager.CloseForm(False)
                            XtraMessageBox.Show("La recepcion ya tiene stock recibido, no se puede anular.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                    Else

                        XtraMessageBox.Show("Debe seleccionar el motivo de anulación para anular la recepción.",
                                                Text,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation)
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("Error: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdUbicacion_Validated(sender As Object, e As EventArgs) Handles txtIdUbicacion.Validated
        Valida_Ubicacion()
    End Sub

    Private Sub Valida_Ubicacion()

        Try

            If String.IsNullOrEmpty(txtIdUbicacion.Text.Trim()) = False AndAlso txtIdUbicacion.Text > "0" Then

                Dim BeBodegaUbicacion As New clsBeBodega_ubicacion

                BeBodegaUbicacion = clsLnBodega_ubicacion.Get_Ubicacion_Recepcion(txtIdUbicacion.Text.Trim)

                If BeBodegaUbicacion IsNot Nothing AndAlso BeBodegaUbicacion.IdUbicacion > 0 Then
                    txtNombreUbicacion.Text = BeBodegaUbicacion.Descripcion
                Else
                    XtraMessageBox.Show(String.Format("No existe Ubicación de Recepción con código {0}", txtIdUbicacion.Text.Trim(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
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

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub
    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            If cmbBodega.ItemIndex > -1 Then

                cmbPropietario.Properties.DataSource = Nothing
                IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
                cmbMuelle.Properties.DataSource = Nothing
                IMS.Listar_Muelles(cmbMuelle, cmbBodega.EditValue, True)

                '#EJC20220301: Set BeBodega en EditValueChange.
                Dim pIdBodega As Integer = cmbBodega.EditValue
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)

                Valida_Operadores()

                If Not pRecepcionInmediata Then
                    If BeBodega IsNot Nothing AndAlso String.IsNullOrEmpty(BeBodega.IdTipoTransaccion) = False Then
                        txtIdTipoTR.Text = BeBodega.IdTipoTransaccion
                        txtIdTipoTR_TextChanged(Nothing, Nothing)
                    End If
                Else
                    txtIdTipoTR.Text = clsDataContractDI.tTipo_Rec.HCOC00.ToString()
                    txtIdTipoTR_TextChanged(Nothing, Nothing)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        If cmbPropietario.EditValue <> 0 Then
            If cmbBodega.Text <> "" Then
                cmbPropietario.Tag = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, cmbPropietario.EditValue)

                '#CKFK 20210624 Se llama a la función creada por EJC para habilitar o no el stock basado en las reglas del propietario
                Check_Reglas_Propietario_Ingreso()
                '#GT31012024: recargamos los estados/productos en el grid para el nuevo propietario
                Actualizar_LookupGridEstadoProducto()
                Actualizar_LookupGridProducto()

            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub txtIdVehiculo_Validated(sender As Object, e As EventArgs) Handles txtIdVehiculo.Validated

        Try

            If String.IsNullOrEmpty(txtIdVehiculo.Text.Trim()) = False AndAlso txtIdVehiculo.Text > "0" Then

                Dim Obj As New clsBeEmpresa_transporte_vehiculos
                Obj = clsLnEmpresa_transporte_vehiculos.GetNombre(txtIdVehiculo.Text)

                If Obj IsNot Nothing AndAlso Obj.IdVehiculo > 0 Then
                    'txtNombreVehiculo.Text = Trim(String.Format("Placa: {0}", Obj.Placa))
                    '#GT13112023: homologar los mismo campos, al seleccionar y al cargar de la re_enc
                    txtNombreVehiculo.Text = Trim(String.Format("Placa: {0} - {1} - {2} - {3}", Obj.Placa, Obj.Marca, Obj.Modelo, Obj.Tipo))

                Else

                    XtraMessageBox.Show(String.Format("No existe Vehículo con código {0}", txtIdVehiculo.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdVehiculo.Focus()
                    txtIdVehiculo.SelectAll()

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdVehiculo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdVehiculo.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdVehiculo.Text.Length = 1 Then
                txtNombreVehiculo.Text = String.Empty
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Sub txtIdPiloto_Validated(sender As Object, e As EventArgs) Handles txtIdPiloto.Validated

        Try

            If String.IsNullOrEmpty(txtIdPiloto.Text.Trim()) = False AndAlso txtIdPiloto.Text > "0" Then

                Dim BePiloto As New clsBeEmpresa_transporte_pilotos
                BePiloto = clsLnEmpresa_transporte_pilotos.GetNombre(txtIdPiloto.Text)

                If BePiloto IsNot Nothing AndAlso BePiloto.IdPiloto > 0 Then
                    txtNombrePiloto.Text = Trim(String.Format("{0} {1}", BePiloto.Nombres, BePiloto.Apellidos))

                    clsLnTrans_re_enc.Actualizar_Piloto_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc, BePiloto.IdPiloto)

                Else

                    XtraMessageBox.Show(String.Format("No existe Piloto con código {0}", txtIdPiloto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdPiloto.Focus()
                    txtIdPiloto.SelectAll()

                End If

            Else

                If Cargando Then Return

                '#EJC303405090229AM
                If Modo = TipoTrans.Editar Then

                    If Val(txtIdPiloto.Text) = 0 AndAlso gBeRecepcionEnc.IdPiloto <> 0 Then

                        txtNombrePiloto.Text = ""

                        If XtraMessageBox.Show("¿Quitar el piloto de la recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            clsLnTrans_re_enc.Actualizar_Piloto_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc, Val(txtIdPiloto.Text))

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdPiloto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdPiloto.KeyPress

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

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdPiloto.Text.Length = 1 Then
                txtNombrePiloto.Text = String.Empty
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Sub lnkPiloto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkPiloto.LinkClicked

        Try

            Dim Piloto As New frmEmpresa_Transporte_PilotoList() With {.Modo = frmEmpresa_Transporte_PilotoList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Piloto.OpcionesMenu = OpcionesMenu
                Piloto.mnuNuevo.Enabled = OpcionesMenu.Modificar
                Piloto.mmuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Piloto.ShowDialog()

            If Piloto.pObjPiloto IsNot Nothing AndAlso Piloto.pObjPiloto.IdPiloto > 0 Then

                txtIdPiloto.Text = Piloto.pObjPiloto.IdPiloto
                txtNombrePiloto.Text = Trim(String.Format("{0} {1}", Piloto.pObjPiloto.Nombres, Piloto.pObjPiloto.Apellidos))

                clsLnTrans_re_enc.Actualizar_Piloto_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc, Piloto.pObjPiloto.IdPiloto)

            End If

            Piloto.Close()
            Piloto.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Sub lnkVehiculo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkVehiculo.LinkClicked

        Try

            Dim Vehiculo As New frmEmpresa_Transporte_VehiculoList() With {.Modo = frmEmpresa_Transporte_VehiculoList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Vehiculo.OpcionesMenu = OpcionesMenu
                Vehiculo.mnuNuevo.Enabled = OpcionesMenu.Modificar
                Vehiculo.mmuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Vehiculo.ShowDialog()

            If Vehiculo.pObjVehiculo IsNot Nothing AndAlso Vehiculo.pObjVehiculo.IdVehiculo > 0 Then

                txtIdVehiculo.Text = Vehiculo.pObjVehiculo.IdVehiculo
                txtNombreVehiculo.Text = Trim(String.Format("Placa: {0} - {1} - {2} - {3}", Vehiculo.pObjVehiculo.Placa, Vehiculo.pObjVehiculo.Marca, Vehiculo.pObjVehiculo.Modelo, Vehiculo.pObjVehiculo.Tipo))

                clsLnTrans_re_enc.Actualizar_Vehiculo_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc, Vehiculo.pObjVehiculo.IdVehiculo)

                If BeBodega.Es_Bodega_Fiscal Then

                    If Not BeTipoDocumento Is Nothing Then

                        If Not BeTipoDocumento.IdTipoIngresoOC <> 0 Then
                            txtCartaCupo.Focus()
                        End If

                    End If

                End If

            End If

            Vehiculo.Close()
            Vehiculo.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Sub cmdActualizarDetalle_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizarDetalle.ItemClick

        Recargar_Objeto_Recepcion()
        xtrRecepcion.SelectedTabPageIndex = 1

    End Sub


    Private Sub Recargar_Objeto_Recepcion()

        UseWaitCursor = True

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

        Try

            gBeRecepcionEnc = clsLnTrans_re_enc.GetSingle(gBeRecepcionEnc.IdRecepcionEnc)

            Cargar_Datos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        Finally
            UseWaitCursor = False
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub cmdMarcarTodosOperador_Click(sender As Object, e As EventArgs) Handles cmdMarcarTodosOperador.Click


        Try

            Dim IdOperadorBodega As Integer = 0
            Dim UsaHH As Boolean = False

            For i As Integer = 0 To GrdOperadorBobega.DataRowCount - 1
                GrdOperadorBobega.SetRowCellValue(i, "Selección", True)
                IdOperadorBodega = GrdOperadorBobega.GetRowCellValue(i, "IdOperadorBodega")
                UsaHH = GrdOperadorBobega.GetRowCellValue(i, "colUsaHH")
                Procesar_Operador_Seleccion(IdOperadorBodega, True, UsaHH)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private DesmarcandoOperadores As Boolean = False
    Private Sub cmdDesmarcarTodosOperador_Click(sender As Object, e As EventArgs) Handles cmdDesmarcarTodosOperador.Click

        DesmarcandoOperadores = True

        Try

            Dim IdOperadorBodega As Integer = 0
            Dim UsaHH As Boolean = False
            Dim vSeleccionado As Boolean = False

            For i As Integer = 0 To GrdOperadorBobega.DataRowCount - 1
                vSeleccionado = GrdOperadorBobega.GetRowCellValue(i, "Selección")
                GrdOperadorBobega.SetRowCellValue(i, "Selección", False)
                IdOperadorBodega = GrdOperadorBobega.GetRowCellValue(i, "IdOperadorBodega")
                UsaHH = GrdOperadorBobega.GetRowCellValue(i, "colUsaHH")
                Procesar_Operador_Seleccion(IdOperadorBodega, False, UsaHH)
            Next

            Valida_Operadores()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        Finally
            DesmarcandoOperadores = False
        End Try

    End Sub

    Private Sub Procesar_Operador_Seleccion(ByVal pIdOperadorBodega As Integer,
                                            ByVal pSeleccion As Boolean,
                                            ByVal pUsaHH As Boolean)

        Try

            Dim lIndex As Integer = -1
            Dim d As Integer = pIdOperadorBodega

            lIndex = pListOpe.FindIndex(Function(b) b.IdOperadorBodega = d)

            If lIndex > -1 Then

                If Not pSeleccion Then
                    If pListOpe(lIndex).IdOperadorBodega > 0 AndAlso pListOpe(lIndex).IdRecepcionEnc > 0 Then
                        clsLnTrans_re_op.Delete(pListOpe(lIndex).IdOperadorRec, pListOpe(lIndex).IdRecepcionEnc)
                    End If
                    pListOpe.RemoveAt(lIndex)
                End If

            Else
                If pSeleccion Then

                    '#GT23092025: evita duplicar al operador en la lista
                    If Not pListOpe.Any(Function(x) x.IdOperadorBodega = pIdOperadorBodega) Then
                        Dim Obj As New clsBeTrans_re_op() With
                        {.IdOperadorBodega = pIdOperadorBodega,
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now,
                        .IsNew = True,
                        .UsaHH = pUsaHH}
                        pListOpe.Add(Obj)
                    End If

                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Sub PicImg_MouseWheel(sender As Object, e As MouseEventArgs) Handles PicImg.MouseWheel

        Try


            If e.Delta <> 0 Then

                If e.Delta <= 0 Then
                    If PicImg.Width < 500 Then Exit Sub 'minimum 500?
                Else
                    If PicImg.Width > 2000 Then Exit Sub 'maximum 2000?
                End If
                PicImg.Width += CInt(PicImg.Width * e.Delta / 1000)
                PicImg.Height += CInt(PicImg.Height * e.Delta / 1000)

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Sub GrdOperadorBobega_CellValueChanging(sender As Object, e As CellValueChangedEventArgs) Handles GrdOperadorBobega.CellValueChanging

        If e.Column.Name = "colSelección" Then

            Debug.Print("something IS changing")

            Try

                'GT30052022: se cambia de layoutview a GridView
                'Dim vView As LayoutView = TryCast(sender, LayoutView)
                Dim vView As GridView = TryCast(sender, GridView)

                If Not vView Is Nothing Then

                    'GT09022022: vView solo devuelve False
                    'Dim vSeleccionado As Boolean = vView.GetRowCellValue(e.RowHandle, "colSelección")
                    Dim vSeleccionado As Boolean = e.Value
                    Dim Dr As DataRowView = GrdOperadorBobega.GetFocusedRow
                    Dim lIndex As Integer = -1
                    Dim d As Integer = Dr.Item("IdOperadorBodega")

                    lIndex = pListOpe.FindIndex(Function(b) b.IdOperadorBodega = d)

                    If lIndex > -1 Then

                        'pListOpe(lIndex).UsaHH = vSeleccionado

                        If Not vSeleccionado Then

                            If pListOpe(lIndex).IdOperadorRec > 0 AndAlso pListOpe(lIndex).IdRecepcionEnc Then
                                clsLnTrans_re_op.Delete(pListOpe(lIndex).IdOperadorRec, pListOpe(lIndex).IdRecepcionEnc)
                            End If

                            pListOpe.RemoveAt(lIndex)
                            'GT 20072021: Si es una recepción nueva, no se requiere Listar_Operadores, cada vez que se desactiva uno de las cards
                            'GT 23052021 la carga de operadores se hace despues de eliminar un operador, cuando es edición.
                            If Modo = TipoTrans.Editar Then
                                Listar_Operadores()
                            End If

                        End If

                    Else

                        '#GT23092025: evita duplicar al mismo operador en la lista
                        Dim tmpOperadorBodega = CInt(Dr.Item("IdOperadorBodega"))

                        If Not pListOpe.Any(Function(x) x.IdOperadorBodega = tmpOperadorBodega) Then

                            Dim Obj As New clsBeTrans_re_op() With
                                {.IdOperadorBodega = Dr.Item("IdOperadorBodega"),
                                .User_agr = AP.UsuarioAp.IdUsuario,
                                .Fec_agr = Now,
                                .User_mod = AP.UsuarioAp.IdUsuario,
                                .Fec_mod = Now,
                                .IsNew = True,
                                .UsaHH = Dr.Item("colUsaHH")}
                            pListOpe.Add(Obj)
                        End If

                    End If

                End If

            Catch ex As Exception
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        End If

    End Sub
    Private Class MarcajeOperador
        Public Property IdOperadorBodega As Integer = 0
        Public Property TieneMarcaje As Boolean = False

    End Class
    Private Sub GrdOperadorBobega_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GrdOperadorBobega.RowCellStyle

        Try

            Dim View1 As GridView = sender

            If View1.Columns Is Nothing Then Exit Sub
            If View1.Columns.Count = 0 Then Exit Sub

            Dim TieneMarcaje As Boolean = IIf(IsDBNull(View1.GetRowCellValue(e.RowHandle, "IngresoHH")), False, View1.GetRowCellValue(e.RowHandle, "IngresoHH"))

            If TieneMarcaje Then 'Ya se logueo en la HH
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.BackColor2 = Color.White
            Else 'No se ha logueado en la HH
                'e.Appearance.BackColor = Color.PaleVioletRed
                e.Appearance.BackColor2 = Color.White
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private DTGridDetalleDocIngresos As New DataTable("DetalleIngreso")

    Private Sub Set_Datata_Table_Grid_Detalle_Documento_Ingreso()

        DTGridDetalleDocIngresos.Columns.Clear()
        DTGridDetalleDocIngresos.Columns.Add("IdPropietarioBodega", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("NombrePropietario", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("NoLinea", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdProductoBodega", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("CodigoProducto", GetType(String))
        'DTGridDetalleDocIngresos.Columns.Add("CodigoBarra", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("NombreProducto", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("UMBas", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("IdUmBas", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdPresentacion", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("Arancel", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdMotivoDevolucion", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("Cantidad", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Cantidad_Recibida", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Cantidad_Pendiente", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("PesoBruto", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("PesoNeto", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Costo", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorAduana", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorFOB", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorIVA", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorDAI", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorSeguro", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorFlete", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Total", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("IdProducto", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IsNew", GetType(Boolean))
        DTGridDetalleDocIngresos.Columns.Add("IdOrdenCompraEnc", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdOrdenCompraDet", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("CapturaArancel", GetType(Boolean))
        DTGridDetalleDocIngresos.Columns.Add("Variant_Code", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("EsKit", GetType(Boolean))
        DTGridDetalleDocIngresos.Columns.Add("IdPedidoCompraDet", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdOrdenCompraDetPadre", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("ControlPeso", GetType(Boolean))
        DTGridDetalleDocIngresos.Columns.Add("PesoReferenciaUMBas", GetType(Double))

    End Sub

    Private Sub Set_Columnas_Grid_Detalle_Documento_Ingreso()

        Try

            DgridDetalleOC.DataSource = DTGridDetalleDocIngresos

            Dim ColIndexAux As Integer = 0

            gvDetalleDocIngreso.OptionsView.ShowFooter = True
            gvDetalleDocIngreso.OptionsView.ShowGroupPanel = False

            gvDetalleDocIngreso.OptionsView.ColumnAutoWidth = False

            gvDetalleDocIngreso.Columns.Clear()

#Region "Columna - Propietario"

            PropietarioGridLookUpEdit.View.Columns.Clear()

            PropietarioGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdPropietarioBodega", .Caption = "IdPropietario", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            PropietarioGridLookUpEdit.ValueMember = "IdPropietarioBodega"
            PropietarioGridLookUpEdit.DisplayMember = "Nombre"
            PropietarioGridLookUpEdit.NullText = ""
            PropietarioGridLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(cmbBodega.EditValue)
            PropietarioGridLookUpEdit.PopupFormWidth = 700
            PropietarioGridLookUpEdit.View.BestFitColumns()

            PropietarioGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColPropieario As New GridColumn With {
                .FieldName = "IdPropietarioBodega",
                .Caption = "Propietario",
                .Visible = True,
                .VisibleIndex = 0,
                .ColumnEdit = PropietarioGridLookUpEdit
            }
            ColPropieario.Width = 300
            ColPropieario.OptionsColumn.AllowEdit = False
            ColPropieario.Visible = (gBeOrdenCompra.TipoIngreso.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado)
            gvDetalleDocIngreso.Columns.Add(ColPropieario)

#End Region

#Region "Columna - No_Linea"

            Dim ColNoLinea As New GridColumn With {
                .FieldName = "NoLinea",
                .Caption = "No. Linea",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtNoLineaGrid,
                .VisibleIndex = 1
            }

            ColNoLinea.OptionsColumn.AllowEdit = False
            gvDetalleDocIngreso.Columns.Add(ColNoLinea)


#End Region

#Region "Columna - IdProductoBodega"

            ProductoGridLookUpEdit.View.Columns.Clear()

            ProductoGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdProductoBodega", .Caption = "IdProductoBodega", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True},
                New GridColumn With {.FieldName = "UMBas", .Caption = "UMBas", .Visible = True},
                New GridColumn With {.FieldName = "IdUmBas", .Caption = "IdUmBas", .Visible = False},
                New GridColumn With {.FieldName = "ControlPeso", .Caption = "ControlPeso", .Visible = False}})

            ProductoGridLookUpEdit.ValueMember = "IdProductoBodega"
            ProductoGridLookUpEdit.DisplayMember = "Codigo"
            ProductoGridLookUpEdit.NullText = "-> Producto"
            ProductoGridLookUpEdit.PopupFormWidth = 700
            ProductoGridLookUpEdit.DataSource = clsLnProducto.Get_Lista_For_Grid_By_IdBodega(cmbBodega.EditValue)
            ProductoGridLookUpEdit.View.BestFitColumns()

            ProductoGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColIdProductoBodega As New GridColumn With {
                .FieldName = "IdProductoBodega",
                .Caption = "Código",
                .Visible = True,
                .VisibleIndex = 2,
                .ColumnEdit = ProductoGridLookUpEdit
            }

            '#EJC20210306: Permitir el ingreso de valores que no estén en la lista.
            ProductoGridLookUpEdit.AcceptEditorTextAsNewValue = DefaultBoolean.True

            ColIdProductoBodega.OptionsColumn.AllowEdit = False
            ColIdProductoBodega.Width = 150
            gvDetalleDocIngreso.Columns.Add(ColIdProductoBodega)

#End Region

#Region "Columna - CodigoProducto"

            Dim ColCodigoProducto As New GridColumn With {
                .FieldName = "CodigoProducto",
                .Caption = "CodigoProducto",
                .Width = 100,
                .VisibleIndex = 3
            }

            ColCodigoProducto.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColCodigoProducto)


#End Region

#Region "Columna - Nombre_Producto"

            Dim ColNombreProducto As New GridColumn With {
                .FieldName = "NombreProducto",
                .Caption = "Nombre",
                .Visible = True,
                .VisibleIndex = 4,
                .Width = 300
            }

            ColNombreProducto.OptionsColumn.AllowEdit = False

            gvDetalleDocIngreso.Columns.Add(ColNombreProducto)

#End Region

#Region "Columna - NomUMBas"


            Dim ColUMBas As New GridColumn With {
                .FieldName = "UMBas",
                .Caption = "UMBas",
                .Visible = True,
                .VisibleIndex = 4,
                .Width = 75
            }

            ColUMBas.OptionsColumn.AllowEdit = False

            gvDetalleDocIngreso.Columns.Add(ColUMBas)

#End Region

#Region "Columna - IdUMBas"

            Dim ColIdUmBas As New GridColumn With {
                .FieldName = "IdUmBas",
                .Caption = "IdUmBas",
                .VisibleIndex = 5,
                .Width = 75
            }

            ColIdUmBas.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColIdUmBas)

#End Region

#Region "Columna - Presentacion"

            PresentacionGridLookUpEdit.View.Columns.Clear()

            PresentacionGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdPresentacion", .Caption = "IdPresentacion", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            PresentacionGridLookUpEdit.ValueMember = "IdPresentacion"
            PresentacionGridLookUpEdit.DisplayMember = "Nombre"
            PresentacionGridLookUpEdit.NullText = ""
            PresentacionGridLookUpEdit.DataSource = clsLnProducto_presentacion.Get_All_By_IdBodega(cmbBodega.EditValue)
            PresentacionGridLookUpEdit.View.BestFitColumns()
            PresentacionGridLookUpEdit.PopupFormWidth = 700

            PresentacionGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColPresentacion As New GridColumn With {
                .FieldName = "IdPresentacion",
                .Caption = "Presentacion",
                .Visible = True,
                .VisibleIndex = 6,
                .ColumnEdit = PresentacionGridLookUpEdit
            }

            ColPresentacion.Width = 100
            ColPresentacion.OptionsColumn.AllowEdit = False
            gvDetalleDocIngreso.Columns.Add(ColPresentacion)

#End Region

#Region "Columna - Motivo_Devolución"

            MotivoDevolcuionGridLookUpEdit.View.Columns.Clear()

            MotivoDevolcuionGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdMotivoDevolucion", .Caption = "Código", .Visible = False},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            MotivoDevolcuionGridLookUpEdit.DataSource = clsLnMotivo_devolucion.Get_All_By_IdPropietario_And_Bodega_DT(cmbPropietario.Tag, cmbBodega.EditValue)
            MotivoDevolcuionGridLookUpEdit.ValueMember = "IdMotivoDevolucion"
            MotivoDevolcuionGridLookUpEdit.DisplayMember = "Nombre"
            MotivoDevolcuionGridLookUpEdit.NullText = ""
            MotivoDevolcuionGridLookUpEdit.View.BestFitColumns()

            Dim ColMotivoDevolucion As New GridColumn With {
                  .FieldName = "IdMotivoDevolucion",
                  .Caption = "Motivo Devolución",
                  .Visible = True,
                  .Width = 150,
                  .VisibleIndex = 7,
                  .ColumnEdit = MotivoDevolcuionGridLookUpEdit
              }

            ColMotivoDevolucion.OptionsColumn.AllowEdit = False

            gvDetalleDocIngreso.Columns.Add(ColMotivoDevolucion)

#End Region

#Region "Columna - Cantidad"

            ColIndexAux = 8

            Dim ColCantidad As New GridColumn With {
                .FieldName = "Cantidad",
                .Caption = "Cantidad",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidad.OptionsColumn.AllowEdit = False
            ColCantidad.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidad.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColCantidad)

            gvDetalleDocIngreso.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("Cantidad").SummaryItem.DisplayFormat = "Cantidad: {0:n6}"
            gvDetalleDocIngreso.Columns("Cantidad").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - Cantidad_Recibida"

            ColIndexAux = 8

            Dim ColCantidadRecibida As New GridColumn With {
                .FieldName = "Cantidad_Recibida",
                .Caption = "Cantidad Recibida",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidadRecibida.OptionsColumn.AllowEdit = False
            ColCantidadRecibida.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidadRecibida.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColCantidadRecibida)

            gvDetalleDocIngreso.Columns("Cantidad_Recibida").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("Cantidad_Recibida").SummaryItem.DisplayFormat = "Recibido: {0:n6}"
            gvDetalleDocIngreso.Columns("Cantidad_Recibida").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Cantidad_Recibida").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - Cantidad_Pendiente"

            ColIndexAux = 8

            Dim ColCantidadPendiente As New GridColumn With {
                .FieldName = "Cantidad_Pendiente",
                .Caption = "Cantidad Pendiente",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidadPendiente.OptionsColumn.AllowEdit = False
            ColCantidadPendiente.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidadPendiente.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColCantidadPendiente)

            gvDetalleDocIngreso.Columns("Cantidad_Pendiente").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("Cantidad_Pendiente").SummaryItem.DisplayFormat = "Pendiente: {0:n6}"
            gvDetalleDocIngreso.Columns("Cantidad_Pendiente").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Cantidad_Pendiente").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - PesoBruto"

            Dim ColPesoBruto As New GridColumn With {
                .FieldName = "PesoBruto",
                .Caption = "Peso Bruto",
                .Visible = True,
                .Width = 100,
                .VisibleIndex = ColIndexAux
            }

            ColPesoBruto.OptionsColumn.AllowEdit = False
            ColPesoBruto.DisplayFormat.FormatType = FormatType.Numeric
            ColPesoBruto.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColPesoBruto)

            gvDetalleDocIngreso.Columns("PesoBruto").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("PesoBruto").SummaryItem.DisplayFormat = "Peso bruto: {0:n6}"
            gvDetalleDocIngreso.Columns("PesoBruto").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("PesoBruto").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - PesoNeto"

            Dim ColPesoNeto As New GridColumn With {
                .FieldName = "PesoNeto",
                .Caption = "Peso Neto",
                .Visible = True,
                .Width = 100,
                .VisibleIndex = ColIndexAux
            }

            ColPesoNeto.OptionsColumn.AllowEdit = False
            ColPesoNeto.DisplayFormat.FormatType = FormatType.Numeric
            ColPesoNeto.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColPesoNeto)

            gvDetalleDocIngreso.Columns("PesoNeto").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("PesoNeto").SummaryItem.DisplayFormat = "Peso neto: {0:n6}"
            gvDetalleDocIngreso.Columns("PesoNeto").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("PesoNeto").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - Costo"

            Dim ColCosto As New GridColumn With {
                .FieldName = "Costo",
                .Caption = "Costo_Unitario",
                .Visible = True,
                .Width = 100,
                 .ColumnEdit = txtCostoGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColCosto.OptionsColumn.AllowEdit = False
            ColCosto.DisplayFormat.FormatType = FormatType.Numeric
            ColCosto.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColCosto)

            gvDetalleDocIngreso.Columns("Costo").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Costo").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorAduana"

            Dim ColValorAduana As New GridColumn With {
                .FieldName = "ValorAduana",
                .Caption = "Valor Aduana",
                .Visible = True,
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorAduana.OptionsColumn.AllowEdit = False
            ColValorAduana.DisplayFormat.FormatType = FormatType.Numeric
            ColValorAduana.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColValorAduana)

            gvDetalleDocIngreso.Columns("ValorAduana").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorAduana").SummaryItem.DisplayFormat = "Aduana: {0:n6}"
            gvDetalleDocIngreso.Columns("ValorAduana").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorAduana").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorFOB"

            Dim ColValorFOB As New GridColumn With {
                .FieldName = "ValorFOB",
                .Caption = "Valor FOB",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtValorFOBGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorFOB.OptionsColumn.AllowEdit = False
            ColValorFOB.DisplayFormat.FormatType = FormatType.Numeric
            ColValorFOB.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColValorFOB)

            gvDetalleDocIngreso.Columns("ValorFOB").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorFOB").SummaryItem.DisplayFormat = "FOB: {0:n6}"
            gvDetalleDocIngreso.Columns("ValorFOB").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorFOB").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorDAI"

            Dim ColValorDAI As New GridColumn With {
                .FieldName = "ValorDAI",
                .Caption = "Valor DAI",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtValorDAIGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorDAI.OptionsColumn.AllowEdit = False
            ColValorDAI.DisplayFormat.FormatType = FormatType.Numeric
            ColValorDAI.DisplayFormat.FormatString = "{0:n6}"


            gvDetalleDocIngreso.Columns.Add(ColValorDAI)

            gvDetalleDocIngreso.Columns("ValorDAI").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorDAI").SummaryItem.DisplayFormat = "DAI: {0:n6}"

            gvDetalleDocIngreso.Columns("ValorDAI").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorDAI").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorIVA"

            Dim ColValorIVA As New GridColumn With {
                .FieldName = "ValorIVA",
                .Caption = "Valor IVA",
                .Visible = True,
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorIVA.OptionsColumn.AllowEdit = False
            ColValorIVA.DisplayFormat.FormatType = FormatType.Numeric
            ColValorIVA.DisplayFormat.FormatString = "{0:n6}"
            ColValorIVA.OptionsColumn.AllowEdit = True

            gvDetalleDocIngreso.Columns.Add(ColValorIVA)
            gvDetalleDocIngreso.Columns("ValorIVA").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorIVA").SummaryItem.DisplayFormat = "IVA: {0:n6}"
            gvDetalleDocIngreso.Columns("ValorIVA").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorIVA").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorSeguro"

            Dim ColValorSeguro As New GridColumn With {
                .FieldName = "ValorSeguro",
                .Caption = "Valor Seguro",
                .Visible = True,
                .Width = 100,
                   .ColumnEdit = txtValorDAIGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorSeguro.OptionsColumn.AllowEdit = False
            ColValorSeguro.DisplayFormat.FormatType = FormatType.Numeric
            ColValorSeguro.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColValorSeguro)

            gvDetalleDocIngreso.Columns("ValorSeguro").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorSeguro").SummaryItem.DisplayFormat = "Seguro: {0:n6}"

            gvDetalleDocIngreso.Columns("ValorSeguro").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorSeguro").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorFlete"

            Dim ColValorFlete As New GridColumn With {
                .FieldName = "ValorFlete",
                .Caption = "Valor Flete",
                .Visible = True,
                .Width = 100,
                   .ColumnEdit = txtValorDAIGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorFlete.OptionsColumn.AllowEdit = False
            ColValorFlete.DisplayFormat.FormatType = FormatType.Numeric
            ColValorFlete.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColValorFlete)

            gvDetalleDocIngreso.Columns("ValorFlete").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorFlete").SummaryItem.DisplayFormat = "Flete: {0:n6}"

            gvDetalleDocIngreso.Columns("ValorFlete").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorFlete").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorTotal"

            Dim ColValorTotal As New GridColumn With {
                .FieldName = "Total",
                .Caption = "Valor Total",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtTotalGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorTotal.OptionsColumn.AllowEdit = False
            ColValorTotal.DisplayFormat.FormatType = FormatType.Numeric
            ColValorTotal.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColValorTotal)

            gvDetalleDocIngreso.Columns("Total").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("Total").SummaryItem.DisplayFormat = "Total: {0:n6}"

            gvDetalleDocIngreso.Columns("Total").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Total").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - IsNew"

            Dim ColIsNew As New GridColumn With {
                .FieldName = "IsNew",
                .Caption = "IsNew",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColIsNew.Visible = False
            ColIsNew.OptionsColumn.AllowEdit = False
            ColIsNew.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleDocIngreso.Columns.Add(ColIsNew)

            ColIndexAux += 1

#End Region

#Region "Columna - IdOrdenCompraDetPadre"

            Dim ColIdOrdenCompraDetPadre As New GridColumn With {
                .FieldName = "IdOrdenCompraDetPadre",
                .Caption = "IdOrdenCompraDetPadre",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColIdOrdenCompraDetPadre.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColIdOrdenCompraDetPadre)

            ColIndexAux += 1

#End Region

#Region "Columna - ControlPeso"

            Dim ColControlPeso As New GridColumn With {
                .FieldName = "ControlPeso",
                .Caption = "ControlPeso",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColControlPeso.Visible = False
            ColControlPeso.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleDocIngreso.Columns.Add(ColControlPeso)

            ColIndexAux += 1

#End Region

#Region "Columna - PesoReferenciaUMBas"

            Dim ColPesoRefUMBas As New GridColumn With {
                .FieldName = "PesoReferenciaUMBas",
                .Caption = "PesoReferenciaUMBas",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColControlPeso.Visible = False
            ColControlPeso.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleDocIngreso.Columns.Add(ColControlPeso)

            ColIndexAux += 1

#End Region

#Region "Columna - IdOrdenCompraDet"

            Dim ColIdOrdenCompraDet As New GridColumn With {
                .FieldName = "ColIdOrdenCompraDet",
                .Caption = "ColIdOrdenCompraDet",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColIdOrdenCompraDet.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColIdOrdenCompraDet)

            ColIndexAux += 1

#End Region

#Region "Columna - NombrePropietario"

            Dim ColNombrePropietario As New GridColumn With {
                .FieldName = "NombrePropietario",
                .Caption = "Propietario",
                .Visible = False,
                .Width = 100,
                .ColumnEdit = txtNoLineaGrid,
                .VisibleIndex = 1
            }

            ColNombrePropietario.OptionsColumn.AllowEdit = False
            ColNombrePropietario.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColNombrePropietario)


#End Region

            gvDetalleDocIngreso.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private lOCDet As New List(Of clsBeTrans_oc_det)

    Private Sub Cargar_Detalle_OC()

        Try

            DTGridDetalleDocIngresos.Clear()
            lOCDet = BeTransOcEnc.DetalleOC
            gBeOrdenCompra.DetalleOC = BeTransOcEnc.DetalleOC

            Dim i As Integer = -1

            PresentacionGridLookUpEdit.DataSource = clsLnProducto_presentacion.Get_All_By_IdBodega(cmbBodega.EditValue)

            For Each BeTransOCDet As clsBeTrans_oc_det In lOCDet

                Dim vCantidadPendiente As Double = Math.Round(BeTransOCDet.Cantidad_recibida - BeTransOCDet.Cantidad, 6)

                DTGridDetalleDocIngresos.Rows.Add(BeTransOCDet.IdPropietarioBodega,
                                                  BeTransOCDet.Nombre_Propietario,
                                                  BeTransOCDet.No_Linea,
                                                  BeTransOCDet.IdProductoBodega,
                                                  BeTransOCDet.Codigo_Producto,
                                                  BeTransOCDet.Nombre_producto,
                                                  BeTransOCDet.Nombre_unidad_medida_basica,
                                                  BeTransOCDet.IdUnidadMedidaBasica,
                                                  BeTransOCDet.IdPresentacion,
                                                  BeTransOCDet.Arancel.IdArancel,
                                                  BeTransOCDet.IdMotivoDevolucion,
                                                  BeTransOCDet.Cantidad,
                                                  BeTransOCDet.Cantidad_recibida,
                                                  vCantidadPendiente,
                                                  BeTransOCDet.Peso_Bruto,
                                                  BeTransOCDet.Peso_Neto,
                                                  BeTransOCDet.Costo,
                                                  BeTransOCDet.valor_aduana,
                                                  BeTransOCDet.valor_fob,
                                                  BeTransOCDet.valor_iva,
                                                  BeTransOCDet.valor_dai,
                                                  BeTransOCDet.valor_seguro,
                                                  BeTransOCDet.valor_flete,
                                                  BeTransOCDet.Total_linea,
                                                  BeTransOCDet.Producto.IdProducto,
                                                  BeTransOCDet.IsNew,
                                                  BeTransOCDet.IdOrdenCompraEnc,
                                                  BeTransOCDet.IdOrdenCompraDet,
                                                  False,
                                                  BeTransOCDet.Atributo_variante_1,
                                                  BeTransOCDet.Producto.Kit,
                                                  BeTransOCDet.IdPedidoCompraDet,
                                                  BeTransOCDet.IdOrdenCompraDetPadre,
                                                  BeTransOCDet.Producto.Control_peso,
                                                  BeTransOCDet.Producto.Peso_referencia)

                If BeTransOCDet.lProductosHijosKit.Count > 0 Then

                    For Each Hijo As clsBeTrans_oc_det In BeTransOCDet.lProductosHijosKit

                        vCantidadPendiente = Math.Round(Hijo.Cantidad_recibida - Hijo.Cantidad, 6)

                        DTGridDetalleDocIngresos.Rows.Add(Hijo.IdPropietarioBodega,
                                                          Hijo.Nombre_Propietario,
                                                          Hijo.No_Linea,
                                                          Hijo.IdProductoBodega,
                                                          Hijo.Codigo_Producto,
                                                          Hijo.Nombre_producto,
                                                          Hijo.Nombre_unidad_medida_basica,
                                                          Hijo.IdUnidadMedidaBasica,
                                                          Hijo.IdPresentacion,
                                                          Hijo.Arancel.IdArancel,
                                                          Hijo.IdMotivoDevolucion,
                                                          Hijo.Cantidad,
                                                          Hijo.Cantidad_recibida,
                                                          vCantidadPendiente,
                                                          Hijo.Peso_Bruto,
                                                          Hijo.Peso_Neto,
                                                          Hijo.Costo,
                                                          Hijo.valor_aduana,
                                                          Hijo.valor_fob,
                                                          Hijo.valor_iva,
                                                          Hijo.valor_dai,
                                                          Hijo.valor_seguro,
                                                          Hijo.valor_flete,
                                                          Hijo.Total_linea,
                                                          Hijo.Producto.IdProducto,
                                                          Hijo.IsNew,
                                                          Hijo.IdOrdenCompraEnc,
                                                          Hijo.IdOrdenCompraDet,
                                                          False,
                                                          Hijo.Atributo_variante_1,
                                                          Hijo.Producto.Kit,
                                                          Hijo.IdPedidoCompraDet,
                                                          Hijo.IdOrdenCompraDetPadre,
                                                          Hijo.Producto.Control_peso,
                                                          Hijo.Producto.Peso_referencia)
                    Next

                End If


            Next

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub tmrActualizarDatosRecepcion_Tick(sender As Object, e As EventArgs) Handles tmrActualizarDatosRecepcion.Tick

        Try

            If Modo = TipoTrans.Editar Then

                tmrActualizarDatosRecepcion.Enabled = False

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Actualizando ingreso...")

                gBeRecepcionEnc = clsLnTrans_re_enc.GetSingle(gBeRecepcionEnc.IdRecepcionEnc)

                Cargar_Datos()

                Application.DoEvents()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub frmRecepcion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Cargando = True
        Cargar_Recepcion(False)
        Cargando = False

    End Sub


    Private Sub Cargar_Recepcion(ByVal RecargarObjetoRec)

        Guardar_Recepcion_Inmediata_BOF = False
        Me.AutoScroll = True

        '#GT02112023: cargar bodega si la recepción proviene de una OC para validar lote homologado
        pBodega = AP.Bodega

        '#GT31012024: no creo que sea útil...
        ToolStripButton1.Enabled = False

        DT.Columns.Clear()
        DT.Columns.Add("vence", GetType(Date))
        DT.Columns.Add("lote", GetType(String))

        SuspendLayout()

        Llena_Tipos_Tareas_Que_Necesitan_HH()

        Set_Datata_Table_Grid_Detalle_Documento_Ingreso()

        Set_Datata_Table_Grid_Detalle_Recepcion()

        '#GT16012024: para el nuevo grid de recepción manual
        Set_Datata_Table_Grid_Detalle_Recepcion2() : Set_Datata_Table_Grid_Detalle_Lotes() : Set_Datata_Table_Grid_Detalle_Lotes_Rec()

        Try

            If RecargarObjetoRec Then
                If Not gBeRecepcionEnc Is Nothing Then
                    gBeRecepcionEnc = clsLnTrans_re_enc.GetSingle(gBeRecepcionEnc.IdRecepcionEnc)
                Else
                    Exit Sub
                End If
            End If

            '#GT02022024: validamos si OC/Recepción procede de SAP
            NavConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pBodega.IdBodega, pBodega.IdEmpresa)

            oDateTimePicker = New DateTimePicker
            oDateTimePicker.Format = DateTimePickerFormat.Short
            oDateTimePicker.Visible = False
            oDateTimePicker.Width = 100
            DgridDetalleRec.Controls.Add(oDateTimePicker)

            dtmHoraI.Value = Now
            dtmHoraF.Value = Now.AddHours(1)

            AddHandler oDateTimePicker.ValueChanged, AddressOf oDateTimePicker_ValueChanged
            AddHandler DgridDetalleRec.CellBeginEdit, AddressOf grdListaRecepcion_CellBeginEdit
            AddHandler DgridDetalleRec.CellEndEdit, AddressOf grdListaRecepcion_CellEndEdit

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            '#CKFK20181001: Colocar bodega por defecto.
            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
            cmbBodega.RefreshEditValue()

            Set_Columnas_Grid_Detalle_Documento_Ingreso()

            '#GT16012024: llenar propiedades para el nuevo grid de recepción manual
            Set_Columnas_Grid_Detalle_Documento_Recepcion()

            'DgridOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DgridDetalleRec.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            grdListaFactura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DgridDetalleRec.Columns("FechaVencimiento").DefaultCellStyle.Format = "d"
            DgridDetalleRec.Columns("Peso").DefaultCellStyle.Format = "N4"

            '#EJC20200311
            pListBeStockRec = New List(Of clsBeStock_rec)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lnkTipoT.Enabled = True
                    txtIdTipoTR.Enabled = True

                    lblC.Text = clsLnTrans_re_enc.MaxID()

                    lblEstado.Text = "Nuevo"
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    cmdGuardar.Enabled = True
                    cmdActualizar.Enabled = False
                    cmdEliminar.Enabled = False
                    cmdFinalizar.Enabled = False
                    mnuAsignacion.Enabled = False
                    cmdPrint.Enabled = False
                    cmbBodega.Enabled = True
                    dtmFechaRecepcion.DateTime = Today
                    dtmFechaTarea.DateTime = Today

                    txtIdEstadoDefectoRecepcion.Text = ""
                    txtNombreEstado.Text = ""

                    cmdActualizarDetalle.Enabled = False

                    gBeRecepcionEnc = New clsBeTrans_re_enc() With {.IsNew = True}

                    '#EJC20220301: Set BeBodega en Nuevo.
                    Dim pIdBodega As Integer = cmbBodega.EditValue
                    BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)

                    '#GT19062024: si es bodega fiscal y no viene desde una OC, habilitar campos fiscales
                    If BeBodega.Es_Bodega_Fiscal Then
                        grpDatosFiscalSAT.Visible = True
                    End If

                    Valida_Operadores()

                    If pRecepcionInmediata Then
                        cmbBodega.EditValue = IdBodega
                        CargaOCInmediata()
                    End If

                    If pIdPropietarioBodega <> 0 Then
                        cmbPropietario.EditValue = pIdPropietarioBodega
                    End If

                    Cargar_Ubicacion_Defecto_Recepcion()
                    Actualizar_LookupGridEstadoProducto()

                    '#CKFK20240214 Agregué esta funcionalidad para que se pueda configurar el Tipo de Transacción por defecto desde la bodega
                    txtIdTipoTR.Text = AP.Bodega.IdTipoTransaccion

                    '#CKFK20240214 Agregué la funcionalidad de que se guarde la recepción cuando sea este tipo de transacción MCOC00
                    If txtIdTipoTR.Text = "MCOC00" Then

                        chkRecepcionManual.Checked = True

                        '#CKFK 20210624 Se llama a la función creada por EJC para habilitar o no el stock basado en las reglas del propietario
                        Check_Reglas_Propietario_Ingreso()

                        chkHabilitaStock.Checked = True

                        If txtIdOrdenCompra.Text <> "" Then

                            '#GT08022024: guardar el encabezado al recibir por BOF.
                            Guardar_Recepcion_Inmediata_BOF = True
                            Guardar_Recepcion_Inmediata()

                            If chkDI2REC.Checked Then

                                If XtraMessageBox.Show("¿Convertir el documento de ingreso en recepción con valores por defecto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                                    Dim lDetSample As New List(Of clsBeTrans_re_det)
                                    lDetSample = clsLnTrans_re_det.DI_To_Rec(lblC.Text,
                                                                             gBeOrdenCompra.IdOrdenCompraEnc,
                                                                             AP.UsuarioAp.IdUsuario)

                                    Cargar_Detalle_Recepcion(lDetSample)

                                End If

                            End If

                        End If

                    End If

                Case TipoTrans.Editar

                    Modo_Edicion()

                    Cargar_Datos()

                    '#GT26092023: Valida si nuevo viene desde OC con control_poliza, o es edit de uno existente con control_poliza
                    'grpDatosFiscalSAT.Visible = Control_Poliza

                    '#EJC202405122223 Agregado por número de contenedor en Cumbre
                    grpDatosFiscalSAT.Visible = (txtNoContenedor.Text.Trim <> "" OrElse Control_Poliza)

            End Select

            'If BeTransOcEnc.No_Ticket_TMS <> "0") OrElse BeTransOcEnc.No_Ticket_TMS <> "" Then
            If Not BeTransOcEnc.No_Ticket_TMS.Equals("0") AndAlso Not BeTransOcEnc.No_Ticket_TMS.Equals("") Then

                pBeTms_ticket = clsLnTms_ticket.Get_Ticket_By_Id(BeTransOcEnc.No_Ticket_TMS)

                pBeEmpresa_Transporte_Piloto = clsLnEmpresa_transporte_pilotos.Get_By_IdPiloto(pBeTms_ticket.IdPiloto)
                pBeEmpresa_Transporte_Vehiculo = clsLnEmpresa_transporte_vehiculos.Get_Single_By_IdVehiculo(pBeTms_ticket.IdVehiculo)


                If pBeEmpresa_Transporte_Piloto IsNot Nothing AndAlso pBeEmpresa_Transporte_Piloto.IdPiloto > 0 Then
                    txtIdPiloto.Text = pBeEmpresa_Transporte_Piloto.IdPiloto
                    txtNombrePiloto.Text = Trim(String.Format("{0} {1}", pBeEmpresa_Transporte_Piloto.Nombres, pBeEmpresa_Transporte_Piloto.Apellidos))
                    txtIdPiloto.Enabled = False
                Else
                    txtIdPiloto.Focus() : txtIdPiloto.SelectAll()
                End If

                If pBeEmpresa_Transporte_Vehiculo IsNot Nothing AndAlso pBeEmpresa_Transporte_Vehiculo.IdVehiculo > 0 Then
                    txtIdVehiculo.Text = pBeEmpresa_Transporte_Vehiculo.IdVehiculo
                    txtNombreVehiculo.Text = Trim(String.Format("{0} - {1} - {2}", pBeEmpresa_Transporte_Vehiculo.Marca, pBeEmpresa_Transporte_Vehiculo.Modelo, pBeEmpresa_Transporte_Vehiculo.Tipo))
                    txtIdVehiculo.Enabled = False
                Else
                    txtIdVehiculo.Focus() : txtIdVehiculo.SelectAll()
                End If

            End If

            DgridDetalleRec.Focus()
            DgridDetalleRec.Rows(0).Selected = True

            ResumeLayout()

            txtIdTipoTR.SelectAll()
            txtIdTipoTR.Focus()

            '#EJC20210222: Si se envió el operador desde el documento de ingreso, no preguntar si se quiere guardar.
            If IdOperadorBodegaDefecto <> 0 Then
                Guardar_Recepcion(False)
            End If

            '#GT09032023: bloquear bodega para no mezclar ingresos de distintas bodegas
            cmbBodega.Enabled = False

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub
    Private Sub Modo_Edicion()


        lnkTipoT.Enabled = False
        txtIdTipoTR.Enabled = False

        cmdGuardar.Enabled = False
        cmdActualizar.Enabled = True
        cmdFinalizar.Enabled = True
        mnuEliminar.Enabled = True
        mnuAsignacion.Enabled = True
        cmdPrint.Enabled = True
        cmdActualizarDetalle.Enabled = True

        cmdFinalizar.Enabled = IIf(clsLnMenu_rol.Permiso_Funcionalidad("2.1.1.3", AP.IdRol), True, False)

    End Sub

    Private DTTransReDet As New DataTable
    Private Sub Set_Datata_Table_Grid_Detalle_Recepcion()

        DTTransReDet.Columns.Clear()

        ' Adición de columnas al DataTable según el esquema proporcionado
        DTTransReDet.Columns.Add("IdRecepcionDet", GetType(Integer))
        DTTransReDet.Columns.Add("IdRecepcionEnc", GetType(Integer))
        DTTransReDet.Columns.Add("IdProductoBodega", GetType(Integer))
        DTTransReDet.Columns.Add("IdPresentacion", GetType(Integer))
        DTTransReDet.Columns.Add("IdUnidadMedida", GetType(Integer))
        DTTransReDet.Columns.Add("IdProductoEstado", GetType(Integer))
        DTTransReDet.Columns.Add("IdOperadorBodega", GetType(Integer))
        DTTransReDet.Columns.Add("IdMotivoDevolucion", GetType(Integer))
        DTTransReDet.Columns.Add("No_Linea", GetType(Integer))
        DTTransReDet.Columns.Add("cantidad_recibida", GetType(Double))
        DTTransReDet.Columns.Add("nombre_producto", GetType(String))
        DTTransReDet.Columns.Add("nombre_presentacion", GetType(String))
        DTTransReDet.Columns.Add("nombre_unidad_medida", GetType(String))
        DTTransReDet.Columns.Add("nombre_producto_estado", GetType(String))
        DTTransReDet.Columns.Add("lote", GetType(String))
        DTTransReDet.Columns.Add("fecha_vence", GetType(Date))
        DTTransReDet.Columns.Add("fecha_ingreso", GetType(Date))
        DTTransReDet.Columns.Add("peso", GetType(Double))
        DTTransReDet.Columns.Add("peso_estadistico", GetType(Double))
        DTTransReDet.Columns.Add("peso_minimo", GetType(Double))
        DTTransReDet.Columns.Add("peso_maximo", GetType(Double))
        DTTransReDet.Columns.Add("peso_unitario", GetType(Double))
        DTTransReDet.Columns.Add("user_agr", GetType(String))
        DTTransReDet.Columns.Add("fec_agr", GetType(Date))
        DTTransReDet.Columns.Add("observacion", GetType(String))
        DTTransReDet.Columns.Add("añada", GetType(Integer))
        DTTransReDet.Columns.Add("costo", GetType(Double))
        DTTransReDet.Columns.Add("costo_oc", GetType(Double))
        DTTransReDet.Columns.Add("costo_estadistico", GetType(Double))
        DTTransReDet.Columns.Add("atributo_variante_1", GetType(String))
        DTTransReDet.Columns.Add("codigo_producto", GetType(String))
        DTTransReDet.Columns.Add("lic_plate", GetType(String))
        DTTransReDet.Columns.Add("pallet_no_estandar", GetType(Boolean))
        DTTransReDet.Columns.Add("IdOrdenCompraEnc", GetType(Integer))
        DTTransReDet.Columns.Add("IdOrdenCompraDet", GetType(Integer))
        DTTransReDet.Columns.Add("IdJornadaSistema", GetType(Integer))

    End Sub


    '#GT19012024: hacia abajo es codigo nuevo para recibir en el nuevo GRID.
    '#GT16012024: seteamos las columnas en el nuevo grid de recepcion manual

    Private DTTransReDet2 As New DataTable
    Private Sub Set_Datata_Table_Grid_Detalle_Recepcion2()

        DTTransReDet2.Columns.Clear()

        ' Adición de columnas al DataTable según el esquema proporcionado
        DTTransReDet2.Columns.Add("IdRecepcionDet", GetType(Integer))
        DTTransReDet2.Columns.Add("IdRecepcionEnc", GetType(Integer))
        DTTransReDet2.Columns.Add("IdProductoBodega", GetType(Integer))
        DTTransReDet2.Columns.Add("IdPresentacion", GetType(Integer))
        DTTransReDet2.Columns.Add("IdUnidadMedida", GetType(Integer))
        DTTransReDet2.Columns.Add("IdProductoEstado", GetType(Integer))
        DTTransReDet2.Columns.Add("IdOperadorBodega", GetType(Integer))
        DTTransReDet2.Columns.Add("IdMotivoDevolucion", GetType(Integer))
        DTTransReDet2.Columns.Add("No_Linea", GetType(Integer))
        DTTransReDet2.Columns.Add("cantidad_solicitada", GetType(Double))
        DTTransReDet2.Columns.Add("cantidad_recibida", GetType(Double))
        DTTransReDet2.Columns.Add("cantidad_pendiente", GetType(Double))
        DTTransReDet2.Columns.Add("nombre_producto", GetType(String))
        DTTransReDet2.Columns.Add("nombre_presentacion", GetType(String))
        DTTransReDet2.Columns.Add("nombre_unidad_medida", GetType(String))
        DTTransReDet2.Columns.Add("nombre_producto_estado", GetType(String))
        DTTransReDet2.Columns.Add("lote", GetType(String))
        DTTransReDet2.Columns.Add("fecha_vence", GetType(Date))
        DTTransReDet2.Columns.Add("fecha_ingreso", GetType(Date))
        DTTransReDet2.Columns.Add("peso", GetType(Double))
        DTTransReDet2.Columns.Add("peso_estadistico", GetType(Double))
        DTTransReDet2.Columns.Add("peso_minimo", GetType(Double))
        DTTransReDet2.Columns.Add("peso_maximo", GetType(Double))
        DTTransReDet2.Columns.Add("peso_unitario", GetType(Double))
        DTTransReDet2.Columns.Add("user_agr", GetType(String))
        DTTransReDet2.Columns.Add("fec_agr", GetType(Date))
        DTTransReDet2.Columns.Add("observacion", GetType(String))
        DTTransReDet2.Columns.Add("añada", GetType(Integer))
        DTTransReDet2.Columns.Add("costo", GetType(Double))
        DTTransReDet2.Columns.Add("costo_oc", GetType(Double))
        DTTransReDet2.Columns.Add("costo_estadistico", GetType(Double))
        DTTransReDet2.Columns.Add("atributo_variante_1", GetType(String))
        DTTransReDet2.Columns.Add("codigo_producto", GetType(String))
        DTTransReDet2.Columns.Add("lic_plate", GetType(String))
        DTTransReDet2.Columns.Add("pallet_no_estandar", GetType(Boolean))
        DTTransReDet2.Columns.Add("IdOrdenCompraEnc", GetType(Integer))
        DTTransReDet2.Columns.Add("IdOrdenCompraDet", GetType(Integer))
        DTTransReDet2.Columns.Add("IdJornadaSistema", GetType(Integer))
        DTTransReDet2.Columns.Add("Total", GetType(Double))
        DTTransReDet2.Columns.Add("IsNewR", GetType(Boolean))
        DTTransReDet2.Columns.Add("Bono", GetType(Boolean))

    End Sub

    Private DTLotesDocumentoIngreso As New DataTable()
    Private DTLotesDocumentoRecepcion As New DataTable()
    Private Sub Set_Datata_Table_Grid_Detalle_Lotes()

        DTLotesDocumentoIngreso.Columns.Clear()
        DTLotesDocumentoIngreso.Columns.Add("No_Linea", GetType(Integer))
        DTLotesDocumentoIngreso.Columns.Add("lote", GetType(String))
        DTLotesDocumentoIngreso.Columns.Add("fecha_vence", GetType(Date))

    End Sub

    Private Sub Set_Datata_Table_Grid_Detalle_Lotes_Rec()

        DTLotesDocumentoRecepcion.Columns.Clear()
        DTLotesDocumentoRecepcion.Columns.Add("No_Linea", GetType(Integer))
        DTLotesDocumentoRecepcion.Columns.Add("lote", GetType(String))
        DTLotesDocumentoRecepcion.Columns.Add("fecha_vence", GetType(Date))

    End Sub

    'Private Sub ProductoGridLookUpEditRec_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)

    '    Try

    '        ' Asigna el control a una variable para un mejor manejo
    '        Dim lookUpEdit As LookUpEdit = CType(sender, LookUpEdit)

    '        ' Verifica si el valor seleccionado es nulo o DBNull
    '        If lookUpEdit.EditValue Is Nothing OrElse lookUpEdit.EditValue Is DBNull.Value Then
    '            gvDetalleRec2.CancelUpdateCurrentRow()
    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub
    Private Sub Set_Columnas_Grid_Detalle_Documento_Recepcion()

        Try

            DgridDetalleRec2.DataSource = DTTransReDet2

            Dim ColIndexAux As Integer = 0

            gvDetalleRec2.OptionsView.ShowFooter = True
            gvDetalleRec2.OptionsView.ShowGroupPanel = False
            gvDetalleRec2.OptionsView.ColumnAutoWidth = False
            gvDetalleRec2.Columns.Clear()

#Region "Columna - No_Linea"

            Dim ColNoLinea As New GridColumn With {
                .FieldName = "No_Linea",
                .Caption = "No. Linea",
                .Visible = True,
                .Width = 80,
                .ColumnEdit = txtNoLineaGrid,
                .VisibleIndex = ColIndexAux
            }

            ColNoLinea.OptionsColumn.AllowEdit = False
            ColNoLinea.Fixed = FixedStyle.Left
            gvDetalleRec2.Columns.Add(ColNoLinea)

            gvDetalleRec2.Columns("No_Linea").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            gvDetalleRec2.Columns("No_Linea").SummaryItem.DisplayFormat = "Registros: {0:n0}"
            gvDetalleRec2.Columns("No_Linea").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleRec2.Columns("No_Linea").DisplayFormat.FormatString = "{0:n0}"

            ColIndexAux += 1

#End Region

#Region "Columna - IdProductoBodega"

            ProductoGridLookUpEditRec.View.Columns.Clear()

            ProductoGridLookUpEditRec.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdProductoBodega", .Caption = "IdProductoBodega", .Visible = False},
                New GridColumn With {.FieldName = "No_Linea", .Caption = "Línea", .Visible = True, .Width = 80},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True, .Width = 100},
                New GridColumn With {.FieldName = "CodigoBarra", .Caption = "CodigoBarra", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True, .Width = 200},
                New GridColumn With {.FieldName = "UMBas", .Caption = "UMBas", .Visible = True},
                New GridColumn With {.FieldName = "IdUmBas", .Caption = "IdUmBas", .Visible = False},
                New GridColumn With {.FieldName = "ControlPeso", .Caption = "ControlPeso", .Visible = False},
                New GridColumn With {.FieldName = "Presentacion", .Caption = "Presentación", .Visible = True}})

            ProductoGridLookUpEditRec.ValueMember = "No_Linea"
            ProductoGridLookUpEditRec.DisplayMember = "Codigo"
            ProductoGridLookUpEditRec.NullText = "-> Producto"
            ProductoGridLookUpEditRec.PopupFormWidth = 1000
            ProductoGridLookUpEditRec.ImmediatePopup = True

            '#EJC20240122
            If (txtIdTipoTR.Text = clsDataContractDI.tTipo_Rec.HCOC00.ToString()) OrElse (gBeRecepcionEnc.IdTipoTransaccion = clsDataContractDI.tTipo_Rec.HSOC00.ToString()) Then
                If Not gBeRecepcionEnc Is Nothing Then
                    If Not gBeRecepcionEnc.OrdenCompraRec Is Nothing Then
                        If Not gBeRecepcionEnc.OrdenCompraRec.OC Is Nothing Then
                            ProductoGridLookUpEditRec.DataSource = clsLnProducto.Get_Lista_By_IdOrdenCompraEnc(gBeRecepcionEnc.OrdenCompraRec.OC.IdOrdenCompraEnc, cmbBodega.EditValue)
                        End If
                    End If
                End If
            Else
                If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then
                    If gBeRecepcionEnc.OrdenCompraRec.OC.IdOrdenCompraEnc > 0 Then
                        ProductoGridLookUpEditRec.DataSource = clsLnProducto.Get_Lista_By_IdOrdenCompraEnc(gBeRecepcionEnc.OrdenCompraRec.OC.IdOrdenCompraEnc, cmbBodega.EditValue)
                    End If
                End If
            End If

            ProductoGridLookUpEditRec.TextEditStyle = TextEditStyles.Standard
            ProductoGridLookUpEditRec.SearchMode = SearchMode.AutoSuggest
            ProductoGridLookUpEditRec.View.OptionsFind.AlwaysVisible = True
            ProductoGridLookUpEditRec.View.OptionsFind.FindMode = FindMode.Always
            ProductoGridLookUpEditRec.View.OptionsFind.SearchInPreview = False
            ProductoGridLookUpEditRec.View.OptionsFind.FindFilterColumns = "*"
            ProductoGridLookUpEditRec.View.BestFitColumns()

            RemoveHandler ProductoGridLookUpEditRec.Leave, AddressOf ProductoGridLookUpEditRec_Leave
            AddHandler ProductoGridLookUpEditRec.Leave, AddressOf ProductoGridLookUpEditRec_Leave

            RemoveHandler ProductoGridLookUpEditRec.KeyDown, AddressOf ProductoGridLookUpEditRec_KeyDown
            AddHandler ProductoGridLookUpEditRec.KeyDown, AddressOf ProductoGridLookUpEditRec_KeyDown

            RemoveHandler ProductoGridLookUpEditRec.ProcessNewValue, AddressOf ProductoGridLookUpEditRec_ProcessNewValue
            AddHandler ProductoGridLookUpEditRec.ProcessNewValue, AddressOf ProductoGridLookUpEditRec_ProcessNewValue

            'RemoveHandler ProductoGridLookUpEditRec.View.CustomRowFilter, AddressOf ProductoGridLookUpEditRecView_CustomRowFilter
            'AddHandler ProductoGridLookUpEditRec.View.CustomRowFilter, AddressOf ProductoGridLookUpEditRecView_CustomRowFilter

            ProductoGridLookUpEditRec.View.OptionsView.ShowAutoFilterRow = True

            Dim ColIdProductoBodega As New GridColumn With {
                .FieldName = "No_Linea",
                .Caption = "Código",
                .Visible = True,
                .VisibleIndex = ColIndexAux,
                .ColumnEdit = ProductoGridLookUpEditRec
            }

            '#EJC20210306: Permitir el ingreso de valores que no estén en la lista.
            ProductoGridLookUpEditRec.AcceptEditorTextAsNewValue = DefaultBoolean.True

            '#GT03102023: cuando no sea nuevo, evitar que cambien el producto
            'ColIdProductoBodega.OptionsColumn.AllowEdit = True
            Select Case Modo
                Case TipoTrans.Nuevo
                    ColIdProductoBodega.OptionsColumn.AllowEdit = True
                Case TipoTrans.Editar

                    If Not gBeRecepcionEnc Is Nothing Then
                        If gBeRecepcionEnc.Estado.ToUpper = "CERRADO" Or gBeRecepcionEnc.Estado.ToUpper = "ANULADO" Then
                            ColIdProductoBodega.OptionsColumn.AllowEdit = False
                        Else
                            ColIdProductoBodega.OptionsColumn.AllowEdit = True
                        End If
                    End If

            End Select

            ColIdProductoBodega.Width = 150
            ColIdProductoBodega.Fixed = FixedStyle.Left
            gvDetalleRec2.Columns.Add(ColIdProductoBodega)

            ColIndexAux += 1

#End Region

#Region "Columna - CodigoProducto"

            Dim ColCodigoProducto As New GridColumn With {
                .FieldName = "CodigoProducto",
                .Caption = "CodigoProducto",
                .Width = 100,
                .VisibleIndex = ColIndexAux
            }

            ColCodigoProducto.Visible = False
            gvDetalleRec2.Columns.Add(ColCodigoProducto)

            ColIndexAux += 1
#End Region

#Region "Columna - nombre_producto"

            Dim ColNombreProducto As New GridColumn With {
                .FieldName = "nombre_producto",
                .Caption = "Nombre",
                .Visible = True,
                .VisibleIndex = ColIndexAux,
                .Width = 400
            }

            ColNombreProducto.OptionsColumn.AllowEdit = False

            gvDetalleRec2.Columns.Add(ColNombreProducto)

            ColIndexAux += 1

#End Region

#Region "Columna - Presentacion"

            PresentacionGridLookUpEdit.View.Columns.Clear()

            PresentacionGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdPresentacion", .Caption = "IdPresentacion", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            PresentacionGridLookUpEdit.ValueMember = "IdPresentacion"
            PresentacionGridLookUpEdit.DisplayMember = "Nombre"
            PresentacionGridLookUpEdit.NullText = ""
            PresentacionGridLookUpEdit.DataSource = clsLnProducto_presentacion.Get_All_By_IdBodega(cmbBodega.EditValue)
            PresentacionGridLookUpEdit.View.BestFitColumns()
            PresentacionGridLookUpEdit.PopupFormWidth = 700

            RemoveHandler PresentacionGridLookUpEdit.Leave, AddressOf PresentacionGridLookUpEdit_Leave
            AddHandler PresentacionGridLookUpEdit.Leave, AddressOf PresentacionGridLookUpEdit_Leave

            PresentacionGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColPresentacion As New GridColumn With {
                .FieldName = "IdPresentacion",
                .Caption = "Presentacion",
                .Visible = True,
                .VisibleIndex = ColIndexAux,
                .Width = 100,
                .ColumnEdit = PresentacionGridLookUpEdit
            }

            Select Case Modo
                Case TipoTrans.Nuevo
                    ColPresentacion.OptionsColumn.AllowEdit = True
                Case TipoTrans.Editar
                    If Not gBeRecepcionEnc Is Nothing Then
                        If gBeRecepcionEnc.Estado.ToUpper = "CERRADO" Or gBeRecepcionEnc.Estado.ToUpper = "ANULADO" Then
                            ColPresentacion.OptionsColumn.AllowEdit = False
                        Else
                            ColPresentacion.OptionsColumn.AllowEdit = True
                        End If
                    Else
                        ColPresentacion.OptionsColumn.AllowEdit = True
                    End If

            End Select

            gvDetalleRec2.Columns.Add(ColPresentacion)

            ColIndexAux += 1

#End Region

#Region "Columna - nombre_unidad_medida"


            Dim ColUMBas As New GridColumn With {
                .FieldName = "nombre_unidad_medida",
                .Caption = "UMBas",
                .Visible = True,
                .VisibleIndex = ColIndexAux,
                .Width = 120
            }

            gvDetalleRec2.Columns.Add(ColUMBas)

            ColIndexAux += 1

#End Region

#Region "Columna - IdUnidadMedida"

            Dim ColIdUmBas As New GridColumn With {
                .FieldName = "IdUnidadMedida",
                .Caption = "IdUmBas",
                .VisibleIndex = ColIndexAux,
                .Width = 75,
                .ColumnEdit = IdUmBasGridLookUpEdit
            }

            ColIdUmBas.Visible = False
            gvDetalleRec2.Columns.Add(ColIdUmBas)

            ColIndexAux += 1

#End Region

#Region "Columna - cantidad_solicitada"

            Dim ColCantidadSolicitada As New GridColumn With {
                .FieldName = "cantidad_solicitada",
                .Caption = "Solicitado",
                .Visible = True,
                .Width = 150,
                 .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidadSolicitada.OptionsColumn.AllowEdit = False
            ColCantidadSolicitada.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidadSolicitada.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleRec2.Columns.Add(ColCantidadSolicitada)

            ColIndexAux += 1
#End Region

#Region "Columna - cantidad_recibida"

            RemoveHandler txtCantidadGrid.ValueChanged, AddressOf txtCantidadGrid_ValueChanged
            AddHandler txtCantidadGrid.ValueChanged, AddressOf txtCantidadGrid_ValueChanged

            Dim ColCantidad As New GridColumn With {
                .FieldName = "cantidad_recibida",
                .Caption = "Cantidad",
                .Visible = True,
                .Width = 150,
                 .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidad.OptionsColumn.AllowEdit = True
            ColCantidad.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidad.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleRec2.Columns.Add(ColCantidad)

            gvDetalleRec2.Columns("cantidad_recibida").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleRec2.Columns("cantidad_recibida").SummaryItem.DisplayFormat = "Recibido: {0:n6}"
            gvDetalleRec2.Columns("cantidad_recibida").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleRec2.Columns("cantidad_recibida").DisplayFormat.FormatString = "{0:n6}"
            ColIndexAux += 1
#End Region

#Region "Columna - cantidad_pendiente"

            Dim ColCantidadPendiente As New GridColumn With {
                .FieldName = "cantidad_pendiente",
                .Caption = "Pendiente",
                .Visible = True,
                .Width = 150,
                 .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidadPendiente.OptionsColumn.AllowEdit = False
            ColCantidadPendiente.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidadPendiente.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleRec2.Columns.Add(ColCantidadPendiente)
            ColIndexAux += 1
#End Region

#Region "Columna - peso"

            Dim ColControlPeso As New GridColumn With {
                .FieldName = "peso",
                .Caption = "Peso",
                .Width = 80,
                .VisibleIndex = ColIndexAux
            }

            Select Case Modo
                Case TipoTrans.Nuevo
                    ColControlPeso.OptionsColumn.AllowEdit = True
                Case TipoTrans.Editar
                    ColControlPeso.OptionsColumn.AllowEdit = False
            End Select

            ColControlPeso.Visible = True
            ColControlPeso.DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleRec2.Columns.Add(ColControlPeso)

            ColIndexAux += 1

#End Region

#Region "Columna - costo_oc"

            Dim ColCosto As New GridColumn With {
                .FieldName = "costo_oc",
                .Caption = "Costo OC",
                .Width = 100,
                 .ColumnEdit = txtCostoOCGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCosto.Visible = False
            ColCosto.OptionsColumn.AllowEdit = False
            ColCosto.DisplayFormat.FormatType = FormatType.Numeric
            ColCosto.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleRec2.Columns.Add(ColCosto)

            gvDetalleRec2.Columns("costo_oc").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleRec2.Columns("costo_oc").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1
#End Region

#Region "Columna - costo"

            Dim ColCostoReal As New GridColumn With {
                .FieldName = "costo",
                .Caption = "Costo Real",
                .Width = 100,
                 .ColumnEdit = txtCostoGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCostoReal.Visible = False
            ColCostoReal.OptionsColumn.AllowEdit = False
            ColCostoReal.DisplayFormat.FormatType = FormatType.Numeric
            ColCostoReal.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleRec2.Columns.Add(ColCostoReal)
            ColIndexAux += 1

#End Region

#Region "Columna - Total"

            Dim ColTotal As New GridColumn With {
                .FieldName = "Total",
                .Caption = "Total",
                .Width = 100,
                 .ColumnEdit = txtTotalGrid,
                .VisibleIndex = ColIndexAux
            }

            ColTotal.Visible = False
            ColTotal.OptionsColumn.AllowEdit = False
            ColTotal.DisplayFormat.FormatType = FormatType.Numeric
            ColTotal.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleRec2.Columns.Add(ColTotal)
            ColIndexAux += 1

#End Region

#Region "Columna - lote"

            Dim ColLote As New GridColumn

            If Not gBeRecepcionEnc Is Nothing AndAlso gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then

                If gBeRecepcionEnc.OrdenCompraRec.OC.DetalleLotes.Count > 0 Then

                    ProductoLoteGridLookUpEdit.View.Columns.Clear()

                    ProductoLoteGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                    New GridColumn With {.FieldName = "No_Linea", .Caption = "Línea", .Visible = True},
                    New GridColumn With {.FieldName = "lote", .Caption = "Lote", .Visible = True},
                    New GridColumn With {.FieldName = "fecha_vence", .Caption = "Vence", .Visible = True}
                })

                    ProductoLoteGridLookUpEdit.ValueMember = "lote"
                    ProductoLoteGridLookUpEdit.DisplayMember = "lote"
                    ProductoLoteGridLookUpEdit.NullText = ""

                    Dim lotesPorProducto = (From lote In gBeRecepcionEnc.OrdenCompraRec.OC.DetalleLotes
                                            Select New With {.No_Linea = lote.No_linea, .lote = lote.Lote, .fecha_vence = lote.Fecha_vence}).Distinct().ToList()

                    DTLotesDocumentoIngreso.Rows.Clear()

                    For Each item In lotesPorProducto
                        Dim row As DataRow = DTLotesDocumentoIngreso.NewRow()
                        row("No_Linea") = item.No_Linea
                        row("lote") = item.lote
                        row("fecha_vence") = item.fecha_vence
                        DTLotesDocumentoIngreso.Rows.Add(row)
                    Next

                    Dim lotesPorProductoRec = (From lote In gBeRecepcionEnc.Detalle
                                               Select New With {.No_Linea = lote.No_Linea, .lote = lote.Lote, .fecha_vence = lote.Fecha_vence}).
                                           Distinct().ToList

                    For Each item In lotesPorProductoRec
                        Dim row As DataRow = DTLotesDocumentoRecepcion.NewRow()
                        row("No_Linea") = item.No_Linea
                        row("lote") = item.lote
                        row("fecha_vence") = item.fecha_vence
                        DTLotesDocumentoRecepcion.Rows.Add(row)
                    Next

                    DTLotesDocumentoIngreso.Merge(DTLotesDocumentoRecepcion)

                    Dim view As DataView = DTLotesDocumentoRecepcion.DefaultView
                    Dim distinctValues As DataTable = view.ToTable(True, "No_Linea", "lote", "fecha_vence")
                    DTLotesDocumentoIngreso = distinctValues

                    ProductoLoteGridLookUpEdit.DataSource = DTLotesDocumentoIngreso

                    ProductoLoteGridLookUpEdit.TextEditStyle = TextEditStyles.Standard
                    ProductoLoteGridLookUpEdit.SearchMode = SearchMode.AutoSuggest
                    ProductoLoteGridLookUpEdit.View.OptionsFind.AlwaysVisible = True
                    ProductoLoteGridLookUpEdit.View.OptionsFind.FindMode = FindMode.Always
                    ProductoLoteGridLookUpEdit.View.OptionsFind.SearchInPreview = False
                    ProductoLoteGridLookUpEdit.View.OptionsFind.FindFilterColumns = "No_Linea"
                    ProductoLoteGridLookUpEdit.View.BestFitColumns()
                    ProductoLoteGridLookUpEdit.PopupFormWidth = 400

                    RemoveHandler ProductoLoteGridLookUpEdit.Leave, AddressOf ProductoLoteGridLookUpEdit_Leave
                    AddHandler ProductoLoteGridLookUpEdit.Leave, AddressOf ProductoLoteGridLookUpEdit_Leave

                    RemoveHandler ProductoLoteGridLookUpEdit.ProcessNewValue, AddressOf ProductoLoteGridLookUpEdit_ProcessNewValue
                    AddHandler ProductoLoteGridLookUpEdit.ProcessNewValue, AddressOf ProductoLoteGridLookUpEdit_ProcessNewValue

                    RemoveHandler ProductoLoteGridLookUpEdit.View.CustomRowFilter, AddressOf ProductoLoteGridLookUpEdit_View_CustomRowFilter
                    AddHandler ProductoLoteGridLookUpEdit.View.CustomRowFilter, AddressOf ProductoLoteGridLookUpEdit_View_CustomRowFilter

                    ProductoLoteGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

                    ColLote.FieldName = "lote"
                    ColLote.Caption = "Lote"
                    ColLote.Visible = True
                    ColLote.VisibleIndex = ColIndexAux
                    ColLote.Width = 100
                    ColLote.ColumnEdit = ProductoLoteGridLookUpEdit

                    Select Case Modo
                        Case TipoTrans.Nuevo
                            ColLote.OptionsColumn.AllowEdit = True
                        Case TipoTrans.Editar
                            If gBeRecepcionEnc.Estado.ToUpper = "CERRADO" Or gBeRecepcionEnc.Estado.ToUpper = "ANULADO" Then
                                ColLote.OptionsColumn.AllowEdit = False
                            Else
                                ColLote.OptionsColumn.AllowEdit = True
                            End If

                    End Select

                    gvDetalleRec2.Columns.Add(ColLote)

                    ColIndexAux += 1

                Else

                    ColLote.FieldName = "lote"
                    ColLote.Caption = "Lote"
                    ColLote.Visible = True
                    ColLote.Width = 100
                    ColLote.ColumnEdit = txtLoteGrid
                    ColLote.VisibleIndex = ColIndexAux
                    ColLote.OptionsColumn.AllowEdit = True

                    AddHandler txtLoteGrid.Validating, AddressOf txtLoteGrid_Validating

                    gvDetalleRec2.Columns.Add(ColLote)
                    ColIndexAux += 1

                End If

            End If

#End Region

#Region "Columna - FechaVencimiento"

            Dim ColFechaVencimiento As New GridColumn With {
                .FieldName = "fecha_vence",
                .Caption = "Vencimiento",
                .Visible = True,
                .Width = 130,
                .ColumnEdit = txtFechaVenceGrid,
                .VisibleIndex = ColIndexAux
            }

            ColFechaVencimiento.OptionsColumn.AllowEdit = True
            gvDetalleRec2.Columns.Add(ColFechaVencimiento)
            ColIndexAux += 1

#End Region

#Region "Columna - IdProductoEstado"

            EstadoGridLookUpEdit.View.Columns.Clear()

            EstadoGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdEstado", .Caption = "Estado", .Visible = True},
                New GridColumn With {.FieldName = "nombre", .Caption = "Nombre", .Visible = True}
            })

            EstadoGridLookUpEdit.ValueMember = "IdEstado"
            EstadoGridLookUpEdit.DisplayMember = "nombre"
            EstadoGridLookUpEdit.NullText = ""
            EstadoGridLookUpEdit.PopupFormWidth = 700


            If (txtIdTipoTR.Text = clsDataContractDI.tTipo_Rec.HCOC00.ToString()) OrElse (gBeRecepcionEnc.IdTipoTransaccion = clsDataContractDI.tTipo_Rec.HSOC00.ToString()) Then
                If Not gBeOrdenCompra Is Nothing Then
                    EstadoGridLookUpEdit.DataSource = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodegaHH(gBeOrdenCompra.PropietarioBodega.IdPropietario, AP.IdBodega)
                End If
            Else
                'GT 08022021 se obtiene el IdRegimen del combo
                Dim fila As Object = cmbPropietario.GetSelectedDataRow
                Dim IdPropietario As Integer = 0
                If fila Is Nothing Then
                    Throw New Exception("Error_20243101_0930: No se pudo obtener un propietario para cargar los estados del producto.")
                Else
                    IdPropietario = fila.Item("IdPropietario")
                End If
                EstadoGridLookUpEdit.DataSource = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodegaHH(IdPropietario, AP.IdBodega)
            End If


            EstadoGridLookUpEdit.View.BestFitColumns()

            RemoveHandler EstadoGridLookUpEdit.Leave, AddressOf EstadoGridLookUpEdit_Leave
            AddHandler EstadoGridLookUpEdit.Leave, AddressOf EstadoGridLookUpEdit_Leave

            EstadoGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColEstadoProducto As New GridColumn With {
                .FieldName = "IdProductoEstado",
                .Caption = "Estado",
                .Visible = True,
                .Width = 110,
                .ColumnEdit = EstadoGridLookUpEdit,
                .VisibleIndex = ColIndexAux
            }

            ColEstadoProducto.OptionsColumn.AllowEdit = True
            ColEstadoProducto.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleRec2.Columns.Add(ColEstadoProducto)
            ColIndexAux += 1

#End Region

#Region "Columna - Observacion"

            Dim ColObservacion As New GridColumn With {
                .FieldName = "observacion",
                .Caption = "Observacíón",
                .Width = 100,
                .ColumnEdit = txtObservacionGrid,
                .VisibleIndex = ColIndexAux
            }

            ColObservacion.Visible = False
            ColObservacion.OptionsColumn.AllowEdit = True
            gvDetalleRec2.Columns.Add(ColObservacion)
            ColIndexAux += 1

#End Region

#Region "Columna - lic_plate"

            Dim ColLicPlate As New GridColumn With {
                .FieldName = "lic_plate",
                .Caption = "Licencia",
                .Visible = True,
                .Width = 170,
                .ColumnEdit = txtLicPlateGrid,
                .VisibleIndex = ColIndexAux
            }


            Select Case Modo
                Case TipoTrans.Nuevo
                    ColLicPlate.OptionsColumn.AllowEdit = True
                Case TipoTrans.Editar
                    ColLicPlate.OptionsColumn.AllowEdit = False
            End Select

            gvDetalleRec2.Columns.Add(ColLicPlate)
            ColIndexAux += 1

#End Region

#Region "Columna - IdOrdenCompraEnc"

            Dim ColIdOrdenCompraEnc As New GridColumn With {
                .FieldName = "IdOrdenCompraEnc",
                .Caption = "ID Orden Compra",
                .Visible = False,
                .Width = 100,
                .ColumnEdit = txtCompraEncGrid,
                .VisibleIndex = ColIndexAux
            }

            ColIdOrdenCompraEnc.Visible = False
            ColIdOrdenCompraEnc.OptionsColumn.AllowEdit = False
            gvDetalleRec2.Columns.Add(ColIdOrdenCompraEnc)
            ColIndexAux += 1

#End Region

#Region "Columna - IdOrdenCompraDet"

            Dim ColIdOrdenCompraDet As New GridColumn With {
                .FieldName = "IdOrdenCompraDet",
                .Caption = "ID Orden Detalle",
                .Visible = False,
                .Width = 100,
                .ColumnEdit = txtCompraEncDetGrid,
                .VisibleIndex = ColIndexAux
            }

            ColIdOrdenCompraDet.Visible = False
            ColIdOrdenCompraDet.OptionsColumn.AllowEdit = False
            gvDetalleRec2.Columns.Add(ColIdOrdenCompraDet)
            ColIndexAux += 1

#End Region

#Region "Columna - IdRecepcionEnc"

            Dim ColIdRecepcionEnc As New GridColumn With {
                .FieldName = "IdRecepcionEnc",
                .Caption = "Recepcion Enc",
                .Visible = False,
                .Width = 100,
                .ColumnEdit = txtCompraEncDetGrid,
                .VisibleIndex = ColIndexAux
            }

            ColIdRecepcionEnc.Visible = False
            ColIdRecepcionEnc.OptionsColumn.AllowEdit = False
            gvDetalleRec2.Columns.Add(ColIdRecepcionEnc)
            ColIndexAux += 1

#End Region

#Region "Columna - IdRecepcionDet"

            Dim ColIdRecepcionDet As New GridColumn With {
                .FieldName = "IdRecepcionDet",
                .Caption = "Recepcion detalle",
                .Visible = False,
                .Width = 100,
                .ColumnEdit = txtCompraEncDetGrid,
                .VisibleIndex = ColIndexAux
            }

            ColIdRecepcionDet.Visible = False
            ColIdRecepcionDet.OptionsColumn.AllowEdit = False
            gvDetalleRec2.Columns.Add(ColIdRecepcionDet)
            ColIndexAux += 1

#End Region

#Region "Columna - atributo_variante_1"

            Dim Colatributo_variante As New GridColumn With {
                .FieldName = "atributo_variante_1",
                .Caption = "atributo variante",
                .Width = 100,
                .ColumnEdit = txtCompraEncDetGrid,
                .VisibleIndex = ColIndexAux
            }

            Colatributo_variante.Visible = False
            Colatributo_variante.OptionsColumn.AllowEdit = False
            gvDetalleRec2.Columns.Add(Colatributo_variante)
            ColIndexAux += 1

#End Region

#Region "Columna - IsNewR"

            Dim ColIsNew As New GridColumn With {
                .FieldName = "IsNewR",
                .Caption = "IsNewR",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColIsNew.Visible = False
            ColIsNew.OptionsColumn.AllowEdit = False
            ColIsNew.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleRec2.Columns.Add(ColIsNew)

            ColIndexAux += 1

#End Region

#Region "Columna - Bono"

            Dim ColBono As New GridColumn With {
                .FieldName = "Bono",
                .Caption = "Bono",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColBono.Visible = True
            ColBono.OptionsColumn.AllowEdit = False
            ColBono.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleRec2.Columns.Add(ColBono)

            ColIndexAux += 1

#End Region

            gvDetalleRec2.OptionsView.NewItemRowPosition = NewItemRowPosition.Top
            gvDetalleRec2.OptionsNavigation.AutoFocusNewRow = True

            For Each col As GridColumn In gvDetalleRec2.Columns
                col.OptionsColumn.AllowSort = DefaultBoolean.False
            Next

            gvDetalleRec2.ClearSorting()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    'Private Sub ProductoGridLookUpEditRecView_CustomRowFilter(sender As Object, e As RowFilterEventArgs)

    '    Try

    '        If _filtroCodigoBarra <> "" Then

    '            Dim view As GridView = CType(sender, GridView)
    '            Dim vCodigoBarra As String = view.GetListSourceRowCellValue(e.ListSourceRow, "CodigoBarra")

    '            If vCodigoBarra <> _filtroCodigoBarra Then
    '                e.Visible = False
    '                e.Handled = True
    '            End If

    '        Else
    '            e.Visible = True
    '            e.Handled = True
    '        End If


    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

    Private Sub ProductoLoteGridLookUpEdit_View_CustomRowFilter(sender As Object, e As RowFilterEventArgs)

        Try

            If gBeRecepcionEnc.OrdenCompraRec.OC.DetalleLotes.Count > 0 Then

                Dim view As ColumnView = CType(sender, ColumnView)
                Dim noLineaActual As Integer = Convert.ToInt32(view.GetListSourceRowCellValue(e.ListSourceRow, "No_Linea"))

                If noLineaActual <> NoLineaActualFilaGrid Then
                    e.Visible = False
                    e.Handled = True
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ProductoLoteGridLookUpEdit_ProcessNewValue(sender As Object, e As ProcessNewValueEventArgs)

        Try

            If e.DisplayValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.DisplayValue.ToString()) Then

                Dim currentView As GridView = DgridDetalleRec2.FocusedView
                Dim vNoLinea As String = ""

                If currentView IsNot Nothing AndAlso currentView.SelectedRowsCount = 1 Then
                    vNoLinea = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "No_Linea")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "No_Linea"))
                End If

                Dim row As DataRow = DTLotesDocumentoIngreso.NewRow()
                row("No_Linea") = vNoLinea
                row("lote") = e.DisplayValue.ToString()
                row("fecha_vence") = Now.AddYears(1)
                DTLotesDocumentoIngreso.Rows.Add(row)

                ProductoLoteGridLookUpEdit.View.RefreshData()

                gvDetalleRec2.SetRowCellValue(gvDetalleRec2.FocusedRowHandle, "lote", e.DisplayValue.ToString())

                ' Asegurarse de que el nuevo valor sea aceptado.
                e.Handled = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ProductoLoteGridLookUpEdit_Leave(sender As Object, e As EventArgs)

        Try

            Dim ListaLotes As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If ListaLotes.EditValue Is Nothing Then Return
            Dim drLineaGrid As DataRow = gvDetalleRec2.GetFocusedDataRow()
            If drLineaGrid Is Nothing Then Return

            Dim vObjLoteGridLookupEdit As DataRowView = TryCast(ListaLotes.Properties.GetRowByKeyValue(ListaLotes.EditValue), DataRowView)

            If Not vObjLoteGridLookupEdit Is Nothing Then

                Dim fechaVenceObj As Object = vObjLoteGridLookupEdit("fecha_vence")

                If fechaVenceObj IsNot DBNull.Value Then

                    Dim fechaVence As Date = New Date(1900, 1, 1)

                    If Date.TryParse(fechaVenceObj.ToString(), fechaVence) Then
                        drLineaGrid("fecha_vence") = fechaVence.Date
                        drLineaGrid("Lote") = ListaLotes.EditValue.ToString().ToUpper()
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtCantidadGrid_ValueChanged(sender As Object, e As EventArgs)

        Try

            Dim View As GridView = CType(DgridDetalleRec2.DefaultView, GridView)
            Dim vCantidadSpinEditor As SpinEdit = CType(sender, SpinEdit)

            If vCantidadSpinEditor Is Nothing Then Return

            Dim ColCantidadRecibida As Double = vCantidadSpinEditor.EditValue
            Dim Cantidad_Recibida As Double = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_recibida")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_recibida"))
            Dim Cantidad_Solicitada As Double = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_solicitada")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_solicitada"))
            Dim Codigo_Producto As String = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "codigo_producto")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "codigo_producto"))
            Dim Costo As Double = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "costo")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "costo"))
            Dim No_Linea As String = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "No_Linea")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "No_Linea"))

            Dim vCantidadRecibidaGrid As Double = Get_Cantidad_Recibidad_Grid2(No_Linea, gvDetalleRec2.FocusedRowHandle)
            'If vCantidadRecibidaGrid = 0 Then vCantidadRecibidaGrid = ColCantidadRecibida

            gvDetalleRec2.SetRowCellValue(gvDetalleRec2.FocusedRowHandle, "Total", Math.Round(Cantidad_Recibida * Costo, 6))
            gvDetalleRec2.SetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_pendiente", Math.Round(Cantidad_Solicitada - (vCantidadRecibidaGrid + ColCantidadRecibida), 6))

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private PopupTieneErrores As Boolean = False

    '' Método hipotético que debes implementar para verificar si el valor está duplicado

    'Private Sub ProductoGridLookUpEditRec_KeyDown(sender As Object, e As KeyEventArgs)

    '    Dim lista As GridLookUpEdit = Nothing
    '    Dim vCodigoBarra As String = ""

    '    Try

    '        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then

    '            Dim View As GridView = CType(DgridDetalleRec2.DefaultView, GridView)
    '            lista = TryCast(sender, GridLookUpEdit)
    '            Dim ColProducto As GridColumn = View.Columns("IdProductoBodega")
    '            Dim ColNoLinea As GridColumn = View.Columns("No_Linea")

    '            If lista Is Nothing Then Return

    '            vCodigoBarra = IIf(IsDBNull(lista.Text), "", lista.Text)

    '            If lista.EditValue Is Nothing AndAlso vCodigoBarra.Trim = "" Then Return

    '            Dim drLineaGrid As DataRow = gvDetalleRec2.GetFocusedDataRow()
    '            If drLineaGrid Is Nothing AndAlso Not View.IsNewItemRow(View.FocusedRowHandle) Then Return

    '            Dim codigoIngresadoString As String = ""
    '            Dim nolineaSeleccionada As String = ""

    '            If lista.EditValue Is Nothing Then
    '                If Not vCodigoBarra.Trim = "" Then
    '                    codigoIngresadoString = vCodigoBarra.Trim()
    '                    ' Aquí establecemos el filtro global para aplicar
    '                    _filtroCodigoBarra = codigoIngresadoString
    '                    lista.Properties.View.RefreshData()  ' Refresca los datos para aplicar el filtro
    '                End If
    '            End If

    '            If lista.EditValue Is Nothing Then
    '                If Not vCodigoBarra.Trim = "" Then
    '                    codigoIngresadoString = vCodigoBarra.Trim()
    '                    _filtroCodigoBarra = codigoIngresadoString
    '                    lista.Properties.View.RefreshData()  ' Refresca los datos para aplicar el filtro
    '                End If
    '            Else

    '                Dim selectedDataRow = lista.GetSelectedDataRow()

    '                If selectedDataRow IsNot Nothing Then
    '                    ' Acceder a las propiedades del objeto selectedDataRow aquí
    '                    codigoIngresadoString = IIf(IsDBNull(selectedDataRow("Codigo")), "", selectedDataRow("Codigo"))
    '                    nolineaSeleccionada = IIf(IsDBNull(selectedDataRow("No_Linea")), "-1", selectedDataRow("No_Linea"))
    '                Else
    '                    ' Configuramos el filtro en el GridView basado en el valor duplicado
    '                    _filtroCodigoBarra = codigoIngresadoString
    '                    ProductoGridLookUpEditRec.View.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("CodigoBarra", _filtroCodigoBarra, DevExpress.Data.Filtering.BinaryOperatorType.Equal)
    '                    ProductoGridLookUpEditRec.View.RefreshData()
    '                    View.SetColumnError(ColNoLinea, "Código de barra duplicado, seleccione uno de la lista.")
    '                    e.Handled = True
    '                    Exit Sub
    '                End If

    '            End If

    '            If ColNoLinea Is Nothing Then
    '                ColNoLinea = View.Columns(1)
    '            Else
    '                If nolineaSeleccionada = "" Then

    '                End If
    '            End If

    '            View.SetColumnError(ColNoLinea, "") : PopupTieneErrores = False

    '            If String.IsNullOrEmpty(codigoIngresadoString) Then
    '                ' El código ingresado es nulo o vacío
    '                If Not View Is Nothing AndAlso Not ColProducto Is Nothing Then
    '                    'Marca celda de código de producto con error.
    '                    View.SetColumnError(ColProducto, "Código no válido.")
    '                End If
    '            Else

    '                Dim vIdProductoBodega As Integer = clsLnProducto.Get_IdProductoBodega_By_Codigo(codigoIngresadoString, AP.IdBodega)

    '                If Not vIdProductoBodega = 0 Then

    '                    Dim cmbView As GridView = TryCast(lista.Properties.View, GridView)
    '                    Dim vCantPorProducto As Integer = 0

    '                    If Not cmbView Is Nothing Then
    '                        vCantPorProducto = cmbView.RowCount
    '                    End If

    '                    If nolineaSeleccionada = "" Then

    '                        If vCantPorProducto > 1 Then

    '                            PopupTieneErrores = True
    '                            _filtroCodigoBarra = codigoIngresadoString
    '                            ProductoGridLookUpEditRec.View.ActiveFilter.Clear()
    '                            ProductoGridLookUpEditRec.View.ActiveFilterCriteria = New DevExpress.Data.Filtering.BinaryOperator("CodigoBarra", _filtroCodigoBarra, DevExpress.Data.Filtering.BinaryOperatorType.Equal)
    '                            ProductoGridLookUpEditRec.View.RefreshData()
    '                            View.SetColumnError(ColNoLinea, "Código de barra duplicado, seleccione uno de la lista.")
    '                            e.Handled = False
    '                            Exit Sub

    '                        Else
    '                            'Marca celda de código de producto con error.
    '                            View.SetColumnError(ColProducto, "Código no válido para el documento.")
    '                        End If

    '                    Else

    '                        If vCantPorProducto > 1 Then

    '                            vCantPorProducto = clsLnTrans_oc_det.Get_Count_By_Producto_En_OC(txtIdOrdenCompra.Text, vIdProductoBodega)

    '                            If vCantPorProducto > 1 Then

    '                                If XtraMessageBox.Show("¿La línea del documento de ingreso que quiere recepcionar es la " & nolineaSeleccionada & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
    '                                    Eliminar_Fila(Nothing)
    '                                    Exit Sub
    '                                End If

    '                            End If

    '                        End If

    '                    End If

    '                    Try
    '                        lista.EditValue = nolineaSeleccionada 'vIdProductoBodega
    '                    Catch ex As Exception
    '                        Debug.WriteLine(ex.Message)
    '                    End Try


    '                Else
    '                    If Not View Is Nothing AndAlso Not ColProducto Is Nothing Then
    '                        'Marca celda de código de producto con error.
    '                        View.SetColumnError(ColProducto, "Código no válido.")
    '                    End If
    '                End If

    '            End If

    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

    Private Sub ProductoGridLookUpEditRec_KeyDown(sender As Object, e As KeyEventArgs)

        Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim vCodigoBarra As String = ""

        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then

            Try

                Dim View As GridView = CType(DgridDetalleRec2.DefaultView, GridView)
                If lista Is Nothing Then Return
                Dim ColNoLinea As GridColumn = View.Columns("No_Linea")
                Dim nolineaSeleccionada As String = ""

                vCodigoBarra = If(IsDBNull(lista.Text), "", lista.Text.Trim())

                If String.IsNullOrEmpty(vCodigoBarra) Then Return

                Dim drLineaGrid As DataRow = View.GetFocusedDataRow()
                If drLineaGrid Is Nothing AndAlso Not View.IsNewItemRow(View.FocusedRowHandle) Then Return

                Dim codigoIngresadoString As String = vCodigoBarra

                View.SetColumnError(ColNoLinea, "") : PopupTieneErrores = False

                If String.IsNullOrEmpty(codigoIngresadoString) Then
                    If Not View Is Nothing AndAlso Not ColNoLinea Is Nothing Then
                        View.SetColumnError(ColNoLinea, "Código no válido.")
                    End If
                Else

                    Dim vIdProductoBodega As Integer = clsLnProducto.Get_IdProductoBodega_By_Codigo(codigoIngresadoString, AP.IdBodega)

                    If Not vIdProductoBodega = 0 Then

                        Dim cmbView As GridView = lista.Properties.View
                        Dim vCantPorProducto As Integer = 0

                        If Not cmbView Is Nothing Then
                            vCantPorProducto = cmbView.RowCount
                        End If

                        If lista.EditValue Is Nothing Then
                            If Not vCodigoBarra.Trim = "" Then
                                codigoIngresadoString = vCodigoBarra.Trim()
                                _filtroCodigoBarra = codigoIngresadoString
                            End If
                        Else

                            Dim selectedDataRow = lista.GetSelectedDataRow()

                            If selectedDataRow IsNot Nothing Then
                                codigoIngresadoString = IIf(IsDBNull(selectedDataRow("Codigo")), "", selectedDataRow("Codigo"))
                                nolineaSeleccionada = IIf(IsDBNull(selectedDataRow("No_Linea")), "-1", selectedDataRow("No_Linea"))
                            Else
                                PopupTieneErrores = True
                                View.SetColumnError(ColNoLinea, "Seleccione un registro.")
                            End If

                        End If

                        If ColNoLinea Is Nothing Then
                            ColNoLinea = View.Columns(1)
                        End If

                        If nolineaSeleccionada = "" Then

                            If vCantPorProducto > 1 Then
                                _filtroCodigoBarra = codigoIngresadoString
                                PopupTieneErrores = True
                                View.SetColumnError(ColNoLinea, "Código de barra duplicado, seleccione uno de la lista.")
                            End If

                        Else

                            If vCantPorProducto > 1 Then

                                vCantPorProducto = clsLnTrans_oc_det.Get_Count_By_Producto_En_OC(txtIdOrdenCompra.Text, vIdProductoBodega)

                                If vCantPorProducto > 1 Then

                                    If XtraMessageBox.Show("¿La línea del documento de ingreso que quiere recepcionar es la " & nolineaSeleccionada & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                        Eliminar_Fila(Nothing)
                                        Exit Sub
                                    End If

                                End If

                            End If

                        End If

                    End If

                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End If

    End Sub
    Private Function EsCodigoBarraDuplicado(codigoBarra As String, gridView As GridView) As Boolean
        Try
            ' Necesitas castear el DataSource a DataView o DataTable para acceder a los DataRowViews
            Dim view As DataView = TryCast(gridView.DataSource, DataView)

            If view IsNot Nothing Then
                ' Iterar sobre cada DataRowView en el DataView
                For Each drv As DataRowView In view
                    If drv("Codigo_Barra").ToString().Equals(codigoBarra) Then
                        Return True
                    End If
                Next
            End If

            Return False
        Catch ex As Exception
            'XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function

    Private NoLineaActualFilaGrid As Integer = 0
    Private Sub ProductoGridLookUpEditRec_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim pIdOrdenCompradet As Integer = 0
            Dim lMaxIdRecepcionDet As Integer = 0
            Dim pLicencia As String = ""
            Dim pPresentacion As Integer = -1
            Dim IdProductoBodega As Integer = 0
            Dim pExisteLinea As Boolean = False
            Dim BeCompraDet As New clsBeTrans_oc_det

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaGrid As DataRow = gvDetalleRec2.GetFocusedDataRow()
            If drLineaGrid Is Nothing Then Return

            Dim vObjProducto As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)
            Dim View As GridView = CType(DgridDetalleRec2.DefaultView, GridView)
            lista = TryCast(sender, GridLookUpEdit)
            Dim ColLote As GridColumn = View.Columns("lote")
            Dim ColNoLinea As GridColumn = View.Columns("No_Linea")

            If PopupTieneErrores Then
                View.SetColumnError(ColNoLinea, "Código de barra duplicado, seleccione uno de la lista.")
                Exit Sub
            End If

            If Not vObjProducto Is Nothing Then

                Dim drArticulo As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drArticulo Is Nothing Then Return

                'GT01032022_0834: traemos las propiedades del producto para validar lote, peso, vencimiento y LP ?
                BeProducto = clsLnProducto.Get_Single_By_CodigoProducto(drArticulo("Codigo"))
                NoLineaActualFilaGrid = lista.EditValue

                IdProductoBodega = drArticulo("IdProductoBodega") ' lista.EditValue
                Dim vIndiceRecDet As Integer = -1

                If Not BeProducto Is Nothing Then

                    BeProducto.IdProductoBodega = IdProductoBodega
                    Dim IdRecepcionDet As Integer = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "IdRecepcionDet")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "IdRecepcionDet"))
                    Dim vNoLinea As Integer = NoLineaActualFilaGrid 'IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "No_Linea")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "No_Linea"))

                    '#GT01022024: si es el mismo producto en el productolockup, no alteramos los datos del grid
                    pExisteLinea = Existe_Producto_en_Grid(vNoLinea, IdRecepcionDet, BeProducto.IdProductoBodega)

                    If pExisteLinea Then

                        DgridDetalleRec2.BeginInvoke(New MethodInvoker(Sub()
                                                                           gvDetalleRec2.FocusedRowHandle = GridControl.AutoFilterRowHandle
                                                                           gvDetalleRec2.FocusedColumn = gvDetalleRec2.Columns("cantidad_recibida")
                                                                           gvDetalleRec2.ShowEditor()
                                                                       End Sub))

                    Else

                        drLineaGrid("IdProductoBodega") = IdProductoBodega
                        drLineaGrid("nombre_producto") = drArticulo("Nombre")
                        drLineaGrid("IdUnidadMedida") = drArticulo("IdUmBas")
                        drLineaGrid("nombre_unidad_medida") = drArticulo("UmBas")
                        drLineaGrid("codigo_producto") = BeProducto.Codigo
                        drLineaGrid("Costo") = drArticulo("Costo")
                        drLineaGrid("No_Linea") = vNoLinea
                        drLineaGrid("Costo") = 0.1
                        drLineaGrid("costo_oc") = 0.1

                        '#GT30012024: Set de OC_ENC y OC_DET al grid
                        If gBeOrdenCompra.IdOrdenCompraEnc > 0 Then
                            drLineaGrid("IdOrdenCompraEnc") = gBeOrdenCompra.IdOrdenCompraEnc
                        Else
                            If gBeRecepcionEnc.OrdenCompraRec.OC.IdOrdenCompraEnc > 0 Then
                                drLineaGrid("IdOrdenCompraEnc") = gBeRecepcionEnc.OrdenCompraRec.OC.IdOrdenCompraEnc
                            End If
                        End If

                        If Not gBeOrdenCompra Is Nothing Then

                            If Not gBeOrdenCompra.DetalleOC Is Nothing Then

                                If gBeOrdenCompra.DetalleOC.Count > 0 Then

                                    BeCompraDet = gBeOrdenCompra.DetalleOC.Find(Function(x) x.Codigo_Producto = BeProducto.Codigo AndAlso x.No_Linea = NoLineaActualFilaGrid)

                                    If Not BeCompraDet Is Nothing Then

                                        If Not drLineaGrid("cantidad_solicitada") Is Nothing Then
                                            drLineaGrid("cantidad_solicitada") = BeCompraDet.Cantidad
                                        End If

                                        Dim vCantidadRecibidaGrid As Double = Get_Cantidad_Recibidad_Grid2(NoLineaActualFilaGrid, gvDetalleRec2.FocusedRowHandle)
                                        drLineaGrid("cantidad_pendiente") = BeCompraDet.Cantidad - vCantidadRecibidaGrid

                                    Else
                                        drLineaGrid("cantidad_solicitada") = 0
                                    End If

                                Else

                                    If gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Count > 0 Then

                                        BeCompraDet = gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Find(Function(x) x.Codigo_Producto = BeProducto.Codigo AndAlso x.No_Linea = NoLineaActualFilaGrid)

                                        If Not BeCompraDet Is Nothing Then

                                            If Not drLineaGrid("cantidad_solicitada") Is Nothing Then
                                                drLineaGrid("cantidad_solicitada") = BeCompraDet.Cantidad
                                            End If

                                            Dim vCantidadRecibidaGrid As Double = Get_Cantidad_Recibidad_Grid2(NoLineaActualFilaGrid, gvDetalleRec2.FocusedRowHandle)
                                            drLineaGrid("cantidad_pendiente") = BeCompraDet.Cantidad - vCantidadRecibidaGrid

                                        Else
                                            drLineaGrid("cantidad_solicitada") = 0
                                        End If

                                    End If

                                End If

                            End If

                        End If

                        If gBeOrdenCompra.DetalleOC.Count > 0 Then
                            If NoLineaActualFilaGrid >= 0 Then
                                drLineaGrid("No_Linea") = NoLineaActualFilaGrid
                                drLineaGrid("codigo_producto") = BeProducto.Codigo
                                pIdOrdenCompradet = gBeOrdenCompra.DetalleOC.Find(Function(x) x.No_Linea = NoLineaActualFilaGrid).IdOrdenCompraDet
                                If pIdOrdenCompradet > 0 Then
                                    drLineaGrid("IdOrdenCompraDet") = pIdOrdenCompradet
                                End If
                            End If
                        Else
                            If gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Count > 0 Then
                                If NoLineaActualFilaGrid >= 0 Then
                                    drLineaGrid("codigo_producto") = BeProducto.Codigo
                                    drLineaGrid("No_Linea") = NoLineaActualFilaGrid
                                    pIdOrdenCompradet = gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Find(Function(x) x.No_Linea = NoLineaActualFilaGrid).IdOrdenCompraDet
                                    If pIdOrdenCompradet > 0 Then
                                        drLineaGrid("IdOrdenCompraDet") = pIdOrdenCompradet
                                    End If
                                End If
                            End If
                        End If

                        '#GT29012024: validamos peso, presentacion, lote, vencimiento y validar si generan LP
                        If BeProducto.Presentaciones.Count > 0 Then
                            If gBeOrdenCompra.DetalleOC.Count > 0 Then

                                If Val(NoLineaActualFilaGrid) > 0 Then
                                    pPresentacion = gBeOrdenCompra.DetalleOC.Find(Function(x) x.IdProductoBodega = BeProducto.IdProductoBodega AndAlso x.No_Linea = NoLineaActualFilaGrid).IdPresentacion
                                Else
                                    pPresentacion = gBeOrdenCompra.DetalleOC.Find(Function(x) x.IdProductoBodega = BeProducto.IdProductoBodega).IdPresentacion
                                End If

                                If pPresentacion > 0 Then
                                    Dim presentacion = BeProducto.Presentaciones.Find(Function(p) p.IdPresentacion = pPresentacion)
                                    If presentacion IsNot Nothing Then
                                        drLineaGrid("IdPresentacion") = presentacion.IdPresentacion
                                        drLineaGrid("nombre_presentacion") = presentacion.Nombre
                                        If presentacion.Genera_lp_auto Then
                                            drLineaGrid("lic_plate") = Genera_Licencia_BOF(cmbBodega.EditValue, AP.UsuarioAp.IdUsuario)
                                            gvDetalleRec2.Columns("lic_plate").OptionsColumn.AllowEdit = False
                                        End If
                                    End If
                                Else
                                    gvDetalleRec2.Columns("IdPresentacion").OptionsColumn.AllowEdit = False
                                End If
                            Else

                                If gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Count > 0 Then

                                    If Val(NoLineaActualFilaGrid) > 0 Then
                                        pPresentacion = gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Find(Function(x) x.IdProductoBodega = BeProducto.IdProductoBodega AndAlso x.No_Linea = NoLineaActualFilaGrid).IdPresentacion
                                    Else
                                        pPresentacion = gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Find(Function(x) x.IdProductoBodega = BeProducto.IdProductoBodega).IdPresentacion
                                    End If

                                    If pPresentacion > 0 Then
                                        Dim presentacion = BeProducto.Presentaciones.Find(Function(p) p.IdPresentacion = pPresentacion)
                                        If presentacion IsNot Nothing Then
                                            drLineaGrid("IdPresentacion") = presentacion.IdPresentacion
                                            drLineaGrid("nombre_presentacion") = presentacion.Nombre
                                            If presentacion.Genera_lp_auto Then
                                                drLineaGrid("lic_plate") = Genera_Licencia_BOF(cmbBodega.EditValue, AP.UsuarioAp.IdUsuario)
                                                gvDetalleRec2.Columns("lic_plate").OptionsColumn.AllowEdit = False
                                            End If
                                        End If
                                    Else
                                        gvDetalleRec2.Columns("IdPresentacion").OptionsColumn.AllowEdit = False
                                    End If
                                End If

                            End If
                        Else
                            gvDetalleRec2.Columns("IdPresentacion").OptionsColumn.AllowEdit = False
                        End If

                        If BeProducto.Control_peso Then
                            drLineaGrid("peso") = 0
                            gvDetalleRec2.Columns("peso").OptionsColumn.AllowEdit = True
                        Else
                            drLineaGrid("peso") = 0
                            gvDetalleRec2.Columns("peso").OptionsColumn.AllowEdit = False
                        End If

                        If BeProducto.Control_vencimiento Then
                            gvDetalleRec2.Columns("fecha_vence").OptionsColumn.AllowEdit = True
                            drLineaGrid("fecha_vence") = Now.Date.AddYears(1)
                        Else
                            gvDetalleRec2.Columns("fecha_vence").OptionsColumn.AllowEdit = False
                        End If

                        If BeProducto.Control_lote AndAlso BeProducto.Genera_lote Then

                            Dim anio As String = Mid(Now.Date.Year.ToString, 3)
                            Dim mes As String = String.Empty

                            If Now.Date.Month.ToString.Length = 1 Then
                                mes = String.Format("0{0}", Now.Date.Month.ToString)
                            Else
                                mes = Now.Date.Month.ToString
                            End If

                            Dim dia As String = String.Empty

                            If Now.Date.Day.ToString.Length = 1 Then
                                dia = String.Format("0{0}", Now.Date.Day.ToString)
                            Else
                                dia = Now.Date.Day.ToString
                            End If

                            Dim lCorrelativo As String = String.Format("{0}{1}{2}0001", anio, mes, dia)
                            drLineaGrid("lote") = lCorrelativo

                        ElseIf BeProducto.Control_lote AndAlso Not BeProducto.Genera_lote Then
                            drLineaGrid("lote") = ""
                            gvDetalleRec2.Columns("lote").OptionsColumn.AllowEdit = True
                        ElseIf Not BeProducto.Control_lote Then
                            drLineaGrid("lote") = ""
                            gvDetalleRec2.Columns("lote").OptionsColumn.AllowEdit = False
                        End If

                        If BeProducto.Genera_lp AndAlso pPresentacion <= 0 Then
                            drLineaGrid("lic_plate") = Genera_Licencia_BOF(cmbBodega.EditValue, AP.UsuarioAp.IdUsuario)
                            gvDetalleRec2.Columns("lic_plate").OptionsColumn.AllowEdit = False
                        Else

                            If gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Count > 0 Then

                                If Val(NoLineaActualFilaGrid) > 0 Then
                                    pPresentacion = gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Find(Function(x) x.IdProductoBodega = BeProducto.IdProductoBodega AndAlso x.No_Linea = NoLineaActualFilaGrid).IdPresentacion
                                Else
                                    pPresentacion = gBeRecepcionEnc.OrdenCompraRec.OC.DetalleOC.Find(Function(x) x.IdProductoBodega = BeProducto.IdProductoBodega).IdPresentacion
                                End If


                                If pPresentacion > 0 Then
                                    Dim presentacion = BeProducto.Presentaciones.Find(Function(p) p.IdPresentacion = pPresentacion)
                                    If presentacion IsNot Nothing Then
                                        drLineaGrid("IdPresentacion") = presentacion.IdPresentacion
                                        drLineaGrid("nombre_presentacion") = presentacion.Nombre

                                        If presentacion.Genera_lp_auto Then
                                            drLineaGrid("lic_plate") = Genera_Licencia_BOF(cmbBodega.EditValue, AP.UsuarioAp.IdUsuario)
                                            gvDetalleRec2.Columns("lic_plate").OptionsColumn.AllowEdit = False
                                        End If
                                    End If

                                Else
                                    drLineaGrid("lic_plate") = ""
                                    gvDetalleRec2.Columns("lic_plate").OptionsColumn.AllowEdit = True
                                End If

                            End If

                        End If

                        If BeProducto.Parametros.Count > 0 Then
                            gvDetalleRec2.Columns("atributo_variante_1").OptionsColumn.AllowEdit = True
                        Else
                            gvDetalleRec2.Columns("atributo_variante_1").OptionsColumn.AllowEdit = False
                        End If

                        '#GT22012024: validamos estados disponibles para dejar setado un valor inicial
                        Dim Estados As List(Of clsBeProducto_estado)
                        Estados = New List(Of clsBeProducto_estado)

                        If gBeOrdenCompra.PropietarioBodega.IdPropietario > 0 Then
                            '#GT02022024: si OC viene de SAP, validar el estado por defecto para la bodega y no cargar por propietario
                            If NavConfigEnc.Interface_SAP And BeBodega.Restringir_Areas_SAP Then
                                'gvDetalleRec2.Columns("IdProductoEstado").OptionsColumn.AllowEdit = False
                                Dim vBodegaERP As String = clsLnBodega_ubicacion.Get_Bodega_ERP(txtIdUbicacion.Text, pBodega.IdBodega)
                                Estados = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodega_by_SAP(gBeOrdenCompra.PropietarioBodega.IdPropietario, pBodega.IdBodega, vBodegaERP)
                            Else
                                gvDetalleRec2.Columns("IdProductoEstado").OptionsColumn.AllowEdit = True
                                Estados = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodega(gBeOrdenCompra.PropietarioBodega.IdPropietario, AP.IdBodega)
                            End If
                        Else
                            If gBeRecepcionEnc.OrdenCompraRec.OC.PropietarioBodega.IdPropietario > 0 Then
                                If NavConfigEnc.Interface_SAP Then
                                    gvDetalleRec2.Columns("IdProductoEstado").OptionsColumn.AllowEdit = True
                                    Dim vBodegaERP As String = clsLnBodega_ubicacion.Get_Bodega_ERP(txtIdUbicacion.Text, pBodega.IdBodega)

                                    If vBodegaERP <> "" Then
                                        Estados = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodega_by_SAP(gBeRecepcionEnc.OrdenCompraRec.OC.PropietarioBodega.IdPropietario, pBodega.IdBodega, vBodegaERP)
                                    Else

                                        vBodegaERP = clsLnBodega.Get_Codigo_By_IdBodega(pBodega.IdBodega)
                                        If vBodegaERP <> "" Then

                                            If BeBodega.Restringir_Areas_SAP Then
                                                Estados = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodega_by_SAP(gBeRecepcionEnc.OrdenCompraRec.OC.PropietarioBodega.IdPropietario, pBodega.IdBodega, vBodegaERP)
                                            Else
                                                Estados = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodega(gBeRecepcionEnc.OrdenCompraRec.OC.PropietarioBodega.IdPropietario, pBodega.IdBodega)
                                            End If

                                        Else
                                            Throw New Exception("La bodega no tiene definida ubicación de recepción.")
                                        End If
                                    End If
                                Else
                                    gvDetalleRec2.Columns("IdProductoEstado").OptionsColumn.AllowEdit = True
                                    Estados = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodega(gBeRecepcionEnc.PropietarioBodega.IdPropietario, AP.IdBodega)
                                End If
                            End If
                        End If

                        If Estados.Count >= 1 AndAlso gBeRecepcionEnc.OrdenCompraRec.OC.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso Then
                            Actualizar_LookupGridEstadoProducto(Estados(0).Codigo_Bodega_ERP)
                            drLineaGrid("IdProductoEstado") = Estados(0).IdEstado
                            drLineaGrid("nombre_producto_estado") = Estados(0).Nombre
                        ElseIf Estados.Count = 1 Then
                            drLineaGrid("IdProductoEstado") = Estados(0).IdEstado
                            drLineaGrid("nombre_producto_estado") = Estados(0).Nombre
                        ElseIf Estados.Count > 0 Then
                            Actualizar_LookupGridEstadoProducto(Estados(0).Codigo_Bodega_ERP)
                            drLineaGrid("IdProductoEstado") = Estados(0).IdEstado
                            drLineaGrid("nombre_producto_estado") = Estados(0).Nombre
                        Else

                            Throw New Exception("GT08022024: no se encontró un estado para recibir el producto.")
                        End If

                        Dim vIdEstado As Integer = 0
                        vIdEstado = drLineaGrid("IdProductoEstado")

                        vIndiceRecDet = pListBeStockRec.FindIndex(Function(b) b.IdRecepcionDet = IdRecepcionDet _
                                                                  AndAlso b.IdProductoEstado = vIdEstado _
                                                                  AndAlso b.IdProductoBodega = IdProductoBodega)

                        If vIndiceRecDet = -1 Then

                            If pListBeStockRec.Count > 0 Then

                                Dim vIndiceRecDetAnt As Integer = pListBeStockRec.FindIndex(Function(b) _
                                                                                             b.IdRecepcionDet = IdRecepcionDet _
                                                                                             AndAlso b.IdProductoEstado = vIdEstado _
                                                                                             AndAlso b.IdProductoBodega = IdProductoBodega)

                                If vIndiceRecDetAnt <> -1 Then
                                    pListBeStockRec.RemoveAt(vIndiceRecDetAnt)
                                Else

                                    lMaxIdRecepcionDet = pListBeStockRec.Max(Function(b) b.IdRecepcionDet) + 1
                                End If

                            Else
                                lMaxIdRecepcionDet = 1
                            End If

                        Else
                            If lBeTransRecDet.Count > 0 Then
                                lMaxIdRecepcionDet = lBeTransRecDet(vIndiceRecDet).IdRecepcionDet + 1
                            Else
                                lMaxIdRecepcionDet = 1
                            End If
                        End If

                        drLineaGrid("IdRecepcionDet") = lMaxIdRecepcionDet
                        drLineaGrid("IsNewR") = True

                        Dim TieneBono As Boolean = clsLnProducto.Get_Control_Manufactura_By_IdProductoBodega(IdProductoBodega)
                        drLineaGrid("Bono") = TieneBono

                        Set_Stock_Parametro2(BeProducto, lMaxIdRecepcionDet, vIdEstado)

                        gvDetalleRec2.Columns("nombre_unidad_medida").OptionsColumn.AllowEdit = False

                        If Estados.Count >= 1 AndAlso gBeRecepcionEnc.OrdenCompraRec.OC.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso Then

                            drLineaGrid("IdProductoEstado") = 0

                            Dim ColEstado As GridColumn = View.Columns("IdProductoEstado")
                            View.SetColumnError(ColEstado, "Seleccione estado")

                        End If

                        DgridDetalleRec2.BeginInvoke(New MethodInvoker(Sub()
                                                                           gvDetalleRec2.FocusedRowHandle = GridControl.AutoFilterRowHandle
                                                                           gvDetalleRec2.FocusedColumn = gvDetalleRec2.Columns("cantidad_recibida")
                                                                           gvDetalleRec2.ShowEditor()
                                                                       End Sub))

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ProductoGridLookUpEditRec_ProcessNewValue(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs)

        Try

            'Add new values to GridLookUpEdit control's DataSource.
            Dim gridLookup As GridLookUpEdit = TryCast(sender, GridLookUpEdit)

            If e.DisplayValue Is Nothing Then
                Return
            End If

            Dim newValue As String = e.DisplayValue.ToString()

            If newValue = String.Empty Then
                Return
            End If

            If Not clsLnProducto.Existe_Codigo(newValue) Then
                e.Handled = False
            End If

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtCantidadGrid_KeyDown(sender As Object, e As KeyEventArgs)

        Try

            If e.KeyCode = Keys.Enter Then

                Dim View As GridView = CType(DgridDetalleRec2.DefaultView, GridView)
                Dim ColCantidadRecibida As GridColumn = View.Columns("cantidad_recibida")
                Dim Cantidad_Recibida As Double = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_recibida")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_recibida"))
                Dim Costo As Double = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "costo")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "costo"))

                If Cantidad_Recibida > 0 Then

                    DgridDetalleRec2.BeginInvoke(New MethodInvoker(Sub()
                                                                       gvDetalleRec2.FocusedRowHandle = GridControl.AutoFilterRowHandle
                                                                       gvDetalleRec2.FocusedColumn = gvDetalleRec2.Columns("peso")
                                                                       gvDetalleRec2.ShowEditor()
                                                                   End Sub))


                    gvDetalleRec2.PostEditor()

                End If

                gvDetalleRec2.SetRowCellValue(gvDetalleRec2.FocusedRowHandle, "Costo", Math.Round(Cantidad_Recibida * Costo, 6))

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Llena_ProductosLookUp_Grid(ByVal pIdPropietarioBodega As Integer)

        Try

            ProductoGridLookUpEdit.DataSource = clsLnProducto.Get_Lista_For_Grid_By_IdPropietario_And_IdBodega(pIdPropietarioBodega,
                                                                                                               cmbBodega.EditValue)

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub PresentacionGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaGrid As DataRow = gvDetalleRec2.GetFocusedDataRow()
            If drLineaGrid Is Nothing Then Return

            Dim vObjPresentacion As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjPresentacion Is Nothing Then

                Dim drPresentacion As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row

                If drPresentacion Is Nothing Then Return

                drLineaGrid("IdPresentacion") = drPresentacion("IdPresentacion")
                drLineaGrid("nombre_presentacion") = drPresentacion("Nombre")

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub EstadoGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try


            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaGrid As DataRow = gvDetalleRec2.GetFocusedDataRow()
            If drLineaGrid Is Nothing Then Return

            Dim vObjEstado As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjEstado Is Nothing Then

                Dim drEstado As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row

                If drEstado Is Nothing Then Return

                drLineaGrid("IdProductoEstado") = drEstado("IdEstado")
                drLineaGrid("nombre_producto_estado") = drEstado("Nombre")

                Dim ColEstado As GridColumn = gvDetalleRec2.Columns("IdProductoEstado")
                gvDetalleRec2.SetColumnError(ColEstado, "")

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtCostoGrid_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

            Dim Costo As Double = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "costo")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "costo"))
            Dim Cantidad_Recibida As Double = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_recibida")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_recibida"))
            Dim IdPresentacion As Double = CType(IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "IdPresentacion")), 0, gvDetalleRec2.GetRowCellValue(gvDetalleRec2.FocusedRowHandle, "IdPresentacion")), Integer)
            Dim Total As Double = Math.Round(Cantidad_Recibida * Costo, 2)

            If IdPresentacion <> 0 Then

                Dim BePresentacion As New clsBeProducto_Presentacion()
                BePresentacion = clsLnProducto_presentacion.GetSingle(IdPresentacion)

                If Not BePresentacion Is Nothing Then

                    Total = Math.Round(Cantidad_Recibida * BePresentacion.Factor * Costo, 2)
                    gvDetalleRec2.SetRowCellValue(gvDetalleRec2.FocusedRowHandle, "Total", Total)

                End If
            Else
                gvDetalleRec2.SetRowCellValue(gvDetalleRec2.FocusedRowHandle, "Total", Total)
            End If

            gvDetalleRec2.PostEditor()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    '#GT08022024: objetos para llenar la linea y guardar individualmente
    Dim pBeTransReDet As New clsBeTrans_re_det
    Dim pListRecDetParam As List(Of clsBeTrans_re_det_parametros)
    Dim pListStockRecSer As List(Of clsBeStock_se_rec)
    Dim pListProductoPallet As List(Of clsBeProducto_pallet)
    Dim pLotesRec As clsBeTrans_oc_det_lote

    Private Grid_Tiene_Error As Boolean = False
    Private Sub gvDetalleRec2_ValidateRow(sender As Object, e As ValidateRowEventArgs) Handles gvDetalleRec2.ValidateRow

        Dim clsTransaccion As New clsTransaccion

        Try

            Dim View As GridView = CType(sender, GridView)
            Dim ColLinea As GridColumn = View.Columns("No_Linea")
            Dim ColProducto As GridColumn = View.Columns("IdProductoBodega")
            Dim ColCodigoProducto As GridColumn = View.Columns("CodigoProducto")
            Dim ColCantidad As GridColumn = View.Columns("cantidad_recibida")
            Dim ColCostoUnitario As GridColumn = View.Columns("Costo")
            Dim ColPeso As GridColumn = View.Columns("peso")
            Dim ColFechaVencimiento As GridColumn = View.Columns("fecha_vence")
            Dim ColLote As GridColumn = View.Columns("lote")
            Dim ColTotal As GridColumn = View.Columns("Total")
            Dim ColIdMotivoDevolucion As GridColumn = View.Columns("IdMotivoDevolucion")
            Dim ColIdRecepcionDet As GridColumn = View.Columns("IdRecepcionDet")
            Dim ColEstado As GridColumn = View.Columns("IdProductoEstado")
            Dim ColLicencia As GridColumn = View.Columns("lic_plate")
            Dim ColBono As GridColumn = View.Columns("Bono")

            Dim vLineaOC As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "No_Linea")), 0, View.GetRowCellValue(e.RowHandle, "No_Linea"))
            Dim IdOrdenCompraEnc As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdOrdenCompraEnc")), 0, View.GetRowCellValue(e.RowHandle, "IdOrdenCompraEnc"))
            Dim IdOrdenCompraDet As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdOrdenCompraDet")), 0, View.GetRowCellValue(e.RowHandle, "IdOrdenCompraDet"))
            Dim IdProductoBodega As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdProductoBodega")), 0, View.GetRowCellValue(e.RowHandle, "IdProductoBodega"))
            Dim CodigoProducto As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "codigo_producto")), "", View.GetRowCellValue(e.RowHandle, "codigo_producto"))
            Dim IdUmBas As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdUmBas")), 0, View.GetRowCellValue(e.RowHandle, "IdUmBas"))
            Dim Cantidad As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "cantidad_recibida")), 0, View.GetRowCellValue(e.RowHandle, "cantidad_recibida"))
            Dim Peso As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "peso")), 0, View.GetRowCellValue(e.RowHandle, "peso"))
            Dim FechaVencimiento As Date = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "fecha_vence")), New Date(1900, 1, 1), View.GetRowCellValue(e.RowHandle, "fecha_vence"))
            Dim Lote As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "lote")), "", View.GetRowCellValue(e.RowHandle, "lote"))
            Dim Licencia As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "lic_plate")), "", View.GetRowCellValue(e.RowHandle, "lic_plate"))
            Dim IdProductoEstado As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdProductoEstado")), 0, View.GetRowCellValue(e.RowHandle, "IdProductoEstado"))
            Dim IdRecepcionDet As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdRecepcionDet")), 0, View.GetRowCellValue(e.RowHandle, "IdRecepcionDet"))
            Dim vIdMotivoDevolucion As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdMotivoDevolucion")), 0, View.GetRowCellValue(e.RowHandle, "IdMotivoDevolucion"))
            Dim vIdPresentacion As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdPresentacion")), 0, View.GetRowCellValue(e.RowHandle, "IdPresentacion"))
            Dim vIsNewRow As Boolean = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IsNewR")), False, View.GetRowCellValue(e.RowHandle, "IsNewR"))
            Dim vTieneBono As Boolean = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "Bono")), False, View.GetRowCellValue(e.RowHandle, "Bono"))

            Dim Etapa_Uno_Correcta As Boolean = False
            Dim Etapa_Tres_Correcta As Boolean = False

            Grid_Tiene_Error = True

            Dim isValidCantidad As Boolean = True
            Dim isValidIdProductoBodega As Boolean = True
            Dim isValidNoLinea As Boolean = True
            Dim isValidFechaVencimiento As Boolean = True
            Dim isValidPeso As Boolean = True
            Dim isValidLote As Boolean = True
            Dim isValidLicencia As Boolean = True
            Dim isValidHomologacion As Boolean = True
            Dim vIndiceRecDet As Integer = -1
            Dim vIsValidEstado As Boolean = True
            Dim IsValidRulePropietario As Boolean = True

            clsTransaccion.Begin_Transaction()

            If Not BeProducto Is Nothing Then

                pBeTransReDet = New clsBeTrans_re_det
                pBeTransReDet.No_Linea = vLineaOC
                pBeTransReDet.Codigo_Producto = CodigoProducto

                If Cantidad = 0 Then
                    isValidCantidad = False
                    View.SetColumnError(ColCantidad, "Ingrese cantidad > 0")
                Else
                    pBeTransReDet.cantidad_recibida = Cantidad
                    isValidCantidad = True
                    View.SetColumnError(ColCantidad, "")
                End If

                If IdProductoBodega = 0 Then
                    isValidIdProductoBodega = False
                    View.SetColumnError(ColProducto, "Ingrese un código de producto válido")
                Else

                    If Not clsLnProducto.Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega(cmbBodega.EditValue, cmbPropietario.EditValue, CodigoProducto, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                        isValidIdProductoBodega = False
                        View.SetColumnError(ColProducto, "Producto no válido.")
                    Else

                        BeProducto = New clsBeProducto
                        BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(CodigoProducto, cmbBodega.EditValue, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        If BeProducto Is Nothing Then
                            isValidIdProductoBodega = False
                            View.SetColumnError(ColProducto, String.Format("El Código {0} no existe.", CodigoProducto))
                        Else
                            BeProducto.Tag = CodigoProducto
                            View.SetColumnError(ColProducto, "")
                            isValidIdProductoBodega = True
                            ColPeso.OptionsColumn.ReadOnly = Not BeProducto.Control_peso
                        End If

                    End If

                End If

                If IdProductoEstado = 0 Then
                    vIsValidEstado = False
                    View.SetColumnError(ColEstado, "Seleccione un estado")
                Else
                    View.SetColumnError(ColEstado, "")
                    vIsValidEstado = True
                End If

                If BeProducto.Control_vencimiento Then

                    isValidFechaVencimiento = True
                    View.SetColumnError(ColFechaVencimiento, "")

                    If Not BeTipoDocumento Is Nothing Then

                        If Not BeTipoDocumento.Permitir_Vencido_Ingreso Then

                            If FechaVencimiento.Date <= Now.Date Then
                                isValidFechaVencimiento = False
                                View.SetColumnError(ColFechaVencimiento, "Vencimiento no válido.")
                                e.Valid = False
                                Exit Sub
                            End If

                        End If

                    End If

                    If FechaVencimiento = New Date(1900, 1, 1) OrElse Not Validos_Tiempos_Aceptacion_Proveedor(FechaVencimiento) Then
                        isValidFechaVencimiento = False
                        View.SetColumnError(ColFechaVencimiento, If(FechaVencimiento = "01/01/1900", "Ingrese un vencimiento.", "Vencimiento no válido según tiempos de aceptacion."))
                    End If

                End If

                If BeProducto.Control_peso AndAlso Peso = 0 Then
                    isValidPeso = False
                    View.SetColumnError(ColPeso, "Ingrese peso")
                Else
                    isValidPeso = True
                    View.SetColumnError(ColPeso, "")
                End If

                If BeProducto.Control_lote Then

                    If pBodega.Homologar_Lote_Vencimiento Then

                        Dim vFechaDefecto As New Date(1900, 1, 1)

                        If Lote = "" Then
                            isValidLote = False
                            View.SetColumnError(ColLote, "Ingrese lote")
                        Else

                            If Not Existe_Lote_Con_Vencimiento_Diferente2(CodigoProducto,
                                                                          Lote,
                                                                          FechaVencimiento) Then

                                isValidLote = True
                                View.SetColumnError(ColLote, "")

                            Else
                                View.SetColumnError(ColLote, "Homologación de lote: El lote ya existe con un vencimiento diferente")
                                isValidHomologacion = False
                            End If

                        End If
                    Else

                        If Lote = "" Then
                            isValidLote = False
                            View.SetColumnError(ColLote, "Ingrese lote")
                        End If

                    End If

                Else
                    isValidLote = True
                    View.SetColumnError(ColLote, "")
                End If

                If BeProducto.Genera_lp AndAlso Licencia = "" Then
                    isValidLicencia = False
                    View.SetColumnError(ColLicencia, "Ingrese licencia")
                Else
                    isValidLicencia = True
                    View.SetColumnError(ColLicencia, "")
                End If

                If Cantidad > 0 Then

                    BeTransOcEnc.IdTipoIngresoOC = BeTipoDocumento.IdTipoIngresoOC

                    If clsLnTrans_re_enc.Reglas_De_Recepcion_Permiten_Ingreso_By_LineaOC(BeTransOcEnc,
                                                                                         BeProducto.Propietario.IdPropietario,
                                                                                         pBeTransReDet,
                                                                                         clsTransaccion.lConnection,
                                                                                         clsTransaccion.lTransaction) Then
                        pListRecDetParam = New List(Of clsBeTrans_re_det_parametros)
                        pListStockRecSer = New List(Of clsBeStock_se_rec)
                        pListProductoPallet = New List(Of clsBeProducto_pallet)
                        pLotesRec = New clsBeTrans_oc_det_lote

                        Llena_Detalle_Recepcion_By_Linea_Nueva(vIsNewRow,
                                                               BeProducto,
                                                               clsTransaccion.lConnection,
                                                               clsTransaccion.lTransaction,
                                                               View,
                                                               e)
                    Else
                        IsValidRulePropietario = False
                        View.SetColumnError(ColCantidad, "Cantidad no válida por regla de recepcion.")
                    End If

                End If

                e.Valid = isValidCantidad AndAlso isValidIdProductoBodega AndAlso isValidNoLinea AndAlso
                          isValidFechaVencimiento AndAlso isValidPeso AndAlso isValidLote AndAlso
                          isValidLicencia AndAlso isValidHomologacion AndAlso vIsValidEstado AndAlso IsValidRulePropietario

                Etapa_Uno_Correcta = e.Valid

            End If

            If Etapa_Uno_Correcta Then

                If vTieneBono Then

                    If XtraMessageBox.Show(String.Format("¿La linea: " & vLineaOC & " del producto " & CodigoProducto & " ¿Tiene bono?") _
                                              , Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        View.SetColumnError(ColBono, "Confirme la bonificación del producto.")
                        e.Valid = False
                        Exit Sub
                    End If

                End If

            End If

            If Etapa_Uno_Correcta Then

                Dim oBeTrans_oc_det As New clsBeTrans_oc_det
                oBeTrans_oc_det.IdOrdenCompraEnc = IdOrdenCompraEnc
                oBeTrans_oc_det.IdOrdenCompraDet = IdOrdenCompraDet

                Dim vTransReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc_With_OC(gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc,
                                                                                                            gBeRecepcionEnc.IdRecepcionEnc,
                                                                                                            clsTransaccion.lConnection,
                                                                                                            clsTransaccion.lTransaction)

                oBeTrans_oc_det = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(oBeTrans_oc_det.IdOrdenCompraEnc,
                                                                                                        oBeTrans_oc_det.IdOrdenCompraDet,
                                                                                                        oBeTrans_oc_det.IdProductoBodega,
                                                                                                        clsTransaccion.lConnection,
                                                                                                        clsTransaccion.lTransaction)
                If pListBeStockRec.FindAll(Function(x) x.IdBodega = 0).Count > 0 Then
                    pListBeStockRec.RemoveAll(Function(x) x.IdBodega = 0)
                End If

                Dim vRsultGuardarHHBof As Boolean = clsLnTrans_re_enc.GuardarHH_BOF(gBeRecepcionEnc.IdRecepcionEnc,
                                                                                   gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc,
                                                                                   pBeTransReDet,
                                                                                   pListRecDetParam,
                                                                                   pListStockRecSer,
                                                                                   pListBeStockRec,
                                                                                   pListProductoPallet,
                                                                                   pLotesRec,
                                                                                   AP.IdEmpresa,
                                                                                   AP.IdBodega,
                                                                                   AP.UsuarioAp.IdUsuario,
                                                                                   gIdMaxIdRecepcionDet,
                                                                                   clsTransaccion.lConnection,
                                                                                   clsTransaccion.lTransaction)



                'vRsultGuardarHHBof = False

                e.Valid = vRsultGuardarHHBof

                If vRsultGuardarHHBof Then

                    View.SetRowCellValue(e.RowHandle, "IdRecepcionDet", gIdMaxIdRecepcionDet)
                    View.SetRowCellValue(e.RowHandle, "IdRecepcionEnc", gBeRecepcionEnc.IdRecepcionEnc)
                    View.SetRowCellValue(e.RowHandle, "IsNewR", False)

                    Incrementar_Licencia_BOF(AP.IdBodega,
                                             AP.UsuarioAp.IdUsuario,
                                             clsTransaccion.lConnection,
                                             clsTransaccion.lTransaction)

                    '#MECR22092025: Se agregó bitacora para registro de detalle recepcionado.
                    Dim msjResultado As String = "Linea registrada: " & gIdMaxIdRecepcionDet & " del producto " & CodigoProducto & " con licencia " & Licencia & " y cantidad recibida " & Cantidad
                    clsLnLog_error_wms_rec.Agregar_Error(msjResultado, AP.UsuarioAp.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

                    Grid_Tiene_Error = False

                End If

            End If

            clsTransaccion.Commit_Transaction()

            clsTransaccion.Close_Conection()

            If Etapa_Uno_Correcta And Not Grid_Tiene_Error Then

                Dim ObjTransReDet = clsLnTrans_re_det.Get_Recepcion_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc, gIdMaxIdRecepcionDet)

                If ObjTransReDet IsNot Nothing Then
                    Dim BeTipoEtiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(BeProducto.IdTipoEtiqueta)

                    If BeTipoEtiqueta IsNot Nothing Then
                        If BeTipoEtiqueta.Es_Inkjet Then
                            Dim Impresion As New frmImpresionSojet
                            Impresion.pTransReDet = ObjTransReDet
                            Impresion.ShowDialog()
                            Impresion.Dispose()
                        Else
                            Dim Impresion As New frmImpresionRecepcion
                            Impresion.pTransReDet = ObjTransReDet
                            Impresion.ShowDialog()
                            Impresion.Dispose()
                        End If
                    End If

                Else
                    XtraMessageBox.Show("Impresión BOF", "No se cargo el producto para impresión.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                _filtroCodigoBarra = ""

                DgridDetalleRec2.BeginInvoke(New MethodInvoker(Sub()
                                                                   gvDetalleRec2.FocusedRowHandle = GridControl.NewItemRowHandle
                                                                   gvDetalleRec2.FocusedColumn = ColLinea
                                                                   gvDetalleRec2.MakeColumnVisible(ColLinea)
                                                                   gvDetalleRec2.ActiveFilter.Clear()
                                                                   If gvDetalleRec2.FocusedColumn IsNot Nothing Then
                                                                       gvDetalleRec2.ClearColumnsFilter()
                                                                       gvDetalleRec2.ShowEditor()
                                                                   End If
                                                               End Sub))


                Application.DoEvents()

            End If

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            e.Valid = False
        End Try

    End Sub

    Private Sub gvDetalleRec2_InvalidRowException(sender As Object, e As InvalidRowExceptionEventArgs) Handles gvDetalleRec2.InvalidRowException
        Try

            '#EJC20210307: Evita que salte mensaje indicando si se quiere corregir la fila.
            e.ExceptionMode = ExceptionMode.NoAction

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Set_Stock_Parametro2(ByVal pObjProducto As clsBeProducto,
                                     ByVal pIdRecepcionDet As Integer,
                                     ByVal pIdEstado As Integer,
                                     Optional ByVal SkipPantallaParametros As Boolean = False)

        Try

            Dim drLineaGrid As DataRow = gvDetalleRec2.GetFocusedDataRow()

            Dim frmCapturaParametros As New frmCapturaParametroRecepcionS() _
            With
                {.IdEmpresa = AP.IdEmpresa,
                .IdBodega = cmbBodega.EditValue,
                .IdPropietario = clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, cmbPropietario.EditValue),
                .pIdPropietarioBodega = cmbPropietario.EditValue,
                .pBeProducto = pObjProducto,
                .pIdRecepcionDet = pIdRecepcionDet,
                .pFechaRecepcion = dtmFechaRecepcion.EditValue,
                .pIndex = pListBeStockRec.FindIndex(Function(f) _
                                                    f.IdRecepcionDet = pIdRecepcionDet)}

            If pListBeStockSeRec IsNot Nothing AndAlso pListBeStockSeRec.Count = 0 And frmCapturaParametros.pIndex > -1 Then
                pListBeStockSeRec = clsLnStock_se_rec.GetAllSerieByIdStockRec(pListBeStockRec(frmCapturaParametros.pIndex).IdStockRec).ToList
            End If

            If frmCapturaParametros.Mostrar_Parametros() Then

                frmCapturaParametros.pListBeStockRec = pListBeStockRec
                frmCapturaParametros.pListBeStockSeRec = pListBeStockSeRec
                frmCapturaParametros.plistBeReDetParametros = plistBeReDetParametros
                frmCapturaParametros.pListBeProductoPallet = pListBeProductoPallet
                frmCapturaParametros.pNoLinea = drLineaGrid("No_Linea")
                frmCapturaParametros.IdTipoTransaccion = txtIdTipoTR.Text

                If Not IsDBNull(drLineaGrid("IdPresentacion")) Then
                    frmCapturaParametros.pIdPresentacion = drLineaGrid("IdPresentacion")
                End If

                If frmCapturaParametros.ShowDialog() = DialogResult.OK Then

                    drLineaGrid("peso_unitario") = frmCapturaParametros.txtPesoReal.Value
                    drLineaGrid("peso") = frmCapturaParametros.txtPesoReal.Value

                    Dim vCantidad As Double
                    If Not IsDBNull(drLineaGrid("cantidad_recibida")) Then
                        vCantidad = drLineaGrid("cantidad_recibida")
                    End If

                    Dim vPesoUnitario As Double = frmCapturaParametros.txtPesoReal.Value

                    drLineaGrid("peso") = vCantidad * vPesoUnitario
                    drLineaGrid("lic_plate") = frmCapturaParametros.txtLicPlate.Text

                    pListBeStockRec = frmCapturaParametros.pListBeStockRec
                    pListBeStockSeRec = frmCapturaParametros.pListBeStockSeRec
                    plistBeReDetParametros = frmCapturaParametros.plistBeReDetParametros
                    pListBeProductoPallet = frmCapturaParametros.pListBeProductoPallet

                    pListObjProductoP = clsLnProducto_presentacion.Get_All_By_IdProducto(pObjProducto.IdProducto)

                Else
                    Guardar_Stock_Rec2(pObjProducto, pIdEstado)
                End If

            Else
                Guardar_Stock_Rec2(pObjProducto, pIdEstado)
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

            Throw ex
        End Try

    End Sub

    Private Sub Llena_Detalle_Recepcion2()

        Try

            Dim lPesoPresentacion As Double = 0.0

            lBeTransRecDet = New List(Of clsBeTrans_re_det)
            lBeProdPallet = New List(Of clsBeProducto_pallet)

            Dim vIndice As Integer = 0
            Dim vlBeStockRec As New List(Of clsBeStock_rec)
            Dim vIdProductoBodega As Integer = 0
            Dim BePres As clsBeProducto_Presentacion
            Dim vPresentacionRequiereLicencia As Boolean = False

            '#GT24012024: Iteración del nuevo grid para llenar el detalle de la recepción
            For i As Integer = 0 To gvDetalleRec2.RowCount - 1

                vIndice = i
                vPresentacionRequiereLicencia = False
                BePres = Nothing

                If gvDetalleRec2.GetRowCellValue(i, "IdProductoBodega") IsNot Nothing AndAlso gvDetalleRec2.GetRowCellValue(i, "IsNewR") IsNot Nothing Then

                    If gvDetalleRec2.GetRowCellValue(i, "IdRecepcionDet") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "IdRecepcionDet") IsNot Nothing Then
                        Dim pIdRecepcionDet As Integer = gvDetalleRec2.GetRowCellValue(i, "IdRecepcionDet")
                        pIndiceListaStock = pListBeStockRec.FindIndex(Function(f) f.IdRecepcionDet = pIdRecepcionDet)
                    Else
                        pIndiceListaStock = 0
                    End If

                    Dim BeTransReDet As New clsBeTrans_re_det() With
                        {.IdPropietarioBodega = cmbPropietario.EditValue,
                        .Producto = New clsBeProducto()}

                    vIdProductoBodega = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoBodega"))
                    BeTransReDet.Producto = clsLnProducto.Get_Single_By_IdProductoBodega(vIdProductoBodega)

                    If BeTransReDet.Producto Is Nothing Then
                        Throw New Exception("ERROR_202401241526: No se pudo obtener el objeto de producto para el IdProductoBodega: (" & vIdProductoBodega & ")")
                    End If

                    BeTransReDet.Producto.Codigo = CStr(gvDetalleRec2.GetRowCellValue(i, "codigo_producto"))
                    BeTransReDet.Codigo_Producto = CStr(gvDetalleRec2.GetRowCellValue(i, "codigo_producto"))
                    BeTransReDet.IdProductoBodega = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoBodega"))
                    BeTransReDet.Nombre_producto = CStr(gvDetalleRec2.GetRowCellValue(i, "nombre_producto"))

                    If gvDetalleRec2.GetRowCellValue(i, "atributo_variante_1") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "atributo_variante_1") IsNot Nothing Then
                        BeTransReDet.Atributo_Variante_1 = CStr(gvDetalleRec2.GetRowCellValue(i, "atributo_variante_1"))
                    End If

                    BeTransReDet.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc

                    If gvDetalleRec2.GetRowCellValue(i, "IdRecepcionDet") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "IdRecepcionDet") IsNot Nothing Then
                        BeTransReDet.IdRecepcionDet = CInt(gvDetalleRec2.GetRowCellValue(i, "IdRecepcionDet"))
                    Else
                        If lBeTransRecDet.Count > 0 Then
                            BeTransReDet.IdRecepcionDet = lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1
                        Else
                            BeTransReDet.IdRecepcionDet = clsLnTrans_re_det.MaxID(BeTransReDet.IdRecepcionEnc) + 1
                        End If
                    End If

                    '#GT24012024: validar Presentacion y UmBas
                    BeTransReDet.Presentacion = New clsBeProducto_Presentacion
                    BeTransReDet.UnidadMedida = New clsBeUnidad_medida
                    If gvDetalleRec2.GetRowCellValue(i, "IdPresentacion") IsNot Nothing AndAlso gvDetalleRec2.GetRowCellValue(i, "IdPresentacion") IsNot DBNull.Value Then
                        BeTransReDet.Nombre_presentacion = gvDetalleRec2.GetRowCellValue(i, "nombre_presentacion")
                        BeTransReDet.Presentacion.IdPresentacion = CInt(gvDetalleRec2.GetRowCellValue(i, "IdPresentacion"))
                        BeTransReDet.IdPresentacion = CInt(gvDetalleRec2.GetRowCellValue(i, "IdPresentacion"))
                        BeTransReDet.UnidadMedida.IdUnidadMedida = CInt(gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida"))
                        BeTransReDet.Nombre_unidad_medida = gvDetalleRec2.GetRowCellValue(i, "nombre_unidad_medida")
                    ElseIf gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida") IsNot Nothing AndAlso gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida") > 0 Then
                        BeTransReDet.UnidadMedida.IdUnidadMedida = CInt(gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida"))
                        BeTransReDet.IdUnidadMedida = CInt(gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida"))
                        BeTransReDet.Nombre_unidad_medida = gvDetalleRec2.GetRowCellValue(i, "nombre_unidad_medida")
                    ElseIf gvDetalleRec2.GetRowCellValue(i, "IdPresentacion") Is Nothing AndAlso gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida") = 0 Then
                        Throw New Exception(String.Format("Seleccione unidad de medida o presentación de producto {0}", BeTransReDet.Codigo_Producto))
                    End If

                    '#GT24012024: validamos peso
                    If gvDetalleRec2.GetRowCellValue(i, "peso") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "peso") IsNot Nothing Then
                        If IsNumeric(gvDetalleRec2.GetRowCellValue(i, "peso")) Then
                            BeTransReDet.Peso = CDbl(gvDetalleRec2.GetRowCellValue(i, "peso"))
                        End If
                    End If

                    BeTransReDet.Nombre_producto_estado = gvDetalleRec2.GetRowCellValue(i, "nombre_producto_estado")

                    If gvDetalleRec2.GetRowCellValue(i, "costo") Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("Ingrese costo")
                    Else
                        BeTransReDet.Costo = CDbl(gvDetalleRec2.GetRowCellValue(i, "costo"))
                    End If

                    If gvDetalleRec2.GetRowCellValue(i, "Total") Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("No existe total")
                    End If

                    '# VALIDACIÓN DE CANTIDADES CON REGLA SEGÚN PROPIETARIO
                    If gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida") Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("Ingrese la cantidad a Recibir")
                    ElseIf gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida") = 0 Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("La cantidad a Recibir debe ser mayor a 0")
                    Else
                        BeTransReDet.cantidad_recibida = (gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida"))
                        BeTransReDet.cantidad_recibida = Math.Round(BeTransReDet.cantidad_recibida, 6)
                    End If

                    If gvDetalleRec2.GetRowCellValue(i, "No_Linea") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "No_Linea") IsNot Nothing Then
                        BeTransReDet.No_Linea = CInt(gvDetalleRec2.GetRowCellValue(i, "No_Linea"))
                    End If

                    '#EJC202210080831
                    If gvDetalleRec2.GetRowCellValue(i, "lic_plate") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "lic_plate") IsNot Nothing Then
                        BeTransReDet.Lic_plate = CStr(IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(i, "lic_plate")), "", gvDetalleRec2.GetRowCellValue(i, "lic_plate")))
                    End If

                    '#GT24012024: solo se valida si hay valor, ya que previamente asignamos la Umbas
                    If gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida") Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception(String.Format("No existe Unidad de Medida en Producto {0}", gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida")))
                    End If

                    '#GT25012024: validamos Estado del producto
                    '#CM20190214: Se llenaba el objeto BeTransReDet y debía ser el pListBeStockRec(pIndiceListaStock)
                    pListBeStockRec(pIndiceListaStock).ProductoEstado = New clsBeProducto_estado
                    BeTransReDet.ProductoEstado = New clsBeProducto_estado
                    If gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado") Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception(String.Format("No existe Estado en Producto {0}", gvDetalleRec2.GetRowCellValue(i, "nombre_producto")))
                    Else
                        BeTransReDet.ProductoEstado.IdEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                        BeTransReDet.IdProductoEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                        pListBeStockRec(pIndiceListaStock).ProductoEstado.IdEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                        pListBeStockRec(pIndiceListaStock).IdProductoEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                    End If


                    If gvDetalleRec2.GetRowCellValue(i, "lote") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "lote") IsNot Nothing Then
                        BeTransReDet.Lote = CStr(gvDetalleRec2.GetRowCellValue(i, "lote"))
                    End If

                    '#GT25012024: validamos Control vencimiento con el producto y no con valor del grid
                    'Dim ControlVencimiento As Boolean = gvDetalleRec2.GetRowCellValue(i, "fecha_vence")
                    Dim ControlVencimiento As Boolean = BeTransReDet.Producto.Control_vencimiento

                    If ControlVencimiento Then

                        If gvDetalleRec2.GetRowCellValue(i, "fecha_vence") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "fecha_vence") IsNot Nothing Then
                            If IsDate(gvDetalleRec2.GetRowCellValue(i, "fecha_vence")) Then
                                BeTransReDet.Fecha_vence = CDate(gvDetalleRec2.GetRowCellValue(i, "fecha_vence"))
                                If pIndiceListaStock <> -1 Then
                                    pListBeStockRec(pIndiceListaStock).Fecha_vence = BeTransReDet.Fecha_vence
                                    If Not ValidaFechaVencimiento(BeTransReDet.Fecha_vence, BeTransReDet.Producto.Nombre) Then
                                        Throw New Exception(String.Format("Se debe corregir la fecha de vencimiento del producto: {0}", BeTransReDet.Producto.Nombre))
                                    End If
                                End If
                            Else
                                SplashScreenManager.CloseForm(False)
                                Throw New Exception(String.Format("La fecha vencimiento no es válida para el producto {0}.", gvDetalleRec2.GetRowCellValue(i, BeTransReDet.Producto.Nombre)))
                            End If
                        Else
                            SplashScreenManager.CloseForm(False)
                            Throw New Exception(String.Format("Ingrese fecha de vencimiento para el producto {0}.", gvDetalleRec2.GetRowCellValue(i, BeTransReDet.Producto.Nombre)))
                        End If

                    Else
                        BeTransReDet.Fecha_vence = New Date(1900, 1, 1)
                    End If

                    BeTransReDet.Fecha_ingreso = Now

                    Dim pReDet As clsBeTrans_re_det = gBeRecepcionEnc.Detalle.Find(Function(x) x.IdRecepcionEnc = BeTransReDet.IdRecepcionEnc AndAlso
                                                                                               x.IdRecepcionDet = BeTransReDet.IdRecepcionDet AndAlso
                                                                                               x.No_Linea = BeTransReDet.No_Linea)

                    If BePres Is Nothing AndAlso BeTransReDet.IdPresentacion <> 0 Then
                        BePres = New clsBeProducto_Presentacion()
                        BePres = clsLnProducto_presentacion.Get_BeProductoPresentacion_By_IdPresentacion(BeTransReDet.IdPresentacion)
                        If Not BePres Is Nothing Then
                            vPresentacionRequiereLicencia = BePres.Genera_lp_auto
                        End If
                    End If

                    If BeTransReDet.Producto.Genera_lp OrElse vPresentacionRequiereLicencia Then

                        If BeTransReDet.Lic_plate = "" Then
                            If pReDet Is Nothing Then
                                Throw New Exception("ERROR_20220912_1550C: Producto sin LP no se puede actualizar.")
                            Else
                                BeTransReDet.Lic_plate = pReDet.Lic_plate
                            End If
                        End If

                    End If

                    '#EJC20221008: Si es una recepción desde BOF (Ciega) no tiene operador.
                    If Not (gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MSOC00") AndAlso Not (gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MCOC00") Then
                        If BeTransReDet.IdOperadorBodega = 0 Then
                            If pReDet Is Nothing Then
                                Throw New Exception("ERROR_20220912_1550: Producto sin Operador-Bodega no se puede actualizar.")
                            Else
                                BeTransReDet.IdOperadorBodega = pReDet.IdOperadorBodega
                            End If
                        End If
                    End If

                    If gvDetalleRec2.GetRowCellValue(i, "observacion") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "observacion") IsNot Nothing Then
                        BeTransReDet.Observacion = CStr(gvDetalleRec2.GetRowCellValue(i, "observacion"))
                    End If

                    If Not BeTransOcEnc Is Nothing Then
                        '#EJC20220407:Encontrar el IdOrdenCompraEnc y Det.
                        BeTransReDet.IdOrdenCompraEnc = BeTransOcEnc.IdOrdenCompraEnc

                        If gvDetalleRec2.GetRowCellValue(i, "IdOrdenCompraDet") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "IdOrdenCompraDet") IsNot Nothing Then
                            BeTransReDet.IdOrdenCompraDet = gvDetalleRec2.GetRowCellValue(i, "IdOrdenCompraDet")
                        End If

                    End If

                    Dim listaProdPalletsNuevos As New List(Of clsBeProducto_pallet)

                    BeTransReDet.User_agr = AP.UsuarioAp.IdUsuario
                    BeTransReDet.Fec_agr = Now
                    BeTransReDet.IsNew = gvDetalleRec2.GetRowCellValue(i, "IsNewR")
                    BeTransReDet.IdUbicacion = CInt(txtIdUbicacion.Text)

                    '#GT25012024
                    ' CAMPOS FALTANTES STOCK DE ASIGNACIÓN 
                    If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then

                        Dim listaProdPallets As New List(Of clsBeProducto_pallet)
                        Dim listaStockPalletsNuevos As New List(Of clsBeStock_rec)

                        If BeTransReDet.Presentacion.IdPresentacion <> 0 Then

                            '#GT25012024: asignamos idProductoBodega al detalle de stock_rec
                            pListBeStockRec(pIndiceListaStock).IdProductoBodega = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoBodega"))
                            pListBeStockRec(pIndiceListaStock).Cantidad = CInt(gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida"))

                            vlBeStockRec = pListBeStockRec.FindAll(Function(p) _
                                                                   p.IdProductoBodega = BeTransReDet.IdProductoBodega _
                                                                   AndAlso p.ProductoValidado = False _
                                                                   AndAlso p.Presentacion.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion _
                                                                   AndAlso p.IdRecepcionDet = BeTransReDet.IdRecepcionDet _
                                                                   AndAlso p.IdProductoEstado = BeTransReDet.IdProductoEstado _
                                                                   AndAlso p.No_linea = BeTransReDet.No_Linea)

                            If pListBeProductoPallet IsNot Nothing AndAlso pListBeProductoPallet.Count > 0 Then

                                listaProdPallets = pListBeProductoPallet.FindAll(Function(p) _
                                                                                 p.IdRecepcionDet = BeTransReDet.IdRecepcionDet _
                                                                                 AndAlso p.IdProductoBodega = BeTransReDet.IdProductoBodega _
                                                                                 AndAlso p.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion)

                                Dim BePresPP As New clsBeProducto_Presentacion With {.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion}
                                clsLnProducto_presentacion.GetSingle(BePresPP)

                                For Each BePP As clsBeProducto_pallet In listaProdPallets

                                    With BePP

                                        .Lote = CStr(gvDetalleRec2.GetRowCellValue(i, "lote"))
                                        .User_agr = AP.UsuarioAp.IdUsuario
                                        .User_mod = AP.UsuarioAp.IdUsuario
                                        .Cantidad = Math.Round((1 * BePresPP.Factor * BePresPP.CajasPorCama * BePresPP.CamasPorTarima), 6)
                                        If ControlVencimiento Then
                                            If gvDetalleRec2.GetRowCellValue(i, "fecha_vence") IsNot Nothing Then
                                                .Fecha_vence = CDate(gvDetalleRec2.GetRowCellValue(i, "fecha_vence"))
                                            End If
                                        Else
                                            .Fecha_vence = Nothing
                                        End If

                                    End With

                                Next

                            End If

                        Else

                            '#CKFK 20180420 Moví esto para acá por la forma en busca en la lista
                            If gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida") IsNot Nothing Then
                                pListBeStockRec(pIndiceListaStock).IdUnidadMedida = CInt(gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida"))
                            End If

                            '#GT25012024: asociamos la ubicación para stock_rec
                            pListBeStockRec(pIndiceListaStock).IdUbicacion = txtIdUbicacion.EditValue

                            '#GT25012024: asignamos idProductoBodega al detalle de stock_rec
                            pListBeStockRec(pIndiceListaStock).IdProductoBodega = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoBodega"))

                            '#EJC20190214: tu día del cariño, bien pijiado es la 1.40 am..
                            BeTransReDet.ProductoEstado = New clsBeProducto_estado
                            If gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado") IsNot Nothing Then
                                BeTransReDet.ProductoEstado.IdEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                                BeTransReDet.IdProductoEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                                pListBeStockRec(pIndiceListaStock).ProductoEstado.IdEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                                pListBeStockRec(pIndiceListaStock).IdProductoEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                            End If

                            '#EJC20171025_1241PM: Se agregó p.Presentacion.IdPresentacion =0 para que no mezcle el mismo código con y sin presentación..
                            '#EJC20190214: Se agrego filtro por IdProductoEstado, NoLinea e IdRecepcionDet..
                            vlBeStockRec = pListBeStockRec.FindAll(Function(p) _
                                                       p.IdProductoBodega = BeTransReDet.IdProductoBodega _
                                                       AndAlso p.ProductoValidado = False _
                                                       AndAlso p.IdUnidadMedida = BeTransReDet.IdUnidadMedida _
                                                       AndAlso p.IdPresentacion = 0 _
                                                       AndAlso p.IdRecepcionDet = BeTransReDet.IdRecepcionDet _
                                                       AndAlso p.IdProductoEstado = BeTransReDet.IdProductoEstado _
                                                       AndAlso p.No_linea = BeTransReDet.No_Linea)

                        End If

                        For Each BeStockRec As clsBeStock_rec In vlBeStockRec

                            BeStockRec.ProductoEstado = New clsBeProducto_estado
                            If gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado") IsNot Nothing Then
                                BeStockRec.ProductoEstado.IdEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                                BeStockRec.IdProductoEstado = CInt(gvDetalleRec2.GetRowCellValue(i, "IdProductoEstado"))
                            End If

                            BeStockRec.Presentacion = New clsBeProducto_Presentacion
                            If gvDetalleRec2.GetRowCellValue(i, "IdPresentacion") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "IdPresentacion") IsNot Nothing Then
                                BeStockRec.Presentacion.IdPresentacion = CInt(gvDetalleRec2.GetRowCellValue(i, "IdPresentacion"))
                                BeStockRec.IdPresentacion = CInt(gvDetalleRec2.GetRowCellValue(i, "IdPresentacion"))
                            End If

                            '#EJC20180113:Atributo_Variante_1 en detalle de stock -> Llena_Detalle_Recepcion
                            If gvDetalleRec2.GetRowCellValue(i, "atributo_variante_1") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "atributo_variante_1") IsNot Nothing Then
                                BeStockRec.Atributo_Variante_1 = IIf(IsDBNull(gvDetalleRec2.GetRowCellValue(i, "atributo_variante_1")), "", CStr(gvDetalleRec2.GetRowCellValue(i, "atributo_variante_1")))
                            Else
                                BeStockRec.Atributo_Variante_1 = ""
                            End If

                            If gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida") IsNot Nothing Then
                                BeStockRec.IdUnidadMedida = CInt(gvDetalleRec2.GetRowCellValue(i, "IdUnidadMedida"))
                            End If

                            BeStockRec.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc

                            If gvDetalleRec2.GetRowCellValue(i, "lote") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "lote") IsNot Nothing Then
                                BeStockRec.Lote = CStr(gvDetalleRec2.GetRowCellValue(i, "lote"))
                            End If

                            If ControlVencimiento Then
                                If gvDetalleRec2.GetRowCellValue(i, "fecha_vence") IsNot DBNull.Value AndAlso gvDetalleRec2.GetRowCellValue(i, "fecha_vence") IsNot Nothing Then
                                    BeStockRec.Fecha_vence = CDate(gvDetalleRec2.GetRowCellValue(i, "fecha_vence"))
                                End If
                            Else
                                BeStockRec.Fecha_vence = Nothing
                            End If

                            BeStockRec.IdUbicacion = CInt(txtIdUbicacion.Text.Trim)
                            BeStockRec.IdUbicacion_anterior = BeStockRec.IdUbicacion
                            BeStockRec.ProductoValidado = True
                            BeStockRec.Fecha_Ingreso = Now
                            BeStockRec.Fec_agr = Now

                            If BeStockRec.Presentacion.IdPresentacion <> 0 Then

                                BePres.IdPresentacion = BeStockRec.Presentacion.IdPresentacion
                                clsLnProducto_presentacion.GetSingle(BePres)

                                If BePres.EsPallet Then

                                    If CDbl(gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida")) = 1 Then

                                        BeStockRec.Cantidad = Math.Round((CDbl(gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida")) * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima), 6)

                                    Else

                                        If BePres.Genera_lp_auto Then

                                            Dim vCantidadPallets As Integer = gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida")
                                            Dim vIndiceLista As Integer = 0
                                            Dim vBeStockRec As New clsBeStock_rec
                                            Dim BeProdPallet As New clsBeProducto_pallet

                                            For x As Integer = 1 To vCantidadPallets - 1

                                                vIndiceLista = listaStockPalletsNuevos.Count - 1

                                                If x = 1 Then
                                                    clsPublic.CopyObject(BeStockRec, vBeStockRec)
                                                    vBeStockRec.Cantidad = (1 * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima)
                                                Else
                                                    vBeStockRec = New clsBeStock_rec
                                                    clsPublic.CopyObject(listaStockPalletsNuevos(vIndiceLista), vBeStockRec)
                                                End If

                                                vBeStockRec.Cantidad = (1 * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima)

                                                vBeStockRec.Lic_plate = clsLnStock_rec.Get_Nuevo_Correlativo_LicensePlate(AP.IdEmpresa,
                                                                                                                         cmbBodega.EditValue,
                                                                                                                         cmbPropietario.EditValue,
                                                                                                                         BeTransReDet.IdProductoBodega,
                                                                                                                         vBeStockRec.Lic_plate)

                                                listaStockPalletsNuevos.Add(vBeStockRec)

                                                If BePres.Imprime_barra Then

                                                    BeProdPallet = New clsBeProducto_pallet

                                                    With BeProdPallet
                                                        .IdPropietarioBodega = BeTransReDet.IdPropietarioBodega
                                                        .IdProductoBodega = BeTransReDet.IdProductoBodega
                                                        .IdOperadorBodega = Nothing
                                                        .IdPresentacion = BePres.IdPresentacion
                                                        .IdRecepcionDet = vBeStockRec.IdRecepcionDet
                                                        .Impreso = 0
                                                        .IdImpresora = 1
                                                        .Activo = True
                                                        .Fecha_ingreso = Now
                                                        .Codigo_Barra = vBeStockRec.Lic_plate
                                                        .Reimpresiones = 0
                                                        .Cantidad = vBeStockRec.Cantidad
                                                        .Fecha_vence = IIf(ControlVencimiento, vBeStockRec.Fecha_vence, Nothing)
                                                        .Fec_agr = Now
                                                        .Fec_mod = Now
                                                        .Lote = vBeStockRec.Lote
                                                        .User_agr = AP.UsuarioAp.IdUsuario
                                                        .User_mod = AP.UsuarioAp.IdUsuario
                                                        .IsNew = True
                                                    End With

                                                    listaProdPalletsNuevos.Add(BeProdPallet)
                                                    lBeProdPallet.Add(BeProdPallet)

                                                End If

                                            Next

                                        Else

                                            Throw New Exception("La cantidad de pallets es > 1 y genera_lp_auto es Falso, debe recibir los pallets de forma unitaria (Cantidad = 1)")
                                            'La cantidad es > 1 y genera_lp_auto es false

                                        End If

                                    End If

                                Else

                                    BeStockRec.Cantidad = Math.Round((CDbl(gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida")) * BePres.Factor), 6)

                                    If BePres.Imprime_barra Then

                                        Dim BeProdPallet As New clsBeProducto_pallet With {
                                                  .IdPropietarioBodega = BeTransReDet.IdPropietarioBodega,
                                                  .IdProductoBodega = BeTransReDet.IdProductoBodega,
                                                  .IdPresentacion = BePres.IdPresentacion,
                                                  .IdRecepcionDet = BeTransReDet.IdRecepcionDet,
                                                  .IdOperadorBodega = Nothing,
                                                  .Impreso = 0,
                                                  .IdImpresora = 1,
                                                  .Activo = True,
                                                  .Fecha_ingreso = Now,
                                                  .Codigo_Barra = BePres.Codigo_barra,
                                                  .Reimpresiones = 0,
                                                  .Cantidad = BeStockRec.Cantidad,
                                                  .Fecha_vence = BeStockRec.Fecha_vence,
                                                  .Fec_agr = Now,
                                                  .Fec_mod = Now,
                                                  .Lote = BeStockRec.Lote,
                                                  .User_agr = AP.UsuarioAp.IdUsuario,
                                                  .User_mod = AP.UsuarioAp.IdUsuario,
                                                  .IsNew = True}

                                        listaStockPalletsNuevos.Add(BeStockRec)
                                        listaProdPallets.Add(BeProdPallet)
                                        lBeProdPallet.Add(BeProdPallet)

                                    End If

                                End If

                            Else
                                'Agregado por Erik Calderón, si no hay presentación 
                                'no se multiplica por factor, se recibe en unidad 
                                'de medida básica.
                                '#EJC20170710
                                BeStockRec.Cantidad = Math.Round((CDbl(gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida"))), 6)

                                Dim vBestockRec As New clsBeStock_rec()
                                vBestockRec = BeStockRec.Clone()
                                listaStockPalletsNuevos.Add(vBestockRec)

                            End If

                        Next

                        For Each PP As clsBeProducto_pallet In listaProdPalletsNuevos
                            listaProdPallets.Add(PP)
                        Next

                        For Each PP As clsBeProducto_pallet In listaProdPallets
                            lBeProdPallet.Add(PP)
                        Next

                    End If

                    BeTransReDet.MotivoDevolucion = New clsBeMotivo_devolucion
                    lBeTransRecDet.Add(BeTransReDet)

                End If
            Next


        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

            Throw ex
        End Try

    End Sub

    Private Sub Guardar_Stock_Rec2(ByVal pObjProducto As clsBeProducto,
                                   ByVal IdEstado As Integer,
                                   Optional ByVal CheckManualInput As Boolean = True)

        Dim pIdRecepcionDet As Integer
        Dim pIndex As Integer

        Try

            pIdPropietarioBodega = cmbPropietario.EditValue

            Dim drLineaGrid As DataRow = gvDetalleRec2.GetFocusedDataRow()
            Dim vNoLinea As Integer = 0
            Dim vIdRecepcionDet As Integer = 0

            If Not IsDBNull(drLineaGrid("No_Linea")) Then
                vNoLinea = drLineaGrid("No_Linea")
            End If

            If Not IsDBNull(drLineaGrid("IdRecepcionDet")) Then
                vIdRecepcionDet = drLineaGrid("IdRecepcionDet")
            End If

            pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = pObjProducto.IdProductoBodega _
                                                                              AndAlso f.IdProductoEstado = IdEstado _
                                                                              AndAlso f.No_linea = vNoLinea)

            If pIndex > -1 Then
                pIdRecepcionDet = pListBeStockRec(pIndex).IdRecepcionDet
            Else

                If CheckManualInput Then

                    '#EJC20200311: Si modifican él código de producto en la misma línea, eliminar el objeto anterior.
                    Dim vIndiceAnte As Integer = pListBeStockRec.FindIndex(Function(f) f.No_linea = vNoLinea AndAlso f.IdRecepcionDet = vIdRecepcionDet)

                    If vIndiceAnte <> -1 Then

                        If pListBeStockRec(vIndiceAnte).IdProductoBodega <> pObjProducto.IdProducto Then
                            pListBeStockRec.RemoveAt(vIndiceAnte)
                        End If

                    End If

                End If

                If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then
                    pIdRecepcionDet = pListBeStockRec.Max(Function(b) b.IdRecepcionDet) + 1
                Else
                    lMaxIdRecepcionDetParametro = 1
                    pIdRecepcionDet = lMaxIdRecepcionDetParametro
                End If

            End If

            Dim BeStock_rec As New clsBeStock_rec With
                        {.IdRecepcionDet = pIdRecepcionDet,
                        .IdPropietarioBodega = pIdPropietarioBodega,
                        .IdProductoBodega = pObjProducto.IdProductoBodega,
                        .IsNew = True
                        }

            pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = pObjProducto.IdProductoBodega)

            If pIndex >= -1 Then

                If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then
                    BeStock_rec.IdStockRec = pListBeStockRec.Max(Function(b) b.IdStockRec) + 1
                Else
                    BeStock_rec.IdStockRec = clsLnStock_rec.MaxID() + 1
                End If

                Dim currentView As GridView = DgridDetalleRec2.FocusedView

                If currentView IsNot Nothing AndAlso currentView.SelectedRowsCount = 1 Then

                    Dim vCodigoProducto As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "No_Linea")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "No_Linea"))
                    Dim vLic_plate As String = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "lic_plate")), "", currentView.GetRowCellValue(currentView.FocusedRowHandle, "lic_plate"))
                    Dim vIdPresentacion As String = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdPresentacion")), "", currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdPresentacion"))
                    Dim vIdProductoEstado As String = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProductoEstado")), "", currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProductoEstado"))

                    BeStock_rec.Presentacion.IdPresentacion = 0
                    BeStock_rec.Lic_plate = vLic_plate
                    BeStock_rec.IdProductoEstado = vIdProductoEstado
                    BeStock_rec.Fecha_Ingreso = Date.Now
                    BeStock_rec.Fecha_Manufactura = Nothing
                    BeStock_rec.Serial = 0
                    BeStock_rec.Añada = 0
                    BeStock_rec.Peso = 0.0
                    BeStock_rec.Temperatura = 0.0
                    BeStock_rec.Fec_mod = Date.Now
                    BeStock_rec.User_mod = AP.UsuarioAp.IdUsuario
                    BeStock_rec.IdPropietarioBodega = pIdPropietarioBodega
                    BeStock_rec.IdProductoBodega = pObjProducto.IdProductoBodega
                    BeStock_rec.No_linea = vNoLinea
                    pListBeStockRec.Add(BeStock_rec)

                Else
                    Throw New Exception("EJC240419: Por seguridad, se ha detenido la creación del objeto de stock al parecer la vista o línea del grid no es correcta.")
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

        End Try

    End Sub

    Private Function Existe_Producto_en_Grid(ByVal pNoLInea As Integer,
                                             ByVal pIdRecepcionDet As Integer,
                                             ByVal pIdProductoBodega As Integer) As Boolean

        Existe_Producto_en_Grid = False

        Try

            If pListBeStockRec.Count > 0 Then

                Dim Existe = pListBeStockRec.Find(Function(p) p.No_linea = pNoLInea AndAlso
                                                  p.IdRecepcionDet = pIdRecepcionDet AndAlso
                                                  p.IdProductoBodega = pIdProductoBodega)


                If Existe IsNot Nothing Then
                    Existe_Producto_en_Grid = True
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub Actualizar_LookupGridProducto()

        Try

            If Not ProductoGridLookUpEdit Is Nothing Then
                If Not gBeRecepcionEnc Is Nothing Then
                    If (txtIdTipoTR.Text = clsDataContractDI.tTipo_Rec.HCOC00.ToString()) OrElse (gBeRecepcionEnc.IdTipoTransaccion = clsDataContractDI.tTipo_Rec.HSOC00.ToString()) OrElse (gBeRecepcionEnc.IdTipoTransaccion = "") Then
                        If Not gBeOrdenCompra Is Nothing Then
                            ProductoGridLookUpEdit.DataSource = clsLnProducto.Get_Lista_By_IdOrdenCompraEnc(gBeOrdenCompra.IdOrdenCompraEnc, cmbBodega.EditValue)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Actualizar_LookupGridEstadoProducto(Optional pCodigoBodegaERP As String = "")

        Try

            If gBeRecepcionEnc Is Nothing Then Return

            If Not EstadoGridLookUpEdit Is Nothing Then

                If (txtIdTipoTR.Text = clsDataContractDI.tTipo_Rec.HCOC00.ToString()) OrElse (gBeRecepcionEnc.IdTipoTransaccion = clsDataContractDI.tTipo_Rec.HSOC00.ToString()) OrElse (gBeRecepcionEnc.IdTipoTransaccion = "") Then

                    If Not gBeOrdenCompra Is Nothing Then

                        If gBeRecepcionEnc.OrdenCompraRec.OC.PropietarioBodega.IdPropietario > 0 Then
                            EstadoGridLookUpEdit.DataSource = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodegaHH(gBeRecepcionEnc.OrdenCompraRec.OC.PropietarioBodega.IdPropietario, AP.IdBodega)
                        Else
                            EstadoGridLookUpEdit.DataSource = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodegaHH(gBeOrdenCompra.PropietarioBodega.IdPropietario, AP.IdBodega)
                        End If

                        EstadoGridLookUpEdit.DisplayMember = "nombre"
                        EstadoGridLookUpEdit.ValueMember = "IdEstado"

                    End If

                Else

                    'GT 08022021 se obtiene el IdRegimen del combo
                    Dim fila As Object = cmbPropietario.GetSelectedDataRow
                    Dim IdPropietario As Integer = 0
                    If fila Is Nothing Then
                        Throw New Exception("Error_20243101_0930: No se pudo obtener un propietario para cargar los estados del producto.")
                    Else
                        IdPropietario = fila.Item("IdPropietario")
                    End If

                    If AP.InterfaceSAP AndAlso AP.Bodega.Restringir_Areas_SAP Then

                        If Not gBeOrdenCompra Is Nothing Then

                            If Not gBeRecepcionEnc.OrdenCompraRec Is Nothing Then

                                If gBeRecepcionEnc.OrdenCompraRec.OC.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso Then
                                    '#EJC202405241530: Listar estados específicos SAP.
                                    EstadoGridLookUpEdit.DataSource = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodega_By_SAP(IdPropietario, AP.IdBodega, pCodigoBodegaERP)
                                Else
                                    EstadoGridLookUpEdit.DataSource = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodegaHH(IdPropietario, AP.IdBodega)
                                End If

                            End If

                        End If

                    Else
                        EstadoGridLookUpEdit.DataSource = clsLnProducto_estado.Listar_By_IdPropietario_And_IdBodegaHH(IdPropietario, AP.IdBodega)
                    End If

                    EstadoGridLookUpEdit.DisplayMember = "nombre"
                    EstadoGridLookUpEdit.ValueMember = "IdEstado"

                End If

            End If

            EstadoGridLookUpEdit.View.RefreshData()
            EstadoGridLookUpEdit.View.RefreshEditor(True)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private AgregandoFilasGrid As Boolean = False
    Private Sub Cargar_Detalle_Recepcion2(Optional ByVal Actualizar_Productos_Recibidos As Boolean = False)

        Dim clsTransaccion As New clsTransaccion

        Try

            Actualizar_LookupGridEstadoProducto()

            If lBeTransRecDet.Count > 0 Then

                DTTransReDet2.Clear()

                Dim BeTransOcDetLinea As New clsBeTrans_oc_det
                Dim vCantidadSolicitada As Double = 0
                Dim vCantidadPendiente As Double = 0
                Dim vCantidadRecibidaAcumulada As Double = 0
                Dim vNoLineaControl As Integer = 0
                Dim vTotal As Double = 0
                Dim BeProductoRecibido As New clsBeProductosRecepcion
                Dim vIndiceLista As Integer = -1
                Dim TieneBono As Boolean = False

                clsTransaccion.Begin_Transaction()

                For Each BeTransReDet As clsBeTrans_re_det In lBeTransRecDet.OrderByDescending(Function(x) x.IdRecepcionDet)

                    AgregandoFilasGrid = True

                    vTotal = BeTransReDet.cantidad_recibida * BeTransReDet.Costo

                    If vNoLineaControl <> BeTransReDet.No_Linea Then
                        vNoLineaControl = BeTransReDet.No_Linea
                        vCantidadRecibidaAcumulada = BeTransReDet.cantidad_recibida
                    Else
                        vCantidadRecibidaAcumulada += BeTransReDet.cantidad_recibida
                    End If

                    If BeTransReDet.IdOrdenCompraEnc > 0 AndAlso BeTransReDet.IdOrdenCompraDet > 0 Then

                        BeTransOcDetLinea = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(BeTransReDet.IdOrdenCompraEnc,
                                                                                                                  BeTransReDet.IdOrdenCompraDet,
                                                                                                                  BeTransReDet.IdProductoBodega,
                                                                                                                  BeTransReDet.No_Linea,
                                                                                                                  clsTransaccion.lConnection,
                                                                                                                  clsTransaccion.lTransaction)

                        If Not BeTransOcDetLinea Is Nothing Then
                            vCantidadSolicitada = BeTransOcDetLinea.Cantidad
                            vCantidadPendiente = vCantidadSolicitada - vCantidadRecibidaAcumulada
                        End If

                    End If

                    TieneBono = clsLnProducto.Get_Control_Manufactura_By_IdProductoBodega(BeTransReDet.IdProductoBodega,
                                                                                          clsTransaccion.lConnection,
                                                                                          clsTransaccion.lTransaction)

                    DTTransReDet2.Rows.Add(BeTransReDet.IdRecepcionDet,
                                           BeTransReDet.IdRecepcionEnc,
                                           BeTransReDet.IdProductoBodega,
                                           BeTransReDet.IdPresentacion,
                                           BeTransReDet.IdUnidadMedida,
                                           BeTransReDet.IdProductoEstado,
                                           BeTransReDet.IdOperadorBodega,
                                           BeTransReDet.IdMotivoDevolucion,
                                           BeTransReDet.No_Linea,
                                           vCantidadSolicitada,
                                           BeTransReDet.cantidad_recibida,
                                           vCantidadPendiente,
                                           BeTransReDet.Nombre_producto,
                                           BeTransReDet.Nombre_presentacion,
                                           BeTransReDet.Nombre_unidad_medida,
                                           BeTransReDet.Nombre_producto_estado,
                                           BeTransReDet.Lote,
                                           BeTransReDet.Fecha_vence,
                                           BeTransReDet.Fecha_ingreso,
                                           BeTransReDet.Peso,
                                           BeTransReDet.Peso_Estadistico,
                                           BeTransReDet.Peso_Minimo,
                                           BeTransReDet.Peso_Maximo,
                                           BeTransReDet.peso_unitario,
                                           BeTransReDet.User_agr,
                                           BeTransReDet.Fec_agr,
                                           BeTransReDet.Observacion,
                                           BeTransReDet.Aniada,
                                           BeTransReDet.Costo,
                                           BeTransReDet.Costo_Oc,
                                           BeTransReDet.Costo_Estadistico,
                                           BeTransReDet.Atributo_Variante_1,
                                           BeTransReDet.Codigo_Producto,
                                           BeTransReDet.Lic_plate,
                                           BeTransReDet.Pallet_No_Estandar,
                                           BeTransReDet.IdOrdenCompraEnc,
                                           BeTransReDet.IdOrdenCompraDet,
                                           BeTransReDet.IdJornadaSistema,
                                           vTotal,
                                           False,
                                           TieneBono)

                Next

                DgridDetalleRec2.DataSource = DTTransReDet2

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            AgregandoFilasGrid = False
        End Try

    End Sub

    Private Sub ToolEliminarFila_Click(sender As Object, e As EventArgs) Handles ToolEliminarFila.Click

        If Not permiteMenu("2.1.1.2") Then
            Return
        End If

        cmdEliminar.Enabled = False

        If pBeTipo_Tarea_HH.UsaHH Then
            XtraMessageBox.Show("El tipo de tarea requiere que la linea se elimine desde la HH.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            EliminarFila(Nothing)
        End If

    End Sub

    Private Sub Eliminar_Fila(e As KeyEventArgs)

        Try

            If gBeRecepcionEnc.Estado <> "Cerrado" Then

                Dim currentView As GridView = DgridDetalleRec2.FocusedView

                '#GT11102022_1500: validar que es una fila con datos.
                If currentView IsNot Nothing AndAlso currentView.SelectedRowsCount = 1 Then

                    Dim vCodigoProducto As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "No_Linea")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "No_Linea"))
                    Dim vIdRecepcionDet As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionDet")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionDet"))
                    Dim vCantidad_recibida As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "cantidad_recibida")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "cantidad_recibida"))
                    Dim vLic_plate As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "lic_plate")), "", currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "lic_plate"))

                    If XtraMessageBox.Show(String.Format("¿Eliminar la linea: " & vIdRecepcionDet & " del producto " & vCodigoProducto & " con licencia " & vLic_plate & " y cantidad recibida " & vCantidad_recibida) _
                                              , Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Dim gridControl As GridControl = DgridDetalleRec2
                        Eliminar_Fila_Recepcion(gridControl)

                    End If

                End If
            Else
                XtraMessageBox.Show("Recepción cerrada, no se puede modificar el detalle.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Eliminar_Fila_Recepcion(ByVal pGridControl As GridControl)

        Dim currentView As GridView = CType(pGridControl.FocusedView, GridView)

        Try

            Dim lIndex As Integer = -1

            Dim IdProductoBodega As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProductoBodega")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProductoBodega"))
            Dim IdProducto As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProducto")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProducto"))
            Dim IdRecepcionEnc As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdRecepcionEnc")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdRecepcionEnc"))
            Dim IdRecepcionDet As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdRecepcionDet")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdRecepcionDet"))
            Dim lic_plate As String = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "lic_plate")), "", currentView.GetRowCellValue(currentView.FocusedRowHandle, "lic_plate"))
            Dim cantidad_recibida As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "cantidad_recibida")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "cantidad_recibida"))

            '#GT01022024: buscar el index si hay recepcion guardada
            If IdRecepcionEnc > 0 AndAlso IdRecepcionDet > 0 Then

                'pListBeStockRec = clsLnStock_rec.Get_All_By_IdRecepcionEnc(gBeRecepcionEnc.IdRecepcionEnc)

                If lic_plate <> "" Then
                    lIndex = pListBeStockRec.FindIndex(Function(p) p.IdRecepcionEnc = IdRecepcionEnc AndAlso
                                                               p.IdRecepcionDet = IdRecepcionDet AndAlso
                                                               p.IdProductoBodega = IdProductoBodega AndAlso
                                                               p.Lic_plate = lic_plate)
                Else
                    lIndex = pListBeStockRec.FindIndex(Function(p) p.IdRecepcionEnc = IdRecepcionEnc AndAlso
                                                               p.IdRecepcionDet = IdRecepcionDet AndAlso
                                                               p.IdProductoBodega = IdProductoBodega)
                End If

            Else
                lIndex = pListBeStockRec.FindIndex(Function(p) p.IdProductoBodega = IdProductoBodega AndAlso
                                                               p.Cantidad = cantidad_recibida AndAlso
                                                               p.Lic_plate = lic_plate)
            End If

            If lIndex > -1 Then

                pIdOrdenCompraEnc = 0

                If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then
                    pIdOrdenCompraEnc = gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc
                End If

                If gBeRecepcionEnc.Estado <> "Cerrado" Then

                    If IdRecepcionEnc > 0 And IdRecepcionDet > 0 Then
                        '#GT17102024: El método se usa para eliminar linea de recepcion en la HH.
                        clsLnTrans_re_det.Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet(pIdOrdenCompraEnc,
                                                                                          IdRecepcionEnc,
                                                                                          IdRecepcionDet, AP.HostName)

                        pListBeStockRec.RemoveAt(lIndex)
                        currentView.DeleteRow(currentView.FocusedRowHandle)
                    Else
                        pListBeStockRec.RemoveAt(lIndex)
                        currentView.DeleteRow(currentView.FocusedRowHandle)
                    End If

                End If

            Else
                If Not currentView Is Nothing Then
                    currentView.DeleteRow(currentView.FocusedRowHandle)
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Existe_Lote_Con_Vencimiento_Diferente(ByVal IndiceFila As Integer,
                                                           ByVal CodigoProducto As String,
                                                           ByVal Lote As String,
                                                           ByVal FechaVence As Date) As Boolean

        Existe_Lote_Con_Vencimiento_Diferente = False

        Dim vLoteGrid As String = ""
        Dim vFechaVenceGrid As String = ""
        Dim vCodigoProducto As String = ""

        For i As Integer = 0 To DgridDetalleRec.Rows.Count - 1

            vLoteGrid = DgridDetalleRec.Rows(i).Cells("lote").Value
            vFechaVenceGrid = DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value
            vCodigoProducto = DgridDetalleRec.Rows(i).Cells("ProductoP").Value

            If (vCodigoProducto = CodigoProducto) AndAlso (vLoteGrid = Lote) AndAlso (CDate(vFechaVenceGrid).Date <> FechaVence.Date) Then
                Existe_Lote_Con_Vencimiento_Diferente = True
                Exit For
            End If

        Next

    End Function

    Private Function Grid_Tiene_Errores() As Boolean

        Grid_Tiene_Errores = False

        Try
            ' Obtener el GridView desde el GridControl
            Dim view As GridView = gvDetalleRec2

            If view.RowCount > 0 Then
                For rowIndex As Integer = 0 To view.RowCount - 1
                    ' Verificar si la fila actual tiene errores en cualquier columna
                    If view.HasColumnErrors Then
                        Return True ' Si hay errores, devolver True
                    End If

                    ' Alternativamente, puedes verificar cada columna individualmente
                    For Each col As GridColumn In view.Columns
                        ' Verificar si hay error en la columna específica
                        If Not String.IsNullOrEmpty(view.GetColumnError(col)) Then
                            Return True ' Si hay error en la columna, devolver True
                        End If
                    Next
                Next
            Else
                If gvDetalleRec2.IsNewItemRow(gvDetalleRec2.FocusedRowHandle) Then
                    If XtraMessageBox.Show("¿La fila no se ha confirmado, cancelar la edición de la fila?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        gvDetalleRec2.CancelUpdateCurrentRow()
                        Return True
                    End If
                End If
            End If

        Catch ex As Exception
            ' Manejo de cualquier excepción que pueda ocurrir
            MessageBox.Show("Ocurrió un error durante el proceso de validación o guardado: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function Get_Cantidad_Recibidad_Grid2(ByVal No_Linea As String,
                                                  ByVal pFocus As Integer) As Double

        Get_Cantidad_Recibidad_Grid2 = 0

        Try

            Dim vCantidadRecibida As Double = 0

            vCantidadRecibida = clsLnTrans_re_det.getCantidadRecibida(No_Linea,
                                                                      gBeRecepcionEnc.OrdenCompraRec.OC.IdOrdenCompraEnc,
                                                                      gBeRecepcionEnc.IdRecepcionEnc)

            For i As Integer = 0 To gvDetalleRec2.RowCount - 1

                Dim no_linea_grid As Object = gvDetalleRec2.GetRowCellValue(i, "No_Linea")
                Dim cantidad_recibida_grid As Object = gvDetalleRec2.GetRowCellValue(i, "cantidad_recibida")
                Dim vFocus As Integer = gvDetalleRec2.GetRowHandle(i)

                If vFocus <> pFocus Then

                    If (no_linea_grid = No_Linea) Then
                        vCantidadRecibida += Val(cantidad_recibida_grid.ToString())
                    End If

                End If

            Next

            Get_Cantidad_Recibidad_Grid2 = vCantidadRecibida

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Existe_Lote_Con_Vencimiento_Diferente2(ByVal CodigoProducto As String,
                                                            ByVal Lote As String,
                                                            ByVal FechaVence As Date) As Boolean


        Existe_Lote_Con_Vencimiento_Diferente2 = False

        Try

            For i As Integer = 0 To gvDetalleRec2.RowCount - 1

                Dim lote_grid As Object = gvDetalleRec2.GetRowCellValue(i, "lote")
                Dim fecha_vence_grid As Object = gvDetalleRec2.GetRowCellValue(i, "fecha_vence")
                Dim codigo_producto_grid As Object = gvDetalleRec2.GetRowCellValue(i, "codigo_producto")

                If (codigo_producto_grid = CodigoProducto) AndAlso
                    (lote_grid = Lote) AndAlso
                    (CDate(fecha_vence_grid).Date <> FechaVence.Date) Then
                    Existe_Lote_Con_Vencimiento_Diferente2 = True
                    Exit For
                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Actualiza_Cantidad_Pendiente(ByVal NoLinea As String) As Boolean

        Actualiza_Cantidad_Pendiente = False

        Dim BeOrdenCompraDet As New clsBeTrans_oc_det

        Try

            If Not gBeOrdenCompra Is Nothing Then

                If Not gBeOrdenCompra.DetalleOC Is Nothing Then

                    If gBeOrdenCompra.DetalleOC.Count > 0 Then

                        BeOrdenCompraDet = gBeOrdenCompra.DetalleOC.Find(Function(x) x.No_Linea = NoLinea)

                        If Not BeOrdenCompraDet Is Nothing Then

                            Dim vCantidadRecibidaGrid As Double = Get_Cantidad_Recibidad_Grid2(NoLinea, gvDetalleRec2.FocusedRowHandle)

                            gvDetalleRec2.SetRowCellValue(gvDetalleRec2.FocusedRowHandle, "cantidad_pendiente", BeOrdenCompraDet.Cantidad - vCantidadRecibidaGrid)

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private gIdMaxIdRecepcionDet As Integer = 0

    '#GT07022024: proceso para guardar por linea correcta en el grid
    Private Sub Llena_Detalle_Recepcion_By_Linea_Nueva(ByVal pIsNewRow As Boolean,
                                                       ByVal pBeProducto As clsBeProducto,
                                                       ByVal lConnection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction,
                                                       ByVal view As GridView,
                                                       ByVal e As ValidateRowEventArgs)

        Try

            Dim vIndice As Integer = 0
            Dim vlBeStockRec As New List(Of clsBeStock_rec)
            Dim BePres As clsBeProducto_Presentacion
            Dim vPresentacionRequiereLicencia As Boolean = False
            Dim lPesoPresentacion As Double = 0.0

            Dim vLineaOC As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "No_Linea")), 0, view.GetRowCellValue(e.RowHandle, "No_Linea"))
            Dim vIdOrdenCompraEnc As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdOrdenCompraEnc")), 0, view.GetRowCellValue(e.RowHandle, "IdOrdenCompraEnc"))
            Dim vIdOrdenCompraDet As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdOrdenCompraDet")), 0, view.GetRowCellValue(e.RowHandle, "IdOrdenCompraDet"))
            Dim IdProductoBodega As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdProductoBodega")), 0, view.GetRowCellValue(e.RowHandle, "IdProductoBodega"))
            Dim vCodigoProducto As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "codigo_producto")), "", view.GetRowCellValue(e.RowHandle, "codigo_producto"))
            Dim vIdPresentacion As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdPresentacion")), 0, view.GetRowCellValue(e.RowHandle, "IdPresentacion"))
            Dim vnombre_presentacion As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "nombre_presentacion")), "", view.GetRowCellValue(e.RowHandle, "nombre_presentacion"))
            Dim vIdUnidadMedida As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdUnidadMedida")), 0, view.GetRowCellValue(e.RowHandle, "IdUnidadMedida"))
            Dim vnombre_unidad_medida As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "nombre_unidad_medida")), "", view.GetRowCellValue(e.RowHandle, "nombre_unidad_medida"))
            Dim vCantidad As Double = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "cantidad_recibida")), 0, view.GetRowCellValue(e.RowHandle, "cantidad_recibida"))
            Dim vPeso As Double = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "peso")), 0, view.GetRowCellValue(e.RowHandle, "peso"))
            Dim vcosto_oc As Double = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "costo_oc")), 0, view.GetRowCellValue(e.RowHandle, "costo_oc"))
            Dim vcosto As Double = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "costo")), 0, view.GetRowCellValue(e.RowHandle, "costo"))
            Dim vFechaVencimiento As Date = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "fecha_vence")), New Date(1900, 1, 1), view.GetRowCellValue(e.RowHandle, "fecha_vence"))
            Dim vIdProductoEstado As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdProductoEstado")), 0, view.GetRowCellValue(e.RowHandle, "IdProductoEstado"))
            Dim vnombre_producto_estado As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "nombre_producto_estado")), "", view.GetRowCellValue(e.RowHandle, "nombre_producto_estado"))
            Dim vLote As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "lote")), "", view.GetRowCellValue(e.RowHandle, "lote"))
            Dim vobservacion As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "observacion")), "", view.GetRowCellValue(e.RowHandle, "observacion"))
            Dim vLicencia As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "lic_plate")), "", view.GetRowCellValue(e.RowHandle, "lic_plate"))
            Dim vatributo_variante_1 As String = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "atributo_variante_1")), "", view.GetRowCellValue(e.RowHandle, "atributo_variante_1"))
            Dim vIdRecepcionEnc As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdRecepcionEnc")), 0, view.GetRowCellValue(e.RowHandle, "IdRecepcionEnc"))
            Dim vIdRecepcionDet As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdRecepcionDet")), 0, view.GetRowCellValue(e.RowHandle, "IdRecepcionDet"))
            Dim vIdMotivoDevolucion As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdMotivoDevolucion")), 0, view.GetRowCellValue(e.RowHandle, "IdMotivoDevolucion"))
            Dim vIsNewRow As Boolean = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IsNewR")), False, view.GetRowCellValue(e.RowHandle, "IsNewR"))

            If pBeProducto.IdProductoBodega > 0 Then

                BePres = Nothing

                If pIsNewRow Then

                    pBeTransReDet.IdPropietarioBodega = cmbPropietario.EditValue
                    pBeTransReDet.Producto = New clsBeProducto()
                    pBeTransReDet.Producto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeProducto.IdProductoBodega, lConnection, lTransaction)
                    pBeTransReDet.Producto.Codigo = vCodigoProducto
                    pBeTransReDet.Codigo_Producto = vCodigoProducto
                    pBeTransReDet.IdProductoBodega = pBeProducto.IdProductoBodega
                    pBeTransReDet.Nombre_producto = pBeProducto.Nombre
                    pBeTransReDet.Atributo_Variante_1 = vatributo_variante_1
                    pBeTransReDet.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc

                    If gIdMaxIdRecepcionDet = 0 Then
                        pBeTransReDet.IdRecepcionDet = clsLnTrans_re_det.MaxID(pBeTransReDet.IdRecepcionEnc, lConnection, lTransaction) + 1
                    Else
                        pBeTransReDet.IdRecepcionDet = gIdMaxIdRecepcionDet + 1
                    End If

                    If pListBeStockRec Is Nothing Then
                        Throw New Exception("Algo hicimos mal.")
                    Else
                        If pListBeStockRec.Count = 0 Then
                            Throw New Exception("Algo hicimos mal.")
                        End If
                    End If

                    If Modo = TipoTrans.Editar Then
                        vIndice = pListBeStockRec.Count - 1
                    End If

                    '#GT12022024: asignamos la recepcion detalle al stock rec
                    pListBeStockRec(vIndice).IdRecepcionDet = pBeTransReDet.IdRecepcionDet
                    vIdRecepcionDet = pBeTransReDet.IdRecepcionDet
                    pBeTransReDet.IdUbicacion = txtIdUbicacion.EditValue
                    pBeTransReDet.IdUbicacionAnterior = txtIdUbicacion.EditValue

                    '#GT24012024: validar Presentacion y UmBas
                    pBeTransReDet.Presentacion = New clsBeProducto_Presentacion
                    pBeTransReDet.UnidadMedida = New clsBeUnidad_medida

                    If vIdPresentacion > 0 Then
                        pBeTransReDet.Nombre_presentacion = vnombre_presentacion
                        pBeTransReDet.Presentacion.IdPresentacion = vIdPresentacion
                        pBeTransReDet.IdPresentacion = vIdPresentacion
                        pBeTransReDet.UnidadMedida.IdUnidadMedida = vIdUnidadMedida
                        pBeTransReDet.Nombre_unidad_medida = vnombre_unidad_medida
                    Else
                        pBeTransReDet.IdUnidadMedida = vIdUnidadMedida
                        pBeTransReDet.UnidadMedida.IdUnidadMedida = vIdUnidadMedida
                        pBeTransReDet.Nombre_unidad_medida = vnombre_unidad_medida
                    End If

                    pBeTransReDet.Peso = vPeso
                    pBeTransReDet.Nombre_producto_estado = vnombre_producto_estado
                    pBeTransReDet.Costo = vcosto
                    pBeTransReDet.cantidad_recibida = Math.Round(vCantidad, 6)
                    pBeTransReDet.No_Linea = vLineaOC
                    pBeTransReDet.Lic_plate = vLicencia

                    '#GT09022024: producto estado
                    pBeTransReDet.ProductoEstado = New clsBeProducto_estado
                    pBeTransReDet.ProductoEstado.IdEstado = vIdProductoEstado
                    pBeTransReDet.IdProductoEstado = vIdProductoEstado
                    pBeTransReDet.Lote = vLote.ToUpper()

                    '#GT09022024: control vencimiento
                    If BeProducto.Control_vencimiento Then
                        pBeTransReDet.Fecha_vence = vFechaVencimiento
                    Else
                        pBeTransReDet.Fecha_vence = New Date(1900, 1, 1)
                    End If

                    pBeTransReDet.Fecha_ingreso = Now

                    '#GT09022024: validar genera lp en la presentacion
                    Dim pReDet As clsBeTrans_re_det = gBeRecepcionEnc.Detalle.Find(Function(x) x.IdRecepcionEnc = pBeTransReDet.IdRecepcionEnc AndAlso
                                                                                                   x.IdRecepcionDet = pBeTransReDet.IdRecepcionDet AndAlso
                                                                                                   x.No_Linea = pBeTransReDet.No_Linea)

                    If BePres Is Nothing AndAlso pBeTransReDet.IdPresentacion <> 0 Then
                        BePres = New clsBeProducto_Presentacion()
                        BePres = clsLnProducto_presentacion.Get_BeProductoPresentacion_By_IdPresentacion(pBeTransReDet.IdPresentacion, lConnection, lTransaction)
                        If Not BePres Is Nothing Then
                            vPresentacionRequiereLicencia = BePres.Genera_lp_auto
                        End If
                    End If

                    If pBeTransReDet.Producto.Genera_lp OrElse vPresentacionRequiereLicencia Then
                        If pBeTransReDet.Lic_plate = "" Then
                            If pReDet Is Nothing Then
                                Throw New Exception("ERROR_20220912_1550B: Producto sin LP no se puede actualizar.")
                            Else
                                pBeTransReDet.Lic_plate = pReDet.Lic_plate
                            End If
                        End If
                    End If

                    If Not (gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MSOC00") AndAlso Not (gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MCOC00") Then
                        If pBeTransReDet.IdOperadorBodega = 0 Then
                            If Not pReDet Is Nothing Then
                                pBeTransReDet.IdOperadorBodega = pReDet.IdOperadorBodega
                            End If
                        End If
                    End If

                    pBeTransReDet.Observacion = vobservacion
                    pBeTransReDet.IdOrdenCompraEnc = vIdOrdenCompraEnc
                    pBeTransReDet.IdOrdenCompraDet = vIdOrdenCompraDet

                    Dim BeProductoEstado As New clsBeProducto_estado
                    BeProductoEstado = clsLnProducto_estado.GetSingle(vIdProductoEstado, lConnection, lTransaction)
                    Dim BeUbicacion As New clsBeBodega_ubicacion

                    If Not BeProductoEstado Is Nothing Then
                        If BeProductoEstado.IdUbicacionDefecto <> 0 Then

                            BeUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(BeProductoEstado.IdUbicacionBodegaDefecto,
                                                                                                       IdBodega,
                                                                                                       lConnection,
                                                                                                       lTransaction)
                        End If
                    End If

                    '#GT25012024
                    ' CAMPOS FALTANTES STOCK DE ASIGNACIÓN
                    Dim ControlVencimiento As Boolean = BeProducto.Control_vencimiento

                    If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then

                        pListBeStockRec(vIndice).IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc

                        If NavConfigEnc.Interface_SAP Then

                            'Hay que buscar si el documento es una compra o una transferencia.
                            'Si el documento es una transferencia el proveedor indica el origen (IdAreaOrigen)
                            'y el cliente indica el destino (idAreaDestino)
                            'Si el documento es una compra, la ubicación por defecto proviene del área asociada al estado.
                            'El proveedor tiene el IdAreaSAP
                            'El Proveedor tiene el IdUbicaciónAreaSAP (Defecto para recepción).
                            'La ubicación se debe buscar en el proveedor en base el idubicaciónareasap.
                            'Mapeo de IdUbicaciónRef definida por Bodega_Area, inferida por el estado para el stock.

                            Dim BeBodegaArea As New clsBeBodega_area
                            Dim vUbicacionDefecto As Integer = 0

                            If BeProductoEstado IsNot Nothing Then

                                BeBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(BeProductoEstado.Codigo_Bodega_ERP, lConnection, lTransaction)

                                If BeBodegaArea IsNot Nothing Then
                                    vUbicacionDefecto = BeBodegaArea.IdUbicacionRef
                                End If

                            End If

                            If vUbicacionDefecto = 0 Then
                                If Not BeUbicacion Is Nothing Then
                                    vUbicacionDefecto = BeUbicacion.IdUbicacion
                                End If
                            End If

                            If vUbicacionDefecto = 0 Then
                                vUbicacionDefecto = Val(txtIdUbicacion.Text)
                            End If

                            If vUbicacionDefecto = 0 Then
                                Throw New Exception("ERROR_20240213: No se definió la ubicación por defecto para el estado de producto.")
                            End If

                            pListBeStockRec(vIndice).IdUbicacion = vUbicacionDefecto
                            pListBeStockRec(vIndice).IdUbicacion_anterior = vUbicacionDefecto


                        Else

                            If Not BeProductoEstado Is Nothing Then

                                If BeProductoEstado.IdUbicacionDefecto <> 0 Then

                                    pListBeStockRec(vIndice).IdUbicacion = BeProductoEstado.IdUbicacionDefecto
                                    pListBeStockRec(vIndice).IdUbicacion_anterior = BeProductoEstado.IdUbicacionDefecto

                                Else

                                    pListBeStockRec(vIndice).IdUbicacion = txtIdUbicacion.Text
                                    pListBeStockRec(vIndice).IdUbicacion_anterior = txtIdUbicacion.Text

                                End If

                            Else

                                pListBeStockRec(vIndice).IdUbicacion = txtIdUbicacion.Text
                                pListBeStockRec(vIndice).IdUbicacion_anterior = txtIdUbicacion.Text

                            End If

                        End If

                        pListBeStockRec(vIndice).Cantidad = vCantidad
                        pListBeStockRec(vIndice).IdBodega = gBeRecepcionEnc.IdBodega
                        pListBeStockRec(vIndice).IdProductoEstado = vIdProductoEstado
                        pListBeStockRec(vIndice).Lic_plate = vLicencia
                        pListBeStockRec(vIndice).Lote = vLote.ToUpper()
                        pListBeStockRec(vIndice).Activo = True
                        pListBeStockRec(vIndice).User_agr = AP.UsuarioAp.IdUsuario
                        pListBeStockRec(vIndice).IdUnidadMedida = vIdUnidadMedida
                        pListBeStockRec(vIndice).IdProductoBodega = IdProductoBodega

                        pBeTransReDet.ProductoEstado = New clsBeProducto_estado
                        pBeTransReDet.ProductoEstado.IdEstado = vIdProductoEstado
                        pBeTransReDet.IdProductoEstado = vIdProductoEstado

                        Dim listaProdPallets As New List(Of clsBeProducto_pallet)
                        Dim listaStockPalletsNuevos As New List(Of clsBeStock_rec)

                        If pBeTransReDet.Presentacion.IdPresentacion <> 0 Then

                            vlBeStockRec = pListBeStockRec.FindAll(Function(p) _
                                                                   p.IdProductoBodega = pBeTransReDet.IdProductoBodega _
                                                                   AndAlso p.ProductoValidado = False _
                                                                   AndAlso p.Presentacion.IdPresentacion = pBeTransReDet.Presentacion.IdPresentacion _
                                                                   AndAlso p.IdRecepcionDet = pBeTransReDet.IdRecepcionDet _
                                                                   AndAlso p.IdProductoEstado = pBeTransReDet.IdProductoEstado _
                                                                   AndAlso p.No_linea = pBeTransReDet.No_Linea)

                            If pListBeProductoPallet IsNot Nothing AndAlso pListBeProductoPallet.Count > 0 Then

                                listaProdPallets = pListBeProductoPallet.FindAll(Function(p) _
                                                                                 p.IdRecepcionDet = pBeTransReDet.IdRecepcionDet _
                                                                                 AndAlso p.IdProductoBodega = pBeTransReDet.IdProductoBodega _
                                                                                 AndAlso p.IdPresentacion = pBeTransReDet.Presentacion.IdPresentacion)

                                Dim BePresPP As New clsBeProducto_Presentacion With {.IdPresentacion = pBeTransReDet.Presentacion.IdPresentacion}
                                clsLnProducto_presentacion.GetSingle(BePresPP, lConnection, lTransaction)

                                For Each BePP As clsBeProducto_pallet In listaProdPallets

                                    With BePP
                                        .Lote = vLote.ToUpper()
                                        .User_agr = AP.UsuarioAp.IdUsuario
                                        .User_mod = AP.UsuarioAp.IdUsuario
                                        .Cantidad = Math.Round((1 * BePresPP.Factor * BePresPP.CajasPorCama * BePresPP.CamasPorTarima), 6)
                                        If ControlVencimiento Then
                                            .Fecha_vence = vFechaVencimiento
                                        Else
                                            .Fecha_vence = Nothing
                                        End If

                                    End With

                                Next

                            End If

                        Else

                            vlBeStockRec = pListBeStockRec.FindAll(Function(p) _
                                                                   p.IdProductoBodega = pBeTransReDet.IdProductoBodega _
                                                                   AndAlso p.ProductoValidado = False _
                                                                   AndAlso p.IdUnidadMedida = pBeTransReDet.IdUnidadMedida _
                                                                   AndAlso p.IdPresentacion = 0 _
                                                                   AndAlso p.IdRecepcionDet = pBeTransReDet.IdRecepcionDet _
                                                                   AndAlso p.IdProductoEstado = pBeTransReDet.IdProductoEstado _
                                                                   AndAlso p.No_linea = pBeTransReDet.No_Linea)

                        End If

                        For Each BeStockRec As clsBeStock_rec In vlBeStockRec

                            BeStockRec.IdRecepcionEnc = pBeTransReDet.IdRecepcionEnc
                            BeStockRec.ProductoEstado = New clsBeProducto_estado
                            BeStockRec.ProductoEstado.IdEstado = vIdProductoEstado
                            BeStockRec.IdProductoEstado = vIdProductoEstado
                            BeStockRec.Presentacion = New clsBeProducto_Presentacion
                            BeStockRec.Presentacion.IdPresentacion = vIdPresentacion
                            BeStockRec.IdPresentacion = vIdPresentacion
                            BeStockRec.Atributo_Variante_1 = vatributo_variante_1
                            BeStockRec.IdUnidadMedida = vIdUnidadMedida
                            BeStockRec.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc
                            BeStockRec.Lote = vLote.ToUpper()

                            If ControlVencimiento Then
                                BeStockRec.Fecha_vence = vFechaVencimiento
                            Else
                                BeStockRec.Fecha_vence = Nothing
                            End If

                            ' BeStockRec.IdUbicacion = CInt(txtIdUbicacion.Text.Trim)
                            'BeStockRec.IdUbicacion_anterior = CInt(txtIdUbicacion.Text.Trim)
                            BeStockRec.ProductoValidado = True
                            BeStockRec.Fecha_Ingreso = Now
                            BeStockRec.Fec_agr = Now
                            BeStockRec.User_agr = AP.UsuarioAp.IdUsuario
                            BeStockRec.User_mod = AP.UsuarioAp.IdUsuario

                            If BeStockRec.Presentacion.IdPresentacion <> 0 Then

                                BePres.IdPresentacion = BeStockRec.Presentacion.IdPresentacion
                                clsLnProducto_presentacion.GetSingle(BePres, lConnection, lTransaction)

                                If BePres.EsPallet Then

                                    If vCantidad = 1 Then
                                        BeStockRec.Cantidad = Math.Round((vCantidad * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima), 6)
                                    Else

                                        If BePres.Genera_lp_auto Then

                                            Dim vCantidadPallets As Integer = vCantidad
                                            Dim vIndiceLista As Integer = 0
                                            Dim vBeStockRec As New clsBeStock_rec
                                            Dim BeProdPallet As New clsBeProducto_pallet

                                            For x As Integer = 1 To vCantidadPallets - 1

                                                vIndiceLista = listaStockPalletsNuevos.Count - 1

                                                If x = 1 Then
                                                    clsPublic.CopyObject(BeStockRec, vBeStockRec)
                                                    vBeStockRec.Cantidad = (1 * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima)
                                                Else
                                                    vBeStockRec = New clsBeStock_rec
                                                    clsPublic.CopyObject(listaStockPalletsNuevos(vIndiceLista), vBeStockRec)
                                                End If

                                                vBeStockRec.Cantidad = (1 * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima)

                                                vBeStockRec.Lic_plate = Genera_Licencia_BOF(cmbBodega.EditValue, AP.UsuarioAp.IdUsuario)

                                                listaStockPalletsNuevos.Add(vBeStockRec)

                                                If BePres.Imprime_barra Then

                                                    BeProdPallet = New clsBeProducto_pallet

                                                    With BeProdPallet
                                                        .IdPropietarioBodega = pBeTransReDet.IdPropietarioBodega
                                                        .IdProductoBodega = pBeTransReDet.IdProductoBodega
                                                        .IdOperadorBodega = Nothing
                                                        .IdPresentacion = BePres.IdPresentacion
                                                        .IdRecepcionDet = vBeStockRec.IdRecepcionDet
                                                        .Impreso = 0
                                                        .IdImpresora = 1
                                                        .Activo = True
                                                        .Fecha_ingreso = Now
                                                        .Codigo_Barra = vBeStockRec.Lic_plate
                                                        .Reimpresiones = 0
                                                        .Cantidad = vBeStockRec.Cantidad
                                                        .Fecha_vence = IIf(ControlVencimiento, vBeStockRec.Fecha_vence, Nothing)
                                                        .Fec_agr = Now
                                                        .Fec_mod = Now
                                                        .Lote = vBeStockRec.Lote.ToUpper()
                                                        .User_agr = AP.UsuarioAp.IdUsuario
                                                        .User_mod = AP.UsuarioAp.IdUsuario
                                                        .IsNew = True
                                                    End With

                                                    pListProductoPallet.Add(BeProdPallet)
                                                    lBeProdPallet.Add(BeProdPallet)

                                                End If

                                            Next

                                        Else
                                            Throw New Exception("La cantidad de pallets es > 1 y genera_lp_auto es Falso, debe recibir los pallets de forma unitaria (Cantidad = 1)")
                                        End If

                                    End If

                                Else

                                    BeStockRec.Cantidad = Math.Round((vCantidad * BePres.Factor), 6)

                                    If BePres.Imprime_barra Then

                                        Dim BeProdPallet As New clsBeProducto_pallet With {
                                                .IdPropietarioBodega = pBeTransReDet.IdPropietarioBodega,
                                                .IdProductoBodega = pBeTransReDet.IdProductoBodega,
                                                .IdPresentacion = BePres.IdPresentacion,
                                                .IdRecepcionDet = pBeTransReDet.IdRecepcionDet,
                                                .IdOperadorBodega = Nothing,
                                                .Impreso = 0,
                                                .IdImpresora = 1,
                                                .Activo = True,
                                                .Fecha_ingreso = Now,
                                                .Codigo_Barra = BePres.Codigo_barra,
                                                .Reimpresiones = 0,
                                                .Cantidad = BeStockRec.Cantidad,
                                                .Fecha_vence = BeStockRec.Fecha_vence,
                                                .Fec_agr = Now,
                                                .Fec_mod = Now,
                                                .Lote = BeStockRec.Lote.ToUpper(),
                                                .User_agr = AP.UsuarioAp.IdUsuario,
                                                .User_mod = AP.UsuarioAp.IdUsuario,
                                                .IsNew = True}

                                        listaStockPalletsNuevos.Add(BeStockRec)
                                        listaProdPallets.Add(BeProdPallet)
                                        lBeProdPallet.Add(BeProdPallet)

                                    End If

                                End If

                            Else

                                BeStockRec.Cantidad = Math.Round((vCantidad), 6)
                                BeStockRec.IdRecepcionDet = vIdRecepcionDet

                                Dim vBestockRec As New clsBeStock_rec()
                                vBestockRec = BeStockRec.Clone()
                                listaStockPalletsNuevos.Add(vBestockRec)

                            End If

                        Next

                        For Each PP As clsBeProducto_pallet In pListProductoPallet
                            listaProdPallets.Add(PP)
                        Next

                        For Each PP As clsBeProducto_pallet In listaProdPallets
                            lBeProdPallet.Add(PP)
                        Next

                        pBeTransReDet.User_agr = AP.UsuarioAp.IdUsuario
                        pListBeStockRec(vIndice).IdRecepcionDet = vIdRecepcionDet

                        gBeRecepcionEnc.Detalle = New List(Of clsBeTrans_re_det)
                        gBeRecepcionEnc.Detalle.Add(pBeTransReDet)

                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message

            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace,
                                                 pIdRecEnc:=gBeRecepcionEnc.IdRecepcionEnc)

            Throw ex
        End Try

    End Sub

    Private Sub gvDetalleRec2_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvDetalleRec2.ShowingEditor
        Try

            Dim view As GridView = TryCast(sender, GridView)
            Dim value As Object = view.GetRowCellValue(view.FocusedRowHandle, "IsNewR")
            If value IsNot Nothing AndAlso value.Equals(False) Then
                e.Cancel = True ' Cancela la edición para filas donde IsNew es false
            End If

            Dim lista As GridLookUpEdit = TryCast(view.ActiveEditor, GridLookUpEdit)

            If lista IsNot Nothing AndAlso lista.Properties.Tag IsNot Nothing AndAlso lista.Properties.Tag.ToString() = "filtrar" Then

                ' Aplicamos el filtro al GridView del LookupEdit basado en el código de barra almacenado en Tag

                Dim filtroCodigoBarra As String = lista.Properties.Tag.ToString()

                lista.Properties.View.ActiveFilter.NonColumnFilter = $"[CodigoBarra] = '{filtroCodigoBarra}'"

                lista.Properties.Tag = Nothing  ' Resetear el tag después de aplicar el filtro

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvDetalleRec2_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gvDetalleRec2.RowCellStyle

        Try

            '#EJC202402130656: Deshabilitar la fila si ya fue creada.
            Dim view As GridView = TryCast(sender, GridView)
            Dim IsNew As Boolean = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IsNewR")), False, view.GetRowCellValue(e.RowHandle, "IsNewR"))
            Dim IdProductoBodega As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "IdProductoBodega")), 0, view.GetRowCellValue(e.RowHandle, "IdProductoBodega"))
            Dim vCantidadPendiente As Integer = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "cantidad_pendiente")), 0, view.GetRowCellValue(e.RowHandle, "cantidad_pendiente"))
            Dim Bono As Boolean = IIf(IsDBNull(view.GetRowCellValue(e.RowHandle, "Bono")), False, view.GetRowCellValue(e.RowHandle, "Bono"))
            'e.Appearance.BackColor = If(IsNew, Color.White, Color.LightYellow)

            If IsNew Then
                If Bono Then
                    e.Appearance.BackColor = Color.LightSalmon
                Else
                    e.Appearance.BackColor = Color.White
                End If
            Else
                e.Appearance.BackColor = Color.LightYellow
            End If

        Catch ex As Exception
            Debug.WriteLine("ocurrió un errorcito: " & ex.Message)
        End Try

    End Sub

    Private Sub gvDetalleRec2_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles gvDetalleRec2.CustomDrawCell

        Try

            Dim view As GridView = TryCast(sender, GridView)

            If e.Column.FieldName = "IsNewR" Then
                Dim value As Object = view.GetRowCellValue(e.RowHandle, "IsNewR")
                If value IsNot Nothing AndAlso value.Equals(False) Then
                    e.Appearance.BackColor = Color.LightGray ' Cambia el color para deshabilitar visualmente
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdPrintLabels_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPrintLabels.ItemClick

        Try

            If tabDetalleRecepcion2.Visible Then

                Dim currentView As GridView = DgridDetalleRec2.FocusedView

                '#GT11102022_1500: validar que es una fila con datos
                If currentView IsNot Nothing AndAlso currentView.SelectedRowsCount = 1 Then

                    Dim vCodigoProducto As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "No_Linea")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "No_Linea"))
                    Dim vIdRecepcionDet As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionDet")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionDet"))
                    Dim vIdRecepcionEnc As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionEnc")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionEnc"))
                    Dim vCantidad_recibida As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "cantidad_recibida")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "cantidad_recibida"))
                    Dim vLic_plate As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "lic_plate")), "", currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "lic_plate"))

                    If vIdRecepcionEnc <> "" Then

                        Dim ObjTransReDet = clsLnTrans_re_det.Get_Recepcion_By_IdRecepcionEnc(vIdRecepcionEnc, vIdRecepcionDet)

                        If ObjTransReDet IsNot Nothing Then
                            Dim Impresion As New frmImpresionRecepcion
                            Impresion.pTransReDet = ObjTransReDet
                            Impresion.ShowDialog()
                            Impresion.Dispose()
                        Else
                            XtraMessageBox.Show("No se cargo el producto para reimpresión.", "Impresión BOF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Else
                        XtraMessageBox.Show("El Id de Recepción no es válido.", "Impresión BOF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Else
                    XtraMessageBox.Show("No hay una fila seleccionada para reimpresión.", "Impresión BOF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                XtraMessageBox.Show("Para reimpresion, debe ubicarse en la pestaña Detalle Recepcion.", "Impresión BOF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gvDetalleRec2_RowEditCanceled(sender As Object, e As RowObjectEventArgs) Handles gvDetalleRec2.RowEditCanceled

        Try

            If gBeRecepcionEnc.Estado <> "Cerrado" Then

                Dim lBeStockRec As New clsBeStock_rec
                lBeStockRec = pListBeStockRec.FindLast(Function(x) x.IdRecepcionEnc = 0)

                If lBeStockRec IsNot Nothing Then
                    pListBeStockRec.Remove(lBeStockRec)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gvDetalleRec2_KeyDown(sender As Object, e As KeyEventArgs) Handles gvDetalleRec2.KeyDown

        If e.KeyCode = Keys.Escape Then
            If gvDetalleRec2.IsNewItemRow(gvDetalleRec2.FocusedRowHandle) Then
                If XtraMessageBox.Show("¿Cancelar la edición de la fila?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    gvDetalleRec2.CancelUpdateCurrentRow()
                End If
            End If
        End If

    End Sub

    Private Sub xtrRecepcion_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtrRecepcion.SelectedPageChanged

        Try

            If sender.SelectedTabPage Is tabDetalleRecepcion2 Then

                Dim ColLinea As GridColumn = gvDetalleRec2.Columns("No_Linea")

                If Not ColLinea Is Nothing Then

                    DgridDetalleRec2.BeginInvoke(New MethodInvoker(Sub()
                                                                       gvDetalleRec2.FocusedRowHandle = GridControl.NewItemRowHandle
                                                                       gvDetalleRec2.FocusedColumn = ColLinea
                                                                       gvDetalleRec2.MakeColumnVisible(ColLinea)
                                                                       If gvDetalleRec2.FocusedColumn IsNot Nothing Then
                                                                           gvDetalleRec2.ShowEditor()
                                                                       End If
                                                                   End Sub))

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvDetalleRec2_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles gvDetalleRec2.CustomColumnDisplayText

        Try

            If e.Column.FieldName = "Lote" Then

                If Not e.DisplayText Is Nothing Then
                    e.DisplayText = e.DisplayText.ToUpper()
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtLoteGrid_Validating(sender As Object, e As CancelEventArgs)
        Dim editor As TextEdit = TryCast(sender, TextEdit)
        If editor IsNot Nothing Then
            Dim currentValue As String = TryCast(editor.EditValue, String)
            If currentValue IsNot Nothing Then
                editor.EditValue = currentValue.ToUpper()
            End If
        End If
    End Sub

    Private Sub cmbMuelle_EditValueChanged(sender As Object, e As EventArgs) Handles cmbMuelle.EditValueChanged

        Try

            If cmbMuelle.EditValue > -1 Then

                Dim BeBodegaMuelle As New clsBeBodega_muelles
                BeBodegaMuelle = clsLnBodega_muelles.GetSingle(cmbMuelle.EditValue)

                If Not BeBodegaMuelle Is Nothing Then
                    If Not BeBodegaMuelle.IdUbicacionDefecto = 0 Then
                        txtIdUbicacion.Text = BeBodegaMuelle.IdUbicacionDefecto
                        Valida_Ubicacion()
                    End If
                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Get_IdRecepcionDet_Rec_BOF() As Integer
        Dim maxIdRecepcionDet As Integer = 0

        Try
            ' Recorrer cada fila del DataGridView para encontrar el máximo IdRecepcionDet
            For Each Fila As DataGridViewRow In DgridDetalleRec.Rows
                If Not Fila.IsNewRow Then ' Verificar que la fila no sea una fila nueva no guardada
                    Dim currentId As Integer = Convert.ToInt32(Fila.Cells("IdRecepcionDet").Value)
                    ' Actualizar maxIdRecepcionDet si el Id actual es mayor
                    If currentId > maxIdRecepcionDet Then
                        maxIdRecepcionDet = currentId
                    End If
                End If
            Next
            ' Sumar uno al máximo Id encontrado
            Return maxIdRecepcionDet + 1

        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick

        If tabDetalleRecepcion2.Visible Then

            Dim currentView As GridView = DgridDetalleRec2.FocusedView

            If currentView IsNot Nothing AndAlso currentView.SelectedRowsCount = 1 Then

                Dim vCodigoProducto As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "No_Linea")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "No_Linea"))
                Dim vIdRecepcionDet As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionDet")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionDet"))
                Dim vIdRecepcionEnc As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionEnc")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdRecepcionEnc"))
                Dim vCantidad_recibida As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "cantidad_recibida")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "cantidad_recibida"))
                Dim vLic_plate As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "lic_plate")), "", currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "lic_plate"))

                If vIdRecepcionEnc <> "" Then

                    Dim ObjTransReDet = clsLnTrans_re_det.Get_Recepcion_By_IdRecepcionEnc(vIdRecepcionEnc, vIdRecepcionDet)

                    If ObjTransReDet IsNot Nothing Then
                        Dim Impresion As New frmImpresionSojet
                        Impresion.pTransReDet = ObjTransReDet
                        Impresion.ShowDialog()
                        Impresion.Dispose()
                    Else
                        XtraMessageBox.Show("No se cargo el producto para impresión.", "Impresión Sojet BOF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Else
                    XtraMessageBox.Show("El Id de Recepción no es válido.", "Impresión Sojet BOF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                XtraMessageBox.Show("No hay una fila seleccionada para impresión.", "Impresión Sojet BOF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Else
            XtraMessageBox.Show("Para la impresión, debe ubicarse en la pestaña Detalle Recepción.", "Impresión Sojet BOF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    Private Sub txtIdEstadoDefectoRecepcion_EditValueChanged(sender As Object, e As EventArgs) Handles txtIdEstadoDefectoRecepcion.EditValueChanged
        Try

            If gBeRecepcionEnc Is Nothing Then Exit Sub

            If gBeRecepcionEnc.IdEstado_Defecto_Recepcion > 0 OrElse txtIdEstadoDefectoRecepcion.Text <> "" Then

                If gBeRecepcionEnc.IdEstado_Defecto_Recepcion = 0 Then
                    gBeRecepcionEnc.IdEstado_Defecto_Recepcion = txtIdEstadoDefectoRecepcion.Text
                End If

                Dim estadoprod As New clsBeProducto_estado
                estadoprod = clsLnProducto_estado.Get_Single_By_IdEstado(gBeRecepcionEnc.IdEstado_Defecto_Recepcion)

                If Not estadoprod Is Nothing Then
                    Dim ubicacion As Integer = estadoprod.IdUbicacionDefecto
                    If (estadoprod.IdUbicacionDefecto > 0) Then
                        txtIdUbicacion.Text = estadoprod.IdUbicacionDefecto
                        Valida_Ubicacion()
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class