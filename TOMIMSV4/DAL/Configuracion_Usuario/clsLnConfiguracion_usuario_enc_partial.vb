Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Partial Public Class clsLnConfiguracion_usuario_enc

    Public Shared Function Guardar_Layout(ByVal pIdEmpresa As Integer,
                                          ByVal pIdUsuario As Integer,
                                          ByVal pHost As String,
                                          ByVal pNombre_Template As String,
                                          ByVal pString_Template As String) As Boolean

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BeConfiguracionUsuario As New clsBeConfiguracion_usuario_enc
            BeConfiguracionUsuario = GetSingle(pIdEmpresa,
                                               pIdUsuario,
                                               lConnection,
                                               lTransaction)

            If BeConfiguracionUsuario Is Nothing Then

                BeConfiguracionUsuario = New clsBeConfiguracion_usuario_enc()
                BeConfiguracionUsuario.IdConfiguracionUsuarioEnc = MaxID(lConnection, lTransaction) + 1
                BeConfiguracionUsuario.IdEmpresa = pIdEmpresa
                BeConfiguracionUsuario.IdUsuario = pIdUsuario
                Insertar(BeConfiguracionUsuario, lConnection, lTransaction)

                Dim BeConfiguracionDet As New clsBeConfiguracion_usuario_det()
                BeConfiguracionDet = clsLnConfiguracion_usuario_det.Get_Single_By_IdUsuario_And_NombreTemplate(pIdEmpresa,
                                                                                                               pIdUsuario,
                                                                                                               pNombre_Template,
                                                                                                               lConnection,
                                                                                                               lTransaction)

                If BeConfiguracionDet Is Nothing Then

                    BeConfiguracionDet = New clsBeConfiguracion_usuario_det()
                    BeConfiguracionDet.IdConfiguracionUsuarioDet = clsLnConfiguracion_usuario_det.MaxID(lConnection, lTransaction) + 1
                    BeConfiguracionDet.IdConfiguracionUsuarioEnc = BeConfiguracionUsuario.IdConfiguracionUsuarioEnc
                    BeConfiguracionDet.Maquina_Host = pHost
                    BeConfiguracionDet.Nombre_Template = pNombre_Template
                    BeConfiguracionDet.String_Template = pString_Template
                    BeConfiguracionDet.Fecha_Agregado = Now
                    BeConfiguracionDet.Fecha_Modificado = Now

                    clsLnConfiguracion_usuario_det.Insertar(BeConfiguracionDet,
                                                            lConnection,
                                                            lTransaction)

                Else

                    BeConfiguracionDet.String_Template = pString_Template
                    BeConfiguracionDet.Fecha_Modificado = Now

                    clsLnConfiguracion_usuario_det.Actualizar_Template(BeConfiguracionDet,
                                                                       lConnection,
                                                                       lTransaction)

                End If

            Else

                Dim BeConfiguracionDet As New clsBeConfiguracion_usuario_det()
                BeConfiguracionDet = clsLnConfiguracion_usuario_det.Get_Single_By_IdUsuario_And_NombreTemplate(pIdEmpresa,
                                                                                                               pIdUsuario,
                                                                                                               pNombre_Template,
                                                                                                               lConnection,
                                                                                                               lTransaction)

                If BeConfiguracionDet Is Nothing Then

                    BeConfiguracionDet = New clsBeConfiguracion_usuario_det()
                    BeConfiguracionDet.IdConfiguracionUsuarioDet = clsLnConfiguracion_usuario_det.MaxID(lConnection, lTransaction) + 1
                    BeConfiguracionDet.IdConfiguracionUsuarioEnc = BeConfiguracionUsuario.IdConfiguracionUsuarioEnc
                    BeConfiguracionDet.Maquina_Host = pHost
                    BeConfiguracionDet.Nombre_Template = pNombre_Template
                    BeConfiguracionDet.String_Template = pString_Template
                    BeConfiguracionDet.Fecha_Agregado = Now
                    BeConfiguracionDet.Fecha_Modificado = Now

                    clsLnConfiguracion_usuario_det.Insertar(BeConfiguracionDet,
                                                            lConnection,
                                                            lTransaction)

                Else

                    BeConfiguracionDet.String_Template = pString_Template
                    BeConfiguracionDet.Fecha_Modificado = Now

                    clsLnConfiguracion_usuario_det.Actualizar_Template(BeConfiguracionDet,
                                                                       lConnection,
                                                                       lTransaction)

                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Layout(ByVal pIdEmpresa As Integer,
                                      ByVal pIdUsuario As Integer,
                                      ByVal pHost As String,
                                      ByVal pNombre_Template As String) As clsBeConfiguracion_usuario_det

        Get_Layout = Nothing

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BeConfiguracionDet As New clsBeConfiguracion_usuario_det()
            BeConfiguracionDet = clsLnConfiguracion_usuario_det.Get_Single_By_IdUsuario_And_NombreTemplate(pIdEmpresa,
                                                                                                           pIdUsuario,
                                                                                                           pNombre_Template,
                                                                                                           lConnection,
                                                                                                           lTransaction)

            If Not BeConfiguracionDet Is Nothing Then

                BeConfiguracionDet.Stream_Template = StringToStream(BeConfiguracionDet.String_Template, Encoding.UTF8)
                Get_Layout = BeConfiguracionDet

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    Public Shared Function Delete_Layout(ByVal pIdEmpresa As Integer,
                                      ByVal pIdUsuario As Integer,
                                      ByVal pHost As String,
                                      ByVal pNombre_Template As String) As clsBeConfiguracion_usuario_det

        Delete_Layout = Nothing

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BeConfiguracionDet As New clsBeConfiguracion_usuario_det()
            BeConfiguracionDet = clsLnConfiguracion_usuario_det.Get_Single_By_IdUsuario_And_NombreTemplate(pIdEmpresa,
                                                                                                           pIdUsuario,
                                                                                                           pNombre_Template,
                                                                                                           lConnection,
                                                                                                           lTransaction)

            If Not BeConfiguracionDet Is Nothing Then

                clsLnConfiguracion_usuario_det.Eliminar(BeConfiguracionDet,
                                                        lConnection,
                                                        lTransaction)

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    'when using this method, put StringToStream in a using construct
    Public Shared Function StringToStream(input As String, enc As Encoding) As Stream
        Dim memoryStream = New MemoryStream()
        Dim streamWriter = New StreamWriter(memoryStream, enc)
        streamWriter.Write(input)
        streamWriter.Flush()
        memoryStream.Position = 0
        Return memoryStream
    End Function

End Class