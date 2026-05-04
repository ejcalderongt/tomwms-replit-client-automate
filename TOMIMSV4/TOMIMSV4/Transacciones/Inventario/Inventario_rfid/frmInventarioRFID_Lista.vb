Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmInventarioRFID_Lista
    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdNuevos_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdNuevos.ItemClick
        Try
            Nuevo_Inventario()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Nuevo_Inventario()

        Try

            Cierra_Instancia_Previa(frmInventarioRFID)

            With frmInventarioRFID
                .Modo = frmInventarioRFID.TipoTrans.Nuevo
                .MdiParent = MdiParent
                '.InvokeListarInventario = AddressOf Cargar
                .WindowState = FormWindowState.Maximized

                'If OpcionesMenu IsNot Nothing Then
                '    .OpcionesMenu = OpcionesMenu
                '    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                '    .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                '    .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                'End If

                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmInventarioRFID_Lista_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            Cargar()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Sub Cargar()

        Try

            grdInventario.DataSource = Nothing

            Dim lista As New List(Of clsBeTrans_inv_enc)
            Dim gBeTransInvEnc As New clsBeTrans_inv_enc
            Dim listaInvDet As New List(Of clsBeTrans_inv_detalle)

            lista = clsLnTrans_inv_enc.Listar_Inventarios_RFID_By_Rango_Fechas(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date, AP.IdBodega, True)

            For Each objP As clsBeTrans_inv_enc In lista

                listaInvDet = clsLnTrans_inv_detalle.Get_All_By_IdInventarioEnc(objP.Idinventarioenc)

                If listaInvDet.Count > 0 And objP.Estado = "Nuevo" Then

                    objP.Estado = "Procesando"
                    clsLnTrans_inv_enc.Actualizar(objP)

                End If

            Next

            '#EJC20180813_0929AM: Porqué se vuelve a llenar la lista de inventario por fecha si arriba se llena?
            'lista = clsLnTrans_inv_enc.Get_All_By_Rango_Fechas(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date)

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("InventarioLista")
                DT.Columns.Add(("Código"), GetType(Integer))
                DT.Columns.Add(("Propietario"), GetType(String))
                DT.Columns.Add(("Bodega"), GetType(String))
                DT.Columns.Add(("Tipo Inventario"), GetType(String))
                DT.Columns.Add(("Tipo Conteo"), GetType(String))
                DT.Columns.Add(("Doble Verificación"), GetType(Boolean))
                DT.Columns.Add(("Incial"), GetType(Boolean))
                DT.Columns.Add(("Regularizado"), GetType(Boolean))
                DT.Columns.Add(("Estado"), GetType(String))
                DT.Columns.Add(("Fecha"), GetType(Date))
                DT.Columns.Add(("HoraInicio"), GetType(TimeSpan))
                DT.Columns.Add(("HoraFin"), GetType(TimeSpan))

                For Each Obj As clsBeTrans_inv_enc In lista

                    DT.Rows.Add(Obj.Idinventarioenc, Obj.Propietario.Nombre_comercial, Obj.Bodega.Nombre, Obj.TipoInv.Descripcion, Obj.TipoConteo.Descripcion, Obj.Doble_verificacion,
                                Obj.Inicial, Obj.Regularizado, Obj.Estado, Obj.Fecha, Obj.Hora_ini.TimeOfDay, Obj.Hora_fin.TimeOfDay)
                Next

                grdInventario.DataSource = DT

            End If

            lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            Try
                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns()
            Catch ex As Exception
            End Try

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class