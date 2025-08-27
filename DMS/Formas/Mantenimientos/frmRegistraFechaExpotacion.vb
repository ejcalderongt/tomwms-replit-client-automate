Imports DevExpress.XtraEditors
Imports TOMWMS.clsHelper

Public Class frmRegistraFechaExpotacion

    'Public listaDuplas As New List(Of DuplaSinFecha)

    'Public Property listaDuplas As List(Of DuplaSinFecha)

    Private _listaOriginal As List(Of DuplaSinFecha)
    Public Property Resultado As List(Of DuplaSinFecha)

    Public Sub New(lista As List(Of DuplaSinFecha))
        InitializeComponent()
        _listaOriginal = lista
    End Sub



    Private Sub frmRegistraFechaExpotacion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            'Dim listaUnica As List(Of DuplaSinFecha) = _listaOriginal _
            '    .GroupBy(Function(x) x.IdPropietario) _
            '    .Select(Function(g) New DuplaSinFecha With {
            '    .Tabla = "", ' Oculta la tabla
            '    .IdPropietario = g.Key,
            '    .FechaSincronizacion = Nothing
            '    }).ToList()

            'Dim listaUnica As List(Of DuplaSinFecha) = _listaOriginal _
            '    .GroupBy(Function(x) New With {x.IdPropietario, x.Nombre}) _
            '    .Select(Function(g) New DuplaSinFecha With {
            '    .Tabla = "", ' Oculta la tabla
            '    .IdPropietario = g.Key.IdPropietario,
            '    .Nombre = g.Key.Nombre,
            '    .FechaSincronizacion = Nothing
            '    }).ToList()

            Dim listaUnica As List(Of DuplaSinFecha) =
                _listaOriginal _
                .GroupBy(Function(x) x.IdPropietario) _
                .Select(Function(g) New DuplaSinFecha With {
                .Tabla = "", ' Oculta la tabla
                .IdPropietario = g.Key,
                .Nombre = g.First().Nombre, ' Tomar el primer nombre encontrado
                .FechaSincronizacion = Nothing
                }).ToList()


            GridControl1.DataSource = listaUnica

            ' Opcional: ocultar la columna "Tabla" visualmente
            GridView1.Columns("Tabla").Visible = False

        Catch ex As Exception

        End Try
    End Sub



    Private Sub Guardar(listaUI As List(Of DuplaSinFecha))

        Dim clsTransaccion As New clsTransaccion()


        Try

            clsTransaccion.Begin_Transaction()

            Dim maxId As Integer = clsLnDMS_Log_sincronizacion_nube.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            For Each dupla In _listaOriginal

                Dim fechaAsignada = listaUI.First(Function(x) x.IdPropietario = dupla.IdPropietario).FechaSincronizacion

                Dim beLog As New clsBeDMS_Log_sincronizacion_nube()
                beLog.IdLog = maxId
                beLog.Fecha_sincronizacion = If(dupla.Tabla.Contains("producto"), New Date(2021, 1, 1), fechaAsignada.Value)
                beLog.Registros_enviados = 0
                beLog.Estado = "Ok"
                beLog.Mensaje_error = "Sincronización inicial"
                beLog.Tiempo_de_envio = 0
                beLog.User_agr = AP.UsuarioAp.IdUsuario
                beLog.Fec_agr = Now
                beLog.Entidad = dupla.Tabla
                beLog.IdPropietario = dupla.IdPropietario

                clsLnDMS_Log_sincronizacion_nube.Insertar(beLog, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                maxId += 1
            Next

            clsTransaccion.Commit_Transaction()


            Resultado = _listaOriginal

            XtraMessageBox.Show("Fechas registradas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show("Error al guardar fechas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick

        Dim listaUI As List(Of DuplaSinFecha) = CType(GridControl1.DataSource, List(Of DuplaSinFecha))

        If listaUI.Any(Function(x) Not x.FechaSincronizacion.HasValue) Then
            XtraMessageBox.Show("Todos los propietarios deben tener una fecha asignada.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Guardar(listaUI)

        DialogResult = DialogResult.OK
        Close()

    End Sub
End Class