Imports System.Data.SqlClient

Public Class clsLnVW_ubicaciones_por_regla

    Public Shared Sub Cargar(ByRef oBevw_ubicaciones_por_regla As clsBeVW_ubicaciones_por_regla, ByRef dr As DataRow)
        Try
            With oBevw_ubicaciones_por_regla
                .IdReglaUbicacionEnc = IIf(IsDBNull(dr.Item("IdReglaUbicacionEnc")), 0, dr.Item("IdReglaUbicacionEnc"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .Indice_x = IIf(IsDBNull(dr.Item("indice_x")), 0, dr.Item("indice_x"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                .Dañado = IIf(IsDBNull(dr.Item("dañado")), False, dr.Item("dañado"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Bloqueada = IIf(IsDBNull(dr.Item("bloqueada")), False, dr.Item("bloqueada"))
                .Acepta_pallet = IIf(IsDBNull(dr.Item("acepta_pallet")), False, dr.Item("acepta_pallet"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .Regla_ubic_det_prop_Activo = IIf(IsDBNull(dr.Item("regla_ubic_det_prop_Activo")), False, dr.Item("regla_ubic_det_prop_Activo"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdIndiceRotacionRegla = IIf(IsDBNull(dr.Item("IdIndiceRotacionRegla")), 0, dr.Item("IdIndiceRotacionRegla"))
                .IdTipoRotacionRegla = IIf(IsDBNull(dr.Item("IdTipoRotacionRegla")), 0, dr.Item("IdTipoRotacionRegla"))
                .IdTipoProducto = IIf(IsDBNull(dr.Item("IdTipoProducto")), 0, dr.Item("IdTipoProducto"))
                .Regla_ubic_det_tp_Activo = IIf(IsDBNull(dr.Item("regla_ubic_det_tp_Activo")), False, dr.Item("regla_ubic_det_tp_Activo"))
                .IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
                .Regla_ubic_det_pe_Activo = IIf(IsDBNull(dr.Item("regla_ubic_det_pe_Activo")), False, dr.Item("regla_ubic_det_pe_Activo"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Nombre_Completo = IIf(IsDBNull(dr.Item("Nombre_Completo")), "", dr.Item("Nombre_Completo"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeVW_ubicaciones_por_regla)

        Dim lReturnList As New List(Of clsBeVW_ubicaciones_por_regla)

        Try

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBevw_ubicaciones_por_regla As New clsBeVW_ubicaciones_por_regla

                        For Each dr As DataRow In lDataTable.Rows
                            vBevw_ubicaciones_por_regla = New clsBeVW_ubicaciones_por_regla()
                            Cargar(vBevw_ubicaciones_por_regla, dr)
                            lReturnList.Add(vBevw_ubicaciones_por_regla)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBevw_ubicaciones_por_regla As clsBeVW_ubicaciones_por_regla)

        Try

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBevw_ubicaciones_por_regla As New clsBeVW_ubicaciones_por_regla

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBevw_ubicaciones_por_regla, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_All(ByVal IdBodega As Integer) As List(Of clsBeVW_ubicaciones_por_regla)

        Dim lReturnList As New List(Of clsBeVW_ubicaciones_por_regla)

        Try

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla WHERE IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBevw_ubicaciones_por_regla As New clsBeVW_ubicaciones_por_regla

                        For Each dr As DataRow In lDataTable.Rows
                            vBevw_ubicaciones_por_regla = New clsBeVW_ubicaciones_por_regla()
                            Cargar(vBevw_ubicaciones_por_regla, dr)
                            lReturnList.Add(vBevw_ubicaciones_por_regla)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All(ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_ubicaciones_por_regla)

        Dim lReturnList As New List(Of clsBeVW_ubicaciones_por_regla)

        Try

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla WHERE IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBevw_ubicaciones_por_regla As New clsBeVW_ubicaciones_por_regla

                For Each dr As DataRow In lDataTable.Rows
                    vBevw_ubicaciones_por_regla = New clsBeVW_ubicaciones_por_regla()
                    Cargar(vBevw_ubicaciones_por_regla, dr)
                    lReturnList.Add(vBevw_ubicaciones_por_regla)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class
