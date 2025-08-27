Imports System.Data.SqlClient

Public Class clsLnTrans_re_det_parametros_sc
    Inherits clsLnTrans_re_det_parametros

    'Public Overrides Sub Cargar(ByRef oBeTrans_re_det_parametros As clsBeTrans_re_det_parametros, ByRef dr As DataRow)
    '    Try

    '        MyBase.Cargar(oBeTrans_re_det_parametros, dr)
    '        With oBeTrans_re_det_parametros
    '            .Valor_Unico = IIf(IsDBNull(dr.Item("Valor")), "", dr.Item("Valor"))
    '            .TipoParametro.IdParametro = IIf(IsDBNull(dr.Item("IdParametro")), "", dr.Item("IdParametro"))
    '            .ProductoParametro.IdProductoParametro = IIf(IsDBNull(dr.Item("IdProductoParametro")), "", dr.Item("IdProductoParametro"))
    '            clsLnP_parametro.Obtener(.TipoParametro)
    '            clsLnProducto_parametros.Obtener(.ProductoParametro)
    '        End With
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    Public Overloads Sub Cargar(ByRef oBeTrans_re_det_parametros As clsBeTrans_re_det_parametros,
                                ByRef dr As DataRow,
                                ByVal lConnection As SqlConnection,
                                ByVal lTransaction As SqlTransaction)
        Try

            MyBase.Cargar(oBeTrans_re_det_parametros, dr)

            With oBeTrans_re_det_parametros
                .Valor_Unico = IIf(IsDBNull(dr.Item("Valor")), "", dr.Item("Valor"))
                .TipoParametro.IdParametro = IIf(IsDBNull(dr.Item("IdParametro")), "", dr.Item("IdParametro"))
                .ProductoParametro.IdProductoParametro = IIf(IsDBNull(dr.Item("IdProductoParametro")), "", dr.Item("IdProductoParametro"))
                clsLnP_parametro.Obtener(.TipoParametro, lConnection, lTransaction)
                clsLnProducto_parametros.Obtener(.ProductoParametro, lConnection, lTransaction)
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class
