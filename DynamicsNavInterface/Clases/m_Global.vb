Imports System.Reflection

Module m_Global

    Public BD As New BaseDatos

    Public IndiceInstanciaDefecto As Integer = -1

    Public IdUsuario As Integer = -1

    Public Property BeConfigEnc As New clsBeI_nav_config_enc
    Public Property RemoteCallBack As Boolean = False
    Public Property wsTOMHHInstance As WebReference.TOMHHWS
    Public Property gVersionApp As String = "7.9.4"

    Public Property gFechaVersion As Date = New Date(2025, 11, 14)

    Public Enum pInterfaceAEjecutar
        Ninguna = -1
        Importar_Bodegas = 0
        Importar_Productos = 1
        Importar_Proveedores = 2
        Importar_Pedidos_De_Compra = 3
        Importar_Pedidos_De_Transferencia = 4
        Enviar_Pedidos_Compra = 5
        Enviar_Pedidos_Transferencia = 6
        Interface_ROAD = 1989
    End Enum

    Public Sub CopyObject(Of tom)(ByVal ObjOrigen As Object, ByRef ObjDestino As tom)

        Dim pName As String = ""

        Try

            If ObjOrigen Is Nothing OrElse ObjDestino Is Nothing Then Return
            Dim TipoFuente As Type = ObjOrigen.[GetType]()
            Dim TipoDestino As Type = ObjDestino.[GetType]()

            If TipoFuente IsNot Nothing AndAlso TipoDestino IsNot Nothing Then

                For Each p As PropertyInfo In TipoFuente.GetProperties()

                    pName = p.Name

                    Dim ObjPI As PropertyInfo = TipoDestino.GetProperty(p.Name)

                    If ObjPI IsNot Nothing Then
                        Dim l As Object = p.GetValue(ObjOrigen, Nothing)
                        ObjPI.SetValue(ObjDestino, l)
                        ObjPI.SetValue(ObjDestino, p.GetValue(ObjOrigen, Nothing), Nothing)
                    End If

                Next

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} Campo: {2} ", MethodBase.GetCurrentMethod.Name(), ex.Message, pName))
        End Try

    End Sub

    Public Function ToDataTable(Of T)(items As List(Of T)) As DataTable
        Dim dataTable As New DataTable(GetType(T).Name)

        'Get all the properties
        Dim Props As PropertyInfo() = GetType(T).GetProperties(BindingFlags.[Public] Or BindingFlags.Instance)
        For Each prop As PropertyInfo In Props
            'Setting column names as Property names
            dataTable.Columns.Add(prop.Name)
        Next
        For Each item As T In items
            Dim values = New Object(Props.Length - 1) {}
            For i As Integer = 0 To Props.Length - 1
                'inserting property values to datatable rows
                values(i) = Props(i).GetValue(item, Nothing)
            Next
            dataTable.Rows.Add(values)
        Next
        'put a breakpoint here and check datatable
        Return dataTable
    End Function

    Public Function Existe_Ini() As Boolean

        If IO.File.Exists(CurDir() & "\Conn.ini") Then
            Existe_Ini = True
        Else
            Existe_Ini = False
        End If

    End Function

End Module