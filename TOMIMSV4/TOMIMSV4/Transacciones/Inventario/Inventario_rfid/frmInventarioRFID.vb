Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports TOMWMS.wsTOMHH

Public Class frmInventarioRFID

    Public gBeTransInvEnc As New clsBeTrans_inv_enc
    Dim gBetransInvCiclico As New clsBeTrans_inv_ciclico
    Dim BeTareaHH As clsBeTarea_hh
    Dim pIdPropietario As Integer = 0

    '#Variables que ajustar para RFID
    Private ListInventarioCiclico As New List(Of clsBeTrans_inv_ciclico_rfid)
    Public Shared DTInventarioCiclico As New DataTable("InventarioCiclico")


    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    'Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmInventarioRFID_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            If Not AP.Listar_Bodegas_By_Usuario(cmbBodega) Then
                XtraMessageBox.Show("No hay bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            If Not IMS.Listar_TipoInventario(cmbTipoInventario) Then
                XtraMessageBox.Show("No hay tipos de inventarios definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            If Not IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue) Then
                XtraMessageBox.Show("No hay propietarios definidos para la bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_Operadores_By_Rol_Inventario(cmbOperadorProd) Then
                XtraMessageBox.Show("No hay Operadores definidos para toma de inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            cargar_forma()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub


    Public Property IsLoading As Boolean = False

    Private Sub cargar_forma()

        Dim clsTrans As New clsTransaccion

        Try

            clsTrans.Begin_Transaction()


            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando inventario..")

            IsLoading = True
            CheckForIllegalCrossThreadCalls = False
            SetDatataTableCiclico()
            Crea_TreeList_AsignaProductos(dgridAsignacionProductos)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCod.Text = clsLnTrans_inv_enc.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                    Fecha.EditValue = Now
                    dtpHoraInicio.EditValue = Now
                    dtpHoraFin.EditValue = Now
                    Estado.Text = "Nuevo"
                    Fecha.DateTime = Now
                    gBeTransInvEnc = New clsBeTrans_inv_enc With {.IsNew = True}

                    xtraTabInv.TabPages.Remove(xtraTabProductos)
                    xtraTabInv.TabPages.Remove(xtraTabConteo)

                Case TipoTrans.Editar

                    lblCod.Text = gBeTransInvEnc.Idinventarioenc
                    Estado.Text = gBeTransInvEnc.Estado
                    cmbBodega.EditValue = gBeTransInvEnc.IdBodega

                    cmbPropietario.EditValue = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(gBeTransInvEnc.Idpropietario,
                                                                                                                 gBeTransInvEnc.IdBodega,
                                                                                                                 clsTrans.lConnection,
                                                                                                                 clsTrans.lTransaction)

                    cmbTipoInventario.EditValue = gBeTransInvEnc.IdTipoInventario
                    chkCapturarNoAsignado.Checked = gBeTransInvEnc.Capturar_No_Asignados
                    Fecha.EditValue = gBeTransInvEnc.Fecha
                    dtpHoraInicio.EditValue = gBeTransInvEnc.Hora_ini
                    dtpHoraFin.EditValue = gBeTransInvEnc.Hora_fin

                    xtraTabInv.TabPages.Add(xtraTabProductos)
                    xtraTabInv.TabPages.Add(xtraTabConteo)

                    Listar_Productos(clsTrans.lConnection, clsTrans.lTransaction)

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Listando datos del inventario...")

                    Carga_Detalle_Ciclico()


            End Select

            clsTrans.Commit_Transaction()

        Catch ex As Exception

            clsTrans.RollBack_Transaction()

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally

            SplashScreenManager.CloseForm(False)
            IsLoading = False

        End Try

    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Guardando inventario...")

            If Validacion() Then
                If Guardar() Then
                    SplashScreenManager.CloseForm(False)

                    Modo = TipoTrans.Editar

                    XtraMessageBox.Show("Se guardó el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Me.Close()

                End If

            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("No se pudo guardar el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function Guardar(Optional ByVal Es_Actualizacion As Boolean = False) As Boolean

        Guardar = False

        Try

            If gBeTransInvEnc.IsNew Then
                SplashScreenManager.Default.SetWaitFormDescription("Creando tarea de inventario...")
                Crea_Tarea_HH()

                gBeTransInvEnc.Idpropietario = pIdPropietario
                gBeTransInvEnc.IdBodega = cmbBodega.EditValue
                gBeTransInvEnc.IdTipoInventario = cmbTipoInventario.EditValue
                gBeTransInvEnc.Tipo_Conteo_Producto = cmbTipoConteo.EditValue
                gBeTransInvEnc.Fecha = Fecha.EditValue
                gBeTransInvEnc.Fecha_Ultimo_Inventario = dtpUltimoInv.EditValue
                gBeTransInvEnc.Estado = Estado.Text

                gBeTransInvEnc.Inicial = False
                gBeTransInvEnc.Doble_verificacion = chkDobleVerifica.Checked
                gBeTransInvEnc.Mostrar_Cantidad_Teorica_hh = False
                gBeTransInvEnc.Cambia_Ubicacion = False

                'gBeTransInvEnc.Capturar_no_existente = chkCaptNtExist.Checked
                gBeTransInvEnc.Activo = chkActivo.Checked
                gBeTransInvEnc.Regularizado = False
                gBeTransInvEnc.Hora_ini = dtpHoraInicio.EditValue
                gBeTransInvEnc.Hora_fin = dtpHoraFin.EditValue
                gBeTransInvEnc.User_agr = AP.UsuarioAp.Nombres
                gBeTransInvEnc.Fec_agr = Now
                gBeTransInvEnc.User_mod = AP.UsuarioAp.Nombres
                gBeTransInvEnc.Fec_mod = Now
                '#GT27042026: multipropietario aplica solo a cealsa
                gBeTransInvEnc.multi_propietario = False
                gBeTransInvEnc.IdCentroCosto = 0
                gBeTransInvEnc.Tipo_Asignacion = 0
                gBeTransInvEnc.Capturar_No_Asignados = False

                SplashScreenManager.Default.SetWaitFormDescription("Guardando transacción...")

                Guardar = clsLnTrans_inv_enc.Guardar(gBeTransInvEnc, BeTareaHH)

            Else


            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Function

    Private Function Validacion() As Boolean
        Try
            If cmbTipoInventario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione un tipo de inventario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

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

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try

            If Guardar(True) Then

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se actualizó el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                'If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke

                Close()
            Else
                XtraMessageBox.Show("No se pudo actualizar el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Try

            Dim row As DataRowView = CType(cmbPropietario.GetSelectedDataRow(), DataRowView)
            If row IsNot Nothing Then
                pIdPropietario = row("IdPropietario")
            End If
        Catch ex As Exception
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

    Private Sub Listar_Productos_Asignados(ByRef tl As TreeList, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        'Dim CountUbicacionesUnicas As Integer
        Dim CountProductosUnicos As Integer

        Try

            tl.BeginUnboundLoad()

            ListInventarioCiclico.Clear()

            tl.ClearNodes()

            'ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(gBeTransInvEnc.Idinventarioenc,
            '                                                                                                       AP.IdBodega,
            '                                                                                                       lConnection,
            '                                                                                                       lTransaction)


            ListInventarioCiclico = clsLnTrans_inv_ciclico_rfid.Get_All()

            If ListInventarioCiclico.Count > 0 Then


                Dim ProductosUnicos = ListInventarioCiclico _
                                    .GroupBy(Function(x) x.Codigo) _
                                    .Select(Function(grupo) New With {
                                    Key .Codigo = grupo.Key,
                                    Key .Count = grupo.Count()
                                    })

                'Dim UbicacionesUnicas = ListInventarioCiclico _
                '                    .GroupBy(Function(x) x.IdUbicacion) _
                '                    .Select(Function(grupo) New With {
                '                    Key .IdUbicacion = grupo.Key,
                '                    Key .Count = grupo.Count()
                '                    })

                CountProductosUnicos = ProductosUnicos.Count()
                'CountUbicacionesUnicas = UbicacionesUnicas.Count()

                Dim parentForRootNodes As TreeListNode = Nothing

                Dim rootNode As TreeListNode

                Dim Lista = From i In ListInventarioCiclico Group i By Keys = New With {
                                                                              Key i.IdProductoBodega,
                                                                              Key i.Codigo,
                                                                              Key i.Nombre,
                                                                              Key i.Codigo_Barra,
                                                                              Key i.SSCC,
                                                                              Key i.Lote,
                                                                              Key i.Fecha_Produccion,
                                                                              Key i.IdOperador} Into Group
                            Select New With {Keys.IdProductoBodega, Keys.Codigo, Keys.Nombre,
                                              Keys.Codigo_Barra, Keys.SSCC, Keys.Lote, Keys.Fecha_Produccion,
                                              Keys.IdOperador}

                For Each BeTransInvCiclico In Lista

                    rootNode = tl.AppendNode(New Object() {BeTransInvCiclico.IdProductoBodega,
                                                           BeTransInvCiclico.Codigo,
                                                           BeTransInvCiclico.Nombre,
                                                           BeTransInvCiclico.Codigo_Barra,
                                                           BeTransInvCiclico.SSCC,
                                                           BeTransInvCiclico.Lote,
                                                           BeTransInvCiclico.Fecha_Produccion,
                                                           BeTransInvCiclico.IdOperador}, parentForRootNodes)
                    rootNode.Expanded = True
                    rootNode.Tag = BeTransInvCiclico.IdProductoBodega

                Next

                '#GT18012025: mostrar conteo
                'lblRegistros.Text = Lista.Count
                'lblProductosUnicos.Text = CountProductosUnicos
                'lblUbicacionesUnicas.Text = CountUbicacionesUnicas

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

    Private Sub cmdAgregarProducto_Click(sender As Object, e As EventArgs) Handles cmdAgregarProducto.Click

        Dim Operador As New clsBeTrans_inv_operador

        Try

            Dim Productos As New frmInventarioProductosRFID
            Productos.Propietario = cmbPropietario.Text
            Productos.IdBodega = cmbBodega.EditValue
            Productos.FechasCongelacion = gBeTransInvEnc.Fec_agr
            Productos.IdPropietario = pIdPropietario
            Productos.IdInventario = gBeTransInvEnc.Idinventarioenc
            Productos.IdOperador = cmbOperadorProd.EditValue
            Productos.Es_Inventario_RFID = True
            Productos.ShowDialog()
            Productos.Dispose()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando productos...")

            'Inserta_Operadores()

            Listar_Productos()

            'pendiente copiar de inventario para mostrar como avanza la lectura desde la HH
            'Carga_Detalle_Ciclico()



            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                            'If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke
                            Close()

                        Else

                            If Not clsLnTrans_inv_enc.Tiene_Conteos(gBeTransInvEnc.Idinventarioenc) Then
                                gBeTransInvEnc.Activo = False
                                gBeTransInvEnc.Estado = "Anulado"
                                clsLnTrans_inv_enc.Actualizar(gBeTransInvEnc)
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("Se anuló el proceso de inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke
                                Close()
                            Else
                                SplashScreenManager.CloseForm(False)
                                If gBeTransInvEnc.Inicial Then
                                    XtraMessageBox.Show("El inventario tiene conteos, no se puede anular", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Else
                                    If XtraMessageBox.Show("El inventario tiene conteos.¿Seguro de anularlo", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                                        gBeTransInvEnc.Activo = False
                                        gBeTransInvEnc.Estado = "Anulado"
                                        clsLnTrans_inv_enc.Actualizar(gBeTransInvEnc)
                                        SplashScreenManager.CloseForm(False)
                                        XtraMessageBox.Show("Se anuló el proceso de inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        'If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke
                                        Close()
                                    End If
                                End If

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

    Private Sub Listar_Productos_Asignados(ByRef tl As TreeList)

        Try

            tl.BeginUnboundLoad()

            ListInventarioCiclico.Clear()

            tl.ClearNodes()

            'ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(gBeTransInvEnc.Idinventarioenc,
            '                                                                                                       AP.IdBodega)


            ListInventarioCiclico = clsLnTrans_inv_ciclico_rfid.Get_All()

            If ListInventarioCiclico.Count > 0 Then

                Dim parentForRootNodes As TreeListNode = Nothing

                Dim rootNode As TreeListNode
                Dim CountProductosUnicos As Integer

                Dim Lista = From i In ListInventarioCiclico Group i By Keys = New With {
                                                                              Key i.IdProductoBodega,
                                                                              Key i.Codigo,
                                                                              Key i.Nombre,
                                                                              Key i.Codigo_Barra,
                                                                              Key i.SSCC,
                                                                              Key i.Lote,
                                                                              Key i.Fecha_Produccion,
                                                                              Key i.IdOperador} Into Group
                            Select New With {Keys.IdProductoBodega, Keys.Codigo, Keys.Nombre,
                                              Keys.Codigo_Barra, Keys.SSCC, Keys.Lote, Keys.Fecha_Produccion,
                                              Keys.IdOperador}


                For Each BeTransInvCiclico In Lista

                    rootNode = tl.AppendNode(New Object() {BeTransInvCiclico.IdProductoBodega,
                                                           BeTransInvCiclico.Codigo,
                                                           BeTransInvCiclico.Nombre,
                                                           BeTransInvCiclico.Codigo_Barra,
                                                           BeTransInvCiclico.SSCC,
                                                           BeTransInvCiclico.Lote,
                                                           BeTransInvCiclico.Fecha_Produccion,
                                                           BeTransInvCiclico.IdOperador}, parentForRootNodes)

                    rootNode.Expanded = True
                    rootNode.Tag = BeTransInvCiclico.IdProductoBodega

                Next

                Dim ProductosUnicos = ListInventarioCiclico _
                                    .GroupBy(Function(x) x.Codigo) _
                                    .Select(Function(grupo) New With {
                                    Key .Codigo = grupo.Key,
                                    Key .Count = grupo.Count()
                                    })

                CountProductosUnicos = ProductosUnicos.Count()

            End If

            tl.EndUnboundLoad()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub Carga_Detalle_Ciclico()

        Dim CantidadUMBas As Double = 0
        Dim Cantidad_Contada_Pres As Double = 0
        Dim CantStockUM As Double = 0
        Dim Cantidad_Teorica_Stock_Pres As Double = 0
        Dim CantReUM As Double = 0
        Dim Cantidad_Reconteo_Pres As Double = 0
        Dim Cantidad_Reservada_Pres As Double = 0
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

            'ListInventarioCiclico = clsLnTrans_inv_ciclico.Get_All_BeTransInvCiclico_By_IdInventarioEnc(gBeTransInvEnc.Idinventarioenc,
            '                                                                                            AP.IdBodega,
            '                                                                                            clsTrans.lConnection,
            '                                                                                            clsTrans.lTransaction)


            ListInventarioCiclico = clsLnTrans_inv_ciclico_rfid.Get_All()

            'If pIdPropietario > 0 Then
            '    ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdPropietario = pIdPropietario)
            'End If

            'If txtIdFamilia.Text <> "" Then
            '    ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdFamilia = txtIdFamilia.Text)
            'End If

            'If txtIdClasificacion.Text <> "" Then
            '    ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdClasificacion = txtIdClasificacion.Text)
            'End If

            'If txtIdProducto.Text <> "" Then
            '    ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.Codigo = txtIdProducto.Text)
            'End If

            'If txtIdUbicacion.Text <> "" Then
            '    ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdUbicacion = txtIdUbicacion.Text)
            'End If

            'If txtIdTramo.Text <> "" Then
            '    ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdTramo = txtIdTramo.Text)
            'End If

            If txtIdOperador.Text <> "" Then
                ListInventarioCiclico = ListInventarioCiclico.FindAll(Function(x) x.IdOperador = txtIdOperador.Text)
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

                Dim BeTrans_inv_ciclico As New clsBeTrans_inv_ciclico_rfid

                For Each BeTransInvCiclico As clsBeTrans_inv_ciclico_rfid In ListInventarioCiclico

                    'Ubicacion.IdUbicacion = BeTransInvCiclico.IdUbicacion

                    'EstadoNuevo = BeTransInvCiclico.Estado

                    UbicacionNueva = ""

                    'If BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then
                    '    UbicacionNueva = BeTransInvCiclico.Ubicacion_Nueva
                    'End If

                    'If BeTransInvCiclico.EsNuevo Then

                    '    Extraviado = BeTransInvCiclico.Cantidad
                    '    BeTransInvCiclico.Cant_stock = 0
                    '    BeTransInvCiclico.Cant_reconteo = 0
                    '    BeTransInvCiclico.Peso_stock = 0
                    '    BeTransInvCiclico.Peso_reconteo = 0

                    'Else

                    '    Reconteo.Idinventarioenc = BeTransInvCiclico.Idinventarioenc
                    '    Reconteo.IdOperador = BeTransInvCiclico.IdOperador
                    '    Reconteo.IdStock = BeTransInvCiclico.IdStock
                    '    Reconteo.IdUbicacionAnterior = BeTransInvCiclico.IdUbicacion
                    '    Reconteo.IdProductoBodega = BeTransInvCiclico.IdProductoBodega

                    '    If BeTransInvCiclico.IdStock > 0 Then

                    '        If clsLnTrans_inv_reconteo.Obtener_By_Ubicacion(Reconteo, clsTrans.lConnection, clsTrans.lTransaction) Then
                    '            BeTransInvCiclico.Cant_reconteo = Reconteo.Cantidad
                    '            BeTransInvCiclico.Peso_reconteo = Reconteo.Peso
                    '        End If

                    '    End If

                    '    Extraviado = 0

                    'End If

                    CantidadUMBas = 0
                    Cantidad_Contada_Pres = 0
                    Cantidad_Teorica_Stock_Pres = 0
                    CantStockUM = 0
                    Cantidad_Reconteo_Pres = 0
                    CantReUM = 0

                    'Dim PresentacionFinal As String
                    'Dim CantTeoricaFinal As String
                    'Dim CantConteoFinal As String
                    'Dim CantReconteoFinal As String

                    DTInventarioCiclico.Rows.Add(BeTransInvCiclico.Idinvciclico,
                              BeTransInvCiclico.Idinventarioenc,
                              BeTransInvCiclico.IdPallet,
                              BeTransInvCiclico.Codigo,
                              BeTransInvCiclico.Nombre,
                              BeTransInvCiclico.Lote,
                              BeTransInvCiclico.Codigo_Barra,
                              BeTransInvCiclico.SSCC,
                              BeTransInvCiclico.GTIN,
                              BeTransInvCiclico.Fecha_Produccion,
                              BeTransInvCiclico.IdProductoBodega,
                              BeTransInvCiclico.User_agr,
                              BeTransInvCiclico.Fec_agr,
                              BeTransInvCiclico.User_mod,
                              BeTransInvCiclico.Fec_mod,
                              BeTransInvCiclico.IdOperador,
                              BeTransInvCiclico.Cantidad,
                              BeTransInvCiclico.EsPallet,
                              BeTransInvCiclico.EsReconteo,
                              BeTransInvCiclico.Cantidad_reconteo,
                              BeTransInvCiclico.Iddispositivo)

                    SplashScreenManager.Default.SetWaitFormDescription(vContador + 1 & " de: " & ListInventarioCiclico.Count)

                    prgPanInvConteo.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Next

                dgridInventarioCiclico.DataSource = DTInventarioCiclico

                If gdviewTeorico.RowCount > 0 Then

                    'gdviewTeorico.Columns("IdInventario").Visible = False
                    'gdviewTeorico.Columns("IdProductoBodega").Visible = False
                    'gdviewTeorico.Columns("IdInvCiclico").Visible = False

                    gdviewTeorico.Columns("idinvciclico").Visible = False
                    gdviewTeorico.Columns("idinventarioenc").Visible = False
                    gdviewTeorico.Columns("IdPallet").Visible = False

                    gdviewTeorico.OptionsView.ShowFooter = True

                    '#EJC20180830_0540PM: Hot fix para funcionalidad de tablet
                    'gdviewTeorico.Columns("Código").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                    'gdviewTeorico.Columns("Cant.Teorica.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'gdviewTeorico.Columns("Cant.Teorica.Pres").DisplayFormat.FormatString = "{0:n6}"

                    'gdviewTeorico.Columns("Cant.Teorica.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'gdviewTeorico.Columns("Cant.Teorica.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    'gdviewTeorico.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'gdviewTeorico.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                    'gdviewTeorico.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'gdviewTeorico.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                    'gdviewTeorico.Columns("Cant.Conteo.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'gdviewTeorico.Columns("Cant.Conteo.Pres").DisplayFormat.FormatString = "{0:n6}"

                    'gdviewTeorico.Columns("Cant.Conteo.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'gdviewTeorico.Columns("Cant.Conteo.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    'gdviewTeorico.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'gdviewTeorico.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                    'gdviewTeorico.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'gdviewTeorico.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                    'gdviewTeorico.Columns("Cant.Reconteo.Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'gdviewTeorico.Columns("Cant.Reconteo.Pres").DisplayFormat.FormatString = "{0:n6}"

                    'gdviewTeorico.Columns("Cant.Reconteo.Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'gdviewTeorico.Columns("Cant.Reconteo.Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    'gdviewTeorico.Columns("PesoReconteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'gdviewTeorico.Columns("PesoReconteo").DisplayFormat.FormatString = "{0:n6}"

                    'gdviewTeorico.Columns("PesoReconteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'gdviewTeorico.Columns("PesoReconteo").SummaryItem.DisplayFormat = "{0:n6}"

                    'gdviewTeorico.Columns("Extraviado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'gdviewTeorico.Columns("Extraviado").DisplayFormat.FormatString = "{0:n6}"

                    'gdviewTeorico.Columns("Extraviado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'gdviewTeorico.Columns("Extraviado").SummaryItem.DisplayFormat = "{0:n6}"

                    'gdviewTeorico.Columns("Dif.Cant.UMBas").Caption = "Dif.Cant.Pres"

                    'gdviewTeorico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'gdviewTeorico.Columns("Dif.Cant.UMBas").DisplayFormat.FormatString = "{0:n6}"

                    'gdviewTeorico.Columns("Dif.Cant.UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'gdviewTeorico.Columns("Dif.Cant.UMBas").SummaryItem.DisplayFormat = "{0:n6}"



                    gdviewTeorico.BestFitColumns()

                End If

            End If

            'Actualiza_KPIS()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            prgPanInvConteo.Value = 0
            prgPanInvConteo.Visible = False
        End Try

    End Sub

    Private Sub Crea_TreeList_AsignaProductos(ByRef tl As TreeList)

        Try

            tl.BeginUpdate()

            tl.Columns.Clear()

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
            tl.Columns(2).Visible = True

            tl.Columns.Add()
            tl.Columns(3).Caption = "Codigo_Barra"
            tl.Columns(3).VisibleIndex = 3
            tl.Columns(3).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(3).Width = 150
            tl.Columns(3).Visible = True

            tl.Columns.Add()
            tl.Columns(4).Caption = "SSCC"
            tl.Columns(4).VisibleIndex = 4
            tl.Columns(4).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(4).Width = 150
            tl.Columns(4).Visible = True

            tl.Columns.Add()
            tl.Columns(5).Caption = "Lote"
            tl.Columns(5).VisibleIndex = 5
            tl.Columns(5).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(5).Width = 150
            tl.Columns(5).Visible = True


            tl.Columns.Add()
            tl.Columns(6).Caption = "Fecha produccion"
            tl.Columns(6).VisibleIndex = 6
            tl.Columns(6).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(6).Width = 150
            tl.Columns(6).Visible = True

            tl.Columns.Add()
            tl.Columns(7).Caption = "IdOperador"
            tl.Columns(7).VisibleIndex = 7
            tl.Columns(7).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns(7).Width = 150
            tl.Columns(7).Visible = False

            tl.EndUpdate()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub SetDatataTableCiclico()
        DTInventarioCiclico.Columns.Clear()

        DTInventarioCiclico.Columns.Add("idinvciclico", GetType(Integer))
        DTInventarioCiclico.Columns.Add("idinventarioenc", GetType(Integer))
        DTInventarioCiclico.Columns.Add("IdPallet", GetType(Integer))
        DTInventarioCiclico.Columns.Add("Codigo", GetType(String))
        DTInventarioCiclico.Columns.Add("Nombre", GetType(String))
        DTInventarioCiclico.Columns.Add("Lote", GetType(String))
        DTInventarioCiclico.Columns.Add("Codigo_Barra", GetType(String))
        DTInventarioCiclico.Columns.Add("SSCC", GetType(String))
        DTInventarioCiclico.Columns.Add("GTIN", GetType(String))
        DTInventarioCiclico.Columns.Add("Fecha_Produccion", GetType(Date))
        DTInventarioCiclico.Columns.Add("IdProductoBodega", GetType(Integer))
        DTInventarioCiclico.Columns.Add("user_agr", GetType(String))
        DTInventarioCiclico.Columns.Add("fec_agr", GetType(Date))
        DTInventarioCiclico.Columns.Add("user_mod", GetType(String))
        DTInventarioCiclico.Columns.Add("fec_mod", GetType(Date))
        DTInventarioCiclico.Columns.Add("IdOperador", GetType(Integer))
        DTInventarioCiclico.Columns.Add("cantidad", GetType(Integer))
        DTInventarioCiclico.Columns.Add("EsPallet", GetType(Boolean))
        DTInventarioCiclico.Columns.Add("EsReconteo", GetType(Boolean))
        DTInventarioCiclico.Columns.Add("cantidad_reconteo", GetType(Integer))
        DTInventarioCiclico.Columns.Add("Iddispositivo", GetType(String))
    End Sub

End Class