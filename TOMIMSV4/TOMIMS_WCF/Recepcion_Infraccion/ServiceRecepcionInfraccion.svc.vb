' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "ServiceRecepcionInfraccion" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceRecepcionInfraccion.svc o ServiceRecepcionInfraccion.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Net.Mail

Public Class ServiceRecepcionInfraccion
    Implements IServiceRecepcionInfraccion

    Public Function Guardar(ByVal pListObjRD As List(Of clsBeTrans_re_det_infraccion)) As Boolean Implements IServiceRecepcionInfraccion.Guardar

        Dim ObjD As New clsLnTrans_re_det_infraccion()

        Dim lConnection As New SqlClient.SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlClient.SqlTransaction = Nothing

        Dim i As Integer = 0

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction()

            ' Recepción Detalle Infracción

            Dim lMaxIdDet As Integer = clsLnTrans_re_det_infraccion.MaxID(lConnection, lTransaction)
            For Each ObjI As clsBeTrans_re_det_infraccion In pListObjRD
                'If clsLnTrans_re_det_infraccion.Exists(ObjI.IdReglaPropietarioEnc, ObjI.IdPresentacion, ObjI.IdProductoBodega, lConnection, lTransaction) = False Then
                If ObjI.IsNew Then
                    lMaxIdDet += 1
                    ObjI.IdRecepcionDetInfraccion = lMaxIdDet
                    ObjD.Insertar(ObjI, lConnection, lTransaction)
                    i += 1
                    'Else
                    '    ObjD.Actualizar(ObjI, lConnection, lTransaction)
                End If
                'End If
            Next

            lTransaction.Commit()
            lConnection.Close()
            Return True

        Catch ex As Exception
            lTransaction.Rollback()
            lConnection.Close()
            Throw New FaultException(ex.Message & " Transacción: " & i.ToString)
        End Try

    End Function


End Class
