Imports DevExpress.XtraEditors

Public Class frmTablasRelacionadas

    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public ListObjTablasConRelacion As List(Of clsBeTablasRelacionadas)

    Public Property IdEmpresa As Integer = 0

    Private Sub frmTablasRelacionadas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listarTablasConRelacion()
    End Sub

    Private Sub listarTablasConRelacion()

        Try

            Dim contador As Integer = 1
            Dim CantRegs As Integer = 0

            If ListObjTablasConRelacion.Count > 0 Then

                For Each Obj As clsBeTablasRelacionadas In ListObjTablasConRelacion

                    'CantRegs=  clsLnEmpresa.Get_Cantidad_Registros_Tabla_Relacionada(IdEmpresa,Obj.NombreTabla)
                    CantRegs = Obj.NoRelaciones

                    listReferencias.Items.Add(String.Format("{0}. {1} - Regs: {2}", contador, Obj.NombreTabla.ToUpper(), CantRegs))

                    contador += 1

                Next

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

End Class