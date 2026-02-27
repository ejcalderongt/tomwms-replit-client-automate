Imports System.IO
Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class frmRecepcionBOF

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

    'Private lBeStockRec As New List(Of clsBeStock_rec)
    Private lBeProdPallet As New List(Of clsBeProducto_pallet)
    Private pBeTms_ticket As New clsBeTms_ticket
    Private pBeEmpresa_Transporte_Piloto As New clsBeEmpresa_transporte_pilotos
    Private pBeEmpresa_Transporte_Vehiculo As New clsBeEmpresa_transporte_vehiculos

    Private ProductoGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PropietarioGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PresentacionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private MotivoDevolcuionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtNoLineaGrid As New RepositoryItemTextEdit
    Private txtCantidadGrid As New RepositoryItemSpinEdit
    Private txtValorFOBGrid As New RepositoryItemSpinEdit
    Private txtValorDAIGrid As New RepositoryItemSpinEdit
    Private txtTotalGrid As New RepositoryItemSpinEdit
    Private txtCostoGrid As New RepositoryItemSpinEdit
    Private BeBodega As New clsBeBodega()
    Private Control_Poliza As Boolean = False
    Private BeTipoDocumento As New clsBeTrans_oc_ti

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
                    pListObjOrdeCompraDet = BeTransOcEnc.DetalleOC.ToList

                    Cargar_Detalle_OC()

                    '#EJC20220330:Get the parameter by type of document.
                    chkEscanearUbicacionRec.Checked = BeTipoDocumento.Requerir_Ubic_Rec_Ingreso

                    cmbBodega.EditValue = BeTransOcEnc.IdBodega
                    cmbPropietario.EditValue = BeTransOcEnc.PropietarioBodega.IdPropietarioBodega
                    cmbBodega.Enabled = False
                    cmbPropietario.Enabled = False

                    '#GT26092023: Si es nueva REC, y luego se asocia a una OC, validar control poliza
                    'para mostrar carta cupo

                    If BeTransOcEnc.Control_Poliza Then
                        grpDatosFiscalSAT.Visible = True
                    Else
                        grpDatosFiscalSAT.Visible = False
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

                            SplashScreenManager.CloseForm(False)

                            XtraMessageBox.Show("Se ha creado la recepción", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            If Listar IsNot Nothing Then
                                Listar.Invoke()
                            End If

                            DialogResult = DialogResult.OK

                            Close()

                        End If

                    End If

                Else

                    If Guardar() Then

                        SplashScreenManager.CloseForm(False)

                        XtraMessageBox.Show("Se ha creado la recepción: " & gBeRecepcionEnc.IdRecepcionEnc, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If Listar IsNot Nothing Then
                            Listar.Invoke()
                        End If

                        DialogResult = DialogResult.OK

                        Close()

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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

                Else
                    Datos_Correctos = True
                End If
            Else
                Datos_Correctos = True
            End If


            If Datos_Correctos Then
                Datos_Correctos = Datos_Correctos_Por_Tipo_Ingreso()
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
                        pListObjOrdeCompraDet = BeTransOcEnc.DetalleOC.ToList

                        Cargar_Detalle_OC()

                        txtNoDocumento.Text = gBeRecepcionEnc.OrdenCompraRec.No_docto
                        chkRecepcionManual.Checked = gBeRecepcionEnc.OrdenCompraRec.Recepcion_manual
                        dtmHoraIhh.Value = gBeRecepcionEnc.OrdenCompraRec.Hora_ini_hh
                        dtmHoraFhh.Value = gBeRecepcionEnc.OrdenCompraRec.Hora_fin_hh

                        Control_Poliza = BeTransOcEnc.Control_Poliza

                    End If

                End If

                Application.DoEvents()

                chkHabilitaStock.Checked = gBeRecepcionEnc.Habilitar_Stock

                chkMostrarCantidadPI.Checked = gBeRecepcionEnc.Mostrar_Cantidad_Esperada

                If Modo = TipoTrans.Editar Then
                    chkHabilitaStock.Enabled = False
                End If

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

                If gBeRecepcionEnc.IdTipoTransaccion = "HCOC00" Or gBeRecepcionEnc.IdTipoTransaccion = "HSOC00" Then
                    DgridDetalleRec.ReadOnly = True
                Else
                    DgridDetalleRec.ReadOnly = False
                End If

                Cargar_Detalle_Recepcion()

                Application.DoEvents()

                Cargar_Imagenes()

                Cargar_Facturas()

                Dim BeTransReTr As New clsBeTrans_re_tr()
                BeTransReTr.IdTipoTransaccion = gBeRecepcionEnc.IdTipoTransaccion
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

            If vReglasPropietarioDefinidas Then
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

                If cmbMuelle.ItemIndex > -1 Then
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Detalle_Recepcion()

        Try

            Dim lPesoPresentacion As Double = 0.0

            'lBeStockRec = New List(Of clsBeStock_rec)
            lBeTransRecDet = New List(Of clsBeTrans_re_det)
            lBeProdPallet = New List(Of clsBeProducto_pallet)

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

                    DgComboEstado = TryCast(DgridDetalleRec.Rows(i).Cells("Estado"), DataGridViewComboBoxCell)
                    BeTransReDet.Nombre_producto_estado = DgComboEstado.FormattedValue

                    DgComboUnidadMedida = TryCast(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP"), DataGridViewComboBoxCell)
                    BeTransReDet.Nombre_unidad_medida = DgComboUnidadMedida.FormattedValue

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
                            BeTransReDet.IdRecepcionDet = 0
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

                    If DgridDetalleRec.Rows(i).Cells("Lote").Value IsNot Nothing Then
                        BeTransReDet.Lote = CStr(DgridDetalleRec.Rows(i).Cells("Lote").Value)
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
                                Throw New Exception("ERROR_20220912_1550: Producto sin LP no se puede actualizar.")
                            Else
                                BeTransReDet.Lic_plate = pReDet.Lic_plate
                            End If
                        End If

                    End If

                    '#EJC20221008: Si es una recepción desde BOF (Ciega) no tiene operador.
                    If Not (gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MSOC00") AndAlso Not (gBeRecepcionEnc.IdTipoTransaccion.ToString() = "MCOC00") Then
                        If BeTransReDet.IdOperadorBodega = 0 Then
                            If pReDet Is Nothing Then
                                '#EJC20240212: A raíz del cambio de grid a devexpress la recepción en BOF, no necesariamente debe llevar un IdOperadorBodega por lo que se permite que vaya en 0 para que despues se inerte como nulo.
                                'Throw New Exception("ERROR_20220912_1550: Producto sin Operador-Bodega no se puede actualizar.")
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
                                        .Lote = CStr(DgridDetalleRec.Rows(i).Cells("Lote").Value)
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

                            If DgridDetalleRec.Rows(i).Cells("Lote").Value IsNot Nothing Then
                                BeStockRec.Lote = CStr(DgridDetalleRec.Rows(i).Cells("Lote").Value)
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
                                BeStockRec.Cantidad = Math.Round((CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value)), 6)

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            DgridDetalleRec.CommitEdit(DataGridViewDataErrorContexts.Commit)
            'DgridOC.CommitEdit(DataGridViewDataErrorContexts.Commit)

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

                If DgridDetalleRec.Rows.Count > 0 Then

                    pListBeStockRec.ForEach(AddressOf Restablecer)

                    Llena_Detalle_Recepcion()

                    pListBeStockRec.ForEach(AddressOf Restablecer)

                    If lBeTransRecDet.Count = 0 Then
                        Throw New Exception("La recepción no tiene detalle")
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

                'Ingreso sin referencia HH 'Solamente crea tarea para Handheld
                If Finalizar(lBeTransRecDet) Then

                    If AP.IdConfiguracionInterface <> -1 Then
                        Ejecutar_Interface("5-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me)
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
                    {.pIdPropietario = cmbPropietario.EditValue,
                    .Modo = frmProducto_EstadoList.pModo.Seleccion}
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

                                XtraMessageBox.Show("Orden de compra cargada correctamente",
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

            For Each Obj As clsBeTrans_re_det In lBeTransRecDet

                i = DgridDetalleRec.Rows.Add(0, Obj.Producto.Codigo, Obj.Producto.Nombre)

                If Obj.UnidadMedida IsNot Nothing Then
                    setUnidadMedidaGrid2(Obj, i)
                Else
                    Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", Obj.Producto.Nombre))
                End If

                DgridDetalleRec.Rows(i).Cells("CantidadP").Value = Obj.cantidad_recibida
                DgridDetalleRec.Rows(i).Cells("CostoP").Value = Obj.Costo
                ' el total de la linea es calculado
                DgridDetalleRec.Rows(i).Cells("IdProductoP").Value = Obj.IdProductoBodega
                DgridDetalleRec.Rows(i).Cells("KeyP").Value = Obj.Producto.IdProducto

                Dim bo As clsBeProducto = clsLnProducto.Get_Control_Vencimiento_By_IdProducto(Obj.Producto.IdProducto)
                DgridDetalleRec.Rows(i).Cells("ControlVencimiento").Value = bo.Control_vencimiento
                DgridDetalleRec.Rows(i).Cells("ControlPeso").Value = bo.Control_peso

                Dim lBeProductoPresentacion As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(Obj.Producto.IdProducto).ToList
                Dim lBeProductoEstado As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodegaHH(gBeRecepcionEnc.PropietarioBodega.IdPropietario, gBeRecepcionEnc.IdBodega).ToList

                If lBeProductoPresentacion.Count > 0 Then
                    If Obj.IdPresentacion <> 0 Then
                        If Not Obj.Presentacion Is Nothing Then
                            Llena_Presentacion_Grid2(lBeProductoPresentacion, i, Obj.Presentacion.IdPresentacion)
                        End If
                    End If
                Else
                    DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    DgComboPresentacion.DataSource = Nothing
                End If

                If Obj.IdPresentacion <> 0 Then
                    '#EJC20180315: Pendiente de analisis hasta realizar el cambio (por mí) en la hh.
                    If Obj.Presentacion.IdPresentacion <> 0 Then
                        If Obj.Presentacion.EsPallet Then
                            DgridDetalleRec.Rows(i).Cells("CantidadP").Value = Obj.cantidad_recibida '/ (Obj.Presentacion.Factor * Obj.Presentacion.CajasPorCama * Obj.Presentacion.CamasPorTarima)
                        Else
                            DgridDetalleRec.Rows(i).Cells("CantidadP").Value = Obj.cantidad_recibida '/ Obj.Presentacion.Factor
                        End If
                    End If
                End If

                If lBeProductoEstado.Count > 0 Then
                    Llena_Estados(lBeProductoEstado, i, Obj.ProductoEstado.IdEstado)
                Else
                    DgComboEstado = TryCast(DgridDetalleRec.Rows(i).Cells("Estado"), DataGridViewComboBoxCell)
                    DgComboEstado.DataSource = Nothing
                End If

                If Obj.Fecha_vence <> Nothing Then
                    If bo.Control_vencimiento Then
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value = Obj.Fecha_vence
                    Else
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value = "N/A"
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").ReadOnly = True
                    End If
                End If

                Dim valor_peso = Obj.Peso / Obj.cantidad_recibida

                DgridDetalleRec.Rows(i).Cells("Lote").Value = Obj.Lote
                DgridDetalleRec.Rows(i).Cells("Peso").Value = Obj.Peso
                DgridDetalleRec.Rows(i).Cells("PesoUnitario").Value = IIf((valor_peso > 0), valor_peso, 0)
                DgridDetalleRec.Rows(i).Cells("CostoOC").Value = GetCostoByIdProducto(Obj.IdProductoBodega)
                DgridDetalleRec.Rows(i).Cells("Observacion").Value = Obj.Observacion
                DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value = Obj.IdRecepcionEnc
                DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value = Obj.IdRecepcionDet
                DgridDetalleRec.Rows(i).Cells("IsNewR").Value = Obj.IsNew
                DgridDetalleRec.Rows(i).Cells("No_Linea").Value = Obj.No_Linea
                DgridDetalleRec.Rows(i).Cells("atributo_variante_1").Value = Obj.Atributo_Variante_1
                DgridDetalleRec.Rows(i).Cells("lic_plate").Value = Obj.Lic_plate
                DgridDetalleRec.Rows(i).Cells("IdOrdenCompraEnc").Value = Obj.IdOrdenCompraEnc
                DgridDetalleRec.Rows(i).Cells("IdOrdenCompraDet").Value = Obj.IdOrdenCompraDet

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

            For Each Obj As clsBeTrans_re_det In lBeTransRecDet

                i = DgridDetalleRec.Rows.Add(0, Obj.Producto.Codigo, Obj.Producto.Nombre)

                If Obj.UnidadMedida IsNot Nothing Then
                    setUnidadMedidaGrid2(Obj, i)
                Else
                    Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", Obj.Producto.Nombre))
                End If

                DgridDetalleRec.Rows(i).Cells("CantidadP").Value = Obj.cantidad_recibida
                DgridDetalleRec.Rows(i).Cells("CostoP").Value = Obj.Costo
                ' el total de la linea es calculado
                DgridDetalleRec.Rows(i).Cells("IdProductoP").Value = Obj.IdProductoBodega
                DgridDetalleRec.Rows(i).Cells("KeyP").Value = Obj.Producto.IdProducto

                Dim bo As clsBeProducto = clsLnProducto.Get_Control_Vencimiento_By_IdProducto(Obj.Producto.IdProducto)
                DgridDetalleRec.Rows(i).Cells("ControlVencimiento").Value = bo.Control_vencimiento
                DgridDetalleRec.Rows(i).Cells("ControlPeso").Value = bo.Control_peso

                Dim lBeProductoPresentacion As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(Obj.Producto.IdProducto).ToList

                Dim IdPropietario As Integer = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(cmbBodega.EditValue, Obj.IdPropietarioBodega)

                Dim lBeProductoEstado As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodegaHH(IdPropietario, cmbBodega.EditValue).ToList

                If lBeProductoPresentacion.Count > 0 Then
                    If Obj.IdPresentacion <> 0 Then
                        If Not Obj.Presentacion Is Nothing Then
                            Llena_Presentacion_Grid2(lBeProductoPresentacion, i, Obj.Presentacion.IdPresentacion)
                        End If
                    End If
                Else
                    DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    DgComboPresentacion.DataSource = Nothing
                End If

                If Obj.IdPresentacion <> 0 Then
                    '#EJC20180315: Pendiente de analisis hasta realizar el cambio (por mí) en la hh.
                    If Obj.Presentacion.IdPresentacion <> 0 Then
                        If Obj.Presentacion.EsPallet Then
                            DgridDetalleRec.Rows(i).Cells("CantidadP").Value = Obj.cantidad_recibida '/ (Obj.Presentacion.Factor * Obj.Presentacion.CajasPorCama * Obj.Presentacion.CamasPorTarima)
                        Else
                            DgridDetalleRec.Rows(i).Cells("CantidadP").Value = Obj.cantidad_recibida '/ Obj.Presentacion.Factor
                        End If
                    End If
                End If

                If lBeProductoEstado.Count > 0 Then
                    Llena_Estados(lBeProductoEstado, i, Obj.ProductoEstado.IdEstado)
                Else
                    DgComboEstado = TryCast(DgridDetalleRec.Rows(i).Cells("Estado"), DataGridViewComboBoxCell)
                    DgComboEstado.DataSource = Nothing
                End If

                If Obj.Fecha_vence <> Nothing Then
                    If bo.Control_vencimiento Then
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value = Obj.Fecha_vence
                    Else
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value = "N/A"
                        DgridDetalleRec.Rows(i).Cells("FechaVencimiento").ReadOnly = True
                    End If
                End If

                DgridDetalleRec.Rows(i).Cells("Lote").Value = Obj.Lote
                DgridDetalleRec.Rows(i).Cells("Peso").Value = Obj.Peso
                DgridDetalleRec.Rows(i).Cells("PesoUnitario").Value = Obj.Peso / Obj.cantidad_recibida
                DgridDetalleRec.Rows(i).Cells("CostoOC").Value = GetCostoByIdProducto(Obj.IdProductoBodega)
                DgridDetalleRec.Rows(i).Cells("Observacion").Value = Obj.Observacion
                DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value = Obj.IdRecepcionEnc
                DgridDetalleRec.Rows(i).Cells("IdRecepcionDet").Value = Obj.IdRecepcionDet
                DgridDetalleRec.Rows(i).Cells("IsNewR").Value = Obj.IsNew
                DgridDetalleRec.Rows(i).Cells("No_Linea").Value = Obj.No_Linea
                DgridDetalleRec.Rows(i).Cells("atributo_variante_1").Value = Obj.Atributo_Variante_1
                DgridDetalleRec.Rows(i).Cells("lic_plate").Value = Obj.Lic_plate

                Set_TotalGrid2(i)

                '#EJC20210613: Lllenado manual de stock
                Dim pIndex As Integer = -1

                Dim BeStock_rec As New clsBeStock_rec With
                        {.IdRecepcionDet = Obj.IdRecepcionDet,
                        .IdPropietarioBodega = pIdPropietarioBodega,
                        .IdProductoBodega = Obj.IdProductoBodega,
                        .IsNew = True
                        }

                pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = Obj.IdProductoBodega)

                If pIndex >= -1 Then

                    If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then
                        BeStock_rec.IdStockRec = pListBeStockRec.Max(Function(b) b.IdStockRec) + 1
                    Else
                        BeStock_rec.IdStockRec = clsLnStock_rec.MaxID() + 1
                    End If

                    BeStock_rec.Presentacion.IdPresentacion = 0
                    BeStock_rec.Lic_plate = Obj.Lic_plate
                    BeStock_rec.Fecha_Ingreso = Date.Now
                    BeStock_rec.Fecha_Manufactura = Nothing
                    BeStock_rec.Serial = 0
                    BeStock_rec.Añada = 0
                    BeStock_rec.Peso = 0.0
                    BeStock_rec.Temperatura = 0.0
                    BeStock_rec.IdRecepcionDet = Obj.IdRecepcionDet
                    BeStock_rec.IdRecepcionEnc = Obj.IdRecepcionEnc
                    BeStock_rec.Fec_mod = Date.Now
                    BeStock_rec.User_mod = AP.UsuarioAp.IdUsuario
                    BeStock_rec.IdPropietarioBodega = Obj.IdPropietarioBodega
                    BeStock_rec.IdProductoBodega = Obj.IdProductoBodega
                    BeStock_rec.No_linea = Obj.No_Linea
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Presentacion_Grid2(ByVal pList As List(Of clsBeProducto_Presentacion),
                                         ByVal pIndex As Integer,
                                         Optional ByVal pIdPresentacion As Integer = 0)

        Try

            Dim DgCombo As New DataGridViewComboBoxCell()
            DgCombo = TryCast(DgridDetalleRec.Rows(pIndex).Cells("PresentacionP"), DataGridViewComboBoxCell)

            DgCombo.DataSource = pList
            DgCombo.ValueMember = "IdPresentacion"
            DgCombo.DisplayMember = "Nombre"

            If pIdPresentacion <> 0 Then
                DgCombo.Value = pIdPresentacion
            End If

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

                If frmCapturaParametros.Mostrar_Parametros() Then

                    frmCapturaParametros.pListBeStockRec = pListBeStockRec
                    frmCapturaParametros.pListBeStockSeRec = pListBeStockSeRec
                    frmCapturaParametros.plistBeReDetParametros = plistBeReDetParametros
                    frmCapturaParametros.pListBeProductoPallet = pListBeProductoPallet
                    frmCapturaParametros.pNoLinea = Val(DgridDetalleRec.Rows(pRowIndex).Cells("No_Linea").Value)

                    If Not DgridDetalleRec.Rows(pRowIndex).Cells("PresentacionP").Value Is Nothing Then
                        frmCapturaParametros.pIdPresentacion = DgridDetalleRec.Rows(pRowIndex).Cells("PresentacionP").Value
                    End If

                    If frmCapturaParametros.ShowDialog() = DialogResult.OK Then

                        DgridDetalleRec.Rows(pRowIndex).Cells("PesoUnitario").Value = frmCapturaParametros.txtPesoReal.Value
                        DgridDetalleRec.Rows(pRowIndex).Cells("Peso").Value = frmCapturaParametros.txtPesoReal.Value
                        '#CKFK 20210901 Agregué esta línea para que se guardara el LP cuando el ingreso sea manual
                        DgridDetalleRec.Rows(pRowIndex).Cells("Lic_Plate").Value = frmCapturaParametros.txtLicPlate.Text

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Guardar_Stock_Rec(ByVal pObjProducto As clsBeProducto,
                                  ByVal IdEstado As Integer,
                                  Optional ByVal CheckManualInput As Boolean = True)

        Dim pIdRecepcionDet As Integer
        Dim pIndex As Integer

        Try

            pIdPropietarioBodega = cmbPropietario.EditValue

            Dim vNoLinea As Integer = DgridDetalleRec.Rows(DgridDetalleRec.CurrentRow.Index).Cells("No_Linea").Value

            pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = pObjProducto.IdProductoBodega _
                                                                              AndAlso f.IdProductoEstado = IdEstado _
                                                                              AndAlso f.No_linea = vNoLinea)

            If pIndex > -1 Then
                pIdRecepcionDet = pListBeStockRec(pIndex).IdRecepcionDet
            Else

                If CheckManualInput Then

                    '#EJC20200311: Si modifican él código de producto en la misma línea, eliminar el objeto anterior.
                    Dim vIndiceAnte As Integer = pListBeStockRec.FindIndex(Function(f) f.No_linea = vNoLinea)

                    If vIndiceAnte <> -1 Then

                        If pListBeStockRec(vIndiceAnte).IdProductoBodega <> pObjProducto.IdProducto Then
                            pListBeStockRec.RemoveAt(vIndiceAnte)
                            pIdRecepcionDet = -1
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

            pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = pObjProducto.IdProductoBodega)

            If pIndex >= -1 Then

                If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then
                    BeStock_rec.IdStockRec = pListBeStockRec.Max(Function(b) b.IdStockRec) + 1
                Else
                    BeStock_rec.IdStockRec = clsLnStock_rec.MaxID() + 1
                End If

                BeStock_rec.Presentacion.IdPresentacion = 0
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

                                DgridDetalleRec.Rows(e.RowIndex).Cells("IdRecepcionDet").Value = lMaxIdRecepcionDetParametro

                                If Val(DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value) = 0 Then
                                    DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value = lMaxIdRecepcionDetParametro
                                End If

                                Set_Stock_Parametro(BeProducto, lMaxIdRecepcionDetParametro, e.RowIndex, vIdEstado)

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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
                        Else
                            Dim pFechavence As Date = DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").Value

                            If Validar_Tiempos_Aceptacion_Proveedor(pFechavence) Then
                                e.Cancel = False
                                FechaVencimientoCell.ErrorText = ""

                            Else
                                e.Cancel = True
                                FechaVencimientoCell.ErrorText = "Vencimiento no valido."
                                DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").ErrorText = "Vencimiento no valido."
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
                                Dim fechaVence = FechaVencimientoCell.Value

                                If result.Count = 0 AndAlso FechaVencimientoCell.Value > vFechaDefecto Then
                                    DT.Rows.Add(fechaVence, LoteCell.Value)
                                Else
                                    XtraMessageBox.Show("El vencimiento no es valido, el lote ya existe con otra fecha, se asignara la registrada previamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").Value = result(0).Item(0)
                                    DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").ReadOnly = True
                                    e.Cancel = False
                                    LoteCell.ErrorText = ""
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


    Private Function Validar_Tiempos_Aceptacion_Proveedor(pFechaIngresadaVence As Date) As Boolean

        Validar_Tiempos_Aceptacion_Proveedor = False

        Try

            Dim pFechaHoy As Date = Now.Date
            Dim pTipoIngreso = BeTransOcEnc.TipoIngreso
            Dim pTiemposProveedorList = BeTransOcEnc.ProveedorBodega.Proveedor.TiemposProveedor
            Dim pTiempoProveedor As New clsBeProveedor_tiempos

            If pTiemposProveedorList IsNot Nothing Then
                pTiempoProveedor = pTiemposProveedorList.Find(Function(x) x.IdClasificacion = BeProducto.IdClasificacion AndAlso
                                                                                              x.IdFamilia = BeProducto.IdFamilia)
            End If


            If pTipoIngreso.Permitir_Vencido_Ingreso Then
                '#GT31102023: si permite vencido, solo validar vence sea superior a hoy.
                If ValidaFechaVencimiento(pFechaIngresadaVence, BeProducto.Nombre) Then
                    Validar_Tiempos_Aceptacion_Proveedor = True
                End If
            Else
                If pTiempoProveedor IsNot Nothing Then
                    '#GT31102023: si es importacion validar dias exterior
                    If pTipoIngreso.Es_Importacion Then
                        If pTiempoProveedor IsNot Nothing Then
                            pFechaHoy = pFechaHoy.AddDays(pTiempoProveedor.Dias_Exterior)
                            If pFechaIngresadaVence >= pFechaHoy Then
                                Validar_Tiempos_Aceptacion_Proveedor = True
                            Else
                                XtraMessageBox.Show("No se permite la fecha de vencimiento, porque es menor a la fecha de aceptacion exterior.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    Else
                        '#GT31102023: si no es importacion validar dias local
                        If pTiempoProveedor IsNot Nothing Then
                            pFechaHoy = pFechaHoy.AddDays(pTiempoProveedor.Dias_Local)
                            If pFechaIngresadaVence >= pFechaHoy Then
                                Validar_Tiempos_Aceptacion_Proveedor = True
                            Else
                                XtraMessageBox.Show("No se permite la fecha de vencimiento, porque es menor a la fecha de aceptacion local.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    End If
                Else
                    '#GT31102023: si no permite vencido pero no hay tiempos, flujo normal.
                    If ValidaFechaVencimiento(pFechaIngresadaVence, BeProducto.Nombre) Then
                        Validar_Tiempos_Aceptacion_Proveedor = True
                    End If
                End If
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        Try
            cmdActualizar.Enabled = False

            If Actualizar() Then

                clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231712: Se actualizó la recepción: " & gBeRecepcionEnc.IdRecepcionEnc & " Por el IdUsuario: " & AP.UsuarioAp.IdUsuario)

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se actualizó la recepción.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If

                Close()

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

    Private Sub Configura_Opcion_Tipo_Rec(ByVal pObj As clsBeTrans_re_tr)

        Try

            DgridDetalleRec.Enabled = True

            Set_Transaccion(pObj)

            pTransHH = pObj.UsaHH

            xtrRecepcion.TabPages.Remove(tabDetalleOC)
            xtrRecepcion.TabPages.Remove(tabDetRec)
            xtrRecepcion.TabPages.Remove(tabImagenes)
            xtrRecepcion.TabPages.Remove(tabDetOp)

            If Not pObj.ConRef Then

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

            HabilitarTabDetalleRecepcion(pObj)
            xtrRecepcion.TabPages.Add(tabImagenes)
            HabilitarTabOperador(pObj.IdTipoTransaccion)

            DgridDetalleRec.PerformLayout()

            Application.DoEvents()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub lnkTipoT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkTipoT.LinkClicked

        Try

            Dim TR As New frmTipoTransaccion_List() With {.Modo = frmTipoTransaccion_List.pModo.Seleccion}
            TR.ShowDialog()

            If TR.DialogResult = DialogResult.OK Then

                If TR.pObj IsNot Nothing AndAlso String.IsNullOrEmpty(TR.pObj.IdTipoTransaccion) = False Then

                    txtIdTipoTR.Text = TR.pObj.IdTipoTransaccion
                    txtDescripcionTR.Text = TR.pObj.Descripcion
                    Configura_Opcion_Tipo_Rec(TR.pObj)

                    If TR.pObj.UsaHH = 0 Then

                        chkRecepcionManual.Checked = True

                        chkHabilitaStock.Checked = (TR.pObj.IdTipoTransaccion = "MCOC00")

                        '#CKFK 20210624 Se llama a la función creada por EJC para habilitar o no el stock basado en las reglas del propietario
                        Check_Reglas_Propietario_Ingreso()

                        If TR.pObj.IdTipoTransaccion = "MCOC00" Then

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

    Private Sub HabilitarTabDetalleRecepcion(ByVal Obj As clsBeTrans_re_tr)

        Try

            If Obj.IdTipoTransaccion = "HSOC00" Or
               Obj.IdTipoTransaccion = "HSOD00" Or
               Obj.IdTipoTransaccion = "MCOC00" Or
               Obj.IdTipoTransaccion = "MCOD00" Or
               Obj.IdTipoTransaccion = "MSOC00" Or
               Obj.IdTipoTransaccion = "MSOD00" Then

                xtrRecepcion.TabPages.Add(tabDetRec)
                ProductoP.Width = 300
                Estado.Width = 130

                Dim vHabilitarDetalleRecManual As Boolean = ((Obj.IdTipoTransaccion = "MCOC00") AndAlso Not (txtIdOrdenCompra.Text = "")) Or
                   (Obj.IdTipoTransaccion = "MSOC00")

                '#EJC20210930: Si es con D.I. no habilitar el tab hasta que seleccionen la O.C.
                '#CKFK20230512 Modifiqué para el Grid el Enabled por el ReadOnly
                DgridDetalleRec.ReadOnly = Not vHabilitarDetalleRecManual
                cmdAgregarProducto.Enabled = vHabilitarDetalleRecManual
                cmdEliminarFila.Enabled = vHabilitarDetalleRecManual

                If vHabilitarDetalleRecManual Then
                    lblStatus.Text = "Seleccione documento de ingreso para habilitar"
                Else
                    lblStatus.Text = ""
                    GrpDetalleRecepcion.Enabled = True
                    DgridDetalleRec.Enabled = True
                    cmdAgregarProducto.Enabled = True
                    cmdEliminarFila.Enabled = True
                End If

            ElseIf Modo = TipoTrans.Editar Then

                xtrRecepcion.TabPages.Add(tabDetRec)
                ProductoP.Width = 300
                Estado.Width = 130

                Dim vHabilitarDetalleRecManual As Boolean = ((Obj.IdTipoTransaccion = "MCOC00") AndAlso Not (txtIdOrdenCompra.Text = ""))

                '#EJC20210930: Si es con D.I. no habilitar el tab hasta que seleccionen la O.C.
                '#CKFK20230512 Modifiqué para el Grid el Enabled por el ReadOnly
                DgridDetalleRec.ReadOnly = Not vHabilitarDetalleRecManual
                cmdAgregarProducto.Enabled = vHabilitarDetalleRecManual
                cmdEliminarFila.Enabled = vHabilitarDetalleRecManual

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
            Else

                Dim Ti As New clsDataContractDI.tTipoDocumentoIngreso
                Ti = gBeOrdenCompra.TipoIngreso.IdTipoIngresoOC

                lnk.Text = IIf(Ti.ToString = "NoDefinido", "Doc. Ingreso", Ti.ToString)
                tabDetalleOC.Text = "Detalle de pedido"
                tabDetRec.Text = "Detalle de recepción"

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
                IdTipoTransaccion = "HSOC00" Or IdTipoTransaccion = "HSOD00" Or IdTipoTransaccion = "HHSR00" Then
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

                                        Dim Obj As New clsBeTrans_re_op() With
                                          {.IdOperadorBodega = DTOperadores(i)(0),
                                             .User_agr = AP.UsuarioAp.IdUsuario,
                                             .Fec_agr = Now,
                                             .User_mod = AP.UsuarioAp.IdUsuario,
                                             .Fec_mod = Now,
                                             .IsNew = True,
                                             .UsaHH = DTOperadores(i)(2)}
                                        pListOpe.Add(Obj)

                                    End If

                                Else
                                    If vIdOperadorBodega = IdOperadorBodegaDefecto Then
                                        Dim Obj As New clsBeTrans_re_op() With
                                                                              {.IdOperadorBodega = DTOperadores(i)(0),
                                                                              .User_agr = AP.UsuarioAp.IdUsuario,
                                                                              .Fec_agr = Now,
                                                                              .User_mod = AP.UsuarioAp.IdUsuario,
                                                                              .Fec_mod = Now,
                                                                              .IsNew = True,
                                                                              .UsaHH = DTOperadores(i)(2)}
                                        pListOpe.Add(Obj)
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
            'DTOperadores = clsLnOperador_bodega.Get_All_By_IdBodega_DT(cmbBodega.EditValue)
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
                        End If

                        Dim listaStock As New List(Of clsBeStock_rec)
                        listaStock = pListBeStockRec.FindAll(Function(p) p.IdProductoBodega = lIdProducto And p.ProductoValidado = False And p.IdRecepcionDet = lIdRecepcionDet)

                        For Each BeStockRec In listaStock
                            pListBeStockRec.Remove(BeStockRec)
                        Next

                        DgridDetalleRec.Rows.RemoveAt(lIndexF)

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

            If Not BeTransOcEnc Is Nothing Then

                lDetRecAnteriores = clsLnTrans_re_det.Get_All_By_Orden_Compra_Filtro(BeTransOcEnc.IdOrdenCompraEnc, gBeRecepcionEnc.IdRecepcionEnc)

                For Each ProdInOc As clsBeTrans_oc_det In pListObjOrdeCompraDet

                    vPresOC = ProdInOc.Presentacion

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

                        For Each ProdInDetRec As clsBeTrans_re_det In lDetRec

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

            clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231712B: Se finalizó la recepción desde BOF: " & gBeRecepcionEnc.IdRecepcionEnc & " Por el IdUsuario: " & AP.UsuarioAp.IdUsuario)

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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
            If gBeRecepcionEnc.Habilitar_Stock AndAlso (gBeRecepcionEnc.IdTipoTransaccion = "HCOC00" OrElse gBeRecepcionEnc.IdTipoTransaccion = "HSOC00") Then
                gBeRecepcionEnc = clsLnTrans_re_enc.GetSingle(gBeRecepcionEnc.IdRecepcionEnc)
                pListRecepcionDetalle = gBeRecepcionEnc.Detalle
                clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302230123: Se realizó recarga del objeto para la recepción: " & gBeRecepcionEnc.IdRecepcionEnc & " el tipo de transacción es: " & gBeRecepcionEnc.IdTipoTransaccion)
                vRecargoObjetoRecepcion = True
            End If

            SplashScreenManager.CloseForm(False)

            If pListRecepcionDetalle.Count = 0 Then

                If Not vRecargoObjetoRecepcion Then

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
                                clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302230139A: Se realizó llamada recursiva del objeto para la recepción: " & gBeRecepcionEnc.IdRecepcionEnc & " el tipo de transacción es: " & gBeRecepcionEnc.IdTipoTransaccion)
                                Finalizar(gBeRecepcionEnc.Detalle)
                            End If
                        End If

                    End If

                Else
                    XtraMessageBox.Show("La recepción no tiene detalle, No se puede finalizar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            Else

                If XtraMessageBox.Show("¿Finalizar la Recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim BackOrder As Boolean = False
                    Dim lIdOrdenCompra As Integer = 0

                    If gBeRecepcionEnc.OrdenCompraRec IsNot Nothing Then

                        lIdOrdenCompra = gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc

                        BackOrder = Detalle_Tiene_Diferencia_Vrs_OC(pListRecepcionDetalle)

                        If BackOrder Then

                            If XtraMessageBox.Show("La recepción tiene diferencia. ¿dejar el pedido de compra pendiente de finalización?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                BackOrder = False
                            End If

                        End If

                    End If

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Finalizando...")

                    Finalizar = Finaliza_Recepcion(BackOrder, gBeRecepcionEnc.Habilitar_Stock)

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

    Private Sub frmRecepcionBOF_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F2 Then
            lnkAgregarProducto_LinkClicked()
        ElseIf e.KeyCode = Keys.F3 Then
            cmdVerParametros_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Delete Then
            EliminarFila(Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            cmdFinalizar_ItemClick(Nothing, Nothing)
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            chkDI2REC.Checked = Not chkDI2REC.Checked
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

        'SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        'SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

        Try

            'Using r As New frmReporte

            '    Dim rpt As New rptRecepcion
            '    Dim lDS As New DsRecepcion
            '    Dim DT As New DataTable

            If gBeRecepcionEnc.OrdenCompraRec Is Nothing Then
                Genera_Reporte_SinOC()
                'DT = clsLnTrans_re_enc.GetImpresionByRecepcionSinOC(gBeRecepcion.IdRecepcionEnc)
                'lDS.Tables("Imagen").Merge(ObtenerImagen("Imagen", "Empresa", "IdEmpresa=" & AP.IdEmpresa))
                'lDS.Tables("Data").Merge(DT)
            Else

                If gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc <> 0 Then
                    If Cosolida Then
                        Genera_Reporte_ConOCConsolidada()
                    Else
                        Genera_Reporte_ConOC()
                    End If
                    'DT = clsLnTrans_re_enc.GetImpresionByRecepcion(gBeRecepcion.IdRecepcionEnc)
                    'DT.Columns("peso").Caption = "Peso"
                    'lDS.Tables("Imagen").Merge(ObtenerImagen("Imagen", "Empresa", "IdEmpresa=" & AP.IdEmpresa))
                    'lDS.Tables("Data").Merge(DT)
                Else
                    Genera_Reporte_SinOC()
                    'DT = clsLnTrans_re_enc.GetImpresionByRecepcionSinOC(gBeRecepcion.IdRecepcionEnc)
                    'lDS.Tables("Imagen").Merge(ObtenerImagen("Imagen", "Empresa", "IdEmpresa=" & AP.IdEmpresa))
                    'lDS.Tables("Data").Merge(DT)
                End If

            End If

            '    For Each thisFormulaField In rpt.DataDefinition.FormulaFields
            '        Select Case thisFormulaField.FormulaName
            '            Case "{@NombreEmpresa}"
            '                thisFormulaField.Text = String.Format("'{0}'", GetDBValue("Nombre", "Empresa", "IdEmpresa = " & AP.IdEmpresa))
            '            Case "{@Bodega}"
            '                thisFormulaField.Text = String.Format("'{0}'", GetDBValue("Nombre", "Bodega", "IdEmpresa = " & AP.IdBodega))
            '            Case "{@Usuario}"
            '                thisFormulaField.Text = String.Format("'{0}'", AP.UsuarioAp.Nombres)
            '            Case Else
            '                Exit Select
            '        End Select
            '    Next

            '    r.Text = "Ingreso a Bodega"
            '    rpt.SetDataSource(lDS)

            '    r.rptView.ReportSource = rpt
            '    Cursor = Cursors.Default
            '    SplashScreenManager.CloseForm(False)
            '    r.ShowDialog()

            'End Using

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

                If XtraMessageBox.Show(String.Format("La fecha de vencimiento del producto {0} es igual o menor a la fecha de hoy. ¿Desea ingresar un producto ya vencido?", pNombreProducto), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    ValidaFechaVencimiento = True

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                    If gBeRecepcionEnc IsNot Nothing AndAlso gBeRecepcionEnc.IdRecepcionEnc > 0 Then
                        SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")
                    Else
                        SplashScreenManager.Default.SetWaitFormDescription("Guardando...")
                    End If
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
        EliminarFila(Nothing)
    End Sub

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click

        Try

            Dim factura As New frmFactura() With {.pIndex = -1, .pListObjF = pListRecFact, .Cargar = New frmFactura.Operar(AddressOf Cargar_Facturas)}
            factura.ShowDialog()
            factura.Dispose()

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

            GridViewImg.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

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

                Dim Obj As New clsBeTrans_re_tr() With {.IdTipoTransaccion = txtIdTipoTR.Text.Trim}
                Obj = clsLnTrans_re_tr_Partial.GetSingle(Obj.IdTipoTransaccion)

                If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Descripcion) = False Then

                    txtDescripcionTR.Text = Obj.Descripcion
                    Configura_Opcion_Tipo_Rec(Obj)

                    If Obj.UsaHH = 0 Then
                        chkRecepcionManual.Checked = True
                        chkHabilitaStock.Checked = False
                    Else
                        chkRecepcionManual.Checked = False
                        chkHabilitaStock.Checked = True
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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

                                clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231712A: Se anuló la recepción: " & gBeRecepcionEnc.IdRecepcionEnc & " Por el IdUsuario: " & AP.UsuarioAp.IdUsuario)

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

        Try

            If String.IsNullOrEmpty(txtIdUbicacion.Text.Trim()) = False AndAlso txtIdUbicacion.Text > "0" Then

                Dim Obj As New clsBeBodega_ubicacion

                Obj = clsLnBodega_ubicacion.Get_Ubicacion_Recepcion(txtIdUbicacion.Text.Trim)

                If Obj IsNot Nothing AndAlso Obj.IdUbicacion > 0 Then
                    txtNombreUbicacion.Text = Obj.Descripcion
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            If cmbBodega.ItemIndex > -1 Then

                cmbPropietario.Properties.DataSource = Nothing
                IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
                cmbMuelle.Properties.DataSource = Nothing
                IMS.Listar_Muelles(cmbMuelle, cmbBodega.EditValue)

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
                    txtIdTipoTR.Text = "HCOC00" 'Recepción con OC.
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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdPiloto_Validated(sender As Object, e As EventArgs) Handles txtIdPiloto.Validated

        Try

            If String.IsNullOrEmpty(txtIdPiloto.Text.Trim()) = False AndAlso txtIdPiloto.Text > "0" Then

                Dim Obj As New clsBeEmpresa_transporte_pilotos
                Obj = clsLnEmpresa_transporte_pilotos.GetNombre(txtIdPiloto.Text)

                If Obj IsNot Nothing AndAlso Obj.IdPiloto > 0 Then
                    txtNombrePiloto.Text = Trim(String.Format("{0} {1}", Obj.Nombres, Obj.Apellidos))
                Else

                    XtraMessageBox.Show(String.Format("No existe Piloto con código {0}", txtIdPiloto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdPiloto.Focus()
                    txtIdPiloto.SelectAll()

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnkPiloto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkPiloto.LinkClicked

        Try

            Dim Piloto As New frmEmpresa_Transporte_PilotoList() With {.Modo = frmEmpresa_Transporte_PilotoList.pModo.Seleccion}
            Piloto.ShowDialog()

            If Piloto.pObjPiloto IsNot Nothing AndAlso Piloto.pObjPiloto.IdPiloto > 0 Then
                txtIdPiloto.Text = Piloto.pObjPiloto.IdPiloto
                txtNombrePiloto.Text = Trim(String.Format("{0} {1}", Piloto.pObjPiloto.Nombres, Piloto.pObjPiloto.Apellidos))
            End If

            Piloto.Close()
            Piloto.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnkVehiculo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkVehiculo.LinkClicked

        Try

            Dim Vehiculo As New frmEmpresa_Transporte_VehiculoList() With {.Modo = frmEmpresa_Transporte_VehiculoList.pModo.Seleccion}
            Vehiculo.ShowDialog()

            If Vehiculo.pObjVehiculo IsNot Nothing AndAlso Vehiculo.pObjVehiculo.IdVehiculo > 0 Then

                txtIdVehiculo.Text = Vehiculo.pObjVehiculo.IdVehiculo
                'txtNombreVehiculo.Text = Trim(String.Format("Placa: {0}", Vehiculo.pObjVehiculo.Placa))
                '#GT13112023: homologar los mismo campos, al seleccionar y al cargar de la re_enc
                txtNombreVehiculo.Text = Trim(String.Format("Placa: {0} - {1} - {2} - {3}", Vehiculo.pObjVehiculo.Placa, Vehiculo.pObjVehiculo.Marca, Vehiculo.pObjVehiculo.Modelo, Vehiculo.pObjVehiculo.Tipo))

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdActualizarDetalle_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizarDetalle.ItemClick

        Recargar_Objeto_Recepcion()

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub GrdOperadorBobega_CellValueChanging(sender As Object, e As CellValueChangedEventArgs) Handles GrdOperadorBobega.CellValueChanging

        If e.Column.Name = "colSelección" Then

            Debug.Print("something IS changing")

            Try

                'GT30052022: se cambiad de layoutview a GridView
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
                .Width = 200
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
            lOCDet = BeTransOcEnc.DetalleOC.ToList

            Dim i As Integer = -1

            PresentacionGridLookUpEdit.DataSource = clsLnProducto_presentacion.Get_All_By_IdBodega(cmbBodega.EditValue)

            For Each Obj As clsBeTrans_oc_det In lOCDet

                Dim vCantidadPendiente As Double = Math.Round(Obj.Cantidad_recibida - Obj.Cantidad, 6)

                DTGridDetalleDocIngresos.Rows.Add(Obj.IdPropietarioBodega,
                                                   Obj.Nombre_Propietario,
                                                   Obj.No_Linea,
                                                   Obj.IdProductoBodega,
                                                   Obj.Codigo_Producto,
                                                   Obj.Nombre_producto,
                                                   Obj.Nombre_unidad_medida_basica,
                                                   Obj.IdUnidadMedidaBasica,
                                                   Obj.IdPresentacion,
                                                   Obj.Arancel.IdArancel,
                                                   Obj.IdMotivoDevolucion,
                                                   Obj.Cantidad,
                                                   Obj.Cantidad_recibida,
                                                   vCantidadPendiente,
                                                   Obj.Peso_Bruto,
                                                   Obj.Peso_Neto,
                                                   Obj.Costo,
                                                   Obj.valor_aduana,
                                                   Obj.valor_fob,
                                                   Obj.valor_iva,
                                                   Obj.valor_dai,
                                                   Obj.valor_seguro,
                                                   Obj.valor_flete,
                                                   Obj.Total_linea,
                                                   Obj.Producto.IdProducto,
                                                   Obj.IsNew,
                                                   Obj.IdOrdenCompraEnc,
                                                   Obj.IdOrdenCompraDet,
                                                   False,
                                                   Obj.Atributo_variante_1,
                                                   Obj.Producto.Kit,
                                                   Obj.IdPedidoCompraDet,
                                                   Obj.IdOrdenCompraDetPadre,
                                                   Obj.Producto.Control_peso,
                                                   Obj.Producto.Peso_referencia)


                If Obj.lProductosHijosKit.Count > 0 Then

                    For Each Hijo As clsBeTrans_oc_det In Obj.lProductosHijosKit

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
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Dim DT As New DataTable("Lotes")
    Private Sub frmRecepcionBOF_Shown(sender As Object, e As EventArgs) Handles Me.Shown


        Me.AutoScroll = True

        'GrpProducto.VerticalScroll.SmallChange = 100
        'Me.GrpProducto.VerticalScroll.Visible = True

        '#GT02112023: cargar bodega si la recepción proviene de una OC para validar lote homologado
        pBodega = AP.Bodega

        DT.Columns.Add("vence", GetType(Date))
        DT.Columns.Add("lote", GetType(String))

        SuspendLayout()

        Llena_Tipos_Tareas_Que_Necesitan_HH()

        Set_Datata_Table_Grid_Detalle_Documento_Ingreso()

        Try

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

                    Valida_Operadores()

                    If pRecepcionInmediata Then
                        cmbBodega.EditValue = IdBodega
                        CargaOCInmediata()

                    End If

                    If pIdPropietarioBodega <> 0 Then
                        cmbPropietario.EditValue = pIdPropietarioBodega
                    End If

                    Cargar_Ubicacion_Defecto_Recepcion()

                Case TipoTrans.Editar

                    lnkTipoT.Enabled = False
                    txtIdTipoTR.Enabled = False

                    cmdGuardar.Enabled = False
                    cmdActualizar.Enabled = True
                    cmdFinalizar.Enabled = True
                    mnuEliminar.Enabled = True
                    mnuAsignacion.Enabled = True
                    cmdPrint.Enabled = True
                    cmdActualizarDetalle.Enabled = True

                    Cargar_Datos()

            End Select

            'GT 27012021 se valida existencia de ticket para cargar piloto y vehiculo
            'If gBeOrdenCompra.No_Ticket_TMS <> "" Then
            'GT 25032021 el objeto gBeOrdenCompra viene vacio

            If BeTransOcEnc.No_Ticket_TMS = "0" Or BeTransOcEnc.No_Ticket_TMS = "" Then

            Else

                pBeTms_ticket = clsLnTms_ticket.Get_Ticket_By_Id(BeTransOcEnc.No_Ticket_TMS)

                pBeEmpresa_Transporte_Piloto = clsLnEmpresa_transporte_pilotos.Get_By_IdPiloto(pBeTms_ticket.IdPiloto)
                pBeEmpresa_Transporte_Vehiculo = clsLnEmpresa_transporte_vehiculos.Get_Single_By_IdVehiculo(pBeTms_ticket.IdVehiculo)


                If pBeEmpresa_Transporte_Piloto IsNot Nothing AndAlso pBeEmpresa_Transporte_Piloto.IdPiloto > 0 Then

                    txtIdPiloto.Text = pBeEmpresa_Transporte_Piloto.IdPiloto
                    txtNombrePiloto.Text = Trim(String.Format("{0} {1}", pBeEmpresa_Transporte_Piloto.Nombres, pBeEmpresa_Transporte_Piloto.Apellidos))
                    txtIdPiloto.Enabled = False

                Else

                    'XtraMessageBox.Show(String.Format("No existe Piloto con código {0}", txtIdPiloto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdPiloto.Focus()
                    txtIdPiloto.SelectAll()

                End If


                If pBeEmpresa_Transporte_Vehiculo IsNot Nothing AndAlso pBeEmpresa_Transporte_Vehiculo.IdVehiculo > 0 Then
                    txtIdVehiculo.Text = pBeEmpresa_Transporte_Vehiculo.IdVehiculo
                    txtNombreVehiculo.Text = Trim(String.Format("{0} - {1} - {2}", pBeEmpresa_Transporte_Vehiculo.Marca, pBeEmpresa_Transporte_Vehiculo.Modelo, pBeEmpresa_Transporte_Vehiculo.Tipo))
                    txtIdVehiculo.Enabled = False

                Else

                    'XtraMessageBox.Show(String.Format("No existe Vehículo con código {0}", txtIdVehiculo.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    txtIdVehiculo.Focus()
                    txtIdVehiculo.SelectAll()

                End If

            End If

            Focus()


            '#GT26092023: Valida si nuevo viene desde OC con control_poliza, o es edit de uno existente con control_poliza
            If Not Control_Poliza Then
                grpDatosFiscalSAT.Visible = False
            Else
                grpDatosFiscalSAT.Visible = True
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

    Private Sub txtCartaCupo_EditValueChanged(sender As Object, e As EventArgs) Handles txtCartaCupo.EditValueChanged

    End Sub

    Private Sub lnkVehiculo_KeyDown(sender As Object, e As KeyEventArgs) Handles lnkVehiculo.KeyDown


    End Sub

End Class