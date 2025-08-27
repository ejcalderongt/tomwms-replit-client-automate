Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Compatibility
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS

Public Class clsLnPropietarioDMS

    Public Shared Function Get_All_UX() As List(Of clsBePropietarios)

        Get_All_UX = Nothing

        Try


            Get_All_UX = clsLnPropietarios.Get_Propietarios_By_UX()

            'Dim propietarios As List(Of clsBePropietarios) = clsLnPropietarios.Get_Propietarios_By_UX()
            'Dim listaIds As New List(Of Integer)

            'For Each p In propietarios
            '    listaIds.Add(p.IdPropietario)
            'Next

            'Return listaIds

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try
    End Function

    Public Shared Function Get_Propietarios_Bodega_By_IdPropietario(ByVal pPropietario As List(Of Integer)) As List(Of Integer)
        Try
            Dim listaIds As New List(Of Integer)

            For Each pIdPropietario In pPropietario
                Dim PropetarioBodega As List(Of clsBePropietario_bodega) = clsLnPropietario_bodega.Get_All_By_IdPropietario(pIdPropietario)

                For Each p In PropetarioBodega
                    listaIds.Add(p.IdPropietarioBodega)
                Next

            Next

            Return listaIds

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try
    End Function

End Class
