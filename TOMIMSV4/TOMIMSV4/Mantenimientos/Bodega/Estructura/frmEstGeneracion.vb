Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmEstGeneracion

    Public Property IdBodega As Integer = 0

    Private listBeEstructuraUbicacion As New List(Of clsBeEstructura_ubicacion)
    Private item As New clsBeEstructura_ubicacion
    Private tid, tcant, pcant, agrup, nivel, indice_x, ucant, tramo_orient As Integer
    Private btramo As New clsBeBodega_tramo
    Private listBeEstructuraTramo As New List(Of clsBeEstructura_tramo)
    Private listBeTramos As New List(Of clsBeBodega_tramo)
    Private BeBodegaUbicacion, bitem As New clsBeBodega_ubicacion
    Private listBeUbicaciones As New List(Of clsBeBodega_ubicacion)
    Private IndX As Integer = 0


#Region " Metodos principales "

    Private Function Procesar_Sectores(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Dim ns As TreeNode
        Dim ii As Integer

        Try

            pcant = 0
            tid = 0

            GroupBox1.Visible = True : lblPrg.Text = "" : pBar.Value = 0 : pBar.Maximum = tcant

            For ii = 0 To tvData.Nodes.Count - 1
                tvData.Nodes(ii).Collapse()
            Next

            tvData.Refresh() : Application.DoEvents()

            listBeEstructuraTramo.Clear()

            For ii = 0 To tvData.Nodes.Count - 1

                ns = tvData.Nodes(ii)

                If ns.Nodes.Count > 0 Then

                    Dim lResult As Boolean

                    lResult = Procesa_Sector_Async(ns, lblPrg, lConnection, lTransaction)

                    Try

                        If lResult Then
                            ns.ImageIndex = 3 : ns.SelectedImageIndex = 3
                        Else
                            ns.ImageIndex = 4 : ns.SelectedImageIndex = 4
                            Return False
                        End If

                    Catch ex As Exception
                        Throw New Exception("No se pudo procesar el sector: " & ns.Name)
                    End Try

                Else
                    ns.ImageIndex = 5 : ns.SelectedImageIndex = 5
                End If

                ns.ExpandAll()
                tvData.Refresh() : Application.DoEvents()

            Next

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Function

    Private Function Procesa_Sector_Async(ByVal np As TreeNode,
                                                ByVal lblprg As Label,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As Boolean

        Dim vProcesa_Sector As Boolean = False

        Try

            Dim ns As TreeNode
            Dim ii, val As Integer
            'Dim rslt As Task(Of Integer)
            Dim rslt As Integer

            np.ImageIndex = 2 : np.SelectedImageIndex = 2
            np.EnsureVisible() : np.Expand()
            tvData.Refresh()

            Application.DoEvents()

            For ii = 0 To np.Nodes.Count - 1

                ns = np.Nodes(ii)
                ns.EnsureVisible()

                rslt = Procesa_Tramo(ns, lblprg, lConnection, lTransaction)

                Select Case rslt
                    Case -1 : val = 4
                    Case 0 : val = 5
                    Case 1 : val = 3
                End Select

                ns.Text = ns.Text & " , ubicaciones : " & ucant
                ns.ImageIndex = val : ns.SelectedImageIndex = val

                tvData.Refresh()
                Application.DoEvents()

            Next

            vProcesa_Sector = True

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

        Return vProcesa_Sector

    End Function

    Private BeEstructuraTramo As New clsBeEstructura_tramo

    Private Sub ActualizarInterfazUsuario(ByVal NodoSector As TreeNode)
        NodoSector.ImageIndex = 2
        NodoSector.SelectedImageIndex = 2
        pcant += 1 : actualizaProgress()
        pBar.Value = pcant
        pBar.Refresh()
        tvData.Refresh()
        Application.DoEvents()
    End Sub

    Private Function Procesa_Tramo(ByVal NodoSector As TreeNode,
                                   ByVal lblprg As Label,
                                   ByVal lConnection As SqlConnection,
                                   ByVal lTransaction As SqlTransaction) As Integer

        Dim grupos As New List(Of clsBeEstructura_grupo)
        Dim IdTramo As Integer
        Dim Es_Horizontal As Boolean = False

        NodoSector.ImageIndex = 2 : NodoSector.SelectedImageIndex = 2
        pcant += 1 : actualizaProgress()
        pBar.Value = pcant : pBar.Refresh()
        tvData.Refresh() : Application.DoEvents()

        Try
            listBeEstructuraUbicacion.Clear()
            nivel = 0 : indice_x = 0 : IndX = 0
            IdTramo = NodoSector.Tag

            BeEstructuraTramo = clsLnEstructura_Tramo.Get_Single_By_IdTramo(IdBodega, IdTramo, lConnection, lTransaction)
            listBeEstructuraTramo.Add(BeEstructuraTramo)

            tramo_orient = BeEstructuraTramo.Orientacion
            Es_Horizontal = BeEstructuraTramo.Horizontal

            lblprg.Text = "Obteniendo estructura de grupo para tramo: " & IdTramo
            lblprg.Refresh()

            grupos = clsLnEstructura_grupo.Get_All_By_IdBodega_And_IdTramo(IdBodega, IdTramo, lConnection, lTransaction)
            If grupos.Count = 0 Then Return 0

            ucant = 0
            lblprg.Text = "Generando lista de ubicaciones..."
            lblprg.Refresh()

            If grupos.Count > 1 Then
                Debug.Print("Mayor que uno")
            End If

            For Each grp As clsBeEstructura_grupo In grupos
                Dim result As Boolean
                If Es_Horizontal Then
                    result = Procesa_Grupo_Vert(grp)
                Else
                    If grp.Agrupacion = 1 Then
                        result = Procesa_Grupo_Vert(grp)
                    Else
                        result = Procesa_Grupo_Horizontal(grp)
                    End If
                End If
                If Not result Then
                    Return -1
                End If
            Next

            clsLnEstructura_ubicacion.Insertar_Estructura_Ubicacion(listBeEstructuraUbicacion, lblprg, lConnection, lTransaction)

            Return 1

        Catch ex As Exception
            Throw ex
            Return -1
        End Try
    End Function



    'Private Async Function Procesa_Tramo(ByVal NodoSector As TreeNode,
    '                                     ByVal lblprg As Label,
    '                                     ByVal lConnection As SqlConnection,
    '                                     ByVal lTransaction As SqlTransaction) As Task(Of Integer)

    '    Dim grupos As New List(Of clsBeEstructura_grupo)
    '    Dim IdTramo As Integer
    '    Dim Es_Horizontal As Boolean = False
    '    Dim lTask As New List(Of Task(Of Boolean))

    '    NodoSector.ImageIndex = 2 : NodoSector.SelectedImageIndex = 2
    '    pcant += 1 : actualizaProgress()
    '    pBar.Value = pcant : pBar.Refresh()
    '    tvData.Refresh() : Application.DoEvents()

    '    Try

    '        listBeEstructuraUbicacion.Clear()
    '        nivel = 0 : indice_x = 0 : IndX = 0
    '        IdTramo = NodoSector.Tag

    '        BeEstructuraTramo = Await clsLnEstructura_Tramo.GetSingle_Async(IdTramo, lConnection, lTransaction)
    '        listBeEstructuraTramo.Add(BeEstructuraTramo)

    '        tramo_orient = BeEstructuraTramo.Orientacion
    '        Es_Horizontal = BeEstructuraTramo.Horizontal

    '        lblprg.Text = "Obteniendo estructura de grupo para tramo: " & IdTramo
    '        lblprg.Refresh()

    '        grupos = clsLnEstructura_grupo.Get_All_By_IdBodega_And_IdTramo(IdBodega, IdTramo, lConnection, lTransaction)
    '        If grupos.Count = 0 Then Return 0

    '        ucant = 0
    '        lblprg.Text = "Generando lista de ubicaciones..."
    '        lblprg.Refresh()

    '        For Each grp As clsBeEstructura_grupo In grupos
    '            If Es_Horizontal Then
    '                Procesa_Grupo_Horizontal_Async(grp)
    '            Else
    '                If grp.Agrupacion = 1 Then
    '                    Procesa_Grupo_Vert_Async(grp)
    '                Else
    '                    Procesa_Grupo_Horiz_Async(grp)
    '                End If
    '            End If
    '        Next

    '        Await Task.WhenAll(lTask)
    '        Await clsLnEstructura_ubicacion.Insertar_Estructura_Ubicacion(listBeEstructuraUbicacion, lblprg, lConnection, lTransaction)

    '        Return 1

    '    Catch ex As Exception
    '        Throw ex
    '        Return -1
    '    End Try

    'End Function


    'Private Async Function Procesa_Tramo_Async(ByVal NodoSector As TreeNode,
    '                                           ByVal lblprg As Label,
    '                                           ByVal lConnection As SqlConnection,
    '                                           ByVal lTransaction As SqlTransaction) As Task(Of Integer)

    '    Dim grupos As New List(Of clsBeEstructura_grupo)
    '    Dim IdTramo As Integer
    '    Dim Es_Horizontal As Boolean = False
    '    Dim lTask As New List(Of Task)

    '    NodoSector.ImageIndex = 2 : NodoSector.SelectedImageIndex = 2
    '    pcant += 1 : actualizaProgress()

    '    Try
    '        pBar.Value = pcant : pBar.Refresh()
    '    Catch ex As Exception
    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '    End Try

    '    tvData.Refresh() : Application.DoEvents()

    '    Try

    '        listBeEstructuraUbicacion.Clear()

    '        nivel = 0 : indice_x = 0 : IndX = 0

    '        IdTramo = NodoSector.Tag

    '        BeEstructuraTramo = New clsBeEstructura_tramo()
    '        BeEstructuraTramo.IdTramo = IdTramo

    '        lblprg.Text = "Obteniendo estructura de tramo: " & IdTramo
    '        lblprg.Refresh()

    '        BeEstructuraTramo = Await clsLnEstructura_tramo.GetSingle_Async(IdTramo, lConnection, lTransaction)

    '        listBeEstructuraTramo.Add(BeEstructuraTramo)

    '        tramo_orient = BeEstructuraTramo.Orientacion
    '        Es_Horizontal = BeEstructuraTramo.Horizontal

    '        Try

    '            grupos = clsLnEstructura_grupo.Get_All_By_IdBodega_And_IdTramo(IdBodega,
    '                                                                           IdTramo,
    '                                                                           lConnection,
    '                                                                           lTransaction)

    '            lblprg.Text = "Obteniendo estructura de grupo para tramo: " & IdTramo
    '            lblprg.Refresh()

    '            If (grupos.Count = 0) Then Return 0

    '            If (IsNothing(grupos)) Then
    '                MsgBox(IdTramo) : Return 0
    '            End If

    '            agrup = grupos(0).Agrupacion

    '        Catch ex As Exception
    '            Throw ex
    '            Return 0
    '        End Try

    '        ucant = 0

    '        lblprg.Text = "Generando lista de ubicaciones..."
    '        lblprg.Refresh()

    '        For Each grp As clsBeEstructura_grupo In grupos

    '            If grp.IdGrupo = 303 Then
    '                Debug.Write("tipo 3")
    '            End If

    '            If Es_Horizontal Then
    '                'If Not Procesa_Grupo_Horizontal(grp) Then Return -1
    '                Procesa_Grupo_Horizontal_Async(grp)
    '            Else
    '                If agrup = 1 Then
    '                    'If Not Procesa_Grupo_Vert(grp) Then Return -1
    '                    Procesa_Grupo_Vert_(grp)
    '                Else
    '                    'If Not Procesa_Grupo_Horiz(grp) Then Return -1
    '                    Procesa_Grupo_Horiz_Async(grp)
    '                End If
    '            End If

    '        Next

    '        Await Task.WhenAll(lTask)

    '        Await clsLnEstructura_ubicacion.Insertar_Estructura_Ubicacion(listBeEstructuraUbicacion, lblprg, lConnection, lTransaction)

    '        Return 1

    '    Catch ex As Exception
    '        Throw ex
    '        Return -1
    '    End Try

    'End Function


    'Private Async Function Procesa_Grupo_Horiz_Async(grp As clsBeEstructura_grupo) As Task(Of Boolean)


    '    Dim ii, jj, orien As Integer
    '    Dim it, itFR, itBL, itBR As New clsBeEstructura_ubicacion
    '    Dim ancho, largo As Double

    '    Try

    '        For ii = 1 To grp.Tamano

    '            nivel += 1

    '            orien = grp.Orient
    '            ancho = grp.Ancho
    '            largo = grp.Largo

    '            Select Case orien
    '                Case 2
    '                    ancho = ancho / 2
    '                    largo = largo
    '                Case 3
    '                    ancho = ancho
    '                    largo = largo / 2
    '                Case 4
    '                    ancho = ancho / 2
    '                    largo = largo / 2
    '            End Select

    '            For jj = 1 To grp.Cant

    '                tid += 1 : ucant += 1
    '                it = item.Clone

    '                it.IdUbicacion = tid
    '                it.IdTramo = grp.IdTramo
    '                it.IdSector = BeEstructuraTramo.IdSector
    '                it.IdArea = BeEstructuraTramo.IdArea
    '                it.IdBodega = BeEstructuraTramo.IdBodega
    '                it.Ancho = ancho
    '                it.Largo = largo
    '                it.Alto = grp.Alto
    '                it.Nivel = nivel
    '                it.Indice_x = IndX 'jj + grp.Offset
    '                it.Acepta_pallet = grp.Palet = 1

    '                If tramo_orient = 0 Then
    '                    it.IdUbicacion = tid + 1
    '                    it.IdTramo = grp.IdTramo
    '                    it.IdSector = BeEstructuraTramo.IdSector
    '                    it.IdArea = BeEstructuraTramo.IdArea
    '                    it.IdBodega = BeEstructuraTramo.IdBodega
    '                    it.Descripcion = it.Indice_x & "A" & it.Nivel
    '                    it.Orientacion_pos = "A"
    '                Else
    '                    it.Descripcion = it.Indice_x & "B" & it.Nivel
    '                    it.Orientacion_pos = "B"
    '                End If

    '                it.IdIndiceRotacion = 1
    '                it.IdTipoRotacion = 1

    '                listBeEstructuraUbicacion.Add(it)

    '                Select Case orien

    '                    Case 2

    '                        itFR = it.Clone : tid += 1 : ucant += 1
    '                        itFR.IdUbicacion = tid
    '                        itFR.IdTramo = grp.IdTramo
    '                        itFR.IdSector = BeEstructuraTramo.IdSector
    '                        itFR.IdArea = BeEstructuraTramo.IdArea
    '                        itFR.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            '#CKFK 20210815 Cuando sea un Rack de tipo 2 se va a ganerar la posicion Frontal derecha como posicion B
    '                            'itFR.Descripcion = it.Indice_x & "C" & it.Nivel
    '                            'itFR.Orientacion_pos = "C"
    '                            itFR.Descripcion = it.Indice_x & "B" & it.Nivel
    '                            itFR.Orientacion_pos = "B"
    '                        Else
    '                            itFR.Descripcion = it.Indice_x & "D" & it.Nivel
    '                            itFR.Orientacion_pos = "D"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itFR)

    '                    Case 3

    '                        itBL = it.Clone : tid += 1 : ucant += 1
    '                        itBL.IdUbicacion = tid

    '                        itBL.IdTramo = grp.IdTramo
    '                        itBL.IdSector = BeEstructuraTramo.IdSector
    '                        itBL.IdArea = BeEstructuraTramo.IdArea
    '                        itBL.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itBL.Descripcion = it.Indice_x & "A" & it.Nivel
    '                            itBL.Orientacion_pos = "A"
    '                        Else
    '                            itBL.Descripcion = it.Indice_x & "B" & it.Nivel
    '                            itBL.Orientacion_pos = "B"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itBL)

    '                    Case 4

    '                        itFR = it.Clone : tid += 1 : ucant += 1
    '                        itFR.IdUbicacion = tid


    '                        itFR.IdTramo = grp.IdTramo
    '                        itFR.IdSector = BeEstructuraTramo.IdSector
    '                        itFR.IdArea = BeEstructuraTramo.IdArea
    '                        itFR.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itFR.IdUbicacion = tid - 1
    '                            itFR.IdTramo = grp.IdTramo
    '                            itFR.IdSector = BeEstructuraTramo.IdSector
    '                            itFR.IdArea = BeEstructuraTramo.IdArea
    '                            itFR.IdBodega = BeEstructuraTramo.IdBodega
    '                            itFR.Descripcion = it.Indice_x & "C" & it.Nivel
    '                            itFR.Orientacion_pos = "C"
    '                        Else
    '                            itFR.Descripcion = it.Indice_x & "D" & it.Nivel
    '                            itFR.Orientacion_pos = "D"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itFR)

    '                        itBL = it.Clone : tid += 1 : ucant += 1
    '                        itBL.IdUbicacion = tid

    '                        itBL.IdTramo = grp.IdTramo
    '                        itBL.IdSector = BeEstructuraTramo.IdSector
    '                        itBL.IdArea = BeEstructuraTramo.IdArea
    '                        itBL.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itBL.IdUbicacion = tid + 1
    '                            itBL.IdTramo = grp.IdTramo
    '                            itBL.IdSector = BeEstructuraTramo.IdSector
    '                            itBL.IdArea = BeEstructuraTramo.IdArea
    '                            itBL.IdBodega = BeEstructuraTramo.IdBodega
    '                            itBL.Descripcion = it.Indice_x & "B" & it.Nivel
    '                            itBL.Orientacion_pos = "B"
    '                        Else
    '                            itBL.Descripcion = it.Indice_x & "A" & it.Nivel
    '                            itBL.Orientacion_pos = "A"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itBL)

    '                        itBR = it.Clone : tid += 1 : ucant += 1
    '                        itBR.IdUbicacion = tid

    '                        itBR.IdTramo = grp.IdTramo
    '                        itBR.IdSector = BeEstructuraTramo.IdSector
    '                        itBR.IdArea = BeEstructuraTramo.IdArea
    '                        itBR.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itBR.IdUbicacion = tid - 1

    '                            itBR.IdTramo = grp.IdTramo
    '                            itBR.IdSector = BeEstructuraTramo.IdSector
    '                            itBR.IdArea = BeEstructuraTramo.IdArea
    '                            itBR.IdBodega = BeEstructuraTramo.IdBodega

    '                            itBR.Descripcion = it.Indice_x & "C" & it.Nivel
    '                            itBR.Orientacion_pos = "C"
    '                        Else
    '                            itBR.Descripcion = it.Indice_x & "D" & it.Nivel
    '                            itBR.Orientacion_pos = "D"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itBR)

    '                End Select

    '            Next

    '        Next

    '        Return True

    '    Catch ex As Exception
    '        Throw ex
    '        Return False
    '    End Try

    'End Function

    Private Function Procesa_Grupo_Vert(grp As clsBeEstructura_grupo) As Boolean

        Dim ii, jj, orien As Integer
        Dim it, itFR, itBL, itBR As clsBeEstructura_ubicacion
        Dim ancho, largo As Double

        Try

            For ii = 1 To grp.Cant

                indice_x += 1

                orien = grp.Orient
                ancho = grp.Ancho
                largo = grp.Largo

                Select Case orien
                    Case 2
                        ancho = ancho / 2
                        largo = largo
                    Case 3
                        ancho = ancho
                        largo = largo / 2
                    Case 4
                        ancho = ancho / 2
                        largo = largo / 2
                End Select

                For jj = 1 To grp.Tamano

                    tid += 1 : ucant += 1

                    it = item.Clone
                    it.IdUbicacion = tid
                    it.IdTramo = grp.IdTramo
                    it.IdSector = BeEstructuraTramo.IdSector
                    it.IdArea = BeEstructuraTramo.IdArea
                    it.IdBodega = BeEstructuraTramo.IdBodega
                    it.Ancho = ancho
                    it.Largo = largo
                    it.Alto = grp.Alto
                    it.Nivel = jj + grp.Offset
                    it.Indice_x = indice_x
                    it.Acepta_pallet = grp.Palet = 1

                    If tid = 618 Then
                        Debug.Print("Espera")
                    End If

                    'it.Descripcion = it.Indice_x & "A" & it.Nivel
                    If tramo_orient = 0 Then
                        it.IdUbicacion = tid + 1
                        it.IdTramo = grp.IdTramo
                        it.IdSector = BeEstructuraTramo.IdSector
                        it.IdArea = BeEstructuraTramo.IdArea
                        it.IdBodega = BeEstructuraTramo.IdBodega
                        it.Orientacion_pos = "A"
                        it.Descripcion = it.Indice_x & "A" & it.Nivel
                    Else
                        it.Orientacion_pos = "A"
                        it.Descripcion = it.Indice_x & "A" & it.Nivel
                    End If

                    it.IdIndiceRotacion = 1
                    it.IdTipoRotacion = 1

                    listBeEstructuraUbicacion.Add(it)

                    Select Case orien

                        Case 2

                            itFR = it.Clone : tid += 1 : ucant += 1
                            itFR.IdUbicacion = tid
                            itFR.IdTramo = grp.IdTramo
                            itFR.IdSector = BeEstructuraTramo.IdSector
                            itFR.IdArea = BeEstructuraTramo.IdArea
                            itFR.IdBodega = BeEstructuraTramo.IdBodega

                            If tramo_orient = 0 Then
                                itFR.Descripcion = it.Indice_x & "B" & it.Nivel
                                itFR.Orientacion_pos = "B"
                            Else
                                itFR.Descripcion = it.Indice_x & "B" & it.Nivel
                                itFR.Orientacion_pos = "B"
                            End If

                            listBeEstructuraUbicacion.Add(itFR)

                        Case 3

                            itBL = it.Clone : tid += 1 : ucant += 1
                            itBL.IdUbicacion = tid
                            itBL.IdTramo = grp.IdTramo
                            itBL.IdSector = BeEstructuraTramo.IdSector
                            itBL.IdArea = BeEstructuraTramo.IdArea
                            itBL.IdBodega = BeEstructuraTramo.IdBodega

                            If tramo_orient = 0 Then
                                itBL.Descripcion = it.Indice_x & "D" & it.Nivel
                                itBL.Orientacion_pos = "D"
                            Else
                                itBL.Descripcion = it.Indice_x & "C" & it.Nivel
                                itBL.Orientacion_pos = "C"
                            End If

                            listBeEstructuraUbicacion.Add(itBL)

                        Case 4

                            itFR = it.Clone : tid += 1 : ucant += 1
                            itFR.IdUbicacion = tid
                            itFR.IdTramo = grp.IdTramo
                            itFR.IdSector = BeEstructuraTramo.IdSector
                            itFR.IdArea = BeEstructuraTramo.IdArea
                            itFR.IdBodega = BeEstructuraTramo.IdBodega

                            If tramo_orient = 0 Then
                                itFR.IdUbicacion = tid - 1
                                itFR.IdTramo = grp.IdTramo
                                itFR.IdSector = BeEstructuraTramo.IdSector
                                itFR.IdArea = BeEstructuraTramo.IdArea
                                itFR.IdBodega = BeEstructuraTramo.IdBodega
                                itFR.Descripcion = it.Indice_x & "B" & it.Nivel
                                itFR.Orientacion_pos = "B"
                            Else
                                itFR.Descripcion = it.Indice_x & "B" & it.Nivel
                                itFR.Orientacion_pos = "B"
                            End If

                            listBeEstructuraUbicacion.Add(itFR)

                            itBL = it.Clone : tid += 1 : ucant += 1
                            itBL.IdUbicacion = tid
                            itBL.IdTramo = grp.IdTramo
                            itBL.IdSector = BeEstructuraTramo.IdSector
                            itBL.IdArea = BeEstructuraTramo.IdArea
                            itBL.IdBodega = BeEstructuraTramo.IdBodega

                            If tramo_orient = 0 Then
                                itBL.IdUbicacion = tid + 1
                                itBL.IdTramo = grp.IdTramo
                                itBL.IdSector = BeEstructuraTramo.IdSector
                                itBL.IdArea = BeEstructuraTramo.IdArea
                                itBL.IdBodega = BeEstructuraTramo.IdBodega
                                itBL.Descripcion = it.Indice_x & "D" & it.Nivel
                                itBL.Orientacion_pos = "D"
                            Else
                                itBL.Descripcion = it.Indice_x & "C" & it.Nivel
                                itBL.Orientacion_pos = "C"
                            End If

                            listBeEstructuraUbicacion.Add(itBL)

                            itBR = it.Clone : tid += 1 : ucant += 1
                            itBR.IdUbicacion = tid
                            itBR.IdTramo = grp.IdTramo
                            itBR.IdSector = BeEstructuraTramo.IdSector
                            itBR.IdArea = BeEstructuraTramo.IdArea
                            itBR.IdBodega = BeEstructuraTramo.IdBodega

                            If tramo_orient = 0 Then
                                itBR.IdUbicacion = tid - 1
                                itBR.IdTramo = grp.IdTramo
                                itBR.IdSector = BeEstructuraTramo.IdSector
                                itBR.IdArea = BeEstructuraTramo.IdArea
                                itBR.IdBodega = BeEstructuraTramo.IdBodega
                                itBR.Descripcion = it.Indice_x & "C" & it.Nivel
                                itBR.Orientacion_pos = "C"
                            Else
                                itBR.Descripcion = it.Indice_x & "D" & it.Nivel
                                itBR.Orientacion_pos = "D"
                            End If

                            listBeEstructuraUbicacion.Add(itBR)

                    End Select

                Next

            Next

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        End Try

    End Function

    '#CKFK20240611 Voy a quitarle a esta el Async
    Private Function Procesa_Grupo_Horizontal(grp As clsBeEstructura_grupo) As Boolean

        Dim ii, jj, orien As Integer
        Dim BeEstructura_ubicacion, BeEstructura_ubicacionFR, BeEstructura_ubicacionBL, BeEstructura_ubicacionBR As New clsBeEstructura_ubicacion
        Dim ancho, largo As Double

        Try

            For ii = 1 To grp.Cant

                IndX += 1
                nivel = grp.Offset

                orien = grp.Orient

                '#CKFK20230831 Voy a poner esto en el for de adentro
                'ancho = grp.Ancho
                'largo = grp.Largo

                'Select Case orien
                '    Case 2
                '        ancho = ancho / 2
                '        largo = largo
                '    Case 3
                '        ancho = ancho
                '        largo = largo / 2
                '    Case 4
                '        ancho = ancho / 2
                '        largo = largo / 2
                'End Select

                For jj = 1 To grp.Tamano

                    '#CKFK20230831 Antes esto estaba afuera
                    ancho = grp.Ancho
                    largo = grp.Largo

                    Select Case orien
                        Case 2
                            ancho = ancho / 2
                            largo = largo
                        Case 3
                            ancho = ancho
                            largo = largo / 2
                        Case 4
                            ancho = ancho / 2
                            largo = largo / 2
                    End Select

                    nivel += 1

                    tid += 1 : ucant += 1
                    BeEstructura_ubicacion = item.Clone

                    BeEstructura_ubicacion.IdUbicacion = tid

                    If BeEstructura_ubicacion.IdUbicacion = 3 Then
                        Debug.Write("stop")
                    End If

                    BeEstructura_ubicacion.IdTramo = grp.IdTramo
                    BeEstructura_ubicacion.IdSector = BeEstructuraTramo.IdSector
                    BeEstructura_ubicacion.IdArea = BeEstructuraTramo.IdArea
                    BeEstructura_ubicacion.IdBodega = BeEstructuraTramo.IdBodega
                    BeEstructura_ubicacion.Ancho = ancho
                    BeEstructura_ubicacion.Largo = largo
                    BeEstructura_ubicacion.Alto = grp.Alto
                    BeEstructura_ubicacion.Nivel = nivel
                    BeEstructura_ubicacion.Indice_x = IndX 'jj + grp.Offset
                    BeEstructura_ubicacion.Acepta_pallet = grp.Palet = 1

                    If tramo_orient = 0 Then
                        BeEstructura_ubicacion.IdUbicacion = tid + 1
                        BeEstructura_ubicacion.IdTramo = grp.IdTramo
                        BeEstructura_ubicacion.IdSector = BeEstructuraTramo.IdSector
                        BeEstructura_ubicacion.IdArea = BeEstructuraTramo.IdArea
                        BeEstructura_ubicacion.IdBodega = BeEstructuraTramo.IdBodega
                        BeEstructura_ubicacion.Descripcion = BeEstructura_ubicacion.Indice_x & "A" & BeEstructura_ubicacion.Nivel
                        BeEstructura_ubicacion.Orientacion_pos = "A"
                    Else

                        If orien = 1 Then
                            BeEstructura_ubicacion.IdUbicacion = tid + 1
                            BeEstructura_ubicacion.IdTramo = grp.IdTramo
                            BeEstructura_ubicacion.IdSector = BeEstructuraTramo.IdSector
                            BeEstructura_ubicacion.IdArea = BeEstructuraTramo.IdArea
                            BeEstructura_ubicacion.IdBodega = BeEstructuraTramo.IdBodega
                            BeEstructura_ubicacion.Descripcion = BeEstructura_ubicacion.Indice_x & "A" & BeEstructura_ubicacion.Nivel
                            BeEstructura_ubicacion.Orientacion_pos = "A"
                        Else
                            BeEstructura_ubicacion.Descripcion = BeEstructura_ubicacion.Indice_x & "B" & BeEstructura_ubicacion.Nivel
                            BeEstructura_ubicacion.Orientacion_pos = "B"
                        End If

                    End If

                    Select Case orien

                        Case 2

                            BeEstructura_ubicacion.IdUbicacion = tid + 1
                            BeEstructura_ubicacion.IdTramo = grp.IdTramo
                            BeEstructura_ubicacion.IdSector = BeEstructuraTramo.IdSector
                            BeEstructura_ubicacion.IdArea = BeEstructuraTramo.IdArea
                            BeEstructura_ubicacion.IdBodega = BeEstructuraTramo.IdBodega
                            BeEstructura_ubicacion.Descripcion = BeEstructura_ubicacion.Indice_x & "A" & BeEstructura_ubicacion.Nivel
                            BeEstructura_ubicacion.Orientacion_pos = "A"

                    End Select

                    BeEstructura_ubicacion.IdIndiceRotacion = 1
                    BeEstructura_ubicacion.IdTipoRotacion = 1

                    listBeEstructuraUbicacion.Add(BeEstructura_ubicacion)

                    Select Case orien

                        Case 2

                            BeEstructura_ubicacionFR = BeEstructura_ubicacion.Clone : tid += 1 : ucant += 1
                            BeEstructura_ubicacionFR.IdUbicacion = tid

                            BeEstructura_ubicacionFR.IdTramo = grp.IdTramo
                            BeEstructura_ubicacionFR.IdSector = BeEstructuraTramo.IdSector
                            BeEstructura_ubicacionFR.IdArea = BeEstructuraTramo.IdArea
                            BeEstructura_ubicacionFR.IdBodega = BeEstructuraTramo.IdBodega
                            BeEstructura_ubicacionFR.Descripcion = BeEstructura_ubicacion.Indice_x & "B" & BeEstructura_ubicacion.Nivel
                            BeEstructura_ubicacionFR.Orientacion_pos = "B"
                            listBeEstructuraUbicacion.Add(BeEstructura_ubicacionFR)

                        Case 3

                            BeEstructura_ubicacionBL = BeEstructura_ubicacion.Clone : tid += 1 : ucant += 1
                            BeEstructura_ubicacionBL.IdUbicacion = tid
                            BeEstructura_ubicacionBL.IdTramo = grp.IdTramo
                            BeEstructura_ubicacionBL.IdSector = BeEstructuraTramo.IdSector
                            BeEstructura_ubicacionBL.IdArea = BeEstructuraTramo.IdArea
                            BeEstructura_ubicacionBL.IdBodega = BeEstructuraTramo.IdBodega
                            If tramo_orient = 0 Then
                                BeEstructura_ubicacionBL.Descripcion = BeEstructura_ubicacion.Indice_x & "B" & BeEstructura_ubicacion.Nivel
                                BeEstructura_ubicacionBL.Orientacion_pos = "B"
                            Else
                                BeEstructura_ubicacionBL.Descripcion = BeEstructura_ubicacion.Indice_x & "A" & BeEstructura_ubicacion.Nivel
                                BeEstructura_ubicacionBL.Orientacion_pos = "A"
                            End If

                            listBeEstructuraUbicacion.Add(BeEstructura_ubicacionBL)

                        Case 4

                            BeEstructura_ubicacionFR = BeEstructura_ubicacion.Clone : tid += 1 : ucant += 1
                            BeEstructura_ubicacionFR.IdUbicacion = tid
                            BeEstructura_ubicacionFR.IdTramo = grp.IdTramo
                            BeEstructura_ubicacionFR.IdSector = BeEstructuraTramo.IdSector
                            BeEstructura_ubicacionFR.IdArea = BeEstructuraTramo.IdArea
                            BeEstructura_ubicacionFR.IdBodega = BeEstructuraTramo.IdBodega

                            If tramo_orient = 0 Then
                                BeEstructura_ubicacionFR.IdUbicacion = tid - 1
                                BeEstructura_ubicacionFR.IdTramo = grp.IdTramo
                                BeEstructura_ubicacionFR.IdSector = BeEstructuraTramo.IdSector
                                BeEstructura_ubicacionFR.IdArea = BeEstructuraTramo.IdArea
                                BeEstructura_ubicacionFR.IdBodega = BeEstructuraTramo.IdBodega
                                BeEstructura_ubicacionFR.Descripcion = BeEstructura_ubicacion.Indice_x & "C" & BeEstructura_ubicacion.Nivel
                                BeEstructura_ubicacionFR.Orientacion_pos = "C"
                            Else
                                BeEstructura_ubicacionFR.Descripcion = BeEstructura_ubicacion.Indice_x & "D" & BeEstructura_ubicacion.Nivel
                                BeEstructura_ubicacionFR.Orientacion_pos = "D"
                            End If

                            listBeEstructuraUbicacion.Add(BeEstructura_ubicacionFR)

                            BeEstructura_ubicacionBL = BeEstructura_ubicacion.Clone : tid += 1 : ucant += 1
                            BeEstructura_ubicacionBL.IdUbicacion = tid

                            BeEstructura_ubicacionBL.IdTramo = grp.IdTramo
                            BeEstructura_ubicacionBL.IdSector = BeEstructuraTramo.IdSector
                            BeEstructura_ubicacionBL.IdArea = BeEstructuraTramo.IdArea
                            BeEstructura_ubicacionBL.IdBodega = BeEstructuraTramo.IdBodega

                            If tramo_orient = 0 Then
                                BeEstructura_ubicacionBL.IdUbicacion = tid + 1
                                BeEstructura_ubicacionBL.IdTramo = grp.IdTramo
                                BeEstructura_ubicacionBL.IdSector = BeEstructuraTramo.IdSector
                                BeEstructura_ubicacionBL.IdArea = BeEstructuraTramo.IdArea
                                BeEstructura_ubicacionBL.IdBodega = BeEstructuraTramo.IdBodega
                                BeEstructura_ubicacionBL.Descripcion = BeEstructura_ubicacion.Indice_x & "B" & BeEstructura_ubicacion.Nivel
                                BeEstructura_ubicacionBL.Orientacion_pos = "B"
                            Else
                                BeEstructura_ubicacionBL.Descripcion = BeEstructura_ubicacion.Indice_x & "A" & BeEstructura_ubicacion.Nivel
                                BeEstructura_ubicacionBL.Orientacion_pos = "A"
                            End If

                            listBeEstructuraUbicacion.Add(BeEstructura_ubicacionBL)

                            BeEstructura_ubicacionBR = BeEstructura_ubicacion.Clone : tid += 1 : ucant += 1
                            BeEstructura_ubicacionBR.IdUbicacion = tid

                            BeEstructura_ubicacionBR.IdTramo = grp.IdTramo
                            BeEstructura_ubicacionBR.IdSector = BeEstructuraTramo.IdSector
                            BeEstructura_ubicacionBR.IdArea = BeEstructuraTramo.IdArea
                            BeEstructura_ubicacionBR.IdBodega = BeEstructuraTramo.IdBodega

                            If tramo_orient = 0 Then
                                BeEstructura_ubicacionBR.IdUbicacion = tid - 1
                                BeEstructura_ubicacionBR.IdTramo = grp.IdTramo
                                BeEstructura_ubicacionBR.IdSector = BeEstructuraTramo.IdSector
                                BeEstructura_ubicacionBR.IdArea = BeEstructuraTramo.IdArea
                                BeEstructura_ubicacionBR.IdBodega = BeEstructuraTramo.IdBodega
                                BeEstructura_ubicacionBR.Descripcion = BeEstructura_ubicacion.Indice_x & "D" & BeEstructura_ubicacion.Nivel
                                BeEstructura_ubicacionBR.Orientacion_pos = "D"
                            Else
                                BeEstructura_ubicacionBR.Descripcion = BeEstructura_ubicacion.Indice_x & "C" & BeEstructura_ubicacion.Nivel
                                BeEstructura_ubicacionBR.Orientacion_pos = "C"
                            End If

                            listBeEstructuraUbicacion.Add(BeEstructura_ubicacionBR)

                    End Select

                Next

            Next

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        End Try

    End Function

    '#CKFK20240611 Voy a poner esta en comentario
    'Private Function Procesa_Grupo_Horiz(grp As clsBeEstructura_grupo) As Boolean

    '    Try
    '        nivel += 1
    '        ActualizarDimensiones(grp)

    '        For jj As Integer = 1 To grp.Cant
    '            tid += 1 : ucant += 1
    '            Dim it As clsBeEstructura_ubicacion = CrearUbicacion(grp, jj, "A")

    '            listBeEstructuraUbicacion.Add(it)
    '            AgregarUbicacionesAdicionales(grp, it)
    '        Next

    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '        Return False
    '    End Try
    'End Function

    Private Sub ActualizarDimensiones(grp As clsBeEstructura_grupo)
        Select Case grp.Orient
            Case 2
                grp.Ancho /= 2
            Case 3
                grp.Largo /= 2
            Case 4
                grp.Ancho /= 2
                grp.Largo /= 2
        End Select
    End Sub

    Private Function CrearUbicacion(grp As clsBeEstructura_grupo, indice As Integer, orientacion As String) As clsBeEstructura_ubicacion
        Dim it As New clsBeEstructura_ubicacion With {
        .IdUbicacion = tid,
        .IdTramo = grp.IdTramo,
        .IdSector = BeEstructuraTramo.IdSector,
        .IdArea = BeEstructuraTramo.IdArea,
        .IdBodega = BeEstructuraTramo.IdBodega,
        .Ancho = grp.Ancho,
        .Largo = grp.Largo,
        .Alto = grp.Alto,
        .nivel = nivel,
        .indice_x = IndX,
        .Acepta_pallet = grp.Palet = 1,
        .Descripcion = IndX & orientacion & nivel,
        .Orientacion_pos = orientacion,
        .IdIndiceRotacion = 1,
        .IdTipoRotacion = 1
    }
        Return it
    End Function

    Private Sub AgregarUbicacionesAdicionales(grp As clsBeEstructura_grupo, it As clsBeEstructura_ubicacion)
        Select Case grp.Orient
            Case 2, 3, 4
                ' Agregar lógica para agregar ubicaciones adicionales como itFR, itBL, itBR
                ' basado en la orientación
        End Select
    End Sub

    'Private Function Procesa_Grupo_Vert_(grp As clsBeEstructura_grupo) As Boolean

    '    Dim ii, jj, orien As Integer
    '    Dim it, itFR, itBL, itBR As clsBeEstructura_ubicacion
    '    Dim ancho, largo As Double

    '    Try

    '        For ii = 1 To grp.Cant

    '            indice_x += 1

    '            orien = grp.Orient
    '            ancho = grp.Ancho
    '            largo = grp.Largo

    '            Select Case orien
    '                Case 2
    '                    ancho = ancho / 2
    '                    largo = largo
    '                Case 3
    '                    ancho = ancho
    '                    largo = largo / 2
    '                Case 4
    '                    ancho = ancho / 2
    '                    largo = largo / 2
    '            End Select

    '            For jj = 1 To grp.Tamano

    '                tid += 1 : ucant += 1

    '                it = item.Clone
    '                it.IdUbicacion = tid
    '                it.IdTramo = grp.IdTramo
    '                it.IdSector = BeEstructuraTramo.IdSector
    '                it.IdArea = BeEstructuraTramo.IdArea
    '                it.IdBodega = BeEstructuraTramo.IdBodega
    '                it.Ancho = ancho
    '                it.Largo = largo
    '                it.Alto = grp.Alto
    '                it.Nivel = jj + grp.Offset
    '                it.Indice_x = indice_x
    '                it.Acepta_pallet = grp.Palet = 1

    '                If tid = 618 Then
    '                    Debug.Print("Espera")
    '                End If

    '                'it.Descripcion = it.Indice_x & "A" & it.Nivel
    '                If tramo_orient = 0 Then
    '                    it.IdUbicacion = tid + 1
    '                    it.IdTramo = grp.IdTramo
    '                    it.IdSector = BeEstructuraTramo.IdSector
    '                    it.IdArea = BeEstructuraTramo.IdArea
    '                    it.IdBodega = BeEstructuraTramo.IdBodega
    '                    it.Orientacion_pos = "A"
    '                    it.Descripcion = it.Indice_x & "A" & it.Nivel
    '                Else
    '                    it.Orientacion_pos = "A"
    '                    it.Descripcion = it.Indice_x & "A" & it.Nivel
    '                End If

    '                it.IdIndiceRotacion = 1
    '                it.IdTipoRotacion = 1

    '                listBeEstructuraUbicacion.Add(it)

    '                Select Case orien

    '                    Case 2

    '                        itFR = it.Clone : tid += 1 : ucant += 1
    '                        itFR.IdUbicacion = tid
    '                        itFR.IdTramo = grp.IdTramo
    '                        itFR.IdSector = BeEstructuraTramo.IdSector
    '                        itFR.IdArea = BeEstructuraTramo.IdArea
    '                        itFR.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itFR.Descripcion = it.Indice_x & "B" & it.Nivel
    '                            itFR.Orientacion_pos = "B"
    '                        Else
    '                            itFR.Descripcion = it.Indice_x & "B" & it.Nivel
    '                            itFR.Orientacion_pos = "B"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itFR)

    '                    Case 3

    '                        itBL = it.Clone : tid += 1 : ucant += 1
    '                        itBL.IdUbicacion = tid
    '                        itBL.IdTramo = grp.IdTramo
    '                        itBL.IdSector = BeEstructuraTramo.IdSector
    '                        itBL.IdArea = BeEstructuraTramo.IdArea
    '                        itBL.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itBL.Descripcion = it.Indice_x & "D" & it.Nivel
    '                            itBL.Orientacion_pos = "D"
    '                        Else
    '                            itBL.Descripcion = it.Indice_x & "C" & it.Nivel
    '                            itBL.Orientacion_pos = "C"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itBL)

    '                    Case 4

    '                        itFR = it.Clone : tid += 1 : ucant += 1
    '                        itFR.IdUbicacion = tid
    '                        itFR.IdTramo = grp.IdTramo
    '                        itFR.IdSector = BeEstructuraTramo.IdSector
    '                        itFR.IdArea = BeEstructuraTramo.IdArea
    '                        itFR.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itFR.IdUbicacion = tid - 1
    '                            itFR.IdTramo = grp.IdTramo
    '                            itFR.IdSector = BeEstructuraTramo.IdSector
    '                            itFR.IdArea = BeEstructuraTramo.IdArea
    '                            itFR.IdBodega = BeEstructuraTramo.IdBodega
    '                            itFR.Descripcion = it.Indice_x & "B" & it.Nivel
    '                            itFR.Orientacion_pos = "B"
    '                        Else
    '                            itFR.Descripcion = it.Indice_x & "B" & it.Nivel
    '                            itFR.Orientacion_pos = "B"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itFR)

    '                        itBL = it.Clone : tid += 1 : ucant += 1
    '                        itBL.IdUbicacion = tid
    '                        itBL.IdTramo = grp.IdTramo
    '                        itBL.IdSector = BeEstructuraTramo.IdSector
    '                        itBL.IdArea = BeEstructuraTramo.IdArea
    '                        itBL.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itBL.IdUbicacion = tid + 1
    '                            itBL.IdTramo = grp.IdTramo
    '                            itBL.IdSector = BeEstructuraTramo.IdSector
    '                            itBL.IdArea = BeEstructuraTramo.IdArea
    '                            itBL.IdBodega = BeEstructuraTramo.IdBodega
    '                            itBL.Descripcion = it.Indice_x & "D" & it.Nivel
    '                            itBL.Orientacion_pos = "D"
    '                        Else
    '                            itBL.Descripcion = it.Indice_x & "C" & it.Nivel
    '                            itBL.Orientacion_pos = "C"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itBL)

    '                        itBR = it.Clone : tid += 1 : ucant += 1
    '                        itBR.IdUbicacion = tid
    '                        itBR.IdTramo = grp.IdTramo
    '                        itBR.IdSector = BeEstructuraTramo.IdSector
    '                        itBR.IdArea = BeEstructuraTramo.IdArea
    '                        itBR.IdBodega = BeEstructuraTramo.IdBodega

    '                        If tramo_orient = 0 Then
    '                            itBR.IdUbicacion = tid - 1
    '                            itBR.IdTramo = grp.IdTramo
    '                            itBR.IdSector = BeEstructuraTramo.IdSector
    '                            itBR.IdArea = BeEstructuraTramo.IdArea
    '                            itBR.IdBodega = BeEstructuraTramo.IdBodega
    '                            itBR.Descripcion = it.Indice_x & "C" & it.Nivel
    '                            itBR.Orientacion_pos = "C"
    '                        Else
    '                            itBR.Descripcion = it.Indice_x & "D" & it.Nivel
    '                            itBR.Orientacion_pos = "D"
    '                        End If

    '                        listBeEstructuraUbicacion.Add(itBR)

    '                End Select

    '            Next

    '        Next

    '        Return True

    '    Catch ex As Exception
    '        Throw ex
    '        Return False
    '    End Try

    'End Function

    '#CKFK20240611 Estoy poniendo esto en comentario Procesa_Grupo_Vert en comentario
    'Private Function Procesa_Grupo_Vert(grp As clsBeEstructura_grupo) As Boolean

    '    Try

    '        Dim tid As Integer = 0
    '        'Dim ucant As Integer = 0
    '        Dim indice_x As Integer = 0
    '        Dim tramo_orient As Integer = 0
    '        'Dim listBeEstructuraUbicacion As New List(Of clsBeEstructura_ubicacion)

    '        For i = 1 To grp.Cant

    '            indice_x += 1

    '            Dim ancho As Double = grp.Ancho
    '            Dim largo As Double = grp.Largo

    '            Select Case grp.Orient
    '                Case 2
    '                    ancho /= 2
    '                Case 3
    '                    largo /= 2
    '                Case 4
    '                    ancho /= 2
    '                    largo /= 2
    '            End Select

    '            For j = 1 To grp.Tamano
    '                tid += 1
    '                ucant += 1

    '                Dim it As New clsBeEstructura_ubicacion()
    '                it.IdUbicacion = tid
    '                it.IdTramo = grp.IdTramo
    '                it.IdSector = BeEstructuraTramo.IdSector
    '                it.IdArea = BeEstructuraTramo.IdArea
    '                it.IdBodega = BeEstructuraTramo.IdBodega
    '                it.Ancho = ancho
    '                it.Largo = largo
    '                it.Alto = grp.Alto
    '                it.Nivel = j + grp.Offset
    '                it.Indice_x = indice_x
    '                it.Acepta_pallet = grp.Palet = 1
    '                it.Orientacion_pos = If(tramo_orient = 0, "A", "B")

    '                Select Case grp.Orient
    '                    Case 3
    '                        it.Orientacion_pos = If(tramo_orient = 0, "D", "C")
    '                    Case 4
    '                        it.Orientacion_pos = If(tramo_orient = 0, "B", "C")
    '                End Select

    '                it.Descripcion = it.Indice_x & it.Orientacion_pos & it.Nivel
    '                it.IdIndiceRotacion = 1
    '                it.IdTipoRotacion = 1

    '                listBeEstructuraUbicacion.Add(it)
    '            Next

    '        Next

    '        Return True

    '    Catch ex As Exception
    '        Throw New Exception($"{MethodBase.GetCurrentMethod.Name()} {ex.Message}")
    '    End Try

    'End Function

    Private Function Inserta_Estructura(ByVal lConnection As SqlConnection,
                                        ByVal lTransaction As SqlTransaction) As Boolean

        Dim vInserta_Estructura As Boolean = False
        Dim ListBeEstructuraUbic As New List(Of clsBeEstructura_ubicacion)

        GroupBox2.Visible = True
        pBar.Refresh() : Application.DoEvents()

        Try

            listBeTramos.Clear()
            listBeUbicaciones.Clear()
            creaBItem()

            For Each BeEstructuraTramo As clsBeEstructura_tramo In listBeEstructuraTramo

                Llena_Tramo(BeEstructuraTramo, lConnection, lTransaction)
                listBeTramos.Add(btramo)

                ListBeEstructuraUbic = clsLnEstructura_ubicacion.Get_All_By_IdTramo(BeEstructuraTramo.IdTramo,
                                                                                    lConnection,
                                                                                    lTransaction)

                For Each BeEstructuraUbicacion As clsBeEstructura_ubicacion In ListBeEstructuraUbic
                    BeBodegaUbicacion = New clsBeBodega_ubicacion
                    Llena_Ubicacion(BeEstructuraUbicacion)
                    listBeUbicaciones.Add(BeBodegaUbicacion)
                Next

                Application.DoEvents()

            Next

            Application.DoEvents()

            vInserta_Estructura = clsLnBodega_ubicacion.Importar_Estructura(IdBodega,
                                                                            listBeTramos,
                                                                            listBeUbicaciones,
                                                                            lConnection,
                                                                            lTransaction)

        Catch ex As Exception
            Throw ex
        End Try

        Return vInserta_Estructura

    End Function

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick

        Try

            If clsLnUsuario.Rol_Administrador(AP.UsuarioAp, IdBodega) Then

                If MessageBox.Show("Ésta operacion borrará la estructura actual. ¿Continuar? ", "", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return
                If MessageBox.Show("¿Está seguro?", "", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

                clsLnBodega_ubicacion.Borrar_Estructura_Actual(IdBodega)

                XtraMessageBox.Show("Estructura borrada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else

                XtraMessageBox.Show("El usuario no tiene rol de admninistrador, no puede borrar la estructura", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

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

    Private Sub Llena_Ubicacion(ub As clsBeEstructura_ubicacion)

        BeBodegaUbicacion = bitem.Clone

        With BeBodegaUbicacion

            .IdUbicacion = ub.IdUbicacion
            .IdTramo = ub.IdTramo
            .IdSector = ub.IdSector
            .IdArea = ub.IdArea
            .IdBodega = ub.IdBodega
            .Descripcion = ub.Descripcion
            .Ancho = ub.Ancho
            .Largo = ub.Largo
            .Alto = ub.Alto
            .Nivel = ub.Nivel
            .Indice_x = ub.Indice_x
            .Acepta_pallet = ub.Acepta_pallet
            .Orientacion_pos = ub.Orientacion_pos
            .IdTipoRotacion = ub.IdTipoRotacion
            .IdIndiceRotacion = ub.IdIndiceRotacion

        End With

    End Sub

    Private Sub mnuDisenno_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDisenno.ItemClick

        Try

            Using GrafBod As New frmDiseñoB() With {.IdEmpresa = AP.IdEmpresa, .IdBodega = IdBodega}
                GrafBod.ShowDialog(Me)
            End Using

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmEstGeneracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Llena_Tramo(ByVal tr As clsBeEstructura_tramo,
                            ByVal lConnection As SqlConnection,
                            ByVal lTransaction As SqlTransaction)

        Try

            Dim lgrupo As New List(Of clsBeEstructura_grupo)
            btramo = New clsBeBodega_tramo()
            btramo.IdTramo = tr.IdTramo
            btramo.IdSector = tr.IdSector
            btramo.Sistema = tr.Sistema
            btramo.Descripcion = tr.Descripcion
            btramo.User_agr = tr.User_agr
            btramo.Fec_agr = tr.Fec_agr
            btramo.User_mod = tr.User_mod
            btramo.Fec_mod = tr.Fec_mod
            btramo.Activo = tr.Activo
            btramo.Alto = tr.Alto
            btramo.Largo = tr.Largo
            btramo.Ancho = tr.Ancho
            btramo.Margen_izquierdo = tr.Margen_izquierdo
            btramo.Margen_derecho = tr.Margen_derecho
            btramo.Margen_superior = tr.Margen_superior
            btramo.Margen_inferior = tr.Margen_inferior
            btramo.Codigo = tr.Codigo
            btramo.Indice_x = tr.Indice_x
            btramo.Orientacion = tr.Orientacion
            btramo.IdTipoProductoDefault = tr.IdTipoProductoDefault
            btramo.Es_Rack = Not tr.Sistema
            btramo.Horizontal = tr.Horizontal
            btramo.IdArea = tr.IdArea
            btramo.IdBodega = tr.IdBodega
            btramo.Orden_Descendente = tr.Orden_Descendente

            lgrupo = clsLnEstructura_grupo.Get_All_By_IdBodega_And_IdTramo(tr.IdBodega,
                                                                           tr.IdTramo,
                                                                           lConnection,
                                                                           lTransaction)

            btramo.IdTipoRack = 1

            If Not lgrupo Is Nothing Then
                If lgrupo.Count > 0 Then
                    btramo.IdTipoRack = lgrupo(0).Orient
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

#End Region

#Region " Eventos "

    Private Sub btnGenerar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGenerar.ItemClick

        Dim vBD As New clsTransaccion()

        Try

            Dim rslt As Boolean
            btnGenerar.Enabled = False

            If MessageBox.Show("Esta operación reescribe la estructura actual. Continuar ? ", "", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

            vBD.Begin_Transaction()

            clsLnBodega_ubicacion.Eliminar_Bodega_Ubicacion(IdBodega, vBD.lConnection, vBD.lTransaction)

            Mostrar_Estructura(vBD.lConnection, vBD.lTransaction)

            If Not Crea_Item() Then
                vBD.Commit_Transaction()
                Return
            End If

            clsLnEstructura_ubicacion.EliminarTodos(vBD.lConnection, vBD.lTransaction)

            If Procesar_Sectores(vBD.lConnection, vBD.lTransaction) Then
                rslt = Inserta_Estructura(vBD.lConnection, vBD.lTransaction)
            End If

            GroupBox1.Visible = False : GroupBox2.Visible = False

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Esperando estructuras asíncronas...")

            vBD.Commit_Transaction()

            If rslt Then
                lblCreandoUbicaciones.Text = " Trasladando estructuras para: " & listBeUbicaciones.Count & " ubicaciones."
                lblCreandoUbicaciones.Refresh()
                Using GrafBod As New frmDiseñoB() With {.IdEmpresa = AP.IdEmpresa, .IdBodega = IdBodega}
                    GrafBod.CantidadUbicaciones = listBeUbicaciones.Count
                    GrafBod.ShowDialog(Me)
                End Using
                'MsgBox(String.Format("Fueron creadas {0} ubicaciones, generación completa.", listBeUbicaciones.Count))
            End If

        Catch ex As Exception
            vBD.RollBack_Transaction()
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            btnGenerar.Enabled = True
            vBD.Close_Conection()
        End Try

    End Sub

#End Region

#Region " Aux "

    Private Sub Mostrar_Estructura(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim dts As New DataTable
        'Dim Lista_Tramos As New List(Of clsBeEstructura_tramo)
        Dim Lista_Tramos As New DataTable
        Dim NodoSector, TreeNod As TreeNode
        Dim si, ti, mx, my, smx As Integer
        Dim IdSector, IdTramo As Integer
        Dim vIdTramo As Integer = 0
        Dim vNombreTramo As String = ""

        tvData.Nodes.Clear() : tcant = 0

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Actualizando estructuras...")

        If (lConnection Is Nothing And lTransaction Is Nothing) Then
            lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
        End If

        Try

            dts = clsLnBodega_sector.Get_All_Sector_By_IdBodega(IdBodega, lConnection, lTransaction)

            For si = 0 To dts.Rows.Count - 1

                IdSector = dts.Rows(si).Item("idsector")
                Lista_Tramos = clsLnEstructura_Tramo.Get_All_By_IdBodega_And_IdSector_DT(IdBodega,
                                                                                         IdSector,
                                                                                         lConnection,
                                                                                         lTransaction)
                NodoSector = tvData.Nodes.Add(dts.Rows(si).Item("descripcion")) : NodoSector.Tag = IdSector
                NodoSector.ImageIndex = 0 : NodoSector.SelectedImageIndex = 0

                smx = 0

                For ti = 0 To Lista_Tramos.Rows.Count - 1

                    IdTramo = Lista_Tramos.Rows(ti).Item("IdTramo")
                    TreeNod = NodoSector.Nodes.Add(IIf(IsDBNull(Lista_Tramos(ti).Item("Descripcion")), "N/D", Lista_Tramos(ti).Item("Descripcion"))) : TreeNod.Tag = IdTramo
                    TreeNod.ImageIndex = 1 : TreeNod.SelectedImageIndex = 1

                    Estructura_Tramo(IdBodega, IdTramo, mx, my, lConnection, lTransaction) : tcant += 1

                    If (mx * my <> 0) Then
                        TreeNod.Text = TreeNod.Text & " - [ " & mx & " x " & my & " ]"
                    End If

                    smx += mx

                    Application.DoEvents()

                Next

                NodoSector.Text = NodoSector.Text & " - [ tramos : " & Lista_Tramos.Rows.Count & " , posiciones : " & smx & " ]"

                If NodoSector.Nodes.Count = 0 Then
                    NodoSector.ImageIndex = 5 : NodoSector.SelectedImageIndex = 5
                End If

                NodoSector.Expand()

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

        tvData.Refresh()

        Application.DoEvents()

    End Sub

    Private Sub Estructura_Tramo(ByVal IdBodega As Integer,
                                 ByVal IdTramo As Integer,
                                 ByRef mx As Integer,
                                 ByRef my As Integer,
                                 ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction)

        Dim grupos As New List(Of clsBeEstructura_grupo)
        Dim ii, vx, vy, vagrup As Integer

        mx = 0 : my = 0

        Try

            grupos = clsLnEstructura_grupo.Get_All_By_IdBodega_And_IdTramo(IdBodega, IdTramo, lConnection, lTransaction)

            If grupos.Count = 0 Then Return

            vagrup = grupos(0).Agrupacion

            For ii = 0 To grupos.Count - 1

                If vagrup = 1 Then
                    vx = grupos(ii).Cant
                    vy = grupos(ii).Tamano + grupos(ii).Offset
                    mx += vx
                    If (vy > my) Then my = vy
                Else
                    vx = grupos(ii).Tamano + grupos(ii).Offset
                    vy = grupos(ii).Cant
                    If (vx > mx) Then mx = vx
                    my += vy
                End If

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub actualizaProgress()
        lblPrg.Text = pcant & " / " & tcant
        lblPrg.Refresh()
        Try
            pBar.Value = pcant
            pBar.Refresh()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

        Application.DoEvents()
    End Sub

    Private Function Crea_Item() As Boolean

        Try

            With item
                .IdUbicacion = 0
                .IdTramo = 0
                .Descripcion = ""
                .Ancho = 0
                .Largo = 0
                .Alto = 0
                .Nivel = 0
                .Indice_x = 0
                .Acepta_pallet = False
                .Orientacion_pos = ""
                .IdIndiceRotacion = Nothing
                .IdTipoRotacion = 0
                .Sistema = False
                .Codigo_barra = ""
                .Codigo_barra2 = ""
                .User_agr = AP.UsuarioAp.IdUsuario
                .Fec_agr = Date.Now
                .User_mod = AP.UsuarioAp.IdUsuario
                .Fec_mod = Date.Now
                .Dañado = False
                .Activo = True
                .Bloqueada = False
                .Ubicacion_picking = False
                .Ubicacion_recepcion = False
                .Ubicacion_despacho = False
                .Ubicacion_merma = False
                .Margen_izquierdo = 0
                .Margen_derecho = 0
                .Margen_superior = 0
                .Margen_inferior = 0
                .Ubicacion_muelle = False
                .Posicion_X = 0
                .Posicion_Y = 0
            End With

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function creaBItem() As Boolean

        Try
            With bitem
                .IdUbicacion = 0
                .IdTramo = 0
                .Descripcion = ""
                .Ancho = 0
                .Largo = 0
                .Alto = 0
                .Nivel = 0
                .Indice_x = 0
                .Acepta_pallet = False
                .Orientacion_pos = ""

                .IdIndiceRotacion = Nothing
                .IdTipoRotacion = 0
                .Sistema = False
                .Codigo_barra = ""
                .Codigo_barra2 = ""
                .User_agr = AP.UsuarioAp.IdUsuario
                .Fec_agr = Date.Now
                .User_mod = AP.UsuarioAp.IdUsuario
                .Fec_mod = Date.Now
                .Dañado = False
                .Activo = True
                .Bloqueada = False
                .Ubicacion_picking = False
                .Ubicacion_recepcion = False
                .Ubicacion_despacho = False
                .Ubicacion_merma = False
                .Margen_izquierdo = 0
                .Margen_derecho = 0
                .Margen_superior = 0
                .Margen_inferior = 0
                .Ubicacion_muelle = False
                .Posicion_X = 0
                .Posicion_Y = 0
            End With

            Return True
        Catch ex As Exception
            MsgBox(ex.Message) : Return False
        End Try

    End Function


    Private Sub frmEstGeneracion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            btnGenerar.Enabled = False
            GroupBox1.Visible = False
            GroupBox2.Visible = False

            Mostrar_Estructura(Nothing, Nothing)

            btnGenerar.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)
        End Try

    End Sub


#End Region

End Class