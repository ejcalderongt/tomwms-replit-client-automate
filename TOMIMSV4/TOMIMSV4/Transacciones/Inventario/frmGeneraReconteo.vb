Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class frmGeneraReconteo

    Public ListInventarioCiclico As New List(Of clsBeTrans_inv_ciclico)
    Public pIdInventario As Integer = 0

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

    Dim IdInventarioEnc As Integer = 0
    Private Function Generar_Reconteo() As Boolean

        Generar_Reconteo = False

        Try

            Dim Detalle As New clsBeTrans_inv_reconteo
            Dim items As New List(Of clsBeTrans_inv_reconteo)
            Dim MaxId As Integer = clsLnTrans_inv_reconteo.MaxID() + 1

            If Insertar_Enc_Reconteo() Then

                For Each Obj As TreeListNode In tlReConteo.Nodes

                    If Obj.Checked Then

                        Detalle = New clsBeTrans_inv_reconteo

                        Detalle.Idinvreconteo = MaxId
                        Detalle.Idreconteo = IdInventarioEnc
                        Detalle.Idinvciclico = Obj.Item("IdCiclico")
                        Detalle.Idinventarioenc = pIdInventario
                        Detalle.IdStock = Obj.Item("IdStock")
                        Detalle.IdProductoBodega = Obj.Item("IdProductoBodega")
                        Detalle.IdProductoEstado = Obj.Item("IdProductoEstado")
                        Detalle.IdPresentacion = Obj.Item("IdPresentacion")
                        Detalle.IdUbicacionAnterior = Obj.Item("IdUbicacion")
                        Detalle.IdUbicacion = Obj.Item("IdUbicacion")
                        Detalle.EsNuevo = Obj.Item("EsNuevo")
                        Detalle.Fecha_vence = Obj.Item("Fecha_Vence")
                        Detalle.Lote = Obj.Item("Lote")
                        Detalle.CantidadAnterior = Obj.Item("Cantidad_Conteo")
                        Detalle.Cantidad = 0
                        Detalle.PesoAnterior = Obj.Item("Peso_Conteo")
                        Detalle.Peso = 0
                        Detalle.User_agr = ""
                        Detalle.Fec_agr = Now
                        Detalle.IdOperador = Obj.Item("IdOperador")
                        Detalle.EsPallet = Obj.Item("EsPallet")
                        Detalle.lic_plate = Obj.Item("Lic_Plate")

                        items.Add(Detalle)

                        MaxId += 1

                    End If

                Next

                clsLnTrans_inv_reconteo.Guardar_Reconteo(Encabezado, items)

                Generar_Reconteo = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Dim Encabezado As New clsBeTrans_inv_enc_reconteo
    Private Function Insertar_Enc_Reconteo() As Boolean

        Insertar_Enc_Reconteo = False

        Try

            Encabezado.Idinvencreconteo = clsLnTrans_inv_enc_reconteo.MaxID + 1
            IdInventarioEnc = Encabezado.Idinvencreconteo
            Encabezado.Idinventarioenc = pIdInventario
            Encabezado.Reconteo = clsLnTrans_inv_enc_reconteo.MaxReconteo(pIdInventario) + 1
            Encabezado.Estado = "Nuevo"
            Encabezado.Hora_ini = Now
            Encabezado.Hora_fin = Now
            Encabezado.User_agr = AP.UsuarioAp.Nombres
            Encabezado.Fec_agr = Now
            Encabezado.User_mod = AP.UsuarioAp.Nombres
            Encabezado.Fec_mod = Now

            Insertar_Enc_Reconteo = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub frmReConteo_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    LlenaTreeList()

            End Select

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub LlenaTreeList()

        tlReConteo.ClearNodes()

        Crea_EncabezadoReconteo(tlReConteo)
        Crea_DetalleReconteo(tlReConteo)

    End Sub

    Private Sub Crea_EncabezadoReconteo(ByRef tlO As TreeList)

        Try

            tlO.BeginUnboundLoad()
            tlO.Columns.Add()
            tlO.Columns(0).Caption = "Ubicación"
            tlO.Columns(0).VisibleIndex = 0
            tlO.Columns(0).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(1).Caption = "Tramo"
            tlO.Columns(1).VisibleIndex = 1
            tlO.Columns(1).Visible = False
            tlO.Columns(1).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(2).Caption = "IdStock"
            tlO.Columns(2).VisibleIndex = 2
            tlO.Columns(2).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(3).Caption = "Código"
            tlO.Columns(3).VisibleIndex = 3
            tlO.Columns(3).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(4).Caption = "Producto"
            tlO.Columns(4).VisibleIndex = 4
            tlO.Columns(4).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(5).Caption = "Presentación"
            tlO.Columns(5).VisibleIndex = 5
            tlO.Columns(5).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(6).Caption = "Lote"
            tlO.Columns(6).VisibleIndex = 6
            tlO.Columns(6).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(7).Caption = "Fecha_Vence"
            tlO.Columns(7).VisibleIndex = 7
            tlO.Columns(7).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(8).Caption = "Operador"
            tlO.Columns(8).VisibleIndex = 8
            tlO.Columns(8).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(9).Caption = "Estado"
            tlO.Columns(9).VisibleIndex = 9
            tlO.Columns(9).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(10).Caption = "Cantidad_Stock"
            tlO.Columns(10).VisibleIndex = 10
            tlO.Columns(10).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(11).Caption = "Peso_Stock"
            tlO.Columns(11).VisibleIndex = 11
            tlO.Columns(11).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(12).Caption = "Cantidad_Conteo"
            tlO.Columns(12).VisibleIndex = 12
            tlO.Columns(12).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(13).Caption = "Peso_Conteo"
            tlO.Columns(13).VisibleIndex = 13
            tlO.Columns(13).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(14).Caption = "Cantidad_Reconteo"
            tlO.Columns(14).VisibleIndex = 14
            tlO.Columns(14).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(15).Caption = "Peso_Reconteo"
            tlO.Columns(15).VisibleIndex = 15
            tlO.Columns(15).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(16).Caption = "IdUbicacion"
            tlO.Columns(16).VisibleIndex = 16
            tlO.Columns(16).Visible = False
            tlO.Columns(16).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(17).Caption = "IdCiclico"
            tlO.Columns(17).VisibleIndex = 17
            tlO.Columns(17).Visible = False
            tlO.Columns(17).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(18).Caption = "IdProductoBodega"
            tlO.Columns(18).VisibleIndex = 18
            tlO.Columns(18).Visible = False
            tlO.Columns(18).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(19).Caption = "IdProductoEstado"
            tlO.Columns(19).VisibleIndex = 19
            tlO.Columns(19).Visible = False
            tlO.Columns(19).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(20).Caption = "IdPresentacion"
            tlO.Columns(20).VisibleIndex = 20
            tlO.Columns(20).Visible = False
            tlO.Columns(20).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(21).Caption = "IdOperador"
            tlO.Columns(21).VisibleIndex = 21
            tlO.Columns(21).Visible = False
            tlO.Columns(21).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(22).Caption = "EsNuevo"
            tlO.Columns(22).VisibleIndex = 22
            tlO.Columns(22).Visible = False
            tlO.Columns(22).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(23).Caption = "EsPallet"
            tlO.Columns(23).VisibleIndex = 23
            tlO.Columns(23).Visible = False
            tlO.Columns(23).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.Columns(24).Caption = "Lic_Plate"
            tlO.Columns(24).VisibleIndex = 24
            tlO.Columns(24).Visible = False
            tlO.Columns(24).FilterMode = ColumnFilterMode.DisplayText
            tlO.Columns.Add()
            tlO.EndUnboundLoad()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Crea_DetalleReconteo(ByRef tl As TreeList)

        Try

            tl.BeginUnboundLoad()

            If ListInventarioCiclico.Count > 0 Then

                Dim parentForRootNodes As TreeListNode = Nothing

                Dim rootNode As TreeListNode

                'Çiclo
                For Each Obj As clsBeTrans_inv_ciclico In ListInventarioCiclico

                    rootNode = tl.AppendNode(New Object() {Obj.Ubicacion, Obj.Tramo, Obj.IdStock, Obj.Codigo, Obj.Producto, Obj.Presentacion, Obj.Lote,
                                  Obj.Fecha_vence, Obj.Operador, Obj.Estado, Obj.Cant_stock, Obj.Peso_stock, Obj.Cantidad, Obj.Peso, Obj.Cant_reconteo,
                                  Obj.Peso_reconteo, Obj.IdUbicacion, Obj.IdInvCiclico, Obj.IdProductoBodega, Obj.IdProductoEstado, Obj.IdPresentacion, Obj.Idoperador,
                                  Obj.EsNuevo, Obj.EsPallet, Obj.lic_plate}, parentForRootNodes)

                    rootNode.Expanded = True

                Next

            End If

            tlReConteo.BestFitColumns()
            tl.EndUnboundLoad()

            tlReConteo.OptionsView.ShowSummaryFooter = True
            tlReConteo.Columns(0).AllNodesSummary = True
            tlReConteo.Columns(0).SummaryFooterStrFormat = "Productos: {0:n0}"
            tlReConteo.Columns(0).SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Count

            tlReConteo.Columns(10).SummaryFooterStrFormat = "{0:n2}"
            tlReConteo.Columns(10).SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum

            tlReConteo.Columns(11).SummaryFooterStrFormat = "{0:n2}"
            tlReConteo.Columns(11).SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum

            tlReConteo.Columns(12).SummaryFooterStrFormat = "{0:n2}"
            tlReConteo.Columns(12).SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum

            tlReConteo.Columns(13).SummaryFooterStrFormat = "{0:n2}"
            tlReConteo.Columns(13).SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum

            tlReConteo.Columns(14).SummaryFooterStrFormat = "{0:n2}"
            tlReConteo.Columns(14).SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum

            tlReConteo.Columns(15).SummaryFooterStrFormat = "{0:n2}"
            tlReConteo.Columns(15).SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum

            tlReConteo.Columns(10).Format.FormatType = DevExpress.Utils.FormatType.Numeric
            tlReConteo.Columns(10).Format.FormatString = "{0:n2}"

            tlReConteo.Columns(11).Format.FormatType = DevExpress.Utils.FormatType.Numeric
            tlReConteo.Columns(11).Format.FormatString = "{0:n2}"

            tlReConteo.Columns(12).Format.FormatType = DevExpress.Utils.FormatType.Numeric
            tlReConteo.Columns(12).Format.FormatString = "{0:n2}"

            tlReConteo.Columns(13).Format.FormatType = DevExpress.Utils.FormatType.Numeric
            tlReConteo.Columns(13).Format.FormatString = "{0:n2}"

            tlReConteo.Columns(14).Format.FormatType = DevExpress.Utils.FormatType.Numeric
            tlReConteo.Columns(14).Format.FormatString = "{0:n2}"

            tlReConteo.Columns(15).Format.FormatType = DevExpress.Utils.FormatType.Numeric
            tlReConteo.Columns(15).Format.FormatString = "{0:n2}"

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tlReConteo_AfterCheckNode(sender As Object, e As NodeEventArgs) Handles tlReConteo.AfterCheckNode

        Try

            Dim ND = tlReConteo.FocusedNode

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

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        If Generar_Reconteo() Then
            XtraMessageBox.Show("Reconteo Generado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub

    Private Sub Eliminar()

        Try

            For Each Obj As TreeListNode In tlReConteo.Nodes

                If Obj.Checked Then

                    ListInventarioCiclico.RemoveAll(Function(x) x.Idoperador = Obj.Item("IdOperador") And x.IdStock = Obj.Item("IdStock"))

                End If

            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick
        Eliminar()
        LlenaTreeList()
    End Sub

    Private Sub Actualizar()

    End Sub

End Class