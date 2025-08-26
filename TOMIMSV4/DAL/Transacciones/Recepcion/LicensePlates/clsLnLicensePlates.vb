Imports System.Data.SqlClient

Public Class clsLnLicensePlates


    Public Shared Function Get_Licenses_Plates_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeLicensePlates)

        Dim lReturnList As New List(Of clsBeLicensePlates)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = " SELECT S.IdRecepcionEnc, S.IdProductoBodega, ISNULL(S.IdPresentacion,0) AS IdPresentacion, " &
                               " S.Lic_Plate AS LicensePlates, SUM(S.Cantidad) AS CantidadUnidadBasica, " &
                               " ISNULL(SUM(S.Cantidad/P.FACTOR),0) AS CantidadPresentacion, " &
                               " MAX(P.CamasPorTarima * P.CajasPorCama) CantidadMaximaPresentacion, " &
                               " MAX(P.CamasPorTarima * P.CajasPorCama)-ISNULL(SUM(S.Cantidad/P.Factor),0) AS CantidadDisponible " &
                               " FROM stock_rec S LEFT OUTER JOIN producto_presentacion P ON S.IdPresentacion = P.IdPresentacion " &
                               " WHERE S.IdRecepcionEnc = @IdRecepcionEnc AND P.EsPallet = 1 " &
                               " GROUP BY S.IdRecepcionEnc, S.IdProductoBodega, S.IdPresentacion, S.Lic_Plate"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", pIdRecepcionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Dim Obj As clsBeLicensePlates

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each lRow As DataRow In dt.Rows
                    Obj = New clsBeLicensePlates
                    Cargar(Obj, lRow)
                    lReturnList.Add(Obj)
                Next

            End If

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Sub Cargar(ByRef oBeLicensePlates As clsBeLicensePlates, ByRef dr As DataRow)

        Try

            With oBeLicensePlates
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IDPRODUCTOBODEGA")), 0, dr.Item("IDPRODUCTOBODEGA"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .LicensePlates = IIf(IsDBNull(dr.Item("LicensePlates")), 0, dr.Item("LicensePlates"))
                .CantidadUnidadBasica = IIf(IsDBNull(dr.Item("CantidadUnidadBasica")), 0, dr.Item("CantidadUnidadBasica"))
                .CantidadMaximaPresentacion = IIf(IsDBNull(dr.Item("CantidadMaximaPresentacion")), 0, dr.Item("CantidadMaximaPresentacion"))
                .CantidadPresentacion = IIf(IsDBNull(dr.Item("CantidadPresentacion")), 0, dr.Item("CantidadPresentacion"))
                .CantidadDisponible = IIf(IsDBNull(dr.Item("CantidadDisponible")), 0, dr.Item("CantidadDisponible"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class
