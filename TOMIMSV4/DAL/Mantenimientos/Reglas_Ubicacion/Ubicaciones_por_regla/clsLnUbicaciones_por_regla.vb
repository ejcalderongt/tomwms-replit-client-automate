Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnUbicaciones_por_regla

    Public Shared Sub Cargar(ByRef oBevw_ubicaciones_por_regla As clsBeUbicaciones_por_regla, ByRef dr As DataRow)

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
            End With


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBevw_ubicaciones_por_regla As clsBeUbicaciones_por_regla) As Boolean

        Try

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBevw_ubicaciones_por_regla, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeUbicaciones_por_regla)

        Try

            Dim lReturnList As New List(Of clsBeUbicaciones_por_regla)
            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBevw_ubicaciones_por_regla As New clsBeUbicaciones_por_regla

            For Each dr As DataRow In dt.Rows

                vBevw_ubicaciones_por_regla = New clsBeUbicaciones_por_regla
                Cargar(vBevw_ubicaciones_por_regla, dr)
                lReturnList.Add(vBevw_ubicaciones_por_regla)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBevw_ubicaciones_por_regla As clsBeUbicaciones_por_regla)

        Try

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBevw_ubicaciones_por_regla, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM vw_ubicaciones_por_regla"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
