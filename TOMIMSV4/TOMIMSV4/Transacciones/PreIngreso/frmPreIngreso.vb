Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen

Public Class frmPreIngreso

    Public pRecepcionInmediata As Boolean
    Public pIdOrdenCompraEnc As Integer
    Public pIdPropietarioBodega As Integer

    '------------------------------------------------------------------'
    Private pListObjOrdeCompraDet As New List(Of clsBeTrans_oc_det)
    '------------------------------------------------------------------'    
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

    Private pListPresentacionesProd As New List(Of clsBeProducto_Presentacion)

    Private DT As DataTable

    Public gBeRecepcion As New clsBeTrans_re_enc
    Public gBeOrdenCompra As New clsBeTrans_oc_enc

    Private pObjOC As New clsBeTrans_oc_enc
    Private oDateTimePicker As DateTimePicker
    Private lMaxIdRecepcionDetParametro As Integer = 0
    Private pTransHH As Boolean
    Private pSelecciono As Boolean

    Private DgComboPresentacion As New DataGridViewComboBoxCell()
    Private DgComboEstado As New DataGridViewComboBoxCell()
    Private DgComboUnidadMedida As New DataGridViewComboBoxCell()
    Private DgComboArancel As New DataGridViewComboBoxCell()

    Public Delegate Sub Listar_Recepciones()
    Public Property Listar As Listar_Recepciones

    Private lPalletsToPrint As New List(Of clsBeVW_Impresion_Pallet)

    Private lBeStockRec As New List(Of clsBeStock_rec)
    Private lBeProdPallet As New List(Of clsBeProducto_pallet)

    Private Sub lnk_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnk.LinkClicked

        txtIdOrdenCompra.Text = String.Empty
        txtOC.Text = String.Empty

        Try

            Dim OC As New frmOrdenCompra_List() With
                {.pIdBodega = cmbBodega.EditValue,
                .pIdPropietario = cmbPropietario.Tag,
                .Modo = frmOrdenCompra_List.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                OC.OpcionesMenu = OpcionesMenu
                OC.mnuNuevoIngresoConsolidados.Enabled = OpcionesMenu.Modificar
                OC.mnuNuevo.Enabled = OpcionesMenu.Modificar
            End If

            If OC.ShowDialog() = DialogResult.OK Then

                gBeOrdenCompra = OC.gBeOrdenCompra

                If gBeOrdenCompra IsNot Nothing AndAlso gBeOrdenCompra.IdOrdenCompraEnc > 0 Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                    If gBeOrdenCompra.IdEstadoOC = 4 Then

                        'Si el IdEstadoOc=4 osea si esta finalizada o cerrada entonces no dejar crear recepción con esta OC

                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show(String.Format("La Orden de Compra {0} se encuentra finalizada. No puede generar Recepción con esta Orden de Compra.", gBeOrdenCompra.IdOrdenCompraEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    ElseIf gBeOrdenCompra.ExisteRecepcionNoFinalizada Then

                        SplashScreenManager.CloseForm(False)
                        txtIdOrdenCompra.Text = String.Empty
                        txtOC.Text = String.Empty
                        XtraMessageBox.Show("Existe una Recepción que aún no se ha finalizado. Favor de finalizarla antes de crear otra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Else

                        ValidaOC(gBeOrdenCompra.IdOrdenCompraEnc)

                    End If

                    If gBeOrdenCompra.EsDevolucion Then
                        SplashScreenManager.Default.SetWaitFormCaption("Devolución")
                    Else
                        SplashScreenManager.Default.SetWaitFormCaption("Orden de Compra")
                    End If

                    pObjOC = clsLnTrans_oc_enc.Get_Orden_Compra(gBeOrdenCompra.IdOrdenCompraEnc)
                    txtIdOrdenCompra.Text = pObjOC.IdOrdenCompraEnc
                    txtOC.Text = String.Format("{0} {1}", pObjOC.Referencia, pObjOC.No_Documento)
                    pListObjOrdeCompraDet = pObjOC.DetalleOC.ToList

                    Llena_Detalle_OC(pObjOC.DetalleOC.ToList)

                    cmbBodega.EditValue = pObjOC.IdBodega
                    cmbPropietario.EditValue = pObjOC.PropietarioBodega.IdPropietarioBodega
                    cmbBodega.Enabled = False
                    cmbPropietario.Enabled = False
                Else
                    cmbBodega.Enabled = True
                    cmbPropietario.Enabled = True
                End If

                SplashScreenManager.CloseForm(False)

                If pObjOC IsNot Nothing AndAlso pObjOC.IsNew = False AndAlso pObjOC.IdOrdenCompraEnc > 0 Then
                    ListarTipoTransaccion(True, IIf(pObjOC.EsDevolucion, "DEVOLUCION", "INGRESO"))
                End If

                XtraMessageBox.Show("Orden de compra cargada correctamente",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            End If

            OC.Close()
            OC.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            TransaccionesQueNecesitanHH.Add("PICH000")

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmRecepcion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SuspendLayout()

        Llena_Tipos_Tareas_Que_Necesitan_HH()

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

            DgridOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DgridDetalleRec.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            grdListaFactura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DgridDetalleRec.Columns("FechaVencimiento").DefaultCellStyle.Format = "d"
            DgridDetalleRec.Columns("Peso").DefaultCellStyle.Format = "N4"

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
                    cmdImprimir.Enabled = False
                    cmbBodega.Enabled = True
                    dtmFechaRecepcion.DateTime = Today
                    dtmFechaTarea.DateTime = Today

                    gBeRecepcion = New clsBeTrans_re_enc() With {.IsNew = True}

                    ValidaOperadores()

                    If pRecepcionInmediata Then
                        CargaOCInmediata()
                    End If

                    If pIdPropietarioBodega <> 0 Then
                        cmbPropietario.EditValue = pIdPropietarioBodega
                    End If

                    Cargar_Ubicacion_Defecto_Recepcion()

                Case TipoTrans.Editar

                    lnkTipoT.Enabled = False
                    txtIdTipoTR.Enabled = False

                    ValidaOperadores()

                    cmdGuardar.Enabled = False
                    cmdActualizar.Enabled = True
                    cmdFinalizar.Enabled = True
                    mnuEliminar.Enabled = True
                    mnuAsignacion.Enabled = True
                    cmdImprimir.Enabled = True

                    Cargar_Datos()

            End Select

            Focus()

            DgridDetalleRec.Focus()
            DgridDetalleRec.Rows(0).Selected = True

            ResumeLayout()

            txtIdTipoTR.SelectAll()
            txtIdTipoTR.Focus()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    '#CM20171026_1228PM: Agregué IdUbicación por defecto para recepción y picking
    '#EJC20171205_0635AM: Agregué Try, renombre.
    Private Sub Cargar_Ubicacion_Defecto_Recepcion()

        Try

            Dim pIdBodega As Integer = cmbBodega.EditValue
            Dim Obj = New clsBeBodega() With {.IdBodega = pIdBodega}
            clsLnBodega.Obtener(Obj)
            txtIdUbicacion.Text = Obj.Ubic_recepcion
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

            pObjOC = clsLnTrans_oc_enc.Get_Orden_Compra_By_Propietario(pIdOrdenCompraEnc, pIdPropietarioBodega)

            txtIdOrdenCompra.Text = pObjOC.IdOrdenCompraEnc
            txtOC.Text = pObjOC.No_Documento

            lnk.Enabled = False
            txtIdOrdenCompra.Enabled = False

            Llena_Detalle_OC(pObjOC.DetalleOC.ToList)

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

        Try

            If Datos_Correctos() Then

                SplashScreenManager.CloseForm(False)

                If XtraMessageBox.Show("¿Guardar Recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

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

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf pObjOC Is Nothing AndAlso pBeTR.ConRef Then
                XtraMessageBox.Show("Seleccione Orden de Compra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtIdTipoTR.Text.Trim) Then
                XtraMessageBox.Show("Seleccione Tipo de Transacción.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdTipoTR.Focus()
            ElseIf String.IsNullOrEmpty(txtIdUbicacion.Text.Trim) Then
                XtraMessageBox.Show("Seleccione Ubicación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdUbicacion.Focus()
                xtrRecepcion.SelectedTabPageIndex = 0
            ElseIf pTransHH Then
                If pListOpe.Count = 0 Then
                    XtraMessageBox.Show("Seleccione al menos un operador que use HandHeld.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    If xtrRecepcion.TabPages.Count >= 4 Then
                        xtrRecepcion.SelectedTabPageIndex = 3
                    Else
                        xtrRecepcion.SelectedTabPageIndex = 2
                    End If

                Else
                    Datos_Correctos = True
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

    Private Sub Cargar_Datos()

        Try

            If gBeRecepcion IsNot Nothing Then

                lblC.Text = gBeRecepcion.IdRecepcionEnc

                cmbBodega.EditValue = gBeRecepcion.PropietarioBodega.IdBodega
                cmbPropietario.EditValue = gBeRecepcion.PropietarioBodega.IdPropietario
                cmbBodega.Enabled = False
                cmbPropietario.Enabled = False

                If gBeRecepcion.OrdenCompraRec IsNot Nothing Then

                    pObjOC = clsLnTrans_oc_enc.GetSingle(gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc)
                    txtIdOrdenCompra.Text = pObjOC.IdOrdenCompraEnc
                    txtOC.Text = String.Format("{0} {1}", pObjOC.Referencia, pObjOC.No_Documento)
                    pListObjOrdeCompraDet = pObjOC.DetalleOC.ToList
                    Llena_Detalle_OC(pObjOC.DetalleOC.ToList)
                    txtNoDocumento.Text = gBeRecepcion.OrdenCompraRec.No_docto
                    chkRecepcionManual.Checked = gBeRecepcion.OrdenCompraRec.Recepcion_manual
                    dtmHoraIhh.Value = gBeRecepcion.OrdenCompraRec.Hora_ini_hh
                    dtmHoraFhh.Value = gBeRecepcion.OrdenCompraRec.Hora_fin_hh

                End If

                txtIdOrdenCompra.Enabled = False

                cmbMuelle.EditValue = gBeRecepcion.IdMuelle

                If gBeRecepcion.IdUbicacionRecepcion > 0 Then
                    txtIdUbicacion.Text = gBeRecepcion.IdUbicacionRecepcion
                    txtNombreUbicacion.Text = gBeRecepcion.UbicacionRecepcion
                End If

                If String.IsNullOrEmpty(gBeRecepcion.IdTipoTransaccion) = False Then
                    txtIdTipoTR.Text = gBeRecepcion.IdTipoTransaccion
                    txtDescripcionTR.Text = gBeRecepcion.Descripcion
                    HabilitarTabOperador(gBeRecepcion.IdTipoTransaccion)
                Else
                    txtIdTipoTR.Text = String.Empty
                    txtDescripcionTR.Text = String.Empty
                End If

                dtmFechaRecepcion.EditValue = gBeRecepcion.Fecha_recepcion
                dtmHoraI.Value = gBeRecepcion.Hora_ini_pc
                dtmHoraF.Value = gBeRecepcion.Hora_fin_pc
                chkMuestraCosto.Checked = gBeRecepcion.Muestra_precio
                dtmFechaTarea.EditValue = gBeRecepcion.Fecha_tarea
                chkTomarFoto.Checked = gBeRecepcion.Tomar_fotos
                chkEscanear.Checked = gBeRecepcion.Escanear_rec_ubic
                chkParaPorCodigo.Checked = gBeRecepcion.Para_por_codigo
                txtObservacion.Text = gBeRecepcion.Observacion

                User_agrTextEdit.Text = gBeRecepcion.User_agr
                Fec_agrDateEdit.Text = gBeRecepcion.Fec_agr
                User_modTextEdit.Text = gBeRecepcion.User_mod
                Fec_modDateEdit.Text = gBeRecepcion.Fec_mod

                'Encabezados cargados ahora cargaremos detalle
                lBeTransRecDet = gBeRecepcion.Detalle.ToList
                pListRecImgs = gBeRecepcion.DetalleImagenes.ToList
                plistBeReDetParametros = gBeRecepcion.DetalleParametros.ToList
                pListRecFact = gBeRecepcion.DetalleFacturas.ToList
                pListBeStockRec = clsLnStock_rec.Get_All_By_IdRecepcionEnc(gBeRecepcion.IdRecepcionEnc).ToList
                pListBeProductoPallet = clsLnProducto_pallet.Get_All_By_IdRecepcionEnc(gBeRecepcion.IdRecepcionEnc).ToList
                lPalletsToPrint = clsLnProducto_pallet.Get_All_Barras_Recepcion(gBeRecepcion.IdRecepcionEnc)

                Cargar_Detalle_Recepcion()

                Cargar_Detalle_Barras_Recepcion()

                Cargar_Imagenes()

                Cargar_Facturas()

                If gBeRecepcion.Estado.ToUpper = "CERRADO" Or gBeRecepcion.Estado.ToUpper = "ANULADO" Then
                    If gBeRecepcion.Estado.ToUpper = "CERRADO" Then
                        lblEstado.Text = "Cerrado"
                    ElseIf gBeRecepcion.Estado.ToUpper = "ANULADO" Then
                        lblEstado.Text = "Anulado"
                        lblDiagonal.Visible = True
                        lblMotivoAnulacion.Visible = True
                        lblId.Visible = True

                        Dim ObjMA As New clsBeMotivo_anulacion_bodega() With {.IdMotivoAnulacionBodega = gBeRecepcion.IdMotivoAnulacionBodega}

                        If clsLnMotivo_anulacion_bodega.Get_Single_With_Detail(ObjMA) Then
                            lblId.Text = ObjMA.IdMotivoAnulacion
                            lblMotivoAnulacion.Text = ObjMA.MotivoAnulacion.Nombre
                        End If

                    End If

                    GrpImagen.Enabled = False
                    GrpOperadorBodega.Enabled = False
                    cmdActualizar.Enabled = False
                    cmdEliminar.Enabled = False
                    cmdFinalizar.Enabled = False
                    GrpFactura.Enabled = False
                    Bloquea_Objetos()
                Else
                    lblEstado.Text = "Nuevo"
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

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        chkEscanear.Enabled = False
        chkTomarFoto.Enabled = False
        chkRecepcionManual.Enabled = False
        chkMuestraCosto.Enabled = False
        chkParaPorCodigo.Enabled = False
        dtmFechaTarea.Enabled = False
        dtmHoraI.Enabled = False
        dtmHoraIhh.Enabled = False
        dtmHoraF.Enabled = False
        dtmHoraFhh.Enabled = False
        lnk.Enabled = False
    End Sub

    Private Sub Llena_Valores_Encabezado_Rec()

        Try
            ' Encabezado de Recepción
            If gBeRecepcion.IsNew Then

                gBeRecepcion.IdRecepcionEnc = clsLnTrans_re_enc.MaxID()
                gBeRecepcion.PropietarioBodega = New clsBePropietario_bodega
                gBeRecepcion.PropietarioBodega.IdBodega = cmbBodega.EditValue
                gBeRecepcion.PropietarioBodega.IdPropietarioBodega = cmbPropietario.EditValue
                gBeRecepcion.User_agr = AP.UsuarioAp.IdUsuario
                gBeRecepcion.Fec_agr = Now
                gBeRecepcion.Activo = True
                gBeRecepcion.Estado = "Nuevo"

                If String.IsNullOrEmpty(txtIdOrdenCompra.Text) = False Then
                    gBeRecepcion.OrdenCompraRec = New clsBeTrans_re_oc
                    gBeRecepcion.OrdenCompraRec.IsNew = True
                    gBeRecepcion.OrdenCompraRec.IdRecepcionOc = clsLnTrans_re_oc.MaxID(gBeRecepcion.IdRecepcionEnc)
                    gBeRecepcion.OrdenCompraRec.IdRecepcionEnc = gBeRecepcion.IdRecepcionEnc
                End If

            End If

            If gBeRecepcion.PropietarioBodega Is Nothing OrElse gBeRecepcion.PropietarioBodega.IdPropietarioBodega <= 0 Then
                Throw New Exception("Seleccione Propietario")
            End If

            If String.IsNullOrEmpty(txtIdTipoTR.Text.Trim) = False Then
                gBeRecepcion.IdTipoTransaccion = txtIdTipoTR.Text.Trim
            Else
                gBeRecepcion.IdTipoTransaccion = ""
            End If

            gBeRecepcion.IdMuelle = cmbMuelle.EditValue
            gBeRecepcion.IdUbicacionRecepcion = CInt(txtIdUbicacion.Text.Trim())
            gBeRecepcion.Fecha_recepcion = dtmFechaRecepcion.EditValue
            gBeRecepcion.Hora_ini_pc = dtmHoraI.Value
            gBeRecepcion.Hora_fin_pc = dtmHoraF.Value
            gBeRecepcion.Muestra_precio = chkMuestraCosto.Checked
            gBeRecepcion.Fec_mod = Now
            gBeRecepcion.User_mod = AP.UsuarioAp.IdUsuario
            gBeRecepcion.Fecha_tarea = dtmFechaTarea.EditValue
            gBeRecepcion.Tomar_fotos = chkTomarFoto.Checked
            gBeRecepcion.Escanear_rec_ubic = chkEscanear.Checked
            gBeRecepcion.Para_por_codigo = chkParaPorCodigo.Checked
            gBeRecepcion.Observacion = txtObservacion.Text.Trim

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Valores_Rec_OC()

        Try

            ' Encabezado de Recepción OC
            If gBeRecepcion.OrdenCompraRec IsNot Nothing AndAlso gBeRecepcion.OrdenCompraRec.IsNew Then
                gBeRecepcion.OrdenCompraRec.User_agr = AP.UsuarioAp.IdUsuario
                gBeRecepcion.OrdenCompraRec.Fec_agr = Now
            End If

            If String.IsNullOrEmpty(txtIdOrdenCompra.Text.Trim) = False Then
                gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc = CInt(txtIdOrdenCompra.Text)
            End If

            If gBeRecepcion.OrdenCompraRec IsNot Nothing Then
                gBeRecepcion.OrdenCompraRec.Recepcion_ciega = False
                gBeRecepcion.OrdenCompraRec.Recepcion_manual = chkRecepcionManual.Checked
                gBeRecepcion.OrdenCompraRec.No_docto = txtNoDocumento.Text.Trim
                gBeRecepcion.OrdenCompraRec.Hora_ini_hh = dtmHoraIhh.Value
                gBeRecepcion.OrdenCompraRec.Hora_fin_hh = dtmHoraFhh.Value
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

            If gBeRecepcion IsNot Nothing AndAlso gBeRecepcion.IsNew AndAlso pTransHH Then

                BeTareaHH.IdPropietario = cmbPropietario.Tag
                BeTareaHH.IdBodega = cmbBodega.EditValue

                If cmbMuelle.ItemIndex > -1 Then
                    BeTareaHH.IdMuelle = CInt(cmbMuelle.EditValue)
                End If

                BeTareaHH.IdEstado = 1
                BeTareaHH.IdPrioridad = 1
                BeTareaHH.IdTipoTarea = 1
                BeTareaHH.IdTransaccion = gBeRecepcion.IdRecepcionEnc
                BeTareaHH.Tipo = 0
                BeTareaHH.FechaInicio = dtmHoraI.Value
                BeTareaHH.FechaFin = dtmHoraF.Value
                BeTareaHH.DiaCompleto = False
                BeTareaHH.Descripcion = txtObservacion.Text.Trim
                BeTareaHH.CreaTarea = True
                BeTareaHH.IsNew = True

                Select Case gBeRecepcion.IdTipoTransaccion.ToString()
                    Case "HSOC00"
                        BeTareaHH.Asunto = "Ingreso sin Orden de Compra "
                    Case "HSOD00"
                        BeTareaHH.Asunto = "Ingreso de devolución sin referencia"
                    Case "HCOC00"
                        BeTareaHH.Asunto = "Ingreso con pedido de compra"
                    Case "HCOD00"
                        BeTareaHH.Asunto = "Devolución de pedido de cliente"
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

            lBeStockRec = New List(Of clsBeStock_rec)
            lBeTransRecDet = New List(Of clsBeTrans_re_det)

            lBeProdPallet = New List(Of clsBeProducto_pallet)

            Dim vIndice As Integer = 0

            For i As Integer = 0 To DgridDetalleRec.Rows.Count - 1

                vIndice = i

                If DgridDetalleRec.Rows(i).Cells("ProductoP").Value IsNot Nothing Then

                    pIndiceListaStock = pListBeStockRec.FindIndex(Function(f) f.IdRecepcionDet = DgridDetalleRec.Rows(vIndice).Cells("IdRecepcionDet").Value)

                    Dim BeTransReDet As New clsBeTrans_re_det() With
                        {.IdPropietarioBodega = cmbPropietario.EditValue,
                        .Producto = New clsBeProducto()}

                    BeTransReDet.Producto.IdProducto = CInt(DgridDetalleRec.Rows(i).Cells("KeyP").Value)
                    BeTransReDet.Producto.Codigo = CStr(DgridDetalleRec.Rows(i).Cells("CodigoP").Value)
                    BeTransReDet.Codigo_Producto = CStr(DgridDetalleRec.Rows(i).Cells("CodigoP").Value)
                    BeTransReDet.IdProductoBodega = CInt(DgridDetalleRec.Rows(i).Cells("IdProductoP").Value)
                    BeTransReDet.Nombre_producto = CStr(DgridDetalleRec.Rows(i).Cells("ProductoP").Value)
                    '#EJC20180113: Atributo_Variante_1 en Llena_Detalle_Recepción
                    BeTransReDet.Atributo_Variante_1 = CStr(DgridDetalleRec.Rows(i).Cells("Atributo_Variante_1").Value)

                    DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    BeTransReDet.Nombre_presentacion = DgComboPresentacion.FormattedValue

                    DgComboEstado = TryCast(DgridDetalleRec.Rows(i).Cells("Estado"), DataGridViewComboBoxCell)
                    BeTransReDet.Nombre_producto_estado = DgComboEstado.FormattedValue

                    DgComboUnidadMedida = TryCast(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP"), DataGridViewComboBoxCell)
                    BeTransReDet.Nombre_unidad_medida = DgComboUnidadMedida.FormattedValue

                    If DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value IsNot Nothing Then
                        BeTransReDet.IdRecepcionEnc = CInt(DgridDetalleRec.Rows(i).Cells("IdRecepcionEnc").Value)
                    Else
                        BeTransReDet.IdRecepcionEnc = gBeRecepcion.IdRecepcionEnc
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
                    ElseIf DgridDetalleRec.Rows(i).Cells("CostoP").Value <= 0 Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("El costo debe ser mayor a 0")
                    Else
                        BeTransReDet.Costo = CDbl(DgridDetalleRec.Rows(i).Cells("CostoP").Value)
                    End If

                    If DgridDetalleRec.Rows(i).Cells("TotalP").Value Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception("No existe total")
                        '#EJC20171018_0704: Validar en algun momento el total de la línea en la recepción de forma parametrizable.
                        'ElseIf DgridDetalleRec.Rows(i).Cells("TotalP").Value <= 0 Then
                        '    SplashScreenManager.CloseForm(False)
                        '    Throw New Exception("El total debe ser mayor a 0")
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
                        BeTransReDet.cantidad_recibida = CDec(DgridDetalleRec.Rows(i).Cells("CantidadP").Value)
                    End If

                    If DgridDetalleRec.Rows(i).Cells("No_Linea").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(i).Cells("No_Linea").Value IsNot Nothing Then
                        BeTransReDet.No_Linea = CInt(DgridDetalleRec.Rows(i).Cells("No_Linea").Value)
                        pListBeStockRec(pIndiceListaStock).No_linea = BeTransReDet.No_Linea
                    End If

                    If DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception(String.Format("No existe Unidad de Medida en Producto {0}", DgridDetalleRec.Rows(i).Cells("ProductoP").Value))
                    Else
                        BeTransReDet.UnidadMedida = New clsBeUnidad_medida
                        BeTransReDet.UnidadMedida.IdUnidadMedida = CInt(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value)
                    End If

                    BeTransReDet.ProductoEstado = New clsBeProducto_estado
                    If DgridDetalleRec.Rows(i).Cells("Estado").Value Is Nothing Then
                        SplashScreenManager.CloseForm(False)
                        Throw New Exception(String.Format("No existe Estado en Producto {0}", DgridDetalleRec.Rows(i).Cells("ProductoP").Value))
                    Else
                        BeTransReDet.ProductoEstado.IdEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                    End If

                    If DgridDetalleRec.Rows(i).Cells("Lote").Value IsNot Nothing Then
                        BeTransReDet.Lote = CStr(DgridDetalleRec.Rows(i).Cells("Lote").Value)
                    End If

                    Dim ControlVencimiento As Boolean = DgridDetalleRec.Rows(i).Cells("ControlVencimiento").Value

                    If ControlVencimiento Then

                        If DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value IsNot Nothing Then
                            If IsDate(DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value) Then
                                BeTransReDet.Fecha_vence = CDate(DgridDetalleRec.Rows(i).Cells("FechaVencimiento").Value)
                                pListBeStockRec(pIndiceListaStock).Fecha_vence = BeTransReDet.Fecha_vence
                                If Not ValidaFechaVencimiento(BeTransReDet.Fecha_vence, DgridDetalleRec.Rows(i).Cells("ProductoP").Value.ToString) Then
                                    Throw New Exception(String.Format("Se debe corregir la fecha de vencimiento del producto: {0}", DgridDetalleRec.Rows(i).Cells("ProductoP").Value))
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

                        BeTransReDet.Fecha_vence = Nothing

                    End If

                    BeTransReDet.Fecha_ingreso = Now

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

                    Dim listaStockPalletsNuevos As New List(Of clsBeStock_rec)
                    Dim listaProdPalletsNuevos As New List(Of clsBeProducto_pallet)

                    ' CAMPOS FALTANTES STOCK DE ASIGNACIÓN 
                    If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then

                        Dim listaProdPallets As New List(Of clsBeProducto_pallet)

                        If BeTransReDet.Presentacion.IdPresentacion <> 0 Then
                            lBeStockRec = pListBeStockRec.FindAll(Function(p) _
                                     p.IdProductoBodega = BeTransReDet.IdProductoBodega AndAlso
                                     p.ProductoValidado = False AndAlso
                                     p.Presentacion.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion)

                            If pListBeProductoPallet IsNot Nothing AndAlso pListBeProductoPallet.Count > 0 Then

                                listaProdPallets = pListBeProductoPallet.FindAll(Function(p) _
                                     p.IdRecepcionDet = BeTransReDet.IdRecepcionDet AndAlso
                                     p.IdProductoBodega = BeTransReDet.IdProductoBodega AndAlso
                                     p.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion)

                                Dim BePresPP As New clsBeProducto_Presentacion With {.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion}
                                clsLnProducto_presentacion.GetSingle(BePresPP)

                                For Each BePP As clsBeProducto_pallet In listaProdPallets

                                    With BePP
                                        .Lote = CStr(DgridDetalleRec.Rows(i).Cells("Lote").Value)
                                        .User_agr = AP.UsuarioAp.IdUsuario
                                        .User_mod = AP.UsuarioAp.IdUsuario
                                        .Cantidad = (1 * BePresPP.Factor * BePresPP.CajasPorCama * BePresPP.CamasPorTarima)
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
                            '#EJC20171025_1241PM: Se agregó p.Presentacion.IdPresentacion =0 para que no mezcle el mismo código con y sin presentación.
                            lBeStockRec = pListBeStockRec.FindAll(Function(p) _
                                     p.IdProductoBodega = BeTransReDet.IdProductoBodega AndAlso
                                     p.ProductoValidado = False AndAlso
                                     p.IdUnidadMedida = BeTransReDet.IdUnidadMedida AndAlso
                                     p.Presentacion.IdPresentacion = 0)
                        End If

                        For Each BeStockRec As clsBeStock_rec In lBeStockRec

                            BeStockRec.ProductoEstado = New clsBeProducto_estado
                            If DgridDetalleRec.Rows(i).Cells("Estado").Value IsNot Nothing Then
                                BeStockRec.ProductoEstado.IdEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                                BeStockRec.IdProductoEstado = CInt(DgridDetalleRec.Rows(i).Cells("Estado").Value)
                            End If

                            BeStockRec.Presentacion = New clsBeProducto_Presentacion
                            If DgridDetalleRec.Rows(i).Cells("PresentacionP").Value IsNot Nothing Then
                                BeStockRec.Presentacion.IdPresentacion = CInt(DgridDetalleRec.Rows(i).Cells("PresentacionP").Value)
                            End If

                            '#EJC20180113:Atributo_Variante_1 en detalle de stock -> Llena_Detalle_Recepcion
                            If DgridDetalleRec.Rows(i).Cells("Atributo_Variante_1").Value IsNot Nothing Then
                                BeStockRec.Atributo_Variante_1 = CStr(DgridDetalleRec.Rows(i).Cells("Atributo_Variante_1").Value)
                            Else
                                BeStockRec.Atributo_Variante_1 = ""
                            End If

                            If DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value IsNot Nothing Then
                                BeStockRec.IdUnidadMedida = CInt(DgridDetalleRec.Rows(i).Cells("UnidadMedidaP").Value)
                            End If

                            BeStockRec.IdRecepcionEnc = gBeRecepcion.IdRecepcionEnc

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

                            If BeStockRec.Presentacion.IdPresentacion <> 0 Then

                                Dim BePres As New clsBeProducto_Presentacion With {.IdPresentacion = BeStockRec.Presentacion.IdPresentacion}
                                clsLnProducto_presentacion.GetSingle(BePres)

                                If BePres.EsPallet Then

                                    If CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value) = 1 Then
                                        BeStockRec.Cantidad = (CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value) * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima)
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

                                                End If

                                            Next

                                        Else

                                            Throw New Exception("La cantidad de pallets es > 1 y genera_lp_auto es Falso, debe recibir los pallets de forma unitaria (Cantidad = 1)")
                                            'La cantidad es > 1 y genera_lp_auto es false

                                        End If

                                    End If

                                Else

                                    BeStockRec.Cantidad = (CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value) * BePres.Factor)

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

                                        listaProdPallets.Add(BeProdPallet)
                                        lBeProdPallet.Add(BeProdPallet)

                                    End If

                                End If

                            Else
                                'Agregado por Erik Calderón, si no hay presentación 
                                'no se multiplica por factor, se recibe en unidad 
                                'de medida básica.
                                '#EJC20170710
                                BeStockRec.Cantidad = (CDbl(DgridDetalleRec.Rows(i).Cells("CantidadP").Value))
                            End If

                        Next

                        For Each PN As clsBeStock_rec In listaStockPalletsNuevos
                            lBeStockRec.Add(PN)
                        Next

                        For Each PP As clsBeProducto_pallet In listaProdPalletsNuevos
                            listaProdPallets.Add(PP)
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
            DgridOC.CommitEdit(DataGridViewDataErrorContexts.Commit)

            Llena_Valores_Encabezado_Rec()

            Llena_Valores_Rec_OC()

            Crea_Tarea_HH()

            If gBeRecepcion.IdTipoTransaccion.ToString() = "HSOC00" OrElse
                gBeRecepcion.IdTipoTransaccion.ToString() = "HSOD00" OrElse
                gBeRecepcion.IdTipoTransaccion.ToString() = "MCOC00" OrElse
                gBeRecepcion.IdTipoTransaccion.ToString() = "MCOD00" OrElse
                gBeRecepcion.IdTipoTransaccion.ToString() = "MSOC00" OrElse
                gBeRecepcion.IdTipoTransaccion.ToString() = "MSOD00" OrElse
                gBeRecepcion.IdTipoTransaccion.ToString() = "PICH000" Then

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

            ElseIf gBeRecepcion.IdTipoTransaccion = "HCOC00" Or gBeRecepcion.IdTipoTransaccion = "HCOD00" Then

                ' Ingreso con Orden de Compra 
                ' Devolución de Pedido (PC)
                If DgridOC.Rows.Count > 0 Then
                    'Llena_Detalle_Rec_Con_OC()
                    '#EJC20170721
                    'Analizar si aqui se debe llenar detalle.
                End If

            End If

            'GT 01022021 aqui se envia nothing por parametros de # ticket y estado.
            clsLnTrans_re_enc.Guardar(BeTareaHH,
                gBeRecepcion,
                gBeRecepcion.OrdenCompraRec,
                lBeTransRecDet,
                plistBeReDetParametros,
                pListOpe,
                pListRecFact,
                pListRecImgs,
                pListBeStockSeRec,
                lBeStockRec,
                lBeProdPallet,
                cmbBodega.EditValue,
                0
               )

            Guardar = True

            If Not TransaccionesQueNecesitanHH.Exists(Function(x) x = gBeRecepcion.IdTipoTransaccion) Then
                Finalizar(lBeTransRecDet)
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

                            pObjOC = clsLnTrans_oc_enc.Get_Orden_Compra_By_Propietario(txtIdOrdenCompra.Text.Trim(), cmbPropietario.EditValue)

                            If pObjOC IsNot Nothing AndAlso pObjOC.IdOrdenCompraEnc > 0 Then

                                txtOC.Text = String.Format("{0} {1}", pObjOC.Referencia, pObjOC.No_Documento)

                                If pObjOC.IdEstadoOC = 4 Then

                                    'Si el IdEstadoOc=4 osea si esta finalizada o cerrada entonces no dejar crear recepción con esta OC

                                    SplashScreenManager.CloseForm(False)
                                    XtraMessageBox.Show(String.Format(
                                    "El pedido de compra {0} se encuentra finalizado. 
No puede generar recepción con éste  documento.", pObjOC.IdOrdenCompraEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    txtIdOrdenCompra.Text = String.Empty
                                    txtOC.Text = String.Empty
                                    txtIdOrdenCompra.Focus()
                                    txtIdOrdenCompra.SelectAll()
                                    Return

                                ElseIf pObjOC.ExisteRecepcionNoFinalizada Then

                                    SplashScreenManager.CloseForm(False)
                                    XtraMessageBox.Show("Existe una Recepción que aún no se ha finalizado. Favor de finalizarla antes de crear otra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Return
                                    'ElseIf gBeOrdenCompra.ExisteRecepcionNoFinalizada = False Then

                                Else

                                    ValidaOC(pObjOC.IdOrdenCompraEnc)

                                End If

                                If pObjOC.EsDevolucion Then
                                    SplashScreenManager.Default.SetWaitFormCaption("Devolución")
                                Else
                                    SplashScreenManager.Default.SetWaitFormCaption("Orden de Compra")
                                End If

                                pListObjOrdeCompraDet = pObjOC.DetalleOC.ToList
                                Llena_Detalle_OC(pObjOC.DetalleOC.ToList)

                                SplashScreenManager.CloseForm(False)

                                If pObjOC IsNot Nothing AndAlso pObjOC.IsNew = False AndAlso pObjOC.IdOrdenCompraEnc > 0 Then
                                    ListarTipoTransaccion(True, IIf(pObjOC.EsDevolucion, "DEVOLUCION", "INGRESO"))
                                End If

                                XtraMessageBox.Show("Orden de compra cargada correctamente",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)

                                xtrRecepcion.SelectedTabPageIndex = 2

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

                Dim l As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(Obj.Producto.IdProducto).ToList

                Dim le As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_All_By_IdPropietarioBodega(gBeRecepcion.PropietarioBodega.IdPropietario, gBeRecepcion.IdBodega).ToList

                If l.Count > 0 Then
                    Llena_Presentacion_Grid(l, i, Obj.Presentacion.IdPresentacion)
                Else
                    'Dim DgCombo As New DataGridViewComboBoxCell()
                    DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    DgComboPresentacion.DataSource = Nothing
                End If

                If le.Count > 0 Then
                    LlenaEstados(le, i, Obj.ProductoEstado.IdEstado)
                Else
                    'Dim DgCombo As New DataGridViewComboBoxCell()
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

                setTotalGrid2(i)

            Next

            DgridDetalleRec.ResumeLayout()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Cargar_Detalle_Barras_Recepcion()

        Dim lPalletsToPrint As New List(Of clsBeVW_Impresion_Pallet)

        Try

            lPalletsToPrint = clsLnProducto_pallet.Get_All_Barras_Recepcion(gBeRecepcion.IdRecepcionEnc)

            If Not lPalletsToPrint Is Nothing Then

                Dim i As Integer = -1

                For Each BP In lPalletsToPrint

                    i = dgridBarrasRec.Rows.Add(BP.IdStockRec,
                                                BP.Imprimir,
                                                BP.LP,
                                                BP.Producto_Codigo,
                                                BP.Producto_Nombre_Largo,
                                                BP.Producto_Presentacion,
                                                BP.Producto_UM,
                                                BP.Producto_Cantidad,
                                                BP.Producto_Peso,
                                                BP.Producto_Vence,
                                                BP.Producto_Estado,
                                                BP.Producto_Lote,
                                                BP.Fecha_Produccion)

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

    Private Function GetCostoByIdProducto(ByVal pIdProducto As Integer) As Double

        GetCostoByIdProducto = 0

        Try

            For i As Integer = 0 To DgridOC.Rows.Count - 1
                If DgridOC.Rows(i).Cells("IdProducto").Value = pIdProducto Then
                    GetCostoByIdProducto = DgridOC.Rows(i).Cells("Costo").Value
                    Exit For
                End If
            Next

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    '#EJC20180114: Comentarié toda la función anterior y desplegué solo lo que tien la O.C.
    'Aun no se en que afecte, anteriormente se comparaba lo que ya se había recibido.
    'Ver '#EJC20180114:Ref
    Private Sub Llena_Detalle_OC(ByVal pLista As List(Of clsBeTrans_oc_det))

        Try

            DgridOC.SuspendLayout() : DgridOC.Rows.Clear()

            Dim i As Integer = -1

            For Each Obj As clsBeTrans_oc_det In pLista

                i = DgridOC.Rows.Add(0, Obj.Producto.Codigo, Obj.Producto.Nombre)

                If Obj.UnidadMedida IsNot Nothing Then
                    setUnidadMedidaGrid1(Obj, i)
                Else
                    Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", Obj.Producto.Nombre))
                End If

                If Obj.Arancel IsNot Nothing Then
                    setArancelGrid1(Obj, i)
                Else
                    Dim DgCombo As New DataGridViewComboBoxCell()
                    DgCombo = TryCast(DgridOC.Rows(i).Cells("Arancel"), DataGridViewComboBoxCell)
                    DgCombo.DataSource = Nothing
                End If

                DgridOC.Rows(i).Cells("Cantidad").Value = Obj.Cantidad
                DgridOC.Rows(i).Cells("PesoOC").Value = Obj.Peso
                DgridOC.Rows(i).Cells("Costo").Value = Obj.Costo
                DgridOC.Rows(i).Cells("Total").Value = Obj.Total_linea
                DgridOC.Rows(i).Cells("IdProducto").Value = Obj.IdProductoBodega
                DgridOC.Rows(i).Cells("Key").Value = Obj.Producto.IdProducto

                Dim l As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(Obj.Producto.IdProducto).ToList

                If l.Count > 0 Then
                    LlenaPresentacionGrid1(l, i, Obj.Presentacion.IdPresentacion)
                Else
                    Dim DgCombo As New DataGridViewComboBoxCell()
                    DgCombo = TryCast(DgridOC.Rows(i).Cells("Presentacion"), DataGridViewComboBoxCell)
                    DgCombo.DataSource = Nothing
                End If

                DgridOC.Rows(i).Cells("IsNew").Value = Obj.IsNew
                'DgridOC.Rows(i).Cells("IsNewRecepcion").Value = Obj.ExisteEnRecepcion
                DgridOC.Rows(i).Cells("IdOrdenCompraEnc").Value = Obj.IdOrdenCompraEnc
                DgridOC.Rows(i).Cells("IdOrdenCompraDet").Value = Obj.IdOrdenCompraDet
                DgridOC.Rows(i).Cells("NoLinea").Value = Obj.No_Linea
                DgridOC.Rows(i).Cells("Factor1").Value = clsLnProducto_presentacion.Get_Factor_By_IdProducto_And_IdPresentacion(Obj.Producto.IdProducto, Obj.Presentacion.IdPresentacion)
                DgridOC.Rows(i).Cells("AtributoVariante1").Value = Obj.Atributo_variante_1

            Next

            DgridOC.ResumeLayout()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    '#EJC20180114:Ref
    'Private Sub Cargar_Detalle_OC(ByVal pLista As List(Of clsBeTrans_oc_det))

    '    Try

    '        DgridOC.SuspendLayout() : DgridOC.Rows.Clear()

    '        Dim i As Integer = -1

    '        If ListPC.Count > 0 Then

    '            For Each bo As clsProductoCargar In ListPC

    '                'For Each Obj As clsBeTrans_oc_det In pLista.FindAll(Function(b) b.IdProductoBodega = bo.IdProductoBodega)
    '                For Each Obj As clsBeTrans_oc_det In pLista.FindAll(Function(b) b.No_Linea = bo.No_Linea AndAlso b.IdProductoBodega = bo.IdProductoBodega)

    '                    i = DgridOC.Rows.Add(0, Obj.Producto.Codigo, Obj.Producto.Nombre)

    '                    Application.DoEvents()

    '                    If Obj.UnidadMedida IsNot Nothing Then
    '                        setUnidadMedidaGrid1(Obj, i)
    '                    Else
    '                        Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", Obj.Producto.Nombre))
    '                    End If

    '                    If Obj.Arancel IsNot Nothing Then
    '                        setArancelGrid1(Obj, i)
    '                    Else
    '                        'Dim DgCombo As New DataGridViewComboBoxCell()
    '                        DgComboArancel = TryCast(DgridOC.Rows(i).Cells("Arancel"), DataGridViewComboBoxCell)
    '                        DgComboArancel.DataSource = Nothing
    '                    End If

    '                    DgridOC.Rows(i).Cells("Cantidad").Value = Obj.Cantidad
    '                    DgridOC.Rows(i).Cells("Costo").Value = Obj.Costo
    '                    DgridOC.Rows(i).Cells("PesoOC").Value = Obj.Peso
    '                    DgridOC.Rows(i).Cells("Total").Value = Obj.Total_linea
    '                    DgridOC.Rows(i).Cells("IdProducto").Value = Obj.IdProductoBodega
    '                    DgridOC.Rows(i).Cells("Key").Value = Obj.Producto.IdProducto

    '                    Dim l As List(Of clsBeProducto_presentacion) = clsLnProducto_presentacion.GetByProducto(Obj.Producto.IdProducto).ToList

    '                    If l.Count > 0 Then
    '                        LlenaPresentacionGrid1(l, i, Obj.Presentacion.IdPresentacion)
    '                    Else
    '                        'Dim DgCombo As New DataGridViewComboBoxCell()
    '                        DgComboPresentacion = TryCast(DgridOC.Rows(i).Cells("Presentacion"), DataGridViewComboBoxCell)
    '                        DgComboPresentacion.DataSource = Nothing
    '                    End If

    '                    DgridOC.Rows(i).Cells("IsNew").Value = Obj.IsNew
    '                    DgridOC.Rows(i).Cells("IsNewRecepcion").Value = Obj.ExisteEnRecepcion
    '                    DgridOC.Rows(i).Cells("IdOrdenCompraEnc").Value = Obj.IdOrdenCompraEnc
    '                    DgridOC.Rows(i).Cells("IdOrdenCompraDet").Value = Obj.IdOrdenCompraDet
    '                    DgridOC.Rows(i).Cells("NoLinea").Value = Obj.No_Linea
    '                    DgridOC.Rows(i).Cells("Factor1").Value = clsLnProducto_presentacion.GetFactorByProductoPresentacion(Obj.Producto.IdProducto, Obj.IdPresentacion)
    '                    DgridOC.Rows(i).Cells("AtributoVariante1").Value = Obj.Atributo_variante_1

    '                Next

    '            Next

    '        Else

    '            For Each Obj As clsBeTrans_oc_det In pLista

    '                i = DgridOC.Rows.Add(0, Obj.Producto.Codigo, Obj.Producto.Nombre)

    '                If Obj.UnidadMedida IsNot Nothing Then
    '                    setUnidadMedidaGrid1(Obj, i)
    '                Else
    '                    Throw New Exception(String.Format("Configure la Unidad de Medida del Producto {0}", Obj.Producto.Nombre))
    '                End If

    '                If Obj.Arancel IsNot Nothing Then
    '                    setArancelGrid1(Obj, i)
    '                Else
    '                    Dim DgCombo As New DataGridViewComboBoxCell()
    '                    DgCombo = TryCast(DgridOC.Rows(i).Cells("Arancel"), DataGridViewComboBoxCell)
    '                    DgCombo.DataSource = Nothing
    '                End If

    '                DgridOC.Rows(i).Cells("Cantidad").Value = Obj.Cantidad
    '                DgridOC.Rows(i).Cells("PesoOC").Value = Obj.Peso
    '                DgridOC.Rows(i).Cells("Costo").Value = Obj.Costo
    '                DgridOC.Rows(i).Cells("Total").Value = Obj.Total_linea
    '                DgridOC.Rows(i).Cells("IdProducto").Value = Obj.IdProductoBodega
    '                DgridOC.Rows(i).Cells("Key").Value = Obj.Producto.IdProducto

    '                Dim l As List(Of clsBeProducto_presentacion) = clsLnProducto_presentacion.GetByProducto(Obj.Producto.IdProducto).ToList

    '                If l.Count > 0 Then
    '                    LlenaPresentacionGrid1(l, i, Obj.Presentacion.IdPresentacion)
    '                Else
    '                    Dim DgCombo As New DataGridViewComboBoxCell()
    '                    DgCombo = TryCast(DgridOC.Rows(i).Cells("Presentacion"), DataGridViewComboBoxCell)
    '                    DgCombo.DataSource = Nothing
    '                End If

    '                DgridOC.Rows(i).Cells("IsNew").Value = Obj.IsNew
    '                DgridOC.Rows(i).Cells("IsNewRecepcion").Value = Obj.ExisteEnRecepcion
    '                DgridOC.Rows(i).Cells("IdOrdenCompraEnc").Value = Obj.IdOrdenCompraEnc
    '                DgridOC.Rows(i).Cells("IdOrdenCompraDet").Value = Obj.IdOrdenCompraDet
    '                DgridOC.Rows(i).Cells("NoLinea").Value = Obj.No_Linea
    '                DgridOC.Rows(i).Cells("Factor1").Value = clsLnProducto_presentacion.GetFactorByProductoPresentacion(Obj.Producto.IdProducto, Obj.Presentacion.IdPresentacion)
    '                DgridOC.Rows(i).Cells("AtributoVariante1").Value = Obj.Atributo_variante_1

    '            Next

    '        End If

    '        DgridOC.ResumeLayout()

    '    Catch ex As Exception
    '        SplashScreenManager.CloseForm(False)
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try

    'End Sub

    Private Sub setUnidadMedidaGrid1(ByVal Obj As Object, ByVal pIndex As Integer)

        Try

            Dim D As New DataTable("UnidadMedida")
            D.Columns.Add("Id", GetType(Integer))
            D.Columns.Add("Nombre", GetType(String))
            D.Rows.Add(Obj.UnidadMedida.IdUnidadMedida, Obj.UnidadMedida.Nombre)
            Dim DgCombo As New DataGridViewComboBoxCell()
            DgCombo = TryCast(DgridOC.Rows(pIndex).Cells("UnidadMedida"), DataGridViewComboBoxCell)
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

    Private Sub setArancelGrid1(ByVal Obj As Object, ByVal pIndex As Integer)

        Try

            Dim DA As New DataTable("Arancel")
            DA.Columns.Add("Id", GetType(Integer))
            DA.Columns.Add("Nombre", GetType(String))
            DA.Rows.Add(Obj.Arancel.IdArancel, Obj.Arancel.Nombre)

            DgComboArancel = TryCast(DgridOC.Rows(pIndex).Cells("Arancel"), DataGridViewComboBoxCell)
            DgComboArancel.DataSource = DA
            DgComboArancel.ValueMember = "Id"
            DgComboArancel.DisplayMember = "Nombre"
            DgComboArancel.Value = CInt(DA(0)(0))

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub LlenaPresentacionGrid1(ByVal pList As List(Of clsBeProducto_Presentacion),
                                  ByVal pIndex As Integer, Optional ByVal pIdPresentacion As Integer = 0)

        Try

            DgComboPresentacion = TryCast(DgridOC.Rows(pIndex).Cells("Presentacion"), DataGridViewComboBoxCell)
            DgComboPresentacion.DataSource = pList
            DgComboPresentacion.ValueMember = "IdPresentacion"
            DgComboPresentacion.DisplayMember = "Nombre"

            If pIdPresentacion <> 0 Then
                DgComboPresentacion.Value = pIdPresentacion
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub comboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim i As Integer = DgridDetalleRec.CurrentRow.Index

            DgComboPresentacion = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
            'Console.Write(DgComboPresentacion.Value)

            If Not DgComboPresentacion.Value Is Nothing Then

                If pListBeStockRec.Count > 0 Then
                    pListBeStockRec.Find(Function(p) _
                                     p.IdProductoBodega = CInt(DgridDetalleRec.Rows(i).Cells("IdProductoP").Value) AndAlso
                                     p.ProductoValidado = False AndAlso
                                     p.Presentacion.IdPresentacion = CInt(DgridDetalleRec.Rows(i).Cells("PresentacionP").Value)).Presentacion.IdPresentacion = DgComboPresentacion.Value
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

    Private Function SetCantidadGrid1() As Double

        Dim c As Double

        Try

            For x = 0 To DgridOC.Rows.Count - 1
                If String.IsNullOrEmpty(DgridOC.Rows(x).Cells("Producto").Value) = False Then
                    Dim str As String = DgridOC.Rows(x).Cells("Cantidad").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(DgridOC.Item("Cantidad".ToLower, x).Value)
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

    Private Function SetCostoGrid1() As Double

        Dim c As Double

        Try

            For x = 0 To DgridOC.Rows.Count - 1
                If String.IsNullOrEmpty(DgridOC.Rows(x).Cells("Producto").Value) = False Then
                    Dim str As String = DgridOC.Rows(x).Cells("Costo").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(DgridOC.Item("Costo".ToLower, x).Value)
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

    Private Function SetTotalGrid1() As Double

        Dim c As Double

        Try

            For x = 0 To DgridOC.Rows.Count - 1
                If String.IsNullOrEmpty(DgridOC.Rows(x).Cells("Producto").Value) = False Then
                    Dim str As String = DgridOC.Rows(x).Cells("Total").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(DgridOC.Item("Total".ToLower, x).Value)
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

    Private Sub Get_Totales_Grid_OC(ByVal pIndex As Integer)

        Try

            If IsNumeric(DgridOC.Rows(pIndex).Cells("Cantidad").Value) AndAlso
                IsNumeric(DgridOC.Rows(pIndex).Cells("Costo").Value) Then

                Dim costo As Decimal
                Dim cantidad As Decimal = DgridOC.Rows(pIndex).Cells("Cantidad").Value

                costo = CDec(DgridOC.Rows(pIndex).Cells("Costo").Value)

                Dim total As Decimal = Math.Round(cantidad * costo, 6)
                DgridOC.Rows(pIndex).Cells("Total").Value = total

                lblCantidad.Text = "Cant: " & Format(SetCantidadGrid1(), "N2")
                lblCosto.Text = "Costo: " & Format(SetCostoGrid1(), "c")
                lblTotal.Text = "Total: " & Format(SetTotalGrid1(), "c")
                lblPesoOC.Text = "Peso: " & Format(GetTotalPesoOCGrid(), "N2")
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Function GetTotalPesoOCGrid() As Double

        Dim c As Double

        Try

            For x = 0 To DgridOC.Rows.Count - 1
                If String.IsNullOrEmpty(DgridOC.Rows(x).Cells("Producto").Value) = False Then
                    Dim str As String = DgridOC.Rows(x).Cells("PesoOC").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(DgridOC.Item("PesoOC".ToLower, x).Value)
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

    Private ErrorEnPresentacion As String = ""
    Private Sub Llena_Presentacion_Grid(ByVal pList As List(Of clsBeProducto_Presentacion),
                                       ByVal pIndex As Integer,
                                        Optional ByVal pIdPresentacion As Integer = 0)

        Try

            Dim DgCombo As New DataGridViewComboBoxCell()
            DgCombo = TryCast(DgridDetalleRec.Rows(pIndex).Cells("PresentacionP"), DataGridViewComboBoxCell)

            Dim PresentacionesPallet = Nothing
            Dim PresentacionCodigo As New clsBeProducto_Presentacion

            Dim vCodigoGrid As String = DgridDetalleRec.Rows(pIndex).Cells("CodigoP").Value

            'PresentacionesPallet = (From p in pList).Where(Function(x) x.EsPallet = True) 'Buscar solo las pres. que sean pallets.

            If pList Is Nothing Then 'No tiene presentaciones de pallets definidas.
                ErrorEnPresentacion = String.Format("Error en datos: El Código {0} no tiene presentaciones definidas.", vCodigoGrid)
                'Throw New Exception(ErrorEnPresentacion)
            Else

                'Buscar presentación por código ingresado en grid.
                PresentacionCodigo = (From P In pList).Where(Function(x) x.Codigo_barra = vCodigoGrid).FirstOrDefault()

                If Not PresentacionCodigo Is Nothing Then

                    If PresentacionCodigo.EsPallet Then

                        Dim List = (From P In pList).Where(Function(x) x.Codigo_barra = vCodigoGrid).ToList()

                        DgCombo.DataSource = List
                        DgCombo.ValueMember = "IdPresentacion"
                        DgCombo.DisplayMember = "Nombre"
                        DgCombo.Value = PresentacionCodigo.IdPresentacion

                        If pIdPresentacion <> 0 Then
                            DgCombo.Value = pIdPresentacion
                        End If

                        ErrorEnPresentacion = ""

                    Else
                        Dim List = (From P In pList).Where(Function(x) x.Codigo_barra = vCodigoGrid).ToList()
                        DgCombo.DataSource = List
                        DgCombo.ValueMember = "IdPresentacion"
                        DgCombo.DisplayMember = "Nombre"
                        DgCombo.Value = List(0).IdPresentacion
                        ErrorEnPresentacion = String.Format("La presentación {1} para el Código {0} no es pallet.", vCodigoGrid, PresentacionCodigo.Nombre)
                        'Throw New Exception(ErrorEnPresentacion)
                    End If

                Else

                    DgCombo.DataSource = pList
                    DgCombo.ValueMember = "IdPresentacion"
                    DgCombo.DisplayMember = "Nombre"

                    If pIdPresentacion <> 0 Then
                        DgCombo.Value = pIdPresentacion
                    End If

                    ErrorEnPresentacion = ""

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            Throw New Exception(String.Format("{0}", ex.Message))
        End Try

    End Sub

    Private Sub LlenaEstados(ByVal pList As List(Of clsBeProducto_estado),
                             ByVal pIndex As Integer, Optional ByVal pIdEstado As Integer = 0)

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
                    DgCombo.Value = pList(0).IdEstado
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

    Private Sub setTotalGrid2(ByVal pIndex As Integer)

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
                        ValidarCambioPresentacion(pIndex)
                    End If

                End If

                DgridDetalleRec.Rows(pIndex).Cells("Peso").Value = vPesoUnitario * cantidad

                lblCantidadR.Text = "Cant: " & Format(SetCantidadGrid2(), "N2")
                lblCostoR.Text = "Costo: " & Format(SetCostoGrid2(), "c")
                lblTotalR.Text = "Total: " & Format(Calcula_Total_Grid(), "c")
                lblPesoR.Text = "Peso: " & Format(Get_Peso_Grid(), "N2")

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub grdLista_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgridOC.CellValueChanged
        If DgridOC.Columns(e.ColumnIndex).Name() = "Cantidad" Or DgridOC.Columns(e.ColumnIndex).Name() = "Costo" Then
            Get_Totales_Grid_OC(e.RowIndex)
        End If
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
            For i As Integer = 0 To DgridOC.Rows.Count - 1

                If DgridOC.Rows(i).Cells("Producto").Value = pProducto Then
                    Costo = CDbl(DgridOC.Rows(i).Cells("Costo").Value)
                    Exit For
                End If

            Next

            Return Costo

        Catch ex As Exception
            Return 0.0
        End Try

    End Function

    Private Sub Set_Stock_Parametro(ByVal pObjProducto As clsBeProducto, ByVal pIdRecepcionDet As Integer, ByVal pRowIndex As Integer)

        Try

            If DgridDetalleRec.Rows.Count > 0 Then

                Dim frmCapturaParametros As New frmCapturaParametroRecepcionS() With {.IdEmpresa = AP.IdEmpresa,
                    .IdBodega = cmbBodega.EditValue,
                    .IdPropietario = clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, cmbPropietario.EditValue),
                    .pIdPropietarioBodega = cmbPropietario.EditValue,
                    .pBeProducto = pObjProducto,
                    .pIdRecepcionDet = pIdRecepcionDet,
                    .pFechaRecepcion = dtmFechaRecepcion.EditValue,
                    .pIndex = pListBeStockRec.FindIndex(Function(f) f.IdRecepcionDet = pIdRecepcionDet)}
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

                        pListPresentacionesProd = clsLnProducto_presentacion.Get_All_By_IdProducto(pObjProducto.IdProducto).ToList

                        If frmCapturaParametros.pBePresentacionProducto IsNot Nothing AndAlso pListPresentacionesProd.Count > 0 Then
                            Llena_Presentacion_Grid(pListPresentacionesProd, pRowIndex, frmCapturaParametros.pBePresentacionProducto.IdPresentacion)
                        Else
                            Dim DgCombo As New DataGridViewComboBoxCell()
                            DgCombo = TryCast(DgridDetalleRec.CurrentRow.Cells("PresentacionP"), DataGridViewComboBoxCell)
                            DgCombo.DataSource = Nothing
                        End If

                    Else
                        GuardarStockRec(pObjProducto)
                    End If

                Else
                    GuardarStockRec(pObjProducto)
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub GuardarStockRec(ByVal pObjProducto As clsBeProducto)

        Dim pIdRecepcionDet As Integer
        Dim pIndex As Integer

        Try

            pIdPropietarioBodega = cmbPropietario.EditValue

            pIndex = pListBeStockRec.FindIndex(Function(f) f.IdProductoBodega = pObjProducto.IdProductoBodega)

            If pIndex > -1 Then
                pIdRecepcionDet = pListBeStockRec(pIndex).IdRecepcionDet
            Else

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

                Dim le As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_All_By_IdPropietarioBodega(cmbPropietario.EditValue, cmbBodega.EditValue).ToList

                pListPresentacionesProd = clsLnProducto_presentacion.Get_All_By_IdProducto(ObjP.IdProducto).ToList

                If pListPresentacionesProd IsNot Nothing AndAlso pListPresentacionesProd.Count > 0 Then
                    Llena_Presentacion_Grid(pListPresentacionesProd, i)
                Else
                    Dim DgCombo As New DataGridViewComboBoxCell()
                    DgCombo = TryCast(DgridDetalleRec.Rows(i).Cells("PresentacionP"), DataGridViewComboBoxCell)
                    DgCombo.DataSource = Nothing
                End If

                If le.Count > 0 Then
                    LlenaEstados(le, i)
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

                    If Not pObjOC Is Nothing Then

                        If pObjOC.IdOrdenCompraEnc <> 0 Then

                            vTransOcDet = pObjOC.DetalleOC.Find(Function(x) x.No_Linea = vNoLinea)

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

                            pListPresentacionesProd = clsLnProducto_presentacion.Get_All_By_IdProducto(BeProducto.IdProducto).ToList

                            Dim le As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_All_By_IdPropietarioBodega(cmbPropietario.EditValue, cmbBodega.EditValue).ToList

                            If pListPresentacionesProd IsNot Nothing AndAlso pListPresentacionesProd.Count > 0 Then
                                Llena_Presentacion_Grid(pListPresentacionesProd, e.RowIndex)
                            Else
                                Dim DgCombo As New DataGridViewComboBoxCell()
                                DgCombo = TryCast(DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP"), DataGridViewComboBoxCell)
                                DgCombo.DataSource = Nothing
                                ErrorEnPresentacion = String.Format("El Código {0} no tiene presentaciones definidas.", lCodigo)
                            End If

                            'AndAlso pObjOC.listaD.Count > 0'

                            If Not (pObjOC Is Nothing) AndAlso Not (pObjOC.DetalleOC Is Nothing) Then

                                Dim LIst As List(Of clsBeTrans_oc_det) = pObjOC.DetalleOC.ToList

                                If LIst IsNot Nothing AndAlso LIst.Count > 0 Then

                                    Dim lIndex As Integer = LIst.FindIndex(Function(b) b.IdProductoBodega = BeProducto.IdProductoBodega _
                                                                           AndAlso b.Producto.IdProducto = BeProducto.IdProducto)

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

                                    End If

                                End If

                            End If

                            If le.Count > 0 Then
                                LlenaEstados(le, e.RowIndex)
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
                                DgridDetalleRec.Rows(e.RowIndex).Cells("IdRecepcionDet").Value = lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1
                                If Val(DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value) = 0 Then
                                    DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value = lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1
                                End If
                                Set_Stock_Parametro(BeProducto, lBeTransRecDet.Max(Function(b) b.IdRecepcionDet) + 1, e.RowIndex)
                            Else

                                vIndiceRecDet = pListBeStockRec.FindIndex(Function(b) b.IdRecepcionDet = DgridDetalleRec.Rows(e.RowIndex).Cells("IdRecepcionDet").Value)

                                If vIndiceRecDet = -1 Then
                                    lMaxIdRecepcionDetParametro += 1
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
                                Set_Stock_Parametro(BeProducto, lMaxIdRecepcionDetParametro, e.RowIndex)
                            End If

                        Else
                            Throw New Exception(String.Format("El Código {0} no existe.", lCodigo))
                        End If

                    End If

                ElseIf DgridDetalleRec.Columns(e.ColumnIndex).Name() = "CantidadP" Or DgridDetalleRec.Columns(e.ColumnIndex).Name() = "CostoP" Then
                    setTotalGrid2(e.RowIndex)
                ElseIf DgridDetalleRec.Columns(e.ColumnIndex).Name() = "PresentacionP" Or DgridDetalleRec.Columns(e.ColumnIndex).Name() = "TotalP" Then

                    '#EJC20171018_0928AM: Fix, error al colocar peso presentación por el Or, 
                    'la columna afectada es el TotalP, pero siempre se ejecutaba la presentación aunque no fuera esa columna.
                    If DgridDetalleRec.Columns(e.ColumnIndex).Name() = "PresentacionP" Then
                        setPesoPresentacion(e.RowIndex, e.ColumnIndex)
                    End If

                    If DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value IsNot DBNull.Value AndAlso DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value IsNot Nothing Then
                        ValidarCambioPresentacion(e.RowIndex)
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("Error: {0} ", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ValidarCambioPresentacion(ByVal pIndexRow As Integer)

        Try

            Dim lIdProducto As Integer = DgridDetalleRec.Rows(pIndexRow).Cells("KeyP").Value
            Dim lIdProductoBodega As Integer = DgridDetalleRec.Rows(pIndexRow).Cells("IdProductoP").Value
            Dim lIdPresentacion As Integer = DgridDetalleRec.Rows(pIndexRow).Cells("PresentacionP").Value
            Dim lCantidad As Integer = DgridDetalleRec.Rows(pIndexRow).Cells("CantidadP").Value

            '#EJC20171018_0916AM: Se valida primero el encabezado si el encabezado es nothing, el detalle throw exception por object reference null
            If Not (pObjOC Is Nothing) Then

                '#EJC20171018_0916AM: Se valida después el detalle porque si el encabezado es nothing, el detalle throw exception por object reference null
                If Not (pObjOC.DetalleOC Is Nothing) Then

                    Dim LIst As List(Of clsBeTrans_oc_det) = pObjOC.DetalleOC.ToList

                    If LIst IsNot Nothing AndAlso LIst.Count > 0 Then

                        Dim lIndex As Integer = LIst.FindIndex(Function(b) b.IdProductoBodega = lIdProductoBodega _
                                                               AndAlso b.Presentacion.IdProducto = lIdProducto _
                                                               AndAlso b.Presentacion.IdPresentacion = lIdPresentacion)

                        If lIndex > -1 = False Then

                            Dim lFactorR As Double = clsLnProducto_presentacion.Get_Factor_By_IdProducto_And_IdPresentacion(lIdProducto, lIdPresentacion)
                            Dim ltotalR As Double = lCantidad * lFactorR

                            lIndex = pObjOC.DetalleOC.ToList.FindIndex(Function(b) b.IdProductoBodega = lIdProductoBodega AndAlso b.Presentacion.IdProducto = lIdProducto)

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

    Private Sub setPesoPresentacion(ByVal pIndexRow As Integer, ByVal pIndexColumn As Integer)

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
    Dim cPresentacion As DataGridViewCell

    Private Sub grdListaRecepcion_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DgridDetalleRec.RowValidating

        Try

            FechaVencimientoCell = DgridDetalleRec.Rows(e.RowIndex).Cells(DgridDetalleRec.Columns("FechaVencimiento").Index)
            LoteCell = DgridDetalleRec.Rows(e.RowIndex).Cells(DgridDetalleRec.Columns("Lote").Index)
            cPresentacion = DgridDetalleRec.Rows(e.RowIndex).Cells(DgridDetalleRec.Columns("PresentacionP").Index)

            If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("ProductoP").Value) = False Then

                If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value) Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").ErrorText = "La Cantidad no puede estar vacía."
                    Exit Sub
                ElseIf IsNumeric(DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value) = False Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").ErrorText = "La Cantidad debe ser un valor númerico."
                    Exit Sub
                ElseIf CDbl(DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").Value) = 0 Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").ErrorText = "La Cantidad no puede ser 0."
                    Exit Sub
                Else
                    e.Cancel = False
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CantidadP").ErrorText = String.Empty
                End If

                If ErrorEnPresentacion <> "" Then
                    e.Cancel = True
                    cPresentacion.ErrorText = ErrorEnPresentacion
                    DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").ErrorText = ErrorEnPresentacion
                    Exit Sub
                Else
                    e.Cancel = False
                    DgridDetalleRec.Rows(e.RowIndex).Cells("PresentacionP").ErrorText = String.Empty
                End If

                If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value) Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").ErrorText = "El Costo no puede estar vacío."
                    Exit Sub
                ElseIf IsNumeric(DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value) = False Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").ErrorText = "El Costo debe ser un valor númerico."
                    Exit Sub
                ElseIf Val(DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").Value) = 0 Then
                    e.Cancel = True
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").ErrorText = "El Costo no puede ser 0."
                    Exit Sub
                Else
                    e.Cancel = False
                    DgridDetalleRec.Rows(e.RowIndex).Cells("CostoP").ErrorText = String.Empty
                End If

                If Not BeProducto Is Nothing Then

                    If BeProducto.Control_vencimiento Then
                        If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("FechaVencimiento").Value) Then
                            e.Cancel = True
                            FechaVencimientoCell.ErrorText = "Ingrese fecha vencimiento"
                            Exit Sub
                        Else
                            e.Cancel = False
                            FechaVencimientoCell.ErrorText = ""
                        End If
                    Else
                        e.Cancel = False
                        FechaVencimientoCell.ErrorText = ""
                    End If

                    If BeProducto.Control_lote Then
                        If String.IsNullOrEmpty(DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").Value) Then
                            e.Cancel = True
                            LoteCell.ErrorText = "Ingrese lote"
                            DgridDetalleRec.Rows(e.RowIndex).Cells("Lote").ErrorText = "Ingrese lote"
                            Exit Sub
                        Else
                            e.Cancel = False
                            LoteCell.ErrorText = ""
                        End If
                    Else
                        e.Cancel = False
                        LoteCell.ErrorText = ""
                    End If

#Region "Validación no_linea"
                    Dim vNoLinea As String = IIf(IsDBNull(DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value), "", DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").Value)

                    If Not pObjOC Is Nothing Then

                        If vNoLinea <> "" Then

                            If Not IsNumeric(vNoLinea) Then
                                e.Cancel = True
                                DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = "El número de línea debe ser un valor numérico"
                                Exit Sub
                            End If

                            Dim ExisteNoLineaEnPC As Boolean = False

                            For Each DetInOc In pObjOC.DetalleOC
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
                                e.Cancel = False
                                DgridDetalleRec.Rows(e.RowIndex).Cells("No_Linea").ErrorText = ""
                            End If

                        Else

                            Dim PCContieneLineasVacias As Boolean = False
                            For Each DetInOc In pObjOC.DetalleOC
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

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        Try
            If Actualizar() Then
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Se actualizó la Recepción.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If
                Close()
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

            If Not pObj.ConRef Then
                Campos_Pedido_Compra(False)
            Else
                Campos_Pedido_Compra(True)
                txtIdOrdenCompra.Enabled = True
                lnk.Enabled = (txtIdOrdenCompra.Text = "")
                txtIdOrdenCompra.Enabled = (txtIdOrdenCompra.Text = "")
                xtrRecepcion.TabPages.Add(DetalleOC)
            End If

            HabilitarTabDetalleRecepcion(pObj)
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

            If OpcionesMenu IsNot Nothing Then
                TR.OpcionesMenu = OpcionesMenu
                TR.mnuActualizar.Enabled = OpcionesMenu.Leer
            End If

            TR.ShowDialog()

            If TR.DialogResult = DialogResult.OK Then

                If TR.pObj IsNot Nothing AndAlso String.IsNullOrEmpty(TR.pObj.IdTipoTransaccion) = False Then

                    txtIdTipoTR.Text = TR.pObj.IdTipoTransaccion
                    txtDescripcionTR.Text = TR.pObj.Descripcion
                    Configura_Opcion_Tipo_Rec(TR.pObj)

                    If TR.pObj.UsaHH = 0 Then
                        chkRecepcionManual.Checked = True
                    Else
                        chkRecepcionManual.Checked = False
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

            If Obj.IdTipoTransaccion = "HSOC00" OrElse
               Obj.IdTipoTransaccion = "HSOD00" OrElse
               Obj.IdTipoTransaccion = "MCOC00" OrElse
               Obj.IdTipoTransaccion = "MCOD00" OrElse
               Obj.IdTipoTransaccion = "MSOC00" OrElse
               Obj.IdTipoTransaccion = "MSOD00" OrElse
               Obj.IdTipoTransaccion = "PICH000" Then

                xtrRecepcion.TabPages.Add(DetRec)
                ProductoP.Width = 300
                Estado.Width = 130

            ElseIf Modo = TipoTrans.Editar Then
                xtrRecepcion.TabPages.Add(DetRec)
                ProductoP.Width = 300
                Estado.Width = 130
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
                        HabilitarTabOperador(Obj.IdTipoTransaccion)
                        Configura_Opcion_Tipo_Rec(Obj)

                    Else

                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show(String.Format("No existe Tipo Transacción con código {0}", txtIdTipoTR.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub Oculta_Campos_Check
        chkEscanear.Visible = False
        chkTomarFoto.Visible = False
        chkRecepcionManual.Visible = False
        lblSEscanearUbicRec.Visible = False
        lblTomarFotos.Visible = False
        lblRecManual.Visible = False
        lblMuestraCosto.Visible = False
        chkMuestraCosto.Visible = False
        lblParaPorCodigo.Visible = False
        chkParaPorCodigo.Visible = False
    End Sub
    Private Sub Campos_Pedido_Compra(ByVal Mostrar As Boolean)

        lnk.Visible = Mostrar
        txtIdOrdenCompra.Visible = Mostrar
        txtOC.Visible = Mostrar

        If Not Mostrar Then
            xtrRecepcion.TabPages.Remove(DetalleOC)
        Else
            xtrRecepcion.TabPages.Add(DetalleOC)
        End If

    End Sub

    Private Sub Deshabilita_Tipo_Documento()
        txtIdTipoTR.Enabled = False
        lnkTipoT.Enabled = False
    End Sub

    Private Sub Set_Transaccion(ByVal Obj As clsBeTrans_re_tr)

        Try

            Select Case Obj.IdTipoTransaccion

                Case "HCOD00", "MCOD00"
                    lnk.Text = "Pedido"
                    DetalleOC.Text = "Detalle de Pedido"
                    DetRec.Text = "Detalle de Devolución"

                Case "PICH000"

                    Deshabilita_Tipo_Documento()
                    Oculta_Campos_Check()
                    xtrRecepcion.TabPages.Remove(Imagenes)
                    cmdFinalizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                Case Else
                    lnk.Text = "Orden de Compra"
                    DetalleOC.Text = "Detalle de OC"
                    DetRec.Text = "Detalle de Recepción OC"
            End Select

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub HabilitarTabOperador(ByVal IdTipoTransaccion As String)

        Try

            If IdTipoTransaccion = "HCOC00" OrElse IdTipoTransaccion = "HCOD00" OrElse
                IdTipoTransaccion = "HSOC00" OrElse IdTipoTransaccion = "HSOD00" OrElse
                IdTipoTransaccion = "HHSR00" OrElse IdTipoTransaccion = "PICH000" Then
                xtrRecepcion.TabPages.Add(DetOp)
            Else
                xtrRecepcion.TabPages.Remove(DetOp)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub ListaOperadores()

        Try

            Grid.BeginUpdate()

            If DT.Rows.Count > 0 Then

                For i As Integer = 0 To DT.Rows.Count - 1

                    Dim lRow As DataRow = DsOrdenCompraRecepcionOperador.Data.NewRow
                    lRow.Item("IdOperadorBodega") = DT(i)(0)
                    lRow.Item("Operador") = DT(i)(1)
                    lRow.Item("Selección") = False
                    lRow.Item("colUsaHH") = CBool(DT(i)(2))

                    If TipoTrans.Editar Then

                        If pListOpe IsNot Nothing AndAlso pListOpe.Count > 0 Then

                            For Each Obj As clsBeTrans_re_op In pListOpe

                                If Obj.IdOperadorBodega = CInt(DT(i)(0)) Then
                                    lRow.Item("Selección") = True
                                    lRow.Item("IdOperadorRec") = Obj.IdOperadorRec
                                End If

                            Next

                        End If

                    End If

                    DsOrdenCompraRecepcionOperador.Data.AddDataRow(lRow)
                Next

            End If

            Grid.EndUpdate()
            Grid.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GrdOperadorBobega.Columns("Selección").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

            If GrdOperadorBobega.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GrdOperadorBobega.RowCount)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = GrdOperadorBobega.GetFocusedRow
                Dim lIndex As Integer = -1
                Dim d As Integer = Dr.Item("IdOperadorBodega")

                lIndex = pListOpe.FindIndex(Function(b) b.IdOperadorBodega = d)

                If lIndex > -1 Then

                    If ritem.Checked = False Then
                        If pListOpe(lIndex).IdOperadorRec > 0 AndAlso pListOpe(lIndex).IdRecepcionEnc Then
                            clsLnTrans_re_op.Delete(pListOpe(lIndex).IdOperadorRec, pListOpe(lIndex).IdRecepcionEnc)
                            DsOrdenCompraRecepcionOperador.Clear()
                            ListaOperadores()
                        End If
                        pListOpe.RemoveAt(lIndex)
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

    End Sub

    Private Sub ValidaOperadores()

        Try

            DsOrdenCompraRecepcionOperador.Clear()

            DT = clsLnOperador_bodega.Get_All_By_IdBodega_DT(cmbBodega.EditValue)

            'ejc_18092016
            If gBeRecepcion IsNot Nothing Then
                pListOpe = clsLnTrans_re_op.Get_All_By_IdRecepcionEnc(gBeRecepcion.IdRecepcionEnc).ToList
            Else
                pListOpe = New List(Of clsBeTrans_re_op)()
            End If

            ListaOperadores()

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
                            pIdOrdenCompraEnc = gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc

                            If gBeRecepcion.Estado <> "Cerrado" Then
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

                        e.SuppressKeyPress = True

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
        Finalizar(lBeTransRecDet)
    End Sub

    Public Enum tIndicadorDiferenciaRec
        Producto_Faltante = 0
        Producto_Sobrante = 1
    End Enum

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

            If Not pObjOC Is Nothing Then

                lDetRecAnteriores = clsLnTrans_re_det.Get_All_By_Orden_Compra_Filtro(pObjOC.IdOrdenCompraEnc, gBeRecepcion.IdRecepcionEnc)

                For Each ProdInOc As clsBeTrans_oc_det In pListObjOrdeCompraDet

                    vPresOC = ProdInOc.Presentacion
                    vCantidadOCUMBas = ProdInOc.Cantidad

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

                                vPresRec = clsLnProducto_presentacion.GetSingle(vPresOC.IdPresentacion)

                                If vPresRec.EsPallet Then
                                    vCantidadRecUMBas += ProdInDetRec.cantidad_recibida / (vPresRec.Factor * vPresRec.CajasPorCama * vPresRec.CamasPorTarima)
                                Else
                                    vCantidadRecUMBas += ProdInDetRec.cantidad_recibida / vPresRec.Factor
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

    Private Function Finaliza_Recepcion(ByVal backOrder As Boolean, ByVal habilitarStock As Boolean) As Boolean

        Finaliza_Recepcion = False

        Try

            Dim vIdOrdenCompra As Integer

            If gBeRecepcion.OrdenCompraRec IsNot Nothing Then
                vIdOrdenCompra = gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc
            Else
                vIdOrdenCompra = 0
            End If

            clsLnTrans_re_enc.Finalizar_Recepcion(gBeRecepcion,
                                                  backOrder,
                                                  vIdOrdenCompra,
                                                  gBeRecepcion.IdRecepcionEnc,
                                                  AP.IdEmpresa,
                                                  cmbBodega.EditValue,
                                                  AP.UsuarioAp.IdUsuario, lBeTransRecDet, habilitarStock)

            Finaliza_Recepcion = True

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("Recepción {0} finalizada correctamente.", gBeRecepcion.IdRecepcionEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

        Try

            SplashScreenManager.CloseForm(False)

            If pListRecepcionDetalle.Count = 0 Then

                If XtraMessageBox.Show("La recepción no tiene detalle, ¿finalizar de todas formas?",
                                       Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Finalizar = Finaliza_Recepcion(False, False)

                End If

            Else

                If XtraMessageBox.Show("¿Desea finalizar la Recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim BackOrder As Boolean = False
                    Dim lIdOrdenCompra As Integer = 0

                    If gBeRecepcion.OrdenCompraRec IsNot Nothing Then

                        lIdOrdenCompra = gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc

                        BackOrder = Detalle_Tiene_Diferencia_Vrs_OC(pListRecepcionDetalle)

                        If BackOrder Then

                            If XtraMessageBox.Show("La recepción tiene diferencia. ¿dejar la orden de compra pendiente de finalización?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                BackOrder = False
                            End If

                        End If

                    End If

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Finalizando...")

                    Finalizar = Finaliza_Recepcion(BackOrder, gBeRecepcion.Habilitar_Stock)

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

        If e.KeyCode = Keys.F2 Then
            lnkAgregarProducto_LinkClicked()
        ElseIf e.KeyCode = Keys.F3 Then
            cmdVerParametros_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Delete Then
            EliminarFila(Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            Close()
        End If

    End Sub

    Private Sub ImprimirRecepcion()

        Cursor = Cursors.WaitCursor

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

        Try

            Using r As New frmReporte

                Dim rpt As New rptRecepcion
                Dim lDS As New DsRecepcion
                Dim DT As New DataTable

                If gBeRecepcion.OrdenCompraRec Is Nothing Then
                    DT = clsLnTrans_re_enc.Get_Impresion_By_IdRecepcionEnc_SinOC(gBeRecepcion.IdRecepcionEnc)
                    lDS.Tables("Imagen").Merge(ObtenerImagen("Imagen", "Empresa", "IdEmpresa=" & AP.IdEmpresa))
                    lDS.Tables("Data").Merge(DT)
                Else

                    If gBeRecepcion.OrdenCompraRec.IdOrdenCompraEnc <> 0 Then
                        DT = clsLnTrans_re_enc.Get_Impresion_By_IdRecepcionEnc(gBeRecepcion.IdRecepcionEnc)
                        DT.Columns("peso").Caption = "Peso"
                        lDS.Tables("Imagen").Merge(ObtenerImagen("Imagen", "Empresa", "IdEmpresa=" & AP.IdEmpresa))
                        lDS.Tables("Data").Merge(DT)
                    Else
                        DT = clsLnTrans_re_enc.Get_Impresion_By_IdRecepcionEnc_SinOC(gBeRecepcion.IdRecepcionEnc)
                        lDS.Tables("Imagen").Merge(ObtenerImagen("Imagen", "Empresa", "IdEmpresa=" & AP.IdEmpresa))
                        lDS.Tables("Data").Merge(DT)
                    End If

                End If

                For Each thisFormulaField In rpt.DataDefinition.FormulaFields
                    Select Case thisFormulaField.FormulaName
                        Case "{@NombreEmpresa}"
                            thisFormulaField.Text = String.Format("'{0}'", GetDBValue("Nombre", "Empresa", "IdEmpresa = " & AP.IdEmpresa))
                        Case "{@Bodega}"
                            thisFormulaField.Text = String.Format("'{0}'", GetDBValue("Nombre", "Bodega", "IdEmpresa = " & AP.IdBodega))
                        Case "{@Usuario}"
                            thisFormulaField.Text = String.Format("'{0}'", AP.UsuarioAp.Nombres)
                        Case Else
                            Exit Select
                    End Select
                Next

                r.Text = "Ingreso a Bodega"
                rpt.SetDataSource(lDS)

                r.rptView.ReportSource = rpt
                Cursor = Cursors.Default
                SplashScreenManager.CloseForm(False)
                r.ShowDialog()

            End Using

        Catch ex As Exception
            Cursor = Cursors.Default
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function ValidaFechaVencimiento(ByVal pFechaIngresada As Date, ByVal pNombreProducto As String) As Boolean

        Try

            Dim FHoy As Date = Now.Date

            If pFechaIngresada <= FHoy Then

                SplashScreenManager.CloseForm(False)

                If XtraMessageBox.Show(String.Format("La fecha de vencimiento del producto {0} es igual o menor a la fecha de hoy. ¿Desea ingresar un producto ya vencido?", pNombreProducto), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Return True

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                    If gBeRecepcion IsNot Nothing AndAlso gBeRecepcion.IdRecepcionEnc > 0 Then
                        SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")
                    Else
                        SplashScreenManager.Default.SetWaitFormDescription("Guardando...")
                    End If

                Else
                    Return False
                End If

            Else
                Return True
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

                        If Not pObjOC Is Nothing Then

                            txtNoLinea.AutoCompleteCustomSource.Clear()
                            txtNoLinea.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                            txtNoLinea.AutoCompleteSource = AutoCompleteSource.CustomSource

                            Dim lNoLineas = (From u In pObjOC.DetalleOC.AsEnumerable()
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

                        If (pObjOC Is Nothing OrElse pObjOC.IdOrdenCompraEnc = 0) Then

                            Dt = GetCodigosSugeridos()

                            txtCodigoProductoGrid.AutoCompleteCustomSource.AddRange(Dt.ToArray())

                        Else

                            Dim lCodigosTodos As New List(Of String)

                            Dim lCodigosOC = (From u In pObjOC.DetalleOC.AsEnumerable()
                                              Select u.Producto.IdProducto).Distinct()

                            Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(AP.IdBodega, pObjOC.IdPropietarioBodega)

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

            If TypeOf e.Control Is System.Windows.Forms.ComboBox Then
                If DgridDetalleRec.CurrentCell.OwningColumn.Name = "PresentacionP" Then
                    Dim cb As System.Windows.Forms.ComboBox = TryCast(e.Control, System.Windows.Forms.ComboBox)

                    'remove handler if it was added before
                    RemoveHandler cb.SelectedIndexChanged, AddressOf comboBox_SelectedIndexChanged
                    AddHandler cb.SelectedIndexChanged, AddressOf comboBox_SelectedIndexChanged
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

            Return clsLnProducto.Get_Codigos_Sugeridos(vIdPropietario, cmbBodega.EditValue, True)

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

    Private Sub GridViewImg_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewImg.RowStyle

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

    Private Sub GrdOperadorBobega_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GrdOperadorBobega.RowStyle

        Try

            GrdOperadorBobega.OptionsBehavior.Editable = True
            GrdOperadorBobega.OptionsSelection.EnableAppearanceFocusedCell = True

            GrdOperadorBobega.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GrdOperadorBobega.OptionsSelection.EnableAppearanceFocusedRow = True
            GrdOperadorBobega.OptionsSelection.EnableAppearanceHideSelection = True
            GrdOperadorBobega.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GrdOperadorBobega.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GrdOperadorBobega.Appearance.FocusedRow.ForeColor = Color.White
            GrdOperadorBobega.Appearance.SelectedRow.ForeColor = Color.White

            GrdOperadorBobega.Appearance.SelectedRow.Options.UseBackColor = True
            GrdOperadorBobega.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub grdLista_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgridOC.CellFormatting

        Try

            If e.ColumnIndex >= 6 AndAlso e.ColumnIndex <= 8 Then
                If e.Value IsNot Nothing AndAlso IsNumeric(e.Value.ToString()) Then
                    Dim Valor As Double = Double.Parse(e.Value.ToString())
                    e.Value = Valor.ToString("N6")
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub grdListaRecepcion_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgridDetalleRec.CellFormatting

        Try

            If e.ColumnIndex = 5 OrElse e.ColumnIndex = 7 OrElse
                e.ColumnIndex = 8 OrElse e.ColumnIndex = 9 OrElse e.ColumnIndex = 10 Then
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
                    Else
                        chkRecepcionManual.Checked = False
                    End If

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

                If BeProducto Is Nothing Then

                    Dim lCodigo As Object = DgridDetalleRec.CurrentRow.Cells("CodigoP").Value

                    BeProducto = New clsBeProducto
                    BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(lCodigo, cmbBodega.EditValue)

                    If BeProducto.IdProducto > 0 Then
                        Set_Stock_Parametro(BeProducto, vIdRecDet, DgridDetalleRec.CurrentRow.Index)
                    End If

                Else
                    Set_Stock_Parametro(BeProducto, vIdRecDet, DgridDetalleRec.CurrentRow.Index)
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

                If gBeRecepcion.Estado = "Cerrado" Then
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

                        If clsLnTrans_re_enc.Anular_Recepcion(gBeRecepcion, IdMotivoAnulacionBodega, gBeRecepcion.TareaHH) Then

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
                Obj = clsLnBodega_ubicacion.GetSingle(txtIdUbicacion.Text.Trim, AP.IdBodega, "ubicacion_recepcion", False)

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
                ValidaOperadores()

                Dim b As New clsBeBodega() With {.IdBodega = cmbBodega.EditValue}
                clsLnBodega.Obtener(b)

                'If Not pRecepcionInmediata Then

                '    If txtIdTipoTR.Text.Trim = "" Then
                '        If b IsNot Nothing AndAlso String.IsNullOrEmpty(b.IdTipoTransaccion) = False Then
                '            txtIdTipoTR.Text = b.IdTipoTransaccion
                '            txtIdTipoTR_TextChanged(Nothing, Nothing)
                '        End If
                '    End If

                'Else
                '    txtIdTipoTR.Text = "HCOC00" 'Recepción con OC.
                '    txtIdTipoTR_TextChanged(Nothing, Nothing)
                'End If

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
            End If
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDocumentoPreIngreso.ItemClick
        ImprimirRecepcion()
    End Sub

    '#CKFK 20180321 06:22PM Modifiqué el substring de los campos Nombre largo del producto, Dirección Proveedor y Nombre Proveedor
    Private Shared Sub Imprimir_Etiqueta_Pallet(ByVal pBeProductoPresentacion As clsBeVW_Impresion_Pallet,
                                  ByVal PrinterName As String)

        Dim vBarra As String = "(02)" & '(02)
            Strings.Right("00000000" & pBeProductoPresentacion.Producto_Codigo, 8) &
            "" & '(37)
            Strings.Right("00000" & pBeProductoPresentacion.Producto_Cantidad_Paralela, 5) &
            "" & '(10)
            Strings.Right("00000" & pBeProductoPresentacion.Producto_Lote, 5) &
            "" & '(17)
            FormatoFechas.tFecha(pBeProductoPresentacion.Producto_Vence)
        '&  
        '    "(11)" &
        '    FormatoFechas.tFecha(pBeProductoPresentacion.Fecha_Produccion) 

        Dim ZPLString As String = String.Format(
            "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR4,4~SD26^JUS^LRN^CI0^XZ
            ^XA
            ^MMT
            ^PW812
            ^LL1218
            ^LS0
            ^FT805,1178^A0I,25,24^FH\^FDTOM, WMS. - Pallet Reciving Tag^FS
            ^FO1,1^GB809,0,13^FS
            ^FO3,1150^GB806,0,9^FS
            ^FO4,728^GB806,0,8^FS
            ^FO4,1027^GB806,0,9^FS
            ^FT630,1053^A0I,34,24^FH\^FD{2}^FS
            ^FT797,1053^A0I,34,33^FH\^FDPropietario:^FS
            ^FT199,1050^A0I,34,33^FH\^FD{3}^FS
            ^FT351,1051^A0I,34,33^FH\^FDImprimi\A2:^FS
            ^FT228,1098^A0I,34,33^FH\^FD{0}^FS
            ^FT347,1102^A0I,34,33^FH\^FDBodega:^FS
            ^FT661,1099^A0I,34,33^FH\^FD{1}^FS
            ^FT794,1101^A0I,34,33^FH\^FDEmpresa:^FS
            ^FT793,982^A0I,34,33^FH\^FDDE:^FS
            ^FT793,838^A0I,25,24^FH\^FDDir.:^FS
            ^FT793,874^A0I,25,24^FH\^FDTel.:^FS
            ^FT342,923^A0I,25,24^FH\^FD{4}^FS
            ^FT294,959^A0I,25,24^FH\^FD{5}^FS
            ^FT339,996^A0I,25,24^FH\^FD{6}^FS
            ^FT793,913^A0I,25,24^FH\^FDNom.:^FS
            ^FT407,924^A0I,25,24^FH\^FDOBS.:^FS
            ^FT408,960^A0I,25,24^FH\^FDFecha P.C.:^FS
            ^FT408,996^A0I,25,24^FH\^FDP.C.#:^FS
            ^FT250,646^A0I,25,24^FH\^FD{7}^FS
            ^FT250,557^A0I,25,24^FH\^FD{8}^FS
            ^FT250,603^A0I,25,24^FH\^FD{9}^FS
            ^FT693,496^A0I,25,24^FH\^FD{10}^FS
            ^FT698,539^A0I,25,24^FH\^FD{11}^FS
            ^FT698,577^A0I,25,24^FH\^FD{12}^FS
            ^FT698,614^A0I,25,24^FH\^FD{13}^FS
            ^FT300,842^A0I,25,24^FH\^FD{14}^FS
            ^FT327,880^A0I,25,24^FH\^FD{15}^FS
            ^FT698,655^A0I,25,24^FH\^FD{16}^FS
            ^FT722,948^A0I,25,24^FH\^FD{17}^FS
            ^FT722,913^A0I,25,24^FH\^FD{18}^FS
            ^FT724,874^A0I,25,24^FH\^FD{19}^FS
            ^FT727,838^A0I,25,24^FH\^FD{20}^FS
            ^FT340,646^A0I,25,24^FH\^FDLote:^FS
            ^FT340,561^A0I,25,24^FH\^FDF. PRD:^FS
            ^FT340,607^A0I,25,24^FH\^FDF. Vence:^FS
            ^FT793,496^A0I,25,24^FH\^FDCant.:^FS
            ^FT793,539^A0I,25,24^FH\^FDPres.:^FS
            ^FT793,573^A0I,25,24^FH\^FDU.M.:^FS
            ^FT793,613^A0I,25,24^FH\^FDNom.:^FS
            ^FT793,655^A0I,25,24^FH\^FDCod.#:^FS
            ^FT405,843^A0I,25,24^FH\^FDRec. Tipo:^FS
            ^FT405,880^A0I,25,24^FH\^FDRec.#:^FS
            ^FT793,948^A0I,25,24^FH\^FDCod.#:^FS
            ^BY2,3,48^FT401,777^BCI,,Y,N
            ^FD{6}^FS
            ^FT794,688^A0I,34,33^FH\^FDPRODUCTO:^FS
            ^BY3,3,152^FT787,297^BCI,,Y,N
            ^FD(01){21}^FS
            ^FO419,720^GB0,294,9^FS
            ^BY2,3,147^FT765,86^BCI,,Y,N
            ^FD{22}^FS
            ^FO0,466^GB810,0,8^FS
            ^PQ1,0,1,Y^XZ",
            pBeProductoPresentacion.Bodega, '-> 0
            pBeProductoPresentacion.Empresa, '-> 1
            pBeProductoPresentacion.Propietario_Nombre, '-> 2
            pBeProductoPresentacion.Imprimio,             '-> 3
            pBeProductoPresentacion.Observacion,'-> 4
            FormatoFechas.sFecha(pBeProductoPresentacion.Fecha_PC),'-> 5
            pBeProductoPresentacion.PC,'-> 6
            pBeProductoPresentacion.Producto_Lote, '-> 7
            FormatoFechas.sFecha(pBeProductoPresentacion.Fecha_Produccion), '-> 8
            FormatoFechas.sFecha(pBeProductoPresentacion.Producto_Vence), '->  9            
            IIf(pBeProductoPresentacion.Presentacion.EsPallet, pBeProductoPresentacion.Producto_Cantidad_Paralela, pBeProductoPresentacion.Producto_Cantidad), '-> 10
            pBeProductoPresentacion.Producto_Presentacion, '-> 11
            pBeProductoPresentacion.Producto_UM,'-> 12
            pBeProductoPresentacion.Producto_Nombre_Largo.Substring(0, IIf(pBeProductoPresentacion.Producto_Nombre_Largo.Length < 28, pBeProductoPresentacion.Producto_Nombre_Largo.Length, 28)), '-> 13
            pBeProductoPresentacion.Rec_Tipo_Albaran, '-> 14
            pBeProductoPresentacion.Rec_No, '-> 15
            pBeProductoPresentacion.Producto_Codigo, '-> 16
            pBeProductoPresentacion.Proveedor_Codigo,'-> 17
            pBeProductoPresentacion.Proveedor_Nombre.Substring(0, IIf(pBeProductoPresentacion.Proveedor_Nombre.Length < 25, pBeProductoPresentacion.Proveedor_Nombre.Length, 25)),'-> 18
            pBeProductoPresentacion.Proveedor_Tel,'-> 19
            pBeProductoPresentacion.Proveedor_Dir.Substring(0, IIf(pBeProductoPresentacion.Proveedor_Dir.Length < 25, pBeProductoPresentacion.Proveedor_Dir.Length, 25)),'-> 20
            pBeProductoPresentacion.LP, '-> 21
            vBarra '-> 22
            )

        Try

            RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Impresión de ubicaciones",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub Imprimir_Barras(ByVal PrinterName As String)

        Try

            If Not lPalletsToPrint Is Nothing Then
                For Each BarraPallet In lPalletsToPrint.Where(Function(x) x.Imprimir = True).ToList()
                    Imprimir_Etiqueta_Pallet(BarraPallet, PrinterName)
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

    Private Sub mnuImprimirBarras_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimirBarras.ItemClick

        Try

            Dim pd As PrintDialog = New PrintDialog()
            pd.PrinterSettings = New PrinterSettings()

            If DialogResult.OK = pd.ShowDialog(Me) Then
                Imprimir_Barras(pd.PrinterSettings.PrinterName)
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub dgridBarrasRec_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgridBarrasRec.CellValueChanged

        Try

            If dgridBarrasRec.Columns(e.ColumnIndex).Name = "ColImprimir" Then

                Dim IdSockRec As Integer = 0

                Dim checkCell As DataGridViewCheckBoxCell =
                        CType(dgridBarrasRec.Rows(e.RowIndex).Cells("ColImprimir"),
                        DataGridViewCheckBoxCell)

                If Not dgridBarrasRec.Rows(e.RowIndex).Cells("ColIdStockRecBarras").Value Is Nothing Then

                    IdSockRec = dgridBarrasRec.Rows(e.RowIndex).Cells("ColIdStockRecBarras").Value

                    If CType(checkCell.Value, Boolean) = False Then
                        lPalletsToPrint.Find(Function(x) x.IdStockRec = IdSockRec).Imprimir = False
                    Else
                        lPalletsToPrint.Find(Function(x) x.IdStockRec = IdSockRec).Imprimir = True
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

    Private Sub Marcar_Registros(ByVal pMarcar As Boolean)

        Try

            Dim checkCell As DataGridViewCheckBoxCell

            For Each B As DataGridViewRow In dgridBarrasRec.Rows

                Try

                    checkCell = CType(B.Cells("ColImprimir"), DataGridViewCheckBoxCell)
                    checkCell.Value = pMarcar

                Catch ex As Exception
                    XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
                End Try

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdMarcarTodos_Click(sender As Object, e As EventArgs) Handles cmdMarcarTodos.Click
        Marcar_Registros(True)
    End Sub

    Private Sub cmdDesMarcarTodos_Click(sender As Object, e As EventArgs) Handles cmdDesmarcarTodos.Click
        Marcar_Registros(False)
    End Sub

    Private Sub DgridDetalleRec_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgridDetalleRec.CellContentClick

    End Sub
End Class