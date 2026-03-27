Imports System.Data.SqlClient
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class frmSeleccionUbicacionesInv

    Public pObjBeB As New clsBeBodega
    Public pBeUbicacion As New clsBeBodega_ubicacion
    Public ListUbicacion As New List(Of clsBeBodega_ubicacion)
    Public Property lStockCongelado As New DataTable

    Private ptramo As String
    Public nombreCampo As String = ""
    Public Property Dañado As Boolean = False
    Public IdInventarioEnc As Integer = 0
    Public Property IdOperador As Integer = 0

    Private DTArea As DataTable
    Private DTSector As DataTable
    Private DTTramo As DataTable
    Private DTUbicacion As DataTable
    Private DTOri As New DataTable

    Private ufilt() As String
    Private ufiltcod As Boolean
    Private ufiltubic As String
    Private ufiltcnt As Integer

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

    Private Sub Crea_TreeList_Bodega(ByRef tl As TreeList)

        Try

            tl.ClearNodes()
            tl.BeginUpdate()
            tl.Columns.Add()
            tl.Columns(0).Caption = "Bodega: " & pObjBeB.IdBodega
            tl.Columns(0).VisibleIndex = 0
            tl.Columns(0).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(1).Caption = pObjBeB.Nombre
            tl.Columns(1).VisibleIndex = 1
            tl.Columns(1).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.Columns(2).Caption = pObjBeB.Nombre & pObjBeB.IdBodega
            tl.Columns(2).VisibleIndex = 1
            tl.Columns(2).Visible = False
            tl.Columns(2).FilterMode = ColumnFilterMode.DisplayText
            tl.Columns.Add()
            tl.EndUpdate()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        'DTOri.Clear()
        DTOri = clsLnBodega_orientacion_pos.Listar
        txtFiltroUbic.Text = "" : txtFiltroUbic.Focus()

    End Sub

    Private Sub Crea_Areas(ByRef tl As TreeList, ByVal pIdBodega As Integer)

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Cargando Ubicaciones...")

        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            tl.BeginUnboundLoad()

            If chkUbicexist.Checked Then
                If Not DTArea Is Nothing Then DTArea.Clear()
                DTArea = clsLnBodega_area.Get_All_Area_By_IdBodega_And_IdIventario(pIdBodega,
                                                                                   IdInventarioEnc,
                                                                                   clsTransaccion.lConnection,
                                                                                   clsTransaccion.lTransaction)
            Else
                DTArea = clsLnBodega_area.Get_All_Areas_By_IdBodega(pIdBodega,
                                                                    clsTransaccion.lConnection,
                                                                    clsTransaccion.lTransaction)
            End If

            'Obtener las areas de la bodega
            Dim parentForRootNodes As TreeListNode = Nothing

            Dim rootNode As TreeListNode

            Dim vIdArea As Integer = 0

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            'Çiclo
            For Each r As DataRow In DTArea.Rows

                vIdArea = r.Item("IdArea")

                SplashScreenManager.Default.SetWaitFormDescription("Cargando [Área]: " & vIdArea)

                rootNode = tl.AppendNode(New Object() {"Área: " & r.Item("IdArea").ToString(), r.Item("Descripcion"), ("A")}, parentForRootNodes)

                Crea_Sectores(tlUbicacionesTodas,
                              vIdArea,
                              pIdBodega,
                              rootNode,
                              clsTransaccion.lConnection,
                              clsTransaccion.lTransaction)

                rootNode.Expanded = True

            Next

            tl.EndUnboundLoad()

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            clsTransaccion.Close_Conection()
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Crea_Sectores(ByRef tl As TreeList,
                              ByVal pIdArea As Integer,
                              ByVal pIdBodega As Integer,
                              ByVal Padre As TreeListNode,
                              ByVal lConnection As SqlConnection,
                              ByVal lTransaction As SqlTransaction)

        Try

            tl.BeginUnboundLoad()

            If chkUbicexist.Checked Then
                If Not DTSector Is Nothing Then DTSector.Clear()
                DTSector = clsLnBodega_sector.Get_All_Sector_By_IdArea_And_IdInventario(pIdArea,
                                                                                        IdInventarioEnc,
                                                                                        pIdBodega,
                                                                                        lConnection,
                                                                                        lTransaction)
            Else
                DTSector = clsLnBodega_sector.Get_All_Sector_By_Area_And_IdBodega(pIdArea,
                                                                                  AP.IdBodega,
                                                                                  lConnection,
                                                                                  lTransaction)
            End If

            Dim rootNode As TreeListNode
            Dim vIdSector As Integer = 0

            SplashScreenManager.Default.SetWaitFormDescription("Cargando [Sector]: " & DTSector.Rows.Count)

            For Each r As DataRow In DTSector.Rows

                vIdSector = r.Item("IdSector")

                rootNode = tl.AppendNode(New Object() {"Sector: " & r.Item("IdSector").ToString(), r.Item("Descripcion"), ("S")}, Padre)

                Crea_Tramos(tlUbicacionesTodas,
                            vIdSector,
                            pIdBodega,
                            rootNode,
                            lConnection,
                            lTransaction)

                rootNode.Expanded = True

            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Sectores(ByRef tl As TreeList,
                              ByVal pIdArea As Integer,
                              ByVal pIdBodega As Integer,
                              ByVal Padre As TreeListNode)

        Try

            tl.BeginUnboundLoad()

            If chkUbicexist.Checked Then
                If Not DTSector Is Nothing Then DTSector.Clear()
                DTSector = clsLnBodega_sector.Get_All_Sector_By_IdArea_And_IdInventario(pIdArea, IdInventarioEnc, pIdBodega)
            Else
                DTSector = clsLnBodega_sector.Get_All_Sector_By_Area_And_IdBodega(pIdArea, AP.IdBodega)
            End If

            Dim rootNode As TreeListNode
            Dim vIdSector As Integer = 0

            SplashScreenManager.Default.SetWaitFormDescription("Cargando [Sector]: " & DTSector.Rows.Count)

            For Each r As DataRow In DTSector.Rows
                vIdSector = r.Item("IdSector")
                rootNode = tl.AppendNode(New Object() {"Sector: " & r.Item("IdSector").ToString(), r.Item("Descripcion"), ("S")}, Padre)
                Crea_Tramos(tlUbicacionesTodas, vIdSector, pIdBodega, rootNode)
                rootNode.Expanded = True
            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Tramos(ByRef tl As TreeList,
                            ByVal pIdSector As Integer,
                            ByVal pIdBodega As Integer,
                            ByRef Padre As TreeListNode,
                            ByVal lConnection As SqlConnection,
                            ByVal lTransaction As SqlTransaction)

        Try

            tl.BeginUnboundLoad()

            If chkUbicexist.Checked Then
                If Not DTTramo Is Nothing Then DTTramo.Clear()
                DTTramo = clsLnBodega_tramo.Get_All_Tramos_By_IdSector_And_IdInventario(pIdSector,
                                                                                        IdInventarioEnc,
                                                                                        pIdBodega,
                                                                                        lConnection,
                                                                                        lTransaction)
            Else
                DTTramo = clsLnBodega_tramo.Get_All_Tramos_By_Sector_And_IdBodega(pIdSector,
                                                                                  AP.IdBodega,
                                                                                  lConnection,
                                                                                  lTransaction)
            End If

            Dim rootNode As TreeListNode
            Dim vIdTramo As Integer = 0

            SplashScreenManager.Default.SetWaitFormDescription("Cargando [Tramo]: " & DTTramo.Rows.Count)

            For Each r As DataRow In DTTramo.Rows

                vIdTramo = r.Item("IdTramo")

                rootNode = tl.AppendNode(New Object() {"Tramo: " & r.Item("IdTramo").ToString(), r.Item("Descripcion"), ("T")}, Padre)

                Crea_Ubicaciones(tlUbicacionesTodas,
                                 vIdTramo,
                                 rootNode,
                                 lConnection,
                                 lTransaction)

            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Tramos(ByRef tl As TreeList,
                            ByVal pIdSector As Integer,
                            ByVal pIdBodega As Integer,
                            ByRef Padre As TreeListNode)

        Try

            tl.BeginUnboundLoad()

            If chkUbicexist.Checked Then
                If Not DTTramo Is Nothing Then DTTramo.Clear()
                DTTramo = clsLnBodega_tramo.Get_All_Tramos_By_IdSector_And_IdInventario(pIdSector, IdInventarioEnc, pIdBodega)
            Else
                DTTramo = clsLnBodega_tramo.Get_All_Tramos_By_Sector_And_IdBodega(pIdSector, AP.IdBodega)
            End If

            Dim rootNode As TreeListNode
            Dim vIdTramo As Integer = 0

            SplashScreenManager.Default.SetWaitFormDescription("Cargando [Tramo]: " & DTTramo.Rows.Count)

            For Each r As DataRow In DTTramo.Rows
                vIdTramo = r.Item("IdTramo")
                rootNode = tl.AppendNode(New Object() {"Tramo: " & r.Item("IdTramo").ToString(), r.Item("Descripcion"), ("T")}, Padre)
                Crea_Ubicaciones(tlUbicacionesTodas, vIdTramo, rootNode)
            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Ubicaciones(ByRef tl As TreeList,
                                 ByVal IdTramo As Integer,
                                 ByRef Padre As TreeListNode,
                                 ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction)

        Dim vIdUbicacion As Integer
        Dim vNombUbicacion As String = ""

        Try

            tl.BeginUnboundLoad()

            If chkUbicexist.Checked Then
                If Not DTUbicacion Is Nothing Then DTUbicacion.Clear()
                DTUbicacion = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_Existencias(IdTramo,
                                                                                      IdInventarioEnc,
                                                                                      AP.IdBodega,
                                                                                      lConnection,
                                                                                      lTransaction,
                                                                                      nombreCampo,
                                                                                      1,
                                                                                      1)
            Else
                DTUbicacion = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_IdTramo_And_IdBodega(IdTramo,
                                                                                               AP.IdBodega,
                                                                                               lConnection,
                                                                                               lTransaction,
                                                                                               nombreCampo,
                                                                                               1,
                                                                                               0)
            End If

            Dim rootNode As TreeListNode

            SplashScreenManager.Default.SetWaitFormDescription("Cargando Ubicaciones: (" & DTUbicacion.Rows.Count & ") Tramo: " & IdTramo)

            For Each r As DataRow In DTUbicacion.Rows

                vIdUbicacion = r.Item("IdUbicacion")

                '#EJC20211201: valida que, según la consulta, la columna exista
                Dim columna = IIf(IsDBNull(r.Table.Columns("Nombre_Completo")), "", r.Table.Columns("Nombre_Completo"))

                If columna IsNot Nothing Then
                    vNombUbicacion = IIf(IsDBNull(r.Item("Nombre_Completo")), vIdUbicacion, r.Item("Nombre_Completo"))
                Else
                    vNombUbicacion = vIdUbicacion
                End If

                rootNode = tl.AppendNode(New Object() {vIdUbicacion, vNombUbicacion, ("U")}, Padre)
                rootNode.Tag = IIf(r.Item("IdUbicacion") Is Nothing Or IsDBNull(r.Item("IdUbicacion")), 0, r.Item("IdUbicacion"))


            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Crea_Ubicaciones(ByRef tl As TreeList, ByVal IdTramo As Integer, ByRef Padre As TreeListNode)

        Dim vIdUbicacion As Integer
        Dim vNombUbicacion As String = ""

        Try

            tl.BeginUnboundLoad()

            If chkUbicexist.Checked Then
                If Not DTUbicacion Is Nothing Then DTUbicacion.Clear()
                DTUbicacion = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_Existencias(IdTramo,
                                                                                      IdInventarioEnc,
                                                                                      AP.IdBodega,
                                                                                      nombreCampo,
                                                                                      1,
                                                                                      1)
            Else
                DTUbicacion = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_IdTramo_And_IdBodega(IdTramo,
                                                                                               AP.IdBodega,
                                                                                               nombreCampo,
                                                                                               1,
                                                                                               0)
            End If

            Dim rootNode As TreeListNode

            SplashScreenManager.Default.SetWaitFormDescription("Cargando Ubicaciones: (" & DTUbicacion.Rows.Count & ") Tramo: " & IdTramo)

            For Each r As DataRow In DTUbicacion.Rows

                vIdUbicacion = r.Item("IdUbicacion")

                'GT 02092021: valida que, según la consulta, la columna exista
                Dim columna = r.Table.Columns("Nombre_Completo")

                If columna IsNot Nothing Then
                    vNombUbicacion = IIf(IsDBNull(r.Item("Nombre_Completo")), vIdUbicacion, r.Item("Nombre_Completo"))
                Else
                    vNombUbicacion = vIdUbicacion
                End If

                rootNode = tl.AppendNode(New Object() {vIdUbicacion, vNombUbicacion, ("U")}, Padre)
                rootNode.Tag = IIf(r.Item("IdUbicacion") Is Nothing Or IsDBNull(r.Item("IdUbicacion")), 0, r.Item("IdUbicacion"))

                'ListUbicacion = ListUbicacion.FindAll(Function(x) x.IdUbicacion = r.Item("IdUbicacion"))

                'If ListUbicacion.Count = 0 Then

                '    '#EJC20200207: Traer ubicación completa desde query
                '    'col = IIf(r.Item("Indice_x") Is Nothing Or IsDBNull(r.Item("Indice_x")), "", r.Item("Indice_x"))
                '    'niv = IIf(r.Item("Nivel") Is Nothing Or IsDBNull(r.Item("Nivel")), "", r.Item("Nivel"))
                '    'ori = IIf(r.Item("orientacion_pos") Is Nothing Or IsDBNull(r.Item("orientacion_pos")), "", r.Item("orientacion_pos"))

                '    'dr = DTOri.Select(String.Format("Nombre LIKE '%{0}%'", ori))
                '    'orr = IIf(dr(0).Item("Codigo") Is Nothing Or IsDBNull(dr(0).Item("Codigo")), "", dr(0).Item("Codigo"))

                '    'su = String.Format("C[{0}] N[{1}] P[{2}]", col, niv, orr)
                '    su = r.Item("IdUbicacion")

                '    rootNode = tl.AppendNode(New Object() {IIf(r.Item("IdUbicacion") Is Nothing Or IsDBNull(r.Item("IdUbicacion")), 0, r.Item("IdUbicacion")), su, ("U")}, Padre)
                '    rootNode.Tag = IIf(r.Item("IdUbicacion") Is Nothing Or IsDBNull(r.Item("IdUbicacion")), 0, r.Item("IdUbicacion"))

                'End If

            Next

            tl.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tlUbicacionesTodas_Click(sender As Object, e As EventArgs) Handles tlUbicacionesTodas.Click

        Try

            If tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(2)) = "U" Then

                If pBeUbicacion Is Nothing Then pBeUbicacion = New clsBeBodega_ubicacion
                pBeUbicacion.IdUbicacion = tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(0))
                pBeUbicacion.Descripcion = tlUbicacionesTodas.FocusedNode.GetDisplayText(tlUbicacionesTodas.Columns(1))
                '#CF20171113 09:58PM Erik dijo que solo filtraramos por IdUbicación pBeUbicacion = clsLnBodega_ubicacion.GetSingle(pBeUbicacion.IdUbicacion, "", Dañado, IdIndiceRotacion)
                pBeUbicacion = clsLnBodega_ubicacion.GetSingle(pBeUbicacion.IdUbicacion, AP.IdBodega)

                ptramo = tlUbicacionesTodas.FocusedNode.ParentNode.GetDisplayText(tlUbicacionesTodas.Columns(1))

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

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

    Private Sub txtFiltroUbic_EditValueChanged(sender As Object, e As EventArgs) Handles txtFiltroUbic.EditValueChanged
        If txtFiltroUbic.Text = "" Then
            limpiaFiltro()
            Exit Sub
        End If
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

    Private Sub Llena_Bodega()

        Try

            Crea_TreeList_Bodega(tlUbicacionesTodas)

            If pObjBeB.IdBodega <> 0 Then
                Crea_Areas(tlUbicacionesTodas, pObjBeB.IdBodega)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub tlUbicacionesTodas_DoubleClick(sender As Object, e As EventArgs) Handles tlUbicacionesTodas.DoubleClick

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

                ListUbicacion.Add(pBeUbicacion)

                Llena_Bodega()

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

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        If Guardar() Then
            XtraMessageBox.Show("Ubicaciones guardadas correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        Else
            XtraMessageBox.Show("Ubicaciones no guardadas", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Dim vIdProductoBodega As Integer = 0
        Dim vIdStock As Integer = 0
        Dim clsTrans As New clsTransaccion
        Dim lInvCongelado As New List(Of clsBeTrans_inv_stock)

        Try

            clsTrans.Open_Connection() : clsTrans.Begin_Transaction()

            Dim BeTransInvCiclicoUbic As New clsBeTrans_inv_ciclico_ubic
            Dim Ubicaciones As New List(Of clsBeTrans_inv_ciclico_ubic)

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Guardando ubicaciones...")

            For Each NA As TreeListNode In tlUbicacionesTodas.Nodes

                For Each NS As TreeListNode In NA.Nodes

                    For Each NT As TreeListNode In NS.Nodes

                        For Each NU As TreeListNode In NT.Nodes

                            If NU.Checked Then

                                BeTransInvCiclicoUbic = New clsBeTrans_inv_ciclico_ubic
                                BeTransInvCiclicoUbic.Idinventarioenc = IdInventarioEnc
                                BeTransInvCiclicoUbic.Idubicacion = NU.Tag
                                BeTransInvCiclicoUbic.IdBodega = pObjBeB.IdBodega
                                Ubicaciones.Add(BeTransInvCiclicoUbic)

                                Dim FilasFiltradas() As DataRow = lStockCongelado.Select("IdUbicacion = " & NU.Tag)

                                For Each ProductoCongeladoInvByUbic As DataRow In FilasFiltradas

                                    vIdProductoBodega = ProductoCongeladoInvByUbic.Item("IdProductoBodega")
                                    vIdStock = ProductoCongeladoInvByUbic.Item("IdStock")

                                    SplashScreenManager.Default.SetWaitFormDescription("Procesando IdStock: " & vIdStock)

                                    If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(NU.Tag, IdInventarioEnc, clsTrans.lConnection, clsTrans.lTransaction) Then

                                        BeTransInvCiclicoUbic = New clsBeTrans_inv_ciclico_ubic()
                                        BeTransInvCiclicoUbic.Idinventarioenc = IdInventarioEnc
                                        BeTransInvCiclicoUbic.Idubicacion = NU.Tag
                                        BeTransInvCiclicoUbic.IdBodega = AP.IdBodega
                                        clsLnTrans_inv_ciclico_ubic.Insertar(BeTransInvCiclicoUbic,
                                                                             clsTrans.lConnection,
                                                                             clsTrans.lTransaction)

                                    End If

                                    lInvCongelado = clsLnTrans_inv_stock.Get_All_By_IdInventarioEnc_And_IdProductoBodega(IdInventarioEnc,
                                                                                                                        vIdProductoBodega,
                                                                                                                        NU.Tag,
                                                                                                                        clsTrans.lConnection,
                                                                                                                        clsTrans.lTransaction)
                                    'GT 02092021 1222: si hay existencia iterar
                                    If lInvCongelado.Count > 0 Then

                                        For Each StockCongelado In lInvCongelado

                                            Dim InvCiclico As New clsBeTrans_inv_ciclico
                                            InvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                                            InvCiclico.Idinventarioenc = IdInventarioEnc
                                            InvCiclico.IdStock = StockCongelado.IdStock
                                            InvCiclico.IdProductoBodega = StockCongelado.IdProductoBodega
                                            InvCiclico.IdProductoEstado = StockCongelado.IdProductoEstado
                                            InvCiclico.IdProductoEst_nuevo = StockCongelado.IdProductoEstado
                                            InvCiclico.IdPresentacion = StockCongelado.IdPresentacion
                                            InvCiclico.IdUbicacion = StockCongelado.IdUbicacion
                                            InvCiclico.IdUnidadMedida = StockCongelado.IdUnidadMedida
                                            InvCiclico.Lote_stock = StockCongelado.Lote
                                            InvCiclico.Lote = StockCongelado.Lote
                                            InvCiclico.Fecha_vence_stock = StockCongelado.Fecha_vence
                                            InvCiclico.Fecha_vence = StockCongelado.Fecha_vence
                                            InvCiclico.Cant_stock = StockCongelado.Cantidad
                                            InvCiclico.Peso_stock = StockCongelado.Peso
                                            InvCiclico.EsNuevo = False
                                            InvCiclico.Idoperador = IdOperador
                                            InvCiclico.User_agr = AP.UsuarioAp.User_agr
                                            InvCiclico.Fec_agr = Now
                                            InvCiclico.Cantidad = 0.0
                                            InvCiclico.EsPallet = False 'StockCongelado.IdPresentacion Is Pallet ? -> EJC20180807
                                            InvCiclico.lic_plate = StockCongelado.Lic_plate
                                            InvCiclico.IdBodega = StockCongelado.IdBodega

                                            clsLnTrans_inv_ciclico.Insertar(InvCiclico, clsTrans.lConnection, clsTrans.lTransaction)

                                            Dim Operador As New clsBeTrans_inv_operador
                                            Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                                            Operador.Idinventarioenc = IdInventarioEnc
                                            Operador.Idinvencreconteo = 0
                                            Operador.Idubic = StockCongelado.IdUbicacion
                                            Operador.IdBodega = StockCongelado.IdBodega
                                            Operador.Idoperador = IdOperador

                                            If Not clsLnTrans_inv_operador.Existe_Operador_By_IdUbicacion(Operador,
                                                                                                          clsTrans.lConnection,
                                                                                                          clsTrans.lTransaction) Then
                                                clsLnTrans_inv_operador.Insertar(Operador,
                                                                                 clsTrans.lConnection,
                                                                                 clsTrans.lTransaction)
                                            End If

                                            If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(StockCongelado.IdUbicacion,
                                                                                                IdInventarioEnc,
                                                                                                clsTrans.lConnection,
                                                                                                clsTrans.lTransaction) Then

                                                Dim Ubicacion As New clsBeTrans_inv_ciclico_ubic
                                                Ubicacion.Idubicacion = StockCongelado.IdUbicacion
                                                Ubicacion.Idinventarioenc = IdInventarioEnc
                                                Ubicacion.IdBodega = StockCongelado.IdBodega

                                                clsLnTrans_inv_ciclico_ubic.Insertar(Ubicacion,
                                                                                     clsTrans.lConnection,
                                                                                     clsTrans.lTransaction)

                                            End If

                                            Debug.Print("Procesando interno IdStock: " & StockCongelado.IdStock)

                                        Next

                                    End If


                                Next

                            End If

                        Next

                    Next

                Next

            Next

            clsLnTrans_inv_ciclico_ubic.Guardar_Ubicaciones(Ubicaciones,
                                                            IdOperador,
                                                            clsTrans.lConnection,
                                                            clsTrans.lTransaction)

            clsTrans.Commit_Transaction()

            SplashScreenManager.CloseForm(False)

            Guardar = True

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
            clsTrans.Close_Conection()
        End Try

    End Function

    'Private Function Guardar() As Boolean

    '    Guardar = False

    '    Dim clsTrans As New clsTransaccion
    '    Dim lInvCongelado As New List(Of clsBeTrans_inv_stock)

    '    Try
    '        clsTrans.Open_Connection() : clsTrans.Begin_Transaction()

    '        ' Mostrar pantalla de carga
    '        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
    '        SplashScreenManager.Default.SetWaitFormDescription("Guardando ubicaciones...")

    '        Dim Ubicaciones As New Concurrent.ConcurrentBag(Of clsBeTrans_inv_ciclico_ubic)

    '        ' Ejecutar en paralelo las iteraciones de las ubicaciones
    '        Parallel.ForEach(tlUbicacionesTodas.Nodes.Cast(Of TreeListNode), Sub(NA)
    '                                                                             For Each NS As TreeListNode In NA.Nodes
    '                                                                                 For Each NT As TreeListNode In NS.Nodes
    '                                                                                     For Each NU As TreeListNode In NT.Nodes
    '                                                                                         If NU.Checked Then
    '                                                                                             Dim BeTransInvCiclicoUbic = New clsBeTrans_inv_ciclico_ubic() With {
    '                            .Idinventarioenc = IdInventarioEnc,
    '                            .Idubicacion = NU.Tag,
    '                            .IdBodega = pObjBeB.IdBodega
    '                        }
    '                                                                                             Ubicaciones.Add(BeTransInvCiclicoUbic)

    '                                                                                             Dim FilasFiltradas() As DataRow = lStockCongelado.Select("IdUbicacion = " & NU.Tag)

    '                                                                                             Parallel.ForEach(FilasFiltradas, Sub(ProductoCongeladoInvByUbic)
    '                                                                                                                                  Dim vIdProductoBodega = ProductoCongeladoInvByUbic.Item("IdProductoBodega")
    '                                                                                                                                  Dim vIdStock = ProductoCongeladoInvByUbic.Item("IdStock")

    '                                                                                                                                  If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(NU.Tag, IdInventarioEnc, clsTrans.lConnection, clsTrans.lTransaction) Then
    '                                                                                                                                      Dim NuevaUbicacion = New clsBeTrans_inv_ciclico_ubic() With {
    '                                    .Idinventarioenc = IdInventarioEnc,
    '                                    .Idubicacion = NU.Tag,
    '                                    .IdBodega = AP.IdBodega
    '                                }
    '                                                                                                                                      clsLnTrans_inv_ciclico_ubic.Insertar(NuevaUbicacion, clsTrans.lConnection, clsTrans.lTransaction)
    '                                                                                                                                  End If

    '                                                                                                                                  lInvCongelado = clsLnTrans_inv_stock.Get_All_By_IdInventarioEnc_And_IdProductoBodega(IdInventarioEnc, vIdProductoBodega, NU.Tag, clsTrans.lConnection, clsTrans.lTransaction)

    '                                                                                                                                  If lInvCongelado.Count > 0 Then
    '                                                                                                                                      Parallel.ForEach(lInvCongelado, Sub(StockCongelado)
    '                                                                                                                                                                          Dim InvCiclico = New clsBeTrans_inv_ciclico() With {
    '                                        .IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(clsTrans.lConnection, clsTrans.lTransaction),
    '                                        .Idinventarioenc = IdInventarioEnc,
    '                                        .IdStock = StockCongelado.IdStock,
    '                                        .IdProductoBodega = StockCongelado.IdProductoBodega,
    '                                        .IdProductoEstado = StockCongelado.IdProductoEstado,
    '                                        .IdPresentacion = StockCongelado.IdPresentacion,
    '                                        .IdUbicacion = StockCongelado.IdUbicacion,
    '                                        .IdUnidadMedida = StockCongelado.IdUnidadMedida,
    '                                        .Lote_stock = StockCongelado.Lote,
    '                                        .Lote = StockCongelado.Lote,
    '                                        .Fecha_vence_stock = StockCongelado.Fecha_vence,
    '                                        .Fecha_vence = StockCongelado.Fecha_vence,
    '                                        .Cant_stock = StockCongelado.Cantidad,
    '                                        .Peso_stock = StockCongelado.Peso,
    '                                        .EsNuevo = False,
    '                                        .Idoperador = IdOperador,
    '                                        .User_agr = AP.UsuarioAp.User_agr,
    '                                        .Fec_agr = Now,
    '                                        .Cantidad = 0.0,
    '                                        .EsPallet = False,
    '                                        .lic_plate = StockCongelado.Lic_plate,
    '                                        .IdBodega = StockCongelado.IdBodega
    '                                    }
    '                                                                                                                                                                          clsLnTrans_inv_ciclico.Insertar(InvCiclico, clsTrans.lConnection, clsTrans.lTransaction)

    '                                                                                                                                                                          Dim Operador = New clsBeTrans_inv_operador() With {
    '                                        .Idinvoperador = clsLnTrans_inv_operador.MaxID(clsTrans.lConnection, clsTrans.lTransaction),
    '                                        .Idinventarioenc = IdInventarioEnc,
    '                                        .Idinvencreconteo = 0,
    '                                        .Idubic = StockCongelado.IdUbicacion,
    '                                        .IdBodega = StockCongelado.IdBodega,
    '                                        .Idoperador = IdOperador
    '                                    }

    '                                                                                                                                                                          If Not clsLnTrans_inv_operador.Existe_Operador_By_IdUbicacion(Operador, clsTrans.lConnection, clsTrans.lTransaction) Then
    '                                                                                                                                                                              clsLnTrans_inv_operador.Insertar(Operador, clsTrans.lConnection, clsTrans.lTransaction)
    '                                                                                                                                                                          End If
    '                                                                                                                                                                      End Sub)
    '                                                                                                                                  End If
    '                                                                                                                              End Sub)
    '                                                                                         End If
    '                                                                                     Next
    '                                                                                 Next
    '                                                                             Next
    '                                                                         End Sub)

    '        clsLnTrans_inv_ciclico_ubic.Guardar_Ubicaciones(Ubicaciones.ToList(), IdOperador, clsTrans.lConnection, clsTrans.lTransaction)

    '        clsTrans.Commit_Transaction()

    '        SplashScreenManager.CloseForm(False)
    '        Guardar = True

    '    Catch ex As Exception
    '        clsTrans.RollBack_Transaction()
    '        SplashScreenManager.CloseForm(False)
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Finally
    '        SplashScreenManager.CloseForm(False)
    '        clsTrans.Close_Conection()
    '    End Try

    '    Return Guardar

    'End Function


    Private Sub tlUbicacionesTodas_AfterCheckNode(sender As Object, e As NodeEventArgs) Handles tlUbicacionesTodas.AfterCheckNode

        tlUbicacionesTodas.SetFocusedNode(e.Node)

        Try

            Dim ND = tlUbicacionesTodas.FocusedNode

            If ND.Checked Then

                ND.CheckAll()
            Else
                ND.UncheckAll()

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub btnFiltLimpia_Click(sender As Object, e As EventArgs) Handles btnFiltLimpia.Click
        limpiaFiltro()
    End Sub

    Private Sub tlUbicacionesTodas_MouseClick(sender As Object, e As MouseEventArgs) Handles tlUbicacionesTodas.MouseClick

        tlUbicacionesTodas.SetFocusedNode(tlUbicacionesTodas.GetNodeAt(e.X, e.Y))

    End Sub

    Private Sub chkUbicexist_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkUbicexist.CheckedChanged
        Llena_Bodega()
    End Sub

    Private Sub mnuMarcarTodos_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles mnuMarcarTodos.CheckedChanged

        Try

            pgrUbic.Properties.PercentView = True
            pgrUbic.Properties.Minimum = 0
            pgrUbic.Properties.Step = 1
            pgrUbic.Visible = True
            pgrUbic.EditValue = 0

            For Each NA As TreeListNode In tlUbicacionesTodas.Nodes
                If NA.Checked = False Then
                    NA.Checked = True
                Else
                    NA.Checked = False
                End If

                For Each NS As TreeListNode In NA.Nodes

                    If NS.Checked = False Then
                        NS.Checked = True
                    Else
                        NS.Checked = False
                    End If

                    For Each NT As TreeListNode In NS.Nodes

                        If NT.Checked = False Then
                            NT.Checked = True
                        Else
                            NT.Checked = False
                        End If

                        For Each NU As TreeListNode In NT.Nodes

                            If NU.Checked = False Then
                                NU.Checked = True
                            Else
                                NU.Checked = False
                            End If

                            pgrUbic.PerformStep()
                            pgrUbic.Update()

                        Next
                    Next
                Next
            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            pgrUbic.Visible = False
        End Try

    End Sub

    Private Sub frmSeleccionUbicacionesInv_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Llena_Bodega()

    End Sub

End Class