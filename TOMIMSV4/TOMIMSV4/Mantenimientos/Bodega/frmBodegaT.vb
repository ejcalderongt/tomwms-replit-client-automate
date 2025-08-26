Imports DevExpress.XtraEditors
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class frmBodegaT

    Public pObjBeB As New clsBeBodega
    Private ReadOnly pObjBAB As New clsBeBodega_area
    Private DTArea As DataTable
    Private DTSector As DataTable
    Private DTTramo As DataTable
    Private DTUbiacion As DataTable

    Public pIdBodega As Integer
    Public pObj As New clsBeBodega_ubicacion

    Public Nombre_Campo As String = ""
    Public idubicacion As String = ""
    Public nombreUbicacion As String = ""
    Public Property dañado As Boolean
    Public Property utilizable As Boolean
    Public Property idIndiceRotacion As Integer

    '#GT13022024: filtro por area si tuviera una asignada
    Public Property IdAreaFiltro As Integer

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmBodegaT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Select Case Modo
                Case TipoTrans.Nuevo
                Case TipoTrans.Editar
                    LLena_Bodega()
            End Select

            Application.DoEvents()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub LLena_Bodega()

        Try

            Crea_Nodo_Bodega(tlUbicaciones)
            'Crea_Nodos_Areas(tlUbicaciones, pObjBeB.IdBodega)
            Crea_Nodos_Areas(tlUbicaciones, pObjBeB.IdBodega, IdAreaFiltro)
            tlUbicaciones.ExpandAll()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub Crea_Nodo_Bodega(ByRef tl As TreeList)

        Try

            tl.BeginUpdate()
            tl.Columns.Add()
            tl.Columns(0).Caption = pObjBeB.IdBodega
            tl.Columns(0).VisibleIndex = 0
            tl.Columns(0).FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(1).Caption = pObjBeB.Nombre
            tl.Columns(1).VisibleIndex = 1
            tl.Columns(1).FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(2).Caption = pObjBeB.Nombre
            tl.Columns(2).VisibleIndex = 1
            tl.Columns(2).Visible = False
            tl.Columns(2).FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.EndUpdate()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Crea_Nodos_Areas(ByRef tl As TreeList, IdBodega As Integer, ByVal IdAreaFiltro As Integer)

        Try

            tl.BeginUnboundLoad()

            If IdBodega <> 0 Then
                '#GT13022024: aqui filtramos por area
                'el método se llama en muchos lugares, como IMS, se queda optional IdAreaFiltro
                DTArea = clsLnBodega_area.Get_All_Areas_By_IdBodega(IdBodega, IdAreaFiltro)
            Else
                DTArea = clsLnBodega_area.Listar()
            End If

            'Obtener las areas de la bodega
            Dim parentForRootNodes As TreeListNode = Nothing
            Dim rootNode As TreeListNode

            'Çiclo
            For Each r As DataRow In DTArea.Rows
                rootNode = tl.AppendNode(New Object() {r.Item("IdArea").ToString(), r.Item("Descripcion"), ("A")}, parentForRootNodes)
                Crea_Nodos_Sectores(tlUbicaciones, r.Item("IdArea"), rootNode)
            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Nodos_Sectores(ByRef tl As TreeList, idArea As Integer, Padre As TreeListNode)

        Try

            tl.BeginUnboundLoad()
            DTSector = clsLnBodega_sector.Get_All_Sector_By_Area_And_IdBodega(idArea, pObjBeB.IdBodega)
            'Obtener los sectores por área de la bodega

            Dim rootNode As TreeListNode
            'Çiclo
            For Each r As DataRow In DTSector.Rows
                rootNode = tl.AppendNode(New Object() {r.Item("IdSector"), r.Item("Descripcion"), ("S")}, Padre)
                Crea_Nodos_Tramos(tlUbicaciones, r.Item("IdSector"), rootNode)
            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub Crea_Nodos_Tramos(ByRef tl As TreeList, IdSector As Integer, ByRef Padre As TreeListNode)

        Try

            tl.BeginUnboundLoad()
            DTTramo = clsLnBodega_tramo.Get_All_Tramos_By_Sector_And_IdBodega(IdSector, pObjBeB.IdBodega)
            'Obtener los tramos por sector de la bodega

            Dim rootNode As TreeListNode
            'Çiclo
            For Each r As DataRow In DTTramo.Rows
                rootNode = tl.AppendNode(New Object() {r.Item("IdTramo"), r.Item("Descripcion"), ("T")}, Padre)
                Crea_Nodos_Ubicaciones(tlUbicaciones, r.Item("IdTramo"), rootNode)
            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Nodos_Ubicaciones(ByRef tl As TreeList, IdTramo As Integer, ByRef Padre As TreeListNode)

        Try

            tl.BeginUnboundLoad()

            DTUbiacion = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_IdTramo_And_IdBodega(IdTramo,
                                                                                           pObjBeB.IdBodega,
                                                                                           Nombre_Campo,
                                                                                           1,
                                                                                           idIndiceRotacion)

            Dim rootNode As TreeListNode

            For Each r As DataRow In DTUbiacion.Rows
                rootNode = tl.AppendNode(New Object() {r.Item("IdUbicacion"), r.Item("Descripcion"), ("U")}, Padre)
            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tlUbicaciones_DoubleClick(sender As Object, e As EventArgs) Handles tlUbicaciones.DoubleClick

        Try

            If tlUbicaciones.FocusedNode.GetDisplayText(tlUbicaciones.Columns(2)) = "U" Then
                pObj.IdUbicacion = tlUbicaciones.FocusedNode.GetDisplayText(tlUbicaciones.Columns(0))
                pObj.Descripcion = tlUbicaciones.FocusedNode.GetDisplayText(tlUbicaciones.Columns(1))
                pObj = clsLnBodega_ubicacion.GetSingle(pObj.IdUbicacion, pObjBeB.IdBodega)
                Hide()
            Else
                XtraMessageBox.Show("El nivel seleccionado, no corresponde a una ubicación válida", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tlUbicaciones_NodeCellStyle(sender As Object, e As GetCustomNodeCellStyleEventArgs) Handles tlUbicaciones.NodeCellStyle

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

End Class