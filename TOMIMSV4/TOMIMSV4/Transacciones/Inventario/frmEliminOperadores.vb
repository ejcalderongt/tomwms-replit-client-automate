Imports DevExpress.XtraEditors

Public Class frmEliminOperadores

    Public IdUbicacion As Integer
    Public IdInventario As Integer

    Public gBeCiclico As New clsBeTrans_inv_ciclico
    Public gBeReconteo As New clsBeTrans_inv_reconteo

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
        Eliminar = 3
        Reconteo = 4
        NoStock = 5
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmEliminOperadores_Load(sender As Object, e As EventArgs) Handles Me.Load

        Select Case Modo

            Case TipoTrans.Nuevo
                cargar()
                cmdAsignarOperador.Visible = False
            Case TipoTrans.Editar
                cargar()
                Me.Text = "Asignación de Operadores"
                cmdEliminar.Visible = False
            Case TipoTrans.Eliminar
                cargar()
                Me.Text = "Eliminar Operadores"
                cmdAsignarOperador.Visible = False
            Case TipoTrans.Reconteo
                cargar()
                Me.Text = "Asignación de operadores reconteo"
                cmdEliminar.Visible = False
            Case TipoTrans.NoStock
                cargar()
                Me.Text = "Asignación de operadores"
                cmdEliminar.Visible = False
        End Select

    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As EventArgs) Handles cmdEliminar.Click

        If EliminarOperador() Then
            XtraMessageBox.Show("Operador Eliminado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        Else
            XtraMessageBox.Show("No fue posible eliminar", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function EliminarOperador() As Boolean

        EliminarOperador = False

        Try

            Dim Operador As New clsBeTrans_inv_operador

            If Modo = TipoTrans.Nuevo Then

                Operador.Idubic = IdUbicacion
                Operador.Idinventarioenc = IdInventario
                Operador.Idoperador = cmbOperadoresAsignados.EditValue

                clsLnTrans_inv_operador.EliminarByOperador(Operador)

                EliminarOperador = True

            ElseIf Modo = TipoTrans.Eliminar Then


                gBeCiclico.Idoperador = cmbOperadoresAsignados.EditValue

                clsLnTrans_inv_ciclico.EliminarByOperador(gBeCiclico)

                EliminarOperador = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub cargar()
        If Modo = TipoTrans.Nuevo Then
            IMS.Listar_OperadoresByInventario(cmbOperadoresAsignados, IdUbicacion, IdInventario)
        ElseIf Modo = TipoTrans.Editar Then
            IMS.Listar_OperadoresByInventario(cmbOperadoresAsignados, gBeCiclico.IdUbicacion, gBeCiclico.Idinventarioenc)
        ElseIf Modo = TipoTrans.Eliminar Then
            IMS.Listar_OperadoresByStock(cmbOperadoresAsignados, gBeCiclico.IdUbicacion, gBeCiclico.Idinventarioenc, gBeCiclico.IdStock)
        ElseIf Modo = TipoTrans.Reconteo Then
            IMS.Listar_Operadores(cmbOperadoresAsignados)
        ElseIf Modo = TipoTrans.NoStock Then
            IMS.Listar_Operadores(cmbOperadoresAsignados)
        End If

    End Sub

    Private Sub cmdAsignarOperador_Click(sender As Object, e As EventArgs) Handles cmdAsignarOperador.Click

        If Modo = TipoTrans.Reconteo Then
            If InsertarOperadorReconteo() Then
                XtraMessageBox.Show("Operador Asignado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Else
                XtraMessageBox.Show("Operador no asignado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            If InsertarOperador() Then
                XtraMessageBox.Show("Operador Asignado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Else
                XtraMessageBox.Show("Operador no asignado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

    End Sub

    Private Function InsertarOperadorReconteo() As Boolean

        InsertarOperadorReconteo = False

        Try

            gBeReconteo.IdOperador = cmbOperadoresAsignados.EditValue

            clsLnTrans_inv_reconteo.Actualizar(gBeReconteo)

            InsertarOperadorReconteo = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function InsertarOperador() As Boolean

        InsertarOperador = False

        Try

            Dim NuevoStock As New clsBeTrans_inv_ciclico

            If gBeCiclico.Idoperador = 0 Then

                gBeCiclico.Idoperador = cmbOperadoresAsignados.EditValue

                Try

                    clsLnTrans_inv_ciclico.Actualizar(gBeCiclico)

                    InsertarOperador = True

                Catch ex As Exception
                    InsertarOperador = False
                End Try

            Else

                NuevoStock.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID
                NuevoStock.Idinventarioenc = gBeCiclico.Idinventarioenc
                NuevoStock.IdStock = gBeCiclico.IdStock
                NuevoStock.IdProductoBodega = gBeCiclico.IdProductoBodega
                NuevoStock.IdProductoEstado = gBeCiclico.IdProductoEstado
                NuevoStock.IdUbicacion = gBeCiclico.IdUbicacion
                NuevoStock.IdPresentacion = gBeCiclico.IdPresentacion
                NuevoStock.EsNuevo = False
                NuevoStock.Lote_stock = gBeCiclico.Lote_stock
                NuevoStock.Lote = gBeCiclico.Lote
                NuevoStock.Fecha_vence_stock = gBeCiclico.Fecha_vence_stock
                NuevoStock.Fecha_vence = gBeCiclico.Fecha_vence
                NuevoStock.Cant_stock = gBeCiclico.Cant_stock
                NuevoStock.Cantidad = gBeCiclico.Cantidad
                NuevoStock.Cant_reconteo = gBeCiclico.Cant_reconteo
                NuevoStock.Peso_stock = gBeCiclico.Peso_stock
                NuevoStock.Peso = gBeCiclico.Peso
                NuevoStock.Peso_reconteo = gBeCiclico.Peso_reconteo
                NuevoStock.Idoperador = cmbOperadoresAsignados.EditValue
                NuevoStock.User_agr = AP.UsuarioAp.Nombres
                NuevoStock.Fec_agr = Now

                Try

                    clsLnTrans_inv_ciclico.Insertar(NuevoStock)

                    InsertarOperador = True

                Catch ex As Exception
                    InsertarOperador = False
                End Try

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

End Class