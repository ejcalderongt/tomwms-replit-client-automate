Imports System.Reflection
Imports TOMWMS.WSPaginaLotes

Public Class clsSyncLotes : Inherits clsInterfaceBase

    Private Shared ListaLotes() As Pagina_lotes

    Private Shared wsLotes As New Pagina_lotes_Service() With
        {
        .UseDefaultCredentials = UsarCredencialesPorDefecto,
        .Credentials = CredencialesConexion
        }

    Public Shared Function Get_Lista_Lotes(ByVal NoPedidoTransf As String,
                                           ByVal CodigoProducto As String,
                                           ByVal NumeroLinea As Integer) As List(Of Pagina_lotes)

        Get_Lista_Lotes = Nothing

        Try

            Dim vFiltro2 As New Pagina_lotes_Filter() With {.Field = Pagina_lotes_Fields.Source_ID, .Criteria = NoPedidoTransf}
            Dim vFiltro1 As New Pagina_lotes_Filter() With {.Field = Pagina_lotes_Fields.Item_No, .Criteria = CodigoProducto}
            Dim vFiltro0 As New Pagina_lotes_Filter() With {.Field = Pagina_lotes_Fields.Source_Prod_Order_Line, .Criteria = NumeroLinea}
            Dim vFiltros As Pagina_lotes_Filter() = New Pagina_lotes_Filter() {vFiltro2, vFiltro1, vFiltro0}

            wsLotes.Url = BD.Instancia.URLLotesTransfRec

            ListaLotes = wsLotes.ReadMultiple(vFiltros, Nothing, 0)

            Return ListaLotes.ToList()

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Get_Lista_Lotes(ByVal NoPedidoTransf As String) As List(Of Pagina_lotes)

        Get_Lista_Lotes = Nothing

        Try

            Dim vFiltro2 As New Pagina_lotes_Filter() With {.Field = Pagina_lotes_Fields.Source_ID, .Criteria = NoPedidoTransf}
            Dim vFiltros As Pagina_lotes_Filter() = New Pagina_lotes_Filter() {vFiltro2}

            ListaLotes = wsLotes.ReadMultiple(vFiltros, Nothing, 0)

            Return ListaLotes.ToList()

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Lista_Lotes(ByVal NoPedidoTransf As String,
                                           ByVal CodigoProducto As String) As List(Of Pagina_lotes)

        Get_Lista_Lotes = Nothing

        Try

            Dim vFiltro2 As New Pagina_lotes_Filter() With {.Field = Pagina_lotes_Fields.Source_ID, .Criteria = NoPedidoTransf}
            Dim vFiltro1 As New Pagina_lotes_Filter() With {.Field = Pagina_lotes_Fields.Item_No, .Criteria = CodigoProducto}
            Dim vFiltros As Pagina_lotes_Filter() = New Pagina_lotes_Filter() {vFiltro2, vFiltro1}

            wsLotes.Url = BD.Instancia.URLLotesTransfRec

            ListaLotes = wsLotes.ReadMultiple(vFiltros, Nothing, 0)

            Return ListaLotes.ToList()

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class