Imports System.ComponentModel
Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class frmBodegaSelUbic

    Public pObjBeB As New clsBeBodega
    Public pUbicSugReq As New clsBeUbicacionSugeridaRequest
    Public lUbicacionesExcluidas As New List(Of clsBeUbicacionExcluida)
    Public pListObjMov As New List(Of clsBeTrans_movimientos) '#EJC20170913
    Public pListStockMov As New List(Of clsBeStock) '#EJC20170913
    Public pListObjDet As New List(Of clsBeTrans_ubic_hh_det) '#EJC20170919
    Public lStockMemoria As New List(Of clsBeVW_stock_res) '#EJC20170919
    Public lStockMemoriaOrigen As New List(Of clsBeVW_stock_res) '#EJC20171014
    Private Aplicar As Boolean = False

    Public pObjDet As New clsBeTrans_ubic_hh_det

    Public pDetCorrel As Integer

    '#EJC20171006_0516AM: Agregado para validar si es cambio de estado.
    Public Property EsCambioEstado As Boolean = False

    Private DTArea As DataTable
    Private DTSector As DataTable
    Private DTTramo As DataTable
    Private DTUbiacion As DataTable
    Private DTOri As New DataTable

    Public pIdBodega As Integer
    Public pBeUbicacion As New clsBeBodega_ubicacion

    Private ReadOnly lUbicSel As New List(Of clsBeUbicacionSugeridaList)
    Private selUbic As clsBeUbicacionSugeridaList
    Private pUbs As clsLnTrans_ubicsug
    Private BePresentacion As New clsBeProducto_Presentacion
    Private ptramo As String
    Private ubicTotal, ubicAplic, ubicFalta, cantUbic As Double
    Private ufilt() As String
    Private ufiltcod As Boolean
    Private ufiltubic As String
    Private ufiltcnt As Integer

    Public nombreCampo As String = ""

    Private BeProducto As New clsBeProducto
    Private BeEstadoProd As New clsBeProducto_estado

    '#GT21102024: valida que es cambio masivo
    Public SeleccionMultiple As Boolean
    Public pStockRes_SeleccionMultiple As New List(Of clsBeVW_stock_res)

    Private _ubicacionDestinoEsValidaSegunRegla As Boolean = True
    Private _mensajeUbicacionNoValida As String = ""

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Dañado As Boolean
    Public Property Utilizable As Boolean
    Public Property IdIndiceRotacion As Integer

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As pModo
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmBodegaT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Limpia_Info()
        Try
            ' ── Recargar parámetros frescos de la bodega ──
            Dim bodegaFresca = clsLnBodega.GetSingle_By_Idbodega(AP.IdBodega)
            If bodegaFresca IsNot Nothing Then
                pObjBeB = bodegaFresca
            End If
            ' ── Fin recarga ──

            If Not SeleccionMultiple Then
                ubicTotal = pUbicSugReq.Cantidad : lUbicSel.Clear()
                Calcula_Valores()
                Get_Info_Producto()
                Get_Info_Presentacion()
                Get_Info_Estado_Producto()
                Calcula_Ubicaciones_Sugeridas()
            End If
            Application.DoEvents()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Llena_Bodega()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando...")

            Crea_Bodega(tlUbicacionesTodas)
            Crea_Areas(tlUbicacionesTodas)

            SplashScreenManager.CloseForm()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_Info_Producto()

        Try

            BeProducto.IdProducto = pUbicSugReq.IdProducto
            BeProducto = clsLnProducto.Get_Single_By_IdProducto(BeProducto.IdProducto)
            BeProducto.IdProductoBodega = clsLnProducto.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto, pUbicSugReq.IdBodega)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_Info_Presentacion()

        Try

            BePresentacion.IdPresentacion = pUbicSugReq.IdPresentacion
            BePresentacion = clsLnProducto_presentacion.GetSingle(BePresentacion.IdPresentacion)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_Info_Estado_Producto()

        Try

            BeEstadoProd.IdEstado = pUbicSugReq.IdEstadoProd
            BeEstadoProd = clsLnProducto_estado.Get_Single_By_IdEstado(BeEstadoProd.IdEstado)

            If BeEstadoProd Is Nothing Then
                Throw New Exception("No se pudo obtener el estado del producto")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Calcula_Ubicaciones_Sugeridas()

        Dim lUbicFinal As New List(Of clsBeUbicacionSugeridaFinal)
        Dim sUbic As clsBeUbicacionSugeridaFinal

        lUbicSel.Clear()

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Calculando  ...")

        Try

            If pUbicSugReq.Lote <> "" Then
                lblProducto.Text = String.Format("{0}, Cant : {1}, Lote : {2}, Estado : {3}",
                                                 BeProducto.Codigo + " - " + BeProducto.Nombre,
                                                 pUbicSugReq.Cantidad,
                                                 pUbicSugReq.Lote,
                                                 BeEstadoProd.Nombre)
            Else
                lblProducto.Text = String.Format("{0} , Cant : {1}", BeProducto.Codigo + " - " + BeProducto.Nombre,
                                                 pUbicSugReq.Cantidad)
            End If

            clsLnTrans_ubicsug.pIdBodega = pUbicSugReq.IdBodega
            clsLnTrans_ubicsug.pBeProducto = BeProducto
            clsLnTrans_ubicsug.pBePresentacion = BePresentacion
            clsLnTrans_ubicsug.pBeEstado = BeEstadoProd
            clsLnTrans_ubicsug.pLote = pUbicSugReq.Lote
            clsLnTrans_ubicsug.lUbicacionesExcluidas = lUbicacionesExcluidas

            If Not clsLnTrans_ubicsug.Get_Ubicaciones_Sugeridas(pUbicSugReq.Cantidad, pUbicSugReq.IdUbicStock) Then
                SplashScreenManager.CloseForm(False) : Return
            End If

            If clsLnTrans_ubicsug.lUbicacionesSugeridas.Count = 0 Then
                SplashScreenManager.CloseForm(False) : Return
            End If

            Parallel.ForEach(clsLnTrans_ubicsug.lUbicacionesSugeridas, Sub(ByVal Ubic)
                                                                           SyncLock lUbicFinal
                                                                               sUbic = New clsBeUbicacionSugeridaFinal()
                                                                               sUbic.IdUbicacion = Ubic.IdUbicacion
                                                                               sUbic.Descripcion = Ubic.Descripcion
                                                                               sUbic.Tramo = Ubic.Tramo
                                                                               sUbic.Nivel = Ubic.Nivel
                                                                               sUbic.Cant_Ubicar = Ubic.Cant_Ubicar
                                                                               lUbicFinal.Add(sUbic)
                                                                           End SyncLock
                                                                       End Sub)


            dgridUbicacionesSugeridas.DataSource = lUbicFinal

            SplashScreenManager.CloseForm(False)

            grdDetalle_Click(Nothing, Nothing)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Bodega(ByRef tl As TreeList)

        Try

            'tl.Columns.Clear()
            tl.BeginUpdate()
            tl.Columns.Add()
            tl.Columns(0).Caption = "Bodegas: " & pObjBeB.IdBodega
            tl.Columns(0).VisibleIndex = 0
            tl.Columns(0).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(1).Caption = pObjBeB.Nombre
            tl.Columns(1).VisibleIndex = 1
            tl.Columns(1).FilterMode = ColumnFilterMode.DisplayText
            '#EJC20220419:En devexpress 26.2 ahora exige que el caption sea único-.
            tl.Columns.Add()
            tl.Columns(2).Caption = pObjBeB.Nombre & "_Def"
            tl.Columns(2).VisibleIndex = 2
            tl.Columns(2).Visible = False
            tl.Columns(2).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.EndUpdate()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        DTOri = clsLnBodega_orientacion_pos.Listar()
        txtFiltroUbic.Text = "" : txtFiltroUbic.Focus()

    End Sub

    Private Sub Crea_Areas(ByRef tl As TreeList)

        Try

            tl.BeginUnboundLoad()

            DTArea = clsLnBodega_area.Get_All_Areas_By_IdBodega(pObjBeB.IdBodega)

            'Obtener las areas de la bodega
            Dim parentForRootNodes As TreeListNode = Nothing

            Dim rootNode As TreeListNode

            'Çiclo
            For Each r As DataRow In DTArea.Rows
                rootNode = tl.AppendNode(New Object() {"Área: " & r.Item("IdArea").ToString(), r.Item("Descripcion"), ("A")}, parentForRootNodes)
                Crea_Sectores(tlUbicacionesTodas, r.Item("IdArea"), rootNode)
                rootNode.Expanded = True
            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Sectores(ByRef tl As TreeList, idArea As Integer, Padre As TreeListNode)

        Try

            tl.BeginUnboundLoad()

            DTSector = clsLnBodega_sector.Get_All_Sector_By_Area_And_IdBodega(idArea, AP.IdBodega)

            Dim rootNode As TreeListNode

            For Each r As DataRow In DTSector.Rows
                rootNode = tl.AppendNode(New Object() {"Sector: " & r.Item("IdSector").ToString(), r.Item("Descripcion"), ("A")}, Padre)
                Crea_Tramos(tlUbicacionesTodas, r.Item("IdSector"), rootNode)
                rootNode.Expanded = True
            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Tramos(ByRef tl As TreeList, idSector As Integer, ByRef Padre As TreeListNode)

        'SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        ' SplashScreenManager.Default.SetWaitFormCaption("Calculando  ...")

        Try

            tl.BeginUnboundLoad()

            DTTramo = clsLnBodega_tramo.Get_All_Tramos_By_Sector_And_IdBodega(idSector, AP.IdBodega)

            Dim rootNode As TreeListNode

            For Each r As DataRow In DTTramo.Rows
                rootNode = tl.AppendNode(New Object() {"Tramo: " & r.Item("IdTramo").ToString(), r.Item("Descripcion"), ("A")}, Padre)
                Crea_Ubicaciones(tlUbicacionesTodas, r.Item("IdTramo"), rootNode)
            Next

            tl.EndUnboundLoad()

            '  SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Ubicaciones(ByRef tl As TreeList,
                                 idTramo As Integer,
                                 ByRef Padre As TreeListNode)

        Dim dr() As DataRow
        Dim su, ori, orr As String
        Dim col, niv As String
        Dim vNombreUbicacion As String = ""

        Try

            tl.BeginUnboundLoad()

            DTUbiacion = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_IdTramo_And_IdBodega(idTramo,
                                                                                           AP.IdBodega,
                                                                                           nombreCampo,
                                                                                           1,
                                                                                           0)

            Dim rootNode As TreeListNode

            For Each r As DataRow In DTUbiacion.Rows

                col = IIf(IsDBNull(r.Item("Indice_x")), 0, r.Item("Indice_x"))
                niv = IIf(IsDBNull(r.Item("Nivel")), 0, r.Item("Nivel"))
                ori = IIf(IsDBNull(r.Item("orientacion_pos")), "", r.Item("orientacion_pos"))
                vNombreUbicacion = IIf(IsDBNull(r.Item("descripcion")), "", r.Item("descripcion"))

                Try

                    dr = DTOri.Select(String.Format("Nombre LIKE '%{0}%'", ori))
                    orr = dr(0).Item("Codigo")

                Catch ex As Exception
                    orr = ori
                End Try

                su = vNombreUbicacion 'String.Format("C[{0}] N[{1}] P[{2}]", col, niv, orr)

                rootNode = tl.AppendNode(New Object() {r.Item("IdUbicacion"), su, ("U")}, Padre)

                Application.DoEvents()

            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Buscar_Ubicacion()
        Dim ss As String

        Try

            For Each r1 As TreeListNode In tlUbicacionesTodas.Nodes
                For Each r2 As TreeListNode In r1.Nodes
                    For Each r3 As TreeListNode In r2.Nodes
                        For Each r4 As TreeListNode In r3.Nodes

                            ss = r4.GetDisplayText(tlUbicacionesTodas.Columns(0))
                            If ss = ufiltubic Then

                                tlUbicacionesTodas.CollapseAll()

                                r3.Expand()
                                r4.Selected = True

                                tlUbicacionesTodas.SetFocusedNode(r4)
                                tlUbicacionesTodas.MakeNodeVisible(r4)

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

    Private Sub Aplicar_Filtro()

        Dim nl, fcnt As Integer
        Dim nd As TreeListNode
        Dim flag As Boolean
        Dim subic As String

        Try

            nl = tlUbicacionesTodas.FocusedNode.Level

            If nl < 2 Then
                XtraMessageBox.Show("Debe definir un tramo para aplicar filtro de búsqueda", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return
            End If

            If nl = 2 Then nd = tlUbicacionesTodas.FocusedNode Else nd = tlUbicacionesTodas.FocusedNode.ParentNode

            limpiaFiltro()
            nd.Expand()

            For Each r1 As TreeListNode In nd.Nodes

                subic = r1.GetDisplayText(tlUbicacionesTodas.Columns(1))
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

            tlUbicacionesTodas.MakeNodeVisible(nd)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnFiltLimpia_Click(sender As Object, e As EventArgs) Handles btnFiltLimpia.Click
        limpiaFiltro()
        txtFiltroUbic.Text = ""
    End Sub

    Private Sub limpiaFiltro()

        tlUbicacionesTodas.BeginUnboundLoad()

        Try
            For Each r1 As TreeListNode In tlUbicacionesTodas.Nodes
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

        tlUbicacionesTodas.EndUnboundLoad()

    End Sub

    Private Sub tlUbicaciones_Click(sender As Object, e As EventArgs) Handles tlUbicacionesTodas.Click

        Try

            If tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(2)) = "U" Then

                If pBeUbicacion Is Nothing Then pBeUbicacion = New clsBeBodega_ubicacion
                pBeUbicacion.IdUbicacion = tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(0))
                pBeUbicacion.Descripcion = tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(1))
                '#CF20171113 09:58PM Erik dijo que solo filtraramos por IdUbicación pBeUbicacion = clsLnBodega_ubicacion.GetSingle(pBeUbicacion.IdUbicacion, "", Dañado, IdIndiceRotacion)
                pBeUbicacion = clsLnBodega_ubicacion.GetSingle(pBeUbicacion.IdUbicacion, AP.IdBodega)

                ptramo = tlUbicacionesTodas.FocusedNode.ParentNode.GetDisplayText(tlUbicacionesTodas.Columns(1))

                Get_Info_Ubicacion()

            End If

        Catch ex As Exception
            Limpia_Info()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tlUbicaciones_DoubleClick(sender As Object, e As EventArgs) Handles tlUbicacionesTodas.DoubleClick

        Try

            If tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(2)) = "U" Then

                'EJC20171009_0721PM: Al llegar aquí el objeto tiene valor nothing y no se le podían asignar valores.
                If pBeUbicacion Is Nothing Then pBeUbicacion = New clsBeBodega_ubicacion

                pBeUbicacion.IdUbicacion = tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(0))
                pBeUbicacion.Descripcion = tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(1))

                '#EJC20171009: Devolvía nothing por no pasar los parámetros.
                '#CF20171113 09:58PM Erik dijo que solo filtraramos por IdUbicación pBeUbicacion = clsLnBodega_ubicacion.GetSingle(pBeUbicacion.IdUbicacion, "", Dañado, IdIndiceRotacion)
                pBeUbicacion = clsLnBodega_ubicacion.GetSingle(pBeUbicacion.IdUbicacion, AP.IdBodega)

                ptramo = tlUbicacionesTodas.FocusedNode.ParentNode.GetDisplayText(tlUbicacionesTodas.Columns(1))

                cantUbic = 0

                Get_Info_Ubicacion()

                If Not ValidarUbicacionDestinoSeleccionada() Then Exit Sub
                If SeleccionMultiple Then
                    '#GT21102024: guardar la ubicación seleccionada
                    Agregar_A_Lista()
                    '#GT21102024: iterar la lista de stock y crear lista ubic_hh_det
                    'Agrega_Registros_Detalle()
                    Agrega_Registros_Detalle_Multiple()
                Else
                    Set_Cantidad_Ubicacion()
                End If

            Else
                XtraMessageBox.Show("El nivel seleccionado no es una ubicación válida",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub grdDetalle_Click(sender As Object, e As EventArgs) Handles dgridUbicacionesSugeridas.Click

        Dim dr As clsBeUbicacionSugeridaFinal

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Calculando  ...")

        Try

            dr = GridViewDet.GetFocusedRow()

            If Not dr Is Nothing Then

                pBeUbicacion = clsLnBodega_ubicacion.GetSingle(dr.IdUbicacion, AP.IdBodega)

                ptramo = dr.Tramo

                cantUbic = dr.Cant_Ubicar

                Get_Info_Ubicacion()

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            Limpia_Info()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub dgridUbicacionesSugeridas_DoubleClick(sender As Object, e As EventArgs) Handles dgridUbicacionesSugeridas.DoubleClick

        Dim dr As clsBeUbicacionSugeridaFinal
        Dim vmax As Integer

        Try

            dr = GridViewDet.GetFocusedRow()

            If Not dr Is Nothing Then

                pBeUbicacion = clsLnBodega_ubicacion.GetSingle(dr.IdUbicacion, AP.IdBodega)

                ptramo = dr.Tramo

                cantUbic = dr.Cant_Ubicar

                Get_Info_Ubicacion()

                If Not ValidarUbicacionDestinoSeleccionada() Then Exit Sub

                vmax = ubicFalta

                If selUbic.Maximo < vmax Then vmax = selUbic.Maximo

                selUbic.Ubicar = vmax

                Dim BeEstadoDestino As New clsBeProducto_estado() With {.IdEstado = pObjDet.IdEstadoDestino}
                clsLnProducto_estado.GetSingle(BeEstadoDestino)

                '#EJC20171024_0610PM: Validación para ubicación que no es para producto dañado y el producto se encuentra en estado dañado.
                If Dañado AndAlso Not pBeUbicacion.Dañado AndAlso Not (EsCambioEstado AndAlso Not BeEstadoDestino.Dañado) Then

                    If XtraMessageBox.Show("¿El producto se encuentra dañado y la ubicación seleccionada no está configurada como ubicación para producto dañado, colocar en ésta ubicación de todas formas?",
                                            Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        Exit Sub

                    End If

                End If

                'If (IdIndiceRotacion <> 0 AndAlso pBeUbicacion.IdIndiceRotacion <> 0) AndAlso (IdIndiceRotacion <> pBeUbicacion.IdIndiceRotacion) Then

                '    '#EJC20180126: Validación del índice de rotación Producto Vrs. Ubicación.
                '    If XtraMessageBox.Show(String.Format("¿El índice de rotación de la ubicación {0} 
                '    no coincide con el índice de rotación del producto {1}, 
                '    colocar en ésta ubicación de todas formas?", pBeUbicacion.IdIndiceRotacion, IdIndiceRotacion),
                '        Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                '        Exit Sub

                '    End If

                'End If

                If vmax = 0 Then

                    Set_Cantidad_Ubicacion()

                Else

                    Agregar_A_Lista()

                End If

            End If

        Catch ex As Exception
            Limpia_Info()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_Info_Ubicacion()

        Dim dvol, vol, pvol As Double
        Dim dispcant As Integer

        Try

            lblUbicacionTitulo.Text = "Ubicacion : " & pBeUbicacion.NombreCompleto
            lblUbicacion.Text = pBeUbicacion.IdUbicacion
            lblDisponibleAlmacenaje.Text = "" : lblVolumenDisponible.Text = ""

            lblNivel.Text = pBeUbicacion.Nivel : lblAncho.Text = FormatNumber(pBeUbicacion.Ancho, 2)
            lblLargo.Text = FormatNumber(pBeUbicacion.Largo, 2) : lblAlto.Text = FormatNumber(pBeUbicacion.Alto, 2)

            vol = pBeUbicacion.Ancho * pBeUbicacion.Largo * pBeUbicacion.Alto

            lblVolumen.Text = FormatNumber(vol, 2)
            lblDisponible.Text = FormatNumber(0, 2)

            Dim vVolUbic As Double = clsLnBodega_ubicacion.GetVolumenUbicacionByIdUbicacion(pBeUbicacion.IdUbicacion, pBeUbicacion.IdBodega)

            lStockMemoria.Clear()

            'Listado de registros que están en la misma ubicación
            Dim lIdStocksMemoriaPorUbicacion = (From P In pListObjDet Where P.IdUbicacionDestino = pBeUbicacion.IdUbicacion Select P).ToList

            Dim BeStockMemoria As New clsBeVW_stock_res

            If Not lIdStocksMemoriaPorUbicacion Is Nothing Then

                Parallel.ForEach(lIdStocksMemoriaPorUbicacion, Sub(ByVal Det)
                                                                   SyncLock lStockMemoria
                                                                       vVolUbic += clsLnBodega_ubicacion.GetVolumenUbicacionByIdStock(Det.IdStock)
                                                                       BeStockMemoria = New clsBeVW_stock_res()
                                                                       BeStockMemoria.IdStock = Det.IdStock
                                                                       clsLnVW_stock_res.GetSingle(BeStockMemoria)
                                                                       BeStockMemoria.CantidadReservadaUMBas = Det.Cantidad
                                                                       lStockMemoria.Add(BeStockMemoria)
                                                                   End SyncLock
                                                               End Sub)

            End If

            '#EJC20171014_0903PM:
            'Listado de registros que fueron agregados al listado de cambio de ubicación_.
            'Reflejar en la ubicación origen, la cantidad menos lo ubicado.
            Dim lIdStocksMemoriaPorUbicacionOrigen = (From P In pListObjDet Where P.IdUbicacionOrigen = pObjDet.IdUbicacionOrigen Select P).ToList

            If Not lIdStocksMemoriaPorUbicacionOrigen Is Nothing Then

                Parallel.ForEach(lIdStocksMemoriaPorUbicacionOrigen, Sub(ByVal Det)
                                                                         SyncLock lStockMemoriaOrigen
                                                                             vVolUbic += clsLnBodega_ubicacion.GetVolumenUbicacionByIdStock(Det.IdStock)
                                                                             BeStockMemoria = New clsBeVW_stock_res()
                                                                             BeStockMemoria.IdStock = Det.IdStock
                                                                             clsLnVW_stock_res.GetSingle(BeStockMemoria)
                                                                             BeStockMemoria.CantidadReservadaUMBas = Det.Cantidad
                                                                             lStockMemoriaOrigen.Add(BeStockMemoria)
                                                                         End SyncLock
                                                                     End Sub)

            End If

            dvol = vol - vVolUbic

            lblDisponible.Text = FormatNumber(dvol, 2)

            If vol = 0 Then pvol = 0 Else pvol = 100 * dvol / vol
            lblVolumenDisponible.Text = FormatNumber(pvol, 0) & " %"

            If Not BePresentacion Is Nothing Then

                If BePresentacion.Volumen <> 0 Then
                    dispcant = Math.Truncate(dvol / BePresentacion.Volumen)
                Else
                    dispcant = 0 'No se cuantos caben en la ubicación.
                End If
            End If


            lblDisponibleAlmacenaje.Text = dispcant

            chkAceptaPallet.Checked = pBeUbicacion.Acepta_pallet
            chkDañado.Checked = pBeUbicacion.Dañado
            chkBloqueado.Checked = pBeUbicacion.Bloqueada : chkActivo.Checked = pBeUbicacion.Activo

            selUbic = New clsBeUbicacionSugeridaList() With {.IdUbicacionDestino = pBeUbicacion.IdUbicacion,
                .Descripcion = pBeUbicacion.Descripcion,
                .Tramo = ptramo,
                .Nivel = pBeUbicacion.Nivel}

            If cantUbic <> 0 Then
                selUbic.Ubicar = cantUbic
            Else
                selUbic.Ubicar = dispcant
            End If

            selUbic.IdUbicacionOrigen = pObjDet.IdUbicacionOrigen

            selUbic.Maximo = dispcant

            Listar_Stock_Ubicacion()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Stock_Ubicacion()

        Dim lStockFull As New List(Of clsBeVW_stock_res)
        Dim lStock As New List(Of clsBeUbicacionSugeridaStock)
        Dim istock As clsBeUbicacionSugeridaStock

        Try

            lStockFull = clsLnStock.Get_All_By_IdUbicacion(pBeUbicacion.IdUbicacion, pBeUbicacion.IdBodega)


            Parallel.ForEach(lStockFull, Sub(ByVal fstock As clsBeVW_stock_res)
                                             SyncLock lStock
                                                 istock = New clsBeUbicacionSugeridaStock

                                                 istock.IdStock = fstock.IdStock
                                                 istock.Codigo = fstock.Codigo_Producto
                                                 istock.Nombre = fstock.Nombre_Producto
                                                 istock.CantidadUMBas = fstock.CantidadUmBas
                                                 istock.UMBas = fstock.UMBas
                                                 istock.Presentacion = fstock.Nombre_Presentacion
                                                 istock.CantidadPresentacion = fstock.CantidadPresentacion
                                                 istock.Lote = fstock.Lote
                                                 istock.FechaVence = fstock.Fecha_Vence
                                                 istock.Serial = fstock.Serial
                                                 lStock.Add(istock)
                                             End SyncLock
                                         End Sub)

            If lStockMemoriaOrigen.Count > 0 Then

                Dim vCantidadUbicada As Double = 0
                Dim lLStockMemoriaOrigen As New clsBeVW_stock_res

                Parallel.ForEach(lStock, Sub(ByVal S)
                                             lLStockMemoriaOrigen = lStockMemoriaOrigen.Where(Function(x) x.IdStock = S.IdStock).FirstOrDefault()
                                             If Not lLStockMemoriaOrigen Is Nothing Then
                                                 vCantidadUbicada = lLStockMemoriaOrigen.CantidadReservadaUMBas
                                                 S.CantidadUMBas -= vCantidadUbicada
                                             End If
                                         End Sub)

            End If

            Parallel.ForEach(lStockMemoria, Sub(ByVal fstock)
                                                SyncLock lStock
                                                    istock = New clsBeUbicacionSugeridaStock()
                                                    istock.Codigo = fstock.Codigo_Producto
                                                    istock.Nombre = fstock.Nombre_Producto
                                                    istock.CantidadUMBas = fstock.CantidadUmBas
                                                    istock.UMBas = fstock.UMBas
                                                    istock.Presentacion = fstock.Nombre_Presentacion
                                                    istock.CantidadPresentacion = fstock.CantidadPresentacion
                                                    istock.Lote = fstock.Lote
                                                    istock.FechaVence = fstock.Fecha_Vence
                                                    istock.Serial = fstock.Serial
                                                    lStock.Add(istock)
                                                End SyncLock
                                            End Sub)

            dgridStockUbic.DataSource = lStock

            '#EJC20171014_0751PM: Totales en footer del grid.

            Try

                GridView1.Columns("Codigo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                GridView1.Columns("Codigo").SummaryItem.DisplayFormat = "Count = {0}"

                GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "Sum = {0:n2}"

                GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("CantidadPresentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "Sum = {0:n2}"

            Catch ex As Exception

            End Try

            GridView1.BestFitColumns()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Limpia_Info()

        lblProducto.Text = ""
        lblUbicacionTitulo.Text = "" : lblUbicacion.Text = ""
        lblDisponibleAlmacenaje.Text = "" : lblVolumenDisponible.Text = ""
        lblNivel.Text = "" : lblAncho.Text = "" : lblLargo.Text = ""
        lblAlto.Text = "" : lblVolumen.Text = "" : lblDisponible.Text = ""

        chkAceptaPallet.Checked = False : chkDañado.Checked = False
        chkBloqueado.Checked = False : chkActivo.Checked = False

        Try
            dgridStockUbic.DataSource = Nothing
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Function ValidarUbicacionDestinoSeleccionada() As Boolean

        Try
            If pBeUbicacion Is Nothing Then Return False
            If pObjBeB Is Nothing Then Return True
            If pObjBeB Is Nothing Then Return True

            If Not ValidarUbicacionOrigenDestinoDiferente() Then Return False
            If Not EvaluarUbicacionValidaSegunRegla() Then Return False
            If Not ValidarCambioUbicacionRestrictivo() Then Return False
            If Not ValidarIndiceRotacionDestino() Then Return False
            If Not ValidarMismoProductoPosicion() Then Return False

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function

    Private Function ValidarUbicacionOrigenDestinoDiferente() As Boolean

        Try
            If pObjDet Is Nothing Then Return True
            If pBeUbicacion Is Nothing Then Return True

            If pObjDet.IdUbicacionOrigen = pBeUbicacion.IdUbicacion Then
                XtraMessageBox.Show("No se permite mover a la misma ubicación de origen.",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw New Exception("Error validando ubicación origen/destino: " & ex.Message)
        End Try

    End Function



    Private Function ValidarCambioUbicacionRestrictivo() As Boolean

        Try
            If pObjBeB Is Nothing Then Return True
            If _ubicacionDestinoEsValidaSegunRegla Then Return True

            If pObjBeB.cambio_ubicacion_restrictivo Then
                XtraMessageBox.Show(_mensajeUbicacionNoValida,
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
                Return False
            Else

                Return True
            End If

        Catch ex As Exception
            Throw New Exception("Error validando cambio de ubicación restrictivo: " & ex.Message)
        End Try

    End Function
    Private Function EvaluarUbicacionValidaSegunRegla() As Boolean

        Try
            _ubicacionDestinoEsValidaSegunRegla = True
            _mensajeUbicacionNoValida = ""

            If pObjBeB Is Nothing Then Return True
            If BeProducto Is Nothing Then Return True
            If pBeUbicacion Is Nothing Then Return False
            If BeEstadoProd Is Nothing Then Return True

            Dim dtReglas As DataTable = clsLnRegla_ubic_enc.Listar(AP.IdBodega, AP.IdEmpresa, True)

            If dtReglas Is Nothing OrElse dtReglas.Rows.Count = 0 Then
                Return True
            End If

            Dim existeReglaCompatible As Boolean = False

            For Each dr As DataRow In dtReglas.Rows

                Dim regla As New clsBeRegla_ubic_enc()
                regla.IdReglaUbicacionEnc = CInt(dr("Código"))
                clsLnRegla_ubic_enc.GetSingleWithDetails(regla)

                Dim cumple As Boolean = True

                ' Validar índice de rotación
                If regla.listDetRegla_Ubic_Det_Ir IsNot Nothing AndAlso regla.listDetRegla_Ubic_Det_Ir.Count > 0 Then
                    If IdIndiceRotacion = 0 Then
                        cumple = False
                    Else
                        cumple = cumple AndAlso regla.listDetRegla_Ubic_Det_Ir.
                        Any(Function(x) x.Activo AndAlso x.IdIndiceRotacion = IdIndiceRotacion)
                    End If
                End If

                ' Validar tipo de rotación
                If regla.listDetRegla_Ubic_Det_Tr IsNot Nothing AndAlso regla.listDetRegla_Ubic_Det_Tr.Count > 0 Then
                    If pBeUbicacion.IdTipoRotacion = 0 Then
                        cumple = False
                    Else
                        cumple = cumple AndAlso regla.listDetRegla_Ubic_Det_Tr.
                        Any(Function(x) x.Activo AndAlso x.IdTipoRotacion = pBeUbicacion.IdTipoRotacion)
                    End If
                End If

                ' Validar estado
                If regla.listDetRegla_Ubic_Det_Pe IsNot Nothing AndAlso regla.listDetRegla_Ubic_Det_Pe.Count > 0 Then
                    If BeEstadoProd Is Nothing OrElse BeEstadoProd.IdEstado = 0 Then
                        cumple = False
                    Else
                        cumple = cumple AndAlso regla.listDetRegla_Ubic_Det_Pe.
                        Any(Function(x) x.Activo AndAlso x.IdEstado = BeEstadoProd.IdEstado)
                    End If
                End If

                If cumple Then
                    existeReglaCompatible = True
                    Exit For
                End If
            Next

            If Not existeReglaCompatible Then
                _ubicacionDestinoEsValidaSegunRegla = False
                _mensajeUbicacionNoValida = "Ubicación no válida para ese producto."
            End If

            Return True

        Catch ex As Exception
            Throw New Exception("Error evaluando reglas de ubicación: " & ex.Message)
        End Try

    End Function


    Private Function ValidarMismoProductoPosicion() As Boolean
        Try
            If pObjBeB Is Nothing Then Return True
            If Not pObjBeB.requerir_mismo_producto_posiciones Then Return True
            If pBeUbicacion Is Nothing Then Return True

            ' ── Determinar IdProducto según modo ──
            Dim idProductoAUbicar As Integer = 0

            If SeleccionMultiple Then
                If pStockRes_SeleccionMultiple Is Nothing OrElse
               pStockRes_SeleccionMultiple.Count = 0 Then Return True
                Dim primerIdProducto As Integer = pStockRes_SeleccionMultiple.First().IdProducto
                If pStockRes_SeleccionMultiple.Any(Function(s) s.IdProducto <> primerIdProducto) Then
                    XtraMessageBox.Show(
                    "La selección contiene productos diferentes. " &
                    "Solo se permite mover el mismo producto en una operación.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
                idProductoAUbicar = primerIdProducto
            Else
                If BeProducto Is Nothing Then Return True
                If BeProducto.IdProducto = 0 Then Return True
                idProductoAUbicar = BeProducto.IdProducto
            End If

            ' ── PASO ①: stock en BD ──
            Dim stockDestino As List(Of clsBeVW_stock_res) =
            clsLnStock.Get_All_By_IdUbicacion(pBeUbicacion.IdUbicacion, pBeUbicacion.IdBodega)

            If stockDestino IsNot Nothing AndAlso stockDestino.Count > 0 Then
                If stockDestino.Any(Function(s) s IsNot Nothing AndAlso
                                            s.IdProducto > 0 AndAlso
                                            s.IdProducto <> idProductoAUbicar) Then
                    XtraMessageBox.Show(
                    "La ubicación destino ya contiene un producto diferente. " &
                    "Solo se permite ubicar el mismo producto en esa posición.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End If

            ' ── PASO ①B: stock en memoria (aún no guardado) ──
            ' En modo normal usamos pObjDet.IdStock para identificar el stock actual
            ' y BeProducto.IdProducto para comparar contra lo que ya está en memoria
            If pListObjDet IsNot Nothing AndAlso pListObjDet.Count > 0 Then
                Dim hayDistintoEnMemoria = pListObjDet.Any(
                Function(d)
                    ' Solo revisar dets que van a la misma ubicación destino
                    ' y que NO son el mismo stock que estamos moviendo ahora
                    If d.IdUbicacionDestino <> pBeUbicacion.IdUbicacion Then Return False
                    If d.IdStock = pObjDet.IdStock Then Return False
                    ' Comparar por IdProducto del stock en memoria
                    Dim stockEnMem = pStockRes_SeleccionMultiple.
                        FirstOrDefault(Function(s) s.IdStock = d.IdStock)
                    If stockEnMem IsNot Nothing Then
                        Return stockEnMem.IdProducto <> idProductoAUbicar
                    End If
                    ' Si no está en pStockRes, buscar en lStockMemoria
                    Dim stockMem2 = lStockMemoria.
                        FirstOrDefault(Function(s) s.IdStock = d.IdStock)
                    If stockMem2 IsNot Nothing Then
                        Return stockMem2.IdProducto <> idProductoAUbicar
                    End If
                    Return False
                End Function)

                If hayDistintoEnMemoria Then
                    XtraMessageBox.Show(
                    "La ubicación destino ya tiene asignado un producto diferente " &
                    "en esta operación. Solo se permite el mismo producto en esa posición.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End If

            ' ── PASO ②: ubicaciones relacionadas en BD ──
            Dim ubicacionesRelacionadas As List(Of clsBeBodega_ubicacion) =
            clsLnBodega_ubicacion.Get_Ubicaciones_Misma_Posicion(
                pBeUbicacion.IdBodega,
                pBeUbicacion.IdTramo,
                pBeUbicacion.Indice_x,
                pBeUbicacion.Nivel,
                pBeUbicacion.IdUbicacion)

            If ubicacionesRelacionadas Is Nothing OrElse ubicacionesRelacionadas.Count = 0 Then
                Return True
            End If

            Dim productosEncontrados As New List(Of Integer)

            For Each ubicRelacionada In ubicacionesRelacionadas
                Dim stockEnUbicacion As List(Of clsBeVW_stock_res) =
                clsLnStock.Get_All_By_IdUbicacion(ubicRelacionada.IdUbicacion, ubicRelacionada.IdBodega)

                If stockEnUbicacion Is Nothing OrElse stockEnUbicacion.Count = 0 Then Continue For

                For Each stock In stockEnUbicacion
                    If stock Is Nothing Then Continue For
                    If stock.IdProducto <= 0 Then Continue For
                    If Not productosEncontrados.Contains(stock.IdProducto) Then
                        productosEncontrados.Add(stock.IdProducto)
                    End If
                Next
            Next

            If productosEncontrados.Count = 0 Then Return True

            If productosEncontrados.Any(Function(idProd) idProd <> idProductoAUbicar) Then
                XtraMessageBox.Show(
                "La posición ya contiene un producto diferente. " &
                "Solo se permite ubicar el mismo producto en esa posición.",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw New Exception("Error validando mismo producto por posición: " & ex.Message)
        End Try
    End Function

    Private Function ValidarIndiceRotacionDestino() As Boolean

        Try
            If pObjBeB Is Nothing Then Return True
            If pBeUbicacion Is Nothing Then Return True

            If IdIndiceRotacion = 0 Then Return True
            If pBeUbicacion.IdIndiceRotacion = 0 Then Return True

            If IdIndiceRotacion = pBeUbicacion.IdIndiceRotacion Then
                Return True
            End If

            If pBeUbicacion.IdIndiceRotacion < IdIndiceRotacion Then
                If pObjBeB.permitir_cambio_ubic_indice_menor Then
                    Return True
                Else
                    XtraMessageBox.Show(String.Format("No se permite ubicar en un índice menor. Índice producto: {0}, índice ubicación: {1}.",
                                                 IdIndiceRotacion,
                                                 pBeUbicacion.IdIndiceRotacion),
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                    Return False
                End If
            End If

            Return True

        Catch ex As Exception
            Throw New Exception("Error validando índice de rotación: " & ex.Message)
        End Try

    End Function
    Private Sub Agregar_A_Lista()

        Dim idx As Integer

        Try

            idx = lUbicSel.FindIndex(Function(x) x.IdUbicacionDestino = selUbic.IdUbicacionDestino)

            If idx <> -1 Then Return

            '#20171014_0950PM: Variable de control interno para no agregar al listado final la misma ubicación.
            selUbic.IsNew = True

            lUbicSel.Add(selUbic)

            DgridDetalleUbics.DataSource = lUbicSel

            GridUbic.BestFitColumns()

            Calcula_Valores()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdDesactivarParametro_Click(sender As Object, e As EventArgs) Handles cmdDesactivarParametro.Click

        Dim dr As clsBeUbicacionSugeridaList
        Dim idx As Integer

        Try

            dr = GridUbic.GetFocusedRow()

            idx = lUbicSel.FindIndex(Function(x) x.IdUbicacionDestino = dr.IdUbicacionDestino)

            If idx = -1 Then Return

            lUbicSel.RemoveAt(idx)
            pListObjMov.RemoveAt(idx)
            pListStockMov.RemoveAt(idx)
            pListObjDet.RemoveAt(idx)

            GridUbic.BestFitColumns()
            DgridDetalleUbics.DataSource = lUbicSel
            GridUbic.BestFitColumns()

            Calcula_Valores()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Calcula_Valores()

        Try

            Dim vval As Double

            Dim vsum As Double = 0

            Parallel.ForEach(lUbicSel, Sub(ByVal ubic)
                                           vval = ubic.Ubicar
                                           vsum += vval
                                       End Sub)

            ubicAplic = vsum
            ubicFalta = ubicTotal - ubicAplic

            lblAlmacenado.Text = FormatNumber(ubicAplic, 6)

            lblFalta.Text = FormatNumber(ubicFalta, 6)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuAplicar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAplicar.ItemClick

        Try

            mnuAplicar.Enabled = False

            If lUbicSel.Count = 0 Then
                DialogResult = DialogResult.Abort
            Else
                'Agrega_Registros_Detalle()
                Aplicar = True
                DialogResult = DialogResult.Yes
            End If

            mnuAplicar.Enabled = True

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Agrega_Registros_Detalle()

        Dim pObjUbicHHDet As clsBeTrans_ubic_hh_det

        Try

            Parallel.ForEach(lUbicSel, Sub(ByVal Ubic)
                                           If Ubic.IsNew Then
                                               pDetCorrel += 1
                                               pObjUbicHHDet = New clsBeTrans_ubic_hh_det()
                                               pObjUbicHHDet = pObjDet.Clone
                                               pObjUbicHHDet.IdTareaUbicacionDet = pDetCorrel
                                               pObjUbicHHDet.IdUbicacionOrigen = pObjDet.IdUbicacionOrigen
                                               pObjUbicHHDet.IdUbicacionDestino = Ubic.IdUbicacionDestino
                                               pObjUbicHHDet.Cantidad = Ubic.Ubicar
                                               pObjUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion()
                                               pObjUbicHHDet.UbicacionDestino.Descripcion = Ubic.Descripcion
                                               '#EJC20171025_0947AM: Si es cambio de ubicación estado origen y destino es el mismo.
                                               If Not EsCambioEstado Then
                                                   pObjUbicHHDet.IdEstadoDestino = BeEstadoProd.IdEstado
                                               Else
                                                   pObjUbicHHDet.IdEstadoDestino = pObjDet.IdEstadoDestino
                                               End If
                                               pObjUbicHHDet.IdEstadoOrigen = BeEstadoProd.IdEstado
                                               '#EJC20171023_0158PM: Se utilizaba instancia de la forma de ubicación en vez de lista. Ver -> '#EJC20171023_0356PM_REF
                                               pListObjDet.Add(pObjUbicHHDet)
                                               Dim pObjStockMov As New clsBeStock() With {.IdStockOrigen = pObjDet.IdStock, .IdStock = pObjDet.IdStock}
                                               clsLnStock.GetSingle(pObjStockMov)
                                               '#EJC20171014_1016PM: Se agregó esto porque si se cambia de ubicación, la ubicación anterior del stock, no será su úlitma ubicación (Confusio)
                                               pObjStockMov.IdUbicacion_anterior = pObjDet.IdUbicacionOrigen
                                               pObjUbicHHDet.Stock = pObjStockMov
                                               If EsCambioEstado Then
                                                   pObjStockMov.ProductoEstado.IdEstado = pObjDet.IdEstadoDestino
                                               Else
                                                   pObjStockMov.ProductoEstado.IdEstado = pObjStockMov.IdProductoEstado
                                               End If
                                               pObjStockMov.Cantidad = Ubic.Ubicar
                                               pObjStockMov.Producto = New clsBeProducto()
                                               clsLnProducto.GetSingle(clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(pObjStockMov.IdProductoBodega))
                                               If EsCambioEstado Then
                                                   pObjUbicHHDet.ProductoEstado.IdEstado = pObjDet.IdEstadoDestino
                                               Else
                                                   pObjUbicHHDet.ProductoEstado.IdEstado = pObjStockMov.IdProductoEstado
                                               End If
                                               clsLnProducto_presentacion.GetSingle(pObjUbicHHDet.ProductoPresentacion)
                                               pObjUbicHHDet.ProductoPresentacion.IdPresentacion = pObjStockMov.IdPresentacion
                                               Cargar_Movimiento(pObjUbicHHDet, pObjStockMov)
                                               pObjStockMov.IdUbicacion = Ubic.IdUbicacionDestino

                                               '#CKFK20250515 Agregué la cantidad en presentación
                                               If pObjStockMov.IdPresentacion <> 0 Then

                                                   Dim BePresentacion As New clsBeProducto_Presentacion
                                                   BePresentacion = clsLnProducto_presentacion.GetSingle(pObjStockMov.IdPresentacion)

                                                   If Not BePresentacion Is Nothing Then
                                                       If BePresentacion.Factor = 0 Then
                                                           Throw New Exception("ERR20220202_1458: El factor de la presentación es 0. esto crearía un movimiento no válido para el sistema, valide el factor de la presentación. Identificador de presentación: " & pObjStockMov.IdPresentacion)
                                                       Else
                                                           pObjStockMov.Cantidad = Math.Round(pObjStockMov.Cantidad * BePresentacion.Factor, 6)
                                                       End If
                                                   Else
                                                       Throw New Exception("ERR20220202_1458: No se encontró el objeto de presentación para el identificador: " & pObjStockMov.IdPresentacion)
                                                   End If
                                               End If

                                               pListStockMov.Add(pObjStockMov)

                                               Ubic.IsNew = False
                                           End If
                                       End Sub)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    '#GT21102024: metodo para iterar la lista de stock y asignarle la ubicación seleccionada.
    Private Sub Agrega_Registros_Detalle_Multiple()

        Dim pObjUbicHHDet As clsBeTrans_ubic_hh_det

        Try

            Dim Ubic = lUbicSel.FirstOrDefault()
            pListObjDet = New List(Of clsBeTrans_ubic_hh_det)

            For Each pStockRes In pStockRes_SeleccionMultiple

                pDetCorrel += 1

                prepararObj_Ubic_HH_Det(pStockRes)

                pObjUbicHHDet = New clsBeTrans_ubic_hh_det()
                pObjUbicHHDet = pBeTransUbicHHDet.Clone
                pObjUbicHHDet.IdTareaUbicacionDet = pDetCorrel
                pObjUbicHHDet.IdUbicacionOrigen = pBeTransUbicHHDet.IdUbicacionOrigen
                pObjUbicHHDet.IdUbicacionDestino = Ubic.IdUbicacionDestino
                'pObjUbicHHDet.Cantidad = Ubic.Ubicar
                pObjUbicHHDet.Cantidad = pStockRes.CantidadUmBas
                pObjUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion()
                pObjUbicHHDet.UbicacionDestino.Descripcion = Ubic.Descripcion

                BeEstadoProd.IdEstado = pStockRes.IdProductoEstado
                BeEstadoProd = clsLnProducto_estado.Get_Single_By_IdEstado(BeEstadoProd.IdEstado)

                If Not EsCambioEstado Then
                    pObjUbicHHDet.IdEstadoDestino = BeEstadoProd.IdEstado
                Else
                    pObjUbicHHDet.IdEstadoDestino = pBeTransUbicHHDet.IdEstadoDestino
                End If

                '#EJC20171023_0158PM: Se utilizaba instancia de la forma de ubicación en vez de lista. Ver -> '#EJC20171023_0356PM_REF
                pListObjDet.Add(pObjUbicHHDet)

                Dim pObjStockMov As New clsBeStock() With {.IdStockOrigen = pStockRes.IdStock, .IdStock = pStockRes.IdStock}
                clsLnStock.GetSingle(pObjStockMov)
                '#EJC20171014_1016PM: Se agregó esto porque si se cambia de ubicación, la ubicación anterior del stock, no será su úlitma ubicación (Confusio)
                pObjStockMov.IdUbicacion_anterior = pBeTransUbicHHDet.IdUbicacionOrigen
                pObjUbicHHDet.Stock = pObjStockMov
                If EsCambioEstado Then
                    pObjStockMov.ProductoEstado.IdEstado = pBeTransUbicHHDet.IdEstadoDestino
                Else
                    pObjStockMov.ProductoEstado.IdEstado = pObjStockMov.IdProductoEstado
                End If
                '#GT21102024: en seleccion multiple la cantidad no esta en la ubicacion.
                'pObjStockMov.Cantidad = Ubic.Ubicar
                pObjStockMov.Cantidad = pStockRes.CantidadUmBas

                pObjStockMov.Producto = New clsBeProducto()
                clsLnProducto.GetSingle(clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(pObjStockMov.IdProductoBodega))
                If EsCambioEstado Then
                    pObjUbicHHDet.ProductoEstado.IdEstado = pBeTransUbicHHDet.IdEstadoDestino
                Else
                    pObjUbicHHDet.ProductoEstado.IdEstado = pObjStockMov.IdProductoEstado
                End If
                clsLnProducto_presentacion.GetSingle(pObjUbicHHDet.ProductoPresentacion)
                pObjUbicHHDet.ProductoPresentacion.IdPresentacion = pObjStockMov.IdPresentacion
                Cargar_Movimiento(pObjUbicHHDet, pObjStockMov)
                pObjStockMov.IdUbicacion = Ubic.IdUbicacionDestino
                pListStockMov.Add(pObjStockMov)
                Ubic.IsNew = False

            Next

            Ubic.IsNew = False


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    '#GT18102024: convertir el stock en objetos de ubicacion_hh_det
    Dim pBeTransUbicHHDet As clsBeTrans_ubic_hh_det
    Private Sub prepararObj_Ubic_HH_Det(pObjStock As clsBeVW_stock_res)

        pBeTransUbicHHDet = New clsBeTrans_ubic_hh_det()

        pBeTransUbicHHDet.IdTareaUbicacionDet = 0
        pBeTransUbicHHDet.IdStock = pObjStock.IdStock
        pBeTransUbicHHDet.Producto = New clsBeProducto
        pBeTransUbicHHDet.Stock = New clsBeStock
        pBeTransUbicHHDet.ProductoEstado = New clsBeProducto_estado
        pBeTransUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
        pBeTransUbicHHDet.UnidadMedida = New clsBeUnidad_medida
        pBeTransUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
        pBeTransUbicHHDet.Producto.Nombre = pObjStock.Nombre_Producto
        pBeTransUbicHHDet.Producto.Codigo = pObjStock.Codigo_Producto
        pBeTransUbicHHDet.Stock.IdUbicacion_anterior = pObjStock.IdUbicacion
        pBeTransUbicHHDet.IdUbicacionOrigen = pObjStock.IdUbicacion
        pBeTransUbicHHDet.Stock.Fecha_vence = pObjStock.Fecha_Vence
        pBeTransUbicHHDet.Stock.Serial = pObjStock.Serial
        pBeTransUbicHHDet.IdBodega = AP.IdBodega
        pBeTransUbicHHDet.ProductoEstado.Nombre = pObjStock.NomEstado
        pBeTransUbicHHDet.Stock.Añada = pObjStock.Añada
        pBeTransUbicHHDet.Stock.Lote = pObjStock.Lote
        pBeTransUbicHHDet.Stock.Fecha_Ingreso = pObjStock.Fecha_ingreso
        pBeTransUbicHHDet.ProductoPresentacion.Nombre = pObjStock.Nombre_Presentacion
        '#GT16062022_1355: faltaban estos campos en el grid del frmCambioUbicación detalle.
        pBeTransUbicHHDet.ProductoPresentacion.IdPresentacion = pObjStock.IdPresentacion
        pBeTransUbicHHDet.ProductoPresentacion.Factor = pObjStock.Factor
        pBeTransUbicHHDet.UnidadMedida.Nombre = pObjStock.UMBas
        '#GT21102024: faltaba agregar el estado origen
        pBeTransUbicHHDet.IdEstadoOrigen = pObjStock.IdProductoEstado
        pBeTransUbicHHDet.Activo = True

    End Sub








    Private Sub txtFiltroUbic_EditValueChanged(sender As Object, e As EventArgs) Handles txtFiltroUbic.EditValueChanged
        If txtFiltroUbic.Text = "" Then
            limpiaFiltro()
            Exit Sub
        End If
    End Sub

    Private Sub Cargar_Movimiento(ByVal det As clsBeTrans_ubic_hh_det, ByVal pObjStock As clsBeStock)

        Try

            Dim mov As New clsBeTrans_movimientos() _
                With {.IdEmpresa = AP.IdEmpresa,
                .IdBodegaOrigen = AP.IdBodega,
                .IdTransaccion = det.IdTareaUbicacionEnc,
                .IdPropietarioBodega = pObjStock.IdPropietarioBodega,
                .IdProductoBodega = pObjStock.IdProductoBodega,
                .IdUbicacionOrigen = pObjStock.IdUbicacion,
                .IdUbicacionDestino = det.IdUbicacionDestino,
                .IdPresentacion = pObjStock.IdPresentacion,
                .IdEstadoOrigen = pObjStock.IdProductoEstado,
                .IdEstadoDestino = pObjStock.IdProductoEstado,
                .IdProductoTallaColor = pObjStock.IdProductoTallaColor}

            If EsCambioEstado Then
                mov.IdEstadoDestino = pObjDet.IdEstadoDestino
            End If

            '#EJC20170913 - Tomar en cuenta para cambio de estado a futuro!!!!!!!!!!!!!!
            'If Not String.IsNullOrEmpty(txtIdEstado.Text.Trim()) Then
            '    mov.IdEstadoDestino = det.IdEstadoDestino
            'Else
            '    mov.IdEstadoDestino = pObjStock.IdProductoEstado
            'End If
            mov.IdUnidadMedida = pObjStock.IdUnidadMedida
            mov.IdTipoTarea = 2 'TAREA UBI'
            mov.IdBodegaDestino = AP.IdBodega
            mov.IdRecepcion = pObjStock.IdRecepcionEnc
            mov.IdRecepcionDet = pObjStock.IdRecepcionDet
            mov.Cantidad = det.Cantidad
            mov.Serie = det.Stock.Serial
            mov.Peso = det.Stock.Peso
            mov.Lote = det.Stock.Lote
            mov.Fecha_vence = pObjStock.Fecha_vence
            mov.Fecha = pObjStock.Fecha_Ingreso
            mov.Barra_pallet = pObjStock.Lic_plate
            mov.Hora_ini = Now
            mov.Hora_fin = Now
            mov.Fecha_agr = Now
            mov.Usuario_agr = AP.IdRol
            mov.Cantidad_hist = det.Stock.Cantidad
            mov.Peso_hist = det.Stock.Peso

            If mov.IdPresentacion <> 0 Then

                Dim BePresentacion As New clsBeProducto_Presentacion
                BePresentacion = clsLnProducto_presentacion.GetSingle(mov.IdPresentacion)

                If Not BePresentacion Is Nothing Then
                    If BePresentacion.Factor = 0 Then
                        Throw New Exception("ERR20220202_1458: El factor de la presentación es 0. esto crearía un movimiento no válido para el sistema, valide el factor de la presentación. Identificador de presentación: " & mov.IdPresentacion)
                    Else
                        mov.Cantidad = Math.Round(mov.Cantidad * BePresentacion.Factor, 6)
                        mov.Cantidad_hist = Math.Round(mov.Cantidad_hist * BePresentacion.Factor, 6)
                    End If
                Else
                    Throw New Exception("ERR20220202_1458: No se encontró el objeto de presentación para el identificador: " & mov.IdPresentacion)
                End If

            End If

            pListObjMov.Add(mov)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub xtrBodegaSelUbic_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtrBodegaSelUbic.SelectedPageChanged

        Try

            If xtrBodegaSelUbic.SelectedTabPageIndex = 1 Then
                txtFiltroUbic.Focus()
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub


    Private Sub Set_Cantidad_Ubicacion()

        Try

            If Not ValidarUbicacionDestinoSeleccionada() Then Exit Sub

            '#EJC20171001_0719: Validar cuando no tiene presentación.
            If Not BePresentacion Is Nothing Then

                If BePresentacion.EsPallet AndAlso Not pBeUbicacion.Acepta_pallet Then

                    If XtraMessageBox.Show("¿La ubicación no está configurada para ubicar pallets, ubicar en ésta posición de todas formas?",
                                            Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        Exit Sub
                    End If

                End If

            End If

            Dim vmax As Double = ubicFalta

            '#EJC20170909 Agregué selUbic.Maximo <> 0  porque si no vmax = 0 y no puedo ubicar pallet.
            vmax = Math.Round(vmax, 6)
            If (selUbic.Maximo < vmax AndAlso
                selUbic.Maximo <> 0 AndAlso vmax > 0) Then vmax = selUbic.Maximo

            '#EJC20170912 -> cambie por pantalla o por panel para ingreso y validación de cantidad.
            Using vCant As New frmCantidad()

                vCant.CantidadSugeridaAUbicar = cantUbic
                vCant.CantidadMaxima = vmax
                vCant.lblCantMax.Text = "Cantidad maxima : " & vmax
                vCant.txtCantidad.Maximum = vmax

                If vCant.ShowDialog = DialogResult.OK Then

                    If vCant.txtCantidad.Value < 1 Then
                        XtraMessageBox.Show("La cantidad no puede ser negativa", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf vCant.txtCantidad.Value = 0 Then
                        XtraMessageBox.Show("La cantidad debe ser mayor que 0", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf vCant.txtCantidad.Value > vmax Then
                        XtraMessageBox.Show("Cantidad a ubicar mayor que disponible", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        selUbic.Ubicar = vCant.txtCantidad.Value
                        Agregar_A_Lista()
                        Agrega_Registros_Detalle()
                    End If

                End If

            End Using

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End Try

    End Sub

    Private Sub tlUbicaciones_NodeCellStyle(sender As Object, e As GetCustomNodeCellStyleEventArgs) Handles tlUbicacionesTodas.NodeCellStyle

        Try

            If e.Node.Level = 3 Then
                e.Appearance.BackColor = Color.LightGreen
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

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mmuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mmuActualizar.ItemClick

    End Sub

    Private Sub tlUbicacionesTodas_FocusedNodeChanged(sender As Object, e As FocusedNodeChangedEventArgs) Handles tlUbicacionesTodas.FocusedNodeChanged

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

    Private Sub frmBodegaSelUbic_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try
            Llena_Bodega()
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0}", ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmBodegaSelUbic_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        Dim idx As Integer
        Dim idxd As Integer
        Dim idxm As Integer

        Try

            If selUbic IsNot Nothing Then

                idx = lUbicSel.FindIndex(Function(x) x.IdUbicacionDestino = selUbic.IdUbicacionDestino)
                idxd = pListObjDet.FindIndex(Function(x) x.IdUbicacionDestino = selUbic.IdUbicacionDestino)
                idxm = pListObjMov.FindIndex(Function(x) x.IdUbicacionDestino = selUbic.IdUbicacionDestino)

                If idx <> -1 AndAlso idxd <> -1 AndAlso idxm <> -1 AndAlso Not Aplicar Then

                    lUbicSel.RemoveAt(idx)
                    pListObjDet.RemoveAt(idxd)
                    pListObjMov.RemoveAt(idxm)

                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


End Class

