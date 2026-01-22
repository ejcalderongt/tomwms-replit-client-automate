Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLn_vw_ajustes

    Public Shared Sub Cargar(ByRef oBeT_vw_ajustes As clsBe_vw_ajustes, ByRef dr As DataRow)

        Try

            With oBeT_vw_ajustes

                .IdAjusteEnc = IIf(IsDBNull(dr.Item("IdAjusteEnc")), 0, dr.Item("IdAjusteEnc"))
                .IdAjusteDet = IIf(IsDBNull(dr.Item("IdAjusteDet")), 0, dr.Item("IdAjusteDet"))
                .Fecha = IIf(IsDBNull(dr.Item("Fecha")), Nothing, dr.Item("Fecha"))
                .Referencia = IIf(IsDBNull(dr.Item("Referencia")), "", dr.Item("Referencia"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("Codigo_Producto")), "", dr.Item("Codigo_Producto"))
                .Nombre_Producto = IIf(IsDBNull(dr.Item("Nombre_Producto")), "", dr.Item("Nombre_Producto"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .UMBas = IIf(IsDBNull(dr.Item("UMBas")), "", dr.Item("UMBas"))
                .IdBodegaERP = IIf(IsDBNull(dr.Item("IdBodegaERP")), 0, dr.Item("IdBodegaERP"))
                .Codigo_Bodega = IIf(IsDBNull(dr.Item("Codigo_Bodega")), "", dr.Item("Codigo_Bodega"))
                .Nombre_Bodega = IIf(IsDBNull(dr.Item("Nombre_Bodega")), "", dr.Item("Nombre_Bodega"))
                .Cantidad_original = IIf(IsDBNull(dr.Item("cantidad_original")), 0.0, dr.Item("cantidad_original"))
                .Cantidad_nueva = IIf(IsDBNull(dr.Item("cantidad_nueva")), 0.0, dr.Item("cantidad_nueva"))
                .Peso_nuevo = IIf(IsDBNull(dr.Item("peso_nuevo")), 0.0, dr.Item("peso_nuevo"))
                .Peso_original = IIf(IsDBNull(dr.Item("peso_original")), 0.0, dr.Item("peso_original"))
                .Fecha_vence_nueva = IIf(IsDBNull(dr.Item("fecha_vence_nueva")), Date.Now, dr.Item("fecha_vence_nueva"))
                .Fecha_vence_original = IIf(IsDBNull(dr.Item("fecha_vence_original")), Date.Now, dr.Item("fecha_vence_original"))
                .Lote_Original = IIf(IsDBNull(dr.Item("Lote_Original")), "", dr.Item("Lote_Original"))
                .Lote_Nuevo = IIf(IsDBNull(dr.Item("Lote_Nuevo")), "", dr.Item("Lote_Nuevo"))
                .Tipo_Ajuste = IIf(IsDBNull(dr.Item("Tipo_Ajuste")), "", dr.Item("Tipo_Ajuste"))
                .Modifica_Cantidad = IIf(IsDBNull(dr.Item("Modifica_Cantidad")), False, dr.Item("Modifica_Cantidad"))
                .Enviado = IIf(IsDBNull(dr.Item("Enviado")), False, dr.Item("Enviado"))
                .Motivo_Ajuste = IIf(IsDBNull(dr.Item("Motivo_Ajuste")), "", dr.Item("Motivo_Ajuste"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .IdProductoFamilia = IIf(IsDBNull(dr.Item("IdProductoFamilia")), 0, dr.Item("IdProductoFamilia"))
                .Nombre_Presentacion = IIf(IsDBNull(dr.Item("Nombre_Presentacion")), "", dr.Item("Nombre_Presentacion"))
                .Factor = IIf(IsDBNull(dr.Item("Factor")), 0, dr.Item("Factor"))
                .Codigo_Centro_Costo = IIf(IsDBNull(dr.Item("Codigo_Centro_Costo")), 0, dr.Item("Codigo_Centro_Costo"))
                .Nombre_Centro_Costo = IIf(IsDBNull(dr.Item("Nombre_Centro_Costo")), 0, dr.Item("Nombre_Centro_Costo"))
                .Talla = IIf(IsDBNull(dr.Item("Talla")), "", dr.Item("Talla"))
                .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))
                .User_Agr = IIf(IsDBNull(dr.Item("User_Agr")), "", dr.Item("User_Agr"))
                .Centro_Costo_Erp = IIf(IsDBNull(dr.Item("Centro_Costo_Erp")), "", dr.Item("Centro_Costo_Erp"))
                .Centro_Costo_Dir_Erp = IIf(IsDBNull(dr.Item("Centro_Costo_Dir_Erp")), "", dr.Item("Centro_Costo_Dir_Erp"))
                .Centro_Costo_Dep_Erp = IIf(IsDBNull(dr.Item("Centro_Costo_Dep_Erp")), "", dr.Item("Centro_Costo_Dep_Erp"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM vw_ajustes"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeT_vw_ajustes As clsBe_vw_ajustes) As Boolean

        Try

            Const sp As String = "SELECT * FROM vw_ajustes"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeT_vw_ajustes, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBe_vw_ajustes)

        Try

            Dim lReturnList As New List(Of clsBe_vw_ajustes)
            Const sp As String = "SELECT * FROM vw_ajustes"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeT_vw_ajustes As New clsBe_vw_ajustes

            For Each dr As DataRow In dt.Rows
                vBeT_vw_ajustes = New clsBe_vw_ajustes
                Cargar(vBeT_vw_ajustes, dr)
                lReturnList.Add(vBeT_vw_ajustes)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeT_vw_ajustes As clsBe_vw_ajustes)

        Try

            Const sp As String = "SELECT * FROM vw_ajustes"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeT_vw_ajustes, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
