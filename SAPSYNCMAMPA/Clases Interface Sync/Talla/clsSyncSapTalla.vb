Imports System.Data.SqlClient
Imports Sap.Data.Hana
Public Class clsSyncSapTalla
    Implements IDisposable

    Private disposedValue As Boolean

    Public Shared Function Get_Tallas_From_Sap_Hana(BeConfig As clsBeI_nav_config_enc,
                                                    lblprg As RichTextBox,
                                                    prg As ProgressBar) As Boolean
        Get_Tallas_From_Sap_Hana = False
        Dim clsTrans As New clsTransaccion()
        Dim i As Integer = 0

        clsPublic.Actualizar_Progreso(lblprg, "Iniciando importación de Tallas.")

        Try
            Using conn As HanaConnection = HanaHelper.OpenDB()

                Dim query As String = ConstruirQueryTalla()
                Dim dt As DataTable = HanaHelper.OpenDT(query, conn)

                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    clsPublic.Actualizar_Progreso(lblprg, "No se encontraron tallas.")
                    Return False
                End If

                clsTrans.Begin_Transaction()
                prg.Maximum = dt.Rows.Count
                prg.Visible = True

                For Each row As DataRow In dt.Rows
                    Try
                        Dim BeTalla = MapearTallaDesdeRow(row, BeConfig, clsTrans.lConnection, clsTrans.lTransaction)
                        If Not clsLnTalla.Existe_By_Codigo(BeTalla.Codigo, clsTrans.lConnection, clsTrans.lTransaction) Then
                            clsLnTalla.Insertar(BeTalla, clsTrans.lConnection, clsTrans.lTransaction)
                        End If

                        clsPublic.Actualizar_Progreso(lblprg, $"Procesando Talla: {BeTalla.Codigo} - {BeTalla.Nombre}")
                        prg.Value = i : i += 1
                    Catch exRow As Exception
                        clsPublic.Actualizar_Progreso(lblprg, $"Error en talla: {exRow.Message}")
                    End Try
                Next

                clsTrans.Commit_Transaction()
                clsPublic.Actualizar_Progreso(lblprg, $"Fin de importación de Tallas: {Now}")
                Get_Tallas_From_Sap_Hana = True

            End Using

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            clsPublic.Actualizar_Progreso(lblprg, $"Error general: {ex.Message}")
            Throw
        Finally
            prg.Visible = False
        End Try

    End Function

    Public Shared Function Insertar_Talla_From_Sap_Hana(codigo As String, lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer
        Insertar_Talla_From_Sap_Hana = 0
        Try
            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)
            Dim query As String = ConstruirQueryTalla(codigo)
            Using conn As HanaConnection = HanaHelper.OpenDB()
                Dim dt As DataTable = HanaHelper.OpenDT(query, conn)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim row As DataRow = dt.Rows(0)
                    Dim BeTalla = MapearTallaDesdeRow(row, BeConfigEnc, lConnection, lTransaction)
                    If Not clsLnTalla.Existe_By_Codigo(BeTalla.Codigo, lConnection, lTransaction) Then
                        clsLnTalla.Insertar(BeTalla, lConnection, lTransaction)
                        Insertar_Talla_From_Sap_Hana = BeTalla.IdTalla
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Function MapearTallaDesdeRow(row As DataRow, config As clsBeI_nav_config_enc,
                                                conn As SqlConnection, tran As SqlTransaction) As clsBeTalla
        Return New clsBeTalla With {
            .IdTalla = clsLnTalla.MaxID(conn, tran) + 1,
            .Codigo = row("CODE").ToString(),
            .Nombre = row("NAME").ToString(),
            .Descripcion = row("NAME").ToString(),
            .IdPropietario = config.IdPropietario,
            .Fec_agr = Now,
            .Fec_mod = Now,
            .User_agr = config.User_agr,
            .User_mod = config.User_mod,
            .Activo = True
        }
    End Function

    Private Shared Function ConstruirQueryTalla(Optional codigo As String = "") As String
        Dim baseQuery As String = "SELECT * FROM ""@TALLAS"""
        If Not String.IsNullOrWhiteSpace(codigo) Then
            baseQuery &= $" WHERE ""Code"" = '{codigo}'"
        End If
        Return baseQuery
    End Function


    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
