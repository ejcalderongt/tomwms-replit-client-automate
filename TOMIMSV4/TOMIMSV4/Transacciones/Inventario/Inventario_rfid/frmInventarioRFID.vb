Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraEditors
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
    Private ListInventarioCiclico As New List(Of clsBeTrans_inv_ciclico)


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

            IsLoading = True

            CheckForIllegalCrossThreadCalls = False

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCod.Text = clsLnTrans_inv_enc.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                    Fecha.EditValue = Now
                    dtpHoraInicio.EditValue = Now
                    dtpHoraFin.EditValue = Now
                    Estado.Text = "Nuevo"
                    Fecha.DateTime = Today
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
            'dgridAsignacionProductos.Columns(1).SummaryFooterStrFormat = "Productos: {0:n0}"
            'dgridAsignacionProductos.Columns(1).SummaryFooter = SummaryItemType.Count

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class