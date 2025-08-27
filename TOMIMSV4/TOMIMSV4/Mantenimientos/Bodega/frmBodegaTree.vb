Imports DevExpress.XtraEditors
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class frmBodegaTree

    Public pObjBeB As New clsBeBodega
    Private DTArea As DataTable

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

    Private Sub frmBodegaTree_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not IMS.Listar_Paises(cmbPais) Then
                XtraMessageBox.Show("No hay paises definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Select Case Modo

                Case TipoTrans.Nuevo

                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrTextEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modTextEdit.Text = Now

                    Me.tabDatos.TabPages.Remove(TabUbicacion)

                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    cmbEmpresa.Enabled = True
                    verInformacion()

                Case TipoTrans.Editar
                    verInformacion()
                    cargarBodega()
                    validarAreas()
                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = True
                    mnuEliminar.Enabled = True

            End Select

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub validarAreas()
        Try
            DsBodega.Clear()
            DTArea = clsLnBodega_area.Get_All_Areas_By_IdBodega(pObjBeB.IdBodega)
            tLArea.DataSource = DTArea
            tLArea.KeyFieldName = "IdArea"
            tLArea.ParentFieldName = "IdBodega"
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub verInformacion()
        Try
            For Each N As TreeListNode In tlPrueba.Nodes
                If N.Selected Then
                    XtraMessageBox.Show(N.Item(0), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit For
                End If
            Next
            CreateNodes(tlPrueba)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub CreateNodes(ByVal tl As TreeList)
        tl.BeginUnboundLoad()
        ' Create a root node .
        Dim parentForRootNodes As TreeListNode = Nothing
        Dim rootNode As TreeListNode = tl.AppendNode(New Object() {"Alfreds Futterkiste", "Germany, Obere Str. 57", "030-0074321"}, parentForRootNodes)
        ' Create a child node for the node1            
        tl.AppendNode(New Object() {"Suyama, Michael", "Obere Str. 55", "030-0074263"}, rootNode)
        ' Creating more nodes
        ' ...
        tl.ExpandAll()

        tl.EndUnboundLoad()
    End Sub
    'Bodega
    Private Function Guardar() As Boolean
        Guardar = False
        Try
            If cmbPais.EditValue <> 0 Then
                pObjBeB.IdPais = cmbPais.EditValue
            End If

            pObjBeB.IdBodega = clsLnBodega.MaxID()
            pObjBeB.IdEmpresa = cmbEmpresa.EditValue

            pObjBeB.Codigo = txtCodigo.Text.Trim()
            pObjBeB.Codigo_barra = txtCodigoBarra.Text.Trim()
            pObjBeB.Nombre = txtNombre.Text.Trim()
            pObjBeB.Nombre_comercial = txtNombreComercial.Text.Trim()
            pObjBeB.Direccion = DireccionTextEdit.Text.Trim()
            pObjBeB.Telefono = TelefonoTextEdit.Text.Trim()
            pObjBeB.Email = EmailTextEdit.Text.Trim()
            pObjBeB.Encargado = EncargadoTextEdit.Text.Trim()

            pObjBeB.Ubic_recepcion = txtUbicacionRecepcion.Text.Trim()
            pObjBeB.Ubic_despacho = txtUbicacionDespacho.Text.Trim()
            pObjBeB.Ubic_picking = txtUbicacionPicking.Text.Trim()
            pObjBeB.Ubic_merma = txtUbicacionMerma.Text.Trim()

            pObjBeB.Largo = txtLargo.Text
            pObjBeB.Alto = txtAlto.Text
            pObjBeB.Ancho = txtAncho.Text

            pObjBeB.Coordenada_x = txtCoordenadaX.Text
            pObjBeB.Coordenada_y = txtCoordenadaY.Text

            pObjBeB.User_agr = AP.UsuarioAp.IdUsuario
            pObjBeB.Fec_agr = Now
            pObjBeB.User_mod = AP.UsuarioAp.IdUsuario
            pObjBeB.Fec_mod = Now

            pObjBeB.Activo = True

            Guardar = clsLnBodega.Insertar(pObjBeB) > 0

            'pObjBAB.IdBodega = pObjBeB.IdBodega


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function
    Private Sub cargarBodega()
        Try
            clsLnBodega.Obtener(pObjBeB)

            If pObjBeB.IdPais <> 0 Then
                cmbPais.EditValue = pObjBeB.IdPais
            End If

            cmbEmpresa.EditValue = pObjBeB.IdEmpresa
            cmbEmpresa.Enabled = False

            txtCodigo.Text = pObjBeB.Codigo
            txtCodigoBarra.Text = pObjBeB.Codigo_barra
            txtNombre.Text = pObjBeB.Nombre
            txtNombreComercial.Text = pObjBeB.Nombre_comercial
            DireccionTextEdit.Text = pObjBeB.Direccion
            TelefonoTextEdit.Text = pObjBeB.Telefono
            EmailTextEdit.Text = pObjBeB.Email
            EncargadoTextEdit.Text = pObjBeB.Encargado

            txtAlto.Text = pObjBeB.Alto
            txtLargo.Text = pObjBeB.Largo
            txtAncho.Text = pObjBeB.Ancho

            txtUbicacionDespacho.Text = pObjBeB.Ubic_despacho
            txtUbicacionMerma.Text = pObjBeB.Ubic_merma
            txtUbicacionPicking.Text = pObjBeB.Ubic_picking
            txtUbicacionRecepcion.Text = pObjBeB.Ubic_recepcion

            txtCoordenadaX.Text = pObjBeB.Coordenada_x
            txtCoordenadaY.Text = pObjBeB.Coordenada_y

            'Bitácora
            User_agrTextEdit.Text = pObjBeB.User_agr
            Fec_agrTextEdit.Text = pObjBeB.Fec_agr
            User_modTextEdit.Text = pObjBeB.User_mod
            Fec_modTextEdit.Text = pObjBeB.Fec_mod

            chkActivo.Checked = pObjBeB.Activo

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

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
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function
    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False

        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar Bodega?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If MessageBox.Show("¿Desea asignar área?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        tabDatos.TabPages.Add(TabUbicacion)
                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = True
                        mnuEliminar.Enabled = True

                    End If

                Else

                    Close()

                End If

            End If

        End If

        mnuGuardar.Enabled = True

    End Sub


    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

    End Sub

    Private Sub tlPrueba_DoubleClick(sender As Object, e As EventArgs) Handles tlPrueba.DoubleClick
        'Dim en As IEnumerator = tLArea.Nodes.GetEnumerator()
        'Dim ColumnDep As TreeListColumn = tLArea.Columns("IdUbicacion")
        'Dim ColumnBudget As TreeListColumn = tLArea.Columns("Descripcion")
        'While en.MoveNext() = True
        '    Dim ChildNode As TreeListNode = CType(en.Current, TreeListNode)
        '    ChildNode.GetDisplayText(ColumnDep)
        'End While

        'MsgBox(tlPrueba.Nodes.FirstNode.Item(0))
        'verInformacion()

        XtraMessageBox.Show(tlPrueba.FocusedNode.GetDisplayText(tlPrueba.Columns(0)), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub tlPrueba_FocusedNodeChanged(sender As Object, e As FocusedNodeChangedEventArgs) Handles tlPrueba.FocusedNodeChanged
        'MsgBox("se selecciono el nodo: " + e.Node.Item(0), MsgBoxStyle.Information, Text)
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

    End Sub
End Class
