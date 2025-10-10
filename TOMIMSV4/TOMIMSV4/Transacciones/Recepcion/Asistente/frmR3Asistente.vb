Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository

Public Class frmR3Asistente

    Public pObjTR As clsBeTrans_re_tr

    Private pIngreso As Boolean
    Private pHandHeld As Boolean
    Private pReferencia As Boolean
    Private l As New List(Of clsBeTrans_re_tr)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdCancelar_Click(sender As Object, e As EventArgs) Handles cmdCancelar.Click
        Close()
    End Sub

    Private Sub cmdSiguiente_Click(sender As Object, e As EventArgs) Handles cmdSiguiente.Click

        Try

            If GrpTipoTR.Visible Then

                If rdIngreso.Checked = False And rdMotivoDevolucion.Checked = False Then
                    Throw New Exception("Seleccione un tipo de transacción.")
                Else
                    If rdIngreso.Checked Then
                        pIngreso = True
                    Else
                        pIngreso = False
                    End If
                    GrpTipoTR.Visible = False
                    GrpHH.Location = New Point(35, 97)
                    GrpHH.Visible = True
                    PicBack.Visible = True
                    lblDescripcion.Text = "HandHeld"
                End If

            ElseIf GrpHH.Visible Then

                If rdConHH.Checked = False And rdSinHH.Checked = False Then
                    Throw New Exception("Seleccione con o sin HandHeld.")
                Else
                    If rdConHH.Checked Then
                        pHandHeld = True
                    Else
                        pHandHeld = False
                    End If
                    GrpHH.Visible = False
                    GrpReferencia.Location = New Point(35, 97)
                    GrpReferencia.Visible = True
                    lblDescripcion.Text = "Referencia"
                End If

            ElseIf GrpReferencia.Visible Then

                If rdConReferencia.Checked = False And rdSinReferencia.Checked = False Then
                    Throw New Exception("Seleccione con o sin Referencia.")
                Else
                    If rdConReferencia.Checked Then
                        pReferencia = True
                    Else
                        pReferencia = False
                    End If
                    GrpReferencia.Visible = False
                    GrpListado.Location = New Point(35, 97)
                    GrpListado.Visible = True
                    lblDescripcion.Text = "Listado de Transacciones"
                    ListaTransaccion()
                End If

            ElseIf GrpListado.Visible Then

                If l.All(Function(b) b.Checked = False) Then
                    Throw New Exception("Seleccione un Tipo de Transacción.")
                Else
                    pObjTR = l.Find(Function(b) b.Checked = True)
                    Close()
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub ListaTransaccion()

        DsAsistente.Clear()

        Try

            'l = clsLnTrans_re_tr.GetAll(pIngreso, pHandHeld, pReferencia)

            Grid.BeginUpdate()

            Dim lIndex As Integer = 0

            For Each bo As clsBeTrans_re_tr In l

                bo.Index += lIndex
                Dim lRow As DataRow = DsAsistente.DT.NewRow
                lRow.Item("colTipo") = bo.Descripcion.Trim
                lRow.Item("colSeleccion") = False
                DsAsistente.DT.AddDTRow(lRow)
                lIndex += 1

            Next

            Grid.EndUpdate()
            Grid.ForceInitialize()
            Dim ritem As RepositoryItemCheckEdit = TryCast(gridView1.Columns("colSeleccion").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As DevExpress.XtraEditors.CheckEdit = TryCast(sender, DevExpress.XtraEditors.CheckEdit)
            If ritem IsNot Nothing Then

                Dim Dr As DataRowView = gridView1.GetFocusedRow
                Dim lIndex As Integer = -1
                Dim str As String = CStr(Dr.Item("colTipo"))
                lIndex = l.FindIndex(Function(b) b.Descripcion = str)

                If ritem.Checked Then
                    For Each Obj As clsBeTrans_re_tr In l
                        If Obj.Index = lIndex Then
                            Obj.Checked = True
                        Else
                            Obj.Checked = False
                        End If
                    Next
                    If l.Count > 0 Then
                        DsAsistente.Clear()
                        Grid.BeginUpdate()
                        For Each Obj As clsBeTrans_re_tr In l
                            Dim lRow As DataRow = DsAsistente.DT.NewRow
                            lRow.Item("colSeleccion") = Obj.Checked
                            lRow.Item("colTipo") = Obj.Descripcion
                            DsAsistente.DT.AddDTRow(lRow)
                        Next
                        Grid.EndUpdate()
                        Grid.ForceInitialize()
                    End If
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub PicBack_Click(sender As Object, e As EventArgs) Handles PicBack.Click

        Try

            If GrpListado.Visible Then

                GrpListado.Visible = False
                GrpReferencia.Location = New Point(35, 97)
                GrpReferencia.Visible = True
                lblDescripcion.Text = "Referencia"

            ElseIf GrpReferencia.Visible Then

                GrpReferencia.Visible = False
                GrpHH.Location = New Point(35, 97)
                GrpHH.Visible = True
                lblDescripcion.Text = "HandHeld"

            ElseIf GrpHH.Visible Then

                GrpHH.Visible = False
                GrpTipoTR.Location = New Point(35, 97)
                GrpTipoTR.Visible = True
                PicBack.Visible = False
                lblDescripcion.Text = "Tipo Transacción"

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub frmR3Asistente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PicBack.Visible = False
    End Sub
End Class