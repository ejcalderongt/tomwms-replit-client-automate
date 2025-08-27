Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnVW_Impresion_Pallet

    Public Shared Sub Cargar(ByRef oBeT_VW_Impresion_Pallet As clsBeVW_Impresion_Pallet, ByRef dr As DataRow)
        Try
            With oBeT_VW_Impresion_Pallet
                .IdStockRec = IIf(IsDBNull(dr.Item("IdStockRec")), 0, dr.Item("IdStockRec"))
                .Rec_No = IIf(IsDBNull(dr.Item("Rec_No")), 0, dr.Item("Rec_No"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Empresa = IIf(IsDBNull(dr.Item("Empresa")), "", dr.Item("Empresa"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Propietario_Nombre = IIf(IsDBNull(dr.Item("Propietario_Nombre")), "", dr.Item("Propietario_Nombre"))
                .Bodega = IIf(IsDBNull(dr.Item("Bodega")), "", dr.Item("Bodega"))
                .Proveedor_Nombre = IIf(IsDBNull(dr.Item("Proveedor_Nombre")), "", dr.Item("Proveedor_Nombre"))
                .Proveedor_Codigo = IIf(IsDBNull(dr.Item("Proveedor_Codigo")), "", dr.Item("Proveedor_Codigo"))
                .Proveedor_Tel = IIf(IsDBNull(dr.Item("Proveedor_Tel")), "", dr.Item("Proveedor_Tel"))
                .Proveedor_Dir = IIf(IsDBNull(dr.Item("Proveedor_Dir")), "", dr.Item("Proveedor_Dir"))
                .Producto_Codigo = IIf(IsDBNull(dr.Item("Producto_Codigo")), "", dr.Item("Producto_Codigo"))
                .Producto_Nombre_Largo = IIf(IsDBNull(dr.Item("Producto_Nombre_Largo")), "", dr.Item("Producto_Nombre_Largo"))
                .Producto_UM = IIf(IsDBNull(dr.Item("Producto_UM")), "", dr.Item("Producto_UM"))
                .Producto_Presentacion = IIf(IsDBNull(dr.Item("Producto_Presentacion")), "", dr.Item("Producto_Presentacion"))
                .Producto_Cantidad = IIf(IsDBNull(dr.Item("Producto_Cantidad")), 0.0, dr.Item("Producto_Cantidad"))
                .Producto_Peso = IIf(IsDBNull(dr.Item("Producto_Peso")), 0.0, dr.Item("Producto_Peso"))
                .Producto_Vence = IIf(IsDBNull(dr.Item("Producto_Vence")), Date.Now, dr.Item("Producto_Vence"))
                .Producto_Lote = IIf(IsDBNull(dr.Item("Producto_Lote")), "", dr.Item("Producto_Lote"))
                .Producto_Estado = IIf(IsDBNull(dr.Item("Producto_Estado")), "", dr.Item("Producto_Estado"))
                .LP = IIf(IsDBNull(dr.Item("LP")), "", dr.Item("LP"))
                .PC = IIf(IsDBNull(dr.Item("PC")), "", dr.Item("PC"))
                .Ref_PC = IIf(IsDBNull(dr.Item("Ref_PC")), "", dr.Item("Ref_PC"))
                .Fecha_PC = IIf(IsDBNull(dr.Item("Fecha_PC")), Date.Now, dr.Item("Fecha_PC"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .Rec_Tipo_Albaran = "" ' IIf(IsDBNull(dr.Item("Rec_Tipo_Albaran")), "", dr.Item("Rec_Tipo_Albaran"))
                .Imprimio = IIf(IsDBNull(dr.Item("Imprimio")), "", dr.Item("Imprimio"))
                .Fecha_Produccion = IIf(IsDBNull(dr.Item("Fecha_Manufactura")), Date.Now, dr.Item("Fecha_Manufactura"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM T_VW_Impresion_Pallet"
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

    Public Shared Function Obtener(ByRef oBeT_VW_Impresion_Pallet As clsBeVW_Impresion_Pallet) As Boolean

        Try

            Const sp As String = "SELECT * FROM T_VW_Impresion_Pallet"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeT_VW_Impresion_Pallet, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeVW_Impresion_Pallet)

        Try

            Dim lReturnList As New List(Of clsBeVW_Impresion_Pallet)
            Const sp As String = "SELECT * FROM T_VW_Impresion_Pallet"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeT_VW_Impresion_Pallet As New clsBeVW_Impresion_Pallet

            For Each dr As DataRow In dt.Rows
                vBeT_VW_Impresion_Pallet = New clsBeVW_Impresion_Pallet
                Cargar(vBeT_VW_Impresion_Pallet, dr)
                lReturnList.Add(vBeT_VW_Impresion_Pallet)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeT_VW_Impresion_Pallet As clsBeVW_Impresion_Pallet)

        Try

            Const sp As String = "SELECT * FROM T_VW_Impresion_Pallet"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeT_VW_Impresion_Pallet, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
